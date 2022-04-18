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
   public class CostCenter_Facade
   {
       public DataTable Query_CostCenter(string sCostNo, string sCostName)
       {

           StringBuilder sb = new StringBuilder();
           sb.AppendLine(@" SELECT COST_CENTER_NO 
                                 , COST_CENTER_NAME 
                            FROM   COST_CENTER 
                            WHERE 1 = 1 
                        ");
           if (!string.IsNullOrEmpty(sCostNo))
           {
               sb.AppendLine(" AND COST_CENTER_NO = " + OracleDBUtil.SqlStr(sCostNo));
           }

           if (!string.IsNullOrEmpty(sCostName))
           {
               sb.AppendLine(" AND COST_CENTER_NAME LIKE " + OracleDBUtil.LikeStr(sCostName));
           }

           DataTable dt = OracleDBUtil.Query_Data(sb.ToString());
           return dt;
       }

       public DataTable Query_CostCenterByNo(string sCostNo)
       {

           StringBuilder sb = new StringBuilder();
           sb.AppendLine(@" SELECT COST_CENTER_NO 
                                 , COST_CENTER_NAME 
                            FROM   COST_CENTER 
                            WHERE 1 = 1 
                              AND COST_CENTER_NO = " + OracleDBUtil.SqlStr(sCostNo)
                        );

           DataTable dt = OracleDBUtil.Query_Data(sb.ToString());
           return dt;
       }

   }
}
