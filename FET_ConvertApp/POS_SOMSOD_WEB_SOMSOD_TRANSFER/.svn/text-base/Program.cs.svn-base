using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.OracleClient;
using Advtek.Utility;

using System.Threading;

namespace POS_SOMSOD_WEB_SOMSOD_TRANSFER
{
    class Program
    {

        static OracleConnection objConn = null;
        static OracleTransaction txConn = null;
        static OracleConnection objConn_erp = null;
        static OracleTransaction txConn_erp = null;
        static string sMSG;

        static void Main(string[] args)
        {
            //初始化LOG
            Console.WriteLine("1.POS_SOMSOD_WEB_SOMSOD_TRANSFER");
            Console.WriteLine("2.初始化LOG");
            ConvertLog con_log = new ConvertLog("POS_SOMSOD_WEB_TRANS");
            sMSG = "";
            try
            {
                Console.WriteLine("3.建立新舊資料庫連線");
                objConn = OracleDBUtil.GetConnection();
                objConn_erp = OracleDBUtil.GetERPPOSConnection();

                //GET NEW POS DATA
                //DataTable dtERPSOM = GetOLdPosSOM();
                Console.WriteLine("4.清除TEMP TABLE");
                clearTemp();

                Console.WriteLine("5.查詢SOM(POS)");
                DataTable dtERPSOM = Query_OLD_SOM();
                Console.WriteLine("6.SOM(POS)複製到SOM_TEMP(WEB)");
                CloneSOM(dtERPSOM);
                //DataTable dtERPSOD = GetOLdPosSOD();Query_OLD_SOD
                Console.WriteLine("7.查詢SOD(POS)");
                DataTable dtERPSOD = Query_OLD_SOD();
                Console.WriteLine("8.SOD(POS)複製到SOD_TEMP(WEB)");
                CloneSOD(dtERPSOD);

                Console.WriteLine("9.SP_SOMSOD_TRANSFER");
                txConn = objConn.BeginTransaction();
                OracleParameter op1 = new OracleParameter();
                op1.OracleType = OracleType.VarChar;
                op1.ParameterName = "outCODE";
                op1.Size = 50;
                op1.Direction = ParameterDirection.Output;
                OracleParameter op2 = new OracleParameter();
                op2.OracleType = OracleType.VarChar;
                op2.ParameterName = "outMESSAGE";
                op2.Size = 2000;
                op2.Direction = ParameterDirection.Output;
                Console.WriteLine("10.執行PK_CONVERT.SP_SOMSOD_TRANSFER");
                OracleDBUtil.ExecuteSql_SP(
                    txConn,
                    "PK_CONVERT.SP_SOMSOD_TRANSFER",
                    op1,
                    op2
                    );
                txConn.Commit();

                //回寫狀態
                Console.WriteLine("11.SOM(POS)狀態更新");
                Update_OLD_SOM();

                //成功資訊
                Console.WriteLine("執行結束，寫入LOG");
                con_log.Success(op2.Value.ToString());
                Thread.Sleep(5000);
            }
            catch (Exception ex)
            {
                
                //失敗資訊
                Console.WriteLine("例外產生，寫入LOG");
                sMSG += "SP_SOMSOD_TRANSFER錯誤" + ex.Message + "\n";
                con_log.Fail(sMSG);
                Console.WriteLine(sMSG);
                Thread.Sleep(5000);
            }
            finally 
            {
                if (txConn != null) txConn.Dispose();
                if (txConn_erp != null) txConn_erp.Dispose();
                if (objConn_erp.State == ConnectionState.Open) objConn_erp.Close();
                if (objConn.State == ConnectionState.Open) objConn.Close();
                objConn_erp.Dispose();
                objConn.Dispose();
                OracleConnection.ClearAllPools();
            }
        }

      
        public static DataTable Query_OLD_SOM()
        {
            OracleConnection oCon = null;
            try
            {
                //OLD POS
                oCon = OracleDBUtil.GetERPPOSConnection();

                ////回寫狀態STATUS='T'
                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                OracleDBUtil.ExecuteSql(oCon, "Update SOM set WP_DWNFLAG='T' where nvl(WP_DWNFLAG,'0')='0'");
                sb.Length = 0;
                sb.Append("SELECT *  ");
                sb.Append("FROM SOM where WP_DWNFLAG='T' ");

                DataTable dt = OracleDBUtil.GetDataSet(oCon, sb.ToString()).Tables[0];
                return dt;
            }
            catch (Exception ex)
            {
                sMSG += "Query_OLD_SOM()錯誤" + ex.Message + "\n";
                throw ex;
            }
            finally
            {
                if (oCon.State == ConnectionState.Open) oCon.Close();
                oCon.Dispose();
                OracleConnection.ClearAllPools();
            }
        }

