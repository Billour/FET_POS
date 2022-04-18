using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Advtek.Utility;
using FET.POS.Model.DTO;
using System.Data.OracleClient;
using System.Data;

namespace FET.POS.Model.Facade.FacadeImpl
{
    public class DIS08_Facade
    {
        /// <summary>
        /// 取得查詢結果組合促銷轉換值查詢
        /// </summary>
        /// <param name="SelectClassType">分類</param>
        /// <param name="Ps_Type">商品分類</param>
        /// <param name="Category">商品編號</param>
        /// <param name="RangeS_BeginDate">生效起日(起)</param>
        /// <param name="RangeS_EndDate">生效起日(訖)</param>
        /// <param name="RangeE_BeginDate">生效訖日(起)</param>
        /// <param name="RangeE_EndDate">生效訖日(訖)</param>
        /// <returns>查詢結果</returns>
        public DataTable Query_PROD_TRANS_VALUE(string SelectClassType, string Ps_Type, string Category, string RangeS_BeginDate, string RangeS_EndDate, string RangeE_BeginDate, string RangeE_EndDate)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append(" SELECT  rownum           AS ITEM_NO, tb.*						   	   ");
            sb.Append(" FROM  (                                                                ");
            sb.Append(" SELECT tab1.PS_TYPE     AS PS_TYPE                                    ");
            sb.Append("        ,tab1.CATEGORY    AS CATEGORY                                   ");
            sb.Append("        ,tab1.B_PROD_NO   AS B_PROD_NO                                  ");
            sb.Append("        ,prod1.PRODNAME   AS B_PRODNAME                                 ");
            sb.Append("        ,tab1.E_PROD_NO   AS E_PROD_NO                                  ");
            sb.Append("        ,prod2.PRODNAME   AS E_PRODNAME                                 ");
            sb.Append("        ,tab1.TRANS_VALUE AS TRANS_VALUE                                ");
            sb.Append("        ,tab1.B_DATE      AS B_DATE                                     ");
            sb.Append("        ,tab1.E_DATE      AS E_DATE                                     ");
            sb.Append("        ,tab1.CREATE_USER AS CREATE_USER                                ");
            sb.Append("        ,TO_CHAR(tab1.CREATE_DTM,'yyyy/MM/dd hh24:mi:ss')  AS CREATE_DTM ");
            sb.Append("        ,tab1.MODI_USER   AS MODI_USER                                  ");
            sb.Append("        ,emp.EMPNAME AS MODI_USER_NAME                                  ");
            sb.Append("        ,TO_CHAR(tab1.MODI_DTM,'yyyy/MM/dd hh24:mi:ss')    AS MODI_DTM  ");
            sb.Append("        ,prod1.PRODNAME   AS PRODNAME                                   ");
            sb.Append("   FROM PROD_TRANS_VALUE tab1, product prod1, product prod2, EMPLOYEE emp ");
            sb.Append("  WHERE tab1.B_PROD_NO = prod1.PRODNO(+)                                ");
            sb.Append("    AND tab1.E_PROD_NO = prod2.PRODNO(+)                                ");
            sb.Append("    AND   tab1.MODI_USER = emp.EMPNO(+)                                 ");


            if (!string.IsNullOrEmpty(SelectClassType))
            {
                if (SelectClassType == "0")
                {
                    sb.Append(" AND tab1.PS_TYPE ='0'");
                }
                else if (SelectClassType == "1")
                {
                    sb.Append(" AND tab1.PS_TYPE ='1'");
                }
            }

            if (!string.IsNullOrEmpty(RangeS_BeginDate))
            {
                sb.Append(" AND TRUNC(tab1.B_DATE) >= ");
                sb.Append(OracleDBUtil.DateStr(RangeS_BeginDate));
            }

            if (!string.IsNullOrEmpty(RangeS_EndDate))
            {
                sb.Append(" AND TRUNC(tab1.B_DATE) <= ");
                sb.Append(OracleDBUtil.DateStr(RangeS_EndDate));
            }

            if (!string.IsNullOrEmpty(RangeE_BeginDate))
            {
                sb.Append(" AND TRUNC(tab1.E_DATE) >= ");
                sb.Append(OracleDBUtil.DateStr(RangeE_BeginDate));
            }

            if (!string.IsNullOrEmpty(RangeE_EndDate))
            {
                sb.Append(" AND TRUNC(tab1.E_DATE) <= ");
                sb.Append(OracleDBUtil.DateStr(RangeE_EndDate));
            }

            if (!string.IsNullOrEmpty(Ps_Type))
                sb.Append(" AND tab1.CATEGORY = " + OracleDBUtil.SqlStr(Ps_Type));

            if (!string.IsNullOrEmpty(Category))
            {
                sb.Append(" AND tab1.B_PROD_NO <= " + OracleDBUtil.SqlStr(Category));
                sb.Append(" AND tab1.E_PROD_NO >= " + OracleDBUtil.SqlStr(Category));
            }
            sb.Append(" ORDER BY CATEGORY, B_PROD_NO, B_DATE ");
            sb.Append("  ) tb ");
            sb.Append(" ORDER BY ITEM_NO ");

            DataTable dt = OracleDBUtil.Query_Data(sb.ToString());
            return dt;

        }
    }
}
