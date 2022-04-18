using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.OracleClient;
using Advtek.Utility;
using System.Threading;

namespace INVOICE_HEAD_HANDOVER
{
    class Program
    {
        static string sMSG = "";
        static void Main(string[] args)
        {
            //初始化LOG
            OutputMsg("1.INVOICE_HEAD_HANDOVER(發票作廢上傳ERP)開始");
            OutputMsg("2.初始化LOG");
            ConvertLog con_log = new ConvertLog("INVOICE_HEAD_HANDOVER");

            try
            {
                //取出上傳的銷售交易
                OutputMsg("3.INVOICE_HEAD(WEB),取出上傳的銷售交易，更新狀態為T");
                DataTable dtWebSale = Query_WEB_INVOICE_HEAD();

                OutputMsg("4.ERP_GUI(POS)寫入");
                Insert_ERP_GUI(dtWebSale);

                OutputMsg("5.INVOICE_HEAD(WEB)更新狀態為Y");
                Update_INVOICE_HEAD();

                OutputMsg("6.執行結束，寫入LOG");
                con_log.Success(sMSG);
                Thread.Sleep(5000);
            }
            catch (Exception ex)
            {
                OutputMsg(ex.Message);
                con_log.Fail( sMSG);
                Thread.Sleep(5000);
            }
        }

        #region 程式備份 20110209
        //static void Main(string[] args)
        //{
        //    //初始化LOG
        //    ConvertLog con_log = new ConvertLog("INVOICE_HEAD_HANDOVER");

        //    INVOICE_HEAD_HANDOVER_Facade cFacade = new INVOICE_HEAD_HANDOVER_Facade();
        //    try
        //    {
        //        //取出上傳的銷售交易
        //        DataTable dtWebSale = cFacade.Query_WEB_INVOICE_HEAD();

        //        Dictionary<string, string> dic = new Dictionary<string, string>();
        //        //< WEB_COLUMN_NAME,ERP_COLUMN_NAME >
        //        dic.Add("COMPANYCODE", "COMPANYCODE");
        //        //dic.Add("SALESCD", "SALESCD");//
        //        dic.Add("GUIDATE", "GUIDATE");
        //        dic.Add("GUINO", "GUINO");
        //        dic.Add("CANFLAG", "CANFLAG");//***
        //        dic.Add("STORENO", "STORENO");
        //        dic.Add("CORPNO", "CORPNO");
        //        dic.Add("TAXSALE", "TAXSALE");
        //        dic.Add("TAX", "TAX");
        //        dic.Add("DWNDATE", "DWNDATE");


        //        //Insert to 舊POS 的ERP_SALES+POS.SALE_HEAD 回壓上傳成功註記
        //        INVOICE_HEAD_HANDOVER_DTO dsERP = new INVOICE_HEAD_HANDOVER_DTO();
        //        INVOICE_HEAD_HANDOVER_DTO.ERP_GUIDataTable dtERP = dsERP.ERP_GUI;
        //        foreach (DataColumn dc in dtERP.Columns)
        //            dc.AllowDBNull = true;

        //        for (int i = 0; i < dtWebSale.Rows.Count; i++)
        //        {
        //            DataRow drWebSale = dtWebSale.Rows[i];
        //            INVOICE_HEAD_HANDOVER_DTO.ERP_GUIRow drERP = dtERP.NewERP_GUIRow();
        //            foreach (KeyValuePair<string, string> pair in dic)
        //            {
        //                drERP[pair.Value] = drWebSale[pair.Key];
        //            }
        //            dtERP.Rows.Add(drERP);
        //        }
        //        dtERP.AcceptChanges();

        //        cFacade.Insert_ERP_GUI(dtERP);
        //        con_log.Success("INVOICE_HEAD_HANDOVER:INVOICE_HEAD(WEB)=>ERP_GUI(POS)INSERT，異動筆數" + dtERP.Rows.Count.ToString());
        //    }
        //    catch (Exception ex) {
        //        con_log.Fail(ex.Message);
        //        throw ex; }
        //}
        #endregion

        static DataTable Query_WEB_INVOICE_HEAD()
        {
            OracleConnection oCon = null;
            try
            {
                //WEB
                oCon = OracleDBUtil.GetConnection();

                //回寫狀態STATUS='T'
                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                sb.Append("UPDATE INVOICE_HEAD SET FLG_TOERP='T' WHERE  nvl(FLG_TOERP,'N')='N'  ");
                OracleDBUtil.ExecuteSql(oCon, sb.ToString());

                sb.Length = 0;
                sb.Append("SELECT ");
                sb.Append("trunc(INVOICE_DATE) AS GUIDATE, ");
                sb.Append("INVOICE_NO AS GUINO, ");
                sb.Append("DECODE (NVL (IS_INVALID, 'N'), 'Y', '1', 'N', '0') AS CANFLAG, ");
                sb.Append("'01' AS COMPANYCODE, ");
                //sb.Append("BUYER AS CORPNO, ");
                sb.Append("UNI_NO AS CORPNO, ");
                sb.Append("TOTAL_AMOUNT AS TAXSALE, ");
                sb.Append("TAX, ");
                sb.Append("'0' AS DWNFLAG, ");
                sb.Append("STORE_NO AS STORENO, ");
                sb.Append("ID AS POSUUID_MASTER ");
                sb.Append("FROM INVOICE_HEAD ");
                //sb.Append("WHERE NVL (FLG_TOERP, 'N') = 'N'  ");
                sb.Append("WHERE FLG_TOERP= 'T'  ");
                sb.Append("union ");
                sb.Append("select trunc(invoice_date) as GUIDATE, ");
                sb.Append("invoice_no as GUINO, ");
                sb.Append("DECODE (NVL (IS_INVALID, 'N'), 'Y', '1', 'N', '0') AS CANFLAG, ");
                sb.Append("'01' AS COMPANYCODE, ");
                sb.Append("UNI_NO AS CORPNO, ");
                sb.Append("TOTAL_AMOUNT AS TAXSALE, ");
                sb.Append("TAX, ");
                sb.Append("'0' AS DWNFLAG, ");
                sb.Append("STORE_NO, ");
                sb.Append("ID AS POSUUID_MASTER ");
                sb.Append("FROM manual_invoice_head ");
                sb.Append("WHERE invalid_date IS NOT NULL AND UPPER (flg_credit_note) = 'T' ");
                //sb.Append("ORDER BY INVOICE_NO ");

                DataTable dt = OracleDBUtil.GetDataSet(oCon, sb.ToString()).Tables[0];
                return dt;
            }
            catch (Exception ex)
            {
                OutputMsg("3.INVOICE_HEAD(WEB),取出上傳的銷售交易 例外產生");
                throw ex;
            }
            finally
            {
                if (oCon.State == ConnectionState.Open) oCon.Close();
                oCon.Dispose();
                OracleConnection.ClearAllPools();
            }
        }

