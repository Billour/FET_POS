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
    public class CON05_Facade
    {

        /// <summary>
        /// 寄銷商品訂單查詢
        /// </summary>
        /// <param name="ORDDATE">訂單日期</param>
        /// <param name="ORDNO">訂單編號</param>
        /// <param name="STATUS">狀態</param>
        /// <param name="PRODNO">商品料號</param>
        /// <returns></returns>
        public DataTable getCSM_ORDERM(string ORDDATE,string ORDNO,string STATUS,string PRODNO,string STORENO)
        {
            DataTable dt = new DataTable();
            string sqlstr = "select  m.CSM_ORDERM_ID,m.ORDDATE,m.ORDNO,decode(m.STATUS,'0','已存檔','1','轉單中','2','已成單','3','待進貨','4','已驗收') as STATUS,em.EMPNAME as MODI_USER,m.MODI_DTM from CSM_ORDERM M,EMPLOYEE em where M.MODI_USER=em.EMPNO  ";

            if (!string.IsNullOrEmpty(ORDDATE))
            {
                sqlstr += " and m.ORDDATE = " + OracleDBUtil.SqlStr(ORDDATE);
            }

            if (!string.IsNullOrEmpty(ORDNO))
            {
                sqlstr += " and m.ORDNO Like " + OracleDBUtil.LikeStr(ORDNO);
            }

            if (!string.IsNullOrEmpty(STATUS))
            {
                sqlstr += " and m.STATUS = " + OracleDBUtil.SqlStr(STATUS);
            }

            if (!string.IsNullOrEmpty(PRODNO))
            {
                sqlstr += " and M.CSM_ORDERM_ID in (Select CSM_ORDERM_ID From CSM_ORDERD D WHERE  PRODNO Like " + OracleDBUtil.LikeStr(PRODNO) + ")" ;
            }

            if (!string.IsNullOrEmpty(STORENO))
            {
                sqlstr += " and m.STORE_NO = " + OracleDBUtil.SqlStr(STORENO);
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
        /// <param name="ORDNO">訂單明細</param>
        /// <returns></returns>
        public DataTable getCSM_ORDERD(string CSM_ORDERM_ID)
        {
            DataTable dt = new DataTable();
            string sqlstr = "select CD.SEQNO,CS.SUPP_NAME,CD.PRODNO,PROD.PRODNAME,PT.PRODTYPENAME,CD.ADVISEQTY,CD.ORDQTY,CD.APPROVEQTY,CD.IN_QTY from CSM_ORDERD CD ,CSM_ORDERM CM,CSM_SUPPLIER CS,PRODUCT PROD,CSM_PRODUCT_TYPE PT where CD.CSM_ORDERM_ID = CM.CSM_ORDERM_ID and CM.SUPP_ID=CS.SUPP_ID and CD.PRODNO = PROD.PRODNO  and PROD.PRODTYPENO = PT.PRODTYPENO(+) and CD.CSM_ORDERM_ID = " + OracleDBUtil.SqlStr(CSM_ORDERM_ID);

          
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
