using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.OracleClient;
using Advtek.Utility;
using FET.POS.Model.DTO;
using FET.POS.Model.Common;

namespace FET.POS.Model.Facade.FacadeImpl
{
    public class INV25_Facade
    {
        /// <summary>
        /// 取得查詢結果移出作業(Master Data)
        /// </summary>
        /// <param name="STNO">移撥單號</param>
        /// <param name="PRODNAME">商品名稱</param>
        /// <param name="TSTATUS">移撥狀態</param>
        /// <param name="STDATE_S">移出日期(起)</param>
        /// <param name="STDATE_E">移出日期(迄)</param>
        /// <param name="STORENAME">撥入門市名稱</param>
        /// <returns>查詢結果</returns>
        public DataTable Query_StoreTransferM(string STNO, string PRODNO, string PRODNAME,
            string TSTATUS, string STDATE_S, string STDATE_E, string STORENO, string STORENAME,
            string UserRole, string UserStore, string RoleHQ)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT DISTINCT M.STNO, M.TO_STORE_NO, S.STORENAME, M.STDATE, ");
            sb.Append("M.TSTDATE, M.TSTATUS, M.MODI_USER, E.EMPNAME, TO_CHAR(M.MODI_DTM, 'yyyy/mm/dd hh24:mi:ss') as MODI_DTM ");
            sb.Append("FROM STORETRANSFER_M M, STORETRANSFER_D D, PRODUCT P, STORE S, EMPLOYEE E ");
            sb.Append("WHERE 1=1 ");
            sb.Append("AND M.STNO = D.STNO AND D.PRODNO = P.PRODNO AND M.TO_STORE_NO = S.STORE_NO AND  M.MODI_USER= E.EMPNO(+) ");

            if (UserRole != RoleHQ)  //非總部人員，只能看到自己門市移出的商品
            {
                sb.Append("AND M.FROM_STORE_NO = " + OracleDBUtil.SqlStr(UserStore));
            }

            if (!string.IsNullOrEmpty(STNO))
            {
                sb.Append(" AND M.STNO LIKE " + OracleDBUtil.LikeStr(STNO));
            }

            if (!string.IsNullOrEmpty(PRODNO))
            {
                sb.Append(" AND D.PRODNO = " + OracleDBUtil.SqlStr(PRODNO));
            }


            if (!string.IsNullOrEmpty(PRODNAME))
            {
                sb.Append(" AND P.PRODNAME LIKE " + OracleDBUtil.LikeStr(PRODNAME));
            }

            if (!string.IsNullOrEmpty(TSTATUS))
            {
                sb.Append(" AND M.TSTATUS = " + OracleDBUtil.SqlStr(TSTATUS));
            }

            if (!string.IsNullOrEmpty(STDATE_S))
            {
                sb.Append(" AND to_date(M.STDATE, 'YYYY/MM/DD') >= " + OracleDBUtil.DateStr(STDATE_S));
            }

            if (!string.IsNullOrEmpty(STDATE_E))
            {
                sb.Append(" AND to_date(M.STDATE, 'YYYY/MM/DD') <= " + OracleDBUtil.DateStr(STDATE_E));
            }

            if (!string.IsNullOrEmpty(STORENO))
            {
                sb.Append(" AND M.TO_STORE_NO = " + OracleDBUtil.SqlStr(STORENO));
            }

            if (!string.IsNullOrEmpty(STORENAME))
            {
                sb.Append(" AND S.STORENAME LIKE " + OracleDBUtil.LikeStr(STORENAME));
            }

            sb.Append(" ORDER BY M.STNO DESC, M.TO_STORE_NO ");

            DataTable dt = OracleDBUtil.Query_Data(sb.ToString());
            return dt;
        }

