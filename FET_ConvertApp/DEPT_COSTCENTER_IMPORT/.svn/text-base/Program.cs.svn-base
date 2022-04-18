using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data;
using System.Data.OracleClient;
using System.Threading;
using Advtek.Utility;

namespace DEPT_COSTCENTER_IMPORT
{
    class Program
    {
        static OracleConnection pcon = null;
        static OracleConnection wcon = null;
        static string sMSG = "";
        static void Main(string[] args)
        {
            //
            //初始化LOG
            OutputMsg("1.DEPT_COSTCENTER_IMPORT開始");
            OutputMsg("2.初始化LOG");
            ConvertLog con_log = new ConvertLog("DEPT_COSTCENTER_IMPORT");

            try
            {
                OutputMsg("3.建立新舊連線");
                pcon = OracleDBUtil.GetERPPOSConnection();
                wcon = OracleDBUtil.GetConnection();

                // DEPT_TEMP
                OutputMsg("4.DEPT(POS) 寫入 DEPT_POS_TEMP(WEB)");
                insertDEPT_POS_TEMP();


                OracleTransaction wotx = wcon.BeginTransaction();
                OutputMsg("5.由SP_DEPT_IMPORT做新增修改");
                OracleParameter op = new OracleParameter("outMessage", OracleType.VarChar);
                op.Size = 2000;
                op.Direction = ParameterDirection.Output;
                OracleDBUtil.ExecuteSql_SP(
                    wotx
                    , "PK_CONVERT.SP_DEPT_IMPORT"
                    , op
                    );

                wotx.Commit();
                OutputMsg(op.Value.ToString());

                OutputMsg("6.執行結束，寫入LOG");
                con_log.Success(sMSG);
            }
            catch (Exception ex)
            {
                OutputMsg(ex.Message);
                con_log.Fail(sMSG);
            }
            finally
            {
                if (pcon != null)
                {
                    if (pcon.State == ConnectionState.Open) pcon.Close();
                    pcon.Dispose();
                }
                if (wcon != null)
                {
                    if (wcon.State == ConnectionState.Open) wcon.Close();
                    wcon.Dispose();
                }
                OracleConnection.ClearAllPools();
            }
        }

        static void insertDEPT_POS_TEMP() {
            string sql = string.Format("select * from DEPT ");
            insertTEMP_TABLE("DEPT","DEPT_POS_TEMP"
                    , @"Insert into DEPT_POS_TEMP ( 
                         DEPTNO, COMPANYCODE, DEPTNAME, 
                         COSTCENTER, UPDDATE, UPDUSRNO, 
                         ULSNO, ISACTIVE )
                        Values
                        ([DEPTNO], [COMPANYCODE], [DEPTNAME], 
                         [COSTCENTER], [UPDDATE], [UPDUSRNO], 
                         [ULSNO], [ISACTIVE]); ", sql);

        }

        static void insertTEMP_TABLE(string FromTable, string ToTable,string SqlTemplate, string sql)
        {
            OracleTransaction wotx = null;
            StringBuilder sb = new StringBuilder();
            try
            {
                OutputMsg("****************************");
                OutputMsg(string.Format("{0}(POS)，查詢資料", FromTable));
                DataTable dtPOS = SelectTable(sql, pcon);

                #region 清除TEMP檔
                wotx = wcon.BeginTransaction();
                OutputMsg(string.Format("{0}(WEB)，TEMP清除資料", ToTable));
                sb.Length = 0;
                sb.Append(string.Format("delete from {0}", ToTable));
                ExeSql(sb.ToString(), wotx);
                wotx.Commit();
                #endregion

                int iTEN = 0;
                int iCount = 0;
                if (dtPOS.Rows.Count > 0)
                {
                    OutputMsg(string.Format("{0}(WEB)，TEMP寫入資料", ToTable));
                    wotx = wcon.BeginTransaction();
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
                                    sb2.Replace("[" + dcname + "]", OracleDBUtil.SqlStr(dr[dcname].ToString()));
                                    #region 註解
                                    //if (!dcname.ToUpper().Equals("STATUS"))
                                    //{
                                    //    sb2.Replace("[" + dcname + "]", OracleDBUtil.SqlStr(dr[dcname].ToString()));
                                    //}
                                    //else
                                    //{
                                    //    sb2.Replace("[" + dcname + "]", "'T'");
                                    //}
                                    #endregion
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
                    wotx.Commit();
                    OutputMsg(string.Format("{0}(WEB)，新增筆數:{1}", ToTable, iCount.ToString()));
                }
                else
                {
                    //NO DATA
                    OutputMsg(string.Format("{0}(WEB)，更新筆數:0", ToTable));
                }
                OutputMsg("****************************");
            }
            catch (Exception ex)
            {
                OutputMsg(string.Format("{0}(WEB)，寫入TEMP資料時，產生例外。", ToTable));
                OutputMsg(sb.ToString());
                throw ex;
            }
            finally
            {
                if (wotx != null) wotx.Dispose();
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


        static void OutputMsg(string s)
        {
            Console.WriteLine(s);
            sMSG += s + "\r\n";
        }   
    }
}
