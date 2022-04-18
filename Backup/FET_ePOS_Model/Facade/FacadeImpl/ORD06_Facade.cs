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
    public class ORD06_Facade
    {
        private DateTime SysDate = DateTime.Now;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="MainProductNumber">主商品編號</param>
        /// <param name="MainProductName">商品名稱</param>
        /// <param name="CollocationProductCode">搭配商品編號</param>
        /// <param name="CollocationProductName">搭配商品名稱</param>
        /// <param name="StartDate">開始日期</param>
        /// <param name="EndDate">結束日期</param>
        /// <param name="Status">狀態</param>
        /// <returns></returns>
        public DataTable GetOneToOneMethodData(
                                 string MainProductNumber, string MainProductName,
                                 string CollocationProductCode, string CollocationProductName,
                                 string StartDate, string EndDate, string Status)
        {
            string sqlStr = @"SELECT TO_CHAR(ROWNUM) ITEMS,S1.* FROM 
                               (SELECT  TO_CHAR(M.SID) SID,
                               CASE WHEN (TRUNC(SYSDATE) >= TRUNC(M.S_DATE) AND TRUNC(SYSDATE)<= TRUNC(M.E_DATE))  THEN '有效'
                                       WHEN(TRUNC(SYSDATE)> TRUNC(M.E_DATE)) THEN '過期'
                                       WHEN(TRUNC(SYSDATE)< TRUNC(M.S_DATE)) THEN '尚未生效'
                                       ELSE '有效'
                                END STATUS
                               ,M.PRODNO PM_PRODNO,PM.PRODNAME PM_PRODNAME,D.PRODNO PD_PRODNO,PD.PRODNAME PD_PRODNAME,
                                M.S_DATE ,
                                (CASE WHEN TO_CHAR(M.E_DATE,'YYYY')='9999' THEN null ELSE M.E_DATE END) as E_DATE,
                                E.EMPNAME as MODI_USER,to_char(M.MODI_DTM,'yyyy/mm/dd hh24:mi:ss') MODI_DTM 
                               FROM ONETOONE_M M,ONETOONE_D D, PRODUCT PM,PRODUCT PD,EMPLOYEE E               
                              WHERE M.SID = D.SID(+)
                                AND M.PRODNO = PM.PRODNO (+)
                                AND D.PRODNO = PD.PRODNO(+)
                                AND M.MODI_USER = E.EMPNO(+)
                                )S1 WHERE 1=1";

            if (!string.IsNullOrEmpty(MainProductNumber))
            {
                sqlStr += " AND PM_PRODNO = " + OracleDBUtil.SqlStr(MainProductNumber);
            }
            if (!string.IsNullOrEmpty(MainProductName))
            {
                sqlStr += " AND PM_PRODNAME like " + OracleDBUtil.LikeStr(MainProductName);
            }
            if (!string.IsNullOrEmpty(CollocationProductCode))
            {
                sqlStr += " AND PD_PRODNO = " + OracleDBUtil.SqlStr(CollocationProductCode);
            }
            if (!string.IsNullOrEmpty(CollocationProductName))
            {
                sqlStr += " AND PD_PRODNAME like " + OracleDBUtil.LikeStr(CollocationProductName);
            }
            if (!string.IsNullOrEmpty(StartDate))
            {
                sqlStr += "  AND TRUNC(S_DATE) >= " + OracleDBUtil.DateStr(StartDate);
            }
            if (!string.IsNullOrEmpty(EndDate))
            {
                sqlStr += "  AND TRUNC(E_DATE) <= " + OracleDBUtil.DateStr(EndDate);
            }

            if (!string.IsNullOrEmpty(Status) && Status != "0")
            {
                sqlStr += " AND STATUS = decode(" + Status + ",1,'有效',2,'過期',3,'尚未生效') ";
            }

            DataTable dt = OracleDBUtil.Query_Data(sqlStr);
            return dt;
        }

        /// <summary>
        /// 檢核已被設定為主商品的商品在該商品的一搭一組合尚未失效前，無法被選為任何一搭一組合的搭配商品
        /// </summary>
        /// <param name="PD_PRODNO">搭配商品編號</param>
        /// <param name="PM_PRODNO">主商品編號</param>
        /// <param name="CHECK_TYPE">INSERT OR UPDATE</param>
        /// <param name="S_DATE">開始日期</param>
        /// <param name="E_DATE">結束日期</param>
        /// <returns></returns>
        public static string CheckMainProduct(string PD_PRODNO, string PM_PRODNO,string CHECK_TYPE,string S_DATE,string E_DATE)
        {
            string r = "";
            int CountResult = 0;
            if (CHECK_TYPE == "UPDATE")
            {
                CountResult = 1;
            }
            string SDate = string.Empty;
            if (!string.IsNullOrEmpty(S_DATE))
            {
                SDate = Convert.ToDateTime(S_DATE).ToString("yyyy/MM/dd");
            }
            string EDate = string.Empty;
            if (!string.IsNullOrEmpty(E_DATE))
            {
                EDate = Convert.ToDateTime(E_DATE).ToString("yyyy/MM/dd");
            }
            string sqlStr = @"SELECT * 
                                FROM (
                                          SELECT M.PRODNO,D.PRODNO AS PRODNO_D,
                                                 CASE WHEN (TRUNC(SYSDATE) >= TRUNC(M.S_DATE) AND TRUNC(SYSDATE) <= TRUNC(M.E_DATE))  THEN '有效'
                                                 WHEN(TRUNC(SYSDATE) > TRUNC(M.E_DATE)) THEN '過期'
                                                 WHEN(TRUNC(SYSDATE) < TRUNC(M.S_DATE)) THEN '尚未生效'
                                                 ELSE '有效'
                                          END STATUS
                                        ,M.S_DATE,M.E_DATE
                                            FROM ONETOONE_M M, ONETOONE_D D
                                            WHERE M.SID =D.SID
                                                ) M1
                                WHERE STATUS != '過期' 
                                AND ( ";
//                                 prodno = " + OracleDBUtil.SqlStr(PM_PRODNO);
//                    sqlStr += " OR";
                    sqlStr += "  prodno = " + OracleDBUtil.SqlStr(PD_PRODNO);
                    sqlStr += " OR";
                    //sqlStr += "  PRODNO_D = " + OracleDBUtil.SqlStr(PD_PRODNO);
                    //sqlStr += " OR";
                    sqlStr += " PRODNO_D = " + OracleDBUtil.SqlStr(PM_PRODNO);
                    sqlStr += " )";
                    sqlStr += @" AND (  
                                      maxdate(TO_CHAR(M1.S_DATE, 'yyyy/mm/dd'),NVL(" + OracleDBUtil.SqlStr(SDate) + ",'1000/01/01'))";
                    sqlStr += @"  <= mindate(TO_CHAR (nvl(M1.E_DATE,to_date('9999/12/31','yyyy/MM/dd')), 'yyyy/MM/dd'),  NVL(" + OracleDBUtil.SqlStr(EDate) + ",'9999/12/31'))";
                    sqlStr += @"        )";
            
            DataTable dt = OracleDBUtil.Query_Data(sqlStr);
            if (dt.Rows.Count > CountResult)
                r = dt.Rows[0][0].ToString();
            return r;
        }

        /// <summary>
        /// 檢核主商品與搭配商品在有效期間內，無重覆設定，否則顯示「一搭一組合己存在，請重新輸入」
        /// </summary>
        /// <param name="PM_PRODNO">主商品編號</param>
        /// <param name="PD_PRODNO">搭配商品編號</param>
        /// <param name="S_DATE">S_DATE</param>
        /// <param name="E_DATE">E_DATE</param>
        /// <returns></returns>
        public static string CheckMainDetailProduct(string PM_PRODNO, string PD_PRODNO, string S_DATE,string E_DATE)
        {
            string SDate = string.Empty;
            if (!string.IsNullOrEmpty(S_DATE))
            {
                SDate = Convert.ToDateTime(S_DATE).ToString("yyyy/MM/dd");
            }
            string EDate = string.Empty;
            if (!string.IsNullOrEmpty(E_DATE))
            {
                EDate = Convert.ToDateTime(E_DATE).ToString("yyyy/MM/dd");
            }
            string r = "";
            string sqlStr = @"
                               SELECT *  FROM (
                                SELECT M.PRODNO PM_PRODNO,D.PRODNO PD_PRDNO,M.E_DATE,M.S_DATE,
                                CASE WHEN (TRUNC(SYSDATE) >= TRUNC(M.S_DATE) AND TRUNC(SYSDATE) <= TRUNC(M.E_DATE))  THEN '有效'
                                     WHEN(TRUNC(SYSDATE) > TRUNC(M.E_DATE)) THEN '過期'
                                     WHEN(TRUNC(SYSDATE) < TRUNC(M.S_DATE)) THEN '尚未生效'
                                END STATUS 
                                FROM ONETOONE_M M,ONETOONE_D D
                                WHERE M.SID = D.SID)M1
                                 WHERE STATUS != '過期'
                                AND (  
                                      maxdate(TO_CHAR(M1.S_DATE, 'yyyy/mm/dd')," + OracleDBUtil.SqlStr(SDate) + ")";
            sqlStr += @"  <= mindate(TO_CHAR (nvl(M1.E_DATE,to_date('9999/12/31','yyyy/MM/dd')), 'yyyy/MM/dd'),  NVL(" + OracleDBUtil.SqlStr(EDate) + ",'9999/12/31'))";
                       sqlStr += @"        )
                               AND PM_PRODNO = " + OracleDBUtil.SqlStr(PM_PRODNO) + " AND PD_PRDNO = " + OracleDBUtil.SqlStr(PD_PRODNO);

            DataTable dt = OracleDBUtil.Query_Data(sqlStr);
            if (dt.Rows.Count > 0)
                r = dt.Rows[0][0].ToString();
            return r;
        }

        public void UpdateOenToOneMethodData(object SID, object PM_PRODNO, object PD_PRODNO, object S_DATE, object E_DATE, object MODI_USER)
        {
            OracleConnection objConn = null;
            OracleTransaction objTX = null;
            try
            {
                //表頭資料
                objConn = OracleDBUtil.GetConnection();
                objTX = objConn.BeginTransaction();

                DataTable dt = OracleDBUtil.GetDataSet(objTX, "SELECT PRODNO FROM ONETOONE_D WHERE SID = '" + SID.ToString() + "'").Tables[0];
                ORD06_OneToOne_DTO.ONETOONE_MDataTable dtm = new ORD06_OneToOne_DTO.ONETOONE_MDataTable();
                dtm.CREATE_USERColumn.AllowDBNull = true;
                dtm.CREATE_DTMColumn.AllowDBNull = true;
                ORD06_OneToOne_DTO.ONETOONE_MRow drm = dtm.NewONETOONE_MRow();
                drm.SID = SID.ToString();
                drm.PRODNO = PM_PRODNO.ToString();
                drm.S_DATE = Advtek.Utility.DateUtil.NullDateFormat(S_DATE);
                drm.E_DATE = (E_DATE != null && E_DATE.ToString().Substring(0, 4) == "0001") ? Convert.ToDateTime("9999/12/31") : Advtek.Utility.DateUtil.NullDateFormat(E_DATE);
                //drm["E_DATE"] = E_DATE == null ? DBNull.Value : E_DATE;
                drm.MODI_USER = MODI_USER.ToString();
                drm.MODI_DTM = SysDate;
                dtm.AddONETOONE_MRow(drm);
                dtm.AcceptChanges();
                OracleDBUtil.UPDDATEByUUID(objTX, dtm, "SID");

                //表身資料
                ORD06_OneToOne_DTO.ONETOONE_DDataTable dtd = new ORD06_OneToOne_DTO.ONETOONE_DDataTable();
                dtd.CREATE_USERColumn.AllowDBNull = true;
                dtd.CREATE_DTMColumn.AllowDBNull = true;
                ORD06_OneToOne_DTO.ONETOONE_DRow drd = dtd.NewONETOONE_DRow();
                drd.SID = SID.ToString();
                drd.COMPANYCODE = "01";
                drd.PRODNO = PD_PRODNO.ToString();
                drd.MODI_USER = MODI_USER.ToString();
                drd.MODI_DTM = SysDate;
                if (dt.Rows.Count > 0)
                {
                    dtd.AddONETOONE_DRow(drd);
                    dtd.AcceptChanges();
                    OracleDBUtil.UPDDATEByUUID(objTX, dtd, "SID");
                }
                else
                {
                    drd.CREATE_USER = MODI_USER.ToString();
                    drd.CREATE_DTM = SysDate;
                    dtd.AddONETOONE_DRow(drd);
                    dtd.AcceptChanges();

                    OracleDBUtil.Insert(objTX, dtd);
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

        public void DeleteOneToOne(DataSet ds)
        {
            OracleConnection objConn = null;
            OracleTransaction objTX = null;

            try
            {
                objConn = OracleDBUtil.GetConnection();
                objTX = objConn.BeginTransaction();

                OracleDBUtil.DELETEByUUID(objTX, ds.Tables["ONETOONE_M"], "SID");
                OracleDBUtil.DELETEByUUID(objTX, ds.Tables["ONETOONE_D"], "SID");

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

        public void InsertOneToOneMethodData(object PM_PRODNO, object PD_PRODNO, object S_DATE, object E_DATE, object MODI_USER)
        {
            OracleConnection objConn = null;
            OracleTransaction objTX = null;
            try
            {
                string SID = GuidNo.getUUID();

                //表頭資料
                objConn = OracleDBUtil.GetConnection();
                objTX = objConn.BeginTransaction();

                ORD06_OneToOne_DTO.ONETOONE_MDataTable dtm = new ORD06_OneToOne_DTO.ONETOONE_MDataTable();
                dtm.CREATE_USERColumn.AllowDBNull = true;
                dtm.CREATE_DTMColumn.AllowDBNull = true;
                ORD06_OneToOne_DTO.ONETOONE_MRow drm = dtm.NewONETOONE_MRow();
                drm.SID = SID;
                drm.PRODNO = PM_PRODNO.ToString();
                drm.S_DATE = Convert.ToDateTime(S_DATE);
                drm.E_DATE = (E_DATE == null || E_DATE.ToString() == "" || E_DATE.ToString().Substring(0, 4) == "0001") ? Convert.ToDateTime("9999/12/31") : Convert.ToDateTime(E_DATE);
                drm.CREATE_USER = MODI_USER.ToString();
                drm.CREATE_DTM = SysDate;
                drm.MODI_USER = MODI_USER.ToString();
                drm.MODI_DTM = SysDate;
                dtm.AddONETOONE_MRow(drm);
                dtm.AcceptChanges();
                OracleDBUtil.Insert(objTX, dtm);

                //表身資料
                if (PD_PRODNO != null)
                {
                    ORD06_OneToOne_DTO.ONETOONE_DDataTable dtd = new ORD06_OneToOne_DTO.ONETOONE_DDataTable();
                    dtd.CREATE_USERColumn.AllowDBNull = true;
                    dtd.CREATE_DTMColumn.AllowDBNull = true;
                    ORD06_OneToOne_DTO.ONETOONE_DRow drd = dtd.NewONETOONE_DRow();
                    drd.SID = SID;
                    //固定寫入01FET 
                    drd.COMPANYCODE = "01";
                    drd.PRODNO = PD_PRODNO.ToString();
                    drd.MODI_USER = MODI_USER.ToString();
                    drd.MODI_DTM = SysDate;
                    drd.CREATE_USER = MODI_USER.ToString();
                    drd.CREATE_DTM = SysDate;
                    dtd.AddONETOONE_DRow(drd);
                    dtd.AcceptChanges();
                    OracleDBUtil.Insert(objTX, dtd);
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

        public void UpdateOne_UpLoadTempMethodSet(string BATCH_NO, string FINC_ID, string USER_ID)
        {
            OracleConnection objConn = null;
            OracleTransaction objTX = null;

            try
            {
                objConn = OracleDBUtil.GetConnection();
                objTX = objConn.BeginTransaction();

                OracleDBUtil.ExecuteSql(objTX,
                    @"update UPLOAD_TEMP SET STATUS = 'C'
                                         where BATCH_NO = " + OracleDBUtil.SqlStr(BATCH_NO) + @"
                                          and FINC_ID  = " + OracleDBUtil.SqlStr(FINC_ID) + @"
                                          and USER_ID  = " + OracleDBUtil.SqlStr(USER_ID));


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

        public void ImportHeadQuarter(ORD06_OneToOne_DTO ds, string strType)
        {
            OracleConnection objConn = null;
            OracleTransaction objTX = null;

            try
            {
                objConn = OracleDBUtil.GetConnection();
                objTX = objConn.BeginTransaction();

                string strSql = null;
                strSql = "delete UPLOAD_TEMP where nvl(STATUS,'T') <> 'C' " +
                                              " and FINC_ID  = " + OracleDBUtil.SqlStr(ds.Tables["UPLOAD_TEMP"].Rows[0]["FINC_ID"].ToString()) +
                                              " and USER_ID  = " + OracleDBUtil.SqlStr(ds.Tables["UPLOAD_TEMP"].Rows[0]["USER_ID"].ToString());
                OracleDBUtil.ExecuteSql(objTX, strSql);

                OracleDBUtil.Insert(ds.Tables["UPLOAD_TEMP"]);

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

        public DataTable GetImportTempData(string BATCH_NO, string FINC_ID, string USER_ID)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(" SELECT F2 AS MPRODNO ");
            sb.Append("       ,F3 AS MPRODNAME ");
            sb.Append("       ,F4 AS DPRODNO");
            sb.Append("       ,F5 AS DPRODNAME");
            sb.Append("       ,F6 AS SDATE");
            sb.Append("       ,F7 AS EDATE");
            sb.Append("       ,F8 AS RESULT");
            sb.Append("  FROM UPLOAD_TEMP ");
            sb.Append("  WHERE 1=1 ");
            sb.Append("    AND BATCH_NO = " + OracleDBUtil.SqlStr(BATCH_NO));
            sb.Append("    AND USER_ID  = " + OracleDBUtil.SqlStr(USER_ID));
            sb.Append("    AND FINC_ID  = " + OracleDBUtil.SqlStr(FINC_ID));
            sb.Append("    AND F1       = 'PRODUCT'");

            DataTable dt = OracleDBUtil.Query_Data(sb.ToString());

            return dt;
        }


    }
}
