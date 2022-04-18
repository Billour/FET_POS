using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data;
using System.Data.OracleClient;
using System.Globalization;
using FET.POS.Model.Helper;
using FET.POS.Model.DTO;
using Advtek.Utility;

namespace FET.POS.Model.Facade.FacadeImpl
{
    public class IMEI_Facade
    {
        private DateTime SysDate = DateTime.Now;

        /// <summary>
        /// 查詢商品的IMEI清單
        /// </summary>
        /// <param name="TableName">資料表名稱</param>
        /// <param name="RefId">外來鍵</param>
        /// <param name="PRODNO">商品料號</param>
        /// <returns>DataTable</returns>
        public DataTable getINV_IMEI(string TableName, string RefId, string PRODNO)
        {
            string sqlStr = "";
            switch (TableName)
            {
                case "STORETRANSFER_IMEI":
                    sqlStr = @"SELECT SID,STORETRSF_D_ID,PRODNO,IMEI FROM STORETRANSFER_IMEI WHERE STORETRSF_D_ID = " + OracleDBUtil.SqlStr(RefId)
                                + " AND PRODNO = " + OracleDBUtil.SqlStr(PRODNO);
                    break;
                case "STOCKADJ_D_IMEI":
                    sqlStr = @"SELECT STOCKADJ_D_IMEI_ID AS SID ,IMEI FROM STOCKADJ_D_IMEI WHERE STOCKADJD_ID = " + OracleDBUtil.SqlStr(RefId);
                    break;
                case "SALE_IMEI_LOG":
                    sqlStr = @"SELECT ID AS SID, IMEI FROM SALE_IMEI_LOG WHERE SALE_DETAIL_ID = " + OracleDBUtil.SqlStr(RefId);
                    break;
                case "RTND_IMEI":
                    sqlStr = @"SELECT RTND_IMEI_ID AS SID ,IMEI FROM RTND_IMEI WHERE RTND_UP_ID = " + OracleDBUtil.SqlStr(RefId);
                    break;
                default:
                    sqlStr = @"SELECT INV_APPRO_IMEI_ID SID,PO_OE_NO,PRODNO,IMEI FROM INV_APPRO_IMEI WHERE INV_APPROVAL_D_ID = " + OracleDBUtil.SqlStr(RefId) + " AND PRODNO = " + OracleDBUtil.SqlStr(PRODNO);
                    break;

            }
            OracleConnection objConn = null;
            try
            {
                objConn = OracleDBUtil.GetConnection();
                return OracleDBUtil.GetDataSet(objConn, sqlStr).Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (objConn.State == ConnectionState.Open)
                    objConn.Close();
                objConn.Dispose();
                OracleConnection.ClearAllPools();
            }
        }

        /// <summary>
        /// 查詢可以銷售的IMEI清單
        /// </summary>
        /// <param name="StoreNo">店點</param>
        /// <param name="Location">倉別</param>
        /// <param name="PRODNO">商品料號</param>
        /// <param name="IMEI">IMEI值</param>
        /// <returns>DataTable</returns>
        public DataTable getApprove_AllowIMEI(string TableName, string StoreNo, string Location, string PRODNO, string IMEI)
        {
            string sqlStr = "";

            switch (TableName)
            {
                case "STOCKADJ_D_IMEI":
                    sqlStr = @"SELECT IMEI AS IMEI,IVRCODE,STATUS FROM IMEI WHERE PRODNO = " + OracleDBUtil.SqlStr(PRODNO) + " And IMEI = "
                            + OracleDBUtil.SqlStr(IMEI) + "  ";//And IVRCODE= " + OracleDBUtil.SqlStr(StoreNo); 
                    break;
                case "INV_APPRO_IMEI":
                    sqlStr = @"SELECT IMEI AS IMEI FROM IMEI WHERE IVRCODE = " + OracleDBUtil.SqlStr(StoreNo) + " And LOC = "
                           + OracleDBUtil.SqlStr(Location) + " And PRODNO = " + OracleDBUtil.SqlStr(PRODNO) + " And IMEI = "
                           + OracleDBUtil.SqlStr(IMEI) + " And STATUS = '1' And CHANNEL = 'RETAIL'";
                    break;
                case "STORETRANSFER_IMEI":
                case "SALE_IMEI_LOG":
                case "RTND_IMEI":
                default:
                    sqlStr = @"SELECT IMEI AS IMEI FROM IMEI WHERE IVRCODE = " + OracleDBUtil.SqlStr(StoreNo) + " And LOC = "
                        + OracleDBUtil.SqlStr(Location) + " And PRODNO = " + OracleDBUtil.SqlStr(PRODNO) + " And IMEI = "
                        + OracleDBUtil.SqlStr(IMEI) + " And STATUS = '2' And CHANNEL = 'RETAIL'";
                    break;

            }


            OracleConnection objConn = null;
            DataTable dt = new DataTable();
            try
            {
                objConn = OracleDBUtil.GetConnection();
                dt = OracleDBUtil.GetDataSet(objConn, sqlStr).Tables[0];
                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (objConn.State == ConnectionState.Open)
                    objConn.Close();
                objConn.Dispose();
                OracleConnection.ClearAllPools();
            }
        }

