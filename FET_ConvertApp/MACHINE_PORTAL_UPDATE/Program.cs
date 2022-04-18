using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.OracleClient;
using Advtek.Utility;
using System.Threading;

namespace MACHINE_PORTAL_UPDATE
{
    class Program
    {
        static string sShemaName="";
        static string sMSG = "";
        static void Main(string[] args)
        {
            //初始化LOG
            OutputMsg("1.MACHINE_PORTAL_UPDATE開始");
            OutputMsg("2.初始化LOG");
            ConvertLog con_log = new ConvertLog("MACHINE_PORTALUPDATE");

            try
            {
                //Get SPDB Schema Name
                OutputMsg("3.查詢SPDB_Schema_Name");
                DataTable dtTmp = Query_SPDB_Schema_Name();
                if (dtTmp != null && dtTmp.Rows.Count > 0) sShemaName = dtTmp.Rows[0][0].ToString();

                //GET OLD POS DATA
                OutputMsg("4.查詢SPDB 資料");
                DataTable dtOld = GetOLdPosData();

                //Clear Temp Table
                OutputMsg("5.清除暫存檔(STORE_TERMINATING_MACHINE_TEMP)");
                ClearTempTable(); //***尚未CREATE

                //Insert to Temp Table
                OutputMsg("6.寫入暫存檔(STORE_TERMINATING_MACHINE_TEMP)");
                InsertTempTable(dtOld);

                //Call SP
                OutputMsg("7.執行PK_CONVERT.SP_STORE_TERMINATING_MACHINE");
                string sRet=SP_STORE_TERMINATING_MACHINE(); //***SP尚未CREATE
                OutputMsg(sRet);
                //LOG
                OutputMsg("8.執行結束，寫入LOG");
                con_log.Success(sMSG);

                Thread.Sleep(5000);

                #region 程式備分
                //update isnew status
                //SetNewTable(dtOld);

                //MACHINE_PORTAL_UPDATE_Facade cFacade = new MACHINE_PORTAL_UPDATE_Facade();
                //Dictionary<string, string> dic = new Dictionary<string, string>();
                ////< OLD_COLUMN_NAME,NEW_COLUMN_NAME >
                //dic.Add("STORE_NO", "STORE_NO");
                //dic.Add("HOST_NO", "HOST_NO");
                //dic.Add("IP_ADDRESS", "IP_ADDRESS");


                ////
                ////MACHINE_PORTAL_UPDATE_DTO
                //MACHINE_PORTAL_UPDATE_DTO dsAdd = new MACHINE_PORTAL_UPDATE_DTO();
                //MACHINE_PORTAL_UPDATE_DTO.STORE_TERMINATING_MACHINEDataTable dtAdd = dsAdd.STORE_TERMINATING_MACHINE;
                //MACHINE_PORTAL_UPDATE_DTO dsUpd = new MACHINE_PORTAL_UPDATE_DTO();
                //MACHINE_PORTAL_UPDATE_DTO.STORE_TERMINATING_MACHINEDataTable dtUpd = dsUpd.STORE_TERMINATING_MACHINE;

                //foreach (DataColumn dc in dtAdd.Columns)
                //{
                //    dc.AllowDBNull = true;
                //}

                //foreach (DataColumn dc in dtUpd.Columns)
                //{
                //    dc.AllowDBNull = true;
                //}

                ////dtAdd
                //DataRow[] drOlds = dtOld.Select("ISNEW='Y'");
                //DataTable dtOldNew = dtOld.Clone();
                //for (int i = 0; i < drOlds.Length; i++)
                //{
                //    dtOldNew.LoadDataRow(drOlds[i].ItemArray, false);
                //}
                //dtOldNew.AcceptChanges();
                //for (int i = 0; i < dtOldNew.Rows.Count; i++)
                //{
                //    DataRow drNew = dtOldNew.Rows[i];
                //    //STORE_PORTAL_UPDATE_DTO.STORERow drAdd = dtAdd.NewSTORERow();
                //    MACHINE_PORTAL_UPDATE_DTO.STORE_TERMINATING_MACHINERow drAdd = dtAdd.NewSTORE_TERMINATING_MACHINERow();
                //    foreach (KeyValuePair<string, string> pair in dic)
                //    {
                //        drAdd[pair.Value] = drNew[pair.Key];
                //    }
                //    drAdd["MACHINE_ID"] = GuidNo.getUUID();
                //    drAdd["CREATE_USER"] = "CONVERT";
                //    drAdd["CREATE_DTM"] = DateTime.Now;
                //    drAdd["MODI_USER"] = "CONVERT";
                //    drAdd["MODI_DTM"] = DateTime.Now;
                //    dtAdd.Rows.Add(drAdd);
                //}
                //dtAdd.AcceptChanges();


                ////dtUpd
                //drOlds = dtOld.Select("ISNEW='N'");

                //DataTable dtOldUpd = dtOld.Clone();
                //for (int i = 0; i < drOlds.Length; i++)
                //{
                //    dtOldUpd.LoadDataRow(drOlds[i].ItemArray, false);
                //}
                //dtOldUpd.AcceptChanges();

                //for (int i = 0; i < dtOldUpd.Rows.Count; i++)
                //{
                //    DataRow drOld = dtOldUpd.Rows[i];
                //    //STORE_PORTAL_UPDATE_DTO.STORERow drUpd = dtUpd.NewSTORERow();
                //    MACHINE_PORTAL_UPDATE_DTO.STORE_TERMINATING_MACHINERow drUpd = dtUpd.NewSTORE_TERMINATING_MACHINERow();
                //    foreach (KeyValuePair<string, string> pair in dic)
                //    {
                //        drUpd[pair.Value] = drOld[pair.Key];
                //    }
                //    dtUpd.Rows.Add(drUpd);
                //}
                //dtUpd.AcceptChanges();

                ////Insert and update Web POS
                //cFacade.UpdateAndInsert_Machine(dtAdd, dtUpd);

                ////成功資訊
                //con_log.Success("MACHINE_PORTAL_UPDATE:STORE_TERMINATING_MACHINE(WEB)異動筆數"+(dtAdd.Rows.Count+dtUpd.Rows.Count).ToString());
                #endregion
            }
            catch (Exception ex)
            {
                //失敗資訊
                OutputMsg(ex.Message );
                con_log.Fail(sMSG);
                Thread.Sleep(5000);
            }
        }

