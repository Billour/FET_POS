using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.OracleClient;
using Advtek.Utility;
using FET.POS.Model.DTO;

namespace FET.POS.Model.Facade.FacadeImpl
{
    public class OPT18_Facade
    {
        /// <summary>
        /// 取得查詢結果門市店長折扣設定(Master Data)
        /// </summary>
        /// <param name="STORE_NO">門市編號</param>
        /// <param name="STORENAME">門市名稱</param>
        /// <param name="YYYYMM_S">折扣月份起</param>
        /// <param name="YYYYMM_E">折扣月份迄</param>
        /// <returns>查詢結果</returns>
        public DataTable Query_StoreSpecialDisM(string STORE_NO, string STORENAME,
            string YYYYMM_S, string YYYYMM_E)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(" SELECT DIS_M.SSD_ID, DIS_M.STORE_NO, STORE.STORENAME, ");
            sb.Append(" DIS_M.YYMM, DIS_M.DIS_AMT, DIS_M.MODI_USER, EMPLOYEE.EMPNAME, TO_CHAR(DIS_M.MODI_DTM, 'yyyy/mm/dd hh24:mi:ss') MODI_DTM  ");
            sb.Append(" FROM STORE_SPECIAL_DIS_M DIS_M, STORE, EMPLOYEE ");
            sb.Append(" WHERE STORE.STORE_NO = DIS_M.STORE_NO AND DIS_M.MODI_USER = EMPLOYEE.EMPNO AND DIS_M.DEL_FLAG = 'N' ");

            if (!string.IsNullOrEmpty(STORE_NO))
            {
                sb.Append(" AND DIS_M.STORE_NO like " + OracleDBUtil.LikeStr(STORE_NO));
            }

            if (!string.IsNullOrEmpty(STORENAME))
            {
                sb.Append(" AND STORE.STORENAME like " + OracleDBUtil.LikeStr(STORENAME));
            }

            if (!string.IsNullOrEmpty(YYYYMM_S))
            {
                sb.Append(" AND DIS_M.YYMM >= " + OracleDBUtil.SqlStr(YYYYMM_S));
            }

            if (!string.IsNullOrEmpty(YYYYMM_E))
            {
                sb.Append(" AND DIS_M.YYMM <= " + OracleDBUtil.SqlStr(YYYYMM_E));
            }
            sb.Append(" Order by DIS_M.STORE_NO,DIS_M.YYMM ");

            DataTable dt = OracleDBUtil.Query_Data(sb.ToString());
            return dt;
        }

        /// <summary>
        /// 新增門市店長折扣設定(Master Data)
        /// </summary>
        /// <param name="ds">OPT18 DataSet</param>
        public void AddNewOne_StoreSpecialDisM(OPT18_StoreSpecialDis_DTO ds)
        {
             OracleDBUtil.Insert(ds.Tables["STORE_SPECIAL_DIS_M"]);
        }

        /// <summary>
        /// 修改門市店長折扣設定(Master Data)
        /// </summary>
        /// <param name="ds">OPT18 DataSet</param>
        public void UpdateOne_StoreSpecialDisM(OPT18_StoreSpecialDis_DTO ds)
        {
           OracleDBUtil.UPDDATEByUUID(ds.Tables["STORE_SPECIAL_DIS_M"], "SSD_ID");
        }

