using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Advtek.Utility;
using System.Data.OracleClient;

namespace FET.POS.Model.Facade.FacadeImpl
{
    public partial class RPL_Facade
    {
        #region 宗佑
        /*Author：宗佑
          Date：100.2.18
          Description：修正RPL035 SQL敘述 PROMPTION_CODE修正為PROMOTION_CODE
        */
        /// <summary>
        /// RPL035
        /// </summary>
        /// <param name="PromotionCode">促銷代號</param>
        /// <param name="ProductNo">商品代號</param>
        /// <param name="DATE_S">交易日期(起)</param>
        /// <param name="DATE_E">交易日期(訖)</param>
        public DataTable RPL035(string PromotionCode, string ProductNo, string DATE_S, string DATE_E)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(" SELECT ROWNUM              AS 項次                  ");
            sb.Append("       ,TO_CHAR(TRADE_DATE,'YYYY/MM/DD') AS 交易日期 ");
            sb.Append("       ,PRODNO              AS 商品料號              ");
            sb.Append("       ,PRODNAME            AS 商品名稱              ");
            sb.Append("       ,STORE_NO            AS 門市代碼              ");
            sb.Append("       ,STORE_NAME          AS 門市名稱              ");
            sb.Append("       ,SALE_TYPE           AS 銷售銷退             ");
            sb.Append("       ,NVL(SALE_QTY,0)     AS 銷售數量              ");
            sb.Append("       ,MSISDN              AS 門號                  ");
            sb.Append("       ,PROMOTION_CODE      AS 促銷代碼              ");
            sb.Append("       ,NVL(TOTAL_AMOUNT,0) AS 銷售金額              ");
            sb.Append("       ,SALE_PERSON         AS 員工編號              ");
            sb.Append("   FROM VW_RPL_PFAnalysis_REPORT                     ");
            sb.Append("  WHERE 1 = 1                                        ");

            if (!string.IsNullOrEmpty(PromotionCode))
            {
                sb.Append(" AND PROMOTION_CODE = " + OracleDBUtil.SqlStr(PromotionCode.Trim()));
            }
            if (!string.IsNullOrEmpty(ProductNo))
            {
                sb.Append(" AND PRODNO = " + OracleDBUtil.SqlStr(ProductNo.Trim()));
            }
            if (!string.IsNullOrEmpty(DATE_S))
            {
                sb.Append(" AND TRUNC(TRADE_DATE) >= " + OracleDBUtil.DateStr(DATE_S.Trim()));
            }
            if (!string.IsNullOrEmpty(DATE_E))
            {
                sb.Append(" AND TRUNC(TRADE_DATE) <= " + OracleDBUtil.DateStr(DATE_E.Trim()));
            }

            DataTable dt = OracleDBUtil.Query_Data(sb.ToString());
            return dt;
        }
        #endregion
        #region 宗佑
        /* Author：宗佑
           Date：100.2.16
           decription：修正RPL051 SQL Where中PAID_MODE改為PAID_MODE_NAME
        */
        /// <summary>
        /// RPL051
        /// </summary>
        /// <param name="STORE_S">門市編號(起)</param>
        /// <param name="STORE_E">門市編號(訖)</param>
        /// <param name="DATE_S">交易日期(起)</param>
        /// <param name="DATE_E">交易日期(訖)</param>
        /// <param name="PAID_MODE">儲值類別</param>
        public DataTable RPL051(string STORE_S, string STORE_E, string DATE_S, string DATE_E, string PAID_MODE)
        {
            StringBuilder sb = new StringBuilder();

            #region -SELECT-
            sb.Append(" SELECT * FROM ( ");
            sb.Append(" SELECT TO_CHAR(TRADE_DATE,'YYYY/MM/DD') AS 交易日期 ");
            sb.Append("       ,MACHINE_ID          AS 機台編號              ");
            sb.Append("       ,SALE_NO             AS 交易序號              ");
            sb.Append("       ,PAID_MODE_NAME      AS 儲值類別              ");
            sb.Append("       ,CREDIT_CARD_NO      AS 卡號                  ");
            sb.Append("       ,NVL(TOTAL_AMOUNT,0) AS 儲值金額              ");
            sb.Append("       ,REMARK              AS 備註                  ");
            sb.Append("   FROM VW_RPL_ETC_REPORT                            ");
            sb.Append("  WHERE 1 = 1                                        ");
            #endregion

            #region -WHERE-
            #region -門市編號-
            if (!string.IsNullOrEmpty(STORE_S))
            {
                sb.Append(" AND STORE_NO >= " + OracleDBUtil.SqlStr(STORE_S.Trim()));
            }
            if (!string.IsNullOrEmpty(STORE_E))
            {
                sb.Append(" AND STORE_NO <= " + OracleDBUtil.SqlStr(STORE_E.Trim()));
            }
            #endregion
            #region -交易日期-
            if (!string.IsNullOrEmpty(DATE_S))
            {
                sb.Append(" AND TRUNC(TRADE_DATE) >= " + OracleDBUtil.DateStr(DATE_S.Trim()));
            }
            if (!string.IsNullOrEmpty(DATE_E))
            {
                sb.Append(" AND TRUNC(TRADE_DATE) <= " + OracleDBUtil.DateStr(DATE_E.Trim()));
            }
            #endregion
            #region -儲值類別-
            if (!string.IsNullOrEmpty(PAID_MODE) && PAID_MODE.Trim() != "ALL")
            {
                sb.Append(" AND PAID_MODE_NAME = " + OracleDBUtil.SqlStr(PAID_MODE.Trim()));
            }
            #endregion
            #endregion

            #region -ORDER BY-
            sb.AppendFormat(" ORDER BY TO_CHAR(TRADE_DATE,'YYYY/MM/DD') ,SALE_NO ");
            #endregion

            sb.AppendFormat(" ) ");
            sb.AppendFormat(" UNION ALL ");

            #region -SELECT-
            sb.Append(" SELECT '總計' AS 交易日期   ");
            sb.Append("       ,''     AS 機台編號   ");
            sb.Append("       ,''     AS 交易序號   ");
            sb.Append("       ,''     AS 儲值類別   ");
            sb.Append("       ,''     AS 卡號       ");
            sb.Append("       ,NVL(SUM(TOTAL_AMOUNT),0) AS 儲值金額 ");
            sb.Append("       ,''     AS 備註       ");
            sb.Append("   FROM VW_RPL_ETC_REPORT    ");
            sb.Append("  WHERE 1 = 1                ");
            #endregion

            #region -WHERE-
            #region -門市編號-
            if (!string.IsNullOrEmpty(STORE_S))
            {
                sb.Append(" AND STORE_NO >= " + OracleDBUtil.SqlStr(STORE_S.Trim()));
            }
            if (!string.IsNullOrEmpty(STORE_E))
            {
                sb.Append(" AND STORE_NO <= " + OracleDBUtil.SqlStr(STORE_E.Trim()));
            }
            #endregion
            #region -交易日期-
            if (!string.IsNullOrEmpty(DATE_S))
            {
                sb.Append(" AND TRUNC(TRADE_DATE) >= " + OracleDBUtil.DateStr(DATE_S.Trim()));
            }
            if (!string.IsNullOrEmpty(DATE_E))
            {
                sb.Append(" AND TRUNC(TRADE_DATE) <= " + OracleDBUtil.DateStr(DATE_E.Trim()));
            }
            #endregion
            #region -儲值類別-
            if (!string.IsNullOrEmpty(PAID_MODE) && PAID_MODE.Trim() != "ALL")
            {
                sb.Append(" AND PAID_MODE_NAME = " + OracleDBUtil.SqlStr(PAID_MODE.Trim()));
            }
            #endregion
            #endregion


            DataTable dt = OracleDBUtil.Query_Data(sb.ToString());
            return dt;
        }
        #endregion

        /// <summary>
        /// RPL060
        /// </summary>
        /// <param name="DATE_S">交易日期(起)</param>
        /// <param name="DATE_E">交易日期(訖)</param>
        /// <param name="MSISDN">門號</param>
        public DataTable RPL060(string DATE_S, string DATE_E, string MSISDN, string STORE_NO)
        {
            StringBuilder sb = new StringBuilder();

            #region -SELECT-
            sb.AppendFormat(" SELECT DISTINCT                                     ");
            sb.AppendFormat("        TO_CHAR(TRADE_DATE,'YYYY/MM/DD') AS 交易日期 ");
            sb.AppendFormat("       ,MSISDN              AS 門號                  ");
            sb.AppendFormat("       ,SALE_NO             AS 交易序號              ");
            sb.AppendFormat("       ,PAY_MODE_NAME       AS 支付方式              ");
            sb.AppendFormat("       ,INVOICE_NO          AS 發票號碼              ");
            sb.AppendFormat("       ,NVL(INVOICE_TOTAL_AMOUNT,0) AS 發票金額      ");
            sb.AppendFormat("       ,REMARK              AS 備註                  ");
            sb.AppendFormat("   FROM VW_RPL_LiDamages_REPORT                      ");
            sb.AppendFormat("  WHERE 1 = 1                                        ");
            #endregion

            #region -WHERE-
            #region -門號-
            if (!string.IsNullOrEmpty(MSISDN))
            {
                sb.AppendFormat(" AND MSISDN = " + OracleDBUtil.SqlStr(MSISDN.Trim()));
            }
            #endregion
            #region -交易日期-
            if (!string.IsNullOrEmpty(DATE_S))
            {
                sb.AppendFormat(" AND TRUNC(TRADE_DATE) >= " + OracleDBUtil.DateStr(DATE_S.Trim()));
            }
            if (!string.IsNullOrEmpty(DATE_E))
            {
                sb.AppendFormat(" AND TRUNC(TRADE_DATE) <= " + OracleDBUtil.DateStr(DATE_E.Trim()));
            }
            #endregion
            #region -門市編號-
            if (!string.IsNullOrEmpty(STORE_NO) && STORE_NO.Length == 4) sb.AppendFormat(" AND STORE_NO ='{0}' ", STORE_NO);
            #endregion
            #endregion

            DataTable dt = OracleDBUtil.Query_Data(sb.ToString());
            return dt;
        }

