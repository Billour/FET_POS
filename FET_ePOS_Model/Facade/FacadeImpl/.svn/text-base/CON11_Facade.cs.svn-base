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
    public class CON11_Facade
    {

        /// <summary>
        /// 寄銷商品退倉單查詢
        /// </summary>
        /// <param name="RTNNO">退倉單號</param>
        /// <param name="STORENO">門市編號</param>
        /// <param name="B_DATE_S">退倉起日(起)</param>
        /// <param name="B_DATE_E">退倉起日(迄)</param>
        /// <param name="E_DATE_S">退倉訖日(起)</param>
        /// <param name="E_DATE_E">退倉訖日(迄)</param>
        /// <param name="RTNDATE_S">退倉日期(起)</param>
        /// <param name="RTNDATE_E">退倉日期(迄)</param>
        /// <param name="STATUS">狀態</param>   
        /// <returns></returns>
        /// 

        public DataTable getCSM_RTNM(string RTNNO, string STORENO,  string B_DATE_S, string B_DATE_E, string E_DATE_S, string E_DATE_E, string RTNDATE_S, string RTNDATE_E, string STATUS)
        {
            DataTable dt = new DataTable();
            //string sqlstr = "select  m.CSM_ORDERM_ID,m.ORDDATE,m.ORDNO,decode(m.STATUS,'0','已存檔','1','轉單中','2','已成單','3','待進貨','4','已驗收') as STATUS,em.EMPNAME as MODI_USER,m.MODI_DTM from CSM_ORDERM M,EMPLOYEE em where M.MODI_USER=em.EMPNO  ";

            //strSql.Append("SELECT CR.CSM_RTNM_UUID,CR.RTNNO,CR.B_DATE,CR.E_DATE,CRU.MODI_USER,CRU.MODI_DTM, ");
            //strSql.Append("  CRU.STATUS,CRU.RTNDATE ");
            //strSql.Append(" FROM CSM_RTNM CR ");
            //strSql.Append(" JOIN (select distinct CSM_RTNM_UUID,RTNDATE,STATUS,MODI_USER,MODI_DTM from CSM_RTND_UP where STORE_NO=" + OracleDBUtil.SqlStr(strStoreNo) + ") CRU on  CR.CSM_RTNM_UUID = CRU.CSM_RTNM_UUID ");
            //strSql.Append(" WHERE 1=1 ");
            //strSql.Append("AND CR.CSM_RTNM_UUID  = " + OracleDBUtil.SqlStr(strRtnmUUID));

            string sqlstr = @"select m.CSM_RTNM_UUID
                                ,m.RTNNO
                                ,TO_CHAR(TO_DATE(m.B_DATE,'yyyy/MM/dd'),'yyyy/mm/dd') AS B_DATE
                                ,TO_CHAR(TO_DATE(m.E_DATE,'yyyy/MM/dd'),'yyyy/mm/dd') AS E_DATE
                                ,decode(CRU.STATUS,'10','已存檔','50','已傳輸','60','已完成','未存檔') as STATUS
                                ,em.EMPNAME as MODI_USER
                                ,CRU.MODI_DTM   
                                ,TO_CHAR(TO_DATE(CRU.RTNDATE ,'yyyy/MM/dd'),'yyyy/mm/dd') AS RTNDATE   
                                ,M.REMARK   
                                from CSM_RTNM M     
                                JOIN (select distinct CSM_RTNM_UUID,RTNDATE,STATUS,MODI_USER,MODI_DTM from CSM_RTND_UP where STORE_NO='2101') CRU 
                                on  m.CSM_RTNM_UUID = CRU.CSM_RTNM_UUID  
                                JOIN  EMPLOYEE em on CRU.MODI_USER=em.EMPNO   
                                where 1=1 ";


            if (!string.IsNullOrEmpty(RTNNO))
            {
                sqlstr += " and m.RTNNO Like " + OracleDBUtil.LikeStr(RTNNO);
            }

            //if (!string.IsNullOrEmpty(STORENO))
            //{
            //    sqlstr += " and m.CSM_RTNM_UUID in (select CSM_RTNM_UUID From CSM_RTND_STORE Where STORE_NO= " + OracleDBUtil.SqlStr(STORENO) + " Group by CSM_RTNM_UUID)";
            //}          

            if (!string.IsNullOrEmpty(B_DATE_S))
            {
                sqlstr += " and m.B_DATE >= " + OracleDBUtil.SqlStr(B_DATE_S);
            }

            if (!string.IsNullOrEmpty(B_DATE_E))
            {
                sqlstr += " and m.B_DATE <= " + OracleDBUtil.SqlStr(B_DATE_E);
            }

            if (!string.IsNullOrEmpty(E_DATE_S))
            {
                sqlstr += " and m.E_DATE >= " + OracleDBUtil.SqlStr(E_DATE_S);
            }

            if (!string.IsNullOrEmpty(E_DATE_E))
            {
                sqlstr += " and m.E_DATE <= " + OracleDBUtil.SqlStr(E_DATE_E);
            }

            if (!string.IsNullOrEmpty(RTNDATE_S))
            {
                sqlstr += " and CRU.RTNDATE >= " + OracleDBUtil.SqlStr(RTNDATE_S);
            }

            if (!string.IsNullOrEmpty(RTNDATE_E))
            {
                sqlstr += " and CRU.RTNDATE <= " + OracleDBUtil.SqlStr(RTNDATE_E);
            }

            if (!string.IsNullOrEmpty(STATUS))
            {
                sqlstr += " and m.STATUS = " + OracleDBUtil.SqlStr(STATUS);
            }
            sqlstr += " Order by m.RTNNO ";
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
        /// 查詢寄銷退倉明細
        /// </summary>
        /// <param name="CSM_RTNM_UUID">CSM_RTNM_UUID</param>
        /// <returns></returns>
        public DataTable getCSM_RTND(string CSM_RTNM_UUID,string STORENO)
        {
            DataTable dt = new DataTable();
            //string sqlstr = "select distinct CR.CSM_RTNM_UUID,CS.SUPP_NO,CS.SUPP_NAME,SE.STORE_NO,SE.STORENAME, ";
            //sqlstr += "  PT.PRODNO,PT.PRODNAME,CRU.STOCKQTY,CRU.RTNQTY ";
            //sqlstr += " from CSM_RTNM CR ";
            //sqlstr += " JOIN CSM_RTND_UP CRU ON CR.CSM_RTNM_UUID = CRU.CSM_RTNM_UUID  ";
            //sqlstr += " JOIN CSM_RTND_PROD CRP ON CR.CSM_RTNM_UUID = CRP.CSM_RTNM_UUID ";
            //sqlstr += " LEFT JOIN CSM_SUPPLIER CS ON CRP.SUPP_ID = CS.SUPP_ID ";
            //sqlstr += " LEFT JOIN PRODUCT PT ON CRP.PRODNO = PT.PRODNO ";
            //sqlstr += " JOIN CSM_RTND_STORE CRS ON CR.CSM_RTNM_UUID = CRS.CSM_RTNM_UUID ";
            //sqlstr += " LEFT JOIN STORE SE ON CRS.STORE_NO = SE.STORE_NO ";
            //sqlstr += " Where CR.CSM_RTNM_UUID = " + OracleDBUtil.SqlStr(CSM_RTNM_UUID);

            string sqlstr = "select distinct CRU.CSM_RTNM_UUID,CRU.SEQNO ";
            sqlstr += "  ,PT.PRODNO,PT.PRODNAME ";
            sqlstr += "  ,CS.SUPP_NO,CS.SUPP_NAME ";
            sqlstr += "  ,CRU.STOCKQTY,CRU.RTNQTY ";
            sqlstr += " from CSM_RTND_UP CRU   ";    
            sqlstr += " LEFT JOIN CSM_SUPPLIER CS ON CRU.SUPP_ID = CS.SUPP_ID ";
            sqlstr += " LEFT JOIN PRODUCT PT ON CRU.PRODNO = PT.PRODNO ";       
            sqlstr += " LEFT JOIN STORE SE ON CRU.STORE_NO = SE.STORE_NO ";
            sqlstr += " Where CRU.CSM_RTNM_UUID = " + OracleDBUtil.SqlStr(CSM_RTNM_UUID);
            sqlstr += "   And CRU.STORE_NO = " + OracleDBUtil.SqlStr(STORENO);
            sqlstr += " Order by CRU.SEQNO ";

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

    }


}