        public static DataTable Query_OLD_SOD()
        {
            OracleConnection oCon = null;
            try
            {
                //OLD POS
                oCon = OracleDBUtil.GetERPPOSConnection();

                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                sb.Length = 0;
                sb.Append("SELECT *  ");
                sb.Append("FROM SOD WHERE SOD.SOID IN (SELECT SOM.SOID FROM SOM WHERE SOM.WP_DWNFLAG='T' ) ");

                DataTable dt = OracleDBUtil.GetDataSet(oCon, sb.ToString()).Tables[0];
                return dt;
            }
            catch (Exception ex)
            {
                sMSG += "Query_OLD_SOD()錯誤" + ex.Message + "\n";
                throw ex;
            }
            finally
            {
                if (oCon.State == ConnectionState.Open) oCon.Close();
                oCon.Dispose();
                OracleConnection.ClearAllPools();
            }
        }
        public static void CloneSOM(DataTable dt) 
        {
            StringBuilder sb = new StringBuilder();
            
            try {
                txConn = objConn.BeginTransaction();
                foreach (DataRow dr in dt.Rows)
                {
                    
                    sb.Length = 0;
                    sb.AppendLine(
                        @"Insert into SOM_TEMP
                           (SOMID, SOID, STORENO, 
                            COMPANYCODE, SONO, REVISIONNUMBER, 
                            SUBTOTAL, ORDERTYPE, TAXAMT, 
                            ORDERDATE, PAIDMODEID, TOTALAMT, 
                            MODIDATE, SIMCARDNO, IFVOID, 
                            COMPANYNAME, CREDITCARDTYPEID, CUSTOMERNAME, 
                            SEX, COMPTEL, DELIADDRREV, 
                            CREDITCARDNO, HOMETEL, DELIADDRREVREASON, 
                            CHECKPRODNO, MOTONUM, CANCELSOREASON, 
                            DELIFLAG, DELIADDR, MEMO0, 
                            AUTHNO, UNINO, PRECREDITCARDPAID, 
                            PREAPCLEARING, PREBUSINESSPAID, ORDERSTATUS, 
                            PREAR, DELIVERMODEID, CASHPAID, 
                            PAIDDATE, VOIDREASON, DELISTATUS, 
                            RECESTATUS, RECEDATE, MODIUSER, 
                            BILLTO, SRTYPE, PICKNO, 
                            SHIPPINGDATE, SRNO, MEMO1, 
                            SRDATE, ACTUALSRDATE, MEMO2, 
                            MEMO3, SREMPNO, PROMOTENO, 
                            EMPNO, CONVERTDATE, CONVERTTIME, 
                            CUSTTYPE, IA_AMT, RECEUSER, 
                            COSTCENTER, ACCNAME, PICKEMPNO, 
                            REAPPLYEMPNO, REDATE, REEMPNO, 
                            REMODDATE, CHG_DIFF_AMT, SHIPPINGNO, 
                            SHIPPINGUSER, ACTUALSREMPNO, PICKDATETIME, 
                            IFPROCESSED, BOOKDATE, NEWORDERAMT, 
                            OLDPAIDMODEID, OLDSONO, IFSAMEMODEL, 
                            B_DATE, IFPROCESSEDREFUND, ORDERMODIFYUSER, 
                            ORDERMODIFYDATE, REAMOUNT, PROMOTENO_B_DATE, 
                            PAYAR, CHGDATE, CHGEMPNO, 
                            NOCHGAR, NONRECV, MEMO_R, 
                            SOURCE_TYPE, MW_TXID, ASR2SR_MEMO, 
                            PREMOBILECREDITCARDPAID, PREMOBILEVIRTUALACCOUNT, NAPAID, 
                            DELI_SEG, UNI_TITLE, MEMO_S, 
                            SAFLAG, INVAMT, FIRST_SONO, 
                            VSONO, SUBORDERTYPE, ULSNO, 
                            NOT_YET_DLV, VSONOR, BS_FLAG, 
                            BS_DATE, BSS_FLAG, BSS_DATE, 
                            BSN_FLAG, BSN_DATE, BR_FLAG, 
                            BR_DATE, DELI_STORENO, NPFLAG, 
                            DEADDATE, PACKAGE_NO, EINVOICE_FLAG, 
                            PREORDER_FLAG, INVOICE_DOC, TAXABLE, 
                            MONOPOLY_FLAG, 
                            DONATIONUNIT, TOB_FLAG, TOB_DATE,WP_DWNFLAG)
                            Values
                           ([SOMID], [SOID], [STORENO], 
                            [COMPANYCODE], [SONO], [REVISIONNUMBER], 
                            [SUBTOTAL], [ORDERTYPE], [TAXAMT], 
                            [ORDERDATE], [PAIDMODEID], [TOTALAMT], 
                            [MODIDATE], [SIMCARDNO], [IFVOID], 
                            [COMPANYNAME], [CREDITCARDTYPEID], [CUSTOMERNAME], 
                            [SEX], [COMPTEL], [DELIADDRREV], 
                            [CREDITCARDNO], [HOMETEL], [DELIADDRREVREASON], 
                            [CHECKPRODNO], [MOTONUM], [CANCELSOREASON], 
                            [DELIFLAG], [DELIADDR], [MEMO0], 
                            [AUTHNO], [UNINO], [PRECREDITCARDPAID], 
                            [PREAPCLEARING], [PREBUSINESSPAID], [ORDERSTATUS], 
                            [PREAR], [DELIVERMODEID], [CASHPAID], 
                            [PAIDDATE], [VOIDREASON], [DELISTATUS], 
                            [RECESTATUS], [RECEDATE], [MODIUSER], 
                            [BILLTO], [SRTYPE], [PICKNO], 
                            [SHIPPINGDATE], [SRNO], [MEMO1], 
                            [SRDATE], [ACTUALSRDATE], [MEMO2], 
                            [MEMO3], [SREMPNO], [PROMOTENO], 
                            [EMPNO], [CONVERTDATE], [CONVERTTIME], 
                            [CUSTTYPE], [IA_AMT], [RECEUSER], 
                            [COSTCENTER], [ACCNAME], [PICKEMPNO], 
                            [REAPPLYEMPNO], [REDATE], [REEMPNO], 
                            [REMODDATE], [CHG_DIFF_AMT], [SHIPPINGNO], 
                            [SHIPPINGUSER], [ACTUALSREMPNO], [PICKDATETIME], 
                            [IFPROCESSED], [BOOKDATE], [NEWORDERAMT], 
                            [OLDPAIDMODEID], [OLDSONO], [IFSAMEMODEL], 
                            [B_DATE], [IFPROCESSEDREFUND], [ORDERMODIFYUSER], 
                            [ORDERMODIFYDATE], [REAMOUNT], [PROMOTENO_B_DATE], 
                            [PAYAR], [CHGDATE], [CHGEMPNO], 
                            [NOCHGAR], [NONRECV], [MEMO_R], 
                            [SOURCE_TYPE], [MW_TXID], [ASR2SR_MEMO], 
                            [PREMOBILECREDITCARDPAID], [PREMOBILEVIRTUALACCOUNT], [NAPAID], 
                            [DELI_SEG], [UNI_TITLE], [MEMO_S], 
                            [SAFLAG], [INVAMT], [FIRST_SONO], 
                            [VSONO], [SUBORDERTYPE], [ULSNO], 
                            [NOT_YET_DLV], [VSONOR], [BS_FLAG], 
                            [BS_DATE], [BSS_FLAG], [BSS_DATE], 
                            [BSN_FLAG], [BSN_DATE], [BR_FLAG], 
                            [BR_DATE], [DELI_STORENO], [NPFLAG], 
                            [DEADDATE], [PACKAGE_NO], [EINVOICE_FLAG], 
                            [PREORDER_FLAG], [INVOICE_DOC], [TAXABLE], 
                            [MONOPOLY_FLAG], 
                            [DONATIONUNIT], [TOB_FLAG], [TOB_DATE], [WP_DWNFLAG])"
                     );
                    foreach (DataColumn dc in dt.Columns)
                    {
                        string dcname = dc.ColumnName;
                        switch (dc.DataType.ToString())
                        {
                            case "System.String":
                                sb.Replace("[" + dcname + "]", OracleDBUtil.SqlStr(dr[dcname].ToString()));
                                break;
                            case "System.Decimal":
                                sb.Replace("[" + dcname + "]", (dr[dcname] != DBNull.Value) ? dr[dcname].ToString() : "0");
                                break;
                            case "System.DateTime":
                                sb.Replace("[" + dcname + "]", (dr[dcname] != DBNull.Value) ? OracleDBUtil.DateFormate(dr[dcname]) : "null");
                                break;
                            default:
                                sb.Replace("[" + dcname + "]", OracleDBUtil.SqlStr(dr[dcname].ToString()));
                                break;
                        }
                    }
                    
                    OracleDBUtil.ExecuteSql(txConn, sb.ToString());
                }
                txConn.Commit();
            }
            catch (Exception ex) {
                sMSG += "CloneSOM(DataTable dt)錯誤:" + ex.Message + "\n";
                if (txConn != null) txConn.Rollback();
                throw ex;
            }
            
        }

