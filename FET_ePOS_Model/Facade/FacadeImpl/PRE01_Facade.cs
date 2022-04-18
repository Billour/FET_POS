using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data;
using System.Data.OracleClient;
using Advtek.Utility;
using FET.POS.Model.DTO;

namespace FET.POS.Model.Facade.FacadeImpl
{
    public class PRE01_Facade
    {
        //查詢當日是否有未結資料
        public DataTable Query_PREPAY_PAID_CACHE(string MACHINE_ID, string STORE_NO)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(@"SELECT PAID_MODE,PAID_AMOUNT FROM PREPAY_PAID_CACHE  ");
            sb.AppendLine(" WHERE trunc(CREATE_DTM)=trunc(sysdate) ");
            sb.AppendLine(" AND MACHINE_ID = " + OracleDBUtil.SqlStr(MACHINE_ID));
            sb.AppendLine(" AND STORE_NO = " + OracleDBUtil.SqlStr(STORE_NO));
            DataTable dt = OracleDBUtil.Query_Data(StringUtil.CStr(sb));
            return dt;
        }

        //查詢PREPAY_HEAD
        public DataTable Query_PREPAY_HEAD(string PREPAY_NO)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(@" SELECT ID,PREPAY_NO,ID_NO,PREPAY_STATUS 
            ,decode(PREPAY_STATUS,'1','未存檔','2','已結帳','3','已作廢') PREPAY_STATUS_NAME
            ,INVOICE_NO,CUST_NAME,TO_CHAR(MODI_DTM,'YYYY/MM/DD HH:mm:ss') MODI_DTM
            ,UNI_TITLE,MSISDN,MODI_USER,UNI_NO,CONTACT_PHONE,START_TYPE,EMAIL
            FROM PREPAY_HEAD ");
            sb.AppendLine(" WHERE 1=1 ");
            sb.AppendLine(" AND PREPAY_NO = " + OracleDBUtil.SqlStr(PREPAY_NO));
            DataTable dt = OracleDBUtil.Query_Data(StringUtil.CStr(sb));
            return dt;

        }

        //查詢PREPAY_ITEM
        public DataTable Query_PREPAY_ITEM(string PREPAY_HEAD_ID)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(@" SELECT ID,PRODNO,
                NVL((SELECT PRODNAME FROM PRODUCT P WHERE I.PRODNO=P.PRODNO ),'') PRODNAME,
                UNIT_PRICE,QUANTITY,DESCRIPTION,AMOUNT,REMARK
            FROM PREPAY_ITEM I ");
            sb.AppendLine(" WHERE 1=1 ");
            sb.AppendLine(" AND PREPAY_HEAD_ID = " + OracleDBUtil.SqlStr(PREPAY_HEAD_ID));
            DataTable dt = OracleDBUtil.Query_Data(StringUtil.CStr(sb));
            return dt;

        }

        //查詢PREPAY_PAID
        public DataTable Query_PREPAY_PAID(string PREPAY_HEAD_ID)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(@" SELECT ID,PAID_MODE,
                             decode(PAID_MODE,'1','現金','2','信用卡','3','離線信用卡','6','金融卡') PAID_MODE_NAME,
                             PAID_AMOUNT,DESCRIPTION,
                             CREDIT_TYPE,CREDIT_CARD_NO,CREDIT_CARD_AUTH_NO,
                             CREDIT_CARD_TYPE_ID,CREDID_CARD_TYPE_NAME
                             FROM PREPAY_PAID ");
            sb.AppendLine(" WHERE 1=1 ");
            sb.AppendLine(" AND PREPAY_HEAD_ID = " + OracleDBUtil.SqlStr(PREPAY_HEAD_ID));
            DataTable dt = OracleDBUtil.Query_Data(StringUtil.CStr(sb));
            return dt;

        }

