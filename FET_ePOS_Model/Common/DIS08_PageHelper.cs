using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Advtek.Utility;
using System.Data;

namespace FET.POS.Model.Common
{
    public class DIS08_PageHelper : BaseClass
    {
        public static DataTable GetCategoryList()
        {
            DataTable dt = null;

            try
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("SELECT CATE_NO,CATE_NAME ");
                strSql.Append("FROM PRODUCT_CATEGORY ");
                strSql.Append("WHERE 1 = 1 ");

                dt = OracleDBUtil.GetDataSet(OracleDBUtil.GetConnection(), strSql.ToString()).Tables[0];

                DataRow dr = dt.NewRow();
                dr["CATE_NO"] = "ALL";
                dr["CATE_NAME"] = "ALL";
                dt.Rows.InsertAt(dr, 0);
            }
            catch (Exception ex)
            {
                Logger.Log.Error(ex.Message, ex);
                throw ex;
            }

            return dt;

        }
    }

}