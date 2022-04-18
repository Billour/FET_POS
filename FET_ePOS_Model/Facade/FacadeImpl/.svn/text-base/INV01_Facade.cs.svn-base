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
    public class INV01_Facade
   {
        public DataTable Query_STORETRANSFER_M(
             string strSTNO
            , string strPRODNO
            , string strSTATUS
            , string strSTDATE1
            , string strSTDATE2
            , string strTSTDATE1
            , string strTSTDATE2
            , string strFROM_STORE_NO
            , string strTO_STORE_NO
            , string strFROM_STORE_NAME
            , string strTO_STORE_NAME)
        {

            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT Distinct ");
            sb.Append("       M.STNO as STNO ");
            sb.Append("      ,(case TSTATUS when '20' then '在途' else '已撥入' end) TSTATUS ");
            sb.Append("      ,(SELECT STORE_NO || ' ' || STORENAME FROM STORE WHERE STORE_NO=FROM_STORE_NO)  as FROM_STORE_NO ");
            sb.Append("      ,M.STDATE as STDATE ");
            sb.Append("      ,(SELECT STORE_NO || ' ' || STORENAME FROM STORE WHERE STORE_NO=TO_STORE_NO) as TO_STORE_NO ");
            sb.Append("      ,TSTDATE as TSTDATE ");
            sb.Append("      ,M.MODI_USER as MODI_USER, E.EMPNAME MODI_USER_NAME ");
            sb.Append("      ,TO_CHAR(M.MODI_DTM, 'yyyy/mm/dd hh24:mi:ss') as MODI_DTM ");
            sb.Append("FROM   STORETRANSFER_M M, ");
            sb.Append("STORETRANSFER_D D, EMPLOYEE E  ");
            sb.Append("WHERE 1 = 1 AND M.STNO = D.STNO ");
            sb.Append("AND M.MODI_USER = E.EMPNO(+) ");

            if (!string.IsNullOrEmpty(strSTNO))
            {
                sb.Append(" AND M.STNO like " + OracleDBUtil.LikeStr(strSTNO));
            }
            if (!string.IsNullOrEmpty(strPRODNO))
            {
                sb.Append(" AND  D.PRODNO = " + OracleDBUtil.SqlStr(strPRODNO));
            }
            if (!string.IsNullOrEmpty(strSTATUS))
            {
                sb.Append(" AND M.TSTATUS = " + OracleDBUtil.SqlStr(strSTATUS));
            }
            if (!(string.IsNullOrEmpty(strSTDATE1)) || !(string.IsNullOrEmpty(strSTDATE2)))
            {
                if (!(string.IsNullOrEmpty(strSTDATE1)))
                {
                    sb.Append(" AND M.STDATE >= " + OracleDBUtil.SqlStr(strSTDATE1));
                }
                if (!(string.IsNullOrEmpty(strSTDATE2)))
                {
                    sb.Append(" AND M.STDATE <= " + OracleDBUtil.SqlStr(strSTDATE2));
                }
            }
            if (!(string.IsNullOrEmpty(strTSTDATE1) || string.IsNullOrEmpty(strTSTDATE2)))
            {
                if (!(string.IsNullOrEmpty(strTSTDATE1)))
                {
                    sb.Append(" AND M.TSTDATE >= " + OracleDBUtil.SqlStr(strTSTDATE1));
                }
                if (!(string.IsNullOrEmpty(strTSTDATE2)))
                {
                    sb.Append(" AND M.TSTDATE <= " + OracleDBUtil.SqlStr(strTSTDATE2));
                }
            }
            if (!string.IsNullOrEmpty(strFROM_STORE_NO))
            {
                sb.Append(" AND M.FROM_STORE_NO = " + OracleDBUtil.SqlStr(strFROM_STORE_NO));
            }
            if (!string.IsNullOrEmpty(strTO_STORE_NO))
            {
                sb.Append(" AND M.TO_STORE_NO = " + OracleDBUtil.SqlStr(strTO_STORE_NO));
            }
            if (!string.IsNullOrEmpty(strFROM_STORE_NAME))
            {
                sb.Append(" AND (SELECT STORENAME FROM STORE WHERE STORE_NO=FROM_STORE_NO) like " + OracleDBUtil.LikeStr(strFROM_STORE_NAME));
            }
            if (!string.IsNullOrEmpty(strTO_STORE_NAME))
            {
                sb.Append(" AND (SELECT STORENAME FROM STORE WHERE STORE_NO=TO_STORE_NO) like " + OracleDBUtil.LikeStr(strTO_STORE_NAME));
            }
            sb.Append(" ORDER BY STNO DESC ");

            DataTable dt = OracleDBUtil.Query_Data(sb.ToString());
            return dt;
        }

        public DataTable Query_STORETRANSFER_D(string strSTNO)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT  ");
            sb.Append("       STORETRANSFER_D.STORETRANSFER_D_ID ");
            sb.Append("       ,STORETRANSFER_D.STNO, STORETRANSFER_D.SEQNO ");
            sb.Append("      ,STORETRANSFER_D.prodno prodno ");
            sb.Append("      ,(select PRODUCT.PRODNAME from product where product.prodno=STORETRANSFER_D.prodno) prodName ");
            sb.Append("      ,decode((select IMEI_FLAG from PRODUCT where PRODUCT.prodno= STORETRANSFER_D.prodno ),'0','False','True') chkIMEI ");
            sb.Append("      ,TRANOUTQTY outQty ");
            sb.Append("      ,IMEI inIMEI ");
            sb.Append("      ,TRANINQTY inQty ");
            sb.Append("      ,IMEI outIMEI ");
            sb.Append("      ,(select PRODUCT.IMEI_FLAG from product where product.prodno=STORETRANSFER_D.prodno) IMEI_FLAG ");
            sb.Append("      ,(SELECT COUNT(*) FROM STORETRANSFER_IMEI WHERE STORETRANSFER_IMEI.STORETRSF_D_ID= STORETRANSFER_D.STORETRANSFER_D_ID AND IMEI IS NOT NULL) IMEI_QTY ");
            sb.Append("FROM   STORETRANSFER_D,STORETRANSFER_IMEI ");
            sb.Append("WHERE 1 = 1 AND STORETRANSFER_D.STORETRANSFER_D_ID = STORETRANSFER_IMEI.STORETRSF_D_ID(+) ");

            if (!string.IsNullOrEmpty(strSTNO))
            {
                sb.Append(" AND STORETRANSFER_D.STNO = " + OracleDBUtil.SqlStr(strSTNO));
            }
            else
            {
                sb.Append(" AND 1<>1 ");
            }

            DataTable dt = OracleDBUtil.Query_Data(sb.ToString());

            return dt;
        }

        public DataTable ExportSTORETRANSFER_M_D(string strSTNOList)
        {

            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT  ");
            sb.Append("       T1.STNO as 移撥單號 ");
            sb.Append("      ,(case T1.TSTATUS when '20' then '在途' else '已撥入' end) as 移撥狀態 ");
            sb.Append("      ,T1.FROM_STORE_NO as 移出門市 ");
            sb.Append("      ,T1.STDATE as 移出日期 ");
            sb.Append("      ,T1.TO_STORE_NO as 移入門市 ");
            sb.Append("      ,T1.TSTDATE as 移入日期 ");
            sb.Append("      ,T1.MODI_USER as 更新人員 ");
            sb.Append("      ,T1.MODI_DTM as 更新日期 ");
            sb.Append("      ,T2.PRODNO as  商品料號 ");
            sb.Append("      ,T3.PRODNAME 商品名稱 ");
            sb.Append("      ,T2.TRANOUTQTY 移出數量 ");
            sb.Append("      ,T4.IMEI 移出IMEI ");
            sb.Append("      ,T2.TRANINQTY 移入數量 ");
            sb.Append("      ,decode(T2.TRANINQTY,'','',IMEI ) 移入IMEI ");
            sb.Append("FROM STORETRANSFER_M T1, STORETRANSFER_D T2,PRODUCT T3, STORETRANSFER_IMEI T4  ");
            sb.Append("WHERE T1.STNO=T2.STNO(+) AND T2.PRODNO=T3.PRODNO(+) AND T2.STORETRANSFER_D_ID=T4.STORETRSF_D_ID(+) ");

            if (!string.IsNullOrEmpty(strSTNOList))
            {
                sb.Append(" AND T1.STNO = " + OracleDBUtil.SqlStr(strSTNOList));
            }

            DataTable dt = OracleDBUtil.Query_Data(sb.ToString());
            return dt;
        }

        public DataTable Query_IMEIList(string strSTNO)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT  ");
            sb.Append("       STORETRANSFER_IMEI.IMEI IMEI ");
            sb.Append("FROM STORETRANSFER_D, STORETRANSFER_IMEI ");
            sb.Append("WHERE 1 = 1 AND  STORETRANSFER_D.STORETRANSFER_D_ID = STORETRANSFER_IMEI.STORETRSF_D_ID ");
            sb.Append(" AND STORETRANSFER_D.STNO = " + OracleDBUtil.SqlStr(strSTNO));

            DataTable dt = OracleDBUtil.Query_Data(sb.ToString());
            return dt;
        }
   }
}
