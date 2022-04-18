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
    public class Instant_SEEDNET_BILLING
    {
        public string DOMain(string UUID)
        {
            //2.初始化LOG
            ConvertLog log = new ConvertLog("SEEDNET_BILLING");
            SEEDNET_BILLING sb = new SEEDNET_BILLING(log);
            try
            {

                sb.Init(UUID);
                sb.Load(UUID); //讀取資料
                sb.Insert(); //新增資料
                sb.ProduceFile();//產生實體檔案
                sb.Update(UUID);
                sb.Commit();
                log.Success("SEEDNET_BILLING 處理完成");
            }
            catch (Exception ex)
            {
                sb.Rollback();
                Console.WriteLine(ex.Message);
                Console.ReadKey();
                log.Fail(ex.Message);
            }
            finally
            {
                sb.Close();
            }
            return sb.header.str_xmml;
        }
    }


    public class SEEDNET_BILLING
    {
        #region Header Class
        public class Header
        {

            public string SEEDNET_BFM_ID;
            public string FILE_NAME;
            public string XML_NAME;
            public string DATE_OF_FILE;
            public string DATE_OF_FILE1;
            public string BATCH_NO;
            public string ORGAN;
            public string HEADER_FLG;
            public string SEND_STATUS;
            public string STATUS;
            public string SEND_DTM;
            public string SEND_STATUS_MSG;
            public string str_xmml;

            public Header()
            {
                this.SEEDNET_BFM_ID = GuidNo.getUUID();
                string year = (DateTime.Now.Year - 1911).ToString();
                if (year.Length >= 3) year = year.Substring(1);
                DATE_OF_FILE = "010" + year + DateTime.Now.ToString("MMdd");
                DATE_OF_FILE1 = year + DateTime.Now.ToString("MMdd");
                //***20110317 因改抓昨日之前的資料，檔名改取昨日
                //this.FILE_NAME = "Seednet_01_" + DateTime.Now.ToString("yyyyMMdd");
                this.FILE_NAME = "Seednet_01_" + DateTime.Now.AddDays(-1).ToString("yyyyMMdd");
                this.XML_NAME = "Seednet_01_" + DateTime.Now.AddDays(-1).ToString("yyyyMMdd") + ".xml";
                this.BATCH_NO = SEEDNET_BILLING.SEQNO();
                this.SEND_STATUS = "0";
                this.STATUS = "0";
                this.SEND_STATUS_MSG = "";
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

            public Data()
            {
                this.SEEDNET_BFD_ID = GuidNo.getUUID();
                this.HEADER_FLG = "2";

            }

            #region Static Method
            public static List<Data> getDataList(OracleTransaction objTx, string UUID)
            {
                List<Data> dataList = new List<Data>();
                OracleConnection conn = objTx.Connection;
                //***20110317 + ORDER BY Sh.TRADE_DATE for 產生不同交易日的檔案
                string sqlstr = @"select POS_UUID() as SEEDNET_BFD_ID ,''  as SEEDNET_BFM_ID ,'2' as HEADER_FLG ,
                SD.BARCODE2 As BARCODE_2,Substr(SD.barcode1,0,4) As BILL_DATE,Substr(SD.barcode3,5,2)  As CHECK_DIGITAL,
                BD.AMOUNT as AMOUNT,SH.TRADE_DATE as TRANS_DATE, '' as ORGAN_ID,'F' as REC_TYPE,SH.STORE_NO  as REC_STORENO, '' as CHECK_ID,
                Sh.TRADE_DATE  as TXN_DTM,Bd.BILL_DISPATCH_ID ,Bd.PAY_MODE_ID ,Bd.PAY_MODE_NAME 
                from BILL_DISPATCH bd,sale_head sh,sale_detail sd 
                where bd.POSUUID_MASTER = sh.posuuid_master and bd.SALE_DETAIL_ID = sd.id and bd.bill_type='3' and bd.status='T' and  bd.BILL_TX_TYPE='1' ";

                if (UUID != "")
                {
                    sqlstr += " and sh.POSUUID_MASTER='" + UUID + "'";
                }
                sqlstr += " ORDER BY Sh.TRADE_DATE,SH.STORE_NO";
                OracleCommand cmd = new OracleCommand(sqlstr, conn, objTx);

                try
                {

                    OracleDataReader dr = cmd.ExecuteReader();

                    while (dr.Read())
                    {
                        Data data = new Data();
                        data.SEEDNET_BFD_ID = dr["SEEDNET_BFD_ID"].ToString();
                        string year = (((DateTime)dr["TXN_DTM"]).Year - 1911).ToString();
                        if (year.Length >= 3) year = year.Substring(1);
                        data.TXN_DTM = year + ((DateTime)dr["TXN_DTM"]).ToString("MMdd");

                        data.TXN_DTM1 = ((DateTime)dr["TXN_DTM"]).ToString("yyyyMMdd");
                        data.HEADER_FLG = "2";
                        data.BARCODE_2 = dr["BARCODE_2"].ToString();
                        //data.BILL_DATE = dr["BILL_DATE"].ToString();
                        data.BILL_DATE = dr["BILL_DATE"].ToString();
                        data.AMOUNT = dr["AMOUNT"].ToString();
                        data.CHECK_DIGITAL = dr["CHECK_DIGITAL"].ToString();
                        data.TRANS_DATE = dr["TRANS_DATE"].ToString();
                        data.ORGAN_ID = "";
                        data.REC_TYPE = "F1";
                        data.REC_STORENO = "R" + dr["REC_STORENO"].ToString();
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

        public void Init(string UUID)
        {
            conn = OracleDBUtil.GetConnection();
            trans = conn.BeginTransaction();
            string sqlstr = "update BILL_DISPATCH set STATUS='T' where bill_type='3' and STATUS='0' and BILL_TX_TYPE='1' ";
            if (UUID != "")
            {
                sqlstr += " and POSUUID_MASTER='" + UUID + "'";
            }
            OracleCommand cmd = new OracleCommand(sqlstr, conn, trans);
            cmd.ExecuteNonQuery();
            log.Success("Update BILL_DISPATCH STATUS '0' -> 'T' Success,");
        }

        public void Load(string UUID)
        {

            header = new Header();
            dataList = Data.getDataList(this.trans, UUID);
            log.Success(string.Format("Select Data : {0} 筆,", dataList.Count.ToString()));
            trailer = new Trailer();
            trailer.TOTAL_RECORD_AMOUNT = dataList.Count.ToString();
            trailer.TOTAL_AMOUNT = SUM();
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

        public void Insert()
        {
            string sqlstr = "insert into SEEDNET_BILLING_M(SEEDNET_BFM_ID,HEADER_FLG,ORGAN,BATCH_NO,DATE_OF_FILE,SEND_DTM,SEND_STATUS,SEND_STATUS_MSG,FILE_NAME,STATUS) values(:SEEDNET_BFM_ID,:HEADER_FLG,:ORGAN,:BATCH_NO,:DATE_OF_FILE,SYSDATE,:SEND_STATUS,:SEND_STATUS_MSG,:FILE_NAME,:STATUS)";
            OracleCommand cmd = new OracleCommand(sqlstr, conn, trans);
            cmd.Parameters.Add(":SEEDNET_BFM_ID", OracleType.NVarChar, 32).Value = header.SEEDNET_BFM_ID;
            cmd.Parameters.Add(":HEADER_FLG", OracleType.NVarChar, 1).Value = header.HEADER_FLG;
            cmd.Parameters.Add(":ORGAN", OracleType.NVarChar, 8).Value = header.ORGAN;
            cmd.Parameters.Add(":BATCH_NO", OracleType.NVarChar, 11).Value = header.BATCH_NO.PadLeft(11, '0');
            cmd.Parameters.Add(":DATE_OF_FILE", OracleType.NVarChar, 8).Value = header.DATE_OF_FILE;
            cmd.Parameters.Add(":SEND_STATUS", OracleType.NVarChar, 20).Value = header.SEND_STATUS;
            cmd.Parameters.Add(":STATUS", OracleType.NVarChar, 20).Value = header.STATUS;
            cmd.Parameters.Add(":SEND_STATUS_MSG", OracleType.NVarChar, 20).Value = header.SEND_STATUS_MSG;
            cmd.Parameters.Add(":FILE_NAME", OracleType.NVarChar, 10).Value = " ";


            try
            {
                cmd.ExecuteNonQuery();
                log.Success("Insert SEEDNET_BILLING_M Success,");
                #region Insert Data
                sqlstr = "insert into SEEDNET_BILLING_D (SEEDNET_BFD_ID,SEEDNET_BFM_ID,HEADER_FLG,BARCODE_2,BILL_DATE,AMOUNT,CHECK_DIGITAL,TRANS_DATE,ORGAN_ID,REC_TYPE,REC_STORENO,CHECK_ID,TXN_DTM,EMP_NO,BILL_DISPATCH_ID) VALUES(:SEEDNET_BFD_ID,:SEEDNET_BFM_ID,:HEADER_FLG,:BARCODE_2,:BILL_DATE,:AMOUNT,:CHECK_DIGITAL,:TRANS_DATE,:ORGAN_ID,:REC_TYPE,:REC_STORENO,:CHECK_ID,:TXN_DTM,:EMP_NO,:BILL_DISPATCH_ID)";
                cmd = new OracleCommand(sqlstr, conn, trans);
                cmd.Parameters.Add(":SEEDNET_BFD_ID", OracleType.NVarChar, 32);
                cmd.Parameters.Add(":SEEDNET_BFM_ID", OracleType.NVarChar, 32);
                cmd.Parameters.Add(":HEADER_FLG", OracleType.NVarChar, 1);
                cmd.Parameters.Add(":BARCODE_2", OracleType.NVarChar, 17);
                cmd.Parameters.Add(":BILL_DATE", OracleType.NVarChar, 20);
                cmd.Parameters.Add(":AMOUNT", OracleType.Number);
                cmd.Parameters.Add(":CHECK_DIGITAL", OracleType.NVarChar, 2);
                cmd.Parameters.Add(":TRANS_DATE", OracleType.NVarChar, 6);
                cmd.Parameters.Add(":ORGAN_ID", OracleType.NVarChar, 4);
                cmd.Parameters.Add(":REC_TYPE", OracleType.NVarChar, 1);
                cmd.Parameters.Add(":REC_STORENO", OracleType.NVarChar, 7);
                cmd.Parameters.Add(":CHECK_ID", OracleType.NVarChar, 1);
                cmd.Parameters.Add(":TXN_DTM", OracleType.NVarChar, 6);
                cmd.Parameters.Add(":EMP_NO", OracleType.NVarChar, 20);
                cmd.Parameters.Add(":BILL_DISPATCH_ID", OracleType.NVarChar, 32);
                foreach (Data data in dataList)
                {
                    cmd.Parameters[":SEEDNET_BFD_ID"].Value = data.SEEDNET_BFD_ID;
                    cmd.Parameters[":SEEDNET_BFM_ID"].Value = header.SEEDNET_BFM_ID;
                    cmd.Parameters[":HEADER_FLG"].Value = data.HEADER_FLG;
                    cmd.Parameters[":BARCODE_2"].Value = data.BARCODE_2;
                    cmd.Parameters[":BILL_DATE"].Value = data.BILL_DATE;
                    cmd.Parameters[":AMOUNT"].Value = Convert.ToDecimal(data.AMOUNT);
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
                sqlstr = "insert into SEEDNET_BILLING_TR(SEEDNET_BFT_ID,HEADER_FLG,TOTAL_RECORD_AMOUNT,TOTAL_AMOUNT,SEEDNET_BFM_ID) values(:SEEDNET_BFT_ID,:HEADER_FLG,:TOTAL_RECORD_AMOUNT,:TOTAL_AMOUNT,:SEEDNET_BFM_ID)";
                cmd = new OracleCommand(sqlstr, conn, trans);
                cmd.Parameters.Add(":SEEDNET_BFT_ID", OracleType.NVarChar, 32).Value = trailer.SEEDNET_BFT_ID;
                cmd.Parameters.Add(":HEADER_FLG", OracleType.NVarChar, 20).Value = trailer.HEADER_FLG;
                cmd.Parameters.Add(":TOTAL_RECORD_AMOUNT", OracleType.Number).Value = Convert.ToDecimal(trailer.TOTAL_RECORD_AMOUNT);
                cmd.Parameters.Add(":TOTAL_AMOUNT", OracleType.Number).Value = Convert.ToDecimal(trailer.TOTAL_AMOUNT);
                cmd.Parameters.Add(":SEEDNET_BFM_ID", OracleType.NVarChar, 32).Value = header.SEEDNET_BFM_ID;
                cmd.ExecuteNonQuery();
                log.Success("Insert SEEDNET_BILLING_TR Success,");
                #endregion

                //if (Batch == "Y")
                //{
                //    #region 資料交接
                //    sqlstr = "update BILL_DISPATCH set STATUS='1' where bill_type='3'";
                //    cmd = new OracleCommand(sqlstr, conn, trans);
                //    cmd.ExecuteNonQuery();
                //    log.Success("Update BILL_DISPATCH STATUS 'T' -> '1' Success,");
                //    #endregion
                //}


                //#region SEEDNET_BILLING_M 預壓狀態
                //sqlstr = "update SEEDNET_BILLING_M set SEND_STATUS='T' where SEND_STATUS='0'";
                //cmd = new OracleCommand(sqlstr, conn, trans);
                //cmd.ExecuteNonQuery();
                //log.Success("Update SEEDNET_BILLING_M SEND_STATUS '0' -> 'T' Success,");
                //#endregion

                log.Success("Insert Table Success,");
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }        

        public void ProduceFile()
        {
            this.header.str_xmml = "<?xml version=\"1.0\" encoding=\"big5\"?>";
            int i = 0;
            foreach (Data data in dataList)
            {
                i += 1;
                this.header.str_xmml += "<PaySvcRs>";
                this.header.str_xmml += "<StatusCode>00</StatusCode>";
                this.header.str_xmml += "<StatusDesc>Success</StatusDesc>";
                this.header.str_xmml += "<PR_Key1>" + data.BARCODE_2 + "</PR_Key1>";
                this.header.str_xmml += "<TxnSeq>" + i.ToString("00000000") + "</TxnSeq>";
                this.header.str_xmml += "<FileDate>" + data.TXN_DTM1 + "</FileDate>";
                this.header.str_xmml += "<TxnDate>" + DateTime.Now.ToString("yyyyMMddhhmmss") + "</TxnDate>";
                this.header.str_xmml += "<TxnAmt>" + data.AMOUNT + "</TxnAmt>";
                this.header.str_xmml += "<RecvType>FET</ RecvType >";
                this.header.str_xmml += "</PaySvcRs>";
            }

        }

        public void Update(string UUID)
        {           
            #region 資料交接
            string sqlstr = "update BILL_DISPATCH set STATUS='1' where bill_type='3' AND STATUS='T'";
            if (UUID != "")
            {
                sqlstr += " and POSUUID_MASTER='" + UUID + "'";
            }
            OracleCommand cmd = new OracleCommand(sqlstr, conn, trans);
            cmd.ExecuteNonQuery();
            log.Success("Update BILL_DISPATCH STATUS 'T' -> '1' Success,");
            #endregion
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


        #endregion


    }
}
