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
    public class CON06_Facade
    {
        public DataTable Query_CsmProductType(string ProdTypeNo)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT PRODTYPENO,PRODTYPENAME ");
            sb.Append("FROM CSM_PRODUCT_TYPE ");
            sb.Append("WHERE 1 =1 ");
            sb.Append(" AND PRODTYPENO = " + OracleDBUtil.SqlStr(ProdTypeNo));

            DataTable dt = OracleDBUtil.Query_Data(sb.ToString());
            return dt;
        }

        public string Query_StoreAtr(string ProdNo, string StoreNo) //商品ATR量
        {
            string iStoreAtr = "0";
            StringBuilder sb = new StringBuilder();
            sb.Append(" select PROD.PRODNO,  PSA.REMAINED_ATR  ");
            sb.Append("  FROM   LOC");
            sb.Append("  INNER JOIN POS_ATR PA");
            sb.Append("  ON LOC.LOC_ID = PA.LOC_ID");
            sb.Append("  AND to_char(TRANS_DATE,'yyyyMMdd')= to_char((select WorkingDay(" + OracleDBUtil.SqlStr(StoreNo) + ") v_Work_D from dual),'yyyyMMdd')");
            sb.Append("  INNER JOIN PRODUCT PROD");
            sb.Append("  ON PA.PRODNO = PROD.PRODNO");
            sb.Append("  INNER JOIN POS_STORE_ATR PSA");
            sb.Append("  ON PA.PRODNO = PSA.PRODNO");
            sb.Append("  AND PSA.TRANS_DATE= (select WorkingDay(" + OracleDBUtil.SqlStr(StoreNo) + ") v_Work_D from dual)");
            sb.Append(" where LOC.LOC_ID = INV_GOODLOCUUID ");
            sb.Append(" AND PA.ATRQTY > 0 ");
            sb.Append(" AND PSA.REMAINED_ATR > 0");
            sb.Append(" AND PSA.STORE_NO = " + OracleDBUtil.SqlStr(StoreNo));
            sb.Append(" AND  PROD.S_DATE <=  (select WorkingDay(" + OracleDBUtil.SqlStr(StoreNo) + ") v_Work_D from dual)");
            sb.Append(" AND  PROD.E_DATE <=  (select WorkingDay(" + OracleDBUtil.SqlStr(StoreNo) + ") v_Work_D from dual)");

            sb.Append(" AND PROD.PRODNO = " + OracleDBUtil.SqlStr(ProdNo));

            DataTable dt = OracleDBUtil.Query_Data(sb.ToString());

            if (dt.Rows.Count > 0)
            {
                iStoreAtr = dt.Rows[0]["REMAINED_ATR"].ToString().Trim() == "" ? "0" : dt.Rows[0]["REMAINED_ATR"].ToString();

            }

            return iStoreAtr;
        }

        public DataTable Query_CsmOrderM(string ORDNO)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT * ");
            sb.Append("FROM CSM_ORDERM ");
            sb.Append("WHERE 1 =1 ");
            sb.Append(" AND ORDNO = " + OracleDBUtil.SqlStr(ORDNO));

            DataTable dt = OracleDBUtil.Query_Data(sb.ToString());
            return dt;

        }

        public DataTable Query_CsmOrderD(string ORDERMID)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT SEQNO ITEMS,");
            sb.Append(" CD.PRODNO,PT.PRODNAME, ");
            sb.Append(" CD.PRODTYPENO,CE.PRODTYPENAME, ");
            sb.Append(" CD.ADVISEQTY,CD.ORDQTY,PT.PRICE,(CD.ORDQTY * PT.PRICE) AMOUNT");
            sb.Append(" FROM CSM_ORDERD CD ");
            sb.Append(" LEFT JOIN PRODUCT PT ON CD.PRODNO=PT.PRODNO ");
            sb.Append(" LEFT JOIN CSM_PRODUCT_TYPE CE ON CD.PRODTYPENO=CE.PRODTYPENO ");
            sb.Append(" WHERE 1 =1 ");
            sb.Append(" AND CSM_ORDERM_ID = " + OracleDBUtil.SqlStr(ORDERMID));
            sb.Append(" ORDER BY 1 ");

            DataTable dt = OracleDBUtil.Query_Data(sb.ToString());
            return dt;

        }

        public int SaveOrderData(DataSet ORDER, string OrderNo)
        {
            int intResult = 0;
            OracleConnection objConn = null;
            OracleTransaction objTx = null;
            try
            {
                objConn = OracleDBUtil.GetConnection();
                objTx = objConn.BeginTransaction();

                if (OrderNo != "")
                {
                    //先刪除Detail
                    OracleDBUtil.ExecuteSql(objTx,
                        @"delete CSM_ORDERD where CSM_ORDERM_ID = (SELECT CSM_ORDERM_ID FROM CSM_ORDERM WHERE ORDNO = " + OracleDBUtil.SqlStr(OrderNo) + ")");

                    OracleDBUtil.ExecuteSql(objTx,
                       @"delete CSM_ORDERM where ORDNO = " + OracleDBUtil.SqlStr(OrderNo));

                }

                intResult += OracleDBUtil.Insert(objTx, ORDER.Tables["CSM_ORDERM"]);
                intResult += OracleDBUtil.Insert(objTx, ORDER.Tables["CSM_ORDERD"]);

                objTx.Commit();
            }

            catch (Exception ex)
            {
                objTx.Rollback();
                throw ex;
            }
            finally
            {
                objTx.Dispose();
                if (objConn.State == ConnectionState.Open) objConn.Close();
                objConn.Dispose();
                OracleConnection.ClearAllPools();
            }
            return intResult;
        }

        //作廢
        public void DropOrderData(string OrderNo)
        {

            OracleConnection objConn = null;
            OracleTransaction objTx = null;
            try
            {
                objConn = OracleDBUtil.GetConnection();
                objTx = objConn.BeginTransaction();

                if (OrderNo != "")
                {
                    //先刪除Detail
                    OracleDBUtil.ExecuteSql(objTx,
                        @"delete CSM_ORDERD where CSM_ORDERM_ID = (SELECT CSM_ORDERM_ID FROM CSM_ORDERM WHERE ORDNO = " + OracleDBUtil.SqlStr(OrderNo) + ")");

                    OracleDBUtil.ExecuteSql(objTx,
                       @"delete CSM_ORDERM where ORDNO = " + OracleDBUtil.SqlStr(OrderNo));

                }


                objTx.Commit();
            }

            catch (Exception ex)
            {
                objTx.Rollback();
                throw ex;
            }
            finally
            {
                objTx.Dispose();
                if (objConn.State == ConnectionState.Open) objConn.Close();
                objConn.Dispose();
                OracleConnection.ClearAllPools();
            }
        }

        public int SaleToOrder(string ProdNo, string STORENO, string SUPPId)
        {
            int intResult = -1;
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT FN_INV_TO_ORD_COUNT(" + OracleDBUtil.SqlStr(STORENO) + "," + OracleDBUtil.SqlStr(ProdNo) + ","  + OracleDBUtil.SqlStr(SUPPId) + ") as ORD_COUNT FROM DUAL ");


            DataTable dt = OracleDBUtil.Query_Data(sb.ToString());
            if (dt.Rows.Count > 0)
            {
                intResult = Convert.ToInt32(dt.Rows[0]["ORD_COUNT"]);
            }
            return intResult;
        }
    }
}
