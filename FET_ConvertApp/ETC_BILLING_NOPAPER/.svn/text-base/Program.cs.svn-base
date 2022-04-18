using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Advtek.Utility;
using System.Data;
using System.Data.OracleClient;
using System.IO;
using FTP;
namespace ETC_BILLING_NOPAPER
{
    class Program
    {
        static void Main(string[] args)
        {
            //2.初始化LOG
            ConvertLog log = new ConvertLog("ETC_BILLING_NOPAPER");
            ETC_BILLING_NOPAPER nb = new ETC_BILLING_NOPAPER(log);
            try
            {
                Console.WriteLine("Init");
                nb.Init();
                Console.WriteLine("Load Data");
                nb.Load(); //讀取資料
                Console.WriteLine("Insert Table");
                nb.Insert(); //新增資料
                Console.WriteLine("Export File");
                nb.ExportFile();//產生實體檔案
                Console.WriteLine("Update");
                nb.Update();
                nb.RunFTP();
                nb.Commit();
                log.Success("ETC_BILLING_NOPAPER 處理完成");
            }
            catch (Exception ex)
            {
                nb.Rollback();
                Console.WriteLine(ex.Message);
             
                log.Fail(ex.Message);
            }
            finally
            {
                nb.Close();
            }

        }


    }

    public class ETC_BILLING_NOPAPER
    {
        #region Header Class
        public class Header
        {

            public string ETC_BFM_ID;
            public string FILE_NAME;
            public string TRAND_TYPE;
            public string DATE_OF_FILE;
            public string BATCH_NO;
            public string ORGAN;
            public string HEADER_FLG;
            public string SEND_STATUS;
            public string SEND_DTM;
            public string SEND_STATUS_MSG;


            public Header()
            {
                this.ETC_BFM_ID = GuidNo.getUUID();
                int year = DateTime.Now.Year - 1911;
                DATE_OF_FILE = year.ToString("0000") + DateTime.Now.ToString("MMdd");
                //20110323 資料改抓前一天，檔名跟著改
                //this.FILE_NAME = "FETB02." + DateTime.Now.ToString("yyyyMMdd") + "_01.TXT";
                this.FILE_NAME = "FETB02." + DateTime.Now.AddDays(-1).ToString("yyyyMMdd") + "_01.txt";
                this.BATCH_NO = ETC_BILLING_NOPAPER.SEQNO();
                this.SEND_STATUS = "0";
                this.SEND_STATUS_MSG = "";
                this.ORGAN = "B020000";
                this.HEADER_FLG = "1";
                this.TRAND_TYPE = "B02";

            }



        }
        #endregion

        #region Data Class
        public class Data
        {
            public string ETC_BFD_ID;
            public string HEADER_FLG;
            public string STORE_NO;
            public string HOST_ID;
            public string TXT_DATE;
            public string TXT_TIME;
            public string SALE_DETAIL_ID;
            public string BILL_DATE;
            public string AMOUNT;
            public string BARCODE1;
            public string BARCODE2;
            public string BARCODE3;
            public string DIFF_FLG;
            public string SEQ_NO;
            public string REC_TYPE;
            public string BILL_DISPATCH_ID;



            public Data()
            {
                this.ETC_BFD_ID = GuidNo.getUUID();
                this.HEADER_FLG = "2";
                this.TXT_DATE = DateTime.Now.ToString("yyyyMMdd");
                this.TXT_TIME = DateTime.Now.ToString("HHmmss");
                this.DIFF_FLG = "1";
                this.REC_TYPE = "PAY";

            }

