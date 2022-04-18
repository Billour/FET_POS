using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Advtek.Utility;
using System.Net.Mail;



namespace SendMailSchedule
{
    class AdvMail
    {
        public AdvMail() { }

        #region 備份20110121
        //public bool GoToSentMailAction(string[] args)
        //{
        //    try
        //    {
        //        string smtpServer = args[0];
        //        string username = args[1];
        //        string password = args[2];
        //        string from = args[3];
        //        string to = args[4];
        //        string subject = args[5];
        //        string body = args[6];
        //        //string smtpServer = "smtp.domain.com";   
               
        //        int cdoBasic = 1;    
        //        int cdoSendUsingPort = 2; 
        //        //				SmtpMail.SmtpServer=smtpServer;
        //        //				SmtpMail.Send(from,to,subject,body);

        //        //	CultureInfo ci = new CultureInfo("ja-JP");
        //        MailMessage message = new MailMessage();
               
        //        message.From = from;
        //        message.To = to;
        //        message.Body = body;
        //        message.BodyFormat = MailFormat.Html;
        //        //message.BodyEncoding=MailEncoding.UUEncode;
        //        //附加檔
        //        //myEmail.Attachments.Add(new MailAttachment(Server.MapPath("WebPage2.aspx")));
        //        message.Fields.Add("http://schemas.microsoft.com/cdo/configuration/smtpserver", smtpServer);
        //        message.Fields.Add("http://schemas.microsoft.com/cdo/configuration/smtpserverport", 25);
        //        message.Fields.Add("http://schemas.microsoft.com/cdo/configuration/sendusing", cdoSendUsingPort);
        //        message.Fields.Add("http://schemas.microsoft.com/cdo/configuration/smtpauthenticate", cdoBasic);
        //        message.Fields.Add("http://schemas.microsoft.com/cdo/configuration/sendusername", username);
        //        message.Fields.Add("http://schemas.microsoft.com/cdo/configuration/sendpassword", password);   
        //        //	message.BodyEncoding = Encoding.GetEncoding(ci.TextInfo.ANSICodePage);
        //        message.Subject = subject;
        //        //message.Fields

               
        //        //SmtpMail.SmtpServer = smtpServer;
        //        SmtpMail.Send(message);
        //        return true;
        //    }
        //    catch (Exception err)
        //    {
        //        //  errmsg.Value = err.Message.ToString();

        //        //  obj.Record_SystemLogin("GoSendMail", "SendMail", err.Message.ToString());
        //        return false;
        //    }

        //}

        /// <summary>
        /// args[0]=server,args[1]=from,args[2]=to,
        /// args[3]=subject,args[4]=body,args[5]=HTML or TEXT,
        /// args[>5]=Attachement paths
        /// </summary>
        /// <param name="args"> List(string) </param>
        /// <returns>true:success,false:fail</returns>
        //public bool GoToSentMailAction2(List<string> args)
        //{
        //    try
        //    {

        //        string smtpServer = args[0];
        //        string username = args[1];
        //        string password = args[2];
        //        string from = args[3];
        //        string to = args[4];
        //        string subject = args[5];
        //        string body = args[6];
        //        string bodyFormat = args[7];
        //        string cc = args[8];
        //        string bcc = args[9];
        //        int cdoBasic = 1;
        //        int cdoSendUsingPort = 2; 
        //        if (string.IsNullOrEmpty(from) || string.IsNullOrEmpty(to))
        //            return false;

        //        List<string> paths=new List<string>();
        //        for (int i = 10,j=0; i < args.Count; i++,j++) 
        //        {
        //            paths.Add(args[i]);
        //        }

        //        //				SmtpMail.SmtpServer=smtpServer;
        //        //				SmtpMail.Send(from,to,subject,body);
        //        //	CultureInfo ci = new CultureInfo("ja-JP");
        //        MailMessage message = new MailMessage();
                
        //        message.From = from;
        //        message.To = to;
        //        if(!string.IsNullOrEmpty(cc))
        //            message.Cc = cc;
        //        if(!string.IsNullOrEmpty(bcc))
        //            message.Bcc = bcc;

        //        message.Body = body;
        //        if (bodyFormat.ToUpper() == "HTML")
        //        {
        //            message.BodyFormat = MailFormat.Html;
        //        }
        //        else 
        //        {
        //            message.BodyFormat = MailFormat.Text;
        //        }
                
