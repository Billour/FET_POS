using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

using FET.POS.Model.DTO;
using Advtek.Utility;
using System.Data.OracleClient;

namespace FET.POS.Model.Facade.FacadeImpl
{
    public class OPT05_Facade
    {
        /// <summary>
        /// 總部發票設定資料新增
        /// </summary>
        public void InsertHeadQuarterInvoice(OPT05_HqInvoiceAssign_DTO ds)
        {
            OracleConnection objConn = null;
            OracleTransaction objTX = null;

            try
            {
                objConn = OracleDBUtil.GetConnection();
                objTX = objConn.BeginTransaction();

                OPT05_HqInvoiceAssign_DTO.HQ_INVOICE_ASSIGNDataTable dt = ds.HQ_INVOICE_ASSIGN;

                OracleDBUtil.Insert(objTX, dt);

                foreach (OPT05_HqInvoiceAssign_DTO.HQ_INVOICE_ASSIGNRow MasterDr in dt.Rows)
                {
                    if (MasterDr["USE_TYPE"].ToString() == "2")
                    {
                        //取得該門市所有機台
                        StringBuilder strSql = new StringBuilder();
                        strSql.Append(" SELECT * ");
                        strSql.Append(" FROM  STORE_TERMINATING_MACHINE ");
                        strSql.Append(" WHERE STORE_NO=" + OracleDBUtil.SqlStr(MasterDr["STORE_NO"].ToString()));

                        OPT05_HqInvoiceAssign_DTO.STORE_TERMINATING_MACHINEDataTable MachineDt = new OPT05_HqInvoiceAssign_DTO.STORE_TERMINATING_MACHINEDataTable();

                        OracleDataReader odr = OracleDBUtil.GetDataReader(objTX, strSql.ToString());
                        MachineDt.Load(odr);

                        //新增所有機台離線發票設定資料為空值
                        OPT05_HqInvoiceAssign_DTO.STORE_INVOICE_ASSIGNDataTable AssignDt = new OPT05_HqInvoiceAssign_DTO.STORE_INVOICE_ASSIGNDataTable();

                        foreach (OPT05_HqInvoiceAssign_DTO.STORE_TERMINATING_MACHINERow MachineDr in MachineDt.Rows)
                        {
                            OPT05_HqInvoiceAssign_DTO.STORE_INVOICE_ASSIGNRow AssignDr = AssignDt.NewSTORE_INVOICE_ASSIGNRow();

                            AssignDr["MACHINE_ID"] = MachineDr["MACHINE_ID"];
                            AssignDr["ASSIGN_ID"] = MasterDr["ASSIGN_ID"];
                            AssignDr["STORE_NO"] = MasterDr["STORE_NO"];
                            AssignDr["MODI_USER"] = MasterDr["MODI_USER"];
                            AssignDr["MODI_DTM"] = MasterDr["MODI_DTM"];
                            AssignDr["CREATE_USER"] = AssignDr["MODI_USER"];
                            AssignDr["CREATE_DTM"] = AssignDr["MODI_DTM"];

                            AssignDt.AddSTORE_INVOICE_ASSIGNRow(AssignDr);
                        }

                        AssignDt.AcceptChanges();

                        OracleDBUtil.Insert(objTX, AssignDt);
                    }
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
                objTX = null;
                if (objConn.State == ConnectionState.Open) objConn.Close();
                objConn.Dispose();
                OracleConnection.ClearAllPools();
            }
        }

        /// <summary>
        /// 總部發票設定資料匯入(至暫存)
        /// </summary>
        /// <param name="ds"></param>
        public void ImportHeadQuarterInvoice(OPT05_HqInvoiceAssign_DTO ds)
        {
            OracleConnection objConn = null;
            OracleTransaction objTX = null;

            try
            {
                objConn = OracleDBUtil.GetConnection();
                objTX = objConn.BeginTransaction();

                //處理程序
                OPT05_HqInvoiceAssign_DTO.HQ_INVOICE_ASSIGN_TEMPDataTable dt = ds.HQ_INVOICE_ASSIGN_TEMP;

                OracleDBUtil.ExecuteSql(objTX,
                    @" delete INVOICE_NO_TEMP_POOL
                    where BATCH_NO in (
                    select BATCH_NO
                    from HQ_INVOICE_ASSIGN_TEMP
                    where nvl(STATUS,'T')<>'C'  
                    )");

                OracleDBUtil.ExecuteSql(objTX,
                    @"delete HQ_INVOICE_ASSIGN_TEMP
                    where nvl(STATUS,'T')<>'C'");

                OracleDBUtil.Insert(objTX, dt);

                OracleDBUtil.ExecuteSql_SP(
                    objTX
                    , "SP_CHECK_INV_NO_1"
                    , new OracleParameter("P_BATCH_NO", dt.Rows[0]["BATCH_NO"].ToString())
                    );

                objTX.Commit();
            }
            catch (Exception ex)
            {
                objTX.Rollback();
                throw ex;
            }
            finally
            {
                objTX = null;
                if (objConn.State == ConnectionState.Open) objConn.Close();
                objConn.Dispose();
                OracleConnection.ClearAllPools();
            }
        }

        /// <summary>
        /// 總部發票設定資料匯入(至正式)
        /// </summary>
        /// <param name="BATCH_NO"></param>
        public void ImportHeadQuarterInvoice(string BATCH_NO, string UserName)
        {
            OracleConnection objConn = null;
            OracleTransaction objTX = null;

            try
            {
                objConn = OracleDBUtil.GetConnection();
                objTX = objConn.BeginTransaction();

                //處理程序
                OracleDBUtil.ExecuteSql(objTX,
                    @" INSERT INTO HQ_INVOICE_ASSIGN
                    (ASSIGN_ID, USE_TYPE, S_USE_YM, E_USE_YM, LEADER_CODE, INIT_NO
                    , END_NO, SHEET_COUNT, CREATE_USER, CREATE_DTM, MODI_USER, MODI_DTM
                    ,STORE_NO, INVOICE_TYPE_ID,STATUS)
                    (
                        SELECT ASSIGN_ID, USE_TYPE, S_USE_YM, E_USE_YM, LEADER_CODE, INIT_NO
                            , END_NO, SHEET_COUNT, " + OracleDBUtil.SqlStr(UserName) + @", SYSDATE, " + OracleDBUtil.SqlStr(UserName) + @"
                            , SYSDATE, STORE_NO, INVOICE_TYPE_ID,STATUS
                        FROM HQ_INVOICE_ASSIGN_TEMP
                        WHERE BATCH_NO=" + OracleDBUtil.SqlStr(BATCH_NO) + @"
                    ) ");
                

                //匯入完成Delete Temp
                OracleDBUtil.ExecuteSql(objTX,
                    @" DELETE HQ_INVOICE_ASSIGN_TEMP
                       WHERE BATCH_NO=" + OracleDBUtil.SqlStr(BATCH_NO) + @"
                     ");

                objTX.Commit();
            }
            catch (Exception ex)
            {
                objTX.Rollback();
                throw ex;
            }
            finally
            {
                objTX = null;
                if (objConn.State == ConnectionState.Open) objConn.Close();
                objConn.Dispose();
                OracleConnection.ClearAllPools();
            }
        }

        /// <summary>
        /// 總部發票設定資料修改
        /// </summary>
        public void UpdateHeadQuarterInvoiceData(OPT05_HqInvoiceAssign_DTO ds)
        {
            OPT05_HqInvoiceAssign_DTO.HQ_INVOICE_ASSIGNDataTable dt = ds.HQ_INVOICE_ASSIGN;

            OracleDBUtil.UPDDATEByUUID(dt, dt.PrimaryKey[0].ColumnName);
        }

        /// <summary>
        /// 總部發票設定資料刪除
        /// </summary>
        /// <param name="dt"></param>
        public void DeleteHeadQuarterInvoiceData(DataTable dt)
        {
            OracleDBUtil.DELETEByUUID(dt, dt.Columns[0].ColumnName);
        }

        private DataTable getHQ_INVOICE_ASSIGN(string ASSIGN_ID)
        {
            string sqlStr = "SELECT * FROM  HQ_INVOICE_ASSIGN WHERE ASSIGN_ID =" + OracleDBUtil.SqlStr(ASSIGN_ID);

            return OracleDBUtil.Query_Data(sqlStr);
        }

        private DataTable getSTORE_INVOICE_ASSIGN(string ASSIGN_ID)
        {
            string sqlStr = "SELECT * FROM  STORE_INVOICE_ASSIGN WHERE ASSIGN_ID =" + OracleDBUtil.SqlStr(ASSIGN_ID);

            return OracleDBUtil.Query_Data(sqlStr);
        }

        /// <summary>
        /// 刪除總部發票設定(刪除表頭資料時也要把表身刪除)
        /// </summary>
        /// <param name="keyValues"></param>
        public void DeleteTakeOff_MData(List<object> keyValues)
        {
            OracleConnection objConn = null;
            OracleTransaction objTX = null;
            try
            {

                objConn = OracleDBUtil.GetConnection();
                objTX = objConn.BeginTransaction();

                foreach (string key in keyValues)
                {
                    if (getHQ_INVOICE_ASSIGN(key).Rows.Count > 0)
                    {
                        //一次刪除一筆
                        OPT05_HqInvoiceAssign_DTO.HQ_INVOICE_ASSIGNDataTable dtm = new OPT05_HqInvoiceAssign_DTO.HQ_INVOICE_ASSIGNDataTable();
                        //INV27_TakeOff_DTO.SEAL_OFF_PROD_MDataTable dtm = new INV27_TakeOff_DTO.SEAL_OFF_PROD_MDataTable();
                        foreach (DataColumn dc in dtm.Columns) dc.AllowDBNull = true;
                        OPT05_HqInvoiceAssign_DTO.HQ_INVOICE_ASSIGNRow drm = dtm.NewHQ_INVOICE_ASSIGNRow();
                        //INV27_TakeOff_DTO.SEAL_OFF_PROD_MRow drm = dtm.NewSEAL_OFF_PROD_MRow();
                        drm.ASSIGN_ID = key;
                        dtm.AddHQ_INVOICE_ASSIGNRow(drm);
                        dtm.AcceptChanges();
                        //DeleteTakeOff_IMEIData(objTX, key);//刪除IMEI
                        DeleteTakeOff_DData(objTX, key);   //刪除表身
                        OracleDBUtil.DELETEByUUID(objTX, dtm, "ASSIGN_ID");  //刪除表頭
                    }
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
                objTX = null;

                if (objConn.State == ConnectionState.Open) objConn.Close();
                objConn.Dispose();
                OracleConnection.ClearAllPools();
            }
        }

        /// <summary>
        /// 表身刪除：表頭刪除時連帶將表身刪除
        /// </summary>
        /// <param name="keyValues"></param>
        public void DeleteTakeOff_DData(OracleTransaction objTX, string ASSIGN_ID)
        {
            //INV27_TakeOff_DTO.SEAL_OFF_STOREDataTable dtd = new INV27_TakeOff_DTO.SEAL_OFF_STOREDataTable();
            OPT05_HqInvoiceAssign_DTO.STORE_INVOICE_ASSIGNDataTable dtd = new OPT05_HqInvoiceAssign_DTO.STORE_INVOICE_ASSIGNDataTable();
            dtd.Rows.Clear();
            foreach (DataColumn dc in dtd.Columns) dc.AllowDBNull = true;
            //表身有可能沒有資料
            if (getSTORE_INVOICE_ASSIGN(ASSIGN_ID).Rows.Count > 0)
            {
                OPT05_HqInvoiceAssign_DTO.STORE_INVOICE_ASSIGNRow drd = dtd.NewSTORE_INVOICE_ASSIGNRow();
                //INV27_TakeOff_DTO.SEAL_OFF_STORERow drd = dtd.NewSEAL_OFF_STORERow();
                drd.ASSIGN_ID = ASSIGN_ID;
                dtd.AddSTORE_INVOICE_ASSIGNRow(drd);
                //drd.SEAL_OFF_PROD_ID = ASSIGN_ID;
                //dtd.AddSEAL_OFF_STORERow(drd);
                dtd.AcceptChanges();
                OracleDBUtil.DELETEByUUID(objTX, dtd, "ASSIGN_ID");
            }
        }

        /// <summary>
        /// 修改門市離線機台發票設定
        /// </summary>
        /// <param name="dt"></param>
        public void UpdateStoreMachineInvoiceData(DataTable dt)
        {
             OracleConnection objConn = null;
             try
             {
                 objConn = OracleDBUtil.GetConnection();
                 foreach (DataRow dr in dt.Rows)
                 {
                     StringBuilder strSql = new StringBuilder();
                     if (dr["IsModify"].ToString() == "T")
                     {
                         if (string.IsNullOrEmpty(dr["OLDASSIGN_ID"].ToString()))  //表示為新機台，要Insert
                         {
                             strSql.Append(" INSERT INTO STORE_INVOICE_ASSIGN (ASSIGN_ID, MACHINE_ID, STORE_NO, START_NO, END_NO, SHEET_COUNT, MODI_USER, MODI_DTM, CREATE_USER, CREATE_DTM)");
                             strSql.Append(" VALUES(");
                             strSql.Append(" " + OracleDBUtil.SqlStr(dr["ASSIGN_ID"].ToString()));
                             strSql.Append(" ," + OracleDBUtil.SqlStr(dr["MACHINE_ID"].ToString()));
                             strSql.Append(" ," + OracleDBUtil.SqlStr(dr["STORE_NO"].ToString()));
                             strSql.Append(" ," + OracleDBUtil.SqlStr(dr["START_NO"].ToString()));
                             strSql.Append(" ," + OracleDBUtil.SqlStr(dr["END_NO"].ToString()));
                             strSql.Append(" ," + OracleDBUtil.SqlStr(dr["SHEET_COUNT"].ToString()));
                             strSql.Append(" ," + OracleDBUtil.SqlStr(dr["MODI_USER"].ToString()));
                             strSql.Append(" ," + (string.IsNullOrEmpty(dr["MODI_DTM"].ToString()) ? "null" : "sysdate"));
                             strSql.Append(" ," + OracleDBUtil.SqlStr(dr["MODI_USER"].ToString()));
                             strSql.Append(" ,sysdate");
                             strSql.Append(" )");
                         }
                         else
                         {
                             strSql.Append(" UPDATE STORE_INVOICE_ASSIGN ");
                             strSql.Append(" SET START_NO=" + OracleDBUtil.SqlStr(dr["START_NO"].ToString()));
                             strSql.Append(" , END_NO=" + OracleDBUtil.SqlStr(dr["END_NO"].ToString()));
                             strSql.Append(" , SHEET_COUNT=" + OracleDBUtil.SqlStr(dr["SHEET_COUNT"].ToString()));
                             strSql.Append(" , STORE_NO=" + OracleDBUtil.SqlStr(dr["STORE_NO"].ToString()));
                             strSql.Append(" , MODI_USER=" + OracleDBUtil.SqlStr(dr["MODI_USER"].ToString()));
                             strSql.Append(" , MODI_DTM =" + (string.IsNullOrEmpty(dr["MODI_DTM"].ToString()) ? "null" : "sysdate"));
                             strSql.Append(" WHERE MACHINE_ID=" + OracleDBUtil.SqlStr(dr["MACHINE_ID"].ToString()));
                             strSql.Append(" AND ASSIGN_ID=" + OracleDBUtil.SqlStr(dr["ASSIGN_ID"].ToString()));
                         }

                         OracleDBUtil.ExecuteSql(objConn, strSql.ToString());
                     }
                 }
             }
             catch(Exception ex)
             {
                 throw ex;
             }
             finally
             {
                 if (objConn.State == ConnectionState.Open) objConn.Close();
                 objConn.Dispose();
                 OracleConnection.ClearAllPools();
             }
        }

        /// <summary>
        /// 新增總部發票設定
        /// </summary>
        /// <param name="ASSIGN_ID">UUID</param>
        /// <param name="StoreNO">門市編號</param>
        /// <param name="USE_TYPE">用途 1. 連線 2. 離線 3. 手開</param>
        /// <param name="LEADER_CODE">字軌</param>
        /// <param name="S_DATE">所屬年月(起)</param>
        /// <param name="E_DATE">所屬年月(迄)</param>
        /// <param name="INIT_NO">編號(起)</param>
        /// <param name="END_NO">編號(迄)</param>
        /// <returns></returns>
        public static bool CheckInvoiceNO_New(string ASSIGN_ID, string StoreNO, string USE_TYPE,
           string LEADER_CODE, string S_DATE, string E_DATE,
           string INIT_NO, string END_NO
           )
        {
            bool returnValue = false;

            StringBuilder sb = new StringBuilder();
            sb.AppendLine("SELECT check_invoiceno_duplicate_New ");
            sb.AppendLine("( ");
            //sb.AppendLine("AND USE_TYPE = " + OracleDBUtil.SqlStr(USE_TYPE));  --不同USE_TYPE也不能重疊
            sb.AppendLine(OracleDBUtil.SqlStr(LEADER_CODE) + " , ");
            sb.AppendLine(OracleDBUtil.SqlStr(INIT_NO) + " , ");
            sb.AppendLine(OracleDBUtil.SqlStr(END_NO) + " , ");
            sb.AppendLine(OracleDBUtil.SqlStr(S_DATE) + " , ");
            sb.AppendLine(OracleDBUtil.SqlStr(E_DATE) + " ) checknum ");
            sb.AppendLine(" from dual ");

            DataTable dt = OracleDBUtil.Query_Data(sb.ToString());

            foreach (DataRow dr in dt.Rows)
            {
                if (dr["checknum"].ToString() != "0")
                {
                    returnValue = true;
                }
                else
                {
                    returnValue = false;
                }
            }

            return returnValue;
        }

        /// <summary>
        /// 修改總部發票設定
        /// </summary>
        /// <param name="ASSIGN_ID">UUID</param>
        /// <param name="StoreNO">門市編號</param>
        /// <param name="USE_TYPE">用途 1. 連線 2. 離線 3. 手開</param>
        /// <param name="LEADER_CODE">字軌</param>
        /// <param name="S_DATE">所屬年月(起)</param>
        /// <param name="E_DATE">所屬年月(迄)</param>
        /// <param name="INIT_NO">編號(起)</param>
        /// <param name="END_NO">編號(迄)</param>
        /// <returns></returns>
        public static bool CheckInvoiceNO_UPD(string ASSIGN_ID, string StoreNO, string USE_TYPE,
           string LEADER_CODE, string S_DATE, string E_DATE,
           string INIT_NO, string END_NO
           )
        {
            bool returnValue = false;

            StringBuilder sb = new StringBuilder();
            sb.AppendLine("SELECT check_invoiceno_duplicate_UPD ");
            sb.AppendLine("( ");
            //sb.AppendLine("AND USE_TYPE = " + OracleDBUtil.SqlStr(USE_TYPE));  --不同USE_TYPE也不能重疊+
            sb.AppendLine(OracleDBUtil.SqlStr(ASSIGN_ID) + " , ");
            sb.AppendLine(OracleDBUtil.SqlStr(LEADER_CODE) + " , ");
            sb.AppendLine(OracleDBUtil.SqlStr(INIT_NO) + " , ");
            sb.AppendLine(OracleDBUtil.SqlStr(END_NO) + " , ");
            sb.AppendLine(OracleDBUtil.SqlStr(S_DATE) + " , ");
            sb.AppendLine(OracleDBUtil.SqlStr(E_DATE) + " ) checknum ");
            sb.AppendLine(" from dual ");

            DataTable dt = OracleDBUtil.Query_Data(sb.ToString());

            foreach (DataRow dr in dt.Rows)
            {
                if (dr["checknum"].ToString() != "0")
                {
                    returnValue = true;
                }
                else
                {
                    returnValue = false;
                }
            }

            return returnValue;
        }

        /// <summary>
        /// 新增門市離線發票設定
        /// </summary>
        /// <param name="ASSIGN_ID">UUID</param>
        /// <param name="StoreNO">門市編號</param>
        /// <param name="USE_TYPE">用途 1. 連線 2. 離線 3. 手開</param>
        /// <param name="LEADER_CODE">字軌</param>
        /// <param name="S_DATE">所屬年月(起)</param>
        /// <param name="E_DATE">所屬年月(迄)</param>
        /// <param name="INIT_NO">編號(起)</param>
        /// <param name="END_NO">編號(迄)</param>
        /// <returns></returns>
        public static bool CheckStoreInvoiceNO_New(string ASSIGN_ID, string StoreNO, string USE_TYPE,
           string LEADER_CODE, string S_DATE, string E_DATE,
           string INIT_NO, string END_NO
           )
        {
            bool returnValue = false;

            StringBuilder sb = new StringBuilder();
            sb.AppendLine("SELECT check_invoiceno_duplicate_UIN ");
            sb.AppendLine("( ");
            //sb.AppendLine("AND USE_TYPE = " + OracleDBUtil.SqlStr(USE_TYPE));  --不同USE_TYPE也不能重疊
            sb.AppendLine(OracleDBUtil.SqlStr(LEADER_CODE) + " , ");
            sb.AppendLine(OracleDBUtil.SqlStr(INIT_NO) + " , ");
            sb.AppendLine(OracleDBUtil.SqlStr(END_NO) + " , ");
            sb.AppendLine(OracleDBUtil.SqlStr(S_DATE) + " , ");
            sb.AppendLine(OracleDBUtil.SqlStr(E_DATE) + " ) checknum ");
            sb.AppendLine(" from dual ");

            DataTable dt = OracleDBUtil.Query_Data(sb.ToString());

            foreach (DataRow dr in dt.Rows)
            {
                if (dr["checknum"].ToString() != "0")
                {
                    returnValue = true;
                }
                else
                {
                    returnValue = false;
                }
            }

            return returnValue;
        }

        /// <summary>
        /// 修改門市離線發票設定
        /// </summary>
        /// <param name="ASSIGN_ID">UUID</param>
        /// <param name="StoreNO">門市編號</param>
        /// <param name="USE_TYPE">用途 1. 連線 2. 離線 3. 手開</param>
        /// <param name="LEADER_CODE">字軌</param>
        /// <param name="S_DATE">所屬年月(起)</param>
        /// <param name="E_DATE">所屬年月(迄)</param>
        /// <param name="INIT_NO">編號(起)</param>
        /// <param name="END_NO">編號(迄)</param>
        /// <returns></returns>
        public static bool CheckStoreInvoiceNO_UPD(string ASSIGN_ID, string StoreNO, string USE_TYPE,
           string LEADER_CODE, string S_DATE, string E_DATE,
           string INIT_NO, string END_NO
           )
        {
            bool returnValue = false;

            StringBuilder sb = new StringBuilder();
            sb.AppendLine("SELECT check_invoiceno_duplicate_UIU ");
            sb.AppendLine("( ");
            //sb.AppendLine("AND USE_TYPE = " + OracleDBUtil.SqlStr(USE_TYPE));  --不同USE_TYPE也不能重疊+
            sb.AppendLine(OracleDBUtil.SqlStr(ASSIGN_ID) + " , ");
            sb.AppendLine(OracleDBUtil.SqlStr(LEADER_CODE) + " , ");
            sb.AppendLine(OracleDBUtil.SqlStr(INIT_NO) + " , ");
            sb.AppendLine(OracleDBUtil.SqlStr(END_NO) + " , ");
            sb.AppendLine(OracleDBUtil.SqlStr(S_DATE) + " , ");
            sb.AppendLine(OracleDBUtil.SqlStr(E_DATE) + " ) checknum ");
            sb.AppendLine(" from dual ");

            DataTable dt = OracleDBUtil.Query_Data(sb.ToString());

            foreach (DataRow dr in dt.Rows)
            {
                if (dr["checknum"].ToString() != "0")
                {
                    returnValue = true;
                }
                else
                {
                    returnValue = false;
                }
            }

            return returnValue;
        }

        public static bool CheckInvoiceNO(string ASSIGN_ID, string StoreNO, string USE_TYPE,
            string LEADER_CODE, string S_DATE, string E_DATE,
            string INIT_NO, string END_NO
            )
        {
            bool returnValue = false;

            StringBuilder sb = new StringBuilder();
            sb.AppendLine("SELECT ASSIGN_ID FROM HQ_INVOICE_ASSIGN ");
            sb.AppendLine("WHERE ");
            //sb.AppendLine("AND USE_TYPE = " + OracleDBUtil.SqlStr(USE_TYPE));  --不同USE_TYPE也不能重疊
            sb.AppendLine(" LEADER_CODE = " + OracleDBUtil.SqlStr(LEADER_CODE));
            sb.AppendLine("AND S_USE_YM = " + OracleDBUtil.SqlStr(S_DATE));
            sb.AppendLine("AND E_USE_YM = " + OracleDBUtil.SqlStr(E_DATE));
            sb.AppendLine("AND INIT_NO <= " + OracleDBUtil.SqlStr(INIT_NO));
            sb.AppendLine("AND END_NO >= " + OracleDBUtil.SqlStr(END_NO));

            if (!string.IsNullOrEmpty(ASSIGN_ID))
            {
                sb.AppendLine("AND ASSIGN_ID != " + OracleDBUtil.SqlStr(ASSIGN_ID));
            }

            DataTable dt = OracleDBUtil.Query_Data(sb.ToString());

            if (dt.Rows.Count > 0)
            {
                returnValue = true;
            }
            return returnValue;
        }

        /// <summary>
        /// 總部發票設定匯入
        /// </summary>
        /// <param name="StoreNO">門市編號</param>
        /// <param name="USE_TYPE">用途 1. 連線 2. 離線 3. 手開</param>
        /// <param name="LEADER_CODE">字軌</param>
        /// <param name="S_DATE">所屬年月(起)</param>
        /// <param name="E_DATE">所屬年月(迄)</param>
        /// <param name="INIT_NO">編號(起)</param>
        /// <param name="END_NO">編號(迄)</param>
        /// <param name="objConn">Oracle Connection</param>
        /// <returns></returns>
        public static bool CheckInvoiceNO_Import(string StoreNO, string USE_TYPE,
          string LEADER_CODE, string S_DATE, string E_DATE,
          string INIT_NO, string END_NO, OracleConnection objConn,string BATCH_NO,string ASSIGN_ID
          )
        {
            bool returnValue = false;

            //從外部傳Connection進來，關閉Connection的動作也在外部執行

            try
            {
                StringBuilder sb = new StringBuilder();
                sb.AppendLine("SELECT Check_InvoiceNo_Duplicate_1 ");
                sb.AppendLine("( ");
                //sb.AppendLine("AND USE_TYPE = " + OracleDBUtil.SqlStr(USE_TYPE));  --不同USE_TYPE也不能重疊sb.AppendLine(OracleDBUtil.SqlStr(LEADER_CODE) + " , ");
                sb.AppendLine(OracleDBUtil.SqlStr(BATCH_NO) + " , ");
                sb.AppendLine(OracleDBUtil.SqlStr(ASSIGN_ID) + " , ");
                sb.AppendLine(OracleDBUtil.SqlStr(LEADER_CODE) + " , ");
                sb.AppendLine(OracleDBUtil.SqlStr(INIT_NO) + " , ");
                sb.AppendLine(OracleDBUtil.SqlStr(END_NO) + " , ");
                sb.AppendLine(OracleDBUtil.SqlStr(S_DATE) + " , ");
                sb.AppendLine(OracleDBUtil.SqlStr(E_DATE) + " ) checknum ");
                sb.AppendLine(" from dual ");


                objConn = OracleDBUtil.GetConnection();
                DataTable dt = OracleDBUtil.GetDataSet(objConn, sb.ToString()).Tables[0];

                foreach (DataRow dr in dt.Rows)
                {
                    if (dr["checknum"].ToString() != "0")
                    {
                        returnValue = true;
                    }
                    else
                    {
                        returnValue = false;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return returnValue;
        }

        /// <summary>
        /// 新增總部發票設定Excel上傳檔(Temp)
        /// </summary>
        /// <param name="DS">OPT15 DataSet</param>
        /// <param name="BATCH_NO">上傳批號</param>
        public void AddNew_UploadTemp(OPT05_HqInvoiceAssign_DTO ds, string BATCH_NO)
        {
            OracleConnection objConn = null;
            OracleTransaction objTX = null;

            try
            {
                objConn = OracleDBUtil.GetConnection();
                objTX = objConn.BeginTransaction();

                //處理程序
                OracleDBUtil.Insert(objTX, ds.Tables["UPLOAD_TEMP"]);

                //欄位檢查
                OracleDBUtil.ExecuteSql_SP(
                    objTX
                    , "SP_OPT05_CHECK_INV", new OracleParameter("inBATCH_NO", BATCH_NO)
                    );

                objTX.Commit();
            }
            catch (Exception ex)
            {
                objTX.Rollback();
                throw ex;
            }
            finally
            {
                objTX = null;
                if (objConn.State == ConnectionState.Open) objConn.Close();
                objConn.Dispose();
                OracleConnection.ClearAllPools();
            }

        }

        public static bool CheckInvoiceNOIsUsed(string LEADER_CODE, string S_DATE, string E_DATE, string INIT_NO)
        {
            bool returnValue = false;

            StringBuilder sb = new StringBuilder();
            sb.AppendLine("SELECT ASSIGN_ID FROM HQ_INVOICE_ASSIGN ");
            sb.AppendLine("WHERE ");
            sb.AppendLine(" CONVERT_FLAG = 'Y'");
            sb.AppendLine(" AND LEADER_CODE = " + OracleDBUtil.SqlStr(LEADER_CODE));
            sb.AppendLine(" AND S_USE_YM = " + OracleDBUtil.SqlStr(S_DATE));
            sb.AppendLine(" AND E_USE_YM = " + OracleDBUtil.SqlStr(E_DATE));
            sb.AppendLine(" AND INIT_NO = " + OracleDBUtil.SqlStr(INIT_NO));

            DataTable dt = OracleDBUtil.Query_Data(sb.ToString());

            if (dt.Rows.Count > 0)
            {
                returnValue = true;
            }
            else
            {
                sb.Remove(0, sb.Length);
                sb.AppendLine("SELECT * FROM INVOICE_NO_POOL ");
                sb.AppendLine("WHERE ");
                sb.AppendLine(" INVOICE_NO = " + OracleDBUtil.SqlStr(LEADER_CODE) + OracleDBUtil.SqlStr(INIT_NO));
                sb.AppendLine(" AND S_USE_YM = " + OracleDBUtil.SqlStr(S_DATE));
                sb.AppendLine(" AND E_USE_YM = " + OracleDBUtil.SqlStr(E_DATE));
                dt = OracleDBUtil.Query_Data(sb.ToString());
                if (dt.Rows.Count > 0)
                {
                    returnValue = true;
                }
            }
            return returnValue;
        }
    }
}
