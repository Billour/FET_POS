using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Advtek.Utility;
using System.Data;
using System.Data.OracleClient;
using System.IO;

namespace SEEDNET_BILLING
{



    public class SEEDNET_BILLING
    {
        #region Header Class
        public class Header
        {
            public string FILE_NAME;
            public string tFILE_NAME;
            public string DATE_OF_FILE;
            public string DATE_OF_FILE1;
            public string ORGAN;
            public string HEADER_FLG;
            public Header()
            {
                string year = (DateTime.Now.Year - 1911).ToString();
                if (year.Length >= 3) year = year.Substring(1);
                DATE_OF_FILE = "010" + year + DateTime.Now.ToString("MMdd");
                DATE_OF_FILE1 = year + DateTime.Now.ToString("MMdd");
                //***20110317 因改抓昨日之前的資料，檔名改取昨日
                //this.FILE_NAME = "Seednet_01_" + DateTime.Now.ToString("yyyyMMdd");
                this.FILE_NAME = "Seednet_01_" + DateTime.Now.AddDays(-1).ToString("yyyyMMdd");
                this.tFILE_NAME = "";
                this.ORGAN = "FET";
                this.HEADER_FLG = "1";
            }
        }
        #endregion

        #region Data Class
        public class Data
        {
            public string SEEDNET_BFD_ID;
            public string SEEDNET_BFM_ID;
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
            public string TXN_DTM1;
            public string EMP_NO;
            public string BILL_DISPATCH_ID;

