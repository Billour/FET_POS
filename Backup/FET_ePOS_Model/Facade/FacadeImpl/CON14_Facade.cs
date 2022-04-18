using System;
using System.Data;
using System.Data.OracleClient;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Advtek.Utility;
using FET.POS.Model.DTO;

namespace FET.POS.Model.Facade.FacadeImpl
{
    public class CON14_Facade
    {

        /// <summary>
        /// 寄銷M驗收單主檔查詢
        /// </summary>
        /// <param name="ORDER_ID">訂單/主配編號</param>
        /// <param name="OENO">出貨編號</param>
        /// <param name="STATUS">驗收狀態</param>
        /// <param name="PRODNO">商品料號</param>
        /// <param name="ORDERDTS">進貨日期起</param>
        /// <param name="ORDERDTE">進貨日期訖</param>
        /// <param name="SuppNo">廠商編號</param>       
        /// 
        /// <returns></returns>
        public DataTable get_Master_VW_CON13_SELECT(string ORDER_ID, string OENO, string STATUS, string PRODNO, string ORDERDTS, string ORDERDTE, string SuppNo)
        {
            DataTable dt = new DataTable();
            string sqlstr = " SELECT DISTINCT ORDNO,OENO,SUPP_NAME,STATUSNAME,ORDER_DATE , ORDER_USER FROM VW_CON13_SELECT WHERE 1=1 ";

            if (!string.IsNullOrEmpty(ORDER_ID) && ORDER_ID != "ALL")
            {
                sqlstr += " and ORDNO = " + OracleDBUtil.SqlStr(ORDER_ID);
            }

            if (!string.IsNullOrEmpty(OENO))
            {
                sqlstr += " and OENO Like " + OracleDBUtil.LikeStr(OENO);
            }

            if (!string.IsNullOrEmpty(STATUS) && STATUS != "全部")
            {
                sqlstr += " and STATUSNAME = " + OracleDBUtil.SqlStr(STATUS);
            }

            if (!string.IsNullOrEmpty(PRODNO))
            {
                sqlstr += " and  PRODNO Like " + OracleDBUtil.LikeStr(PRODNO);
            }

            if (!string.IsNullOrEmpty(SuppNo) && SuppNo != "-請選擇-")
            {
                sqlstr += " and SUPP_NO = " + OracleDBUtil.SqlStr(SuppNo);
            }

            if (!string.IsNullOrEmpty(ORDERDTS))
            {
                sqlstr += " AND ORDER_DATE >= " + OracleDBUtil.DateStr(ORDERDTS);
            }

            if (!string.IsNullOrEmpty(ORDERDTE))
            {
                sqlstr += " AND ORDER_DATE <= " + OracleDBUtil.DateStr(ORDERDTE);
            }

            OracleConnection conn = OracleDBUtil.GetConnection();
            try
            {
                OracleDataAdapter da = new OracleDataAdapter(sqlstr, conn);
                da.Fill(dt);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (conn.State == ConnectionState.Open) conn.Close();
                conn.Dispose();
                OracleConnection.ClearAllPools();
            }
            return dt;
        }

        /// <summary>
        /// 寄銷驗收單明細查詢
        /// </summary>
        /// <param name="ORDER_ID">訂單/主配編號</param>
        /// 
        /// <returns></returns>
        public DataTable getDetail_VW_CON13_SELECT(string ORDER_ID)
        {
            DataTable dt = new DataTable();
            string sqlstr = " SELECT   OENO,ORDNO,PRODNO,PRODNAME,STATUSNAME,IN_QTY FROM VW_CON13_SELECT WHERE 1=1 ";

            if (!string.IsNullOrEmpty(ORDER_ID))
            {
                sqlstr += " and ORDNO = " + OracleDBUtil.SqlStr(ORDER_ID);
            }



            OracleConnection conn = OracleDBUtil.GetConnection();
            try
            {
                OracleDataAdapter da = new OracleDataAdapter(sqlstr, conn);
                da.Fill(dt);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (conn.State == ConnectionState.Open) conn.Close();
                conn.Dispose();
                OracleConnection.ClearAllPools();
            }
            return dt;
        }

