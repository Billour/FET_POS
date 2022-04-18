using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.OracleClient;
using Advtek.Utility;

using System.Threading;


namespace INVOICE_INVALID
{
    class Program
    {
        static string sMSG = "";
        static OracleConnection wcon = null;
        static OracleConnection pcon = null;
        static void Main(string[] args)
        {
            //初始化LOG
            OutputMsg("1.INVOICE_INVALID(發票過期作廢)");
            OutputMsg("2.初始化LOG");
            ConvertLog cLog = new ConvertLog("INVOICE_INVALID");
            try
            {
                OutputMsg("2.1 建立新舊連線");
                wcon = OracleDBUtil.GetConnection();
                pcon = OracleDBUtil.GetERPPOSConnection();

                OutputMsg("3.執行PK_CONVERT.SP_INVOICE_INVALID");
                string sMessage = SP_INVOICE_INVALID();
                OutputMsg(sMessage);

                OutputMsg("4.POS2ERP_RELEASE_GUI_TEMP(WEB) => POS2ERP_RELEASE_GUI(POS)");
                insertERP_GUI_TEMP();

                OutputMsg("6.ERP_GUI_TEMP(WEB) => ERP_GUI(POS)");
                insertPOS2ERP_RELEASE_GUI();

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
                if (wcon != null) {
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

        static string SP_INVOICE_INVALID()
        {
            OracleConnection objConn = null;
            OracleTransaction objTX = null;
            string sRet = "";
            try
            {
                objConn = OracleDBUtil.GetConnection();
                objTX = objConn.BeginTransaction();

                //OracleDBUtil.ExecuteSql_SP(objTX, "SP_COUNT_RECOMMANDED");
                OracleCommand oraCmd = new OracleCommand("PK_CONVERT.SP_INVOICE_INVALID");
                oraCmd.CommandType = System.Data.CommandType.StoredProcedure;
                OracleParameter p = new OracleParameter("SDATE", DBNull.Value);
                p.OracleType = OracleType.VarChar;
                p.Size = 10;
                p.Direction = ParameterDirection.Input;
                oraCmd.Parameters.Add(p);
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

        #region 程式備份 20110318
        //static void Main(string[] args)
        //{
        //    //初始化LOG
        //    OutputMsg("1.INVOICE_INVALID(發票過期作廢)");
        //    OutputMsg("2.初始化LOG");
        //    ConvertLog cLog = new ConvertLog("INVOICE_INVALID");
        //    try
        //    {
        //        OutputMsg("3.執行PK_INVOICE.SP_INVOICE_INVALID");
        //        string sMessage = SP_INVOICE_INVALID();
        //        OutputMsg("********************");
        //        OutputMsg(sMessage);
        //        OutputMsg("********************");
        //        cLog.Success(sMSG);
        //        OutputMsg("4.執行結束，寫入LOG");
        //        Thread.Sleep(3000);
        //    }
        //    catch (Exception ex)
        //    {
        //        OutputMsg(ex.Message);
        //        cLog.Fail(sMSG);
        //        Thread.Sleep(3000);
        //    }
        //}

        //static string SP_INVOICE_INVALID()
        //{
        //    OracleConnection objConn = null;
        //    OracleTransaction objTX = null;
        //    string sRet = "";
        //    try
        //    {
        //        objConn = OracleDBUtil.GetConnection();
        //        objTX = objConn.BeginTransaction();

        //        //OracleDBUtil.ExecuteSql_SP(objTX, "SP_COUNT_RECOMMANDED");
        //        OracleCommand oraCmd = new OracleCommand("PK_CONVERT.SP_INVOICE_INVALID");
        //        oraCmd.CommandType = System.Data.CommandType.StoredProcedure;
        //        OracleParameter p = new OracleParameter("sDATE", DBNull.Value);
        //        p.OracleType = OracleType.VarChar;
        //        p.Size = 10;
        //        p.Direction = ParameterDirection.Input;
        //        oraCmd.Parameters.Add(p);
        //        oraCmd.Parameters.Add(new OracleParameter("outMESSAGE", OracleType.VarChar, 2000)).Direction = ParameterDirection.Output;
        //        oraCmd.Connection = objConn;
        //        oraCmd.Transaction = objTX;
        //        oraCmd.ExecuteNonQuery();
        //        sRet = oraCmd.Parameters["outMESSAGE"].Value.ToString();
        //        objTX.Commit();

        //    }
        //    catch (Exception ex)
        //    {
        //        objTX.Rollback();
        //        throw ex;
        //    }
        //    finally
        //    {
        //        objTX.Dispose();
        //        if (objConn.State == ConnectionState.Open) objConn.Close();
        //        objConn.Dispose();
        //        OracleConnection.ClearAllPools();
        //    }
        //    return sRet;
        //}
        #endregion

        static void OutputMsg(string s)
        {
            Console.WriteLine(s);
            sMSG += s + "\r\n";
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

        static void insertERP_GUI_TEMP() {
            string FromTable = "ERP_GUI_TEMP";
            string ToTable = "ERP_GUI";
            string SourceSql = "select GUIDATE, GUINO, CANFLAG,COMPANYCODE, CORPNO, TAXSALE, TAX, DWNFLAG, DWNDATE, STORENO from ERP_GUI_TEMP where dwnflag='T' ";
            string SqlTemplate = @"
insert into ERP_GUI (GUIDATE, GUINO, CANFLAG, 
COMPANYCODE, CORPNO, TAXSALE, 
TAX, DWNFLAG, DWNDATE, 
STORENO) values (
[GUIDATE], [GUINO], [CANFLAG], 
[COMPANYCODE], [CORPNO], [TAXSALE], 
[TAX], [DWNFLAG], [DWNDATE], 
[STORENO]
);";
            string sqlUpdate = "update ERP_GUI_TEMP set dwnflag='1',dwndate=sysdate where dwnflag='T'";

            insertTEMP_TABLE(FromTable, ToTable, SourceSql, SqlTemplate,sqlUpdate);
        }

        static void insertPOS2ERP_RELEASE_GUI() {
            string sql = @"";
            string FromTable = "POS2ERP_RT_GUI_TEMP";
            string ToTable = "INVOICE_INVALID";
            string SourceSql = @"select  STORENO, INVO_BYYMM, INVOTYPENO, 
   INVOCHAR, INVO_BNO, COMPANYCODE, 
   INVO_EYYMM, INVO_ENO, UNINO, 
   TAXNO, DWNFALG, DWNDATE from POS2ERP_RT_GUI_TEMP where dwnflag='T' ";
            string SqlTemplate = @"
insert into INVOICE_INVALID (STORENO, INVO_BYYMM, INVOTYPENO, 
   INVOCHAR, INVO_BNO, COMPANYCODE, 
   INVO_EYYMM, INVO_ENO, UNINO, 
   TAXNO, DWNFALG, DWNDATE) values (
[STORENO], [INVO_BYYMM], [INVOTYPENO], 
[INVOCHAR], [INVO_BNO], [COMPANYCODE], 
[INVO_EYYMM], [INVO_ENO], [UNINO], 
[TAXNO], [DWNFALG], [DWNDATE]
);";
            string sqlUpdate = "update POS2ERP_RELEASE_GUI_TEMP set dwnflag='1',dwndate=sysdate where dwnflag='T'";

            insertTEMP_TABLE(FromTable, ToTable, SourceSql, SqlTemplate, sqlUpdate);
        }

        static void insertTEMP_TABLE(string FromTable, string ToTable, string SourceSql, string SqlTemplate ,string sqlUpdate)
        {
            OracleTransaction potx = pcon.BeginTransaction();
            StringBuilder sb = new StringBuilder();
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
                    OutputMsg(string.Format("{0}(POS)，新增筆數:{1}", ToTable, iCount.ToString()));
                }
                else
                {
                    //NO DATA
                    OutputMsg(string.Format("{0}(POS)，新增筆數:0", ToTable));
                }
                OutputMsg(string.Format("{0}(WEB)，回壓FLAG", FromTable));
                OracleTransaction wotx = wcon.BeginTransaction();
                //ExeSql(sqlUpdate, wcon);
                ExeSql(sqlUpdate, wotx);
                OutputMsg("****************************");
                potx.Commit();
                wotx.Commit();
            }
            catch (Exception ex)
            {
                OutputMsg(string.Format("{0}(POS)，寫入資料時，產生例外。", ToTable));
                OutputMsg(sb.ToString());
                throw ex;
            }
        }


    }
}
