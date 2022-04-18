using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Advtek.Utility;
using System.Data;
using System.Data.OracleClient;
using System.Threading;

namespace WEB_STORE_TRANSFER
{
    class Program
    {
        static OracleConnection pcon = null;
        static OracleConnection wcon = null;
        static string sMSG = "";

        static void Main(string[] args)
        {
            //初始化LOG
            OutputMsg("1.WEB_STORE_TRANSFER開始");
            OutputMsg("2.初始化LOG");
            ConvertLog con_log = new ConvertLog("WEB_STORE_TRANSFER");
            try {
                OutputMsg("3.建立新舊連線");
                pcon = getOldConnection();
                wcon = OracleDBUtil.GetConnection();

                OutputMsg("4.WEB_STORETRANSFERM_TEMP+WEB_STORETRANSFERD_TEMP)[POS] => (WEB_STORETRANSFERM_TEMP+WEB_STORETRANSFERD_TEMP)[WEB]");
                insertWebTransferMAndD();

                OutputMsg("5.PK_CONVERT.SP_STORE_TRANSFER");
                OracleTransaction wtx = wcon.BeginTransaction();
                try {
                    OracleParameter op = new OracleParameter("outMESSAGE", OracleType.VarChar);
                    op.Size = 2000;
                    op.Direction = ParameterDirection.Output;
                    OracleDBUtil.ExecuteSql_SP(
                        wtx
                        , "PK_CONVERT.SP_STORE_TRANSFER"
                        , op
                        );

                    wtx.Commit();
                    OutputMsg(op.Value.ToString());

                }
                catch (Exception ex) {
                   if (wtx!=null) wtx.Rollback();
                   throw ex;
                }
                OutputMsg("6.執行結束，寫入LOG");

                con_log.Success(sMSG);

            }
            catch (Exception ex) {
                OutputMsg(ex.Message);
                con_log.Fail(sMSG);
                Thread.Sleep(3000);
            }
            finally {
                DisposeConnection(pcon);
                DisposeConnection(wcon);
                OracleConnection.ClearAllPools();
            }
            

        }

