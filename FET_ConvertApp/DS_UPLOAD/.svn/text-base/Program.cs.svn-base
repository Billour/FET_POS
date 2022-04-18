using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.OracleClient;
using System.IO;
using Advtek.Utility;

using FTP;
using System.Threading;
using System.Web;

namespace DS_UPLOAD
{
    class Program
    {
        static void Main(string[] args)
        {
            //初始化LOG
            Console.WriteLine("DS_UPLOAD");
            Console.WriteLine("初始化LOG");
            ConvertLog cLog = new ConvertLog("DS_UPLOAD");

            //計算此程式已執行數
            string s_no = OracleDBUtil.GetDataSet(OracleDBUtil.GetConnection()
                , "select count(SID) from SCHEDULE_JOB_LOG where TASK_NAME='DS_UPLOAD' and trunc(START_DTM)=trunc(sysdate) "
                ).Tables[0].Rows[0][0].ToString();

            //檔案路徑
            Console.WriteLine("取得暫存檔檔名");
            string first_fileName = "POS" + DateTime.Today.ToString("yyyyMMdd") + s_no.PadLeft(2,'0');
            string fileName = first_fileName + ".csv";
            string trg_file_name = first_fileName + ".trg";

            string sDir=Query_TEMP_PATH();//取得SYS_PARA設定的路徑。
            checkDirectory(sDir);//檢查檔案是否產生，否則新增。
            string sPath = sDir + "\\" + fileName;
            Console.WriteLine("暫存檔檔名與路徑:" + sPath);

            OracleConnection objConn_erp = OracleDBUtil.GetERPPOSConnection();
            OracleTransaction trans_erp = objConn_erp.BeginTransaction();

            try 
            {
                POS2EP_DS_ORDER cPOS = new POS2EP_DS_ORDER();

                Dictionary<string, int> dic = cPOS.getDic();

                Console.WriteLine("建立暫存檔");
                StreamWriter sw = new StreamWriter(sPath, true, Encoding.GetEncoding("big5"));//big5

                StringBuilder sb = new StringBuilder();

                //DataTable dt = cFacade.Query_POS2EP_DS_ORDER();
                Console.WriteLine("查詢QUERY_POS2EP_ORDER_INFO(WEB)");
                DataTable dt = Query_POS2EP_DS_ORDER();                
                
                string last_order_no = "";
                int seq_no = 1;

                for (int i = 0; i < dt.Rows.Count; i++) 
                {
                    DataRow dr = dt.Rows[i];

                    if (dr["POS_ORDER_NUM"].ToString() != last_order_no)
                    {
                        last_order_no = dr["POS_ORDER_NUM"].ToString();
                        seq_no = 1;
                    }
                    else
                    {
                        seq_no++;
                    }

                    foreach (KeyValuePair<string, int> pair in dic) 
                    {
                        string ColName=pair.Key;
                        int length=pair.Value;
                        string s = dr[ColName].ToString().Trim();

                        if (ColName == "POS_ORDER_LINENUM")
                        {
                            sb.Append("\"" + seq_no.ToString().PadLeft(length, '0') + "\",");
                        }
                        else if (ColName == "RETAIL_NUM")
                        {
                            sb.Append("\"" + ("R"+s).ToString().PadRight(length, ' ') + "\",");
                        }
                        else
                        {
                            sb.Append("\"" + StringUtil.Pad(cPOS.getByteString(s, length), " ", length, "R") + "\",");
                        }
                    }

                    sb.Remove(sb.Length - 1, 1).Append("\r\n");//***20110304                    

                    StringBuilder sb2 = new StringBuilder();

                    sb2.AppendLine(
                    @"Insert into DS_ORDERD
                       (STORENO, DS_ORDERNO, PRODNO, SEQNO, 
                       ASSIGNQTY, SUGGESTQTY, VENDORNAME, FORCASTQTY,
                       REALORDQTY, APPROVEQTY, STOREORDQTY,
                       SUBINVENTORY, DS_ORDERDATE, SALE_AVG_QTY, STOCK_QTY, LOCATOR)
                     Values
                       (:STORENO, :ORDERNO, :PRODNO, :SEQNO, 
                        :ASSIGNQTY, :ASSIGNQTY, :VENDERNAME, :REMAINED_QTY,
                        :REALORDQTY, :APPROVEQTY, :STOREORDQTY,
                        :SUBINVENTORY, :DS_ORDERDATE, :ASSIGNQTY, :STOCK_QTY , :LOCATOR)"
                    );

                    string realordaty = "0";
                    string approveqty = "0";
                    string storordqty = "0";

                    //主配單時
                    if (dr["ORDER_TYPE"].ToString() == "2" || dr["ORDER_TYPE"].ToString() == "3")
                    {
                        realordaty = dr["QUANTITY"].ToString();
                    }
                    else
                    {
                        realordaty = dr["QUANTITY"].ToString();
                        approveqty = dr["QUANTITY"].ToString(); 
                        storordqty = dr["QUANTITY"].ToString(); 
                    }

                    sb2.Replace(":ORDERNO", OracleDBUtil.SqlStr(dr["POS_ORDER_NUM"].ToString()));
                    sb2.Replace(":PRODNO", OracleDBUtil.SqlStr(dr["ITEM_NUM"].ToString()));
                    sb2.Replace(":STORENO", OracleDBUtil.SqlStr("R" + dr["RETAIL_NUM"].ToString()));
                    sb2.Replace(":ASSIGNQTY", OracleDBUtil.SqlStr(dr["QUANTITY"].ToString()));
                    sb2.Replace(":VENDERNAME", OracleDBUtil.SqlStr(dr["VENDERNAME"].ToString()));
                    sb2.Replace(":SUBINVENTORY", OracleDBUtil.SqlStr(dr["DESTINATION_SUBINVENTORY"].ToString()));
                    sb2.Replace(":REMAINED_QTY", OracleDBUtil.SqlStr(dr["REMAINED_QTY"].ToString()));//建議訂購量
                    sb2.Replace(":STOCK_QTY", OracleDBUtil.SqlStr(dr["STOCK_QTY"].ToString()));
                    sb2.Replace(":DS_ORDERDATE", OracleDBUtil.SqlStr(dr["ORDER_DATE"].ToString()));
                    sb2.Replace(":SEQNO", OracleDBUtil.SqlStr(seq_no.ToString()));
                    sb2.Replace(":REALORDQTY", realordaty);
                    sb2.Replace(":APPROVEQTY", approveqty);
                    sb2.Replace(":STOREORDQTY", storordqty);
                    sb2.Replace(":LOCATOR", OracleDBUtil.SqlStr(dr["DESTINATION_LOCATOR"].ToString()));

                    OracleDBUtil.ExecuteSql(trans_erp, sb2.ToString());
                }

                //資料寫入
                Console.WriteLine("暫存檔資料寫入");
                sw.Write(sb.ToString());
                sw.Close();

                //再產生一個.trg的空檔
                string trg_file_path = sDir + "\\" + trg_file_name;
                StreamWriter sw2 = new StreamWriter(
                    trg_file_path, 
                    true, 
                    Encoding.GetEncoding("big5"));//big5

                sw2.Close();

                //上傳
                //DataTable dtFtpInfo = cFacade.Query_FTP_INFO();
                Console.WriteLine("查詢 FTP SERVER 設定");
                DataTable dtFtpInfo = Query_FTP_INFO();
                if (dtFtpInfo.Rows.Count < 1)
                    throw new Exception("SYS_PARA未設定FTP連線資訊");
                string F_HOST = dtFtpInfo.Rows[0]["F_HOST"].ToString();
                string F_USER = dtFtpInfo.Rows[0]["F_USER"].ToString();
                string F_PASSWORD = dtFtpInfo.Rows[0]["F_PASSWORD"].ToString();
                string F_DIR = dtFtpInfo.Rows[0]["F_DIR"].ToString()  ;
                FTPclient ftp = new FTPclient(F_HOST, F_USER, F_PASSWORD);
                bool bStatus = ftp.Upload(sPath, F_DIR + fileName);
                bool bStatus2 = ftp.Upload(trg_file_path, F_DIR + trg_file_name);

                if (!bStatus || !bStatus2)
                {
                    throw new Exception(fileName + "上傳失敗");
                }

                //更新狀態
                //cFacade.UPDATE_POS2EP_DS_ORDER();
                Console.WriteLine("上傳成功，更新 POS2EP_DS_ORDER(WEB) 狀態");
                UPDATE_POS2EP_DS_ORDER();
                Console.WriteLine("執行結束，");
                Console.WriteLine("執行結束，寫入LOG");

                trans_erp.Commit();

                cLog.Success("DS_UPLOAD:" + fileName + "上傳成功。");
                Thread.Sleep(3000);

            }
            catch (Exception ex) 
            {
                trans_erp.Rollback();

                Console.WriteLine("例外產生");
                cLog.Fail(ex.Message);
                Console.WriteLine(ex.Message);
                Thread.Sleep(3000);
                //throw ex; 
            }
            finally
            {
                if (objConn_erp.State == ConnectionState.Open) objConn_erp.Close();
                objConn_erp.Dispose();
                OracleConnection.ClearAllPools();
            }
        }

