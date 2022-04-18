using System.Data.OracleClient;
using log4net;
using System;
using System.Collections;
using System.Data;
using System.Runtime.CompilerServices;
using System.Text;
using System.Configuration;

namespace Advtek.Utility
{
    public sealed class GuidNo 
    {
        private static readonly ILog Logger = LogManager.GetLogger(typeof(GuidNo));
        private static readonly string CONNECT_STRING_KEY ="Connection_String";
       

        public GuidNo() { }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public static string getUUID()
        {
            OracleConnection conn = null;
            string strSQL = "Select POS_UUID() as UUID from dual ";         
            DataSet ds = null;
            string strResult="";
          
            try
            {
                conn = OracleDBUtil.GetConnection(CONNECT_STRING_KEY);
               ds = OracleDBUtil.GetDataSet(conn, strSQL);            
            
                if (ds.Tables[0].Rows.Count >0)
                {
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        strResult = dr[0].ToString().Replace("-", "");
                      
                        Logger.Info("UUID 取號:" + strResult.ToString() + "   :   Length=" + strResult.Length);
                    } 
               }
               else
               {
                   throw new Exception("GUID No Data Fund");
               }               
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message);
                throw ex;
            }
            finally
            {
                conn = null;
                ds.Dispose();
            }
            return strResult;
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public static string SEQNO()
        {
            OracleConnection conn = null;
            string strSQL = "Select STORETRANSFER_D_SEQNO.nextval  from dual ";
            DataSet ds = null;
            string strResult = "";

            try
            {
                conn = OracleDBUtil.GetConnection(CONNECT_STRING_KEY);
                ds = OracleDBUtil.GetDataSet(conn, strSQL);

                if (ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        strResult = dr[0].ToString().Replace("-", "");

                        Logger.Info("SEQNO 取號:" + strResult.ToString() + "   :   Length=" + strResult.Length);
                    }
                }
                else
                {
                    throw new Exception("SEQNO No Data Fund");
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message);
                throw ex;
            }
            finally
            {
                conn = null;
                ds.Dispose();
            }
            return strResult;
        }


    }
}