        static DataTable GetOLdPosData()
        {
            DataTable dt = new DataTable();
            dt = Query_OLD_V_RETAIL_MACHINE_IP_MAPPING();
            return dt;

        }

        //static void SetNewTable(DataTable dtIn)
        //{
        //    MACHINE_PORTAL_UPDATE_Facade cFacade = new MACHINE_PORTAL_UPDATE_Facade();
        //    for (int i = 0; i < dtIn.Rows.Count; i++)
        //    {
        //        DataRow dr = dtIn.Rows[i];
        //        string store_no = dr["STORE_NO"].ToString();
        //        string HOST_NO = dr["HOST_NO"].ToString();
        //        string IP_ADDRESS = dr["IP_ADDRESS"].ToString();
        //        if (cFacade.Query_TERMINATING_MACHINE(store_no, HOST_NO, IP_ADDRESS) == false)
        //        {
        //            dr["ISNEW"] = "Y";
        //        }
        //    }
        //    dtIn.AcceptChanges();
        //}

        static DataTable Query_OLD_V_RETAIL_MACHINE_IP_MAPPING()
        {
            OracleConnection oCon = null;
            try
            {
                //OLD POS
                //string sCon = OracleDBUtil.GetOldPOSConnectionStringByTNSName();
                //OracleConnection oCon = OracleDBUtil.GetConnectionByConnString(sCon);
                //oCon = OracleDBUtil.GetERPPOSConnection();
                oCon = OracleDBUtil.GetSPDBConnection();

                ////回寫狀態STATUS='T'
                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                sb.Length = 0;
                //sb.Append("SELECT RSA.STOREID AS STORE_NO,  ");
                sb.Append("SELECT RSA.SALESCD AS STORE_NO,  ");
                sb.Append("MIP.MACHINE_NO AS HOST_NO,  ");
                sb.Append("MIP.MACHINE_IP AS IP_ADDRESS  ");
                sb.Append("FROM ");
                if (!string.IsNullOrEmpty(sShemaName)) sb.Append(sShemaName + ".");
                sb.Append("V_RETAIL_SALESORG_AREA RSA , ");
                if (!string.IsNullOrEmpty(sShemaName)) sb.Append(sShemaName + ".");
                sb.Append("V_RETAIL_MACHINE_IP_MAPPING MIP  ");
                sb.Append("WHERE RSA.SALESCD = MIP.RETAILCODE ");

                DataTable dt = OracleDBUtil.GetDataSet(oCon, sb.ToString()).Tables[0];
                return dt;
            }
            catch (Exception ex)
            {
                OutputMsg("4.查詢SPDB 資料，產生例外");
                throw ex;
            }
            finally
            {
                if (oCon.State == ConnectionState.Open) oCon.Close();
                oCon.Dispose();
                OracleConnection.ClearAllPools();
            }
        }

