using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.OracleClient;
using Advtek.Utility;
using System.Threading;

namespace STOCK_MONTHLY_CLOSE
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("1.STOCK_MONTHLY_CLOSE");
            Console.WriteLine("2.初始化LOG");
            ConvertLog cLog = new ConvertLog("STOCK_MONTHLY_CLOSE");
            try
            {
                Console.WriteLine("3.SP_STOCK_MONTHLY_CLOSE");
                string sMsg =SP_STOCK_MONTHLY_CLOSE();
                Console.WriteLine("4.執行結束，寫入LOG");
                cLog.Success(sMsg);
                Thread.Sleep(5000);
            }
            catch (Exception ex)
            {
                Console.WriteLine("例外產生，寫入LOG");
                cLog.Fail(ex.Message);
                Console.WriteLine(ex.Message);
                Thread.Sleep(5000);

            }
        }

        static string SP_STOCK_MONTHLY_CLOSE()
        {
            OracleConnection objConn = null;
            OracleTransaction objTX = null;
            string sRet = "";
            try
            {
                objConn = OracleDBUtil.GetConnection();
                objTX = objConn.BeginTransaction();

                OracleCommand oraCmd = new OracleCommand("PK_CONVERT.SP_STOCK_MONTHLY_CLOSE");
                oraCmd.CommandType = System.Data.CommandType.StoredProcedure;
                OracleParameter p = new OracleParameter("sDATE", DBNull.Value);//NULL:正式，2011/01/01測試
                p.OracleType = OracleType.VarChar;
                p.Size = 10;
                p.Direction = ParameterDirection.Input;
                oraCmd.Parameters.Add(p);
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
