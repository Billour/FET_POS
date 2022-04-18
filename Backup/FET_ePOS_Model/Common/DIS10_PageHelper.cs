using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Advtek.Utility;
using System.Data;

namespace FET.POS.Model.Common
{
    public class DIS10_PageHelper
    {
        /// <summary>
        /// 取得商品分類
        /// </summary>
        /// <returns></returns>
        public static DataTable GetProductCategory()
        {
            DataTable dt = null;

            try
            {
                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                sb.Append("SELECT CATE_NO, CATE_NAME FROM VW_PRODUCT_CATEGORY ");

                dt = OracleDBUtil.GetDataSet(OracleDBUtil.GetConnection(), sb.ToString()).Tables[0];

                DataRow dr = dt.NewRow();
                dr["CATE_NO"] = "";
                dr["CATE_NAME"] = "ALL";
                dt.Rows.InsertAt(dr, 0);
                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
    }
}
