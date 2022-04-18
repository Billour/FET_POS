using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Data;
using System.Data.OracleClient;
using Advtek.Utility;
using FET.POS.Model.Facade.FacadeImpl;

namespace FET.POS.Model.Common
{
    public class OPT01_PageHelper
    {
        public static DataTable GetgvMasterSet(string AccountName, string cbPayModeValue, string cbStatusValue, string SDate_S, string SDate_E, string EDate_S, string EDate_E)
       {
           OPT01_Facade _OPT01_Facade = new OPT01_Facade();

          DataTable dt = new DataTable();
          dt = _OPT01_Facade.Query_PaymentMethodSet(AccountName, cbPayModeValue, cbStatusValue, "", "", "", "");

          return dt;
       }

       public static DataTable Query_EmptyPaymentMethodSet()
       {
           StringBuilder sb = new StringBuilder();
           sb.Append(" SELECT rownum ITEMNO, M.PAY_MODE_NAME PAY_MODE_NAME, GetPayModleStatus(S.PAYMENT_METHOD_ID) as Status, S.PAYMENT_METHOD_ID PAYMENT_METHOD_ID, S.PAY_MODE_ID PAY_MODE_ID,");
           sb.Append(" substr(S.ACCOUNT_CODE,0,2) ACC1,substr(S.ACCOUNT_CODE,3,3) ACC2, substr(S.ACCOUNT_CODE,6,4) ACC3,substr(S.ACCOUNT_CODE,10,6) ACC4,substr(S.ACCOUNT_CODE,16,4) ACC5,substr(S.ACCOUNT_CODE,20,4) ACC6,");
           sb.Append(" S.S_DATE S_DATE, S.E_DATE E_DATE, S.CREATE_USER CREATE_USER, S.CREATE_DTM CREATE_DTM,");
           sb.Append(" S.MODI_USER MODI_USER, S.MODI_DTM MODI_DTM");
           sb.Append(" FROM PAYMENT_METHOD_SET S, PAY_MODE M ");
           sb.Append(" WHERE S.PAY_MODE_ID = M.PAY_MODE_ID");
           sb.Append(" AND 1 <> 1");

           DataTable dt = OracleDBUtil.Query_Data(sb.ToString());
           return dt;
       }

       public static DataTable Query_PaymentMethodSetByKey(string eKey)
       {

           StringBuilder sb = new StringBuilder();
           sb.Append(" SELECT rownum ITEMNO, M.PAY_MODE_NAME PAY_MODE_NAME, GetPayModleStatus(S.PAYMENT_METHOD_ID) as Status, S.PAYMENT_METHOD_ID PAYMENT_METHOD_ID, S.PAY_MODE_ID PAY_MODE_ID,");
           sb.Append(" substr(S.ACCOUNT_CODE,0,2) ACC1,substr(S.ACCOUNT_CODE,3,3) ACC2, substr(S.ACCOUNT_CODE,6,4) ACC3,substr(S.ACCOUNT_CODE,10,6) ACC4,substr(S.ACCOUNT_CODE,16,4) ACC5,substr(S.ACCOUNT_CODE,20,4) ACC6,");
           sb.Append(" S.S_DATE S_DATE, S.E_DATE E_DATE, S.CREATE_USER CREATE_USER, S.CREATE_DTM CREATE_DTM,");
           sb.Append(" S.MODI_USER MODI_USER, S.MODI_DTM MODI_DTM");
           sb.Append(" FROM PAYMENT_METHOD_SET S, PAY_MODE M");
           sb.Append(" WHERE S.PAY_MODE_ID = M.PAY_MODE_ID");
           sb.Append(" AND 1 = 1");

           if (!string.IsNullOrEmpty(eKey))
           {
               sb.Append(" AND S.PAYMENT_METHOD_ID = " + OracleDBUtil.SqlStr(eKey));
           }

           DataTable dt = OracleDBUtil.Query_Data(sb.ToString());
           return dt;

       }

       public static DataSet GetPaymentModeName(bool canEmpty)
       {
           OracleConnection conn = null;
           DataSet objDS = null;
           StringBuilder strSql = null;

           try
           {
               strSql = new StringBuilder();
               strSql.Append("SELECT DISTINCT ");
               strSql.Append("       pay_mode_name as PayModeName,  ");
               strSql.Append("       pay_mode_id as PayModeId ");
               strSql.Append("  FROM  pay_mode where pay_mode_id <>'8'");
               strSql.Append(" ORDER  BY pay_mode_id ");

               conn = OracleDBUtil.GetConnection();
               objDS = OracleDBUtil.GetDataSet(conn, strSql.ToString());

               if (canEmpty)
               {
                   DataRow dr = objDS.Tables[0].NewRow();
                   dr["PayModeName"] = "ALL";
                   dr["PayModeId"] = "";
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

       public static string GetPaymentModeName(string PAY_MODE_ID)
       {
           string strName = "";

           StringBuilder strSql = new StringBuilder();
           strSql.Append("SELECT DISTINCT ");
           strSql.Append("       pay_mode_name as PayModeName,  ");
           strSql.Append("       pay_mode_id as PayModeId ");
           strSql.Append("  FROM  pay_mode  ");
           strSql.Append(" WHERE pay_mode_id = " + OracleDBUtil.SqlStr(PAY_MODE_ID));

           DataTable objDt = OracleDBUtil.Query_Data(strSql.ToString());
           if (objDt.Rows.Count > 0)
           {
               strName = objDt.Rows[0]["PayModeName"].ToString();
           }

           return strName;
       }

    }
}

