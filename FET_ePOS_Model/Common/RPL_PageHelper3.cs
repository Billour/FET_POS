using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Advtek.Utility;

namespace FET.POS.Model.Common
{
    public class RPL_PageHelper3 : BaseClass
    {

        /// <summary>
        /// 取得有交易過的促銷代碼 ("All"/"")
        /// <returns>DataTable</returns>
        public static DataTable GetPROMO_NO()
        {
            DataTable dt = null;

            try
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append(" SELECT MM.PROMO_NO, MM.PROMO_NAME ");
                strSql.Append(" FROM MM INNER JOIN  SALE_DETAIL SALE ON SALE.PROMPTION_CODE = MM.PROMO_NO ");

                dt = OracleDBUtil.GetDataSet(OracleDBUtil.GetConnection(), strSql.ToString()).Tables[0];

                DataRow dr = dt.NewRow();
                dr["PROMO_NAME"] = "ALL";
                dr["PROMO_NO"] = "";
                dt.Rows.InsertAt(dr, 0);
            }
            catch (Exception ex)
            {
                Logger.Log.Error(ex.Message, ex);
                throw ex;
            }

            return dt;

        }

        /// <summary>
        /// 取得有交易過的商品代號 ("All"/"")
        /// <returns>DataTable</returns>
        public static DataTable GetPROD_NO()
        {
            DataTable dt = null;

            try
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append(" SELECT PROD.PRODNO AS PROD_NO ,PROD.PRODNAME AS PROD_NAME ");
                strSql.Append("   FROM PRODUCT PROD ,SALE_DETAIL SD                       ");
                strSql.Append("  WHERE 1 = 1                                              ");
                strSql.Append("    AND PROD.PRODNO = SD.PRODNO                            ");

                dt = OracleDBUtil.GetDataSet(OracleDBUtil.GetConnection(), strSql.ToString()).Tables[0];

                DataRow dr = dt.NewRow();
                dr["PROD_NAME"] = "ALL";
                dr["PROD_NO"] = "";
                dt.Rows.InsertAt(dr, 0);
            }
            catch (Exception ex)
            {
                Logger.Log.Error(ex.Message, ex);
                throw ex;
            }

            return dt;

        }

        /// <summary>
        /// 取得信用卡別 ("All"/"")
        /// <returns>DataTable</returns>
        public static DataTable GetCARD_TYPE()
        {
            DataTable dt = null;

            try
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append(" SELECT CCT.CREDIT_CARD_TYPE_NO   AS CCT_NO	 ");
                strSql.Append("       ,CCT.CREDIT_CARD_TYPE_NAME AS CCT_NAME ");
                strSql.Append(" FROM CREDIT_CARD_TYPE CCT                    ");
                strSql.Append("  WHERE 1 = 1                                 ");


                dt = OracleDBUtil.GetDataSet(OracleDBUtil.GetConnection(), strSql.ToString()).Tables[0];

                DataRow dr = dt.NewRow();
                dr["CCT_NAME"] = "ALL";
                dr["CCT_NO"] = "ALL";
                dt.Rows.InsertAt(dr, 0);
            }
            catch (Exception ex)
            {
                Logger.Log.Error(ex.Message, ex);
                throw ex;
            }

            return dt;

        }

        /// <summary>
        /// 取得機台號碼 ("All"/"")
        /// <returns>DataTable</returns>
        public static DataTable GetMACHINE_ID(string STORE_NO)
        {
            DataTable dt = null;

            try
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append(" SELECT MACHINE_ID ,MACHINE_NAME ");
                strSql.Append("   FROM STORE_MACHINE            ");
                strSql.Append("  WHERE 1 = 1                    ");
                strSql.Append("    AND STORE_NO =               ");
                strSql.Append(Advtek.Utility.OracleDBUtil.SqlStr(STORE_NO.Trim()));

                dt = OracleDBUtil.GetDataSet(OracleDBUtil.GetConnection(), strSql.ToString()).Tables[0];

                DataRow dr = dt.NewRow();
                dr["MACHINE_NAME"] = "ALL";
                dr["MACHINE_ID"] = "ALL";
                dt.Rows.InsertAt(dr, 0);
            }
            catch (Exception ex)
            {
                Logger.Log.Error(ex.Message, ex);
                throw ex;
            }

            return dt;

        }





    }
}
