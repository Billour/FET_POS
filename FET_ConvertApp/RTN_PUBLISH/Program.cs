using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.OracleClient;
using Advtek.Utility;

using System.Threading;


namespace RTN_PUBLISH
{
    class Program
    {
        private static string sMSG = "";
        static void Main(string[] args)
        {
            //初始化LOG
            OutputMsg("1.初始化LOG");
            ConvertLog cLog = new ConvertLog("RTN_PUBLISH");
            try
            {
                OutputMsg("2.執行PK_CONVERT.SP_RTN_NOTIFICATION()");
                //string sMsg = new RTN_PUBLISH_Facade().SP_RTN_PUBLISH();
                string sMsg = SP_RTN_PUBLISH();
                OutputMsg(sMsg);
                OutputMsg("3.執行結束，寫入OG");
                cLog.Success(sMSG);
                Thread.Sleep(3000);
            }
            catch (Exception ex)
            {
                OutputMsg("<<例外產生>>");
                OutputMsg(ex.Message);
                cLog.Fail(sMSG);
                Thread.Sleep(3000);
            }
        }

        static string SP_RTN_PUBLISH()
        {
            OracleConnection objConn = null;
            OracleTransaction objTX = null;
            string sRet = "";
            try
            {
                objConn = OracleDBUtil.GetConnection();
                objTX = objConn.BeginTransaction();

                //OracleDBUtil.ExecuteSql_SP(objTX, "SP_COUNT_RECOMMANDED");
                OracleCommand oraCmd = new OracleCommand("PK_CONVERT.SP_RTN_PUBLISH");
                oraCmd.CommandType = System.Data.CommandType.StoredProcedure;
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

        static void OutputMsg(string s)
        {
            Console.WriteLine(s);
            sMSG += s + "\r\n";
        }  
    }
}
