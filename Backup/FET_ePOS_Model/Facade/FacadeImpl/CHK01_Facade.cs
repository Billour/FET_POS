using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.OracleClient;

using Advtek.Utility;

using FET.POS.Model.DTO;

namespace FET.POS.Model.Facade.FacadeImpl
{
    public class CHK01_Facade
    {
        public DataTable Query_VW_CHK01_SAIL_HEAD_DETAIL(string sSTORE_NO)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(@"SELECT  *
                            FROM   VW_CHK01_SAIL_HEAD_DETAIL 
                            WHERE  1=1 
                        ");

            if (!string.IsNullOrEmpty(sSTORE_NO))
            {
                sb.AppendLine(" AND STORE_NO = " + OracleDBUtil.SqlStr(sSTORE_NO));
            }

            DataTable dt = OracleDBUtil.Query_Data(sb.ToString());
            return dt;

        }

        public string Query_TRADEDATE(string strEMPNO)
        {
            string sRet = "";
            if (strEMPNO != null)
            {
                sRet = OracleDBUtil.WorkDay(strEMPNO);
            }
            return sRet;
        }

        public DataTable Query_DAY_CLOSE_DATE(string sSTORE_NO)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(@"SELECT TO_CHAR((TO_DATE(D) + 1),'YYYY/MM/DD') as DAY_CLOSE_DATE
                            FROM  
                            (   SELECT max(DAY_CLOSE_DATE) as  D 
                                FROM   DAY_CLOSE_STATUS 
                                WHERE  CLOSE_STATUS='Y'  
                         ");

            if (!string.IsNullOrEmpty(sSTORE_NO))
            {
                sb.AppendLine(" AND STORE_NO = " + OracleDBUtil.SqlStr(sSTORE_NO));
            }
            sb.AppendLine("   ) T  ");

            DataTable dt = OracleDBUtil.Query_Data(sb.ToString());
            return dt;
        }

        //銷售總額
        public DataTable Query_TOTAL_AMOUNT(string sSTORE_NO, string sDATE)
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine(@"SELECT  DECODE(SUM(PAID_AMOUNT)
                                    , NULL
                                    , 0
                                    , SUM(PAID_AMOUNT)) AS TOTAL_AMOUNT  
                            FROM PAID_DETAIL PD
                            JOIN SALE_HEAD SH ON PD.POSUUID_MASTER=SH.POSUUID_MASTER  
                            WHERE SALE_STATUS in ('2','3','4','5','6') 
                            AND SALE_TYPE='1' AND PAID_MODE <> 9 
                            AND STORE_NO = " + OracleDBUtil.SqlStr(sSTORE_NO) + @"
                            AND TRUNC(TRADE_DATE) = " + OracleDBUtil.DateStr(sDATE)
                          );


            DataTable dt = OracleDBUtil.Query_Data(sb.ToString());
            return dt;
        }

