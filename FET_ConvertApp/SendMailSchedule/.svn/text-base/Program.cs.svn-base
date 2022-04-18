using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Advtek.Utility;

using System.IO;
using System.Data.OracleClient;
using System.Threading;


namespace SendMailSchedule
{
    class Program
    {
        static void Main(string[] args)
        {
            AdvMail myMail = new AdvMail();
            #region 測試用CODE
            //List<string> lstTest=new List<string>();
            //lstTest.Add("192.168.8.238");
            //lstTest.Add("mailuser");
            //lstTest.Add("mailuser");
            //lstTest.Add("fangrayray@gmail.com");
            //lstTest.Add("fang_ray_ray@hotmail.com");
            //lstTest.Add("Mail測試");
            //lstTest.Add("test");
            //lstTest.Add("HTML");
            //lstTest.Add("");
            //lstTest.Add("");
            //myMail.GoToSentMailAction3(lstTest);
            //return;
            #endregion

            //SendMailSchedule_Facade cFacade = new SendMailSchedule_Facade();
            ConvertLog cLog = new ConvertLog("SendMailSchedule");
            int iLogCount = 0;
            try
            {
                //取得要發送MAIL的資料
                DataTable dtMail = Query_ALERTMAIL_POOL();
                DataTable dtSet = Query_Mail_Setting();
                DataTable dtSID = dtMail.DefaultView.ToTable(true, "SID");

                string sHost = "";
                string sUSERNAME = "";
                string sPASSWORD = "";
                //***20110223 抓SYS_PARA設定的MAIL SENDER
                string sMailSender = Query_MAIL_SENDER();
                if (string.IsNullOrEmpty(sMailSender.Trim())) 
                {
                    //sMailSender = "SYSTEM@FAREASTONE.COM.TW";***20110224 未設定丟EXCEPTION
                    throw new Exception("SYS_PARA尚未設定系統寄件者。PARA_KEY='MAIL_SYSTEM_SENDER'");
                }

                if (dtSet.Rows.Count > 0)
                {
                    sHost = dtSet.Rows[0]["MAILHOST"].ToString();
                    sUSERNAME = dtSet.Rows[0]["USERNAME"].ToString();
                    sPASSWORD = dtSet.Rows[0]["PASSWORD"].ToString();
                }
                else
                {
                    throw new Exception("查無MAIL SERVER設定");
                }


                //DS for 更新狀態
                //SendMailSchedule_DTO dsPool = new SendMailSchedule_DTO();
                //SendMailSchedule_DTO.ALERTMAIL_POOLDataTable dtPool = dsPool.ALERTMAIL_POOL;
                DataTable dtPool = new DataTable();
                dtPool.TableName = "ALERTMAIL_POOL";
                dtPool.Columns.Add(new DataColumn("SID"));
                dtPool.Columns.Add(new DataColumn("SEND_FLAG"));
                dtPool.Columns.Add(new DataColumn("SEND_COUNT"));
                
                
                for (int i = 0; i < dtSID.Rows.Count; i++)
                {
                    DataRow drSID = dtSID.Rows[i];
                    DataRow[] drMails = dtMail.Select("SID='" + drSID["SID"].ToString() + "'");

                    if (drMails.Length > 0)
                    {
                        DataRow drMail = drMails[0];
                        List<string> lstMail = new List<string>();
                        #region 欄位註解
                        //ALERTMAIL_POOL.SID, SID UUID
                        //ALERTMAIL_POOL.ALERT_MAIL_TYPE_ID, 
                        //ALERTMAIL_POOL.ALERTTYPE, 1:e-Mail 2. SMS 簡訊
                        //ALERTMAIL_POOL.BODYFORMAT, 1:TEXT 2.HTML
                        //ALERTMAIL_POOL.SUBJECT,  Mail 主旨
                        //ALERTMAIL_POOL.BODY_CONTENT,  Mail 內文
                        //ALERTMAIL_POOL.SEND_FLAG, 0:未送 1:已送 2:失敗
                        //ALERTMAIL_POOL.IS_RESEND,  失敗是否重送:Y:YES N:NO
                        //ALERTMAIL_POOL.SEND_COUNT, 已傳(重)送次數
                        //ALERTMAIL_POOL.MAIL_SENDER,  寄件人, 限一組MAIL ADDRESS
                        //ALERTMAIL_POOL.MAIL_RECEIVER,收件人 MAIL Address -多帳號時用分號隔開
                        //ALERTMAIL_POOL.MAIL_CC_RECEIVER, 副本收件人,多帳號時用分號隔開
                        //ALERTMAIL_POOL.MAIL_BCC_RECEIVER,密件收件人,多帳號時用分號隔開
                        //ALERTMAIL_ATTACHFILES.FILE_PATH 附件位置
                        #endregion
                        //string smtpServer = args[0];
                        lstMail.Add(sHost);
                        lstMail.Add(sUSERNAME);
                        lstMail.Add(sPASSWORD);
                        //string from = args[1];
                        //lstMail.Add(drMail["MAIL_SENDER"].ToString());//***20110223 抓SYS_PARA設定的MAIL SENDER
                        lstMail.Add(sMailSender);
                        //string to = args[2];
                        lstMail.Add(drMail["MAIL_RECEIVER"].ToString());
                        //string subject = args[3];
                        lstMail.Add(drMail["SUBJECT"].ToString());
                        //string body = args[4];
                        lstMail.Add(drMail["BODY_CONTENT"].ToString());
                        //string bodyFormat = args[5];
                        if (drMail["BODYFORMAT"].ToString() == "1")
                        {
                            lstMail.Add("TEXT");
                        }
                        else
                        {
                            lstMail.Add("HTML");
                        }
                        //string cc = args[6];
                        lstMail.Add(drMail["MAIL_CC_RECEIVER"].ToString());
                        //string bcc = args[7];
                        lstMail.Add(drMail["MAIL_BCC_RECEIVER"].ToString());
                        //File Path
                        for (int j = 0; j < drMails.Length; j++)
                        {
                            if (drMails[j]["FILE_PATH"] != DBNull.Value || !drMails[j]["FILE_PATH"].ToString().Trim().Equals(""))
                            {
                                lstMail.Add(drMails[j]["FILE_PATH"].ToString());
                            }

                        }
                        //狀態寫入暫存的dtPool
                        DataRow drPool = dtPool.NewRow();
                        if (myMail.GoToSentMailAction3(lstMail))
                        {
                            drPool["SID"] = drMail["SID"];
                            drPool["SEND_FLAG"] = "1";
                            drPool["SEND_COUNT"] = Convert.ToInt32(drMail["SEND_COUNT"].ToString()) + 1;
                        }
                        else
                        {
                            drPool["SID"] = drMail["SID"];
                            drPool["SEND_FLAG"] = "2";
                            drPool["SEND_COUNT"] = Convert.ToInt32(drMail["SEND_COUNT"].ToString()) + 1;
                        }
                        dtPool.Rows.Add(drPool);

                    }//(drMails.Length > 0) 

                    dtPool.AcceptChanges();

                }// (int i = 0; i < dtSID.Rows.Count; i++)

                //  發送後，更新狀態
                //cFacade.Update_ALERTMAIL_POOL(dtPool);
                Update_ALERTMAIL_POOL(dtPool);
                iLogCount = dtPool.Rows.Count;
                cLog.Success("MAIL發送筆數:" + iLogCount.ToString());
                Thread.Sleep(3000);
            }
            catch (Exception ex)
            {
                cLog.Fail(ex.Message);
                Console.Write(ex.Message);
                Thread.Sleep(3000);
            }
            
        }

