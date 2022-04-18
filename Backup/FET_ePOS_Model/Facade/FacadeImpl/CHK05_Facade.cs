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
    public class CHK05_Facade
    {
        /// <summary>
        /// 取得查詢結果繳大鈔
        /// </summary>
        /// <param name="txtTraneDateS">交易日期(起)</param>
        /// <param name="txtTraneDateE">交易日期(迄)</param>
        /// <param name="MACHINE_ID">機台編號</param>
        /// <returns>查詢結果</returns>
        public DataTable Query_BigMoney(string txtTraneDateS, string txtTraneDateE,
            string MACHINE_ID, string STORE_NO)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT * FROM ( ");
            sb.Append("SELECT B.ID ,TO_CHAR(B.TRADE_DATE, 'YYYY/MM/DD') as TRADE_DATE, B.STORE_NO, M.HOST_NO, ");
            sb.Append("B.BATCH_NO, B.AMOUNT, B.MODI_USER, E.EMPNAME, TO_CHAR(B.MODI_DTM, 'yyyy/mm/dd hh24:mi:ss') as MODI_DTM ");
            sb.Append("FROM BIG_MONEY B, STORE_TERMINATING_MACHINE M, EMPLOYEE E ");
            sb.Append("WHERE 1 =1 ");
            sb.Append("AND B.MACHINE_ID = M.HOST_NO(+) AND B.STORE_NO = M.STORE_NO(+) ");
            sb.Append("AND B.MODI_USER = E.EMPNO(+) ");
            sb.Append("AND B.STORE_NO = " + OracleDBUtil.SqlStr(STORE_NO));

            if (!string.IsNullOrEmpty(txtTraneDateS))
            {
                sb.Append(" AND B.TRADE_DATE >= " + OracleDBUtil.DateStr(txtTraneDateS));
            }

            if (!string.IsNullOrEmpty(txtTraneDateE))
            {
                sb.Append(" AND B.TRADE_DATE <= " + OracleDBUtil.DateStr(txtTraneDateE));
            }

            if (!string.IsNullOrEmpty(MACHINE_ID) && MACHINE_ID != "ALL")
            {
                sb.Append(" AND B.MACHINE_ID = " + OracleDBUtil.SqlStr(MACHINE_ID));
            }

            sb.Append(" AND rownum <= 500 ");  //最多顯示500筆
            sb.Append(" ORDER BY B.TRADE_DATE DESC");  //交易日期最新的前500筆
            sb.Append(" ) t ");
            sb.Append(" order by t.TRADE_DATE, t.HOST_NO, t.BATCH_NO");

            DataTable dt = OracleDBUtil.Query_Data(sb.ToString());
            return dt;
        }

        /// <summary>
        /// 新增繳大鈔
        /// </summary>
        /// <param name="ds">CHK05 DataSet</param>
        public void AddNewOne_BigMoney(CHK05_BigMoney_DTO ds)
        {
            OracleDBUtil.Insert(ds.Tables["BIG_MONEY"]);
        }

        /// <summary>
        /// 刪除繳大鈔
        /// </summary>
        /// <param name="ds">CHK05 DataSet</param>
        public void Delete_BigMoney(CHK05_BigMoney_DTO ds)
        {
            OracleConnection objConn = null;
            OracleTransaction objTX = null;

            try
            {
                objConn = OracleDBUtil.GetConnection();
                objTX = objConn.BeginTransaction();

                OracleDBUtil.DELETEByUUID(objTX, ds.Tables["BIG_MONEY"], "ID");

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
