using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.OracleClient;
using Advtek.Utility;


using System.Threading;

namespace INVOICE_TRANSFER
{
    class Program
    {
        static string sMSG = "";
        static void Main(string[] args)
        {
            //初始化LOG
            OutputMsg("1.INVOICE_TRANSFER");
            OutputMsg("2.初始化LOG");
            ConvertLog cLog = new ConvertLog("INVOICE_TRANSFER");
            try 
            {
                //string sMessage=new INVOICE_TRANSFER_Facade().PK_INVOICE_SP_STORE_INVOICE_NO();
                OutputMsg("3.執行PK_INVOICE.SP_STORE_INVOICE_NO");
                string sMessage = PK_INVOICE_SP_STORE_INVOICE_NO();
                OutputMsg("********************");
                OutputMsg(sMessage);
                OutputMsg("********************");
                cLog.Success(sMSG);
                OutputMsg("4.執行結束，寫入LOG");
                Thread.Sleep(3000);
            }
            catch (Exception ex) 
            {
                OutputMsg(ex.Message);
                cLog.Fail(sMSG);    
                Thread.Sleep(3000);
            }
        }

        static string PK_INVOICE_SP_STORE_INVOICE_NO()
        {
            OracleConnection objConn = null;
            OracleTransaction objTX = null;
            string sRet = "";
            try
            {
                objConn = OracleDBUtil.GetConnection();
                objTX = objConn.BeginTransaction();


                //OracleDBUtil.ExecuteSql_SP(objTX, "PK_INVOICE.SP_STORE_INVOICE_NO",new OracleParameter("sRunningDate",""),new OracleParameter();
                OracleCommand oraCmd = new OracleCommand("PK_INVOICE.SP_STORE_INVOICE_NO");
                oraCmd.CommandType = System.Data.CommandType.StoredProcedure;
                oraCmd.Parameters.Add(new OracleParameter("sRunningDate", OracleType.VarChar)).Value=DBNull.Value;
                oraCmd.Parameters.Add(new OracleParameter("outMSGCODE", OracleType.VarChar, 2000)).Direction = ParameterDirection.Output;
                oraCmd.Parameters.Add(new OracleParameter("outMESSAGE", OracleType.VarChar, 2000)).Direction = ParameterDirection.Output;
                oraCmd.Connection = objConn;
                oraCmd.Transaction = objTX;
                oraCmd.ExecuteNonQuery();
                sRet = oraCmd.Parameters["outMESSAGE"].Value.ToString();
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
            return sRet;
        }

        static void OutputMsg(string s)
        {
            Console.WriteLine(s);
            sMSG += s + "\r\n";
        }   
    }
}
