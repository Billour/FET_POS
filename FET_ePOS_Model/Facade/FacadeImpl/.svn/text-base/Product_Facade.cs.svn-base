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
   public class Product_Facade
   {
       /// <summary>
       /// 共用條件
       /// 一、 為【遠傳】Product, 
       /// 二、 在【有效期限】內, 
       /// 三、【非被刪除項目】
       /// </summary>
       private string Conditions = " AND T1.COMPANYCODE = '01' AND TRUNC(SYSDATE) >= TRUNC(NVL(T1.S_DATE, SYSDATE)) AND TRUNC(SYSDATE) <= TRUNC(NVL(T1.E_DATE, SYSDATE)) AND T1.DEL_FLAG='N'";

       /// <summary>
       /// 取得商品資料(要篩選出有庫存的料號)
       /// </summary>
       /// <param name="sProductNo">商品料號</param>
       /// <param name="sProductName">商品名稱</param>
       /// <param name="sProdctTypeNo">商品料號編號</param>
       /// <param name="sStoreNo">門市編號</param>
       /// <param name="sStockID">倉別UUID</param>
       /// <returns></returns>
       public DataTable Query_Product(string sProductNo, string sProductName, string sProdctTypeNo, string sStoreNo, string sStockID, string HQ)
       {
           StringBuilder sb = new StringBuilder();
           sb.AppendLine(@"SELECT DISTINCT PRODNO
                                        , PRODNAME
                                        , PRODTYPENO
                                        , PRODTYPENAME 
                            FROM VW_PRODUCT_SELECT 
                            WHERE 1 = 1 
                            AND PRODNO not in (select PRODNO from NOT_IN_SALE_PROD where STATUS = 0)
                        ");

           if (sProductNo.Length > 8)
           {
               if (!string.IsNullOrEmpty(sStoreNo) && sStoreNo != HQ)
               {
                   sb.AppendLine(" AND STORE_NO = " + OracleDBUtil.SqlStr(sStoreNo));
               }

               if (!string.IsNullOrEmpty(sStockID))
               {
                   sb.AppendLine(" AND STOCK_ID = " + OracleDBUtil.SqlStr(sStockID));
               }
           }

           if (!string.IsNullOrEmpty(sProductNo))
           {
               sb.AppendLine(" AND PRODNO LIKE " + OracleDBUtil.LastLikeStr(sProductNo));
           }

           if (!string.IsNullOrEmpty(sProductName))
           {
               sb.AppendLine(" AND LOWER(PRODNAME) LIKE " + OracleDBUtil.ToLowerLikeStr(sProductName));
           }

           if (!string.IsNullOrEmpty(sProdctTypeNo))
           {
               sb.AppendLine(" AND PRODTYPENO =  " + OracleDBUtil.SqlStr(sProdctTypeNo));
           }

           sb.AppendLine(" AND rownum <= 500 Order by PRODNO ");

           DataTable dt = OracleDBUtil.Query_Data(sb.ToString());
           return dt;
       }

       /// <summary>
       /// 取得商品資料及商品類型(要篩選出有效日期區間內的料號，且不包含已失效的商品料號)
       /// </summary>
       /// <param name="sProductNo">商品料號</param>
       /// <param name="sProductName">商品名稱</param>
       /// <param name="sProdctTypeNo">商品料號編號</param>
       /// <returns></returns>
       public DataTable Query_Product(string sProductNo, string sProductName, string sProdctTypeNo)
       {
           StringBuilder sb = new StringBuilder();
           sb.AppendLine(@"SELECT T1.PRODNO
                                , T1.PRODNAME
                                , T2.PRODTYPENAME
                            FROM PRODUCT T1,  PRODUCT_TYPE T2
                            WHERE T1.PRODTYPENO = T2.PRODTYPENO(+) 
                            AND PRODNO not in (select PRODNO from NOT_IN_SALE_PROD where STATUS = 0) 
                            AND T1.ISCONSIGNMENT <> '1'
                        ");
           sb.AppendLine(Conditions);

           if (!string.IsNullOrEmpty(sProductNo))
           {
               sb.AppendLine(" AND T1.PRODNO LIKE " + OracleDBUtil.LastLikeStr(sProductNo));
           }

           if (!string.IsNullOrEmpty(sProductName))
           {
               sb.AppendLine(" AND LOWER(T1.PRODNAME) LIKE " + OracleDBUtil.ToLowerLikeStr(sProductName));
           }

           if (!string.IsNullOrEmpty(sProdctTypeNo))
           {
               sb.AppendLine(" AND T1.PRODTYPENO = " + OracleDBUtil.SqlStr(sProdctTypeNo));
           }
           sb.AppendLine(" AND rownum <= 500 Order by T1.PRODNO ");

           DataTable dt = OracleDBUtil.Query_Data(sb.ToString());
           return dt;
       }

       /// <summary>
       /// 取得商品資料及商品類型(要篩選出有效日期區間內的料號，且不包含已失效的商品料號與不在卡片群組裡)
       /// </summary>
       /// <param name="sProductNo">商品料號</param>
       /// <param name="sProductName">商品名稱</param>
       /// <param name="sProdctTypeNo">商品料號編號</param>
       /// <returns></returns>
       public DataTable Query_ProductNotInSIMGroup(string sProductNo, string sProductName, string sProdctTypeNo,string sStoreNo)
       {
           StringBuilder sb = new StringBuilder();
//           sb.AppendLine(@"SELECT T1.PRODNO
//                                , T1.PRODNAME
//                                , T2.PRODTYPENAME 
//                            FROM PRODUCT T1,  PRODUCT_TYPE T2 
//                            WHERE T1.PRODTYPENO = T2.PRODTYPENO(+) 
//                            AND NVL(T1.IS_DISCOUNT, 'N') = 'N' 
//                            AND T1.PRODNO NOT IN (SELECT PRODNO FROM SIM_GROUP_PROD D,SIM_GROUP_M M 
//                                                  WHERE D.SIM_GROUP_ID = M.SIM_GROUP_ID 
//                                                    AND TRUNC (S_DATE) <=TRUNC (SYSDATE ) 
//                            AND TRUNC(NVL(E_DATE,TO_DATE('9999/12/31','YYYY/MM/DD'))) >= TRUNC(SYSDATE )) 
//                        ");
           sb.AppendLine(@"SELECT T1.PRODNO
                                , T1.PRODNAME
                                , T2.PRODTYPENAME 
                            FROM PRODUCT T1,  PRODUCT_TYPE T2 
                            WHERE T1.PRODTYPENO = T2.PRODTYPENO(+) 
                            AND PRODNO not in (select PRODNO from NOT_IN_SALE_PROD where STATUS = 0) 
                            AND T1.PRODNO NOT IN (
                                                     SELECT PRODNO                                                             
                                                               FROM SIM_GROUP_PROD D
                                                                 JOIN SIM_GROUP_M M ON D.SIM_GROUP_ID = M.SIM_GROUP_ID
                                                                 JOIN SCQC_D SD ON M.SIM_GROUP_ID = SD.SIM_GROUP_ID   
                                                                 JOIN SCQC_M SM ON SD.SCQC_M_ID = SM.SCQC_M_ID   
                                                              WHERE 1 = 1
                                                                    AND TRUNC (M.S_DATE) <= TRUNC (SYSDATE)
                                                                    AND 
                                                                           NVL (M.E_DATE, TO_DATE ('9999/12/31', 'YYYY/MM/DD')) >=
                                                                           TRUNC (SYSDATE)
                                                                    AND  TRUNC (SD.S_DATE)<= TRUNC (SYSDATE) 
                                                                    AND NVL(TO_CHAR(SD.E_DATE,'YYYY/MM/DD'),'9999/12/31') >= TO_CHAR(SYSDATE,'YYYY/MM/DD') 
                                                                    $FUNC_STORE_NO  
                                                                    AND PRODNO = T1.PRODNO
                                                   ) 
                        ");
           sb.AppendLine(Conditions);

           if (!string.IsNullOrEmpty(sProductNo))
           {
               sb.AppendLine(" AND T1.PRODNO LIKE " + OracleDBUtil.LastLikeStr(sProductNo));
           }

           if (!string.IsNullOrEmpty(sProductName))
           {
               sb.AppendLine(" AND LOWER(T1.PRODNAME) LIKE " + OracleDBUtil.ToLowerLikeStr(sProductName));
           }

           if (!string.IsNullOrEmpty(sProdctTypeNo))
           {
               sb.AppendLine(" AND T1.PRODTYPENO = " + OracleDBUtil.SqlStr(sProdctTypeNo));
           }

           sb.AppendLine(" AND rownum <= 500 Order by T1.PRODNO ");

           string sbStr = sb.ToString();
           sbStr = sbStr.Replace("$FUNC_STORE_NO", " AND SM.STORE_NO = " + OracleDBUtil.SqlStr(sStoreNo));

           DataTable dt = OracleDBUtil.Query_Data(sbStr.ToString());
           return dt;
       }

       /// <summary>
       /// 取得商品類型為20, 21, 25, 26的商品資料(要篩選出有效日期區間內的料號，且不包含已失效的商品料號)
       /// </summary>
       /// <param name="sProductNo">商品料號</param>
       /// <param name="sProductName">商品名稱</param>
       /// <param name="sProdctTypeNo">商品料號編號</param>
       /// <returns></returns>
       public DataTable Query_Product2(string sProductNo, string sProductName, string sProdctTypeNo)
       {
           StringBuilder sb = new StringBuilder();
           sb.Append("SELECT T1.PRODNO, T1.PRODNAME, T2.PRODTYPENAME ");
           sb.Append("FROM PRODUCT T1, PRODUCT_TYPE T2 ");
           sb.Append("WHERE T1.PRODTYPENO = T2.PRODTYPENO(+) ");
           sb.Append("AND T2.PRODTYPENO IN ('20','21','25','26') ");
           sb.Append(Conditions);

           if (!string.IsNullOrEmpty(sProductNo))
           {
               sb.Append(" AND T1.PRODNO LIKE " + OracleDBUtil.LastLikeStr(sProductNo));
           }

           if (!string.IsNullOrEmpty(sProductName))
           {
               sb.Append(" AND LOWER(T1.PRODNAME) LIKE " + OracleDBUtil.ToLowerLikeStr(sProductName));
           }

           if (!string.IsNullOrEmpty(sProdctTypeNo))
           {
               sb.Append(" AND T1.PRODTYPENO = " + OracleDBUtil.SqlStr(sProdctTypeNo));
           }

           sb.Append(" AND rownum <= 500 Order by T1.PRODNO ");

           DataTable dt = OracleDBUtil.Query_Data(sb.ToString());
           return dt;
       }

       /// <summary>
       /// 取得加價購商品資料(要篩選出有效日期區間內的料號，且不包含已失效的商品料號)
       /// </summary>
       /// <param name="sProductNo">商品料號</param>
       /// <param name="sProductName">商品名稱</param>
       /// <param name="sProdctTypeNo">商品料號編號</param>
       /// <returns></returns>
       public DataTable Query_ProductExtraSale(string sProductNo, string sProductName, string sProdctTypeNo)
       {
           StringBuilder sb = new StringBuilder();
           sb.AppendLine(@" SELECT T1.PRODNO
                                    , T1.PRODNAME
                                    , T2.PRODTYPENAME 
                            FROM PRODUCT T1, PRODUCT_TYPE T2 
                            WHERE T1.PRODTYPENO = T2.PRODTYPENO(+) 
                            AND LENGTH (T1.prodno) = 9 
                            AND T1.PRODTYPENO IN ('10','11','15','22','30','31','35','80') 
                            AND PRODNO not in (select PRODNO from NOT_IN_SALE_PROD where STATUS = 0) ");
           sb.AppendLine(Conditions);

           if (!string.IsNullOrEmpty(sProductNo))
           {
               sb.AppendLine(" AND T1.PRODNO LIKE " + OracleDBUtil.LastLikeStr(sProductNo));
           }

           if (!string.IsNullOrEmpty(sProductName))
           {
               sb.AppendLine(" AND LOWER(T1.PRODNAME) LIKE " + OracleDBUtil.ToLowerLikeStr(sProductName));
           }

           if (!string.IsNullOrEmpty(sProdctTypeNo))
           {
               sb.AppendLine(" AND T1.PRODTYPENO = " + OracleDBUtil.SqlStr(sProdctTypeNo));
           }

           sb.AppendLine(" AND rownum <= 500 Order by T1.PRODNO ");

           DataTable dt = OracleDBUtil.Query_Data(sb.ToString());
           return dt;
       }

       /// <summary>
       /// 取得預訂購商品資料(要篩選出有效日期區間內的料號，且不包含已失效的商品料號)
       /// </summary>
       /// <param name="PRODNO">商品料號</param>
       /// <returns></returns>
       public DataTable Query_ProductPreOrderSale(string PRODNO)
       {
           StringBuilder sb = new StringBuilder();
           sb.Append(@"SELECT T1.PRODNO 
                            FROM PRODUCT T1
                            WHERE LENGTH(T1.PRODNO) = 9 
                            AND T1.PRICE >= 0
                            AND T1.PRODNO=" + OracleDBUtil.SqlStr(PRODNO) + Conditions + " Order by T1.PRODNO ");

           DataTable dt = OracleDBUtil.Query_Data(sb.ToString());
           return dt;
       }

       /// <summary>
       /// 取得加價購商品資料(要篩選出有效日期區間內的料號，且不包含已失效的商品料號)
       /// </summary>
       /// <param name="PRODNO">加價購商品料號</param>
       /// <returns></returns>
       public DataTable getPRODExtraSale(string PRODNO)
       {
           StringBuilder sb = new StringBuilder();
           sb.AppendLine(@" SELECT * 
                            FROM product T1 
                            WHERE LENGTH (T1.prodno) = 9 
                            AND T1.PRODTYPENO IN ('10','11','15','22','30','31','35','80') 
                            AND NVL(T1.IS_DISCOUNT,'N') = 'N' " +  Conditions + @"
                            AND T1.PRODNO = " + OracleDBUtil.SqlStr(PRODNO) + " Order by T1.PRODNO ");

           return OracleDBUtil.Query_Data(sb.ToString());
       }

       /// <summary>
       /// 取得所有Product資料，沒有任何條件
       /// </summary>
       /// <returns></returns>
       public DataTable Query_Product(string PRODNO)
       {
           StringBuilder sb = new StringBuilder();
           sb.AppendLine(@" SELECT PRODNO 
                                   , PRODNAME 
                            FROM   PRODUCT 
                            WHERE PRODNO = " + OracleDBUtil.SqlStr(PRODNO));

           DataTable dt = OracleDBUtil.Query_Data(sb.ToString());
           return dt;
       }

       /// <summary>
       /// 取得查詢商品資訊(要篩選出有效日期區間內的料號，且不包含已失效的商品料號)
       /// </summary>
       /// <param name="ProductNo">商品料號</param>
       /// <returns>DataTable</returns>
       public DataTable Query_ProductInfo(string ProductNo)
       {
           StringBuilder sb = new StringBuilder();
           sb.AppendLine(@" SELECT  T1.* 
                            FROM   PRODUCT T1 
                            WHERE  NVL(T1.IS_DISCOUNT, 'N') = 'N' " + Conditions +@"
                            AND T1.PRODNO = " + OracleDBUtil.SqlStr(ProductNo) + @" 
                            Order by T1.PRODNO 
                        ");

           DataTable dt = OracleDBUtil.Query_Data(sb.ToString());
           return dt;
       }

       /// <summary>
       /// 取得查詢商品資訊(要篩選出有效日期區間內的料號，且不包含已失效的商品料號，且商品料號不存在於[not_in_sale_prod]不可單品銷售商品檔且狀態為0:啟用)
       /// </summary>
       /// <param name="ProductNo">商品料號</param>
       /// <returns>DataTable</returns>
       public DataTable Query_ProductInfo(string ProductNo, string STORE_NO, string STOCK)
       {
           StringBuilder sb = new StringBuilder();
           sb.AppendLine(@" SELECT  T1.*, I.ON_HAND_QTY
                            FROM   PRODUCT T1, 
                                (
                                 Select * 
                                 from INV_ON_HAND_CURRENT 
                                 Where STOCK_ID = " + OracleDBUtil.SqlStr(STOCK) + @"
                                 And STORE_NO = " + OracleDBUtil.SqlStr(STORE_NO) + @"
                                ) I 
                            WHERE NVL(T1.IS_DISCOUNT, 'N') = 'N' 
                              AND T1.PRODNO = I.PRODNO(+) " + Conditions + @"
                              AND T1.PRODNO = " + OracleDBUtil.SqlStr(ProductNo) + @" 
                              AND T1.PRODNO not in (Select PRODNO From not_in_sale_prod Where Status = 0) 
                            Order by T1.PRODNO 
                        ");

           DataTable dt = OracleDBUtil.Query_Data(sb.ToString());
           return dt;
       }

       /// <summary>
       /// 取得折扣料號(篩選出有效日期區間內的料號，且不包含已失效的商品料號)
       /// </summary>
       /// <param name="sProductNo">商品料號</param>
       /// <param name="sProductName">商品名稱</param>
       /// <returns></returns>
       public DataTable Query_DiscountProduct(string sProductNo, string sProductName)
       {
           StringBuilder sb = new StringBuilder();
           sb.AppendLine(@" SELECT  T1.PRODNO 
                                    ,T1.PRODNAME 
                                    ,T2.PRODTYPENAME 
                            FROM PRODUCT T1, PRODUCT_TYPE T2 
                            WHERE T1.PRODTYPENO = T2.PRODTYPENO(+)  
                            AND T1.IS_DISCOUNT = 'Y' 
                        ");
           sb.AppendLine(Conditions);

           if (!string.IsNullOrEmpty(sProductNo))
           {
               sb.AppendLine(" AND T1.PRODNO LIKE " + OracleDBUtil.LikeStr(sProductNo));
           }

           if (!string.IsNullOrEmpty(sProductName))
           {
               sb.AppendLine(" AND T1.PRODNAME LIKE " + OracleDBUtil.LikeStr(sProductName));
           }

           sb.AppendLine(" ORDER BY T1.PRODNO ");

           DataTable dt = OracleDBUtil.Query_Data(sb.ToString());
           return dt;
       }

       /// <summary>
       /// 取得折扣名稱資訊(篩選出有效日期區間內的料號，且不包含已失效的商品料號)
       /// </summary>
       /// <param name="sProductNo"></param>
       /// <returns></returns>
       public DataTable Query_DiscountProduct(string sProductNo)
       {
           StringBuilder sb = new StringBuilder();
           sb.AppendLine(@" SELECT T1.PRODNO
                                   , T1.PRODNAME
                            FROM PRODUCT T1 
                            WHERE T1.IS_DISCOUNT = 'Y' " + Conditions  + @"
                            AND T1.PRODNO = " + OracleDBUtil.SqlStr(sProductNo) + @"
                            Order by T1.PRODNO 
                        ");

           DataTable dt = OracleDBUtil.Query_Data(sb.ToString());
           return dt;
       }

       /// <summary>
       /// 取得所有折扣名稱資訊List(篩選出有效日期區間內的料號，且不包含已失效的商品料號)
       /// </summary>
       /// <param name="sProductNo"></param>
       /// <returns></returns>
       public DataTable Query_DiscountProduct()
       {
           StringBuilder sb = new StringBuilder();
           sb.AppendLine(@"SELECT T1.PRODNO
                                , T1.PRODNAME 
                            FROM PRODUCT T1 
                            WHERE T1.IS_DISCOUNT = 'Y' " + Conditions + @"
                            Order by T1.PRODNO 
                        ");

           DataTable dt = OracleDBUtil.Query_Data(sb.ToString());
           return dt;
       }

       /// <summary>
       /// Join PRODUCT T1, SUPPLIER T2, PRODUCT_TYPE T3
       /// </summary>
       /// <param name="PRODNO">商品料號</param>
       /// <returns></returns>
       public DataTable Query_VW_PRODUCT(string PRODNO)
       {
           string sqlStr = "SELECT * FROM VW_PRODUCT WHERE PRODNO =" + OracleDBUtil.SqlStr(PRODNO) + " Order by PRODNO ";

           return OracleDBUtil.Query_Data(sqlStr);
       }

       /// <summary>
       /// 取得商品及ATR量資訊
       /// </summary>
       /// <param name="PRODNO">商品料號</param>
       /// <returns></returns>
       public DataTable Query_PRODUCT_ATR(string PRODNO)
       {
           StringBuilder sb = new StringBuilder();
           sb.AppendLine(@"SELECT T1.PRODNO
                                , T1.PRODNAME
                                , (NVL(ATRQTY,'0')-NVL(USE_ATR_QTY,'0')-NVL(NDS_BOOK_QTY,'0')) ATRQTY 
                            FROM  PRODUCT T1,POS_ATR PR  
                            WHERE T1.PRODNO = PR.PRODNO " + Conditions + @"
                            AND PR.DS_FLAG = 'N' --Y:DropShipment, N:NonDropShipment 
                            AND NVL(T1.IS_DISCOUNT, 'N') = 'N'--非折扣料號
                            AND T1.PRODNO = " + OracleDBUtil.SqlStr(PRODNO) + @"
                            AND TO_CHAR(PR.DWNDATE, 'YYYY/MM/DD') = " + OracleDBUtil.SqlStr(System.DateTime.Today.ToString("yyyy/MM/dd")));  //當日的ATR量
           sb.Append(" Order by T1.PRODNO ");

                      //sb.Append(" AND T1.DS_FLAG = 'N' ");     //Y:DropShipment, N:NonDropShipment  2011/01/25 Tina：不須判斷 PRODUCT 的 DS_FLAG

           DataTable dt = OracleDBUtil.Query_Data(sb.ToString());
           return dt;
       }
       
       /// <summary>
       /// 取得DropShipment主配商品名稱
       /// </summary>
       /// <param name="PRODNO">商品料號</param>
       /// <returns></returns>
       public DataTable Query_DSProdInfo(string PRODNO)
       {
           //Y:DropShipment, N:NonDropShipment  2011/04/18 Tina：DropShipment主配須判斷 PRODUCT 的 DS_FLAG
           StringBuilder sb = new StringBuilder();
           sb.AppendLine(@"SELECT P.PRODNAME
                            FROM  PRODUCT P
                            JOIN  POS_ATR PA ON P.PRODNO=PA.PRODNO and TRUNC(PA.DWNDATE) =TRUNC(sysdate)  
                            WHERE PA.DS_FLAG = 'Y' 
                              AND P.COMPANYCODE = '01' 
                              AND TRUNC(SYSDATE) >= TRUNC(NVL(P.S_DATE, SYSDATE)) AND TRUNC(SYSDATE) <= TRUNC(NVL(P.E_DATE, SYSDATE))
                              AND P.PRODNO = " + OracleDBUtil.SqlStr(PRODNO)
                        );
         

           DataTable dt = OracleDBUtil.Query_Data(sb.ToString());
           return dt;

       }

       /// <summary>
       /// 取得該門市此商品的庫存資訊(篩選出有效日期區間內的料號，且不包含已失效的商品料號)
       /// </summary>
       /// <param name="PRODNO">商品料號</param>
       /// <param name="STORE">門市編號</param>
       /// <returns></returns>
       public DataTable Query_PRODUCT_STOCK(string PRODNO, string STORE)
       {
           StringBuilder sb = new StringBuilder();
           sb.AppendLine(@" SELECT  T1.PRODNO
                                    , T1.PRODNAME 
                                    , NVL(INV_OnHandQty('" + PRODNO.Trim() + "','" + STORE.Trim() + @"'),0) AS INV_OnHandQty
                                    , T1.IMEI_FLAG 
                            FROM PRODUCT T1 
                            WHERE 1=1 
                            AND ROWNUM=1
                            AND NVL(T1.IS_DISCOUNT, 'N') = 'N' --非折扣料號 " + Conditions + @"
                            AND T1.PRODNO = " + OracleDBUtil.SqlStr(PRODNO.Trim()) + @"
                            Order by T1.PRODNO 
                        ");

           DataTable dt = OracleDBUtil.Query_Data(sb.ToString());
           return dt;

       }

       /// <summary>
       /// 查詢商品類別
       /// </summary>
       /// <param name="prodTypeNo">類別編號</param>
       /// <param name="prodTypeName">類別名稱</param>
       /// <returns></returns>
       public DataTable Query_ProductType(string prodTypeNo, string prodTypeName)
       {
           //string sqlStr = "SELECT * FROM VW_PRODUCT WHERE PRODNO =" + OracleDBUtil.SqlStr(prodTypeNo);
           StringBuilder sb = new StringBuilder();
           sb.Append(@"
                    SELECT
                        PRODTYPENO,
                        PRODTYPENAME
                    FROM
                        PRODUCT_TYPE
                    WHERE
                        DEL_FLAG = 'N'
                ");

           if (!string.IsNullOrEmpty(prodTypeNo))
           {
               sb.Append(" AND PRODTYPENO LIKE " + OracleDBUtil.LikeStr(prodTypeNo));
           }

           if (!string.IsNullOrEmpty(prodTypeName))
           {
               sb.Append(" AND PRODTYPENAME LIKE " + OracleDBUtil.LikeStr(prodTypeName));
           }

           return OracleDBUtil.Query_Data(sb.ToString());
       }
       /// <summary>
       /// 取得寄銷商品資料(要篩選出有效日期區間內的料號，且不包含已失效的商品料號)
       /// </summary>
       /// <param name="sProductNo">商品料號</param>
       /// <param name="sProductName">商品名稱</param>
       /// <param name="sProdctTypeNo">商品料號編號</param>
       /// <returns></returns>
       public DataTable Query_ProductConsignmentSale(string sProductNo, string sProductName, string sProdctTypeNo)
       {
           StringBuilder sb = new StringBuilder();
           sb.AppendLine(@" SELECT T1.PRODNO
                                    , T1.PRODNAME
                                    , T1.PRODTYPENO
                                    , T2.PRODTYPENAME
                                    , T1.PRICE 
                            FROM PRODUCT T1, CSM_PRODUCT_TYPE T2 
                            WHERE T1.PRODTYPENO = T2.PRODTYPENO(+) 
                            AND LENGTH (T1.prodno) = 9 
                            AND PRODNO not in (select PRODNO from NOT_IN_SALE_PROD where STATUS = 0) ");
           sb.AppendLine(Conditions);
           //sb.AppendLine(" AND T1.PRODTYPENO IN ('10','11','15','22','30','31','35','80')  ");          

           if (!string.IsNullOrEmpty(sProductNo))
           {
               sb.AppendLine(" AND T1.PRODNO LIKE " + OracleDBUtil.LastLikeStr(sProductNo));
           }

           if (!string.IsNullOrEmpty(sProductName))
           {
               sb.AppendLine(" AND LOWER(T1.PRODNAME) LIKE " + OracleDBUtil.ToLowerLikeStr(sProductName));
           }

           if (!string.IsNullOrEmpty(sProdctTypeNo))
           {
               sb.AppendLine(" AND T1.PRODTYPENO = " + OracleDBUtil.SqlStr(sProdctTypeNo));
           }

           sb.AppendLine(" AND rownum <= 500 Order by T1.PRODNO ");

           DataTable dt = OracleDBUtil.Query_Data(sb.ToString());
           return dt;
       }

       /// <summary>
       /// 取得寄銷商品資料(要篩選出有效日期區間內的料號，且不包含已失效的商品料號)
       /// </summary>
       /// <param name="sProductNo">商品料號</param>
       /// <param name="sProductName">商品名稱</param>
       /// <param name="sProdctTypeNo">商品料號編號</param>
       /// <param name="sSuppID">廠商編號</param>
       /// <returns></returns>
       public DataTable Query_ProductConsignmentSale(string sProductNo, string sProductName, string sProdctTypeNo, string sSuppID)
       {
           StringBuilder sb = new StringBuilder();
           sb.AppendLine(@" SELECT  T1.PRODNO
                                    , T1.PRODNAME
                                    , T1.PRODTYPENO
                                    , T2.PRODTYPENAME
                                    , T1.PRICE 
                                    , T1.ACCOUNTCODE
                            FROM PRODUCT T1, CSM_PRODUCT_TYPE T2 
                            WHERE T1.PRODTYPENO = T2.PRODTYPENO(+) 
                            AND LENGTH (T1.prodno) = 9 
                            AND NVL(T1.ISCONSIGNMENT,'0') = '1'" + Conditions
                        );

           //sb.Append(" AND T1.PRODTYPENO IN ('10','11','15','22','30','31','35','80')  ");          
           //sb.Append(" AND T1.SUPP_ID =" + OracleDBUtil.SqlStr(sSuppID)); 彈性輸入的條件

           if (!string.IsNullOrEmpty(sSuppID))
           {//彈性輸入的條件
               sb.AppendLine(" AND T1.SUPP_ID = " + OracleDBUtil.SqlStr(sSuppID));
           }
           if (!string.IsNullOrEmpty(sProductNo))
           {
               sb.AppendLine(" AND T1.PRODNO LIKE " + OracleDBUtil.LastLikeStr(sProductNo));
           }

           if (!string.IsNullOrEmpty(sProductName))
           {
               sb.Append(" AND LOWER(T1.PRODNAME) LIKE " + OracleDBUtil.ToLowerLikeStr(sProductName));
           }

           if (!string.IsNullOrEmpty(sProdctTypeNo))
           {
               sb.AppendLine(" AND T1.PRODTYPENO = " + OracleDBUtil.SqlStr(sProdctTypeNo));
           }

           sb.AppendLine(" AND rownum <= 500 Order by T1.PRODNO ");

           DataTable dt = OracleDBUtil.Query_Data(sb.ToString());
           return dt;
       }
       /// <summary>
       /// 確認商品料號是否重複
       /// </summary>
       /// <param name="PRODNO"></param>
       /// <returns></returns>
       public string Check_Id(string PRODNO)
       {
           StringBuilder sb = new StringBuilder();
           sb.AppendLine(" SELECT * FROM PRODUCT WHERE 1=1");
           if (!string.IsNullOrEmpty(PRODNO))
           {
               sb.AppendLine(" AND ROWNUM =1 AND PRODNO = " + OracleDBUtil.SqlStr(PRODNO));
           }

           DataTable dt = OracleDBUtil.Query_Data(sb.ToString());

           if (dt.Rows.Count > 0)
           {
               return "商品料號重複";
           }
           else
           {
               return "";
           }
       }

       /// <summary>
       /// 租賃專用
       /// 取得折扣料號(篩選出有效日期區間內的料號，且不包含已失效的商品料號) 
       /// 只取 DISCOUNT_TYPE 3.租賃 
       /// </summary>
       /// <param name="sProductNo">商品料號</param>
       /// <param name="sProductName">商品名稱</param>
       /// <returns></returns>
       public DataTable Query_LeaseDiscountProduct(string sProductNo, string sProductName)
       {
           StringBuilder sb = new StringBuilder();
           sb.AppendLine(@" SELECT  T1.PRODNO 
                                    ,T1.PRODNAME
                                    ,T2.PRODTYPENAME
                            FROM PRODUCT T1, PRODUCT_TYPE T2, PRODUCT_DISCOUNT M , DISCOUNT_MASTER D 
                            WHERE T1.PRODTYPENO = T2.PRODTYPENO(+) 
                            AND D.DISCOUNT_TYPE = 3 
                            AND T1.IS_DISCOUNT = 'Y'
                            AND M.PRODNO = T1.PRODNO
                            AND M.DISCOUNT_MASTER_ID = D.DISCOUNT_MASTER_ID " + Conditions
                      );

           if (!string.IsNullOrEmpty(sProductNo))
           {
               sb.AppendLine(" AND T1.PRODNO LIKE " + OracleDBUtil.LikeStr(sProductNo));
           }

           if (!string.IsNullOrEmpty(sProductName))
           {
               sb.AppendLine(" AND T1.PRODNAME LIKE " + OracleDBUtil.LikeStr(sProductName));
           }

           sb.AppendLine(" ORDER BY T1.PRODNO ");

           DataTable dt = OracleDBUtil.Query_Data(sb.ToString());
           return dt;
       }

       /// <summary>
       /// 依據商品料號取得Product所有資料，沒有任何條件
       /// </summary>
       /// <returns></returns>
       public DataTable Query_ProductAllInfo_By_ProdNo(string PRODNO)
       {
           StringBuilder sb = new StringBuilder();
           sb.AppendLine(@" SELECT *  
                            FROM   PRODUCT 
                            WHERE PRODNO = " + OracleDBUtil.SqlStr(PRODNO));

           DataTable dt = OracleDBUtil.Query_Data(sb.ToString());
           return dt;
       }
   }
}
