using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.OracleClient;
using Advtek.Utility;
using System.Threading;

namespace PROCESSORDER_IMPORT
{
    class Program
    {
        static string sMSG = "";
        static void Main(string[] args)
        {
            //初始化LOG
            OutputMsg("1.PROCESSORDER_IMPORT開始");
            OutputMsg("2.初始化LOG");
            ConvertLog con_log = new ConvertLog("PROCESSORDER_IMPORT");
            try
            {
                //抓舊pos資料
                FETDB01T_PROCESSORDER_WEBPOS_PROCESSORDER();

                //**SP_PROCESSORDER_IMPORT
                PROCESSORDER_ORDER_SHIPCONFIRM_HEAD_DETAIL();

                //成功資訊
                OutputMsg("7.執行結束，寫入LOG");
                con_log.Success(sMSG);
                Thread.Sleep(5000);
            }
            catch (Exception ex)
            {
                //失敗資訊
                con_log.Fail(sMSG+ex.Message);
                Thread.Sleep(5000);
                //throw ex;
            }
        }

        static void FETDB01T_PROCESSORDER_WEBPOS_PROCESSORDER() 
        {
            OutputMsg("3.查詢PROCESSORDER(POS)");
            DataTable dt = Query_PROCESSORDER_OLD();
            StringBuilder sb = new StringBuilder();
            OracleConnection objConn = null;
            OracleTransaction objTX = null;

            OracleConnection objConn_erp = null;
            OracleTransaction objTX_erp = null;

            try 
            {
                objConn = OracleDBUtil.GetConnection();
                objTX = objTX = objConn.BeginTransaction();
                OutputMsg("4.寫入PROCESSORDER(WEB,TEMP檔)");
                foreach (DataRow dr in dt.Rows)
                {
                    sb.Length = 0;
                    sb.AppendLine(
                        @"Insert into PROCESSORDER
                      (STORENO, SC_ORDNO, PRODNO, 
                       COMPANYCODE, COLLECTDATE, ORDDATE, 
                       ORDQTY, BRANCHNO, TOSO, 
                       ADVISEQTY, APPROVEQTY, DIFFQTY, 
                       FLG_APPROVE, FLG_TRANS, OENO, 
                       VERIFYDATE, VERIFYUSRNO, TRANSDATE, 
                       TRANSUSRNO, UPDUSRNO, UPDDATE, 
                       COLLECTUSRNO, INWAYQTY, HQ_ORDNO, 
                       ORD_TYPE, FLG_DOWNLOAD, ULSNO, 
                       REMARK, COLLECTDATETIME, VERIFYDATETIME, 
                       TRANSDATETIME )
                      Values
                      ([STORENO], [SC_ORDNO], [PRODNO], 
                       [COMPANYCODE], [COLLECTDATE], [ORDDATE], 
                       [ORDQTY], [BRANCHNO], [TOSO], 
                       [ADVISEQTY], [APPROVEQTY], [DIFFQTY], 
                       [FLG_APPROVE], [FLG_TRANS], [OENO], 
                       [VERIFYDATE], [VERIFYUSRNO], [TRANSDATE], 
                       [TRANSUSRNO], [UPDUSRNO], [UPDDATE], 
                       [COLLECTUSRNO], [INWAYQTY], [HQ_ORDNO], 
                       [ORD_TYPE], [FLG_DOWNLOAD], [ULSNO], 
                       [REMARK], [COLLECTDATETIME], [VERIFYDATETIME], 
                       [TRANSDATETIME] )");

                    sb.Replace("[STORENO]", OracleDBUtil.SqlStr(dr["STORENO"].ToString().Replace("R","")));
                    sb.Replace("[SC_ORDNO]", OracleDBUtil.SqlStr(dr["SC_ORDNO"].ToString()));
                    sb.Replace("[PRODNO]", OracleDBUtil.SqlStr(dr["PRODNO"].ToString()));
                    sb.Replace("[COMPANYCODE]", OracleDBUtil.SqlStr(dr["COMPANYCODE"].ToString()));
                    sb.Replace("[COLLECTDATE]", OracleDBUtil.SqlStr(dr["COLLECTDATE"].ToString()));
                    sb.Replace("[ORDDATE]", OracleDBUtil.SqlStr(dr["ORDDATE"].ToString()));
                    sb.Replace("[ORDQTY]", dr["ORDQTY"].ToString()); //number
                    sb.Replace("[BRANCHNO]", OracleDBUtil.SqlStr(dr["BRANCHNO"].ToString()));
                    sb.Replace("[TOSO]", OracleDBUtil.SqlStr(dr["TOSO"].ToString()));
                    sb.Replace("[ADVISEQTY]", dr["ADVISEQTY"].ToString()); //number
                    sb.Replace("[APPROVEQTY]", dr["APPROVEQTY"].ToString());//number
                    sb.Replace("[DIFFQTY]", dr["DIFFQTY"].ToString());//number
                    sb.Replace("[FLG_APPROVE]", OracleDBUtil.SqlStr(dr["FLG_APPROVE"].ToString()));
                    sb.Replace("[FLG_TRANS]", OracleDBUtil.SqlStr(dr["FLG_TRANS"].ToString()));
                    sb.Replace("[OENO]", OracleDBUtil.SqlStr(dr["OENO"].ToString()));
                    sb.Replace("[VERIFYDATE]", OracleDBUtil.SqlStr(dr["VERIFYDATE"].ToString()));
                    sb.Replace("[VERIFYUSRNO]", OracleDBUtil.SqlStr(dr["VERIFYUSRNO"].ToString()));
                    sb.Replace("[TRANSDATE]", OracleDBUtil.SqlStr(dr["TRANSDATE"].ToString()));
                    sb.Replace("[TRANSUSRNO]", OracleDBUtil.SqlStr(dr["TRANSUSRNO"].ToString()));
                    sb.Replace("[UPDUSRNO]", OracleDBUtil.SqlStr(dr["UPDUSRNO"].ToString()));
                    sb.Replace("[UPDDATE]", OracleDBUtil.SqlStr(dr["UPDDATE"].ToString()));
                    sb.Replace("[COLLECTUSRNO]", OracleDBUtil.SqlStr(dr["COLLECTUSRNO"].ToString()));
                    sb.Replace("[INWAYQTY]", dr["INWAYQTY"].ToString());//number
                    sb.Replace("[HQ_ORDNO]", OracleDBUtil.SqlStr(dr["HQ_ORDNO"].ToString()));
                    sb.Replace("[ORD_TYPE]", OracleDBUtil.SqlStr(dr["ORD_TYPE"].ToString()));
                    sb.Replace("[FLG_DOWNLOAD]", "'T'");//flag
                    sb.Replace("[ULSNO]", OracleDBUtil.SqlStr(dr["ULSNO"].ToString()));
                    sb.Replace("[REMARK]", OracleDBUtil.SqlStr(dr["REMARK"].ToString()));
                    sb.Replace("[COLLECTDATETIME]", OracleDBUtil.SqlStr(dr["COLLECTDATETIME"].ToString()));
                    sb.Replace("[VERIFYDATETIME]", OracleDBUtil.SqlStr(dr["VERIFYDATETIME"].ToString()));
                    sb.Replace("[TRANSDATETIME]", OracleDBUtil.SqlStr(dr["TRANSDATETIME"].ToString()));

                    OracleDBUtil.ExecuteSql(objTX, sb.ToString());
                }

                objTX.Commit();
            }
            catch (Exception ex) 
            {
                objTX.Rollback();
                OutputMsg("4.寫入PROCESSORDER(WEB,TEMP檔)例外產生");
                throw ex;
            }
            finally 
            {
                if (objTX != null) objTX.Dispose();
                if (objConn.State == ConnectionState.Open) objConn.Close();
                objConn.Dispose();
                OracleConnection.ClearAllPools();
            }

            //回寫狀態STATUS='T'
            try 
            {
                OutputMsg("5.回寫PROCESSORDER(POS)狀態為1");
                objConn_erp = OracleDBUtil.GetERPPOSConnection();
                objTX_erp = objConn_erp.BeginTransaction();
                sb.Length = 0;
                sb.Append("UPDATE PROCESSORDER ");
                sb.Append("   SET FLG_DOWNLOAD='1' ");
                sb.Append("WHERE FLG_DOWNLOAD='T' ");
                OracleDBUtil.ExecuteSql(objTX_erp, sb.ToString());
                objTX_erp.Commit();
            }
            catch (Exception ex) 
            {
                objTX_erp.Rollback();
                OutputMsg("5.回寫PROCESSORDER(POS)狀態為1例外產生");
                throw ex;
            }
            finally 
            {
                if (objTX_erp != null) objTX_erp.Dispose();
                if (objConn_erp.State == ConnectionState.Open) objConn_erp.Close();
                objConn_erp.Dispose();
                OracleConnection.ClearAllPools();
            }
        }

        static void PROCESSORDER_ORDER_SHIPCONFIRM_HEAD_DETAIL() 
        {
            //new APP04_Facade().SP_PROCESSORDER_IMPORT();        
            SP_PROCESSORDER_IMPORT();
        }

        static DataTable Query_PROCESSORDER_OLD()
        {
            OracleConnection oCon = null;
            OracleTransaction oTX = null;
            try
            {
                oCon = OracleDBUtil.GetERPPOSConnection();
                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                oTX = oCon.BeginTransaction();
                sb.Append("UPDATE PROCESSORDER ");
                sb.Append("   SET FLG_DOWNLOAD='T' ");
                sb.Append(" WHERE (NVL(FLG_DOWNLOAD,'0')='0'  OR FLG_DOWNLOAD='N') AND COMPANYCODE='01'");
                OracleDBUtil.ExecuteSql(oTX, sb.ToString());
                sb.Length = 0;
                sb.Append(@"SELECT (CASE WHEN LENGTH(STORENO)>4 THEN SUBSTR(STORENO,2,4) ELSE STORENO END) AS STORENO, SC_ORDNO, PRODNO, 
                             COMPANYCODE, COLLECTDATE, ORDDATE, 
                             nvl(ORDQTY,0) as ORDQTY, BRANCHNO, TOSO, 
                             nvl(ADVISEQTY,0) as ADVISEQTY, nvl(APPROVEQTY,0) as APPROVEQTY, nvl(DIFFQTY,0) as DIFFQTY, 
                             FLG_APPROVE, FLG_TRANS, OENO, 
                             VERIFYDATE, VERIFYUSRNO, TRANSDATE, 
                             TRANSUSRNO, UPDUSRNO, UPDDATE, 
                             COLLECTUSRNO, nvl(INWAYQTY,0) as INWAYQTY, HQ_ORDNO, 
                             ORD_TYPE, FLG_DOWNLOAD, ULSNO, 
                             REMARK, COLLECTDATETIME, VERIFYDATETIME, 
                             TRANSDATETIME 
                            FROM PROCESSORDER 
                            WHERE FLG_DOWNLOAD='T' "
                          );

                DataTable dt = OracleDBUtil.GetDataSet(oTX, sb.ToString()).Tables[0];

                oTX.Commit();

                return dt;
            }
            catch (Exception ex)
            {
                oTX.Rollback();
                OutputMsg("3.查詢PROCESSORDER(POS)例外產生");
                throw ex;
            }
            finally
            {
                oTX.Dispose();
                if (oCon.State == ConnectionState.Open) oCon.Close();
                oCon.Dispose();
                OracleConnection.ClearAllPools();
            }
        }

        static void SP_PROCESSORDER_IMPORT()
        {
            OracleConnection objConn = null;
            OracleTransaction objTX = null;
            try
            {
                OutputMsg("6.執行SP_PROCESSORDER_IMPORT");
                objConn = OracleDBUtil.GetConnection();
                objTX = objConn.BeginTransaction();

                //OracleDBUtil.ExecuteSql_SP(objTX, "SP_PROCESSORDER_IMPORT");
                OracleCommand cmd = new OracleCommand("SP_PROCESSORDER_IMPORT");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = objConn;
                cmd.Transaction = objTX;
                OracleParameter p1 = new OracleParameter("outMESSAGE", OracleType.VarChar, 3000);
                p1.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(p1);
                cmd.ExecuteNonQuery();
                objTX.Commit();
                OutputMsg(p1.Value.ToString());

            }
            catch (Exception ex)
            {
                objTX.Rollback();
                OutputMsg("6.執行SP_PROCESSORDER_IMPORT 例外產生");
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
        static void OutputMsg(string s) 
        {
            Console.WriteLine(s);
            sMSG += s + "\r\n";
        }
    }
}