        /// <summary>
        /// 刪除門市店長折扣設定(Master Data)
        /// </summary>
        /// <param name="ds">OPT18 DataSet</param>
        public void Delete_StoreSpecialDisM(OPT18_StoreSpecialDis_DTO ds)
        {
            OracleConnection objConn = null;
            OracleTransaction objTX = null;

            try
            {
                objConn = OracleDBUtil.GetConnection();
                objTX = objConn.BeginTransaction();

                foreach (OPT18_StoreSpecialDis_DTO.STORE_SPECIAL_DIS_MRow dr in ds.STORE_SPECIAL_DIS_M.Rows)
                {
                    string SSD_ID = dr["SSD_ID"].ToString();
                    string strsql = "UPDATE STORE_SPECIAL_DIS_D SET DEL_FLAG='Y' WHERE SSD_ID = " + OracleDBUtil.SqlStr(SSD_ID);

                    OracleDBUtil.ExecuteSql(objTX, strsql);
                }

                OracleDBUtil.UPDDATEByUUID(objTX, ds.Tables["STORE_SPECIAL_DIS_M"], "SSD_ID");  //將 DEL_FLAS 設為 Y

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
        /// 取得查詢結果門市店長折扣設定(Detail Data)
        /// </summary>
        /// <param name="SSD_ID">特殊折扣設定主檔ID_UUID</param>
        /// <returns>查詢結果</returns>
        public DataTable Query_StoreSpecialDisD(string SSD_ID)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(" SELECT DIS.SSDD_ID, STORE_ROLE.ROLE_ID, STORE_ROLE.ROLE_NAME, ");
            sb.Append(" to_char(DIS.DIS_AMT)DIS_AMT, to_char(DIS.DIS_RATE) DIS_RATE, DIS.DIS_AMT_UBOND ");
            sb.Append(" FROM STORE_SPECIAL_DIS_D DIS, STORE_ROLE");
            sb.Append(" WHERE DIS.ROLE_ID = STORE_ROLE.ROLE_ID");
            sb.Append(" AND DIS.SSD_ID = " + OracleDBUtil.SqlStr(SSD_ID));
            sb.Append(" AND DIS.DEL_FLAG = 'N'");

            DataTable dt = OracleDBUtil.Query_Data(sb.ToString());
            return dt;
        }

        /// <summary>
        /// 新增門市店長折扣設定(Detail Data)
        /// </summary>
        /// <param name="ds">OPT18 DataSet</param>
        public void AddNewOne_StoreSpecialDisD(OPT18_StoreSpecialDis_DTO ds)
        {
            OracleDBUtil.Insert(ds.Tables["STORE_SPECIAL_DIS_D"]);
        }

        /// <summary>
        /// 修改門市店長折扣設定(Detail Data)
        /// </summary>
        /// <param name="ds">OPT18 DataSet</param>
        public void UpdateOne_StoreSpecialDisD(OPT18_StoreSpecialDis_DTO ds)
        {
            OracleDBUtil.UPDDATEByUUID(ds.Tables["STORE_SPECIAL_DIS_D"], "SSDD_ID"); 
        }

        /// <summary>
        /// 刪除門市店長折扣設定(Detail Data)
        /// </summary>
        /// <param name="ds">OPT18 DataSet</param>
        public void Delete_StoreSpecialDisD(OPT18_StoreSpecialDis_DTO ds)
        {
            OracleDBUtil.UPDDATEByUUID(ds.Tables["STORE_SPECIAL_DIS_D"], "SSDD_ID");  //將 DEL_FLAS 設為 Y 
        }

        /// <summary>
        /// 取得查詢結果門市特殊客訴處理折扣設定Excel上傳檔(Temp)
        /// </summary>
        /// <param name="BATCH_NO">上傳批號_UUID</param>
        /// <returns>查詢結果</returns>
        public DataTable Query_StoreSpecialDiscountTemp(string BATCH_NO)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT TEMP.SID, TEMP.STORE_NO, STORE.STORENAME, TEMP.ROLE_ID, TEMP.YYMM, TEMP.DIS_AMT, ");
            sb.Append("TEMP.ROLE_DIS_AMT, TEMP.ROLE_DIS_RATE, TEMP.ROLE_DIS_AMT_UBOND,TEMP.EXCEPTIOB_CAUSE ");
            sb.Append("FROM STORE_SPECIAL_DISCOUNT_TEMP TEMP, STORE ");
            sb.Append("WHERE TEMP.STORE_NO = STORE.STORE_NO(+) ");
            sb.Append("AND TEMP.BATCH_NO = " + OracleDBUtil.SqlStr(BATCH_NO));

            DataTable dt = OracleDBUtil.Query_Data(sb.ToString());
            return dt;
        }

