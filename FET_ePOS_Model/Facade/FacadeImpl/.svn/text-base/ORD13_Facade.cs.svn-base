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
    public class ORD13_Facade 
    {
        public DataTable GetMasterData(object S_STORE_NO, object E_STORE_NO, object STORE_NAME)
        {

            StringBuilder sb = new StringBuilder();
            sb.AppendLine(@" SELECT MASTER_ID
                                  , STORE_NO
                                  , to_char(MODI_DTM,'yyyy/mm/dd hh24:mi:ss') MODI_DTM 
                                  , MODI_USER_NAME AS MODI_USER 
                                  , STORENAME 
                             FROM VW_ORD13 
                             WHERE 1=1 ");
            if (!string.IsNullOrEmpty((string)S_STORE_NO))
            {
                sb.AppendLine(" AND STORE_NO >= " + OracleDBUtil.SqlStr(S_STORE_NO.ToString()));
            }
            if (!string.IsNullOrEmpty((string)E_STORE_NO))
            {
                sb.AppendLine(" AND STORE_NO <= " + OracleDBUtil.SqlStr(E_STORE_NO.ToString()));
            }

            if (!string.IsNullOrEmpty((string)STORE_NAME))
            {
                sb.AppendLine(" AND STORENAME like " + OracleDBUtil.LikeStr(STORE_NAME.ToString()));
            }
            sb.AppendLine(" ORDER BY STORE_NO ");

            DataTable dt = OracleDBUtil.Query_Data(sb.ToString());
            return dt;

        }

        public DataTable GetDetailData(object MASTER_ID)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(@" SELECT (M.SAFE_QTY - NVL(M.ORDER_QTY,0)) ORDER_QTY
                                    , M.*
                            FROM VW_ORD13_DETAIL M
                            WHERE M.MASTER_ID=" + OracleDBUtil.SqlStr(MASTER_ID.ToString())
                            + " ORDER BY M.SIM_GROUP_NAME ");

            DataTable dt = OracleDBUtil.Query_Data(sb.ToString());
            return dt;
        }

        public void InsertMaster(ORD13_SCQC_DTO ds)
        {
            OracleConnection objConn = null;
            OracleTransaction objTX = null;

            try
            {
                objConn = OracleDBUtil.GetConnection();
                objTX = objConn.BeginTransaction();

                OracleDBUtil.Insert(objTX, ds.SCQC_M);

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

        public void InsertDetail(ORD13_SCQC_DTO ds)
        {
            OracleConnection objConn = null;
            OracleTransaction objTX = null;

            try
            {
                objConn = OracleDBUtil.GetConnection();
                objTX = objConn.BeginTransaction();

                OracleDBUtil.Insert(objTX, ds.SCQC_D);

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

        public void UpdateMaster(ORD13_SCQC_DTO ds)
        {
            OracleConnection objConn = null;
            OracleTransaction objTX = null;

            try
            {
                objConn = OracleDBUtil.GetConnection();
                objTX = objConn.BeginTransaction();

                OracleDBUtil.UPDDATEByUUID(objTX, ds.SCQC_M, "SCQC_M_ID");

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

        public void UpdateDetail(ORD13_SCQC_DTO ds)
        {
            OracleConnection objConn = null;
            OracleTransaction objTX = null;

            try
            {
                objConn = OracleDBUtil.GetConnection();
                objTX = objConn.BeginTransaction();

                OracleDBUtil.UPDDATEByUUID(objTX, ds.SCQC_D, "SCQC_D_ID");

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

        public void DeleteMaster(DataTable dt)
        {
           // OracleDBUtil.DELETEByUUID(dt, dt.Columns[0].ColumnName);

            OracleConnection objConn = null;
            OracleTransaction objTX = null;
            try
            {
                objConn = OracleDBUtil.GetConnection();
                objTX = objConn.BeginTransaction();

                OracleDBUtil.ExecuteSql(objTX,
                    @"delete SCQC_M
                    where SCQC_M_ID  = " + OracleDBUtil.SqlStr(dt.Rows[0]["SCQC_M_ID"].ToString()));

                OracleDBUtil.ExecuteSql(objTX,
                @"delete SCQC_D
                    where SCQC_M_ID  = " + OracleDBUtil.SqlStr(dt.Rows[0]["SCQC_M_ID"].ToString()));
          

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

        public void DeleteDetail(DataTable dt)
        {
            DeleteMaster(dt);
        }

        public void ImportTemp(DataTable dt)
        {
            OracleConnection objConn = null;
            OracleTransaction objTX = null;

            try
            {
                objConn = OracleDBUtil.GetConnection();
                objTX = objConn.BeginTransaction();

                OracleDBUtil.ExecuteSql(objTX,
                    @"delete UPLOAD_TEMP
                    where nvl(STATUS,'T')<>'C' " +
                                              "and FINC_ID  = " + OracleDBUtil.SqlStr(dt.Rows[0]["FINC_ID"].ToString()) +
                                              "and USER_ID  = " + OracleDBUtil.SqlStr(dt.Rows[0]["USER_ID"].ToString()));

                OracleDBUtil.Insert(objTX, dt);

                //ora_com
                OracleDBUtil.ExecuteSql_SP(
                    objTX
                    , "PK_Upload_Check.ORD13_CHECK"
                    , new OracleParameter("p_BATCH_NO", dt.Rows[0]["BATCH_NO"].ToString())
                    , new OracleParameter("p_USER_ID", dt.Rows[0]["USER_ID"].ToString())
                    , new OracleParameter("p_FINC_ID", dt.Rows[0]["FINC_ID"].ToString())
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
                objTX.Dispose();

                if (objConn.State == ConnectionState.Open) objConn.Close();
                objConn.Dispose();
                OracleConnection.ClearAllPools();
            }
        }

        public INV05_RTNM_DTO.UPLOAD_TEMPDataTable GetTemp(string BATCH_NO)
        {
            INV05_RTNM_DTO.UPLOAD_TEMPDataTable dt = new INV05_RTNM_DTO.UPLOAD_TEMPDataTable();
            OracleConnection objConn = null;
            try
            {
                StringBuilder sb = new StringBuilder();
                sb.AppendLine(@" SELECT M.SID
                                        , M.BATCH_NO
                                        , M.USER_ID
                                        , M.FINC_ID
                                        , M.STATUS
                                        , M.F1
                                        , S.STORENAME F2
                                        , M.F3
                                        , M.F4
                                        , M.F5
                                        , M.F6
                                        , M.F7
                                        , M.F8
                                 FROM UPLOAD_TEMP M, STORE S
                                 WHERE M.F1 = S.STORE_NO(+)
                                   AND M.BATCH_NO=" + OracleDBUtil.SqlStr(BATCH_NO.ToString()));

                objConn = OracleDBUtil.GetConnection();
                dt.Load(OracleDBUtil.GetDataReader(objConn, sb.ToString()));
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

            return dt;
        }

        public void TempToData(string BATCH_NO, string USER_ID)
        {
            OracleConnection objConn = null;
            OracleTransaction objTX = null;

            try
            {
                objConn = OracleDBUtil.GetConnection();
                objTX = objConn.BeginTransaction();

                //處理程序
                OracleDBUtil.ExecuteSql_SP(
                    objTX
                    , "PK_Upload_Check.ORD13_IMPORT"
                    , new OracleParameter("p_BATCH_NO", BATCH_NO)
                    , new OracleParameter("p_USER_ID", USER_ID)
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
                objTX.Dispose();

                if (objConn.State == ConnectionState.Open) objConn.Close();
                objConn.Dispose();
                OracleConnection.ClearAllPools();
            }
        }

        public DataTable TopDatatable1(string SIMGROUPNO, string PRODNO, bool bInitial)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(@"SELECT DISTINCT SIM_GROUP_M.SIM_GROUP_ID,
                             SIM_GROUP_M.SIM_GROUP_NO,
                             SIM_GROUP_NAME,
                             S_DATE,
                             E_DATE,
                             SIM_GROUP_M.MODI_DTM FROM  SIM_GROUP_M 
                            WHERE     1=1 
                               AND S_DATE <= TRUNC(SYSDATE) 
                               AND NVL(e_DATE,TO_DATE('99991231','YYYYMMDD')) >= TRUNC(SYSDATE) ");
            sb.Append(" order by SIM_GROUP_NAME ");

            DataTable dt = OracleDBUtil.Query_Data(sb.ToString());
            return dt;
        }

        public string getSIM_GROUP_NAME(object SIM_GROUP_ID)
        {
            string Name = "";
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(@" SELECT  DISTINCT SIM_GROUP_NAME FROM SIM_GROUP_M WHERE SIM_GROUP_ID=" + OracleDBUtil.SqlStr(SIM_GROUP_ID.ToString()));

            DataTable dt = OracleDBUtil.Query_Data(sb.ToString());
            if (dt.Rows.Count > 0)
            {
                Name = dt.Rows[0]["SIM_GROUP_NAME"].ToString();
            }
            return Name;
        }
        /// <summary>
        /// 匯出EXCEL檔
        /// </summary>
        /// <param name="S_STORE_NO"></param>
        /// <param name="E_STORE_NO"></param>
        /// <param name="STORE_NAME"></param>
        /// <returns></returns>
        public DataTable QueryExportExcel(object S_STORE_NO, object E_STORE_NO, object STORE_NAME)
        {

            StringBuilder sb = new StringBuilder();
            sb.AppendLine(@"SELECT M.STORE_NO AS 門市編號,
                                    M.STORENAME AS 門市名稱,
                                    D.SIM_GROUP_NAME AS 卡片群組名稱,
                                    S_DATE AS 開始日期,
                                    E_DATE AS 結束日期,
                                    D.SAFE_QTY AS 安全庫存量,
                                    D.L_BOUND AS 最低庫存量,
                                    (D.SAFE_QTY - NVL(D.ORDER_QTY,0))AS 應補貨量,
                                    D.IN_QTY AS 已補貨量
                                    FROM VW_ORD13 M ,VW_ORD13_DETAIL D
                                    WHERE M.master_id = D.MASTER_ID ");
            if (!string.IsNullOrEmpty((string)S_STORE_NO))
            {
                sb.AppendLine(" AND M.STORE_NO >= " + OracleDBUtil.SqlStr(S_STORE_NO.ToString()));
            }
            if (!string.IsNullOrEmpty((string)E_STORE_NO))
            {
                sb.AppendLine(" AND M.STORE_NO <= " + OracleDBUtil.SqlStr(E_STORE_NO.ToString()));
            }

            if (!string.IsNullOrEmpty((string)STORE_NAME))
            {
                sb.AppendLine(" AND  M.STORENAME like " + OracleDBUtil.LikeStr(STORE_NAME.ToString()));
            }
            sb.AppendLine(" ORDER BY M.STORE_NO,D.SIM_GROUP_NAME ");

            DataTable dt = OracleDBUtil.Query_Data(sb.ToString());
            return dt;

        }
    }
}
