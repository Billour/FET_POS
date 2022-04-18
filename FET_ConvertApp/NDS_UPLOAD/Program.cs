using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.OracleClient;

using Advtek.Utility;
using System.Threading;

namespace NDS_UPLOAD
{
    class Program
    {
        static void Main(string[] args)
        {
            //初始化LOG
            Console.WriteLine("NDS_UPLOAD");
            Console.WriteLine("初始化LOG");
            ConvertLog con_log = new ConvertLog("NDS_UPLOAD");

            DataTable dt = new DataTable();

            OracleConnection objConn = null;
            OracleConnection objConn_erp = null;

            OracleTransaction trans = null;
            OracleTransaction trans_erp = null;

            try
            {
                Console.WriteLine("建立新舊DB連線");
                objConn = OracleDBUtil.GetConnection();
                objConn_erp = OracleDBUtil.GetERPPOSConnection();

                trans = objConn.BeginTransaction();
                trans_erp = objConn_erp.BeginTransaction();
            
                Console.WriteLine(" POS2ERP_ORDER(WEB) >  ERP_ORDER(POS) ");
                Console.WriteLine("    QUERY POS2ERP_ORDER(WEB) ");

                //明細都沒有量時不會產生
                dt = OracleDBUtil.GetDataSet(
                    trans,
                    @"SELECT *
                    FROM POS2ERP_ORDER
                    WHERE NVL(dwnflag,'N')='N' 
                          and ORDER_NO is not null 
                          and BRANCHNO is not null 
                          and STORE_NO is not null 
                          and COMPANYCODE is not null 
                          and ORDTYPE is not null
                          AND POS2ERP_ORDER_ID IN (
                                SELECT DISTINCT M.POS2ERP_ORDER_ID
                                FROM POS2ERP_ORDER M , POS2ERP_ORDERS D
                                WHERE M.POS2ERP_ORDER_ID=D.POS2ERP_ORDER_ID
                                AND D.ORDQTY>0 )"
                    ).Tables[0];

                if (dt.Rows.Count > 0)
                {
                    #region upload erp_order
                    Console.WriteLine("    INSERT ERP_ORDER(POS) ");
                    foreach (DataRow dr in dt.Rows)
                    {
                        StringBuilder sb = new StringBuilder();

                        sb.AppendLine(
                            @"Insert into ERP_ORDER
                           (ORDERNO, 
                            BRANCHNO, STORENO, ORDTYPE, COMPANYCODE, OENO)
                         Values
                           (:ORDERNO, 
                            :BRANCHNO, :STORENO, :ORDTYPE, :COMPANYCODE, :OENO)"
                        );

                        sb.Replace(":ORDERNO", OracleDBUtil.SqlStr(dr["ORDER_NO"].ToString()));
                        sb.Replace(":BRANCHNO", OracleDBUtil.SqlStr(dr["BRANCHNO"].ToString()));
                        sb.Replace(":STORENO", OracleDBUtil.SqlStr("R" + dr["STORE_NO"].ToString()));
                        sb.Replace(":ORDTYPE", OracleDBUtil.SqlStr(dr["ORDTYPE"].ToString()));
                        sb.Replace(":COMPANYCODE", OracleDBUtil.SqlStr(dr["COMPANYCODE"].ToString()));
                        sb.Replace(":OENO", OracleDBUtil.SqlStr(dr["OENO"].ToString()));

                        OracleDBUtil.ExecuteSql(trans_erp, sb.ToString());
                    }

                    #endregion
                }

                int order_count = dt.Rows.Count;
                Console.WriteLine("    UPDATE POS2ERP_ORDER(WEB) ");
                OracleDBUtil.ExecuteSql(
                    trans,
                    "update POS2ERP_ORDER set dwnflag='Y', DWNDATE=TO_CHAR(SYSDATE,'YYYYMMDD'), STATUS='1' where NVL(dwnflag,'N')='N'");

                Console.WriteLine(" POS2ERP_ORDERS(WEB) >  ERP_ORDERS(POS) ");
                Console.WriteLine("    QUERY POS2ERP_ORDERS(WEB) ");

                //寫入ERP_ORDERS(POS)前，檢查WEB POS中的OLD POS complex pk是否重複
                DataTable dtCheck = OracleDBUtil.GetDataSet(trans,
                    @"SELECT  
                        STORE_NO,ORDER_NO,COMPANYCODE,ITEMCODE
                      FROM 
                        POS2ERP_ORDERS
                      WHERE NVL(DWNFLAG,'N')='N'
                        AND ORDQTY>0
                        GROUP BY STORE_NO,ORDER_NO,COMPANYCODE,ITEMCODE HAVING COUNT(*)>1").Tables[0];

                if (dtCheck.Rows.Count > 0) {
                    string sERROR = "重複資料如下:\r\n";
                    foreach (DataRow dr in dtCheck.Rows) 
                    {
                        sERROR +=string.Format("STORE_NO:{0},ORDER_NO:{1},COMPANYCODE:{2},ITEMCODE:{3}\r\n"
                            , dr["STORE_NO"].ToString() 
                            , dr["ORDER_NO"].ToString() 
                            , dr["COMPANYCODE"].ToString() 
                            , dr["ITEMCODE"].ToString());
                    }
                    new Exception(sERROR);
                }

                dt = OracleDBUtil.GetDataSet(
                    trans,
                    @"SELECT  POS2ERP_ORDERS_ID, STORE_NO, ORDER_NO, 
                       ITEMCODE, COMPANYCODE, ORDDATE, 
                       ORDTYPE, ORDQTY, BRANCHNO, 
                       DWNFLAG, DWNDATE, POS2ERP_ORDER_ID, 
                       ORDER_ITEMS_ID, STATUS,
                       remained_atr (ITEMCODE, STORE_NO) AS ADVISEQTY,  --建議訂購量
                       INWAY_QTY(STORE_NO, ITEMCODE) AS INWAYQTY  --在途量
                    FROM POS2ERP_ORDERS
                    WHERE NVL(dwnflag,'N')='N'
                    AND ORDQTY>0"
                    //只抓QTY大於零的
                    ).Tables[0];

                if (dt.Rows.Count > 0)
                {
                    Console.WriteLine("    INSERT ERP_ORDERS(POS) ");
                    foreach (DataRow dr in dt.Rows)
                    {
                        #region upload erp_orders

                        StringBuilder sb = new StringBuilder();

                        sb.AppendLine(
                            @"Insert into ERP_ORDERS
                           (STORENO, 
                            SC_ORDERNO, 
                            ITEMCODE, COMPANYCODE, ORDDATE, ORDTYPE, 
                            ORDQTY, BRANCHNO)
                         Values
                           (:STORENO, 
                            :SC_ORDERNO, 
                            :ITEMCODE, :COMPANYCODE, :ORDDATE, :ORDTYPE, 
                            :ORDQTY, :BRANCHNO)"
                        );

                        sb.Replace(":STORENO", OracleDBUtil.SqlStr("R" + dr["STORE_NO"].ToString()));//PK
                        sb.Replace(":SC_ORDERNO", OracleDBUtil.SqlStr(dr["ORDER_NO"].ToString()));//PK
                        sb.Replace(":ORDTYPE", OracleDBUtil.SqlStr(dr["ORDTYPE"].ToString()));
                        sb.Replace(":COMPANYCODE", OracleDBUtil.SqlStr(dr["COMPANYCODE"].ToString()));//PK
                        sb.Replace(":ITEMCODE", OracleDBUtil.SqlStr(dr["ITEMCODE"].ToString()));//PK
                        sb.Replace(":ORDDATE", OracleDBUtil.DateStr(Convert.ToDateTime(dr["ORDDATE"]).ToString("yyyy/MM/dd")));
                        sb.Replace(":ORDQTY", OracleDBUtil.SqlStr(dr["ORDQTY"].ToString()));
                        sb.Replace(":BRANCHNO", OracleDBUtil.SqlStr(dr["BRANCHNO"].ToString()));

                        OracleDBUtil.ExecuteSql(trans_erp, sb.ToString());
                        
                        #endregion

                        #region upload process_order

                        sb.Length = 0;
                        sb.AppendLine(
                            @"Insert into PROCESSORDER
                           (STORENO, 
                            SC_ORDNO, PRODNO, COMPANYCODE, COLLECTDATE, ORDDATE, 
                            ORDQTY, BRANCHNO, TOSO, ADVISEQTY, APPROVEQTY, 
                            DIFFQTY, FLG_APPROVE, FLG_TRANS, OENO, VERIFYDATE, 
                            VERIFYUSRNO, TRANSDATE, TRANSUSRNO, UPDUSRNO, UPDDATE, 
                            COLLECTUSRNO, INWAYQTY, HQ_ORDNO, ORD_TYPE, FLG_DOWNLOAD, 
                            ULSNO, REMARK, COLLECTDATETIME, VERIFYDATETIME, TRANSDATETIME)
                         Values
                           (:STORENO, 
                            :SC_ORDNO, :PRODNO, :COMPANYCODE, NULL, :ORDDATE, 
                            :ORDQTY, :BRANCHNO, NULL, :ADVISEQTY, :APPROVEQTY, 
                            0, NULL, NULL, NULL, NULL, 
                            NULL, NULL, NULL, NULL, NULL, 
                            NULL, :INWAYQTY, NULL, NULL, NULL, 
                            NULL, NULL, NULL, NULL, NULL)"
                        );

                        sb.Replace(":STORENO", OracleDBUtil.SqlStr("R" + dr["STORE_NO"].ToString()));//PK
                        sb.Replace(":SC_ORDNO", OracleDBUtil.SqlStr(dr["ORDER_NO"].ToString()));//PK
                        sb.Replace(":COMPANYCODE", OracleDBUtil.SqlStr(dr["COMPANYCODE"].ToString()));//PK
                        sb.Replace(":PRODNO", OracleDBUtil.SqlStr(dr["ITEMCODE"].ToString()));//PK
                        sb.Replace(":ORDDATE", OracleDBUtil.SqlStr(Convert.ToDateTime(dr["ORDDATE"]).ToString("yyyyMMdd")));
                        sb.Replace(":ORDQTY", OracleDBUtil.SqlStr(dr["ORDQTY"].ToString()));
                        sb.Replace(":BRANCHNO", OracleDBUtil.SqlStr(dr["BRANCHNO"].ToString()));
                        sb.Replace(":ADVISEQTY", OracleDBUtil.SqlStr(dr["ORDQTY"].ToString()));
                        sb.Replace(":APPROVEQTY", OracleDBUtil.SqlStr(dr["ORDQTY"].ToString()));
                        sb.Replace(":INWAYQTY", OracleDBUtil.SqlStr(dr["INWAYQTY"].ToString()));

                        OracleDBUtil.ExecuteSql(trans_erp, sb.ToString());

                        #endregion
                    }
                }

                Console.WriteLine("    UPDATE POS2ERP_ORDERS(WEB) ");
                OracleDBUtil.ExecuteSql(
                    trans,
                    "update POS2ERP_ORDERS set dwnflag='Y', DWNDATE=TO_CHAR(SYSDATE,'YYYYMMDD'), STATUS='1' where NVL(dwnflag,'N')='N'");
                
                trans.Commit();
                trans_erp.Commit();
                Console.WriteLine("執行結束，寫入LOG");
                //成功資訊
                con_log.Success("NDS上傳筆數:" + order_count.ToString());
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
