using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Advtek.Utility;
using System.Data.OracleClient;


namespace FET.POS.Model.Common
{
    public class Common_PageHelper
    {

        /// <summary>
        /// 取得銷售倉的倉別UUID
        /// </summary>
        /// <returns>銷售倉UUID</returns>
        public static string GetGoodLOCUUID()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("select INV_GoodLOCUUID() as UUID from dual");

            DataTable dt = OracleDBUtil.Query_Data(sb.ToString());

            string UUID = "";
            if (dt.Rows.Count > 0)
            {
                UUID = dt.Rows[0]["UUID"].ToString();
            }

            return UUID;
        }

        /// <summary>
        /// 取得展示倉的倉別UUID
        /// </summary>
        /// <returns>展示倉UUID</returns>
        public static string GetDemoLOCUUID()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("select INV_DEMOLOCUUID() as UUID from dual");

            DataTable dt = OracleDBUtil.Query_Data(sb.ToString());

            string UUID = "";
            if (dt.Rows.Count > 0)
            {
                UUID = dt.Rows[0]["UUID"].ToString();
            }

            return UUID;
        }

        /// <summary>
        /// 取得機台編號 (只列出登入者門市的機台)
        /// </summary>
        /// <returns></returns>
        public static DataTable GetStoreTerminatingMachine(string STORE_NO)
        {
            DataTable dt = null;
            OracleConnection objConn = null;

            try
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("SELECT MACHINE_ID, HOST_NO ");
                strSql.Append("FROM STORE_TERMINATING_MACHINE ");
                strSql.Append("WHERE 1 = 1 ");
                strSql.Append("AND STORE_NO = " + OracleDBUtil.SqlStr(STORE_NO));

                objConn = OracleDBUtil.GetConnection();
                dt = OracleDBUtil.GetDataSet(objConn, strSql.ToString()).Tables[0];

                DataRow dr = dt.NewRow();
                dr["HOST_NO"] = "ALL";
                dr["MACHINE_ID"] = "";
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
        /// 取得下拉式"區域"代碼資料
        /// </summary>
        /// <param name="canEmpty">是否要顯示"ALL"選項</param>
        /// <returns></returns>
        public static DataTable getZone(bool canEmpty)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(@"
                    SELECT DISTINCT ZONE, ZONE_NAME 
                    FROM   ZONE
                    ORDER  BY ZONE 
                ");

            DataTable dt = OracleDBUtil.Query_Data(strSql.ToString());

            if (canEmpty)
            {
                DataRow dr = dt.NewRow();
                dr["ZONE_NAME"] = "ALL";
                dr["ZONE"] = "";
                dt.Rows.InsertAt(dr, 0);
            }

            return dt;
        }
        /// <summary>
        /// 取收據列印檔產生路徑
        /// </summary>
        /// <returns>收據列印檔產生路徑</returns>
        public static string GetPriReceipt_Path()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(@"select PARA_VALUE  from SYS_PARA where PARA_KEY='RECEIPT_PATH'");

            DataTable dt = OracleDBUtil.Query_Data(sb.ToString());

            string strPath = "";
            if (dt.Rows.Count > 0)
            {
                strPath = dt.Rows[0]["PARA_VALUE"].ToString();
            }

            return strPath;
        }

    }
}
