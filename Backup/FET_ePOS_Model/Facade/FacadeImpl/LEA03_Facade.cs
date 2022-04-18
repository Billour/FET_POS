using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.OracleClient;
using Advtek.Utility;
using FET.POS.Model.DTO;
using FET.POS.Model.Common;

namespace FET.POS.Model.Facade.FacadeImpl
{
    public class LEA03_Facade
    {
        /// <summary>
        /// 取得查詢已租賃設備查詢
        /// </summary>
        /// <param name="CustName">客戶姓名</param>
        ///  <param name="Msisdn">客戶門號</param>
        ///  <param name="StoreNo">門市編號</param>
        ///  <param name="ckBookinToday">是否只列出今日預定客戶</param>
        /// <returns>查詢結果</returns>
        public DataTable Query_Rent_M(string StoreNo, string CustName, string Msisdn ,bool blBookinToday)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(@"SELECT ROWNUM AS ITEM,
BOOKING_DATE,
RENT_SHEET_NO,
CUST_NAME,
RENT_STATUS,
IMEI,
DEVICE_TYPE,
MSISDN,
SEX,
STORENAME,
PRE_S_DATE,
PRE_E_DATE,
STATUS,
MODI_DTM,
EMPNAME
FROM
(SELECT TO_CHAR(R.BOOKING_DATE   ,'YYYY/MM/DD') AS  BOOKING_DATE
        ,R.CUST_NAME,R.RENT_STATUS,L.IMEI,L.DEVICE_TYPE,R.RENT_SHEET_NO
        ,R.MSISDN,
         CASE
               WHEN R.SEX = 'F' THEN '女'
                WHEN R.SEX = 'M' THEN '男'
         END  AS SEX
        ,S.STORENAME
        ,TO_CHAR(R.PRE_S_DATE   ,'YYYY/MM/DD') AS  PRE_S_DATE
        ,TO_CHAR(R.PRE_E_DATE   ,'YYYY/MM/DD') AS  PRE_E_DATE
        , CASE
               WHEN R.RENT_STATUS =10 THEN '已預約'
               WHEN R.RENT_STATUS =20 THEN '出租中'
               WHEN R.RENT_STATUS =30 THEN '設備歸還結案'
               WHEN R.RENT_STATUS =40 THEN '預約取消'
               WHEN R.RENT_STATUS =50 THEN '須賠償'
               WHEN R.RENT_STATUS =00 THEN '未存檔'
            END AS STATUS
        ,TO_CHAR(R.MODI_DTM   ,'YYYY/MM/DD') AS  MODI_DTM
        ,E.EMPNAME
        FROM LEASE_STOCK L , RENT_M R ,STORE S ,EMPLOYEE E
        WHERE L.LEASE_ID = R.LEASE_ID
        AND L.STORE_NO = S.STORE_NO(+)
        AND R.MODI_USER = E.EMPNO(+)
        AND L.RENT_STATUS <> 99
        AND TRUNC(L.RENT_STATUS) BETWEEN 20 AND 40
                        ");


            if (!string.IsNullOrEmpty(CustName))
            {
                sb.Append(" AND R.CUST_NAME LIKE " + OracleDBUtil.LikeStr  (CustName));
            }
            if (!string.IsNullOrEmpty(Msisdn))
            {
                sb.Append(" AND  R.MSISDN  LIKE " + OracleDBUtil.LikeStr(Msisdn));
            }

            if (!string.IsNullOrEmpty(StoreNo))
            {
                sb.Append(" AND  L.STORE_NO = " + OracleDBUtil.SqlStr(StoreNo));
            }

            if (blBookinToday)
            {
                sb.Append("  AND TO_CHAR(R.BOOKING_DATE,'YYYY/MM/DD') = TO_CHAR(SYSDATE,'YYYY/MM/DD') ");
            }

            sb.Append(" ORDER BY L.STORE_NO,R.BOOKING_DATE  )");

            DataTable dt = OracleDBUtil.Query_Data(sb.ToString());
            return dt;
        }


      

       
    }
}
