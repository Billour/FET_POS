using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.OracleClient;
using System.Data;
using Advtek.Utility;

namespace FET.POS.Model.Facade.FacadeImpl
{
    public class SAL03_Facade
    {
        public static DateTime SysDate = DateTime.Now;
        string PAID_MODE = @" DECODE(PAID_MODE,'1','現金',
                                               '2','信用卡',
                                               '3','離線信用卡',
                                               '4','分期付款',
                                               '5','禮劵',
                                               '6','金融卡',
                                               '7','Happy GO') PAID_MODE_NAME,
                            ";

        /// <summary>
        /// 確認Cache的資料是否存在 等級最高 優先處理 若為Cache資料時其他來源資料將不會被執行而先RUNCache的資料
        /// </summary>
        /// <param name="STORE_NO"></param>
        /// <param name="SALE_PERSON"></param>
        /// <returns></returns>
        public DataTable CheckExchangeCacheData(string STORE_NO, string SALE_PERSON)
        {
            string sqlStr = @"SELECT POSUUID_MASTER FROM SALE_HEAD 
                               WHERE SALE_STATUS = '7' and STORE_NO = " + OracleDBUtil.SqlStr(STORE_NO) + " and SALE_PERSON = " + OracleDBUtil.SqlStr(SALE_PERSON);

            OracleConnection objConn = null;
            try
            {
                objConn = Advtek.Utility.OracleDBUtil.GetConnection();
                return Advtek.Utility.OracleDBUtil.GetDataSet(objConn, sqlStr).Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (objConn.State == ConnectionState.Open)
                    objConn.Close();
                objConn.Dispose();
                OracleConnection.ClearAllPools();
            }
        }

        /// <summary>
        /// 取得換貨所需的未結清單資料表頭(新交易)
        /// </summary>
        /// <param name="PKEY">POSUUID_DETAIL</param>
        /// <returns></returns>
        public DataTable getTO_CLOSE_HEAD(string PKEY)
        {

            string[] Keys = PKEY.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
            string where = "";
            foreach (string key in Keys)
            {
                where += "'" + key + "',";
            }
            if (where.Trim() == "")
                where = " 1<>1 ";
            else
            {
                where = where.Substring(0, where.Length - 1);
                where = " POSUUID_DETAIL IN (" + where + ")";
            }
            string sqlStr = @"SELECT
                                    '' ITEM_STATUS,
                                    '1' VOUCHER_TYPE,--憑證類型
                                    h.POSUUID_MASTER,
                                    '' HOST_ID,
                                    DECODE(SERVICE_TYPE,3,2,1) SALE_TYPE,
                                     DECODE(SERVICE_TYPE,3,'帳單代收','銷售交易') SALE_TYPE_NAME,
                                     '' TRADE_DATE,
                                     7 SALE_STATUS,      --換貨未結帳
                                     '未結帳' SALE_STATUS_NAME,                                
                                     '' UNI_TITLE,
                                     '' UNI_NO,
                                     '01' INVOICE_TYPE,
                                     '' INVOICE_NO,
                                     '' INVOICE_DATE,
                                     '' SALE_NO ,--交易序號
                                     0 SALE_BEFORE_TAX,--稅前金額
                                     0 SALE_TAX,--稅額
                                     0 SALE_TOTAL_AMOUNT, --稅後金額
                                     0 DISCOUNT_BEFORE_TAX ,--折扣稅前金額
                                     0 DISCOUNT_TAX,--折扣稅額
                                     0 DISCOUNT_TOTAL_AMOUNT, --折扣稅後金額  TAX+AMOUNT  
                                     '' HG_PRODNO, --HappyGo累點料號
                                     '' HG_CARD_NO ,--Happy Go卡號
                                     0 HG_AWARD_POINT,--Happy Go累點點數
                                     0 HG_REMAIN_POINT,--Happy Go剩餘點數
                                     '' INVALID_ID, --此銷貨單對應的上一張作廢的POSUUID_MASTER
                                     NULL INVALID_DATE ,--作廢日期
                                     '' INVALID_REASON ,--作廢原因
                                     '' ORIGINAL_ID,--最原始的ID, 從未作廢的會與ID同，退換貨時會一路帶下來                                     
                                      h.SALE_PERSON,
                                     '' CREATE_USER,
                                     SYSDATE CREATE_DTM,
                                     '' MODI_USER,
                                     SYSDATE MODI_DTM,
                                           DECODE(h.STATUS,1,'未結',
                                                    2,'已結',
                                                    3,'取消',
                                                    h.STATUS) STATUS_NAME,
                                                    h.APPLY_DATE,
                                                    h.APPROVE_STATUS_POS AS APPROVE_STATUS_POS,
                                                    h.BUNDLE_ID,
                                                    h.BUNDLE_TYPE,
                                                    h.CONN_FLAG,
                                                    h.CREATE_DTM,
                                                    h.CREATE_USER,
                                                    h.DATA,
                                                    h.FUN_ID,
                                                    h.HRS_NO, 
                                                    h.LEASE_NO,
                                                    h.MACHINE_ID,
                                                    h.MODI_DTM,
                                                    h.MODI_USER,
                                                    h.POSUUID_DETAIL,
                                                    
                                                    h.QUERY_TABLE_NAME,
                                                    h.REMARK,
                                                    h.R_RATE,
                                                    h.SERVICE_SYS_ID,
                                                    h.SERVICE_TYPE,
                                                    h.STATUS,
                                                    h.STORE_NAME,
                                                    h.STORE_NO,
                                                    h.TOTAL_AMOUNT,
                                                    h.TRANS_TYPE,
                                                    h.VOICE, 
                                                    0 INVOICE_TOTAL_AMOUNT 
                               FROM TO_CLOSE_HEAD h 
                               WHERE " + where + @" 
                               AND h.FUN_ID IN ('121','122','123','124')"; //合約資料-變更促代 121, 123; 取消促代 122, 124

            OracleConnection objConn = null;
            try
            {
                objConn = Advtek.Utility.OracleDBUtil.GetConnection();
                return Advtek.Utility.OracleDBUtil.GetDataSet(objConn, sqlStr).Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (objConn.State == ConnectionState.Open)
                    objConn.Close();
                objConn.Dispose();
                OracleConnection.ClearAllPools();
            }
        }

