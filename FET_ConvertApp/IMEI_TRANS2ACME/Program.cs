using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Advtek.Utility;
using System.Data;
using System.Data.OracleClient;
using System.Data.SqlClient;
using System.Threading;

namespace IMEI_TRANS2ACME
{
    class Program
    {
        private static OracleConnection wcon = null;
        private static SqlConnection scon = null;
        private static string sMSG = "";
        private static string sLogSql = "";
        static void Main(string[] args)
        {
            //初始化LOG
            OutputMsg("1.IMEI_TRANS2ACME開始");
            OutputMsg("2.初始化LOG");
            ConvertLog con_log = new ConvertLog("IMEI_TRANS2ACME");
            try
            {
                OutputMsg("3.建立連線(ORACLE+SQL)");
                wcon = OracleDBUtil.GetConnection();
                //scon = new SqlConnection("data source=127.0.0.1;initial catalog=RENTAL;user id=sa; pwd=;Pooling=true;connection timeout=3600;");
                OutputMsg("3.1 查詢SYS_PARA(PARA_KEY='SQL_CON'");
                string constr = Query_SQL_ConStr();
                scon = new SqlConnection(constr);

                OutputMsg("4.PK_CONVERT.SP_IMEI_TRANS2ACME");
                string sRet = PK_CONVERT_SP_IMEI_TRANS2ACME();
                OutputMsg(sRet);

                OutputMsg("5.POS_IMEI_TEMP(WEB) => POS_IMEI(SQL SERVER)");
                insertSQL_POS_IMEI();

                OutputMsg("6.更新POS_IMEI_TEMP(WEB)，UP_FLAG='Y'");
                updateStatus();

                OutputMsg("7.執行結束，寫入LOG");
                con_log.Success(sMSG);

                Thread.Sleep(3000);
            }
            catch (Exception ex)
            {
                OutputMsg(ex.Message);
                OutputMsg("最後執行SQL:" + sLogSql);
                con_log.Fail(sMSG);
                Thread.Sleep(3000);
            }
            finally
            {
                if (wcon != null)
                {
                    if (wcon.State == ConnectionState.Open) wcon.Close();
                    wcon.Dispose();
                }
                if (scon != null)
                {
                    if (scon.State == ConnectionState.Open) scon.Close();
                    scon.Dispose();
                }
                SqlConnection.ClearAllPools();
                OracleConnection.ClearAllPools();
            }



        }