        //        //message.BodyEncoding=MailEncoding.UUEncode;
        //        //附加檔
        //        //myEmail.Attachments.Add(new MailAttachment(Server.MapPath("WebPage2.aspx")));
        //        foreach (string sPath in paths) 
        //        {
        //            message.Attachments.Add(new MailAttachment(sPath));
        //        }


        //        //	message.BodyEncoding = Encoding.GetEncoding(ci.TextInfo.ANSICodePage);
        //        message.Subject = subject;
        //        message.Fields.Add("http://schemas.microsoft.com/cdo/configuration/smtpserver", smtpServer);
        //        message.Fields.Add("http://schemas.microsoft.com/cdo/configuration/smtpserverport", 25);
        //        message.Fields.Add("http://schemas.microsoft.com/cdo/configuration/sendusing", cdoSendUsingPort);
        //        message.Fields.Add("http://schemas.microsoft.com/cdo/configuration/smtpauthenticate", cdoBasic);
        //        message.Fields.Add("http://schemas.microsoft.com/cdo/configuration/sendusername", username);
        //        message.Fields.Add("http://schemas.microsoft.com/cdo/configuration/sendpassword", password);  
                
        //        SmtpMail.SmtpServer = smtpServer;
                


        //        SmtpMail.Send(message);
        //        return true;
        //    }
        //    catch (Exception err)
        //    {
        //        //  errmsg.Value = err.Message.ToString();

        //        //  obj.Record_SystemLogin("GoSendMail", "SendMail", err.Message.ToString());
        //        return false;
        //    }

        //}
        #endregion

        public bool GoToSentMailAction3(List<string> args)
        {
            try
            {

                string smtpServer = args[0];
                string username = args[1];
                string password = args[2];
                string from = args[3];
                string to = args[4];
                string subject = args[5];
                string body = args[6];
                string bodyFormat = args[7];
                string cc = args[8];
                string bcc = args[9];
      
                if (string.IsNullOrEmpty(from.Trim()) || string.IsNullOrEmpty(to.Trim()))
                    return false;

                List<string> paths = new List<string>();
                for (int i = 10, j = 0; i < args.Count; i++, j++)
                {
                    paths.Add(args[i]);
                }

              
                MailMessage message = new MailMessage();

                //寄件者
                message.From = new MailAddress(from,  from, System.Text.Encoding.UTF8);

                //收件者
                string[] arrTo=to.Split(';');
                for (int i = 0; i < arrTo.Length; i++) {
                    if (!string.IsNullOrEmpty(arrTo[i].Trim())) {
                        message.To.Add(arrTo[i]);
                    }
                }
                //檢查收件者是否無資料
                if (message.To.Count == 0)
                    return false;
                //CC
                if (!string.IsNullOrEmpty(cc)) 
                {
                    string[] arrCC = cc.Split(';');

                    for (int i = 0; i < arrCC.Length; i++) 
                    {
                        if (!string.IsNullOrEmpty(arrCC[i].Trim())) {
                            message.CC.Add(arrCC[i]);
                        }
                    }
                        
                }

                //BCC
                if (!string.IsNullOrEmpty(bcc)) {
                    string[] arrBCC = bcc.Split(';');
                    for (int i = 0; i < arrBCC.Length; i++) 
                    {
                        if (!string.IsNullOrEmpty(arrBCC[i].Trim())) {
                            message.Bcc.Add(arrBCC[i]);
                        }
                    }
                    
                }

                //內文
                message.Body = body;
                message.BodyEncoding = System.Text.Encoding.UTF8;
                if (bodyFormat.ToUpper() == "HTML")
                    message.IsBodyHtml = true;
                else
                    message.IsBodyHtml = false;

                //附加檔
                foreach (string sPath in paths)
                {
                    message.Attachments.Add(new Attachment(sPath));
                }

                //主旨
                message.Subject = subject;
                message.SubjectEncoding = System.Text.Encoding.UTF8;

                SmtpClient smtp = new SmtpClient(smtpServer, 25);

                //帳號/密碼
                //smtp.Credentials = new System.Net.NetworkCredential(username, password);

                //發mail
                smtp.Send(message);
                return true;
            }
            catch (Exception err)
            {
                return false;
            }

        }
       
    }

}
