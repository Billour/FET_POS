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
    public class CON09_Facade
    {

        /// <summary>
        /// 寄銷商品退倉單查詢
        /// </summary>
        /// <param name="RTNNO">退倉單號</param>
        /// <param name="STORENO">門市編號</param>
        /// <param name="SUPPNO">廠商編號</param>
        /// <param name="PRODNO">商品料號</param>
        /// <param name="B_DATE_S">退倉起日(起)</param>
        /// <param name="B_DATE_E">退倉起日(迄)</param>
        /// <param name="E_DATE_S">退倉訖日(起)</param>
        /// <param name="E_DATE_E">退倉訖日(迄)</param>
        /// <param name="STATUS">狀態</param>   
        /// <returns></returns>
        /// 

        public DataTable getCSM_RTNM(string RTNNO, string STORENO, string SUPPNO, string PRODNO, string B_DATE_S, string B_DATE_E, string E_DATE_S, string E_DATE_E, string STATUS)
        {
            DataTable dt = new DataTable();
            //string sqlstr = "select  m.CSM_ORDERM_ID,m.ORDDATE,m.ORDNO,decode(m.STATUS,'0','已存檔','1','轉單中','2','已成單','3','待進貨','4','已驗收') as STATUS,em.EMPNAME as MODI_USER,m.MODI_DTM from CSM_ORDERM M,EMPLOYEE em where M.MODI_USER=em.EMPNO  ";

            string sqlstr = @"select m.CSM_RTNM_UUID,
                                m.RTNNO,
                                TO_CHAR(TO_DATE(m.B_DATE,'YYYY/MM/DD'),'YYYY/MM/DD') AS B_DATE,
                                TO_CHAR(TO_DATE(m.E_DATE,'YYYY/MM/DD'),'YYYY/MM/DD') AS E_DATE,
                                decode(m.STATUS,'10','已存檔','50','已傳輸','60','已完成') as STATUS,
                                em.EMPNAME as MODI_USER,
                                m.MODI_DTM 
                                from CSM_RTNM M,EMPLOYEE em 
                                where M.MODI_USER=em.EMPNO ";

            if (!string.IsNullOrEmpty(RTNNO))
            {
                sqlstr += " and m.RTNNO Like " + OracleDBUtil.LikeStr(RTNNO);
            }

            if (!string.IsNullOrEmpty(STORENO))
            {
                sqlstr += " and m.CSM_RTNM_UUID in (select CSM_RTNM_UUID From CSM_RTND_STORE Where STORE_NO= " + OracleDBUtil.SqlStr(STORENO) + " Group by CSM_RTNM_UUID)";
            }

            if (!string.IsNullOrEmpty(SUPPNO))
            {
                sqlstr += " and m.CSM_RTNM_UUID in (select CSM_RTNM_UUID From CSM_RTND_PROD Where SUPP_ID= (Select SUPP_ID From CSM_SUPPLIER Where  SUPP_NO = " + OracleDBUtil.SqlStr(SUPPNO) + ") Group by CSM_RTNM_UUID)";
            }

            if (!string.IsNullOrEmpty(PRODNO))
            {
                sqlstr += " and M.CSM_RTNM_UUID in (Select CSM_RTNM_UUID From CSM_RTND_PROD WHERE  PRODNO Like " + OracleDBUtil.LikeStr(PRODNO) + " Group by CSM_RTNM_UUID)";
            }

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

            if (!string.IsNullOrEmpty(STATUS))
            {
                sqlstr += " and m.STATUS = " + OracleDBUtil.SqlStr(STATUS);
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
        /// 查詢寄銷訂單明細
        /// </summary>
        /// <param name="CSM_RTNM_UUID">CSM_RTNM_UUID</param>
        /// <returns></returns>
        public DataTable getCSM_RTND(string CSM_RTNM_UUID)
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

            string sqlstr = "select distinct CR.CSM_RTNM_UUID,CS.SUPP_NO,CS.SUPP_NAME,SE.STORE_NO,SE.STORENAME, ";
            sqlstr += "  PT.PRODNO,PT.PRODNAME,CRU.STOCKQTY,CRU.RTNQTY ";
            sqlstr += " from CSM_RTNM CR ";
            sqlstr += " JOIN CSM_RTND_UP CRU ON CR.CSM_RTNM_UUID = CRU.CSM_RTNM_UUID  ";    
            sqlstr += " LEFT JOIN CSM_SUPPLIER CS ON CRU.SUPP_ID = CS.SUPP_ID ";
            sqlstr += " LEFT JOIN PRODUCT PT ON CRU.PRODNO = PT.PRODNO ";       
            sqlstr += " LEFT JOIN STORE SE ON CRU.STORE_NO = SE.STORE_NO ";
            sqlstr += " Where CR.CSM_RTNM_UUID = " + OracleDBUtil.SqlStr(CSM_RTNM_UUID);
            sqlstr += " Order by SE.STORE_NO ";

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

        public DataTable ExportWeightDistribute(string strCSM_RTNM_UUID)
        {
            //退倉單號  退倉起日  退倉迄日 退倉狀態  更新人員 更新日期 廠商編號 廠商名稱 門市編號 門市名稱 商品料號 商品名稱 庫存數量 實際退倉數量

            string sqlStr = " SELECT  DISTINCT CR.RTNNO AS 退倉單號                       ";
            sqlStr += "              ,CR.B_DATE 退倉起日          ";
            sqlStr += "              ,CR.E_DATE 退倉迄日          ";
            sqlStr += "              ,DECODE(CR.STATUS,'10','已存檔','50','已傳輸','60','已完成') 退倉狀態  ";
            sqlStr += "              ,CR.MODI_USER ||'-'||EMP.EMPNAME  更新人員                               ";
            sqlStr += "              ,TO_CHAR(CR.MODI_DTM,'YYYY/MM/DD HH:mM:ss') 更新日期 ";
            sqlStr += "              ,CS.SUPP_NO    廠商編號                               ";
            sqlStr += "              ,CS.SUPP_NAME  廠商名稱                               ";
            sqlStr += "              ,CRS.STORE_NO  門市編號                               ";
            sqlStr += "              ,ST.STORENAME 門市名稱                               ";
            sqlStr += "              ,CRP.PRODNO    商品料號                               ";
            sqlStr += "              ,PR.PRODNAME  商品名稱                               ";
            sqlStr += "              ,CRU.STOCKQTY  庫存數量                               ";
            sqlStr += "              ,CRU.RTNQTY  實際退倉數量                               ";
            sqlStr += "         FROM  CSM_RTNM CR                                                  ";
            sqlStr += "         JOIN CSM_RTND_UP CRU ON CR.CSM_RTNM_UUID = CRU.CSM_RTNM_UUID  ";
            sqlStr += "         INNER JOIN CSM_RTND_PROD CRP                                     ";
            sqlStr += "            ON CR.CSM_RTNM_UUID = CRP.CSM_RTNM_UUID      ";
            sqlStr +="           LEFT JOIN CSM_SUPPLIER CS ON CRP.SUPP_ID=CS.SUPP_ID ";
            sqlStr +="           LEFT JOIN PRODUCT PR ON CRP.PRODNO=PR.PRODNO ";
            sqlStr += "         INNER JOIN CSM_RTND_STORE CRS ";
            sqlStr += "            ON CR.CSM_RTNM_UUID = CRS.CSM_RTNM_UUID      ";
            sqlStr +="           LEFT JOIN STORE ST ON CRS.STORE_NO=ST.STORE_NO ";
            sqlStr += "    INNER JOIN EMPLOYEE EMP ON CR.MODI_USER = EMP.EMPNO          ";
            sqlStr += "        WHERE 1 = 1                                                  ";

            if (!string.IsNullOrEmpty(strCSM_RTNM_UUID))
            {
                sqlStr += "   AND CR.CSM_RTNM_UUID IN (" + strCSM_RTNM_UUID + ")";
            }

            sqlStr += " ORDER BY CR.RTNNO ";

            DataTable dt = OracleDBUtil.Query_Data(sqlStr);
            return dt;
        }
    }


}