        /// <summary>
        /// 取得換貨所需的未結清單明細及折扣明細轉成SALE_DETAIL格式(新交易)
        /// </summary>
        /// <param name="PKEY"></param>
        /// <returns></returns>
        public DataTable getTO_CLOSE_ITEM(string PKEY, string ITEM_TYPE)
        {
            //item TYPE
            /*1: 單品
            2: 服務系統(組合促銷等來自於未結明細的資料)
            3: 預購轉銷售
            4: Happy Go折抵
            5: 銷售折扣(來自於未結明細的資料)
            6: 店長折扣
            從未結明細轉入時: 2
              未結折扣轉入時為 5
            */
            string sqlStr = @"
            SELECT  ROWNUM ITEMS,
                    POS_UUID ID,
                    DECODE(M.ITEM_TYPE,2,M.I_ID,5,M.D_ID) SOURCE_REFERENCE_KEY,
                    DECODE(M.ITEM_TYPE,2,M.I_ID,5,M.D_ID) S_ID,--來源單的UUID
                    DECODE(M.ITEM_TYPE,2,AMOUNT,5,DISCOUNT_AMOUNT) TOTAL_AMOUNT,
                    DECODE(M.ITEM_TYPE,2,I_POSUUID_DETAIL,5,D_POSUUID_DETAIL) POSUUID_DETAIL,
                    DECODE(M.ITEM_TYPE,2,I_PRODNO,5,D_PRODNO) PRODNO,
                    DECODE(M.ITEM_TYPE,2,AMOUNT,5,DISCOUNT_AMOUNT) UNIT_PRICE,
                    0 IMEI_QTY ,
                    1 QUANTITY,
                    '' POSUUID_MASTER,
                    0 BEFORE_TAX,            
                    0 TAX,
                    H.Service_TYPE SOURCE_TYPE,
                    '' HG_CARD_NO,
                    0 HG_REDEEM_POINT,
                    0 HG_LEVEL_PRICE,
                    ''HG_RULE,
                    0 SH_DISCOUNT_RATE,
                    '' SH_DISCOUNT_REASON,
                    '' SH_DISCOUNT_DESC,
                    ''WRITE_OFF_FILE,
                    '' BATCH_NO,
                    h.APPLY_DATE,
                    h.SERVICE_SYS_ID,
                    h.BUNDLE_ID,
                    h.BUNDLE_TYPE,
                    h.MNP,
                    h.HRS_NO,
                    h.LEASE_NO,
                    h.FUN_ID,
                    h.R_RATE,
                    h.DATA,
                    h.VOICE,
                    h.TRANS_TYPE,
                    ''APPROVE_STATUS,
                    h.CONN_FLAG,
                    h.QUERY_TABLE_NAME,    
                    '' MSISDN_TYPE,    
                    '' IMEI,   
                    '' CREATE_USER,
                    SYSDATE CREATE_DTM,
                    '' MODI_USER,
                    SYSDATE MODI_DTM,
                    (SELECT DISCOUNT_NAME FROM DISCOUNT_MASTER D WHERE M.DISCOUNT_ID = D.DISCOUNT_CODE AND ROWNUM = 1)DISCOUNT_NAME
                    , 
                    PROMOTION_CODE PROMPTION_CODE,
                    M.* 
                    FROM 
                    (
                        SELECT 2 ITEM_TYPE ,'促' ITEM_TYPE_NAME,     
                                    I.AMOUNT,
                                    I.BARCODE1,
                                    I.BARCODE2,
                                    I.BARCODE3,
                                    I.ETC_NO,
                                    I.ID I_ID,
                                    I.MSISDN,
                                    I.ORDER_NO,
                                    I.POSUUID_DETAIL I_POSUUID_DETAIL,
                                    I.PRODNO I_PRODNO,
                                    I.PROMOTION_CODE,
                                    I.SIM_CARD_NO,
                                    D.POSUUID_DETAIL D_POSUUID_DETAIL,    
                                    D.DISCOUNT_AMOUNT,
                                    D.DISCOUNT_B_DATE,
                                    D.DISCOUNT_E_DATE,
                                    D.DISCOUNT_ID D_PRODNO,
                                    D.DISCOUNT_ID,
                                    D.DISCOUNT_PRICE,
                                    D.ID D_ID,
                                    MM.PROMO_NAME,
                                    P.PRODNAME PRODNAME,                        
                                    P.TAXABLE,
                                    P.TAXRATE, 
                                    To_Number(I.SEQNO) SEQNO      
                        FROM TO_CLOSE_ITEM I,TO_CLOSE_DISCOUNT D,
                                (Select * from MM Where to_date(to_char(sysdate, 'yyyy/mm/dd'),'yyyy/mm/dd') Between NVL(B_DATE, to_date('2000/01/01','YYYY/MM/DD')) 
                                 And NVL(E_DATE,to_date('3000/01/01','YYYY/MM/DD')) And PROMO_STATUS = '1') MM,PRODUCT P
                        WHERE I.PROMOTION_CODE = MM.PROMO_NO(+)  
                        AND I.PRODNO = P.PRODNO(+)     
                        AND I.ID = D.ID(+)
                        UNION ALL
                        SELECT 5 ITEM_TYPE , '折' ITEM_NAME,
                                I.AMOUNT,
                                I.BARCODE1,
                                I.BARCODE2,
                                I.BARCODE3,
                                I.ETC_NO,
                                I.ID I_ID,
                                I.MSISDN,
                                I.ORDER_NO,
                                I.POSUUID_DETAIL I_POSUUID_DETAIL,
                                I.PRODNO I_PRODNO,
                                I.PROMOTION_CODE,
                                I.SIM_CARD_NO,                
                                D.POSUUID_DETAIL D_POSUUID_DETAIL,    
                                D.DISCOUNT_AMOUNT,
                                D.DISCOUNT_B_DATE,
                                D.DISCOUNT_E_DATE,
                                D.DISCOUNT_ID D_PRODNO,
                                D.DISCOUNT_ID,
                                D.DISCOUNT_PRICE,
                                D.ID D_ID,
                                MM.PROMO_NAME,
                                P.PRODNAME,
                                P.TAXABLE,
                                P.TAXRATE, 
                                D.SEQNO                     
                        FROM TO_CLOSE_ITEM I,TO_CLOSE_DISCOUNT D,
                                (Select * from MM Where to_date(to_char(sysdate, 'yyyy/mm/dd'),'yyyy/mm/dd') Between NVL(B_DATE, to_date('2000/01/01','YYYY/MM/DD')) 
                                 And NVL(E_DATE,to_date('3000/01/01','YYYY/MM/DD')) And PROMO_STATUS = '1') MM,PRODUCT P
                        WHERE I.PROMOTION_CODE = MM.PROMO_NO(+)  
                        AND I.PRODNO = P.PRODNO(+)     
                        AND D.ID = I.ID(+)
                     )M ,TO_CLOSE_HEAD h
                     WHERE DECODE(M.ITEM_TYPE,2,I_POSUUID_DETAIL,5,D_POSUUID_DETAIL)  = H.POSUUID_DETAIL
                     AND DECODE(M.ITEM_TYPE,2,I_POSUUID_DETAIL,5,D_POSUUID_DETAIL) IN ('" + PKEY.Replace(";", "','") + @"') ";

            if (ITEM_TYPE != "")
            {
                sqlStr += " AND ITEM_TYPE= " + ITEM_TYPE;
            }
            sqlStr += " AND h.FUN_ID IN ('121','123') --合約資料-變更促代 ";
            sqlStr += " ORDER BY POSUUID_DETAIL, SEQNO ASC";
            OracleConnection objConn = null;
            try
            {
                objConn = Advtek.Utility.OracleDBUtil.GetConnection();
                return Advtek.Utility.OracleDBUtil.GetDataSet(objConn, sqlStr).Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (objConn.State == ConnectionState.Open)
                    objConn.Close();
                objConn.Dispose();
                OracleConnection.ClearAllPools();
            }
        }

