using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

using FET.POS.Model.DTO;
using Advtek.Utility;
using System.Data.OracleClient;

namespace FET.POS.Model.Facade.FacadeImpl
{
    public class ORD12_Facade
    {
        /// <summary>
        /// 新增預訂單
        /// </summary>
        /// <param name="ds"></param>
        public void InsertProOrder(ORD12_PreOrder_DTO ds)
        {
            OracleConnection objConn = null;
            OracleTransaction objTX = null;

            try
            {
                objConn = OracleDBUtil.GetConnection();
                objTX = objConn.BeginTransaction();

                OracleDBUtil.Insert(objTX, ds.PRE_ORDER_M);
                OracleDBUtil.Insert(objTX, ds.PRE_ORDER_D);

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

        /// <summary>
        /// 更新預訂單
        /// </summary>
        /// <param name="ds"></param>
        public void UpdateProOrder(ORD12_PreOrder_DTO ds)
        {
            OracleConnection objConn = null;
            OracleTransaction objTX = null;

            try
            {
                objConn = OracleDBUtil.GetConnection();
                objTX = objConn.BeginTransaction();


                if ((ds.PRE_ORDER_D).Count > 0)
                {
                    //更新主檔
                    OracleDBUtil.UPDDATEByUUID(objTX, ds.PRE_ORDER_M, "PRE_ORDER_M_ID");

                    //刪除明細
                    OracleDBUtil.ExecuteSql(objTX, @"delete  from PRE_ORDER_D  Where  PRE_ORDER_M_ID = " + OracleDBUtil.SqlStr(ds.PRE_ORDER_M.Rows[0]["PRE_ORDER_M_ID"].ToString()));
                   
                    //新增明細
                    OracleDBUtil.Insert(objTX, ds.PRE_ORDER_D);
                }
                else
                {
                    //刪除明細
                    OracleDBUtil.ExecuteSql(objTX, @"delete from PRE_ORDER_D  Where  PRE_ORDER_M_ID = " + OracleDBUtil.SqlStr(ds.PRE_ORDER_M.Rows[0]["PRE_ORDER_M_ID"].ToString()));

                    //刪除主檔
                    OracleDBUtil.DELETEByUUID(objTX, ds.PRE_ORDER_M, "PRE_ORDER_M_ID");
                }

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

        //查詢預訂單主檔資料
        public string GetPreOrderMID(string StoreNo)
        {
            string returnValue = "";
            StringBuilder sb = new StringBuilder();
            //取得主檔資料
            sb.Append(@"select PRE_ORDER_M_ID 
                            from PRE_ORDER_M
                            where STATUS='11' And ROWNUM=1
                            and STORE_NO=" + OracleDBUtil.SqlStr(StoreNo));

            DataTable dt = OracleDBUtil.Query_Data(sb.ToString());
            if (dt.Rows.Count > 0)
            {
                returnValue = dt.Rows[0]["PRE_ORDER_M_ID"].ToString();
            }

            return returnValue;
        }

        //查詢預訂單明細資料
        public DataTable GetPreOrderD(string PreOrderMID, string STORE_NO)
        {
            //取得明細資料
            string strSQL = @"select   PRE_ORDER_D_ID ,d.PRE_ORDER_SEQNO,d.ES_QTY,d.STOCK_QTY,nvl((SELECT INWAY_QTY(" + OracleDBUtil.SqlStr(STORE_NO) + ",d.PRODNO)  FROM dual),0) as INWAYQTY   ";
            strSQL = strSQL + ",d.ADVISEQTY,d.ORDQTY,d.REMARK ,d.ULSNO,d.PRODNO_M,d.QTY_BDATE     ";
            strSQL = strSQL + ",d.QTY_EDATE         ";
            strSQL = strSQL + ",d.CREATE_USER       ";
            strSQL = strSQL + ",d.CREATE_DTM        ";
            strSQL = strSQL + ",d.MODI_USER         ";
            strSQL = strSQL + ",d.MODI_DTM          ";
            strSQL = strSQL + ",d.PRODNO            ";
            strSQL = strSQL + ",d.GIFT_FLAG         ";
            strSQL = strSQL + ",d.APPROVEQTY        ";
            strSQL = strSQL + ",d.REAL_ORDER_QTY    ";
            strSQL = strSQL + ",d.FAIL_REASON       ";
            strSQL = strSQL + ",d.ORDER_ITEMS_ID    ";
            strSQL = strSQL + ",d.PRE_ORDER_M_ID   , p.PRODNAME ";
            strSQL = strSQL + " from PRE_ORDER_D d ";
            strSQL = strSQL + " left join PRODUCT p ";
            strSQL = strSQL + "on d.PRODNO=p.PRODNO ";
            strSQL = strSQL + "where d.GIFT_FLAG='0'   ";
            strSQL = strSQL + "and d.PRE_ORDER_M_ID=" + OracleDBUtil.SqlStr(PreOrderMID);
            strSQL = strSQL + " order by d.PRE_ORDER_SEQNO ";

            DataTable returnValue = OracleDBUtil.Query_Data(strSQL);
            return returnValue;
        }
    }
}
