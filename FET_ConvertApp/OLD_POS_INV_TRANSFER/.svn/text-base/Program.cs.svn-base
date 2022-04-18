using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.OracleClient;
using Advtek.Utility;

namespace OLD_POS_INV_TRANSFER
{
    class Program
    {
        static void Main(string[] args)
        {
            //初始化LOG
            ConvertLog con_log = new ConvertLog("OLD_POS_INV_TRANSFER");
            OracleConnection objConn = null;
            OracleTransaction objTX = null;
            OracleConnection objConn_erp = null;
            try
            {
                string sMsg = "";
                Console.WriteLine("建立新舊DB連線");
                objConn = OracleDBUtil.GetConnection();
                
                objConn_erp = OracleDBUtil.GetERPPOSConnection();

                //GET OLD POS DATA
                Console.WriteLine("查詢STOCK(POS)");
                DataTable dt = Query_ERP_THISMONTH_STOCK();

                if (dt.Rows.Count > 0)
                {
                    Console.WriteLine("清除STOCK_TEMP(WEB)");
                    OracleDBUtil.ExecuteSql(objConn, "DELETE STOCK_TEMP");

                    StringBuilder sb = new StringBuilder();
                    Console.WriteLine("寫入STOCK_TEMP");
                    objTX = objConn.BeginTransaction();
                    foreach (DataRow dr in dt.Rows)
                    {
                        sb.Length = 0;
                        sb.AppendLine(
                            @"Insert into STOCK_TEMP
                                  (PRODNO, STKYYMM, STORENO, 
                                   COMPANYCODE, BEGSTK, ORDQTY, 
                                   INQTY, RTNQTY, SALEQTY, 
                                   SALERTNQTY, TRNINQTY, TRNOUTQTY, 
                                   STKCHGQTY, STKCHKQTY, STKADJQTY, 
                                   ENDQTY, UPDDATE, UPDUSRNO, 
                                   DEPTGETQTY, ERPRTN_QTY )
                                  Values
                                  ([PRODNO], [STKYYMM], [STORENO], 
                                   [COMPANYCODE], [BEGSTK], [ORDQTY], 
                                   [INQTY], [RTNQTY], [SALEQTY], 
                                   [SALERTNQTY], [TRNINQTY], [TRNOUTQTY], 
                                   [STKCHGQTY], [STKCHKQTY], [STKADJQTY], 
                                   [ENDQTY], [UPDDATE], [UPDUSRNO], 
                                   [DEPTGETQTY], [ERPRTN_QTY] )");

                        sb.Replace("[PRODNO]", OracleDBUtil.SqlStr(dr["PRODNO"].ToString()));
                        sb.Replace("[STKYYMM]", OracleDBUtil.SqlStr(dr["STKYYMM"].ToString()));
                        sb.Replace("[STORENO]", OracleDBUtil.SqlStr(dr["STORENO"].ToString()));
                        sb.Replace("[COMPANYCODE]", OracleDBUtil.SqlStr(dr["COMPANYCODE"].ToString()));
                        sb.Replace("[BEGSTK]", OracleDBUtil.SqlStr(dr["BEGSTK"].ToString()));
                        sb.Replace("[ORDQTY]", OracleDBUtil.SqlStr(dr["ORDQTY"].ToString()));
                        sb.Replace("[INQTY]", OracleDBUtil.SqlStr(dr["INQTY"].ToString()));
                        sb.Replace("[RTNQTY]", OracleDBUtil.SqlStr(dr["RTNQTY"].ToString()));
                        sb.Replace("[SALEQTY]", OracleDBUtil.SqlStr(dr["SALEQTY"].ToString()));
                        sb.Replace("[SALERTNQTY]", OracleDBUtil.SqlStr(dr["SALERTNQTY"].ToString()));
                        sb.Replace("[TRNINQTY]", OracleDBUtil.SqlStr(dr["TRNINQTY"].ToString()));
                        sb.Replace("[TRNOUTQTY]", OracleDBUtil.SqlStr(dr["TRNOUTQTY"].ToString()));
                        sb.Replace("[STKCHGQTY]", OracleDBUtil.SqlStr(dr["STKCHGQTY"].ToString()));
                        sb.Replace("[STKCHKQTY]", OracleDBUtil.SqlStr(dr["STKCHKQTY"].ToString()));
                        sb.Replace("[STKADJQTY]", OracleDBUtil.SqlStr(dr["STKADJQTY"].ToString()));
                        sb.Replace("[ENDQTY]", OracleDBUtil.SqlStr(dr["ENDQTY"].ToString()));
                        sb.Replace("[UPDDATE]", OracleDBUtil.SqlStr(dr["UPDDATE"].ToString()));
                        sb.Replace("[UPDUSRNO]", OracleDBUtil.SqlStr(dr["UPDUSRNO"].ToString()));
                        sb.Replace("[DEPTGETQTY]", OracleDBUtil.SqlStr(dr["DEPTGETQTY"].ToString()));
                        sb.Replace("[ERPRTN_QTY]", OracleDBUtil.SqlStr(dr["ERPRTN_QTY"].ToString()));
                        
                        OracleDBUtil.ExecuteSql(objTX, sb.ToString());
                    }
                    objTX.Commit();
                    objTX = objConn.BeginTransaction();
                    //////WEB TEMP TABLE > WEB TARGET TABLE
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
                    Console.WriteLine("執行SP_OLD_POS_INV_TRANSFER");
                    OracleDBUtil.ExecuteSql_SP(
                        objTX,
                        "PK_CONVERT.SP_OLD_POS_INV_TRANSFER",
                        op1,
                        op2
                        );

                    objTX.Commit();
                    sMsg = op2.Value.ToString();
                }
                else
                {
                    sMsg = "STOCK(POS)來源無資料";
                }

                //成功資訊
                //con_log.Success("INV_ON_HAND_CURRENT(WEB)，異動筆數" + (dtAdd.Rows.Count + dtUpd.Rows.Count).ToString());
                con_log.Success(sMsg);
                Console.WriteLine(sMsg);
            }
            catch (Exception ex)
            {
                //失敗資訊
                if (objTX != null) objTX.Rollback();
                con_log.Fail(ex.Message);
                Console.WriteLine(ex.Message);
            }
            finally 
            {
                if (objTX != null) objTX.Dispose();
                if (objConn != null)
                {
                    if (objConn.State == ConnectionState.Open) objConn.Close();
                    objConn.Dispose();
                }

                if (objConn_erp != null)
                {
                    if (objConn_erp.State == ConnectionState.Open) objConn_erp.Close();
                    objConn_erp.Dispose();
                }
                OracleConnection.ClearAllPools();
            }
        }

     

        //static void SetNewTable(DataTable dtIn)
        //{
        //    OLD_POS_INV_TRANSFER_Facade cFacade = new OLD_POS_INV_TRANSFER_Facade();
        //    for (int i = 0; i < dtIn.Rows.Count; i++)
        //    {
        //        DataRow dr = dtIn.Rows[i];
        //        string PRODNO = dr["PRODNO"].ToString();
        //        string STORENO = dr["STORENO"].ToString();

        //        if (cFacade.Query_INV_ON_HAND_CURRENT(PRODNO, STORENO) == false)
        //        {
        //            dr["ISNEW"] = "Y";
        //        }
        //    }
        //    dtIn.AcceptChanges();
        //}

        public static DataTable Query_ERP_THISMONTH_STOCK()
        {
            OracleConnection oCon = null;
            try
            {
                //ERP
                oCon = OracleDBUtil.GetERPPOSConnection();

                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                sb.Length = 0;
                sb.Append(@"SELECT PRODNO, STKYYMM, substr(STORENO,2,4) as STORENO, 
                            COMPANYCODE, BEGSTK, ORDQTY, 
                            INQTY, RTNQTY, SALEQTY, 
                            SALERTNQTY, TRNINQTY, TRNOUTQTY, 
                            STKCHGQTY, STKCHKQTY, STKADJQTY, 
                            ENDQTY, UPDDATE, UPDUSRNO, 
                            DEPTGETQTY, ERPRTN_QTY  ");
                sb.Append("FROM STOCK  ");
                sb.Append("WHERE STKYYMM=to_char(sysdate,'YYMM') ");
                //sb.Append("WHERE STKYYMM='1012' ");
                //sb.Append("where rownum <2  ");

                DataTable dt = OracleDBUtil.GetDataSet(oCon, sb.ToString()).Tables[0];
                return dt;
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

        #region 備份20110119
        //static void Main(string[] args)
        //{
        //    //初始化LOG
        //    ConvertLog con_log = new ConvertLog("OLD_POS_INV_TRANSFER");

        //    try
        //    {
        //        //GET OLD POS DATA
        //        DataTable dtOld = GetOLdPosData();

        //        //update isnew status
        //        SetNewTable(dtOld);

        //        OLD_POS_INV_TRANSFER_Facade cFacade = new OLD_POS_INV_TRANSFER_Facade();
        //        Dictionary<string, string> dic = new Dictionary<string, string>();
        //        //< OLD_COLUMN_NAME,NEW_COLUMN_NAME >
        //        dic.Add("PRODNO", "PRODNO");
        //        dic.Add("STORENO", "STORE_NO");
        //        dic.Add("ENDQTY", "ON_HAND_QTY");


        //        OLD_POS_INV_TRANSFER_DTO dsAdd = new OLD_POS_INV_TRANSFER_DTO();
        //        OLD_POS_INV_TRANSFER_DTO.INV_ON_HAND_CURRENTDataTable dtAdd = dsAdd.INV_ON_HAND_CURRENT;
        //        OLD_POS_INV_TRANSFER_DTO dsUpd = new OLD_POS_INV_TRANSFER_DTO();
        //        OLD_POS_INV_TRANSFER_DTO.INV_ON_HAND_CURRENTDataTable dtUpd = dsUpd.INV_ON_HAND_CURRENT;

        //        foreach (DataColumn dc in dtAdd.Columns)
        //        {
        //            dc.AllowDBNull = true;
        //        }

        //        foreach (DataColumn dc in dtUpd.Columns)
        //        {
        //            dc.AllowDBNull = true;
        //        }
        //        string stockid = cFacade.Query_INV_GOODLOCUUID();
        //        //dtAdd
        //        DataRow[] drOlds = dtOld.Select("ISNEW='Y'");
        //        DataTable dtOldNew = dtOld.Clone();
        //        for (int i = 0; i < drOlds.Length; i++)
        //        {
        //            dtOldNew.LoadDataRow(drOlds[i].ItemArray, false);
        //        }
        //        dtOldNew.AcceptChanges();
        //        for (int i = 0; i < dtOldNew.Rows.Count; i++)
        //        {
        //            DataRow drNew = dtOldNew.Rows[i];
        //            //STORE_PORTAL_UPDATE_DTO.STORERow drAdd = dtAdd.NewSTORERow();
        //            //MACHINE_PORTAL_UPDATE_DTO.STORE_TERMINATING_MACHINERow drAdd = dtAdd.NewSTORE_TERMINATING_MACHINERow();
        //            OLD_POS_INV_TRANSFER_DTO.INV_ON_HAND_CURRENTRow drAdd = dtAdd.NewINV_ON_HAND_CURRENTRow();
        //            foreach (KeyValuePair<string, string> pair in dic)
        //            {
        //                drAdd[pair.Value] = drNew[pair.Key];
        //            }
        //            drAdd["STOCK_ID"] = stockid;
        //            drAdd["INV_TYPE"] = "";
        //            drAdd["BOOKED_QTY"] = 0;
        //            drAdd["CREATE_USER"] = "CONVERT";
        //            drAdd["CREATE_DTM"] = DateTime.Now;
        //            drAdd["MODI_USER"] = "CONVERT";
        //            drAdd["MODI_DTM"] = DateTime.Now;
        //            drAdd["INV_ONHAND_CURRENT_ID"] = GuidNo.getUUID();
        //            dtAdd.Rows.Add(drAdd);
        //        }
        //        dtAdd.AcceptChanges();


        //        //dtUpd
        //        drOlds = dtOld.Select("ISNEW='N'");

        //        DataTable dtOldUpd = dtOld.Clone();
        //        for (int i = 0; i < drOlds.Length; i++)
        //        {
        //            dtOldUpd.LoadDataRow(drOlds[i].ItemArray, false);
        //        }
        //        dtOldUpd.AcceptChanges();

        //        //for (int i = 0; i < dtOldUpd.Rows.Count; i++)
        //        //{
        //        //    DataRow drOld = dtOldUpd.Rows[i];
        //        //    //STORE_PORTAL_UPDATE_DTO.STORERow drUpd = dtUpd.NewSTORERow();
        //        //    OLD_POS_INV_TRANSFER_DTO.INV_ON_HAND_CURRENTRow drUpd = dtUpd.NewINV_ON_HAND_CURRENTRow();
        //        //    foreach (KeyValuePair<string, string> pair in dic)
        //        //    {
        //        //        drUpd[pair.Value] = drOld[pair.Key];
        //        //    }
        //        //    drUpd["STOCK_ID"] = stockid;
        //        //    drUpd["MODI_USER"] = "CONVERT";
        //        //    drUpd["MODI_DTM"] = DateTime.Now;
        //        //    //drUpd["INV_ONHAND_CURRENT_ID"] = drOld["INV_ONHAND_CURRENT_ID"];
        //        //    dtUpd.Rows.Add(drUpd);
        //        //}
        //        //dtUpd.AcceptChanges();

        //        //Insert and Update Web POS
        //        //cFacade.UpdateINV_ON_HAND_CURRENT(dtAdd, dtUpd);
        //        cFacade.UpdateINV_ON_HAND_CURRENT(dtAdd, dtOldUpd);
                

        //        //成功資訊
        //        con_log.Success("INV_ON_HAND_CURRENT(WEB)，異動筆數"+(dtAdd.Rows.Count+dtUpd.Rows.Count).ToString());
        //    }
        //    catch (Exception ex)
        //    {
        //        //失敗資訊
        //        con_log.Fail(ex.Message);

        //        throw ex;
        //    }
        //}

        //static DataTable GetOLdPosData()
        //{
        //    DataTable dt = new DataTable();
        //    dt = new OLD_POS_INV_TRANSFER_Facade().Query_ERP_THISMONTH_STOCK();
        //    return dt;

        //}

        //static void SetNewTable(DataTable dtIn)
        //{
        //    OLD_POS_INV_TRANSFER_Facade cFacade = new OLD_POS_INV_TRANSFER_Facade();
        //    for (int i = 0; i < dtIn.Rows.Count; i++)
        //    {
        //        DataRow dr = dtIn.Rows[i];
        //        string PRODNO = dr["PRODNO"].ToString();
        //        string STORENO = dr["STORENO"].ToString();

        //        if (cFacade.Query_INV_ON_HAND_CURRENT(PRODNO, STORENO) == false)
        //        {
        //            dr["ISNEW"] = "Y";
        //        }
        //    }
        //    dtIn.AcceptChanges();
        //}
        #endregion
    }
}