        static void insertWebTransferMAndD()
        {
            OracleTransaction wtx = null;
            try {
                OracleDBUtil.ExecuteSql(pcon, "update WEB_STORETRANSFERM set ULSNO='T' where ULSNO IS NULL");
                string sSEQ = "WB" + DateTime.Now.ToString("yyyyMMdd")+ getSeq();
                wtx = wcon.BeginTransaction();
                string sql = @"select STORENO, STNO, VERSION,
                                  STDATE, TSTORENO, TSTATUS, 
                                  REMARK, UPDDATE, UPDUSRNO, 
                                  STUSRNO, TSTDATE, TREMARK, 
                                  TSTUSRNO, CCOK, TUPDDATE, 
                                  TUPDUSRNO, ULSNO, GETDATE, 
                                  LOC, UPDATELOC, MARKNO, 
                                  COMPANYCODE, FLG_TOES, TRANSTYPEID from WEB_STORETRANSFERM WHERE ULSNO ='T' ";
                string sTemplate = @" INSERT INTO WEB_STORETRANSFERM_TEMP (STORENO, STNO, VERSION,
                                     STDATE, TSTORENO, TSTATUS, REMARK, UPDDATE, UPDUSRNO, 
                                     STUSRNO, TSTDATE, TREMARK, TSTUSRNO, CCOK, TUPDDATE, 
                                     TUPDUSRNO, ULSNO, GETDATE, LOC, UPDATELOC, MARKNO, 
                                     COMPANYCODE, FLG_TOES, TRANSTYPEID) 
                                   values ([STORENO], [STNO], [VERSION],
                                     [STDATE], [TSTORENO], [TSTATUS], [REMARK], [UPDDATE], [UPDUSRNO], 
                                     [STUSRNO], [TSTDATE], [TREMARK], [TSTUSRNO], [CCOK], [TUPDDATE], 
                                     [TUPDUSRNO], [ULSNO], [GETDATE], [LOC], [UPDATELOC], [MARKNO], " +
                                     "[COMPANYCODE], [FLG_TOES], [TRANSTYPEID]); ";
                insertTEMP_TABLE("WEB_STORETRANSFERM", "WEB_STORETRANSFERM_TEMP", sql, sTemplate,wtx);

                sql = @"SELECT STORENO, STNO, SEQNO,PRODNO, TRANOUTQTY, TRANINQTY,UPDUSRNO, UPDDATE, FLG_TOERP, COMPANYCODE, VERSION, TRANSTYPEID
                          FROM WEB_STORETRANSFERD 
                         WHERE (STORENO,STNO,VERSION,COMPANYCODE) IN 
                               (SELECT STORENO,STNO,VERSION,COMPANYCODE FROM WEB_STORETRANSFERM WHERE ULSNO ='T')";

                sTemplate = @"INSERT INTO WEB_STORETRANSFERD_TEMP ( STORENO, STNO, SEQNO, 
                                PRODNO, TRANOUTQTY, TRANINQTY, 
                                UPDUSRNO, UPDDATE, FLG_TOERP, 
                                COMPANYCODE, VERSION, TRANSTYPEID )
                              values ( [STORENO], [STNO], [SEQNO], 
                                [PRODNO], [TRANOUTQTY], [TRANINQTY], 
                                [UPDUSRNO], [UPDDATE], [FLG_TOERP], 
                                [COMPANYCODE], [VERSION], [TRANSTYPEID]); ";

                insertTEMP_TABLE("WEB_STORETRANSFERD", "WEB_STORETRANSFERD_TEMP", sql, sTemplate, wtx);

                ExeSql("update WEB_STORETRANSFERM_TEMP set ULSNO='" + sSEQ + "' ", wtx);

                ExeSql("update WEB_STORETRANSFERM set ULSNO='" + sSEQ + "' where ULSNO='T' ", pcon);
                wtx.Commit();
            }
            catch (Exception ex) {
                throw ex;
            }
        }

       
        static string getSeq() {
            string sRet = "";
            DataTable dt = new DataTable();
            string sToday=DateTime.Now.ToString("yyyyMMdd");
            string sql = @"select to_number(max(seq_no))+1 from schedule_seq where scheduleid='STORE_TRANSFER' and  runningdate='" + sToday + "'";
            OracleTransaction wtx = wcon.BeginTransaction();
            dt = OracleDBUtil.GetDataSet(wtx, sql).Tables[0];
            try {
                sRet = dt.Rows[0][0].ToString().PadLeft(3, '0');
                if (sRet.Equals("000"))
                {
                    sRet = "001";
                    ExeSql(@"insert into schedule_seq ( ID, SCHEDULEID, RUNNINGDATE, SEQ_NO)
                                                   values (pos_uuid(),'STORE_TRANSFER','" + sToday + "','001')",wtx);
                }
                else
                {
                    sRet = dt.Rows[0][0].ToString().PadLeft(3, '0');
                    ExeSql(@" update schedule_seq set seq_no='" +sRet +"' " +
                                                  " where scheduleid='STORE_TRANSFER' and  runningdate='" + sToday + "' ",wtx);
                }
                wtx.Commit();
            }
            catch (Exception ex) {
                throw ex;
            }
            
            return sRet;
        }
        static void insertTEMP_TABLE(string FromTable,string ToTable,string SourceSql, string SqlTemplate,OracleTransaction tx)
        {
            OracleTransaction wotx = tx;
            StringBuilder sb = new StringBuilder();
            try
            {
                OutputMsg("****************************");
                OutputMsg(string.Format("{0}(POS)，查詢資料", FromTable));
                DataTable dtPOS = SelectTable(SourceSql.Replace("\r"," ").Replace("\n"," "), pcon);

                #region 不清除TEMP檔
                //清除TEMP檔
                //wotx = tx;
                OutputMsg(string.Format("{0}(WEB)，TEMP清除資料", ToTable));
                sb.Length = 0;
                sb.Append(string.Format("delete from {0}", ToTable));
                ExeSql(sb.ToString(), wotx);
                //wotx.Commit();
                #endregion

                int iTEN = 0;
                int iCount = 0;
                if (dtPOS.Rows.Count > 0)
                {
                    OutputMsg(string.Format("{0}(WEB)，TEMP寫入資料", ToTable));
                    //wotx = wcon.BeginTransaction();
                    StringBuilder sb2 = new StringBuilder();//local StringBuilder
                    sb.Length = 0;
                    foreach (DataRow dr in dtPOS.Rows)
                    {
                        if (iTEN > 200)
                        {
                            sb.Insert(0, "BEGIN ");
                            sb.Append(" END;");
                            ExeSql(sb.Replace("\r", " ").Replace("\n", " ").ToString(), wotx);

                            sb.Length = 0;
                            iTEN = 0;

                        }

                        sb2.Length = 0;
                        sb2.Append(SqlTemplate);
                        foreach (DataColumn dc in dtPOS.Columns)
                        {
                            string dcname = dc.ColumnName;
                            switch (dc.DataType.ToString().ToUpper())
                            {
                                case "SYSTEM.STRING":
                                    //if (!dcname.ToUpper().Equals("ULSNO"))
                                    //{
                                        sb2.Replace("[" + dcname + "]", OracleDBUtil.SqlStr(dr[dcname].ToString()));
                                    //}
                                    break;
                                case "SYSTEM.DECIMAL":
                                    sb2.Replace("[" + dcname + "]", (dr[dcname] != DBNull.Value) ? dr[dcname].ToString() : "0");
                                    break;
                                case "SYSTEM.DATETIME":
                                    sb2.Replace("[" + dcname + "]", (dr[dcname] != DBNull.Value) ? OracleDBUtil.DateFormate(dr[dcname]) : "null");
                                    break;
                                default:
                                    sb2.Replace("[" + dcname + "]", OracleDBUtil.SqlStr(dr[dcname].ToString()));
                                    break;
                            }
                        }

                        //sb.AppendLine(sb2.ToString());
                        sb.Append(sb2.ToString());
                        iTEN++;
                        iCount++;
                    }
                    if (sb.Length > 0)
                    {
                        sb.Insert(0, "BEGIN ");
                        sb.Append(" END;");
                        ExeSql(sb.Replace("\r", " ").Replace("\n", " ").ToString(), wotx);

                        sb.Length = 0;
                        iTEN = 0;
                    }
                    OutputMsg(string.Format("{0}(WEB)，新增筆數:{1}", ToTable, iCount.ToString()));

                    //wotx.Commit();
                    
                }
                else
                {
                    //NO DATA
                    OutputMsg(string.Format("{0}(WEB)，新增筆數:0", ToTable));
                }
                OutputMsg("****************************");
            }
            catch (Exception ex)
            {
                //if (wotx != null) wotx.Rollback();
                OutputMsg(string.Format("{0}(WEB)，寫入資料時，產生例外。", ToTable));
                OutputMsg(sb.ToString());
                throw ex;
            }
            finally
            {
                //if (wotx != null) wotx.Dispose();
            }
        }

        static OracleConnection getOldConnection()
        {
            return OracleDBUtil.GetConnectionByParaKey("OLD_POS_USER", "OLD_POS_PW", "OLD_POS_HOST", "OLD_POS_DBSID", "OLD_POS_DBPORT");
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

        static int ExeSql(string sql, OracleConnection con)
        {
            OracleTransaction otx = null;
            int i = 0;
            try
            {
                otx = con.BeginTransaction();
                i = OracleDBUtil.ExecuteSql(otx, sql);
                otx.Commit();
            }
            catch (Exception ex)
            {
                otx.Rollback();
                throw ex;
            }
            finally
            {
                if (otx != null) otx.Dispose();
            }
            return i;
        }

        static int ExeSql(string sql, OracleTransaction otx)
        {
            int i = 0;
            try
            {
                i = OracleDBUtil.ExecuteSql(otx, sql);
            }
            catch (Exception ex)
            {
                otx.Rollback();
                throw ex;
            }
            return i;
        }


        static void OutputMsg(string s)
        {
            Console.WriteLine(s);
            sMSG += s + "\r\n";
        }

        static void DisposeConnection(OracleConnection con)
        {
            if (con != null)
            {
                if (con.State == ConnectionState.Open) con.Close();
                con.Dispose();
            }
        }
    }
}
