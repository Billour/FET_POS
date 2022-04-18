using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Advtek.Utility;
using System.Data;
using System.Data.OracleClient;
using System.Threading;

namespace UPLOADPOSRT_GUI
{
    class Program
    {
        static OracleConnection pcon = null;
        static OracleConnection wcon = null;
        static string sMSG = "";
        static void Main(string[] args)
        {

            //初始化LOG
            OutputMsg("1.UPLOADPOSRT_GUI(折讓單上傳)");
            OutputMsg("2.初始化LOG");
            ConvertLog cLog = new ConvertLog("UPLOADPOSRT_GUI");
            try
            {
                OutputMsg("2.1 建立新舊連線");
                wcon = OracleDBUtil.GetConnection();
                pcon = OracleDBUtil.GetERPPOSConnection();

                OutputMsg("3.執行SP_UPLOADPOSRT_GUI");
                string sMessage = SP_UPLOADPOSRT_GUI();
                OutputMsg(sMessage);

                OutputMsg("4.POS2ERP_RT_GUI(WEB) => ERP_RT_GUI(POS)");
                insertERP_RT_GUI();

                OutputMsg("5.執行結束，寫入LOG");
                cLog.Success(sMSG);

                Thread.Sleep(3000);
            }
            catch (Exception ex)
            {
                OutputMsg(ex.Message);
                cLog.Fail(sMSG);
                Thread.Sleep(3000);
            }
            finally
            {
                if (wcon != null)
                {
                    if (wcon.State == ConnectionState.Open) wcon.Close();
                    wcon.Dispose();
                }
                if (pcon != null)
                {
                    if (pcon.State == ConnectionState.Open) pcon.Close();
                    pcon.Dispose();
                }
                OracleConnection.ClearAllPools();
            }
        }

        static string SP_UPLOADPOSRT_GUI() {
            OracleConnection objConn = null;
            OracleTransaction objTX = null;
            string sRet = "";
            try
            {
                objConn = OracleDBUtil.GetConnection();
                objTX = objConn.BeginTransaction();

                //OracleDBUtil.ExecuteSql_SP(objTX, "SP_COUNT_RECOMMANDED");
                OracleCommand oraCmd = new OracleCommand("PK_CONVERT.SP_UPLOADPOSRT_GUI");
                oraCmd.CommandType = System.Data.CommandType.StoredProcedure;
                oraCmd.Parameters.Add(new OracleParameter("OUTMESSAGE", OracleType.VarChar, 2000)).Direction = ParameterDirection.Output;
                oraCmd.Connection = objConn;
                oraCmd.Transaction = objTX;
                oraCmd.ExecuteNonQuery();
                sRet = oraCmd.Parameters["OUTMESSAGE"].Value.ToString();
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
        static void OutputMsg(string s)
        {
            Console.WriteLine(s);
            sMSG += s + "\r\n";
        }

        static void insertERP_RT_GUI() {
            OracleTransaction potx = pcon.BeginTransaction();
            
            StringBuilder sb = new StringBuilder();
            string FromTable = "POS2ERP_RT_GUI";
            string ToTable = "ERP_RT_GUI";
            string SourceSql = @"SELECT storeno, TRUNC (guidate) AS guidate, guino, rtno,
                                     TRUNC (rtdate) AS rtdate, canflag, companycode, 
                                     corpno, taxsale, tax,'0' dwnflag, 
                                     SYSDATE AS cretae_dtm, 'WEBPOS' AS cretae_user,
                                     SYSDATE AS modi_dtm, 'WEBPOS' AS modi_user
                                 FROM pos2erp_rt_gui WHERE cc2erp_flag = 'T'";
            string SqlTemplate = @"INSERT INTO ERP_RT_GUI (STORENO,GUIDATE,GUINO,RTNO,RTDATE,
CANFLAG,COMPANYCODE,CORPNO,TAXSALE,
TAX,DWNFLAG,CRETAE_DTM,CRETAE_USER,
MODI_DTM,MODI_USER
) VALUES (
[STORENO],[GUIDATE],[GUINO],[RTNO],[RTDATE],
[CANFLAG],[COMPANYCODE],[CORPNO],[TAXSALE],
[TAX],[DWNFLAG],[CRETAE_DTM],[CRETAE_USER],
[MODI_DTM],[MODI_USER]);";
            string sqlUpdate = "UPDATE POS2ERP_RT_GUI SET CC2ERP_FLAG = '1',CC2ERP_DTM = sysdate  WHERE CC2ERP_FLAG = 'T'";
            try
            {
                
                OutputMsg("****************************");
                OutputMsg(string.Format("{0}(POS)，查詢資料", FromTable));
                DataTable dtPOS = SelectTable(SourceSql.Replace("\r", " ").Replace("\n", " "), wcon);
                

                #region 不清除TEMP檔
                //清除TEMP檔
                //wotx = tx;
                //OutputMsg(string.Format("{0}(WEB)，TEMP清除資料", ToTable));
                //sb.Length = 0;
                //sb.Append(string.Format("delete from {0}", ToTable));
                //ExeSql(sb.ToString(), wotx);
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
                            ExeSql(sb.Replace("\r", " ").Replace("\n", " ").ToString(), potx);

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
                        ExeSql(sb.Replace("\r", " ").Replace("\n", " ").ToString(), potx);

                        sb.Length = 0;
                        iTEN = 0;
                    }
                    OutputMsg(string.Format("{0}(WEB)，新增筆數:{1}", ToTable, iCount.ToString()));
                }
                else
                {
                    //NO DATA
                    OutputMsg(string.Format("{0}(WEB)，新增筆數:0", ToTable));
                }
                OutputMsg(string.Format("{0}(WEB)，回壓FLAG", FromTable));
                OracleTransaction wotx = wcon.BeginTransaction();//20110324
                ExeSql(sqlUpdate, wotx);
                OutputMsg("****************************");
                potx.Commit();
                wotx.Commit();

            }
            catch (Exception ex)
            {
                OutputMsg(string.Format("{0}(WEB)，寫入資料時，產生例外。", ToTable));
                OutputMsg(sb.ToString());
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
    }
}
