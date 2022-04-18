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
    public class INV05_PageHelper
    {
        public static string TextField = "ZONE_NAME";

        public static string ValueField = "ZONE";

        /// <summary>
        /// 取得區域別設定資料
        /// </summary>
        /// <returns></returns>
        public static DataTable GetZoneTypes()
        {
            DataTable dt = null;
            OracleConnection objConn = null;

            try
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append(" SELECT " + TextField + ", " + ValueField);
                strSql.Append("   FROM  ZONE ");

                objConn = OracleDBUtil.GetConnection();
                dt = OracleDBUtil.GetDataSet(objConn, strSql.ToString()).Tables[0];

                DataRow dr = dt.NewRow();
                dr[TextField] = "請選擇";
                dr[ValueField] = "";
                dt.Rows.InsertAt(dr, 0);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (objConn.State == ConnectionState.Open) objConn.Close();
                objConn.Dispose();
                OracleConnection.ClearAllPools();
            }


            return dt;
        }

        /// <summary>
        /// 取得退倉原因設定資料
        /// </summary>
        /// <returns></returns>
        public static DataSet GetRtRsCo(bool canEmpty )
        {
            OracleConnection conn = null;
            DataSet objDS = null;

            try
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("SELECT DISTINCT                                  ");
                strSql.Append("       RETURN_REASON_CODE AS RETURN_REASON_CODE, ");
                strSql.Append("       RETURN_DESCRIPTION AS RETURN_DESCRIPTION  ");
                strSql.Append("  FROM RETURN_REASON                             ");
                strSql.Append(" ORDER BY RETURN_REASON_CODE                     ");

                conn = OracleDBUtil.GetConnection();
                objDS = OracleDBUtil.GetDataSet(conn, strSql.ToString());

                if ((canEmpty))
                { 
                    DataRow dr = objDS.Tables[0].NewRow();
                    dr["RETURN_DESCRIPTION"] = "請選擇";
                    dr["RETURN_REASON_CODE"] = "";
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

        /// <summary>
        /// 取得後續處理設定資料
        /// </summary>
        /// <returns></returns>
        public static DataSet GetAftProCo(bool canEmpty)
        {
            OracleConnection conn = null;
            DataSet objDS = null;
            StringBuilder strSql = null;

            try
            {
                strSql = new StringBuilder();
                strSql.Append("SELECT DISTINCT                                  ");
                strSql.Append("       AFTER_PROCESS_CODE AS AFTER_PROCESS_CODE, ");
                strSql.Append("       DESCRIPTION        AS DESCRIPTION         ");
                strSql.Append("  FROM AFTER_PROCESS                             ");
                strSql.Append(" ORDER BY AFTER_PROCESS_CODE                     ");

                conn = OracleDBUtil.GetConnection();
                objDS = OracleDBUtil.GetDataSet(conn, strSql.ToString());

                if ((canEmpty))
                {
                    DataRow dr = objDS.Tables[0].NewRow();
                    dr["DESCRIPTION"] = "請選擇";
                    dr["AFTER_PROCESS_CODE"] = "";
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
