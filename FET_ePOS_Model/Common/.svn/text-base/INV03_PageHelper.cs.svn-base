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
   public class INV03_PageHelper
   {
      /// <summary>
      /// /*倉別 對照表*/
      /// </summary>
      public static DataTable GetLocInfoMethodData(bool setAll)
      {
         INV03_Facade _INV03_Facade = new INV03_Facade();
         DataTable dt = new DataTable();

         dt = _INV03_Facade.Query_LOCInfo();

         if ((setAll))
         {
            DataRow dr = dt.NewRow();
            dr["STOCK_NAME"] = "ALL";
            dr["LOC_ID"] = "";
            dt.Rows.InsertAt(dr, 0);
         }

         return dt;
      }

      /// <summary>
      /// 取得查詢資料
      /// </summary>
      public static DataTable GetStockMethodData(
         string strZone,
         string strStoreNo,
         string strStoreName,
         string strProdType,
         string strProdNo,
         string strProdName,
         string strLoc)
      {
         INV03_Facade _INV03_Facade = new INV03_Facade();
         DataTable dt = new DataTable();

         dt = _INV03_Facade.Query_INVONHANDCURRENTSet
                   (strZone,
                   strStoreNo,
                   strStoreName,
                   strProdType,
                   strProdNo,
                   strProdName,
                   strLoc);

         return dt;
      }
   }
}
