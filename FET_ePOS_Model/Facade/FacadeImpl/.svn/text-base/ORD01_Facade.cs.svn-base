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

    public class ORD01_Facade
    {
        /// <summary>
        /// 判斷今日暫存訂單主檔是否已經產生
        /// </summary>
        /// <param name="StoreNo">門市代碼</param>
        /// <param name="WorkDate">營業日</param>
        /// <returns></returns>
        private bool IsExist_OrderMDTemp(string StoreNo, string WorkDate)
        {
            OracleConnection objConn = null;
            DataTable dt;

            try
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("SELECT COUNT(OMT.ORDER_TEMP_ID) CNT ");
                sb.Append("FROM   ORDER_M_TEMP OMT ");
                sb.Append("WHERE 1 = 1 ");
                sb.Append("  AND OMT.ORDER_TYPE = '1' ");
                sb.Append("  AND OMT.ORDDATE = " + OracleDBUtil.SqlStr(WorkDate));
                sb.Append("  AND STORE_NO = " + OracleDBUtil.SqlStr(StoreNo));

                objConn = OracleDBUtil.GetConnection();
                dt = OracleDBUtil.GetDataSet(objConn, sb.ToString()).Tables[0];

                if (Convert.ToDecimal(dt.Rows[0]["CNT"]) > 0) { return true; } else { return false; }
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

        /// <summary>
        /// 判斷今日訂單主檔是否已經完成
        /// </summary>
        /// <param name="StoreNo">門市代碼</param>
        /// <param name="WorkDate">營業日</param>
        /// <returns></returns>
        public bool IsExist_OrderMDOver(string StoreNo, string WorkDate)
        {
            OracleConnection objConn = null;
            DataTable dt;

            try
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("SELECT COUNT(OM.ORDER_ID) CNT ");
                sb.Append("FROM   ORDER_M OM ");
                sb.Append("WHERE  1 = 1 ");
                sb.Append("  AND OM.ORDER_TYPE = '1' ");
                sb.Append("  AND OM.STATUS >= '50' ");
                sb.Append("  AND OM.ORDDATE = " + OracleDBUtil.SqlStr(WorkDate));
                sb.Append("  AND STORE_NO = " + OracleDBUtil.SqlStr(StoreNo));

                objConn = OracleDBUtil.GetConnection();
                dt = OracleDBUtil.GetDataSet(objConn, sb.ToString()).Tables[0];

                if (Convert.ToDecimal(dt.Rows[0]["CNT"]) > 0) { return true; } else { return false; }
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

        /// <summary>
        /// 查詢營業日已存在的 OrderMTemp
        /// </summary>
        /// <param name="StroeNo">門市代碼</param>
        /// <returns></returns>
        private DataTable Query_ExistOrderMTemp(string StoreNo, string WorkDate)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT ISOK, ");
            sb.Append("       ORDER_TEMP_ID, ");
            sb.Append("       AMOUNT, ");
            sb.Append("       DIFFREASON, ");
            sb.Append("       REMARK, ");
            sb.Append("       ORDDATE, ");
            sb.Append("       ERP_ORDER_HEADER_ID, ");
            sb.Append("       ULSNO, ");
            sb.Append("       CREATE_USER, ");
            sb.Append("       CREATE_DTM, ");
            sb.Append("       MODI_USER, ");
            sb.Append("       MODI_DTM, ");
            sb.Append("       ORDER_NO, ");
            sb.Append("       ORDER_TYPE, ");
            sb.Append("       PRE_ORDER_M_ID, ");
            sb.Append("       STORE_NO, ");
            sb.Append("       HQ_ORDER_M_ID ");
            sb.Append(" FROM   ORDER_M_TEMP OMT");
            sb.Append(" WHERE  1 = 1  ");
            sb.Append("   AND OMT.ORDDATE = " + OracleDBUtil.SqlStr(WorkDate));
            sb.Append("   AND OMT.ORDER_TYPE = '1' ");
            sb.Append("   AND OMT.STORE_NO = " + OracleDBUtil.SqlStr(StoreNo));

            DataTable dt = OracleDBUtil.Query_Data(sb.ToString());
            return dt;
        }

        /// <summary>
        /// 查詢營業日已存在的 OrderDTemp
        /// </summary>
        /// <param name="StoreNo">門市代碼</param>
        /// <param name="WorkDate">營業日</param>
        /// <returns></returns>
        private DataTable Query_ExistOrderDTemp(string StoreNo, string WorkDate)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(
                        @"
                                SELECT ODT.SEQNO ITEMNO, 
                                       PROD.PRODNAME, 
                                       PROD.PRODNO, 
                                       ODT.SEQNO, 
                                       OMT.STORE_NO, 
                                       OMT.ORDER_TEMP_ID, 
                                       ODT.ORDER_D_TEMP_ID, 
                                       $FUNC_ORDQTY ORDQTY, 
                                       ODT.ADVISEQTY, 
                                       $FUNC_ES_QTY ES_QTY,                                           --$FUNC_ES_QTY                     ES_QTY, 
                                       ODT.STOCK_QTY,                                        --$FUNC_STOCK_QTY                  STOCK_QTY, 
                                       $FUNC_STOCK_INWAYQTY             INWAYQTY, 
                                       ODT.TODAY_ORDER_QTY,                                  --$FUNC_STOCK_TODAY_ORDER_QTY      TODAY_ORDER_QTY, 
                                       $FUNC_STOCK_CHECK_IN_QTY         CHECK_IN_QTY 
                                FROM   ORDER_M_TEMP OMT 
                                       INNER JOIN ORDER_D_TEMP ODT 
                                         ON OMT.ORDER_TEMP_ID = ODT.ORDER_TEMP_ID 
                                       LEFT JOIN STORE ST 
                                         ON OMT.STORE_NO = ST.STORE_NO 
                                       LEFT JOIN PRODUCT PROD 
                                         ON ODT.PRODNO = PROD.PRODNO 
                                WHERE  1 = 1 
                                AND OMT.ORDER_TYPE = '1'
                                AND ODT.GIFT_FALG = '0'
                             "
                );
            sb.Append(" AND OMT.ORDDATE = " + OracleDBUtil.SqlStr(WorkDate));

            if (!string.IsNullOrEmpty(StoreNo))
            {
                sb.Append(" AND OMT.STORE_NO = " + OracleDBUtil.SqlStr(StoreNo));
            }
            sb.Append(" order by to_number(ODT.SEQNO)");
            string sbStr = sb.ToString();
            sbStr = sbStr.Replace("$FUNC_ES_QTY", "nvl(ODT.ES_QTY,0)");
            sbStr = sbStr.Replace("$FUNC_STOCK_QTY", OracleDBUtil.NumberStr("0"));
            sbStr = sbStr.Replace("$FUNC_STOCK_INWAYQTY", "nvl((SELECT INWAY_QTY('" + StoreNo + "',PROD.PRODNO) FROM dual),0)");
            sbStr = sbStr.Replace("$FUNC_STOCK_TODAY_ORDER_QTY", OracleDBUtil.NumberStr("0"));
            sbStr = sbStr.Replace("$FUNC_STOCK_CHECK_IN_QTY", "nvl((SELECT INORDER_QTY('" + StoreNo + "',PROD.PRODNO) FROM dual),0)");
            // CASE WHEN  (NVL ( (SELECT estore_OrderQty (PROD.PRODNO, '2101') FROM DUAL), 0)- ODT.STOCK_QTY) >= 0 THEN (NVL ( (SELECT estore_OrderQty (PROD.PRODNO, '2101') FROM DUAL), 0)- ODT.STOCK_QTY) ELSE 0 END
            //sbStr = sbStr.Replace("$FUNC_ORDQTY", "CASE  WHEN NVL (ORDQTY,0) > 0 THEN ORDQTY WHEN NVL (ORDQTY,0) = 0 AND (NVL ( (SELECT estore_OrderQty (PROD.PRODNO,'" + StoreNo + "') FROM DUAL), 0)- ODT.STOCK_QTY) >= 0 THEN (NVL ( (SELECT estore_OrderQty (PROD.PRODNO,'" + StoreNo + "') FROM DUAL), 0)- ODT.STOCK_QTY) ELSE 0 END");
            //當日頂購量預設為建議訂購量
            // sbStr = sbStr.Replace("$FUNC_ORDQTY", "nvl((SELECT estore_OrderQty(PROD.PRODNO,'" + StoreNo + "') FROM dual),0)");
            //sbStr = sbStr.Replace("$FUNC_ORDQTY", "CASE  WHEN NVL (ORDQTY,0) > 0 THEN ORDQTY WHEN NVL (ORDQTY,0) = 0 THEN  NVL(ODT.ADVISEQTY, 0)  ELSE 0 END");
            sbStr = sbStr.Replace("$FUNC_ORDQTY", "CASE  WHEN OMT.STATUS IS NOT NULL THEN ORDQTY  ELSE  NVL(ODT.ADVISEQTY, 0)  END");

            //sbStr = sbStr.Replace("$FUNC_ORDQTY", "CASE  WHEN OMT.STATUS IS NOT NULL THEN ORDQTY  ELSE  nvl((SELECT GET_MAX_ORDER_QTY('" + StoreNo + "',PROD.PRODNO) FROM dual),0)  END");


            DataTable dt = OracleDBUtil.Query_Data(sbStr.ToString());
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
            sb.Append("SELECT ISOK, ");
            sb.Append("       ORDER_ID ORDER_TEMP_ID, ");
            sb.Append("       AMOUNT, ");
            sb.Append("       DIFFREASON, ");
            sb.Append("       REMARK, ");
            sb.Append("       ORDDATE, ");
            sb.Append("       ERP_ORDER_HEADER_ID, ");
            sb.Append("       ULSNO, ");
            sb.Append("       CREATE_USER, ");
            sb.Append("       CREATE_DTM, ");
            sb.Append("       MODI_USER, ");
            sb.Append("       MODI_DTM, ");
            sb.Append("       ORDER_NO, ");
            sb.Append("       ORDER_TYPE, ");
            sb.Append("       PRE_ORDER_M_ID, ");
            sb.Append("       STORE_NO, ");
            sb.Append("       HQ_ORDER_M_ID, ");
            sb.Append("       OMT.ORDER_TYPE ");
            sb.Append(" FROM   ORDER_M OMT");
            sb.Append(" WHERE  1 = 1 ");
            sb.Append("   AND OMT.ORDER_ID = " + OracleDBUtil.SqlStr(OrderId));

            DataTable dt = OracleDBUtil.Query_Data(sb.ToString());
            return dt;
        }

        public DataTable GetOrderMDTempBy1(string OrderId, string STORENO)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT ODT.SEQNO ITEMNO, ");
            sb.Append("       PROD.PRODNAME, ");
            sb.Append("       PROD.PRODNO, ");
            sb.Append("       ODT.SEQNO, ");
            sb.Append("       OMT.STORE_NO, ");
            sb.Append("       OMT.ORDER_ID ORDER_TEMP_ID, ");
            sb.Append("       ODT.ORDER_ITEMS_ID ORDER_D_TEMP_ID, ");
            sb.Append("       ODT.ADVISEQTY, ");
            sb.Append("       ODT.ORDQTY, ");
            //sb.Append("       ODT.ADVISEQTY ORDQTY, ");//當日頂購量預設為建議訂購量
            sb.Append("       ODT.ES_QTY, ");
            sb.Append("       ODT.STOCK_QTY, ");
            sb.Append("       nvl((SELECT INWAY_QTY('" + STORENO + "',PROD.PRODNO) FROM dual),0)  INWAYQTY, ");
            sb.Append("       nvl((SELECT FN_TODAY_ORDER_QTY(OMT.PRE_ORDER_M_ID,PROD.PRODNO) FROM dual),0) TODAY_ORDER_QTY, ");
            sb.Append("       nvl((SELECT INORDER_QTY('" + STORENO + "',PROD.PRODNO) FROM dual),0)  CHECK_IN_QTY ");
            sb.Append("FROM   ORDER_M OMT, ORDER_D ODT, STORE ST, PRODUCT PROD ");
            sb.Append("WHERE  OMT.ORDER_ID = ODT.ORDER_ID AND OMT.STORE_NO = ST.STORE_NO(+) AND ODT.PRODNO = PROD.PRODNO(+) ");
            sb.Append("  AND  ( ODT.ADVISEQTY > 0  or OMT.ORDER_TYPE in ('2','3','5'))");
            sb.Append("  AND  OMT.ORDER_ID = " + OracleDBUtil.SqlStr(OrderId));
            sb.Append("       Order by to_number(ODT.SEQNO) ");

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
            sb.Append("SELECT ISOK, ");
            sb.Append("       ORDER_TEMP_ID, ");
            sb.Append("       AMOUNT, ");
            sb.Append("       DIFFREASON, ");
            sb.Append("       REMARK, ");
            sb.Append("       ORDDATE, ");
            sb.Append("       ERP_ORDER_HEADER_ID, ");
            sb.Append("       ULSNO, ");
            sb.Append("       CREATE_USER, ");
            sb.Append("       CREATE_DTM, ");
            sb.Append("       MODI_USER, ");
            sb.Append("       MODI_DTM, ");
            sb.Append("       ORDER_NO, ");
            sb.Append("       ORDER_TYPE, ");
            sb.Append("       PRE_ORDER_M_ID, ");
            sb.Append("       STORE_NO, ");
            sb.Append("       HQ_ORDER_M_ID ");
            sb.Append(" FROM   ORDER_M_TEMP OMT");
            sb.Append(" WHERE  1 = 1 ");
            sb.Append("   AND OMT.ORDER_TYPE = '1' ");
            sb.Append("   AND OMT.ORDER_TEMP_ID = " + OracleDBUtil.SqlStr(OrderId));

            DataTable dt = OracleDBUtil.Query_Data(sb.ToString());
            return dt;
        }

        public DataTable GetOrderMDTempBy(string OrderId, string STORENO)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT ODT.SEQNO ITEMNO, ");
            sb.Append("       PROD.PRODNAME, ");
            sb.Append("       PROD.PRODNO, ");
            sb.Append("       ODT.SEQNO, ");
            sb.Append("       OMT.STORE_NO, ");
            sb.Append("       OMT.ORDER_TEMP_ID, ");
            sb.Append("       ODT.ORDER_D_TEMP_ID, ");
            sb.Append("       ODT.ADVISEQTY, ");
            sb.Append("       ODT.ORDQTY, ");
            //sb.Append("       ODT.ADVISEQTY ORDQTY, "); //當日頂購量預設為建議訂購量
            sb.Append("       ODT.ES_QTY, ");
            sb.Append("       ODT.STOCK_QTY, ");
            sb.Append("       nvl((SELECT INWAY_QTY('" + STORENO + "',PROD.PRODNO) FROM dual),0)   INWAYQTY, ");
            //sb.Append("       ODT.TODAY_ORDER_QTY, ");
            sb.Append("       nvl((SELECT FN_TODAY_ORDER_QTY(OMT.PRE_ORDER_M_ID,PROD.PRODNO) FROM dual),0) TODAY_ORDER_QTY, ");
            sb.Append("       nvl((SELECT INORDER_QTY('" + STORENO + "',PROD.PRODNO) FROM dual),0) CHECK_IN_QTY ");
            sb.Append("FROM   ORDER_M_TEMP OMT,ORDER_D_TEMP ODT,STORE ST, PRODUCT PROD ");
            sb.Append("WHERE  OMT.ORDER_TEMP_ID = ODT.ORDER_TEMP_ID AND OMT.STORE_NO = ST.STORE_NO(+) AND ODT.PRODNO = PROD.PRODNO(+) ");
            sb.Append("       AND OMT.ORDER_TYPE = '1' ");
            sb.Append("       AND OMT.ORDER_TEMP_ID = " + OracleDBUtil.SqlStr(OrderId));
            sb.Append("       Order by to_number(ODT.SEQNO) ");

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

        /// <summary>
        /// 新增今日的 ORDER_M_TEMP
        /// 新增今日的 ORDER_D_TEMP
        /// </summary>
        /// <param name="StoreNo">門市代碼</param>
        /// <param name="WorkDate">營業日</param>
        private void AddNew_OrderMDTemp(string StoreNo, string WorkDate, string CreateUser)
        {
            string OrderTmpM_UUID = GuidNo.getUUID();
            using (OracleConnection oConn = OracleDBUtil.GetConnection())
            {
                OracleCommand oraCmd = new OracleCommand("SP_INSERT_ORDER_M_TEMP");
                oraCmd.CommandType = System.Data.CommandType.StoredProcedure;
                oraCmd.Parameters.Add(new OracleParameter("v_STORE", OracleType.VarChar, 2000)).Value = StoreNo;
                oraCmd.Parameters.Add(new OracleParameter("v_UUID", OracleType.VarChar, 2000)).Value = OrderTmpM_UUID;
                oraCmd.Parameters.Add(new OracleParameter("v_User", OracleType.VarChar, 2000)).Value = CreateUser;
                oraCmd.Connection = oConn;
                oraCmd.ExecuteNonQuery();
            }
        }

        /// <summary>
        /// 取得營業日的訂單
        /// </summary>
        /// <param name="StoreNo">門市代碼</param>
        /// <param name="WorkDate">營業日</param>
        /// <returns></returns>
        public DataTable GetTodayOrder(string StoreNo, string WorkDate, string CreateUser)
        {
            if (IsExist_OrderMDTemp(StoreNo, WorkDate) == false)
            {
                AddNew_OrderMDTemp(StoreNo, WorkDate, CreateUser);
            }

            DataTable dt = Query_ExistOrderMTemp(StoreNo, WorkDate);
            return dt;
        }

        /// <summary>
        /// 取得營業日可訂購的訂單細項
        /// </summary>
        /// <param name="StoreNo">門市代碼</param>
        /// <param name="WorkDate">營業日</param>
        /// <returns></returns>
        public DataTable GetTodayAvailableOrderItems(string StoreNo, string WorkDate, string CreateUser)
        {
            DataTable dt;

            if (IsExist_OrderMDTemp(StoreNo, WorkDate) == false)
            {
                dt = GetTodayOrder(StoreNo, WorkDate, CreateUser);
            }

            dt = Query_ExistOrderDTemp(StoreNo, WorkDate);
            return dt;
        }

        /// <summary>
        /// 更新今日可訂購的訂單細項
        /// </summary>
        /// <param name="ds"></param>
        public void Update_TodayAvailableOrderItems(ORD01_OrderMD_DTO ds, string StoreNo, string WorkDate, string CreateUser)
        {
            OracleConnection objConn = null;
            OracleTransaction objTX = null;

            try
            {
                objConn = OracleDBUtil.GetConnection();
                objTX = objConn.BeginTransaction();

                OracleDBUtil.UPDDATEByUUID(objTX, ds.ORDER_M_TEMP, "ORDER_TEMP_ID");
                OracleDBUtil.UPDDATEByUUID(objTX, ds.ORDER_D_TEMP, "ORDER_D_TEMP_ID");

                StringBuilder sb = new StringBuilder();

                sb.Append("select ORDER_ID ");
                sb.Append("from ORDER_M  ");
                sb.Append(" where 1=1 ");
                sb.Append(" AND ORDDATE =  " + OracleDBUtil.SqlStr(WorkDate));
                sb.Append(" AND STORE_NO =" + OracleDBUtil.SqlStr(StoreNo));
                sb.Append(" AND  ORDER_TYPE = '1' ");

                DataTable dt = OracleDBUtil.GetDataSet(objTX, sb.ToString()).Tables[0];


                if (dt.Rows.Count > 0)
                {
                    string delOrderM = "";
                    string delOrderD = "";

                    delOrderM = "delete From ORDER_M where  ORDER_ID=" + OracleDBUtil.SqlStr(dt.Rows[0][0].ToString());

                    OracleDBUtil.ExecuteSql(objTX, delOrderM);

                    delOrderD = "delete From ORDER_D where  ORDER_ID=" + OracleDBUtil.SqlStr(dt.Rows[0][0].ToString());

                    OracleDBUtil.ExecuteSql(objTX, delOrderD);

                }

                INSERT_ORDER_DATA(objTX, StoreNo, WorkDate);

                //objTX.Rollback();
                objTX.Commit();
            }
            catch (Exception ex)
            {
                objTX.Rollback();
                throw ex;
            }
            finally
            {
                objTX = null;
                if (objConn.State == ConnectionState.Open) objConn.Close();
                objConn.Dispose();
                OracleConnection.ClearAllPools();
            }
        }

        public void INSERT_ORDER_DATA(OracleTransaction objTX, string StoreNo, string WorkDate)
        {
            string NewOrderSID = GuidNo.getUUID();
            StringBuilder sb_m = new StringBuilder();
            //INSERT INTO ORDER_M
            sb_m.Append(@"INSERT INTO ORDER_M
                               (
                               ISOK, ORDER_ID, AMOUNT, 
                               DIFFREASON, REMARK, ORDDATE, 
                               ERP_ORDER_HEADER_ID, ULSNO, CREATE_USER, 
                               CREATE_DTM, MODI_USER, MODI_DTM, 
                               ORDER_NO, ORDER_TYPE, PRE_ORDER_M_ID, 
                               STORE_NO, HQ_ORDER_M_ID,STATUS
                               )
                                SELECT 
                                   ISOK, $PARAM_ORDER_ID, AMOUNT, 
                                   DIFFREASON, REMARK, ORDDATE, 
                                   ERP_ORDER_HEADER_ID, ULSNO, CREATE_USER, 
                                   CREATE_DTM, MODI_USER, MODI_DTM, 
                                   ORDER_NO, ORDER_TYPE, PRE_ORDER_M_ID, 
                                   STORE_NO, HQ_ORDER_M_ID,STATUS
                                FROM ORDER_M_TEMP OMT
                                WHERE 1 = 1
                                AND OMT.ORDDATE = $PARAM_ORDDATE
                                AND STORE_NO = $PARAM_STORE_NO
                                AND ORDER_TYPE = '1' 
                                AND ROWNUM =1 ORDER BY MODI_DTM DESC"
                        );

            string sb_mString = sb_m.ToString();
            sb_mString = sb_mString.Replace(@"$PARAM_ORDER_ID".ToString(), OracleDBUtil.SqlStr(NewOrderSID));
            sb_mString = sb_mString.Replace(@"$PARAM_STORE_NO".ToString(), OracleDBUtil.SqlStr(StoreNo));
            sb_mString = sb_mString.Replace(@"$PARAM_ORDDATE".ToString(), OracleDBUtil.SqlStr(WorkDate));


            //INSERT INTO ORDER_D
            StringBuilder sb_d = new StringBuilder();
            sb_d.Append(@"INSERT INTO ORDER_D
                              (
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
                               HQ_ORDER_STORE, LOC_ID,VENDOR_NAME  
                              )
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
                                   $PARAM_ORDER_ID, POS_UUID, PRE_ORDER_D_ID, 
                                   HQ_ORDER_STORE, LOC_ID,VENDOR_NAME  
                                FROM ORDER_D_TEMP ODT
                                WHERE 1 = 1 
                                 AND  ORDER_TEMP_ID=
                                (SELECT 
                                   ORDER_TEMP_ID
                                FROM ORDER_M_TEMP OMT
                                WHERE 1 = 1
                                AND OMT.ORDER_TYPE = '1'
                                AND OMT.ORDDATE =$PARAM_ORDDATE
                                AND STORE_NO = $PARAM_STORE_NO
                                AND ROWNUM =1 )"

                        );

            string sb_dString = sb_d.ToString();
            sb_dString = sb_dString.Replace(@"$PARAM_ORDER_ID".ToString(), OracleDBUtil.SqlStr(NewOrderSID));
            sb_dString = sb_dString.Replace(@"$PARAM_STORE_NO".ToString(), OracleDBUtil.SqlStr(StoreNo));
            sb_dString = sb_dString.Replace(@"$PARAM_ORDDATE".ToString(), OracleDBUtil.SqlStr(WorkDate));

            OracleDBUtil.ExecuteSql(objTX, sb_mString);
            OracleDBUtil.ExecuteSql(objTX, sb_dString);
        }

        /// <summary>
        /// 確認送出本店今日的訂購細項
        /// </summary>
        /// <param name="ds"></param>
        public void CommitTodayOrderItems(ORD01_OrderMD_DTO ds, string StoreNo, string WorkDate, string CreateUser)
        {
            ds.Tables["ORDER_M_TEMP"].Rows[0]["ISOK"] = "1";
            ds.Tables["ORDER_M_TEMP"].Rows[0]["STATUS"] = "50";
            ds.AcceptChanges();

            OracleConnection objConn = null;
            OracleTransaction objTX = null;
            OracleCommand oraCmd = null;
            try
            {
                objConn = OracleDBUtil.GetConnection();
                objTX = objConn.BeginTransaction();

                OracleDBUtil.UPDDATEByUUID(objTX, ds.ORDER_M_TEMP, "ORDER_TEMP_ID");

                StringBuilder sb_m = new StringBuilder();
                sb_m.Append(@"UPDATE ORDER_M set STATUS='50',ISOK='1',MODI_USER= $PARAM_MODI_USER,MODI_DTM=sysdate
                              where ORDDATE= $PARAM_ORDDATE 
                                AND  STORE_NO= $PARAM_STORE_NO
                                AND  ORDER_NO= $PARAM_ORDER_NO"
                       );

                string sb_mString = sb_m.ToString();
                sb_mString = sb_mString.Replace(@"$PARAM_ORDDATE".ToString(), OracleDBUtil.SqlStr(WorkDate));
                sb_mString = sb_mString.Replace(@"$PARAM_STORE_NO".ToString(), OracleDBUtil.SqlStr(StoreNo));
                sb_mString = sb_mString.Replace(@"$PARAM_ORDER_NO".ToString(), OracleDBUtil.SqlStr(ds.Tables["ORDER_M_TEMP"].Rows[0]["ORDER_NO"].ToString()));
                sb_mString = sb_mString.Replace(@"$PARAM_MODI_USER".ToString(), OracleDBUtil.SqlStr(CreateUser));
                OracleDBUtil.ExecuteSql(objTX, sb_mString.ToString());

                oraCmd = new OracleCommand("SP_ORDER_QTY_CHECK");
                oraCmd.CommandType = System.Data.CommandType.StoredProcedure;
                oraCmd.Parameters.Add(new OracleParameter("I_ORDER_NO", ds.Tables["ORDER_M_TEMP"].Rows[0]["ORDER_NO"].ToString()));
                oraCmd.Parameters.Add(new OracleParameter("I_STORE_NO", StoreNo));

                oraCmd.Connection = objTX.Connection;
                oraCmd.Transaction = objTX;
                oraCmd.ExecuteNonQuery();

                //objTX.Rollback();
                objTX.Commit();
            }
            catch (Exception ex)
            {
                objTX.Rollback();
                throw ex;
            }
            finally
            {
                objTX = null;
                oraCmd.Dispose();
                if (objConn.State == ConnectionState.Open) objConn.Close();
                objConn.Dispose();
                OracleConnection.ClearAllPools();
            }
        }

        /// <summary>
        /// 取得搭贈ORDER_D_TEMP_ID
        /// </summary>
        /// <param name="ProductNo">產品編號</param>
        /// <param name="WorkDate">營業日</param>
        /// <returns></returns>
        public DataTable GetGiftORDER_D_TEMP_ID(string ORDER_M_TEMP_ID, string ProductNo, string StoreNo, string WorkDate, string M_ORDER_D_TEMP_ID)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(" select ORDER_D_TEMP_ID from ORDER_M_TEMP OMT  ");
            sb.Append("  Join ORDER_D_TEMP ODT");
            sb.Append("  on OMT.ORDER_TEMP_ID=ODT.ORDER_TEMP_ID ");
            sb.Append("  AND ODT.PRODNO_M = " + OracleDBUtil.SqlStr(ProductNo));
            sb.Append("  AND ODT.ORDER_D_TEMP_ID <> " + OracleDBUtil.SqlStr(M_ORDER_D_TEMP_ID));
            sb.Append(" where 1=1 ");
            sb.Append("  AND OMT.ORDER_TEMP_ID = " + OracleDBUtil.SqlStr(ORDER_M_TEMP_ID));
            sb.Append("  AND OMT.ORDER_TYPE = '1' ");
            sb.Append("  AND OMT.ORDDATE = " + OracleDBUtil.SqlStr(WorkDate));
            sb.Append("  AND OMT.STORE_NO = " + OracleDBUtil.SqlStr(StoreNo));

            DataTable dt = OracleDBUtil.Query_Data(sb.ToString());

            return dt;
        }

        /// <summary>
        /// 取得搭贈商品資料
        /// </summary>
        /// <param name="OneToOneSID">搭贈主檔SID</param>
        /// <returns></returns>
        public DataTable GetGiftProducts(string ProductNo, string WorkDate, string detail_BASE_QTY)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("select A.SID,B.PRODNO,B.PRODNAME,'" + detail_BASE_QTY + "' as BASE_QTY,'1' as REAL_QTY ");
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

        /// <summary>
        /// 取得訂單編號
        /// </summary>
        /// <returns></returns>
        public string GetParaValue(string Para_Key)
        {
            string Para_Value = "";

            StringBuilder sb = new StringBuilder();
            sb.Append(" select PARA_VALUE from SYS_PARA ");
            sb.Append(" where PARA_KEY=" + OracleDBUtil.SqlStr(Para_Key));

            DataTable dt = OracleDBUtil.Query_Data(sb.ToString());

            if (dt.Rows.Count > 0)
            {
                Para_Value = dt.Rows[0]["PARA_VALUE"].ToString();
            }
            return Para_Value;
        }

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


        //檢查StroeAtr量
        //20110417拿掉檢查StroeAtr的量
        //public string Check_StoreAtr(string ProdNo, string StoreNo, string Qty, string WorkDate) //搭贈商品ATR量檢查
        //{
        //    string strStoreAtr_ProdNo = "";

        //    //**2010/02/11 Tina：輸入的訂購量是否超過允許的數量
        //    //**1. 判斷主商品和搭配商品ATR數量為者較小，以數量較小的商品做第2點判斷
        //    //**2. 依照 POS_STORE_ATR 的 ATRQTY, MAX_ORDER_QTY, REMAINED_ATR 此三種數量來判斷

        //    //StringBuilder sb = new StringBuilder();
        //    //sb.Append(" select PRODNO,ATRQTY from POS_STORE_ATR ");
        //    //sb.Append(" where PRODNO=$PARAM_PRODNO");
        //    //sb.Append(" AND TRANS_DATE=$PARAM_TRANS_DATE ");
        //    //sb.Append(" AND store_no=$PARAM_STORE_NO");

        //    //StringBuilder strProd = new StringBuilder();
        //    //strProd.Append(" (select D.PRODNO ");
        //    //strProd.Append("  FROM ONETOONE_M M, ONETOONE_D D ,PRODUCT PROD,PRODUCT PROD1 ");
        //    //strProd.Append(" WHERE M.SID =  D.SID ");
        //    //strProd.Append(" AND  M.PRODNO = PROD.PRODNO ");
        //    //strProd.Append(" AND  D.PRODNO=PROD1.PRODNO ");
        //    //strProd.Append(" AND  PROD.COMPANYCODE ='01' ");
        //    //strProd.Append(" AND  PROD1.COMPANYCODE='01' ");
        //    //strProd.Append(" AND  M.S_DATE <= (SELECT WorkingDay (" + OracleDBUtil.SqlStr(StoreNo) + ")  v_Work_D FROM DUAL)");
        //    //strProd.Append(" AND  NVL(M.E_DATE,TO_DATE('9999/12/31','YYYY/MM/DD') ) >=(SELECT WorkingDay (" + OracleDBUtil.SqlStr(StoreNo) + ")  v_Work_D FROM DUAL)");
        //    //strProd.Append(" AND  PROD.S_DATE <= (SELECT WorkingDay (" + OracleDBUtil.SqlStr(StoreNo) + ")  v_Work_D FROM DUAL)");
        //    //strProd.Append(" AND  NVL(PROD.E_DATE,TO_DATE('9999/12/31','YYYY/MM/DD') ) >= (SELECT WorkingDay (" + OracleDBUtil.SqlStr(StoreNo) + ")  v_Work_D FROM DUAL)");
        //    //strProd.Append(" AND  PROD1.S_DATE <= (SELECT WorkingDay (" + OracleDBUtil.SqlStr(StoreNo) + ")  v_Work_D FROM DUAL)");
        //    //strProd.Append(" AND  NVL(PROD1.E_DATE,TO_DATE('9999/12/31','YYYY/MM/DD') ) >= (SELECT WorkingDay (" + OracleDBUtil.SqlStr(StoreNo) + ")  v_Work_D FROM DUAL)");
        //    //strProd.Append(" AND M.PRODNO=" + OracleDBUtil.SqlStr(ProdNo) + ") ");

        //    //string sb_String = sb.ToString();
        //    //sb_String = sb_String.Replace(@"$PARAM_PRODNO".ToString(), OracleDBUtil.SqlStr(ProdNo));
        //    //sb_String = sb_String.Replace(@"$PARAM_TRANS_DATE".ToString(), OracleDBUtil.DateStr(WorkDate));
        //    //sb_String = sb_String.Replace(@"$PARAM_STORE_NO".ToString(), OracleDBUtil.SqlStr(StoreNo));

        //    int iDetailAtr = 0;
        //    DataRow drD = null;
        //    DataTable dtDetail = GetGiftProducts(ProdNo, WorkDate, "");

        //   OracleConnection objConn = null;
        //    try
        //    {
        //        objConn = OracleDBUtil.GetConnection();

        //        if (dtDetail.Rows.Count > 0)
        //        {
        //            StringBuilder sbD = new StringBuilder();
        //            sbD.AppendLine(" select PRODNO,ATRQTY, MAX_ORDER_QTY, REMAINED_ATR from POS_STORE_ATR ");
        //            sbD.AppendLine(" where PRODNO = " +  OracleDBUtil.SqlStr(dtDetail.Rows[0]["PRODNO"].ToString()));
        //            sbD.AppendLine(" AND TRANS_DATE = " + OracleDBUtil.DateStr(WorkDate));
        //            sbD.AppendLine(" AND store_no = " + OracleDBUtil.SqlStr(StoreNo));

        //            DataTable dtD = OracleDBUtil.GetDataSet(objConn, sbD.ToString()).Tables[0];
        //            if (dtD.Rows.Count > 0)
        //            {
        //                drD = dtD.Rows[0];
        //                iDetailAtr = drD["ATRQTY"].ToString().Trim() == "" ? 0 : int.Parse(drD["ATRQTY"].ToString());
        //            }
        //        }

        //        StringBuilder sbM = new StringBuilder();
        //        sbM.AppendLine(" select PRODNO,ATRQTY, MAX_ORDER_QTY, REMAINED_ATR from POS_STORE_ATR ");
        //        sbM.AppendLine(" where PRODNO = " + OracleDBUtil.SqlStr(ProdNo));
        //        sbM.AppendLine(" AND TRANS_DATE = " + OracleDBUtil.DateStr(WorkDate));
        //        sbM.AppendLine(" AND store_no = " + OracleDBUtil.SqlStr(StoreNo));

        //        DataTable dtM = OracleDBUtil.GetDataSet(objConn, sbM.ToString()).Tables[0];

        //        if (dtM.Rows.Count > 0)
        //        {
        //            //for (int i = 0; i < dt.Rows.Count; i++)
        //            //{
        //            //    iStoreAtr = dt.Rows[i]["ATRQTY"].ToString().Trim() == "" ? 0 : int.Parse(dt.Rows[i]["ATRQTY"].ToString());

        //            //    if (int.Parse(Qty) > iStoreAtr)
        //            //    {
        //            //        strStoreAtr_ProdNo = dt.Rows[i]["PRODNO"].ToString().Trim();
        //            //        return strStoreAtr_ProdNo;
        //            //    }

        //            //}

        //            int iStoreAtr = 0;
        //            int iStoreMax = 0;
        //            int iStoreRem = 0;
        //            string strPRODNO = "";
        //            bool isMasterData = false;

        //            DataRow drM = dtM.Rows[0];
        //            int iMasterAtr = drM["ATRQTY"].ToString().Trim() == "" ? 0 : int.Parse(drM["ATRQTY"].ToString());

        //            //判斷主商品和搭配商品ATR數量為者較小，以數量較小的商品做第2點判斷
        //            //iDetailAtr為0表示，此主商品無搭配商品
        //            if (iDetailAtr == 0 || iMasterAtr <= iDetailAtr)
        //            {
        //                iStoreAtr = iMasterAtr;
        //                iStoreMax = drM["MAX_ORDER_QTY"].ToString().Trim() == "" ? 0 : int.Parse(drM["MAX_ORDER_QTY"].ToString());
        //                iStoreRem = drM["REMAINED_ATR"].ToString().Trim() == "" ? 0 : int.Parse(drM["REMAINED_ATR"].ToString());
        //                strPRODNO = drM["PRODNO"].ToString().Trim();
        //                isMasterData = true;
        //            }
        //            else
        //            {
        //                iStoreAtr = iDetailAtr;
        //                iStoreMax = drD["MAX_ORDER_QTY"].ToString().Trim() == "" ? 0 : int.Parse(drD["MAX_ORDER_QTY"].ToString());
        //                iStoreRem = drD["REMAINED_ATR"].ToString().Trim() == "" ? 0 : int.Parse(drD["REMAINED_ATR"].ToString());
        //                strPRODNO = drD["PRODNO"].ToString().Trim();
        //                isMasterData = false;
        //            }


        //            //當日ATR量 小於或等於 建議訂購量，則以ATR量與訂購量做比較
        //            if (iStoreAtr <= iStoreRem)
        //            {
        //                if (int.Parse(Qty) > iStoreAtr)
        //                {
        //                    if (isMasterData)
        //                    {
        //                        strStoreAtr_ProdNo = "已超過今日的ATR量!!!";
        //                    }
        //                    else
        //                    {
        //                        strStoreAtr_ProdNo = "的搭贈商品【" + strPRODNO + "】已超過今日的ATR量!!!";
        //                    }
        //                }
        //            }
        //            else //當日ATR量 大於 建議訂購量，則以最大訂購量與訂購量做比較
        //            {
        //                if (int.Parse(Qty) > iStoreMax)
        //                {
        //                    if (isMasterData)
        //                    {
        //                        strStoreAtr_ProdNo = "已超過今日的最大訂購量!!!";
        //                    }
        //                    else
        //                    {
        //                        strStoreAtr_ProdNo = "的搭贈商品【" + strPRODNO + "】已超過今日的最大訂購量!!!";
        //                    }
        //                }
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //    finally
        //    {
        //        if (objConn.State == ConnectionState.Open) objConn.Close();
        //        objConn.Dispose();
        //        OracleConnection.ClearAllPools();
        //    }

        //    return strStoreAtr_ProdNo;
        //}
    }
}