        /// <summary>
        /// 刪除PREPAY_PAID_CACHE
        /// </summary>
        /// <param name="D_Type"></param>       ONE刪除一筆ALL刪除當日全部
        /// <param name="ID"></param>           
        /// <param name="MACHINE_ID"></param>   
        /// <param name="STORE_NO"></param>     
        public void Del_PREPAY_PAID_CACHE(string D_Type, string ID, string MACHINE_ID, string STORE_NO)
        {
            OracleConnection objConn = null;
            OracleTransaction objTX = null;
            try
            {
                objConn = OracleDBUtil.GetConnection();
                objTX = objConn.BeginTransaction();

                string strSql = "";
                strSql = "  DELETE FROM PREPAY_PAID_CACHE";
                if (D_Type == "ONE")
                {
                    strSql += " WHERE PREPAY_PAID_ID = " + OracleDBUtil.SqlStr(ID);
                }
                else
                {
                    strSql += " WHERE trunc(CREATE_DTM)=trunc(sysdate)";
                    strSql += " AND MACHINE_ID = " + OracleDBUtil.SqlStr(MACHINE_ID);
                    strSql += " AND STORE_NO = " + OracleDBUtil.SqlStr(STORE_NO);
                }
                OracleDBUtil.ExecuteSql(objTX, strSql);
                objTX.Commit();
            }
            catch (Exception ex)
            {
                objTX.Rollback();
                throw ex;
            }
            finally
            {
                objTX.Dispose();
                if (objConn.State == ConnectionState.Open) objConn.Close();
                objConn.Dispose();
                OracleConnection.ClearAllPools();
            }
        }

        /// <summary>
        /// 刪除PREPAY_PAID_CACHE
        /// </summary>
        /// <param name="PREPAY_PAID"></param>       刪除PREPAY_PAID中的PREPAY_PAID_CACHE
        public void Del_PREPAY_PAID_CACHE(DataTable PREPAY_PAID)
        {
            OracleConnection objConn = null;
            OracleTransaction objTX = null;
            try
            {
                objConn = OracleDBUtil.GetConnection();
                objTX = objConn.BeginTransaction();
                string strSql = "";
                foreach (DataRow dr in PREPAY_PAID.Rows)
                {
                    strSql = "  DELETE FROM PREPAY_PAID_CACHE";
                    strSql += " WHERE PREPAY_PAID_ID = " + OracleDBUtil.SqlStr(StringUtil.CStr(dr["ID"]));
                    OracleDBUtil.ExecuteSql(objTX, strSql);
                }
                objTX.Commit();
            }
            catch (Exception ex)
            {
                objTX.Rollback();
                throw ex;
            }
            finally
            {
                objTX.Dispose();
                if (objConn.State == ConnectionState.Open) objConn.Close();
                objConn.Dispose();
                OracleConnection.ClearAllPools();
            }
        }

        /// <summary>
        /// 新增PREPAY_PAID_CACHE
        /// </summary>
        /// <param name="PREPAY_PAID_CACHE"></param>
        public void Insert_PREPAY_PAID_CACHE(DataTable PREPAY_PAID_CACHE)
        {
            OracleConnection objConn = null;
            OracleTransaction objTX = null;

            try
            {
                objConn = OracleDBUtil.GetConnection();
                objTX = objConn.BeginTransaction();
                OracleDBUtil.Insert(objTX, PREPAY_PAID_CACHE);
                objTX.Commit();
            }
            catch (Exception ex)
            {
                objTX.Rollback();
                throw ex;
            }
            finally
            {
                objTX.Dispose();

                if (objConn.State == ConnectionState.Open) objConn.Close();
                objConn.Dispose();
                OracleConnection.ClearAllPools();
            }
        }

