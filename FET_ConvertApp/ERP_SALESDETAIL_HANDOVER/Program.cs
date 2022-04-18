using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.OracleClient;
using Advtek.Utility;
using System.Threading;

namespace ERP_SALESDETAIL_HANDOVER
{
    class Program
    {
        static string sMSG = "";
        static void Main(string[] args)
        {
            //初始化LOG
            OutputMsg("1.ERP_SALESDETAIL_HANDOVER開始執行");
            OutputMsg("2.初始化LOG");
            ConvertLog cLog = new ConvertLog("ERP_SALESDETAIL");
            try
            {
                //取出上傳的銷售交易
                OutputMsg("3.查詢SALE_HEAD+SALE_DETAIL(WEB)，更新狀態為T");
                DataTable dtWebSale = Query_WEB_SALE_HEAD();

                //寫入ERP_SALES(POS)
                OutputMsg("4.寫入ERP_SALES(POS)");
                Insert_ERP(dtWebSale);

                //更新狀態
                OutputMsg("6.更新SALE_HEAD(WEB)，狀態為Y");
                Update_SALE_HEAD();

                #region 程式備份
                //Dictionary<string, string> dic = new Dictionary<string, string>();
                ////< WEB_COLUMN_NAME,ERP_COLUMN_NAME >
                //dic.Add("MYNO", "MYNO");
                ////dic.Add("SALESCD", "SALESCD");//
                //dic.Add("COMPANYCODE", "COMPANYCODE");
                //dic.Add("STORENO", "STORENO");
                //dic.Add("TRANDATE", "TRANDATE");
                ////dic.Add("LOCATION", "LOCATION");
                //dic.Add("ITEMCODE", "ITEMCODE");
                //dic.Add("QTY", "QTY");
                //dic.Add("TXTYPE", "TXTYPE");


                ////Insert to 舊POS 的ERP_SALES+POS.SALE_HEAD 回壓上傳成功註記
                //ERP_SALESDETAIL_HANDOVER_DTO dsERP = new ERP_SALESDETAIL_HANDOVER_DTO();
                //ERP_SALESDETAIL_HANDOVER_DTO.ERP_SALESDataTable dtERP = dsERP.ERP_SALES;
                //foreach (DataColumn dc in dtERP.Columns)
                //    dc.AllowDBNull = true;

                //for (int i = 0; i < dtWebSale.Rows.Count; i++)
                //{
                //    DataRow drWebSale = dtWebSale.Rows[i];
                //    ERP_SALESDETAIL_HANDOVER_DTO.ERP_SALESRow drERP = dtERP.NewERP_SALESRow();
                //    foreach (KeyValuePair<string, string> pair in dic)
                //    {
                //        drERP[pair.Value] = drWebSale[pair.Key];
                //    }
                //    dtERP.Rows.Add(drERP);
                //}
                //dtERP.AcceptChanges();

                //cFacade.Insert_ERP_ERP_SALES(dtERP);
                #endregion

                OutputMsg("7.執行結束，寫入LOG");
                cLog.Success(sMSG);
                Thread.Sleep(5000);
            }
            catch (Exception ex)
            {
                cLog.Fail(sMSG + ex.Message);
                Thread.Sleep(5000);
            }
        }

