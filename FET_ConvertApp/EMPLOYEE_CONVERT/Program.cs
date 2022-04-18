using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.OracleClient;
using System.Data.SqlClient;
using Advtek.Utility;
using System.Threading;

namespace EMPLOYEE_CONVERT
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("EMPLOYEE_CONVERT");
            Console.WriteLine("初始化LOG");
            ConvertLog cLog = new ConvertLog("EMPLOYEE_CONVERT");
            try
            {
                //EFP SOURCE TABLE > WEB TEMP TABLE
                Console.WriteLine("查詢VIEMEMBER(SQL SERVER)");
                DataTable dtAdd = Query_OLD_VIEMEMBER();
                dtAdd.TableName = "VIEMEMBER";
                Console.WriteLine("VIEMEMBER(SQL SERVER) Clone toIEM VEMBER(Oracle)");
                Insert_VIEMEMBER(dtAdd);

                //***20110223 VIEDEPT(SQL) > VIEDEPT(ORACLE)
                Console.WriteLine("查詢VIEDEPT(SQL SERVER)");
                DataTable dtDept = Query_OLD_VIEDEPT();
                dtAdd.TableName = "VIEDEPT";
                Console.WriteLine("VIEDEPT(SQL SERVER) Clone to VIEDEPT(Oracle)");
                Insert_VIEDEPT(dtDept);

                //WEB TEMP TABLE > WEB TARGET TABLE
                Console.WriteLine("PK_CONVERT.SP_EMPLYEE_CONVERT");
                string sMsg = PK_CONVERT_SP_EMPLYEE_CONVERT();
                Console.WriteLine("執行結束，寫入LOG");
                cLog.Success(sMsg);
                Thread.Sleep(3000);
            }
            catch (Exception ex) {
                Console.WriteLine("例外產生");
                cLog.Fail(ex.Message);
                Console.WriteLine(ex.Message);
                Thread.Sleep(3000);
            }
        }

        public static string PK_CONVERT_SP_EMPLYEE_CONVERT()
        {
            OracleConnection objConn = null;
            OracleTransaction objTX = null;
            string sRet = "";
            try
            {
                objConn = OracleDBUtil.GetConnection();
                objTX = objConn.BeginTransaction();

                //OracleDBUtil.ExecuteSql_SP(objTX, "PK_CONVERT.SP_EMPLYEE_CONVERT");
                OracleCommand oraCmd = new OracleCommand("PK_CONVERT.SP_EMPLYEE_CONVERT");
                oraCmd.CommandType = System.Data.CommandType.StoredProcedure;
                oraCmd.Parameters.Add(new OracleParameter("outMESSAGE", OracleType.VarChar, 2000)).Direction = ParameterDirection.Output;
                oraCmd.Connection = objConn;
                oraCmd.Transaction = objTX;
                oraCmd.ExecuteNonQuery();
                sRet = oraCmd.Parameters["outMESSAGE"].Value.ToString();
                objTX.Commit();
            }
            catch (Exception ex)
            {
                objTX.Rollback();
                throw ex;
            }
            finally
            {
                if (objTX != null) objTX.Dispose();
                if (objConn != null) {
                    if (objConn.State == ConnectionState.Open) objConn.Close();
                    objConn.Dispose();
                }
                
            }
            return sRet;
        }

        public static void Insert_VIEMEMBER(DataTable dtAdd)
        {
            OracleConnection objConn = null;
            OracleTransaction objTX = null;

            try
            {
                objConn = OracleDBUtil.GetConnection();
                objTX = objConn.BeginTransaction();

                OracleDBUtil.ExecuteSql(objTX, "DELETE FROM VIEMEMBER");

                //SOM
                //if (dtUpd.Rows.Count > 0)
                //    OracleDBUtil.UPDDATEByUUID(dtUpd, "SEGMENT1");
                if (dtAdd.Rows.Count > 0) 
                {
                    StringBuilder sb = new StringBuilder();

                    foreach (DataRow dr in dtAdd.Rows)
                    {
                        sb.Length = 0;
                        sb.AppendLine(
                            @"Insert into VIEMEMBER
                           (EMPNO, DEPTCODE, EMPNAME, 
                            EMPFNAME, EMPLNAME, ENGNAME, 
                            ENGFNAME, ENGLNAME, ALIASNAME, 
                            CELLULAR, EXTENSION, FAXNO, 
                            MVPN, EMAIL, TITLE, 
                            TITLENAME, TITLECLASS, COMPCODE, 
                            COMPNAME, BUSNSCODE, BUSNSNAME, 
                            REGION, REGIONNAME, COSTCENTER, 
                            ENTDATE, OFFDATE, CONTRACTSDATE, 
                            CONTRACTEDATE, SEX, BIRTHDATE, 
                            LOCATIONCODE, LOCATIONNAME, FINALDATE, 
                            STARTDATE, EMPTYPE, OPID, 
                            LOGINID, REPFLG, HIRECODE, 
                            UPDDATE)
                         Values
                           ([EMPNO], [DEPTCODE], [EMPNAME], 
                            [EMPFNAME], [EMPLNAME], [ENGNAME], 
                            [ENGFNAME], [ENGLNAME], [ALIASNAME], 
                            [CELLULAR], [EXTENSION], [FAXNO], 
                            [MVPN], [EMAIL], [TITLE], 
                            [TITLENAME], [TITLECLASS], [COMPCODE], 
                            [COMPNAME], [BUSNSCODE], [BUSNSNAME], 
                            [REGION], [REGIONNAME], [COSTCENTER], 
                            [ENTDATE], [OFFDATE], [CONTRACTSDATE], 
                            [CONTRACTEDATE], [SEX], [BIRTHDATE], 
                            [LOCATIONCODE], [LOCATIONNAME], [FINALDATE], 
                            [STARTDATE], [EMPTYPE], [OPID], 
                            [LOGINID], [REPFLG], [HIRECODE], 
                            [UPDDATE])"
                        );

                        sb.Replace("[EMPNO]", OracleDBUtil.SqlStr(dr["EMPNO"].ToString()));
                        sb.Replace("[DEPTCODE]", OracleDBUtil.SqlStr(dr["DEPTCODE"].ToString()));
                        sb.Replace("[EMPNAME]", OracleDBUtil.SqlStr(dr["EMPNAME"].ToString()));
                        sb.Replace("[EMPFNAME]", OracleDBUtil.SqlStr(dr["EMPFNAME"].ToString()));
                        sb.Replace("[EMPLNAME]", OracleDBUtil.SqlStr(dr["EMPLNAME"].ToString()));
                        sb.Replace("[ENGNAME]", OracleDBUtil.SqlStr(dr["ENGNAME"].ToString()));
                        sb.Replace("[ENGFNAME]", OracleDBUtil.SqlStr(dr["ENGFNAME"].ToString()));
                        sb.Replace("[ENGLNAME]", OracleDBUtil.SqlStr(dr["ENGLNAME"].ToString()));
                        sb.Replace("[ALIASNAME]", OracleDBUtil.SqlStr(dr["ALIASNAME"].ToString()));
                        sb.Replace("[CELLULAR]", OracleDBUtil.SqlStr(dr["CELLULAR"].ToString()));
                        sb.Replace("[EXTENSION]", OracleDBUtil.SqlStr(dr["EXTENSION"].ToString()));
                        sb.Replace("[FAXNO]", OracleDBUtil.SqlStr(dr["FAXNO"].ToString()));
                        sb.Replace("[MVPN]", OracleDBUtil.SqlStr(dr["MVPN"].ToString()));
                        sb.Replace("[EMAIL]", OracleDBUtil.SqlStr(dr["EMAIL"].ToString()));
                        sb.Replace("[TITLE]", OracleDBUtil.SqlStr(dr["TITLE"].ToString()));
                        sb.Replace("[TITLENAME]", OracleDBUtil.SqlStr(dr["TITLENAME"].ToString()));
                        sb.Replace("[TITLECLASS]", OracleDBUtil.SqlStr(dr["TITLECLASS"].ToString()));
                        sb.Replace("[COMPCODE]", OracleDBUtil.SqlStr(dr["COMPCODE"].ToString()));
                        sb.Replace("[COMPNAME]", OracleDBUtil.SqlStr(dr["COMPNAME"].ToString()));
                        sb.Replace("[BUSNSCODE]", OracleDBUtil.SqlStr(dr["BUSNSCODE"].ToString()));
                        sb.Replace("[BUSNSNAME]", OracleDBUtil.SqlStr(dr["BUSNSNAME"].ToString()));
                        sb.Replace("[REGION]", OracleDBUtil.SqlStr(dr["REGION"].ToString()));
                        sb.Replace("[REGIONNAME]", OracleDBUtil.SqlStr(dr["REGIONNAME"].ToString()));
                        sb.Replace("[COSTCENTER]", OracleDBUtil.SqlStr(dr["COSTCENTER"].ToString()));
                        sb.Replace("[ENTDATE]", OracleDBUtil.SqlStr(dr["ENTDATE"].ToString()));
                        sb.Replace("[OFFDATE]", OracleDBUtil.SqlStr(dr["OFFDATE"].ToString()));
                        sb.Replace("[CONTRACTSDATE]", OracleDBUtil.SqlStr(dr["CONTRACTSDATE"].ToString()));
                        sb.Replace("[CONTRACTEDATE]", OracleDBUtil.SqlStr(dr["CONTRACTEDATE"].ToString()));
                        sb.Replace("[SEX]", OracleDBUtil.SqlStr(dr["SEX"].ToString()));
                        sb.Replace("[BIRTHDATE]", OracleDBUtil.SqlStr(dr["BIRTHDATE"].ToString()));
                        sb.Replace("[LOCATIONCODE]", OracleDBUtil.SqlStr(dr["LOCATIONCODE"].ToString()));
                        sb.Replace("[LOCATIONNAME]", OracleDBUtil.SqlStr(dr["LOCATIONNAME"].ToString()));
                        sb.Replace("[FINALDATE]", OracleDBUtil.SqlStr(dr["FINALDATE"].ToString()));
                        sb.Replace("[STARTDATE]", OracleDBUtil.SqlStr(dr["STARTDATE"].ToString()));
                        sb.Replace("[EMPTYPE]", OracleDBUtil.SqlStr(dr["EMPTYPE"].ToString()));
                        sb.Replace("[OPID]", OracleDBUtil.SqlStr(dr["OPID"].ToString()));
                        sb.Replace("[LOGINID]", OracleDBUtil.SqlStr(dr["LOGINID"].ToString()));
                        sb.Replace("[REPFLG]", OracleDBUtil.SqlStr(dr["REPFLG"].ToString()));
                        sb.Replace("[HIRECODE]", OracleDBUtil.SqlStr(dr["HIRECODE"].ToString()));
                        sb.Replace("[UPDDATE]", OracleDBUtil.SqlStr(dr["UPDDATE"].ToString()));
                        OracleDBUtil.ExecuteSql(objTX, sb.ToString());
                    }
                    
                }
                    //OracleDBUtil.Insert(dtAdd);

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
        }

        //***20110223 COSTCENTER AND DEPT
        public static void Insert_VIEDEPT(DataTable dtAdd)
        {
            OracleConnection objConn = null;
            OracleTransaction objTX = null;

            try
            {
                objConn = OracleDBUtil.GetConnection();
                objTX = objConn.BeginTransaction();

                OracleDBUtil.ExecuteSql(objTX, "DELETE FROM VIEDEPT");

                //SOM
                //if (dtUpd.Rows.Count > 0)
                //    OracleDBUtil.UPDDATEByUUID(dtUpd, "SEGMENT1");
                if (dtAdd.Rows.Count > 0)
                {
                    StringBuilder sb = new StringBuilder();

                    foreach (DataRow dr in dtAdd.Rows)
                    {
                        sb.Length = 0;
                        sb.AppendLine(
                            @"Insert into VIEDEPT
                           (DEPTCODE, DEPTENGNAME, DEPTCHINAME, 
                            PARENT, SDEPTNAME, COSTCENTERFLG, 
                            COSTCENTER, COMPCODE, COMPNAME, 
                            SETDATE, OFFDATE, EMPNO, 
                            DEPTLEVEL, DEPTLEVELNAME, DEPTTYPE, 
                            DEPTTYPENAME, DEPTMARK2, UPDDATE)
                         Values
                           ([DEPTCODE], [DEPTENGNAME], [DEPTCHINAME], 
                            [PARENT], [SDEPTNAME], [COSTCENTERFLG], 
                            [COSTCENTER], [COMPCODE], [COMPNAME], 
                            [SETDATE], [OFFDATE], [EMPNO], 
                            [DEPTLEVEL], [DEPTLEVELNAME], [DEPTTYPE], 
                            [DEPTTYPENAME], [DEPTMARK2], [UPDDATE])"
                        );

                        sb.Replace("[DEPTCODE]", OracleDBUtil.SqlStr(dr["DEPTCODE"].ToString()));
                        sb.Replace("[DEPTENGNAME]", OracleDBUtil.SqlStr(dr["DEPTENGNAME"].ToString()));
                        sb.Replace("[DEPTCHINAME]", OracleDBUtil.SqlStr(dr["DEPTCHINAME"].ToString()));
                        sb.Replace("[PARENT]", OracleDBUtil.SqlStr(dr["PARENT"].ToString()));
                        sb.Replace("[SDEPTNAME]", OracleDBUtil.SqlStr(dr["SDEPTNAME"].ToString()));
                        sb.Replace("[COSTCENTERFLG]", OracleDBUtil.SqlStr(dr["COSTCENTERFLG"].ToString()));
                        sb.Replace("[COSTCENTER]", OracleDBUtil.SqlStr(dr["COSTCENTER"].ToString()));
                        sb.Replace("[COMPCODE]", OracleDBUtil.SqlStr(dr["COMPCODE"].ToString()));
                        sb.Replace("[COMPNAME]", OracleDBUtil.SqlStr(dr["COMPNAME"].ToString()));
                        sb.Replace("[SETDATE]", OracleDBUtil.SqlStr(dr["SETDATE"].ToString()));
                        sb.Replace("[OFFDATE]", OracleDBUtil.SqlStr(dr["OFFDATE"].ToString()));
                        sb.Replace("[EMPNO]", OracleDBUtil.SqlStr(dr["EMPNO"].ToString()));
                        sb.Replace("[DEPTLEVEL]", OracleDBUtil.SqlStr(dr["DEPTLEVEL"].ToString()));
                        sb.Replace("[DEPTLEVELNAME]", OracleDBUtil.SqlStr(dr["DEPTLEVELNAME"].ToString()));
                        sb.Replace("[DEPTTYPE]", OracleDBUtil.SqlStr(dr["DEPTTYPE"].ToString()));
                        sb.Replace("[DEPTTYPENAME]", OracleDBUtil.SqlStr(dr["DEPTTYPENAME"].ToString()));
                        sb.Replace("[DEPTMARK2]", OracleDBUtil.SqlStr(dr["DEPTMARK2"].ToString()));
                        sb.Replace("[UPDDATE]", OracleDBUtil.SqlStr(dr["UPDDATE"].ToString()));
                        OracleDBUtil.ExecuteSql(objTX, sb.ToString());
                    }

                }
                //OracleDBUtil.Insert(dtAdd);

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
        }

        public static DataTable Query_OLD_VIEMEMBER()
        {
            DataTable dt = new DataTable();
            try
            {
                string sCon = Query_SQL_ConStr();
                using (SqlConnection con = new SqlConnection(sCon))
                {
                    try
                    {
                        string sql = @"select * from viemember 
                                       where (OffDate IS NULL) 
                                          OR (OffDate = '') 
                                          OR (OffDate >= CONVERT(char, GETDATE(), 111)) ";
                        using (SqlCommand cmd = new SqlCommand(sql, con))
                        {
                            using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                            {
                                da.Fill(dt);
                            }
                        }
                    }
                    catch (Exception ex) { throw ex; }
                    finally
                    {
                        if (con.State == ConnectionState.Open) con.Close();
                        con.Dispose();
                        OracleConnection.ClearAllPools();
                    }
                }
            }
            catch (Exception ex) { throw ex; }
            return dt;
        }

        //***20110223 COSTCENTER
        public static DataTable Query_OLD_VIEDEPT()
        {
            DataTable dt = new DataTable();
            try
            {
                string sCon = Query_SQL_ConStr();
                using (SqlConnection con = new SqlConnection(sCon))
                {
                    try
                    {
                        string sql = @"select * from VIEDEPT";
                        using (SqlCommand cmd = new SqlCommand(sql, con))
                        {
                            using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                            {
                                da.Fill(dt);
                            }
                        }
                    }
                    catch (Exception ex) { throw ex; }
                    finally
                    {
                        if (con.State == ConnectionState.Open) con.Close();
                        con.Dispose();
                        OracleConnection.ClearAllPools();
                    }
                }
            }
            catch (Exception ex) { throw ex; }
            return dt;
        }

        public static string Query_SQL_ConStr()
        {
            OracleConnection oCon = null;
            try
            {
                oCon = OracleDBUtil.GetConnection();
                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                sb.Length = 0;
                sb.Append("SELECT PARA_VALUE  ");
                sb.Append("FROM SYS_PARA WHERE PARA_KEY='SQL_CON'  ");

                DataTable dt = OracleDBUtil.GetDataSet(oCon, sb.ToString()).Tables[0];
                return dt.Rows[0][0].ToString();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (oCon.State == ConnectionState.Open) oCon.Close();
                oCon.Dispose();
                OracleConnection.ClearAllPools();
            }
        }
    }
}
