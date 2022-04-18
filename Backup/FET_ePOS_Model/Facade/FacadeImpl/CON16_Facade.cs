using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Advtek.Utility;
using FET.POS.Model.DTO;
using System.Data.OracleClient;

namespace FET.POS.Model.Facade.FacadeImpl
{  
    public class CON16_Facade
    {//SQL 需修改為CSM_CSM_CSM_STOCKCHK_M 、CSM_STOCKCHK_ID、CSM_SUPPLIER、PRODUCT 寄銷商品

        /// <summary>
        /// 取得盤點作業主檔特定一筆資料
        /// </summary>
        /// <param name="STKCHKNO">盤點單號</param>
        /// <returns>查詢結果</returns>
        public DataTable Query_StockChkM_ByKey(string STKCHKNO)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(@"SELECT M.CSM_STOCKCHKM_ID, M.STKCHKNO, M.STKCHK_TYPE, E.EMPNAME, E.EMPNO, 
                            M.STORE_NO, M.STKCHKDATE, S.STORENAME ,M.MODI_USER ,EE.EMPNAME  MODI_NAME
                       FROM CSM_STOCKCHK_M M , EMPLOYEE E , STORE S , EMPLOYEE EE
                            WHERE 1 = 1 AND M.STKCHKNO = " + OracleDBUtil.SqlStr(STKCHKNO)
                    );
            sb.Append(@"
                            AND  M.STKCHK_USERNO = E.EMPNO(+)
                            AND  M.MODI_USER = EE.EMPNO(+)
                            AND  M.STORE_NO = S.STORE_NO(+)
                        ");

            DataTable dt = OracleDBUtil.Query_Data(sb.ToString());
            return dt;
        }

        /// <summary>
        /// 取得盤點作業主檔
        /// </summary>
        /// <param name="STKCHKNO">盤點單號</param>
        /// <param name="STKCHKDATE_S">盤點日期(起)</param>
        /// <param name="STKCHKDATE_E">盤點日期(迄)</param>
        /// <returns>查詢結果</returns>
        public DataTable Query_StockChkM(string STKCHKNO, string STKCHKDATE_S, string STKCHKDATE_E, string STORE_NO, string STATUS)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT * ");
            sb.Append("FROM VW_CON16_QueryStockChkM ");
            sb.Append("WHERE 1 = 1 ");
            sb.Append(" AND STORE_NO =" + OracleDBUtil.SqlStr(STORE_NO.Trim()));

            if (!string.IsNullOrEmpty(STKCHKNO))
            {
                sb.Append(" AND STKCHKNO = " + OracleDBUtil.SqlStr(STKCHKNO.Trim()));
            }

            if (!string.IsNullOrEmpty(STKCHKDATE_S))
            {
                sb.Append(" AND To_Date(STKCHKDATE,'YYYY/MM/DD')  >= " + OracleDBUtil.DateStr(STKCHKDATE_S.Trim()));
            }

            if (!string.IsNullOrEmpty(STKCHKDATE_E))
            {
                sb.Append(" AND To_Date(STKCHKDATE,'YYYY/MM/DD')  <= " + OracleDBUtil.DateStr(STKCHKDATE_E.Trim()));
            }
           
            if (STATUS != "ALL")
            {
                string strName = STATUS == "0" ? "盤點中" : "已盤點";
                sb.Append(" AND STATUS = " + OracleDBUtil.SqlStr(strName));
            }
            DataTable dt = OracleDBUtil.Query_Data(sb.ToString());
            return dt;
        }

        /// <summary>
        /// 取得盤點作業明細檔
        /// </summary>
        /// <param name="STKCHKNO">盤點單號</param>
        /// <returns>查詢結果</returns>
        public DataTable Query_StockChkD(string STKCHKNO)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT * FROM VW_CON16_QueryStockChkD WHERE 1 = 1 AND STKCHKNO = " + OracleDBUtil.SqlStr(STKCHKNO));

