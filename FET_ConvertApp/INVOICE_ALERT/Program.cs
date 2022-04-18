using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.OracleClient;

using Advtek.Utility;
using System.Threading;

namespace INVOICE_ALERT
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("INVOICE_ALERT(發票不足通知)");
            ConvertLog con_log = new ConvertLog("INVOICE_ALERT");

            Console.WriteLine("建立DB連線");
            OracleConnection objConn = OracleDBUtil.GetConnection();

            OracleTransaction trans = objConn.BeginTransaction();

             try
            {

                Console.WriteLine("    CALL SP SP_INV_ALERT_MAIL");

                OracleParameter outSTATUS = new OracleParameter();
                outSTATUS.OracleType = OracleType.VarChar;
                outSTATUS.Size = 1;
                outSTATUS.ParameterName = "outSTATUS";
                outSTATUS.Direction = ParameterDirection.Output;

                OracleParameter outMESSAGE = new OracleParameter();
                outMESSAGE.OracleType = OracleType.VarChar;
                outMESSAGE.Size = 200;
                outMESSAGE.ParameterName = "outMESSAGE";
                outMESSAGE.Direction = ParameterDirection.Output;

                OracleDBUtil.ExecuteSql_SP(
                    trans,
                    @"SP_INV_ALERT_MAIL",
                    outSTATUS,
                    outMESSAGE
                    );

                if (outSTATUS.Value.ToString() == "1")
                {
                    trans.Commit();

                    Console.WriteLine("執行結束，寫入LOG");

                    //成功資訊
                    con_log.Success(outMESSAGE.Value.ToString());
                    Thread.Sleep(3000);
                }
                else
                {
                    throw new Exception(outMESSAGE.Value.ToString());
                }
            }
             catch (Exception ex)
             {
                 trans.Rollback();

                 //失敗資訊
                 con_log.Fail(ex.Message);
                 Console.WriteLine("例外產生，寫入LOG");
                 Console.WriteLine(ex.Message);
                 Thread.Sleep(3000);
             }
             finally
             {
                 if (objConn.State == ConnectionState.Open) objConn.Close();
                 objConn.Dispose();
                 OracleConnection.ClearAllPools();
             }
        }
    }
}