        /// <summary>
        /// INSERT 時檢查資料存在否
        /// </summary>
        /// <param name="TableName"></param>
        /// <param name="SID"></param>
        /// <param name="RefId"></param>
        /// <param name="PRODNO"></param>
        /// <param name="IMEI"></param>
        /// <returns></returns>
        public DataTable CheckINV_IMEI(string TableName, string RefId, string PRODNO, string IMEI)
        {
            string sqlStr = "";
            switch (TableName)
            {
                case "STORETRANSFER_IMEI":
                    sqlStr = @"SELECT IMEI FROM STORETRANSFER_IMEI WHERE STORETRSF_D_ID = " + OracleDBUtil.SqlStr(RefId) + " AND PRODNO = "
                                + OracleDBUtil.SqlStr(PRODNO) + " AND IMEI = " + OracleDBUtil.SqlStr(IMEI);
                    break;

                case "STOCKADJ_D_IMEI":
                    sqlStr = @"SELECT IMEI FROM STOCKADJ_D_IMEI WHERE STOCKADJD_ID = " + OracleDBUtil.SqlStr(RefId) + " AND IMEI = " + OracleDBUtil.SqlStr(IMEI);
                    break;

                case "SALE_IMEI_LOG":
                    sqlStr = @"SELECT IMEI FROM SALE_IMEI_LOG WHERE SALE_STATUS not in ('3','4','5','6') AND IMEI = " + OracleDBUtil.SqlStr(IMEI);
                    break;
                case "RTND_IMEI":
                    sqlStr = @"SELECT IMEI FROM RTND_IMEI WHERE RTND_UP_ID = " + OracleDBUtil.SqlStr(RefId) + " AND IMEI = " + OracleDBUtil.SqlStr(IMEI);
                    break;
                default:
                    sqlStr = @"SELECT IMEI FROM INV_APPRO_IMEI  WHERE  INV_APPROVAL_D_ID = '{0}' AND PRODNO='{1}' AND IMEI = '{2}' ";
                    sqlStr = string.Format(sqlStr, RefId, PRODNO, IMEI);
                    break;

            }

            OracleConnection objConn = null;
            try
            {
                objConn = OracleDBUtil.GetConnection();
                DataTable dt = OracleDBUtil.GetDataSet(objConn, sqlStr).Tables[0];
                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (objConn.State == ConnectionState.Open)
                    objConn.Close();
                objConn.Dispose();
                OracleConnection.ClearAllPools();
            }
        }

