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
    public class ORD14_Facade 
    {

        private DateTime SysDate = DateTime.Now;

        public DataTable TopDatatable1(string SIMGROUPNO, string PRODNO, bool bInitial)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(@"SELECT DISTINCT SIM_GROUP_M.SIM_GROUP_ID
                                            , SIM_GROUP_NAME
                                            , S_DATE
                                            , E_DATE
                                            , nvl((SELECT EMPNAME FROM EMPLOYEE WHERE EMPLOYEE.EMPNO=SIM_GROUP_M.MODI_USER AND ROWNUM=1 ),'') as MODI_USER
                                            , to_Char(SIM_GROUP_M.MODI_DTM,'yyyy/mm/dd hh24:mi:ss') as MODI_DTM 
                                            , (CASE WHEN (S_DATE <= TRUNC(SYSDATE) AND NVL(E_DATE, TO_DATE ('9999/12/31', 'yyyy/mm/dd')) >= TRUNC(SYSDATE)) THEN '有效'
                                                    WHEN (NVL(E_DATE, TO_DATE('9999/12/31', 'yyyy/mm/dd')) < TRUNC(SYSDATE)) THEN '已過期'
                                                    WHEN (S_DATE > TRUNC(SYSDATE)) THEN '尚未生效'
                                               END) AS STATUS
                            FROM  SIM_GROUP_M, SIM_GROUP_PROD
                            WHERE SIM_GROUP_M.SIM_GROUP_ID = SIM_GROUP_PROD.SIM_GROUP_ID(+) ");

            if (!string.IsNullOrEmpty(SIMGROUPNO))
            {
                sb.AppendLine(" AND SIM_GROUP_NAME like " + OracleDBUtil.LikeStr(SIMGROUPNO));
            }
            if (!string.IsNullOrEmpty(PRODNO))
            {
                sb.AppendLine(" AND PRODNO like " + OracleDBUtil.LikeStr(PRODNO));
            }
            if (bInitial)
            {
                sb.AppendLine(" AND S_DATE <= TRUNC (SYSDATE) AND NVL (e_date, TO_DATE ('99991231', 'yyyymmdd')) >= TRUNC (SYSDATE)");
            }
            sb.AppendLine(" order by SIM_GROUP_NAME ");

            DataTable dt = OracleDBUtil.Query_Data(sb.ToString());
            return dt;

        }

        public void AddDatatable2(System.Collections.Specialized.OrderedDictionary NewValues, string User)
        {
            ORD14_SIM_GROUP.SIM_GROUP_MDataTable dtd = new ORD14_SIM_GROUP.SIM_GROUP_MDataTable();
            ORD14_SIM_GROUP.SIM_GROUP_MRow drd = dtd.NewSIM_GROUP_MRow();

            foreach (DataColumn dc in dtd.Columns) dc.AllowDBNull = true;

            drd.SIM_GROUP_ID = GuidNo.getUUID();
            drd.SIM_GROUP_NAME = ReplaceInjectionStrig(NewValues["SIM_GROUP_NAME"].ToString());
            if (NewValues["E_DATE"] == null)
            {
                drd.E_DATE = Advtek.Utility.DateUtil.NullDateFormat(null);
            }
            else
            {
                if (NewValues["E_DATE"].ToString() == "")
                {
                    drd.E_DATE = Advtek.Utility.DateUtil.NullDateFormat(null);
                }
                else
                {
                    drd.E_DATE = Convert.ToDateTime(NewValues["E_DATE"].ToString());
                }
            }

            drd.S_DATE = Convert.ToDateTime(NewValues["S_DATE"].ToString());
            drd.SIM_GROUP_NO = "1";
            drd.CREATE_USER = User;
            drd.CREATE_DTM = SysDate;
            drd.MODI_DTM = SysDate;
            drd.MODI_USER = User;
            dtd.AddSIM_GROUP_MRow(drd);
            dtd.AcceptChanges();
            OracleDBUtil.Insert(dtd);
        }

        public DataTable GetTakeOff_DData(string SIM_GROUP_ID)
        {
            string sqlStr = @"SELECT SIM_GROUP_PROD
                                    ,SIM_GROUP_ID
                                    ,SIM_GROUP_PROD.PRODNO
                                    ,PRODUCT.PRODNAME 
                            FROM  SIM_GROUP_PROD,PRODUCT 
                            WHERE SIM_GROUP_PROD.PRODNO = PRODUCT.PRODNO(+)
                            AND   SIM_GROUP_ID = " + OracleDBUtil.SqlStr(SIM_GROUP_ID);

            DataTable dtReturn = OracleDBUtil.Query_Data(sqlStr);
            return dtReturn;
        }

        public void ADDPROD(System.Collections.Specialized.OrderedDictionary NewValues, string User)
        {
            ORD14_SIM_GROUP.SIM_GROUP_PRODDataTable dtd = new ORD14_SIM_GROUP.SIM_GROUP_PRODDataTable();
            ORD14_SIM_GROUP.SIM_GROUP_PRODRow drd = dtd.NewSIM_GROUP_PRODRow();

            foreach (DataColumn dc in dtd.Columns) dc.AllowDBNull = true;

            drd.SIM_GROUP_PROD = GuidNo.getUUID();
            drd.PRODNO = ReplaceInjectionStrig(NewValues["PRODNO"].ToString());
            drd.SIM_GROUP_ID = NewValues["SIM_GROUP_ID"].ToString();
            drd.CREATE_USER = User;
            drd.CREATE_DTM = SysDate;
            drd.MODI_DTM = SysDate;
            drd.MODI_USER = User;
            dtd.AddSIM_GROUP_PRODRow(drd);
            dtd.AcceptChanges();
            OracleDBUtil.Insert(dtd);
        }

        public DataTable GetTakeOff_DPData(string SIM_GROUP_ID, string PRODNO, string SIM_GROUP_PROD)
        {
            //**2011/03/22 Tina："編輯"儲存時在判斷是否有商品重複，要排除自己。
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(@" SELECT D.SIM_GROUP_PROD
                                    , D.SIM_GROUP_ID
                                    , D.PRODNO
                                    , P.PRODNAME 
                               FROM SIM_GROUP_PROD D,PRODUCT P
                               WHERE D.PRODNO = P.PRODNO(+)
                                 AND D.SIM_GROUP_ID = " + OracleDBUtil.SqlStr(SIM_GROUP_ID) + @" 
                                 AND D.PRODNO = " + OracleDBUtil.SqlStr(PRODNO)
                          );

            if (!string.IsNullOrEmpty(SIM_GROUP_PROD))
            { 
                sb.AppendLine("AND D.SIM_GROUP_PROD <> " + OracleDBUtil.SqlStr(SIM_GROUP_PROD));
            }
            DataTable dtReturn = OracleDBUtil.Query_Data(sb.ToString());
            return dtReturn;
        }

        public DataTable CheckTakeOff_DPData(string PRODNO, string SIM_GROUP_ID, string S_DATE, string E_DATE)
        {

            //**2011/03/08 Tina：註解，因為未失效的卡片群組商品都不能重複
            /*string sqlStr = @"SELECT SIM_GROUP_NAME
                                    FROM SIM_GROUP_PROD p join SIM_GROUP_M m on p.SIM_GROUP_ID = m.SIM_GROUP_ID 
                                    WHERE p.PRODNO = " + OracleDBUtil.SqlStr(PRODNO)+ @" and trunc(m.e_date) >= trunc(sysdate)";
            */

            //**2011/03/22 Tina：1. "編輯"儲存時在判斷是否與其它群組裡的商品重複，要排除自己。
            //                   2. 商品料號不允許跨同一有效卡片群組期間，而非所有未過期的群組。
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(@"
                            SELECT SIM_GROUP_NAME
                            FROM SIM_GROUP_M M, SIM_GROUP_PROD D
                            WHERE M.SIM_GROUP_ID = D.SIM_GROUP_ID 
                            AND D.PRODNO = " + OracleDBUtil.SqlStr(PRODNO) + @"
                            AND MAXDATE(NVL(TO_CHAR(M.S_DATE,'YYYY/MM/DD'),'9999/12/31') , ':SDATE' ) <= MINDATE(NVL(TO_CHAR(M.E_DATE,'YYYY/MM/DD'),'9999/12/31') , ':EDATE' ) 
                            AND ROWNUM = 1
                            AND M.SIM_GROUP_ID <>  " + OracleDBUtil.SqlStr(SIM_GROUP_ID));

            string sE_DATE = (string.IsNullOrEmpty(E_DATE)) ? "9999/12/31" : E_DATE;
            DateTime dStart = Convert.ToDateTime(S_DATE);
            DateTime dStop = Convert.ToDateTime(sE_DATE);

            sb.Replace(":SDATE", dStart.ToString("yyyy/MM/dd"));
            sb.Replace(":EDATE", dStop.ToString("yyyy/MM/dd"));

            DataTable dtReturn = OracleDBUtil.Query_Data(sb.ToString());
            return dtReturn;
        } 

        public DataTable GetTakeOff_GROUPData(string SIM_GROUP_NAME, string S_DATE, string E_DATE)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(" select SIM_GROUP_NAME,S_DATE,E_DATE from SIM_GROUP_M where 1=1 ");
            if (SIM_GROUP_NAME != "")
            {
                sb.Append(" AND SIM_GROUP_NAME = " + OracleDBUtil.SqlStr(SIM_GROUP_NAME));
            }

            DataTable dtReturn = OracleDBUtil.Query_Data(sb.ToString());
            return dtReturn;
        }

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

                        DeleteTakeOff_DData(objTX, key);//刪除表身

                        //一次刪除一筆
                        ORD14_SIM_GROUP.SIM_GROUP_MDataTable dtm = new ORD14_SIM_GROUP.SIM_GROUP_MDataTable();
                        foreach (DataColumn dc in dtm.Columns) dc.AllowDBNull = true;
                        ORD14_SIM_GROUP.SIM_GROUP_MRow drm = dtm.NewSIM_GROUP_MRow();
                        drm.SIM_GROUP_ID = key;
                        dtm.AddSIM_GROUP_MRow(drm);
                        dtm.AcceptChanges();

                        OracleDBUtil.DELETEByUUID(objTX, dtm, "SIM_GROUP_ID");  //刪除表頭
                        //DeleteTakeOff_DData(objTX, key);//刪除表身
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

        public void DeleteTakeOff_DData(OracleTransaction objTX, string SIM_GROUP_ID)
        {
            ORD14_SIM_GROUP.SIM_GROUP_PRODDataTable dtd = new ORD14_SIM_GROUP.SIM_GROUP_PRODDataTable();
            dtd.Rows.Clear();
            foreach (DataColumn dc in dtd.Columns) dc.AllowDBNull = true;

            //表身有可能沒有資料
            if (getSEAL_OFF_STORE_ALL(SIM_GROUP_ID).Rows.Count > 0)
            {
                ORD14_SIM_GROUP.SIM_GROUP_PRODRow drd = dtd.NewSIM_GROUP_PRODRow();
                drd.SIM_GROUP_ID = SIM_GROUP_ID;
                dtd.AddSIM_GROUP_PRODRow(drd);
                dtd.AcceptChanges();
                OracleDBUtil.DELETEByUUID(objTX, dtd, "SIM_GROUP_ID");
            }
        }

        public void DeleteTakeOff_DData(List<object> keyValues)
        {
            OracleConnection objConn = null;
            OracleTransaction objTX = null;
            try
            {
                objConn = OracleDBUtil.GetConnection();
                objTX = objConn.BeginTransaction();

                foreach (string key in keyValues)
                {
                    if (getSEAL_OFF_PROD_D(key).Rows.Count > 0)
                    {
                        //一次刪除一筆
                        ORD14_SIM_GROUP.SIM_GROUP_PRODDataTable dtm = new ORD14_SIM_GROUP.SIM_GROUP_PRODDataTable();
                        foreach (DataColumn dc in dtm.Columns) dc.AllowDBNull = true;
                        ORD14_SIM_GROUP.SIM_GROUP_PRODRow drm = dtm.NewSIM_GROUP_PRODRow();
                        drm.SIM_GROUP_PROD = key;
                        dtm.AddSIM_GROUP_PRODRow(drm);
                        dtm.AcceptChanges();
                        OracleDBUtil.DELETEByUUID(objTX, dtm, "SIM_GROUP_PROD"); //刪除表身
                        //DeleteTakeOff_DData(objTX, key);
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
                objTX.Dispose();
                if (objConn.State == ConnectionState.Open) objConn.Close();
                objConn.Dispose();
                OracleConnection.ClearAllPools();
            }
        }

        private DataTable getSEAL_OFF_STORE_ALL(string SIM_GROUP_ID)
        {
            string sqlStr = @"SELECT * FROM  SIM_GROUP_PROD WHERE SIM_GROUP_ID = " + OracleDBUtil.SqlStr(SIM_GROUP_ID);

            DataTable dtReturn = OracleDBUtil.Query_Data(sqlStr);
            return dtReturn;
        }

        private DataTable getSEAL_OFF_PROD_M(string SIM_GROUP_ID)
        {
            string sqlStr = @"SELECT * FROM  SIM_GROUP_M WHERE SIM_GROUP_ID ='{0}'";
            sqlStr = string.Format(sqlStr, SIM_GROUP_ID);

            DataTable dtReturn = OracleDBUtil.Query_Data(sqlStr);
            return dtReturn;
        }

        private DataTable getSEAL_OFF_PROD_D(string SIM_GROUP_PROD)
        {
            string sqlStr = @"SELECT * FROM  SIM_GROUP_PROD WHERE SIM_GROUP_PROD ='{0}'";
            sqlStr = string.Format(sqlStr, SIM_GROUP_PROD);

            DataTable dtReturn = OracleDBUtil.Query_Data(sqlStr);
            return dtReturn;
        }

        public void UpdateTakeOff_MData(System.Collections.Specialized.OrderedDictionary NewValues, string MODI_USER)
        {
            //表頭資料
            ORD14_SIM_GROUP.SIM_GROUP_MDataTable dtm = new ORD14_SIM_GROUP.SIM_GROUP_MDataTable();
            foreach (DataColumn dc in dtm.Columns) dc.AllowDBNull = true;

            ORD14_SIM_GROUP.SIM_GROUP_MRow drm = dtm.NewSIM_GROUP_MRow();

            drm.SIM_GROUP_ID = NewValues["SIM_GROUP_ID"].ToString();
            drm.SIM_GROUP_NAME = ReplaceInjectionStrig(NewValues["SIM_GROUP_NAME"].ToString());
            drm.S_DATE = Advtek.Utility.DateUtil.NullDateFormat(NewValues["S_DATE"]);
            drm.E_DATE = Advtek.Utility.DateUtil.NullDateFormat(NewValues["E_DATE"]);
            drm.MODI_USER = MODI_USER.ToString();
            drm.MODI_DTM = SysDate;
            dtm.AddSIM_GROUP_MRow(drm);
            dtm.AcceptChanges();
            OracleDBUtil.UPDDATEByUUID(dtm, "SIM_GROUP_ID");
        }

        public void UpdateTakeOff_DData(System.Collections.Specialized.OrderedDictionary NewValues, string MODI_USER)
        {
            //表頭資料
            ORD14_SIM_GROUP.SIM_GROUP_PRODDataTable dtm = new ORD14_SIM_GROUP.SIM_GROUP_PRODDataTable();
            foreach (DataColumn dc in dtm.Columns) dc.AllowDBNull = true;

            ORD14_SIM_GROUP.SIM_GROUP_PRODRow drm = dtm.NewSIM_GROUP_PRODRow();

            drm.SIM_GROUP_PROD = NewValues["SIM_GROUP_PROD"].ToString();
            drm.PRODNO = ReplaceInjectionStrig(NewValues["PRODNO"].ToString());
            drm.MODI_USER = MODI_USER.ToString();
            drm.MODI_DTM = SysDate;
            dtm.AddSIM_GROUP_PRODRow(drm);
            dtm.AcceptChanges();
            OracleDBUtil.UPDDATEByUUID(dtm, "SIM_GROUP_PROD");
        }

        public int GetTakeOff_GROUPData2(string S_DATE, string E_DATE, string PrimaryKey, string SIM_GROUP_NAME)
        {
            int iRet = 0;
            StringBuilder sb = new StringBuilder();
            sb.Append(@"SELECT count(*) FROM SIM_GROUP_M SM
                             WHERE  
                             (
                                 MAXDATE(NVL(TO_CHAR(SM.S_DATE,'YYYY/MM/DD'),'9999/12/31') , ':SDATE' ) 
                                 <= 
                                 MINDATE(NVL(TO_CHAR(SM.E_DATE,'YYYY/MM/DD'),'9999/12/31') , ':EDATE' ) 
                             ) AND SIM_GROUP_NAME=':SIM_GROUP_NAME'");

            string sE_DATE = (string.IsNullOrEmpty(E_DATE)) ? "9999/12/31" : E_DATE;
            DateTime dStart = Convert.ToDateTime(S_DATE);
            DateTime dStop = Convert.ToDateTime(sE_DATE);

            sb.Replace(":SDATE", dStart.ToString("yyyy/MM/dd"));
            sb.Replace(":EDATE", dStop.ToString("yyyy/MM/dd"));

            if (!string.IsNullOrEmpty(SIM_GROUP_NAME))
            {
                sb.Replace(":SIM_GROUP_NAME", ReplaceInjectionStrig(SIM_GROUP_NAME));
            }

            if (!string.IsNullOrEmpty(PrimaryKey))
            {
                sb.Append(" AND SIM_GROUP_ID <> " + OracleDBUtil.SqlStr(PrimaryKey));
            }

            DataTable dtReturn = OracleDBUtil.Query_Data(sb.ToString());
            iRet = Convert.ToInt32(dtReturn.Rows[0][0].ToString());
            return iRet;
        }

        private string ReplaceInjectionStrig(string mString)
        {
            mString = mString.Replace("'", "''");
            //mString = mString.Replace(" ", "");
            mString = mString.Replace(";", "");
            mString = mString.Replace("--", "");
            mString = mString.Replace("|", "");
            mString = mString.Replace("\t", "");
            mString = mString.Replace("\n", "");

            return mString;
        }
    }
}
