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
    public class DIS04_Facade
    {
        public DataTable Query_ProdRelationMethodSet(string PromotionCode, string PromotionName, string ProductType1, string ProductType2, string ProdNo1, string ProdNo2)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(" SELECT  rownum       AS ITEM_NO                                         ");
            sb.Append("        ,A.PROMO_NO   AS PROMO_NO                                        ");
            sb.Append("        ,A.PROMO_NAME AS PROMO_NAME                                      ");
            sb.Append("        ,A.MODI_DTM   AS MODI_DTM                                        ");
            sb.Append("        ,A.MODI_USER  AS MODI_USER                                       ");
            sb.Append("        ,A.MODI_USER_NAME AS MODI_USER_NAME                              ");
            sb.Append("   FROM (                                                                ");
            sb.Append(" SELECT  DISTINCT                                                        ");
            sb.Append("         VW_DIS04.PROMO_NO   AS PROMO_NO                                 ");
            sb.Append("        ,VW_DIS04.PROMO_NAME AS PROMO_NAME                               ");
            sb.Append("        ,TO_CHAR(VW_DIS04.MODI_DTM,'YYYY/MM/DD hh24:mi:ss') AS MODI_DTM  ");
            sb.Append("        ,VW_DIS04.MODI_USER  AS MODI_USER                                ");
            sb.Append("        ,EMP.EMPNAME AS MODI_USER_NAME                                   ");
            sb.Append("   FROM VW_DIS04, EMPLOYEE EMP                                           ");
            sb.Append("  WHERE 1 = 1                                                            ");
            sb.Append("  AND VW_DIS04.MODI_USER = EMP.EMPNO                                     ");

            if ((!string.IsNullOrEmpty(ProdNo1)) && (!string.IsNullOrEmpty(ProdNo2)) && (!string.IsNullOrEmpty(ProductType1)) && (!string.IsNullOrEmpty(ProductType2)))
            {
                sb.AppendFormat(" AND ((   VW_DIS04.PROD_NO >= {0}     ", OracleDBUtil.SqlStr(ProdNo1));
                sb.AppendFormat("      AND VW_DIS04.PROD_NO <= {0}  )  ", OracleDBUtil.SqlStr(ProdNo2));
                sb.AppendFormat("  OR  (   VW_DIS04.CATEGORY >= {0}    ", OracleDBUtil.SqlStr(ProductType1));
                sb.AppendFormat("      AND VW_DIS04.CATEGORY <= {0} )) ", OracleDBUtil.SqlStr(ProductType2));
            }
            else
            {

                if (((!string.IsNullOrEmpty(ProdNo1)) || (!string.IsNullOrEmpty(ProdNo2))) && ((!string.IsNullOrEmpty(ProductType1)) || (!string.IsNullOrEmpty(ProductType2))))
                {
                    if (!string.IsNullOrEmpty(ProdNo1))
                    {
                        sb.AppendFormat(" AND VW_DIS04.PROD_NO >= {0} ", OracleDBUtil.SqlStr(ProdNo1));
                    }
                    if (!string.IsNullOrEmpty(ProdNo2))
                    {
                        sb.AppendFormat(" AND VW_DIS04.PROD_NO <= {0} ", OracleDBUtil.SqlStr(ProdNo2));
                    }
                    //!!!
                    sb.Append("  OR ");
                    if (!string.IsNullOrEmpty(ProductType1))
                    {
                        sb.AppendFormat(" VW_DIS04.CATEGORY >= {0} ", OracleDBUtil.SqlStr(ProductType1));
                    }
                    if ((!string.IsNullOrEmpty(ProductType2)) && (!string.IsNullOrEmpty(ProductType1)))
                    {
                        sb.AppendFormat(" AND VW_DIS04.CATEGORY <= {0} ", OracleDBUtil.SqlStr(ProductType2));
                    }
                    else
                    {
                        sb.AppendFormat("  VW_DIS04.CATEGORY <= {0} ", OracleDBUtil.SqlStr(ProductType2));
                    }
                }
                else
                {
                    if (!string.IsNullOrEmpty(ProdNo1))
                    {
                        sb.AppendFormat(" AND VW_DIS04.PROD_NO >= {0} ", OracleDBUtil.SqlStr(ProdNo1));
                    }
                    if (!string.IsNullOrEmpty(ProdNo2))
                    {
                        sb.AppendFormat(" AND VW_DIS04.PROD_NO <= {0} ", OracleDBUtil.SqlStr(ProdNo2));
                    }
                    if (!string.IsNullOrEmpty(ProductType1))
                    {
                        sb.AppendFormat(" AND VW_DIS04.CATEGORY >= {0} ", OracleDBUtil.SqlStr(ProductType1));
                    }
                    if (!string.IsNullOrEmpty(ProductType2))
                    {
                        sb.AppendFormat(" AND VW_DIS04.CATEGORY <= {0} ", OracleDBUtil.SqlStr(ProductType2));
                    }
                }

            }
            sb.Append(" ORDER BY  PROMO_NO ");
            sb.Append("  ) A                                                            ");
            sb.Append("  WHERE 1 = 1                                                    ");


            if (!string.IsNullOrEmpty(PromotionCode))
            {
                sb.AppendFormat(" AND A.PROMO_NO LIKE {0} ", OracleDBUtil.LikeStr(PromotionCode.Trim()));
            }
            if (!string.IsNullOrEmpty(PromotionName))
            {
                sb.AppendFormat(" AND A.PROMO_NAME LIKE {0} ", OracleDBUtil.LikeStr(PromotionName.Trim()));
            }


            sb.Append("     ORDER BY ITEM_NO ");

            DataTable dt = OracleDBUtil.Query_Data(sb.ToString());
            return dt;
        }

        public DataTable Query_GroupMethodSet(string PROMO_NO, string PROMO_PROD_GROUP)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(" SELECT                          ");
            sb.Append("         rownum   as 項次        ");
            //sb.Append("        ,nvl(group" + PROMO_PROD_GROUP.Trim() + ".PROD_NO,group" + PROMO_PROD_GROUP.Trim() + ".category)  as 商品料號    ");
            //sb.Append("        ,(case when group" + PROMO_PROD_GROUP.Trim() + ".PROD_NO is null then prodc.cate_name else prod.PRODNAME end) as 商品名稱    ");
            sb.Append("        ,nvl(group" + PROMO_PROD_GROUP.Trim() + ".PROD_NO,'')    as 商品料號    ");
            sb.Append("        ,nvl(prod.PRODNAME,'')   as 商品名稱    ");
            sb.Append("        ,nvl(group" + PROMO_PROD_GROUP.Trim() + ".category,'')   as 商品類別    ");
            sb.Append("        ,nvl(prodc.cate_name,'') as 類別名稱    ");
            sb.Append("   FROM  prod_relation_group_" + PROMO_PROD_GROUP.Trim() + " group" + PROMO_PROD_GROUP.Trim() + " ,product prod ,product_category prodc ");
            sb.Append("  WHERE 1 = 1                    ");
            sb.Append("    AND group" + PROMO_PROD_GROUP.Trim() + ".prod_no = prod.prodno(+) ");
            sb.Append("    AND group" + PROMO_PROD_GROUP.Trim() + ".category = prodc.cate_no(+) ");


            if (!string.IsNullOrEmpty(PROMO_NO))
            {
                sb.Append(" AND group" + PROMO_PROD_GROUP.Trim() + ".PROMO_NO = " + OracleDBUtil.SqlStr(PROMO_NO.Trim()));
            }

            DataTable dt = OracleDBUtil.Query_Data(sb.ToString());
            return dt;

        }

    }
}