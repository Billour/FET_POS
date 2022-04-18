using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

using System.Data.OracleClient;
using System.Globalization;

using Advtek.Utility;
using FET.POS.Model.Helper;
using FET.POS.Model.DTO;
using FET.POS.Model.Facade;
using FET.POS.Model.Common; 


namespace FET.POS.Model.Facade.FacadeImpl
{
    public class OPT13_Facade
    {

        #region HG_CONVERT_REST_PROD, HG_CONVERTIBLE_RESTRICTED CRUD OPERATIONS 

        public DataTable Query_HgRestProdAct(string activityNo, string activityName, string sDate, string eDate, string sProdNo, 
            string eProdNo, string prodName)
        {
            return Query_HgRestProdAct(activityNo, activityName, sDate, eDate, sProdNo, eProdNo, prodName, "1");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="activityNo"></param>
        /// <param name="activityName"></param>
        /// <param name="sDate"></param>
        /// <param name="eDate"></param>
        /// <param name="sProdNo"></param>
        /// <param name="eProdNo"></param>
        /// <param name="prodName"></param>
        /// <param name="CONVERT_REST_TYPE">限制類型  1:單項商品 2:指定促銷</param>
        /// <returns></returns>
        public DataTable Query_HgRestProdAct(string activityNo, string activityName,
                                                         string sDate, string eDate,
                                                         string sProdNo, string eProdNo,
                                                         string prodName, string CONVERT_REST_TYPE)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT ROWNUM ITEMNO, a.* FROM ");
            sb.Append("( ");
            sb.Append("SELECT distinct ");
            sb.Append("       HGCR.ACTIVITY_ID, ");
            sb.Append("       HGCR.PAY_OFF_TYPE, ");
            sb.Append("       HGCR.ACTIVITY_NAME, ");
            sb.Append("       HGCR.ACTIVITY_NO, ");
            sb.Append("       HGCR.PRODNO, ");
            sb.Append("       HGCR.PROMO_ID, ");
            sb.Append("       HGCR.S_DATE, ");
            sb.Append("       HGCR.E_DATE, ");
            sb.Append("       DECODE(HGCR.MEMBER_CHECK_FLAG,'Y','True','N','False','False') MEMBER_CHECK_FLAG, ");
            sb.Append("       HGCR.U_BOUND, ");
            sb.Append("       (CASE WHEN TO_CHAR(HGCR.USE_COUNT)='0' THEN '' ELSE TO_CHAR(HGCR.USE_COUNT) END) as USE_COUNT, ");
            sb.Append("       HGCR.CREATE_USER, ");
            sb.Append("       HGCR.CREATE_DTM, ");
            sb.Append("       EMP.EMPNAME MODI_USER, ");
            sb.Append("       TO_CHAR(HGCR.MODI_DTM, 'yyyy/mm/dd hh24:mi:ss') MODI_DTM, ");
            sb.Append("       HGCR.CONVERT_REST_TYPE, ");
            sb.Append("       HGPOT.PAY_OFF_TYPE_NAME, ");
            sb.Append("       HGCRT.CONVERT_REST_TYPE_NAME ");
            sb.Append("FROM   HG_CONVERTIBLE_RESTRICTED HGCR ");
            sb.Append("       INNER JOIN HG_PAY_OFF_TYPE HGPOT ON HGCR.PAY_OFF_TYPE = HGPOT.PAY_OFF_TYPE ");
            sb.Append("       LEFT JOIN HG_CONVERT_REST_TYPE HGCRT ON HGCR.CONVERT_REST_TYPE = HGCRT.CONVERT_REST_TYPE ");
            sb.Append("       INNER JOIN PRODUCT PROD ON HGCR.PRODNO = PROD.PRODNO ");
            sb.Append("       AND PROD.DEL_FLAG = 'N'  ");
            sb.Append("       AND PROD.COMPANYCODE = '01' ");
            sb.Append("       INNER JOIN EMPLOYEE EMP ON HGCR.MODI_USER = EMP.EMPNO ");
            if (CONVERT_REST_TYPE == "2")
            {
                sb.Append("   LEFT JOIN HG_CONVERT_REST_MM MM  on HGCR.ACTIVITY_ID = MM.ACTIVITY_ID ");
            }
            else
            {
                sb.Append("   LEFT JOIN HG_CONVERT_REST_PROD HGPROD on HGCR.ACTIVITY_ID = HGPROD.ACTIVITY_ID ");
                sb.Append("   LEFT JOIN PRODUCT PROD2 ON HGPROD.PRODNO = PROD2.PRODNO ");

            }
            sb.Append("WHERE  1 = 1 ");

            if (!string.IsNullOrEmpty(activityNo))
            {
                sb.Append(" AND HGCR.ACTIVITY_NO = " + OracleDBUtil.SqlStr(activityNo));
            }

            if (!string.IsNullOrEmpty(activityName))
            {
                sb.Append(" AND HGCR.ACTIVITY_NAME like " + OracleDBUtil.LikeStr(activityName));
            }

            if (!string.IsNullOrEmpty(sDate) && !string.IsNullOrEmpty(eDate))
            {
                sb.Append(" AND HGCR.S_DATE >= " + OracleDBUtil.TimeStr(sDate));
                sb.Append(" AND HGCR.S_DATE <= " + OracleDBUtil.TimeStr(eDate));
            }

            if (!string.IsNullOrEmpty(sDate) && string.IsNullOrEmpty(eDate))
            {
                sb.Append(" AND HGCR.S_DATE >= " + OracleDBUtil.TimeStr(sDate));
            }

            if ((!string.IsNullOrEmpty(eDate)) && string.IsNullOrEmpty(sDate))
            {
                sb.Append(" AND HGCR.S_DATE <= " + OracleDBUtil.TimeStr(eDate));
            }
            if (CONVERT_REST_TYPE == "2")
            {
                if (!(string.IsNullOrEmpty(sProdNo)))
                {
                    sb.Append(" AND MM.PROMOTE_CODE >= " + OracleDBUtil.SqlStr(sProdNo));
                }

                if (!(string.IsNullOrEmpty(eProdNo)))
                {
                    sb.Append(" AND MM.PROMOTE_CODE <= " + OracleDBUtil.SqlStr(eProdNo));
                }

                if (!string.IsNullOrEmpty(prodName))
                {
                    sb.Append(" AND MM.PROMOTE_NAME like " + OracleDBUtil.LikeStr(prodName));
                }
            }
            else
            {
                if (!(string.IsNullOrEmpty(sProdNo)))
                {
                    sb.Append(" AND HGPROD.PRODNO >= " + OracleDBUtil.SqlStr(sProdNo));
                }

                if (!(string.IsNullOrEmpty(eProdNo)))
                {
                    sb.Append(" AND HGPROD.PRODNO <= " + OracleDBUtil.SqlStr(eProdNo));
                }

                if (!string.IsNullOrEmpty(prodName))
                {
                    sb.Append(" AND PROD2.PRODNAME like " + OracleDBUtil.LikeStr(prodName));
                }
            }

            //限制類型
            if (!string.IsNullOrEmpty(CONVERT_REST_TYPE))
            {
                sb.Append(" AND HGCR.CONVERT_REST_TYPE = " + CONVERT_REST_TYPE);
            }

            sb.Append(" ORDER BY ACTIVITY_NO ) a ");

            DataTable dt = OracleDBUtil.Query_Data(sb.ToString());
            return dt;
        }