        static void Insert_ERP_GUI(DataTable dtADD)
        {
            //ERP
            OracleConnection conERP = null;
            OracleTransaction txERP = null;
            StringBuilder sb = new StringBuilder();
            try
            {
                //ERP
                conERP = OracleDBUtil.GetERPPOSConnection();
                txERP = conERP.BeginTransaction();
                
                if (dtADD.Rows.Count > 0)
                {
                    //ERP:ERP_SALES Insert
                    //OracleDBUtil.Insert(txERP, dtADD);
                    foreach (DataRow dr in dtADD.Rows)
                    {
                        sb.Length = 0;
                        sb.AppendLine(
                            @"Insert into ERP_GUI
                              (GUIDATE, GUINO, CANFLAG, 
                               COMPANYCODE, CORPNO, TAXSALE, 
                               TAX, DWNFLAG, 
                               STORENO )
                              Values
                              ([GUIDATE], [GUINO], [CANFLAG], 
                               [COMPANYCODE], [CORPNO], [TAXSALE], 
                               [TAX], [DWNFLAG], 
                               [STORENO] )");

                        sb.Replace("[GUIDATE]", formatDate(dr["GUIDATE"].ToString())); //DATE
                        sb.Replace("[GUINO]", OracleDBUtil.SqlStr(dr["GUINO"].ToString()));
                        sb.Replace("[CANFLAG]", OracleDBUtil.SqlStr(dr["CANFLAG"].ToString()));
                        sb.Replace("[COMPANYCODE]", OracleDBUtil.SqlStr(dr["COMPANYCODE"].ToString()));
                        sb.Replace("[CORPNO]", OracleDBUtil.SqlStr(dr["CORPNO"].ToString()));
                        sb.Replace("[TAXSALE]", (dr["TAXSALE"] != DBNull.Value) ? dr["TAXSALE"].ToString() : "0");//number
                        sb.Replace("[TAX]", (dr["TAX"]!=DBNull.Value) ? dr["TAX"].ToString() : "0"); //number
                        sb.Replace("[DWNFLAG]", "NULL");
                        //sb.Replace("[DWNDATE]", formatDate(dr["DWNDATE"].ToString()));//DATE
                        //STORE_NO回寫舊POS要加R，2121 > R2121
                        sb.Replace("[STORENO]", OracleDBUtil.SqlStr("R"+dr["STORENO"].ToString()));

                        OracleDBUtil.ExecuteSql(txERP, sb.ToString());
                    }
                }

                txERP.Commit();
                OutputMsg("ERP_GUI(POS)新增筆數:" + dtADD.Rows.Count.ToString());
            }
            catch (Exception ex)
            {
                txERP.Rollback();
                OutputMsg("4.ERP_GUI(POS)寫入，例外產生");
                throw ex;
            }
            finally
            {
                //ERP
                txERP.Dispose();
                if (conERP.State == ConnectionState.Open) conERP.Close();
                conERP.Dispose();
                OracleConnection.ClearAllPools();
            }
        }

        static void Update_INVOICE_HEAD()
        {
            //WEB
            OracleConnection objConn = null;
            OracleTransaction objTX = null;
            try
            {
                //WEB
                objConn = OracleDBUtil.GetConnection();
                objTX = objConn.BeginTransaction();
                //WEB:SALE_HEAD Update
                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                sb.Append("UPDATE INVOICE_HEAD SET FLG_TOERP='Y',DTM_TOERP =to_char(SYSDATE,'YYYYMMDDhh24miss') WHERE  FLG_TOERP = 'T' ");
                OracleDBUtil.ExecuteSql(objTX, sb.ToString());

                objTX.Commit();
            }
            catch (Exception ex)
            {
                objTX.Rollback();
                OutputMsg("5.INVOICE_HEAD(WEB)更新狀態為Y，例外產生");
                throw ex;
            }
            finally
            {
                //WEB
                objTX.Dispose();
                if (objConn.State == ConnectionState.Open) objConn.Close();
                objConn.Dispose();
                OracleConnection.ClearAllPools();
            }
        }

        static string formatDate(string sDate) 
        {
            string sRet = "NULL";
            if (!string.IsNullOrEmpty(sDate)) 
            {
                sRet="to_date('" + Convert.ToDateTime(sDate).ToString("yyyy/MM/dd") + "','YYYY/MM/DD')";
            }
            return sRet;
        }

        static void OutputMsg(string s)
        {
            Console.WriteLine(s);
            sMSG += s + "\r\n";
        }
    }
}
