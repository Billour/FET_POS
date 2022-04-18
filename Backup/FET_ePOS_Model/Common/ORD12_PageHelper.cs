using System;
using System.Web;
using System.Data;
using System.Text;

using Advtek.Utility;
using FET.POS.Model.DTO;
using System.Data.OracleClient;

namespace FET.POS.Model.Common
{
    public class ORD12_PageHelper
    {
        /// <summary>
        /// 計算當日該門市該料號網購需求量
        /// </summary>
        /// <param name="StoreNo"></param>
        /// <param name="ProductNo"></param>
        /// <returns></returns>
        public static string GetEStoreBookQTY(string StoreNo, string ProductNo)
        {
            string EStoreBookQTY = "0";
            StringBuilder sb = new StringBuilder();
            sb.Append("select nvl( estore_OrderQty(" + OracleDBUtil.SqlStr(ProductNo) + "," + OracleDBUtil.SqlStr(StoreNo) + "),0) as EStoreBookQTY from dual ");

            DataTable dt = OracleDBUtil.Query_Data(sb.ToString());

            if (dt.Rows.Count > 0)
            {
                EStoreBookQTY = dt.Rows[0]["EStoreBookQTY"].ToString();
            }

            return EStoreBookQTY;
        }

        /// <summary>
        /// 計算該門市該料號庫存量
        /// </summary>
        /// <param name="StoreNo"></param>
        /// <param name="ProductNo"></param>
        /// <returns></returns>
        public static string GetCurrentInvQTY(string StoreNo, string ProductNo)
        {
            string returnValue = "0";
            StringBuilder sb = new StringBuilder();
            sb.Append(@"select nvl( INV_ONHANDQTY(" + OracleDBUtil.SqlStr(ProductNo) + "," + OracleDBUtil.SqlStr(StoreNo) + "),0) as EStoreBookQTY from dual ");

            DataTable dt = OracleDBUtil.Query_Data(sb.ToString());

            if (dt.Rows.Count > 0)
            {
                returnValue = dt.Rows[0]["EStoreBookQTY"].ToString();
            }

            return returnValue;
        }

