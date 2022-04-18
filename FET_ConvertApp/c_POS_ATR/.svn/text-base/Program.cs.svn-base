using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Advtek.Utility;
using System.Threading;
using System.Transactions;
using System.Configuration;
using System.Data.OracleClient;

using FET.POS.Model.DTO;
using FET.POS.Model.Facade;
using FET.POS.Model.Facade.ConvertApp;
using FET.POS.Model.DTO.ConvertApp;
using FET.POS.Model.Common;

namespace c_POS_ATR
{
   class Program
   {
      static cPOSATR_Facade _cPOSATR_Facade;
      static cPOSATR_DataSet_DTO _cPOSATRDataSet_DTO;

      static void Main(string[] args)
      {
          try 
          {
              _cPOSATR_Facade = new cPOSATR_Facade();
              Console.WriteLine("c_POS_ATR");
              Console.WriteLine("初始化LOG");
              ConvertLog cLog = new ConvertLog("c_POS_ATR");
              Console.WriteLine("清除TEMP，ERP_ATR(WEB)+ERP_DS_ATR(WEB)");
              clearTemp();

              DataTable dtSource = new DataTable();
              DataTable dtTarget = new DataTable();
              DataTable dtTargetToPos = new DataTable();
              DataTable dtSource1 = new DataTable();
              DataTable dtTarget1 = new DataTable();
              DataTable dtTargetToPos1 = new DataTable();

              int intResult = 0;
              int intResult1 = 0;

              try
              {
                  //1.更新sys para LAST_ATR_TRAN_TO_STORE
                  Update_LAST_ATR_TRAN_TO_STORE();

                  //2.Update ERP ATR Status for 'T'
                  intResult = UpdateERPATR_STATUS();
                  intResult1 = UpdateERPDSATR_STATUS();

                  if (intResult == 0 && intResult1 == 0)
                  {
                      cLog.Fail("來源無資料。");
                      return;
                  }

                  //3.匯入NDS的ATR
                  if (intResult != 0)
                  {
                      Console.WriteLine("寫入TEMP，ERP_ATR(WEB)");
                      dtSource = Query_ERPOLDData();
                      dtTarget = Query_ERPPOSData();
                      //check insert or update
                      if (dtTarget.Rows.Count == 0)
                      {
                          //insert into db
                          InsertPOSATR_NDSALL(dtSource);
                          cLog.Success("ERP_ATR(WEB)，新增筆數" + dtSource.Rows.Count.ToString());
                      }
                      else
                      {
                          foreach (DataRow dr1 in dtSource.Rows)
                          {
                              if (dtTarget.AsEnumerable().Any(dr => dr.Field<string>("ITEMCODE").Equals(dr1["ITEMCODE"])))
                              {
                                  DataRow drUp = dtTarget.AsEnumerable().Single(dr => dr.Field<string>("ITEMCODE") == dr1["ITEMCODE"].ToString());
                                  UpdatePOSATR_NDS(drUp);
                              }
                          }
                          cLog.Success("ERP_ATR(WEB)，更新筆數" + dtSource.Rows.Count.ToString());
                      }
                      
                  }

                  //4.匯入DS的ATR
                  if (intResult1 != 0)
                  {
                      dtSource1 = _cPOSATR_Facade.Query_ERPDSOLDData();
                      dtTarget1 = _cPOSATR_Facade.Query_ERPDSPOSData();
                      Console.WriteLine("寫入TEMP，ERP_DS_ATR(WEB)");
                      //check insert or update
                      if (dtTarget1.Rows.Count == 0)
                      {
                          //insert into db
                          InsertPOSATR_DSALL(dtSource1);
                          cLog.Success("ERP_DS_ATR(POS)=>ERP_DS_ATR(WEB)INSERT，新增筆數" + dtSource1.Rows.Count.ToString());
                      }
                      else
                      {
                          foreach (DataRow dr1 in dtSource1.Rows)
                          {
                              if (dtTarget1.AsEnumerable().Any(dr => dr.Field<string>("PRODNO").Equals(dr1["PRODNO"])))
                              {
                                  DataRow drUp = dtTarget1.AsEnumerable().Single(dr => dr.Field<string>("PRODNO") == dr1["PRODNO"].ToString());
                                  UpdatePOSATR_DS(drUp);
                              }
                          }
                          cLog.Success("ERP_DS_ATR(POS)=>ERP_DS_ATR(WEB)UPDATE，異動筆數" + dtSource1.Rows.Count.ToString());
                      }
                      
                  }
                  Console.WriteLine("SP_POS_ATR");
                  string sMsg = PK_CONVERT_SP_POS_ATR();
                  Console.WriteLine("執行結束，寫入LOG");
                  cLog.Success(sMsg);
                  Thread.Sleep(5000);

              }
              catch (Exception ex)
              {
                  cLog.Fail(ex.Message);
                  Console.Write(ex.Message.ToString());
                  Thread.Sleep(5000);
              }
          }
          catch (Exception ex) 
          {
              Console.WriteLine(ex.Message);
              Thread.Sleep(5000);
          }         
      }

