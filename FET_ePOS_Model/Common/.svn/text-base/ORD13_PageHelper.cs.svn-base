using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Advtek.Utility;
using System.Data.OracleClient;
using System.Data;

namespace FET.POS.Model.Common
{
    public class ORD13_PageHelper
    {
        /// <summary>
        /// 取得門市名稱
        /// </summary>
        /// <returns></returns>
        public static string GetStoreName(string STORENO)
        {
            string returnValue = "";

            StringBuilder sb = new StringBuilder();
            sb.AppendLine("SELECT STORENAME FROM STORE WHERE STORE_NO = " + OracleDBUtil.SqlStr(STORENO));

            DataTable dt = OracleDBUtil.Query_Data(sb.ToString());

            if (dt.Rows.Count > 0)
            {
                returnValue += dt.Rows[0][0].ToString();
            }

            return returnValue;
        }

        /// <summary>
        /// 判斷該店是否已設定
        /// </summary>
        /// <param name="store_no"></param>
        /// <returns></returns>
        public static bool CheckStore(string store_no)
        {
            return CoreCheckStore(store_no, "");
        }

        /// <summary>
        /// 判斷該店是否已設定
        /// </summary>
        /// <param name="store_no"></param>
        /// <param name="my_store_no"></param>update 時自已的舊店號
        /// <returns></returns>
        public static bool CheckStore(string store_no , string my_store_no)
        {
            return CoreCheckStore( store_no ,  my_store_no);
        }

        /// <summary>
        /// 核心查詢
        /// </summary>
        /// <param name="store_no"></param>
        /// <param name="my_store_no"></param>
        /// <returns></returns>
        private static bool CoreCheckStore(string store_no, string my_store_no)
        {
            bool returnValue = false;

            StringBuilder sb = new StringBuilder();
            sb.AppendLine("SELECT STORE_NO FROM SCQC_M WHERE STORE_NO = " + OracleDBUtil.SqlStr(store_no));

            if (!string.IsNullOrEmpty(my_store_no))
            {
                sb.AppendLine(" AND STORE_NO NOT IN (" + OracleDBUtil.SqlStr(my_store_no) + ") ");
            }

            DataTable dt = OracleDBUtil.Query_Data(sb.ToString());

            if (dt.Rows.Count > 0)
            {
                returnValue = true;
            }

            return returnValue;
        }


        /// <summary>
        /// 判斷該店的卡片群組是否已設定
        /// </summary>
        /// <param name="SCQC_M_ID">主檔UUID</param>
        /// <param name="SIM_GROUP_ID">卡片群組代號</param>
        /// <param name="SCQC_D_ID">明細檔UUID</param>
        /// <param name="S_DATE">開始日期</param>
        /// <param name="E_DATE">結束日期</param>
        /// <returns>bool</returns>
        public static bool CheckGroup(string SCQC_M_ID, string SIM_GROUP_ID, string SCQC_D_ID, string S_DATE, string E_DATE)
        {
            bool returnValue = false;
            string sE_DATE = null;
            if (!string.IsNullOrEmpty(E_DATE))
            {
                sE_DATE = E_DATE;
            }
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(@"SELECT   D.SCQC_D_ID
                            FROM SCQC_M M, SCQC_D D, SIM_GROUP_M SM
                            WHERE M.SCQC_M_ID = D.SCQC_M_ID
                              AND D.SIM_GROUP_ID = SM.SIM_GROUP_ID
                              AND M.SCQC_M_ID = " + OracleDBUtil.SqlStr(SCQC_M_ID) + @"
                              AND SM.SIM_GROUP_ID = " + OracleDBUtil.SqlStr(SIM_GROUP_ID) + @"
                              AND (maxdate(TO_CHAR(D.S_DATE, 'yyyy/mm/dd'), " + OracleDBUtil.SqlStr(S_DATE) + @")
                                <= mindate(TO_CHAR(NVL(D.E_DATE,TO_DATE('9999/12/31','yyyy/MM/dd')), 'yyyy/MM/dd'), NVL(" + OracleDBUtil.SqlStr(sE_DATE) + ",'9999/12/31')))"
                          );
            if (!string.IsNullOrEmpty(SCQC_D_ID))
            {
                sb.AppendLine("  AND D.SCQC_D_ID <>" + OracleDBUtil.SqlStr(SCQC_D_ID));
            }

            DataTable dt = OracleDBUtil.Query_Data(sb.ToString());

            if (dt.Rows.Count > 0)
            {
                returnValue = true;
            }

            return returnValue;
        }

        public static string GetSimGroupName(string SIM_GROUP_ID)
        {
            string returnValue = "";

            StringBuilder sb = new StringBuilder();
            sb.AppendLine(@"SELECT DISTINCT SIM_GROUP_M.SIM_GROUP_ID,
                             SIM_GROUP_M.SIM_GROUP_NO,
                             SIM_GROUP_NAME,
                             S_DATE,
                             E_DATE,
                             SIM_GROUP_M.MODI_DTM FROM  SIM_GROUP_M 
                            WHERE     1=1 
                               AND S_DATE <= TRUNC(SYSDATE) 
                               AND NVL(e_DATE,TO_DATE('99991231','YYYYMMDD')) >= TRUNC(SYSDATE)");
            sb.AppendLine("AND SIM_GROUP_M.SIM_GROUP_ID = " + OracleDBUtil.SqlStr(SIM_GROUP_ID));

            DataTable dt = OracleDBUtil.Query_Data(sb.ToString());

            if (dt.Rows.Count > 0)
            {
                returnValue += dt.Rows[0][0].ToString();
            }

            return returnValue;
        }
    }
}
