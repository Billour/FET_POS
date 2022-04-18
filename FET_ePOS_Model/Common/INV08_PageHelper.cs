using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Data;
using System.Data.OracleClient;
using Advtek.Utility;

namespace FET.POS.Model.Common
{
    public class INV08_PageHelper
    {
        public static string TextField = "SUPPNAME";

        public static string ValueField = "SUPPNO";

        /// <summary>
        /// 取得供貨商設定資料
        /// </summary>
        /// <returns></returns>
        public static DataSet GetSupplierTypes(bool canEmpty,string Store_NO)
        {
            OracleConnection conn = null;
            DataSet objDS = null;
            StringBuilder strSql = null;

            try
            {
                strSql = new StringBuilder();
                //strSql.Append(" SELECT " + TextField + ", " + ValueField);
                //strSql.Append("   FROM  Supplier ");
                strSql = new StringBuilder();
                strSql.AppendLine(@" SELECT DISTINCT VENDORNAME AS SUPPNO
                                     ,VENDORNAME AS SUPPNAME  
                                     FROM QueryOEPOInfo Q WHERE VENDORNAME IS NOT NULL  ");
                strSql.AppendLine(" AND STORE_NO =  " + OracleDBUtil.SqlStr(Store_NO.Trim()));

                conn = OracleDBUtil.GetConnection();
                objDS = OracleDBUtil.GetDataSet(conn, strSql.ToString());

                if ((canEmpty))
                {
                    DataRow dr = objDS.Tables[0].NewRow();
                    dr["SUPPNAME"] = "ALL";
                    dr["SUPPNO"] = "";
                    objDS.Tables[0].Rows.InsertAt(dr, 0);

                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (conn.State == ConnectionState.Open) conn.Close();
                conn.Dispose();
                OracleConnection.ClearAllPools();
            }

            return objDS;
        }

    }
}
