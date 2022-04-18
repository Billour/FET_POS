using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Advtek.Utility;
using System.Data.OracleClient;

namespace FET.POS.Model.Common
{
    public class OPT12_PageHelper
    {
        /// <summary>
        /// 取得查詢HappyGo點數累點設定
        /// </summary>
        /// <param name="ACCU_ID">UUID</param>
        /// <returns>查詢結果</returns>
        public static DataTable Query_HgAccumulate_ByKey(string ACCU_ID)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT * ");
            sb.Append("FROM HG_ACCUMULATE ");
            sb.Append("WHERE ACCU_ID = " + OracleDBUtil.SqlStr(ACCU_ID));

            DataTable dt = OracleDBUtil.Query_Data(sb.ToString());
            return dt;
        }

        /// <summary>
        /// 取得查詢排外條件設定
        /// </summary>
        /// <param name="SID">UUID</param>
        /// <returns>查詢結果</returns>
        public static DataTable Query_HgAccuExcludeProd_ByKey(string SID)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT * ");
            sb.Append("FROM HG_ACCU_EXCLUDE_PROD ");
            sb.Append("WHERE SID = " + OracleDBUtil.SqlStr(SID));

            DataTable dt = OracleDBUtil.Query_Data(sb.ToString());
            return dt;
        }

        /// <summary>
        /// 依日期取得查詢HappyGo點數累點設定 (判斷同一個料號的起迄日期是否有重疊)
        /// </summary>
        /// <param name="ACCU_ID">UUID</param>
        /// <param name="ACCU_NO">折扣料號</param>
        /// <param name="Date">開始或結束日期</param>
        /// <returns>查詢結果</returns>
        public static int Query_HgAccumulate_ByDate(string ACCU_ID, string ACCU_NO, string sS_DATE, string sE_DATE)
        {
            StringBuilder sb = new StringBuilder();
            DateTime dStart = DateTime.Parse(sS_DATE);
            DateTime dStop = DateTime.Parse((string.IsNullOrEmpty(sE_DATE)) ? "9999/12/31" : sE_DATE);

            sb.Append("SELECT 1 ");
            sb.Append("FROM HG_ACCUMULATE ");
            sb.Append("WHERE 1 = 1 ");
            sb.Append("    AND DELETE_FLAG = 'N' ");
            sb.Append("    AND ACCU_NO = " + OracleDBUtil.SqlStr(ACCU_NO));
            if (!string.IsNullOrEmpty(ACCU_ID))
            {
                sb.Append(" AND ACCU_ID <> " + OracleDBUtil.SqlStr(ACCU_ID));
            }

            sb.Append(@"  AND (  
                                maxdate(TO_CHAR(s_date, 'yyyy/mm/dd'), " + OracleDBUtil.SqlStr(dStart.ToString("yyyy/MM/dd")) + ") ");
            sb.Append(@"      <= mindate(TO_CHAR (nvl(e_date,to_date('9999/12/31','yyyy/MM/dd')), 'yyyy/MM/dd'), " + OracleDBUtil.SqlStr(dStop.ToString("yyyy/MM/dd")) + ") )");

            DataTable dt = OracleDBUtil.Query_Data(sb.ToString());
            return dt.Rows.Count;
        }

        /// <summary>
        /// 依商品料號取得排外條件設定 (判斷同一個料號是否重複加入)
        /// </summary>
        /// <param name="PRODNO">商品料號</param>
        /// <param name="SID">UUID</param>
        /// <returns>查詢結果</returns>
        public static int Query_HgAccuExcludeProd_ByProdNo(string PRODNO, string SID)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT * ");
            sb.Append("FROM HG_ACCU_EXCLUDE_PROD ");
            sb.Append("WHERE 1 = 1 ");
            sb.Append("AND DELETE_FLAG = 'N' ");
            sb.Append("AND PRODNO = " + OracleDBUtil.SqlStr(PRODNO));
            if (!string.IsNullOrEmpty(SID))
            {
                sb.Append(" AND SID <> " + OracleDBUtil.SqlStr(SID));
            }

            DataTable dt = OracleDBUtil.Query_Data(sb.ToString());
            return dt.Rows.Count;
        }

        /// <summary>
        /// 取得折扣料號
        /// </summary>
        /// <returns></returns>
        public static string GetDiscountMaster(string strDiscount)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT DISCOUNT_NAME FROM DISCOUNT_MASTER ");
            sb.Append("WHERE DISCOUNT_CODE = " + OracleDBUtil.SqlStr(strDiscount));

            DataTable dt = OracleDBUtil.Query_Data(sb.ToString());

            string strName = "";
            if (dt.Rows.Count > 0)
            {
                strName = dt.Rows[0]["DISCOUNT_NAME"].ToString();
            }

            return strName;
        }
    
    }
}
