using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Advtek.Utility;
using System.Data;
using System.Data.OracleClient;

namespace BILL_DISPATCH
{
    class Program
    {
        static void Main(string[] args)
        {
            //初始化LOG
            Console.WriteLine("1.BILL_DISPATCH開始");
            Console.WriteLine("2.初始化LOG");
            ConvertLog con_log = new ConvertLog("BILL_DISPATCH");

            OracleConnection objConn = null;
            OracleTransaction objTrans = null;

            try
            {
                Console.WriteLine("3.建立連線");
                objConn = OracleDBUtil.GetConnection();
                objTrans = objConn.BeginTransaction();

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
                    objTrans,
                    "PK_BILL.PayModeSplit",
                    outSTATUS,
                    outMESSAGE
                    );

                if (outSTATUS.Value.ToString() == "1")
                {
                    objTrans.Commit();

                    //成功資訊
                    Console.WriteLine("排程執行結束，寫入LOG");
                    con_log.Success(outMESSAGE.Value.ToString());
                }
                else
                {
                    throw new Exception(outMESSAGE.Value.ToString());
                }
            }
            catch (Exception ex)
            {
                objTrans.Rollback();

                //失敗資訊
                con_log.Fail(ex.Message);
                Console.WriteLine(ex.Message);
            }
            finally
            {
                objTrans.Dispose();
                if (objConn.State == ConnectionState.Open) objConn.Close();
                objConn.Dispose();
                OracleConnection.ClearAllPools();
            }
        }
    }
}