        public void AddNew_PREPAY(DataTable PREPAY_HEAD, DataTable PREPAY_ITEM, DataTable PREPAY_PAID, DataTable INVOICE_HEAD, DataTable INVOICE_ITEM)
        {
            OracleConnection objConn = null;
            OracleTransaction objTX = null;

            try
            {
                objConn = OracleDBUtil.GetConnection();
                objTX = objConn.BeginTransaction();
                string strSql = "";
                string HEAD_INVICE_NO = "", MSG_CODE = "", MSG = "";
                PK_INVOICE_SP_INVOICE_NO(StringUtil.CStr(PREPAY_HEAD.Rows[0]["STORE_NO"]),
                                                 StringUtil.CStr(PREPAY_HEAD.Rows[0]["ID"]), StringUtil .CStr (PREPAY_HEAD.Rows[0]["MODI_USER"]),
                                                 StringUtil .CStr (PREPAY_HEAD.Rows[0]["MACHINE_ID"]), ref HEAD_INVICE_NO, ref MSG_CODE, ref MSG);
                PREPAY_HEAD.Rows[0]["INVOICE_NO"] = HEAD_INVICE_NO;
                INVOICE_HEAD.Rows[0]["INVOICE_NO"] = HEAD_INVICE_NO;
                OracleDBUtil.Insert(objTX, PREPAY_HEAD);
                OracleDBUtil.Insert(objTX, PREPAY_ITEM);
                OracleDBUtil.Insert(objTX, PREPAY_PAID);
                OracleDBUtil.Insert(objTX, INVOICE_HEAD);
                OracleDBUtil.Insert(objTX, INVOICE_ITEM);
                foreach (DataRow dr in PREPAY_PAID.Rows)
                {
                    strSql = "  DELETE FROM PREPAY_PAID_CACHE";
                    strSql += " WHERE PREPAY_PAID_ID = " + OracleDBUtil.SqlStr(StringUtil.CStr(dr["ID"]));
                    OracleDBUtil.ExecuteSql(objTX, strSql);
                }
                objTX.Commit();
            }
            catch (Exception ex)
            {
                objTX.Rollback();
                throw ex;
            }
            finally
            {
                objTX.Dispose();

                if (objConn.State == ConnectionState.Open) objConn.Close();
                objConn.Dispose();
                OracleConnection.ClearAllPools();
            }
        }

        public void Update_PREPAY(string UUID)
        {
            OracleConnection objConn = null;
            OracleTransaction objTX = null;
            try
            {
                objConn = OracleDBUtil.GetConnection();
                objTX = objConn.BeginTransaction();

                string strSql = "";
                strSql = "  UPDATE PREPAY_HEAD";
                strSql += "           SET PREPAY_STATUS = '3', INVALID_DATE = sysdate, INVALID_NO =" + OracleDBUtil.SqlStr(Advtek.Utility.SerialNo.GenNo("SC"));
                strSql += "           WHERE ID = " + OracleDBUtil.SqlStr(UUID);
                OracleDBUtil.ExecuteSql(objTX, strSql);
                objTX.Commit();
            }
            catch (Exception ex)
            {
                objTX.Rollback();
                throw ex;
            }
            finally
            {
                objTX.Dispose();
                if (objConn.State == ConnectionState.Open) objConn.Close();
                objConn.Dispose();
                OracleConnection.ClearAllPools();
            }
        }

        public void Delet_PREPAY(string UUID)
        {
            OracleConnection objConn = null;
            OracleTransaction objTX = null;
            try
            {
                objConn = OracleDBUtil.GetConnection();
                objTX = objConn.BeginTransaction();


                string strSql = "";
                strSql = "  DELETE FROM PREPAY_ITEM";
                strSql += "           WHERE PREPAY_HEAD_ID = " + OracleDBUtil.SqlStr(UUID);
                OracleDBUtil.ExecuteSql(objTX, strSql);

                strSql = "  DELETE FROM PREPAY_PAID";
                strSql += "           WHERE PREPAY_HEAD_ID = " + OracleDBUtil.SqlStr(UUID);
                OracleDBUtil.ExecuteSql(objTX, strSql);

                strSql = "  DELETE FROM PREPAY_HEAD";
                strSql += "           WHERE ID = " + OracleDBUtil.SqlStr(UUID);
                OracleDBUtil.ExecuteSql(objTX, strSql);
                objTX.Commit();
            }
            catch (Exception ex)
            {
                objTX.Rollback();
                throw ex;
            }
            finally
            {
                objTX.Dispose();
                if (objConn.State == ConnectionState.Open) objConn.Close();
                objConn.Dispose();
                OracleConnection.ClearAllPools();
            }
        }

