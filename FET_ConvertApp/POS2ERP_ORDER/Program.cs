using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.OracleClient;
using Advtek.Utility;

namespace POS2ERP_ORDER
{
    class Program
    {
        static void Main(string[] args)
        {
            //初始化LOG
            Console.WriteLine("1.POS2ERP_ORDER開始");
            Console.WriteLine("2.初始化LOG");
            ConvertLog con_log = new ConvertLog("POS2ERP_ORDER");

            try
            {
                Console.WriteLine("3.SP_POS2ERP_ORDER");
                string sMsg=SP_POS2ERP_ORDER();

                //成功資訊
                con_log.Success(sMsg);
                Console.WriteLine("4.排程結束，寫入LOG");
            }
            catch (Exception ex) 
            {
                //失敗資訊
                con_log.Fail(ex.Message);
                Console.WriteLine(ex.Message);
                myPause();
            }
        }
        public static string SP_POS2ERP_ORDER()
        {
            OracleConnection objConn = null;
            OracleTransaction objTX = null;
            string sRet = "";
            try
            {
                objConn = OracleDBUtil.GetConnection();
                objTX = objConn.BeginTransaction();
                OracleParameter op = new OracleParameter("outMessage", OracleType.VarChar, 2000);
                op.Direction = ParameterDirection.Output;
                int iRet = OracleDBUtil.ExecuteSql_SP(objTX, "SP_POS2ERP_ORDER", op);
                sRet = op.Value.ToString();
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
        static void myPause()
        {
            Console.WriteLine("請按任意鍵繼續...");
            Console.ReadKey(true);
        }
    }
}
