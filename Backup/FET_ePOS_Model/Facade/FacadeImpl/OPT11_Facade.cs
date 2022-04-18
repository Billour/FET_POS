using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data;
using System.Data.OracleClient;
using System.Globalization;

using FET.POS.Model.Helper;
using FET.POS.Model.DTO;
using Advtek.Utility;

namespace FET.POS.Model.Facade.FacadeImpl
{
    public class OPT11_Facade
    {
        public static DataTable Query_HGCONVERTIBLE(string HG_EXCHANGE_TYPE, string SDATES, string SDATED,
            string CONVERT_NAME, string DIVIDABLE_POINT_S, string DIVIDABLE_POINT_E, string CONVERT_CURRENCY_S, string CONVERT_CURRENCY_E)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT ROWNUM AS ITEMNO, tb.* ");
            sb.Append("FROM  ( ");
            sb.Append(" SELECT CONVERT_ID,");
            sb.Append(" DECODE(HCO.HG_EXCHANGE_TYPE,1,'銷售',2,'代收')AS HG_EXCHANGE_TYPE_NAME, ");
            sb.Append(" HCO.HG_EXCHANGE_TYPE, ");
            sb.Append(" DMT.DISCOUNT_CODE AS CONVERT_NO,HCO.CONVERT_NAME AS CONVERT_NAME,");
            sb.Append(" HCO.S_DATE AS S_DATE , ");
            sb.Append(" HCO.E_DATE E_DATE ,HCO.DIVIDABLE_POINT AS DIVIDABLE_POINT, ");
            sb.Append(" HCO.CONVERT_CURRENCY AS CONVERT_CURRENCY,HCO.MODI_USER AS UUSER,EMP.EMPNAME AS MODI_USER_NAME, ");
            sb.Append(" TO_CHAR(HCO.MODI_DTM, 'yyyy/mm/dd hh24:mi:ss') AS UDTM, ");
            sb.Append(" DMT.DISCOUNT_CODE AS DISCOUNT_CODE,DISCOUNT_MASTER_ID ");
            sb.Append(" FROM HG_CONVERTIBLE HCO,DISCOUNT_MASTER DMT,HG_EXCHANGE_TYPE HET,EMPLOYEE EMP ");
            sb.Append(" WHERE HCO.CONVERT_NO = DMT.DISCOUNT_MASTER_ID ");
            sb.Append(" AND HCO.HG_EXCHANGE_TYPE = HET.HG_EXCHANGE_TYPE ");
            sb.Append(" AND HCO.MODI_USER = EMP.EMPNO ");

            if (!string.IsNullOrEmpty(CONVERT_NAME))
            {
                sb.Append(" AND HCO.CONVERT_NAME LIKE  " + OracleDBUtil.LikeStr(CONVERT_NAME));
            }
            if (!string.IsNullOrEmpty(HG_EXCHANGE_TYPE))
            {
                sb.Append(" AND TRIM(HCO.HG_EXCHANGE_TYPE) = " + OracleDBUtil.SqlStr(HG_EXCHANGE_TYPE));
            }
            if (!string.IsNullOrEmpty(SDATES))
            {
                sb.Append(" AND HCO.S_DATE >= " + OracleDBUtil.DateStr(SDATES));

            }
            if (!string.IsNullOrEmpty(SDATED))
            {
                sb.Append(" AND HCO.S_DATE <= " + OracleDBUtil.DateStr(SDATED));

            }
            if (!string.IsNullOrEmpty(DIVIDABLE_POINT_S))
            {
                sb.Append(" AND HCO.DIVIDABLE_POINT >=  " + OracleDBUtil.SqlStr(DIVIDABLE_POINT_S));
            }
            if (!string.IsNullOrEmpty(DIVIDABLE_POINT_E))
            {
                sb.Append(" AND HCO.DIVIDABLE_POINT <=  " + OracleDBUtil.SqlStr(DIVIDABLE_POINT_E));
            }

            if (!string.IsNullOrEmpty(CONVERT_CURRENCY_S))
            {
                sb.Append(" AND HCO.CONVERT_CURRENCY >=  " + OracleDBUtil.SqlStr(CONVERT_CURRENCY_S));
            }
            if (!string.IsNullOrEmpty(CONVERT_CURRENCY_E))
            {
                sb.Append(" AND HCO.CONVERT_CURRENCY <=  " + OracleDBUtil.SqlStr(CONVERT_CURRENCY_E));
            }

            sb.Append(" ORDER BY HCO.HG_EXCHANGE_TYPE,CONVERT_NO ");
            sb.Append("  ) tb ");
            sb.Append("ORDER BY ITEMNO ");

            DataTable dt = OracleDBUtil.Query_Data(sb.ToString());
            return dt;
        }

        public void Update_HGCONVERTIBLE(OPT11_HGCONVERTIBLE_DTO ds)
        {
            //edit can't edit the pay_mode table value
            //so not update PAY_MODE
            OracleDBUtil.UPDDATEByUUID(ds.Tables["HG_CONVERTIBLE"], "CONVERT_ID");

        }

        public void Add_HGCONVERTIBLE(OPT11_HGCONVERTIBLE_DTO ds)
        {
           OracleDBUtil.Insert(ds.Tables["HG_CONVERTIBLE"]);
        }

        /// <summary>
        /// 取得折扣設定主檔ID
        /// </summary>
        /// <param name="DISCOUNT_CODE">折扣料號</param>
        /// <returns>DISCOUNT_MASTER_ID 折扣設定主檔ID</returns>
        public static string Query_DiscountMasterID(string DISCOUNT_CODE)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT DISCOUNT_MASTER_ID ");
            sb.Append("FROM DISCOUNT_MASTER ");
            sb.Append("WHERE DISCOUNT_CODE = " + OracleDBUtil.SqlStr(DISCOUNT_CODE));
            sb.Append(" AND  DISCOUNT_TYPE = 5 ");

            DataTable dt = OracleDBUtil.Query_Data(sb.ToString());
            string strId = "";
            if (dt.Rows.Count > 0)
            {
                strId = dt.Rows[0]["DISCOUNT_MASTER_ID"].ToString();
            }
            return strId;
        }
    }
}

