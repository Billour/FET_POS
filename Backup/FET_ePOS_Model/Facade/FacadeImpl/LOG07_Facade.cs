using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data;
using System.Data.OracleClient;
using System.Globalization;

using FET.POS.Model.Helper;
using FET.POS.Model.DTO;
using Advtek.Utility;

namespace FET.POS.Model.Facade.FacadeImpl
{
    public class LOG07_Facade
    {   
        /// <summary>
        /// 確認是否為店長
        /// </summary>
        /// <param name="STORE_NO">登入門市編號</param>
        /// <param name="LOGIN_ID">登入者ID</param>
        /// <returns></returns>
        public bool CheckLogIn(string STORE_NO, string LOGIN_ID)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(@"SELECT *
                        FROM V_RETAIL_SALESPERSON 
                        WHERE POSITIONID =8           ");
            if (!string.IsNullOrEmpty(STORE_NO))
            {
                sb.Append(" AND SALESCD = " + OracleDBUtil.SqlStr(STORE_NO));
            }

            if (!string.IsNullOrEmpty(LOGIN_ID))
            {
                sb.Append(" AND EMPLOYEEID = " + OracleDBUtil.SqlStr(LOGIN_ID));
            }

            DataTable dt = OracleDBUtil.Query_Data(sb.ToString());
            return dt.Rows.Count>0 ? true:false ;
        }
        /// <summary>
        /// 抓目前的密碼
        /// </summary>
        /// <param name="STORE_NO">登入門市編號</param>
        /// <returns></returns>
        public string  QueryOldPassword(string STORE_NO)
        {
            string StorePw = string.Empty;
            StringBuilder sb = new StringBuilder();
            sb.Append(@"SELECT STORE_PW
                        FROM STORE 
                        WHERE 1=1           ");
            if (!string.IsNullOrEmpty(STORE_NO))
            {
                sb.Append(" AND STORE_NO = " + OracleDBUtil.SqlStr(STORE_NO));
            }


            DataTable dt = OracleDBUtil.Query_Data(sb.ToString());
            StorePw = dt.Rows.Count > 0 ? dt.Rows[0]["STORE_PW"].ToString() : "";

            return StorePw;
        }

/// <summary>
/// 更新密碼
/// </summary>
/// <param name="STORE_NO">登入門市</param>
/// <param name="STORE_PW">新密碼</param>
/// <returns>執行筆數</returns>
        public int UpdatePassWord(string STORE_NO, string STORE_PW)
        {
            int intResult = 0;
            OracleConnection objConn = null;
            OracleTransaction objTx = null;
            try
            {
                objConn = OracleDBUtil.GetConnection();
                objTx = objConn.BeginTransaction();
                StringBuilder sb = new StringBuilder();
                sb.Append(@"UPDATE STORE SET  MODI_DTM = SYSDATE
                            , STORE_PW =" + OracleDBUtil.SqlStr(STORE_PW));
                sb.Append(" WHERE STORE_NO =" + OracleDBUtil.SqlStr(STORE_NO));
                intResult = OracleDBUtil.ExecuteSql(objTx, sb.ToString());
                objTx.Commit();
            }
            catch (Exception ex)
            {
                objTx.Rollback();
                throw ex;
            }
            finally
            {
                objTx.Dispose();
                if (objConn.State == ConnectionState.Open) objConn.Close();
                objConn.Dispose();
                OracleConnection.ClearAllPools();
            }
            return intResult;
        }

    
       
    }
}
