using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Advtek.Utility;
using System.Data;
using System.Data.OracleClient;
using System.Threading;

namespace SO_UPDATE_LOG_UPLOAD
{
    class Program
    {
        static void Main(string[] args)
        {
            ConvertLog con_log = null;

            OracleConnection objConn = null;
            OracleConnection objConn_erp = null;

            OracleTransaction trans = null;
            OracleTransaction trans_erp = null;

            try
            {
                //初始化LOG
                Console.WriteLine("SyncPOS2HRS_FIXI");
                Console.WriteLine("初始化LOG");
                con_log = new ConvertLog("SyncPOS2HRS_FIXI");

                Console.WriteLine("建立新DB連線");
                objConn = OracleDBUtil.GetConnection();

                Console.WriteLine("GET SO_UPDATE_LOG");
                DataTable dt = OracleDBUtil.GetDataSet(
                    objConn,
                    @"SELECT *
                    FROM SO_UPDATE_LOG
                    WHERE NVL(DWNFLAG,'0')='0'"
                    ).Tables[0];

                if (dt.Rows.Count > 0)
                {
                    #region upload SO_UPDATE_LOG

                    objConn_erp = OracleDBUtil.GetERPPOSConnection();
                    trans_erp = objConn_erp.BeginTransaction();

                    Console.WriteLine("    INSERT INTO SO_UPDATE_LOG ");
                    foreach (DataRow dr in dt.Rows)
                    {
                        StringBuilder sb = new StringBuilder();

                        sb.AppendLine(
                            @"Insert into SO_UPDATE_LOG
                               (SONO, 
                                STATUS, TRANS_DATE, DWNFLAG, 
                                SONOX)
                             Values
                               (:SONO, 
                                :STATUS, TO_DATE(':TRANS_DATE', 'YYYY/MM/DD HH24:MI:SS'), '0',  
                                :SXX)"
                        );

                        sb.Replace(":SONO", OracleDBUtil.SqlStr(dr["SONO"].ToString()));
                        sb.Replace(":STATUS", OracleDBUtil.SqlStr(dr["STATUS"].ToString()));
                        sb.Replace(":TRANS_DATE", Convert.ToDateTime(dr["TRANS_DATE"]).ToString("yyyy/MM/dd HH:mm:ss"));
                        sb.Replace(":SXX", OracleDBUtil.SqlStr(dr["SONOX"].ToString()));

                        OracleDBUtil.ExecuteSql(trans_erp, sb.ToString());
                    }

                    trans_erp.Commit();

                    #endregion
                }

                trans = objConn.BeginTransaction();

                Console.WriteLine("    UPDATE SO_UPDATE_LOG DWNFLAG ");
                OracleDBUtil.ExecuteSql(
                    trans,
                    "Update SO_UPDATE_LOG  Set  DWNFLAG ='1',DWNDATE=SYSDATE where NVL(DWNFLAG,'0') ='0'");

                trans.Commit();
                
                Console.WriteLine("執行結束，寫入LOG");
                //成功資訊
                con_log.Success("SO_UPDATE_LOG上傳筆數:" + dt.Rows.Count.ToString());
                Thread.Sleep(3000);
            }
            catch (Exception ex)
            {
                if (trans != null)
                {
                    trans.Rollback();
                }
                trans_erp.Rollback();

                //失敗資訊
                con_log.Fail(ex.Message);
                Console.WriteLine("例外產生，寫入LOG");
                Console.WriteLine(ex.Message);
                Thread.Sleep(3000);
            }
            finally
            {
                if (objConn_erp.State == ConnectionState.Open) objConn_erp.Close();
                objConn_erp.Dispose();
                if (objConn.State == ConnectionState.Open) objConn.Close();
                objConn.Dispose();
                OracleConnection.ClearAllPools();
            }
        }
    }
}
