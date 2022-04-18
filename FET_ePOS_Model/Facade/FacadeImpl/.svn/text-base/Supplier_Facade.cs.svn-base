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
    public class Supplier_Facade
    {
        /// <summary>
        /// QUERY 廠商資料
        /// </summary>
        /// <param name="strSuppNo">廠商編號</param>
        /// <param name="strSuppName">廠商名稱</param>
        /// <returns></returns>
        public DataTable Query_CsmSupplier(string strSuppNo, string strSuppName)
        {
            OracleConnection objConn = null;

            try
            {
                StringBuilder sb = new StringBuilder();

                sb.Append("SELECT DISTINCT SUPP_NAME,SUPP_NO");
                sb.Append("    FROM  CSM_SUPPLIER");
                sb.Append("    WHERE TRUNC(SYSDATE) BETWEEN S_DATE AND E_DATE ");
                if (!string.IsNullOrEmpty(strSuppNo))
                {
                    sb.Append(" AND SUPP_NO LIKE " + OracleDBUtil.LikeStr(strSuppNo));
                }

                if (!string.IsNullOrEmpty(strSuppName))
                {
                    sb.Append(" AND SUPP_NAME LIKE " + OracleDBUtil.LikeStr(strSuppName));
                }

                objConn = OracleDBUtil.GetConnection();
                DataTable dt = OracleDBUtil.GetDataSet(objConn, sb.ToString()).Tables[0];

                return dt;
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
        }

        /// <summary>
        /// 寄銷廠商CSM_SUPPLIER
        /// </summary>
        /// <param name="canEmpty">是否要顯示"ALL"選項</param>
        /// <returns></returns>
        public static DataTable GetSupplierNo(bool canEmpty)
        {
            OracleConnection conn = null;
            DataSet objDS = null;
            StringBuilder strSql = null;

            try
            {
                strSql = new StringBuilder();
                strSql.Append("SELECT DISTINCT                          ");
                strSql.Append("       SUPP_NO,        ");
                strSql.Append("       SUPP_NAME     ");
                strSql.Append("  FROM  CSM_SUPPLIER                      ");
                strSql.Append("  WHERE TRUNC(SYSDATE) BETWEEN S_DATE AND E_DATE                     ");
                strSql.Append(" ORDER BY SUPP_NAME                    ");

                conn = OracleDBUtil.GetConnection();
                objDS = OracleDBUtil.GetDataSet(conn, strSql.ToString());

                if (canEmpty)
                {
                    DataRow dr = objDS.Tables[0].NewRow();
                    dr["SUPP_NAME"] = "ALL";
                    dr["SUPP_NO"] = "";
                    objDS.Tables[0].Rows.InsertAt(dr, 0);
                }
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
        /// 取得寄銷廠商
        /// </summary>
        /// <param name="SUPP_NO">廠商編號</param>
        /// <returns>DataTable</returns>
        public DataTable Query_SuppData(string SuppNo)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT SUPP_ID,                          ");
            strSql.Append("       SUPP_NO,        ");
            strSql.Append("       SUPP_NAME,     ");
            strSql.Append("       AMOUNT_MAX     ");
            strSql.Append("  FROM  CSM_SUPPLIER                      ");
            strSql.Append("  WHERE TRUNC(SYSDATE) BETWEEN S_DATE AND E_DATE                     ");
            strSql.Append(" AND SUPP_NO = " + OracleDBUtil.SqlStr(SuppNo));
            strSql.Append(" ORDER BY SUPP_NAME                    ");

            DataTable dt = OracleDBUtil.Query_Data(strSql.ToString());
            return dt;
        }
        /// <summary>
        /// 取得外部廠商Suppid
        /// </summary>
        /// <param name="strSuppNo">傳suppno</param>
        /// <returns></returns>
        public String GetSuppId(string strSuppNo)
        {
            string SuppId = string.Empty;
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT DISTINCT                          ");
            strSql.Append("       SUPP_ID        ");
            strSql.Append("  FROM  CSM_SUPPLIER                      ");
            strSql.Append("  WHERE TRUNC(SYSDATE) BETWEEN S_DATE AND E_DATE                     ");
            strSql.Append(" AND SUPP_NO = " + OracleDBUtil.SqlStr(strSuppNo));
            strSql.Append(" ORDER BY SUPP_NAME                    ");

            DataTable dt = OracleDBUtil.Query_Data(strSql.ToString());
            if (dt.Rows.Count > 0)
            {
                SuppId = dt.Rows[0]["SUPP_ID"].ToString();
            }
            return SuppId;
        }

        /// <summary>
        /// 取得外部廠商Suppid
        /// </summary>
        /// <param name="strProdNo">傳商品料號</param>
        /// <returns></returns>
        public String GetSuppId2(string strProdNo)
        {
            string SuppId = string.Empty;
            StringBuilder strSql = new StringBuilder();
            strSql.Append(@"SELECT DISTINCT P.SUPP_ID
                            FROM PRODUCT P
                            WHERE P.PRODNO= " + OracleDBUtil.SqlStr(strProdNo));
            

            DataTable dt = OracleDBUtil.Query_Data(strSql.ToString());
            if (dt.Rows.Count > 0)
            {
                SuppId = dt.Rows[0]["SUPP_ID"].ToString();
            }
            return SuppId;
        }
        /// <summary>
        /// 取得寄銷廠商
        /// </summary>
        /// <param name="SUPP_ID">廠商ID</param>
        /// <returns>DataTable</returns>
        public DataTable Query_SuppData_ID(string SuppId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT SUPP_ID,                          ");
            strSql.Append("       SUPP_NO,        ");
            strSql.Append("       SUPP_NAME,     ");
            strSql.Append("       AMOUNT_MAX     ");
            strSql.Append("  FROM  CSM_SUPPLIER                      ");
            strSql.Append("  WHERE TRUNC(SYSDATE) BETWEEN S_DATE AND E_DATE                     ");
            strSql.Append(" AND SUPP_ID = " + OracleDBUtil.SqlStr(SuppId));
            strSql.Append(" ORDER BY SUPP_NAME                    ");

            DataTable dt = OracleDBUtil.Query_Data(strSql.ToString());
            return dt;
        }

    }
}
