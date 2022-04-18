using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Advtek.Utility;
using System.Data;
using System.Data.OracleClient;
using System.IO;

namespace FET_BILL_TXT_FILE
{
    class Program
    {
        const int LOG_FILE_NAME_LENGTH = 19;

        static void Main(string[] args)
        {
            //初始化LOG
            Console.WriteLine("1.FET_BILL_TXT_FILE開始");
            Console.WriteLine("2.初始化LOG");
            ConvertLog con_log = new ConvertLog("FET_BILL_TXT_FILE");

            OracleConnection objConn = null;
            OracleTransaction objTrans = null;

            try
            {
                Console.WriteLine("3.建立連線");
                objConn = OracleDBUtil.GetConnection();
                objTrans = objConn.BeginTransaction();

                string outSTATUS;
                string outMESSAGE;

                OracleDBUtil.ExecuteSql_SP(
                    objTrans,
                    "PK_BILL.INSERT_FET_TXT_FILE_DATA",
                    out outSTATUS,
                    out outMESSAGE
                    );

                if (outSTATUS != "000")
                {
                    throw new Exception(outMESSAGE);
                }
                else
                {
                    OracleDBUtil.ExecuteSql(
                        objTrans,
                        "update FET_TO_BILLING_FILE set FTP_STATUS='T' where nvl(FTP_STATUS, '0')='0' "
                    );

                    DataTable sourceDt = OracleDBUtil.GetDataSet(
                        objTrans,
                        "Select * FROM FET_TO_BILLING_FILE where FTP_STATUS='T' ORDER BY TRANS_DATE "
                    ).Tables[0];

                    if (sourceDt.Rows.Count > 0)
                    {
                        //取得路徑
                        //string file_path = OracleDBUtil.GetDataSet(
                        //    objTrans,
                        //    "Select PARA_VALUE  from SYS_PARA where PARA_KEY = 'IIS_URL' "
                        //    ).Tables[0].Rows[0][0].ToString() + "\\Bill_FILES\\FET\\";
                        string file_path = OracleDBUtil.GetDataSet(
                            objTrans,
                            "Select PARA_VALUE  from SYS_PARA where PARA_KEY = 'IIS_URL_FET' "
                            ).Tables[0].Rows[0][0].ToString();//***20110323

                        //***20110316 檢查目錄是否存在
                        if (!Directory.Exists(file_path)) Directory.CreateDirectory(file_path);

                        //組文字檔
                        foreach (DataRow dr in sourceDt.Rows)
                        {
                            string bill_file_name = dr["FILE_NAME"].ToString();

                            StreamWriter writer = new StreamWriter(file_path + "\\" + bill_file_name);

                            //head
                            writer.WriteLine("最後變更時間             門號           正負數           金額  店號      帳號           STATUS    ");
                            writer.WriteLine("-------------------------------------------------------------------------------------------------------");

                            DataTable sourceDt_DETAIL = OracleDBUtil.GetDataSet(
                                    objTrans
                                    , "Select * FROM FET_TO_BILLING_FILE_DETAIL where TO_BILLINGFILE_ID= "
                                    + OracleDBUtil.SqlStr(dr["TO_BILLINGFILE_ID"].ToString())
                                    + " ORDER BY STORE_NO "
                                ).Tables[0];

                            //detail
                            foreach (DataRow dr_DETAIL in sourceDt_DETAIL.Rows)
                            {
                                writer.WriteLine(
                                    string.Format("{0}     {1}     {2}      {3}  {4}  {5}     {6}"
                                    , Convert.ToDateTime(dr_DETAIL["LAST_UPDATE_DTM"]).ToString("yyyyMMdd-HH:mm:ss").PadRight(20, ' ')
                                    , dr_DETAIL["MSISDN"].ToString().PadRight(10, ' ')
                                    , (dr_DETAIL["TX_TYPE"].ToString() == "1") ? "+     " : "-     "
                                    , dr_DETAIL["AMOUNT"].ToString().PadLeft(9, ' ')
                                    , dr_DETAIL["STORE_NO"].ToString().PadRight(8, ' ')
                                    , dr_DETAIL["BORCODE1"].ToString().PadRight(10, ' ')
                                    , dr_DETAIL["BILLING_STATUS"].ToString()
                                    ));
                            }

                            //trailer
                            writer.WriteLine(" ");
                            writer.WriteLine("總筆數    總金額       成功筆數   成功金額   Retry失敗筆數   Retry失敗金額  上傳失敗筆數   上傳失敗金額");
                            writer.WriteLine("-------------------------------------------------------------------------------------------------------");

                            writer.WriteLine(
                                    string.Format("{0}{1}{2}{3}{4}{5}{6}{7}"
                                    , dr["TOTAL_RECORD"].ToString().PadRight(10, ' ')
                                    , dr["TOTAL_AMOUNT"].ToString().PadRight(13, ' ')
                                    , dr["SEND_OK_RECORD"].ToString().PadRight(11, ' ')
                                    , dr["SEND_OK_AMOUNT"].ToString().PadRight(11, ' ')
                                    , dr["RETRY_FAIL_RECORD"].ToString().PadRight(16, ' ')
                                    , dr["RETRY_FAIL_AMOUNT"].ToString().PadRight(15, ' ')
                                    , dr["SEND_FAIL_RECORD"].ToString().PadRight(15, ' ')
                                    , dr["SEND_FAIL_AMOUNT"].ToString().PadRight(13, ' ')
                                    ));

                            writer.Close();
                        }

                        //組LOG檔
                        DataTable trans_date_Dt = OracleDBUtil.GetDataSet(
                                objTrans,
                                "Select distinct TRANS_DATE FROM FET_TO_BILLING_FILE where FTP_STATUS='T'"
                            ).Tables[0];

                        foreach (DataRow trans_date_Dr in trans_date_Dt.Rows)
                        {
                            string this_trans_date = trans_date_Dr["TRANS_DATE"].ToString();

                            string log_file_name = "POS" + this_trans_date.Replace("/", "") + "-" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".log";

                            StreamWriter writer_log = new StreamWriter(file_path + "\\" + log_file_name);

                            writer_log.WriteLine("Processing Date = " + this_trans_date.Replace("/", ""));

                            DataTable m_Dt = OracleDBUtil.GetDataSet(
                                    objTrans,
                                    "Select * FROM FET_TO_BILLING_FILE where FTP_STATUS='T' and trans_date= '" + this_trans_date + "'"
                                ).Tables[0];

                            //foreach 
                            foreach (DataRow m_dr in m_Dt.Rows)
                            {
                                string temp = m_dr["PAYMENT_SOURCE_NAME"].ToString();

                                if (temp == "HG")
                                {
                                    temp = "HappyGo";
                                }

                                writer_log.WriteLine(("Output " + temp + " File").PadRight(LOG_FILE_NAME_LENGTH, ' ') + " : " + m_dr["FILE_NAME"].ToString());
                            }

                            writer_log.WriteLine("Output Discount File".PadRight(20, ' ') + " : ");
                            writer_log.WriteLine("Log File".PadRight(LOG_FILE_NAME_LENGTH, ' ') + " : " + log_file_name);
                            writer_log.WriteLine(" ");
                            writer_log.WriteLine("STORENO               CASH                     CREDIT         HAPPY GO                 Discount                 ");
                            writer_log.WriteLine("----------------------------------------------------------------------------------------------------------------");

                            DataTable sourceDt_total = OracleDBUtil.GetDataSet(
                                        objTrans,
                                        string.Format(
                                        @"SELECT D.STORE_NO AS STORE_NO 
                                        ,COUNT( CASE M.PAYMENT_SOURCE_NAME WHEN 'Cash' THEN M.FILE_NAME END ) AS CashA
                                        ,NVL(SUM( CASE M.PAYMENT_SOURCE_NAME WHEN 'Cash' THEN D.AMOUNT END ), 0) AS CashC
                                        ,COUNT( CASE M.PAYMENT_SOURCE_NAME WHEN 'Credit' THEN M.FILE_NAME END ) AS CreditA
                                        ,NVL(SUM( CASE M.PAYMENT_SOURCE_NAME WHEN 'Credit' THEN D.AMOUNT END ), 0) AS CreditC
                                        ,COUNT( CASE M.PAYMENT_SOURCE_NAME WHEN 'HG' THEN M.FILE_NAME END ) AS HappyGoA
                                        ,NVL(SUM( CASE M.PAYMENT_SOURCE_NAME WHEN 'HG' THEN D.AMOUNT END ), 0) AS HappyGoC
                                        ,0 AS DiscountA
                                        ,0 AS DiscountC
                                        FROM FET_TO_BILLING_FILE_DETAIL D, FET_TO_BILLING_FILE M
                                        WHERE D.TO_BILLINGFILE_ID=M.TO_BILLINGFILE_ID
                                        AND M.TRANS_DATE='{0}'
                                        AND M.FTP_STATUS='T'
                                        GROUP BY D.STORE_NO
                                        ORDER BY D.STORE_NO"
                                        , this_trans_date
                                        )
                                        ).Tables[0];

                            int CashA_total = 0;
                            int CashC_total = 0;
                            int CreditA_total = 0;
                            int CreditC_total = 0;
                            int HappyGoA_total = 0;
                            int HappyGoC_total = 0;
                            int DiscountA_total = 0;
                            int DiscountC_total = 0;

                            foreach (DataRow dr_total in sourceDt_total.Rows)
                            {
                                writer_log.WriteLine(
                                        string.Format("{0}{1}{2}{3}{4}{5}{6}{7}{8}"
                                        , dr_total["STORE_NO"].ToString().PadRight(12, ' ')
                                        , dr_total["CashA"].ToString().PadRight(10, ' ')
                                        , dr_total["CashC"].ToString().PadRight(15, ' ')
                                        , dr_total["CreditA"].ToString().PadRight(10, ' ')
                                        , dr_total["CreditC"].ToString().PadRight(15, ' ')
                                        , dr_total["HappyGoA"].ToString().PadRight(10, ' ')
                                        , dr_total["HappyGoC"].ToString().PadRight(15, ' ')
                                        , dr_total["DiscountA"].ToString().PadRight(10, ' ')
                                        , dr_total["DiscountC"].ToString().PadRight(15, ' ')
                                        ));

                                CashA_total += Convert.ToInt32(dr_total["CashA"]);
                                CashC_total += Convert.ToInt32(dr_total["CashC"]);
                                CreditA_total += Convert.ToInt32(dr_total["CreditA"]);
                                CreditC_total += Convert.ToInt32(dr_total["CreditC"]);
                                HappyGoA_total += Convert.ToInt32(dr_total["HappyGoA"]);
                                HappyGoC_total += Convert.ToInt32(dr_total["HappyGoC"]);
                                DiscountA_total += Convert.ToInt32(dr_total["DiscountA"]);
                                DiscountC_total += Convert.ToInt32(dr_total["DiscountC"]);
                            }

                            writer_log.WriteLine("----------------------------------------------------------------------------------------------------------------");

                            writer_log.WriteLine(
                                        string.Format("Total:      {0}{1}{2}{3}{4}{5}{6}{7}"
                                        , CashA_total.ToString().PadRight(10, ' ')
                                        , CashC_total.ToString().PadRight(15, ' ')
                                        , CreditA_total.ToString().PadRight(10, ' ')
                                        , CreditC_total.ToString().PadRight(15, ' ')
                                        , HappyGoA_total.ToString().PadRight(10, ' ')
                                        , HappyGoC_total.ToString().PadRight(15, ' ')
                                        , DiscountA_total.ToString().PadRight(10, ' ')
                                        , DiscountC_total.ToString().PadRight(15, ' ')
                                        ));

                            writer_log.Close();
                        }
                    }

                    //上傳FTP

                    //上傳FTP完成
                    OracleDBUtil.ExecuteSql(
                        objTrans,
                        "update FET_TO_BILLING_FILE set FTP_STATUS='1' where FTP_STATUS='T' "
                    );

                    objTrans.Commit();

                    //成功資訊
                    Console.WriteLine("排程執行結束，寫入LOG");
                    con_log.Success(outMESSAGE);
                }

            }
            catch (Exception ex)
            {
                objTrans.Rollback();

                //失敗資訊
                con_log.Fail(ex.Message);
                Console.WriteLine(ex.Message);
            }
            finally
            {
                objTrans.Dispose();
                if (objConn.State == ConnectionState.Open) objConn.Close();
                objConn.Dispose();
                OracleConnection.ClearAllPools();
            }

        }
    }
}