        /// <summary>
        /// 寄銷驗收單明細查詢
        /// </summary>
        /// <param name="OENO">OENO</param>
        /// 
        /// <returns></returns>
        public DataTable getDetail_VW_CON14(string OENO)
        {
            DataTable dt = new DataTable();
            string sqlstr = " SELECT  SUPP_ID,STORE_NO, ORDNO,to_char(ORDER_DATE,'yyyy/mm/dd') ORDER_DATE,PRODNO,PRODNAME,SUPP_NO,SUPP_NAME,SH_QTY QTY,IN_QTY,'' REMARK FROM VW_CON13_SELECT WHERE 1=1 ";

            if (!string.IsNullOrEmpty(OENO))
            {
                sqlstr += " and OENO = " + OracleDBUtil.SqlStr(OENO);
            }
            else
            {
                sqlstr += " and 1=0" ;
            }



            OracleConnection conn = OracleDBUtil.GetConnection();
            try
            {
                OracleDataAdapter da = new OracleDataAdapter(sqlstr, conn);
                da.Fill(dt);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (conn.State == ConnectionState.Open) conn.Close();
                conn.Dispose();
                OracleConnection.ClearAllPools();
            }
            return dt;
        }

        /// <summary>
        /// 取得目前的OENO
        /// </summary>
        /// 
        /// <returns></returns>
        public DataTable get_OENO(bool canEmpty)
        {
            OracleConnection conn = null;
            DataSet objDS = null;
            StringBuilder strSql = null;

            try
            {
                strSql = new StringBuilder();
                strSql.Append("SELECT DISTINCT  OENO FROM CSM_OENO WHERE 1=1 ");
                conn = OracleDBUtil.GetConnection();
                objDS = OracleDBUtil.GetDataSet(conn, strSql.ToString());
                if (canEmpty)
                {
                    DataRow dr = objDS.Tables[0].NewRow();
                    dr["OENO"] = "ALL";
                    objDS.Tables[0].Rows.InsertAt(dr, 0);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (conn.State == ConnectionState.Open) conn.Close();
                conn.Dispose();
                OracleConnection.ClearAllPools();
            }

            return objDS.Tables[0];
        }

        public int SaveOrderData(DataTable CSM_INM, DataTable CSM_IND)
        {
            int intResult = 0;
            OracleConnection objConn = null;
            OracleTransaction objTx = null;
            try
            {
                objConn = OracleDBUtil.GetConnection();
                objTx = objConn.BeginTransaction();
                string strSql = "";
                intResult += OracleDBUtil.Insert(objTx, CSM_INM);
                intResult += OracleDBUtil.Insert(objTx, CSM_IND);
                if (CSM_IND.Rows.Count > 0)
                {
                    foreach (DataRow dr in CSM_IND.Rows)
                    {
                        strSql = "  UPDATE CSM_ORDERD          ";
                        strSql += "           SET IN_QTY =IN_QTY +" + dr["INQTY"];
                        strSql += "           WHERE CSM_ORDERM_ID =NVL((SELECT CSM_ORDERM_ID FROM CSM_ORDERM WHERE ORDNO =" + OracleDBUtil.SqlStr(CSM_INM.Rows[0]["ORDNO"].ToString()) + "),'')";
                        strSql += "           AND PRODNO =" + OracleDBUtil.SqlStr(dr["PRODNO"].ToString ());
                        OracleDBUtil.ExecuteSql(objTx, strSql);
                    }
                }
                  strSql = "  UPDATE CSM_ORDERM          ";
                strSql += "           SET STATUS = '4'    ";
                strSql += "           WHERE ORDNO = " + OracleDBUtil.SqlStr(CSM_INM.Rows[0]["ORDNO"].ToString()) ;
                OracleDBUtil.ExecuteSql(objTx, strSql);
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

    }


}
