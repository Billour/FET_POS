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
        /*
         Author：宗佑
         Date：100.2.16
         Description：RPL036 SQL敘述
        */
        public DataTable RPL036(string SDATE, string EDATE, string PRODTYPENO_S, string PRODTYPENO_E, string PRODNAME, string NO_S, string NO_E, string PROMONAME)
        {
            StringBuilder sb = new StringBuilder();

            if (!string.IsNullOrEmpty(SDATE) && (Convert.ToInt64(SDATE.Replace("/", ""))) == Convert.ToInt64(DateTime.Now.ToString("yyyyMMdd")))
            {
                #region -SELECT-
                sb.AppendFormat(" SELECT TO_CHAR(TRADE_DATE,'YYYY/MM/DD') as 銷售年月日,");
                sb.AppendFormat(" PRODNO as 商品料號, PRODNAME as 商品名稱,");
                sb.AppendFormat(" PROMOTION_CODE as 促銷代碼, PROMO_NAME as 促銷名稱, DEVICE_SUBSIDY as SUBSIDY,");
                sb.AppendFormat(" QUANTITY as QTY, QTS,");
                sb.AppendFormat(" AMOUNT as 單機價, TRANS_VALUE as 轉換值,");
                sb.AppendFormat(" AMOUNT_1 as 還原單機價, AMOUNT_2 as \"SUBSIDY 差異金額\"");
                sb.AppendFormat(" FROM VW_RPL036 ");
                sb.AppendFormat(" WHERE 1=1");
                #endregion

                #region -WHERE-
                #region 銷售日期
                if (!string.IsNullOrEmpty(SDATE))
                {
                    sb.Append(" AND TRUNC(TRADE_DATE) >= " + OracleDBUtil.DateStr(SDATE.Trim()));
                }
                if (!string.IsNullOrEmpty(EDATE))
                {
                    sb.Append(" AND TRUNC(TRADE_DATE) <= " + OracleDBUtil.DateStr(EDATE.Trim()));
                }
                #endregion
                #region 商品料號
                if (!string.IsNullOrEmpty(PRODTYPENO_S))
                {
                    sb.Append(" AND PRODNO >= " + OracleDBUtil.SqlStr(PRODTYPENO_S.Trim()));
                }
                if (!string.IsNullOrEmpty(PRODTYPENO_E))
                {
                    sb.Append(" AND PRODNO <= " + OracleDBUtil.SqlStr(PRODTYPENO_E.Trim()));
                }
                #endregion
                #region 商品名稱
                if (!string.IsNullOrEmpty(PRODNAME))
                {
                    sb.Append(" AND PRODNAME >= " + OracleDBUtil.SqlStr(PRODNAME.Trim()));
                }
                #endregion
                #region 促銷代碼
                if (!string.IsNullOrEmpty(NO_S))
                {
                    sb.Append(" AND PROMOTION_CODE >= " + OracleDBUtil.SqlStr(NO_S.Trim()));
                }
                if (!string.IsNullOrEmpty(NO_E))
                {
                    sb.Append(" AND PROMOTION_CODE <= " + OracleDBUtil.SqlStr(NO_E.Trim()));
                }
                #endregion
                #region 促銷名稱
                if (!string.IsNullOrEmpty(PROMONAME))
                {
                    sb.Append(" AND PROMO_NAME >= " + OracleDBUtil.SqlStr(PROMONAME.Trim()));
                }
                #endregion
                #endregion
            }
            else if (!string.IsNullOrEmpty(SDATE) && (Convert.ToInt64(EDATE.Replace("/", ""))) < Convert.ToInt64(DateTime.Now.ToString("yyyyMMdd")))
            {
                #region -SELECT-
                sb.AppendFormat(" SELECT TO_CHAR(TRADE_DATE,'YYYY/MM/DD') as 銷售年月日,");
                sb.AppendFormat(" PRODNO as 商品料號, PRODNAME as 商品名稱,");
                sb.AppendFormat(" PROMOTION_CODE as 促銷代碼, PROMO_NAME as 促銷名稱, DEVICE_SUBSIDY as SUBSIDY,");
                sb.AppendFormat(" QUANTITY as QTY, QTS,");
                sb.AppendFormat(" AMOUNT as 單機價, TRANS_VALUE as 轉換值,");
                sb.AppendFormat(" AMOUNT_1 as 還原單機價, AMOUNT_2 as \"SUBSIDY 差異金額\"");
                sb.AppendFormat(" FROM VW_RPL0361 ");
                sb.AppendFormat(" WHERE 1=1");
                #endregion

                #region -WHERE-
                #region 銷售日期
                if (!string.IsNullOrEmpty(SDATE))
                {
                    sb.Append(" AND TRUNC(TRADE_DATE) >= " + OracleDBUtil.DateStr(SDATE.Trim()));
                }
                if (!string.IsNullOrEmpty(EDATE))
                {
                    sb.Append(" AND TRUNC(TRADE_DATE) <= " + OracleDBUtil.DateStr(EDATE.Trim()));
                }
                #endregion
                #region 商品料號
                if (!string.IsNullOrEmpty(PRODTYPENO_S))
                {
                    sb.Append(" AND PRODNO >= " + OracleDBUtil.SqlStr(PRODTYPENO_S.Trim()));
                }
                if (!string.IsNullOrEmpty(PRODTYPENO_E))
                {
                    sb.Append(" AND PRODNO <= " + OracleDBUtil.SqlStr(PRODTYPENO_E.Trim()));
                }
                #endregion
                #region 商品名稱
                if (!string.IsNullOrEmpty(PRODNAME))
                {
                    sb.Append(" AND PRODNAME >= " + OracleDBUtil.SqlStr(PRODNAME.Trim()));
                }
                #endregion
                #region 促銷代碼
                if (!string.IsNullOrEmpty(NO_S))
                {
                    sb.Append(" AND PROMOTION_CODE >= " + OracleDBUtil.SqlStr(NO_S.Trim()));
                }
                if (!string.IsNullOrEmpty(NO_E))
                {
                    sb.Append(" AND PROMOTION_CODE <= " + OracleDBUtil.SqlStr(NO_E.Trim()));
                }
                #endregion
                #region 促銷名稱
                if (!string.IsNullOrEmpty(PROMONAME))
                {
                    sb.Append(" AND PROMO_NAME >= " + OracleDBUtil.SqlStr(PROMONAME.Trim()));
                }
                #endregion
                #endregion

            }
            else if (!string.IsNullOrEmpty(SDATE) && (Convert.ToInt64(EDATE.Replace("/", ""))) == Convert.ToInt64(DateTime.Now.ToString("yyyyMMdd")))
            {
                #region -SELECT-
                sb.AppendFormat(" SELECT TO_CHAR(TRADE_DATE,'YYYY/MM/DD') as 銷售年月日,");
                sb.AppendFormat(" PRODNO as 商品料號, PRODNAME as 商品名稱,");
                sb.AppendFormat(" PROMOTION_CODE as 促銷代碼, PROMO_NAME as 促銷名稱, DEVICE_SUBSIDY as SUBSIDY,");
                sb.AppendFormat(" QUANTITY as QTY, QTS,");
                sb.AppendFormat(" AMOUNT as 單機價, TRANS_VALUE as 轉換值,");
                sb.AppendFormat(" AMOUNT_1 as 還原單機價, AMOUNT_2 as \"SUBSIDY 差異金額\"");
                sb.AppendFormat(" FROM VW_RPL036 ");
                sb.AppendFormat(" WHERE 1=1");
                #endregion

                #region -WHERE-
                #region 銷售日期
                if (!string.IsNullOrEmpty(SDATE))
                {
                    sb.Append(" AND TRUNC(TRADE_DATE) >= " + OracleDBUtil.DateStr(SDATE.Trim()));
                }
                if (!string.IsNullOrEmpty(EDATE))
                {
                    sb.Append(" AND TRUNC(TRADE_DATE) <= " + OracleDBUtil.DateStr(EDATE.Trim()));
                }
                #endregion
                #region 商品料號
                if (!string.IsNullOrEmpty(PRODTYPENO_S))
                {
                    sb.Append(" AND PRODNO >= " + OracleDBUtil.SqlStr(PRODTYPENO_S.Trim()));
                }
                if (!string.IsNullOrEmpty(PRODTYPENO_E))
                {
                    sb.Append(" AND PRODNO <= " + OracleDBUtil.SqlStr(PRODTYPENO_E.Trim()));
                }
                #endregion
                #region 商品名稱
                if (!string.IsNullOrEmpty(PRODNAME))
                {
                    sb.Append(" AND PRODNAME >= " + OracleDBUtil.SqlStr(PRODNAME.Trim()));
                }
                #endregion
                #region 促銷代碼
                if (!string.IsNullOrEmpty(NO_S))
                {
                    sb.Append(" AND PROMOTION_CODE >= " + OracleDBUtil.SqlStr(NO_S.Trim()));
                }
                if (!string.IsNullOrEmpty(NO_E))
                {
                    sb.Append(" AND PROMOTION_CODE <= " + OracleDBUtil.SqlStr(NO_E.Trim()));
                }
                #endregion
                #region 促銷名稱
                if (!string.IsNullOrEmpty(PROMONAME))
                {
                    sb.Append(" AND PROMO_NAME >= " + OracleDBUtil.SqlStr(PROMONAME.Trim()));
                }
                #endregion
                #endregion

                sb.AppendFormat(" UNION ALL ");

                #region -SELECT-
                sb.AppendFormat(" SELECT TO_CHAR(TRADE_DATE,'YYYY/MM/DD') as 銷售年月日,");
                sb.AppendFormat(" PRODNO as 商品料號, PRODNAME as 商品名稱,");
                sb.AppendFormat(" PROMOTION_CODE as 促銷代碼, PROMO_NAME as 促銷名稱, DEVICE_SUBSIDY as SUBSIDY,");
                sb.AppendFormat(" QUANTITY as QTY, QTS,");
                sb.AppendFormat(" AMOUNT as 單機價, TRANS_VALUE as 轉換值,");
                sb.AppendFormat(" AMOUNT_1 as 還原單機價, AMOUNT_2 as \"SUBSIDY 差異金額\"");
                sb.AppendFormat(" FROM VW_RPL0361 ");
                sb.AppendFormat(" WHERE 1=1");
                #endregion

                #region -WHERE-
                #region 銷售日期
                if (!string.IsNullOrEmpty(SDATE))
                {
                    sb.Append(" AND TRUNC(TRADE_DATE) >= " + OracleDBUtil.DateStr(SDATE.Trim()));
                }
                if (!string.IsNullOrEmpty(EDATE))
                {
                    sb.Append(" AND TRUNC(TRADE_DATE) <= " + OracleDBUtil.DateStr(EDATE.Trim()));
                }
                #endregion
                #region 商品料號
                if (!string.IsNullOrEmpty(PRODTYPENO_S))
                {
                    sb.Append(" AND PRODNO >= " + OracleDBUtil.SqlStr(PRODTYPENO_S.Trim()));
                }
                if (!string.IsNullOrEmpty(PRODTYPENO_E))
                {
                    sb.Append(" AND PRODNO <= " + OracleDBUtil.SqlStr(PRODTYPENO_E.Trim()));
                }
                #endregion
                #region 商品名稱
                if (!string.IsNullOrEmpty(PRODNAME))
                {
                    sb.Append(" AND PRODNAME >= " + OracleDBUtil.SqlStr(PRODNAME.Trim()));
                }
                #endregion
                #region 促銷代碼
                if (!string.IsNullOrEmpty(NO_S))
                {
                    sb.Append(" AND PROMOTION_CODE >= " + OracleDBUtil.SqlStr(NO_S.Trim()));
                }
                if (!string.IsNullOrEmpty(NO_E))
                {
                    sb.Append(" AND PROMOTION_CODE <= " + OracleDBUtil.SqlStr(NO_E.Trim()));
                }
                #endregion
                #region 促銷名稱
                if (!string.IsNullOrEmpty(PROMONAME))
                {
                    sb.Append(" AND PROMO_NAME >= " + OracleDBUtil.SqlStr(PROMONAME.Trim()));
                }
                #endregion
                #endregion

            }


            DataTable dt = OracleDBUtil.Query_Data(sb.ToString());
            return dt;
        }
        #endregion
        #region 宗佑
        /*
         Author：宗佑
         Date：100.2.14(初版)
               100.2.17(第一次修正商品編號更正為商品料號)
         Description：RPL039 SQL敘述
        */
        public DataTable RPL039(string SDATE, string EDATE)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(" SELECT PROMO_NO as 促銷代碼,");
            sb.Append(" PROMO_NAME as 促銷名稱, PROD_NO as 商品料號,");
            sb.Append(" PRODNAME as 商品名稱, AMOUNT as 組合促銷價格, PRICE as 單機價, TRANS_VALUE as 組合促銷轉換值,");
            sb.Append(" DEVICE_SUBSIDY as 該促銷的補貼金額, BASE_SUBSIDY as 基準補貼值,");
            sb.Append(" SUBSIDY as 補貼差異值,");
            sb.Append(" '' as 價格變動");
            sb.Append(" FROM VW_RPL_PROMO_PRICE_REPORT");
            sb.Append(" WHERE 1=1");

            #region 生效日期
            if (!string.IsNullOrEmpty(SDATE))
            {
                sb.Append(" AND TRUNC(B_DATE) >= " + OracleDBUtil.DateStr(SDATE.Trim()));
            }
            if (!string.IsNullOrEmpty(EDATE))
            {
                sb.Append(" AND TRUNC(B_DATE) <= " + OracleDBUtil.DateStr(EDATE.Trim()));
            }
            #endregion

            DataTable dt = OracleDBUtil.Query_Data(sb.ToString());
            return dt;
        }
        #endregion
        public DataTable RPL061(string SDATE, string EDATE, string INStore, string InStore2, string SPRODNO, string EPRODNO)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(" SELECT to_char(STDATE,'YYYY/MM/DD') as STDATE,");
            sb.Append(" STNO, FROM_STORENAME,");
            sb.Append(" TO_STORE_NO, TO_STORENAME, FROM_STORE_NO, FROM_EMPNAME,");
            sb.Append(" TO_EMPNAME, PRODNO,");
            sb.Append(" PRODNAME, TRANOUTQTY,");
            sb.Append(" TRANINQTY, IMEI");
            sb.Append(" FROM VW_RPL_STORETRANSFER_D");
            sb.Append(" WHERE 1=1");

            if (!string.IsNullOrEmpty(SDATE))
            {
                sb.Append(" AND TRUNC(STDATE) >= " + OracleDBUtil.DateStr(SDATE.Trim()));
            }
            if (!string.IsNullOrEmpty(EDATE))
            {
                sb.Append(" AND TRUNC(STDATE) <= " + OracleDBUtil.DateStr(EDATE.Trim()));
            }
            if (!string.IsNullOrEmpty(INStore))
            {
                sb.Append(" AND (TO_STORE_NO = " + OracleDBUtil.SqlStr(INStore.Trim()));
                if (!string.IsNullOrEmpty(InStore2))
                {
                    sb.Append(" OR FROM_STORE_NO = " + OracleDBUtil.SqlStr(InStore2.Trim()));
                }
                sb.Append(" ) ");
            }
            else
            {
                if (!string.IsNullOrEmpty(InStore2))
                {
                    sb.Append(" AND FROM_STORE_NO = " + OracleDBUtil.SqlStr(InStore2.Trim()));
                }
            }

            if (!string.IsNullOrEmpty(SPRODNO))
            {
                sb.Append(" AND PRODNO >= " + OracleDBUtil.SqlStr(SPRODNO.Trim()));
            }
            if (!string.IsNullOrEmpty(EPRODNO))
            {
                sb.Append(" AND PRODNO <= " + OracleDBUtil.SqlStr(EPRODNO.Trim()));
            }

            sb.Append(" ORDER BY STDATE, STNO");

            DataTable dt = OracleDBUtil.Query_Data(sb.ToString());
            return dt;
        }

        public DataTable RPL063(string SDATE, string EDATE, string SPRODNO)
        {
            StringBuilder sb = new StringBuilder();
            //商品料號    商品名稱    商品類別           維修倉    展示倉(包含拆封倉)    租賃倉
            //PRODNO      PRODNAME    PRODTYPENAME 
            //PRODNO      PRODNAME    PRODTYPENAME 
            sb.Append(" SELECT PRODNO AS 商品料號, ");
            sb.Append(" PRODNAME AS 商品名稱, PRODTYPENAME AS 商品類別, ");
            sb.Append(" 維修倉, 展示倉(包含拆封倉), ");
            sb.Append(" 租賃倉,  ");
            sb.Append(" FROM VW_RPL_EMPTY_STOCKCHK");
            sb.Append(" WHERE 1 = 1");

            if (!string.IsNullOrEmpty(SDATE))
            {
                sb.Append(" AND TRUNC(STKCHKDATE) >= " + OracleDBUtil.DateStr(SDATE.Trim()));
            }
            if (!string.IsNullOrEmpty(EDATE))
            {
                sb.Append(" AND TRUNC(STKCHKDATE) <= " + OracleDBUtil.DateStr(EDATE.Trim()));
            }
            if (!string.IsNullOrEmpty(SPRODNO))
            {
                sb.Append(" AND PRODTYPENAME = " + OracleDBUtil.SqlStr(SPRODNO.Trim()));
            }

            DataTable dt = OracleDBUtil.Query_Data(sb.ToString());
            return dt;
        }

        public DataTable RPL064(string TYPE, string ORDERNO, string SDate, string EDate, string SPRODNO, string EPRODNO, string STORENO)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(" SELECT to_char(to_date(RTNDATE,'YYYY/MM/DD'),'YYYY/MM/DD') RTNDATE, ");
            sb.Append(" RTNNO, PRODNO, ");
            sb.Append(" PRODNAME, UNIT, ");
            sb.Append(" OPENQTY, UNOPENQTY, ");
            sb.Append(" RTNQTY  ");
            sb.Append(" FROM VW_RPL_RTNM ");
            sb.Append(" WHERE 1 = 1");

            if (!string.IsNullOrEmpty(TYPE))
            {
                sb.Append(" AND UNIT = " + OracleDBUtil.SqlStr(TYPE.Trim()));
            }
            if (!string.IsNullOrEmpty(ORDERNO))
            {
                sb.Append(" AND RTNNO = " + OracleDBUtil.SqlStr(ORDERNO.Trim()));
            }
            if (!string.IsNullOrEmpty(SDate))
            {
                sb.Append(" AND TRUNC(RTNDATE) >= to_char(TO_DATE(" + OracleDBUtil.SqlStr(SDate.Trim()) + ",'YYYY/MM/DD'),'YYYYMMDD')");
            }
            if (!string.IsNullOrEmpty(EDate))
            {
                sb.Append(" AND TRUNC(RTNDATE) <= to_char(TO_DATE(" + OracleDBUtil.SqlStr(EDate.Trim()) + ",'YYYY/MM/DD'),'YYYYMMDD')");
            }
            if (!string.IsNullOrEmpty(SPRODNO))
            {
                sb.Append(" AND PRODNO >= " + OracleDBUtil.SqlStr(SPRODNO.Trim()));
            }
            if (!string.IsNullOrEmpty(EPRODNO))
            {
                sb.Append(" AND PRODNO <= " + OracleDBUtil.SqlStr(EPRODNO.Trim()));
            }
            if (!string.IsNullOrEmpty(STORENO))
            {
                sb.Append(" AND STORE_NO = " + OracleDBUtil.SqlStr(STORENO.Trim()));
            }
            sb.Append(" ORDER BY RTNDATE,RTNNO, PRODNO ");

            DataTable dt = OracleDBUtil.Query_Data(sb.ToString());
            return dt;
        }

        public DataTable RPL065(string SDate, string EDate, string InvoiceNo, string ProdNo, string SALE_NO, string EmployeeNo)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT ORIGINAL_ID FROM VW_RPL_RETURN_REPORT WHERE 1=1");

            if (!string.IsNullOrEmpty(SDate))
            {
                sb.Append(" AND TRUNC(TRADE_DATE) >= " + OracleDBUtil.DateStr(SDate.Trim()));
            }
            if (!string.IsNullOrEmpty(EDate))
            {
                sb.Append(" AND TRUNC(TRADE_DATE) <= " + OracleDBUtil.DateStr(EDate.Trim()));
            }
            if (!string.IsNullOrEmpty(InvoiceNo))
            {
                sb.Append(" AND INVOICE_NO = " + OracleDBUtil.SqlStr(InvoiceNo.Trim()));
            }
            if (!string.IsNullOrEmpty(ProdNo))
            {
                sb.Append(" AND PRODNO = " + OracleDBUtil.SqlStr(ProdNo.Trim()));
            }
            if (!string.IsNullOrEmpty(SALE_NO))
            {
                sb.Append(" AND SALE_NO = " + OracleDBUtil.SqlStr(SALE_NO.Trim()));
            }
            //EmployeeNo
            if (!string.IsNullOrEmpty(EmployeeNo))
            {
                sb.Append(" AND SALE_PERSON IN (" + OracleDBUtil.MultiSqlStr(EmployeeNo.Trim(), ","));
                sb.Append(")");
            }
            StringBuilder sb1 = new StringBuilder();
            sb1.Append("SELECT * FROM VW_RPL_RETURN_REPORT1 WHERE ORIGINAL_ID IN (" + sb.ToString().Trim());
            sb1.Append(")");

            DataTable dt = OracleDBUtil.Query_Data(sb1.ToString());
            return dt;
        }

        //public DataTable RPL070(string SDATE, string EDATE, string PromotionCode, string ProdCateory, string PRODNO, string SAmount, string EAmount)
        //{
        //    StringBuilder sb = new StringBuilder();
        //    //退倉日期    退倉單號            商品料號        商品名稱    單位    已拆封數量    未拆封數量    退倉數量    
        //    //RTNDATE     RTNNO                 PRODNO           PRODNAME   UNIT  OPENQTY        UNOPENQTY   RTNQTY
        //    //VW_RPL_PORMO_PROD_ANALYSIS

        //    sb.Append(" SELECT TO_CHAR(RTNDATE,'YYYY/MM/DD') AS 退倉日期, ");
        //    sb.Append(" RTNNO AS 退倉單號, PRODNO AS 商品料號, ");
        //    sb.Append(" PRODNAME AS 商品名稱, UNIT AS 單位, ");
        //    sb.Append(" OPENQTY AS 已拆封數量, UNOPENQTY AS 未拆封數量, ");
        //    sb.Append(" RTNQTY AS 退倉數量 ");
        //    sb.Append(" FROM VW_RPL_RTNM ");
        //    sb.Append(" WHERE 1 = 1");

        //    DataTable dt = OracleDBUtil.Query_Data(sb.ToString());
        //    return dt;
        //}

        public DataTable RPL062(string STNO)
        {
            StringBuilder sb = new StringBuilder();
            //商品料號    商品名稱    數量          IMEI ST2121-101111001
            //PRODNO       PRODNAME  PROD_QTY  IMEI
            //移撥單號：STNO 調出門市：FROM_STORENAME 調出日期：STDATE 撥入門市：TO_STORENAME
            sb.Append(" SELECT PRODNO AS 商品料號, ");
            sb.Append(" PRODNAME AS 商品名稱, PROD_QTY AS 數量, ");
            sb.Append(" IMEI AS IMEI, STNO AS 移撥單號, ");
            sb.Append(" FROM_STORENAME AS 調出門市, STDATE AS 調出日期, ");
            sb.Append(" TO_STORENAME AS 撥入門市 ");
            sb.Append(" FROM VW_RPL_RTNM ");
            sb.Append(" WHERE 1 = 1");

            if (!string.IsNullOrEmpty(STNO))
            {
                sb.Append(" AND STNO = " + OracleDBUtil.SqlStr(STNO.Trim()));
            }

            DataTable dt = OracleDBUtil.Query_Data(sb.ToString());
            return dt;
        }

        #region ---  蔡坤霖

        /*Author : 蔡坤霖
          Date : 2011 / 02 / 17
          Description: RPL014 發票明細表
         */
        /// <summary>
        /// RPL014 發票明細表
        /// </summary>
        /// <param name="STORE_NO_S">門市編號_起</param>
        /// <param name="STORE_NO_E">門市編號_訖</param>
        /// <param name="INVOICE_DATE_S">發票號碼_起</param>
        /// <param name="INVOICE_DATE_E">發票號碼_訖</param>
        /// <param name="TAX_TYPE">課稅別</param>
        /// <returns></returns>
        public DataTable RPL014(string STORE_NO_S, string STORE_NO_E, string INVOICE_DATE_S, string INVOICE_DATE_E, string TAX_TYPE, string INVOICE_S, string INVOICE_E, string AMOUNT_S, string AMOUNT_E, string EMPLOYEE_S, string EMPLOYEE_E)
        {
            OracleConnection objConn = null;

            try
            {
                objConn = OracleDBUtil.GetConnection();
                System.Text.StringBuilder sb = new System.Text.StringBuilder();

                #region -SELECT-
                sb.AppendFormat("/*發票明細表*/ ");
                sb.AppendFormat("SELECT T.STORE_NO       門市編號   ");
                sb.AppendFormat("      ,T.STORE_NAME     門市名稱   ");
                sb.AppendFormat("      ,TO_CHAR(T.INVOICE_DATE,'YYYY/MM/DD')   發票日期 ");
                sb.AppendFormat("      ,T.INVOICE_NO     發票號碼   ");
                sb.AppendFormat("      ,T.INVOICE_TYPE   格式       ");
                sb.AppendFormat("      ,T.MACHINE_ID     機台       ");
                sb.AppendFormat("      ,T.TAX_TYPE       課稅別     ");
                sb.AppendFormat("      ,T.UNI_NO         客戶統編   ");
                sb.AppendFormat("      ,T.SALE_AMOUNT    銷售額     ");
                sb.AppendFormat("      ,T.TAX            稅額       ");
                sb.AppendFormat("      ,T.INVOICE_AMOUNT 發票總金額 ");
                sb.AppendFormat("      ,T.SALE_PERSON || T.EMPNAME             員工編號 ");
                sb.AppendFormat("  FROM VW_RPL_STORE_INVOICE_DETAIL T ");
                sb.AppendFormat(" WHERE 1 = 1");
                #endregion -SELECT-

                #region -WHERE-
                #region 門市編號
                if (!string.IsNullOrEmpty(STORE_NO_S))
                {
                    sb.Append(" AND STORE_NO >= ");
                    sb.Append(Advtek.Utility.OracleDBUtil.SqlStr(STORE_NO_S.Trim()));
                }
                if (!string.IsNullOrEmpty(STORE_NO_E))
                {
                    sb.Append(" AND STORE_NO <= ");
                    sb.Append(Advtek.Utility.OracleDBUtil.SqlStr(STORE_NO_E.Trim()));
                }
                #endregion 門市編號
                #region 發票日期
                if (!string.IsNullOrEmpty(INVOICE_DATE_S))
                {
                    sb.Append(" AND TRUNC(INVOICE_DATE) >= ");
                    sb.Append(Advtek.Utility.OracleDBUtil.DateStr(INVOICE_DATE_S.Trim()));
                }
                if (!string.IsNullOrEmpty(INVOICE_DATE_E))
                {
                    sb.Append(" AND TRUNC(INVOICE_DATE) <= ");
                    sb.Append(Advtek.Utility.OracleDBUtil.DateStr(INVOICE_DATE_E.Trim()));
                }
                #endregion 發票日期
                #region 課稅別
                if (!string.IsNullOrEmpty(TAX_TYPE) && TAX_TYPE.ToUpper() != "ALL")
                {
                    sb.Append(" AND TAX_TYPE = ");
                    sb.Append(Advtek.Utility.OracleDBUtil.SqlStr(TAX_TYPE.Trim()));
                }
                #endregion 課稅別
                #region 發票號碼
                if (!string.IsNullOrEmpty(INVOICE_S))
                {
                    sb.Append(" AND INVOICE_NO >= ");
                    sb.Append(Advtek.Utility.OracleDBUtil.SqlStr(INVOICE_S.Trim()));
                }
                if (!string.IsNullOrEmpty(INVOICE_E))
                {
                    sb.Append(" AND INVOICE_NO <= ");
                    sb.Append(Advtek.Utility.OracleDBUtil.SqlStr(INVOICE_E.Trim()));
                }
                #endregion
                #region 發票金額
                if (!string.IsNullOrEmpty(AMOUNT_S))
                {
                    sb.Append(" AND INVOICE_AMOUNT >= ");
                    sb.Append(Advtek.Utility.OracleDBUtil.SqlStr(AMOUNT_S.Trim()));
                }
                if (!string.IsNullOrEmpty(AMOUNT_E))
                {
                    sb.Append(" AND INVOICE_AMOUNT <= ");
                    sb.Append(Advtek.Utility.OracleDBUtil.SqlStr(AMOUNT_E.Trim()));
                }
                #endregion
                #region 員工編號
                if (!string.IsNullOrEmpty(EMPLOYEE_S))
                {
                    sb.Append(" AND SALE_PERSON >= ");
                    sb.Append(Advtek.Utility.OracleDBUtil.SqlStr(EMPLOYEE_S.Trim()));
                }
                if (!string.IsNullOrEmpty(EMPLOYEE_E))
                {
                    sb.Append(" AND SALE_PERSON <= ");
                    sb.Append(Advtek.Utility.OracleDBUtil.SqlStr(EMPLOYEE_E.Trim()));
                }
                #endregion
                #endregion

                #region -ORDER BY-
                sb.AppendFormat(" ORDER BY T.BOOK_INV_SEQNO ,T.INVOICE_NO ");
                #endregion

                DataTable dt = Advtek.Utility.OracleDBUtil.GetDataSet(objConn, sb.ToString()).Tables[0];

                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (objConn.State == ConnectionState.Open) objConn.Close();
                objConn.Dispose();
                OracleConnection.ClearAllPools();
            }
        }

        /*Author : 蔡坤霖
          Date : 2011 / 02 / 18
          Description: RPL042 POS門市銷量表-3C
         */
        /// <summary>
        /// RPL042 POS門市銷量表-3C
        /// </summary>
        /// <param name="SDATE">銷售日期_起</param>
        /// <param name="EDATE">銷售日期_訖</param>
        /// <param name="SSTORE">門市編號_起</param>
        /// <param name="ESTORE">門市編號_訖</param>
        /// <returns></returns>
        public DataTable RPL042(string SDATE, string EDATE, string SSTORE, string ESTORE)
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendFormat("SELECT T.STORE_NO 客戶代號, ");
            sb.AppendFormat("       T.STORE_NAME 客戶名稱, ");
            sb.AppendFormat("       T.PRODNO 手機料號, ");
            sb.AppendFormat("       T.PRODNAME 手機名稱, ");
            sb.AppendFormat("       T.QUANTITY 單機銷貨數, ");
            sb.AppendFormat("       T.Avg_PRICE 單機均價, ");
            sb.AppendFormat("       T.D_QUANTITY 促銷銷貨數, ");
            sb.AppendFormat("       T.D_Avg_PRICE 促銷均價, ");
            sb.AppendFormat("       T.ATTRIBUTE2 ");
            sb.AppendFormat("  FROM VW_RPL_POS_MOBILE_3C T ");
            sb.AppendFormat(" WHERE 1 = 1 ");

            if (!string.IsNullOrEmpty(SSTORE))
            {
                sb.Append(" AND STORE_NO >= " + OracleDBUtil.SqlStr(SSTORE.Trim()));
            }
            if (!string.IsNullOrEmpty(ESTORE))
            {
                sb.Append(" AND STORE_NO <= " + OracleDBUtil.SqlStr(ESTORE.Trim()));
            }
            if (!string.IsNullOrEmpty(SDATE))
            {
                sb.Append(" AND TRUNC(TRADE_DATE) >= " + OracleDBUtil.DateStr(SDATE.Trim()));
            }
            if (!string.IsNullOrEmpty(EDATE))
            {
                sb.Append(" AND TRUNC(TRADE_DATE) <= " + OracleDBUtil.DateStr(EDATE.Trim()));
            }

            DataTable dt = OracleDBUtil.Query_Data(sb.ToString());
            return dt;
        }

        /*Author : 蔡坤霖
          Date : 2011 / 02 / 18
          Description: RPL043 POS門市銷量表-CPA
         */
        /// <summary>
        /// RPL043 POS門市銷量表-CPA
        /// </summary>
        /// <param name="SDATE">銷售日期_起</param>
        /// <param name="EDATE">銷售日期_訖</param>
        /// <param name="SSTORE">門市編號_起</param>
        /// <param name="ESTORE">門市編號_訖</param>
        /// <returns></returns>
        public DataTable RPL043(string SDATE, string EDATE, string SSTORE, string ESTORE)
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendFormat("SELECT T.STORE_NO 客戶代號, ");
            sb.AppendFormat("       T.STORE_NAME 客戶名稱, ");
            sb.AppendFormat("       T.PRODNO 手機料號, ");
            sb.AppendFormat("       T.PRODNAME 手機名稱, ");
            sb.AppendFormat("       T.QUANTITY 單機銷貨數, ");
            sb.AppendFormat("       T.Avg_PRICE 單機均價, ");
            sb.AppendFormat("       T.D_QUANTITY 促銷銷貨數, ");
            sb.AppendFormat("       T.D_Avg_PRICE 促銷均價, ");
            sb.AppendFormat("       T.ATTRIBUTE2 ");
            sb.AppendFormat("  FROM VW_RPL_POS_MOBILE_CPA T ");
            sb.AppendFormat(" WHERE 1 = 1 ");

            if (!string.IsNullOrEmpty(SSTORE))
            {
                sb.Append(" AND STORE_NO >= " + OracleDBUtil.SqlStr(SSTORE.Trim()));
            }
            if (!string.IsNullOrEmpty(ESTORE))
            {
                sb.Append(" AND STORE_NO <= " + OracleDBUtil.SqlStr(ESTORE.Trim()));
            }
            if (!string.IsNullOrEmpty(SDATE))
            {
                sb.Append(" AND TRUNC(TRADE_DATE) >= " + OracleDBUtil.DateStr(SDATE.Trim()));
            }
            if (!string.IsNullOrEmpty(EDATE))
            {
                sb.Append(" AND TRUNC(TRADE_DATE) <= " + OracleDBUtil.DateStr(EDATE.Trim()));
            }

            DataTable dt = OracleDBUtil.Query_Data(sb.ToString());
            return dt;
        }

        /*Author : 蔡坤霖
          Date : 2011 / 02 / 10
          Description: RPL055 單品銷售報表
         */
        /// <summary>
        /// RPL055 單品銷售報表
        /// </summary>
        /// <param name="SSTORE">門市編號_起</param>
        /// <param name="ESTORE">門市編號_訖</param>
        /// <param name="SPRODTYPENO">商品類別_起</param>
        /// <param name="EPRODTYPENO">商品類別_訖</param>
        /// <param name="SPRODNO">商品料號_起</param>
        /// <param name="EPRODNO">商品料號_訖</param>
        /// <param name="SDATE">交易日期_起</param>
        /// <param name="EDATE">交易日期_訖</param>
        /// <param name="SPRICE">單品金額_起</param>
        /// <param name="EPRICE">單品金額_訖</param>
        /// <param name="SSAMOUNT">銷售金額_起</param>
        /// <param name="ESAMOUNT">銷售金額_訖</param>
        /// <returns></returns>
        public DataTable RPL055(string SSTORE, string ESTORE, string SPRODTYPENO, string EPRODTYPENO, string SPRODNO, string EPRODNO, string SDATE, string EDATE, string SPRICE, string EPRICE, string SSAMOUNT, string ESAMOUNT)
        {
            StringBuilder sb = new StringBuilder();

            #region -SELECT-
            sb.AppendFormat("SELECT T.STORE_NO     AS 門市編號, ");
            sb.AppendFormat("       T.STORENAME    AS 門市名稱, ");
            sb.AppendFormat("       T.SALE_NO      AS 交易序號, ");
            sb.AppendFormat("       TO_CHAR(T.TRADE_DATE,'YYYY/MM/DD')   AS 交易日期, ");
            sb.AppendFormat("       T.INVOICE_NO   AS 發票號碼, ");
            sb.AppendFormat("       T.UNI_NO       AS 統一編號, ");
            sb.AppendFormat("       T.PRODTYPENAME   AS 商品類別, ");
            sb.AppendFormat("       T.PRODNO       AS 商品料號, ");
            sb.AppendFormat("       T.PRODNAME     AS 商品名稱, ");
            sb.AppendFormat("       T.PRICE        AS 單品金額, ");
            sb.AppendFormat("       T.TOTAL_AMOUNT AS 銷售金額, ");
            sb.AppendFormat("       T.REMARK       AS 備註, ");
            sb.AppendFormat("       T.SALE_STATUS  AS 狀態, ");
            sb.AppendFormat("       T.SALE_PERSON  AS 處理人員 ");
            sb.AppendFormat("  FROM VW_RPL_PRODUCT_SALE_STATIC T ");
            sb.Append(" WHERE 1 = 1  ");
            #endregion

            #region -WHERE-
            #region 門市編號
            if (!string.IsNullOrEmpty(SSTORE))
            {
                sb.Append(" AND STORE_NO >= " + OracleDBUtil.SqlStr(SSTORE.Trim()));
            }
            if (!string.IsNullOrEmpty(ESTORE))
            {
                sb.Append(" AND STORE_NO <= " + OracleDBUtil.SqlStr(ESTORE.Trim()));
            }
            #endregion
            #region 商品類別
            if (!string.IsNullOrEmpty(SPRODTYPENO))
            {
                sb.Append(" AND PRODTYPENO >= " + OracleDBUtil.SqlStr(SPRODTYPENO.Trim()));
            }
            if (!string.IsNullOrEmpty(EPRODTYPENO))
            {
                sb.Append(" AND PRODTYPENO <= " + OracleDBUtil.SqlStr(EPRODTYPENO.Trim()));
            }
            #endregion
            #region 商品料號
            if (!string.IsNullOrEmpty(SPRODNO))
            {
                sb.Append(" AND PRODNO >= " + OracleDBUtil.SqlStr(SPRODNO.Trim()));
            }
            if (!string.IsNullOrEmpty(EPRODNO))
            {
                sb.Append(" AND PRODNO <= " + OracleDBUtil.SqlStr(EPRODNO.Trim()));
            }
            #endregion
            #region 交易日期
            if (!string.IsNullOrEmpty(SDATE))
            {
                sb.Append(" AND TRUNC(TRADE_DATE) >= " + OracleDBUtil.DateStr(SDATE.Trim()));
            }
            if (!string.IsNullOrEmpty(EDATE))
            {
                sb.Append(" AND TRUNC(TRADE_DATE) <= " + OracleDBUtil.DateStr(EDATE.Trim()));
            }
            #endregion
            #region 單品金額
            if (!string.IsNullOrEmpty(SPRICE))
            {
                sb.Append(" AND PRICE >= " + OracleDBUtil.NumberStr(SPRICE.Trim()));
            }
            if (!string.IsNullOrEmpty(EPRICE))
            {
                sb.Append(" AND PRICE <= " + OracleDBUtil.NumberStr(EPRICE.Trim()));
            }
            #endregion
            #region 銷售金額
            if (!string.IsNullOrEmpty(SSAMOUNT))
            {
                sb.Append(" AND TOTAL_AMOUNT >= " + OracleDBUtil.NumberStr(SSAMOUNT.Trim()));
            }
            if (!string.IsNullOrEmpty(ESAMOUNT))
            {
                sb.Append(" AND TOTAL_AMOUNT <= " + OracleDBUtil.NumberStr(ESAMOUNT.Trim()));
            }
            #endregion
            #endregion

            #region -ORDER BY-
            sb.AppendFormat(" ORDER BY T.STORE_NO ,TO_CHAR(T.TRADE_DATE,'YYYY/MM/DD') ,T.SALE_NO ,T.PRODNO ");
            #endregion

            DataTable dt = OracleDBUtil.Query_Data(sb.ToString());
            return dt;
        }

        #endregion ---  蔡坤霖
    }
}