        /// <summary>
        /// 產生發票號
        /// </summary>
        /// <param name="oraConn"></param>
        /// <param name="INSTORE_NO"></param>
        /// <param name="INPOS_MASTER_UUID"></param>
        /// <param name="USER_ID"></param>
        /// <param name="HOST_ID"></param>
        /// <param name="OUTINVOICE_NO"></param>
        /// <param name="OUTMSGCODE"></param>
        /// <param name="OUTMESSAGE"></param>
        public void PK_INVOICE_SP_INVOICE_NO(string INSTORE_NO, string INPOS_MASTER_UUID, string USER_ID, string HOST_ID, ref string OUTINVOICE_NO, ref string OUTMSGCODE, ref string OUTMESSAGE)
        {
            OracleCommand oraCmd = new OracleCommand("PK_INVOICE.SP_INVOICE_NO");
            oraCmd.CommandType = System.Data.CommandType.StoredProcedure;
            oraCmd.Parameters.Add(new OracleParameter("INSTORE_NO", OracleType.VarChar, 2000)).Value = INSTORE_NO;
            oraCmd.Parameters.Add(new OracleParameter("INPOS_MASTER_UUID", OracleType.VarChar, 2000)).Value = INPOS_MASTER_UUID;
            oraCmd.Parameters.Add(new OracleParameter("USER_ID", OracleType.VarChar, 2000)).Value = USER_ID;
            oraCmd.Parameters.Add(new OracleParameter("HOST_ID", OracleType.VarChar, 2000)).Value = HOST_ID;
            oraCmd.Parameters.Add(new OracleParameter("OUTINVOICE_NO", OracleType.VarChar, 2000)).Direction = ParameterDirection.Output;
            oraCmd.Parameters.Add(new OracleParameter("OUTMSGCODE", OracleType.VarChar, 2000)).Direction = ParameterDirection.Output;
            oraCmd.Parameters.Add(new OracleParameter("OUTMESSAGE", OracleType.VarChar, 2000)).Direction = ParameterDirection.Output;
            OracleConnection oConn = OracleDBUtil.GetConnection();
            try
            {
                oraCmd.Connection = oConn;
                oraCmd.ExecuteNonQuery();
                OUTINVOICE_NO = StringUtil.CStr(oraCmd.Parameters["OUTINVOICE_NO"].Value);
                OUTMSGCODE = StringUtil .CStr (oraCmd.Parameters["OUTMSGCODE"].Value);
                OUTMESSAGE = StringUtil .CStr (oraCmd.Parameters["OUTMESSAGE"].Value);
            }
            finally
            {
                oraCmd.Dispose();
                if (oConn.State == ConnectionState.Open)
                    oConn.Close();
                oConn.Dispose();
                OracleConnection.ClearAllPools();
            }
        }

        public DataTable Query_PriPREPAY_Title(string PREPAY_NO)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(@" SELECT ID,PREPAY_NO,ID_NO
            ,STORE_NO,STORE_NAME
            ,TRADE_DATE
            ,START_TYPE
            ,decode(START_TYPE,'1','新啟用','2','續約','3','MNP','4','其他') START_TYPE_NAME
            ,CUST_NAME
            ,ID_NO
            ,MSISDN
            ,CONTACT_PHONE
            ,EMAIL
            ,TO_CHAR(MODI_DTM,'YYYY/MM/DD HH:mm:ss') MODI_DTM          
            FROM PREPAY_HEAD ");
            sb.AppendLine(" WHERE 1=1 ");
            sb.AppendLine(" AND PREPAY_NO = " + OracleDBUtil.SqlStr(PREPAY_NO));
            DataTable dt = OracleDBUtil.Query_Data(StringUtil.CStr(sb));
            return dt;

        }

        public DataTable Query_PriPREPAY_Body(string PREPAY_HEAD_ID)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(@" SELECT PREPAY_HEAD_ID
            ,PI.PRODNO
            ,P.PRODNAME
            ,PI.DESCRIPTION
            ,PI.QUANTITY
            ,PI.AMOUNT
            ,PI.REMARK          
            ,PI.CREATE_USER S_USER          
            FROM PREPAY_ITEM PI
            Left Join PRODUCT P ON PI.PRODNO = P.PRODNO        
            ");
            sb.AppendLine(" WHERE 1=1 ");
            sb.AppendLine(" AND PREPAY_HEAD_ID = " + OracleDBUtil.SqlStr(PREPAY_HEAD_ID));
            DataTable dt = OracleDBUtil.Query_Data(StringUtil.CStr(sb));
            return dt;

        }

