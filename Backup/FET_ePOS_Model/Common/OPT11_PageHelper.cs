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
    public class OPT11_PageHelper
    {
        //類別
        public static string TextField = "HG_EXCHANGE_TYPE";

        public static string ValueField = "HG_EXCHANGE_TYPE_NAME";

        //參數分類
        public static DataSet GetTypeId(bool canEmpty)
        {
            OracleConnection conn = null;
            DataSet objDS = null;
            StringBuilder strSql = null;

            try
            {
                strSql = new StringBuilder();
                strSql.Append(" SELECT DISTINCT " + TextField + ", " + ValueField);
                strSql.Append("  FROM HG_EXCHANGE_TYPE         ");
                strSql.Append(" ORDER BY HG_EXCHANGE_TYPE   ");

                conn = OracleDBUtil.GetConnection();
                objDS = OracleDBUtil.GetDataSet(conn, strSql.ToString());

                if (canEmpty)
                {
                    DataRow dr = objDS.Tables[0].NewRow();
                    dr["HG_EXCHANGE_TYPE_NAME"] = "ALL";
                    dr["HG_EXCHANGE_TYPE"] = "";
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

        public static void DeleteCutOffDateMethodData(DataTable dt, string eKey)
        {
            OracleDBUtil.DELETEByUUID(dt, eKey);
        }

        /// <summary>
        /// 取得查詢折扣商品
        /// </summary>
        /// <returns>折扣商品</returns>
        public static DataTable GetDiscountMaster()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT DISCOUNT_MASTER_ID, DISCOUNT_NAME,DISCOUNT_CODE FROM DISCOUNT_MASTER WHERE DISCOUNT_TYPE = 5 ");

            DataTable dt = OracleDBUtil.Query_Data(sb.ToString());
            return dt;
        }

        /// <summary>
        /// 檢查同一類別的折扣料號是否已重複
        /// </summary>
        /// <returns>true=已重複 or false=沒重複</returns>
        public static bool CheckCvNo(string sCvNo, string sType, object CONVERT_ID)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT CONVERT_NO FROM HG_CONVERTIBLE ");
            sb.Append("WHERE CONVERT_NO = " + OracleDBUtil.SqlStr(sCvNo) + " AND HG_EXCHANGE_TYPE = " + OracleDBUtil.SqlStr(sType));
            if (CONVERT_ID != null && !string.IsNullOrEmpty(CONVERT_ID.ToString()))
            {
                sb.Append(" AND CONVERT_ID <> " + OracleDBUtil.SqlStr(CONVERT_ID.ToString()));
            }

            DataTable dt = OracleDBUtil.Query_Data(sb.ToString());

            if (dt.Rows.Count == 0)
                return false;
            else
                return true;

        }


    }
}