        /// <summary>
        /// 取得換貨所需的未結清單明細及折扣明細轉成SALE_DETAIL格式(新交易)
        /// </summary>
        /// <param name="PKEY"></param>
        /// <returns></returns>
        public DataTable getTO_CLOSE_ITEM_For_Exchange(string PKEY, string ITEM_TYPE)
        {
            //item TYPE
            /*1: 單品
            2: 服務系統(組合促銷等來自於未結明細的資料)
            3: 預購轉銷售
            4: Happy Go折抵
            5: 銷售折扣(來自於未結明細的資料)
            6: 店長折扣
            從未結明細轉入時: 2
              未結折扣轉入時為 5
            */
            string sqlStr = @"
            SELECT  M.*
            FROM 
            (
                SELECT '' ITEM_STATUS,ROWNUM ITEMS,0 IMEI_QTY,
                '促' as ITEM_TYPE_NAME, MM.PROMO_NAME, pd.PRODNAME,
                '' IMEI, POS_UUID ID,2 as  ITEM_TYPE, tci.PRODNO, tci.PROMOTION_CODE, tci.CREATE_DTM,
                0 as SOURCE_TYPE, tci.ID as SOURCE_REFERENCE_KEY, '' as POSUUID_MASTER,
                to_number(tci.SEQNO) SEQNO, tci.amount as UNIT_PRICE, tci.MODI_USER, pd.TAXABLE as TAXABLE, tci.MODI_DTM,
                decode(pd.Taxable,'Y',5,'N',0,'',0) as TAXRATE, tci.CREATE_USER, tci.POSUUID_DETAIL, 
                tci.amount * tci.QUANTITY as TOTAL_AMOUNT, '' as HG_CARD_NO, 0 as HG_REDEEM_POINT, 
                '' as HG_RULE, 0 as SH_DISCOUNT_RATE, '' as SH_DISCOUNT_REASON, '' as WRITE_OFF_FILE, 
                0 as HG_LEVEL_PRICE, '' as SH_DISCOUNT_DESC, '' as BATCH_NO, 
                tch.APPLY_DATE, tch.SERVICE_SYS_ID, tch.BUNDLE_ID, tch.HRS_NO, tch.LEASE_NO, 
                tci.ORDER_NO, tci.ETC_NO, tch.FUN_ID, tch.R_RATE, tch.DATA,tch.VOICE, tch.TRANS_TYPE,
                '' as APPROVE_STATUS, tch.CONN_FLAG, tch.QUERY_TABLE_NAME, tch.BUNDLE_TYPE,
                tci.BARCODE1, tci.BARCODE2, tci.BARCODE3, tci.SIM_CARD_NO, tci.MSISDN,
                Null as DISCOUNT_B_DATE, Null as DISCOUNT_E_DATE, tci.QUANTITY, 0 as BEFORE_TAX,
                0 as TAX, tci.MSISDN_TYPE, tch.MNP,'' as  BILLING_ACCOUNT_ID,
                '' as SUBSCRIBE_NO, tci.GROUP_ID, 0 as MM_PLU_AMOUNT,0 as  MM_PLU_PRICE,
                0 as MM_PLU_TRANS_VALUE, 0 as MM_PLU_DEVICE_SUBSIDY, 0 as MM_PLU_BASE_SUBSIDY 
                from to_close_item tci, product pd, to_close_head tch, 
                        (Select * from MM Where to_date(to_char(sysdate, 'yyyy/mm/dd'),'yyyy/mm/dd') Between NVL(B_DATE, to_date('2000/01/01','YYYY/MM/DD')) 
                                 And NVL(E_DATE,to_date('3000/01/01','YYYY/MM/DD')) And PROMO_STATUS = '1') MM
                where TCI.PRODNO = pd.prodno and tch.posuuid_detail = tci.posuuid_detail(+) 
                      and tci.PROMOTION_CODE = MM.PROMO_NO(+) 
                      and tci.posuuid_detail IN ('" + PKEY.Replace(";", "','") + @"')
                      AND tch.FUN_ID IN ('121','123') --合約資料-變更促代 
                union all 
                SELECT '' ITEM_STATUS,ROWNUM ITEMS,0 IMEI_QTY,
                '折' as ITEM_TYPE_NAME, MM.PROMO_NAME, pd.PRODNAME,
                '' IMEI, POS_UUID ID,5 as  ITEM_TYPE, tci.PRODNO, tci.PROMOTION_CODE, tci.CREATE_DTM,
                0 as SOURCE_TYPE, tci.ID as SOURCE_REFERENCE_KEY, '' as POSUUID_MASTER,
                to_number(tcd.SEQNO) SEQNO, tcd.DISCOUNT_PRICE as UNIT_PRICE, tci.MODI_USER, pd.TAXABLE as TAXABLE, 
                tci.MODI_DTM, decode(pd.Taxable,'Y',5,'N',0,'',0) as TAXRATE, tci.CREATE_USER, 
                tci.POSUUID_DETAIL, tcd.DISCOUNT_AMOUNT as TOTAL_AMOUNT, '' as HG_CARD_NO, 
                0 as HG_REDEEM_POINT, '' as HG_RULE, 0 as SH_DISCOUNT_RATE, '' as SH_DISCOUNT_REASON, 
                '' as WRITE_OFF_FILE, 0 as HG_LEVEL_PRICE, '' as SH_DISCOUNT_DESC, 
                '' as BATCH_NO, tch.APPLY_DATE, tch.SERVICE_SYS_ID, tch.BUNDLE_ID, tch.HRS_NO, 
                tch.LEASE_NO, tci.ORDER_NO, tci.ETC_NO, tch.FUN_ID, tch.R_RATE, tch.DATA,tch.VOICE, 
                tch.TRANS_TYPE,'' as APPROVE_STATUS, tch.CONN_FLAG, tch.QUERY_TABLE_NAME, tch.BUNDLE_TYPE,
                tci.BARCODE1, tci.BARCODE2, tci.BARCODE3, tci.SIM_CARD_NO, tci.MSISDN,
                tcd.DISCOUNT_B_DATE, tcd.DISCOUNT_E_DATE, tci.QUANTITY, 0 as BEFORE_TAX,
                0 as TAX, tci.MSISDN_TYPE, tch.MNP,'' as  BILLING_ACCOUNT_ID,
                '' as SUBSCRIBE_NO, tci.GROUP_ID, 0 as MM_PLU_AMOUNT,0 as  MM_PLU_PRICE,
                0 as MM_PLU_TRANS_VALUE, 0 as MM_PLU_DEVICE_SUBSIDY, 0 as MM_PLU_BASE_SUBSIDY 
                from to_close_item tci, product pd, to_close_head tch, 
                        (Select * from MM Where to_date(to_char(sysdate, 'yyyy/mm/dd'),'yyyy/mm/dd') Between NVL(B_DATE, to_date('2000/01/01','YYYY/MM/DD')) 
                                 And NVL(E_DATE,to_date('3000/01/01','YYYY/MM/DD')) And PROMO_STATUS = '1') MM, to_close_discount tcd 
                where TCI.PRODNO = pd.prodno and tch.posuuid_detail = tcd.posuuid_detail(+) 
                      and tci.PROMOTION_CODE = MM.PROMO_NO(+) and tcd.ID = tci.ID(+) 
                      and tcd.posuuid_detail IN ('" + PKEY.Replace(";", "','") + @"')
                      AND tch.FUN_ID IN ('121','123') --合約資料-變更促代 
             ) M 
             WHERE 1 = 1 ";

            if (ITEM_TYPE != "")
            {
                sqlStr += " AND M.ITEM_TYPE=" + ITEM_TYPE;
            }
            sqlStr += " ORDER BY POSUUID_DETAIL, SEQNO ASC";

            OracleConnection objConn = null;
            try
            {
                objConn = Advtek.Utility.OracleDBUtil.GetConnection();
                return Advtek.Utility.OracleDBUtil.GetDataSet(objConn, sqlStr).Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (objConn.State == ConnectionState.Open)
                    objConn.Close();
                objConn.Dispose();
                OracleConnection.ClearAllPools();
            }
        }

