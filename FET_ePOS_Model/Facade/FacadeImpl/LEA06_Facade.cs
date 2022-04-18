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
    public class LEA06_Facade 
    {

        public DataTable GetSelectData(string STORE_NO, string REAL_RECEV_SDATE, string REAL_RECEV_EDATE)
        {

            StringBuilder sb = new StringBuilder();
            sb.AppendLine(@"select ROWNUM NO,STORENAME,
                            CUST_NAME,MSISDN,TO_CHAR(REAL_RECEV_DATE,'YYYY/MM/DD') REAL_RECEV_DATE,TO_CHAR(REAL_RETURN_DTM,'YYYY/MM/DD') REAL_RETURN_DTM
                            ,NVL(RENT_AMT,0) RENT_AMT,NVL(IND_AMT,0) IND_AMT,''IND_ERR,0 AMT1, RENT_AMT+IND_AMT SAMT,'' RMARK
                            from RENT_M R,LEASE_STOCK L,STORE S
                            where R.IMEI=L.IMEI(+)
                            AND L.STORE_NO = S.STORE_NO(+)");
            if (!string.IsNullOrEmpty(STORE_NO))
            {
                sb.AppendLine(" AND L.STORE_NO = " + OracleDBUtil.SqlStr(STORE_NO.ToString()));
            }
            if (!string.IsNullOrEmpty(REAL_RECEV_SDATE))
            {
                 sb.AppendLine(" AND R.REAL_RECEV_DATE >= " + OracleDBUtil.DateStr(REAL_RECEV_SDATE));
            }

            if (!string.IsNullOrEmpty(REAL_RECEV_EDATE))
            {
                sb.AppendLine(" AND R.REAL_RECEV_DATE <= " + OracleDBUtil.DateStr(REAL_RECEV_EDATE));
            }
            sb.AppendLine(" ORDER BY NO, L.STORE_NO,R.MSISDN ");

            DataTable dt = OracleDBUtil.Query_Data(sb.ToString());
            return dt;

        }
        public DataTable GetExportData(string STORE_NO, string REAL_RECEV_SDATE, string REAL_RECEV_EDATE)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(@"select ROWNUM NO,STORENAME 手機地點,
                            CUST_NAME 客戶姓名,MSISDN 客戶門號,TO_CHAR(REAL_RECEV_DATE,'YYYY/MM/DD') 實際領取日,TO_CHAR(REAL_RETURN_DTM,'YYYY/MM/DD') 實際歸還日
                            ,NVL(RENT_AMT,0) 租金,NVL(IND_AMT,0) 賠償金,FN_IND_ITEM_NAME(RENT_SHEET_NO) 賠償原因,0 折扣金額, RENT_AMT+IND_AMT 總金額,'' 備註
                            from RENT_M R,LEASE_STOCK L,STORE S
                            where R.IMEI=L.IMEI(+)
                            AND L.STORE_NO = S.STORE_NO(+)");
            if (!string.IsNullOrEmpty(STORE_NO))
            {
                sb.AppendLine(" AND L.STORE_NO = " + OracleDBUtil.SqlStr(STORE_NO.ToString()));
            }
            if (!string.IsNullOrEmpty(REAL_RECEV_SDATE))
            {
                sb.AppendLine(" AND R.REAL_RECEV_DATE >= " + OracleDBUtil.DateStr(REAL_RECEV_SDATE));
            }

            if (!string.IsNullOrEmpty(REAL_RECEV_EDATE))
            {
                sb.AppendLine(" AND R.REAL_RECEV_DATE <= " + OracleDBUtil.DateStr(REAL_RECEV_EDATE));
            }
            sb.AppendLine(" ORDER BY  NO, L.STORE_NO,R.MSISDN ");

            DataTable dt = OracleDBUtil.Query_Data(sb.ToString());
            return dt;

        }
    

    }
}