        public static DataTable Query_Mail_Setting()
        {
            OracleConnection objConn = null;
            try
            {
                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                sb.Append("SELECT ");
                sb.Append(" (SELECT PARA_VALUE FROM  SYS_PARA WHERE PARA_KEY='MAIL_SERVER' ) AS MAILHOST, ");
                sb.Append(" (SELECT PARA_VALUE FROM  SYS_PARA WHERE PARA_KEY='MAIL_SERVER_USERNAME') AS USERNAME, ");
                sb.Append(" (SELECT PARA_VALUE FROM  SYS_PARA WHERE PARA_KEY='MAIL_SERVER_PASSWORD') AS PASSWORD ");
                sb.Append("FROM DUAL ");
                objConn = OracleDBUtil.GetConnection();
                DataTable dt = OracleDBUtil.GetDataSet(objConn, sb.ToString()).Tables[0];
                return dt;
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

        public static DataTable Query_ALERTMAIL_POOL()
        {
            OracleConnection objConn = null;
            try
            {
                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                sb.Append("SELECT ");
                sb.Append(" T1.SID as SID, ");
                //sb.Append(" T1.ALERT_MAIL_TYPE_ID as ALERT_MAIL_TYPE_ID, ");
                sb.Append(" nvl(T1.ALERTTYPE,'1') as ALERTTYPE, ");
                sb.Append(" nvl(T1.BODYFORMAT,'1') as BODYFORMAT, ");
                sb.Append(" nvl(T1.SUBJECT,'') as SUBJECT, ");
                sb.Append(" nvl(T1.BODY_CONTENT,'') as BODY_CONTENT, ");
                sb.Append(" nvl(T1.SEND_FLAG,'0') as SEND_FLAG, ");
                sb.Append(" nvl(T1.IS_RESEND,'N') as IS_RESEND, ");
                sb.Append(" nvl(T1.SEND_COUNT,0) as SEND_COUNT, ");
                sb.Append(" nvl(T1.MAIL_SENDER,'') as MAIL_SENDER, ");
                sb.Append(" nvl(T1.MAIL_RECEIVER,'') as MAIL_RECEIVER, ");
                sb.Append(" nvl(T1.MAIL_CC_RECEIVER,'') as MAIL_CC_RECEIVER, ");
                sb.Append(" nvl(T1.MAIL_BCC_RECEIVER,'') as MAIL_BCC_RECEIVER, ");
                sb.Append(" nvl(T4.FILE_PATH,'') as FILE_PATH ");
                sb.Append("FROM ALERTMAIL_POOL T1 ");
                //sb.Append(" INNER JOIN ALERT_MAIL_TYPE T2 ON T1.ALERT_MAIL_TYPE_ID=T2.ALERT_MAIL_TYPE_ID  ");
                //sb.Append(" INNER JOIN ALERT_MAIL_SET T3 ON T2.ALERT_MAIL_TYPE_ID=T3.ALERT_MAIL_TYPE_ID ");
                sb.Append(" LEFT OUTER JOIN ALERTMAIL_ATTACHFILES T4 ON T1.SID=T4.SID ");
                sb.Append("WHERE ");
                sb.Append("(  T1.ALERTTYPE='1' AND T1.SEND_FLAG='0'  )   ");
                sb.Append(" OR ");
                sb.Append("( ");
                sb.Append(" T1.ALERTTYPE='1' ");
                sb.Append(" AND ");
                sb.Append(" T1.SEND_FLAG='2' ");
                sb.Append(" AND ");
                sb.Append(" T1.IS_RESEND='Y' ");
                //sb.Append(" AND ");
                //sb.Append(" T1.mail_sender='SYSTEM@SYSTEM.com.tw' ");
                //sb.Append(" AND ");
                //sb.Append(" T3.RESEND_COUNT<T1.SEND_COUNT ");
                sb.Append(") ");

                objConn = OracleDBUtil.GetConnection();
                DataTable dt = OracleDBUtil.GetDataSet(objConn, sb.ToString()).Tables[0];
                return dt;
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

        public static void Update_ALERTMAIL_POOL(DataTable dtUpd)
        {
            OracleConnection objConn = null;
            OracleTransaction objTX = null;

            try
            {
                objConn = OracleDBUtil.GetConnection();
                objTX = objConn.BeginTransaction();
                if (dtUpd.Rows.Count > 0)
                    OracleDBUtil.UPDDATEByUUID(dtUpd, "SID");
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
                if (objConn.State == ConnectionState.Open) objConn.Close();
                objConn.Dispose();
                OracleConnection.ClearAllPools();
            }
        }

        static string Query_MAIL_SENDER()
        {
            OracleConnection objConn = null;
            string sRet = "";
            try
            {
                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                sb.Append(@"SELECT  MAX(PARA_VALUE)  FROM SYS_PARA WHERE PARA_KEY='MAIL_SYSTEM_SENDER'");
                objConn = OracleDBUtil.GetConnection();
                DataTable dt = new DataTable();
                dt=OracleDBUtil.GetDataSet(objConn, sb.ToString()).Tables[0];
                if (dt.Rows.Count > 0) {
                    sRet = dt.Rows[0][0].ToString();
                }
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
            return sRet;
        }
    }
}
