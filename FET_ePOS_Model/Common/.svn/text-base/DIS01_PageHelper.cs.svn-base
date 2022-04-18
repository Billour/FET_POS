using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Advtek.Utility;
using System.Data.OracleClient;
using System.Data;
using FET.POS.Model.Facade.FacadeImpl;

namespace FET.POS.Model.Common
{
   public class DIS01_PageHelper : BaseClass
   {
      public static DataTable GetStoreDataByKey(string STORENO)
      {
          Store_Facade _Store_Facade = new Store_Facade();

         DataTable dt = new DataTable();
         dt = _Store_Facade.Query_StoreZone_ByKey(STORENO);
         return dt;
      }

      public static DataTable GetProdDataByKey(string PRODNO)
      {
         DIS01_Facade _DIS01_Facade = new DIS01_Facade();

         DataTable dt = new DataTable();
         dt = new Product_Facade().Query_ProductInfo(PRODNO);
         return dt;
      }

      public static DataTable GetPromoDataByKey(string PROMONO)
      {
         DIS01_Facade _DIS01_Facade = new DIS01_Facade();

         DataTable dt = new DataTable();
         dt = _DIS01_Facade.Query_PromoData_ByKey(PROMONO);
         return dt;
      }

      public static string GetProdTypeNameByKey(string ProdTypeNo)
      {
         DIS01_Facade _DIS01_Facade = new DIS01_Facade();

         DataTable dt = new DataTable();
         dt = _DIS01_Facade.Query_ProdType_ByKey(ProdTypeNo);
         string returnData = dt.Rows[0]["PRODTYPENAME"].ToString();

         return returnData;
      }
      public static string GetCostCenterNameByKey(string CostNo)
      {
         string returnData = null; ;
         DIS01_Facade _DIS01_Facade = new DIS01_Facade();

         DataTable dt = new DataTable();
         dt = _DIS01_Facade.Query_CostCenterByKey(CostNo);
         if (dt.Rows.Count > 0)
         {
            returnData = dt.Rows[0]["COST_CENTER_NAME"].ToString();
         }
         else
         {
            returnData = "";
         }
         return returnData;
      }
      public static string GetDiscountMasterIDByKey(string DNO, string sDate)
      {
         string returnData = null; ;
         DIS01_Facade _DIS01_Facade = new DIS01_Facade();

         DataTable dt = new DataTable();
         dt = _DIS01_Facade.Query_DiscountMasterByKey(DNO, sDate);
         returnData = dt.Rows[0]["CountDMID"].ToString();

         return returnData;
      }
   }
}
