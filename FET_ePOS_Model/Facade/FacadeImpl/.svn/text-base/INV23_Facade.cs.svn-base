using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.OracleClient;
using Advtek.Utility;
using FET.POS.Model.DTO;

namespace FET.POS.Model.Facade.FacadeImpl
{
   public class INV23_Facade
   {
       public DataTable QueryCheckImeiTypeMethodData()
       {
           StringBuilder sb = new StringBuilder();
           sb.Append("SELECT  ");
           sb.Append("       CHECK_IMEI_TYPE, ");
           sb.Append("       CHECK_IMEI_TYPE_NAME ");
           sb.Append("FROM   CHECK_IMEI_TYPE ");
           sb.Append("WHERE 1=1 AND STATUS = '1' ");

           DataTable dt = OracleDBUtil.Query_Data(sb.ToString());
           return dt;
       }

      public int QueryLocNameCount(string STOCK_NAME)
      {
          OracleConnection objConn = null;

          int iRet = 0;
          try
          {
              StringBuilder sb = new StringBuilder();
              sb.Append("SELECT COUNT(*) as CNT FROM LOC WHERE STOCK_NAME=" + OracleDBUtil.SqlStr(STOCK_NAME));

              objConn = OracleDBUtil.GetConnection();
              DataTable dt = OracleDBUtil.GetDataSet(objConn, sb.ToString()).Tables[0];

              if (dt.Rows.Count > 0)
                  iRet = Convert.ToInt32(dt.Rows[0]["CNT"]);
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
          return iRet;
      }

      public DataTable QueryLocMethodData(bool Empty)
      {
          StringBuilder sb = new StringBuilder();
          sb.Append("SELECT ROWNUM ITEMNO, TAB1.LOC_ID, TAB1.STOCK_NAME, TAB1.SALES_FLAG, TAB1.CREATE_USER, ");
          sb.Append("TAB1.CREATE_DTM, TAB1.MODI_USER, TAB1.MODI_USER_NAME, TAB1.MODI_DTM, TAB1.CHECK_IMEI_TYPE, TAB1.CHECK_IMEI_TYPE_NAME, ");
          sb.Append("TAB1.COMPANY_CODE ");
          sb.Append("FROM ( ");
          sb.Append("SELECT LOC_ID, STOCK_NAME, ");
          sb.Append("DECODE(SALES_FLAG,'1','True','0','False','False') AS SALES_FLAG, LOC.CREATE_USER, ");
          sb.Append("LOC.CREATE_DTM, LOC.MODI_USER, E.EMPNAME AS MODI_USER_NAME, LOC.MODI_DTM, LOC.CHECK_IMEI_TYPE, CHECK_IMEI_TYPE.CHECK_IMEI_TYPE_NAME, ");
          sb.Append("COMPANY_CODE ");
          sb.Append("FROM LOC, CHECK_IMEI_TYPE,EMPLOYEE E  ");
          sb.Append("WHERE LOC.CHECK_IMEI_TYPE = CHECK_IMEI_TYPE.CHECK_IMEI_TYPE AND LOC.MODI_USER = E.EMPNO(+) ");
          sb.Append((!Empty ? "AND 1=1 " : "AND 1<>1 "));
          sb.Append("ORDER BY LOC_ID) TAB1 ");

          DataTable dt = OracleDBUtil.Query_Data(sb.ToString());
          return dt;
      }

      public void InsertLocMethodData(INV23_LOCSet_DTO ds)
      {
          OracleDBUtil.Insert(ds.Tables["LOC"]);
      }

      public void UpdateLocMethodData(INV23_LOCSet_DTO ds)
      {
          OracleDBUtil.UPDDATEByUUID(ds.Tables["LOC"], "LOC_ID");
      }
   }
}