        //銷退總額
        public DataTable Query_TOTAL_AMOUNT_CANCEL(string sSTORE_NO, string sDATE)
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine(@"SELECT  DECODE(SUM(PAID_AMOUNT)
                                    , NULL
                                    , 0
                                    , SUM(PAID_AMOUNT)) AS TOTAL_AMOUNT  
                            FROM PAID_DETAIL PD 
                            JOIN SALE_HEAD SH ON PD.POSUUID_MASTER = SH.POSUUID_MASTER  
                            WHERE SALE_STATUS in ('3','4','5','6') 
                            AND SALE_TYPE='1' AND PAID_MODE <> 9 
                            AND STORE_NO = " + OracleDBUtil.SqlStr(sSTORE_NO) +@"
                            AND TRUNC(INVALID_DATE) = " + OracleDBUtil.DateStr(sDATE)
                            );

            DataTable dt = OracleDBUtil.Query_Data(sb.ToString());
            return dt;
        }

        //代收總額
        public DataTable Query_TOTAL_AMOUNT_2(string sSTORE_NO, string sDATE)
        {
            StringBuilder sb = new StringBuilder();
            //sb.Append(" SELECT  DECODE(SUM(TOTAL_AMOUNT),NULL,0,SUM(TOTAL_AMOUNT)) AS TOTAL_AMOUNT  ");
            //sb.Append(" FROM VW_CHK01_SAIL_HEAD_DETAIL ");
            //sb.Append(" WHERE  STORE_NO = " + OracleDBUtil.SqlStr(sSTORE_NO));
            //sb.Append(" AND TRADE_DATE = " + OracleDBUtil.DateStr(sDATE));
            //sb.Append(" AND SALE_TYPE='2' AND SALE_STATUS='2' ");

            sb.AppendLine(@"SELECT  DECODE(SUM(PAID_AMOUNT)
                                    , NULL
                                    , 0
                                    , SUM(PAID_AMOUNT)) AS TOTAL_AMOUNT 
                            FROM PAID_DETAIL PD 
                            JOIN SALE_HEAD SH ON PD.POSUUID_MASTER=SH.POSUUID_MASTER 
                            WHERE SALE_STATUS in ('2','3','4','5','6')
                            AND SALE_TYPE='2' AND PAID_MODE <> 9 
                            AND STORE_NO = " + OracleDBUtil.SqlStr(sSTORE_NO) + @"
                            AND TRUNC(TRADE_DATE) = " + OracleDBUtil.DateStr(sDATE)
                          );

            DataTable dt = OracleDBUtil.Query_Data(sb.ToString());
            return dt;
        }

        //代收作廢總額
        public DataTable Query_TOTAL_AMOUNT_2_CANCEL(string sSTORE_NO, string sDATE)
        {
            StringBuilder sb = new StringBuilder();
            //sb.Append(" SELECT  DECODE(SUM(TOTAL_AMOUNT),NULL,0,SUM(TOTAL_AMOUNT)) AS TOTAL_AMOUNT  ");
            //sb.Append(" FROM VW_CHK01_SAIL_HEAD_DETAIL ");
            //sb.Append(" WHERE  STORE_NO=" + OracleDBUtil.SqlStr(sSTORE_NO));
            //sb.Append(" AND TRADE_DATE=" + OracleDBUtil.DateStr(sDATE));
            ////sb.Append(" AND SALE_TYPE='2' AND SALE_STATUS='3'   ");
            //sb.Append(" AND SALE_TYPE='2' AND SALE_STATUS in ('3','4','5','6') ");

            sb.AppendLine(@"SELECT  DECODE(SUM(PAID_AMOUNT)
                                    , NULL
                                    , 0
                                    , SUM(PAID_AMOUNT)) AS TOTAL_AMOUNT 
                            FROM PAID_DETAIL PD 
                            JOIN SALE_HEAD SH ON PD.POSUUID_MASTER=SH.POSUUID_MASTER 
                            WHERE SALE_STATUS in ('3','4','5','6')
                            AND SALE_TYPE='2'
                            AND STORE_NO = " + OracleDBUtil.SqlStr(sSTORE_NO) + @"
                            AND TRUNC(TRADE_DATE) = " + OracleDBUtil.DateStr(sDATE)
                          );
            DataTable dt = OracleDBUtil.Query_Data(sb.ToString());
            return dt;
        }

        //快樂購點數
        public DataTable Query_TOTAL_AMOUNT_HG_POINT(string sSTORE_NO, string sDATE)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(@"SELECT  DECODE(SUM(T1.HG_REDEEM_POINT)
                                    , NULL
                                    , 0
                                    , SUM(T1.HG_REDEEM_POINT)) AS TOTAL_AMOUNT  
                            FROM VW_CHK01_SAIL_HEAD_DETAIL T1  LEFT OUTER JOIN PAID_DETAIL T2 ON T1.POSUUID_MASTER=T2.POSUUID_MASTER
                            WHERE  T1.STORE_NO = " + OracleDBUtil.SqlStr(sSTORE_NO) + @"
                            AND TRUNC(T1.TRADE_DATE) = " + OracleDBUtil.DateStr(sDATE) + @"
                            AND T2.PAID_MODE = '7' 
                            AND T1.ITEM_TYPE = '4' 
                        ");

            DataTable dt = OracleDBUtil.Query_Data(sb.ToString());
            return dt;
        }

        ////快樂購金額
        public DataTable Query_TOTAL_AMOUNT_HG_AMOUNT(string sSTORE_NO, string sDATE)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(@"SELECT  DECODE(SUM(T2.PAID_AMOUNT)
                                    , NULL
                                    , 0
                                    , SUM(T2.PAID_AMOUNT)) AS TOTAL_AMOUNT 
                            FROM VW_CHK01_SAIL_HEAD_DETAIL T1  LEFT OUTER JOIN PAID_DETAIL T2 ON T1.POSUUID_MASTER=T2.POSUUID_MASTER 
                            WHERE  T1.STORE_NO = " + OracleDBUtil.SqlStr(sSTORE_NO) + @"
                            AND TRUNC(T1.TRADE_DATE) = " + OracleDBUtil.DateStr(sDATE) + @"
                            AND T2.PAID_MODE = '7'
                            AND T1.ITEM_TYPE = '4' 
                        ");
            DataTable dt = OracleDBUtil.Query_Data(sb.ToString());
            return dt;
        }

        //現金總額:
        public DataTable Query_TOTAL_AMOUNT_CASH_AMOUNT(string sSTORE_NO, string sDATE)
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine(@"SELECT  DECODE(SUM(PAID_AMOUNT)
                                    , NULL
                                    , 0
                                    , SUM(PAID_AMOUNT)) AS TOTAL_AMOUNT  
                            FROM VW_CHK01_SALE_HEAD_PAID_DETAIL
                            WHERE STORE_NO = " + OracleDBUtil.SqlStr(sSTORE_NO) + @"
                            AND TRUNC(TRADE_DATE) = " + OracleDBUtil.DateStr(sDATE) + @"
                            AND PAID_MODE in ('1', '8') 
                            AND SALE_TYPE in ('1','2') AND SALE_STATUS in ('2','3','4','5','6') 
                         ");
    
          
            //sb.Append(" AND POSUUID_MASTER not in (select POSUUID_MASTER from SALE_HEAD ");
            //sb.Append(" WHERE STORE_NO = " + OracleDBUtil.SqlStr(sSTORE_NO));
            //sb.Append(" AND TRADE_DATE = " + OracleDBUtil.DateStr(sDATE));
            //sb.Append(" AND SALE_STATUS in ('3','4','5','6') AND SALE_TYPE in ('1','2')) ");
            DataTable dt = OracleDBUtil.Query_Data(sb.ToString());
            return dt;
        }

        //現金銷退總額:
        public DataTable Query_TOTAL_AMOUNT_CANCLE_CASH_AMOUNT(string sSTORE_NO, string sDATE)
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine(@"SELECT  DECODE(SUM(PAID_AMOUNT)
                                    , NULL
                                    , 0
                                    , SUM(PAID_AMOUNT)) AS TOTAL_AMOUNT 
                            FROM VW_CHK01_SALE_HEAD_PAID_DETAIL 
                            WHERE STORE_NO = " + OracleDBUtil.SqlStr(sSTORE_NO) + @"
                            AND TRUNC(INVALID_DATE) = " + OracleDBUtil.DateStr(sDATE) + @"
                            AND PAID_MODE in ('1', '8') 
                            AND SALE_TYPE in ('1','2') 
                            AND SALE_STATUS IN ('3', '4', '5', '6') 
                          ");


            //sb.Append(" AND POSUUID_MASTER not in (select POSUUID_MASTER from SALE_HEAD ");
            //sb.Append(" WHERE STORE_NO = " + OracleDBUtil.SqlStr(sSTORE_NO));
            //sb.Append(" AND TRADE_DATE = " + OracleDBUtil.DateStr(sDATE));
            //sb.Append(" AND SALE_STATUS in ('3','4','5','6') AND SALE_TYPE in ('1','2')) ");
            DataTable dt = OracleDBUtil.Query_Data(sb.ToString());
            return dt;
        }

        //信用卡額:
        public DataTable Query_TOTAL_AMOUNT_CREDIT_AMOUNT(string sSTORE_NO, string sDATE)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(@"SELECT  DECODE(SUM(PAID_AMOUNT)
                                    , NULL
                                    , 0
                                    , SUM(PAID_AMOUNT)) AS TOTAL_AMOUNT   
                            FROM VW_CHK01_SALE_HEAD_PAID_DETAIL 
                            WHERE STORE_NO = " + OracleDBUtil.SqlStr(sSTORE_NO) + @"
                            AND TRUNC(TRADE_DATE) = " + OracleDBUtil.DateStr(sDATE) + @"
                            AND (PAID_MODE=2 OR PAID_MODE=3) 
                            AND SALE_TYPE in ('1','2') 
                            AND SALE_STATUS in ('2','3','4','5','6')  
                        ");

            DataTable dt = OracleDBUtil.Query_Data(sb.ToString());
            return dt;
        }

        //信用卡額:
        public DataTable Query_TOTAL_AMOUNT_CANCLE_CREDIT_AMOUNT(string sSTORE_NO, string sDATE)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(@"SELECT  DECODE(SUM(PAID_AMOUNT)
                                    , NULL
                                    , 0
                                    , SUM(PAID_AMOUNT)) AS TOTAL_AMOUNT  
                            FROM VW_CHK01_SALE_HEAD_PAID_DETAIL
                            WHERE STORE_NO = " + OracleDBUtil.SqlStr(sSTORE_NO) + @"
                            AND TRUNC(INVALID_DATE) = " + OracleDBUtil.DateStr(sDATE) + @"
                            AND (PAID_MODE='2' OR PAID_MODE='3') 
                            AND SALE_TYPE in ('1','2') 
                            AND SALE_STATUS IN ('3', '4', '5', '6') 
                            ");

            DataTable dt = OracleDBUtil.Query_Data(sb.ToString());
            return dt;
        }

        //分期付款
        public DataTable Query_TOTAL_AMOUNT_TERM_AMOUNT(string sSTORE_NO, string sDATE)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(@"SELECT  DECODE(SUM(PAID_AMOUNT)
                                    , NULL
                                    , 0
                                    , SUM(PAID_AMOUNT)) AS TOTAL_AMOUNT  
                            FROM VW_CHK01_SALE_HEAD_PAID_DETAIL 
                            WHERE STORE_NO = " + OracleDBUtil.SqlStr(sSTORE_NO) + @"
                            AND TRUNC(TRADE_DATE) = " + OracleDBUtil.DateStr(sDATE) + @"
                            AND PAID_MODE='4' 
                            AND SALE_TYPE in ('1','2') 
                            AND SALE_STATUS in ('2','3','4','5','6') 
                        ");

            DataTable dt = OracleDBUtil.Query_Data(sb.ToString());
            return dt;
        }

        //分期付款
        public DataTable Query_TOTAL_AMOUNT_CANCLE_TERM_AMOUNT(string sSTORE_NO, string sDATE)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(@"SELECT  DECODE(SUM(PAID_AMOUNT)
                                    , NULL
                                    , 0
                                    , SUM(PAID_AMOUNT)) AS TOTAL_AMOUNT  
                            FROM VW_CHK01_SALE_HEAD_PAID_DETAIL 
                            WHERE STORE_NO = " + OracleDBUtil.SqlStr(sSTORE_NO) + @"
                            AND TRUNC(INVALID_DATE) = " + OracleDBUtil.DateStr(sDATE) + @"
                            AND PAID_MODE='4' 
                            AND SALE_TYPE in ('1','2')
                            AND SALE_STATUS IN ('3', '4', '5', '6')
                        ");

            DataTable dt = OracleDBUtil.Query_Data(sb.ToString());
            return dt;
        }

        //禮券總額
        public DataTable Query_TOTAL_AMOUNT_VOUCHER_AMOUNT(string sSTORE_NO, string sDATE)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(@"SELECT  DECODE(SUM(PAID_AMOUNT)
                                    , NULL
                                    , 0
                                    , SUM(PAID_AMOUNT)) AS TOTAL_AMOUNT  
                            FROM VW_CHK01_SALE_HEAD_PAID_DETAIL
                            WHERE STORE_NO = " + OracleDBUtil.SqlStr(sSTORE_NO) + @"
                            AND TRUNC(TRADE_DATE) = " + OracleDBUtil.DateStr(sDATE) + @"
                            AND PAID_MODE=5 
                            AND SALE_TYPE in ('1','2') 
                            AND SALE_STATUS NOT IN ('3', '4', '5', '6')
                        ");

            DataTable dt = OracleDBUtil.Query_Data(sb.ToString());
            return dt;
        }

        //金融卡額
        public DataTable Query_TOTAL_AMOUNT_DEBIT_AMOUNT(string sSTORE_NO, string sDATE)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(@"SELECT  DECODE(SUM(PAID_AMOUNT)
                                    , NULL
                                    , 0
                                    , SUM(PAID_AMOUNT)) AS TOTAL_AMOUNT  
                            FROM VW_CHK01_SALE_HEAD_PAID_DETAIL 
                            WHERE STORE_NO = " + OracleDBUtil.SqlStr(sSTORE_NO) + @"
                            AND TRUNC(TRADE_DATE) = " + OracleDBUtil.DateStr(sDATE) + @"
                            AND PAID_MODE=6 
                            AND SALE_TYPE in ('1','2') 
                            AND SALE_STATUS NOT IN ('3', '4', '5', '6')
                        ");

            DataTable dt = OracleDBUtil.Query_Data(sb.ToString());
            return dt;
        }

        public DataTable Query_SAIL_COUNT(string sSTORE_NO, string sDATE)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(@"SELECT  count(*) AS TOTAL_AMOUNT 
                            FROM VW_CHK01_SAIL_HEAD_DETAIL T1  
                            WHERE  T1.STORE_NO = " + OracleDBUtil.SqlStr(sSTORE_NO) + @"
                            AND TRUNC(T1.TRADE_DATE) = " + OracleDBUtil.DateStr(sDATE)
                          );

            DataTable dt = OracleDBUtil.Query_Data(sb.ToString());
            return dt;
        }

        public DataTable Query_SAIL_DATA(string sSTORE_NO, string sDATE)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(@"SELECT  POSUUID_MASTER
                                    , TRADE_DATE  
                            FROM 
                            ( 
                            SELECT  POSUUID_MASTER
                                    , TRADE_DATE  
                            FROM VW_CHK01_SAIL_HEAD_DETAIL T1 
                            WHERE  T1.STORE_NO = " + OracleDBUtil.SqlStr(sSTORE_NO) + @"
                            AND TRUNC(T1.TRADE_DATE) = " + OracleDBUtil.DateStr(sDATE) + @"
                            AND T1.SALE_STATUS in ('2','3','4','5','6') 
                            UNION ALL --作廢
                            SELECT  POSUUID_MASTER
                                    ,INVALID_DATE TRADE_DATE  
                            FROM VW_CHK01_SAIL_HEAD_DETAIL T1 
                            WHERE  T1.STORE_NO = " + OracleDBUtil.SqlStr(sSTORE_NO) + @"
                            AND TRUNC(T1.INVALID_DATE) = " + OracleDBUtil.DateStr(sDATE) + @"
                            AND T1.SALE_STATUS in ('3','4','5','6')           
                            )
                            GROUP BY POSUUID_MASTER,TRADE_DATE
                        ");

            DataTable dt = OracleDBUtil.Query_Data(sb.ToString());
            return dt;
        }

        public DataTable Query_SAIL_MACHINE(string sSTORE_NO, string sDATE)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(@"SELECT  T1.MACHINE_ID1
                                    , T2.MACHINE_ID2
                                    , T2.MACHINE_NAME  
                            FROM 
                            ( 
                              SELECT MACHINE_ID AS MACHINE_ID1 
                              FROM TERM_CLOSE 
                              WHERE STORE_NO=" + OracleDBUtil.SqlStr(sSTORE_NO) + @" 
                              AND TRUNC(DAY_CLOSE_DATE)=" + OracleDBUtil.DateStr(sDATE) + @" 
                              GROUP BY MACHINE_ID
                            ) T1 
                            RIGHT OUTER JOIN
                            (
                              SELECT HOST_NO MACHINE_ID2
                                    ,HOST_NO MACHINE_NAME  
                              FROM STORE_TERMINATING_MACHINE 
                              WHERE STORE_NO=" + OracleDBUtil.SqlStr(sSTORE_NO) + @"
                            ) T2 
                            ON T1.MACHINE_ID1=T2.MACHINE_ID2 
                            ORDER BY MACHINE_NAME
                        ");

            //sb.Append("SELECT T1.MACHINE_ID1,T2.MACHINE_ID2,T2.MACHINE_NAME   FROM (SELECT MACHINE_ID AS MACHINE_ID1 FROM SALE_HEAD WHERE STORE_NO='2101'  ");
            //sb.Append("AND TRADE_DATE=to_date('2011/2/23', 'YYYY/MM/DD')  GROUP BY MACHINE_ID) T1        RIGHT OUTER JOIN       ");
            //sb.Append("(SELECT HOST_NO AS MACHINE_ID2,MACHINE_NAME FROM  STORE_TERMINATING_MACHINE T,STORE_MACHINE M WHERE T.STORE_NO=M.STORE_NO AND T.MACHINE_ID=M.MACHINE_ID) T2      ");
            //sb.Append("ON T1.MACHINE_ID1=T2.MACHINE_ID2 ORDER BY MACHINE_ID1 ");

            DataTable dt = OracleDBUtil.Query_Data(sb.ToString());
            return dt;
        }

        public DataTable Query_VW_CHK01_DAY_CLOSE_STATUS(string sSTORE_NO, string sDATE)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(@"SELECT *  
                            FROM  VW_CHK01_DAY_CLOSE_STATUS 
                            WHERE RTRIM(STORE_NO) = " + OracleDBUtil.SqlStr(sSTORE_NO) + @"
                            AND trunc(DAY_CLOSE_DATE) = " + OracleDBUtil.DateStr(sDATE) + @"
                            AND trunc(DAY_CLOSE_DATE2) = " + OracleDBUtil.DateStr(sDATE)
                          );

            DataTable dt = OracleDBUtil.Query_Data(sb.ToString());
            return dt;
        }

        public void AddNewOne_DayClose(CHK01_DAY_CLOSE_DTO ds)
        {
            OracleConnection objConn = null;
            OracleTransaction objTX = null;

            try
            {
                objConn = OracleDBUtil.GetConnection();
                objTX = objConn.BeginTransaction();

                OracleDBUtil.Insert(objTX, ds.Tables["DAY_CLOSE"]);
                OracleDBUtil.UPDDATEByUUID(objTX, ds.Tables["DAY_CLOSE_STATUS"], "ID");
                if (ds.Tables["STORE_WORKING_DAY"].Rows.Count > 0)
                    OracleDBUtil.UPDDATEByUUID(objTX, ds.Tables["STORE_WORKING_DAY"], "SID");

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

        public void AddNewOne_DayClose_NO_WorkingDay(CHK01_DAY_CLOSE_DTO ds)
        {
            OracleConnection objConn = null;
            OracleTransaction objTX = null;

            try
            {
                objConn = OracleDBUtil.GetConnection();
                objTX = objConn.BeginTransaction();

                OracleDBUtil.Insert(objTX, ds.Tables["DAY_CLOSE"]);
                OracleDBUtil.UPDDATEByUUID(objTX, ds.Tables["DAY_CLOSE_STATUS"], "ID");
                //OracleDBUtil.UPDDATEByUUID(ds.Tables["STORE_WORKING_DAY"], "SID");

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

        public void UpdateOne_DayClose(CHK01_DAY_CLOSE_DTO ds)
        {
            OracleConnection objConn = null;
            OracleTransaction objTX = null;

            try
            {
                objConn = OracleDBUtil.GetConnection();
                objTX = objConn.BeginTransaction();

                OracleDBUtil.UPDDATEByUUID(objTX, ds.Tables["DAY_CLOSE"], "ID");
                OracleDBUtil.UPDDATEByUUID(objTX, ds.Tables["DAY_CLOSE_STATUS"], "ID");
                //OracleDBUtil.UPDDATEByUUID(ds.Tables["STORE_WORKING_DAY"], "SID");


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

        public DataTable Query_STORE_WORKING_DAY_SID(string sSTORE_NO)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(@"SELECT SID 
                            FROM  STORE_WORKING_DAY 
                            WHERE STORE_NO = " + OracleDBUtil.SqlStr(sSTORE_NO)
                         );

            DataTable dt = OracleDBUtil.Query_Data(sb.ToString());
            return dt;
        }

        public DataTable Query_DAY_CLOSE_SATUS_ID(string sSTORE_NO, string sDate)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(@"SELECT ID  
                            FROM  DAY_CLOSE_STATUS  
                            WHERE STORE_NO = " + OracleDBUtil.SqlStr(sSTORE_NO)+ @"
                            AND trunc(DAY_CLOSE_DATE) = " + OracleDBUtil.DateStr(sDate)
                          );

            //sb.Append(" AND TO_CHAR(DAY_CLOSE_DATE, 'yyyy/mm/dd') = " + OracleDBUtil.SqlStr(sDate));

            DataTable dt = OracleDBUtil.Query_Data(sb.ToString());
            return dt;
        }
    }
}
