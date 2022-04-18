using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.OracleClient;

using Advtek.Utility;

using FET.POS.Model.DTO;
using FET.POS.Model.Common;

namespace FET.POS.Model.Facade.FacadeImpl
{
    public class INV28_Facade
   {
        public DataTable Query_SEAL_OFF_PROD_M(
            string strSDATE
           , string strEDATE
           , string strPRODNO1
           , string strPRODNO2
           , string strSTORENO
           )
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(@"SELECT 
                             T2.STORE_NO                --門市編號
                            ,T1.SEAL_OFF_PROD_ID        --拆封商品設定ID
                            ,T2.SEAL_OFF_STORE_ID       --拆封商品指定明門市ID
                            ,T3.STORENAME               --門市名稱
                            ,T1.PRODNO                  --商品料號
                            ,T4.PRODNAME                --商品名稱
                            ,T1.S_DATE                  --展示起日
                            ,T1.E_DATE                  --展示訖日
                            ,T1.SEAL_OFF_QTY            --拆封數量
                            ,DECODE(T1.DISCOUNT_TYPE, '1', '金額', '百分比')  DISCOUNT_TYPE --折扣方式
                            ,T1.DISCOUNT_PRICE          --金額/佔比
                            ,(SELECT COUNT(*) FROM SEAL_OFF_IMEI WHERE SEAL_OFF_IMEI.SEAL_OFF_PROD_ID=T1.SEAL_OFF_PROD_ID AND SEAL_OFF_IMEI.SEAL_OFF_STORE_ID=T2.SEAL_OFF_STORE_ID) AS IMEI_QTY --IMEI數量
                            FROM   SEAL_OFF_PROD_M T1, SEAL_OFF_STORE T2, STORE T3, PRODUCT T4 
                            WHERE T1.SEAL_OFF_PROD_ID = T2.SEAL_OFF_PROD_ID 
                              AND T2.STORE_NO = T3.STORE_NO 
                              AND T1.PRODNO = T4.PRODNO  
                        ");

            if (!(string.IsNullOrEmpty(strSDATE)))
            {
                sb.AppendLine(" AND T1.S_DATE >= " + OracleDBUtil.TimeStr(strSDATE));
            }
            if (!(string.IsNullOrEmpty(strEDATE)))
            {
                sb.AppendLine(" AND T1.S_DATE <= " + OracleDBUtil.TimeStr(strEDATE));
            }
            if (!(string.IsNullOrEmpty(strPRODNO1)))
            {
                sb.AppendLine(" AND T1.PRODNO >= " + OracleDBUtil.SqlStr(strPRODNO1));
            }
            if (!(string.IsNullOrEmpty(strPRODNO2)))
            {
                sb.AppendLine(" AND T1.PRODNO <= " + OracleDBUtil.SqlStr(strPRODNO2));
            }
            if (!(string.IsNullOrEmpty(strSTORENO)))
            {
                sb.AppendLine(" AND  T2.STORE_NO = " + OracleDBUtil.SqlStr(strSTORENO));
            }

            DataTable dt = OracleDBUtil.Query_Data(sb.ToString());
            return dt;
        }