        /// <summary>
        /// 取得換貨所需的未結清單明細及折扣明細轉成SALE_DETAIL格式(新交易)
        /// </summary>
        /// <param name="PKEY"></param>
        /// <returns></returns>
        public DataTable getTO_CLOSE_ITEM_For_Exchange(string PKEY, string ITEM_TYPE, string STORE_NO, string STOCK)
        {
            //item TYPE
            /*1: 單品
            2: 服務系統(組合促銷等來自於未結明細的資料)
            3: 預購轉銷售
            4: Happy Go折抵
            5: 銷售折扣(來自於未結明細的資料)
            6: 店長折扣
            從未結明細轉入時: 2
              未結折扣轉入時為 5
            */
            string sqlStr = @"
            SELECT  M.* 
            FROM 
            (
                SELECT '' ITEM_STATUS,ROWNUM ITEMS,0 IMEI_QTY,
                '促' as ITEM_TYPE_NAME, MM.PROMO_NAME, pd.PRODNAME, NVL(pd.ISSTOCK,0) ISSTOCK, 
                '' IMEI, I.ON_HAND_QTY, POS_UUID ID,2 as  ITEM_TYPE, tci.PRODNO, tci.PROMOTION_CODE, tci.CREATE_DTM,
                0 as SOURCE_TYPE, tci.ID as SOURCE_REFERENCE_KEY, '' as POSUUID_MASTER,
                to_number(tci.SEQNO) SEQNO, tci.amount as UNIT_PRICE, tci.MODI_USER, pd.TAXABLE as TAXABLE, tci.MODI_DTM,
                decode(pd.Taxable,'Y',5,'N',0,'',0) as TAXRATE, tci.CREATE_USER, tci.POSUUID_DETAIL, 
                tci.amount * tci.QUANTITY as TOTAL_AMOUNT, '' as HG_CARD_NO, 0 as HG_REDEEM_POINT, 
                '' as HG_RULE, 0 as SH_DISCOUNT_RATE, '' as SH_DISCOUNT_REASON, '' as WRITE_OFF_FILE, 
                0 as HG_LEVEL_PRICE, '' as SH_DISCOUNT_DESC, '' as BATCH_NO, 
                tch.APPLY_DATE, tch.SERVICE_SYS_ID, tch.BUNDLE_ID, tch.HRS_NO, tch.LEASE_NO, 
                tci.ORDER_NO, tci.ETC_NO, tch.FUN_ID, tch.R_RATE, tch.DATA,tch.VOICE, tch.TRANS_TYPE,
                '' as APPROVE_STATUS, tch.CONN_FLAG, tch.QUERY_TABLE_NAME, tch.BUNDLE_TYPE,
                tci.BARCODE1, tci.BARCODE2, tci.BARCODE3, tci.SIM_CARD_NO, tci.MSISDN,
                Null as DISCOUNT_B_DATE, Null as DISCOUNT_E_DATE, tci.QUANTITY, 0 as BEFORE_TAX,
                0 as TAX, tci.MSISDN_TYPE, tch.MNP,'' as  BILLING_ACCOUNT_ID,
                '' as SUBSCRIBE_NO, tci.GROUP_ID, 0 as MM_PLU_AMOUNT,0 as  MM_PLU_PRICE,
                0 as MM_PLU_TRANS_VALUE, 0 as MM_PLU_DEVICE_SUBSIDY, 0 as MM_PLU_BASE_SUBSIDY, 0 as ORI_UNIT_PRICE, pd.INV_TYPE   
                from to_close_item tci, product pd, to_close_head tch, 
                        (Select * from MM Where to_date(to_char(sysdate, 'yyyy/mm/dd'),'yyyy/mm/dd') Between NVL(B_DATE, to_date('2000/01/01','YYYY/MM/DD')) 
                                 And NVL(E_DATE,to_date('3000/01/01','YYYY/MM/DD')) And PROMO_STATUS = '1') MM, 
                        (Select * from INV_ON_HAND_CURRENT Where STOCK_ID = " + OracleDBUtil.SqlStr(STOCK)
                + " And STORE_NO = " + OracleDBUtil.SqlStr(STORE_NO) + ") I " +
                @" where TCI.PRODNO = pd.prodno and tch.posuuid_detail = tci.posuuid_detail(+) 
                      and tci.PROMOTION_CODE = MM.PROMO_NO(+) 
                      AND tci.PRODNO = I.PRODNO(+) 
                      and tci.posuuid_detail IN ('" + PKEY.Replace(";", "','") + @"')
                      AND tch.FUN_ID IN ('121','123') --合約資料-變更促代 
                union all 
                SELECT '' ITEM_STATUS,ROWNUM ITEMS,0 IMEI_QTY,
                '折' as ITEM_TYPE_NAME, '' as PROMO_NAME, d.DISCOUNT_NAME as PRODNAME, '0' ISSTOCK, 
                '' IMEI, 0 as ON_HAND_QTY, POS_UUID ID,5 as  ITEM_TYPE, tcd.DISCOUNT_ID as PRODNO, '' as PROMOTION_CODE, tcd.CREATE_DTM,
                TCH.SERVICE_TYPE as SOURCE_TYPE, tcd.ID as SOURCE_REFERENCE_KEY, '' as POSUUID_MASTER,
                to_number(tcd.SEQNO) SEQNO, tcd.DISCOUNT_PRICE as UNIT_PRICE, tcd.MODI_USER, 'N' as TAXABLE, 
                tcd.MODI_DTM, 0 as TAXRATE, tch.CREATE_USER, 
                tcd.POSUUID_DETAIL, tcd.DISCOUNT_AMOUNT as TOTAL_AMOUNT, '' as HG_CARD_NO, 
                0 as HG_REDEEM_POINT, '' as HG_RULE, 0 as SH_DISCOUNT_RATE, '' as SH_DISCOUNT_REASON, 
                '' as WRITE_OFF_FILE, 0 as HG_LEVEL_PRICE, '' as SH_DISCOUNT_DESC, 
                '' as BATCH_NO, tch.APPLY_DATE, tch.SERVICE_SYS_ID, tch.BUNDLE_ID, tch.HRS_NO, 
                tch.LEASE_NO, '' ORDER_NO, '' ETC_NO, tch.FUN_ID, tch.R_RATE, tch.DATA,tch.VOICE, 
                tch.TRANS_TYPE,'' as APPROVE_STATUS, tch.CONN_FLAG, tch.QUERY_TABLE_NAME, tch.BUNDLE_TYPE,
                '' BARCODE1, '' BARCODE2, '' BARCODE3, '' as SIM_CARD_NO, '' as MSISDN,
                tcd.DISCOUNT_B_DATE, tcd.DISCOUNT_E_DATE, 1 QUANTITY, 0 as BEFORE_TAX,
                0 as TAX, '' MSISDN_TYPE, tch.MNP,'' as  BILLING_ACCOUNT_ID,
                '' as SUBSCRIBE_NO, '' GROUP_ID, 0 as MM_PLU_AMOUNT,0 as  MM_PLU_PRICE,
                0 as MM_PLU_TRANS_VALUE, 0 as MM_PLU_DEVICE_SUBSIDY, 0 as MM_PLU_BASE_SUBSIDY, 0 as ORI_UNIT_PRICE, '1' as INV_TYPE   
                from to_close_head tch, 
                       to_close_discount tcd, discount_master d 
                where tch.posuuid_detail = tcd.posuuid_detail and tcd.discount_id = d.discount_code 
                      and tcd.posuuid_detail IN ('" + PKEY.Replace(";", "','") + @"')
                      AND tch.FUN_ID IN ('121','123') --合約資料-變更促代 
             ) M Where 1 = 1 ";

            if (ITEM_TYPE != "")
            {
                sqlStr += " AND M.ITEM_TYPE=" + ITEM_TYPE;
            }
            sqlStr += " ORDER BY POSUUID_DETAIL, SEQNO ASC";

            OracleConnection objConn = null;
            try
            {
                objConn = Advtek.Utility.OracleDBUtil.GetConnection();
                return Advtek.Utility.OracleDBUtil.GetDataSet(objConn, sqlStr).Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (objConn.State == ConnectionState.Open)
                    objConn.Close();
                objConn.Dispose();
                OracleConnection.ClearAllPools();
            }
        }

        /// <summary>
        /// 取得換貨需作廢的舊交易表頭資訊(舊交易)
        /// </summary>
        /// <param name="POSUUID_MASTER"></param>
        /// <returns></returns>
        public DataTable getSale_Head(string POSUUID_MASTER)
        {
            string sqlStr = @" SELECT VR.VOUCHER_TYPE AS VOUCHER_TYPE,'' HOST_ID, H.* 
                               ,DECODE(VR.VOUCHER_TYPE,'1',IH.INVOICE_NO,'2',MIH.INVOICE_NO,'3',RH.RECEIPT_NO) AS INVOICE_NO   
                               ,DECODE(VR.VOUCHER_TYPE,'1',IH.INVOICE_DATE,'2',MIH.INVOICE_DATE,'3',RH.INVOICE_DATE) AS INVOICE_DATE   
                               ,DECODE(VR.VOUCHER_TYPE,'1','發票','2',MIH.INVOICE_TYPE,'3',RH.RECEIPT_TYPE) AS INVOICE_TYPE 
                               FROM SALE_HEAD H, VOUCHER_RELATION VR, MANUAL_INVOICE_HEAD MIH, INVOICE_HEAD IH, RECEIPT_HEAD RH
                               Where H.POSUUID_MASTER = VR.SALE_HEAD_ID(+) 
                                        And H.POSUUID_MASTER = MIH.POSUUID_MASTER(+) 
                                        And H.POSUUID_MASTER = IH.POSUUID_MASTER(+) 
                                        And H.POSUUID_MASTER = RH.POSUUID_MASTER(+)  
                                        And H.POSUUID_MASTER = '" + POSUUID_MASTER + "'";

            OracleConnection objConn = null;
            try
            {
                objConn = Advtek.Utility.OracleDBUtil.GetConnection();
                return Advtek.Utility.OracleDBUtil.GetDataSet(objConn, sqlStr).Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (objConn.State == ConnectionState.Open)
                    objConn.Close();
                objConn.Dispose();
                OracleConnection.ClearAllPools();
            }
        }

        /// <summary>
        /// 取得換貨需作廢的舊交易資訊(舊交易)
        /// </summary>
        /// <param name="POSUUID_MASTER"></param>
        /// <returns></returns>
        public DataTable getSale_Detail(string POSUUID_MASTER, string ITEM_TYPE)
        {
            string sqlStr = @"SELECT '作廢' ITEM_STATUS,ROWNUM ITEMS,0 IMEI_QTY,
                                     DECODE(ITEM_TYPE,'1','單',--直接輸入
                                                      '2','促',--未結清單而來
                                                      '3','預',--預收轉採購
                                                      '4','折',--HAPPY GO 折扣
                                                      '5','折',--銷售折扣
                                                      '6','折',--店長折扣
                                                      '7','加',--加價購商品
                                                      '8','來',--HG來店禮
                                                      '9','通',--受信通聯
                                                      '10','租',--租貸
                                                      '11','租',--租貸折扣
                                                      '12','舊',--舊機換新機折扣
                                                      '13','贈',--贈品
                                                      '14','加',--HAPPY GO加價購商品
                                                      '15','折',--加價購折扣
                                                      '16','折',--贈品折扣
                                                      '17','折' --HAPPY GO加價購商品折扣
                                            ) ITEM_TYPE_NAME,
                                 MM.PROMO_NAME,
                                 p. PRODNAME,
                                 NVL(P.ISSTOCK,0) ISSTOCK, p.INV_TYPE,
                                 '' IMEI, I.ON_HAND_QTY, P.IS_OPEN_PRICE, 
                             d.* FROM  SALE_DETAIL d ,
                                        (Select * from MM Where to_date(to_char(sysdate, 'yyyy/mm/dd'),'yyyy/mm/dd') Between NVL(B_DATE, to_date('2000/01/01','YYYY/MM/DD')) 
                                 And NVL(E_DATE,to_date('3000/01/01','YYYY/MM/DD')) And PROMO_STATUS = '1') MM,PRODUCT P
                          WHERE d.PROMOTION_CODE = MM.PROMO_NO(+)  
                            AND d.PRODNO = P.PRODNO(+) 
                            AND POSUUID_MASTER = '" + POSUUID_MASTER + "'";
            string where = "";
            if (ITEM_TYPE != "") //未結折扣資料要另外放
            {
                where += " AND ITEM_TYPE IN(" + ITEM_TYPE + ")";
            }
            sqlStr += where;
            sqlStr += " ORDER BY d.SEQNO";
            OracleConnection objConn = null;
            try
            {
                objConn = Advtek.Utility.OracleDBUtil.GetConnection();
                return Advtek.Utility.OracleDBUtil.GetDataSet(objConn, sqlStr).Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (objConn.State == ConnectionState.Open)
                    objConn.Close();
                objConn.Dispose();
                OracleConnection.ClearAllPools();
            }
        }

        /// <summary>
        /// 取得換貨需作廢的舊交易資訊(舊交易)
        /// </summary>
        /// <param name="POSUUID_MASTER"></param>
        /// <returns></returns>
        public DataTable getSale_Detail(string POSUUID_MASTER, string ITEM_TYPE, string STORE_NO, string STOCK)
        {
            string sqlStr = @"SELECT '作廢' ITEM_STATUS,ROWNUM ITEMS,0 IMEI_QTY,
                                     DECODE(ITEM_TYPE,'1','單',--直接輸入
                                                      '2','促',--未結清單而來
                                                      '3','預',--預收轉採購
                                                      '4','折',--HAPPY GO 折扣
                                                      '5','折',--銷售折扣
                                                      '6','折',--店長折扣
                                                      '7','加',--加價購商品
                                                      '8','來',--HG來店禮
                                                      '9','通',--受信通聯
                                                      '10','租',--租貸
                                                      '11','租',--租貸折扣
                                                      '12','舊',--舊機換新機折扣
                                                      '13','贈',--贈品
                                                      '14','加',--HAPPY GO加價購商品
                                                      '15','折',--加價購折扣
                                                      '16','折',--贈品折扣
                                                      '17','折' --HAPPY GO加價購商品折扣
                                            ) ITEM_TYPE_NAME,
                                 MM.PROMO_NAME,
                                 p. PRODNAME,
                                 NVL(P.ISSTOCK,0) ISSTOCK, p.INV_TYPE,
                                 '' IMEI, I.ON_HAND_QTY, P.IS_OPEN_PRICE,   
                             d.* FROM  SALE_DETAIL d , MM,PRODUCT P, 
                             (Select * from INV_ON_HAND_CURRENT Where STOCK_ID = " + OracleDBUtil.SqlStr(STOCK)
                             + " And STORE_NO = " + OracleDBUtil.SqlStr(STORE_NO) + ") I "
                             + " WHERE d.PROMOTION_CODE = MM.PROMO_NO(+) AND d.PRODNO = P.PRODNO(+) AND d.PRODNO = I.PRODNO(+) "
                             + " AND POSUUID_MASTER = '" + POSUUID_MASTER + "'";
            string where = "";
            if (ITEM_TYPE != "") //未結折扣資料要另外放
            {
                where += " AND ITEM_TYPE IN(" + ITEM_TYPE + ")";
            }
            sqlStr += where;
            sqlStr += " ORDER BY d.SEQNO";
            OracleConnection objConn = null;
            try
            {
                objConn = Advtek.Utility.OracleDBUtil.GetConnection();
                return Advtek.Utility.OracleDBUtil.GetDataSet(objConn, sqlStr).Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (objConn.State == ConnectionState.Open)
                    objConn.Close();
                objConn.Dispose();
                OracleConnection.ClearAllPools();
            }
        }

        /// <summary>
        /// 取得需作廢的支付明細資料(舊交易)
        /// </summary>
        /// <param name="POSUUID_MASTER"></param>
        /// <returns></returns>
        public DataTable getPaid_Detail(string POSUUID_MASTER)
        {
            //2011-02-15 新增paid_mode '8' 找零 for 分錄帳平用, 不顯示在畫面上
            string sqlStr = @"SELECT '作廢' ITEM_STATUS, " + PAID_MODE + @"
                             P.* FROM  PAID_DETAIL P WHERE POSUUID_MASTER = " + OracleDBUtil.SqlStr(POSUUID_MASTER);
            OracleConnection objConn = null;
            try
            {
                objConn = Advtek.Utility.OracleDBUtil.GetConnection();
                return Advtek.Utility.OracleDBUtil.GetDataSet(objConn, sqlStr).Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (objConn.State == ConnectionState.Open)
                    objConn.Close();
                objConn.Dispose();
                OracleConnection.ClearAllPools();
            }
        }

        ///// <summary>
        ///// 取得手機的IMEI資料
        ///// </summary>
        ///// <param name="SALE_DETAIL_ID"> SALE_DETAIL.ID</param>
        ///// <returns></returns>
        //public DataTable getSale_IMEI_LOG(string SALE_DETAIL_ID)
        //{
        //    string sqlStr = "SELECT * FROM  SALE_IMEI_LOG WHERE SALE_DETAIL_ID = '" + SALE_DETAIL_ID + "'";
        //    OracleConnection advtekUtilityOracleDBUtilGetConnection = Advtek.Utility.OracleDBUtil.GetConnection();
        //    return Advtek.Utility.OracleDBUtil.GetDataSet(advtekUtilityOracleDBUtilGetConnection, sqlStr).Tables[0];
        //}

        /// <summary>
        /// 透過POSUUID_DETAIL取得SALE的POSUUID_MASTER
        /// </summary>
        /// <param name="POSUUID_DETAIL"></param>
        /// <returns></returns>
        public string getPOSUUID_MasterByPOSUUID_Detail(string POSUUID_DETAIL)
        {
            OracleConnection objConn = null;
            try
            {
                string[] Keys = POSUUID_DETAIL.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
                string where = "";
                foreach (string key in Keys)
                {
                    where += "'" + key + "',";
                }
                if (where.Trim() == "")
                    where = " 1<>1 ";
                else
                {
                    where = where.Substring(0, where.Length - 1);
                    where = " POSUUID_DETAIL IN (" + where + ")";
                }

                string strPOSUUID_MASTER = "";
                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                sb.Append(" SELECT DISTINCT POSUUID_MASTER FROM SALE_DETAIL " + where);

                objConn = OracleDBUtil.GetConnection();
                DataTable dt = OracleDBUtil.GetDataSet(objConn, sb.ToString()).Tables[0];
                if (dt.Rows.Count > 0)
                {
                    strPOSUUID_MASTER = dt.Rows[0]["POSUUID_MASTER"].ToString();
                }

                return strPOSUUID_MASTER;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (objConn.State == ConnectionState.Open)
                    objConn.Close();
                objConn.Dispose();
                OracleConnection.ClearAllPools();
            }
        }

        /// <summary>
        /// 透過未結清單POSUUID_MASTER取得未結清單POSUUID_DETAIL
        /// </summary>
        /// <param name="POSUUID_DETAIL"></param>
        /// <returns></returns>
        public string getPOSUUID_DetailByPOSUUID_Master(string POSUUID_MASTER)
        {
            OracleConnection objConn = null;
            try
            {
                string strPOSUUID_DETAIL = "";
                string strSql = @" Select POSUUID_DETAIL From TO_CLOSE_HEAD Where POSUUID_MASTER = " + OracleDBUtil.SqlStr(POSUUID_MASTER);

                objConn = OracleDBUtil.GetConnection();
                DataTable dt = OracleDBUtil.GetDataSet(objConn, strSql).Tables[0];
                if (dt != null && dt.Rows.Count > 0)
                {
                    strPOSUUID_DETAIL = dt.Rows[0]["POSUUID_DETAIL"].ToString();
                }

                return strPOSUUID_DETAIL;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (objConn.State == ConnectionState.Open)
                    objConn.Close();
                objConn.Dispose();
                OracleConnection.ClearAllPools();
            }
        }

        /// <summary>
        /// 取得Cache的交易資訊(Cache)
        /// </summary>
        /// <param name="POSUUID_MASTER"></param>
        /// <returns></returns>
        public DataTable getCacheSale_Detail(string POSUUID_MASTER, string ITEM_TYPE)
        {
            string sqlStr = @"SELECT '' ITEM_STATUS,ROWNUM ITEMS,0 IMEI_QTY,
                                     DECODE(ITEM_TYPE,'1','單',--直接輸入
                                                      '2','促',--未結清單而來
                                                      '3','預',--預收轉採購
                                                      '4','折',--HAPPY GO 折扣
                                                      '5','折',--銷售折扣
                                                      '6','折',--店長折扣
                                                      '7','加',--加價購商品
                                                      '8','來',--HG來店禮
                                                      '9','通',--受信通聯
                                                      '10','租',--租貸
                                                      '11','租',--租貸折扣
                                                      '12','舊',--舊機換新機折扣
                                                      '13','贈',--贈品
                                                      '14','加',--HAPPY GO加價購商品
                                                      '15','折',--加價購折扣
                                                      '16','折',--贈品折扣
                                                      '17','折' --HAPPY GO加價購商品折扣
                                            ) ITEM_TYPE_NAME,
                                 MM.PROMO_NAME,
                                 p. PRODNAME, NVL(P.ISSTOCK,0) ISSTOCK, p.INV_TYPE,
                                 '' IMEI, I.ON_HAND_QTY, P.IS_OPEN_PRICE, 
                             d.* FROM  SALE_DETAIL d ,MM,PRODUCT P
                          WHERE d.PROMOTION_CODE = MM.PROMO_NO(+)  
                            AND d.PRODNO = P.PRODNO(+)    
                            AND POSUUID_MASTER = '" + POSUUID_MASTER + "'";
            string where = "";
            if (ITEM_TYPE != "") //未結折扣資料要另外放
            {
                where += " AND ITEM_TYPE IN(" + ITEM_TYPE + ")";
            }
            sqlStr += where;
            sqlStr += " ORDER BY ROWNUM"; 
            OracleConnection objConn = null;
            try
            {
                objConn = Advtek.Utility.OracleDBUtil.GetConnection();
                return Advtek.Utility.OracleDBUtil.GetDataSet(objConn, sqlStr).Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (objConn.State == ConnectionState.Open)
                    objConn.Close();
                objConn.Dispose();
                OracleConnection.ClearAllPools();
            }
        }

        /// <summary>
        /// 取得Cache的交易資訊(Cache)
        /// </summary>
        /// <param name="POSUUID_MASTER"></param>
        /// <returns></returns>
        public DataTable getCacheSale_Detail(string POSUUID_MASTER, string ITEM_TYPE, string STORE_NO, string STOCK)
        {
            string sqlStr = @"SELECT '' ITEM_STATUS,ROWNUM ITEMS,0 IMEI_QTY,
                                     DECODE(ITEM_TYPE,'1','單',--直接輸入
                                                      '2','促',--未結清單而來
                                                      '3','預',--預收轉採購
                                                      '4','折',--HAPPY GO 折扣
                                                      '5','折',--銷售折扣
                                                      '6','折',--店長折扣
                                                      '7','加',--加價購商品
                                                      '8','來',--HG來店禮
                                                      '9','通',--受信通聯
                                                      '10','租',--租貸
                                                      '11','租',--租貸折扣
                                                      '12','舊',--舊機換新機折扣
                                                      '13','贈',--贈品
                                                      '14','加',--HAPPY GO加價購商品
                                                      '15','折',--加價購折扣
                                                      '16','折',--贈品折扣
                                                      '17','折' --HAPPY GO加價購商品折扣
                                            ) ITEM_TYPE_NAME,
                                 MM.PROMO_NAME,
                                 p.PRODNAME,
                                 NVL(P.ISSTOCK,0) ISSTOCK, p.INV_TYPE, 
                                 '' IMEI,
                                 I.ON_HAND_QTY, P.IS_OPEN_PRICE,  
                             d.* FROM  SALE_DETAIL d ,MM,PRODUCT P, 
                             (Select * from INV_ON_HAND_CURRENT Where STOCK_ID = " + OracleDBUtil.SqlStr(STOCK)
                             + " And STORE_NO = " + OracleDBUtil.SqlStr(STORE_NO) + ") I "
                             + " WHERE d.PROMOTION_CODE = MM.PROMO_NO(+) AND d.PRODNO = P.PRODNO(+) AND d.PRODNO = I.PRODNO(+) "
                             + " AND POSUUID_MASTER = '" + POSUUID_MASTER + "'";
            string where = "";
            if (ITEM_TYPE != "") //未結折扣資料要另外放
            {
                where += " AND ITEM_TYPE IN(" + ITEM_TYPE + ")";
            }
            sqlStr += where;
            sqlStr += " ORDER BY ROWNUM"; 
            OracleConnection objConn = null;
            try
            {
                objConn = Advtek.Utility.OracleDBUtil.GetConnection();
                return Advtek.Utility.OracleDBUtil.GetDataSet(objConn, sqlStr).Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (objConn.State == ConnectionState.Open)
                    objConn.Close();
                objConn.Dispose();
                OracleConnection.ClearAllPools();
            }
        }

        /// <summary>
        /// 取得換貨後的支付方式(新交易、Cache)
        /// </summary>
        /// <param name="POSUUID_MASTER"></param>
        /// <returns></returns>
        public DataTable getCachePaid_Detail(string POSUUID_MASTER)
        {
            //2011-02-15 新增paid_mode '8' 找零 for 分錄帳平用, 不顯示在畫面上
            string sqlStr = @"SELECT '' ITEM_STATUS, " + PAID_MODE + @"
                             P.* FROM  PAID_DETAIL P WHERE POSUUID_MASTER = '" + POSUUID_MASTER + "' And PAID_MODE != '8'";
            OracleConnection objConn = null;
            try
            {
                objConn = Advtek.Utility.OracleDBUtil.GetConnection();
                return Advtek.Utility.OracleDBUtil.GetDataSet(objConn, sqlStr).Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (objConn.State == ConnectionState.Open)
                    objConn.Close();
                objConn.Dispose();
                OracleConnection.ClearAllPools();
            }
        }

        /// <summary>
        /// 刪除未結清單資料
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public int delTO_CLOSE(DataTable sale_head)
        {
            StringBuilder condition = new StringBuilder("");
            string where = "";
            OracleConnection objConn = null;

            foreach (DataRow to_close_headRow in sale_head.Rows)
            {
                if (to_close_headRow["POSUUID_DETAIL"] != null && to_close_headRow["POSUUID_DETAIL"].ToString().Trim() != "" &&
                    condition.ToString().IndexOf(to_close_headRow["POSUUID_DETAIL"].ToString().Trim()) < 0)
                {
                    condition.Append("'").Append(to_close_headRow["POSUUID_DETAIL"].ToString().Trim()).Append("'").Append(",");
                }
            }
            if (condition.ToString().Trim() == "")
                where = " 1<>1 ";
            else
            {
                where = condition.ToString().Substring(0, condition.ToString().Length - 1);
                where = " POSUUID_DETAIL IN (" + where + ")";
            }
            string sqlStr = @"Delete FROM TO_CLOSE_ITEM WHERE " + where + " And POSUUID_DETAIL IN (Select POSUUID_DETAIL From TO_CLOSE_HEAD where FUN_ID IN ('121','123')) ";
            try
            {
                objConn = Advtek.Utility.OracleDBUtil.GetConnection();
                if (Advtek.Utility.OracleDBUtil.ExecuteSql(objConn, sqlStr) > -1)
                {
                    sqlStr = @"Delete FROM TO_CLOSE_DISCOUNT WHERE " + where + " And POSUUID_DETAIL IN (Select POSUUID_DETAIL From TO_CLOSE_HEAD where FUN_ID IN ('121','123')) ";
                    if (Advtek.Utility.OracleDBUtil.ExecuteSql(objConn, sqlStr) > -1)
                    {
                        sqlStr = @"Delete FROM TO_CLOSE_HEAD h WHERE " + where + " And FUN_ID IN ('121','123')";
                        int ret = Advtek.Utility.OracleDBUtil.ExecuteSql(objConn, sqlStr);
                        if (ret > -1)
                        {
                            return ret;
                        }
                        else
                        {
                            return -1;
                        }
                    }
                    else
                    {
                        return -1;
                    }
                }
                else
                {
                    return -1;
                }
            }
            catch (Exception)
            {
                return -1;
            }
            finally
            {
                if (objConn.State == ConnectionState.Open)
                    objConn.Close();
                objConn.Dispose();
                OracleConnection.ClearAllPools();
            }
        }

        /// <summary>
        /// 作廢原銷售交易資料
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public int invalidOldTransaction(OracleTransaction objTX, string POSUUID_MASTER, DateTime transDate, string MODI_USER)
        {
            string sqlStr = "";
            if (transDate.Month == DateTime.Now.Month)
            {
                sqlStr = @"Update Sale_Head Set SALE_STATUS= '5', INVALID_DATE = sysdate, INVALID_NO = " + Advtek.Utility.SerialNo.GenNo("SC") +
                            ", INVALID_REMARK = '換貨作廢', MODI_USER = " + OracleDBUtil.SqlStr(MODI_USER) + ", MODI_DTM = sysdate Where POSUUID_MASTER = "
                            + OracleDBUtil.SqlStr(POSUUID_MASTER);
            }
            else
            {
                sqlStr = @"Update Sale_Head Set SALE_STATUS= '6', INVALID_DATE = sysdate, INVALID_NO = " + Advtek.Utility.SerialNo.GenNo("SC") +
                            ", INVALID_REMARK = '跨月換貨作廢', MODI_USER = " + OracleDBUtil.SqlStr(MODI_USER) + ", MODI_DTM = sysdate Where POSUUID_MASTER = "
                            + OracleDBUtil.SqlStr(POSUUID_MASTER);
            }

            int ret = Advtek.Utility.OracleDBUtil.ExecuteSql(objTX, sqlStr);
            if (ret > -1)
            {
                return ret;
            }
            else
            {
                return -1;
            }
        }

        /// <summary>
        /// 作廢原銷售交易資料
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public int invalidOldTransactionPeriod(OracleTransaction objTX, string POSUUID_MASTER, DateTime invoiceDate, string MODI_USER)
        {
            string sqlStr = "";
            int invMonth = invoiceDate.Month;
            int curMonth = DateTime.Now.Month;
            if ((invMonth == curMonth) || ((invMonth % 2 == 1) && (curMonth == invMonth + 1)))
            {
                sqlStr = @"Update Sale_Head Set SALE_STATUS= '5', INVALID_DATE = sysdate, INVALID_NO = " + Advtek.Utility.SerialNo.GenNo("SC") +
                            ", INVALID_REMARK = '換貨作廢', MODI_USER = " + OracleDBUtil.SqlStr(MODI_USER) + ", MODI_DTM = sysdate Where POSUUID_MASTER = "
                            + OracleDBUtil.SqlStr(POSUUID_MASTER);
            }
            else
            {
                sqlStr = @"Update Sale_Head Set SALE_STATUS= '6', INVALID_DATE = sysdate, INVALID_NO = " + Advtek.Utility.SerialNo.GenNo("SC") +
                            ", INVALID_REMARK = '跨期換貨作廢', MODI_USER = " + OracleDBUtil.SqlStr(MODI_USER) + ", MODI_DTM = sysdate Where POSUUID_MASTER = "
                            + OracleDBUtil.SqlStr(POSUUID_MASTER);
            }

            int ret = Advtek.Utility.OracleDBUtil.ExecuteSql(objTX, sqlStr);
            if (ret > -1)
            {
                return ret;
            }
            else
            {
                return -1;
            }
        }

        /// <summary>
        /// 作廢原銷售發票及收據資料
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public int invalidOldInvoice(OracleTransaction objTX, string POSUUID_MASTER, DateTime transDate, string CreditNoteNo)
        {
            string sqlStr = "";
            if (transDate.Month == DateTime.Now.Month)
            {
                sqlStr = @"Update INVOICE_HEAD Set INVALID_DATE = sysdate, IS_INVALID = 'Y' Where POSUUID_MASTER = " + OracleDBUtil.SqlStr(POSUUID_MASTER);
            }
            else
            {
                sqlStr = @"Update INVOICE_HEAD Set INVALID_DATE = sysdate, CREDIT_NOTE_NO = " + OracleDBUtil.SqlStr(CreditNoteNo) +
                            ", IS_INVALID = 'Y', CREDIT_NOTE_STATUS = 'Y' Where POSUUID_MASTER = " + OracleDBUtil.SqlStr(POSUUID_MASTER);
            }

            int ret = Advtek.Utility.OracleDBUtil.ExecuteSql(objTX, sqlStr);
            if (ret > -1)
            {
                if (transDate.Month == DateTime.Now.Month)
                {
                    sqlStr = @"Update MANUAL_INVOICE_HEAD Set INVALID_DATE = sysdate, IS_INVALID = 'Y' Where POSUUID_MASTER = " + OracleDBUtil.SqlStr(POSUUID_MASTER);
                }
                else
                {
                    sqlStr = @"Update MANUAL_INVOICE_HEAD Set INVALID_DATE = sysdate, CREDIT_NOTE_NO = " + OracleDBUtil.SqlStr(CreditNoteNo) +
                                ", IS_INVALID = 'Y', CREDIT_NOTE_STATUS = 'Y' Where POSUUID_MASTER = " + OracleDBUtil.SqlStr(POSUUID_MASTER);
                }

                int ret2 = Advtek.Utility.OracleDBUtil.ExecuteSql(objTX, sqlStr);
                if (ret2 > -1)
                {
                    sqlStr = @"Update RECEIPT_HEAD Set INVALID_DATE = sysdate, IS_INVALID = 'Y' Where POSUUID_MASTER = " + OracleDBUtil.SqlStr(POSUUID_MASTER);
                    ret += ret2;

                    int ret3 = Advtek.Utility.OracleDBUtil.ExecuteSql(objTX, sqlStr);
                    if (ret3 > -1)
                    {
                        ret += ret3;
                    }
                    else
                    {
                        return -1;
                    }
                }
                else
                {
                    return -1;
                }

                return ret;
            }
            else
            {
                return -1;
            }
        }

        /// <summary>
        /// 作廢原銷售發票及收據資料
        /// 判斷是否為作廢或折讓
        /// </summary>
        /// <param name="Invalid">是否為作廢</param> true 為作廢 false 為折讓
        /// <param name="POSUUID_MASTER">銷貨單號</param>
        /// <returns></returns>
        public int invalidOldInvoice1(OracleTransaction objTX, string POSUUID_MASTER, Boolean Invalid, string CreditNoteNo)
        {
            string sqlStr = "";
            if (Invalid)
            {
                sqlStr = @"Update INVOICE_HEAD Set INVALID_DATE = sysdate, IS_INVALID = 'Y' Where POSUUID_MASTER = " + OracleDBUtil.SqlStr(POSUUID_MASTER);
            }
            else
            {
                sqlStr = @"Update INVOICE_HEAD Set INVALID_DATE = sysdate, CREDIT_NOTE_NO = " + OracleDBUtil.SqlStr(CreditNoteNo) +
                            ", IS_INVALID = 'Y', CREDIT_NOTE_STATUS = 'Y' Where POSUUID_MASTER = " + OracleDBUtil.SqlStr(POSUUID_MASTER);
            }

            int ret = Advtek.Utility.OracleDBUtil.ExecuteSql(objTX, sqlStr);
            if (ret > -1)
            {
                if (Invalid)
                {
                    sqlStr = @"Update MANUAL_INVOICE_HEAD Set INVALID_DATE = sysdate, IS_INVALID = 'Y' Where POSUUID_MASTER = " + OracleDBUtil.SqlStr(POSUUID_MASTER);
                }
                else
                {
                    sqlStr = @"Update MANUAL_INVOICE_HEAD Set INVALID_DATE = sysdate, CREDIT_NOTE_NO = " + OracleDBUtil.SqlStr(CreditNoteNo) +
                                ", IS_INVALID = 'Y', CREDIT_NOTE_STATUS = 'Y' Where POSUUID_MASTER = " + OracleDBUtil.SqlStr(POSUUID_MASTER);
                }

                int ret2 = Advtek.Utility.OracleDBUtil.ExecuteSql(objTX, sqlStr);
                if (ret2 > -1)
                {
                    sqlStr = @"Update RECEIPT_HEAD Set INVALID_DATE = sysdate, IS_INVALID = 'Y' Where POSUUID_MASTER = " + OracleDBUtil.SqlStr(POSUUID_MASTER);
                    ret += ret2;

                    int ret3 = Advtek.Utility.OracleDBUtil.ExecuteSql(objTX, sqlStr);
                    if (ret3 > -1)
                    {
                        sqlStr = @"Update INVOICE_NO_POOL Set INV_DISUSE_DATE = sysdate, STATUS = '2' Where POS_MASTER_UUID = " + OracleDBUtil.SqlStr(POSUUID_MASTER);
                        ret += ret3;

                        int ret4 = Advtek.Utility.OracleDBUtil.ExecuteSql(objTX, sqlStr);
                        if (ret4 > -1)
                        {
                            ret += ret4;
                        }
                        else
                        {
                            return -1;
                        }
                    }
                    else
                    {
                        return -1;
                    }
                }
                else
                {
                    return -1;
                }

                return ret;
            }
            else
            {
                return -1;
            }
        }

        /// <summary>
        /// 換貨GL分錄
        /// </summary>
        /// <param name="newPOSUUID_MASTER">新銷售單主鍵</param>
        /// <param name="oldPOSUUID_MASTER">舊銷售單主鍵</param>
        /// <returns></returns>
        public string runGL(OracleTransaction objTX, string newPOSUUID_MASTER, string oldPOSUUID_MASTER)
        {
            string strMCode = "";
            OracleCommand oraCmd = null;
            try
            {
                oraCmd = new OracleCommand("PK_GL.GL_CHANGE");
                oraCmd.CommandType = System.Data.CommandType.StoredProcedure;

                oraCmd.Parameters.Add(new OracleParameter("OLD_POSUUID_MASTER", oldPOSUUID_MASTER));
                oraCmd.Parameters.Add(new OracleParameter("NEW_POSUUID_MASTER", newPOSUUID_MASTER));
                oraCmd.Parameters.Add(new OracleParameter("outMSGCODE", OracleType.VarChar, 4000)).Direction = ParameterDirection.Output;
                oraCmd.Parameters.Add(new OracleParameter("outMESSAGE", OracleType.VarChar, 4000)).Direction = ParameterDirection.Output;

                oraCmd.Connection = objTX.Connection;
                oraCmd.Transaction = objTX;
                oraCmd.ExecuteNonQuery();

                strMCode = oraCmd.Parameters["outMSGCODE"].Value.ToString() + "|" + oraCmd.Parameters["outMESSAGE"].Value.ToString();
            }
            catch //(Exception ex)
            {
                strMCode = "999|SP Error!"; //+ ex.Message.ToString();
                //throw ex;
            }
            return strMCode;
        }

        /// <summary>
        /// IMEI_LOG
        /// </summary>
        /// <param name="POSUUID_MASTER"></param>
        /// <returns></returns>
        public string IMEICHANGE_Log(OracleTransaction objTX, string POSUUID_MASTER, string oldPOSUUID_MASTER, string MODI_USER)
        {
            string strMCode = "";
            OracleCommand oraCmd = null;
            try
            {
                oraCmd = new OracleCommand("SP_IMEICHANGE_LOG");
                oraCmd.CommandType = System.Data.CommandType.StoredProcedure;

                oraCmd.Parameters.Add(new OracleParameter("inPOSUUID_MASTER", POSUUID_MASTER));
                oraCmd.Parameters.Add(new OracleParameter("inOLDPOSUUID_MASTER", oldPOSUUID_MASTER));
                oraCmd.Parameters.Add(new OracleParameter("inCHANNEL_ID", "RETAIL"));
                oraCmd.Parameters.Add(new OracleParameter("inFUNC_ID", "SAL03"));
                oraCmd.Parameters.Add(new OracleParameter("inMODI_USER", MODI_USER));
                oraCmd.Parameters.Add(new OracleParameter("outMSGCODE", OracleType.VarChar, 4000)).Direction = ParameterDirection.Output;
                oraCmd.Parameters.Add(new OracleParameter("outMESSAGE", OracleType.VarChar, 4000)).Direction = ParameterDirection.Output;
                oraCmd.Parameters.Add(new OracleParameter("outIMEI_LOG_ID", OracleType.VarChar, 4000)).Direction = ParameterDirection.Output;

                oraCmd.Connection = objTX.Connection;
                oraCmd.Transaction = objTX;
                oraCmd.ExecuteNonQuery();

                strMCode = oraCmd.Parameters["outMSGCODE"].Value.ToString() + "|" + oraCmd.Parameters["outMESSAGE"].Value.ToString();
            }
            catch //(Exception ex)
            {
                strMCode = "999|SP Error!"; //+ ex.Message.ToString();
                //throw ex;
            }
            return strMCode;
        }

        /// <summary>
        /// SALE_IMEI_LOG
        /// </summary>
        /// <returns></returns>
        public int ADD_SALE_IMEI_Log(string SALE_DETAIL_ID, string oldSALE_DETAIL_ID, string creater, string modiuser, OracleTransaction objTX)
        {
            int ret = 1;
            try
            {
                string sqlStr = @" Select IMEI From SALE_IMEI_LOG Where SALE_STATUS = '2' AND SALE_DETAIL_ID = " + OracleDBUtil.SqlStr(oldSALE_DETAIL_ID);
                DataTable dtIMEI = Advtek.Utility.OracleDBUtil.GetDataSet(objTX, sqlStr).Tables[0];
                if (dtIMEI != null && dtIMEI.Rows.Count > 0)
                {
                    if (dtIMEI.Rows[0][0] != null && StringUtil.CStr(dtIMEI.Rows[0][0]) != "")
                    {
                        string IMEI = StringUtil.CStr(dtIMEI.Rows[0][0]);
                        sqlStr = @" Insert into SALE_IMEI_LOG values(POS_UUID(), " + OracleDBUtil.SqlStr(IMEI) +
                                    ", '7', null, null, null, " + OracleDBUtil.SqlStr(creater) + ", sysdate, " + OracleDBUtil.SqlStr(modiuser) +
                                    ", sysdate, " + OracleDBUtil.SqlStr(SALE_DETAIL_ID) + ")";
                        ret = OracleDBUtil.ExecuteSql(objTX, sqlStr);
                        if (ret == 0)
                            ret = -1;
                    }
                }
            }
            catch 
            {
                ret = -1;
            }
            return ret;
        }

        /// <summary>
        /// SALE_IMEI_LOG
        /// </summary>
        /// <returns></returns>
        public int ADD_SALE_IMEI_Log(string SALE_DETAIL_ID, string POSUUID_DETAIL, string PRODNO, string creater, string modiuser, OracleTransaction objTX)
        {
            int ret = 1;
            try
            {
                string sqlStr = @" Select IMEI From SALE_IMEI_LOG Where SALE_STATUS = '2' AND SALE_DETAIL_ID in 
                                    (Select id as SALE_DETAIL_ID from SALE_DETAIL where POSUUID_DETAIL = " +
                                   OracleDBUtil.SqlStr(POSUUID_DETAIL) + " and PRODNO = " + OracleDBUtil.SqlStr(PRODNO) +
                                   @") And IMEI Not in (Select IMEI From SALE_IMEI_LOG Where SALE_STATUS = '7')";
                DataTable dtIMEI = Advtek.Utility.OracleDBUtil.GetDataSet(objTX, sqlStr).Tables[0];
                if (dtIMEI != null && dtIMEI.Rows.Count > 0)
                {
                    if (dtIMEI.Rows[0][0] != null && StringUtil.CStr(dtIMEI.Rows[0][0]) != "")
                    {
                        string IMEI = StringUtil.CStr(dtIMEI.Rows[0][0]);
                        sqlStr = @" Insert into SALE_IMEI_LOG values(POS_UUID(), " + OracleDBUtil.SqlStr(IMEI) +
                                    ", '7', null, null, null, " + OracleDBUtil.SqlStr(creater) + ", sysdate, " + OracleDBUtil.SqlStr(modiuser) +
                                    ", sysdate, " + OracleDBUtil.SqlStr(SALE_DETAIL_ID) + ")";
                        ret = OracleDBUtil.ExecuteSql(objTX, sqlStr);
                        if (ret == 0)
                            ret = -1;
                    }
                }
            }
            catch
            {
                ret = -1;
            }
            return ret;
        }
    }
}