        public static int Query_HgRestProdAct_ByProdNo(string activityNo, string sDate, string eDate, string SID, string CONVERT_REST_TYPE)
        {
            string strEDate = "9999/12/31";
            if (!string.IsNullOrEmpty(eDate))
            {
                strEDate = Convert.ToDateTime(eDate).ToString("yyyy/MM/dd");
            }

            string strSDate = Convert.ToDateTime(sDate).ToString("yyyy/MM/dd");

            StringBuilder sb = new StringBuilder();
            sb.Append(" SELECT * ");
            sb.Append(" FROM   HG_CONVERTIBLE_RESTRICTED HGCR,HG_PAY_OFF_TYPE HGPOT, HG_CONVERT_REST_TYPE HGCRT, PRODUCT PROD");
            sb.Append(" WHERE HGCR.PAY_OFF_TYPE = HGPOT.PAY_OFF_TYPE AND HGCRT.CONVERT_REST_TYPE = HGCR.CONVERT_REST_TYPE(+) AND HGCR.PRODNO = PROD.PRODNO");
            sb.Append(" AND PROD.DEL_FLAG = 'N' ");
            sb.Append(" AND PROD.COMPANYCODE = '01' ");
            sb.Append(" AND HGCR.CONVERT_REST_TYPE = " + OracleDBUtil.SqlStr(CONVERT_REST_TYPE));
            sb.Append(" AND HGCR.ACTIVITY_NO = " + OracleDBUtil.SqlStr(activityNo));
            sb.Append(" AND (" + OracleDBUtil.DateStr(strSDate) + " BETWEEN HGCR.S_DATE AND HGCR.E_DATE ");
            sb.Append(" OR " + OracleDBUtil.DateStr(strEDate) + " BETWEEN HGCR.S_DATE AND HGCR.E_DATE ");
            sb.Append(" OR HGCR.S_DATE BETWEEN " + OracleDBUtil.DateStr(strSDate) + " AND " + OracleDBUtil.DateStr(strEDate));
            sb.Append(" OR HGCR.E_DATE BETWEEN " + OracleDBUtil.DateStr(strSDate) + " AND " + OracleDBUtil.DateStr(strEDate) + " )");
            if (!string.IsNullOrEmpty(SID))
                sb.Append("AND ACTIVITY_ID <> " + OracleDBUtil.SqlStr(SID));

            DataTable dt = OracleDBUtil.Query_Data(sb.ToString());
            return dt.Rows.Count;
        }