        public DataTable Query_SEAL_OFF_IMEI(string sSEAL_OFF_PROD_ID, string sSEAL_OFF_STORE_ID)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(@"SELECT SOI.SEAL_OFF_IMEI_ID     --IMEI編號
                                  ,SOI.SEAL_OFF_PROD_ID     --拆封商品指定ID
                                  ,SOI.SEAL_OFF_STORE_ID    --門市指定ID
                                  ,SOI.IMEI                 --IMEI
                                  ,SOI.MODI_USER            --異動人員
                                  ,EMP.EMPNAME as MODI_USER_NAME 
                                  ,to_char(SOI.MODI_DTM,'yyyy/mm/dd hh24:mi:ss') as MODI_DTM    --異動時間
                                  ,SOI.STORE_NO 
                            FROM SEAL_OFF_IMEI SOI, EMPLOYEE EMP 
                            WHERE SOI.MODI_USER = EMP.EMPNO 
                          ");
            if (!(string.IsNullOrEmpty(sSEAL_OFF_PROD_ID))) //拆封商品設定ID
            {
                sb.AppendLine(" AND SOI.SEAL_OFF_PROD_ID = " + OracleDBUtil.SqlStr(sSEAL_OFF_PROD_ID));
            }

            if (!(string.IsNullOrEmpty(sSEAL_OFF_STORE_ID))) //拆封指定門市ID
            {
                sb.AppendLine(" AND SOI.SEAL_OFF_STORE_ID = " + OracleDBUtil.SqlStr(sSEAL_OFF_STORE_ID));
            }

            DataTable dt = OracleDBUtil.Query_Data(sb.ToString());
            return dt;
        }

        public DataTable Query_SEAL_OFF_IMEI(string sSEAL_OFF_IMEI_ID)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT  ");
            sb.Append("       SOI.SEAL_OFF_IMEI_ID "); //IMEI編號
            sb.Append("      ,SOI.SEAL_OFF_PROD_ID "); //拆封商品指定ID
            sb.Append("      ,SOI.SEAL_OFF_STORE_ID "); //門市指定ID
            sb.Append("      ,SOI.IMEI "); //IMEI
            sb.Append("      ,SOI.MODI_USER "); //異動人員
            sb.Append("      ,EMP.EMPNAME as MODI_USER_NAME ");
            sb.Append("      ,to_char(SOI.MODI_DTM,'yyyy/mm/dd hh24:mi:ss') as MODI_DTM  ");//異動時間
            sb.Append(" FROM SEAL_OFF_IMEI SOI, EMPLOYEE EMP ");
            sb.Append(" WHERE SOI.MODI_USER = EMP.EMPNO ");
            if (!(string.IsNullOrEmpty(sSEAL_OFF_IMEI_ID))) //拆封商品設定ID
            {
                sb.Append(" AND SOI.SEAL_OFF_IMEI_ID = " + OracleDBUtil.SqlStr(sSEAL_OFF_IMEI_ID));
            }

            DataTable dt = OracleDBUtil.Query_Data(sb.ToString());
            return dt;
        }

        public DataTable Query_IS_SEAL_OFF_IMEI_OVER(string sSEAL_OFF_PROD_ID, string sSEAL_OFF_STORE_ID)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT  ");
            sb.Append("       SEAL_OFF_PROD_ID "); //拆封商品設定ID
            sb.Append("      ,SEAL_OFF_QTY "); //拆封數量
            sb.Append("      ,(SELECT COUNT(*) FROM SEAL_OFF_IMEI WHERE SEAL_OFF_PROD_ID=" + OracleDBUtil.SqlStr(sSEAL_OFF_PROD_ID));
            sb.Append("         AND SEAL_OFF_STORE_ID=" + OracleDBUtil.SqlStr(sSEAL_OFF_STORE_ID));
            sb.Append("       ) AS IMEI_QTY "); //IMEI數量
            sb.Append(" FROM SEAL_OFF_PROD_M ");
            sb.Append(" where SEAL_OFF_PROD_ID= " + OracleDBUtil.SqlStr(sSEAL_OFF_PROD_ID));