        /// <summary>
        /// RPL060 -小計，總計
        /// </summary>
        /// <param name="DATE_S">交易日期(起)</param>
        /// <param name="DATE_E">交易日期(訖)</param>
        /// <param name="MSISDN">門號</param>
        public DataTable RPL060_SUM(string DATE_S, string DATE_E, string MSISDN, string STORE_NO)
        {
            StringBuilder sb = new StringBuilder();

            #region -SELECT-
            sb.AppendFormat("WITH RPL060 AS ( \n");
            sb.AppendFormat(" SELECT DISTINCT                                     ");
            sb.AppendFormat("        '1' ORDER_INDEX                              ");
            sb.AppendFormat("       ,TO_CHAR(TRADE_DATE,'YYYY/MM/DD') AS 交易日期 ");
            sb.AppendFormat("       ,MSISDNMASK(MSISDN)  AS 門號                  ");
            sb.AppendFormat("       ,SALE_NO             AS 交易序號              ");
            sb.AppendFormat("       ,PAY_MODE_NAME       AS 支付方式              ");
            sb.AppendFormat("       ,INVOICE_NO          AS 發票號碼              ");
            sb.AppendFormat("       ,NVL(INVOICE_TOTAL_AMOUNT,0) AS 發票金額      ");
            sb.AppendFormat("       ,REMARK              AS 備註                  ");
            sb.AppendFormat("   FROM VW_RPL_LiDamages_REPORT                      ");
            sb.AppendFormat("  WHERE 1 = 1                                        ");
            #endregion

            #region -WHERE-
            #region -門號-
            if (!string.IsNullOrEmpty(MSISDN))
            {
                sb.AppendFormat(" AND MSISDN = " + OracleDBUtil.SqlStr(MSISDN.Trim()));
            }
            #endregion
            #region -交易日期-
            if (!string.IsNullOrEmpty(DATE_S))
            {
                sb.AppendFormat(" AND TRUNC(TRADE_DATE) >= " + OracleDBUtil.DateStr(DATE_S.Trim()));
            }
            if (!string.IsNullOrEmpty(DATE_E))
            {
                sb.AppendFormat(" AND TRUNC(TRADE_DATE) <= " + OracleDBUtil.DateStr(DATE_E.Trim()));
            }
            #endregion
            #region -門市編號-
            if (!string.IsNullOrEmpty(STORE_NO) && STORE_NO.Length == 4) sb.AppendFormat(" AND STORE_NO ='{0}' ", STORE_NO);
            #endregion
            #endregion

            sb.AppendFormat("  ) \n");
            sb.AppendFormat("  SELECT * FROM ( \n");
            sb.AppendFormat("  SELECT * \n");
            sb.AppendFormat("    FROM RPL060 \n");
            sb.AppendFormat("  UNION ALL \n");

            #region 統計部分 - 匯出顯示

            sb.AppendFormat("  SELECT '2' AS ORDER_INDEX  \n");
            sb.AppendFormat("        ,''    \n");
            sb.AppendFormat("        ,''    \n");
            sb.AppendFormat("        ,''    \n");
            sb.AppendFormat("        ,支付方式 || '小計' AS 支付方式 \n");
            sb.AppendFormat("        ,''    \n");
            sb.AppendFormat("        ,NVL(SUM(發票金額), 0) 發票金額    \n");
            sb.AppendFormat("        ,''    \n");
            sb.AppendFormat("    FROM RPL060 \n");
            sb.AppendFormat("   GROUP BY 支付方式   \n");
            sb.AppendFormat("   UNION ALL \n");
            sb.AppendFormat("  SELECT '3' AS ORDER_INDEX  \n");
            sb.AppendFormat("        ,''    \n");
            sb.AppendFormat("        ,''    \n");
            sb.AppendFormat("        ,''    \n");
            sb.AppendFormat("        ,'總計' AS 支付方式 \n");
            sb.AppendFormat("        ,''    \n");
            sb.AppendFormat("        ,NVL(SUM(發票金額), 0) 發票金額    \n");
            sb.AppendFormat("        ,''    \n");
            sb.AppendFormat("    FROM RPL060 \n");
            sb.AppendFormat("   ) \n");

            #region -ORDER BY-
            sb.AppendFormat("   ORDER BY 支付方式 ,ORDER_INDEX ,交易日期 ,交易序號 \n");
            #endregion


            #endregion

            DataTable dt = OracleDBUtil.Query_Data(sb.ToString());
            return dt;
        }

        /// <summary>
        /// RPL069
        /// </summary>
        /// <param name="PromotionCode_S">促銷代號(起)</param>
        /// <param name="PromotionCode_E">促銷代號(訖)</param>
        /// <param name="DATE_S">交易日期(起)</param>
        /// <param name="DATE_E">交易日期(訖)</param>
        public DataTable RPL069(string PromotionCode_S, string PromotionCode_E, string DATE_S, string DATE_E, string STORE_NO)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(" SELECT MM.PROMO_NO    AS 促銷代碼     ");
            sb.Append("       ,MM.PROMO_NAME  AS 促銷名稱     ");
            sb.Append("       ,SD.PRODNO      AS 商品編號     ");
            sb.Append("       ,PROD.PRODNAME  AS 商品名稱     ");
            sb.Append("       ,NVL(SUM(SD.QUANTITY),0)     AS 數量            ");
            sb.Append("       ,NVL(SUM(SD.TOTAL_AMOUNT),0) AS 銷售金額        ");
            sb.Append("   FROM MM ,SALE_HEAD SH ,SALE_DETAIL SD ,PRODUCT PROD ");
            sb.Append("  WHERE SH.POSUUID_MASTER = SD.POSUUID_MASTER          ");
            sb.Append("    AND MM.PROMO_NO = SD.PROMOTION_CODE(+)             ");
            sb.Append("    AND SD.PRODNO = PROD.PRODNO                        ");

            if (!string.IsNullOrEmpty(PromotionCode_S))
            {
                sb.Append(" AND MM.PROMO_NO >= " + OracleDBUtil.SqlStr(PromotionCode_S.Trim()));
            }
            if (!string.IsNullOrEmpty(PromotionCode_E))
            {
                sb.Append(" AND MM.PROMO_NO <= " + OracleDBUtil.SqlStr(PromotionCode_E.Trim()));
            }
            if (!string.IsNullOrEmpty(DATE_S))
            {
                sb.Append(" AND TRUNC(SH.TRADE_DATE) >= " + OracleDBUtil.DateStr(DATE_S.Trim()));
            }
            if (!string.IsNullOrEmpty(DATE_E))
            {
                sb.Append(" AND TRUNC(SH.TRADE_DATE) <= " + OracleDBUtil.DateStr(DATE_E.Trim()));
            }
            if (!string.IsNullOrEmpty(STORE_NO) && STORE_NO.Length == 4)
            {
                sb.Append(" AND SH.STORE_NO = " + OracleDBUtil.SqlStr(STORE_NO.Trim()));
            }