        /// <summary>
        /// 取得查詢結果門市特殊客訴處理折扣設定Excel上傳檔(Temp) 裡 資料是否有異常
        /// ex1. 同一門市+月份+角色 重複
        /// ex2. 同一門市+月份+折扣總額 不一致
        /// </summary>
        /// <param name="BATCH_NO">上傳批號_UUID</param>
        /// <returns></returns>
        private void GetStoreSpecialDiscountAbnormalData(string BATCH_NO)
        {
            OracleConnection objConn = null;

            try
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("SELECT STORE_NO, YYMM, ROLE_ID, COUNT(*) ");
                sb.Append("FROM STORE_SPECIAL_DISCOUNT_TEMP ");
                sb.Append("WHERE BATCH_NO = " + OracleDBUtil.SqlStr(BATCH_NO) + " ");
                sb.Append("GROUP BY STORE_NO, YYMM, ROLE_ID ");
                sb.Append("HAVING COUNT(*) > 1");

                objConn = OracleDBUtil.GetConnection();
                DataTable dt = OracleDBUtil.GetDataSet(objConn, sb.ToString()).Tables[0];

                if (dt.Rows.Count > 0)
                {
                    //匯入的資料中有異常，更新"異常原因"欄位值
                    UpdateStoreSpecialDiscountTemp(dt, BATCH_NO, "匯入的資料中同一門市、月份的角色重複");
                }
                
                //同一門市+月份+折扣總額 資料是否一致
                System.Text.StringBuilder sbA = new System.Text.StringBuilder();
                sbA.Append("SELECT DISTINCT STORE_NO, YYMM, Count(DIS_AMT) ");
                sbA.Append("FROM STORE_SPECIAL_DISCOUNT_TEMP ");
                sbA.Append("WHERE BATCH_NO = " + OracleDBUtil.SqlStr(BATCH_NO) + " ");
                sbA.Append("GROUP BY STORE_NO, YYMM ");
                sbA.Append("HAVING COUNT(*) > 1");

                DataTable dtA = OracleDBUtil.GetDataSet(objConn, sbA.ToString()).Tables[0];

                if (dt.Rows.Count > 0)
                {
                    //匯入的資料中有異常，更新"異常原因"欄位值
                    UpdateStoreSpecialDiscountTemp(dt, BATCH_NO, "匯入的資料中同一門市、月份的折扣總額不一致");
                }
            }
            catch (Exception ex)
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
        /// 匯入的資料中有異常，更新"異常原因"欄位值
        /// </summary>
        /// <param name="dt">資料有異常的Table</param>
        /// <param name="BATCH_NO">上傳批號_UUID</param>
        /// <param name="strError">異常訊息</param>
        private void UpdateStoreSpecialDiscountTemp(DataTable dt, string BATCH_NO, string strError)
        {
            OracleConnection objConn = null;
            OracleTransaction objTX = null;

            try
            {
                objConn = OracleDBUtil.GetConnection();
                objTX = objConn.BeginTransaction();

                foreach (DataRow dr in dt.Rows)
                {
                    System.Text.StringBuilder sb = new System.Text.StringBuilder();
                    sb.Append("UPDATE STORE_SPECIAL_DISCOUNT_TEMP SET STATUS = '1', EXCEPTIOB_CAUSE = EXCEPTIOB_CAUSE || DECODE(nvl(EXCEPTIOB_CAUSE,''),'','',',') || '" + strError + "' ");
                    sb.Append("WHERE STORE_NO = " + OracleDBUtil.SqlStr(dr["STORE_NO"].ToString()) + " ");
                    sb.Append("AND YYMM = " + OracleDBUtil.SqlStr(dr["YYMM"].ToString()) + " ");
                    sb.Append("AND ROLE_ID = " + OracleDBUtil.SqlStr(dr["ROLE_ID"].ToString()) + " ");
                    sb.Append("AND BATCH_NO = " + OracleDBUtil.SqlStr(BATCH_NO) + " ");

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
                objTX = null;

                if (objConn.State == ConnectionState.Open) objConn.Close();
                objConn.Dispose();
                OracleConnection.ClearAllPools();
            }

        }

        /// <summary>
        /// 新增門市特殊客訴處理折扣設定檔
        /// </summary>
        /// <param name="BATCH_NO">上傳批號_UUID</param>
        public void AddNew_StoreSpecialDiscount(string BATCH_NO)
        {
            OracleConnection objConn = null;
            OracleTransaction objTX = null;

            try
            {
                objConn = OracleDBUtil.GetConnection();
                objTX = objConn.BeginTransaction();

                OracleDBUtil.ExecuteSql_SP(
                   objTX
                   , "SP_OPT18_MODIFY_STORE_SPECIAL"
                   , new OracleParameter("inBATCH_NO", BATCH_NO)
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
        /// 新增門市特殊客訴處理折扣設定Excel上傳檔(Temp)
        /// </summary>
        /// <param name="DS">OPT18 DataSet</param>
        /// <param name="BATCH_NO">上傳批號</param>
        public void AddNew_UploadTemp(OPT18_StoreSpecialDis_DTO ds, string BATCH_NO)
        {
            OracleDBUtil.Insert(ds.Tables["UPLOAD_TEMP"]);
            Check_ImportData(BATCH_NO); //欄位檢查
        }

        /// <summary>
        /// 檢查門市特殊客訴處理折扣設定Excel上傳檔資料(Temp)
        /// </summary>
        /// <param name="BATCH_NO">上傳批號</param>
        public void Check_ImportData(string BATCH_NO)
        {
            OracleConnection objConn = null;
            OracleTransaction objTX = null;

            try
            {
                objConn = OracleDBUtil.GetConnection();
                objTX = objConn.BeginTransaction();

                OracleDBUtil.ExecuteSql_SP(
                   objTX
                   , "SP_CHECK_STORESPECIALDISCOUNT"
                   , new OracleParameter("v_BATCHNO", BATCH_NO)
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
    }
}
