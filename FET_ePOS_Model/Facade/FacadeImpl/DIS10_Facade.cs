using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Advtek.Utility;
using System.Data;
using System.Data.OracleClient;

namespace FET.POS.Model.Facade.FacadeImpl
{
    public class DIS10_Facade
    {
        /// <summary>
        /// 取得查詢結果ERP Attribute1、商品類別對應維護檔
        /// </summary>
        /// <param name="CATEGORY">商品類別</param>
        /// <param name="ERP_ATTRIBUTE1">ERP屬性欄位1</param>
        /// <returns>查詢結果</returns>
        public DataTable Query_ProdMapping(string CATEGORY, string ERP_ATTRIBUTE1)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT * ");
            sb.Append("FROM VW_PROD_MAPPING ");
            sb.Append("WHERE 1=1 ");

            if (!string.IsNullOrEmpty(CATEGORY))
            {
                sb.Append(" AND CATEGORY = " + OracleDBUtil.SqlStr(CATEGORY));
            }

            if (!string.IsNullOrEmpty(ERP_ATTRIBUTE1))
            {
                sb.Append(" AND ERP_ATTRIBUTE1 LIKE " + OracleDBUtil.LikeStr(ERP_ATTRIBUTE1));
            }

            DataTable dt = OracleDBUtil.Query_Data(sb.ToString());
            return dt;
        }
    }
}
