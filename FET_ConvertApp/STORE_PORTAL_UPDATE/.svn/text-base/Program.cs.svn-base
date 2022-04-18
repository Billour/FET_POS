using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.OracleClient;
using Advtek.Utility;
using System.Threading;

namespace STORE_PORTAL_UPDATE
{
    class Program
    {
        static string sShemaName;
        static void Main(string[] args)
        {
            Console.WriteLine("STORE_PORTAL_UPDATE");
            Console.WriteLine("1.初始化LOG");
            //初始化LOG
            ConvertLog con_log = new ConvertLog("STORE_PORTAL_UPDATE");
            sShemaName = "";
            DataTable dtTmp = Query_SPDB_Schema_Name();
            if (dtTmp != null && dtTmp.Rows.Count > 0) sShemaName = dtTmp.Rows[0][0].ToString();
            try
            {
                //Get Schema Name

                //GET OLD POS DATA
                Console.WriteLine("2.查詢OLD POS資料:V_RETAIL_SALESORG_AREA(POS)、V_RETAIL_CONTACT_INFO(POS)、V_RETAIL_SALESPERSON(POS) 與 V_SP2POS_SPECIALGROUP(POS)");
                DataSet dsOld = GetOLdPosData_New();


                //清除暫存檔
                Console.WriteLine("3.清除暫存檔:V_RETAIL_SALESORG_AREA(WEB) 與 V_RETAIL_CONTACT_INFO(WEB)");
                ClearTempTable();

                //暫存檔 > 目的地
                Console.WriteLine("3.OLD POS資料複寫至暫存檔");
                COPY_SALESORG_AREA_CONTACT_INFO(dsOld);

                //呼叫SP同步STORE資料
                Console.WriteLine("4.執行預存程序:PK_CONVERT.SP_STORE_PORTAL_UPDATE");
                string sMsg = PK_CONVERT_SP_STORE_PORTAL_UPDATE();
                //成功資訊
                Console.WriteLine("5.執行結束，寫入LOG");
                con_log.Success(sMsg);
                Thread.Sleep(3000);
            }
            catch (Exception ex)
            {
                //失敗資訊
                con_log.Fail(ex.Message);
                Console.WriteLine("例外產生");
                Console.WriteLine(ex.Message);
                Thread.Sleep(10000);
            }
        }

        static DataSet GetOLdPosData_New()
        {
            DataSet ds = new DataSet();

            DataTable dt;
            dt = Query_OLD_V_RETAIL_SALESORG_AREA2();
            dt.TableName = "V_RETAIL_SALESORG_AREA";
            ds.Tables.Add(dt.Copy());

            dt = Query_OLD_V_RETAIL_CONTACT_INFO2();
            dt.TableName = "V_RETAIL_CONTACT_INFO";
            ds.Tables.Add(dt.Copy());

            dt = Query_OLD_V_RETAIL_SALESPERSON2();
            dt.TableName = "V_RETAIL_SALESPERSON";
            ds.Tables.Add(dt.Copy());

            dt = Query_OLD_V_SP2POS_SPECIALGROUP2();
            dt.TableName = "V_SP2POS_SPECIALGROUP";
            ds.Tables.Add(dt.Copy());

            ds.AcceptChanges();
            return ds;

        }