        /// <summary>
        /// 刪除時檢查資料存在否
        /// </summary>
        private DataTable CheckINV_IMEI(string TableName, string SID)
        {
            string sqlStr = "";
            switch (TableName)
            {
                case "STORETRANSFER_IMEI":
                    sqlStr = @"SELECT IMEI FROM STORETRANSFER_IMEI WHERE SID = " + OracleDBUtil.SqlStr(SID);
                    break;

                case "STOCKADJ_D_IMEI":
                    sqlStr = @"SELECT IMEI FROM STOCKADJ_D_IMEI WHERE STOCKADJ_D_IMEI_ID = " + OracleDBUtil.SqlStr(SID);
                    break;

                case "SALE_IMEI_LOG":
                    sqlStr = @"SELECT IMEI FROM SALE_IMEI_LOG WHERE ID = " + OracleDBUtil.SqlStr(SID);
                    break;
                case "RTND_IMEI":
                    sqlStr = @"SELECT IMEI FROM RTND_IMEI WHERE RTND_IMEI_ID = " + OracleDBUtil.SqlStr(SID);
                    break;

                default:
                    sqlStr = @"SELECT IMEI FROM INV_APPRO_IMEI  WHERE INV_APPRO_IMEI_ID = " + OracleDBUtil.SqlStr(SID);
                    break;

            }

            OracleConnection objConn = null;
            try
            {
                objConn = OracleDBUtil.GetConnection();
                return OracleDBUtil.GetDataSet(objConn, sqlStr).Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (objConn.State == ConnectionState.Open)
                    objConn.Close();
                objConn.Dispose();
                OracleConnection.ClearAllPools();
            }
        }

        public void DeleteINV_IMEI(string TableName, List<object> keyValues)
        {
            //           DTO.PROD_IMEI_DTO.INV_APPRO_IMEIDataTable dtm = new PROD_IMEI_DTO.INV_APPRO_IMEIDataTable();
            System.Data.DataTable dtm = new DataTable();
            string Sid_Key = "";
            switch (TableName)
            {
                case "STORETRANSFER_IMEI":
                    Sid_Key = "SID";
                    break;
                case "STOCKADJ_D_IMEI":
                    Sid_Key = "STOCKADJ_D_IMEI_ID";
                    break;
                case "SALE_IMEI_LOG":
                    Sid_Key = "ID";
                    break;
                case "INV_APPRO_IMEI":
                    Sid_Key = "INV_APPRO_IMEI_ID";
                    break;
                case "RTND_IMEI":
                    Sid_Key = "RTND_IMEI_ID";
                    break;

                default:
                    Sid_Key = "SID";
                    break;
            }

            OracleConnection objConn = null;
            OracleTransaction objTX = null;
            try
            {
                DataColumn dc = new DataColumn(Sid_Key, typeof(string));
                dtm.Columns.Add(dc);
                dtm.TableName = TableName;
                //foreach (DataColumn dc in dtm.Columns) dc.AllowDBNull = true;
                foreach (string key in keyValues)
                {
                    if (CheckINV_IMEI(TableName, key).Rows.Count > 0)//檢查資料存在才刪除
                    {
                        DataRow dr = dtm.NewRow();
                        // PROD_IMEI_DTO.INV_APPRO_IMEIRow drm = dtm.NewINV_APPRO_IMEIRow();
                        dr[Sid_Key] = key;
                        dtm.Rows.Add(dr);
                    }
                }
                dtm.AcceptChanges();

                objConn = OracleDBUtil.GetConnection();
                objTX = objConn.BeginTransaction();
                OracleDBUtil.DELETEByUUID(objTX, dtm, Sid_Key);
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
                if (objConn.State == ConnectionState.Open)
                    objConn.Close();
                objConn.Dispose();
                OracleConnection.ClearAllPools();
            }
        }

        public int CleanINV_IMEI(string TableName, string RefId)
        {
            OracleConnection objConn = null;
            try
            {
                string sqlStr = "";
                objConn = OracleDBUtil.GetConnection();
                switch (TableName)
                {
                    case "SALE_IMEI_LOG":
                        sqlStr = "Delete From SALE_IMEI_LOG Where SALE_STATUS in ('1','7') AND SALE_DETAIL_ID = " + OracleDBUtil.SqlStr(RefId);
                        break;
                    default:
                        sqlStr = "";
                        break;
                }

                if (sqlStr != "")
                {
                    int ret = OracleDBUtil.ExecuteSql(objConn, sqlStr);
                    return ret;
                }
                else
                {
                    return 0;
                }
            }
            catch //(Exception ex)
            {
                return -1;
            }
            finally
            {
                if (objConn.State == ConnectionState.Open)
                    objConn.Close();
                objConn.Dispose();
                OracleConnection.ClearAllPools();
            }
        }

