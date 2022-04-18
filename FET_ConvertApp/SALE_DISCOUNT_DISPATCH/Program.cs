using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Advtek.Utility;
using System.Data;
using System.Data.OracleClient;

namespace SALE_DISCOUNT_DISPATCH
{
    class Program
    {
        static string sMSG = "";

        static void Main(string[] args)
        {
            //初始化LOG
            OutputMsg("1.SALE_DISCOUNT_DISPATCH開始");
            OutputMsg("2.初始化LOG");
            ConvertLog con_log = new ConvertLog("SALE_DISCOUNT_DISPATCH");

            OracleConnection objConn = null;
            OracleTransaction objTrans = null;

            try
            {
                OutputMsg("3.建立連線");
                objConn = OracleDBUtil.GetConnection();
                objTrans = objConn.BeginTransaction();

                string outSTATUS;
                string outMESSAGE;

                OutputMsg("4.執行SP_SALE_DISCOUNT_DISPATCH");
                OracleDBUtil.ExecuteSql_SP(
                    objTrans,
                    "SP_SALE_DISCOUNT_DISPATCH",
                    out outSTATUS,
                    out outMESSAGE
                    );

                if (outSTATUS == "000")
                {
                    objTrans.Commit();

                    //成功資訊
                    OutputMsg("5.執行結果:" + outMESSAGE);
                    OutputMsg("6.排程執行結束，寫入LOG");
                    con_log.Success(sMSG);
                }
                else
                {
                    throw new Exception(outMESSAGE);
                }
            }
            catch (Exception ex)
            {
                objTrans.Rollback();

                //失敗資訊
                OutputMsg(ex.Message);
                con_log.Fail(sMSG);
            }
            finally
            {
                objTrans.Dispose();
                if (objConn.State == ConnectionState.Open) objConn.Close();
                objConn.Dispose();
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