        /// <summary>
        /// 取得查詢結果移出作業(Master Data)
        /// </summary>
        /// <param name="STNO">移撥單號</param>
        /// <param name="PRODNO">商品料號</param>
        /// <param name="PRODNAME">商品名稱</param>
        /// <param name="TSTATUS">移撥狀態</param>
        /// <param name="STDATE_S">移出日期(起)</param>
        /// <param name="STDATE_E">移出日期(迄)</param>
        /// <param name="STORENO">撥入門市編號</param>
        /// <param name="STORENAME">撥入門市名稱</param>
        /// <param name="UserRole">登入者角色(店長、店員、總部人員)</param>
        /// <param name="UserStore">登入者門市</param>
        /// <returns>查詢結果</returns>
        public DataTable Query_StoreTransferM_ByINV26(string STNO, string PRODNO, string PRODNAME,
            string TSTATUS, string STDATE_S, string STDATE_E, string STORENO, string STORENAME,
            string UserRole, string UserStore, string RoleHQ)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT DISTINCT M.STNO, M.TO_STORE_NO, M.FROM_STORE_NO, S.STORENAME, M.STDATE, ");
            sb.Append("M.TSTDATE, M.TSTATUS, M.MODI_USER, E.EMPNAME, TO_CHAR(M.MODI_DTM, 'yyyy/mm/dd hh24:mi:ss') as MODI_DTM ");
            sb.Append("FROM STORETRANSFER_M M, STORETRANSFER_D D, PRODUCT P, STORE S, EMPLOYEE E ");
            sb.Append("WHERE 1=1 ");
            sb.Append("AND M.STNO = D.STNO AND D.PRODNO = P.PRODNO AND M.FROM_STORE_NO = S.STORE_NO AND  M.MODI_USER= E.EMPNO(+) ");

            if (UserRole != RoleHQ)  //非總部人員，只能看到撥入至自己門市的商品
            {
                sb.Append("AND M.TO_STORE_NO = " + OracleDBUtil.SqlStr(UserStore));
            }

            if (!string.IsNullOrEmpty(STNO))
            {
                sb.Append(" AND M.STNO LIKE " + OracleDBUtil.LikeStr(STNO));
            }

            if (!string.IsNullOrEmpty(PRODNO))
            {
                sb.Append(" AND D.PRODNO = " + OracleDBUtil.SqlStr(PRODNO));
            }

            if (!string.IsNullOrEmpty(PRODNAME))
            {
                sb.Append(" AND P.PRODNAME LIKE " + OracleDBUtil.LikeStr(PRODNAME));
            }

            if (!string.IsNullOrEmpty(TSTATUS))
            {
                sb.Append(" AND M.TSTATUS = " + OracleDBUtil.SqlStr(TSTATUS));
            }

            if (!string.IsNullOrEmpty(STDATE_S))
            {
                sb.Append(" AND to_date(M.TSTDATE, 'YYYY/MM/DD') >= " + OracleDBUtil.DateStr(STDATE_S));
            }

            if (!string.IsNullOrEmpty(STDATE_E))
            {
                sb.Append(" AND to_date(M.TSTDATE, 'YYYY/MM/DD') <= " + OracleDBUtil.DateStr(STDATE_E));
            }

            if (!string.IsNullOrEmpty(STORENO))
            {
                sb.Append(" AND M.FROM_STORE_NO = " + OracleDBUtil.SqlStr(STORENO));
            }


            if (!string.IsNullOrEmpty(STORENAME))
            {
                sb.Append(" AND S.STORENAME LIKE " + OracleDBUtil.LikeStr(STORENAME));
            }

            sb.Append(" ORDER BY M.STNO DESC, M.TO_STORE_NO ");

