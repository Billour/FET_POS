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
   public class INV23_PageHelper
   {
      public static DataTable GetCheckImeiTypeMethodData()
      {
         INV23_Facade _INV23_Facade = new INV23_Facade();
         DataTable dt = new DataTable();
         dt = _INV23_Facade.QueryCheckImeiTypeMethodData();

         return dt;
      }
      public static DataTable GetLocMethodData()
      {

         INV23_Facade _INV23_Facade = new INV23_Facade();

         DataTable dt = new DataTable();
         dt = _INV23_Facade.QueryLocMethodData(false);

         return dt;
      }

      public static DataTable GetEmptyLocMethodData()
      {
         INV23_Facade _INV23_Facade = new INV23_Facade();

         DataTable dt = new DataTable();
         dt = _INV23_Facade.QueryLocMethodData(true);

         return dt;
      }

   }
}