        public int DeleteINV_IMEI(string TableName, string IMEI)
        {
            OracleConnection objConn = null;
            try
            {
                string sqlStr = "";
                objConn = OracleDBUtil.GetConnection();
                switch (TableName)
                {
                    case "SALE_IMEI_LOG":
                        //判斷是否已經有關聯目前IMEI值的SALE_DETAIL資料，所以不加時間判斷
                        sqlStr = " Select ID From SALE_DETAIL Where ID in (Select SALE_DETAIL_ID as ID From SALE_IMEI_LOG Where IMEI = "
                                    + OracleDBUtil.SqlStr(IMEI) + ")";
                        DataTable dtIMEI = OracleDBUtil.GetDataSet(objConn, sqlStr).Tables[0];
                        if (dtIMEI == null || dtIMEI.Rows.Count == 0)
                            sqlStr = "Delete From SALE_IMEI_LOG Where SALE_STATUS in ('1','7') AND IMEI = " + OracleDBUtil.SqlStr(IMEI) + " And sysdate > CREATE_DTM + interval '1' hour ";
                        else
                            sqlStr = "";
                        break;
                    default:
                        sqlStr = "Delete From " + OracleDBUtil.SqlStr(TableName) + " Where IMIE = " + OracleDBUtil.SqlStr(IMEI);
                        break;
                }

                if (sqlStr != "")
                {
                    int ret = OracleDBUtil.ExecuteSql(objConn, sqlStr);
                    return ret;
                }
                else
                {
                    return 0;
                }
            }
            catch //(Exception ex)
            {
                return -1;
            }
            finally
            {
                if (objConn.State == ConnectionState.Open)
                    objConn.Close();
                objConn.Dispose();
                OracleConnection.ClearAllPools();
            }
        }

        public int DeleteINV_IMEI(string TableName, string IMEI, bool forcedDelete)
        {
            OracleConnection objConn = null;
            try
            {
                string sqlStr = "";
                objConn = OracleDBUtil.GetConnection();
                switch (TableName)
                {
                    case "SALE_IMEI_LOG":
                        //判斷是否已經有關聯目前IMEI值的SALE_DETAIL資料，所以不加時間判斷
                        sqlStr = " Select ID From SALE_DETAIL Where ID in (Select SALE_DETAIL_ID as ID From SALE_IMEI_LOG Where IMEI = "
                                    + OracleDBUtil.SqlStr(IMEI) + ")";
                        DataTable dtIMEI = OracleDBUtil.GetDataSet(objConn, sqlStr).Tables[0];
                        if (dtIMEI == null || dtIMEI.Rows.Count == 0)
                            if (forcedDelete)
                                sqlStr = "Delete From SALE_IMEI_LOG Where SALE_STATUS in ('1','7') AND IMEI = " + OracleDBUtil.SqlStr(IMEI);
                            else
                                sqlStr = "Delete From SALE_IMEI_LOG Where SALE_STATUS in ('1','7') AND IMEI = " + OracleDBUtil.SqlStr(IMEI) 
                                            + " And sysdate > CREATE_DTM + interval '1' hour ";
                        else
                            sqlStr = "";
                        break;
                    default:
                        sqlStr = "Delete From " + OracleDBUtil.SqlStr(TableName) + " Where IMIE = " + OracleDBUtil.SqlStr(IMEI);
                        break;
                }

                if (sqlStr != "")
                {
                    int ret = OracleDBUtil.ExecuteSql(objConn, sqlStr);
                    return ret;
                }
                else
                {
                    return 0;
                }
            }
            catch //(Exception ex)
            {
                return -1;
            }
            finally
            {
                if (objConn.State == ConnectionState.Open)
                    objConn.Close();
                objConn.Dispose();
                OracleConnection.ClearAllPools();
            }
        }

