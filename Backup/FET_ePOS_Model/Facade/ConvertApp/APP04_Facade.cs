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
    public class APP04_Facade : BaseClass
   {
        public DataTable Query_PROCESSORDER_OLD()
        {
            OracleConnection oCon = null;
            OracleTransaction oTX = null;
            try
            {
                oCon = OracleDBUtil.GetERPPOSConnection();
                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                oTX = oCon.BeginTransaction();
                sb.Append("UPDATE PROCESSORDER ");
                sb.Append("   SET FLG_DOWNLOAD='T' ");
                sb.Append(" WHERE (NVL(FLG_DOWNLOAD,'0')='0'  OR FLG_DOWNLOAD='N') ");
                OracleDBUtil.ExecuteSql(oTX, sb.ToString());
                sb.Length = 0;
                sb.Append("SELECT * ");
                sb.Append("FROM PROCESSORDER ");
                sb.Append("WHERE FLG_DOWNLOAD='T' ");

                DataTable dt = OracleDBUtil.GetDataSet(oTX, sb.ToString()).Tables[0];

                oTX.Commit();

                return dt;
            }
            catch (Exception ex)
            {
                oTX.Rollback();
                throw ex;
            }
            finally 
            {
                oTX.Dispose();
                if (oCon.State == ConnectionState.Open) oCon.Close();
                oCon.Dispose();
                OracleConnection.ClearAllPools();    
            }
        }

        public void Add_PROCESSORDER(APP04_ProcessOrder_DTO dsAdd)
        {
            OracleConnection objConn = null;
            OracleTransaction objTX = null;
            try
            {
                //WEB POS
                objConn = OracleDBUtil.GetConnection();
                objTX = objConn.BeginTransaction();

                OracleDBUtil.Insert(objTX, dsAdd.Tables["PROCESSORDER"]);
                objTX.Commit();

                //OLD POS
                OracleConnection oCon = OracleDBUtil.GetERPPOSConnection();
                objTX = oCon.BeginTransaction();

                //回寫狀態STATUS='T'
                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                sb.Append("UPDATE PROCESSORDER ");
                sb.Append("   SET FLG_DOWNLOAD='1' ");
                sb.Append("WHERE FLG_DOWNLOAD='T' ");
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
                if (objConn.State == ConnectionState.Open) objConn.Close();
                objConn.Dispose();
                OracleConnection.ClearAllPools();
            }
        }

        public void SP_PROCESSORDER_IMPORT()
        {
            OracleConnection objConn = null;
            OracleTransaction objTX = null;

            try
            {
                objConn = OracleDBUtil.GetConnection();
                objTX = objConn.BeginTransaction();

                OracleDBUtil.ExecuteSql_SP(objTX,"SP_PROCESSORDER_IMPORT");

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
   }
}
