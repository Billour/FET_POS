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
   public class INV03_Facade
   {
       public DataTable Query_LOCInfo()
       {
           StringBuilder sb = new StringBuilder();
           sb.Append("SELECT  ");
           sb.Append("       LOC_ID, ");
           sb.Append("       STOCK_NAME ");
           sb.Append("FROM   LOC ");
           sb.Append("WHERE 1 = 1 ");

           DataTable dt = OracleDBUtil.Query_Data(sb.ToString());
           return dt;
       }

       public DataTable Query_INVONHANDCURRENTSet(
          string strZone,
          string strStoreNo,
          string strStoreName,
          string strProdType,
          string strProdNo,
          string strProdName,
          string strLoc)
       {
           StringBuilder sb = new StringBuilder();
           sb.Append("SELECT ZONE, ");
           sb.Append("       ZONE_NAME, ");
           sb.Append("       STORE_NO, ");
           sb.Append("       STORENAME, ");
           sb.Append("       PRODNO, ");
           sb.Append("       PRODNAME, ");
           sb.Append("       PRODTYPENAME, ");
           sb.Append("       STOCK_ID, ");
           sb.Append("       STOCK_NAME, ");
           sb.Append("       ON_HAND_QTY ");
           sb.Append("FROM   VW_INV03_SELECT ");
           sb.Append("WHERE 1 = 1  ");

           if (!string.IsNullOrEmpty(strZone))
           {
               sb.Append(" AND ZONE = " + OracleDBUtil.SqlStr(strZone));
           }
           if (!string.IsNullOrEmpty(strStoreNo))
           {
               sb.Append(" AND STORE_NO = " + OracleDBUtil.SqlStr(strStoreNo));
           }
           if (!string.IsNullOrEmpty(strStoreName))
           {
               sb.Append(" AND STORENAME LIKE " + OracleDBUtil.LikeStr(strStoreName));
           }
           if (!string.IsNullOrEmpty(strProdType))
           {
               sb.Append(" AND PRODTYPENO = " + OracleDBUtil.SqlStr(strProdType));
           }
           if (!string.IsNullOrEmpty(strProdNo))
           {
               sb.Append(" AND PRODNO LIKE " + OracleDBUtil.LikeStr(strProdNo));
           }
           if (!string.IsNullOrEmpty(strProdName))
           {
               sb.Append(" AND PRODNAME LIKE " + OracleDBUtil.LikeStr(strProdName));
           }
           if (!string.IsNullOrEmpty(strLoc))
           {
               sb.Append(" AND STOCK_ID = " + OracleDBUtil.SqlStr(strLoc));
           }

           sb.Append(" AND rownum <= 500");
           sb.Append(" Order by ZONE_NAME,STORE_NO,PRODNO");

           DataTable dt = OracleDBUtil.Query_Data(sb.ToString());
           return dt;
       }
   }
}
