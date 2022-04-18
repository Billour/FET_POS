using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.OracleClient;
using Advtek.Utility;
using System.Threading;


namespace UPD_STOCK_DAILY_CLOSE
{
    class Program
    {
        static void Main(string[] args)
        {
            //初始化LOG
            Console.WriteLine("1.UPD_STOCK_DAILY_CLOSE");
            Console.WriteLine("2.初始化LOG");
            ConvertLog con_log = new ConvertLog("UPD_STOCK_DAILYCLOSE");

            try 
            {
                Console.WriteLine("3.SP_UPD_STOCK_DAILY_CLOSE");
                string sMsg = SP_UPD_STOCK_DAILY_CLOSE();

                //成功資訊
                Console.WriteLine("4.執行結束，寫入LOG");
                con_log.Success(sMsg);
                Thread.Sleep(5000);
            }
            catch (Exception ex) 
            {
                //失敗資訊
                Console.WriteLine("例外產生，寫入LOG");
                con_log.Fail(ex.Message);
                Console.WriteLine(ex.Message);
                Thread.Sleep(5000);
            }
        }

        static string SP_UPD_STOCK_DAILY_CLOSE()
        {
            string sRet = "";
            OracleConnection objConn = null;
            OracleTransaction objTX = null;
            try
            {
                objConn = OracleDBUtil.GetConnection();
                objTX = objConn.BeginTransaction();
                //iRet = OracleDBUtil.ExecuteSql_SP(objTX, "SP_UPD_STOCK_DAILY_CLOSE");
                OracleCommand oraCmd = new OracleCommand("SP_UPD_STOCK_DAILY_CLOSE");
                oraCmd.CommandType = System.Data.CommandType.StoredProcedure;
                oraCmd.Parameters.Add(new OracleParameter("outCODE", OracleType.VarChar, 50)).Direction = ParameterDirection.Output;
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
    }
}
