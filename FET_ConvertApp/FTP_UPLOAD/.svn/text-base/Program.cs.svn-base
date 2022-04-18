using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Advtek.Utility;
using System.Data;
using System.Data.OracleClient;
using System.Threading;

using Advtek.Utility;
using System.Data;
using System.Data.OracleClient;
using FTP;
using System.IO;

namespace FTP_UPLOAD
{
    class Program
    {
        static OracleConnection wcon = null;
        static string sMSG = "";

        static void Main(string[] args)
        {
            //初始化LOG
            OutputMsg("1.FTP_UPLOAD開始");
            OutputMsg("2.初始化LOG");
            ConvertLog con_log = new ConvertLog("FTP_UPLOAD");
            OracleTransaction wtx = null;
            try
            {
                OutputMsg("3.建立連線");
                wcon = OracleDBUtil.GetConnection();
                wtx = wcon.BeginTransaction();

                OutputMsg("4.註記待處理的資料");
                StringBuilder sb = new StringBuilder();
                sb.Append(@" UPDATE FTP_UPLOAD_POOL SET STATUS='T' WHERE  STATUS='0'");
                ExeSql(sb.ToString(), wtx);

                OutputMsg("5.查詢待處理的資料");
                sb.Length = 0;
                sb.Append(@"SELECT T1.FTP_UPLOAD_ID as FTP_UPLOAD_ID, T1.FUNC_ID as FUNC_ID, T1.FTP_URL as FTP_URL, 
                              T1.LOGIN_ID as LOGIN_ID, T1.LOGIN_PW as LOGIN_PW, T1.PORT_NO as PORT_NO, 
                              T2.FTP_UPLOAD_FILES_ID as FTP_UPLOAD_FILES_ID, T2.SEQ_NO as SEQ_NO, 
                              T2.FILE_PATH as FILE_PATH, T2.FILE_NAME as FILE_NAME
                            FROM FTP_UPLOAD_POOL T1,FTP_UPLOAD_FILES T2
                           WHERE T1.FTP_UPLOAD_ID=T2.FTP_UPLOAD_ID
                             AND  T1.STATUS='T' order by T1.FTP_URL,T1.LOGIN_ID,T1.LOGIN_PW ");
                DataTable dt = SelectTable(sb.ToString(), wtx);
                DataTable dtMaster = dt.DefaultView.ToTable(true, new string[] { "FTP_URL", "LOGIN_ID", "LOGIN_PW" });

                OutputMsg("6.上傳待處理的資料");
                FTPclient ftp = null;
                int iCount = 0;
                int iError = 0;
                int iSuccess = 0;
                sb.Length = 0;
                foreach (DataRow dr in dtMaster.Rows) {
                    if (isNullValue(dr["FTP_URL"].ToString()) || isNullValue(dr["LOGIN_ID"].ToString()) || isNullValue(dr["LOGIN_PW"].ToString())) {
                        continue;
                    }
                    string FTP_URL = dr["FTP_URL"].ToString().Trim();
                    string LOGIN_ID = dr["LOGIN_ID"].ToString().Trim();
                    string LOGIN_PW = dr["LOGIN_PW"].ToString().Trim();

                    ftp = new FTPclient(FTP_URL, LOGIN_ID, LOGIN_PW);

                    DataRow[] drs = dt.Select("FTP_URL=" + OracleDBUtil.SqlStr(dr["FTP_URL"].ToString())
                        + " and LOGIN_ID=" + OracleDBUtil.SqlStr(dr["LOGIN_ID"].ToString())
                        + " and LOGIN_PW=" + OracleDBUtil.SqlStr(dr["LOGIN_PW"].ToString()), "FTP_UPLOAD_ID,SEQ_NO");
                    string sCache = "";
                    foreach (DataRow dr2 in drs) 
                    {
                        if (!sCache.Equals(dr2["FTP_UPLOAD_ID"].ToString()))
                        {
                            sb.Append("update FTP_UPLOAD_POOL set status='1',MODI_USER='CONVERT',MODI_DTM=SYSDATE where FTP_UPLOAD_ID=" + OracleDBUtil.SqlStr(dr2["FTP_UPLOAD_ID"].ToString()) +";");
                            sCache = dr2["FTP_UPLOAD_ID"].ToString();
                        }
                        if (iCount > 200) 
                        {
                            sb.Insert(0, "BEGIN ");
                            sb.Append(" END;");
                            ExeSql(sb.Replace("\r", " ").Replace("\n", " ").ToString(), wtx);
                            sb.Length = 0;
                        }
                        string FILE_NAME = dr2["FILE_NAME"].ToString().Trim();
                        string LOCAL_FILE_PATH = dr2["FILE_PATH"].ToString().Trim() + "\\" + FILE_NAME;
                        
                        if (File.Exists(LOCAL_FILE_PATH))
                        {
                            bool bRet = false;
                            try {
                                bRet = ftp.Upload(LOCAL_FILE_PATH, FILE_NAME);
                            }
                            catch (Exception ex) {
                                sb.Append("update FTP_UPLOAD_POOL set status='X',MODI_USER='CONVERT',MODI_DTM=SYSDATE where FTP_UPLOAD_ID=" + OracleDBUtil.SqlStr(dr2["FTP_UPLOAD_ID"].ToString()) + ";");
                                sb.Append("update FTP_UPLOAD_FILES set status='X' where FTP_UPLOAD_FILES_ID=" + OracleDBUtil.SqlStr(dr2["FTP_UPLOAD_FILES_ID"].ToString()) + ";");
                                iError++;
                                OutputMsg(ex.Message);
                                OutputMsg("FTP_URL=" + OracleDBUtil.SqlStr(dr["FTP_URL"].ToString()));
                                OutputMsg("LOGIN_ID=" + OracleDBUtil.SqlStr(dr["LOGIN_ID"].ToString()));
                                OutputMsg("LOGIN_PW=" + OracleDBUtil.SqlStr(dr["LOGIN_PW"].ToString()));
                                continue ;
                            }
                            
                            if (bRet)
                            {
                                sb.Append("update FTP_UPLOAD_FILES set status='1' where FTP_UPLOAD_FILES_ID=" + OracleDBUtil.SqlStr(dr2["FTP_UPLOAD_FILES_ID"].ToString()) + ";");
                                iSuccess++;
                            }
                            else {

                                sb.Append("update FTP_UPLOAD_FILES set status='X' where FTP_UPLOAD_FILES_ID=" + OracleDBUtil.SqlStr(dr2["FTP_UPLOAD_FILES_ID"].ToString()) + ";");
                                sb.Append("update FTP_UPLOAD_POOL set status='X',MODI_USER='CONVERT',MODI_DTM=SYSDATE where FTP_UPLOAD_ID=" + OracleDBUtil.SqlStr(dr2["FTP_UPLOAD_ID"].ToString()) + ";");
                                iError++;
                            }
                        }
                        else 
                        {
                            sb.Append("update FTP_UPLOAD_FILES set status='X' where FTP_UPLOAD_FILES_ID=" + OracleDBUtil.SqlStr(dr2["FTP_UPLOAD_FILES_ID"].ToString()) + ";");
                            sb.Append("update FTP_UPLOAD_POOL set status='X',MODI_USER='CONVERT',MODI_DTM=SYSDATE where FTP_UPLOAD_ID=" + OracleDBUtil.SqlStr(dr2["FTP_UPLOAD_ID"].ToString()) + ";");
                            iError++;
                        }
                        iCount++;
                    }
                
                }
                OutputMsg("*****************");
                OutputMsg("資料總筆數:" + iCount.ToString());
                OutputMsg("成功筆數:" + iSuccess.ToString());
                OutputMsg("失敗筆數:" + iError.ToString());
                OutputMsg("*****************");

                if (sb.Length>0)
                {
                    sb.Insert(0, "BEGIN ");
                    sb.Append(" END;");
                    ExeSql(sb.Replace("\r", " ").Replace("\n", " ").ToString(), wtx);
                    sb.Length = 0;
                }

                #region Backup
                //int iCount = 0;
                //FTPclient ftp = null;
                //string sTmp = "";
                
                //foreach (DataRow dr in dt.Rows)
                //{
                //    if (string.IsNullOrEmpty(dr["FTP_URL"].ToString()) || string.IsNullOrEmpty(dr["LOGIN_ID"].ToString())
                //        || string.IsNullOrEmpty(dr["LOGIN_PW"].ToString()) || string.IsNullOrEmpty(dr["FILE_PATH"].ToString())
                //        || string.IsNullOrEmpty(dr["FILE_NAME"].ToString())
                //        )
                //    {
                //        continue;
                //    }
                //    string FTP_URL = dr["FTP_URL"].ToString().Trim();
                //    string LOGIN_ID = dr["LOGIN_ID"].ToString().Trim();
                //    string LOGIN_PW = dr["LOGIN_PW"].ToString().Trim();
                //    string FILE_NAME = dr["FILE_NAME"].ToString().Trim();
                //    string LOCAL_FILE_PATH = dr["FILE_PATH"].ToString().Trim() + "\\" + FILE_NAME;
                //    if (!sTmp.Equals(FTP_URL + "," + LOGIN_ID + "," + LOGIN_PW)) 
                //    {
                //        ftp = new FTPclient(FTP_URL, LOGIN_ID, LOGIN_PW);
                //        sTmp = FTP_URL + "," + LOGIN_ID + "," + LOGIN_PW;
                //    }
                    
                //    //STATUS:傳送狀態:
                //    //0:未上傳
                //    //1:已完成
                //    //T:處理中
                //    //X:傳送失敗
                //    sb.Length = 0;
                //    if (File.Exists(LOCAL_FILE_PATH))
                //    {
                //        bool bRet = ftp.Upload(LOCAL_FILE_PATH, FILE_NAME);
                //        if (bRet)
                //        {
                //            sb.Append("update FTP_UPLOAD_POOL set status='1' where FTP_UPLOAD_ID=" + OracleDBUtil.SqlStr(dr["FTP_UPLOAD_ID"].ToString()));
                //        }
                //        else
                //        {
                //            sb.Append("update FTP_UPLOAD_POOL set status='X' where FTP_UPLOAD_ID=" + OracleDBUtil.SqlStr(dr["FTP_UPLOAD_ID"].ToString()));
                //        }
                //    }
                //    else {
                //        sb.Append("update FTP_UPLOAD_POOL set status='X' where FTP_UPLOAD_ID=" + OracleDBUtil.SqlStr(dr["FTP_UPLOAD_ID"].ToString()));
                //    }
                    
                //    ExeSql(sb.ToString(), wtx);

                //}
                #endregion

                wtx.Commit();
                if (sMSG.Length > 2000) sMSG = sMSG.Substring(0, 1999);
                con_log.Success(sMSG);
                Thread.Sleep(3000);
            }
            catch (Exception ex)
            {
                if (wtx != null) wtx.Dispose();
                OutputMsg(ex.Message);
                con_log.Fail(sMSG);
                Thread.Sleep(3000);
            }
            finally {
                DisposeConnection(wcon);
                OracleConnection.ClearAllPools();
            }
        }

        static DataTable SelectTable(string sql, OracleConnection con)
        {
            DataTable dt = new DataTable();
            try
            {
                dt = OracleDBUtil.GetDataSet(con, sql).Tables[0];
            }
            catch (Exception ex) { throw ex; }
            return dt;
        }

        static DataTable SelectTable(string sql, OracleTransaction otx)
        {
            DataTable dt = new DataTable();
            try
            {
                dt = OracleDBUtil.GetDataSet(otx, sql).Tables[0];
            }
            catch (Exception ex) {
                if (otx != null) otx.Rollback();
                throw ex; 
            }
            return dt;
        }

        static int ExeSql(string sql, OracleConnection con)
        {
            OracleTransaction otx = null;
            int i = 0;
            try
            {
                otx = con.BeginTransaction();
                i = OracleDBUtil.ExecuteSql(otx, sql);
                otx.Commit();
            }
            catch (Exception ex)
            {
                otx.Rollback();
                throw ex;
            }
            finally
            {
                if (otx != null) otx.Dispose();
            }
            return i;
        }

        static int ExeSql(string sql, OracleTransaction otx)
        {
            int i = 0;
            try
            {
                i = OracleDBUtil.ExecuteSql(otx, sql);
            }
            catch (Exception ex)
            {
                otx.Rollback();
                throw ex;
            }
            return i;
        }


        static void OutputMsg(string s)
        {
            Console.WriteLine(s);
            sMSG += s + "\r\n";
        }

        static bool isNullValue(string s) {
            bool bRet = false;
            if(string.IsNullOrEmpty(s) || string.IsNullOrEmpty(s.Trim())) bRet=true;
            return bRet;
        }
        static void DisposeConnection(OracleConnection con)
        {
            if (con != null)
            {
                if (con.State == ConnectionState.Open) con.Close();
                con.Dispose();
            }
        }
    }
}
