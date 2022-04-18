using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Advtek.Utility;
using System.Data;
using System.Data.OracleClient;

namespace PREORDER_TO_ORDER
{
    class Program
    {
        static void Main(string[] args)
        {
            //初始化LOG
            Console.WriteLine("1.PREORDER_TO_ORDER開始");
            Console.WriteLine("2.初始化LOG");
            ConvertLog con_log = new ConvertLog("PREORDER_TO_ORDER");
            Console.WriteLine("3.建立連線");
            OracleConnection objConn = OracleDBUtil.GetConnection();
            OracleTransaction objTrans = objConn.BeginTransaction();

            try
            {
                //因為會是第一個跑的排程,所以先更新so_orderno,讓這個排程抓到的是今天的編號
                Console.WriteLine("4.更新so_orderno,讓這個排程抓到的是今天的編號");
                updateSo_orderno(objTrans);
                //更新sys para的STORE_ORDER_NO ***20110308 so_orderno的SEQNO+1
                updateSTORE_ORDER_NO(objTrans);

                updatePo_orderno(objTrans);

                Console.WriteLine("5.SP_PRORDER_TO_ORDER");
                OracleCommand oraCmd = new OracleCommand("SP_PRORDER_TO_ORDER");
                oraCmd.CommandType = System.Data.CommandType.StoredProcedure;
                oraCmd.Parameters.Add(new OracleParameter("outMESSAGE", OracleType.VarChar, 2000)).Direction = ParameterDirection.Output;
                oraCmd.Connection = objConn;
                oraCmd.Transaction = objTrans;
                oraCmd.ExecuteNonQuery();
                string sRet = oraCmd.Parameters["outMESSAGE"].Value.ToString();

                //跑完再更新一次so_orderno,讓下一個抓sys para so_orderno的可以抓到新的
                Console.WriteLine("6.再更新次so_orderno，讓下一個抓sys para so_orderno的可以抓到新的");
                updateSo_orderno(objTrans);
                updateSo_orderno(objTrans);//***20110308 再跑一次，跳2個號碼。

                objTrans.Commit();

                //成功資訊
                Console.WriteLine("排程執行結束，寫入LOG");
                con_log.Success(sRet);
            }
            catch (Exception ex)
            {
                objTrans.Rollback();

                //失敗資訊
                con_log.Fail(ex.Message);
                Console.WriteLine(ex.Message);
                myPause();
            }
            finally
            {
                objTrans.Dispose();
                if (objConn.State == ConnectionState.Open) objConn.Close();
                objConn.Dispose();
                OracleConnection.ClearAllPools();
            }
        }

        //更新sys para的SO_ORDER_NO
        private static void updateSo_orderno(OracleTransaction objTrans)
        {
            OracleDBUtil.ExecuteSql(
                        objTrans,
                        "update sys_para set PARA_VALUE='" + SerialNo.GenNo("SOORDER") + "'  where PARA_KEY='SO_ORDER_NO'"
                        );
        }

        //更新sys para的STORE_ORDER_NO
        private static void updateSTORE_ORDER_NO(OracleTransaction objTrans)
        {
            OracleDBUtil.ExecuteSql(
                        objTrans,
                        @" update sys_para set para_value=(
                            select substr(para_value,1,length(para_value)-3) || lpad(to_number(substr(PARA_VALUE,-3))+1,3,'0')   from sys_para  where PARA_KEY='SO_ORDER_NO'
                           ) where para_key='STORE_ORDER_NO'"
                        );
        }

        //更新sys para的PO_ORDER_NO
        private static void updatePo_orderno(OracleTransaction objTrans)
        {
            OracleDBUtil.ExecuteSql(
                        objTrans,
                        "update sys_para set PARA_VALUE='" + SerialNo.GenNo("POORDER") + "'  where PARA_KEY='PO_ORDER_NO'"
                        );
        }

        static void myPause()
        {
            Console.WriteLine("請按任意鍵繼續...");
            Console.ReadKey(true);
        }
    }
}
