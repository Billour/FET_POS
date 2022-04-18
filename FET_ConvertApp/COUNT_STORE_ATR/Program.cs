using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.OracleClient;
using Advtek.Utility;
using System.Transactions;
using System.Configuration;
using System.Threading;

namespace COUNT_STORE_ATR
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("1.COUNT_STORE_ATR開始");
            Console.WriteLine("2.初始化LOG");
            ConvertLog cLog = new ConvertLog("COUNT_STORE_ATR");
            try
            {
                //門市ATR分配量計算 
                Console.WriteLine("3.SP_COUNT_STORE_ATR開始");
                string sMsg = SP_COUNT_STORE_ATR();
                Console.WriteLine("4.COUNT_STORE_ATR結束，寫入LOG");
                cLog.Success(sMsg);

            }
            catch (Exception ex)
            {
                cLog.Fail(ex.Message);
                Console.WriteLine(ex.Message);
                myPause();
            }
        }

        public static string SP_COUNT_STORE_ATR()
        {
            OracleConnection objConn = null;
            OracleTransaction objTX = null;
            string sRet = "";
            try
            {
                objConn = OracleDBUtil.GetConnection();
                objTX = objConn.BeginTransaction();

                //OracleDBUtil.ExecuteSql_SP(objTX, "SP_COUNT_STORE_ATR");
                OracleCommand oraCmd = new OracleCommand("SP_COUNT_STORE_ATR");
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

        static void myPause()
        {
            Console.WriteLine("請按任意鍵繼續...");
            Console.ReadKey(true);
        }
        #region 備份20110117
        //private static OracleConnection _con;
        //private static TransactionScope _ts;
        //private static ConvertLog cLog;

        //static void Main(string[] args)
        //{
        //    //***檢查逾時設定
        //    string sSecondTimeOut = ConfigurationManager.AppSettings["SecondTimeOut"].ToString();
        //    int iSecondTimeOut;
        //    if (!int.TryParse(sSecondTimeOut, out iSecondTimeOut))
        //    {
        //        Console.WriteLine("App.config 逾時設定非整數，請檢查設定");
        //        myPause();
        //    }
        //    //設定TransactionScope
        //    TransactionOptions ops = new TransactionOptions();
        //    ops.IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted;
        //    ops.Timeout = getTimeSpan(iSecondTimeOut);//設定+5秒
        //    Console.WriteLine("初始化log");
        //    cLog = new ConvertLog("COUNT_STORE_ATR");

        //    //進行Transaction
        //    using (_ts = new TransactionScope(TransactionScopeOption.Required, ops))
        //    {
        //        try
        //        {
        //            CallWithTimeout(DoDBMethod, iSecondTimeOut);
        //            //CallWithTimeout(FiveSecondMethod, 4000);
        //            _ts.Complete();
        //            Console.WriteLine("執行結束");
        //            myPause();
        //        }
        //        catch (TimeoutException ex1)
        //        {
        //            //逾時處理
        //            Console.WriteLine(ex1.Message);
        //            cLog.Fail(ex1.Message);
        //            myPause();
        //        }
        //        catch (Exception ex)
        //        {
        //            Console.WriteLine(ex.Message);
        //            cLog.Fail(ex.Message);
        //            myPause();
        //        }
        //        finally
        //        {
        //            if (_con != null)
        //            {
        //                if (_con.State == ConnectionState.Open) _con.Close();
        //                _con.Dispose();
        //                OracleConnection.ClearAllPools();
        //            }
        //        }
        //    }//using (_ts = new TransactionScope(TransactionScopeOption.Required, ops)) 
            
        //}

        //static void DoDBMethod()
        //{
        //    try
        //    {
        //        Console.WriteLine("SP_COUNT_STORE_ATR");
        //        string sMsg = SP_COUNT_STORE_ATR();
        //        Console.WriteLine("執行成功寫入LOG");
        //        cLog.Success(sMsg);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }

        //}

        //static string SP_COUNT_STORE_ATR()
        //{
        //    string sRet = "";
        //    try
        //    {
        //        _con = OracleDBUtil.GetConnection();

        //        OracleCommand oraCmd = new OracleCommand("SP_COUNT_STORE_ATR");
        //        oraCmd.CommandType = System.Data.CommandType.StoredProcedure;
        //        oraCmd.Parameters.Add(new OracleParameter("outMESSAGE", OracleType.VarChar, 2000)).Direction = ParameterDirection.Output;
        //        oraCmd.Connection = _con;
        //        oraCmd.ExecuteNonQuery();
        //        sRet = oraCmd.Parameters["outMESSAGE"].Value.ToString();


        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //    return sRet;
        //}

        //static void CallWithTimeout(Action action, int timeoutMilliseconds)
        //{
        //    Thread threadToKill = null;
        //    Action wrappedAction = () =>
        //    {
        //        threadToKill = Thread.CurrentThread;
        //        action();
        //    };
        //    IAsyncResult result = wrappedAction.BeginInvoke(null, null);
        //    if (result.AsyncWaitHandle.WaitOne(timeoutMilliseconds))
        //    {
        //        wrappedAction.EndInvoke(result);
        //    }
        //    else
        //    {
        //        threadToKill.Abort();
        //        throw new TimeoutException();
        //    }
        //}

        //static TimeSpan getTimeSpan(int iSecondTimeOut)
        //{
        //    int mod = iSecondTimeOut + 5000;
        //    int iHour = mod / (60 * 60 * 1000);
        //    mod = iSecondTimeOut % (60 * 60 * 1000);
        //    int iMinute = mod / (60 * 1000);
        //    mod = mod % (60 * 1000);
        //    int iSecond = mod / 1000;
        //    return new TimeSpan(iHour, iMinute, iSecond);
        //}

        //static void myPause()
        //{
        //    Console.WriteLine("請按任意鍵繼續...");
        //    Console.ReadKey(true);
        //}
        #endregion

    }
}