            DataTable dt = OracleDBUtil.Query_Data(sb.ToString());
            return dt;
        }

        /// <summary>
        /// 取得查詢結果移出作業(Detail Data)
        /// </summary>
        /// <param name="STNO">移撥單號</param>
        /// <returns>查詢結果</returns>
        public DataTable Query_StoreTransferD(string STNO)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT DISTINCT D.STORETRANSFER_D_ID, D.SEQNO, D.STNO, D.PRODNO, P.PRODNAME, D.TRANOUTQTY, D.TRANINQTY ");
            sb.Append(", (SELECT I.IMEI FROM STORETRANSFER_IMEI I WHERE D.STORETRANSFER_D_ID = I.STORETRSF_D_ID AND ROWNUM = 1) IMEI ");
            sb.Append(", (SELECT COUNT(I.IMEI) FROM STORETRANSFER_IMEI I WHERE D.STORETRANSFER_D_ID = I.STORETRSF_D_ID) AS IMEI_QTY ");
            sb.Append(", P.IMEI_FLAG ");
            sb.Append("FROM STORETRANSFER_D D, PRODUCT P, STORETRANSFER_IMEI I ");
            sb.Append("WHERE 1 =1 ");
            sb.Append("AND D.PRODNO = P.PRODNO AND D.STORETRANSFER_D_ID = I.STORETRSF_D_ID(+) ");
            sb.Append("AND P.COMPANYCODE = '01' ");
            sb.Append("AND P.DEL_FLAG = 'N' ");
            sb.Append("AND D.STNO = " + OracleDBUtil.SqlStr(STNO));

            DataTable dt = OracleDBUtil.Query_Data(sb.ToString());
            return dt;
        }

        /// <summary>
        /// 查詢調撥作業中商品的IMEI
        /// </summary>
        /// <param name="STNO">調撥單號</param>
        /// <param name="SEQNO">移出作業明細的序號</param>
        /// <param name="PRODNO">商品料號</param>
        /// <returns>DataTable</returns>
        public DataTable Query_StoreTransferIMEI(string STORETRSF_D_ID, string PRODNO)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(" SELECT IMEI ");
            sb.Append(" FROM STORETRANSFER_IMEI");
            sb.Append(" WHERE 1 =1 ");
            sb.Append(" AND STORETRANSFER_D_ID = " + OracleDBUtil.SqlStr(STORETRSF_D_ID));
            sb.Append(" AND PRODNO = " + OracleDBUtil.SqlStr(PRODNO));

            DataTable dt = OracleDBUtil.Query_Data(sb.ToString());
            return dt;
        }

        /// <summary>
        /// 新增移出作業
        /// </summary>
        /// <param name="ds">INV25 DataSet</param>
        public void AddNewOne_StoreTransfer(INV25_StoreTransfer_DTO ds, string HOST_ID)
        {
            OracleConnection objConn = null;
            OracleTransaction objTX = null;

            try
            {
                INVENTORY_Facade Inventory = new INVENTORY_Facade();
                objConn = OracleDBUtil.GetConnection();
                objTX = objConn.BeginTransaction();

                OracleDBUtil.Insert(objTX, ds.Tables["STORETRANSFER_M"]);
                OracleDBUtil.Insert(objTX, ds.Tables["STORETRANSFER_D"]);
                               
                foreach (INV25_StoreTransfer_DTO.STORETRANSFER_DRow dr in ds.STORETRANSFER_D.Rows)
                {
                   string Code = "";
                   string Message ="";
                   string Stock = Common_PageHelper.GetGoodLOCUUID();

                   Inventory.PK_INVENTORY_SHIFTSTOCK(objTX, "1", "SO", dr.PRODNO,
                       ds.Tables["STORETRANSFER_M"].Rows[0]["FROM_STORE_NO"].ToString(),
                       Stock, dr.STNO, Convert.ToInt32(dr.TRANOUTQTY),
                       dr.MODI_USER, dr.STORETRANSFER_D_ID, ref Code, ref Message);

                   if (Code != "000") throw new Exception("移撥單號：" + dr.STNO + ", 商品料號：" + dr.PRODNO + ", 異動庫存檔失敗. ERROR_MSG:" + Message);


                   Call_SP_ChangeStore(objTX, dr.STORETRANSFER_D_ID, dr.PRODNO, ds.Tables["STORETRANSFER_M"].Rows[0]["FROM_STORE_NO"].ToString()
                       , ds.Tables["STORETRANSFER_M"].Rows[0]["TO_STORE_NO"].ToString(), HOST_ID, dr.CREATE_USER, ref Code, ref Message);

                   if (Code == "999") throw new Exception("移撥單號：" + dr.STNO + ", 商品料號：" + dr.PRODNO + ", 變更IMEI狀態失敗. ERROR_MSG:" + Message);

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

                if (objConn.State == ConnectionState.Open)
                    objConn.Close();
                objConn.Dispose();
                OracleConnection.ClearAllPools();
            }

        }

        /// <summary>
        /// 修改移出作業(Detail Data)
        /// </summary>
        /// <param name="ds">INV25 DataSet</param>
        public void UpdateOne_StoreTransfer(INV25_StoreTransfer_DTO ds, string HOST_ID)
        {
            OracleConnection objConn = null;
            OracleTransaction objTX = null;

            try
            {
                INVENTORY_Facade Inventory = new INVENTORY_Facade();
                objConn = OracleDBUtil.GetConnection();
                objTX = objConn.BeginTransaction();

                OracleDBUtil.UPDDATEByUUID(objTX, ds.Tables["STORETRANSFER_M"], "STNO");
                OracleDBUtil.UPDDATEByUUID(objTX, ds.Tables["STORETRANSFER_D"], "STORETRANSFER_D_ID");

                foreach (INV25_StoreTransfer_DTO.STORETRANSFER_DRow dr in ds.STORETRANSFER_D.Rows)
                {
                    string Code = "";
                    string Message = "";
                    string Stock = Common_PageHelper.GetGoodLOCUUID();

                    Inventory.PK_INVENTORY_SHIFTSTOCK(objTX, "1", "SI", dr.PRODNO,
                        ds.Tables["STORETRANSFER_M"].Rows[0]["TO_STORE_NO"].ToString(),
                        Stock, dr.STNO, Convert.ToInt32(dr.TRANINQTY),
                        dr.MODI_USER, dr.STORETRANSFER_D_ID, ref Code, ref Message);

                    if (Code != "000") throw new Exception("999移撥單號：" + dr.STNO + ", 商品料號：" + dr.PRODNO + "異動庫存檔失敗. ERROR_MSG:" + Message);

                    Call_SP_ChangeStoreCheckIn(objTX, dr.STORETRANSFER_D_ID, ds.Tables["STORETRANSFER_M"].Rows[0]["TO_STORE_NO"].ToString(), ds.Tables["STORETRANSFER_M"].Rows[0]["FROM_STORE_NO"].ToString(),
                        dr.PRODNO, HOST_ID, dr.MODI_USER, dr.STNO, ds.Tables["STORETRANSFER_M"].Rows[0]["TSTDATE"].ToString(), ref Code, ref Message);

                    if (Code == "999") throw new Exception("999移撥單號：" + dr.STNO + ", 商品料號：" + dr.PRODNO + ", 變更IMEI狀態失敗. ERROR_MSG:" + Message);

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

                if (objConn.State == ConnectionState.Open)
                    objConn.Close();
                objConn.Dispose();
                OracleConnection.ClearAllPools();
            }
        }

        /// <summary>
        /// 取得列印移出單的資料
        /// </summary>
        /// <param name="STNO">移撥單號</param>
        /// <returns>DataTable</returns>
        public DataTable Query_PrintStkChk(string STNO)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT DISTINCT D.PRODNO AS 商品料號, P.PRODNAME AS 商品名稱, ");
            sb.Append("D.TRANOUTQTY AS 數量, ");
            sb.Append(" FN_STORETRANSFER_IMEI(D.STORETRANSFER_D_ID) AS IMEI  ");
            sb.Append("FROM STORETRANSFER_D D, PRODUCT P, STORETRANSFER_IMEI M ");
            sb.Append("WHERE D.PRODNO = P.PRODNO AND D.STORETRANSFER_D_ID = M.STORETRSF_D_ID(+) ");
            sb.Append("AND D.STNO = " + OracleDBUtil.SqlStr(STNO));
            sb.Append("ORDER BY D.PRODNO ");

            DataTable dt = OracleDBUtil.Query_Data(sb.ToString());
            return dt;
        }

        /// <summary>
        /// 變更IMEI狀態 (移出)
        /// </summary>
        /// <param name="objTX"></param>
        /// <param name="inSTNO">移撥單號</param>
        /// <param name="inSEQNO">項次</param>
        /// <param name="inPRODNO">商品料號</param>
        /// <param name="inOut_Store">移出門市</param>
        /// <param name="inIn_Store">撥入門市</param>
        /// <param name="inHOST_ID"></param>
        /// <param name="inUSER_ID">調撥人員</param>
        /// <param name="outMSGCODE"></param>
        /// <param name="outMESSAGE"></param>
        private void Call_SP_ChangeStore(OracleTransaction objTX, string inSTORETRSF_D_ID, string inPRODNO,
            string inOut_Store, string inIn_Store, string inHOST_ID, string inUSER_ID, ref string outMSGCODE, ref string outMESSAGE)
        {
            OracleCommand oraCmd = null;

            try
            {
                oraCmd = new OracleCommand("SP_IMEI_STORETRANSFER_OUT");
                oraCmd.CommandType = System.Data.CommandType.StoredProcedure;
                oraCmd.Parameters.Add(new OracleParameter("inSTORETRSF_D_ID", inSTORETRSF_D_ID));
                oraCmd.Parameters.Add(new OracleParameter("inPRODNO", inPRODNO));
                oraCmd.Parameters.Add(new OracleParameter("inOut_Store", inOut_Store));
                oraCmd.Parameters.Add(new OracleParameter("inIn_Store", inIn_Store));
                oraCmd.Parameters.Add(new OracleParameter("inHOST_ID", inHOST_ID));
                oraCmd.Parameters.Add(new OracleParameter("inUSER_ID", inUSER_ID));
                oraCmd.Parameters.Add(new OracleParameter("outMSGCODE", OracleType.VarChar, 2000)).Direction = ParameterDirection.Output;
                oraCmd.Parameters.Add(new OracleParameter("outMESSAGE", OracleType.VarChar, 2000)).Direction = ParameterDirection.Output;

                oraCmd.Connection = objTX.Connection;
                oraCmd.Transaction = objTX;
                oraCmd.ExecuteNonQuery();

                outMSGCODE = oraCmd.Parameters["outMSGCODE"].Value.ToString();
                outMESSAGE = oraCmd.Parameters["outMESSAGE"].Value.ToString(); 
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                oraCmd.Dispose();
            }
        }


        /// <summary>
        /// 變更IMEI狀態 (撥入)
        /// </summary>
        /// <param name="objTX"></param>
        /// <param name="inSEQNO">項次</param>
        /// <param name="inIVRCODE">撥入門市代碼</param>
        /// <param name="inPRODNO">商品料號</param>
        /// <param name="inHOST_ID"></param>
        /// <param name="inUSER_ID"></param>
        /// <param name="inOrder_NO">移撥單號</param>
        /// <param name="inORDER_DATE">撥入日期</param>
        /// <param name="outMSGCODE"></param>
        /// <param name="outMESSAGE"></param>
        private void Call_SP_ChangeStoreCheckIn(OracleTransaction objTX, string inSTORETRSF_D_ID, string inIVRCODE, string inFROM_STORE_NO, string inPRODNO
            , string inHOST_ID, string inUSER_ID, string inOrder_NO, string inORDER_DATE, ref string outMSGCODE, ref string outMESSAGE)
        {

            OracleCommand oraCmd = null;

            try
            {
                oraCmd = new OracleCommand("SP_IMEI_STORETRANSFER_IN");
                oraCmd.CommandType = System.Data.CommandType.StoredProcedure;

                oraCmd.Parameters.Add(new OracleParameter("inSTORETRSF_D_ID", inSTORETRSF_D_ID));
                oraCmd.Parameters.Add(new OracleParameter("inIVRCODE", inIVRCODE));
                oraCmd.Parameters.Add(new OracleParameter("inPRODNO", inPRODNO));
                oraCmd.Parameters.Add(new OracleParameter("inHOST_ID", inHOST_ID));
                oraCmd.Parameters.Add(new OracleParameter("inUSER_ID", inUSER_ID));
                oraCmd.Parameters.Add(new OracleParameter("inOrder_NO", inOrder_NO));
                oraCmd.Parameters.Add(new OracleParameter("inORDER_DATE", Convert.ToDateTime(inORDER_DATE)));
                oraCmd.Parameters.Add(new OracleParameter("inOE_NO", ""));
                oraCmd.Parameters.Add(new OracleParameter("inPO_NO", ""));
                oraCmd.Parameters.Add(new OracleParameter("outMSGCODE", OracleType.VarChar, 2000)).Direction = ParameterDirection.Output;
                oraCmd.Parameters.Add(new OracleParameter("outMESSAGE", OracleType.VarChar, 2000)).Direction = ParameterDirection.Output;

                oraCmd.Connection = objTX.Connection;
                oraCmd.Transaction = objTX;
                oraCmd.ExecuteNonQuery();

                outMSGCODE = oraCmd.Parameters["outMSGCODE"].Value.ToString();
                outMESSAGE = oraCmd.Parameters["outMESSAGE"].Value.ToString(); 
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                oraCmd.Dispose();
            }
        }

    }
}
