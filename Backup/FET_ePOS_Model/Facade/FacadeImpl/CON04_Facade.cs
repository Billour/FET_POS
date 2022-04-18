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
    public class CON04_Facade
    {

        /// <summary>
        /// 查詢寄銷商品作業
        /// </summary>
        /// <param name="strSupplierNo">廠商編號</param>
        /// <param name="strSupplierName">廠商名稱</param>
        /// <param name="strProductCategory">商品類別</param>
        /// <param name="strProductCode">商品料號</param>
        /// <param name="strProductName">商品名稱</param>
        /// <param name="strOrderEndDayS">停止訂購日起</param>
        /// <param name="strOrderEndDayE">停止訂購日迄</param>
        /// <param name="SupportStartDateS">上架日期起</param>
        /// <param name="strSupportStartDateE">上架日期迄</param>
        /// <param name="strSupportExpiryDateS">下架日期起</param>
        /// <param name="strSupportExpiryDateE">下架日期迄</param>
        /// <returns></returns>
        public DataTable Query_CsmSupplierGrid(string strSupplierNo, string strSupplierName,
                             string strProductCategory, string strProductCode, string strProductName,
                             string strOrderEndDayS, string strOrderEndDayE, string strSupportStartDateS,
                             string strSupportStartDateE, string strSupportExpiryDateS, string strSupportExpiryDateE)
        {
            OracleConnection objConn = null;

            try
            {
                StringBuilder sb = new StringBuilder();
                sb.Append(@"  SELECT S.SUPP_NO,S.SUPP_NAME,PD.PRODNO,PD.PRODNAME,PDT.PRODTYPENAME,     
                                TO_CHAR(PD.S_DATE,'YYYY/MM/dd')AS S_DATE,
                                NVL(TO_CHAR(PD.E_DATE,'YYYY/MM/dd'),'9999/12/31')AS E_DATE,  
                                TO_CHAR(TO_DATE(NVL(PD.CEASEDATE,'99991231'),'yyyy/MM/dd'),'yyyy/MM/dd')AS CEASEDATE,
                                E.EMPNAME,     
                                TO_CHAR(PD.MODI_DTM,'YYYY/MM/dd')AS MODI_DTM     
                                FROM PRODUCT PD , CSM_SUPPLIER S         
                                ,CSM_PRODUCT_TYPE PDT         
                                , EMPLOYEE E        
                                WHERE PD.SUPP_ID = S.SUPP_ID  
                                AND  PD.PRODTYPENO = PDT.PRODTYPENO  
                                AND E.EMPNO (+)= PD.MODI_USER      
                                AND PD.COMPANYCODE = '01'         
                                AND PD.ISCONSIGNMENT='1'         
                                AND  PD.DEL_FLAG ='N'   ");
                if (!string.IsNullOrEmpty(strSupplierNo))
                {
                    sb.Append(" AND S.SUPP_NO LIKE " + OracleDBUtil.LikeStr(strSupplierNo));
                }

                if (!string.IsNullOrEmpty(strSupplierName))
                {
                    sb.Append(" AND S.SUPP_NAME LIKE " + OracleDBUtil.LikeStr(strSupplierName));
                }
                if (!string.IsNullOrEmpty(strProductCategory))
                {
                    sb.Append(" AND PD.PRODTYPENO = " + OracleDBUtil.SqlStr(strProductCategory));
                }
                if (!string.IsNullOrEmpty(strProductCode))
                {
                    sb.Append(" AND PD.PRODNO LIKE " + OracleDBUtil.LikeStr(strProductCode));
                }
              
                if (!string.IsNullOrEmpty(strProductName))
                {
                    sb.Append(" AND PD.PRODNAME LIKE " + OracleDBUtil.LikeStr(strProductName));
                }
                if (!string.IsNullOrEmpty(strOrderEndDayS))
                {
                    sb.Append(" AND TO_DATE(PD.CEASEDATE, 'YYYY/MM/DD') >= TO_DATE( " + OracleDBUtil.SqlStr(strOrderEndDayS) + " , 'YYYY/MM/DD')");
                }
                if (!string.IsNullOrEmpty(strOrderEndDayE))
                {
                    sb.Append(" AND TO_DATE(PD.CEASEDATE, 'YYYY/MM/DD') <= TO_DATE( " + OracleDBUtil.SqlStr(strOrderEndDayE) + " , 'YYYY/MM/DD')");
                }
                if (!string.IsNullOrEmpty(strSupportStartDateS))
                {
                    sb.Append(" AND PD.S_DATE >= " + OracleDBUtil.DateStr(strSupportStartDateS));
                }
                if (!string.IsNullOrEmpty(strSupportStartDateE))
                {
                    sb.Append(" AND PD.S_DATE <= " + OracleDBUtil.DateStr(strSupportStartDateE));
                }
                if (!string.IsNullOrEmpty(strSupportExpiryDateS))
                {
                    sb.Append(" AND PD.E_DATE >= " + OracleDBUtil.DateStr(strSupportExpiryDateS));
                }
                if (!string.IsNullOrEmpty(strSupportExpiryDateE))
                {
                    sb.Append(" AND PD.E_DATE <= " + OracleDBUtil.DateStr(strSupportExpiryDateE));
                }
                objConn = OracleDBUtil.GetConnection();
                DataTable dt = OracleDBUtil.GetDataSet(objConn, sb.ToString()).Tables[0];

                return dt;
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
        /// 匯出寄銷商品作業
        /// </summary>
        /// <param name="strSupplierNo">廠商編號</param>
        /// <param name="strSupplierName">廠商名稱</param>
        /// <param name="strProductCategory">商品類別</param>
        /// <param name="strProductCode">商品料號</param>
        /// <param name="strProductName">商品名稱</param>
        /// <param name="strOrderEndDayS">停止訂購日起</param>
        /// <param name="strOrderEndDayE">停止訂購日迄</param>
        /// <param name="SupportStartDateS">上架日期起</param>
        /// <param name="strSupportStartDateE">上架日期迄</param>
        /// <param name="strSupportExpiryDateS">下架日期起</param>
        /// <param name="strSupportExpiryDateE">下架日期迄</param>
        /// <returns></returns>
        public DataTable Export_CsmSupplierGrid(string strSupplierNo, string strSupplierName,
                             string strProductCategory, string strProductCode, string strProductName,
                             string strOrderEndDayS, string strOrderEndDayE, string strSupportStartDateS,
                             string strSupportStartDateE, string strSupportExpiryDateS, string strSupportExpiryDateE)
        {
            OracleConnection objConn = null;

            try
            {
                StringBuilder sb = new StringBuilder();
                sb.Append(@" SELECT S.SUPP_NO,S.SUPP_NAME,PD.PRODNO,PD.PRODNAME,PDT.PRODTYPENAME,     
                                TO_CHAR(PD.S_DATE,'YYYY/MM/dd')AS S_DATE,
                                NVL(TO_CHAR(PD.E_DATE,'YYYY/MM/dd'),'9999/12/31')AS E_DATE,  
                                TO_CHAR(TO_DATE(NVL(PD.CEASEDATE,'99991231'),'yyyy/MM/dd'),'yyyy/MM/dd')AS CEASEDATE,
                                E.EMPNAME,     
                                TO_CHAR(PD.MODI_DTM,'YYYY/MM/dd')AS MODI_DTM     
                                FROM PRODUCT PD , CSM_SUPPLIER S         
                                ,CSM_PRODUCT_TYPE PDT         
                                , EMPLOYEE E        
                                WHERE PD.SUPP_ID = S.SUPP_ID  
                                AND  PD.PRODTYPENO = PDT.PRODTYPENO  
                                AND E.EMPNO (+)= PD.MODI_USER      
                                AND PD.COMPANYCODE = '01'         
                                AND PD.ISCONSIGNMENT='1'         
                                AND  PD.DEL_FLAG ='N'  ");
                if (!string.IsNullOrEmpty(strSupplierNo))
                {
                    sb.Append(" AND S.SUPP_NO LIKE " + OracleDBUtil.LikeStr(strSupplierNo));
                }

                if (!string.IsNullOrEmpty(strSupplierName))
                {
                    sb.Append(" AND S.SUPP_NAME LIKE " + OracleDBUtil.LikeStr(strSupplierName));
                }
                if (!string.IsNullOrEmpty(strProductCategory))
                {
                    sb.Append(" AND PD.PRODTYPENO = " + OracleDBUtil.SqlStr(strProductCategory));
                }
                if (!string.IsNullOrEmpty(strProductCode))
                {
                    sb.Append(" AND PD.PRODNO LIKE " + OracleDBUtil.LikeStr(strProductCode));
                }

                if (!string.IsNullOrEmpty(strProductName))
                {
                    sb.Append(" AND PD.PRODNAME LIKE " + OracleDBUtil.LikeStr(strProductName));
                }
                if (!string.IsNullOrEmpty(strOrderEndDayS))
                {
                    sb.Append(" AND TO_DATE(PD.CEASEDATE, 'YYYY/MM/DD') >= TO_DATE( " + OracleDBUtil.SqlStr(strOrderEndDayS) + " , 'YYYY/MM/DD')");
                }
                if (!string.IsNullOrEmpty(strOrderEndDayE))
                {
                    sb.Append(" AND TO_DATE(PD.CEASEDATE, 'YYYY/MM/DD') <= TO_DATE( " + OracleDBUtil.SqlStr(strOrderEndDayE) + " , 'YYYY/MM/DD')");
                }
                if (!string.IsNullOrEmpty(strSupportStartDateS))
                {
                    sb.Append(" AND PD.S_DATE >= " + OracleDBUtil.DateStr(strSupportStartDateS));
                }
                if (!string.IsNullOrEmpty(strSupportStartDateE))
                {
                    sb.Append(" AND PD.S_DATE <= " + OracleDBUtil.DateStr(strSupportStartDateE));
                }
                if (!string.IsNullOrEmpty(strSupportExpiryDateS))
                {
                    sb.Append(" AND PD.E_DATE >= " + OracleDBUtil.DateStr(strSupportExpiryDateS));
                }
                if (!string.IsNullOrEmpty(strSupportExpiryDateE))
                {
                    sb.Append(" AND PD.E_DATE <= " + OracleDBUtil.DateStr(strSupportExpiryDateE));
                }
                objConn = OracleDBUtil.GetConnection();
                DataTable dt = new DataTable();
                dt = OracleDBUtil.GetDataSet(objConn, sb.ToString()).Tables[0];

                DataTable dt2 = new DataTable();

                dt2.Columns.Add("廠商代碼");
                dt2.Columns.Add("廠商名稱");
                dt2.Columns.Add("商品料號");
                dt2.Columns.Add("商品名稱");
                dt2.Columns.Add("商品類別");
                dt2.Columns.Add("上架日期");
                dt2.Columns.Add("下架日期");
                dt2.Columns.Add("停止訂購日");
                dt2.Columns.Add("更新人員");
                dt2.Columns.Add("更新日期");

                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        DataRow dr2 = dt2.NewRow();
                        dr2[0] = dr["SUPP_NO"];
                        dr2[2] = dr["PRODNO"];
                        dr2[1] = dr["SUPP_NAME"];
                        dr2[3] = dr["PRODNAME"];
                        dr2[4] = dr["PRODTYPENAME"];
                        dr2[5] = dr["S_DATE"];
                        dr2[6] = dr["E_DATE"];
                        dr2[7] = dr["CEASEDATE"];
                        dr2[8] = dr["EMPNAME"];
                        dr2[9] = dr["MODI_DTM"];
                        dt2.Rows.Add(dr2);
                    }
                }
                return dt2;
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
        /// 取得商品價格資料
        /// </summary>
        /// <param name="strSupplierNo">廠商編號</param>
        /// <param name="strProdNo">商品料號</param>
        /// <returns></returns>
        public DataTable QueryCsmProdPrice(string strSupplierNo, string strProdNo)
        { 
         OracleConnection objConn = null;

            try
            {
                StringBuilder sb = new StringBuilder();
                sb.Append(@"  SELECT CPR.PRICE,
                            TO_CHAR(TO_DATE(CPR.S_DATE,'YYYY/MM/DD'),'YYYY/MM/DD') S_DATE,
                            TO_CHAR(TO_DATE(CPR.E_DATE,'YYYY/MM/DD'),'YYYY/MM/DD') E_DATE,
                            TO_CHAR(CPR.MODI_DTM,'YYYY/MM/DD') MODI_DTM
                            FROM CSM_SUPPLIER CS    , CSM_PROD_PRICE CPR    
                            WHERE   CS.SUPP_ID = CPR.SUPP_ID  ");
                if (!string.IsNullOrEmpty(strSupplierNo))
                {
                    sb.Append(" AND CS.SUPP_NO = " + OracleDBUtil.SqlStr(strSupplierNo));
                }

                if (!string.IsNullOrEmpty(strProdNo))
                {
                    sb.Append(" AND CPR.PRODNO = " + OracleDBUtil.SqlStr(strProdNo));
                }
               
                objConn = OracleDBUtil.GetConnection();
                DataTable dt = OracleDBUtil.GetDataSet(objConn, sb.ToString()).Tables[0];

                return dt;
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
        /// 取得會計科目資料
        /// </summary>
        /// <param name="strSupplierNo">廠商ID</param>
        /// <returns></returns>
        public string  QueryCsmAccountCode(string strSupplierId)
        {
            OracleConnection objConn = null;

            try
            {
                string strResult = string.Empty;
                StringBuilder sb = new StringBuilder();
                sb.Append("   SELECT   ACCOUNTCODE ");
                sb.Append("   FROM CSM_SUPPLIER   ");
                sb.Append("  WHERE  0=0   ");
                if (!string.IsNullOrEmpty(strSupplierId))
                {
                    sb.Append(" AND SUPP_ID = " + OracleDBUtil.SqlStr(strSupplierId));
                }

               
                objConn = OracleDBUtil.GetConnection();
                DataTable dt = OracleDBUtil.GetDataSet(objConn, sb.ToString()).Tables[0];
                if (dt.Rows.Count > 0)
                {
                    strResult = dt.Rows[0]["ACCOUNTCODE"].ToString();
                }
                return strResult;
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
        /// 取得佣金比率資料
        /// </summary>
        /// <param name="strSupplierNo">廠商編號</param>
        /// <returns></returns>
        public DataTable QueryCsmProdCommission(string strSupplierNo, string strProdNo)
        {
            OracleConnection objConn = null;

            try
            {

                StringBuilder sb = new StringBuilder();
                sb.Append("   SELECT  CPC.*  ");
                sb.Append("   FROM CSM_SUPPLIER CS , CSM_PROD_COMMISSION CPC  ");
                sb.Append("   WHERE CS.SUPP_ID = CPC.SUPP_ID ");
                sb.Append(" AND CS.SUPP_NO = " + OracleDBUtil.SqlStr(strSupplierNo));
                sb.Append(" AND CPC.PRODNO = " + OracleDBUtil.SqlStr(strProdNo));
                objConn = OracleDBUtil.GetConnection();
                DataTable dt = OracleDBUtil.GetDataSet(objConn, sb.ToString()).Tables[0];

                return dt;
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
        /// 不同佣金比率取得佣金比率重複區間資料
        /// </summary>
        ///  <param name="strCommission">佣金比率</param>
        ///  <param name="strCpcId">PKID</param>
        /// <param name="strSupplierNo">廠商編號</param>
        /// <param name="strProdNo">商品料號</param>
        /// <param name="strSDate">開始日</param>
        /// <param name="strEDate">結束日</param>
        /// <returns></returns>
        public DataTable QueryCsmProdCommission(string strCommission,string strCpcId,string strSupplierNo, string strProdNo, string strSDate, string strEDate)
        {
            OracleConnection objConn = null;

            try
            {
                if (strEDate == "" || strEDate == " ")
                {
                    strEDate = "9999/12";
                }
                StringBuilder sb = new StringBuilder();
                sb.Append("   SELECT  CPC.*  ");
                sb.Append("   FROM CSM_SUPPLIER CS , CSM_PROD_COMMISSION CPC  ");
                sb.Append("   WHERE CS.SUPP_ID = CPC.SUPP_ID ");
                if (!string.IsNullOrEmpty(strCommission))
                {
                    sb.Append(" AND CPC.COMMISSION <> " + OracleDBUtil.SqlStr(strCommission));
                }
                if (!string.IsNullOrEmpty(strSupplierNo))
                {
                    sb.Append(" AND CS.SUPP_NO = " + OracleDBUtil.SqlStr(strSupplierNo));
                }

                if (!string.IsNullOrEmpty(strProdNo))
                {
                    sb.Append(" AND CPC.PRODNO = " + OracleDBUtil.SqlStr(strProdNo));
                }
                if (!string.IsNullOrEmpty(strCpcId))
                {
                    sb.Append(" AND CPC.CPC_ID <> " + OracleDBUtil.SqlStr(strCpcId));
                }
                //sb.Append("  AND( ");
                //sb.Append(" ( TRUNC(REPLACE(" + OracleDBUtil.SqlStr(strSDate) + ",'/','')) > TRUNC(REPLACE(CPC.S_DATE,'/',''))");
                //sb.Append("  AND  TRUNC(REPLACE(" + OracleDBUtil.SqlStr(strSDate) + ",'/','')) < TRUNC(REPLACE(CPC.S_DATE,'/',''))");
                //sb.Append("  )");
                //sb.Append("  OR(");
                //sb.Append("    TRUNC(REPLACE(" + OracleDBUtil.SqlStr(strSDate) + ",'/','')) > TRUNC(REPLACE(CPC.S_DATE,'/','')) ");
                //sb.Append("    AND TRUNC(REPLACE(" + OracleDBUtil.SqlStr(strSDate) + ",'/','')) < TRUNC(REPLACE(CPC.E_DATE,'/',''))");
                //sb.Append(" )");
                //sb.Append(" OR");
                //sb.Append("  (");
                //sb.Append("   TRUNC(REPLACE(CPC.S_DATE,'/','')) >= TRUNC(REPLACE(" + OracleDBUtil.SqlStr(strSDate) + ",'/',''))");
                //sb.Append("   AND TRUNC(REPLACE(CPC.S_DATE,'/','')) <= TRUNC(REPLACE(" + OracleDBUtil.SqlStr(strSDate) + ",'/',''))");
                //sb.Append("   )");
                //sb.Append("  OR (");
                //sb.Append("   TRUNC(REPLACE(CPC.E_DATE,'/','')) > TRUNC(REPLACE(" + OracleDBUtil.SqlStr(strSDate) + ",'/',''))");
                //sb.Append("    AND  TRUNC(REPLACE(CPC.E_DATE,'/','')) < TRUNC(REPLACE(" + OracleDBUtil.SqlStr(strSDate) + ",'/',''))");
                //sb.Append("   )");
                //sb.Append(" )");
                sb.Append(@"  AND (  
                                maxdate(CPC.S_DATE || '/01', " + OracleDBUtil.SqlStr(strSDate + "/01") + ") ");
                sb.Append(@"      <= mindate(nvl(CPC.E_DATE ,'9999/12')|| '/28', " + OracleDBUtil.SqlStr(strEDate + "/28") + ") )");

                objConn = OracleDBUtil.GetConnection();
                DataTable dt = OracleDBUtil.GetDataSet(objConn, sb.ToString()).Tables[0];

                return dt;
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
        /// 存資料至PRODUCT、CSM_PROD_COMMISSION
        /// </summary>
        /// <param name="dtHead">PRODUCT TABLE</param>
        /// <param name="dtBody">CSM_PROD_COMMISSION TABLE</param>
        /// <returns></returns>
        public int InsertCsmProdData(CON04_CSM_PROD.PRODUCTDataTable dtProduct, 
                     CON04_CSM_PROD.CSM_PROD_COMMISSIONDataTable  dtCsmProdCommission)
        {

            int intResult = 0;
            OracleConnection objConn = null;
            OracleTransaction objTx = null;
            try
            {
                objConn = OracleDBUtil.GetConnection();
                objTx = objConn.BeginTransaction();
                if (dtProduct.Rows.Count > 0)
                { 
                intResult += OracleDBUtil.Insert(objTx, dtProduct);
                }
                if (dtCsmProdCommission.Rows.Count > 0)
                { 
                intResult += OracleDBUtil.Insert(objTx, dtCsmProdCommission);
                }
                
                objTx.Commit();
            }

            catch (Exception ex)
            {
                objTx.Rollback();
                throw ex;
            }
            finally
            {
                objTx.Dispose();
                if (objConn.State == ConnectionState.Open) objConn.Close();
                objConn.Dispose();
                OracleConnection.ClearAllPools();
            }
            return intResult;

        }

        /// <summary>
        /// 維護修改時QUERY PRODUCT、CSM_PROD_COMMISSION
        /// </summary>
        /// <param name="strProdNo">商品料號</param>
        /// <returns></returns>
        public DataSet QueryCsmProduct(string strProdNo)
        {
            OracleConnection objConn = null;
            DataSet ds2 = new DataSet();
            DataTable dt = new DataTable();
            DataTable dt2 = new DataTable();
            try
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("   SELECT  CS.SUPP_NO,P.PRODNO,P.PRODNAME,P.PRODTYPENO,P.S_DATE,P.E_DATE,  ");
                sb.Append("   TO_DATE(P.CEASEDATE,'YYYY/mm/dd')AS CEASEDATE,  ");
                sb.Append("   SUBSTR(P.ACCOUNTCODE,1,2)AS ACCT1,  ");
                sb.Append("   SUBSTR(P.ACCOUNTCODE,3,2)AS ACCT2, ");
                sb.Append("   SUBSTR(P.ACCOUNTCODE,5,6)AS ACCT3,");
                sb.Append("   SUBSTR(P.ACCOUNTCODE,11,6)AS ACCT4,");
                sb.Append("   SUBSTR(P.ACCOUNTCODE,17,4)AS ACCT5,");
                sb.Append("   SUBSTR(P.ACCOUNTCODE,21,4)AS ACCT6,");
                sb.Append("   UNIT");
                sb.Append("   FROM PRODUCT P , CSM_SUPPLIER CS  ");
                sb.Append("   WHERE P.SUPP_ID = CS.SUPP_ID");

                if (!string.IsNullOrEmpty(strProdNo))
                {
                    sb.Append(" AND P.PRODNO = " + OracleDBUtil.SqlStr(strProdNo));
                }

                objConn = OracleDBUtil.GetConnection();
                 dt = OracleDBUtil.GetDataSet(objConn, sb.ToString()).Tables[0];
                dt.TableName = "PRODUCT";
               

                StringBuilder sb2 = new StringBuilder();
                sb2.Append("   SELECT CPC_ID,COMMISSION,S_DATE,E_DATE  ");
                sb2.Append("   FROM CSM_PROD_COMMISSION  ");

                if (!string.IsNullOrEmpty(strProdNo))
                {
                    sb2.Append(" WHERE PRODNO = " + OracleDBUtil.SqlStr(strProdNo));
                }

                dt2 = OracleDBUtil.GetDataSet(objConn, sb2.ToString()).Tables[0];
                dt2.TableName = "CSM_PROD_COMMISSION";
               
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
            ds2.Tables.Add(dt.Copy());
           ds2.Tables.Add(dt2.Copy());
            return ds2;
        }

        /// <summary>
        /// Update資料至PRODUCT、CSM_PROD_COMMISSION
        /// </summary>
        /// <param name="dtHead">PRODUCT TABLE</param>
        /// <param name="dtBody">CSM_PROD_COMMISSION TABLE</param>
        /// <returns></returns>
        public int UpdateCsmProdData(CON04_CSM_PROD.PRODUCTDataTable dtProduct,
                     CON04_CSM_PROD.CSM_PROD_COMMISSIONDataTable dtCsmProdCommission,
          CON04_CSM_PROD.CSM_PROD_COMMISSIONDataTable  dtDeleteCommission)
        {

            int intResult = 0;
            OracleConnection objConn = null;
            OracleTransaction objTx = null;
            CON04_CSM_PROD.CSM_PROD_COMMISSIONDataTable dtUpdateCommission = new CON04_CSM_PROD.CSM_PROD_COMMISSIONDataTable();
            CON04_CSM_PROD.CSM_PROD_COMMISSIONDataTable dtInsertCommission1 = new CON04_CSM_PROD.CSM_PROD_COMMISSIONDataTable();
            try
            {
                objConn = OracleDBUtil.GetConnection();
                objTx = objConn.BeginTransaction();

                intResult += OracleDBUtil.UPDDATEByUUID(dtProduct, "PRODNO");
                if (dtCsmProdCommission.Rows.Count > 0)
                {
                    foreach (DataRow dr in dtCsmProdCommission.Rows)
                    {
                       
                        if (dr["CPC_ID"].ToString() == "新增")
                        {
                            DataRow dr2 = dtInsertCommission1.NewCSM_PROD_COMMISSIONRow();
                            dr2 = dr;
                            dr2["CPC_ID"] = GuidNo.getUUID();
                            dtInsertCommission1.ImportRow(dr2);
                        }
                        else
                        {
                            DataRow dr3 = dtUpdateCommission.NewCSM_PROD_COMMISSIONRow();
                            dr3 = dr;
                            dtUpdateCommission.ImportRow(dr3);
                       
                        }
                    }
                }
               
                    intResult += OracleDBUtil.UPDDATEByUUID(dtUpdateCommission, "CPC_ID");
               
                intResult += OracleDBUtil.Insert(objTx,dtInsertCommission1);
               
                intResult += OracleDBUtil.DELETEByUUID(dtDeleteCommission, "CPC_ID");
                
                
                objTx.Commit();
            }

            catch (Exception ex)
            {
                objTx.Rollback();
                throw ex;
            }
            finally
            {
                objTx.Dispose();
                if (objConn.State == ConnectionState.Open) objConn.Close();
                objConn.Dispose();
                OracleConnection.ClearAllPools();
            }
            return intResult;

        }

    

        /// <summary>
        /// Delete資料至PRODUCT、CSM_PROD_COMMISSION
        /// </summary>
        /// <param name="ProdNo"></param>
        /// <returns></returns>
        public int DeleteCsmProdData(string ProdNo)
        {
            //Product DEL_FLAG押Y
            //CSM_PROD_COMMISSION E_DATE 押NOW的YYYY/MM
             int intResult = 0;
            OracleConnection objConn = null;
            OracleTransaction objTx = null;
            StringBuilder sb = new StringBuilder();
            StringBuilder sb2 = new StringBuilder();
           
             try
            {
                objConn = OracleDBUtil.GetConnection();
                objTx = objConn.BeginTransaction();

                sb2.Append(@"SELECT CPC_ID
                            FROM (
                            SELECT  *
                            FROM CSM_PROD_COMMISSION
                            WHERE  PRODNO =" + OracleDBUtil.SqlStr(ProdNo));
                sb2.Append(@"ORDER BY CREATE_DTM DESC)
                            WHERE  rownum   = 1
                            ");
                DataTable dt = OracleDBUtil.Query_Data(sb2.ToString());
                if (dt.Rows.Count > 0)
                {
                    string strCpcId = dt.Rows[0]["CPC_ID"].ToString();
                    if (!string.IsNullOrEmpty(strCpcId))
                    { 
                    StringBuilder sb3 = new StringBuilder();
                    sb3.Append(@"UPDATE CSM_PROD_COMMISSION SET E_DATE= TO_CHAR(SYSDATE,'YYYY/MM')  ");
                    sb3.Append("  WHERE CPC_ID = " + OracleDBUtil.SqlStr(strCpcId));
                    intResult = OracleDBUtil.ExecuteSql(objTx, sb3.ToString());
                    }
                    
                }
                sb.Append(@"UPDATE PRODUCT SET DEL_FLAG= 'Y' ");
                sb.Append(" WHERE PRODNO = " + OracleDBUtil.SqlStr(ProdNo));
                intResult = OracleDBUtil.ExecuteSql(objTx, sb.ToString());

                
                 
              objTx.Commit();
            return intResult;
            }

            catch (Exception ex)
            {
                objTx.Rollback();
                throw ex;
            }
            finally
            {
                objTx.Dispose();
                if (objConn.State == ConnectionState.Open) objConn.Close();
                objConn.Dispose();
                OracleConnection.ClearAllPools();
            }
            
        }

        /// <summary>
        /// 新增excel商品主檔
        /// </summary>
        public static DataTable SP_CHECK_CSM_PRODUCT(string I_BATCH_NO, string I_USER_ID, string I_FINC_ID)
        {
            DataTable O_DATA = new DataTable();
            using (OracleConnection oConn = OracleDBUtil.GetConnection())
            {
                OracleCommand oraCmd = new OracleCommand("SP_CHECK_CSM_PRODUCT");
                oraCmd.CommandType = System.Data.CommandType.StoredProcedure;
                oraCmd.Parameters.Add(new OracleParameter("I_BATCH_NO", OracleType.VarChar, 2000)).Value = I_BATCH_NO;
                oraCmd.Parameters.Add(new OracleParameter("I_USER_ID", OracleType.VarChar, 2000)).Value = I_USER_ID;
                oraCmd.Parameters.Add(new OracleParameter("I_FINC_ID", OracleType.VarChar, 2000)).Value = I_FINC_ID;
                oraCmd.Parameters.Add(new OracleParameter("O_DATA", OracleType.Cursor)).Direction = ParameterDirection.Output;
                oraCmd.Connection = oConn;
                oraCmd.ExecuteNonQuery();
                OracleDataAdapter da = new OracleDataAdapter(oraCmd);
                da.Fill(O_DATA);
                return O_DATA;
            }

        }
        /// <summary>
        /// 新增excel商品佣金比率檔
        /// </summary>
        public static DataTable SP_CHECK_CSM_PROD_COMMISSION(string I_BATCH_NO, string I_USER_ID, string I_FINC_ID, string I_BATCH_NO2, string I_FINC_ID2)
        {
            DataTable O_DATA = new DataTable();
            using (OracleConnection oConn = OracleDBUtil.GetConnection())
            {
                OracleCommand oraCmd = new OracleCommand("SP_CHECK_CSM_PROD_COMMISSION");
                oraCmd.CommandType = System.Data.CommandType.StoredProcedure;
                oraCmd.Parameters.Add(new OracleParameter("I_BATCH_NO", OracleType.VarChar, 2000)).Value = I_BATCH_NO;
                oraCmd.Parameters.Add(new OracleParameter("I_BATCH_NO2", OracleType.VarChar, 2000)).Value = I_BATCH_NO2;
                oraCmd.Parameters.Add(new OracleParameter("I_USER_ID", OracleType.VarChar, 2000)).Value = I_USER_ID;
                oraCmd.Parameters.Add(new OracleParameter("I_FINC_ID", OracleType.VarChar, 2000)).Value = I_FINC_ID;
                oraCmd.Parameters.Add(new OracleParameter("I_FINC_ID2", OracleType.VarChar, 2000)).Value = I_FINC_ID2;
                oraCmd.Parameters.Add(new OracleParameter("O_DATA", OracleType.Cursor)).Direction = ParameterDirection.Output;
                oraCmd.Connection = oConn;
                oraCmd.ExecuteNonQuery();
                OracleDataAdapter da = new OracleDataAdapter(oraCmd);
                da.Fill(O_DATA);
                return O_DATA;
            }

        }
    }
}
