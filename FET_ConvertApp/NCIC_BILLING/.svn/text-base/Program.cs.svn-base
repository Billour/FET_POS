using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Advtek.Utility;
using System.Data;
using System.Data.OracleClient;
using System.IO;

namespace NCIC_BILLING
{
    class Program
    {
        static void Main(string[] args)
        {


            //2.初始化LOG
            ConvertLog log = new ConvertLog("NCIC_BILLING");
            NCIC_BILLING nb = new NCIC_BILLING(log);
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
                log.Success("NCIC_BILLING 處理完成");
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


    public class NCIC_BILLING
    {
        #region Header Class
        public class Header
        {

            public string NCIC_BFM_ID;
            public string FILE_NAME;
            public string FILE_LOG;//***20110311
            public string DATE_OF_FILE;
            public string BATCH_NO;
            public string ORGAN;
            public string HEADER_FLG;
            public string SEND_STATUS;
            public string SEND_DTM;
            public string SEND_STATUS_MSG;


            public Header()
            {
                this.NCIC_BFM_ID = GuidNo.getUUID();
                int year = DateTime.Now.Year - 1911;
                DATE_OF_FILE = year.ToString("000").Substring(1) + DateTime.Now.ToString("MMdd");
                //***20110317 因改抓昨日之前的資料，檔名改取昨日
                //this.FILE_NAME = "FET010." + DateTime.Now.ToString("yyyyMMdd");
                this.FILE_NAME = "FetToSparq_" + DateTime.Now.ToString("yyyyMMddHHmmss")+".txt";
                this.FILE_LOG = "FetToSparq_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".log";
                //this.FILE_NAME = "FetToSparq_" + DateTime.Now.AddDays(-1).ToString("yyyyMMddHHmmss") + ".txt";
                //this.FILE_LOG = "FetToSparq_" + DateTime.Now.AddDays(-1).ToString("yyyyMMddHHmmss") + ".log";
                this.BATCH_NO = NCIC_BILLING.SEQNO();
                this.SEND_STATUS = "0";
                this.SEND_STATUS_MSG = "";
                this.ORGAN = "FET001"; //***20110311
                this.HEADER_FLG = "1";
            }



        }
        #endregion

        #region Data Class
        public class Data
        {
            public string NCIC_BFD_ID;
            public string NCIC_BFM_ID;
            public string HEADER_FLG;
            public string BARCODE_2;
            public string BILL_DATE;
            public string AMOUNT;
            public string CHECK_DIGITAL;
            public string TRANS_DATE;
            public string ORGAN_ID;
            public string REC_TYPE;
            public string REC_STORENO;
            public string CHECK_ID;
            public string TXN_DTM;
            public string EMP_NO;
            public string BILL_DISPATCH_ID;
            public string BARCODE_3; //**20110311
            

            public Data()
            {
                this.NCIC_BFD_ID = GuidNo.getUUID();
                this.HEADER_FLG = "2";

            }

            #region Static Method
            public static List<Data> getDataList(OracleTransaction trans)
            {
                List<Data> dataList = new List<Data>();
                OracleConnection conn = trans.Connection;
                string sqlstr = @"select POS_UUID() as NCIC_BFD_ID ,'2' as HEADER_FLG ,SD.BARCODE2 As BARCODE_2,Substr(SD.barcode1,1,6) As BILL_DATE,
                                   Substr(SD.barcode3,5,2)  As CHECK_DIGITAL,BD.AMOUNT as AMOUNT,SH.TRADE_DATE as TRANS_DATE, '' as ORGAN_ID,
                                   'F' as REC_TYPE,SH.STORE_NO  as REC_STORENO, '' as CHECK_ID,
                                   sh.trade_date as TXN_DTM,
                                   Bd.BILL_DISPATCH_ID ,
                                   Bd.PAY_MODE_ID ,Bd.PAY_MODE_NAME
                                  ,BARCODE3 as BARCODE_3
                                  from BILL_DISPATCH bd,sale_head sh,sale_detail sd where bd.POSUUID_MASTER = sh.posuuid_master and bd.SALE_DETAIL_ID = sd.id and bd.bill_type='6' and bd.status='T' and  bd.BILL_TX_TYPE='1' order by SH.STORE_NO ";

                OracleCommand cmd = new OracleCommand(sqlstr, conn, trans);

                try
                {

                    OracleDataReader dr = cmd.ExecuteReader();

                    while (dr.Read())
                    {
                        Data data = new Data();
                        data.NCIC_BFD_ID = dr["NCIC_BFD_ID"].ToString();
                        data.HEADER_FLG = "2";
                        data.BARCODE_2 = dr["BARCODE_2"].ToString();
                        data.BILL_DATE = dr["BILL_DATE"].ToString();
                        data.AMOUNT = dr["AMOUNT"].ToString();
                        data.CHECK_DIGITAL = dr["CHECK_DIGITAL"].ToString();
                        //data.TRANS_DATE = dr["TRANS_DATE"].ToString();***20110323
                        data.TRANS_DATE = ((DateTime)dr["TRANS_DATE"]).ToString("yyyyMMdd");
                        data.ORGAN_ID = "";
                        //data.REC_TYPE = "F";
                        data.REC_TYPE = "1"; //***20110316 
                        data.REC_STORENO = dr["REC_STORENO"].ToString();
                        data.CHECK_ID = "";
                        string year = (Convert.ToDateTime(dr["TXN_DTM"]).Year - 1911).ToString();
                        data.TXN_DTM = year.Substring(1) + Convert.ToDateTime(dr["TXN_DTM"]).ToString("MMdd");
                        data.EMP_NO = "";
                        data.BILL_DISPATCH_ID = dr["BILL_DISPATCH_ID"].ToString();
                        data.BARCODE_3 = dr["BARCODE_3"].ToString(); //***20110311
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
            public string NCIC_BFT_ID;
            public string NCIC_BFM_ID;
            public string HEADER_FLG;
            public string TOTAL_RECORD_AMOUNT;
            public string TOTAL_AMOUNT;

            public Trailer()
            {
                this.NCIC_BFT_ID = GuidNo.getUUID();
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
        public NCIC_BILLING(ConvertLog log)
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
            string strSQL = "Select PARA_VALUE  from SYS_PARA where PARA_KEY = 'IIS_URL_NCIC'";//***20110323

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
            string strSQL = "Select NCIC_BILLFILE_SEQ.nextval  from dual ";
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
            //this.file_path = Path.Combine(getFile_path(), "Bill_FILES\\NCIC\\");
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
            //***20110323 * in => exists
            //***20110317 + POSUUID_MASTER IN (SELECT POSUUID_MASTER FROM SALE_HEAD WHERE TRUNC(TRADE_DATE)<=TRUNC(SYSDATE-1)) 取昨天之前的資料
            //string sqlstr = "update BILL_DISPATCH set STATUS='T' where bill_type='6' and STATUS='0' and BILL_TX_TYPE='1'";
            //string sqlstr = "update BILL_DISPATCH set STATUS='T' where bill_type='6' and STATUS='0' and BILL_TX_TYPE='1' AND POSUUID_MASTER IN (SELECT POSUUID_MASTER FROM SALE_HEAD WHERE TRUNC(TRADE_DATE)<=TRUNC(SYSDATE-1))";
            string sqlstr = "update BILL_DISPATCH set STATUS='T' where bill_type='6' and STATUS='0' and BILL_TX_TYPE='1' AND exists  (SELECT 'X' FROM SALE_HEAD WHERE posuuid_master=bill_dispatch.posuuid_master and   TRUNC(TRADE_DATE)<=TRUNC(SYSDATE-1))";
            OracleCommand cmd = new OracleCommand(sqlstr, conn, trans);
            cmd.ExecuteNonQuery();
            log.Success("Update BILL_DISPATCH STATUS '0' -> 'T' Success,");
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
            string USER = "NCIC_BILLING";
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

        public void Insert()
        {
            string sqlstr = "insert into NCIC_BILLING_M(NCIC_BFM_ID,HEADER_FLG,ORGAN,BATCH_NO,DATE_OF_FILE,SEND_DTM,SEND_STATUS,SEND_STATUS_MSG,FILE_NAME) values(:NCIC_BFM_ID,:HEADER_FLG,:ORGAN,:BATCH_NO,:DATE_OF_FILE,SYSDATE,:SEND_STATUS,:SEND_STATUS_MSG,:FILE_NAME)";
            OracleCommand cmd = new OracleCommand(sqlstr, conn, trans);
            cmd.Parameters.Add(":NCIC_BFM_ID", OracleType.NVarChar, 32).Value = header.NCIC_BFM_ID;
            cmd.Parameters.Add(":HEADER_FLG", OracleType.NVarChar, 12).Value = header.HEADER_FLG;
            cmd.Parameters.Add(":ORGAN", OracleType.NVarChar, 8).Value = header.ORGAN;
            cmd.Parameters.Add(":BATCH_NO", OracleType.NVarChar, 11).Value = header.BATCH_NO.PadLeft(11, '0');
            cmd.Parameters.Add(":DATE_OF_FILE", OracleType.NVarChar, 10).Value = header.DATE_OF_FILE;
            cmd.Parameters.Add(":SEND_STATUS", OracleType.NVarChar, 20).Value = header.SEND_STATUS;
            cmd.Parameters.Add(":SEND_STATUS_MSG", OracleType.NVarChar, 20).Value = header.SEND_STATUS_MSG;
            cmd.Parameters.Add(":FILE_NAME", OracleType.NVarChar, 10).Value = header.FILE_NAME;


            try
            {
                cmd.ExecuteNonQuery();
                log.Success("Insert NCIC_BILLING_M Success,");
                #region Insert Data
                sqlstr = "insert into NCIC_BILLING_D (NCIC_BFD_ID,NCIC_BFM_ID,HEADER_FLG,BARCODE2,BILL_DATE,AMOUNT,CHECK_DIGITAL,TRANS_DATE,ORGAN_ID,REC_TYPE,REC_STORENO,CHECK_ID,TXN_DTM,EMP_NO,BILL_DISPATCH_ID) VALUES(:NCIC_BFD_ID,:NCIC_BFM_ID,:HEADER_FLG,:BARCODE2,:BILL_DATE,:AMOUNT,:CHECK_DIGITAL,:TRANS_DATE,:ORGAN_ID,:REC_TYPE,:REC_STORENO,:CHECK_ID,:TXN_DTM,:EMP_NO,:BILL_DISPATCH_ID)";
                cmd = new OracleCommand(sqlstr, conn, trans);
                cmd.Parameters.Add(":NCIC_BFD_ID", OracleType.NVarChar, 32);
                cmd.Parameters.Add(":NCIC_BFM_ID", OracleType.NVarChar, 32);
                cmd.Parameters.Add(":HEADER_FLG", OracleType.NVarChar, 1);
                cmd.Parameters.Add(":BARCODE2", OracleType.NVarChar, 17);
                cmd.Parameters.Add(":BILL_DATE", OracleType.NVarChar, 20);
                cmd.Parameters.Add(":AMOUNT", OracleType.Number);
                cmd.Parameters.Add(":CHECK_DIGITAL", OracleType.NVarChar, 2);
                cmd.Parameters.Add(":TRANS_DATE", OracleType.NVarChar, 6);
                cmd.Parameters.Add(":ORGAN_ID", OracleType.NVarChar, 4);
                cmd.Parameters.Add(":REC_TYPE", OracleType.NVarChar, 1);
                cmd.Parameters.Add(":REC_STORENO", OracleType.NVarChar, 7);
                cmd.Parameters.Add(":CHECK_ID", OracleType.NVarChar, 1);
                cmd.Parameters.Add(":TXN_DTM", OracleType.NVarChar );
                cmd.Parameters.Add(":EMP_NO", OracleType.NVarChar, 20);
                cmd.Parameters.Add(":BILL_DISPATCH_ID", OracleType.NVarChar, 32);
                foreach (Data data in dataList)
                {
                    cmd.Parameters[":NCIC_BFD_ID"].Value = data.NCIC_BFD_ID;
                    cmd.Parameters[":NCIC_BFM_ID"].Value = header.NCIC_BFM_ID;
                    cmd.Parameters[":HEADER_FLG"].Value = data.HEADER_FLG;
                    cmd.Parameters[":BARCODE2"].Value = data.BARCODE_2;
                    cmd.Parameters[":BILL_DATE"].Value = data.BILL_DATE;
                    cmd.Parameters[":AMOUNT"].Value = OracleNumber.Parse(data.AMOUNT);
                    cmd.Parameters[":CHECK_DIGITAL"].Value = data.CHECK_DIGITAL;
                    cmd.Parameters[":TRANS_DATE"].Value = data.TRANS_DATE;
                    cmd.Parameters[":ORGAN_ID"].Value = data.ORGAN_ID;
                    cmd.Parameters[":REC_TYPE"].Value = data.REC_TYPE;
                    cmd.Parameters[":REC_STORENO"].Value = data.REC_STORENO;
                    cmd.Parameters[":CHECK_ID"].Value = data.CHECK_ID;
                    cmd.Parameters[":TXN_DTM"].Value = data.TXN_DTM;
                    cmd.Parameters[":EMP_NO"].Value = data.EMP_NO;
                    cmd.Parameters[":BILL_DISPATCH_ID"].Value = data.BILL_DISPATCH_ID;
                    cmd.ExecuteNonQuery();
                }
                log.Success(string.Format("Insert SEEDNET_BILLING_D Count : {0} Success,", dataList.Count));
                #endregion

                #region Insert Trailer
                sqlstr = "insert into NCIC_BILLING_TR(NCIC_BFT_ID,HEADER_FLG,TOTAL_RECORD_AMOUNT,TOTAL_AMOUNT,NCIC_BFM_ID) values(:NCIC_BFT_ID,:HEADER_FLG,:TOTAL_RECORD_AMOUNT,:TOTAL_AMOUNT,:NCIC_BFM_ID)";
                cmd = new OracleCommand(sqlstr, conn, trans);
                cmd.Parameters.Add(":NCIC_BFT_ID", OracleType.NVarChar, 32).Value = trailer.NCIC_BFT_ID;
                cmd.Parameters.Add(":HEADER_FLG", OracleType.NVarChar, 20).Value = trailer.HEADER_FLG;
                cmd.Parameters.Add(":TOTAL_RECORD_AMOUNT", OracleType.Number).Value = Convert.ToDecimal(trailer.TOTAL_RECORD_AMOUNT);
                cmd.Parameters.Add(":TOTAL_AMOUNT", OracleType.Number).Value = Convert.ToDecimal(trailer.TOTAL_AMOUNT);
                cmd.Parameters.Add(":NCIC_BFM_ID", OracleType.NVarChar, 32).Value = header.NCIC_BFM_ID;
                cmd.ExecuteNonQuery();
                log.Success("Insert SEEDNET_BILLING_TR Success,");
                #endregion

                #region 資料交接
                sqlstr = "update BILL_DISPATCH set STATUS='1' where bill_type='6' and STATUS='T'";
                cmd = new OracleCommand(sqlstr, conn, trans);
                cmd.ExecuteNonQuery();
                log.Success("Update BILL_DISPATCH STATUS 'T' -> '1' Success,");
                #endregion

                #region NCIC_BILLING_M 預壓狀態
                sqlstr = "update NCIC_BILLING_M set SEND_STATUS='T' where SEND_STATUS='0'";
                cmd = new OracleCommand(sqlstr, conn, trans);
                cmd.ExecuteNonQuery();
                log.Success("Update NCIC_BILLING_M SEND_STATUS '0' -> 'T' Success,");
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

            //Header ***20110311
            //(SAMPLE DATA)
            //(1    FET001000225                                          )
            string headerString = this.header.HEADER_FLG.PadRight(5,' ') + this.header.ORGAN + this.header.DATE_OF_FILE.PadRight(48,' ');

            writer.WriteLine(headerString);

            //Data
            foreach (Data data in dataList)
            {
                DateTime Date_Trade = new DateTime(Convert.ToInt32(data.TRANS_DATE.Substring(0, 4)), Convert.ToInt32(data.TRANS_DATE.Substring(4, 2)), Convert.ToInt32(data.TRANS_DATE.Substring(6, 2)));
                string RocY = (Date_Trade.Year - 1911).ToString();
                RocY = RocY.Substring(RocY.Length - 2, 2);
                //(SAMPLE DATA)
                //(22100210100522048000200000027123000225    1  R2101   000224)
                string dataString = data.HEADER_FLG + "210021" + data.BARCODE_2.Substring(6, 10).PadLeft(10, ' ') + data.BILL_DATE.Substring(0, 4) + Convert.ToDecimal(data.AMOUNT).ToString("000000000") + data.BARCODE_3.Substring(4, 2).PadLeft(2, ' ') + header.DATE_OF_FILE + "".PadLeft(4, ' ') + data.REC_TYPE + "".PadLeft(2, ' ') + ((data.REC_STORENO.Length == 4) ? ("R" + data.REC_STORENO) : data.REC_STORENO).PadLeft(5,' ') + "".PadLeft(3, ' ') + data.TXN_DTM;
                //string dataString = data.HEADER_FLG + "210021" + data.BARCODE_2.Substring(6, 10).PadLeft(10, ' ') + data.BILL_DATE.Substring(0, 4) + Convert.ToDecimal(data.AMOUNT).ToString("000000000") + data.BARCODE_3.Substring(4, 2).PadLeft(2, ' ') + (RocY + Date_Trade.ToString("MMdd")) + "".PadLeft(4, ' ') + data.REC_TYPE + "".PadLeft(2, ' ') + ((data.REC_STORENO.Length == 4) ? ("R" + data.REC_STORENO) : data.REC_STORENO).PadLeft(5, ' ') + "".PadLeft(3, ' ') + data.TXN_DTM;

                writer.WriteLine(dataString);
            }

            //Trailer
            //(SAMPLE DATA)
            //(3000000250000000055787                                     )
            string TrailerString = this.trailer.HEADER_FLG + this.trailer.TOTAL_RECORD_AMOUNT.PadLeft(8, '0') + this.trailer.TOTAL_AMOUNT.PadLeft(13, '0') + "".PadLeft(37,' ');
            writer.WriteLine(TrailerString);

            writer.Close();

            //***20110311 log file
            writer = new StreamWriter(this.file_path + "\\" + this.header.FILE_LOG);
            string LineString = this.header.FILE_LOG;
            writer.WriteLine(LineString);
            LineString = "1".PadLeft(5, ' ') + this.trailer.TOTAL_RECORD_AMOUNT.ToString().PadLeft(9, ' ') + this.trailer.TOTAL_AMOUNT.ToString().PadLeft(11, ' ');
            writer.WriteLine(LineString);
            LineString = "2".PadLeft(5, ' ') + 0.ToString().PadLeft(9, ' ') + 0.ToString().PadLeft(11, ' ');
            writer.WriteLine(LineString);
            LineString = "total".PadLeft(5, ' ') + this.trailer.TOTAL_RECORD_AMOUNT.ToString().PadLeft(9, ' ') + this.trailer.TOTAL_AMOUNT.ToString().PadLeft(11, ' ');
            writer.WriteLine(LineString);
            writer.Close();
            writer.Dispose();
        }

        public void Update()
        {
            string sqlstr = "update SEEDNET_BILLING_M set SEND_STATUS = '1',SEND_DTM = SYSDATE where SEND_STATUS = 'T' and FILE_NAME = :FILE_NAME and  BATCH_NO = :BATCH_NO";
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
        #endregion


    }
}
