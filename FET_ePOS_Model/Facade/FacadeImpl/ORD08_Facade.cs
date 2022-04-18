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
    public class ORD08_Facade
    {
        public DataTable QueryNDSMasterMethodData(string NdsNO)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(@"SELECT NDSM.STATUS
                                 , NDSM.HQ_ORDER_M_ID
                                 , NDSM.HQ_NDS_ORDER_NO
                                 , NDSD.HQ_ORDER_D
                                 , NDSD.PRODNO
                                 , PROD.PRODNAME
                                 , TO_CHAR(NDSD.ATR_QTY) ATR_QTY
                                 , DECODE(NDSD.AUTO_DIS_FLAG,'1','true','0','false','false') AS AUTO_DIS_FLAG
                                 , TO_CHAR(NDSD.DIS_QTY) DIS_QTY
                                 , NDSD.REMARK
                                 , TO_CHAR(NDSM.MODI_DTM,'YYYY/mm/DD hh24:mm:ss') AS MODI_DTM
                                 , NDSM.MODI_USER
                            FROM   HQ_NDS_ORDER_M  NDSM,HQ_NDS_ORDER_D  NDSD,PRODUCT  PROD  
                            WHERE  NDSM.HQ_ORDER_M_ID = NDSD.HQ_ORDER_M_ID(+) AND NDSD.PRODNO = PROD.PRODNO(+) ");

            if (!string.IsNullOrEmpty(NdsNO))
            {
                sb.AppendLine("AND 1 = 1 AND NDSM.HQ_NDS_ORDER_NO = " + OracleDBUtil.SqlStr(NdsNO));
            }
            else
            {
                sb.AppendLine("AND 1 <> 1");
            }

            DataTable dt = OracleDBUtil.Query_Data(sb.ToString());
            return dt;
        }

        public DataTable QueryNDSMasterMethodDataStatus(string key)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(@" SELECT PRODNO
                                    , MODI_DTM
                                    , MODI_USER
                            FROM  HQ_NDS_ORDER_M 
                            WHERE 1=1 
                            AND HQ_NDS_ORDER_M.HQ_NDS_ORDER_NO = " + OracleDBUtil.SqlStr(key));

            DataTable dt = OracleDBUtil.Query_Data(sb.ToString());
            return dt;
        }

        /// <summary>
        /// 取得主配作業門市資料
        /// </summary>
        /// <param name="HQ_NDS_ORDER_NO">主配單號</param>
        /// <param name="Empty">是否要取得空資料表</param>
        /// <returns>DataTable</returns>
        public DataTable QueryNDSDetailMethodData(string HQ_NDS_ORDER_NO, bool Empty)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(@" SELECT S.HQ_ORDER_STORE
                                   , S.LOC_ID
                                   , S.HQ_ORDER_D 
                                   , D.PRODNO 
                                   , S.STORE_NO 
                                   , STORE.STORENAME
                                   , S.ASSIGN_QTY 
                                   , W.WEIGHT 
                            FROM   HQ_NDS_ORDER_M M, HQ_NDS_ORDER_D D, HQ_NDS_ORDER_STORE S, STORE, STORE_WEIGHT_DISTRIBUTE W 
                            WHERE  M.HQ_ORDER_M_ID = D.HQ_ORDER_M_ID(+) 
                            AND    D.HQ_ORDER_D = S.HQ_ORDER_D(+) AND D.PRODNO = S.PRODNO(+) 
                            AND    S.STORE_NO = STORE.STORE_NO(+)  
                            AND    S.STORE_NO = W.STORE_NO(+) 
                        ");

            sb.AppendLine((!Empty ? "AND 1=1 " : "AND 1<>1 "));

            if (!string.IsNullOrEmpty(HQ_NDS_ORDER_NO))
            {
                sb.AppendLine("   AND M.HQ_NDS_ORDER_NO = " + OracleDBUtil.SqlStr(HQ_NDS_ORDER_NO));
            }

            DataTable dt = OracleDBUtil.Query_Data(sb.ToString());
            return dt;
        }

        /// <summary>
        /// Non-DropShipment主配作業Excel上傳檔(Temp)
        /// </summary>
        /// <param name="DS">ORD08 DataSet</param>
        public void AddNew_UploadTemp(ORD08_HQNDSORDERMSet_DTO ds)
        {
            OracleDBUtil.Insert(ds.Tables["UPLOAD_TEMP"]);
        }

        /// <summary>
        /// Non-DropShipment主配作業Excel上傳檔查詢(Temp)
        /// </summary>
        /// <param name="BATCH_NO">上傳批號</param>
        /// <returns>查詢結果</returns>
        public DataTable Query_UploadTemp(string BATCH_NO)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(@"  SELECT SID
                                , F1 AS STORENO
                                , S.STORENAME
                                , F2 AS PRODNO
                                , P.PRODNAME
                                , F3 AS ASSIGN_QTY
                                , F4 AS EXCEPTIOB_CAUSE 
                            FROM UPLOAD_TEMP M, STORE S, PRODUCT P  
                            WHERE FINC_ID = 'ORD08_IMPORT' AND M.F1 = S.STORE_NO(+) AND M.F2 = P.PRODNO(+)                            
                        ");

            if (!string.IsNullOrEmpty((string)BATCH_NO))
            {
                sb.AppendLine(" AND BATCH_NO = " + OracleDBUtil.SqlStr(BATCH_NO));
            }
            sb.AppendLine("  order by 2  ");
            DataTable dt = OracleDBUtil.Query_Data(sb.ToString());
            return dt;
        }

        /// <summary>
        /// 查詢Non-DropShipment主配作業的匯出資料
        /// </summary>
        /// <param name="HQ_NDS_ORDER_NO">主配單號</param>
        /// <returns>查詢結果</returns>
        public DataTable Export_HQNDSOrder(string HQ_NDS_ORDER_NO)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(@" SELECT STORE_NO AS 門市編號
                                , STORENAME AS 門市名稱
                                , PRODNO AS 商品料號
                                , PRODNAME AS 商品名稱
                                , ASSIGN_QTY AS 主配量
                            FROM VW_ORD08_EXPORT 
                            WHERE HQ_NDS_ORDER_NO = " + OracleDBUtil.SqlStr(HQ_NDS_ORDER_NO)
                         );

            DataTable dt = OracleDBUtil.Query_Data(sb.ToString());
            return dt;
        }

        /// <summary>
        /// 新增NDS主配作業
        /// </summary>
        /// <param name="ds">ORD08 DataSet</param>
        public void AddNew_NDSMMethodData(OracleTransaction objTX, ORD08_HQNDSORDERMSet_DTO ds, string STATUS)
        {
            OracleDBUtil.Insert(objTX, ds.Tables["HQ_NDS_ORDER_M"]);
            OracleDBUtil.Insert(objTX, ds.Tables["HQ_NDS_ORDER_D"]);
            OracleDBUtil.Insert(objTX, ds.Tables["HQ_NDS_ORDER_STORE"]);
            //20110418新加按[上傳確認]時才壓NDS_BOOK_QTY
            if (ds.HQ_NDS_ORDER_D != null && STATUS == "2")
            {
                foreach (ORD08_HQNDSORDERMSet_DTO.HQ_NDS_ORDER_DRow dr in ds.HQ_NDS_ORDER_D.Rows)
                {
                    string strsql = "UPDATE POS_ATR SET NDS_BOOK_QTY = NVL(NDS_BOOK_QTY,0) + " + dr["DIS_QTY"] + " WHERE PRODNO = " + OracleDBUtil.SqlStr(dr["PRODNO"].ToString());
                    OracleDBUtil.ExecuteSql(objTX, strsql);
                }
            }
        }

        /// <summary>
        /// 新增NDS主配作業
        /// </summary>
        /// <param name="ds">ORD08 DataSet</param>
        public void AddNew_NDSMMethodData(ORD08_HQNDSORDERMSet_DTO ds, string STATUS)
        {
            OracleConnection objConn = null;
            OracleTransaction objTX = null;

            try
            {
                objConn = OracleDBUtil.GetConnection();
                objTX = objConn.BeginTransaction();
                ORD08_HQNDSORDERMSet_DTO.HQ_NDS_ORDER_MRow dr = ds.HQ_NDS_ORDER_M.Rows[0] as ORD08_HQNDSORDERMSet_DTO.HQ_NDS_ORDER_MRow;
                string inHQ_NDS_ORDER_NO = dr.HQ_NDS_ORDER_NO;

                AddNew_NDSMMethodData(objTX, ds, STATUS);  //新增新資料

                if (STATUS == "2") //上傳確認
                {
                    //將各個門市的 "主配量" 加回 SCQC_D(卡片安全庫存量暨最低庫存量)的IN_QTY(已補貨量)
                    Call_SP_ORD08_UPDATE_SCQC_D(objTX, inHQ_NDS_ORDER_NO, "ADD");
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
        /// 修改NDS主配作業
        /// </summary>
        /// <param name="ds">ORD08 DataSet</param>
        /// <param name="STATUS">狀態 1:已存檔 2:已上傳 3.已轉門市訂單</param>
        public void Update_NDSMMethodData(ORD08_HQNDSORDERMSet_DTO ds, string STATUS, DataTable gvMasterOLD)
        {
            OracleConnection objConn = null;
            OracleTransaction objTX = null;

            try
            {
                objConn = OracleDBUtil.GetConnection();
                objTX = objConn.BeginTransaction();
                ORD08_HQNDSORDERMSet_DTO.HQ_NDS_ORDER_MRow dr = ds.HQ_NDS_ORDER_M.Rows[0] as ORD08_HQNDSORDERMSet_DTO.HQ_NDS_ORDER_MRow;
                string inHQ_NDS_ORDER_NO = dr.HQ_NDS_ORDER_NO;
                //**按下上傳確認才會加總主配量到已補貨量，所以不須減回動作。
                //if (STATUS == "2") //上傳確認
                //{
                //    //將各個門市的 "主配量" 減回 SCQC_D(卡片安全庫存量暨最低庫存量) 卡片群組有效期限內 的 IN_QTY(已補貨量)
                //    Call_SP_ORD08_UPDATE_SCQC_D(objTX, inHQ_NDS_ORDER_NO, "DEL");
                //}

                Delete_NDSMMethodData(objTX, ds, gvMasterOLD);  //先刪除舊資料
                AddNew_NDSMMethodData(objTX, ds, STATUS);  //再新增新資料

                if (STATUS == "2") //上傳確認
                {
                    //將各個門市的 "主配量" 加回 SCQC_D(卡片安全庫存量暨最低庫存量) 卡片群組有效期限內 的IN_QTY(已補貨量)
                    Call_SP_ORD08_UPDATE_SCQC_D(objTX, inHQ_NDS_ORDER_NO, "ADD");
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
        /// 將各個門市的 "主配量" 加 / 減 回 SCQC_D(卡片安全庫存量暨最低庫存量) 卡片群組有效期限內 的IN_QTY(已補貨量)
        /// </summary>
        /// <param name="objTX"></param>
        /// <param name="inHQ_NDS_ORDER_NO"></param>
        /// <param name="inACTION"></param>
        private void Call_SP_ORD08_UPDATE_SCQC_D(OracleTransaction objTX, string inHQ_NDS_ORDER_NO, string inACTION)
        {
            OracleCommand oraCmd = null;

            try
            {
                oraCmd = new OracleCommand("SP_ORD08_UPDATE_SCQC_D");
                oraCmd.CommandType = System.Data.CommandType.StoredProcedure;
                oraCmd.Parameters.Add(new OracleParameter("inHQ_NDS_ORDER_NO", inHQ_NDS_ORDER_NO));
                oraCmd.Parameters.Add(new OracleParameter("inACTION", inACTION));

                oraCmd.Connection = objTX.Connection;
                oraCmd.Transaction = objTX;
                oraCmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                oraCmd.Dispose();
            }
        }


        /// <summary>
        /// 刪除NDS主配作業
        /// </summary>
        /// <param name="ds">ORD08 DataSet</param>
        public void Delete_NDSMMethodData(OracleTransaction objTX, ORD08_HQNDSORDERMSet_DTO ds, DataTable gvMasterOLD)
        {

            foreach (ORD08_HQNDSORDERMSet_DTO.HQ_NDS_ORDER_MRow dr in ds.HQ_NDS_ORDER_M.Rows)
            {
                string HQ_NDS_ORDER_NO = dr["HQ_NDS_ORDER_NO"].ToString();
                string strsqlSTORE = @"DELETE FROM HQ_NDS_ORDER_STORE WHERE HQ_ORDER_D IN 
                        (SELECT HQ_ORDER_D FROM HQ_NDS_ORDER_D WHERE HQ_ORDER_M_ID IN 
                        (SELECT HQ_ORDER_M_ID FROM HQ_NDS_ORDER_M WHERE HQ_NDS_ORDER_NO = " + OracleDBUtil.SqlStr(HQ_NDS_ORDER_NO) + "))";
                OracleDBUtil.ExecuteSql(objTX, strsqlSTORE);
                string strsqlDETAIL = "DELETE FROM HQ_NDS_ORDER_D WHERE HQ_ORDER_M_ID IN (SELECT HQ_ORDER_M_ID FROM HQ_NDS_ORDER_M WHERE HQ_NDS_ORDER_NO =" + OracleDBUtil.SqlStr(HQ_NDS_ORDER_NO) + ")";
                OracleDBUtil.ExecuteSql(objTX, strsqlDETAIL);

            }
            //20110418 因儲存已經不存NDS_BOOK_QTY 所以刪除回壓 NDS_BOOK_QTY的做法拿掉 by 力維
            //if (gvMasterOLD != null)
            //{
            //    foreach (DataRow dr in gvMasterOLD.Rows)
            //    {
            //        string strsql = "UPDATE POS_ATR SET NDS_BOOK_QTY = NVL(NDS_BOOK_QTY,0) - " + dr["ASSIGN_QTY"] + " WHERE PRODNO = " + OracleDBUtil.SqlStr(dr["PRODNO"].ToString());
            //        OracleDBUtil.ExecuteSql(objTX, strsql);
            //    }
            //}
            string strsqlMaster = "DELETE FROM HQ_NDS_ORDER_M WHERE HQ_NDS_ORDER_NO = " + OracleDBUtil.SqlStr(ds.HQ_NDS_ORDER_M.Rows[0]["HQ_NDS_ORDER_NO"].ToString());
            OracleDBUtil.ExecuteSql(objTX, strsqlMaster);

        }

        /// <summary>
        /// 刪除NDS主配作業
        /// </summary>
        /// <param name="ds">ORD08 DataSet</param>
        public void Delete_NDSMMethodData(ORD08_HQNDSORDERMSet_DTO ds, DataTable gvMasterOLD)
        {
            OracleConnection objConn = null;
            OracleTransaction objTX = null;

            try
            {
                objConn = OracleDBUtil.GetConnection();
                objTX = objConn.BeginTransaction();

                Delete_NDSMMethodData(objTX, ds, gvMasterOLD);

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
        /// 變更主配作的狀態
        /// </summary>
        /// <param name="ds">ORD08 DataSet</param>
        public void Update_HQ_NDS_ORDER_M(ORD08_HQNDSORDERMSet_DTO ds)
        {
            OracleConnection objConn = null;
            OracleTransaction objTX = null;

            try
            {
                objConn = OracleDBUtil.GetConnection();
                objTX = objConn.BeginTransaction();

                OracleDBUtil.UPDDATEByUUID(objTX, ds.Tables["HQ_NDS_ORDER_M"], "HQ_ORDER_M_ID");

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
        /// 取得查詢結果NDS主配作業(Master Data)
        /// </summary>
        /// <param name="HQ_NDS_ORDER_NO">主配單號</param>
        /// <param name="LOC_ID">出貨倉別</param>
        /// <param name="STATUS">狀態</param>
        /// <param name="CREATE_DTM_S">主配日期(起)</param>
        /// <param name="CREATE_DTM_E">主配日期(迄)</param>
        /// <param name="PRODNO">商品料號</param>
        /// <param name="MODI_USER">更新人員編號</param>
        /// <returns>查詢結果</returns>
        public DataTable Query_HQNDSORDER(string HQ_NDS_ORDER_NO, string LOC_ID,
            string STATUS, string CREATE_DTM_S, string CREATE_DTM_E, string PRODNO, string MODI_USER)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(@"SELECT DISTINCT M.HQ_NDS_ORDER_NO
                                , TO_CHAR(M.CREATE_DTM, 'yyyy/MM/dd') as CREATE_DATE
                                , M.STATUS AS STATUS_ID
                                , DECODE(M.STATUS, '1', '已存檔', '2', '已上傳', '3' , '已轉門市訂單', M.STATUS) AS STATUS 
                                , M.MODI_USER
                                , E.EMPNAME
                                , TO_CHAR(M.MODI_DTM, 'yyyy/MM/dd hh24:mm:ss') as MODI_DTM
                                , S.LOC_ID 
                            FROM HQ_NDS_ORDER_M M,HQ_NDS_ORDER_D D, HQ_NDS_ORDER_STORE S, EMPLOYEE E 
                            WHERE M.HQ_ORDER_M_ID = D.HQ_ORDER_M_ID(+) AND D.HQ_ORDER_D = S.HQ_ORDER_D(+) AND M.MODI_USER = E.EMPNO 
                        ");

            if (!string.IsNullOrEmpty(HQ_NDS_ORDER_NO.Trim()))
            {
                sb.AppendLine(" AND M.HQ_NDS_ORDER_NO LIKE " + OracleDBUtil.LikeStr(HQ_NDS_ORDER_NO.Trim()));
            }

            if (!string.IsNullOrEmpty(LOC_ID))
            {
                sb.AppendLine(" AND S.LOC_ID = " + OracleDBUtil.SqlStr(LOC_ID));
            }

            if (!string.IsNullOrEmpty(STATUS))
            {
                sb.AppendLine(" AND M.STATUS = " + OracleDBUtil.SqlStr(STATUS));
            }

            if (!string.IsNullOrEmpty(CREATE_DTM_S))
            {
                sb.AppendLine(" AND trunc(M.CREATE_DTM) >= " + OracleDBUtil.DateStr(CREATE_DTM_S));
            }

            if (!string.IsNullOrEmpty(CREATE_DTM_E))
            {
                sb.AppendLine(" AND trunc(M.CREATE_DTM) <= " + OracleDBUtil.DateStr(CREATE_DTM_E));
            }

            if (!string.IsNullOrEmpty(PRODNO.Trim()))
            {
                sb.AppendLine(" AND D.PRODNO LIKE " + OracleDBUtil.LikeStr(PRODNO.Trim()));
            }

            if (!string.IsNullOrEmpty(MODI_USER.Trim()))
            {
                //sb.Append(" AND M.MODI_USER LIKE " + OracleDBUtil.LikeStr(MODI_USER.Trim())); //已編號查詢

                sb.AppendLine(" AND E.EMPNAME LIKE " + OracleDBUtil.LikeStr(MODI_USER.Trim()));  //已姓名查詢
            }

            DataTable dt = OracleDBUtil.Query_Data(sb.ToString());
            return dt;
        }

        /// <summary>
        /// 新增主配門市資料 - 新增整個區域的門市
        /// </summary>
        /// <param name="HQ_ORDER_D">主配商品_UUID</param>
        /// <param name="Zone">區域</param>
        public DataTable AddNew_HqNDSOrderStore(string HQ_ORDER_D, string Zone)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(@" SELECT S.STORE_NO
                                , S.STORENAME
                                , S.BRANCHNO
                                , W.WEIGHT
                           FROM STORE S, STORE_WEIGHT_DISTRIBUTE W 
                           WHERE S.STORE_NO = W.STORE_NO 
                       ");
            if (!string.IsNullOrEmpty(Zone))
            {
                sb.AppendLine("   AND S.ZONE = " + OracleDBUtil.SqlStr(Zone));
            }
            sb.AppendLine(@"  AND TO_CHAR(SYSDATE,'yyyyMMdd') <= NVL(S.CLOSEDATE,'99991231')                      --不包含已關閉的門市
                            AND (TO_CHAR(SYSDATE,'yyyyMMdd') < NVL(S.STOP_BDATE, TO_CHAR(SYSDATE+1,'yyyyMMdd')) 
                            OR  TO_CHAR(SYSDATE,'yyyyMMdd') > NVL(S.STOP_EDATE,TO_CHAR(SYSDATE-1,'yyyyMMdd')))  --也不包含暫停營業的門市
                       ");

            //**2011/01/19 Tina：門市重複的判斷在前端的Session中判斷
            //sb.Append("   AND STORE_NO NOT IN ( ");
            //sb.Append("      SELECT STORE_NO FROM HQ_NDS_ORDER_STORE WHERE HQ_ORDER_D = " + OracleDBUtil.SqlStr(HQ_ORDER_D) + " ");  //不可重複新增門市編號
            //sb.Append("   ) ");

            DataTable dt = OracleDBUtil.Query_Data(sb.ToString());
            return dt;
        }
        public DataTable GetImportTempData(string BATCH_NO, string FINC_ID, string USER_ID)
        {

            StringBuilder sb = new StringBuilder();
            sb.Append(" SELECT F1 AS STORENO ");
            sb.Append("       ,S.STORENAME ");
            sb.Append("       ,F2 AS PRODNO");
            sb.Append("       ,P.PRODNAME");
            sb.Append("       ,F3 AS ASSIGN_QTY");
            sb.Append("       ,F4 AS EXCEPTIOB_CAUSE");
            sb.Append("  FROM UPLOAD_TEMP M, STORE S, PRODUCT P  ");
            sb.Append("  WHERE 1=1 ");
            sb.Append("  AND M.F1 = S.STORE_NO(+) AND M.F2 = P.PRODNO(+) ");
            sb.Append("    AND BATCH_NO = " + OracleDBUtil.SqlStr(BATCH_NO));
            sb.Append("    AND USER_ID  = " + OracleDBUtil.SqlStr(USER_ID));
            sb.Append("    AND FINC_ID  = " + OracleDBUtil.SqlStr(FINC_ID));
            sb.Append("  Order by 1 ");


            DataTable dt = OracleDBUtil.Query_Data(sb.ToString());

            return dt;
        }

    }
}
