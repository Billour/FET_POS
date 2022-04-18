using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Advtek.Utility;
using System.Data.OracleClient;

namespace FET.POS.Model.Facade.FacadeImpl
{
    public partial class RPL_Facade
    {
        #region 100.2.15宗佑新增(100.2.16第一次修正)
        /*
     Author：宗佑
     Date：100.02.15
     Description:RPL001報表SQL敘述
     100.02.16修正輸入日期會發生文字及字串不符問題
*/
        /// <summary>
        /// RPL001
        /// </summary>
        /// <param name="STORE_NO_S">門市編號(起)</param>
        /// <param name="STORE_NO_E">門市編號(訖)</param>
        /// <param name="TRADE_DATE_S">交易日期(起)</param>
        /// <param name="TRADE_DATE_E">交易日期(訖)</param>
        /// <param name="PAID_MODE">付款方式</param>
        public DataTable RPL001(string STORE_NO_S, string STORE_NO_E,
            string TRADE_DATE_S, string TRADE_DATE_E,
            string PAID_MODE)
        {

            StringBuilder sb = new StringBuilder();
            sb.Append(" SELECT to_char(t.TRADE_DATE,'YYYY/MM/DD') AS 交易日期, t.STORE_NO AS 門市編號, ");
            sb.Append(" t.STORE_NAME AS 門市名稱, t.PAID_MODE_NAME AS 付款方式, ");
            sb.Append(" t.CREDID_CARD_TYPE_NAME AS 卡別, SUM(t.SAL_AMT) AS 銷售金額, ");
            sb.Append(" t.SOURCE_TYPE AS 訂單通路 ");
            sb.Append(" FROM VW_RPL_STORE_PAIDDETAIL t ");
            sb.Append(" WHERE 1 = 1");

            #region -WHERE-
            if (!string.IsNullOrEmpty(STORE_NO_S))
            {
                sb.Append(" AND t.STORE_NO >= ");
                sb.Append(Advtek.Utility.OracleDBUtil.SqlStr(STORE_NO_S.Trim()));
            }
            if (!string.IsNullOrEmpty(STORE_NO_E))
            {
                sb.Append(" AND t.STORE_NO <= ");
                sb.Append(Advtek.Utility.OracleDBUtil.SqlStr(STORE_NO_E.Trim()));
            }
            if (!string.IsNullOrEmpty(TRADE_DATE_S))
            {
                sb.Append(" AND TRUNC(T.TRADE_DATE) >= ");
                sb.Append(Advtek.Utility.OracleDBUtil.DateStr(TRADE_DATE_S.Trim()));
            }
            if (!string.IsNullOrEmpty(TRADE_DATE_E))
            {
                sb.Append(" AND TRUNC(T.TRADE_DATE) <= ");
                sb.Append(Advtek.Utility.OracleDBUtil.DateStr(TRADE_DATE_E.Trim()));
            }
            if (!string.IsNullOrEmpty(PAID_MODE) && PAID_MODE.ToUpper() != "ALL") sb.AppendFormat(" AND t.PAID_MODE_NAME ='{0}' ", PAID_MODE);
            #endregion

            #region -GROUP BY-
            sb.AppendFormat(" GROUP BY to_char(t.TRADE_DATE,'YYYY/MM/DD') ");
            sb.AppendFormat("         ,t.STORE_NO                         ");
            sb.AppendFormat("         ,t.STORE_NAME                       ");
            sb.AppendFormat("         ,t.PAID_MODE_NAME                   ");
            sb.AppendFormat("         ,t.CREDID_CARD_TYPE_NAME            ");
            sb.AppendFormat("         ,t.SOURCE_TYPE                      ");
            #endregion

            #region -ORDER BY-
            sb.AppendFormat(" ORDER BY to_char(t.TRADE_DATE,'YYYY/MM/DD') ");
            sb.AppendFormat("         ,t.STORE_NO                         ");
            #endregion

            DataTable dt = OracleDBUtil.Query_Data(sb.ToString());
            return dt;
        }
        #region ---  蔡坤霖
        /*Author : 蔡坤霖
          Date : 2011 / 02 / 21
          Description: RPL002 門市對帳明細表
         */
        /// <summary>
        /// RPL002 門市對帳明細表
        /// </summary>
        /// <param name="STORENO_S">門市編號_起</param>
        /// <param name="STORENO_E">門市編號_訖</param>
        /// <param name="DATE_S">交易日期_起</param>
        /// <param name="DATE_E">交易日期_訖</param>
        /// <param name="PAID_MODE">付款方式(Value)</param>
        /// <returns></returns>
        public DataTable RPL002(string STORENO_S, string STORENO_E, string DATE_S, string DATE_E, string PAID_MODE)
        {
            OracleConnection objConn = null;
            DataTable dt = null;
            try
            {
                objConn = OracleDBUtil.GetConnection();

                System.Text.StringBuilder sb = new System.Text.StringBuilder();

                #region -SQL-
                sb.AppendFormat("WITH RPL002 AS ( SELECT STORE_NO 門市編號, \n");
                sb.AppendFormat("       STORENAME 門市名稱, \n");
                sb.AppendFormat("       TO_CHAR(SALE_DATE,'YYYY/MM/DD') 交易日期, \n");
                sb.AppendFormat("       SALE_AMOUNT 銷售, \n");
                sb.AppendFormat("       PAYMENT_AMOUNT 代收, \n");
                sb.AppendFormat("       NVL(SALE_AMOUNT, 0) + NVL(PAYMENT_AMOUNT, 0) 收款總計, \n");
                sb.AppendFormat("       POS_TOTAL_AMOUNT POS加值金額, \n");
                sb.AppendFormat("       CASH_AMOUNT 銀行應入帳金額, \n");
                sb.AppendFormat("       CARD_TOTAL_AMOUNT 卡機加值金額, \n");
                sb.AppendFormat("       '' 銀行存入日期, \n");
                sb.AppendFormat("       BANK_CASH_IN 銀行入帳金額, \n");
                sb.AppendFormat("       CREDIT_CARD_FEE 信用卡手續費, \n");
                sb.AppendFormat("       NVL(CASH_AMOUNT, 0) - (NVL(SALE_AMOUNT, 0) + NVL(PAYMENT_AMOUNT, 0)) 當日差額, \n");
                sb.AppendFormat("       0 差異調整金額, \n");
                sb.AppendFormat("       0 累計差額, \n");
                sb.AppendFormat("       NVL(POS_TOTAL_AMOUNT, 0) + NVL(CARD_TOTAL_AMOUNT, 0) 加值差額, \n");
                sb.AppendFormat("       0 加值差異調整, \n");
                sb.AppendFormat("       0 加值累計差額 \n");
                sb.AppendFormat(" \n");
                sb.AppendFormat("  FROM VW_RPL002 T \n");
                sb.AppendFormat(" WHERE 1 = 1 \n");

                #region -WHERE-
                #region 交易日期
                if (!string.IsNullOrEmpty(DATE_S))
                {
                    sb.Append(" AND T.SALE_DATE >= ");
                    sb.Append(Advtek.Utility.OracleDBUtil.DateStr(DATE_S.Trim()));
                }
                if (!string.IsNullOrEmpty(DATE_E))
                {
                    sb.Append(" AND T.SALE_DATE <= ");
                    sb.Append(Advtek.Utility.OracleDBUtil.DateStr(DATE_E.Trim()));
                }
                #endregion
                #region 門市編號
                if (!string.IsNullOrEmpty(STORENO_S))
                {
                    sb.Append(" AND T.STORE_NO >= ");
                    sb.Append(Advtek.Utility.OracleDBUtil.SqlStr(STORENO_S.Trim()));
                }
                if (!string.IsNullOrEmpty(STORENO_E))
                {
                    sb.Append(" AND T.STORE_NO <= ");
                    sb.Append(Advtek.Utility.OracleDBUtil.SqlStr(STORENO_E.Trim()));
                }
                #endregion
                #region 付款方式
                if (!string.IsNullOrEmpty(PAID_MODE) && PAID_MODE.ToUpper() != "ALL")
                {
                    sb.Append(" AND T.PAID_MODE = ");
                    sb.Append(Advtek.Utility.OracleDBUtil.SqlStr(PAID_MODE.Trim()));
                }
                #endregion
                #endregion

                sb.AppendFormat(" ) \n");
                sb.AppendFormat("  \n");
                sb.AppendFormat("SELECT * FROM RPL002 \n");
                sb.AppendFormat(" \n");
                sb.AppendFormat("UNION ALL \n");
                sb.AppendFormat(" \n");
                sb.AppendFormat("SELECT '總計' 門市編號, \n");
                sb.AppendFormat("       '' 門市名稱, \n");
                sb.AppendFormat("       '' 交易日期, \n");
                sb.AppendFormat("       SUM(銷售) 銷售, \n");
                sb.AppendFormat("       SUM(代收) 代收, \n");
                sb.AppendFormat("       SUM(收款總計) 收款總計, \n");
                sb.AppendFormat("       SUM(POS加值金額) POS加值金額, \n");
                sb.AppendFormat("       SUM(銀行應入帳金額) 銀行應入帳金額, \n");
                sb.AppendFormat("       SUM(卡機加值金額) 卡機加值金額, \n");
                sb.AppendFormat("       '' 銀行存入日期, \n");
                sb.AppendFormat("       SUM(銀行入帳金額) 銀行入帳金額, \n");
                sb.AppendFormat("       SUM(信用卡手續費) 信用卡手續費, \n");
                sb.AppendFormat("       SUM(當日差額) 當日差額, \n");
                sb.AppendFormat("       SUM(差異調整金額), \n");
                sb.AppendFormat("       0 累計差額, \n");
                sb.AppendFormat("       SUM(加值差額) 加值差額, \n");
                sb.AppendFormat("       SUM(加值差異調整) 加值差異調整, \n");
                sb.AppendFormat("       0 加值累計差額 \n");
                sb.AppendFormat("  FROM RPL002 \n");
                #endregion

                dt = Advtek.Utility.OracleDBUtil.GetDataSet(objConn, sb.ToString()).Tables[0];

                //Int64 RowO = 0, RowR = 0;
                //for (int i = 0; i < dt.Rows.Count; i++)
                //{
                //    if (i != 0 && dt.Rows[i]["門市編號"].ToString() != "總計")
                //    {
                //        dt.Rows[i]["累計差額"] = RowO + Convert.ToInt64(dt.Rows[i]["當日差額"]) + Convert.ToInt64(dt.Rows[i]["差異調整金額"]);
                //        dt.Rows[i]["加值累計差額"] = RowR + Convert.ToInt64(dt.Rows[i]["加值差額"]);
                //    }

                //    RowO = Convert.ToInt64("0" + dt.Rows[i]["累計差額"].ToString());
                //    RowR = Convert.ToInt64("0" + dt.Rows[i]["加值累計差額"].ToString());
                //}
                return dt;
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
        }
        #endregion
        #region ---  蔡坤霖
        /*Author : 蔡坤霖
          Date : 2011 / 02 / 21
          Description: RPL003 門市對帳總表
         */
        /// <summary>
        /// RPL003 門市對帳總表
        /// </summary>
        /// <param name="DATE_S">交易日期_起</param>
        /// <param name="DATE_E">交易日期_訖</param>
        /// <param name="PAID_MODE">付款方式(Value)</param>
        /// <returns></returns>
        public DataTable RPL003(string DATE_S, string DATE_E, string PAID_MODE)
        {
            OracleConnection objConn = null;
            DataTable dt = null;
            try
            {
                objConn = OracleDBUtil.GetConnection();

                System.Text.StringBuilder sb = new System.Text.StringBuilder();

                #region -SQL-
                sb.AppendFormat("WITH RPL002 AS ( SELECT   \n");
                sb.AppendFormat("       TO_CHAR(SALE_DATE,'YYYY/MM/DD') 交易日期, \n");
                sb.AppendFormat("       SUM(SALE_AMOUNT) 銷售, \n");
                sb.AppendFormat("       SUM(PAYMENT_AMOUNT) 代收, \n");
                sb.AppendFormat("       SUM(NVL(SALE_AMOUNT, 0) + NVL(PAYMENT_AMOUNT, 0)) 收款總計, \n");
                sb.AppendFormat("       SUM(POS_TOTAL_AMOUNT) POS加值金額, \n");
                sb.AppendFormat("       SUM(CASH_AMOUNT) 銀行應入帳金額, \n");
                sb.AppendFormat("       SUM(CARD_TOTAL_AMOUNT) 卡機加值金額, \n");
                sb.AppendFormat("       '' 銀行存入日期, \n");
                sb.AppendFormat("       SUM(BANK_CASH_IN) 銀行入帳金額, \n");
                sb.AppendFormat("       SUM(CREDIT_CARD_FEE) 信用卡手續費, \n");
                sb.AppendFormat("       SUM(NVL(CASH_AMOUNT, 0) - (NVL(SALE_AMOUNT, 0) + NVL(PAYMENT_AMOUNT, 0))) 當日差額, \n");
                sb.AppendFormat("       0 差異調整金額, \n");
                sb.AppendFormat("       0 累計差額, \n");
                sb.AppendFormat("       SUM(NVL(POS_TOTAL_AMOUNT, 0) + NVL(CARD_TOTAL_AMOUNT, 0)) 加值差額, \n");
                sb.AppendFormat("       0 加值差異調整, \n");
                sb.AppendFormat("       0 加值累計差額 \n");
                sb.AppendFormat(" \n");
                sb.AppendFormat("  FROM VW_RPL002 T \n");
                sb.AppendFormat(" WHERE 1 = 1 \n");

                #region -WHERE-
                #region 交易日期
                if (!string.IsNullOrEmpty(DATE_S))
                {
                    sb.Append(" AND T.SALE_DATE >= ");
                    sb.Append(Advtek.Utility.OracleDBUtil.DateStr(DATE_S.Trim()));
                }
                if (!string.IsNullOrEmpty(DATE_E))
                {
                    sb.Append(" AND T.SALE_DATE <= ");
                    sb.Append(Advtek.Utility.OracleDBUtil.DateStr(DATE_E.Trim()));
                }
                #endregion

                #region 付款方式
                if (!string.IsNullOrEmpty(PAID_MODE) && PAID_MODE.ToUpper() != "ALL")
                {
                    sb.Append(" AND T.PAID_MODE = ");
                    sb.Append(Advtek.Utility.OracleDBUtil.SqlStr(PAID_MODE.Trim()));
                }
                #endregion
                #endregion

                sb.AppendFormat(" GROUP BY TO_CHAR(SALE_DATE,'YYYY/MM/DD')        \n");

                sb.AppendFormat(" ) \n");
                sb.AppendFormat("  \n");
                sb.AppendFormat("SELECT * FROM RPL002 \n");
                sb.AppendFormat(" \n");
                sb.AppendFormat("UNION ALL \n");
                sb.AppendFormat(" \n");
                sb.AppendFormat("SELECT  \n");
                sb.AppendFormat("        \n");
                sb.AppendFormat("       '總計' 交易日期, \n");
                sb.AppendFormat("       SUM(銷售) 銷售, \n");
                sb.AppendFormat("       SUM(代收) 代收, \n");
                sb.AppendFormat("       SUM(收款總計) 收款總計, \n");
                sb.AppendFormat("       SUM(POS加值金額) POS加值金額, \n");
                sb.AppendFormat("       SUM(銀行應入帳金額) 銀行應入帳金額, \n");
                sb.AppendFormat("       SUM(卡機加值金額) 卡機加值金額, \n");
                sb.AppendFormat("       '' 銀行存入日期, \n");
                sb.AppendFormat("       SUM(銀行入帳金額) 銀行入帳金額, \n");
                sb.AppendFormat("       SUM(信用卡手續費) 信用卡手續費, \n");
                sb.AppendFormat("       SUM(當日差額) 當日差額, \n");
                sb.AppendFormat("       SUM(差異調整金額), \n");
                sb.AppendFormat("       0 累計差額, \n");
                sb.AppendFormat("       SUM(加值差額) 加值差額, \n");
                sb.AppendFormat("       SUM(加值差異調整) 加值差異調整, \n");
                sb.AppendFormat("       0 加值累計差額 \n");
                sb.AppendFormat("  FROM RPL002 \n");
                #endregion

                dt = Advtek.Utility.OracleDBUtil.GetDataSet(objConn, sb.ToString()).Tables[0];

                //Int64 RowO = 0, RowR = 0;
                //for (int i = 0; i < dt.Rows.Count; i++)
                //{
                //    if (i != 0 && dt.Rows[i]["交易日期"].ToString() != "總計")
                //    {
                //        dt.Rows[i]["累計差額"] = RowO + Convert.ToInt64(dt.Rows[i]["當日差額"]) + Convert.ToInt64(dt.Rows[i]["差異調整金額"]);
                //        dt.Rows[i]["加值累計差額"] = RowR + Convert.ToInt64(dt.Rows[i]["加值差額"]);
                //    }

                //    RowO = Convert.ToInt64("0" + dt.Rows[i]["累計差額"].ToString());
                //    RowR = Convert.ToInt64("0" + dt.Rows[i]["加值累計差額"].ToString());
                //}
                return dt;
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
        }
        #endregion
        #region ---  蔡坤霖
        /*Author : 蔡坤霖
          Date : 2011 / 02 / 21
          Description: RPL004 門市差異調整報表
         */
        public DataTable RPL004(string DATE_S, string DATE_E, string STORENO_S, string STORENO_E, string PAYTYPE)
        {
            // 尚未提供VIEW 暫時使用自創測試資料
            DataTable dt = new DataTable();
            dt.Columns.Add("調整日期", typeof(string));
            dt.Columns.Add("門市編號", typeof(string));
            dt.Columns.Add("門市名稱", typeof(string));
            dt.Columns.Add("付款方式", typeof(string));
            dt.Columns.Add("調整原因", typeof(string));
            dt.Columns.Add("調整金額", typeof(double));
            dt.Columns.Add("會計科目", typeof(string));
            dt.Columns.Add("備註說明", typeof(string));

            DataRow dr = dt.NewRow();
            dr[0] = "2011/02/21";
            dr[1] = "123456";
            dr[2] = "測試1";
            dr[3] = "信用卡";
            dr[4] = "無";
            dr[5] = "111";
            dr[6] = "00112233";
            dr[7] = "尚未提供VIEW 暫時使用自創測試資料";
            dt.Rows.Add(dr);
            return dt;
        }
        #endregion---  蔡坤霖
        #endregion
        /// <summary>
        /// RPL024 調整明細表 
        /// </summary>
        /// <param name="STORE_NO_S">調整門市(起)</param>
        /// <param name="STORE_NO_E">調整門市(訖)</param>
        /// <param name="ADJDATE_S">調整日期(起)</param>
        /// <param name="ADJDATE_E">調整日期(訖)</param>
        /// <param name="StockID">調整倉別ID</param>
        /// <param name="ReasonID">調整原因ID</param>
        /// <param name="PRODNO_S">商品編號(起)</param>
        /// <param name="PRODNO_E">商品編號(訖)</param>
        public DataTable RPL024(string STORE_NO_S, string STORE_NO_E,
            string ADJDATE_S, string ADJDATE_E, string StockID,
            string ReasonID, string PRODNO_S, string PRODNO_E)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(" SELECT STORENAME AS 門市名稱, ");
            sb.Append(" to_char(ADJDATE,'YYYY/MM/DD') AS 調整日期, ADJNO AS 調整單號, ");
            sb.Append(" PRODNO AS 商品編號, PRODNAME AS 商品名稱, ");
            sb.Append(" STOCK_NAME AS 調整倉別, ADJQTY AS 調整數量, ");
            sb.Append(" STOCKADJ_DESCRIPTION AS 調整原因, ACCOUNTCODE AS 會計科目 ");
            sb.Append(" FROM VW_RPL_STOCKADJD");
            sb.Append(" WHERE 1 = 1");

            #region -門市編號-
            if (!string.IsNullOrEmpty(STORE_NO_S))
            {
                sb.Append(" AND STORE_NO >= " + OracleDBUtil.SqlStr(STORE_NO_S.Trim()));
            }
            if (!string.IsNullOrEmpty(STORE_NO_E))
            {
                sb.Append(" AND STORE_NO <= " + OracleDBUtil.SqlStr(STORE_NO_E.Trim()));
            }
            #endregion
            #region -調整日期-
            if (!string.IsNullOrEmpty(ADJDATE_S))
            {
                sb.Append(" AND TRUNC(ADJDATE) >= " + OracleDBUtil.DateStr(ADJDATE_S.Trim()));
            }
            if (!string.IsNullOrEmpty(ADJDATE_E))
            {
                sb.Append(" AND TRUNC(ADJDATE) <= " + OracleDBUtil.DateStr(ADJDATE_E.Trim()));
            }
            #endregion
            #region -調整倉別-
            if (!string.IsNullOrEmpty(StockID))
            {
                sb.Append(" AND LOC_ID = " + OracleDBUtil.SqlStr(StockID.Trim()));
            }
            #endregion
            #region -調整原因-
            if (ReasonID != "0")
            {
                sb.Append(" AND STOCKADJ_REASON_CODE = " + OracleDBUtil.SqlStr(ReasonID.Trim()));
            }
            #endregion
            #region -商品料號-
            if (!string.IsNullOrEmpty(PRODNO_S))
            {
                sb.Append(" AND PRODNO >= " + OracleDBUtil.SqlStr(PRODNO_S.Trim()));
            }
            if (!string.IsNullOrEmpty(PRODNO_E))
            {
                sb.Append(" AND PRODNO <= " + OracleDBUtil.SqlStr(PRODNO_E.Trim()));
            }
            #endregion

            DataTable dt = OracleDBUtil.Query_Data(sb.ToString());
            return dt;
        }

        /// <summary>
        /// RPL026
        /// </summary>
        /// <param name="tradeType">交易類別</param>
        /// <param name="store_s">門市編號(起)</param>
        /// <param name="store_e">門市編號(訖)</param>
        /// <param name="tradeDate_s">交易日期(起)</param>
        /// <param name="tradeDate_e">交易日期(訖)</param>
        /// <param name="storeOut_s">調出門市(起)</param>
        /// <param name="storeOut_e">調出門市(訖)</param>
        /// <param name="storeIn_s">調入門市(起)</param>
        /// <param name="storeIn_e">調入門市(訖)</param>
        /// <param name="prodNo_s">商品料號(起)</param>
        /// <param name="prodNo_e">商品料號(訖)</param>
        /// <returns></returns>
        public DataTable RPL026(string tradeType,
            string store_s, string store_e,
            string tradeDate_s, string tradeDate_e,
            string storeOut_s, string storeOut_e,
            string storeIn_s, string storeIn_e,
            string prodNo_s, string prodNo_e)
        {
            StringBuilder sbSql = new StringBuilder();
            StringBuilder sbWhere = new StringBuilder();
            string selectSql = @"SELECT * FROM VW_RPL_STORETRANSFER_D_NOTYET";
            string store_no = "";
            string storeIn = "";
            string storeOut = "";

            DataTable dt = new DataTable();
            sbWhere.Append(" Where 1=1");

            if (tradeDate_s.Length != 0 && tradeDate_e.Length != 0)
                sbWhere.AppendFormat(" AND trunc(STDATE) BETWEEN TO_DATE({0}, 'YYYY/MM/DD') AND TO_DATE({1}, 'YYYY/MM/DD')",
                    OracleDBUtil.SqlStr(tradeDate_s), OracleDBUtil.SqlStr(tradeDate_e));

            if (prodNo_s.Length != 0 && prodNo_e.Length != 0)
                sbWhere.AppendFormat(" AND PRODNO BETWEEN {0} AND {1}", OracleDBUtil.SqlStr(prodNo_s), OracleDBUtil.SqlStr(prodNo_e));

            if (storeOut_s.Length != 0 && storeOut_e.Length != 0)
                storeOut = string.Format(" AND FROM_STORE_NO BETWEEN {0} AND {1}", OracleDBUtil.SqlStr(storeOut_s), OracleDBUtil.SqlStr(storeOut_e));

            if (storeIn_s.Length != 0 && storeIn_e.Length != 0)
                storeIn = string.Format(" AND TO_STORE_NO BETWEEN {0} AND {1}", OracleDBUtil.SqlStr(storeIn_s), OracleDBUtil.SqlStr(storeIn_e));

            if (store_s.Length != 0 && store_e.Length != 0)
                store_no = string.Format(" AND TO_STORE_NO BETWEEN {0} AND {1}", OracleDBUtil.SqlStr(store_s), OracleDBUtil.SqlStr(store_e));

            sbSql.Append(selectSql);
            sbSql.Append(sbWhere.ToString());
            switch (tradeType)
            {
                case "進貨":
                    sbSql.Append(" AND SALE_TYPE = '進貨'");
                    sbSql.Append(store_no);
                    break;
                case "調撥":
                    sbSql.Append(" AND SALE_TYPE = '調撥'");
                    sbSql.Append(storeOut);
                    sbSql.Append(storeIn);
                    break;
                default:
                    //sbSql.Append(" AND SALE_TYPE = '進貨'");
                    sbSql.Append(store_no);
                    //sbSql.Append(" UNION ");
                    //sbSql.Append(selectSql);
                    //sbSql.Append(sbWhere.ToString());
                    //sbSql.Append(" AND SALE_TYPE = '調撥'");
                    sbSql.Append(storeOut);
                    sbSql.Append(storeIn);
                    break;
            }

            dt = OracleDBUtil.Query_Data(sbSql.ToString());
            return dt;
        }

        /// <summary>
        /// RPL027總部庫存進銷存明細表
        /// </summary>
        /// <param name="STORE_NO">商店編號</param>
        /// <param name="INV_TRAN_DTM_S">交易起始日期</param>
        /// <param name="INV_TRAN_DTM_E">交易結束日期</param>
        /// <param name="INV_TRAN_TYPE">交易類別</param>
        /// <param name="PRODNO_S">商品起始料號</param>
        /// <param name="PRODNO_E">商品結束料號</param>
        /// <returns></returns>
        public DataTable RPL027(string STORE_NO, string INV_TRAN_DTM_S, string INV_TRAN_DTM_E, string INV_TRAN_TYPE, string PRODNO_S, string PRODNO_E)
        {
            //,'CG','銷售換貨-入庫','AI','總部庫存調整-加庫存'
            //,'TO','庫存移倉-移出','TI','庫存移倉-移入','UO','部門領用'

            StringBuilder sb = new StringBuilder();

            sb.Append(" select STORE_NO || STORENAME as 門市名稱,");
            sb.Append(" PRODNO as 商品料號,");
            sb.Append(" PRODNAME as 商品名稱,");
            sb.Append(" to_char(INV_TRAN_DTM,'YYYY/MM/DD') as 日期,");
            sb.Append(@" decode(INV_TRAN_TYPE,'CI','進貨驗收加庫存','SA','門市銷售','SR','銷售退貨'
                                ,'CG','銷售退貨','AI','總部庫存調整-加庫存'
                                ,'TO','移倉','TI','移倉','UO','部門領用'
                                ,'MO','門市盤點異動量(盤損)','MI','門市盤點異動量(盤盈)','AO','總部庫存調整-扣庫存'
                                ,'SI','門市撥入','SO','門市移出','RO','門市退倉傳送'
                                ,'SC','門市訂貨出貨確認','RI','ERP驗退'
                                ) as 交易型態,");
            sb.Append(" INV_QTY as 數量,");
            sb.Append(" SHEET_NO as 交易序號 ");
            sb.Append(" FROM VW_RPL_INV_TRAN_LOG ");
            sb.Append(" WHERE 1=1 ");

            if (!string.IsNullOrEmpty(STORE_NO.Trim()))
            {
                sb.AppendFormat("AND STORE_NO  = {0}", OracleDBUtil.SqlStr(STORE_NO.Trim()));
            }

            if (!string.IsNullOrEmpty(INV_TRAN_DTM_S.Trim()))
            {
                sb.Append(" AND TRUNC(INV_TRAN_DTM) >= " + OracleDBUtil.DateStr(INV_TRAN_DTM_S.Trim()));
            }

            if (!string.IsNullOrEmpty(INV_TRAN_DTM_E.Trim()))
            {
                sb.Append(" AND TRUNC(INV_TRAN_DTM) <= " + OracleDBUtil.DateStr(INV_TRAN_DTM_E.Trim()));
            }

            if (!string.IsNullOrEmpty(INV_TRAN_TYPE.Trim()))
            {
                if (!INV_TRAN_TYPE.Equals("AI") && !INV_TRAN_TYPE.Equals("SR") && !INV_TRAN_TYPE.Equals("TO"))
                {
                    sb.Append(" AND INV_TRAN_TYPE = " + OracleDBUtil.SqlStr(INV_TRAN_TYPE.Trim()));
                }
                else if (INV_TRAN_TYPE.Equals("AI"))
                {
                    sb.Append("AND (INV_TRAN_TYPE = 'AI' or INV_TRAN_TYPE = 'AO') ");
                }
                else if (INV_TRAN_TYPE.Equals("SR"))
                {
                    sb.Append("AND (INV_TRAN_TYPE = 'SR' or INV_TRAN_TYPE = 'CG') ");
                }
                else if (INV_TRAN_TYPE.Equals("TO"))
                {
                    sb.Append("AND (INV_TRAN_TYPE = 'TO' or INV_TRAN_TYPE = 'TI') ");
                }
            }

            if (!string.IsNullOrEmpty(PRODNO_S.Trim()))
            {
                sb.Append(" AND PRODNO >= " + OracleDBUtil.SqlStr(PRODNO_S.Trim()));
            }

            if (!string.IsNullOrEmpty(PRODNO_E.Trim()))
            {
                sb.Append(" AND PRODNO <= " + OracleDBUtil.SqlStr(PRODNO_E.Trim()));
            }

            sb.Append(" ORDER BY 商品料號,門市名稱,日期,交易型態");

            DataTable dt = OracleDBUtil.Query_Data(sb.ToString());
            return dt;
        }

        /*Author : 蔡坤霖
          Date : 2011 / 02 / 21
          Description: RPL010 門市結帳分錄明細表
         */
        /// <summary>
        /// RPL010 門市結帳分錄明細表
        /// </summary>
        /// <param name="STORE_NO_S">門市編號_起</param>
        /// <param name="STORE_NO_E">門市編號_訖</param>
        /// <param name="TOTAL_AMT">轉入總帳</param>
        /// <param name="TRADE_DATE_S">交易日期_起</param>
        /// <param name="TRADE_DATE_E">交易日期_訖</param>
        /// <returns></returns>
        public DataTable RPL010(string STORE_NO_S, string STORE_NO_E, string CC2ERP_FLAG, string TRADE_DATE_S, string TRADE_DATE_E)
        {
            OracleConnection objConn = null;

            try
            {
                objConn = OracleDBUtil.GetConnection();
                System.Text.StringBuilder sb = new System.Text.StringBuilder();

                #region SELECT
                sb.AppendFormat("SELECT TO_CHAR(TRADE_DATE,'YYYY/MM/DD') AS 交易日期   \n");
                sb.AppendFormat("      ,STORE_NO    AS 門市編號                        \n");
                sb.AppendFormat("      ,MACHINE_ID  AS 機台                            \n");
                sb.AppendFormat("      ,SALE_NO     AS 交易序號                        \n");
                sb.AppendFormat("      ,SALE_TYPE   AS 交易類型                        \n");
                sb.AppendFormat("      ,SALE_STATUS AS 交易類別                        \n");
                sb.AppendFormat("      ,ACC_TYPE    AS 科目類型                        \n");
                sb.AppendFormat("      ,DRCR        AS 借貸方                          \n");
                sb.AppendFormat("      ,A1          AS 科目1                           \n");
                sb.AppendFormat("      ,B2          AS 科目2                           \n");
                sb.AppendFormat("      ,C3          AS 科目3                           \n");
                sb.AppendFormat("      ,D4          AS 科目4                           \n");
                sb.AppendFormat("      ,E5          AS 科目5                           \n");
                sb.AppendFormat("      ,F6          AS 科目6                           \n");
                sb.AppendFormat("      ,(AMT)       AS 金額                            \n");
                sb.AppendFormat("      ,CC2ERP_FLAG_NAME AS 轉入總帳                   \n");
                sb.AppendFormat("  FROM VW_RPL_STORE_CLOSE_D_REPORT T                  \n");
                sb.AppendFormat(" WHERE 1 = 1                                          \n");
                #endregion

                #region -WHERE-
                #region 門市編號
                if (!string.IsNullOrEmpty(STORE_NO_S))
                {
                    sb.Append(" AND STORE_NO >= ");
                    sb.Append(Advtek.Utility.OracleDBUtil.SqlStr(STORE_NO_S.Trim()));
                }
                if (!string.IsNullOrEmpty(STORE_NO_E))
                {
                    sb.Append(" AND STORE_NO <= ");
                    sb.Append(Advtek.Utility.OracleDBUtil.SqlStr(STORE_NO_E.Trim()));
                }
                #endregion
                #region 轉入總帳
                if (!string.IsNullOrEmpty(CC2ERP_FLAG) && CC2ERP_FLAG == "Y")
                {
                    sb.Append(" AND CC2ERP_FLAG = 'Y' ");
                    //sb.Append(Advtek.Utility.OracleDBUtil.SqlStr(CC2ERP_FLAG.Trim()));
                }
                else
                {
                    sb.Append(" AND CC2ERP_FLAG = 'N' ");
                }
                #endregion
                #region 交易日期
                if (!string.IsNullOrEmpty(TRADE_DATE_S))
                {
                    sb.Append(" AND TRUNC(TRADE_DATE) >= ");
                    sb.Append(Advtek.Utility.OracleDBUtil.DateStr(TRADE_DATE_S.Trim()));
                }
                if (!string.IsNullOrEmpty(TRADE_DATE_E))
                {
                    sb.Append(" AND TRUNC(TRADE_DATE) <= ");
                    sb.Append(Advtek.Utility.OracleDBUtil.DateStr(TRADE_DATE_E.Trim()));
                }
                #endregion
                #endregion

                #region -GROUP BY-
                //sb.AppendFormat(" GROUP BY TO_CHAR(TRADE_DATE,'YYYY/MM/DD') ");
                //sb.AppendFormat("         ,STORE_NO                         ");
                //sb.AppendFormat("         ,MACHINE_ID                       ");
                //sb.AppendFormat("         ,SALE_NO                          ");
                //sb.AppendFormat("         ,SALE_TYPE                        ");
                //sb.AppendFormat("         ,SALE_STATUS                      ");
                //sb.AppendFormat("         ,ACC_TYPE                         ");
                //sb.AppendFormat("         ,DRCR                             ");
                //sb.AppendFormat("         ,A1                               ");
                //sb.AppendFormat("         ,B2                               ");
                //sb.AppendFormat("         ,C3                               ");
                //sb.AppendFormat("         ,D4                               ");
                //sb.AppendFormat("         ,E5                               ");
                //sb.AppendFormat("         ,F6                               ");
                //sb.AppendFormat("         ,CC2ERP_FLAG_NAME                 ");
                #endregion

                #region -ORDER BY- 排序item_no是為了讓"稅額(貸)"-312202出現在每一交易序號的最後一筆。
                sb.AppendFormat(" ORDER BY TO_CHAR(TRADE_DATE,'YYYY/MM/DD') ,STORE_NO ,SALE_NO ,DRCR ,ACC_TYPE ,ITEM_NO ");
                #endregion

                DataTable dt = Advtek.Utility.OracleDBUtil.GetDataSet(objConn, sb.ToString()).Tables[0];

                return dt;
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
        }

        /*Author : 蔡坤霖
          Date : 2011 / 02 / 23
          Description: RPL028 門市結帳分錄明細表
         */
        public DataTable RPL028(string COMPANY, string TRADE_TYPE, string STORE_NO_S, string STORE_NO_E,
                                string TRADE_DATE_S, string TRADE_DATE_E, string BILL_S, string BILL_E,
                                string BILLNO, string MOBILENO)
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendFormat("SELECT T.STORE_NO \"門市編號\", \n");
            sb.AppendFormat("       T.STORE_NAME \"門市名稱\", \n");
            sb.AppendFormat("       TO_CHAR(T.TRADE_DATE, 'YYYY/MM/DD') \"交易日期\", \n");
            sb.AppendFormat("       T.MACHINE_ID \"機台\", \n");
            sb.AppendFormat("       T.BILL_TYPE_NAME \"交易型態\", \n");
            sb.AppendFormat("       (SELECT COUNT(*) FROM SALE_HEAD S WHERE S.STORE_NO = T.STORE_NO AND TRUNC(S.TRADE_DATE) = TRUNC(T.TRADE_DATE) AND S.MACHINE_ID = T.MACHINE_ID) \"交易筆數\", \n");
            sb.AppendFormat("       T.PAID_MODE_NAME \"現金/信用卡\", \n");
            sb.AppendFormat("       T.信用卡筆數 \"信用卡筆數\", \n");
            sb.AppendFormat("       T.遠傳現金 \"遠傳現金\", \n");
            sb.AppendFormat("       T.和信現金 \"和信現金\", \n");
            sb.AppendFormat("       T.速博現金 \"速博現金\", \n");
            sb.AppendFormat("       T.遠通有單現金 \"遠通有單現金\", \n");
            sb.AppendFormat("       T.遠通無單現金 \"遠通無單現金\", \n");
            sb.AppendFormat("       T.\"SeedNet帳單現金\" \"SeedNet帳單現金\", \n");
            sb.AppendFormat("       T.遠傳帳單折抵 \"遠傳帳單折抵\", \n");
            sb.AppendFormat("       T.和信帳單折抵 \"和信帳單折抵\", \n");
            sb.AppendFormat("       T.快樂購總兌點數 \"快樂購總兌點數\", \n");
            sb.AppendFormat("       T.遠傳快樂購兌點數 \"遠傳快樂購兌點數\", \n");
            sb.AppendFormat("       T.和信快樂購兌點數 \"和信快樂購兌點數\", \n");
            sb.AppendFormat("       T.遠傳帳單快樂購金 \"遠傳帳單快樂購金\", \n");
            sb.AppendFormat("       T.和信帳單快樂購金 \"和信帳單快樂購金\", \n");
            sb.AppendFormat("       T.信用卡別 \"信用卡別\", \n");
            sb.AppendFormat("       T.遠傳信用卡 \"遠傳信用卡\", \n");
            sb.AppendFormat("       T.和信信用卡 \"和信信用卡\", \n");
            sb.AppendFormat("       T.速博信用卡 \"速博信用卡\" \n");
            sb.AppendFormat("  FROM VW_RPL_PIVOT_RECV_SUM T \n");
            sb.AppendFormat(" WHERE 1 = 1 \n");

            #region WHERE
            if (!string.IsNullOrEmpty(COMPANY.Trim()) && COMPANY.Trim() != "ALL")
            {
                if (COMPANY != "3")
                {
                    sb.AppendFormat("AND T.CMPNY_BILL_CODE  = " + OracleDBUtil.SqlStr(COMPANY.Trim()));
                }
                else
                {
                    sb.AppendFormat("AND (T.CMPNY_BILL_CODE  = '3' OR T.CMPNY_BILL_CODE = '4') ");
                }
            }

            if (!string.IsNullOrEmpty(TRADE_TYPE.Trim()) && TRADE_TYPE.Trim() != "ALL")
            {
                sb.AppendFormat(" AND T.BILL_TYPE = " + OracleDBUtil.SqlStr(TRADE_TYPE.Trim()));
            }

            if (!string.IsNullOrEmpty(STORE_NO_S.Trim()))
            {
                sb.AppendFormat(" AND T.STORE_NO >= " + OracleDBUtil.SqlStr(STORE_NO_S.Trim()));
            }

            if (!string.IsNullOrEmpty(STORE_NO_E.Trim()))
            {
                sb.AppendFormat(" AND T.STORE_NO <= " + OracleDBUtil.SqlStr(STORE_NO_E.Trim()));
            }

            if (!string.IsNullOrEmpty(TRADE_DATE_S.Trim()))
            {
                sb.AppendFormat(" AND TRUNC(T.TRADE_DATE) >= " + OracleDBUtil.DateStr(TRADE_DATE_S.Trim()));
            }

            if (!string.IsNullOrEmpty(TRADE_DATE_E.Trim()))
            {
                sb.AppendFormat(" AND TRUNC(T.TRADE_DATE) <= " + OracleDBUtil.DateStr(TRADE_DATE_E.Trim()));
            }
            #region 帳單金額

            if (!string.IsNullOrEmpty(BILL_S.Trim()) || !string.IsNullOrEmpty(BILL_S.Trim()))
            {
                String strAnd = null;
                if (!string.IsNullOrEmpty(BILL_S.Trim()) || !string.IsNullOrEmpty(BILL_S.Trim())) strAnd = " AND ";
                String[] fields = new String[] { "遠傳現金", "和信現金", "速博現金", "遠通有單現金", "遠通無單現金", 
                                                 "\"SeedNet帳單現金\"", "遠傳帳單折抵", "和信帳單折抵", "快樂購總兌點數", "遠傳快樂購兌點數", 
                                                 "和信快樂購兌點數", "遠傳帳單快樂購金", "和信帳單快樂購金", "遠傳信用卡", "和信信用卡", 
                                                 "速博信用卡" };

                sb.AppendFormat(" AND ( \n");

                foreach (String str in fields)
                {
                    sb.AppendFormat("(");
                    if (!string.IsNullOrEmpty(BILL_S.Trim())) sb.AppendFormat(" " + str + " >= " + OracleDBUtil.NumberStr(BILL_S.Trim()) + " \n");
                    sb.AppendFormat(strAnd);
                    if (!string.IsNullOrEmpty(BILL_E.Trim())) sb.AppendFormat(" " + str + " <= " + OracleDBUtil.NumberStr(BILL_E.Trim()) + " \n");
                    sb.AppendFormat(") OR ");
                }

                sb.AppendFormat(" 1 = 0 ) \n");
            }

            #endregion

            if (!string.IsNullOrEmpty(MOBILENO.Trim()))
            {
                sb.AppendFormat(" AND T.MSISDN = " + OracleDBUtil.SqlStr(MOBILENO.Trim()));
            }

            if (!string.IsNullOrEmpty(BILLNO.Trim()))
            {
                sb.AppendFormat(" AND T.SALE_NO = " + OracleDBUtil.SqlStr(BILLNO.Trim()));
            }
            #endregion

            sb.AppendFormat(" ORDER BY T.STORE_NO \n");

            return OracleDBUtil.Query_Data(sb.ToString());
        }

        /*Author : 蔡坤霖
          Date : 2011 / 02 / 23
          Description: RPL028 門市結帳分錄明細表
         */
        public DataTable RPL028_TOTAL(string COMPANY, string TRADE_TYPE, string STORE_NO_S, string STORE_NO_E,
                                string TRADE_DATE_S, string TRADE_DATE_E, string BILL_S, string BILL_E,
                                string BILLNO, string MOBILENO)
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendFormat("WITH RPL028 AS ( \n");
            sb.AppendFormat("SELECT '1' ORDER_INDEX, \n");
            sb.AppendFormat("       T.STORE_NO \"門市編號\", \n");
            sb.AppendFormat("       T.STORE_NAME \"門市名稱\", \n");
            sb.AppendFormat("       TO_CHAR(T.TRADE_DATE, 'YYYY/MM/DD') \"交易日期\", \n");
            sb.AppendFormat("       TO_CHAR(T.MACHINE_ID) \"機台\", \n");
            sb.AppendFormat("       T.BILL_TYPE_NAME \"交易型態\", \n");
            sb.AppendFormat("       (SELECT COUNT(*) FROM SALE_HEAD S WHERE S.STORE_NO = T.STORE_NO AND TRUNC(S.TRADE_DATE) = TRUNC(T.TRADE_DATE) AND S.MACHINE_ID = T.MACHINE_ID) \"交易筆數\", \n");
            sb.AppendFormat("       T.PAID_MODE_NAME \"現金/信用卡\", \n");
            sb.AppendFormat("       T.信用卡筆數 \"信用卡筆數\", \n");
            sb.AppendFormat("       T.遠傳現金 \"遠傳現金\", \n");
            sb.AppendFormat("       T.和信現金 \"和信現金\", \n");
            sb.AppendFormat("       T.速博現金 \"速博現金\", \n");
            sb.AppendFormat("       T.遠通有單現金 \"遠通有單現金\", \n");
            sb.AppendFormat("       T.遠通無單現金 \"遠通無單現金\", \n");
            sb.AppendFormat("       T.\"SeedNet帳單現金\" \"SeedNet帳單現金\", \n");
            sb.AppendFormat("       T.遠傳帳單折抵 \"遠傳帳單折抵\", \n");
            sb.AppendFormat("       T.和信帳單折抵 \"和信帳單折抵\", \n");
            sb.AppendFormat("       T.快樂購總兌點數 \"快樂購總兌點數\", \n");
            sb.AppendFormat("       T.遠傳快樂購兌點數 \"遠傳快樂購兌點數\", \n");
            sb.AppendFormat("       T.和信快樂購兌點數 \"和信快樂購兌點數\", \n");
            sb.AppendFormat("       T.遠傳帳單快樂購金 \"遠傳帳單快樂購金\", \n");
            sb.AppendFormat("       T.和信帳單快樂購金 \"和信帳單快樂購金\", \n");
            sb.AppendFormat("       T.信用卡別 \"信用卡別\", \n");
            sb.AppendFormat("       T.遠傳信用卡 \"遠傳信用卡\", \n");
            sb.AppendFormat("       T.和信信用卡 \"和信信用卡\", \n");
            sb.AppendFormat("       T.速博信用卡 \"速博信用卡\" \n");
            sb.AppendFormat("  FROM VW_RPL_PIVOT_RECV_SUM T \n");
            sb.AppendFormat(" WHERE 1 = 1 \n");

            #region WHERE
            if (!string.IsNullOrEmpty(COMPANY.Trim()) && COMPANY.Trim() != "ALL")
            {
                if (COMPANY != "3")
                {
                    sb.AppendFormat("AND T.CMPNY_BILL_CODE  = " + OracleDBUtil.SqlStr(COMPANY.Trim()));
                }
                else
                {
                    sb.AppendFormat("AND (T.CMPNY_BILL_CODE  = '3' OR T.CMPNY_BILL_CODE = '4') ");
                }
            }

            if (!string.IsNullOrEmpty(TRADE_TYPE.Trim()) && TRADE_TYPE.Trim() != "ALL")
            {
                sb.AppendFormat(" AND T.BILL_TYPE = " + OracleDBUtil.SqlStr(TRADE_TYPE.Trim()));
            }

            if (!string.IsNullOrEmpty(STORE_NO_S.Trim()))
            {
                sb.AppendFormat(" AND T.STORE_NO >= " + OracleDBUtil.SqlStr(STORE_NO_S.Trim()));
            }

            if (!string.IsNullOrEmpty(STORE_NO_E.Trim()))
            {
                sb.AppendFormat(" AND T.STORE_NO <= " + OracleDBUtil.SqlStr(STORE_NO_E.Trim()));
            }

            if (!string.IsNullOrEmpty(TRADE_DATE_S.Trim()))
            {
                sb.AppendFormat(" AND TRUNC(T.TRADE_DATE) >= " + OracleDBUtil.DateStr(TRADE_DATE_S.Trim()));
            }

            if (!string.IsNullOrEmpty(TRADE_DATE_E.Trim()))
            {
                sb.AppendFormat(" AND TRUNC(T.TRADE_DATE) <= " + OracleDBUtil.DateStr(TRADE_DATE_E.Trim()));
            }

            #region 帳單金額

            if (!string.IsNullOrEmpty(BILL_S.Trim()) || !string.IsNullOrEmpty(BILL_S.Trim()))
            {
                String strAnd = null;
                if (!string.IsNullOrEmpty(BILL_S.Trim()) || !string.IsNullOrEmpty(BILL_S.Trim())) strAnd = " AND ";
                String[] fields = new String[] { "遠傳現金", "和信現金", "速博現金", "遠通有單現金", "遠通無單現金", 
                                                 "\"SeedNet帳單現金\"", "遠傳帳單折抵", "和信帳單折抵", "快樂購總兌點數", "遠傳快樂購兌點數", 
                                                 "和信快樂購兌點數", "遠傳帳單快樂購金", "和信帳單快樂購金", "遠傳信用卡", "和信信用卡", 
                                                 "速博信用卡" };

                sb.AppendFormat(" AND ( \n");

                foreach (String str in fields)
                {
                    sb.AppendFormat("(");
                    if (!string.IsNullOrEmpty(BILL_S.Trim())) sb.AppendFormat(" " + str + " >= " + OracleDBUtil.NumberStr(BILL_S.Trim()) + " \n");
                    sb.AppendFormat(strAnd);
                    if (!string.IsNullOrEmpty(BILL_E.Trim())) sb.AppendFormat(" " + str + " <= " + OracleDBUtil.NumberStr(BILL_E.Trim()) + " \n");
                    sb.AppendFormat(") OR ");
                }

                sb.AppendFormat(" 1 = 0 ) \n");
            }

            #endregion

            if (!string.IsNullOrEmpty(MOBILENO.Trim()))
            {
                sb.AppendFormat(" AND T.MSISDN = " + OracleDBUtil.SqlStr(MOBILENO.Trim()));
            }

            if (!string.IsNullOrEmpty(BILLNO.Trim()))
            {
                sb.AppendFormat(" AND T.SALE_NO = " + OracleDBUtil.SqlStr(BILLNO.Trim()));
            }
            #endregion

            sb.AppendFormat("  ) \n");
            sb.AppendFormat("  SELECT * FROM ( \n");
            sb.AppendFormat("  SELECT * \n");
            sb.AppendFormat("    FROM RPL028 \n");
            sb.AppendFormat("  UNION ALL \n");
            sb.AppendFormat("  SELECT '2' AS ORDER_INDEX, \n");
            sb.AppendFormat("         門市編號, \n");
            sb.AppendFormat("         門市名稱, \n");
            sb.AppendFormat("         交易日期, \n");
            sb.AppendFormat("         機台, \n");
            sb.AppendFormat("         '門市小計', \n");
            sb.AppendFormat("         NVL(SUM(交易筆數), 0) 交易筆數, \n");
            sb.AppendFormat("         NULL, \n");
            sb.AppendFormat("         NVL(SUM(信用卡筆數), 0) 信用卡筆數, \n");
            sb.AppendFormat("         NVL(SUM(遠傳現金), 0) 遠傳現金, \n");
            sb.AppendFormat("         NVL(SUM(和信現金), 0) 和信現金, \n");
            sb.AppendFormat("         NVL(SUM(速博現金), 0) 速博現金, \n");
            sb.AppendFormat("         NVL(SUM(遠通有單現金), 0) 遠通有單現金, \n");
            sb.AppendFormat("         NVL(SUM(遠通無單現金), 0) 遠通無單現金, \n");
            sb.AppendFormat("         NVL(SUM(\"SeedNet帳單現金\"), 0) SeedNet帳單現金, \n");
            sb.AppendFormat("         NVL(SUM(遠傳帳單折抵), 0) 遠傳帳單折抵, \n");
            sb.AppendFormat("         NVL(SUM(和信帳單折抵), 0) 和信帳單折抵, \n");
            sb.AppendFormat("         NVL(SUM(快樂購總兌點數), 0) 快樂購總兌點數, \n");
            sb.AppendFormat("         NVL(SUM(遠傳快樂購兌點數), 0) 遠傳快樂購兌點數, \n");
            sb.AppendFormat("         NVL(SUM(和信快樂購兌點數), 0) 和信快樂購兌點數, \n");
            sb.AppendFormat("         NVL(SUM(遠傳帳單快樂購金), 0) 遠傳帳單快樂購金, \n");
            sb.AppendFormat("         NVL(SUM(和信帳單快樂購金), 0) 和信帳單快樂購金, \n");
            sb.AppendFormat("         NULL, \n");
            sb.AppendFormat("         NVL(SUM(遠傳信用卡), 0) 遠傳信用卡, \n");
            sb.AppendFormat("         NVL(SUM(和信信用卡), 0) 和信信用卡, \n");
            sb.AppendFormat("         NVL(SUM(速博信用卡), 0) 速博信用卡 \n");
            sb.AppendFormat("    FROM RPL028 \n");
            sb.AppendFormat("   GROUP BY 門市編號, 門市名稱, 交易日期, 機台 \n");
            sb.AppendFormat("   ) \n");
            sb.AppendFormat("   ORDER BY 門市編號, 交易日期, 機台, 交易筆數, ORDER_INDEX \n");

            return OracleDBUtil.Query_Data(sb.ToString());
        }
        /*Author : 蔡坤霖
           Date : 2011 / 02 / 21
           Description: RPL029 日結交易彙總表
          */
        /// <summary>
        /// RPL029 日結交易彙總表
        /// </summary>
        /// <param name="STORE_NO_S">門市編號_起</param>
        /// <param name="STORE_NO_E">門市編號_訖</param>
        /// <param name="TOTAL_AMT">轉入總帳</param>
        /// <param name="TRADE_DATE_S">交易日期_起</param>
        /// <param name="TRADE_DATE_E">交易日期_訖</param>
        /// <returns></returns>
        public DataTable RPL029(string STORE_NO_S, string STORE_NO_E, string TRADE_DATE_S, string TRADE_DATE_E)
        {
            OracleConnection objConn = null;

            try
            {
                objConn = OracleDBUtil.GetConnection();
                System.Text.StringBuilder sb = new System.Text.StringBuilder();

                #region SELECT
                sb.AppendFormat("SELECT TO_CHAR(TRADE_DATE, 'YYYY/MM/DD')       \"交易日期\", \n");
                sb.AppendFormat("       STORE_NO                \"門市編號\", \n");
                sb.AppendFormat("       STORE_NAME              \"門市名稱\", \n");
                sb.AppendFormat("       NVL(FET_PAYMENT,0)      \"FET-帳單(現金)\", \n");
                sb.AppendFormat("       NVL(FET_CARD_PAYMENT,0) \"FET-帳單(信用卡)\", \n");
                sb.AppendFormat("       NVL(FET_CASH,0)         \"FET-銷售(現金)\", \n");
                sb.AppendFormat("       NVL(FET_CARD,0)         \"FET-銷售(信用卡)\", \n");
                sb.AppendFormat("       NVL(FET_退保證金,0)     \"退保證金\", \n");
                sb.AppendFormat("       NVL(FET_CASH_SUM,0)     \"FET-現金(合計)\", \n");
                sb.AppendFormat("       NVL(FET_CARD_SUM,0)     \"FET-信用卡(合計)\", \n");
                sb.AppendFormat("       NVL(KGT_PAYMENT,0)      \"KGT-帳單(現金)\", \n");
                sb.AppendFormat("       NVL(KGT_CARD_PAYMENT,0) \"KGT-帳單(信用卡)\", \n");
                sb.AppendFormat("       NVL(KGT_CASH,0)         \"KGT-銷售(現金)\", \n");
                sb.AppendFormat("       NVL(KGT_CARD,0)         \"KGT-銷售(信用卡)\", \n");
                sb.AppendFormat("       NVL(KGT_退保證金,0)     \"退保證金\", \n");
                sb.AppendFormat("       NVL(KGT_CASH_SUM,0)     \"KGT-現金(合計)\", \n");
                sb.AppendFormat("       NVL(KGT_CARD_SUM,0)     \"KGT-信用卡(合計)\", \n");
                sb.AppendFormat("       速博帳單                \"速博-帳單(現金)\" \n");
                sb.AppendFormat("  FROM VW_RPL029 T \n");
                sb.AppendFormat(" WHERE 1 = 1 \n");
                #endregion

                #region WHERE
                #region 門市邊號
                if (!string.IsNullOrEmpty(STORE_NO_S))
                {
                    sb.Append(" AND STORE_NO >= ");
                    sb.Append(Advtek.Utility.OracleDBUtil.SqlStr(STORE_NO_S.Trim()));
                }
                if (!string.IsNullOrEmpty(STORE_NO_E))
                {
                    sb.Append(" AND STORE_NO <= ");
                    sb.Append(Advtek.Utility.OracleDBUtil.SqlStr(STORE_NO_E.Trim()));
                }
                #endregion
                #region 交易日期
                if (!string.IsNullOrEmpty(TRADE_DATE_S))
                {
                    sb.Append(" AND TRUNC(TRADE_DATE) >= ");
                    sb.Append(Advtek.Utility.OracleDBUtil.DateStr(TRADE_DATE_S.Trim()));
                }
                if (!string.IsNullOrEmpty(TRADE_DATE_E))
                {
                    sb.Append(" AND TRUNC(TRADE_DATE) <= ");
                    sb.Append(Advtek.Utility.OracleDBUtil.DateStr(TRADE_DATE_E.Trim()));
                }
                #endregion
                #endregion

                #region -ORDER BY-
                sb.AppendFormat(" ORDER BY TO_CHAR(TRADE_DATE, 'YYYY/MM/DD') ,STORE_NO ");
                #endregion

                DataTable dt = Advtek.Utility.OracleDBUtil.GetDataSet(objConn, sb.ToString()).Tables[0];

                return dt;
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
        }

        /*Author : 蔡坤霖
          Date : 2011 / 02 / 17
          Description: RPL031 各項促銷銷售分析收入表
         */
        /// <summary>
        /// RPL031
        /// </summary>
        /// <param name="TRADE_DATE_S">交易日期(起)</param>
        /// <param name="TRADE_DATE_E">交易日期(訖)</param>
        /// <param name="PROMO_NO">促銷代碼</param>
        public DataTable RPL031(string TRADE_DATE_S, string TRADE_DATE_E, string PROMO_NO, string STORENO)
        {
            StringBuilder sb = new StringBuilder();
            StringBuilder strSelect = new StringBuilder();

            #region 組合SQL
            #region 區域
            //sb.AppendFormat("SELECT S.ZONE, S.ZONE_NAME, '1' AS TYPE ");
            //sb.AppendFormat("  FROM ZONE S ");
            //sb.AppendFormat(" ORDER BY ZONE ");
            sb.AppendFormat(" SELECT S.ZONE ,S.ZONE_NAME ,S.ROWN ,'1' AS TYPE FROM (                                 ");
            sb.AppendFormat(" SELECT ZONE, ZONE_NAME, '1' AS ROWN FROM ZONE WHERE SUBSTR(ZONE_NAME,1,1) = '北' UNION ");
            sb.AppendFormat(" SELECT ZONE, ZONE_NAME, '2' AS ROWN FROM ZONE WHERE SUBSTR(ZONE_NAME,1,1) = '中' UNION ");
            sb.AppendFormat(" SELECT ZONE, ZONE_NAME, '3' AS ROWN FROM ZONE WHERE SUBSTR(ZONE_NAME,1,1) = '南') S    ");
            sb.AppendFormat(" ORDER BY S.ROWN ,S.ZONE                                                                ");

            DataTable dt = OracleDBUtil.Query_Data(sb.ToString());

            foreach (DataRow dr in dt.Rows)
            {
                strSelect.AppendFormat(",\n");
                strSelect.AppendFormat("SUM(CASE WHEN T.ZONE = '{0}' AND T.CLOSEDATE IS NULL THEN T.TOTAL_AMOUNT ELSE 0 END) AS \"{1}\"", dr["ZONE"].ToString(), dr["ZONE_NAME"].ToString().PadRight(10, ' ').Substring(0, 10));
            }
            strSelect.AppendFormat(",\n");
            strSelect.AppendFormat(" SUM(CASE WHEN T.CLOSEDATE IS NOT NULL THEN T.TOTAL_AMOUNT ELSE 0 END) AS 閉店門市 ");
            #endregion

            #region 門市
            sb = new StringBuilder();

            sb.AppendFormat("SELECT T.STORE_NO AS ZONE, T.STORENAME AS ZONE_NAME, '2' AS TYPE ");
            sb.AppendFormat("  FROM STORE T ");
            sb.AppendFormat(" WHERE T.CLOSEDATE IS NULL ");
            if (!string.IsNullOrEmpty(STORENO) && STORENO.Length == 4)
            {
                sb.AppendFormat(" AND T.STORE_NO = " + OracleDBUtil.SqlStr(STORENO));
            }
            sb.AppendFormat(" ORDER BY ZONE ");

            dt = OracleDBUtil.Query_Data(sb.ToString());

            foreach (DataRow dr in dt.Rows)
            {
                strSelect.AppendFormat(",\n");
                strSelect.AppendFormat("SUM(CASE WHEN T.ZONE = '{0}' AND T.CLOSEDATE IS NULL THEN T.TOTAL_AMOUNT ELSE 0 END) AS \"{1}\"", dr["ZONE"].ToString(), dr["ZONE_NAME"].ToString().PadRight(10, ' ').Substring(0, 10));
            }
            #endregion
            #endregion

            sb = new StringBuilder();

            #region -SELECT-

            sb.AppendFormat("SELECT ");
            sb.AppendFormat(" T.PROMO_NO        AS 促銷代碼, \n");
            sb.AppendFormat(" T.PROMO_NAME      AS 促銷名稱, \n");
            sb.AppendFormat(" T.PRODNAME        AS 商品名稱, \n");
            sb.AppendFormat(" SUM(CASE WHEN T.ORDER_INDEX ='1' THEN T.TOTAL_AMOUNT ELSE 0 END) AS 全區 ");
            sb.Append(strSelect.ToString());
            sb.AppendFormat("  FROM VW_RPL_PIVOT_PROMO_SALE T \n");
            sb.AppendFormat(" WHERE 1 = 1 ");

            #endregion

            #region -WHERE-
            #region 交易日期
            if (!string.IsNullOrEmpty(TRADE_DATE_S))
            {
                sb.Append(" AND TRUNC(T.TRADE_DATE) >= ");
                sb.Append(Advtek.Utility.OracleDBUtil.DateStr(TRADE_DATE_S.Trim()));
            }
            if (!string.IsNullOrEmpty(TRADE_DATE_E))
            {
                sb.Append(" AND TRUNC(T.TRADE_DATE) <= ");
                sb.Append(Advtek.Utility.OracleDBUtil.DateStr(TRADE_DATE_E.Trim()));
            }
            #endregion
            #region 促銷代碼
            if (!string.IsNullOrEmpty(PROMO_NO))
            {
                sb.Append(" AND T.PROMO_NO = ");
                sb.Append(Advtek.Utility.OracleDBUtil.SqlStr(PROMO_NO.Trim()));
            }
            #endregion
            #endregion

            sb.AppendFormat(" GROUP BY T.PROMO_NO, T.PROMO_NAME, T.PRODNAME \n");
            sb.AppendFormat(" ORDER BY T.PROMO_NO, T.PROMO_NAME, T.PRODNAME \n");

            dt = OracleDBUtil.Query_Data(sb.ToString());

            return dt;
        }

        public DataTable RPL032(string store_s, string store_e, string prodNo_s, string prodNo_e, string ordDate_s, string ordDate_e, string STORENO)
        {
            DataTable dtResult = new DataTable();
            StringBuilder sbSql = new StringBuilder();
            StringBuilder sbWhere = new StringBuilder();
            string selectSql = @"SELECT ZONE_NAME,STORE_NO,STORENAME,TO_DATE(ORDDATE,'YYYY/MM/DD') as ORDDATE,ORDER_NO,SEQNO,PRODNO,PRODNAME,VENDER_NAME,ORDQTY,HQ_ADJ_ORDER_QTY,ADJ_QTY,REGULATION,REAL_ATR_QTY,APPROVE_RATE,INCOUNTQTY,INWAYQTY,INCOUNT_RATE,REMARK  FROM VW_RPL032";

            sbWhere.Append(" Where 1=1");

            if (ordDate_s.Length != 0 && ordDate_e.Length != 0)
                sbWhere.AppendFormat(" AND ORDDATE BETWEEN  {0} AND  {1} ",
                    OracleDBUtil.SqlStr(ordDate_s.Replace("/", "")), OracleDBUtil.SqlStr(ordDate_e.Replace("/", "")));

            if (prodNo_s.Length != 0 && prodNo_e.Length != 0)
                sbWhere.AppendFormat(" AND PRODNO BETWEEN {0} AND {1}", OracleDBUtil.SqlStr(prodNo_s), OracleDBUtil.SqlStr(prodNo_e));

            if (store_s.Length != 0 && store_e.Length != 0)
                sbWhere.AppendFormat(" AND STORE_NO BETWEEN {0} AND {1}", OracleDBUtil.SqlStr(store_s), OracleDBUtil.SqlStr(store_e));

            if (STORENO != "HQ")
                sbWhere.AppendFormat(" AND STORE_NO = {0}", OracleDBUtil.SqlStr(STORENO));

            sbSql.Append(selectSql);
            sbSql.Append(sbWhere.ToString());

            dtResult = OracleDBUtil.Query_Data(sbSql.ToString());
            return dtResult;
        }

        #region
        ///// <summary>
        ///// RPL033
        ///// </summary>
        ///// <param name="STK_DATE">月份</param>
        ///// <param name="PRODTYPENO_S">商品類別(起)</param>
        ///// <param name="PRODTYPENO_E">商品類別(訖)</param>
        ///// <param name="PRODNO_S">商品料號(起)</param>
        ///// <param name="PRODNO_E">商品料號(訖)</param>
        //public DataTable RPL033(string STK_DATE, string PRODTYPENO_S, string PRODTYPENO_E,
        //    string PRODNO_S, string PRODNO_E)
        //{
        //    StringBuilder sb = new StringBuilder();
        //    sb.Append(" SELECT PRODNO as ItemCode, ");
        //    sb.Append(" PRODNAME as 品名,");
        //    sb.Append(" TITLE_TAB,ON_HAND_QTY,ORDER_INDEX ");
        //    sb.Append(" FROM VW_RPL_PIVOT_STORE_STOCK");
        //    sb.Append(" WHERE 1 = 1");

        //    if (!string.IsNullOrEmpty(STK_DATE))
        //    {
        //        sb.Append(" AND STK_DATE = " + OracleDBUtil.SqlStr(STK_DATE.Trim().Replace("/", "")));
        //    }
        //    if (!string.IsNullOrEmpty(PRODTYPENO_S))
        //    {
        //        sb.Append(" AND PRODTYPENO >= " + OracleDBUtil.SqlStr(PRODTYPENO_S.Trim()));
        //    }
        //    if (!string.IsNullOrEmpty(PRODTYPENO_E))
        //    {
        //        sb.Append(" AND PRODTYPENO <= " + OracleDBUtil.SqlStr(PRODTYPENO_E.Trim()));
        //    }
        //    if (!string.IsNullOrEmpty(PRODNO_S))
        //    {
        //        sb.Append(" AND PRODNO >= " + OracleDBUtil.SqlStr(PRODNO_S.Trim()));
        //    }
        //    if (!string.IsNullOrEmpty(PRODNO_E))
        //    {
        //        sb.Append(" AND PRODNO <= " + OracleDBUtil.SqlStr(PRODNO_E.Trim()));
        //    }
        //    //sb.Append(" ORDER BY ORDER_INDEX ,STORENO ");

        //    DataTable dt = OracleDBUtil.Query_Data(sb.ToString());

        //    try
        //    {

        //        #region 處理門市欄位
        //        DataTable mainDt = dt.DefaultView.ToTable(true, new string[] { "ItemCode", "品名" }); //distinct Itemcode
        //        DataTable resultDt = mainDt.Copy();

        //        resultDt.Columns.Add(new DataColumn("全區"));
        //        resultDt.PrimaryKey = new DataColumn[] { resultDt.Columns["ItemCode"] };
        //        //區域
        //        foreach (DataRow row in mainDt.Rows)
        //        {
        //            DataTable detailDt = dt.DefaultView.ToTable(true, new string[] { "ItemCode", "TITLE_TAB", "ON_HAND_QTY", "ORDER_INDEX" });//抓出門市名稱及數量
        //            DataRow[] rows = detailDt.Select(string.Format("ItemCode = '{0}' and ORDER_INDEX=1 ", row["ItemCode"].ToString()));
        //            foreach (DataRow row2 in rows)
        //            {
        //                if (!resultDt.Columns.Contains(row2["TITLE_TAB"].ToString()))
        //                {
        //                    DataColumn col = new DataColumn(row2["TITLE_TAB"].ToString());
        //                    col.DefaultValue = 0;
        //                    //判斷是否已經有欄位

        //                    resultDt.Columns.Add(col);
        //                }

        //                DataRow editRow = resultDt.Rows.Find(row["ItemCode"]);
        //                editRow.BeginEdit();
        //                editRow[row2["TITLE_TAB"].ToString()] = row2["ON_HAND_QTY"];
        //                editRow.EndEdit();
        //                editRow.AcceptChanges();
        //            }

        //        }
        //        #region 計算全區
        //        foreach (DataRow row in resultDt.Rows)
        //        {
        //            int sum = 0;
        //            for (int i = 3; i < resultDt.Columns.Count; i++)
        //            {
        //                sum += Convert.ToInt32(row[i]);
        //            }

        //            row.BeginEdit();
        //            row["全區"] = sum;
        //            row.EndEdit();
        //        }
        //        #endregion

        //        //門市
        //        foreach (DataRow row in mainDt.Rows)
        //        {
        //            DataTable detailDt = dt.DefaultView.ToTable(true, new string[] { "ItemCode", "TITLE_TAB", "ON_HAND_QTY", "ORDER_INDEX" });//抓出門市名稱及數量
        //            DataRow[] rows = detailDt.Select(string.Format("ItemCode = '{0}' and ORDER_INDEX=2 ", row["ItemCode"].ToString()));
        //            foreach (DataRow row2 in rows)
        //            {
        //                if (!resultDt.Columns.Contains(row2["TITLE_TAB"].ToString()))
        //                {
        //                    DataColumn col = new DataColumn(row2["TITLE_TAB"].ToString());
        //                    col.DefaultValue = 0;
        //                    //判斷是否已經有欄位

        //                    resultDt.Columns.Add(col);
        //                }

        //                DataRow editRow = resultDt.Rows.Find(row["ItemCode"]);
        //                editRow.BeginEdit();
        //                editRow[row2["TITLE_TAB"].ToString()] = row2["ON_HAND_QTY"];
        //                editRow.EndEdit();
        //                editRow.AcceptChanges();
        //            }

        //        }
        //        #endregion

        //        return resultDt;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}
        #endregion

        /// <summary>
        /// RPL033 門市庫存量分析表
        /// </summary>
        /// <param name="STK_DATE">月份</param>
        /// <param name="PRODTYPENO_S">商品類別(起)</param>
        /// <param name="PRODTYPENO_E">商品類別(訖)</param>
        /// <param name="PRODNO_S">商品料號(起)</param>
        /// <param name="PRODNO_E">商品料號(訖)</param>
        public DataTable RPL033(string STK_DATE, string PRODTYPENO_S, string PRODTYPENO_E,
            string PRODNO_S, string PRODNO_E)
        {

            StringBuilder sb = new StringBuilder();
            StringBuilder strSelect = new StringBuilder();

            #region 組合SQL
            #region 區域
            //sb.AppendFormat("SELECT S.ZONE, S.ZONE_NAME, '1' AS TYPE ");
            //sb.AppendFormat("  FROM ZONE S ");
            //sb.AppendFormat(" ORDER BY ZONE ");
            sb.AppendFormat(" SELECT S.ZONE ,S.ZONE_NAME ,S.ROWN ,'1' AS TYPE FROM (                                 ");
            sb.AppendFormat(" SELECT ZONE, ZONE_NAME, '1' AS ROWN FROM ZONE WHERE SUBSTR(ZONE_NAME,1,1) = '北' UNION ");
            sb.AppendFormat(" SELECT ZONE, ZONE_NAME, '2' AS ROWN FROM ZONE WHERE SUBSTR(ZONE_NAME,1,1) = '中' UNION ");
            sb.AppendFormat(" SELECT ZONE, ZONE_NAME, '3' AS ROWN FROM ZONE WHERE SUBSTR(ZONE_NAME,1,1) = '南') S    ");
            sb.AppendFormat(" ORDER BY S.ROWN ,S.ZONE                                                                ");

            DataTable dt = OracleDBUtil.Query_Data(sb.ToString());

            foreach (DataRow dr in dt.Rows)
            {
                strSelect.AppendFormat(",\n");
                strSelect.AppendFormat("SUM(CASE WHEN T.ZONE = '{0}' AND T.CLOSEDATE IS NULL THEN T.ON_HAND_QTY ELSE 0 END) AS \"{1}\"", dr["ZONE"].ToString(), dr["ZONE_NAME"].ToString().PadRight(10, ' ').Substring(0, 10));
            }
            #endregion

            #region 門市
            sb = new StringBuilder();

            sb.AppendFormat("SELECT T.STORE_NO AS ZONE, T.STORENAME AS ZONE_NAME, '2' AS TYPE ,T.ZONE AS ZONE1 ");
            sb.AppendFormat("  FROM STORE T ");
            sb.AppendFormat(" WHERE T.CLOSEDATE IS NULL ");
            sb.AppendFormat(" ORDER BY ZONE1 ,ZONE ");

            dt = OracleDBUtil.Query_Data(sb.ToString());

            foreach (DataRow dr in dt.Rows)
            {
                strSelect.AppendFormat(",\n");
                strSelect.AppendFormat("SUM(CASE WHEN T.ZONE = '{0}' AND T.CLOSEDATE IS NULL THEN T.ON_HAND_QTY ELSE 0 END) AS \"{1}\"", dr["ZONE"].ToString(), dr["ZONE_NAME"].ToString().PadRight(10, ' ').Substring(0, 10));
            }
            #endregion
            #endregion

            sb = new StringBuilder();

            #region -SELECT-

            sb.AppendFormat("SELECT ");
            sb.AppendFormat(" T.PRODNO          AS ItemCode, \n");
            sb.AppendFormat(" T.PRODNAME        AS 品名, \n");
            sb.AppendFormat(" SUM(CASE WHEN T.ORDER_INDEX ='1' THEN T.ON_HAND_QTY ELSE 0 END) AS 全區 ");
            sb.Append(strSelect.ToString());
            if (DateTime.Now.ToString("yyyy/MM") == STK_DATE)
            { sb.AppendFormat("  FROM VW_RPL_PIVOT_STORE_STOCK1 T \n"); }
            else
            { sb.AppendFormat("  FROM VW_RPL_PIVOT_STORE_STOCK T \n"); }
            sb.AppendFormat(" WHERE 1 = 1 ");

            #endregion

            #region -WHERE-

            #region 月份
            if (!string.IsNullOrEmpty(STK_DATE))
            {
                sb.Append(" AND T.STK_DATE = " + OracleDBUtil.SqlStr(STK_DATE.Trim().Replace("/", "")));
            }
            #endregion
            #region 商品類別
            if (!string.IsNullOrEmpty(PRODTYPENO_S))
            {
                sb.Append(" AND T.PRODTYPENO >= ");
                sb.Append(Advtek.Utility.OracleDBUtil.SqlStr(PRODTYPENO_S.Trim()));
            }
            if (!string.IsNullOrEmpty(PRODTYPENO_E))
            {
                sb.Append(" AND T.PRODTYPENO <= ");
                sb.Append(Advtek.Utility.OracleDBUtil.SqlStr(PRODTYPENO_E.Trim()));
            }
            #endregion
            #region 商品料號
            if (!string.IsNullOrEmpty(PRODNO_S))
            {
                sb.Append(" AND T.PRODNO >= ");
                sb.Append(Advtek.Utility.OracleDBUtil.SqlStr(PRODNO_S.Trim()));
            }
            if (!string.IsNullOrEmpty(PRODNO_E))
            {
                sb.Append(" AND T.PRODNO <= ");
                sb.Append(Advtek.Utility.OracleDBUtil.SqlStr(PRODNO_E.Trim()));
            }
            #endregion

            #endregion

            sb.AppendFormat(" GROUP BY T.PRODNO, T.PRODNAME \n");
            sb.AppendFormat(" ORDER BY T.PRODNO, T.PRODNAME \n");

            dt = OracleDBUtil.Query_Data(sb.ToString());

            return dt;
        }


        #region 宗佑
        /*
         Author：宗佑
         Date：100.2.14
         Description：RPL040 SQL敘述
        */
        /// <summary>
        /// RPL040
        /// </summary>
        /// <param name="TRADE_DATE_S">交易日期(起)</param>
        /// <param name="TRADE_DATE_E">交易日期(訖)</param>
        public DataTable RPL040(string TRADE_DATE_S, string TRADE_DATE_E)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(" SELECT PRODNO AS 商品料號, PRODNAME AS 商品名稱, ");
            sb.Append(" SALE_QTY AS 總數量, SALE_AMOUNT AS 總金額, ");
            sb.Append(" ACCOUNTCODE AS 會計科目 ");
            sb.Append(" FROM VW_RPL_POS_DISCOUNT ");
            sb.Append(" WHERE 1 = 1");

            if (!string.IsNullOrEmpty(TRADE_DATE_S))
            {
                sb.Append(" AND TRUNC(TRADE_DATE) >= " + OracleDBUtil.DateStr(TRADE_DATE_S.Trim()));
            }
            if (!string.IsNullOrEmpty(TRADE_DATE_E))
            {
                sb.Append(" AND TRUNC(TRADE_DATE) <= " + OracleDBUtil.DateStr(TRADE_DATE_E.Trim()));
            }

            DataTable dt = OracleDBUtil.Query_Data(sb.ToString());

            return dt;
        }
        #endregion


        /// <summary>
        /// RPL053 庫存日報表
        /// </summary>
        /// <param name="Search_list">交易日期(起)</param>
        /// <param name="STORE_S">門市編號(起)</param>
        /// <param name="STORE_E">門市編號(訖)</param>
        /// <param name="ORDER_DATE_S">交易日期(起)</param>
        /// <param name="ORDER_DATE_E">交易日期(訖)</param>
        /// <param name="PRODTYPENO_S">商品類別(起)</param>
        /// <param name="PRODTYPENO_E">商品類別(訖)</param>
        /// <param name="PRODNO_S">商品料號(起)</param>
        /// <param name="PRODNO_E">商品料號(訖)</param>
        /// <param name="SALE_STATUS">銷售型態</param>
        public DataTable RPL053(string Search_list, string STORE_S, string STORE_E,
     string ORDER_DATE_S, string ORDER_DATE_E, string PRODTYPENO_S, string PRODTYPENO_E,
     string PRODNO_S, string PRODNO_E)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append(" select PRODNO as 商品料號,");
            sb.Append(" PRODNAME as 商品名稱,");
            sb.Append("SUM(展示倉		 ) AS 展示倉		 ,");
            sb.Append("INV_VW_RPL053(PRODNO, " + OracleDBUtil.SqlStr(ORDER_DATE_S) + ",'1'," + OracleDBUtil.SqlStr(STORE_S) + "," + OracleDBUtil.SqlStr(STORE_E) + ") AS 期初			 ,");
            sb.Append("SUM(進貨			 ) AS 進貨			 ,");
            sb.Append("SUM(銷貨			 ) AS 銷貨			 ,");
            sb.Append("SUM(銷退			 ) AS 銷退			 ,");
            sb.Append("SUM(退倉			 ) AS 退倉			 ,");
            sb.Append("SUM(移出			 ) AS 移出			 ,");
            sb.Append("SUM(撥入			 ) AS 撥入			 ,");
            sb.Append("SUM(調整			 ) AS 調整			 ,");
            sb.Append("SUM(驗退			 ) AS 驗退			 ,");
            sb.Append("SUM(維修倉		 ) AS 維修倉		 ,");
            sb.Append("INV_VW_RPL053(PRODNO, " + OracleDBUtil.SqlStr(ORDER_DATE_E) + ",'2'," + OracleDBUtil.SqlStr(STORE_S) + "," + OracleDBUtil.SqlStr(STORE_E) + ") AS 銷售倉期末,");
            sb.Append("INV_VW_RPL053(PRODNO, " + OracleDBUtil.SqlStr(ORDER_DATE_E) + ",'3'," + OracleDBUtil.SqlStr(STORE_S) + "," + OracleDBUtil.SqlStr(STORE_E) + ") AS 租賃倉期末 ");
            sb.Append(" FROM VW_RPL053 where 1=1 ");

            if (!string.IsNullOrEmpty(Search_list) && Search_list != "ALL")
            {
                sb.Append(" AND ISCONSIGNMENT = " + OracleDBUtil.SqlStr(Search_list));
            }

            if (!string.IsNullOrEmpty(STORE_S))
            {
                sb.Append(" AND STORE_NO >= " + OracleDBUtil.SqlStr(STORE_S));
            }

            if (!string.IsNullOrEmpty(STORE_E))
            {
                sb.Append(" AND STORE_NO <= " + OracleDBUtil.SqlStr(STORE_E));
            }

            if (!string.IsNullOrEmpty(ORDER_DATE_S))
            {
                sb.Append(" AND STK_DATE >= ");
                sb.Append(Advtek.Utility.OracleDBUtil.DateStr(ORDER_DATE_S.Trim()));
            }

            if (!string.IsNullOrEmpty(ORDER_DATE_E))
            {
                sb.Append(" AND STK_DATE <= ");
                sb.Append(Advtek.Utility.OracleDBUtil.DateStr(ORDER_DATE_E.Trim()));
            }

            if (!string.IsNullOrEmpty(PRODTYPENO_S))
            {
                sb.Append(" AND PRODTYPENO >= " + OracleDBUtil.SqlStr(PRODTYPENO_S));
            }

            if (!string.IsNullOrEmpty(PRODTYPENO_E))
            {
                sb.Append(" AND PRODTYPENO <= " + OracleDBUtil.SqlStr(PRODTYPENO_E));
            }

            if (!string.IsNullOrEmpty(PRODNO_S))
            {
                sb.Append(" AND PRODNO >= " + OracleDBUtil.SqlStr(PRODNO_S));
            }

            if (!string.IsNullOrEmpty(PRODNO_E))
            {
                sb.Append(" AND PRODNO <= " + OracleDBUtil.SqlStr(PRODNO_E));
            }

            sb.Append(" GROUP BY PRODNO,PRODNAME");
            sb.Append(" ORDER BY PRODNO");

            DataTable dt = OracleDBUtil.Query_Data(sb.ToString());
            return dt;
        }

        /*Author : 蔡坤霖
           Date : 2011 / 02 / 21
           Description: RPL045 Billing保證金明細表/退保證金明細表
          */
        /// <summary>
        /// RPL045 Billing保證金明細表/退保證金明細表
        /// </summary>
        /// <param name="TRADE_DATE_S">啟用日期_起</param>
        /// <param name="TRADE_DATE_E">啟用日期_訖</param>
        /// <param name="MSISDN">門號</param>
        /// <returns></returns>
        public DataTable RPL045(string TRADE_DATE_S, string TRADE_DATE_E, string MSISDN)
        {
            OracleConnection objConn = null;

            try
            {
                objConn = OracleDBUtil.GetConnection();
                System.Text.StringBuilder sb = new System.Text.StringBuilder();

                #region SELECT
                sb.AppendFormat("SELECT TO_CHAR(T.TRADE_DATE,'YYYY/MM/DD') AS 啟用日期  \n");
                sb.AppendFormat("      ,MSISDNMASK(T.MSISDN) AS 門號        \n");
                sb.AppendFormat("      ,T.BILLING_ACCOUNT_ID AS 客戶帳號    \n");
                sb.AppendFormat("      ,T.PROMOTION_CODE     AS 促銷方案    \n");
                sb.AppendFormat("      ,T.TOTAL_AMOUNT       AS 保證金金額  \n");
                sb.AppendFormat("      ,T.STORE_NO           AS 門市編號    \n");
                sb.AppendFormat("      ,T.SALE_PERSON || T.EMPNAME  AS 處理人員    \n");
                sb.AppendFormat("      ,''                   AS \"保證金狀態說明\" \n");
                sb.AppendFormat("  FROM VW_RPL_MARGIN_REPORT T \n");
                sb.AppendFormat(" WHERE 1 = 1 \n");
                #endregion

                #region WHERE
                #region 啟用日期
                if (!string.IsNullOrEmpty(TRADE_DATE_S))
                {
                    sb.Append(" AND TRUNC(T.TRADE_DATE) >= ");
                    sb.Append(Advtek.Utility.OracleDBUtil.DateStr(TRADE_DATE_S.Trim()));
                }
                if (!string.IsNullOrEmpty(TRADE_DATE_E))
                {
                    sb.Append(" AND TRUNC(T.TRADE_DATE) <= ");
                    sb.Append(Advtek.Utility.OracleDBUtil.DateStr(TRADE_DATE_E.Trim()));
                }
                #endregion
                #region 門號
                if (!string.IsNullOrEmpty(MSISDN))
                {
                    sb.Append(" AND T.MSISDN = ");
                    sb.Append(Advtek.Utility.OracleDBUtil.SqlStr(MSISDN.Trim()));
                }
                #endregion
                #endregion

                #region -ORDER BY-
                sb.AppendFormat(" ORDER BY TO_CHAR(T.TRADE_DATE,'YYYY/MM/DD') ,T.MSISDN  ");
                #endregion

                DataTable dt = Advtek.Utility.OracleDBUtil.GetDataSet(objConn, sb.ToString()).Tables[0];

                return dt;
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
        }

        /*Author : 蔡坤霖
           Date : 2011 / 02 / 25
           Description: RPL046 門市銷售日報表 明細表
          */
        /// <summary>
        /// RPL046 門市銷售日報表 明細表
        /// </summary>
        /// <param name="STORE_NO_S"></param>
        /// <param name="STORE_NO_E"></param>
        /// <param name="TRADE_DATE_S"></param>
        /// <param name="TRADE_DATE_E"></param>
        /// <param name="MACHINE_ID"></param>
        /// <param name="MSISDN"></param>
        /// <param name="PRODTYPENO_S"></param>
        /// <param name="PRODTYPENO_E"></param>
        /// <param name="PRODNO_S"></param>
        /// <param name="PRODNO_E"></param>
        /// <param name="INVOICE_NO_S"></param>
        /// <param name="INVOICE_NO_E"></param>
        /// <param name="TOTAL_AMOUNT_S"></param>
        /// <param name="TOTAL_AMOUNT_E"></param>
        /// <param name="SALE_PERSON_S"></param>
        /// <param name="SALE_PERSON_E"></param>
        /// <returns></returns>
        public DataTable RPL046_DETAIL(string STORE_NO_S, string STORE_NO_E, string TRADE_DATE_S, string TRADE_DATE_E,
                                string MACHINE_ID, string MSISDN, string PRODTYPENO_S, string PRODTYPENO_E,
                                string PRODNO_S, string PRODNO_E, string INVOICE_NO_S, string INVOICE_NO_E,
                                string TOTAL_AMOUNT_S, string TOTAL_AMOUNT_E, string SALE_PERSON_S, string SALE_PERSON_E)
        {
            OracleConnection objConn = null;

            try
            {
                objConn = OracleDBUtil.GetConnection();
                System.Text.StringBuilder sb = new System.Text.StringBuilder();

                #region RPL046 明細表 SQL
                sb.AppendFormat("WITH   \n");
                sb.AppendFormat("RPL046_HEADER AS (   \n");
                sb.AppendFormat("SELECT SH.POSUUID_MASTER,  \n");
                sb.AppendFormat("       '1' ORDER_INDEX,  \n");
                sb.AppendFormat("       SH.SALE_TYPE,  \n");
                sb.AppendFormat("       SH.TRADE_DATE,  \n");
                sb.AppendFormat("       SH.STORE_NO 門市編號,  \n");
                sb.AppendFormat("       TO_CHAR(SH.TRADE_DATE,'YYYY/MM/DD') 交易日期,  \n");
                sb.AppendFormat("       SH.MACHINE_ID 機台編號,  \n");
                sb.AppendFormat("       SD.MSISDN 門號,  \n");
                sb.AppendFormat("       PROD.PRODTYPENO 商品類別,  \n");
                sb.AppendFormat("       SD.PRODNO 商品料號,  \n");
                //sb.AppendFormat("       VOUCHER.INVOICE_NO 發票號碼,  \n");
                sb.AppendFormat("       FN_GET_INVOICE_DATA(SH.POSUUID_MASTER) 發票號碼,  \n");
                sb.AppendFormat("       VOUCHER.TOTAL_AMOUNT 交易金額,  \n");
                sb.AppendFormat("       SH.SALE_PERSON 員工編號,  \n");
                sb.AppendFormat("       DECODE(SH.SALE_TYPE, 1, '銷售交易', 2, '帳單代收') AS 交易類別,  \n");
                sb.AppendFormat("       SH.SALE_NO 交易序號,  \n");
                sb.AppendFormat("       SD.BARCODE1 帳單號碼_條碼一,  \n");
                sb.AppendFormat("       SD.MSISDN || SD.BARCODE2 門號_條碼二,  \n");
                sb.AppendFormat("       PROD.PRODNAME 商品名稱, \n");
                sb.AppendFormat("       SD.BARCODE3 條碼三,  \n");
                sb.AppendFormat("       SD.QUANTITY 數量,  \n");
                sb.AppendFormat("       SD.BEFORE_TAX 未稅金額,  \n");
                sb.AppendFormat("       SD.TAX 稅額,  \n");
                sb.AppendFormat("       SD.TOTAL_AMOUNT 銷售金額,  \n");
                sb.AppendFormat("       SD.PROMOTION_CODE 促銷代碼,  \n");
                sb.AppendFormat("       SH.REMARK 備註  \n");
                sb.AppendFormat("  FROM SALE_HEAD SH,  \n");
                sb.AppendFormat("       SALE_DETAIL SD,  \n");
                sb.AppendFormat("       PRODUCT PROD,  \n");
                sb.AppendFormat("       (SELECT SH.POSUUID_MASTER, IH.ID, IH.INVOICE_NO, IH.TOTAL_AMOUNT ,II.PRODNO \n");
                sb.AppendFormat("          FROM SALE_HEAD        SH,  \n");
                sb.AppendFormat("               VOUCHER_RELATION VR,  \n");
                sb.AppendFormat("               INVOICE_HEAD     IH,  \n");
                sb.AppendFormat("               INVOICE_ITEM     II  \n");
                sb.AppendFormat("         WHERE SH.POSUUID_MASTER = VR.SALE_HEAD_ID  \n");
                sb.AppendFormat("           AND VR.VOUCHER_ID = IH.ID  \n");
                sb.AppendFormat("           AND IH.ID = II.INVOICE_HEAD_ID  \n");
                //sb.AppendFormat("           AND NVL(IH.IS_INVALID, 'N') = 'N'  \n");
                sb.AppendFormat("        UNION ALL  \n");
                sb.AppendFormat("        SELECT SH.POSUUID_MASTER, MIH.ID, MIH.INVOICE_NO, MIH.TOTAL_AMOUNT ,MII.PRODNO \n");
                sb.AppendFormat("          FROM SALE_HEAD           SH,  \n");
                sb.AppendFormat("               VOUCHER_RELATION    VR,  \n");
                sb.AppendFormat("               MANUAL_INVOICE_HEAD MIH,  \n");
                sb.AppendFormat("               MANUAL_INVOICE_ITEM MII  \n");
                sb.AppendFormat("         WHERE SH.POSUUID_MASTER = VR.SALE_HEAD_ID  \n");
                sb.AppendFormat("           AND VR.VOUCHER_ID = MIH.ID  \n");
                sb.AppendFormat("           AND MIH.ID = MII.MANUAL_INVOICE_HEAD_ID  \n");
                //sb.AppendFormat("           AND NVL(MIH.IS_INVALID, 'N') = 'N'  \n");
                sb.AppendFormat("        UNION ALL  \n");
                sb.AppendFormat("        SELECT SH.POSUUID_MASTER,  \n");
                sb.AppendFormat("               RH.ID,  \n");
                sb.AppendFormat("               RH.RECEIPT_NO AS INVOICE_NO,  \n");
                sb.AppendFormat("               RH.TOTAL_AMOUNT   \n");
                sb.AppendFormat("               RI.PRODNO  \n");
                sb.AppendFormat("          FROM SALE_HEAD SH, VOUCHER_RELATION VR, RECEIPT_HEAD RH ,RECEIPT_ITEM RI \n");
                sb.AppendFormat("         WHERE SH.POSUUID_MASTER = VR.SALE_HEAD_ID  \n");
                sb.AppendFormat("           AND VR.VOUCHER_ID = RH.ID  \n");
                sb.AppendFormat("           AND RH.ID = RI.RECEIPT_HEAD_ID  \n");
                //sb.AppendFormat("           AND NVL(RH.IS_INVALID, 'N') = 'N') VOUCHER  \n");
                sb.AppendFormat("            ) VOUCHER  \n");
                sb.AppendFormat(" WHERE SH.POSUUID_MASTER = SD.POSUUID_MASTER  \n");
                sb.AppendFormat("   AND SH.POSUUID_MASTER = VOUCHER.POSUUID_MASTER  \n");
                sb.AppendFormat("   AND SD.PRODNO = PROD.PRODNO  \n");
                sb.AppendFormat("   AND SD.PRODNO = VOUCHER.PRODNO(+)  \n");
                //sb.AppendFormat("   AND SH.SALE_STATUS = '2'  \n");

                #region WHERE
                #region 門市編號
                if (!string.IsNullOrEmpty(STORE_NO_S))
                {
                    sb.Append(" AND SH.STORE_NO >= ");
                    sb.Append(Advtek.Utility.OracleDBUtil.SqlStr(STORE_NO_S.Trim()));
                    sb.AppendLine();
                }
                if (!string.IsNullOrEmpty(STORE_NO_E))
                {
                    sb.Append(" AND SH.STORE_NO <= ");
                    sb.Append(Advtek.Utility.OracleDBUtil.SqlStr(STORE_NO_E.Trim()));
                    sb.AppendLine();
                }
                #endregion
                #region 交易日期
                if (!string.IsNullOrEmpty(TRADE_DATE_S))
                {
                    sb.Append(" AND TRUNC(SH.TRADE_DATE) >= ");
                    sb.Append(Advtek.Utility.OracleDBUtil.DateStr(TRADE_DATE_S.Trim()));
                    sb.AppendLine();
                }
                if (!string.IsNullOrEmpty(TRADE_DATE_E))
                {
                    sb.Append(" AND TRUNC(SH.TRADE_DATE) <= ");
                    sb.Append(Advtek.Utility.OracleDBUtil.DateStr(TRADE_DATE_E.Trim()));
                    sb.AppendLine();
                }
                #endregion
                #region 機台編號
                if (!string.IsNullOrEmpty(MACHINE_ID) && MACHINE_ID.ToUpper().Trim() != "ALL")
                {
                    sb.Append(" AND SH.MACHINE_ID = ");
                    sb.Append(Advtek.Utility.OracleDBUtil.SqlStr(MACHINE_ID.Trim()));
                    sb.AppendLine();
                }
                else if (string.IsNullOrEmpty(MACHINE_ID) && MACHINE_ID.ToUpper().Trim() == "ALL")
                {
                    sb.Append(" AND SH.MACHINE_ID IN (SELECT HOST_NO FROM STORE_TERMINATING_MACHINE WHERE 1=1 ");

                    if (!string.IsNullOrEmpty(STORE_NO_S))
                    {
                        sb.Append(" AND STORE_NO >= ");
                        sb.Append(Advtek.Utility.OracleDBUtil.SqlStr(STORE_NO_S.Trim()));
                    }
                    if (!string.IsNullOrEmpty(STORE_NO_E))
                    {
                        sb.Append(" AND STORE_NO <= ");
                        sb.Append(Advtek.Utility.OracleDBUtil.SqlStr(STORE_NO_E.Trim()));
                    }

                    sb.Append(" ) ");
                    sb.AppendLine();
                }
                #endregion
                #region 門號
                if (!string.IsNullOrEmpty(MSISDN))
                {
                    sb.Append(" AND SD.MSISDN = ");
                    sb.Append(Advtek.Utility.OracleDBUtil.SqlStr(MSISDN.Trim()));
                    sb.AppendLine();
                }
                #endregion
                #region 商品類別
                if (!string.IsNullOrEmpty(PRODTYPENO_S))
                {
                    sb.Append(" AND PROD.PRODTYPENO >= ");
                    sb.Append(Advtek.Utility.OracleDBUtil.SqlStr(PRODTYPENO_S.Trim()));
                    sb.AppendLine();
                }
                if (!string.IsNullOrEmpty(PRODTYPENO_E))
                {
                    sb.Append(" AND PROD.PRODTYPENO <= ");
                    sb.Append(Advtek.Utility.OracleDBUtil.SqlStr(PRODTYPENO_E.Trim()));
                    sb.AppendLine();
                }
                #endregion
                #region 商品料號
                if (!string.IsNullOrEmpty(PRODNO_S))
                {
                    sb.Append(" AND SD.PRODNO >= ");
                    sb.Append(Advtek.Utility.OracleDBUtil.SqlStr(PRODNO_S.Trim()));
                    sb.AppendLine();
                }
                if (!string.IsNullOrEmpty(PRODNO_E))
                {
                    sb.Append(" AND SD.PRODNO <= ");
                    sb.Append(Advtek.Utility.OracleDBUtil.SqlStr(PRODNO_E.Trim()));
                    sb.AppendLine();
                }
                #endregion
                #region 發票號碼
                if (!string.IsNullOrEmpty(INVOICE_NO_S))
                {
                    sb.Append(" AND VOUCHER.INVOICE_NO >= ");
                    sb.Append(Advtek.Utility.OracleDBUtil.SqlStr(INVOICE_NO_S.Trim()));
                    sb.AppendLine();
                }
                if (!string.IsNullOrEmpty(INVOICE_NO_E))
                {
                    sb.Append(" AND VOUCHER.INVOICE_NO <= ");
                    sb.Append(Advtek.Utility.OracleDBUtil.SqlStr(INVOICE_NO_E.Trim()));
                    sb.AppendLine();
                }
                #endregion
                #region 交易金額
                if (!string.IsNullOrEmpty(TOTAL_AMOUNT_S))
                {
                    sb.Append(" AND VOUCHER.TOTAL_AMOUNT >= ");
                    sb.Append(Advtek.Utility.OracleDBUtil.SqlStr(TOTAL_AMOUNT_S.Trim()));
                    sb.AppendLine();
                }
                if (!string.IsNullOrEmpty(TOTAL_AMOUNT_E))
                {
                    sb.Append(" AND VOUCHER.TOTAL_AMOUNT <= ");
                    sb.Append(Advtek.Utility.OracleDBUtil.SqlStr(TOTAL_AMOUNT_E.Trim()));
                    sb.AppendLine();
                }
                #endregion
                #region 員工編號
                if (!string.IsNullOrEmpty(SALE_PERSON_S))
                {
                    sb.Append(" AND SH.SALE_PERSON >= ");
                    sb.Append(Advtek.Utility.OracleDBUtil.SqlStr(SALE_PERSON_S.Trim()));
                    sb.AppendLine();
                }
                if (!string.IsNullOrEmpty(SALE_PERSON_E))
                {
                    sb.Append(" AND SH.SALE_PERSON <= ");
                    sb.Append(Advtek.Utility.OracleDBUtil.SqlStr(SALE_PERSON_E.Trim()));
                    sb.AppendLine();
                }
                #endregion
                #endregion

                sb.AppendFormat(")   \n");

                sb.AppendFormat("  \n");
                sb.AppendFormat("SELECT 交易日期,  \n");
                sb.AppendFormat("       交易類別,  \n");
                sb.AppendFormat("       機台編號,  \n");
                sb.AppendFormat("       交易序號,  \n");
                sb.AppendFormat("       發票號碼,  \n");
                sb.AppendFormat("       帳單號碼_條碼一,  \n");
                sb.AppendFormat("       門號_條碼二,  \n");
                sb.AppendFormat("       商品料號,  \n");
                sb.AppendFormat("       商品名稱, \n");
                sb.AppendFormat("       條碼三,  \n");
                sb.AppendFormat("       數量,  \n");
                sb.AppendFormat("       未稅金額,  \n");
                sb.AppendFormat("       稅額,  \n");
                sb.AppendFormat("       銷售金額,  \n");
                sb.AppendFormat("       促銷代碼,  \n");
                sb.AppendFormat("       備註,  \n");
                sb.AppendFormat("       POSUUID_MASTER  \n");
                sb.AppendFormat("  FROM (  \n");
                sb.AppendFormat("          \n");
                sb.AppendFormat("        SELECT DISTINCT *  \n");
                sb.AppendFormat("          FROM RPL046_HEADER RH1  \n");
                sb.AppendFormat("         ORDER BY SALE_TYPE,TRADE_DATE,交易序號, POSUUID_MASTER, ORDER_INDEX)  \n");

                #region WHERE
                #region 交易金額
                sb.Append(" WHERE 1=1 ");
                if (!string.IsNullOrEmpty(TOTAL_AMOUNT_S))
                {
                    sb.Append(" AND 銷售金額 >= ");
                    sb.Append(Advtek.Utility.OracleDBUtil.SqlStr(TOTAL_AMOUNT_S.Trim()));
                    sb.AppendLine();
                }
                if (!string.IsNullOrEmpty(TOTAL_AMOUNT_E))
                {
                    sb.Append(" AND 銷售金額 <= ");
                    sb.Append(Advtek.Utility.OracleDBUtil.SqlStr(TOTAL_AMOUNT_E.Trim()));
                    sb.AppendLine();
                }
                #endregion
                #endregion


                #endregion

                DataTable dt = Advtek.Utility.OracleDBUtil.GetDataSet(objConn, sb.ToString()).Tables[0];

                return dt;
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
        }

        /*Author : 蔡坤霖
           Date : 2011 / 02 / 25
           Description: RPL046 門市銷售日報表 明細表
          */
        /// <summary>
        /// RPL046 門市銷售日報表 明細表
        /// </summary>
        /// <param name="STORE_NO_S"></param>
        /// <param name="STORE_NO_E"></param>
        /// <param name="TRADE_DATE_S"></param>
        /// <param name="TRADE_DATE_E"></param>
        /// <param name="MACHINE_ID"></param>
        /// <param name="MSISDN"></param>
        /// <param name="PRODTYPENO_S"></param>
        /// <param name="PRODTYPENO_E"></param>
        /// <param name="PRODNO_S"></param>
        /// <param name="PRODNO_E"></param>
        /// <param name="INVOICE_NO_S"></param>
        /// <param name="INVOICE_NO_E"></param>
        /// <param name="TOTAL_AMOUNT_S"></param>
        /// <param name="TOTAL_AMOUNT_E"></param>
        /// <param name="SALE_PERSON_S"></param>
        /// <param name="SALE_PERSON_E"></param>
        /// <returns></returns>
        public DataTable RPL046_DETAIL_TOTAL(string STORE_NO_S, string STORE_NO_E, string TRADE_DATE_S, string TRADE_DATE_E,
                                string MACHINE_ID, string MSISDN, string PRODTYPENO_S, string PRODTYPENO_E,
                                string PRODNO_S, string PRODNO_E, string INVOICE_NO_S, string INVOICE_NO_E,
                                string TOTAL_AMOUNT_S, string TOTAL_AMOUNT_E, string SALE_PERSON_S, string SALE_PERSON_E)
        {
            OracleConnection objConn = null;

            try
            {
                objConn = OracleDBUtil.GetConnection();
                System.Text.StringBuilder sb = new System.Text.StringBuilder();

                #region RPL046 明細表 SQL
                sb.AppendFormat("WITH   \n");
                sb.AppendFormat("RPL046_HEADER AS (   \n");
                sb.AppendFormat("SELECT SH.POSUUID_MASTER,  \n");
                sb.AppendFormat("       '1' ORDER_INDEX,  \n");
                sb.AppendFormat("       SH.SALE_TYPE,  \n");
                sb.AppendFormat("       SH.TRADE_DATE,  \n");
                sb.AppendFormat("       SH.STORE_NO 門市編號,  \n");
                sb.AppendFormat("       TO_CHAR(SH.TRADE_DATE,'YYYY/MM/DD') 交易日期,  \n");
                sb.AppendFormat("       SH.MACHINE_ID 機台編號,  \n");
                sb.AppendFormat("       SD.MSISDN 門號,  \n");
                sb.AppendFormat("       PROD.PRODTYPENO 商品類別,  \n");
                sb.AppendFormat("       SD.PRODNO 商品料號,  \n");
                //sb.AppendFormat("       VOUCHER.INVOICE_NO 發票號碼,  \n");
                sb.AppendFormat("       FN_GET_INVOICE_DATA(SH.POSUUID_MASTER) 發票號碼,  \n");
                sb.AppendFormat("       VOUCHER.TOTAL_AMOUNT 交易金額,  \n");
                sb.AppendFormat("       SH.SALE_PERSON 員工編號,  \n");
                sb.AppendFormat("       DECODE(SH.SALE_TYPE, 1, '銷售交易', 2, '帳單代收') AS 交易類別,  \n");
                sb.AppendFormat("       SH.SALE_NO 交易序號,  \n");
                sb.AppendFormat("       SD.BARCODE1 帳單號碼_條碼一,  \n");
                sb.AppendFormat("       SD.MSISDN || SD.BARCODE2 門號_條碼二,  \n");
                sb.AppendFormat("       PROD.PRODNAME 商品名稱, \n");
                sb.AppendFormat("       SD.BARCODE3 條碼三,  \n");
                sb.AppendFormat("       SD.QUANTITY 數量,  \n");
                sb.AppendFormat("       TO_CHAR(SD.BEFORE_TAX) 未稅金額,  \n");
                sb.AppendFormat("       SD.TAX 稅額,  \n");
                sb.AppendFormat("       TO_CHAR(SD.TOTAL_AMOUNT) 銷售金額,  \n");
                sb.AppendFormat("       SD.PROMOTION_CODE 促銷代碼,  \n");
                sb.AppendFormat("       SH.REMARK 備註  \n");
                sb.AppendFormat("  FROM SALE_HEAD SH,  \n");
                sb.AppendFormat("       SALE_DETAIL SD,  \n");
                sb.AppendFormat("       PRODUCT PROD,  \n");
                sb.AppendFormat("       (SELECT SH.POSUUID_MASTER, IH.ID, IH.INVOICE_NO, IH.TOTAL_AMOUNT, II.PRODNO, II.SALE_ITEM_ID  \n");
                sb.AppendFormat("          FROM SALE_HEAD        SH,  \n");
                sb.AppendFormat("               VOUCHER_RELATION VR,  \n");
                sb.AppendFormat("               INVOICE_HEAD     IH,  \n");
                sb.AppendFormat("               INVOICE_ITEM     II   \n"); 
                sb.AppendFormat("         WHERE SH.POSUUID_MASTER = VR.SALE_HEAD_ID  \n");
                sb.AppendFormat("           AND VR.VOUCHER_ID = IH.ID  \n");
                sb.AppendFormat("           AND IH.ID = II.INVOICE_HEAD_ID  \n");
                //sb.AppendFormat("           AND NVL(IH.IS_INVALID, 'N') = 'N'  \n");
                sb.AppendFormat("        UNION ALL  \n");
                sb.AppendFormat("        SELECT SH.POSUUID_MASTER, MIH.ID, MIH.INVOICE_NO, MIH.TOTAL_AMOUNT, MII.PRODNO, MII.SALE_ITEM_ID  \n");
                sb.AppendFormat("          FROM SALE_HEAD           SH,  \n");
                sb.AppendFormat("               VOUCHER_RELATION    VR,  \n");
                sb.AppendFormat("               MANUAL_INVOICE_HEAD MIH, \n");
                sb.AppendFormat("               MANUAL_INVOICE_ITEM MII  \n"); 
                sb.AppendFormat("         WHERE SH.POSUUID_MASTER = VR.SALE_HEAD_ID  \n");
                sb.AppendFormat("           AND VR.VOUCHER_ID = MIH.ID  \n");
                sb.AppendFormat("           AND MIH.ID = MII.MANUAL_INVOICE_HEAD_ID  \n");
                //sb.AppendFormat("           AND NVL(MIH.IS_INVALID, 'N') = 'N'  \n");
                sb.AppendFormat("        UNION ALL  \n");
                sb.AppendFormat("        SELECT SH.POSUUID_MASTER,  \n");
                sb.AppendFormat("               RH.ID,  \n");
                sb.AppendFormat("               RH.RECEIPT_NO AS INVOICE_NO,  \n");
                sb.AppendFormat("               RH.TOTAL_AMOUNT,   \n");
                sb.AppendFormat("               RI.PRODNO,         \n");
                sb.AppendFormat("               RI.SALE_ITEM_ID    \n");
                sb.AppendFormat("          FROM SALE_HEAD SH, VOUCHER_RELATION VR, RECEIPT_HEAD RH, RECEIPT_ITEM RI  \n");
                sb.AppendFormat("         WHERE SH.POSUUID_MASTER = VR.SALE_HEAD_ID  \n");
                sb.AppendFormat("           AND VR.VOUCHER_ID = RH.ID  \n");
                sb.AppendFormat("           AND RH.ID = RI.RECEIPT_HEAD_ID  \n");
                //sb.AppendFormat("           AND NVL(RH.IS_INVALID, 'N') = 'N') VOUCHER  \n");
                sb.AppendFormat("            ) VOUCHER  \n");
                sb.AppendFormat(" WHERE SH.POSUUID_MASTER = SD.POSUUID_MASTER  \n");
                sb.AppendFormat("   AND SH.POSUUID_MASTER = VOUCHER.POSUUID_MASTER  \n");
                sb.AppendFormat("   AND SD.PRODNO = PROD.PRODNO  \n");
                sb.AppendFormat("   AND SD.PRODNO = VOUCHER.PRODNO  \n");
                sb.AppendFormat("   AND SD.ID = VOUCHER.SALE_ITEM_ID  \n");
                //sb.AppendFormat("   AND SH.SALE_STATUS = '2'  \n");

                #region WHERE
                #region 門市編號
                if (!string.IsNullOrEmpty(STORE_NO_S))
                {
                    sb.Append(" AND SH.STORE_NO >= ");
                    sb.Append(Advtek.Utility.OracleDBUtil.SqlStr(STORE_NO_S.Trim()));
                    sb.AppendLine();
                }
                if (!string.IsNullOrEmpty(STORE_NO_E))
                {
                    sb.Append(" AND SH.STORE_NO <= ");
                    sb.Append(Advtek.Utility.OracleDBUtil.SqlStr(STORE_NO_E.Trim()));
                    sb.AppendLine();
                }
                #endregion
                #region 交易日期
                if (!string.IsNullOrEmpty(TRADE_DATE_S))
                {
                    sb.Append(" AND TRUNC(SH.TRADE_DATE) >= ");
                    sb.Append(Advtek.Utility.OracleDBUtil.DateStr(TRADE_DATE_S.Trim()));
                    sb.AppendLine();
                }
                if (!string.IsNullOrEmpty(TRADE_DATE_E))
                {
                    sb.Append(" AND TRUNC(SH.TRADE_DATE) <= ");
                    sb.Append(Advtek.Utility.OracleDBUtil.DateStr(TRADE_DATE_E.Trim()));
                    sb.AppendLine();
                }
                #endregion
                #region 機台編號
                if (!string.IsNullOrEmpty(MACHINE_ID) && MACHINE_ID.ToUpper().Trim() != "ALL")
                {
                    sb.Append(" AND SH.MACHINE_ID = ");
                    sb.Append(Advtek.Utility.OracleDBUtil.SqlStr(MACHINE_ID.Trim()));
                    sb.AppendLine();
                }
                else if (string.IsNullOrEmpty(MACHINE_ID) && MACHINE_ID.ToUpper().Trim() == "ALL")
                {
                    sb.Append(" AND SH.MACHINE_ID IN (SELECT HOST_NO FROM STORE_TERMINATING_MACHINE WHERE 1=1 ");

                    if (!string.IsNullOrEmpty(STORE_NO_S))
                    {
                        sb.Append(" AND STORE_NO >= ");
                        sb.Append(Advtek.Utility.OracleDBUtil.SqlStr(STORE_NO_S.Trim()));
                    }
                    if (!string.IsNullOrEmpty(STORE_NO_E))
                    {
                        sb.Append(" AND STORE_NO <= ");
                        sb.Append(Advtek.Utility.OracleDBUtil.SqlStr(STORE_NO_E.Trim()));
                    }

                    sb.Append(" ) ");
                    sb.AppendLine();
                }
                #endregion
                #region 門號
                if (!string.IsNullOrEmpty(MSISDN))
                {
                    sb.Append(" AND SD.MSISDN = ");
                    sb.Append(Advtek.Utility.OracleDBUtil.SqlStr(MSISDN.Trim()));
                    sb.AppendLine();
                }
                #endregion
                #region 商品類別
                if (!string.IsNullOrEmpty(PRODTYPENO_S))
                {
                    sb.Append(" AND PROD.PRODTYPENO >= ");
                    sb.Append(Advtek.Utility.OracleDBUtil.SqlStr(PRODTYPENO_S.Trim()));
                    sb.AppendLine();
                }
                if (!string.IsNullOrEmpty(PRODTYPENO_E))
                {
                    sb.Append(" AND PROD.PRODTYPENO <= ");
                    sb.Append(Advtek.Utility.OracleDBUtil.SqlStr(PRODTYPENO_E.Trim()));
                    sb.AppendLine();
                }
                #endregion
                #region 商品料號
                if (!string.IsNullOrEmpty(PRODNO_S))
                {
                    sb.Append(" AND SD.PRODNO >= ");
                    sb.Append(Advtek.Utility.OracleDBUtil.SqlStr(PRODNO_S.Trim()));
                    sb.AppendLine();
                }
                if (!string.IsNullOrEmpty(PRODNO_E))
                {
                    sb.Append(" AND SD.PRODNO <= ");
                    sb.Append(Advtek.Utility.OracleDBUtil.SqlStr(PRODNO_E.Trim()));
                    sb.AppendLine();
                }
                #endregion
                #region 發票號碼
                if (!string.IsNullOrEmpty(INVOICE_NO_S))
                {
                    sb.Append(" AND VOUCHER.INVOICE_NO >= ");
                    sb.Append(Advtek.Utility.OracleDBUtil.SqlStr(INVOICE_NO_S.Trim()));
                    sb.AppendLine();
                }
                if (!string.IsNullOrEmpty(INVOICE_NO_E))
                {
                    sb.Append(" AND VOUCHER.INVOICE_NO <= ");
                    sb.Append(Advtek.Utility.OracleDBUtil.SqlStr(INVOICE_NO_E.Trim()));
                    sb.AppendLine();
                }
                #endregion
                #region 交易金額
                if (!string.IsNullOrEmpty(TOTAL_AMOUNT_S))
                {
                    sb.Append(" AND VOUCHER.TOTAL_AMOUNT >= ");
                    sb.Append(Advtek.Utility.OracleDBUtil.SqlStr(TOTAL_AMOUNT_S.Trim()));
                    sb.AppendLine();
                }
                if (!string.IsNullOrEmpty(TOTAL_AMOUNT_E))
                {
                    sb.Append(" AND VOUCHER.TOTAL_AMOUNT <= ");
                    sb.Append(Advtek.Utility.OracleDBUtil.SqlStr(TOTAL_AMOUNT_E.Trim()));
                    sb.AppendLine();
                }
                #endregion
                #region 員工編號
                if (!string.IsNullOrEmpty(SALE_PERSON_S))
                {
                    sb.Append(" AND SH.SALE_PERSON >= ");
                    sb.Append(Advtek.Utility.OracleDBUtil.SqlStr(SALE_PERSON_S.Trim()));
                    sb.AppendLine();
                }
                if (!string.IsNullOrEmpty(SALE_PERSON_E))
                {
                    sb.Append(" AND SH.SALE_PERSON <= ");
                    sb.Append(Advtek.Utility.OracleDBUtil.SqlStr(SALE_PERSON_E.Trim()));
                    sb.AppendLine();
                }
                #endregion
                #endregion

                sb.AppendFormat(")   \n");
                sb.AppendFormat(",   \n");
                sb.AppendFormat("RPL046_BODY AS (   \n");
                sb.AppendFormat("SELECT R.POSUUID_MASTER,  \n");
                sb.AppendFormat("       R.SALE_TYPE,  \n");
                sb.AppendFormat("       R.TRADE_DATE,  \n");
                sb.AppendFormat("       '收款方式' AS PAID_MODE,  \n");
                sb.AppendFormat("       '現金' AS PAID_MODE_1,  \n");
                sb.AppendFormat("       R.CASH_AMT,  \n");
                sb.AppendFormat("       '分期付款' AS PAID_MODE_2,  \n");
                sb.AppendFormat("       R.Installment_AMT,  \n");
                sb.AppendFormat("       '禮券' AS PAID_MODE_3,  \n");
                sb.AppendFormat("       R.GIFT_AMT,  \n");
                sb.AppendFormat("       '快樂購累點' AS PAID_MODE_4,  \n");
                sb.AppendFormat("       NVL(0, 0) AS HG_POINTS1,  \n");
                sb.AppendFormat("       '快樂購兌點' AS PAID_MODE_5,  \n");
                sb.AppendFormat("       NVL(0, 0) AS HG_POINTS2,  \n");
                sb.AppendFormat("       '快樂購金額' AS PAID_MODE_6,  \n");
                sb.AppendFormat("       R.HG_AMT,  \n");
                sb.AppendFormat("       '信用卡' AS PAID_MODE_7,  \n");
                sb.AppendFormat("       (R.CARD_AMT + R.CARD_AMT1) AS CARD_AMT,  \n");
                sb.AppendFormat("       '信用卡號' AS PAID_MODE_8,  \n");
                sb.AppendFormat("       P.CREDIT_CARD_NO AS CREDIT_CARD_NO,  \n");
                sb.AppendFormat("       '金融卡' AS PAID_MODE_9,  \n");
                sb.AppendFormat("       R.DebitCard_AMT,  \n");
                sb.AppendFormat("       '金融卡號' AS PAID_MODE_10,  \n");
                sb.AppendFormat("       P.DEBIT_CARD_NO AS DEBIT_CARD_NO  \n");
                sb.AppendFormat("  FROM (SELECT SH.POSUUID_MASTER,  \n");
                sb.AppendFormat("               SH.SALE_TYPE,  \n");
                sb.AppendFormat("               SH.TRADE_DATE,  \n");
                sb.AppendFormat("               NVL(SUM(DECODE(PD.PAID_MODE, 1, PD.PAID_AMOUNT)), 0) AS CASH_AMT,  \n");
                sb.AppendFormat("               NVL(SUM(DECODE(PD.PAID_MODE, 4, PD.PAID_AMOUNT)), 0) AS Installment_AMT,  \n");
                sb.AppendFormat("               NVL(SUM(DECODE(PD.PAID_MODE, 5, PD.PAID_AMOUNT)), 0) AS GIFT_AMT,  \n");
                sb.AppendFormat("               NVL(0, 0) AS HG_POINTS1,  \n");
                sb.AppendFormat("               NVL(0, 0) AS HG_POINTS2,  \n");
                sb.AppendFormat("               NVL(SUM(DECODE(PD.PAID_MODE, 7, PD.PAID_AMOUNT)), 0) AS HG_AMT,  \n");
                sb.AppendFormat("               NVL(SUM(DECODE(PD.PAID_MODE, 2, PD.PAID_AMOUNT)), 0) AS CARD_AMT,  \n");
                sb.AppendFormat("               NVL(SUM(DECODE(PD.PAID_MODE, 3, PD.PAID_AMOUNT)), 0) AS CARD_AMT1,  \n");
                sb.AppendFormat("               NVL(SUM(DECODE(PD.PAID_MODE, 6, PD.PAID_AMOUNT)), 0) AS DebitCard_AMT  \n");
                sb.AppendFormat("          FROM SALE_HEAD SH, PAID_DETAIL PD  \n");
                sb.AppendFormat("         WHERE SH.POSUUID_MASTER = PD.POSUUID_MASTER  \n");
                //sb.AppendFormat("           AND SH.SALE_STATUS = '2'  \n");
                sb.AppendFormat("         GROUP BY SH.POSUUID_MASTER, SH.SALE_TYPE,SH.TRADE_DATE) R,  \n");
                sb.AppendFormat("       (SELECT SH.POSUUID_MASTER,  \n");
                sb.AppendFormat("               PD.CREDIT_CARD_NO AS CREDIT_CARD_NO,  \n");
                sb.AppendFormat("               PD.DEBIT_CARD_NO AS DEBIT_CARD_NO  \n");
                sb.AppendFormat("          FROM SALE_HEAD SH, PAID_DETAIL PD  \n");
                sb.AppendFormat("         WHERE SH.POSUUID_MASTER = PD.POSUUID_MASTER  \n");
                sb.AppendFormat("           AND (PD.CREDIT_CARD_NO IS NOT NULL OR  \n");
                sb.AppendFormat("               PD.DEBIT_CARD_NO IS NOT NULL)  \n");
                sb.AppendFormat("         GROUP BY SH.POSUUID_MASTER, PD.CREDIT_CARD_NO, PD.DEBIT_CARD_NO) P  \n");
                sb.AppendFormat(" WHERE R.POSUUID_MASTER = P.POSUUID_MASTER(+)  \n");
                sb.AppendFormat(")   \n");
                sb.AppendFormat(",   \n");
                sb.AppendFormat("RPL046_FOOTER AS (   \n");
                sb.AppendFormat("SELECT SH.SALE_TOTAL_AMOUNT,  \n");
                sb.AppendFormat("       VOUCHER.TOTAL_AMOUNT,  \n");
                sb.AppendFormat("       VOUCHER.TAX,  \n");
                sb.AppendFormat("       NVL(VOUCHER.TOTAL_AMOUNT, 0) - NVL(VOUCHER.TAX, 0) AS BEFTAX,  \n");
                sb.AppendFormat("       SH.SALE_PERSON,  \n");
                sb.AppendFormat("       SH.UNI_NO,  \n");
                sb.AppendFormat("       SH.POSUUID_MASTER,  \n");
                sb.AppendFormat("       SH.SALE_TYPE,  \n");
                sb.AppendFormat("       SH.TRADE_DATE  \n");
                sb.AppendFormat("  FROM SALE_HEAD SH,  \n");
                sb.AppendFormat("       SALE_DETAIL SD,  \n");
                sb.AppendFormat("       PRODUCT PROD,  \n");
                sb.AppendFormat("       (SELECT SH.POSUUID_MASTER,  \n");
                sb.AppendFormat("               IH.ID,  \n");
                sb.AppendFormat("               IH.INVOICE_NO,  \n");
                sb.AppendFormat("               IH.TOTAL_AMOUNT,  \n");
                sb.AppendFormat("               IH.TAX,  \n");
                sb.AppendFormat("               SH.SALE_TYPE,   \n");
                sb.AppendFormat("               II.PRODNO,      \n");
                sb.AppendFormat("               II.SALE_ITEM_ID       \n"); 
                sb.AppendFormat("          FROM SALE_HEAD        SH,  \n");
                sb.AppendFormat("               VOUCHER_RELATION VR,  \n");
                sb.AppendFormat("               INVOICE_HEAD     IH,   \n");
                sb.AppendFormat("               INVOICE_ITEM     II   \n"); 
                sb.AppendFormat("         WHERE SH.POSUUID_MASTER = VR.SALE_HEAD_ID  \n");
                sb.AppendFormat("           AND VR.VOUCHER_ID = IH.ID  \n");
                sb.AppendFormat("           AND IH.ID = II.INVOICE_HEAD_ID  \n");
                //sb.AppendFormat("           AND nvl(IH.IS_INVALID, 'N') = 'N'  \n");
                sb.AppendFormat("        UNION ALL  \n");
                sb.AppendFormat("        SELECT SH.POSUUID_MASTER,  \n");
                sb.AppendFormat("               MIH.ID,  \n");
                sb.AppendFormat("               MIH.INVOICE_NO,  \n");
                sb.AppendFormat("               MIH.TOTAL_AMOUNT,  \n");
                sb.AppendFormat("               MIH.TAX,  \n");
                sb.AppendFormat("               SH.SALE_TYPE ,  \n");
                sb.AppendFormat("               MII.PRODNO,     \n"); 
                sb.AppendFormat("               MII.SALE_ITEM_ID         \n"); 
                sb.AppendFormat("          FROM SALE_HEAD           SH,  \n");
                sb.AppendFormat("               VOUCHER_RELATION    VR,  \n");
                sb.AppendFormat("               MANUAL_INVOICE_HEAD MIH, \n");
                sb.AppendFormat("               MANUAL_INVOICE_ITEM MII  \n"); 
                sb.AppendFormat("         WHERE SH.POSUUID_MASTER = VR.SALE_HEAD_ID  \n");
                sb.AppendFormat("           AND VR.VOUCHER_ID = MIH.ID  \n");
                sb.AppendFormat("           AND MIH.ID = MII.MANUAL_INVOICE_HEAD_ID  \n");
                //sb.AppendFormat("           AND nvl(MIH.IS_INVALID, 'N') = 'N'  \n");
                sb.AppendFormat("        UNION ALL  \n");
                sb.AppendFormat("        SELECT SH.POSUUID_MASTER,  \n");
                sb.AppendFormat("               RH.ID,  \n");
                sb.AppendFormat("               RH.RECEIPT_NO AS INVOICE_NO,  \n");
                sb.AppendFormat("               RH.TOTAL_AMOUNT,  \n");
                sb.AppendFormat("               RH.TAX,  \n");
                sb.AppendFormat("               SH.SALE_TYPE, \n");
                sb.AppendFormat("               RI.PRODNO,    \n");
                sb.AppendFormat("               RI.SALE_ITEM_ID     \n");
                sb.AppendFormat("          FROM SALE_HEAD SH, VOUCHER_RELATION VR, RECEIPT_HEAD RH, RECEIPT_ITEM RI   \n");
                sb.AppendFormat("         WHERE SH.POSUUID_MASTER = VR.SALE_HEAD_ID  \n");
                sb.AppendFormat("           AND VR.VOUCHER_ID = RH.ID  \n");
                sb.AppendFormat("           AND RH.ID = RI.RECEIPT_HEAD_ID  \n");
                //sb.AppendFormat("           AND nvl(RH.IS_INVALID, 'N') = 'N') VOUCHER  \n");
                sb.AppendFormat("            ) VOUCHER  \n");
                sb.AppendFormat(" WHERE SH.POSUUID_MASTER = SD.POSUUID_MASTER  \n");
                sb.AppendFormat("   AND SH.POSUUID_MASTER = VOUCHER.POSUUID_MASTER  \n");
                sb.AppendFormat("   AND SD.PRODNO = PROD.PRODNO  \n");
                sb.AppendFormat("   AND SD.PRODNO = VOUCHER.PRODNO   \n");
                //sb.AppendFormat("   AND SH.SALE_STATUS = '2'  \n");
                sb.AppendFormat(" ORDER BY SH.SALE_TYPE, SH.MACHINE_ID, SH.SALE_NO  \n");
                sb.AppendFormat(")   \n");
                sb.AppendFormat("  \n");
                sb.AppendFormat("SELECT 交易日期,  \n");
                sb.AppendFormat("       交易類別,  \n");
                sb.AppendFormat("       機台編號,  \n");
                sb.AppendFormat("       交易序號,  \n");
                sb.AppendFormat("       發票號碼,  \n");
                sb.AppendFormat("       帳單號碼_條碼一,  \n");
                sb.AppendFormat("       門號_條碼二,  \n");
                sb.AppendFormat("       商品料號,  \n");
                sb.AppendFormat("       商品名稱, \n");
                sb.AppendFormat("       條碼三,  \n");
                sb.AppendFormat("       數量,  \n");
                sb.AppendFormat("       未稅金額,  \n");
                sb.AppendFormat("       稅額,  \n");
                sb.AppendFormat("       銷售金額,  \n");
                sb.AppendFormat("       促銷代碼,  \n");
                sb.AppendFormat("       備註,  \n");
                sb.AppendFormat("       POSUUID_MASTER  \n");
                sb.AppendFormat("  FROM (  \n");
                sb.AppendFormat("          \n");
                //sb.AppendFormat("        SELECT DISTINCT *  \n");
                sb.AppendFormat("        SELECT *  \n");
                sb.AppendFormat("          FROM RPL046_HEADER RH1  \n");
                sb.AppendFormat("        UNION ALL  \n");
                sb.AppendFormat("        SELECT POSUUID_MASTER,  \n");
                sb.AppendFormat("               '2' ORDER_INDEX,  \n");
                sb.AppendFormat("               SALE_TYPE,  \n");
                sb.AppendFormat("               TRADE_DATE,  \n");
                sb.AppendFormat("               NULL 門市編號,  \n");
                sb.AppendFormat("               NULL 交易日期,  \n");
                sb.AppendFormat("               PAID_MODE 機台編號,  \n");
                sb.AppendFormat("               NULL 門號,  \n");
                sb.AppendFormat("               NULL 商品類別,  \n");
                sb.AppendFormat("               NULL 商品料號,  \n");
                sb.AppendFormat("               PAID_MODE_2 || ' ' || Installment_AMT 發票號碼,  \n");
                sb.AppendFormat("               NULL 交易金額,  \n");
                sb.AppendFormat("               NULL 員工編號,  \n");
                sb.AppendFormat("               NULL 交易類別,  \n");
                sb.AppendFormat("               PAID_MODE_1 || ' ' || CASH_AMT 交易序號,  \n");
                sb.AppendFormat("               NULL 帳單號碼_條碼一,  \n");
                sb.AppendFormat("               NULL 門號_條碼二,  \n");
                sb.AppendFormat("               PAID_MODE_3 || ' ' || GIFT_AMT 商品名稱, \n");
                sb.AppendFormat("               NULL 條碼三,  \n");
                sb.AppendFormat("               NULL 數量,  \n");
                sb.AppendFormat("               PAID_MODE_4 || ' ' || HG_POINTS1 未稅金額,  \n");
                sb.AppendFormat("               NULL 稅額,  \n");
                sb.AppendFormat("               PAID_MODE_5 || ' ' || HG_POINTS2 銷售金額,  \n");
                sb.AppendFormat("               NULL 促銷代碼,  \n");
                sb.AppendFormat("               PAID_MODE_6 || ' ' || HG_AMT 備註  \n");
                sb.AppendFormat("          \n");
                sb.AppendFormat("          FROM RPL046_BODY RB  \n");
                sb.AppendFormat("         WHERE RB.POSUUID_MASTER IN  \n");
                sb.AppendFormat("               (SELECT POSUUID_MASTER FROM RPL046_HEADER)  \n");
                sb.AppendFormat("        UNION ALL  \n");
                sb.AppendFormat("        SELECT POSUUID_MASTER,  \n");
                sb.AppendFormat("               '3' ORDER_INDEX,  \n");
                sb.AppendFormat("               SALE_TYPE,  \n");
                sb.AppendFormat("               TRADE_DATE,  \n");
                sb.AppendFormat("               NULL 門市編號,  \n");
                sb.AppendFormat("               NULL 交易日期,  \n");
                sb.AppendFormat("               NULL 機台編號,  \n");
                sb.AppendFormat("               NULL 門號,  \n");
                sb.AppendFormat("               NULL 商品類別,  \n");
                sb.AppendFormat("               NULL 商品料號,  \n");
                sb.AppendFormat("               PAID_MODE_8 || ' ' || CREDIT_CARD_NO 發票號碼,  \n");
                sb.AppendFormat("               NULL 交易金額,  \n");
                sb.AppendFormat("               NULL 員工編號,  \n");
                sb.AppendFormat("               NULL 交易類別,  \n");
                sb.AppendFormat("               PAID_MODE_7 || ' ' || CARD_AMT 交易序號,  \n");
                sb.AppendFormat("               NULL 帳單號碼_條碼一,  \n");
                sb.AppendFormat("               NULL 門號_條碼二,  \n");
                sb.AppendFormat("               NULL 商品名稱, \n");
                sb.AppendFormat("               NULL 條碼三,  \n");
                sb.AppendFormat("               NULL 數量,  \n");
                sb.AppendFormat("               PAID_MODE_9 || ' ' || DebitCard_AMT 未稅金額,  \n");
                sb.AppendFormat("               NULL 稅額,  \n");
                sb.AppendFormat("               PAID_MODE_10 || ' ' || DEBIT_CARD_NO 銷售金額,  \n");
                sb.AppendFormat("               NULL 促銷代碼,  \n");
                sb.AppendFormat("               NULL 備註  \n");
                sb.AppendFormat("          \n");
                sb.AppendFormat("          FROM RPL046_BODY RB  \n");
                sb.AppendFormat("         WHERE RB.POSUUID_MASTER IN  \n");
                sb.AppendFormat("               (SELECT POSUUID_MASTER FROM RPL046_HEADER)  \n");
                sb.AppendFormat("        UNION ALL  \n");
                sb.AppendFormat("        SELECT DISTINCT POSUUID_MASTER,  \n");
                sb.AppendFormat("                        '4' ORDER_INDEX,  \n");
                sb.AppendFormat("                        SALE_TYPE,  \n");
                sb.AppendFormat("                        TRADE_DATE,  \n");
                sb.AppendFormat("                        NULL 門市編號,  \n");
                sb.AppendFormat("                        NULL 交易日期,  \n");
                sb.AppendFormat("                        '交易總額' 機台編號,  \n");
                sb.AppendFormat("                        NULL 門號,  \n");
                sb.AppendFormat("                        NULL 商品類別,  \n");
                sb.AppendFormat("                        NULL 商品料號,  \n");
                sb.AppendFormat("                        '發票金額' || ' ' || TOTAL_AMOUNT 發票號碼,  \n");
                sb.AppendFormat("                        NULL 交易金額,  \n");
                sb.AppendFormat("                        NULL 員工編號,  \n");
                sb.AppendFormat("                        NULL 交易類別,  \n");
                sb.AppendFormat("                        TO_CHAR(SALE_TOTAL_AMOUNT) 交易序號,  \n");
                sb.AppendFormat("                        NULL 帳單號碼_條碼一,  \n");
                sb.AppendFormat("                        NULL 門號_條碼二,  \n");
                sb.AppendFormat("                        '稅款' || ' ' || TAX 商品名稱, \n");
                sb.AppendFormat("                        NULL 條碼三,  \n");
                sb.AppendFormat("                        NULL 數量,  \n");
                sb.AppendFormat("                        '未稅金額' || ' ' || BEFTAX 未稅金額,  \n");
                sb.AppendFormat("                        NULL 稅額,  \n");
                sb.AppendFormat("                        '銷售人員' || ' ' || SALE_PERSON 銷售金額,  \n");
                sb.AppendFormat("                        NULL 促銷代碼,  \n");
                sb.AppendFormat("                        '統一編號' || ' ' || UNI_NO 備註  \n");
                sb.AppendFormat("          FROM RPL046_FOOTER RF  \n");
                sb.AppendFormat("         WHERE RF.POSUUID_MASTER IN  \n");
                sb.AppendFormat("               (SELECT POSUUID_MASTER FROM RPL046_HEADER)  \n");
                sb.AppendFormat("         ORDER BY SALE_TYPE,TRADE_DATE, POSUUID_MASTER, ORDER_INDEX)  \n");

                #endregion

                DataTable dt = Advtek.Utility.OracleDBUtil.GetDataSet(objConn, sb.ToString()).Tables[0];

                return dt;
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
        }

        /*Author : 蔡坤霖
      Date : 2011 / 03 / 05
      Description: RPL046 門市銷售日報表 總表 門市總計
     */
        /// <summary>
        /// RPL046 門市銷售日報表 總表 門市總計
        /// </summary>
        /// <param name="STORE_NO_S"></param>
        /// <param name="STORE_NO_E"></param>
        /// <param name="TRADE_DATE_S"></param>
        /// <param name="TRADE_DATE_E"></param>
        /// <param name="MACHINE_ID"></param>
        /// <param name="MSISDN"></param>
        /// <param name="PRODTYPENO_S"></param>
        /// <param name="PRODTYPENO_E"></param>
        /// <param name="PRODNO_S"></param>
        /// <param name="PRODNO_E"></param>
        /// <param name="INVOICE_NO_S"></param>
        /// <param name="INVOICE_NO_E"></param>
        /// <param name="TOTAL_AMOUNT_S"></param>
        /// <param name="TOTAL_AMOUNT_E"></param>
        /// <param name="SALE_PERSON_S"></param>
        /// <param name="SALE_PERSON_E"></param>
        /// <returns></returns>
        public DataTable RPL046_SUM_TOTAL(string STORE_NO_S, string STORE_NO_E, string TRADE_DATE_S, string TRADE_DATE_E,
                                          string MACHINE_ID, string MSISDN, string PRODTYPENO_S, string PRODTYPENO_E,
                                          string PRODNO_S, string PRODNO_E, string INVOICE_NO_S, string INVOICE_NO_E,
                                          string TOTAL_AMOUNT_S, string TOTAL_AMOUNT_E, string SALE_PERSON_S, string SALE_PERSON_E,
                                          Boolean isHQ
                                         )
        {
            OracleConnection objConn = null;

            try
            {
                objConn = OracleDBUtil.GetConnection();
                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                System.Text.StringBuilder sbSQL = new System.Text.StringBuilder();

                #region -ETC聯名卡儲值金-
                Int64 CASH_AMT = 0;
                Int64 CARD_AMT = 0;
                Int64 Installment_AMT = 0;
                Int64 GIFT_AMT = 0;
                Int64 DebitCard_AMT = 0;
                Int64 HG_AMT = 0;

                #region -ETC聯名卡儲值金-
                sbSQL.AppendFormat("  --ETC聯名卡儲值金    \n");
                sbSQL.AppendFormat("  SELECT 'ETC聯名卡儲值金' AS TITLE ,SH.STORE_NO ,SH.MACHINE_ID        \n");
                sbSQL.AppendFormat("        ,SUM(DECODE(PAID_MODE,1,TOTAL_AMOUNT,0)) AS CASH_AMT           \n");
                sbSQL.AppendFormat("        ,SUM(DECODE(PAID_MODE,2,TOTAL_AMOUNT,0) + DECODE(PAID_MODE,3,TOTAL_AMOUNT,0)) AS CARD_AMT      \n");
                sbSQL.AppendFormat("        ,SUM(DECODE(PAID_MODE,4,TOTAL_AMOUNT,0)) AS Installment_AMT    \n");
                sbSQL.AppendFormat("        ,SUM(DECODE(PAID_MODE,5,TOTAL_AMOUNT,0)) AS GIFT_AMT           \n");
                sbSQL.AppendFormat("        ,SUM(DECODE(PAID_MODE,6,TOTAL_AMOUNT,0)) AS DebitCard_AMT      \n");
                sbSQL.AppendFormat("        ,SUM(DECODE(PAID_MODE,7,TOTAL_AMOUNT,0)) AS HG_AMT             \n");
                sbSQL.AppendFormat("    FROM (                         \n");
                sbSQL.AppendFormat("  SELECT DISTINCT SH.TRADE_DATE    \n");
                sbSQL.AppendFormat("        ,SH.STORE_NO               \n");
                sbSQL.AppendFormat("        ,SH.MACHINE_ID             \n");
                sbSQL.AppendFormat("        ,SH.SALE_NO                \n");
                sbSQL.AppendFormat("        ,PD.PAID_MODE              \n");
                sbSQL.AppendFormat("        ,DECODE(PD.PAID_MODE,1,'現金',2,'信用卡',3,'離線信用卡',4,'分期付款',5,'禮券',6,'金融卡',7,'Happy Go') AS PAID_MODE_NAME     \n");
                sbSQL.AppendFormat("        ,SD.TOTAL_AMOUNT           \n");
                sbSQL.AppendFormat("        ,SH.POSUUID_MASTER         \n");
                sbSQL.AppendFormat("    FROM SALE_HEAD    SH           \n");
                sbSQL.AppendFormat("        ,SALE_DETAIL  SD           \n");
                sbSQL.AppendFormat("        ,PAID_DETAIL  PD           \n");
                sbSQL.AppendFormat("        ,PRODUCT PROD              \n");
                sbSQL.AppendFormat("  WHERE SH.POSUUID_MASTER = SD.POSUUID_MASTER     \n");
                sbSQL.AppendFormat("    AND SH.POSUUID_MASTER = PD.POSUUID_MASTER     \n");
                sbSQL.AppendFormat("    AND SD.PRODNO = PROD.PRODNO                   \n");

                #region WHERE
                #region 門市編號
                if (!string.IsNullOrEmpty(STORE_NO_S))
                {
                    sbSQL.Append(" AND SH.STORE_NO >= ");
                    sbSQL.Append(Advtek.Utility.OracleDBUtil.SqlStr(STORE_NO_S.Trim()));
                    sbSQL.AppendLine();
                }
                if (!string.IsNullOrEmpty(STORE_NO_E))
                {
                    sbSQL.Append(" AND SH.STORE_NO <= ");
                    sbSQL.Append(Advtek.Utility.OracleDBUtil.SqlStr(STORE_NO_E.Trim()));
                    sbSQL.AppendLine();
                }
                #endregion
                #region 交易日期
                if (!string.IsNullOrEmpty(TRADE_DATE_S))
                {
                    sbSQL.Append(" AND TRUNC(SH.TRADE_DATE) >= ");
                    sbSQL.Append(Advtek.Utility.OracleDBUtil.DateStr(TRADE_DATE_S.Trim()));
                    sbSQL.AppendLine();
                }
                if (!string.IsNullOrEmpty(TRADE_DATE_E))
                {
                    sbSQL.Append(" AND TRUNC(SH.TRADE_DATE) <= ");
                    sbSQL.Append(Advtek.Utility.OracleDBUtil.DateStr(TRADE_DATE_E.Trim()));
                    sbSQL.AppendLine();
                }
                #endregion
                #region 機台編號
                if (!string.IsNullOrEmpty(MACHINE_ID) && MACHINE_ID.ToUpper().Trim() != "ALL")
                {
                    sbSQL.Append(" AND SH.MACHINE_ID = ");
                    sbSQL.Append(Advtek.Utility.OracleDBUtil.SqlStr(MACHINE_ID.Trim()));
                    sbSQL.AppendLine();
                }
                else if (string.IsNullOrEmpty(MACHINE_ID) && MACHINE_ID.ToUpper().Trim() == "ALL")
                {
                    sbSQL.Append(" AND SH.MACHINE_ID IN (SELECT HOST_NO FROM STORE_TERMINATING_MACHINE WHERE 1=1 ");

                    if (!string.IsNullOrEmpty(STORE_NO_S))
                    {
                        sbSQL.Append(" AND STORE_NO >= ");
                        sbSQL.Append(Advtek.Utility.OracleDBUtil.SqlStr(STORE_NO_S.Trim()));
                    }
                    if (!string.IsNullOrEmpty(STORE_NO_E))
                    {
                        sbSQL.Append(" AND STORE_NO <= ");
                        sbSQL.Append(Advtek.Utility.OracleDBUtil.SqlStr(STORE_NO_E.Trim()));
                    }

                    sbSQL.Append(" ) ");
                    sbSQL.AppendLine();
                }
                #endregion
                #region 門號
                if (!string.IsNullOrEmpty(MSISDN))
                {
                    sbSQL.Append(" AND SD.MSISDN = ");
                    sbSQL.Append(Advtek.Utility.OracleDBUtil.SqlStr(MSISDN.Trim()));
                    sbSQL.AppendLine();
                }
                #endregion
                #region 商品類別
                if (!string.IsNullOrEmpty(PRODTYPENO_S))
                {
                    sbSQL.Append(" AND PROD.PRODTYPENO >= ");
                    sbSQL.Append(Advtek.Utility.OracleDBUtil.SqlStr(PRODTYPENO_S.Trim()));
                    sbSQL.AppendLine();
                }
                if (!string.IsNullOrEmpty(PRODTYPENO_E))
                {
                    sbSQL.Append(" AND PROD.PRODTYPENO <= ");
                    sbSQL.Append(Advtek.Utility.OracleDBUtil.SqlStr(PRODTYPENO_E.Trim()));
                    sbSQL.AppendLine();
                }
                #endregion
                #region 商品料號
                if (!string.IsNullOrEmpty(PRODNO_S))
                {
                    sbSQL.Append(" AND SD.PRODNO >= ");
                    sbSQL.Append(Advtek.Utility.OracleDBUtil.SqlStr(PRODNO_S.Trim()));
                    sbSQL.AppendLine();
                }
                if (!string.IsNullOrEmpty(PRODNO_E))
                {
                    sbSQL.Append(" AND SD.PRODNO <= ");
                    sbSQL.Append(Advtek.Utility.OracleDBUtil.SqlStr(PRODNO_E.Trim()));
                    sbSQL.AppendLine();
                }
                #endregion
                #region 員工編號
                if (!string.IsNullOrEmpty(SALE_PERSON_S))
                {
                    sbSQL.Append(" AND SH.SALE_PERSON >= ");
                    sbSQL.Append(Advtek.Utility.OracleDBUtil.SqlStr(SALE_PERSON_S.Trim()));
                    sbSQL.AppendLine();
                }
                if (!string.IsNullOrEmpty(SALE_PERSON_E))
                {
                    sbSQL.Append(" AND SH.SALE_PERSON <= ");
                    sbSQL.Append(Advtek.Utility.OracleDBUtil.SqlStr(SALE_PERSON_E.Trim()));
                    sbSQL.AppendLine();
                }
                #endregion
                #endregion

                //sbSQL.AppendFormat("    AND SH.SALE_STATUS = '2'       \n");
                sbSQL.AppendFormat("    AND SD.SOURCE_TYPE = 11        \n");
                sbSQL.AppendFormat("    AND SD.PRODNO = (select para_value from sys_para where para_key = 'ETC_ITEM_CODE')) SH     \n");
                sbSQL.AppendFormat("  GROUP BY SH.STORE_NO ,SH.MACHINE_ID  \n");
                #endregion

                DataTable dtSQL = Advtek.Utility.OracleDBUtil.GetDataSet(objConn, sbSQL.ToString()).Tables[0];

                //ETC聯名卡儲值卡-總計
                if (dtSQL.Rows.Count > 0)
                {

                    for (int i = 0; i < dtSQL.Rows.Count; i++)
                    {
                        CASH_AMT = CASH_AMT + ToInt64(dtSQL.Rows[i].ItemArray[3].ToString());
                        CARD_AMT = CARD_AMT + ToInt64(dtSQL.Rows[i].ItemArray[4].ToString());
                        Installment_AMT = Installment_AMT + ToInt64(dtSQL.Rows[i].ItemArray[5].ToString());
                        GIFT_AMT = GIFT_AMT + ToInt64(dtSQL.Rows[i].ItemArray[6].ToString());
                        DebitCard_AMT = DebitCard_AMT + ToInt64(dtSQL.Rows[i].ItemArray[7].ToString());
                        HG_AMT = HG_AMT + ToInt64(dtSQL.Rows[i].ItemArray[8].ToString());
                    }
                }
                #endregion

                #region RPL046 門市總計 SQL 不含小計、加總 (不知道為什麼加入小計加總的SQL就會需要跑很久很久...)

                #region VOUCHER
                sb.AppendFormat("WITH   \n");
                sb.AppendFormat("VOUCHER AS (  \n");
                sb.AppendFormat("  SELECT SH.POSUUID_MASTER, IH.ID, IH.INVOICE_NO, IH.TOTAL_AMOUNT  \n");
                sb.AppendFormat("    FROM SALE_HEAD        SH,  \n");
                sb.AppendFormat("         VOUCHER_RELATION VR,  \n");
                sb.AppendFormat("         INVOICE_HEAD     IH,  \n");
                sb.AppendFormat("         SALE_DETAIL      SD,  \n");
                sb.AppendFormat("         PRODUCT          PROD,  \n");
                sb.AppendFormat("         INVOICE_ITEM     II  \n");
                sb.AppendFormat("   WHERE SH.POSUUID_MASTER = VR.SALE_HEAD_ID  \n");
                sb.AppendFormat("     AND VR.VOUCHER_ID = IH.ID  \n");
                sb.AppendFormat("     AND IH.ID = II.INVOICE_HEAD_ID  \n");
                //sb.AppendFormat("     AND NVL(IH.IS_INVALID, 'N') = 'N'  \n");
                sb.AppendFormat("     AND SH.POSUUID_MASTER = SD.POSUUID_MASTER  \n");
                sb.AppendFormat("     AND SD.PRODNO = PROD.PRODNO  \n");
                //sb.AppendFormat("     AND SH.SALE_STATUS = '2'  \n");
                sb.AppendFormat("     AND 1 = 1  \n");

                #region WHERE
                #region 門市編號
                if (!string.IsNullOrEmpty(STORE_NO_S))
                {
                    sb.Append(" AND SH.STORE_NO >= ");
                    sb.Append(Advtek.Utility.OracleDBUtil.SqlStr(STORE_NO_S.Trim()));
                    sb.AppendLine();
                }
                if (!string.IsNullOrEmpty(STORE_NO_E))
                {
                    sb.Append(" AND SH.STORE_NO <= ");
                    sb.Append(Advtek.Utility.OracleDBUtil.SqlStr(STORE_NO_E.Trim()));
                    sb.AppendLine();
                }
                #endregion
                #region 交易日期
                if (!string.IsNullOrEmpty(TRADE_DATE_S))
                {
                    sb.Append(" AND TRUNC(SH.TRADE_DATE) >= ");
                    sb.Append(Advtek.Utility.OracleDBUtil.DateStr(TRADE_DATE_S.Trim()));
                    sb.AppendLine();
                }
                if (!string.IsNullOrEmpty(TRADE_DATE_E))
                {
                    sb.Append(" AND TRUNC(SH.TRADE_DATE) <= ");
                    sb.Append(Advtek.Utility.OracleDBUtil.DateStr(TRADE_DATE_E.Trim()));
                    sb.AppendLine();
                }
                #endregion
                #region 機台編號
                if (!string.IsNullOrEmpty(MACHINE_ID) && MACHINE_ID.ToUpper().Trim() != "ALL")
                {
                    sb.Append(" AND SH.MACHINE_ID = ");
                    sb.Append(Advtek.Utility.OracleDBUtil.SqlStr(MACHINE_ID.Trim()));
                    sb.AppendLine();
                }
                else if (string.IsNullOrEmpty(MACHINE_ID) && MACHINE_ID.ToUpper().Trim() == "ALL")
                {
                    sb.Append(" AND SH.MACHINE_ID IN (SELECT HOST_NO FROM STORE_TERMINATING_MACHINE WHERE 1=1 ");

                    if (!string.IsNullOrEmpty(STORE_NO_S))
                    {
                        sb.Append(" AND STORE_NO >= ");
                        sb.Append(Advtek.Utility.OracleDBUtil.SqlStr(STORE_NO_S.Trim()));
                    }
                    if (!string.IsNullOrEmpty(STORE_NO_E))
                    {
                        sb.Append(" AND STORE_NO <= ");
                        sb.Append(Advtek.Utility.OracleDBUtil.SqlStr(STORE_NO_E.Trim()));
                    }

                    sb.Append(" ) ");
                    sb.AppendLine();
                }
                #endregion
                #region 門號
                if (!string.IsNullOrEmpty(MSISDN))
                {
                    sb.Append(" AND SD.MSISDN = ");
                    sb.Append(Advtek.Utility.OracleDBUtil.SqlStr(MSISDN.Trim()));
                    sb.AppendLine();
                }
                #endregion
                #region 商品類別
                if (!string.IsNullOrEmpty(PRODTYPENO_S))
                {
                    sb.Append(" AND PROD.PRODTYPENO >= ");
                    sb.Append(Advtek.Utility.OracleDBUtil.SqlStr(PRODTYPENO_S.Trim()));
                    sb.AppendLine();
                }
                if (!string.IsNullOrEmpty(PRODTYPENO_E))
                {
                    sb.Append(" AND PROD.PRODTYPENO <= ");
                    sb.Append(Advtek.Utility.OracleDBUtil.SqlStr(PRODTYPENO_E.Trim()));
                    sb.AppendLine();
                }
                #endregion
                #region 商品料號
                if (!string.IsNullOrEmpty(PRODNO_S))
                {
                    sb.Append(" AND SD.PRODNO >= ");
                    sb.Append(Advtek.Utility.OracleDBUtil.SqlStr(PRODNO_S.Trim()));
                    sb.AppendLine();
                }
                if (!string.IsNullOrEmpty(PRODNO_E))
                {
                    sb.Append(" AND SD.PRODNO <= ");
                    sb.Append(Advtek.Utility.OracleDBUtil.SqlStr(PRODNO_E.Trim()));
                    sb.AppendLine();
                }
                #endregion
                #region 發票號碼
                if (!string.IsNullOrEmpty(INVOICE_NO_S))
                {
                    sb.Append(" AND IH.INVOICE_NO >= ");
                    sb.Append(Advtek.Utility.OracleDBUtil.SqlStr(INVOICE_NO_S.Trim()));
                    sb.AppendLine();
                }
                if (!string.IsNullOrEmpty(INVOICE_NO_E))
                {
                    sb.Append(" AND IH.INVOICE_NO <= ");
                    sb.Append(Advtek.Utility.OracleDBUtil.SqlStr(INVOICE_NO_E.Trim()));
                    sb.AppendLine();
                }
                #endregion
                #region 交易金額
                if (!string.IsNullOrEmpty(TOTAL_AMOUNT_S))
                {
                    sb.Append(" AND IH.TOTAL_AMOUNT >= ");
                    sb.Append(Advtek.Utility.OracleDBUtil.SqlStr(TOTAL_AMOUNT_S.Trim()));
                    sb.AppendLine();
                }
                if (!string.IsNullOrEmpty(TOTAL_AMOUNT_E))
                {
                    sb.Append(" AND IH.TOTAL_AMOUNT <= ");
                    sb.Append(Advtek.Utility.OracleDBUtil.SqlStr(TOTAL_AMOUNT_E.Trim()));
                    sb.AppendLine();
                }
                #endregion
                #region 員工編號
                if (!string.IsNullOrEmpty(SALE_PERSON_S))
                {
                    sb.Append(" AND SH.SALE_PERSON >= ");
                    sb.Append(Advtek.Utility.OracleDBUtil.SqlStr(SALE_PERSON_S.Trim()));
                    sb.AppendLine();
                }
                if (!string.IsNullOrEmpty(SALE_PERSON_E))
                {
                    sb.Append(" AND SH.SALE_PERSON <= ");
                    sb.Append(Advtek.Utility.OracleDBUtil.SqlStr(SALE_PERSON_E.Trim()));
                    sb.AppendLine();
                }
                #endregion
                #endregion

                sb.AppendFormat("  UNION ALL  \n");
                sb.AppendFormat("  SELECT SH.POSUUID_MASTER, MIH.ID, MIH.INVOICE_NO, MIH.TOTAL_AMOUNT  \n");
                sb.AppendFormat("    FROM SALE_HEAD           SH,  \n");
                sb.AppendFormat("         VOUCHER_RELATION    VR,  \n");
                sb.AppendFormat("         SALE_DETAIL         SD,  \n");
                sb.AppendFormat("         PRODUCT             PROD,  \n");
                sb.AppendFormat("         MANUAL_INVOICE_HEAD MIH,  \n");
                sb.AppendFormat("         MANUAL_INVOICE_ITEM MII  \n");
                sb.AppendFormat("   WHERE SH.POSUUID_MASTER = VR.SALE_HEAD_ID  \n");
                sb.AppendFormat("     AND SH.POSUUID_MASTER = SD.POSUUID_MASTER  \n");
                sb.AppendFormat("     AND SD.PRODNO = PROD.PRODNO  \n");
                sb.AppendFormat("     AND VR.VOUCHER_ID = MIH.ID  \n");
                sb.AppendFormat("     AND MIH.ID = MII.MANUAL_INVOICE_HEAD_ID  \n");
                //sb.AppendFormat("     AND NVL(MIH.IS_INVALID, 'N') = 'N'  \n");
                //sb.AppendFormat("     AND SH.SALE_STATUS = '2'  \n");
                sb.AppendFormat("     AND 1 = 1  \n");

                #region WHERE
                #region 門市編號
                if (!string.IsNullOrEmpty(STORE_NO_S))
                {
                    sb.Append(" AND SH.STORE_NO >= ");
                    sb.Append(Advtek.Utility.OracleDBUtil.SqlStr(STORE_NO_S.Trim()));
                    sb.AppendLine();
                }
                if (!string.IsNullOrEmpty(STORE_NO_E))
                {
                    sb.Append(" AND SH.STORE_NO <= ");
                    sb.Append(Advtek.Utility.OracleDBUtil.SqlStr(STORE_NO_E.Trim()));
                    sb.AppendLine();
                }
                #endregion
                #region 交易日期
                if (!string.IsNullOrEmpty(TRADE_DATE_S))
                {
                    sb.Append(" AND TRUNC(SH.TRADE_DATE) >= ");
                    sb.Append(Advtek.Utility.OracleDBUtil.DateStr(TRADE_DATE_S.Trim()));
                    sb.AppendLine();
                }
                if (!string.IsNullOrEmpty(TRADE_DATE_E))
                {
                    sb.Append(" AND TRUNC(SH.TRADE_DATE) <= ");
                    sb.Append(Advtek.Utility.OracleDBUtil.DateStr(TRADE_DATE_E.Trim()));
                    sb.AppendLine();
                }
                #endregion
                #region 機台編號
                if (!string.IsNullOrEmpty(MACHINE_ID) && MACHINE_ID.ToUpper().Trim() != "ALL")
                {
                    sb.Append(" AND SH.MACHINE_ID = ");
                    sb.Append(Advtek.Utility.OracleDBUtil.SqlStr(MACHINE_ID.Trim()));
                    sb.AppendLine();
                }
                else if (string.IsNullOrEmpty(MACHINE_ID) && MACHINE_ID.ToUpper().Trim() == "ALL")
                {
                    sb.Append(" AND SH.MACHINE_ID IN (SELECT HOST_NO FROM STORE_TERMINATING_MACHINE WHERE 1=1 ");

                    if (!string.IsNullOrEmpty(STORE_NO_S))
                    {
                        sb.Append(" AND STORE_NO >= ");
                        sb.Append(Advtek.Utility.OracleDBUtil.SqlStr(STORE_NO_S.Trim()));
                    }
                    if (!string.IsNullOrEmpty(STORE_NO_E))
                    {
                        sb.Append(" AND STORE_NO <= ");
                        sb.Append(Advtek.Utility.OracleDBUtil.SqlStr(STORE_NO_E.Trim()));
                    }

                    sb.Append(" ) ");
                    sb.AppendLine();
                }
                #endregion
                #region 門號
                if (!string.IsNullOrEmpty(MSISDN))
                {
                    sb.Append(" AND SD.MSISDN = ");
                    sb.Append(Advtek.Utility.OracleDBUtil.SqlStr(MSISDN.Trim()));
                    sb.AppendLine();
                }
                #endregion
                #region 商品類別
                if (!string.IsNullOrEmpty(PRODTYPENO_S))
                {
                    sb.Append(" AND PROD.PRODTYPENO >= ");
                    sb.Append(Advtek.Utility.OracleDBUtil.SqlStr(PRODTYPENO_S.Trim()));
                    sb.AppendLine();
                }
                if (!string.IsNullOrEmpty(PRODTYPENO_E))
                {
                    sb.Append(" AND PROD.PRODTYPENO <= ");
                    sb.Append(Advtek.Utility.OracleDBUtil.SqlStr(PRODTYPENO_E.Trim()));
                    sb.AppendLine();
                }
                #endregion
                #region 商品料號
                if (!string.IsNullOrEmpty(PRODNO_S))
                {
                    sb.Append(" AND SD.PRODNO >= ");
                    sb.Append(Advtek.Utility.OracleDBUtil.SqlStr(PRODNO_S.Trim()));
                    sb.AppendLine();
                }
                if (!string.IsNullOrEmpty(PRODNO_E))
                {
                    sb.Append(" AND SD.PRODNO <= ");
                    sb.Append(Advtek.Utility.OracleDBUtil.SqlStr(PRODNO_E.Trim()));
                    sb.AppendLine();
                }
                #endregion
                #region 發票號碼
                if (!string.IsNullOrEmpty(INVOICE_NO_S))
                {
                    sb.Append(" AND MIH.INVOICE_NO >= ");
                    sb.Append(Advtek.Utility.OracleDBUtil.SqlStr(INVOICE_NO_S.Trim()));
                    sb.AppendLine();
                }
                if (!string.IsNullOrEmpty(INVOICE_NO_E))
                {
                    sb.Append(" AND MIH.INVOICE_NO <= ");
                    sb.Append(Advtek.Utility.OracleDBUtil.SqlStr(INVOICE_NO_E.Trim()));
                    sb.AppendLine();
                }
                #endregion
                #region 交易金額
                if (!string.IsNullOrEmpty(TOTAL_AMOUNT_S))
                {
                    sb.Append(" AND IH.TOTAL_AMOUNT >= ");
                    sb.Append(Advtek.Utility.OracleDBUtil.SqlStr(TOTAL_AMOUNT_S.Trim()));
                    sb.AppendLine();
                }
                if (!string.IsNullOrEmpty(TOTAL_AMOUNT_E))
                {
                    sb.Append(" AND IH.TOTAL_AMOUNT <= ");
                    sb.Append(Advtek.Utility.OracleDBUtil.SqlStr(TOTAL_AMOUNT_E.Trim()));
                    sb.AppendLine();
                }
                #endregion
                #region 員工編號
                if (!string.IsNullOrEmpty(SALE_PERSON_S))
                {
                    sb.Append(" AND SH.SALE_PERSON >= ");
                    sb.Append(Advtek.Utility.OracleDBUtil.SqlStr(SALE_PERSON_S.Trim()));
                    sb.AppendLine();
                }
                if (!string.IsNullOrEmpty(SALE_PERSON_E))
                {
                    sb.Append(" AND SH.SALE_PERSON <= ");
                    sb.Append(Advtek.Utility.OracleDBUtil.SqlStr(SALE_PERSON_E.Trim()));
                    sb.AppendLine();
                }
                #endregion
                #endregion

                sb.AppendFormat("  UNION ALL  \n");
                sb.AppendFormat("  SELECT SH.POSUUID_MASTER,  \n");
                sb.AppendFormat("         RH.ID,  \n");
                sb.AppendFormat("         RH.RECEIPT_NO AS INVOICE_NO,  \n");
                sb.AppendFormat("         RH.TOTAL_AMOUNT  \n");
                sb.AppendFormat("    FROM SALE_HEAD        SH,  \n");
                sb.AppendFormat("         VOUCHER_RELATION VR,  \n");
                sb.AppendFormat("         SALE_DETAIL      SD,  \n");
                sb.AppendFormat("         PRODUCT          PROD,  \n");
                sb.AppendFormat("         RECEIPT_HEAD     RH  \n");
                sb.AppendFormat("   WHERE SH.POSUUID_MASTER = VR.SALE_HEAD_ID  \n");
                sb.AppendFormat("     AND SH.POSUUID_MASTER = SD.POSUUID_MASTER  \n");
                sb.AppendFormat("     AND SD.PRODNO = PROD.PRODNO  \n");
                sb.AppendFormat("     AND VR.VOUCHER_ID = RH.ID  \n");
                //sb.AppendFormat("     AND NVL(RH.IS_INVALID, 'N') = 'N'  \n");
                //sb.AppendFormat("     AND SH.SALE_STATUS = '2'  \n");
                sb.AppendFormat("     AND 1 = 1  \n");

                #region WHERE
                #region 門市編號
                if (!string.IsNullOrEmpty(STORE_NO_S))
                {
                    sb.Append(" AND SH.STORE_NO >= ");
                    sb.Append(Advtek.Utility.OracleDBUtil.SqlStr(STORE_NO_S.Trim()));
                    sb.AppendLine();
                }
                if (!string.IsNullOrEmpty(STORE_NO_E))
                {
                    sb.Append(" AND SH.STORE_NO <= ");
                    sb.Append(Advtek.Utility.OracleDBUtil.SqlStr(STORE_NO_E.Trim()));
                    sb.AppendLine();
                }
                #endregion
                #region 交易日期
                if (!string.IsNullOrEmpty(TRADE_DATE_S))
                {
                    sb.Append(" AND TRUNC(SH.TRADE_DATE) >= ");
                    sb.Append(Advtek.Utility.OracleDBUtil.DateStr(TRADE_DATE_S.Trim()));
                    sb.AppendLine();
                }
                if (!string.IsNullOrEmpty(TRADE_DATE_E))
                {
                    sb.Append(" AND TRUNC(SH.TRADE_DATE) <= ");
                    sb.Append(Advtek.Utility.OracleDBUtil.DateStr(TRADE_DATE_E.Trim()));
                    sb.AppendLine();
                }
                #endregion
                #region 機台編號
                if (!string.IsNullOrEmpty(MACHINE_ID) && MACHINE_ID.ToUpper().Trim() != "ALL")
                {
                    sb.Append(" AND SH.MACHINE_ID = ");
                    sb.Append(Advtek.Utility.OracleDBUtil.SqlStr(MACHINE_ID.Trim()));
                    sb.AppendLine();
                }
                else if (string.IsNullOrEmpty(MACHINE_ID) && MACHINE_ID.ToUpper().Trim() == "ALL")
                {
                    sb.Append(" AND SH.MACHINE_ID IN (SELECT HOST_NO FROM STORE_TERMINATING_MACHINE WHERE 1=1 ");

                    if (!string.IsNullOrEmpty(STORE_NO_S))
                    {
                        sb.Append(" AND STORE_NO >= ");
                        sb.Append(Advtek.Utility.OracleDBUtil.SqlStr(STORE_NO_S.Trim()));
                    }
                    if (!string.IsNullOrEmpty(STORE_NO_E))
                    {
                        sb.Append(" AND STORE_NO <= ");
                        sb.Append(Advtek.Utility.OracleDBUtil.SqlStr(STORE_NO_E.Trim()));
                    }

                    sb.Append(" ) ");
                    sb.AppendLine();
                }
                #endregion
                #region 門號
                if (!string.IsNullOrEmpty(MSISDN))
                {
                    sb.Append(" AND SD.MSISDN = ");
                    sb.Append(Advtek.Utility.OracleDBUtil.SqlStr(MSISDN.Trim()));
                    sb.AppendLine();
                }
                #endregion
                #region 商品類別
                if (!string.IsNullOrEmpty(PRODTYPENO_S))
                {
                    sb.Append(" AND PROD.PRODTYPENO >= ");
                    sb.Append(Advtek.Utility.OracleDBUtil.SqlStr(PRODTYPENO_S.Trim()));
                    sb.AppendLine();
                }
                if (!string.IsNullOrEmpty(PRODTYPENO_E))
                {
                    sb.Append(" AND PROD.PRODTYPENO <= ");
                    sb.Append(Advtek.Utility.OracleDBUtil.SqlStr(PRODTYPENO_E.Trim()));
                    sb.AppendLine();
                }
                #endregion
                #region 商品料號
                if (!string.IsNullOrEmpty(PRODNO_S))
                {
                    sb.Append(" AND SD.PRODNO >= ");
                    sb.Append(Advtek.Utility.OracleDBUtil.SqlStr(PRODNO_S.Trim()));
                    sb.AppendLine();
                }
                if (!string.IsNullOrEmpty(PRODNO_E))
                {
                    sb.Append(" AND SD.PRODNO <= ");
                    sb.Append(Advtek.Utility.OracleDBUtil.SqlStr(PRODNO_E.Trim()));
                    sb.AppendLine();
                }
                #endregion
                #region 發票號碼
                if (!string.IsNullOrEmpty(INVOICE_NO_S))
                {
                    sb.Append(" AND RH.RECEIPT_NO >= ");
                    sb.Append(Advtek.Utility.OracleDBUtil.SqlStr(INVOICE_NO_S.Trim()));
                    sb.AppendLine();
                }
                if (!string.IsNullOrEmpty(INVOICE_NO_E))
                {
                    sb.Append(" AND RH.RECEIPT_NO <= ");
                    sb.Append(Advtek.Utility.OracleDBUtil.SqlStr(INVOICE_NO_E.Trim()));
                    sb.AppendLine();
                }
                #endregion
                #region 交易金額
                if (!string.IsNullOrEmpty(TOTAL_AMOUNT_S))
                {
                    sb.Append(" AND IH.TOTAL_AMOUNT >= ");
                    sb.Append(Advtek.Utility.OracleDBUtil.SqlStr(TOTAL_AMOUNT_S.Trim()));
                    sb.AppendLine();
                }
                if (!string.IsNullOrEmpty(TOTAL_AMOUNT_E))
                {
                    sb.Append(" AND IH.TOTAL_AMOUNT <= ");
                    sb.Append(Advtek.Utility.OracleDBUtil.SqlStr(TOTAL_AMOUNT_E.Trim()));
                    sb.AppendLine();
                }
                #endregion
                #region 員工編號
                if (!string.IsNullOrEmpty(SALE_PERSON_S))
                {
                    sb.Append(" AND SH.SALE_PERSON >= ");
                    sb.Append(Advtek.Utility.OracleDBUtil.SqlStr(SALE_PERSON_S.Trim()));
                    sb.AppendLine();
                }
                if (!string.IsNullOrEmpty(SALE_PERSON_E))
                {
                    sb.Append(" AND SH.SALE_PERSON <= ");
                    sb.Append(Advtek.Utility.OracleDBUtil.SqlStr(SALE_PERSON_E.Trim()));
                    sb.AppendLine();
                }
                #endregion
                #endregion

                sb.AppendFormat(")  \n");
                #endregion
                sb.AppendFormat(",  \n");
                #region MACHINE

                sb.AppendFormat("MACHINE AS (  \n");
                sb.AppendFormat("  SELECT T.TITLE_INDEX, T.TITLE, HOST_NO AS MACHINE_ID, STORE_NO FROM STORE_TERMINATING_MACHINE S,  \n");
                sb.AppendFormat("  (  \n");
                sb.AppendFormat("        SELECT '0101' AS TITLE_INDEX, '銷貨收入' AS TITLE FROM DUAL  \n");
                sb.AppendFormat("  UNION SELECT '0102' AS TITLE_INDEX, '維修收入' AS TITLE FROM DUAL  \n");
                sb.AppendFormat("  UNION SELECT '0103' AS TITLE_INDEX, '租賃收入' AS TITLE FROM DUAL  \n");
                sb.AppendFormat("  UNION SELECT '0104' AS TITLE_INDEX, '預購收入' AS TITLE FROM DUAL  \n");
                sb.AppendFormat("  UNION SELECT '0201' AS TITLE_INDEX, '遠傳帳單' AS TITLE FROM DUAL  \n");
                sb.AppendFormat("  UNION SELECT '0202' AS TITLE_INDEX, '和信帳單' AS TITLE FROM DUAL  \n");
                sb.AppendFormat("  UNION SELECT '0203' AS TITLE_INDEX, '速博帳單' AS TITLE FROM DUAL  \n");
                sb.AppendFormat("  UNION SELECT '0204' AS TITLE_INDEX, '遠通帳單' AS TITLE FROM DUAL  \n");
                sb.AppendFormat("  UNION SELECT '0205' AS TITLE_INDEX, '遠通加值' AS TITLE FROM DUAL  \n");
                sb.AppendFormat("  UNION SELECT '0206' AS TITLE_INDEX, 'SEEDNET帳單' AS TITLE FROM DUAL  \n");
                sb.AppendFormat("  UNION SELECT '0301' AS TITLE_INDEX, '遠傳保證金' AS TITLE FROM DUAL  \n");
                sb.AppendFormat("  UNION SELECT '0302' AS TITLE_INDEX, '和信保證金' AS TITLE FROM DUAL  \n");
                sb.AppendFormat("  UNION SELECT '0303' AS TITLE_INDEX, '遠通保證金' AS TITLE FROM DUAL  \n");
                sb.AppendFormat("  UNION SELECT '0304' AS TITLE_INDEX, '租賃保證金' AS TITLE FROM DUAL  \n");
                sb.AppendFormat("  UNION SELECT '0305' AS TITLE_INDEX, '折抵金' AS TITLE FROM DUAL  \n");
                sb.AppendFormat("  UNION SELECT '0306' AS TITLE_INDEX, '折抵點數' AS TITLE FROM DUAL  \n");
                //sb.AppendFormat("  UNION SELECT '1001' AS TITLE_INDEX, 'ETC聯名卡儲值金' AS TITLE FROM DUAL  \n");
                sb.AppendFormat("  ) T  \n");
                sb.AppendFormat("  WHERE 1 = 1  \n");

                #region 門市編號
                if (!string.IsNullOrEmpty(STORE_NO_S))
                {
                    sb.Append(" AND S.STORE_NO >= ");
                    sb.Append(Advtek.Utility.OracleDBUtil.SqlStr(STORE_NO_S.Trim()));
                    sb.AppendLine();
                }
                if (!string.IsNullOrEmpty(STORE_NO_E))
                {
                    sb.Append(" AND S.STORE_NO <= ");
                    sb.Append(Advtek.Utility.OracleDBUtil.SqlStr(STORE_NO_E.Trim()));
                    sb.AppendLine();
                }
                #endregion
                #region 機台編號
                if (!string.IsNullOrEmpty(MACHINE_ID) && MACHINE_ID.ToUpper().Trim() != "ALL")
                {
                    sb.Append(" AND S.MACHINE_ID = ");
                    sb.Append(Advtek.Utility.OracleDBUtil.SqlStr(MACHINE_ID.Trim()));
                    sb.AppendLine();
                }
                else if (string.IsNullOrEmpty(MACHINE_ID) && MACHINE_ID.ToUpper().Trim() == "ALL")
                {
                    sb.Append(" AND S.MACHINE_ID IN (SELECT HOST_NO FROM STORE_TERMINATING_MACHINE WHERE 1=1 ");

                    if (!string.IsNullOrEmpty(STORE_NO_S))
                    {
                        sb.Append(" AND STORE_NO >= ");
                        sb.Append(Advtek.Utility.OracleDBUtil.SqlStr(STORE_NO_S.Trim()));
                    }
                    if (!string.IsNullOrEmpty(STORE_NO_E))
                    {
                        sb.Append(" AND STORE_NO <= ");
                        sb.Append(Advtek.Utility.OracleDBUtil.SqlStr(STORE_NO_E.Trim()));
                    }

                    sb.Append(" ) ");
                    sb.AppendLine();
                }
                #endregion

                sb.AppendFormat(")  \n");
                #endregion
                sb.AppendFormat(",  \n");
                #region TITLE_ROW
                sb.AppendFormat("TITLE_ROW AS (  \n");
                sb.AppendFormat("        SELECT '-1' MACHINE_ID, '代收' AS TITLE , '0200' AS TITLE_INDEX FROM DUAL  \n");
                sb.AppendFormat("  UNION SELECT '-1' MACHINE_ID, '保證金' AS TITLE , '0300' AS TITLE_INDEX FROM DUAL  \n");
                sb.AppendFormat(" \n");
                sb.AppendFormat("  UNION ALL \n");
                sb.AppendFormat("  SELECT DISTINCT HOST_NO AS MACHINE_ID, TITLE, TITLE_INDEX FROM STORE_TERMINATING_MACHINE S,  \n");
                sb.AppendFormat("  (  \n");
                sb.AppendFormat("        SELECT '代收' AS TITLE , '0200' AS TITLE_INDEX FROM DUAL  \n");
                sb.AppendFormat("  UNION SELECT '保證金' AS TITLE , '0300' AS TITLE_INDEX FROM DUAL  \n");
                sb.AppendFormat("  )  \n");
                sb.AppendFormat("  WHERE 1 = 1  \n");

                #region 門市編號
                if (!string.IsNullOrEmpty(STORE_NO_S))
                {
                    sb.Append(" AND S.STORE_NO >= ");
                    sb.Append(Advtek.Utility.OracleDBUtil.SqlStr(STORE_NO_S.Trim()));
                    sb.AppendLine();
                }
                if (!string.IsNullOrEmpty(STORE_NO_E))
                {
                    sb.Append(" AND S.STORE_NO <= ");
                    sb.Append(Advtek.Utility.OracleDBUtil.SqlStr(STORE_NO_E.Trim()));
                    sb.AppendLine();
                }
                #endregion
                #region 機台編號
                if (!string.IsNullOrEmpty(MACHINE_ID) && MACHINE_ID.ToUpper().Trim() != "ALL")
                {
                    sb.Append(" AND S.MACHINE_ID = ");
                    sb.Append(Advtek.Utility.OracleDBUtil.SqlStr(MACHINE_ID.Trim()));
                    sb.AppendLine();
                }
                else if (string.IsNullOrEmpty(MACHINE_ID) && MACHINE_ID.ToUpper().Trim() == "ALL")
                {
                    sb.Append(" AND S.MACHINE_ID IN (SELECT HOST_NO FROM STORE_TERMINATING_MACHINE WHERE 1=1 ");

                    if (!string.IsNullOrEmpty(STORE_NO_S))
                    {
                        sb.Append(" AND STORE_NO >= ");
                        sb.Append(Advtek.Utility.OracleDBUtil.SqlStr(STORE_NO_S.Trim()));
                    }
                    if (!string.IsNullOrEmpty(STORE_NO_E))
                    {
                        sb.Append(" AND STORE_NO <= ");
                        sb.Append(Advtek.Utility.OracleDBUtil.SqlStr(STORE_NO_E.Trim()));
                    }

                    sb.Append(" ) ");
                    sb.AppendLine();
                }
                #endregion

                sb.AppendFormat(")  \n");
                #endregion
                sb.AppendFormat(",  \n");
                #region SALE_LIST
                sb.AppendFormat("SALE_LIST AS (  \n");
                sb.AppendFormat("  --總表  \n");
                #region -收入類- (銷貨收入=全部-代收-保證金-維修收入-租賃收入-預購收入)
                sb.AppendFormat("  --銷貨收入  \n");
                sb.AppendFormat("  SELECT '銷貨收入' AS TITLE ,SH.STORE_NO ,SH.MACHINE_ID  \n");
                sb.AppendFormat("        ,NVL(SUM(DECODE(SOD.PAY_MODE_ID,'1',SOD.AMOUNT)),0) AS CASH_AMT  \n");
                sb.AppendFormat("        ,NVL(SUM(DECODE(SOD.PAY_MODE_ID,'2',SOD.AMOUNT)),0) + NVL(SUM(DECODE(SOD.PAY_MODE_ID,'3',SOD.AMOUNT)),0) AS CARD_AMT  \n");
                sb.AppendFormat("        ,NVL(SUM(DECODE(SOD.PAY_MODE_ID,'4',SOD.AMOUNT)),0) AS Installment_AMT  \n");
                sb.AppendFormat("        ,NVL(SUM(DECODE(SOD.PAY_MODE_ID,'6',SOD.AMOUNT)),0) AS DebitCard_AMT  \n");
                sb.AppendFormat("        ,NVL(SUM(DECODE(SOD.PAY_MODE_ID,'5',SOD.AMOUNT)),0) AS GIFT_AMT  \n");
                sb.AppendFormat("        ,NVL(SUM(DECODE(SOD.PAY_MODE_ID,'7',SOD.AMOUNT)),0) AS HG_AMT  \n");
                sb.AppendFormat("    FROM SALE_HEAD SH ,SALE_DETAIL SD ,SUPPLIER_OUT_DISPATCH SOD ,PRODUCT PROD   \n");
                sb.AppendFormat("         ,VOUCHER  \n");
                sb.AppendFormat("     WHERE SH.POSUUID_MASTER = SD.POSUUID_MASTER       \n");
                sb.AppendFormat("       AND SH.POSUUID_MASTER = SOD.POSUUID_MASTER      \n");
                sb.AppendFormat("       AND SH.POSUUID_MASTER = VOUCHER.POSUUID_MASTER  \n");
                sb.AppendFormat("       AND SD.PRODNO = PROD.PRODNO   \n");
                sb.AppendFormat("       AND SD.PRODNO NOT IN (SELECT GUARANTEE_PRODNO FROM GUARANTEE_PROD_MAPPING )   \n");   //保證金及租賃收入
                sb.AppendFormat("       AND SD.PRODNO NOT IN (SELECT PARA_VALUE FROM SYS_PARA WHERE PARA_KEY = 'PHONE_REPAIR_ITEM_CODE' )   \n");   //維修收入
                //sb.AppendFormat("       AND SH.SALE_STATUS = '2'  \n");   
                sb.AppendFormat("       AND SH.SALE_TYPE = 1      \n");   //SALE_TYPE=1=>銷貨收入；2=>代收收入
                #region -WHERE-
                #region 門市編號
                if (!string.IsNullOrEmpty(STORE_NO_S))
                {
                    sb.Append(" AND SH.STORE_NO >= ");
                    sb.Append(Advtek.Utility.OracleDBUtil.SqlStr(STORE_NO_S.Trim()));
                    sb.AppendLine();
                }
                if (!string.IsNullOrEmpty(STORE_NO_E))
                {
                    sb.Append(" AND SH.STORE_NO <= ");
                    sb.Append(Advtek.Utility.OracleDBUtil.SqlStr(STORE_NO_E.Trim()));
                    sb.AppendLine();
                }
                #endregion
                #region 交易日期
                if (!string.IsNullOrEmpty(TRADE_DATE_S))
                {
                    sb.Append(" AND TRUNC(SH.TRADE_DATE) >= ");
                    sb.Append(Advtek.Utility.OracleDBUtil.DateStr(TRADE_DATE_S.Trim()));
                    sb.AppendLine();
                }
                if (!string.IsNullOrEmpty(TRADE_DATE_E))
                {
                    sb.Append(" AND TRUNC(SH.TRADE_DATE) <= ");
                    sb.Append(Advtek.Utility.OracleDBUtil.DateStr(TRADE_DATE_E.Trim()));
                    sb.AppendLine();
                }
                #endregion
                #region 機台編號
                if (!string.IsNullOrEmpty(MACHINE_ID) && MACHINE_ID.ToUpper().Trim() != "ALL")
                {
                    sb.Append(" AND SH.MACHINE_ID = ");
                    sb.Append(Advtek.Utility.OracleDBUtil.SqlStr(MACHINE_ID.Trim()));
                    sb.AppendLine();
                }
                else if (string.IsNullOrEmpty(MACHINE_ID) && MACHINE_ID.ToUpper().Trim() == "ALL")
                {
                    sb.Append(" AND SH.MACHINE_ID IN (SELECT HOST_NO FROM STORE_TERMINATING_MACHINE WHERE 1=1 ");

                    if (!string.IsNullOrEmpty(STORE_NO_S))
                    {
                        sb.Append(" AND STORE_NO >= ");
                        sb.Append(Advtek.Utility.OracleDBUtil.SqlStr(STORE_NO_S.Trim()));
                    }
                    if (!string.IsNullOrEmpty(STORE_NO_E))
                    {
                        sb.Append(" AND STORE_NO <= ");
                        sb.Append(Advtek.Utility.OracleDBUtil.SqlStr(STORE_NO_E.Trim()));
                    }

                    sb.Append(" ) ");
                    sb.AppendLine();
                }
                #endregion
                #region 門號
                if (!string.IsNullOrEmpty(MSISDN))
                {
                    sb.Append(" AND SD.MSISDN = ");
                    sb.Append(Advtek.Utility.OracleDBUtil.SqlStr(MSISDN.Trim()));
                    sb.AppendLine();
                }
                #endregion
                #region 商品類別
                if (!string.IsNullOrEmpty(PRODTYPENO_S))
                {
                    sb.Append(" AND PROD.PRODTYPENO >= ");
                    sb.Append(Advtek.Utility.OracleDBUtil.SqlStr(PRODTYPENO_S.Trim()));
                    sb.AppendLine();
                }
                if (!string.IsNullOrEmpty(PRODTYPENO_E))
                {
                    sb.Append(" AND PROD.PRODTYPENO <= ");
                    sb.Append(Advtek.Utility.OracleDBUtil.SqlStr(PRODTYPENO_E.Trim()));
                    sb.AppendLine();
                }
                #endregion
                #region 商品料號
                if (!string.IsNullOrEmpty(PRODNO_S))
                {
                    sb.Append(" AND SD.PRODNO >= ");
                    sb.Append(Advtek.Utility.OracleDBUtil.SqlStr(PRODNO_S.Trim()));
                    sb.AppendLine();
                }
                if (!string.IsNullOrEmpty(PRODNO_E))
                {
                    sb.Append(" AND SD.PRODNO <= ");
                    sb.Append(Advtek.Utility.OracleDBUtil.SqlStr(PRODNO_E.Trim()));
                    sb.AppendLine();
                }
                #endregion
                #region 交易金額
                if (!string.IsNullOrEmpty(TOTAL_AMOUNT_S))
                {
                    sb.Append(" AND SD.TOTAL_AMOUNT >= ");
                    sb.Append(Advtek.Utility.OracleDBUtil.SqlStr(TOTAL_AMOUNT_S.Trim()));
                    sb.AppendLine();
                }
                if (!string.IsNullOrEmpty(TOTAL_AMOUNT_E))
                {
                    sb.Append(" AND SD.TOTAL_AMOUNT <= ");
                    sb.Append(Advtek.Utility.OracleDBUtil.SqlStr(TOTAL_AMOUNT_E.Trim()));
                    sb.AppendLine();
                }
                #endregion
                #region 員工編號
                if (!string.IsNullOrEmpty(SALE_PERSON_S))
                {
                    sb.Append(" AND SH.SALE_PERSON >= ");
                    sb.Append(Advtek.Utility.OracleDBUtil.SqlStr(SALE_PERSON_S.Trim()));
                    sb.AppendLine();
                }
                if (!string.IsNullOrEmpty(SALE_PERSON_E))
                {
                    sb.Append(" AND SH.SALE_PERSON <= ");
                    sb.Append(Advtek.Utility.OracleDBUtil.SqlStr(SALE_PERSON_E.Trim()));
                    sb.AppendLine();
                }
                #endregion
                #endregion
                sb.AppendFormat("   GROUP BY SH.STORE_NO ,SH.MACHINE_ID   \n");
                sb.AppendFormat("   UNION   \n");
                sb.AppendFormat("  --維修收入  \n");
                sb.AppendFormat("  SELECT '維修收入' AS TITLE ,SH.STORE_NO ,SH.MACHINE_ID   \n");
                sb.AppendFormat("        ,NVL(SUM(SD.TOTAL_AMOUNT),0) AS CASH_AMT           \n");
                sb.AppendFormat("        ,0 AS CARD_AMT                                     \n");
                sb.AppendFormat("        ,0 AS Installment_AMT                              \n");
                sb.AppendFormat("        ,0 AS DebitCard_AMT                                \n");
                sb.AppendFormat("        ,0 AS GIFT_AMT                                     \n");
                sb.AppendFormat("        ,0 AS HG_AMT                                       \n");
                sb.AppendFormat("    FROM SALE_DETAIL SD ,SALE_HEAD SH ,HRS_POSFIXI FIXH ,PRODUCT PROD   \n");
                sb.AppendFormat("        ,VOUCHER                                           \n");
                sb.AppendFormat("   WHERE SD.POSUUID_MASTER = SH.POSUUID_MASTER             \n");
                sb.AppendFormat("     AND SH.POSUUID_MASTER = VOUCHER.POSUUID_MASTER        \n");
                sb.AppendFormat("     AND SD.HRS_NO = FIXH.FIXNO                            \n");
                sb.AppendFormat("     AND SD.PRODNO = PROD.PRODNO                           \n");
                #region -WHERE-
                #region 門市編號
                if (!string.IsNullOrEmpty(STORE_NO_S))
                {
                    sb.Append(" AND SH.STORE_NO >= ");
                    sb.Append(Advtek.Utility.OracleDBUtil.SqlStr(STORE_NO_S.Trim()));
                    sb.AppendLine();
                }
                if (!string.IsNullOrEmpty(STORE_NO_E))
                {
                    sb.Append(" AND SH.STORE_NO <= ");
                    sb.Append(Advtek.Utility.OracleDBUtil.SqlStr(STORE_NO_E.Trim()));
                    sb.AppendLine();
                }
                #endregion
                #region 交易日期
                if (!string.IsNullOrEmpty(TRADE_DATE_S))
                {
                    sb.Append(" AND TRUNC(SH.TRADE_DATE) >= ");
                    sb.Append(Advtek.Utility.OracleDBUtil.DateStr(TRADE_DATE_S.Trim()));
                    sb.AppendLine();
                }
                if (!string.IsNullOrEmpty(TRADE_DATE_E))
                {
                    sb.Append(" AND TRUNC(SH.TRADE_DATE) <= ");
                    sb.Append(Advtek.Utility.OracleDBUtil.DateStr(TRADE_DATE_E.Trim()));
                    sb.AppendLine();
                }
                #endregion
                #region 機台編號
                if (!string.IsNullOrEmpty(MACHINE_ID) && MACHINE_ID.ToUpper().Trim() != "ALL")
                {
                    sb.Append(" AND SH.MACHINE_ID = ");
                    sb.Append(Advtek.Utility.OracleDBUtil.SqlStr(MACHINE_ID.Trim()));
                    sb.AppendLine();
                }
                else if (string.IsNullOrEmpty(MACHINE_ID) && MACHINE_ID.ToUpper().Trim() == "ALL")
                {
                    sb.Append(" AND SH.MACHINE_ID IN (SELECT HOST_NO FROM STORE_TERMINATING_MACHINE WHERE 1=1 ");

                    if (!string.IsNullOrEmpty(STORE_NO_S))
                    {
                        sb.Append(" AND STORE_NO >= ");
                        sb.Append(Advtek.Utility.OracleDBUtil.SqlStr(STORE_NO_S.Trim()));
                    }
                    if (!string.IsNullOrEmpty(STORE_NO_E))
                    {
                        sb.Append(" AND STORE_NO <= ");
                        sb.Append(Advtek.Utility.OracleDBUtil.SqlStr(STORE_NO_E.Trim()));
                    }

                    sb.Append(" ) ");
                    sb.AppendLine();
                }
                #endregion
                #region 門號
                if (!string.IsNullOrEmpty(MSISDN))
                {
                    sb.Append(" AND SD.MSISDN = ");
                    sb.Append(Advtek.Utility.OracleDBUtil.SqlStr(MSISDN.Trim()));
                    sb.AppendLine();
                }
                #endregion
                #region 商品類別
                if (!string.IsNullOrEmpty(PRODTYPENO_S))
                {
                    sb.Append(" AND PROD.PRODTYPENO >= ");
                    sb.Append(Advtek.Utility.OracleDBUtil.SqlStr(PRODTYPENO_S.Trim()));
                    sb.AppendLine();
                }
                if (!string.IsNullOrEmpty(PRODTYPENO_E))
                {
                    sb.Append(" AND PROD.PRODTYPENO <= ");
                    sb.Append(Advtek.Utility.OracleDBUtil.SqlStr(PRODTYPENO_E.Trim()));
                    sb.AppendLine();
                }
                #endregion
                #region 商品料號
                if (!string.IsNullOrEmpty(PRODNO_S))
                {
                    sb.Append(" AND SD.PRODNO >= ");
                    sb.Append(Advtek.Utility.OracleDBUtil.SqlStr(PRODNO_S.Trim()));
                    sb.AppendLine();
                }
                if (!string.IsNullOrEmpty(PRODNO_E))
                {
                    sb.Append(" AND SD.PRODNO <= ");
                    sb.Append(Advtek.Utility.OracleDBUtil.SqlStr(PRODNO_E.Trim()));
                    sb.AppendLine();
                }
                #endregion
                #region 交易金額
                if (!string.IsNullOrEmpty(TOTAL_AMOUNT_S))
                {
                    sb.Append(" AND SD.TOTAL_AMOUNT >= ");
                    sb.Append(Advtek.Utility.OracleDBUtil.SqlStr(TOTAL_AMOUNT_S.Trim()));
                    sb.AppendLine();
                }
                if (!string.IsNullOrEmpty(TOTAL_AMOUNT_E))
                {
                    sb.Append(" AND SD.TOTAL_AMOUNT <= ");
                    sb.Append(Advtek.Utility.OracleDBUtil.SqlStr(TOTAL_AMOUNT_E.Trim()));
                    sb.AppendLine();
                }
                #endregion
                #region 員工編號
                if (!string.IsNullOrEmpty(SALE_PERSON_S))
                {
                    sb.Append(" AND SH.SALE_PERSON >= ");
                    sb.Append(Advtek.Utility.OracleDBUtil.SqlStr(SALE_PERSON_S.Trim()));
                    sb.AppendLine();
                }
                if (!string.IsNullOrEmpty(SALE_PERSON_E))
                {
                    sb.Append(" AND SH.SALE_PERSON <= ");
                    sb.Append(Advtek.Utility.OracleDBUtil.SqlStr(SALE_PERSON_E.Trim()));
                    sb.AppendLine();
                }
                #endregion
                #endregion
                //sb.AppendFormat("     AND SD.PRODNO = '700100004'                           \n");
                sb.AppendFormat("     AND SD.PRODNO = (SELECT PARA_VALUE FROM SYS_PARA WHERE PARA_KEY = 'PHONE_REPAIR_ITEM_CODE')   \n");
                sb.AppendFormat("     AND SD.SOURCE_TYPE = 6                                \n");
                //sb.AppendFormat("     AND SH.SALE_STATUS = '2'                              \n");
                sb.AppendFormat("   GROUP BY SH.STORE_NO ,SH.MACHINE_ID                     \n");
                sb.AppendFormat("   UNION   \n");
                sb.AppendFormat("  --租賃收入  \n");
                //租賃收入=>為第二階段功能
                sb.AppendFormat("  SELECT '租賃收入' AS TITLE ,SH.STORE_NO ,SH.MACHINE_ID  \n");
                sb.AppendFormat("        ,NVL(SUM(DECODE(PD.PAID_MODE,1,PD.PAID_AMOUNT)),0) AS CASH_AMT  \n");
                sb.AppendFormat("        ,NVL(SUM(DECODE(PD.PAID_MODE,2,PD.PAID_AMOUNT)),0) AS CARD_AMT  \n");
                //sb.AppendFormat("        ,NVL(SUM(DECODE(PD.PAID_MODE,2,PD.PAID_AMOUNT)),0) AS CARD_AMT2  \n");
                sb.AppendFormat("        ,NVL(SUM(DECODE(PD.PAID_MODE,4,PD.PAID_AMOUNT)),0) AS Installment_AMT  \n");
                sb.AppendFormat("        ,NVL(SUM(DECODE(PD.PAID_MODE,6,PD.PAID_AMOUNT)),0) AS DebitCard_AMT  \n");
                sb.AppendFormat("        ,NVL(SUM(DECODE(PD.PAID_MODE,5,PD.PAID_AMOUNT)),0) AS GIFT_AMT  \n");
                sb.AppendFormat("        ,NVL(SUM(DECODE(PD.PAID_MODE,7,PD.PAID_AMOUNT)),0) AS HG_AMT  \n");
                sb.AppendFormat("    FROM SALE_HEAD SH ,SALE_DETAIL SD ,PAID_DETAIL PD ,PRODUCT PROD   \n");
                sb.AppendFormat("        ,VOUCHER  \n");
                sb.AppendFormat("   WHERE SH.POSUUID_MASTER = SD.POSUUID_MASTER  \n");
                sb.AppendFormat("     AND SH.POSUUID_MASTER = PD.POSUUID_MASTER   \n");
                sb.AppendFormat("     AND SH.POSUUID_MASTER = VOUCHER.POSUUID_MASTER   \n");
                sb.AppendFormat("     AND SD.PRODNO = PROD.PRODNO   \n");
                sb.AppendFormat("     AND SD.PRODNO IN (SELECT GUARANTEE_PRODNO FROM GUARANTEE_PROD_MAPPING     \n");
                sb.AppendFormat("         WHERE COMPANYCODE = '01' /* 遠傳 */ AND GUARANTEE_ID = '2' /* 租機服務 */ )   \n");
                //sb.AppendFormat("     AND SH.SALE_STATUS = '2'  \n");
                sb.AppendFormat("   GROUP BY SH.STORE_NO ,SH.MACHINE_ID   \n");
                sb.AppendFormat("   UNION   \n");
                sb.AppendFormat("  --預購收入  \n");
                sb.AppendFormat("  SELECT '預購收入' AS TITLE ,SH.STORE_NO ,SH.MACHINE_ID  \n");
                sb.AppendFormat("        ,NVL(SUM(DECODE(PD.PAID_MODE,1,PD.PAID_AMOUNT)),0) AS CASH_AMT  \n");
                sb.AppendFormat("        ,NVL(SUM(DECODE(PD.PAID_MODE,2,PD.PAID_AMOUNT)),0) AS CARD_AMT  \n");
                //sb.AppendFormat("        ,NVL(SUM(DECODE(PD.PAID_MODE,2,PD.PAID_AMOUNT)),0) AS CARD_AMT2  \n");
                sb.AppendFormat("        ,NVL(SUM(DECODE(PD.PAID_MODE,4,PD.PAID_AMOUNT)),0) AS Installment_AMT  \n");
                sb.AppendFormat("        ,NVL(SUM(DECODE(PD.PAID_MODE,6,PD.PAID_AMOUNT)),0) AS DebitCard_AMT  \n");
                sb.AppendFormat("        ,NVL(SUM(DECODE(PD.PAID_MODE,5,PD.PAID_AMOUNT)),0) AS GIFT_AMT  \n");
                sb.AppendFormat("        ,NVL(SUM(DECODE(PD.PAID_MODE,7,PD.PAID_AMOUNT)),0) AS HG_AMT  \n");
                sb.AppendFormat("    FROM SALE_HEAD SH ,SALE_DETAIL SD ,PAID_DETAIL PD ,PRODUCT PROD   \n");
                sb.AppendFormat("        ,VOUCHER  \n");
                sb.AppendFormat("   WHERE SH.POSUUID_MASTER = SD.POSUUID_MASTER  \n");
                sb.AppendFormat("     AND SH.POSUUID_MASTER = PD.POSUUID_MASTER   \n");
                sb.AppendFormat("     AND SH.POSUUID_MASTER = VOUCHER.POSUUID_MASTER   \n");
                sb.AppendFormat("     AND SD.PRODNO = PROD.PRODNO   \n");
                sb.AppendFormat("     AND SD.PRODNO IN (SELECT PARA_VALUE FROM SYS_PARA   \n");
                sb.AppendFormat("         WHERE SYS_PARA_TYPE_ID = '52D05731B723410EAE13CEC6F2CE3654')  \n");
                //sb.AppendFormat("     AND SH.SALE_STATUS = '2'  \n");
                sb.AppendFormat("   GROUP BY SH.STORE_NO ,SH.MACHINE_ID   \n");
                #endregion
                sb.AppendFormat("   UNION   \n");
                #region -保證金-
                sb.AppendFormat("  --遠傳保證金  \n");
                sb.AppendFormat("  SELECT '遠傳保證金' AS TITLE ,SH.STORE_NO ,SH.MACHINE_ID     \n");
                sb.AppendFormat("        ,(SUM(SD.TOTAL_AMOUNT)) AS CASH_AMT               \n");
                sb.AppendFormat("        ,0 AS CARD_AMT         \n");
                sb.AppendFormat("        ,0 AS Installment_AMT  \n");
                sb.AppendFormat("        ,0 AS DebitCard_AMT    \n");
                sb.AppendFormat("        ,0 AS GIFT_AMT         \n");
                sb.AppendFormat("        ,0 AS HG_AMT           \n");
                sb.AppendFormat("    FROM SALE_HEAD SH ,SALE_DETAIL SD ,PRODUCT PROD    \n");
                //sb.AppendFormat("        ,VOUCHER  \n");
                sb.AppendFormat("   WHERE SH.POSUUID_MASTER = SD.POSUUID_MASTER \n");
                sb.AppendFormat("     AND SD.PRODNO = PROD.PRODNO               \n");
                //sb.AppendFormat("     AND SH.POSUUID_MASTER = VOUCHER.POSUUID_MASTER   \n");
                sb.AppendFormat("     AND SD.PRODNO IN (SELECT GUARANTEE_PRODNO FROM GUARANTEE_PROD_MAPPING WHERE COMPANYCODE = '01' AND GUARANTEE_ID = '1' )    \n");
                sb.AppendFormat("     AND SD.FUN_ID in ('180','150','11')       \n");
                sb.AppendFormat("     AND SD.SOURCE_TYPE in (4,6,8)             \n");
                //sb.AppendFormat("     AND NVL(SD.TOTAL_AMOUNT,0) > 0            \n");
                //sb.AppendFormat("     AND SH.SALE_STATUS = '2'                  \n");
                #region -WHERE-
                #region 門市編號
                if (!string.IsNullOrEmpty(STORE_NO_S))
                {
                    sb.Append(" AND SH.STORE_NO >= ");
                    sb.Append(Advtek.Utility.OracleDBUtil.SqlStr(STORE_NO_S.Trim()));
                    sb.AppendLine();
                }
                if (!string.IsNullOrEmpty(STORE_NO_E))
                {
                    sb.Append(" AND SH.STORE_NO <= ");
                    sb.Append(Advtek.Utility.OracleDBUtil.SqlStr(STORE_NO_E.Trim()));
                    sb.AppendLine();
                }
                #endregion
                #region 交易日期
                if (!string.IsNullOrEmpty(TRADE_DATE_S))
                {
                    sb.Append(" AND TRUNC(SH.TRADE_DATE) >= ");
                    sb.Append(Advtek.Utility.OracleDBUtil.DateStr(TRADE_DATE_S.Trim()));
                    sb.AppendLine();
                }
                if (!string.IsNullOrEmpty(TRADE_DATE_E))
                {
                    sb.Append(" AND TRUNC(SH.TRADE_DATE) <= ");
                    sb.Append(Advtek.Utility.OracleDBUtil.DateStr(TRADE_DATE_E.Trim()));
                    sb.AppendLine();
                }
                #endregion
                #region 機台編號
                if (!string.IsNullOrEmpty(MACHINE_ID) && MACHINE_ID.ToUpper().Trim() != "ALL")
                {
                    sb.Append(" AND SH.MACHINE_ID = ");
                    sb.Append(Advtek.Utility.OracleDBUtil.SqlStr(MACHINE_ID.Trim()));
                    sb.AppendLine();
                }
                else if (string.IsNullOrEmpty(MACHINE_ID) && MACHINE_ID.ToUpper().Trim() == "ALL")
                {
                    sb.Append(" AND SH.MACHINE_ID IN (SELECT HOST_NO FROM STORE_TERMINATING_MACHINE WHERE 1=1 ");

                    if (!string.IsNullOrEmpty(STORE_NO_S))
                    {
                        sb.Append(" AND STORE_NO >= ");
                        sb.Append(Advtek.Utility.OracleDBUtil.SqlStr(STORE_NO_S.Trim()));
                    }
                    if (!string.IsNullOrEmpty(STORE_NO_E))
                    {
                        sb.Append(" AND STORE_NO <= ");
                        sb.Append(Advtek.Utility.OracleDBUtil.SqlStr(STORE_NO_E.Trim()));
                    }

                    sb.Append(" ) ");
                    sb.AppendLine();
                }
                #endregion
                #region 門號
                if (!string.IsNullOrEmpty(MSISDN))
                {
                    sb.Append(" AND SD.MSISDN = ");
                    sb.Append(Advtek.Utility.OracleDBUtil.SqlStr(MSISDN.Trim()));
                    sb.AppendLine();
                }
                #endregion
                #region 商品類別
                if (!string.IsNullOrEmpty(PRODTYPENO_S))
                {
                    sb.Append(" AND PROD.PRODTYPENO >= ");
                    sb.Append(Advtek.Utility.OracleDBUtil.SqlStr(PRODTYPENO_S.Trim()));
                    sb.AppendLine();
                }
                if (!string.IsNullOrEmpty(PRODTYPENO_E))
                {
                    sb.Append(" AND PROD.PRODTYPENO <= ");
                    sb.Append(Advtek.Utility.OracleDBUtil.SqlStr(PRODTYPENO_E.Trim()));
                    sb.AppendLine();
                }
                #endregion
                #region 商品料號
                if (!string.IsNullOrEmpty(PRODNO_S))
                {
                    sb.Append(" AND SD.PRODNO >= ");
                    sb.Append(Advtek.Utility.OracleDBUtil.SqlStr(PRODNO_S.Trim()));
                    sb.AppendLine();
                }
                if (!string.IsNullOrEmpty(PRODNO_E))
                {
                    sb.Append(" AND SD.PRODNO <= ");
                    sb.Append(Advtek.Utility.OracleDBUtil.SqlStr(PRODNO_E.Trim()));
                    sb.AppendLine();
                }
                #endregion
                #region 交易金額
                if (!string.IsNullOrEmpty(TOTAL_AMOUNT_S))
                {
                    sb.Append(" AND SD.TOTAL_AMOUNT >= ");
                    sb.Append(Advtek.Utility.OracleDBUtil.SqlStr(TOTAL_AMOUNT_S.Trim()));
                    sb.AppendLine();
                }
                if (!string.IsNullOrEmpty(TOTAL_AMOUNT_E))
                {
                    sb.Append(" AND SD.TOTAL_AMOUNT <= ");
                    sb.Append(Advtek.Utility.OracleDBUtil.SqlStr(TOTAL_AMOUNT_E.Trim()));
                    sb.AppendLine();
                }
                #endregion
                #region 員工編號
                if (!string.IsNullOrEmpty(SALE_PERSON_S))
                {
                    sb.Append(" AND SH.SALE_PERSON >= ");
                    sb.Append(Advtek.Utility.OracleDBUtil.SqlStr(SALE_PERSON_S.Trim()));
                    sb.AppendLine();
                }
                if (!string.IsNullOrEmpty(SALE_PERSON_E))
                {
                    sb.Append(" AND SH.SALE_PERSON <= ");
                    sb.Append(Advtek.Utility.OracleDBUtil.SqlStr(SALE_PERSON_E.Trim()));
                    sb.AppendLine();
                }
                #endregion
                #endregion
                sb.AppendFormat("   GROUP BY SH.STORE_NO ,SH.MACHINE_ID         \n");
                sb.AppendFormat("   UNION   \n");
                sb.AppendFormat("  --和信保證金  \n");
                sb.AppendFormat("  SELECT '和信保證金' AS TITLE ,SH.STORE_NO ,SH.MACHINE_ID     \n");
                sb.AppendFormat("        ,(SUM(SD.TOTAL_AMOUNT)) AS CASH_AMT               \n");
                sb.AppendFormat("        ,0 AS CARD_AMT         \n");
                sb.AppendFormat("        ,0 AS Installment_AMT  \n");
                sb.AppendFormat("        ,0 AS DebitCard_AMT    \n");
                sb.AppendFormat("        ,0 AS GIFT_AMT         \n");
                sb.AppendFormat("        ,0 AS HG_AMT           \n");
                sb.AppendFormat("    FROM SALE_HEAD SH ,SALE_DETAIL SD ,PRODUCT PROD    \n");
                //sb.AppendFormat("        ,VOUCHER  \n");
                sb.AppendFormat("   WHERE SH.POSUUID_MASTER = SD.POSUUID_MASTER \n");
                sb.AppendFormat("     AND SD.PRODNO = PROD.PRODNO               \n");
                //sb.AppendFormat("     AND SH.POSUUID_MASTER = VOUCHER.POSUUID_MASTER   \n");
                sb.AppendFormat("     AND SD.PRODNO IN (SELECT GUARANTEE_PRODNO FROM GUARANTEE_PROD_MAPPING WHERE COMPANYCODE = '02' AND GUARANTEE_ID = '1' )    \n");
                sb.AppendFormat("     AND SD.FUN_ID in ('180','150','11')       \n");
                sb.AppendFormat("     AND SD.SOURCE_TYPE in (4,6,8)             \n");
                //sb.AppendFormat("     AND NVL(SD.TOTAL_AMOUNT,0) > 0            \n");
                //sb.AppendFormat("     AND SH.SALE_STATUS = '2'                  \n");
                #region -WHERE-
                #region 門市編號
                if (!string.IsNullOrEmpty(STORE_NO_S))
                {
                    sb.Append(" AND SH.STORE_NO >= ");
                    sb.Append(Advtek.Utility.OracleDBUtil.SqlStr(STORE_NO_S.Trim()));
                    sb.AppendLine();
                }
                if (!string.IsNullOrEmpty(STORE_NO_E))
                {
                    sb.Append(" AND SH.STORE_NO <= ");
                    sb.Append(Advtek.Utility.OracleDBUtil.SqlStr(STORE_NO_E.Trim()));
                    sb.AppendLine();
                }
                #endregion
                #region 交易日期
                if (!string.IsNullOrEmpty(TRADE_DATE_S))
                {
                    sb.Append(" AND TRUNC(SH.TRADE_DATE) >= ");
                    sb.Append(Advtek.Utility.OracleDBUtil.DateStr(TRADE_DATE_S.Trim()));
                    sb.AppendLine();
                }
                if (!string.IsNullOrEmpty(TRADE_DATE_E))
                {
                    sb.Append(" AND TRUNC(SH.TRADE_DATE) <= ");
                    sb.Append(Advtek.Utility.OracleDBUtil.DateStr(TRADE_DATE_E.Trim()));
                    sb.AppendLine();
                }
                #endregion
                #region 機台編號
                if (!string.IsNullOrEmpty(MACHINE_ID) && MACHINE_ID.ToUpper().Trim() != "ALL")
                {
                    sb.Append(" AND SH.MACHINE_ID = ");
                    sb.Append(Advtek.Utility.OracleDBUtil.SqlStr(MACHINE_ID.Trim()));
                    sb.AppendLine();
                }
                else if (string.IsNullOrEmpty(MACHINE_ID) && MACHINE_ID.ToUpper().Trim() == "ALL")
                {
                    sb.Append(" AND SH.MACHINE_ID IN (SELECT HOST_NO FROM STORE_TERMINATING_MACHINE WHERE 1=1 ");

                    if (!string.IsNullOrEmpty(STORE_NO_S))
                    {
                        sb.Append(" AND STORE_NO >= ");
                        sb.Append(Advtek.Utility.OracleDBUtil.SqlStr(STORE_NO_S.Trim()));
                    }
                    if (!string.IsNullOrEmpty(STORE_NO_E))
                    {
                        sb.Append(" AND STORE_NO <= ");
                        sb.Append(Advtek.Utility.OracleDBUtil.SqlStr(STORE_NO_E.Trim()));
                    }

                    sb.Append(" ) ");
                    sb.AppendLine();
                }
                #endregion
                #region 門號
                if (!string.IsNullOrEmpty(MSISDN))
                {
                    sb.Append(" AND SD.MSISDN = ");
                    sb.Append(Advtek.Utility.OracleDBUtil.SqlStr(MSISDN.Trim()));
                    sb.AppendLine();
                }
                #endregion
                #region 商品類別
                if (!string.IsNullOrEmpty(PRODTYPENO_S))
                {
                    sb.Append(" AND PROD.PRODTYPENO >= ");
                    sb.Append(Advtek.Utility.OracleDBUtil.SqlStr(PRODTYPENO_S.Trim()));
                    sb.AppendLine();
                }
                if (!string.IsNullOrEmpty(PRODTYPENO_E))
                {
                    sb.Append(" AND PROD.PRODTYPENO <= ");
                    sb.Append(Advtek.Utility.OracleDBUtil.SqlStr(PRODTYPENO_E.Trim()));
                    sb.AppendLine();
                }
                #endregion
                #region 商品料號
                if (!string.IsNullOrEmpty(PRODNO_S))
                {
                    sb.Append(" AND SD.PRODNO >= ");
                    sb.Append(Advtek.Utility.OracleDBUtil.SqlStr(PRODNO_S.Trim()));
                    sb.AppendLine();
                }
                if (!string.IsNullOrEmpty(PRODNO_E))
                {
                    sb.Append(" AND SD.PRODNO <= ");
                    sb.Append(Advtek.Utility.OracleDBUtil.SqlStr(PRODNO_E.Trim()));
                    sb.AppendLine();
                }
                #endregion
                #region 交易金額
                if (!string.IsNullOrEmpty(TOTAL_AMOUNT_S))
                {
                    sb.Append(" AND SD.TOTAL_AMOUNT >= ");
                    sb.Append(Advtek.Utility.OracleDBUtil.SqlStr(TOTAL_AMOUNT_S.Trim()));
                    sb.AppendLine();
                }
                if (!string.IsNullOrEmpty(TOTAL_AMOUNT_E))
                {
                    sb.Append(" AND SD.TOTAL_AMOUNT <= ");
                    sb.Append(Advtek.Utility.OracleDBUtil.SqlStr(TOTAL_AMOUNT_E.Trim()));
                    sb.AppendLine();
                }
                #endregion
                #region 員工編號
                if (!string.IsNullOrEmpty(SALE_PERSON_S))
                {
                    sb.Append(" AND SH.SALE_PERSON >= ");
                    sb.Append(Advtek.Utility.OracleDBUtil.SqlStr(SALE_PERSON_S.Trim()));
                    sb.AppendLine();
                }
                if (!string.IsNullOrEmpty(SALE_PERSON_E))
                {
                    sb.Append(" AND SH.SALE_PERSON <= ");
                    sb.Append(Advtek.Utility.OracleDBUtil.SqlStr(SALE_PERSON_E.Trim()));
                    sb.AppendLine();
                }
                #endregion
                #endregion
                sb.AppendFormat("   GROUP BY SH.STORE_NO ,SH.MACHINE_ID         \n");
                sb.AppendFormat("   UNION   \n");
                sb.AppendFormat("  --遠通保證金  \n");
                sb.AppendFormat("  SELECT '遠通保證金' AS TITLE ,SH.STORE_NO ,SH.MACHINE_ID     \n");
                sb.AppendFormat("        ,(SUM(SD.TOTAL_AMOUNT)) AS CASH_AMT               \n");
                sb.AppendFormat("        ,0 AS CARD_AMT         \n");
                sb.AppendFormat("        ,0 AS Installment_AMT  \n");
                sb.AppendFormat("        ,0 AS DebitCard_AMT    \n");
                sb.AppendFormat("        ,0 AS GIFT_AMT         \n");
                sb.AppendFormat("        ,0 AS HG_AMT           \n");
                sb.AppendFormat("    FROM SALE_HEAD SH ,SALE_DETAIL SD ,PRODUCT PROD    \n");
                //sb.AppendFormat("        ,VOUCHER  \n");
                sb.AppendFormat("   WHERE SH.POSUUID_MASTER = SD.POSUUID_MASTER \n");
                sb.AppendFormat("     AND SD.PRODNO = PROD.PRODNO               \n");
                //sb.AppendFormat("     AND SH.POSUUID_MASTER = VOUCHER.POSUUID_MASTER   \n");
                sb.AppendFormat("     AND SD.PRODNO IN (SELECT GUARANTEE_PRODNO FROM GUARANTEE_PROD_MAPPING WHERE GUARANTEE_ID = '3'  )    \n");
                sb.AppendFormat("     AND SD.FUN_ID in ('180','150','11')       \n");
                sb.AppendFormat("     AND SD.SOURCE_TYPE in (4,6,8)             \n");
                //sb.AppendFormat("     AND NVL(SD.TOTAL_AMOUNT,0) > 0            \n");
                //sb.AppendFormat("     AND SH.SALE_STATUS = '2'                  \n");
                #region -WHERE-
                #region 門市編號
                if (!string.IsNullOrEmpty(STORE_NO_S))
                {
                    sb.Append(" AND SH.STORE_NO >= ");
                    sb.Append(Advtek.Utility.OracleDBUtil.SqlStr(STORE_NO_S.Trim()));
                    sb.AppendLine();
                }
                if (!string.IsNullOrEmpty(STORE_NO_E))
                {
                    sb.Append(" AND SH.STORE_NO <= ");
                    sb.Append(Advtek.Utility.OracleDBUtil.SqlStr(STORE_NO_E.Trim()));
                    sb.AppendLine();
                }
                #endregion
                #region 交易日期
                if (!string.IsNullOrEmpty(TRADE_DATE_S))
                {
                    sb.Append(" AND TRUNC(SH.TRADE_DATE) >= ");
                    sb.Append(Advtek.Utility.OracleDBUtil.DateStr(TRADE_DATE_S.Trim()));
                    sb.AppendLine();
                }
                if (!string.IsNullOrEmpty(TRADE_DATE_E))
                {
                    sb.Append(" AND TRUNC(SH.TRADE_DATE) <= ");
                    sb.Append(Advtek.Utility.OracleDBUtil.DateStr(TRADE_DATE_E.Trim()));
                    sb.AppendLine();
                }
                #endregion
                #region 機台編號
                if (!string.IsNullOrEmpty(MACHINE_ID) && MACHINE_ID.ToUpper().Trim() != "ALL")
                {
                    sb.Append(" AND SH.MACHINE_ID = ");
                    sb.Append(Advtek.Utility.OracleDBUtil.SqlStr(MACHINE_ID.Trim()));
                    sb.AppendLine();
                }
                else if (string.IsNullOrEmpty(MACHINE_ID) && MACHINE_ID.ToUpper().Trim() == "ALL")
                {
                    sb.Append(" AND SH.MACHINE_ID IN (SELECT HOST_NO FROM STORE_TERMINATING_MACHINE WHERE 1=1 ");

                    if (!string.IsNullOrEmpty(STORE_NO_S))
                    {
                        sb.Append(" AND STORE_NO >= ");
                        sb.Append(Advtek.Utility.OracleDBUtil.SqlStr(STORE_NO_S.Trim()));
                    }
                    if (!string.IsNullOrEmpty(STORE_NO_E))
                    {
                        sb.Append(" AND STORE_NO <= ");
                        sb.Append(Advtek.Utility.OracleDBUtil.SqlStr(STORE_NO_E.Trim()));
                    }

                    sb.Append(" ) ");
                    sb.AppendLine();
                }
                #endregion
                #region 門號
                if (!string.IsNullOrEmpty(MSISDN))
                {
                    sb.Append(" AND SD.MSISDN = ");
                    sb.Append(Advtek.Utility.OracleDBUtil.SqlStr(MSISDN.Trim()));
                    sb.AppendLine();
                }
                #endregion
                #region 商品類別
                if (!string.IsNullOrEmpty(PRODTYPENO_S))
                {
                    sb.Append(" AND PROD.PRODTYPENO >= ");
                    sb.Append(Advtek.Utility.OracleDBUtil.SqlStr(PRODTYPENO_S.Trim()));
                    sb.AppendLine();
                }
                if (!string.IsNullOrEmpty(PRODTYPENO_E))
                {
                    sb.Append(" AND PROD.PRODTYPENO <= ");
                    sb.Append(Advtek.Utility.OracleDBUtil.SqlStr(PRODTYPENO_E.Trim()));
                    sb.AppendLine();
                }
                #endregion
                #region 商品料號
                if (!string.IsNullOrEmpty(PRODNO_S))
                {
                    sb.Append(" AND SD.PRODNO >= ");
                    sb.Append(Advtek.Utility.OracleDBUtil.SqlStr(PRODNO_S.Trim()));
                    sb.AppendLine();
                }
                if (!string.IsNullOrEmpty(PRODNO_E))
                {
                    sb.Append(" AND SD.PRODNO <= ");
                    sb.Append(Advtek.Utility.OracleDBUtil.SqlStr(PRODNO_E.Trim()));
                    sb.AppendLine();
                }
                #endregion
                #region 交易金額
                if (!string.IsNullOrEmpty(TOTAL_AMOUNT_S))
                {
                    sb.Append(" AND SD.TOTAL_AMOUNT >= ");
                    sb.Append(Advtek.Utility.OracleDBUtil.SqlStr(TOTAL_AMOUNT_S.Trim()));
                    sb.AppendLine();
                }
                if (!string.IsNullOrEmpty(TOTAL_AMOUNT_E))
                {
                    sb.Append(" AND SD.TOTAL_AMOUNT <= ");
                    sb.Append(Advtek.Utility.OracleDBUtil.SqlStr(TOTAL_AMOUNT_E.Trim()));
                    sb.AppendLine();
                }
                #endregion
                #region 員工編號
                if (!string.IsNullOrEmpty(SALE_PERSON_S))
                {
                    sb.Append(" AND SH.SALE_PERSON >= ");
                    sb.Append(Advtek.Utility.OracleDBUtil.SqlStr(SALE_PERSON_S.Trim()));
                    sb.AppendLine();
                }
                if (!string.IsNullOrEmpty(SALE_PERSON_E))
                {
                    sb.Append(" AND SH.SALE_PERSON <= ");
                    sb.Append(Advtek.Utility.OracleDBUtil.SqlStr(SALE_PERSON_E.Trim()));
                    sb.AppendLine();
                }
                #endregion
                #endregion
                sb.AppendFormat("   GROUP BY SH.STORE_NO ,SH.MACHINE_ID         \n");
                sb.AppendFormat("   UNION   \n");

                sb.AppendFormat("  --租賃保證金  \n");
                sb.AppendFormat("  SELECT '租賃保證金' AS TITLE ,SH.STORE_NO ,SH.MACHINE_ID  \n");
                sb.AppendFormat("        ,(SUM(SD.TOTAL_AMOUNT)) AS CASH_AMT               \n");
                sb.AppendFormat("        ,0 AS CARD_AMT         \n");
                sb.AppendFormat("        ,0 AS Installment_AMT  \n");
                sb.AppendFormat("        ,0 AS DebitCard_AMT    \n");
                sb.AppendFormat("        ,0 AS GIFT_AMT         \n");
                sb.AppendFormat("        ,0 AS HG_AMT           \n");
                sb.AppendFormat("    FROM SALE_HEAD SH ,SALE_DETAIL SD ,PRODUCT PROD   \n");
                //sb.AppendFormat("        ,VOUCHER  \n");
                sb.AppendFormat("   WHERE SH.POSUUID_MASTER = SD.POSUUID_MASTER \n");
                sb.AppendFormat("     AND SD.PRODNO = PROD.PRODNO               \n");
                //sb.AppendFormat("     AND SH.POSUUID_MASTER = VOUCHER.POSUUID_MASTER   \n");
                sb.AppendFormat("     AND SD.PRODNO IN (SELECT GUARANTEE_PRODNO FROM GUARANTEE_PROD_MAPPING WHERE GUARANTEE_ID = '2'  )    \n");
                sb.AppendFormat("     AND SD.FUN_ID in ('180','150','11')       \n");
                sb.AppendFormat("     AND SD.SOURCE_TYPE in (4,6,8)             \n");
                //sb.AppendFormat("     AND NVL(SD.TOTAL_AMOUNT,0) > 0            \n");
                //sb.AppendFormat("     AND SH.SALE_STATUS = '2'                  \n");
                #region -WHERE-
                #region 門市編號
                if (!string.IsNullOrEmpty(STORE_NO_S))
                {
                    sb.Append(" AND SH.STORE_NO >= ");
                    sb.Append(Advtek.Utility.OracleDBUtil.SqlStr(STORE_NO_S.Trim()));
                    sb.AppendLine();
                }
                if (!string.IsNullOrEmpty(STORE_NO_E))
                {
                    sb.Append(" AND SH.STORE_NO <= ");
                    sb.Append(Advtek.Utility.OracleDBUtil.SqlStr(STORE_NO_E.Trim()));
                    sb.AppendLine();
                }
                #endregion
                #region 交易日期
                if (!string.IsNullOrEmpty(TRADE_DATE_S))
                {
                    sb.Append(" AND TRUNC(SH.TRADE_DATE) >= ");
                    sb.Append(Advtek.Utility.OracleDBUtil.DateStr(TRADE_DATE_S.Trim()));
                    sb.AppendLine();
                }
                if (!string.IsNullOrEmpty(TRADE_DATE_E))
                {
                    sb.Append(" AND TRUNC(SH.TRADE_DATE) <= ");
                    sb.Append(Advtek.Utility.OracleDBUtil.DateStr(TRADE_DATE_E.Trim()));
                    sb.AppendLine();
                }
                #endregion
                #region 機台編號
                if (!string.IsNullOrEmpty(MACHINE_ID) && MACHINE_ID.ToUpper().Trim() != "ALL")
                {
                    sb.Append(" AND SH.MACHINE_ID = ");
                    sb.Append(Advtek.Utility.OracleDBUtil.SqlStr(MACHINE_ID.Trim()));
                    sb.AppendLine();
                }
                else if (string.IsNullOrEmpty(MACHINE_ID) && MACHINE_ID.ToUpper().Trim() == "ALL")
                {
                    sb.Append(" AND SH.MACHINE_ID IN (SELECT HOST_NO FROM STORE_TERMINATING_MACHINE WHERE 1=1 ");

                    if (!string.IsNullOrEmpty(STORE_NO_S))
                    {
                        sb.Append(" AND STORE_NO >= ");
                        sb.Append(Advtek.Utility.OracleDBUtil.SqlStr(STORE_NO_S.Trim()));
                    }
                    if (!string.IsNullOrEmpty(STORE_NO_E))
                    {
                        sb.Append(" AND STORE_NO <= ");
                        sb.Append(Advtek.Utility.OracleDBUtil.SqlStr(STORE_NO_E.Trim()));
                    }

                    sb.Append(" ) ");
                    sb.AppendLine();
                }
                #endregion
                #region 門號
                if (!string.IsNullOrEmpty(MSISDN))
                {
                    sb.Append(" AND SD.MSISDN = ");
                    sb.Append(Advtek.Utility.OracleDBUtil.SqlStr(MSISDN.Trim()));
                    sb.AppendLine();
                }
                #endregion
                #region 商品類別
                if (!string.IsNullOrEmpty(PRODTYPENO_S))
                {
                    sb.Append(" AND PROD.PRODTYPENO >= ");
                    sb.Append(Advtek.Utility.OracleDBUtil.SqlStr(PRODTYPENO_S.Trim()));
                    sb.AppendLine();
                }
                if (!string.IsNullOrEmpty(PRODTYPENO_E))
                {
                    sb.Append(" AND PROD.PRODTYPENO <= ");
                    sb.Append(Advtek.Utility.OracleDBUtil.SqlStr(PRODTYPENO_E.Trim()));
                    sb.AppendLine();
                }
                #endregion
                #region 商品料號
                if (!string.IsNullOrEmpty(PRODNO_S))
                {
                    sb.Append(" AND SD.PRODNO >= ");
                    sb.Append(Advtek.Utility.OracleDBUtil.SqlStr(PRODNO_S.Trim()));
                    sb.AppendLine();
                }
                if (!string.IsNullOrEmpty(PRODNO_E))
                {
                    sb.Append(" AND SD.PRODNO <= ");
                    sb.Append(Advtek.Utility.OracleDBUtil.SqlStr(PRODNO_E.Trim()));
                    sb.AppendLine();
                }
                #endregion
                #region 交易金額
                if (!string.IsNullOrEmpty(TOTAL_AMOUNT_S))
                {
                    sb.Append(" AND SD.TOTAL_AMOUNT >= ");
                    sb.Append(Advtek.Utility.OracleDBUtil.SqlStr(TOTAL_AMOUNT_S.Trim()));
                    sb.AppendLine();
                }
                if (!string.IsNullOrEmpty(TOTAL_AMOUNT_E))
                {
                    sb.Append(" AND SD.TOTAL_AMOUNT <= ");
                    sb.Append(Advtek.Utility.OracleDBUtil.SqlStr(TOTAL_AMOUNT_E.Trim()));
                    sb.AppendLine();
                }
                #endregion
                #region 員工編號
                if (!string.IsNullOrEmpty(SALE_PERSON_S))
                {
                    sb.Append(" AND SH.SALE_PERSON >= ");
                    sb.Append(Advtek.Utility.OracleDBUtil.SqlStr(SALE_PERSON_S.Trim()));
                    sb.AppendLine();
                }
                if (!string.IsNullOrEmpty(SALE_PERSON_E))
                {
                    sb.Append(" AND SH.SALE_PERSON <= ");
                    sb.Append(Advtek.Utility.OracleDBUtil.SqlStr(SALE_PERSON_E.Trim()));
                    sb.AppendLine();
                }
                #endregion
                #endregion
                sb.AppendFormat("   GROUP BY SH.STORE_NO ,SH.MACHINE_ID         \n");
                sb.AppendFormat("   UNION   \n");

                sb.AppendFormat("  --折抵金  \n");
                sb.AppendFormat("  SELECT '折抵金' AS TITLE ,SH.STORE_NO ,SH.MACHINE_ID  \n");
                sb.AppendFormat("        ,NVL(SUM(DECODE(PD.PAID_MODE,1,PD.PAID_AMOUNT)),0) AS CASH_AMT  \n");
                sb.AppendFormat("        ,NVL(SUM(DECODE(PD.PAID_MODE,2,PD.PAID_AMOUNT)),0) AS CARD_AMT  \n");
                //sb.AppendFormat("        ,NVL(SUM(DECODE(PD.PAID_MODE,2,PD.PAID_AMOUNT)),0) AS CARD_AMT2  \n");
                sb.AppendFormat("        ,NVL(SUM(DECODE(PD.PAID_MODE,4,PD.PAID_AMOUNT)),0) AS Installment_AMT  \n");
                sb.AppendFormat("        ,NVL(SUM(DECODE(PD.PAID_MODE,6,PD.PAID_AMOUNT)),0) AS DebitCard_AMT  \n");
                sb.AppendFormat("        ,NVL(SUM(DECODE(PD.PAID_MODE,5,PD.PAID_AMOUNT)),0) AS GIFT_AMT  \n");
                sb.AppendFormat("        ,NVL(SUM(DECODE(PD.PAID_MODE,7,PD.PAID_AMOUNT)),0) AS HG_AMT  \n");
                sb.AppendFormat("    FROM SALE_HEAD SH ,SALE_DETAIL SD ,PAID_DETAIL PD ,PRODUCT PROD   \n");
                sb.AppendFormat("        ,VOUCHER  \n");
                sb.AppendFormat("   WHERE SH.POSUUID_MASTER = SD.POSUUID_MASTER  \n");
                sb.AppendFormat("     AND SH.POSUUID_MASTER = PD.POSUUID_MASTER   \n");
                sb.AppendFormat("     AND SH.POSUUID_MASTER = VOUCHER.POSUUID_MASTER   \n");
                sb.AppendFormat("     AND SD.PRODNO = PROD.PRODNO   \n");
                sb.AppendFormat("     AND SD.PRODNO IN (SELECT PARA_VALUE FROM SYS_PARA   \n");
                sb.AppendFormat("         WHERE SYS_PARA_TYPE_ID = '0B265970C46A48C290CAC6448FA14312')  \n");
                //sb.AppendFormat("     AND SH.SALE_STATUS = '2'  \n");
                sb.AppendFormat("   GROUP BY SH.STORE_NO ,SH.MACHINE_ID   \n");
                sb.AppendFormat("   UNION   \n");
                sb.AppendFormat("  --折抵點數  \n");
                sb.AppendFormat("  SELECT '折抵點數' AS TITLE ,SH.STORE_NO ,SH.MACHINE_ID  \n");
                sb.AppendFormat("        ,NVL(SUM(DECODE(PD.PAID_MODE,1,PD.PAID_AMOUNT)),0) AS CASH_AMT  \n");
                sb.AppendFormat("        ,NVL(SUM(DECODE(PD.PAID_MODE,2,PD.PAID_AMOUNT)),0) AS CARD_AMT  \n");
                //sb.AppendFormat("        ,NVL(SUM(DECODE(PD.PAID_MODE,2,PD.PAID_AMOUNT)),0) AS CARD_AMT2  \n");
                sb.AppendFormat("        ,NVL(SUM(DECODE(PD.PAID_MODE,4,PD.PAID_AMOUNT)),0) AS Installment_AMT  \n");
                sb.AppendFormat("        ,NVL(SUM(DECODE(PD.PAID_MODE,6,PD.PAID_AMOUNT)),0) AS DebitCard_AMT  \n");
                sb.AppendFormat("        ,NVL(SUM(DECODE(PD.PAID_MODE,5,PD.PAID_AMOUNT)),0) AS GIFT_AMT  \n");
                sb.AppendFormat("        ,NVL(SUM(DECODE(PD.PAID_MODE,7,PD.PAID_AMOUNT)),0) AS HG_AMT  \n");
                sb.AppendFormat("    FROM SALE_HEAD SH ,SALE_DETAIL SD ,PAID_DETAIL PD ,PRODUCT PROD   \n");
                sb.AppendFormat("        ,VOUCHER  \n");
                sb.AppendFormat("   WHERE SH.POSUUID_MASTER = SD.POSUUID_MASTER  \n");
                sb.AppendFormat("     AND SH.POSUUID_MASTER = PD.POSUUID_MASTER   \n");
                sb.AppendFormat("     AND SH.POSUUID_MASTER = VOUCHER.POSUUID_MASTER   \n");
                sb.AppendFormat("     AND SD.PRODNO = PROD.PRODNO   \n");
                sb.AppendFormat("     AND SD.PRODNO IN (SELECT PARA_VALUE FROM SYS_PARA   \n");
                sb.AppendFormat("         WHERE SYS_PARA_TYPE_ID = '')  \n");
                //sb.AppendFormat("     AND SH.SALE_STATUS = '2'  \n");
                sb.AppendFormat("   GROUP BY SH.STORE_NO ,SH.MACHINE_ID   \n");
                #endregion
                sb.AppendFormat("    \n");
                sb.AppendFormat("   UNION   \n");
                #region -代收類-
                sb.AppendFormat("  --代收遠傳帳單  \n");
                sb.AppendFormat("  SELECT '遠傳帳單' AS TITLE ,SH.STORE_NO ,SH.MACHINE_ID  \n");
                sb.AppendFormat("        ,SUM(DECODE(BD.PAY_MODE_ID,'1', BD.AMOUNT,0 )) AS CASH_AMT \n");
                sb.AppendFormat("        ,SUM(DECODE(BD.PAY_MODE_ID,'2', BD.AMOUNT,0 )) + SUM(DECODE(BD.PAY_MODE_ID,'3', BD.AMOUNT,0 )) AS CARD_AMT \n");
                sb.AppendFormat("        ,SUM(DECODE(BD.PAY_MODE_ID,'4', BD.AMOUNT,0 )) AS Installment_AMT \n");
                sb.AppendFormat("        ,SUM(DECODE(BD.PAY_MODE_ID,'5', BD.AMOUNT,0 )) AS GIFT_AMT        \n");
                sb.AppendFormat("        ,SUM(DECODE(BD.PAY_MODE_ID,'6', BD.AMOUNT,0 )) AS DebitCard_AMT   \n");
                sb.AppendFormat("        ,SUM(DECODE(BD.PAY_MODE_ID,'7', BD.AMOUNT,0 )) AS HG_AMT          \n");
                sb.AppendFormat("    FROM SALE_HEAD SH ,BILL_DISPATCH BD   \n");
                //sb.AppendFormat("        ,VOUCHER  \n");
                sb.AppendFormat("   WHERE SH.POSUUID_MASTER = BD.POSUUID_MASTER   \n");
                sb.AppendFormat("     AND BD.SALE_DETAIL_ID IN (SELECT ID FROM SALE_DETAIL SD ,PRODUCT PROD WHERE SD.PRODNO = PROD.PRODNO   \n");
                sb.AppendFormat("         AND SD.PRODNO IN (SELECT PARA_VALUE FROM SYS_PARA WHERE PARA_KEY = 'FET_BILLING_ITEM_CODE')       \n");
                #region WHERE
                #region 門號
                if (!string.IsNullOrEmpty(MSISDN))
                {
                    sb.Append(" AND SD.MSISDN = ");
                    sb.Append(Advtek.Utility.OracleDBUtil.SqlStr(MSISDN.Trim()));
                    sb.AppendLine();
                }
                #endregion
                #region 商品類別
                if (!string.IsNullOrEmpty(PRODTYPENO_S))
                {
                    sb.Append(" AND PROD.PRODTYPENO >= ");
                    sb.Append(Advtek.Utility.OracleDBUtil.SqlStr(PRODTYPENO_S.Trim()));
                    sb.AppendLine();
                }
                if (!string.IsNullOrEmpty(PRODTYPENO_E))
                {
                    sb.Append(" AND PROD.PRODTYPENO <= ");
                    sb.Append(Advtek.Utility.OracleDBUtil.SqlStr(PRODTYPENO_E.Trim()));
                    sb.AppendLine();
                }
                #endregion
                #region 商品料號
                if (!string.IsNullOrEmpty(PRODNO_S))
                {
                    sb.Append(" AND SD.PRODNO >= ");
                    sb.Append(Advtek.Utility.OracleDBUtil.SqlStr(PRODNO_S.Trim()));
                    sb.AppendLine();
                }
                if (!string.IsNullOrEmpty(PRODNO_E))
                {
                    sb.Append(" AND SD.PRODNO <= ");
                    sb.Append(Advtek.Utility.OracleDBUtil.SqlStr(PRODNO_E.Trim()));
                    sb.AppendLine();
                }
                #endregion
                #endregion
                sb.AppendFormat("         )                     \n");
                //sb.AppendFormat("     AND SH.POSUUID_MASTER = VOUCHER.POSUUID_MASTER   \n");
                //sb.AppendFormat("     AND SH.SALE_STATUS = '2'  \n");
                sb.AppendFormat("     AND SH.SALE_TYPE = 2      \n");
                #region -WHERE-
                #region 門市編號
                if (!string.IsNullOrEmpty(STORE_NO_S))
                {
                    sb.Append(" AND SH.STORE_NO >= ");
                    sb.Append(Advtek.Utility.OracleDBUtil.SqlStr(STORE_NO_S.Trim()));
                    sb.AppendLine();
                }
                if (!string.IsNullOrEmpty(STORE_NO_E))
                {
                    sb.Append(" AND SH.STORE_NO <= ");
                    sb.Append(Advtek.Utility.OracleDBUtil.SqlStr(STORE_NO_E.Trim()));
                    sb.AppendLine();
                }
                #endregion
                #region 交易日期
                if (!string.IsNullOrEmpty(TRADE_DATE_S))
                {
                    sb.Append(" AND TRUNC(SH.TRADE_DATE) >= ");
                    sb.Append(Advtek.Utility.OracleDBUtil.DateStr(TRADE_DATE_S.Trim()));
                    sb.AppendLine();
                }
                if (!string.IsNullOrEmpty(TRADE_DATE_E))
                {
                    sb.Append(" AND TRUNC(SH.TRADE_DATE) <= ");
                    sb.Append(Advtek.Utility.OracleDBUtil.DateStr(TRADE_DATE_E.Trim()));
                    sb.AppendLine();
                }
                #endregion
                #region 機台編號
                if (!string.IsNullOrEmpty(MACHINE_ID) && MACHINE_ID.ToUpper().Trim() != "ALL")
                {
                    sb.Append(" AND SH.MACHINE_ID = ");
                    sb.Append(Advtek.Utility.OracleDBUtil.SqlStr(MACHINE_ID.Trim()));
                    sb.AppendLine();
                }
                else if (string.IsNullOrEmpty(MACHINE_ID) && MACHINE_ID.ToUpper().Trim() == "ALL")
                {
                    sb.Append(" AND SH.MACHINE_ID IN (SELECT HOST_NO FROM STORE_TERMINATING_MACHINE WHERE 1=1 ");

                    if (!string.IsNullOrEmpty(STORE_NO_S))
                    {
                        sb.Append(" AND STORE_NO >= ");
                        sb.Append(Advtek.Utility.OracleDBUtil.SqlStr(STORE_NO_S.Trim()));
                    }
                    if (!string.IsNullOrEmpty(STORE_NO_E))
                    {
                        sb.Append(" AND STORE_NO <= ");
                        sb.Append(Advtek.Utility.OracleDBUtil.SqlStr(STORE_NO_E.Trim()));
                    }

                    sb.Append(" ) ");
                    sb.AppendLine();
                }
                #endregion
                #region 員工編號
                if (!string.IsNullOrEmpty(SALE_PERSON_S))
                {
                    sb.Append(" AND SH.SALE_PERSON >= ");
                    sb.Append(Advtek.Utility.OracleDBUtil.SqlStr(SALE_PERSON_S.Trim()));
                    sb.AppendLine();
                }
                if (!string.IsNullOrEmpty(SALE_PERSON_E))
                {
                    sb.Append(" AND SH.SALE_PERSON <= ");
                    sb.Append(Advtek.Utility.OracleDBUtil.SqlStr(SALE_PERSON_E.Trim()));
                    sb.AppendLine();
                }
                #endregion
                #endregion
                sb.AppendFormat("   GROUP BY SH.STORE_NO ,SH.MACHINE_ID     \n");
                sb.AppendFormat("   UNION   \n");
                sb.AppendFormat("  --代收和信帳單  \n");
                sb.AppendFormat("  SELECT '和信帳單' AS TITLE ,SH.STORE_NO ,SH.MACHINE_ID      \n");
                sb.AppendFormat("        ,SUM(DECODE(BD.PAY_MODE_ID,'1', BD.AMOUNT,0 )) AS CASH_AMT \n");
                sb.AppendFormat("        ,SUM(DECODE(BD.PAY_MODE_ID,'2', BD.AMOUNT,0 )) + SUM(DECODE(BD.PAY_MODE_ID,'3', BD.AMOUNT,0 )) AS CARD_AMT \n");
                sb.AppendFormat("        ,SUM(DECODE(BD.PAY_MODE_ID,'4', BD.AMOUNT,0 )) AS Installment_AMT \n");
                sb.AppendFormat("        ,SUM(DECODE(BD.PAY_MODE_ID,'5', BD.AMOUNT,0 )) AS GIFT_AMT        \n");
                sb.AppendFormat("        ,SUM(DECODE(BD.PAY_MODE_ID,'6', BD.AMOUNT,0 )) AS DebitCard_AMT   \n");
                sb.AppendFormat("        ,SUM(DECODE(BD.PAY_MODE_ID,'7', BD.AMOUNT,0 )) AS HG_AMT          \n");
                sb.AppendFormat("    FROM SALE_HEAD SH ,BILL_DISPATCH BD   \n");
                //sb.AppendFormat("        ,VOUCHER  \n");
                sb.AppendFormat("   WHERE SH.POSUUID_MASTER = BD.POSUUID_MASTER   \n");
                sb.AppendFormat("     AND BD.SALE_DETAIL_ID IN (SELECT ID FROM SALE_DETAIL SD ,PRODUCT PROD WHERE SD.PRODNO = PROD.PRODNO   \n");
                sb.AppendFormat("         AND SD.PRODNO IN (SELECT PARA_VALUE FROM SYS_PARA WHERE PARA_KEY = 'KGT_BILLING_ITEM_CODE')       \n");
                #region WHERE
                #region 門號
                if (!string.IsNullOrEmpty(MSISDN))
                {
                    sb.Append(" AND SD.MSISDN = ");
                    sb.Append(Advtek.Utility.OracleDBUtil.SqlStr(MSISDN.Trim()));
                    sb.AppendLine();
                }
                #endregion
                #region 商品類別
                if (!string.IsNullOrEmpty(PRODTYPENO_S))
                {
                    sb.Append(" AND PROD.PRODTYPENO >= ");
                    sb.Append(Advtek.Utility.OracleDBUtil.SqlStr(PRODTYPENO_S.Trim()));
                    sb.AppendLine();
                }
                if (!string.IsNullOrEmpty(PRODTYPENO_E))
                {
                    sb.Append(" AND PROD.PRODTYPENO <= ");
                    sb.Append(Advtek.Utility.OracleDBUtil.SqlStr(PRODTYPENO_E.Trim()));
                    sb.AppendLine();
                }
                #endregion
                #region 商品料號
                if (!string.IsNullOrEmpty(PRODNO_S))
                {
                    sb.Append(" AND SD.PRODNO >= ");
                    sb.Append(Advtek.Utility.OracleDBUtil.SqlStr(PRODNO_S.Trim()));
                    sb.AppendLine();
                }
                if (!string.IsNullOrEmpty(PRODNO_E))
                {
                    sb.Append(" AND SD.PRODNO <= ");
                    sb.Append(Advtek.Utility.OracleDBUtil.SqlStr(PRODNO_E.Trim()));
                    sb.AppendLine();
                }
                #endregion
                #endregion
                sb.AppendFormat("         )                     \n");
                //sb.AppendFormat("     AND SH.POSUUID_MASTER = VOUCHER.POSUUID_MASTER   \n");
                //sb.AppendFormat("     AND SH.SALE_STATUS = '2'  \n");
                sb.AppendFormat("     AND SH.SALE_TYPE = 2      \n");
                #region -WHERE-
                #region 門市編號
                if (!string.IsNullOrEmpty(STORE_NO_S))
                {
                    sb.Append(" AND SH.STORE_NO >= ");
                    sb.Append(Advtek.Utility.OracleDBUtil.SqlStr(STORE_NO_S.Trim()));
                    sb.AppendLine();
                }
                if (!string.IsNullOrEmpty(STORE_NO_E))
                {
                    sb.Append(" AND SH.STORE_NO <= ");
                    sb.Append(Advtek.Utility.OracleDBUtil.SqlStr(STORE_NO_E.Trim()));
                    sb.AppendLine();
                }
                #endregion
                #region 交易日期
                if (!string.IsNullOrEmpty(TRADE_DATE_S))
                {
                    sb.Append(" AND TRUNC(SH.TRADE_DATE) >= ");
                    sb.Append(Advtek.Utility.OracleDBUtil.DateStr(TRADE_DATE_S.Trim()));
                    sb.AppendLine();
                }
                if (!string.IsNullOrEmpty(TRADE_DATE_E))
                {
                    sb.Append(" AND TRUNC(SH.TRADE_DATE) <= ");
                    sb.Append(Advtek.Utility.OracleDBUtil.DateStr(TRADE_DATE_E.Trim()));
                    sb.AppendLine();
                }
                #endregion
                #region 機台編號
                if (!string.IsNullOrEmpty(MACHINE_ID) && MACHINE_ID.ToUpper().Trim() != "ALL")
                {
                    sb.Append(" AND SH.MACHINE_ID = ");
                    sb.Append(Advtek.Utility.OracleDBUtil.SqlStr(MACHINE_ID.Trim()));
                    sb.AppendLine();
                }
                else if (string.IsNullOrEmpty(MACHINE_ID) && MACHINE_ID.ToUpper().Trim() == "ALL")
                {
                    sb.Append(" AND SH.MACHINE_ID IN (SELECT HOST_NO FROM STORE_TERMINATING_MACHINE WHERE 1=1 ");

                    if (!string.IsNullOrEmpty(STORE_NO_S))
                    {
                        sb.Append(" AND STORE_NO >= ");
                        sb.Append(Advtek.Utility.OracleDBUtil.SqlStr(STORE_NO_S.Trim()));
                    }
                    if (!string.IsNullOrEmpty(STORE_NO_E))
                    {
                        sb.Append(" AND STORE_NO <= ");
                        sb.Append(Advtek.Utility.OracleDBUtil.SqlStr(STORE_NO_E.Trim()));
                    }

                    sb.Append(" ) ");
                    sb.AppendLine();
                }
                #endregion
                #region 員工編號
                if (!string.IsNullOrEmpty(SALE_PERSON_S))
                {
                    sb.Append(" AND SH.SALE_PERSON >= ");
                    sb.Append(Advtek.Utility.OracleDBUtil.SqlStr(SALE_PERSON_S.Trim()));
                    sb.AppendLine();
                }
                if (!string.IsNullOrEmpty(SALE_PERSON_E))
                {
                    sb.Append(" AND SH.SALE_PERSON <= ");
                    sb.Append(Advtek.Utility.OracleDBUtil.SqlStr(SALE_PERSON_E.Trim()));
                    sb.AppendLine();
                }
                #endregion
                #endregion
                sb.AppendFormat("   GROUP BY SH.STORE_NO ,SH.MACHINE_ID     \n");
                sb.AppendFormat("   UNION   \n");
                sb.AppendFormat("  --代收速博帳單  \n");
                sb.AppendFormat("  SELECT '速博帳單' AS TITLE ,SH.STORE_NO ,SH.MACHINE_ID  \n");
                sb.AppendFormat("        ,SUM(DECODE(BD.PAY_MODE_ID,'1', BD.AMOUNT,0 )) AS CASH_AMT \n");
                sb.AppendFormat("        ,SUM(DECODE(BD.PAY_MODE_ID,'2', BD.AMOUNT,0 )) + SUM(DECODE(BD.PAY_MODE_ID,'3', BD.AMOUNT,0 )) AS CARD_AMT \n");
                sb.AppendFormat("        ,SUM(DECODE(BD.PAY_MODE_ID,'4', BD.AMOUNT,0 )) AS Installment_AMT \n");
                sb.AppendFormat("        ,SUM(DECODE(BD.PAY_MODE_ID,'5', BD.AMOUNT,0 )) AS GIFT_AMT        \n");
                sb.AppendFormat("        ,SUM(DECODE(BD.PAY_MODE_ID,'6', BD.AMOUNT,0 )) AS DebitCard_AMT   \n");
                sb.AppendFormat("        ,SUM(DECODE(BD.PAY_MODE_ID,'7', BD.AMOUNT,0 )) AS HG_AMT          \n");
                sb.AppendFormat("    FROM SALE_HEAD SH ,BILL_DISPATCH BD   \n");
                //sb.AppendFormat("        ,VOUCHER  \n");
                sb.AppendFormat("   WHERE SH.POSUUID_MASTER = BD.POSUUID_MASTER   \n");
                sb.AppendFormat("     AND BD.SALE_DETAIL_ID IN (SELECT ID FROM SALE_DETAIL SD ,PRODUCT PROD WHERE SD.PRODNO = PROD.PRODNO   \n");
                sb.AppendFormat("         AND SD.PRODNO IN (SELECT PARA_VALUE FROM SYS_PARA WHERE PARA_KEY = 'NCIC_BILLING_ITEM_CODE')      \n");
                #region WHERE
                #region 門號
                if (!string.IsNullOrEmpty(MSISDN))
                {
                    sb.Append(" AND SD.MSISDN = ");
                    sb.Append(Advtek.Utility.OracleDBUtil.SqlStr(MSISDN.Trim()));
                    sb.AppendLine();
                }
                #endregion
                #region 商品類別
                if (!string.IsNullOrEmpty(PRODTYPENO_S))
                {
                    sb.Append(" AND PROD.PRODTYPENO >= ");
                    sb.Append(Advtek.Utility.OracleDBUtil.SqlStr(PRODTYPENO_S.Trim()));
                    sb.AppendLine();
                }
                if (!string.IsNullOrEmpty(PRODTYPENO_E))
                {
                    sb.Append(" AND PROD.PRODTYPENO <= ");
                    sb.Append(Advtek.Utility.OracleDBUtil.SqlStr(PRODTYPENO_E.Trim()));
                    sb.AppendLine();
                }
                #endregion
                #region 商品料號
                if (!string.IsNullOrEmpty(PRODNO_S))
                {
                    sb.Append(" AND SD.PRODNO >= ");
                    sb.Append(Advtek.Utility.OracleDBUtil.SqlStr(PRODNO_S.Trim()));
                    sb.AppendLine();
                }
                if (!string.IsNullOrEmpty(PRODNO_E))
                {
                    sb.Append(" AND SD.PRODNO <= ");
                    sb.Append(Advtek.Utility.OracleDBUtil.SqlStr(PRODNO_E.Trim()));
                    sb.AppendLine();
                }
                #endregion
                #endregion
                sb.AppendFormat("         )                     \n");
                //sb.AppendFormat("     AND SH.POSUUID_MASTER = VOUCHER.POSUUID_MASTER   \n");
                //sb.AppendFormat("     AND SH.SALE_STATUS = '2'  \n");
                sb.AppendFormat("     AND SH.SALE_TYPE = 2      \n");
                #region -WHERE-
                #region 門市編號
                if (!string.IsNullOrEmpty(STORE_NO_S))
                {
                    sb.Append(" AND SH.STORE_NO >= ");
                    sb.Append(Advtek.Utility.OracleDBUtil.SqlStr(STORE_NO_S.Trim()));
                    sb.AppendLine();
                }
                if (!string.IsNullOrEmpty(STORE_NO_E))
                {
                    sb.Append(" AND SH.STORE_NO <= ");
                    sb.Append(Advtek.Utility.OracleDBUtil.SqlStr(STORE_NO_E.Trim()));
                    sb.AppendLine();
                }
                #endregion
                #region 交易日期
                if (!string.IsNullOrEmpty(TRADE_DATE_S))
                {
                    sb.Append(" AND TRUNC(SH.TRADE_DATE) >= ");
                    sb.Append(Advtek.Utility.OracleDBUtil.DateStr(TRADE_DATE_S.Trim()));
                    sb.AppendLine();
                }
                if (!string.IsNullOrEmpty(TRADE_DATE_E))
                {
                    sb.Append(" AND TRUNC(SH.TRADE_DATE) <= ");
                    sb.Append(Advtek.Utility.OracleDBUtil.DateStr(TRADE_DATE_E.Trim()));
                    sb.AppendLine();
                }
                #endregion
                #region 機台編號
                if (!string.IsNullOrEmpty(MACHINE_ID) && MACHINE_ID.ToUpper().Trim() != "ALL")
                {
                    sb.Append(" AND SH.MACHINE_ID = ");
                    sb.Append(Advtek.Utility.OracleDBUtil.SqlStr(MACHINE_ID.Trim()));
                    sb.AppendLine();
                }
                else if (string.IsNullOrEmpty(MACHINE_ID) && MACHINE_ID.ToUpper().Trim() == "ALL")
                {
                    sb.Append(" AND SH.MACHINE_ID IN (SELECT HOST_NO FROM STORE_TERMINATING_MACHINE WHERE 1=1 ");

                    if (!string.IsNullOrEmpty(STORE_NO_S))
                    {
                        sb.Append(" AND STORE_NO >= ");
                        sb.Append(Advtek.Utility.OracleDBUtil.SqlStr(STORE_NO_S.Trim()));
                    }
                    if (!string.IsNullOrEmpty(STORE_NO_E))
                    {
                        sb.Append(" AND STORE_NO <= ");
                        sb.Append(Advtek.Utility.OracleDBUtil.SqlStr(STORE_NO_E.Trim()));
                    }

                    sb.Append(" ) ");
                    sb.AppendLine();
                }
                #endregion
                #region 員工編號
                if (!string.IsNullOrEmpty(SALE_PERSON_S))
                {
                    sb.Append(" AND SH.SALE_PERSON >= ");
                    sb.Append(Advtek.Utility.OracleDBUtil.SqlStr(SALE_PERSON_S.Trim()));
                    sb.AppendLine();
                }
                if (!string.IsNullOrEmpty(SALE_PERSON_E))
                {
                    sb.Append(" AND SH.SALE_PERSON <= ");
                    sb.Append(Advtek.Utility.OracleDBUtil.SqlStr(SALE_PERSON_E.Trim()));
                    sb.AppendLine();
                }
                #endregion
                #endregion
                sb.AppendFormat("   GROUP BY SH.STORE_NO ,SH.MACHINE_ID     \n");
                sb.AppendFormat("   UNION   \n");
                sb.AppendFormat("  --代收遠通帳單  \n");
                sb.AppendFormat("  SELECT '遠通帳單' AS TITLE ,SH.STORE_NO ,SH.MACHINE_ID  \n");
                sb.AppendFormat("        ,SUM(DECODE(BD.PAY_MODE_ID,'1', BD.AMOUNT,0 )) AS CASH_AMT \n");
                sb.AppendFormat("        ,SUM(DECODE(BD.PAY_MODE_ID,'2', BD.AMOUNT,0 )) + SUM(DECODE(BD.PAY_MODE_ID,'3', BD.AMOUNT,0 )) AS CARD_AMT \n");
                sb.AppendFormat("        ,SUM(DECODE(BD.PAY_MODE_ID,'4', BD.AMOUNT,0 )) AS Installment_AMT \n");
                sb.AppendFormat("        ,SUM(DECODE(BD.PAY_MODE_ID,'5', BD.AMOUNT,0 )) AS GIFT_AMT        \n");
                sb.AppendFormat("        ,SUM(DECODE(BD.PAY_MODE_ID,'6', BD.AMOUNT,0 )) AS DebitCard_AMT   \n");
                sb.AppendFormat("        ,SUM(DECODE(BD.PAY_MODE_ID,'7', BD.AMOUNT,0 )) AS HG_AMT          \n");
                sb.AppendFormat("    FROM SALE_HEAD SH ,BILL_DISPATCH BD   \n");
                //sb.AppendFormat("        ,VOUCHER  \n");
                sb.AppendFormat("   WHERE SH.POSUUID_MASTER = BD.POSUUID_MASTER   \n");
                sb.AppendFormat("     AND BD.SALE_DETAIL_ID IN (SELECT ID FROM SALE_DETAIL SD ,PRODUCT PROD WHERE SD.PRODNO = PROD.PRODNO   \n");
                sb.AppendFormat("         AND SD.PRODNO IN (SELECT PARA_VALUE FROM SYS_PARA WHERE PARA_KEY = 'ETC_ITEM_CODE')               \n");
                #region WHERE
                #region 門號
                if (!string.IsNullOrEmpty(MSISDN))
                {
                    sb.Append(" AND SD.MSISDN = ");
                    sb.Append(Advtek.Utility.OracleDBUtil.SqlStr(MSISDN.Trim()));
                    sb.AppendLine();
                }
                #endregion
                #region 商品類別
                if (!string.IsNullOrEmpty(PRODTYPENO_S))
                {
                    sb.Append(" AND PROD.PRODTYPENO >= ");
                    sb.Append(Advtek.Utility.OracleDBUtil.SqlStr(PRODTYPENO_S.Trim()));
                    sb.AppendLine();
                }
                if (!string.IsNullOrEmpty(PRODTYPENO_E))
                {
                    sb.Append(" AND PROD.PRODTYPENO <= ");
                    sb.Append(Advtek.Utility.OracleDBUtil.SqlStr(PRODTYPENO_E.Trim()));
                    sb.AppendLine();
                }
                #endregion
                #region 商品料號
                if (!string.IsNullOrEmpty(PRODNO_S))
                {
                    sb.Append(" AND SD.PRODNO >= ");
                    sb.Append(Advtek.Utility.OracleDBUtil.SqlStr(PRODNO_S.Trim()));
                    sb.AppendLine();
                }
                if (!string.IsNullOrEmpty(PRODNO_E))
                {
                    sb.Append(" AND SD.PRODNO <= ");
                    sb.Append(Advtek.Utility.OracleDBUtil.SqlStr(PRODNO_E.Trim()));
                    sb.AppendLine();
                }
                #endregion
                #endregion
                sb.AppendFormat("         )                     \n");
                //sb.AppendFormat("     AND SH.POSUUID_MASTER = VOUCHER.POSUUID_MASTER   \n");
                //sb.AppendFormat("     AND SH.SALE_STATUS = '2'  \n");
                sb.AppendFormat("     AND SH.SALE_TYPE = 2      \n");
                #region -WHERE-
                #region 門市編號
                if (!string.IsNullOrEmpty(STORE_NO_S))
                {
                    sb.Append(" AND SH.STORE_NO >= ");
                    sb.Append(Advtek.Utility.OracleDBUtil.SqlStr(STORE_NO_S.Trim()));
                    sb.AppendLine();
                }
                if (!string.IsNullOrEmpty(STORE_NO_E))
                {
                    sb.Append(" AND SH.STORE_NO <= ");
                    sb.Append(Advtek.Utility.OracleDBUtil.SqlStr(STORE_NO_E.Trim()));
                    sb.AppendLine();
                }
                #endregion
                #region 交易日期
                if (!string.IsNullOrEmpty(TRADE_DATE_S))
                {
                    sb.Append(" AND TRUNC(SH.TRADE_DATE) >= ");
                    sb.Append(Advtek.Utility.OracleDBUtil.DateStr(TRADE_DATE_S.Trim()));
                    sb.AppendLine();
                }
                if (!string.IsNullOrEmpty(TRADE_DATE_E))
                {
                    sb.Append(" AND TRUNC(SH.TRADE_DATE) <= ");
                    sb.Append(Advtek.Utility.OracleDBUtil.DateStr(TRADE_DATE_E.Trim()));
                    sb.AppendLine();
                }
                #endregion
                #region 機台編號
                if (!string.IsNullOrEmpty(MACHINE_ID) && MACHINE_ID.ToUpper().Trim() != "ALL")
                {
                    sb.Append(" AND SH.MACHINE_ID = ");
                    sb.Append(Advtek.Utility.OracleDBUtil.SqlStr(MACHINE_ID.Trim()));
                    sb.AppendLine();
                }
                else if (string.IsNullOrEmpty(MACHINE_ID) && MACHINE_ID.ToUpper().Trim() == "ALL")
                {
                    sb.Append(" AND SH.MACHINE_ID IN (SELECT HOST_NO FROM STORE_TERMINATING_MACHINE WHERE 1=1 ");

                    if (!string.IsNullOrEmpty(STORE_NO_S))
                    {
                        sb.Append(" AND STORE_NO >= ");
                        sb.Append(Advtek.Utility.OracleDBUtil.SqlStr(STORE_NO_S.Trim()));
                    }
                    if (!string.IsNullOrEmpty(STORE_NO_E))
                    {
                        sb.Append(" AND STORE_NO <= ");
                        sb.Append(Advtek.Utility.OracleDBUtil.SqlStr(STORE_NO_E.Trim()));
                    }

                    sb.Append(" ) ");
                    sb.AppendLine();
                }
                #endregion
                #region 員工編號
                if (!string.IsNullOrEmpty(SALE_PERSON_S))
                {
                    sb.Append(" AND SH.SALE_PERSON >= ");
                    sb.Append(Advtek.Utility.OracleDBUtil.SqlStr(SALE_PERSON_S.Trim()));
                    sb.AppendLine();
                }
                if (!string.IsNullOrEmpty(SALE_PERSON_E))
                {
                    sb.Append(" AND SH.SALE_PERSON <= ");
                    sb.Append(Advtek.Utility.OracleDBUtil.SqlStr(SALE_PERSON_E.Trim()));
                    sb.AppendLine();
                }
                #endregion
                #endregion
                sb.AppendFormat("   GROUP BY SH.STORE_NO ,SH.MACHINE_ID     \n");
                sb.AppendFormat("   UNION   \n");
                sb.AppendFormat("  --代收遠通加值  \n");
                sb.AppendFormat("  SELECT '遠通加值' AS TITLE ,SH.STORE_NO ,SH.MACHINE_ID  \n");
                sb.AppendFormat("        ,SUM(DECODE(BD.PAY_MODE_ID,'1', BD.AMOUNT,0 )) AS CASH_AMT \n");
                sb.AppendFormat("        ,SUM(DECODE(BD.PAY_MODE_ID,'2', BD.AMOUNT,0 )) + SUM(DECODE(BD.PAY_MODE_ID,'3', BD.AMOUNT,0 )) AS CARD_AMT \n");
                sb.AppendFormat("        ,SUM(DECODE(BD.PAY_MODE_ID,'4', BD.AMOUNT,0 )) AS Installment_AMT \n");
                sb.AppendFormat("        ,SUM(DECODE(BD.PAY_MODE_ID,'5', BD.AMOUNT,0 )) AS GIFT_AMT        \n");
                sb.AppendFormat("        ,SUM(DECODE(BD.PAY_MODE_ID,'6', BD.AMOUNT,0 )) AS DebitCard_AMT   \n");
                sb.AppendFormat("        ,SUM(DECODE(BD.PAY_MODE_ID,'7', BD.AMOUNT,0 )) AS HG_AMT          \n");
                sb.AppendFormat("    FROM SALE_HEAD SH ,BILL_DISPATCH BD   \n");
                //sb.AppendFormat("        ,VOUCHER  \n");
                sb.AppendFormat("   WHERE SH.POSUUID_MASTER = BD.POSUUID_MASTER   \n");
                sb.AppendFormat("     AND BD.SALE_DETAIL_ID IN (SELECT ID FROM SALE_DETAIL SD ,PRODUCT PROD WHERE SD.PRODNO = PROD.PRODNO   \n");
                sb.AppendFormat("         AND SD.PRODNO IN (SELECT PARA_VALUE FROM SYS_PARA WHERE PARA_KEY = 'ETC2_BILLING_ITEM_CODE')      \n");
                #region WHERE
                #region 門號
                if (!string.IsNullOrEmpty(MSISDN))
                {
                    sb.Append(" AND SD.MSISDN = ");
                    sb.Append(Advtek.Utility.OracleDBUtil.SqlStr(MSISDN.Trim()));
                    sb.AppendLine();
                }
                #endregion
                #region 商品類別
                if (!string.IsNullOrEmpty(PRODTYPENO_S))
                {
                    sb.Append(" AND PROD.PRODTYPENO >= ");
                    sb.Append(Advtek.Utility.OracleDBUtil.SqlStr(PRODTYPENO_S.Trim()));
                    sb.AppendLine();
                }
                if (!string.IsNullOrEmpty(PRODTYPENO_E))
                {
                    sb.Append(" AND PROD.PRODTYPENO <= ");
                    sb.Append(Advtek.Utility.OracleDBUtil.SqlStr(PRODTYPENO_E.Trim()));
                    sb.AppendLine();
                }
                #endregion
                #region 商品料號
                if (!string.IsNullOrEmpty(PRODNO_S))
                {
                    sb.Append(" AND SD.PRODNO >= ");
                    sb.Append(Advtek.Utility.OracleDBUtil.SqlStr(PRODNO_S.Trim()));
                    sb.AppendLine();
                }
                if (!string.IsNullOrEmpty(PRODNO_E))
                {
                    sb.Append(" AND SD.PRODNO <= ");
                    sb.Append(Advtek.Utility.OracleDBUtil.SqlStr(PRODNO_E.Trim()));
                    sb.AppendLine();
                }
                #endregion
                #endregion
                sb.AppendFormat("         )                     \n");
                //sb.AppendFormat("     AND SH.POSUUID_MASTER = VOUCHER.POSUUID_MASTER   \n");
                //sb.AppendFormat("     AND SH.SALE_STATUS = '2'  \n");
                sb.AppendFormat("     AND SH.SALE_TYPE = 2      \n");
                #region -WHERE-
                #region 門市編號
                if (!string.IsNullOrEmpty(STORE_NO_S))
                {
                    sb.Append(" AND SH.STORE_NO >= ");
                    sb.Append(Advtek.Utility.OracleDBUtil.SqlStr(STORE_NO_S.Trim()));
                    sb.AppendLine();
                }
                if (!string.IsNullOrEmpty(STORE_NO_E))
                {
                    sb.Append(" AND SH.STORE_NO <= ");
                    sb.Append(Advtek.Utility.OracleDBUtil.SqlStr(STORE_NO_E.Trim()));
                    sb.AppendLine();
                }
                #endregion
                #region 交易日期
                if (!string.IsNullOrEmpty(TRADE_DATE_S))
                {
                    sb.Append(" AND TRUNC(SH.TRADE_DATE) >= ");
                    sb.Append(Advtek.Utility.OracleDBUtil.DateStr(TRADE_DATE_S.Trim()));
                    sb.AppendLine();
                }
                if (!string.IsNullOrEmpty(TRADE_DATE_E))
                {
                    sb.Append(" AND TRUNC(SH.TRADE_DATE) <= ");
                    sb.Append(Advtek.Utility.OracleDBUtil.DateStr(TRADE_DATE_E.Trim()));
                    sb.AppendLine();
                }
                #endregion
                #region 機台編號
                if (!string.IsNullOrEmpty(MACHINE_ID) && MACHINE_ID.ToUpper().Trim() != "ALL")
                {
                    sb.Append(" AND SH.MACHINE_ID = ");
                    sb.Append(Advtek.Utility.OracleDBUtil.SqlStr(MACHINE_ID.Trim()));
                    sb.AppendLine();
                }
                else if (string.IsNullOrEmpty(MACHINE_ID) && MACHINE_ID.ToUpper().Trim() == "ALL")
                {
                    sb.Append(" AND SH.MACHINE_ID IN (SELECT HOST_NO FROM STORE_TERMINATING_MACHINE WHERE 1=1 ");

                    if (!string.IsNullOrEmpty(STORE_NO_S))
                    {
                        sb.Append(" AND STORE_NO >= ");
                        sb.Append(Advtek.Utility.OracleDBUtil.SqlStr(STORE_NO_S.Trim()));
                    }
                    if (!string.IsNullOrEmpty(STORE_NO_E))
                    {
                        sb.Append(" AND STORE_NO <= ");
                        sb.Append(Advtek.Utility.OracleDBUtil.SqlStr(STORE_NO_E.Trim()));
                    }

                    sb.Append(" ) ");
                    sb.AppendLine();
                }
                #endregion
                #region 員工編號
                if (!string.IsNullOrEmpty(SALE_PERSON_S))
                {
                    sb.Append(" AND SH.SALE_PERSON >= ");
                    sb.Append(Advtek.Utility.OracleDBUtil.SqlStr(SALE_PERSON_S.Trim()));
                    sb.AppendLine();
                }
                if (!string.IsNullOrEmpty(SALE_PERSON_E))
                {
                    sb.Append(" AND SH.SALE_PERSON <= ");
                    sb.Append(Advtek.Utility.OracleDBUtil.SqlStr(SALE_PERSON_E.Trim()));
                    sb.AppendLine();
                }
                #endregion
                #endregion
                sb.AppendFormat("   GROUP BY SH.STORE_NO ,SH.MACHINE_ID     \n");
                sb.AppendFormat("    \n");
                sb.AppendFormat("   UNION   \n");
                sb.AppendFormat("  --代收SEEDNET帳單  \n");
                sb.AppendFormat("  SELECT 'SEEDNET帳單' AS TITLE ,SH.STORE_NO ,SH.MACHINE_ID  \n");
                sb.AppendFormat("        ,SUM(DECODE(BD.PAY_MODE_ID,'1', BD.AMOUNT,0 )) AS CASH_AMT \n");
                sb.AppendFormat("        ,SUM(DECODE(BD.PAY_MODE_ID,'2', BD.AMOUNT,0 )) + SUM(DECODE(BD.PAY_MODE_ID,'3', BD.AMOUNT,0 )) AS CARD_AMT \n");
                sb.AppendFormat("        ,SUM(DECODE(BD.PAY_MODE_ID,'4', BD.AMOUNT,0 )) AS Installment_AMT \n");
                sb.AppendFormat("        ,SUM(DECODE(BD.PAY_MODE_ID,'5', BD.AMOUNT,0 )) AS GIFT_AMT        \n");
                sb.AppendFormat("        ,SUM(DECODE(BD.PAY_MODE_ID,'6', BD.AMOUNT,0 )) AS DebitCard_AMT   \n");
                sb.AppendFormat("        ,SUM(DECODE(BD.PAY_MODE_ID,'7', BD.AMOUNT,0 )) AS HG_AMT          \n");
                sb.AppendFormat("    FROM SALE_HEAD SH ,BILL_DISPATCH BD   \n");
                //sb.AppendFormat("        ,VOUCHER  \n");
                sb.AppendFormat("   WHERE SH.POSUUID_MASTER = BD.POSUUID_MASTER   \n");
                sb.AppendFormat("     AND BD.SALE_DETAIL_ID IN (SELECT ID FROM SALE_DETAIL SD ,PRODUCT PROD WHERE SD.PRODNO = PROD.PRODNO   \n");
                sb.AppendFormat("         AND SD.PRODNO IN (SELECT PARA_VALUE FROM SYS_PARA WHERE PARA_KEY = 'SEEDNET_BILLING_ITEM_CODE')   \n");
                #region WHERE
                #region 門號
                if (!string.IsNullOrEmpty(MSISDN))
                {
                    sb.Append(" AND SD.MSISDN = ");
                    sb.Append(Advtek.Utility.OracleDBUtil.SqlStr(MSISDN.Trim()));
                    sb.AppendLine();
                }
                #endregion
                #region 商品類別
                if (!string.IsNullOrEmpty(PRODTYPENO_S))
                {
                    sb.Append(" AND PROD.PRODTYPENO >= ");
                    sb.Append(Advtek.Utility.OracleDBUtil.SqlStr(PRODTYPENO_S.Trim()));
                    sb.AppendLine();
                }
                if (!string.IsNullOrEmpty(PRODTYPENO_E))
                {
                    sb.Append(" AND PROD.PRODTYPENO <= ");
                    sb.Append(Advtek.Utility.OracleDBUtil.SqlStr(PRODTYPENO_E.Trim()));
                    sb.AppendLine();
                }
                #endregion
                #region 商品料號
                if (!string.IsNullOrEmpty(PRODNO_S))
                {
                    sb.Append(" AND SD.PRODNO >= ");
                    sb.Append(Advtek.Utility.OracleDBUtil.SqlStr(PRODNO_S.Trim()));
                    sb.AppendLine();
                }
                if (!string.IsNullOrEmpty(PRODNO_E))
                {
                    sb.Append(" AND SD.PRODNO <= ");
                    sb.Append(Advtek.Utility.OracleDBUtil.SqlStr(PRODNO_E.Trim()));
                    sb.AppendLine();
                }
                #endregion
                #endregion
                sb.AppendFormat("         )                     \n");
                //sb.AppendFormat("     AND BD.SALE_DETAIL_ID IN (SELECT ID FROM SALE_DETAIL WHERE PRODNO    \n");
                //sb.AppendFormat("         IN (SELECT PARA_VALUE FROM SYS_PARA WHERE PARA_KEY = 'SEEDNET_BILLING_ITEM_CODE'))   \n");
                //sb.AppendFormat("     AND SH.POSUUID_MASTER = VOUCHER.POSUUID_MASTER   \n");
                //sb.AppendFormat("     AND SH.SALE_STATUS = '2'  \n");
                sb.AppendFormat("     AND SH.SALE_TYPE = 2      \n");
                #region -WHERE-
                #region 門市編號
                if (!string.IsNullOrEmpty(STORE_NO_S))
                {
                    sb.Append(" AND SH.STORE_NO >= ");
                    sb.Append(Advtek.Utility.OracleDBUtil.SqlStr(STORE_NO_S.Trim()));
                    sb.AppendLine();
                }
                if (!string.IsNullOrEmpty(STORE_NO_E))
                {
                    sb.Append(" AND SH.STORE_NO <= ");
                    sb.Append(Advtek.Utility.OracleDBUtil.SqlStr(STORE_NO_E.Trim()));
                    sb.AppendLine();
                }
                #endregion
                #region 交易日期
                if (!string.IsNullOrEmpty(TRADE_DATE_S))
                {
                    sb.Append(" AND TRUNC(SH.TRADE_DATE) >= ");
                    sb.Append(Advtek.Utility.OracleDBUtil.DateStr(TRADE_DATE_S.Trim()));
                    sb.AppendLine();
                }
                if (!string.IsNullOrEmpty(TRADE_DATE_E))
                {
                    sb.Append(" AND TRUNC(SH.TRADE_DATE) <= ");
                    sb.Append(Advtek.Utility.OracleDBUtil.DateStr(TRADE_DATE_E.Trim()));
                    sb.AppendLine();
                }
                #endregion
                #region 機台編號
                if (!string.IsNullOrEmpty(MACHINE_ID) && MACHINE_ID.ToUpper().Trim() != "ALL")
                {
                    sb.Append(" AND SH.MACHINE_ID = ");
                    sb.Append(Advtek.Utility.OracleDBUtil.SqlStr(MACHINE_ID.Trim()));
                    sb.AppendLine();
                }
                else if (string.IsNullOrEmpty(MACHINE_ID) && MACHINE_ID.ToUpper().Trim() == "ALL")
                {
                    sb.Append(" AND SH.MACHINE_ID IN (SELECT HOST_NO FROM STORE_TERMINATING_MACHINE WHERE 1=1 ");

                    if (!string.IsNullOrEmpty(STORE_NO_S))
                    {
                        sb.Append(" AND STORE_NO >= ");
                        sb.Append(Advtek.Utility.OracleDBUtil.SqlStr(STORE_NO_S.Trim()));
                    }
                    if (!string.IsNullOrEmpty(STORE_NO_E))
                    {
                        sb.Append(" AND STORE_NO <= ");
                        sb.Append(Advtek.Utility.OracleDBUtil.SqlStr(STORE_NO_E.Trim()));
                    }

                    sb.Append(" ) ");
                    sb.AppendLine();
                }
                #endregion
                #region 員工編號
                if (!string.IsNullOrEmpty(SALE_PERSON_S))
                {
                    sb.Append(" AND SH.SALE_PERSON >= ");
                    sb.Append(Advtek.Utility.OracleDBUtil.SqlStr(SALE_PERSON_S.Trim()));
                    sb.AppendLine();
                }
                if (!string.IsNullOrEmpty(SALE_PERSON_E))
                {
                    sb.Append(" AND SH.SALE_PERSON <= ");
                    sb.Append(Advtek.Utility.OracleDBUtil.SqlStr(SALE_PERSON_E.Trim()));
                    sb.AppendLine();
                }
                #endregion
                #endregion
                sb.AppendFormat("   GROUP BY SH.STORE_NO ,SH.MACHINE_ID     \n");
                #endregion
                sb.AppendFormat("    \n");
                //sb.AppendFormat("   UNION   \n");
                sb.AppendFormat(" )  \n");
                #endregion

                sb.AppendFormat(",  \n");
                #region STORE_SALE_LIST
                sb.AppendFormat("STORE_SALE_LIST AS (  \n");
                sb.AppendFormat("  SELECT MA.STORE_NO,  \n");
                sb.AppendFormat("         MA.MACHINE_ID,  \n");
                sb.AppendFormat("         MA.TITLE_INDEX,  \n");
                sb.AppendFormat("         MA.TITLE,  \n");
                sb.AppendFormat("         NVL(SL.CASH_AMT,0) CASH_AMT,  \n");
                sb.AppendFormat("         NVL(SL.CARD_AMT,0) CARD_AMT,  \n");
                //sb.AppendFormat("         NVL(SL.CARD_AMT2,0) CARD_AMT2,  \n");
                sb.AppendFormat("         NVL(SL.INSTALLMENT_AMT,0) INSTALLMENT_AMT,  \n");
                sb.AppendFormat("         NVL(SL.DebitCard_AMT,0) DebitCard_AMT,  \n");
                sb.AppendFormat("         NVL(SL.GIFT_AMT,0) GIFT_AMT,  \n");
                sb.AppendFormat("         NVL(SL.HG_AMT,0) HG_AMT,  \n");
                sb.AppendFormat("         NVL(SL.CASH_AMT,0)+NVL(SL.CARD_AMT,0)+NVL(SL.INSTALLMENT_AMT,0)  \n");
                sb.AppendFormat("        +NVL(SL.DebitCard_AMT,0)+NVL(SL.GIFT_AMT,0)+NVL(SL.HG_AMT,0) SUM_AMT  \n");
                sb.AppendFormat("    FROM SALE_LIST SL, MACHINE MA  \n");
                sb.AppendFormat("   WHERE MA.TITLE = SL.TITLE(+)  \n");
                sb.AppendFormat("     AND MA.STORE_NO = SL.STORE_NO(+)  \n");
                sb.AppendFormat("     AND MA.MACHINE_ID = SL.MACHINE_ID(+)  \n");
                sb.AppendFormat("   ORDER BY STORE_NO, MACHINE_ID, TITLE_INDEX  \n");
                sb.AppendFormat(")  \n");
                #endregion
                sb.AppendFormat(", \n");
                #region RPL046
                sb.AppendFormat("RPL046 AS ( \n");
                sb.AppendFormat("SELECT '-1' MACHINE_ID, TITLE_INDEX, TITLE,  \n");
                sb.AppendFormat("       NVL(SUM(CASH_AMT),0) 現金,  \n");
                //sb.AppendFormat("       NVL(SUM(CARD_AMT1),0)+ NVL(SUM(CARD_AMT2),0) 信用卡,  \n");
                sb.AppendFormat("       NVL(SUM(CARD_AMT),0) 信用卡,  \n");
                sb.AppendFormat("       NVL(SUM(INSTALLMENT_AMT),0) 分期付款,  \n");
                sb.AppendFormat("       NVL(SUM(DebitCard_AMT),0) 金融卡,  \n");
                sb.AppendFormat("       NVL(SUM(GIFT_AMT),0) 禮券,  \n");
                sb.AppendFormat("       NVL(SUM(HG_AMT),0) HG,  \n");
                sb.AppendFormat("       NVL(SUM(SUM_AMT),0) 加總  \n");
                sb.AppendFormat("  FROM STORE_SALE_LIST  \n");
                sb.AppendFormat(" GROUP BY TITLE_INDEX, TITLE  \n");
                sb.AppendFormat("UNION ALL  \n");
                sb.AppendFormat("SELECT  MACHINE_ID, TITLE_INDEX, TITLE ,NULL,NULL,NULL,NULL,NULL,NULL,NULL FROM TITLE_ROW WHERE MACHINE_ID = '-1' \n");
                if (!isHQ)
                {
                    sb.AppendFormat("UNION ALL  \n");
                    sb.AppendFormat("SELECT MACHINE_ID, TITLE_INDEX,  TITLE,  \n");
                    sb.AppendFormat("       NVL(SUM(CASH_AMT),0) 現金,  \n");
                    sb.AppendFormat("       NVL(SUM(CARD_AMT),0) 信用卡,  \n");
                    //sb.AppendFormat("       NVL(SUM(CARD_AMT1),0)+ NVL(SUM(CARD_AMT2),0) 信用卡,  \n");
                    sb.AppendFormat("       NVL(SUM(INSTALLMENT_AMT),0) 分期付款,  \n");
                    sb.AppendFormat("       NVL(SUM(DebitCard_AMT),0) 金融卡,  \n");
                    sb.AppendFormat("       NVL(SUM(GIFT_AMT),0) 禮券,  \n");
                    sb.AppendFormat("       NVL(SUM(HG_AMT),0) HG,  \n");
                    sb.AppendFormat("       NVL(SUM(SUM_AMT),0) 加總  \n");
                    sb.AppendFormat("  FROM STORE_SALE_LIST  \n");
                    sb.AppendFormat(" GROUP BY TITLE_INDEX, TITLE, MACHINE_ID  \n");
                    sb.AppendFormat("UNION ALL     \n");
                    sb.AppendFormat("SELECT  MACHINE_ID, TITLE_INDEX, TITLE ,NULL,NULL,NULL,NULL,NULL,NULL,NULL FROM TITLE_ROW WHERE MACHINE_ID <> '-1' \n");
                }
                sb.AppendFormat(") \n");
                #endregion
                sb.AppendFormat("SELECT * FROM RPL046 \n");
                #region 加總與小計 加上會卡住
                //sb.AppendFormat("UNION ALL  \n");
                //sb.AppendFormat("SELECT MACHINE_ID, SUBSTR(TITLE_INDEX,1,2) || '09' TITLE_INDEX, '小計',  \n");
                //sb.AppendFormat("       NVL(SUM(現金),0) 現金,  \n");
                //sb.AppendFormat("       NVL(SUM(信用卡),0) 信用卡,  \n");
                //sb.AppendFormat("       NVL(SUM(分期付款),0) 分期付款,  \n");
                //sb.AppendFormat("       NVL(SUM(金融卡),0) 金融卡,  \n");
                //sb.AppendFormat("       NVL(SUM(禮券),0) 禮券,  \n");
                //sb.AppendFormat("       NVL(SUM(HG),0) HG,  \n");
                //sb.AppendFormat("       NVL(SUM(加總),0) 加總  \n");
                //sb.AppendFormat("FROM RPL046  \n");
                //sb.AppendFormat("GROUP BY SUBSTR(TITLE_INDEX,1,2), MACHINE_ID  \n");
                //sb.AppendFormat("UNION ALL  \n");
                //sb.AppendFormat("SELECT MACHINE_ID, '0901' TITLE_INDEX, '加總',  \n");
                //sb.AppendFormat("       NVL(SUM(現金),0) 現金,  \n");
                //sb.AppendFormat("       NVL(SUM(信用卡),0) 信用卡,  \n");
                //sb.AppendFormat("       NVL(SUM(分期付款),0) 分期付款,  \n");
                //sb.AppendFormat("       NVL(SUM(金融卡),0) 金融卡,  \n");
                //sb.AppendFormat("       NVL(SUM(禮券),0) 禮券,  \n");
                //sb.AppendFormat("       NVL(SUM(HG),0) HG,  \n");
                //sb.AppendFormat("       NVL(SUM(加總),0) 加總  \n");
                //sb.AppendFormat("FROM RPL046  \n");
                //sb.AppendFormat("GROUP BY MACHINE_ID  \n");
                sb.AppendFormat("ORDER BY MACHINE_ID, TITLE_INDEX \n");
                #endregion
                #endregion

                DataTable dt = Advtek.Utility.OracleDBUtil.GetDataSet(objConn, sb.ToString()).Tables[0];

                #region 自行實作加總與小計
                String strMACHINE_ID = null;
                String strTITLE_INDEX = null;
                DataRow drSubTotal = null;
                DataRow drTotal = null;

                DataRow drETC = null;

                DataTable dtTotal = dt.Clone();


                foreach (DataRow dr in dt.Rows)
                {
                    switch (dr["TITLE"].ToString())
                    {
                        case "代收":
                        case "保證金":
                            continue;
                        default:
                            break;
                    }
                    if (strTITLE_INDEX != dr["TITLE_INDEX"].ToString().Substring(0, 2))
                    {
                        drSubTotal = dtTotal.NewRow();
                        dtTotal.Rows.Add(drSubTotal);

                        strTITLE_INDEX = dr["TITLE_INDEX"].ToString().Substring(0, 2);
                        drSubTotal["MACHINE_ID"] = dr["MACHINE_ID"];
                        drSubTotal["TITLE_INDEX"] = dr["TITLE_INDEX"].ToString().Substring(0, 2) + "09";
                        drSubTotal["TITLE"] = "小計";
                    }

                    if (strMACHINE_ID != dr["MACHINE_ID"].ToString())
                    {
                        drTotal = dtTotal.NewRow();
                        dtTotal.Rows.Add(drTotal);

                        strMACHINE_ID = dr["MACHINE_ID"].ToString();
                        drTotal["MACHINE_ID"] = dr["MACHINE_ID"];
                        drTotal["TITLE_INDEX"] = "0901";
                        drTotal["TITLE"] = "加總";

                        if (dtSQL.Rows.Count > 0)
                        {
                            drETC = dtTotal.NewRow();
                            dtTotal.Rows.Add(drETC);

                            drETC["MACHINE_ID"] = dr["MACHINE_ID"];
                            drETC["TITLE_INDEX"] = "1001";
                            drETC["TITLE"] = "ETC聯名卡儲值金";

                            for (int i = 0; i < dtSQL.Rows.Count; i++)
                            {
                                if (dtSQL.Rows[i].ItemArray[2].ToString() == dr["MACHINE_ID"].ToString())
                                {
                                    //各機台"ETC聯名卡儲值金"-金額
                                    drETC["現金"] = dtSQL.Rows[i].ItemArray[3].ToString();
                                    drETC["信用卡"] = dtSQL.Rows[i].ItemArray[4].ToString();
                                    drETC["分期付款"] = dtSQL.Rows[i].ItemArray[5].ToString();
                                    drETC["金融卡"] = dtSQL.Rows[i].ItemArray[6].ToString();
                                    drETC["禮券"] = dtSQL.Rows[i].ItemArray[7].ToString();
                                    drETC["HG"] = dtSQL.Rows[i].ItemArray[8].ToString();
                                    drETC["加總"] = ToInt64(dtSQL.Rows[i].ItemArray[3].ToString()) + ToInt64(dtSQL.Rows[i].ItemArray[4].ToString()) + ToInt64(dtSQL.Rows[i].ItemArray[5].ToString()) + ToInt64(dtSQL.Rows[i].ItemArray[6].ToString()) + ToInt64(dtSQL.Rows[i].ItemArray[7].ToString()) + ToInt64(dtSQL.Rows[i].ItemArray[8].ToString());
                                }
                                else if (dr["MACHINE_ID"].ToString() == "-1")
                                {
                                    //門市總計"ETC聯名卡儲值金"-金額
                                    drETC["現金"] = CASH_AMT;
                                    drETC["信用卡"] = CARD_AMT;
                                    drETC["分期付款"] = Installment_AMT;
                                    drETC["金融卡"] = DebitCard_AMT;
                                    drETC["禮券"] = GIFT_AMT;
                                    drETC["HG"] = HG_AMT;
                                    drETC["加總"] = CASH_AMT + CARD_AMT + Installment_AMT + DebitCard_AMT + GIFT_AMT + HG_AMT;
                                }
                                else
                                {
                                    drETC["現金"] = 0;
                                    drETC["信用卡"] = 0;
                                    drETC["分期付款"] = 0;
                                    drETC["金融卡"] = 0;
                                    drETC["禮券"] = 0;
                                    drETC["HG"] = 0;
                                    drETC["加總"] = 0;
                                }
                            }
                        }
                        else
                        {
                            drETC = dtTotal.NewRow();
                            dtTotal.Rows.Add(drETC);

                            drETC["MACHINE_ID"] = dr["MACHINE_ID"];
                            drETC["TITLE_INDEX"] = "1001";
                            drETC["TITLE"] = "ETC聯名卡儲值金";

                            drETC["現金"] = 0;
                            drETC["信用卡"] = 0;
                            drETC["分期付款"] = 0;
                            drETC["金融卡"] = 0;
                            drETC["禮券"] = 0;
                            drETC["HG"] = 0;
                            drETC["加總"] = 0;
                        }
                    }

                    //drTotal["現金"] = Convert.ToInt64("0" + dr["現金"].ToString()) + Convert.ToInt64("0" + drTotal["現金"].ToString());
                    //drTotal["信用卡"] = Convert.ToInt64("0" + dr["信用卡"].ToString()) + Convert.ToInt64("0" + drTotal["信用卡"].ToString());
                    //drTotal["分期付款"] = Convert.ToInt64("0" + dr["分期付款"].ToString()) + Convert.ToInt64("0" + drTotal["分期付款"].ToString());
                    //drTotal["金融卡"] = Convert.ToInt64("0" + dr["金融卡"].ToString()) + Convert.ToInt64("0" + drTotal["金融卡"].ToString());
                    //drTotal["禮券"] = Convert.ToInt64("0" + dr["禮券"].ToString()) + Convert.ToInt64("0" + drTotal["禮券"].ToString());
                    //drTotal["HG"] = Convert.ToInt64("0" + dr["HG"].ToString()) + Convert.ToInt64("0" + drTotal["HG"].ToString());
                    //drTotal["加總"] = Convert.ToInt64("0" + dr["加總"].ToString()) + Convert.ToInt64("0" + drTotal["加總"].ToString());

                    //drSubTotal["現金"] = Convert.ToInt64("0" + dr["現金"].ToString()) + Convert.ToInt64("0" + drSubTotal["現金"].ToString());
                    //drSubTotal["信用卡"] = Convert.ToInt64("0" + dr["信用卡"].ToString()) + Convert.ToInt64("0" + drSubTotal["信用卡"].ToString());
                    //drSubTotal["分期付款"] = Convert.ToInt64("0" + dr["分期付款"].ToString()) + Convert.ToInt64("0" + drSubTotal["分期付款"].ToString());
                    //drSubTotal["金融卡"] = Convert.ToInt64("0" + dr["金融卡"].ToString()) + Convert.ToInt64("0" + drSubTotal["金融卡"].ToString());
                    //drSubTotal["禮券"] = Convert.ToInt64("0" + dr["禮券"].ToString()) + Convert.ToInt64("0" + drSubTotal["禮券"].ToString());
                    //drSubTotal["HG"] = Convert.ToInt64("0" + dr["HG"].ToString()) + Convert.ToInt64("0" + drSubTotal["HG"].ToString());
                    //drSubTotal["加總"] = Convert.ToInt64("0" + dr["加總"].ToString()) + Convert.ToInt64("0" + drSubTotal["加總"].ToString());

                    drTotal["現金"] = ToInt64(dr["現金"].ToString()) + ToInt64(drTotal["現金"].ToString());
                    drTotal["信用卡"] = ToInt64(dr["信用卡"].ToString()) + ToInt64(drTotal["信用卡"].ToString());
                    drTotal["分期付款"] = ToInt64(dr["分期付款"].ToString()) + ToInt64(drTotal["分期付款"].ToString());
                    drTotal["金融卡"] = ToInt64(dr["金融卡"].ToString()) + ToInt64(drTotal["金融卡"].ToString());
                    drTotal["禮券"] = ToInt64(dr["禮券"].ToString()) + ToInt64(drTotal["禮券"].ToString());
                    drTotal["HG"] = ToInt64(dr["HG"].ToString()) + ToInt64(drTotal["HG"].ToString());
                    drTotal["加總"] = ToInt64(dr["加總"].ToString()) + ToInt64(drTotal["加總"].ToString());

                    drSubTotal["現金"] = ToInt64(dr["現金"].ToString()) + ToInt64(drSubTotal["現金"].ToString());
                    drSubTotal["信用卡"] = ToInt64(dr["信用卡"].ToString()) + ToInt64(drSubTotal["信用卡"].ToString());
                    drSubTotal["分期付款"] = ToInt64(dr["分期付款"].ToString()) + ToInt64(drSubTotal["分期付款"].ToString());
                    drSubTotal["金融卡"] = ToInt64(dr["金融卡"].ToString()) + ToInt64(drSubTotal["金融卡"].ToString());
                    drSubTotal["禮券"] = ToInt64(dr["禮券"].ToString()) + ToInt64(drSubTotal["禮券"].ToString());
                    drSubTotal["HG"] = ToInt64(dr["HG"].ToString()) + ToInt64(drSubTotal["HG"].ToString());
                    drSubTotal["加總"] = ToInt64(dr["加總"].ToString()) + ToInt64(drSubTotal["加總"].ToString());

                }
                dt.Merge(dtTotal);

                dt.DefaultView.Sort = "MACHINE_ID, TITLE_INDEX";

                dt.DefaultView.RowFilter = "MACHINE_ID = '-1'";
                DataTable dtHEAD = dt.DefaultView.ToTable();
                dt.DefaultView.RowFilter = "MACHINE_ID <> '-1'";
                DataTable dtBODY = dt.DefaultView.ToTable();

                dtHEAD.Merge(dtBODY);

                #endregion

                return dtHEAD;

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
        }

        //轉int64，非數字 一律變成0
        public Int64 ToInt64(object obj)
        {
            Int64 i;
            if (Int64.TryParse(obj.ToString(), out i)) return i; else return 0;
        }


        /*Author : 蔡坤霖
          Date : 2011 / 02 / 21
          Description: RPL050 門市折讓明細表
         */
        /// <summary>
        /// RPL050 門市折讓明細表
        /// </summary>
        /// <param name="STORENO_S">門市編號_起</param>
        /// <param name="STORENO_E">門市編號_訖</param>
        /// <param name="DATE_S">銷退日期_起</param>
        /// <param name="DATE_E">銷退日期_訖</param>
        /// <returns></returns>
        public DataTable RPL050(string STORENO_S, string STORENO_E, string DATE_S, string DATE_E)
        {
            OracleConnection objConn = null;

            try
            {
                objConn = OracleDBUtil.GetConnection();
                System.Text.StringBuilder sb = new System.Text.StringBuilder();

                #region SELECT
                sb.AppendFormat("SELECT STORE_NO     \"門市編號\", \n");
                sb.AppendFormat("       UNI_NO       \"統一編號\", \n");
                sb.AppendFormat("       INVOICE_DATE \"開立日期\", \n");
                sb.AppendFormat("       BUYER        \"原買受人\", \n");
                sb.AppendFormat("       UNI_NO       \"統一編號\", \n");
                sb.AppendFormat("       INVOICE_TYPE \"聯式\", \n");
                sb.AppendFormat("       INVOICE_NO   \"發票號碼\", \n");
                sb.AppendFormat("       PRODNO       \"商品料號\", \n");
                sb.AppendFormat("       PRODNAME     \"商品名稱\", \n");
                sb.AppendFormat("       QUANTITY     \"數量\", \n");
                sb.AppendFormat("       UNIT_PRICE   \"金額(不含稅)\", \n");
                sb.AppendFormat("       TAX          \"稅額\" \n");
                sb.AppendFormat("  FROM VW_RPL_STORE_PAYOFF_DTL T \n");
                sb.AppendFormat(" WHERE 1 = 1 \n");
                #endregion
                #region WHERE
                #region 門市編號
                if (!string.IsNullOrEmpty(STORENO_S))
                {
                    sb.Append(" AND T.STORE_NO >= ");
                    sb.Append(Advtek.Utility.OracleDBUtil.SqlStr(STORENO_S.Trim()));
                }
                if (!string.IsNullOrEmpty(STORENO_E))
                {
                    sb.Append(" AND T.STORE_NO <= ");
                    sb.Append(Advtek.Utility.OracleDBUtil.SqlStr(STORENO_E.Trim()));
                }
                #endregion
                #region 銷退日期
                if (!string.IsNullOrEmpty(DATE_S))
                {
                    sb.Append(" AND TRUNC(T.INVALID_DATE) >= ");
                    sb.Append(Advtek.Utility.OracleDBUtil.DateStr(DATE_S.Trim()));
                }
                if (!string.IsNullOrEmpty(DATE_E))
                {
                    sb.Append(" AND TRUNC(T.INVALID_DATE) <= ");
                    sb.Append(Advtek.Utility.OracleDBUtil.DateStr(DATE_E.Trim()));
                }
                #endregion
                #endregion

                DataTable dt = Advtek.Utility.OracleDBUtil.GetDataSet(objConn, sb.ToString()).Tables[0];

                return dt;
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
        }

        ///// <summary>
        ///// RPL054
        ///// </summary>
        ///// <param name="TRADE_DATE_S">交易日期(起)</param>
        ///// <param name="TRADE_DATE_E">交易日期(訖)</param>
        ///// <param name="PRODTYPENO_S">商品類別(起)</param>
        ///// <param name="PRODTYPENO_E">商品類別(訖)</param>
        ///// <param name="PRODNO_S">商品料號(起)</param>
        ///// <param name="PRODNO_E">商品料號(訖)</param>
        ///// <param name="SALE_STATUS">銷售型態</param>
        //public DataTable RPL054(string TRADE_DATE_S, string TRADE_DATE_E,
        //    string PRODTYPENO_S, string PRODTYPENO_E,
        //    string PRODNO_S, string PRODNO_E, string SALE_STATUS)
        //{
        //    DataTable O_DATA = new DataTable();
        //    using (OracleConnection oConn = OracleDBUtil.GetConnection())
        //    {
        //        OracleCommand oraCmd = new OracleCommand("SP_QUERY_RPL054");
        //        oraCmd.CommandType = System.Data.CommandType.StoredProcedure;
        //        oraCmd.Parameters.Add(new OracleParameter("v_TRADE_DATE_S", TRADE_DATE_S.Trim()));
        //        oraCmd.Parameters.Add(new OracleParameter("v_TRADE_DATE_E", TRADE_DATE_E.Trim()));
        //        oraCmd.Parameters.Add(new OracleParameter("v_PRODTYPENO_S", PRODTYPENO_S.Trim()));
        //        oraCmd.Parameters.Add(new OracleParameter("v_PRODTYPENO_E", PRODTYPENO_E.Trim()));
        //        oraCmd.Parameters.Add(new OracleParameter("v_PRODNO_S", PRODNO_S.Trim()));
        //        oraCmd.Parameters.Add(new OracleParameter("v_PRODNO_E", PRODNO_E.Trim()));
        //        oraCmd.Parameters.Add(new OracleParameter("v_SALE_STATUS", SALE_STATUS.Trim()));

        //        OracleParameter cursor = new OracleParameter();
        //        cursor.ParameterName = "v_return_cur";
        //        cursor.Direction = ParameterDirection.Output;
        //        cursor.OracleType = OracleType.Cursor;
        //        oraCmd.Parameters.Add(cursor);

        //        oraCmd.Connection = oConn;
        //        oraCmd.ExecuteNonQuery();
        //        OracleDataAdapter da = new OracleDataAdapter(oraCmd);
        //        da.Fill(O_DATA);
        //        return O_DATA;
        //    }
        //}

        public DataTable RPL062(string Transfer_Slip_No, string OUT_STORE_NO_S,
            string OUT_STORE_NO_E, string Out_Date, string IN_STORE_NO_S,
            string IN_STORE_NO_E)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append(@" SELECT  PRODNO , PRODNAME  , PROD_QTY ,   IMEI  
                            FROM VW_RPL_STORETRANSFER_SIGN
                ");
            sb.Append(" WHERE 1 = 1");

            if (!string.IsNullOrEmpty(Transfer_Slip_No))
                sb.Append(" AND STNO = " + OracleDBUtil.SqlStr(Transfer_Slip_No));

            if (OUT_STORE_NO_S.Length != 0 && OUT_STORE_NO_E.Length != 0)
                sb.AppendFormat(" AND FROM_STORE_NO >= {0}  AND FROM_STORE_NO <= {1}",
                    OracleDBUtil.SqlStr(OUT_STORE_NO_S), OracleDBUtil.SqlStr(OUT_STORE_NO_E));

            if (Out_Date.Length != 0)
                sb.AppendFormat(" AND TRUNC(STDATE) = {0} ", OracleDBUtil.SqlStr(Out_Date));

            if (IN_STORE_NO_S.Length != 0 && IN_STORE_NO_E.Length != 0)
                sb.Append(string.Format(" AND TO_STORE_NO >= {0}  AND TO_STORE_NO <= {1}",
                    OracleDBUtil.SqlStr(IN_STORE_NO_S), OracleDBUtil.SqlStr(IN_STORE_NO_E)));

            DataTable dt = OracleDBUtil.Query_Data(sb.ToString());
            return dt;
        }

        #region 宗佑
        /*Author：宗佑
          Date：100.02.17
          Description：RPL006信用卡分期付款明細表 SQL敘述
        */
        /// <summary>
        /// RPL006 信用卡分期付款明細表
        /// </summary>
        /// <param name="TRADE_DATE_S">交易日期_起</param>
        /// <param name="TRADE_DATE_E">交易日期_訖</param>
        /// <param name="STORE_NO_S">門市編號_起</param>
        /// <param name="STORE_NO_E">門市編號_訖</param>
        /// <param name="SALE_TYPE">服務類型</param>
        /// <param name="PRODNO_S">料號_起</param>
        /// <param name="PRODNO_E">料號_訖</param>
        /// <param name="PROMOTION_CODE">促銷代號</param>
        /// <returns></returns>
        public DataTable RPL006(string StoreNo_S, string StoreNo_E, string DATE_S, string DATE_E, string BANK_ID, string CREDIT_INSTALLMENT)
        {
            OracleConnection objConn = null;
            DataTable dt = null;
            try
            {
                objConn = OracleDBUtil.GetConnection();

                System.Text.StringBuilder sb = new System.Text.StringBuilder();

                #region -SELECT-
                sb.AppendFormat(" SELECT STORE_NO              AS 門市編號       ");
                sb.AppendFormat("       ,STORE_NAME            AS 門市名稱       ");
                sb.AppendFormat("       ,TO_CHAR(TRADE_DATE,'YYYY/MM/DD') AS 交易日期    ");
                sb.AppendFormat("       ,SALE_TYPE_NAME        AS 交易類型       ");
                sb.AppendFormat("       ,SALE_STATUS_NAME      AS 交易類別       ");
                sb.AppendFormat("       ,SERVICE_TYPE_NAME     AS 服務類型       ");
                sb.AppendFormat("       ,SOURCE_TYPE_NAME      AS 資料來源       ");
                sb.AppendFormat("       ,SALE_NO               AS 交易序號       ");
                sb.AppendFormat("       ,PROMOTION_CODE        AS 促銷代碼       ");
                sb.AppendFormat("       ,INVOICE_NO            AS 發票號碼       ");
                sb.AppendFormat("       ,INV_AMT               AS 發票金額       ");
                //sb.AppendFormat("       ,INSTALLMENT_ID        AS 分期代號       ");
                sb.AppendFormat("       ,CREDIT_BANK_ID        AS 分期代號       ");
                sb.AppendFormat("       ,BANK_NAME             AS 分期銀行       ");
                sb.AppendFormat("       ,CREDIT_INSTALLMENT    AS 分期期數       ");
                sb.AppendFormat("       ,INTEREST_RATE         AS 分期利率       ");
                sb.AppendFormat("       ,PAID_AMOUNT           AS 分期金額       ");
                sb.AppendFormat("       ,INTEREST_FEE          AS 分期手續費     ");
                sb.AppendFormat("       ,CREDIT_CARD_NO        AS 信用卡卡號     ");
                sb.AppendFormat("       ,STORE_SETTLEMENT_RATE AS 門市分攤利率   ");
                sb.AppendFormat("       ,STORE_INTEREST_FEE    AS 門市分攤手續費 ");
                sb.AppendFormat("       ,PASSAGEWAY_CNAME      AS 訂單通路       ");
                sb.AppendFormat("  FROM VW_RPL_INSTALLMENT_PAIDDETAIL  T         ");
                sb.AppendFormat(" WHERE 1 = 1                                    ");
                #endregion

                #region -WHERE-
                #region -交易日期-
                if (!string.IsNullOrEmpty(DATE_S))
                {
                    sb.Append(" AND TRUNC(T.TRADE_DATE) >= ");
                    sb.Append(Advtek.Utility.OracleDBUtil.DateStr(DATE_S.Trim()));
                }
                if (!string.IsNullOrEmpty(DATE_E))
                {
                    sb.Append(" AND TRUNC(T.TRADE_DATE) <= ");
                    sb.Append(Advtek.Utility.OracleDBUtil.DateStr(DATE_E.Trim()));
                }
                #endregion
                #region -門市編號-
                if (!string.IsNullOrEmpty(StoreNo_S))
                {
                    sb.Append(" AND T.STORE_NO >= ");
                    sb.Append(Advtek.Utility.OracleDBUtil.SqlStr(StoreNo_S.Trim()));
                }
                if (!string.IsNullOrEmpty(StoreNo_E))
                {
                    sb.Append(" AND T.STORE_NO <= ");
                    sb.Append(Advtek.Utility.OracleDBUtil.SqlStr(StoreNo_E.Trim()));
                }
                #endregion
                #region -分期銀行-
                if (!string.IsNullOrEmpty(BANK_ID) && BANK_ID.Trim() != "ALL")
                {
                    sb.Append(" AND T.CREDIT_BANK_ID = ");
                    sb.Append(Advtek.Utility.OracleDBUtil.SqlStr(BANK_ID.Trim()));
                }
                #endregion
                #region -分期期數-
                if (!string.IsNullOrEmpty(CREDIT_INSTALLMENT) && CREDIT_INSTALLMENT.Trim() != "ALL")
                {
                    sb.Append(" AND T.CREDIT_INSTALLMENT = ");
                    sb.Append(Advtek.Utility.OracleDBUtil.SqlStr(CREDIT_INSTALLMENT.Trim()));
                }
                #endregion
                #endregion

                #region -ORDER BY-
                sb.AppendFormat(" ORDER BY STORE_NO ,STORE_NAME ,ORIGINAL_ID ,TO_CHAR(TRADE_DATE,'YYYY/MM/DD') ,SALE_NO ");
                #endregion

                dt = Advtek.Utility.OracleDBUtil.GetDataSet(objConn, sb.ToString()).Tables[0];
                return dt;
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
        }
        #endregion

        #region ---  蔡坤霖
        /*Author : 蔡坤霖
          Date : 2011 / 02 / 08
          Description: RPL007 門市交易明細表
         */
        /// <summary>
        /// RPL007 門市交易明細表
        /// </summary>
        /// <param name="TRADE_DATE_S">交易日期_起</param>
        /// <param name="TRADE_DATE_E">交易日期_訖</param>
        /// <param name="STORE_NO_S">門市編號_起</param>
        /// <param name="STORE_NO_E">門市編號_訖</param>
        /// <param name="SALE_TYPE">服務類型</param>
        /// <param name="PRODNO_S">料號_起</param>
        /// <param name="PRODNO_E">料號_訖</param>
        /// <param name="PROMOTION_CODE">促銷代號</param>
        /// <returns></returns>
        // public DataTable RPL007(string TRADE_DATE_S, string TRADE_DATE_E, string STORE_NO_S, string STORE_NO_E, string PRODNO_S, string PRODNO_E, string PROMOTION_CODE, string SALE_TYPE, string SOURCE_TYPE)
        public DataTable RPL007(string TRADE_DATE_S, string TRADE_DATE_E, string STORE_NO_S, string STORE_NO_E, string PRODNO_S, string PRODNO_E, string PROMOTION_CODE, string SALE_TYPE, string TRADE_TYPE, string SERVICE_TYPE, string SOURCE_TYPE)
        {
            OracleConnection objConn = null;
            DataTable dt = null;
            try
            {
                objConn = OracleDBUtil.GetConnection();

                System.Text.StringBuilder sb = new System.Text.StringBuilder();

                #region -SELECT-
                sb.AppendFormat("SELECT T.ZONE_NAME AS 門市區域, ");
                sb.AppendFormat("       T.STORE_NO AS 門市編號, ");
                sb.AppendFormat("       T.STORE_NAME AS 門市名稱, ");
                sb.AppendFormat("       T.SALE_NO AS 交易序號, ");
                sb.AppendFormat("       T.INVOICE_NO AS 發票憑證號碼, ");
                sb.AppendFormat("       T.MACHINE_ID AS 機台, ");
                sb.AppendFormat("       T.SALE_TYPE_NAME AS 交易類型, ");
                sb.AppendFormat("       T.SALE_STATUS_NAME AS 交易類別, ");
                sb.AppendFormat("       T.SERVICE_TYPE_NAME AS 服務類型, ");
                sb.AppendFormat("       T.SOURCE_TYPE_NAME AS 資料來源, ");
                sb.AppendFormat("       T.QUANTITY AS 數量, ");
                sb.AppendFormat("       T.PRODNO AS 商品料號, ");
                sb.AppendFormat("       T.PRODNAME AS 商品名稱, ");
                sb.AppendFormat("       T.PROMOTION_CODE AS 促銷代號, ");
                sb.AppendFormat("       T.BILLING_ACCOUNT_ID AS 客戶帳號, ");
                sb.AppendFormat("       T.MSISDN AS 啟用續約門號, ");
                sb.AppendFormat("       TO_CHAR(T.TRADE_DATE,'YYYY/MM/DD') AS 交易日期, ");
                //sb.AppendFormat("       DECODE(T.SALE_TYPE,  1, '應稅',  2, '') AS 稅別, ");
                sb.AppendFormat("       T.TAXABLE AS 稅別, ");
                sb.AppendFormat("       T.BEFORE_TAX AS 未稅金額, ");
                sb.AppendFormat("       T.TAX AS 稅額, ");
                sb.AppendFormat("       T.TOTAL_AMOUNT AS 應收金額, ");
                sb.AppendFormat("       T.Accountcode AS 會計科目, ");
                sb.AppendFormat("       T.ORIGINAL_ID, ");
                sb.AppendFormat("       T.BEFORE_SALE_NO AS 原交易序號 ");
                sb.AppendFormat("  FROM VW_RPL_STORE_SALE_DETAIL T ");
                sb.AppendFormat(" WHERE 1 = 1 ");
                #endregion

                #region -WHERE-
                #region 交易日期
                if (!string.IsNullOrEmpty(TRADE_DATE_S))
                {
                    sb.Append(" AND TRUNC(T.TRADE_DATE) >= ");
                    sb.Append(Advtek.Utility.OracleDBUtil.DateStr(TRADE_DATE_S.Trim()));
                }
                if (!string.IsNullOrEmpty(TRADE_DATE_E))
                {
                    sb.Append(" AND TRUNC(T.TRADE_DATE) <= ");
                    sb.Append(Advtek.Utility.OracleDBUtil.DateStr(TRADE_DATE_E.Trim()));
                }
                #endregion
                #region 門市編號
                if (!string.IsNullOrEmpty(STORE_NO_S))
                {
                    sb.Append(" AND T.STORE_NO >= ");
                    sb.Append(Advtek.Utility.OracleDBUtil.SqlStr(STORE_NO_S.Trim()));
                }
                if (!string.IsNullOrEmpty(STORE_NO_E))
                {
                    sb.Append(" AND T.STORE_NO <= ");
                    sb.Append(Advtek.Utility.OracleDBUtil.SqlStr(STORE_NO_E.Trim()));
                }
                #endregion
                #region 料號
                if (!string.IsNullOrEmpty(PRODNO_S))
                {
                    sb.Append(" AND T.PRODNO >= ");
                    sb.Append(Advtek.Utility.OracleDBUtil.SqlStr(PRODNO_S.Trim()));
                }
                if (!string.IsNullOrEmpty(PRODNO_E))
                {
                    sb.Append(" AND T.PRODNO <= ");
                    sb.Append(Advtek.Utility.OracleDBUtil.SqlStr(PRODNO_E.Trim()));
                }
                #endregion
                #region 促銷代號
                if (!string.IsNullOrEmpty(PROMOTION_CODE))
                {
                    sb.Append(" AND T.PROMOTION_CODE = ");
                    sb.Append(Advtek.Utility.OracleDBUtil.SqlStr(PROMOTION_CODE.Trim()));
                }
                #endregion
                #region 交易類型
                if (!string.IsNullOrEmpty(SALE_TYPE) && SALE_TYPE != "ALL") sb.AppendFormat(" AND T.SALE_TYPE ='{0}' ", SALE_TYPE);
                #endregion
                #region 交易類別
                if (!string.IsNullOrEmpty(TRADE_TYPE) && TRADE_TYPE != "ALL") sb.AppendFormat(" AND T.SALE_STATUS ='{0}' ", TRADE_TYPE);
                #endregion
                #region 服務類型
                if (!string.IsNullOrEmpty(SERVICE_TYPE) && SERVICE_TYPE != "ALL") sb.AppendFormat(" AND T.SERVICE_TYPE ='{0}' ", SERVICE_TYPE.Trim().Replace("A", ""));
                #endregion
                #region 資料來源
                if (!string.IsNullOrEmpty(SOURCE_TYPE) && SOURCE_TYPE != "ALL") sb.AppendFormat(" AND T.SOURCE_TYPE ='{0}' ", SOURCE_TYPE);
                #endregion
                #endregion

                #region -ORDER BY-
                sb.AppendFormat(" ORDER BY T.ORIGINAL_ID ,T.STORE_NO ,TO_CHAR(T.TRADE_DATE,'YYYY/MM/DD') ,T.SALE_NO ,T.INVOICE_NO ,T.PRODNO ");
                #endregion

                dt = Advtek.Utility.OracleDBUtil.GetDataSet(objConn, sb.ToString()).Tables[0];
                return dt;
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
        }


        /*Author : 蔡坤霖
          Date : 2011 / 02 / 08
          Description: RPL008 門市保證金明細表
         */
        /// <summary>
        /// RPL008 門市保證金明細表
        /// </summary>
        /// <param name="TRADE_DATE_S">交易日期_起</param>
        /// <param name="TRADE_DATE_E">交易日期_訖</param>
        /// <param name="PRODNO">保證金類型</param>
        /// <returns></returns>
        public DataTable RPL008(string TRADE_DATE_S, string TRADE_DATE_E, string GUARANTEE_TYPE, string SALE_STATUS)
        {
            OracleConnection objConn = null;
            DataTable dt = null;
            try
            {
                objConn = OracleDBUtil.GetConnection();

                System.Text.StringBuilder sb = new System.Text.StringBuilder();

                #region -SELECT-
                sb.AppendFormat("SELECT TO_CHAR(T.TRADE_DATE,'YYYY/MM/DD') AS 交易日期     ");
                sb.AppendFormat("      ,T.STORE_NO                         AS 門市編號     ");
                sb.AppendFormat("      ,T.STORE_NAME                       AS 門市名稱     ");
                sb.AppendFormat("      ,T.GUARANTEE_TYPE                   AS 保證金類型   ");
                sb.AppendFormat("      ,T.SALE_STATUS_NAME                 AS 交易型態     ");
                sb.AppendFormat("      ,T.PRODNAME                         AS 商品名稱     ");
                sb.AppendFormat("      ,T.BILLING_ACCOUNT_ID               AS 客戶帳號     ");
                sb.AppendFormat("      ,T.MSISDN                           AS 客戶手機號碼 ");
                sb.AppendFormat("      ,T.TOTAL_AMOUNT                     AS 保證金金額   ");
                sb.AppendFormat("      ,T.ACCOUNTCODE                      AS 會計科目     ");
                sb.AppendFormat("      ,T.SALE_PERSON || T.EMPNAME         AS 處理人員     ");
                sb.AppendFormat("  FROM VW_RPL_STORE_MARGIN_REPORT T ");
                sb.AppendFormat(" WHERE 1 = 1 ");
                #endregion

                #region -WHERE-
                #region 交易日期
                if (!string.IsNullOrEmpty(TRADE_DATE_S))
                {
                    sb.Append(" AND TRUNC(T.TRADE_DATE) >= ");
                    sb.Append(Advtek.Utility.OracleDBUtil.DateStr(TRADE_DATE_S.Trim()));
                }
                if (!string.IsNullOrEmpty(TRADE_DATE_E))
                {
                    sb.Append(" AND TRUNC(T.TRADE_DATE) <= ");
                    sb.Append(Advtek.Utility.OracleDBUtil.DateStr(TRADE_DATE_E.Trim()));
                }
                #endregion
                #region 保證金類型
                if (!string.IsNullOrEmpty(GUARANTEE_TYPE) && GUARANTEE_TYPE != "ALL") sb.AppendFormat(" AND T.GUARANTEE_ID ='{0}' ", GUARANTEE_TYPE);
                #endregion
                #region 交易型態
                if (!string.IsNullOrEmpty(SALE_STATUS) && SALE_STATUS != "ALL") sb.AppendFormat(" AND T.SALE_STATUS_NAME ='{0}' ", SALE_STATUS);
                #endregion
                #endregion

                #region -ORDER BY-
                sb.AppendFormat(" ORDER BY TO_CHAR(T.TRADE_DATE,'YYYY/MM/DD') ,T.STORE_NO ");
                #endregion

                dt = Advtek.Utility.OracleDBUtil.GetDataSet(objConn, sb.ToString()).Tables[0];
                return dt;
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
        }

        /*Author : 蔡坤霖
          Date : 2011 / 02 / 17
          Description: RPL009 門市結帳分錄彙總表
         */
        /// <summary>
        /// RPL009 門市結帳分錄彙總表
        /// </summary>
        /// <param name="DATE_S">交易日期_起</param>
        /// <param name="DATE_E">交易日期_訖</param>
        /// <returns></returns>
        public DataTable RPL009(string DATE_S, string DATE_E)
        {   // 由於該報表查詢條件作用在GL_TEMP，因此無法使用VIEW
            OracleConnection objConn = null;
            DataTable dt = null;
            try
            {
                objConn = OracleDBUtil.GetConnection();

                System.Text.StringBuilder sb = new System.Text.StringBuilder();

                #region -SELECT-

                sb.AppendFormat(" SELECT GL.ACC_TYPE AS 科目類型                                       \n");
                sb.AppendFormat("       ,SUBSTR(GL.ACCOUNT_CODE, 1, 2) AS 科目1                        \n");
                sb.AppendFormat("       ,SUBSTR(GL.ACCOUNT_CODE, 3, 3) AS 科目2                        \n");
                sb.AppendFormat("       ,SUBSTR(GL.ACCOUNT_CODE, 6, 4) AS 科目3                        \n");
                sb.AppendFormat("       ,SUBSTR(GL.ACCOUNT_CODE, 10, 6) AS 科目4                       \n");
                sb.AppendFormat("       ,SUBSTR(GL.ACCOUNT_CODE, 16, 4) AS 科目5                       \n");
                sb.AppendFormat("       ,SUBSTR(GL.ACCOUNT_CODE, 20, 4) AS 科目6                       \n");
                sb.AppendFormat("       ,SUM(GL.DR) AS 借方                                            \n");
                sb.AppendFormat("       ,SUM(GL.CR) AS 貸方                                            \n");
                sb.AppendFormat("   FROM (                                                             \n");
                sb.AppendFormat(" SELECT DECODE(NVL(POS_GL.IS_DISCOUNT,'N'),'Y','折扣','N','一般') AS ACC_TYPE  \n");
                sb.AppendFormat("       ,POS_GL.ACCOUNT_CODE                                           \n");
                sb.AppendFormat("       ,NVL(DECODE (POS_GL.DRCR, 'DR', POS_GL.AMT), 0) AS DR          \n");
                sb.AppendFormat("       ,NVL(DECODE (POS_GL.DRCR, 'CR', POS_GL.AMT), 0) AS CR          \n");
                sb.AppendFormat("   FROM POS_GL                                                        \n");
                sb.AppendFormat("  WHERE 1=1                                                           \n");

                #region -Where-
                if (!string.IsNullOrEmpty(DATE_S))
                {
                    sb.Append(" AND (POS_GL.GL_DATE) >= '");
                    sb.Append(DATE_S.Trim().Replace("/", ""));
                    sb.Append("' ");
                }
                if (!string.IsNullOrEmpty(DATE_E))
                {
                    sb.Append(" AND (POS_GL.GL_DATE) <= '");
                    sb.Append(DATE_E.Trim().Replace("/", ""));
                    sb.Append("' ");
                }
                #endregion

                sb.AppendFormat("  UNION ALL                                                           \n");
                sb.AppendFormat(" SELECT DECODE(GL_TEMP.ITEM_NO,'SA_04','折扣','一般') AS ACC_TYPE     \n");
                sb.AppendFormat("       ,GL_TEMP.ACCOUNT_CODE                                          \n");
                sb.AppendFormat("       ,NVL(DECODE (GL_TEMP.DRCR, '0', GL_TEMP.AMT), 0) AS DR         \n");
                sb.AppendFormat("       ,NVL(DECODE (GL_TEMP.DRCR, '1', GL_TEMP.AMT), 0) AS CR         \n");
                sb.AppendFormat("   FROM GL_TEMP                                                       \n");
                sb.AppendFormat("  WHERE 1=1                                                           \n");

                #region -Where-
                if (!string.IsNullOrEmpty(DATE_S))
                {
                    sb.Append(" AND TRUNC(GL_TEMP.TRAD_DATE) >= ");
                    sb.Append(Advtek.Utility.OracleDBUtil.DateStr(DATE_S.Trim()));
                }
                if (!string.IsNullOrEmpty(DATE_E))
                {
                    sb.Append(" AND TRUNC(GL_TEMP.TRAD_DATE) <= ");
                    sb.Append(Advtek.Utility.OracleDBUtil.DateStr(DATE_E.Trim()));
                }
                #endregion

                sb.AppendFormat("  ) GL                                                                \n");

                #endregion

                #region -GROUP BY-
                sb.AppendFormat("   GROUP BY GL.ACC_TYPE                    \n");
                sb.AppendFormat("           ,SUBSTR(GL.ACCOUNT_CODE, 1, 2)  \n");
                sb.AppendFormat("           ,SUBSTR(GL.ACCOUNT_CODE, 3, 3)  \n");
                sb.AppendFormat("           ,SUBSTR(GL.ACCOUNT_CODE, 6, 4)  \n");
                sb.AppendFormat("           ,SUBSTR(GL.ACCOUNT_CODE, 10, 6) \n");
                sb.AppendFormat("           ,SUBSTR(GL.ACCOUNT_CODE, 16, 4) \n");
                sb.AppendFormat("           ,SUBSTR(GL.ACCOUNT_CODE, 20, 4) \n");
                #endregion

                sb.AppendFormat("UNION ALL ");

                #region -SELECT-

                sb.AppendFormat(" SELECT '總計：'            \n");
                sb.AppendFormat("       ,''                  \n");
                sb.AppendFormat("       ,''                  \n");
                sb.AppendFormat("       ,''                  \n");
                sb.AppendFormat("       ,''                  \n");
                sb.AppendFormat("       ,''                  \n");
                sb.AppendFormat("       ,''                  \n");
                sb.AppendFormat("       ,SUM(GL.DR) AS 借方                                            \n");
                sb.AppendFormat("       ,SUM(GL.CR) AS 貸方                                            \n");
                sb.AppendFormat("   FROM (                                                             \n");
                sb.AppendFormat(" SELECT DECODE(NVL(POS_GL.IS_DISCOUNT,'N'),'Y','折扣','N','一般') AS ACC_TYPE  \n");
                sb.AppendFormat("       ,POS_GL.ACCOUNT_CODE                                           \n");
                sb.AppendFormat("       ,NVL(DECODE (POS_GL.DRCR, 'DR', POS_GL.AMT), 0) AS DR          \n");
                sb.AppendFormat("       ,NVL(DECODE (POS_GL.DRCR, 'CR', POS_GL.AMT), 0) AS CR          \n");
                sb.AppendFormat("   FROM POS_GL                                                        \n");
                sb.AppendFormat("  WHERE 1=1                                                           \n");

                #region -Where-
                if (!string.IsNullOrEmpty(DATE_S))
                {
                    sb.Append(" AND (POS_GL.GL_DATE) >= '");
                    sb.Append(DATE_S.Trim().Replace("/", ""));
                    sb.Append("' ");
                }
                if (!string.IsNullOrEmpty(DATE_E))
                {
                    sb.Append(" AND (POS_GL.GL_DATE) <= '");
                    sb.Append(DATE_E.Trim().Replace("/", ""));
                    sb.Append("' ");
                }
                #endregion

                sb.AppendFormat("  UNION ALL                                                           \n");
                sb.AppendFormat(" SELECT DECODE(GL_TEMP.ITEM_NO,'SA_04','折扣','一般') AS ACC_TYPE     \n");
                sb.AppendFormat("       ,GL_TEMP.ACCOUNT_CODE                                          \n");
                sb.AppendFormat("       ,NVL(DECODE (GL_TEMP.DRCR, '0', GL_TEMP.AMT), 0) AS DR         \n");
                sb.AppendFormat("       ,NVL(DECODE (GL_TEMP.DRCR, '1', GL_TEMP.AMT), 0) AS CR         \n");
                sb.AppendFormat("   FROM GL_TEMP                                                       \n");
                sb.AppendFormat("  WHERE 1=1                                                           \n");

                #region -Where-
                if (!string.IsNullOrEmpty(DATE_S))
                {
                    sb.Append(" AND TRUNC(GL_TEMP.TRAD_DATE) >= ");
                    sb.Append(Advtek.Utility.OracleDBUtil.DateStr(DATE_S.Trim()));
                }
                if (!string.IsNullOrEmpty(DATE_E))
                {
                    sb.Append(" AND TRUNC(GL_TEMP.TRAD_DATE) <= ");
                    sb.Append(Advtek.Utility.OracleDBUtil.DateStr(DATE_E.Trim()));
                }
                #endregion

                sb.AppendFormat("  ) GL                                                                \n");

                #endregion

                #region -ORDER BY-
                sb.AppendFormat(" ORDER BY 2,3,4,5,6,7  ");
                #endregion

                dt = Advtek.Utility.OracleDBUtil.GetDataSet(objConn, sb.ToString()).Tables[0];
                return dt;
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
        }

        /*Author : 蔡坤霖
                  Date : 2011 / 02 / 08
                  Description: RPL011 代收業務彙總表
                 */
        public DataTable RPL011(string TRADE_DATE_S, string TRADE_DATE_E, string STORE_NO_S, string STORE_NO_E, string FUN_ID, string PAID_MODE)
        {
            OracleConnection objConn = null;

            try
            {
                objConn = OracleDBUtil.GetConnection();
                System.Text.StringBuilder sb = new System.Text.StringBuilder();

                #region -SELECT-
                sb.AppendFormat("SELECT TO_CHAR(TRADE_DATE,'YYYY/MM/DD')            AS 交易日期 ");
                sb.AppendFormat("      ,STORE_NO              AS 門市編號   ");
                sb.AppendFormat("      ,STORE_NAME            AS 門市名稱   ");
                sb.AppendFormat("      ,SALE_STATUS_NAME      AS 交易別     ");
                sb.AppendFormat("      ,RCV_TYPE              AS 代收類別   ");
                sb.AppendFormat("      ,PAID_MODE_NAME        AS 付款方式   ");
                sb.AppendFormat("      ,CREDID_CARD_TYPE_NAME AS 信用卡卡別 ");
                sb.AppendFormat("      ,SUM(RCV_AMOUNT)       AS 代收金額   ");
                sb.AppendFormat("  FROM VW_RPL_AGNC_RCPT ");
                sb.AppendFormat(" WHERE 1 = 1");
                #endregion

                #region -WHERE-
                #region -交易日期-
                if (!string.IsNullOrEmpty(TRADE_DATE_S))
                {
                    sb.Append(" AND TRUNC(TRADE_DATE) >= ");
                    sb.Append(Advtek.Utility.OracleDBUtil.DateStr(TRADE_DATE_S.Trim()));
                }
                if (!string.IsNullOrEmpty(TRADE_DATE_E))
                {
                    sb.Append(" AND TRUNC(TRADE_DATE) <= ");
                    sb.Append(Advtek.Utility.OracleDBUtil.DateStr(TRADE_DATE_E.Trim()));
                }
                #endregion
                #region -門市編號-
                if (!string.IsNullOrEmpty(STORE_NO_S))
                {
                    sb.Append(" AND STORE_NO >= ");
                    sb.Append(Advtek.Utility.OracleDBUtil.SqlStr(STORE_NO_S.Trim()));
                }
                if (!string.IsNullOrEmpty(STORE_NO_E))
                {
                    sb.Append(" AND STORE_NO <= ");
                    sb.Append(Advtek.Utility.OracleDBUtil.SqlStr(STORE_NO_E.Trim()));
                }
                #endregion
                #region -代收類別-
                if (!string.IsNullOrEmpty(FUN_ID) && FUN_ID != "ALL") sb.AppendFormat(" AND FUN_ID ='{0}' ", FUN_ID);
                #endregion
                #region -付款方式-
                if (!string.IsNullOrEmpty(PAID_MODE) && PAID_MODE != "ALL") sb.AppendFormat(" AND PAID_MODE ='{0}' ", PAID_MODE);
                #endregion
                #endregion

                #region -GROUP BY-
                sb.AppendFormat(" GROUP BY TO_CHAR(TRADE_DATE,'YYYY/MM/DD') ");
                sb.AppendFormat("         ,STORE_NO                         ");
                sb.AppendFormat("         ,STORE_NAME                       ");
                sb.AppendFormat("         ,SALE_STATUS_NAME                 ");
                sb.AppendFormat("         ,RCV_TYPE                         ");
                sb.AppendFormat("         ,PAID_MODE_NAME                   ");
                sb.AppendFormat("         ,CREDID_CARD_TYPE_NAME            ");
                #endregion

                #region -ORDER BY-
                sb.AppendFormat(" ORDER BY TO_CHAR(TRADE_DATE,'YYYY/MM/DD') ");
                sb.AppendFormat("         ,STORE_NO                         ");
                #endregion


                DataTable dt = Advtek.Utility.OracleDBUtil.GetDataSet(objConn, sb.ToString()).Tables[0];

                return dt;
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
        }

        /*Author : 蔡坤霖
          Date : 2011 / 02 / 08
          Description: RPL012 代收業務明細表
         */
        public DataTable RPL012(string TrdDate, string StoreNoStart, string StoreNoEnd, string FunID, string BillType)
        {
            OracleConnection objConn = null;

            try
            {
                objConn = OracleDBUtil.GetConnection();
                System.Text.StringBuilder sb = new System.Text.StringBuilder();

                #region -SELECT-
                sb.AppendFormat("SELECT STORE_NO          AS 門市編號 ");
                sb.AppendFormat("      ,STORE_NAME        AS 門市名稱 ");
                sb.AppendFormat("      ,FUN_ID_NAME       AS 代收類別 ");
                sb.AppendFormat("      ,SALE_NO           AS 交易序號 ");
                sb.AppendFormat("      ,PAID_MODE_NAME    AS 付款方式 ");
                sb.AppendFormat("      ,SALE_STATUS_NAME  AS 交易別   ");
                sb.AppendFormat("      ,CustomerID        AS 客戶帳號 ");
                sb.AppendFormat("      ,MSISDN            AS 代收門號號碼 ");
                sb.AppendFormat("      ,PAID_AMOUNT       AS 代收金額 ");
                sb.AppendFormat("  FROM VW_RPL_RECEIVE_DTL ");
                sb.AppendFormat(" WHERE 1 = 1");
                #endregion

                #region -WHERE-
                #region -交易日期-
                if (!string.IsNullOrEmpty(TrdDate))
                {
                    sb.Append(" AND TRUNC(TRADE_DATE) = ");
                    sb.Append(Advtek.Utility.OracleDBUtil.DateStr(TrdDate.Trim()));
                }
                #endregion
                #region -門市編號-
                if (!string.IsNullOrEmpty(StoreNoStart))
                {
                    sb.Append(" AND STORE_NO >= ");
                    sb.Append(Advtek.Utility.OracleDBUtil.SqlStr(StoreNoStart.Trim()));
                }
                if (!string.IsNullOrEmpty(StoreNoEnd))
                {
                    sb.Append(" AND STORE_NO <= ");
                    sb.Append(Advtek.Utility.OracleDBUtil.SqlStr(StoreNoEnd.Trim()));
                }
                #endregion
                #region -代收服務類別-
                if (!string.IsNullOrEmpty(FunID) && FunID.ToUpper() != "ALL")
                {
                    sb.Append(" AND FUN_ID = ");
                    sb.Append(Advtek.Utility.OracleDBUtil.SqlStr(FunID.Trim()));
                }
                #endregion
                #region -交易別-
                if (!string.IsNullOrEmpty(BillType) && BillType.ToUpper() != "ALL")
                {
                    sb.Append(" AND SALE_STATUS_NAME = ");
                    sb.Append(Advtek.Utility.OracleDBUtil.SqlStr(BillType.Trim()));
                }
                #endregion
                #endregion

                #region -ORDER BY-
                sb.AppendFormat(" ORDER BY STORE_NO ,SALE_NO ,FUN_ID_NAME ");
                #endregion

                DataTable dt = Advtek.Utility.OracleDBUtil.GetDataSet(objConn, sb.ToString()).Tables[0];

                return dt;
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
        }
        /*Author : 蔡坤霖
          Date : 2011 / 02 / 21
          Description: RPL013 門市報稅彙總表
         */
        public DataTable RPL013(string MONTH_S, string MONTH_E, string TAX, string STORENO_S, string STORENO_E)
        {
            OracleConnection objConn = null;

            try
            {
                objConn = OracleDBUtil.GetConnection();
                System.Text.StringBuilder sb = new System.Text.StringBuilder();

                sb.AppendFormat("SELECT STORE_NO,   STORE_NAME,   UNI_NO,   TAXNO,   TAXABLE,     TAXABLE_NAME,   YYYYMM, \n");
                sb.AppendFormat("   SUM(INVO_AMT) INVO_AMT,SUM(INVO_TAX) INVO_TAX,SUM(MINVO_AMT_3) MINVO_AMT_3,SUM(MINVO_TAX_3) MINVO_TAX_3,\n");
                sb.AppendFormat("   SUM(MINVO_AMT_2) MINVO_AMT_2,SUM(MINVO_TAX_2) MINVO_TAX_2,SUM(INVO_PAYOFF_AMT) INVO_PAYOFF_AMT,\n");
                sb.AppendFormat("   SUM(INVO_PAYOFF_TAX) INVO_PAYOFF_TAX,SUM(TOTAL_AMT) TOTAL_AMT,SUM(TOTAL_TAX) TOTAL_TAX \n");
                sb.AppendFormat("  FROM VW_RPL_STORE_TAX_DECLARATION1 T \n");
                sb.AppendFormat(" WHERE 1 = 1 \n");

                if (!string.IsNullOrEmpty(MONTH_S))
                {
                    sb.AppendFormat(" AND YYYYMM >= '{0}' ", MONTH_S.Trim());
                }
                if (!string.IsNullOrEmpty(MONTH_E))
                {
                    sb.AppendFormat(" AND YYYYMM <= '{0}' ", MONTH_E.Trim());
                }
                if (!string.IsNullOrEmpty(TAX) && (TAX) != "ALL")
                {
                    sb.Append(" AND TAXABLE_NAME = ");
                    sb.Append(Advtek.Utility.OracleDBUtil.SqlStr(TAX.Trim()));
                }
                if (!string.IsNullOrEmpty(STORENO_S))
                {
                    sb.Append(" AND STORE_NO >= ");
                    sb.Append(Advtek.Utility.OracleDBUtil.SqlStr(STORENO_S.Trim()));
                }
                if (!string.IsNullOrEmpty(STORENO_E))
                {
                    sb.Append(" AND STORE_NO <= ");
                    sb.Append(Advtek.Utility.OracleDBUtil.SqlStr(STORENO_E.Trim()));
                }
                sb.AppendFormat("  GROUP BY STORE_NO,   STORE_NAME,   UNI_NO,   TAXNO,   TAXABLE,     TAXABLE_NAME,   YYYYMM");



                sb.AppendFormat(" UNION ALL   SELECT '門市申報表總計' STORE_NO,  '' STORE_NAME, ''  UNI_NO, ''  TAXNO,  '' TAXABLE,  ''   TAXABLE_NAME,  '' YYYYMM, \n");
                sb.AppendFormat("   SUM(INVO_AMT) INVO_AMT,SUM(INVO_TAX) INVO_TAX,SUM(MINVO_AMT_3) MINVO_AMT_3,SUM(MINVO_TAX_3) MINVO_TAX_3,\n");
                sb.AppendFormat("   SUM(MINVO_AMT_2) MINVO_AMT_2,SUM(MINVO_TAX_2) MINVO_TAX_2,SUM(INVO_PAYOFF_AMT) INVO_PAYOFF_AMT,\n");
                sb.AppendFormat("   SUM(INVO_PAYOFF_TAX) INVO_PAYOFF_TAX,SUM(TOTAL_AMT) TOTAL_AMT,SUM(TOTAL_TAX) TOTAL_TAX \n");
                sb.AppendFormat("  FROM VW_RPL_STORE_TAX_DECLARATION1 T \n");
                sb.AppendFormat(" WHERE 1 = 1 \n");

                if (!string.IsNullOrEmpty(MONTH_S))
                {
                    sb.AppendFormat(" AND YYYYMM >= '{0}' ", MONTH_S.Trim());
                }
                if (!string.IsNullOrEmpty(MONTH_E))
                {
                    sb.AppendFormat(" AND YYYYMM <= '{0}' ", MONTH_E.Trim());
                }
                if (!string.IsNullOrEmpty(TAX) && (TAX) != "ALL")
                {
                    sb.Append(" AND TAXABLE_NAME = ");
                    sb.Append(Advtek.Utility.OracleDBUtil.SqlStr(TAX.Trim()));
                }
                if (!string.IsNullOrEmpty(STORENO_S))
                {
                    sb.Append(" AND STORE_NO >= ");
                    sb.Append(Advtek.Utility.OracleDBUtil.SqlStr(STORENO_S.Trim()));
                }
                if (!string.IsNullOrEmpty(STORENO_E))
                {
                    sb.Append(" AND STORE_NO <= ");
                    sb.Append(Advtek.Utility.OracleDBUtil.SqlStr(STORENO_E.Trim()));
                }
             

                DataTable dt = Advtek.Utility.OracleDBUtil.GetDataSet(objConn, sb.ToString()).Tables[0];

                return dt;
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
        }

        /*Author : 蔡坤霖
          Date : 2011 / 02 / 21
          Description: RPL015 發票跨月作廢明細表
         */
        public DataTable RPL015(string STORE_NO_S, string STORE_NO_E, string DATE_S, string DATE_E, string IS_PAYOFF, string S1)
        {
            OracleConnection objConn = null;

            try
            {
                objConn = OracleDBUtil.GetConnection();
                System.Text.StringBuilder sb = new System.Text.StringBuilder();

                #region SELECT
                sb.AppendFormat("SELECT STORE_NO || ' ' || STORE_NAME \"門市編號及名稱\", \n");
                sb.AppendFormat("       INVOICE_NO \"發票號碼\", \n");
                sb.AppendFormat("       INVOICE_TYPE \"聯式\", \n");
                sb.AppendFormat("       TO_CHAR(SH_BAD_DATE, 'YYYY/MM/DD') \"跨月作廢日期\", \n");
                sb.AppendFormat("       TO_CHAR(TRADE_DATE, 'YYYY/MM/DD') \"原交易日期\", \n");
                sb.AppendFormat("       MACHINE_ID || ' / ' || SALE_NO \"機台 / 原序號\", \n");
                sb.AppendFormat("       UNI_NO \"統一編號\", \n");
                sb.AppendFormat("       CASH_AMT \"現金\", \n");
                sb.AppendFormat("       CREDIT_CARD_AMT \"信用卡\", \n");
                sb.AppendFormat("       SETTLEMENT_AMT \"分期付款\", \n");
                sb.AppendFormat("       CHECK_AMT \"支票\", \n");
                sb.AppendFormat("       SALE_BEFORE_TAX \"未稅金額\", \n");
                sb.AppendFormat("       SALE_TAX \"稅額\", \n");
                sb.AppendFormat("       INVO_AMT \"發票金額\", \n");
                sb.AppendFormat("       IS_PAYOFF \"是否開立折讓單\", \n");
                sb.AppendFormat("       '' \"是否收回\" \n");
                sb.AppendFormat("  FROM VW_RPL_STORE_OVERMON_BAD T \n");
                sb.AppendFormat(" WHERE 1 = 1 \n");
                #endregion

                #region -WHERE-
                #region 門市編號
                if (!string.IsNullOrEmpty(STORE_NO_S))
                {
                    sb.Append(" AND T.STORE_NO >= ");
                    sb.Append(Advtek.Utility.OracleDBUtil.SqlStr(STORE_NO_S.Trim()));
                }
                if (!string.IsNullOrEmpty(STORE_NO_E))
                {
                    sb.Append(" AND T.STORE_NO <= ");
                    sb.Append(Advtek.Utility.OracleDBUtil.SqlStr(STORE_NO_E.Trim()));
                }
                #endregion

                #region 跨月作廢日期
                if (!string.IsNullOrEmpty(DATE_S))
                {
                    sb.Append(" AND TRUNC(T.SH_BAD_DATE) >= ");
                    sb.Append(Advtek.Utility.OracleDBUtil.DateStr(DATE_S.Trim()));
                }
                if (!string.IsNullOrEmpty(DATE_E))
                {
                    sb.Append(" AND TRUNC(T.SH_BAD_DATE) <= ");
                    sb.Append(Advtek.Utility.OracleDBUtil.DateStr(DATE_E.Trim()));
                }
                #endregion

                #region 是否開立折讓單
                if (!string.IsNullOrEmpty(IS_PAYOFF) && IS_PAYOFF != "ALL") sb.AppendFormat(" AND T.IS_PAYOFF ='{0}' ", IS_PAYOFF);
                #endregion
                #endregion

                DataTable dt = Advtek.Utility.OracleDBUtil.GetDataSet(objConn, sb.ToString()).Tables[0];

                return dt;
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
        }


        #region 宗佑
        /*
         Author：宗佑
         Date：100.2.14
         Description：RPL019 促銷及折扣設定項目明細表
        */
        public DataTable RPL019(string DISCOUNTTYPE, string DISCOUNTCODE, string ISERROR)
        {
            OracleConnection objConn = null;
            DataTable dt = null;
            try
            {
                objConn = OracleDBUtil.GetConnection();

                System.Text.StringBuilder sb = new System.Text.StringBuilder();

                #region -SELECT-
                sb.AppendFormat("SELECT T.DISCOUNT_TYPE_NAME AS 折扣類別    ");
                sb.AppendFormat("      ,T.DISCOUNT_CODE      AS 折扣料號    ");
                sb.AppendFormat("      ,T.DISCOUNT_NAME      AS 折扣名稱    ");
                sb.AppendFormat("      ,T.DISCOUNT_MONEY     AS 折扣金額    ");
                sb.AppendFormat("      ,TO_CHAR(T.S_DATE,'YYYY/MM/DD') AS 生效日    ");
                sb.AppendFormat("      ,TO_CHAR(T.E_DATE,'YYYY/MM/DD') AS 失效日    ");
                //sb.AppendFormat("      ,T.COST_CENTER_NO ||' '|| COST_CENTER_NAME AS 成本中心   ");
                sb.AppendFormat("      ,T.COST_CENTER_NO     AS 成本中心   ");
                sb.AppendFormat("      ,T.AMT                AS 成本中心負擔金額    ");
                sb.AppendFormat("      ,T.A1 AS 科目1   ");
                sb.AppendFormat("      ,T.B2 AS 科目2   ");
                sb.AppendFormat("      ,T.C3 AS 科目3   ");
                sb.AppendFormat("      ,T.D4 AS 科目4   ");
                sb.AppendFormat("      ,T.E5 AS 科目5   ");
                sb.AppendFormat("      ,T.F6 AS 科目6   ");
                sb.AppendFormat("  FROM VW_RPL_PROMODISCOUNT_REPORT T ");
                sb.AppendFormat(" WHERE 1 = 1 ");
                #endregion

                #region -WHERE-
                #region 折扣類別
                if (!string.IsNullOrEmpty(DISCOUNTTYPE) && DISCOUNTTYPE.Trim() != "ALL")
                {
                    sb.AppendFormat(" AND T.DISCOUNT_TYPE ='{0}' ", DISCOUNTTYPE);
                }
                #endregion
                #region 折扣料號
                if (!string.IsNullOrEmpty(DISCOUNTCODE))
                {
                    sb.Append(" AND T.DISCOUNT_CODE = ");
                    sb.Append(Advtek.Utility.OracleDBUtil.SqlStr(DISCOUNTCODE.Trim()));
                }
                #endregion
                #region 是否失效
                if (!string.IsNullOrEmpty(ISERROR) && ISERROR.Trim() != "ALL")
                {
                    if (ISERROR != "N")
                    {
                        sb.Append(" AND T.E_DATE IS NOT NULL ");
                        //sb.Append(Advtek.Utility.OracleDBUtil.SqlStr(DISCOUNT_CODE.Trim()));
                    }
                    else
                    {
                        sb.Append(" AND T.E_DATE IS NULL ");
                        //sb.Append(Advtek.Utility.OracleDBUtil.SqlStr(DISCOUNT_CODE.Trim()));
                    }
                }
                #endregion
                #endregion

                dt = Advtek.Utility.OracleDBUtil.GetDataSet(objConn, sb.ToString()).Tables[0];
                return dt;
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
        }
        #endregion

        /*Author : 蔡坤霖
          Date : 2011 / 02 / 08
          Description: RPL020 0元訂單明細表
         */
        public DataTable RPL020(string TRADE_DATE_S, string TRADE_DATE_E, string STORE_NO_S, string STORE_NO_E, string SALE_STATUS)
        {
            OracleConnection objConn = null;
            DataTable dt = null;
            try
            {
                objConn = OracleDBUtil.GetConnection();

                System.Text.StringBuilder sb = new System.Text.StringBuilder();

                #region -SELECT-
                sb.AppendFormat("SELECT TO_CHAR(T.TRADE_DATE,'YYYY/MM/DD') AS 交易日期 ");
                sb.AppendFormat("      ,T.STORE_NO         AS 門市編號       ");
                sb.AppendFormat("      ,T.STORE_NAME       AS 門市名稱       ");
                sb.AppendFormat("      ,T.SALE_NO          AS 交易序號       ");
                sb.AppendFormat("      ,T.SA_NO            AS SA單號         ");
                sb.AppendFormat("      ,T.MSISDN           AS 門號資料       ");
                sb.AppendFormat("      ,T.BILLING_ACCOUNT_ID AS 客戶帳號     ");
                sb.AppendFormat("      ,T.PRODNO           AS 商品料號       ");
                sb.AppendFormat("      ,T.PRODNAME         AS 商品及交易名稱 ");
                sb.AppendFormat("      ,T.UNIT_PRICE       AS 商品金額       ");
                sb.AppendFormat("      ,T.QUANTITY         AS 數量           ");
                sb.AppendFormat("      ,T.PROMOTION_CODE   AS 促銷代碼       ");
                sb.AppendFormat("      ,T.PRICEDATE        AS 是否變價       ");
                sb.AppendFormat("      ,T.SALE_STATUS_NAME AS 訂單狀態       ");
                //sb.AppendFormat("      ,DECODE(T.SALE_STATUS,'1','未結帳','2','已結帳','3','退貨作廢','4','跨月退貨作廢','5','換貨作廢','6','跨月換貨作廢',T.SALE_STATUS) AS 訂單狀態 ");
                sb.AppendFormat("  FROM VW_RPL_ZEROAMT_ORDER_CHK T ");
                sb.AppendFormat(" WHERE 1 = 1 ");
                #endregion

                #region -WHERE-
                #region 交易日期
                if (!string.IsNullOrEmpty(TRADE_DATE_S))
                {
                    sb.Append(" AND TRUNC(T.TRADE_DATE) >= ");
                    sb.Append(Advtek.Utility.OracleDBUtil.DateStr(TRADE_DATE_S.Trim()));
                }
                if (!string.IsNullOrEmpty(TRADE_DATE_E))
                {
                    sb.Append(" AND TRUNC(T.TRADE_DATE) <= ");
                    sb.Append(Advtek.Utility.OracleDBUtil.DateStr(TRADE_DATE_E.Trim()));
                }
                #endregion

                #region 門市編號
                if (!string.IsNullOrEmpty(STORE_NO_S))
                {
                    sb.Append(" AND T.STORE_NO >= ");
                    sb.Append(Advtek.Utility.OracleDBUtil.SqlStr(STORE_NO_S.Trim()));
                }
                if (!string.IsNullOrEmpty(STORE_NO_E))
                {
                    sb.Append(" AND T.STORE_NO <= ");
                    sb.Append(Advtek.Utility.OracleDBUtil.SqlStr(STORE_NO_E.Trim()));
                }
                #endregion

                #region 服務類型
                if (!string.IsNullOrEmpty(SALE_STATUS) && SALE_STATUS != "ALL") sb.AppendFormat(" AND T.SALE_STATUS ='{0}' ", SALE_STATUS);
                #endregion
                #endregion

                #region -ORDER BY-
                sb.AppendFormat(" ORDER BY T.TRADE_DATE ,T.STORE_NO ,T.SALE_NO ");
                #endregion

                dt = Advtek.Utility.OracleDBUtil.GetDataSet(objConn, sb.ToString()).Tables[0];
                return dt;
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
        }

        /*Author : 蔡坤霖
          Date : 2011 / 02 / 09
          Description: RPL021 易付卡及儲值卡卡片銷售明細
         */
        /// <summary>
        /// RPL021 易付卡及儲值卡卡片銷售明細
        /// </summary>
        /// <param name="TRADE_DATE_S">銷售日期_起</param>
        /// <param name="TRADE_DATE_E">銷售日期_訖</param>
        /// <param name="PRODNO">料號</param>
        /// <returns></returns>
        public DataTable RPL021(string TRADE_DATE_S, string TRADE_DATE_E, string PRODNO_S, string PRODNO_E)
        {
            OracleConnection objConn = null;
            DataTable dt = null;
            try
            {
                objConn = OracleDBUtil.GetConnection();

                System.Text.StringBuilder sb = new System.Text.StringBuilder();

                #region -SELECT-
                sb.AppendFormat("SELECT TO_CHAR(T.TRADE_DATE,'YYYY/MM/DD')   AS 銷售日期, ");
                sb.AppendFormat("       T.SALE_NO      AS 交易序號, ");
                sb.AppendFormat("       T.STORE_NO     AS 門市編號, ");
                sb.AppendFormat("       T.STORE_NAME   AS 門市名稱, ");
                sb.AppendFormat("       T.SALE_STATUS  AS 交易類型, ");
                sb.AppendFormat("       T.CARD_TYPE    AS 卡別類型, ");
                sb.AppendFormat("       T.PRODNO       AS 商品料號, ");
                sb.AppendFormat("       T.PRODNAME     AS 商品名稱, ");
                sb.AppendFormat("       T.SIM_CARD_NO  AS 卡號,     ");
                sb.AppendFormat("       T.TOTAL_AMOUNT AS 金額,     ");
                sb.AppendFormat("       T.ORI_NO       AS 原交易序號");

                sb.AppendFormat("  FROM VW_RPL_ECARD_SALE_REPORT T ");
                sb.AppendFormat(" WHERE 1 = 1 ");
                #endregion

                #region -WHERE-
                #region 銷售日期
                if (!string.IsNullOrEmpty(TRADE_DATE_S))
                {
                    sb.Append(" AND TRUNC(T.TRADE_DATE) >= ");
                    sb.Append(Advtek.Utility.OracleDBUtil.DateStr(TRADE_DATE_S.Trim()));
                }
                if (!string.IsNullOrEmpty(TRADE_DATE_E))
                {
                    sb.Append(" AND TRUNC(T.TRADE_DATE) <= ");
                    sb.Append(Advtek.Utility.OracleDBUtil.DateStr(TRADE_DATE_E.Trim()));
                }
                #endregion
                #region 商品料號 若未輸滿9碼，料號(起)右邊補0至9碼；料號(訖)右邊補9至9碼。
                if (!string.IsNullOrEmpty(PRODNO_S) && PRODNO_S != "") sb.AppendFormat(" AND T.PRODNO >='{0}' ", StringUtil.Pad(PRODNO_S, "0", 9, "R"));
                if (!string.IsNullOrEmpty(PRODNO_E) && PRODNO_E != "") sb.AppendFormat(" AND T.PRODNO <='{0}' ", StringUtil.Pad(PRODNO_E, "9", 9, "R"));
                #endregion
                #endregion

                #region -ORDER BY-
                sb.AppendFormat(" ORDER BY TO_CHAR(T.TRADE_DATE,'YYYY/MM/DD') ,T.STORE_NO ");
                #endregion

                dt = Advtek.Utility.OracleDBUtil.GetDataSet(objConn, sb.ToString()).Tables[0];
                return dt;
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

        }

        /*Author : 蔡坤霖
          Date : 2011 / 02 / 17
          Description: RPL034 門市銷售量分析表
         */
        /// <summary>
        /// RPL034
        /// </summary>
        /// <param name="TRADE_DATE_S">交易日期(起)</param>
        /// <param name="TRADE_DATE_E">交易日期(訖)</param>
        /// <param name="PRODTYPENO_S">商品類別(起)</param>
        /// <param name="PRODTYPENO_E">商品類別(訖)</param>
        /// <param name="PRODNO_S">商品料號(起)</param>
        /// <param name="PRODNO_E">商品料號(訖)</param>
        public DataTable RPL034(string TRADE_DATE_S, string TRADE_DATE_E, string PRODTYPENO_S, string PRODTYPENO_E,
                                string PRODNO_S, string PRODNO_E)
        {

            StringBuilder sb = new StringBuilder();
            StringBuilder strSelect = new StringBuilder();

            #region 組合SQL
            #region 區域
            //sb.AppendFormat("SELECT S.ZONE, S.ZONE_NAME, '1' AS TYPE ");
            //sb.AppendFormat("  FROM ZONE S ");
            //sb.AppendFormat(" ORDER BY ZONE ");
            sb.AppendFormat(" SELECT S.ZONE ,S.ZONE_NAME ,S.ROWN ,'1' AS TYPE FROM (                                 ");
            sb.AppendFormat(" SELECT ZONE, ZONE_NAME, '1' AS ROWN FROM ZONE WHERE SUBSTR(ZONE_NAME,1,1) = '北' UNION ");
            sb.AppendFormat(" SELECT ZONE, ZONE_NAME, '2' AS ROWN FROM ZONE WHERE SUBSTR(ZONE_NAME,1,1) = '中' UNION ");
            sb.AppendFormat(" SELECT ZONE, ZONE_NAME, '3' AS ROWN FROM ZONE WHERE SUBSTR(ZONE_NAME,1,1) = '南') S    ");
            sb.AppendFormat(" ORDER BY S.ROWN ,S.ZONE                                                                ");

            DataTable dt = OracleDBUtil.Query_Data(sb.ToString());

            foreach (DataRow dr in dt.Rows)
            {
                strSelect.AppendFormat(",\n");
                strSelect.AppendFormat("SUM(CASE WHEN T.ZONE = '{0}' AND T.CLOSEDATE IS NULL THEN T.TOTAL_AMOUNT ELSE 0 END) AS \"{1}\"", dr["ZONE"].ToString(), dr["ZONE_NAME"].ToString().PadRight(10, ' ').Substring(0, 10));
            }
            //ST.CHNNAL_TYPE 
            strSelect.AppendFormat(",\n");
            strSelect.AppendFormat(" SUM(CASE WHEN SUBSTR(T.CHNNAL_TYPE,1,1) = 'P' AND T.CLOSEDATE IS NULL THEN T.TOTAL_AMOUNT ELSE 0 END) AS Paradigm ");
            strSelect.AppendFormat(",\n");
            strSelect.AppendFormat(" SUM(CASE WHEN SUBSTR(T.CHNNAL_TYPE,1,1) = 'C' AND T.CLOSEDATE IS NULL THEN T.TOTAL_AMOUNT ELSE 0 END) AS CONCEPT ");
            strSelect.AppendFormat(",\n");
            strSelect.AppendFormat(" SUM(CASE WHEN SUBSTR(T.ZONE,1,1) = '7' AND T.CLOSEDATE IS NULL AND (TRIM(T.CHNNAL_TYPE) = '' OR TRIM(T.CHNNAL_TYPE) IS NULL) THEN T.TOTAL_AMOUNT ELSE 0 END) AS Repair ");
            strSelect.AppendFormat(",\n");
            strSelect.AppendFormat(" SUM(CASE WHEN T.CLOSEDATE IS NOT NULL THEN T.TOTAL_AMOUNT ELSE 0 END) AS 閉店門市 ");
            #endregion

            #region 門市
            sb = new StringBuilder();

            sb.AppendFormat("SELECT T.STORE_NO AS ZONE, T.STORENAME AS ZONE_NAME, '2' AS TYPE ");
            sb.AppendFormat("  FROM STORE T ");
            sb.AppendFormat(" WHERE T.CLOSEDATE IS NULL ");
            sb.AppendFormat(" ORDER BY ZONE ");

            dt = OracleDBUtil.Query_Data(sb.ToString());

            foreach (DataRow dr in dt.Rows)
            {
                strSelect.AppendFormat(",\n");
                strSelect.AppendFormat("SUM(CASE WHEN T.ZONE = '{0}' AND T.CLOSEDATE IS NULL THEN T.TOTAL_AMOUNT ELSE 0 END) AS \"{1}\"", dr["ZONE"].ToString(), dr["ZONE_NAME"].ToString().PadRight(10, ' ').Substring(0, 10));
            }
            #endregion
            #endregion

            sb = new StringBuilder();

            #region -SELECT-

            sb.AppendFormat("SELECT ");
            sb.AppendFormat(" T.PRODNO          AS ITEMCODE, \n");
            sb.AppendFormat(" T.PRODNAME        AS 品名, \n");
            sb.AppendFormat(" T.SUPPNAME        AS 廠商名稱, \n");
            sb.AppendFormat(" SUM(CASE WHEN T.ORDER_INDEX ='1' THEN T.TOTAL_AMOUNT ELSE 0 END) AS 全區 ");
            sb.Append(strSelect.ToString());
            sb.AppendFormat("  FROM VW_RPL_PIVOT_STORE_SALAMT T \n");
            sb.AppendFormat(" WHERE 1 = 1 ");

            #endregion

            #region -WHERE-

            #region 交易日期
            if (!string.IsNullOrEmpty(TRADE_DATE_S))
            {
                sb.Append(" AND TRUNC(T.TRADE_DATE) >= ");
                sb.Append(Advtek.Utility.OracleDBUtil.DateStr(TRADE_DATE_S.Trim()));
            }
            if (!string.IsNullOrEmpty(TRADE_DATE_E))
            {
                sb.Append(" AND TRUNC(T.TRADE_DATE) <= ");
                sb.Append(Advtek.Utility.OracleDBUtil.DateStr(TRADE_DATE_E.Trim()));
            }
            #endregion
            #region 商品類別
            if (!string.IsNullOrEmpty(PRODTYPENO_S))
            {
                sb.Append(" AND T.PRODTYPENO >= ");
                sb.Append(Advtek.Utility.OracleDBUtil.SqlStr(PRODTYPENO_S.Trim()));
            }
            if (!string.IsNullOrEmpty(PRODTYPENO_E))
            {
                sb.Append(" AND T.PRODTYPENO <= ");
                sb.Append(Advtek.Utility.OracleDBUtil.SqlStr(PRODTYPENO_E.Trim()));
            }
            #endregion
            #region 商品料號
            if (!string.IsNullOrEmpty(PRODNO_S))
            {
                sb.Append(" AND T.PRODNO >= ");
                sb.Append(Advtek.Utility.OracleDBUtil.SqlStr(PRODNO_S.Trim()));
            }
            if (!string.IsNullOrEmpty(PRODNO_E))
            {
                sb.Append(" AND T.PRODNO <= ");
                sb.Append(Advtek.Utility.OracleDBUtil.SqlStr(PRODNO_E.Trim()));
            }
            #endregion

            #endregion

            sb.AppendFormat(" GROUP BY T.PRODNO, T.PRODNAME, T.SUPPNAME \n");
            sb.AppendFormat(" ORDER BY T.PRODNO, T.PRODNAME, T.SUPPNAME \n");

            dt = OracleDBUtil.Query_Data(sb.ToString());

            return dt;
        }

        /*Author : 蔡坤霖
          Date : 2011 / 02 / 16
          Description: RPL037 促銷及補貼檢核表
         */
        /// <summary>
        /// RPL037 促銷及補貼檢核表
        /// </summary>
        /// <param name="B_DATE_S">促銷生效日_起</param>
        /// <param name="B_DATE_E">促銷生效日_訖</param>
        /// <param name="PROMO_NO_S">促銷代碼_起</param>
        /// <param name="PROMO_NO_E">促銷代碼_訖</param>
        /// <param name="CATEGORY_S">商品類別_起</param>
        /// <param name="CATEGORY_E">商品類別_訖</param>
        /// <returns></returns>
        public DataTable RPL037(string B_DATE_S, string B_DATE_E, string PROMO_NO_S, string PROMO_NO_E, string CATEGORY_S, string CATEGORY_E)
        {

            OracleConnection objConn = null;
            DataTable dt = null;
            try
            {
                objConn = OracleDBUtil.GetConnection();

                System.Text.StringBuilder sb = new System.Text.StringBuilder();

                #region -SELECT-
                sb.AppendFormat("SELECT PROMO_NO 促銷代碼, ");
                sb.AppendFormat("       PROMO_NAME 促銷名稱, ");
                sb.AppendFormat("       TO_CHAR(B_DATE,'YYYY/MM/DD') 促銷生效日, ");
                sb.AppendFormat("       TO_CHAR(E_DATE,'YYYY/MM/DD') 促銷失效日, ");
                sb.AppendFormat("       PROMO_SUBSIDY 促銷補貼金額, ");
                sb.AppendFormat("       BASE_SUBSIDY 基準補貼金額, ");
                sb.AppendFormat("       PROMO_PROD_GROUP 商品群組, ");
                sb.AppendFormat("       CATEGORY 商品類別, ");
                sb.AppendFormat("       DEVICE_SUBSIDY 補貼金額, ");
                sb.AppendFormat("       ERP_ATTRIBUTE_1 \"ERP Attribute1\", ");
                sb.AppendFormat("       TRANS_VALUE 轉換值, ");
                sb.AppendFormat("       TO_CHAR(B_DATE1,'YYYY/MM/DD') 生效日, ");
                sb.AppendFormat("       TO_CHAR(E_DATE1,'YYYY/MM/DD') 失效日 ");
                sb.AppendFormat("  FROM VW_RPL037 T");

                sb.AppendFormat(" WHERE 1 = 1 ");
                #endregion

                #region -WHERE-

                #region 促銷生效日
                if (!string.IsNullOrEmpty(B_DATE_S))
                {
                    sb.Append(" AND TRUNC(T.B_DATE) >= ");
                    sb.Append(Advtek.Utility.OracleDBUtil.DateStr(B_DATE_S.Trim()));
                }
                if (!string.IsNullOrEmpty(B_DATE_E))
                {
                    sb.Append(" AND TRUNC(T.B_DATE) <= ");
                    sb.Append(Advtek.Utility.OracleDBUtil.DateStr(B_DATE_E.Trim()));
                }
                #endregion

                #region 促銷代號
                if (!string.IsNullOrEmpty(PROMO_NO_S))
                {
                    sb.Append(" AND T.PROMO_NO >= ");
                    sb.Append(Advtek.Utility.OracleDBUtil.SqlStr(PROMO_NO_S.Trim()));
                }
                if (!string.IsNullOrEmpty(PROMO_NO_E))
                {
                    sb.Append(" AND T.PROMO_NO <= ");
                    sb.Append(Advtek.Utility.OracleDBUtil.SqlStr(PROMO_NO_E.Trim()));
                }
                #endregion

                #region 商品類別
                if (!string.IsNullOrEmpty(CATEGORY_S))
                {
                    sb.Append(" AND T.CATEGORY >= ");
                    sb.Append(Advtek.Utility.OracleDBUtil.SqlStr(CATEGORY_S.Trim()));
                }
                if (!string.IsNullOrEmpty(CATEGORY_E))
                {
                    sb.Append(" AND T.CATEGORY <= ");
                    sb.Append(Advtek.Utility.OracleDBUtil.SqlStr(CATEGORY_E.Trim()));
                }
                #endregion

                #endregion

                dt = Advtek.Utility.OracleDBUtil.GetDataSet(objConn, sb.ToString()).Tables[0];
                return dt;
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

        }

        /*Author : 蔡坤霖
          Date : 2011 / 02 / 16
          Description: RPL038 促銷新增商品檢核表
         */
        /// <summary>
        /// RPL038 促銷新增商品檢核表
        /// </summary>
        /// <param name="B_DATE_S">促銷生效日期(起)</param>
        /// <param name="B_DATE_E">促銷生效日期(訖)</param>
        /// <param name="PROMO_NO_S">促銷代號(起)</param>
        /// <param name="PROMO_NO_E">促銷代號(訖)</param>
        /// <param name="PRODTYPENO">商品類別ID</param>
        public DataTable RPL038(string B_DATE_S, string B_DATE_E, string PROMO_NO_S, string PROMO_NO_E, string PRODTYPENO)
        {
            StringBuilder sb = new StringBuilder();

            #region -SELECT-
            sb.AppendFormat("SELECT T.PROMO_NO        促銷代碼, ");
            sb.AppendFormat("       T.PROMO_NAME      促銷名稱, ");
            sb.AppendFormat("       T.PRODNO          商品料號, ");
            sb.AppendFormat("       T.PRODNAME        商品名稱, ");
            sb.AppendFormat("       T.CATEGORY        商品類別, ");
            sb.AppendFormat("       T.ERP_ATTRIBUTE_1 \"ERP Attribute1\" ");
            sb.AppendFormat("  FROM VW_RPL_PORMO_PRODADD_VALD T ");
            #endregion

            #region -WHERE-
            sb.Append(" WHERE 1 = 1");

            if (!string.IsNullOrEmpty(B_DATE_S))
            {
                sb.Append(" AND TRUNC(B_DATE) >= " + OracleDBUtil.DateStr(B_DATE_S.Trim()));
            }

            if (!string.IsNullOrEmpty(B_DATE_E))
            {
                sb.Append(" AND TRUNC(B_DATE) <= " + OracleDBUtil.DateStr(B_DATE_E.Trim()));
            }

            if (!string.IsNullOrEmpty(PROMO_NO_S))
            {
                sb.Append(" AND PROMO_NO >= " + OracleDBUtil.SqlStr(PROMO_NO_S.Trim()));
            }

            if (!string.IsNullOrEmpty(PROMO_NO_E))
            {
                sb.Append(" AND PROMO_NO <= " + OracleDBUtil.SqlStr(PROMO_NO_E.Trim()));
            }

            if (!string.IsNullOrEmpty(PRODTYPENO) && PRODTYPENO.ToUpper() != "ALL")
            {
                sb.Append(" AND CATEGORY = " + OracleDBUtil.SqlStr(PRODTYPENO.Trim()));
            }
            #endregion


            DataTable dt = OracleDBUtil.Query_Data(sb.ToString());
            return dt;
        }

        /*Author : 蔡坤霖
          Date : 2011 / 02 / 10
          Description: RPL044 各項交易明細表
         */
        /// <summary>
        /// RPL044 各項交易明細表
        /// </summary>
        /// <param name="B_DATE_S">促銷生效日期(起)</param>
        /// <param name="B_DATE_E">促銷生效日期(訖)</param>
        /// <param name="PROMO_NO_S">促銷代號(起)</param>
        /// <param name="PROMO_NO_E">促銷代號(訖)</param>
        /// <param name="PRODNAME">商品名稱</param>
        /// <param name="STORENO">門市編號</param>
        public DataTable RPL044(string B_DATE_S, string B_DATE_E, string PROMO_NO_S, string PROMO_NO_E, string PRODNAME, string STORENO)
        {
            StringBuilder sb = new StringBuilder();
            StringBuilder strSelect = new StringBuilder();

            #region 組合SQL
            #region 區域
            //sb.AppendFormat("SELECT S.ZONE, S.ZONE_NAME, '1' AS TYPE ");
            //sb.AppendFormat("  FROM ZONE S ");
            //sb.AppendFormat(" ORDER BY ZONE ");
            sb.AppendFormat(" SELECT S.ZONE ,S.ZONE_NAME ,S.ROWN ,'1' AS TYPE FROM (                                 ");
            sb.AppendFormat(" SELECT ZONE, ZONE_NAME, '1' AS ROWN FROM ZONE WHERE SUBSTR(ZONE_NAME,1,1) = '北' UNION ");
            sb.AppendFormat(" SELECT ZONE, ZONE_NAME, '2' AS ROWN FROM ZONE WHERE SUBSTR(ZONE_NAME,1,1) = '中' UNION ");
            sb.AppendFormat(" SELECT ZONE, ZONE_NAME, '3' AS ROWN FROM ZONE WHERE SUBSTR(ZONE_NAME,1,1) = '南') S    ");
            sb.AppendFormat(" ORDER BY S.ROWN ,S.ZONE                                                                ");

            DataTable dt = OracleDBUtil.Query_Data(sb.ToString());

            foreach (DataRow dr in dt.Rows)
            {
                strSelect.AppendFormat(",\n");
                strSelect.AppendFormat("SUM(CASE WHEN T.ZONE = '{0}' AND T.CLOSEDATE IS NULL THEN T.TOTAL_AMOUNT ELSE 0 END) AS \"{1}\"", dr["ZONE"].ToString(), dr["ZONE_NAME"].ToString().PadRight(10, ' ').Substring(0, 10));
            }
            //ST.CHNNAL_TYPE 
            strSelect.AppendFormat(",\n");
            strSelect.AppendFormat(" SUM(CASE WHEN SUBSTR(T.CHNNAL_TYPE,1,1) = 'P' AND T.CLOSEDATE IS NULL THEN T.TOTAL_AMOUNT ELSE 0 END) AS Paradigm ");
            strSelect.AppendFormat(",\n");
            strSelect.AppendFormat(" SUM(CASE WHEN SUBSTR(T.CHNNAL_TYPE,1,1) = 'C' AND T.CLOSEDATE IS NULL THEN T.TOTAL_AMOUNT ELSE 0 END) AS CONCEPT ");
            strSelect.AppendFormat(",\n");
            strSelect.AppendFormat(" SUM(CASE WHEN SUBSTR(T.ZONE,1,1) = '7' AND T.CLOSEDATE IS NULL AND (TRIM(T.CHNNAL_TYPE) = '' OR TRIM(T.CHNNAL_TYPE) IS NULL) THEN T.TOTAL_AMOUNT ELSE 0 END) AS Repair ");
            strSelect.AppendFormat(",\n");
            strSelect.AppendFormat(" SUM(CASE WHEN T.CLOSEDATE IS NOT NULL THEN T.TOTAL_AMOUNT ELSE 0 END) AS 閉店門市 ");
            #endregion

            #region 門市
            sb = new StringBuilder();

            sb.AppendFormat("SELECT T.STORE_NO AS ZONE, T.STORENAME AS ZONE_NAME, '2' AS TYPE ");
            sb.AppendFormat("  FROM STORE T ");
            sb.AppendFormat(" WHERE T.CLOSEDATE IS NULL ");
            if (!string.IsNullOrEmpty(STORENO) && STORENO.Length == 4)
            {
                sb.AppendFormat(" AND T.STORE_NO = " + OracleDBUtil.SqlStr(STORENO));
            }
            sb.AppendFormat(" ORDER BY ZONE ");

            dt = OracleDBUtil.Query_Data(sb.ToString());

            foreach (DataRow dr in dt.Rows)
            {
                strSelect.AppendFormat(",\n");
                strSelect.AppendFormat("SUM(CASE WHEN T.ZONE = '{0}' AND T.CLOSEDATE IS NULL THEN T.TOTAL_AMOUNT ELSE 0 END) AS \"{1}\"", dr["ZONE"].ToString(), dr["ZONE_NAME"].ToString().PadRight(10, ' ').Substring(0, 10));
            }
            #endregion
            #endregion

            sb = new StringBuilder();

            #region -SELECT-

            sb.AppendFormat("SELECT ");
            sb.AppendFormat(" T.PROMOTION_CODE AS 促銷代碼, \n");
            sb.AppendFormat(" T.PROMO_NAME AS 促銷名稱, \n");
            sb.AppendFormat(" T.PRODNAME AS 商品名稱, \n");
            sb.AppendFormat(" SUM(CASE WHEN T.ORDER_INDEX ='1' THEN T.TOTAL_AMOUNT ELSE 0 END) AS 全區 ");
            sb.Append(strSelect.ToString());
            sb.AppendFormat("  FROM VW_RPL_PIVOT_PROMO_PROD_DTL T \n");
            sb.AppendFormat(" WHERE 1 = 1 ");

            #endregion

            #region -WHERE-

            #region 促銷生效日期
            if (!string.IsNullOrEmpty(B_DATE_S))
            {
                sb.Append(" AND TRUNC(T.B_DATE) >= ");
                sb.Append(Advtek.Utility.OracleDBUtil.DateStr(B_DATE_S.Trim()));
            }
            if (!string.IsNullOrEmpty(B_DATE_E))
            {
                sb.Append(" AND TRUNC(T.B_DATE) <= ");
                sb.Append(Advtek.Utility.OracleDBUtil.DateStr(B_DATE_E.Trim()));
            }
            #endregion
            #region 促銷代號
            if (!string.IsNullOrEmpty(PROMO_NO_S))
            {
                sb.Append(" AND T.PROMOTION_CODE >= ");
                sb.Append(Advtek.Utility.OracleDBUtil.SqlStr(PROMO_NO_S.Trim()));
            }
            if (!string.IsNullOrEmpty(PROMO_NO_E))
            {
                sb.Append(" AND T.PROMOTION_CODE <= ");
                sb.Append(Advtek.Utility.OracleDBUtil.SqlStr(PROMO_NO_E.Trim()));
            }
            #endregion
            #region 商品名稱
            if (!string.IsNullOrEmpty(PRODNAME))
            {
                sb.Append(" AND T.PRODNAME LIKE ");
                sb.Append(Advtek.Utility.OracleDBUtil.LikeStr(PRODNAME.Trim()));
            }
            #endregion

            #endregion

            sb.AppendFormat(" GROUP BY T.PROMOTION_CODE, T.PROMO_NAME, T.PRODNAME \n");
            sb.AppendFormat(" ORDER BY T.PROMOTION_CODE, T.PROMO_NAME, T.PRODNAME \n");

            dt = OracleDBUtil.Query_Data(sb.ToString());

            return dt;
        }

        #region 宗佑
        /*author：宗佑
          date：100.2.17
          description：RPL054 SQL重新改寫
        */
        public DataTable RPL054(string ISCONSIGNMENT, string TRADE_DATE_S, string TRADE_DATE_E,
            string PRODTYPENO_S, string PRODTYPENO_E,
            string PRODNO_S, string PRODNO_E, string SALE_STATUS, string STORE_NO)
        {
            OracleConnection objConn = null;

            try
            {
                objConn = OracleDBUtil.GetConnection();
                System.Text.StringBuilder sb = new System.Text.StringBuilder();

                sb.AppendFormat("WITH RPL054 AS ( \n");
                sb.AppendFormat("SELECT TO_CHAR(TRADE_DATE, 'YYYY/MM/DD') AS 交易日期, \n");
                sb.AppendFormat("       PRODNO AS 商品料號, \n");
                sb.AppendFormat("       PRODNAME AS 商品名稱, \n");
                sb.AppendFormat("       NVL(PROD_QTY,0) AS 數量, \n");
                sb.AppendFormat("       NVL(TOTAL_AMOUNT,0) AS 金額, \n");
                sb.AppendFormat("       SALE_STATUS_NAME AS 銷售型態, \n");
                sb.AppendFormat("       SALE_STATUS \n");
                sb.AppendFormat("  FROM VW_RPL_STORE_SALE_STATIC \n");
                sb.AppendFormat(" WHERE 1 = 1 \n");

                #region WHERE

                if (!string.IsNullOrEmpty(TRADE_DATE_S))
                {
                    sb.Append(" AND TRUNC(TRADE_DATE) >= ");
                    sb.Append(Advtek.Utility.OracleDBUtil.DateStr(TRADE_DATE_S.Trim()));
                }
                if (!string.IsNullOrEmpty(TRADE_DATE_E))
                {
                    sb.Append(" AND TRUNC(TRADE_DATE) <= ");
                    sb.Append(Advtek.Utility.OracleDBUtil.DateStr(TRADE_DATE_E.Trim()));
                }
                if (!string.IsNullOrEmpty(PRODTYPENO_S))
                {
                    sb.Append(" AND PRODTYPENO >= ");
                    sb.Append(Advtek.Utility.OracleDBUtil.SqlStr(PRODTYPENO_S.Trim()));
                }
                if (!string.IsNullOrEmpty(PRODTYPENO_E))
                {
                    sb.Append(" AND PRODTYPENO <= ");
                    sb.Append(Advtek.Utility.OracleDBUtil.SqlStr(PRODTYPENO_E.Trim()));
                }
                if (!string.IsNullOrEmpty(PRODNO_S))
                {
                    sb.Append(" AND PRODNO >= ");
                    sb.Append(Advtek.Utility.OracleDBUtil.SqlStr(PRODNO_S.Trim()));
                }
                if (!string.IsNullOrEmpty(PRODNO_E))
                {
                    sb.Append(" AND PRODNO <= ");
                    sb.Append(Advtek.Utility.OracleDBUtil.SqlStr(PRODNO_E.Trim()));
                }
                if (SALE_STATUS != "0")
                {
                    sb.Append(" AND SALE_STATUS_NAME = ");
                    sb.Append(Advtek.Utility.OracleDBUtil.SqlStr(SALE_STATUS.Trim()));
                }
                if (!string.IsNullOrEmpty(ISCONSIGNMENT) && ISCONSIGNMENT.ToUpper() != "ALL")
                {
                    if (ISCONSIGNMENT == "0")
                    {
                        sb.Append(" AND ( ISCONSIGNMENT = ");
                        sb.Append(Advtek.Utility.OracleDBUtil.SqlStr(ISCONSIGNMENT.Trim()));
                        sb.Append(" OR  ISCONSIGNMENT IS NULL ) ");
                    }
                    else
                    {
                        sb.Append(" AND ISCONSIGNMENT = ");
                        sb.Append(Advtek.Utility.OracleDBUtil.SqlStr(ISCONSIGNMENT.Trim()));
                    }
                }
                if (!string.IsNullOrEmpty(STORE_NO) && STORE_NO.Length == 4)
                {
                    sb.Append(" AND STORE_NO = " + OracleDBUtil.SqlStr(STORE_NO.Trim()));
                }
                #endregion

                sb.AppendFormat(" ) \n");
                sb.AppendFormat("  \n");
                sb.AppendFormat(" SELECT * \n");
                sb.AppendFormat("   FROM RPL054 \n");

                #region 統計部分 - 畫面不顯示

                //sb.AppendFormat(" UNION ALL \n");
                //sb.AppendFormat(" SELECT '銷貨總計', \n");
                //sb.AppendFormat("        NULL, \n");
                //sb.AppendFormat("        NULL, \n");
                //sb.AppendFormat("        NVL(SUM(數量), 0), \n");
                //sb.AppendFormat("        NVL(SUM(金額), 0), \n");
                //sb.AppendFormat("        NULL, \n");
                //sb.AppendFormat("        NULL \n");
                //sb.AppendFormat("   FROM RPL054 \n");
                //sb.AppendFormat("  WHERE SALE_STATUS = 2 \n");
                //sb.AppendFormat("  \n");
                //sb.AppendFormat(" UNION ALL \n");
                //sb.AppendFormat(" SELECT '銷退總計', \n");
                //sb.AppendFormat("        NULL, \n");
                //sb.AppendFormat("        NULL, \n");
                //sb.AppendFormat("        NVL(SUM(數量), 0), \n");
                //sb.AppendFormat("        NVL(SUM(金額), 0), \n");
                //sb.AppendFormat("        NULL, \n");
                //sb.AppendFormat("        NULL \n");
                //sb.AppendFormat("   FROM RPL054 \n");
                //sb.AppendFormat("  WHERE SALE_STATUS IN (3, 4, 5, 6) \n");
                //sb.AppendFormat("  \n");
                //sb.AppendFormat("  UNION ALL \n");
                //sb.AppendFormat(" SELECT '總計', \n");
                //sb.AppendFormat("        NULL, \n");
                //sb.AppendFormat("        NULL, \n");
                //sb.AppendFormat("        NVL(SUM(數量), 0), \n");
                //sb.AppendFormat("        NVL(SUM(金額), 0), \n");
                //sb.AppendFormat("        NULL, \n");
                //sb.AppendFormat("        NULL \n");
                //sb.AppendFormat("   FROM RPL054 \n");

                #endregion

                DataTable dt = Advtek.Utility.OracleDBUtil.GetDataSet(objConn, sb.ToString()).Tables[0];

                return dt;
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
        }

        /*author：宗佑
          date：100.2.17
          description：RPL054_SUM(銷貨/銷退小計總計)
        */
        public DataTable RPL054_SUM(string ISCONSIGNMENT, string TRADE_DATE_S, string TRADE_DATE_E,
            string PRODTYPENO_S, string PRODTYPENO_E,
            string PRODNO_S, string PRODNO_E, string SALE_STATUS, string STORE_NO)
        {
            OracleConnection objConn = null;

            try
            {
                objConn = OracleDBUtil.GetConnection();
                System.Text.StringBuilder sb = new System.Text.StringBuilder();

                sb.AppendFormat("WITH RPL054 AS ( \n");
                sb.AppendFormat("SELECT TO_CHAR(TRADE_DATE, 'YYYY/MM/DD') AS 交易日期, \n");
                sb.AppendFormat("       PRODNO AS 商品料號, \n");
                sb.AppendFormat("       PRODNAME AS 商品名稱, \n");
                sb.AppendFormat("       NVL(PROD_QTY,0) AS 數量, \n");
                sb.AppendFormat("       NVL(TOTAL_AMOUNT,0) AS 金額, \n");
                sb.AppendFormat("       SALE_STATUS_NAME AS 銷售型態, \n");
                sb.AppendFormat("       SALE_STATUS \n");
                sb.AppendFormat("  FROM VW_RPL_STORE_SALE_STATIC \n");
                sb.AppendFormat(" WHERE 1 = 1 \n");

                #region WHERE

                if (!string.IsNullOrEmpty(TRADE_DATE_S))
                {
                    sb.Append(" AND TRUNC(TRADE_DATE) >= ");
                    sb.Append(Advtek.Utility.OracleDBUtil.DateStr(TRADE_DATE_S.Trim()));
                }
                if (!string.IsNullOrEmpty(TRADE_DATE_E))
                {
                    sb.Append(" AND TRUNC(TRADE_DATE) <= ");
                    sb.Append(Advtek.Utility.OracleDBUtil.DateStr(TRADE_DATE_E.Trim()));
                }
                if (!string.IsNullOrEmpty(PRODTYPENO_S))
                {
                    sb.Append(" AND PRODTYPENO >= ");
                    sb.Append(Advtek.Utility.OracleDBUtil.SqlStr(PRODTYPENO_S.Trim()));
                }
                if (!string.IsNullOrEmpty(PRODTYPENO_E))
                {
                    sb.Append(" AND PRODTYPENO <= ");
                    sb.Append(Advtek.Utility.OracleDBUtil.SqlStr(PRODTYPENO_E.Trim()));
                }
                if (!string.IsNullOrEmpty(PRODNO_S))
                {
                    sb.Append(" AND PRODNO >= ");
                    sb.Append(Advtek.Utility.OracleDBUtil.SqlStr(PRODNO_S.Trim()));
                }
                if (!string.IsNullOrEmpty(PRODNO_E))
                {
                    sb.Append(" AND PRODNO <= ");
                    sb.Append(Advtek.Utility.OracleDBUtil.SqlStr(PRODNO_E.Trim()));
                }
                if (SALE_STATUS != "0")
                {
                    sb.Append(" AND SALE_STATUS_NAME = ");
                    sb.Append(Advtek.Utility.OracleDBUtil.SqlStr(SALE_STATUS.Trim()));
                }
                if (!string.IsNullOrEmpty(ISCONSIGNMENT) && ISCONSIGNMENT.ToUpper() != "ALL")
                {
                    if (ISCONSIGNMENT == "0")
                    {
                        sb.Append(" AND ( ISCONSIGNMENT = ");
                        sb.Append(Advtek.Utility.OracleDBUtil.SqlStr(ISCONSIGNMENT.Trim()));
                        sb.Append(" OR  ISCONSIGNMENT IS NULL ) ");
                    }
                    else
                    {
                        sb.Append(" AND ISCONSIGNMENT = ");
                        sb.Append(Advtek.Utility.OracleDBUtil.SqlStr(ISCONSIGNMENT.Trim()));
                    }
                }

                if (!string.IsNullOrEmpty(STORE_NO) && STORE_NO.Length == 4)
                {
                    sb.Append(" AND STORE_NO = " + OracleDBUtil.SqlStr(STORE_NO.Trim()));
                }
                #endregion

                sb.AppendFormat(" ) \n");
                sb.AppendFormat("  \n");
                sb.AppendFormat(" SELECT * \n");
                sb.AppendFormat("   FROM RPL054 \n");

                #region 統計部分 - 匯出顯示

                sb.AppendFormat(" UNION ALL \n");
                sb.AppendFormat(" SELECT '銷貨總計', \n");
                sb.AppendFormat("        NULL, \n");
                sb.AppendFormat("        NULL, \n");
                sb.AppendFormat("        NVL(SUM(數量), 0), \n");
                sb.AppendFormat("        NVL(SUM(金額), 0), \n");
                sb.AppendFormat("        NULL, \n");
                sb.AppendFormat("        NULL \n");
                sb.AppendFormat("   FROM RPL054 \n");
                sb.AppendFormat("  WHERE SALE_STATUS = '2' \n");
                sb.AppendFormat("  \n");
                sb.AppendFormat(" UNION ALL \n");
                sb.AppendFormat(" SELECT '銷退總計', \n");
                sb.AppendFormat("        NULL, \n");
                sb.AppendFormat("        NULL, \n");
                sb.AppendFormat("        NVL(SUM(數量), 0), \n");
                sb.AppendFormat("        NVL(SUM(金額), 0), \n");
                sb.AppendFormat("        NULL, \n");
                sb.AppendFormat("        NULL \n");
                sb.AppendFormat("   FROM RPL054 \n");
                sb.AppendFormat("  WHERE SALE_STATUS IN ('3', '4', '5', '6') \n");
                sb.AppendFormat("  \n");
                sb.AppendFormat("  UNION ALL \n");
                sb.AppendFormat(" SELECT '總計', \n");
                sb.AppendFormat("        NULL, \n");
                sb.AppendFormat("        NULL, \n");
                sb.AppendFormat("        NVL(SUM(數量), 0), \n");
                sb.AppendFormat("        NVL(SUM(金額), 0), \n");
                sb.AppendFormat("        NULL, \n");
                sb.AppendFormat("        NULL \n");
                sb.AppendFormat("   FROM RPL054 \n");
                if (SALE_STATUS != "0")
                { }
                #endregion

                DataTable dt = Advtek.Utility.OracleDBUtil.GetDataSet(objConn, sb.ToString()).Tables[0];

                return dt;
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
        }
        #endregion

        /*Author : 蔡坤霖
          Date : 2011 / 02 / 09
          Description: RPL056 商品銷售排行表
         */
        /// <summary>
        /// RPL056 商品銷售排行表
        /// </summary>
        /// <param name="ORDER"></param>
        /// <param name="ZONE"></param>
        /// <param name="STORE_NO_S"></param>
        /// <param name="STORE_NO_E"></param>
        /// <param name="PRODTYPENO_S"></param>
        /// <param name="PRODTYPENO_E"></param>
        /// <param name="PRODNO_S"></param>
        /// <param name="PRODNO_E"></param>
        /// <param name="TRADE_DATE_S"></param>
        /// <param name="TRADE_DATE_E"></param>
        /// <returns></returns>
        public DataTable RPL056(string ORDER, string ZONE, string STORE_NO_S, string STORE_NO_E,
                                string PRODTYPENO_S, string PRODTYPENO_E, string PRODNO_S, string PRODNO_E,
                                string TRADE_DATE_S, string TRADE_DATE_E)
        {
            OracleConnection objConn = null;
            DataTable dt = null;
            try
            {
                objConn = OracleDBUtil.GetConnection();

                System.Text.StringBuilder sb = new System.Text.StringBuilder();

                #region -SELECT-
                sb.AppendFormat("SELECT SH.STORE_NO             AS 門市編號, ");
                sb.AppendFormat("       ST.STORENAME            AS 門市名稱, ");
                sb.AppendFormat("       PROD_TYPE.PRODTYPENAME  AS 商品類別, ");
                sb.AppendFormat("       SD.PRODNO               AS 商品料號, ");
                sb.AppendFormat("       PROD.PRODNAME           AS 商品名稱, ");
                if (!string.IsNullOrEmpty(ORDER) && ORDER == "TOTAL_AMOUNT")
                { sb.AppendFormat("     RANK () OVER (ORDER BY SUM(SD.TOTAL_AMOUNT) DESC) AS 名次, "); }
                else
                { sb.AppendFormat("     RANK () OVER (ORDER BY SUM(SD.QUANTITY) DESC)     AS 名次, "); }
                sb.AppendFormat("       NVL(SUM(SD.QUANTITY),0)             AS 數量, ");
                sb.AppendFormat("       NVL(SUM(SD.TOTAL_AMOUNT),0)         AS 金額, ");
                sb.AppendFormat("       ROUND (RATIO_TO_REPORT (NVL(SUM (SD.QUANTITY),0)) OVER () * 100, 2)     AS 數量銷貨比率, ");
                sb.AppendFormat("       ROUND (RATIO_TO_REPORT (NVL(SUM (SD.TOTAL_AMOUNT),0)) OVER () * 100, 2) AS 金額銷貨比率  ");
                sb.AppendFormat("  FROM SALE_HEAD SH    ");
                sb.AppendFormat("      ,SALE_DETAIL SD  ");
                sb.AppendFormat("      ,PRODUCT PROD    ");
                sb.AppendFormat("      ,PRODUCT_TYPE PROD_TYPE  ");
                sb.AppendFormat("      ,STORE ST        ");
                sb.AppendFormat("      ,ZONE            ");

                sb.AppendFormat(" WHERE SH.POSUUID_MASTER = SD.POSUUID_MASTER   ");
                sb.AppendFormat("   AND SD.PRODNO = PROD.PRODNO                 ");
                sb.AppendFormat("   AND PROD.PRODTYPENO = PROD_TYPE.PRODTYPENO  ");
                sb.AppendFormat("   AND SH.STORE_NO = ST.STORE_NO               ");
                sb.AppendFormat("   AND ST.ZONE = ZONE.ZONE                     ");
                sb.AppendFormat("   AND SH.SALE_STATUS = '2'                    ");
                #endregion

                #region -WHERE-
                #region 區域別
                if (!string.IsNullOrEmpty(ZONE) && ZONE.ToUpper() != "ALL") sb.AppendFormat(" AND ZONE.ZONE ='{0}' ", ZONE);
                #endregion
                #region 門市編號
                if (!string.IsNullOrEmpty(STORE_NO_S))
                {
                    sb.Append(" AND SH.STORE_NO >= ");
                    sb.Append(Advtek.Utility.OracleDBUtil.SqlStr(STORE_NO_S.Trim()));
                }
                if (!string.IsNullOrEmpty(STORE_NO_E))
                {
                    sb.Append(" AND SH.STORE_NO <= ");
                    sb.Append(Advtek.Utility.OracleDBUtil.SqlStr(STORE_NO_E.Trim()));
                }
                #endregion
                #region 商品類別
                if (!string.IsNullOrEmpty(PRODTYPENO_S))
                {
                    sb.Append(" AND PROD.PRODTYPENO >= ");
                    sb.Append(Advtek.Utility.OracleDBUtil.SqlStr(PRODTYPENO_S.Trim()));
                }
                if (!string.IsNullOrEmpty(PRODTYPENO_E))
                {
                    sb.Append(" AND PROD.PRODTYPENO <= ");
                    sb.Append(Advtek.Utility.OracleDBUtil.SqlStr(PRODTYPENO_E.Trim()));
                }
                #endregion
                #region 產品料號
                if (!string.IsNullOrEmpty(PRODNO_S))
                {
                    sb.Append(" AND SD.PRODNO >= ");
                    sb.Append(Advtek.Utility.OracleDBUtil.SqlStr(PRODNO_S.Trim()));
                }
                if (!string.IsNullOrEmpty(PRODNO_E))
                {
                    sb.Append(" AND SD.PRODNO <= ");
                    sb.Append(Advtek.Utility.OracleDBUtil.SqlStr(PRODNO_E.Trim()));
                }
                #endregion
                #region 交易日期
                if (!string.IsNullOrEmpty(TRADE_DATE_S))
                {
                    sb.Append(" AND TRUNC(SH.TRADE_DATE) >= ");
                    sb.Append(Advtek.Utility.OracleDBUtil.DateStr(TRADE_DATE_S.Trim()));
                }
                if (!string.IsNullOrEmpty(TRADE_DATE_E))
                {
                    sb.Append(" AND TRUNC(SH.TRADE_DATE) <= ");
                    sb.Append(Advtek.Utility.OracleDBUtil.DateStr(TRADE_DATE_E.Trim()));
                }
                #endregion
                #endregion

                #region -GROUP BY-
                sb.AppendFormat(" GROUP BY SH.STORE_NO              ");
                sb.AppendFormat("         ,ST.STORENAME             ");
                sb.AppendFormat("         ,PROD_TYPE.PRODTYPENAME   ");
                sb.AppendFormat("         ,SD.PRODNO                ");
                sb.AppendFormat("         ,PROD.PRODNAME            ");
                #endregion

                #region -ORDER BY-
                if (!string.IsNullOrEmpty(ORDER) && ORDER == "TOTAL_AMOUNT")
                { sb.AppendFormat(" ORDER BY SUM(SD.TOTAL_AMOUNT) DESC "); }
                //{ sb.AppendFormat(" ORDER BY T.{0} DESC ", ORDER); }
                else
                { sb.AppendFormat(" ORDER BY SUM(SD.QUANTITY) DESC "); }
                #endregion


                dt = Advtek.Utility.OracleDBUtil.GetDataSet(objConn, sb.ToString()).Tables[0];
                return dt;
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
        }

        /*Author : 蔡坤霖
          Date : 2011 / 02 / 17
          Description: RPL057 商品迴轉率表
         */
        /// <summary>
        /// RPL057 商品迴轉率表
        /// </summary>
        /// <param name="CATE_NO">商品類別</param>
        /// <param name="PRODNO">商品編號(模糊查詢)</param>
        /// <param name="PRODNAME">商品名稱(模糊查詢)</param>
        /// <param name="DATE_S">日期_起</param>
        /// <param name="DATE_E">日期_訖</param>
        /// <returns></returns>
        public DataTable RPL057(string CATE_NO, string PRODNO, string PRODNAME, string DATE_S, string DATE_E, string STORE_NO)
        {
            OracleConnection objConn = null;
            DataTable dt = null;
            try
            {
                objConn = OracleDBUtil.GetConnection();

                System.Text.StringBuilder sb = new System.Text.StringBuilder();

                #region -SELECT-
                sb.AppendFormat("WITH RPL057 AS ( \n");
                sb.AppendFormat("SELECT B.商品類別 ");
                sb.AppendFormat("      ,B.商品料號 ");
                sb.AppendFormat("      ,B.商品名稱 ");
                sb.AppendFormat("      ,B.銷貨     ");
                sb.AppendFormat("      ,B.期初庫存 ");
                sb.AppendFormat("      ,B.期末庫存 ");
                sb.AppendFormat("      ,B.週轉率   ");
                sb.AppendFormat("      ,B.月週轉天數 ");
                sb.AppendFormat("      ,B.庫存天數 ");
                sb.AppendFormat("      ,(CASE WHEN B.月週轉天數  = 0  THEN '滯銷'   ");
                sb.AppendFormat("             WHEN B.月週轉天數 <= 15 THEN '暢銷'   ");
                sb.AppendFormat("             WHEN B.月週轉天數  > 15 THEN '慢銷'   ");
                sb.AppendFormat("        END )  暢滯銷                              ");
                sb.AppendFormat("  FROM (          ");

                sb.AppendFormat("SELECT A.商品類別 ");
                sb.AppendFormat("      ,A.商品料號 ");
                sb.AppendFormat("      ,A.商品名稱 ");
                sb.AppendFormat("      ,A.銷貨 ");
                sb.AppendFormat("      ,A.期初庫存 ");
                sb.AppendFormat("      ,A.期末庫存 ");
                sb.AppendFormat("      ,ROUND((CASE WHEN ((A.期初庫存 + A.期末庫存 ) / 2 ) = 0 THEN 0       ");
                sb.AppendFormat("                   ELSE (A.銷貨 / ROUND((A.期初庫存 + A.期末庫存)/2,1))    ");
                sb.AppendFormat("              END),1) AS 週轉率                                            ");

                sb.AppendFormat("      ,ROUND((CASE WHEN (A.期末庫存) <= 0 THEN 0                                               ");
                sb.AppendFormat("                   WHEN (ROUND((CASE WHEN ((A.期初庫存 + A.期末庫存 ) / 2 ) = 0 THEN 0         ");
                sb.AppendFormat("                                          ELSE (A.銷貨 / ROUND((A.期初庫存 + A.期末庫存)/2,1)) ");
                sb.AppendFormat("                                     END),1)) <=0 THEN 0                                       ");
                sb.AppendFormat("                   ELSE (31 / ROUND((CASE WHEN ((A.期初庫存 + A.期末庫存 ) / 2 ) = 0 THEN 0    ");
                sb.AppendFormat("                                          ELSE (A.銷貨 / ROUND((A.期初庫存 + A.期末庫存)/2,1)) ");
                sb.AppendFormat("                                     END),1))                                                  ");
                sb.AppendFormat("              END),1) AS 月週轉天數                                                            ");

                sb.AppendFormat("      ,ROUND((CASE WHEN A.期末庫存 <= 0 THEN 0                                                  ");
                sb.AppendFormat("                   WHEN ROUND((A.銷貨 / A.期末庫存),1)  = 0 THEN 0                             ");
                sb.AppendFormat("                   ELSE 31 / (A.銷貨 / A.期末庫存)                                             ");
                sb.AppendFormat("              END),1) AS 庫存天數       ");

                sb.AppendFormat("  FROM (                                     ");
                //sb.AppendFormat("SELECT T.CATE_NAME                商品類別   ");
                //sb.AppendFormat("      ,T.PRODNO                   商品料號   ");
                //sb.AppendFormat("      ,T.PRODNAME                 商品名稱   ");
                //sb.AppendFormat("      ,SUM(T.SALE_QTY)            銷貨       ");
                //sb.AppendFormat("      ,SUM(INV_VW_RPL057(PRODNO, " + OracleDBUtil.SqlStr(DATE_S) + ",'1','" + StringUtil.CStr(STORE_NO) + "')) AS 期初庫存     ");
                //sb.AppendFormat("      ,SUM(INV_VW_RPL057(PRODNO, " + OracleDBUtil.SqlStr(DATE_E) + ",'2','" + StringUtil.CStr(STORE_NO) + "')) AS 期末庫存     ");

                ////sb.AppendFormat("      ,SUM(T.BEGSTK_QTY)          期初庫存   ");
                ////sb.AppendFormat("      ,SUM(T.END_QTY)             期末庫存   ");
                ////sb.AppendFormat("      ,SUM(T.Turnover)            週轉率     ");
                ////sb.AppendFormat("      ,SUM(T.Month_Turnover_Days) 月週轉天數 ");
                ////sb.AppendFormat("      ,SUM(T.Inventory_Days)      庫存天數   ");
                //sb.AppendFormat("  FROM VW_RPL057 T                           ");
                //sb.AppendFormat(" WHERE 1 = 1                                 ");

                #region 第一層SQL
                sb.AppendFormat(" SELECT PT.CATE_NAME            商品類別   ");
                sb.AppendFormat("       ,SDC.PRODNO              商品料號   ");
                sb.AppendFormat("       ,P.PRODNAME              商品名稱   ");
                sb.AppendFormat("       ,SDC.STORE_NO			            ");
                //sb.AppendFormat("       ,SDC.STK_DATE			            ");
                sb.AppendFormat("       ,SDC.LOC_ID  			            ");
                sb.AppendFormat("       ,ABS(SUM(SDC.SALEQTY)) AS 銷貨      ");
                sb.AppendFormat("       ,(SELECT SUM(BEGSTK) FROM STOCK_DAILY_CLOSE S WHERE 1=1 ");
                sb.AppendFormat("            AND S.PRODNO = SDC.PRODNO      ");
                sb.AppendFormat("            AND S.LOC_ID = SDC.LOC_ID      ");
                #region 日期
                if (!string.IsNullOrEmpty(DATE_S))
                {
                    sb.AppendFormat(" AND TRUNC(S.STK_DATE) = ");
                    sb.AppendFormat(Advtek.Utility.OracleDBUtil.DateStr(DATE_S.Trim()));
                }
                #endregion
                #region -門市編號-
                if (!string.IsNullOrEmpty(STORE_NO) && STORE_NO.Length == 4)
                {
                    sb.AppendFormat(" AND S.STORE_NO = ");
                    sb.AppendFormat(Advtek.Utility.OracleDBUtil.SqlStr(STORE_NO.Trim()));
                }
                #endregion
                sb.AppendFormat("         )  AS 期初庫存  ");
                sb.AppendFormat("       ,(SELECT SUM(ENDQTY) FROM STOCK_DAILY_CLOSE S WHERE 1=1 ");
                sb.AppendFormat("            AND S.PRODNO = SDC.PRODNO      ");
                sb.AppendFormat("            AND S.LOC_ID = SDC.LOC_ID      ");
                #region 日期
                if (!string.IsNullOrEmpty(DATE_E))
                {
                    sb.AppendFormat(" AND TRUNC(S.STK_DATE) = ");
                    sb.AppendFormat(Advtek.Utility.OracleDBUtil.DateStr(DATE_E.Trim()));
                }
                #endregion
                #region -門市編號-
                if (!string.IsNullOrEmpty(STORE_NO) && STORE_NO.Length == 4)
                {
                    sb.AppendFormat(" AND S.STORE_NO = ");
                    sb.AppendFormat(Advtek.Utility.OracleDBUtil.SqlStr(STORE_NO.Trim()));
                }
                #endregion
                sb.AppendFormat("         )  AS 期末庫存  ");
                sb.AppendFormat("   FROM STOCK_DAILY_CLOSE SDC ,PRODUCT P ,PROD_MAPPING PM ,PRODUCT_CATEGORY PT ");
                sb.AppendFormat("  WHERE SDC.PRODNO = P.PRODNO                   				                ");
                sb.AppendFormat("    AND P.ERP_ATTRIBUTE_1 = PM.ERP_ATTRIBUTE1   				                ");
                sb.AppendFormat("    AND PM.CATEGORY = PT.CATE_NO 						                        ");
                sb.AppendFormat("    AND SDC.LOC_ID = INV_GoodLOCUUID()						                    ");
                #region -WHERE-
                #region 商品類別
                if (!string.IsNullOrEmpty(CATE_NO) && CATE_NO.ToUpper() != "ALL") sb.AppendFormat(" AND PM.CATEGORY ='{0}' ", CATE_NO);
                #endregion
                #region 商品料號
                if (!string.IsNullOrEmpty(PRODNO))
                {
                    sb.AppendFormat(" AND SDC.PRODNO LIKE ");
                    sb.AppendFormat(Advtek.Utility.OracleDBUtil.LikeStr(PRODNO.Trim()));
                }
                #endregion
                #region 商品名稱
                if (!string.IsNullOrEmpty(PRODNAME))
                {
                    sb.AppendFormat(" AND P.PRODNAME LIKE ");
                    sb.AppendFormat(Advtek.Utility.OracleDBUtil.LikeStr(PRODNAME.Trim()));
                }
                #endregion
                #region 日期
                if (!string.IsNullOrEmpty(DATE_S))
                {
                    sb.AppendFormat(" AND TRUNC(SDC.STK_DATE) >= ");
                    sb.AppendFormat(Advtek.Utility.OracleDBUtil.DateStr(DATE_S.Trim()));
                }
                if (!string.IsNullOrEmpty(DATE_E))
                {
                    sb.AppendFormat(" AND TRUNC(SDC.STK_DATE) <= ");
                    sb.AppendFormat(Advtek.Utility.OracleDBUtil.DateStr(DATE_E.Trim()));
                }
                #endregion
                #region -門市編號-
                if (!string.IsNullOrEmpty(STORE_NO) && STORE_NO.Length == 4)
                {
                    sb.AppendFormat(" AND SDC.STORE_NO = ");
                    sb.AppendFormat(Advtek.Utility.OracleDBUtil.SqlStr(STORE_NO.Trim()));
                }
                #endregion
                #endregion
                //sb.AppendFormat("    AND ROWNUM <=500 ");
                sb.AppendFormat("  GROUP BY PT.CATE_NAME ,SDC.PRODNO ,P.PRODNAME ,SDC.STORE_NO ,SDC.LOC_ID      ");
                #endregion

                sb.AppendFormat(" ) A  ");
                sb.AppendFormat("   WHERE A.期初庫存 > 0 AND A.期末庫存 > 0  ");
                sb.AppendFormat(" ) B  ");

                sb.AppendFormat(" WHERE 1=1            ");
                sb.AppendFormat("   AND rownum <= 500  ");
                #endregion

                //sb.AppendFormat("UNION ALL ");

                sb.AppendFormat("  ) \n");
                sb.AppendFormat("  SELECT * FROM (  ");
                sb.AppendFormat("  SELECT *         ");
                sb.AppendFormat("    FROM RPL057    ");
                sb.AppendFormat("  UNION ALL        ");

                #region 統計部分 - 匯出顯示

                sb.AppendFormat("  SELECT '總計' AS 商品類別    ");
                sb.AppendFormat("        ,''            ");
                sb.AppendFormat("        ,''            ");
                sb.AppendFormat("        ,NVL(SUM(銷貨), 0) 銷貨    ");
                sb.AppendFormat("        ,NVL(SUM(期初庫存), 0) 期初庫存    ");
                sb.AppendFormat("        ,NVL(SUM(期末庫存), 0) 期末庫存    ");
                sb.AppendFormat("        ,NVL(SUM(週轉率), 0)   週轉率      ");
                sb.AppendFormat("        ,NVL(SUM(月週轉天數), 0) 月週轉天數 ");
                sb.AppendFormat("        ,NVL(SUM(庫存天數), 0) 庫存天數    ");
                sb.AppendFormat("        ,''            ");
                sb.AppendFormat("    FROM RPL057    ");
                sb.AppendFormat("   )               ");

                #region -ORDER BY-
                sb.AppendFormat("   ORDER BY 商品類別 ,商品料號     ");
                #endregion


                #endregion



                dt = Advtek.Utility.OracleDBUtil.GetDataSet(objConn, sb.ToString()).Tables[0];
                return dt;
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
        }


        /*Author : 蔡坤霖
          Date : 2011 / 02 / 18
          Description: RPL067 設備賠償明細表
         */
        /// <summary>
        /// RPL067 設備賠償明細表
        /// </summary>
        /// <param name="STORE_NO_S">門市編號</param>
        /// <param name="STORE_NO_E">門市編號</param>
        /// <param name="PRE_S_DATE_S">設備租借日期</param>
        /// <param name="PRE_S_DATE_E">設備租借日期</param>
        /// <param name="INVOICE_DATE_S">發票日期</param>
        /// <param name="INVOICE_DATE_E">發票日期</param>
        /// <param name="LEASE_TYPE_NAME">租借類別</param>
        /// <param name="IND_UNIT_PRICE">處理人員</param>
        /// <param name="EmployeeNo">員工編號</param>
        /// <returns></returns>
        public DataTable RPL067(string STORE_NO_S, string STORE_NO_E,
                                string PRE_S_DATE_S, string PRE_S_DATE_E,
                                string INVOICE_DATE_S, string INVOICE_DATE_E,
                                string LEASE_TYPE_NAME, string IND_UNIT_PRICE, string EmployeeNo)
        {
            OracleConnection objConn = null;
            DataTable dt = null;
            try
            {
                objConn = OracleDBUtil.GetConnection();

                System.Text.StringBuilder sb = new System.Text.StringBuilder();

                #region -SELECT-
                sb.AppendFormat("SELECT STORE_NO             \"門市編號\"  \n");
                sb.AppendFormat("      ,STORE_NAME           \"門市名稱\"  \n");
                sb.AppendFormat("      ,INDEMNIFICATION_NAME \"租借類別\"  \n");
                sb.AppendFormat("      ,PRODTYPENAME         \"商品型號\"  \n");
                sb.AppendFormat("      ,IMEINO               \"IMEI\"      \n");
                sb.AppendFormat("      ,'' AS                \"設備租借日期\"  \n");
                sb.AppendFormat("      ,TO_CHAR(TRADE_DATE, 'YYYY/MM/DD')   \"設備歸還日期\"  \n");
                sb.AppendFormat("      ,TO_CHAR(INVOICE_DATE, 'YYYY/MM/DD') \"發票日期\"  \n");
                sb.AppendFormat("      ,INVOICE_NO           \"發票號碼\"  \n");
                sb.AppendFormat("      ,'' AS                \"賠償項目\"  \n");
                sb.AppendFormat("      ,TOTAL_AMOUNT         \"賠償金\"    \n");
                sb.AppendFormat("      ,T.SALE_PERSON || T.EMPNAME AS \"處理人員\" \n");
                sb.AppendFormat("  FROM VW_RPL067 T                        \n");
                sb.AppendFormat(" WHERE 1 = 1                              \n");
                #endregion

                #region -WHERE-
                #region 門市編號
                if (!string.IsNullOrEmpty(STORE_NO_S))
                {
                    sb.Append(" AND T.STORE_NO >= ");
                    sb.Append(Advtek.Utility.OracleDBUtil.SqlStr(STORE_NO_S.Trim()));
                }
                if (!string.IsNullOrEmpty(STORE_NO_E))
                {
                    sb.Append(" AND T.STORE_NO <= ");
                    sb.Append(Advtek.Utility.OracleDBUtil.SqlStr(STORE_NO_E.Trim()));
                }
                #endregion
                #region 設備租借日期
                //if (!string.IsNullOrEmpty(PRE_S_DATE_S))
                //{
                //    sb.Append(" AND TRUNC(T.PRE_S_DATE) >= ");
                //    sb.Append(Advtek.Utility.OracleDBUtil.DateStr(PRE_S_DATE_S.Trim()));
                //}
                //if (!string.IsNullOrEmpty(PRE_S_DATE_E))
                //{
                //    sb.Append(" AND TRUNC(T.PRE_S_DATE) <= ");
                //    sb.Append(Advtek.Utility.OracleDBUtil.DateStr(PRE_S_DATE_E.Trim()));
                //}
                #endregion
                #region 發票日期
                if (!string.IsNullOrEmpty(INVOICE_DATE_S))
                {
                    sb.Append(" AND TRUNC(T.INVOICE_DATE) >= ");
                    sb.Append(Advtek.Utility.OracleDBUtil.DateStr(INVOICE_DATE_S.Trim()));
                }
                if (!string.IsNullOrEmpty(INVOICE_DATE_E))
                {
                    sb.Append(" AND TRUNC(T.INVOICE_DATE) <= ");
                    sb.Append(Advtek.Utility.OracleDBUtil.DateStr(INVOICE_DATE_E.Trim()));
                }
                #endregion
                #region 租借類別
                if (!string.IsNullOrEmpty(LEASE_TYPE_NAME) && LEASE_TYPE_NAME.ToUpper() != "ALL") sb.AppendFormat(" AND T.INDEMNIFICATION_ID ='{0}' ", LEASE_TYPE_NAME);
                #endregion
                #region 處理人員
                if (!string.IsNullOrEmpty(IND_UNIT_PRICE) && IND_UNIT_PRICE.ToUpper() != "ALL")
                {
                    sb.Append(" AND SALE_PERSON = " + OracleDBUtil.SqlStr(IND_UNIT_PRICE.Trim()));
                }
                #endregion
                #region 員工編號
                if (!string.IsNullOrEmpty(EmployeeNo) && EmployeeNo.ToUpper() != "ALL")
                {
                    sb.Append(" AND SALE_PERSON = " + OracleDBUtil.SqlStr(EmployeeNo.Trim()));
                }
                #endregion
                #endregion

                dt = Advtek.Utility.OracleDBUtil.GetDataSet(objConn, sb.ToString()).Tables[0];
                return dt;
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

        }

        /*Author : 蔡坤霖
          Date : 2011 / 02 / 18
          Description: RPL067 設備賠償明細表
         */
        /// <summary>
        /// RPL067 設備賠償明細表
        /// </summary>
        /// <param name="STORE_NO_S">門市編號</param>
        /// <param name="STORE_NO_E">門市編號</param>
        /// <param name="PRE_S_DATE_S">設備租借日期</param>
        /// <param name="PRE_S_DATE_E">設備租借日期</param>
        /// <param name="INVOICE_DATE_S">發票日期</param>
        /// <param name="INVOICE_DATE_E">發票日期</param>
        /// <param name="LEASE_TYPE_NAME">租借類別</param>
        /// <param name="IND_UNIT_PRICE">處理人員</param>
        /// <param name="EmployeeNo">員工編號</param>
        /// <returns></returns>
        public DataTable RPL067_SUM(string STORE_NO_S, string STORE_NO_E,
                                string PRE_S_DATE_S, string PRE_S_DATE_E,
                                string INVOICE_DATE_S, string INVOICE_DATE_E,
                                string LEASE_TYPE_NAME, string IND_UNIT_PRICE, string EmployeeNo)
        {
            OracleConnection objConn = null;
            DataTable dt = null;
            try
            {
                objConn = OracleDBUtil.GetConnection();

                System.Text.StringBuilder sb = new System.Text.StringBuilder();

                #region -SELECT-
                sb.AppendFormat("WITH RPL067 AS ( \n");
                sb.AppendFormat("SELECT '1'                AS ORDER_INDEX  \n");
                sb.AppendFormat("      ,STORE_NO             \"門市編號\"  \n");
                sb.AppendFormat("      ,STORE_NAME           \"門市名稱\"  \n");
                sb.AppendFormat("      ,INDEMNIFICATION_NAME \"租借類別\"  \n");
                sb.AppendFormat("      ,PRODTYPENAME         \"商品型號\"  \n");
                sb.AppendFormat("      ,IMEINO               \"IMEI\"      \n");
                sb.AppendFormat("      ,'' AS                \"設備租借日期\"  \n");
                sb.AppendFormat("      ,TO_CHAR(TRADE_DATE, 'YYYY/MM/DD')   \"設備歸還日期\"  \n");
                sb.AppendFormat("      ,TO_CHAR(INVOICE_DATE, 'YYYY/MM/DD') \"發票日期\"  \n");
                sb.AppendFormat("      ,INVOICE_NO           \"發票號碼\"  \n");
                sb.AppendFormat("      ,'' AS                \"賠償項目\"  \n");
                sb.AppendFormat("      ,TOTAL_AMOUNT         \"賠償金\"    \n");
                sb.AppendFormat("      ,T.SALE_PERSON || T.EMPNAME AS \"處理人員\" \n");
                sb.AppendFormat("  FROM VW_RPL067 T                        \n");
                sb.AppendFormat(" WHERE 1 = 1                              \n");
                #endregion

                #region -WHERE-
                #region 門市編號
                if (!string.IsNullOrEmpty(STORE_NO_S))
                {
                    sb.Append(" AND T.STORE_NO >= ");
                    sb.Append(Advtek.Utility.OracleDBUtil.SqlStr(STORE_NO_S.Trim()));
                }
                if (!string.IsNullOrEmpty(STORE_NO_E))
                {
                    sb.Append(" AND T.STORE_NO <= ");
                    sb.Append(Advtek.Utility.OracleDBUtil.SqlStr(STORE_NO_E.Trim()));
                }
                #endregion
                #region 設備租借日期
                //if (!string.IsNullOrEmpty(PRE_S_DATE_S))
                //{
                //    sb.Append(" AND TRUNC(T.PRE_S_DATE) >= ");
                //    sb.Append(Advtek.Utility.OracleDBUtil.DateStr(PRE_S_DATE_S.Trim()));
                //}
                //if (!string.IsNullOrEmpty(PRE_S_DATE_E))
                //{
                //    sb.Append(" AND TRUNC(T.PRE_S_DATE) <= ");
                //    sb.Append(Advtek.Utility.OracleDBUtil.DateStr(PRE_S_DATE_E.Trim()));
                //}
                #endregion
                #region 發票日期
                if (!string.IsNullOrEmpty(INVOICE_DATE_S))
                {
                    sb.Append(" AND TRUNC(T.INVOICE_DATE) >= ");
                    sb.Append(Advtek.Utility.OracleDBUtil.DateStr(INVOICE_DATE_S.Trim()));
                }
                if (!string.IsNullOrEmpty(INVOICE_DATE_E))
                {
                    sb.Append(" AND TRUNC(T.INVOICE_DATE) <= ");
                    sb.Append(Advtek.Utility.OracleDBUtil.DateStr(INVOICE_DATE_E.Trim()));
                }
                #endregion
                #region 租借類別
                if (!string.IsNullOrEmpty(LEASE_TYPE_NAME) && LEASE_TYPE_NAME.ToUpper() != "ALL") sb.AppendFormat(" AND T.INDEMNIFICATION_ID ='{0}' ", LEASE_TYPE_NAME);
                #endregion
                #region 處理人員
                if (!string.IsNullOrEmpty(IND_UNIT_PRICE) && IND_UNIT_PRICE.ToUpper() != "ALL")
                {
                    sb.Append(" AND SALE_PERSON = " + OracleDBUtil.SqlStr(IND_UNIT_PRICE.Trim()));
                }
                #endregion
                #region 員工編號
                if (!string.IsNullOrEmpty(EmployeeNo) && EmployeeNo.ToUpper() != "ALL")
                {
                    sb.Append(" AND SALE_PERSON = " + OracleDBUtil.SqlStr(EmployeeNo.Trim()));
                }
                #endregion
                #endregion

                sb.AppendFormat("  ) \n");
                sb.AppendFormat("  SELECT * FROM ( \n");
                sb.AppendFormat("  SELECT * \n");
                sb.AppendFormat("    FROM RPL067 \n");
                sb.AppendFormat("  UNION ALL \n");

                #region 統計部分 - 匯出顯示

                sb.AppendFormat("  SELECT '2' AS ORDER_INDEX  \n");
                sb.AppendFormat("        ,門市編號 || '門市小計' AS 門市編號    \n");
                sb.AppendFormat("        ,''    \n");
                sb.AppendFormat("        ,''    \n");
                sb.AppendFormat("        ,''    \n");
                sb.AppendFormat("        ,''    \n");
                sb.AppendFormat("        ,''    \n");
                sb.AppendFormat("        ,''    \n");
                sb.AppendFormat("        ,''    \n");
                sb.AppendFormat("        ,''    \n");
                sb.AppendFormat("        ,''    \n");
                sb.AppendFormat("        ,NVL(SUM(賠償金), 0) 賠償金    \n");
                sb.AppendFormat("        ,''    \n");
                sb.AppendFormat("    FROM RPL067 \n");
                sb.AppendFormat("   GROUP BY 門市編號   \n");
                sb.AppendFormat("   UNION ALL \n");
                sb.AppendFormat("  SELECT '3' AS ORDER_INDEX  \n");
                sb.AppendFormat("        ,'總計' AS 門市編號 \n");
                sb.AppendFormat("        ,''    \n");
                sb.AppendFormat("        ,''    \n");
                sb.AppendFormat("        ,''    \n");
                sb.AppendFormat("        ,''    \n");
                sb.AppendFormat("        ,''    \n");
                sb.AppendFormat("        ,''    \n");
                sb.AppendFormat("        ,''    \n");
                sb.AppendFormat("        ,''    \n");
                sb.AppendFormat("        ,''    \n");
                sb.AppendFormat("        ,NVL(SUM(賠償金), 0) 賠償金    \n");
                sb.AppendFormat("        ,''    \n");
                sb.AppendFormat("    FROM RPL067 \n");
                sb.AppendFormat("   ) \n");

                #region -ORDER BY-
                sb.AppendFormat("   ORDER BY 門市編號 ,ORDER_INDEX \n");
                #endregion


                #endregion


                dt = Advtek.Utility.OracleDBUtil.GetDataSet(objConn, sb.ToString()).Tables[0];
                return dt;
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

        }


        /*Author : 蔡坤霖
          Date : 2011 / 02 / 18
          Description: RPL068 退保證金明細表
         */
        /// <summary>
        /// RPL068 退保證金明細表
        /// </summary>
        /// <param name="TRADE_DATE_S">退現日期_起</param>
        /// <param name="TRADE_DATE_E">退現日期_迄</param>
        /// <param name="MSISDN">門號</param>
        /// <returns></returns>
        public DataTable RPL068(string TRADE_DATE_S, string TRADE_DATE_E, string MSISDN, string STORENO)
        {
            OracleConnection objConn = null;
            DataTable dt = null;
            try
            {
                objConn = OracleDBUtil.GetConnection();

                System.Text.StringBuilder sb = new System.Text.StringBuilder();

                #region -SELECT-
                sb.AppendFormat("SELECT * FROM (  ");
                sb.AppendFormat("SELECT TO_CHAR(T.TRADE_DATE,'YYYY/MM/DD') AS 退現日期  ");
                //sb.AppendFormat("      ,MSISDNMASK(T.MSISDN) AS 門號        ");
                sb.AppendFormat("      ,T.MSISDN             AS 門號        ");
                sb.AppendFormat("      ,T.BILLING_ACCOUNT_ID AS 客戶帳號    ");
                sb.AppendFormat("      ,T.TOTAL_AMOUNT       AS 保證金金額  ");
                sb.AppendFormat("      ,T.SALE_PERSON        AS 員工編號    ");
                sb.AppendFormat("      ,T.EMPNAME            AS 員工姓名    ");
                sb.AppendFormat("  FROM VW_RPL_RETURN_MARGIN_REPORT T       ");
                sb.AppendFormat(" WHERE 1 = 1 ");
                #endregion

                #region -WHERE-
                #region 門號
                if (!string.IsNullOrEmpty(MSISDN)) sb.AppendFormat(" AND T.MSISDN ='{0}' ", MSISDN);
                #endregion

                #region 退現日期
                if (!string.IsNullOrEmpty(TRADE_DATE_S))
                {
                    sb.Append(" AND TRUNC(T.TRADE_DATE) >= ");
                    sb.Append(Advtek.Utility.OracleDBUtil.DateStr(TRADE_DATE_S.Trim()));
                }
                if (!string.IsNullOrEmpty(TRADE_DATE_E))
                {
                    sb.Append(" AND TRUNC(T.TRADE_DATE) <= ");
                    sb.Append(Advtek.Utility.OracleDBUtil.DateStr(TRADE_DATE_E.Trim()));
                }
                #endregion

                #region 門市編號
                if (!string.IsNullOrEmpty(STORENO) && STORENO.Length == 4) sb.AppendFormat(" AND T.STORE_NO ='{0}' ", STORENO);
                #endregion

                #endregion

                #region -ORDER BY-
                sb.AppendFormat(" ORDER BY TO_CHAR(T.TRADE_DATE,'YYYY/MM/DD') ,T.MSISDN  ");
                #endregion

                sb.AppendFormat(" ) ");
                sb.AppendFormat(" UNION ALL ");

                #region -SELECT-
                sb.AppendFormat("SELECT '總計'              AS 退現日期     ");
                sb.AppendFormat("      ,''                  AS 門號         ");
                sb.AppendFormat("      ,''                  AS 客戶帳號     ");
                sb.AppendFormat("      ,NVL(SUM(T.TOTAL_AMOUNT),0) AS 保證金金額   ");
                sb.AppendFormat("      ,''                  AS 員工編號     ");
                sb.AppendFormat("      ,''                  AS 員工姓名     ");
                sb.AppendFormat("  FROM VW_RPL_RETURN_MARGIN_REPORT T       ");
                sb.AppendFormat(" WHERE 1 = 1 ");
                #endregion

                #region -WHERE-
                #region 門號
                if (!string.IsNullOrEmpty(MSISDN)) sb.AppendFormat(" AND T.MSISDN ='{0}' ", MSISDN);
                #endregion

                #region 退現日期
                if (!string.IsNullOrEmpty(TRADE_DATE_S))
                {
                    sb.Append(" AND TRUNC(T.TRADE_DATE) >= ");
                    sb.Append(Advtek.Utility.OracleDBUtil.DateStr(TRADE_DATE_S.Trim()));
                }
                if (!string.IsNullOrEmpty(TRADE_DATE_E))
                {
                    sb.Append(" AND TRUNC(T.TRADE_DATE) <= ");
                    sb.Append(Advtek.Utility.OracleDBUtil.DateStr(TRADE_DATE_E.Trim()));
                }
                #endregion

                #region 門市編號
                if (!string.IsNullOrEmpty(STORENO) && STORENO.Length == 4) sb.AppendFormat(" AND T.STORE_NO ='{0}' ", STORENO);
                #endregion

                #endregion

                //#region -GROUP BY-
                //sb.AppendFormat(" GROUP BY ");
                //#endregion

                dt = Advtek.Utility.OracleDBUtil.GetDataSet(objConn, sb.ToString()).Tables[0];
                return dt;
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

        }
        #endregion ---  蔡坤霖

        #region  政峰
        /// <summary>
        /// RPL005 信用卡付款入帳明細表
        /// </summary>
        /// <param name="STORE_NO_S">門市編號_起</param>
        /// <param name="STORE_NO_E">門市編號_迄</param>
        /// <param name="TRADE_DATE_S">交易日期_起</param>
        /// <param name="TRADE_DATE_E">交易日期_迄</param>
        /// <param name="SALE_TYPE">服務類型</param>
        /// <param name="SOURCE_TYPE">訂單通路</param>
        /// <returns></returns>
        public DataTable RPL005(string STORE_NO_S, string STORE_NO_E, string TRADE_DATE_S, string TRADE_DATE_E, string SALE_TYPE, string PASSAGEWAY)
        {
            //STORE_NO, STORE_NAME, SALE_NO, INVOICE_NO, SALE_TYPE, FUN_ID, PROMOTION_CODE, INVOICE_AMOUNT, CREDID_CARD_TYPE_NAME, PAID_AMOUNT, '信用卡手續費', PASSAGEWAY_CNAME
            OracleConnection objConn = null;
            DataTable dt = null;
            try
            {
                objConn = OracleDBUtil.GetConnection();

                System.Text.StringBuilder sb = new System.Text.StringBuilder();

                #region -SELECT-
                sb.AppendFormat("SELECT T.STORE_NO               AS 門市編號 ");
                sb.AppendFormat("      ,T.STORE_NAME             AS 門市名稱 ");
                sb.AppendFormat("      ,T.SALE_NO                AS 交易序號 ");
                sb.AppendFormat("      ,T.INVOICE_NO             AS 發票號碼 ");
                sb.AppendFormat("      ,T.SALE_TYPE_NAME         AS 交易類型 ");
                sb.AppendFormat("      ,T.SALE_STATUS_NAME       AS 交易類別 ");
                sb.AppendFormat("      ,TO_CHAR(T.TRADE_DATE,'YYYY/MM/DD')   AS 交易日期 ");
                sb.AppendFormat("      ,T.SERVICE_TYPE_NAME      AS 服務類型 ");
                sb.AppendFormat("      ,T.SOURCE_TYPE_NAME       AS 資料來源 ");
                sb.AppendFormat("      ,T.PROMOTION_CODE         AS 促銷代碼 ");
                sb.AppendFormat("      ,T.INVOICE_AMOUNT         AS 發票金額 ");
                sb.AppendFormat("      ,T.CREDID_CARD_TYPE_NAME  AS 信用卡卡別   ");
                sb.AppendFormat("      ,T.PAID_AMOUNT            AS 刷卡金額     ");
                sb.AppendFormat("      ,T.CREDIT_CARD_FEE        AS 信用卡手續費 ");
                sb.AppendFormat("      ,T.PASSAGEWAY_CNAME       AS 訂單通路     ");
                sb.AppendFormat("      ,T.BEFORE_SALENO          AS 原交易序號   ");
                sb.AppendFormat("  FROM VW_RPL_CRDCRD_PAIDDETL T ");
                sb.AppendFormat(" WHERE 1 = 1 ");
                #endregion

                #region -WHERE-
                #region 門市編號
                if (!string.IsNullOrEmpty(STORE_NO_S))
                {
                    sb.Append(" AND T.STORE_NO >= ");
                    sb.Append(Advtek.Utility.OracleDBUtil.SqlStr(STORE_NO_S.Trim()));
                }
                if (!string.IsNullOrEmpty(STORE_NO_E))
                {
                    sb.Append(" AND T.STORE_NO <= ");
                    sb.Append(Advtek.Utility.OracleDBUtil.SqlStr(STORE_NO_E.Trim()));
                }
                #endregion
                #region 交易日期
                if (!string.IsNullOrEmpty(TRADE_DATE_S))
                {
                    sb.Append(" AND TRUNC(T.TRADE_DATE) >= ");
                    sb.Append(Advtek.Utility.OracleDBUtil.DateStr(TRADE_DATE_S.Trim()));
                }
                if (!string.IsNullOrEmpty(TRADE_DATE_E))
                {
                    sb.Append(" AND TRUNC(T.TRADE_DATE) <= ");
                    sb.Append(Advtek.Utility.OracleDBUtil.DateStr(TRADE_DATE_E.Trim()));
                }
                #endregion
                #region 交易類型
                if (!string.IsNullOrEmpty(SALE_TYPE) && SALE_TYPE != "ALL") sb.AppendFormat(" AND T.SALE_TYPE ='{0}' ", SALE_TYPE);
                #endregion
                #region 訂單通路
                if (!string.IsNullOrEmpty(PASSAGEWAY) && PASSAGEWAY != "ALL") sb.AppendFormat(" AND T.PASSAGEWAY_CNAME ='{0}' ", PASSAGEWAY);
                #endregion
                #endregion

                #region -ORDER BY-
                sb.AppendFormat(" ORDER BY T.STORE_NO ,T.STORE_NAME ,T.SALE_NO ");
                #endregion

                dt = Advtek.Utility.OracleDBUtil.GetDataSet(objConn, sb.ToString()).Tables[0];
                return dt;
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
        }

        /// <summary>
        /// RPL022 廠商結算報表
        /// </summary>
        /// <param name="TRADE_DATE_S">交易日期_起</param>
        /// <param name="TRADE_DATE_E">交易日期_迄</param>
        /// <param name="SUPP_ID">廠商代號</param>
        /// <param name="SUPPNAME">廠商名稱</param>
        /// <param name="PRODNO">料號</param>
        /// <param name="SALE_TYPE">服務類型</param>
        /// <returns></returns>
        public DataTable RPL022(string TRADE_DATE_S, string TRADE_DATE_E, string SUPP_ID, string SUPPNAME, string PRODNO, string SALE_TYPE)
        {
            OracleConnection objConn = null;
            DataTable dt = null;
            try
            {
                objConn = OracleDBUtil.GetConnection();

                System.Text.StringBuilder sb = new System.Text.StringBuilder();

                #region -SELECT-
                sb.AppendFormat("SELECT T.STORE_NO AS 門市編號, ");
                sb.AppendFormat("       T.STORE_NAME AS 門市名稱, ");
                sb.AppendFormat("       TO_CHAR(T.TRADE_DATE,'YYYY/MM/DD') AS 交易日期, ");
                sb.AppendFormat("       T.MACHINE_ID AS 機台編號, ");
                sb.AppendFormat("       T.SALE_NO AS 交易序號, ");
                sb.AppendFormat("       T.INVOICE_NO AS 發票號碼, ");
                sb.AppendFormat("       SALE_TYPE AS 服務類型, ");
                sb.AppendFormat("       T.PRODNO AS 料號, ");
                sb.AppendFormat("       T.PRODNAME AS 商品名稱, ");
                sb.AppendFormat("       T.SALE_QTY AS 數量, ");
                sb.AppendFormat("       T.BEFORE_TAX AS 未稅金額,");
                sb.AppendFormat("       T.TAX AS 稅額, ");
                sb.AppendFormat("       T.TOTAL_AMOUNT AS 應收金額,");
                sb.AppendFormat("       T.PAID_MODE_NAME AS 付款方式,");
                sb.AppendFormat("       T.CREDIT_CARD_FEE AS 信用卡手續費");
                sb.AppendFormat("  FROM VW_RPL_SUPPLIER_CHK_REPORT T ");
                sb.AppendFormat(" WHERE 1 = 1 ");
                #endregion

                #region -WHERE-
                #region 交易日期
                if (!string.IsNullOrEmpty(TRADE_DATE_S))
                {
                    sb.Append(" AND TRUNC(T.TRADE_DATE) >= ");
                    sb.Append(Advtek.Utility.OracleDBUtil.DateStr(TRADE_DATE_S.Trim()));
                }
                if (!string.IsNullOrEmpty(TRADE_DATE_E))
                {
                    sb.Append(" AND TRUNC(T.TRADE_DATE) <= ");
                    sb.Append(Advtek.Utility.OracleDBUtil.DateStr(TRADE_DATE_E.Trim()));
                }
                #endregion
                #region 廠商代號
                if (!string.IsNullOrEmpty(SUPP_ID)) sb.AppendFormat(" AND T.SUPP_ID ='{0}' ", SUPP_ID);
                #endregion
                #region 廠商名稱
                if (!string.IsNullOrEmpty(SUPPNAME)) sb.AppendFormat(" AND T.SUPPNAME ='{0}' ", SUPPNAME);
                #endregion
                #region 料號
                if (!string.IsNullOrEmpty(PRODNO)) sb.AppendFormat(" AND T.PRODNO ='{0}' ", PRODNO);
                #endregion
                #region 服務類型
                if (!string.IsNullOrEmpty(SALE_TYPE) && SALE_TYPE != "ALL") sb.AppendFormat(" AND T.SALE_TYPE ='{0}' ", SALE_TYPE);
                #endregion
                #endregion

                dt = Advtek.Utility.OracleDBUtil.GetDataSet(objConn, sb.ToString()).Tables[0];
                return dt;
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
        }

        /// <summary>
        /// RPL016 交易取消憑證回收狀態表
        /// </summary>
        /// <param name="TRADE_DATE_S">交易取消日期_起</param>
        /// <param name="TRADE_DATE_E">交易取消日期_迄</param>
        /// <param name="STORE_NO_S">門市編號_起</param>
        /// <param name="STORE_NO_E">門市編號_迄</param>
        /// <param name="VOUCHER_TYPE">憑證類型</param>
        /// <param name="INVALID_DATE_S">收回日期_起</param>
        /// <param name="INVALID_DATE_E">收回日期_迄</param>
        /// <returns></returns>
        public DataTable RPL016(string TRADE_DATE_S, string TRADE_DATE_E,
                                string STORE_NO_S, string STORE_NO_E,
                                string VOUCHER_TYPE,
                                string INVALID_DATE_S, string INVALID_DATE_E)
        {
            OracleConnection objConn = null;
            DataTable dt = null;
            try
            {
                objConn = OracleDBUtil.GetConnection();

                System.Text.StringBuilder sb = new System.Text.StringBuilder();

                #region -SELECT-
                sb.AppendFormat("SELECT T.STORE_NO AS 門市編號, ");
                sb.AppendFormat("       T.STORE_NAME AS 門市名稱, ");
                sb.AppendFormat("       TO_CHAR(T.TRADE_DATE,'YYYY/MM/DD') AS 交易取消日期, ");
                sb.AppendFormat("       T.SALE_NO AS 交易序號, ");
                sb.AppendFormat("       T.VOUCHER_TYPE_NAME AS 憑證類型, ");
                sb.AppendFormat("       T.INVOICE_NO AS 原開立發票號碼, ");
                sb.AppendFormat("       T.UNI_NO AS 客戶統編, ");
                sb.AppendFormat("       T.CREDIT_NOTE_NO AS 折讓單編號, ");
                sb.AppendFormat("       T.SALE_AMOUNT  AS 未稅金額, ");
                sb.AppendFormat("       T.TAX AS 稅額, ");
                sb.AppendFormat("       T.TOTAL_AMOUNT AS 含稅金額, ");
                sb.AppendFormat("       TO_CHAR(T.INVALID_DATE,'YYYY/MM/DD') AS 回收日期, ");
                sb.AppendFormat("       T.CREDIT_NOTE_STATUS AS 折讓單申報否, ");
                sb.AppendFormat("       TO_CHAR(T.TRADE_DATE,'YYYY/MM/DD') AS 原交易日期 ");
                sb.AppendFormat("  FROM VW_RPL_TRADEVOU_CANCEL_RCV_DTL T ");
                sb.AppendFormat(" WHERE 1 = 1 ");
                #endregion

                #region -WHERE-
                #region 交易日期
                if (!string.IsNullOrEmpty(TRADE_DATE_S))
                {
                    sb.Append(" AND TRUNC(T.TRADE_DATE) >= ");
                    sb.Append(Advtek.Utility.OracleDBUtil.DateStr(TRADE_DATE_S.Trim()));
                }
                if (!string.IsNullOrEmpty(TRADE_DATE_E))
                {
                    sb.Append(" AND TRUNC(T.TRADE_DATE) <= ");
                    sb.Append(Advtek.Utility.OracleDBUtil.DateStr(TRADE_DATE_E.Trim()));
                }
                #endregion
                #region 門市編號
                if (!string.IsNullOrEmpty(STORE_NO_S))
                {
                    sb.Append(" AND T.STORE_NO >= ");
                    sb.Append(Advtek.Utility.OracleDBUtil.SqlStr(STORE_NO_S.Trim()));
                }
                if (!string.IsNullOrEmpty(STORE_NO_E))
                {
                    sb.Append(" AND T.STORE_NO <= ");
                    sb.Append(Advtek.Utility.OracleDBUtil.SqlStr(STORE_NO_E.Trim()));
                }
                #endregion
                #region 憑證類型
                if (!string.IsNullOrEmpty(VOUCHER_TYPE) && VOUCHER_TYPE != "ALL") sb.AppendFormat(" AND T.VOUCHER_TYPE ='{0}' ", VOUCHER_TYPE);
                #endregion
                #region 回收日期
                if (!string.IsNullOrEmpty(INVALID_DATE_S))
                {
                    sb.Append(" AND TRUNC(T.INVALID_DATE) >= ");
                    sb.Append(Advtek.Utility.OracleDBUtil.DateStr(INVALID_DATE_S.Trim()));
                }
                if (!string.IsNullOrEmpty(INVALID_DATE_E))
                {
                    sb.Append(" AND TRUNC(T.INVALID_DATE) <= ");
                    sb.Append(Advtek.Utility.OracleDBUtil.DateStr(INVALID_DATE_E.Trim()));
                }
                #endregion
                #endregion

                dt = Advtek.Utility.OracleDBUtil.GetDataSet(objConn, sb.ToString()).Tables[0];
                return dt;
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
        }

        /// <summary>
        /// RPL041 POS門市手機銷量表
        /// </summary>
        /// <param name="TRADE_DATE_S">交易日期(起)</param>
        /// <param name="TRADE_DATE_E">交易日期(訖)</param>
        /// <param name="STORE_NO_S">門市編號(起)</param>
        /// <param name="STORE_NO_E">門市編號(訖)</param>
        public DataTable RPL041(string TRADE_DATE_S, string TRADE_DATE_E,
            string STORE_NO_S, string STORE_NO_E)
        {
            OracleConnection objConn = null;
            DataTable dt = null;
            try
            {
                objConn = OracleDBUtil.GetConnection();
                StringBuilder sb = new StringBuilder();
                #region -SELECT-
                sb.Append(" SELECT STORE_NO AS 門市編號, ");
                sb.Append(" STORE_NAME AS 門市名稱, ");
                sb.Append(" PRODNO AS 手機料號,");
                sb.Append(" PRODNAME AS 手機名稱, ");
                sb.Append(" QUANTITY AS 單機銷貨數, ");
                sb.Append(" AVG_PRICE AS 單機均價, ");
                sb.Append(" D_QUANTITY AS 促銷銷貨數, ");
                sb.Append(" D_AVG_PRICE AS 促銷均價 ");
                sb.Append(" FROM VW_RPL_POS_MOBILE ");
                sb.Append(" WHERE 1 = 1");
                #endregion

                #region -WHERE-
                #region 銷售日期
                if (!string.IsNullOrEmpty(TRADE_DATE_S))
                {
                    sb.Append(" AND TRUNC(TRADE_DATE) >= " + OracleDBUtil.DateStr(TRADE_DATE_S.Trim()));
                }
                if (!string.IsNullOrEmpty(TRADE_DATE_E))
                {
                    sb.Append(" AND TRUNC(TRADE_DATE) <= " + OracleDBUtil.DateStr(TRADE_DATE_E.Trim()));
                }
                #endregion
                #region 門市編號
                if (!string.IsNullOrEmpty(STORE_NO_S))
                {
                    sb.Append(" AND STORE_NO >= " + OracleDBUtil.SqlStr(STORE_NO_S.Trim()));
                }
                if (!string.IsNullOrEmpty(STORE_NO_E))
                {
                    sb.Append(" AND STORE_NO <= ");
                    sb.Append(Advtek.Utility.OracleDBUtil.SqlStr(STORE_NO_E.Trim()));
                }
                #endregion
                #endregion

                dt = Advtek.Utility.OracleDBUtil.GetDataSet(objConn, sb.ToString()).Tables[0];
                return dt;
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
        }

        /// <summary>
        /// RPL047 發票作廢明細表
        /// </summary>
        /// <param name="STORE_NO_S">門市編號_起</param>
        /// <param name="STORE_NO_E">門市編號_迄</param>
        /// <param name="CancelDate_S">作廢日期_起</param>
        /// <param name="CancelDate_E">作廢日期_迄</param>
        /// <param name="InvoiceNo_S">發票號碼_起</param>
        /// <param name="InvoiceNo_E">發票號碼_迄</param>
        /// <param name="InvoiceAmount_S">發票金額_起</param>
        /// <param name="InvoiceAmount_E">發票金額_迄</param>
        /// <param name="EmployeeNo_S">員工編號_起</param>
        /// <param name="EmployeeNo_E">員工編號_迄</param>
        /// <returns></returns>
        public DataTable RPL047(string STORE_NO_S, string STORE_NO_E, string CancelDate_S, string CancelDate_E, string InvoiceNo_S, string InvoiceNo_E,
                                string InvoiceAmount_S, string InvoiceAmount_E, string EmployeeNo_S, string EmployeeNo_E, string Search_list)
        {
            OracleConnection objConn = null;
            DataTable dt = null;
            try
            {
                objConn = OracleDBUtil.GetConnection();
                System.Text.StringBuilder sb = new System.Text.StringBuilder();

                #region -SELECT-
                sb.AppendFormat("SELECT T.STORE_NO AS 門市編號, ");
                sb.AppendFormat("       T.STORENAME AS 門市名稱, ");
                sb.AppendFormat("       TO_CHAR(T.INVALID_DATE,'YYYY/MM/DD') AS 作廢日期, ");
                sb.AppendFormat("       T.INVOICE_NO AS 作廢發票號碼, ");
                sb.AppendFormat("       T.CREDIT_NOTE_NO AS 折讓單號, ");
                sb.AppendFormat("       T.MACHINE_ID AS 機台編號, ");
                sb.AppendFormat("       TO_CHAR(T.INVOICE_DATE,'YYYY/MM/DD') AS 發票日期, ");
                sb.AppendFormat("       T.SALE_AMOUNT AS 銷售金額, ");
                sb.AppendFormat("       T.TAX AS 稅額, ");
                sb.AppendFormat("       T.INVOICE_AMOUNT AS 發票金額, ");
                sb.AppendFormat("       T.PAID_MODE AS 付款方式, ");
                sb.AppendFormat("       T.SALE_PERSON_NAME AS 銷售人員, ");
                sb.AppendFormat("       T.SALE_STATUS AS 狀態 ");
                sb.AppendFormat("  FROM VW_RPL_BAD_INVO_DTL T ");
                sb.AppendFormat(" WHERE 1 = 1 ");
                #endregion

                #region -WHERE-
                #region 當月/跨月
                if (!string.IsNullOrEmpty(Search_list) && Search_list != "ALL")
                {
                    if (Search_list == "0")
                    {
                        sb.Append(" AND T.SALE_STATUS in ('退貨作廢','換貨作廢') ");
                    }
                    if (Search_list == "1")
                    {
                        sb.Append(" AND T.SALE_STATUS in ('跨月退貨作廢','跨月換貨作廢') ");
                    }
                }
                #endregion
                #region 門市編號
                if (!string.IsNullOrEmpty(STORE_NO_S))
                {
                    sb.Append(" AND T.STORE_NO >= ");
                    sb.Append(Advtek.Utility.OracleDBUtil.SqlStr(STORE_NO_S.Trim()));
                }
                if (!string.IsNullOrEmpty(STORE_NO_E))
                {
                    sb.Append(" AND T.STORE_NO <= ");
                    sb.Append(Advtek.Utility.OracleDBUtil.SqlStr(STORE_NO_E.Trim()));
                }
                #endregion
                #region 作廢日期
                if (!string.IsNullOrEmpty(CancelDate_S))
                {
                    sb.Append(" AND TRUNC(T.INVALID_DATE) >= ");
                    sb.Append(Advtek.Utility.OracleDBUtil.DateStr(CancelDate_S.Trim()));
                }
                if (!string.IsNullOrEmpty(CancelDate_E))
                {
                    sb.Append(" AND TRUNC(T.INVALID_DATE) <= ");
                    sb.Append(Advtek.Utility.OracleDBUtil.DateStr(CancelDate_E.Trim()));
                }
                #endregion
                #region 發票號碼
                if (!string.IsNullOrEmpty(InvoiceNo_S))
                {
                    sb.Append(" AND T.INVOICE_NO >= ");
                    sb.Append(Advtek.Utility.OracleDBUtil.SqlStr(InvoiceNo_S.Trim()));
                }
                if (!string.IsNullOrEmpty(InvoiceNo_E))
                {
                    sb.Append(" AND T.INVOICE_NO <= ");
                    sb.Append(Advtek.Utility.OracleDBUtil.SqlStr(InvoiceNo_E.Trim()));
                }
                #endregion
                #region 發票金額
                if (!string.IsNullOrEmpty(InvoiceAmount_S))
                {
                    sb.Append(" AND T.INVOICE_AMOUNT >= " + InvoiceAmount_S.Trim());
                }
                if (!string.IsNullOrEmpty(InvoiceAmount_E))
                {
                    sb.Append(" AND T.INVOICE_AMOUNT <= " + InvoiceAmount_E.Trim());
                }
                #endregion
                #region 員工編號
                if (!string.IsNullOrEmpty(EmployeeNo_S))
                {
                    sb.Append(" AND T.SALE_PERSON >= " + OracleDBUtil.SqlStr(EmployeeNo_S.Trim()));
                }
                if (!string.IsNullOrEmpty(EmployeeNo_E))
                {
                    sb.Append(" AND T.SALE_PERSON <= " + OracleDBUtil.SqlStr(EmployeeNo_E.Trim()));
                }
                #endregion
                #endregion

                dt = Advtek.Utility.OracleDBUtil.GetDataSet(objConn, sb.ToString()).Tables[0];
                return dt;
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
        }

        /// <summary>
        /// RPL070 促銷/商品分析表
        /// </summary>
        /// <param name="TRADE_DATE_S">交易日期_起</param>
        /// <param name="TRADE_DATE_E">交易日期_迄</param>
        /// <param name="PROMO_NO">促銷代號</param>
        /// <param name="CATEGORY">商品類別</param>
        /// <param name="PROD_NO">商品料號</param>
        /// <param name="TOTAL_AMOUNT_S">交易金額_迄</param>
        /// <param name="TOTAL_AMOUNT_E">交易金額_起</param>
        /// <param name="SALE_PERSON">銷售人員</param>
        /// <returns></returns>
        public DataTable RPL070(string TRADE_DATE_S, string TRADE_DATE_E, string PROMO_NO, string CATEGORY, string PROD_NO, string TOTAL_AMOUNT_S,
                                string TOTAL_AMOUNT_E, string SALE_PERSON, string STORE_NO)
        {
            OracleConnection objConn = null;
            DataTable dt = null;
            try
            {
                objConn = OracleDBUtil.GetConnection();
                System.Text.StringBuilder sb = new System.Text.StringBuilder();

                #region -SELECT-
                sb.AppendFormat("SELECT T.PROMO_NO AS 促銷代碼, ");
                sb.AppendFormat("       T.PROMO_NAME AS 促銷名稱, ");
                sb.AppendFormat("       T.CATEGORY_NAME AS 商品類別, ");
                sb.AppendFormat("       T.PROD_NO AS 商品料號, ");
                sb.AppendFormat("       T.PRODNAME AS 商品名稱, ");
                sb.AppendFormat("       TO_CHAR(T.TRADE_DATE,'YYYY/MM/DD') AS 交易日期, ");
                sb.AppendFormat("       T.TOTAL_AMOUNT AS 交易金額, ");
                sb.AppendFormat("       T.SALE_PERSON AS 銷售人員 ");
                sb.AppendFormat("  FROM VW_RPL_PROMO_PROD_REPORT T ");
                sb.AppendFormat(" WHERE 1 = 1 ");
                #endregion

                #region -WHERE-
                #region 交易日期
                if (!string.IsNullOrEmpty(TRADE_DATE_S))
                {
                    sb.Append(" AND TRUNC(T.TRADE_DATE) >= ");
                    sb.Append(Advtek.Utility.OracleDBUtil.DateStr(TRADE_DATE_S.Trim()));
                }
                if (!string.IsNullOrEmpty(TRADE_DATE_E))
                {
                    sb.Append(" AND TRUNC(T.TRADE_DATE) <= ");
                    sb.Append(Advtek.Utility.OracleDBUtil.DateStr(TRADE_DATE_E.Trim()));
                }
                #endregion
                #region 促銷代碼
                if (!string.IsNullOrEmpty(PROMO_NO)) sb.AppendFormat(" AND T.PROMO_NO ='{0}' ", PROMO_NO);
                #endregion
                #region 商品類別
                if (!string.IsNullOrEmpty(CATEGORY)) sb.AppendFormat(" AND T.CATEGORY ='{0}' ", CATEGORY);
                #endregion
                #region 商品料號
                if (!string.IsNullOrEmpty(PROD_NO)) sb.AppendFormat(" AND T.PROD_NO ='{0}' ", PROD_NO);
                #endregion
                #region 交易金額
                if (!string.IsNullOrEmpty(TOTAL_AMOUNT_S))
                {
                    sb.Append(" AND T.TOTAL_AMOUNT >= " + OracleDBUtil.SqlStr(TOTAL_AMOUNT_S.Trim()));
                }
                if (!string.IsNullOrEmpty(TOTAL_AMOUNT_E))
                {
                    sb.Append(" AND T.TOTAL_AMOUNT <= " + OracleDBUtil.SqlStr(TOTAL_AMOUNT_E.Trim()));
                }
                #endregion
                #region 銷售人員
                if (!string.IsNullOrEmpty(SALE_PERSON)) sb.AppendFormat(" AND T.SALE_PERSON ='{0}' ", SALE_PERSON);
                #endregion
                #region 登入門市編號
                if (!string.IsNullOrEmpty(STORE_NO) && STORE_NO.Length == 4) sb.AppendFormat(" AND T.STORE_NO ='{0}' ", STORE_NO);
                #endregion
                #endregion

                dt = Advtek.Utility.OracleDBUtil.GetDataSet(objConn, sb.ToString()).Tables[0];
                return dt;
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
        }

        #endregion
        #region 蔡坤霖
        /*Author : 蔡坤霖
          Date : 2011 / 02 / 22
          Description: RPL071 折扣明細表
         */
        /// <summary>
        /// RPL071 折扣明細表
        /// </summary>
        /// <param name="DATE_S">日期區間_起</param>
        /// <param name="DATE_E">日期區間_訖</param>
        /// <param name="PRODNO">商品料號</param>
        /// <param name="DISCOUNT_REASON">折扣原因</param>
        /// <param name="COST_CENTER_NO">成本中心</param>
        /// <param name="EMPLOYEE">員工編號</param>
        /// <returns></returns>
        public DataTable RPL071(string DATE_S, string DATE_E, string PRODNO, string DISCOUNT_REASON, string COST_CENTER_NO, string EMPLOYEE, string STORENO)
        {
            OracleConnection objConn = null;
            DataTable dt = null;
            try
            {
                objConn = OracleDBUtil.GetConnection();

                System.Text.StringBuilder sb = new System.Text.StringBuilder();

                #region -SELECT-
                sb.AppendFormat("WITH RPL071 AS ( \n");
                sb.AppendFormat("SELECT PRODTYPENAME \"商品類別\", \n");
                sb.AppendFormat("       STORE_DIS_REASON_DESC \"折扣原因\", \n");
                sb.AppendFormat("       COST_CENTER_NAME \"成本中心\", \n");
                sb.AppendFormat("       TO_CHAR(DISCOUNT_DATE, 'YYYY/MM/DD') \"折扣日期\", \n");
                sb.AppendFormat("       MACHINE_ID \"機台\", \n");
                sb.AppendFormat("       SALE_NO \"交易序號\", \n");
                sb.AppendFormat("       INVOICE_NO \"發票號碼\", \n");
                sb.AppendFormat("       PRODNO \"商品料號\", \n");
                sb.AppendFormat("       PRODNAME \"商品名稱\", \n");
                sb.AppendFormat("       NVL(QUANTITY,0) \"數量\", \n");
                sb.AppendFormat("       NVL(UNIT_PRICE,0) \"售價\", \n");
                sb.AppendFormat("       NVL(DISCOUNT_MONEY,0) \"折扣金額\", \n");
                sb.AppendFormat("       NVL(DISCOUNT_RATE,0) || '%' \"折扣率\", \n");
                sb.AppendFormat("       SALE_PERSON || EMPNAME \"銷售人員\", \n");
                sb.AppendFormat("       EMPTYPE \"員工類別\", \n");
                sb.AppendFormat("       SH_DISCOUNT_DESC \"折扣說明\" \n");
                sb.AppendFormat("  FROM VW_RPL_DISCOUNT_DETAIL T \n");
                sb.AppendFormat(" WHERE 1 = 1 \n");

                #region -WHERE-
                #region 日期區間
                if (!string.IsNullOrEmpty(DATE_S))
                {
                    sb.Append(" AND TRUNC(T.DISCOUNT_DATE) >= ");
                    sb.Append(Advtek.Utility.OracleDBUtil.DateStr(DATE_S.Trim()));
                }
                if (!string.IsNullOrEmpty(DATE_E))
                {
                    sb.Append(" AND TRUNC(T.DISCOUNT_DATE) <= ");
                    sb.Append(Advtek.Utility.OracleDBUtil.DateStr(DATE_E.Trim()));
                }
                #endregion

                #region 商品料號
                if (!string.IsNullOrEmpty(PRODNO)) sb.AppendFormat(" AND T.PRODNO ='{0}' ", PRODNO);
                #endregion

                #region 折扣原因
                if (!string.IsNullOrEmpty(DISCOUNT_REASON) && DISCOUNT_REASON != "ALL") sb.AppendFormat(" AND T.SH_DISCOUNT_REASON ='{0}' ", DISCOUNT_REASON);
                #endregion

                #region 成本中心
                if (!string.IsNullOrEmpty(COST_CENTER_NO)) sb.AppendFormat(" AND T.COST_CENTER_NO ='{0}' ", COST_CENTER_NO);
                #endregion

                #region 員工編號
                if (!string.IsNullOrEmpty(EMPLOYEE)) sb.AppendFormat(" AND T.SALE_PERSON ='{0}' ", EMPLOYEE);
                #endregion

                #region 門市編號
                if (!string.IsNullOrEmpty(STORENO) && STORENO.Length == 4) sb.AppendFormat(" AND T.STORE_NO ='{0}' ", STORENO);
                #endregion

                #endregion

                #region -ORDER BY-
                sb.AppendFormat(" ORDER BY SALE_NO ,INVOICE_NO ,PRODNO ");
                #endregion

                sb.AppendFormat(" ) \n");

                sb.AppendFormat(" SELECT * \n");
                sb.AppendFormat("   FROM RPL071 \n");
                sb.AppendFormat(" UNION ALL \n");
                sb.AppendFormat(" SELECT NULL, \n");
                sb.AppendFormat("        NULL, \n");
                sb.AppendFormat("        NULL, \n");
                sb.AppendFormat("        NULL, \n");
                sb.AppendFormat("        NULL, \n");
                sb.AppendFormat("        NULL, \n");
                sb.AppendFormat("        NULL, \n");
                sb.AppendFormat("        NULL, \n");
                sb.AppendFormat("        '總計', \n");
                sb.AppendFormat("        NVL(SUM(數量),0), \n");
                //總計之"售價"顯示方式為一種"商品料號"只加總一次。因此與折扣金額一致。
                sb.AppendFormat("        NVL(SUM(折扣金額),0), \n");
                sb.AppendFormat("        NVL(SUM(折扣金額),0), \n");
                sb.AppendFormat("        NVL(SUM(REPLACE(折扣率,'%')),0) || '%', \n");
                sb.AppendFormat("        NULL, \n");
                sb.AppendFormat("        NULL, \n");
                sb.AppendFormat("        NULL \n");
                sb.AppendFormat("   FROM RPL071 \n");
                #endregion

                dt = Advtek.Utility.OracleDBUtil.GetDataSet(objConn, sb.ToString()).Tables[0];
                return dt;
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

        }
        #endregion ---  蔡坤霖
    }
}
