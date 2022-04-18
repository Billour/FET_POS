using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Advtek.Utility;
using System.Collections.Specialized;
using System.Data.OracleClient;

namespace FET.POS.Model.Common
{
    public class RPL_PageHelper : BaseClass
    {
        /// <summary>
        /// 取得調整原因 ("All"/"0")
        /// </summary>
        /// <returns>DataTable</returns>
        public static DataTable GetReason()
        {
            OracleConnection objConn = null;
            DataTable dt = null;

            try
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("SELECT DISTINCT REASONID, REASON FROM REASON WHERE REASONTYPE ='2' ORDER BY REASON ");
                dt = OracleDBUtil.GetDataSet(OracleDBUtil.GetConnection(), strSql.ToString()).Tables[0];

                DataRow dr = dt.NewRow();
                dr["REASON"] = "All";
                dr["REASONID"] = "0";
                dt.Rows.InsertAt(dr, 0);
            }
            catch (Exception ex)
            {
                Logger.Log.Error(ex.Message, ex);
                throw ex;
            }
            //finally
            //{
            //    if (objConn.State == ConnectionState.Open)
            //        objConn.Close();
            //    objConn.Dispose();
            //    OracleConnection.ClearAllPools();
            //}

            return dt;
        }


        /// <summary>
        /// 取得倉別 ("All"/"")
        /// </summary>
        /// <returns>DataTable</returns>
        public static DataTable GetStock()
        {
            OracleConnection objConn = null;
            DataTable dt = null;

            try
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("SELECT LOC_ID, STOCK_NAME FROM LOC ");
                dt = OracleDBUtil.GetDataSet(OracleDBUtil.GetConnection(), strSql.ToString()).Tables[0];

                DataRow dr = dt.NewRow();
                dr["STOCK_NAME"] = "All";
                dr["LOC_ID"] = "";
                dt.Rows.InsertAt(dr, 0);
            }
            catch (Exception ex)
            {
                Logger.Log.Error(ex.Message, ex);
                throw ex;
            }
            //finally
            //{
            //    if (objConn.State == ConnectionState.Open)
            //        objConn.Close();
            //    objConn.Dispose();
            //    OracleConnection.ClearAllPools();
            //}