      protected static void UpdatePOSATR_NDS(DataRow dr)
      {
         try
         {
            //更新資料庫
            _cPOSATR_Facade.UpdateNDS_ARTData(dr);

         }
         catch (Exception ex)
         {
            throw ex;
         }
      }

      protected static void UpdatePOSATR_DS(DataRow dr)
      {


         try
         {

            //更新資料庫
            _cPOSATR_Facade.UpdateDS_ARTData(dr);

         }
         catch (Exception ex)
         {
            throw ex;
         }
      }

      protected static void InsertPOSATR_NDSALL(DataTable dtNDS)
      {
         _cPOSATR_Facade = new cPOSATR_Facade();
         _cPOSATRDataSet_DTO = new cPOSATR_DataSet_DTO();

         cPOSATR_DataSet_DTO.ERP_ATRDataTable dtERPATR;
         cPOSATR_DataSet_DTO.ERP_ATRRow drERPATR;
         dtERPATR = (cPOSATR_DataSet_DTO.ERP_ATRDataTable)_cPOSATRDataSet_DTO.Tables["ERP_ATR"];

         try
         {
            for (int i = 0; i < dtNDS.Rows.Count; i++)
            {
               drERPATR = dtERPATR.NewERP_ATRRow();
               drERPATR.ITEMCODE = dtNDS.Rows[i]["ITEMCODE"].ToString();
               drERPATR.LOADDATE = DateTime.Parse(dtNDS.Rows[i]["LOADDATE"].ToString());
               drERPATR.DWNFLAG = "Y";
               drERPATR.ATRQTY = decimal.Parse(dtNDS.Rows[i]["ATRQTY"].ToString());
               drERPATR.BRANCHNO = dtNDS.Rows[i]["BRANCHNO"].ToString();
               drERPATR.COMPANYCODE = dtNDS.Rows[i]["COMPANYCODE"].ToString();
               
               dtERPATR.Rows.Add(drERPATR);
            }

            _cPOSATRDataSet_DTO.AcceptChanges();

            //寫回資料庫
            _cPOSATR_Facade.InsertNDS_ARTData(_cPOSATRDataSet_DTO);

            //回寫ERP STATUS
            _cPOSATR_Facade.UpdateERPATR_TO_POSATR_STATUS();

         }
         catch (Exception ex)
         {
            throw ex;
         }
      }

      protected static void InsertPOSATR_DSALL(DataTable dtDS)
      {
         _cPOSATR_Facade = new cPOSATR_Facade();
         _cPOSATRDataSet_DTO = new cPOSATR_DataSet_DTO();

         cPOSATR_DataSet_DTO.ERP_DS_ATRDataTable dtDSERPATR;
         cPOSATR_DataSet_DTO.ERP_DS_ATRRow drDSERPATR;
         dtDSERPATR = (cPOSATR_DataSet_DTO.ERP_DS_ATRDataTable)_cPOSATRDataSet_DTO.Tables["ERP_DS_ATR"];

         try
         {
            for (int i = 0; i < dtDS.Rows.Count; i++)
            {
               drDSERPATR = dtDSERPATR.NewERP_DS_ATRRow();
               drDSERPATR.BRANCHNO = dtDS.Rows[i]["BRANCHNO"].ToString();
               drDSERPATR.PRODNO = dtDS.Rows[i]["PRODNO"].ToString();
               drDSERPATR.ATRQTY = decimal.Parse(dtDS.Rows[i]["ATRQTY"].ToString());
               drDSERPATR.PRICE = decimal.Parse(dtDS.Rows[i]["PRICE"].ToString());
               drDSERPATR.VENDORNAME = dtDS.Rows[i]["VENDORNAME"].ToString();
               drDSERPATR.TRANSDATE = dtDS.Rows[i]["TRANSDATE"].ToString();
               drDSERPATR.DWNFLAG = "Y";
               drDSERPATR.DWNDATE = DateTime.Parse(dtDS.Rows[i]["DWNDATE"].ToString());
               dtDSERPATR.Rows.Add(drDSERPATR);
            }

            _cPOSATRDataSet_DTO.AcceptChanges();

            //寫回資料庫
            _cPOSATR_Facade.InsertDS_ARTData(_cPOSATRDataSet_DTO);
            //回寫ERP STATUS
            _cPOSATR_Facade.UpdateERPDSATR_TO_POSDSATR_STATUS();

         }
         catch (Exception ex)
         {
            throw ex;
         }
      }

