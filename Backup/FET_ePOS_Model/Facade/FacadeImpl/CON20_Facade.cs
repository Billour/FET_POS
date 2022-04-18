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
    public class CON20_Facade
    {
        /// <summary>
        /// 取得查詢結果寄銷移出作業(Master Data)
        /// </summary>
        /// <param name="STNO">移撥單號</param>
        /// <param name="PRODNAME">商品名稱</param>
        /// <param name="TSTATUS">移撥狀態</param>
        /// <param name="STDATE_S">移出日期(起)</param>
        /// <param name="STDATE_E">移出日期(迄)</param>
        /// <param name="STORENAME">撥入門市名稱</param>
        /// <returns>查詢結果</returns>
        public DataTable Query_CsmStoreTransferM(string STNO, string PRODNO, string PRODNAME,
            string TSTATUS, string STDATE_S, string STDATE_E, string STORENO, string STORENAME,
            string UserRole, string UserStore, string RoleHQ)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(@"SELECT DISTINCT M.CSM_STORETRANSFERM_ID, M.STNO, M.TO_STORE_NO, S1.STORENAME AS TO_STORE_NAME, 
                        S2.STORENAME  AS FROM_STORE_NAME, M.STDATE, M.TSTDATE, M.TSTATUS, M.MODI_USER, E.EMPNAME,
                        TO_CHAR(M.MODI_DTM, 'yyyy/mm/dd hh24:mi:ss') as MODI_DTM 
                        FROM CSM_STORETRANSFER_M M, CSM_STORETRANSFER_D D, PRODUCT P, STORE S1, STORE S2,EMPLOYEE E
                        WHERE 1=1 
                        AND M.CSM_STORETRANSFERM_ID = D.CSM_STORETRANSFERM_ID 
                        AND D.PRODNO = P.PRODNO
                        AND M.TO_STORE_NO = S1.STORE_NO 
                        AND M.FROM_STORE_NO = S2.STORE_NO 
                        AND  M.MODI_USER= E.EMPNO(+)");

            if (UserRole != RoleHQ)  //非總部人員，只能看到自己門市移出的商品
            {
                sb.Append(" AND M.FROM_STORE_NO = " + OracleDBUtil.SqlStr(UserStore));
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
                sb.Append(" AND M.STDATE >= " + OracleDBUtil.DateStr(STDATE_S));
            }

            if (!string.IsNullOrEmpty(STDATE_E))
            {
                sb.Append(" AND  M.STDATE <= " + OracleDBUtil.DateStr(STDATE_E));
            }

            if (!string.IsNullOrEmpty(STORENO))
            {
                sb.Append(" AND M.TO_STORE_NO = " + OracleDBUtil.SqlStr(STORENO));
            }

            if (!string.IsNullOrEmpty(STORENAME))
            {
                sb.Append(" AND S1.STORENAME LIKE " + OracleDBUtil.LikeStr(STORENAME));
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
       /// <param name="UserRole">登入者角色(店長、店員、總部人員)</param>
        /// <param name="UserStore">登入者門市</param>
        /// <param name="FORM_STORE_NAME">移出門市編號</param>
        /// <param name="TSTDATE_S">撥入日期(起)</param>
        /// <param name="TSTDATE_E">撥入日期(迄)</param>
        /// <returns>查詢結果</returns>
        public DataTable Query_StoreTransferM_ByCON21(string STNO, string PRODNO, string PRODNAME,
            string TSTATUS, string STDATE_S, string STDATE_E,  string UserRole, string UserStore, 
            string RoleHQ,string FORM_STORE_NAME,string TSTDATE_S ,string TSTDATE_E)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(@" SELECT DISTINCT M.CSM_STORETRANSFERM_ID, M.STNO, M.TO_STORE_NO, S1.STORENAME AS TO_STORE_NAME, 
                        S2.STORENAME  AS FROM_STORE_NAME, M.STDATE, M.TSTDATE, M.TSTATUS, M.MODI_USER, E.EMPNAME,
                        TO_CHAR(M.MODI_DTM, 'yyyy/mm/dd hh24:mi:ss') as MODI_DTM 
                        FROM CSM_STORETRANSFER_M M, CSM_STORETRANSFER_D D, PRODUCT P, STORE S1, STORE S2,EMPLOYEE E
                        WHERE 1=1 
                        AND M.CSM_STORETRANSFERM_ID = D.CSM_STORETRANSFERM_ID 
                        AND D.PRODNO = P.PRODNO
                        AND M.TO_STORE_NO = S1.STORE_NO 
                        AND M.FROM_STORE_NO = S2.STORE_NO 
                        AND  M.MODI_USER= E.EMPNO(+)");

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

           
            if (!string.IsNullOrEmpty(STDATE_S) )
            {
                sb.Append(" AND TRUNC(M.STDATE) >= " + OracleDBUtil.DateStr(STDATE_S));
            }
            if (!string.IsNullOrEmpty(STDATE_E) )
            {
                sb.Append(" AND TRUNC(M.STDATE) <= " + OracleDBUtil.DateStr(STDATE_E));
            }
            
            if (!string.IsNullOrEmpty(TSTDATE_S) )
            {
                sb.Append(" AND TRUNC(M.TSTDATE) <= " + OracleDBUtil.DateStr(TSTDATE_S));
            }
            if (!string.IsNullOrEmpty(TSTDATE_E) )
            {
                sb.Append(" AND TRUNC(M.TSTDATE) >= " + OracleDBUtil.DateStr(TSTDATE_E));
            }
            
            //移出門市
            if (!string.IsNullOrEmpty(FORM_STORE_NAME))
            {
                sb.Append(" AND S2.STORENAME LIKE " + OracleDBUtil.LikeStr(FORM_STORE_NAME));
            }
            
            sb.Append(" ORDER BY M.STNO DESC, M.TO_STORE_NO ");

            DataTable dt = OracleDBUtil.Query_Data(sb.ToString());
            return dt;
        }

        /// <summary>
        /// 取得查詢結果寄銷移出作業(Detail Data)
        /// </summary>
        /// <param name="CsmStoretransferDId"> CsmStoretransferD </param>
        /// <returns>查詢結果</returns>
        public DataTable Query_CsmStoreTransferD(string CsmStoretransferDId)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(@" SELECT DISTINCT D.CSM_STORETRANSFER_D_ID, D.SEQNO,
                         D.PRODNO, P.PRODNAME, D.TRANOUTQTY, D.TRANINQTY ,PT.PRODTYPENAME
                        FROM CSM_STORETRANSFER_D D, PRODUCT P ,CSM_PRODUCT_TYPE PT
                        WHERE 1 =1 
                        AND D.PRODNO = P.PRODNO
                        AND P.PRODTYPENO = PT.PRODTYPENO (+)
                        AND P.COMPANYCODE = '01' 
                        AND P.ISCONSIGNMENT = 1
                        AND P.DEL_FLAG = 'N' ");
            sb.Append("  AND D.CSM_STORETRANSFERM_ID = " + OracleDBUtil.SqlStr(CsmStoretransferDId));

            DataTable dt = OracleDBUtil.Query_Data(sb.ToString());
            return dt;
        }


        /// <summary>
        /// 新增寄銷移出作業
        /// </summary>
        /// <param name="ds">CON20 DataSet</param>
        public void AddNewOne_StoreTransfer(CON20_CSM_StoreTransfer_DTO ds, string HOST_ID)
        {
            OracleConnection objConn = null;
            OracleTransaction objTX = null;

            try
            {
                INVENTORY_Facade Inventory = new INVENTORY_Facade();
                objConn = OracleDBUtil.GetConnection();
                objTX = objConn.BeginTransaction();

                OracleDBUtil.Insert(objTX, ds.Tables["CSM_STORETRANSFER_M"]);
                OracleDBUtil.Insert(objTX, ds.Tables["CSM_STORETRANSFER_D"]);
                string STNO = ds.Tables["CSM_STORETRANSFER_M"].Rows[0]["STNO"].ToString();
                foreach (CON20_CSM_StoreTransfer_DTO.CSM_STORETRANSFER_DRow dr in ds.Tables["CSM_STORETRANSFER_D"].Rows)
                {
                    string Code = "";
                    string Message = "";
                    string Stock = Common_PageHelper.GetGoodLOCUUID();
                    string FromStoreNo = ds.Tables["CSM_STORETRANSFER_M"].Rows[0]["FROM_STORE_NO"].ToString();
                    string Stno = ds.Tables["CSM_STORETRANSFER_M"].Rows[0]["STNO"].ToString();
                    Inventory.PK_INVENTORY_SHIFTSTOCK(objTX, "1", "SO", dr.PRODNO,
                        FromStoreNo,
                        Stock, Stno, Convert.ToInt32(dr.TRANOUTQTY),
                        dr.MODI_USER, dr.CSM_STORETRANSFER_D_ID, ref Code, ref Message);

                    if (Code != "000") throw new Exception("移撥單號：" + Stno + ", 商品料號：" + dr.PRODNO + ", 異動庫存檔失敗. ERROR_MSG:" + Message);


                    //if (Code == "999") throw new Exception("移撥單號：" + dr.STNO + ", 商品料號：" + dr.PRODNO + ", 變更IMEI狀態失敗. ERROR_MSG:" + Message);

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
        /// 修改寄銷移出作業(Detail Data)
        /// </summary>
        /// <param name="ds">CON20 DataSet</param>
        public void UpdateOne_StoreTransfer(CON20_CSM_StoreTransfer_DTO ds, string HOST_ID)
        {
            OracleConnection objConn = null;
            OracleTransaction objTX = null;

            try
            {
                INVENTORY_Facade Inventory = new INVENTORY_Facade();
                objConn = OracleDBUtil.GetConnection();
                objTX = objConn.BeginTransaction();

                OracleDBUtil.UPDDATEByUUID(objTX, ds.Tables["CSM_STORETRANSFER_M"], "CSM_STORETRANSFERM_ID");
                OracleDBUtil.UPDDATEByUUID(objTX, ds.Tables["CSM_STORETRANSFER_D"], "CSM_STORETRANSFER_D_ID");

                foreach (CON20_CSM_StoreTransfer_DTO.CSM_STORETRANSFER_DRow dr in ds.CSM_STORETRANSFER_D.Rows)
                {
                    string Code = "";
                    string Message = "";
                    string Stock = Common_PageHelper.GetGoodLOCUUID();
                    string ToStoreNo = ds.Tables["CSM_STORETRANSFER_M"].Rows[0]["TO_STORE_NO"].ToString();
                    string Stno = ds.Tables["CSM_STORETRANSFER_M"].Rows[0]["STNO"].ToString();

                    Inventory.PK_INVENTORY_SHIFTSTOCK(objTX, "1", "SI", dr.PRODNO,
                        ToStoreNo,
                        Stock, Stno, Convert.ToInt32(dr.TRANINQTY),
                        dr.MODI_USER, dr.CSM_STORETRANSFER_D_ID, ref Code, ref Message);

                    if (Code != "000") throw new Exception("999移撥單號：" + Stno + ", 商品料號：" + dr.PRODNO + "異動庫存檔失敗. ERROR_MSG:" + Message);

               //     if (Code == "999") throw new Exception("999移撥單號：" + dr.STNO + ", 商品料號：" + dr.PRODNO + ", 變更IMEI狀態失敗. ERROR_MSG:" + Message);

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
        /// <param name="CSM_STORETRANSFERM_ID">移撥單號主檔UUID</param>
        /// <returns>DataTable</returns>
        public DataTable Query_PrintStkChk(string CSM_STORETRANSFERM_ID)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(@" SELECT DISTINCT T.PRODTYPENAME AS 商品類別,D.PRODNO AS 商品料號, P.PRODNAME AS 商品名稱,
                         D.TRANOUTQTY AS 數量
                         FROM CSM_STORETRANSFER_D D, PRODUCT P,CSM_PRODUCT_TYPE T
                         WHERE D.PRODNO = P.PRODNO AND P.PRODTYPENO = T.PRODTYPENO");
            sb.Append("  AND D.CSM_STORETRANSFERM_ID = " + OracleDBUtil.SqlStr(CSM_STORETRANSFERM_ID));
            sb.Append("  ORDER BY D.PRODNO ");

            DataTable dt = OracleDBUtil.Query_Data(sb.ToString());
            return dt;
        }

       
    }
}
