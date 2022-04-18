using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.OracleClient;

using Advtek.Utility;

using FET.POS.Model.DTO.ConvertApp;

namespace FET.POS.Model.Facade.ConvertApp
{
   public class cPOSATR_Facade : BaseClass
   {
      public DataTable Query_ERPOLDData()
      {

         OracleConnection objConn = null;

         try
         {
            objConn = OracleDBUtil.GetERPPOSConnection();

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append(
                              @"SELECT 
                                                BRANCHNO, ITEMCODE, LOADDATE, COMPANYCODE,
                                                ATRQTY, DWNFLAG, DWNDATE
                              FROM ERP_ATR");
            sb.Append(@" WHERE to_char(LOADDATE,'YYYY/MM/DD') = to_char(sysdate,'YYYY/MM/DD') AND DWNFLAG = 'T' ");

            DataTable dt = OracleDBUtil.GetDataSet(objConn, sb.ToString()).Tables[0];

            return dt;
         }
         catch (Exception ex)
         {
            throw ex;
         }
         finally
         {
          
             if (objConn.State == ConnectionState.Open) objConn.Close();
             objConn.Dispose();
             OracleConnection.ClearAllPools();
         }

      }

      public DataTable Query_ERPPOSData()
      {

         OracleConnection objConn = null;

         try
         {
            objConn = OracleDBUtil.GetConnection();

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append(
                              @"SELECT 
                                                BRANCHNO, ITEMCODE, LOADDATE, COMPANYCODE,
                                                ATRQTY, DWNFLAG, DWNDATE
                              FROM ERP_ATR");
            sb.Append(@" WHERE to_char(LOADDATE,'YYYY/MM/DD') = to_char(sysdate,'YYYY/MM/DD') AND DWNFLAG = 'Y' ");

            DataTable dt = OracleDBUtil.GetDataSet(objConn, sb.ToString()).Tables[0];

            return dt;
         }
         catch (Exception ex)
         {
            throw ex;
         }
         finally
         {
             if (objConn.State == ConnectionState.Open) objConn.Close();
             objConn.Dispose();
             OracleConnection.ClearAllPools();
         }

      }

      public DataTable Query_ERPDSOLDData()
      {

         OracleConnection objConn = null;

         try
         {
            objConn = OracleDBUtil.GetERPPOSConnection();

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append(
                                   @"SELECT 
                                       BRANCHNO, PRODNO, DWNDATE, 
                                       ATRQTY, NVL(PRICE,'0') PRICE, NVL(VENDORNAME,'') VENDORNAME, 
                                       NVL(DWNFLAG,'0') DWNFLAG, TRANSDATE
                                    FROM ERP_DS_ATR");
            sb.Append(@" WHERE to_char(DWNDATE,'YYYY/MM/DD') = to_char(sysdate,'YYYY/MM/DD') AND DWNFLAG = 'T' ");

            DataTable dt = OracleDBUtil.GetDataSet(objConn, sb.ToString()).Tables[0];

            return dt;
         }
         catch (Exception ex)
         {
            throw ex;
         }
         finally
         {
             if (objConn.State == ConnectionState.Open) objConn.Close();
             objConn.Dispose();
             OracleConnection.ClearAllPools();
         }

      }

      public DataTable Query_ERPDSPOSData()
      {

         OracleConnection objConn = null;

         try
         {
            objConn = OracleDBUtil.GetConnection();

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append(
                                   @"SELECT 
                                       BRANCHNO, PRODNO, DWNDATE, 
                                       ATRQTY, PRICE, VENDORNAME, 
                                       DWNFLAG, TRANSDATE
                                    FROM ERP_DS_ATR");
            sb.Append(@" WHERE to_char(DWNDATE,'YYYY/MM/DD') = to_char(sysdate,'YYYY/MM/DD') AND DWNFLAG = 'Y' ");

            DataTable dt = OracleDBUtil.GetDataSet(objConn, sb.ToString()).Tables[0];

            return dt;
         }
         catch (Exception ex)
         {
            throw ex;
         }
         finally
         {
             if (objConn.State == ConnectionState.Open) objConn.Close();
             objConn.Dispose();
             OracleConnection.ClearAllPools();
         }

      }

      public void InsertNDS_ARTData(cPOSATR_DataSet_DTO ds)
      {
         OracleConnection objConn = null;
         OracleTransaction objTX = null;

         try
         {
            objConn = OracleDBUtil.GetConnection();
            objTX = objConn.BeginTransaction();

            OracleDBUtil.Insert(ds.Tables["ERP_ATR"]);

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

      public void InsertDS_ARTData(cPOSATR_DataSet_DTO ds)
      {
         OracleConnection objConn = null;
         OracleTransaction objTX = null;

         try
         {
            objConn = OracleDBUtil.GetConnection();
            objTX = objConn.BeginTransaction();

            OracleDBUtil.Insert(ds.Tables["ERP_DS_ATR"]);

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

      public void InsertPOS_ATRData(cPOSATR_DataSet_DTO ds)
      {
         OracleConnection objConn = null;
         OracleTransaction objTX = null;

         try
         {
            objConn = OracleDBUtil.GetConnection();
            objTX = objConn.BeginTransaction();

            OracleDBUtil.Insert(ds.Tables["POS_ATR"]);

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

      public void UpdateNDS_ARTData(DataRow dr)
      {
         OracleConnection objConn = null;
         OracleTransaction objTX = null;

         try
         {
            objConn = OracleDBUtil.GetConnection();
            objTX = objConn.BeginTransaction();

            StringBuilder sb = new StringBuilder();
            sb.AppendLine("UPDATE ERP_ATR SET ");
            sb.AppendLine("       BRANCHNO = " + OracleDBUtil.SqlStr(dr["BRANCHNO"].ToString()));
            sb.AppendLine("       ,ITEMCODE= " + OracleDBUtil.SqlStr(dr["ITEMCODE"].ToString()));
            sb.AppendLine("       ,LOADDATE= " + OracleDBUtil.TimeStr(Convert.ToDateTime(dr["LOADDATE"]).ToString("yyyy/MM/dd HH:mm:ss")));
            sb.AppendLine("       ,COMPANYCODE= " + OracleDBUtil.SqlStr(dr["COMPANYCODE"].ToString()));
            sb.AppendLine("       ,ATRQTY= " + OracleDBUtil.NumberStr(dr["ATRQTY"].ToString()));
            sb.AppendLine("       ,DWNDATE= " + OracleDBUtil.SqlStr(dr["DWNDATE"].ToString()));
            sb.AppendLine("WHERE to_char(LOADDATE,'YYYY/MM/DD') = to_char(sysdate,'YYYY/MM/DD')  AND DWNFLAG = 'Y' AND ITEMCODE = " + OracleDBUtil.SqlStr(dr["ITEMCODE"].ToString()));

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

      public void UpdateDS_ARTData(DataRow dr)
      {
         OracleConnection objConn = null;
         OracleTransaction objTX = null;

         try
         {
            objConn = OracleDBUtil.GetConnection();
            objTX = objConn.BeginTransaction();

            StringBuilder sb = new StringBuilder();
            sb.AppendLine("UPDATE ERP_DS_ATR SET ");
            sb.AppendLine("       BRANCHNO = " + OracleDBUtil.SqlStr(dr["BRANCHNO"].ToString()));
            sb.AppendLine("       ,PRODNO= " + OracleDBUtil.SqlStr(dr["PRODNO"].ToString()));
            sb.AppendLine("       ,DWNDATE= " + OracleDBUtil.TimeStr(dr["DWNDATE"].ToString()));
            sb.AppendLine("       ,ATRQTY= " + OracleDBUtil.NumberStr(dr["ATRQTY"].ToString()));
            sb.AppendLine("       ,PRICE= " + OracleDBUtil.NumberStr(dr["PRICE"].ToString()));
            sb.AppendLine("       ,VENDORNAME= " + OracleDBUtil.SqlStr(dr["VENDORNAME"].ToString()));
            sb.AppendLine("WHERE to_char(LOADDATE,'YYYY/MM/DD') = to_char(sysdate,'YYYY/MM/DD')  AND DWNFLAG = 'Y' AND PRODNO = " + OracleDBUtil.SqlStr(dr["PRODNO"].ToString()));

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

      public int UpdateERPATR_STATUS()
      {
         int intResult = 0;
         OracleConnection objConn = null;
         OracleTransaction objTX = null;
         try
         {
            objConn = OracleDBUtil.GetERPPOSConnection();
            objTX = objConn.BeginTransaction();

            string strSQL = @"UPDATE ERP_ATR SET DWNFLAG = 'T'
                                                   WHERE to_char(LOADDATE,'YYYY/MM/DD') = to_char(sysdate,'YYYY/MM/DD')  AND DWNFLAG = '0' ";

            intResult = OracleDBUtil.ExecuteSql(objTX, strSQL);
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
         return intResult;

      }

      public int UpdateERPATR_TO_POSATR_STATUS()
      {
         int intResult = 0;
         OracleConnection objConn = null;
         OracleTransaction objTX = null;
         try
         {
            objConn = OracleDBUtil.GetERPPOSConnection();
            objTX = objConn.BeginTransaction();

            string strSQL = @"UPDATE ERP_ATR SET DWNFLAG = 'Y'
                                                   WHERE to_char(LOADDATE,'YYYY/MM/DD') = to_char(sysdate,'YYYY/MM/DD')  AND DWNFLAG = 'T' ";

            intResult = OracleDBUtil.ExecuteSql(objTX, strSQL);
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
         return intResult;

      }


      public int UpdateERPDSATR_STATUS()
      {
         int intResult = 0;
         OracleConnection objConn = null;
         OracleTransaction objTX = null;
         try
         {
            objConn = OracleDBUtil.GetERPPOSConnection();
            objTX = objConn.BeginTransaction();

            string strSQL = @"UPDATE ERP_DS_ATR SET DWNFLAG = 'T'
                                                   WHERE to_char(DWNDATE,'YYYY/MM/DD') = to_char(sysdate,'YYYY/MM/DD')  AND DWNFLAG='1' ";

            intResult = OracleDBUtil.ExecuteSql(objTX, strSQL);
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
         return intResult;

      }

      public int UpdateERPDSATR_TO_POSDSATR_STATUS()
      {
         int intResult = 0;
         OracleConnection objConn = null;
         OracleTransaction objTX = null;
         try
         {
            objConn = OracleDBUtil.GetERPPOSConnection();
            objTX = objConn.BeginTransaction();

            string strSQL = @"UPDATE ERP_DS_ATR SET DWNFLAG = 'Y'
                                                   WHERE to_char(DWNDATE,'YYYY/MM/DD') = to_char(sysdate,'YYYY/MM/DD')  AND DWNFLAG = 'T' ";

            intResult = OracleDBUtil.ExecuteSql(objTX, strSQL);
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
         return intResult;

      }

      //更新sys para LAST_ATR_TRAN_TO_STORE
      public void Update_LAST_ATR_TRAN_TO_STORE()
      {
          OracleConnection objConn = null;
          try
          {
              System.Text.StringBuilder sb = new System.Text.StringBuilder();
              sb.Append("update sys_para set para_value=to_char(sysdate,'yyyymmddhh24mi') where para_key='LAST_ATR_TRAN_TO_STORE'");
              objConn = OracleDBUtil.GetConnection();
              OracleDBUtil.ExecuteSql(objConn,sb.ToString());
          }
          catch (Exception ex)
          {
              throw ex;
          }
          finally
          {
              if (objConn.State == ConnectionState.Open) objConn.Close();
              objConn.Dispose();
              OracleConnection.ClearAllPools();
          }
      }
   }
}
