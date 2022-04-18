using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.OracleClient;

using Advtek.Utility;
using System.Threading;

namespace INV_TRAN_LOG_UPLOAD
{
    class Program
    {
        static string sMSG = "";
        static void Main(string[] args)
        {
            //初始化LOG
            OutputMsg("1.INV_TRAN_LOG_UPLOAD開始");
            OutputMsg("2.初始化LOG");
            ConvertLog con_log = new ConvertLog("INV_TRAN_LOG_UPLOAD");
            OutputMsg("3.建立連線");
            OracleConnection objConn = OracleDBUtil.GetConnection();

            try
            {
                OutputMsg("4.取得BATCH NO");
                string BATCH_NO = GuidNo.getUUID();
                OutputMsg("5.INV_TRAN_LOG(WEB) => POS2ERP_TSTOCKS(WEB)，BATCH_NO:"+BATCH_NO);
                SetTemp(objConn, BATCH_NO);

                DataTable dtTmp = GetDuplicateTemp(objConn, BATCH_NO);
                if (dtTmp.Rows.Count > 0) {
                    StringBuilder sb2 = new StringBuilder();
                    sb2.AppendLine("POS2ERP_TSTOCKS(WEB)有重複性資料，請先確定再執行排程");
                    sb2.AppendLine(string.Format("{0},{1},{2},{3},{4},{5}","TXTTYPE", "ASTORENO","ITEMCODE","TXTNO","COMPANYCODE","CNT"));
                    foreach (DataRow dr in dtTmp.Rows) {
                        //TXTTYPE,ASTORENO,ITEMCODE,TXTNO,COMPANYCODE,COUNT(1) AS CNT 
                        sb2.AppendLine(string.Format("{0},{1},{2},{3},{4},{5}",dr["TXTTYPE"].ToString(),dr["ASTORENO"].ToString(),dr["ITEMCODE"].ToString(),dr["TXTNO"].ToString(),dr["COMPANYCODE"].ToString(),dr["CNT"].ToString()));
                    }
                    throw new Exception(sb2.ToString());
                } 

                OutputMsg("5_1.查詢POS2ERP_TSTOCKS(WEB)");
                DataTable dt = GetTemp(objConn, BATCH_NO);
                
                OutputMsg("6.POS2ERP_TSTOCKS(WEB) => ERP_TSTOCKS(POS)");
                if (dt.Rows.Count > 0)
                {
                    //using (OracleConnection objConn_erp = OracleDBUtil.GetConnection())***20110216 取錯連線
                    using (OracleConnection objConn_erp = OracleDBUtil.GetERPPOSConnection() )
                    {
                        foreach (DataRow dr in dt.Rows)
                        {
                            StringBuilder sb = new StringBuilder();

                            sb.AppendLine(
                            @"INSERT INTO ERP_TSTOCKS VALUES(
                            :TXTTYPE, :ASTORENO, :ITEMCODE, :TXTNO,
                            :COMPANYCODE, :TXTDATE, :TXTQTY, :REASON, :COSTCENTER, :ACCOUNTCODE,
                            :BSTORENO, :DWNFLAG, :DWNDATE, :SEAL_NO, :RETURN_REASON_CODE, 
                            :AFTER_PROCESS_CODE, :ALOCATION, :BLOCATION 
                        )"
                            );

                            sb.Replace(":TXTTYPE", OracleDBUtil.SqlStr(dr["TXTTYPE"].ToString()));//pk
                            //STORE_NO回寫舊POS要加R，2121 > R2121
                            sb.Replace(":ASTORENO", OracleDBUtil.SqlStr("R"+dr["ASTORENO"].ToString()));//pk
                            sb.Replace(":ITEMCODE", OracleDBUtil.SqlStr(dr["ITEMCODE"].ToString()));//pk
                            sb.Replace(":TXTNO", OracleDBUtil.SqlStr(dr["TXTNO"].ToString()));//pk
                            sb.Replace(":COMPANYCODE", OracleDBUtil.SqlStr(dr["COMPANYCODE"].ToString()));//pk
                            //sb.Replace(":TXTDATE", OracleDBUtil.SqlStr(dr["TXTDATE"].ToString()));
                            sb.Replace(":TXTDATE", OracleDBUtil.DateStr(dr["TXTDATE"].ToString()));
                            sb.Replace(":TXTQTY", OracleDBUtil.SqlStr(dr["TXTQTY"].ToString()));
                            sb.Replace(":REASON", OracleDBUtil.SqlStr(dr["REASON"].ToString()));//***
                            sb.Replace(":COSTCENTER", OracleDBUtil.SqlStr(dr["COSTCENTER"].ToString()));//***
                            sb.Replace(":ACCOUNTCODE", OracleDBUtil.SqlStr(dr["ACCOUNTCODE"].ToString()));//**
                            sb.Replace(":BSTORENO", OracleDBUtil.SqlStr("R"+dr["BSTORENO"].ToString()));//**OO
                            sb.Replace(":DWNFLAG", OracleDBUtil.SqlStr(dr["DWNFLAG"].ToString()));
                            //sb.Replace(":DWNDATE", OracleDBUtil.SqlStr(dr["DWNDATE"].ToString()));
                            sb.Replace(":DWNDATE", OracleDBUtil.DateStr(dr["DWNDATE"].ToString()));
                            sb.Replace(":SEAL_NO", OracleDBUtil.SqlStr(dr["SEAL_NO"].ToString()));
                            sb.Replace(":RETURN_REASON_CODE", OracleDBUtil.SqlStr(dr["RETURN_REASON_CODE"].ToString()));
                            sb.Replace(":AFTER_PROCESS_CODE", OracleDBUtil.SqlStr(dr["AFTER_PROCESS_CODE"].ToString()));
                            sb.Replace(":ALOCATION", OracleDBUtil.SqlStr(dr["ALOCATION"].ToString()));
                            sb.Replace(":BLOCATION", OracleDBUtil.SqlStr(dr["BLOCATION"].ToString()));

                            OracleDBUtil.ExecuteSql(objConn_erp, sb.ToString());
                        }
                    }
                    //***20110225 DWNDATE :YYYY/MM/DD
                    OutputMsg("7.POS2ERP_TSTOCKS(WEB)更新狀態為1");
                    OracleDBUtil.ExecuteSql(objConn, @"Update POS2ERP_TSTOCKS set DWNFLAG=1 , DWNDATE=to_char(sysdate,'YYYY/MM/DD')
                                                    where BATCH_NO =" + OracleDBUtil.SqlStr(BATCH_NO));
                    OutputMsg("8.inv_tran_log(WEB)更新狀態為Y");
                    OracleDBUtil.ExecuteSql(objConn, @"update inv_tran_log set ERP_FLAG='Y' , TRAN_TO_ERP_DTM=SYSDATE 
                                                where INVENTORY_LOG_ID in 
                                                (select INVENTORY_LOG_ID from POS2ERP_TSTOCKS 
                                                 where BATCH_NO =" + OracleDBUtil.SqlStr(BATCH_NO) + ")");
                }

                //成功資訊
                OutputMsg("9.ERP_TSTOCKS(POS):新增筆數" + dt.Rows.Count.ToString());
                OutputMsg("10.執行結束，寫入LOG");
                con_log.Success(sMSG);
                Thread.Sleep(5000);
            }
            catch (Exception ex)
            {
                //失敗資訊
                OutputMsg(ex.Message);
                con_log.Fail(sMSG);
                Thread.Sleep(5000);
            }
            finally
            {
                if (objConn.State == ConnectionState.Open) objConn.Close();
                objConn.Dispose();
                OracleConnection.ClearAllPools();
            }
        }

        private static DataTable GetTemp(OracleConnection objConn, string BATCH_NO)
        {
            DataTable dt = new DataTable();

            StringBuilder sb = new StringBuilder();

            sb.AppendLine(
                @"SELECT * FROM POS2ERP_TSTOCKS WHERE BATCH_NO=" + OracleDBUtil.SqlStr(BATCH_NO)
            );
            try {
                dt = OracleDBUtil.GetDataSet(objConn, sb.ToString()).Tables[0];
            }
            catch (Exception ex) {
                OutputMsg("5_1.查詢POS2ERP_TSTOCKS(WEB) 例外產生");
                throw ex;
            }
            

            return dt;
        }

        private static DataTable GetDuplicateTemp(OracleConnection objConn, string BATCH_NO)
        {
            DataTable dt = new DataTable();

            StringBuilder sb = new StringBuilder();

            sb.AppendLine(
                @"SELECT TXTTYPE,ASTORENO,ITEMCODE,TXTNO,COMPANYCODE,COUNT(1) AS CNT 
                  FROM POS2ERP_TSTOCKS 
                  WHERE BATCH_NO=" + OracleDBUtil.SqlStr(BATCH_NO) + 
                 " GROUP BY    TXTTYPE,ASTORENO,ITEMCODE,TXTNO,COMPANYCODE HAVING COUNT(1) >1 "
            );
            try
            {
                dt = OracleDBUtil.GetDataSet(objConn, sb.ToString()).Tables[0];
            }
            catch (Exception ex)
            {
                OutputMsg("5_1.查詢POS2ERP_TSTOCKS(WEB) 例外產生");
                throw ex;
            }


            return dt;
        }

        private static void SetTemp(OracleConnection objConn, string BATCH_NO)
        {
            StringBuilder sb = new StringBuilder();
            //***20110225 txtdate > YYYY/MM/DD
            sb.AppendLine(
                @"INSERT INTO POS2ERP_TSTOCKS(
                txttype,
               astoreno, itemcode, txtno,
               companycode, txtdate, txtqty,
               bstoreno, dwnflag, dwndate, alocation,
               blocation, batch_no, INVENTORY_LOG_ID,
               COSTCENTER,REASON,ACCOUNTCODE
                )
SELECT DECODE (inv_tran_type,
               'RO', 'R',                                           --門市退倉
               'AI', 'A',                                       --總部庫存調整
               'AO', 'A',                                       --總部庫存調整
               'SO', 'T',                                       --門市移出撥入
               'SI', 'T'                                            --門市撥入
              ) AS txttype,
       store_no AS astoreno, 
       prodno AS itemcode, 
       sheet_no AS txtno,
       '01' AS companycode, 
       (TO_CHAR (inv_tran_dtm, 'YYYY/MM/DD')
                            ) AS txtdate, 
       inv_qty AS txtqty,
       DECODE(inv_tran_type ,
              'SO' ,(select distinct TO_STORE_NO   FROM STORETRANSFER_M WHERE STNO =inv_tran_log.SHEET_NO) ,
              'SI' ,(select distinct FROM_STORE_NO FROM STORETRANSFER_M WHERE STNO =inv_tran_log.SHEET_NO),
              store_no ) bstoreno,
       '0' AS dwnflag, 
       '' AS dwndate, 
       '' AS alocation,
       '' AS blocation, 
       :BATCH_NO AS batch_no, 
       inventory_log_id,
        DECODE( inv_tran_type,
        'SO' , NULL,
       (SELECT costcenter
          FROM STORE
         WHERE STORE.store_no = inv_tran_log.store_no
           AND ROWNUM = 1)) AS costcenter,
       (SELECT stockadjd.stockadj_reason_code
          FROM stockadjm INNER JOIN stockadjd
               ON stockadjm.adjno = stockadjd.adjno
         WHERE stockadjm.adjno = inv_tran_log.sheet_no
           AND stockadjm.store_no = inv_tran_log.store_no
           AND stockadjd.prodno = inv_tran_log.prodno
           AND ROWNUM = 1) AS reason,
       (SELECT accno
          FROM reason
         WHERE reasonid =
                  (SELECT stockadjd.stockadj_reason_code
                     FROM stockadjm INNER JOIN stockadjd
                          ON stockadjm.adjno = stockadjd.adjno
                    WHERE stockadjm.adjno =
                                         inv_tran_log.sheet_no
                      AND stockadjm.store_no = inv_tran_log.store_no
                      AND stockadjd.prodno = inv_tran_log.prodno
                      AND ROWNUM = 1)) AS accountcode
  FROM inv_tran_log
 WHERE inv_tran_type IN ('RO', 'AI', 'AO','SO')
       AND UPPER (NVL (erp_flag, 'N')) = 'N'"
            );
            
            sb.Replace(":BATCH_NO", OracleDBUtil.SqlStr(BATCH_NO));
            try {
                OracleDBUtil.ExecuteSql(objConn, sb.ToString());
            }
            catch (Exception ex) {
                OutputMsg("5.INV_TRAN_LOG(WEB) => POS2ERP_TSTOCKS(WEB) 例外產生");
                throw ex;
            }
            
        }

        static DataTable SelectTable(string sql, OracleConnection con)
        {
            DataTable dt = new DataTable();
            try
            {
                dt = OracleDBUtil.GetDataSet(con, sql).Tables[0];
            }
            catch (Exception ex) { throw ex; }
            return dt;
        }

        static void OutputMsg(string s)
        {
            Console.WriteLine(s);
            sMSG += s + "\r\n";
        }
    }
}
