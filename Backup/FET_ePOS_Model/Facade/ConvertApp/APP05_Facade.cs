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
    public class APP05_Facade : BaseClass
   {
        public DataTable Query_DS_ORDERD_OLD()
        {
            //OLD POS
            //string sCon = OracleDBUtil.GetOldPOSConnectionStringByTNSName();
            //string sCon =Advtek.Utility.OracleDBUtil.GetOldPOSConnectionStringByTNSName();
            //OracleDBUtil.GetOldPOSConnectionStringByTNSName()
            //OracleConnection oCon = OracleDBUtil.GetConnectionByConnString(sCon);
            OracleConnection oCon = null;
            OracleTransaction oTX = null;
            try
            {
                oCon = OracleDBUtil.GetERPPOSConnection();
                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                oTX = oCon.BeginTransaction();
                sb.Append("UPDATE DS_ORDERD ");
                sb.Append("   SET FLG_SUBINVENTORY_TRANSFER='T' ");
                sb.Append(" WHERE (NVL(FLG_SUBINVENTORY_TRANSFER,'0')='0'  OR FLG_SUBINVENTORY_TRANSFER='N') ");
                sb.Append("   AND ROWNUM=1 ");
                OracleDBUtil.ExecuteSql(oTX, sb.ToString());
                sb.Length = 0;
                sb.Append("SELECT * ");
                sb.Append("FROM DS_ORDERD ");
                sb.Append("WHERE FLG_SUBINVENTORY_TRANSFER='T' ");
                //DataTable dt = OracleDBUtil.GetDataSet(oCon, sb.ToString()).Tables[0];
                DataTable dt = OracleDBUtil.GetDataSet(oTX, sb.ToString()).Tables[0];
                oTX.Commit();
                return dt;
            }
            catch (Exception ex)
            {
                oTX.Rollback();
                throw ex;
            }
            finally {
                oTX.Dispose();
                if (oCon.State == ConnectionState.Open) oCon.Close();
                oCon.Dispose();
                OracleConnection.ClearAllPools();    
            }
        }


        public void Add_DS_ORDERD(APP05_DS_ORDERM_IMPORT_DTO dsAdd)
        {
            OracleConnection objConn = null;
            OracleTransaction objTX = null;
            try
            {
                //WEB POS
                objConn = OracleDBUtil.GetConnection();
                objTX = objConn.BeginTransaction();

                //OracleDBUtil.Insert(dsAdd.Tables["PROCESSORDER"]);
                OracleDBUtil.Insert(objTX, dsAdd.Tables["DS_ORDERD"]);
                objTX.Commit();

                //OLD POS
                //string sCon = OracleDBUtil.GetOldPOSConnectionStringByTNSName();
                //OracleConnection oCon = OracleDBUtil.GetConnectionByConnString(sCon);
                OracleConnection oCon = OracleDBUtil.GetERPPOSConnection();
                objTX = oCon.BeginTransaction();
                ////回寫狀態STATUS='T'
                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                sb.Append("UPDATE DS_ORDERD ");
                sb.Append("   SET FLG_SUBINVENTORY_TRANSFER='1' ");
                sb.Append("WHERE FLG_SUBINVENTORY_TRANSFER='T' ");
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

        public string SP_DS_ORDERD_IMPORT()
        {
            OracleConnection objConn = null;
            OracleTransaction objTX = null;
            string sRet = "";
            try
            {
                objConn = OracleDBUtil.GetConnection();
                objTX = objConn.BeginTransaction();

                //OracleDBUtil.ExecuteSql_SP(objTX, "SP_DS_ORDERD_IMPORT");
                OracleCommand oraCmd = new OracleCommand("SP_DS_ORDERD_IMPORT");
                oraCmd.CommandType = System.Data.CommandType.StoredProcedure;
                oraCmd.Parameters.Add(new OracleParameter("outMESSAGE", OracleType.VarChar, 2000)).Direction = ParameterDirection.Output;
                oraCmd.Connection = objConn;
                oraCmd.Transaction = objTX;
                oraCmd.ExecuteNonQuery();
                sRet = oraCmd.Parameters["outMESSAGE"].Value.ToString();

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
            return sRet;
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
