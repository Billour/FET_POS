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
    public class CON10_Facade
    {

        public DataTable QueryCSM_RTNM(string RTNNO)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT  CSM_RTNM_UUID,RTNNO,B_DATE,E_DATE,CREATE_USER,CREATE_DTM,MODI_USER,MODI_DTM ");
            // sb.Append(" ,decode(STATUS,'10','已存檔','50','已傳輸','60','已完成') STATUS ");
            sb.Append(" ,STATUS ");
            sb.Append("FROM CSM_RTNM ");
            sb.Append("WHERE 1 =1 ");
            sb.Append(" AND RTNNO = " + OracleDBUtil.SqlStr(RTNNO));

            DataTable dt = OracleDBUtil.Query_Data(sb.ToString());
            return dt;

        }

        public DataTable QueryCSM_RTNS(string strRTNM_ID_UUID)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT  CS.STORE_NO,S.STORENAME ");
            sb.Append(" FROM CSM_RTND_STORE CS ");
            sb.Append(" LEFT JOIN STORE S ON CS.STORE_NO=S.STORE_NO ");
            sb.Append(" WHERE 1 =1 ");
            sb.Append(" AND CSM_RTNM_UUID = " + OracleDBUtil.SqlStr(strRTNM_ID_UUID));

            DataTable dt = OracleDBUtil.Query_Data(sb.ToString());
            return dt;
        }

        public DataTable QueryCSM_RTNP(string strRTNM_ID_UUID)
        {
           
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT P.PRODNO,P.PRODNAME,CS.SUPP_ID,CS.SUPP_NO,CS.SUPP_NAME  ");
            sb.Append(" FROM CSM_RTND_PROD CRP ");
            sb.Append(" LEFT JOIN CSM_SUPPLIER CS ON CRP.SUPP_ID=CS.SUPP_ID ");
            sb.Append(" LEFT JOIN PRODUCT P ON CRP.PRODNO=P.PRODNO ");
            sb.Append(" WHERE 1 =1 ");
            sb.Append(" AND CRP.CSM_RTNM_UUID = " + OracleDBUtil.SqlStr(strRTNM_ID_UUID));

            DataTable dt = OracleDBUtil.Query_Data(sb.ToString());
            return dt;
        }

        //public DataTable Query_CsmOrderD(string ORDERMID)
        //{
        //    StringBuilder sb = new StringBuilder();
        //    sb.Append("SELECT SEQNO ITEMS,");
        //    sb.Append(" CD.PRODNO,PT.PRODNAME, ");
        //    sb.Append(" CD.PRODTYPENO,CE.PRODTYPENAME, ");
        //    sb.Append(" CD.ADVISEQTY,CD.ORDQTY,PT.PRICE,(CD.ORDQTY * PT.PRICE) AMOUNT");
        //    sb.Append(" FROM CSM_ORDERD CD ");
        //    sb.Append(" LEFT JOIN PRODUCT PT ON CD.PRODNO=PT.PRODNO ");
        //    sb.Append(" LEFT JOIN CSM_PRODUCT_TYPE CE ON CD.PRODTYPENO=CE.PRODTYPENO ");
        //    sb.Append(" WHERE 1 =1 ");
        //    sb.Append(" AND CSM_ORDERM_ID = " + OracleDBUtil.SqlStr(ORDERMID));
        //    sb.Append(" ORDER BY 1 ");

        //    DataTable dt = OracleDBUtil.Query_Data(sb.ToString());
        //    return dt;

        //}

        public int SaveOrderData(DataSet CSMRTN, string RTNNO)
        {
            int intResult = 0;
            OracleConnection objConn = null;
            OracleTransaction objTx = null;
            try
            {
                objConn = OracleDBUtil.GetConnection();
                objTx = objConn.BeginTransaction();

                if (RTNNO != "")
                {
                    //先刪除Detail
                    OracleDBUtil.ExecuteSql(objTx,
                        @"delete CSM_RTND_UP where CSM_RTNM_UUID = (SELECT CSM_RTNM_UUID FROM CSM_RTNM WHERE RTNNO = " + OracleDBUtil.SqlStr(RTNNO) + ")");

                    OracleDBUtil.ExecuteSql(objTx,
                      @"delete CSM_RTND_PROD where CSM_RTNM_UUID = (SELECT CSM_RTNM_UUID FROM CSM_RTNM WHERE RTNNO = " + OracleDBUtil.SqlStr(RTNNO) + ")");

                    OracleDBUtil.ExecuteSql(objTx,
                  @"delete CSM_RTND_STORE where CSM_RTNM_UUID = (SELECT CSM_RTNM_UUID FROM CSM_RTNM WHERE RTNNO = " + OracleDBUtil.SqlStr(RTNNO) + ")");

                    OracleDBUtil.ExecuteSql(objTx,
                       @"delete CSM_RTNM where RTNNO = " + OracleDBUtil.SqlStr(RTNNO));

                }

                intResult += OracleDBUtil.Insert(objTx, CSMRTN.Tables["CSM_RTND_UP"]);
                intResult += OracleDBUtil.Insert(objTx, CSMRTN.Tables["CSM_RTND_PROD"]);
                intResult += OracleDBUtil.Insert(objTx, CSMRTN.Tables["CSM_RTND_STORE"]);
                intResult += OracleDBUtil.Insert(objTx, CSMRTN.Tables["CSM_RTNM"]);

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
            DataTable O_DATA = new DataTable();
            using (OracleConnection oConn = OracleDBUtil.GetConnection())
            {
                OracleCommand oraCmd = new OracleCommand("SP_CHECK_CSM_RTND_STORE");
                oraCmd.CommandType = System.Data.CommandType.StoredProcedure;
                oraCmd.Parameters.Add(new OracleParameter("I_BATCH_NO", OracleType.VarChar, 2000)).Value = BATCH_NO;
                oraCmd.Parameters.Add(new OracleParameter("I_USER_ID", OracleType.VarChar, 2000)).Value = USER_ID;
                oraCmd.Parameters.Add(new OracleParameter("I_FINC_ID", OracleType.VarChar, 2000)).Value = FINC_ID;
                oraCmd.Parameters.Add(new OracleParameter("I_TYPE", OracleType.VarChar, 2000)).Value = TYPE;
                oraCmd.Parameters.Add(new OracleParameter("O_DATA", OracleType.Cursor)).Direction = ParameterDirection.Output;
                oraCmd.Connection = oConn;
                oraCmd.ExecuteNonQuery();
                OracleDataAdapter da = new OracleDataAdapter(oraCmd);
                da.Fill(O_DATA);
                
            }
            DataTable O_DATA2 = new DataTable();
            O_DATA2.Columns.Add("門市編號");
            O_DATA2.Columns.Add("門市名稱");
            O_DATA2.Columns.Add("異常原因");
            foreach (DataRow dr in O_DATA.Rows)
            {
                DataRow dr2 = O_DATA2.NewRow();
                dr2["門市編號"] = dr["STORE_NO"];
                dr2["門市名稱"] = dr["STORE_NAME"];
                dr2["異常原因"] = dr["ERR_DESC"];
                O_DATA2.Rows.Add(dr2);
                O_DATA2.AcceptChanges();
            }
            //StringBuilder sb = new StringBuilder();
            //sb.Append(" SELECT F2 AS 門市編號 ");
            //sb.Append("       ,F3 AS 門市名稱 ");
            //sb.Append("       ,F4 AS 異常原因");
            //sb.Append("   FROM UPLOAD_TEMP ");
            //sb.Append("  WHERE 1=1 ");
            //sb.Append("    AND BATCH_NO = " + OracleDBUtil.SqlStr(BATCH_NO));
            //sb.Append("    AND USER_ID  = " + OracleDBUtil.SqlStr(USER_ID));
            //sb.Append("    AND FINC_ID  = " + OracleDBUtil.SqlStr(FINC_ID));
            //sb.Append("    AND F1       = " + OracleDBUtil.SqlStr(TYPE));

            //DataTable dt = OracleDBUtil.Query_Data(sb.ToString());
            //return dt;
            return O_DATA2;
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
            sb.Append("    AND FINC_ID = 'CON10_IMPORT' ");
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
            sb.Append("        ,F5 AS SUPPID            ");
            sb.Append("        ,F6 AS SUPPNO          ");
            sb.Append("        ,F7 AS SUPPNAME          ");
            sb.Append("   FROM UPLOAD_TEMP              ");
            sb.Append("  WHERE 1 = 1                    ");
            sb.Append("    AND FINC_ID = 'CON10_IMPORT' ");
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
        /// 寄銷退倉設定作業資料匯入(至暫存)---商品料號---門市編號
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
                    , "PK_Upload_Check.CON10_CHECK"
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

        public void UpdateOne_UpLoadTempMethodSet(string BATCH_NO, string FINC_ID, string USER_ID)
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
                                        " and FINC_ID  = " + OracleDBUtil.SqlStr(FINC_ID) +
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
        
    }
}