        public static DataTable Query_POS2EP_DS_ORDER()
        {
            OracleConnection oCon = null;
            OracleTransaction objTX = null;
            try
            {
                oCon = OracleDBUtil.GetConnection();
                objTX = oCon.BeginTransaction();
                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                sb.Append("UPDATE POS2EP_DS_ORDER ");
                sb.Append("   SET STATUS='T' ");
                sb.Append(" WHERE STATUS='0' ");
                OracleDBUtil.ExecuteSql(objTX, sb.ToString());

                sb.Length = 0;
                sb.Append("SELECT * ");
                sb.Append("FROM QUERY_POS2EP_ORDER_INFO  ");
                //sb.Append("WHERE STATUS='T' AND PRODNO NOT IN (SELECT PRODNO FROM PRODUCT)");

                DataTable dt = OracleDBUtil.GetDataSet(objTX, sb.ToString()).Tables[0];
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
                if (oCon.State == ConnectionState.Open) oCon.Close();
                oCon.Dispose();
                OracleConnection.ClearAllPools();
            }
        }


        public static DataTable Query_FTP_INFO()
        {
            OracleConnection oCon = null;
            OracleTransaction objTX = null;
            try
            {
                oCon = OracleDBUtil.GetConnection();
                objTX = oCon.BeginTransaction();
                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                sb.Length = 0;
                sb.Append("SELECT ");
                sb.Append("( SELECT PARA_VALUE FROM SYS_PARA WHERE PARA_KEY='FTP_HOST' ) AS F_HOST,");
                sb.Append("( select para_value from sys_para where para_key='FTP_USER' ) AS F_USER,");
                sb.Append("( select para_value from sys_para where para_key='FTP_DIR' ) AS F_DIR,");
                sb.Append("( SELECT PARA_VALUE FROM SYS_PARA WHERE PARA_KEY='FTP_PASSWORD' ) AS F_PASSWORD ");
                sb.Append("FROM DUAL");

                DataTable dt = OracleDBUtil.GetDataSet(objTX, sb.ToString()).Tables[0];
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
                if (oCon.State == ConnectionState.Open) oCon.Close();
                oCon.Dispose();
                OracleConnection.ClearAllPools();
            }
        }

