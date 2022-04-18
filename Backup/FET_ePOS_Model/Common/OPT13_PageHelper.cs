using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Data;
using System.Data.OracleClient;
using Advtek.Utility;


namespace FET.POS.Model.Common
{
    public class OPT13_PageHelper
    {
        public static DataSet GetPayOffTypeName(bool canEmpty)
        {
            OracleConnection conn = null;
            DataSet objDS = null;
            StringBuilder strSql = null;

            try
            {
                strSql = new StringBuilder();
                strSql.Append("SELECT DISTINCT ");
                strSql.Append("       PAY_OFF_TYPE,  ");
                strSql.Append("       PAY_OFF_TYPE_NAME ");
                strSql.Append("  FROM  HG_PAY_OFF_TYPE  ");
                strSql.Append(" ORDER  BY PAY_OFF_TYPE ");

                conn = OracleDBUtil.GetConnection();
                objDS = OracleDBUtil.GetDataSet(conn, strSql.ToString());

                if (canEmpty)
                {
                    DataRow dr = objDS.Tables[0].NewRow();
                    dr["PAYOFF_TYPE_NAME"] = "ALL";
                    dr["PAYOFF_TYPE"] = "";
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

            return objDS;
        }

        public static DataSet GetConvertRestTypeName(bool canEmpty)
        {
            OracleConnection conn = null;
            DataSet objDS = null;
            StringBuilder strSql = null;

            try
            {
                strSql = new StringBuilder();
                strSql.Append("SELECT DISTINCT ");
                strSql.Append("       CONVERT_REST_TYPE,  ");
                strSql.Append("       CONVERT_REST_TYPE_NAME ");
                strSql.Append("  FROM  HG_CONVERT_REST_TYPE  ");
                strSql.Append(" ORDER  BY CONVERT_REST_TYPE ");

                conn = OracleDBUtil.GetConnection();
                objDS = OracleDBUtil.GetDataSet(conn, strSql.ToString());

                if (canEmpty)
                {
                    DataRow dr = objDS.Tables[0].NewRow();
                    dr["CONVERT_REST_TYPE_NAME"] = "ALL";
                    dr["CONVERT_REST_TYPE"] = "";
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

            return objDS;
        }

        public static DataTable GetDisActByActivityId(string activityId)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT HGCRP.ACTIVITY_ID, ");
            sb.Append("       HGCRP.SID, ");
            sb.Append("       HGCRP.PRODNO, ");
            sb.Append("       HGCRP.CREATE_USER, ");
            sb.Append("       HGCRP.CREATE_DTM, ");
            sb.Append("       HGCRP.MODI_USER, ");
            sb.Append("       HGCRP.MODI_DTM ");
            sb.Append("FROM   HG_CONVERT_REST_PROD HGCRP, PRODUCT PROD ");
            sb.Append("WHERE  HGCRP.PRODNO = PROD.PRODNO ");
            sb.Append("       AND PROD.DEL_FLAG = 'N'  ");
            sb.Append("       AND PROD.COMPANYCODE = '01' ");

            if (!string.IsNullOrEmpty(activityId))
            {
                sb.Append(" AND HGCRP.ACTIVITY_ID = " + OracleDBUtil.SqlStr(activityId));
            }

            DataTable dt = OracleDBUtil.Query_Data(sb.ToString());
            return dt;
        }

        public static DataTable GetDisProdByActivityId(string activityId)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT PROD.PRODNO, ");
            sb.Append("       PROD.PRODNAME, ");
            sb.Append("       PROD.UNIT, ");
            sb.Append("       PROD.CEASEDATE, ");
            sb.Append("       PROD.ISKEY, ");
            sb.Append("       PROD.TOSO, ");
            sb.Append("       PROD.ISCONSIGNMENT, ");
            sb.Append("       PROD.PRICE, ");
            sb.Append("       PROD.PRICEDATE, ");
            sb.Append("       PROD.BARCODE1, ");
            sb.Append("       PROD.COST, ");
            sb.Append("       PROD.BARCODE2, ");
            sb.Append("       PROD.COSTDATE, ");
            sb.Append("       PROD.BARCODE3, ");
            sb.Append("       PROD.BARCODE4, ");
            sb.Append("       PROD.ULSNO, ");
            sb.Append("       PROD.STATUS, ");
            sb.Append("       PROD.ACCOUNTCODE, ");
            sb.Append("       PROD.ISSTOCK, ");
            sb.Append("       PROD.IMEI_FLAG, ");
            sb.Append("       PROD.PRODTYPENO, ");
            sb.Append("       PROD.EFFECT_DATE, ");
            sb.Append("       PROD.ATTRI_ID, ");
            sb.Append("       PROD.IS_POS_DEF_PRICE, ");
            sb.Append("       PROD.CREATE_USER, ");
            sb.Append("       PROD.MODI_USER, ");
            sb.Append("       PROD.MODI_DTM, ");
            sb.Append("       PROD.CREATE_DTM, ");
            sb.Append("       PROD.ERP_ATTRIBUTE_1, ");
            sb.Append("       PROD.ERP_ATTRIBUTE_2, ");
            sb.Append("       PROD.IS_DROPSHIPMENT, ");
            sb.Append("       PROD.IS_DISCOUNT, ");
            sb.Append("       PROD.SUPP_ID, ");
            sb.Append("       PROD.S_DATE, ");
            sb.Append("       PROD.E_DATE, ");
            sb.Append("       PROD.COMPANYCODE ");
            sb.Append("FROM   HG_CONVERT_REST_PROD HGCRP, PRODUCT PROD ");
            sb.Append("WHERE  HGCRP.PRODNO = PROD.PRODNO ");
            sb.Append("       AND PROD.DEL_FLAG = 'N'  ");
            sb.Append("       AND PROD.COMPANYCODE = '01' ");


            if (!string.IsNullOrEmpty(activityId))
            {
                sb.Append(" AND HGCRP.ACTIVITY_ID = " + OracleDBUtil.SqlStr(activityId));
            }

            DataTable dt = OracleDBUtil.Query_Data(sb.ToString());
            return dt;
        }

        public static DataTable GetHGCRByActivityId(string activityId)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT ACTIVITY_ID, ");
            sb.Append("       PAY_OFF_TYPE, ");
            sb.Append("       ACTIVITY_NAME, ");
            sb.Append("       PRODNO, ");
            sb.Append("       PROMO_ID, ");
            sb.Append("       S_DATE, ");
            sb.Append("       E_DATE, ");
            sb.Append("       MEMBER_CHECK_FLAG, ");
            sb.Append("       U_BOUND, ");
            sb.Append("       USE_COUNT, ");
            sb.Append("       CREATE_USER, ");
            sb.Append("       CREATE_DTM, ");
            sb.Append("       MODI_USER, ");
            sb.Append("       MODI_DTM, ");
            sb.Append("       CONVERT_REST_TYPE ");
            sb.Append("FROM   HG_CONVERTIBLE_RESTRICTED ");
            sb.Append("WHERE  1 = 1 ");


            if (!string.IsNullOrEmpty(activityId))
            {
                sb.Append(" AND ACTIVITY_ID = " + OracleDBUtil.SqlStr(activityId));
            }

            DataTable dt = OracleDBUtil.Query_Data(sb.ToString());
            return dt;
        }

        public static void QueryStroeByZone(string zone,
                                            string sid, string activity_id,
                                            string create_user, DateTime create_dtm,
                                            string modi_user, DateTime modi_dtm)
        {
            OracleConnection conn = null;

            StringBuilder sb = new StringBuilder();
            try
            {
                sb.Append("SELECT ");
                sb.Append(" pos_uuid() , ");
                sb.Append(OracleDBUtil.SqlStr(activity_id) + ",");
                sb.Append(OracleDBUtil.SqlStr(create_user) + ",");
                sb.Append(" Sysdate ,");
                sb.Append(OracleDBUtil.SqlStr(modi_user) + ",");
                sb.Append(" Sysdate ,");
                sb.Append("       STORE.STORE_NO ");
                sb.Append("FROM   HG_CONVERT_REST_STORE HGCRS ");
                sb.Append("       RIGHT JOIN STORE ");
                sb.Append("         ON HGCRS.STORE_NO = STORE.STORE_NO ");
                sb.Append("       INNER JOIN ZONE ");
                sb.Append("         ON STORE.ZONE = ZONE.ZONE ");
                sb.Append("WHERE  1 = 1 ");
                sb.Append("AND ZONE.ZONE = " + OracleDBUtil.SqlStr(zone));

                conn = OracleDBUtil.GetConnection();
                OracleDBUtil.ExecuteSql(conn, sb.ToString());
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
        }

        public static void InsertStroeByZone(string zone,
                                             string sid, string activity_id,
                                             string create_user, DateTime create_dtm, 
                                             string modi_user, DateTime modi_dtm) 
        {
            OracleConnection conn = null;            

            StringBuilder sb = new StringBuilder();
            try
            {
                sb.Append("INSERT INTO HG_CONVERT_REST_STORE ");
                sb.Append("SELECT pos_uuid() , a.* FROM ");
                sb.Append("( ");
                sb.Append("SELECT DISTINCT ");
                sb.Append(OracleDBUtil.SqlStr(activity_id) + " ACTIVITY_ID,");
                sb.Append(OracleDBUtil.SqlStr(create_user) + " CREATE_USER,");
                sb.Append(" Sysdate CREATE_DTM,");
                sb.Append(OracleDBUtil.SqlStr(modi_user) + " MODI_USER,");
                sb.Append(" Sysdate MODI_DTM,");
                sb.Append(" STORE.STORE_NO STORE_NO");
                sb.Append(" FROM   HG_CONVERT_REST_STORE HGCRS ");
                sb.Append("       RIGHT JOIN STORE ");
                sb.Append("         ON HGCRS.STORE_NO = STORE.STORE_NO ");
                sb.Append("       INNER JOIN ZONE ");
                sb.Append("         ON STORE.ZONE = ZONE.ZONE ");
                sb.Append("WHERE  1 = 1 ");
                sb.Append("AND (TO_CHAR(SYSDATE,'yyyyMMdd') >= NVL(STARTDATE, TO_CHAR(SYSDATE,'yyyyMMdd'))  ");
                sb.Append("AND  TO_CHAR(SYSDATE,'yyyyMMdd') <= NVL(CLOSEDATE, TO_DATE('99991231','yyyyMMdd'))) "); //也不包含暫停營業的門市
                if (!string.IsNullOrEmpty(zone))
                {
                    sb.Append("AND ZONE.ZONE = " + OracleDBUtil.SqlStr(zone));
                }
                sb.Append(" AND NOT EXISTS (SELECT * FROM HG_CONVERT_REST_STORE WHERE ACTIVITY_ID = " + OracleDBUtil.SqlStr(activity_id) + " AND STORE_NO = STORE.STORE_NO) ");
                sb.Append(" ) a ");

                conn = OracleDBUtil.GetConnection();
                OracleDBUtil.ExecuteSql(conn, sb.ToString());
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
        }

        /// <summary>
        /// 取得促銷名稱
        /// </summary>
        /// <param name="para1">促銷代號</param>
        /// <returns></returns>
        public static string GetPromoName(string para1)
        {
            string returnValue = "";

            StringBuilder sb = new StringBuilder();
            sb.Append(@"select *
                              from mm 
                             where B_date <= trunc(sysdate)
                              and E_date  >= trunc(sysdate)
                              and PROMO_NO=" + OracleDBUtil.SqlStr(para1));

            DataTable dt = OracleDBUtil.Query_Data(sb.ToString());

            if (dt.Rows.Count > 0)
            {
                returnValue = dt.Rows[0]["PROMO_NAME"].ToString();
            }

            return returnValue;
        }

        public static DataSet Get_PAY_OFF_TYPE()
        {
            OracleConnection conn = null;
            DataSet objDS = null;
            StringBuilder strSql = null;

            try
            {
                strSql = new StringBuilder();
                strSql.Append("SELECT DISTINCT             ");
                strSql.Append("       PAY_OFF_TYPE,        ");
                strSql.Append("       PAY_OFF_TYPE_NAME    ");
                strSql.Append("  FROM HG_PAY_OFF_TYPE      ");
                strSql.Append(" ORDER BY PAY_OFF_TYPE      ");

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

            return objDS;
        }
    }
}
