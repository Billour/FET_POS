using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.OracleClient;
using System.Web;
using Advtek.Utility;
using FET.POS.Model.DTO;

namespace FET.POS.Model.Facade.FacadeImpl
{
    public class OSAL04_Facade : BaseClass
    {

        /// <summary>
        /// 檢查是否有資料
        /// </summary>
        /// <param name="HOST_ID"></param>
        /// <param name="SALE_NO"></param>
        /// <param name="TRADE_DATE"></param>
        /// <param name="STORE_NO"></param>
        /// <returns></returns>
        public string CheckData(string HOST_ID, string SALE_NO, string TRADE_DATE, string STORE_NO)
        {
            string POSUUID_MASTER = "";
            OracleConnection conn = null;
            StringBuilder sb = new StringBuilder();
            sb.Append(" SELECT POSUUID_MASTER ");
            sb.Append(" FROM THD ");
            sb.AppendFormat(" Where STATUS is null and  TERM_NO = {0} ", OracleDBUtil.SqlStr(HOST_ID));
            sb.AppendFormat(" AND TDATE ={0} ", OracleDBUtil.SqlStr(TRADE_DATE));
            sb.AppendFormat(" AND STORENO ={0} ", OracleDBUtil.SqlStr("R" + STORE_NO));
            sb.AppendFormat(" AND SERIAL_NO ={0} ", OracleDBUtil.SqlStr(SALE_NO));

            try
            {
                conn = OracleDBUtil.GetConnection();
                OracleCommand cmd = new OracleCommand(sb.ToString(), conn);
                bool hasRow = false;
                OracleDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    POSUUID_MASTER = dr.IsDBNull(0) ? "" : dr.GetString(0);
                    hasRow = true;
                }
                dr.Close();

                if (hasRow)
                {
                    if (string.IsNullOrEmpty(POSUUID_MASTER))
                    {
                        string sale_type = "1";
                        POSUUID_MASTER = GuidNo.getUUID();
                        string sqlstr = "update THD set POSUUID_MASTER = '{0}',SALE_TYPE = '{5}' where TERM_NO = '{1}' and TDATE='{2}' and STORENO = 'R{3}' and SERIAL_NO = '{4}' ";
                        sqlstr = string.Format(sqlstr, POSUUID_MASTER, HOST_ID, TRADE_DATE, STORE_NO, SALE_NO, sale_type);
                        cmd = new OracleCommand(sqlstr, conn);
                        cmd.ExecuteNonQuery();

                        sqlstr = "select barcode1 from TDL where  BARCODE1 is not null and TERM_NO = '{0}' and TDATE='{1}' and STORENO = 'R{2}' and SERIAL_NO = '{3}'";
                        sqlstr = string.Format(sqlstr,HOST_ID, TRADE_DATE, STORE_NO, SALE_NO);
                        cmd = new OracleCommand(sqlstr, conn);
                        dr = cmd.ExecuteReader();
                        if (dr.Read())
                        {
                            sale_type = "2";
                        }
                        dr.Close();

                        sqlstr = "update TDL set POSUUID_MASTER = '{0}'  where TERM_NO = '{1}' and TDATE='{2}' and STORENO = 'R{3}' and SERIAL_NO = '{4}' ";
                        sqlstr = string.Format(sqlstr, POSUUID_MASTER, HOST_ID, TRADE_DATE, STORE_NO, SALE_NO);
                        cmd = new OracleCommand(sqlstr, conn);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (conn.State == ConnectionState.Open) conn.Close();
                OracleConnection.ClearPool(conn);
            }

            return POSUUID_MASTER;
        }

        /// <summary>
        /// 取得舊POS交易表頭資料
        /// </summary>
        /// <param name="POSUUID_MASTER"></param>
        /// <returns></returns>
        public DataTable getSale_Head(string UUID)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(" SELECT '1' AS VOUCHER_TYPE,");
            sb.Append(" TERM_NO AS HOST_ID,");
            sb.Append(" TO_DATE(TDATE,'yyyy/mm/dd') AS TRADE_DATE, ");
            sb.Append(" TAX_NO AS INVOICE_NO,");
            sb.Append(" STORENO AS STORE_NO,");
            sb.Append(" TO_DATE(TDATE,'yyyy/mm/dd') AS INVOICE_DATE,");
            sb.Append(" '發票' AS INVOICE_TYPE, ");
            sb.Append(" '' AS REMARK, ");
            sb.Append(" SERIAL_NO AS SALE_NO, ");
            sb.Append(" SUBTOT AS SALE_TOTAL_AMOUNT ");
            sb.Append(" FROM THD ");
            sb.AppendFormat(" Where POSUUID_MASTER = {0} ", OracleDBUtil.SqlStr(UUID));
     
            OracleConnection objConn = null;
            try
            {
                objConn = Advtek.Utility.OracleDBUtil.GetConnection();
                return Advtek.Utility.OracleDBUtil.GetDataSet(objConn, sb.ToString()).Tables[0];
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
        /// 取得舊POS交易表身資料
        /// </summary>
        /// <param name="POSUUID_MASTER"></param>
        /// <returns></returns>
        public DataTable getSale_Detail(string UUID)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(" SELECT '1' AS VOUCHER_TYPE,");
            sb.Append(" TERM_NO AS HOST_ID,");
            sb.Append(" TO_DATE(TDATE,'yyyy/mm/dd') AS TRADE_DATE, ");
            sb.Append(" STORENO AS STORE_NO,");
            sb.Append(" TO_DATE(TDATE,'yyyy/mm/dd') AS INVOICE_DATE,");
            sb.Append(" PLU_NO AS PRODNO,");
            sb.Append(" NAME AS PRODNAME,");
            sb.Append(" 0 as IMEI_QTY, ");
            sb.Append(" imeino_1 as IMEI, ");
            sb.Append(" PROMOTENO AS PROMOTION_CODE,");
            sb.Append(" PROMOTENO AS PROMO_NAME, ");
            sb.Append(" DETAILID AS SEQNO,");
            sb.Append(" AMOUNT AS UNIT_PRICE,");
            sb.Append(" TAXFLAG AS TAXABLE,");
            sb.Append(" BARCODE1 AS BARCODE1,");
            sb.Append(" BARCODE2 AS BARCODE2,");
            sb.Append(" BARCODE3 AS BARCODE3,");
            sb.Append(" SIM_CARD_NO AS SIM_CARD_NO,");
            sb.Append(" MOTONUM AS MSISDN,");
            sb.Append(" QTY AS QUANTITY,");
            sb.Append(" NET_SALE AS BEFORE_TAX,");
            sb.Append(" TAX AS TAX,");
            sb.Append(" TAXFLAG AS TAXABLE,");
            sb.Append(" QTY * AMOUNT AS TOTAL_AMOUNT,");
            sb.Append(" '舊' AS ITEM_TYPE_NAME ");
            sb.Append(" FROM TDL ");
            sb.AppendFormat(" Where POSUUID_MASTER = {0} ", OracleDBUtil.SqlStr(UUID));
          
            OracleConnection objConn = null;
            try
            {
                objConn = Advtek.Utility.OracleDBUtil.GetConnection();
                return Advtek.Utility.OracleDBUtil.GetDataSet(objConn, sb.ToString()).Tables[0];
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
        /// 作廢原銷售交易資料
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public int invalidOldTransaction(OracleTransaction objTX, string POSUUID_MASTER, DateTime transDate, string cancelNo, string modiUser)
        {
            string sqlStr = "";
            if (transDate.Month == DateTime.Now.Month)
            {
                sqlStr = @"Update Sale_Head Set SALE_STATUS= '3', INVALID_DATE = sysdate, INVALID_NO = " + OracleDBUtil.SqlStr(cancelNo) +
                            ", INVALID_REMARK = '退貨作廢', MODI_DTM = sysdate, MODI_USER = " + OracleDBUtil.SqlStr(modiUser) + " Where POSUUID_MASTER = " + OracleDBUtil.SqlStr(POSUUID_MASTER);
            }
            else
            {
                sqlStr = @"Update Sale_Head Set SALE_STATUS= '4', INVALID_DATE = sysdate, INVALID_NO = " + OracleDBUtil.SqlStr(cancelNo) +
                            ", INVALID_REMARK = '跨月退貨作廢', MODI_DTM = sysdate, MODI_USER = " + OracleDBUtil.SqlStr(modiUser) + " Where POSUUID_MASTER = " + OracleDBUtil.SqlStr(POSUUID_MASTER);
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


        public static bool INVENTORY_RETURN(string PRODNO, string STORE_NO, string STOCK, string QTY, string MODI_USER, OracleTransaction objTX)
        {
            bool result = false;
            //判斷是否有庫存資料
            string sqlstr = string.Format("select * from inv_on_hand_current where STORE_NO = '{0}' and PRODNO = '{1}'", STORE_NO, PRODNO);
            OracleConnection conn = null;
            try
            {
                conn = objTX.Connection;
                string inv_onhand_current_id = "";
                OracleCommand cmd = new OracleCommand(sqlstr, conn, objTX);
                OracleDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    inv_onhand_current_id = dr.IsDBNull(0) ? "" : dr.GetString(0);

                }
                dr.Close();

                if (string.IsNullOrEmpty(inv_onhand_current_id))
                {
                    sqlstr = "insert into INV_ON_HAND_CURRENT (PRODNO,STOCK_ID,ON_HAND_QTY,INV_TYPE,BOOKED_QTY,CREATE_DTM,MODI_USER,MODI_DTM,STORE_NO,INV_ONHAND_CURRENT_ID) ";
                    sqlstr += string.Format("values(:PRODNO,:STOCK_ID,:ON_HAND_QTY,'',0,sysdate,:MODI_USER,sysdate,:STORE_NO,pos_uuid)");
                    cmd = new OracleCommand(sqlstr, conn, objTX);
                    cmd.Parameters.Add(":PRODNO", SqlDbType.NVarChar).Value = PRODNO;
                    cmd.Parameters.Add(":STOCK_ID", SqlDbType.NVarChar).Value = STOCK;
                    cmd.Parameters.Add(":ON_HAND_QTY", SqlDbType.Decimal).Value = OracleNumber.Parse(QTY);
                    cmd.Parameters.Add(":MODI_USER", SqlDbType.NVarChar).Value = MODI_USER;
                    cmd.Parameters.Add(":STORE_NO", SqlDbType.NVarChar).Value = STORE_NO;
                    cmd.ExecuteNonQuery();
                    result = true;
                }
                else
                {
                    sqlstr = "update INV_ON_HAND_CURRENT set ON_HAND_QTY = ON_HAND_QTY+ :ON_HAND_QTY,MODI_USER = :MODI_USER,MODI_DTM=sysdate where inv_onhand_current_id=:inv_onhand_current_id";
                    cmd = new OracleCommand(sqlstr, conn, objTX);
                    cmd.Parameters.Add(":PRODNO", SqlDbType.NVarChar).Value = PRODNO;
                    cmd.Parameters.Add(":inv_onhand_current_id", SqlDbType.NVarChar).Value = inv_onhand_current_id;
                    cmd.Parameters.Add(":ON_HAND_QTY", SqlDbType.Decimal).Value = OracleNumber.Parse(QTY);
                    cmd.Parameters.Add(":MODI_USER", SqlDbType.NVarChar).Value = MODI_USER;
                    cmd.Parameters.Add(":STORE_NO", SqlDbType.NVarChar).Value = STORE_NO;
                    cmd.ExecuteNonQuery();
                    result = true;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
         
            

            return result;
        }

        /// <summary>
        /// 作廢原銷售交易資料
        /// 判斷是否為作廢或折讓
        /// </summary>
        /// <param name="Invalid">是否為作廢</param> true 為作廢 false 為折讓
        /// <param name="POSUUID_MASTER">銷貨單號</param>
        /// <returns></returns>
        public int invalidOldTransactionPeriod(OracleTransaction objTX, string UUID, string modiUser,string MACHINE_ID)
        {
          
            StringBuilder sb = new StringBuilder();
            sb.Append(" Update THD Set ");
            sb.Append(" status= '1',");
            sb.Append(" CANCEL_DTM = sysdate, ");
            sb.AppendFormat(" CANCEL_USER = '{0}' ,", modiUser);
            sb.AppendFormat(" CANCEL_MACHINE = '{0}' ", MACHINE_ID);
            sb.AppendFormat(" Where POSUUID_MASTER = {0} ", OracleDBUtil.SqlStr(UUID));
          
            int ret = Advtek.Utility.OracleDBUtil.ExecuteSql(objTX, sb.ToString());
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
        /// <param name="POSUUID_MASTER">銷貨單號</param>
        /// <returns></returns>
        public int invalidOldTransaction1(OracleTransaction objTX, string POSUUID_MASTER, Boolean Invalid, string cancelNo, string modiUser)
        {
            string sqlStr = "";
            if (Invalid)
            {
                sqlStr = @"Update Sale_Head Set SALE_STATUS= '3', INVALID_DATE = sysdate, INVALID_NO = " + OracleDBUtil.SqlStr(cancelNo) +
                            ", INVALID_REMARK = '退貨作廢', MODI_DTM = sysdate, MODI_USER = " + OracleDBUtil.SqlStr(modiUser) + " Where POSUUID_MASTER = " + OracleDBUtil.SqlStr(POSUUID_MASTER);
            }
            else
            {
                sqlStr = @"Update Sale_Head Set SALE_STATUS= '4', INVALID_DATE = sysdate, INVALID_NO = " + OracleDBUtil.SqlStr(cancelNo) +
                            ", INVALID_REMARK = '跨期退貨作廢', MODI_DTM = sysdate, MODI_USER = " + OracleDBUtil.SqlStr(modiUser) + " Where POSUUID_MASTER = " + OracleDBUtil.SqlStr(POSUUID_MASTER);
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
        /// IMEI_LOG
        /// </summary>
        /// <param name="POSUUID_MASTER"></param>
        /// <returns></returns>
        public string IMEIRETURN_Log(OracleTransaction objTX, string POSUUID_MASTER)
        {
            string strMCode = "";
            OracleCommand oraCmd = null;
            try
            {
                oraCmd = new OracleCommand("SP_IMEIRETURN_LOG");
                oraCmd.CommandType = System.Data.CommandType.StoredProcedure;

                oraCmd.Parameters.Add(new OracleParameter("inPOSUUID_MASTER", POSUUID_MASTER));
                oraCmd.Parameters.Add(new OracleParameter("inCHANNEL_ID", "RETAIL"));
                oraCmd.Parameters.Add(new OracleParameter("inFUNC_ID", "SAL04"));
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

        public DataTable Export_StoreOrders(string sDate, string eDate, string STORENO)
        {
            //**2011/03/08 Tina：匯出時，主商品和搭贈商品要放在同一筆資料中

            #region "訂單資料"

            //單一商品查詢(訂單)
            StringBuilder sbColumns1 = new StringBuilder();
            sbColumns1.AppendLine(@" SELECT VR.VOUCHER_TYPE AS VOUCHER_TYPE,'' HOST_ID, H.* 
                                    ,DECODE(VR.VOUCHER_TYPE,'1',IH.INVOICE_NO,'2',MIH.INVOICE_NO,'3',RH.RECEIPT_NO) AS INVOICE_NO   
                                    ,DECODE(VR.VOUCHER_TYPE,'1',IH.INVOICE_DATE,'2',MIH.INVOICE_DATE,'3',RH.INVOICE_DATE) AS INVOICE_DATE   
                                    ,DECODE(VR.VOUCHER_TYPE,'1','發票','2',MIH.INVOICE_TYPE,'3',RH.RECEIPT_TYPE) AS INVOICE_TYPE 
                                    FROM SALE_HEAD H, VOUCHER_RELATION VR, MANUAL_INVOICE_HEAD MIH, INVOICE_HEAD IH, RECEIPT_HEAD RH
                                    Where H.POSUUID_MASTER = VR.SALE_HEAD_ID(+) 
                                        And H.POSUUID_MASTER = MIH.POSUUID_MASTER(+) 
                                        And H.POSUUID_MASTER = IH.POSUUID_MASTER(+) 
                                        And H.POSUUID_MASTER = RH.POSUUID_MASTER(+) 
                                    ");
            #endregion

            //查詢條件
            StringBuilder sbWhere = new StringBuilder();
            if (!string.IsNullOrEmpty(sDate))
            {
                sbWhere.AppendLine(" AND TO_CHAR(TO_DATE(H.TRADE_DATE, 'yyyy/MM/dd'), 'yyyy/MM/dd') >= " + OracleDBUtil.SqlStr(sDate));
            }

            if (!string.IsNullOrEmpty(eDate))
            {
                sbWhere.AppendLine(" AND TO_CHAR(TO_DATE(H.TRADE_DATE, 'yyyy/MM/dd'), 'yyyy/MM/dd') <= " + OracleDBUtil.SqlStr(eDate));
            }

            //總部人員可查詢各門市資料, 門市人員只能查詢自己的門市
            if (!string.IsNullOrEmpty(STORENO))
            {
                sbWhere.AppendLine(" AND H.STORE_NO=" + OracleDBUtil.SqlStr(STORENO));
            }


            StringBuilder sb = new StringBuilder();
            sb.AppendLine(@" SELECT 
                                ORDDATE             AS 訂單日期
                                , ORDER_NO          AS 訂單編號
                                , PRE_ORDER_NO      AS 預訂單號
                                , STORE_NO          AS 訂單門市
                                , STATUS            AS 狀態
                                , MODI_USER         AS 更新人員
                                , MODI_DTM          AS 更新日期
                                , PRODNO_M          AS 商品料號
                                , PRODNAME_M        AS 商品名稱
                                , ORDQTY_M          AS 訂購量
                                , APPROVEQTY_M      AS 總部核准量
                                , CHECK_IN_QTY_M    AS 驗收量              
                                , PRODNO            AS 搭配商品料號
                                , PRODNAME          AS 搭配商品名稱
                                , ORDQTY_D          AS "" 訂購量""
                                , APPROVEQTY_D      AS "" 總部核准量""
                                , CHECK_IN_QTY_D    AS "" 驗收量""
                            FROM (");
            sb.AppendLine(sbColumns1.ToString() + @" AND GIFT_FALG='0' 
                    AND D.PRODNO NOT IN (SELECT DISTINCT NVL(prodno_m,'NULL DATA'))");
            sb.AppendLine(" ) t");
            sb.AppendLine(" WHERE 1= 1");
            sb.AppendLine(sbWhere.ToString());

            DataTable dt = OracleDBUtil.Query_Data(sb.ToString());
            return dt;
        }


        public static bool CheckISSTOCK(string prodno)
        {
            bool result = false;
            OracleConnection conn = null;

            string sqlstr = "select ISSTOCK from PRODUCT where ISSTOCK='1' and prodno = " + OracleDBUtil.SqlStr(prodno);

            try
            {
                conn = OracleDBUtil.GetConnection();
                OracleCommand cmd = new OracleCommand(sqlstr, conn);
                OracleDataReader dr = cmd.ExecuteReader();
                result = dr.HasRows;
                dr.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (conn.State == ConnectionState.Open) conn.Close();
                OracleConnection.ClearPool(conn);
            }

            return result;
        }
    }
}