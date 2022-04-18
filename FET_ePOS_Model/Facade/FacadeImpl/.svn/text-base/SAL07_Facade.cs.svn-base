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
    public class SAL07_Facade
    {
        public DataTable Query_MM(string PROMO_NO, string PROMO_NAME,
            string BDATE_S, string BDATE_E,
            string PriceS, string PriceE, string PROD_NO,
            string PRODNAME1, string PRODNAME2, string PRODNAME3,
            bool IsExport, bool MoreQTY, string UserRole, string UserStore, bool IsEffective)
        {
            StringBuilder sb = new StringBuilder();

            if (IsExport)
            {
                sb.AppendLine(@"SELECT PROMO_NO as 促銷代碼
                                     , PROMO_NAME as 促銷名稱
                                     , to_char(B_DATE,'yyyy/mm/dd') as 開始日期
                                     , to_char(E_DATE,'yyyy/mm/dd') as 結束日期
                                     , PROD_NO as 商品料號
                                     , PRODNAME as 商品名稱
                                     , PROMO_PROD_GROUP as 商品群組
                                     , AMOUNT as 促銷價
                                     , ONHANDQTY as 庫存量 
                                 ");
            }
            else
            {
                sb.AppendLine(@"SELECT distinct UUID
                                        , PROMO_NO
                                        , PROMO_NAME
                                        , to_char(B_DATE,'yyyy/mm/dd') B_DATE
                                        , to_char(E_DATE,'yyyy/mm/dd') E_DATE 
                            ");

                //  sb.AppendLine("B_DATE, E_DATE, PROD_ADDED_DATE, MM_TYPE, ");
                //  sb.AppendLine("PROD_CONFIG_TYPE, PROMO_SUBSIDY, NEED_TO_PRICING, ");
                //  sb.AppendLine("REMARK, MODI_USER, EMPNAME, MODI_DTM, ONHANDQTY ");
            }

            sb.AppendLine("FROM VW_MM ");
            sb.AppendLine("WHERE 1=1  ");
            if (UserRole != "Admin" && UserRole != "HQ")  //非總部人員，只能看到自己門市的撥入商品
            {
                sb.AppendLine(" AND STORE_NO = " + OracleDBUtil.SqlStr(UserStore));
            }
            if (!string.IsNullOrEmpty(PriceS.Trim()))
            {
                sb.AppendLine(" AND AMOUNT >= " + OracleDBUtil.SqlStr(PriceS.Trim()));
            }

            if (!string.IsNullOrEmpty(PriceE.Trim()))
            {
                sb.AppendLine(" AND AMOUNT <= " + OracleDBUtil.SqlStr(PriceE.Trim()));
            }

            if (MoreQTY)
            {
                sb.AppendLine(" AND ONHANDQTY > 0 ");
            }

            if (!string.IsNullOrEmpty(PROMO_NO.Trim()))
            {
                sb.AppendLine(" AND LOWER(PROMO_NO) LIKE " + OracleDBUtil.ToLowerLikeStr(PROMO_NO.Trim()));
            }

            if (!string.IsNullOrEmpty(PROMO_NAME))
            {
                sb.AppendLine(" AND LOWER(PROMO_NAME) LIKE " + OracleDBUtil.ToLowerLikeStr(PROMO_NAME.Trim()));
            }

            if (!string.IsNullOrEmpty(BDATE_S.Trim()))
            {
                if (!string.IsNullOrEmpty(BDATE_E.Trim()))
                {
                    sb.AppendLine(" AND B_DATE <= " + OracleDBUtil.DateStr(BDATE_S.Trim()));
                    sb.AppendLine(" AND E_DATE >= " + OracleDBUtil.DateStr(BDATE_E.Trim()));
                    //sb.AppendLine(" AND nvl(to_char(E_DATE,'YYYY/MM/DD'),'9999/12/31') >= " + OracleDBUtil.DateStr(BDATE_E.Trim()));
                }
                else
                {
                    //sb.AppendLine(" AND nvl(to_char(E_DATE,'YYYY/MM/DD'),'9999/12/31') <= " + OracleDBUtil.DateStr(BDATE_S.Trim()));
                    sb.AppendLine(" AND E_DATE <= " + OracleDBUtil.DateStr(BDATE_S.Trim()));
                }
            }
            if (IsExport)
            {
                if (!string.IsNullOrEmpty(PROD_NO.Trim()))
                {
                    sb.AppendLine(" AND PROMO_NO IN (select PROMO_NO from VW_MM where PROD_NO LIKE " + OracleDBUtil.LikeStr(PROD_NO.Trim()) + ")");
                }
                if (!string.IsNullOrEmpty(PRODNAME1.Trim()))
                {
                    sb.AppendLine(" AND PROMO_NO IN (select PROMO_NO from VW_MM where LOWER(PRODNAME) LIKE " + OracleDBUtil.ToLowerLikeStr(PRODNAME1.Trim()) + ")");
                }

                if (!string.IsNullOrEmpty(PRODNAME2.Trim()))
                {
                    sb.AppendLine(" AND PROMO_NO IN (select PROMO_NO from VW_MM where LOWER(PRODNAME) LIKE " + OracleDBUtil.ToLowerLikeStr(PRODNAME2.Trim()) + ")");
                }

                if (!string.IsNullOrEmpty(PRODNAME3.Trim()))
                {
                    sb.AppendLine(" AND PROMO_NO IN (select PROMO_NO from VW_MM where LOWER(PRODNAME) LIKE " + OracleDBUtil.ToLowerLikeStr(PRODNAME3.Trim()) + ")");
                }
            }
            else
            {
                if (!string.IsNullOrEmpty(PROD_NO.Trim()))
                {
                    sb.AppendLine(" AND PROD_NO LIKE " + OracleDBUtil.LikeStr(PROD_NO.Trim()));
                }
                if (!string.IsNullOrEmpty(PRODNAME1.Trim()))
                {
                    sb.AppendLine(" AND LOWER(PRODNAME) LIKE " + OracleDBUtil.ToLowerLikeStr(PRODNAME1.Trim()));
                }

                if (!string.IsNullOrEmpty(PRODNAME2.Trim()))
                {
                    sb.AppendLine(" AND LOWER(PRODNAME) LIKE " + OracleDBUtil.ToLowerLikeStr(PRODNAME2.Trim()));
                }

                if (!string.IsNullOrEmpty(PRODNAME3.Trim()))
                {
                    sb.AppendLine(" AND LOWER(PRODNAME) LIKE " + OracleDBUtil.ToLowerLikeStr(PRODNAME3.Trim()));
                }
            }
            //sb.AppendLine(" AND ROWNUM <= 3000 ");


            if (IsEffective)
            {
                sb.AppendLine(" AND ( TRUNC(SYSDATE) >= TRUNC(NVL(SS_DATE, SYSDATE)) AND TRUNC(SYSDATE) <= TRUNC(NVL(EE_DATE, SYSDATE)) AND DEL_FLAG='N')");
            }

            sb.AppendLine(" order by PROMO_NO,B_DATE");

            DataTable dt = OracleDBUtil.Query_Data(sb.ToString());
            return dt;
        }

        /// <summary>
        /// 促銷商品明細 查詢
        /// </summary>
        /// <param name="key"></param>
        /// <param name="MoreQTY"></param>
        /// <param name="UserRole"></param>
        /// <param name="UserStore"></param>
        /// <param name="ProdNo"></param>商品料號
        /// <param name="TradeMark"></param>商品廠牌
        /// <param name="ProductName"></param>商品型號
        /// <returns></returns>
        public DataTable Query_Detail(string key, 
                                                   bool MoreQTY,
                                                   string UserRole,
                                                   string UserStore,
                                                   string ProdNo,
                                                   string TradeMark,
                                                   string ProductName)
        {

            StringBuilder sb = new StringBuilder();
            StringBuilder sTemp1 = new StringBuilder();//組條件
            StringBuilder sTemp2 = new StringBuilder();//組條件
            sTemp1.Append(" ");
            sTemp2.Append(" ");

            sb.AppendLine(" SELECT * ");
            sb.AppendLine(" FROM VW_MM ");
            sb.AppendLine(" WHERE UUID=" + OracleDBUtil.SqlStr(key.Trim()));
            sb.AppendLine(" AND NVL(PROD_NO, ' ')<>' ' ");
            if (MoreQTY) sb.Append(" AND ONHANDQTY > 0 ");
            if (UserRole != "Admin" && UserRole != "HQ")  //非總部人員，只能看到自己門市的撥入商品
            {
                sb.Append(" AND STORE_NO = " + OracleDBUtil.SqlStr(UserStore));
            }

            //商品料號
            if (!string.IsNullOrEmpty(ProdNo.Trim()))
            {
                sb.AppendLine(" AND  upper(PROD_NO) like upper( " + OracleDBUtil.LikeStr(ProdNo.Trim()) + " ) ");
            }

            int iCount = 0;
            // and( PROMO_NAME LIKE  '%IPHONE%' OR  PROMO_NAME LIKE '%NOKIA%' )
            //商品廠牌   
            if (!string.IsNullOrEmpty(TradeMark.Trim()))
            {
                sTemp1.AppendLine(" upper( PRODNAME ) like upper( " + OracleDBUtil.LikeStr(TradeMark.Trim()) + " ) ");
                iCount++;
            }

            //商品型號
            if (!string.IsNullOrEmpty(ProductName.Trim()))
            {
                sTemp2.AppendLine("  upper( PRODNAME) like upper( " + OracleDBUtil.LikeStr(ProductName.Trim()) + " ) ");
                iCount++;
            }

            if (iCount > 0)
            {
                if(iCount ==2)
                    sb.Append(" AND (" + sTemp1.ToString() + " OR " + sTemp2.ToString() + " ) ");
                else
                    sb.Append(" AND (" + sTemp1.ToString() + " " + sTemp2.ToString() + " ) ");
            }

            sb.AppendLine(" order by PROMO_NO,PROMO_PROD_GROUP ");

            DataTable dt = OracleDBUtil.Query_Data(sb.ToString());
            return dt;
        }

        public DataTable Query_Discount(string PROMO_NO, string M_TYPE,
            string ARPB, string STORE_NO,
            string MSISDN, string PROD_NO,
            string PRODNAME)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("SELECT DISTINCT DISCOUNT_MASTER_ID, DISCOUNT_CODE, DISCOUNT_NAME, ");
            sb.AppendLine("DISCOUNT_MONEY,FN_SAL07_QUERY_DISCOUNT_GIFT(DISCOUNT_MASTER_ID) AS GIFT ");
            sb.AppendLine("FROM VW_SAL07 ");
            sb.AppendLine("WHERE 1=1 ");

            //促銷料號
            if (!string.IsNullOrEmpty(PROMO_NO.Trim()))
            {
                sb.AppendLine(" AND PROMOTION_CODE = " + OracleDBUtil.SqlStr(PROMO_NO.Trim()));
            }

            //類型
            if (!string.IsNullOrEmpty(M_TYPE))
            {
                sb.Append(" AND M_TYPE IN (" + M_TYPE + ") ");
            }

            //商品料號 PRODNO
            if (!string.IsNullOrEmpty(PROD_NO.Trim()))
            {
                sb.AppendLine(" AND PRODNO = " + OracleDBUtil.SqlStr(PROD_NO.Trim()));
            }

            //商店店號
            if (!string.IsNullOrEmpty(STORE_NO.Trim()))
            {
                sb.AppendLine(" AND STORE_NO = " + OracleDBUtil.SqlStr(STORE_NO.Trim()));
            }

            //ARPB
            if (!string.IsNullOrEmpty(ARPB.Trim()))
            {
                sb.AppendLine(" AND " + OracleDBUtil.SqlStr(STORE_NO.Trim()) + " >= ARPB_S");
            }

            //門號MSISDN
            if (!string.IsNullOrEmpty(MSISDN.Trim()))
            {
                sb.AppendLine(" AND MSISDN=" + OracleDBUtil.SqlStr(MSISDN.Trim()));
            }
            sb.Append(" order by DISCOUNT_CODE");

            DataTable dt = OracleDBUtil.Query_Data(sb.ToString());
            return dt;
        }

        /// <summary>
        /// 取得贈品/加價購
        /// </summary>
        /// <param name="DISCOUNT_MASTER_ID"></param>
        /// <returns></returns>
        public string QueryDiscountGIFT(string DISCOUNT_MASTER_ID)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("SELECT FN_SAL07_QUERY_DISCOUNT_GIFT(" + 
                OracleDBUtil.SqlStr(DISCOUNT_MASTER_ID.Trim()) + ")  ");
            sb.AppendLine("FROM dual ");

            DataTable dt = OracleDBUtil.Query_Data(sb.ToString());

            string retVal = "";
            if( dt.Rows.Count > 0)
                retVal = dt.Rows[0].ItemArray[0].ToString();

            return retVal;
        }

        public bool QueryPromotionDiscount(string DISCOUNT_MASTER_ID)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("SELECT  1 ");
            sb.AppendLine("FROM PROMOTION_DISCOUNT ");
            sb.AppendLine(" WHERE DISCOUNT_MASTER_ID = " + OracleDBUtil.SqlStr(DISCOUNT_MASTER_ID.Trim()));

            DataTable dt = OracleDBUtil.Query_Data(sb.ToString());

            bool retVal = false;
            if (dt.Rows.Count > 0)
                retVal = true;//有存在促銷

            return retVal;
        }
    }
}
