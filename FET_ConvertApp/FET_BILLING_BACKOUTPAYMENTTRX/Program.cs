using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.OracleClient;
using System.IO;
using Advtek.Utility;
using System.Threading;

namespace FET_BILLING_BACKOUTPAYMENTTRX
{
    class Program
    {
        static void Main(string[] args)
        {
            FET_BILLING_BACKOUTPAYMENTTRX nb = new FET_BILLING_BACKOUTPAYMENTTRX();
            nb.DOMain();
        }


    }

    public class FET_BILLING_BACKOUTPAYMENTTRX
    {

        public string DOMain()
        {
            //初始化LOG
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            OracleConnection oCon = null;
            OracleTransaction oTX = null;
            //string sPath = "";
            oCon = OracleDBUtil.GetConnection();
            oTX = oCon.BeginTransaction();
            ConvertLog cLog = new ConvertLog("FET_BILLING_BACKOUTPAYMENTTRX");
            try
            {
                Console.WriteLine("FET_BILLING_INSPAYMENTTRX");
                Console.WriteLine("初始化LOG");
                Console.WriteLine("產生取消FET帳單繳款(交易作廢)銷帳檔");
                DataTable dt = Query_SALE_HEAD(oCon, oTX);
                if (dt.Rows.Count > 0)
                {

                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        DataRow dr = dt.Rows[i];
                        sb.Length = 0;
                        sb.Append("Select count(*) as myvalue ");
                        sb.Append("from BILL_DISPATCH where BILL_DISPATCH_ID=" + OracleDBUtil.SqlStr(dr["posuuid_master"].ToString()));
                        DataTable dt1 = OracleDBUtil.GetDataSet(oTX, sb.ToString()).Tables[0];
                        if (dt1.Rows.Count == 0)
                        {
                            //call 拆帳
                            Console.WriteLine("PK_BILL.PAYMODESPLITONE");
                            string sMsg = PAYMODESPLITONE(oCon, oTX, dr["posuuid_master"].ToString());
                            Console.WriteLine("產生帳單支付方式拆分執行結束，寫入LOG");
                            cLog.Success(sMsg);
                        }
                        Console.WriteLine("PK_BILL.INFET_BILLING_CANCLE_FILE");
                        string sMsg1 = INFET_BILLING_CANCLE_FILE(oCon, oTX, dr["posuuid_master"].ToString());
                        Console.WriteLine("產生取消FET帳單繳款(交易作廢)的交易執行結束，寫入LOG");
                        cLog.Success(sMsg1);

                    }
                }


                DataTable dt2 = Query_FET_BILLING_CANCLE_FILE(oCon, oTX);
                if (dt2.Rows.Count > 0)
                {
                    Console.WriteLine("取得檔名");
                    string fileName = "FET_" + DateTime.Now.ToString("yyyyMMddHHmmss") + "_BackOutPaymentTrx.XML";
                    //sPath = GetUrl() + "\\SERVER SIDE\\Bill_FILES\\FET\\" + fileName;
                    if (!Directory.Exists(GetUrl() + "\\SERVER SIDE\\Bill_FILES\\FET\\")) Directory.CreateDirectory(GetUrl() + "\\SERVER SIDE\\Bill_FILES\\FET\\");
                    //sPath = "D:\\work\\FET_POS\\FET_WEB_POS_v2\\SERVER SIDE\\Bill_FILES\\FET\\" + fileName;
                    //Console.WriteLine("暫存檔檔名與路徑:" + sPath);
                    string V_BATCH_NO = GetGoodLOCUUID();
                    //StreamWriter sw = new StreamWriter(sPath);
                    sb.Length = 0;
                    sb.Append("http://IP:port/posapp/BackoutPaymentTrx?req=<?xml version=" + @"""1.0""" + " encoding=" + @"""Big5""" + " ?>");
                    for (int i = 0; i < dt2.Rows.Count; i++)
                    {
                        DataRow dr = dt2.Rows[i];
                        sb.Append("<fet-pos-backout-req>");
                        sb.Append("<account-id>" + dr["ACCOUNT_ID"].ToString() + "</account-id>");
                        sb.Append("<msisdn>" + dr["MSISDN"].ToString() + "</msisdn>");
                        sb.Append("<amount>" + dr["AMOUNT"].ToString() + "</amount>");
                        sb.Append("<backout-reason>BCK-WP</backout-reason>");
                        sb.Append("<payment-id>0</payment-id>");
                        sb.Append("<login-id>" + dr["LOGIN_ID"].ToString() + "</login-id>");
                        sb.Append("<password>" + dr["PASSWORD"].ToString() + "</password>");
                        sb.Append("<pos-key>" + dr["POS_KEY"].ToString() + "</pos-key>");
                        sb.Append("<store-type>" + dr["STORE_TYPE"].ToString() + "</store-type>");
                        sb.Append("<payment-source-id>" + dr["PAYMENT_SOURCE_ID"].ToString() + "</payment-source-id>");
                        sb.Append("<store-number>" + dr["STORE_NUMBER"].ToString() + "</store-number>");
                        sb.Append("</fet-pos-backout-req>");
                    }
                    sb.Append("</XML>");
                    //資料寫入
                    Console.WriteLine("取消FET帳單繳款資料寫入");
                    cLog.Success("取消FET帳單繳款資料寫入");
                    //sw.Write(sb.ToString());
                    //sw.Close();
                    UPDATE_FET_BILLING_CANCLE_FILE(oCon, oTX);
                }
                else
                {
                    Console.WriteLine("未產生取消FET帳單繳款資料，寫入LOG");
                    cLog.Success("未產生取消FET帳單繳款資料");
                    UPDATE_FET_BILLING_CANCLE_FILE(oCon, oTX);
                }
                oTX.Commit();
            }
            catch (Exception ex)
            {
                oTX.Rollback();
                cLog.Fail(ex.Message);
                Console.Write(ex.Message.ToString());
                Thread.Sleep(5000);
                return ex.Message;
            }
            finally
            {
                if (oTX != null) oTX.Dispose();
                if (oCon != null)
                {
                    if (oCon.State == ConnectionState.Open) oCon.Close();
                    oCon.Dispose();
                }
            }
            return sb.ToString();
        }

