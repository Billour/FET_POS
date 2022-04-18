using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Advtek.Utility;
using System.Data.OracleClient;
using System.Data;

namespace FET.POS.Model.Common
{
    public class OPT16_PageHelper
    {
        /// <summary>
        /// 取得查詢主檔的折扣料號筆數 (判斷是否存在於主檔)
        /// </summary>
        /// <param name="ACTIVITY_NO">折扣料號</param>
        /// <param name="FUNID">Function ID(ex. OPT13 OPT15)</param>
        /// <returns>Table中的筆數</returns>
        public static int Query_HgConvertibleGiftM_ByActivityNo(string ACTIVITY_NO, string FUNID)
        {
            string TableName = "";

            if (FUNID == "OPT15")
            {
                TableName = "HG_CONVERTIBLE_GIFT_M";
            }

            if (!string.IsNullOrEmpty(TableName))
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("SELECT ACTIVITY_NO ");
                sb.Append("FROM " + TableName + " ");
                sb.Append("WHERE ACTIVITY_NO = " + OracleDBUtil.SqlStr(ACTIVITY_NO));

                DataTable dt = OracleDBUtil.Query_Data(sb.ToString());
                return dt.Rows.Count;
            }
            else
            {
                return 0;
            }
        }

    }
}