        /// <summary>
        /// 計算該門市該料號的在途量
        /// </summary>
        /// <param name="StoreNo"></param>
        /// <param name="ProductNo"></param>
        /// <returns></returns>
        public static string GetPurchQTY(string StoreNo, string ProductNo)
        {
            string returnValue = "0";

            StringBuilder sb = new StringBuilder();
            sb.AppendLine("SELECT NVL(SUM(TB1.INWAY_QTY),0)  AS INWAY_QTY");
            sb.AppendLine("FROM");
            sb.AppendLine("(SELECT SM.TO_STORE_NO AS STORE_NO,");
            sb.AppendLine("SD.PRODNO, ");
            sb.AppendLine("(SD.TRANOUTQTY - NVL(SD.TRANINQTY,0) ) AS INWAY_QTY");
            sb.AppendLine("FROM STORETRANSFER_M SM,STORETRANSFER_d SD");
            sb.AppendLine("WHERE SM.STNO=SD.STNO AND SM.TSTATUS='20'");
            sb.AppendLine("UNION");
            sb.AppendLine("SELECT  STORE_NO,");
            sb.AppendLine("ITEMCODE AS PRODNO , INWAY_QTY");
            sb.AppendLine("FROM OENO_INQTY");
            sb.AppendLine("UNION");
            sb.AppendLine("SELECT STORENO AS STORE_NO,");
            sb.AppendLine("ITEMCODE AS PRODNO,TXTQTY AS INWAY_QTY");
            sb.AppendLine("FROM ERP_RTNNO");
            sb.AppendLine(") TB1");
            sb.AppendLine("WHERE 1=1 ");
            sb.AppendLine(" AND STORE_NO=" + OracleDBUtil.SqlStr(StoreNo));
            sb.AppendLine(" AND PRODNO=" + OracleDBUtil.SqlStr(ProductNo));
            sb.AppendLine("GROUP BY STORE_NO , PRODNO");

            DataTable dt = OracleDBUtil.Query_Data(sb.ToString());

            if (dt.Rows.Count > 0)
            {
                returnValue = dt.Rows[0]["inway_qty"].ToString();
            }

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
        public static DataTable GetGiftProducts(string PRODNOID, string REAL_QTY)
        {
            StringBuilder sb = new StringBuilder();
            //sb.Append(@"select B.PRODNO,B.PRODNAME,'1' as BASE_QTY," + OracleDBUtil.SqlStr(REAL_QTY) + "as REAL_QTY ");
            //sb.Append(" from ONETOONE_D A join PRODUCT B on A.PRODNO=B.PRODNO  ");
            //sb.Append(" where A.SID in (select SID  from ONETOONE_M  where S_DATE<=sysdate and NVL(TO_CHAR(E_DATE,'YYYY/MM/DD'),'9999/12/31') >= TO_CHAR(SYSDATE,'YYYY/MM/DD') AND PRODNO=" + OracleDBUtil.SqlStr(PRODNOID) + ")");

            sb.AppendLine(@"select  B.PRODNO
                                    , B.PRODNAME
                                    , '1' as BASE_QTY
                                    , " + OracleDBUtil.SqlStr(REAL_QTY) + @" as REAL_QTY,NVL(TO_CHAR(M.S_DATE,'YYYYMMDD'),'') S_DATE
                                    , NVL(TO_CHAR(M.E_DATE,'YYYYMMDD'),'99991231') E_DATE 
                            from ONETOONE_D A  
                            join PRODUCT B on A.PRODNO=B.PRODNO 
                            join ONETOONE_M M on M.sid=A.sid 
                            and TRUNC(M.S_DATE) <= TRUNC(sysdate) 
                            and NVL(TO_CHAR(M.E_DATE,'YYYY/MM/DD'),'9999/12/31') >= TO_CHAR(SYSDATE,'YYYY/MM/DD') 
                            AND M.PRODNO=" + OracleDBUtil.SqlStr(PRODNOID) 
                      );
            
            DataTable dt = OracleDBUtil.Query_Data(sb.ToString());
            return dt;
        }

        public static bool HasPreOrder(string StoreNo)
        {
            bool returnValue = false;

            StringBuilder sb = new StringBuilder();

            //20110419修改 by wayne
//            sb.Append(@"select PRE_ORDER_NO 
//                            from PRE_ORDER_M
//                            where trunc(CREATE_DTM)=trunc(sysdate)
//                            and STORE_NO=" + OracleDBUtil.SqlStr(StoreNo));

            sb.Append(@"select PRE_ORDER_NO 
                            from PRE_ORDER_M
                            where STATUS='11'
                            and STORE_NO=" + OracleDBUtil.SqlStr(StoreNo));

            DataTable dt = OracleDBUtil.Query_Data(sb.ToString());

            returnValue = (dt.Rows.Count > 0);

            return returnValue;
        }

        public static bool HasOldPreOrder(string StoreNo)
        {
            bool returnValue = false;

            StringBuilder sb = new StringBuilder();
            sb.Append(@"select PRE_ORDER_NO 
                            from PRE_ORDER_M
                            where STATUS='11'
                            and STORE_NO=" + OracleDBUtil.SqlStr(StoreNo));

            DataTable dt = OracleDBUtil.Query_Data(sb.ToString());

            returnValue = (dt.Rows.Count > 0);

            return returnValue;
        }

        //判斷門市訂貨門始時間
        public static string GetSTORE_ORDER_SE_DTM(string STRAT_END)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("select PARA_VALUE from SYS_PARA where ROWNUM = 1 AND PARA_KEY=" + OracleDBUtil.SqlStr(STRAT_END));

            DataTable dt = OracleDBUtil.Query_Data(sb.ToString());
            return dt.Rows[0]["PARA_VALUE"].ToString();
        }

        //檢查商品與搭贈商品是否設定在卡片群組
        public static string HasSIMCardGroupProduct(string ProductNo, string StoreNO)
        {
            string retProductNo = "";
            bool chk = true;

            //檢查商品是否在卡片群組裡
            StringBuilder sb = new StringBuilder();
            //            sb.Append(@"select PRODNO 
            //                            from SIM_GROUP_PROD SGP, SIM_GROUP_M SGM
            //                            where SGP.SIM_GROUP_ID = SGM.SIM_GROUP_ID
            //                            and SYSDATE >= NVL(SGM.S_DATE, SYSDATE) and SYSDATE <= NVL(SGM.E_DATE, SYSDATE)
            //                            AND PRODNO=" + OracleDBUtil.SqlStr(ProductNo));

            sb.Append(@"SELECT PRODNO FROM SIM_GROUP_PROD D
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
                                                                    $FUNC_PRODNO ");

            string sbStr = sb.ToString();
            sbStr = sbStr.Replace("$FUNC_STORE_NO", " AND SM.STORE_NO = " + OracleDBUtil.SqlStr(StoreNO));
            sbStr = sbStr.Replace("$FUNC_PRODNO", " AND PRODNO = " + OracleDBUtil.SqlStr(ProductNo));

            DataTable dt = OracleDBUtil.Query_Data(sbStr.ToString());

            if (dt.Rows.Count > 0)
            {
                retProductNo = "0|" + ProductNo;
                chk = false;
            }

            if (chk)
            {
                //檢查搭贈商品是否在卡片群組裡 
                sb.Length = 0;
                sb.Append(@"select PRODNO 
                            from SIM_GROUP_PROD SGP
                             JOIN SIM_GROUP_M SGM ON SGP.SIM_GROUP_ID = SGM.SIM_GROUP_ID
                             JOIN SCQC_D SD ON SGM.SIM_GROUP_ID = SD.SIM_GROUP_ID   
                             JOIN SCQC_M SM ON SD.SCQC_M_ID = SM.SCQC_M_ID   
                            where 1 = 1
                            and TRUNC(SYSDATE) >= TRUNC(NVL(SGM.S_DATE, SYSDATE)) and TRUNC(SYSDATE) <= TRUNC(NVL(SGM.E_DATE, SYSDATE))
                            and TRUNC(SD.S_DATE) <= TRUNC(sysdate)
                            and NVL(TO_CHAR(SD.E_DATE,'YYYY/MM/DD'),'9999/12/31') >= TO_CHAR(SYSDATE,'YYYY/MM/DD') 
                            AND SM.STORE_NO = " + OracleDBUtil.SqlStr(StoreNO)
                     );
                sb.Append(@"    AND PRODNO 
                             in (select D.PRODNO  from ONETOONE_M M 
                                             JOIN ONETOONE_D D ON M.SID=D.SID                                           
                                            where TRUNC(M.S_DATE) <= TRUNC(sysdate) 
                                              and NVL(TO_CHAR(M.E_DATE,'YYYY/MM/DD'),'9999/12/31') >= TO_CHAR(SYSDATE,'YYYY/MM/DD')                                          
                                              AND M.PRODNO=" + OracleDBUtil.SqlStr(ProductNo) + ")"
                                   );
                dt.Dispose();
                dt = OracleDBUtil.Query_Data(sb.ToString());
                if (dt.Rows.Count > 0)
                {
                    retProductNo = "1|";

                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        retProductNo += dt.Rows[i][0].ToString() + ",";
                    }
                    retProductNo = retProductNo.Substring(0, retProductNo.Length - 1);
                    chk = false;
                }
            }

            if (chk)
            {
                //檢查商品是否為別人的搭贈商品  
                sb.Length = 0;       
                sb.Append(@"    select M.PRODNO  from ONETOONE_M M 
                                             JOIN ONETOONE_D D ON M.SID=D.SID                                           
                                            where TRUNC(M.S_DATE) <= TRUNC(sysdate) 
                                              and NVL(TO_CHAR(M.E_DATE,'YYYY/MM/DD'),'9999/12/31') >= TO_CHAR(SYSDATE,'YYYY/MM/DD')                                          
                                              AND D.PRODNO=" + OracleDBUtil.SqlStr(ProductNo) + " "
                                   );
                dt.Dispose();
                dt = OracleDBUtil.Query_Data(sb.ToString());
                if (dt.Rows.Count > 0)
                {
                    retProductNo = "2|";

                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        retProductNo += dt.Rows[i][0].ToString() + ",";
                    }
                    retProductNo = retProductNo.Substring(0, retProductNo.Length - 1);                
                }
            
            }



            return retProductNo;
        }

    }

}
