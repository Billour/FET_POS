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
    public class INV11_Facade
    {
        /// <summary>
        /// 取得盤點作業主檔特定一筆資料
        /// </summary>
        /// <param name="STKCHKNO">盤點單號</param>
        /// <returns>查詢結果</returns>
        public DataTable Query_StockChkM_ByKey(string STKCHKNO)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(@"SELECT M.STKCHK_M_ID
                                , M.STKCHKNO
                                , M.STKCHK_TYPE
                                , E.EMPNAME
                                , E.EMPNO
                                , M.STORE_NO
                                , M.STKCHKDATE
                                , S.STORENAME 
                                , M.MODI_USER 
                                , EE.EMPNAME  MODI_NAME
                                , DECODE(M.STATUS, '00', '盤點中', '10', '已盤點') STATUS
                       FROM STOCKCHK_M M , EMPLOYEE E , STORE S , EMPLOYEE EE
                            WHERE 1 = 1 
                            AND  M.STKCHK_USERNO = E.EMPNO(+)
                            AND  M.MODI_USER = EE.EMPNO(+)
                            AND  M.STORE_NO = S.STORE_NO(+) "
                       + @" AND M.STKCHKNO = " + OracleDBUtil.SqlStr(STKCHKNO)
            );

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
        public DataTable Query_StockChkM(string STKCHKNO, string STKCHKDATE_S, string STKCHKDATE_E, string STORE_NO)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(@"SELECT * 
                            FROM VW_INV11_QueryStockChkM
                            WHERE 1 = 1 
                            AND STORE_NO =" + OracleDBUtil.SqlStr(STORE_NO.Trim()));

            if (!string.IsNullOrEmpty(STKCHKNO))
            {
                sb.AppendLine(" AND STKCHKNO = " + OracleDBUtil.SqlStr(STKCHKNO.Trim()));
            }

            if (!string.IsNullOrEmpty(STKCHKDATE_S))
            {
                sb.AppendLine(" AND To_Date(STKCHKDATE,'YYYY/MM/DD')  >= " + OracleDBUtil.DateStr(STKCHKDATE_S.Trim()));
            }

            if (!string.IsNullOrEmpty(STKCHKDATE_E))
            {
                sb.AppendLine(" AND To_Date(STKCHKDATE,'YYYY/MM/DD')  <= " + OracleDBUtil.DateStr(STKCHKDATE_E.Trim()));
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
            sb.AppendLine("SELECT * FROM VW_INV11_QueryStockChkD WHERE 1 = 1 AND STKCHKNO = " + OracleDBUtil.SqlStr(STKCHKNO));

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
        public void AddNewOne_StockCHK(INV11_StockCHK_DTO ds)
        {
            OracleConnection objConn = null;
            OracleTransaction objTX = null;

            try
            {
                objConn = OracleDBUtil.GetConnection();
                objTX = objConn.BeginTransaction();

                OracleDBUtil.Insert(objTX, ds.Tables["STOCKCHK_M"]);

                INV11_StockCHK_DTO.STOCKCHK_MRow dr = ds.STOCKCHK_M.Rows[0] as INV11_StockCHK_DTO.STOCKCHK_MRow;
                string STKCHK_M_ID = dr.STKCHK_M_ID;
                string STKCHK_TYPE = dr.STKCHK_TYPE;
                string STORE_NO = dr.STORE_NO;
                string STKCHKDATE = dr.STKCHKDATE;
                string CHK_PERSON = dr.STKCHK_USERNO;

                System.Text.StringBuilder sb = new System.Text.StringBuilder();

                sb.AppendLine(@"INSERT INTO STOCKCHK_D (STKQTY
                                                        , STKCHK_D_ID
                                                        , STKCHKQTY
                                                        , DIFF_STKQTY
                                                        , CHK_PERSON
                                                        , PRODNO
                                                        , CREATE_USER
                                                        , CREATE_DTM
                                                        , MODI_USER
                                                        , MODI_DTM
                                                        , STKCHK_M_ID
                                                        , INV_ONHAND_CURRENT_ID) 

                                SELECT    C.ON_HAND_QTY
                                        , pos_uuid()
                                        , NULL
                                        , C.ON_HAND_QTY
                                        , " +  OracleDBUtil.SqlStr(CHK_PERSON) + 
                                      @", C.PRODNO
                                        , " + OracleDBUtil.SqlStr(CHK_PERSON) + 
                                      @", SYSDATE 
                                        ," + OracleDBUtil.SqlStr(CHK_PERSON) + 
                                      @", SYSDATE
                                        , " + OracleDBUtil.SqlStr(STKCHK_M_ID) + 
                                      @", C.INV_ONHAND_CURRENT_ID 
                               FROM INV_ON_HAND_CURRENT C, PRODUCT P 
                               WHERE 1 = 1  
                                 AND TO_NUMBER(C.ON_HAND_QTY) > 0   
                                 AND P.ISSTOCK = '1' 
                                 AND NVL(P.ISCONSIGNMENT,'0') = '0'
                                 AND C.PRODNO = P.PRODNO  
                                 AND C.STORE_NO = " + OracleDBUtil.SqlStr(STORE_NO)
                );  //沒有庫存量就不用盤點

                if (STKCHK_TYPE == "1")  //重盤(只需盤點該門市在營業日當天有異動的商品)
                {
                    sb.AppendLine(@" AND C.PRODNO IN (SELECT DISTINCT PRODNO FROM INV_TRAN_LOG 
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
        public void UpdateOne_StockCHK(INV11_StockCHK_DTO ds)
        {
            OracleDBUtil.UPDDATEByUUID_IncludeDBNull(ds.Tables["STOCKCHK_D"], "STKCHK_D_ID");
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

                StringBuilder sbD = new StringBuilder();
                sbD.AppendLine(@"DELETE STOCKCHK_D WHERE STKCHK_M_ID IN 
                                 (SELECT STKCHK_M_ID FROM STOCKCHK_M WHERE STKCHKNO = " + OracleDBUtil.SqlStr(STKCHKNO) + ") ");

                StringBuilder sbM = new StringBuilder();
                sbM.AppendLine("DELETE STOCKCHK_M WHERE STKCHKNO = " + OracleDBUtil.SqlStr(STKCHKNO) + " ");

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
            sb.AppendLine(@"SELECT ""商品料號"",""商品名稱"",""商品類別"", ""銷售倉"", ""展示倉"", ""租賃倉"", ""維修倉""
                            FROM VW_INV11_EXPORT
                            WHERE 1 = 1 
                            AND 盤點單號 = " + OracleDBUtil.SqlStr(STKCHKNO));

            DataTable dt = OracleDBUtil.Query_Data(sb.ToString());
            return dt;
        }

        /// <summary>
        /// 變更盤點單狀態及異動人員
        /// </summary>
        /// <param name="STKCHKNO">盤點單號</param>
        /// <param name="MODI_USER">異動人員編號</param>
        public void UPDATE_StockChkM(string STKCHKNO, string MODI_USER)
        {
            OracleConnection objConn = null;
             OracleTransaction objTX = null;
            try
            {
                objConn = OracleDBUtil.GetConnection();
                objTX = objConn.BeginTransaction();

                //**2011/04/11 Tina：所有明細資料的盤點量都要輸入，才回壓狀態為「已盤點」。
                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                sb.AppendLine(@" UPDATE STOCKCHK_M SET 
                                    STATUS = '10'
                                  , MODI_USER =" + OracleDBUtil.SqlStr(MODI_USER)
                            + @"  WHERE STKCHKNO = " + OracleDBUtil.SqlStr(STKCHKNO)
                            + @"    AND (
                                       SELECT COUNT(STKCHKNO) 
                                       FROM VW_INV11_QueryStockChkD 
                                       WHERE NVL(STKCHKQTY, '-99999999') = '-99999999' 
                                       AND STKCHKNO = " + OracleDBUtil.SqlStr(STKCHKNO)
                            + ") = 0");

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
        /// 取得明細資料盤點量為Null的資料筆數
        /// </summary>
        /// <param name="STKCHKNO">盤點單號</param>
        /// <returns></returns>
        public int GetDetailNullDataCount(string STKCHKNO)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("SELECT STKCHK_D_ID FROM VW_INV11_QueryStockChkD WHERE NVL(STKCHKQTY, '-99999999') = '-99999999' AND STKCHKNO = " + OracleDBUtil.SqlStr(STKCHKNO));

            DataTable dt = OracleDBUtil.Query_Data(sb.ToString());
            return dt.Rows.Count;
        }
    }
}
