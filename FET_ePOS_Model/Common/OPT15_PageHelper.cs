using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Advtek.Utility;
using System.Data.OracleClient;
using System.Data;

namespace FET.POS.Model.Common
{
    public class OPT15_PageHelper
    {
        /// <summary>
        /// 取得查詢HappyGo點數兌換-來店禮(Master Data)特定的一筆資料
        /// </summary>
        /// <param name="ACTIVITY_ID">UUID</param>
        /// <returns>查詢結果</returns>
        public static DataTable Query_HgConvertibleGiftM_ByKey(string ACTIVITY_ID)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT GIFT_M.* ");
            sb.Append("FROM HG_CONVERTIBLE_GIFT_M GIFT_M ");
            sb.Append("WHERE GIFT_M.ACTIVITY_ID = " + OracleDBUtil.SqlStr(ACTIVITY_ID));

            DataTable dt = OracleDBUtil.Query_Data(sb.ToString());
            return dt;

        }

        /// <summary>
        /// 取得查詢HappyGo點數兌換-來店禮(Detail Data)特定的一筆資料
        /// </summary>
        /// <param name="SID">UUID</param>
        /// <returns>查詢結果</returns>
        public static DataRow Query_HgConvertibleGiftD_ByKey(string SID)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT GIFT_D.* ");
            sb.Append("FROM HG_CONVERT_GIFT_D GIFT_D ");
            sb.Append("WHERE GIFT_D.SID = " + OracleDBUtil.SqlStr(SID));

            DataTable dt = OracleDBUtil.Query_Data(sb.ToString());
            return dt.Rows[0];

        }

        /// <summary>
        /// 取得查詢主檔的折扣料號筆數 (判斷是否存在於主檔)
        /// </summary>
        /// <param name="ACTIVITY_NO">折扣料號</param>
        /// <param name="ACTIVITY_ID">UUID</param>
        /// <returns>Table中的筆數</returns>
        public static int Query_HgConvertibleGiftM_ByActivityNo(string ACTIVITY_NO, string sS_DATE, string sE_DATE, string OldACTIVITY_NO)
        {
            StringBuilder sb = new StringBuilder();
            DateTime dStart = DateTime.Parse(sS_DATE);
            DateTime dStop = DateTime.Parse((string.IsNullOrEmpty(sE_DATE)) ? "9999/12/31" : sE_DATE);

            sb.Append("SELECT ACTIVITY_NO ");
            sb.Append("FROM HG_CONVERTIBLE_GIFT_M ");
            sb.Append("WHERE ACTIVITY_NO = " + OracleDBUtil.SqlStr(ACTIVITY_NO));
            sb.Append(" AND DEL_FLAG = 'N'");
            sb.Append(" AND (maxdate(TO_CHAR (s_date, 'yyyy/mm/dd'), " + OracleDBUtil.SqlStr(dStart.ToString("yyyy/MM/dd")) + ") ");
            sb.Append("      <= mindate(TO_CHAR (nvl(e_date,to_date('9999/12/31','yyyy/MM/dd')), 'yyyy/MM/dd'), " + OracleDBUtil.SqlStr(dStop.ToString("yyyy/MM/dd")) + ") )");
            if (OldACTIVITY_NO != "")
            {
                sb.Append(" AND ACTIVITY_NO <> " + OracleDBUtil.SqlStr(OldACTIVITY_NO));
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
