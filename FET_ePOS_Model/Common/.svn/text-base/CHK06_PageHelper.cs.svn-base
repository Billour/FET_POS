using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Advtek.Utility;
using System.Data;
using System.Data.OracleClient;

namespace FET.POS.Model.Common
{
    public class CHK06_PageHelper
    {
        /// <summary>
        /// 依照虛擬帳號取得門市編號
        /// </summary>
        /// <param name="ACCOUNT">虛擬帳號</param>
        /// <returns>門市編號</returns>
        public static string GetStoreNoByACCOUNT(string ACCOUNT)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT STORE_NO ");
            sb.Append("FROM STORE_ACCOUNT ");
            sb.Append("WHERE 1 = 1 ");
            sb.Append(" AND ACCOUNT = " + OracleDBUtil.SqlStr(ACCOUNT));

            DataTable dt = OracleDBUtil.Query_Data(sb.ToString());
            string strNo = "";
            if (dt.Rows.Count > 0)
            {
                strNo = dt.Rows[0]["STORE_NO"].ToString();
            }
            return strNo;
        }

    }
}
