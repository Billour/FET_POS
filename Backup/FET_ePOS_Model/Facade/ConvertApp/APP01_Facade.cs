using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.OracleClient;

using Advtek.Utility;

using FET.POS.Model.DTO;
using FET.POS.Model.Facade;
using FET.POS.Model.Facade.ConvertApp;
using FET.POS.Model.DTO.ConvertApp;
using FET.POS.Model.Common;


namespace FET.POS.Model.Facade.ConvertApp
{
    public class APP01_Facade : BaseClass
   {
        public DataTable Query_One_Product_TEMP()
        {
            OracleConnection oCon = null;
            try
            {
                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                sb.Append("SELECT * ");
                sb.Append("FROM PRODUCT_TEMP where rownum=1");
                oCon=OracleDBUtil.GetConnection();
                DataTable dt = OracleDBUtil.GetDataSet(oCon, sb.ToString()).Tables[0];

                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (oCon.State == ConnectionState.Open) oCon.Close();
                oCon.Dispose();
                OracleConnection.ClearAllPools();
            }
        }

        public DataTable Query_Product_TEMP_NEW()
        {
            OracleConnection oCon = null;
            try
            {
                oCon=OracleDBUtil.GetConnection();
                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                sb.Append("UPDATE PRODUCT_TEMP ");
                sb.Append("   SET STATUS='T' ");
                sb.Append(" WHERE PRODNO NOT IN (SELECT PRODNO FROM PRODUCT) ");
                OracleDBUtil.ExecuteSql(oCon, sb.ToString());

                sb.Length = 0;
                sb.Append("SELECT * ");
                sb.Append("FROM PRODUCT_TEMP ");
                sb.Append("WHERE STATUS='T' AND PRODNO NOT IN (SELECT PRODNO FROM PRODUCT)");

                DataTable dt = OracleDBUtil.GetDataSet(oCon, sb.ToString()).Tables[0];

                return dt;
            }
            catch (Exception ex)
            {
                
                throw ex;
            }
            finally 
            {
                if (oCon.State == ConnectionState.Open) oCon.Close();
                oCon.Dispose();
                OracleConnection.ClearAllPools();
            }
        }

