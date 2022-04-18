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
    public class LOG04_PageHelper : BaseClass
    {
        //public static string TextField = "SYS_PARA_TYPE_NAME";
        //public static string ValueField = "SYS_PARA_TYPE_ID";

        //參數分類
        public static DataSet GetSysParaTypeId(bool canEmpty, string ed_status)
        {
            OracleConnection conn = null;
            DataSet objDS = null;
            StringBuilder strSql = null;

            try
            {
                strSql = new StringBuilder();
                strSql.Append(" SELECT DISTINCT             ");
                strSql.Append("       SYS_PARA_TYPE_ID      ");
                strSql.Append("      ,SYS_PARA_TYPE_NAME    ");
                strSql.Append("  FROM SYS_PARA_TYPE         ");
                strSql.Append(" ORDER BY SYS_PARA_TYPE_ID   ");

                conn = OracleDBUtil.GetConnection();
                objDS = OracleDBUtil.GetDataSet(conn, strSql.ToString());

                if ((canEmpty))
                {
                    if (ed_status == "Select")
                    {
                        DataRow dr = objDS.Tables[0].NewRow();
                        dr["SYS_PARA_TYPE_NAME"] = "ALL";
                        dr["SYS_PARA_TYPE_ID"] = "-999";
                        objDS.Tables[0].Rows.InsertAt(dr, 0);
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Log.Error(ex.Message, ex);
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

