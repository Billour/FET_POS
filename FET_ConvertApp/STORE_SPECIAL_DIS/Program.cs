using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.OracleClient;

using Advtek.Utility;
using System.Threading;

namespace STORE_SPECIAL_DIS
{
    class Program
    {
        static string sMSG="";
        static void Main(string[] args)
        {
            //初始化LOG
            OutputMsg("1.STORE_SPECIAL_DIS");
            OutputMsg("2.初始化LOG");
            ConvertLog con_log = new ConvertLog("STORE_SPECIAL_DIS");

            //if (DateTime.Today.Day == 1)
            //{
            OutputMsg("3.建立DB(WEB)連線");
            OracleConnection conn = OracleDBUtil.GetConnection();
            OracleTransaction objTX = objTX = conn.BeginTransaction();

            try
            {
                OutputMsg("4.執行SP_STORE_SPECIAL_DIS");
                OracleCommand oraCmd = new OracleCommand("SP_STORE_SPECIAL_DIS");
                oraCmd.CommandType = System.Data.CommandType.StoredProcedure;
                oraCmd.Parameters.Add(new OracleParameter("OUTMESSAGE", OracleType.VarChar, 2000)).Direction = ParameterDirection.Output;
                oraCmd.Connection = conn;
                oraCmd.Transaction = objTX;
                oraCmd.ExecuteNonQuery();
                string sRet = oraCmd.Parameters["OUTMESSAGE"].Value.ToString();
                //OracleDBUtil.ExecuteSql_SP(
                //    objTX,
                //    "SP_STORE_SPECIAL_DIS"
                //    );

                objTX.Commit();
                OutputMsg(sRet);
                OutputMsg("5.執行結束，寫入LOG");
                con_log.Success(sMSG);
                Thread.Sleep(3000);

            }
            catch (Exception ex)
            {
                objTX.Rollback();
                OutputMsg("例外產生");
                OutputMsg(ex.Message);
                con_log.Fail(sMSG);
                Thread.Sleep(3000);
            }
            finally
            {
                if (conn.State == ConnectionState.Open) conn.Close();
                conn.Dispose();
                OracleConnection.ClearAllPools();
            }
            //}
            //else
            //{
            //    con_log.Fail("日期非每月一日!");
            //    Console.WriteLine("日期非每月一日!");
            //    Thread.Sleep(3000);
            //}
        }


        static void OutputMsg(string s)
        {
            Console.WriteLine(s);
            sMSG += s + "\r\n";
        }
    }
}
