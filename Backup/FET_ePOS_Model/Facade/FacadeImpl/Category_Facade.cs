using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Advtek.Utility;
using System.Data;
using System.Data.OracleClient;

namespace FET.POS.Model.Facade.FacadeImpl
{
    public class Category_Facade
    {

        /// <summary>
        /// 取得查詢商品類別資料
        /// </summary>
        /// <param name="CATE_NO">商品分類代碼</param>
        /// <param name="CATE_NAME">商品分類名稱</param>
        /// <returns>查詢結果</returns>
        public DataTable Query_Category(string CATE_NO, string CATE_NAME)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT CATE_NO, CATE_NAME ");
            sb.Append("FROM PRODUCT_CATEGORY ");
            sb.Append("WHERE 1 =1 ");

            if (!string.IsNullOrEmpty((string)CATE_NO))
            {
                sb.Append(" AND CATE_NO LIKE ");
                sb.Append(OracleDBUtil.LikeStr(CATE_NO));
            }

            if (!string.IsNullOrEmpty((string)CATE_NAME))
            {
                sb.Append(" AND CATE_NAME LIKE ");
                sb.Append(OracleDBUtil.LikeStr(CATE_NAME));
            }

            DataTable dt = OracleDBUtil.Query_Data(sb.ToString());
            return dt;
        }

    }
}