            #region Static Method
            public static List<Data> getDataList(OracleTransaction objTx)
            {
                List<Data> dataList = new List<Data>();
                OracleConnection conn = objTx.Connection;
                //string sqlstr = "select SH.STORE_NO as store_no,SH.MACHINE_ID as Host_id,To_char(SH. TRADE_DATE,'YYYYMMDD')  as TXT_DATE,To_char(SH. TRADE_DATE,'HH24MISS')  as TXT_TIME,SD.ID as SALE_DETAIL_ID,SD.BARCODE1 As BARCODE1,SD.BARCODE2 As BARCODE2,SD.BARCODE3 As BARCODE3,Substr(SD.barcode1,1,6) As BILL_DATE, BD.AMOUNT as AMOUNT, ROWNUM as SEQ_NO,Bd.BILL_DISPATCH_ID , Bd.PAY_MODE_ID ,Bd.PAY_MODE_NAME from BILL_DISPATCH bd,  sale_head sh,sale_detail sd where bd.POSUUID_MASTER = sh.posuuid_master   and bd.SALE_DETAIL_ID = sd.id and bd.bill_type='5' and bd.status='T'   ORDER BY SH.STORE_NO";
                string sqlstr = "select SH.STORE_NO as store_no,SH.MACHINE_ID as Host_id,To_char(SH.TRADE_DATE,'YYYYMMDD')  as TXT_DATE,To_char(SH.TRADE_DATE,'HH24MISS')  as TXT_TIME,SD.ID as SALE_DETAIL_ID,SD.BARCODE1 As BARCODE1,SD.BARCODE2 As BARCODE2,SD.BARCODE3 As BARCODE3,Substr(SD.barcode1,1,6) As BILL_DATE, BD.AMOUNT as AMOUNT, sd.SEQNO as SEQ_NO,Bd.BILL_DISPATCH_ID , Bd.PAY_MODE_ID ,Bd.PAY_MODE_NAME from BILL_DISPATCH bd,  sale_head sh,sale_detail sd where bd.POSUUID_MASTER = sh.posuuid_master   and bd.SALE_DETAIL_ID = sd.id and bd.bill_type='5' and bd.status='T' and trunc(SH.TRADE_DATE)=trunc(sysdate-1) and prodno not in (select para_value from sys_para where para_key ='ETC_WORKING_ITEM_CODE')   ORDER BY SH.STORE_NO";

             

                OracleCommand cmd = new OracleCommand(sqlstr, conn, objTx);

                try
                {

                    OracleDataReader dr = cmd.ExecuteReader();

                    while (dr.Read())
                    {
                        Data data = new Data();
                        data.STORE_NO = dr["STORE_NO"].ToString();
                        data.HOST_ID = dr["HOST_ID"].ToString();
                        data.SALE_DETAIL_ID = dr["SALE_DETAIL_ID"].ToString();
                        data.BILL_DATE = dr["BILL_DATE"].ToString();
                        data.AMOUNT = dr["AMOUNT"].ToString();
                        data.TXT_DATE = dr["TXT_DATE"].ToString();
                        data.TXT_TIME = dr["TXT_TIME"].ToString();
                        data.BARCODE1 = dr["BARCODE1"].ToString();
                        data.BARCODE2 = dr["BARCODE2"].ToString();
                        data.BARCODE3 = dr["BARCODE3"].ToString();
                        data.SEQ_NO = dr["SEQ_NO"].ToString();
                        data.BILL_DISPATCH_ID = dr["BILL_DISPATCH_ID"].ToString();
                        dataList.Add(data);

                    }
                    dr.Close();

                    return dataList;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
             
            }
            #endregion
        }
        #endregion

        #region Trailer Class
        public class Trailer
        {
            public string ETC_BFT_ID;

            public string HEADER_FLG;
            public string TOTAL_RECORD_AMOUNT;
            public string TOTAL_AMOUNT;

            public Trailer()
            {
                this.ETC_BFT_ID = GuidNo.getUUID();
                this.HEADER_FLG = "3";
                this.TOTAL_AMOUNT = "0";
                this.TOTAL_RECORD_AMOUNT = "0";
            }
        }
        #endregion

        public Header header;
        public List<Data> dataList;
        public Trailer trailer;
        string file_path;
        OracleConnection conn;
        OracleTransaction trans;
        ConvertLog log;
        public ETC_BILLING_NOPAPER(ConvertLog log)
        {
            this.log = log;
        }

