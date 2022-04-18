using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data;
using System.Data.OracleClient;
using System.Globalization;

using Advtek.Utility;
using FET.POS.Model.Helper;
using FET.POS.Model.DTO;


namespace FET.POS.Model.Facade.FacadeImpl
{
    public class OPT03_Facade
    {
        /// <summary>
        /// 信用卡分期設定 主檔
        /// </summary>
        /// <param name="BANK_ID">發卡銀行編號</param>
        /// <param name="COST_CENTER_NO">成本中心編號</param>
        /// <param name="PAY_SEQMENT">分期期數</param>
        /// <param name="Status">狀態</param>
        /// <param name="SDate_S">開始日期(起)</param>
        /// <param name="SDate_E">開始日期(迄)</param>
        /// <param name="EDate_S">結束日期(起)</param>
        /// <param name="EDate_E">結束日期(迄)</param>
        /// <returns></returns>
        public DataTable Query_CreditCardInstallment(string BANK_ID, string COST_CENTER_NO, string PAY_SEQMENT, string Status, string SDate_S, string SDate_E, string EDate_S, string EDate_E)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(@"SELECT ROWNUM AS ITEMNO, tb.*  
                            FROM  (       
                                SELECT  DISTINCT CCI.INSTELLMENT_ID AS INSTELLMENT_ID     
                                        ,CCI.PAY_SEQMENT                       AS PAY_SEQMENT   
                                        ,TO_NUMBER(CCI.SEQMENT_RATE,999999.99) AS SEQMENT_RATE   
                                        ,TO_CHAR(CCI.S_DATE,'YYYY/MM/DD')      AS S_DATE     
                                        ,TO_CHAR(CCI.E_DATE,'YYYY/MM/DD')      AS E_DATE  
                                        ,CCI.CREATE_USER           
                                        ,TO_CHAR(CCI.CREATE_DTM,'yyyy/mm/dd hh24:mi:ss')  AS CREATE_DTM  
                                        ,CCI.MODI_USER              
                                        ,EMPLOYEE.EMPNAME AS MODI_USER_NAME    
                                        ,TO_CHAR(CCI.MODI_DTM,'yyyy/mm/dd hh24:mi:ss')    AS MODI_DTM      
                                        ,CCI.BANK_ID                    
                                        ,BANK.BANK_NAME             
                                        ,(CASE  WHEN(NVL(CCI.E_DATE, TO_DATE('9999/12/31','yyyy/mm/dd')) < TRUNC(SYSDATE)) THEN '已過期'       
                                                WHEN(CCI.S_DATE <= TRUNC(SYSDATE) AND NVL(CCI.E_DATE, TO_DATE('9999/12/31','yyyy/mm/dd')) >= TRUNC(SYSDATE)) THEN '有效'                                                   
                                                WHEN(CCI.S_DATE > TRUNC(SYSDATE)) THEN '尚未生效' END )  AS STATUS   
                                FROM CREDIT_CART_INSTELLMENT CCI, BANK, CREDIT_CARD_SETTLEMMENT CCS, EMPLOYEE   
                                WHERE CCI.BANK_ID = BANK.BANK_ID(+) AND CCI.INSTELLMENT_ID = CCS.INSTELLMENT_ID(+) AND CCI.MODI_USER = EMPLOYEE.EMPNO(+) 
                            ");

            if (!string.IsNullOrEmpty(BANK_ID))
            {
                sb.AppendLine(" AND BANK.BANK_ID = " + OracleDBUtil.SqlStr(BANK_ID));
            }

            if (!string.IsNullOrEmpty(COST_CENTER_NO) && COST_CENTER_NO != "ALL")
            {
                sb.AppendLine(" AND CCS.COST_CENTER_NO = " + OracleDBUtil.SqlStr(COST_CENTER_NO));
            }

            if (!string.IsNullOrEmpty(PAY_SEQMENT))
            {
                sb.AppendLine(" AND CCI.PAY_SEQMENT = " + PAY_SEQMENT);
            }

            if (!string.IsNullOrEmpty(SDate_S))
            {
                sb.AppendLine(" and CCI.S_DATE >= " + OracleDBUtil.DateStr(SDate_S));
            }

            if (!string.IsNullOrEmpty(SDate_E))
            {
                sb.AppendLine(" and CCI.S_DATE <= " + OracleDBUtil.DateStr(SDate_E));
            }

            if (!string.IsNullOrEmpty(EDate_S))
            {
                sb.AppendLine(" and CCI.E_DATE >=  " + OracleDBUtil.DateStr(EDate_S));
            }

            if (!string.IsNullOrEmpty(EDate_E))
            {
                sb.AppendLine(" and CCI.E_DATE <= " + OracleDBUtil.DateStr(EDate_E));
            }

            if (!string.IsNullOrEmpty(Status))
            {
                if (Status == "有效")
                {
                    sb.AppendLine(" AND TRUNC(CCI.S_DATE) <= TRUNC(SYSDATE) AND TRUNC(NVL(CCI.E_DATE, TO_DATE('9999/12/31','yyyy/mm/dd'))) >= TRUNC(SYSDATE) ");
                }
                else if (Status == "已過期")
                {
                    sb.AppendLine(" AND TRUNC(NVL(CCI.E_DATE, TO_DATE('9999/12/31','yyyy/mm/dd'))) < TRUNC(SYSDATE) ");
                }
                else if (Status == "尚未生效")
                {
                    sb.AppendLine(" AND TRUNC(CCI.S_DATE) > TRUNC(SYSDATE) ");
                }
            }

            sb.AppendLine(@"    AND ROWNUM <= 500 
                                ORDER BY S_DATE ASC
                                ) tb              
                            ORDER BY ITEMNO   
                        ");

            DataTable dt = OracleDBUtil.Query_Data(sb.ToString());
            return dt;
        }

        /// <summary>
        /// 信用卡分期設定 明細檔
        /// </summary>
        /// <param name="InstallmentId">主檔UUID</param>
        /// <returns></returns>
        public DataTable Query_CreditCardSettlement(string InstallmentId)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(@"SELECT ROWNUM AS ITEMNO, tb.*  
                            FROM  (  
                                SELECT CCS.SETTLEMENT_ID   
                                        , CCS.INSTELLMENT_ID       
                                        , CCS.LINE_NO             
                                        , TO_NUMBER(CCS.SETTLEMENT_RATE, 999999.99) AS SETTLEMENT_RATE     
                                        , CCS.CREATE_USER, TO_Char(CCS.CREATE_DTM,'yyyy/mm/dd hh24:mi:ss') as CREATE_DTM 
                                        , CCS.MODI_USER, TO_Char(CCS.MODI_DTM,'yyyy/mm/dd hh24:mi:ss') as MODI_DTM   
                                        , CCS.COST_CENTER_NO          
                                        , CC.COST_CENTER_NAME          
                                FROM CREDIT_CARD_SETTLEMMENT CCS, COST_CENTER CC 
                                WHERE CCS.COST_CENTER_NO = CC.COST_CENTER_NO(+)  
                                AND CCS.INSTELLMENT_ID = " + OracleDBUtil.SqlStr(InstallmentId) + @"
                                ORDER BY CCS.LINE_NO    
                            ) tb                    
                            ORDER BY ITEMNO                                                          
                         ");

            DataTable dt = OracleDBUtil.Query_Data(sb.ToString());
            return dt;
        }

        /// <summary>
        /// 新增 信用卡分期設定
        /// </summary>
        /// <param name="ds"></param>
        public void AddNewOne_CreditCardInstallment(OPT03_CreditCardInstallment_DTO ds)
        {
            OracleConnection objConn = null;
            OracleTransaction objTX = null;

            try
            {
                objConn = OracleDBUtil.GetConnection();
                objTX = objConn.BeginTransaction();

                OracleDBUtil.Insert(objTX, ds.CREDIT_CART_INSTELLMENT);
                OracleDBUtil.Insert(objTX, ds.CREDIT_CARD_SETTLEMMENT);

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
        /// 修改 信用卡分期設定
        /// </summary>
        /// <param name="ds"></param>
        public void UpdateOne_CreditCardInstallment(OPT03_CreditCardInstallment_DTO ds)
        {
            OracleConnection objConn = null;
            OracleTransaction objTX = null;

            try
            {
                objConn = OracleDBUtil.GetConnection();
                objTX = objConn.BeginTransaction();

                OracleDBUtil.UPDDATEByUUID_IncludeDBNull(ds.CREDIT_CART_INSTELLMENT, "INSTELLMENT_ID");

                //明細檔的更新 = Delete + Insert 
                //OracleDBUtil.DELETEByUUID(objTX, ds.CREDIT_CARD_SETTLEMMENT, "INSTELLMENT_ID");
                StringBuilder strDelete = new StringBuilder();
                strDelete.Append(" Delete CREDIT_CARD_SETTLEMMENT Where INSTELLMENT_ID = " + OracleDBUtil.SqlStr(ds.CREDIT_CART_INSTELLMENT.Rows[0]["INSTELLMENT_ID"].ToString()));
                OracleDBUtil.ExecuteSql(objTX, strDelete.ToString());
                OracleDBUtil.Insert(objTX, ds.CREDIT_CARD_SETTLEMMENT);

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
        /// 刪除 信用卡分期設定
        /// </summary>
        /// <param name="dt"></param>
        public void Delete_CreditCartInstellment(DataTable dt)
        {
            OracleConnection objConn = null;
            OracleTransaction objTX = null;

            try
            {
                objConn = OracleDBUtil.GetConnection();
                objTX = objConn.BeginTransaction();

                foreach (DataRow dr in dt.Rows)
                {
                    StringBuilder strDelete = new StringBuilder();
                    strDelete.Append(" Delete CREDIT_CARD_SETTLEMMENT Where INSTELLMENT_ID = " + OracleDBUtil.SqlStr(dr["INSTELLMENT_ID"].ToString()));
                    OracleDBUtil.ExecuteSql(objTX, strDelete.ToString());
                }

                OracleDBUtil.DELETEByUUID(objTX, dt, "INSTELLMENT_ID");

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

        public DataTable Query_UsingCreditCardInstellment()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(@"SELECT Distinct Instellment_ID,BANK_ID
                                            , PAY_SEQMENT
                            FROM CREDIT_CART_INSTELLMENT 
                            WHERE (E_DATE is null) OR (TRUNC(S_DATE) <= TRUNC(SYSDATE) AND TRUNC(E_DATE) >= TRUNC(SYSDATE)) 
                            ORDER BY BANK_ID 
                         ");

            DataTable dt = OracleDBUtil.Query_Data(sb.ToString());
            return dt;

        }

    }


}
