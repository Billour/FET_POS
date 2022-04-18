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
using System.Collections.Specialized;

namespace FET.POS.Model.Facade.FacadeImpl
{
    public class SAL01_Facade : BaseClass
    {
        public static DateTime SysDate = DateTime.Now;
        /// <summary>
        /// 取得未結清單資料表頭
        /// </summary>
        /// <param name="args"></param>
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
            string sqlStr = @"SELECT '1' VOUCHER_TYPE,--憑證類型
                                    h.POSUUID_MASTER,
                                    '' HOST_ID,
                                    DECODE(SERVICE_TYPE,3,2,1) SALE_TYPE,
                                     DECODE(SERVICE_TYPE,3,'帳單代收','銷售交易') SALE_TYPE_NAME,
                                     '' TRADE_DATE,
                                     1 SALE_STATUS,      --未結帳
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
                                                 --   h.ORI_POSUUID_MASTER,
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
                                                    0 as INVOICE_TOTAL_AMOUNT 
                               FROM TO_CLOSE_HEAD h WHERE " + where;

            DataTable dt = OracleDBUtil.Query_Data(sqlStr);
            return dt;

        }

        /// <summary>
        /// 取得未結清單資料 for 取消交易用
        /// </summary>
        /// <param name="POSUUID_MASTER">銷售主檔鍵值</param>
        /// <returns></returns>
        public DataTable getCancleTO_CLOSE_DATA(string POSUUID_MASTER)
        {
            string sqlStr = @"Select  h.POSUUID_DETAIL
                                    , h.SERVICE_TYPE
                                    , h.SERVICE_SYS_ID
                                    , h.BUNDLE_ID
                                    , h.TOTAL_AMOUNT
                                    , h.STORE_NO
                                    , h.SALE_PERSON 
                                    , i.BARCODE1
                                    , i.BARCODE2
                                    , i.BARCODE3
                                    , i.AMOUNT 
                                FROM TO_CLOSE_HEAD h, 
                                    ( Select POSUUID_DETAIL
                                            , BARCODE1
                                            , BARCODE2
                                            , BARCODE3
                                            , AMOUNT 
                                      From TO_CLOSE_ITEM 
                                      Where BARCODE1 is not null or BARCODE2 is not null or BARCODE3 is not null 
                                        And POSUUID_DETAIL IN (Select POSUUID_DETAIL From TO_CLOSE_HEAD where POSUUID_MASTER = " + OracleDBUtil.SqlStr(POSUUID_MASTER) + @")
                                    ) i 
                                 Where h.POSUUID_DETAIL = i.POSUUID_DETAIL(+) 
                                   And h.POSUUID_MASTER = " + OracleDBUtil.SqlStr(POSUUID_MASTER);

            DataTable dt = OracleDBUtil.Query_Data(sqlStr);
            return dt;
        }

