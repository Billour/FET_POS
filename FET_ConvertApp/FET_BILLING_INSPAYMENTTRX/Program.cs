using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.OracleClient;
using System.IO;
using Advtek.Utility;
using System.Threading;

namespace FET_BILLING_INSPAYMENTTRX
{
    class Program
    {
        static void Main(string[] args)
        {
            FET_BILLING_INSPAYMENTTRX nb = new FET_BILLING_INSPAYMENTTRX();
            nb.DOMain("");
        }


    }
    public class FET_BILLING_INSPAYMENTTRX
    {

        public string DOMain(string UUID)
        {
            //初始化LOG
            OracleConnection oCon = null;
            OracleTransaction oTX = null;
            oCon = OracleDBUtil.GetConnection();
            oTX = oCon.BeginTransaction();
            //string sPath = "";
            StringBuilder sb = new StringBuilder();
            ConvertLog cLog = new ConvertLog("FET_BILLING_INSPAYMENTTRX");
            try
            {
                Console.WriteLine("FET_BILLING_INSPAYMENTTRX");
                Console.WriteLine("初始化LOG");
                Console.WriteLine("產生銷帳檔資料");
                Console.WriteLine("PK_BILL.INFET_BILLING_FILE");
                string sMsg = INFET_BILLING_FILE(oCon, oTX, UUID);
                Console.WriteLine("產生銷帳檔資料執行結束，寫入LOG");
                cLog.Success(sMsg);

                DataTable dt = Query_FET_BILLING_FILE(oCon, oTX);
                if (dt.Rows.Count > 0)
                {
                    Console.WriteLine("取得檔名");
                    string fileName = "FET_" + DateTime.Now.ToString("yyyyMMddHHmmss") + "_InsPaymentTrx.XML";
                    //sPath = GetUrl() + "\\SERVER SIDE\\Bill_FILES\\FET\\" + fileName;
                    if (!Directory.Exists(GetUrl() + "\\SERVER SIDE\\Bill_FILES\\FET\\")) Directory.CreateDirectory(GetUrl() + "\\SERVER SIDE\\Bill_FILES\\FET\\");
                    //sPath = "D:\\work\\FET_POS\\FET_WEB_POS_v2\\SERVER SIDE\\Bill_FILES\\FET\\" + fileName;
                    //Console.WriteLine("暫存檔檔名與路徑:" + sPath);
                    string V_BATCH_NO = GetGoodLOCUUID();
                    //StreamWriter sw = new StreamWriter(sPath);

                    sb.Append("http://IP:port/posapp/InsPaymentTrx?req=<?xml version=" + @"""1.0""" + " encoding=" + @"""Big5""" + " ?>");
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        DataRow dr = dt.Rows[i];
                        sb.Append("<fet-pos-pay-create-req>");
                        sb.Append("<account-id>" + dr["ACCOUNT_ID"].ToString() + "</account-id>");
                        sb.Append("<msisdn>" + dr["MSISDN"].ToString() + "</msisdn>");
                        sb.Append("<amount>" + dr["AMOUNT"].ToString() + "</amount>");
                        sb.Append("<login-id>" + dr["LOGIN_ID"].ToString() + "</login-id>");
                        sb.Append("<password>" + dr["PASSWORD"].ToString() + "</password>");
                        sb.Append("<un-hotline-flag-from-pos>" + dr["UN_HOTLINE_FLAG_FROM_POS"].ToString() + "</un-hotline-flag-from-pos>");
                        sb.Append("<payment-source-id>" + dr["PAYMENT_SOURCE_ID"].ToString() + "</payment-source-id>");
                        sb.Append("<pos-key>" + dr["POS_KEY"].ToString() + "</pos-key>");
                        sb.Append("<store-type>" + dr["STORE_TYPE"].ToString() + "</store-type>");
                        sb.Append("<trans-date>" + dr["TRANS_DATE"].ToString() + "</trans-date>");
                        sb.Append("<store-number>" + dr["STORE_NUMBER"].ToString() + "</store-number>");
                        sb.Append("</fet-pos-pay-create-req>");
                    }
                    sb.Append("</XML>");
                    //資料寫入
                    Console.WriteLine("銷帳檔資料寫入");
                    cLog.Success("銷帳檔資料寫入");
                    //sw.Write(sb.ToString());
                    //sw.Close();
                    UPDATE_FET_BILLING_FILE(oCon, oTX);
                }
                else
                {
                    UPDATE_FET_BILLING_FILE(oCon, oTX);
                    Console.WriteLine("未產生銷帳檔資料，寫入LOG");
                    cLog.Success("未產生銷帳檔資料");
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

        public static string INFET_BILLING_FILE(OracleConnection objConn, OracleTransaction objTX, string UUID)
        {
            string sRet = "";
            try
            {
                OracleCommand oraCmd = new OracleCommand("PK_BILL.INFET_BILLING_FILE");
                oraCmd.Parameters.Add(new OracleParameter("I_BILL_DISPATCH_ID", OracleType.VarChar, 2000)).Value = UUID;
                oraCmd.CommandType = System.Data.CommandType.StoredProcedure;
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

        public static DataTable Query_FET_BILLING_FILE(OracleConnection oCon, OracleTransaction oTX)
        {
            try
            {
                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                sb.Append("UPDATE WEBPOS.FET_BILLING_FILE ");
                sb.Append("   SET SEND_FLAG='T' ");
                sb.Append(" WHERE SEND_FLAG='0'");
                OracleDBUtil.ExecuteSql(oTX, sb.ToString());
                sb.Length = 0;
                sb.Append("SELECT                                                    ");
                sb.Append("ROWID as ITEM_NO, F.FET_BILL_FILE_ID, ");
                sb.Append("F.ACCOUNT_ID, F.MSISDN, ");
                sb.Append("F.AMOUNT, F.LOGIN_ID, F.PASSWORD, ");
                sb.Append("F.UN_HOTLINE_FLAG_FROM_POS, F.PAYMENT_SOURCE_ID, ");
                sb.Append("F.POS_KEY, F.STORE_TYPE, F.TRANS_DATE, F.STORE_NUMBER, ");
                sb.Append("F.FILE_NO, F.SEND_FLAG, F.SEND_DTM, ");
                sb.Append("F.RETRY_FLAG, F.RETRY_COUNT, F.FET_PROCESS_STATUS, ");
                sb.Append("F.ERROR_MESSAGE, F.BATCH_NO, F.BILL_DISPATCH_ID ");
                sb.Append("FROM FET_BILLING_FILE F ");
                sb.Append("where  f.send_flag='T' ");
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

        static void UPDATE_FET_BILLING_FILE(OracleConnection oCon, OracleTransaction oTX)
        {
            try
            {
                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                sb.Append("UPDATE FET_BILLING_FILE ");
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
    }
}
