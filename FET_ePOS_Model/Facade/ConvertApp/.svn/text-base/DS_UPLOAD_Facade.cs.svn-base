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
    public class DS_UPLOAD_Facade : BaseClass
   {
        
        public DataTable Query_POS2EP_DS_ORDER()
        {
            OracleConnection oCon = null;
            OracleTransaction objTX = null;
            try
            {
                oCon=OracleDBUtil.GetConnection();
                objTX = oCon.BeginTransaction();
                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                sb.Append("UPDATE POS2EP_DS_ORDER ");
                sb.Append("   SET STATUS='T' ");
                sb.Append(" WHERE STATUS='0' ");
                OracleDBUtil.ExecuteSql(objTX, sb.ToString());

                sb.Length = 0;
                sb.Append("SELECT POS_ORDER_NUM, ");
                sb.Append("POS_ORDER_LINENUM,");
                sb.Append("RETAIL_NUM,");
                sb.Append("RETAIL_NAME,");
                sb.Append("RETAIL_ADDRESS,");
                sb.Append("RETAIL_TEL,");
                sb.Append("RETAIL_CONTACT_NAME,");
                sb.Append("CATEGORY_LEVEL1,");
                sb.Append("CATEGORY_LEVEL2,");
                sb.Append("CATEGORY_LEVEL3,");
                sb.Append("CATEGORY_LEVEL4,");
                sb.Append("CATEGORY_LEVEL5,");
                sb.Append("CATEGORY_LEVEL6,");
                sb.Append("ITEM_NUM,");
                sb.Append("ITEM_DESC,");
                sb.Append("QUANTITY,");
                sb.Append("UNIT_PRICE,");
                sb.Append("DESTINATION_ORG_ID,");
                sb.Append("DESTINATION_SUBINVENTORY,");
                sb.Append("DESTINATION_LOCATOR,");
                sb.Append("SUGGESTED_VENDOR_NUM,");
                sb.Append("SUGGESTED_VENDOR_SITE_ID,");
                sb.Append("NEED_BY_DATE ");
                sb.Append("FROM QUERY_POS2EP_ORDER_INFO  ");

                DataTable dt = OracleDBUtil.GetDataSet(objTX, sb.ToString()).Tables[0];
                objTX.Commit();
                return dt;
            }
            catch (Exception ex)
            {
                objTX.Rollback();
                throw ex;
            }
            finally {
                objTX.Dispose();
                if (oCon.State == ConnectionState.Open) oCon.Close();
                oCon.Dispose();
                OracleConnection.ClearAllPools();
            }
        }


        public DataTable Query_FTP_INFO()
        {
            OracleConnection oCon = null;
            OracleTransaction objTX = null;
            try
            {
                oCon = OracleDBUtil.GetConnection();
                objTX = oCon.BeginTransaction();
                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                sb.Length = 0;
                sb.Append("SELECT ");
                sb.Append("( SELECT PARA_VALUE FROM SYS_PARA WHERE PARA_KEY='FTP_HOST' ) AS F_HOST,");
                sb.Append("( select para_value from sys_para where para_key='FTP_USER' ) AS F_USER,");
                sb.Append("( select para_value from sys_para where para_key='FTP_DIR' ) AS F_DIR,");
                sb.Append("( SELECT PARA_VALUE FROM SYS_PARA WHERE PARA_KEY='FTP_PASSWORD' ) AS F_PASSWORD ");
                sb.Append("FROM DUAL");

                DataTable dt = OracleDBUtil.GetDataSet(objTX, sb.ToString()).Tables[0];
                objTX.Commit();
                return dt;
            }
            catch (Exception ex)
            {
                objTX.Rollback();
                throw ex;
            }
            finally
            {
                objTX.Dispose();
                if (oCon.State == ConnectionState.Open) oCon.Close();
                oCon.Dispose();
                OracleConnection.ClearAllPools();
            }
        }
        public void UPDATE_POS2EP_DS_ORDER()
        {
            OracleConnection oCon = null;
            OracleTransaction objTX = null;
            try
            {
                oCon = OracleDBUtil.GetConnection();
                objTX = oCon.BeginTransaction();
                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                sb.Append("UPDATE POS2EP_DS_ORDER ");
                sb.Append("   SET STATUS='1' ");
                sb.Append(" WHERE STATUS='T' ");
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
                objTX.Dispose();
                if (oCon.State == ConnectionState.Open) oCon.Close();
                oCon.Dispose();
                OracleConnection.ClearAllPools();
            }
        }
   }
}
