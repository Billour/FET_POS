using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Advtek.Utility;
using System.Data;
using System.Data.OracleClient;

namespace FET.POS.Model.Common
{
    public class CHK05_PageHelper
    {

        /// <summary>
        /// 取得查詢繳大鈔特定的一筆資料
        /// </summary>
        /// <param name="ID">UUID</param>
        /// <returns>查詢結果</returns>
        public static DataTable Query_BigMoney_ByKey(string ID)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT * ");
            sb.Append("FROM BIG_MONEY ");
            sb.Append("WHERE ID = " + OracleDBUtil.SqlStr(ID));

            DataTable dt = OracleDBUtil.Query_Data(sb.ToString());
            return dt;
        }

        /// <summary>
        /// 依照門市編號、機台代號、營業日期取得資料筆數 (批號累加)
        /// </summary>
        /// <param name="STORE_NO">門市編號</param>
        /// <param name="MACHINE_ID">機台代號</param>
        /// <param name="WorkDate">營業日</param>
        /// <returns>Table資料筆數</returns>
        public static int GetDataCount(string STORE_NO, string MACHINE_ID, string WorkDate)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT BATCH_NO ");
            sb.Append("FROM BIG_MONEY ");
            sb.Append("WHERE 1 = 1 ");
            sb.Append("AND TO_CHAR(TRADE_DATE, 'YYYY/MM/DD') = " + OracleDBUtil.SqlStr(WorkDate));
            sb.Append(" AND STORE_NO = " + OracleDBUtil.SqlStr(STORE_NO));
            sb.Append(" AND MACHINE_ID =  " + OracleDBUtil.SqlStr(MACHINE_ID));

            DataTable dt = OracleDBUtil.Query_Data(sb.ToString());
            return dt.Rows.Count;
        }

        /// <summary>
        /// 依照門市編號、機台代號取得機台編號
        /// </summary>
        /// <param name="STORE_NO">門市編號</param>
        /// <param name="MACHINE_ID">機台代號</param>
        /// <returns>機台編號</returns>
        public static string GetHostNo(string STORE_NO, string MACHINE_ID)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(@"SELECT HOST_NO 
                            FROM STORE_TERMINATING_MACHINE 
                            WHERE 1 = 1 
                            AND STORE_NO = " + OracleDBUtil.SqlStr(STORE_NO) + @"
                            AND HOST_NO =  " + OracleDBUtil.SqlStr(MACHINE_ID)
                       );
            //AND MACHINE_ID =  " + OracleDBUtil.SqlStr(MACHINE_ID)
            DataTable dt = OracleDBUtil.Query_Data(sb.ToString());
            string strNo = "";
            if (dt.Rows.Count > 0)
            {
                strNo = dt.Rows[0]["HOST_NO"].ToString();
            }
            return strNo;
        }

    }
}
