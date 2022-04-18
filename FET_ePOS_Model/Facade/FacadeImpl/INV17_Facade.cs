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
   public class INV17_Facade
   {
       public DataTable QueryCutOffDateMethodData(string S_DATE, string E_DATE, string eKey, bool Empty)
       {
           StringBuilder sb = new StringBuilder();
           sb.Append("SELECT CUT_OFF_DATE_ID, ");
           sb.Append("       CUT_YYMM, ");
           sb.Append("       CUT_YEAR, ");
           sb.Append("       CUT_OFF_MM, ");
           sb.Append("       to_char(CUT_OFF_DATE,'YYYY/MM/DD') CUT_OFF_DATE, ");
           //sb.Append("       NVL((SELECT EMPNAME FROM EMPLOYEE WHERE EMPNO = CREATE_USER),''), ");
           sb.Append("       CREATE_USER, ");
           sb.Append("       CREATE_DTM, ");
           sb.Append("       (SELECT EMPNAME FROM EMPLOYEE WHERE EMPNO = D.MODI_USER) MODI_USER, ");
           sb.Append("       to_char(MODI_DTM,'YYYY/MM/DD HH24:MI:SS') MODI_DTM ");
           sb.Append("FROM   CUT_OFF_DATE D ");


           if (!Empty)
           {
               sb.Append("WHERE 1 = 1");

               if (!string.IsNullOrEmpty(eKey))
               {
                   sb.Append(" AND CUT_OFF_DATE_ID = " + OracleDBUtil.SqlStr(eKey));
               }
               else
               {
                   if (!string.IsNullOrEmpty(S_DATE) && !string.IsNullOrEmpty(E_DATE))
                   {
                       sb.Append(" AND CUT_OFF_DATE BETWEEN " + OracleDBUtil.TimeStr(S_DATE));
                       sb.Append(" AND " + OracleDBUtil.TimeStr(E_DATE));
                   }
                   if (!string.IsNullOrEmpty(S_DATE) && string.IsNullOrEmpty(E_DATE))
                   {
                       sb.Append(" AND CUT_OFF_DATE >= " + OracleDBUtil.TimeStr(S_DATE));
                   }
                   if (string.IsNullOrEmpty(S_DATE) && !string.IsNullOrEmpty(E_DATE))
                   {
                       sb.Append(" AND CUT_OFF_DATE <= " + OracleDBUtil.TimeStr(E_DATE));
                   }
               }
           }
           else
           {
               //***20101130 ray 預設帶今年度資料
               sb.Append("WHERE CUT_YEAR=TO_CHAR(SYSDATE,'YYYY') ");
           }
           //***20101130 ray 依關帳日升冪排列
           sb.Append(" Order by CUT_OFF_DATE ");

           DataTable dt = OracleDBUtil.Query_Data(sb.ToString());
           return dt;
       }

      public void InsertCutOffDateMethodData(INV17_CUTOFFDATESet_DTO ds)
      {
         OracleConnection objConn = null;
         OracleTransaction objTX = null;

         try
         {
            objConn = OracleDBUtil.GetConnection();
            objTX = objConn.BeginTransaction();

            OracleDBUtil.Insert(objTX,ds.Tables["CUT_OFF_DATE"]);

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

      public void UpdateCutOffDateMethodData(INV17_CUTOFFDATESet_DTO ds)
      {
         OracleConnection objConn = null;
         OracleTransaction objTX = null;

         try
         {
            objConn = OracleDBUtil.GetConnection();
            objTX = objConn.BeginTransaction();

            OracleDBUtil.UPDDATEByUUID(objTX,ds.Tables["CUT_OFF_DATE"], "CUT_OFF_DATE_ID");

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

      public void DeleteCutOffDateMethodData(INV17_CUTOFFDATESet_DTO ds)
      {
         OracleConnection objConn = null;
         OracleTransaction objTX = null;

         try
         {
            objConn = OracleDBUtil.GetConnection();
            objTX = objConn.BeginTransaction();

            OracleDBUtil.DELETEByUUID(objTX,ds.Tables["CUT_OFF_DATE"], "CUT_OFF_DATE_ID");

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