        static string PK_CONVERT_SP_IMEI_TRANS2ACME()
        {

            OracleTransaction objTX = null;
            string sRet = "";
            try
            {
                objTX = wcon.BeginTransaction();
                OracleCommand oraCmd = new OracleCommand("PK_CONVERT.SP_IMEI_TRANS2ACME");
                oraCmd.CommandType = System.Data.CommandType.StoredProcedure;
                oraCmd.Parameters.Add(new OracleParameter("outMESSAGE", OracleType.VarChar, 2000)).Direction = ParameterDirection.Output;
                oraCmd.Connection = wcon;
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

            return sRet;
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

        static void insertSQL_POS_IMEI()
        {
            OutputMsg("5.1 查詢POS_IMEI_TEMP(UP_FLAG=T)");
            string sql = "select * from POS_IMEI_TEMP where up_flag='T'";
            DataTable dt = SelectTable(sql, wcon);
            StringBuilder sb = new StringBuilder();
            StringBuilder sb2 = new StringBuilder();
            int insertCount = 0;
            OutputMsg("5.2 上傳POS_IMEI(SQL SERVER)");
            if (dt.Rows.Count > 0)
            {
                if (scon.State == ConnectionState.Closed) scon.Open();
                SqlTransaction stx = scon.BeginTransaction();
                int count = 0;
                foreach (DataRow dr in dt.Rows)
                {

                    if (count > 200)
                    {
                        ExeSql(sb.Replace("\r", " ").Replace("\n", " ").ToString(), stx);
                        count = 0;
                        sb.Length = 0;
                    }
                    sb2.Length = 0;
                    sb2.Append(@"INSERT into POS_IMEI (
                                  TRANSACTIONDATE, TERMNO, 
                                  TRANSACTIONTIME, SERIALNO, STORENO, 
                                  DETAILID, COMPANYCODE, PRODNO, 
                                  PROMOTENO, MSISDN, INSERTDATE, 
                                  MSISDNDETAILID, STATUS, CREATEDATE, 
                                  PROCESSFLAG, IMEI, WS_RETURN,SOURCE_REFERENCE)
                                 values (
                                  [TRANSACTIONDATE], [TERMNO], 
                                  [TRANSACTIONTIME], [SERIALNO], [STORENO], 
                                  [DETAILID], [COMPANYCODE], [PRODNO], 
                                  [PROMOTENO], [MSISDN], [INSERTDATE], 
                                  [MSISDNDETAILID], [STATUS], [CREATEDATE], 
                                  [PROCESSFLAG], [IMEI], [WS_RETURN],[SOURCE_REFERENCE])  ");
                    sb2.Replace("[TRANSACTIONDATE]", "convert(datetime,'" + dr["TRANSACTIONDATE"].ToString() + "')");
                    sb2.Replace("[TERMNO]", OracleDBUtil.SqlStr(dr["TERMNO"].ToString()));
                    sb2.Replace("[TRANSACTIONTIME]", OracleDBUtil.SqlStr(dr["TRANSACTIONTIME"].ToString()));
                    sb2.Replace("[SERIALNO]", OracleDBUtil.SqlStr(dr["SERIALNO"].ToString()));
                    sb2.Replace("[STORENO]", OracleDBUtil.SqlStr(dr["STORENO"].ToString()));
                    sb2.Replace("[DETAILID]", OracleDBUtil.SqlStr(dr["DETAILID"].ToString()));
                    sb2.Replace("[COMPANYCODE]", OracleDBUtil.SqlStr(dr["COMPANYCODE"].ToString()));
                    sb2.Replace("[PRODNO]", OracleDBUtil.SqlStr(dr["PRODNO"].ToString()));
                    sb2.Replace("[PROMOTENO]", OracleDBUtil.SqlStr(dr["PROMOTENO"].ToString()));
                    sb2.Replace("[MSISDN]", OracleDBUtil.SqlStr(dr["MSISDN"].ToString()));
                    sb2.Replace("[INSERTDATE]", "convert(datetime,'" + dr["INSERTDATE"].ToString() + "')");
                    sb2.Replace("[MSISDNDETAILID]", OracleDBUtil.SqlStr(dr["MSISDNDETAILID"].ToString()));
                    sb2.Replace("[STATUS]", OracleDBUtil.SqlStr(dr["STATUS"].ToString()));
                    sb2.Replace("[CREATEDATE]", "convert(datetime,'" + dr["CREATEDATE"].ToString() + "')");
                    sb2.Replace("[PROCESSFLAG]", dr["PROCESSFLAG"].ToString());
                    sb2.Replace("[IMEI]", OracleDBUtil.SqlStr(dr["IMEI"].ToString()));
                    sb2.Replace("[WS_RETURN]", OracleDBUtil.SqlStr(dr["WS_RETURN"].ToString()));
                    sb2.Replace("[SOURCE_REFERENCE]", OracleDBUtil.SqlStr(dr["SALE_IMEI_LOG_ID"].ToString()));

                    sb.Append(sb2.ToString());
                    count++;
                    insertCount++;
                }

                if (sb.Length > 0)
                {
                    ExeSql(sb.Replace("\r", " ").Replace("\n", " ").ToString(), stx);
                    count = 0;
                    sb.Length = 0;
                }
                stx.Commit();

            }
            OutputMsg("5.3 POS_IMEI上傳筆數:" + insertCount);
        }

        static void updateStatus()
        {
            try
            {
                OracleTransaction otx = wcon.BeginTransaction();
                string sql = "UPDATE POS_IMEI_TEMP SET UP_FLAG='Y' WHERE UP_FLAG='T'";
                ExeSql(sql, otx);
                otx.Commit();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #region 備份程式
        //static int ExeSql(string sql, SqlConnection con)
        //{
        //    SqlTransaction otx = null;
        //    int i = 0;
        //    try
        //    {
        //        otx = con.BeginTransaction();
        //        i = OracleDBUtil.ExecuteSql(otx, sql);
        //        otx.Commit();
        //    }
        //    catch (Exception ex)
        //    {
        //        otx.Rollback();
        //        throw ex;
        //    }
        //    finally
        //    {
        //        if (otx != null) otx.Dispose();
        //    }
        //    return i;
        //}
        #endregion

        static void ExeSql(string sql, SqlTransaction otx)
        {
            try
            {
                //i = OracleDBUtil.ExecuteSql(otx, sql);
                sLogSql = sql;
                using (SqlCommand cmd = new SqlCommand(sql, otx.Connection, otx))
                {
                    if (otx.Connection.State == ConnectionState.Closed) otx.Connection.Open();
                    cmd.ExecuteNonQuery();
                }

            }
            catch (Exception ex)
            {
                otx.Rollback();
                throw ex;
            }
        }

        static void ExeSql(string sql, OracleTransaction otx)
        {
            try
            {
                sLogSql = sql;
                using (OracleCommand cmd = new OracleCommand(sql, otx.Connection, otx))
                {
                    if (otx.Connection.State == ConnectionState.Closed) otx.Connection.Open();
                    cmd.ExecuteNonQuery();
                }

            }
            catch (Exception ex)
            {
                otx.Rollback();
                throw ex;
            }
        }

        static string Query_SQL_ConStr()
        {
            try
            {
                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                sb.Length = 0;
                sb.Append("SELECT PARA_VALUE  FROM SYS_PARA WHERE PARA_KEY='SQL_CON_APROJECT'  ");//***20110302 修改連線字串
                sLogSql = sb.ToString();
                DataTable dt = OracleDBUtil.GetDataSet(wcon, sb.ToString()).Tables[0];
                return dt.Rows[0][0].ToString();
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        static void OutputMsg(string s)
        {
            Console.WriteLine(s);
            sMSG += s + "\r\n";
        }


    }
}
