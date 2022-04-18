using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.OracleClient;

using Advtek.Utility;
using System.Threading;

namespace ERP_RTNNO
{
    class Program
    {
        static void Main(string[] args)
        {
            OracleConnection objConn = OracleDBUtil.GetConnection();
            OracleConnection objConn_erp = OracleDBUtil.GetERPPOSConnection();

            //初始化LOG
            Console.WriteLine("1.ERP_RTNNO");
            Console.WriteLine("2.初始化LOG");
            ConvertLog con_log = new ConvertLog("ERP_RTNNO");

            try
            {
                Console.WriteLine("3.註記ERP_RTNNO(POS)狀態");
                string sql="update erp_rtnno set dwnflag = 'T' where nvl(dwnflag,'0')='0'";
                OracleDBUtil.ExecuteSql(objConn_erp, sql);

                Console.WriteLine("4.查詢ERP_RTNNO(POS)資料");
                DataTable dt = OracleDBUtil.GetDataSet(
                    objConn_erp,
                    @"SELECT  e.storeno, e.txtno, e.itemcode, e.companycode, e.txtdate, e.txtqty,
                         e.dwnflag, e.dwndate, e.rtnno
                  FROM erp_rtnno e
                 WHERE dwnflag = 'T'
                 ORDER BY e.storeno, e.rtnno, e.txtno").Tables[0];
                int iCount = 0;
                if (dt.Rows.Count > 0)
                {
                    #region insert
                    Console.WriteLine("5.更新RTND_UP(WEB)資料");
                    foreach (DataRow dr in dt.Rows)
                    {
                        StringBuilder sb = new StringBuilder();

                        sb.AppendLine(
                            @"UPDATE  RTND_UP
                           SET  ERPRTN_DATE = :ERPRTN_DATE,
                                 ERPRTN_NO = :TXTNO, 
                                 ERPRTN_QTY = :ERPRTN_QTY 
                        WHERE  RTND_PROD_ID in 
                          (   
                               SELECT DISTINCT RP.RTND_PROD_ID 
                                 FROM  rtnm RM , RTND_PROD RP 
                                WHERE RM.RTNN_ID = RP.RTNN_ID  
                                  AND RP.PRODNO=:itemcode
                                  AND RM.rtnno =:ERPRTN_NO
                          )
                          and store_no =:storeno"
                        );
                        string sStore = dr["STORENO"].ToString(); //R2121(POS) => 2121(WEB)
                        if (sStore.Length > 4) sStore = sStore.Substring(1);
                        sb.Replace(":ERPRTN_DATE", OracleDBUtil.SqlStr(Convert.ToDateTime(dr["txtdate"]).ToString("yyyyMMdd")));
                        //sb.Replace(":ERPRTN_NO", OracleDBUtil.SqlStr(dr["RTNNO"].ToString()));R2121HR1234567 > HR1234567
                        sb.Replace(":ERPRTN_NO", OracleDBUtil.SqlStr(dr["txtno"].ToString().Substring(5)));
                        //sb.Replace(":TXTNO", OracleDBUtil.SqlStr(dr["txtno"].ToString())); R2121HR1234567 > 2968
                        sb.Replace(":TXTNO", OracleDBUtil.SqlStr(dr["RTNNO"].ToString()));
                        sb.Replace(":ERPRTN_QTY", OracleDBUtil.SqlStr(dr["TXTQTY"].ToString()));
                        sb.Replace(":itemcode", OracleDBUtil.SqlStr(dr["ITEMCODE"].ToString()));
                        sb.Replace(":storeno", OracleDBUtil.SqlStr(sStore));

                        OracleDBUtil.ExecuteSql(objConn, sb.ToString());

                        OracleTransaction objTx = objConn.BeginTransaction();

                        try
                        {
                            string ret_code="";
                            string ret_message="";

                            //扣庫存
                            PK_ERPRTN(
                                objTx,
                                "1",
                                OracleDBUtil.SqlStr(dr["ITEMCODE"].ToString()),
                                OracleDBUtil.SqlStr(sStore),
                                GetGoodLOCUUID(),
                                OracleDBUtil.SqlStr(dr["RTNNO"].ToString()),
                                Convert.ToInt32(OracleDBUtil.SqlStr(dr["TXTQTY"].ToString())),
                                "convert",
                                null,
                                ref ret_code,
                                ref ret_message);

                            if (ret_code != "000")
                            {
                                throw new Exception(ret_message);
                            }
                            
                            objTx.Commit();
                        }
                        catch(Exception ex)
                        {
                            objTx.Rollback();
                            throw ex;
                        }

                        iCount++;
                    }
                    Console.WriteLine("6.更新ERP_RTNNO(POS)狀態");
                    sql = "update erp_rtnno set dwnflag = 'Y',dwndate=to_char(sysdate,'YYYYMMDD') where dwnflag='T'";
                    OracleDBUtil.ExecuteSql(objConn_erp, sql);

                    //成功資訊
                    con_log.Success("ERP_RTNNO(POS)=>RTND_UP(WEB)UPDATE，異動筆數:" + iCount.ToString());
                    
                    #endregion
                }
                else {
                    //成功資訊
                    con_log.Success("ERP_RTNNO(POS)=>RTND_UP(WEB)UPDATE，來源無資料。" );
                }
                
                Console.WriteLine("7.執行結束，寫入LOG");
                Thread.Sleep(5000);
                //OracleDBUtil.ExecuteSql(
                //    objConn,
                //    "update POS2ERP_ORDER set dwnflag='Y', dwndate=sysdate where NVL(dwnflag,'N')='N'");

               
            }
            catch (Exception ex)
            {
                //失敗資訊
                con_log.Fail(ex.Message);
                Console.WriteLine("例外產生");
                Console.WriteLine(ex.Message);
                Thread.Sleep(5000);
                //throw ex;
            }
            finally
            {
                objConn.Dispose();
                objConn_erp.Dispose();
            }
        }

        public static void PK_ERPRTN(OracleTransaction objTX, string I_SERVICE_TYPE
                                        , string I_PRODNO, string I_STORE_NO, string I_STOCK_ID
                                        , string I_SHEET_NO, int I_INV_QTY, string I_USER
                                        , string I_SOURCE_REFERENCE, ref string O_RT_CODE, ref string O_RT_MESSAGE)
        {
            OracleCommand oraCmd = null;

            try
            {
                oraCmd = new OracleCommand("PK_INVENTORY.ERPRTN");
                oraCmd.CommandType = System.Data.CommandType.StoredProcedure;
                oraCmd.Parameters.Add(new OracleParameter("I_SERVICE_TYPE", OracleType.VarChar, 2000)).Value = I_SERVICE_TYPE;              //服務系統           
                oraCmd.Parameters.Add(new OracleParameter("I_PRODNO", OracleType.VarChar, 2000)).Value = I_PRODNO;                          //商品料號
                oraCmd.Parameters.Add(new OracleParameter("I_STORE_NO", OracleType.VarChar, 2000)).Value = I_STORE_NO;                      //門市代號
                oraCmd.Parameters.Add(new OracleParameter("I_STOCK_ID", OracleType.VarChar, 2000)).Value = I_STOCK_ID;                      //倉庫代碼
                oraCmd.Parameters.Add(new OracleParameter("I_SHEET_NO", OracleType.VarChar, 2000)).Value = I_SHEET_NO;                      //交易序號
                oraCmd.Parameters.Add(new OracleParameter("I_INV_QTY", OracleType.Number)).Value = I_INV_QTY;                               //庫存異動量
                oraCmd.Parameters.Add(new OracleParameter("I_USER", OracleType.VarChar, 2000)).Value = I_USER;                              //人員
                oraCmd.Parameters.Add(new OracleParameter("I_SOURCE_REFERENCE", OracleType.VarChar, 2000)).Value = I_SOURCE_REFERENCE;      //呼叫來源_UUID
                oraCmd.Parameters.Add(new OracleParameter("O_RT_CODE", OracleType.VarChar, 2000)).Direction = ParameterDirection.Output;    //回傳碼
                oraCmd.Parameters.Add(new OracleParameter("O_RT_MESSAGE", OracleType.VarChar, 2000)).Direction = ParameterDirection.Output; //回傳訊息

                oraCmd.Connection = objTX.Connection;
                oraCmd.Transaction = objTX;
                oraCmd.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                oraCmd.Dispose();
            }
        }

        /// <summary>
        /// 取得銷售倉的倉別UUID
        /// </summary>
        /// <returns>銷售倉UUID</returns>
        public static string GetGoodLOCUUID()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("select INV_GoodLOCUUID() as UUID from dual");

            DataTable dt = OracleDBUtil.Query_Data(sb.ToString());

            string UUID = "";
            if (dt.Rows.Count > 0)
            {
                UUID = dt.Rows[0]["UUID"].ToString();
            }

            return UUID;
        }

    }
}
