using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Advtek.Utility;
using FET.POS.Model.DTO;
using System.Data.OracleClient;
using System.Data;

namespace FET.POS.Model.Facade.FacadeImpl
{
    public class CHK06_Facade
    {
        /// <summary>
        /// 總部對帳作業Excel上傳檔(Temp)
        /// </summary>
        /// <param name="DS">CHK06 DataSet</param>
        public void AddNew_UploadTemp(CHK06_BankCash_DTO ds, string BATCH_NO)
        {
            OracleDBUtil.Insert(ds.Tables["UPLOAD_TEMP"]);
        }

        /// <summary>
        /// 總部對帳作業Excel上傳檔(Temp)
        /// </summary>
        /// <param name="BATCH_NO">上傳批號</param>
        /// <param name="TRADE_DATE_S">對帳日期區間(起)</param>
        /// <param name="TRADE_DATE_E">對帳日期區間(訖)</param>
        public void AddNew_BankCashTemp(string BATCH_NO, string TRADE_DATE_S, string TRADE_DATE_E)
        {
            OracleConnection objConn = null;
            OracleTransaction objTX = null;

            try
            {
                objConn = OracleDBUtil.GetConnection();
                objTX = objConn.BeginTransaction();

                OracleDBUtil.ExecuteSql_SP(
                   objTX
                   , "SP_INSERT_BANKCASHTEMP"
                   , new OracleParameter("v_BATCHNO", BATCH_NO)
                   , new OracleParameter("v_TRADEDATE_S", TRADE_DATE_S)
                   , new OracleParameter("v_TRADEDATE_E", TRADE_DATE_E)
                   );

                objTX.Commit();

            }
            catch (Exception ex)
            {
                objTX.Rollback();
                throw ex;
            }
            finally
            {
                objTX = null;
                if (objConn.State == ConnectionState.Open) objConn.Close();
                objConn.Dispose();
                OracleConnection.ClearAllPools();
            }
        }

        /// <summary>
        /// 總部對帳作業Excel上傳檔(Temp)
        /// </summary>
        /// <param name="BATCH_NO">上傳批號</param>
        public void AddNew_BankCash(string BATCH_NO)
        {
            OracleConnection objConn = null;
            OracleTransaction objTX = null;

            try
            {
                objConn = OracleDBUtil.GetConnection();
                objTX = objConn.BeginTransaction();

                OracleDBUtil.ExecuteSql_SP(
                   objTX
                   , "SP_INSERT_BANKCASH"
                   , new OracleParameter("v_BATCHNO", BATCH_NO)
                   );

                objTX.Commit();

            }
            catch (Exception ex)
            {
                objTX.Rollback();
                throw ex;
            }
            finally
            {
                objTX = null;
                if (objConn.State == ConnectionState.Open) objConn.Close();
                objConn.Dispose();
                OracleConnection.ClearAllPools();
            }
        }

        /// <summary>
        /// 取得查詢結果總部對帳作業(Master Data)
        /// </summary>
        /// <param name="BATCH_NO">上傳批號</param>
        /// <returns>查詢結果</returns>
        public DataTable Query_BankCashTempM(string BATCH_NO)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT BANK_CASH_M_ID, TO_CHAR(TRADE_DATE, 'YYYY/MM/DD') TRADE_DATE, BANK_CASH_IN, POS_CASH_IN, ");
            sb.Append("NCCC_CC_IN, POS_CC_IN, EXCEPTION_CAUSE ");
            sb.Append("FROM BANK_CASH_MASTER_TEMP ");
            sb.Append("WHERE 1 =1 ");

            if (!string.IsNullOrEmpty(BATCH_NO))
            {
                sb.Append(" AND UL_BATCH_NO = " + OracleDBUtil.SqlStr(BATCH_NO));
            }

            DataTable dt = OracleDBUtil.Query_Data(sb.ToString());
            return dt;
        }

