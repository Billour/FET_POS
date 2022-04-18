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
    public class INV05_Facade
    {
        public DataTable Query_RTNMMethodSet(string RTNNO, string StoreNo, string ProdNo, string B_DATE, string E_DATE, string Status)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(" SELECT  DISTINCT RTNM.RTNNO AS RTNNO                                    ");
            sb.Append("        ,DECODE(RTNM.STATUS,10,'已存檔',50,'已傳輸',60,'已完成','已存檔') AS STATUS,RTNM.STATUS as STATUSCODE  ");
            sb.Append("        ,TO_CHAR(RTNM.B_DATE,'YYYY/MM/DD') AS B_DATE                     ");
            sb.Append("        ,TO_CHAR(RTNM.E_DATE,'YYYY/MM/DD') AS E_DATE                     ");
            sb.Append("        ,RTRS.RETURN_DESCRIPTION AS RETURN_DESCRIPTION                   ");
            sb.Append("        ,RTNM.REMARK AS REMARK                                           ");
            sb.Append("        ,TO_CHAR(RTNM.MODI_DTM, 'yyyy/mm/dd hh24:mi:ss') MODI_DTM        ");
            sb.Append("        ,RTNM.MODI_USER AS MODI_USER , E.EMPNAME MODI_USER_NAME         ");
            sb.Append("        ,TO_CHAR(RTNM.CREATE_DTM,'YYYY/MM/DD HH:mm:ss') AS CREATE_DTM    ");
            sb.Append("        ,TO_CHAR(RTNM.CREATE_DTM,'YYYY/MM/DD') AS CREATE_DTM1    ");
            sb.Append("        ,RTNM.CREATE_USER AS CREATE_USER                                 ");
            sb.Append("        ,RTNM.RETURN_REASON_CODE AS RETURN_REASON_CODE                   ");
            sb.Append("        ,RTNM.AFTER_PROCESS_CODE AS AFTER_PROCESS_CODE                   ");
            sb.Append("   FROM RTNM, RETURN_REASON RTRS, RTND_PROD PROD, RTND_STORE STORE ,EMPLOYEE E ");
            sb.Append("  WHERE RTNM.RETURN_REASON_CODE = RTRS.RETURN_REASON_CODE                ");
            sb.Append("  AND RTNM.RTNN_ID = PROD.RTNN_ID(+) AND RTNM.RTNN_ID = STORE.RTNN_ID(+) ");
            sb.Append("  AND RTNM.MODI_USER =  E.EMPNO(+) ");

            if (!string.IsNullOrEmpty(RTNNO))
            {
                sb.Append(" AND RTNM.RTNNO LIKE " + OracleDBUtil.LikeStr(RTNNO));
            }
            if (!string.IsNullOrEmpty(StoreNo))
            {
                sb.Append(" AND STORE.STORE_NO = " + OracleDBUtil.SqlStr(StoreNo));
            }
            if (!string.IsNullOrEmpty(ProdNo))
            {
                sb.Append(" AND PROD.PRODNO = " + OracleDBUtil.SqlStr(ProdNo));
            }
            if (!string.IsNullOrEmpty(B_DATE))
            {
                sb.Append(" AND trunc(RTNM.CREATE_DTM) >= " + OracleDBUtil.DateStr(B_DATE));
            }
            if (!string.IsNullOrEmpty(E_DATE))
            {
                sb.Append(" AND trunc(RTNM.CREATE_DTM) <= " + OracleDBUtil.DateStr(E_DATE));
            }
            if (!string.IsNullOrEmpty(Status))
            {
                sb.Append(" AND RTNM.STATUS = " + OracleDBUtil.SqlStr(Status));
            }
            sb.Append(" ORDER BY RTNNO ");

            DataTable dt = OracleDBUtil.Query_Data(sb.ToString());
            return dt;
        }

        public int SaveRTNM(INV05_RTNM_DTO.RTNMDataTable dtRTNM,
                            INV05_RTNM_DTO.RTND_STOREDataTable dtRTNS,
                            INV05_RTNM_DTO.RTND_PRODDataTable dtRTNP)
        {

            int intResult = 0;
            OracleConnection objConn = null;
            OracleTransaction objTx = null;
            try
            {
                objConn = OracleDBUtil.GetConnection();
                objTx = objConn.BeginTransaction();
                 
                intResult += OracleDBUtil.Insert(objTx, dtRTNM);
                intResult += OracleDBUtil.Insert(objTx, dtRTNS);
                intResult += OracleDBUtil.Insert(objTx, dtRTNP);
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

        public void AddNewOne_RTNMMethodSet(INV05_RTNM_DTO ds)
        {
            OracleDBUtil.Insert(ds.Tables["RTNM"]);
        }

        public void AddNewOne_RTND_STOREMethodSet(INV05_RTNM_DTO ds)
        {
            OracleDBUtil.Insert(ds.Tables["RTND_STORE"]);
        }

        public void AddNewOne_RTND_PRODUCTMethodSet(INV05_RTNM_DTO ds)
        {
            OracleDBUtil.Insert(ds.Tables["RTND_PROD"]);
        }

        public void UpdateOne_UpLoadTempMethodSet(string BATCH_NO,string FINC_ID,string USER_ID)
        {
            OracleConnection objConn = null;
            OracleTransaction objTX = null;

            try
            {
                objConn = OracleDBUtil.GetConnection();
                objTX = objConn.BeginTransaction();

                OracleDBUtil.ExecuteSql(objTX,
                    @"update UPLOAD_TEMP SET STATUS = 'C'
                                         where BATCH_NO = " + OracleDBUtil.SqlStr(BATCH_NO) + 
                                        " and FINC_ID  = " + OracleDBUtil.SqlStr(FINC_ID)+
                                        " and USER_ID  = " + OracleDBUtil.SqlStr(USER_ID));

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

        public void DeleteOne_MethodSet(string _RTNN_ID)
        {
            OracleConnection objConn = null;
            OracleTransaction objTX = null;

            try
            {
                objConn = OracleDBUtil.GetConnection();
                objTX = objConn.BeginTransaction();

                //先刪除Detail
                OracleDBUtil.ExecuteSql(objTX,
                    @"delete RTND_STORE where RTND_STORE.RTNN_ID = " + OracleDBUtil.SqlStr(_RTNN_ID));

                OracleDBUtil.ExecuteSql(objTX,
                    @"delete RTND_PROD where RTND_PROD.RTNN_ID = " + OracleDBUtil.SqlStr(_RTNN_ID));

                //刪除Master
                OracleDBUtil.ExecuteSql(objTX,
                    @"delete RTNM where RTNM.RTNN_ID = " + OracleDBUtil.SqlStr(_RTNN_ID));

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

        public DataTable ExportWeightDistribute(string RTNNO, string StoreNo, string ProdNo, string B_DATE, string E_DATE, string Status)
        {
            //退倉單號   退倉狀態   退倉開始日 退倉結束日 退倉原因 備註 更新人員 更新日期 商品料號 商品名稱 門市編號 門市名稱

            string sqlStr = " SELECT  DISTINCT RTNM.RTNNO AS 退倉單號                       ";
            sqlStr += "              ,DECODE(RTNM.STATUS,10,'已存檔',50,'已傳輸',60,'已完成','已存檔') 退倉狀態  ";
            sqlStr += "              ,TO_CHAR(RTNM.B_DATE,'YYYY/MM/DD') 退倉開始日          ";
            sqlStr += "              ,TO_CHAR(RTNM.E_DATE,'YYYY/MM/DD') 退倉結束日          ";
            sqlStr += "              ,RTRS.RETURN_DESCRIPTION  退倉原因                     ";
            sqlStr += "              ,RTNM.REMARK    備註                                   ";
            sqlStr += "              ,RTNM.MODI_USER ||'-'||E.EMPNAME  更新人員                               ";
            sqlStr += "              ,TO_CHAR(RTNM.MODI_DTM,'YYYY/MM/DD HH:mM:ss') 更新日期 ";
            sqlStr += "              ,RTNP.PRODNO    商品料號                               ";
            sqlStr += "              ,PROD.PRODNAME  商品名稱                               ";
            sqlStr += "              ,RTNS.STORE_NO  門市編號                               ";
            sqlStr += "              ,STOR.STORENAME 門市名稱                               ";
            sqlStr += "         FROM  RTNM                                                  ";
            sqlStr += "   INNER JOIN RETURN_REASON RTRS                                     ";
            sqlStr += "           ON RTNM.RETURN_REASON_CODE = RTRS.RETURN_REASON_CODE      ";
            sqlStr += "    LEFT JOIN (RTND_PROD RTNP INNER JOIN PRODUCT PROD ON RTNP.PRODNO = PROD.PRODNO)    ";
            sqlStr += "           ON RTNM.RTNN_ID = RTNP.RTNN_ID                            ";
            sqlStr += "    LEFT JOIN (RTND_STORE RTNS INNER JOIN STORE STOR ON RTNS.STORE_NO = STOR.STORE_NO) ";
            sqlStr += "           ON RTNM.RTNN_ID = RTNS.RTNN_ID                      ";
            sqlStr += "    INNER JOIN EMPLOYEE E ON RTNM.MODI_USER = E.EMPNO          ";
            sqlStr += "        WHERE 1 = 1                                                  ";

            if (!string.IsNullOrEmpty(RTNNO))
            {
                sqlStr += "   AND RTNM.RTNNO LIKE " + OracleDBUtil.LikeStr(RTNNO);
            }
            if (!string.IsNullOrEmpty(StoreNo))
            {
                sqlStr += "   AND RTNS.STORE_NO = " + OracleDBUtil.SqlStr(StoreNo);

            }
            if (!string.IsNullOrEmpty(ProdNo))
            {
                sqlStr += "   AND RTNP.PRODNO = " + OracleDBUtil.SqlStr(ProdNo);
            }

            if (!string.IsNullOrEmpty(B_DATE))
            {
                sqlStr += " AND trunc(RTNM.CREATE_DTM) >= " + OracleDBUtil.DateStr(B_DATE);

            }
            if (!string.IsNullOrEmpty(E_DATE))
            {
                sqlStr += " AND trunc(RTNM.CREATE_DTM) <= " + OracleDBUtil.DateStr(E_DATE);

            }
            if (Status != "請選擇" && Status != string.Empty)
            {
                sqlStr += "   AND RTNM.STATUS = " + OracleDBUtil.SqlStr(Status);
            }
            sqlStr += " ORDER BY RTNM.RTNNO ";

            DataTable dt = OracleDBUtil.Query_Data(sqlStr);
            return dt;
        }

        public DataTable GetRTNM(string RTNNO)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(" SELECT  DISTINCT RTNM.RTNNO AS RTNNO                                        ");
            sb.Append("        ,DECODE(RTNM.STATUS,10,'已存檔',50,'已傳輸',00,'未存檔',60,'已完成') AS STATUS         ");
            sb.Append("        ,TO_CHAR(RTNM.B_DATE,'YYYY/MM/DD') AS B_DATE                         ");
            sb.Append("        ,TO_CHAR(RTNM.E_DATE,'YYYY/MM/DD') AS E_DATE                         ");
            sb.Append("        ,RTRS.RETURN_DESCRIPTION AS RETURN_DESCRIPTION                       ");
            sb.Append("        ,RTNM.REMARK AS REMARK                                               ");
            sb.Append("        ,TO_CHAR(RTNM.MODI_DTM,'YYYY/MM/DD HH24:mi:ss') AS MODI_DTM            ");
            sb.Append("         ,RTNM.MODI_DTM as MODI_DTM1 ");
            sb.Append("        ,RTNM.MODI_USER AS MODI_USER , E.EMPNAME MODI_USER_NAME              ");
            sb.Append("        ,TO_CHAR(RTNM.CREATE_DTM,'YYYY/MM/DD HH24:mi:ss') AS CREATE_DTM                 ");
            sb.Append("        ,TO_CHAR(RTNM.CREATE_DTM,'YYYY/MM/DD') AS CREATE_DTM1                 ");
            sb.Append("        ,RTNM.CREATE_USER AS CREATE_USER                                     ");
            sb.Append("        ,RTNM.RETURN_REASON_CODE AS RETURN_REASON_CODE                       ");
            sb.Append("        ,RTNM.AFTER_PROCESS_CODE AS AFTER_PROCESS_CODE         		        ");
            sb.Append("        ,RTNM.RTNN_ID AS RTNN_ID     							            ");
            sb.Append("        ,AFTS.DESCRIPTION        AS DESCRIPTION                              ");
            sb.Append("    FROM RTNM,  RETURN_REASON RTRS,  AFTER_PROCESS AFTS  , EMPLOYEE E       	");
            sb.Append("   WHERE  RTNM.RETURN_REASON_CODE = RTRS.RETURN_REASON_CODE                  ");
            sb.Append("   AND RTNM.AFTER_PROCESS_CODE = AFTS.AFTER_PROCESS_CODE                     ");
            sb.Append("   AND RTNM.MODI_USER =  E.EMPNO(+) ");

            if (RTNNO.Trim() != string.Empty)
            {
                sb.Append(" AND RTNM.RTNNO = " + OracleDBUtil.SqlStr(RTNNO.Trim()));
            }

            DataTable dt = OracleDBUtil.Query_Data(sb.ToString());
            return dt;
        }

        public DataTable GetRTNS(string RTNN_ID)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(" SELECT  RTNM.RTNNO AS RTNNO                                                         ");
            sb.Append("        ,RTNM.RTNN_ID                                                                ");
            sb.Append("        ,RTND_STORE.STORE_NO                                                         ");
            sb.Append("        ,STORE.STORENAME                                                             ");
            sb.Append("    FROM RTNM                                                            	        ");
            sb.Append("   INNER JOIN (RTND_STORE INNER JOIN STORE ON RTND_STORE.STORE_NO = STORE.STORE_NO)  ");
            sb.Append("           ON  RTNM.RTNN_ID = RTND_STORE.RTNN_ID                                     ");
            sb.Append("   WHERE 1 = 1            							                                ");

            if (RTNN_ID.Trim() != string.Empty)
            {
                sb.Append(" AND RTND_STORE.RTNN_ID = " + OracleDBUtil.SqlStr(RTNN_ID.Trim()));
            }

            sb.Append("  ORDER BY STORE_NO ");

            DataTable dt = OracleDBUtil.Query_Data(sb.ToString());
            return dt;
        }

        public DataTable GetRTNP(string RTNN_ID)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(" SELECT  RTNM.RTNNO AS RTNNO                                                         ");
            sb.Append("        ,RTNM.RTNN_ID                                                                ");
            sb.Append("        ,PROD.PRODNO                                                                 ");
            sb.Append("        ,PRODUCT.PRODNAME                                                            ");
            sb.Append("    FROM RTNM                                                            	        ");
            sb.Append("   INNER JOIN (RTND_PROD PROD INNER JOIN PRODUCT ON PROD.PRODNO = PRODUCT.PRODNO)    ");
            sb.Append("           ON  RTNM.RTNN_ID = PROD.RTNN_ID                                           ");
            sb.Append("   WHERE 1 = 1            							                                ");

            if (RTNN_ID.Trim() != string.Empty)
            {
                sb.Append(" AND PROD.RTNN_ID = " + OracleDBUtil.SqlStr(RTNN_ID.Trim()));
            }

            sb.Append("  ORDER BY PRODNO ");

            DataTable dt = OracleDBUtil.Query_Data(sb.ToString());
            return dt;
        }

        //匯入門市之TABLE---UPLOAD_TEMP
        public DataTable Get_UploadTemp_RTNS(string BATCH_NO)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(" SELECT  BATCH_NO ,FINC_ID ,USER_ID  ");
            sb.Append("        ,F2 AS STORE_NO ");
            sb.Append("        ,F3 AS STORENAME ");
            sb.Append("   FROM UPLOAD_TEMP              ");
            sb.Append("  WHERE 1 = 1                    ");
            sb.Append("    AND FINC_ID = 'INV05_IMPORT' ");
            sb.Append("    AND STATUS = 'C'             ");
            sb.Append("    AND F1 = 'STORE'             ");

            if (BATCH_NO.Trim() != string.Empty)
            {
                sb.Append(" AND BATCH_NO = " + OracleDBUtil.SqlStr(BATCH_NO.Trim()));
            }

            DataTable dt = OracleDBUtil.Query_Data(sb.ToString());
            return dt;
        }

        //匯入商品之TABLE---UPLOAD_TEMP
        public DataTable Get_UploadTemp_RTNP(string BATCH_NO)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(" SELECT  BATCH_NO ,FINC_ID ,USER_ID  ");
            sb.Append("        ,F2 AS PRODNO            ");
            sb.Append("        ,F3 AS PRODNAME          ");
            sb.Append("   FROM UPLOAD_TEMP              ");
            sb.Append("  WHERE 1 = 1                    ");
            sb.Append("    AND FINC_ID = 'INV05_IMPORT' ");
            sb.Append("    AND STATUS = 'C'             ");
            sb.Append("    AND F1 = 'PRODUCT'           ");

            if (!string.IsNullOrEmpty(BATCH_NO.Trim()))
            {
                sb.Append(" AND BATCH_NO = " + OracleDBUtil.SqlStr(BATCH_NO.Trim()));
            }

            DataTable dt = OracleDBUtil.Query_Data(sb.ToString());
            return dt;
        }

        /// <summary>
        /// 退倉設定作業資料匯入(至暫存)---商品料號---門市編號
        /// </summary>
        /// <param name="ds"></param>
        public void ImportHeadQuarter(DataTable dt, DataTable dt1)
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
                    where nvl(STATUS,'T')<>'C' and BATCH_NO = '" + dt.Rows[0]["BATCH_NO"] + "'" +
                                              "and FINC_ID  = '" + dt.Rows[0]["FINC_ID"] + "'" +
                                              "and USER_ID  = '" + dt.Rows[0]["USER_ID"] + "'");

                OracleDBUtil.Insert(objTX, dt);
                OracleDBUtil.Insert(objTX, dt1);

                //ora_com
                OracleDBUtil.ExecuteSql_SP(
                    objTX
                    , "PK_Upload_Check.INV05_CHECK"
                    , new OracleParameter("p_BATCH_NO", dt.Rows[0]["BATCH_NO"].ToString())
                    , new OracleParameter("p_USER_ID", dt.Rows[0]["USER_ID"].ToString())
                    , new OracleParameter("p_FINC_ID", dt.Rows[0]["FINC_ID"].ToString())
                    );


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

        /// <summary>
        /// 取得暫存的匯入資料---商品料號
        /// </summary>
        /// <param name="BATCH_NO"></param>
        /// <returns></returns>
        public DataTable GetImportTempData_P(string BATCH_NO, string FINC_ID, string USER_ID, string TYPE)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(" SELECT F2 AS 商品料號 ");
            sb.Append("       ,F3 AS 商品名稱 ");
            sb.Append("       ,F4 AS 異常原因");
            sb.Append("   FROM UPLOAD_TEMP ");
            sb.Append("  WHERE 1=1 ");
            sb.Append("    AND BATCH_NO = " + OracleDBUtil.SqlStr(BATCH_NO));
            sb.Append("    AND USER_ID  = " + OracleDBUtil.SqlStr(USER_ID));
            sb.Append("    AND FINC_ID  = " + OracleDBUtil.SqlStr(FINC_ID));
            sb.Append("    AND F1       = " + OracleDBUtil.SqlStr(TYPE));

            DataTable dt = OracleDBUtil.Query_Data(sb.ToString());
            return dt;
        }

        /// <summary>
        /// 取得暫存的匯入資料---門市編號
        /// </summary>
        /// <param name="BATCH_NO"></param>
        /// <returns></returns>
        public DataTable GetImportTempData_S(string BATCH_NO, string FINC_ID, string USER_ID, string TYPE)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(" SELECT F2 AS 門市編號 ");
            sb.Append("       ,F3 AS 門市名稱 ");
            sb.Append("       ,F4 AS 異常原因");
            sb.Append("   FROM UPLOAD_TEMP ");
            sb.Append("  WHERE 1=1 ");
            sb.Append("    AND BATCH_NO = " + OracleDBUtil.SqlStr(BATCH_NO));
            sb.Append("    AND USER_ID  = " + OracleDBUtil.SqlStr(USER_ID));
            sb.Append("    AND FINC_ID  = " + OracleDBUtil.SqlStr(FINC_ID));
            sb.Append("    AND F1       = " + OracleDBUtil.SqlStr(TYPE));

            DataTable dt = OracleDBUtil.Query_Data(sb.ToString());
            return dt;
        }

    }
}
