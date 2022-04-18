using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.OracleClient;

using Advtek.Utility;

using FET.POS.Model.DTO;
using FET.POS.Model.Facade;
using FET.POS.Model.Facade.ConvertApp;
using FET.POS.Model.DTO.ConvertApp;
using FET.POS.Model.Common;


namespace FET.POS.Model.Facade.ConvertApp
{
    public class SendMailSchedule_Facade : BaseClass
   {
        public void Update_ALERTMAIL_POOL(DataTable dtUpd)
        {
            OracleConnection objConn = null;
            OracleTransaction objTX = null;

            try
            {
                objConn = OracleDBUtil.GetConnection();
                objTX = objConn.BeginTransaction();
                if(dtUpd.Rows.Count>0)
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

        public DataTable Query_ALERTMAIL_POOL()
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
                //sb.Append(" T3.RESEND_COUNT<T1.SEND_COUNT ");
                sb.Append(") ");

                objConn=OracleDBUtil.GetConnection();
                DataTable dt = OracleDBUtil.GetDataSet(objConn, sb.ToString()).Tables[0];
                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally {
                if (objConn.State == ConnectionState.Open) objConn.Close();
                objConn.Dispose();
                OracleConnection.ClearAllPools();
            }
        }

        #region 舊SPEC
        //public DataTable Query_ALERTMAIL_POOL()
        //{
        //    try
        //    {
        //        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        //        sb.Append("SELECT ");
        //        sb.Append(" T1.SID as SID, ");
        //        sb.Append(" T1.ALERT_MAIL_TYPE_ID as ALERT_MAIL_TYPE_ID, ");
        //        sb.Append(" nvl(T1.ALERTTYPE,'1') as ALERTTYPE, ");
        //        sb.Append(" nvl(T1.BODYFORMAT,'1') as BODYFORMAT, ");
        //        sb.Append(" nvl(T1.SUBJECT,'') as SUBJECT, ");
        //        sb.Append(" nvl(T1.BODY_CONTENT,'') as BODY_CONTENT, ");
        //        sb.Append(" nvl(T1.SEND_FLAG,'0') as SEND_FLAG, ");
        //        sb.Append(" nvl(T1.IS_RESEND,'N') as IS_RESEND, ");
        //        sb.Append(" nvl(T1.SEND_COUNT,0) as SEND_COUNT, ");
        //        sb.Append(" nvl(T3.MAIL_SENDER,'') as MAIL_SENDER, ");
        //        sb.Append(" nvl(T3.MAIL_RECEIVER,'') as MAIL_RECEIVER, ");
        //        sb.Append(" nvl(T3.MAIL_CC_RECEIVER,'') as MAIL_CC_RECEIVER, ");
        //        sb.Append(" nvl(T3.MAIL_BCC_RECEIVER,'') as MAIL_BCC_RECEIVER, ");
        //        sb.Append(" nvl(T4.FILE_PATH,'') as FILE_PATH ");
        //        sb.Append("FROM ALERTMAIL_POOL T1 ");
        //        sb.Append(" INNER JOIN ALERT_MAIL_TYPE T2 ON T1.ALERT_MAIL_TYPE_ID=T2.ALERT_MAIL_TYPE_ID  ");
        //        sb.Append(" INNER JOIN ALERT_MAIL_SET T3 ON T2.ALERT_MAIL_TYPE_ID=T3.ALERT_MAIL_TYPE_ID ");
        //        sb.Append(" LEFT OUTER JOIN ALERTMAIL_ATTACHFILES T4 ON T1.SID=T4.SID ");
        //        sb.Append("WHERE ");
        //        sb.Append("(  T1.ALERTTYPE='1' AND T1.SEND_FLAG='0'  )   ");
        //        sb.Append(" OR ");
        //        sb.Append("( ");
        //        sb.Append(" T1.ALERTTYPE='1' ");
        //        sb.Append(" AND ");
        //        sb.Append(" T1.SEND_FLAG='2' ");
        //        sb.Append(" AND ");
        //        sb.Append(" T1.IS_RESEND='Y' ");
        //        sb.Append(" AND ");
        //        sb.Append(" T3.RESEND_COUNT<T1.SEND_COUNT ");
        //        sb.Append(") ");


        //        DataTable dt = OracleDBUtil.GetDataSet(OracleDBUtil.GetConnection(), sb.ToString()).Tables[0];
        //        return dt;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //    finally { }
        //}
        #endregion
       

        public DataTable Query_Mail_Setting()
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
                objConn=OracleDBUtil.GetConnection();
                DataTable dt = OracleDBUtil.GetDataSet(objConn, sb.ToString()).Tables[0];
                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally {
                if (objConn.State == ConnectionState.Open) objConn.Close();
                objConn.Dispose();
                OracleConnection.ClearAllPools();
            }
        }
       
        
       

       #region call SP
       // public void Check_Upload_Temp(string sBATCH_NO, string sUSER_ID)
       // {
       //     OracleConnection objConn = null;
       //     OracleTransaction objTX = null;

       //     try
       //     {
       //         objConn = OracleDBUtil.GetConnection();
       //         objTX = objConn.BeginTransaction();

       //         OracleDBUtil.ExecuteSql_SP(
       //            objTX
       //            , "SP_INV29_CheckImei_Upload_Temp"
       //            , new OracleParameter("inBATCHNO", sBATCH_NO)
       //            , new OracleParameter("inUSERID", sUSER_ID)
       //            );

       //         objTX.Commit();

       //     }
       //     catch (Exception ex)
       //     {
       //         objTX.Rollback();
       //         throw ex;
       //     }
       //     finally
       //     {
       //         objTX = null;
       //         objConn = null;
       //     }
       // }
       #endregion
       

      
   }
}
