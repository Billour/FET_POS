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
    public class STORE_PORTAL_UPDATE_Facade : BaseClass
   {
       public void COPY_SALESORG_AREA_CONTACT_INFO(STORE_PORTAL_UPDATE_DTO ds)
       {
           OracleConnection objConn = null;
           OracleTransaction objTX = null;

           try
           {
               objConn = OracleDBUtil.GetConnection();
               objTX = objConn.BeginTransaction();
               OracleDBUtil.ExecuteSql(objTX, "DELETE FROM V_RETAIL_CONTACT_INFO");
               OracleDBUtil.ExecuteSql(objTX, "DELETE FROM V_RETAIL_SALESORG_AREA");
               OracleDBUtil.Insert(ds.Tables["V_RETAIL_CONTACT_INFO"]);
               OracleDBUtil.Insert(ds.Tables["V_RETAIL_SALESORG_AREA"]);

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


       public DataTable Query_OLD_V_RETAIL_SALESORG_AREA2()
       {
           OracleConnection oCon = null;
           try
           {
               oCon = OracleDBUtil.GetERPPOSConnection();

               System.Text.StringBuilder sb = new System.Text.StringBuilder();
               sb.Length = 0;
               sb.Append("SELECT * FROM V_RETAIL_SALESORG_AREA  ");
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

       public DataTable Query_OLD_V_RETAIL_CONTACT_INFO2()
       {
           OracleConnection oCon = null;
           try
           {
               oCon = OracleDBUtil.GetERPPOSConnection();

               System.Text.StringBuilder sb = new System.Text.StringBuilder();
               sb.Length = 0;
               sb.Append("SELECT * FROM V_RETAIL_CONTACT_INFO  ");
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

       public string PK_CONVERT_SP_STORE_PORTAL_UPDATE()
       {
           OracleConnection objConn = null;
           OracleTransaction objTX = null;
           string sRet = "";
           try
           {
               objConn = OracleDBUtil.GetConnection();
               objTX = objConn.BeginTransaction();

               //OracleDBUtil.ExecuteSql_SP(objTX, "SP_COUNT_RECOMMANDED");
               OracleCommand oraCmd = new OracleCommand("PK_CONVERT.SP_STORE_PORTAL_UPDATE");
               oraCmd.CommandType = System.Data.CommandType.StoredProcedure;
               oraCmd.Parameters.Add(new OracleParameter("outCODE", OracleType.VarChar, 2000)).Direction = ParameterDirection.Output;
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

       #region 舊方法
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

        public void UpdateAndInsert_Store(DataTable dtAdd,DataTable dtUpd)
        {
            OracleConnection objConn = null;
            OracleTransaction objTX = null;

            try
            {
                objConn = OracleDBUtil.GetConnection();
                objTX = objConn.BeginTransaction();
                if(dtUpd.Rows.Count>0)
                    OracleDBUtil.UPDDATEByUUID(dtUpd, "STORE_NO");
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

        

        public DataTable Query_OLD_V_RETAIL_SALESORG_AREA()
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
                sb.Append("SELECT STOREID AS STORE_NO, SALESCD, '' AS CHNNAL_TYPE,  ");
                sb.Append("STORENAME AS STORE_NAME, REGIONID AS ZONE, SHIPPINGSITE AS BRANCHNO,  ");
                sb.Append("STORETYPE AS STORELEVEL, BUSINESSNO AS UNINO, TAXNO AS TAXNO,  ");
                sb.Append("NONSHIPPINGSITE AS DS_BRANCHNO, COSTCENTER, ENDDT AS CLOSEDATE,  ");
                sb.Append("STARTDT AS STARTDATE, EMAIL, '01' AS COMPANYCODE ,'N' AS ISNEW ");
                sb.Append("FROM V_RETAIL_SALESORG_AREA  ");
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

        public bool Query_Store_No(string Store_No)
        {
            OracleConnection oCon = null;
            try
            {
                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                sb.Append("SELECT count(*) FROM STORE  ");
                sb.Append("WHERE store_no='" + Store_No + "'");
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

       
        public void SP_STORE_PORTAL_UPDATE()
        {
            OracleConnection objConn = null;
            OracleTransaction objTX = null;

            try
            {
                objConn = OracleDBUtil.GetConnection();
                objTX = objConn.BeginTransaction();

                OracleDBUtil.ExecuteSql_SP(objTX, "SP_STORE_PORTAL_UPDATE");

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
       #endregion


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
