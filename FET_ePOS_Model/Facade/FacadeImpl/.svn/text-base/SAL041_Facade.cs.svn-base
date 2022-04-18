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
    public class SAL041_Facade : BaseClass
    {

        public DataTable Query_SaleMethodSet(string StoreNo, string S_TradeDate, string E_TradeDate, string CashRegister, string CustomerNum, string Status, string TransactionNo, string PromotionCode, string SALE_PERSON, string InvoiceNo, string ProdNo, string PaymentMethod)
        {
            OracleConnection objConn = null;
            OracleTransaction objTX = null;

            try
            {
                objConn = OracleDBUtil.GetConnection();
                objTX = objConn.BeginTransaction();
                System.Text.StringBuilder sb = new System.Text.StringBuilder();


                sb.Append(" SELECT ROWNUM AS SEQNO ,A.* FROM ( SELECT DISTINCT                                                                          ");
                sb.Append("        DECODE(SH.SALE_STATUS,'1','未結帳','2','已結帳','3','已作廢','4','已作廢','5','已作廢','6','已作廢') AS STATUS       ");
                sb.Append("       ,TO_CHAR(SH.TRADE_DATE,'yyyy/MM/dd') AS TRADE_DATE                                                                    ");
                sb.Append("       ,SH.SALE_NO                                                                                                           ");
                sb.Append("       ,SH.MACHINE_ID                                                                                                        ");
                sb.Append("       ,SD.MSISDN                                                                                                            ");
                sb.Append("       ,DECODE(VR.VOUCHER_TYPE,'1',IH.INVOICE_NO,'2',MIH.INVOICE_NO,'3',RH.RECEIPT_NO) AS INVOICE_NO                         ");
                sb.Append("       ,SH.SALE_TOTAL_AMOUNT                                                                                                 ");
                sb.Append("       ,DECODE(PD.PAID_MODE,1,'現金',2,'信用卡',3,'離線信用卡',4,'分期付款',5,'禮券',6,'金融卡',7,'Happy Go') AS PAID_MODE   ");
                sb.Append("       ,SH.SALE_PERSON                                                                                                       ");
                sb.Append("       ,EM.EMPNAME                                                                                                           ");
                sb.Append("       ,TO_CHAR(SH.MODI_DTM,'yyyy/MM/dd') AS MODI_DTM                                                                        ");
                sb.Append("       ,SH.POSUUID_MASTER                                                                                                    ");
                sb.Append("       ,SH.STORE_NO                                                                                                          ");
                sb.Append("       ,SD.PRODNO                                                                                                            ");
                sb.Append("       ,SD.PROMOTION_CODE                                                                                                    ");
                sb.Append("   FROM SALE_HEAD SH ,SALE_DETAIL SD ,PAID_DETAIL PD ,VOUCHER_RELATION VR                                                    ");
                sb.Append("       ,INVOICE_HEAD IH ,MANUAL_INVOICE_HEAD MIH ,RECEIPT_HEAD RH, EMPLOYEE EM                                               ");
                sb.Append("  WHERE 1 = 1                                                                                                                ");
                sb.Append("    AND SH.POSUUID_MASTER = SD.POSUUID_MASTER                                                                                ");
                sb.Append("    AND SH.POSUUID_MASTER = PD.POSUUID_MASTER                                                                                ");
                sb.Append("    AND SH.POSUUID_MASTER = VR.SALE_HEAD_ID                                                                                  ");
                sb.Append("    AND SH.POSUUID_MASTER = IH.POSUUID_MASTER(+)                                                                             ");
                sb.Append("    AND SH.POSUUID_MASTER = MIH.POSUUID_MASTER(+)                                                                            ");
                sb.Append("    AND SH.POSUUID_MASTER = RH.POSUUID_MASTER(+)                                                                             ");
                sb.Append("    AND EM.EMPNO = SH.SALE_PERSON(+)                                                                                         ");
                sb.Append("       ) A                                                                                                                   ");
                sb.Append("         WHERE 1 = 1                                                                                                         ");


                if (!string.IsNullOrEmpty(StoreNo))
                {
                    sb.Append(" AND A.STORE_NO =  " + Advtek.Utility.OracleDBUtil.SqlStr(StoreNo) + "  ");
                }
                if (!string.IsNullOrEmpty(S_TradeDate))
                {
                    sb.Append(" AND A.TRADE_DATE >=  " + Advtek.Utility.OracleDBUtil.SqlStr(S_TradeDate) + "  ");
                }
                if (!string.IsNullOrEmpty(E_TradeDate))
                {
                    sb.Append(" AND A.TRADE_DATE <=  " + Advtek.Utility.OracleDBUtil.SqlStr(E_TradeDate) + "  ");
                }
                if (!string.IsNullOrEmpty(CashRegister))
                {
                    sb.Append(" AND A.MACHINE_ID =  " + Advtek.Utility.OracleDBUtil.SqlStr(CashRegister) + "  ");
                }
                if (!string.IsNullOrEmpty(CustomerNum))
                {
                    sb.Append(" AND A.MSISDN =  " + Advtek.Utility.OracleDBUtil.SqlStr(CustomerNum) + "  ");
                }
                if (!string.IsNullOrEmpty(Status))
                {
                    sb.Append(" AND A.STATUS =  " + Advtek.Utility.OracleDBUtil.SqlStr(Status) + "  ");
                }
                if (!string.IsNullOrEmpty(TransactionNo))
                {
                    sb.Append(" AND A.SALE_NO =  " + Advtek.Utility.OracleDBUtil.SqlStr(TransactionNo) + "  ");
                }
                if (!string.IsNullOrEmpty(PromotionCode))
                {
                    sb.Append(" AND A.PROMOTION_CODE =  " + Advtek.Utility.OracleDBUtil.SqlStr(PromotionCode) + "  ");
                }
                if (!string.IsNullOrEmpty(SALE_PERSON) && SALE_PERSON.Trim() != "請選擇")
                {
                    sb.Append(" AND A.SALE_PERSON =  " + Advtek.Utility.OracleDBUtil.SqlStr(SALE_PERSON) + "  ");
                }
                if (!string.IsNullOrEmpty(InvoiceNo))
                {
                    sb.Append(" AND A.INVOICE_NO =  " + Advtek.Utility.OracleDBUtil.SqlStr(InvoiceNo) + "  ");
                }
                if (!string.IsNullOrEmpty(ProdNo))
                {
                    sb.Append(" AND A.PRODNO =  " + Advtek.Utility.OracleDBUtil.SqlStr(ProdNo) + "  ");
                }
                if (!string.IsNullOrEmpty(PaymentMethod) && PaymentMethod.Trim() != "請選擇")
                {
                    sb.Append(" AND A.PAID_MODE =  " + Advtek.Utility.OracleDBUtil.SqlStr(PaymentMethod) + "  ");
                }

                OracleConnection advtekUtilityOracleDBUtilGetConnection = Advtek.Utility.OracleDBUtil.GetConnection();
                DataTable dt = Advtek.Utility.OracleDBUtil.GetDataSet(advtekUtilityOracleDBUtilGetConnection, sb.ToString()).Tables[0];

                objTX.Commit();

                return dt;
            }
            catch (Exception ex)
            {
                objTX.Rollback();
                throw ex;
            }
            finally
            {
                objTX.Dispose();
                if (objConn.State == ConnectionState.Open)
                    objConn.Close();
                objConn.Dispose();
                OracleConnection.ClearAllPools();
            }

        }

        //1.POSUUID; 2.銷售狀態:3-退貨作廢;4跨月退貨作廢; 3.憑證類型=>1:發票;2:手開發票;3:收據; 4.user_id; 5.門市
        //6.發票號碼; 7.Gridview-gvMaster; 8.作廢序號; 9.作廢備註; 10.折讓單號; 11.HOST_IP;
        public void Update_SaleData(string POSUUID_MASTER, string SALE_STATUS, string VOUCHER_TYPE, string MODI_USER, string STORE_NO, string INVOICE_NO, DataTable dtDetail, string INVALID_NO, string INVALID_REMARK, string CREDIT_NOTE_NO, string HOST_IP)
        {
            OracleConnection objConn = null;
            OracleTransaction objTX = null;

            try
            {
                INVENTORY_Facade Inventory = new INVENTORY_Facade();
                objConn = OracleDBUtil.GetConnection();
                objTX = objConn.BeginTransaction();

                string Code = "";
                string Message = "";
                string strSQL = "";


                if (SALE_STATUS == "4")
                {
                    strSQL = "  update SALE_HEAD SET SALE_STATUS = '4' ,INVALID_DATE = sysdate ";
                    strSQL += ",INVALID_NO = '" + INVALID_NO + "'";
                    strSQL += ",INVALID_REMARK = '" + INVALID_REMARK + "'";
                    strSQL += ",INVALID_REASON = '" + MODI_USER + "'";
                    strSQL += ",MODI_USER = '" + MODI_USER + "'";
                    strSQL += ",MODI_DTM  = sysdate ";
                    strSQL += " where POSUUID_MASTER = '" + POSUUID_MASTER + "'";
                    //跨月退貨作廢
                    OracleDBUtil.ExecuteSql(objTX, strSQL);

                    /* 憑證類型=>1: 發票; 2: 手開發票; 3: 收據 */
                    if (VOUCHER_TYPE == "1")
                    {
                        strSQL = "  update INVOICE_HEAD SET CREDIT_NOTE_NO = '" + CREDIT_NOTE_NO + "' ,CREDIT_NOTE_STATUS = 'Y' ";
                        strSQL += ",IS_INVALID = 'Y' ,INVALID_DATE = sysdate ";
                        strSQL += ",MODI_USER = '" + MODI_USER + "'";
                        strSQL += ",MODI_DTM  = sysdate ";
                        strSQL += " where POSUUID_MASTER = '" + POSUUID_MASTER + "'";
                        /* CREDIT_NOTE_STATUS => Y: 已申報 ; N: 未申報 */
                        /* IS_INVALID作廢狀態: Y. YES ; N. NO  */
                        OracleDBUtil.ExecuteSql(objTX, strSQL);
                    }
                    else if (VOUCHER_TYPE == "2")
                    {
                        strSQL = "  update MANUAL_INVOICE_HEAD SET CREDIT_NOTE_NO = '" + CREDIT_NOTE_NO + "' ,CREDIT_NOTE_STATUS = 'Y' ";
                        strSQL += ",IS_INVALID = 'Y' ,INVALID_DATE = sysdate ";
                        strSQL += ",MODI_USER = '" + MODI_USER + "'";
                        strSQL += ",MODI_DTM  = sysdate ";
                        strSQL += " where POSUUID_MASTER = '" + POSUUID_MASTER + "'";
                        /* CREDIT_NOTE_STATUS => Y: 已申報 ; N: 未申報 */
                        /* IS_INVALID作廢狀態: Y. YES ; N. NO  */
                        OracleDBUtil.ExecuteSql(objTX, strSQL);

                    }
                    else if (VOUCHER_TYPE == "3")
                    {
                        strSQL = "  update RECEIPT_HEAD SET IS_INVALID = 'Y' ,INVALID_DATE = sysdate ";
                        strSQL += ",MODI_USER = '" + MODI_USER + "'";
                        strSQL += ",MODI_DTM  = sysdate ";
                        strSQL += " where POSUUID_MASTER = '" + POSUUID_MASTER + "'";
                        /* IS_INVALID作廢狀態: Y. YES ; N. NO  */
                        OracleDBUtil.ExecuteSql(objTX, strSQL);

                    }
                }
                else if (SALE_STATUS == "3")
                {
                    strSQL = "  update SALE_HEAD SET SALE_STATUS = '3' ,INVALID_DATE = sysdate ";
                    strSQL += ",INVALID_NO = '" + INVALID_NO + "'";
                    strSQL += ",INVALID_REMARK = '" + INVALID_REMARK + "'";
                    strSQL += ",INVALID_REASON = '" + MODI_USER + "'";
                    strSQL += ",MODI_USER = '" + MODI_USER + "'";
                    strSQL += ",MODI_DTM  = sysdate ";
                    strSQL += " where POSUUID_MASTER = '" + POSUUID_MASTER + "'";
                    //退貨作廢
                    OracleDBUtil.ExecuteSql(objTX, strSQL);

                    /* 憑證類型=>1: 發票; 2: 手開發票; 3: 收據 */
                    if (VOUCHER_TYPE == "1")
                    {
                        strSQL = "  update INVOICE_HEAD SET IS_INVALID = 'Y' ,INVALID_DATE = sysdate ";
                        strSQL += ",MODI_USER = '" + MODI_USER + "'";
                        strSQL += ",MODI_DTM  = sysdate ";
                        strSQL += " where POSUUID_MASTER = '" + POSUUID_MASTER + "'";
                        /* IS_INVALID作廢狀態: Y. YES ; N. NO  */
                        OracleDBUtil.ExecuteSql(objTX, strSQL);
                    }
                    else if (VOUCHER_TYPE == "2")
                    {
                        strSQL = "  update MANUAL_INVOICE_HEAD SET IS_INVALID = 'Y' ,INVALID_DATE = sysdate ";
                        strSQL += ",MODI_USER = '" + MODI_USER + "'";
                        strSQL += ",MODI_DTM  = sysdate ";
                        strSQL += " where POSUUID_MASTER = '" + POSUUID_MASTER + "'";
                        /* IS_INVALID作廢狀態: Y. YES ; N. NO  */
                        OracleDBUtil.ExecuteSql(objTX, strSQL);

                    }
                    else if (VOUCHER_TYPE == "3")
                    {
                        strSQL = "  update RECEIPT_HEAD SET IS_INVALID = 'Y' ,INVALID_DATE = sysdate ";
                        strSQL += ",MODI_USER = '" + MODI_USER + "'";
                        strSQL += ",MODI_DTM  = sysdate ";
                        strSQL += " where POSUUID_MASTER = '" + POSUUID_MASTER + "'";
                        /* IS_INVALID作廢狀態: Y. YES ; N. NO  */
                        OracleDBUtil.ExecuteSql(objTX, strSQL);

                    }
                }


                //發票作廢註銷發票號碼
                SP_INVOICE_NO_RELEASE(objTX, STORE_NO, INVOICE_NO, POSUUID_MASTER, MODI_USER, HOST_IP, ref Code, ref Message);

                if (Code != "") throw new Exception("交易序號：" + "" + ", 發票號碼：" + INVOICE_NO + "發票作廢失敗. ERROR_MSG:" + Message);



                if (dtDetail.Rows.Count > 0)
                {
                    foreach (DataRow dr in dtDetail.Rows)
                    {
                        //異動庫存 PK_INVENTORY_RETURN(); 
                        //string Code = "";
                        //string Message = "";
                        string Stock = FET.POS.Model.Common.Common_PageHelper.GetGoodLOCUUID();

                        Inventory.PK_INVENTORY_RETURN(objTX, "1", dr["PRODNO"].ToString(),
                           STORE_NO, Stock, "", Convert.ToInt32(dr["QUANTITY"].ToString()),
                           MODI_USER, dr["ID"].ToString(), ref Code, ref Message);

                        if (Code == "999") throw new Exception("交易序號：" + "" + ", 商品料號：" + dr["PRODNO"].ToString() + "異動庫存檔失敗. ERROR_MSG:" + Message);

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
                objTX.Dispose();
                if (objConn.State == ConnectionState.Open)
                    objConn.Close();
                objConn.Dispose();
                OracleConnection.ClearAllPools();
            }

        }

        public void SP_INVOICE_NO_RELEASE(OracleTransaction objTX, string I_STORE_NO, string I_INVOICE_NO, string I_POSUUID_MASTER, string I_USER, string I_HOST_ID, ref string O_RT_CODE, ref string O_RT_MESSAGE)
        {
            OracleCommand oraCmd = null;

            try
            {
                oraCmd = new OracleCommand("PK_INVOICE.SP_INVOICE_NO_RELEASE");
                oraCmd.CommandType = System.Data.CommandType.StoredProcedure;
                oraCmd.Parameters.Add(new OracleParameter("STORE_NO", OracleType.VarChar, 2000)).Value = I_STORE_NO;
                oraCmd.Parameters.Add(new OracleParameter("INVOICE_NO", OracleType.VarChar, 2000)).Value = I_INVOICE_NO;
                oraCmd.Parameters.Add(new OracleParameter("POS_MASTER_UUID", OracleType.VarChar, 2000)).Value = I_POSUUID_MASTER;
                oraCmd.Parameters.Add(new OracleParameter("USER_ID", OracleType.VarChar, 2000)).Value = I_USER;
                oraCmd.Parameters.Add(new OracleParameter("HOST_ID", OracleType.VarChar, 2000)).Value = I_HOST_ID;

                oraCmd.Parameters.Add(new OracleParameter("outMSGCODE", OracleType.VarChar, 2000)).Direction = ParameterDirection.Output;
                oraCmd.Parameters.Add(new OracleParameter("outMESSAGE", OracleType.VarChar, 2000)).Direction = ParameterDirection.Output;

                oraCmd.Connection = objTX.Connection;
                oraCmd.Transaction = objTX;
                oraCmd.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                oraCmd.Dispose();
            }
        }

        /// <summary>
        /// 作廢GL分錄
        /// </summary>
        /// <param name="POSUUID_MASTER">銷售單主鍵</param>
        /// <returns></returns>
        public string runGL(OracleTransaction objTX, string POSUUID_MASTER)
        {
            string strMCode = "";
            OracleCommand oraCmd = null;
            try
            {
                oraCmd = new OracleCommand("PK_GL.GL_RT");
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

        /// <summary>
        /// 作廢原銷售交易資料
        /// 判斷是否為作廢或折讓
        /// </summary>
        /// <param name="Invalid">是否為作廢</param> true 為作廢 false 為折讓
        /// <param name="POSUUID_MASTER">銷貨單號</param>
        /// <returns></returns>
        public int invalidOldTransactionPeriod(OracleTransaction objTX, string POSUUID_MASTER, DateTime invoiceDate, string cancelNo, string modiUser)
        {
            string sqlStr = "";
            int invMonth = invoiceDate.Month;
            int curMonth = DateTime.Now.Month;
            if ((invMonth == curMonth) || ((invMonth % 2 == 1) && (curMonth == invMonth + 1)))
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
        public string IMEIRETURN_Log(OracleTransaction objTX, string POSUUID_MASTER, string MODI_USER)
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
        /// INVOICE
        /// </summary>
        /// <param name="POSUUID_MASTER"></param>
        /// <returns></returns>
        public DataTable GetINVOICE(string POSUUID_MASTER)
        {

            string sqlStr = @"SELECT ID,INVOICE_DATE FROM INVOICE_HEAD Where (IS_INVALID IS NULL OR IS_INVALID <> 'Y') AND POSUUID_MASTER = '" + POSUUID_MASTER + "' UNION ALL ";
            sqlStr += "SELECT ID,INVOICE_DATE FROM MANUAL_INVOICE_HEAD Where (IS_INVALID IS NULL OR IS_INVALID <> 'Y') AND POSUUID_MASTER = '" + POSUUID_MASTER + "'";
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
        /// INVOICE
        /// </summary>
        /// <param name="POSUUID_MASTER"></param>
        /// <returns></returns>
        public bool IsExistINVOICE(string POSUUID_MASTER)
        {

            string sqlStr = @"SELECT ID,INVOICE_DATE FROM INVOICE_HEAD Where POSUUID_MASTER = '" + POSUUID_MASTER + "' UNION ALL ";
            sqlStr += "SELECT ID,INVOICE_DATE FROM MANUAL_INVOICE_HEAD Where POSUUID_MASTER = '" + POSUUID_MASTER + "'";
            OracleConnection objConn = null;
            try
            {
                objConn = Advtek.Utility.OracleDBUtil.GetConnection();
                DataTable dt = Advtek.Utility.OracleDBUtil.GetDataSet(objConn, sqlStr).Tables[0];
                if (dt != null && dt.Rows.Count > 0)
                    return true;
                else
                    return false;
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
        /// 取得FETC料號
        /// </summary>
        /// <returns>string</returns>
        public string getAllFETCProuductNo()
        {
            OracleConnection objConn = null;
            try
            {
                objConn = OracleDBUtil.GetConnection();
                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                sb.Append("select para_value from sys_para where para_key like 'ETC%CODE' ");

                DataTable dt = OracleDBUtil.GetDataSet(objConn, sb.ToString()).Tables[0];

                if (dt != null && dt.Rows.Count > 0)
                {
                    sb = new StringBuilder("");
                    foreach (DataRow dr in dt.Rows)
                    {
                        sb.Append(OracleDBUtil.SqlStr(StringUtil.CStr(dr[0]))).Append(",");
                    }
                    string strProd = sb.ToString();
                    return strProd.Substring(0, strProd.Length - 1);
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
    }
}