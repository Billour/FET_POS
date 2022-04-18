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
    public class ORD04_Facade
    {
        public void UpdateOne_ORDER(ORD04_ORDERM ds)
        {
            OracleDBUtil.UPDDATEByUUID(ds.Tables["ORDER_D"], "ORDER_ITEMS_ID");
        }

        public DataTable TopDatatable1(string SNO, string ENO, string StoreNO, string ORDDate, string ORDDate1, string PRODNO, string STATUS, string zone)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(@" select ORDER_ITEMS_ID
                            ,ORDER_M.ORDER_ID
                            ,ORDDATE
                            ,ORDER_M.ORDER_NO
                            ,ORDER_M.ISOK
                            ,ORDER_M.STORE_NO
                            ,STORENAME
                            ,ORDQTY
                            ,PRODNO
                            ,PRODNO_M
                            ,decode(STATUS,'10','正式','50','已傳輸','51','已成單') STATUS
                            ,NVL(STOCK_QTY,0) STOCK_QTY
                            ,NVL(HQ_ADJ_ORDER_QTY,0) HQ_ADJ_ORDER_QTY
                            ,ORDER_D.REMARK 
                            FROM  ORDER_M,ORDER_D,STORE,ZONE 
                            where ORDER_M.ORDER_ID = ORDER_D.ORDER_ID(+) 
                              and ORDER_M.STORE_NO = STORE.STORE_NO AND ORDER_TYPE = 1 
                              and STORE.ZONE = ZONE.ZONE 
                              and NVL(PRODNO_M, ' ') = ' ' ");
            //10-正式/11-預訂/50-己傳輸/51-已成單/70-已刪除/80-未驗收/85-部份驗收/90-己結案。
            if (!string.IsNullOrEmpty(ORDDate) && !string.IsNullOrEmpty(ORDDate1))
            {
                ORDDate = ORDDate.Replace("/", "");
                ORDDate1 = ORDDate1.Replace("/", "");
                sb.AppendLine(" AND ORDDATE between " + OracleDBUtil.SqlStr(ORDDate) + " AND  " + OracleDBUtil.SqlStr(ORDDate1));
            }
            if (!string.IsNullOrEmpty(SNO) && !string.IsNullOrEmpty(ENO))
            {
                sb.AppendLine(" AND ORDER_NO between " + OracleDBUtil.SqlStr(SNO) + " AND " + OracleDBUtil.SqlStr(ENO));
            }
            if (!string.IsNullOrEmpty(SNO) && string.IsNullOrEmpty(ENO))
            {
                sb.AppendLine(" AND ORDER_NO >= " + OracleDBUtil.SqlStr(SNO));
            }
            if (string.IsNullOrEmpty(SNO) && !string.IsNullOrEmpty(ENO))
            {
                sb.AppendLine(" AND ORDER_NO <= " + OracleDBUtil.SqlStr(ENO));
            }
            if (!string.IsNullOrEmpty(StoreNO))
            {
                sb.AppendLine(" AND ORDER_M.STORE_NO like " + OracleDBUtil.LikeStr(StoreNO));
            }
            if (!string.IsNullOrEmpty(PRODNO))
            {
                sb.AppendLine(" AND PRODNO like " + OracleDBUtil.LikeStr(PRODNO));
            }
            if (!string.IsNullOrEmpty(STATUS))
            {
                sb.AppendLine(" AND STATUS = " + OracleDBUtil.SqlStr(STATUS));
            }
            if (!string.IsNullOrEmpty(zone))
            {
                sb.AppendLine(" AND Store.ZONE = " + OracleDBUtil.SqlStr(zone));
            }
            sb.AppendLine(" ORDER By ORDDATE,ORDER_M.ORDER_NO ");

            DataTable dt = OracleDBUtil.Query_Data(sb.ToString());
            return dt;
        }

        public string GetProdNo(string KeyID)
        {
            string str = "";
            StringBuilder sb = new StringBuilder();

            sb.Append(@" select  PRODNO, PRODNO_M  
                            from ORDER_D 
                            WHERE 1=1 ");
            if (!string.IsNullOrEmpty(KeyID))
            {
                sb.Append(" AND ORDER_ITEMS_ID = " + OracleDBUtil.SqlStr(KeyID));
            }

            DataTable dt = OracleDBUtil.Query_Data(sb.ToString());

            foreach (DataRow dr in dt.Rows)
            {
                str = dr["PRODNO"].ToString() + ";" + dr["PRODNO_M"].ToString();
            }

            return str;
        }

        public string getPOSATR(string PRODNO)
        {
            string str = "";
            StringBuilder sb = new StringBuilder();
            sb.Append(" select  decode(ATRQTY,'',0,ATRQTY) ATRQTY  ");
            sb.Append(" from pos_atr ");
            sb.Append(" WHERE 1=1 and To_char(DWNDATE,'yyyy/mm/dd') = '" + DateTime.Now.ToString("yyyy/MM/dd") + "' ");
            if (!string.IsNullOrEmpty(PRODNO))
            {
                sb.Append(" AND PRODNO = " + OracleDBUtil.SqlStr(PRODNO));
            }

            DataTable dt = OracleDBUtil.Query_Data(sb.ToString());

            foreach (DataRow dr in dt.Rows)
            {
                str = dr["ATRQTY"].ToString();
            }

            return str;
        }

        public DataTable TopDatatable2(string SNO, string ENO, string StoreNO, string ORDDate, string ORDDate1, string PRODNO, string STATUS, string zone)
        {
            //**2011/03/01 Tina：商品料號只需顯示一個，不能重複顯示(舊程式中，商品若有搭贈商品，同一主商品料號會重複顯示)。
            //                   可分配量須先比較主商品和搭贈商品何者的ATR量最少，就以最少者顯示。
            //                   總訂貨量為訂購量的總合，以及總調整量為業助調整數量的總合，兩者不需為Group by的條件。
            StringBuilder sbColumns1 = new StringBuilder();
            //單一商品查詢
            sbColumns1.AppendLine(@"SELECT ORDER_D.PRODNO as PRODNO_M
                                    , A.PRODNAME PRODNAME_M
                                    , ORDER_D.PRODNO_M as PRODNO
                                    , B.PRODNAME 
                                    , sum(ORDQTY)  ORDQTY
                                    , (CASE WHEN ATR1.ATRQTY <= NVL(ATR2.ATRQTY,99999999999) THEN (NVL(ATR1.ATRQTY,0)-NVL(ATR1.USE_ATR_QTY,0)-NVL(ATR1.NDS_BOOK_QTY,0)) ELSE (NVL(ATR2.ATRQTY,0)-NVL(ATR2.USE_ATR_QTY,0)-NVL(ATR2.NDS_BOOK_QTY,0)) END) STOCK_QTY
                                    , sum(HQ_ADJ_ORDER_QTY)  HQ_QTY ");

            //有搭贈商品的主商品查詢
            StringBuilder sbColumns2 = new StringBuilder();
            sbColumns2.AppendLine(@"SELECT PRODNO_M
                                    , B.PRODNAME PRODNAME_M 
                                    , ORDER_D.PRODNO 
                                    , A.PRODNAME
                                    , sum(ORDQTY)  ORDQTY
                                    , (CASE WHEN ATR1.ATRQTY <= NVL(ATR2.ATRQTY,99999999999) THEN (NVL(ATR1.ATRQTY,0)-NVL(ATR1.USE_ATR_QTY,0)-NVL(ATR1.NDS_BOOK_QTY,0)) ELSE (NVL(ATR2.ATRQTY,0)-NVL(ATR2.USE_ATR_QTY,0)-NVL(ATR2.NDS_BOOK_QTY,0)) END) STOCK_QTY
                                    , sum(HQ_ADJ_ORDER_QTY)  HQ_QTY ");
            //Join的Tables
            StringBuilder sbFrom = new StringBuilder();
            sbFrom.AppendLine(@"FROM  ORDER_M, ORDER_D, Product A,  Product B, STORE, LOC, POS_ATR ATR1, POS_ATR ATR2   
                                WHERE ORDER_M.ORDER_ID = ORDER_D.ORDER_ID(+) 
                                  AND ORDER_D.PRODNO = A.PRODNO(+)
                                  AND ORDER_D.PRODNO_M = B.PRODNO(+)
                                  AND ORDER_D.PRODNO = ATR1.PRODNO(+)
                                  AND ORDER_D.PRODNO_M = ATR2.PRODNO(+)
                                  AND ORDER_M.STORE_NO = STORE.STORE_NO(+)
                                  AND LOC.LOC_ID =  (CASE WHEN ATR1.ATRQTY <= NVL(ATR2.ATRQTY,99999999999) THEN ATR1.LOC_ID ELSE ATR2.LOC_ID END)
                                  AND " + OracleDBUtil.DateStr(DateTime.Now.ToString("yyyy/MM/dd")) + @" = trunc(NVL(ATR1.DWNDATE, SYSDATE))
                                  AND " + OracleDBUtil.DateStr(DateTime.Now.ToString("yyyy/MM/dd")) + @" = trunc(NVL(ATR2.DWNDATE, SYSDATE))
                                  AND order_type = '1' ");
            //** 2011/03/02 Tina：變更 AND " + OracleDBUtil.DateStr(DateTime.Now.ToString("yyyy/MM/dd")) + @" = trunc((CASE WHEN ATR1.ATRQTY <= NVL(ATR2.ATRQTY,99999999999) THEN ATR1.DWNDATE ELSE ATR2.DWNDATE END))

            //查詢條件
            StringBuilder sbWhere = new StringBuilder();
            if (!string.IsNullOrEmpty(ORDDate) && !string.IsNullOrEmpty(ORDDate1))
            {
                ORDDate = ORDDate.Replace("/", "");
                ORDDate1 = ORDDate1.Replace("/", "");
                sbWhere.AppendLine(" AND ORDDATE between " + OracleDBUtil.SqlStr(ORDDate) + " AND  " + OracleDBUtil.SqlStr(ORDDate1));
            }
            if (!string.IsNullOrEmpty(SNO) && !string.IsNullOrEmpty(ENO))
            {
                sbWhere.AppendLine(" AND ORDER_NO between " + OracleDBUtil.SqlStr(SNO) + " AND " + OracleDBUtil.SqlStr(ENO));
            }
            if (!string.IsNullOrEmpty(SNO) && string.IsNullOrEmpty(ENO))
            {
                sbWhere.AppendLine(" AND ORDER_NO >= " + OracleDBUtil.SqlStr(SNO));
            }
            if (string.IsNullOrEmpty(SNO) && !string.IsNullOrEmpty(ENO))
            {
                sbWhere.AppendLine(" AND ORDER_NO <= " + OracleDBUtil.SqlStr(ENO));
            }
            if (!string.IsNullOrEmpty(StoreNO))
            {
                sbWhere.AppendLine(" AND ORDER_M.STORE_NO like " + OracleDBUtil.LikeStr(StoreNO));
            }
            if (!string.IsNullOrEmpty(PRODNO))
            {
                sbWhere.AppendLine(" AND (ORDER_D.PRODNO like " + OracleDBUtil.LikeStr(PRODNO));
                sbWhere.AppendLine(" OR ORDER_D.PRODNO_M like " + OracleDBUtil.LikeStr(PRODNO) + ")");
            }
            if (!string.IsNullOrEmpty(STATUS))
            {
                sbWhere.AppendLine(" AND ORDER_M.STATUS = " + OracleDBUtil.SqlStr(STATUS));
            }
            if (!string.IsNullOrEmpty(zone))
            {
                sbWhere.AppendLine(" AND Store.ZONE = " + OracleDBUtil.SqlStr(zone));
            }

            //Group的欄位
            StringBuilder sbGroup = new StringBuilder();
            sbGroup.AppendLine(@"GROUP by ORDER_D.PRODNO,(CASE WHEN ATR1.ATRQTY <= NVL(ATR2.ATRQTY,99999999999) THEN (NVL(ATR1.ATRQTY,0)-NVL(ATR1.USE_ATR_QTY,0)-NVL(ATR1.NDS_BOOK_QTY,0)) ELSE (NVL(ATR2.ATRQTY,0)-NVL(ATR2.USE_ATR_QTY,0)-NVL(ATR2.NDS_BOOK_QTY,0)) END),A.PRODNAME,PRODNO_M ,B.PRODNAME ");

            StringBuilder sb = new StringBuilder();
            sb.AppendLine(sbColumns1.ToString() + sbFrom.ToString() + @" AND GIFT_FALG='0' 
                    AND ORDER_D.PRODNO NOT IN (SELECT DISTINCT NVL(prodno_m,'NULL DATA') " + sbFrom.ToString() + sbWhere.ToString() + ")" + sbWhere.ToString() + sbGroup.ToString());
            sb.AppendLine("union");
            sb.AppendLine(sbColumns2.ToString() + sbFrom.ToString() + " AND GIFT_FALG='1' " + sbWhere.ToString() + sbGroup.ToString());

            #region 2011/03/01 Tina：註解舊程式
            /*
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT ORDER_D.PRODNO as PRODNO_M,A.PRODNAME PRODNAME_M,PRODNO_M as PRODNO, B.PRODNAME , ");
            sb.Append("        sum(ORDQTY)  ORDQTY, ");
            sb.Append("        ATRQTY  STOCK_QTY, ");
            sb.Append("        sum(HQ_ADJ_ORDER_QTY)  HQ_QTY ");
            sb.Append(" FROM  ORDER_M, ORDER_D, STORE, Product A,  Product B, LOC, POS_ATR  ");
            sb.Append(" WHERE ORDER_M.ORDER_ID = ORDER_D.ORDER_ID(+) AND ORDER_M.STORE_NO = STORE.STORE_NO(+) AND ORDER_D.PRODNO = A.PRODNO(+) AND ORDER_D.PRODNO_M = B.PRODNO(+) ");
            sb.Append(" AND  A.PRODNO = POS_ATR.PRODNO AND LOC.LOC_ID = POS_ATR.LOC_ID ");
            sb.Append(" AND To_char(POS_ATR.DWNDATE,'yyyy/mm/dd') = '" + DateTime.Now.ToString("yyyy/MM/dd") + "' and order_type = '1'  and GIFT_FALG='0'  ");
 

            if (!string.IsNullOrEmpty(ORDDate) && !string.IsNullOrEmpty(ORDDate1))
            {
                ORDDate = ORDDate.Replace("/", "");
                ORDDate1 = ORDDate1.Replace("/", "");
                sb.Append(" AND ORDDATE between " + OracleDBUtil.SqlStr(ORDDate));
                sb.Append(" AND  " + OracleDBUtil.SqlStr(ORDDate1));
            }
            if (!string.IsNullOrEmpty(SNO) && !string.IsNullOrEmpty(ENO))
            {
                sb.Append(" AND ORDER_NO between " + OracleDBUtil.SqlStr(SNO));
                sb.Append(" AND " + OracleDBUtil.SqlStr(ENO));
            }
            if (!string.IsNullOrEmpty(SNO) && string.IsNullOrEmpty(ENO))
            {
                sb.Append(" AND ORDER_NO >= " + OracleDBUtil.SqlStr(SNO));
            }
            if (string.IsNullOrEmpty(SNO) && !string.IsNullOrEmpty(ENO))
            {
                sb.Append(" AND ORDER_NO <= " + OracleDBUtil.SqlStr(ENO));
            }
            if (!string.IsNullOrEmpty(StoreNO))
            {
                sb.Append(" AND ORDER_M.STORE_NO like " + OracleDBUtil.LikeStr(StoreNO));
            }
            if (!string.IsNullOrEmpty(PRODNO))
            {
                sb.Append(" AND ORDER_D.PRODNO like " + OracleDBUtil.LikeStr(PRODNO));
            }
            if (!string.IsNullOrEmpty(STATUS))
            {
                sb.Append(" AND ORDER_M.STATUS = " + OracleDBUtil.SqlStr(STATUS));
            }
            if (!string.IsNullOrEmpty(zone))
            {
                sb.Append(" AND Store.ZONE = " + OracleDBUtil.SqlStr(zone));
            }
            sb.Append("  GROUP by ORDER_D.PRODNO,ORDQTY,ATRQTY,A.PRODNAME,HQ_ADJ_ORDER_QTY,PRODNO_M ,B.PRODNAME");
            sb.Append(" union ");
            sb.Append(" SELECT PRODNO_M, B.PRODNAME PRODNAME_M ,ORDER_D.PRODNO ,A.PRODNAME ,  ");
            sb.Append("        sum(ORDQTY)  ORDQTY, ");
            sb.Append("        ATRQTY  STOCK_QTY, ");
            sb.Append("        sum(HQ_ADJ_ORDER_QTY)  HQ_QTY ");
            sb.Append(" FROM  ORDER_M, ORDER_D, STORE, Product A,  Product B, LOC, POS_ATR  ");
            sb.Append(" WHERE ORDER_M.ORDER_ID = ORDER_D.ORDER_ID(+) AND ORDER_M.STORE_NO = STORE.STORE_NO(+) AND ORDER_D.PRODNO = A.PRODNO(+) AND ORDER_D.PRODNO_M = B.PRODNO(+) ");
            sb.Append(" AND  A.PRODNO = POS_ATR.PRODNO AND LOC.LOC_ID = POS_ATR.LOC_ID ");
            sb.Append(" AND To_char(POS_ATR.DWNDATE,'yyyy/mm/dd') = '" + DateTime.Now.ToString("yyyy/MM/dd") + "' and order_type = '1' and GIFT_FALG='1'  ");


            if (!string.IsNullOrEmpty(ORDDate) && !string.IsNullOrEmpty(ORDDate1))
            {
                ORDDate = ORDDate.Replace("/", "");
                ORDDate1 = ORDDate1.Replace("/", "");
                sb.Append(" AND ORDDATE between " + OracleDBUtil.SqlStr(ORDDate));
                sb.Append(" AND  " + OracleDBUtil.SqlStr(ORDDate1));
            }
            if (!string.IsNullOrEmpty(SNO) && !string.IsNullOrEmpty(ENO))
            {
                sb.Append(" AND ORDER_NO between " + OracleDBUtil.SqlStr(SNO));
                sb.Append(" AND " + OracleDBUtil.SqlStr(ENO));
            }
            if (!string.IsNullOrEmpty(SNO) && string.IsNullOrEmpty(ENO))
            {
                sb.Append(" AND ORDER_NO >= " + OracleDBUtil.SqlStr(SNO));
            }
            if (string.IsNullOrEmpty(SNO) && !string.IsNullOrEmpty(ENO))
            {
                sb.Append(" AND ORDER_NO <= " + OracleDBUtil.SqlStr(ENO));
            }
            if (!string.IsNullOrEmpty(StoreNO))
            {
                sb.Append(" AND ORDER_M.STORE_NO like " + OracleDBUtil.LikeStr(StoreNO));
            }
            if (!string.IsNullOrEmpty(PRODNO))
            {
                sb.Append(" AND ORDER_D.PRODNO like " + OracleDBUtil.LikeStr(PRODNO));
            }
            if (!string.IsNullOrEmpty(STATUS))
            {
                sb.Append(" AND ORDER_M.STATUS = " + OracleDBUtil.SqlStr(STATUS));
            }
            if (!string.IsNullOrEmpty(zone))
            {
                sb.Append(" AND Store.ZONE = " + OracleDBUtil.SqlStr(zone));
            }
            sb.Append("  GROUP by ORDER_D.PRODNO,ORDQTY,ATRQTY,A.PRODNAME,HQ_ADJ_ORDER_QTY,PRODNO_M ,B.PRODNAME");
            //           SELECT ORDER_D.PRODNO,A.PRODNAME,PRODNO_M, B.PRODNAME PRODNAME_M,         sum(ORDQTY)  ORDQTY,         ATRQTY  STOCK_QTY,       
            //sum(HQ_ADJ_ORDER_QTY)  HQ_QTY  FROM  ORDER_M, ORDER_D, STORE, Product A,  Product B, LOC, POS_ATR   
            //WHERE ORDER_M.ORDER_ID = ORDER_D.ORDER_ID(+) AND ORDER_M.STORE_NO = STORE.STORE_NO(+) AND ORDER_D.PRODNO = A.PRODNO(+) 
            //AND ORDER_D.PRODNO_M = B.PRODNO(+)  AND A.PRODNO = POS_ATR.PRODNO AND LOC.LOC_ID = POS_ATR.LOC_ID 
            //and order_type = '1'   AND ORDDATE between '20110130' AND  '20110224'  and to_CHAR(DWNDATE,'yyyy/mm/dd') = '2011/02/24'  and GIFT_FALG='0'
            // GROUP by ORDER_D.PRODNO,ORDQTY,ATRQTY,A.PRODNAME,HQ_ADJ_ORDER_QTY,PRODNO_M ,B.PRODNAME  
            // union 
            // SELECT ORDER_D.PRODNO as  PRODNO_M,A.PRODNAME PRODNAME_M,PRODNO_M as  PRODNO, B.PRODNAME ,         sum(ORDQTY)  ORDQTY,         ATRQTY  STOCK_QTY,        
            //sum(HQ_ADJ_ORDER_QTY)  HQ_QTY  FROM  ORDER_M, ORDER_D, STORE, Product A,  Product B, LOC, POS_ATR   
            //WHERE ORDER_M.ORDER_ID = ORDER_D.ORDER_ID(+) AND ORDER_M.STORE_NO = STORE.STORE_NO(+) AND ORDER_D.PRODNO = A.PRODNO(+) 
            //AND ORDER_D.PRODNO_M = B.PRODNO(+)   AND A.PRODNO = POS_ATR.PRODNO AND LOC.LOC_ID = POS_ATR.LOC_ID 
            //and order_type = '1'   AND ORDDATE between '20110130' AND  '20110224'   and GIFT_FALG='1'
            // GROUP by ORDER_D.PRODNO,ORDQTY,ATRQTY,A.PRODNAME,HQ_ADJ_ORDER_QTY,PRODNO_M ,B.PRODNAM
            */
            #endregion

            DataTable dt = OracleDBUtil.Query_Data(sb.ToString());
            return dt;
        }

        public DataTable TopDatatable3(string SNO, string ENO, string StoreNO, string ORDDate, string ORDDate1, string PRODNO, string STATUS, string zone, string PRODNO_M)
        {
            #region 2011/03/02 Tina：註解舊程式

            //StringBuilder sb = new StringBuilder();
            ////sb.Append("SELECT ORDER_D.PRODNO as PRODNO_M,A.PRODNAME PRODNAME_M,PRODNO_M as PRODNO, B.PRODNAME , ");
            ////sb.Append("        sum(ORDQTY)  ORDQTY, ");
            ////sb.Append("        ATRQTY  STOCK_QTY, ");
            ////sb.Append("        sum(HQ_ADJ_ORDER_QTY)  HQ_QTY ");
            ////sb.Append(" FROM  ORDER_M, ORDER_D, STORE, Product A,  Product B, LOC, POS_ATR  ");
            ////sb.Append(" WHERE ORDER_M.ORDER_ID = ORDER_D.ORDER_ID(+) AND ORDER_M.STORE_NO = STORE.STORE_NO(+) AND ORDER_D.PRODNO = A.PRODNO(+) AND ORDER_D.PRODNO_M = B.PRODNO(+) ");
            ////sb.Append(" AND  A.PRODNO = POS_ATR.PRODNO AND LOC.LOC_ID = POS_ATR.LOC_ID ");
            ////sb.Append(" AND To_char(POS_ATR.DWNDATE,'yyyy/mm/dd') = '" + DateTime.Now.ToString("yyyy/MM/dd") + "' and order_type = '1'  and GIFT_FALG='0'  ");


            //sb.Append("SELECT ORDER_D.PRODNO as PRODNO_M,A.PRODNAME PRODNAME_M,PRODNO_M as PRODNO, B.PRODNAME, ");
            //sb.Append("        sum(ORDQTY)  ORDQTY, ");
            //sb.Append("        ATRQTY  STOCK_QTY, ");
            //sb.Append("        sum(HQ_ADJ_ORDER_QTY)  HQ_QTY ");
            //sb.Append(" FROM   ORDER_M, ORDER_D, STORE, Product A, Product B, pos_store_atr  ");
            //sb.Append(" WHERE ORDER_M.ORDER_ID = ORDER_D.ORDER_ID(+) AND ORDER_M.STORE_NO = STORE.STORE_NO(+) AND ORDER_D.PRODNO = A.PRODNO(+) ");
            //sb.Append(" AND ORDER_D.PRODNO_M = B.PRODNO(+) AND A.PRODNO = pos_store_atr.PRODNO AND STORE.STORE_NO = POS_STORE_ATR.STORE_NO (+) ");
            //sb.Append(" AND To_char(ATR_DATE,'yyyy/mm/dd') ='" + DateTime.Now.ToString("yyyy/MM/dd") + "' and order_type = '1'   and GIFT_FALG='0'   ");

            //if (!string.IsNullOrEmpty(ORDDate) && !string.IsNullOrEmpty(ORDDate1))
            //{
            //    ORDDate = ORDDate.Replace("/", "");
            //    ORDDate1 = ORDDate1.Replace("/", "");
            //    sb.Append(" AND ORDDATE between " + OracleDBUtil.SqlStr(ORDDate));
            //    sb.Append(" AND  " + OracleDBUtil.SqlStr(ORDDate1));
            //}
            //if (!string.IsNullOrEmpty(SNO) && !string.IsNullOrEmpty(ENO))
            //{
            //    sb.Append(" AND ORDER_NO between " + OracleDBUtil.SqlStr(SNO));
            //    sb.Append(" AND " + OracleDBUtil.SqlStr(ENO));
            //}
            //if (!string.IsNullOrEmpty(SNO) && string.IsNullOrEmpty(ENO))
            //{
            //    sb.Append(" AND ORDER_NO >= " + OracleDBUtil.SqlStr(SNO));
            //}
            //if (string.IsNullOrEmpty(SNO) && !string.IsNullOrEmpty(ENO))
            //{
            //    sb.Append(" AND ORDER_NO <= " + OracleDBUtil.SqlStr(ENO));
            //}
            //if (!string.IsNullOrEmpty(ORDER_ITEM_ID))
            //{
            //    sb.Append(" AND ORDER_ITEMS_ID = " + OracleDBUtil.SqlStr(ORDER_ITEM_ID));
            //}
            //if (!string.IsNullOrEmpty(StoreNO))
            //{
            //    sb.Append(" AND ORDER_M.STORE_NO like " + OracleDBUtil.LikeStr(StoreNO));
            //}
            //if (!string.IsNullOrEmpty(PRODNO))
            //{
            //    sb.Append(" AND ORDER_D.PRODNO like " + OracleDBUtil.LikeStr(PRODNO));
            //}
            //if (!string.IsNullOrEmpty(STATUS))
            //{
            //    sb.Append(" AND ORDER_M.STATUS = " + OracleDBUtil.SqlStr(STATUS));
            //}
            //if (!string.IsNullOrEmpty(zone))
            //{
            //    sb.Append(" AND Store.ZONE = " + OracleDBUtil.SqlStr(zone));
            //}
            //sb.Append("  GROUP by ORDER_D.PRODNO,ORDQTY,ATRQTY,A.PRODNAME,HQ_ADJ_ORDER_QTY,PRODNO_M ,B.PRODNAME");

            //sb.Append(" union ");
            //sb.Append("SELECT PRODNO_M, B.PRODNAME PRODNAME_M ,ORDER_D.PRODNO ,A.PRODNAME, ");
            //sb.Append("        sum(ORDQTY)  ORDQTY, ");
            //sb.Append("        ATRQTY  STOCK_QTY, ");
            //sb.Append("        sum(HQ_ADJ_ORDER_QTY)  HQ_QTY ");
            //sb.Append(" FROM   ORDER_M, ORDER_D, STORE, Product A, Product B, pos_store_atr  ");
            //sb.Append(" WHERE ORDER_M.ORDER_ID = ORDER_D.ORDER_ID(+) AND ORDER_M.STORE_NO = STORE.STORE_NO(+) AND ORDER_D.PRODNO = A.PRODNO(+) ");
            //sb.Append(" AND ORDER_D.PRODNO_M = B.PRODNO(+) AND A.PRODNO = pos_store_atr.PRODNO AND STORE.STORE_NO = POS_STORE_ATR.STORE_NO (+) ");
            //sb.Append(" AND To_char(ATR_DATE,'yyyy/mm/dd') ='" + DateTime.Now.ToString("yyyy/MM/dd") + "' and order_type = '1'   and GIFT_FALG='1'   ");

            //if (!string.IsNullOrEmpty(ORDDate) && !string.IsNullOrEmpty(ORDDate1))
            //{
            //    ORDDate = ORDDate.Replace("/", "");
            //    ORDDate1 = ORDDate1.Replace("/", "");
            //    sb.Append(" AND ORDDATE between " + OracleDBUtil.SqlStr(ORDDate));
            //    sb.Append(" AND  " + OracleDBUtil.SqlStr(ORDDate1));
            //}
            //if (!string.IsNullOrEmpty(SNO) && !string.IsNullOrEmpty(ENO))
            //{
            //    sb.Append(" AND ORDER_NO between " + OracleDBUtil.SqlStr(SNO));
            //    sb.Append(" AND " + OracleDBUtil.SqlStr(ENO));
            //}
            //if (!string.IsNullOrEmpty(SNO) && string.IsNullOrEmpty(ENO))
            //{
            //    sb.Append(" AND ORDER_NO >= " + OracleDBUtil.SqlStr(SNO));
            //}
            //if (string.IsNullOrEmpty(SNO) && !string.IsNullOrEmpty(ENO))
            //{
            //    sb.Append(" AND ORDER_NO <= " + OracleDBUtil.SqlStr(ENO));
            //}
            //if (!string.IsNullOrEmpty(ORDER_ITEM_ID))
            //{
            //    sb.Append(" AND ORDER_ITEMS_ID = " + OracleDBUtil.SqlStr(ORDER_ITEM_ID));
            //}
            //if (!string.IsNullOrEmpty(StoreNO))
            //{
            //    sb.Append(" AND ORDER_M.STORE_NO like " + OracleDBUtil.LikeStr(StoreNO));
            //}
            //if (!string.IsNullOrEmpty(PRODNO))
            //{
            //    sb.Append(" AND ORDER_D.PRODNO like " + OracleDBUtil.LikeStr(PRODNO));
            //}
            //if (!string.IsNullOrEmpty(STATUS))
            //{
            //    sb.Append(" AND ORDER_M.STATUS = " + OracleDBUtil.SqlStr(STATUS));
            //}
            //if (!string.IsNullOrEmpty(zone))
            //{
            //    sb.Append(" AND Store.ZONE = " + OracleDBUtil.SqlStr(zone));
            //}
            //sb.Append("  GROUP by ORDER_D.PRODNO,ORDQTY,ATRQTY,A.PRODNAME,HQ_ADJ_ORDER_QTY,PRODNO_M ,B.PRODNAME");
            ////sb.Append(" union ");
            ////sb.Append(" SELECT PRODNO_M, B.PRODNAME PRODNAME_M ,ORDER_D.PRODNO ,A.PRODNAME ,  ");
            ////sb.Append("        sum(ORDQTY)  ORDQTY, ");
            ////sb.Append("        ATRQTY  STOCK_QTY, ");
            ////sb.Append("        sum(HQ_ADJ_ORDER_QTY)  HQ_QTY ");
            ////sb.Append(" FROM  ORDER_M, ORDER_D, STORE, Product A,  Product B, LOC, POS_ATR  ");
            ////sb.Append(" WHERE ORDER_M.ORDER_ID = ORDER_D.ORDER_ID(+) AND ORDER_M.STORE_NO = STORE.STORE_NO(+) AND ORDER_D.PRODNO = A.PRODNO(+) AND ORDER_D.PRODNO_M = B.PRODNO(+) ");
            ////sb.Append(" AND  A.PRODNO = POS_ATR.PRODNO AND LOC.LOC_ID = POS_ATR.LOC_ID ");
            ////sb.Append(" AND To_char(POS_ATR.DWNDATE,'yyyy/mm/dd') = '" + DateTime.Now.ToString("yyyy/MM/dd") + "' and order_type = '1' and GIFT_FALG='1'  ");

            #endregion

            DataTable dtAll = new DataTable();
            DataTable dt = new DataTable();
            dtAll = TopDatatable2(SNO, ENO, StoreNO, ORDDate, ORDDate1, PRODNO, STATUS, zone);
            if (dtAll.Rows.Count > 0)
            {
                DataView dv = dtAll.DefaultView;
                dv.RowFilter = "PRODNO_M = '" + PRODNO_M + "'";
                dt = dv.ToTable();
            }
            
            return dt;

        }
    }
}
