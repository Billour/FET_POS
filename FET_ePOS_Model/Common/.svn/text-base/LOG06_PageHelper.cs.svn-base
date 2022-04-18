using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.OracleClient;
using FET.POS.Model.Helper;
using FET.POS.Model.DTO;
using Advtek.Utility;
using System.Collections;

namespace FET.POS.Model.Common
{
    public class LOG06_PageHelper
    {
        //參數分類
        public static DataTable GetSHDP(string SID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(" SELECT * ");
            strSql.Append("  FROM STORE_HEADER_DISCOUNT_PWD         ");
            strSql.Append(" WHERE 1=1");
            strSql.Append(" AND SID = ");
            strSql.Append(OracleDBUtil.SqlStr(SID));

            DataTable dt = OracleDBUtil.Query_Data(strSql.ToString());

            return dt;
        }
    }
}
