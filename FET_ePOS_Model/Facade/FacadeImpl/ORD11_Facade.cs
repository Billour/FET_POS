using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data;
using System.Data.OracleClient;
using System.Globalization;

using FET.POS.Model.Helper;
using FET.POS.Model.DTO;
using Advtek.Utility;

namespace FET.POS.Model.Facade.FacadeImpl
{
    public class ORD11_Facade
    {
        private DateTime SysDate = DateTime.Now;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="MainProductNumber">主商品編號</param>
        /// <param name="MainProductName">商品名稱</param>
        /// <returns></returns>
        public DataTable GetPORQMethodData(string MainProductNumber, string MainProductName)
        {
            string sqlStr = @"SELECT Q.SID,Q.PRODNO,P.PRODNAME ,Q.SALE_REGION,
                                     DECODE(Q.SALE_REGION,0,'14天',1,'30天',2,'指定期間') SALE_REGION_NAME,
                                     TO_CHAR(Q.S_DATE,'YYYY/MM/DD') AS S_DATE,TO_CHAR(Q.E_DATE,'YYYY/MM/DD') AS E_DATE,
                                     Q.SAFETY_VALUE,Q.CREATE_USER,Q.CREATE_DTM,Q.MODI_USER,Q.MODI_DTM
                               FROM PROD_ORDER_RECOMMANDED_QTY Q ,PRODUCT P
                               WHERE Q.PRODNO = P.PRODNO ";
            if (!string.IsNullOrEmpty(MainProductNumber))
            {
                sqlStr += " AND Q.PRODNO = " + OracleDBUtil.SqlStr(MainProductNumber);
            }
            if (!string.IsNullOrEmpty(MainProductName))
            {
                sqlStr += " AND P.PRODNAME like " + OracleDBUtil.LikeStr(MainProductName);

            }
            sqlStr += " ORDER BY Q.PRODNO, Q.CREATE_DTM";

            DataTable dt = OracleDBUtil.Query_Data(sqlStr);
            return dt;
        }

        public DataTable GetPORQMethodData2()
        {
            string sqlStr = @"SELECT Q.SID,'通則' as PRODNAME,Q.SALE_REGION,
                                DECODE(Q.SALE_REGION,0,'14天',1,'30天',2,'指定期間') SALE_REGION_NAME,
                                TO_CHAR(Q.S_DATE,'YYYY/MM/DD') AS S_DATE,TO_CHAR(Q.E_DATE,'YYYY/MM/DD') AS E_DATE
                                ,Q.SAFETY_VALUE,Q.CREATE_USER,Q.CREATE_DTM,Q.MODI_USER,Q.MODI_DTM
                              FROM PROD_ORDER_RECOMMANDED_GENERAL Q ORDER BY CREATE_DTM ";

            DataTable dt = OracleDBUtil.Query_Data(sqlStr);
            return dt;
        }