        /// <summary>
        /// 取得發票檔案存放路徑
        /// </summary>
        /// <returns>DataTable</returns>
        public String getUploadPath(string type, string posuuid_master)
        {
            OracleConnection objConn = null;
            try
            {
                objConn = OracleDBUtil.GetConnection();
                if (type == "INV")
                {
                    string sqlStr = @"select para_value as MyValue from sys_para where para_key='INVOICE_DOWNLOAD_PATH' ";

                    DataTable dt = OracleDBUtil.GetDataSet(objConn, sqlStr).Tables[0];

                    if (dt != null && dt.Rows.Count > 0)
                    {
                        string sysPath =StringUtil .CStr ( dt.Rows[0][0]);
                        sqlStr = @"select '/' || to_char(invoice_date,'YYYYMMDD') || '/' || store_no from invoice_head where posuuid_master = "
                                    + OracleDBUtil.SqlStr(posuuid_master) + " Order by invoice_no";
                        dt = OracleDBUtil.GetDataSet(objConn, sqlStr).Tables[0];
                        if (dt != null && dt.Rows.Count > 0)
                            return sysPath + StringUtil.CStr(dt.Rows[0][0]);
                        else
                            return "";
                    }
                    else
                        return "";
                }
                else
                {
                    string sqlStr = @"select para_value as MyValue from sys_para where para_key='INVOICE_DOWNLOAD_PATH' ";

                    DataTable dt = OracleDBUtil.GetDataSet(objConn, sqlStr).Tables[0];

                    if (dt != null && dt.Rows.Count > 0)
                        return StringUtil.CStr(dt.Rows[0][0]);
                    else
                        return "";
                }
            }
            catch //(Exception ex)
            {
                return "";
            }
            finally
            {
                if (objConn.State == ConnectionState.Open)
                    objConn.Close();
                objConn.Dispose();
                OracleConnection.ClearAllPools();
            }
        }


        public DataTable getReceiptData(string POSUUID_MASTER)
        {

            string sql = "SELECT S.STORENAME AS STORENAME " + //店名
                                   ",IH.INVOICE_DATE AS INVOICE_DATE " + //發票日
                                   ",IH.INVOICE_NO AS INVOICE_NO " +  //發票號碼
                                   ",IH.BUYER AS BUYER " +  //買受人
                                   ",IH.UNI_NO AS UNI_NO " +  //統一編號
                                   ",IH.REMARK AS H_REMARK " +  //備註_HEAD 
                                   ",IH.ADDRESS AS ADDRESS " +  //地址
                                   ",IH.TAX_TYPE AS TAX_TYPE " +  //營業稅種類
                                   ",IH.TOTAL_AMOUNT AS TOTAL_AMOUNT " + //總額
                                   ",IH.TAX AS TAX " +  //營業稅
                                   ",IH.SALE_AMOUNT AS SALE_AMOUNT " + //銷售合計
                                   ",II.PROD_NAME AS PROD_NAME " + //品名
                                   ",II.QUANTITY AS QUANTITY " + //數量
                                   ",II.PRICE AS PRICE " + //單價
                                   ",II.TOTAL_AMOUNT AS AMOUNT " +  //金額
                                   ",II.REMARK I_REMARK " + //備註_ITEM
                                   ",IH.STORE_NO STORE_NO " + //店點代碼
                                   "FROM INVOICE_HEAD IH,INVOICE_ITEM  II,STORE S  " +
                                   "WHERE IH.ID =II.INVOICE_HEAD_ID " +
                                   "  AND IH.STORE_NO=S.STORE_NO " +
                                   "  AND IH.POSUUID_MASTER=" + OracleDBUtil.SqlStr(POSUUID_MASTER);
            DataTable dt = OracleDBUtil.Query_Data(StringUtil.CStr(sql));
            return dt;
        }

    }
}
