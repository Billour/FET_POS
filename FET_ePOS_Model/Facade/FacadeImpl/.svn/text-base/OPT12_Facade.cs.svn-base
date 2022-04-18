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
    public class OPT12_Facade
    {

        /// <summary>
        /// 取得查詢結果HappyGo點數累點設定
        /// </summary>
        /// <param name="txtAccuName">累點名稱</param>
        /// <param name="txtSDateS">開始日期(起)</param>
        /// <param name="txtSDateE">開始日期(迄)</param>
        /// <param name="txtAccuCurrencyS">累點金額(起)</param>
        /// <param name="txtAccuCurrencyE">累點金額(迄)</param>
        /// <param name="txtDividablePointS">累點點數(起)</param>
        /// <param name="txtDividablePointE">累點點數(迄)</param>
        /// <returns>查詢結果</returns>
        public DataTable Query_HgAccumulate(string txtAccuName, string txtSDateS
            , string txtSDateE, string txtDividablePointS, string txtDividablePointE,
            string txtAccuCurrencyS, string txtAccuCurrencyE)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(" SELECT M.ACCU_NO, M.ACCU_ID, M.ACCU_NAME, M.S_DATE, ");
            sb.Append(" M.E_DATE, M.ACCU_CURRENCY, M.DIVIDABLE_POINT, ");
            sb.Append(" M.MODI_USER, E.EMPNAME AS EMPNAME, TO_CHAR(M.MODI_DTM, 'yyyy/mm/dd hh24:mi:ss') MODI_DTM ");
            sb.Append(" FROM HG_ACCUMULATE M, EMPLOYEE E ");
            sb.Append(" WHERE M.MODI_USER = E.EMPNO ");
            sb.Append(" AND M.DELETE_FLAG = 'N'");

            if (!string.IsNullOrEmpty(txtAccuName))
            {
                sb.Append(" AND M.ACCU_NAME LIKE " + OracleDBUtil.LikeStr(txtAccuName));
            }

            if (!string.IsNullOrEmpty(txtSDateS))
            {
                sb.Append(" AND M.S_DATE >= " + OracleDBUtil.DateStr(txtSDateS));
            }

            if (!string.IsNullOrEmpty(txtSDateE))
            {
                sb.Append(" AND M.S_DATE <= " + OracleDBUtil.DateStr(txtSDateE));
            }

            if (!string.IsNullOrEmpty(txtDividablePointS))
            {
                sb.Append(" AND TO_NUMBER(M.DIVIDABLE_POINT) >= " + OracleDBUtil.NumberStr(txtDividablePointS));
            }

            if (!string.IsNullOrEmpty(txtDividablePointE))
            {
                sb.Append(" AND TO_NUMBER(M.DIVIDABLE_POINT) <= " + OracleDBUtil.NumberStr(txtDividablePointE));
            }

            if (!string.IsNullOrEmpty(txtAccuCurrencyS))
            {
                sb.Append(" AND TO_NUMBER(M.ACCU_CURRENCY) >= " + OracleDBUtil.NumberStr(txtAccuCurrencyS));
            }

            if (!string.IsNullOrEmpty(txtAccuCurrencyE))
            {
                sb.Append(" AND TO_NUMBER(M.ACCU_CURRENCY) <= " + OracleDBUtil.NumberStr(txtAccuCurrencyE));
            }

            sb.Append(" ORDER BY M.ACCU_NO, M.S_DATE");

            DataTable dt = OracleDBUtil.Query_Data(sb.ToString());
            return dt;
        }

        /// <summary>
        /// 查詢已生效的資料
        /// </summary>
        /// <returns>DataTable</returns>
        public DataTable Query_HgAccumulate_Effective()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(" SELECT M.ACCU_NO, M.ACCU_ID, M.ACCU_NAME, M.S_DATE, ");
            sb.Append(" M.E_DATE, M.ACCU_CURRENCY, M.DIVIDABLE_POINT, ");
            sb.Append(" M.MODI_USER, M.MODI_USER || '  ' ||E.EMPNAME AS EMPNAME, TO_CHAR(M.MODI_DTM, 'yyyy/mm/dd hh24:mi:ss')' MODI_DTM ");
            sb.Append(" FROM HG_ACCUMULATE M, EMPLOYEE E ");
            sb.Append(" WHERE M.MODI_USER = E.EMPNO ");
            sb.Append(" AND M.DELETE_FLAG = 'N'");
            sb.Append(" AND M.S_DATE <= sysdate ");
            sb.Append(" AND (NVL(TO_CHAR(M.E_DATE), ' ') = ' ' OR M.E_DATE > sysdate) ");
            sb.Append(" ORDER BY M.ACCU_NO, M.S_DATE");

            DataTable dt = OracleDBUtil.Query_Data(sb.ToString());
            return dt;

        }

        /// <summary>
        /// 新增HappyGo點數累點設定
        /// </summary>
        /// <param name="ds">OPT12 DataSet</param>
        public void AddNewOne_HgAccumulate(OPT12_HgAccumulate_DTO ds)
        {
            OracleDBUtil.Insert(ds.Tables["HG_ACCUMULATE"]);
        }

        /// <summary>
        /// 修改HappyGo點數累點設定
        /// </summary>
        /// <param name="ds">OPT12 DataSet</param>
        public void UpdateOne_HgAccumulate(OPT12_HgAccumulate_DTO ds)
        {
            OracleConnection objConn = null;
            OracleTransaction objTX = null;

            try
            {
                objConn = OracleDBUtil.GetConnection();
                objTX = objConn.BeginTransaction();

                OracleDBUtil.UPDDATEByUUID_IncludeDBNull(objTX, ds.Tables["HG_ACCUMULATE"], "ACCU_ID");

                objTX.Commit();
            }
            catch (Exception ex)
            {
                objTX.Rollback();
                throw ex;
            }
            finally
            {
                objTX.Dispose();

                if (objConn.State == ConnectionState.Open) objConn.Close();
                objConn.Dispose();
                OracleConnection.ClearAllPools();
            }
        }

        /// <summary>
        /// 刪除HappyGo點數累點設定
        /// </summary>
        /// <param name="ds">OPT12 DataSet</param>
        public void Delete_HgAccumulate(OPT12_HgAccumulate_DTO ds)
        {
            OracleConnection objConn = null;
            OracleTransaction objTX = null;

            try
            {
                objConn = OracleDBUtil.GetConnection();
                objTX = objConn.BeginTransaction();

                OracleDBUtil.UPDDATEByUUID(objTX, ds.Tables["HG_ACCUMULATE"], "ACCU_ID");  //將 DEL_FLAS 設為 Y

                objTX.Commit();
            }
            catch (Exception ex)
            {
                objTX.Rollback();
                throw ex;
            }
            finally
            {
                objTX.Dispose();

                if (objConn.State == ConnectionState.Open) objConn.Close();
                objConn.Dispose();
                OracleConnection.ClearAllPools();
            }
        }

        /// <summary>
        /// 取得查詢結果排外條件設定
        /// </summary>
        /// <returns>查詢結果</returns>
        public DataTable Query_HgAccuExcludeProd()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT C.SID, P.PRODNO, P.PRODNAME  ");
            sb.Append("FROM HG_ACCU_EXCLUDE_PROD C, PRODUCT P ");
            sb.Append("WHERE C.PRODNO = P.PRODNO ");
            sb.Append("AND C.DELETE_FLAG = 'N' ");
            sb.Append("ORDER BY P.PRODNO ");

            DataTable dt = OracleDBUtil.Query_Data(sb.ToString());
            return dt;
        }

        /// <summary>
        /// 取得查詢結果銷售ID設定
        /// </summary>
        /// <returns>查詢結果</returns>
        public DataTable Query_DiscountID(string RTNNO)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT DISCOUNT_MASTER_ID  FROM DISCOUNT_MASTER  ");
            sb.Append("WHERE 1 = 1 ");
            sb.Append("AND DISCOUNT_CODE = ");
            sb.Append(Advtek.Utility.OracleDBUtil.SqlStr(RTNNO));

            DataTable dt = OracleDBUtil.Query_Data(sb.ToString());
            return dt;
        }

        /// <summary>
        /// 新增排外條件設定
        /// </summary>
        /// <param name="ds">OPT12 DataSet</param>
        public void AddNewOne_HgAccuExcludeProd(OPT12_HgAccumulate_DTO ds)
        {
             OracleDBUtil.Insert(ds.Tables["HG_ACCU_EXCLUDE_PROD"]);
        }

        /// <summary>
        /// 修改排外條件設定
        /// </summary>
        /// <param name="ds">OPT12 DataSet</param>
        public void UpdateOne_HgAccuExcludeProd(OPT12_HgAccumulate_DTO ds)
        {
            OracleConnection objConn = null;
            OracleTransaction objTX = null;

            try
            {
                objConn = OracleDBUtil.GetConnection();
                objTX = objConn.BeginTransaction();

                OracleDBUtil.UPDDATEByUUID(objTX, ds.Tables["HG_ACCU_EXCLUDE_PROD"], "SID");

                objTX.Commit();
            }
            catch (Exception ex)
            {
                objTX.Rollback();
                throw ex;
            }
            finally
            {
                objTX.Dispose();

                if (objConn.State == ConnectionState.Open) objConn.Close();
                objConn.Dispose();
                OracleConnection.ClearAllPools();
            }
        }

        /// <summary>
        /// 刪除排外條件設定
        /// </summary>
        /// <param name="ds">OPT12 DataSet</param>
        public void Delete_HgAccuExcludeProd(OPT12_HgAccumulate_DTO ds)
        {
            OracleConnection objConn = null;
            OracleTransaction objTX = null;

            try
            {
                objConn = OracleDBUtil.GetConnection();
                objTX = objConn.BeginTransaction();

                OracleDBUtil.UPDDATEByUUID(objTX, ds.Tables["HG_ACCU_EXCLUDE_PROD"], "SID");  //將 DEL_FLAS 設為 Y

                objTX.Commit();
            }
            catch (Exception ex)
            {
                objTX.Rollback();
                throw ex;
            }
            finally
            {
                objTX.Dispose();

                if (objConn.State == ConnectionState.Open) objConn.Close();
                objConn.Dispose();
                OracleConnection.ClearAllPools();
            }
        }


    }

}