        /// <summary>
        /// 新增IMEI
        /// </summary>
        /// <param name="objTX"></param>
        /// <param name="TableName">欲新增資料的資料表名稱</param>
        /// <param name="RefId">FK_UUID</param>
        /// <param name="PRODNO">商品料號</param>
        /// <param name="IMEI_UUID">資料表的PK_UUID</param>
        /// <param name="IMEI">IMEI值</param>
        /// <param name="MODI_USER">變更人員代號</param>
        /// <param name="POOENO"></param>
        public void InsertINV_IMEI(OracleTransaction objTX, string TableName, string RefId, string PRODNO, string IMEI_UUID, string IMEI, string MODI_USER, string POOENO)
        {
            switch (TableName)
            {
                case "STORETRANSFER_IMEI":
                    INV25_StoreTransfer_DTO.STORETRANSFER_IMEIDataTable dt_STORETRANSFER = new INV25_StoreTransfer_DTO.STORETRANSFER_IMEIDataTable();
                    INV25_StoreTransfer_DTO.STORETRANSFER_IMEIRow dr_STORETRANSFER = dt_STORETRANSFER.NewSTORETRANSFER_IMEIRow();
                    dr_STORETRANSFER.SID = IMEI_UUID;
                    dr_STORETRANSFER.STORETRSF_D_ID = RefId;
                    dr_STORETRANSFER.PRODNO = PRODNO;
                    dr_STORETRANSFER.IMEI = IMEI;
                    dr_STORETRANSFER.CREATE_DTM = SysDate;
                    dr_STORETRANSFER.CREATE_USER = MODI_USER;
                    dr_STORETRANSFER.MODI_USER = MODI_USER;
                    dr_STORETRANSFER.MODI_DTM = SysDate;

                    dt_STORETRANSFER.AddSTORETRANSFER_IMEIRow(dr_STORETRANSFER);
                    dt_STORETRANSFER.AcceptChanges();

                    OracleDBUtil.Insert(objTX, dt_STORETRANSFER);
                    break;

                case "STOCKADJ_D_IMEI":
                    INV18_StockADJ_DTO.STOCKADJ_D_IMEIDataTable dt_STOCKADJ = new INV18_StockADJ_DTO.STOCKADJ_D_IMEIDataTable();
                    INV18_StockADJ_DTO.STOCKADJ_D_IMEIRow dr_STOCKADJ = dt_STOCKADJ.NewSTOCKADJ_D_IMEIRow();
                    dr_STOCKADJ.STOCKADJ_D_IMEI_ID = IMEI_UUID;
                    dr_STOCKADJ.STOCKADJD_ID = RefId;
                    dr_STOCKADJ.IMEI = IMEI;
                    dr_STOCKADJ.CREATE_DTM = SysDate;
                    dr_STOCKADJ.CREATE_USER = MODI_USER;
                    dr_STOCKADJ.MODI_USER = MODI_USER;
                    dr_STOCKADJ.MODI_DTM = SysDate;

                    dt_STOCKADJ.AddSTOCKADJ_D_IMEIRow(dr_STOCKADJ);
                    dt_STOCKADJ.AcceptChanges();

                    OracleDBUtil.Insert(objTX, dt_STOCKADJ);
                    break;

                case "SALE_IMEI_LOG":
                    SAL01_SALE_DTO.SALE_IMEI_LOGDataTable dt_SALE = new SAL01_SALE_DTO.SALE_IMEI_LOGDataTable();
                    SAL01_SALE_DTO.SALE_IMEI_LOGRow dr_SALE = dt_SALE.NewSALE_IMEI_LOGRow();
                    dr_SALE.ID = IMEI_UUID;
                    dr_SALE.IMEI = IMEI;
                    dr_SALE.SALE_DETAIL_ID = RefId;
                    dr_SALE.CREATE_USER = MODI_USER;
                    dr_SALE.CREATE_DTM = SysDate;
                    dr_SALE.MODI_USER = MODI_USER;
                    dr_SALE.MODI_DTM = SysDate;
                    dr_SALE.SALE_STATUS = "1";

                    dt_SALE.AddSALE_IMEI_LOGRow(dr_SALE);
                    dt_SALE.AcceptChanges();

                    OracleDBUtil.Insert(objTX, dt_SALE);
                    break;
                case "RTND_IMEI":
                    INV06_RTNM.RTND_IMEIDataTable dt_RTNM = new INV06_RTNM.RTND_IMEIDataTable();
                    INV06_RTNM.RTND_IMEIRow dr_RTNM = dt_RTNM.NewRTND_IMEIRow();
                    dr_RTNM.RTND_IMEI_ID = IMEI_UUID;
                    dr_RTNM.IMEI = IMEI;
                    dr_RTNM.RTND_UP_ID = RefId;
                    dr_RTNM.CREATE_USER = MODI_USER;
                    dr_RTNM.CREATE_DTM = SysDate;
                    dr_RTNM.MODI_USER = MODI_USER;
                    dr_RTNM.MODI_DTM = SysDate;

                    dt_RTNM.AddRTND_IMEIRow(dr_RTNM);
                    dt_RTNM.AcceptChanges();

                    OracleDBUtil.Insert(objTX, dt_RTNM);
                    break;
                default:
                    PROD_IMEI_DTO.INV_APPRO_IMEIDataTable dt = new PROD_IMEI_DTO.INV_APPRO_IMEIDataTable();
                    PROD_IMEI_DTO.INV_APPRO_IMEIRow dr = dt.NewINV_APPRO_IMEIRow();
                    dr.INV_APPRO_IMEI_ID = IMEI_UUID;
                    dr.IMEI = IMEI;
                    dr.PRODNO = PRODNO;
                    dr.PO_OE_NO = POOENO;
                    dr.CREATE_DTM = SysDate;
                    dr.CREATE_USER = MODI_USER;
                    dr.MODI_DTM = SysDate;
                    dr.CREATE_USER = MODI_USER;
                    dr.INV_APPROVAL_D_ID = RefId;

                    dt.AddINV_APPRO_IMEIRow(dr);
                    dt.AcceptChanges();

                    OracleDBUtil.Insert(objTX, dt);
                    break;

            }

        }

