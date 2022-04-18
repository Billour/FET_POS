using System;
using System.Data;
using System.Data.OracleClient;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Advtek.Utility;
using FET.POS.Model.DTO;

namespace FET.POS.Model.Facade.FacadeImpl
{
    public class CON12_Facade
    {


        /// <summary>
        /// 取得目前的RTNNO
        /// </summary>
         /// <param name="strStoreNo">門市編號</param>
        /// <returns></returns>
        public DataTable get_RTNNO(string strStoreNo)
        {
            OracleConnection conn = null;
            DataSet objDS = null;
            StringBuilder strSql = null;

            try
            {
                strSql = new StringBuilder();
                strSql.Append("SELECT CSM_RTNM_UUID,RTNNO  FROM CSM_RTNM WHERE 1=1  ");
                strSql.Append("AND CSM_RTNM_UUID IN (SELECT DISTINCT CSM_RTNM_UUID FROM CSM_RTND_UP CRU WHERE 1=1 AND STORE_NO = " + OracleDBUtil.SqlStr(strStoreNo) + " AND CRU.STATUS='00' )");
                conn = OracleDBUtil.GetConnection();
                objDS = OracleDBUtil.GetDataSet(conn, strSql.ToString());

                DataRow dr = objDS.Tables[0].NewRow();
                dr["CSM_RTNM_UUID"] = "SELECT";
                dr["RTNNO"] = "-請選擇-";
                objDS.Tables[0].Rows.InsertAt(dr, 0);

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (conn.State == ConnectionState.Open) conn.Close();
                conn.Dispose();
                OracleConnection.ClearAllPools();
            }

            return objDS.Tables[0];
        }

        public string get_RTNMUUID(string strRTNNO)
        {
            string strRTNMUUID = "";

            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT CSM_RTNM_UUID ");
            sb.Append(" FROM CSM_RTNM ");
            sb.Append(" WHERE RTNNO = " + OracleDBUtil.SqlStr(strRTNNO));

            DataTable dt = OracleDBUtil.Query_Data(sb.ToString());

            if (dt.Rows.Count > 0)
                strRTNMUUID = dt.Rows[0]["CSM_RTNM_UUID"].ToString();
           

            return strRTNMUUID;
        }

        /// <summary>
        /// 取得目前Rtnm的資料
        /// </summary>
        /// <param name="strRtnmUUID">退倉單號主檔UUID</param> 
        /// <returns></returns>
        public DataTable get_RtnmData(string strRtnmUUID, string strStoreNo)
        {
            OracleConnection conn = null;
            DataSet objDS = null;
            StringBuilder strSql = null;

            try
            {
                strSql = new StringBuilder();
                //strSql.Append("SELECT CR.CSM_RTNM_UUID,CR.RTNNO,CR.B_DATE,CR.E_DATE,CR.MODI_USER,CR.MODI_DTM,CR.STATUS, ");
                //strSql.Append(" (SELECT RTNDATE FROM CSM_RTND_UP CRU WHERE  CR.CSM_RTNM_UUID = CRU.CSM_RTNM_UUID and ROWNUM =1)  RTNDATE ");
                //strSql.Append(" FROM CSM_RTNM CR WHERE 1=1 ");  
                //strSql.Append("AND CSM_RTNM_UUID  = " + OracleDBUtil.SqlStr(strRtnmUUID));
                strSql.Append("SELECT CR.CSM_RTNM_UUID,CR.RTNNO,CR.B_DATE,CR.E_DATE,CRU.MODI_USER,CRU.MODI_DTM, ");
                strSql.Append("  CRU.STATUS,CRU.RTNDATE ");
                strSql.Append(" FROM CSM_RTNM CR ");
                strSql.Append(" JOIN (select distinct CSM_RTNM_UUID,RTNDATE,STATUS,MODI_USER,MODI_DTM from CSM_RTND_UP where STORE_NO=" + OracleDBUtil.SqlStr(strStoreNo) + ") CRU on  CR.CSM_RTNM_UUID = CRU.CSM_RTNM_UUID ");
                strSql.Append(" WHERE 1=1 ");
                strSql.Append("AND CR.CSM_RTNM_UUID  = " + OracleDBUtil.SqlStr(strRtnmUUID));
                conn = OracleDBUtil.GetConnection();
                objDS = OracleDBUtil.GetDataSet(conn, strSql.ToString());


            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (conn.State == ConnectionState.Open) conn.Close();
                conn.Dispose();
                OracleConnection.ClearAllPools();
            }

            return objDS.Tables[0];
        }