      protected static void InsertPOS_ATRNDSALL(DataTable dtNDS)
      {
         _cPOSATR_Facade = new cPOSATR_Facade();
         _cPOSATRDataSet_DTO = new cPOSATR_DataSet_DTO();

         cPOSATR_DataSet_DTO.POS_ATRDataTable dtPOSATR;
         cPOSATR_DataSet_DTO.POS_ATRRow drPOSATR;
         dtPOSATR = (cPOSATR_DataSet_DTO.POS_ATRDataTable)_cPOSATRDataSet_DTO.Tables["POS_ATR"];

         try
         {
            for (int i = 0; i < dtNDS.Rows.Count; i++)
            {
               drPOSATR = dtPOSATR.NewPOS_ATRRow();
               drPOSATR.TYPE = "2";
               drPOSATR.PRODNO = dtNDS.Rows[i]["ITEMCODE"].ToString();
               drPOSATR.LOC_ID = GetGoodLOCUUID();
               drPOSATR.ATRQTY = decimal.Parse(dtNDS.Rows[i]["ATRQTY"].ToString());
               drPOSATR.DWNDATE = DateTime.Parse(dtNDS.Rows[i]["LOADDATE"].ToString());
               drPOSATR.DS_FLAG = "N";
               drPOSATR.TRANS_DATE = DateTime.Now;
               drPOSATR.MODI_DTM = DateTime.Now;
               drPOSATR.MODI_USER = "Convert";
               drPOSATR.CREATE_DTM = DateTime.Now;
               drPOSATR.CREATE_USER = "Convert";
               dtPOSATR.Rows.Add(drPOSATR);
            }

            _cPOSATRDataSet_DTO.AcceptChanges();

            //寫回資料庫
            _cPOSATR_Facade.InsertPOS_ATRData(_cPOSATRDataSet_DTO);

         }
         catch (Exception ex)
         {
            throw ex;
         }
      }

      protected static void InsertPOS_ATRDSALL(DataTable dtDS)
      {
         _cPOSATR_Facade = new cPOSATR_Facade();
         _cPOSATRDataSet_DTO = new cPOSATR_DataSet_DTO();

         cPOSATR_DataSet_DTO.POS_ATRDataTable dtPOSATR;
         cPOSATR_DataSet_DTO.POS_ATRRow drPOSATR;
         dtPOSATR = (cPOSATR_DataSet_DTO.POS_ATRDataTable)_cPOSATRDataSet_DTO.Tables["POS_ATR"];

         try
         {
            for (int i = 0; i < dtDS.Rows.Count; i++)
            {
               drPOSATR = dtPOSATR.NewPOS_ATRRow();
               drPOSATR.TYPE = "1";
               drPOSATR.PRODNO = dtDS.Rows[i]["PRODNO"].ToString();
               drPOSATR.LOC_ID = GetGoodLOCUUID();
               drPOSATR.ATRQTY = decimal.Parse(dtDS.Rows[i]["ATRQTY"].ToString());
               drPOSATR.PRICE = decimal.Parse(dtDS.Rows[i]["PRICE"].ToString());
               drPOSATR.DWNDATE = DateTime.Parse(dtDS.Rows[i]["DWNDATE"].ToString());
               drPOSATR.DS_FLAG = "Y";
               drPOSATR.VENDOR_NAME = dtDS.Rows[i]["VENDORNAME"].ToString();
               drPOSATR.TRANS_DATE = DateTime.Now;
               drPOSATR.MODI_DTM = DateTime.Now;
               drPOSATR.MODI_USER = "Convert";
               drPOSATR.CREATE_DTM = DateTime.Now;
               drPOSATR.CREATE_USER = "Convert";
               dtPOSATR.Rows.Add(drPOSATR);
            }

            _cPOSATRDataSet_DTO.AcceptChanges();

            //寫回資料庫
            _cPOSATR_Facade.InsertPOS_ATRData(_cPOSATRDataSet_DTO);

         }
         catch (Exception ex)
         {
            throw ex;
         }
      }

