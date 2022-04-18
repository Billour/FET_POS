using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Advtek.Utility;
using System.Data.OracleClient;
using System.Data;

namespace FET.POS.Model.Common
{
    public class OPT18_PageHelper
    {
        /// <summary>
        /// 取得查詢門市店長折扣設定(Master Data)特定的一筆資料
        /// </summary>
        /// <param name="SSD_ID">特殊折扣設定主檔ID_UUID</param>
        /// <returns>查詢結果</returns>
        public static DataRow Query_StoreSpecialDisM_ByKey(string SSD_ID)
        {
            OracleConnection conn = null;

            try
            {
                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                sb.Append("SELECT * ");
                sb.Append("FROM STORE_SPECIAL_DIS_M ");
                sb.Append("WHERE SSD_ID = " + OracleDBUtil.SqlStr(SSD_ID));

                conn = OracleDBUtil.GetConnection();
                DataTable dt = OracleDBUtil.GetDataSet(conn, sb.ToString()).Tables[0];

                return dt.Rows[0];
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
        }

        /// <summary>
        /// 取得查詢門市店長折扣設定(Detail Data)特定的一筆資料
        /// </summary>
        /// <param name="SSDD_ID">UUID</param>
        /// <returns>查詢結果</returns>
        public static DataRow Query_StoreSpecialDisD_ByKey(string SSDD_ID)
        {
            OracleConnection conn = null;

            try
            {
                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                sb.Append("SELECT * ");
                sb.Append("FROM STORE_SPECIAL_DIS_D ");
                sb.Append("WHERE SSDD_ID = " + OracleDBUtil.SqlStr(SSDD_ID));

                conn = OracleDBUtil.GetConnection();
                DataTable dt = OracleDBUtil.GetDataSet(conn, sb.ToString()).Tables[0];

                return dt.Rows[0];
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
        }

        /// <summary>
        /// 取得查詢店長折扣的折扣料號
        /// </summary>
        /// <returns>店長折扣的折扣料號</returns>
        public static string GetDiscountNo()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT PARA_VALUE AS PRODNO FROM SYS_PARA WHERE PARA_KEY = 'STORE_SPECIAL_DIS_CODE'");

            DataTable dt = OracleDBUtil.Query_Data(sb.ToString());

            string strName = "";
            if (dt.Rows.Count > 0)
            {
                strName = dt.Rows[0]["PRODNO"].ToString();
            }
            return strName;
        }

        /// <summary>
        /// 取得查詢門市代碼和折扣月份 (判斷是否重複匯入)
        /// </summary>
        /// <param name="STORE_NO">門市代碼</param>
        /// <param name="YYMM">折扣月份</param>
        /// <param name="SSD_ID">UUID</param>
        /// <returns>Table中的筆數</returns>
        public static int Query_StoreSpecialDisM_ByParams(string STORE_NO, string YYMM, string SSD_ID)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT STORE_NO, YYMM ");
            sb.Append("FROM STORE_SPECIAL_DIS_M ");
            sb.Append("WHERE STORE_NO = " + OracleDBUtil.SqlStr(STORE_NO.Trim()));
            sb.Append(" AND YYMM = " + OracleDBUtil.SqlStr(YYMM.Trim()));
            sb.Append(" AND DEL_FLAG = 'N'");
            if (!string.IsNullOrEmpty(SSD_ID))
            {
                sb.Append(" AND SSD_ID <> " + OracleDBUtil.SqlStr(SSD_ID));
            }

            DataTable dt = OracleDBUtil.Query_Data(sb.ToString());
            return dt.Rows.Count;
        }

        /// <summary>
        /// 取得查詢角色資料
        /// </summary>
        /// <returns>角色資料</returns>
        public static DataTable GetRole()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT ROLE_ID, ROLE_NAME FROM STORE_ROLE");

            DataTable dt = OracleDBUtil.Query_Data(sb.ToString());
            return dt;
        }

