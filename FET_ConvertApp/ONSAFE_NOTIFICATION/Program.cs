using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.OracleClient;

using Advtek.Utility;
using System.Threading;

namespace ONSAFE_NOTIFICATION
{
    class Program
    {
        static void Main(string[] args)
        {
            //初始化LOG
            Console.WriteLine("SP_ONSAFE");
            Console.WriteLine("初始化LOG");
            ConvertLog con_log = new ConvertLog("SP_ONSAFE");

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
                    "SP_ONSAFE_NOTIFICATION",
                    outMESSAGE
                    );

                objTX.Commit();

                con_log.Success("succss!");

            }
            catch (Exception ex)
            {
                objTX.Rollback();
                con_log.Fail(ex.Message);
            }
            finally
            {
                conn.Dispose();
            }
        }
    }
}
