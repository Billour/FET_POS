using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.OracleClient;
using Advtek.Utility;

namespace DS_ORDERM_IMPORT
{
    class Program
    {
        static ConvertLog cLog;

        static void Main(string[] args)
        {
            Console.WriteLine("1.DS_ORDERM_IMPORT開始執行");
            Console.WriteLine("2.初始化LOG");
            cLog = new ConvertLog("DS_ORDERM_IMPORT");
            try {
                //**002FETDB01T.DS_ORDERD -> WEBPOS.DS_ORDERD
                Console.WriteLine("3.DS_ORDERD(ERP) -> DS_ORDERD(WEBPOS)");
                FETDB01T_DS_ORDERD_WEBPOS_DS_ORDERD();

                //**003比對 WEBPOS.PROCESSORDER WEBPOS.ORDER_SHIPCONFIRM_HEAD ORDER_SHIPCONFIRM_DETAIL
                Console.WriteLine("4.比對 PROCESSORDER(WEBPOS) ORDER_SHIPCONFIRM_HEAD(WEBPOS) ORDER_SHIPCONFIRM_DETAIL(WEBPOS)");
                DS_ORDERD_ORDER_SHIPCONFIRM_HEAD_DETAIL();
            }
            catch (Exception ex) 
            {
                cLog.Fail(ex.Message);
                Console.WriteLine(ex.Message);
                myPause(); 
            }           
        }
        static void FETDB01T_DS_ORDERD_WEBPOS_DS_ORDERD()
        {
            try {
                //GET OLD POS DATA
                DataTable dtOld = Query_DS_ORDERD_OLD();
                Add_DS_ORDERD(dtOld);
            }
            catch (Exception ex) 
            {
                throw ex;
            }            
        }

        static void DS_ORDERD_ORDER_SHIPCONFIRM_HEAD_DETAIL()
        {
            try
            {
                Console.WriteLine("  SP_DS_ORDERD_IMPORT");
                string sMsg=SP_DS_ORDERD_IMPORT();
                cLog.Success(sMsg);
                Console.WriteLine("排程執行結束，寫入LOG");
            }
            catch (Exception ex) 
            {
                throw ex;                
            }
        }

