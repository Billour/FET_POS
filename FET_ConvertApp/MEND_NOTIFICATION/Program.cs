using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Advtek.Utility;
using System.Data;
using System.Data.OracleClient;
using System.Threading;

namespace MEND_NOTIFICATION
{
    class Program
    {
        static string sMSG = "";
        static void Main(string[] args)
        {
            OracleConnection wcon = null;
            OracleTransaction wtx = null;
            //初始化LOG
            OutputMsg("1.MEND_NOTIFICATION(SIM卡5日前補貨量通知)");
            OutputMsg("2.初始化LOG");
            ConvertLog cLog = new ConvertLog("MEND_NOTIFICATION");
            try
            {
                OutputMsg("3.建立連線");
                wcon = OracleDBUtil.GetConnection();

                OutputMsg("4.執行PK_CONVERT_SP_MEND_NOTIFICATIONN");
                wtx = wcon.BeginTransaction();
                OracleCommand oraCmd = new OracleCommand("PK_CONVERT.SP_MEND_NOTIFICATION");
                oraCmd.CommandType = System.Data.CommandType.StoredProcedure;
                oraCmd.Parameters.Add(new OracleParameter("OUTMESSAGE", OracleType.VarChar, 2000)).Direction = ParameterDirection.Output;
                oraCmd.Connection = wcon;
                oraCmd.Transaction = wtx;
                oraCmd.ExecuteNonQuery();
                string sRet = oraCmd.Parameters["OUTMESSAGE"].Value.ToString();
                wtx.Commit();
                OutputMsg(sRet);

                OutputMsg("5.執行結束，寫入LOG");
                cLog.Success(sMSG);

                Thread.Sleep(3000);
            }
            catch (Exception ex)
            {
                OutputMsg(ex.Message);
                cLog.Fail(sMSG);
                Thread.Sleep(3000);
            }
            finally
            {
                if (wcon != null)
                {
                    if (wcon.State == ConnectionState.Open) wcon.Close();
                    wcon.Dispose();
                }
                OracleConnection.ClearAllPools();
            }
        }

         static void OutputMsg(string s)
        {
            Console.WriteLine(s);
            sMSG += s + "\r\n";
        }

    }
}