            DataTable dt = OracleDBUtil.Query_Data(sb.ToString());
            return dt;
        }

        /// <summary>
        /// 產生門市盤點單
        /// </summary>
        /// <param name="STKCHK_TYPE">盤點型態 1:重盤, 2:全盤, 3:關帳日盤點</param>
        /// <param name="STORE_NO">門市編號</param>
        /// <param name="STKCHKDATE">營業日</param>
        /// <param name="ds">INV11 DataSet</param>
        public void AddNewOne_StockCHK(CONS16_CSM_STOCKCHK_M ds)
        {
            OracleConnection objConn = null;
            OracleTransaction objTX = null;

            try
            {
                objConn = OracleDBUtil.GetConnection();
                objTX = objConn.BeginTransaction();

                OracleDBUtil.Insert(objTX, ds.Tables["CSM_STOCKCHK_M"]);
                
                CONS16_CSM_STOCKCHK_M.CSM_STOCKCHK_MRow dr = ds.CSM_STOCKCHK_M.Rows[0] as CONS16_CSM_STOCKCHK_M.CSM_STOCKCHK_MRow;
                string CSM_STOCKCHKM_ID = dr.CSM_STOCKCHKM_ID;
                string STKCHK_TYPE = dr.STKCHK_TYPE;
                string STORE_NO = dr.STORE_NO;
                string STKCHKDATE = dr.STKCHKDATE;
                string CHK_PERSON = dr.STKCHK_USERNO;

                System.Text.StringBuilder sb = new System.Text.StringBuilder();

                sb.Append(@"INSERT INTO CSM_STOCKCHK_D (STKQTY, CSM_STOCKCHKD_ID,  STKCHKQTY, 
                            DIFF_STKQTY, CHK_PERSON, PRODNO,
                            CREATE_USER, CREATE_DTM, MODI_USER,
                            MODI_DTM, CSM_STOCKCHKM_ID, INV_ONHAND_CURRENT_ID, SUPP_ID) 
                            SELECT C.ON_HAND_QTY, pos_uuid(), NULL, C.ON_HAND_QTY,
                ");

                sb.Append(" " + OracleDBUtil.SqlStr(CHK_PERSON) + ", C.PRODNO, ");
                sb.Append("   " + OracleDBUtil.SqlStr(CHK_PERSON) + ", SYSDATE," + OracleDBUtil.SqlStr(CHK_PERSON) + ", ");
                sb.Append("   SYSDATE, " + OracleDBUtil.SqlStr(CSM_STOCKCHKM_ID) + ", C.INV_ONHAND_CURRENT_ID ");
                sb.Append(@" ,P.SUPP_ID FROM INV_ON_HAND_CURRENT C, PRODUCT P 
                           WHERE 1 = 1 
                        ");
                sb.Append("AND C.STORE_NO = " + OracleDBUtil.SqlStr(STORE_NO) + " ");
                sb.Append(@"AND TO_NUMBER(C.ON_HAND_QTY) > 0   
                            AND P.ISSTOCK = '1' 
                            AND NVL (P.ISCONSIGNMENT, '0') = '1'
                            AND C.PRODNO = P.PRODNO  ");  //沒有庫存量就不用盤點

                if (STKCHK_TYPE == "1")  //重盤(只需盤點該門市在營業日當天有異動的商品)
                {
                    sb.Append(@" AND C.PRODNO IN (SELECT DISTINCT PRODNO FROM INV_TRAN_LOG 
                                   WHERE STORE_NO =" + OracleDBUtil.SqlStr(STORE_NO) + " "
                               + " AND TO_CHAR(INV_TRAN_DTM, 'YYYY/MM/DD') =" + OracleDBUtil.SqlStr(STKCHKDATE) + ") "
                                );
                }

                OracleDBUtil.ExecuteSql(objTX, sb.ToString());

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
        /// 修改門市盤點作業
        /// </summary>
        /// <param name="ds">INV11 DataSet</param>
        public void UpdateOne_StockCHK(CONS16_CSM_STOCKCHK_M ds)
        {
            OracleConnection objConn = null;
            OracleTransaction objTX = null;

            try
            {
                INVENTORY_Facade Inventory = new INVENTORY_Facade();
                objConn = OracleDBUtil.GetConnection();
                objTX = objConn.BeginTransaction();

                OracleDBUtil.UPDDATEByUUID(objTX, ds.Tables["CSM_STOCKCHK_D"], "CSM_STOCKCHKD_ID");

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
        /// 刪除門市盤點作業
        /// </summary>
        /// <param name="STKCHKNO">盤點單號</param>
        public void Delete_StockCHK(string STKCHKNO)
        {
            OracleConnection objConn = null;
            OracleTransaction objTX = null;

            try
            {
                objConn = OracleDBUtil.GetConnection();
                objTX = objConn.BeginTransaction();

                System.Text.StringBuilder sbD = new System.Text.StringBuilder();
                sbD.Append("DELETE CSM_STOCKCHK_D WHERE CSM_STOCKCHKM_ID IN ");
                sbD.Append("(SELECT CSM_STOCKCHKM_ID FROM CSM_STOCKCHK_M WHERE STKCHKNO = " + OracleDBUtil.SqlStr(STKCHKNO) + ") ");

                System.Text.StringBuilder sbM = new System.Text.StringBuilder();
                sbM.Append("DELETE CSM_STOCKCHK_M WHERE STKCHKNO = " + OracleDBUtil.SqlStr(STKCHKNO) + " ");

                OracleDBUtil.ExecuteSql(objTX, sbD.ToString());
                OracleDBUtil.ExecuteSql(objTX, sbM.ToString());

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
        /// 取得列印(空白盤點單)的明細資料
        /// </summary>
        /// <param name="STKCHKNO">盤點單號</param>
        /// <returns>查詢結果</returns>
        public DataTable GetExportData(string STKCHKNO)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT \"商品料號\",\"商品名稱\",\"商品類別\", \"銷售倉\", \"展示倉\", \"租賃倉\" ");
            sb.Append("FROM VW_CON16_EXPORT ");
            sb.Append("WHERE 1 = 1 ");
            sb.Append("AND 盤點單號 = " + OracleDBUtil.SqlStr(STKCHKNO));

            DataTable dt = OracleDBUtil.Query_Data(sb.ToString());
            return dt;
        }

        public void UPDATE_StockChkM(string CSM_STOCKCHKM_ID, string MODI_USER)
        {
            OracleConnection objConn = null;
            OracleTransaction objTX = null;
            try
            {
                objConn = OracleDBUtil.GetConnection();
                objTX = objConn.BeginTransaction();

                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                sb.Append(@" UPDATE
                    CSM_STOCKCHK_M  SET MODI_USER =" + OracleDBUtil.SqlStr(MODI_USER));
                sb.Append("  WHERE CSM_STOCKCHKM_ID = " + OracleDBUtil.SqlStr(CSM_STOCKCHKM_ID));

                OracleDBUtil.ExecuteSql(objTX, sb.ToString());
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

    }
}