        /// <summary>
        /// 取得查詢結果總部對帳作業(Detail Data)
        /// </summary>
        /// <param name="BATCH_NO">上傳批號</param>
        /// <param name="TRADE_DATE">對帳日期</param>
        /// <returns>查詢結果</returns>
        public DataTable Query_BankCashTempD(string BATCH_NO, string TRADE_DATE)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(@"SELECT M.BANK_CASH_D_ID
                            , TO_CHAR(M.TRADE_DATE, 'YYYY/MM/DD') TRADE_DATE
                            , M.STORE_NO
                            , S.STORENAME
                            , M.BANK_CASH_IN
                            , M.POS_CASH_IN
                            , M.NCCC_CC_IN
                            , M.POS_CC_IN
                            , M.EXCEPTION_CAUSE 
                            , M.FLG_TO_SAVE
                            FROM BANK_CASH_DETAIL_TEMP M, STORE S 
                            WHERE M.STORE_NO = S.STORE_NO(+) ");

            if (!string.IsNullOrEmpty(BATCH_NO))
            {
                sb.Append(" AND M.UL_BATCH_NO = " + OracleDBUtil.SqlStr(BATCH_NO));
            }

            if (!string.IsNullOrEmpty(TRADE_DATE))
            {
                sb.Append(" AND M.TRADE_DATE = " + OracleDBUtil.DateStr(TRADE_DATE));
            }

            DataTable dt = OracleDBUtil.Query_Data(sb.ToString());
            return dt;
        }

        public string GetDay()
        {
            string str = "0";

            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT PARA_VALUE ");
            sb.Append("FROM   SYS_PARA ");
            sb.Append(" WHERE PARA_KEY='SET_DAY' ");
           

            DataTable dt = OracleDBUtil.Query_Data(sb.ToString());

            foreach (DataRow dr in dt.Rows)
            {
                str = dr["PARA_VALUE"].ToString();
                if (str == "")
                {
                    str = "10";
                }
            }

            return str;
        }
        /// <summary>
        /// 取得查詢結果總部對帳作業(Master Data)
        /// </summary>
        /// <param name="BATCH_NO">上傳批號</param>
        /// <returns>查詢結果</returns>
        public DataTable Query_BankCashM(string BATCH_NO)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT BANK_CASH_M_ID, TO_CHAR(TRADE_DATE, 'YYYY/MM/DD') TRADE_DATE, BANK_CASH_IN, POS_CASH_IN, ");
            sb.Append("NCCC_CC_IN, POS_CC_IN, EXCEPTION_CAUSE ");
            sb.Append("FROM BANK_CASH_MASTER ");
            sb.Append("WHERE 1 =1 ");

            if (!string.IsNullOrEmpty(BATCH_NO))
            {
                sb.Append(" AND UL_BATCH_NO = " + OracleDBUtil.SqlStr(BATCH_NO));
            }

            DataTable dt = OracleDBUtil.Query_Data(sb.ToString());
            return dt;
        }

        /// <summary>
        /// 取得查詢結果總部對帳作業(Detail Data)
        /// </summary>
        /// <param name="BATCH_NO">上傳批號</param>
        /// <param name="TRADE_DATE">對帳日期</param>
        /// <returns>查詢結果</returns>
        public DataTable Query_BankCashD(string BATCH_NO, string TRADE_DATE)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT M.BANK_CASH_D_ID, TO_CHAR(M.TRADE_DATE, 'YYYY/MM/DD') TRADE_DATE, M.STORE_NO, S.STORENAME, ");
            sb.Append("M.BANK_CASH_IN, M.POS_CASH_IN, ");
            sb.Append("M.NCCC_CC_IN, M.POS_CC_IN, M.EXCEPTION_CAUSE ");
            sb.Append("FROM BANK_CASH_DETAIL M ");
            sb.Append("LEFT JOIN STORE S ON M.STORE_NO = S.STORE_NO ");
            sb.Append("WHERE 1 =1 ");

            if (!string.IsNullOrEmpty(BATCH_NO))
            {
                sb.Append(" AND M.UL_BATCH_NO = " + OracleDBUtil.SqlStr(BATCH_NO));
            }

            if (!string.IsNullOrEmpty(TRADE_DATE))
            {
                sb.Append(" AND M.TRADE_DATE = " + OracleDBUtil.DateStr(TRADE_DATE));
            }

            DataTable dt = OracleDBUtil.Query_Data(sb.ToString());
            return dt;
        }

    }
}