        public DataTable Query_Product_TEMP_Update()
        {
            OracleConnection oCon = null;
            try
            {
                oCon = OracleDBUtil.GetConnection();
                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                sb.Append("UPDATE PRODUCT_TEMP ");
                sb.Append("   SET STATUS='T' ");
                sb.Append(" WHERE PRODNO IN (SELECT PRODNO FROM PRODUCT) ");
                OracleDBUtil.ExecuteSql(oCon, sb.ToString());

                sb.Length = 0;
                sb.Append("SELECT * ");
                sb.Append("FROM PRODUCT_TEMP ");
                sb.Append("WHERE STATUS='T' AND  PRODNO IN (SELECT PRODNO FROM PRODUCT)");
                //sb.Append("FROM PRODUCT_TEMP where prodno='93800008' and prodno in (select prodno from product)");

                DataTable dt = OracleDBUtil.GetDataSet(oCon, sb.ToString()).Tables[0];

                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally 
            {
                if (oCon.State == ConnectionState.Open) oCon.Close();
                oCon.Dispose();
                OracleConnection.ClearAllPools();
            }
        }

        public DataTable Query_OLD_Product()
        {
            OracleConnection oCon = null;
            try
            {
                oCon = OracleDBUtil.GetERPPOSConnection();

                //回寫狀態STATUS='T'
                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                //sb.Append("UPDATE PRODUCT ");
                //sb.Append("   SET STATUS='T' ");
                //sb.Append("WHERE (LENGTH(PRODNO)=10 OR LENGTH(PRODNO)=9)  AND COMPANYCODE='01' ");
                //OracleDBUtil.ExecuteSql(oCon, sb.ToString());

                sb.Length = 0;
                sb.Append("SELECT * ");
                sb.Append("FROM PRODUCT ");
                sb.Append("WHERE (LENGTH(PRODNO)=10 OR LENGTH(PRODNO)=9)  AND COMPANYCODE='01'");
         
                DataTable dt = OracleDBUtil.GetDataSet(oCon, sb.ToString()).Tables[0];
                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally 
            {
                if (oCon.State == ConnectionState.Open) oCon.Close();
                oCon.Dispose();
                OracleConnection.ClearAllPools();
            }
        }

        public DataTable Query_Discount_Master_New()
        {
            OracleConnection oCon = null;
            try
            {
                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                sb.Append("SELECT * FROM DISCOUNT_MASTER  ");
                sb.Append("WHERE (to_char(MODI_DTM,'YYYYMMDD') =to_char(SYSDATE,'YYYYMMDD') OR to_char(MODI_DTM,'YYYYMMDD')=to_char((SYSDATE-1),'YYYYMMDD'))  ");
                sb.Append("  AND LENGTH(LTRIM(RTRIM(DISCOUNT_CODE)))=8  ");
                sb.Append("  AND DISCOUNT_CODE NOT IN (SELECT PRODNO FROM PRODUCT) ");
                oCon=OracleDBUtil.GetConnection();
                DataTable dt = OracleDBUtil.GetDataSet(oCon, sb.ToString()).Tables[0];

                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally 
            {
                if (oCon.State == ConnectionState.Open) oCon.Close();
                oCon.Dispose();
                OracleConnection.ClearAllPools();
            }
        }


        public DataTable Query_Discount_Master_Update()
        {
            OracleConnection oCon = null;
            try
            {
                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                sb.Append("SELECT * FROM DISCOUNT_MASTER  ");
                sb.Append("WHERE (MODI_DTM =SYSDATE OR MODI_DTM=(SYSDATE-1)) ");
                sb.Append("  AND LENGTH(LTRIM(RTRIM(DISCOUNT_CODE)))=8  ");
                sb.Append("  AND DISCOUNT_CODE IN (SELECT PRODNO FROM PRODUCT) ");
                oCon=OracleDBUtil.GetConnection();
                DataTable dt = OracleDBUtil.GetDataSet(oCon, sb.ToString()).Tables[0];

                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally 
            {
                if (oCon.State == ConnectionState.Open) oCon.Close();
                oCon.Dispose();
                OracleConnection.ClearAllPools();
            }
        }

        public DataTable Query_Discount_Master_Today_Yesterday()
        {
            OracleConnection oCon=null;
            try
            {
                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                sb.Append("SELECT * FROM DISCOUNT_MASTER  ");
                sb.Append("WHERE (to_char(MODI_DTM,'YYYYMMDD') =to_char(SYSDATE,'YYYYMMDD') OR to_char(MODI_DTM,'YYYYMMDD')=to_char((SYSDATE-1),'YYYYMMDD'))  ");
                sb.Append("  AND LENGTH(LTRIM(RTRIM(DISCOUNT_CODE)))=8  ");
                //sb.Append("  AND DISCOUNT_CODE NOT IN (SELECT PRODNO FROM PRODUCT) ");
                oCon=OracleDBUtil.GetConnection();
                DataTable dt = OracleDBUtil.GetDataSet(oCon, sb.ToString()).Tables[0];

                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally 
            {
                if (oCon.State == ConnectionState.Open) oCon.Close();
                oCon.Dispose();
                OracleConnection.ClearAllPools();
            }
        }


        public DataTable Query_Product()
        {
            OracleConnection oCon=null;
            try
            {
                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                sb.Append("SELECT * ");
                sb.Append("FROM PRODUCT ");
                oCon=OracleDBUtil.GetConnection();
                DataTable dt = OracleDBUtil.GetDataSet(oCon, sb.ToString()).Tables[0];

                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally 
            {
                if (oCon.State == ConnectionState.Open) oCon.Close();
                oCon.Dispose();
                OracleConnection.ClearAllPools();
            }
        }


        public void Clear_PRODUCT_TEMP(APP01_ProductImport ds)
        {
            OracleConnection objConn = null;
            OracleTransaction objTX = null;

            try
            {
                objConn = OracleDBUtil.GetConnection();
                objTX = objConn.BeginTransaction();

                //OracleDBUtil.UPDDATEByUUID(ds.Tables["HG_CONVERT_GIFT_D"], "SID");
                //OracleDBUtil.DELETEByUUID(ds.Tables["UPLOAD_TEMP"], "BATCH_NO");
                OracleDBUtil.DELETE(ds.Tables["PRODUCT_TEMP"], "");

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
       

        public void Update_Add_PRODUCT(APP01_ProductImport dsAdd,APP01_ProductImport dsUpd)
        {
            OracleConnection objConn = null;
            OracleTransaction objTX = null;

            try
            {
                objConn = OracleDBUtil.GetConnection();
                objTX = objConn.BeginTransaction();
                OracleDBUtil.Insert(dsAdd.Tables["PRODUCT"]);
                OracleDBUtil.UPDDATEByUUID(dsUpd.Tables["PRODUCT"], "PRODNO");
                objTX.Commit();

                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                sb.Append("UPDATE PRODUCT_TEMP ");
                sb.Append("   SET STATUS='1' ");
                sb.Append(" WHERE STATUS='T' ");
                OracleDBUtil.ExecuteSql(objConn, sb.ToString());

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


        public void Add_PRODUCT_TEMP(APP01_ProductImport dsAdd)
        {
            OracleConnection objConn = null;
            OracleTransaction objTX = null;

            try
            {
                //WEB POS
                objConn = OracleDBUtil.GetConnection();
                objTX = objConn.BeginTransaction();

                OracleDBUtil.Insert(dsAdd.Tables["PRODUCT_TEMP"]);
                objTX.Commit();

                //OLD POS
                //string sCon = OracleDBUtil.GetOldPOSConnectionStringByTNSName();
                //OracleConnection oCon = OracleDBUtil.GetConnectionByConnString(sCon);
                OracleConnection oCon = OracleDBUtil.GetERPPOSConnection();

                //回寫狀態STATUS='T'
                //System.Text.StringBuilder sb = new System.Text.StringBuilder();
                //sb.Append("UPDATE PRODUCT ");
                //sb.Append("   SET STATUS='Y' ");
                //sb.Append("WHERE UPDDATE=TO_CHAR(SYSDATE,'YYYYMMDD') ");
                ////sb.Append(" WHERE UPDDATE='20100301' ");
                //sb.Append("   AND  STATUS='1'");
                //OracleDBUtil.ExecuteSql(oCon, sb.ToString());
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

       #region call SP
       // public void Check_Upload_Temp(string sBATCH_NO, string sUSER_ID)
       // {
       //     OracleConnection objConn = null;
       //     OracleTransaction objTX = null;

       //     try
       //     {
       //         objConn = OracleDBUtil.GetConnection();
       //         objTX = objConn.BeginTransaction();

       //         OracleDBUtil.ExecuteSql_SP(
       //            objTX
       //            , "SP_INV29_CheckImei_Upload_Temp"
       //            , new OracleParameter("inBATCHNO", sBATCH_NO)
       //            , new OracleParameter("inUSERID", sUSER_ID)
       //            );

       //         objTX.Commit();

       //     }
       //     catch (Exception ex)
       //     {
       //         objTX.Rollback();
       //         throw ex;
       //     }
       //     finally
       //     {
       //         objTX = null;
       //         objConn = null;
       //     }
       // }
       #endregion
       

      
   }
}