        static DataTable Query_SPDB_Schema_Name()
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
                OutputMsg("3.查詢SPDB_Schema_Name，產生例外");
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
        static void ClearTempTable()
        {
            OracleConnection objConn = null;
            OracleTransaction objTX = null;
            try
            {
                objConn = OracleDBUtil.GetConnection();
                objTX = objConn.BeginTransaction();
                StringBuilder sb = new StringBuilder();
                sb.Append("DELETE FROM STORE_TERMINATING_MACHINE_TEMP"); //***尚未CREATE
                OracleDBUtil.ExecuteSql(objTX, sb.ToString());
        
                objTX.Commit();
            }
            catch (Exception ex)
            {
                objTX.Rollback();
                OutputMsg("5.清除暫存檔(STORE_TERMINATING_MACHINE_TEMP)，產生例外");
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

        //清除暫存檔
        static void InsertTempTable(DataTable dt)
        {
            OracleConnection objConn = null;
            OracleTransaction objTX = null;
            try
            {
                objConn = OracleDBUtil.GetConnection();
                objTX = objConn.BeginTransaction();
                StringBuilder sb = new StringBuilder();
                //sb.Append("DELETE FROM STORE_TERMINATING_MACHIN_TEMP"); //***尚未CREATE
                //OracleDBUtil.ExecuteSql(objTX, sb.ToString());
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        sb.Length = 0;
                        sb.AppendLine(
                            @"Insert into STORE_TERMINATING_MACHINE_TEMP
                              (STORE_NO, HOST_NO, IP_ADDRESS)
                              Values
                              ([STORE_NO], [HOST_NO], [IP_ADDRESS])");

                        sb.Replace("[STORE_NO]", OracleDBUtil.SqlStr(dr["STORE_NO"].ToString()));
                        sb.Replace("[HOST_NO]", OracleDBUtil.SqlStr(dr["HOST_NO"].ToString()));
                        sb.Replace("[IP_ADDRESS]", OracleDBUtil.SqlStr(dr["IP_ADDRESS"].ToString()));

                        OracleDBUtil.ExecuteSql(objTX, sb.ToString());
                    }
                }

                objTX.Commit();
            }
            catch (Exception ex)
            {
                objTX.Rollback();
                OutputMsg("6.寫入暫存檔(STORE_TERMINATING_MACHINE_TEMP)，產生例外");
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

        static string SP_STORE_TERMINATING_MACHINE()
        {
            OracleConnection objConn = null;
            OracleTransaction objTX = null;
            string sRet = "";
            try
            {
                objConn = OracleDBUtil.GetConnection();
                objTX = objConn.BeginTransaction();

                //OracleDBUtil.ExecuteSql_SP(objTX, "SP_COUNT_RECOMMANDED");
                OracleCommand oraCmd = new OracleCommand("PK_CONVERT.SP_STORE_TERMINATING_MACHINE");

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
                OutputMsg("7.執行PK_CONVERT.SP_STORE_TERMINATING_MACHINE，產生例外");
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

        static void OutputMsg(string s)
        {
            Console.WriteLine(s);
            sMSG += s + "\r\n";
        }
    }
}
