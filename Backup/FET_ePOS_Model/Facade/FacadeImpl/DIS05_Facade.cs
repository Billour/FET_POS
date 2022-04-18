using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Advtek.Utility;
using System.Data.OracleClient;

namespace FET.POS.Model.Facade.FacadeImpl
{
    public class DIS05_Facade
    {
        /// <summary>
        /// 取得查詢結果_促銷商品設定(主檔)
        /// </summary>
        /// <param name="PROMO_NO">促銷代號</param>
        /// <param name="PROMO_NAME">促銷名稱</param>
        /// <param name="BDATE_S">開始日期(起)</param>
        /// <param name="BDATE_E">開始日期(訖)</param>
        /// <param name="PROD_CONFIG_TYPE">商品型態(NULL/1/2)</param>
        /// <param name="NEED_TO_PRICING">變價規則(NULL/Y/N)</param>
        /// <param name="PROD_NO_S">商品料號(起)</param>
        /// <param name="PROD_NO_E">商品料號(訖)</param>
        /// <param name="PRODNAME">商品名稱</param>
        /// <returns>查詢結果</returns>
        public DataTable Query_MM(string PROMO_NO, string PROMO_NAME,
            string BDATE_S, string BDATE_E, string PROD_CONFIG_TYPE,
            string NEED_TO_PRICING, string PROD_NO_S, string PROD_NO_E,
            string PRODNAME)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT DISTINCT UUID, PROMO_NO, PROMO_NAME, ");
            sb.Append("B_DATE, E_DATE, PROD_ADDED_DATE, MM_TYPE, ");
            sb.Append("PROD_CONFIG_TYPE, PROMO_SUBSIDY, NEED_TO_PRICING, ");
            sb.Append("REMARK, MODI_USER, EMPNAME, MODI_DTM ");
            sb.Append("FROM VW_MM ");
            sb.Append("WHERE 1=1 ");

            if (!string.IsNullOrEmpty(PROMO_NO.Trim()))
            {
                sb.Append(" AND PROMO_NO LIKE " + OracleDBUtil.LikeStr(PROMO_NO.Trim()));
            }

            if (!string.IsNullOrEmpty(PROMO_NAME))
            {
                sb.Append(" AND PROMO_NAME LIKE " + OracleDBUtil.LikeStr(PROMO_NAME.Trim()));
            }

            if (!string.IsNullOrEmpty(BDATE_S.Trim()))
            {
                sb.Append(" AND B_DATE >= " + OracleDBUtil.DateStr(BDATE_S.Trim()));
            }

            if (!string.IsNullOrEmpty(BDATE_E.Trim()))
            {
                sb.Append(" AND B_DATE <= " + OracleDBUtil.DateStr(BDATE_E.Trim()));
            }

            if (!string.IsNullOrEmpty(PROD_CONFIG_TYPE.Trim()))
            {
                sb.Append(" AND PROD_CONFIG_TYPE = " + OracleDBUtil.SqlStr(PROD_CONFIG_TYPE.Trim()));
            }

            if (!string.IsNullOrEmpty(NEED_TO_PRICING.Trim()))
            {
                sb.Append(" AND NEED_TO_PRICING = " + OracleDBUtil.SqlStr(NEED_TO_PRICING.Trim()));
            }

            if (!string.IsNullOrEmpty(PROD_NO_S.Trim()))
            {
                sb.Append(" AND PROD_NO >= " + OracleDBUtil.SqlStr(PROD_NO_S.Trim()));
            }

            if (!string.IsNullOrEmpty(PROD_NO_E.Trim()))
            {
                sb.Append(" AND PROD_NO <= " + OracleDBUtil.SqlStr(PROD_NO_E.Trim()));
            }

            if (!string.IsNullOrEmpty(PRODNAME.Trim()))
            {
                sb.Append(" AND PRODNAME LIKE " + OracleDBUtil.LikeStr(PRODNAME.Trim()));
            }

            sb.Append(" ORDER BY B_DATE, PROMO_NO ");

            DataTable dt = OracleDBUtil.Query_Data(sb.ToString());
            return dt;
        }

        /// <summary>
        /// 取得查詢結果_商品類別
        /// </summary>
        /// <param name="MM_UUID">促銷主檔UUID</param>
        /// <returns>查詢結果</returns>
        public DataTable Query_MM_CATEGORY(string MM_UUID)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT  ");
            sb.Append("(select DTL.CATEGORY   from MM_CATEGORY_DTL DTL where DTL.MM_CATEGORY_M_UUID = M.UUID and CATEGORY='2G') AS A ,");
            sb.Append("(select DTL.CATEGORY   from MM_CATEGORY_DTL DTL where DTL.MM_CATEGORY_M_UUID = M.UUID and CATEGORY='3G') AS B , ");
            sb.Append("(select DTL.CATEGORY   from MM_CATEGORY_DTL DTL where DTL.MM_CATEGORY_M_UUID = M.UUID and CATEGORY='3.5G') AS C , ");
            sb.Append("(select DTL.CATEGORY   from MM_CATEGORY_DTL DTL where DTL.MM_CATEGORY_M_UUID = M.UUID and CATEGORY='Netbook') AS D ,");
            sb.Append("(select DTL.CATEGORY   from MM_CATEGORY_DTL DTL where DTL.MM_CATEGORY_M_UUID = M.UUID and CATEGORY='Datacard') AS E, ");
            sb.Append("(select DTL.CATEGORY   from MM_CATEGORY_DTL DTL where DTL.MM_CATEGORY_M_UUID = M.UUID and CATEGORY='Others') AS F,");
            sb.Append("M.SUB_SUBSIDY ");
            sb.Append("FROM MM_CATEGORY_M  M ");
            sb.Append("WHERE 1=1 ");
            sb.Append("AND M.MM_UUID = " + OracleDBUtil.SqlStr(MM_UUID));
            sb.Append(" and  (select count(*) from MM_CATEGORY_DTL DTL where DTL.MM_CATEGORY_M_UUID = M.UUID) > 0 ");

            DataTable dt = OracleDBUtil.Query_Data(sb.ToString());
            return dt;
        }

        /// <summary>
        /// 取得查詢結果_促銷商品
        /// </summary>
        /// <param name="MM_UUID">促銷主檔UUID</param>
        /// <returns>查詢結果</returns>
        public DataTable Query_MM_PLU(string MM_UUID)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT * ");
            sb.Append("FROM VW_MM_PLU ");
            sb.Append("WHERE 1=1 ");
            sb.Append("AND MM_UUID = " + OracleDBUtil.SqlStr(MM_UUID));

            DataTable dt = OracleDBUtil.Query_Data(sb.ToString());
            return dt;
        }

        /// <summary>
        /// 取得查詢結果_指定門市
        /// </summary>
        /// <param name="MM_UUID">促銷主檔UUID</param>
        /// <returns>查詢結果</returns>
        public DataTable Query_MM_STORE(string MM_UUID)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT * ");
            sb.Append("FROM VW_MM_STORE ");
            sb.Append("WHERE 1=1 ");
            sb.Append("AND MM_UUID = " + OracleDBUtil.SqlStr(MM_UUID));

            DataTable dt = OracleDBUtil.Query_Data(sb.ToString());
            return dt;
        }

    }
}