            sb.Append(" GROUP BY MM.PROMO_NO ,MM.PROMO_NAME ,SD.PRODNO ,PROD.PRODNAME ");
            sb.Append(" ORDER BY MM.PROMO_NO ,SD.PRODNO ");
            DataTable dt = OracleDBUtil.Query_Data(sb.ToString());
            return dt;
        }

        /// <summary>
        /// RPL069
        /// </summary>
        /// <param name="PromotionCode_S">促銷代號(起)</param>
        /// <param name="PromotionCode_E">促銷代號(訖)</param>
        /// <param name="DATE_S">交易日期(起)</param>
        /// <param name="DATE_E">交易日期(訖)</param>
        public DataTable RPL069_TOTAL(string PromotionCode_S, string PromotionCode_E, string DATE_S, string DATE_E, string STORE_NO)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("WITH RPL069 AS ( \n");
            sb.AppendFormat("SELECT '1' ORDER_INDEX, \n");
            sb.AppendFormat("       MM.PROMO_NO AS PROMO_NO, \n");
            sb.AppendFormat("       MM.PROMO_NO AS 促銷代碼, \n");
            sb.AppendFormat("       MM.PROMO_NAME AS 促銷名稱, \n");
            sb.AppendFormat("       SD.PRODNO AS 商品編號, \n");
            sb.AppendFormat("       PROD.PRODNAME AS 商品名稱, \n");
            sb.AppendFormat("       TO_CHAR(NVL(SUM(SD.QUANTITY),0)) AS 數量, \n");
            sb.AppendFormat("       NVL(SUM(SD.TOTAL_AMOUNT),0) AS 銷售金額 \n");
            sb.AppendFormat("  FROM MM ,SALE_HEAD SH ,SALE_DETAIL SD ,PRODUCT PROD \n");
            sb.AppendFormat(" WHERE SH.POSUUID_MASTER = SD.POSUUID_MASTER          \n");
            sb.AppendFormat("   AND MM.PROMO_NO = SD.PROMOTION_CODE(+)             \n");
            sb.AppendFormat("   AND SD.PRODNO = PROD.PRODNO                        \n");

            #region WHERE
            if (!string.IsNullOrEmpty(PromotionCode_S))
            {
                sb.Append(" AND MM.PROMO_NO >= " + OracleDBUtil.SqlStr(PromotionCode_S.Trim()));
            }
            if (!string.IsNullOrEmpty(PromotionCode_E))
            {
                sb.Append(" AND MM.PROMO_NO <= " + OracleDBUtil.SqlStr(PromotionCode_E.Trim()));
            }
            if (!string.IsNullOrEmpty(DATE_S))
            {
                sb.Append(" AND TRUNC(SH.TRADE_DATE) >= " + OracleDBUtil.DateStr(DATE_S.Trim()));
            }
            if (!string.IsNullOrEmpty(DATE_E))
            {
                sb.Append(" AND TRUNC(SH.TRADE_DATE) <= " + OracleDBUtil.DateStr(DATE_E.Trim()));
            }
            if (!string.IsNullOrEmpty(STORE_NO) && STORE_NO.Length == 4)
            {
                sb.Append(" AND SH.STORE_NO = " + OracleDBUtil.SqlStr(STORE_NO.Trim()));
            }
            #endregion

            sb.AppendFormat(" GROUP BY MM.PROMO_NO ,MM.PROMO_NAME ,SD.PRODNO ,PROD.PRODNAME \n");
            sb.AppendFormat(" ORDER BY MM.PROMO_NO ,SD.PRODNO \n");
            sb.AppendFormat(" ) \n");
            sb.AppendFormat(" SELECT * \n");
            sb.AppendFormat("   FROM RPL069 \n");

            #region 統計部分
            sb.AppendFormat(" UNION ALL \n");
            sb.AppendFormat(" SELECT '2', \n");
            sb.AppendFormat("        PROMO_NO, \n");
            sb.AppendFormat("        NULL, \n");
            sb.AppendFormat("        NULL, \n");
            sb.AppendFormat("        NULL, \n");
            sb.AppendFormat("        NULL, \n");
            sb.AppendFormat("        '小計', \n");
            sb.AppendFormat("        NVL(SUM(銷售金額), 0) \n");
            sb.AppendFormat("   FROM RPL069 \n");
            sb.AppendFormat("  GROUP BY PROMO_NO \n");
            sb.AppendFormat("  \n");
            sb.AppendFormat(" UNION ALL \n");
            sb.AppendFormat(" SELECT '3', NULL, NULL, NULL, NULL, NULL, '總計', NVL(SUM(銷售金額), 0) \n");
            sb.AppendFormat("   FROM RPL069 \n");
            #endregion

            sb.AppendFormat("  ORDER BY 2, 1 \n");

            DataTable dt = OracleDBUtil.Query_Data(sb.ToString());
            return dt;
        }


        /// <summary>
        /// RPL073
        /// </summary>
        /// <param name="StoreNo_S">門市編號(起)</param>
        /// <param name="StoreNo_E">門市編號(訖)</param>
        /// <param name="DATE_S">交易日期(起)</param>
        /// <param name="DATE_E">交易日期(訖)</param>
        /// <param name="DiscountClass">折扣類別</param>
        /// <param name="ChangeClass">兌點類別</param>
        /// <param name="ServiceType">服務類型</param>
        public DataTable RPL073(string StoreNo_S, string StoreNo_E, string DATE_S, string DATE_E, string DiscountClass, string ChangeClass, string ServiceType)
        {
            StringBuilder sb = new StringBuilder();

            #region -SELECT-
            sb.AppendFormat(" SELECT STORE_NO        AS 門市編號              ");
            sb.AppendFormat("       ,STORE_NAME      AS 門市名稱              ");
            sb.AppendFormat("       ,TO_CHAR(TRADE_DATE,'YYYY/MM/DD') AS 日期 ");
            sb.AppendFormat("       ,MACHINE_ID      AS 機台                  ");
            sb.AppendFormat("       ,SALE_NO         AS 交易序號              ");
            sb.AppendFormat("       ,折扣類別                                 ");
            sb.AppendFormat("       ,兌點類別                                 ");
            sb.AppendFormat("       ,HG_CARD_NO        AS 集團卡卡號          ");
            sb.AppendFormat("       ,SALE_TYPE_NAME    AS 交易類型            ");
            sb.AppendFormat("       ,SALE_STATUS_NAME  AS 交易類別            ");
            sb.AppendFormat("       ,SERVICE_TYPE_NAME AS 服務類型            ");
            sb.AppendFormat("       ,SOURCE_TYPE_NAME  AS 資料來源            ");
            sb.AppendFormat("       ,DISCOUNT_CODE     AS 折扣代號            ");
            sb.AppendFormat("       ,PROMOTION_CODE    AS 促銷代碼            ");
            sb.AppendFormat("       ,PRODNO            AS 商品料號            ");
            sb.AppendFormat("       ,PRODNAME          AS 商品名稱            ");
            sb.AppendFormat("       ,DISCOUNT_NAME     AS 累兌點說明          ");
            sb.AppendFormat("       ,HG_REDEEM_POINT   AS 兌換點數            ");
            sb.AppendFormat("       ,TOTAL_AMOUNT      AS 兌換金額            ");
            sb.AppendFormat("   FROM VW_RPL_GROUP_CARD_HG_REPORT              ");
            sb.AppendFormat("  WHERE 1 = 1                                    ");
            #endregion

            #region -WHERE-
            if (!string.IsNullOrEmpty(StoreNo_S))
            {
                sb.AppendFormat(" AND STORE_NO >= " + OracleDBUtil.SqlStr(StoreNo_S.Trim()));
            }
            if (!string.IsNullOrEmpty(StoreNo_E))
            {
                sb.AppendFormat(" AND STORE_NO <= " + OracleDBUtil.SqlStr(StoreNo_E.Trim()));
            }
            if (!string.IsNullOrEmpty(DATE_S))
            {
                sb.AppendFormat(" AND TRUNC(TRADE_DATE) >= " + OracleDBUtil.DateStr(DATE_S.Trim()));
            }
            if (!string.IsNullOrEmpty(DATE_E))
            {
                sb.AppendFormat(" AND TRUNC(TRADE_DATE) <= " + OracleDBUtil.DateStr(DATE_E.Trim()));
            }

            if (!string.IsNullOrEmpty(DiscountClass) && DiscountClass.Trim() != "ALL")
            {
                sb.AppendFormat(" AND 折扣類別 = " + OracleDBUtil.SqlStr(DiscountClass.Trim()));
            }
            if (!string.IsNullOrEmpty(ChangeClass) && ChangeClass.Trim() != "ALL")
            {
                sb.AppendFormat(" AND 兌點類別 = " + OracleDBUtil.SqlStr(ChangeClass.Trim()));
            }
            if (!string.IsNullOrEmpty(ServiceType) && ServiceType.Trim() != "ALL")
            {
                sb.AppendFormat(" AND SERVICE_TYPE = " + OracleDBUtil.SqlStr(ServiceType.Trim()));
            }
            #endregion

            #region -ORDER BY-
            sb.AppendFormat("  ORDER BY STORE_NO ,TO_CHAR(TRADE_DATE,'YYYY/MM/DD') ,MACHINE_ID ");
            #endregion

            DataTable dt = OracleDBUtil.Query_Data(sb.ToString());
            return dt;
        }

        #region ---  蔡坤霖
        /*Author : 蔡坤霖
          Date : 2011 / 02 / 10
          Description: RPL030 商品銷售統計表
         */
        /// <summary>
        /// RPL030 商品銷售統計表
        /// </summary>
        /// <param name="STORE_S">門市編號(起)</param>
        /// <param name="STORE_E">門市編號(訖)</param>
        /// <param name="DATE_S">交易日期(起)</param>
        /// <param name="DATE_E">交易日期(訖)</param>
        /// <param name="PRODTYPE_S">商品類別(起)</param>
        /// <param name="PRODTYPE_E">商品類別(訖)</param>
        /// <param name="PROD_S">商品料號(起)</param>
        /// <param name="PROD_E">商品料號(訖)</param>
        public DataTable RPL030(string STORE_S, string STORE_E, string DATE_S, string DATE_E, string PRODTYPE_S, string PRODTYPE_E, string PROD_S, string PROD_E)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(" SELECT DISTINCT                    ");
            sb.Append("        STORE_NO   AS 門市編號      ");
            sb.Append("       ,STORE_NAME AS 門市名稱      ");
            sb.Append("       ,PRODTYPENAME AS 商品類別      ");
            sb.Append("       ,PRODNO     AS 商品料號      ");
            sb.Append("       ,PRODNAME   AS 商品名稱      ");
            sb.Append("       ,ACC1       AS 科目1         ");
            sb.Append("       ,ACC2       AS 科目2         ");
            sb.Append("       ,ACC3       AS 科目3         ");
            sb.Append("       ,ACC4       AS 科目4         ");
            sb.Append("       ,ACC5       AS 科目5         ");
            sb.Append("       ,ACC6       AS 科目6         ");
            sb.Append("       ,SALE_TYPE  AS 銷售型態      ");
            sb.Append("       ,NVL(SALE_QTY,0) AS 數量     ");
            sb.Append("       ,NVL(SALE_AMOUNT,0) AS 金額  ");
            sb.Append("   FROM VW_RPL_PROD_SALE_STATISTICS ");
            sb.Append("  WHERE 1 = 1                       ");

            if (!string.IsNullOrEmpty(STORE_S))
            {
                sb.Append(" AND STORE_NO >= " + OracleDBUtil.SqlStr(STORE_S.Trim()));
            }
            if (!string.IsNullOrEmpty(STORE_E))
            {
                sb.Append(" AND STORE_NO <= " + OracleDBUtil.SqlStr(STORE_E.Trim()));
            }
            if (!string.IsNullOrEmpty(DATE_S))
            {
                sb.Append(" AND TRUNC(TRADE_DATE) >= " + OracleDBUtil.DateStr(DATE_S.Trim()));
            }
            if (!string.IsNullOrEmpty(DATE_E))
            {
                sb.Append(" AND TRUNC(TRADE_DATE) <= " + OracleDBUtil.DateStr(DATE_E.Trim()));
            }
            if (!string.IsNullOrEmpty(PRODTYPE_S) && PRODTYPE_S.Trim() != "All")
            {
                sb.Append(" AND PRODTYPENO >= " + OracleDBUtil.SqlStr(PRODTYPE_S.Trim()));
            }
            if (!string.IsNullOrEmpty(PRODTYPE_E) && PRODTYPE_E.Trim() != "All")
            {
                sb.Append(" AND PRODTYPENO <= " + OracleDBUtil.SqlStr(PRODTYPE_E.Trim()));
            }
            if (!string.IsNullOrEmpty(PROD_S))
            {
                sb.Append(" AND PRODNO >= " + OracleDBUtil.SqlStr(PROD_S.Trim()));
            }
            if (!string.IsNullOrEmpty(PROD_E))
            {
                sb.Append(" AND PRODNO <= " + OracleDBUtil.SqlStr(PROD_E.Trim()));
            }

            DataTable dt = OracleDBUtil.Query_Data(sb.ToString());
            return dt;
        }

        /*Author : 蔡坤霖
                  Date : 2011 / 02 / 10
                  Description: RPL030 商品銷售統計表
                 */
        /// <summary>
        /// RPL030 商品銷售統計表_含小記合計
        /// </summary>
        /// <param name="STORE_S">門市編號(起)</param>
        /// <param name="STORE_E">門市編號(訖)</param>
        /// <param name="DATE_S">交易日期(起)</param>
        /// <param name="DATE_E">交易日期(訖)</param>
        /// <param name="PRODTYPE_S">商品類別(起)</param>
        /// <param name="PRODTYPE_E">商品類別(訖)</param>
        /// <param name="PROD_S">商品料號(起)</param>
        /// <param name="PROD_E">商品料號(訖)</param>
        public DataTable RPL030_TOTAL(string STORE_S, string STORE_E, string DATE_S, string DATE_E, string PRODTYPE_S, string PRODTYPE_E, string PROD_S, string PROD_E)
        {
            #region BaseSQL
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("          			SELECT DISTINCT '1' ORDER_INDEX, \n");
            sb.AppendFormat("                                STORE_NO, \n");
            sb.AppendFormat("                                PRODNO, \n");
            sb.AppendFormat("                                STORE_NO AS 門市編號, \n");
            sb.AppendFormat("                                DECODE(NVL(STORE_NAME,'@'),'@','　　',STORE_NAME) AS 門市名稱, \n");
            sb.AppendFormat("                                PRODTYPENAME AS 商品類別, \n");
            sb.AppendFormat("                                PRODNO AS 商品料號, \n");
            sb.AppendFormat("                                PRODNAME AS 商品名稱, \n");
            sb.AppendFormat("                                ACC1 AS 科目1, \n");
            sb.AppendFormat("                                ACC2 AS 科目2, \n");
            sb.AppendFormat("                                ACC3 AS 科目3, \n");
            sb.AppendFormat("                                ACC4 AS 科目4, \n");
            sb.AppendFormat("                                ACC5 AS 科目5, \n");
            sb.AppendFormat("                                ACC6 AS 科目6, \n");
            sb.AppendFormat("                                DECODE(NVL(SALE_TYPE,'@'),'@','　　',SALE_TYPE) AS 銷售型態, \n");
            sb.AppendFormat("                                NVL(SALE_QTY, 0) AS 數量, \n");
            sb.AppendFormat("                                NVL(SALE_AMOUNT, 0) AS 金額 \n");
            sb.AppendFormat("                  FROM VW_RPL_PROD_SALE_STATISTICS \n");
            sb.AppendFormat("                 WHERE 1 = 1 \n");

            if (!string.IsNullOrEmpty(STORE_S))
            {
                sb.AppendLine(" AND STORE_NO >= " + OracleDBUtil.SqlStr(STORE_S.Trim()));
            }
            if (!string.IsNullOrEmpty(STORE_E))
            {
                sb.AppendLine(" AND STORE_NO <= " + OracleDBUtil.SqlStr(STORE_E.Trim()));
            }
            if (!string.IsNullOrEmpty(DATE_S))
            {
                sb.AppendLine(" AND TRUNC(TRADE_DATE) >= " + OracleDBUtil.DateStr(DATE_S.Trim()));
            }
            if (!string.IsNullOrEmpty(DATE_E))
            {
                sb.AppendLine(" AND TRUNC(TRADE_DATE) <= " + OracleDBUtil.DateStr(DATE_E.Trim()));
            }
            if (!string.IsNullOrEmpty(PRODTYPE_S) && PRODTYPE_S.Trim() != "All")
            {
                sb.AppendLine(" AND PRODTYPENO >= " + OracleDBUtil.SqlStr(PRODTYPE_S.Trim()));
            }
            if (!string.IsNullOrEmpty(PRODTYPE_E) && PRODTYPE_E.Trim() != "All")
            {
                sb.AppendLine(" AND PRODTYPENO <= " + OracleDBUtil.SqlStr(PRODTYPE_E.Trim()));
            }
            if (!string.IsNullOrEmpty(PROD_S))
            {
                sb.AppendLine(" AND PRODNO >= " + OracleDBUtil.SqlStr(PROD_S.Trim()));
            }
            if (!string.IsNullOrEmpty(PROD_E))
            {
                sb.AppendLine(" AND PRODNO <= " + OracleDBUtil.SqlStr(PROD_E.Trim()));
            }
            #endregion BaseSQL

            StringBuilder sbSQL = new StringBuilder();

            #region 加入 商品小計 門市小計 合計
            #region 基本資料
            sbSQL.AppendFormat("SELECT * FROM ( \n");
            sbSQL.AppendFormat(sb.ToString());
            #endregion
            sbSQL.AppendFormat("        UNION ALL \n");
            #region 商品類別小計
            sbSQL.AppendFormat("        SELECT '2' ORDER_INDEX, \n");
            sbSQL.AppendFormat("               STORE_NO, \n");
            sbSQL.AppendFormat("               PRODNO, \n");
            sbSQL.AppendFormat("               '' 門市編號, \n");
            sbSQL.AppendFormat("               '' 門市名稱, \n");
            sbSQL.AppendFormat("               '' 商品類別, \n");
            sbSQL.AppendFormat("               '' 商品料號, \n");
            sbSQL.AppendFormat("               '' 商品名稱, \n");
            sbSQL.AppendFormat("               '' 科目1, \n");
            sbSQL.AppendFormat("               '' 科目2, \n");
            sbSQL.AppendFormat("               '' 科目3, \n");
            sbSQL.AppendFormat("               '' 科目4, \n");
            sbSQL.AppendFormat("               '' 科目5, \n");
            sbSQL.AppendFormat("               '商品類別小計' 科目6, \n");
            sbSQL.AppendFormat("               銷售型態, \n");
            sbSQL.AppendFormat("               SUM(數量) 數量, \n");
            sbSQL.AppendFormat("               SUM(金額) 金額 \n");
            sbSQL.AppendFormat("          FROM ( \n");
            sbSQL.AppendFormat(sb.ToString());
            sbSQL.AppendFormat("               ) \n");
            sbSQL.AppendFormat("         GROUP BY STORE_NO, 銷售型態, PRODNO \n");
            #endregion 商品類別小計
            sbSQL.AppendFormat("        UNION ALL \n");
            #region 門市小計
            sbSQL.AppendFormat("        SELECT '3' ORDER_INDEX, \n");
            sbSQL.AppendFormat("               STORE_NO, \n");
            sbSQL.AppendFormat("               '' PRODNO, \n");
            sbSQL.AppendFormat("               門市名稱 || 銷售型態 || '小計' AS 門市編號, \n");
            sbSQL.AppendFormat("               '' 門市名稱, \n");
            sbSQL.AppendFormat("               '' 商品類別, \n");
            sbSQL.AppendFormat("               '' 商品料號, \n");
            sbSQL.AppendFormat("               '' 商品名稱, \n");
            sbSQL.AppendFormat("               '' 科目1, \n");
            sbSQL.AppendFormat("               '' 科目2, \n");
            sbSQL.AppendFormat("               '' 科目3, \n");
            sbSQL.AppendFormat("               '' 科目4, \n");
            sbSQL.AppendFormat("               '' 科目5, \n");
            sbSQL.AppendFormat("               '' 科目6, \n");
            sbSQL.AppendFormat("               '' 銷售型態, \n");
            sbSQL.AppendFormat("               SUM(數量) 數量, \n");
            sbSQL.AppendFormat("               SUM(金額) 金額 \n");
            sbSQL.AppendFormat("          FROM ( \n");
            sbSQL.AppendFormat(sb.ToString());
            sbSQL.AppendFormat("               ) \n");
            sbSQL.AppendFormat("         GROUP BY STORE_NO, 銷售型態, 門市名稱 \n");
            sbSQL.AppendFormat("         ORDER BY STORE_NO, PRODNO, ORDER_INDEX, 銷售型態 DESC)  \n");
            #endregion 門市小計
            sbSQL.AppendFormat("       UNION ALL \n");
            #region 合計
            sbSQL.AppendFormat("        SELECT '4' ORDER_INDEX, \n");
            sbSQL.AppendFormat("               '' STORE_NO, \n");
            sbSQL.AppendFormat("               '' PRODNO, \n");
            sbSQL.AppendFormat("               銷售型態 || '總計' AS 門市編號, \n");
            sbSQL.AppendFormat("               '' 門市名稱, \n");
            sbSQL.AppendFormat("               '' 商品類別, \n");
            sbSQL.AppendFormat("               '' 商品料號, \n");
            sbSQL.AppendFormat("               '' 商品名稱, \n");
            sbSQL.AppendFormat("               '' 科目1, \n");
            sbSQL.AppendFormat("               '' 科目2, \n");
            sbSQL.AppendFormat("               '' 科目3, \n");
            sbSQL.AppendFormat("               '' 科目4, \n");
            sbSQL.AppendFormat("               '' 科目5, \n");
            sbSQL.AppendFormat("               '' 科目6, \n");
            sbSQL.AppendFormat("               '' 銷售型態, \n");
            sbSQL.AppendFormat("               SUM(數量) 數量, \n");
            sbSQL.AppendFormat("               SUM(金額) 金額 \n");
            sbSQL.AppendFormat("          FROM ( \n");
            sbSQL.AppendFormat(sb.ToString());
            sbSQL.AppendFormat("               ) \n");
            sbSQL.AppendFormat("         GROUP BY 銷售型態 \n");
            #endregion
            #endregion 加入 商品小計 門市小計 合計

            DataTable dt = OracleDBUtil.Query_Data(sbSQL.ToString());
            return dt;
        }


        /*Author : 蔡坤霖
          Date : 2011 / 02 / 16
          Description: RPL048 HAPPY GO明細表
         */
        /// <summary>
        /// RPL048 HAPPY GO明細表
        /// </summary>
        /// <param name="TYPE1">類別</param>
        /// <param name="TYPE2">分類</param>
        /// <param name="STORE_NO">門市編號</param>
        /// <param name="DATE_S">交易日期(起)</param>
        /// <param name="DATE_E">交易日期(訖)</param>
        /// <param name="TRAN_NO">交易序號</param>
        /// <param name="HG_CARD_NO">Happy Go卡號</param>
        /// <param name="MACHINE_ID">機台編號</param>
        public DataTable RPL048(string TYPE1, string TYPE2, string STORE_NO, string DATE_S, string DATE_E, string TRAN_NO, string HG_CARD_NO, string MACHINE_ID)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("SELECT STORE_NO 門市編號, ");
            sb.AppendFormat("       STORE_NAME 門市名稱, ");
            sb.AppendFormat("       TO_CHAR(TRADE_DATE, 'YYYY/MM/DD') 交易日期, ");
            sb.AppendFormat("       SALE_NO 交易序號, ");
            sb.AppendFormat("       MACHINE_ID 機台編號, ");
            sb.AppendFormat("       TYPE1 \"兌點/累點\", ");
            sb.AppendFormat("       TYPE2 \"分類\", ");
            sb.AppendFormat("       HG_CARD_NO \"Happy Go卡號\", ");
            sb.AppendFormat("       CONVERT_NAME 項目名稱, ");
            sb.AppendFormat("       SALE_QTY 數量, ");
            sb.AppendFormat("       INVOICE_NO 發票號碼, ");
            sb.AppendFormat("       HG_REDEEM_POINT \"累/兌點數\", ");
            sb.AppendFormat("       TOTAL_AMOUNT 兌換金額, ");
            sb.AppendFormat("       CREATE_USER 銷售人員 ");
            sb.AppendFormat("  FROM VW_RPL_HAPPY_GO_REPORT T ");
            sb.AppendFormat(" WHERE 1 = 1 ");

            if (!string.IsNullOrEmpty(TYPE1) && TYPE1.Trim() != "ALL")
            {
                sb.Append(" AND TYPE1 = " + OracleDBUtil.SqlStr(TYPE1.Trim()));
            }
            if (!string.IsNullOrEmpty(TYPE2) && TYPE2.Trim() != "ALL")
            {
                sb.Append(" AND TYPE2 = " + OracleDBUtil.SqlStr(TYPE2.Trim()));
            }
            if (!string.IsNullOrEmpty(STORE_NO))
            {
                sb.Append(" AND STORE_NO = " + OracleDBUtil.SqlStr(STORE_NO.Trim()));
            }
            if (!string.IsNullOrEmpty(DATE_S))
            {
                sb.Append(" AND TRUNC(TRADE_DATE) >= " + OracleDBUtil.DateStr(DATE_S.Trim()));
            }
            if (!string.IsNullOrEmpty(DATE_E))
            {
                sb.Append(" AND TRUNC(TRADE_DATE) <= " + OracleDBUtil.DateStr(DATE_E.Trim()));
            }
            if (!string.IsNullOrEmpty(TRAN_NO))
            {
                sb.Append(" AND SALE_NO = " + OracleDBUtil.SqlStr(TRAN_NO.Trim()));
            }
            if (!string.IsNullOrEmpty(HG_CARD_NO))
            {
                sb.Append(" AND HG_CARD_NO = " + OracleDBUtil.SqlStr(HG_CARD_NO.Trim()));
            }
            if (!string.IsNullOrEmpty(MACHINE_ID) && MACHINE_ID.Trim() != "ALL")
            {
                sb.Append(" AND MACHINE_ID = " + OracleDBUtil.SqlStr(MACHINE_ID.Trim()));
            }

            DataTable dt = OracleDBUtil.Query_Data(sb.ToString());
            return dt;
        }

        /*Author : 蔡坤霖
          Date : 2011 / 02 / 16
          Description: RPL049 信用卡交易明細表
         */
        public DataTable RPL049(string STORE_S, string STORE_E, string DATE_S, string DATE_E, string PAID, string CARDNO)
        {
            StringBuilder sb = new StringBuilder();

            #region -SELECT-

            sb.AppendFormat("SELECT T.STORE_NO   AS 門市編號  \n");
            sb.AppendFormat("      ,T.STORE_NAME AS 門市名稱  \n");
            sb.AppendFormat("      ,TO_CHAR(T.TRADE_DATE, 'YYYY/MM/DD') AS 交易日期  \n");
            sb.AppendFormat("      ,T.SALE_NO    AS 交易序號  \n");
            sb.AppendFormat("      ,T.MACHINE_ID AS 機台編號  \n");
            sb.AppendFormat("      ,T.PAID_MODE_NAME AS 刷卡類型  \n");
            sb.AppendFormat("      ,T.INVOICE_NO     AS 發票號碼  \n");
            //sb.AppendFormat("      ,DECODE(T.SALE_TYPE,1,T.INVOICE_NO,'')     AS 發票號碼  \n");
            sb.AppendFormat("      ,T.PAID_AMOUNT    AS 發票金額  \n");
            sb.AppendFormat("      ,DECODE(T.PAID_MODE,2,0,3,0,T.PAID_AMOUNT) AS 分期付款  \n");
            sb.AppendFormat("      ,T.CREDIT_INSTALLMENT AS 分期期數      \n");
            sb.AppendFormat("      ,T.BANK_NAME          AS 分期銀行名稱  \n");
            sb.AppendFormat("      ,T.CREDIT_CARD_NO     AS 信用卡號      \n");
            sb.AppendFormat("      ,T.SALE_PERSON        AS 處理人員      \n");
            sb.AppendFormat("  FROM VW_RPL_CRDCARD_DTL T                  \n");
            sb.AppendFormat(" WHERE 1 = 1                                   ");

            #endregion

            #region -WHERE-

            if (!string.IsNullOrEmpty(STORE_S))
            {
                sb.Append(" AND STORE_NO >= " + OracleDBUtil.SqlStr(STORE_S.Trim()));
            }
            if (!string.IsNullOrEmpty(STORE_E))
            {
                sb.Append(" AND STORE_NO <= " + OracleDBUtil.SqlStr(STORE_E.Trim()));
            }
            if (!string.IsNullOrEmpty(DATE_S))
            {
                sb.Append(" AND TRUNC(TRADE_DATE) >= " + OracleDBUtil.DateStr(DATE_S.Trim()));
            }
            if (!string.IsNullOrEmpty(DATE_E))
            {
                sb.Append(" AND TRUNC(TRADE_DATE) <= " + OracleDBUtil.DateStr(DATE_E.Trim()));
            }
            if (!string.IsNullOrEmpty(PAID) && PAID.Trim() != "ALL")
            {
                sb.Append(" AND PAID_MODE = " + OracleDBUtil.SqlStr(PAID.Trim()));
            }
            if (!string.IsNullOrEmpty(CARDNO))
            {
                sb.Append(" AND CREDIT_CARD_NO = " + OracleDBUtil.SqlStr(CARDNO.Trim()));
            }
            #endregion

            #region -ORDER BY-
            sb.AppendFormat(" ORDER BY T.STORE_NO ,TO_CHAR(T.TRADE_DATE, 'YYYY/MM/DD') ,T.SALE_NO ");
            #endregion

            DataTable dt = OracleDBUtil.Query_Data(sb.ToString());
            return dt;
        }

        /*Author : 蔡坤霖
          Date : 2011 / 03 / 03
          Description: RPL049 信用卡交易明細表
         */
        public DataTable RPL049_TOTAL(string STORE_S, string STORE_E, string DATE_S, string DATE_E, string PAID, string CARDNO)
        {
            StringBuilder sb = new StringBuilder();

            #region -SELECT-

            sb.AppendFormat("WITH RPL049 AS ( \n");
            sb.AppendFormat("SELECT T.STORE_NO   AS 門市編號  \n");
            sb.AppendFormat("      ,T.STORE_NAME AS 門市名稱  \n");
            sb.AppendFormat("      ,TO_CHAR(T.TRADE_DATE, 'YYYY/MM/DD') AS 交易日期  \n");
            sb.AppendFormat("      ,T.SALE_NO    AS 交易序號  \n");
            sb.AppendFormat("      ,T.MACHINE_ID AS 機台編號  \n");
            sb.AppendFormat("      ,T.PAID_MODE_NAME AS 刷卡類型  \n");
            sb.AppendFormat("      ,T.INVOICE_NO     AS 發票號碼  \n");
            //sb.AppendFormat("      ,DECODE(T.SALE_TYPE,1,T.INVOICE_NO,'')     AS 發票號碼  \n");
            sb.AppendFormat("      ,T.PAID_AMOUNT    AS 發票金額  \n");
            sb.AppendFormat("      ,DECODE(T.PAID_MODE,2,0,3,0,T.PAID_AMOUNT) AS 分期付款  \n");
            sb.AppendFormat("      ,T.CREDIT_INSTALLMENT AS 分期期數      \n");
            sb.AppendFormat("      ,T.BANK_NAME          AS 分期銀行名稱  \n");
            sb.AppendFormat("      ,T.CREDIT_CARD_NO     AS 信用卡號      \n");
            sb.AppendFormat("      ,T.SALE_PERSON        AS 處理人員      \n");
            sb.AppendFormat("  FROM VW_RPL_CRDCARD_DTL T \n");
            sb.AppendFormat(" WHERE 1 = 1 \n");

            #endregion

            #region -WHERE-

            if (!string.IsNullOrEmpty(STORE_S))
            {
                sb.Append(" AND STORE_NO >= " + OracleDBUtil.SqlStr(STORE_S.Trim()));
            }
            if (!string.IsNullOrEmpty(STORE_E))
            {
                sb.Append(" AND STORE_NO <= " + OracleDBUtil.SqlStr(STORE_E.Trim()));
            }
            if (!string.IsNullOrEmpty(DATE_S))
            {
                sb.Append(" AND TRUNC(TRADE_DATE) >= " + OracleDBUtil.DateStr(DATE_S.Trim()));
            }
            if (!string.IsNullOrEmpty(DATE_E))
            {
                sb.Append(" AND TRUNC(TRADE_DATE) <= " + OracleDBUtil.DateStr(DATE_E.Trim()));
            }
            if (!string.IsNullOrEmpty(PAID) && PAID.Trim() != "ALL")
            {
                sb.Append(" AND PAID_MODE = " + OracleDBUtil.SqlStr(PAID.Trim()));
            }
            if (!string.IsNullOrEmpty(CARDNO))
            {
                sb.Append(" AND CREDIT_CARD_NO = " + OracleDBUtil.SqlStr(CARDNO.Trim()));
            }
            #endregion

            #region -ORDER BY-
            sb.AppendFormat(" ORDER BY T.STORE_NO ,TO_CHAR(T.TRADE_DATE, 'YYYY/MM/DD') ,T.SALE_NO ");
            #endregion

            sb.AppendFormat(" ) \n");
            sb.AppendFormat(" SELECT * \n");
            sb.AppendFormat("   FROM RPL049 \n");

            #region 統計部分
            sb.AppendFormat(" UNION ALL \n");
            sb.AppendFormat(" SELECT NULL, \n");
            sb.AppendFormat("        NULL, \n");
            sb.AppendFormat("        NULL, \n");
            sb.AppendFormat("        NULL, \n");
            sb.AppendFormat("        NULL, \n");
            sb.AppendFormat("        NULL, \n");
            sb.AppendFormat("        '總計', \n");
            sb.AppendFormat("        NVL(SUM(發票金額), 0), \n");
            sb.AppendFormat("        NVL(SUM(分期付款), 0), \n");
            sb.AppendFormat("        NULL, \n");
            sb.AppendFormat("        NULL, \n");
            sb.AppendFormat("        NULL, \n");
            sb.AppendFormat("        NULL \n");
            sb.AppendFormat("   FROM RPL049 \n");
            #endregion

            DataTable dt = OracleDBUtil.Query_Data(sb.ToString());
            return dt;
        }

        /*Author : 蔡坤霖
                 Date : 2011 / 02 / 15
                 Description: RPL059 信用卡明細表
                */
        /// <summary>
        /// RPL059 信用卡明細表
        /// </summary>
        /// <param name="DATE_S">交易日期(起)</param>
        /// <param name="DATE_E">交易日期(訖)</param>
        /// <param name="AMOUNT_S">交易金額(起)</param>
        /// <param name="AMOUNT_E">交易金額(訖)</param>
        /// <param name="TRAN_TYPE">交易類型</param>
        /// <param name="CARD_TYPE">卡別</param>
        public DataTable RPL059(string DATE_S, string DATE_E, string AMOUNT_S, string AMOUNT_E, string TRAN_TYPE, string CARD_TYPE, string STORE_NO)
        {
            StringBuilder sb = new StringBuilder();

            #region -SELECT
            sb.Append(" SELECT DISTINCT TO_CHAR(TRADE_DATE,'YYYY/MM/DD') AS 交易日期 ");
            sb.Append("       ,MACHINE_ID             AS 機台編號           ");
            sb.Append("       ,SALE_NO                AS 交易序號           ");
            sb.Append("       ,CREDIT_CARD_AUTH_NO    AS 銀行授權號碼       ");
            sb.Append("       ,SALE_TYPE              AS 交易類型           ");
            sb.Append("       ,NVL(TRIM(FN_SAL_QUERY_BARCODE1(POSUUID_MASTER)),INVOICE_NO )            AS 發票號碼           ");
            //sb.Append("       ,INVOICE_NO             AS 發票號碼           ");
            sb.Append("       ,FN_SAL_QUERY_MSISDN(POSUUID_MASTER) AS 用戶門號     ");
            //sb.Append("       ,MSISDN                 AS 用戶門號           ");
            sb.Append("       ,CREDID_CARD_TYPE_NAME  AS 信用卡別           ");
            sb.Append("       ,CREDIT_CARD_NO         AS 信用卡號           ");
            sb.Append("       ,TO_CHAR(NVL(PAID_AMOUNT,0))     AS 信用卡金額         ");
            sb.Append("       ,REMARK                 AS 備註               ");
            sb.Append("   FROM VW_RPL_CREDIT_CARD_REPORT                    ");
            sb.Append("  WHERE 1 = 1                                        ");
            #endregion

            #region -WHERE-
            if (!string.IsNullOrEmpty(TRAN_TYPE) && TRAN_TYPE.Trim() != "ALL")
            {
                sb.Append(" AND SALE_TYPE = " + OracleDBUtil.SqlStr(TRAN_TYPE.Trim()));
            }
            if (!string.IsNullOrEmpty(CARD_TYPE) && CARD_TYPE.Trim() != "ALL")
            {
                sb.Append(" AND CREDID_CARD_TYPE_NAME = " + OracleDBUtil.SqlStr(CARD_TYPE.Trim()));
            }
            if (!string.IsNullOrEmpty(AMOUNT_S))
            {
                sb.Append(" AND PAID_AMOUNT >= " + OracleDBUtil.SqlStr(AMOUNT_S.Trim()));
            }
            if (!string.IsNullOrEmpty(AMOUNT_E))
            {
                sb.Append(" AND PAID_AMOUNT <= " + OracleDBUtil.SqlStr(AMOUNT_E.Trim()));
            }
            if (!string.IsNullOrEmpty(DATE_S))
            {
                sb.Append(" AND TRUNC(TRADE_DATE) >= " + OracleDBUtil.DateStr(DATE_S.Trim()));
            }
            if (!string.IsNullOrEmpty(DATE_E))
            {
                sb.Append(" AND TRUNC(TRADE_DATE) <= " + OracleDBUtil.DateStr(DATE_E.Trim()));
            }
            if (!string.IsNullOrEmpty(STORE_NO))
            {
                sb.Append(" AND STORE_NO = " + OracleDBUtil.SqlStr(STORE_NO.Trim()));
            }
            #endregion

            #region -ORDER BY
            sb.AppendFormat(" ORDER BY TO_CHAR(TRADE_DATE,'YYYY/MM/DD') ,SALE_NO ");
            #endregion

            DataTable dt = OracleDBUtil.Query_Data(sb.ToString());
            return dt;
        }


        /*Author : 蔡坤霖
          Date : 2011 / 02 / 15
          Description: RPL059 信用卡明細表 合計部分
         */
        public DataTable RPL059_SUM(string DATE_S, string DATE_E, string AMOUNT_S, string AMOUNT_E, string TRAN_TYPE, string CARD_TYPE, string STORE_NO)
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendFormat("SELECT CREDIT_CARD_TYPE_NAME || '  小計' 卡別, ");
            sb.AppendFormat("       NVL(金額, 0) 金額, ");
            sb.AppendFormat("       NVL(數量, 0) 數量 ");
            sb.AppendFormat("  FROM (SELECT VRCC.CREDID_CARD_TYPE_NAME 卡別, ");
            sb.AppendFormat("               SUM(VRCC.PAID_AMOUNT) 金額, ");
            sb.AppendFormat("               COUNT(VRCC.CREDID_CARD_TYPE_NAME) 數量 ");
            sb.AppendFormat("          FROM VW_RPL_CREDIT_CARD_REPORT VRCC ");
            sb.AppendFormat("         WHERE 1 = 1                          ");

            #region -WHERE-

            if (!string.IsNullOrEmpty(TRAN_TYPE) && TRAN_TYPE.Trim() != "ALL")
            {
                sb.Append(" AND SALE_TYPE = " + OracleDBUtil.SqlStr(TRAN_TYPE.Trim()));
            }
            if (!string.IsNullOrEmpty(CARD_TYPE) && CARD_TYPE.Trim() != "ALL")
            {
                sb.Append(" AND CREDID_CARD_TYPE_NAME = " + OracleDBUtil.SqlStr(CARD_TYPE.Trim()));
            }
            if (!string.IsNullOrEmpty(AMOUNT_S))
            {
                sb.Append(" AND PAID_AMOUNT >= " + OracleDBUtil.SqlStr(AMOUNT_S.Trim()));
            }
            if (!string.IsNullOrEmpty(AMOUNT_E))
            {
                sb.Append(" AND PAID_AMOUNT <= " + OracleDBUtil.SqlStr(AMOUNT_E.Trim()));
            }
            if (!string.IsNullOrEmpty(DATE_S))
            {
                sb.Append(" AND TRUNC(TRADE_DATE) >= " + OracleDBUtil.DateStr(DATE_S.Trim()));
            }
            if (!string.IsNullOrEmpty(DATE_E))
            {
                sb.Append(" AND TRUNC(TRADE_DATE) <= " + OracleDBUtil.DateStr(DATE_E.Trim()));
            }
            if (!string.IsNullOrEmpty(STORE_NO))
            {
                sb.Append(" AND STORE_NO = " + OracleDBUtil.SqlStr(STORE_NO.Trim()));
            }
            #endregion

            sb.AppendFormat("         GROUP BY VRCC.CREDID_CARD_TYPE_NAME) T, ");
            sb.AppendFormat("       CREDIT_CARD_TYPE CCT ");
            sb.AppendFormat(" WHERE T.卡別(+) = CCT.CREDIT_CARD_TYPE_NAME ");
            sb.AppendFormat(" ");
            sb.AppendFormat("UNION ALL ");
            sb.AppendFormat(" ");
            sb.AppendFormat("SELECT '門市總計' AS 卡別, SUM(金額), SUM(數量) ");
            sb.AppendFormat("  FROM (SELECT CREDIT_CARD_TYPE_NAME || '  小計' 卡別, ");
            sb.AppendFormat("               NVL(金額, 0) 金額, ");
            sb.AppendFormat("               NVL(數量, 0) 數量 ");
            sb.AppendFormat("          FROM (SELECT VRCC.CREDID_CARD_TYPE_NAME 卡別, ");
            sb.AppendFormat("                       SUM(VRCC.PAID_AMOUNT) 金額, ");
            sb.AppendFormat("                       COUNT(VRCC.CREDID_CARD_TYPE_NAME) 數量 ");
            sb.AppendFormat("                  FROM VW_RPL_CREDIT_CARD_REPORT VRCC ");
            sb.AppendFormat("                 WHERE 1 = 1                          ");

            #region -WHERE-

            if (!string.IsNullOrEmpty(TRAN_TYPE) && TRAN_TYPE.Trim() != "ALL")
            {
                sb.Append(" AND SALE_TYPE = " + OracleDBUtil.SqlStr(TRAN_TYPE.Trim()));
            }
            if (!string.IsNullOrEmpty(CARD_TYPE) && CARD_TYPE.Trim() != "ALL")
            {
                sb.Append(" AND CREDID_CARD_TYPE_NAME = " + OracleDBUtil.SqlStr(CARD_TYPE.Trim()));
            }
            if (!string.IsNullOrEmpty(AMOUNT_S))
            {
                sb.Append(" AND PAID_AMOUNT >= " + OracleDBUtil.SqlStr(AMOUNT_S.Trim()));
            }
            if (!string.IsNullOrEmpty(AMOUNT_E))
            {
                sb.Append(" AND PAID_AMOUNT <= " + OracleDBUtil.SqlStr(AMOUNT_E.Trim()));
            }
            if (!string.IsNullOrEmpty(DATE_S))
            {
                sb.Append(" AND TRUNC(TRADE_DATE) >= " + OracleDBUtil.DateStr(DATE_S.Trim()));
            }
            if (!string.IsNullOrEmpty(DATE_E))
            {
                sb.Append(" AND TRUNC(TRADE_DATE) <= " + OracleDBUtil.DateStr(DATE_E.Trim()));
            }
            if (!string.IsNullOrEmpty(STORE_NO))
            {
                sb.Append(" AND STORE_NO = " + OracleDBUtil.SqlStr(STORE_NO.Trim()));
            }
            #endregion

            sb.AppendFormat("                 GROUP BY VRCC.CREDID_CARD_TYPE_NAME) T, ");
            sb.AppendFormat("               CREDIT_CARD_TYPE CCT ");
            sb.AppendFormat("         WHERE T.卡別(+) = CCT.CREDIT_CARD_TYPE_NAME) ");

            DataTable dt = OracleDBUtil.Query_Data(sb.ToString());
            return dt;
        }

        /*Author : 蔡坤霖
          Date : 2011 / 02 / 15
          Description: RPL066 維修費明細表
         */
        /// <summary>
        /// RPL066 維修費明細表
        /// </summary>
        /// <param name="SOTRE_NO"></param>
        /// <param name="FIXNO"></param>
        /// <param name="IMEI"></param>
        /// <param name="SUPP"></param>
        /// <param name="FIXDATE_S"></param>
        /// <param name="FIXDATE_E"></param>
        /// <param name="INVOICE_DATE_S"></param>
        /// <param name="INVOICE_DATE_E"></param>
        /// <returns></returns>
        public DataTable RPL066(string STORENO, string FIXNO, string IMEINO, string SUPP, string FIXDATE_S, string FIXDATE_E, string INVOICE_DATE_S, string INVOICE_DATE_E, string PERSON)
        {
            StringBuilder sb = new StringBuilder();

            #region -SELECT-
            sb.AppendFormat("SELECT T.STORE_NO   AS 門市編號    ");
            sb.AppendFormat("      ,T.STORE_NAME AS 門市名稱    ");
            sb.AppendFormat("      ,T.HRS_NO     AS 維修單號    ");
            sb.AppendFormat("      ,T.FIXDATE    AS 維修日期    ");
            sb.AppendFormat("      ,T.FIX_BRAND  AS 維修廠商    ");
            sb.AppendFormat("      ,T.PROD_TYPE  AS 商品類別    ");
            sb.AppendFormat("      ,T.BRAND      AS 廠牌        ");
            sb.AppendFormat("      ,T.MODEL      AS 型號        ");
            sb.AppendFormat("      ,T.IMEINO     AS IMEI        ");
            sb.AppendFormat("      ,T.AMOUNT     AS 維修費用    ");
            sb.AppendFormat("      ,TO_CHAR(T.INVOICE_DATE,'YYYY/MM/DD') AS 發票日期    ");
            sb.AppendFormat("      ,T.INVOICE_NO AS 發票號碼    ");
            sb.AppendFormat("      ,T.PERSON || T.EMPNAME AS 處理人員   ");
            sb.AppendFormat("  FROM VW_RPL_REPAIR_FEE_DTL T     ");
            sb.AppendFormat(" WHERE 1 = 1                       ");
            #endregion

            #region -WHERE-
            #region -門市編號-
            if (!string.IsNullOrEmpty(STORENO))
            {
                sb.Append(" AND STORE_NO = " + OracleDBUtil.SqlStr(STORENO.Trim()));
            }
            #endregion
            #region -維修單號-
            if (!string.IsNullOrEmpty(FIXNO))
            {
                sb.Append(" AND HRS_NO = " + OracleDBUtil.SqlStr(FIXNO.Trim()));
            }
            #endregion
            #region -IMEI-
            if (!string.IsNullOrEmpty(IMEINO))
            {
                sb.Append(" AND IMEINO = " + OracleDBUtil.SqlStr(IMEINO.Trim()));
            }
            #endregion
            #region -維護廠商-
            if (!string.IsNullOrEmpty(SUPP) && SUPP.ToUpper() != "ALL")
            {
                sb.Append(" AND FIX_BRAND = " + OracleDBUtil.SqlStr(SUPP.Trim()));
            }
            #endregion
            #region -維修日期-
            if (!string.IsNullOrEmpty(FIXDATE_S))
            {
                sb.Append(" AND FIXDATE >= " + OracleDBUtil.SqlStr(FIXDATE_S.Trim().Replace("/", "")));
            }
            if (!string.IsNullOrEmpty(FIXDATE_E))
            {
                sb.Append(" AND FIXDATE <= " + OracleDBUtil.SqlStr(FIXDATE_E.Trim().Replace("/", "")));
            }
            #endregion
            #region -發票日期-
            if (!string.IsNullOrEmpty(INVOICE_DATE_S))
            {
                sb.Append(" AND TRUNC(INVOICE_DATE) >= " + OracleDBUtil.DateStr(INVOICE_DATE_S.Trim()));
            }
            if (!string.IsNullOrEmpty(INVOICE_DATE_E))
            {
                sb.Append(" AND TRUNC(INVOICE_DATE) <= " + OracleDBUtil.DateStr(INVOICE_DATE_E.Trim()));
            }
            #endregion
            #region -處理人員-
            if (!string.IsNullOrEmpty(PERSON) && PERSON.ToUpper() != "ALL")
            {
                sb.Append(" AND NVL(PERSON," + OracleDBUtil.SqlStr(PERSON.Trim()) + ") = " + OracleDBUtil.SqlStr(PERSON.Trim()));
            }
            #endregion
            #endregion

            #region -ORDER BY-
            sb.AppendFormat(" ORDER BY STORE_NO ,HRS_NO ");
            #endregion

            DataTable dt = OracleDBUtil.Query_Data(sb.ToString());
            return dt;
        }

        /*Author : 蔡坤霖
          Date : 2011 / 02 / 15
          Description: RPL066 維修費明細表
         */
        /// <summary>
        /// RPL066 維修費明細表
        /// </summary>
        /// <param name="SOTRE_NO"></param>
        /// <param name="FIXNO"></param>
        /// <param name="IMEI"></param>
        /// <param name="SUPP"></param>
        /// <param name="FIXDATE_S"></param>
        /// <param name="FIXDATE_E"></param>
        /// <param name="INVOICE_DATE_S"></param>
        /// <param name="INVOICE_DATE_E"></param>
        /// <returns></returns>
        public DataTable RPL066_SUM(string STORENO, string FIXNO, string IMEINO, string SUPP, string FIXDATE_S, string FIXDATE_E, string INVOICE_DATE_S, string INVOICE_DATE_E, string PERSON)
        {
            StringBuilder sb = new StringBuilder();

            #region -SELECT-
            sb.AppendFormat("WITH RPL066 AS ( \n");
            sb.AppendFormat("SELECT '1'          AS ORDER_INDEX ");
            sb.AppendFormat("      ,T.STORE_NO   AS 門市編號    ");
            sb.AppendFormat("      ,T.STORE_NAME AS 門市名稱    ");
            sb.AppendFormat("      ,T.HRS_NO     AS 維修單號    ");
            sb.AppendFormat("      ,T.FIXDATE    AS 維修日期    ");
            sb.AppendFormat("      ,T.FIX_BRAND  AS 維修廠商    ");
            sb.AppendFormat("      ,T.PROD_TYPE  AS 商品類別    ");
            sb.AppendFormat("      ,T.BRAND      AS 廠牌        ");
            sb.AppendFormat("      ,T.MODEL      AS 型號        ");
            sb.AppendFormat("      ,T.IMEINO     AS IMEI        ");
            sb.AppendFormat("      ,T.AMOUNT     AS 維修費用    ");
            sb.AppendFormat("      ,TO_CHAR(T.INVOICE_DATE,'YYYY/MM/DD') AS 發票日期    ");
            sb.AppendFormat("      ,T.INVOICE_NO AS 發票號碼    ");
            sb.AppendFormat("      ,T.PERSON || T.EMPNAME AS 處理人員   ");
            sb.AppendFormat("  FROM VW_RPL_REPAIR_FEE_DTL T     ");
            sb.AppendFormat(" WHERE 1 = 1                       ");
            #endregion

            #region -WHERE-
            #region -門市編號-
            if (!string.IsNullOrEmpty(STORENO))
            {
                sb.Append(" AND STORE_NO = " + OracleDBUtil.SqlStr(STORENO.Trim()));
            }
            #endregion
            #region -維修單號-
            if (!string.IsNullOrEmpty(FIXNO))
            {
                sb.Append(" AND HRS_NO = " + OracleDBUtil.SqlStr(FIXNO.Trim()));
            }
            #endregion
            #region -IMEI-
            if (!string.IsNullOrEmpty(IMEINO))
            {
                sb.Append(" AND IMEINO = " + OracleDBUtil.SqlStr(IMEINO.Trim()));
            }
            #endregion
            #region -維護廠商-
            if (!string.IsNullOrEmpty(SUPP) && SUPP.ToUpper() != "ALL")
            {
                sb.Append(" AND FIX_BRAND = " + OracleDBUtil.SqlStr(SUPP.Trim()));
            }
            #endregion
            #region -維修日期-
            if (!string.IsNullOrEmpty(FIXDATE_S))
            {
                sb.Append(" AND FIXDATE >= " + OracleDBUtil.SqlStr(FIXDATE_S.Trim().Replace("/", "")));
            }
            if (!string.IsNullOrEmpty(FIXDATE_E))
            {
                sb.Append(" AND FIXDATE <= " + OracleDBUtil.SqlStr(FIXDATE_E.Trim().Replace("/", "")));
            }
            #endregion
            #region -發票日期-
            if (!string.IsNullOrEmpty(INVOICE_DATE_S))
            {
                sb.Append(" AND TRUNC(INVOICE_DATE) >= " + OracleDBUtil.DateStr(INVOICE_DATE_S.Trim()));
            }
            if (!string.IsNullOrEmpty(INVOICE_DATE_E))
            {
                sb.Append(" AND TRUNC(INVOICE_DATE) <= " + OracleDBUtil.DateStr(INVOICE_DATE_E.Trim()));
            }
            #endregion
            #region -處理人員-
            if (!string.IsNullOrEmpty(PERSON) && PERSON.ToUpper() != "ALL")
            {
                sb.Append(" AND NVL(PERSON," + OracleDBUtil.SqlStr(PERSON.Trim()) + ") = " + OracleDBUtil.SqlStr(PERSON.Trim()));
            }
            #endregion
            #endregion

            sb.AppendFormat("  )                ");
            sb.AppendFormat("  SELECT * FROM (  ");
            sb.AppendFormat("  SELECT *         ");
            sb.AppendFormat("    FROM RPL066    ");
            sb.AppendFormat("  UNION ALL        ");

            #region 統計部分 - 匯出顯示

            sb.AppendFormat("  SELECT '2' AS ORDER_INDEX                    ");
            sb.AppendFormat("        ,門市編號 || '門市小計' AS 門市編號    ");
            sb.AppendFormat("        ,''    ");
            sb.AppendFormat("        ,''    ");
            sb.AppendFormat("        ,''    ");
            sb.AppendFormat("        ,''    ");
            sb.AppendFormat("        ,''    ");
            sb.AppendFormat("        ,''    ");
            sb.AppendFormat("        ,''    ");
            sb.AppendFormat("        ,''    ");
            sb.AppendFormat("        ,NVL(SUM(維修費用), 0) 維修費用    ");
            sb.AppendFormat("        ,''    ");
            sb.AppendFormat("        ,''    ");
            sb.AppendFormat("        ,''    ");
            sb.AppendFormat("    FROM RPL066        ");
            sb.AppendFormat("   GROUP BY 門市編號   ");
            sb.AppendFormat("   UNION ALL           ");
            sb.AppendFormat("  SELECT '3' AS ORDER_INDEX    ");
            sb.AppendFormat("        ,'總計' AS 門市編號    ");
            sb.AppendFormat("        ,''    ");
            sb.AppendFormat("        ,''    ");
            sb.AppendFormat("        ,''    ");
            sb.AppendFormat("        ,''    ");
            sb.AppendFormat("        ,''    ");
            sb.AppendFormat("        ,''    ");
            sb.AppendFormat("        ,''    ");
            sb.AppendFormat("        ,''    ");
            sb.AppendFormat("        ,NVL(SUM(維修費用), 0) 維修費用    ");
            sb.AppendFormat("        ,''    ");
            sb.AppendFormat("        ,''    ");
            sb.AppendFormat("        ,''    ");
            sb.AppendFormat("    FROM RPL066  ");
            sb.AppendFormat("   )   ");

            #region -ORDER BY-
            sb.AppendFormat("   ORDER BY 門市編號 ,ORDER_INDEX ,維修單號  ");
            #endregion

            #endregion

            DataTable dt = OracleDBUtil.Query_Data(sb.ToString());
            return dt;
        }

        #endregion ---  蔡坤霖
    }
}