        public int GetPORQGCount(string S_DATE, string E_DATE)
        {
            int iRet = 0;

            StringBuilder sb = new StringBuilder();
            if (!string.IsNullOrEmpty(S_DATE) && !string.IsNullOrEmpty(E_DATE))
            {
                DateTime dStart = DateTime.Parse(S_DATE);
                DateTime dStop = DateTime.Parse((string.IsNullOrEmpty(E_DATE)) ? "9999/12/31" : E_DATE);
                sb.Append("SELECT  COUNT(*) AS CNT ");
                sb.Append(" FROM PROD_ORDER_RECOMMANDED_GENERAL S");
                sb.Append(" WHERE ");
                sb.Append("     (  ");
                sb.Append("       (  ");
                sb.Append("          to_char(S.S_DATE,'YYYY/MM/DD') <= '" + dStart.ToString("yyyy/MM/dd") + "' ");
                sb.Append("          and  ");
                sb.Append("          nvl(to_char(S.E_DATE,'YYYY/MM/DD'),'9999/12/31') >= '" + dStart.ToString("yyyy/MM/dd") + "' ");
                sb.Append("        ) ");
                sb.Append("        or ");
                sb.Append("        ( to_char(S.S_DATE,'YYYY/MM/DD') <= '" + dStop.ToString("yyyy/MM/dd") + "' ");
                sb.Append("          and  ");
                sb.Append("          nvl(to_char(S.E_DATE,'YYYY/MM/DD'),'9999/12/31') >= '" + dStop.ToString("yyyy/MM/dd") + "' ");
                sb.Append("        ) ");
                sb.Append("        or ");
                sb.Append("        ( '" + dStart.ToString("yyyy/MM/dd") + "' <= to_char(S.S_DATE,'YYYY/MM/DD') ");
                sb.Append("          and  ");
                sb.Append("          '" + dStop.ToString("yyyy/MM/dd") + "'  >=to_char(S.S_DATE,'YYYY/MM/DD') ");
                sb.Append("        ) ");
                sb.Append("        or ");
                sb.Append("        ( '" + dStart.ToString("yyyy/MM/dd") + "' <= nvl(to_char(S.E_DATE,'YYYY/MM/DD'),'9999/12/31') ");
                sb.Append("          and  ");
                sb.Append("          '" + dStop.ToString("yyyy/MM/dd") + "'  >=nvl(to_char(S.E_DATE,'YYYY/MM/DD'),'9999/12/31') ");
                sb.Append("        ) ");
                sb.Append("      ) ");

                DataTable dt = OracleDBUtil.Query_Data(sb.ToString());

                if (dt != null && dt.Rows.Count > 0)
                    iRet = Convert.ToInt32(dt.Rows[0]["CNT"]);
            }
            return iRet;
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="pdr"></param>
        /// <param name="MODI_USER"></param>
        public void UpdatePORQMethodData(System.Collections.Specialized.OrderedDictionary NewValues, object MODI_USER)
        {
            //表頭資料
            ORD11_PORQ_DTO.PROD_ORDER_RECOMMANDED_QTYDataTable dtm = new ORD11_PORQ_DTO.PROD_ORDER_RECOMMANDED_QTYDataTable();
            dtm.CREATE_USERColumn.AllowDBNull = true;
            dtm.CREATE_DTMColumn.AllowDBNull = true;
            ORD11_PORQ_DTO.PROD_ORDER_RECOMMANDED_QTYRow drm = dtm.NewPROD_ORDER_RECOMMANDED_QTYRow();
            drm.SID = NewValues["SID"].ToString();
            drm.PRODNO = NewValues["PRODNO"].ToString();
            drm.SALE_REGION = NewValues["SALE_REGION"].ToString();
            drm.SAFETY_VALUE = Convert.ToDecimal(NewValues["SAFETY_VALUE"]);
            drm.S_DATE = Advtek.Utility.DateUtil.NullDateFormat(NewValues["S_DATE"]);
            drm.E_DATE = Advtek.Utility.DateUtil.NullDateFormat(NewValues["E_DATE"]);
            drm.MODI_USER = MODI_USER.ToString();
            drm.MODI_DTM = SysDate;
            dtm.AddPROD_ORDER_RECOMMANDED_QTYRow(drm);
            dtm.AcceptChanges();
            OracleDBUtil.UPDDATEByUUID(dtm, "SID");
        }

        public void UpdatePORQMethodData2(System.Collections.Specialized.OrderedDictionary NewValues, object MODI_USER)
        {
            //表頭資料
            ORD11_PORQ_DTO.PROD_ORDER_RECOMMANDED_GENERALDataTable dtm = new ORD11_PORQ_DTO.PROD_ORDER_RECOMMANDED_GENERALDataTable();
            dtm.CREATE_USERColumn.AllowDBNull = true;
            dtm.CREATE_DTMColumn.AllowDBNull = true;
            ORD11_PORQ_DTO.PROD_ORDER_RECOMMANDED_GENERALRow drm = dtm.NewPROD_ORDER_RECOMMANDED_GENERALRow();
            drm.SID = NewValues["SID"].ToString();
            drm.SALE_REGION = NewValues["SALE_REGION"].ToString();
            drm.SAFETY_VALUE = Convert.ToDecimal(NewValues["SAFETY_VALUE"]);
            drm.S_DATE = Advtek.Utility.DateUtil.NullDateFormat(NewValues["S_DATE"]);
            drm.E_DATE = Advtek.Utility.DateUtil.NullDateFormat(NewValues["E_DATE"]);
            drm.MODI_USER = MODI_USER.ToString();
            drm.MODI_DTM = SysDate;
            dtm.AddPROD_ORDER_RECOMMANDED_GENERALRow(drm);
            dtm.AcceptChanges();
            OracleDBUtil.UPDDATEByUUID(dtm, "SID");
        }

        /// <summary>
        /// 檢查商品建議訂購量設定貨品資料是否已輸入
        /// </summary>
        /// <param name="SID"></param>
        /// <param name="PRODNO"></param>
        /// <returns></returns>
        public static string CheckProductQTY(string SID, string PRODNO)
        {
            string r = "";
            string sqlStr = @"SELECT PRODNO FROM PROD_ORDER_RECOMMANDED_QTY WHERE PRODNO = '{0}'";
            if (SID.Trim() != "")
            {
                sqlStr += " AND SID != '" + SID + "' ";
            }
            sqlStr = string.Format(sqlStr, PRODNO);

            DataTable dt = OracleDBUtil.Query_Data(sqlStr);
            if (dt.Rows.Count > 0)
                r = dt.Rows[0][0].ToString();
            return r;
        }

        public void DeletePORQMethodData(ORD11_PORQ_DTO ds)
        {
            OracleDBUtil.DELETEByUUID(ds.Tables["PROD_ORDER_RECOMMANDED_QTY"], "SID");
        }

        public void DeletePORQMethodData2(ORD11_PORQ_DTO ds)
        {
            OracleDBUtil.DELETEByUUID(ds.Tables["PROD_ORDER_RECOMMANDED_GENERAL"], "SID");
        }

        public void InsertPREQMethodData(System.Collections.Specialized.OrderedDictionary NewValues, object MODI_USER)
        {
            string SID = GuidNo.getUUID().ToString();

            //表頭資料
            ORD11_PORQ_DTO.PROD_ORDER_RECOMMANDED_QTYDataTable dtm = new ORD11_PORQ_DTO.PROD_ORDER_RECOMMANDED_QTYDataTable();
            ORD11_PORQ_DTO.PROD_ORDER_RECOMMANDED_QTYRow drm = dtm.NewPROD_ORDER_RECOMMANDED_QTYRow();
            drm.SID = SID;
            drm.PRODNO = NewValues["PRODNO"].ToString();
            drm.S_DATE = Advtek.Utility.DateUtil.NullDateFormat(NewValues["S_DATE"]);
            drm.E_DATE = Advtek.Utility.DateUtil.NullDateFormat(NewValues["E_DATE"]);
            drm.SALE_REGION = NewValues["SALE_REGION"].ToString();
            drm.SAFETY_VALUE = NewValues["SAFETY_VALUE"] == null ? 0 : Convert.ToDecimal(NewValues["SAFETY_VALUE"]);
            drm.CREATE_USER = MODI_USER.ToString();
            drm.CREATE_DTM = SysDate;
            drm.MODI_USER = MODI_USER.ToString();
            drm.MODI_DTM = SysDate;
            dtm.AddPROD_ORDER_RECOMMANDED_QTYRow(drm);
            dtm.AcceptChanges();
            OracleDBUtil.Insert(dtm);

        }

        public void InsertPREGMethodData(System.Collections.Specialized.OrderedDictionary NewValues, object MODI_USER)
        {
            string SID = GuidNo.getUUID().ToString();
            //表頭資料
            ORD11_PORQ_DTO.PROD_ORDER_RECOMMANDED_GENERALDataTable dtm = new ORD11_PORQ_DTO.PROD_ORDER_RECOMMANDED_GENERALDataTable();
            ORD11_PORQ_DTO.PROD_ORDER_RECOMMANDED_GENERALRow drm = dtm.NewPROD_ORDER_RECOMMANDED_GENERALRow();
            drm.SID = SID;
            drm.S_DATE = Advtek.Utility.DateUtil.NullDateFormat(NewValues["S_DATE"]);
            drm.E_DATE = Advtek.Utility.DateUtil.NullDateFormat(NewValues["E_DATE"]);
            drm.SALE_REGION = NewValues["SALE_REGION"].ToString();
            drm.SAFETY_VALUE = NewValues["SAFETY_VALUE"] == null ? 0 : Convert.ToDecimal(NewValues["SAFETY_VALUE"]);
            drm.CREATE_USER = MODI_USER.ToString();
            drm.CREATE_DTM = SysDate;
            drm.MODI_USER = MODI_USER.ToString();
            drm.MODI_DTM = SysDate;
            dtm.AddPROD_ORDER_RECOMMANDED_GENERALRow(drm);
            dtm.AcceptChanges();
            OracleDBUtil.Insert(dtm);
        }
    }
}
