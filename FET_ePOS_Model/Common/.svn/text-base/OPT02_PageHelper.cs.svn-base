using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Advtek.Utility;
using System.Data.OracleClient;
using System.Data;

namespace FET.POS.Model.Common
{
    public class OPT02_PageHelper
    {
        public static string TextField  = "CREDIT_CARD_TYPE_NAME";

        public static string ValueField = "CREDIT_CARD_TYPE_ID";

        /// <summary>
        /// 取得信用卡別設定資料
        /// </summary>
        /// <returns></returns>
        public static DataTable GetCreditCardTypes()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(" SELECT " + TextField + ", " + ValueField);
            strSql.Append(" FROM  CREDIT_CARD_TYPE ");

            DataTable dt = OracleDBUtil.Query_Data(strSql.ToString());
            return dt;
        }
    }
}