            return dt;
        }

        /// <summary>
        /// 取得有交易過的促銷代碼 ("All"/"")
        /// <returns>DataTable</returns>
        public static DataTable GetPROMO_NO()
        {
            OracleConnection objConn = null;
            DataTable dt = null;

            try
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("SELECT MM.PROMO_NO, MM.PROMO_NAME ");
                strSql.Append("FROM MM INNER JOIN  SALE_DETAIL SALE ON SALE.PROMPTION_CODE = MM.PROMO_NO ");

                dt = OracleDBUtil.GetDataSet(OracleDBUtil.GetConnection(), strSql.ToString()).Tables[0];

                DataRow dr = dt.NewRow();
                dr["PROMO_NAME"] = "All";
                dr["PROMO_NO"] = "";
                dt.Rows.InsertAt(dr, 0);
            }
            catch (Exception ex)
            {
                Logger.Log.Error(ex.Message, ex);
                throw ex;
            }
            //finally
            //{
            //    if (objConn.State == ConnectionState.Open)
            //        objConn.Close();
            //    objConn.Dispose();
            //    OracleConnection.ClearAllPools();
            //}

            return dt;

        }

        /// <summary>
        /// 取得所有的商品類別 ("All"/"All")
        /// </summary>
        /// <returns>DataTable</returns>
        public static DataTable GetAllProdType()
        {
            OracleConnection objConn = null;
            DataTable dt = null;

            try
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("SELECT PRODTYPENO, PRODTYPENAME FROM PRODUCT_TYPE ");

                dt = OracleDBUtil.GetDataSet(OracleDBUtil.GetConnection(), strSql.ToString()).Tables[0];

                DataRow dr = dt.NewRow();
                dr["PRODTYPENAME"] = "All";
                dr["PRODTYPENO"] = "";
                dt.Rows.InsertAt(dr, 0);
            }
            catch (Exception ex)
            {
                Logger.Log.Error(ex.Message, ex);
                throw ex;
            }
            //finally
            //{
            //    if (objConn.State == ConnectionState.Open)
            //        objConn.Close();
            //    objConn.Dispose();
            //    OracleConnection.ClearAllPools();
            //}

            return dt;
        }

        /// <summary>
        /// 取得有交易過的商品代號 ("All"/"")
        /// <returns>DataTable</returns>
        public static DataTable GetPROD_NO()
        {
            OracleConnection objConn = null;
            DataTable dt = null;

            try
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append(" SELECT PROD.PRODNO AS PROD_NO ,PROD.PRODNAME AS PROD_NAME ");
                strSql.Append("   FROM PRODUCT PROD ,SALE_DETAIL SD                       ");
                strSql.Append("  WHERE 1 = 1                                              ");
                strSql.Append("    AND PROD.PRODNO = SD.PRODNO                            ");

                dt = OracleDBUtil.GetDataSet(OracleDBUtil.GetConnection(), strSql.ToString()).Tables[0];

                DataRow dr = dt.NewRow();
                dr["PROD_NAME"] = "ALL";
                dr["PROD_NO"] = "";
                dt.Rows.InsertAt(dr, 0);
            }
            catch (Exception ex)
            {
                Logger.Log.Error(ex.Message, ex);
                throw ex;
            }
            //finally
            //{
            //    if (objConn.State == ConnectionState.Open)
            //        objConn.Close();
            //    objConn.Dispose();
            //    OracleConnection.ClearAllPools();
            //}

            return dt;

        }

        /// <summary>
        /// 取得信用卡別 ("All"/"")
        /// <returns>DataTable</returns>
        public static DataTable GetCARD_TYPE()
        {
            OracleConnection objConn = null;
            DataTable dt = null;

            try
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append(" SELECT CCT.CREDIT_CARD_TYPE_NO   AS CCT_NO	 ");
                strSql.Append("       ,CCT.CREDIT_CARD_TYPE_NAME AS CCT_NAME ");
                strSql.Append(" FROM CREDIT_CARD_TYPE CCT                    ");
                strSql.Append("  WHERE 1 = 1                                 ");


                dt = OracleDBUtil.GetDataSet(OracleDBUtil.GetConnection(), strSql.ToString()).Tables[0];

                DataRow dr = dt.NewRow();
                dr["CCT_NAME"] = "ALL";
                dr["CCT_NO"] = "ALL";
                dt.Rows.InsertAt(dr, 0);
            }
            catch (Exception ex)
            {
                Logger.Log.Error(ex.Message, ex);
                throw ex;
            }
            //finally
            //{
            //    if (objConn.State == ConnectionState.Open)
            //        objConn.Close();
            //    objConn.Dispose();
            //    OracleConnection.ClearAllPools();
            //}

            return dt;

        }

        /// <summary>
        /// 取得機台號碼 ("All"/"")
        /// <returns>DataTable</returns>
        public static DataTable GetMACHINE_ID(string STORE_NO)
        {
            OracleConnection objConn = null;
            DataTable dt = null;

            try
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append(" SELECT HOST_NO AS MACHINE_ID ,HOST_NAME AS MACHINE_NAME     ");
                strSql.Append("   FROM STORE_TERMINATING_MACHINE    ");
                strSql.Append("  WHERE 1 = 1                        ");
                if (!string.IsNullOrEmpty(STORE_NO))
                {
                    strSql.Append("    AND STORE_NO =               ");
                    strSql.Append(Advtek.Utility.OracleDBUtil.SqlStr(STORE_NO.Trim()));
                }

                dt = OracleDBUtil.GetDataSet(OracleDBUtil.GetConnection(), strSql.ToString()).Tables[0];

                DataRow dr = dt.NewRow();
                dr["MACHINE_NAME"] = "ALL";
                dr["MACHINE_ID"] = "ALL";
                dt.Rows.InsertAt(dr, 0);
            }
            catch (Exception ex)
            {
                Logger.Log.Error(ex.Message, ex);
                throw ex;
            }
            //finally
            //{
            //    if (objConn.State == ConnectionState.Open)
            //        objConn.Close();
            //    objConn.Dispose();
            //    OracleConnection.ClearAllPools();
            //}

            return dt;

        }

        #region 2011/02/09 ADD BY 蔡坤霖

        /// <summary>
        /// 取得區域
        /// </summary>
        /// <returns></returns>
        public static DataTable GetZONE()
        {
            OracleConnection objConn = null;
            DataTable dt = null;

            try
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("SELECT T.ZONE, T.ZONE_NAME ");
                strSql.Append("  FROM ZONE T ");
                strSql.Append(" ORDER BY T.ZONE ");

                dt = OracleDBUtil.GetDataSet(OracleDBUtil.GetConnection(), strSql.ToString()).Tables[0];

                DataRow dr = dt.NewRow();
                dr["ZONE"] = "ALL";
                dr["ZONE_NAME"] = "ALL";
                dt.Rows.InsertAt(dr, 0);
            }
            catch (Exception ex)
            {
                Logger.Log.Error(ex.Message, ex);
                throw ex;
            }
            //finally
            //{
            //    if (objConn.State == ConnectionState.Open)
            //        objConn.Close();
            //    objConn.Dispose();
            //    OracleConnection.ClearAllPools();
            //}

            return dt;
        }

        /// <summary>
        /// 取得商品料號 ("All"/"")
        /// </summary>
        /// <returns>DataTable</returns>
        public static DataTable GetPRODNO()
        {
            OracleConnection objConn = null;
            DataTable dt = null;

            try
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("SELECT T.PRODNO, T.PRODNO || '_' || T.PRODNAME AS PRODNAME ");
                strSql.Append("  FROM PRODUCT T ");
                strSql.Append(" ORDER BY T.PRODNO ");

                dt = OracleDBUtil.GetDataSet(OracleDBUtil.GetConnection(), strSql.ToString()).Tables[0];

                DataRow dr = dt.NewRow();
                dr["PRODNO"] = "ALL";
                dr["PRODNAME"] = "ALL";
                dt.Rows.InsertAt(dr, 0);
            }
            catch (Exception ex)
            {
                Logger.Log.Error(ex.Message, ex);
                throw ex;
            }
            //finally
            //{
            //    if (objConn.State == ConnectionState.Open)
            //        objConn.Close();
            //    objConn.Dispose();
            //    OracleConnection.ClearAllPools();
            //}

            return dt;

        }

        /*Author：蔡坤霖
          Date：100 / 02 / 22
          Description：分期銀行
        */
        public static DataTable GetStagingBank()
        {
            OracleConnection objConn = null;
            DataTable dt = null;

            try
            {
                StringBuilder sb = new StringBuilder();
                sb.AppendFormat("SELECT DISTINCT CCI.BANK_ID, BANK.BANK_NAME \n");
                sb.AppendFormat("  FROM CREDIT_CART_INSTELLMENT CCI, BANK \n");
                sb.AppendFormat(" WHERE CCI.BANK_ID = BANK.BANK_ID(+) \n");
                sb.AppendFormat(" ORDER BY CCI.BANK_ID \n");

                dt = OracleDBUtil.GetDataSet(OracleDBUtil.GetConnection(), sb.ToString()).Tables[0];

                DataRow dr = dt.NewRow();
                dr["BANK_ID"] = "ALL";
                dr["BANK_NAME"] = "ALL";
                dt.Rows.InsertAt(dr, 0);
            }
            catch (Exception ex)
            {
                Logger.Log.Error(ex.Message, ex);
                throw ex;
            }
            //finally
            //{
            //    if (objConn.State == ConnectionState.Open)
            //        objConn.Close();
            //    objConn.Dispose();
            //    OracleConnection.ClearAllPools();
            //}

            return dt;
        }

        /*Author：蔡坤霖
          Date：100 / 02 / 22
          Description：分期期數
        */
        public static DataTable GeInstallmentsQty()
        {
            return GeInstallmentsQty(null);
        }

        /*Author：蔡坤霖
          Date：100 / 02 / 22
          Description：分期期數
        */
        public static DataTable GeInstallmentsQty(string BANK_ID)
        {
            OracleConnection objConn = null;
            DataTable dt = null;

            try
            {
                StringBuilder sb = new StringBuilder();

                sb.AppendFormat("SELECT DISTINCT TO_CHAR(CCI.PAY_SEQMENT) PAY_SEQMENT, CCI.PAY_SEQMENT || '期' PAY_SEQMENT_NAME \n");
                sb.AppendFormat("  FROM CREDIT_CART_INSTELLMENT CCI \n");
                if (!string.IsNullOrEmpty(BANK_ID) && BANK_ID.ToUpper() != "ALL") sb.AppendFormat(" WHERE CCI.BANK_ID = '{0}' \n", BANK_ID);
                sb.AppendFormat(" ORDER BY CCI.PAY_SEQMENT \n");

                dt = OracleDBUtil.GetDataSet(OracleDBUtil.GetConnection(), sb.ToString()).Tables[0];

                DataRow dr = dt.NewRow();
                dr["PAY_SEQMENT"] = "ALL";
                dr["PAY_SEQMENT_NAME"] = "ALL";
                dt.Rows.InsertAt(dr, 0);
            }
            catch (Exception ex)
            {
                Logger.Log.Error(ex.Message, ex);
                throw ex;
            }
            //finally
            //{
            //    if (objConn.State == ConnectionState.Open)
            //        objConn.Close();
            //    objConn.Dispose();
            //    OracleConnection.ClearAllPools();
            //}

            return dt;
        }

        /*Author：蔡坤霖
          Date：100 / 03 / 03
          Description：取得區域
        */
        public static String GetZONE(string STORE_NO)
        {
            OracleConnection objConn = null;
            DataTable dt = null;

            try
            {
                StringBuilder sb = new StringBuilder();

                sb.AppendFormat("SELECT ZONE FROM STORE WHERE STORE_NO = '{0}'", STORE_NO);

                dt = OracleDBUtil.GetDataSet(OracleDBUtil.GetConnection(), sb.ToString()).Tables[0];

                if (dt.Rows.Count > 0)
                {
                    return dt.Rows[0][0].ToString();
                }
                else
                    return "-1";

            }
            catch (Exception ex)
            {
                Logger.Log.Error(ex.Message, ex);
                throw ex;
            }
            //finally
            //{
            //    if (objConn.State == ConnectionState.Open)
            //        objConn.Close();
            //    objConn.Dispose();
            //    OracleConnection.ClearAllPools();
            //}

        }

        #endregion

        #region 2011/3/29 shirley

        /// <summary>
        /// 取得 折抵原因 List
        /// </summary>
        /// <returns>DataTable</returns>
        public DataTable getStoreDISReason()
        {
            OracleConnection objConn = null;
            try
            {
                objConn = OracleDBUtil.GetConnection();
                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                sb.Append(" SELECT STORE_DIS_REASON_ID, STORE_DIS_REASON_DESC");
                sb.Append(" FROM STORE_DIS_REASON ");

                DataTable dt = OracleDBUtil.GetDataSet(objConn, sb.ToString()).Tables[0];

                DataRow dr = dt.NewRow();
                dr["STORE_DIS_REASON_DESC"] = "ALL";
                dr["STORE_DIS_REASON_ID"] = "ALL";
                dt.Rows.InsertAt(dr, 0);

                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            //finally
            //{
            //    if (objConn.State == ConnectionState.Open)
            //        objConn.Close();
            //    objConn.Dispose();
            //    OracleConnection.ClearAllPools();
            //}
        }

        /// <summary>
        /// 取得 維護廠商 List
        /// </summary>
        /// <returns>DataTable</returns>
        public DataTable getStoreMaintenance()
        {
            OracleConnection objConn = null;
            try
            {
                objConn = OracleDBUtil.GetConnection();
                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                sb.Append(" SELECT DISTINCT FIX_BRAND ");
                sb.Append(" FROM HRS_POSFIXI ");

                DataTable dt = OracleDBUtil.GetDataSet(objConn, sb.ToString()).Tables[0];

                DataRow dr = dt.NewRow();
                dr["FIX_BRAND"] = "ALL";
                dt.Rows.InsertAt(dr, 0);

                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            //finally
            //{
            //    if (objConn.State == ConnectionState.Open)
            //        objConn.Close();
            //    objConn.Dispose();
            //    OracleConnection.ClearAllPools();
            //}
        }

        #endregion

        /// <summary>
        /// 取得付款方式 ("All"/"ALL")
        /// </summary>
        /// <returns>DataTable</returns>
        public static DataTable GetPiadMode()
        {
            OracleConnection objConn = null;
            DataTable dt = null;

            try
            {
                StringBuilder strSql = new StringBuilder();
                strSql.AppendFormat(" SELECT DISTINCT TO_CHAR(PAID_MODE) AS PAID_MODE ");
                strSql.AppendFormat("       ,DECODE(PAID_MODE,1,'現金',2,'信用卡',3,'離線信用卡',4,'分期付款',5,'禮券',6,'金融卡',7,'Happy Go',DESCRIPTION) AS PAID_NAME ");
                strSql.AppendFormat("   FROM PAID_DETAIL WHERE PAID_MODE IN (1,2,3,4,5,6,7) ORDER BY PAID_MODE ");

                dt = OracleDBUtil.GetDataSet(OracleDBUtil.GetConnection(), strSql.ToString()).Tables[0];

                DataRow dr = dt.NewRow();
                dr["PAID_MODE"] = "ALL";
                dr["PAID_NAME"] = "ALL";
                dt.Rows.InsertAt(dr, 0);
            }
            catch (Exception ex)
            {
                Logger.Log.Error(ex.Message, ex);
                throw ex;
            }
            //finally
            //{
            //    if (objConn.State == ConnectionState.Open)
            //        objConn.Close();
            //    objConn.Dispose();
            //    OracleConnection.ClearAllPools();
            //}

            return dt;
        }


        /// <summary>
        /// 取得查詢商品類別資料
        /// </summary>
        /// <returns>查詢結果</returns>
        public DataTable GetProductCategory()
        {
            DataTable dt = new DataTable();
            try
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("SELECT CATE_NO, CATE_NAME ");
                sb.Append("FROM PRODUCT_CATEGORY ");


                dt = OracleDBUtil.Query_Data(sb.ToString());
                DataRow dr = dt.NewRow();
                dr["CATE_NO"] = "ALL";
                dr["CATE_NAME"] = "ALL";
                dt.Rows.InsertAt(dr, 0);
            }
            catch (Exception ex)
            {

                Logger.Log.Error(ex.Message, ex);
                throw ex;
            }
            
            return dt;
        }

    }
}
