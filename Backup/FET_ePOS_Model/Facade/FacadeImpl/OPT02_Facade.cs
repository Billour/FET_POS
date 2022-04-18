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
    public class OPT02_Facade
    {
        /// <summary>
        /// 檢查日期區間是否重複
        /// </summary>
        /// <param name="S_Date"></param>
        /// <param name="E_Date"></param>
        /// <param name="RateType"></param>
        /// <param name="CCPRID"></param>
        /// <param name="Status">Grid Status 1.新增 2.編輯 </param>
        /// <returns></returns>
        public bool CheckDateLimit(String S_Date, String E_Date, String RateType, String CCPRID, String Status)
        {
            bool result = false;

            //結束日期無輸入設定比對值為9999/12/31
            if (E_Date == "0001/01/01")
                E_Date = "9999/12/31";

            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT count(1) ");
            sb.Append("FROM CREDIT_CARD_PROCE_RATE CCPR ");
            sb.Append(",CREDIT_CARD_TYPE CCT ");
            sb.Append("WHERE CCPR.CREDIT_CARD_TYPE_ID = CCT.CREDIT_CARD_TYPE_ID  ");
            sb.Append("  And CCPR.CREDIT_CARD_TYPE_ID ='");
            sb.Append(RateType);
            sb.Append("'");

            switch (Status)
            {
                case "1":
                    break;
                case "2":
                    sb.Append(" AND CCPR.CCPR_ID<>'");
                    sb.Append(CCPRID);
                    sb.Append("'");
                    break;
                default:
                    break;
            }

            sb.Append(" And (maxdate (TO_CHAR (CCPR.S_DATE, 'YYYY/MM/DD'), '");
            sb.Append(S_Date);
            sb.Append("') <= mindate (TO_CHAR (nvl(CCPR.E_DATE,to_date('9999/12/31','yyyy/mm/dd')), 'yyyy/mm/dd'), '");
            sb.Append(E_Date);
            sb.Append("'))");

            DataTable dt = OracleDBUtil.Query_Data(sb.ToString());
            if (dt.Rows.Count > 0)
            {
                if (!(dt.Rows[0].ItemArray[0].ToString() == "0"))
                    result = true;
            }
            return result;
        }

        /// <summary>
        /// 取得查詢結果
        /// </summary>
        /// <param name="CreditCardType"></param>
        /// <param name="Status"></param>
        /// <returns></returns>
        public DataTable Query_CreditCardProceRate(object CreditCardType, object Status, string SDate_S, string SDate_E, string EDate_S, string EDate_E)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(@"SELECT ROWNUM AS ITEMNO, tb.* 
                            FROM  ( 
                                    SELECT CCPR.CCPR_ID
                                            , CCPR.CREDIT_CARD_TYPE_ID
                                            , CCT.CREDIT_CARD_TYPE_NAME
                                            --, TO_CHAR(CCPR.CHARGE_RATE, '00.00') CHARGE_RATE
                                            , CCPR.CHARGE_RATE
                                            , CCPR.S_DATE S_DATE
                                            , CCPR.E_DATE E_DATE 
                                            , CCPR.CREATE_USER
                                            , TO_CHAR(CCPR.CREATE_DTM,'YYYY/MM/DD') as CREATE_DTM
                                            , CCPR.MODI_USER MODI_USER, E.EMPNAME MODI_USER_NAME
                                            , TO_CHAR(CCPR.MODI_DTM, 'yyyy/mm/dd hh24:mi:ss') as MODI_DTM
                                            , GetCreditCardProceRateStatus(CCPR.CCPR_ID) AS STATUS 
                                    FROM CREDIT_CARD_PROCE_RATE CCPR,CREDIT_CARD_TYPE CCT, EMPLOYEE E
                                    WHERE CCPR.CREDIT_CARD_TYPE_ID = CCT.CREDIT_CARD_TYPE_ID  
                                    AND CCPR.MODI_USER = E.EMPNO(+) 
                            ");

            if (!string.IsNullOrEmpty((string)CreditCardType))
            {
                sb.AppendLine(" AND CCPR.CREDIT_CARD_TYPE_ID = " + OracleDBUtil.SqlStr(CreditCardType.ToString()));
            }

            if (!string.IsNullOrEmpty((string)Status))
            {
                sb.AppendLine(" AND GetCreditCardProceRateStatus(CCPR.CCPR_ID) = " + OracleDBUtil.SqlStr(Status.ToString()));
            }

            if (!string.IsNullOrEmpty(SDate_S))
            {
                sb.Append(" and CCPR.S_DATE >= " + OracleDBUtil.DateStr(SDate_S));
            }

            if (!string.IsNullOrEmpty(SDate_E))
            {
                sb.Append(" and CCPR.S_DATE <= " + OracleDBUtil.DateStr(SDate_E));
            }

            if (!string.IsNullOrEmpty(EDate_S))
            {
                sb.Append(" and CCPR.E_DATE >=  " + OracleDBUtil.DateStr(EDate_S));
            }

            if (!string.IsNullOrEmpty(EDate_E))
            {
                sb.Append(" and CCPR.E_DATE <= " + OracleDBUtil.DateStr(EDate_E));
            }

            sb.AppendLine(" ORDER BY CCPR.CREATE_DTM ");
            sb.AppendLine("  ) tb ");
            sb.AppendLine("ORDER BY ITEMNO ");

            DataTable dt = new DataTable();
            dt = OracleDBUtil.Query_Data(sb.ToString());
            return dt;
        }

        /// <summary>
        /// 新增信用卡手續費資料
        /// </summary>
        /// <param name="CREDIT_CARD_TYPE_ID">信用卡別</param>
        /// <param name="CHARGE_RATE">手續費</param>
        /// <param name="S_DATE">開始日期</param>
        /// <param name="E_DATE">結束日期</param>
        /// <param name="MODI_USER">更新人員</param>
        public void AddNewOne_CreditCardProceRate(object CREDIT_CARD_TYPE_ID, object CHARGE_RATE,
            object S_DATE, object E_DATE, object MODI_USER)
        {
            OPT02_CreditCardProceRate_DTO.CREDIT_CARD_PROCE_RATEDataTable dt = new OPT02_CreditCardProceRate_DTO().CREDIT_CARD_PROCE_RATE;

            OPT02_CreditCardProceRate_DTO.CREDIT_CARD_PROCE_RATERow dr = dt.NewCREDIT_CARD_PROCE_RATERow();

            dr["CCPR_ID"] = GuidNo.getUUID();
            dr["CREDIT_CARD_TYPE_ID"] = CREDIT_CARD_TYPE_ID;
            dr["CHARGE_RATE"] = CHARGE_RATE;
            dr["S_DATE"] = S_DATE;
            dr["E_DATE"] = E_DATE ?? DBNull.Value;
            dr["MODI_USER"] = MODI_USER;
            dr["MODI_DTM"] = DateTime.Now;
            dr["CREATE_USER"] = dr["MODI_USER"];
            dr["CREATE_DTM"] = dr["MODI_DTM"];

            dt.AddCREDIT_CARD_PROCE_RATERow(dr);

            dt.AcceptChanges();

            OracleDBUtil.Insert(dt);
        }

        /// <summary>
        /// 修改信用卡手續費資料
        /// </summary>
        /// <param name="CCPR_ID">Key</param>
        /// <param name="CREDIT_CARD_TYPE_ID">信用卡別</param>
        /// <param name="CHARGE_RATE">手續費</param>
        /// <param name="S_DATE">開始日期</param>
        /// <param name="E_DATE">結束日期</param>
        /// <param name="MODI_USER">更新人員</param>
        public void UpdateOne_CreditCardProceRate(OPT02_CreditCardProceRate_DTO ds)
        {
            OracleDBUtil.UPDDATEByUUID(ds.CREDIT_CARD_PROCE_RATE, "CCPR_ID");
        }

        /// <summary>
        /// 判斷同一信用卡在同一時間區間是否有手續費設定
        /// </summary>
        /// <param name="CREDIT_CARD_TYPE_ID"></param>
        /// <param name="S_DATE"></param>
        /// <param name="E_DATE"></param>
        /// <returns></returns>
        public static bool CheckRate(object CREDIT_CARD_TYPE_ID, object S_DATE, object E_DATE)
        {
            bool returnValue = false;

            StringBuilder sb = new StringBuilder();
            sb.AppendLine("SELECT CCPR_ID FROM CREDIT_CARD_PROCE_RATE ");
            sb.AppendLine("WHERE CREDIT_CARD_TYPE_ID = " + OracleDBUtil.SqlStr(CREDIT_CARD_TYPE_ID.ToString()));
            //判斷區間重複
            sb.AppendLine("AND NVL(E_DATE,TO_DATE('9999/12/31','YYYY/MM/DD')) >= " + OracleDBUtil.DateStr(((DateTime)S_DATE).ToString("yyyy/MM/dd")));

            if (E_DATE != null)
            {
                sb.AppendLine("AND S_DATE <= " + OracleDBUtil.DateStr(((DateTime)E_DATE).ToString("yyyy/MM/dd")));
            }

            DataTable dt = OracleDBUtil.Query_Data(sb.ToString());

            if (dt.Rows.Count > 0)
            {
                returnValue = true;
            }

            return returnValue;
        }

        public void Delete_CREDIT_CARD_PROCE_RATE(DataTable dt, string eKey)
        {
            OracleDBUtil.DELETEByUUID(dt, eKey);
        }
    }
}
