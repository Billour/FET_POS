using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.OracleClient;

using Advtek.Utility;
using System.Threading;

namespace ATR_DEFICIENCY_NOTIFICATION
{
    class Program
    {
        static void Main(string[] args)
        {
            //初始化LOG
            Console.WriteLine("SP_ATR_Deficiency");
            Console.WriteLine("初始化LOG");
            ConvertLog con_log = new ConvertLog("SP_ATR_Deficiency");

            OracleConnection conn = OracleDBUtil.GetConnection();
            OracleTransaction objTX = objTX = conn.BeginTransaction();
            OracleParameter outMESSAGE = new OracleParameter();
            outMESSAGE.OracleType = OracleType.VarChar;
            outMESSAGE.Size = 200;
            outMESSAGE.ParameterName = "outMESSAGE";
            outMESSAGE.Direction = ParameterDirection.Output;
            try
            {

                OracleDBUtil.ExecuteSql_SP(
                    objTX,
                    "SP_ATR_Deficiency_NOTIFICATION",
                    outMESSAGE
                    );

                objTX.Commit();

                con_log.Success("succss!");

            }
            catch (Exception ex)
            {
                objTX.Rollback();
                con_log.Fail(ex.Message);
                OutputMsg("<<例外產生>>");
                OutputMsg(ex.Message);
            }
            finally
            {
                conn.Dispose();
            }

            OutputMsg("執行成功");
        }

        static void OutputMsg(string s)
        {
            Console.WriteLine(s);
        }

    }
}