        public static int Query_HgRestExchange(string activityNo, int dividablePoint, string SID)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(" select * from HG_CONVERT_REST_EXCHANGE ");
            sb.Append(" where ACTIVITY_ID = " + OracleDBUtil.SqlStr(activityNo) + " and DIVIDABLE_POINT = " + dividablePoint);
            if (!string.IsNullOrEmpty(SID))
                sb.Append(" and SID <> " + OracleDBUtil.SqlStr(SID));

            DataTable dt = OracleDBUtil.Query_Data(sb.ToString());
            return dt.Rows.Count;
        }

        public static int Query_HgExtraSale(string activityNo, string prodno, string SID)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(" select * from HG_CONVERT_EXTRA_SALE ");
            sb.Append(" where ACTIVITY_ID = " + OracleDBUtil.SqlStr(activityNo) + " and PRODNO = " + OracleDBUtil.SqlStr(prodno));
            if (!string.IsNullOrEmpty(SID))
                sb.Append(" and SID <> " + OracleDBUtil.SqlStr(SID));

            DataTable dt = OracleDBUtil.Query_Data(sb.ToString());
            return dt.Rows.Count;
        }

        public static int Query_HgRestMM(string activityNo, string promoteCode, string SID)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(" select * from HG_CONVERT_REST_MM ");
            sb.Append(" where ACTIVITY_ID = " + OracleDBUtil.SqlStr(activityNo) + " and PROMOTE_CODE = " + OracleDBUtil.SqlStr(promoteCode));
            if (!string.IsNullOrEmpty(SID))
                sb.Append(" and SID <> " + OracleDBUtil.SqlStr(SID));

            DataTable dt = OracleDBUtil.Query_Data(sb.ToString());
            return dt.Rows.Count;
        }

        public static int Query_HgRestPROD(string activityNo, string prodNo, string SID)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(" select * from HG_CONVERT_REST_PROD ");
            sb.Append(" where ACTIVITY_ID = " + OracleDBUtil.SqlStr(activityNo) + " and PRODNO = " + OracleDBUtil.SqlStr(prodNo));
            if (!string.IsNullOrEmpty(SID))
                sb.Append(" and SID <> " + OracleDBUtil.SqlStr(SID));

            DataTable dt = OracleDBUtil.Query_Data(sb.ToString());
            return dt.Rows.Count;
        }

        public static int Query_HgRestStore(string activityNo, string storeNo, string SID)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(" select * from HG_CONVERT_REST_STORE ");
            sb.Append(" where ACTIVITY_ID = " + OracleDBUtil.SqlStr(activityNo) + " and STORE_NO = " + OracleDBUtil.SqlStr(storeNo));
            if (!string.IsNullOrEmpty(SID))
                sb.Append(" and SID <> " + OracleDBUtil.SqlStr(SID));

            DataTable dt = OracleDBUtil.Query_Data(sb.ToString());
            return dt.Rows.Count;
        }

        public static int Query_HgMemberList(string activityNo)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("select * from HG_CONVERT_MEMBER_LIST ");
            sb.Append("where ACTIVITY_ID = " + OracleDBUtil.SqlStr(activityNo));

            DataTable dt = OracleDBUtil.Query_Data(sb.ToString());
            return dt.Rows.Count;
        }

        public void AddNewOne_HgRestProdAct(OPT13_HgConvertibleRestricted_DTO ds)
        {
            OracleConnection objConn = null;
            OracleTransaction objTX = null;

            try
            {
                objConn = OracleDBUtil.GetConnection();
                objTX = objConn.BeginTransaction();

                OracleDBUtil.Insert(objTX, ds.HG_CONVERTIBLE_RESTRICTED);          

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

        public void UpdateOne_HgRestProdAct(OPT13_HgConvertibleRestricted_DTO ds)
        {
            OracleConnection objConn = null;
            OracleTransaction objTX = null;

            try
            {
                objConn = OracleDBUtil.GetConnection();
                objTX = objConn.BeginTransaction();

                OracleDBUtil.UPDDATEByUUID_IncludeDBNull(objTX, ds.HG_CONVERTIBLE_RESTRICTED, "ACTIVITY_ID");                 

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

        public void DeleteOne_HgRestProdAct(DataSet ds, string CONVERT_REST_TYPE)
        {
            OracleConnection objConn = null;
            OracleTransaction objTX = null;

            try
            {
                objConn = OracleDBUtil.GetConnection();
                objTX = objConn.BeginTransaction();

                DELETEByACTIVITYID(objTX, ds.Tables["HG_CONVERTIBLE_RESTRICTED"], "ACTIVITY_ID");
                DELETEByACTIVITYID(objTX, ds.Tables["HG_CONVERT_REST_EXCHANGE"], "ACTIVITY_ID");
                DELETEByACTIVITYID(objTX, ds.Tables["HG_CONVERT_REST_STORE"], "ACTIVITY_ID");
                DELETEByACTIVITYID(objTX, ds.Tables["HG_CONVERT_EXTRA_SALE"], "ACTIVITY_ID");
                if (CONVERT_REST_TYPE == "1")
                    DELETEByACTIVITYID(objTX, ds.Tables["HG_CONVERT_REST_PROD"], "ACTIVITY_ID");
                else
                    DELETEByACTIVITYID(objTX, ds.Tables["HG_CONVERT_REST_MM"], "ACTIVITY_ID");
                DELETEByACTIVITYID(objTX, ds.Tables["HG_CONVERT_MEMBER_LIST"], "ACTIVITY_ID");

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

        public int DELETEByACTIVITYID(OracleTransaction objTX, DataTable _DT, string _strUUIDField)
        {
            int intResult = 0;
            StringBuilder _sbScript;

            try
            {
                foreach (DataRow dr in _DT.Rows)
                {
                    if (GetRowCount(_DT.TableName.ToString(), _strUUIDField, OracleDBUtil.SqlStr(StringUtil.CStr(dr[_strUUIDField]))).Rows.Count > 0)
                    {
                        _sbScript = new StringBuilder();
                        _sbScript.Append(" Delete " + _DT.TableName.ToString());
                        _sbScript.Append("  Where  " + _strUUIDField + "=" + OracleDBUtil.SqlStr(StringUtil.CStr(dr[_strUUIDField])));

                        intResult = OracleDBUtil.ExecuteSql(objTX, _sbScript.ToString());
                        if (intResult == 0) throw new Exception("DELETE SQL Execute 失敗. ");
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {

            }
            return intResult;
        }

        #endregion

        public DataTable GetRowCount(string TableName, string FieldName, string FieldValue)
        {
            string sqlStr = @"SELECT * FROM  " + TableName + " WHERE " + FieldName + " = {0}";
            sqlStr = string.Format(sqlStr, FieldValue);
            return OracleDBUtil.Query_Data(sqlStr);
        }

        /// <summary>
        /// 取得促銷代號限制資料
        /// </summary>
        /// <param name="activityId">活動代碼</param>
        /// <returns></returns>
        public DataTable Query_HgConvertRestMM(string activityId)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(" select * from HG_CONVERT_REST_MM where ACTIVITY_ID=" + OracleDBUtil.SqlStr(activityId));
            sb.Append(" ORDER BY PROMOTE_CODE ");

            DataTable dt = OracleDBUtil.Query_Data(sb.ToString());
            return dt;
        }

        public static DataTable Query_MMData_ById(string promoNo)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(@" select * from MM where PROMO_NO= '" + promoNo + "' ");

            DataTable dt = OracleDBUtil.Query_Data(sb.ToString());
            return dt;
        }

        public DataTable Query_Flag(string ID)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(@" select MEMBER_CHECK_FLAG from HG_CONVERTIBLE_RESTRICTED where ACTIVITY_ID=" + OracleDBUtil.SqlStr(ID));

            DataTable dt = OracleDBUtil.Query_Data(sb.ToString());
            return dt;
        }

        public DataTable Query_HgConvertRest(string ID)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(@" select ACTIVITY_NAME from HG_CONVERTIBLE_RESTRICTED where ACTIVITY_NO=" + OracleDBUtil.SqlStr(ID));

            DataTable dt = OracleDBUtil.Query_Data(sb.ToString());
            return dt;
        }

        public void Insert_HgConvertRestMM(OPT13_HgConvertibleRestricted_DTO ds)
        {
           OracleDBUtil.Insert(ds.HG_CONVERT_REST_MM);
        }

        public void Delete_HgConvertRestMM(DataTable dt)
        {
            OracleConnection objConn = null;
            OracleTransaction objTX = null;

            try
            {
                objConn = OracleDBUtil.GetConnection();
                objTX = objConn.BeginTransaction();

                DELETEByACTIVITYID(objTX, dt, "SID");

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

        public void Update_HgConvertRestMM(OPT13_HgConvertibleRestricted_DTO ds)
        {
            OPT13_HgConvertibleRestricted_DTO.HG_CONVERT_REST_MMDataTable dt = ds.HG_CONVERT_REST_MM;
            OracleDBUtil.UPDDATEByUUID(dt, dt.PrimaryKey[0].ColumnName);
        }

        #region HG_CONVERT_REST_PROD CRUD OPERATIONS

        public DataTable Query_HgConvertDisActProduct(string activityId)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT ROWNUM ITEMNO, a.* FROM ");
            sb.Append("( ");
            sb.Append("SELECT HGCRP.SID, ");
            sb.Append("       PROD.PRODNO, ");
            sb.Append("       PROD.PRODNAME, ");
            sb.Append("       PROD.UNIT, ");
            sb.Append("       PROD.CEASEDATE, ");
            sb.Append("       PROD.ISKEY, ");
            sb.Append("       PROD.TOSO, ");
            sb.Append("       PROD.ISCONSIGNMENT, ");
            sb.Append("       PROD.PRICE, ");
            sb.Append("       PROD.PRICEDATE, ");
            sb.Append("       PROD.BARCODE1, ");
            sb.Append("       PROD.COST, ");
            sb.Append("       PROD.BARCODE2, ");
            sb.Append("       PROD.COSTDATE, ");
            sb.Append("       PROD.BARCODE3, ");
            sb.Append("       PROD.BARCODE4, ");
            sb.Append("       PROD.ULSNO, ");
            sb.Append("       PROD.STATUS, ");
            sb.Append("       PROD.ACCOUNTCODE, ");
            sb.Append("       PROD.ISSTOCK, ");
            sb.Append("       PROD.IMEI_FLAG, ");
            sb.Append("       PROD.PRODTYPENO, ");
            sb.Append("       PROD.EFFECT_DATE, ");
            sb.Append("       PROD.ATTRI_ID, ");
            sb.Append("       PROD.IS_POS_DEF_PRICE, ");
            sb.Append("       HGCRP.CREATE_USER, ");
            sb.Append("       HGCRP.MODI_USER, ");
            sb.Append("       HGCRP.MODI_DTM, ");
            sb.Append("       HGCRP.CREATE_DTM, ");
            sb.Append("       PROD.ERP_ATTRIBUTE_1, ");
            sb.Append("       PROD.ERP_ATTRIBUTE_2, ");
            sb.Append("       PROD.IS_DROPSHIPMENT, ");
            sb.Append("       PROD.IS_DISCOUNT, ");
            sb.Append("       PROD.SUPP_ID, ");
            sb.Append("       PROD.S_DATE, ");
            sb.Append("       PROD.E_DATE, ");
            sb.Append("       PROD.COMPANYCODE ");
            sb.Append("FROM   HG_CONVERT_REST_PROD HGCRP, PRODUCT PROD ");
            sb.Append("WHERE  HGCRP.PRODNO = PROD.PRODNO(+) ");
            sb.Append("       AND PROD.DEL_FLAG = 'N'  ");
            sb.Append("       AND PROD.COMPANYCODE = '01' ");

            if (!string.IsNullOrEmpty(activityId))
            {
                sb.Append(" AND HGCRP.ACTIVITY_ID = " + OracleDBUtil.SqlStr(activityId));
            }
            sb.Append(" ORDER BY PROD.PRODNO ) a");

            DataTable dt = OracleDBUtil.Query_Data(sb.ToString());
            return dt;
        }

        public void AddNewOne_HgConvertDisActProduct(OPT13_HgConvertibleRestricted_DTO ds)
        {
            OracleDBUtil.Insert(ds.HG_CONVERT_REST_PROD);
        }

        public void DeleteOne_HgConvertDisActProduct(DataTable dt)
        {
            OracleConnection objConn = null;
            OracleTransaction objTX = null;

            try
            {
                objConn = OracleDBUtil.GetConnection();
                objTX = objConn.BeginTransaction();

                DELETEByACTIVITYID(objTX, dt, "SID");

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

        public void UpdateOne_HgConvertDisActProduct(OPT13_HgConvertibleRestricted_DTO ds)
        {
            OracleDBUtil.UPDDATEByUUID(ds.HG_CONVERT_REST_PROD, "SID");
        }

        #endregion


        #region HG_CONVERT_EXTRA_SALE CRUD OPERATIONS

        public DataTable Query_HgConvertExtraSale(string activityId)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT ROWNUM ITEMNO, a.* FROM ");
            sb.Append("( ");
            sb.Append("SELECT PROD.PRODNAME, ");
            sb.Append("       HGCES.SID, ");
            sb.Append("       HGCES.ACTIVITY_ID, ");
            sb.Append("       HGCES.PRODNO, ");
            sb.Append("       HGCES.DIVIDABLE_POINT, ");
            sb.Append("       HGCES.EXTRA_SALE_PRICE, ");
            sb.Append("       HGCES.CREATE_USER, ");
            sb.Append("       HGCES.CREATE_DTM, ");
            sb.Append("       HGCES.MODI_USER, ");
            sb.Append("       HGCES.MODI_DTM ");
            sb.Append("FROM   HG_CONVERT_EXTRA_SALE HGCES,PRODUCT PROD ");
            sb.Append(" WHERE HGCES.PRODNO = PROD.PRODNO ");
            sb.Append("       AND PROD.DEL_FLAG = 'N'  ");
            sb.Append("       AND PROD.COMPANYCODE = '01' ");

            if (!string.IsNullOrEmpty(activityId))
            {
                sb.Append(" AND HGCES.ACTIVITY_ID = " + OracleDBUtil.SqlStr(activityId));
            }
            sb.Append(" ORDER BY HGCES.PRODNO ) a");

            DataTable dt = OracleDBUtil.Query_Data(sb.ToString());
            return dt;

        }

        public void AddNewOne_HgConvertExtraSale(OPT13_HgConvertibleRestricted_DTO ds)
        {
            OracleDBUtil.Insert(ds.HG_CONVERT_EXTRA_SALE);
        }

        public void UpdateOne_HgConvertExtraSale(OPT13_HgConvertibleRestricted_DTO ds)
        {
            OracleDBUtil.UPDDATEByUUID(ds.HG_CONVERT_EXTRA_SALE, "SID");
        }

        public void DeleteOne_HgConvertExtraSale(DataTable dt)
        {
            OracleConnection objConn = null;
            OracleTransaction objTX = null;

            try
            {
                objConn = OracleDBUtil.GetConnection();
                objTX = objConn.BeginTransaction();

                DELETEByACTIVITYID(objTX, dt, "SID");

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

        #endregion 


        #region HG_CONVERT_REST_STORE CRUD OPERATIONS

        public DataTable Query_HgConvertRestStore(string activityId)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT ROWNUM ITEMNO, a.* FROM ");
            sb.Append("( ");
            sb.Append("SELECT HGCRS.SID, ");
            sb.Append("       HGCRS.ACTIVITY_ID, ");
            sb.Append("       HGCRS.CREATE_USER, ");
            sb.Append("       HGCRS.CREATE_DTM, ");
            sb.Append("       HGCRS.MODI_USER, ");
            sb.Append("       HGCRS.MODI_DTM, ");
            sb.Append("       HGCRS.STORE_NO, ");
            sb.Append("       STORE.STORENAME, ");
            sb.Append("       ZONE.ZONE_NAME ");
            sb.Append("FROM   HG_CONVERT_REST_STORE HGCRS, STORE, ZONE ");
            sb.Append("WHERE  HGCRS.STORE_NO = STORE.STORE_NO AND  STORE.ZONE = ZONE.ZONE ");

            if (!string.IsNullOrEmpty(activityId))
            {
                sb.Append(" AND HGCRS.ACTIVITY_ID = " + OracleDBUtil.SqlStr(activityId));
            }
            sb.Append(" ORDER BY HGCRS.STORE_NO ) a");

            DataTable dt = OracleDBUtil.Query_Data(sb.ToString());
            return dt;
        }

        public void AddNewOne_HgConvertRestStore(OPT13_HgConvertibleRestricted_DTO ds)
        {
            OracleDBUtil.Insert(ds.HG_CONVERT_REST_STORE);
        }

        public void UpdateOne_HgConvertRestStore(OPT13_HgConvertibleRestricted_DTO ds)
        {
            OracleDBUtil.UPDDATEByUUID(ds.HG_CONVERT_REST_STORE, "SID");
        }

        public void DeleteOne_HgConvertRestStore(DataTable dt)
        {
            OracleConnection objConn = null;
            OracleTransaction objTX = null;

            try
            {
                objConn = OracleDBUtil.GetConnection();
                objTX = objConn.BeginTransaction();

                DELETEByACTIVITYID(objTX, dt, "SID");

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

        #endregion 


        #region HG_CONVERT_REST_EXCHANGE CRUD OPERATIONS

        public DataTable Query_HgConvertRestExchange(string activityId)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT ROWNUM ITEMNO, ");
            sb.Append("       SID, ");
            sb.Append("       ACTIVITY_ID, ");
            sb.Append("       EXCHANGE_NAME, ");
            sb.Append("       DIVIDABLE_POINT, ");
            sb.Append("       CONVERT_CURRENCY, ");
            sb.Append("       CREATE_USER, ");
            sb.Append("       CREATE_DTM, ");
            sb.Append("       MODI_USER, ");
            sb.Append("       MODI_DTM ");
            sb.Append("FROM   HG_CONVERT_REST_EXCHANGE ");
            sb.Append("WHERE  1 = 1 ");

            if (!string.IsNullOrEmpty(activityId))
            {
                sb.Append(" AND ACTIVITY_ID = " + OracleDBUtil.SqlStr(activityId));
            }

            DataTable dt = OracleDBUtil.Query_Data(sb.ToString());
            return dt;

        }

        public void AddNewOne_HgConvertRestExchange(OPT13_HgConvertibleRestricted_DTO ds)
        {
            OracleDBUtil.Insert(ds.HG_CONVERT_REST_EXCHANGE);
        }

        public void UpdateOne_HgConvertRestExchange(OPT13_HgConvertibleRestricted_DTO ds)
        {
            OracleDBUtil.UPDDATEByUUID(ds.HG_CONVERT_REST_EXCHANGE, "SID");
        }

        public void DeleteOne_HgConvertRestExchange(DataTable dt)
        {
            OracleConnection objConn = null;
            OracleTransaction objTX = null;

            try
            {
                objConn = OracleDBUtil.GetConnection();
                objTX = objConn.BeginTransaction();

                DELETEByACTIVITYID(objTX, dt, "SID");

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

        #endregion 
 
    }
}