            #region Static Method
            public static List<Data> getDataList(OracleTransaction objTx)
            {
                List<Data> dataList = new List<Data>();
                OracleConnection conn = objTx.Connection;
                string sqlstr = @"SELECT * FROM (SELECT BD.SEEDNET_BFD_ID,BD.SEEDNET_BFM_ID , BD.HEADER_FLG ,  BARCODE_2,SUBSTR(BILL_DATE,1,4) BILL_DATE, CHECK_DIGITAL,
                AMOUNT, SUBSTR(TRANS_DATE,1,4)||SUBSTR(TXN_DTM,-4,4) TRANS_DATE, '' as ORGAN_ID,'F' as REC_TYPE, REC_STORENO, '' as CHECK_ID, TXN_DTM,BILL_DISPATCH_ID                
                FROM SEEDNET_BILLING_M BM,SEEDNET_BILLING_D BD WHERE BM.SEEDNET_BFM_ID=BD.SEEDNET_BFM_ID AND STATUS='T') ORDER BY TRANS_DATE";
                OracleCommand cmd = new OracleCommand(sqlstr, conn, objTx);
                try
                {
                    OracleDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        Data data = new Data();
                        data.SEEDNET_BFD_ID = dr["SEEDNET_BFD_ID"].ToString();
                        data.SEEDNET_BFM_ID = dr["SEEDNET_BFM_ID"].ToString();
                        data.TXN_DTM = dr["TXN_DTM"].ToString();
                        data.TXN_DTM1 = dr["TRANS_DATE"].ToString();
                        data.HEADER_FLG = dr["HEADER_FLG"].ToString();
                        data.BARCODE_2 = dr["BARCODE_2"].ToString();
                        data.BILL_DATE = dr["BILL_DATE"].ToString();
                        data.AMOUNT = dr["AMOUNT"].ToString();
                        data.CHECK_DIGITAL = dr["CHECK_DIGITAL"].ToString();
                        data.TRANS_DATE = dr["TRANS_DATE"].ToString();
                        data.ORGAN_ID = "";
                        data.REC_TYPE = "F1";
                        if (dr["REC_STORENO"].ToString().Substring(0, 1) != "R")
                            data.REC_STORENO = "R" + dr["REC_STORENO"].ToString();
                        else
                            data.REC_STORENO = dr["REC_STORENO"].ToString();
                        data.CHECK_ID = "";
                        data.EMP_NO = "";
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
            public string SEEDNET_BFT_ID;
            public string SEEDNET_BFM_ID;
            public string HEADER_FLG;
            public string TOTAL_RECORD_AMOUNT;
            public string TOTAL_AMOUNT;

            public Trailer()
            {
                this.SEEDNET_BFT_ID = GuidNo.getUUID();
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

        public SEEDNET_BILLING(ConvertLog log)
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
            string strSQL = "Select PARA_VALUE  from SYS_PARA where PARA_KEY = 'IIS_URL_SEEDNET'";

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
            string strSQL = "Select SEEDNET_BILLFILE_SEQ.nextval  from dual ";
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

        public void Init()
        {
            conn = OracleDBUtil.GetConnection();
            trans = conn.BeginTransaction();
            string sqlstr = "update SEEDNET_BILLING_M set STATUS='T' where STATUS='0'";

            OracleCommand cmd = new OracleCommand(sqlstr, conn, trans);
            cmd.ExecuteNonQuery();
            log.Success("Update SEEDNET_BILLING_M STATUS '0' -> 'T' Success,");
        }

        public void Load()
        {

            header = new Header();
            dataList = Data.getDataList(this.trans);
            log.Success(string.Format("Select Data : {0} 筆,", dataList.Count.ToString()));
            trailer = new Trailer();
            trailer.TOTAL_RECORD_AMOUNT = dataList.Count.ToString();
            trailer.TOTAL_AMOUNT = SUM();
            //this.file_path = Path.Combine(getFile_path(), "Bill_FILES\\SEEDNET\\");
            this.file_path = getFile_path();//***20110323
            if (!Directory.Exists(file_path)) Directory.CreateDirectory(file_path);
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

        public void ExportFile()
        {
            StreamWriter writer = null;
            string sTempDate = "";
            string sFileName = "";
            double dTotal = 0;
            int iRecordCount = 0;
            foreach (Data data in dataList)
            {
                //this.FILE_NAME = "Seednet_01_" + DateTime.Now.AddDays(-1).ToString("yyyyMMdd");
                DateTime Date_Trade = new DateTime(Convert.ToInt32(data.TXN_DTM1.Substring(0, 4)), Convert.ToInt32(data.TXN_DTM1.Substring(4, 2)), Convert.ToInt32(data.TXN_DTM1.Substring(6, 2)));
                string RocY = (Date_Trade.Year - 1911).ToString();
                RocY = RocY.Substring(RocY.Length - 2, 2);
                if (!Date_Trade.ToString("yyyy/MM/dd").Equals(sTempDate))
                {

                    if (writer != null)
                    {
                        //Trailer
                        //string TrailerString = this.trailer.HEADER_FLG + this.trailer.TOTAL_RECORD_AMOUNT.PadLeft(8, '0') + this.trailer.TOTAL_AMOUNT.PadLeft(13, '0');
                        string TrailerString = this.trailer.HEADER_FLG + iRecordCount.ToString().PadLeft(8, '0') + dTotal.ToString().PadLeft(13, '0');
                        writer.WriteLine(TrailerString);
                        writer.Close();
                    }
                    dTotal = 0;
                    iRecordCount = 0;
                    DirectoryInfo di = new DirectoryInfo(this.file_path);
                    sFileName = "Seednet_01_" + Date_Trade.ToString("yyyyMMdd") + ".txt";
                    this.header.tFILE_NAME = sFileName;
                    FileInfo[] fi = di.GetFiles(sFileName + "*", SearchOption.TopDirectoryOnly);
                    if (fi.Length > 0) sFileName += "_" + fi.Length.ToString();
                    writer = new StreamWriter(this.file_path + "\\" + sFileName);

                    //Header
                    string year = (Date_Trade.Year - 1911).ToString();
                    if (year.Length >= 3) year = year.Substring(1);
                    string DATE_OF_FILE = "010" + year + Date_Trade.ToString("MMdd");
                    string headerString = this.header.HEADER_FLG + this.header.ORGAN + "".PadLeft(4, ' ') + DATE_OF_FILE + "".PadLeft(42, ' ');
                    writer.WriteLine(headerString);


                    sTempDate = Date_Trade.ToString("yyyy/MM/dd");
                }
                string sqlstr = "update SEEDNET_BILLING_M set FILE_NAME = '" + this.header.tFILE_NAME + "'  where SEEDNET_BFM_ID =  '" + data.SEEDNET_BFM_ID + "' ";
                OracleCommand cmd = new OracleCommand(sqlstr, conn, trans);
                cmd.ExecuteNonQuery();
                iRecordCount += 1;
                dTotal += Convert.ToDouble(data.AMOUNT);
                ////Data
                //string dataString = data.HEADER_FLG + data.BARCODE_2.PadLeft(16, '0') + data.BILL_DATE.PadLeft(4, '0') + data.AMOUNT.PadLeft(9, '0') + data.CHECK_DIGITAL.PadLeft(2, '0') + (header.DATE_OF_FILE1.Substring(0, 2) + Date_Trade.ToString("MMdd")).PadLeft(6, '0') + "".PadLeft(4, ' ') + data.REC_TYPE + data.REC_STORENO.PadLeft(6, ' ') + "   " + data.TXN_DTM.PadLeft(6, '0');
                string dataString = data.HEADER_FLG + data.BARCODE_2.PadLeft(16, '0') + data.BILL_DATE.PadLeft(4, '0') + data.AMOUNT.PadLeft(9, '0') + data.CHECK_DIGITAL.PadLeft(2, '0') + (RocY + Date_Trade.ToString("MMdd")).PadLeft(6, '0') + "".PadLeft(4, ' ') + data.REC_TYPE + data.REC_STORENO.PadLeft(6, ' ') + "   " + data.TXN_DTM.PadLeft(6, '0');
                writer.WriteLine(dataString);
            }

            if (writer != null)
            {
                //Trailer
                //string TrailerString = this.trailer.HEADER_FLG + this.trailer.TOTAL_RECORD_AMOUNT.PadLeft(8, '0') + this.trailer.TOTAL_AMOUNT.PadLeft(13, '0');
                string TrailerString = this.trailer.HEADER_FLG + iRecordCount.ToString().PadLeft(8, '0') + dTotal.ToString().PadLeft(13, '0');
                writer.WriteLine(TrailerString);
                writer.Close();
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
            string USER = "SEEDNET_BILLING";
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
                sb.Append("( SELECT PARA_VALUE FROM SYS_PARA WHERE PARA_KEY='SEEDNET_BILLING_FTP_SERVER' ) AS F_HOST,");
                sb.Append("( select para_value from sys_para where para_key='SEEDNET_BILLING_FTP_USER' ) AS F_USER,");
                sb.Append("( select para_value from sys_para where para_key='SEEDNET_BILLING_FTP_PORT' ) AS F_PORT,");
                sb.Append("( SELECT PARA_VALUE FROM SYS_PARA WHERE PARA_KEY='SEEDNET_BILLING_FTP_PW' ) AS F_PASSWORD ");
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

        public void Update()
        {
            string sqlstr = "update SEEDNET_BILLING_M set STATUS = '1' where STATUS = 'T' ";
            OracleCommand cmd = new OracleCommand(sqlstr, conn, trans);
            try
            {
                cmd.ExecuteNonQuery();
                log.Success("Update SEEDNET_BILLING_M STATUS 'T' -> '1' Success,");
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
