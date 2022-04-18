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
    public class OPT10_Facade
    {

        public DataTable Query_ProductMethodSet(string ProductCategory, string ProductCode, string ProductName, string VerifyImei, string Status)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(@"SELECT * 
                            FROM ( 
                            SELECT CASE WHEN (TRUNC(SYSDATE) >= TRUNC(S_DATE) AND TRUNC(SYSDATE) <= TRUNC(E_DATE)) THEN '有效' WHEN(TRUNC(SYSDATE) > TRUNC(E_DATE)) THEN '過期' WHEN(TRUNC(SYSDATE) < TRUNC(S_DATE)) THEN '尚未生效' ELSE '有效' END STATUS 
                                    , PRD.PRODNO AS PRODNO    
                                    , PRD.PRODNAME AS PRODNAME    
                                    , PRD.PRODTYPENO AS PRODTYPENO 
                                    , PRDT.PRODTYPENAME AS PRODTYPENAME 
                                    , PRD.UNIT AS UNIT 
                                    , PRD.PRICE AS PRICE   
                                    , PRD.S_DATE               
                                    , PRD.E_DATE                
                                    , DECODE(PRD.ISSTOCK,'Y','True','N','False','False') AS ISSTOCK     
                                    , PRD.IMEI_FLAG AS IMEI_FLAG     
                                    , CHKI.CHECK_IMEI_TYPE_NAME AS CHECK_IMEI_TYPE_NAME  
                                    , DECODE(PRD.IS_POS_DEF_PRICE,'Y','True','N','False','False') AS IS_POS_DEF_PRICE 
                                    , DECODE(PRD.IS_OPEN_PRICE,'Y','True','N','False','False') AS IS_OPEN_PRICE 
                                    , substr(PRD.ACCOUNTCODE,0,2) ACC1 ,substr(PRD.ACCOUNTCODE,3,3) ACC2 
                                    , substr(PRD.ACCOUNTCODE,6,4) ACC3 ,substr(PRD.ACCOUNTCODE,10,6) ACC4     
                                    , substr(PRD.ACCOUNTCODE,16,4) ACC5,substr(PRD.ACCOUNTCODE,20,4) ACC6  
                                    , TO_CHAR(PRD.MODI_DTM,'YYYY/MM/DD HH:mi:ss') AS MODI_DTM        
                                    , PRD.MODI_USER AS MODI_USER    
                                    , EMP.EMPNAME AS MODI_USER_NAME  
                                    , TO_CHAR(PRD.CREATE_DTM,'YYYY/MM/DD HH:mi:ss') AS CREATE_DTM   
                                    , PRD.CREATE_USER AS CREATE_USER 
                                    , PRD.COMPANYCODE AS COMPANYCODE  
                                    , PRD.IS_DISCOUNT AS IS_DISCOUNT  
                                    , PRD.DEL_FLAG    AS DEL_FLAG     
                            FROM PRODUCT PRD, PRODUCT_TYPE PRDT, CHECK_IMEI_TYPE CHKI, EMPLOYEE EMP  
                            WHERE PRD.PRODTYPENO = PRDT.PRODTYPENO AND PRD.IMEI_FLAG = CHKI.CHECK_IMEI_TYPE AND  PRD.MODI_USER = EMP.EMPNO(+)   
                            AND NVL(IS_DISCOUNT,'N') = 'N'  
                            AND (length(PRODNO) = 8 OR length(PRODNO) = 9 OR length(PRODNO) = 10 ) 
                            AND PRD.DEL_FLAG = 'N'       
                            AND PRD.COMPANYCODE = '01' 
                        ");


            if (!string.IsNullOrEmpty(ProductCategory))
            {
                sb.AppendLine(" AND PRD.PRODTYPENO = " + OracleDBUtil.SqlStr(ProductCategory));
            }
            if (!string.IsNullOrEmpty(ProductCode))
            {
                sb.AppendLine(" AND PRD.PRODNO LIKE " + OracleDBUtil.LikeStr(ProductCode));
            }
            if (!string.IsNullOrEmpty(ProductName))
            {
                sb.AppendLine(" AND PRD.PRODNAME LIKE " + OracleDBUtil.LikeStr(ProductName));
            }
            if (!string.IsNullOrEmpty(VerifyImei))
            {
                sb.AppendLine(" AND PRD.IMEI_FLAG = " + OracleDBUtil.SqlStr(VerifyImei));
            }
            sb.AppendLine(" )");

            if (!string.IsNullOrEmpty(Status))
            {
                sb.AppendLine(" WHERE 1=1");
                sb.AppendLine(" AND STATUS = decode(" + Status + ",0,'有效',2,'過期',1,'尚未生效') ");
            }

            sb.AppendLine("  ORDER BY PRODNO ");

            DataTable dt = OracleDBUtil.Query_Data(sb.ToString());
            return dt;
        }

        //public string Check_Id(string PRODNO)
        //{
        //    StringBuilder sb = new StringBuilder();
        //    sb.Append(" SELECT * FROM PRODUCT WHERE 1=1");
        //    if (!string.IsNullOrEmpty(PRODNO))
        //    {
        //        sb.Append(" AND ROWNUM =1 AND PRODNO = " + OracleDBUtil.SqlStr(PRODNO));
        //    }

        //    DataTable dt = OracleDBUtil.Query_Data(sb.ToString());

        //    if (dt.Rows.Count > 0)
        //    {
        //        return "商品料號重複";
        //    }
        //    else
        //    {
        //        return "";
        //    }
        //}

        public void AddNewOne_ProductMethodSet(OPT10_Product_DTO ds)
        {
            OracleConnection objConn = null;
            OracleTransaction objTX = null;

            try
            {
                objConn = OracleDBUtil.GetConnection();
                objTX = objConn.BeginTransaction();

                OracleDBUtil.Insert(ds.Tables["PRODUCT"]);
                //OracleDBUtil.Insert(ds.Tables["PAY_MODE"]);

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

        public void UpdateOne_ProductMethodSet(OPT10_Product_DTO ds)
        {
            OracleConnection objConn = null;
            OracleTransaction objTX = null;

            try
            {
                objConn = OracleDBUtil.GetConnection();
                objTX = objConn.BeginTransaction();

                //edit can't edit the pay_mode table value
                //so not update PAY_MODE
                OracleDBUtil.UPDDATEByUUID(ds.Tables["PRODUCT"], "PRODNO");
                //OracleDBUtil.UPDDATEByUUID(ds.Tables["PAY_MODE"], "PAY_MODE_ID");

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
