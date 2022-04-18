using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Advtek.Utility;
using System.Data;
using System.Data.OracleClient;
using System.IO;
namespace KGT_BILLING
{
    class Program
    {
        static void Main(string[] args)
        {
            List<KGT_BILLING> kgList = new List<KGT_BILLING>();
            OracleConnection conn = OracleDBUtil.GetConnection();
            OracleTransaction trans = conn.BeginTransaction();
            //讀取支付類型
            Dictionary<string, string> dir = KGT_BILLING.getPAY_MODE();

            //2.初始化LOG
            ConvertLog log = new ConvertLog("KGT_BILLING");
            try
            {
                Dictionary<string, string> fname = new Dictionary<string, string>();
                KGT_BILLING.Init(log, trans);
                KGT_BILLING kgt = new KGT_BILLING(conn, trans, log);
                //***20110317
                string sqlstr = @"SELECT to_char(SH.TRADE_DATE,'YYYY/MM/DD') as TRADE_DATE
                                    FROM BILL_DISPATCH BD,SALE_HEAD SH,SALE_DETAIL SD 
                                   WHERE BD.POSUUID_MASTER = SH.POSUUID_MASTER AND BD.SALE_DETAIL_ID = SD.ID AND BD.BILL_TYPE='2' AND BD.STATUS='T' 
                                   GROUP BY to_char(SH.TRADE_DATE,'YYYY/MM/DD') order by TRADE_DATE";
                DataTable dt = OracleDBUtil.GetDataSet(trans, sqlstr).Tables[0];
                foreach (DataRow dr in dt.Rows)
                {
                    string sDate = dr["TRADE_DATE"].ToString();
                    foreach (KeyValuePair<string, string> item in dir)
                    {

                        kgt.Load(item.Value, item.Key, sDate);
                        //header.Exchange_File_Name
                        fname.Add(item.Value, kgt.header.Exchange_File_Name);
                        kgt.Insert();
                        kgt.ExportFile();
                        if (item.Value == "7")
                        {
                            kgt.ExportLogFile(fname);
                        }

                        //kgList.Add(kgt);
                    }
                    kgt.RunFTP();
                    kgt.Update();
                    fname.Clear();
                }



                #region 程式備份 20110317
                //foreach (KeyValuePair<string, string> item in dir)
                //{
                //    KGT_BILLING kgt = new KGT_BILLING(conn, trans, log);
                //    kgt.Load(item.Value, item.Key);
                //    //header.Exchange_File_Name
                //    fname.Add(item.Value, kgt.header.Exchange_File_Name);
                //    kgt.Insert();
                //    kgt.ExportFile();
                //    if (item.Value == "7")
                //    {
                //        kgt.ExportLogFile(fname);
                //    }
                //    kgt.RunFTP();
                //    kgt.Update();
                //    //kgList.Add(kgt);
                //}
                #endregion

                //產生實體繳款銷帳檔案
                //foreach (KGT_BILLING kg in kgList)
                //{

                //}

                //10.Call KGT FTP SERVER

                trans.Commit();
                log.Success("KGT_BILLING 處理成功");
            }
            catch (Exception ex)
            {

                trans.Rollback();
                Console.WriteLine(ex.Message);

                log.Fail(ex.Message);

            }
            finally
            {
                if (conn.State == ConnectionState.Open) conn.Close();
                conn.Dispose();
                OracleConnection.ClearAllPools();
                conn = null;
            }
        }
    }

    public class KGT_BILLING
    {
        public Header header;
        public List<Data> dataList;
        public Trailer trailer;
        ConvertLog cLog;
        string file_path = "";
        OracleConnection conn;
        OracleTransaction trans;
        ConvertLog log;

        public KGT_BILLING(OracleConnection conn, OracleTransaction trans, ConvertLog log)
        {
            this.conn = conn;
            this.trans = trans;
            this.log = log;

            //this.file_path = Path.Combine(getFile_path(), "Bill_FILES\\KGT\\");
            this.file_path = getFile_path(); //***20110323
            if (!Directory.Exists(file_path)) Directory.CreateDirectory(file_path);
        }

        public string getFile_path()
        {
            string path = getIISUrl();

            return path;
        }

        public static string getIISUrl()
        {
            OracleConnection conn = null;
            //string strSQL = "Select PARA_VALUE  from SYS_PARA where PARA_KEY = 'IIS_URL'";
            string strSQL = "Select PARA_VALUE  from SYS_PARA where PARA_KEY = 'IIS_URL_KGT'";//***20110323

            string strResult = "";

            try
            {
                conn = OracleDBUtil.GetConnection();
                OracleCommand cmd = new OracleCommand(strSQL, conn);
                strResult = cmd.ExecuteScalar() == null ? "" : cmd.ExecuteScalar().ToString();
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                if (conn.State == ConnectionState.Open) conn.Close();
                conn.Dispose();
                OracleConnection.ClearAllPools();
                conn = null;

            }
            return strResult;
        }

        public static void Init(ConvertLog log, OracleTransaction trans)
        {
            OracleConnection conn = trans.Connection;
            //***20110317 + POSUUID_MASTER IN (SELECT POSUUID_MASTER FROM SALE_HEAD WHERE TRUNC(TRADE_DATE)<=TRUNC(SYSDATE-1) ) 取昨天之前的資料
            //string sqlstr = "update BILL_DISPATCH set STATUS='T' where bill_type='2' and STATUS='0'";
            string sqlstr = "update BILL_DISPATCH set STATUS='T' where bill_type='2' and STATUS='0' AND POSUUID_MASTER IN (SELECT POSUUID_MASTER FROM SALE_HEAD WHERE TRUNC(TRADE_DATE)<=TRUNC(SYSDATE-1) )";
            OracleCommand cmd = new OracleCommand(sqlstr, conn, trans);
            try
            {
                cmd.ExecuteNonQuery();
                log.Success("Update BILL_DISPATCH STATUS '0' -> 'T' Success,");
            }
            catch (Exception ex)
            {
                log.Fail(ex.Message);
            }
        }

        public void Load(string PAY_MODE_ID, string PAY_MODE_NAME, string sTRADE_DATE)
        {

            header = new Header();
            header.PAY_MODE_ID = PAY_MODE_ID;
            header.PAY_MODE_NAME = PAY_MODE_NAME;
            header.TRADE_DATE = sTRADE_DATE;//***
            header.FILE_NAME = getFileName();
            header.Exchange_File_Name = getExchange_File_Name();
            dataList = Data.getDataList(PAY_MODE_ID, sTRADE_DATE, this.conn, this.trans);//***
            trailer = new Trailer();
            trailer.TOTAL_TRANS_COUNT = dataList.Count.ToString();
            trailer.SUM_OF_TRANS = SUM().ToString();
            trailer.CONTROL_DIGIT = getDigit().ToString();
        }

