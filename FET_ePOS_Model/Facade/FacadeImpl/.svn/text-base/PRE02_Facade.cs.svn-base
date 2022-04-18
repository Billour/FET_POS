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
using System.Collections.Specialized;

namespace FET.POS.Model.Facade.FacadeImpl
{
    public class PRE02_Facade
    {

        /// <summary>
        /// 取得該門市有預收資料的員工資料, 但如門市為HQ(門市)則取得所有門市有預收資料的員工資料
        /// </summary>
        /// <param name="STORENO">門市編號</param>
        /// <returns>員工編號與姓名</returns>
        public DataTable getSalePersonInPrepayHead(string STORENO)
        {
            OracleConnection objConn = OracleDBUtil.GetConnection();
            DataTable dt = new DataTable();

            StringBuilder sbSql = new StringBuilder();
            sbSql.Append("Select Distinct Sale_Person as EMPNO From Prepay_Head");
            if (STORENO.ToUpper() != "HQ")
                sbSql.AppendFormat(" Where STORE_NO = {0}", OracleDBUtil.SqlStr(STORENO));

            try
            {
                dt = OracleDBUtil.GetDataSet(objConn, sbSql.ToString()).Tables[0];
                if (dt != null && dt.Rows.Count > 0)
                {
                    sbSql = new StringBuilder();
                    sbSql.Append("SELECT EMPNO, EMPNAME, (EMPNO || ' ' || EMPNAME) EMP_SHOWNAME FROM EMPLOYEE WHERE EMPNO in (Select Distinct Sale_Person as EMPNO From Prepay_Head");
                    if (STORENO.ToUpper() != "HQ")
                        sbSql.AppendFormat(" Where STORE_NO = {0})", OracleDBUtil.SqlStr(STORENO));
                    else
                        sbSql.Append(")");
                    dt = OracleDBUtil.GetDataSet(objConn, sbSql.ToString()).Tables[0];
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (objConn.State == ConnectionState.Open) objConn.Close();
                objConn.Dispose();
                OracleConnection.ClearAllPools();
            }
            return dt;
        }


        /// <summary>
        /// 取得預收資料
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public DataTable getPREPAY_HEAD(OrderedDictionary args)
        {
            StringBuilder sbSql = new StringBuilder();
            StringBuilder sbWhere = new StringBuilder();

            // 門市編號
            if (args["STORE_NO"].ToString().Length != 0 && args["STORE_NO"].ToString().ToUpper() != "HQ")
                sbWhere.AppendFormat(" AND h.STORE_NO = {0}", OracleDBUtil.SqlStr(args["STORE_NO"].ToString()));
            // 客戶姓名
            if (args["CUST_NAME"].ToString() != "")
                sbWhere.AppendFormat(" AND h.CUST_NAME like {0}", OracleDBUtil.LikeStr(args["CUST_NAME"].ToString()));
            // 狀態
            if (args["PREPAY_STATUS"].ToString() != "" )
                sbWhere.AppendFormat(" AND h.PREPAY_STATUS = {0}", OracleDBUtil.SqlStr(args["PREPAY_STATUS"].ToString()));
            // 預收款單號
            if (args["PREPAY_NO"].ToString() != "")
                sbWhere.AppendFormat(" AND h.PREPAY_NO like {0}", OracleDBUtil.LikeStr(args["PREPAY_NO"].ToString()));
            // 客戶身份證號
            if (args["ID_NO"].ToString() != "")
                sbWhere.AppendFormat(" AND h.ID_NO = {0}", OracleDBUtil.SqlStr(args["ID_NO"].ToString()));
            // 銷售人員
            if (args["SALE_PERSON"].ToString() != "")
                sbWhere.AppendFormat(" AND h.SALE_PERSON = {0}", OracleDBUtil.SqlStr(args["SALE_PERSON"].ToString()));
            // 預購日期 起
            if (args["TRADE_DATE_S"].ToString() != "")
                sbWhere.AppendFormat(" AND TRUNC(h.TRADE_DATE) >= TO_DATE({0},'yyyy/MM/dd')", OracleDBUtil.SqlStr(args["TRADE_DATE_S"].ToString()));
            // 預購日期 迄
            if (args["TRADE_DATE_E"].ToString() != "")
                sbWhere.AppendFormat(" AND TRUNC(h.TRADE_DATE) <= TO_DATE({0},'yyyy/MM/dd')", OracleDBUtil.SqlStr(args["TRADE_DATE_E"].ToString()));
            //客戶門號MSISDN
            if (args["MSISDN"].ToString() != "")
                sbWhere.AppendFormat(" AND h.MSISDN like {0}", OracleDBUtil.LikeStr(args["MSISDN"].ToString()));
            //統一編號
            if (args["UNI_NO"].ToString() != "")
                sbWhere.AppendFormat(" AND h.UNI_NO like {0}", OracleDBUtil.LikeStr(args["UNI_NO"].ToString()));
            //聯絡電話
            if (args["CONTACT_PHONE"].ToString() != "")
                sbWhere.AppendFormat(" AND h.CONTACT_PHONE like {0}", OracleDBUtil.LikeStr(args["CONTACT_PHONE"].ToString()));
            //發票號碼
            if (args["INVOICE_NO"].ToString() != "")
                sbWhere.AppendFormat(" AND h.INVOICE_NO like {0}", OracleDBUtil.LikeStr(args["INVOICE_NO"].ToString()));

            sbSql.AppendFormat(@"
                                    SELECT ROWNUM ITEMS, 
                                        T1.PREPAY_STATUS, 
                                        T1.PREPAY_STATUS_NAME, 
                                        T1.TRADE_DATE, 
                                        T1.PREPAY_NO,   
                                        T1.CUST_NAME,                                  
                                        T1.MSISDN, 
                                        T1.CONTACT_PHONE, 
                                        T1.AR_AMOUNT, 
                                        T1.STORE_NO, 
                                        T1.STORE_NAME,
                                        T1.SALE_PERSON,
                                        T1.SALE_PERSON_NAME
                                    FROM
            	                        (SELECT H.PREPAY_STATUS, 
                                            DECODE(H.PREPAY_STATUS,1,'未存檔',2,'已結帳',3,'作廢') PREPAY_STATUS_NAME,
                                            TO_CHAR(H.TRADE_DATE, 'yyyy/mm/dd') TRADE_DATE,
                                            H.PREPAY_NO,
                                            H.CUST_NAME,
                                            H.MSISDN,
                                            H.CONTACT_PHONE,
                                            nvl(H.AR_AMOUNT,'0') AR_AMOUNT,
                                            H.STORE_NO,
                                            H.STORE_NAME,
                                            H.SALE_PERSON,
                                            (SELECT  EMPNAME FROM EMPLOYEE WHERE EMPNO=H.SALE_PERSON AND ROWNUM=1) SALE_PERSON_NAME
            	                        FROM PREPAY_HEAD H 
            	                        WHERE 1 = 1 {0}
            	                        ORDER BY H.TRADE_DATE, H.STORE_NO, H.PREPAY_NO) T1", sbWhere);

            DataTable dt = OracleDBUtil.Query_Data(sbSql.ToString());
            return dt;
        }
        //************************************************************
        enum ConditionsType
        {
            INVOICE_NO, PROMOTION_CODE, PRODNO, MSISDN, PAY_METHOD
        }



        public string checkData(OrderedDictionary args)
        {
            OracleConnection objConn = null;

            string result = string.Empty;
            StringBuilder sbSql = new StringBuilder();
            string sql1 = "-1", sql2 = "-1", sql3 = "-1", sql4 = "-1", sql5 = "-1";
            string tmp = null;
            //機台
            if (args["MACHINE_ID"].ToString() != "")
                sql1 = string.Format("select count(*) FROM SALE_HEAD H where  h.MACHINE_ID like '%{0}%'", args["MACHINE_ID"].ToString());

            //交易序號
            if (args["SALE_NO"].ToString() != "")
                sql2 = string.Format("select count(*) FROM SALE_HEAD H where  h.SALE_NO like '%{0}%'", args["SALE_NO"].ToString());

            //發票號碼
            if (args["INVOICE_NO"].ToString() != "")
                sql3 = string.Format("select count(*) FROM SALE_HEAD H where  POSUUID_MASTER IN({0})", getPOSUUID_MASTER_QueryString(ConditionsType.INVOICE_NO, args["INVOICE_NO"].ToString()));

            //客戶門號
            if (args["MSISDN"].ToString() != "")
                sql4 = string.Format(" select count(*) FROM SALE_HEAD H where POSUUID_MASTER IN({0})", getPOSUUID_MASTER_QueryString(ConditionsType.MSISDN, args["MSISDN"].ToString()));

            //促銷代碼
            if (args["PROMOTION_CODE"].ToString() != "")
                sql5 = string.Format(" select count(*) FROM SALE_HEAD H where POSUUID_MASTER IN({0})", getPOSUUID_MASTER_QueryString(ConditionsType.PROMOTION_CODE, args["PROMOTION_CODE"].ToString()));

            //組合其他資料
            // 門市編號
            if (args["STORENO"].ToString().Length != 0)
            {
                tmp = OracleDBUtil.SqlStr(args["STORENO"].ToString());
                if (sql1 != "-1") sql1 += string.Format(" AND h.STORE_NO = {0}", tmp);
                if (sql2 != "-1") sql2 += string.Format(" AND h.STORE_NO = {0}", tmp);
                if (sql3 != "-1") sql3 += string.Format(" AND h.STORE_NO = {0}", tmp);
                if (sql4 != "-1") sql4 += string.Format(" AND h.STORE_NO = {0}", tmp);
                if (sql5 != "-1") sql5 += string.Format(" AND h.STORE_NO = {0}", tmp);
            }

            // 交易日期 起
            if (args["S_DATE"].ToString() != "")
            {
                tmp = args["S_DATE"].ToString();
                if (sql1 != "-1") sql1 += string.Format(" AND h.trade_date >= TO_DATE('{0}','yyyy/MM/dd')", tmp);
                if (sql2 != "-1") sql2 += string.Format(" AND h.trade_date >= TO_DATE('{0}','yyyy/MM/dd')", tmp);
                if (sql3 != "-1") sql3 += string.Format(" AND h.trade_date >= TO_DATE('{0}','yyyy/MM/dd')", tmp);
                if (sql4 != "-1") sql4 += string.Format(" AND h.trade_date >= TO_DATE('{0}','yyyy/MM/dd')", tmp);
                if (sql5 != "-1") sql5 += string.Format(" AND h.trade_date >= TO_DATE('{0}','yyyy/MM/dd')", tmp);
            }

            // 交易日期 止
            if (args["E_DATE"].ToString() != "")
            {
                tmp = args["E_DATE"].ToString();
                if (sql1 != "-1") sql1 += string.Format(" AND h.trade_date <= TO_DATE('{0}','yyyy/MM/dd')", tmp);
                if (sql2 != "-1") sql2 += string.Format(" AND h.trade_date <= TO_DATE('{0}','yyyy/MM/dd')", tmp);
                if (sql3 != "-1") sql3 += string.Format(" AND h.trade_date <= TO_DATE('{0}','yyyy/MM/dd')", tmp);
                if (sql4 != "-1") sql4 += string.Format(" AND h.trade_date <= TO_DATE('{0}','yyyy/MM/dd')", tmp);
                if (sql5 != "-1") sql5 += string.Format(" AND h.trade_date <= TO_DATE('{0}','yyyy/MM/dd')", tmp);
            }

            // 狀態
            if (args["SALE_STATUS"].ToString() != "")
            {
                tmp = OracleDBUtil.SqlStr(args["SALE_STATUS"].ToString());
                if (sql1 != "-1") sql1 += string.Format(" AND h.SALE_STATUS = {0}", tmp);
                if (sql2 != "-1") sql2 += string.Format(" AND h.SALE_STATUS = {0}", tmp);
                if (sql3 != "-1") sql3 += string.Format(" AND h.SALE_STATUS = {0}", tmp);
                if (sql4 != "-1") sql4 += string.Format(" AND h.SALE_STATUS = {0}", tmp);
                if (sql5 != "-1") sql5 += string.Format(" AND h.SALE_STATUS = {0}", tmp);
            }

            // 銷售人員
            if (args["SALE_PERSON"].ToString() != "")
            {
                tmp = OracleDBUtil.SqlStr(args["SALE_PERSON"].ToString());
                if (sql1 != "-1") sql1 += string.Format(" AND h.SALE_PERSON = {0}", tmp);
                if (sql2 != "-1") sql2 += string.Format(" AND h.SALE_PERSON = {0}", tmp);
                if (sql3 != "-1") sql3 += string.Format(" AND h.SALE_PERSON = {0}", tmp);
                if (sql4 != "-1") sql4 += string.Format(" AND h.SALE_PERSON = {0}", tmp);
                if (sql5 != "-1") sql5 += string.Format(" AND h.SALE_PERSON = {0}", tmp);
            }


            //付款方式
            if (args["PAY_METHOD"].ToString() != "")
            {
                tmp = OracleDBUtil.SqlStr(args["PAY_METHOD"].ToString());
                if (sql1 != "-1") sql1 += string.Format(" AND POSUUID_MASTER IN({0})", getPOSUUID_MASTER_QueryString(ConditionsType.PAY_METHOD, tmp));
                if (sql2 != "-1") sql2 += string.Format(" AND POSUUID_MASTER IN({0})", getPOSUUID_MASTER_QueryString(ConditionsType.PAY_METHOD, tmp));
                if (sql3 != "-1") sql3 += string.Format(" AND POSUUID_MASTER IN({0})", getPOSUUID_MASTER_QueryString(ConditionsType.PAY_METHOD, tmp));
                if (sql4 != "-1") sql4 += string.Format(" AND POSUUID_MASTER IN({0})", getPOSUUID_MASTER_QueryString(ConditionsType.PAY_METHOD, tmp));
                if (sql5 != "-1") sql5 += string.Format(" AND POSUUID_MASTER IN({0})", getPOSUUID_MASTER_QueryString(ConditionsType.PAY_METHOD, tmp));
            }

            try
            {
                sbSql.AppendFormat("select ({0}) count1,({1}) count2,({2}) count3,({3}) count4 ,({4}) count5 from dual ", sql1, sql2, sql3, sql4, sql5);

                objConn = OracleDBUtil.GetConnection();
                DataTable dt = OracleDBUtil.GetDataSet(objConn, sbSql.ToString()).Tables[0];

                if (Convert.ToInt32(dt.Rows[0][0]) == 0)
                {
                    result += @"[機台]不存在，請重新輸入\r\n";
                }
                if (Convert.ToInt32(dt.Rows[0][1]) == 0)
                {
                    result += @"[交易序號]不存在，請重新輸入\r\n";
                }
                if (Convert.ToInt32(dt.Rows[0][2]) == 0)
                {
                    result += @"[發票號碼]不存在，請重新輸入\r\n";
                }
                if (Convert.ToInt32(dt.Rows[0][3]) == 0)
                {
                    result += @"[客戶門號]不存在，請重新輸入\r\n";
                }
                if (Convert.ToInt32(dt.Rows[0][4]) == 0)
                {
                    result += @"[促銷代碼]不存在，請重新輸入\r\n";
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (objConn.State == ConnectionState.Open) objConn.Close();
                objConn.Dispose();
                OracleConnection.ClearAllPools();
            }
            return result;
        }


        /// <summary>
        /// 取得 POSUUID_MASTRE DB Query String
        /// </summary>
        /// <param name="type">類型</param>
        /// <param name="arg">關鍵字</param>
        /// <returns>T-SQL Query String</returns>
        private string getPOSUUID_MASTER_QueryString(ConditionsType type, string arg)
        {
            StringBuilder POSUUID_MASTER = new StringBuilder();
            string sqlStr = string.Empty;
            switch (type)
            {
                case ConditionsType.INVOICE_NO:
                    // 發票 (電子 & 手開)
                    sqlStr = string.Format(@"
                    SELECT POSUUID_MASTER FROM invoice_head WHERE NVL(IS_INVALID,'N') !='Y' AND INVOICE_NO like '%{0}%' GROUP BY POSUUID_MASTER
                    UNION
                    SELECT POSUUID_MASTER FROM manual_invoice_head WHERE NVL(IS_INVALID,'N') !='Y' AND INVOICE_NO like '%{0}%' GROUP BY POSUUID_MASTER", arg);
                    break;
                case ConditionsType.PROMOTION_CODE:
                    // 促銷代碼
                    sqlStr = string.Format(@"SELECT POSUUID_MASTER FROM sale_detail where PROMOTION_CODE like '%{0}%' GROUP BY POSUUID_MASTER", arg);
                    break;
                case ConditionsType.PRODNO:
                    // 商品料號
                    sqlStr = string.Format(@"SELECT POSUUID_MASTER FROM sale_detail where PRODNO ={0} GROUP BY POSUUID_MASTER", OracleDBUtil.SqlStr(arg));
                    break;
                case ConditionsType.MSISDN:
                    // 客戶門號
                    sqlStr = string.Format(@"SELECT POSUUID_MASTER FROM sale_detail where MSISDN like '%{0}%' GROUP BY POSUUID_MASTER ", arg);
                    break;
                case ConditionsType.PAY_METHOD:
                    // 付款方式
                    sqlStr = string.Format(@"SELECT POSUUID_MASTER FROM paid_detail where paid_mode ={0} GROUP BY POSUUID_MASTER ", OracleDBUtil.SqlStr(arg));
                    break;
                default:
                    return "''";
            }
            return sqlStr;
        }

        private string getPAID_MODE_NAME(string PAID_MODE)
        {
            switch (PAID_MODE)
            {
                case "1": return "現金";
                case "2": return "信用卡";
                case "3": return "離線信用卡";
                case "4": return "分期付款";
                case "5": return "禮券";
                case "6": return "金融卡";
                case "7": return "Happy GO";
                default: return "";
            }
        }

        /// <summary>
        /// 取得銷售交易發票
        /// </summary>
        /// <param name="POSUUID_MASTER"></param>
        /// <returns></returns>
        private string getINVOICE_NO(string POSUUID_MASTER)
        {
            string INVOICE_NO = "";
            //先取電子發票, 若已作廢則改捉手開發票
            string sqlStr = string.Format(@"
                            SELECT INVOICE_NO FROM invoice_head WHERE NVL(IS_INVALID,'N') !='Y' AND POSUUID_MASTER = {0} 
                            UNION
                            SELECT INVOICE_NO FROM manual_invoice_head WHERE NVL(IS_INVALID,'N') !='Y' AND POSUUID_MASTER = {0}", OracleDBUtil.SqlStr(POSUUID_MASTER));

            DataTable dt = OracleDBUtil.Query_Data(sqlStr);
            if (dt.Rows.Count > 0) //有電子發票
                INVOICE_NO = dt.Rows[0]["INVOICE_NO"].ToString();

            return INVOICE_NO;
        }

        /// <summary>
        /// 取得付款方式資料
        /// </summary>
        /// <param name="POSUUID_MASTER"></param>
        /// <returns></returns>
        public string getPAID_DETAIL(string POSUUID_MASTER)
        {
            StringBuilder PAY_METHOD = new StringBuilder();
            string sqlStr = string.Format("SELECT * FROM PAID_DETAIL WHERE POSUUID_MASTER = {0}", OracleDBUtil.SqlStr(POSUUID_MASTER));

            DataTable dtd = OracleDBUtil.Query_Data(sqlStr);

            foreach (DataRow dtr in dtd.Rows)
            {
                PAY_METHOD.AppendFormat("{0},", getPAID_MODE_NAME(dtr["PAID_MODE"].ToString()));
            }

            return (PAY_METHOD.Length > 0) ? PAY_METHOD.ToString().Substring(0, PAY_METHOD.Length - 1) : "";
        }

        /// <summary>
        /// 取得MSISDN客戶門號多筆
        /// </summary>
        /// <param name="POSUUID_MASTER"></param>
        /// <returns></returns>
        public string getMSISDN(string POSUUID_MASTER)
        {
            StringBuilder MSISDN = new StringBuilder();
            string sqlStr = string.Format(@"SELECT MSISDN FROM sale_detail WHERE POSUUID_MASTER = {0}", OracleDBUtil.SqlStr(POSUUID_MASTER));

            DataTable dtd = OracleDBUtil.Query_Data(sqlStr);

            foreach (DataRow dtr in dtd.Rows)
            {
                MSISDN.AppendFormat("{0},", dtr["MSISDN"]);
            }

            return (MSISDN.Length > 0) ? MSISDN.ToString().Substring(0, MSISDN.Length - 1) : "";
        }
    }
}
