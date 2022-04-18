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
   public class INV17_PageHelper
   {
      public static DataTable GetCutOffDateMethodData(string sDate, string eDate)
      {
         INV17_Facade _INV17_Facade = new INV17_Facade();

         DataTable dt = new DataTable();
         dt = _INV17_Facade.QueryCutOffDateMethodData(sDate, eDate,"",false);

         return dt;
      }

      public static DataTable GetEmptyCutOffDateMethodData()
      {
         INV17_Facade _INV17_Facade = new INV17_Facade();

         DataTable dt = new DataTable();
         dt = _INV17_Facade.QueryCutOffDateMethodData("", "","", true);

         return dt;
      }

      public static DataTable GetCutOffDateMethodDataByKey(string eKey)
      {
         INV17_Facade _INV17_Facade = new INV17_Facade();

         DataTable dt = new DataTable();
         dt = _INV17_Facade.QueryCutOffDateMethodData("", "", eKey, false);

         return dt;
      }

      public static void DeleteCutOffDateMethodData(DataTable dt,string eKey)
      {
         OracleDBUtil.DELETEByUUID(dt, eKey);
      }
   }
}
