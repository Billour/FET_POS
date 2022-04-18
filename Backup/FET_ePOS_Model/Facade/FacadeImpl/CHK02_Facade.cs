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
    public class CHK02_Facade
    {
        //**2011/03/09 Tina：註解舊程式，此查詢語法在統計上有錯誤
        /*
        public DataTable Query_SaleMethodSet(string StoreNo, string CashRegisterNo, string TradeDate)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(@" SELECT 
                             SH.STORE_NO    AS STORE_NO 
                            ,SH.STORE_NAME  AS STORE_NAME 
                            ,SH.MACHINE_ID  AS MACHINE_ID
                            ,TO_CHAR(SH.TRADE_DATE,'YYYY/MM/DD') AS TRADE_DATE 
                            ,SUM(SH.SALE_TOTAL_AMOUNT) AS SALE_TOTAL_AMOUNT 
                            ,COUNT(DISTINCT SH.POSUUID_MASTER) AS D_COUNT  
                            ,SUM(DECODE(SH.SALE_TYPE,'1',NVL(SD.TOTAL_AMOUNT,0),0)) AS 銷售總額 
                            ,SUM(DECODE(SH.SALE_TYPE,'1',DECODE(SH.SALE_STATUS,'3',NVL(SD.TOTAL_AMOUNT,0),0) ,0)) AS 銷退總額 
                            ,SUM(DECODE(SH.SALE_TYPE,'1',DECODE(PD.PAID_MODE,'1',NVL(PD.PAID_AMOUNT,0),0) ,0)) AS 現金總額   
                            ,SUM(DECODE(SH.SALE_TYPE,'1',DECODE(PD.PAID_MODE,'2',NVL(PD.PAID_AMOUNT,0),0) ,0)) AS 信用卡額
                            ,SUM(DECODE(SH.SALE_TYPE,'1',DECODE(PD.PAID_MODE,'4',NVL(PD.PAID_AMOUNT,0),0) ,0)) AS 分期付款
                            ,SUM(DECODE(SH.SALE_TYPE,'1',DECODE(PD.PAID_MODE,'5',NVL(PD.PAID_AMOUNT,0),0) ,0)) AS 禮券總額
                            ,SUM(DECODE(SH.SALE_TYPE,'1',DECODE(PD.PAID_MODE,'6',NVL(PD.PAID_AMOUNT,0),0) ,0)) AS 金融卡額
                            ,(SELECT SUM(AMOUNT) FROM BIG_MONEY " + CommonCondition(true, StoreNo, CashRegisterNo, TradeDate, "") + @") AS 繳大鈔             
                            ,SUM(DECODE(SH.SALE_TYPE,'2',NVL(SD.TOTAL_AMOUNT,0),0)) AS 代收總額  
                            ,SUM(DECODE(SH.SALE_TYPE,'2',DECODE(SH.SALE_STATUS,'3',NVL(SD.TOTAL_AMOUNT,0),0) ,0)) AS 代收作廢總額
                            ,SUM(DECODE(SH.SALE_TYPE,'2',DECODE(PD.PAID_MODE,'1',NVL(PD.PAID_AMOUNT,0),0) ,0)) AS 代收現金總額  
                            ,SUM(DECODE(SH.SALE_TYPE,'2',DECODE(PD.PAID_MODE,'2',NVL(PD.PAID_AMOUNT,0),0) ,0)) AS 代收信用卡額
                            ,(SELECT SUM(AMOUNT) FROM SMALL_CHANGE " + CommonCondition(true, StoreNo, CashRegisterNo, TradeDate, "") + @") AS 找零金
                            ,SUM(NVL(SD.HG_REDEEM_POINT,0)) + SUM(NVL(PD.HG_REDEEM_POINT,0)) AS 快樂購點數 
                            ,SUM(DECODE(SD.ITEM_TYPE,'4',NVL(SD.TOTAL_AMOUNT,0),0)) + SUM(DECODE(PD.PAID_MODE,'7',NVL(PD.PAID_AMOUNT,0),0)) AS 快樂購金額  
                            ,(SELECT COUNT(ID) FROM PAID_DETAIL PD, SALE_HEAD SH WHERE PD.POSUUID_MASTER = SH.POSUUID_MASTER AND PD.PAID_MODE IN ('2','3','4') " + CommonCondition(false, StoreNo, CashRegisterNo, TradeDate, "") + @") AS 刷卡張數  
                            ,(SELECT COUNT(ID) FROM INVOICE_HEAD IH, SALE_HEAD SH WHERE IH.POSUUID_MASTER = SH.POSUUID_MASTER AND IH.IS_INVALID = 'Y' " + CommonCondition(false, StoreNo, CashRegisterNo, TradeDate, "SH.") + @") AS 發票作廢張數        
                            ,(SELECT COUNT(POSUUID_MASTER) FROM SALE_HEAD WHERE SALE_HEAD.SALE_TYPE = '2' AND SALE_HEAD.SALE_STATUS = '3' " + CommonCondition(false, StoreNo, CashRegisterNo, TradeDate, "") + @") AS 帳單作廢張數    
                            FROM SALE_HEAD    SH    
                            ,SALE_DETAIL  SD        
                            ,PAID_DETAIL  PD        
                            WHERE SH.POSUUID_MASTER = SD.POSUUID_MASTER  
                              AND SH.POSUUID_MASTER = PD.POSUUID_MASTER            
                        ");

            if (!string.IsNullOrEmpty(StoreNo))
            {
                sb.AppendLine(" AND SH.STORE_NO = " + OracleDBUtil.SqlStr(StoreNo.Trim()));
            }
            if (!string.IsNullOrEmpty(CashRegisterNo))
            {
                sb.AppendLine(" AND SH.MACHINE_ID = " + OracleDBUtil.SqlStr(CashRegisterNo.Trim()));
            }
            if (!string.IsNullOrEmpty(TradeDate))
            {
                sb.AppendLine(" AND TO_CHAR(SH.TRADE_DATE,'YYYY/MM/DD') = " + OracleDBUtil.SqlStr(TradeDate.Trim()));
            }

            sb.AppendLine(@"  GROUP BY SH.STORE_NO, SH.STORE_NAME, SH.MACHINE_ID, TO_CHAR(SH.TRADE_DATE,'YYYY/MM/DD') 
                              ORDER BY SH.STORE_NO 
                          ");

            DataTable dt = OracleDBUtil.Query_Data(sb.ToString());
            return dt;
        }
        */

        //**2011/03/09 Tina：add Method，此查詢語法為解決舊程式的問題
        //取得所有明細資料
        public DataSet Query_SaleMethodSet(string StoreNo, string CashRegisterNo, string TradeDate)
        {
            OracleConnection objConn = null;
            DataSet ds = new DataSet();

            DataTable dt = new DataTable();
            string strSALE_STATUS = " AND SH.SALE_STATUS in ('2','3','4','5','6') ";     
            //銷售條件
            StringBuilder sbWhere = new StringBuilder();
            if (!string.IsNullOrEmpty(StoreNo))
            {
                sbWhere.AppendLine(" AND SH.STORE_NO = " + OracleDBUtil.SqlStr(StoreNo.Trim()));
            }
            if (!string.IsNullOrEmpty(CashRegisterNo))
            {
                sbWhere.AppendLine(" AND SH.MACHINE_ID = " + OracleDBUtil.SqlStr(CashRegisterNo.Trim()));
            }
            if (!string.IsNullOrEmpty(TradeDate))
            {
                sbWhere.AppendLine(" AND TO_CHAR(SH.TRADE_DATE,'YYYY/MM/DD') = " + OracleDBUtil.SqlStr(TradeDate.Trim()));
            }
            //  sbWhere.AppendLine(strSALE_STATUS);

            //銷退條件
            StringBuilder sbInvWhere = new StringBuilder();
            if (!string.IsNullOrEmpty(StoreNo))
            {
                sbInvWhere.AppendLine(" AND SH.STORE_NO = " + OracleDBUtil.SqlStr(StoreNo.Trim()));
            }
            if (!string.IsNullOrEmpty(CashRegisterNo))
            {
                sbInvWhere.AppendLine(" AND SH.MACHINE_ID = " + OracleDBUtil.SqlStr(CashRegisterNo.Trim()));
            }
            if (!string.IsNullOrEmpty(TradeDate))
            {
                sbInvWhere.AppendLine(" AND TO_CHAR(SH.INVALID_DATE,'YYYY/MM/DD') = " + OracleDBUtil.SqlStr(TradeDate.Trim()));
            }


            try
            {
                objConn = OracleDBUtil.GetConnection();

                //主體資料
                StringBuilder sb = new StringBuilder();
                dt = new DataTable("MasterData");
                sb.AppendLine(@"SELECT    STORE_NO 
                                        , STORE_NAME
                                        , MACHINE_ID 
                                        , TRADE_DATE
                                        , COUNT(POSUUID_MASTER) D_COUNT 
                               From
                               (SELECT   SH.STORE_NO 
                                        , SH.STORE_NAME
                                        , SH.MACHINE_ID 
                                        , TO_CHAR(SH.TRADE_DATE,'YYYY/MM/DD') AS TRADE_DATE
                                        , SH.POSUUID_MASTER                                        
                                FROM SALE_HEAD SH  
                                WHERE 1 = 1 " + sbWhere + strSALE_STATUS + @"
                                UNION ALL
                                SELECT   SH.STORE_NO 
                                        , SH.STORE_NAME
                                        , SH.MACHINE_ID 
                                        , TO_CHAR(SH.INVALID_DATE,'YYYY/MM/DD') AS TRADE_DATE
                                        , SH.POSUUID_MASTER                                        
                                FROM SALE_HEAD SH  
                                WHERE 1 = 1 " + sbInvWhere + @"
                                AND SH.SALE_STATUS in ('3','4','5','6')
                                )
                                GROUP BY STORE_NO, STORE_NAME, MACHINE_ID, TRADE_DATE 
                            ");
                dt.Load(OracleDBUtil.GetDataReader(objConn, sb.ToString()));
                if (dt.Rows.Count > 0)
                {
                    ds.Tables.Add(dt);
                }

                //其它資訊
                sb = new StringBuilder();
                dt = new DataTable("OtherData");
                sb.AppendLine(@"SELECT (SELECT SUM(AMOUNT) FROM BIG_MONEY " + CommonCondition(true, StoreNo, CashRegisterNo, TradeDate, "") + @") AS 繳大鈔
                                , (SELECT SUM(AMOUNT) FROM SMALL_CHANGE " + CommonCondition(true, StoreNo, CashRegisterNo, TradeDate, "") + @") AS 找零金
                                , (SELECT COUNT(ID) FROM PAID_DETAIL PD, SALE_HEAD SH WHERE PD.POSUUID_MASTER = SH.POSUUID_MASTER AND PD.PAID_MODE IN ('2','3','4') " + CommonCondition(false, StoreNo, CashRegisterNo, TradeDate, "") + @") AS 刷卡張數  
                                , (SELECT COUNT(ID) FROM INVOICE_HEAD IH, SALE_HEAD SH WHERE IH.POSUUID_MASTER = SH.POSUUID_MASTER AND IH.IS_INVALID = 'Y' AND TO_CHAR(SH.INVALID_DATE,'YYYY/MM/DD') = " + OracleDBUtil.SqlStr(TradeDate.Trim()) + CommonCondition(false, StoreNo, CashRegisterNo, "", "SH.") + @") AS 發票作廢張數        
                                , (SELECT COUNT(POSUUID_MASTER) FROM SALE_HEAD SH WHERE SH.SALE_TYPE = '2' AND TO_CHAR(INVALID_DATE,'YYYY/MM/DD') = " + OracleDBUtil.SqlStr(TradeDate.Trim()) + CommonCondition(false, StoreNo, CashRegisterNo, "", "") + @") AS 帳單作廢張數    
                                FROM DUAL
                            ");
                dt.Load(OracleDBUtil.GetDataReader(objConn, sb.ToString()));
                if (dt.Rows.Count > 0)
                {
                    ds.Tables.Add(dt);
                }

                //銷售總額
                sb = new StringBuilder();
                dt = new DataTable("TOTAL_AMOUNT");
                sb.AppendLine(@"SELECT  DECODE(SUM(PAID_AMOUNT)
                                        , NULL
                                        , 0
                                        , SUM(PAID_AMOUNT)) AS 銷售總額 
                                FROM PAID_DETAIL PD 
                                JOIN SALE_HEAD SH ON PD.POSUUID_MASTER = SH.POSUUID_MASTER 
                                WHERE SALE_STATUS in ('2','3','4','5','6') 
                                AND SALE_TYPE='1' 
                                AND PD.PAID_MODE <> 9 
                                " + sbWhere
                              );
                dt.Load(OracleDBUtil.GetDataReader(objConn, sb.ToString()));
                if (dt.Rows.Count > 0)
                {
                    ds.Tables.Add(dt);
                }

                //銷退總額
                sb = new StringBuilder();
                dt = new DataTable("TOTAL_AMOUNT_CANCEL");
                sb.AppendLine(@"SELECT  DECODE(SUM(PAID_AMOUNT)
                                        , NULL
                                        , 0
                                        , SUM(PAID_AMOUNT)) AS 銷退總額  
                                FROM PAID_DETAIL PD 
                                JOIN SALE_HEAD SH ON PD.POSUUID_MASTER = SH.POSUUID_MASTER  
                                WHERE SALE_STATUS in ('3','4','5','6')
                                AND SALE_TYPE='1' 
                                AND PD.PAID_MODE <> 9 
                                " + sbInvWhere
                              );

                dt.Load(OracleDBUtil.GetDataReader(objConn, sb.ToString()));
                if (dt.Rows.Count > 0)
                {
                    ds.Tables.Add(dt);
                }

                //代收總額
                sb = new StringBuilder();
                dt = new DataTable("TOTAL_AMOUNT_2");
                sb.AppendLine(@"SELECT  DECODE(SUM(PAID_AMOUNT)
                                        , NULL
                                        , 0
                                        , SUM(PAID_AMOUNT)) AS 代收總額 
                                FROM PAID_DETAIL PD 
                                JOIN SALE_HEAD SH ON PD.POSUUID_MASTER=SH.POSUUID_MASTER 
                                WHERE SALE_STATUS='2' 
                                AND SALE_TYPE='2' AND PD.PAID_MODE <> 9 
                                " + sbWhere
                              );
                dt.Load(OracleDBUtil.GetDataReader(objConn, sb.ToString()));
                if (dt.Rows.Count > 0)
                {
                    ds.Tables.Add(dt);
                }

                //代收作廢總額
                sb = new StringBuilder();
                dt = new DataTable("TOTAL_AMOUNT_2_CANCEL");
                sb.AppendLine(@"SELECT  DECODE(SUM(PAID_AMOUNT)
                                        , NULL
                                        , 0
                                        , SUM(PAID_AMOUNT)) AS 代收作廢總額  
                                FROM PAID_DETAIL PD 
                                JOIN SALE_HEAD SH ON PD.POSUUID_MASTER=SH.POSUUID_MASTER  
                                WHERE SALE_STATUS in ('3','4','5','6') 
                                AND SALE_TYPE='2' 
                                AND PD.PAID_MODE <> 9 
                                " + sbInvWhere
                              );
                dt.Load(OracleDBUtil.GetDataReader(objConn, sb.ToString()));
                if (dt.Rows.Count > 0)
                {
                    ds.Tables.Add(dt);
                }

                //統計1
                sb = new StringBuilder();
                dt = new DataTable("Statistics1");
                //                sb.AppendLine(@"SELECT SUM(DECODE(SH.SALE_TYPE,'1',NVL(SD.TOTAL_AMOUNT,0),0)) AS 銷售總額 
                //                                            ,SUM(DECODE(SH.SALE_TYPE,'1',DECODE(SH.SALE_STATUS,'3',NVL(SD.TOTAL_AMOUNT,0),0) ,0)) AS 銷退總額           
                //                                            ,SUM(DECODE(SH.SALE_TYPE,'2',NVL(SD.TOTAL_AMOUNT,0),0)) AS 代收總額  
                //                                            ,SUM(DECODE(SH.SALE_TYPE,'2',DECODE(SH.SALE_STATUS,'3',NVL(SD.TOTAL_AMOUNT,0),0) ,0)) AS 代收作廢總額
                //                                            ,SUM(NVL(SD.HG_REDEEM_POINT,0)) AS 快樂購點數的被加數
                //                                            ,SUM(DECODE(SD.ITEM_TYPE,'4',NVL(SD.TOTAL_AMOUNT,0),0))  AS 快樂購金額的被加數
                //                                FROM SALE_HEAD  SH ,SALE_DETAIL  SD             
                //                                WHERE SH.POSUUID_MASTER = SD.POSUUID_MASTER      
                //                                     " + sbWhere + strSALE_STATUS + @"
                //                                GROUP BY SH.STORE_NO, SH.STORE_NAME, SH.MACHINE_ID, TO_CHAR(SH.TRADE_DATE,'YYYY/MM/DD')                  
                //                            ");
                sb.AppendLine(@"SELECT       SUM(NVL(SD.HG_REDEEM_POINT,0)) AS 快樂購點數的被加數
                                            ,SUM(DECODE(SD.ITEM_TYPE,'4',NVL(SD.TOTAL_AMOUNT,0),0))  AS 快樂購金額的被加數
                                FROM SALE_HEAD  SH ,SALE_DETAIL  SD             
                                WHERE SH.POSUUID_MASTER = SD.POSUUID_MASTER      
                                     " + sbWhere + strSALE_STATUS + @"
                                GROUP BY SH.STORE_NO, SH.STORE_NAME, SH.MACHINE_ID, TO_CHAR(SH.TRADE_DATE,'YYYY/MM/DD')                  
                            ");
                dt.Load(OracleDBUtil.GetDataReader(objConn, sb.ToString()));
                if (dt.Rows.Count > 0)
                {
                    ds.Tables.Add(dt);
                }

                //現金總額
                sb = new StringBuilder();
                dt = new DataTable("TOTAL_AMOUNT_CASH_AMOUNT");
                //sb.Append(" SELECT SUM(TOTAL_AMOUNT) AS 現金總額 From (");
                //sb.Append(" SELECT  DECODE(SUM(PAID_AMOUNT),NULL,0,SUM(PAID_AMOUNT)) AS TOTAL_AMOUNT  ");
                //sb.Append(" FROM PAID_DETAIL PD ");
                //sb.Append(" JOIN SALE_HEAD SH ON PD.POSUUID_MASTER=SH.POSUUID_MASTER  ");
                //sb.Append(" WHERE 1=1 ");
                //sb.Append(" AND PAID_MODE in ('1','8') AND SALE_TYPE in ('1','2') AND SALE_STATUS NOT IN ('3', '4', '5', '6') ");
                //sb.Append(sbWhere);
                //sb.Append(" UNION ALL ");
                //sb.Append(" SELECT  (DECODE(SUM(AMOUNT),NULL,0,SUM(AMOUNT)) * -1) AS TOTAL_AMOUNT   ");
                //sb.Append(" FROM SMALL_CHANGE " + CommonCondition(true, StoreNo, CashRegisterNo, TradeDate, "") + @" ");
                //sb.Append(" ) T1");

                sb.AppendLine(@"SELECT  DECODE(SUM(PAID_AMOUNT)
                                        , NULL
                                        , 0
                                        , SUM(PAID_AMOUNT)) AS 現金總額  
                                FROM PAID_DETAIL PD 
                                JOIN SALE_HEAD SH ON PD.POSUUID_MASTER=SH.POSUUID_MASTER  
                                WHERE 1=1 
                                AND PAID_MODE in ('1','8') AND SALE_TYPE = '1' AND SALE_STATUS NOT IN ('1','7','8','9')  
                                " + sbWhere
                              );

                dt.Load(OracleDBUtil.GetDataReader(objConn, sb.ToString()));
                if (dt.Rows.Count > 0)
                {
                    ds.Tables.Add(dt);
                }

                //現金銷退
                sb = new StringBuilder();
                dt = new DataTable("TOTAL_AMOUNT_CANCLE_CASH_AMOUNT");
                sb.AppendLine(@"SELECT  DECODE(SUM(PAID_AMOUNT)
                                        , NULL
                                        , 0
                                        , SUM(PAID_AMOUNT)) AS 現金銷退 
                                FROM PAID_DETAIL PD
                                JOIN SALE_HEAD SH ON PD.POSUUID_MASTER=SH.POSUUID_MASTER  
                                WHERE 1=1 
                                AND PAID_MODE in ('1', '8') AND SALE_TYPE = '1' AND SALE_STATUS IN ('3', '4', '5', '6') 
                                " + sbInvWhere
                              );

                dt.Load(OracleDBUtil.GetDataReader(objConn, sb.ToString()));
                if (dt.Rows.Count > 0)
                {
                    ds.Tables.Add(dt);
                }


                //信用卡額
                sb = new StringBuilder();
                dt = new DataTable("TOTAL_AMOUNT_CREDIT_AMOUNT");
                sb.AppendLine(@"SELECT  DECODE(SUM(PAID_AMOUNT)
                                        , NULL
                                        , 0
                                        , SUM(PAID_AMOUNT)) AS 信用卡額  
                            FROM PAID_DETAIL PD 
                            JOIN SALE_HEAD SH ON PD.POSUUID_MASTER=SH.POSUUID_MASTER  
                            WHERE 1=1 
                            AND (PAID_MODE=2 OR PAID_MODE=3) 
                            AND SALE_TYPE = '1' 
                            AND SALE_STATUS NOT IN ('1','7','8','9') 
                            ");
                sb.Append(sbWhere);
                dt.Load(OracleDBUtil.GetDataReader(objConn, sb.ToString()));
                if (dt.Rows.Count > 0)
                {
                    ds.Tables.Add(dt);
                }

                //信用卡銷退
                sb = new StringBuilder();
                dt = new DataTable("TOTAL_AMOUNT_CREDIT_CANCLE_AMOUNT");
                sb.AppendLine(@"SELECT  DECODE(SUM(PAID_AMOUNT)
                                        , NULL
                                        , 0
                                        , SUM(PAID_AMOUNT)) AS 信用卡銷退 
                            FROM PAID_DETAIL PD 
                            JOIN SALE_HEAD SH ON PD.POSUUID_MASTER=SH.POSUUID_MASTER 
                            WHERE 1=1 
                            AND (PAID_MODE=2 OR PAID_MODE=3) 
                            AND SALE_TYPE = '1' 
                            AND SALE_STATUS  IN ('3', '4', '5', '6') 
                            ");
                sb.Append(sbInvWhere);
                dt.Load(OracleDBUtil.GetDataReader(objConn, sb.ToString()));
                if (dt.Rows.Count > 0)
                {
                    ds.Tables.Add(dt);
                }

                //分期付款
                sb = new StringBuilder();
                dt = new DataTable("TOTAL_AMOUNT_TERM_AMOUNT");
                sb.AppendLine(@"SELECT  DECODE(SUM(PAID_AMOUNT)
                                        , NULL
                                        , 0
                                        , SUM(PAID_AMOUNT)) AS 分期付款 
                            FROM PAID_DETAIL PD 
                            JOIN SALE_HEAD SH ON PD.POSUUID_MASTER=SH.POSUUID_MASTER  
                            WHERE 1=1 
                            AND PAID_MODE=4 
                            AND SALE_TYPE ='1'  
                            AND SALE_STATUS NOT IN ('1','7','8','9') 
                            ");
                sb.Append(sbWhere);
                dt.Load(OracleDBUtil.GetDataReader(objConn, sb.ToString()));
                if (dt.Rows.Count > 0)
                {
                    ds.Tables.Add(dt);
                }

                //分期付款銷退
                sb = new StringBuilder();
                dt = new DataTable("TOTAL_AMOUNT_TERM_CANCLE_AMOUNT");
                sb.AppendLine(@"SELECT  DECODE(SUM(PAID_AMOUNT)
                                        , NULL
                                        , 0
                                        , SUM(PAID_AMOUNT)) AS 分期付款銷退 
                            FROM PAID_DETAIL PD 
                            JOIN SALE_HEAD SH ON PD.POSUUID_MASTER=SH.POSUUID_MASTER  
                            WHERE 1=1 
                            AND PAID_MODE=4 
                            AND SALE_TYPE ='1' 
                            AND SALE_STATUS IN ('3', '4', '5', '6') 
                            ");
                sb.Append(sbInvWhere);
                dt.Load(OracleDBUtil.GetDataReader(objConn, sb.ToString()));
                if (dt.Rows.Count > 0)
                {
                    ds.Tables.Add(dt);
                }


                //禮券總額
                sb = new StringBuilder();
                dt = new DataTable("TOTAL_AMOUNT_VOUCHER_AMOUNT");
                sb.AppendLine(@"SELECT  DECODE(SUM(PAID_AMOUNT)
                                        , NULL
                                        , 0
                                        , SUM(PAID_AMOUNT)) AS 禮券總額  
                            FROM PAID_DETAIL PD 
                            JOIN SALE_HEAD SH ON PD.POSUUID_MASTER=SH.POSUUID_MASTER  
                            WHERE 1=1 
                            AND PAID_MODE=5 
                            AND SALE_TYPE in ('1','2') 
                            AND SALE_STATUS NOT IN ('3', '4', '5', '6') 
                            ");
                sb.Append(sbWhere);
                dt.Load(OracleDBUtil.GetDataReader(objConn, sb.ToString()));
                if (dt.Rows.Count > 0)
                {
                    ds.Tables.Add(dt);
                }

                //金融卡額
                sb = new StringBuilder();
                dt = new DataTable("TOTAL_AMOUNT_DEBIT_AMOUNT");
                sb.AppendLine(@"SELECT  DECODE(SUM(PAID_AMOUNT)
                                        , NULL
                                        , 0
                                        , SUM(PAID_AMOUNT)) AS 金融卡額  
                            FROM PAID_DETAIL PD
                            JOIN SALE_HEAD SH ON PD.POSUUID_MASTER=SH.POSUUID_MASTER  
                            WHERE 1=1 
                            AND PAID_MODE=6 
                            AND SALE_TYPE in ('1','2') 
                            AND SALE_STATUS NOT IN ('3', '4', '5', '6') 
                            ");
                sb.Append(sbWhere);
                dt.Load(OracleDBUtil.GetDataReader(objConn, sb.ToString()));
                if (dt.Rows.Count > 0)
                {
                    ds.Tables.Add(dt);
                }

                //代收現金總額
                sb = new StringBuilder();
                dt = new DataTable("RE_TOTAL_AMOUNT");
                sb.AppendLine(@"SELECT  DECODE(SUM(PAID_AMOUNT)
                                        , NULL
                                        , 0
                                        , SUM(PAID_AMOUNT)) AS 代收現金總額  
                                FROM PAID_DETAIL PD 
                                JOIN SALE_HEAD SH ON PD.POSUUID_MASTER=SH.POSUUID_MASTER 
                                WHERE 1=1 
                                AND PAID_MODE in ('1','8') 
                                AND SALE_TYPE = '2' 
                                AND SALE_STATUS NOT IN ('1','7','8','9') 
                                " + sbWhere
                              );
                dt.Load(OracleDBUtil.GetDataReader(objConn, sb.ToString()));
                if (dt.Rows.Count > 0)
                {
                    ds.Tables.Add(dt);
                }

                //代收信用卡額
                sb = new StringBuilder();
                dt = new DataTable("RE_CARD_TOTAL_AMOUNT");
                sb.AppendLine(@"SELECT  DECODE(SUM(PAID_AMOUNT)
                                        , NULL
                                        , 0
                                        , SUM(PAID_AMOUNT)) AS 代收信用卡額
                                FROM PAID_DETAIL PD 
                                JOIN SALE_HEAD SH ON PD.POSUUID_MASTER=SH.POSUUID_MASTER 
                                WHERE 1=1 
                                AND PAID_MODE in (2,3) 
                                AND SALE_TYPE = '2' 
                                AND SALE_STATUS NOT IN ('1','7','8','9')
                                " + sbWhere
                              );
                dt.Load(OracleDBUtil.GetDataReader(objConn, sb.ToString()));
                if (dt.Rows.Count > 0)
                {
                    ds.Tables.Add(dt);
                }

                //統計2
                sb = new StringBuilder();
                dt = new DataTable("Statistics2");
                //                sb.AppendLine(@"SELECT 
                //                                     SUM(DECODE(SH.SALE_TYPE,'1',CASE WHEN PD.PAID_MODE = '1' OR PD.PAID_MODE = '8' THEN NVL(PD.PAID_AMOUNT,0) ELSE 0 END,0)) AS 現金總額  
                //                                    ,SUM(DECODE(SH.SALE_TYPE,'1',DECODE(PD.PAID_MODE,'2',NVL(PD.PAID_AMOUNT,0),0) ,0)) AS 信用卡額
                //                                    ,SUM(DECODE(SH.SALE_TYPE,'1',DECODE(PD.PAID_MODE,'4',NVL(PD.PAID_AMOUNT,0),0) ,0)) AS 分期付款
                //                                    ,SUM(DECODE(SH.SALE_TYPE,'1',DECODE(PD.PAID_MODE,'5',NVL(PD.PAID_AMOUNT,0),0) ,0)) AS 禮券總額
                //                                    ,SUM(DECODE(SH.SALE_TYPE,'1',DECODE(PD.PAID_MODE,'6',NVL(PD.PAID_AMOUNT,0),0) ,0)) AS 金融卡額
                //                                    ,SUM(DECODE(SH.SALE_TYPE,'2',CASE WHEN PD.PAID_MODE = '1' OR PD.PAID_MODE = '8' THEN NVL(PD.PAID_AMOUNT,0) ELSE 0 END,0)) AS 代收現金總額  
                //                                    ,SUM(DECODE(SH.SALE_TYPE,'2',DECODE(PD.PAID_MODE,'2',NVL(PD.PAID_AMOUNT,0),0) ,0)) AS 代收信用卡額
                //                                    ,SUM(NVL(PD.HG_REDEEM_POINT,0)) AS 快樂購點數的加數
                //                                    ,SUM(DECODE(PD.PAID_MODE,'7',NVL(PD.PAID_AMOUNT,0),0)) AS 快樂購金額的加數                   
                //                                FROM SALE_HEAD SH, PAID_DETAIL PD         
                //                                WHERE SH.POSUUID_MASTER = PD.POSUUID_MASTER
                //                                    " + sbWhere + strSALE_STATUS + @"
                //                                GROUP BY SH.STORE_NO, SH.STORE_NAME, SH.MACHINE_ID, TO_CHAR(SH.TRADE_DATE,'YYYY/MM/DD')       
                //                            ");
                sb.AppendLine(@"SELECT  SUM(NVL(PD.HG_REDEEM_POINT,0)) AS 快樂購點數的加數
                                    ,SUM(DECODE(PD.PAID_MODE,'7',NVL(PD.PAID_AMOUNT,0),0)) AS 快樂購金額的加數                   
                                FROM SALE_HEAD SH, PAID_DETAIL PD         
                                WHERE SH.POSUUID_MASTER = PD.POSUUID_MASTER
                                    " + sbWhere + strSALE_STATUS + @"
                                GROUP BY SH.STORE_NO, SH.STORE_NAME, SH.MACHINE_ID, TO_CHAR(SH.TRADE_DATE,'YYYY/MM/DD')       
                            ");
                dt.Load(OracleDBUtil.GetDataReader(objConn, sb.ToString()));
                if (dt.Rows.Count > 0)
                {
                    ds.Tables.Add(dt);
                }

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

            return ds;
        }

        private string CommonCondition(bool isWhere, string StoreNo, string CashRegisterNo, string TradeDate, string TableAbb)
        {
            StringBuilder sb = new StringBuilder();
            if (!string.IsNullOrEmpty(StoreNo))
            {
                if (isWhere)
                    sb.Append(" WHERE ");
                else
                    sb.Append(" AND ");
                sb.Append(TableAbb + "STORE_NO = " + OracleDBUtil.SqlStr(StoreNo.Trim()));
            }
            if (!string.IsNullOrEmpty(CashRegisterNo))
            {
                sb.Append(" AND " + TableAbb + "MACHINE_ID = " + OracleDBUtil.SqlStr(CashRegisterNo.Trim()));
            }
            if (!string.IsNullOrEmpty(TradeDate))
            {
                sb.Append(" AND TO_CHAR(" + TableAbb + "TRADE_DATE,'YYYY/MM/DD') = " + OracleDBUtil.SqlStr(TradeDate.Trim()));
            }

            return sb.ToString();
        }

        public int SaveSALE(CHK02_SALE_DTO.TERM_CLOSEDataTable dtSALE)
        {
            int intResult = 0;
            OracleConnection objConn = null;
            OracleTransaction objTx = null;
            try
            {
                objConn = OracleDBUtil.GetConnection();
                objTx = objConn.BeginTransaction();

                intResult += OracleDBUtil.Insert(objTx, dtSALE);

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

        /// <summary>
        /// 取得舊POS作廢總額
        /// </summary>
        /// <param name="CANCEL_DTM">日期</param>
        /// <param name="STORENO">門市代號</param>
        /// <param name="CANCEL_MACHINE">機台編號</param>
        /// <param name="SALE_TYPE">作廢類型 1：銷售  2：代收</param>
        /// <returns>作廢總額</returns>
        public string GetCancelTotalAmount(string CANCEL_DTM, string STORENO, string CANCEL_MACHINE, string SALE_TYPE)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(@"SELECT NVL(Sum(SUBTOT), 0) AS TOTALAMOUNT 
                            FROM THD 
                            WHERE STATUS = '1' 
                            AND TO_CHAR(CANCEL_DTM, 'YYYY/MM/DD') = " + OracleDBUtil.SqlStr(CANCEL_DTM) + @" 
                            AND SALE_TYPE = " + OracleDBUtil.SqlStr(SALE_TYPE) + @" 
                            AND STORENO = " + OracleDBUtil.SqlStr(STORENO) 
                       );

            if (!string.IsNullOrEmpty(CANCEL_MACHINE))
            {
                sb.AppendLine("AND CANCEL_MACHINE = " + OracleDBUtil.SqlStr(CANCEL_MACHINE));
            }

            DataTable dt = OracleDBUtil.Query_Data(sb.ToString());

            return dt.Rows[0]["TOTALAMOUNT"].ToString();

        }
    }
}