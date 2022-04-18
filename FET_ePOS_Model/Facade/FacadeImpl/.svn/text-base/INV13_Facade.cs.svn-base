using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.OracleClient;
using Advtek.Utility;
using FET.POS.Model.DTO;

namespace FET.POS.Model.Facade.FacadeImpl
{
    public class INV13_Facade
    {
        /// <summary>
        /// 無訂單進貨資料查詢
        /// </summary>
        /// <param name="S_DATE">進貨日期  起</param>
        /// <param name="E_DATE">進貨日期  訖</param>
        /// <returns></returns>
        public DataTable QueryOrderData(string S_DATE, string E_DATE, string STORE_NO)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(" SELECT INV.NP_ORDER_M_ID, INV.ORDER_NO, INV.ORDDATE, INV.REMARK, TO_CHAR(INV.MODI_DTM, 'yyyy/mm/dd hh24:mi:ss') MODI_DTM, ");
            sb.AppendLine(" EMP.EMPNAME MODI_USER, INV.CREATE_DTM, INV.STORE_NO, INV.STORENAME ");
            sb.AppendLine(" FROM VW_INV13 INV, EMPLOYEE EMP ");
            sb.AppendLine(" WHERE 1=1 ");
            sb.AppendLine(" AND INV.MODI_USER = EMP.EMPNO ");

            if (!string.IsNullOrEmpty(S_DATE))
            {
                sb.AppendLine(" AND ORDDATE >= " + OracleDBUtil.SqlStr(S_DATE));
            }
            if (!string.IsNullOrEmpty(E_DATE))
            {
                sb.AppendLine(" AND ORDDATE <= " + OracleDBUtil.SqlStr(E_DATE));
            }

            if (!string.IsNullOrEmpty(STORE_NO))
            {
                sb.AppendLine(" AND STORE_NO = " + OracleDBUtil.SqlStr(STORE_NO.ToString()));
            }

            DataTable dt = OracleDBUtil.Query_Data(sb.ToString());
            return dt;

        }

        /// <summary>
        /// 取得無訂單進貨資料明細
        /// </summary>
        /// <param name="NP_ORDER_M_ID">主檔UUID</param>
        /// <returns></returns>
        public DataTable QueryOrderDetailData(object NP_ORDER_M_ID)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("SELECT * FROM VW_INV13_DETAIL WHERE NP_ORDER_M_ID = " + OracleDBUtil.SqlStr(NP_ORDER_M_ID.ToString()));

            DataTable dt = OracleDBUtil.Query_Data(sb.ToString());
            return dt;
        }
    }
}
