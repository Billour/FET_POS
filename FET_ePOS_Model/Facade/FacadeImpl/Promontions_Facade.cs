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
   public class Promontions_Facade : BaseClass
   {
       public DataTable Query_Promontions(string sPromoNo, string sPromoName, string sKeyFieldValue) 
       {
           OracleConnection objConn = null;

           try
           {
               objConn = OracleDBUtil.GetConnection();

               System.Text.StringBuilder sb = new System.Text.StringBuilder();
               sb.Append("SELECT  ");
               sb.Append("       PROMO_NO ");
               sb.Append("      ,PROMO_NAME ");
               sb.Append("      ,ROWNUM ItemNo ");
               sb.Append("      ,B_DATE ");
               sb.Append("      ,E_DATE ");
               sb.Append("      ,'' Category ");
               sb.Append("      , UUID ");
               sb.Append("FROM   MM ");
               sb.Append("where 1=1 ");
               //sb.Append("WHERE (E_DATE > sysdate ");
               //sb.Append("OR E_DATE Is Null) ");
               if (!string.IsNullOrEmpty(sPromoNo))
               {
                   sb.Append(" AND PROMO_NO LIKE " + OracleDBUtil.LikeStr(sPromoNo));
               }

               if (!string.IsNullOrEmpty(sPromoName))
               {
                  sb.Append(" AND PROMO_NAME LIKE " + OracleDBUtil.LikeStr( sPromoName));
               }
               if (string.IsNullOrEmpty(sKeyFieldValue) || sKeyFieldValue != "NoDeadline")
                   sb.Append(" AND Sysdate Between NVL(B_DATE, to_date('2000/01/01','YYYY/MM/DD')) And NVL(E_DATE,to_date('3000/01/01','YYYY/MM/DD')) ");

               DataTable dt = OracleDBUtil.GetDataSet(objConn, sb.ToString()).Tables[0];
               dt = SelectTop(500, dt);

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

       public DataTable Query_Promontions(string sPromoNo, string sPromoName)
       {
           OracleConnection objConn = null;

           try
           {
               objConn = OracleDBUtil.GetConnection();

               System.Text.StringBuilder sb = new System.Text.StringBuilder();
               sb.Append("SELECT  ");
               sb.Append("       PROMO_NO ");
               sb.Append("      ,PROMO_NAME ");
               sb.Append("      ,ROWNUM ItemNo ");
               sb.Append("      ,B_DATE ");
               sb.Append("      ,E_DATE ");
               sb.Append("      ,'' Category ");
               sb.Append("      , UUID ");
               sb.Append("FROM   MM ");
               sb.Append("where 1=1 ");
               //sb.Append("WHERE (E_DATE > sysdate ");
               //sb.Append("OR E_DATE Is Null) ");
               if (!string.IsNullOrEmpty(sPromoNo))
               {
                   sb.Append(" AND PROMO_NO = " + OracleDBUtil.SqlStr(sPromoNo));
               }

               if (!string.IsNullOrEmpty(sPromoName))
               {
                   sb.Append(" AND PROMO_NAME LIKE " + OracleDBUtil.LikeStr(sPromoName));
               }
               sb.Append(" AND Sysdate Between NVL(B_DATE, to_date('2000/01/01','YYYY/MM/DD')) And NVL(E_DATE,to_date('3000/01/01','YYYY/MM/DD')) ");

               DataTable dt = OracleDBUtil.GetDataSet(objConn, sb.ToString()).Tables[0];
               dt = SelectTop(500, dt);

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

       public DataTable Query_Promontions(string sPromoNo, string sPromoName, bool isFuzzySearch)
       {
           OracleConnection objConn = null;

           try
           {
               objConn = OracleDBUtil.GetConnection();

               System.Text.StringBuilder sb = new System.Text.StringBuilder();
               sb.Append("SELECT  ");
               sb.Append("       PROMO_NO ");
               sb.Append("      ,PROMO_NAME ");
               sb.Append("      ,ROWNUM ItemNo ");
               sb.Append("      ,B_DATE ");
               sb.Append("      ,E_DATE ");
               sb.Append("      ,'' Category ");
               sb.Append("      , UUID ");
               sb.Append("FROM   MM ");
               sb.Append("where 1=1 ");
               //sb.Append("WHERE (E_DATE > sysdate ");
               //sb.Append("OR E_DATE Is Null) ");
               if (!string.IsNullOrEmpty(sPromoNo))
               {
                   if (isFuzzySearch)
                       sb.Append(" AND PROMO_NO LIKE " + OracleDBUtil.LikeStr(sPromoNo));
                   else 
                       sb.Append(" AND PROMO_NO = " + OracleDBUtil.SqlStr(sPromoNo));
               }

               if (!string.IsNullOrEmpty(sPromoName))
               {
                   sb.Append(" AND PROMO_NAME LIKE " + OracleDBUtil.LikeStr(sPromoName));
               }
               sb.Append(" AND Sysdate Between NVL(B_DATE, to_date('2000/01/01','YYYY/MM/DD')) And NVL(E_DATE,to_date('3000/01/01','YYYY/MM/DD')) ");

               DataTable dt = OracleDBUtil.GetDataSet(objConn, sb.ToString()).Tables[0];
               dt = SelectTop(500, dt);

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

       public DataTable Query_Promontions(string UUID)
       {
           OracleConnection objConn = null;

           try
           {
               objConn = OracleDBUtil.GetConnection();

               System.Text.StringBuilder sb = new System.Text.StringBuilder();
               sb.Append("SELECT  ");
               sb.Append("       PROMO_NO ");
               sb.Append("      ,PROMO_NAME ");
               sb.Append("      ,ROWNUM ItemNo ");
               sb.Append("      ,B_DATE ");
               sb.Append("      ,E_DATE ");
               sb.Append("      ,'' Category ");
               sb.Append("      , UUID ");
               sb.Append("FROM   MM ");
               sb.Append("where 1=1 ");

               if (!string.IsNullOrEmpty(UUID))
               {
                   sb.Append(" AND UUID = " + OracleDBUtil.SqlStr(UUID));
               }

               sb.Append(" AND Sysdate Between NVL(B_DATE, to_date('2000/01/01','YYYY/MM/DD')) And NVL(E_DATE,to_date('3000/01/01','YYYY/MM/DD')) ");

               DataTable dt = OracleDBUtil.GetDataSet(objConn, sb.ToString()).Tables[0];
               dt = SelectTop(500, dt);

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

       private DataTable SelectTop(int Top, DataTable oDT)
       {
           if (oDT.Rows.Count < Top) return oDT;

           DataTable NewTable = oDT.Clone();
           DataRow[] rows = oDT.Select("1=1");
           for (int i = 0; i < Top; i++)
           {
               NewTable.ImportRow((DataRow)rows[i]);
           }
           return NewTable;
       }

   }
}