        public static DataTable Query_DS_ORDERD_OLD()
        {
            OracleConnection oCon = null;
            OracleTransaction oTX = null;
            try
            {
                oCon = OracleDBUtil.GetERPPOSConnection();
                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                oTX = oCon.BeginTransaction();
                sb.Append("UPDATE DS_ORDERD ");
                sb.Append("   SET FLG_SUBINVENTORY_TRANSFER='T' ");
                sb.Append(" WHERE (NVL(FLG_SUBINVENTORY_TRANSFER,'0')='0'  OR FLG_SUBINVENTORY_TRANSFER='N') ");
                //sb.Append("   AND ROWNUM=1 ");
                OracleDBUtil.ExecuteSql(oTX, sb.ToString());
                sb.Length = 0;
                sb.Append("SELECT * ");
                sb.Append("FROM DS_ORDERD ");
                sb.Append("WHERE FLG_SUBINVENTORY_TRANSFER='T' ");
                //DataTable dt = OracleDBUtil.GetDataSet(oCon, sb.ToString()).Tables[0];
                DataTable dt = OracleDBUtil.GetDataSet(oTX, sb.ToString()).Tables[0];
                oTX.Commit();
                return dt;
            }
            catch (Exception ex)
            {
                oTX.Rollback();
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

        public static void Add_DS_ORDERD(DataTable dtOld)
        {
            OracleConnection objConn = null;
            OracleTransaction objTX = null;
            OracleConnection oCon = null;
            try
            {
                //WEB POS
                objConn = OracleDBUtil.GetConnection();
                objTX = objConn.BeginTransaction();

                //OracleDBUtil.Insert(dsAdd.Tables["PROCESSORDER"]);
                //OracleDBUtil.Insert(objTX, dsAdd.Tables["DS_ORDERD"]);
                StringBuilder sb = new StringBuilder();
                foreach (DataRow dr in dtOld.Rows) {
                    sb.Length = 0;
                    sb.AppendLine(
                       @"Insert into DS_ORDERD
                       (STORENO, DS_ORDERNO, PRODNO, 
                        SEQNO, FORCASTQTY, ASSIGNQTY, 
                        SUGGESTQTY, STOREORDQTY, APPROVEQTY, 
                        REALORDQTY, SHIPCONFIRMQTY, INCOUNTQTY, 
                        INWAYQTY, VENDORNAME, PO_NO, 
                        PO_LINE, SUBINVENTORY, LOCATOR, 
                        CATEGORY_LEVEL1, CATEGORY_LEVEL2, CATEGORY_LEVEL3, 
                        CATEGORY_LEVEL4, CATEGORY_LEVEL5, CATEGORY_LEVEL6, 
                        SHIPDATE, DS_ORDERDATE, FLG_SUBINVENTORY_TRANSFER, 
                        SALE_AVG_QTY, TRUNOVER_DAY, STOCK_QTY, 
                        ALL_LEVEL, STORE_LEVEL0, STORE_LEVEL1, 
                        STORE_LEVEL2)
                        Values
                       ([STORENO], [DS_ORDERNO], [PRODNO], 
                        [SEQNO], [FORCASTQTY], [ASSIGNQTY], 
                        [SUGGESTQTY], [STOREORDQTY], [APPROVEQTY], 
                        [REALORDQTY], [SHIPCONFIRMQTY], [INCOUNTQTY], 
                        [INWAYQTY], [VENDORNAME], [PO_NO], 
                        [PO_LINE], [SUBINVENTORY], [LOCATOR], 
                        [CATEGORY_LEVEL1], [CATEGORY_LEVEL2], [CATEGORY_LEVEL3], 
                        [CATEGORY_LEVEL4], [CATEGORY_LEVEL5], [CATEGORY_LEVEL6], 
                        [SHIPDATE], [DS_ORDERDATE], [FLG_SUBINVENTORY_TRANSFER], 
                        [SALE_AVG_QTY], [TRUNOVER_DAY], [STOCK_QTY], 
                        [ALL_LEVEL], [STORE_LEVEL0], [STORE_LEVEL1], 
                        [STORE_LEVEL2])");

                    sb.Replace("[STORENO]", OracleDBUtil.SqlStr(dr["STORENO"].ToString().Replace("R","")));
                    sb.Replace("[DS_ORDERNO]", OracleDBUtil.SqlStr(dr["DS_ORDERNO"].ToString()));
                    sb.Replace("[PRODNO]", OracleDBUtil.SqlStr(dr["PRODNO"].ToString()));
                    sb.Replace("[SEQNO]", OracleDBUtil.SqlStr(dr["SEQNO"].ToString()));
                    sb.Replace("[FORCASTQTY]", OracleDBUtil.SqlStr(dr["FORCASTQTY"].ToString()));
                    sb.Replace("[ASSIGNQTY]", OracleDBUtil.SqlStr(dr["ASSIGNQTY"].ToString()));
                    sb.Replace("[SUGGESTQTY]", OracleDBUtil.SqlStr(dr["SUGGESTQTY"].ToString()));
                    sb.Replace("[STOREORDQTY]", OracleDBUtil.SqlStr(dr["STOREORDQTY"].ToString()));
                    sb.Replace("[APPROVEQTY]", OracleDBUtil.SqlStr(dr["APPROVEQTY"].ToString()));
                    sb.Replace("[REALORDQTY]", OracleDBUtil.SqlStr(dr["REALORDQTY"].ToString()));
                    sb.Replace("[SHIPCONFIRMQTY]", OracleDBUtil.SqlStr(dr["SHIPCONFIRMQTY"].ToString()));
                    sb.Replace("[INCOUNTQTY]", OracleDBUtil.SqlStr(dr["INCOUNTQTY"].ToString()));
                    sb.Replace("[INWAYQTY]", OracleDBUtil.SqlStr(dr["INWAYQTY"].ToString()));
                    sb.Replace("[VENDORNAME]", OracleDBUtil.SqlStr(dr["VENDORNAME"].ToString()));
                    sb.Replace("[PO_NO]", OracleDBUtil.SqlStr(dr["PO_NO"].ToString()));
                    sb.Replace("[PO_LINE]", OracleDBUtil.SqlStr(dr["PO_LINE"].ToString()));
                    sb.Replace("[SUBINVENTORY]", OracleDBUtil.SqlStr(dr["SUBINVENTORY"].ToString()));
                    sb.Replace("[LOCATOR]", OracleDBUtil.SqlStr(dr["LOCATOR"].ToString()));
                    sb.Replace("[CATEGORY_LEVEL1]", OracleDBUtil.SqlStr(dr["CATEGORY_LEVEL1"].ToString()));
                    sb.Replace("[CATEGORY_LEVEL2]", OracleDBUtil.SqlStr(dr["CATEGORY_LEVEL2"].ToString()));
                    sb.Replace("[CATEGORY_LEVEL3]", OracleDBUtil.SqlStr(dr["CATEGORY_LEVEL3"].ToString()));
                    sb.Replace("[CATEGORY_LEVEL4]", OracleDBUtil.SqlStr(dr["CATEGORY_LEVEL4"].ToString()));
                    sb.Replace("[CATEGORY_LEVEL5]", OracleDBUtil.SqlStr(dr["CATEGORY_LEVEL5"].ToString()));
                    sb.Replace("[CATEGORY_LEVEL6]", OracleDBUtil.SqlStr(dr["CATEGORY_LEVEL6"].ToString()));
                    sb.Replace("[SHIPDATE]", OracleDBUtil.SqlStr(dr["SHIPDATE"].ToString()));
                    sb.Replace("[DS_ORDERDATE]", OracleDBUtil.SqlStr(dr["DS_ORDERDATE"].ToString()));
                    sb.Replace("[FLG_SUBINVENTORY_TRANSFER]", "'T'");
                    sb.Replace("[SALE_AVG_QTY]", OracleDBUtil.SqlStr(dr["SALE_AVG_QTY"].ToString()));
                    sb.Replace("[TRUNOVER_DAY]", OracleDBUtil.SqlStr(dr["TRUNOVER_DAY"].ToString()));
                    sb.Replace("[STOCK_QTY]", OracleDBUtil.SqlStr(dr["STOCK_QTY"].ToString()));
                    sb.Replace("[ALL_LEVEL]", OracleDBUtil.SqlStr(dr["ALL_LEVEL"].ToString()));
                    sb.Replace("[STORE_LEVEL0]", OracleDBUtil.SqlStr(dr["STORE_LEVEL0"].ToString()));
                    sb.Replace("[STORE_LEVEL1]", OracleDBUtil.SqlStr(dr["STORE_LEVEL1"].ToString()));
                    sb.Replace("[STORE_LEVEL2]", OracleDBUtil.SqlStr(dr["STORE_LEVEL2"].ToString()));
                    OracleDBUtil.ExecuteSql(objTX, sb.ToString());                
                }
                objTX.Commit();

                //OLD POS
                oCon = OracleDBUtil.GetERPPOSConnection();
                objTX = oCon.BeginTransaction();

                ////回寫狀態STATUS='T'
                sb.Length = 0;
                sb.Append("UPDATE DS_ORDERD ");
                sb.Append("   SET FLG_SUBINVENTORY_TRANSFER='1' ");
                sb.Append("WHERE FLG_SUBINVENTORY_TRANSFER='T' ");
                OracleDBUtil.ExecuteSql(objTX, sb.ToString());
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
                if (oCon.State == ConnectionState.Open) oCon.Close();
                oCon.Dispose();
                OracleConnection.ClearAllPools();
            }
        }

        public static string SP_DS_ORDERD_IMPORT()
        {
            OracleConnection objConn = null;
            OracleTransaction objTX = null;
            string sRet = "";
            try
            {
                objConn = OracleDBUtil.GetConnection();
                objTX = objConn.BeginTransaction();

                OracleCommand oraCmd = new OracleCommand("SP_DS_ORDERD_IMPORT");
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
                objTX.Dispose();
                if (objConn.State == ConnectionState.Open) objConn.Close();
                objConn.Dispose();
                OracleConnection.ClearAllPools();
            }

            return sRet;
        }

        static void myPause()
        {
            Console.WriteLine("請按任意鍵繼續...");
            Console.ReadKey(true);
        }
    }
}