        static string GetGoodLOCUUID()
        {
            DataTable dt = null;
            OracleConnection objConn = null;

            try
            {
                objConn = OracleDBUtil.GetConnection();
                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                sb.Append("select INV_GoodLOCUUID() as UUID from dual");

                dt = OracleDBUtil.GetDataSet(objConn, sb.ToString()).Tables[0];

                string UUID = "";
                if (dt.Rows.Count > 0)
                {
                    UUID = dt.Rows[0]["UUID"].ToString();
                }

                return UUID;
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

        public static DataTable Query_SALE_HEAD(OracleConnection oCon, OracleTransaction oTX)
        {
            try
            {
                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                sb.Append("select h.posuuid_master                   ");
                sb.Append("from Sale_head H ");
                sb.Append("where 1=1 ");
                sb.Append("and H.SALE_TYPE = '2'   ");
                sb.Append("and H.SALE_STATUS in ('3','4','5','6') ");
                sb.Append("and H.FLG_INVALID_DISPATCH='N' ");

                DataTable dt = OracleDBUtil.GetDataSet(oTX, sb.ToString()).Tables[0];
                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
            }
        }

        public static DataTable Query_FET_BILLING_CANCLE_FILE(OracleConnection oCon, OracleTransaction oTX)
        {
            try
            {
                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                sb.Append("UPDATE FET_BILLING_CANCLE_FILE set send_flag='T' where send_flag='0' ");

                OracleDBUtil.ExecuteSql(oTX, sb.ToString());
                sb.Length = 0;
                sb.Append("	 SELECT                                                   ");
                sb.Append("   ROWID as ITEM_NO, F.FET_BILL_CAN_FILE_ID,               ");
                sb.Append("   F.ACCOUNT_ID, F.MSISDN,                                 ");
                sb.Append("   F.AMOUNT, F.LOGIN_ID, F.PASSWORD,                       ");
                sb.Append("   F.backout_reason,                                       ");
                sb.Append("   F.payment_id,                                           ");
                sb.Append("   F.PAYMENT_SOURCE_ID,                                    ");
                sb.Append("   F.POS_KEY, F.STORE_TYPE, F.TRANS_DATE, F.STORE_NUMBER,  ");
                sb.Append("   F.FILE_NO, F.SEND_FLAG, F.SEND_DTM,                     ");
                sb.Append("   F.RETRY_FLAG, F.RETRY_COUNT, F.FET_PROCESS_STATUS,      ");
                sb.Append("   F.ERROR_MESSAGE, F.BATCH_NO, F.BILL_DISPATCH_ID         ");
                sb.Append("	 FROM FET_BILLING_CANCLE_FILE F                           ");
                sb.Append("   WHERE f.send_flag='T'                                     ");

                DataTable dt = OracleDBUtil.GetDataSet(oTX, sb.ToString()).Tables[0];
                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
            }
        }

        static void UPDATE_FET_BILLING_CANCLE_FILE(OracleConnection oCon, OracleTransaction oTX)
        {
            try
            {
                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                sb.Append("UPDATE FET_BILLING_CANCLE_FILE ");
                sb.Append("   SET SEND_FLAG='T' ");
                sb.Append(" WHERE SEND_FLAG='0'");
                OracleDBUtil.ExecuteSql(oTX, sb.ToString());
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
            }
        }

        public static string PAYMODESPLITONE(OracleConnection objConn, OracleTransaction objTX, string I_MASTER_ID)
        {
            string sRet = "";
            try
            {
                OracleCommand oraCmd = new OracleCommand("PK_BILL.PAYMODESPLITONE");
                oraCmd.CommandType = System.Data.CommandType.StoredProcedure;
                oraCmd.Parameters.Add(new OracleParameter("I_MASTER_ID", OracleType.VarChar, 2000)).Value = I_MASTER_ID;
                oraCmd.Connection = objConn;
                oraCmd.Transaction = objTX;
                oraCmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {

            }
            return sRet;
        }

        public static string INFET_BILLING_CANCLE_FILE(OracleConnection objConn, OracleTransaction objTX, string I_MASTER_ID)
        {
            string sRet = "";
            try
            {
                OracleCommand oraCmd = new OracleCommand("PK_BILL.INFET_BILLING_CANCLE_FILE");
                oraCmd.CommandType = System.Data.CommandType.StoredProcedure;
                oraCmd.Parameters.Add(new OracleParameter("I_BILL_DISPATCH_ID", OracleType.VarChar, 2000)).Value = I_MASTER_ID;
                oraCmd.Connection = objConn;
                oraCmd.Transaction = objTX;
                oraCmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {

            }
            return sRet;
        }

        public static string GetUrl()
        {

            DataTable dt = null;
            OracleConnection objConn = null;

            try
            {
                objConn = OracleDBUtil.GetConnection();
                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                sb.Append("select para_value from sys_para where para_key='IIS_URL'");

                dt = OracleDBUtil.GetDataSet(objConn, sb.ToString()).Tables[0];

                string Url = "";
                if (dt.Rows.Count > 0)
                {
                    Url = dt.Rows[0]["para_value"].ToString();
                }

                return Url;
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
    }
}