        /// <summary>
        /// 變更IMEI(DELETE + INSERT)
        /// </summary>
        /// <param name="dt">ASPxGridView資料</param>
        /// <param name="TableName">變更的資料表名稱</param>
        /// <param name="RefId">FK_UUID</param>
        /// <param name="PRODNO">商品料號</param>
        /// <param name="MODI_USER">變更人員代號</param>
        /// <param name="POOENO"></param>
        public void Modify_IMEI(DataTable dt, string TableName, string RefId, string PRODNO, string MODI_USER, string POOENO)
        {
            //DELETE + INSERT = UPDATE
            string sqlStr = "";
            switch (TableName)
            {
                case "STORETRANSFER_IMEI":
                    sqlStr = @"DELETE FROM STORETRANSFER_IMEI WHERE STORETRSF_D_ID = " + OracleDBUtil.SqlStr(RefId) + " AND PRODNO = " + OracleDBUtil.SqlStr(PRODNO);
                    break;
                case "STOCKADJ_D_IMEI":
                    sqlStr = @"DELETE FROM STOCKADJ_D_IMEI WHERE STOCKADJD_ID = " + OracleDBUtil.SqlStr(RefId);
                    break;
                case "SALE_IMEI_LOG":
                    sqlStr = @"DELETE FROM SALE_IMEI_LOG WHERE SALE_DETAIL_ID = " + OracleDBUtil.SqlStr(RefId);
                    break;
                case "RTND_IMEI":
                    sqlStr = @"DELETE FROM RTND_IMEI WHERE RTND_UP_ID = " + OracleDBUtil.SqlStr(RefId);
                    break;
                default:
                    sqlStr = @"DELETE FROM INV_APPRO_IMEI WHERE INV_APPROVAL_D_ID = " + OracleDBUtil.SqlStr(RefId) + " AND PRODNO = " + OracleDBUtil.SqlStr(PRODNO);
                    break;
            }

            OracleConnection objConn = null;
            OracleTransaction objTX = null;

            try
            {
                objConn = OracleDBUtil.GetConnection();
                objTX = objConn.BeginTransaction();

                //先刪除資料
                int ret = OracleDBUtil.ExecuteSql(objTX, sqlStr);

                //再新增資料
                foreach (DataRow dr in dt.Rows)
                {
                    InsertINV_IMEI(objTX, TableName, RefId, PRODNO, dr["SID"].ToString(), dr["IMEI"].ToString(), MODI_USER, POOENO);
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
    }

}
