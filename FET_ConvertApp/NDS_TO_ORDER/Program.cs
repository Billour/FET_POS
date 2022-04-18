using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.OracleClient;
using Advtek.Utility;

namespace NDS_TO_ORDER
{
    class Program
    {
        static void Main(string[] args)
        {
            //初始化LOG
            ConvertLog con_log = new ConvertLog("NDS_TO_ORDER");

            OracleConnection conn = null;
            OracleTransaction objTX = null;

            try
            {
                conn = OracleDBUtil.GetConnection();
                objTX = conn.BeginTransaction();

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
                    objTX,
                    "SP_NDS_TO_ORDER",
                    outSTATUS,
                    outMESSAGE
                    );

                if (outSTATUS.Value.ToString() == "1")
                {
                    objTX.Commit();

                    con_log.Success(outMESSAGE.Value.ToString());
                }
                else
                {
                    throw new Exception(outMESSAGE.Value.ToString());
                }
            }
            catch (Exception ex)
            {
                objTX.Rollback();
                con_log.Fail(ex.Message);
            }
            finally
            {
                conn.Close();
                conn.Dispose();
            }
        }
    }
}
