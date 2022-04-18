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
    public class ERP_PRODUCTPRICE_HANDOVER_Facade : BaseClass
   {

        public DataTable Query_WEB_SALE_HEAD()
        {
            OracleConnection oCon = null;
            try
            {
                //OLD POS
                //OracleConnection oCon = OracleDBUtil.GetERPPOSConnection();

                //WEB
                oCon = OracleDBUtil.GetConnection();

                //回寫狀態STATUS='T'
                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                sb.Append("UPDATE SALE_HEAD SET FLG_TOERP='T' WHERE  FLG_TOERP IS NULL ");
                OracleDBUtil.ExecuteSql(oCon, sb.ToString());

                sb.Length = 0;
                sb.Append("SELECT ");
                sb.Append("DETAIL.ID AS MYNO, ");
                sb.Append("'01'  AS COMPANYCODE, ");
                sb.Append("HEAD.STORE_NO AS STORENO, ");
                sb.Append("HEAD.SALE_STATUS AS TXTTYPE, ");
                sb.Append("HEAD.TRADE_DATE AS TRANDATE, ");
                sb.Append("(SELECT STOCK_NAME FROM LOC WHERE LOC_ID = INV_GOODLOCUUID ) AS LOCATION, ");
                sb.Append("DETAIL.PRODNO AS ITEMCODE, ");
                sb.Append("DETAIL.QUANTITY AS QTY, ");
                sb.Append("'1' AS DWNFLAG ");
                sb.Append("FROM SALE_HEAD HEAD, SALE_DETAIL DETAIL ");
                sb.Append("WHERE HEAD.POSUUID_MASTER = DETAIL.POSUUID_MASTER AND FLG_TOERP = 'T' ");
                sb.Append("AND (LENGTH(TRIM(DETAIL.PRODNO))) = 9  ");
                sb.Append("ORDER BY HEAD.SALE_NO ");

                DataTable dt = OracleDBUtil.GetDataSet(oCon, sb.ToString()).Tables[0];
                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally {
                if (oCon.State == ConnectionState.Open) oCon.Close();
                oCon.Dispose();
                OracleConnection.ClearAllPools();
            }
        }

        public void Insert_ERP_ERP_SALES(DataTable dtADD)
        {
            //ERP
            OracleConnection conERP = null;
            OracleTransaction txERP = null;
            //WEB
            OracleConnection objConn = null;
            OracleTransaction objTX = null;
            try
            {
                //WEB
                objConn = OracleDBUtil.GetConnection();
                objTX = objConn.BeginTransaction();

                //ERP
                conERP = OracleDBUtil.GetERPPOSConnection();
                txERP = conERP.BeginTransaction();

                if (dtADD.Rows.Count > 0) 
                {
                    //ERP:ERP_SALES Insert
                    OracleDBUtil.Insert(txERP,dtADD);

                    //WEB:SALE_HEAD Update
                    System.Text.StringBuilder sb = new System.Text.StringBuilder();
                    sb.Append("UPDATE SALE_HEAD SET  FLG_TOERP='Y',DTM_TOERP =SYSDATE WHERE  FLG_TOERP = 'T' ");
                    OracleDBUtil.ExecuteSql(objTX, sb.ToString());
                    
                }

                txERP.Commit();
                objTX.Commit();
            }
            catch (Exception ex)
            {
                objTX.Rollback();
                txERP.Rollback();
                throw ex;
            }
            finally
            {
                //ERP
                txERP.Dispose();
                if (conERP.State == ConnectionState.Open) conERP.Close();
                conERP.Dispose();
                //WEB
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