      //更新sys para LAST_ATR_TRAN_TO_STORE
      public static void Update_LAST_ATR_TRAN_TO_STORE()
      {
          OracleConnection objConn = null;
          try
          {
              System.Text.StringBuilder sb = new System.Text.StringBuilder();
              sb.Append("update sys_para set para_value=to_char(sysdate,'yyyymmddhh24mi') where para_key='LAST_ATR_TRAN_TO_STORE'");
              objConn = OracleDBUtil.GetConnection();
              OracleDBUtil.ExecuteSql(objConn, sb.ToString());
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
      public static void clearTemp()
      {
          OracleConnection objConn = null;
          try
          {
              objConn = OracleDBUtil.GetConnection();
              OracleDBUtil.ExecuteSql(objConn, "delete from ERP_ATR");
              OracleDBUtil.ExecuteSql(objConn, "delete from ERP_DS_ATR");

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

      public static int UpdateERPATR_STATUS()
      {
          int intResult = 0;
          OracleConnection objConn = null;
          OracleTransaction objTX = null;
          try
          {
              objConn = OracleDBUtil.GetERPPOSConnection();
              objTX = objConn.BeginTransaction();

              string strSQL = @"UPDATE ERP_ATR SET DWNFLAG = 'T'
                                                   WHERE to_char(LOADDATE,'YYYY/MM/DD') = to_char(sysdate,'YYYY/MM/DD')  AND nvl(DWNFLAG,'0') = '0' and companycode='01' ";

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

      public static int UpdateERPDSATR_STATUS()
      {
          int intResult = 0;
          OracleConnection objConn = null;
          OracleTransaction objTX = null;
          try
          {
              objConn = OracleDBUtil.GetERPPOSConnection();
              objTX = objConn.BeginTransaction();

              string strSQL = @"UPDATE ERP_DS_ATR SET DWNFLAG = 'T'
                                                   WHERE to_char(DWNDATE,'YYYY/MM/DD') = to_char(sysdate,'YYYY/MM/DD')  AND nvl(DWNFLAG,'0')='0' ";

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

      public static DataTable Query_ERPOLDData()
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

      public static DataTable Query_ERPPOSData()
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

      public static string PK_CONVERT_SP_POS_ATR()
      {
          OracleConnection objConn = null;
          OracleTransaction objTX = null;
          string sRet = "";
          try
          {
              objConn = OracleDBUtil.GetConnection();
              objTX = objConn.BeginTransaction();

              //OracleDBUtil.ExecuteSql_SP(objTX, "PK_CONVERT.SP_EMPLYEE_CONVERT");
              OracleCommand oraCmd = new OracleCommand("SP_POS_ATR");
              oraCmd.CommandType = System.Data.CommandType.StoredProcedure;
              oraCmd.Parameters.Add(new OracleParameter("outCODE", OracleType.VarChar, 50)).Direction = ParameterDirection.Output;
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
              if (objTX != null) objTX.Dispose();
              if (objConn != null)
              {
                  if (objConn.State == ConnectionState.Open) objConn.Close();
                  objConn.Dispose();
              }

          }
          return sRet;
      }

      static string GetGoodLOCUUID()
      {
          DataTable dt = null;
          OracleConnection objConn = null;

          try
          {
              objConn = OracleDBUtil.GetConnection();
              System.Text.StringBuilder sb = new System.Text.StringBuilder();
              sb.Append("select INV_GoodLOCUUID() as UUID from dual");

              dt = OracleDBUtil.GetDataSet(objConn, sb.ToString()).Tables[0];

              string UUID = "";
              if (dt.Rows.Count > 0)
              {
                  UUID = dt.Rows[0]["UUID"].ToString();
              }

              return UUID;
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
