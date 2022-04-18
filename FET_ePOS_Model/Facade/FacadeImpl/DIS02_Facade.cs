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
   public class DIS02_Facade
   {
       public DataTable Query_DiscountMaster(
           string sCategory, string sPartNumberOfDiscount,
           string sDiscountName, string sDiscountAmount,
           string sDiscountRate, string sS_DATE, string sE_DATE)
       {
           StringBuilder sb = new StringBuilder();
           sb.Append(@"SELECT M.DISCOUNT_MASTER_ID
                        , M.DISCOUNT_CODE
                        , M.DISCOUNT_NAME
                        , E.EMPNAME MODI_USER
                        , to_char(M.MODI_DTM,'yyyy/mm/dd hh24:mi:ss') as MODI_DTM
                        , M.DISCOUNT_MONEY
                        , M.DISCOUNT_RATE
                        , to_char(M.S_DATE,'YYYY/MM/DD') as S_DATE
                        , to_char(M.E_DATE,'YYYY/MM/DD') as E_DATE
                        , M.DISCOUNT_TYPE
                        , M.DISCOUNT_MASTER_ID
                        , (CASE M.DIS_USE_TYPE WHEN '1' THEN '無'
                                               WHEN '2' THEN '指定' 
                                               WHEN '3' THEN '總量'
                                               WHEN '4' THEN '均分'
                                               ELSE '無' 
                             END) DIS_USE_TYPE 
                        , (CASE WHEN trunc(M.S_DATE) <= trunc(SYSDATE) AND (trunc(M.E_DATE) >= trunc(SYSDATE) OR M.E_DATE IS NULL) THEN '有效'
                                            WHEN trunc(M.E_DATE) < trunc(SYSDATE) THEN '已過期'
                                            ELSE '尚未生效' 
                            END) STATUS
                    FROM VW_DIS01_DISCOUNT_MASTER M, EMPLOYEE E
                    WHERE M.MODI_USER = E.EMPNO(+) ");
           if (sCategory != "0")
           {
               sb.Append(" AND M.DISCOUNT_TYPE=" + sCategory);
           }
           if (!string.IsNullOrEmpty(sPartNumberOfDiscount))
           {
               sb.Append(" AND M.DISCOUNT_CODE=" + OracleDBUtil.SqlStr(sPartNumberOfDiscount));
           }
           if (!string.IsNullOrEmpty(sDiscountName))
           {
               sb.Append(" AND LOWER(M.DISCOUNT_NAME) like " + OracleDBUtil.ToLowerLikeStr(sDiscountName));
           }
           if (!string.IsNullOrEmpty(sDiscountAmount))
           {
               sb.Append(" AND M.DISCOUNT_MONEY=" + sDiscountAmount);
           }
           if (!string.IsNullOrEmpty(sDiscountRate))
           {
               sb.Append(" AND M.DISCOUNT_RATE=" + sDiscountRate);
           }
           if (!string.IsNullOrEmpty(sS_DATE))
           {
               DateTime d = Convert.ToDateTime(sS_DATE);
               sb.Append(" AND to_char(M.S_DATE,'YYYY/MM/DD')>=" + OracleDBUtil.SqlStr(d.ToString("yyyy/MM/dd")));
           }
           if (!string.IsNullOrEmpty(sE_DATE))
           {
               DateTime d = Convert.ToDateTime(sE_DATE);
               sb.Append(" AND to_char(M.E_DATE,'YYYY/MM/DD')<=" + OracleDBUtil.SqlStr(d.ToString("yyyy/MM/dd")));
           }
           sb.Append(" ORDER BY M.S_DATE, M.DISCOUNT_CODE ");

           DataTable dt = OracleDBUtil.Query_Data(sb.ToString());
           return dt;
       }
    
   }
}
