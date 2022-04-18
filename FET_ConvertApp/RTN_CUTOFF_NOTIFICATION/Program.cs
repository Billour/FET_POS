using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.OracleClient;
using Advtek.Utility;


using System.Threading;

namespace RTN_CUTOFF_NOTIFICATION
{
    class Program
    {
        private static string sMSG = "";
        static void Main(string[] args)
        {
             //初始化LOG
            OutputMsg("1.初始化LOG");
            ConvertLog con_log = new ConvertLog("RTN_CUTOFF_NOTIFICATION");
            try
            {
                //string sMsg = new RTN_CUTOFF_NOTIFICATION_Facade().PK_CONVERT_SP_RTN_CUTOFF_NOTIFICATION();
                OutputMsg("2.執行PK_CONVERT.SP_RTN_CUTOFF_NOTIFICATION()");
                string sMsg = PK_CONVERT_SP_RTN_CUTOFF_NOTIFICATION();
                OutputMsg(sMsg);
                OutputMsg("3.執行結束，寫入OG");
                con_log.Success(sMSG);
                Thread.Sleep(3000);

            }
            catch (Exception ex)
            {
                //失敗資訊
                OutputMsg("<<例外產生>>");
                OutputMsg(ex.Message);
                con_log.Fail(sMSG);
                Thread.Sleep(3000);
            }
        }

        static string PK_CONVERT_SP_RTN_CUTOFF_NOTIFICATION()
        {
            OracleConnection objConn = null;
            OracleTransaction objTX = null;
            string sRet = "";
            try
            {
                objConn = OracleDBUtil.GetConnection();
                objTX = objConn.BeginTransaction();

                //OracleDBUtil.ExecuteSql_SP(objTX, "SP_COUNT_RECOMMANDED");
                OracleCommand oraCmd = new OracleCommand("PK_CONVERT.SP_RTN_CUTOFF_NOTIFICATION");
                oraCmd.CommandType = System.Data.CommandType.StoredProcedure;
                oraCmd.Parameters.Add(new OracleParameter("outCODE", OracleType.VarChar, 2000)).Direction = ParameterDirection.Output;
                oraCmd.Parameters.Add(new OracleParameter("outMESSAGE", OracleType.VarChar, 2000)).Direction = ParameterDirection.Output;
                oraCmd.Connection = objConn;
                oraCmd.Transaction = objTX;
                oraCmd.ExecuteNonQuery();
                sRet = oraCmd.Parameters["outMESSAGE"].Value.ToString();
                if (oraCmd.Parameters["outCODE"].Value.ToString().CompareTo("000") == 0)
                {
                    objTX.Commit();
                }
                else
                {
                    objTX.Rollback();
                }
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