        public decimal getDigit()
        {
            int digit = 0;
            foreach (Data data in dataList)
            {
                digit += Convert.ToInt32(data.CONTROL_DIGIT);
            }

            digit = digit % 11;
            digit = digit == 10 ? 0 : digit;
            return digit;
        }

        public decimal SUM()
        {
            decimal sum = 0;
            foreach (Data data in dataList)
            {
                sum += Convert.ToDecimal(data.AMOUNT);
            }
            return sum;
        }



        #region Update
        public void Update()
        {

            string sqlstr = "update KGT_BILLING_M set SEND_FLAG = '1',SEND_DTM = SYSDATE where SEND_FLAG = '0' and FILE_NAME = :FILE_NAME and  BATCH_N0 = :BATCH_N0 ";
            OracleCommand cmd = new OracleCommand(sqlstr, conn, trans);
            cmd.Parameters.Add(":FILE_NAME", OracleType.NVarChar, 12).Value = this.header.FILE_NAME;
            cmd.Parameters.Add(":BATCH_N0", OracleType.NVarChar, 10).Value = this.header.BATCH_NO;
            try
            {
                cmd.ExecuteNonQuery();
                //sqlstr = "update BILL_DISPATCH set status = '1' where status='T' and bill_type='2' and PAY_MODE_ID = :PAY_MODE_ID";
                sqlstr = @"update BILL_DISPATCH T1 set T1.status = '1' where T1.status='T' and T1.bill_type='2' 
                              AND EXISTS ( SELECT 'X' FROM SALE_HEAD T2 WHERE TRUNC(T2.TRADE_DATE)=TRUNC( TO_DATE('" + this.header.TRADE_DATE + "','YYYY/MM/DD') ) AND T2.POSUUID_MASTER=T1.POSUUID_MASTER)";
                cmd = new OracleCommand(sqlstr, conn, trans);
                //cmd.Parameters.Add(":PAY_MODE_ID", OracleType.NVarChar, 20).Value = header.PAY_MODE_ID;
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        #endregion

        #region 產生實體檔案
        public void ExportFile()
        {

            StreamWriter writer = new StreamWriter(this.file_path + "\\" + this.header.Exchange_File_Name);

            //Header
            //string File_name = header.FILE_NAME.PadLeft(12, ' ');
            string File_name = getFileName2().PadLeft(12, ' ');
            string Day_of_file = header.DATE_OF_FILE;
            string Time = header.Time;
            string Batch_no = header.BATCH_NO.PadRight(10, '0');
            string issuer = header.ISSUER.PadLeft(10, ' ');
            string Band_code = header.BANK_CODE.PadRight(10, ' ');
            string Spare1 = header.Spare1.PadLeft(20, ' ');
            string Spare2 = header.Spare2.PadLeft(20, ' ');

            string headerString = string.Format("{0},{1},{2},{3},{4},{5},{6},{7},", File_name, Day_of_file, Time, Batch_no, issuer, Band_code, Spare1, Spare2);

            writer.WriteLine(headerString);
            //Data
            foreach (Data data in dataList)
            {
                string DOCUMENT_NO = data.DOCUMENT_NO.PadLeft(8, '0');
                string USER_BAR_CODE = data.USER_BAR_CODE.PadLeft(14, '0');
                string AMOUNT = Convert.ToDouble(data.AMOUNT).ToString("000000000000.00");
                string OLD_STORENO = data.OLD_STORENO.PadRight(8, ' ');
                string DATE_OF_TRANSACTION = data.TRANS_DATE;
                string Spare3 = data.Spare3.PadLeft(20, ' ');
                string Spare4 = data.Spare4.PadLeft(20, ' ');
                string CONTROL_DIGIT = data.CONTROL_DIGIT;
                string dataString = string.Format("{0},{1},{2},{3},{4},{5},{6},{7},", DOCUMENT_NO, USER_BAR_CODE, AMOUNT, OLD_STORENO, DATE_OF_TRANSACTION, Spare3, Spare4, CONTROL_DIGIT);
                writer.WriteLine(dataString);
            }

            //Trailer
            string TOTAL_NO_OF_TRANSACTION = dataList.Count.ToString("0000000000");
            string TOTAL_AMOUNT_OF_TRANSACTION = Convert.ToDouble(trailer.SUM_OF_TRANS).ToString("000000000000.00");
            string TOTAL_CONTROL_DIGIT = trailer.CONTROL_DIGIT;
            //string TrailerString = string.Format("{0},{1},{2}", TOTAL_NO_OF_TRANSACTION, TOTAL_AMOUNT_OF_TRANSACTION, TOTAL_CONTROL_DIGIT);
            string TrailerString = string.Format("{0},{1},{2},", TOTAL_NO_OF_TRANSACTION, TOTAL_AMOUNT_OF_TRANSACTION, TOTAL_CONTROL_DIGIT);
            writer.WriteLine(TrailerString);

            writer.Close();
        }

        #region 程式備份 20110317
        //public void ExportFile()
        //{

        //    StreamWriter writer = new StreamWriter(this.file_path + "\\" + this.header.Exchange_File_Name);

        //    //Header
        //    string File_name = header.FILE_NAME.PadLeft(12, ' ');
        //    string Day_of_file = header.DATE_OF_FILE;
        //    string Time = header.Time;
        //    string Batch_no = header.BATCH_NO.PadRight(10, '0');
        //    string issuer = header.ISSUER.PadLeft(10, ' ');
        //    string Band_code = header.BANK_CODE.PadRight(10, ' ');
        //    string Spare1 = header.Spare1.PadLeft(20, ' ');
        //    string Spare2 = header.Spare2.PadLeft(20, ' ');

        //    string headerString = string.Format("{0},{1},{2},{3},{4},{5},{6},{7},", File_name, Day_of_file, Time, Batch_no, issuer, Band_code, Spare1, Spare2);

        //    writer.WriteLine(headerString);
        //    //Data
        //    foreach (Data data in dataList)
        //    {
        //        string DOCUMENT_NO = data.DOCUMENT_NO.PadLeft(8, '0');
        //        string USER_BAR_CODE = data.USER_BAR_CODE.PadLeft(14, '0');
        //        string AMOUNT = Convert.ToDouble(data.AMOUNT).ToString("000000000000.00");
        //        string OLD_STORENO = data.OLD_STORENO.PadRight(8, ' ');
        //        string DATE_OF_TRANSACTION = data.TRANS_DATE;
        //        string Spare3 = data.Spare3.PadLeft(20, ' ');
        //        string Spare4 = data.Spare4.PadLeft(20, ' ');
        //        string CONTROL_DIGIT = data.CONTROL_DIGIT;
        //        string dataString = string.Format("{0},{1},{2},{3},{4},{5},{6},{7},", DOCUMENT_NO, USER_BAR_CODE, AMOUNT, OLD_STORENO, DATE_OF_TRANSACTION, Spare3, Spare4, CONTROL_DIGIT);
        //        writer.WriteLine(dataString);
        //    }

        //    //Trailer
        //    string TOTAL_NO_OF_TRANSACTION = dataList.Count.ToString("0000000000");
        //    string TOTAL_AMOUNT_OF_TRANSACTION = Convert.ToDouble(trailer.SUM_OF_TRANS).ToString("000000000000.00");
        //    string TOTAL_CONTROL_DIGIT = trailer.CONTROL_DIGIT;
        //    //string TrailerString = string.Format("{0},{1},{2}", TOTAL_NO_OF_TRANSACTION, TOTAL_AMOUNT_OF_TRANSACTION, TOTAL_CONTROL_DIGIT);
        //    string TrailerString = string.Format("{0},{1},{2},", TOTAL_NO_OF_TRANSACTION, TOTAL_AMOUNT_OF_TRANSACTION, TOTAL_CONTROL_DIGIT);
        //    writer.WriteLine(TrailerString);

        //    writer.Close();
        //}
        #endregion

        #endregion

        #region 產生實體LOG檔案
        public void ExportLogFile(Dictionary<string, string> Dfname)
        {
            Dictionary<string, string> fname = Dfname;
            //string file_name = "KGP" + DateTime.Now.ToString("yyMMdd") + "-" + DateTime.Now.ToString("yyMMddhhmmss") + "POS.log";
            string file_name = "KGP" + this.header.TRADE_DATE.Replace("/", "").Substring(2) + "-" + DateTime.Now.ToString("yyMMddhhmmss") + "POS.log";
            StreamWriter writer = new StreamWriter(this.file_path + "\\" + file_name);
            string headerString = "Processing Date = " + DateTime.Now.ToString("yyyyMMdd");
            writer.WriteLine(headerString);
            foreach (KeyValuePair<string, string> item in fname)
            {
                if (item.Key == "1")
                {
                    headerString = "Output Cash  File   : " + item.Value;
                }
                else if (item.Key == "2")
                {
                    headerString = "Output Credit  File : " + item.Value;
                }
                else if (item.Key == "7")
                {
                    headerString = "Output HappyGo File : " + item.Value;
                }
                else { headerString = ""; }
                writer.WriteLine(headerString);
            }
            headerString = "Output Discount File : ";
            writer.WriteLine(headerString);
            headerString = "Log File           : " + file_name;
            writer.WriteLine(headerString);
            headerString = "NEW_STORENO    OLD_STORENO              CASH                     CREDIT                   HappyGo                  Discount       ";
            writer.WriteLine(headerString);
            headerString = "----------------------------------------------------------------------------------------------------------------------------------";
            writer.WriteLine(headerString);

            //string sqlstr = "select 'R'||STORE_NO STORE_NO,NVL((SELECT OLD_STORENO FROM STORENO_MAPPING WHERE NEW_STORENO='R'||STORE_NO),'') OLD_STORENO ";
            string sqlstr = "select decode(length(STORE_NO),4,'R'||STORE_NO,STORE_NO) STORE_NO,NVL((SELECT OLD_STORENO FROM STORENO_MAPPING WHERE NEW_STORENO=decode(length(STORE_NO),4,'R'||STORE_NO,STORE_NO)  ),'') OLD_STORENO ";
            sqlstr += ",SUM(CASE  PAY_MODE_ID WHEN '1' THEN AMOUNT ELSE 0 END) CashA ";
            //sqlstr += ",COUNT(CASE  PAY_MODE_ID WHEN '1' THEN 1 ELSE 0 END) CashC ";
            sqlstr += ",COUNT(CASE  PAY_MODE_ID WHEN '1' THEN 1 ELSE NULL END) CashC ";
            sqlstr += ",SUM(CASE  WHEN PAY_MODE_ID in ('2','3') THEN AMOUNT ELSE 0 END) CreditA ";
            //sqlstr += ",COUNT(CASE  PAY_MODE_ID WHEN '2' THEN 1 ELSE 0 END) CreditC ";
            sqlstr += ",COUNT(CASE WHEN PAY_MODE_ID in ('2','3') THEN 1 ELSE NULL END) CreditC ";
            sqlstr += ",SUM(CASE  PAY_MODE_ID WHEN '7' THEN AMOUNT ELSE 0 END) HappyGoA ";
            //sqlstr += ",COUNT(CASE  PAY_MODE_ID WHEN '7' THEN 1 ELSE 0 END) HappyGoC ";
            sqlstr += ",COUNT(CASE  PAY_MODE_ID WHEN '7' THEN 1 ELSE NULL END) HappyGoC ";
            sqlstr += " from BILL_DISPATCH bd,sale_head sh,sale_detail sd ";
            sqlstr += " where bd.POSUUID_MASTER = sh.posuuid_master and bd.SALE_DETAIL_ID = sd.id and bd.bill_type='2'  ";
            sqlstr += "and bd.status='T' and trunc(sh.trade_date)=trunc(to_date(" + OracleDBUtil.SqlStr(this.header.TRADE_DATE) + ",'YYYY/MM/DD')) GROUP BY STORE_NO";
            //OracleConnection conn2 = OracleDBUtil.GetConnection();//***20110316
            DataTable dt = OracleDBUtil.GetDataSet(trans, sqlstr).Tables[0];//***20110316

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                string NSTORE = dt.Rows[i]["STORE_NO"].ToString().PadRight(15, ' ');
                string OSTORE = dt.Rows[i]["OLD_STORENO"].ToString().PadRight(15, ' ');
                string CashA = dt.Rows[i]["CashA"].ToString().PadRight(15, ' ');
                string CashC = dt.Rows[i]["CashC"].ToString().PadRight(10, ' ');
                //string CreditA = dt.Rows[i]["CreditC"].ToString().PadRight(10, ' ');
                string CreditA = dt.Rows[i]["CreditA"].ToString().PadRight(15, ' ');
                string CreditC = dt.Rows[i]["CreditC"].ToString().PadRight(10, ' ');
                string HappyGoA = dt.Rows[i]["HappyGoA"].ToString().PadRight(15, ' ');
                string HappyGoC = dt.Rows[i]["HappyGoC"].ToString().PadRight(10, ' ');
                string DiscountA = "0".PadRight(15, ' ');
                string DiscountC = "0".PadRight(10, ' ');
                string dataString = NSTORE + OSTORE + CashC + CashA + CreditC + CreditA + HappyGoC + HappyGoA + DiscountC + DiscountA;
                writer.WriteLine(dataString);
            }
            headerString = "----------------------------------------------------------------------------------------------------------------------------------";
            writer.WriteLine(headerString);

            sqlstr = "select SUM(CASE  PAY_MODE_ID WHEN '1' THEN AMOUNT ELSE 0 END) CashA ";
            //sqlstr += ",COUNT(CASE  PAY_MODE_ID WHEN '1' THEN 1 ELSE 0 END) CashC ";
            sqlstr += ",COUNT(CASE  PAY_MODE_ID WHEN '1' THEN 1 ELSE NULL END) CashC ";
            sqlstr += ",SUM(CASE  WHEN PAY_MODE_ID in ('2','3') THEN AMOUNT ELSE 0 END) CreditA ";
            //sqlstr += ",COUNT(CASE  PAY_MODE_ID WHEN '2' THEN 1 ELSE 0 END) CreditC ";
            sqlstr += ",COUNT(CASE  WHEN PAY_MODE_ID in ('2','3') THEN 1 ELSE NULL END) CreditC ";
            sqlstr += ",SUM(CASE  PAY_MODE_ID WHEN '7' THEN AMOUNT ELSE 0 END) HappyGoA ";
            //sqlstr += ",COUNT(CASE  PAY_MODE_ID WHEN '7' THEN 1 ELSE 0 END) HappyGoC ";
            sqlstr += ",COUNT(CASE  PAY_MODE_ID WHEN '7' THEN 1 ELSE NULL END) HappyGoC ";
            sqlstr += " from BILL_DISPATCH bd,sale_head sh,sale_detail sd ";
            sqlstr += " where bd.POSUUID_MASTER = sh.posuuid_master and bd.SALE_DETAIL_ID = sd.id and bd.bill_type='2'  ";
            sqlstr += " and trunc(sh.trade_date)=trunc(to_date(" + OracleDBUtil.SqlStr(this.header.TRADE_DATE) + ",'YYYY/MM/DD')) ";
            sqlstr += "and bd.status='T' ";

            //dt = OracleDBUtil.GetDataSet(conn, sqlstr).Tables[0];
            DataTable dt2 = OracleDBUtil.GetDataSet(trans, sqlstr).Tables[0];//***20110316
            for (int i = 0; i < dt2.Rows.Count; i++)
            {
                string STORE = "Total:".PadRight(30, ' ');
                string CashA = dt2.Rows[i]["CashA"].ToString().PadRight(15, ' ');
                string CashC = dt2.Rows[i]["CashC"].ToString().PadRight(10, ' ');
                //string CreditA = dt2.Rows[i]["CreditC"].ToString().PadRight(10, ' ');
                string CreditA = dt2.Rows[i]["CreditA"].ToString().PadRight(15, ' ');
                string CreditC = dt2.Rows[i]["CreditC"].ToString().PadRight(10, ' ');
                string HappyGoA = dt2.Rows[i]["HappyGoA"].ToString().PadRight(15, ' ');
                string HappyGoC = dt2.Rows[i]["HappyGoC"].ToString().PadRight(10, ' ');
                string DiscountA = "0".PadRight(15, ' ');
                string DiscountC = "0".PadRight(10, ' ');
                string dataString = STORE + CashC + CashA + CreditC + CreditA + HappyGoC + HappyGoA + DiscountC + DiscountA;
                writer.WriteLine(dataString);
            }
            ////***20110316
            //if (conn2.State == ConnectionState.Open) conn2.Close();
            //conn2.Dispose();
            writer.Close();

        }
        #region 程式備份 20110317
        //public void ExportLogFile(Dictionary<string, string> Dfname)
        //{
        //    Dictionary<string, string> fname = Dfname;
        //    string file_name = "KGP" + DateTime.Now.ToString("yyMMdd") + "-" + DateTime.Now.ToString("yyMMddhhmmss") + "POS.log";
        //    StreamWriter writer = new StreamWriter(this.file_path + "\\" + file_name);
        //    string headerString = "Processing Date = " + DateTime.Now.ToString("yyyyMMdd");
        //    writer.WriteLine(headerString);
        //    foreach (KeyValuePair<string, string> item in fname)
        //    {
        //        if (item.Key == "1")
        //        {
        //            headerString = "Output Cash  File   :" + item.Value;
        //        }
        //        else if (item.Key == "2")
        //        {
        //            headerString = "Output Credit  File   :" + item.Value;
        //        }
        //        else if (item.Key == "7")
        //        {
        //            headerString = "Output HappyGo  File   :" + item.Value;
        //        }
        //        else { headerString = ""; }
        //        writer.WriteLine(headerString);
        //    }
        //    headerString = "Output Discount  File   :";
        //    writer.WriteLine(headerString);
        //    headerString = "Log File           :" + file_name;
        //    writer.WriteLine(headerString);
        //    headerString = "NEW_STORENO    OLD_STORENO              CASH                     CREDIT                   HappyGo                  Discount       ";
        //    writer.WriteLine(headerString);
        //    headerString = "----------------------------------------------------------------------------------------------------------------------------------";
        //    writer.WriteLine(headerString);

        //    string sqlstr = "select 'R'||STORE_NO STORE_NO,NVL((SELECT OLD_STORENO FROM STORENO_MAPPING WHERE NEW_STORENO='R'||STORE_NO),'') OLD_STORENO ";
        //    sqlstr += ",SUM(CASE  PAY_MODE_ID WHEN '1' THEN AMOUNT ELSE 0 END) CashA ";
        //    sqlstr += ",COUNT(CASE  PAY_MODE_ID WHEN '1' THEN 1 ELSE 0 END) CashC ";
        //    sqlstr += ",SUM(CASE  PAY_MODE_ID WHEN '2' THEN AMOUNT ELSE 0 END) CreditA ";
        //    sqlstr += ",COUNT(CASE  PAY_MODE_ID WHEN '2' THEN 1 ELSE 0 END) CreditC ";
        //    sqlstr += ",SUM(CASE  PAY_MODE_ID WHEN '7' THEN AMOUNT ELSE 0 END) HappyGoA ";
        //    sqlstr += ",COUNT(CASE  PAY_MODE_ID WHEN '7' THEN 1 ELSE 0 END) HappyGoC ";
        //    sqlstr += " from BILL_DISPATCH bd,sale_head sh,sale_detail sd ";
        //    sqlstr += " where bd.POSUUID_MASTER = sh.posuuid_master and bd.SALE_DETAIL_ID = sd.id and bd.bill_type='2'  ";
        //    sqlstr += "and bd.status='T' GROUP BY STORE_NO";
        //    OracleConnection conn2 = OracleDBUtil.GetConnection();//***20110316
        //    DataTable dt = OracleDBUtil.GetDataSet(conn2, sqlstr).Tables[0];//***20110316

        //    for (int i = 0; i < dt.Rows.Count; i++)
        //    {
        //        string NSTORE = dt.Rows[i]["STORE_NO"].ToString().PadRight(16, ' ');
        //        string OSTORE = dt.Rows[i]["OLD_STORENO"].ToString().PadRight(15, ' ');
        //        string CashA = dt.Rows[i]["CashA"].ToString().PadRight(10, ' ');
        //        string CashC = dt.Rows[i]["CashC"].ToString().PadRight(15, ' ');
        //        string CreditA = dt.Rows[i]["CreditC"].ToString().PadRight(10, ' ');
        //        string CreditC = dt.Rows[i]["CreditC"].ToString().PadRight(15, ' ');
        //        string HappyGoA = dt.Rows[i]["HappyGoA"].ToString().PadRight(10, ' ');
        //        string HappyGoC = dt.Rows[i]["HappyGoC"].ToString().PadRight(15, ' ');
        //        string DiscountA = "".PadRight(10, ' ');
        //        string DiscountC = "".PadRight(15, ' ');
        //        string dataString = NSTORE + OSTORE + CashA + CashC + CreditA + CreditC + HappyGoA + HappyGoC + DiscountA + DiscountC;
        //        writer.WriteLine(dataString);
        //    }
        //    headerString = "----------------------------------------------------------------------------------------------------------------------------------";
        //    writer.WriteLine(headerString);

        //    sqlstr = "select SUM(CASE  PAY_MODE_ID WHEN '1' THEN AMOUNT ELSE 0 END) CashA ";
        //    sqlstr += ",COUNT(CASE  PAY_MODE_ID WHEN '1' THEN 1 ELSE 0 END) CashC ";
        //    sqlstr += ",SUM(CASE  PAY_MODE_ID WHEN '2' THEN AMOUNT ELSE 0 END) CreditA ";
        //    sqlstr += ",COUNT(CASE  PAY_MODE_ID WHEN '2' THEN 1 ELSE 0 END) CreditC ";
        //    sqlstr += ",SUM(CASE  PAY_MODE_ID WHEN '7' THEN AMOUNT ELSE 0 END) HappyGoA ";
        //    sqlstr += ",COUNT(CASE  PAY_MODE_ID WHEN '7' THEN 1 ELSE 0 END) HappyGoC ";
        //    sqlstr += " from BILL_DISPATCH bd,sale_head sh,sale_detail sd ";
        //    sqlstr += " where bd.POSUUID_MASTER = sh.posuuid_master and bd.SALE_DETAIL_ID = sd.id and bd.bill_type='2'  ";
        //    sqlstr += "and bd.status='T' ";

        //    //dt = OracleDBUtil.GetDataSet(conn, sqlstr).Tables[0];
        //    dt = OracleDBUtil.GetDataSet(conn2, sqlstr).Tables[0];//***20110316
        //    for (int i = 0; i < dt.Rows.Count; i++)
        //    {
        //        string STORE = "Total:".PadRight(31, ' ');
        //        string CashA = dt.Rows[i]["CashA"].ToString().PadRight(10, ' ');
        //        string CashC = dt.Rows[i]["CashC"].ToString().PadRight(15, ' ');
        //        string CreditA = dt.Rows[i]["CreditC"].ToString().PadRight(10, ' ');
        //        string CreditC = dt.Rows[i]["CreditC"].ToString().PadRight(15, ' ');
        //        string HappyGoA = dt.Rows[i]["HappyGoA"].ToString().PadRight(10, ' ');
        //        string HappyGoC = dt.Rows[i]["HappyGoC"].ToString().PadRight(15, ' ');
        //        string DiscountA = "".PadRight(10, ' ');
        //        string DiscountC = "".PadRight(15, ' ');
        //        string dataString = STORE + CashA + CashC + CreditA + CreditC + HappyGoA + HappyGoC + DiscountA + DiscountC;
        //        writer.WriteLine(dataString);
        //    }
        //    //***20110316
        //    if (conn2.State == ConnectionState.Open) conn2.Close();
        //    conn2.Dispose();
        //    writer.Close();

        //}
        #endregion
        #endregion

        public void RunFTP()
        {
            string FTP_UPLOAD_ID = GuidNo.getUUID();
            Console.WriteLine("查詢 FTP SERVER 設定");
            DataTable dtFtpInfo = Query_FTP_INFO();
            if (dtFtpInfo.Rows.Count < 1)
                throw new Exception("SYS_PARA未設定FTP連線資訊");
            string F_HOST = dtFtpInfo.Rows[0]["F_HOST"].ToString();
            string F_USER = dtFtpInfo.Rows[0]["F_USER"].ToString();
            string F_PASSWORD = dtFtpInfo.Rows[0]["F_PASSWORD"].ToString();
            string F_PORT = dtFtpInfo.Rows[0]["F_PORT"].ToString();
            string USER = "KGT_BILLING";
            string sqlstr = "insert into FTP_UPLOAD_POOL(FTP_UPLOAD_ID,FUNC_ID,FTP_URL,LOGIN_ID,LOGIN_PW,PORT_NO,STATUS,CREATE_USER,CREATE_DTM,MODI_USER,MODI_DTM) VALUES(:FTP_UPLOAD_ID,:FUNC_ID,:FTP_URL,:LOGIN_ID,:LOGIN_PW,:PORT_NO,'0',:CREATE_USER,SYSDATE,:MODI_USER,SYSDATE)";

            OracleCommand cmd = new OracleCommand(sqlstr, conn, trans);
            cmd.Parameters.Add(":FTP_UPLOAD_ID", OracleType.NVarChar).Value = FTP_UPLOAD_ID;
            cmd.Parameters.Add(":FUNC_ID", OracleType.NVarChar).Value = USER;
            cmd.Parameters.Add(":FTP_URL", OracleType.NVarChar).Value = F_HOST;
            cmd.Parameters.Add(":LOGIN_ID", OracleType.NVarChar).Value = F_USER;
            cmd.Parameters.Add(":LOGIN_PW", OracleType.Char).Value = F_PASSWORD;
            cmd.Parameters.Add(":PORT_NO", OracleType.NVarChar).Value = F_PORT;
            cmd.Parameters.Add(":CREATE_USER", OracleType.NVarChar).Value = USER;
            cmd.Parameters.Add(":MODI_USER", OracleType.NVarChar).Value = USER;

            cmd.ExecuteNonQuery();


            StringBuilder sb = new StringBuilder();
            sb.Append(" insert into FTP_UPLOAD_FILES(");
            sb.Append(" FTP_UPLOAD_FILES_ID, ");
            sb.Append(" FTP_UPLOAD_ID, ");
            sb.Append(" SEQ_NO, ");
            sb.Append(" FILE_PATH, ");
            sb.Append(" FILE_NAME ");
            sb.Append(" )VALUES( ");
            sb.Append(" :FTP_UPLOAD_FILES_ID, ");
            sb.Append(" :FTP_UPLOAD_ID, ");
            sb.Append(" :SEQ_NO, ");
            sb.Append(" :FILE_PATH, ");
            sb.Append(" :FILE_NAME ");
            sb.Append(" ) ");

            cmd = new OracleCommand(sb.ToString(), conn, trans);
            cmd.Parameters.Add(":FTP_UPLOAD_FILES_ID", OracleType.NVarChar);
            cmd.Parameters.Add(":FTP_UPLOAD_ID", OracleType.NVarChar).Value = FTP_UPLOAD_ID;
            cmd.Parameters.Add(":SEQ_NO", OracleType.Number);
            cmd.Parameters.Add(":FILE_PATH", OracleType.NVarChar);
            cmd.Parameters.Add(":FILE_NAME", OracleType.NVarChar);
            string[] files = Directory.GetFiles(this.file_path);
            int i = 1;
            foreach (string file in files)
            {
                FileInfo f = new FileInfo(file);

                //判斷是否需要塞檔案
                sqlstr = "select * from FTP_UPLOAD_FILES where FILE_PATH=:FILE_PATH and FILE_NAME = :FILE_NAME and (status = '0' or status = '1')";
                OracleCommand cmd2 = new OracleCommand(sqlstr, conn, trans);
                cmd2.Parameters.Add(":FILE_PATH", OracleType.NVarChar).Value = f.DirectoryName;
                cmd2.Parameters.Add(":FILE_NAME", OracleType.NVarChar).Value = f.Name;
                OracleDataReader dr = cmd2.ExecuteReader();
                bool hasFile = dr.HasRows;
                dr.Close();
                if (!hasFile)
                {
                    cmd.Parameters[":FTP_UPLOAD_FILES_ID"].Value = GuidNo.getUUID();
                    cmd.Parameters[":SEQ_NO"].Value = i++;
                    cmd.Parameters[":FILE_PATH"].Value = f.DirectoryName;
                    cmd.Parameters[":FILE_NAME"].Value = f.Name;
                    cmd.ExecuteNonQuery();
                }
            }
        }

        private DataTable Query_FTP_INFO()
        {
            try
            {
                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                sb.Length = 0;
                sb.Append("SELECT ");
                sb.Append("( SELECT PARA_VALUE FROM SYS_PARA WHERE PARA_KEY='KGT_BILLING_FTP_SERVER' ) AS F_HOST,");
                sb.Append("( select para_value from sys_para where para_key='KGT_BILLING_FTP_USER' ) AS F_USER,");
                sb.Append("( select para_value from sys_para where para_key='KGT_BILLING_FTP_PORT' ) AS F_PORT,");
                sb.Append("( SELECT PARA_VALUE FROM SYS_PARA WHERE PARA_KEY='KGT_BILLING_FTP_PW' ) AS F_PASSWORD ");
                sb.Append("FROM DUAL");
                OracleCommand cmd = new OracleCommand(sb.ToString(), conn, trans);
                OracleDataAdapter da = new OracleDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        public static string SEQNO()
        {
            OracleConnection conn = null;
            string strSQL = "Select KGT_BILLFILE_SEQ.nextval  from dual ";
            DataSet ds = null;
            string strResult = "";

            try
            {
                conn = OracleDBUtil.GetConnection();
                ds = OracleDBUtil.GetDataSet(conn, strSQL);

                if (ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        strResult = dr[0].ToString().Replace("-", "");


                    }
                }
                else
                {
                    throw new Exception("SEQNO No Data Fund");
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                if (conn.State == ConnectionState.Open) conn.Close();
                conn.Dispose();
                OracleConnection.ClearAllPools();
                conn = null;
                ds.Dispose();
            }
            return strResult;
        }

        public static Dictionary<string, string> getPAY_MODE()
        {
            Dictionary<string, string> dir = new Dictionary<string, string>();
            dir.Add("現金", "1");
            dir.Add("信用卡", "2");
            dir.Add("HappyGo", "7");
            return dir;
        }

        public string getExchange_File_Name()
        {
            //***20110317 信用卡KGR開頭
            //string file_name = "KGP" + DateTime.Now.ToString("yyMMdd") + "-" + DateTime.Now.ToString("yyMMddhhmmss") + "{0}";
            //string file_name = "{0}" + DateTime.Now.ToString("yyMMdd") + "-" + DateTime.Now.ToString("yyMMddhhmmss") + "{1}";
            string file_name = "{0}" + this.header.TRADE_DATE.Replace("/", "").Substring(2) + "-" + DateTime.Now.ToString("yyMMddhhmmss") + "{1}";
            if (this.header.PAY_MODE_ID == "1")
            {
                file_name = string.Format(file_name, "KGP", "-Ca.pos");
            }
            else if (this.header.PAY_MODE_ID == "2")
            {
                file_name = string.Format(file_name, "KGR", "-Cr.pos");
            }
            else if (this.header.PAY_MODE_ID == "7")
            {
                file_name = string.Format(file_name, "KGP", "-Hg.pos");
            }
            else
            {
                file_name = string.Format(file_name, "");
            }

            #region 程式備份 20110317
            //string file_name = "KGP" + DateTime.Now.ToString("yyMMdd") + "-" + DateTime.Now.ToString("yyMMddhhmmss") + "{0}";
            //if (this.header.PAY_MODE_ID == "1")
            //{
            //    file_name = string.Format(file_name, "-Ca.POS");
            //}
            //else if (this.header.PAY_MODE_ID == "2")
            //{
            //    file_name = string.Format(file_name, "-Cr.POS");
            //}
            //else if (this.header.PAY_MODE_ID == "7")
            //{
            //    file_name = string.Format(file_name, "-Hg.POS");
            //}
            //else
            //{
            //    file_name = string.Format(file_name, "");
            //}
            #endregion

            return file_name;
        }

        public string getFileName()
        {
            //***20110317 + 以前一天日期命名
            //string file_name = "K" + DateTime.Now.ToString("yyyyMMdd") + "{0}";
            string file_name = "K" + DateTime.Now.AddDays(-1).ToString("yyyyMMdd") + "{0}";
            if (this.header.PAY_MODE_ID == "1")
            {
                file_name = string.Format(file_name, "POS");
            }
            else if (this.header.PAY_MODE_ID == "2")
            {
                //file_name = string.Format(file_name, "CR");
                file_name = string.Format(file_name, "POS");
            }
            else if (this.header.PAY_MODE_ID == "7")
            {
                //file_name = string.Format(file_name, "HG");
                file_name = string.Format(file_name, "HGO");
            }
            else
            {
                file_name = string.Format(file_name, "");
            }

            return file_name;
        }


        public string getFileName2()
        {
            //***20110317 + 以前一天日期命名
            //string file_name = "K" + DateTime.Now.ToString("yyyyMMdd") + "{0}";
            string file_name = "K" + this.header.TRADE_DATE.Replace("/", "") + "{0}";
            if (this.header.PAY_MODE_ID == "1")
            {
                file_name = string.Format(file_name, "POS");
            }
            else if (this.header.PAY_MODE_ID == "2")
            {
                //file_name = string.Format(file_name, "CR");
                file_name = string.Format(file_name, "POS");
            }
            else if (this.header.PAY_MODE_ID == "7")
            {
                //file_name = string.Format(file_name, "HG");
                file_name = string.Format(file_name, "HGO");
            }
            else
            {
                file_name = string.Format(file_name, "");
            }

            return file_name;
        }
        #region Insert Table
        public void Insert()
        {

            string sqlstr = "insert into KGT_BILLING_M(KGT_BFM_ID,FILE_NAME,DATE_OF_FILE,Time,BATCH_N0,ISSUER,BANK_CODE,Spare1,Spare2,PAY_MODE_ID,PAY_MODE_NAME) values(:KGT_BFM_ID,:FILE_NAME,:DATE_OF_FILE,:Time,:BATCH_N0,:ISSUER,:BANK_CODE,:Spare1,:Spare2,:PAY_MODE_ID,:PAY_MODE_NAME)";
            OracleCommand cmd = new OracleCommand(sqlstr, conn, trans);
            cmd.Parameters.Add(":KGT_BFM_ID", OracleType.NVarChar, 32).Value = header.KGT_BF_ID;
            cmd.Parameters.Add(":FILE_NAME", OracleType.NVarChar, 12).Value = header.FILE_NAME;
            cmd.Parameters.Add(":DATE_OF_FILE", OracleType.NVarChar, 8).Value = header.DATE_OF_FILE;
            cmd.Parameters.Add(":Time", OracleType.NVarChar, 8).Value = header.Time;
            cmd.Parameters.Add(":BATCH_N0", OracleType.NVarChar, 10).Value = header.BATCH_NO;
            cmd.Parameters.Add(":ISSUER", OracleType.NVarChar, 10).Value = header.ISSUER;
            cmd.Parameters.Add(":Spare1", OracleType.NVarChar, 20).Value = header.Spare1;
            cmd.Parameters.Add(":Spare2", OracleType.NVarChar, 20).Value = header.Spare2;
            cmd.Parameters.Add(":BANK_CODE", OracleType.NVarChar, 10).Value = header.BANK_CODE;
            cmd.Parameters.Add(":PAY_MODE_ID", OracleType.NVarChar, 20).Value = header.PAY_MODE_ID;
            cmd.Parameters.Add(":PAY_MODE_NAME", OracleType.NVarChar, 20).Value = header.PAY_MODE_NAME;

            try
            {
                cmd.ExecuteNonQuery();
                log.Success("Insert KGT_BILLING_M Success,");
                #region Insert Data
                sqlstr = "insert into KGT_BILLING_D (KGT_BFD_ID,KGT_BFM_ID,BILL_DISPATCH_ID,DOCUMENT_NO,USER_BAR_CODE,AMOUNT,REF_NO,TRANS_DATE,SPARE3,SPARE4,CONTROL_DIGIT) VALUES(:KGT_BFD_ID,:KGT_BFM_ID,:BILL_DISPATCH_ID,:DOCUMENT_NO,:USER_BAR_CODE,:AMOUNT,:REF_NO,:TRANS_DATE,:SPARE3,:SPARE4,:CONTROL_DIGIT)";
                cmd = new OracleCommand(sqlstr, conn, trans);
                cmd.Parameters.Add(":KGT_BFD_ID", OracleType.NVarChar, 32);
                cmd.Parameters.Add(":KGT_BFM_ID", OracleType.NVarChar, 32);
                cmd.Parameters.Add(":BILL_DISPATCH_ID", OracleType.NVarChar, 32);
                cmd.Parameters.Add(":DOCUMENT_NO", OracleType.NVarChar, 8);
                cmd.Parameters.Add(":USER_BAR_CODE", OracleType.NVarChar, 20);
                cmd.Parameters.Add(":AMOUNT", OracleType.Number);
                cmd.Parameters.Add(":REF_NO", OracleType.NVarChar, 8);
                cmd.Parameters.Add(":TRANS_DATE", OracleType.NVarChar, 8);
                cmd.Parameters.Add(":SPARE3", OracleType.NVarChar, 20);
                cmd.Parameters.Add(":SPARE4", OracleType.NVarChar, 20);
                cmd.Parameters.Add(":CONTROL_DIGIT", OracleType.NVarChar, 1);


                foreach (Data data in dataList)
                {
                    cmd.Parameters[":KGT_BFD_ID"].Value = data.KGT_BFD_ID;
                    cmd.Parameters[":KGT_BFM_ID"].Value = header.KGT_BF_ID;//***20110306 欄位KEY錯
                    cmd.Parameters[":BILL_DISPATCH_ID"].Value = data.BILL_DISPATCH_ID;
                    cmd.Parameters[":DOCUMENT_NO"].Value = data.DOCUMENT_NO;
                    cmd.Parameters[":USER_BAR_CODE"].Value = data.USER_BAR_CODE;
                    cmd.Parameters[":AMOUNT"].Value = Convert.ToDecimal(data.AMOUNT);
                    cmd.Parameters[":REF_NO"].Value = data.REF_NO;
                    cmd.Parameters[":TRANS_DATE"].Value = data.TRANS_DATE;
                    cmd.Parameters[":SPARE3"].Value = data.Spare3;
                    cmd.Parameters[":SPARE4"].Value = data.Spare4;
                    cmd.Parameters[":CONTROL_DIGIT"].Value = data.CONTROL_DIGIT;

                    cmd.ExecuteNonQuery();
                }
                log.Success(string.Format("Insert KGT_BILLING_D Count : {0} Success,", dataList.Count));
                #endregion

                #region Insert Trailer
                sqlstr = "insert into KGT_BILLING_TR(TRAILER_RECORD_ID,KGT_BFM_ID,TOTAL_TRANS_COUNT,SUM_OF_TRANS,CONTROL_DIGIT) values(:TRAILER_RECORD_ID,:KGT_BFM_ID,:TOTAL_TRANS_COUNT,:SUM_OF_TRANS,:CONTROL_DIGIT)";
                cmd = new OracleCommand(sqlstr, conn, trans);
                cmd.Parameters.Add(":TRAILER_RECORD_ID", OracleType.NVarChar, 32).Value = trailer.TRAILER_RECORD_ID;
                cmd.Parameters.Add(":KGT_BFM_ID", OracleType.NVarChar, 32).Value = header.KGT_BF_ID;
                cmd.Parameters.Add(":TOTAL_TRANS_COUNT", OracleType.Number).Value = Convert.ToDecimal(trailer.TOTAL_TRANS_COUNT);
                cmd.Parameters.Add(":SUM_OF_TRANS", OracleType.Number).Value = Convert.ToDecimal(trailer.SUM_OF_TRANS);
                cmd.Parameters.Add(":CONTROL_DIGIT", OracleType.NVarChar, 1).Value = trailer.CONTROL_DIGIT;
                cmd.ExecuteNonQuery();
                log.Success("Insert KGT_BILLING_TR Success,");
                #endregion

                #region KGT_BILLING_M 預壓狀態
                sqlstr = "update KGT_BILLING_M set SEND_FLAG='T' where SEND_FLAG='0' and PAY_MODE_ID = :PAY_MODE_ID";
                cmd = new OracleCommand(sqlstr, conn, trans);
                cmd.Parameters.Add(":PAY_MODE_ID", OracleType.NVarChar, 20).Value = header.PAY_MODE_ID;
                cmd.ExecuteNonQuery();
                #endregion
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
        #endregion

        public void Commit()
        {
            trans.Commit();
        }

        public void Rollback()
        {
            trans.Rollback();
        }

        public void Close()
        {
            if (conn.State == ConnectionState.Open) conn.Close();
            conn.Dispose();
            OracleConnection.ClearAllPools();
        }

        #region Header Class
        public class Header
        {
            public string Exchange_File_Name;
            public string KGT_BF_ID;
            public string FILE_NAME;
            public string DATE_OF_FILE;
            public string Time;
            public string BATCH_NO;
            public string ISSUER;
            public string BANK_CODE;
            public string Spare1;
            public string Spare2;
            public string PAY_MODE_ID;
            public string PAY_MODE_NAME;
            public string TRADE_DATE;//***20110317

            public Header()
            {
                KGT_BF_ID = GuidNo.getUUID();

                int year = DateTime.Now.Year - 1911;
                DATE_OF_FILE = year.ToString("0000") + DateTime.Now.ToString("MMdd");
                Time = DateTime.Now.ToString("HH:mm:ss");
                BATCH_NO = KGT_BILLING.SEQNO();
                ISSUER = "COMP STORE";
                BANK_CODE = "S01";
                Spare1 = "";
                Spare2 = "";
                PAY_MODE_ID = "";
                PAY_MODE_NAME = "";
                this.TRADE_DATE = "";
            }



        }
        #endregion

        #region Data Class
        public class Data
        {
            public string KGT_BFD_ID;
            public string BILL_DISPATCH_ID;
            public string DOCUMENT_NO;
            public string USER_BAR_CODE;
            public string AMOUNT;
            public string TRANS_DATE;
            public string Spare3;
            public string Spare4;
            public string CONTROL_DIGIT;
            public string PAY_MODE_ID;
            public string PAY_MODE_NAME;
            public string REF_NO;
            public string OLD_STORENO;

            public Data(string PAY_MODE_ID)
            {
                this.KGT_BFD_ID = GuidNo.getUUID();
                this.PAY_MODE_ID = PAY_MODE_ID;
                this.REF_NO = "";

            }


            #region 讀取Data
            public static List<Data> getDataList(string PAY_MODE_ID, string sTRADE_DATE, OracleConnection conn, OracleTransaction trans)
            {
                List<Data> dataList = new List<Data>();

                //***20110316 + order by store_no ; 
                //string sqlstr = "select  '' as DOCUMENT_NO,SD.BARCODE1 as USER_BAR_CODE,bd.AMOUNT as AMOUNT,''  as REF_NO,SH.TRADE_DATE as TRANS_DATE,'' as SPARE3,'' as SPARE4,''  as CONTROL_DIGIT,bd.BILL_DISPATCH_ID as BILL_DISPATCH_ID ,NVL((SELECT OLD_STORENO FROM STORENO_MAPPING WHERE NEW_STORENO='R'||STORE_NO),'') OLD_STORENO from BILL_DISPATCH bd,sale_head sh,sale_detail sd where bd.POSUUID_MASTER = sh.posuuid_master and bd.SALE_DETAIL_ID = sd.id and bd.bill_type='2' and bd.status='T' and bd.PAY_MODE_ID =:PAY_MODE_ID   ORDER BY SH.STORE_NO ";
                string sqlstr = "select  '' as DOCUMENT_NO,SD.BARCODE1 as USER_BAR_CODE,bd.AMOUNT as AMOUNT,''  as REF_NO,SH.TRADE_DATE as TRANS_DATE,'' as SPARE3,'' as SPARE4,''  as CONTROL_DIGIT,bd.BILL_DISPATCH_ID as BILL_DISPATCH_ID ,NVL((SELECT OLD_STORENO FROM STORENO_MAPPING WHERE NEW_STORENO='R'||STORE_NO),'') OLD_STORENO from BILL_DISPATCH bd,sale_head sh,sale_detail sd where bd.POSUUID_MASTER = sh.posuuid_master and bd.SALE_DETAIL_ID = sd.id and bd.bill_type='2' and bd.status='T' and bd.PAY_MODE_ID IN (:PAY_MODE_ID)  and trunc(sh.trade_date)=trunc(to_date(:TRADE_DATE,'YYYY/MM/DD')) ORDER BY SH.STORE_NO ";

                if (PAY_MODE_ID.ToString() == "2")
                {
                    sqlstr = sqlstr.Replace(":PAY_MODE_ID", "2,3");
                }
                else
                {
                    sqlstr = sqlstr.Replace(":PAY_MODE_ID", PAY_MODE_ID);
                }
                OracleCommand cmd = new OracleCommand(sqlstr, conn, trans);
                //cmd.Parameters.Add(":PAY_MODE_ID", OracleType.VarChar).Value = PAY_MODE_ID;
                cmd.Parameters.Add(":TRADE_DATE", OracleType.VarChar).Value = sTRADE_DATE;
                try
                {

                    OracleDataReader dr = cmd.ExecuteReader();
                    int i = 1;
                    while (dr.Read())
                    {
                        Data data = new Data(PAY_MODE_ID);
                        data.DOCUMENT_NO = i.ToString();
                        data.USER_BAR_CODE = dr["USER_BAR_CODE"].ToString();
                        data.AMOUNT = dr["AMOUNT"].ToString();
                        data.REF_NO = "";
                        string year = (Convert.ToDateTime(dr["TRANS_DATE"]).Year - 1911).ToString("0000");
                        data.TRANS_DATE = year + Convert.ToDateTime(dr["TRANS_DATE"]).ToString("MMdd");
                        data.OLD_STORENO = dr["OLD_STORENO"].ToString();
                        data.Spare3 = "";
                        data.Spare4 = "";
                        data.BILL_DISPATCH_ID = dr["BILL_DISPATCH_ID"].ToString();
                        //計算control_digit
                        string control_digit = data.DOCUMENT_NO + data.USER_BAR_CODE + data.AMOUNT + data.REF_NO + data.TRANS_DATE;
                        char[] digit_chars = control_digit.ToCharArray();
                        int digit_int = 0;
                        foreach (char ch in digit_chars)
                        {
                            if (char.IsNumber(ch))
                            {
                                digit_int += Convert.ToInt32(ch);
                            }
                        }
                        digit_int = digit_int % 11;
                        digit_int = digit_int == 10 ? 0 : digit_int;
                        data.CONTROL_DIGIT = digit_int.ToString();
                        dataList.Add(data);
                        i++;
                    }
                    dr.Close();

                    return dataList;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                //finally
                //{
                //    if (conn.State == ConnectionState.Open) conn.Close();
                //    conn.Dispose();
                //    OracleConnection.ClearAllPools();
                //}
            }
            #endregion

        }
        #endregion

        #region Trailer Class
        public class Trailer
        {
            public string TRAILER_RECORD_ID;
            public string TOTAL_TRANS_COUNT;
            public string SUM_OF_TRANS;
            public string CONTROL_DIGIT;

            public Trailer()
            {
                this.TRAILER_RECORD_ID = GuidNo.getUUID();

            }
        }
        #endregion
    }
}