        public static void CloneSOD(DataTable dt)
        {
            StringBuilder sb = new StringBuilder();

            try
            {
                txConn = objConn.BeginTransaction();
                foreach (DataRow dr in dt.Rows)
                {
                    
                    sb.Length = 0;
                    sb.AppendLine(
                        @"Insert into SOD_TEMP
                           (LINENUMBER, SOID, STORENO, 
                            COMPANYCODE, ORDERQTY, UNITPRICE, 
                            UNITPRICEDISCOUNT, BOOKEDQTY, LINETOTAL, 
                            MODIDATE, SHIPPINGQTY, LINETOTALBEFORETAX, 
                            TAXAMOUNT, REMARK, MODIUSER, 
                            IFDELINEEDED, SRQTY, REASONID, 
                            IFSAMEMODEL, PRODNO, ACTUALSRQTY, 
                            ACTUALSRLOCATION, ORDERTYPE, PRODUCTID, 
                            SENDALERTMAIL, SONO, ULSNO, 
                            TRANINQTY, PLU_NAME)
                            Values
                           ([LINENUMBER], [SOID], [STORENO], 
                            [COMPANYCODE], [ORDERQTY], [UNITPRICE], 
                            [UNITPRICEDISCOUNT], [BOOKEDQTY], [LINETOTAL], 
                            [MODIDATE], [SHIPPINGQTY], [LINETOTALBEFORETAX], 
                            [TAXAMOUNT], [REMARK], [MODIUSER], 
                            [IFDELINEEDED], [SRQTY], [REASONID], 
                            [IFSAMEMODEL], [PRODNO], [ACTUALSRQTY], 
                            [ACTUALSRLOCATION], [ORDERTYPE], [PRODUCTID], 
                            [SENDALERTMAIL], [SONO], [ULSNO], 
                            [TRANINQTY], [PLU_NAME])"
                     );
                    foreach (DataColumn dc in dt.Columns)
                    {
                        string dcname = dc.ColumnName;
                        switch (dc.DataType.ToString())
                        {
                            case "System.String":
                                sb.Replace("[" + dcname + "]", OracleDBUtil.SqlStr(dr[dcname].ToString()));
                                break;
                            case "System.Decimal":
                                sb.Replace("[" + dcname + "]", (dr[dcname] != DBNull.Value) ? dr[dcname].ToString() : "0");
                                break;
                            case "System.DateTime":
                                sb.Replace("[" + dcname + "]", (dr[dcname] != DBNull.Value) ? OracleDBUtil.DateFormate(dr[dcname]) : "null");
                                break;
                            default:
                                sb.Replace("[" + dcname + "]", OracleDBUtil.SqlStr(dr[dcname].ToString()));
                                break;
                        }
                    }

                    OracleDBUtil.ExecuteSql(txConn, sb.ToString());
                }
                txConn.Commit();
            }
            catch (Exception ex)
            {
                sMSG += "CloneSOD(DataTable dt)錯誤" + ex.Message + "\n";
                if (txConn != null) txConn.Rollback();
                throw ex;
            }

        }

        public static void Update_OLD_SOM()
        {
            OracleConnection oCon = null;
            try
            {
                //OLD POS
                oCon = OracleDBUtil.GetERPPOSConnection();

                ////回寫狀態STATUS='1'
                OracleDBUtil.ExecuteSql(oCon, "Update SOM set WP_DWNFLAG='1' where WP_DWNFLAG='T'");
            }
            catch (Exception ex)
            {
                sMSG += "Update_OLD_SOM()錯誤" + ex.Message + "\n";
                throw ex;
            }
            finally
            {
                if (oCon.State == ConnectionState.Open) oCon.Close();
                oCon.Dispose();
                OracleConnection.ClearAllPools();
            }
        }

        static void clearTemp() 
        {
            try {
                txConn = objConn.BeginTransaction();
                OracleDBUtil.ExecuteSql(txConn, "Delete from SOM_TEMP");
                OracleDBUtil.ExecuteSql(txConn, "Delete from SOD_TEMP");
                txConn.Commit();
            }
            catch (Exception ex) {
                sMSG += "clearTemp()錯誤:" + ex.Message + "\n";
                txConn.Rollback();
                throw ex; 
            }
        }
    }
}