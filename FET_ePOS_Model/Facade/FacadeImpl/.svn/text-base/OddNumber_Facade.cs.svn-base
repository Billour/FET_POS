using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.OracleClient;
using Advtek.Utility;
using FET.POS.Model.DTO;

namespace FET.POS.Model.Facade.FacadeImpl
{
    public class OddNumber_Facade
    {

        public DataTable Query_OddNumber(string TableName, string ColumnName, string sOddNo)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(" SELECT DISTINCT " + ColumnName + " as OddNo ");
            strSql.Append("   FROM  " + TableName + " ");
            strSql.Append("  WHERE 1=1 ");
            if (sOddNo != "")
              strSql.Append("    AND " + ColumnName + " Like " + OracleDBUtil.LikeStr(sOddNo));
            strSql.Append(" Order by 1");
            DataTable dt = OracleDBUtil.Query_Data(strSql.ToString());
         
            return dt;

        }

        public DataTable Query_Prepay_Head(string TableName, string Store, string sOddNo)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(" SELECT DISTINCT  PREPAY_NO as OddNo ");
            strSql.Append("   FROM  " + TableName + " ");
            strSql.Append("  WHERE 1=1 ");
            if (Store != "HQ")
                strSql.Append("    AND STORE_NO = " + OracleDBUtil.SqlStr(Store));

            if (sOddNo != "")
                strSql.Append("    AND PREPAY_NO  Like " + OracleDBUtil.LikeStr(sOddNo));
            strSql.Append(" Order by 1");
            DataTable dt = OracleDBUtil.Query_Data(strSql.ToString());

            return dt;

        }
    }
}
