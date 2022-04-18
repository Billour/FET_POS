using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data;
using System.Data.OracleClient;
using System.Threading;
using Advtek.Utility;

namespace GL_TEMP_CLEAR
{
    class Program
    {
        static string sMSG = "";
        static void Main(string[] args)
        {
            //初始化LOG
            OutputMsg("1.GL_TEMP_CLEAR");
            OutputMsg("2.初始化LOG");
            ConvertLog cLog = new ConvertLog("GL_TEMP_CLEAR");
            try
            {
                OutputMsg("3.執行PK_GL.GL_TEMP_CLEAR");
                string sMessage = PK_GL_GL_TEMP_CLEAR();
                OutputMsg(sMessage);
                cLog.Success(sMSG);
                OutputMsg("4.執行結束，寫入LOG");
                Thread.Sleep(3000);
            }
            catch (Exception ex)
            {
                OutputMsg(ex.Message);
                cLog.Fail(sMSG);
                Thread.Sleep(3000);
            }
            
        }

        static string PK_GL_GL_TEMP_CLEAR()
        {
            string sRet = "";
            OracleConnection wcon = null;
            OracleTransaction wtx = null;
            try
            {
                wcon = OracleDBUtil.GetConnection();
                wtx = wcon.BeginTransaction();
                OracleCommand oraCmd = new OracleCommand("PK_GL.GL_TEMP_CLEAR");
                oraCmd.CommandType = System.Data.CommandType.StoredProcedure;
                oraCmd.Parameters.Add(new OracleParameter("OUTMESSAGE", OracleType.VarChar, 2000)).Direction = ParameterDirection.Output;
                oraCmd.Connection = wcon;
                oraCmd.Transaction = wtx;
                oraCmd.ExecuteNonQuery();
                sRet = oraCmd.Parameters["OUTMESSAGE"].Value.ToString();
                wtx.Commit();
            }
            catch (Exception ex)
            {
                OutputMsg("PK_GL.GL_TEMP_CLEAR例外產生");
                if (wtx != null) wtx.Rollback();
                throw ex;
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

            return sRet;

        }

        static void OutputMsg(string s)
        {
            Console.WriteLine(s);
            sMSG += s + "\r\n";
        } 
    }
}
