using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.OracleClient;
using Advtek.Utility;
using System.Threading;

namespace DAY_CLOSE
{
    class Program
    {
        static string sMSG = "";

        static void Main(string[] args)
        {
            //***20110302 廢棄不用，移到DAY_CLOSE_STATUS執行
            //初始化LOG
            OutputMsg("1.DAY_CLOSE開始");
            OutputMsg("2.初始化LOG");
            ConvertLog con_log = new ConvertLog("DAY_CLOSE");
            try {
                OutputMsg("3.執行PK_CONVERT.SP_DAY_CLOSE");
                string sRet = PK_CONVERT_SP_DAY_CLOSE();
                OutputMsg(sRet);
                OutputMsg("4.執行結束，寫入LOG");
                con_log.Success(sMSG);
                Thread.Sleep(5000);
            }
            catch (Exception ex) {
                //失敗資訊
                OutputMsg(sMSG + ex.Message);
                con_log.Fail(sMSG);
                Thread.Sleep(5000);
            }
        }

        static string PK_CONVERT_SP_DAY_CLOSE()
        {
            OracleConnection objConn = null;
            OracleTransaction objTX = null;
            string sRet = "";
            try
            {
                objConn = OracleDBUtil.GetConnection();
                objTX = objConn.BeginTransaction();

                //OracleDBUtil.ExecuteSql_SP(objTX, "SP_COUNT_RECOMMANDED");
                OracleCommand oraCmd = new OracleCommand("PK_CONVERT.SP_DAY_CLOSE");
                oraCmd.CommandType = System.Data.CommandType.StoredProcedure;
                oraCmd.Parameters.Add(new OracleParameter("outCODE", OracleType.VarChar, 2000)).Direction = ParameterDirection.Output;
                oraCmd.Parameters.Add(new OracleParameter("outMESSAGE", OracleType.VarChar, 2000)).Direction = ParameterDirection.Output;
                oraCmd.Connection = objConn;
                oraCmd.Transaction = objTX;
                oraCmd.ExecuteNonQuery();
                sRet = oraCmd.Parameters["outMESSAGE"].Value.ToString();
                if (oraCmd.Parameters["outCODE"].Value.ToString().CompareTo("000") == 0)
                {
                    //更新SYS_PARA三個訂貨相關的欄位值
                    //SO_ORDER_NO、HR_ORDER_NO、PO_ORDER_NO
                    string SO_ORDER_NO = SerialNo.GenNo("SOORDER");
                    string HR_ORDER_NO = SerialNo.GenNo("HRORDER");
                    string PO_ORDER_NO = SerialNo.GenNo("POORDER");

                    string sql = @"update sys_para set para_value='" + SO_ORDER_NO + "' where para_key='SO_ORDER_NO' ";
                    oraCmd = new OracleCommand(sql, objConn, objTX);
                    oraCmd.CommandType = CommandType.Text;
                    oraCmd.ExecuteNonQuery();
                    sql = @" update sys_para set para_value='" + HR_ORDER_NO + "' where para_key='HR_ORDER_NO' ";
                    oraCmd.CommandText = sql;
                    oraCmd.ExecuteNonQuery();
                    sql = @" update sys_para set para_value='" + PO_ORDER_NO + "' where para_key='PO_ORDER_NO' ";
                    oraCmd.CommandText = sql;
                    oraCmd.ExecuteNonQuery();
                    objTX.Commit();
                }
                else
                {
                    throw new Exception(sRet);
                }


            }
            catch (Exception ex)
            {
                objTX.Rollback();
                OutputMsg("3.執行PK_CONVERT.SP_DAY_CLOSE，例外產生");
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