            DataTable dt = OracleDBUtil.Query_Data(sb.ToString());
            return dt;
        }

        public void AddNewOne_SealOffImei(INV28_SEAL_OFF_IMEI_DTO ds, string HOST_ID)
        {
            OracleConnection objConn = null;
            OracleTransaction objTX = null;
            try
            {
                objConn = OracleDBUtil.GetConnection();
                objTX = objConn.BeginTransaction();

                INV28_SEAL_OFF_IMEI_DTO.SEAL_OFF_IMEIDataTable dt = ds.SEAL_OFF_IMEI;
                string Code = "";
                string Message = "";
                string IMEILogId = "";
                string Stock = FET.POS.Model.Common.Common_PageHelper.GetGoodLOCUUID();  //銷售倉
                string Demo = FET.POS.Model.Common.Common_PageHelper.GetDemoLOCUUID();   //展示倉

                foreach (INV28_SEAL_OFF_IMEI_DTO.SEAL_OFF_IMEIRow dr in dt.Rows)
                {
                    //IMEI異動
                    SaveCHANGELOC(objTX, dr.IMEI, dr.STORE_NO, dr.PRODNO, HOST_ID, dr.MODI_USER, ref Code, ref Message,ref IMEILogId, "add", Stock, Demo);
                    if (Code == "000")
                    {
                        Code = "";
                        Message = "";
                        //庫存移倉
                        Call_SP_INVENTORY_Transfer(objTX, "1", dr.STORE_NO, dr.PRODNO, "", dr.MODI_USER, dr.SEAL_OFF_IMEI_ID, Stock, Demo, ref Code, ref Message);
                        if (Code != "000") throw new Exception(Message);
                    }
                    else
                    {
                        throw new Exception(Message);
                    }
                }

                OracleDBUtil.Insert(objTX, ds.Tables["SEAL_OFF_IMEI"]);

                objTX.Commit();
            }
            catch (Exception ex)
            {
                objTX.Rollback();
                throw ex;
            }
            finally
            {
                objTX = null;

                if (objConn.State == ConnectionState.Open) objConn.Close();
                objConn.Dispose();
                OracleConnection.ClearAllPools();
            }
        }

        public void UpdateOne_SealOffImei(INV28_SEAL_OFF_IMEI_DTO ds, string HOST_ID, bool IsDiff, string sOldIMEI)
        {
            OracleConnection objConn = null;
            OracleTransaction objTX = null;
            try
            {
                objConn = OracleDBUtil.GetConnection();
                objTX = objConn.BeginTransaction();

                INV28_SEAL_OFF_IMEI_DTO.SEAL_OFF_IMEIDataTable dt = ds.SEAL_OFF_IMEI;
                string Code = "";
                string Message = "";
                string IMEILogId = "";
                string Stock = FET.POS.Model.Common.Common_PageHelper.GetGoodLOCUUID();  //銷售倉
                string Demo = FET.POS.Model.Common.Common_PageHelper.GetDemoLOCUUID();   //展示倉

                foreach (INV28_SEAL_OFF_IMEI_DTO.SEAL_OFF_IMEIRow dr in dt.Rows)
                {
                    //IMEI異動
                    if (IsDiff)
                    {
                        SaveCHANGELOC(objTX, sOldIMEI, dr.STORE_NO, dr.PRODNO, HOST_ID, dr.MODI_USER, ref Code, ref Message, ref IMEILogId, "delete", Stock, Demo);
                        if (Code != "000") throw new Exception(Message);
                        SaveCHANGELOC(objTX, dr.IMEI, dr.STORE_NO, dr.PRODNO, HOST_ID, dr.MODI_USER, ref Code, ref Message, ref IMEILogId, "add", Stock, Demo);
                        if (Code != "000") throw new Exception(Message);
                    }

                    Code = "";
                    Message = "";
                    //庫存移倉
                    Call_SP_INVENTORY_Transfer(objTX, "2", dr.STORE_NO, dr.PRODNO, "", dr.MODI_USER, dr.SEAL_OFF_IMEI_ID, Stock, Demo, ref Code, ref Message);
                    if (Code != "000") throw new Exception(Message);
                }

                OracleDBUtil.UPDDATEByUUID(objTX, ds.Tables["SEAL_OFF_IMEI"], "SEAL_OFF_IMEI_ID");
                objTX.Commit();
            }
            catch (Exception ex)
            {
                objTX.Rollback();
                throw ex;
            }
            finally
            {
                objTX = null;

                if (objConn.State == ConnectionState.Open) objConn.Close();
                objConn.Dispose();
                OracleConnection.ClearAllPools();
            }
        }

        public void Delete_SealOffImei(INV28_SEAL_OFF_IMEI_DTO ds)
        {
            OracleDBUtil.DELETEByUUID(ds.Tables["SEAL_OFF_IMEI"], "SEAL_OFF_IMEI_ID");
        }

        public void Delete_SealOffImei(List<object> keyValues)
        {
            OracleConnection objConn = null;
            OracleTransaction objTX = null;
            try
            {
                objConn = OracleDBUtil.GetConnection();
                objTX = objConn.BeginTransaction();

                foreach (string key in keyValues)
                {
                    //if (getSEAL_OFF_PROD_M(key).Rows.Count > 0)
                    //{  //一次刪除一筆
                    INV28_SEAL_OFF_IMEI_DTO.SEAL_OFF_IMEIDataTable dtm = new INV28_SEAL_OFF_IMEI_DTO().SEAL_OFF_IMEI;
                    foreach (DataColumn dc in dtm.Columns) dc.AllowDBNull = true;
                    INV28_SEAL_OFF_IMEI_DTO.SEAL_OFF_IMEIRow drm = dtm.NewSEAL_OFF_IMEIRow();
                    drm.SEAL_OFF_IMEI_ID = key.ToString();
                    dtm.AddSEAL_OFF_IMEIRow(drm);
                    dtm.AcceptChanges();

                    OracleDBUtil.DELETEByUUID(objTX, dtm, "SEAL_OFF_IMEI_ID");
                    //}
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
                objTX = null;

                if (objConn.State == ConnectionState.Open) objConn.Close();
                objConn.Dispose();
                OracleConnection.ClearAllPools();
            }
        }

        public static int GetIMEIByProdnoIsExist(string PRODNO, string IMEI, string STORENO, string OldIMEI)
        {
            string Stock = Common_PageHelper.GetGoodLOCUUID();  //銷售倉
            string Demo = Common_PageHelper.GetDemoLOCUUID();   //展示倉

            string sqlStr = "";
            if (IMEI == OldIMEI)  //已轉入展示倉
            {
                sqlStr = @" SELECT IMEI FROM IMEI T1 INNER JOIN PRODUCT T2 ON T1.PRODNO=T2.PRODNO "
                              + " WHERE T2.DEL_FLAG='N' AND T1.PRODNO = " + OracleDBUtil.SqlStr(PRODNO) + " AND T1.IMEI= " + OracleDBUtil.SqlStr(IMEI)
                              + " AND T1.IVRCODE = " + OracleDBUtil.SqlStr(STORENO)
                              + " AND T1.CHANNEL = 'RETAIL' AND (T1.STATUS = '2' OR T1.STATUS = '11') "
                              + " AND T1.LOC = " + OracleDBUtil.SqlStr(Demo);
            }
            else  //此IMEI是否為銷售倉
            {
                sqlStr = @" SELECT IMEI FROM IMEI T1 INNER JOIN PRODUCT T2 ON T1.PRODNO=T2.PRODNO "
                              + " WHERE T2.DEL_FLAG='N' AND T1.PRODNO = " + OracleDBUtil.SqlStr(PRODNO) + " AND T1.IMEI= " + OracleDBUtil.SqlStr(IMEI)
                              + " AND T1.IVRCODE = " + OracleDBUtil.SqlStr(STORENO)
                              + " AND T1.CHANNEL = 'RETAIL' AND T1.STATUS = '2' AND T1.LOC = " + OracleDBUtil.SqlStr(Stock);
            }

            DataTable dt = OracleDBUtil.Query_Data(sqlStr);
            return dt.Rows.Count;
        }

        public static DataTable getImeiByProdno(string PRODNO, string IMEI, string SEAL_OFF_IMEI_ID)
        {
           // string Stock = Common_PageHelper.GetGoodLOCUUID();

            StringBuilder sb = new StringBuilder();
            sb.Append(" SELECT * ");
            sb.Append(" FROM SEAL_OFF_IMEI ");
            sb.Append(" WHERE IMEI= " + OracleDBUtil.SqlStr(IMEI));
            if (!string.IsNullOrEmpty(SEAL_OFF_IMEI_ID))
            {
                sb.Append(" AND SEAL_OFF_IMEI_ID <> " + OracleDBUtil.SqlStr(SEAL_OFF_IMEI_ID));
            }

            DataTable dt = OracleDBUtil.Query_Data(sb.ToString());
            return dt;
        }

        public void DeleteOne_SealOffIMEI(DataTable dt, string HOST_ID)
        {
            OracleConnection objConn = null;
            OracleTransaction objTX = null;

            try
            {
                objConn = OracleDBUtil.GetConnection();
                objTX = objConn.BeginTransaction();

                string Code = "";
                string Message = "";
                string IMEILogId = "";
                string Stock = Common_PageHelper.GetGoodLOCUUID();  //銷售倉
                string Demo = Common_PageHelper.GetDemoLOCUUID();   //展示倉

                foreach (DataRow dr in dt.Rows)
                {
                    //IMEI異動
                    SaveCHANGELOC(objTX, dr["IMEI"].ToString(), dr["STORE_NO"].ToString(), dr["PRODNO"].ToString(), HOST_ID, dr["MODI_USER"].ToString(), ref Code, ref Message, ref IMEILogId, "delete", Stock, Demo);
                    if (Code == "000")
                    {
                        Code = "";
                        Message = "";
                        //庫存移倉
                        Call_SP_INVENTORY_Transfer(objTX, "3", dr["STORE_NO"].ToString(), dr["PROD_NO"].ToString(), "", dr["MODI_USER"].ToString(), dr["SEAL_OFF_IMEI_ID"].ToString(), Stock, Demo, ref Code, ref Message);
                        if (Code != "000") throw new Exception(Message);
                    }
                    else
                    {
                        throw new Exception(Message);
                    }
                }

                DELETEByACTIVITYID(objTX, dt, "SEAL_OFF_IMEI_ID");

                objTX.Commit();
            }
            catch (Exception ex)
            {
                objTX.Rollback();
                throw ex;
            }
            finally
            {
                objTX = null;

                if (objConn.State == ConnectionState.Open) objConn.Close();
                objConn.Dispose();
                OracleConnection.ClearAllPools();
            }
        }

        public int DELETEByACTIVITYID(OracleTransaction objTX, DataTable _DT, string _strUUIDField)
        {
            int intResult = 0;
            StringBuilder _sbScript;

            try
            {
                foreach (DataRow dr in _DT.Rows)
                {
                    if (GetRowCount(_DT.TableName.ToString(), _strUUIDField, OracleDBUtil.SqlStr(StringUtil.CStr(dr[_strUUIDField]))).Rows.Count > 0)
                    {
                        _sbScript = new StringBuilder();
                        _sbScript.Append(" Delete " + _DT.TableName.ToString());
                        _sbScript.Append("  Where  " + _strUUIDField + "=" + OracleDBUtil.SqlStr(StringUtil.CStr(dr[_strUUIDField])));

                        intResult = OracleDBUtil.ExecuteSql(objTX, _sbScript.ToString());
                        if (intResult == 0) throw new Exception("DELETE SQL Execute 失敗. ");
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return intResult;
        }

        public DataTable GetRowCount(string TableName, string FieldName, string FieldValue)
        {
            string sqlStr = "SELECT * FROM  " + TableName + " WHERE " + FieldName + " = {0}";
            sqlStr = string.Format(sqlStr, FieldValue);

            return OracleDBUtil.Query_Data(sqlStr);
        }

        public void SaveCHANGELOC(OracleTransaction objTx, string IMEI, string STORENO, string PRODNO
            , string inHOST_ID, string MODI_USER, ref string outMSGCODE, ref string outMESSAGE, ref string IMEILogId
            , string status, string Stock, string Demo)
        {

                if (status == "add")
                {
                    //變更IMEI
                    Call_SP_CHANGELOC(objTx, IMEI, STORENO, "RETAIL", Stock, Demo,
                        inHOST_ID, MODI_USER, ref outMSGCODE, ref outMESSAGE, ref IMEILogId);
                }
                else
                {
                    //變更IMEI
                    Call_SP_CHANGELOC(objTx, IMEI, STORENO, "RETAIL", Demo, Stock,
                        inHOST_ID, MODI_USER, ref outMSGCODE, ref outMESSAGE, ref IMEILogId);
                }
        }

        /// <summary>
        /// 變更IMEI狀態 (庫調)
        /// </summary>
        /// <param name="objTX"></param>
        /// <param name="inIMEI">IMEI</param>
        /// <param name="inIVRCODE">門市</param>
        /// <param name="inCHANNEL_ID">CHANNEL_ID</param>
        /// <param name="inOut_LOC_ID">Out_LOC</param>
        /// <param name="inLOC_ID">LOC</param>
        /// <param name="inHOST_ID"></param>
        /// <param name="inUSER_ID">調撥人員</param>
        /// <param name="outMSGCODE"></param>
        /// <param name="outMESSAGE"></param>
        /// <param name="outIMEI_LOG_ID"></param>
        private void Call_SP_CHANGELOC(OracleTransaction objTX, string inIMEI, string inIVRCODE, string inCHANNEL_ID, string inOut_LOC_ID
            , string inLOC_ID, string inHOST_ID, string inUSER_ID, ref string outMSGCODE, ref string outMESSAGE, ref string outIMEI_LOG_ID)
        {
            OracleCommand oraCmd = null;

            try
            {
                oraCmd = new OracleCommand("PK_IMEI.SP_CHANGELOC");
                oraCmd.CommandType = System.Data.CommandType.StoredProcedure;

                oraCmd.Parameters.Add(new OracleParameter("inIMEI", OracleType.VarChar, 2000)).Value = inIMEI;
                oraCmd.Parameters.Add(new OracleParameter("inIVRCODE", OracleType.VarChar, 2000)).Value = inIVRCODE;
                oraCmd.Parameters.Add(new OracleParameter("inCHANNEL_ID", OracleType.VarChar, 2000)).Value = inCHANNEL_ID;
                oraCmd.Parameters.Add(new OracleParameter("inOut_LOC_ID", OracleType.VarChar, 2000)).Value = inOut_LOC_ID;
                oraCmd.Parameters.Add(new OracleParameter("inIn_LOC_ID", OracleType.VarChar, 2000)).Value = inLOC_ID;
                oraCmd.Parameters.Add(new OracleParameter("inHOST_ID", OracleType.VarChar, 2000)).Value = inHOST_ID;
                oraCmd.Parameters.Add(new OracleParameter("inUSER_ID", OracleType.VarChar, 2000)).Value = inUSER_ID;
                oraCmd.Parameters.Add(new OracleParameter("outMSGCODE", OracleType.VarChar, 2000)).Direction = ParameterDirection.Output;
                oraCmd.Parameters.Add(new OracleParameter("outMESSAGE", OracleType.VarChar, 2000)).Direction = ParameterDirection.Output;
                oraCmd.Parameters.Add(new OracleParameter("outIMEI_LOG_ID", OracleType.VarChar, 2000)).Direction = ParameterDirection.Output;


                oraCmd.Connection = objTX.Connection;
                oraCmd.Transaction = objTX;
                oraCmd.ExecuteNonQuery();

                outMSGCODE = oraCmd.Parameters["outMSGCODE"].Value.ToString();
                outMESSAGE = oraCmd.Parameters["outMESSAGE"].Value.ToString();
                outIMEI_LOG_ID = oraCmd.Parameters["outIMEI_LOG_ID"].Value.ToString();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                oraCmd.Dispose();
            }
        }

        /// <summary>
        /// 庫存移倉
        /// </summary>
        /// <param name="objTX"></param>
        /// <param name="ActionType">新增:1 修改:2 刪除:3</param>
        /// <param name="inSTORE_NO">門市代號</param>
        /// <param name="inPRODNO">商品料號</param>
        /// <param name="I_SHEET_NO">交易序號</param>
        /// <param name="inUSER">異動人員</param>
        /// <param name="inSOURCE_REFERENCE">呼叫來源_UUID</param>
        /// <param name="inLOC_GOOD">銷售倉UUID</param>
        /// <param name="inLOC_DEMO">展示銀UUID</param>
        /// <param name="outMSGCODE">回傳碼</param>
        /// <param name="outMESSAGE">回傳訊息</param>
        private void Call_SP_INVENTORY_Transfer(OracleTransaction objTX, string ActionType, string inSTORE_NO, string inPRODNO,string I_SHEET_NO
            , string inUSER, string inSOURCE_REFERENCE, string inLOC_GOOD, string inLOC_DEMO, ref string outMSGCODE, ref string outMESSAGE)
        {
            OracleCommand oraCmd = null;

            try
            {
                oraCmd = new OracleCommand("SP_INVENTORY_INS_UPD_DEL");
                oraCmd.CommandType = System.Data.CommandType.StoredProcedure;

                oraCmd.Parameters.Add(new OracleParameter("I_INS_1_UPD_2_DEL_3", OracleType.VarChar, 2000)).Value = ActionType;
                oraCmd.Parameters.Add(new OracleParameter("I_STORE_NO", OracleType.VarChar, 2000)).Value = inSTORE_NO;
                oraCmd.Parameters.Add(new OracleParameter("I_PRODNO", OracleType.VarChar, 2000)).Value = inPRODNO;
                oraCmd.Parameters.Add(new OracleParameter("I_SHEET_NO", OracleType.VarChar, 2000)).Value = I_SHEET_NO;
                oraCmd.Parameters.Add(new OracleParameter("I_USER", OracleType.VarChar, 2000)).Value = inUSER;
                oraCmd.Parameters.Add(new OracleParameter("I_SOURCE_REFERENCE", OracleType.VarChar, 2000)).Value = inSOURCE_REFERENCE;
                oraCmd.Parameters.Add(new OracleParameter("I_LOC_GOOD", OracleType.VarChar, 2000)).Value = inLOC_GOOD;
                oraCmd.Parameters.Add(new OracleParameter("I_LOC_DEMO", OracleType.VarChar, 2000)).Value = inLOC_DEMO;
                oraCmd.Parameters.Add(new OracleParameter("O_RT_CODE", OracleType.VarChar, 2000)).Direction = ParameterDirection.Output;
                oraCmd.Parameters.Add(new OracleParameter("O_RT_MESSAGE", OracleType.VarChar, 2000)).Direction = ParameterDirection.Output;


                oraCmd.Connection = objTX.Connection;
                oraCmd.Transaction = objTX;
                oraCmd.ExecuteNonQuery();

                outMSGCODE = oraCmd.Parameters["O_RT_CODE"].Value.ToString();
                outMESSAGE = oraCmd.Parameters["O_RT_MESSAGE"].Value.ToString();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                oraCmd.Dispose();
            }
        }


        public static string GetIMEI(string SEAL_OFF_IMEI_ID)
        {
            //string Stock = Common_PageHelper.GetGoodLOCUUID();

            StringBuilder sb = new StringBuilder();
            sb.Append(" SELECT IMEI ");
            sb.Append(" FROM SEAL_OFF_IMEI ");
            sb.Append(" WHERE SEAL_OFF_IMEI_ID = " + OracleDBUtil.SqlStr(SEAL_OFF_IMEI_ID));

            DataTable dt = OracleDBUtil.Query_Data(sb.ToString());

            string IMEI = "";
            if (dt.Rows.Count > 0)
            {
                IMEI = dt.Rows[0]["IMEI"].ToString();
            }

            return IMEI;
        }
   }
}
