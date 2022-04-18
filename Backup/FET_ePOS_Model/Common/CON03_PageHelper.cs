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
    public class CON03_PageHelper : BaseClass
    {

        /// <summary>
        /// 寄銷廠商CSM_SUPPLIER
        /// </summary>
        /// <param name="canEmpty">是否要顯示"ALL"選項</param>
        /// <returns></returns>
        public static DataTable GetSupplierNo(bool canEmpty)
        {
            OracleConnection conn = null;
            DataSet objDS = null;
            StringBuilder strSql = null;

            try
            {
                strSql = new StringBuilder();
                strSql.Append("SELECT DISTINCT                          ");
                strSql.Append("       SUPP_NO,        ");
                strSql.Append("       SUPP_NAME     ");
                strSql.Append("  FROM  CSM_SUPPLIER                      ");
                strSql.Append("  WHERE SYSDATE BETWEEN S_DATE AND E_DATE                     ");
                strSql.Append(" ORDER BY SUPP_NAME                    ");

                conn = OracleDBUtil.GetConnection();
                objDS = OracleDBUtil.GetDataSet(conn, strSql.ToString());

                if (canEmpty)
                {
                        DataRow dr = objDS.Tables[0].NewRow();
                        dr["SUPP_NAME"] = "ALL";
                        dr["SUPP_NO"] = "";
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

            return objDS.Tables[0];
        }

    }
}

