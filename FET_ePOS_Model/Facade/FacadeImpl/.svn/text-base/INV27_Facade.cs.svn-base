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
    public class INV27_Facade
    {
        private DateTime SysDate = DateTime.Now;

        /// <summary>
        /// (1)	同一展示期間，商品料號不可重覆，若重覆，系統顯示「商品料號已存在，請重新輸入」。
        /// </summary>
        /// <param name="SEAL_OFF_PROD_ID"></param>
        /// <param name="PRODNO"></param>
        /// <param name="S_DATE"></param>
        /// <param name="E_DATE"></param>
        /// <returns></returns>
        public static DataTable Check_Start_End_PROD(string SEAL_OFF_PROD_ID, string PRODNO, string S_DATE, string E_DATE)
        {
            string sqlStr = @"SELECT * FROM SEAL_OFF_PROD_M 
                                           WHERE SEAL_OFF_PROD_ID <> '{0}' 
                                             AND PRODNO = '{1}' 
                                             AND S_DATE <= TO_DATE('{2}','yyyy/MM/dd') 
                                             AND E_DATE>=TO_DATE('{3}','yyyy/MM/dd')";
            sqlStr = string.Format(sqlStr, SEAL_OFF_PROD_ID, PRODNO, S_DATE, E_DATE);

            return OracleDBUtil.Query_Data(sqlStr);
        }

        /// <summary>
        /// 取得下拉式區域代碼資料
        /// </summary>
        /// <returns></returns>
        public static DataTable getZone()
        {
            string sqlStr = @"  SELECT '' ZONE,'區域' ZONE_NAME FROM DUAL
                                UNION ALL
                                SELECT 'ALL' ZONE,'ALL' ZONE_NAME FROM DUAL
                                UNION ALL
                                SELECT ZONE,ZONE_NAME FROM ZONE";

            return OracleDBUtil.Query_Data(sqlStr);
        }

        /// <summary>
        /// 總部拆封商品設定修改
        /// </summary>
        /// <param name="NewValues"></param>
        /// <param name="MODI_USER"></param>
        public void UpdateTakeOff_MData(System.Collections.Specialized.OrderedDictionary NewValues, object MODI_USER)
        {
            //表頭資料
            INV27_TakeOff_DTO.SEAL_OFF_PROD_MDataTable dtm = new INV27_TakeOff_DTO.SEAL_OFF_PROD_MDataTable();
            dtm.CREATE_USERColumn.AllowDBNull = true;
            dtm.CREATE_DTMColumn.AllowDBNull = true;
            INV27_TakeOff_DTO.SEAL_OFF_PROD_MRow drm = dtm.NewSEAL_OFF_PROD_MRow();
            foreach (DataColumn dc in dtm.Columns) dc.AllowDBNull = true;

            drm.SEAL_OFF_PROD_ID = NewValues["SEAL_OFF_PROD_ID"].ToString();
            drm.SEAL_OFF_DATE = Advtek.Utility.DateUtil.NullDateFormat(NewValues["SEAL_OFF_DATE"]);
            drm.S_DATE = Advtek.Utility.DateUtil.NullDateFormat(NewValues["S_DATE"]);
            drm.E_DATE = Advtek.Utility.DateUtil.NullDateFormat(NewValues["E_DATE"]);
            drm.PRODNO = NewValues["PRODNO"].ToString();
            drm.SEAL_OFF_QTY = Convert.ToDecimal(NewValues["SEAL_OFF_QTY"]);
            drm.DISCOUNT_TYPE = NewValues["DISCOUNT_TYPE"].ToString();
            drm.DISCOUNT_PRICE = Convert.ToDecimal(NewValues["DISCOUNT_PRICE"]);
            drm.UNIT_PRICE = Convert.ToDecimal(NewValues["UNIT_PRICE"]);
            drm.MODI_USER = MODI_USER.ToString();
            drm.MODI_DTM = SysDate;
            dtm.AddSEAL_OFF_PROD_MRow(drm);
            dtm.AcceptChanges();

            OracleDBUtil.UPDDATEByUUID(dtm, "SEAL_OFF_PROD_ID");
        }

        /// <summary>
        /// 更新總部拆封商品門市資料
        /// </summary>
        /// <param name="NewValues"></param>
        /// <param name="MODI_USER"></param>
        public void UpdateTakeOff_DData(System.Collections.Specialized.OrderedDictionary NewValues, object MODI_USER)
        {
            //表身資料
            INV27_TakeOff_DTO.SEAL_OFF_STOREDataTable dtd = new INV27_TakeOff_DTO.SEAL_OFF_STOREDataTable();
            dtd.CREATE_USERColumn.AllowDBNull = true;
            dtd.CREATE_DTMColumn.AllowDBNull = true;
            INV27_TakeOff_DTO.SEAL_OFF_STORERow drd = dtd.NewSEAL_OFF_STORERow();
            foreach (DataColumn dc in dtd.Columns) dc.AllowDBNull = true;
            drd.SEAL_OFF_STORE_ID = NewValues["SEAL_OFF_STORE_ID"].ToString();
            drd.STORE_NO = NewValues["STORE_NO"].ToString();
            drd.MODI_USER = MODI_USER.ToString();
            drd.MODI_DTM = SysDate;
            dtd.AddSEAL_OFF_STORERow(drd);
            dtd.AcceptChanges();

            OracleDBUtil.UPDDATEByUUID(dtd, "SEAL_OFF_STORE_ID");
        }

        private DataTable getSEAL_OFF_PROD_M(string SEAL_OFF_PROD_ID)
        {
            string sqlStr = @"SELECT * FROM  SEAL_OFF_PROD_M WHERE SEAL_OFF_PROD_ID ='{0}'";
            sqlStr = string.Format(sqlStr, SEAL_OFF_PROD_ID);

            return OracleDBUtil.Query_Data(sqlStr);
        }

        /// <summary>
        /// 刪除總部拆封商品設定(刪除表頭資料時也要把表身刪除)
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
                    if (getSEAL_OFF_PROD_M(key).Rows.Count > 0)
                    {  
                        //一次刪除一筆
                        INV27_TakeOff_DTO.SEAL_OFF_PROD_MDataTable dtm = new INV27_TakeOff_DTO.SEAL_OFF_PROD_MDataTable();
                        foreach (DataColumn dc in dtm.Columns) dc.AllowDBNull = true;
                        INV27_TakeOff_DTO.SEAL_OFF_PROD_MRow drm = dtm.NewSEAL_OFF_PROD_MRow();
                        drm.SEAL_OFF_PROD_ID = key;
                        dtm.AddSEAL_OFF_PROD_MRow(drm);
                        dtm.AcceptChanges();
                        DeleteTakeOff_IMEIData(objTX, key);//刪除IMEI
                        DeleteTakeOff_DData(objTX, key);   //刪除表身
                        OracleDBUtil.DELETEByUUID(objTX, dtm, "SEAL_OFF_PROD_ID");  //刪除表頭
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

        private DataTable getSEAL_OFF_STORE(string SEAL_OFF_STORE_ID)
        {
            string sqlStr = "SELECT * FROM  SEAL_OFF_STORE WHERE SEAL_OFF_STORE_ID =" + OracleDBUtil.SqlStr(SEAL_OFF_STORE_ID);

            return OracleDBUtil.Query_Data(sqlStr);
        }

        /// <summary>
        /// 表身刪除：依表身門市資料選取要刪除的資料做刪除
        /// </summary>
        /// <param name="keyValues"></param>
        public void DeleteTakeOff_DData(List<object> keyValues)
        {
            INV27_TakeOff_DTO.SEAL_OFF_STOREDataTable dtd = new INV27_TakeOff_DTO.SEAL_OFF_STOREDataTable();

            foreach (DataColumn dc in dtd.Columns) dc.AllowDBNull = true;
            OracleConnection objConn = null;
            OracleTransaction objTX = null;

            try
            {
                objConn = OracleDBUtil.GetConnection();
                objTX = objConn.BeginTransaction();

                foreach (string key in keyValues)
                {
                    if (getSEAL_OFF_STORE(key).Rows.Count > 0)
                    {
                        dtd.Rows.Clear();
                        INV27_TakeOff_DTO.SEAL_OFF_STORERow drd = dtd.NewSEAL_OFF_STORERow();
                        drd.SEAL_OFF_STORE_ID = key;
                        dtd.AddSEAL_OFF_STORERow(drd);
                        dtd.AcceptChanges();

                        OracleDBUtil.DELETEByUUID(objTX, dtd, "SEAL_OFF_STORE_ID");
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

        public void DeleteTakeOff_DIMEIData(List<object> keyValues)
        {
            INV27_TakeOff_DTO.SEAL_OFF_IMEIDataTable dtd = new INV27_TakeOff_DTO.SEAL_OFF_IMEIDataTable();
            foreach (DataColumn dc in dtd.Columns) dc.AllowDBNull = true;

            OracleConnection objConn = null;
            OracleTransaction objTX = null;
            try
            {
                objConn = OracleDBUtil.GetConnection();
                objTX = objConn.BeginTransaction();

                foreach (string key in keyValues)
                {
                    if (getSEAL_OFF_IMEI(key).Rows.Count > 0)
                    {
                        dtd.Rows.Clear();
                        INV27_TakeOff_DTO.SEAL_OFF_IMEIRow drd = dtd.NewSEAL_OFF_IMEIRow();
                        drd.SEAL_OFF_STORE_ID = key;
                        dtd.AddSEAL_OFF_IMEIRow(drd);
                        dtd.AcceptChanges();

                        OracleDBUtil.DELETEByUUID(objTX, dtd, "SEAL_OFF_STORE_ID");
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

        private DataTable getSEAL_OFF_IMEI(string SEAL_OFF_PROD_ID)
        {
            string sqlStr = "SELECT * FROM  SEAL_OFF_IMEI WHERE SEAL_OFF_PROD_ID =" + OracleDBUtil.SqlStr(SEAL_OFF_PROD_ID);

            return OracleDBUtil.Query_Data(sqlStr);
        }

        /// <summary>
        /// 表身刪除：依表頭資料選擇要刪除的IMEI資料
        /// </summary>
        /// <param name="keyValues"></param>
        public void DeleteTakeOff_IMEIData(OracleTransaction objTX, string SEAL_OFF_PROD_ID)
        {
            INV27_TakeOff_DTO.SEAL_OFF_IMEIDataTable dtdIMEI = new INV27_TakeOff_DTO.SEAL_OFF_IMEIDataTable();
            dtdIMEI.Rows.Clear();
            foreach (DataColumn dc in dtdIMEI.Columns) dc.AllowDBNull = true;
            //表身有可能沒有資料
            if (getSEAL_OFF_IMEI(SEAL_OFF_PROD_ID).Rows.Count > 0)
            {
                INV27_TakeOff_DTO.SEAL_OFF_IMEIRow drd = dtdIMEI.NewSEAL_OFF_IMEIRow();
                drd.SEAL_OFF_PROD_ID = SEAL_OFF_PROD_ID;
                dtdIMEI.AddSEAL_OFF_IMEIRow(drd);
                dtdIMEI.AcceptChanges();
                OracleDBUtil.DELETEByUUID(objTX, dtdIMEI, "SEAL_OFF_PROD_ID");
            }
        }

        private DataTable getSEAL_OFF_STOREIMEI(string SEAL_OFF_STORE_ID)
        {
            string sqlStr = "SELECT * FROM  SEAL_OFF_IMEI WHERE SEAL_OFF_STORE_ID =" + OracleDBUtil.SqlStr(SEAL_OFF_STORE_ID);

            return OracleDBUtil.Query_Data(sqlStr);
        }

        /// <summary>
        /// 表身刪除：表頭刪除時連帶將IMEI資料刪除
        /// </summary>
        /// <param name="keyValues"></param>
        public void DeleteTakeOff_STOREIMEIData(string SEAL_OFF_STORE_ID)
        {
            INV27_TakeOff_DTO.SEAL_OFF_IMEIDataTable dtdIMEI = new INV27_TakeOff_DTO.SEAL_OFF_IMEIDataTable();
            dtdIMEI.Rows.Clear();
            foreach (DataColumn dc in dtdIMEI.Columns) dc.AllowDBNull = true;
            //表身有可能沒有資料
            if (getSEAL_OFF_STOREIMEI(SEAL_OFF_STORE_ID).Rows.Count > 0)
            {
                INV27_TakeOff_DTO.SEAL_OFF_IMEIRow drd = dtdIMEI.NewSEAL_OFF_IMEIRow();
                drd.SEAL_OFF_STORE_ID = SEAL_OFF_STORE_ID;
                dtdIMEI.AddSEAL_OFF_IMEIRow(drd);
                dtdIMEI.AcceptChanges();
                OracleDBUtil.DELETEByUUID(dtdIMEI, "SEAL_OFF_STORE_ID");
            }
        }

        private DataTable getSEAL_OFF_STORE_ALL(string SEAL_OFF_PROD_ID)
        {
            string sqlStr = "SELECT * FROM  SEAL_OFF_STORE WHERE SEAL_OFF_PROD_ID = " + OracleDBUtil.SqlStr(SEAL_OFF_PROD_ID);

            return OracleDBUtil.Query_Data(sqlStr);
        }

        /// <summary>
        /// 表身刪除：表頭刪除時連帶將表身刪除
        /// </summary>
        /// <param name="keyValues"></param>
        public void DeleteTakeOff_DData(OracleTransaction objTX, string SEAL_OFF_PROD_ID)
        {
            INV27_TakeOff_DTO.SEAL_OFF_STOREDataTable dtd = new INV27_TakeOff_DTO.SEAL_OFF_STOREDataTable();
            dtd.Rows.Clear();
            foreach (DataColumn dc in dtd.Columns) dc.AllowDBNull = true;
            //表身有可能沒有資料
            if (getSEAL_OFF_STORE_ALL(SEAL_OFF_PROD_ID).Rows.Count > 0)
            {
                INV27_TakeOff_DTO.SEAL_OFF_STORERow drd = dtd.NewSEAL_OFF_STORERow();
                drd.SEAL_OFF_PROD_ID = SEAL_OFF_PROD_ID;
                dtd.AddSEAL_OFF_STORERow(drd);
                dtd.AcceptChanges();
                OracleDBUtil.DELETEByUUID(objTX, dtd, "SEAL_OFF_PROD_ID");
            }
        }

        /// <summary>
        /// 新增總部拆封商品設定
        /// </summary>
        /// <param name="NewValues"></param>
        /// <param name="MODI_USER"></param>
        public void InsertTakeOff_MData(System.Collections.Specialized.OrderedDictionary NewValues, object MODI_USER)
        {
            string SEAL_OFF_PROD_ID = GuidNo.getUUID();

            //表頭資料
            INV27_TakeOff_DTO.SEAL_OFF_PROD_MDataTable dtm = new INV27_TakeOff_DTO.SEAL_OFF_PROD_MDataTable();
            INV27_TakeOff_DTO.SEAL_OFF_PROD_MRow drm = dtm.NewSEAL_OFF_PROD_MRow();
            decimal test = new decimal();
            test = Convert.ToDecimal(NewValues["DISCOUNT_PRICE"]);
            drm.SEAL_OFF_PROD_ID = SEAL_OFF_PROD_ID;
            drm.SEAL_OFF_DATE = Advtek.Utility.DateUtil.NullDateFormat(NewValues["SEAL_OFF_DATE"]);
            drm.S_DATE = Advtek.Utility.DateUtil.NullDateFormat(NewValues["S_DATE"]);
            drm.E_DATE = Advtek.Utility.DateUtil.NullDateFormat(NewValues["E_DATE"]);
            drm.PRODNO = NewValues["PRODNO"].ToString();
            drm.SEAL_OFF_QTY = Convert.ToDecimal(NewValues["SEAL_OFF_QTY"]);
            drm.DISCOUNT_TYPE = NewValues["DISCOUNT_TYPE"].ToString();
            drm.DISCOUNT_PRICE = Convert.ToDecimal(NewValues["DISCOUNT_PRICE"]);
            drm.UNIT_PRICE = Convert.ToDecimal(GetPrice(NewValues["PRODNO"].ToString()));
            drm.MODI_USER = MODI_USER.ToString();
            drm.MODI_DTM = SysDate;
            drm.CREATE_USER = MODI_USER.ToString();
            drm.CREATE_DTM = SysDate;

            dtm.AddSEAL_OFF_PROD_MRow(drm);
            dtm.AcceptChanges();

            OracleDBUtil.Insert(dtm);
        }

        public string GetPrice(string PRODNO)
        {
            string str = "";

            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT PRICE ");
            sb.Append("FROM   PRODUCT ");
            sb.Append(" WHERE 1=1  ");
            sb.Append(" AND PRODNO = " + OracleDBUtil.SqlStr(PRODNO));

            DataTable dt = OracleDBUtil.Query_Data(sb.ToString());

            str = "0";

            if (dt.Rows.Count == 0)
            {
                str = dt.Rows[0]["PRICE"].ToString();
                if (string.IsNullOrEmpty(str))
                {
                    str = "0";
                }
            }

            return str;
        }
 
        /// <summary>
        /// 新增總部拆封商品門市資料
        /// </summary>
        /// <param name="NewValues"></param>
        /// <param name="MODI_USER"></param>
        public void InsertTakeOff_DData(System.Collections.Specialized.OrderedDictionary NewValues, object MODI_USER)
        {
            //表身資料
            INV27_TakeOff_DTO.SEAL_OFF_STOREDataTable dtm = new INV27_TakeOff_DTO.SEAL_OFF_STOREDataTable();
            INV27_TakeOff_DTO.SEAL_OFF_STORERow drm = dtm.NewSEAL_OFF_STORERow();

            drm.SEAL_OFF_STORE_ID = Advtek.Utility.GuidNo.getUUID().ToString(); //主KEY值
            drm.SEAL_OFF_PROD_ID = NewValues["SEAL_OFF_PROD_ID"].ToString();//表頭主值
            drm.STORE_NO = NewValues["STORE_NO"].ToString();
            drm.MODI_USER = MODI_USER.ToString();
            drm.MODI_DTM = SysDate;
            drm.CREATE_USER = MODI_USER.ToString();
            drm.CREATE_DTM = SysDate;
            dtm.AddSEAL_OFF_STORERow(drm);
            dtm.AcceptChanges();

            OracleDBUtil.Insert(dtm);
        }

        public static DataTable getTakeOffStore(string SEAL_OFF_PROD_ID, string STORE_NO)
        {
            string sqlStr = @"SELECT STORE_NO FROM SEAL_OFF_STORE WHERE SEAL_OFF_PROD_ID = " + OracleDBUtil.SqlStr(SEAL_OFF_PROD_ID);
            if (!string.IsNullOrEmpty(STORE_NO))
            {
                sqlStr += " AND Store_No = " + OracleDBUtil.SqlStr(STORE_NO);
            }

            return OracleDBUtil.Query_Data(sqlStr);
        }

        public static DataTable getTakeOffStore(string SEAL_OFF_PROD_ID, string SEAL_OFF_STORE_ID, string STORE_NO)
        {
            string sqlStr = @"SELECT STORE_NO FROM SEAL_OFF_STORE WHERE SEAL_OFF_PROD_ID = " + OracleDBUtil.SqlStr(SEAL_OFF_PROD_ID);
            if (!string.IsNullOrEmpty(STORE_NO))
            {
                sqlStr += " AND Store_No = " + OracleDBUtil.SqlStr(STORE_NO);
            }
            if (!string.IsNullOrEmpty(SEAL_OFF_STORE_ID))
            {
                sqlStr += " AND SEAL_OFF_STORE_ID = " + OracleDBUtil.SqlStr(SEAL_OFF_STORE_ID);
            }

            return OracleDBUtil.Query_Data(sqlStr);
        }

        /// <summary>
        /// 新增手機門市資料依區域別新增
        /// </summary>
        /// <param name="ZONE"></param>
        /// <param name="MODI_USER"></param>
        public void InsertTakeOff_DData(string SEAL_OFF_PROD_ID, string ZONE, object MODI_USER)
        {
            DataTable StoreDT = new Store_Facade().Query_Store_ByZone(ZONE);
            DataTable TakeOffStore = getTakeOffStore(SEAL_OFF_PROD_ID, "");

            INV27_TakeOff_DTO.SEAL_OFF_STOREDataTable dtd = new INV27_TakeOff_DTO.SEAL_OFF_STOREDataTable();
            foreach (DataRow dr in StoreDT.Rows)
            {
                //判斷資料是否已存在
                DataRow[] drm = TakeOffStore.Select("STORE_NO = '" + dr["STORE_NO"].ToString() + "'");
                if (drm.Length == 0)
                {
                    INV27_TakeOff_DTO.SEAL_OFF_STORERow drd = dtd.NewSEAL_OFF_STORERow();
                    drd.SEAL_OFF_PROD_ID = SEAL_OFF_PROD_ID;//表頭主值
                    drd.SEAL_OFF_STORE_ID = Advtek.Utility.GuidNo.getUUID().ToString(); //主KEY值
                    drd.STORE_NO = dr["STORE_NO"].ToString();
                    drd.CREATE_DTM = SysDate;
                    drd.CREATE_USER = MODI_USER.ToString();
                    drd.MODI_DTM = SysDate;
                    drd.MODI_USER = MODI_USER.ToString();
                    dtd.AddSEAL_OFF_STORERow(drd);
                    dtd.AcceptChanges();
                }
            }

            if (dtd.Rows.Count > 0)
            {
                //表身資料
                OracleDBUtil.Insert(dtd);
            }
        }

        /// <summary>
        /// 總部拆封商品設定查詢
        /// </summary>
        /// <param name="S_DATE"></param>
        /// <param name="E_DATE"></param>
        /// <param name="S_PRODNO"></param>
        /// <param name="E_PRODNO"></param>
        /// <returns></returns>
        public DataTable GetTakeOff_MData(string S_DATE, string E_DATE, string S_PRODNO, string E_PRODNO)
        {
            string sqlStr = @"SELECT S.SEAL_OFF_PROD_ID,S.SEAL_OFF_DATE,S.S_DATE,S.E_DATE,S.PRODNO,P.PRODNAME,
                            S.SEAL_OFF_QTY,S.DISCOUNT_TYPE,S.UNIT_PRICE,S.DISCOUNT_PRICE,S.MODI_USER,EMP.EMPNAME as MODI_USER_NAME,
                            TO_CHAR(S.MODI_DTM, 'yyyy/mm/dd hh24:mi:ss') as MODI_DTM
                            FROM SEAL_OFF_PROD_M S,PRODUCT P, EMPLOYEE EMP
                            WHERE S.PRODNO = P.PRODNO(+) 
                            AND S.MODI_USER = EMP.EMPNO";

            if (!string.IsNullOrEmpty(S_DATE) || !string.IsNullOrEmpty(E_DATE))
            {
                sqlStr += " AND  S.SEAL_OFF_DATE BETWEEN TO_DATE(NVL('" + S_DATE + "','0001/01/01'),'yyyy/MM/dd') AND TO_DATE(NVL('" + E_DATE + "','9999/01/01'),'yyyy/MM/dd') ";
                //  sqlStr += " AND TO_CHAR(S.SEAL_OFF_DATE,'yyyy/MM/dd') BETWEEN NVL('"+S_DATE+"','0001/01/01') AND NVL('"+E_DATE+"','9999/01/01')";
            }

            if (!string.IsNullOrEmpty(S_PRODNO) || !string.IsNullOrEmpty(E_PRODNO))
            {
                sqlStr += " AND P.PRODNO BETWEEN NVL('" + S_PRODNO + "','0000000000000000000') AND NVL('" + E_PRODNO + "','ZZZZZZZZZZZZZZZZZZZZ')";
            }
            sqlStr += " ORDER BY S.SEAL_OFF_DATE";

            return OracleDBUtil.Query_Data(sqlStr);
        }

        /// <summary>
        /// 總部拆封商品設定查詢門市
        /// </summary>
        /// <param name="SEAL_OFF_PROD_ID"></param>
        /// <returns></returns>
        public DataTable GetTakeOff_DData(string SEAL_OFF_PROD_ID)
        {
            string sqlStr = @"SELECT O.SEAL_OFF_PROD_ID,O.SEAL_OFF_STORE_ID,O.STORE_NO,S.ZONE,Z.ZONE_NAME,O.MODI_USER,O.MODI_DTM,S.STORENAME 
                              FROM SEAL_OFF_STORE O ,STORE S  ,ZONE Z
                              WHERE  O.STORE_NO = S.STORE_NO (+) AND S.ZONE = Z.ZONE(+) AND o.SEAL_OFF_PROD_ID = " + OracleDBUtil.SqlStr(SEAL_OFF_PROD_ID);

            return OracleDBUtil.Query_Data(sqlStr);
        }
    }

}
