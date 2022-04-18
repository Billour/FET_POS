using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Advtek.Utility;
using System.Data.OracleClient;

namespace FET.POS.Model.Common
{
    public class INV11_PageHelper
    {
        /// <summary>
        /// 依照營業日的年月取得關帳日 (要比對當天是否為關帳日)
        /// </summary>
        /// <param name="YYMM">營業日的年月</param>
        /// <returns>關帳日</returns>
        public static string GetCutOffDate(string YYMM)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(@"SELECT CUT_OFF_DATE 
                            FROM CUT_OFF_DATE 
                            WHERE 1 = 1
                            AND CUT_YYMM = " + OracleDBUtil.SqlStr(YYMM));

            DataTable dt = OracleDBUtil.Query_Data(sb.ToString());

            string strNo = "";
            if (dt.Rows.Count > 0)
            {
                strNo = Convert.ToDateTime(dt.Rows[0]["CUT_OFF_DATE"]).ToString("yyyy/MM/dd");
            }
            return strNo;
        }

        /// <summary>
        /// 依照門市編號、營業日期查詢盤點作業主檔 (是否已盤點過了)
        /// </summary>
        /// <param name="STORE_NO">門市編號</param>
        /// <param name="STKCHKDATE">營業日</param>
        /// <returns>查詢結果</returns>
        public static DataTable GetStockChkM(string STORE_NO, string STKCHKDATE)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(@"SELECT DISTINCT M.STKCHK_M_ID
                                            , M.STKCHKNO
                                            , M.STKCHK_TYPE
                                            , E.EMPNAME
                                            , E.EMPNO
                                            , M.STORE_NO
                                            , M.STKCHKDATE
                                            , S.STORENAME 
                                            , M.MODI_USER 
                                            , EE.EMPNAME MODI_NAME
                                            , DECODE(M.STATUS, '00', '盤點中', '10', '已盤點') STATUS
                            FROM STOCKCHK_M M , EMPLOYEE E , STORE S , EMPLOYEE EE 
                            WHERE 1 = 1 
                              AND  M.STKCHK_USERNO = E.EMPNO(+)
                              AND  M.MODI_USER = EE.EMPNO(+)
                              AND  M.STORE_NO = S.STORE_NO(+)
                              AND M.STKCHKDATE = " + OracleDBUtil.SqlStr(STKCHKDATE)
                         + @" AND M.STORE_NO = " + OracleDBUtil.SqlStr(STORE_NO)
            );

            DataTable dt = OracleDBUtil.Query_Data(sb.ToString());
            return dt;
        }

        /// <summary>
        /// 取得盤點作業明細檔特定一筆資料
        /// </summary>
        /// <param name="STKCHK_D_ID">UUID</param>
        /// <returns>查詢結果</returns>
        public static DataTable Query_StockChkD_ByKey(string STKCHK_D_ID)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(@"SELECT * 
                            FROM STOCKCHK_D 
                            WHERE STKCHK_D_ID = " + OracleDBUtil.SqlStr(STKCHK_D_ID));

            DataTable dt = OracleDBUtil.Query_Data(sb.ToString());
            return dt;
        }

        public static string GetModiDate(string STKCHK_M_ID)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT DISTINCT  MODI_DTM  FROM STOCKCHK_D WHERE STKCHK_M_ID = " + OracleDBUtil.SqlStr(STKCHK_M_ID));

            DataTable dt = OracleDBUtil.Query_Data(sb.ToString());
            string MODI_DTM = "";
            if (dt.Rows.Count > 0)
            {
                try
                {
                    MODI_DTM = DateTime.Parse(dt.Rows[0]["MODI_DTM"].ToString()).ToString("yyyy/MM/dd HH:mm:ss");
                }
                catch //(Exception ex)
                {
                    MODI_DTM = "date Error";
                }
            }
            return MODI_DTM;
        }
    }
}