        public static void UPDATE_POS2EP_DS_ORDER()
        {
            OracleConnection oCon = null;
            OracleTransaction objTX = null;
            try
            {
                oCon = OracleDBUtil.GetConnection();
                objTX = oCon.BeginTransaction();
                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                sb.Append("UPDATE POS2EP_DS_ORDER ");
                sb.Append("   SET STATUS='1' ");
                sb.Append(" WHERE STATUS='T' ");
                OracleDBUtil.ExecuteSql(objTX, sb.ToString());
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
                if (oCon.State == ConnectionState.Open) oCon.Close();
                oCon.Dispose();
                OracleConnection.ClearAllPools();
            }
        }

        static string Query_TEMP_PATH()
        {
            OracleConnection oCon = null;
            string sRet = "";
            try
            {
                oCon = OracleDBUtil.GetConnection();
                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                sb.Length = 0;
                sb.Append("SELECT PARA_VALUE FROM SYS_PARA WHERE PARA_KEY='DS_UPLOAD_TEMP_DIR' ");

                DataTable dt = OracleDBUtil.GetDataSet(oCon, sb.ToString()).Tables[0];
                if (dt != null && dt.Rows.Count > 0) {
                    sRet = dt.Rows[0][0].ToString();
                }
                return sRet;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (oCon.State == ConnectionState.Open) oCon.Close();
                oCon.Dispose();
                OracleConnection.ClearAllPools();
            }
        }

        static void checkDirectory(string sPath)
        {
            if (!string.IsNullOrEmpty(sPath))
            {
                //sPath = sPath.Replace('\\', '/');
                string[] arrDir = sPath.Split('/');
                string sCheckPath = "";
                string sRealPath = "";
                for (int i = 0; i < arrDir.Length; i++)
                {
                    sCheckPath += arrDir[i] + "/";
                    //sRealPath = HttpContext.Current.Server.MapPath(sCheckPath);
                    sRealPath = System.IO.Path.GetFullPath(sCheckPath);
                    if (!Directory.Exists(sRealPath)) Directory.CreateDirectory(sRealPath);
                }
            }
        }
    }
}
