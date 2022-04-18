using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.OracleClient;

using Advtek.Utility;
using System.Threading;

namespace SIM_CARD_AUTO
{
    class Program
    {
        static void Main(string[] args)
        {
            //初始化LOG
            Console.WriteLine("SIM_CARD_AUTO");
            Console.WriteLine("初始化LOG");
            ConvertLog con_log = new ConvertLog("SIM_CARD_AUTO");

            OracleConnection conn = OracleDBUtil.GetConnection();
            OracleTransaction objTX = objTX = conn.BeginTransaction();

            try
            {

                OracleDBUtil.ExecuteSql_SP(
                    objTX,
                    "SIM_CARD_AUTO_REPLENISHMENT"
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
