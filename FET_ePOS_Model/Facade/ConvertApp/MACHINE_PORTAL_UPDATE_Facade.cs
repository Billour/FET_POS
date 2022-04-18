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
    public class MACHINE_PORTAL_UPDATE_Facade : BaseClass
   {
        public void AddNewOne_STORE(STORE_PORTAL_UPDATE_DTO ds)
        {
            OracleConnection objConn = null;
            OracleTransaction objTX = null;

            try
            {
                objConn = OracleDBUtil.GetConnection();
                objTX = objConn.BeginTransaction();

                OracleDBUtil.Insert(ds.Tables["STORE"]);

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

        public void UpdateAndInsert_Machine(DataTable dtAdd,DataTable dtUpd)
        {
            OracleConnection objConn = null;
            OracleTransaction objTX = null;

            try
            {
                objConn = OracleDBUtil.GetConnection();
                objTX = objConn.BeginTransaction();
                if(dtUpd.Rows.Count>0)
                    OracleDBUtil.UPDDATEByUUID(dtUpd, "MACHINE_ID");
                if(dtAdd.Rows.Count>0)
                    OracleDBUtil.Insert(dtAdd);

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

        public DataTable Query_OLD_V_RETAIL_MACHINE_IP_MAPPING()
        {
            OracleConnection oCon = null;
            try
            {
                //OLD POS
                //string sCon = OracleDBUtil.GetOldPOSConnectionStringByTNSName();
                //OracleConnection oCon = OracleDBUtil.GetConnectionByConnString(sCon);
                oCon = OracleDBUtil.GetERPPOSConnection();

                ////回寫狀態STATUS='T'
                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                sb.Length = 0;
                sb.Append("SELECT RSA.STOREID AS STORE_NO,  ");
                sb.Append("MIP.MACHINE_NO AS HOST_NO,  ");
                sb.Append("MIP.MACHINE_IP AS IP_ADDRESS,  ");
                sb.Append("'N' AS ISNEW  ");
                sb.Append("FROM V_RETAIL_SALESORG_AREA RSA , V_RETAIL_MACHINE_IP_MAPPING MIP  ");
                sb.Append("WHERE RSA.SALESCD = MIP.RETAILCODE ");
                //sb.Append("where rownum <2  ");

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

        public bool Query_TERMINATING_MACHINE(string Store_No, string IP_ADDRESS, string HOST_NO)
        {
            OracleConnection oCon = null;
            try
            {
                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                sb.Append("SELECT COUNT(*) FROM STORE_TERMINATING_MACHINE  ");
                sb.Append("WHERE STORE_NO='" + Store_No + "'");
                sb.Append("  AND IP_ADDRESS='" + IP_ADDRESS + "'");
                sb.Append("  AND HOST_NO='" + HOST_NO + "'");
                bool bRet = false;
                oCon=OracleDBUtil.GetConnection();
                DataTable dt = OracleDBUtil.GetDataSet(oCon, sb.ToString()).Tables[0];
                if (dt.Rows[0][0].ToString() != "0")
                {
                    bRet = true;
                }
                return bRet;
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
