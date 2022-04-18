using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Advtek.Utility;
using System.Data;
using System.Data.OracleClient;
using System.Threading;

namespace SyncPOS2HRS_FIXI
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

                trans = objConn.BeginTransaction();

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
                    trans,
                    @"SP_HRS_FIXI_TO_TEMP",
                    outSTATUS,
                    outMESSAGE
                    );

                if (outSTATUS.Value.ToString() == "1")
                {
                    trans.Commit();
                    Console.WriteLine(outMESSAGE.Value.ToString());
                    con_log.Success(outMESSAGE.Value.ToString());
                }
                else
                {
                    throw new Exception(outMESSAGE.Value.ToString());
                }

                trans = objConn.BeginTransaction();

                OracleDBUtil.ExecuteSql(
                    trans,
                    "Update HRS_FIXI_TEMP  Set  Send_hrs_FLAG ='T' where Send_hrs_FLAG ='0'"
                    );

                objConn_erp = OracleDBUtil.GetERPPOSConnection();
                trans_erp = objConn_erp.BeginTransaction();

                DataTable dt = OracleDBUtil.GetDataSet(
                    trans,
                    @"SELECT *
                    FROM HRS_FIXI_TEMP
                    WHERE Send_hrs_FLAG='T'"
                    ).Tables[0];

                if (dt.Rows.Count > 0)
                {
                    #region upload HRS_FIXI

                    Console.WriteLine("    INSERT INTO HRS_FIXI ");
                    foreach (DataRow dr in dt.Rows)
                    {
                        StringBuilder sb = new StringBuilder();

                        sb.AppendLine(
                            @"Insert into HRS_FIXI
                         Values
                           (:SREIALNO,
                            :STORENO,
                            :FIXNO ,
                            :INVOICENO,
                            :INVOICE_DATE,
                            :PRO_AMT,
                            :AMOUNT ,
                            :STATUS_CODE,
                            sysdate
                            )"
                        );

                        sb.Replace(":STORENO", OracleDBUtil.SqlStr(dr["STORENO"].ToString()));
                        sb.Replace(":SREIALNO", OracleDBUtil.SqlStr(dr["SREIALNO"].ToString()));
                        sb.Replace(":FIXNO", OracleDBUtil.SqlStr(dr["FIXNO"].ToString()));
                        sb.Replace(":INVOICENO", OracleDBUtil.SqlStr(dr["INVOICENO"].ToString()));
                        sb.Replace(":INVOICE_DATE", OracleDBUtil.DateStr(Convert.ToDateTime(dr["INVOICE_DATE"]).ToString("yyyy/MM/dd")));
                        sb.Replace(":PRO_AMT", OracleDBUtil.SqlStr(dr["PRO_AMT"].ToString()));
                        sb.Replace(":AMOUNT", OracleDBUtil.SqlStr(dr["AMOUNT"].ToString()));
                        sb.Replace(":STATUS_CODE", OracleDBUtil.SqlStr(dr["STATUS_CODE"].ToString()));

                        OracleDBUtil.ExecuteSql(trans_erp, sb.ToString());
                    }

                    #endregion
                }

                Console.WriteLine("    UPDATE HRS_FIXI_TEMP ");
                OracleDBUtil.ExecuteSql(
                    trans,
                    "Update HRS_FIXI_TEMP  Set  Send_hrs_FLAG ='1' where Send_hrs_FLAG ='T'");

                trans.Commit();
                trans_erp.Commit();
                Console.WriteLine("執行結束，寫入LOG");
                //成功資訊
                con_log.Success("HRS_FIXI上傳筆數:" + dt.Rows.Count.ToString());
                Thread.Sleep(3000);
            }
            catch (Exception ex)
            {
                trans.Rollback();
                trans_erp.Rollback();

                //失敗資訊
                con_log.Fail(ex.Message);
                Console.WriteLine("例外產生，寫入LOG");
                Console.WriteLine(ex.Message);
                Thread.Sleep(3000);

                //throw ex;
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
