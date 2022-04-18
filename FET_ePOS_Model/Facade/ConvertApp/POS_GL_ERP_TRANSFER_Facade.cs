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
    //POS交接分錄檔回ERP
    public class POS_GL_ERP_TRANSFER_Facade : BaseClass
   {
        public DataTable Query_POS_GL()
        {
            OracleConnection oCon = null;
            try
            {
                oCon=OracleDBUtil.GetConnection();
                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                sb.Append("UPDATE POS_GL ");
                sb.Append("   SET CC2ERP_FLAG='T' ");
                sb.Append(" WHERE CC2ERP_FLAG is null or CC2ERP_FLAG='N'  ");
                OracleDBUtil.ExecuteSql(oCon, sb.ToString());

                sb.Length = 0;
                sb.Append("SELECT * ");
                sb.Append("FROM POS_GL ");
                sb.Append("WHERE CC2ERP_FLAG='T'");

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
        public void UpdateAndInsert_Store(DataTable dtAdd)
        {
            OracleConnection objConnOld = null;
            OracleConnection objConnNew = null;
            OracleTransaction objTXOld = null;
            OracleTransaction objTXNew = null;

            try
            {
                objConnOld = OracleDBUtil.GetERPPOSConnection();//OLD
                objConnNew = OracleDBUtil.GetConnection();//NEW
                
                objTXOld = objConnOld.BeginTransaction();
                objTXNew = objConnNew.BeginTransaction();
                
                //if (dtUpd.Rows.Count > 0)
                //    OracleDBUtil.UPDDATEByUUID(dtUpd, "STORE_NO");
                if (dtAdd.Rows.Count > 0) 
                {
                    //OLD POS
                    OracleDBUtil.Insert(objTXOld, dtAdd);
                    //New POS update status 
                    System.Text.StringBuilder sb = new System.Text.StringBuilder();
                    sb.Append("UPDATE POS_GL ");
                    sb.Append("   SET CC2ERP_FLAG='Y',MODI_USER='CONVERT',MODI_DTM=SYSDATE,CC2ERP_ID='CONVERT',CC2ERP_DTM=SYSDATE ");
                    sb.Append(" WHERE CC2ERP_FLAG='T'  ");
                    OracleDBUtil.ExecuteSql(objTXNew, sb.ToString());
                }
                objTXOld.Commit();
                objTXNew.Commit();
            }
            catch (Exception ex)
            {
                objTXOld.Rollback();
                objTXNew.Rollback();
                throw ex;
            }
            finally
            {
                if (objConnOld.State == ConnectionState.Open) objConnOld.Close();
                objConnOld.Dispose();
                objTXNew.Dispose();
                if (objConnNew.State == ConnectionState.Open) objConnNew.Close();
                objConnNew.Dispose();
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