        static DataTable Query_WEB_SALE_HEAD()
        {
            OracleConnection oCon = null;
            OracleTransaction otx = null;
            try
            {
                //WEB
                oCon = OracleDBUtil.GetConnection();

                //回寫狀態STATUS='T'
                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                sb.Append("UPDATE SALE_HEAD SET FLG_TOERP='T' WHERE  FLG_TOERP IS NULL and SALE_STATUS='2' and SALE_TYPE='1' ");
                OracleDBUtil.ExecuteSql(oCon, sb.ToString());
                sb.Length = 0;
                sb.Append("UPDATE SALE_HEAD SET FLG_TO_INVALID_ERP='T' WHERE FLG_TO_INVALID_ERP IS NULL and INVALID_NO is not null and SALE_TYPE='1' ");
                OracleDBUtil.ExecuteSql(oCon, sb.ToString());
                sb.Length = 0;
                sb.Append("UPDATE SALE_HEAD SET FLG_TOERP='T' WHERE FLG_TOERP IS NULL and FLG_TO_INVALID_ERP IS NULL and INVALID_NO is not null and SALE_TYPE='1' ");
                OracleDBUtil.ExecuteSql(oCon, sb.ToString());

                //串丟回POS的ERP_MYNO(R2121-YYYYMMDD-HH24MISS-Machine_ID-SALE_NO-SEQNO

                sb.Length = 0;
                sb.Append(@"SELECT T2.ID,T1.POSUUID_MASTER,T2.ID,'R' || T1.STORE_NO STORE_NO,TO_CHAR(TRADE_DATE,'YYYYMMDD-hh24miss') TRADE_DATE,substr(T1.MACHINE_ID,-2,2) MACHINE_ID,SUBSTR(T1.SALE_NO,-8,8) SALE_NO
                            from SALE_HEAD T1,SALE_DETAIL T2 where T1.POSUUID_MASTER=T2.POSUUID_MASTER and T2.POSUUID_MASTER IN (SELECT POSUUID_MASTER FROM SALE_HEAD WHERE FLG_TOERP='T')
                            ORDER BY T1.POSUUID_MASTER,T2.ID");
                DataTable dt1 = OracleDBUtil.GetDataSet(oCon, sb.ToString()).Tables[0];
                sb.Length = 0;
                sb.Append(@"SELECT T2.ID,T1.POSUUID_MASTER,T2.ID,'R' || T1.STORE_NO STORE_NO,TO_CHAR(INVALID_DATE,'YYYYMMDD-hh24miss') INVALID_DATE,substr(T1.MACHINE_ID,-2,2) MACHINE_ID,SUBSTR(T1.INVALID_NO,-8,8)  SALE_NO
                            from SALE_HEAD T1,SALE_DETAIL T2 where T1.POSUUID_MASTER=T2.POSUUID_MASTER and T2.POSUUID_MASTER IN (SELECT POSUUID_MASTER FROM SALE_HEAD WHERE FLG_TO_INVALID_ERP='T' and INVALID_NO is not null )
                            ORDER BY T1.POSUUID_MASTER,T2.ID");
                DataTable dt2 = OracleDBUtil.GetDataSet(oCon, sb.ToString()).Tables[0];

                otx = oCon.BeginTransaction();
                string MASTER = "";
                int COUNT = 0;
                foreach (DataRow dr in dt1.Rows)
                {
                    if (MASTER != dr["POSUUID_MASTER"].ToString())
                    {
                        COUNT = 1;
                        //Thread.Sleep(1000);
                    }
                    else
                    {
                        COUNT += 1;
                    }
                    string MYNO = dr["STORE_NO"] + "_" + dr["TRADE_DATE"].ToString().Replace("-", "_") + "_" + dr["MACHINE_ID"] + "_" + dr["SALE_NO"] + "_" + COUNT.ToString().PadLeft(3, '0');

                    if (MYNO.Length > 37)
                    {
                        MYNO = MYNO.ToString().Substring(0, 37);
                    }

                    sb.Length = 0;
                    sb.Append(@"UPDATE SALE_DETAIL 
                                SET ERP_MYNO =[ERP_MYNO] WHERE ID = [ID]");
                    sb.Replace("[ERP_MYNO]", OracleDBUtil.SqlStr(MYNO));
                    sb.Replace("[ID]", OracleDBUtil.SqlStr(dr["ID"].ToString()));
                    OracleDBUtil.ExecuteSql(otx, sb.Replace("\r", " ").Replace("\n", " ").ToString());
                    MASTER = dr["POSUUID_MASTER"].ToString();
                }



                MASTER = "";
                COUNT = 0;
                foreach (DataRow dr in dt2.Rows)
                {
                    if (MASTER != dr["POSUUID_MASTER"].ToString())
                    {
                        COUNT = 1;
                        //Thread.Sleep(1000);
                    }
                    else
                    {
                        COUNT += 1;
                    }
                    string MYNO = dr["STORE_NO"] + "_" + dr["INVALID_DATE"].ToString().Replace("-", "_") + "_" + dr["MACHINE_ID"] + "_" + dr["SALE_NO"] + "_" + COUNT.ToString().PadLeft(3, '0');

                    if (MYNO.Length > 37)
                    {
                        MYNO = MYNO.ToString().Substring(0, 37);
                    }

                    sb.Length = 0;
                    sb.Append(@"UPDATE SALE_DETAIL 
                                SET ERP_MYNO_INVALID =[ERP_MYNO] WHERE ID = [ID]");
                    sb.Replace("[ERP_MYNO]", OracleDBUtil.SqlStr(MYNO));
                    sb.Replace("[ID]", OracleDBUtil.SqlStr(dr["ID"].ToString()));
                    OracleDBUtil.ExecuteSql(otx, sb.Replace("\r", " ").Replace("\n", " ").ToString());
                    MASTER = dr["POSUUID_MASTER"].ToString();
                }
                otx.Commit();

                sb.Length = 0;
                sb.Append("SELECT ");
                //sb.Append("DETAIL.ID AS MYNO, ");
                sb.Append("'01'  AS COMPANYCODE, ");
                sb.Append("HEAD.STORE_NO AS STORENO, ");

                sb.Append("HEAD.FLG_TOERP, ");
                sb.Append("HEAD.FLG_TO_INVALID_ERP, ");


                sb.Append("HEAD.SALE_STATUS AS TXTYPE, ");
                sb.Append("TRUNC(HEAD.TRADE_DATE) AS TRANDATE, ");
                sb.Append("TRUNC(HEAD.INVALID_DATE) AS INVALIDDATE, ");
                sb.Append("(SELECT STOCK_NAME FROM LOC WHERE LOC_ID = INV_GOODLOCUUID ) AS LOCATION, ");
                sb.Append("DETAIL.PRODNO AS ITEMCODE, ");
                //sb.Append("DETAIL.QUANTITY AS QTY, ");
                sb.Append("nvl(DETAIL.QUANTITY,0) AS QTY, ");
                sb.Append("'1' AS DWNFLAG, ");
                sb.Append(" DETAIL.ERP_MYNO as MYNO, ");//20110217
                sb.Append(" DETAIL.ERP_MYNO_INVALID as MYNO_INVALID ");//20110218
                sb.Append("FROM SALE_HEAD HEAD, SALE_DETAIL DETAIL ");
                sb.Append("WHERE HEAD.POSUUID_MASTER = DETAIL.POSUUID_MASTER AND (FLG_TOERP = 'T' or FLG_TO_INVALID_ERP='T') ");
                sb.Append("AND (LENGTH(TRIM(DETAIL.PRODNO))) = 9  ");
                sb.Append("ORDER BY HEAD.SALE_NO ");

                DataTable dt = OracleDBUtil.GetDataSet(oCon, sb.ToString()).Tables[0];
                return dt;
            }
            catch (Exception ex)
            {
                //otx.Rollback();
                OutputMsg("3.查詢SALE_HEAD+SALE_DETAIL(WEB)，更新狀態為T 例外產生");
                throw ex;
            }
            finally
            {
                //if (otx != null) otx.Dispose();
                if (oCon.State == ConnectionState.Open) oCon.Close();
                oCon.Dispose();
                OracleConnection.ClearAllPools();
            }
        }

        static void Insert_ERP(DataTable dtADD)
        {
            OracleConnection conERP = null;
            OracleTransaction txERP = null;
            StringBuilder sb = new StringBuilder();
            int iCountSale = 0;
            int iCountOut = 0;
            string myno = "";
            try
            {
                conERP = OracleDBUtil.GetERPPOSConnection();
                txERP = conERP.BeginTransaction();
                if (dtADD.Rows.Count > 0)
                {
                    //ERP:ERP_SALES Insert

                    foreach (DataRow dr in dtADD.Rows)
                    {
                        sb.Length = 0;
                        string sTemp = "";
                        if ((dr["MYNO"] != DBNull.Value || !string.IsNullOrEmpty(dr["MYNO"].ToString())) && dr["FLG_TOERP"].ToString() == "T")
                        {
                            myno = dr["MYNO"].ToString();
                            sb.Append(@"INSERT INTO ERP_SALES
                                     (MYNO, COMPANYCODE, TXTYPE, 
                                      STORENO, TRANDATE, ITEMCODE, 
                                      QTY, DWNFLAG)
                                    VALUES
                                     ([MYNO], [COMPANYCODE], [TXTYPE], 
                                      [STORENO], [TRANDATE], [ITEMCODE], 
                                      [QTY], [DWNFLAG]);");

                            sb.Replace("[MYNO]", OracleDBUtil.SqlStr(dr["MYNO"].ToString()));
                            sb.Replace("[COMPANYCODE]", OracleDBUtil.SqlStr(dr["COMPANYCODE"].ToString()));
                            //sb.Replace("[TXTYPE]", OracleDBUtil.SqlStr(dr["TXTYPE"].ToString()));
                            sb.Replace("[TXTYPE]", "'1'");//20110221 銷售
                            //STORE_NO回寫舊POS要加R，2121 > R2121
                            sb.Replace("[STORENO]", OracleDBUtil.SqlStr("R" + dr["STORENO"].ToString()));


                            sTemp = dr["TRANDATE"].ToString();
                            if (!string.IsNullOrEmpty(sTemp))
                            {
                                sTemp = "to_date('" + Convert.ToDateTime(sTemp).ToString("yyyy/MM/dd") + "','YYYY/MM/DD')";
                            }
                            else
                            {
                                sTemp = "NULL";
                            }
                            sb.Replace("[TRANDATE]", sTemp);//DATE

                            sb.Replace("[ITEMCODE]", OracleDBUtil.SqlStr(dr["ITEMCODE"].ToString()));
                            int aQty = Convert.ToInt32(dr["QTY"].ToString());//***20110323
                            string sQty = (aQty >= 0) ? aQty.ToString() : (-aQty).ToString();
                            sb.Replace("[QTY]", sQty);//number
                            //sb.Replace("[DWNFLAG]", OracleDBUtil.SqlStr(dr["DWNFLAG"].ToString()));
                            sb.Replace("[DWNFLAG]", "NULL");

                            iCountSale++;
                        }

                        if ((dr["MYNO_INVALID"] != DBNull.Value || !string.IsNullOrEmpty(dr["MYNO_INVALID"].ToString())) && dr["FLG_TO_INVALID_ERP"].ToString() == "T") //有註記銷退，多寫一筆TYPE=2
                        {
                            myno = dr["MYNO_INVALID"].ToString();
                            sb.Append(@"INSERT INTO ERP_SALES
                                     (MYNO, COMPANYCODE, TXTYPE, 
                                      STORENO, TRANDATE, ITEMCODE, 
                                      QTY, DWNFLAG)
                                    VALUES
                                     ([MYNO], [COMPANYCODE], [TXTYPE], 
                                      [STORENO], [TRANDATE], [ITEMCODE], 
                                      [QTY], [DWNFLAG]);");

                            sb.Replace("[MYNO]", OracleDBUtil.SqlStr(dr["MYNO_INVALID"].ToString()));
                            sb.Replace("[COMPANYCODE]", OracleDBUtil.SqlStr(dr["COMPANYCODE"].ToString()));
                            sb.Replace("[TXTYPE]", "'2'"); //銷退
                            //STORE_NO回寫舊POS要加R，2121 > R2121
                            sb.Replace("[STORENO]", OracleDBUtil.SqlStr("R" + dr["STORENO"].ToString()));

                            sTemp = "";
                            sTemp = dr["TRANDATE"].ToString();
                            if (!string.IsNullOrEmpty(sTemp))
                            {
                                sTemp = "to_date('" + Convert.ToDateTime(sTemp).ToString("yyyy/MM/dd") + "','YYYY/MM/DD')";
                            }
                            else
                            {
                                sTemp = "NULL";
                            }
                            sb.Replace("[TRANDATE]", sTemp);//DATE

                            sb.Replace("[ITEMCODE]", OracleDBUtil.SqlStr(dr["ITEMCODE"].ToString()));
                            //sb.Replace("[QTY]", dr["QTY"].ToString());//number
                            int aQty = Convert.ToInt32(dr["QTY"].ToString());//***20110323
                            string sQty = (aQty < 0) ? aQty.ToString() : (-aQty).ToString();
                            sb.Replace("[QTY]", sQty);//number
                            //sb.Replace("[QTY]", dr["QTY"].ToString());//number
                            //sb.Replace("[DWNFLAG]", OracleDBUtil.SqlStr(dr["DWNFLAG"].ToString()));
                            sb.Replace("[DWNFLAG]", "NULL");
                            iCountOut++;
                        }
                        if (sb.Length > 0)
                        {
                            sb.Insert(0, "BEGIN ");
                            sb.Append(" END;");
                            OracleDBUtil.ExecuteSql(txERP, sb.Replace("\r", " ").Replace("\n", " ").ToString());
                        }


                    }
                    txERP.Commit();
                    OutputMsg("5.SALE_HEAD+SALE_DETAIL(WEB)=>ERP_SALES(POS)，銷售新增筆數:" + iCountSale.ToString() + ",銷退新增筆數:" + iCountOut.ToString());
                }
            }
            catch (Exception ex)
            {
                //txERP.Rollback();
                OutputMsg("4.寫入ERP_SALES(POS) 例外產生");
                OutputMsg("MYNO編號" + myno);
                OutputMsg(ex.Message);
                throw ex;
            }
            finally
            {
                txERP.Dispose();
                if (conERP.State == ConnectionState.Open) conERP.Close();
                conERP.Dispose();
                OracleConnection.ClearAllPools();
            }
        }
        static void Update_SALE_HEAD()
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
                sb.Append("UPDATE SALE_HEAD SET  FLG_TOERP='Y',DTM_TOERP =SYSDATE WHERE  FLG_TOERP = 'T' ");
                OracleDBUtil.ExecuteSql(objTX, sb.ToString());
                sb.Length = 0;
                sb.Append("UPDATE SALE_HEAD SET  FLG_TO_INVALID_ERP='Y',DTM_TO_INVALID_ERP =SYSDATE WHERE  FLG_TO_INVALID_ERP = 'T' ");
                OracleDBUtil.ExecuteSql(objTX, sb.ToString());


                objTX.Commit();
            }
            catch (Exception ex)
            {
                objTX.Rollback();
                OutputMsg("5.更新SALE_HEAD(WEB)，狀態為Y，例外產生");
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