        /// <summary>
        /// 刪除未結清單資料
        /// </summary>
        /// <param name="POSUUID_MASTER">銷售表頭檔主鍵</param>
        /// <returns></returns>
        public int delTO_CLOSE(string POSUUID_MASTER)
        {
            OracleConnection objConn = null;
            string sqlStr = @"Delete FROM TO_CLOSE_ITEM WHERE POSUUID_DETAIL IN (Select POSUUID_DETAIL From TO_CLOSE_HEAD where POSUUID_MASTER = " + OracleDBUtil.SqlStr(POSUUID_MASTER) + ") ";
            try
            {
                objConn = OracleDBUtil.GetConnection();
                if (OracleDBUtil.ExecuteSql(objConn, sqlStr) > -1)
                {
                    sqlStr = @"Delete FROM TO_CLOSE_DISCOUNT WHERE POSUUID_DETAIL IN (Select POSUUID_DETAIL From TO_CLOSE_HEAD where POSUUID_MASTER = " + OracleDBUtil.SqlStr(POSUUID_MASTER) + ") ";
                    if (OracleDBUtil.ExecuteSql(objConn, sqlStr) > -1)
                    {
                        sqlStr = @"Delete FROM TO_CLOSE_HEAD where POSUUID_MASTER = " + OracleDBUtil.SqlStr(POSUUID_MASTER);
                        int ret = OracleDBUtil.ExecuteSql(objConn, sqlStr);
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
        /// 刪除未結清單資料
        /// </summary>
        /// <param name="posuuid_detailList">未結清單表頭檔主鍵值群</param>
        /// <returns></returns>
        public int delTO_CLOSE(StringBuilder posuuid_detailList)
        {
            OracleConnection objConn = null;
            string where = "";
            if (posuuid_detailList.ToString().Substring(posuuid_detailList.ToString().Length - 1, 1) == ",")
                where = posuuid_detailList.ToString().Substring(0, posuuid_detailList.ToString().Length - 1);
            else
                where = posuuid_detailList.ToString();
            string sqlStr = @"Delete FROM TO_CLOSE_ITEM WHERE POSUUID_DETAIL IN (" + where + ") ";
            try
            {
                objConn = OracleDBUtil.GetConnection();
                if (OracleDBUtil.ExecuteSql(objConn, sqlStr) > -1)
                {
                    sqlStr = @"Delete FROM TO_CLOSE_DISCOUNT WHERE POSUUID_DETAIL IN (" + where + ") ";
                    if (OracleDBUtil.ExecuteSql(objConn, sqlStr) > -1)
                    {
                        sqlStr = @"Delete FROM TO_CLOSE_HEAD where POSUUID_DETAIL IN (" + where + ") ";
                        int ret = OracleDBUtil.ExecuteSql(objConn, sqlStr);
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
        /// 刪除尚未結帳銷售資料
        /// </summary>
        /// <param name="POSUUID_MASTER">銷售表頭檔鍵值</param>
        /// <returns></returns>
        public int delSaleData(string POSUUID_MASTER)
        {
            OracleConnection objConn = null;
            OracleTransaction objTX = null;

            try
            {
                objConn = OracleDBUtil.GetConnection();
                objTX = objConn.BeginTransaction();

                string sqlStrPaid = @"Delete FROM PAID_DETAIL WHERE POSUUID_MASTER = " + OracleDBUtil.SqlStr(POSUUID_MASTER);
                OracleDBUtil.ExecuteSql(objTX, sqlStrPaid);

                string sqlStr = @"Delete FROM SALE_DETAIL WHERE POSUUID_MASTER = " + OracleDBUtil.SqlStr(POSUUID_MASTER);


                if (OracleDBUtil.ExecuteSql(objTX, sqlStr) > -1)
                {
                    sqlStr = @"Delete FROM SALE_HEAD WHERE POSUUID_MASTER = " + OracleDBUtil.SqlStr(POSUUID_MASTER);
                    int ret = OracleDBUtil.ExecuteSql(objTX, sqlStr);
                    if (ret > -1)
                    {
                        objTX.Commit();
                        return ret;
                    }
                    else
                    {
                        objTX.Rollback();
                        return -1;
                    }
                }
                else
                {
                    objTX.Rollback();
                    return -1;
                }
            }
            catch (Exception ex)
            {
                string msg = ex.Message;
                objTX.Rollback();
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
        /// 歸還及作廢尚未結帳銷售IMEI log資料
        /// </summary>
        /// <param name="POSUUID_MASTER">銷售表頭檔鍵值</param>
        /// <returns></returns>
        public void invalidSaleIMEILog(string POSUUID_MASTER)
        {
            OracleConnection objConn = null;
            OracleTransaction objTx = null;
            try
            {
                objConn = Advtek.Utility.OracleDBUtil.GetConnection();
                objTx = objConn.BeginTransaction();

                string sqlStrPaid = @"Update IMEI Set status = '2' Where imei in (Select imei From SALE_IMEI_LOG where sale_detail_id in 
                                        (Select id as sale_detail_log From SALE_DETAIL where posuuid_master = " + OracleDBUtil.SqlStr(POSUUID_MASTER)
                                    + ")) And ivrcode = (Select store_no as ivrcode From SALE_HEAD where posuuid_master = " 
                                    + OracleDBUtil.SqlStr(POSUUID_MASTER) + ")";
                int ret = OracleDBUtil.ExecuteSql(objTx, sqlStrPaid);

                if (ret >= 0)
                {
                    sqlStrPaid = @"Update SALE_IMEI_LOG Set Sale_Status = '3' WHERE SALE_DETAIL_ID in (Select ID as SALE_DETAIL_ID From SALE_DETAIL Where POSUUID_MASTER = " 
                                    + OracleDBUtil.SqlStr(POSUUID_MASTER) + ")";
                    ret = OracleDBUtil.ExecuteSql(objTx, sqlStrPaid);
                    if (ret < 0)
                        objTx.Rollback();
                    else
                        objTx.Commit();
                }
            }
            catch (Exception ex)
            {
                objTx.Rollback();
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
        /// 歸還及作廢尚未結帳銷售IMEI log資料
        /// </summary>
        /// <param name="POSUUID_MASTER">銷售表頭檔鍵值</param>
        /// <returns></returns>
        public void invalidSaleIMEILog(string POSUUID_MASTER, OracleTransaction objTX)
        {
            try
            {
                string sqlStrPaid = @"Update IMEI Set status = '2' Where imei in (Select imei From SALE_IMEI_LOG where sale_detail_id in 
                                        (Select id as sale_detail_log From SALE_DETAIL where posuuid_master = " + OracleDBUtil.SqlStr(POSUUID_MASTER)
                                    + ")) And ivrcode = (Select store_no as ivrcode From SALE_HEAD where posuuid_master = "
                                    + OracleDBUtil.SqlStr(POSUUID_MASTER) + ")";
                int ret = OracleDBUtil.ExecuteSql(objTX, sqlStrPaid);

                if (ret >= 0)
                {
                    sqlStrPaid = @"Update SALE_IMEI_LOG Set SALE_STATUS = '3' WHERE SALE_DETAIL_ID in (Select ID as SALE_DETAIL_ID From SALE_DETAIL Where POSUUID_MASTER = "
                                    + OracleDBUtil.SqlStr(POSUUID_MASTER) + ")";
                    ret = OracleDBUtil.ExecuteSql(objTX, sqlStrPaid);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 取得未結清單明細及折扣明細轉成SALE_DETAIL格式
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
            SELECT ROWNUM SEQNO,
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
                        null WRITE_OFF_DATE,
                        '' BATCH_NO,
                        h.APPLY_DATE,
                       h.SERVICE_SYS_ID,
                       h.BUNDLE_ID,
                       h.BUNDLE_TYPE,
                       h.MNP,
                       h.HRS_NO,
                       h.LEASE_NO,
                 --      m.order_no,
                --      m.ETC_NO,
                       h.FUN_ID,
                       h.R_RATE,
                       h.DATA,
                       h.VOICE,
                       h.TRANS_TYPE,
                       ''APPROVE_STATUS,
                       null APPROVAL_DATE,
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
                        PROMOTION_CODE,
                        M.* ,
                        'N' AS IS_OPEN_PRICE
                    FROM 
                    (
                        SELECT '2' ITEM_TYPE ,'促' ITEM_TYPE_NAME, To_Number(I.SEQNO) OLDSEQNO,       
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
                                    P.INV_TYPE,                         
                                    P.TAXABLE,
                                    P.TAXRATE,
                                    NVL(P.ISSTOCK,0) ISSTOCK 
                        FROM TO_CLOSE_ITEM I,TO_CLOSE_DISCOUNT D, 
                                (Select * from MM Where to_date(to_char(sysdate, 'yyyy/mm/dd'),'yyyy/mm/dd') Between NVL(B_DATE, to_date('2000/01/01','YYYY/MM/DD')) 
                                 And NVL(E_DATE,to_date('3000/01/01','YYYY/MM/DD')) And PROMO_STATUS = '1') MM,PRODUCT P
                        WHERE I.PROMOTION_CODE = MM.PROMO_NO(+)  
                          AND I.PRODNO = P.PRODNO(+)     
                          AND I.ID = D.ID(+)
                        UNION ALL
                        SELECT '5' ITEM_TYPE , '折' ITEM_NAME, D.SEQNO OLDSEQNO, 
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
                                P.INV_TYPE, 
                                P.TAXABLE,
                                P.TAXRATE,
                                NVL(P.ISSTOCK,0) ISSTOCK                     
                        FROM TO_CLOSE_ITEM I,TO_CLOSE_DISCOUNT D, 
                                (Select * from MM Where to_date(to_char(sysdate, 'yyyy/mm/dd'),'yyyy/mm/dd') Between NVL(B_DATE, to_date('2000/01/01','YYYY/MM/DD')) 
                                 And NVL(E_DATE,to_date('3000/01/01','YYYY/MM/DD')) And PROMO_STATUS = '1') MM,PRODUCT P
                        WHERE I.PROMOTION_CODE = MM.PROMO_NO(+)  
                          AND I.PRODNO = P.PRODNO(+)     
                          AND D.ID = I.ID(+)
                     )M ,TO_CLOSE_HEAD h
                     WHERE DECODE(M.ITEM_TYPE,2,I_POSUUID_DETAIL,5,D_POSUUID_DETAIL)  = H.POSUUID_DETAIL
                     AND DECODE(M.ITEM_TYPE,2,I_POSUUID_DETAIL,5,D_POSUUID_DETAIL) IN ('" + PKEY.Replace(";", "','") + @"') ";
            // string where  = "";
            if (ITEM_TYPE != "")
                sqlStr += " AND M.ITEM_TYPE='" + ITEM_TYPE + "'";
            sqlStr += " ORDER BY M.ITEM_TYPE, M.OLDSEQNO";

            DataTable dt = OracleDBUtil.Query_Data(sqlStr);
            return dt;

        }

        /// <summary>
        /// 取得支付明細資料
        /// </summary>
        /// <param name="POSUUID_MASTER"></param>
        /// <returns></returns>
        public DataTable getPAID_DETAIL(string POSUUID_MASTER)
        {
            string sqlStr = @"SELECT DECODE(pd.PAID_MODE,1,'現金',
                                                         2,'信用卡',
                                                         3,'離線信用卡',
                                                         4,'分期付款',
                                                         5,'禮劵',
                                                         6,'金融卡',
                                                         7,'Happy Go')PAID_MODE_NAME,
                                     pd.* FROM　PAID_DETAIL pd WHERE pd.POSUUID_MASTER = " + OracleDBUtil.SqlStr(POSUUID_MASTER);

            DataTable dt = OracleDBUtil.Query_Data(sqlStr);
            return dt;

        }

        ///todo 取庫存資料    取得目前庫存量
        public int getINV_ON_HAND_CURRENT(string PRODNO, string STORE_NO)
        {
            int r = 0;
            OracleConnection objConn = null;
            try
            {
                objConn = OracleDBUtil.GetConnection();
                string sqlStr = " Select Inv_OnhandQty(" + OracleDBUtil.SqlStr(PRODNO) + ", " + OracleDBUtil.SqlStr(STORE_NO) + ") from dual";
                DataTable dt = Advtek.Utility.OracleDBUtil.GetDataSet(objConn, sqlStr).Tables[0];
                if (dt != null && dt.Rows.Count > 0 && NumberUtil.IsNumeric(dt.Rows[0][0].ToString()))
                    r = Convert.ToInt32(dt.Rows[0][0]);
            }
            catch
            {
                
            }
            finally
            {
                if (objConn.State == ConnectionState.Open)
                    objConn.Close();
                objConn.Dispose();
                OracleConnection.ClearAllPools();
            }
            return r;
        }
        /// <summary>
        /// 加減庫存量資料 
        /// </summary>
        /// <param name="PRODNO"></param>
        /// <param name="QTY"></param>
        /// <returns></returns>
        public int MODI_INV_ON_HAND_CURRENT(string PRODNO, int QTY)
        {
            int r = 0;
            return r;
        }

        #region 存取銷貨單
        public DataTable getSale_Head(string POSUUID_MASTER)
        {
            //string sqlStr = @"SELECT '' VOUCHER_TYPE,'' HOST_ID, h.* FROM  SALE_HEAD h WHERE POSUUID_MASTER = '" + POSUUID_MASTER + "'";
            //Tina 2010/12/20：將join VOUCHER_RELATION 從 INNER 改成 LEFT, 避免在取得CATCH資料時, SELECT 無資料,由於CATCH資料尚未產生發票或收據.

            string sqlStr = @" SELECT VR.VOUCHER_TYPE AS VOUCHER_TYPE
                                    , '' HOST_ID
                                    , H.*  
                                    , DECODE(VR.VOUCHER_TYPE,'1',IH.INVOICE_NO,'2',MIH.INVOICE_NO,'3',RH.RECEIPT_NO) AS INVOICE_NO 
                                    , DECODE(VR.VOUCHER_TYPE,'1',IH.INVOICE_DATE,'2',MIH.INVOICE_DATE,'3',RH.INVOICE_DATE) AS INVOICE_DATE
                                    , DECODE(VR.VOUCHER_TYPE,'1','發票','2',MIH.INVOICE_TYPE,'3',RH.RECEIPT_TYPE) AS INVOICE_TYPE 
                                FROM SALE_HEAD H, VOUCHER_RELATION VR, MANUAL_INVOICE_HEAD MIH, INVOICE_HEAD IH, RECEIPT_HEAD RH 
                                Where H.POSUUID_MASTER = VR.SALE_HEAD_ID(+)  
                                And H.POSUUID_MASTER = MIH.POSUUID_MASTER(+) 
                                And H.POSUUID_MASTER = IH.POSUUID_MASTER(+)  
                                And H.POSUUID_MASTER = RH.POSUUID_MASTER(+)  
                                And H.POSUUID_MASTER = " + OracleDBUtil.SqlStr(POSUUID_MASTER);

            DataTable dt = OracleDBUtil.Query_Data(sqlStr);
            return dt;
        }

        public void InsertSale_Head(DataTable tHead)
        {
            if (tHead.Rows.Count > 0)
            {
                DataRow tHeadRow = tHead.Rows[0];
                SAL01_SALE_DTO.SALE_HEADDataTable head = new SAL01_SALE_DTO.SALE_HEADDataTable();
                SAL01_SALE_DTO.SALE_HEADRow headRow = head.NewSALE_HEADRow();
                headRow["POSUUID_MASTER"] = tHeadRow["POSUUID_MASTER"];
                headRow["SALE_STATUS"] = tHeadRow["SALE_STATUS"];
                headRow["SALE_TYPE"] = tHeadRow["SALE_TYPE"];
                headRow["SALE_NO"] = tHeadRow["SALE_NO"];
                headRow["STORE_NO"] = tHeadRow["STORE_NO"];
                headRow["STORE_NAME"] = tHeadRow["STORE_NAME"];
                headRow["MACHINE_ID"] = tHeadRow["MACHINE_ID"];
                headRow["TRADE_DATE"] = tHeadRow["TRADE_DATE"];
                headRow["UNI_TITLE"] = tHeadRow["UNI_TITLE"];
                headRow["UNI_NO"] = tHeadRow["UNI_NO"];
                headRow["SALE_BEFORE_TAX"] = tHeadRow["SALE_BEFORE_TAX"];//稅前金額+  123
                headRow["SALE_TAX"] = tHeadRow["SALE_TAX"];//稅額
                headRow["SALE_TOTAL_AMOUNT"] = tHeadRow["SALE_TOTAL_AMOUNT"];//稅後金額

                headRow["DISCOUNT_BEFORE_TAX"] = tHeadRow["DISCOUNT_BEFORE_TAX"];//折扣稅前金額
                headRow["DISCOUNT_TAX"] = tHeadRow["DISCOUNT_TAX"];  //折扣稅額
                headRow["DISCOUNT_TOTAL_AMOUNT"] = tHeadRow["DISCOUNT_TOTAL_AMOUNT"];//折扣稅後金額
                headRow["HG_PRODNO"] = tHeadRow["HG_PRODNO"];//HappyGo累點料號
                headRow["HG_CARD_NO"] = tHeadRow["HG_CARD_NO"];//Happy Go卡號
                headRow["HG_AWARD_POINT"] = tHeadRow["HG_AWARD_POINT"];//Happy Go累點點數
                headRow["HG_REMAIN_POINT"] = tHeadRow["HG_REMAIN_POINT"];//Happy Go剩餘點數
                headRow["INVALID_ID"] = tHeadRow["INVALID_ID"];//此銷貨單對應的上一張作廢的POSUUID_MASTER
                headRow["INVALID_DATE"] = tHeadRow["INVALID_DATE"];//作廢日期
                headRow["INVALID_REASON"] = tHeadRow["INVALID_REASON"];//
                headRow["ORIGINAL_ID"] = tHeadRow["ORIGINAL_ID"];//最原始的ID, 從未作廢的會與ID同，退換貨時會一路帶下來
                headRow["SALE_PERSON"] = tHeadRow["SALE_PERSON"];//銷售人員
                headRow["CREATE_USER"] = tHeadRow["CREATE_USER"];//新增人員，顯示交易維護人員員工編號及姓名，由系統自動代入。
                headRow["CREATE_DTM"] = tHeadRow["CREATE_DTM"];//
                headRow["MODI_USER"] = tHeadRow["MODI_USER"];//
                headRow["MODI_DTM"] = tHeadRow["MODI_DTM"];//
                headRow["REMARK"] = tHeadRow["REMARK"]; //備註
                if (tHead.Columns.Contains("PAPER_AUTH_CODE") && tHeadRow["PAPER_AUTH_CODE"] != null)    //紙本授權碼
                    headRow["PAPER_AUTH_CODE"] = tHeadRow["PAPER_AUTH_CODE"];
                if (tHead.Columns.Contains("rental") && tHeadRow["rental"] != null)    //月租金
                    headRow["rental"] = tHeadRow["rental"];
                if (tHead.Columns.Contains("PAPER_AUTH_ACTIVE_DATE") && tHeadRow["PAPER_AUTH_ACTIVE_DATE"] != null)    //紙本授權啟用日期
                    headRow["PAPER_AUTH_ACTIVE_DATE"] = tHeadRow["PAPER_AUTH_ACTIVE_DATE"];
                if (tHead.Columns.Contains("INVOICE_TOTAL_AMOUNT") && tHeadRow["INVOICE_TOTAL_AMOUNT"] != null)    //發票金額
                    headRow["INVOICE_TOTAL_AMOUNT"] = tHeadRow["INVOICE_TOTAL_AMOUNT"];
                head.AddSALE_HEADRow(headRow);
                head.AcceptChanges();

                OracleConnection objConn = OracleDBUtil.GetConnection();
                OracleTransaction objTX = objConn.BeginTransaction();
                try
                {
                    OracleDBUtil.Insert(head);
                    objTX.Commit();
                }
                catch (Exception ex)
                {
                    objTX.Rollback();
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
        }

        public void InsertSale_Head(DataTable tHead, OracleTransaction objTX)
        {
            if (tHead.Rows.Count > 0)
            {
                DataRow tHeadRow = tHead.Rows[0];
                SAL01_SALE_DTO.SALE_HEADDataTable head = new SAL01_SALE_DTO.SALE_HEADDataTable();
                SAL01_SALE_DTO.SALE_HEADRow headRow = head.NewSALE_HEADRow();
                headRow["POSUUID_MASTER"] = tHeadRow["POSUUID_MASTER"];
                headRow["SALE_STATUS"] = tHeadRow["SALE_STATUS"];
                headRow["SALE_TYPE"] = tHeadRow["SALE_TYPE"];
                headRow["STORE_NO"] = tHeadRow["STORE_NO"];
                headRow["STORE_NAME"] = tHeadRow["STORE_NAME"];
                headRow["MACHINE_ID"] = tHeadRow["MACHINE_ID"];
                headRow["TRADE_DATE"] = tHeadRow["TRADE_DATE"];
                headRow["UNI_TITLE"] = tHeadRow["UNI_TITLE"];
                headRow["UNI_NO"] = tHeadRow["UNI_NO"];
                headRow["SALE_BEFORE_TAX"] = tHeadRow["SALE_BEFORE_TAX"];//稅前金額+  123
                headRow["SALE_TAX"] = tHeadRow["SALE_TAX"];//稅額
                headRow["SALE_TOTAL_AMOUNT"] = tHeadRow["SALE_TOTAL_AMOUNT"];//稅後金額

                headRow["DISCOUNT_BEFORE_TAX"] = tHeadRow["DISCOUNT_BEFORE_TAX"];//折扣稅前金額
                headRow["DISCOUNT_TAX"] = tHeadRow["DISCOUNT_TAX"];  //折扣稅額
                headRow["DISCOUNT_TOTAL_AMOUNT"] = tHeadRow["DISCOUNT_TOTAL_AMOUNT"];//折扣稅後金額
                headRow["HG_PRODNO"] = tHeadRow["HG_PRODNO"];//HappyGo累點料號
                headRow["HG_CARD_NO"] = tHeadRow["HG_CARD_NO"];//Happy Go卡號
                headRow["HG_AWARD_POINT"] = tHeadRow["HG_AWARD_POINT"];//Happy Go累點點數
                headRow["HG_REMAIN_POINT"] = tHeadRow["HG_REMAIN_POINT"];//Happy Go剩餘點數
                headRow["INVALID_ID"] = tHeadRow["INVALID_ID"];//此銷貨單對應的上一張作廢的POSUUID_MASTER
                headRow["INVALID_DATE"] = tHeadRow["INVALID_DATE"];//作廢日期
                headRow["INVALID_REASON"] = tHeadRow["INVALID_REASON"];//
                headRow["ORIGINAL_ID"] = tHeadRow["ORIGINAL_ID"];//最原始的ID, 從未作廢的會與ID同，退換貨時會一路帶下來
                headRow["SALE_PERSON"] = tHeadRow["SALE_PERSON"];//銷售人員
                headRow["CREATE_USER"] = tHeadRow["CREATE_USER"];//新增人員，顯示交易維護人員員工編號及姓名，由系統自動代入。
                headRow["CREATE_DTM"] = tHeadRow["CREATE_DTM"];//
                headRow["MODI_USER"] = tHeadRow["MODI_USER"];//
                headRow["MODI_DTM"] = tHeadRow["MODI_DTM"];//
                headRow["REMARK"] = tHeadRow["REMARK"]; //備註
                if (tHead.Columns.Contains("PAPER_AUTH_CODE") && tHeadRow["PAPER_AUTH_CODE"] != null)    //紙本授權碼
                    headRow["PAPER_AUTH_CODE"] = tHeadRow["PAPER_AUTH_CODE"];
                if (tHead.Columns.Contains("rental") && tHeadRow["rental"] != null)    //月租金
                    headRow["rental"] = tHeadRow["rental"];
                if (tHead.Columns.Contains("PAPER_AUTH_ACTIVE_DATE") && tHeadRow["PAPER_AUTH_ACTIVE_DATE"] != null)    //紙本授權啟用日期
                    headRow["PAPER_AUTH_ACTIVE_DATE"] = tHeadRow["PAPER_AUTH_ACTIVE_DATE"];
                if (tHead.Columns.Contains("INVOICE_TOTAL_AMOUNT") && tHeadRow["INVOICE_TOTAL_AMOUNT"] != null)    //發票金額
                    headRow["INVOICE_TOTAL_AMOUNT"] = tHeadRow["INVOICE_TOTAL_AMOUNT"];
                head.AddSALE_HEADRow(headRow);
                head.AcceptChanges();

                try
                {
                    OracleDBUtil.Insert(objTX, head);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public void DeleteSale_Head(DataTable tHead)
        {
            SAL01_SALE_DTO.SALE_HEADDataTable head = new SAL01_SALE_DTO.SALE_HEADDataTable();
            foreach (DataColumn dc in head.Columns) dc.AllowDBNull = true;
            if (tHead.Rows.Count > 0)
            {
                SAL01_SALE_DTO.SALE_HEADRow headRow = head.NewSALE_HEADRow();
                headRow.POSUUID_MASTER = tHead.Rows[0]["POSUUID_MASTER"].ToString();
                head.AddSALE_HEADRow(headRow);
                head.AcceptChanges();
                OracleConnection objConn = OracleDBUtil.GetConnection();
                OracleTransaction objTX = objConn.BeginTransaction();
                try
                {
                    //OracleDBUtil.DELETEByUUID(head, "POSUUID_MASTER");
                    string sqlStr = " Delete From " + head.TableName + " Where POSUUID_MASTER = " +
                                        OracleDBUtil.SqlStr(tHead.Rows[0]["POSUUID_MASTER"].ToString());
                    if (OracleDBUtil.ExecuteSql(objTX, sqlStr) < 0)
                    {
                        objTX.Rollback();
                        return;
                    }
                    objTX.Commit();
                }
                catch (Exception ex)
                {
                    objTX.Rollback();
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
        }

        public void DeleteSale_Head(DataTable tHead, OracleTransaction objTX)
        {
            SAL01_SALE_DTO.SALE_HEADDataTable head = new SAL01_SALE_DTO.SALE_HEADDataTable();
            foreach (DataColumn dc in head.Columns) dc.AllowDBNull = true;
            if (tHead.Rows.Count > 0)
            {
                SAL01_SALE_DTO.SALE_HEADRow headRow = head.NewSALE_HEADRow();
                headRow.POSUUID_MASTER = tHead.Rows[0]["POSUUID_MASTER"].ToString();
                head.AddSALE_HEADRow(headRow);
                head.AcceptChanges();

                try
                {
                    //OracleDBUtil.DELETEByUUID(head, "POSUUID_MASTER");
                    string sqlStr = " Delete From " + head.TableName + " Where POSUUID_MASTER = " +
                                        OracleDBUtil.SqlStr(tHead.Rows[0]["POSUUID_MASTER"].ToString());
                    if (OracleDBUtil.ExecuteSql(objTX, sqlStr) < 0)
                    {
                        return;
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public DataTable getSale_Detail(string POSUUID_MASTER, string ITEM_TYPE)
        {
            string sqlStr = @"SELECT ROWNUM ITEMS,0 IMEI_QTY,
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
                                 P.PRODNAME,
                                 NVL(P.ISSTOCK,0) ISSTOCK,
                                 '' IMEI, P.IS_OPEN_PRICE, 
                             d.* FROM  SALE_DETAIL d ,
                             (Select * from MM Where Sysdate Between NVL(B_DATE, to_date('2000/01/01','YYYY/MM/DD')) And 
                                NVL(E_DATE,to_date('3000/01/01','YYYY/MM/DD')) And PROMO_STATUS = '1') MM, PRODUCT P 
                          WHERE d.PROMOTION_CODE = MM.PROMO_NO(+)  
                            AND d.PRODNO = P.PRODNO(+)    
                            AND POSUUID_MASTER = '" + POSUUID_MASTER + "'";


            string where = "";
            if (ITEM_TYPE != "") //未結折扣資料要另外放
            {
                ITEM_TYPE = "'" + ITEM_TYPE.Replace(",", "','") + "'";
                where += " AND ITEM_TYPE IN(" + ITEM_TYPE + ")";
            }
            sqlStr += where;

            DataTable dt = OracleDBUtil.Query_Data(sqlStr);
            return dt;
        }

        public DataTable getSale_Detail(string POSUUID_MASTER, string ITEM_TYPE, string STORE_NO, string STOCK)
        {
            string sqlStr = @"SELECT ROWNUM ITEMS,0 IMEI_QTY,
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
                                     P.PRODNAME, P.INV_TYPE, 
                                     NVL(P.ISSTOCK,0) ISSTOCK,
                                     '' IMEI, I.ON_HAND_QTY, P.IS_OPEN_PRICE,  
                                     d.* 
                             FROM  SALE_DETAIL d, 
                                   (Select * from MM Where Sysdate Between NVL(B_DATE, to_date('2000/01/01','YYYY/MM/DD')) And NVL(E_DATE,to_date('3000/01/01','YYYY/MM/DD')) And PROMO_STATUS = '1') MM, 
                                   PRODUCT P, 
                                   (Select * from INV_ON_HAND_CURRENT Where STOCK_ID = " + OracleDBUtil.SqlStr(STOCK) + " And STORE_NO = " + OracleDBUtil.SqlStr(STORE_NO) + ") I "
                         + " WHERE d.PROMOTION_CODE = MM.PROMO_NO(+) AND d.PRODNO = P.PRODNO(+) AND d.PRODNO = I.PRODNO(+) "
                         + " AND POSUUID_MASTER = " + OracleDBUtil.SqlStr(POSUUID_MASTER);
            string where = "";
            if (ITEM_TYPE != "") //未結折扣資料要另外放
            {
                ITEM_TYPE = "'" + ITEM_TYPE.Replace(",", "','") + "'";
                where += " AND ITEM_TYPE IN(" + ITEM_TYPE + ")";
            }
            sqlStr += where;

            DataTable dt = OracleDBUtil.Query_Data(sqlStr);
            return dt;
        }

        /// <summary>
        /// 判斷SALE_IMEI_LOG中是否有交易已經被捨棄的IMEI紀錄(SALE_DETAIL_ID存在於SALE_IMEI_LOG中,而不存在於SALE_DETAIL中) 
        /// </summary>
        /// <param name="IMEI">IMEI值</param>
        /// <returns>true/false</returns>
        public bool haveGarbageIMEIRec(string IMEI)
        {
            string sqlStr = @"Select SALE_DETAIL_ID From SALE_IMEI_LOG Where SALE_STATUS = '1' AND IMEI = " + OracleDBUtil.SqlStr(IMEI)
                            + " And to_char(sysdate, 'YYYY-MM-DD HH24:MI:SS') > to_char(CREATE_DTM + interval '1' hour, 'YYYY-MM-DD HH24:MI:SS')";

            OracleConnection objConn = null;
            bool ret = false;
            try
            {
                objConn = OracleDBUtil.GetConnection();
                DataTable dt = OracleDBUtil.GetDataSet(objConn, sqlStr).Tables[0];
                if (dt != null && dt.Rows.Count > 0)
                {
                    sqlStr = @"SELECT ID From SALE_DETAIL Where ID in (Select SALE_DETAIL_ID From SALE_IMEI_LOG Where SALE_STATUS = '1' AND IMEI = " 
                                + OracleDBUtil.SqlStr(IMEI)
                                + " And to_char(sysdate, 'YYYY-MM-DD HH24:MI:SS') > to_char(CREATE_DTM + interval '1' hour, 'YYYY-MM-DD HH24:MI:SS'))";
                    DataTable dtDetail = OracleDBUtil.GetDataSet(objConn, sqlStr).Tables[0];
                    if (dtDetail == null || dtDetail.Rows.Count == 0)
                        ret = true;
                }
            }
            catch //(Exception ex)
            {
                
            }
            finally
            {
                if (objConn.State == ConnectionState.Open)
                    objConn.Close();
                objConn.Dispose();
                OracleConnection.ClearAllPools();
            }
            return ret;
        }

        public void InsertSale_Detail(DataTable tDetail)
        {
            SAL01_SALE_DTO.SALE_DETAILDataTable Detail = new SAL01_SALE_DTO.SALE_DETAILDataTable();
            foreach (DataRow tDetailRow in tDetail.Rows)
            {
                SAL01_SALE_DTO.SALE_DETAILRow DetailRow = Detail.NewSALE_DETAILRow();
                #region 放值到欄位去
                DetailRow["ID"] = tDetailRow["ID"];
                DetailRow["POSUUID_DETAIL"] = tDetailRow["POSUUID_DETAIL"];
                /*ITEM_TYPE
                 *  1: 單品   2: 服務系統(組合促銷等來自於未結明細的資料)3: 預購轉銷售4: Happy Go折抵 5: 銷售折扣(來自於未結明細的資料)
                    6: 店長折扣 從 未結轉入時: 2, 5*/
                DetailRow["ITEM_TYPE"] = tDetailRow["ITEM_TYPE"];
                /*SOURCE_TYPE
                 * 1: IA: IMAGE_ID 2: Loyalty: IMAGE_ID 3: Pa yment: SYSTEM_KEY 4: SSI: IMAGE_ID5: OLR: SYSTEM_KEY
                6: HRS: 不用回押，memo維修單號 7: DMS: 不用回押 8: ETC: 不用回押  9: NCIC: 不用回押
                10: eStore: PACKAGE_NO 11: POS  12: 預購轉銷售 從未結轉入時: [to_close_head].service_type
                 */
                DetailRow["SOURCE_TYPE"] = tDetailRow["SOURCE_TYPE"];
                DetailRow["SOURCE_REFERENCE_KEY"] = tDetailRow["SOURCE_REFERENCE_KEY"];//從未結轉入時(source_type in (1..10)):當item_type = 1時為[to_close_item].id當item_type = 2時為[to_close_discount].id其餘為null
                DetailRow["POSUUID_MASTER"] = tDetailRow["POSUUID_MASTER"];//
                DetailRow["SEQNO"] = tDetailRow["SEQNO"];//
                DetailRow["PRODNO"] = tDetailRow["PRODNO"];//
                DetailRow["TAXABLE"] = tDetailRow["TAXABLE"];//課稅別，由prodno帶入[product].taxable1:應稅2:免稅3:零稅，應稅但taxrate 0
                DetailRow["TAXRATE"] = tDetailRow["TAXRATE"];//稅率，由prodno帶入[product].taxableTAXRATE時5%記錄為5
                DetailRow["UNIT_PRICE"] = tDetailRow["UNIT_PRICE"];//單價
                DetailRow["QUANTITY"] = tDetailRow["QUANTITY"];//數量，商品、銷售折扣、店長折扣、Happy Go折抵由未結轉入時一律為1
                DetailRow["BEFORE_TAX"] = tDetailRow["BEFORE_TAX"];//稅前金額，商品、銷售折扣、店長折扣、Happy Go折抵由稅額計算規則算出
                DetailRow["TAX"] = tDetailRow["TAX"];//稅額，商品、銷售折扣、店長折扣、Happy Go折抵由稅額計算規則算出
                DetailRow["TOTAL_AMOUNT"] = tDetailRow["TOTAL_AMOUNT"];//稅後金額，商品、銷售折扣、店長折扣、Happy Go折抵QUANTITY*UNIT_PRICE
                DetailRow["PROMOTION_CODE"] = tDetailRow["PROMOTION_CODE"];//促代編號，若非組合促銷之商品，此欄位顯示空白。從未結轉入時:[to_close_item].promotion_code
                DetailRow["HG_CARD_NO"] = tDetailRow["HG_CARD_NO"];//HappyGo卡號
                DetailRow["HG_REDEEM_POINT"] = tDetailRow["HG_REDEEM_POINT"];//HG兌換點數
                DetailRow["HG_LEVEL_PRICE"] = tDetailRow["HG_LEVEL_PRICE"];//HG層級價格
                DetailRow["HG_RULE"] = tDetailRow["HG_RULE"];//Happy Go選用規則
                DetailRow["SH_DISCOUNT_RATE"] = tDetailRow["SH_DISCOUNT_RATE"];//店長折扣折抵比率, 交易折抵比率；90表應收總金額*90%
                DetailRow["SH_DISCOUNT_REASON"] = tDetailRow["SH_DISCOUNT_REASON"];//店長折扣折抵原因, 下拉選單顯示折抵原因供使用者選取
                DetailRow["SH_DISCOUNT_DESC"] = tDetailRow["SH_DISCOUNT_DESC"];//店長折扣折抵原因內容
                DetailRow["WRITE_OFF_FILE"] = tDetailRow["WRITE_OFF_FILE"];//scheduling job產生的代收檔名
                DetailRow["WRITE_OFF_DATE"] = tDetailRow["WRITE_OFF_DATE"];//產生的代收檔scheduling job執行時間
                DetailRow["BATCH_NO"] = tDetailRow["BATCH_NO"];//產生的代收檔scheduling job的ID
                DetailRow["APPLY_DATE"] = tDetailRow["APPLY_DATE"];//申請日期從未結轉入時: [to_close_head].apply_date

                /*SERVICE_SYS_ID
                 * image_id, system_key, package_no ...
                    銷貨結帳後，用來回押服務系統的key值
                    從未結轉入時: [to_close_head].service_sys_id
                    IA: IMAGE_ID
                    Loyalty: IMAGE_ID
                    Payment: SYSTEM_KEY
                    SSI: IMAGE_ID
                    OLR: SYSTEM_KEY
                    HRS: 不用回押，memo維修單號
                    DMS: 不用回押
                    ETC: 不用回押
                    NCIC: 不用回押
                    eStore: PACKAGE_NO
                 */
                DetailRow["SERVICE_SYS_ID"] = tDetailRow["SERVICE_SYS_ID"];//
                DetailRow["BUNDLE_ID"] = tDetailRow["BUNDLE_ID"];//BUNDLE_ID從未結轉入時: [to_close_head].bundle_id
                DetailRow["BUNDLE_TYPE"] = tDetailRow["BUNDLE_TYPE"];//從未結轉入時: [to_close_head].bundle_type
                DetailRow["MNP"] = tDetailRow["MNP"];//Y: yesN: no從未結轉入時: [to_close_head].MNP
                DetailRow["HRS_NO"] = tDetailRow["HRS_NO"];//維修單號，HRS memo從未結轉入時: [to_close_head].hrs_no
                DetailRow["LEASE_NO"] = tDetailRow["LEASE_NO"];//租賃單號，memo從未結轉入時: [to_close_head].lease_no
                DetailRow["ORDER_NO"] = tDetailRow["ORDER_NO"];//從未結轉入時: [to_close_head].order_no
                DetailRow["ETC_NO"] = tDetailRow["ETC_NO"];//ETC卡號, ETC memo從未結轉入時: [to_close_item].etc_no
                /*FUN_ID
                 * //從未結轉入時: [to_close_head].fun_id
                    IA:
                    IA_Closing, 扣庫存，需回押
                    IA_nonClosing, 扣庫存，不需回押
                    代收時，FUN_ID由POS自行定義
                    1: 遠傳帳單
                    2: 和信帳單
                    3: Seednet帳單
                    4: 遠通帳單(有單)
                    5: 遠通帳單(無單)
                    6: 速博帳單*/
                DetailRow["FUN_ID"] = tDetailRow["FUN_ID"];
                DetailRow["R_RATE"] = tDetailRow["R_RATE"];//從未結轉入時: [to_close_head].r_rate
                DetailRow["DATA"] = tDetailRow["DATA"];//Y/N從未結轉入時: [to_close_head].data
                DetailRow["VOICE"] = tDetailRow["VOICE"];//Y/N從未結轉入時: [to_close_head].voice
                DetailRow["TRANS_TYPE"] = tDetailRow["TRANS_TYPE"];//從未結轉入時: [to_close_head].trans_type
                DetailRow["APPROVE_STATUS"] = tDetailRow["APPROVE_STATUS"];//unapprove: 未審核approved: 已審核nonapprove: 不須審核
                DetailRow["APPROVAL_DATE"] = tDetailRow["APPROVAL_DATE"];//
                DetailRow["CONN_FLAG"] = tDetailRow["CONN_FLAG"];//從未結轉入時: [to_close_head].conn_flag
                DetailRow["QUERY_TABLE_NAME"] = tDetailRow["QUERY_TABLE_NAME"];//從未結轉入時: [to_close_head].query_table
                DetailRow["BARCODE1"] = tDetailRow["BARCODE1"];//代收memo從未結轉入時: [to_close_item].barcode1
                DetailRow["BARCODE2"] = tDetailRow["BARCODE2"];//
                DetailRow["BARCODE3"] = tDetailRow["BARCODE3"];//
                DetailRow["SIM_CARD_NO"] = tDetailRow["SIM_CARD_NO"];//從未結轉入時: [to_close_item].sim_card_no
                DetailRow["MSISDN"] = tDetailRow["MSISDN"];//從未結轉入時: [to_close_item].msisdn
                DetailRow["MSISDN_TYPE"] = tDetailRow["MSISDN_TYPE"];//門號類型1. prepaid 2. postpaid'
                DetailRow["DISCOUNT_B_DATE"] = tDetailRow["DISCOUNT_B_DATE"];//從未結轉入時: [to_close_discount].discount_b_date
                DetailRow["DISCOUNT_E_DATE"] = tDetailRow["DISCOUNT_E_DATE"];//從未結轉入時: [to_close_discount].discount_e_date
                DetailRow["CREATE_USER"] = tDetailRow["CREATE_USER"];//
                DetailRow["CREATE_DTM"] = tDetailRow["CREATE_DTM"];//
                DetailRow["MODI_USER"] = tDetailRow["MODI_USER"];//
                DetailRow["MODI_DTM"] = tDetailRow["MODI_DTM"];//
                if (tDetail.Columns.Contains("GA_TYPE") && tDetailRow["GA_TYPE"] != null)  //GA
                    DetailRow["GA_TYPE"] = tDetailRow["GA_TYPE"];
                if (tDetail.Columns.Contains("LOY_TYPE") && tDetailRow["LOY_TYPE"] != null)     //LOYALTY
                    DetailRow["LOY_TYPE"] = tDetailRow["LOY_TYPE"];
                if (tDetail.Columns.Contains("TWO_TO_THREE_TYPE") && tDetailRow["TWO_TO_THREE_TYPE"] != null)    //2 to 3
                    DetailRow["TWO_TO_THREE_TYPE"] = tDetailRow["TWO_TO_THREE_TYPE"];
                #endregion
                Detail.AddSALE_DETAILRow(DetailRow);
            }
            Detail.AcceptChanges();
            OracleConnection objConn = OracleDBUtil.GetConnection();
            OracleTransaction objTX = objConn.BeginTransaction();
            try
            {
                OracleDBUtil.Insert(Detail);
                objTX.Commit();
            }
            catch (Exception ex)
            {
                objTX.Rollback();
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

        public void InsertSale_Detail(DataTable tDetail, OracleTransaction objTX)
        {
            SAL01_SALE_DTO.SALE_DETAILDataTable Detail = new SAL01_SALE_DTO.SALE_DETAILDataTable();
            foreach (DataRow tDetailRow in tDetail.Rows)
            {
                SAL01_SALE_DTO.SALE_DETAILRow DetailRow = Detail.NewSALE_DETAILRow();
                #region 放值到欄位去
                DetailRow["ID"] = tDetailRow["ID"];
                DetailRow["POSUUID_DETAIL"] = tDetailRow["POSUUID_DETAIL"];
                /*ITEM_TYPE
                 *  1: 單品   2: 服務系統(組合促銷等來自於未結明細的資料)3: 預購轉銷售4: Happy Go折抵 5: 銷售折扣(來自於未結明細的資料)
                    6: 店長折扣 從 未結轉入時: 2, 5*/
                DetailRow["ITEM_TYPE"] = tDetailRow["ITEM_TYPE"];
                /*SOURCE_TYPE
                 * 1: IA: IMAGE_ID 2: Loyalty: IMAGE_ID 3: Pa yment: SYSTEM_KEY 4: SSI: IMAGE_ID5: OLR: SYSTEM_KEY
                6: HRS: 不用回押，memo維修單號 7: DMS: 不用回押 8: ETC: 不用回押  9: NCIC: 不用回押
                10: eStore: PACKAGE_NO 11: POS  12: 預購轉銷售 從未結轉入時: [to_close_head].service_type
                 */
                DetailRow["SOURCE_TYPE"] = tDetailRow["SOURCE_TYPE"];
                DetailRow["SOURCE_REFERENCE_KEY"] = tDetailRow["SOURCE_REFERENCE_KEY"];//從未結轉入時(source_type in (1..10)):當item_type = 1時為[to_close_item].id當item_type = 2時為[to_close_discount].id其餘為null
                DetailRow["POSUUID_MASTER"] = tDetailRow["POSUUID_MASTER"];//
                DetailRow["SEQNO"] = tDetailRow["SEQNO"];//
                DetailRow["PRODNO"] = tDetailRow["PRODNO"];//
                DetailRow["TAXABLE"] = tDetailRow["TAXABLE"];//課稅別，由prodno帶入[product].taxable1:應稅2:免稅3:零稅，應稅但taxrate 0
                DetailRow["TAXRATE"] = tDetailRow["TAXRATE"];//稅率，由prodno帶入[product].taxableTAXRATE時5%記錄為5
                DetailRow["UNIT_PRICE"] = tDetailRow["UNIT_PRICE"];//單價
                DetailRow["QUANTITY"] = tDetailRow["QUANTITY"];//數量，商品、銷售折扣、店長折扣、Happy Go折抵由未結轉入時一律為1
                DetailRow["BEFORE_TAX"] = tDetailRow["BEFORE_TAX"];//稅前金額，商品、銷售折扣、店長折扣、Happy Go折抵由稅額計算規則算出
                DetailRow["TAX"] = tDetailRow["TAX"];//稅額，商品、銷售折扣、店長折扣、Happy Go折抵由稅額計算規則算出
                DetailRow["TOTAL_AMOUNT"] = tDetailRow["TOTAL_AMOUNT"];//稅後金額，商品、銷售折扣、店長折扣、Happy Go折抵QUANTITY*UNIT_PRICE
                DetailRow["PROMOTION_CODE"] = tDetailRow["PROMOTION_CODE"];//促代編號，若非組合促銷之商品，此欄位顯示空白。從未結轉入時:[to_close_item].promotion_code
                DetailRow["HG_CARD_NO"] = tDetailRow["HG_CARD_NO"];//HappyGo卡號
                DetailRow["HG_REDEEM_POINT"] = tDetailRow["HG_REDEEM_POINT"];//HG兌換點數
                DetailRow["HG_LEVEL_PRICE"] = tDetailRow["HG_LEVEL_PRICE"];//HG層級價格
                DetailRow["HG_RULE"] = tDetailRow["HG_RULE"];//Happy Go選用規則
                DetailRow["SH_DISCOUNT_RATE"] = tDetailRow["SH_DISCOUNT_RATE"];//店長折扣折抵比率, 交易折抵比率；90表應收總金額*90%
                DetailRow["SH_DISCOUNT_REASON"] = tDetailRow["SH_DISCOUNT_REASON"];//店長折扣折抵原因, 下拉選單顯示折抵原因供使用者選取
                DetailRow["SH_DISCOUNT_DESC"] = tDetailRow["SH_DISCOUNT_DESC"];//店長折扣折抵原因內容
                DetailRow["WRITE_OFF_FILE"] = tDetailRow["WRITE_OFF_FILE"];//scheduling job產生的代收檔名
                DetailRow["WRITE_OFF_DATE"] = tDetailRow["WRITE_OFF_DATE"];//產生的代收檔scheduling job執行時間
                DetailRow["BATCH_NO"] = tDetailRow["BATCH_NO"];//產生的代收檔scheduling job的ID
                DetailRow["APPLY_DATE"] = tDetailRow["APPLY_DATE"];//申請日期從未結轉入時: [to_close_head].apply_date

                /*SERVICE_SYS_ID
                 * image_id, system_key, package_no ...
                    銷貨結帳後，用來回押服務系統的key值
                    從未結轉入時: [to_close_head].service_sys_id
                    IA: IMAGE_ID
                    Loyalty: IMAGE_ID
                    Payment: SYSTEM_KEY
                    SSI: IMAGE_ID
                    OLR: SYSTEM_KEY
                    HRS: 不用回押，memo維修單號
                    DMS: 不用回押
                    ETC: 不用回押
                    NCIC: 不用回押
                    eStore: PACKAGE_NO
                 */
                DetailRow["SERVICE_SYS_ID"] = tDetailRow["SERVICE_SYS_ID"];//
                DetailRow["BUNDLE_ID"] = tDetailRow["BUNDLE_ID"];//BUNDLE_ID從未結轉入時: [to_close_head].bundle_id
                DetailRow["BUNDLE_TYPE"] = tDetailRow["BUNDLE_TYPE"];//從未結轉入時: [to_close_head].bundle_type
                DetailRow["MNP"] = tDetailRow["MNP"];//Y: yesN: no從未結轉入時: [to_close_head].MNP
                DetailRow["HRS_NO"] = tDetailRow["HRS_NO"];//維修單號，HRS memo從未結轉入時: [to_close_head].hrs_no
                DetailRow["LEASE_NO"] = tDetailRow["LEASE_NO"];//租賃單號，memo從未結轉入時: [to_close_head].lease_no
                DetailRow["ORDER_NO"] = tDetailRow["ORDER_NO"];//從未結轉入時: [to_close_head].order_no
                DetailRow["ETC_NO"] = tDetailRow["ETC_NO"];//ETC卡號, ETC memo從未結轉入時: [to_close_item].etc_no
                /*FUN_ID
                 * //從未結轉入時: [to_close_head].fun_id
                    IA:
                    IA_Closing, 扣庫存，需回押
                    IA_nonClosing, 扣庫存，不需回押
                    代收時，FUN_ID由POS自行定義
                    1: 遠傳帳單
                    2: 和信帳單
                    3: Seednet帳單
                    4: 遠通帳單(有單)
                    5: 遠通帳單(無單)
                    6: 速博帳單*/
                DetailRow["FUN_ID"] = tDetailRow["FUN_ID"];
                DetailRow["R_RATE"] = tDetailRow["R_RATE"];//從未結轉入時: [to_close_head].r_rate
                DetailRow["DATA"] = tDetailRow["DATA"];//Y/N從未結轉入時: [to_close_head].data
                DetailRow["VOICE"] = tDetailRow["VOICE"];//Y/N從未結轉入時: [to_close_head].voice
                DetailRow["TRANS_TYPE"] = tDetailRow["TRANS_TYPE"];//從未結轉入時: [to_close_head].trans_type
                DetailRow["APPROVE_STATUS"] = tDetailRow["APPROVE_STATUS"];//unapprove: 未審核approved: 已審核nonapprove: 不須審核
                DetailRow["APPROVAL_DATE"] = tDetailRow["APPROVAL_DATE"];//
                DetailRow["CONN_FLAG"] = tDetailRow["CONN_FLAG"];//從未結轉入時: [to_close_head].conn_flag
                DetailRow["QUERY_TABLE_NAME"] = tDetailRow["QUERY_TABLE_NAME"];//從未結轉入時: [to_close_head].query_table
                DetailRow["BARCODE1"] = tDetailRow["BARCODE1"];//代收memo從未結轉入時: [to_close_item].barcode1
                DetailRow["BARCODE2"] = tDetailRow["BARCODE2"];//
                DetailRow["BARCODE3"] = tDetailRow["BARCODE3"];//
                DetailRow["SIM_CARD_NO"] = tDetailRow["SIM_CARD_NO"];//從未結轉入時: [to_close_item].sim_card_no
                DetailRow["MSISDN"] = tDetailRow["MSISDN"];//從未結轉入時: [to_close_item].msisdn
                DetailRow["MSISDN_TYPE"] = tDetailRow["MSISDN_TYPE"];//門號類型1. prepaid 2. postpaid'
                DetailRow["DISCOUNT_B_DATE"] = tDetailRow["DISCOUNT_B_DATE"];//從未結轉入時: [to_close_discount].discount_b_date
                DetailRow["DISCOUNT_E_DATE"] = tDetailRow["DISCOUNT_E_DATE"];//從未結轉入時: [to_close_discount].discount_e_date
                DetailRow["CREATE_USER"] = tDetailRow["CREATE_USER"];//
                DetailRow["CREATE_DTM"] = tDetailRow["CREATE_DTM"];//
                DetailRow["MODI_USER"] = tDetailRow["MODI_USER"];//
                DetailRow["MODI_DTM"] = tDetailRow["MODI_DTM"];//
                if (tDetail.Columns.Contains("GA_TYPE") && tDetailRow["GA_TYPE"] != null)  //GA
                    DetailRow["GA_TYPE"] = tDetailRow["GA_TYPE"];
                if (tDetail.Columns.Contains("LOY_TYPE") && tDetailRow["LOY_TYPE"] != null)     //LOYALTY
                    DetailRow["LOY_TYPE"] = tDetailRow["LOY_TYPE"];
                if (tDetail.Columns.Contains("TWO_TO_THREE_TYPE") && tDetailRow["TWO_TO_THREE_TYPE"] != null)    //2 to 3
                    DetailRow["TWO_TO_THREE_TYPE"] = tDetailRow["TWO_TO_THREE_TYPE"];
                #endregion
                Detail.AddSALE_DETAILRow(DetailRow);
            }
            Detail.AcceptChanges();
            
            try
            {
                OracleDBUtil.Insert(objTX, Detail);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void DeleteSale_Detail(DataTable tDetail)
        {
            SAL01_SALE_DTO.SALE_DETAILDataTable Detail = new SAL01_SALE_DTO.SALE_DETAILDataTable();
            foreach (DataColumn dc in Detail.Columns) dc.AllowDBNull = true;
            foreach (DataRow tDetailRow in tDetail.Rows)
            {
                SAL01_SALE_DTO.SALE_DETAILRow DetailRow = Detail.NewSALE_DETAILRow();
                DetailRow.ID = tDetailRow["ID"].ToString();
                Detail.AddSALE_DETAILRow(DetailRow);
            }
            Detail.AcceptChanges();
            OracleConnection objConn = null;
            OracleTransaction objTX = null;
            try
            {
                //OracleDBUtil.DELETEByUUID(Detail, "ID");
                objConn = OracleDBUtil.GetConnection();
                objTX = objConn.BeginTransaction();
                foreach (DataRow tDetailRow in tDetail.Rows)
                {
                    SAL01_SALE_DTO.SALE_DETAILRow DetailRow = Detail.NewSALE_DETAILRow();
                    string detailID = tDetailRow["ID"].ToString();
                    string strSql = " Delete From " + Detail.TableName.ToString() + " where ID = " + OracleDBUtil.SqlStr(detailID);
                    if (OracleDBUtil.ExecuteSql(objTX, strSql) < 0)
                    {
                        objTX.Rollback();
                        return;
                    }
                }
                objTX.Commit();
            }
            catch (Exception ex)
            {
                objTX.Rollback();
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

        public void DeleteSale_Detail(DataTable tDetail, OracleTransaction objTX)
        {
            SAL01_SALE_DTO.SALE_DETAILDataTable Detail = new SAL01_SALE_DTO.SALE_DETAILDataTable();
            foreach (DataColumn dc in Detail.Columns) dc.AllowDBNull = true;
            foreach (DataRow tDetailRow in tDetail.Rows)
            {
                SAL01_SALE_DTO.SALE_DETAILRow DetailRow = Detail.NewSALE_DETAILRow();
                DetailRow.ID = tDetailRow["ID"].ToString();
                Detail.AddSALE_DETAILRow(DetailRow);
            }
            Detail.AcceptChanges();
            
            try
            {
                //OracleDBUtil.DELETEByUUID(Detail, "ID");
                foreach (DataRow tDetailRow in tDetail.Rows)
                {
                    SAL01_SALE_DTO.SALE_DETAILRow DetailRow = Detail.NewSALE_DETAILRow();
                    string detailID = tDetailRow["ID"].ToString();
                    string strSql = " Delete From " + Detail.TableName.ToString() + " where ID = " + OracleDBUtil.SqlStr(detailID);
                    if (OracleDBUtil.ExecuteSql(objTX, strSql) < 0)
                    {
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        string PAID_MODE = @" DECODE(PAID_MODE,'1','現金',
                                               '2','信用卡',
                                               '3','離線信用卡',
                                               '4','分期付款',
                                               '5','禮劵',
                                               '6','金融卡',
                                               '7','Happy GO',
                                               '8','找零') PAID_MODE_NAME,
                            ";
        public DataTable getPaid_Detail(string POSUUID_MASTER)
        {
            //2011-02-15 新增paid_mode '8' 找零 for 分錄帳平用, 不顯示在畫面上
            string sqlStr = @"SELECT " + PAID_MODE + @"
                                     P.* 
                              FROM  PAID_DETAIL P 
                              WHERE POSUUID_MASTER = " + OracleDBUtil.SqlStr(POSUUID_MASTER) + @" 
                              And PAID_MODE != '8' ";

            DataTable dt = OracleDBUtil.Query_Data(sqlStr);
            return dt;
        }
        public DataTable getPaid_DetailByID(string ID)
        {
            string sqlStr = @"SELECT " + PAID_MODE + @"
                                     P,* 
                              FROM  P.PAID_DETAIL 
                              WHERE ID = " + OracleDBUtil.SqlStr(ID);

            DataTable dt = OracleDBUtil.Query_Data(sqlStr);
            return dt;

        }

        public void InsertPaid_Detail(DataTable tPaid)
        {
            SAL01_SALE_DTO.PAID_DETAILDataTable paid = new SAL01_SALE_DTO.PAID_DETAILDataTable();
            foreach (DataRow tPaidRow in tPaid.Rows)
            {
                SAL01_SALE_DTO.PAID_DETAILRow paidRow = paid.NewPAID_DETAILRow();
                #region 欄位值設定
                paidRow["ID"] = tPaidRow["ID"];
                paidRow["POSUUID_MASTER"] = tPaidRow["POSUUID_MASTER"];
                paidRow["DESCRIPTION"] = tPaidRow["DESCRIPTION"];
                paidRow["PAID_MODE"] = tPaidRow["PAID_MODE"];//1: 現金2: 信用卡3: 離線信用卡4: 分期付款5: 禮券6: 金融卡7: Happy Go
                paidRow["PAID_AMOUNT"] = tPaidRow["PAID_AMOUNT"];//付款金額
                paidRow["GIFT_VOUCHER_ID"] = tPaidRow["GIFT_VOUCHER_ID"];//禮券ID，對應到禮券設定裡的某種禮券
                paidRow["GIFT_VOUCHER_NO"] = tPaidRow["GIFT_VOUCHER_NO"];//禮券號碼
                paidRow["HG_CARD_NO"] = tPaidRow["HG_CARD_NO"];//HappyGo卡號
                paidRow["HG_LEFT_POINT"] = tPaidRow["HG_LEFT_POINT"];//HappyGo卡號可用剩餘點數
                /*HG_REDEEM_POINT
                 * (1).系統依HappyGo卡號取得之剩餘點數計算最大可兌點金額，顯示內容為"金額(點數)"（例：400元(1350點)）。
                    (2).系統顯示之欲兌點數，使用者可以依客戶需求選取可兌點項目變更欲兌換點數，系統依變更兌換點數顯示相對應金額。
                 */
                paidRow["HG_REDEEM_POINT"] = tPaidRow["HG_REDEEM_POINT"];//
                paidRow["HG_RULE"] = tPaidRow["HG_RULE"];//選用的兌點規則
                paidRow["DEBIT_CARD_NO"] = tPaidRow["DEBIT_CARD_NO"];//金融卡卡號                //todo: remove
                paidRow["DEBIT_CARD_AUTH_NO"] = tPaidRow["DEBIT_CARD_AUTH_NO"];//金融卡授權碼
                paidRow["CREDIT_TYPE"] = tPaidRow["CREDIT_TYPE"];//1: 一般2: 分期3: 離線
                paidRow["CREDIT_CARD_NO"] = tPaidRow["CREDIT_CARD_NO"];//信用卡號
                paidRow["CREDIT_CARD_AUTH_NO"] = tPaidRow["CREDIT_CARD_AUTH_NO"];//信用卡授權碼
                paidRow["CREDIT_INSTALLMENT"] = tPaidRow["CREDIT_INSTALLMENT"];//信用卡分期付款的期數
                paidRow["CREDIT_BANK_ID"] = tPaidRow["CREDIT_BANK_ID"];//信用卡銀行別
                paidRow["CREATE_USER"] = tPaidRow["CREATE_USER"];//
                paidRow["CREATE_DTM"] = tPaidRow["CREATE_DTM"];//
                paidRow["MODI_USER"] = tPaidRow["MODI_USER"];//
                paidRow["MODI_DTM"] = tPaidRow["MODI_DTM"];//
                paidRow["CREDIT_CARD_TYPE_ID"] = tPaidRow["CREDIT_CARD_TYPE_ID"];//信用卡別ID, 由EDC讀回信用卡別(master, visa, ...)至[credit_card_type]裡依CREDID_CARD_TYPE_NAME帶回credit_card_type_id
                paidRow["CREDIT_CARD_CHARGE_RATE"] = tPaidRow["CREDIT_CARD_CHARGE_RATE"];//分期手續費率，由credit_card_type_id至[credit_card_proce_rate]以[credit_card_proce_rate].s_date及[credit_card_proce_rate].e_date找出今天的[credit_card_proce_rate].charge_rate帶回
                paidRow["CREDIT_CARD_FEE"] = tPaidRow["CREDIT_CARD_FEE"];//分期手續費，付款金額*分期手續費率
                paidRow["INSTALLMENT_ID"] = tPaidRow["INSTALLMENT_ID"];//分期代號, 由credit_bank_id及credit_installment找[CREDIT_CART_INSTELLMENT]對應的bank_id及pay_seqment帶回時間區間(s_date, e_date)內的[CREDIT_CART_INSTELLMENT].INSTELLMENT_ID*/
                paidRow["INTEREST_RATE"] = tPaidRow["INTEREST_RATE"];//分期利率，由installment_id到[CREDIT_CART_INSTELLMENT] lookup [CREDIT_CART_INSTELLMENT].seqment_rate
                paidRow["STORE_SETTLEMENT_RATE"] = tPaidRow["STORE_SETTLEMENT_RATE"];//門市分攤利率從[cost_center]裡，找出通路的[cost_center].cost_center_no，再用該[cost_center].cost_center_no及[paid_detail].installment_id到[credit_card_settlemment]的cost_center_no及instellment_id找出[credit_card_settlemment].settlement_rate帶回
                paidRow["STORE_SETTLEMENT_AMOUNT"] = tPaidRow["STORE_SETTLEMENT_AMOUNT"];//門市分攤手續費TOTAL_AMOUNT*STORE_SETTLEMENT_RATE
                paidRow["CREDID_CARD_TYPE_NAME"] = tPaidRow["CREDID_CARD_TYPE_NAME"];//信用卡別, visa, master...用credit_card_type_id從[credit_card_type]帶回的credit_card_type_name
                paidRow["NCCC_REF_NO"] = tPaidRow["NCCC_REF_NO"];
                paidRow["NCCC_INV_NO"] = tPaidRow["NCCC_INV_NO"];
                #endregion
                paid.AddPAID_DETAILRow(paidRow);
            }
            paid.AcceptChanges();
            OracleConnection objConn = null;
            OracleTransaction objTX = null;
            try
            {
                objConn = OracleDBUtil.GetConnection();
                objTX = objConn.BeginTransaction();
                OracleDBUtil.Insert(paid);
                objTX.Commit();
            }
            catch (Exception ex)
            {
                objTX.Rollback();
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

        public void InsertPaid_Detail(DataTable tPaid, OracleTransaction objTX)
        {
            SAL01_SALE_DTO.PAID_DETAILDataTable paid = new SAL01_SALE_DTO.PAID_DETAILDataTable();
            foreach (DataRow tPaidRow in tPaid.Rows)
            {
                SAL01_SALE_DTO.PAID_DETAILRow paidRow = paid.NewPAID_DETAILRow();
                #region 欄位值設定
                paidRow["ID"] = tPaidRow["ID"];
                paidRow["POSUUID_MASTER"] = tPaidRow["POSUUID_MASTER"];
                paidRow["DESCRIPTION"] = tPaidRow["DESCRIPTION"];
                paidRow["PAID_MODE"] = tPaidRow["PAID_MODE"];//1: 現金2: 信用卡3: 離線信用卡4: 分期付款5: 禮券6: 金融卡7: Happy Go
                paidRow["PAID_AMOUNT"] = tPaidRow["PAID_AMOUNT"];//付款金額
                paidRow["GIFT_VOUCHER_ID"] = tPaidRow["GIFT_VOUCHER_ID"];//禮券ID，對應到禮券設定裡的某種禮券
                paidRow["GIFT_VOUCHER_NO"] = tPaidRow["GIFT_VOUCHER_NO"];//禮券號碼
                paidRow["HG_CARD_NO"] = tPaidRow["HG_CARD_NO"];//HappyGo卡號
                paidRow["HG_LEFT_POINT"] = tPaidRow["HG_LEFT_POINT"];//HappyGo卡號可用剩餘點數
                /*HG_REDEEM_POINT
                 * (1).系統依HappyGo卡號取得之剩餘點數計算最大可兌點金額，顯示內容為"金額(點數)"（例：400元(1350點)）。
                    (2).系統顯示之欲兌點數，使用者可以依客戶需求選取可兌點項目變更欲兌換點數，系統依變更兌換點數顯示相對應金額。
                 */
                paidRow["HG_REDEEM_POINT"] = tPaidRow["HG_REDEEM_POINT"];//
                paidRow["HG_RULE"] = tPaidRow["HG_RULE"];//選用的兌點規則
                paidRow["DEBIT_CARD_NO"] = tPaidRow["DEBIT_CARD_NO"];//金融卡卡號                //todo: remove
                paidRow["DEBIT_CARD_AUTH_NO"] = tPaidRow["DEBIT_CARD_AUTH_NO"];//金融卡授權碼
                paidRow["CREDIT_TYPE"] = tPaidRow["CREDIT_TYPE"];//1: 一般2: 分期3: 離線
                paidRow["CREDIT_CARD_NO"] = tPaidRow["CREDIT_CARD_NO"];//信用卡號
                paidRow["CREDIT_CARD_AUTH_NO"] = tPaidRow["CREDIT_CARD_AUTH_NO"];//信用卡授權碼
                paidRow["CREDIT_INSTALLMENT"] = tPaidRow["CREDIT_INSTALLMENT"];//信用卡分期付款的期數
                paidRow["CREDIT_BANK_ID"] = tPaidRow["CREDIT_BANK_ID"];//信用卡銀行別
                paidRow["CREATE_USER"] = tPaidRow["CREATE_USER"];//
                paidRow["CREATE_DTM"] = tPaidRow["CREATE_DTM"];//
                paidRow["MODI_USER"] = tPaidRow["MODI_USER"];//
                paidRow["MODI_DTM"] = tPaidRow["MODI_DTM"];//
                paidRow["CREDIT_CARD_TYPE_ID"] = tPaidRow["CREDIT_CARD_TYPE_ID"];//信用卡別ID, 由EDC讀回信用卡別(master, visa, ...)至[credit_card_type]裡依CREDID_CARD_TYPE_NAME帶回credit_card_type_id
                paidRow["CREDIT_CARD_CHARGE_RATE"] = tPaidRow["CREDIT_CARD_CHARGE_RATE"];//分期手續費率，由credit_card_type_id至[credit_card_proce_rate]以[credit_card_proce_rate].s_date及[credit_card_proce_rate].e_date找出今天的[credit_card_proce_rate].charge_rate帶回
                paidRow["CREDIT_CARD_FEE"] = tPaidRow["CREDIT_CARD_FEE"];//分期手續費，付款金額*分期手續費率
                paidRow["INSTALLMENT_ID"] = tPaidRow["INSTALLMENT_ID"];//分期代號, 由credit_bank_id及credit_installment找[CREDIT_CART_INSTELLMENT]對應的bank_id及pay_seqment帶回時間區間(s_date, e_date)內的[CREDIT_CART_INSTELLMENT].INSTELLMENT_ID*/
                paidRow["INTEREST_RATE"] = tPaidRow["INTEREST_RATE"];//分期利率，由installment_id到[CREDIT_CART_INSTELLMENT] lookup [CREDIT_CART_INSTELLMENT].seqment_rate
                paidRow["STORE_SETTLEMENT_RATE"] = tPaidRow["STORE_SETTLEMENT_RATE"];//門市分攤利率從[cost_center]裡，找出通路的[cost_center].cost_center_no，再用該[cost_center].cost_center_no及[paid_detail].installment_id到[credit_card_settlemment]的cost_center_no及instellment_id找出[credit_card_settlemment].settlement_rate帶回
                paidRow["STORE_SETTLEMENT_AMOUNT"] = tPaidRow["STORE_SETTLEMENT_AMOUNT"];//門市分攤手續費TOTAL_AMOUNT*STORE_SETTLEMENT_RATE
                paidRow["CREDID_CARD_TYPE_NAME"] = tPaidRow["CREDID_CARD_TYPE_NAME"];//信用卡別, visa, master...用credit_card_type_id從[credit_card_type]帶回的credit_card_type_name
                paidRow["NCCC_REF_NO"] = tPaidRow["NCCC_REF_NO"];
                paidRow["NCCC_INV_NO"] = tPaidRow["NCCC_INV_NO"];
                #endregion
                paid.AddPAID_DETAILRow(paidRow);
            }
            paid.AcceptChanges();
            
            try
            {
                OracleDBUtil.Insert(objTX, paid);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void DeletePaid_Detail(DataTable tPaid)
        {
            SAL01_SALE_DTO.PAID_DETAILDataTable paid = new SAL01_SALE_DTO.PAID_DETAILDataTable();
            foreach (DataColumn dc in paid.Columns) dc.AllowDBNull = true;
            foreach (DataRow tPaidRow in tPaid.Rows)
            {
                if (getPaid_DetailByID(tPaidRow["ID"].ToString()).Rows.Count > 0)
                {
                    SAL01_SALE_DTO.PAID_DETAILRow paidRow = paid.NewPAID_DETAILRow();
                    paidRow.ID = tPaidRow["ID"].ToString();
                    paid.AddPAID_DETAILRow(paidRow);
                }
            }
            paid.AcceptChanges();
            if (paid.Rows.Count > 0)
            {
                OracleConnection objConn = null;
                OracleTransaction objTX = null;
                try
                {
                    objConn = OracleDBUtil.GetConnection();
                    objTX = objConn.BeginTransaction();
                    OracleDBUtil.DELETEByUUID(paid, "ID");
                    objTX.Commit();
                }
                catch (Exception ex)
                {
                    objTX.Rollback();
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
        }

        public void DeletePaid_DetailByPOSUUID_MASTER(string POSUUID_MASTER)
        {
            SAL01_SALE_DTO.PAID_DETAILDataTable paid = new SAL01_SALE_DTO.PAID_DETAILDataTable();
            foreach (DataColumn dc in paid.Columns) dc.AllowDBNull = true;
            if (getPaid_Detail(POSUUID_MASTER).Rows.Count > 0)
            {
                SAL01_SALE_DTO.PAID_DETAILRow paidRow = paid.NewPAID_DETAILRow();
                paidRow.POSUUID_MASTER = POSUUID_MASTER;
                paid.AddPAID_DETAILRow(paidRow);
            }
            paid.AcceptChanges();
            if (paid.Rows.Count > 0)
            {
                OracleConnection objConn = OracleDBUtil.GetConnection();
                OracleTransaction objTX = objConn.BeginTransaction();
                try
                {
                    OracleDBUtil.DELETEByUUID(paid, "POSUUID_MASTER");
                    objTX.Commit();
                }
                catch (Exception ex)
                {
                    objTX.Rollback();
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
        }

        public void DeletePaid_DetailByPOSUUID_MASTER(string POSUUID_MASTER, OracleTransaction objTX)
        {
            SAL01_SALE_DTO.PAID_DETAILDataTable paid = new SAL01_SALE_DTO.PAID_DETAILDataTable();
            foreach (DataColumn dc in paid.Columns) dc.AllowDBNull = true;
            if (getPaid_Detail(POSUUID_MASTER).Rows.Count > 0)
            {
                SAL01_SALE_DTO.PAID_DETAILRow paidRow = paid.NewPAID_DETAILRow();
                paidRow.POSUUID_MASTER = POSUUID_MASTER;
                paid.AddPAID_DETAILRow(paidRow);
            }
            paid.AcceptChanges();
            if (paid.Rows.Count > 0)
            {
                try
                {
                    OracleDBUtil.DELETEByUUID(objTX, paid, "POSUUID_MASTER");
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        /// <summary>
        /// 依據銷售主檔主鍵及付款類別刪除付款資料
        /// </summary>
        /// <param name="POSUUID_MASTER"> SALE_HEAD.ID</param>
        /// <param name="PAID_MODE"> SALE_HEAD.ID</param>
        /// <returns></returns>
        public int deletePaid_Detail(string POSUUID_MASTER, string PAID_MODE)
        {
            string sqlStr = @"Delete FROM  PAID_DETAIL WHERE POSUUID_MASTER = " + OracleDBUtil.SqlStr(POSUUID_MASTER) + @" And PAID_MODE = "
                                + OracleDBUtil.SqlStr(PAID_MODE);

            OracleConnection objConn = null;
            try
            {
                objConn = OracleDBUtil.GetConnection();
                int ret = OracleDBUtil.ExecuteSql(objConn, sqlStr);
                return ret;
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
        /// 取得手機的IMEI資料
        /// </summary>
        /// <param name="POSUUID_MASTER"> SALE_HEAD.ID</param>
        /// <returns></returns>
        public DataTable getSale_IMEI_LOG(string POSUUID_MASTER)
        {
            string sqlStr = @"SELECT * 
                              FROM  SALE_IMEI_LOG 
                              WHERE SALE_DETAIL_ID in (Select ID From SALE_DETAIL Where POSUUID_MASTER = " + OracleDBUtil.SqlStr(POSUUID_MASTER) + ")";

            DataTable dt = OracleDBUtil.Query_Data(sqlStr);
            return dt;

        }

        public void InsertSale_IMEI_LOG(DataTable tIMEI_LOG)
        {
            SAL01_SALE_DTO.SALE_IMEI_LOGDataTable IMEI_LOG = new SAL01_SALE_DTO.SALE_IMEI_LOGDataTable();
            foreach (DataRow tIMEI_LOGRow in tIMEI_LOG.Rows)
            {
                SAL01_SALE_DTO.SALE_IMEI_LOGRow IMEI_LOGRow = IMEI_LOG.NewSALE_IMEI_LOGRow();
                IMEI_LOGRow["ID"] = tIMEI_LOGRow["ID"];
                IMEI_LOGRow["IMEI"] = tIMEI_LOGRow["IMEI"];
                IMEI_LOGRow["STATUS"] = tIMEI_LOGRow["STATUS"];
                IMEI_LOGRow["SALE_DTM"] = tIMEI_LOGRow["SALE_DTM"];
                IMEI_LOGRow["RTN_DTM"] = tIMEI_LOGRow["RTN_DTM"];
                IMEI_LOGRow["RTN_NO"] = tIMEI_LOGRow["RTN_NO"];
                IMEI_LOGRow["CREATE_USER"] = tIMEI_LOGRow["CREATE_USER"];
                IMEI_LOGRow["CREATE_DTM"] = tIMEI_LOGRow["CREATE_DTM"];
                IMEI_LOGRow["MODI_USER"] = tIMEI_LOGRow["MODI_USER"];
                IMEI_LOGRow["MODI_DTM"] = tIMEI_LOGRow["MODI_DTM"];
                IMEI_LOGRow["SALE_DETAIL_ID"] = tIMEI_LOGRow["SALE_DETAIL_ID"];
                IMEI_LOG.AddSALE_IMEI_LOGRow(IMEI_LOGRow);
            }
            IMEI_LOG.AcceptChanges();
            OracleConnection objConn = OracleDBUtil.GetConnection();
            OracleTransaction objTX = objConn.BeginTransaction();
            try
            {
                OracleDBUtil.Insert(IMEI_LOG);
                objTX.Commit();
            }
            catch (Exception ex)
            {
                objTX.Rollback();
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

        public void InsertSale_IMEI_LOG(DataTable tIMEI_LOG, OracleTransaction objTX)
        {
            SAL01_SALE_DTO.SALE_IMEI_LOGDataTable IMEI_LOG = new SAL01_SALE_DTO.SALE_IMEI_LOGDataTable();
            foreach (DataRow tIMEI_LOGRow in tIMEI_LOG.Rows)
            {
                SAL01_SALE_DTO.SALE_IMEI_LOGRow IMEI_LOGRow = IMEI_LOG.NewSALE_IMEI_LOGRow();
                IMEI_LOGRow["ID"] = tIMEI_LOGRow["ID"];
                IMEI_LOGRow["IMEI"] = tIMEI_LOGRow["IMEI"];
                IMEI_LOGRow["STATUS"] = tIMEI_LOGRow["STATUS"];
                IMEI_LOGRow["SALE_DTM"] = tIMEI_LOGRow["SALE_DTM"];
                IMEI_LOGRow["RTN_DTM"] = tIMEI_LOGRow["RTN_DTM"];
                IMEI_LOGRow["RTN_NO"] = tIMEI_LOGRow["RTN_NO"];
                IMEI_LOGRow["CREATE_USER"] = tIMEI_LOGRow["CREATE_USER"];
                IMEI_LOGRow["CREATE_DTM"] = tIMEI_LOGRow["CREATE_DTM"];
                IMEI_LOGRow["MODI_USER"] = tIMEI_LOGRow["MODI_USER"];
                IMEI_LOGRow["MODI_DTM"] = tIMEI_LOGRow["MODI_DTM"];
                IMEI_LOGRow["SALE_DETAIL_ID"] = tIMEI_LOGRow["SALE_DETAIL_ID"];
                IMEI_LOG.AddSALE_IMEI_LOGRow(IMEI_LOGRow);
            }
            IMEI_LOG.AcceptChanges();
            
            try
            {
                OracleDBUtil.Insert(objTX, IMEI_LOG);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void DeleteSale_IMEI_LOG(string Sale_Detail_Id)
        {
            OracleConnection objConn = null;
            //bool isValid = false;
            try
            {
                objConn = OracleDBUtil.GetConnection();
                string sqlStr = @" Delete From Sale_IMEI_LOG Where Sale_Detail_Id = " + OracleDBUtil.SqlStr(Sale_Detail_Id);

                OracleDBUtil.ExecuteSql(objConn, sqlStr);
            }
            catch (Exception ex)
            {
                string msg = ex.Message;
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

        public void DeleteSale_IMEI_LOG(DataTable tIMEI_LOG)
        {
            SAL01_SALE_DTO.SALE_IMEI_LOGDataTable IMEI_LOG = new SAL01_SALE_DTO.SALE_IMEI_LOGDataTable();
            foreach (DataColumn dc in tIMEI_LOG.Columns) dc.AllowDBNull = true;
            foreach (DataRow tIMEI_LOGRow in tIMEI_LOG.Rows)
            {
                SAL01_SALE_DTO.SALE_IMEI_LOGRow IMEI_LOGRow = IMEI_LOG.NewSALE_IMEI_LOGRow();
                IMEI_LOGRow.ID = tIMEI_LOGRow["ID"].ToString();
                IMEI_LOG.AddSALE_IMEI_LOGRow(IMEI_LOGRow);
            }
            IMEI_LOG.AcceptChanges();
            OracleConnection objConn = null;
            OracleTransaction objTX = null;
            try
            {
                objConn = OracleDBUtil.GetConnection();
                objTX = objConn.BeginTransaction();
                OracleDBUtil.DELETEByUUID(IMEI_LOG, "ID");
                objTX.Commit();
            }
            catch (Exception ex)
            {
                objTX.Rollback();
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

        public void DeleteSale_IMEI_LOG(DataTable tIMEI_LOG, OracleTransaction objTX)
        {
            SAL01_SALE_DTO.SALE_IMEI_LOGDataTable IMEI_LOG = new SAL01_SALE_DTO.SALE_IMEI_LOGDataTable();
            foreach (DataColumn dc in tIMEI_LOG.Columns) dc.AllowDBNull = true;
            foreach (DataRow tIMEI_LOGRow in tIMEI_LOG.Rows)
            {
                SAL01_SALE_DTO.SALE_IMEI_LOGRow IMEI_LOGRow = IMEI_LOG.NewSALE_IMEI_LOGRow();
                IMEI_LOGRow.ID = tIMEI_LOGRow["ID"].ToString();
                IMEI_LOG.AddSALE_IMEI_LOGRow(IMEI_LOGRow);
            }
            IMEI_LOG.AcceptChanges();
            
            try
            {
                OracleDBUtil.DELETEByUUID(objTX, IMEI_LOG, "ID");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //進TEMP時才要用
        //public void InsertSale_IMEI_LOG(string SALE_DETAIL_ID, DataTable IMEI_LOG)
        #endregion

        /// <summary>
        /// FROM未結 結帳回寫銷售主檔,結帳回寫未結清單主檔
        /// </summary>
        public void CheckOutFromUnClose(DataTable sale_head, DataTable sale_detail, string modifyUser, OracleTransaction objTX)
        {
            SAL01_SALE_DTO.SALE_HEADDataTable SALE_HEAD = new SAL01_SALE_DTO.SALE_HEADDataTable();
            foreach (DataColumn dc in SALE_HEAD.Columns) dc.AllowDBNull = true;
            //銷售表頭檔
            SAL01_SALE_DTO.SALE_HEADRow SALE_HEAD_Row = SALE_HEAD.NewSALE_HEADRow();
            SALE_HEAD_Row["POSUUID_MASTER"] = sale_head.Rows[0]["POSUUID_MASTER"];
            SALE_HEAD_Row["SALE_STATUS"] = sale_head.Rows[0]["SALE_STATUS"];
            SALE_HEAD_Row["MODI_USER"] = modifyUser;
            SALE_HEAD_Row["MODI_DTM"] = sale_head.Rows[0]["MODI_DTM"];
            SALE_HEAD_Row["CREATE_USER"] = sale_head.Rows[0]["CREATE_USER"];
            SALE_HEAD_Row["CREATE_DTM"] = sale_head.Rows[0]["CREATE_DTM"];
            SALE_HEAD.AddSALE_HEADRow(SALE_HEAD_Row);
            SALE_HEAD.AcceptChanges();

            try
            {
                OracleDBUtil.UPDDATEByUUID(objTX, SALE_HEAD, "POSUUID_MASTER");
                UpdateUnCloseHead(sale_head, "2", modifyUser, objTX);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// FROM銷售主檔
        /// </summary>
        public void CheckOutFromSAL01(DataTable sale_head)
        {
            SAL01_SALE_DTO.SALE_HEADDataTable SALE_HEAD = new SAL01_SALE_DTO.SALE_HEADDataTable();
            foreach (DataColumn dc in SALE_HEAD.Columns) dc.AllowDBNull = true;
            //銷售表頭檔
            SAL01_SALE_DTO.SALE_HEADRow SALE_HEAD_Row = SALE_HEAD.NewSALE_HEADRow();
            SALE_HEAD_Row["POSUUID_MASTER"] = sale_head.Rows[0]["POSUUID_MASTER"];
            SALE_HEAD_Row["SALE_STATUS"] = sale_head.Rows[0]["SALE_STATUS"];
            SALE_HEAD_Row["MODI_USER"] = sale_head.Rows[0]["MODI_USER"];
            SALE_HEAD_Row["MODI_DTM"] = sale_head.Rows[0]["MODI_DTM"];
            SALE_HEAD_Row["CREATE_USER"] = sale_head.Rows[0]["CREATE_USER"];
            SALE_HEAD_Row["CREATE_DTM"] = sale_head.Rows[0]["CREATE_DTM"];
            SALE_HEAD.AddSALE_HEADRow(SALE_HEAD_Row);
            SALE_HEAD.AcceptChanges();
            OracleConnection objConn = null;
            OracleTransaction objTX = null;
            try
            {
                objConn = OracleDBUtil.GetConnection();
                objTX = objConn.BeginTransaction();
                OracleDBUtil.UPDDATEByUUID(SALE_HEAD, "POSUUID_MASTER");
                objTX.Commit();
            }
            catch (Exception ex)
            {
                objTX.Rollback();
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
        /// FROM銷售主檔
        /// </summary>
        public void CheckOutFromCache(DataTable sale_head)
        {
            SAL01_SALE_DTO.SALE_HEADDataTable SALE_HEAD = new SAL01_SALE_DTO.SALE_HEADDataTable();
            foreach (DataColumn dc in SALE_HEAD.Columns) dc.AllowDBNull = true;
            //銷售表頭檔
            SAL01_SALE_DTO.SALE_HEADRow SALE_HEAD_Row = SALE_HEAD.NewSALE_HEADRow();
            SALE_HEAD_Row["POSUUID_MASTER"] = sale_head.Rows[0]["POSUUID_MASTER"];
            SALE_HEAD_Row["SALE_STATUS"] = sale_head.Rows[0]["SALE_STATUS"];
            SALE_HEAD_Row["MODI_USER"] = sale_head.Rows[0]["MODI_USER"];
            SALE_HEAD_Row["MODI_DTM"] = sale_head.Rows[0]["MODI_DTM"];
            SALE_HEAD_Row["CREATE_USER"] = sale_head.Rows[0]["CREATE_USER"];
            SALE_HEAD_Row["CREATE_DTM"] = sale_head.Rows[0]["CREATE_DTM"];
            SALE_HEAD.AddSALE_HEADRow(SALE_HEAD_Row);
            SALE_HEAD.AcceptChanges();
            OracleConnection objConn = null;
            OracleTransaction objTX = null;
            try
            {
                objConn = OracleDBUtil.GetConnection();
                objTX = objConn.BeginTransaction();
                OracleDBUtil.UPDDATEByUUID(SALE_HEAD, "POSUUID_MASTER");
                objTX.Commit();
            }
            catch (Exception ex)
            {
                objTX.Rollback();
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
        /// 取得貨品資料
        /// </summary>
        /// <param name="PRODNO"></param>
        /// <returns></returns>
        public DataTable getProduct(string PRODNO)
        {
            return new Product_Facade().Query_ProductInfo(PRODNO);
        }

        /// <summary>
        /// 取得商品單價
        /// </summary>
        /// <returns>DataTable</returns>
        public DataTable getProductPrice(string prodno)
        {
            string sqlStr = @"  Select PRICE 
                                from PRODUCT 
                                Where to_date(to_char(sysdate, 'yyyy/mm/dd'),'yyyy/mm/dd') Between NVL(S_DATE, to_date('2000/01/01','YYYY/MM/DD')) 
                                And NVL(E_DATE,to_date('3000/01/01','YYYY/MM/DD'))
                                And DEL_FLAG = 'N' 
                                And PRODNO = " + OracleDBUtil.SqlStr(prodno);

            DataTable dt = OracleDBUtil.Query_Data(sqlStr);
            return dt;
        }

        /// <summary>
        /// 代收交易需要開發票貨品資料
        /// </summary>
        /// <returns>DataTable</returns>
        public DataTable getInvoiceProduct()
        {
            string sqlStr = @"select para_value from sys_para where para_key = 'ETC_WORKING_ITEM_CODE'";

            DataTable dt = OracleDBUtil.Query_Data(sqlStr);
            return dt;
        }

        /// <summary>
        /// 取得一般交易不開發票貨品資料
        /// </summary>
        /// <returns>DataTable</returns>
        public DataTable getNonInvoiceProduct()
        {
            string sqlStr = @"select sys_para_type_id from sys_para_type where sys_para_type_name = '開立收據料號清單' ";

            OracleConnection objConn = null;
            try
            {
                objConn = OracleDBUtil.GetConnection();
                DataTable dtParaType = OracleDBUtil.GetDataSet(objConn, sqlStr).Tables[0];
                if (dtParaType != null && dtParaType.Rows.Count > 0)
                {
                    if (dtParaType.Rows[0][0] != null && dtParaType.Rows[0][0].ToString() != "")
                    {
                        sqlStr = " Select para_value from sys_para where sys_para_type_id = " + OracleDBUtil.SqlStr(dtParaType.Rows[0][0].ToString());
                        return OracleDBUtil.GetDataSet(objConn, sqlStr).Tables[0];
                    }
                    else
                    {
                        return null;
                    }
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                string msg = ex.Message;
                return null;
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
        /// 確認Cache的資料是否存在 等級最高 優先處理 若為Cache資料時其他來源資料將不會被執行而先RUNCache的資料
        /// </summary>
        /// <param name="STORE_NO"></param>
        /// <param name="SALE_PERSON"></param>
        /// <returns></returns>
        public DataTable CheckSaleCacheData(string STORE_NO, string SALE_PERSON)
        {
            string sqlStr = @"SELECT POSUUID_MASTER FROM SALE_HEAD 
                              WHERE SALE_STATUS = '1' 
                                and STORE_NO = " + OracleDBUtil.SqlStr(STORE_NO) + @" 
                                and SALE_PERSON = " + OracleDBUtil.SqlStr(SALE_PERSON);

            DataTable dt = OracleDBUtil.Query_Data(sqlStr);
            return dt;

        }

        /// <summary>
        /// 確認Cache的資料是否存在 等級最高 優先處理 若為Cache資料時其他來源資料將不會被執行而先RUNCache的資料
        /// </summary>
        /// <param name="STORE_NO"></param>
        /// <param name="SALE_PERSON"></param>
        /// <param name="SALE_STATUS">1:銷售交易未結,7:換貨交易未結,8:交易補登未結,9:紙本授權未結</param>
        /// <returns></returns>
        public DataTable CheckSaleCacheData(string STORE_NO, string SALE_PERSON, string SALE_STATUS)
        {
            string sqlStr = @"  SELECT POSUUID_MASTER 
                                FROM SALE_HEAD 
                                WHERE SALE_STATUS = " + OracleDBUtil.SqlStr(SALE_STATUS) + @" 
                                  and STORE_NO = " + OracleDBUtil.SqlStr(STORE_NO) + @" 
                                  and SALE_PERSON = " + OracleDBUtil.SqlStr(SALE_PERSON);

            DataTable dt = OracleDBUtil.Query_Data(sqlStr);
            return dt;
        }

        /// <summary>
        /// 取得分期付款視窗的「銀行別」(在有效期限內)
        /// </summary>
        /// <returns></returns>
        public DataTable getBankData()
        {
            //string sqlStr = @"SELECT * FROM BANK Order By BANK_ID";
            string sqlStr = @" SELECT DISTINCT M.BANK_ID
                                             , B.BANK_NAME 
                                FROM CREDIT_CART_INSTELLMENT M, BANK B
                                WHERE M.BANK_ID = B.BANK_ID(+)
                                  AND TRUNC(SYSDATE) >= M.S_DATE 
                                  AND TRUNC(SYSDATE) <= NVL(M.E_DATE, TRUNC(SYSDATE))
                                Order By B.BANK_NAME";

            DataTable dt = OracleDBUtil.Query_Data(sqlStr);
            return dt;
        }

        public DataTable getCreditDivPeriod(string BANK_ID)
        {
            string sqlStr = @"  select pay_seqment 
                                from  CREDIT_CART_INSTELLMENT 
                                where bank_id = " + OracleDBUtil.SqlStr(BANK_ID) + @" 
                                  and to_date(to_char(sysdate, 'yyyy/mm/dd'),'yyyy/mm/dd') Between NVL(S_DATE, to_date('2000/01/01','YYYY/MM/DD')) " + @" 
                                  and NVL(E_DATE,to_date('3000/01/01','YYYY/MM/DD')) 
                                order by pay_seqment";

            DataTable dt = OracleDBUtil.Query_Data(sqlStr);
            return dt;
        }

        /// <summary>
        /// 取得最原始的ID, 從未作廢的會與ID同，退換貨時會一路帶下來
        /// </summary>
        /// <param name="POSUUID_MASTER">SALE_HEAD table uid</param>
        /// <returns></returns>
        public String getORIGINAL_ID(string POSUUID_MASTER)
        {
            string sqlStr = @"SELECT ORIGINAL_ID FROM SALE_HEAD WHERE POSUUID_MASTER=" + OracleDBUtil.SqlStr(POSUUID_MASTER);

            DataTable dt = OracleDBUtil.Query_Data(sqlStr);

            string ORIGINAL_ID = "";
            if (dt != null && dt.Rows.Count > 0 && (!string.IsNullOrEmpty(dt.Rows[0][0].ToString())))
            {
                ORIGINAL_ID = dt.Rows[0][0].ToString();
            }
            return ORIGINAL_ID;
        }

        /// <summary>
        /// 取得需開發票之貨品
        /// </summary>
        /// <param name="PRODNOs">品號</param>
        /// <param name="INV_TYPE">發票類型</param>
        /// <returns></returns>
        public string getINV_PRODUCT(string PRODNOs, string strCondition)
        {
            string rPRODNOs = "";
            string sqlStr = @"SELECT * FROM PRODUCT WHERE PRODNO IN(" + PRODNOs + ")";
            if (strCondition != "")
            {
                sqlStr += strCondition;
            }

            DataTable dt = OracleDBUtil.Query_Data(sqlStr);

            foreach (DataRow dr in dt.Rows)
            {
                rPRODNOs += "'" + dr["PRODNO"].ToString() + "',";
            }
            if (rPRODNOs.Length > 0)
                rPRODNOs = rPRODNOs.Substring(0, rPRODNOs.Length - 1);
            else rPRODNOs = "''";

            return rPRODNOs;
        }

        /// <summary>
        /// 新增發票資料
        /// </summary>
        /// <param name="head"></param>
        /// <param name="INV_ITEM"></param>
        /// <param name="TAX_TYPE">營業稅類別</param>
        public void InsertInvoice(DataTable head, DataRow[] INV_ITEM, string TAX_TYPE)
        {
            OracleConnection objConn = OracleDBUtil.GetConnection();
            OracleTransaction objTX = objConn.BeginTransaction();
            //銷售憑證關聯 [voucher_relation]
            //發票表頭 [invoice_head]
            //發票明細 [invoice_item]
            if (INV_ITEM.Length > 0) //需INSERT 3 個TABLE  
            {
                SAL01_SALE_DTO.VOUCHER_RELATIONDataTable vrDT = new SAL01_SALE_DTO.VOUCHER_RELATIONDataTable();
                SAL01_SALE_DTO.INVOICE_HEADDataTable ihDT = new SAL01_SALE_DTO.INVOICE_HEADDataTable();
                SAL01_SALE_DTO.INVOICE_ITEMDataTable iiDT = new SAL01_SALE_DTO.INVOICE_ITEMDataTable();

                SAL01_SALE_DTO.VOUCHER_RELATIONRow vrRow = vrDT.NewVOUCHER_RELATIONRow();
                string INVOICE_HEAD_ID = Advtek.Utility.GuidNo.getUUID().ToString();
                int HEAD_TOTAL_AMOUNT = 0;
                int HEAD_TAX = 0;
                int HEAD_SALE_AMOUNT = 0;

                vrRow.ID = Advtek.Utility.GuidNo.getUUID().ToString();
                vrRow.SALE_HEAD_ID = head.Rows[0]["POSUUID_MASTER"].ToString();
                vrRow.VOUCHER_ID = INVOICE_HEAD_ID;
                vrRow.VOUCHER_TYPE = "1"; //VOUCHER_TYPE; //憑證類型：1、發票，2、手開發票，3、收據
                vrDT.AddVOUCHER_RELATIONRow(vrRow);
                vrDT.AcceptChanges();

                foreach (DataRow dr in INV_ITEM)
                {
                    SAL01_SALE_DTO.INVOICE_ITEMRow iiRow = iiDT.NewINVOICE_ITEMRow();
                    iiRow["ID"] = Advtek.Utility.GuidNo.getUUID().ToString();
                    iiRow.INVOICE_HEAD_ID = INVOICE_HEAD_ID;
                    iiRow["PRICE"] = dr["UNIT_PRICE"];
                    iiRow["PRODNO"] = dr["PRODNO"];
                    iiRow["PROD_NAME"] = dr["PRODNAME"];
                    iiRow["QUANTITY"] = dr["QUANTITY"];
                    iiRow["TAX"] = dr["TAX"];
                    iiRow["AMOUNT"] = dr["BEFORE_TAX"];
                    iiRow["TOTAL_AMOUNT"] = dr["TOTAL_AMOUNT"];
                    iiRow["SALE_ITEM_ID"] = dr["ID"];
                    iiRow["CREATE_DATE"] = dr["CREATE_DTM"];
                    iiRow["CREATE_USER"] = dr["CREATE_USER"];
                    iiRow["MODI_DTM"] = dr["MODI_DTM"];
                    iiRow["MODI_USER"] = dr["MODI_USER"];
                    HEAD_TOTAL_AMOUNT += Convert.ToInt32(dr["TOTAL_AMOUNT"]);
                    HEAD_TAX += Convert.ToInt32(dr["TAX"]);
                    HEAD_SALE_AMOUNT += Convert.ToInt32(dr["BEFORE_TAX"]);
                    iiDT.AddINVOICE_ITEMRow(iiRow);
                }
                iiDT.AcceptChanges();

                SAL01_SALE_DTO.INVOICE_HEADRow ihRow = ihDT.NewINVOICE_HEADRow();
                ihRow.ID = INVOICE_HEAD_ID;
                ihRow.UNI_NO = head.Rows[0]["UNI_NO"].ToString();
                ihRow.TOTAL_AMOUNT = HEAD_TOTAL_AMOUNT;
                ihRow.TAX = HEAD_TAX;
                ihRow.STORE_NO = head.Rows[0]["STORE_NO"].ToString();
                ihRow.POSUUID_MASTER = head.Rows[0]["POSUUID_MASTER"].ToString();
                ihRow.BUYER = head.Rows[0]["UNI_TITLE"].ToString();
                ihRow.TAX_TYPE = TAX_TYPE;
                ihRow.INVOICE_DATE = DateTime.Now;
                ihRow.SALE_AMOUNT = HEAD_SALE_AMOUNT;

                ihRow["CREATE_DATE"] = head.Rows[0]["CREATE_DTM"];
                ihRow["CREATE_USER"] = head.Rows[0]["CREATE_USER"];
                ihRow["MODI_DTM"] = head.Rows[0]["MODI_DTM"];
                ihRow["MODI_USER"] = head.Rows[0]["MODI_USER"];

                string HEAD_INVICE_NO = "", MSG_CODE = "", MSG = "";
                PK_INVOICE_SP_INVOICE_NO(ihRow.STORE_NO,
                                                  ihRow.POSUUID_MASTER, ihRow.MODI_USER,
                                                  head.Rows[0]["HOST_ID"].ToString(), ref HEAD_INVICE_NO, ref MSG_CODE, ref MSG);
                ihRow.INVOICE_NO = HEAD_INVICE_NO;
                ihDT.AddINVOICE_HEADRow(ihRow);
                ihDT.AcceptChanges();

                try
                {
                    //取得發票號碼失敗
                    if (MSG_CODE != "000") throw new Exception(MSG);
                    OracleDBUtil.Insert(vrDT);
                    OracleDBUtil.Insert(ihDT);
                    OracleDBUtil.Insert(iiDT);
                    objTX.Commit();
                }
                catch (Exception ex)
                {
                    objTX.Rollback();
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
        }

        /// <summary>
        /// 新增發票資料
        /// </summary>
        /// <param name="head"></param>
        /// <param name="INV_ITEM"></param>
        /// <param name="TAX_TYPE">營業稅類別</param>
        /// <param name="objTX">外部傳入Transaction</param>
        /// <param name="HOST_ID">機台IP</param>
        public void InsertInvoice(DataTable head, DataRow[] INV_ITEM, string TAX_TYPE, OracleTransaction objTX, string HOST_ID)
        {
            //銷售憑證關聯 [voucher_relation]
            //發票表頭 [invoice_head]
            //發票明細 [invoice_item]
            if (INV_ITEM.Length > 0) //需INSERT 3 個TABLE  
            {
                SAL01_SALE_DTO.VOUCHER_RELATIONDataTable vrDT = new SAL01_SALE_DTO.VOUCHER_RELATIONDataTable();
                SAL01_SALE_DTO.INVOICE_HEADDataTable ihDT = new SAL01_SALE_DTO.INVOICE_HEADDataTable();
                SAL01_SALE_DTO.INVOICE_ITEMDataTable iiDT = new SAL01_SALE_DTO.INVOICE_ITEMDataTable();

                SAL01_SALE_DTO.VOUCHER_RELATIONRow vrRow = vrDT.NewVOUCHER_RELATIONRow();
                string INVOICE_HEAD_ID = Advtek.Utility.GuidNo.getUUID().ToString();
                int HEAD_TOTAL_AMOUNT = 0;
                int HEAD_TAX = 0;
                int HEAD_SALE_AMOUNT = 0;

                vrRow.ID = Advtek.Utility.GuidNo.getUUID().ToString();
                vrRow.SALE_HEAD_ID = head.Rows[0]["POSUUID_MASTER"].ToString();
                vrRow.VOUCHER_ID = INVOICE_HEAD_ID;
                vrRow.VOUCHER_TYPE = "1"; //VOUCHER_TYPE; //憑證類型：1、發票，2、手開發票，3、收據
                vrDT.AddVOUCHER_RELATIONRow(vrRow);
                vrDT.AcceptChanges();
                int i = 1;
                foreach (DataRow dr in INV_ITEM)
                {
                    SAL01_SALE_DTO.INVOICE_ITEMRow iiRow = iiDT.NewINVOICE_ITEMRow();
                    iiRow["ID"] = Advtek.Utility.GuidNo.getUUID().ToString();
                    iiRow.INVOICE_HEAD_ID = INVOICE_HEAD_ID;
                    iiRow["PRICE"] = dr["UNIT_PRICE"];
                    iiRow["PRODNO"] = dr["PRODNO"];
                    iiRow["PROD_NAME"] = dr["PRODNAME"];
                    iiRow["QUANTITY"] = dr["QUANTITY"];
                    iiRow["TAX"] = dr["TAX"];
                    iiRow["AMOUNT"] = dr["BEFORE_TAX"];
                    iiRow["TOTAL_AMOUNT"] = dr["TOTAL_AMOUNT"];
                    iiRow["SALE_ITEM_ID"] = dr["ID"];
                    iiRow["CREATE_DATE"] = dr["CREATE_DTM"];
                    iiRow["CREATE_USER"] = dr["CREATE_USER"];
                    iiRow["MODI_DTM"] = dr["MODI_DTM"];
                    iiRow["MODI_USER"] = dr["MODI_USER"];
                    iiRow["INVOICE_SEQ_NO"] = i++;
                  
                    HEAD_TOTAL_AMOUNT += Convert.ToInt32(dr["TOTAL_AMOUNT"]);
                    HEAD_TAX += Convert.ToInt32(dr["TAX"]);
                    HEAD_SALE_AMOUNT += Convert.ToInt32(dr["BEFORE_TAX"]);
                    iiDT.AddINVOICE_ITEMRow(iiRow);
                }
                iiDT.AcceptChanges();

                SAL01_SALE_DTO.INVOICE_HEADRow ihRow = ihDT.NewINVOICE_HEADRow();
                ihRow.ID = INVOICE_HEAD_ID;
                ihRow.UNI_NO = head.Rows[0]["UNI_NO"].ToString();
                ihRow.TOTAL_AMOUNT = HEAD_TOTAL_AMOUNT;
                ihRow.TAX = HEAD_TAX;
                ihRow.BEFORE_TAX = head.Rows[0]["SALE_BEFORE_TAX"].ToString();
                ihRow.STORE_NO = head.Rows[0]["STORE_NO"].ToString();
                ihRow.POSUUID_MASTER = head.Rows[0]["POSUUID_MASTER"].ToString();
                ihRow.BUYER = head.Rows[0]["UNI_TITLE"].ToString();
                ihRow.TAX_TYPE = TAX_TYPE;
                ihRow.INVOICE_DATE = DateTime.Now;
                ihRow.SALE_AMOUNT = HEAD_SALE_AMOUNT;
                if (head.Rows[0]["INVOICE_TYPE"] == null | head.Rows[0]["INVOICE_TYPE"].ToString() == ""
                    || head.Rows[0]["INVOICE_TYPE"].ToString() == "發票")
                    ihRow.INVOICE_TYPE = "01";  //連線電子發票
                else 
                    ihRow.INVOICE_TYPE = head.Rows[0]["INVOICE_TYPE"].ToString();

                ihRow["CREATE_DATE"] = head.Rows[0]["CREATE_DTM"];
                ihRow["CREATE_USER"] = head.Rows[0]["CREATE_USER"];
                ihRow["MODI_DTM"] = head.Rows[0]["MODI_DTM"];
                ihRow["MODI_USER"] = head.Rows[0]["MODI_USER"];
                string remark = TSAL01_Facade.get_sale_remark(ihRow.POSUUID_MASTER);
                string SALE_NO = "交易序號:" + StringUtil.CStr(head.Rows[0]["SALE_NO"]);
                if (!string.IsNullOrEmpty(remark))
                {
                    SALE_NO += Environment.NewLine + "信用卡卡號:" + remark;
                }
                ihRow["REMARK"] = SALE_NO;
                string HEAD_INVICE_NO = "", MSG_CODE = "", MSG = "";
                PK_INVOICE_SP_INVOICE_NO(ihRow.STORE_NO,
                                                  ihRow.POSUUID_MASTER, ihRow.MODI_USER,
                                                  HOST_ID, ref HEAD_INVICE_NO, ref MSG_CODE, ref MSG);
                //head.Rows[0]["HOST_ID"].ToString()
                ihRow.INVOICE_NO = HEAD_INVICE_NO;
                ihDT.AddINVOICE_HEADRow(ihRow);
                ihDT.AcceptChanges();

                try
                {
                    //取得發票號碼失敗
                    if (MSG_CODE != "000") throw new Exception(MSG);
                    OracleDBUtil.Insert(objTX, vrDT);
                    OracleDBUtil.Insert(objTX, ihDT);
                    OracleDBUtil.Insert(objTX, iiDT);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        /// <summary>
        /// 新增收據資料
        /// </summary>
        /// <param name="head"></param>
        /// <param name="INV_ITEM"></param>
        public void InsertReceipt(DataTable head, DataRow[] INV_ITEM)
        {

            OracleConnection objConn = null;
            OracleTransaction objTX = null;
            //銷售憑證關聯 [voucher_relation]
            //收據表頭 [receipt_head]
            //收據明細 [receipt_item]
            if (INV_ITEM.Length > 0) //需INSERT 3個TABLE  
            {
                objConn = OracleDBUtil.GetConnection();
                objTX = objConn.BeginTransaction();

                SAL01_SALE_DTO.VOUCHER_RELATIONDataTable vrDT = new SAL01_SALE_DTO.VOUCHER_RELATIONDataTable();
                SAL01_SALE_DTO.RECEIPT_HEADDataTable rhDT = new SAL01_SALE_DTO.RECEIPT_HEADDataTable();
                SAL01_SALE_DTO.RECEIPT_ITEMDataTable riDT = new SAL01_SALE_DTO.RECEIPT_ITEMDataTable();
                SAL01_SALE_DTO.VOUCHER_RELATIONRow vrRow = vrDT.NewVOUCHER_RELATIONRow();

                string RECEIPT_HEAD_ID = Advtek.Utility.GuidNo.getUUID().ToString();
                int HEAD_TOTAL_AMOUNT = 0;
                int HEAD_TAX = 0;
                int HEAD_SALE_AMOUNT = 0;

                vrRow.ID = Advtek.Utility.GuidNo.getUUID().ToString();
                vrRow.SALE_HEAD_ID = head.Rows[0]["POSUUID_MASTER"].ToString();
                vrRow.VOUCHER_ID = RECEIPT_HEAD_ID;
                vrRow.VOUCHER_TYPE = "3";   //憑證類型：1、發票，2、手開發票，3、收據
                vrDT.AddVOUCHER_RELATIONRow(vrRow);
                vrDT.AcceptChanges();

                foreach (DataRow dr in INV_ITEM)
                {
                    SAL01_SALE_DTO.RECEIPT_ITEMRow riRow = riDT.NewRECEIPT_ITEMRow();

                    riRow.RECEIPT_HEAD_ID = RECEIPT_HEAD_ID;
                    riRow["ID"] = Advtek.Utility.GuidNo.getUUID().ToString();
                    riRow["PRICE"] = dr["UNIT_PRICE"];
                    riRow["PRODNO"] = dr["PRODNO"];
                    riRow["QUANTITY"] = dr["QUANTITY"];
                    riRow["TAX"] = dr["TAX"];
                    riRow["AMOUNT"] = dr["BEFORE_TAX"];
                    riRow["TOTAL_AMOUNT"] = dr["TOTAL_AMOUNT"];
                    riRow["SALE_ITEM_ID"] = dr["ID"];
                    riRow["CREATE_DATE"] = head.Rows[0]["CREATE_DTM"];
                    riRow["CREATE_USER"] = head.Rows[0]["CREATE_USER"];
                    riRow["MODI_DTM"] = head.Rows[0]["MODI_DTM"];
                    riRow["MODI_USER"] = head.Rows[0]["MODI_USER"];
                    HEAD_TOTAL_AMOUNT += Convert.ToInt32(dr["TOTAL_AMOUNT"]);
                    HEAD_TAX += Convert.ToInt32(dr["TAX"]);
                    HEAD_SALE_AMOUNT += Convert.ToInt32(dr["BEFORE_TAX"]);
                    riDT.AddRECEIPT_ITEMRow(riRow);
                }
                riDT.AcceptChanges();

                SAL01_SALE_DTO.RECEIPT_HEADRow rhRow = rhDT.NewRECEIPT_HEADRow();
                rhRow.ID = RECEIPT_HEAD_ID;
                rhRow.UNI_NO = head.Rows[0]["UNI_NO"].ToString();
                rhRow.TOTAL_AMOUNT = HEAD_TOTAL_AMOUNT;
                rhRow.TAX = HEAD_TAX;
                rhRow.SALE_AMOUNT = HEAD_SALE_AMOUNT;
                rhRow.STORE_NO = head.Rows[0]["STORE_NO"].ToString();
                rhRow.POSUUID_MASTER = head.Rows[0]["POSUUID_MASTER"].ToString();
                rhRow.BUYER = head.Rows[0]["UNI_TITLE"].ToString();
                rhRow.RECEIPT_NO = Advtek.Utility.SerialNo.GenNo("RECITT");
                rhRow.TAX_TYPE = "3";
                if (head.Rows[0]["INVOICE_DATE"] == null)
                {
                    rhRow.INVOICE_DATE = DateTime.Now;
                }
                else  //交易補登畫面，發票日期由User key in
                {
                    rhRow["INVOICE_DATE"] = head.Rows[0]["INVOICE_DATE"];
                }
                rhRow["CREATE_DATE"] = head.Rows[0]["CREATE_DTM"];
                rhRow["CREATE_USER"] = head.Rows[0]["CREATE_USER"];
                rhRow["MODI_DTM"] = head.Rows[0]["MODI_DTM"];
                rhRow["MODI_USER"] = head.Rows[0]["MODI_USER"];

                rhDT.AddRECEIPT_HEADRow(rhRow);
                rhDT.AcceptChanges();
                try
                {
                    OracleDBUtil.Insert(vrDT);
                    OracleDBUtil.Insert(rhDT);
                    OracleDBUtil.Insert(riDT);
                    objTX.Commit();
                }
                catch (Exception ex)
                {
                    objTX.Rollback();
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
        }

        /// <summary>
        /// 新增收據資料
        /// </summary>
        /// <param name="head"></param>
        /// <param name="INV_ITEM"></param>
        /// <param name="objTX">外部傳入Transaction</param>
        public void InsertReceipt(DataTable head, DataRow[] INV_ITEM, OracleTransaction objTX)
        {
            //銷售憑證關聯 [voucher_relation]
            //收據表頭 [receipt_head]
            //收據明細 [receipt_item]
            if (INV_ITEM.Length > 0) //需INSERT 3個TABLE  
            {
                SAL01_SALE_DTO.VOUCHER_RELATIONDataTable vrDT = new SAL01_SALE_DTO.VOUCHER_RELATIONDataTable();
                SAL01_SALE_DTO.RECEIPT_HEADDataTable rhDT = new SAL01_SALE_DTO.RECEIPT_HEADDataTable();
                SAL01_SALE_DTO.RECEIPT_ITEMDataTable riDT = new SAL01_SALE_DTO.RECEIPT_ITEMDataTable();
                SAL01_SALE_DTO.VOUCHER_RELATIONRow vrRow = vrDT.NewVOUCHER_RELATIONRow();

                string RECEIPT_HEAD_ID = Advtek.Utility.GuidNo.getUUID().ToString();
                int HEAD_TOTAL_AMOUNT = 0;
                int HEAD_TAX = 0;
                int HEAD_SALE_AMOUNT = 0;

                vrRow.ID = Advtek.Utility.GuidNo.getUUID().ToString();
                vrRow.SALE_HEAD_ID = head.Rows[0]["POSUUID_MASTER"].ToString();
                vrRow.VOUCHER_ID = RECEIPT_HEAD_ID;
                vrRow.VOUCHER_TYPE = "3";   //憑證類型：1、發票，2、手開發票，3、收據
                vrDT.AddVOUCHER_RELATIONRow(vrRow);
                vrDT.AcceptChanges();

                foreach (DataRow dr in INV_ITEM)
                {
                    SAL01_SALE_DTO.RECEIPT_ITEMRow riRow = riDT.NewRECEIPT_ITEMRow();

                    riRow.RECEIPT_HEAD_ID = RECEIPT_HEAD_ID;
                    riRow["ID"] = Advtek.Utility.GuidNo.getUUID().ToString();
                    riRow["PRICE"] = dr["UNIT_PRICE"];
                    riRow["PRODNO"] = dr["PRODNO"];
                    riRow["QUANTITY"] = dr["QUANTITY"];
                    riRow["TAX"] = dr["TAX"];
                    riRow["AMOUNT"] = dr["BEFORE_TAX"];
                    riRow["TOTAL_AMOUNT"] = dr["TOTAL_AMOUNT"];
                    riRow["SALE_ITEM_ID"] = dr["ID"];
                    riRow["CREATE_DATE"] = head.Rows[0]["CREATE_DTM"];
                    riRow["CREATE_USER"] = head.Rows[0]["CREATE_USER"];
                    riRow["MODI_DTM"] = head.Rows[0]["MODI_DTM"];
                    riRow["MODI_USER"] = head.Rows[0]["MODI_USER"];
                    HEAD_TOTAL_AMOUNT += Convert.ToInt32(dr["TOTAL_AMOUNT"]);
                    HEAD_TAX += Convert.ToInt32(dr["TAX"]);
                    HEAD_SALE_AMOUNT += Convert.ToInt32(dr["BEFORE_TAX"]);
                    riDT.AddRECEIPT_ITEMRow(riRow);
                }
                riDT.AcceptChanges();

                SAL01_SALE_DTO.RECEIPT_HEADRow rhRow = rhDT.NewRECEIPT_HEADRow();
                rhRow.ID = RECEIPT_HEAD_ID;
                rhRow.RECEIPT_NO = Advtek.Utility.SerialNo.GenNo("RECITT");
                rhRow.UNI_NO = head.Rows[0]["UNI_NO"].ToString();
                rhRow.TOTAL_AMOUNT = HEAD_TOTAL_AMOUNT;
                rhRow.TAX = HEAD_TAX;
                rhRow.SALE_AMOUNT = HEAD_SALE_AMOUNT;
                rhRow.STORE_NO = head.Rows[0]["STORE_NO"].ToString();
                rhRow.POSUUID_MASTER = head.Rows[0]["POSUUID_MASTER"].ToString();
                rhRow.BUYER = head.Rows[0]["UNI_TITLE"].ToString();
                rhRow.TAX_TYPE = "3";
                if (head.Rows[0]["INVOICE_DATE"] == null)
                {
                    rhRow.INVOICE_DATE = DateTime.Now;
                }
                else  //交易補登畫面，發票日期由User key in
                {
                    rhRow["INVOICE_DATE"] = head.Rows[0]["INVOICE_DATE"];
                }
                rhRow["CREATE_DATE"] = head.Rows[0]["CREATE_DTM"];
                rhRow["CREATE_USER"] = head.Rows[0]["CREATE_USER"];
                rhRow["MODI_DTM"] = head.Rows[0]["MODI_DTM"];
                rhRow["MODI_USER"] = head.Rows[0]["MODI_USER"];

                rhDT.AddRECEIPT_HEADRow(rhRow);
                rhDT.AcceptChanges();
                try
                {
                    OracleDBUtil.Insert(objTX, vrDT);
                    OracleDBUtil.Insert(objTX, rhDT);
                    OracleDBUtil.Insert(objTX, riDT);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        /// <summary>
        /// 新增手開發票資料
        /// </summary>
        /// <param name="head"></param>
        /// <param name="INV_ITEM"></param>
        /// <param name="TAX_TYPE">營業稅類別</param>
        public void InsertManualInvoice(DataTable head, DataRow[] INV_ITEM, string TAX_TYPE)
        {
            OracleConnection objConn = null;
            OracleTransaction objTX = null;
            //銷售憑證關聯 [voucher_relation]
            //發票表頭 [MANUAL_INVOICE_HEAD]
            //發票明細 [MANUAL_INVOICE_ITEM]
            if (INV_ITEM.Length > 0) //需INSERT 3 個TABLE  
            {
                objConn = OracleDBUtil.GetConnection();
                objTX = objConn.BeginTransaction();

                SAL01_SALE_DTO.VOUCHER_RELATIONDataTable vrDT = new SAL01_SALE_DTO.VOUCHER_RELATIONDataTable();
                SAL01_SALE_DTO.MANUAL_INVOICE_HEADDataTable ihDT = new SAL01_SALE_DTO.MANUAL_INVOICE_HEADDataTable();
                SAL01_SALE_DTO.MANUAL_INVOICE_ITEMDataTable iiDT = new SAL01_SALE_DTO.MANUAL_INVOICE_ITEMDataTable();

                SAL01_SALE_DTO.VOUCHER_RELATIONRow vrRow = vrDT.NewVOUCHER_RELATIONRow();
                string MANUAL_INVOICE_HEAD_ID = Advtek.Utility.GuidNo.getUUID().ToString();
                int HEAD_TOTAL_AMOUNT = 0;
                int HEAD_TAX = 0;
                int HEAD_SALE_AMOUNT = 0;

                vrRow.ID = Advtek.Utility.GuidNo.getUUID().ToString();
                vrRow.SALE_HEAD_ID = head.Rows[0]["POSUUID_MASTER"].ToString();
                vrRow.VOUCHER_ID = MANUAL_INVOICE_HEAD_ID;
                vrRow.VOUCHER_TYPE = "2";  //憑證類型：1、發票，2、手開發票，3、收據
                vrDT.AddVOUCHER_RELATIONRow(vrRow);
                vrDT.AcceptChanges();

                foreach (DataRow dr in INV_ITEM)
                {
                    SAL01_SALE_DTO.MANUAL_INVOICE_ITEMRow iiRow = iiDT.NewMANUAL_INVOICE_ITEMRow();
                    iiRow.ID = Advtek.Utility.GuidNo.getUUID().ToString();
                    iiRow.MANUAL_INVOICE_HEAD_ID = MANUAL_INVOICE_HEAD_ID;
                    iiRow["PRODNO"] = dr["PRODNO"];
                    iiRow["PROD_NAME"] = dr["PRODNAME"];
                    iiRow["PRICE"] = dr["UNIT_PRICE"];
                    iiRow["QUANTITY"] = dr["QUANTITY"];
                    iiRow["AMOUNT"] = dr["BEFORE_TAX"];
                    iiRow["TAX"] = dr["TAX"];
                    iiRow["TOTAL_AMOUNT"] = dr["TOTAL_AMOUNT"];
                    iiRow["SALE_ITEM_ID"] = dr["ID"];
                    iiRow["CREATE_DATE"] = dr["CREATE_DTM"];
                    iiRow["CREATE_USER"] = dr["CREATE_USER"];
                    iiRow["MODI_DTM"] = dr["MODI_DTM"];
                    iiRow["MODI_USER"] = dr["MODI_USER"];
                    HEAD_TOTAL_AMOUNT += Convert.ToInt32(dr["TOTAL_AMOUNT"]);
                    HEAD_TAX += Convert.ToInt32(dr["TAX"]);
                    HEAD_SALE_AMOUNT += Convert.ToInt32(dr["BEFORE_TAX"]);
                    iiDT.AddMANUAL_INVOICE_ITEMRow(iiRow);

                }
                iiDT.AcceptChanges();

                SAL01_SALE_DTO.MANUAL_INVOICE_HEADRow ihRow = ihDT.NewMANUAL_INVOICE_HEADRow();
                ihRow.ID = MANUAL_INVOICE_HEAD_ID;
                ihRow.INVOICE_NO = head.Rows[0]["INVOICE_NO"].ToString();
                ihRow.BUYER = head.Rows[0]["UNI_TITLE"].ToString();
                ihRow.UNI_NO = head.Rows[0]["UNI_NO"].ToString();
                ihRow.SALE_AMOUNT = HEAD_SALE_AMOUNT;
                ihRow.TAX = HEAD_TAX;
                ihRow.TOTAL_AMOUNT = HEAD_TOTAL_AMOUNT;
                ihRow.TAX_TYPE = TAX_TYPE;
                ihRow.STORE_NO = head.Rows[0]["STORE_NO"].ToString();
                ihRow.POSUUID_MASTER = head.Rows[0]["POSUUID_MASTER"].ToString();

                ihRow.INVOICE_TYPE = head.Rows[0]["INVOICE_TYPE"].ToString();
                ihRow.IS_INVALID = "N";
                ihRow["INVOICE_DATE"] = head.Rows[0]["TRADE_DATE"];

                ihRow["CREATE_DATE"] = head.Rows[0]["CREATE_DTM"];
                ihRow["CREATE_USER"] = head.Rows[0]["CREATE_USER"];
                ihRow["MODI_DTM"] = head.Rows[0]["MODI_DTM"];
                ihRow["MODI_USER"] = head.Rows[0]["MODI_USER"];

                ihDT.AddMANUAL_INVOICE_HEADRow(ihRow);
                ihDT.AcceptChanges();

                try
                {
                    OracleDBUtil.Insert(vrDT);
                    OracleDBUtil.Insert(ihDT);
                    OracleDBUtil.Insert(iiDT);
                    objTX.Commit();
                }
                catch (Exception ex)
                {
                    objTX.Rollback();
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
        }

        /// <summary>
        /// 新增手開發票資料
        /// </summary>
        /// <param name="head"></param>
        /// <param name="INV_ITEM"></param>
        /// <param name="TAX_TYPE">營業稅類別</param>
        /// <param name="objTX">外部系統傳入Transaction</param>
        public void InsertManualInvoice(DataTable head, DataRow[] INV_ITEM, string TAX_TYPE, string HOST_IP, OracleTransaction objTX)
        {
            //銷售憑證關聯 [voucher_relation]
            //發票表頭 [MANUAL_INVOICE_HEAD]
            //發票明細 [MANUAL_INVOICE_ITEM]
            if (INV_ITEM.Length > 0) //需INSERT 3 個TABLE  
            {
                SAL01_SALE_DTO.VOUCHER_RELATIONDataTable vrDT = new SAL01_SALE_DTO.VOUCHER_RELATIONDataTable();
                SAL01_SALE_DTO.MANUAL_INVOICE_HEADDataTable ihDT = new SAL01_SALE_DTO.MANUAL_INVOICE_HEADDataTable();
                SAL01_SALE_DTO.MANUAL_INVOICE_ITEMDataTable iiDT = new SAL01_SALE_DTO.MANUAL_INVOICE_ITEMDataTable();

                SAL01_SALE_DTO.VOUCHER_RELATIONRow vrRow = vrDT.NewVOUCHER_RELATIONRow();
                string MANUAL_INVOICE_HEAD_ID = Advtek.Utility.GuidNo.getUUID().ToString();
                int HEAD_TOTAL_AMOUNT = 0;
                int HEAD_TAX = 0;
                int HEAD_SALE_AMOUNT = 0;

                vrRow.ID = Advtek.Utility.GuidNo.getUUID().ToString();
                vrRow.SALE_HEAD_ID = head.Rows[0]["POSUUID_MASTER"].ToString();
                vrRow.VOUCHER_ID = MANUAL_INVOICE_HEAD_ID;
                vrRow.VOUCHER_TYPE = "2";  //憑證類型：1、發票，2、手開發票，3、收據
                vrDT.AddVOUCHER_RELATIONRow(vrRow);
                vrDT.AcceptChanges();

                foreach (DataRow dr in INV_ITEM)
                {
                    SAL01_SALE_DTO.MANUAL_INVOICE_ITEMRow iiRow = iiDT.NewMANUAL_INVOICE_ITEMRow();
                    iiRow.ID = Advtek.Utility.GuidNo.getUUID().ToString();
                    iiRow.MANUAL_INVOICE_HEAD_ID = MANUAL_INVOICE_HEAD_ID;
                    iiRow["PRODNO"] = dr["PRODNO"];
                    iiRow["PROD_NAME"] = dr["PRODNAME"];
                    iiRow["PRICE"] = dr["UNIT_PRICE"];
                    iiRow["QUANTITY"] = dr["QUANTITY"];
                    iiRow["AMOUNT"] = dr["BEFORE_TAX"];
                    iiRow["TAX"] = dr["TAX"];
                    iiRow["TOTAL_AMOUNT"] = dr["TOTAL_AMOUNT"];
                    iiRow["SALE_ITEM_ID"] = dr["ID"];
                    iiRow["CREATE_DATE"] = dr["CREATE_DTM"];
                    iiRow["CREATE_USER"] = dr["CREATE_USER"];
                    iiRow["MODI_DTM"] = dr["MODI_DTM"];
                    iiRow["MODI_USER"] = dr["MODI_USER"];
                    HEAD_TOTAL_AMOUNT += Convert.ToInt32(dr["TOTAL_AMOUNT"]);
                    HEAD_TAX += Convert.ToInt32(dr["TAX"]);
                    HEAD_SALE_AMOUNT += Convert.ToInt32(dr["BEFORE_TAX"]);
                    iiDT.AddMANUAL_INVOICE_ITEMRow(iiRow);

                }
                iiDT.AcceptChanges();

                SAL01_SALE_DTO.MANUAL_INVOICE_HEADRow ihRow = ihDT.NewMANUAL_INVOICE_HEADRow();
                ihRow.ID = MANUAL_INVOICE_HEAD_ID;
                ihRow.INVOICE_NO = head.Rows[0]["INVOICE_NO"].ToString();
                ihRow.BUYER = head.Rows[0]["UNI_TITLE"].ToString();
                ihRow.UNI_NO = head.Rows[0]["UNI_NO"].ToString();
                ihRow.SALE_AMOUNT = HEAD_SALE_AMOUNT;
                ihRow.TAX = HEAD_TAX;
                ihRow.TOTAL_AMOUNT = HEAD_TOTAL_AMOUNT;
                ihRow.TAX_TYPE = TAX_TYPE;
                ihRow.STORE_NO = head.Rows[0]["STORE_NO"].ToString();
                ihRow.POSUUID_MASTER = head.Rows[0]["POSUUID_MASTER"].ToString();

                ihRow.INVOICE_TYPE = head.Rows[0]["INVOICE_TYPE"].ToString();
                ihRow.IS_INVALID = "N";
                ihRow["INVOICE_DATE"] = head.Rows[0]["INVOICE_DATE"];

                ihRow["CREATE_DATE"] = head.Rows[0]["CREATE_DTM"];
                ihRow["CREATE_USER"] = head.Rows[0]["CREATE_USER"];
                ihRow["MODI_DTM"] = head.Rows[0]["MODI_DTM"];
                ihRow["MODI_USER"] = head.Rows[0]["MODI_USER"];

                ihDT.AddMANUAL_INVOICE_HEADRow(ihRow);
                ihDT.AcceptChanges();

                try
                {
                    OracleDBUtil.Insert(objTX, vrDT);
                    OracleDBUtil.Insert(objTX, ihDT);
                    OracleDBUtil.Insert(objTX, iiDT);
                    string strSql = @"Update INVOICE_NO_POOL set MODI_USER=" + OracleDBUtil.SqlStr(head.Rows[0]["MODI_USER"].ToString())
                                    + ", MODI_DTM=sysdate, Status = '1', USE_HOST_ID = " + OracleDBUtil.SqlStr(HOST_IP)
                                    + ", INV_DATE= to_date(" 
                                    + OracleDBUtil.SqlStr(Convert.ToDateTime(head.Rows[0]["INVOICE_DATE"].ToString()).ToString("yyyy/MM/dd"))
                                    + ",'yyyy/MM/dd')" 
                                    + ", POS_MASTER_UUID = " + OracleDBUtil.SqlStr(head.Rows[0]["POSUUID_MASTER"].ToString())
                                    + " Where INVOICE_NO = " + OracleDBUtil.SqlStr(head.Rows[0]["INVOICE_NO"].ToString());
                    OracleDBUtil.ExecuteSql(objTX, strSql);
                    // 更新目前編號
                    strSql = @"update HQ_INVOICE_ASSIGN A set CURRENT_NO =   ( "
                             + "    select max(INVOICE_NO) from INVOICE_NO_POOL "
                             + "    where ASSIGN_ID = A.ASSIGN_ID and STATUS = 1 "
                             + ") "
                             + "where ASSIGN_ID in ( "
                             + "select ASSIGN_ID from INVOICE_NO_POOL "
                             + "where INVOICE_NO = " + OracleDBUtil.SqlStr(head.Rows[0]["INVOICE_NO"].ToString())
                             + "and STORE_NO = " + OracleDBUtil.SqlStr(head.Rows[0]["STORE_NO"].ToString())
                             + " ) ";
                    OracleDBUtil.ExecuteSql(objTX, strSql);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        /// <summary>
        /// 刪除發票資訊
        /// </summary>
        /// <param name="POSUUID_MASTER"></param>
        public void DeleteInvoiceOrReceipt(string POSUUID_MASTER)
        {
            if (!string.IsNullOrEmpty(POSUUID_MASTER))
            {
                OracleConnection objConn = null;
                OracleTransaction objTX = null;
                try
                {
                    objConn = OracleDBUtil.GetConnection();
                    objConn.BeginTransaction();

                    StringBuilder sbINVOICE_ITEM = new StringBuilder();
                    sbINVOICE_ITEM.Append("DELETE FROM INVOICE_ITEM WHERE INVOICE_HEAD_ID IN (SELECT ID FROM INVOICE_HEAD WHERE POSUUID_MASTER = " + OracleDBUtil.SqlStr(POSUUID_MASTER) + ")");

                    StringBuilder sbINVOICE_HEAD = new StringBuilder();
                    sbINVOICE_HEAD.Append("DELETE FROM INVOICE_HEAD WHERE POSUUID_MASTER = " + OracleDBUtil.SqlStr(POSUUID_MASTER));

                    StringBuilder sbMANUAL_INVOICE_ITEM = new StringBuilder();
                    sbMANUAL_INVOICE_ITEM.Append("DELETE FROM MANUAL_INVOICE_ITEM WHERE MANUAL_INVOICE_HEAD_ID IN (SELECT ID FROM MANUAL_INVOICE_HEAD WHERE POSUUID_MASTER = " + OracleDBUtil.SqlStr(POSUUID_MASTER) + ")");

                    StringBuilder sbMANUAL_INVOICE_HEAD = new StringBuilder();
                    sbMANUAL_INVOICE_HEAD.Append("DELETE FROM MANUAL_INVOICE_HEAD WHERE POSUUID_MASTER = " + OracleDBUtil.SqlStr(POSUUID_MASTER));

                    StringBuilder sbRECEIPT_ITEM = new StringBuilder();
                    sbRECEIPT_ITEM.Append("DELETE FROM RECEIPT_ITEM WHERE RECEIPT_HEAD_ID IN (SELECT ID FROM RECEIPT_HEAD WHERE POSUUID_MASTER = " + OracleDBUtil.SqlStr(POSUUID_MASTER) + ")");

                    StringBuilder sbRECEIPT_HEAD = new StringBuilder();
                    sbRECEIPT_HEAD.Append("DELETE FROM RECEIPT_HEAD WHERE POSUUID_MASTER = " + OracleDBUtil.SqlStr(POSUUID_MASTER));

                    StringBuilder sbVOUCHER_RELATION = new StringBuilder();
                    sbVOUCHER_RELATION.Append("DELETE FROM VOUCHER_RELATION WHERE SALE_HEAD_ID = " + OracleDBUtil.SqlStr(POSUUID_MASTER));


                    OracleDBUtil.ExecuteSql(objTX, sbINVOICE_ITEM.ToString());
                    OracleDBUtil.ExecuteSql(objTX, sbINVOICE_HEAD.ToString());
                    OracleDBUtil.ExecuteSql(objTX, sbMANUAL_INVOICE_ITEM.ToString());
                    OracleDBUtil.ExecuteSql(objTX, sbMANUAL_INVOICE_HEAD.ToString());
                    OracleDBUtil.ExecuteSql(objTX, sbRECEIPT_ITEM.ToString());
                    OracleDBUtil.ExecuteSql(objTX, sbRECEIPT_HEAD.ToString());
                    OracleDBUtil.ExecuteSql(objTX, sbVOUCHER_RELATION.ToString());

                    objTX.Commit();
                }
                catch (Exception ex)
                {
                    objTX.Rollback();
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
        }


        /// <summary>
        /// 產生發票號
        /// </summary>
        /// <param name="oraConn"></param>
        /// <param name="INSTORE_NO"></param>
        /// <param name="INPOS_MASTER_UUID"></param>
        /// <param name="USER_ID"></param>
        /// <param name="HOST_ID"></param>
        /// <param name="OUTINVOICE_NO"></param>
        /// <param name="OUTMSGCODE"></param>
        /// <param name="OUTMESSAGE"></param>
        public void PK_INVOICE_SP_INVOICE_NO(string INSTORE_NO, string INPOS_MASTER_UUID, string USER_ID, string HOST_ID, ref string OUTINVOICE_NO, ref string OUTMSGCODE, ref string OUTMESSAGE)
        {
            OracleCommand oraCmd = new OracleCommand("PK_INVOICE.SP_INVOICE_NO");
            oraCmd.CommandType = System.Data.CommandType.StoredProcedure;
            oraCmd.Parameters.Add(new OracleParameter("INSTORE_NO", OracleType.VarChar, 2000)).Value = INSTORE_NO;
            oraCmd.Parameters.Add(new OracleParameter("INPOS_MASTER_UUID", OracleType.VarChar, 2000)).Value = INPOS_MASTER_UUID;
            oraCmd.Parameters.Add(new OracleParameter("USER_ID", OracleType.VarChar, 2000)).Value = USER_ID;
            oraCmd.Parameters.Add(new OracleParameter("HOST_ID", OracleType.VarChar, 2000)).Value = HOST_ID;
            oraCmd.Parameters.Add(new OracleParameter("OUTINVOICE_NO", OracleType.VarChar, 2000)).Direction = ParameterDirection.Output;
            oraCmd.Parameters.Add(new OracleParameter("OUTMSGCODE", OracleType.VarChar, 2000)).Direction = ParameterDirection.Output;
            oraCmd.Parameters.Add(new OracleParameter("OUTMESSAGE", OracleType.VarChar, 2000)).Direction = ParameterDirection.Output;
            OracleConnection oConn = OracleDBUtil.GetConnection();
            try
            {
                oraCmd.Connection = oConn;
                oraCmd.ExecuteNonQuery();
                OUTINVOICE_NO = oraCmd.Parameters["OUTINVOICE_NO"].Value.ToString();
                OUTMSGCODE = oraCmd.Parameters["OUTMSGCODE"].Value.ToString();
                OUTMESSAGE = oraCmd.Parameters["OUTMESSAGE"].Value.ToString();
            }
            finally
            {
                oraCmd.Dispose();
                if (oConn.State == ConnectionState.Open)
                    oConn.Close();
                oConn.Dispose();
                OracleConnection.ClearAllPools();
            }
        }


        /// <summary>
        /// 驗證員工密碼
        /// </summary>
        /// <param name="STORENO">門市編號</param>
        /// <param name="EMPNO">員工編號</param>
        /// <param name="PASSWD">員工密碼</param>
        /// <returns>string (1: 驗證通過  0:驗證不通過)</returns>
        public string CheckPassword(string STORENO, string EMPNO, string PASSWD)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(@"SELECT EMPNO
                            FROM STORE_HEADER_DISCOUNT_PWD
                            WHERE STATUS = '1' --已啟用
                              AND to_date(to_char(sysdate, 'yyyy/mm/dd'),'yyyy/mm/dd') --尚未離職
                                  Between NVL(VALID_START_DATE, to_date('2000/01/01','YYYY/MM/DD')) And NVL(VALID_END_DATE,to_date('3000/01/01','YYYY/MM/DD')) --有效開始日
                              AND EMPNO = " + OracleDBUtil.SqlStr(EMPNO) + @"
                              AND ENCRYPT_PASSWORD = " + OracleDBUtil.SqlStr(PASSWD)
                        );

            DataTable dt = OracleDBUtil.Query_Data(sb.ToString());

            string IsReturn = "0";
            if (dt.Rows.Count > 0)
            {
                IsReturn = "1";
            }

            return IsReturn;
        }

        /// <summary>
        /// 驗證門市特殊折扣密碼
        /// </summary>
        /// <param name="STORENO">門市編號</param>
        /// <param name="PASSWD">密碼</param>
        /// <returns>string (1: 驗證通過  0:驗證不通過)</returns>
        public string CheckStoreDiscountPassword(string STORENO, string PASSWD)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(@"SELECT STORENAME
                            FROM STORE
                            WHERE to_date(to_char(sysdate, 'yyyy/mm/dd'),'yyyy/mm/dd') --門市正常營業
                                  Between to_date(NVL(STARTDATE, '20000101'),'yyyymmdd') And to_date(NVL(CLOSEDATE,'30000101'),'yyyymmdd') --有效開始日
                              AND STORE_NO = " + OracleDBUtil.SqlStr(STORENO) + @"
                              AND STORE_PW = " + OracleDBUtil.SqlStr(PASSWD)
                          );

            DataTable dt = OracleDBUtil.Query_Data(sb.ToString());

            string IsReturn = "0";
            if (dt.Rows.Count > 0)
            {
                IsReturn = "1";
            }

            return IsReturn;

        }

        /// <summary>
        /// 取得 折抵原因 List
        /// </summary>
        /// <returns>DataTable</returns>
        public DataTable getStoreDISReason()
        {
            StringBuilder sb = new StringBuilder();
//            sb.AppendLine(@"SELECT STORE_DIS_REASON_ID
//                                 , STORE_DIS_REASON_DESC 
//                            FROM STORE_DIS_REASON 
//                        ");
            //**2010/05/03 Tina：折扣原因來源改從 DISCOUNT_MASTER 取得。
            sb.AppendLine(@"SELECT DISCOUNT_CODE AS STORE_DIS_REASON_ID
                                 , DISCOUNT_NAME AS STORE_DIS_REASON_DESC 
                              FROM DISCOUNT_MASTER 
                             WHERE DISCOUNT_TYPE = '4'
                               AND TRUNC(SYSDATE) >= TRUNC(S_DATE)
                               AND TRUNC(SYSDATE) <= TRUNC(NVL(E_DATE, SYSDATE))
                               AND DEL_FLAG='N'
                        ");

            DataTable dt = OracleDBUtil.Query_Data(sb.ToString());
            return dt;
        }

        /// <summary>
        /// 取得剩餘可折扣金額
        /// </summary>
        /// <param name="STORENO">門市編號</param>
        /// <param name="YYMM">交易日期的年月</param>
        /// <returns>剩餘可折扣金額</returns>
        public string getRemainingDiscountAmount(string STORENO, string YYMM)
        {
            OracleConnection objConn = null;
            OracleTransaction objTX = null;
            OracleCommand oraCmd = null;
            string Amount = "0";
            try
            {
                objConn = OracleDBUtil.GetConnection();
                objTX = objConn.BeginTransaction();

                oraCmd = new OracleCommand("SP_CHECK_DISAMOUNT");
                oraCmd.CommandType = System.Data.CommandType.StoredProcedure;

                oraCmd.Parameters.Add(new OracleParameter("inSTORENO", STORENO));
                oraCmd.Parameters.Add(new OracleParameter("inYYMM", YYMM));
                oraCmd.Parameters.Add(new OracleParameter("outDISAMOUNT", OracleType.VarChar, 2000)).Direction = ParameterDirection.Output;

                oraCmd.Connection = objConn;
                oraCmd.Transaction = objTX;
                oraCmd.ExecuteNonQuery();

                Amount = oraCmd.Parameters["outDISAMOUNT"].Value.ToString();

                return Amount;

            }
            catch (Exception ex)
            {
                objTX.Rollback();
                throw ex;
            }
            finally
            {
                objTX = null;
                if (objConn.State == ConnectionState.Open)
                    objConn.Close();
                objConn.Dispose();
                OracleConnection.ClearAllPools();
            }
        }

        /// <summary>
        /// 取得特殊抱怨折扣資訊
        /// </summary>
        /// <param name="STORENO">門市編號</param>
        /// <param name="YYMM">交易日期的年月</param>
        /// <param name="roleId">人員角色,1:店長,2:店員</param>
        /// <returns></returns>
        public string getSpecDisInfo(string STORENO, string YYMM, string roleId)
        {
            OracleConnection objConn = null;
            OracleTransaction objTX = null;
            OracleCommand oraCmd = null;
            string info = "";
            try
            {
                objConn = OracleDBUtil.GetConnection();
                objTX = objConn.BeginTransaction();

                oraCmd = new OracleCommand("SP_GET_DISINFO");
                oraCmd.CommandType = System.Data.CommandType.StoredProcedure;

                oraCmd.Parameters.Add(new OracleParameter("inSTORENO", STORENO));
                oraCmd.Parameters.Add(new OracleParameter("inYYMM", YYMM));
                oraCmd.Parameters.Add(new OracleParameter("inRole", roleId));
                oraCmd.Parameters.Add(new OracleParameter("outInfo", OracleType.VarChar, 2000)).Direction = ParameterDirection.Output;

                oraCmd.Connection = objConn;
                oraCmd.Transaction = objTX;
                oraCmd.ExecuteNonQuery();

                info = oraCmd.Parameters["outInfo"].Value.ToString();
                objTX.Commit();
                return info;

            }
            catch (Exception ex)
            {
                objTX.Rollback();
                throw ex;
            }
            finally
            {
                objTX = null;
                if (objConn.State == ConnectionState.Open)
                    objConn.Close();
                objConn.Dispose();
                OracleConnection.ClearAllPools();
            }
        }

        /// <summary>
        /// 取得剩餘可折扣金額
        /// </summary>
        /// <param name="STORENO">門市編號</param>
        /// <param name="YYMM">交易日期的年月</param>
        /// <param name="USED_AMT">店長此折抵的金額</param>
        /// <returns>影響筆數</returns>
        public int UpdateStoreSpecialDIS(string STORENO, string YYMM, string USED_AMT)
        {
            OracleConnection objConn = null;
            OracleTransaction objTX = null;
            OracleCommand oraCmd = null;
            int Amount = 0;
            try
            {
                objConn = OracleDBUtil.GetConnection();
                objTX = objConn.BeginTransaction();

                oraCmd = new OracleCommand("SP_UPDATE_STORE_SPECIAL_DIS");
                oraCmd.CommandType = System.Data.CommandType.StoredProcedure;

                oraCmd.Parameters.Add(new OracleParameter("inSTORENO", STORENO));
                oraCmd.Parameters.Add(new OracleParameter("inYYMM", YYMM));
                oraCmd.Parameters.Add(new OracleParameter("inUSED_AMT", USED_AMT));
                oraCmd.Parameters.Add(new OracleParameter("outAffectRow", OracleType.Number)).Direction = ParameterDirection.Output;

                oraCmd.Connection = objConn;
                oraCmd.Transaction = objTX;
                oraCmd.ExecuteNonQuery();

                Amount = Convert.ToInt32(oraCmd.Parameters["outAffectRow"].Value.ToString());

                return Amount;

            }
            catch (Exception ex)
            {
                objTX.Rollback();
                throw ex;
            }
            finally
            {
                oraCmd.Dispose();
                if (objConn.State == ConnectionState.Open)
                    objConn.Close();
                objConn.Dispose();
                OracleConnection.ClearAllPools();
            }
        }

        /// <summary>
        /// 取得剩餘可折扣金額
        /// </summary>
        /// <param name="objTX">外部傳入Transaction</param>
        /// <param name="STORENO">門市編號</param>
        /// <param name="YYMM">交易日期的年月</param>
        /// <param name="USED_AMT">店長此折抵的金額</param>
        /// <param name="roleId">角色,1:店長,2:店員</param>
        /// <returns>影響筆數</returns>
        public int UpdateStoreSpecialDIS(OracleTransaction objTX, string STORENO, string YYMM, string USED_AMT, string roleId)
        {
            OracleCommand oraCmd = null;
            int Amount = 0;
            try
            {
                oraCmd = new OracleCommand("SP_UPDATE_STORE_SPECIAL_DIS");
                oraCmd.CommandType = System.Data.CommandType.StoredProcedure;

                oraCmd.Parameters.Add(new OracleParameter("inSTORENO", STORENO));
                oraCmd.Parameters.Add(new OracleParameter("inYYMM", YYMM));
                oraCmd.Parameters.Add(new OracleParameter("inUSED_AMT", USED_AMT));
                oraCmd.Parameters.Add(new OracleParameter("inRole", roleId));
                oraCmd.Parameters.Add(new OracleParameter("outAffectRow", OracleType.Number)).Direction = ParameterDirection.Output;

                oraCmd.Connection = objTX.Connection;
                oraCmd.Transaction = objTX;
                oraCmd.ExecuteNonQuery();

                Amount = Convert.ToInt32(oraCmd.Parameters["outAffectRow"].Value.ToString());

                return Amount;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 取得HappyGo可折抵的門市促銷活動
        /// </summary>
        /// <param name="PROMOTION_COD">促銷代碼</param>
        /// <param name="STORE_NO">門市代碼</param>
        /// <param name="TRAN_DATE">營業日</param>
        /// <returns>DataTable</returns>
        public DataTable getPromotionAction(string PROMOTION_CODE, string STORE_NO, string TRAN_DATE)
        {
            //System.Text.StringBuilder sb = new System.Text.StringBuilder();
            //sb.Append("SELECT M.ACTIVITY_ID, M.ACTIVITY_NAME, M.MEMBER_CHECK_FLAG, M.USE_COUNT, M.U_BOUND, ");
            //sb.Append("D.EXCHANGE_NAME, D.DIVIDABLE_POINT, D.CONVERT_CURRENCY ");
            //sb.Append("FROM HG_CONVERTIBLE_RESTRICTED M ");
            //sb.Append("INNER JOIN HG_CONVERT_REST_EXCHANGE D ON M.ACTIVITY_ID = D.ACTIVITY_ID ");
            //sb.Append("WHERE M.PAY_OFF_TYPE ='1' ");  //促銷商品折抵活動
            //sb.Append("AND " + OracleDBUtil.DateStr(TRAN_DATE) + " >= M.S_DATE ");
            //sb.Append("AND " + OracleDBUtil.DateStr(TRAN_DATE) + " < NVL(TRUNC(M.E_DATE),TO_DATE('9999/12/31','YYYY/MM/DD'))  ");
            //sb.Append("AND M.ACTIVITY_ID in (select ACTIVITY_ID from HG_CONVERT_REST_MM WHERE PROMOTE_COD = " + OracleDBUtil.SqlStr(PROMOTION_CODE) + ") ");
            //sb.Append("AND M.ACTIVITY_ID in (select ACTIVITY_ID from HG_CONVERT_REST_STORE WHERE STORE_NO =  " + OracleDBUtil.SqlStr(STORE_NO) + ") ");
            //sb.Append("ORDER BY D.DIVIDABLE_POINT DESC "); //兌換點數遞減排序，以方便判斷計算剩餘點數兌換點數最大消耗數

            StringBuilder sb = new StringBuilder();
            sb.AppendLine(@"SELECT " + OracleDBUtil.SqlStr(PROMOTION_CODE) + @" as PROMOTION_CODE, 
                                   (Select promo_name 
                                      from mm 
                                     where promo_no = " + OracleDBUtil.SqlStr(PROMOTION_CODE) + @" 
                                       And PROMO_STATUS = '1' 
                                       And to_date(to_char(sysdate, 'yyyy/mm/dd'),'yyyy/mm/dd') Between NVL(B_DATE, to_date('2000/01/01','YYYY/MM/DD')) 
                                       And NVL(E_DATE,to_date('3000/01/01','YYYY/MM/DD'))
                                    ) as promo_name,
                                    m.activity_id,
                                    m.activity_no,
                                    m.activity_name, 
                                    m.member_check_flag,
                                    m.use_count, 
                                    m.pay_off_type, 
                                    m.u_bound, 
                                    m.consume_count, 
                                    d.exchange_name,
                                    d.dividable_point, 
                                    d.convert_currency 
                            FROM   hg_convertible_restricted m, hg_convert_rest_exchange d 
                            WHERE  m.convert_rest_type = '2' --促銷商品折抵活動
                              AND  m.activity_id = d.activity_id(+) 
                              AND  " + OracleDBUtil.DateStr(TRAN_DATE) + @" >= m.s_date AND " + OracleDBUtil.DateStr(TRAN_DATE) + @" < Nvl(Trunc(m.e_date), To_date('9999/12/31', 'YYYY/MM/DD'))
                              AND  m.activity_id IN (SELECT activity_id FROM hg_convert_rest_mm WHERE promote_code = " + OracleDBUtil.SqlStr(PROMOTION_CODE) + @") 
                              AND  m.activity_id IN (SELECT activity_id FROM hg_convert_rest_store WHERE store_no = " + OracleDBUtil.SqlStr(STORE_NO) + @")
                            ORDER BY D.DIVIDABLE_POINT DESC "); //兌換點數遞減排序，以方便判斷計算剩餘點數兌換點數最大消耗數

            DataTable dt = OracleDBUtil.Query_Data(sb.ToString());
            return dt;
        }

        /// <summary>
        /// 取得HappyGo可加價購的商品的門市促銷活動
        /// </summary>
        /// <param name="PROMOTION_COD">促銷代碼</param>
        /// <param name="STORE_NO">門市代碼</param>
        /// <param name="TRAN_DATE">營業日</param>
        /// <returns>DataTable</returns>
        public DataTable getExtraSalePromotionAction(string PROMOTION_CODE, string STORE_NO, string TRAN_DATE)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(@"SELECT " + OracleDBUtil.SqlStr(PROMOTION_CODE) + @" AS PROMOTION_CODE 
                                 , (SELECT PROMO_NAME FROM MM WHERE PROMO_NO = 'PDD00EP4YEY3'And PROMO_STATUS = '1') AS PROMO_NAME 
                                 , M.ACTIVITY_ID, M.ACTIVITY_NAME, M.USE_COUNT, M.PAY_OFF_TYPE, M.U_BOUND 
                                 , M.MEMBER_CHECK_FLAG 
                                 , M.CONSUME_COUNT 
                                 , D.PRODNO 
                                 , (SELECT PRODNAME FROM PRODUCT WHERE PRODNO = D.PRODNO) AS PRODNAME
                                 , D.DIVIDABLE_POINT 
                                 , D.EXTRA_SALE_PRICE 
                            FROM   HG_CONVERTIBLE_RESTRICTED M, HG_CONVERT_EXTRA_SALE D 
                            WHERE  M.CONVERT_REST_TYPE = '2'
                              AND  M.ACTIVITY_ID = D.ACTIVITY_ID(+) 
                              AND  " + OracleDBUtil.DateStr(TRAN_DATE) + @" >= m.s_date 
                              AND  " + OracleDBUtil.DateStr(TRAN_DATE) + @" < Nvl(Trunc(m.e_date), To_date('9999/12/31', 'YYYY/MM/DD'))
                              AND  M.ACTIVITY_ID IN (SELECT ACTIVITY_ID FROM HG_CONVERT_REST_MM WHERE PROMOTE_CODE = " + OracleDBUtil.SqlStr(PROMOTION_CODE) + @") 
                              AND  M.ACTIVITY_ID IN (SELECT ACTIVITY_ID FROM HG_CONVERT_REST_STORE WHERE STORE_NO = " + OracleDBUtil.SqlStr(STORE_NO) + @") 
                            ORDER BY D.DIVIDABLE_POINT DESC "); //兌換點數遞減排序，以方便判斷計算剩餘點數兌換點數最大消耗數

            DataTable dt = OracleDBUtil.Query_Data(sb.ToString());
            return dt;

        }

        /// <summary>
        /// 取得HappyGo可折抵的門市單商品折抵活動
        /// </summary>
        /// <param name="PRODNO">商品料號</param>
        /// <param name="STORE_NO">門市代碼</param>
        /// <param name="TRAN_DATE">營業日</param>
        /// <returns>DataTable</returns>
        public DataTable getProductAction(string PRODNO, string STORE_NO, string TRAN_DATE)
        {
            //System.Text.StringBuilder sb = new System.Text.StringBuilder();
            //sb.Append("SELECT M.ACTIVITY_ID, M.ACTIVITY_NAME, M.USE_COUNT, M.U_BOUND, ");
            //sb.Append("D.EXCHANGE_NAME, D.DIVIDABLE_POINT, D.CONVERT_CURRENCY ");
            //sb.Append("FROM HG_CONVERTIBLE_RESTRICTED M ");
            //sb.Append("INNER JOIN HG_CONVERT_REST_EXCHANGE D ON M.ACTIVITY_ID = D.ACTIVITY_ID ");
            //sb.Append("WHERE M.PAY_OFF_TYPE ='2' ");  //單商品折抵活動
            //sb.Append("AND " + OracleDBUtil.DateStr(TRAN_DATE) + " >= M.S_DATE ");
            //sb.Append("AND " + OracleDBUtil.DateStr(TRAN_DATE) + " < NVL(TRUNC(M.E_DATE),TO_DATE('9999/12/31','YYYY/MM/DD'))  ");
            //sb.Append("AND M.ACTIVITY_ID IN (SELECT ACTIVITY_ID FROM HG_CONVERT_REST_PROD WHERE PRODNO = " + OracleDBUtil.SqlStr(PRODNO) + ") ");
            //sb.Append("AND M.ACTIVITY_ID IN (SELECT ACTIVITY_ID FROM HG_CONVERT_REST_STORE WHERE STORE_NO =  " + OracleDBUtil.SqlStr(STORE_NO) + ") ");
            //sb.Append("ORDER BY D.DIVIDABLE_POINT DESC "); //兌換點數遞減排序，以方便判斷計算剩餘點數兌換點數最大消耗數

            StringBuilder sb = new StringBuilder();
            sb.AppendLine(@"SELECT m.activity_no, 
                                   m.activity_name,
                                   m.use_count, 
                                   m.pay_off_type, 
                                   m.u_bound,
                                   m.member_check_flag,
                                   m.consume_count,
                                   d.exchange_name,
                                   d.dividable_point,
                                   d.convert_currency
                            FROM   hg_convertible_restricted m, hg_convert_rest_exchange d 
                            WHERE  m.convert_rest_type = '1' --單商品折抵活動
                              AND  m.activity_id = d.activity_id 
                              AND " + OracleDBUtil.DateStr(TRAN_DATE) + @" >= m.s_date
                              AND " + OracleDBUtil.DateStr(TRAN_DATE) + @" < Nvl(Trunc(m.e_date), To_date('9999/12/31', 'YYYY/MM/DD'))
                              AND m.activity_id IN (SELECT ACTIVITY_ID FROM HG_CONVERT_REST_PROD WHERE PRODNO = " + OracleDBUtil.SqlStr(PRODNO) + @")
                              AND m.activity_id IN (SELECT ACTIVITY_ID FROM HG_CONVERT_REST_STORE WHERE STORE_NO =  " + OracleDBUtil.SqlStr(STORE_NO) + @") 
                            ORDER BY D.DIVIDABLE_POINT DESC "); //兌換點數遞減排序，以方便判斷計算剩餘點數兌換點數最大消耗數

            DataTable dt = OracleDBUtil.Query_Data(sb.ToString());
            return dt;

        }

        /// <summary>
        /// 取得HappyGo可折抵的一般兌點通則
        /// </summary>
        /// <param name="TRAN_DATE">營業日</param>
        /// <returns>DataTable</returns>
        public DataTable getConvertAction(string TRAN_DATE, string HG_EXCHANGE_TYPE)
        {
            //System.Text.StringBuilder sb = new System.Text.StringBuilder();
            //sb.Append("SELECT CONVERT_NO, CONVERT_NAME, DIVIDABLE_POINT,CONVERT_CURRENCY , S_DATE, E_DATE ");
            //sb.Append("FROM HG_CONVERTIBLE ");
            //sb.Append("WHERE HG_EXCHANGE_TYPE = '1'  ");  //銷售類
            //sb.Append("AND " + OracleDBUtil.DateStr(TRAN_DATE) + " >= S_DATE ");
            //sb.Append("AND " + OracleDBUtil.DateStr(TRAN_DATE) + " < NVL(TRUNC(E_DATE),TO_DATE('9999/12/31','YYYY/MM/DD')) ");
            //sb.Append("ORDER BY DIVIDABLE_POINT DESC "); //兌換點數遞減排序，以方便判斷計算剩餘點數兌換點數最大消耗數

            StringBuilder sb = new StringBuilder();
            sb.AppendLine(@"SELECT d.DISCOUNT_CODE, 
                                   d.DISCOUNT_NAME,
                                   h.CONVERT_NO, 
                                   h.CONVERT_NAME, 
                                   h.DIVIDABLE_POINT, 
                                   h.CONVERT_CURRENCY, 
                                   h.S_DATE,
                                   h.E_DATE 
                            FROM   HG_CONVERTIBLE h, DISCOUNT_MASTER d 
                            WHERE  h.HG_EXCHANGE_TYPE = " + OracleDBUtil.SqlStr(HG_EXCHANGE_TYPE) + @"
                              AND  h.CONVERT_NO = d.DISCOUNT_MASTER_ID 
                              AND  " + OracleDBUtil.DateStr(TRAN_DATE) + @" >= h.S_DATE 
                              AND  " + OracleDBUtil.DateStr(TRAN_DATE) + @" < Nvl(Trunc(h.E_DATE), To_date('9999/12/31', 'YYYY/MM/DD')) 
                              AND  d.DEL_FLAG = 'N' 
                              AND  d.DISCOUNT_TYPE = '5' 
                              AND  " + OracleDBUtil.DateStr(TRAN_DATE) + @" >= Trunc(d.S_DATE) 
                              AND  " + OracleDBUtil.DateStr(TRAN_DATE) + @" < Nvl(Trunc(d.E_DATE), To_date('9999/12/31', 'YYYY/MM/DD')) 
                            ORDER BY h.DIVIDABLE_POINT DESC 
                        ");

            DataTable dt = OracleDBUtil.Query_Data(sb.ToString());
            return dt;

        }

        /// <summary>
        /// 取得HappyGo可加價購的商品
        /// </summary>
        /// <param name="PRODNO"></param>
        /// <param name="STORE_NO"></param>
        /// <param name="TRAN_DATE"></param>
        /// <returns></returns>
        public DataTable geteExtraSaleAction(string PRODNO, string STORE_NO, string TRAN_DATE)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(@"SELECT M.ACTIVITY_ID
                                   , M.ACTIVITY_NAME
                                   , M.USE_COUNT
                                   , M.PAY_OFF_TYPE
                                   , M.U_BOUND
                                   , M.MEMBER_CHECK_FLAG
                                   , M.CONSUME_COUNT 
                                   , D.PRODNO 
                                   , (SELECT PRODNAME FROM PRODUCT WHERE PRODNO = D.PRODNO) AS PRODNAME 
                                   , D.DIVIDABLE_POINT 
                                   , D.EXTRA_SALE_PRICE 
                            FROM   HG_CONVERTIBLE_RESTRICTED M, HG_CONVERT_EXTRA_SALE D 
                            WHERE  M.ACTIVITY_ID = D.ACTIVITY_ID 
                              AND  " + OracleDBUtil.DateStr(TRAN_DATE) + @" >= m.s_date 
                              AND  " + OracleDBUtil.DateStr(TRAN_DATE) + @" < Nvl(Trunc(m.e_date), To_date('9999/12/31', 'YYYY/MM/DD'))
                              AND  m.activity_id IN (SELECT ACTIVITY_ID FROM HG_CONVERT_REST_PROD WHERE PRODNO = " + OracleDBUtil.SqlStr(PRODNO) + @") 
                              AND  m.activity_id IN (SELECT ACTIVITY_ID FROM HG_CONVERT_REST_STORE WHERE STORE_NO =  " + OracleDBUtil.SqlStr(STORE_NO) + @") 
                            ORDER BY D.DIVIDABLE_POINT DESC "); //兌換點數遞減排序，以方便判斷計算剩餘點數兌換點數最大消耗數

            DataTable dt = OracleDBUtil.Query_Data(sb.ToString());
            return dt;
        }

        /// <summary>
        /// 判斷手機門號是否為該促銷活動的會員，若是，則回傳SID以及已折抵的次數
        /// </summary>
        /// <param name="ACTIVITY_ID">活動UUID</param>
        /// <param name="MSISDN">手機門號</param>
        /// <returns>DataTable</returns>
        public DataTable getUSE_COUNT(string ACTIVITY_ID, string MSISDN)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(@"SELECT SID, USE_COUNT
                            FROM   HG_CONVERT_MEMBER_LIST 
                            WHERE  STATUS = '0' --有效會員
                              AND  ACTIVITY_ID = " + OracleDBUtil.SqlStr(ACTIVITY_ID) + @"
                              AND  MSISDN = " + OracleDBUtil.SqlStr(MSISDN)
                         );

            DataTable dt = OracleDBUtil.Query_Data(sb.ToString());
            return dt;
        }

        /// <summary>
        /// 取得該門市促銷代碼相關組合促銷商品
        /// </summary>
        /// <param name="Store_No">門市代碼</param>
        /// <param name="Promotion_Code">促銷代碼</param>
        /// <param name="ProdNo">商品編號</param>
        /// <returns>DataTable</returns>
        public DataTable getMixPromotion_Item(string Store_No, string Promotion_Code, string ProdNo)
        {
            OracleConnection objConn = null;
            OracleCommand oraCmd = null;
            OracleDataReader oraReader = null;
            
            try
            {
                objConn = OracleDBUtil.GetConnection();
                oraCmd = new OracleCommand("SP_Query_Promotion");
                oraCmd.CommandType = System.Data.CommandType.StoredProcedure;
                oraCmd.Parameters.Add(new OracleParameter("v_Promotion_Code", Promotion_Code));
                oraCmd.Parameters.Add(new OracleParameter("v_Item_Code", ProdNo));
                oraCmd.Parameters.Add(new OracleParameter("v_Employee_Id", ""));
                oraCmd.Parameters.Add(new OracleParameter("v_Sys_Id", ""));
                oraCmd.Parameters.Add(new OracleParameter("v_Stord_Id", Store_No));
                oraCmd.Parameters.Add(new OracleParameter("o_data", OracleType.Cursor)).Direction = ParameterDirection.Output;

                oraCmd.Connection = objConn;
                oraReader = oraCmd.ExecuteReader();

                DataTable dt = new DataTable();

                if (oraReader != null)
                {
                    DataTable dtSchema = oraReader.GetSchemaTable();
                    foreach (DataRow drSchema in dtSchema.Rows)
                    {
                        if (drSchema["ColumnName"] != null)
                            dt.Columns.Add(drSchema["ColumnName"].ToString());
                    }
                    while (oraReader.Read())
                    {
                        DataRow dr = dt.NewRow();
                        for (int i = 0; i < dtSchema.Rows.Count; i++)
                            dr[i] = oraReader.GetValue(i);
                        dt.Rows.Add(dr);
                    }
                }

                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (oraReader != null)
                    oraReader.Dispose();
                oraCmd.Dispose();
                if (objConn.State == ConnectionState.Open)
                    objConn.Close();
                objConn.Dispose();
                OracleConnection.ClearAllPools();
            }
        }

        /// <summary>
        /// 取得該門市促銷代碼相關組合促銷商品折扣
        /// </summary>
        /// <param name="Store_No">門市代碼</param>
        /// <param name="Promotion_Code">促銷代碼</param>
        /// <param name="ProdNo">商品編號</param>
        /// <returns>DataTable</returns>
        public DataTable getMixPromotion_ItemDiscount(string Store_No, string EmployeeId, string Promotion_Code, DataRow drDetail)
        {
            OracleConnection objConn = null;
            OracleCommand oraCmd = null;
            OracleDataReader oraReader = null;

            try
            {
                objConn = OracleDBUtil.GetConnection();
                oraCmd = new OracleCommand("sp_query_discount_ws");
                oraCmd.CommandType = System.Data.CommandType.StoredProcedure;
                oraCmd.Parameters.Add(new OracleParameter("v_msisdn", drDetail["MSISDN"]));
                oraCmd.Parameters.Add(new OracleParameter("v_r_rate", drDetail["R_RATE"]));
                oraCmd.Parameters.Add(new OracleParameter("v_promotion_code", Promotion_Code));
                oraCmd.Parameters.Add(new OracleParameter("v_item_code", drDetail["ProdNo"]));
                oraCmd.Parameters.Add(new OracleParameter("v_data", drDetail["DATA"]));
                oraCmd.Parameters.Add(new OracleParameter("v_voice", drDetail["VOICE"]));
                oraCmd.Parameters.Add(new OracleParameter("v_trans_type", drDetail["TRANS_TYPE"]));
                oraCmd.Parameters.Add(new OracleParameter("v_mnp", drDetail["MNP"]));
                oraCmd.Parameters.Add(new OracleParameter("v_bundle_type", drDetail["BUNDLE_TYPE"]));
                oraCmd.Parameters.Add(new OracleParameter("v_employee_id", EmployeeId));
                oraCmd.Parameters.Add(new OracleParameter("v_sys_id", drDetail["SOURCE_TYPE"]));
                oraCmd.Parameters.Add(new OracleParameter("v_store_id", Store_No));
                oraCmd.Parameters.Add(new OracleParameter("v_msgcode", OracleType.VarChar, 2000)).Direction = ParameterDirection.Output;
                oraCmd.Parameters.Add(new OracleParameter("v_message", OracleType.VarChar, 2000)).Direction = ParameterDirection.Output;
                oraCmd.Parameters.Add(new OracleParameter("o_data", OracleType.Cursor)).Direction = ParameterDirection.Output;

                oraCmd.Connection = objConn;
                oraReader = oraCmd.ExecuteReader();

                DataTable dt = new DataTable();

                if (oraReader != null)
                {
                    DataTable dtSchema = oraReader.GetSchemaTable();
                    foreach (DataRow drSchema in dtSchema.Rows)
                    {
                        if (drSchema["ColumnName"] != null)
                            dt.Columns.Add(drSchema["ColumnName"].ToString());
                    }
                    while (oraReader.Read())
                    {
                        DataRow dr = dt.NewRow();
                        for (int i = 0; i < dtSchema.Rows.Count; i++)
                            dr[i] = oraReader.GetValue(i);
                        dt.Rows.Add(dr);
                    }
                }

                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (oraReader != null)
                    oraReader.Dispose();
                oraCmd.Dispose();
                if (objConn.State == ConnectionState.Open)
                    objConn.Close();
                objConn.Dispose();
                OracleConnection.ClearAllPools();
            }
        }

        /// <summary>
        /// 取得該門市促銷代碼相關組合促銷商品折扣
        /// </summary>
        /// <param name="Store_No">門市代碼</param>
        /// <param name="Promotion_Code">促銷代碼</param>
        /// <param name="ProdNo">商品編號</param>
        /// <returns>DataTable</returns>
        public DataTable getMixPromotion_ItemDiscount(string Store_No, string EmployeeId, string Promotion_Code, string MSISDM, string R_RATE, 
                                                        string DATA, string VOICE, string TRANS_TYPE, string MNP, string BUNDLE_TYPE, string SOURCET_TYPE, 
                                                        string PRODNO)
        {
            OracleConnection objConn = null;
            OracleCommand oraCmd = null;
            OracleDataReader oraReader = null;

            try
            {
                objConn = OracleDBUtil.GetConnection();
                oraCmd = new OracleCommand("sp_query_discount_ws");
                oraCmd.CommandType = System.Data.CommandType.StoredProcedure;
                oraCmd.Parameters.Add(new OracleParameter("v_msisdn", MSISDM));
                oraCmd.Parameters.Add(new OracleParameter("v_r_rate", R_RATE));
                oraCmd.Parameters.Add(new OracleParameter("v_promotion_code", Promotion_Code));
                oraCmd.Parameters.Add(new OracleParameter("v_item_code", PRODNO));
                oraCmd.Parameters.Add(new OracleParameter("v_data", DATA));
                oraCmd.Parameters.Add(new OracleParameter("v_voice", VOICE));
                oraCmd.Parameters.Add(new OracleParameter("v_trans_type", TRANS_TYPE));
                oraCmd.Parameters.Add(new OracleParameter("v_mnp", MNP));
                oraCmd.Parameters.Add(new OracleParameter("v_bundle_type", BUNDLE_TYPE));
                oraCmd.Parameters.Add(new OracleParameter("v_employee_id", EmployeeId));
                oraCmd.Parameters.Add(new OracleParameter("v_sys_id", SOURCET_TYPE));
                oraCmd.Parameters.Add(new OracleParameter("v_store_id", Store_No));
                oraCmd.Parameters.Add(new OracleParameter("v_msgcode", OracleType.VarChar, 2000)).Direction = ParameterDirection.Output;
                oraCmd.Parameters.Add(new OracleParameter("v_message", OracleType.VarChar, 2000)).Direction = ParameterDirection.Output;
                oraCmd.Parameters.Add(new OracleParameter("o_data", OracleType.Cursor)).Direction = ParameterDirection.Output;

                oraCmd.Connection = objConn;
                oraReader = oraCmd.ExecuteReader();

                DataTable dt = new DataTable();

                if (oraReader != null)
                {
                    DataTable dtSchema = oraReader.GetSchemaTable();
                    foreach (DataRow drSchema in dtSchema.Rows)
                    {
                        if (drSchema["ColumnName"] != null)
                            dt.Columns.Add(drSchema["ColumnName"].ToString());
                    }
                    while (oraReader.Read())
                    {
                        DataRow dr = dt.NewRow();
                        for (int i = 0; i < dtSchema.Rows.Count; i++)
                            dr[i] = oraReader.GetValue(i);
                        dt.Rows.Add(dr);
                    }
                }

                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (oraReader != null)
                    oraReader.Dispose();
                oraCmd.Dispose();
                if (objConn.State == ConnectionState.Open)
                    objConn.Close();
                objConn.Dispose();
                OracleConnection.ClearAllPools();
            }
        }

        /// <summary>
        /// 取得更新人員新格式 員編 員工姓名
        /// </summary>
        /// <param name="empno">員編</param>
        /// <returns>string</returns>
        public string getMODI_USER(string empno)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(@"SELECT empno || ' ' || empname 
                            FROM   employee 
                            WHERE  ISACTIVE = '1' --有效會員
                              AND  OUTDATE is null 
                              AND  empno = " + OracleDBUtil.SqlStr(empno)
                        );

            DataTable dt = OracleDBUtil.Query_Data(sb.ToString());

            if (dt != null && dt.Rows.Count > 0)
                if (dt.Rows[0][0] != null && (!string.IsNullOrEmpty(dt.Rows[0][0].ToString())))
                    return dt.Rows[0][0].ToString();
            return empno;
        }

        /// <summary>
        /// 取得折扣的折扣類別
        /// </summary>
        /// <param name="Discount_Id">折扣料號</param>
        /// <returns>string</returns>
        public string getDISCOUNT_TYPE(string Discount_Code)
        {
            string discountType = "1";
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(@"SELECT DISCOUNT_TYPE 
                            FROM   discount_master 
                            WHERE   to_date(to_char(sysdate, 'yyyy/mm/dd'),'yyyy/mm/dd') Between NVL(S_DATE, to_date('2000/01/01','YYYY/MM/DD')) And NVL(E_DATE,to_date('3000/01/01','YYYY/MM/DD'))
                             And   DISCOUNT_CODE = " + OracleDBUtil.SqlStr(Discount_Code)
                        );

            DataTable dt = OracleDBUtil.Query_Data(sb.ToString());

            if (dt != null && dt.Rows.Count > 0)
                if (dt.Rows[0][0] != null && !string.IsNullOrEmpty(dt.Rows[0][0].ToString()))
                    discountType = dt.Rows[0][0].ToString();

            return discountType;
        }

        /// <summary>
        /// 變更兌點項目累計折抵次數
        /// </summary>
        /// <param name="DISCOUNT_CODE">折扣料號</param>
        /// <param name="MSISDN">門號</param>
        /// <returns>影響筆數</returns>
        public int UpdateMemberList(string DISCOUNT_CODE, string MSISDN)
        {
            OracleConnection objConn = null;
            OracleTransaction objTX = null;
            OracleCommand oraCmd = null;
            int Amount = 0;
            try
            {
                objConn = OracleDBUtil.GetConnection();
                objTX = objConn.BeginTransaction();

                oraCmd = new OracleCommand("SP_UPDATE_MEMBERLIST");
                oraCmd.CommandType = System.Data.CommandType.StoredProcedure;
                oraCmd.Parameters.Add(new OracleParameter("v_DISCOUNT_CODE", DISCOUNT_CODE));
                oraCmd.Parameters.Add(new OracleParameter("v_MSISDN", MSISDN));
                oraCmd.Parameters.Add(new OracleParameter("outAffectRow", OracleType.Number)).Direction = ParameterDirection.Output;

                oraCmd.Connection = objConn;
                oraCmd.Transaction = objTX;
                oraCmd.ExecuteNonQuery();

                Amount = Convert.ToInt32(oraCmd.Parameters["outAffectRow"].Value.ToString());

                return Amount;

            }
            catch (Exception ex)
            {
                objTX.Rollback();
                throw ex;
            }
            finally
            {
                oraCmd.Dispose();
                if (objConn.State == ConnectionState.Open)
                    objConn.Close();
                objConn.Dispose();
                OracleConnection.ClearAllPools();
            }
        }

        /// <summary>
        /// 變更兌點項目累計折抵次數
        /// </summary>
        /// <param name="objTX">外部傳入Transaction</param>
        /// <param name="DISCOUNT_CODE">折扣料號</param>
        /// <param name="MSISDN">門號</param>
        /// <returns>影響筆數</returns>
        public int UpdateMemberList(OracleTransaction objTX, string DISCOUNT_CODE, string MSISDN)
        {
            OracleCommand oraCmd = null;
            int Amount = 0;
            try
            {
                oraCmd = new OracleCommand("SP_UPDATE_MEMBERLIST");
                oraCmd.CommandType = System.Data.CommandType.StoredProcedure;
                oraCmd.Parameters.Add(new OracleParameter("v_DISCOUNT_CODE", DISCOUNT_CODE));
                oraCmd.Parameters.Add(new OracleParameter("v_MSISDN", MSISDN));
                oraCmd.Parameters.Add(new OracleParameter("outAffectRow", OracleType.Number)).Direction = ParameterDirection.Output;

                oraCmd.Connection = objTX.Connection;
                oraCmd.Transaction = objTX;
                oraCmd.ExecuteNonQuery();

                Amount = Convert.ToInt32(oraCmd.Parameters["outAffectRow"].Value.ToString());

                return Amount;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// IMEI_LOG
        /// </summary>
        /// <param name="POSUUID_MASTER"></param>
        /// <returns></returns>
        public string IMEISale_Log(string POSUUID_MASTER)
        {
            OracleConnection objConn = null;
            OracleTransaction objTX = null;
            OracleCommand oraCmd = null;
            string strMCode = "";
            try
            {
                objConn = OracleDBUtil.GetConnection();
                objTX = objConn.BeginTransaction();

                oraCmd = new OracleCommand("SP_IMEISALE_LOG");
                oraCmd.CommandType = System.Data.CommandType.StoredProcedure;

                oraCmd.Parameters.Add(new OracleParameter("inPOSUUID_MASTER", POSUUID_MASTER));
                oraCmd.Parameters.Add(new OracleParameter("inCHANNEL_ID", "RETAIL"));
                oraCmd.Parameters.Add(new OracleParameter("outMSGCODE", OracleType.VarChar, 4000)).Direction = ParameterDirection.Output;
                oraCmd.Parameters.Add(new OracleParameter("outMESSAGE", OracleType.VarChar, 4000)).Direction = ParameterDirection.Output;
                oraCmd.Parameters.Add(new OracleParameter("outIMEI_LOG_ID", OracleType.VarChar, 4000)).Direction = ParameterDirection.Output;

                oraCmd.Connection = objConn;
                oraCmd.Transaction = objTX;
                oraCmd.ExecuteNonQuery();

                strMCode = oraCmd.Parameters["outMSGCODE"].Value.ToString() + "|" + oraCmd.Parameters["outMESSAGE"].Value.ToString();


            }
            catch //(Exception ex)
            {
                strMCode = "999|SP Error!"; //+ ex.Message.ToString();
                objTX.Rollback();
                //throw ex;
            }
            finally
            {
                oraCmd.Dispose();
                if (objConn.State == ConnectionState.Open)
                    objConn.Close();
                objConn.Dispose();
                OracleConnection.ClearAllPools();
            }

            return strMCode;

        }

        /// <summary>
        /// IMEI_LOG
        /// </summary>
        /// <param name="POSUUID_MASTER"></param>
        /// <returns></returns>
        public string IMEISale_Log(OracleTransaction objTX, string POSUUID_MASTER, string MODI_USER)
        {
            string strMCode = "";
            OracleCommand oraCmd = null;
            try
            {
                oraCmd = new OracleCommand("SP_IMEISALE_LOG");
                oraCmd.CommandType = System.Data.CommandType.StoredProcedure;

                oraCmd.Parameters.Add(new OracleParameter("inPOSUUID_MASTER", POSUUID_MASTER));
                oraCmd.Parameters.Add(new OracleParameter("inCHANNEL_ID", "RETAIL"));
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
        /// 銷售GL分錄
        /// </summary>
        /// <param name="POSUUID_MASTER">銷售單主鍵</param>
        /// <returns></returns>
        public string runGL(OracleTransaction objTX, string POSUUID_MASTER)
        {
            string strMCode = "";
            OracleCommand oraCmd = null;
            try
            {
                oraCmd = new OracleCommand("PK_GL.GE_Sale");
                oraCmd.CommandType = System.Data.CommandType.StoredProcedure;

                oraCmd.Parameters.Add(new OracleParameter("POSUUID_MASTER", POSUUID_MASTER));
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
        /// 取得店長折扣的折扣料號、名稱
        /// </summary>
        /// <param name="TRAN_DATE">營業日</param>
        /// <returns>DataTable</returns>
        public DataTable getCheckOutSM_DISCOUNTInfo(string TRAN_DATE)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(@"SELECT para_value
                                 , para_name 
                            FROM sys_para 
                            WHERE para_key ='STORE_SPECIAL_DIS_CODE' 
                        ");

            DataTable dt = OracleDBUtil.Query_Data(sb.ToString());
            return dt;
        }

        /// <summary>
        /// 取得FETC料號
        /// </summary>
        /// <returns>DataTable</returns>
        public String getFETCProuductNo()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("select para_value from sys_para where para_key = 'ETC_ITEM_CODE' ");

            DataTable dt = OracleDBUtil.Query_Data(sb.ToString());

            if (dt != null && dt.Rows.Count > 0)
                return dt.Rows[0][0].ToString();
            else
                return "";
        }

        /// <summary>
        /// 取得ETC加值卡商品料號
        /// </summary>
        /// <returns>DataTable</returns>
        public String getETCCardProuductNo()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("select para_value from sys_para where para_key = 'ETC_CARD' ");

            DataTable dt = OracleDBUtil.Query_Data(sb.ToString());

            if (dt != null && dt.Rows.Count > 0)
                return dt.Rows[0][0].ToString();
            else
                return "";
        }

        /// <summary>
        /// 取得ETC加值卡保證金商品料號
        /// </summary>
        /// <returns>DataTable</returns>
        public String getETCCardPromiseProuductNo()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("select para_value from sys_para where para_key = 'ETC_CARD_BAIL' ");

            DataTable dt = OracleDBUtil.Query_Data(sb.ToString());

            if (dt != null && dt.Rows.Count > 0)
                return dt.Rows[0][0].ToString();
            else
                return "";
        }

        /// <summary>
        /// 取得最低FETC加值金額
        /// </summary>
        /// <returns>DataTable</returns>
        public int getFETCLowLimitAmt()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("select para_value from sys_para where para_key = 'ETC_RECV_LIMIT' ");

            DataTable dt = OracleDBUtil.Query_Data(sb.ToString());

            if (dt != null && dt.Rows.Count > 0)
                return Convert.ToInt32(dt.Rows[0][0]);
            else
                return 0;
        }

        /// <summary>
        /// 取得發票檔案存放路徑
        /// </summary>
        /// <returns>DataTable</returns>
        public String getUploadPath()
        {
            string sqlStr = @"select para_value as MyValue from sys_para where para_key='INVOICE_DOWNLOAD_PATH' ";

            DataTable dt = OracleDBUtil.Query_Data(sqlStr);

            if (dt != null && dt.Rows.Count > 0)
                return dt.Rows[0][0].ToString();
            else
                return "";
        }

        /// <summary>
        /// 取得發票檔案存放路徑
        /// </summary>
        /// <returns>DataTable</returns>
        public String getUploadPath(string posuuid_master)
        {
            OracleConnection objConn = null;
            try
            {
                objConn = OracleDBUtil.GetConnection();
                string sqlStr = @"select para_value as MyValue from sys_para where para_key='INVOICE_DOWNLOAD_PATH' ";

                DataTable dt = OracleDBUtil.GetDataSet(objConn, sqlStr).Tables[0];

                if (dt != null && dt.Rows.Count > 0)
                {
                    string sysPath = dt.Rows[0][0].ToString();
                    sqlStr = @"select '/' || to_char(sysdate,'YYYYMMDD') || '/' || store_no from sale_head where posuuid_master = " 
                                + OracleDBUtil.SqlStr(posuuid_master);
                    dt = OracleDBUtil.GetDataSet(objConn, sqlStr).Tables[0];
                    if (dt != null && dt.Rows.Count > 0)
                        return sysPath + dt.Rows[0][0].ToString();
                    else
                        return "";
                }
                else
                    return "";
            }
            catch //(Exception ex)
            {
                return "";
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
        /// 更新交易狀態及交易序號
        /// </summary>
        /// <param name="SALE_NO">交易序號</param>
        /// <param name="SALE_STATUS">交易狀態</param>
        /// <param name="POSUUID_MASTER">交易主鍵值</param>
        /// <returns>影響筆數</returns>
        public int UpdateSaleHead(string SALE_NO, string SALE_STATUS, string POSUUID_MASTER)
        {
            string sqlStr = "";
            if (SALE_NO == "")
                sqlStr = "Update Sale_Head Set SALE_STATUS = " + OracleDBUtil.SqlStr(SALE_STATUS) 
                            + " Where POSUUID_MASTER = " + OracleDBUtil.SqlStr(POSUUID_MASTER);
            else
                sqlStr = "Update Sale_Head Set SALE_NO = " + OracleDBUtil.SqlStr(SALE_NO) + ", SALE_STATUS = " + OracleDBUtil.SqlStr(SALE_STATUS)
                            + " Where POSUUID_MASTER = " + OracleDBUtil.SqlStr(POSUUID_MASTER);

            OracleConnection objConn = null;
            try
            {
                objConn = OracleDBUtil.GetConnection();
                int ret = OracleDBUtil.ExecuteSql(objConn, sqlStr);
                return ret;
            }
            catch
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
        /// 更新交易狀態及交易序號
        /// </summary>
        /// <param name="objTX">外部傳入Transaction</param>
        /// <param name="SALE_NO">交易序號</param>
        /// <param name="SALE_STATUS">交易狀態</param>
        /// <param name="POSUUID_MASTER">交易主鍵值</param>
        /// <returns>影響筆數</returns>
        public int UpdateSaleHead(OracleTransaction objTX, string SALE_NO, string SALE_STATUS, string POSUUID_MASTER, string MODI_USER)
        {
            string sqlStr = "";
            if (SALE_NO == "")
                sqlStr = "Update Sale_Head Set SALE_STATUS = " + OracleDBUtil.SqlStr(SALE_STATUS)
                            + ", MODI_DTM = sysdate, MODI_USER = " + OracleDBUtil.SqlStr(MODI_USER) + " Where POSUUID_MASTER = " + OracleDBUtil.SqlStr(POSUUID_MASTER);
            else
                sqlStr = "Update Sale_Head Set SALE_NO = " + OracleDBUtil.SqlStr(SALE_NO) + ", SALE_STATUS = " + OracleDBUtil.SqlStr(SALE_STATUS)
                            + ", MODI_DTM = sysdate, MODI_USER = " + OracleDBUtil.SqlStr(MODI_USER) + " Where POSUUID_MASTER = " + OracleDBUtil.SqlStr(POSUUID_MASTER);

            try
            {
                int ret = OracleDBUtil.ExecuteSql(objTX, sqlStr);
                return ret;
            }
            catch
            {
                return -1;
            } 
        }

        /// <summary>
        /// Commit外部系統交易狀態
        /// </summary>
        /// <param name="SYSID">系統別</param>
        /// <param name="SERVICE_SYS_ID">外部系統主鍵值</param>
        /// <param name="POSUUID_MASTER">銷售主鍵值</param>
        /// <param name="POSUUID_DETAIL">未結清單主鍵值</param>
        /// <returns>結果:0 成功, -1 失敗</returns>
        public int CommitOuterSystem(string SYSID, string SERVICE_SYS_ID, string POSUUID_MASTER, string POSUUID_DETAIL, string Barcode1,
                                        string EMPLOYEE_ID, string STORE_ID, string bundle_id,  string barcode2, string barcode3)
        {
            string outerConnStr = "";
            string BOUouterConnStr = "";
            string outerCmd = "";
            string strMCode = "";
            string BouCmd = "";
            string BouID = "";
            string CheckFlag = "";
            //string SYSOK = "";
            string strLogMsg = "";
            string strBundleLogMsg = "";
            OracleConnection objConn = null;
            OracleCommand oraCmd = null;
            OracleConnection BouConn = null;
            OracleCommand BouoraCmd = null;
            try
            {
                objConn = Advtek.Utility.OracleDBUtil.GetConnection();
                if (SYSID != "")
                {
                    string strSQL = "Select para_value from sys_para where para_key = " + OracleDBUtil.SqlStr(SYSID + "_COMMIT");
                    DataTable dtSys = OracleDBUtil.GetDataSet(objConn, strSQL).Tables[0];
                    if (dtSys != null && dtSys.Rows.Count > 0)
                        if (dtSys.Rows[0][0] != null)
                            outerCmd = dtSys.Rows[0][0].ToString();
                }

                switch (SYSID)
                {
                    case "IA":
                        outerConnStr = OracleDBUtil.GetIAConnectionStringByTNSName();
                        break;
                    case "LOY":
                        outerConnStr = OracleDBUtil.GetLOYConnectionStringByTNSName();
                        break;
                    case "SSI":
                        outerConnStr = OracleDBUtil.GetSSIConnectionStringByTNSName();
                        break;
                    case "PY":
                        outerConnStr = OracleDBUtil.GetPAYMENTConnectionStringByTNSName();
                        break;
                    case "OLR":
                        outerConnStr = OracleDBUtil.GetOLRConnectionStringByTNSName();
                        break;
                    case "ES":
                        outerConnStr = OracleDBUtil.GetEStoreConnectionStringByTNSName();
                        break;
                }

                if (bundle_id != "")
                {


                    BOUouterConnStr = OracleDBUtil.GetBOUConnectionStringByTNSName();
                   
                    BouID = "BOU";
                    string strSQL = "Select para_value from sys_para where para_key = " + OracleDBUtil.SqlStr(BouID + "_COMMIT");
                    DataTable dt1 = OracleDBUtil.GetDataSet(objConn, strSQL).Tables[0];
                    if (dt1 != null && dt1.Rows.Count > 0)
                        if (dt1.Rows[0][0] != null)
                            BouCmd = dt1.Rows[0][0].ToString();

                    BouConn = OracleDBUtil.GetConnectionByConnString(BOUouterConnStr);
                    BouoraCmd = new OracleCommand(BouCmd);
                    BouoraCmd.CommandType = System.Data.CommandType.StoredProcedure;
                }
         
                if (outerCmd != "" || BouCmd !="" )
                {
                    objConn = OracleDBUtil.GetConnectionByConnString(outerConnStr);
                    oraCmd = new OracleCommand(outerCmd);
                    oraCmd.CommandType = System.Data.CommandType.StoredProcedure;

                    switch (SYSID)
                    {
                        case "IA":
                            oraCmd.Parameters.Add(new OracleParameter("ACTI_NO", OracleType.VarChar, 2000)).Value = SERVICE_SYS_ID;
                            oraCmd.Parameters.Add(new OracleParameter("UUID_MASTER", OracleType.VarChar, 2000)).Value = POSUUID_MASTER;
                            oraCmd.Parameters.Add(new OracleParameter("STATUS", OracleType.VarChar, 4000)).Direction = ParameterDirection.Output;
                            oraCmd.Parameters.Add(new OracleParameter("ERR_CODE", OracleType.Number)).Direction = ParameterDirection.Output;
                            oraCmd.Parameters.Add(new OracleParameter("ERR_MESG", OracleType.VarChar, 4000)).Direction = ParameterDirection.Output;
                            strLogMsg = ",ACTI_NO=" + SERVICE_SYS_ID + ", UUID_MASTER=" + POSUUID_MASTER;
                            break;
                        case "LOY":
                            oraCmd.Parameters.Add(new OracleParameter("POSuuid_Details", OracleType.VarChar, 2000)).Value = POSUUID_DETAIL;
                            oraCmd.Parameters.Add(new OracleParameter("image_number", OracleType.VarChar, 2000)).Value = SERVICE_SYS_ID;
                            oraCmd.Parameters.Add(new OracleParameter("vPOSuuid_Master", OracleType.VarChar, 2000)).Value = POSUUID_MASTER;
                            oraCmd.Parameters.Add(new OracleParameter("msg", OracleType.VarChar, 2000)).Direction = ParameterDirection.Output;
                            strLogMsg = ",POSuuid_Details=" + POSUUID_DETAIL + ",image_number=" + SERVICE_SYS_ID + ", vPOSuuid_Master=" + POSUUID_MASTER;
                            break;
                        case "SSI":
                            oraCmd.Parameters.Add(new OracleParameter("image_number", OracleType.VarChar, 2000)).Value = SERVICE_SYS_ID;
                            oraCmd.Parameters.Add(new OracleParameter("POSuuid_Master", OracleType.VarChar, 2000)).Value = POSUUID_MASTER;
                            oraCmd.Parameters.Add(new OracleParameter("result", OracleType.VarChar, 2000)).Direction = ParameterDirection.Output;
                            strLogMsg = ",image_number=" + SERVICE_SYS_ID + ", POSuuid_Master=" + POSUUID_MASTER;
                            break;
                        case "PY":
                            oraCmd.Parameters.Add(new OracleParameter("SYS_KEY", OracleType.VarChar, 2000)).Value = SERVICE_SYS_ID;
                            oraCmd.Parameters.Add(new OracleParameter("POSUUID_MASTER", OracleType.VarChar, 2000)).Value = POSUUID_MASTER;
                            oraCmd.Parameters.Add(new OracleParameter("BARCODE1", OracleType.VarChar, 2000)).Value = Barcode1;
                            oraCmd.Parameters.Add(new OracleParameter("RTN_POSUUID_MASTER", OracleType.VarChar, 2000)).Direction = ParameterDirection.Output;
                            strLogMsg = ",SYS_KEY=" + SERVICE_SYS_ID + ", POSUUID_MASTER=" + POSUUID_MASTER + ", BARCODE1=" + Barcode1;
                            break;
                        case "OLR":
                            oraCmd.Parameters.Add(new OracleParameter("INPUTTXNO", OracleType.VarChar, 2000)).Value = SERVICE_SYS_ID;
                            oraCmd.Parameters.Add(new OracleParameter("POSUUID2", OracleType.VarChar, 2000)).Value = POSUUID_MASTER;
                            oraCmd.Parameters.Add(new OracleParameter("po_err_msg", OracleType.VarChar, 2000)).Direction = ParameterDirection.Output;
                            strLogMsg = ",INPUTTXNO=" + SERVICE_SYS_ID + ", POSUUID2=" + POSUUID_MASTER;
                            break;
                        case "ES":
                            oraCmd.Parameters.Add(new OracleParameter("package_id", OracleType.VarChar, 2000)).Value = SERVICE_SYS_ID;
                            oraCmd.Parameters.Add(new OracleParameter("POSuuid_details", OracleType.VarChar, 2000)).Value = POSUUID_DETAIL;
                            oraCmd.Parameters.Add(new OracleParameter("POSuuid_master", OracleType.VarChar, 2000)).Value = POSUUID_MASTER;
                            oraCmd.Parameters.Add(new OracleParameter("employee_Id", OracleType.VarChar, 2000)).Value = EMPLOYEE_ID;
                            oraCmd.Parameters.Add(new OracleParameter("Store_Id", OracleType.VarChar, 2000)).Value = STORE_ID;
                            oraCmd.Parameters.Add(new OracleParameter("STATUS", OracleType.VarChar, 2000)).Direction = ParameterDirection.Output;
                            oraCmd.Parameters.Add(new OracleParameter("ERROR_CODE", OracleType.VarChar, 2000)).Direction = ParameterDirection.Output;
                            oraCmd.Parameters.Add(new OracleParameter("ERROR_DESCRIPTION", OracleType.VarChar, 2000)).Direction = ParameterDirection.Output;
                            strLogMsg = ",package_id=" + SERVICE_SYS_ID + ", POSuuid_details=" + POSUUID_DETAIL + ", POSuuid_master=" + POSUUID_MASTER 
                                        + ", employee_Id=" + EMPLOYEE_ID + ", Store_Id=" + STORE_ID;
                            break;
                    }

                   

                    // oraCmd.Parameters.Add(new OracleParameter(SOURCE_REFERENCE_NAME, OracleType.VarChar, 2000)).Value = SERVICE_SYS_ID;
                    //  oraCmd.Parameters.Add(new OracleParameter("POSuuid_Master", OracleType.VarChar, 2000)).Value = POSUUID_MASTER;
                    if (SYSID != "")
                    {
                        oraCmd.Connection = objConn;
                        oraCmd.ExecuteNonQuery();
                       
                    }
                    switch (SYSID)
                    {
                        case "IA":
                            strMCode = ",ERR_CODE=" + oraCmd.Parameters["ERR_CODE"].Value.ToString() + ",ERR_MESG="
                                        + oraCmd.Parameters["ERR_MESG"].Value.ToString() + ",STATUS=" + oraCmd.Parameters["STATUS"].Value.ToString();
                            if (oraCmd.Parameters["ERR_CODE"].Value.ToString() != "0")
                            {
                                Logger.Log.Info("POS通知服務系統結帳失敗: SP=" + outerCmd + ",ACTI_NO=" + SERVICE_SYS_ID +
                                        ", UUID_MASTER=" + POSUUID_MASTER + ", connection string[" + outerConnStr + "]," + strMCode);
                                return -1;
                            }
                            break;
                        case "LOY":
                            strMCode = ",msg=" + oraCmd.Parameters["msg"].Value.ToString();
                            if (strMCode.Contains("Fail"))
                            {
                                Logger.Log.Info("POS通知服務系統結帳失敗: SP=" + outerCmd + ",ACTI_NO=" + SERVICE_SYS_ID +
                                        ", UUID_MASTER=" + POSUUID_MASTER + ", connection string[" + outerConnStr + "]," + strMCode);
                                return -1;
                            }
                            break;
                        case "SSI":
                            oraCmd.Parameters.Add(new OracleParameter("result", OracleType.VarChar, 2000)).Direction = ParameterDirection.Output;
                            strMCode = ",result=" + oraCmd.Parameters["result"].Value.ToString();
                            break;
                        case "ES":
                            strMCode = ",ERROR_CODE=" + oraCmd.Parameters["ERROR_CODE"].Value.ToString() + ",ERROR_DESCRIPTION="
                                        + oraCmd.Parameters["ERROR_DESCRIPTION"].Value.ToString() + ",STATUS=" + oraCmd.Parameters["STATUS"].Value.ToString();
                            break;
                      
                    }
                    if (SYSID != "")
                    {
                        Logger.Log.Info("POS通知服務系統結帳完成: SP=" + outerCmd + strLogMsg + ", connection string[" + outerConnStr + "]," + strMCode);
                        CheckFlag = "1";

                    }

                    if (bundle_id != "")
                    {
                        //SYSOK = "1";
                        BouoraCmd.Parameters.Add(new OracleParameter("BundleID", OracleType.VarChar, 2000)).Value = bundle_id;
                        BouoraCmd.Parameters.Add(new OracleParameter("ReturnCode", OracleType.VarChar, 2000)).Direction = ParameterDirection.Output;
                        BouoraCmd.Parameters.Add(new OracleParameter("ReturnMsg", OracleType.VarChar, 2000)).Direction = ParameterDirection.Output;

                        BouoraCmd.Connection = BouConn;
                        BouoraCmd.ExecuteNonQuery();
                        strMCode = "SP_POS_Feedback_Apply,ReturnMsg=" + BouoraCmd.Parameters["ReturnMsg"].Value.ToString();
                        strBundleLogMsg = ",BundleID=" + bundle_id;

                        Logger.Log.Info("POS通知服務系統結帳完成: SP=" + BouCmd + strBundleLogMsg + ", connection string[" + BOUouterConnStr + "]," + strMCode);

                        
                    }
                    return 0;
                }
                else
                {
                    if (SYSID != "")
                    {

                        Logger.Log.Info("POS通知服務系統結帳失敗: SYSID=" + OracleDBUtil.SqlStr(SYSID) + " 在系統中找不到參數[" + OracleDBUtil.SqlStr(SYSID) + "_COMMIT]");
                    }
                    if (bundle_id != "")
                    {
                        Logger.Log.Info("POS通知服務系統結帳失敗: SYSID=" + OracleDBUtil.SqlStr(BouID) + " 在系統中找不到參數[" + OracleDBUtil.SqlStr(BouID) + "_COMMIT]");
                    }
                    return -1;
                }
            }
            catch (Exception ex)
            {
                string msg = ex.Message;
                //string exmsgg = "";
                if (SYSID != "" && CheckFlag != "1")
                {
                    Logger.Log.Info("POS通知服務系統結帳失敗: SP=" + outerCmd + strLogMsg + ", connection string[" + outerConnStr + "]," + msg);
                }

                if (bundle_id != ""  )
                {
                    Logger.Log.Info("POS通知服務系統結帳失敗: SP=" + BouCmd + strBundleLogMsg + ", connection string[" + BOUouterConnStr + "]," + msg);

                
                }
                return -1;
            }
            finally
            {
                if (oraCmd != null)
                    oraCmd.Dispose();

                if (objConn != null && objConn.State == ConnectionState.Open)
                    objConn.Close();

                if (objConn != null)
                    objConn.Dispose();

                if (BouoraCmd != null)
                    BouoraCmd.Dispose();

                if (BouConn != null && BouConn.State == ConnectionState.Open)
                    BouConn.Close();

                if (BouConn != null)
                    BouConn.Dispose();
                OracleConnection.ClearAllPools();
            }
        }

        /// <summary>
        /// 更新未結表頭檔
        /// </summary>
        public void UpdateUnCloseHead(DataTable sale_head, string status, string modifyUser)
        {
            SAL01_SALE_DTO.TO_CLOSE_HEADDataTable TO_CLOSE_HEAD = new SAL01_SALE_DTO.TO_CLOSE_HEADDataTable();
            string strSQL = @"SELECT Distinct POSUUID_DETAIL FROM SALE_DETAIL  
                              WHERE POSUUID_MASTER = " + OracleDBUtil.SqlStr(sale_head.Rows[0]["POSUUID_MASTER"].ToString());
            OracleConnection objConn = null;
            DataTable dtTo_Close_Head = null;
            try
            {
                objConn = OracleDBUtil.GetConnection();
                dtTo_Close_Head = OracleDBUtil.GetDataSet(objConn, strSQL).Tables[0];
                string strMODI_DTM = "";
                if (sale_head.Rows[0]["MODI_DTM"] != null)
                {
                    strMODI_DTM = ((DateTime)sale_head.Rows[0]["MODI_DTM"]).ToString("yyyy-MM-dd HH:mm:ss");
                }
                if (dtTo_Close_Head != null && dtTo_Close_Head.Rows.Count > 0)
                {
                    foreach (DataRow to_close_headRow in dtTo_Close_Head.Rows)
                    {
                        strSQL = @"Update TO_CLOSE_HEAD Set POSUUID_MASTER = " + OracleDBUtil.SqlStr(sale_head.Rows[0]["POSUUID_MASTER"].ToString())
                                    + ", STATUS = " + OracleDBUtil.SqlStr(status) + ", MODI_USER = "
                                    + OracleDBUtil.SqlStr(modifyUser) + ", MODI_DTM = To_Date("
                                    + OracleDBUtil.SqlStr(strMODI_DTM) + ", 'yyyy-mm-dd hh24:mi:ss')  Where POSUUID_DETAIL = "
                                    + OracleDBUtil.SqlStr(to_close_headRow["POSUUID_DETAIL"].ToString());
                        OracleDBUtil.ExecuteSql(objConn, strSQL);
                    }
                }
            }
            catch 
            {
                
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
        /// 更新未結表頭檔
        /// </summary>
        public void UpdateUnCloseHead(string posuuid_detail, string possuid_master, string status, string modifyUser)
        {
            OracleConnection objConn = null;
            //DataTable dtTo_Close_Head = null;
            try
            {
                objConn = OracleDBUtil.GetConnection();
                string strSQL = @"Update TO_CLOSE_HEAD Set POSUUID_MASTER = " + OracleDBUtil.SqlStr(possuid_master)
                                    + ", STATUS = " + OracleDBUtil.SqlStr(status) + ", MODI_USER = "
                                    + OracleDBUtil.SqlStr(modifyUser) + ", MODI_DTM = sysdate Where POSUUID_DETAIL = "
                                    + OracleDBUtil.SqlStr(posuuid_detail);
                OracleDBUtil.ExecuteSql(objConn, strSQL);
            }
            catch
            {

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
        /// 更新未結表頭檔
        /// </summary>
        public void UpdateUnCloseHead(DataTable sale_head, string status, string modifyUser, OracleTransaction objTX)
        {
            SAL01_SALE_DTO.TO_CLOSE_HEADDataTable TO_CLOSE_HEAD = new SAL01_SALE_DTO.TO_CLOSE_HEADDataTable();
            string strSQL = @"SELECT Distinct POSUUID_DETAIL FROM SALE_DETAIL  
                              WHERE POSUUID_MASTER = " + OracleDBUtil.SqlStr(sale_head.Rows[0]["POSUUID_MASTER"].ToString());
            
            DataTable dtTo_Close_Head = null;
            try
            {
                dtTo_Close_Head = OracleDBUtil.GetDataSet(objTX, strSQL).Tables[0];
                string strMODI_DTM = "";
                if (sale_head.Rows[0]["MODI_DTM"] != null)
                {
                    strMODI_DTM = ((DateTime)sale_head.Rows[0]["MODI_DTM"]).ToString("yyyy-MM-dd HH:mm:ss");
                }
                if (dtTo_Close_Head != null && dtTo_Close_Head.Rows.Count > 0)
                {
                    foreach (DataRow to_close_headRow in dtTo_Close_Head.Rows)
                    {
                        strSQL = @"Update TO_CLOSE_HEAD Set POSUUID_MASTER = " + OracleDBUtil.SqlStr(sale_head.Rows[0]["POSUUID_MASTER"].ToString())
                                    + ", STATUS = " + OracleDBUtil.SqlStr(status) + ", MODI_USER = "
                                    + OracleDBUtil.SqlStr(modifyUser) + ", MODI_DTM = To_Date("
                                    + OracleDBUtil.SqlStr(strMODI_DTM) + ", 'yyyy-mm-dd hh24:mi:ss')  Where POSUUID_DETAIL = "
                                    + OracleDBUtil.SqlStr(to_close_headRow["POSUUID_DETAIL"].ToString());
                        OracleDBUtil.ExecuteSql(objTX, strSQL);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Cancel 外部系統交易
        /// </summary>
        /// <param name="posuuid_detail">未結交易表頭檔主鍵</param>
        /// <param name="service_type">交易類型</param>
        /// <param name="service_sys_id">外部系統主鍵</param>
        /// <param name="bundle_id">bundle_id</param>
        /// <param name="store_no">交易店點</param>
        /// <param name="sale_person">交易人員</param>
        /// <param name="barcode1">barcode1</param>
        /// <param name="barcode2">barcode2</param>
        /// <param name="barcode3">barcode3</param>
        /// <param name="amount">單筆交易金額</param>
        /// <returns>結果:0 成功, -1 失敗</returns>
        public int CancelOuterSystem(string posuuid_detail, string service_type, string service_sys_id, string bundle_id, 
                                        string store_no, string sale_person, string barcode1, string barcode2, string barcode3, string amount)
        {
            string outerConnStr = "";
            string BOUouterConnStr = "";
            string outerCmd = "";
            string strMCode = "";
            string SYSID = "";
            string BouCmd = "";
            string BouID = "";
            string CheckFlag = "";
            //string SYSOK = "";
            string strLogMsg = "";
            string strBundleLogMsg = "";
            OracleConnection objConn = null;
            OracleCommand oraCmd = null;
            OracleConnection BouConn = null;
            OracleCommand BouoraCmd = null;
            
            string deletedBoundleId = "";

            try
            {
                bool cancelOk = true;
                if (posuuid_detail != null && (!string.IsNullOrEmpty(posuuid_detail)))
                {
                    if (service_type != null && (!string.IsNullOrEmpty(service_type)))
                    {
                        switch (service_type)
                        {
                            case "1":   //IA
                                outerConnStr = OracleDBUtil.GetIAConnectionStringByTNSName();
                                SYSID = "IA";
                                break;
                            case "2":   //LOY
                                outerConnStr = OracleDBUtil.GetLOYConnectionStringByTNSName();
                                SYSID = "LOY";
                                break;
                            case "4":   //SSI
                                outerConnStr = OracleDBUtil.GetSSIConnectionStringByTNSName();
                                SYSID = "SSI";
                                break;
                            case "3":   //PAYMENT
                                outerConnStr = OracleDBUtil.GetPAYMENTConnectionStringByTNSName();
                                SYSID = "PY";
                                break;
                            case "10":   //E-Store
                                outerConnStr = OracleDBUtil.GetEStoreConnectionStringByTNSName();
                                SYSID = "ES";
                                break;
                            default:
                                break;
                        }

                        objConn = Advtek.Utility.OracleDBUtil.GetConnection();

                        if (bundle_id != "" && deletedBoundleId.IndexOf(bundle_id + "^" + barcode1 + "^" + barcode2 + "^" + barcode3) < 0)
                        {

                            BOUouterConnStr = OracleDBUtil.GetBOUConnectionStringByTNSName();
                            BouConn = OracleDBUtil.GetConnectionByConnString(BOUouterConnStr);

                            //BouCmd = "SP_POS_Feedback_Cancel";
                            BouID = "BOU";

                            string strSQL = "Select para_value from sys_para where para_key = " + OracleDBUtil.SqlStr(BouID + "_CANCEL");
                            DataTable dt1 = OracleDBUtil.GetDataSet(objConn, strSQL).Tables[0];
                            if (dt1 != null && dt1.Rows.Count > 0)
                                if (dt1.Rows[0][0] != null)
                                    BouCmd = dt1.Rows[0][0].ToString();
                            BouoraCmd = new OracleCommand(BouCmd);
                            BouoraCmd.CommandType = System.Data.CommandType.StoredProcedure;
                        }

                        if (SYSID != "")
                        {
                            string strSQL = "Select para_value from sys_para where para_key = " + OracleDBUtil.SqlStr(SYSID + "_CANCEL");
                            DataTable dtSys = OracleDBUtil.GetDataSet(objConn, strSQL).Tables[0];
                            if (dtSys != null && dtSys.Rows.Count > 0)
                                if (dtSys.Rows[0][0] != null)
                                    outerCmd = dtSys.Rows[0][0].ToString();
                            objConn = OracleDBUtil.GetConnectionByConnString(outerConnStr);
                        }

                  
                   
                        //}

                        try
                        {
                            oraCmd = new OracleCommand(outerCmd);
                            oraCmd.CommandType = System.Data.CommandType.StoredProcedure;

                            switch (SYSID)
                            {
                                case "IA":
                                    oraCmd.Parameters.Add(new OracleParameter("ACTI_NO", OracleType.VarChar, 2000)).Value = service_sys_id;
                                    oraCmd.Parameters.Add(new OracleParameter("UUID_DETAILS", OracleType.VarChar, 2000)).Value = posuuid_detail;
                                    oraCmd.Parameters.Add(new OracleParameter("STATUS", OracleType.VarChar, 4000)).Direction = ParameterDirection.Output;
                                    oraCmd.Parameters.Add(new OracleParameter("ERR_CODE", OracleType.Number)).Direction = ParameterDirection.Output;
                                    oraCmd.Parameters.Add(new OracleParameter("ERR_MESG", OracleType.VarChar, 4000)).Direction = ParameterDirection.Output;
                                    strLogMsg = ",ACTI_NO=" + service_sys_id + ", UUID_DETAILS=" + posuuid_detail;
                                    break;
                                case "LOY":
                                    oraCmd.Parameters.Add(new OracleParameter("POSuuid_Details", OracleType.VarChar, 2000)).Value = posuuid_detail;
                                    oraCmd.Parameters.Add(new OracleParameter("image_number", OracleType.VarChar, 2000)).Value = service_sys_id;
                                    oraCmd.Parameters.Add(new OracleParameter("msg", OracleType.VarChar, 2000)).Direction = ParameterDirection.Output;
                                    strLogMsg = ", POSuuid_Details=" + posuuid_detail;
                                    break;
                                case "SSI":
                                    oraCmd.Parameters.Add(new OracleParameter("image_number", OracleType.VarChar, 2000)).Value = service_sys_id;
                                    oraCmd.Parameters.Add(new OracleParameter("POSuuid_Details", OracleType.VarChar, 2000)).Value = posuuid_detail;
                                    oraCmd.Parameters.Add(new OracleParameter("result", OracleType.VarChar, 2000)).Direction = ParameterDirection.Output;
                                    strLogMsg = ",image_number=" + service_sys_id + ", UUID_DETAILS=" + posuuid_detail;
                                    break;
                                case "PY":
                                    oraCmd.Parameters.Add(new OracleParameter("SYS_KEY", OracleType.VarChar, 2000)).Value = service_sys_id;
                                    oraCmd.Parameters.Add(new OracleParameter("POSUUID_DETAILS", OracleType.VarChar, 2000)).Value = posuuid_detail;
                                    oraCmd.Parameters.Add(new OracleParameter("BARCODE1", OracleType.VarChar, 2000)).Value = barcode1;
                                    oraCmd.Parameters.Add(new OracleParameter("BARCODE2", OracleType.VarChar, 2000)).Value = barcode2;
                                    oraCmd.Parameters.Add(new OracleParameter("BARCODE3", OracleType.VarChar, 2000)).Value = barcode3;
                                    oraCmd.Parameters.Add(new OracleParameter("PAY_AMOUNT", OracleType.VarChar, 2000)).Value = amount;
                                    oraCmd.Parameters.Add(new OracleParameter("STOREID", OracleType.VarChar, 2000)).Value = store_no;
                                    oraCmd.Parameters.Add(new OracleParameter("EMPLOYEE_ID", OracleType.VarChar, 2000)).Value = sale_person;
                                    oraCmd.Parameters.Add(new OracleParameter("RTN_SYS_KEY", OracleType.VarChar, 2000)).Direction = ParameterDirection.Output;
                                    oraCmd.Parameters.Add(new OracleParameter("RTN_POSUUID_DETAILS", OracleType.VarChar, 2000)).Direction = ParameterDirection.Output;
                                    strLogMsg = ",SYS_KEY=" + service_sys_id + ", POSUUID_DETAILS=" + posuuid_detail + ", BARCODE1=" + barcode1 
                                                + ", BARCODE2=" + barcode2 + ", BARCODE3=" + barcode3 + ", PAY_AMOUNT=" + amount + ", STOREID=" + store_no
                                                + ", EMPLOYEE_ID=" + sale_person;
                                    break;
                                case "ES":
                                    oraCmd.Parameters.Add(new OracleParameter("POSuuid_details", OracleType.VarChar, 2000)).Value = posuuid_detail;
                                    oraCmd.Parameters.Add(new OracleParameter("package_id", OracleType.VarChar, 2000)).Value = service_sys_id;
                                    oraCmd.Parameters.Add(new OracleParameter("employee_Id", OracleType.VarChar, 2000)).Value = sale_person;
                                    oraCmd.Parameters.Add(new OracleParameter("Store_Id", OracleType.VarChar, 2000)).Value = store_no;
                                    oraCmd.Parameters.Add(new OracleParameter("STATUS", OracleType.VarChar, 2000)).Direction = ParameterDirection.Output;
                                    oraCmd.Parameters.Add(new OracleParameter("ERROR_CODE", OracleType.VarChar, 2000)).Direction = ParameterDirection.Output;
                                    oraCmd.Parameters.Add(new OracleParameter("ERROR_DESCRIPTION", OracleType.VarChar, 2000)).Direction = ParameterDirection.Output;
                                    strLogMsg = ", POSuuid_details=" + posuuid_detail + ", package_id=" + service_sys_id
                                                + ", employee_Id=" + sale_person + ", Store_Id=" + store_no;
                                    break;
                            }

                          


                            // oraCmd.Parameters.Add(new OracleParameter(SOURCE_REFERENCE_Name, OracleType.VarChar, 2000)).Value = SERVICE_SYS_ID;
                            //  oraCmd.Parameters.Add(new OracleParameter("POSuuid_Master", OracleType.VarChar, 2000)).Value = POSUUID_MASTER;

                            if (oraCmd != null && oraCmd.ToString() != "")
                            {
                                oraCmd.Connection = objConn;
                                oraCmd.ExecuteNonQuery();
                            }
                            switch (SYSID)
                            {
                                case "IA":
                                    strMCode = "SP_IA_CANCEL_POS,ERR_CODE=" + oraCmd.Parameters["ERR_CODE"].Value.ToString() + ",ERR_MESG="
                                                + oraCmd.Parameters["ERR_MESG"].Value.ToString();
                                    if (oraCmd.Parameters["ERR_CODE"].Value.ToString() == "9999")
                                        cancelOk = false;
                                    break;
                                case "LOY":
                                    strMCode = "SP_LOY_CANCEL_POS,msg=" + oraCmd.Parameters["msg"].Value.ToString();
                                    if (oraCmd.Parameters["msg"].Value.ToString() == "9999")
                                        cancelOk = false;
                                    break;
                                case "SSI":
                                    strMCode = "SP_SSI_CANCEL_POS,result=" + oraCmd.Parameters["result"].Value.ToString();
                                    if (oraCmd.Parameters["result"].Value.ToString() == "9999")
                                        cancelOk = false;
                                    break;
                                case "PY":
                                    strMCode = "SP_POS2PAYMENT_CANCEL_DATA,RTN_SYS_KEY=" + oraCmd.Parameters["RTN_SYS_KEY"].Value.ToString() + ",RTN_POSUUID_DETAILS=" + oraCmd.Parameters["RTN_POSUUID_DETAILS"].Value.ToString();
                                    if (oraCmd.Parameters["RTN_SYS_KEY"].Value.ToString() == "")
                                        cancelOk = false;
                                    break;
                                case "ES":
                                    strMCode = "SP_POS4eStore_CancelOrder,ERROR_CODE=" + oraCmd.Parameters["ERROR_CODE"].Value.ToString() + ",ERROR_DESCRIPTION="
                                                + oraCmd.Parameters["ERROR_DESCRIPTION"].Value.ToString() + ",STATUS=" + oraCmd.Parameters["STATUS"].Value.ToString();
                                    if (oraCmd.Parameters["ERROR_CODE"].Value.ToString() == "9999")
                                        cancelOk = false;
                                    break;
                     
                            }

                            if (SYSID != "")
                            {
                                Logger.Log.Info("POS通知服務系統取消交易完成: SP=" + outerCmd + strLogMsg 
                                                + ", connection string[" + outerConnStr + "]," + OracleDBUtil.SqlStr(strMCode));
                                CheckFlag = "1";
                            }


                            if (bundle_id != "" && deletedBoundleId.IndexOf(bundle_id + "^" + barcode1 + "^" + barcode2 + "^" + barcode3) < 0)
                            {
                               
                                BouoraCmd.Parameters.Add(new OracleParameter("BundleID", OracleType.VarChar, 2000)).Value = bundle_id;
                                BouoraCmd.Parameters.Add(new OracleParameter("ReturnCode", OracleType.VarChar, 2000)).Direction = ParameterDirection.Output;
                                BouoraCmd.Parameters.Add(new OracleParameter("ReturnMsg", OracleType.VarChar, 2000)).Direction = ParameterDirection.Output;
                                deletedBoundleId += "," + bundle_id + "^" + barcode1 + "^" + barcode2 + "^" + barcode3;

                                BouoraCmd.Connection = BouConn;
                                BouoraCmd.ExecuteNonQuery();

                                strMCode = "SP_POS_Feedback_Cancel,ReturnMsg=" + BouoraCmd.Parameters["ReturnMsg"].Value.ToString();
                                strBundleLogMsg = ",BundleID=" + bundle_id;
                                if (BouoraCmd.Parameters["ReturnCode"].Value.ToString() == "9999")
                                    cancelOk = false;

                                Logger.Log.Info("POS通知服務系統取消交易完成: SP=" + BouCmd + strBundleLogMsg 
                                         + ", connection string[" + BOUouterConnStr + "]," + OracleDBUtil.SqlStr(strMCode));

                            }
                        }
                        catch (Exception ex)
                        {
                            string msg = ex.Message;
                            cancelOk = false;
                            if (SYSID != "" && CheckFlag != "1")
                            {
                                Logger.Log.Info("POS通知服務系統取消交易失敗: SP=" + outerCmd + strLogMsg 
                                                + ", connection string[" + outerConnStr + "]," + OracleDBUtil.SqlStr(msg));
                            }
                            if (bundle_id != "")
                            {
                                Logger.Log.Info("POS通知服務系統取消交易失敗: SP=" + BouCmd + strBundleLogMsg 
                                             + ", connection string[" + BOUouterConnStr + "]," + OracleDBUtil.SqlStr(msg));
                            }
                        }
                        finally
                        {
                          //  oraCmd.Dispose();
                        }
                    }
                }
                if (cancelOk)
                    return 0;
                else
                    return -1;
            }
            catch (Exception ex)
            {
                if (SYSID != "")
                {
                    Logger.Log.Info("POS通知服務系統取消交易失敗: SP=" + outerCmd + strLogMsg 
                                                + OracleDBUtil.SqlStr(ex.Message.Replace("'", "-").Replace("\"", " ")));
                }
                if (bundle_id != "")
                {
                    Logger.Log.Info("POS通知服務系統取消交易失敗: SP=" + BouCmd + strBundleLogMsg 
                                              + OracleDBUtil.SqlStr(ex.Message.Replace("'", "-").Replace("\"", " ")));
                }
                return -1;
            }
            finally
            {
                if (oraCmd != null)
                    oraCmd.Dispose();

                if (objConn != null && objConn.State == ConnectionState.Open)
                    objConn.Close();

                if (objConn != null)
                    objConn.Dispose();

                if (BouoraCmd != null)
                    BouoraCmd.Dispose();

                if (BouConn != null && BouConn.State == ConnectionState.Open)
                    BouConn.Close();

                if (BouConn != null)
                    BouConn.Dispose();
                OracleConnection.ClearAllPools();
            }
        }

        /// <summary>
        /// 新增取消交易失敗外部交易到log檔中
        /// </summary>
        public void InsertDataUploadLog(string possuuid_detail)
        {
            string uid = Advtek.Utility.GuidNo.getUUID().ToString();
            string strSQL = @"Insert into data_upload_log(id, posuuid_detail, data_type, scan_count, status, cancel_date) 
                                Values(" + OracleDBUtil.SqlStr(uid) + ", " + OracleDBUtil.SqlStr(possuuid_detail) + ", '1', 0, '1', sysdate)";
            OracleConnection objConn = null;
            //DataTable dtTo_Close_Head = null;
            try
            {
                objConn = OracleDBUtil.GetConnection();
                int ret = 0; int i = 0;
                while (ret == 0 && i < 3)
                {
                    ret = OracleDBUtil.ExecuteSql(objConn, strSQL);
                    i++;
                }
            }
            catch
            {

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
        /// 取得發票種類
        /// </summary>
        public DataTable getINVOICE_TYPE()
        {
            string sqlStr = @"SELECT INVOICE_TYPE_ID, INVOICE_TYPE_NAME From INVOICE_TYPE Order by INVOICE_TYPE_ID ";

            DataTable dt = OracleDBUtil.Query_Data(sqlStr);
            return dt;
        }

        /// <summary>
        /// 檢查門市發票設定
        /// </summary>
        public DataTable chkINVSetting(string STORE_NO)
        {
            string sqlStr = @"SELECT invoice_no FROM invoice_no_pool
                               WHERE Trunc(SYSDATE) BETWEEN to_date(s_use_ym||'/01','YYYY/MM/DD') AND to_date(e_use_ym||'/01','YYYY/MM/DD') + interval '1' month - 1
                                 AND inv_date IS NULL    
                                 AND store_no = " + OracleDBUtil.SqlStr(STORE_NO) + @" 
                                 AND ROWNUM = 1 
                            ORDER BY invoice_no";

            DataTable dt = OracleDBUtil.Query_Data(sqlStr);
            return dt;
        }

        /// <summary>
        /// 扣除折扣次數
        /// </summary>
        public int addStoreDiscountCount(string STORE_NO, string Discount_Code, OracleTransaction objTX)
        {
            try
            {
                string sqlStr = @"Update DISCOUNT_MASTER Set DIS_USE_MONEY_CONSUMED = NVL(DIS_USE_MONEY_CONSUMED, 0) + 1 Where Discount_Code = "
                                    + OracleDBUtil.SqlStr(Discount_Code);
                int ret = Advtek.Utility.OracleDBUtil.ExecuteSql(objTX, sqlStr);
                if (ret >= 0)
                {
                    sqlStr = @"SELECT DIS_CONSUME_COUNT FROM STORE_DISCOUNT 
                                    WHERE store_no = " + OracleDBUtil.SqlStr(STORE_NO)
                            + @" AND Discount_Master_ID = (Select Discount_Master_ID From Discount_Master Where Discount_Code = "
                            + OracleDBUtil.SqlStr(Discount_Code) + ")";
                    DataTable dt = Advtek.Utility.OracleDBUtil.GetDataSet(objTX, sqlStr).Tables[0];
                    if (dt != null && dt.Rows.Count > 0)
                    {   //有設定門市分量
                        sqlStr = @"SELECT DIS_CONSUME_COUNT FROM STORE_DISCOUNT 
                                    WHERE NVL(Dis_Use_Count, 0) > NVL(Dis_Consume_Count, 0) And store_no = " + OracleDBUtil.SqlStr(STORE_NO)
                                + @" AND Discount_Master_ID = (Select Discount_Master_ID From Discount_Master Where Discount_Code = "
                                + OracleDBUtil.SqlStr(Discount_Code) + ")";
                        dt = Advtek.Utility.OracleDBUtil.GetDataSet(objTX, sqlStr).Tables[0];
                        if (dt != null && dt.Rows.Count > 0)
                        {
                            sqlStr = @"Update STORE_DISCOUNT Set Dis_Consume_Count = NVL(Dis_Consume_Count, 0) + 1 Where store_no = "
                                   + OracleDBUtil.SqlStr(STORE_NO)
                                   + @" AND Discount_Master_ID = (Select Discount_Master_ID From Discount_Master Where Discount_Code = "
                                   + OracleDBUtil.SqlStr(Discount_Code) + ")";
                            ret = Advtek.Utility.OracleDBUtil.ExecuteSql(objTX, sqlStr);
                        }
                    }
                }
                return ret;
            }
            catch (Exception ex)
            {
                string errMsg = ex.Message;
                return -1;
            }
        }

        /// <summary>
        /// 檢查HG折扣次數
        /// </summary>
        public bool chkHGDiscountCount(string Activity_No, OracleTransaction objTX)
        {
            string sqlStr = @"SELECT USE_COUNT FROM HG_CONVERTIBLE_RESTRICTED  
                                WHERE to_date(to_char(sysdate, 'yyyy/mm/dd'),'yyyy/mm/dd') Between NVL(S_DATE, to_date('2000/01/01','YYYY/MM/DD')) 
                                        And NVL(E_DATE,to_date('3000/01/01','YYYY/MM/DD')) AND ACTIVITY_NO = " + OracleDBUtil.SqlStr(Activity_No);
            bool result = false;
            try
            {
                DataTable dt = OracleDBUtil.GetDataSet(objTX, sqlStr).Tables[0];
                if (dt != null && dt.Rows.Count > 0)
                {
                    int totalCnt = 0;
                    if (dt.Rows[0][0] != null && NumberUtil.IsNumeric(dt.Rows[0][0].ToString()))
                        totalCnt = int.Parse(dt.Rows[0][0].ToString());
                    if (totalCnt > 0)
                    {
                        sqlStr = @"SELECT USE_COUNT FROM HG_CONVERTIBLE_RESTRICTED  
                                WHERE NVL(USE_COUNT, 0) > NVL(CONSUME_COUNT, 0) And to_date(to_char(sysdate, 'yyyy/mm/dd'),'yyyy/mm/dd') 
                                    Between NVL(S_DATE, to_date('2000/01/01','YYYY/MM/DD')) And NVL(E_DATE,to_date('3000/01/01','YYYY/MM/DD')) 
                                      AND ACTIVITY_NO = " + OracleDBUtil.SqlStr(Activity_No);
                        dt = OracleDBUtil.GetDataSet(objTX, sqlStr).Tables[0];
                        if (dt != null && dt.Rows.Count > 0)
                        {
                            sqlStr = @"Update HG_CONVERTIBLE_RESTRICTED Set CONSUME_COUNT = NVL(CONSUME_COUNT, 0) + 1 Where ACTIVITY_NO = " + OracleDBUtil.SqlStr(Activity_No);
                            int ret = Advtek.Utility.OracleDBUtil.ExecuteSql(objTX, sqlStr);
                            if (ret >= 0)
                                result = true;
                        }
                    }
                    else
                    {   //無兌換次數限制
                        result = true;
                    }
                }
                else
                {   //一般兌點規則,不會存在於HG_CONVERTIBLE_RESTRICTED資料表中
                    result = true;
                }
                return result;
            }
            catch (Exception ex)
            {
                string errMsg = ex.Message;
                return false;
            }
        }

        /// <summary>
        /// 檢查門市特殊折扣金額
        /// </summary>
        public bool chkStoreSpecDisAmt(string store_no, string role_type, OracleTransaction objTX)
        {
            string sqlStr = @"SELECT DIS_AMT FROM STORE_SPECIAL_DIS_M   
                                WHERE NVL(DIS_AMT, 0) > NVL(USED_AMT, 0) And DEL_FLAG = 'N' And Store_No = " + OracleDBUtil.SqlStr(store_no)
                           + @" And YYMM = " + OracleDBUtil.SqlStr(DateTime.Today.ToString("yyyy/MM"));
            bool result = false;
            try
            {
                DataTable dt = OracleDBUtil.GetDataSet(objTX, sqlStr).Tables[0];
                if (dt != null && dt.Rows.Count > 0)
                {
                    sqlStr = @" SELECT DIS_AMT_UBOND FROM STORE_SPECIAL_DIS_D WHERE NVL(DIS_AMT_UBOND, 0) > NVL(USED_AMT, 0) 
                                And NVL(DIS_AMT_UBOND, 0) > NVL(DIS_AMT_CONSUMED, 0) And DEL_FLAG = 'N' And Store_No = " + OracleDBUtil.SqlStr(store_no)
                           + @" And Role_ID = " + OracleDBUtil.SqlStr(role_type) + @" And SSD_ID IN 
                                (SELECT SSD_ID FROM STORE_SPECIAL_DIS_M WHERE DEL_FLAG='N' AND STORE_NO = " + OracleDBUtil.SqlStr(store_no) 
                           + @" AND YYMM = " + OracleDBUtil.SqlStr(DateTime.Today.ToString("yyyy/MM")) + ")";
                    dt = OracleDBUtil.GetDataSet(objTX, sqlStr).Tables[0];
                    if (dt != null && dt.Rows.Count > 0)
                        result = true;
                }
                return result;
            }
            catch (Exception ex)
            {
                string errMsg = ex.Message;
                return false;
            }
        }

        /// <summary>
        /// 取得發票電子檔名稱
        /// </summary>
        public string getINVFileName(string posuuid_master)
        {
            string FileName = "";
            string sqlStr = @"SELECT to_char(INVOICE_DATE, 'YYYY') || '_' || INVOICE_NO || '.pdf' 
                                From INVOICE_HEAD 
                               Where posuuid_master = " + OracleDBUtil.SqlStr(posuuid_master) + @"
                            Order by INVOICE_NO";

            DataTable dtInv = OracleDBUtil.Query_Data(sqlStr);

            if (dtInv != null && dtInv.Rows.Count > 0)
            {
                if (dtInv.Rows[0][0] != null)
                    FileName = dtInv.Rows[0][0].ToString();
                else
                    FileName = "";
            }

            return FileName;
        }

        /// <summary>
        /// 信用卡分期支付消費門檻
        /// </summary>
        /// <returns>int</returns>
        public int getCreditDivLimitAmount()
        {
            string sqlStr = " SELECT PARA_VALUE FROM SYS_PARA WHERE PARA_KEY='CREDIT_CARD_PAY_LIMIT' ";

            DataTable dt = OracleDBUtil.Query_Data(sqlStr);

            int limitAmount = 0;
            if (dt != null && dt.Rows.Count > 0)
                if (dt.Rows[0][0] != null && NumberUtil.IsNumeric(dt.Rows[0][0].ToString()) && dt.Rows[0][0].ToString() != "")
                    limitAmount = int.Parse(dt.Rows[0][0].ToString());
            return limitAmount;
        }

        /// <summary>
        /// 取得 信用卡卡別代碼
        /// </summary>
        /// <returns>DataTable</returns>
        public DataTable getCreditCardType(string creditCardName)
        {
            string sqlStr = @" SELECT CREDIT_CARD_TYPE_ID FROM credit_card_type Where CREDIT_CARD_TYPE_NAME = " + OracleDBUtil.SqlStr(creditCardName);

            DataTable dt = OracleDBUtil.Query_Data(sqlStr);
            return dt;
        }

        /// <summary>
        /// 取得 信用卡分期手續費費率
        /// </summary>
        /// <returns>DataTable</returns>
        public DataTable getCreditDivRate(string CREDIT_CARD_TYPE_ID)
        {
            string sqlStr = @" SELECT charge_rate 
                                 FROM credit_card_proce_rate 
                                Where to_date(to_char(sysdate, 'yyyy/mm/dd'),'yyyy/mm/dd') Between NVL(S_DATE, to_date('2000/01/01','YYYY/MM/DD')) And NVL(E_DATE,to_date('3000/01/01','YYYY/MM/DD'))
                                  and credit_card_type_id = " + OracleDBUtil.SqlStr(CREDIT_CARD_TYPE_ID);

            DataTable dt = OracleDBUtil.Query_Data(sqlStr);
            return dt;
        }

        /// <summary>
        /// 取得 信用卡分期代號
        /// </summary>
        /// <returns>DataTable</returns>
        public DataTable getCreditCardInstallmentId(string bank_id, int pay_seqment)
        {
            string sqlStr = @"  SELECT INSTELLMENT_ID 
                                FROM CREDIT_CART_INSTELLMENT 
                                Where to_date(to_char(sysdate, 'yyyy/mm/dd'),'yyyy/mm/dd') Between NVL(S_DATE, to_date('2000/01/01','YYYY/MM/DD')) And NVL(E_DATE,to_date('3000/01/01','YYYY/MM/DD')) 
                                And bank_id = " +  OracleDBUtil.SqlStr(bank_id) + @" 
                                And PAY_SEQMENT = " + pay_seqment.ToString();

            DataTable dt = OracleDBUtil.Query_Data(sqlStr);
            return dt;
        }

        /// <summary>
        /// 取得 信用卡分期利率
        /// </summary>
        /// <returns>DataTable</returns>
        public DataTable getCreditCardInterestRate(string installment_id)
        {
            string sqlStr = "select seqment_rate from CREDIT_CART_INSTELLMENT Where INSTELLMENT_ID = " + OracleDBUtil.SqlStr(installment_id);

            DataTable dt = OracleDBUtil.Query_Data(sqlStr);
            return dt;
        }

        /// <summary>
        /// 取得 門市分攤信用卡分期手續費率
        /// </summary>
        /// <returns>DataTable</returns>
        public DataTable getCreditCardSettlementRate(string installment_id)
        {
            string sqlStr = @"  select settlement_rate 
                                  from CREDIT_CARD_SETTLEMMENT 
                                 Where INSTELLMENT_ID = " + OracleDBUtil.SqlStr(installment_id) + @" 
                                   And rownum =1 
                              order by LINE_NO ASC ";

            DataTable dt = OracleDBUtil.Query_Data(sqlStr);
            return dt;
        }

        /// <summary>
        /// 判斷是否為已結帳交易
        /// </summary>
        /// <param name="POSUUID_DETAIL">未結清單主檔鍵值</param>
        /// <returns>bool</returns>
        public bool IsFinishJob(string POSUUID_DETAIL)
        {
            bool isFinished = false;
            string sqlStr = @"  SELECT POSUUID_MASTER 
                                  FROM SALE_HEAD 
                                 WHERE SALE_STATUS = '2' 
                                   And POSUUID_MASTER in ( Select POSUUID_MASTER From SALE_DETAIL Where POSUUID_DETAIL = " + OracleDBUtil.SqlStr(POSUUID_DETAIL) + ")";

            DataTable dt = OracleDBUtil.Query_Data(sqlStr);

            if (dt != null && dt.Rows.Count > 0)
                isFinished = true;
            return isFinished;
        }

        /// <summary>
        /// 檢查交易補登手開發票是否可用
        /// </summary>
        /// <param name="Store_No">門市代碼</param>
        /// <param name="INV_NO">發票號碼</param>
        /// <returns>bool</returns>
        public bool IsValidINVNo(string Store_No, string INV_NO, string USE_TYPE)
        {
            bool isValid = false;
            string sqlStr = @"SELECT   invoice_no
                              FROM invoice_no_pool
                              WHERE status = '0'
                              AND s_use_ym <= TO_CHAR (SYSDATE, 'YYYY/MM')
                              AND e_use_ym >= TO_CHAR (SYSDATE, 'YYYY/MM')
                              AND store_no = " + OracleDBUtil.SqlStr(Store_No)
                          + " AND invoice_no = " + OracleDBUtil.SqlStr(INV_NO)
                          + " AND USE_TYPE = " + OracleDBUtil.SqlStr(USE_TYPE);

            DataTable dt = OracleDBUtil.Query_Data(sqlStr);

            if (dt != null && dt.Rows.Count > 0)
                isValid = true;
            return isValid;
        }

        /// <summary>
        /// 取得參數值
        /// </summary>
        /// <returns>DataTable</returns>
        public DataTable getParaValue(string para_key)
        {
            string sqlStr = "select para_value from sys_para where para_key = " + OracleDBUtil.SqlStr(para_key);

            DataTable dt = OracleDBUtil.Query_Data(sqlStr);
            return dt;
        }

        /// <summary>
        /// 更新ETC 加值資訊
        /// </summary>
        /// <returns>int</returns>
        public int updateETCAddInfo(DataRow drDetail)
        {
            string sqlStr = @"Update SALE_DETAIL Set ETC_BEFORE_CHARGE = " + drDetail["ETC_BEFORE_CHARGE"].ToString() + ", ETC_AFTER_CHARGE = " +
                                drDetail["ETC_AFTER_CHARGE"].ToString() + ", ETC_SERNO = " + OracleDBUtil.SqlStr(drDetail["ETC_SERNO"].ToString()) +
                            @" Where POSUUID_MASTER = " + OracleDBUtil.SqlStr(drDetail["POSUUID_MASTER"].ToString()) + " And PRODNO = "
                                + OracleDBUtil.SqlStr(drDetail["PRODNO"].ToString());

            OracleConnection objConn = null;
            try
            {
                objConn = Advtek.Utility.OracleDBUtil.GetConnection();
                return OracleDBUtil.ExecuteSql(objConn, sqlStr);
            }
            catch (Exception ex)
            {
                string msg = ex.Message;
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
        /// 代收處理
        /// </summary>
        /// <param name="POSUUID_MASTER"></param>
        public void collection(string POSUUID_MASTER)
        {
            OracleConnection objConn = null;
            try
            {
                objConn = Advtek.Utility.OracleDBUtil.GetConnection();
                OracleCommand cmd = new OracleCommand("PK_BILL.PayModeSplitOne", objConn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(":I_MASTER_ID", OracleType.VarChar, 32).Value = POSUUID_MASTER;
                cmd.ExecuteNonQuery();
            }
            catch
            {

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
        /// 取得目前有效的支付方式
        /// </summary>
        /// <returns>DataTable</returns>
        public DataTable getValidPaymentMode()
        {
            string sqlStr = @"  select pay_mode_id 
                                from   payment_method_set 
                                where  to_date(to_char(sysdate, 'yyyy/mm/dd'),'yyyy/mm/dd') Between NVL(S_DATE, to_date('2000/01/01','YYYY/MM/DD')) 
                                And    NVL(E_DATE,to_date('3000/01/01','YYYY/MM/DD')) 
                                order by pay_mode_id";

            DataTable dt = OracleDBUtil.Query_Data(sqlStr);
            return dt;
        }
    }
}

