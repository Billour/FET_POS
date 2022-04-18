using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Advtek.Utility;
using System.Data;
using System.Data.OracleClient;

namespace FET.POS.Model.Common
{
    public class CON02_PageHelper
    {

        /// <summary>
        /// 依日期取得查詢 佣金比率設定 (判斷同一個料號的起迄日期是否有重疊)
        /// </summary>
        /// <param name="ACCU_ID">UUID</param>
        /// <param name="ACCU_NO">折扣料號</param>
        /// <param name="Date">開始或結束日期</param>
        /// <returns>查詢結果</returns>
        public static int Query_COMMISSIONRATE_ByDate(string ACCU_ID, string ACCU_NO, string sS_DATE, string sE_DATE)
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

        //CSM_SUPPLIER 寄外部廠商代號是否重覆
        public DataTable QuerySuppNO(string SUPP_NO, string Type)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(@" SELECT * FROM CSM_SUPPLIER 
                                 where SUPP_NO = " + OracleDBUtil.SqlStr(SUPP_NO.ToUpper()) + "  ");
            if (!string.IsNullOrEmpty(Type))
                sb.Append(" and CSM_TYPE = " + OracleDBUtil.SqlStr(Type));

            DataTable dt = OracleDBUtil.Query_Data(sb.ToString());
            return dt;
        }
    }
}