        /// <summary>
        /// 取得查詢角色代號
        /// </summary>
        /// <param name="STORENO">角色代號</param>
        /// <returns>角色名稱</returns>
        public static string GetRoleIDByName(string ROLE_NAME)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT ROLE_ID, ROLE_NAME FROM STORE_ROLE WHERE ROLE_NAME = " + OracleDBUtil.SqlStr(ROLE_NAME));

            DataTable dt = OracleDBUtil.Query_Data(sb.ToString());

            string strID = "";
            if (dt.Rows.Count > 0)
            {
                strID = dt.Rows[0]["ROLE_ID"].ToString();
            }
            return strID;
        }


        /// <summary>
        /// 取得查詢門市店長折扣設定(Detail Data) 角色是否重複
        /// </summary>
        /// <param name="SSD_ID">特殊折扣設定主檔ID_UUID</param>
        /// <param name="SSDD_ID">明細檔ID_UUID</param>
        /// <param name="ROLE_ID">角色代號</param>
        /// <returns>查詢結果</returns>
        public static int GetStoreSpecialDisDByRole(string SSD_ID, string SSDD_ID, string ROLE_ID)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT *  FROM STORE_SPECIAL_DIS_D ");
            sb.Append("WHERE 1=1 ");
            sb.Append("AND DEL_FLAG = 'N' ");
            sb.Append("AND SSD_ID = " + OracleDBUtil.SqlStr(SSD_ID));
            sb.Append(" AND ROLE_ID = " + OracleDBUtil.SqlStr(ROLE_ID));

            if (!string.IsNullOrEmpty(SSDD_ID))
            {
                sb.Append(" AND SSDD_ID <> " + OracleDBUtil.SqlStr(SSDD_ID));
            }

            DataTable dt = OracleDBUtil.Query_Data(sb.ToString());
            return dt.Rows.Count;
        }

        /// <summary>
        /// 取得查詢門市店長折扣設定(Detail Data) 折扣上限金額加總排除目前輸入的
        /// </summary>
        /// <param name="SSD_ID">特殊折扣設定主檔ID_UUID</param>
        /// <param name="SSDD_ID">明細檔ID_UUID</param>
        /// <param name="ROLE_ID">角色代號</param>
        /// <returns>查詢結果</returns>
        public static string GetStoreDIS_AMT(string SSD_ID, string SSDD_ID, string ROLE_ID)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT nvl(SUM(DIS_AMT_UBOND),0) SUM_DIS_AMT  FROM STORE_SPECIAL_DIS_D ");
            sb.Append("WHERE 1=1 ");
            sb.Append("AND DEL_FLAG = 'N' ");
            sb.Append("AND SSD_ID = " + OracleDBUtil.SqlStr(SSD_ID));

            if (!string.IsNullOrEmpty(SSDD_ID))
            {
                sb.Append(" AND SSDD_ID <> " + OracleDBUtil.SqlStr(SSDD_ID));
            }

            DataTable dt = OracleDBUtil.Query_Data(sb.ToString());
            return dt.Rows[0]["SUM_DIS_AMT"].ToString();
        }


        /// <summary>
        /// 取得查詢門市店長折扣設定(Detail Data) 折扣上限金額加總排除目前輸入的
        /// </summary>
        /// <param name="SSD_ID">特殊折扣設定主檔ID_UUID</param>
        /// <param name="SSDD_ID">明細檔ID_UUID</param>
        /// <param name="ROLE_ID">角色代號</param>
        /// <returns>查詢結果</returns>
        public static string GetStoreMax_AMT(string SSD_ID, string SSDD_ID, string ROLE_ID)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT nvl(MAX(DIS_AMT_UBOND),0) Max_DIS_AMT  FROM STORE_SPECIAL_DIS_D ");
            sb.Append("WHERE 1=1 ");
            sb.Append("AND DEL_FLAG = 'N' ");
            sb.Append("AND SSD_ID = " + OracleDBUtil.SqlStr(SSD_ID));

            DataTable dt = OracleDBUtil.Query_Data(sb.ToString());
            return dt.Rows[0]["Max_DIS_AMT"].ToString();
        }

    }
}
