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
    public class OPT15_Facade
    {
        /// <summary>
        /// 取得查詢結果HappyGo點數兌換-來店禮(Master Data)
        /// </summary>
        /// <param name="SDATE_S">開始日期起</param>
        /// <param name="SDATE_E">開始日期迄</param>
        /// <param name="S_PartNumberOfDiscount">折扣料號起</param>
        /// <param name="E_PartNumberOfDiscount">折扣料號迄</param>
        /// <param name="DiscountName">折扣名稱</param>
        /// <returns>查詢結果</returns>
        public DataTable Query_HgConvertibleGiftM(string SDATE_S, string SDATE_E,
            string S_PartNumberOfDiscount, string E_PartNumberOfDiscount, string DiscountName)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(@"SELECT ROWNUM AS ITEMNO, tb.* 
                            FROM  ( 
                                SELECT M.ACTIVITY_ID
                                        , M.ACTIVITY_NO
                                        , M.ACTIVITY_NAME
                                        , M.S_DATE
                                        , M.E_DATE
                                        , M.TYPE, M.PRODNO
                                        , TO_CHAR(M.DIVIDABLE_POINT) DIVIDABLE_POINT
                                        , M.MEMBER_CHECK_FLAG
                                        , M.USE_COUNT,M.MODI_USER
                                        , E.EMPNAME
                                        , TO_CHAR(M.MODI_DTM, 'yyyy/mm/dd hh24:mi:ss') as MODI_DTM 
                                FROM HG_CONVERTIBLE_GIFT_M M, EMPLOYEE E 
                                WHERE M.MODI_USER = E.EMPNO(+) 
                                AND DEL_FLAG = 'N' 
                        ");

            if (!string.IsNullOrEmpty(SDATE_S))
            {
                sb.AppendLine(" AND M.S_DATE >= " + OracleDBUtil.TimeStr(SDATE_S));
            }

            if (!string.IsNullOrEmpty(SDATE_E))
            {
                sb.AppendLine(" AND M.S_DATE <= " + OracleDBUtil.TimeStr(SDATE_E));
            }

            if (!string.IsNullOrEmpty(S_PartNumberOfDiscount))
            {
                sb.AppendLine(" AND M.ACTIVITY_NO >= " + OracleDBUtil.SqlStr(S_PartNumberOfDiscount));
            }

            if (!string.IsNullOrEmpty(E_PartNumberOfDiscount))
            {
                sb.AppendLine(" AND M.ACTIVITY_NO <= " + OracleDBUtil.SqlStr(E_PartNumberOfDiscount));
            }

            if (!string.IsNullOrEmpty(DiscountName))
            {
                sb.AppendLine(" AND M.ACTIVITY_NAME like " + OracleDBUtil.LikeStr(DiscountName));
            }

            sb.AppendLine(@" ORDER BY M.ACTIVITY_NO
                            ) tb 
                            ORDER BY ITEMNO 
                         ");

            DataTable dt = OracleDBUtil.Query_Data(sb.ToString());
            return dt;
        }

        /// <summary>
        /// 取得名單是否已匯入 (Count>0表示名單已匯入)
        /// </summary>
        /// <param name="ACTIVITY_NO">折扣料號</param>
        /// <returns>Count</returns>
        public int Query_HgConvertMemberList(string ACTIVITY_NO)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(" SELECT ACTIVITY_NO ");
            sb.Append(" FROM HG_CONVERT_MEMBER_LIST ");
            sb.Append(" WHERE ACTIVITY_NO = " + OracleDBUtil.SqlStr(ACTIVITY_NO));
            sb.Append(" AND FUNC_ID = 'OPT15'");

            DataTable dt = OracleDBUtil.Query_Data(sb.ToString());
            return dt.Rows.Count;
        }

        public int Query_HgConvertMemberListByID(string ACTIVITY_ID)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT 1 FROM HG_CONVERTIBLE_GIFT_M T " +
                      " WHERE T.ACTIVITY_ID=" + OracleDBUtil.SqlStr(ACTIVITY_ID) +
                      " AND TRUNC(T.S_DATE) > TRUNC(SYSDATE)");

            DataTable dt = OracleDBUtil.Query_Data(sb.ToString());
            return dt.Rows.Count;
        }

        /// <summary>
        /// 新增HappyGo點數兌換-來店禮(Master Data)
        /// </summary>
        /// <param name="ds">OPT15 DataSet</param>
        public void AddNewOne_HgConvertibleGiftM(OPT15_HgConvertibleGiftM_DTO ds)
        {
            OracleDBUtil.Insert(ds.Tables["HG_CONVERTIBLE_GIFT_M"]);
        }

        /// <summary>
        /// 修改HappyGo點數兌換-來店禮(Master Data)
        /// </summary>
        /// <param name="ds">OPT15 DataSet</param>
        public void UpdateOne_HgConvertibleGiftM(OPT15_HgConvertibleGiftM_DTO ds)
        {
            OracleDBUtil.UPDDATEByUUID_IncludeDBNull(ds.Tables["HG_CONVERTIBLE_GIFT_M"], "ACTIVITY_ID");
        }

        /// <summary>
        /// 刪除HappyGo點數兌換-來店禮(Master Data)
        /// </summary>
        /// <param name="ds">OPT15 DataSet</param>
        public void Delete_HgConvertibleGiftM(OPT15_HgConvertibleGiftM_DTO ds)
        {
            OracleConnection objConn = null;
            OracleTransaction objTX = null;

            try
            {
                objConn = OracleDBUtil.GetConnection();
                objTX = objConn.BeginTransaction();

                foreach (OPT15_HgConvertibleGiftM_DTO.HG_CONVERTIBLE_GIFT_MRow dr in ds.HG_CONVERTIBLE_GIFT_M.Rows)
                {
                    string ACTIVITY_ID = dr["ACTIVITY_ID"].ToString();
                    string strsql = "UPDATE HG_CONVERT_GIFT_D SET DEL_FLAG='Y' WHERE ACTIVITY_ID = " + OracleDBUtil.SqlStr(ACTIVITY_ID);

                    OracleDBUtil.ExecuteSql(objTX, strsql); 
                }

                OracleDBUtil.UPDDATEByUUID(objTX, ds.Tables["HG_CONVERTIBLE_GIFT_M"], "ACTIVITY_ID");  //將 DEL_FLAS 設為 Y

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
        /// 取得查詢結果HappyGo點數兌換-來店禮(Detail Data)
        /// </summary>
        /// <param name="ACTIVITY_ID">主檔_UUID</param>
        /// <returns>查詢結果</returns>
        public DataTable Query_HgConvertGiftD(string ACTIVITY_ID)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(" SELECT GIFT_D.SID, STORE.STORE_NO, STORE.STORENAME, ZONE.ZONE_NAME ");
            sb.Append(" FROM HG_CONVERT_GIFT_D GIFT_D, STORE, ZONE ");
            sb.Append(" WHERE GIFT_D.STORE_NO = STORE.STORE_NO AND STORE.ZONE = ZONE.ZONE ");
            sb.Append(" AND GIFT_D.ACTIVITY_ID = " + OracleDBUtil.SqlStr(ACTIVITY_ID));
            sb.Append(" AND GIFT_D.DEL_FLAG = 'N' ");
            sb.Append(" Order by ZONE.ZONE_NAME,STORE.STORE_NO");

            DataTable dt = OracleDBUtil.Query_Data(sb.ToString());
            return dt;
        }

        /// <summary>
        /// 新增HappyGo點數兌換-來店禮(Detail Data)
        /// </summary>
        /// <param name="ds">OPT15 DataSet</param>
        public void AddNewOne_HgConvertGiftD(OPT15_HgConvertibleGiftM_DTO ds)
        {
            OracleDBUtil.Insert(ds.Tables["HG_CONVERT_GIFT_D"]);
        }

        /// <summary>
        /// 新增HappyGo點數兌換-來店禮(Detail Data) - 新增整個區域的門市
        /// </summary>
        /// <param name="ACTIVITY_ID">主檔_UUID</param>
        /// <param name="User">登入者</param>
        /// <param name="Zone">區域</param>
        public void AddNew_HgConvertGiftD(string ACTIVITY_ID, string User, string Zone)
        {

            OracleConnection objConn = null;
            OracleTransaction objTX = null;

            try
            {
                objConn = OracleDBUtil.GetConnection();
                objTX = objConn.BeginTransaction();

                System.Text.StringBuilder sbUPD = new System.Text.StringBuilder();
                sbUPD.Append("UPDATE HG_CONVERT_GIFT_D SET ");
                sbUPD.Append("MODI_USER = " + OracleDBUtil.SqlStr(User) + ", ");
                sbUPD.Append("MODI_DTM = sysdate, ");
                sbUPD.Append("DEL_FLAG = 'N' ");
                sbUPD.Append("WHERE 1 =1 ");
                sbUPD.Append("AND ACTIVITY_ID = " + OracleDBUtil.SqlStr(ACTIVITY_ID) + " ");
                sbUPD.Append("AND STORE_NO IN ( ");
                sbUPD.Append(" SELECT STORE_NO FROM STORE");
                sbUPD.Append(" WHERE 1 =1 ");
                if (!string.IsNullOrEmpty(Zone))
                {
                    sbUPD.Append("   AND ZONE = " + OracleDBUtil.SqlStr(Zone) + " ");
                }
                sbUPD.Append("   AND TO_CHAR(SYSDATE,'yyyyMMdd') <= NVL(CLOSEDATE,'99991231') ");   //不包含已關閉的門市
                sbUPD.Append("   AND (TO_CHAR(SYSDATE,'yyyyMMdd') < NVL(STOP_BDATE,SYSDATE-1) ");
                sbUPD.Append("   OR  TO_CHAR(SYSDATE,'yyyyMMdd') > NVL( STOP_EDATE,SYSDATE+1)) "); //也不包含暫停營業的門市
                sbUPD.Append("   ) ");


                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                sb.Append("INSERT INTO HG_CONVERT_GIFT_D( ");
                sb.Append("SID, ACTIVITY_ID, CREATE_USER, CREATE_DTM, MODI_USER, MODI_DTM, STORE_NO, DEL_FLAG ");
                sb.Append(") ");
                sb.Append("  SELECT pos_uuid(), " + OracleDBUtil.SqlStr(ACTIVITY_ID) + ", " + OracleDBUtil.SqlStr(User) + ", ");
                sb.Append("         sysdate, " + OracleDBUtil.SqlStr(User) + ", ");
                sb.Append("         sysdate, STORE_NO, 'N' ");
                sb.Append("   FROM STORE ");
                sb.Append("   WHERE 1 =1 ");
                if (!string.IsNullOrEmpty(Zone))
                {
                    sb.Append("   AND ZONE = " + OracleDBUtil.SqlStr(Zone) + " ");
                }
                sb.Append("   AND TO_CHAR(SYSDATE,'yyyyMMdd') <= NVL(CLOSEDATE,'99991231') ");   //不包含已關閉的門市
                sb.Append("   AND (TO_CHAR(SYSDATE,'yyyyMMdd') < NVL(STOP_BDATE, TO_CHAR(SYSDATE+1,'yyyyMMdd'))  ");
                sb.Append("   OR  TO_CHAR(SYSDATE,'yyyyMMdd') > NVL(STOP_EDATE,TO_CHAR(SYSDATE-1,'yyyyMMdd'))) "); //也不包含暫停營業的門市
                sb.Append("   AND STORE_NO NOT IN ( ");
                sb.Append("      SELECT STORE_NO FROM HG_CONVERT_GIFT_D WHERE ACTIVITY_ID = " + OracleDBUtil.SqlStr(ACTIVITY_ID) + " ");  //不可重複新增門市編號
                sb.Append("   ) ");


                OracleDBUtil.ExecuteSql(objTX, sbUPD.ToString());
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
                objTX = null;

                if (objConn.State == ConnectionState.Open) objConn.Close();
                objConn.Dispose();
                OracleConnection.ClearAllPools();
            }
        }

        /// <summary>
        /// 修改HappyGo點數兌換-來店禮(Detail Data)
        /// </summary>
        /// <param name="ds">OPT15 DataSet</param>
        public void UpdateOne_HgConvertGiftD(OPT15_HgConvertibleGiftM_DTO ds)
        {
            OracleDBUtil.UPDDATEByUUID(ds.Tables["HG_CONVERT_GIFT_D"], "SID");
        }

        /// <summary>
        /// 刪除HappyGo點數兌換-來店禮(Detail Data)
        /// </summary>
        /// <param name="ds">OPT15 DataSet</param>
        public void Delete_HgConvertibleGiftD(OPT15_HgConvertibleGiftM_DTO ds)
        {
            OracleDBUtil.UPDDATEByUUID(ds.Tables["HG_CONVERT_GIFT_D"], "SID");
        }

        public int Query_HgConvertibleGiftD(string ActivityID, string Store_NO, string SID)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT count(*) as cnt ");
            sb.Append("FROM HG_CONVERT_GIFT_D D ");
            sb.Append("WHERE D.DEL_FLAG = 'N' ");

            if (!string.IsNullOrEmpty(SID))
            {
                sb.Append(" AND D.SID <> " + OracleDBUtil.SqlStr(SID));
            }

            if (!string.IsNullOrEmpty(ActivityID))
            {
                sb.Append(" AND D.ACTIVITY_ID = " + OracleDBUtil.SqlStr(ActivityID));
            }

            if (!string.IsNullOrEmpty(Store_NO))
            {
                sb.Append(" AND D.STORE_NO = " + OracleDBUtil.SqlStr(Store_NO));
            }

            DataTable dt = OracleDBUtil.Query_Data(sb.ToString());
            return Convert.ToInt32(dt.Rows[0][0].ToString());
        }
    }
}
