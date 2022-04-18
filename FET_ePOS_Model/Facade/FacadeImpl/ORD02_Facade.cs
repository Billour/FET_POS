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
    public class ORD02_Facade
    {
        /// <summary>
        /// 查詢訂單
        /// </summary>
        /// <param name="sDate"></param>
        /// <param name="eDate"></param>
        /// <param name="prodNo"></param>
        /// <returns></returns>
        public DataTable Query_StoreOrders(string sDate, string eDate, string prodNo, string STORENO, bool isHQ)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(
                          @"SELECT DISTINCT v.ORDDATE, v.ORDER_NO, v.PRE_ORDER_NO, v.STATUS, v.MODI_USER, NVL(e.EMPNAME, 'Convert') EMPNAME
                                ,  v.MODI_DTM, v.ORDER_ID, v.PRE_ORDER_M_ID , v.ORDER_TYPE
                                FROM VW_STORE_ORDERS v left join EMPLOYEE e on  v.MODI_USER = e.EMPNO WHERE 1=1"
                      );

            if (!string.IsNullOrEmpty(sDate))
            {
                sb.Append(" AND v.ORDDATE>=" + OracleDBUtil.SqlStr(sDate));
            }

            if (!string.IsNullOrEmpty(eDate))
            {
                sb.Append(" AND v.ORDDATE<=" + OracleDBUtil.SqlStr(eDate));
            }

            if (!string.IsNullOrEmpty(prodNo))
            {
                sb.Append(" AND v.PRODNO LIKE " + OracleDBUtil.LikeStr(prodNo));
            }

            //總部人員可查詢各門市資料, 門市人員只能查詢自己的門市
            if (!isHQ)
            {
                sb.Append(" AND v.STORE_NO=" + OracleDBUtil.SqlStr(STORENO));
            }

            sb.Append(" ORDER BY TO_DATE(ORDDATE,'YYYY/MM/DD'), ORDER_NO ");

            DataTable dt = OracleDBUtil.Query_Data(sb.ToString());
            return dt;
        }

        public string GetPRE_PRODNO_M(string ID, string STORE_NO, string PRODNO)
        {
            string str = "0";
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT PRODNO_M ");
            sb.Append("FROM   PRE_ORDER_D,PRE_ORDER_M ");
            sb.Append(" WHERE PRE_ORDER_M.PRE_ORDER_M_ID = PRE_ORDER_D.PRE_ORDER_M_ID  and PRE_ORDER_D.PRE_ORDER_M_ID = " + OracleDBUtil.SqlStr(ID) + " and STORE_NO = " + OracleDBUtil.SqlStr(STORE_NO) + " ");
            sb.Append(" and PRE_ORDER_D.PRODNO= " + OracleDBUtil.SqlStr(PRODNO) + "");
            // strsql += " and PRE_ORDER_M.PRE_ORDER_M_ID = PRE_ORDER_D.PRE_ORDER_M_ID  and PRE_ORDER_D.PRE_ORDER_M_ID = " + OracleDBUtil.SqlStr(REALID) + " and PRE_ORDER_D.PRODNO= " + OracleDBUtil.SqlStr(dr["PRODNO"].ToString());
            // strsql += " and STORE_NO = " + OracleDBUtil.SqlStr(dr["STORE_NO"].ToString());

            DataTable dt = OracleDBUtil.Query_Data(sb.ToString());

            foreach (DataRow dr in dt.Rows)
            {
                str = dr["PRODNO_M"].ToString();
                if (str == "")
                {
                    str = "";
                }
            }

            return str;
        }

        public string GetPRODNO_M(string ID, string STORE_NO, string PRODNO)
        {
            string str = "0";
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT PRODNO_M ");
            sb.Append("FROM   ORDER_D,ORDER_M ");
            sb.Append(" WHERE ORDER_M.ORDER_ID = ORDER_D.ORDER_ID and ORDER_D.ORDER_ID = " + OracleDBUtil.SqlStr(ID) + " and ORDER_M.STORE_NO = " + OracleDBUtil.SqlStr(STORE_NO) + " ");
            sb.Append(" AND ORDER_D.PRODNO = " + OracleDBUtil.SqlStr(PRODNO) + "");

            DataTable dt = OracleDBUtil.Query_Data(sb.ToString());

            foreach (DataRow dr in dt.Rows)
            {
                str = dr["PRODNO_M"].ToString();
                if (str == "")
                {
                    str = "";
                }
            }
            return str;
        }

        /// <summary>
        /// 匯出查詢訂單
        /// </summary>
        /// <param name="sDate"></param>
        /// <param name="eDate"></param>
        /// <param name="prodNo"></param>
        /// <returns></returns>
        /*
        public DataTable Export_StoreOrders(string sDate, string eDate, string prodNo, string STORENO, bool isHQ)
        {
            OracleConnection objConn = null;
            DataTable dt;

            try
            {
                StringBuilder sb = new StringBuilder();

                sb.Append(
                              @"select * 
                                FROM VW_STORE_ORDERS v left join EMPLOYEE e on  v.MODI_USER = e.EMPNO WHERE 1=1"
                          );
               
                if (!string.IsNullOrEmpty(sDate))
                {
                    sb.Append(" AND v.ORDDATE>=" + OracleDBUtil.SqlStr(sDate));
                }

                if (!string.IsNullOrEmpty(eDate))
                {
                    sb.Append(" AND v.ORDDATE<=" + OracleDBUtil.SqlStr(eDate));
                }

                if (!string.IsNullOrEmpty(prodNo))
                {
                    sb.Append(" AND v.PRODNO LIKE " + OracleDBUtil.LikeStr(prodNo));
                }

                //總部人員可查詢各門市資料, 門市人員只能查詢自己的門市
                if (!isHQ)
                {
                    sb.Append(" AND v.STORE_NO=" + OracleDBUtil.SqlStr(STORENO));
                }

                sb.Append(" ORDER BY TO_DATE(ORDDATE,'YYYY/MM/DD'), ORDER_NO ");

                objConn = OracleDBUtil.GetConnection();
                dt = OracleDBUtil.GetDataSet(objConn, sb.ToString()).Tables[0];

                DataTable dtt = new DataTable();
                dtt.Columns.Add("訂單日期");
                dtt.Columns.Add("訂單編號");
                dtt.Columns.Add("預訂單號");
                dtt.Columns.Add("狀態");
                dtt.Columns.Add("更新人員");
                dtt.Columns.Add("更新日期");
                dtt.Columns.Add("商品料號");
                dtt.Columns.Add("商品名稱");
                dtt.Columns.Add("訂購量");
                dtt.Columns.Add("總部核准量");
                dtt.Columns.Add("驗收量");
                dtt.Columns.Add("搭配商品料號");
                dtt.Columns.Add("搭配商品名稱");
                dtt.Columns.Add("訂購量 ");
                dtt.Columns.Add("總部核准量 ");
                dtt.Columns.Add("驗收量 ");

                foreach (DataRow dr in dt.Rows)
                {
                    string REALID = "";
                    string strsql = "";
                    string ISPRE = "";
                    DataTable tempdt = new DataTable();
                    string MID = "";
                    if (string.IsNullOrEmpty(dr["PRE_ORDER_M_ID"].ToString()))
                    {
                        REALID = dr["ORDER_ID"].ToString();
                        ISPRE = "0";
                        MID = GetPRODNO_M(dr["ORDER_ID"].ToString(), dr["STORE_NO"].ToString(), dr["PRODNO"].ToString());
                       
                        strsql = " select ORDER_D.PRODNO,PRODNAME,NVL(ORDQTY,0) ORDQTY,NVL(APPROVEQTY,0) APPROVEQTY,NVL(CHECK_IN_QTY,0) CHECK_IN_QTY from ORDER_D,PRODUCT,ORDER_M where 1=1 and ORDER_D.PRODNO_M = PRODUCT.PRODNO ";
                        strsql += " and ORDER_M.ORDER_ID = ORDER_D.ORDER_ID and ORDER_D.ORDER_ID = " + OracleDBUtil.SqlStr(REALID) + " and ORDER_D.PRODNO= " + OracleDBUtil.SqlStr(MID);
                        strsql += " and STORE_NO = " + OracleDBUtil.SqlStr(dr["STORE_NO"].ToString());

                    }
                    else
                    {
                        ISPRE = "1";
                        REALID = dr["PRE_ORDER_M_ID"].ToString();
                        MID = GetPRE_PRODNO_M(REALID, dr["STORE_NO"].ToString(), dr["PRODNO"].ToString());
                        strsql = " select  PRE_ORDER_D.PRODNO,PRODNAME,NVL(ORDQTY,0) ORDQTY,NVL(ES_QTY,0) ES_QTY,NVL(APPROVEQTY,0) APPROVEQTY from PRE_ORDER_D,PRODUCT,PRE_ORDER_M where 1=1  and PRE_ORDER_D.PRODNO = PRODUCT.PRODNO  ";
                        strsql += " and PRE_ORDER_M.PRE_ORDER_M_ID = PRE_ORDER_D.PRE_ORDER_M_ID  and PRE_ORDER_D.PRE_ORDER_M_ID = " + OracleDBUtil.SqlStr(REALID) + " and PRE_ORDER_D.PRODNO= " + OracleDBUtil.SqlStr(MID);
                        strsql += " and STORE_NO = " + OracleDBUtil.SqlStr(dr["STORE_NO"].ToString());
                    }
                    DataRow drr = dtt.NewRow();
                    string PRODNO_M = "";
                    string PRODNAME_M = "";
                    string ORDQTY = "";
                    string APPROVEQTY = "";
                    string ES_QTY = "";
                    string CHECK_IN_QTY = "0";
                    if (MID != "")
                    {
                        tempdt = OracleDBUtil.GetDataSet(objConn, strsql).Tables[0];
                       
                        foreach (DataRow dv in tempdt.Rows)
                        {
                            if (ISPRE == "0")
                            {
                                PRODNO_M = dv["PRODNO_M"].ToString();
                                PRODNAME_M = dv["PRODNAME"].ToString();
                                ORDQTY = dv["ORDQTY"].ToString();
                                APPROVEQTY = dv["APPROVEQTY"].ToString();
                                CHECK_IN_QTY = dv["CHECK_IN_QTY"].ToString();

                            }
                            else
                            {
                                PRODNO_M = dv["PRODNO"].ToString();
                                PRODNAME_M = dv["PRODNAME"].ToString();
                                ORDQTY = dv["ORDQTY"].ToString();
                                APPROVEQTY = dv["APPROVEQTY"].ToString();
                                CHECK_IN_QTY = dv["ES_QTY"].ToString();
                            }

                        }
                    }
                    //dtt.Columns.Add("訂單日期");0
                    //dtt.Columns.Add("訂單編號");1
                    //dtt.Columns.Add("預訂單編號");2
                    //dtt.Columns.Add("狀態");3
                    //dtt.Columns.Add("更新人員");4
                    //dtt.Columns.Add("更新日期");5
                    //dtt.Columns.Add("商品料號");6
                    //dtt.Columns.Add("商品名稱");7
                    //dtt.Columns.Add("訂購量");8
                    //dtt.Columns.Add("總部核准量");9
                    //dtt.Columns.Add("驗收量");10
                    //dtt.Columns.Add("搭配商品料號");11
                    //dtt.Columns.Add("搭配商品名稱");12
                    //dtt.Columns.Add("訂購量");13
                    //dtt.Columns.Add("總部核准量");14
                    //dtt.Columns.Add("驗收量");15

                    drr[0] = dr["ORDDATE"].ToString();
                    drr[1] = dr["ORDER_NO"].ToString();
                    if (ISPRE == "1")
                    {
                        drr[2] = dr["PRE_ORDER_NO"].ToString();
                        drr[15] = "0";
                        drr[10] = "0";
                    }
                    else
                    {
                        drr[2] = "";
                        drr[15] = CHECK_IN_QTY;
                        drr[10] = dr["CHECK_IN_QTY"].ToString();
                    }
                    drr[3] = dr["STATUS"].ToString();
                    drr[4] = dr["EMPNAME"].ToString() != "" ? dr["EMPNAME"].ToString() : "Convert";
                    drr[5] = dr["MODI_DTM"].ToString();
                    drr[6] = dr["PRODNO"].ToString();
                    drr[7] = dr["PRODNAME"].ToString();
                    drr[8] = dr["ORDQTY"].ToString();
                    drr[9] = dr["APPROVEQTY"].ToString();
                    drr[11] = PRODNO_M;
                    drr[12] = PRODNAME_M;
                    drr[13] = ORDQTY;
                    drr[14] = APPROVEQTY;

                    dtt.Rows.Add(drr);
                
                }

                return dtt;

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
                dt = null;
            }
        }
        */
        public DataTable Export_StoreOrders(string sDate, string eDate, string prodNo, string STORENO, bool isHQ)
        {
            //**2011/03/08 Tina：匯出時，主商品和搭贈商品要放在同一筆資料中

            #region "訂單資料"

            //單一商品查詢(訂單)
            StringBuilder sbColumns1 = new StringBuilder();
            sbColumns1.AppendLine(@"SELECT
                                    TO_CHAR(TO_DATE(M.ORDDATE, 'yyyy/MM/dd'), 'yyyy/MM/dd') AS ORDDATE
                                    , M.ORDER_NO
                                    , PM.PRE_ORDER_NO
                                    , DECODE (M.STATUS,
                                                      '10', '正式',
                                                      '11', '預訂',
                                                      '50', '己傳輸',
                                                      '51', '已成單',
                                                      '70', '已刪除',
                                                      '80', '未驗收',
                                                      '85', '部份驗收',
                                                      '90', '己結案') STATUS 
                                    , M.MODI_USER
                                    , TO_CHAR(M.MODI_DTM, 'yyyy/mm/dd hh24:mi:ss') MODI_DTM
                                    , D.PRODNO as PRODNO_M
                                    , A.PRODNAME PRODNAME_M
                                    , D.ORDQTY        ORDQTY_M
                                    , D.APPROVEQTY    APPROVEQTY_M
                                    , D.CHECK_IN_QTY  CHECK_IN_QTY_M  
                                    , D.PRODNO_M as PRODNO
                                    , B.PRODNAME
                                    , NULL  ORDQTY_D
                                    , NULL  APPROVEQTY_D
                                    , NULL  CHECK_IN_QTY_D  
                                    , M.STORE_NO 
                                    , S.STORENAME
                                    ");


            //有搭贈商品的主商品查詢(訂單)
            StringBuilder sbColumns2 = new StringBuilder();
            sbColumns2.AppendLine(@"SELECT 
                                    TO_CHAR(TO_DATE(M.ORDDATE, 'yyyy/MM/dd'), 'yyyy/MM/dd') AS ORDDATE
                                    , M.ORDER_NO
                                    , PM.PRE_ORDER_NO
                                    , DECODE (M.STATUS,
                                                      '10', '正式',
                                                      '11', '預訂',
                                                      '50', '己傳輸',
                                                      '51', '已成單',
                                                      '70', '已刪除',
                                                      '80', '未驗收',
                                                      '85', '部份驗收',
                                                      '90', '己結案') STATUS
                                    , M.MODI_USER
                                    , TO_CHAR(M.MODI_DTM, 'yyyy/mm/dd hh24:mi:ss') MODI_DTM
                                    , D.PRODNO_M
                                    , B.PRODNAME PRODNAME_M
                                    , (SELECT ORDQTY FROM ORDER_D WHERE PRODNO = D.PRODNO_M AND GIFT_FALG='0' AND ORDER_ID =  D.ORDER_ID)  ORDQTY_M
                                    , (SELECT APPROVEQTY FROM ORDER_D WHERE PRODNO = D.PRODNO_M AND GIFT_FALG='0' AND ORDER_ID =  D.ORDER_ID)  APPROVEQTY_M
                                    , (SELECT CHECK_IN_QTY FROM ORDER_D WHERE PRODNO = D.PRODNO_M AND GIFT_FALG='0' AND ORDER_ID =  D.ORDER_ID)  CHECK_IN_QTY_M                             
                                    , D.PRODNO 
                                    , A.PRODNAME
                                    , D.ORDQTY        ORDQTY_D
                                    , D.APPROVEQTY    APPROVEQTY_D
                                    , D.CHECK_IN_QTY  CHECK_IN_QTY_D  
                                    , M.STORE_NO 
                                    , S.STORENAME
                                    ");

            //Join的Tables
            StringBuilder sbFrom1 = new StringBuilder();
            sbFrom1.AppendLine(@"FROM  ORDER_M M, ORDER_D D, PRE_ORDER_M PM, Product A,  Product B,STORE S
                                WHERE M.ORDER_ID = D.ORDER_ID(+) 
                                  AND M.PRE_ORDER_M_ID = PM.PRE_ORDER_M_ID(+)
                                  AND D.PRODNO = A.PRODNO(+)
                                  AND D.PRODNO_M = B.PRODNO(+)
                                  AND M.STORE_NO  = S.STORE_NO (+)
                                  --AND ORDER_TYPE = '1' ");  //門市訂單類型 1: 門市自訂
            #endregion

            #region "預訂單資料"
            StringBuilder sbColumns3 = new StringBuilder();
            //單一商品查詢(預訂單)
            sbColumns3.AppendLine(@"SELECT
                                    TO_CHAR(TO_DATE(M.ORDDATE, 'yyyy/MM/dd'), 'yyyy/MM/dd') AS ORDDATE
                                    , '' ORDER_NO
                                    , M.PRE_ORDER_NO
                                    , DECODE (M.STATUS,
                                                      '10', '正式',
                                                      '11', '預訂',
                                                      '50', '己傳輸',
                                                      '51', '已成單',
                                                      '70', '已刪除',
                                                      '80', '未驗收',
                                                      '85', '部份驗收',
                                                      '90', '己結案') STATUS 
                                    , M.MODI_USER
                                    , TO_CHAR(M.MODI_DTM, 'yyyy/mm/dd hh24:mi:ss') MODI_DTM
                                    , D.PRODNO as PRODNO_M
                                    , A.PRODNAME PRODNAME_M
                                    , D.ORDQTY        ORDQTY_M
                                    , D.APPROVEQTY    APPROVEQTY_M
                                    , NULL  CHECK_IN_QTY_M                   
                                    , D.PRODNO_M as PRODNO
                                    , B.PRODNAME 
                                    , NULL ORDQTY_D
                                    , NULL APPROVEQTY_D
                                    , NULL CHECK_IN_QTY_D                   
                                    , M.STORE_NO
                                    , S.STORENAME 
                                    ");

            //有搭贈商品的主商品查詢(預訂單)
            StringBuilder sbColumns4 = new StringBuilder();
            sbColumns4.AppendLine(@"SELECT 
                                    TO_CHAR(TO_DATE(M.ORDDATE, 'yyyy/MM/dd'), 'yyyy/MM/dd') AS ORDDATE
                                    , '' ORDER_NO
                                    , M.PRE_ORDER_NO
                                    , DECODE (M.STATUS,
                                                      '10', '正式',
                                                      '11', '預訂',
                                                      '50', '己傳輸',
                                                      '51', '已成單',
                                                      '70', '已刪除',
                                                      '80', '未驗收',
                                                      '85', '部份驗收',
                                                      '90', '己結案') STATUS
                                    , M.MODI_USER
                                    , TO_CHAR(M.MODI_DTM, 'yyyy/mm/dd hh24:mi:ss') MODI_DTM
                                    , D.PRODNO_M
                                    , B.PRODNAME PRODNAME_M
                                    , (SELECT ORDQTY FROM PRE_ORDER_D WHERE PRODNO = D.PRODNO_M AND GIFT_FLAG = '0' AND PRE_ORDER_M_ID =  D.PRE_ORDER_M_ID)     ORDQTY_M
                                    , (SELECT APPROVEQTY FROM PRE_ORDER_D WHERE PRODNO = D.PRODNO_M AND GIFT_FLAG = '0' AND PRE_ORDER_M_ID =  D.PRE_ORDER_M_ID) APPROVEQTY_M
                                    , NULL CHECK_IN_QTY_M                                  
                                    , D.PRODNO 
                                    , A.PRODNAME
                                    , D.ORDQTY        ORDQTY_D
                                    , D.APPROVEQTY    APPROVEQTY_D
                                    , NULL  CHECK_IN_QTY_D                  
                                    , M.STORE_NO 
                                    , S.STORENAME
                                    ");

            //Join的Tables
            StringBuilder sbFrom2 = new StringBuilder();
            sbFrom2.AppendLine(@"FROM  PRE_ORDER_M M, PRE_ORDER_D D, Product A,  Product B,STORE S
                                WHERE M.PRE_ORDER_M_ID = D.PRE_ORDER_M_ID(+) 
                                  AND D.PRODNO = A.PRODNO(+)
                                  AND D.PRODNO_M = B.PRODNO(+)
                                  AND M.STORE_NO  = S.STORE_NO (+)
                                  --AND NVL(M.ORDER_ID,' ') = ' '
                                ");

            #endregion

            //查詢條件
            StringBuilder sbWhere = new StringBuilder();
            if (!string.IsNullOrEmpty(sDate))
            {
                sbWhere.AppendLine(" AND TO_CHAR(TO_DATE(ORDDATE, 'yyyy/MM/dd'), 'yyyy/MM/dd') >= " + OracleDBUtil.SqlStr(sDate));
            }

            if (!string.IsNullOrEmpty(eDate))
            {
                sbWhere.AppendLine(" AND TO_CHAR(TO_DATE(ORDDATE, 'yyyy/MM/dd'), 'yyyy/MM/dd') <= " + OracleDBUtil.SqlStr(eDate));
            }

            if (!string.IsNullOrEmpty(prodNo))
            {
                sbWhere.AppendLine(" AND PRODNO_M LIKE " + OracleDBUtil.LikeStr(prodNo));
            }

            //總部人員可查詢各門市資料, 門市人員只能查詢自己的門市
            if (!isHQ)
            {
                sbWhere.AppendLine(" AND STORE_NO=" + OracleDBUtil.SqlStr(STORENO));
            }


            StringBuilder sb = new StringBuilder();
            sb.AppendLine(@" SELECT 
                                ORDDATE             AS 訂單日期
                                , ORDER_NO          AS 訂單編號
                                , PRE_ORDER_NO      AS 預訂單號
                                , STORE_NO          AS 訂單門市
                                , STORENAME         AS 訂單門市名稱
                                , STATUS            AS 狀態
                                , MODI_USER         AS 更新人員
                                , MODI_DTM          AS 更新日期
                                , PRODNO_M          AS 商品料號
                                , PRODNAME_M        AS 商品名稱
                                , ORDQTY_M          AS 訂購量
                                , APPROVEQTY_M      AS 總部核准量
                                , CHECK_IN_QTY_M    AS 驗收量              
                                , PRODNO            AS 搭配商品料號
                                , PRODNAME          AS 搭配商品名稱
                                , ORDQTY_D          AS "" 訂購量""
                                , APPROVEQTY_D      AS "" 總部核准量""
                                , CHECK_IN_QTY_D    AS "" 驗收量""
                            FROM (");
            sb.AppendLine(sbColumns1.ToString() + sbFrom1.ToString() + @" AND GIFT_FALG='0' 
                    AND D.PRODNO NOT IN (SELECT DISTINCT NVL(prodno_m,'NULL DATA') " + sbFrom1.ToString() + ")");
            sb.AppendLine("union");
            sb.AppendLine(sbColumns2.ToString() + sbFrom1.ToString() + " AND GIFT_FALG='1' ");
            sb.AppendLine("union");
            sb.AppendLine(sbColumns3.ToString() + sbFrom2.ToString() + @" AND GIFT_FLAG='0' 
                    AND D.PRODNO NOT IN (SELECT DISTINCT NVL(prodno_m,'NULL DATA') " + sbFrom2.ToString() + ")");
            sb.AppendLine("union");
            sb.AppendLine(sbColumns4.ToString() + sbFrom2.ToString() + " AND GIFT_FLAG='1' ");
            sb.AppendLine(" ) t");
            sb.AppendLine(" WHERE 1= 1");
            sb.AppendLine(sbWhere.ToString());

            DataTable dt = OracleDBUtil.Query_Data(sb.ToString());
            return dt;
        }


        /// <summary>
        /// 以 OrderMId 取得訂單細項
        /// </summary>
        /// <param name="OrderMId"></param>
        /// <returns></returns>
        public DataTable GetOrderDetailsBy(string OrderMId)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(
                          @"SELECT *
                                FROM 
                                   (
                                    SELECT 
                                       APPROVEQTY, OENO, INCOUNTQTY, 
                                       INWAYQTY, DIFFQTY, REASON, 
                                       ADVISEQTY, ORDQTY, REMARK, 
                                       ULSNO, PO_NO, PRODNO_M, 
                                       QTY_BDATE, QTY_EDATE, CREATE_USER, 
                                       CREATE_DTM, MODI_USER, SEQNO, 
                                       PRODNO, GIFT_FALG, MODI_DTM, 
                                       DS_FLAG, CHECK_IN_QTY, STOCK_QTY, 
                                       TODAY_ORDER_QTY, ES_QTY, SHIPCONFIRM_DTM, 
                                       ORDER_ID, ORDER_ITEMS_ID, PRE_ORDER_D_ID, 
                                       HQ_ORDER_STORE, LOC_ID, HQ_ADJ_ORDER_QTY
                                    FROM ORDER_D
                                    UNION ALL
                                    SELECT 
                                       APPROVEQTY, OENO, INCOUNTQTY, 
                                       INWAYQTY, DIFFQTY, REASON, 
                                       ADVISEQTY, ORDQTY, REMARK, 
                                       ULSNO, PO_NO, PRODNO_M, 
                                       QTY_BDATE, QTY_EDATE, CREATE_USER, 
                                       CREATE_DTM, MODI_USER, SEQNO, 
                                       PRODNO, GIFT_FALG, MODI_DTM, 
                                       DS_FLAG, CHECK_IN_QTY, STOCK_QTY, 
                                       TODAY_ORDER_QTY, ES_QTY, SHIPCONFIRM_DTM, 
                                       ORDER_TEMP_ID ORDER_ID, ORDER_D_TEMP_ID ORDER_ITEMS_ID, PRE_ORDER_D_ID, 
                                       HQ_ORDER_STORE, LOC_ID, HQ_ADJ_ORDER_QTY
                                    FROM ORDER_D_TEMP
                                   )      
                                WHERE 1 = 1 
                                AND ORDER_ID = $PARAM_ORDER_ID"
                      );

            string sbStr = sb.ToString();
            sbStr = sbStr.Replace(@"$PARAM_ORDER_ID", OracleDBUtil.SqlStr(OrderMId));

            DataTable dt = OracleDBUtil.Query_Data(sb.ToString());
            return dt;

        }

        /// <summary>
        /// 以 PreOrderMId 取得預訂單細項
        /// </summary>
        /// <param name="PreOrderMId"></param>
        /// <returns></returns>
        public DataTable GetPreOrderDetailsBy(string PreOrderMId)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(
                          @"SELECT ROWNUM ITEMNO, POD.*
                                FROM PRE_ORDER_M POM
                                INNER JOIN PRE_ORDER_D POD
                                ON POM.PRE_ORDER_M_ID = POD.PRE_ORDER_M_ID
                                WHERE 1 = 1
                                AND PRE_ORDER_M_ID = $PARAM_PRE_ORDER_M_ID"
                      );

            string sbStr = sb.ToString();
            sbStr = sbStr.Replace(@"$PARAM_PRE_ORDER_M_ID", OracleDBUtil.SqlStr(PreOrderMId));

            DataTable dt = OracleDBUtil.Query_Data(sb.ToString());
            return dt;
        }

        /// <summary>
        /// 以OrderId取得訂單主檔
        /// </summary>
        /// <param name="OrderId"></param>
        /// <returns></returns>
        public DataTable GetOrderMTempBy(string OrderId)
        {
            StringBuilder sb = new StringBuilder();
            //sb.Append("SELECT ISOK, ");
            //sb.Append("       ORDER_TEMP_ID, ");
            //sb.Append("       AMOUNT, ");
            //sb.Append("       DIFFREASON, ");
            //sb.Append("       REMARK, ");
            //sb.Append("       ORDDATE, ");
            //sb.Append("       ERP_ORDER_HEADER_ID, ");
            //sb.Append("       ULSNO, ");
            //sb.Append("       CREATE_USER, ");
            //sb.Append("       CREATE_DTM, ");
            //sb.Append("       MODI_USER, ");
            //sb.Append("       MODI_DTM, ");
            //sb.Append("       ORDER_NO, ");
            //sb.Append("       ORDER_TYPE, ");
            //sb.Append("       PRE_ORDER_M_ID, ");
            //sb.Append("       STORE_NO, ");
            //sb.Append("       HQ_ORDER_M_ID ");
            //sb.Append(" FROM   ORDER_M_TEMP OMT");
            //sb.Append(" WHERE  1 = 1 ");
            //sb.Append("   AND OMT.ORDER_TYPE = '1' ");
            //sb.Append("   AND OMT.ORDER_TEMP_ID = " + OracleDBUtil.SqlStr(OrderId));

            sb.AppendLine(@"SELECT OMT.ISOK
                                   , OMT.ORDER_ID ORDER_TEMP_ID
                                   , OMT.ORDER_NO
                                   , OMT.ORDER_TYPE
                                   , OMT.STORE_NO
                                   , OMT.ORDER_TYPE
                                   , OMT.REMARK
                                   , OMT.ORDDATE
                                   , ORDQTY,APPROVEQTY,CHECK_IN_QTY
                                   , OMT.CREATE_USER
                                   , OMT.CREATE_DTM
                                   , OMT.MODI_USER 
                                   , OMT.MODI_DTM 
                                    FROM   ORDER_M OMT,ORDER_D D,PRODUCT P
                                    WHERE  1 = 1 
                                    AND OMT.ORDER_ID=D.ORDER_ID 
                                    AND D.PRODNO=P.PRODNO(+) 
                                    AND OMT.ORDER_TYPE = '1' 
                                    AND OMT.ORDER_TEMP_ID = " + OracleDBUtil.SqlStr(OrderId));

            DataTable dt = OracleDBUtil.Query_Data(sb.ToString());
            return dt;
        }

        /// <summary>
        /// 以OrderId取得訂單主檔
        /// </summary>
        /// <param name="OrderId"></param>
        /// <returns></returns>
        public DataTable GetOrderMTempBy1(string OrderId)
        {
            StringBuilder sb = new StringBuilder();
            //sb.Append("SELECT ISOK, ");
            //sb.Append("       ORDER_ID ORDER_TEMP_ID, ");
            //sb.Append("       AMOUNT, ");
            //sb.Append("       DIFFREASON, ");
            //sb.Append("       REMARK, ");
            //sb.Append("       ORDDATE, ");
            //sb.Append("       ERP_ORDER_HEADER_ID, ");
            //sb.Append("       ULSNO, ");
            //sb.Append("       CREATE_USER, ");
            //sb.Append("       CREATE_DTM, ");
            //sb.Append("       MODI_USER, ");
            //sb.Append("       MODI_DTM, ");
            //sb.Append("       ORDER_NO, ");
            //sb.Append("       ORDER_TYPE, ");
            //sb.Append("       PRE_ORDER_M_ID, ");
            //sb.Append("       STORE_NO, ");
            //sb.Append("       HQ_ORDER_M_ID, ");
            //sb.Append("       OMT.ORDER_TYPE ");
            //sb.Append(" FROM   ORDER_M OMT");
            //sb.Append(" WHERE  1 = 1 ");
            //sb.Append("   AND OMT.ORDER_ID = " + OracleDBUtil.SqlStr(OrderId));

            sb.AppendLine(@"SELECT OMT.ISOK
                                   , OMT.ORDER_ID ORDER_TEMP_ID 
                                   , OMT.ORDER_NO
                                   , OMT.ORDER_TYPE
                                   , OMT.STORE_NO
                                   , OMT.ORDER_TYPE
                                   , OMT.REMARK
                                   , OMT.ORDDATE
                                   , ORDQTY,APPROVEQTY,CHECK_IN_QTY
                                   , OMT.CREATE_USER
                                   , OMT.CREATE_DTM 
                                   , OMT.MODI_USER
                                   , OMT.MODI_DTM 
                                    FROM   ORDER_M OMT,ORDER_D D,PRODUCT P
                                    WHERE  1 = 1 
                                    AND OMT.ORDER_ID=D.ORDER_ID 
                                    AND D.PRODNO=P.PRODNO(+) 
                                    AND OMT.ORDER_ID = " + OracleDBUtil.SqlStr(OrderId));

            DataTable dt = OracleDBUtil.Query_Data(sb.ToString());
            return dt;
        }


        public DataTable GetOrderMDTempBy(string OrderId, string STORENO)
        {
            StringBuilder sb = new StringBuilder();
            //sb.Append("SELECT ODT.SEQNO ITEMNO, ");
            //sb.Append("       PROD.PRODNAME, ");
            //sb.Append("       PROD.PRODNO, ");
            //sb.Append("       ODT.SEQNO, ");
            //sb.Append("       OMT.STORE_NO, ");
            //sb.Append("       OMT.ORDER_TEMP_ID, ");
            //sb.Append("       ODT.ORDER_D_TEMP_ID, ");
            //sb.Append("       ODT.ADVISEQTY, ");
            //sb.Append("       ODT.ORDQTY, ");
            //sb.Append("       ODT.ES_QTY, ");
            //sb.Append("       ODT.STOCK_QTY, ");
            //sb.Append("       nvl((SELECT INWAY_QTY('" + STORENO + "',PROD.PRODNO) FROM dual),0)   INWAYQTY, ");
            //sb.Append("       ODT.TODAY_ORDER_QTY, ");
            //sb.Append("       nvl((SELECT INORDER_QTY('" + STORENO + "',PROD.PRODNO) FROM dual),0) CHECK_IN_QTY ");
            //sb.Append("FROM   ORDER_M_TEMP OMT,ORDER_D_TEMP ODT,STORE ST, PRODUCT PROD ");
            //sb.Append("WHERE  OMT.ORDER_TEMP_ID = ODT.ORDER_TEMP_ID AND OMT.STORE_NO = ST.STORE_NO(+) AND ODT.PRODNO = PROD.PRODNO(+) ");
            //sb.Append("       AND OMT.ORDER_TYPE = '1' ");
            //sb.Append("       AND OMT.ORDER_TEMP_ID = " + OracleDBUtil.SqlStr(OrderId));
            //sb.Append("       Order by to_number(ODT.SEQNO) ");


            sb.Append("SELECT rownum ITEMNO, ");
            sb.Append("       PROD.PRODNAME, ");
            sb.Append("       PROD.PRODNO, ");
            sb.Append("       ODT.SEQNO, ");
            sb.Append("       OMT.STORE_NO, ");
            sb.Append("       OMT.ORDER_TEMP_ID, ");
            sb.Append("       ODT.ORDER_D_TEMP_ID, ");
            sb.Append("       nvl(ORDQTY,'0') ORDQTY, ");
            sb.Append("       nvl(REAL_ATR_QTY,'0') APPROVEQTY, ");
            sb.Append("       nvl(CHECK_IN_QTY,'0') CHECK_IN_QTY ");    
            sb.Append("FROM   ORDER_M_TEMP OMT,ORDER_D_TEMP ODT,STORE ST, PRODUCT PROD ");
            sb.Append("WHERE  OMT.ORDER_TEMP_ID = ODT.ORDER_TEMP_ID AND OMT.STORE_NO = ST.STORE_NO(+) AND ODT.PRODNO = PROD.PRODNO(+) ");
            sb.Append("       AND OMT.ORDER_TYPE = '1' AND ODT.PRODNO_M is null ");
            sb.Append("       AND OMT.ORDER_TEMP_ID = " + OracleDBUtil.SqlStr(OrderId));
            sb.Append("       Order by to_number(ODT.SEQNO) ");

            DataTable dt = OracleDBUtil.Query_Data(sb.ToString());
            return dt;
        }

        public DataTable GetOrderMDTempBy1(string OrderId, string STORENO)
        {
            StringBuilder sb = new StringBuilder();
            //sb.Append("SELECT ODT.SEQNO ITEMNO, ");
            //sb.Append("       PROD.PRODNAME, ");
            //sb.Append("       PROD.PRODNO, ");
            //sb.Append("       ODT.SEQNO, ");
            //sb.Append("       OMT.STORE_NO, ");
            //sb.Append("       OMT.ORDER_ID ORDER_TEMP_ID, ");
            //sb.Append("       ODT.ORDER_ITEMS_ID ORDER_D_TEMP_ID, ");
            //sb.Append("       ODT.ADVISEQTY, ");
            //sb.Append("       ODT.ORDQTY, ");
            //sb.Append("       ODT.ES_QTY, ");
            //sb.Append("       ODT.STOCK_QTY, ");
            //sb.Append("       nvl((SELECT INWAY_QTY('" + STORENO + "',PROD.PRODNO) FROM dual),0)  INWAYQTY, ");
            //sb.Append("       ODT.TODAY_ORDER_QTY, ");
            //sb.Append("       nvl((SELECT INORDER_QTY('" + STORENO + "',PROD.PRODNO) FROM dual),0)  CHECK_IN_QTY ");
            //sb.Append("FROM   ORDER_M OMT, ORDER_D ODT, STORE ST, PRODUCT PROD ");
            //sb.Append("WHERE  OMT.ORDER_ID = ODT.ORDER_ID AND OMT.STORE_NO = ST.STORE_NO(+) AND ODT.PRODNO = PROD.PRODNO(+) ");
            //sb.Append("  AND   ODT.ADVISEQTY > 0 ");
            //sb.Append("  AND  OMT.ORDER_ID = " + OracleDBUtil.SqlStr(OrderId));
            //sb.Append("       Order by to_number(ODT.SEQNO) ");

            sb.AppendLine(@"SELECT rownum ITEMNO, 
                                    PROD.PRODNAME,
                                    PROD.PRODNO,
                                    ODT.SEQNO, 
                                    OMT.STORE_NO, 
                                    OMT.ORDER_ID ORDER_TEMP_ID, 
                                    ODT.ORDER_ITEMS_ID ORDER_D_TEMP_ID, 
                                    nvl(ORDQTY,'0') ORDQTY,
                                    nvl(REAL_ATR_QTY,'0') APPROVEQTY,
                                    --nvl(APPROVEQTY,'0') APPROVEQTY, 
                                    nvl(CHECK_IN_QTY,'0') CHECK_IN_QTY 
                            FROM   ORDER_M OMT, ORDER_D ODT, STORE ST, PRODUCT PROD 
                            WHERE  OMT.ORDER_ID = ODT.ORDER_ID AND OMT.STORE_NO = ST.STORE_NO(+) AND ODT.PRODNO = PROD.PRODNO(+) 
                            AND   (ODT.ADVISEQTY > 0 or OMT.ORDER_TYPE in ('2','3','5')) AND ODT.PRODNO_M is null
                            AND  OMT.ORDER_ID = " + OracleDBUtil.SqlStr(OrderId) + @"
                            Order by to_number(ODT.SEQNO) "
                            );

            DataTable dt = OracleDBUtil.Query_Data(sb.ToString());
            return dt;
        }

        /// <summary>
        /// 取得搭贈商品資料
        /// </summary>
        /// <param name="OneToOneSID">搭贈主檔SID</param>
        /// <returns></returns>
        public DataTable GetGiftProducts(string ProductNo, string WorkDate, string detail_ORDQTY, string detail_APPROVEQTY, string detail_CHECK_IN_QTY)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("select A.SID,B.PRODNO,B.PRODNAME,'" + detail_ORDQTY + "' as ORDQTY,'" + detail_APPROVEQTY + "' as APPROVEQTY,'" + detail_CHECK_IN_QTY + "' as CHECK_IN_QTY,'1' as REAL_QTY ");
            sb.Append("from ONETOONE_D A ");
            sb.Append("join PRODUCT B ");
            sb.Append("on A.PRODNO=B.PRODNO ");
            sb.Append("where A.SID in ");
            sb.Append(" (select M.SID ");
            sb.Append("  FROM ONETOONE_M M, ONETOONE_D D ,PRODUCT PROD,PRODUCT PROD1 ");
            sb.Append(" WHERE M.SID =  D.SID ");
            sb.Append(" AND  M.PRODNO = PROD.PRODNO ");
            sb.Append(" AND  D.PRODNO=PROD1.PRODNO ");
            sb.Append(" AND  PROD.COMPANYCODE ='01' ");
            sb.Append(" AND  PROD1.COMPANYCODE='01' ");
            sb.Append(" AND  M.S_DATE <= " + OracleDBUtil.DateStr(WorkDate));
            sb.Append(" AND  NVL(M.E_DATE,TO_DATE('9999/12/31','YYYY/MM/DD') ) >= " + OracleDBUtil.DateStr(WorkDate));
            sb.Append(" AND  PROD.S_DATE <= " + OracleDBUtil.DateStr(WorkDate));
            sb.Append(" AND  NVL(PROD.E_DATE,TO_DATE('9999/12/31','YYYY/MM/DD') ) >= " + OracleDBUtil.DateStr(WorkDate));
            sb.Append(" AND  PROD1.S_DATE <= " + OracleDBUtil.DateStr(WorkDate));
            sb.Append(" AND  NVL(PROD1.E_DATE,TO_DATE('9999/12/31','YYYY/MM/DD') ) >= " + OracleDBUtil.DateStr(WorkDate));
            sb.Append(" AND M.PRODNO=" + OracleDBUtil.SqlStr(ProductNo) + ")");

            DataTable dt = OracleDBUtil.Query_Data(sb.ToString());
            return dt;
        }

        public string GetOrderId(string OrderID, string STORENO)
        {
            string Para_Value = "";
            StringBuilder sb = new StringBuilder();
            sb.Append(" SELECT ORDER_TEMP_ID FROM ORDER_M_TEMP ");
            sb.Append(" WHERE ORDER_NO IN (SELECT ORDER_NO FROM ORDER_M WHERE ORDER_ID=" + OracleDBUtil.SqlStr(OrderID) + ")");
            sb.Append(" AND STORE_NO=" + OracleDBUtil.SqlStr(STORENO));

            DataTable dt = OracleDBUtil.Query_Data(sb.ToString());

            if (dt.Rows.Count > 0)
            {
                Para_Value = dt.Rows[0]["ORDER_TEMP_ID"].ToString();
            }
            return Para_Value;
        }

        #region 預訂單查詢
        //查詢預訂單主檔資料
        public DataTable GetPreOrderM(string PreOrderMID)
        {
            //取得主檔資料
            string strSQL = @"select * 
                                from PRE_ORDER_M 
                                where PRE_ORDER_M_ID=" + OracleDBUtil.SqlStr(PreOrderMID);

            DataTable returnValue = OracleDBUtil.Query_Data(strSQL);

            return returnValue;
        }
        //查詢預訂單明細資料
        public DataTable GetPreOrderD(string PreOrderMID, string STORE_NO)
        {
            //取得明細資料
            string strSQL = @"select    d.PRE_ORDER_D_ID 
                                        , d.PRE_ORDER_SEQNO
                                        , d.ES_QTY
                                        , d.STOCK_QTY 
                                        , d.ADVISEQTY
                                        , d.ORDQTY
                                        , d.REMARK 
                                        , d.ULSNO
                                        , d.PRODNO_M
                                        , d.QTY_BDATE    
                                        , d.QTY_EDATE        
                                        , d.CREATE_USER      
                                        , d.CREATE_DTM     
                                        , d.MODI_USER      
                                        , d.MODI_DTM       
                                        , d.PRODNO           
                                        , d.GIFT_FLAG       
                                        , d.APPROVEQTY       
                                        -- , nvl(d.REAL_ORDER_QTY,'0') REAL_ORDER_QTY 
                                        , nvl(d.REAL_ATR_QTY, 0) REAL_ORDER_QTY 
                                        , d.FAIL_REASON     
                                        , d.ORDER_ITEMS_ID   
                                        , d.PRE_ORDER_M_ID   
                                        , p.PRODNAME
                               from PRE_ORDER_D d,PRODUCT p 
                               where d.PRODNO = p.PRODNO(+)
                                 and d.GIFT_FLAG = '0' 
                                 and d.PRE_ORDER_M_ID = " + OracleDBUtil.SqlStr(PreOrderMID) + @"
                               order by d.PRE_ORDER_SEQNO 
                            ";

            DataTable returnValue = OracleDBUtil.Query_Data(strSQL);
            return returnValue;
        }

        /// <summary>
        /// 取得搭配設定編號
        /// </summary>
        /// <param name="ProductNo"></param>
        /// <returns></returns>
        public static string GetOneToOneSID(string ProductNo)
        {
            string SID = "";

            StringBuilder sb = new StringBuilder();
            sb.Append(@"select SID 
                            from ONETOONE_M 
                            where S_DATE<=sysdate
                            and NVL(TO_CHAR(E_DATE,'YYYY/MM/DD'),'9999/12/31') >= TO_CHAR(SYSDATE,'YYYY/MM/DD')
                            AND PRODNO=" + OracleDBUtil.SqlStr(ProductNo));

            DataTable dt = OracleDBUtil.Query_Data(sb.ToString());

            if (dt.Rows.Count > 0)
            {
                SID = dt.Rows[0]["SID"].ToString();
            }

            return SID;
        }

        /// <summary>
        /// 取得搭配商品資料
        /// </summary>
        /// <param name="OneToOneSID"></param>
        /// <returns></returns>
        public DataTable PreGetGiftProducts(string PRODNOID, string detail_ORDQTY, string detail_REAL_ORDER_QTY, string detail_FAIL_REASON)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(@"select  B.PRODNO
                                    , B.PRODNAME
                                    , " + OracleDBUtil.SqlStr(detail_ORDQTY) + @" as ORDQTY
                                    , " + OracleDBUtil.SqlStr(detail_REAL_ORDER_QTY) + @" as  REAL_ORDER_QTY
                                    , " + OracleDBUtil.SqlStr(detail_FAIL_REASON) + @" as  FAIL_REASON 
                            from ONETOONE_D A join PRODUCT B on A.PRODNO = B.PRODNO 
                            where A.SID in (select SID  
                                            from ONETOONE_M 
                                            where TRUNC(S_DATE) <= TRUNC(sysdate) 
                                            and NVL(TO_CHAR(E_DATE,'YYYY/MM/DD'),'9999/12/31') >= TO_CHAR(SYSDATE,'YYYY/MM/DD') 
                                            AND PRODNO=" + OracleDBUtil.SqlStr(PRODNOID) + ")"
                         );

            DataTable dt = OracleDBUtil.Query_Data(sb.ToString());
            return dt;
        }

     
        #endregion
    }
}