        /// <summary>
        /// 取得目前明細資料
        /// </summary>
        /// <param name="strRtnmUUID">退倉單號主檔UUID</param>
        /// <param name="strStoreNo">門市編號</param>
        /// <returns></returns>
        public DataTable get_MasterData(string strRtnmUUID, string strStoreNo)
        {
            OracleConnection conn = null;
            DataSet objDS = null;

            try
            {

                string sqlstr = "select distinct CRU.CSM_RTNM_UUID,CRU.SEQNO,CS.SUPP_NO SUPPNO,CS.SUPP_NAME SUPPNAME,SE.STORE_NO STORENO,SE.STORENAME, ";
                sqlstr += "  PT.PRODNO,PT.PRODNAME,CRU.RTNQTY ";
                sqlstr += "  ,decode(CRU.STOCKQTY,'0',nvl((SELECT INORDER_QTY('" + strStoreNo + "',CRU.PRODNO) FROM dual),0)) STOCKQTY ";
                sqlstr += " from CSM_RTND_UP CRU ";
                sqlstr += " LEFT JOIN CSM_SUPPLIER CS ON CRU.SUPP_ID = CS.SUPP_ID ";
                sqlstr += " LEFT JOIN PRODUCT PT ON CRU.PRODNO = PT.PRODNO ";
                sqlstr += " LEFT JOIN STORE SE ON CRU.STORE_NO = SE.STORE_NO ";
                sqlstr += " Where CRU.CSM_RTNM_UUID = " + OracleDBUtil.SqlStr(strRtnmUUID);
                sqlstr += "   And CRU.STORE_NO = " + OracleDBUtil.SqlStr(strStoreNo);
                sqlstr += " Order by CRU.SEQNO ";

                conn = OracleDBUtil.GetConnection();
                objDS = OracleDBUtil.GetDataSet(conn, sqlstr);


            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (conn.State == ConnectionState.Open) conn.Close();
                conn.Dispose();
                OracleConnection.ClearAllPools();
            }

            return objDS.Tables[0];
        }

        public int SaveOrderData(DataSet CSM_RTN)
        {
            int intResult = 0;
            OracleConnection objConn = null;
            OracleTransaction objTx = null;
            try
            {
                objConn = OracleDBUtil.GetConnection();
                objTx = objConn.BeginTransaction();
                string strSql = "";

                foreach (DataRow dr in CSM_RTN.Tables["CSM_RTND_UP"].Rows)
                {
                    strSql = "  UPDATE CSM_RTND_UP          ";
                    strSql += "           SET STOCKQTY =" + dr["STOCKQTY"];  //庫存量
                    strSql += "             , RTNQTY =" + dr["RTNQTY"]; //退倉量
                    strSql += "             , RTNDATE =" + OracleDBUtil.SqlStr(dr["RTNDATE"].ToString()); //退倉日期
                    strSql += "             , MODI_USER =" + OracleDBUtil.SqlStr(dr["MODI_USER"].ToString()); //異動人員
                    strSql += "             , MODI_DTM =" + OracleDBUtil.DateFormate(dr["MODI_DTM"]); //異動時間
                    strSql += "             , STATUS = " + OracleDBUtil.SqlStr(dr["STATUS"].ToString());
                    strSql += "           WHERE CSM_RTNM_UUID =" + OracleDBUtil.SqlStr(dr["CSM_RTNM_UUID"].ToString());
                    strSql += "           AND PRODNO =" + OracleDBUtil.SqlStr(dr["PRODNO"].ToString());
                    strSql += "           AND STORE_NO =" + OracleDBUtil.SqlStr(dr["STORE_NO"].ToString());
                    OracleDBUtil.ExecuteSql(objTx, strSql);
                }

                //strSql = "  UPDATE CSM_RTNM          ";
                //strSql += "           SET STATUS = " + OracleDBUtil.SqlStr(CSM_RTN.Tables["CSM_RTNM"].Rows[0]["STATUS"].ToString());
                //strSql += "             , MODI_USER =" + OracleDBUtil.SqlStr(CSM_RTN.Tables["CSM_RTNM"].Rows[0]["MODI_USER"].ToString()); //異動人員
                //strSql += "             , MODI_DTM =" + OracleDBUtil.DateFormate(CSM_RTN.Tables["CSM_RTNM"].Rows[0]["MODI_DTM"]); //異動時間
                //strSql += "           WHERE CSM_RTNM_UUID = " + OracleDBUtil.SqlStr(CSM_RTN.Tables["CSM_RTNM"].Rows[0]["CSM_RTNM_UUID"].ToString());
                //OracleDBUtil.ExecuteSql(objTx, strSql);
                objTx.Commit();
                intResult = 1;
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
        /// 取得列印退倉單的資料
        /// </summary>
        /// <param name="strRtnmUUID">退倉單號主檔UUID</param>
        /// <param name="strStoreNo">門市編號</param>
        /// <returns>DataTable</returns>
        public DataTable Query_PrintRtn(string strRtnmUUID, string strStoreNo)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(@" SELECT DISTINCT T.PRODTYPENAME AS 商品類別,CRU.PRODNO AS 商品料號, P.PRODNAME AS 商品名稱,
                         P.UNIT 單位,nvl(CRU.RTNQTY,'0') AS 退倉數量,'' AS 實收數量
                         FROM CSM_RTND_UP CRU, PRODUCT P,CSM_PRODUCT_TYPE T
                         WHERE CRU.PRODNO = P.PRODNO AND P.PRODTYPENO = T.PRODTYPENO");
            sb.Append("  AND CRU.CSM_RTNM_UUID = " + OracleDBUtil.SqlStr(strRtnmUUID));
            sb.Append("  AND CRU.STORE_NO = " + OracleDBUtil.SqlStr(strStoreNo));
            sb.Append("  ORDER BY CRU.PRODNO ");

            DataTable dt = OracleDBUtil.Query_Data(sb.ToString());
            return dt;
        }

    }


}
