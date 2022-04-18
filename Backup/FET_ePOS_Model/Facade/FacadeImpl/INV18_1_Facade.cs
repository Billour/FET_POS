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
    public class INV18_1_Facade
    {

        public DataTable Query_SADJMethodSet(string ADJNO, string STOREName, string SDATE, string EDATE, string ProdNo1, string ProdNo2, string status)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(" SELECT  DISTINCT SADJM.ADJNO    AS ADJNO                ");
            sb.Append("        ,TO_CHAR(SADJM.ADJDATE,'YYYY/MM/DD') AS ADJDATE  ");
            sb.Append("        ,SADJM.ADJUSRNO AS ADJUSRNO                      ");
            sb.Append("        ,SADJM.REMARK   AS REMARK                        ");
            sb.Append("        ,SADJM.CCOK     AS CCOK                          ");
            sb.Append("        ,SADJM.ADJLOC   AS ADJLOC                        ");
            sb.Append("        ,TO_CHAR(SADJM.MODI_DTM,'YYYY/MM/DD HH24:mi:ss') AS MODI_DTM       ");
            sb.Append("        ,SADJM.MODI_USER AS MODI_USER                    ");
            sb.Append("        ,SADJM.STORE_NO  AS STORE_NO                     ");
            sb.Append("        ,STORE.STORENAME AS STORENAME                    ");
            sb.Append("        ,DECODE(SADJM.STATUS,'00','未存檔','10','暫存','20','已存檔') AS STATUS  ");
            sb.Append("   FROM STOCKADJM SADJM, STORE, STOCKADJD SADJD          ");
            sb.Append("  WHERE SADJM.STORE_NO = STORE.STORE_NO AND SADJM.ADJNO = SADJD.ADJNO  ");

            if (!string.IsNullOrEmpty(ADJNO))
            {
                sb.Append(" AND SADJM.ADJNO LIKE " + OracleDBUtil.LikeStr(ADJNO.Trim()));
            }
            if (!string.IsNullOrEmpty(STOREName))
            {
                sb.Append(" AND STORE.STORENAME  LIKE " + OracleDBUtil.LikeStr(STOREName.Trim()));
            }
            if (!string.IsNullOrEmpty(ProdNo1))
            {
                sb.Append(" AND SADJD.PRODNO >=  " + OracleDBUtil.SqlStr(ProdNo1));
            }
            if (!string.IsNullOrEmpty(ProdNo2))
            {
                sb.Append(" AND SADJD.PRODNO <=  " + OracleDBUtil.SqlStr(ProdNo2));
            }

            if (!string.IsNullOrEmpty(SDATE))
            {
                sb.Append(" AND SADJM.ADJDATE >= " + OracleDBUtil.DateStr(SDATE));

            }
            if (!string.IsNullOrEmpty(EDATE))
            {
                sb.Append(" AND SADJM.ADJDATE <= " + OracleDBUtil.DateStr(EDATE));

            }
            if (!string.IsNullOrEmpty(status))
            {
                sb.Append(" AND SADJM.STATUS =  " + OracleDBUtil.SqlStr(status));
            }

            sb.Append("     ORDER BY ADJNO,ADJDATE ");

            DataTable dt = OracleDBUtil.Query_Data(sb.ToString());
            return dt;
        }

        public DataTable ExPort_SADJMethodSet(string ADJNO, string STOREName, string SDATE, string EDATE, string ProdNo1, string ProdNo2, string status)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(" SELECT  SADJM.ADJNO  AS  調整單號");
            sb.Append("        ,SADJM.STORE_NO  AS 門市編號                     ");
            sb.Append("        ,STORE.STORENAME AS 門市名稱  ");
            sb.Append("        ,TO_CHAR(SADJM.ADJDATE,'YYYY/MM/DD') AS 調整日期  ");
            sb.Append("        ,DECODE(SADJM.STATUS,'00','未存檔','10','暫存','20','已存檔') AS 狀態  ");
            sb.Append("        ,SADJM.MODI_USER AS 更新人員                    ");
            sb.Append("        ,TO_CHAR(SADJM.MODI_DTM,'YYYY/MM/DD HH24:mi:ss') AS 更新日期       ");
            sb.Append("        ,SADJD.PRODNO AS 商品料號                    ");
            sb.Append("        ,PROD.PRODNAME AS 商品名稱                    ");
            sb.Append("        ,SADJD.ADJQTY AS 調整量                    ");
            sb.Append("        ,COALESCE((SELECT  queryimei(STOCKADJD_ID) FROM  DUAL),'') AS IMEI ");
            sb.Append("        ,SADJD.ADJREASON AS 調整原因                    ");
            sb.Append("   FROM STOCKADJM SADJM, STORE, STOCKADJD SADJD, PRODUCT PROD  ");
            sb.Append("  WHERE SADJM.STORE_NO = STORE.STORE_NO AND SADJM.ADJNO = SADJD.ADJNO AND  SADJD.PRODNO = PROD.PRODNO ");

            if (!string.IsNullOrEmpty(ADJNO))
            {
                sb.Append(" AND SADJM.ADJNO LIKE " + OracleDBUtil.LikeStr(ADJNO.Trim()));
            }
            if (!string.IsNullOrEmpty(STOREName))
            {
                sb.Append(" AND STORE.STORENAME  LIKE " + OracleDBUtil.LikeStr(STOREName.Trim()));
            }
            if (!string.IsNullOrEmpty(ProdNo1))
            {
                sb.Append(" AND SADJD.PRODNO >=  " + OracleDBUtil.SqlStr(ProdNo1));
            }
            if (!string.IsNullOrEmpty(ProdNo2))
            {
                sb.Append(" AND SADJD.PRODNO <=  " + OracleDBUtil.SqlStr(ProdNo2));
            }

            if (!string.IsNullOrEmpty(SDATE))
            {
                sb.Append(" AND SADJM.ADJDATE >= " + OracleDBUtil.DateStr(SDATE));

            }
            if (!string.IsNullOrEmpty(EDATE))
            {
                sb.Append(" AND SADJM.ADJDATE <= " + OracleDBUtil.DateStr(EDATE));

            }
            if (!string.IsNullOrEmpty(status))
            {
                sb.Append(" AND SADJM.STATUS =  " + OracleDBUtil.SqlStr(status) + "  ");
            }

            sb.Append("     ORDER BY SADJM.ADJNO,SADJM.ADJDATE ");

            DataTable dt = OracleDBUtil.Query_Data(sb.ToString());
            return dt;
        }

        public static DataTable getAdjNAME(string where)
        {
            string StrSql = " SELECT STOCKADJ_REASON_CODE ";
            StrSql += "         FROM STOCKADJ_REASON WHERE 1=1 and ROWNUM=1 ";
            if (!string.IsNullOrEmpty(where))
            {
                StrSql += "      AND STOCKADJ_DESCRIPTION = '" + where + "'";
            }

            return OracleDBUtil.Query_Data(StrSql);
        }

        public static DataTable getAdjImei(string STOCKADJD_ID)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(" SELECT * FROM STOCKADJ_D_IMEI WHERE 1 = 1  ");
            if (!string.IsNullOrEmpty(STOCKADJD_ID))
            {
                sb.Append(" AND STOCKADJD_ID = " + OracleDBUtil.SqlStr(STOCKADJD_ID.Trim()));
            }
            sb.Append(" ORDER BY IMEI ");

            DataTable dt = OracleDBUtil.Query_Data(sb.ToString());
            return dt;
        }

        public string SaveADJ(INV18_StockADJ_DTO.STOCKADJMDataTable dtADJM,
                           INV18_StockADJ_DTO.STOCKADJDDataTable dtADJD,
                           bool DO_UP_TABLE, string inHOST_ID)
        {

            int intResult = 0;
            string strResult = "";
            OracleConnection objConn = null;
            OracleTransaction objTx = null;
            try
            {
                INVENTORY_Facade Inventory = new INVENTORY_Facade();
                objConn = OracleDBUtil.GetConnection();
                objTx = objConn.BeginTransaction();
                if (DO_UP_TABLE)  //【未存檔】進行【存檔】動作，直接新增資料到Table中
                {
                    intResult += OracleDBUtil.Insert(objTx, dtADJM);
                    intResult += OracleDBUtil.Insert(objTx, dtADJD);
                }
                else //【暫存】進行【存檔】動作，需先刪除Table舊資料，再新增資料到Table中
                {
                    //先刪除舊Detail、IMEI
                    OracleDBUtil.ExecuteSql(objTx, "delete STOCKADJ_D_IMEI where STOCKADJD_ID not in (select STOCKADJD_ID from STOCKADJD)");
                    OracleDBUtil.ExecuteSql(objTx, "delete STOCKADJD where ADJNO = " + OracleDBUtil.SqlStr(dtADJM.Rows[0]["ADJNO"].ToString()));

                    //再新增新Detail
                    intResult += OracleDBUtil.Insert(objTx, dtADJD);

                    //變更Master的狀態【暫存】 => 【已存檔】
                    foreach (INV18_StockADJ_DTO.STOCKADJMRow dr in dtADJM.Rows)
                    {
                        OracleDBUtil.ExecuteSql(objTx, "update STOCKADJM set STATUS='20' where ADJNO = " + OracleDBUtil.SqlStr(dr["ADJNO"].ToString()));
                    }
                }
                string Code = "";
                //異動庫存 UpdateADJUST(); 
                foreach (INV18_StockADJ_DTO.STOCKADJDRow dr in dtADJD.Rows)
                {
                    string Message = "";
                    string Stock = Common_PageHelper.GetGoodLOCUUID();

                    if (int.Parse(dr.ADJQTY.ToString()) > 0)
                    {
                        Inventory.PK_INVENTORY_ADJUST(objTx, "1", "AI", dr.PRODNO.ToString(),
                           dtADJM.Rows[0]["STORE_NO"].ToString(),
                           Stock, dtADJM.Rows[0]["ADJNO"].ToString(), Convert.ToInt32(dr.ADJQTY.ToString()),
                           dr.MODI_USER, dr.STOCKADJD_ID, ref Code, ref Message);
                    }
                    else
                    {
                        Inventory.PK_INVENTORY_ADJUST(objTx, "1", "AO", dr.PRODNO.ToString(),
                           dtADJM.Rows[0]["STORE_NO"].ToString(),
                           Stock, dtADJM.Rows[0]["ADJNO"].ToString(), Convert.ToInt32(dr.ADJQTY.ToString()),
                           dr.MODI_USER, dr.STOCKADJD_ID, ref Code, ref Message);
                    }
                    DataTable dt = getAdjImei(dr.STOCKADJD_ID.ToString());
                    foreach (DataRow dr1 in dt.Rows)
                    {
                        Call_SP_IMEI_HQ_ADJUST(objTx, dr1["IMEI"].ToString(), dtADJM.Rows[0]["STORE_NO"].ToString(), "RETAIL",
                            Stock, dr.PRODNO.ToString(),
                            inHOST_ID, dr.MODI_USER, (dr.ADJQTY.ToString().Substring(0, 1) == "-" ? "-1" : "1"), ref Code, ref Message);
                        if (Code == "999")
                        {
                            strResult = "999-" + Message;
                            throw new Exception(strResult);
                        }
                    }
                }
                objTx.Commit();
            }

            catch (Exception ex)
            {
                objTx.Rollback();
                throw ex;
            }
            finally
            {
                objTx.Dispose();

                if (objConn.State == ConnectionState.Open) objConn.Close();
                objConn.Dispose();
                OracleConnection.ClearAllPools();
            }
            return strResult;
        }

        /// <summary>
        /// 變更IMEI狀態 (庫調)
        /// </summary>
        /// <param name="objTX"></param>
        /// <param name="inIMEI">IMEI</param>
        /// <param name="inIVRCODE">門市</param>
        /// <param name="inCHANNEL_ID">CHANNEL_ID</param>
        /// <param name="inLOC_ID">LOC</param>
        /// <param name="inPRODNO">商品料號</param>
        /// <param name="inHOST_ID"></param>
        /// <param name="inUSER_ID">調撥人員</param>
        /// <param name="inADJ_QTY">調撥數量</param>
        /// <param name="outMSGCODE"></param>
        /// <param name="outMESSAGE"></param>
        private void Call_SP_IMEI_HQ_ADJUST(OracleTransaction objTX, string inIMEI, string inIVRCODE, string inCHANNEL_ID
            , string inLOC_ID, string inPRODNO, string inHOST_ID, string inUSER_ID, string inADJQTY, ref string outMSGCODE, ref string outMESSAGE)
        {
            OracleCommand oraCmd = null;

            try
            {
                oraCmd = new OracleCommand("PK_IMEI.SP_IMEI_HQ_ADJUST");
                oraCmd.CommandType = System.Data.CommandType.StoredProcedure;

                oraCmd.Parameters.Add(new OracleParameter("inIMEI", OracleType.VarChar, 2000)).Value = inIMEI;
                oraCmd.Parameters.Add(new OracleParameter("inIVRCODE", OracleType.VarChar, 2000)).Value = inIVRCODE;
                oraCmd.Parameters.Add(new OracleParameter("inCHANNEL_ID", OracleType.VarChar, 2000)).Value = inCHANNEL_ID;
                oraCmd.Parameters.Add(new OracleParameter("inLOC_ID", OracleType.VarChar, 2000)).Value = inLOC_ID;
                oraCmd.Parameters.Add(new OracleParameter("inPRODNO", OracleType.VarChar, 2000)).Value = inPRODNO;
                oraCmd.Parameters.Add(new OracleParameter("inHOST_ID", OracleType.VarChar, 2000)).Value = inHOST_ID;
                oraCmd.Parameters.Add(new OracleParameter("inUSER_ID", OracleType.VarChar, 2000)).Value = inUSER_ID;
                oraCmd.Parameters.Add(new OracleParameter("inADJ_QTY", OracleType.Int32)).Value = int.Parse(inADJQTY);
                oraCmd.Parameters.Add(new OracleParameter("outMSGCODE", OracleType.VarChar, 2000)).Direction = ParameterDirection.Output;
                oraCmd.Parameters.Add(new OracleParameter("outMESSAGE", OracleType.VarChar, 2000)).Direction = ParameterDirection.Output;

                oraCmd.Connection = objTX.Connection;
                oraCmd.Transaction = objTX;
                oraCmd.ExecuteNonQuery();

                outMSGCODE = oraCmd.Parameters["outMSGCODE"].Value.ToString();
                outMESSAGE = oraCmd.Parameters["outMESSAGE"].Value.ToString();
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

        public int SaveIMPORTADJ(INV18_StockADJ_DTO.STOCKADJMDataTable dtADJM,
                          INV18_StockADJ_DTO.STOCKADJDDataTable dtADJD,
                          INV18_StockADJ_DTO.STOCKADJ_D_IMEIDataTable dt_STOCKADJ)
        {

            int intResult = 0;
            OracleConnection objConn = null;
            OracleTransaction objTx = null;
            try
            {
                objConn = OracleDBUtil.GetConnection();
                objTx = objConn.BeginTransaction();

                intResult += OracleDBUtil.Insert(objTx, dtADJM);
                intResult += OracleDBUtil.Insert(objTx, dtADJD);
                intResult += OracleDBUtil.Insert(objTx, dt_STOCKADJ);

                objTx.Commit();
            }

            catch (Exception ex)
            {
                objTx.Rollback();
                throw ex;
            }
            finally
            {
                objTx.Dispose();

                if (objConn.State == ConnectionState.Open) objConn.Close();
                objConn.Dispose();
                OracleConnection.ClearAllPools();
            }
            return intResult;
        }

        public void DeleteOne_MethodSet(INV18_StockADJ_DTO.STOCKADJMDataTable dtADJM,
                                        INV18_StockADJ_DTO.STOCKADJDDataTable dtADJD)
        {
            OracleConnection objConn = null;
            OracleTransaction objTX = null;

            try
            {
                objConn = OracleDBUtil.GetConnection();
                objTX = objConn.BeginTransaction();
                foreach (INV18_StockADJ_DTO.STOCKADJDRow dr in dtADJD.Rows)
                {
                    OracleDBUtil.ExecuteSql(objTX, @"delete STOCKADJ_D_IMEI where STOCKADJD_ID = " + OracleDBUtil.SqlStr(dr["STOCKADJD_ID"].ToString()));
                    OracleDBUtil.ExecuteSql(objTX, @"delete STOCKADJD where STOCKADJD_ID = " + OracleDBUtil.SqlStr(dr["STOCKADJD_ID"].ToString()));
                }

                foreach (INV18_StockADJ_DTO.STOCKADJMRow dr in dtADJM.Rows)
                {
                    OracleDBUtil.ExecuteSql(objTX, @"delete STOCKADJM where ADJNO = " + OracleDBUtil.SqlStr(dr["ADJNO"].ToString()));
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

        public DataTable Query_SADJ_REASONMethodSet()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(" SELECT  STOCKADJ_DESCRIPTION    ");
            sb.Append("   FROM  STOCKADJ_REASON         ");
            sb.Append("  WHERE 1 = 1                    ");
            sb.Append("  ORDER BY STOCKADJ_DESCRIPTION  ");

            DataTable dt = OracleDBUtil.Query_Data(sb.ToString());
            return dt;
        }

        public DataTable GetADJM(string ADJNO)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(" SELECT  DISTINCT SADJM.ADJNO    AS ADJNO                ");
            sb.Append("        ,TO_CHAR(SADJM.ADJDATE,'YYYY/MM/DD') AS ADJDATE  ");
            sb.Append("        ,SADJM.ADJUSRNO AS ADJUSRNO                      ");
            sb.Append("        ,SADJM.REMARK   AS REMARK                        ");
            sb.Append("        ,SADJM.CCOK     AS CCOK                          ");
            sb.Append("        ,SADJM.ADJLOC   AS ADJLOC                        ");
            sb.Append("        ,TO_CHAR(SADJM.MODI_DTM,'YYYY/MM/DD hh:mi:ss') AS MODI_DTM       ");
            sb.Append("        ,SADJM.MODI_USER AS MODI_USER                    ");
            sb.Append("        ,SADJM.STORE_NO  AS STORE_NO                     ");
            sb.Append("        ,STORE.STORENAME AS STORENAME                    ");
            sb.Append("        ,DECODE(SADJM.STATUS,'00','00 未存檔','10','10 暫存','20','20 已存檔') AS STATUS  ");
            sb.Append("  FROM STOCKADJM SADJM, STORE           ");
            sb.Append("  WHERE SADJM.STORE_NO = STORE.STORE_NO ");

            if (ADJNO.Trim() != string.Empty)
            {
                sb.Append(" AND SADJM.ADJNO =  " + OracleDBUtil.SqlStr(ADJNO.Trim()));
            }

            DataTable dt = OracleDBUtil.Query_Data(sb.ToString());
            return dt;
        }

        public DataTable GetADJD(string ADJNO)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(" SELECT ROWNUM      AS 項次                                                          ");
            sb.Append("       ,ADJD.PRODNO AS PRODNO                                                        ");
            sb.Append("       ,PRODUCT.PRODNAME AS PRODNAME                                                 ");
            sb.Append("       ,NVL(INV_OnHandQty(ADJD.PRODNO,ADJM.STORE_NO),0) AS INV_OnHandQty             ");
            sb.Append("       ,ADJD.ADJQTY AS ADJQTY                                                        ");
            sb.Append("       ,ADJD.STOCKADJ_REASON_CODE AS STOCKADJ_REASON_CODE                            ");
            sb.Append("       ,ADJD.ADJREASON AS ADJREASON                                                  ");
            sb.Append("       ,ADJD.ADJNO                                                                   ");
            sb.Append("       ,'' AS imgIMEI                                                                ");
            sb.Append("       ,'' AS IMEI                                                                   ");
            sb.Append("       ,IMEI_FLAG AS IMEI_FLAG                                                       ");
            sb.Append("       ,ADJD.STOCKADJD_ID AS STOCKADJD_ID                                            ");
            sb.Append("       ,NVL((SELECT COUNT(*) FROM STOCKADJ_D_IMEI WHERE STOCKADJ_D_IMEI.STOCKADJD_ID=ADJD.STOCKADJD_ID),0 ) AS IMEI_QTY ");
            sb.Append("   FROM STOCKADJD ADJD, STOCKADJM ADJM, PRODUCT  ");
            sb.Append("   WHERE ADJM.ADJNO = ADJD.ADJNO AND ADJD.PRODNO = PRODUCT.PRODNO ");

            if (!string.IsNullOrEmpty(ADJNO.Trim()))
            {
                sb.Append(" AND ADJD.ADJNO =  " + OracleDBUtil.SqlStr(ADJNO.Trim()));
            }

            sb.Append("  ORDER BY ROWNUM ");

            DataTable dt = OracleDBUtil.Query_Data(sb.ToString());
            return dt;
        }

        public void ImportTemp(DataTable dt)
        {
            OracleConnection objConn = null;
            OracleTransaction objTX = null;

            try
            {
                objConn = OracleDBUtil.GetConnection();
                objTX = objConn.BeginTransaction();

                //處理程序
                //OPT05_HqInvoiceAssign.HQ_INVOICE_ASSIGN_TEMPDataTable dt = ds.HQ_INVOICE_ASSIGN_TEMP;

                OracleDBUtil.ExecuteSql(objTX,
                    @"delete UPLOAD_TEMP
                    where nvl(STATUS,'T')<>'C' " +
                                              "and FINC_ID  = '" + dt.Rows[0]["FINC_ID"] + "'" +
                                              "and USER_ID  = '" + dt.Rows[0]["USER_ID"] + "'");

                OracleDBUtil.Insert(objTX, dt);

                //ora_com
                OracleDBUtil.ExecuteSql_SP(objTX, "SP_CHECK_INV18_1", new OracleParameter("v_BATCHNO", dt.Rows[0]["BATCH_NO"].ToString()));

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

        public INV18_StockADJ_DTO.UPLOAD_TEMPDataTable GetTemp(string BATCH_NO)
        {
            INV18_StockADJ_DTO.UPLOAD_TEMPDataTable dt = new INV18_StockADJ_DTO.UPLOAD_TEMPDataTable();
            OracleConnection objConn = null;

            try
            {
                StringBuilder sb = new StringBuilder();

                sb.AppendLine(@" SELECT BATCH_NO, SID, USER_ID, FINC_ID, F1, F2, F3, F4, F5, F6, F7, replace(F8, ',', ';') AS F8 , F9, F10 
                        FROM UPLOAD_TEMP WHERE BATCH_NO="
                        + OracleDBUtil.SqlStr(BATCH_NO.ToString()) + "ORDER BY F9");       //依門市編號排序

                objConn = OracleDBUtil.GetConnection();
                dt.Load(OracleDBUtil.GetDataReader(objConn, sb.ToString()));
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (objConn.State == ConnectionState.Open) objConn.Close();
                objConn.Dispose();
                OracleConnection.ClearAllPools();
            }

            return dt;
        }

        public DataTable LoadTemp(string BATCH_NO)
        {
            OracleConnection objConn = null;
            DataTable dt = new DataTable();
            try
            {
                StringBuilder sb = new StringBuilder();
                sb.Append(" SELECT ROWNUM      AS 項次 ");
                sb.Append("       ,F1 AS PRODNO         ");
                sb.Append("       ,F2 AS PRODNAME       ");
                sb.Append("       ,NVL(F3,0) AS INV_ONHANDQTY   ");
                sb.Append("       ,F4 AS ADJQTY         ");
                sb.Append("       ,F5 AS STOCKADJ_REASON_CODE       ");
                sb.Append("       ,F6 AS ADJREASON      ");
                sb.Append("       ,F7 AS IMEI_QTY       ");
                sb.Append("       ,F8 AS IMEI_NAME     ");
                sb.Append("       ,'' AS IMGIMEI     ");
                sb.Append("       ,STOCKADJD_ID AS STOCKADJD_ID     ");
                sb.Append("        FROM UPLOAD_TEMP WHERE BATCH_NO=" + OracleDBUtil.SqlStr(BATCH_NO.ToString()));

                objConn = OracleDBUtil.GetConnection();
                dt.Load(OracleDBUtil.GetDataReader(objConn, sb.ToString()));
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (objConn.State == ConnectionState.Open) objConn.Close();
                objConn.Dispose();
                OracleConnection.ClearAllPools();
            }

            return dt;
        }

        public void Update_ADJD(INV18_StockADJ_DTO.STOCKADJDDataTable dtADJD)
        {
            OracleDBUtil.UPDDATEByUUID(dtADJD, "STOCKADJD_ID");
        }

        public void Del_ADJD(string v_STOCKADJD_ID)
        {
            OracleConnection objConn = null;
            OracleTransaction objTX = null;

            try
            {
                objConn = OracleDBUtil.GetConnection();
                objTX = objConn.BeginTransaction();

                OracleDBUtil.ExecuteSql(objTX, @"delete STOCKADJ_D_IMEI where STOCKADJD_ID = " + OracleDBUtil.SqlStr(v_STOCKADJD_ID));
                OracleDBUtil.ExecuteSql(objTX, @"delete STOCKADJD where STOCKADJD_ID = " + OracleDBUtil.SqlStr(v_STOCKADJD_ID));

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

        public void Add_ADJD(INV18_StockADJ_DTO.STOCKADJDDataTable dtADJD)
        {
            OracleConnection objConn = null;
            OracleTransaction objTX = null;

            try
            {
                objConn = OracleDBUtil.GetConnection();
                objTX = objConn.BeginTransaction();

                OracleDBUtil.Insert(objTX, dtADJD);

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

        public DataTable ExportWeightDistribute(string ADJNO, string STOREName, string SDATE, string EDATE, string ProdNo1, string ProdNo2)
        {
            //調整單號	門市編號	門市名稱	調整日期	狀態	更新人員	更新日期	商品料號	商品名稱	調整量	IMEI	調整原因

            string sqlStr = "  SELECT  DISTINCT SADJM.ADJNO    AS 調整單號                                      ";
            sqlStr += "               ,SADJM.STORE_NO          AS 門市編號                                      ";
            sqlStr += "               ,STORE.STORENAME         AS 門市名稱                                      ";
            sqlStr += "               ,TO_CHAR(SADJM.ADJDATE,'YYYY/MM/DD') AS 調整日期                          ";
            sqlStr += "               ,DECODE(SADJM.STATUS,'00','未存檔','10','暫存','20','已存檔') AS 狀態     ";
            sqlStr += "               ,SADJM.MODI_USER         AS 更新人員                                      ";
            sqlStr += "               ,TO_CHAR(SADJM.MODI_DTM,'YYYY/MM/DD HH24:mi:ss') AS 更新日期                ";
            sqlStr += "               ,SADJD.PRODNO            AS 商品料號                                      ";
            sqlStr += "               ,PROD.PRODNAME           AS 商品名稱                                      ";
            sqlStr += "               ,ADJQTY                  AS 調整量                                        ";
            sqlStr += "               ,ADJREASON               AS 調整原因                                      ";
            sqlStr += "          FROM STOCKADJM SADJM, STORE, STOCKADJD SADJD , PRODUCT PROD                    ";
            sqlStr += "         WHERE SADJM.STORE_NO = STORE.STORE_NO AND SADJM.ADJNO = SADJD.ADJNO AND SADJD.PRODNO = PROD.PRODNO ";

            if (!string.IsNullOrEmpty(ADJNO))
            {
                sqlStr += "   AND SADJM.ADJNO LIKE " + OracleDBUtil.LikeStr(ADJNO.Trim());
            }
            if (!string.IsNullOrEmpty(STOREName))
            {
                sqlStr += "   AND SADJM.STORENAME  LIKE " + OracleDBUtil.LikeStr(STOREName.Trim());

            }

            if (!string.IsNullOrEmpty(SDATE))
            {
                sqlStr += " AND SADJM.ADJDATE >= " + OracleDBUtil.DateStr(SDATE.Trim());

            }
            if (!string.IsNullOrEmpty(EDATE))
            {
                sqlStr += " AND SADJM.ADJDATE <= " + OracleDBUtil.DateStr(EDATE.Trim());

            }

            if (!string.IsNullOrEmpty(ProdNo1))
            {
                sqlStr += " AND SADJD.PRODNO >=  " + OracleDBUtil.SqlStr(ProdNo1.Trim());

            }
            if (!string.IsNullOrEmpty(ProdNo2))
            {
                sqlStr += " AND SADJD.PRODNO <=  " + OracleDBUtil.SqlStr(ProdNo2.Trim());

            }

            sqlStr += "   ORDER BY SADJM.ADJNO ";

            DataTable dt = OracleDBUtil.Query_Data(sqlStr);
            return dt;
        }

        public static string GETLOC(string LOCID)
        {
            string str = "";

            StringBuilder sb = new StringBuilder();
            sb.Append("select LOC_ID from LOC where SALES_FLAG = '1' ");
            //sb.Append("FROM   SYS_PARA ");
            //sb.Append(" WHERE PARA_KEY = 'HR_ORDER_NO' ");

            DataTable dt = OracleDBUtil.Query_Data(sb.ToString());

            foreach (DataRow dr in dt.Rows)
            {
                str = dr["LOC_ID"].ToString();
                if (str == "")
                {
                    str = "0";
                }
            }

            return str;
        }

        public void DeleteIMEI(string UUID)
        {
            OracleConnection objConn = null;
            OracleTransaction objTX = null;

            try
            {
                objConn = OracleDBUtil.GetConnection();
                objTX = objConn.BeginTransaction();
         
                OracleDBUtil.ExecuteSql(objTX, @"delete STOCKADJ_D_IMEI where STOCKADJD_ID = '" + UUID + "'");
                              
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
    }


}