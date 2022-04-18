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
    public class OPT10_PageHelper
    {

        //檢核IMEI
        public static DataSet GetChkImeiTypeNo(bool canEmpty, string ed_status)
        {
            OracleConnection conn = null;
            DataSet objDS = null;
            StringBuilder strSql = null;

            try
            {
                strSql = new StringBuilder();
                strSql.Append("SELECT DISTINCT                                      ");
                strSql.Append("       CHECK_IMEI_TYPE      AS CHECK_IMEI_TYPE,      ");
                strSql.Append("       CHECK_IMEI_TYPE_NAME AS CHECK_IMEI_TYPE_NAME  ");
                strSql.Append("  FROM CHECK_IMEI_TYPE                               ");
                strSql.Append(" ORDER BY CHECK_IMEI_TYPE                            ");

                conn = OracleDBUtil.GetConnection();
                objDS = OracleDBUtil.GetDataSet(conn, strSql.ToString());

                if (canEmpty)
                {
                    if (ed_status == "Select")
                    {
                        DataRow dr = objDS.Tables[0].NewRow();
                        dr["CHECK_IMEI_TYPE_NAME"] = "ALL";
                        dr["CHECK_IMEI_TYPE"] = "";
                        objDS.Tables[0].Rows.InsertAt(dr, 0);
                    }
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

