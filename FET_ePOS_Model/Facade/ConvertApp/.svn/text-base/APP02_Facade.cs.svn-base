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
    public class APP02_Facade : BaseClass
   {
        public DataTable Query_One_Dept_TEMP()
        {
            OracleConnection oCon = null;
            try
            {
                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                sb.Append("SELECT * ");
                sb.Append("FROM DEPT_TEMP where rownum=1");
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

        public DataTable Query_DEPT_TEMP_NEW()
        {
            OracleConnection oCon = null;
            try
            {
                oCon=OracleDBUtil.GetConnection();
                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                sb.Append("UPDATE DEPT_TEMP ");
                sb.Append("   SET STATUS='T' ");
                sb.Append(" WHERE DEPT_NO NOT IN (SELECT DEPT_NO FROM DEPT) ");
                OracleDBUtil.ExecuteSql(oCon, sb.ToString());

                sb.Length = 0;
                sb.Append("SELECT * ");
                sb.Append("FROM DEPT_TEMP ");
                sb.Append("WHERE STATUS='T' AND DEPT_NO NOT IN (SELECT DEPT_NO FROM DEPT)");

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

        public DataTable Query_Dept_TEMP_Update()
        {
            OracleConnection oCon = null;
            try
            {
                oCon = OracleDBUtil.GetConnection();
                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                sb.Append("UPDATE DEPT_TEMP ");
                sb.Append("   SET STATUS='T' ");
                sb.Append(" WHERE DEPT_NO IN (SELECT DEPT_NO FROM DEPT_TEMP) ");
                OracleDBUtil.ExecuteSql(oCon, sb.ToString());

                sb.Length = 0;
                sb.Append("SELECT * ");
                sb.Append("FROM DEPT_TEMP ");
                sb.Append("WHERE STATUS='T' AND  DEPT_NO IN (SELECT DEPT_NO FROM DEPT)");
                //sb.Append("FROM PRODUCT_TEMP where prodno='93800008' and prodno in (select prodno from product)");

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

        public DataTable Query_OLD_Dept()
        {
            OracleConnection oCon=null;

            try
            {
                //OLD POS
                //string sCon = OracleDBUtil.GetOldPOSConnectionStringByTNSName();
                //OracleConnection oCon = OracleDBUtil.GetConnectionByConnString(sCon);
                 oCon= OracleDBUtil.GetERPPOSConnection();

                ////回寫狀態STATUS='T'
                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                //無欄位可塞狀態
                //sb.Append("UPDATE DEPT ");
                //sb.Append("   SET STATUS='T' ");
                //sb.Append("WHERE UPDDATE=TO_CHAR(SYSDATE,'YYYYMMDD') ");
                ////sb.Append(" WHERE UPDDATE='20100301' ");
                //sb.Append(" AND (LENGTH(PRODNO)=10 OR LENGTH(PRODNO)=9) ");
                //OracleDBUtil.ExecuteSql(oCon, sb.ToString());

                sb.Length = 0;
                sb.Append("SELECT * ");
                sb.Append("FROM DEPT ");
                //sb.Append("WHERE UPDDATE=TO_CHAR(SYSDATE,'YYYYMMDD') ");
                ////sb.Append(" WHERE UPDDATE='20100301' ");
                //sb.Append(" AND STATUS='T' ");
         
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


        public void Clear_DEPT_TEMP(APP02_DeptImport_DTO ds)
        {
            OracleConnection objConn = null;
            OracleTransaction objTX = null;

            try
            {
                objConn = OracleDBUtil.GetConnection();
                objTX = objConn.BeginTransaction();

                OracleDBUtil.DELETE(ds.Tables["DEPT_TEMP"], "");

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
       

        public void Update_Add_DEPT(APP02_DeptImport_DTO dsAdd,APP02_DeptImport_DTO dsUpd)
        {
            OracleConnection objConn = null;
            OracleTransaction objTX = null;

            try
            {
                objConn = OracleDBUtil.GetConnection();
                objTX = objConn.BeginTransaction();
                OracleDBUtil.Insert(dsAdd.Tables["DEPT"]);
                OracleDBUtil.UPDDATEByUUID(dsUpd.Tables["DEPT"], "DEPT_NO");
                objTX.Commit();

                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                sb.Append("UPDATE DEPT_TEMP ");
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


        public void Add_DEPT_TEMP(APP02_DeptImport_DTO dsAdd)
        {
            OracleConnection objConn = null;
            OracleTransaction objTX = null;

            try
            {
                //WEB POS
                objConn = OracleDBUtil.GetConnection();
                objTX = objConn.BeginTransaction();

                OracleDBUtil.Insert(dsAdd.Tables["DEPT_TEMP"]);
                objTX.Commit();

                //OLD POS
                //string sCon = OracleDBUtil.GetOldPOSConnectionStringByTNSName();
                //OracleConnection oCon = OracleDBUtil.GetConnectionByConnString(sCon);

                ////回寫狀態STATUS='T'
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

