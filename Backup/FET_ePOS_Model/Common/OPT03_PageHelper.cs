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
    public class OPT03_PageHelper
    {
        /// <summary>
        /// 發卡銀行 
        /// </summary>
        /// <param name="IsQry">是否為查詢資料條件</param>
        /// <returns></returns>
        public static DataTable GetBankNameId(bool IsQry)
        {
            OracleConnection conn = null;
            DataTable dt = null;

            try
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("SELECT BANK_ID, BANK_NAME FROM BANK ORDER  BY BANK_NAME ");

                conn = OracleDBUtil.GetConnection();
                dt = OracleDBUtil.GetDataSet(conn, strSql.ToString()).Tables[0];

                if (IsQry)
                {
                    DataRow dr = dt.NewRow();
                    dr["BANK_NAME"] = "ALL";
                    dr["BANK_ID"] = "";
                    dt.Rows.InsertAt(dr, 0);
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

            return dt;
        }

        /// <summary>
        /// 成本中心 
        /// </summary>
        /// <param name="IsQry">是否為查詢資料條件</param>
        /// <returns></returns>
        public static DataTable GetCostCenterNameNo(bool IsQry)
        {
            OracleConnection conn = null;
            DataTable dt = null;

            try
            {
                StringBuilder strSql = new StringBuilder();
                strSql.AppendLine(@"SELECT COST_CENTER_NAME
                                         , COST_CENTER_NO 
                                    FROM COST_CENTER 
                                    ORDER BY COST_CENTER_NO
                                ");

                conn = OracleDBUtil.GetConnection();
                dt = OracleDBUtil.GetDataSet(conn, strSql.ToString()).Tables[0];

                if (IsQry)
                {
                    DataRow dr = dt.NewRow();
                    dr["COST_CENTER_NAME"] = "ALL";
                    dr["COST_CENTER_NO"] = "ALL";
                    dt.Rows.InsertAt(dr, 0);
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

            return dt;
        }


        /// <summary>
        /// 取得信用卡分期設定主檔特定的一筆資料
        /// </summary>
        /// <param name="InstellmentId">主檔UUID</param>
        /// <returns></returns>
        public static DataRow GetCreditCardInstallmentByInstellmentId(string InstellmentId)
        {
            OracleConnection conn = null;
            DataTable dt = null;

            try
            {
                StringBuilder sb = new StringBuilder();
                sb.AppendLine(@"SELECT * 
                                FROM  CREDIT_CART_INSTELLMENT 
                                WHERE INSTELLMENT_ID = " + OracleDBUtil.SqlStr(InstellmentId)
                         );

                conn = OracleDBUtil.GetConnection();
                dt = OracleDBUtil.GetDataSet(conn, sb.ToString()).Tables[0];

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
        /// 檢查信用卡分期設定主檔，同一家銀行同一分期期數在同一時間區間是否有重複設定
        /// </summary>
        /// <param name="S_DATE">開始日期</param>
        /// <param name="E_DATE">結束日期</param>
        /// <param name="SEQMENT_RATE">分期利率</param>
        /// <param name="BANK_ID">發卡銀行代碼</param>
        /// <param name="INSTELLMENT_ID">主檔UUID</param>
        /// <param name="PAY_SEQMENT">分期期數</param>
        /// <returns>資料筆數(大於0，表示重複)</returns>
        public static int CheckCreditCardInstallmentModifyData(string S_DATE, string E_DATE, string SEQMENT_RATE, string BANK_ID, string INSTELLMENT_ID, string PAY_SEQMENT)
        {
            string eDate = "9999/12/31";
            if (!string.IsNullOrEmpty(E_DATE))
            {
                eDate = E_DATE;
            }
            StringBuilder strSql = new StringBuilder();
            strSql.AppendLine(@"SELECT INSTELLMENT_ID 
                                FROM CREDIT_CART_INSTELLMENT 
                                WHERE 1 = 1 
                                AND BANK_ID = " + OracleDBUtil.SqlStr(BANK_ID) + @"
                                AND PAY_SEQMENT = " + PAY_SEQMENT.Trim() + @"
                                AND (maxdate(TO_CHAR (S_DATE, 'yyyy/mm/dd'), " + OracleDBUtil.SqlStr(S_DATE) + @") 
                                      <= mindate(TO_CHAR (nvl(E_DATE,to_date('9999/12/31','yyyy/MM/dd')), 'yyyy/MM/dd'), " + OracleDBUtil.SqlStr(eDate) + @") )
                                AND INSTELLMENT_ID <> " + OracleDBUtil.SqlStr(INSTELLMENT_ID) 
                             );

            //strSql.Append(" AND SEQMENT_RATE = " + SEQMENT_RATE.Trim());
            DataTable dt = OracleDBUtil.Query_Data(strSql.ToString());

            return dt.Rows.Count;
        }

    }
}

