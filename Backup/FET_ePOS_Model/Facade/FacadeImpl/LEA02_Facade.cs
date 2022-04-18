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
    public class LEA02_Facade
    {
        /// <summary>
        /// 取得查詢設備租賃手機庫存(Master Data)
        /// </summary>
        /// <param name="DeviceType">手機類型</param>
        ///  <param name="Zone">區域</param>
        ///  <param name="StoreNo">門市編號</param>
        /// <returns>查詢結果</returns>
        public DataTable Query_LeaseStock_M(string DeviceType,string Zone,string StoreNo)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(@"SELECT L.STORE_NO,S.STORENAME,L.DEVICE_TYPE,COUNT(L.DEVICE_TYPE)AS INVENTORY
                        FROM LEASE_M M,LEASE_STOCK L,STORE S ,ZONE Z
                        WHERE M.LEASE_ID = L.LEASE_ID
                        AND L.STORE_NO = S.STORE_NO(+)
                        AND S.ZONE = Z.ZONE(+)   
                        AND L.RENT_STATUS <> 99     ");


            if (!string.IsNullOrEmpty(DeviceType))
            {
                sb.Append(" AND  L.DEVICE_TYPE = " + OracleDBUtil.SqlStr(DeviceType));
            }
            if (!string.IsNullOrEmpty(Zone))
            {
                sb.Append(" AND  Z.ZONE = " + OracleDBUtil.SqlStr(Zone));
            }

            if (!string.IsNullOrEmpty(StoreNo))
            {
                sb.Append(" AND  L.STORE_NO= " + OracleDBUtil.SqlStr(StoreNo));
            }



            sb.Append(" GROUP BY L.STORE_NO,S.STORENAME,L.DEVICE_TYPE ");

            DataTable dt = OracleDBUtil.Query_Data(sb.ToString());
            return dt;
        }


        /// <summary>
        /// 取得查詢設備租賃手機庫存子項目(Master Data)
        /// </summary>
        /// <param name="DeviceType">手機類型</param>
        ///  <param name="StoreNo">門市編號</param>
        /// <returns>查詢結果</returns>
        public DataTable Query_LeaseStock_D(string DeviceType, string StoreNo)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(@"SELECT L.STORE_NO,S.STORENAME,L.DEVICE_TYPE,L.IMEI,L.SERIAL_NO,L.RENT_STATUS,
                        CASE
                           WHEN L.RENT_STATUS =10 THEN '有效'
                           WHEN L.RENT_STATUS =20 THEN '預約BOOKING'
                           WHEN L.RENT_STATUS =30 THEN '租賃BOOKING'
                           WHEN L.RENT_STATUS =40 THEN '租賃出借中'
                           WHEN L.RENT_STATUS =50 THEN '設備維修中'
                           WHEN L.RENT_STATUS =60 THEN '配件遺失，暫停租賃'
                           WHEN L.RENT_STATUS =70 THEN '領用到期'
                        END AS STATUS
                        FROM LEASE_M M,LEASE_STOCK L,STORE S ,ZONE Z
                        WHERE  M.LEASE_ID = L.LEASE_ID
                        AND L.STORE_NO = S.STORE_NO(+)
                        AND S.ZONE = Z.ZONE(+)
                        AND L.RENT_STATUS <> 99        ");


            if (!string.IsNullOrEmpty(DeviceType))
            {
                sb.Append(" AND  L.DEVICE_TYPE = " + OracleDBUtil.SqlStr(DeviceType));
            }
          
            if (!string.IsNullOrEmpty(StoreNo))
            {
                sb.Append(" AND  L.STORE_NO= " + OracleDBUtil.SqlStr(StoreNo));
            }




            DataTable dt = OracleDBUtil.Query_Data(sb.ToString());
            return dt;
        }
      

       
    }
}