        #region Method
        public string getFile_path()
        {
            string path = getIISUrl();

            return path;
        }

        public static string getIISUrl()
        {
            OracleConnection conn = null;
            //string strSQL = "Select PARA_VALUE  from SYS_PARA where PARA_KEY = 'IIS_URL'";
            string strSQL = "Select PARA_VALUE  from SYS_PARA where PARA_KEY = 'IIS_URL_ETC'";

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
        public static string SEQNO()
        {
            OracleConnection conn = null;
            string strSQL = "Select ETC_BILLFILE_SEQ.nextval  from dual ";
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

        public void Load()
        {

            header = new Header();
            dataList = Data.getDataList(this.trans);
            log.Success(string.Format("Select Data : {0} 筆,", dataList.Count.ToString()));
            trailer = new Trailer();
            trailer.TOTAL_RECORD_AMOUNT = dataList.Count.ToString();
            trailer.TOTAL_AMOUNT = SUM();
            //this.file_path = Path.Combine(getFile_path(), "Bill_FILES\\ETC\\");
            this.file_path = getFile_path();//***20110323
            if (!Directory.Exists(this.file_path)) Directory.CreateDirectory(this.file_path);
        }

        public string SUM()
        {
            double amount = 0;
            foreach (Data data in dataList)
            {
                amount += Convert.ToDouble(data.AMOUNT);
            }

            return amount.ToString();
        }

        public void Init()
        {
            conn = OracleDBUtil.GetConnection();
            trans = conn.BeginTransaction();
            //***20110323 + and exists  (SELECT 'X' FROM SALE_HEAD WHERE posuuid_master=bill_dispatch.posuuid_master and   TRUNC(TRADE_DATE)<=TRUNC(SYSDATE-1))
            //string sqlstr = "update BILL_DISPATCH set STATUS='T' where bill_type='5' and STATUS='0' and BILL_TX_TYPE='1'";
            string sqlstr = "update BILL_DISPATCH set STATUS='T' where bill_type='5' and STATUS='0' and BILL_TX_TYPE='1' and exists  (SELECT 'X' FROM SALE_HEAD WHERE posuuid_master=bill_dispatch.posuuid_master and   TRUNC(TRADE_DATE)<=TRUNC(SYSDATE-1))";
            OracleCommand cmd = new OracleCommand(sqlstr, conn, trans);
            cmd.ExecuteNonQuery();
            log.Success("Update BILL_DISPATCH STATUS '0' -> 'T' Success,");
        }

        public void Insert()
        {
            string sqlstr = "insert into ETC_BILLING_M(ETC_BFM_ID,HEADER_FLG,TRANS_TYPE,ORGAN,BATCH_NO,DATE_OF_FILE,SEND_DTM,SEND_STATUS,SEND_STATUS_MSG,FILE_NAME) values(:ETC_BFM_ID,:HEADER_FLG,:TRANS_TYPE,:ORGAN,:BATCH_NO,:DATE_OF_FILE,SYSDATE,:SEND_STATUS,:SEND_STATUS_MSG,:FILE_NAME)";
            OracleCommand cmd = new OracleCommand(sqlstr, conn, trans);
            cmd.Parameters.Add(":ETC_BFM_ID", OracleType.NVarChar, 32).Value = header.ETC_BFM_ID;
            cmd.Parameters.Add(":HEADER_FLG", OracleType.NVarChar, 12).Value = header.HEADER_FLG;
            cmd.Parameters.Add(":TRANS_TYPE", OracleType.NVarChar, 4).Value = header.TRAND_TYPE;
            cmd.Parameters.Add(":ORGAN", OracleType.NVarChar, 8).Value = header.ORGAN;
            cmd.Parameters.Add(":BATCH_NO", OracleType.NVarChar, 11).Value = header.BATCH_NO.PadLeft(11, '0');
            cmd.Parameters.Add(":FILE_NAME", OracleType.NVarChar, 10).Value = header.FILE_NAME;
            cmd.Parameters.Add(":DATE_OF_FILE", OracleType.NVarChar, 10).Value = header.DATE_OF_FILE;
            cmd.Parameters.Add(":SEND_STATUS", OracleType.NVarChar, 20).Value = header.SEND_STATUS;
            cmd.Parameters.Add(":SEND_STATUS_MSG", OracleType.NVarChar, 20).Value = header.SEND_STATUS_MSG;




            try
            {
                cmd.ExecuteNonQuery();
                log.Success("Insert ETC_BILLING_M Success,");
                #region Insert Data
                sqlstr = "insert into ETC_BILLING_D (ETC_BFD_ID,ETC_BFM_ID,HEADER_FLG,STORE_NO,HOST_ID,TXT_DATE,TXT_TIME,SALE_DETAIL_ID,BILL_DATE,AMOUNT,BARCODE1,BARCODE2,BARCODE3,DIFF_FLG,SEQ_NO,BILL_DISPATCH_ID,REC_TYPE) VALUES(:ETC_BFD_ID,:ETC_BFM_ID,:HEADER_FLG,:STORE_NO,:HOST_ID,:TXT_DATE,:TXT_TIME,:SALE_DETAIL_ID,:BILL_DATE,:AMOUNT,:BARCODE1,:BARCODE2,:BARCODE3,:DIFF_FLG,:SEQ_NO,:BILL_DISPATCH_ID,:REC_TYPE)";
                cmd = new OracleCommand(sqlstr, conn, trans);
                cmd.Parameters.Add(":ETC_BFD_ID", OracleType.NVarChar, 32);
                cmd.Parameters.Add(":ETC_BFM_ID", OracleType.NVarChar, 32);
                cmd.Parameters.Add(":HEADER_FLG", OracleType.NVarChar, 1);
                cmd.Parameters.Add(":STORE_NO", OracleType.NVarChar, 5);
                cmd.Parameters.Add(":HOST_ID", OracleType.NVarChar, 14);
                cmd.Parameters.Add(":TXT_DATE", OracleType.NVarChar, 8);
                cmd.Parameters.Add(":TXT_TIME", OracleType.NVarChar, 6);
                cmd.Parameters.Add(":SALE_DETAIL_ID", OracleType.NVarChar, 17);
                cmd.Parameters.Add(":BILL_DATE", OracleType.NVarChar, 20);
                cmd.Parameters.Add(":AMOUNT", OracleType.Number);
                cmd.Parameters.Add(":BARCODE1", OracleType.NVarChar, 20);
                cmd.Parameters.Add(":BARCODE2", OracleType.NVarChar, 16);
                cmd.Parameters.Add(":BARCODE3", OracleType.NVarChar, 15);
                cmd.Parameters.Add(":DIFF_FLG", OracleType.Number);
                cmd.Parameters.Add(":SEQ_NO", OracleType.Number);
                cmd.Parameters.Add(":REC_TYPE", OracleType.NVarChar, 1);
                cmd.Parameters.Add(":BILL_DISPATCH_ID", OracleType.NVarChar, 32);
                foreach (Data data in dataList)
                {
                    cmd.Parameters[":ETC_BFD_ID"].Value = data.ETC_BFD_ID;
                    cmd.Parameters[":ETC_BFM_ID"].Value = header.ETC_BFM_ID;
                    cmd.Parameters[":HEADER_FLG"].Value = data.HEADER_FLG;
                    cmd.Parameters[":STORE_NO"].Value = data.STORE_NO;
                    cmd.Parameters[":HOST_ID"].Value = data.HOST_ID;
                    cmd.Parameters[":TXT_DATE"].Value = data.TXT_DATE;
                    cmd.Parameters[":TXT_TIME"].Value = data.TXT_TIME;
                    cmd.Parameters[":SALE_DETAIL_ID"].Value = data.SALE_DETAIL_ID;
                    cmd.Parameters[":BILL_DATE"].Value = data.BILL_DATE;
                    cmd.Parameters[":AMOUNT"].Value = data.AMOUNT;
                    cmd.Parameters[":BARCODE1"].Value = data.BARCODE1;
                    cmd.Parameters[":BARCODE2"].Value = data.BARCODE2;
                    cmd.Parameters[":BARCODE3"].Value = data.BARCODE3;
                    cmd.Parameters[":DIFF_FLG"].Value = data.DIFF_FLG;
                    cmd.Parameters[":BILL_DISPATCH_ID"].Value = data.BILL_DISPATCH_ID;
                    cmd.Parameters[":SEQ_NO"].Value = data.SEQ_NO;
                    cmd.Parameters[":REC_TYPE"].Value = data.REC_TYPE;
                    cmd.ExecuteNonQuery();
                }
                log.Success(string.Format("Insert BILL_DISPATCH_ID Count : {0} Success,", dataList.Count));
                #endregion

                #region Insert Trailer
                sqlstr = "insert into ETC_BILLING_TR(ETC_BFT_ID,ETC_BFM_ID,HEADER_FLG,TOTAL_RECORD_AMOUNT,TOTAL_AMOUNT) values(:ETC_BFT_ID,:ETC_BFM_ID,:HEADER_FLG,:TOTAL_RECORD_AMOUNT,:TOTAL_AMOUNT)";
                cmd = new OracleCommand(sqlstr, conn, trans);
                cmd.Parameters.Add(":ETC_BFT_ID", OracleType.NVarChar, 32).Value = trailer.ETC_BFT_ID;
                cmd.Parameters.Add(":HEADER_FLG", OracleType.NVarChar, 20).Value = trailer.HEADER_FLG;
                cmd.Parameters.Add(":TOTAL_RECORD_AMOUNT", OracleType.Number).Value = Convert.ToDecimal(trailer.TOTAL_RECORD_AMOUNT);
                cmd.Parameters.Add(":TOTAL_AMOUNT", OracleType.Number).Value = Convert.ToDecimal(trailer.TOTAL_AMOUNT);
                cmd.Parameters.Add(":ETC_BFM_ID", OracleType.NVarChar, 32).Value = header.ETC_BFM_ID;
                cmd.ExecuteNonQuery();
                log.Success("Insert ETC_BILLING_TR Success,");
                #endregion

                #region 資料交接
                sqlstr = "update BILL_DISPATCH set STATUS='1' where bill_type='5' and STATUS='T'";
                cmd = new OracleCommand(sqlstr, conn, trans);
                cmd.ExecuteNonQuery();
                log.Success("Update BILL_DISPATCH STATUS 'T' -> '1' Success,");
                #endregion

                #region ETC_BILLING_M 預壓狀態
                sqlstr = "update ETC_BILLING_M set SEND_STATUS='T' where SEND_STATUS='0'";
                cmd = new OracleCommand(sqlstr, conn, trans);
                cmd.ExecuteNonQuery();
                log.Success("Update ETC_BILLING_M SEND_STATUS '0' -> 'T' Success,");
                #endregion
                log.Success("Insert Table Success,");
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public void ExportFile()
        {
            StreamWriter writer = new StreamWriter(this.file_path + "\\" + this.header.FILE_NAME);

            //Header
            string headerString = "1B02B020000" + DateTime.Now.ToString("yyyyMMdd") + "".PadLeft(82, ' ');

            writer.WriteLine(headerString);

            //Data
            foreach (Data data in dataList)
            {
                string Store = "R" + data.STORE_NO;
                //***20110316
                //string dataString = "2" + Store.PadLeft(20, '0') + data.HOST_ID.PadLeft(14, '0') + data.TXT_DATE.PadLeft(8, '0') + data.SALE_DETAIL_ID.PadLeft(50, '0') + data.TXT_TIME.PadLeft(6, '0') + data.BARCODE1.PadLeft(9, '0') + data.BARCODE2.PadLeft(16, '0') + data.BARCODE3.PadLeft(15, '0') + data.DIFF_FLG.PadLeft(1, '0') + data.SEQ_NO.PadLeft(4, '0') + data.REC_TYPE.PadLeft(3, '0');
                string BARCODE3 = data.BARCODE3.PadLeft(15, '0');
                string dataString = "2" + Store.PadLeft(20, '0') + data.HOST_ID.PadLeft(14, '0') + data.TXT_DATE.PadLeft(8, '0') + "0".PadLeft(50, '0') + data.TXT_TIME.PadLeft(6, '0') + data.BARCODE1.PadLeft(9, '0') + data.BARCODE2.PadLeft(16, '0') + (BARCODE3.Substring(0, 6) + "0000" + BARCODE3.Substring(10, 5)).PadLeft(15, '0') + data.DIFF_FLG.PadLeft(1, '0') + data.SEQ_NO.PadLeft(4, '0') + data.REC_TYPE.PadLeft(3, '0');
                writer.WriteLine(dataString);
            }

            //Trailer
            //***20110316

            //string TrailerString = this.trailer.HEADER_FLG + this.trailer.TOTAL_RECORD_AMOUNT.PadLeft(10, '0') + this.trailer.TOTAL_AMOUNT.PadLeft(14, '0') + "".PadLeft(76, ' ');
            string TrailerString = this.trailer.HEADER_FLG + this.trailer.TOTAL_AMOUNT.PadLeft(14, '0') + this.trailer.TOTAL_RECORD_AMOUNT.PadLeft(10, '0') + "".PadLeft(76, ' ');
            writer.WriteLine(TrailerString);

            writer.Close();

            //***20110316
            //File.Create(this.file_path + "\\" + "FETB02." +DateTime.Now.ToString("yyyyMMdd") +"_01.ok");
            writer = new StreamWriter(this.file_path + "\\" + "FETB02." + DateTime.Now.AddDays(-1).ToString("yyyyMMdd") + "_01.ok");
            writer.WriteLine(this.header.FILE_NAME);
            writer.Close();
        }

        public void Update()
        {
            string sqlstr = "update ETC_BILLING_M set SEND_STATUS = '1',SEND_DTM = SYSDATE where SEND_STATUS = 'T' and FILE_NAME = :FILE_NAME and  BATCH_NO = :BATCH_NO";
            OracleCommand cmd = new OracleCommand(sqlstr, conn, trans);
            cmd.Parameters.Add(":FILE_NAME", OracleType.NVarChar, 12).Value = this.header.FILE_NAME;
            cmd.Parameters.Add(":BATCH_NO", OracleType.NVarChar, 10).Value = this.header.BATCH_NO;
            try
            {
                cmd.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

     

        public void Commit()
        {
            try
            {
                if (trans != null) trans.Commit();
            }
            catch (Exception ex)
            {
                log.Fail(ex.Message);
            }
        }

        public void Rollback()
        {
            try
            {
                if (trans != null) trans.Rollback();
            }
            catch (Exception ex)
            {
                log.Fail(ex.Message);
            }
        }

        public void Close()
        {
            if (conn != null)
            {
                if (conn.State == ConnectionState.Open) conn.Close();
                conn.Dispose();
                OracleConnection.ClearAllPools();
            }
        }

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
            string USER = "ETC_BILLING_NOPAPER";
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
                sb.Append("( SELECT PARA_VALUE FROM SYS_PARA WHERE PARA_KEY='ETC_BILLING_FTP_SERVER' ) AS F_HOST,");
                sb.Append("( select para_value from sys_para where para_key='ETC_BILLING_FTP_USER' ) AS F_USER,");
                sb.Append("( select para_value from sys_para where para_key='ETC_BILLING_FTP_PORT' ) AS F_PORT,");
                sb.Append("( SELECT PARA_VALUE FROM SYS_PARA WHERE PARA_KEY='ETC_BILLING_FTP_PW' ) AS F_PASSWORD ");
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
        #endregion


    }
}
