using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.OracleClient;
using Advtek.Utility;
using FET.POS.Model.Facade.FacadeImpl;

namespace FET.POS.Model.Common
{
   public class ORD08_PageHelper
   {
      public static DataTable GetNDSMasterMethodData(string eKey)
      {
         ORD08_Facade _ORD08_Facade = new ORD08_Facade();

         DataTable dt = new DataTable();

         dt = _ORD08_Facade.QueryNDSMasterMethodData(eKey);
            
         return dt;
      }
 
      public static DataTable GetNDSMasterMethodDataStatus(string eKey)
      {
         ORD08_Facade _ORD08_Facade = new ORD08_Facade();

         DataTable dt = new DataTable();
         dt = _ORD08_Facade.QueryNDSMasterMethodDataStatus(eKey);

         return dt;
      }

      public static DataTable GetNDSMasterMethodDataProdInfo(string eKey)
      {
          OracleConnection objConn = null;

          DataTable dt = new DataTable();
          dt.Columns.Add("PRODNO", typeof(string));
          dt.Columns.Add("PRODNAME", typeof(string));
          dt.Columns.Add("ATRQTY", typeof(string));
          dt.Columns.Add("ERROR", typeof(string));

          try
          {
              objConn = OracleDBUtil.GetConnection();

              DataRow NewRow = dt.NewRow();

              DataTable dt1 = new Product_Facade().Query_ProductInfo(eKey);

              DataTable dt2 = new DataTable();
              if (dt1 != null && dt1.Rows.Count > 0)
              {
                  dt2 = new Product_Facade().Query_PRODUCT_ATR(eKey);

                  if (dt2 != null && dt2.Rows.Count > 0)
                  {
                      if (Convert.ToInt32(dt2.Rows[0]["ATRQTY"].ToString()) > 0)
                      {
                          NewRow["PRODNO"] = dt2.Rows[0]["PRODNO"].ToString();
                          NewRow["PRODNAME"] = dt2.Rows[0]["PRODNAME"].ToString();
                          NewRow["ATRQTY"] = dt2.Rows[0]["ATRQTY"].ToString();
                          NewRow["ERROR"] = "";
                      }
                      else NewRow["ERROR"] = "【" + eKey + "】ATR不足，不允許主配， 請重新輸入";
                  }
                  else
                  {
                      NewRow["ERROR"] = "【" + eKey + "】ATR不足，不允許主配， 請重新輸入";
                  }
              }
              else
              {
                  NewRow["ERROR"] = "商品不存在";
              }

              dt.Rows.Add(NewRow);

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

      public static DataTable GetStoreWeightDistributeInfo()
      {
          StringBuilder strSql = new StringBuilder();
          strSql.Append("SELECT STORE_NO FROM STORE  ");
          strSql.Append("WHERE 1 =1   ");
          strSql.Append("AND TO_CHAR(SYSDATE,'yyyyMMdd') <= NVL(CLOSEDATE,'99991231') ");   //不包含已關閉的門市
          strSql.Append("AND (TO_CHAR(SYSDATE,'yyyyMMdd') < NVL(STOP_BDATE, TO_CHAR(SYSDATE+1,'yyyyMMdd'))  ");
          strSql.Append("OR  TO_CHAR(SYSDATE,'yyyyMMdd') > NVL(STOP_EDATE,TO_CHAR(SYSDATE-1,'yyyyMMdd'))) "); //也不包含暫停營業的門市
          strSql.Append("AND STORE_NO NOT IN (SELECT STORE_NO FROM STORE_WEIGHT_DISTRIBUTE)");

          DataTable dt = OracleDBUtil.Query_Data(strSql.ToString());
          return dt;
      }

      /// <summary>
      /// 取得Excel的資料(Master Data)
      /// </summary>
      /// <param name="Batch_No">上傳批號</param>
      /// <returns>查詢結果</returns>
      public static DataTable GetNDSMasterData_Temp(string Batch_No)
      {
          StringBuilder sb = new StringBuilder();
          sb.AppendLine(" SELECT DISTINCT M.BATCH_NO AS HQ_ORDER_M_ID ");
          sb.AppendLine(" , '' AS HQ_NDS_ORDER_NO ");
          sb.AppendLine(" , F8 AS HQ_ORDER_D ");
          sb.AppendLine(" , M.F2 AS PRODNO ");
          sb.AppendLine(" , P.PRODNAME ");
          sb.AppendLine(" ,NVL(F5,'0') AS ATR_QTY ");
          sb.AppendLine(" , 'false' AS AUTO_DIS_FLAG ");
          sb.AppendLine(" , F7 AS  DIS_QTY ");
          sb.AppendLine(" , '' AS REMARK ");
          sb.AppendLine(" , '' AS STATUS ");
          sb.AppendLine(" ,'' AS MODI_USER ");
          sb.AppendLine(" , TO_CHAR(sysdate,'YYYY/mm/DD hh24:mm:ss') AS MODI_DTM ");
          sb.AppendLine(" FROM UPLOAD_TEMP M, PRODUCT P  ");
          sb.AppendLine(" WHERE M.F2 = P.PRODNO(+) ");
          sb.AppendLine(" AND BATCH_NO = " + OracleDBUtil.SqlStr(Batch_No));
          sb.AppendLine(" ORDER BY F2 ");

          DataTable dt = OracleDBUtil.Query_Data(sb.ToString());
          return dt;
      }

      /// <summary>
      /// 取得Excel的資料(Detail Data)
      /// </summary>
      /// <param name="Batch_No">上傳批號</param>
      /// <param name="PRODNO">商品料號</param>
      /// <returns>查詢結果</returns>
      public static DataTable GetNDSDetailData_Temp(string Batch_No, string PRODNO)
      {
          StringBuilder sb = new StringBuilder();
          sb.Append(" SELECT SID AS HQ_ORDER_STORE, ");
          sb.Append(" F8 AS HQ_ORDER_D, ");
          sb.Append(" M.F1 AS STORE_NO, M.F2 AS PRODNO, S.STORENAME, ");
          sb.Append(" TO_NUMBER(NVL(F3,'0')) AS  ASSIGN_QTY, F6 AS LOC_ID, W.WEIGHT ");
          sb.Append(" FROM UPLOAD_TEMP M, STORE S, STORE_WEIGHT_DISTRIBUTE W ");
          sb.Append(" WHERE M.F1 = S.STORE_NO(+) AND M.F1 = W.STORE_NO(+) ");
          sb.Append(" AND BATCH_NO = " + OracleDBUtil.SqlStr(Batch_No));

          if (!string.IsNullOrEmpty(PRODNO))
          {
              sb.Append(" AND F2 = " + OracleDBUtil.SqlStr(PRODNO));
          }

          DataTable dt = OracleDBUtil.Query_Data(sb.ToString());
          return dt;

      }

      /// <summary>
      /// 檢查Non-DropShipment主配作業Excel上傳檔資料(Temp)
      /// </summary>
      /// <param name="BATCH_NO">上傳批號</param>
      /// <param name="New_BATCH_NO">比對完資料後，重新定義上傳批號</param>
      /// <param name="NDNO">主配單號</param>
      public static void Check_ImportData(string BATCH_NO, string New_BATCH_NO, string NDNO)
      {
          OracleConnection objConn = null;
          OracleTransaction objTX = null;

          try
          {
              objConn = OracleDBUtil.GetConnection();
              objTX = objConn.BeginTransaction();

              OracleDBUtil.ExecuteSql_SP(
                 objTX
                 , "SP_CHECK_HQNDSORDER"
                 , new OracleParameter("v_HQ_NDS_ORDER_NO", NDNO)
                 , new OracleParameter("v_BATCHNO", BATCH_NO)
                 , new OracleParameter("v_New_BATCHNO", New_BATCH_NO)
                 );

              objTX.Commit();

          }
          catch (Exception ex)
          {
              objTX.Rollback();
              throw ex;
          }
          finally
          {
              objTX = null;

              if (objConn.State == ConnectionState.Open) objConn.Close();
              objConn.Dispose();
              OracleConnection.ClearAllPools();

          }
      }

      /// <summary>
      /// 取得總部主配訂單號碼(HQ_NDS_ORDER_NO)
      /// </summary>
      /// <returns></returns>
      public static string GetNDS_ORDER_NO()
      {
          StringBuilder sb = new StringBuilder();
          sb.Append("SELECT PARA_VALUE FROM SYS_PARA where PARA_KEY='HR_ORDER_NO' ");

          DataTable dt = OracleDBUtil.Query_Data(sb.ToString());
          string strNO = "";
          if (dt.Rows.Count > 0)
          {
              strNO = dt.Rows[0]["PARA_VALUE"].ToString();
          }
          return strNO;
      }

      /// <summary>
      /// 取得總部主配訂單特定的一筆資料
      /// </summary>
      /// <param name="HQ_NDS_ORDER_NO">主配單號</param>
      /// <returns>查詢結果</returns>
      public static DataTable Query_HQNDSORDERM_ByKey(string HQ_NDS_ORDER_NO)
      {
          StringBuilder sb = new StringBuilder();
          sb.Append("SELECT * ");
          sb.Append("FROM HQ_NDS_ORDER_M ");
          sb.Append("WHERE HQ_NDS_ORDER_NO = " + OracleDBUtil.SqlStr(HQ_NDS_ORDER_NO));

          DataTable dt = OracleDBUtil.Query_Data(sb.ToString());
          return dt;
      }

      /// <summary>
      /// 取得總部主配訂單商品檔特定的一筆資料
      /// </summary>
      /// <param name="HQ_ORDER_D">UUID</param>
      /// <returns>查詢結果</returns>
      public static DataTable Query_HQNDSORDERD_ByKey(string HQ_ORDER_D)
      {
          StringBuilder sb = new StringBuilder();
          sb.Append("SELECT * ");
          sb.Append("FROM HQ_NDS_ORDER_D ");
          sb.Append("WHERE HQ_ORDER_D = " + OracleDBUtil.SqlStr(HQ_ORDER_D));

          DataTable dt = OracleDBUtil.Query_Data(sb.ToString());
          return dt;
      }

      /// <summary>
      /// 取得總部主配訂單門市檔特定的一筆資料
      /// </summary>
      /// <param name="HQ_ORDER_STORE">UUID</param>
      /// <returns>查詢結果</returns>
      public static DataTable Query_HQNDSORDERS_ByKey(string HQ_ORDER_STORE)
      {
          StringBuilder sb = new StringBuilder();
          sb.Append("SELECT * ");
          sb.Append("FROM HQ_NDS_ORDER_STORE ");
          sb.Append("WHERE HQ_ORDER_STORE = " + OracleDBUtil.SqlStr(HQ_ORDER_STORE));

          DataTable dt = OracleDBUtil.Query_Data(sb.ToString());
          return dt;
      }

      /// <summary>
      /// 取得主配作業狀態不為3(已轉門市訂單)的資料
      /// </summary>
      /// <returns></returns>
      public static DataTable GetHQNDSORDERM_STATUS()
      {
          StringBuilder sb = new StringBuilder();
          sb.Append("SELECT STATUS ");
          sb.Append("FROM HQ_NDS_ORDER_M ");
          sb.Append("WHERE STATUS <> '3' ");

          DataTable dt = OracleDBUtil.Query_Data(sb.ToString());
          return dt;
      }

      /// <summary>
      /// 取得出貨倉別
      /// </summary>
      /// <returns></returns>
      public static DataTable GetLOC_ID()
      {
          DataTable dt = null;
          OracleConnection objConn = null;

          try
          {
              StringBuilder strSql = new StringBuilder();
              strSql.Append(" SELECT DISTINCT BRANCHNO AS TEXT, BRANCHNO AS VALUE FROM STORE");
              strSql.Append(" WHERE 1 =1   ");
              strSql.Append(" AND TO_CHAR(SYSDATE,'yyyyMMdd') <= NVL(CLOSEDATE,'99991231') ");   //不包含已關閉的門市
              strSql.Append(" AND (TO_CHAR(SYSDATE,'yyyyMMdd') < NVL(STOP_BDATE, TO_CHAR(SYSDATE+1,'yyyyMMdd'))  ");
              strSql.Append(" OR  TO_CHAR(SYSDATE,'yyyyMMdd') > NVL(STOP_EDATE,TO_CHAR(SYSDATE-1,'yyyyMMdd'))) "); //也不包含暫停營業的門市
              strSql.Append(" AND NVL(BRANCHNO,' ') <> ' ' ");

              objConn= OracleDBUtil.GetConnection();
              dt = OracleDBUtil.GetDataSet(objConn, strSql.ToString()).Tables[0];

              DataRow dr = dt.NewRow();
              dr["TEXT"] = "ALL";
              dr["VALUE"] = "";
              dt.Rows.InsertAt(dr, 0);
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

          return dt;
      }


   }
}