        public static DataTable Query_OLD_V_RETAIL_SALESORG_AREA2()
        {
            OracleConnection oCon = null;
            try
            {
                //oCon = OracleDBUtil.GetERPPOSConnection();
                oCon = OracleDBUtil.GetSPDBConnection();

                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                sb.Length = 0;
                sb.Append("SELECT * FROM ");
                if (!string.IsNullOrEmpty(sShemaName)) sb.Append(sShemaName + ".");
                sb.Append("V_RETAIL_SALESORG_AREA  ");
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

        public static DataTable Query_OLD_V_RETAIL_CONTACT_INFO2()
        {
            OracleConnection oCon = null;
            try
            {
                //oCon = OracleDBUtil.GetERPPOSConnection();
                oCon = OracleDBUtil.GetSPDBConnection();

                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                sb.Length = 0;
                sb.Append("SELECT * FROM ");
                if (!string.IsNullOrEmpty(sShemaName)) sb.Append(sShemaName + ".");
                sb.Append("V_RETAIL_CONTACT_INFO  ");
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

        public static DataTable Query_OLD_V_RETAIL_SALESPERSON2()
        {
            OracleConnection oCon = null;
            try
            {
                //oCon = OracleDBUtil.GetERPPOSConnection();
                oCon = OracleDBUtil.GetSPDBConnection();

                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                sb.Length = 0;
                sb.Append("SELECT * FROM ");
                if (!string.IsNullOrEmpty(sShemaName)) sb.Append(sShemaName + ".");
                sb.Append("V_RETAIL_SALESPERSON  ");
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

        public static DataTable Query_OLD_V_SP2POS_SPECIALGROUP2()
        {
            OracleConnection oCon = null;
            try
            {
                //oCon = OracleDBUtil.GetERPPOSConnection();
                oCon = OracleDBUtil.GetSPDBConnection();

                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                sb.Length = 0;
                sb.Append("SELECT * FROM ");
                if (!string.IsNullOrEmpty(sShemaName)) sb.Append(sShemaName + ".");
                sb.Append("V_SP2POS_SPECIALGROUP  ");
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

        //清除暫存檔
        public static void ClearTempTable()
        {
            OracleConnection objConn = null;
            OracleTransaction objTX = null;
            try
            {
                objConn = OracleDBUtil.GetConnection();
                objTX = objConn.BeginTransaction();
                StringBuilder sb = new StringBuilder();
                sb.Append("DELETE FROM ");
                sb.Append("V_RETAIL_CONTACT_INFO");
                OracleDBUtil.ExecuteSql(objTX, sb.ToString());
                sb.Length = 0;
                sb.Append("DELETE FROM ");
                sb.Append("V_RETAIL_SALESORG_AREA");
                OracleDBUtil.ExecuteSql(objTX, sb.ToString());
                sb.Length = 0;
                sb.Append("DELETE FROM ");
                sb.Append("V_RETAIL_SALESPERSON");
                OracleDBUtil.ExecuteSql(objTX, sb.ToString());
                sb.Length = 0;
                sb.Append("DELETE FROM ");
                sb.Append("V_SP2POS_SPECIALGROUP");
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
                OracleConnection.ClearAllPools();
            }
        }

        public static void COPY_SALESORG_AREA_CONTACT_INFO(DataSet ds)
        {
            OracleConnection objConn = null;
            OracleTransaction objTX = null;
            StringBuilder sb = new StringBuilder();
            //ERP TABLES -> WEB TEMP TABLES
            try
            {
                objConn = OracleDBUtil.GetConnection();
                objTX = objConn.BeginTransaction();
                DataTable dtTEMP1 = ds.Tables["V_RETAIL_CONTACT_INFO"];
                DataTable dtTEMP2 = ds.Tables["V_RETAIL_SALESORG_AREA"];
                DataTable dtTEMP3 = ds.Tables["V_RETAIL_SALESPERSON"];
                DataTable dtTEMP4 = ds.Tables["V_SP2POS_SPECIALGROUP"];



                //V_RETAIL_CONTACT_INFO(POS) > V_RETAIL_CONTACT_INFO(WEB)
                foreach (DataRow dr in dtTEMP1.Rows)
                {
                    sb.Length = 0;
                    sb.AppendLine(
                        @"Insert into V_RETAIL_CONTACT_INFO
                          ( ID, AREACODE, NUM, 
                            EXTENSION, ISACTIVE, PHONETYPE, 
                            RETAILCODE, CREATEDATE, CREATEUSER)
                          Values
                          ([ID], [AREACODE], [NUM], 
                           [EXTENSION], [ISACTIVE], [PHONETYPE], 
                           [RETAILCODE], [CREATEDATE], [CREATEUSER])"
                    );
                    sb.Replace("[ID]", OracleDBUtil.SqlStr(dr["ID"].ToString()));
                    sb.Replace("[AREACODE]", OracleDBUtil.SqlStr(dr["AREACODE"].ToString()));
                    sb.Replace("[NUM]", OracleDBUtil.SqlStr(dr["NUM"].ToString()));
                    sb.Replace("[EXTENSION]", OracleDBUtil.SqlStr(dr["EXTENSION"].ToString()));
                    sb.Replace("[ISACTIVE]", OracleDBUtil.SqlStr(dr["ISACTIVE"].ToString()));
                    sb.Replace("[PHONETYPE]", OracleDBUtil.SqlStr(dr["PHONETYPE"].ToString()));
                    sb.Replace("[RETAILCODE]", OracleDBUtil.SqlStr(dr["RETAILCODE"].ToString()));
                    //sb.Replace("[CREATEDATE]", OracleDBUtil.SqlStr(dr["CREATEDATE"].ToString()));
                    string sDate = ((DateTime)dr["CREATEDATE"]).ToString("yyyy/MM/dd");
                    sb.Replace("[CREATEDATE]", "to_date('" + sDate + "','YYYY/MM/DD')");
                    //sb.Replace("[UPDATEDATE]", OracleDBUtil.SqlStr(dr["UPDATEDATE"].ToString()));
                    sb.Replace("[CREATEUSER]", OracleDBUtil.SqlStr(dr["CREATEUSER"].ToString()));
                    //sb.Replace("[UPDATEUSER]", OracleDBUtil.SqlStr(dr["UPDATEUSER"].ToString()));
                    OracleDBUtil.ExecuteSql(objTX, sb.ToString());
                }

                //V_RETAIL_SALESORG_AREA(POS) > V_RETAIL_SALESORG_AREA(WEB)
                foreach (DataRow dr in dtTEMP2.Rows)
                {
                    sb.Length = 0;
                    sb.AppendLine(
                        @"Insert into V_RETAIL_SALESORG_AREA
                          ( STOREID, SALESCD, REGIONID, 
                            REGIONNAME, PROJECTCD, STORENAME, 
                            STOREOPENTM, STORECLOSETM, STOREOPENTM_SAT, 
                            STORECLOSETM_SAT, STOREOPENTM_SUN, STORECLOSETM_SUN, 
                            STARTDT, ENDDT, SALESPERSONID, 
                            FURURESALESPERSONID, FURURESTARTDT, FURUREENDDT, 
                            NTACCOUNT, EMAIL, BUSINESSNO, 
                            TAXNO, STORETYPE, COSTCENTER, 
                            SHIPPINGSITE, NONSHIPPINGSITE, IRS_ALLOWED_DEPARTMENT, 
                            IRS_ALLOWED_DATE, IRS_ALLOWED_TYPE, IRS_ALLOWED_NO, 
                            ATTACHMENT_ID, ATTACHMENT_NAME ,
                            POS_RECEIPT_TITLE,POS_TEMPORARY_STOP_STARTDT,POS_TEMPORARY_STOP_ENDDT)
                          Values
                          ( [STOREID], [SALESCD], [REGIONID], 
                            [REGIONNAME], [PROJECTCD], [STORENAME], 
                            [STOREOPENTM], [STORECLOSETM], [STOREOPENTM_SAT], 
                            [STORECLOSETM_SAT], [STOREOPENTM_SUN], [STORECLOSETM_SUN], 
                            [STARTDT], [ENDDT], [SALESPERSONID], 
                            [FURURESALESPERSONID], [FURURESTARTDT], [FURUREENDDT], 
                            [NTACCOUNT], [EMAIL], [BUSINESSNO], 
                            [TAXNO], [STORETYPE], [COSTCENTER], 
                            [SHIPPINGSITE], [NONSHIPPINGSITE], [IRS_ALLOWED_DEPARTMENT], 
                            [IRS_ALLOWED_DATE], [IRS_ALLOWED_TYPE], [IRS_ALLOWED_NO], 
                            [ATTACHMENT_ID], [ATTACHMENT_NAME],
                            [POS_RECEIPT_TITLE],[POS_TEMPORARY_STOP_STARTDT],[POS_TEMPORARY_STOP_ENDDT])"
                    );



                    sb.Replace("[STOREID]", (dr["STOREID"] != DBNull.Value) ? dr["STOREID"].ToString() : "NULL");
                    sb.Replace("[SALESCD]", OracleDBUtil.SqlStr(dr["SALESCD"].ToString()));
                    sb.Replace("[REGIONID]", (dr["REGIONID"] != DBNull.Value) ? dr["REGIONID"].ToString() : "NULL");

                    sb.Replace("[PROJECTCD]", OracleDBUtil.SqlStr(dr["PROJECTCD"].ToString()));
                    sb.Replace("[STORENAME]", OracleDBUtil.SqlStr(dr["STORENAME"].ToString()));
                    sb.Replace("[STOREOPENTM]", (dr["STOREOPENTM"] != DBNull.Value) ? dr["STOREOPENTM"].ToString() : "NULL");
                    sb.Replace("[STORECLOSETM]", (dr["STORECLOSETM"] != DBNull.Value) ? dr["STORECLOSETM"].ToString() : "NULL");



                    string sDate = "";
                    if (dr["STARTDT"] != DBNull.Value)
                    {
                        sDate = ((DateTime)dr["STARTDT"]).ToString("yyyy/MM/dd");
                        sb.Replace("[STARTDT]", "to_date('" + sDate + "','YYYY/MM/DD')");
                    }
                    else
                    {
                        sb.Replace("[STARTDT]", "NULL");
                    }

                    if (dr["ENDDT"] != DBNull.Value)
                    {
                        sDate = ((DateTime)dr["ENDDT"]).ToString("yyyy/MM/dd");
                        sb.Replace("[ENDDT]", "to_date('" + sDate + "','YYYY/MM/DD')");
                    }
                    else
                    {
                        sb.Replace("[ENDDT]", "NULL");
                    }
                    sb.Replace("[SALESPERSONID]", (dr["SALESPERSONID"] != DBNull.Value) ? dr["SALESPERSONID"].ToString() : "NULL");
                    sb.Replace("[FURURESALESPERSONID]", (dr["FURURESALESPERSONID"] != DBNull.Value) ? dr["FURURESALESPERSONID"].ToString() : "NULL");
                    if (dr["FURURESTARTDT"] != DBNull.Value)
                    {
                        sDate = ((DateTime)dr["FURURESTARTDT"]).ToString("yyyy/MM/dd");
                        sb.Replace("[FURURESTARTDT]", "to_date('" + sDate + "','YYYY/MM/DD')");
                    }
                    else
                    {
                        sb.Replace("[FURURESTARTDT]", "NULL");
                    }

                    if (dr["FURUREENDDT"] != DBNull.Value)
                    {
                        sDate = ((DateTime)dr["FURUREENDDT"]).ToString("yyyy/MM/dd");
                        sb.Replace("[FURUREENDDT]", "to_date('" + sDate + "','YYYY/MM/DD')");
                    }
                    else
                    {
                        sb.Replace("[FURUREENDDT]", "NULL");
                    }
                    sb.Replace("[CREATEUSR]", "'CONVERT'");
                    sb.Replace("[UPDATEUSR]", "'CONVERT'");
                    sb.Replace("[CREATEDT]", "to_date('" + DateTime.Now.ToString("yyyy/MM/dd") + "','YYYY/MM/DD')");
                    sb.Replace("[UPDATEDT]", "to_date('" + DateTime.Now.ToString("yyyy/MM/dd") + "','YYYY/MM/DD')");
                    sb.Replace("[NTACCOUNT]", OracleDBUtil.SqlStr(dr["NTACCOUNT"].ToString()));
                    sb.Replace("[EMAIL]", OracleDBUtil.SqlStr(dr["EMAIL"].ToString()));
                    sb.Replace("[STOREOPENTM_SAT]", OracleDBUtil.SqlStr(dr["STOREOPENTM_SAT"].ToString()));
                    sb.Replace("[STORECLOSETM_SAT]", OracleDBUtil.SqlStr(dr["STORECLOSETM_SAT"].ToString()));
                    sb.Replace("[STOREOPENTM_SUN]", OracleDBUtil.SqlStr(dr["STOREOPENTM_SUN"].ToString()));
                    sb.Replace("[STORECLOSETM_SUN]", OracleDBUtil.SqlStr(dr["STORECLOSETM_SUN"].ToString()));
                    sb.Replace("[BUSINESSNO]", OracleDBUtil.SqlStr(dr["BUSINESSNO"].ToString()));
                    sb.Replace("[TAXNO]", OracleDBUtil.SqlStr(dr["TAXNO"].ToString()));
                    sb.Replace("[STORETYPE]", OracleDBUtil.SqlStr(dr["STORETYPE"].ToString()));
                    sb.Replace("[COSTCENTER]", OracleDBUtil.SqlStr(dr["COSTCENTER"].ToString()));
                    sb.Replace("[SHIPPINGSITE]", OracleDBUtil.SqlStr(dr["SHIPPINGSITE"].ToString()));
                    sb.Replace("[NONSHIPPINGSITE]", OracleDBUtil.SqlStr(dr["NONSHIPPINGSITE"].ToString()));
                    sb.Replace("[IRS_ALLOWED_DEPARTMENT]", OracleDBUtil.SqlStr(dr["IRS_ALLOWED_DEPARTMENT"].ToString()));
                    sb.Replace("[IRS_ALLOWED_DATE]", OracleDBUtil.SqlStr(dr["IRS_ALLOWED_DATE"].ToString()));
                    sb.Replace("[IRS_ALLOWED_TYPE]", OracleDBUtil.SqlStr(dr["IRS_ALLOWED_TYPE"].ToString()));
                    sb.Replace("[IRS_ALLOWED_NO]", OracleDBUtil.SqlStr(dr["IRS_ALLOWED_NO"].ToString()));
                    sb.Replace("[REGIONNAME]", OracleDBUtil.SqlStr(dr["REGIONNAME"].ToString()));
                    sb.Replace("[ATTACHMENT_ID]", (dr["ATTACHMENT_ID"] != DBNull.Value) ? dr["ATTACHMENT_ID"].ToString() : "NULL");
                    sb.Replace("[ATTACHMENT_NAME]", OracleDBUtil.SqlStr(dr["ATTACHMENT_NAME"].ToString()));

                    sb.Replace("[POS_RECEIPT_TITLE]", OracleDBUtil.SqlStr(dr["POS_RECEIPT_TITLE"].ToString()));

                    if (dr["POS_TEMPORARY_STOP_STARTDT"] != DBNull.Value)
                    {
                        sDate = ((DateTime)dr["POS_TEMPORARY_STOP_STARTDT"]).ToString("yyyy/MM/dd");
                        sb.Replace("[POS_TEMPORARY_STOP_STARTDT]", "to_date('" + sDate + "','YYYY/MM/DD')");
                    }
                    else
                    {
                        sb.Replace("[POS_TEMPORARY_STOP_STARTDT]", "NULL");
                    }
                    if (dr["POS_TEMPORARY_STOP_ENDDT"] != DBNull.Value)
                    {
                        sDate = ((DateTime)dr["POS_TEMPORARY_STOP_ENDDT"]).ToString("yyyy/MM/dd");
                        sb.Replace("[POS_TEMPORARY_STOP_ENDDT]", "to_date('" + sDate + "','YYYY/MM/DD')");
                    }
                    else
                    {
                        sb.Replace("[POS_TEMPORARY_STOP_ENDDT]", "NULL");
                    }
                    OracleDBUtil.ExecuteSql(objTX, sb.ToString());
                }

                //V_RETAIL_SALESPERSON(POS) > V_RETAIL_SALESPERSON(WEB)
                foreach (DataRow dr in dtTEMP3.Rows)
                {
                    sb.Length = 0;
                    sb.AppendLine(
                        @"Insert into V_RETAIL_SALESPERSON
                          ( SALESCD, POSITIONID, JOBCHINESENM, SALESPERSONID,
                            NAME,STARTDT,ENDDT,EMPLOYEEID)
                          Values
                          ( [SALESCD], [POSITIONID], [JOBCHINESENM], [SALESPERSONID],
                            [NAME],[STARTDT],[ENDDT],[EMPLOYEEID])"
                    );
                    sb.Replace("[SALESCD]", OracleDBUtil.SqlStr(dr["SALESCD"].ToString()));
                    sb.Replace("[POSITIONID]", (dr["POSITIONID"] != DBNull.Value) ? dr["POSITIONID"].ToString() : "NULL");
                    sb.Replace("[JOBCHINESENM]", OracleDBUtil.SqlStr(dr["JOBCHINESENM"].ToString()));
                    sb.Replace("[SALESPERSONID]", (dr["SALESPERSONID"] != DBNull.Value) ? dr["SALESPERSONID"].ToString() : "NULL");
                    sb.Replace("[NAME]", OracleDBUtil.SqlStr(dr["NAME"].ToString()));


                    if (dr["STARTDT"] == null || dr["STARTDT"].ToString() == "")
                    {
                        sb.Replace("[STARTDT]", "NULL");
                    }
                    else
                    {
                        string sDate = ((DateTime)dr["STARTDT"]).ToString("yyyy/MM/dd");
                        sb.Replace("[STARTDT]", "to_date('" + sDate + "','YYYY/MM/DD')");
                    }

                    if (dr["ENDDT"] == null || dr["ENDDT"].ToString() == "")
                    {
                        sb.Replace("[ENDDT]", "NULL");
                    }
                    else
                    {
                        string sDate = ((DateTime)dr["ENDDT"]).ToString("yyyy/MM/dd");
                        sb.Replace("[ENDDT]", "to_date('" + sDate + "','YYYY/MM/DD')");
                    }
                    sb.Replace("[EMPLOYEEID]", OracleDBUtil.SqlStr(dr["EMPLOYEEID"].ToString()));
                    OracleDBUtil.ExecuteSql(objTX, sb.ToString());
                }

                //V_SP2POS_SPECIALGROUP(POS) > V_SP2POS_SPECIALGROUP(WEB)
                foreach (DataRow dr in dtTEMP4.Rows)
                {
                    sb.Length = 0;
                    sb.AppendLine(
                        @"Insert into V_SP2POS_SPECIALGROUP
                          ( SPECIAL_GROUP_ID, BEGIN_IVRCODE, END_IVRCODE, CODECHAR)
                          Values
                          ( [SPECIAL_GROUP_ID], [BEGIN_IVRCODE], [END_IVRCODE], [CODECHAR])"
                    );
                    sb.Replace("[SPECIAL_GROUP_ID]", (dr["SPECIAL_GROUP_ID"] != DBNull.Value) ? dr["SPECIAL_GROUP_ID"].ToString() : "NULL");
                    sb.Replace("[BEGIN_IVRCODE]", OracleDBUtil.SqlStr(dr["BEGIN_IVRCODE"].ToString()));
                    sb.Replace("[END_IVRCODE]", OracleDBUtil.SqlStr(dr["END_IVRCODE"].ToString()));
                    sb.Replace("[CODECHAR]", OracleDBUtil.SqlStr(dr["CODECHAR"].ToString()));
                    OracleDBUtil.ExecuteSql(objTX, sb.ToString());
                }

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

        public static string PK_CONVERT_SP_STORE_PORTAL_UPDATE()
        {
            OracleConnection objConn = null;
            OracleTransaction objTX = null;
            string sRet = "";
            try
            {
                objConn = OracleDBUtil.GetConnection();
                objTX = objConn.BeginTransaction();

                OracleCommand oraCmd = new OracleCommand("PK_CONVERT.SP_STORE_PORTAL_UPDATE");
                oraCmd.CommandType = System.Data.CommandType.StoredProcedure;
                oraCmd.Parameters.Add(new OracleParameter("outCODE", OracleType.VarChar, 2000)).Direction = ParameterDirection.Output;
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

        public static DataTable Query_SPDB_Schema_Name()
        {
            OracleConnection oCon = null;
            try
            {
                oCon = OracleDBUtil.GetConnection();

                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                sb.Length = 0;
                sb.Append("SELECT PARA_VALUE  FROM SYS_PARA WHERE PARA_KEY='SP_DB_SCHEMA'  ");
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

        #region 備份20110120
        //static void Main(string[] args)
        //{
        //    //初始化LOG
        //    ConvertLog con_log = new ConvertLog("STORE_PORTAL_UPDATE");

        //    try
        //    {
        //        STORE_PORTAL_UPDATE_Facade cFacade = new STORE_PORTAL_UPDATE_Facade();
        //        //GET OLD POS DATA
        //        DataSet dsOld = GetOLdPosData_New();

        //        //ERP TABLES -> WEB TEMP TABLES
        //        STORE_PORTAL_UPDATE_DTO dsWeb = new STORE_PORTAL_UPDATE_DTO();
        //        STORE_PORTAL_UPDATE_DTO.V_RETAIL_CONTACT_INFODataTable dtINFO = dsWeb.V_RETAIL_CONTACT_INFO;
        //        STORE_PORTAL_UPDATE_DTO.V_RETAIL_SALESORG_AREADataTable dtAREA = dsWeb.V_RETAIL_SALESORG_AREA;
        //        DataTable dtTEMP=dsOld.Tables["V_RETAIL_CONTACT_INFO"];
        //        for (int i = 0; i < dtTEMP.Rows.Count; i++) 
        //        {
        //            STORE_PORTAL_UPDATE_DTO.V_RETAIL_CONTACT_INFORow drINFO = dtINFO.NewV_RETAIL_CONTACT_INFORow();
        //            drINFO.ItemArray = dtTEMP.Rows[i].ItemArray;
        //        }
        //        dtINFO.AcceptChanges();
        //        dtTEMP = dsOld.Tables["V_RETAIL_SALESORG_AREA"];
        //        for (int i = 0; i < dtTEMP.Rows.Count; i++)
        //        {
        //            STORE_PORTAL_UPDATE_DTO.V_RETAIL_SALESORG_AREARow drAREA = dtAREA.NewV_RETAIL_SALESORG_AREARow();
        //            drAREA.ItemArray = dtTEMP.Rows[i].ItemArray;
        //        }
        //        dtAREA.AcceptChanges();
        //        cFacade.COPY_SALESORG_AREA_CONTACT_INFO(dsWeb);

        //        //呼叫SP同步STORE資料
        //        string sMsg = cFacade.PK_CONVERT_SP_STORE_PORTAL_UPDATE();
        //        //成功資訊
        //        con_log.Success(sMsg);

        //        #region 舊方法
        //        //GET OLD POS DATA
        //        //DataTable dtOld = GetOLdPosData();

        //        ////update isnew status
        //        //SetNewTable(dtOld);

        //        //STORE_PORTAL_UPDATE_Facade cFacade = new STORE_PORTAL_UPDATE_Facade();
        //        //Dictionary<string, string> dic = new Dictionary<string, string>();
        //        ////< OLD_COLUMN_NAME,NEW_COLUMN_NAME >
        //        //dic.Add("STORE_NO", "STORE_NO");
        //        ////dic.Add("SALESCD", "SALESCD");//
        //        //dic.Add("CHNNAL_TYPE", "CHNNAL_TYPE");
        //        //dic.Add("STORE_NAME", "STORENAME");
        //        //dic.Add("ZONE", "ZONE");
        //        //dic.Add("BRANCHNO", "BRANCHNO");
        //        //dic.Add("STORELEVEL", "STORELEVEL");
        //        //dic.Add("UNINO", "UNINO");
        //        //dic.Add("TAXNO", "TAXNO");
        //        //dic.Add("DS_BRANCHNO", "DS_BRANCHNO");
        //        //dic.Add("COSTCENTER", "COSTCENTER");
        //        //dic.Add("CLOSEDATE", "CLOSEDATE");
        //        //dic.Add("STARTDATE", "STARTDATE");
        //        ////dic.Add("COMPANYCODE", "COMPANYCODE");//

        //        ////
        //        //STORE_PORTAL_UPDATE_DTO dsAdd = new STORE_PORTAL_UPDATE_DTO();
        //        //STORE_PORTAL_UPDATE_DTO.STOREDataTable dtAdd = dsAdd.STORE;
        //        //STORE_PORTAL_UPDATE_DTO dsUpd = new STORE_PORTAL_UPDATE_DTO();
        //        //STORE_PORTAL_UPDATE_DTO.STOREDataTable dtUpd = dsUpd.STORE;
        //        //foreach (DataColumn dc in dtAdd.Columns)
        //        //{
        //        //    dc.AllowDBNull = true;
        //        //}

        //        //foreach (DataColumn dc in dtUpd.Columns)
        //        //{
        //        //    dc.AllowDBNull = true;
        //        //}

        //        ////dtAdd
        //        //DataRow[] drOlds = dtOld.Select("ISNEW='Y'");
        //        //DataTable dtOldNew = dtOld.Clone();
        //        //for (int i = 0; i < drOlds.Length; i++)
        //        //{
        //        //    dtOldNew.LoadDataRow(drOlds[i].ItemArray, false);
        //        //}
        //        //dtOldNew.AcceptChanges();
        //        //for (int i = 0; i < dtOldNew.Rows.Count; i++)
        //        //{
        //        //    DataRow drNew = dtOldNew.Rows[i];
        //        //    STORE_PORTAL_UPDATE_DTO.STORERow drAdd = dtAdd.NewSTORERow();
        //        //    foreach (KeyValuePair<string, string> pair in dic)
        //        //    {
        //        //        drAdd[pair.Value] = drNew[pair.Key];
        //        //    }
        //        //    drAdd["CREATE_USER"] = "CONVERT";
        //        //    drAdd["CREATE_DTM"] = DateTime.Now;
        //        //    drAdd["MODI_USER"] = "CONVERT";
        //        //    drAdd["MODI_DTM"] = DateTime.Now;
        //        //    dtAdd.Rows.Add(drAdd);
        //        //}
        //        //dtAdd.AcceptChanges();


        //        ////dtUpd
        //        //drOlds = dtOld.Select("ISNEW='N'");

        //        //DataTable dtOldUpd = dtOld.Clone();
        //        //for (int i = 0; i < drOlds.Length; i++)
        //        //{
        //        //    dtOldUpd.LoadDataRow(drOlds[i].ItemArray, false);
        //        //}
        //        //dtOldUpd.AcceptChanges();

        //        //for (int i = 0; i < dtOldUpd.Rows.Count; i++)
        //        //{
        //        //    DataRow drOld = dtOldUpd.Rows[i];
        //        //    STORE_PORTAL_UPDATE_DTO.STORERow drUpd = dtUpd.NewSTORERow();
        //        //    foreach (KeyValuePair<string, string> pair in dic)
        //        //    {
        //        //        drUpd[pair.Value] = drOld[pair.Key];
        //        //    }
        //        //    dtUpd.Rows.Add(drUpd);
        //        //}
        //        //dtUpd.AcceptChanges();

        //        ////Insert and update Web POS
        //        //cFacade.UpdateAndInsert_Store(dtAdd, dtUpd);
        //        #endregion

        //    }
        //    catch (Exception ex)
        //    {
        //        //失敗資訊
        //        con_log.Fail(ex.Message);
        //        throw ex;
        //    }
        //}

        //static DataSet GetOLdPosData_New()
        //{
        //    DataSet ds = new DataSet();

        //    DataTable dt ;
        //    dt = new STORE_PORTAL_UPDATE_Facade().Query_OLD_V_RETAIL_SALESORG_AREA2();
        //    dt.TableName = "V_RETAIL_SALESORG_AREA";
        //    ds.Tables.Add(dt.Copy());

        //    dt = new STORE_PORTAL_UPDATE_Facade().Query_OLD_V_RETAIL_CONTACT_INFO2();
        //    dt.TableName = "V_RETAIL_CONTACT_INFO";
        //    ds.Tables.Add(dt.Copy());

        //    ds.AcceptChanges();
        //    return ds;

        //}
        #endregion


    }
}
