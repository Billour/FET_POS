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
	public class OSAL02_Facade
	{
		enum ConditionsType
		{
			INVOICE_NO, PROMOTION_CODE, PRODNO, MSISDN, PAY_METHOD
		}

		/// <summary>
		/// 取得該門市有銷售資料的員工資料, 但如門市為HQ(門市)則取得所有門市有銷售資料的員工資料
		/// </summary>
		/// <param name="STORENO">門市編號</param>
		/// <returns>員工編號與姓名</returns>
		public DataTable getSalePersonInSaleHead(string STORENO)
		{
			OracleConnection objConn = OracleDBUtil.GetConnection();
			DataTable dt = new DataTable();

			StringBuilder sbSql = new StringBuilder();
			sbSql.Append("Select Distinct Sale_Person as EMPNO From Sale_Head");
			if (STORENO.ToUpper() != "HQ")
				sbSql.AppendFormat(" Where STORE_NO = {0}", OracleDBUtil.SqlStr(STORENO));

			try
			{
				dt = OracleDBUtil.GetDataSet(objConn, sbSql.ToString()).Tables[0];
				if (dt != null && dt.Rows.Count > 0)
				{
					sbSql = new StringBuilder();
					sbSql.Append("SELECT EMPNO, EMPNAME, (EMPNO || ' ' || EMPNAME) EMP_SHOWNAME FROM EMPLOYEE WHERE EMPNO in (Select Distinct Sale_Person as EMPNO From Sale_Head");
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
		/// 取得銷售資料
		/// </summary>
		/// <param name="args"></param>
		/// <returns></returns>
        public DataTable getSALE_HEAD(OrderedDictionary args)
        {
            StringBuilder sbSql = new StringBuilder();
            StringBuilder sbWhere = new StringBuilder();
            bool isM = false;
            // 門市編號
            if (args["STORENO"].ToString().Length != 0 && args["STORENO"].ToString().ToUpper() != "HQ")
                sbWhere.AppendFormat(" AND H.STORENO = {0}", OracleDBUtil.SqlStr("R" + args["STORENO"].ToString()));

            //發票號碼 invoice_head,manual_invoice_head LIKE
            if (args["INVOICE_NO"].ToString() != "")
            {
                isM = true;
                sbWhere.AppendFormat(" AND H.TAX_NO IN({0})", args["INVOICE_NO"].ToString());
            }


            // 交易序號
            if (args["SALE_NO"].ToString() != "")
            {
                isM = true;
                sbWhere.AppendFormat(" AND h.SERIAL_NO like '%{0}%'", args["SALE_NO"].ToString());
            }



            if(!isM)
            {
                // 交易日期 起
                if (args["S_DATE"].ToString() != "")
                    sbWhere.AppendFormat(" AND TRUNC(h.CANCEL_DTM) >= TO_DATE({0},'yyyy/MM/dd')", OracleDBUtil.SqlStr(args["S_DATE"].ToString()));
                // 交易日期 止
                if (args["E_DATE"].ToString() != "")
                    sbWhere.AppendFormat(" AND TRUNC(h.CANCEL_DTM) <= TO_DATE({0},'yyyy/MM/dd')", OracleDBUtil.SqlStr(args["E_DATE"].ToString()));
                // 機台
                if (args["MACHINE_ID"].ToString() != "")
                    sbWhere.AppendFormat(" AND h.CANCEL_MACHINE like '%{0}%'", args["MACHINE_ID"].ToString());
            
               
                // 銷售人員
                if (args["SALE_PERSON"].ToString() != "")
                    sbWhere.AppendFormat(" AND h.CANCEL_USER = {0}", OracleDBUtil.SqlStr(args["SALE_PERSON"].ToString()));


                //促銷代碼 SALE_DETAIL LIKE
                if (args["PROMOTION_CODE"].ToString() != "")
                {
                    sbWhere.AppendFormat(" AND h.SERIAL_NO IN({0})", getSERIAL_NO_QueryString(ConditionsType.PROMOTION_CODE, args["PROMOTION_CODE"].ToString()));
                    sbWhere.AppendFormat(" AND h.TDATE IN({0})", getTDATE_QueryString(ConditionsType.PROMOTION_CODE, args["PROMOTION_CODE"].ToString()));
                    sbWhere.AppendFormat(" AND h.TERM_NO IN({0})", getTERM_NO_QueryString(ConditionsType.PROMOTION_CODE, args["PROMOTION_CODE"].ToString()));
                    sbWhere.AppendFormat(" AND h.STORENO IN({0})", getSTORENO_QueryString(ConditionsType.PROMOTION_CODE, args["PROMOTION_CODE"].ToString()));
                }
                //商品料號 SALE_DETAIL
                if (args["PRODNO"].ToString() != "")
                {
                    sbWhere.AppendFormat(" AND h.SERIAL_NO IN({0})", getSERIAL_NO_QueryString(ConditionsType.PRODNO, args["PRODNO"].ToString()));
                    sbWhere.AppendFormat(" AND h.TDATE IN({0})", getTDATE_QueryString(ConditionsType.PRODNO, args["PRODNO"].ToString()));
                    sbWhere.AppendFormat(" AND h.TERM_NO IN({0})", getTERM_NO_QueryString(ConditionsType.PRODNO, args["PRODNO"].ToString()));
                    sbWhere.AppendFormat(" AND h.STORENO IN({0})", getSTORENO_QueryString(ConditionsType.PRODNO, args["PRODNO"].ToString()));

                }
                //客戶門號MSISDN LIKE
                if (args["MSISDN"].ToString() != "")
                {
                    sbWhere.AppendFormat(" AND h.SERIAL_NO IN({0})", getSERIAL_NO_QueryString(ConditionsType.MSISDN, args["MSISDN"].ToString()));
                    sbWhere.AppendFormat(" AND h.TDATE IN({0})", getTDATE_QueryString(ConditionsType.MSISDN, args["MSISDN"].ToString()));
                    sbWhere.AppendFormat(" AND h.TERM_NO IN({0})", getTERM_NO_QueryString(ConditionsType.MSISDN, args["MSISDN"].ToString()));
                    sbWhere.AppendFormat(" AND h.STORENO IN({0})", getSTORENO_QueryString(ConditionsType.MSISDN, args["MSISDN"].ToString()));

                }
                //付款方式
                if (args["PAY_METHOD"].ToString() != "")
                {
                    sbWhere.AppendFormat(" AND h.SERIAL_NO IN({0})", getSERIAL_NO_QueryString(ConditionsType.PAY_METHOD, args["PAY_METHOD"].ToString()));
                    sbWhere.AppendFormat(" AND h.TDATE IN({0})", getTDATE_QueryString(ConditionsType.PAY_METHOD, args["PAY_METHOD"].ToString()));
                    sbWhere.AppendFormat(" AND h.TERM_NO IN({0})", getTERM_NO_QueryString(ConditionsType.PAY_METHOD, args["PAY_METHOD"].ToString()));
                    sbWhere.AppendFormat(" AND h.STORENO IN({0})", getSTORENO_QueryString(ConditionsType.PAY_METHOD, args["PAY_METHOD"].ToString()));

                }
            }

            sbSql.AppendFormat(@"
                       SELECT ROWNUM ITEMS, 
                        T1.SALE_STATUS, 
                        T1.SALE_STATUS_NAME, 
                        T1.TRADE_DATE, 
                        T1.SALE_NO, 
                        T1.MACHINE_ID, 
                        T1.MSISDN, 
                        T1.INVOICE_NO, 
                        T1.SALE_TOTAL_AMOUNT, 
                        T1.PAY_METHOD, 
                        T1.SALE_PERSON,
                        T1.SALE_PERSON_NAME,
                        (T1.SALE_PERSON || ' ' || T1.SALE_PERSON_NAME) SALE_PERSON_SHOW_NAME,
                        T1.CREATE_USER, 
                        T1.CREATE_USER_NAME,
                        (T1.CREATE_USER || ' ' || T1.CREATE_USER_NAME) CREATE_USER_SHOW_NAME,
                        T1.MODI_USER, 
                        T1.MODI_USER_NAME,
                        (T1.MODI_USER || ' ' || T1.MODI_USER_NAME) MODI_USER_SHOW_NAME,
                        T1.MODI_DTM
                        FROM
                        (SELECT H.STATUS AS SALE_STATUS, 
                            '已作廢' SALE_STATUS_NAME,
                             H.CANCEL_DTM AS  TRADE_DATE,
                            H.SERIAL_NO AS SALE_NO,
                            H.CANCEL_MACHINE AS MACHINE_ID,
                            (SELECT DISTINCT  MOTONUM FROM TDL WHERE  SERIAL_NO = H.SERIAL_NO AND TDATE = H.TDATE AND TERM_NO = H.TERM_NO AND STORENO = H.STORENO AND ROWNUM=1) MSISDN,
                             H.TAX_NO AS  INVOICE_NO,
                            H.SUBTOT SALE_TOTAL_AMOUNT,
                           CASE
                          WHEN H.CASH IS NOT NULL THEN '現金'
                          WHEN H.CREDITNO IS NOT NULL THEN '信用卡'
                          WHEN H.GCARDNO IS NOT NULL THEN 'HAPPYGO'
                          END AS PAY_METHOD ,
                            H.CANCEL_USER AS SALE_PERSON,
                            E.EMPNAME AS SALE_PERSON_NAME,
                            H.CANCEL_USER AS CREATE_USER,
                            E.EMPNAME AS  CREATE_USER_NAME,
                            H.CANCEL_USER AS MODI_USER,
                            E.EMPNAME AS  MODI_USER_NAME,
                            H.CANCEL_DTM AS MODI_DTM,
                            H.STATUS
                        FROM THD H ,EMPLOYEE E
                        WHERE H.CANCEL_USER = E.EMPNO(+)
                        AND H.STATUS = '1' 
                        {0}
                        ORDER BY H.CANCEL_DTM, H.CANCEL_MACHINE, H.SERIAL_NO) T1", sbWhere);

            DataTable dt = OracleDBUtil.Query_Data(sbSql.ToString());
            return dt;
        }


        /// <summary>
        /// 取得銷售資料
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public DataTable getOLDPOS(OrderedDictionary args)
        {
            StringBuilder sbSql = new StringBuilder();
            StringBuilder sbWhere = new StringBuilder();
            bool isM = false;
            // 門市編號
            if (args["STORENO"].ToString().Length != 0 && args["STORENO"].ToString().ToUpper() != "HQ")
                sbWhere.AppendFormat(" AND H.STORENO = {0}", OracleDBUtil.SqlStr("R" + args["STORENO"].ToString()));

            //發票號碼 invoice_head,manual_invoice_head LIKE
            if (args["INVOICE_NO"].ToString() != "")
            {
                isM = true;
                sbWhere.AppendFormat(" AND H.TAX_NO IN({0})", args["INVOICE_NO"].ToString());
            }


            // 交易序號
            if (args["SALE_NO"].ToString() != "")
            {
                isM = true;
                sbWhere.AppendFormat(" AND h.SERIAL_NO like '%{0}%'", args["SALE_NO"].ToString());
            }



            if (!isM)
            {
                // 交易日期 起
                if (args["S_DATE"].ToString() != "")
                    sbWhere.AppendFormat(" AND TRUNC(h.CANCEL_DTM) >= TO_DATE({0},'yyyy/MM/dd')", OracleDBUtil.SqlStr(args["S_DATE"].ToString()));
                // 交易日期 止
                if (args["E_DATE"].ToString() != "")
                    sbWhere.AppendFormat(" AND TRUNC(h.CANCEL_DTM) <= TO_DATE({0},'yyyy/MM/dd')", OracleDBUtil.SqlStr(args["E_DATE"].ToString()));
                // 機台
                if (args["MACHINE_ID"].ToString() != "")
                    sbWhere.AppendFormat(" AND h.CANCEL_MACHINE like '%{0}%'", args["MACHINE_ID"].ToString());


                // 銷售人員
                if (args["SALE_PERSON"].ToString() != "")
                    sbWhere.AppendFormat(" AND h.CANCEL_USER = {0}", OracleDBUtil.SqlStr(args["SALE_PERSON"].ToString()));


                //促銷代碼 SALE_DETAIL LIKE
                if (args["PROMOTION_CODE"].ToString() != "")
                {
                    sbWhere.AppendFormat(" AND h.SERIAL_NO IN({0})", getSERIAL_NO_QueryString(ConditionsType.PROMOTION_CODE, args["PROMOTION_CODE"].ToString()));
                    sbWhere.AppendFormat(" AND h.TDATE IN({0})", getTDATE_QueryString(ConditionsType.PROMOTION_CODE, args["PROMOTION_CODE"].ToString()));
                    sbWhere.AppendFormat(" AND h.TERM_NO IN({0})", getTERM_NO_QueryString(ConditionsType.PROMOTION_CODE, args["PROMOTION_CODE"].ToString()));
                    sbWhere.AppendFormat(" AND h.STORENO IN({0})", getSTORENO_QueryString(ConditionsType.PROMOTION_CODE, args["PROMOTION_CODE"].ToString()));
                }
                //商品料號 SALE_DETAIL
                if (args["PRODNO"].ToString() != "")
                {
                    sbWhere.AppendFormat(" AND h.SERIAL_NO IN({0})", getSERIAL_NO_QueryString(ConditionsType.PRODNO, args["PRODNO"].ToString()));
                    sbWhere.AppendFormat(" AND h.TDATE IN({0})", getTDATE_QueryString(ConditionsType.PRODNO, args["PRODNO"].ToString()));
                    sbWhere.AppendFormat(" AND h.TERM_NO IN({0})", getTERM_NO_QueryString(ConditionsType.PRODNO, args["PRODNO"].ToString()));
                    sbWhere.AppendFormat(" AND h.STORENO IN({0})", getSTORENO_QueryString(ConditionsType.PRODNO, args["PRODNO"].ToString()));

                }
                //客戶門號MSISDN LIKE
                if (args["MSISDN"].ToString() != "")
                {
                    sbWhere.AppendFormat(" AND h.SERIAL_NO IN({0})", getSERIAL_NO_QueryString(ConditionsType.MSISDN, args["MSISDN"].ToString()));
                    sbWhere.AppendFormat(" AND h.TDATE IN({0})", getTDATE_QueryString(ConditionsType.MSISDN, args["MSISDN"].ToString()));
                    sbWhere.AppendFormat(" AND h.TERM_NO IN({0})", getTERM_NO_QueryString(ConditionsType.MSISDN, args["MSISDN"].ToString()));
                    sbWhere.AppendFormat(" AND h.STORENO IN({0})", getSTORENO_QueryString(ConditionsType.MSISDN, args["MSISDN"].ToString()));

                }
                //付款方式
                if (args["PAY_METHOD"].ToString() != "")
                {
                    sbWhere.AppendFormat(" AND h.SERIAL_NO IN({0})", getSERIAL_NO_QueryString(ConditionsType.PAY_METHOD, args["PAY_METHOD"].ToString()));
                    sbWhere.AppendFormat(" AND h.TDATE IN({0})", getTDATE_QueryString(ConditionsType.PAY_METHOD, args["PAY_METHOD"].ToString()));
                    sbWhere.AppendFormat(" AND h.TERM_NO IN({0})", getTERM_NO_QueryString(ConditionsType.PAY_METHOD, args["PAY_METHOD"].ToString()));
                    sbWhere.AppendFormat(" AND h.STORENO IN({0})", getSTORENO_QueryString(ConditionsType.PAY_METHOD, args["PAY_METHOD"].ToString()));

                }
            }

            sbSql.AppendFormat(@"
                       SELECT *
                        FROM THD H ,TDL D
                        WHERE H.STATUS = '1' AND H.TDATE = D.TDATE AND H.SERIAL_NO = D.SERIAL_NO AND H.STORENO = D.STORENO AND H.TERM_NO = D.TERM_NO 
                        {0}
                        ORDER BY H.CANCEL_DTM, H.CANCEL_MACHINE, H.SERIAL_NO", sbWhere);

            DataTable dt = OracleDBUtil.Query_Data(sbSql.ToString());
            return dt;
        }



		/// <summary>
        /// 取得 SERIAL_NO DB Query String
		/// </summary>
		/// <param name="type">類型</param>
		/// <param name="arg">關鍵字</param>
		/// <returns>T-SQL Query String</returns>
        private string getSERIAL_NO_QueryString(ConditionsType type, string arg)
        {
            StringBuilder POSUUID_MASTER = new StringBuilder();
            string sqlStr = string.Empty;
            switch (type)
            {
                case ConditionsType.PROMOTION_CODE:
                    // 促銷代碼
                    sqlStr = string.Format(@"SELECT SERIAL_NO FROM TDL where PROMOTENO like '%{0}%' GROUP BY SERIAL_NO,TDATE,TERM_NO,STORENO", arg);
                    break;
                case ConditionsType.PRODNO:
                    // 商品料號
                    sqlStr = string.Format(@"SELECT SERIAL_NO FROM TDL where PLU_NO ={0} GROUP BY SERIAL_NO,TDATE,TERM_NO,STORENO", OracleDBUtil.SqlStr(arg));
                    break;
                case ConditionsType.MSISDN:
                    // 客戶門號
                    sqlStr = string.Format(@"SELECT SERIAL_NO FROM TDL where MOTONUM like '%{0}%' GROUP BY SERIAL_NO,TDATE,TERM_NO,STORENO ", arg);
                    break;
                case ConditionsType.PAY_METHOD:
                    // 付款方式
                    switch (arg)
                    {
                        case "1" : //現金
                            sqlStr = "SELECT  SERIAL_NO FROM THD WHERE CASH IS NOT NULL GROUP BY  SERIAL_NO,TDATE,TERM_NO,STORENO ";

                            break;
                        case "2"://信用卡
                            sqlStr = "SELECT  SERIAL_NO FROM THD WHERE CREDITNO IS NOT NULL GROUP BY  SERIAL_NO,TDATE,TERM_NO,STORENO ";
                            break;
                        case "3": //HAPPY GO
                            sqlStr = "SELECT  SERIAL_NO FROM THD WHERE GCARDNO IS NOT NULL GROUP BY  SERIAL_NO,TDATE,TERM_NO,STORENO ";
                            break;
                    }
                    break;
                default:
                    return "''";
            }
            return sqlStr;
        }

       

        /// <summary>
        /// 取得 TDATE DB Query String
        /// </summary>
        /// <param name="type">類型</param>
        /// <param name="arg">關鍵字</param>
        /// <returns>T-SQL Query String</returns>
        private string getTDATE_QueryString(ConditionsType type, string arg)
        {
            StringBuilder POSUUID_MASTER = new StringBuilder();
            string sqlStr = string.Empty;
            switch (type)
            {
                case ConditionsType.PROMOTION_CODE:
                    // 促銷代碼
                    sqlStr = string.Format(@"SELECT TDATE FROM TDL where PROMOTENO like '%{0}%' GROUP BY SERIAL_NO,TDATE,TERM_NO,STORENO", arg);
                    break;
                case ConditionsType.PRODNO:
                    // 商品料號
                    sqlStr = string.Format(@"SELECT TDATE FROM TDL where PLU_NO ={0} GROUP BY SERIAL_NO,TDATE,TERM_NO,STORENO", OracleDBUtil.SqlStr(arg));
                    break;
                case ConditionsType.MSISDN:
                    // 客戶門號
                    sqlStr = string.Format(@"SELECT TDATE FROM TDL where MOTONUM like '%{0}%' GROUP BY SERIAL_NO,TDATE,TERM_NO,STORENO ", arg);
                    break;
                case ConditionsType.PAY_METHOD:
                    // 付款方式
                    switch (arg)
                    {
                        case "1": //現金
                            sqlStr = "SELECT  TDATE FROM THD WHERE CASH IS NOT NULL GROUP BY  SERIAL_NO,TDATE,TERM_NO,STORENO ";

                            break;
                        case "2"://信用卡
                            sqlStr = "SELECT  TDATE FROM THD WHERE CREDITNO IS NOT NULL GROUP BY  SERIAL_NO,TDATE,TERM_NO,STORENO ";
                            break;
                        case "3": //HAPPY GO
                            sqlStr = "SELECT  TDATE FROM THD WHERE GCARDNO IS NOT NULL GROUP BY  SERIAL_NO,TDATE,TERM_NO,STORENO ";
                            break;
                    }
                    break;
                default:
                    return "''";
            }
            return sqlStr;
        }
        /// <summary>
        /// 取得 TERM_NO DB Query String
        /// </summary>
        /// <param name="type">類型</param>
        /// <param name="arg">關鍵字</param>
        /// <returns>T-SQL Query String</returns>
        private string getTERM_NO_QueryString(ConditionsType type, string arg)
        {
            StringBuilder POSUUID_MASTER = new StringBuilder();
            string sqlStr = string.Empty;
            switch (type)
            {
                case ConditionsType.PROMOTION_CODE:
                    // 促銷代碼
                    sqlStr = string.Format(@"SELECT TERM_NO FROM TDL where PROMOTENO like '%{0}%' GROUP BY SERIAL_NO,TDATE,TERM_NO,STORENO", arg);
                    break;
                case ConditionsType.PRODNO:
                    // 商品料號
                    sqlStr = string.Format(@"SELECT TERM_NO FROM TDL where PLU_NO ={0} GROUP BY SERIAL_NO,TDATE,TERM_NO,STORENO", OracleDBUtil.SqlStr(arg));
                    break;
                case ConditionsType.MSISDN:
                    // 客戶門號
                    sqlStr = string.Format(@"SELECT TERM_NO FROM TDL where MOTONUM like '%{0}%' GROUP BY SERIAL_NO,TDATE,TERM_NO,STORENO ", arg);
                    break;
                case ConditionsType.PAY_METHOD:
                    // 付款方式
                    switch (arg)
                    {
                        case "1": //現金
                            sqlStr = "SELECT TERM_NO FROM THD WHERE CASH IS NOT NULL GROUP BY  SERIAL_NO,TDATE,TERM_NO,STORENO ";

                            break;
                        case "2"://信用卡
                            sqlStr = "SELECT TERM_NO FROM THD WHERE CREDITNO IS NOT NULL GROUP BY  SERIAL_NO,TDATE,TERM_NO,STORENO ";
                            break;
                        case "3": //HAPPY GO
                            sqlStr = "SELECT  TERM_NO FROM THD WHERE GCARDNO IS NOT NULL GROUP BY  SERIAL_NO,TDATE,TERM_NO,STORENO ";
                            break;
                    }
                    break;
                default:
                    return "''";
            }
            return sqlStr;
        }


        /// <summary>
        /// 取得 STORENO DB Query String
        /// </summary>
        /// <param name="type">類型</param>
        /// <param name="arg">關鍵字</param>
        /// <returns>T-SQL Query String</returns>
        private string getSTORENO_QueryString(ConditionsType type, string arg)
        {
            StringBuilder POSUUID_MASTER = new StringBuilder();
            string sqlStr = string.Empty;
            switch (type)
            {
                case ConditionsType.PROMOTION_CODE:
                    // 促銷代碼
                    sqlStr = string.Format(@"SELECT STORENO FROM TDL where PROMOTENO like '%{0}%' GROUP BY SERIAL_NO,TDATE,TERM_NO,STORENO", arg);
                    break;
                case ConditionsType.PRODNO:
                    // 商品料號
                    sqlStr = string.Format(@"SELECT STORENO FROM TDL where PLU_NO ={0} GROUP BY SERIAL_NO,TDATE,TERM_NO,STORENO", OracleDBUtil.SqlStr(arg));
                    break;
                case ConditionsType.MSISDN:
                    // 客戶門號
                    sqlStr = string.Format(@"SELECT STORENO FROM TDL where MOTONUM like '%{0}%' GROUP BY SERIAL_NO,TDATE,TERM_NO,STORENO ", arg);
                    break;
                case ConditionsType.PAY_METHOD:
                    // 付款方式
                    switch (arg)
                    {
                        case "1": //現金
                            sqlStr = "SELECT  STORENO FROM THD WHERE CASH IS NOT NULL GROUP BY  SERIAL_NO,TDATE,TERM_NO,STORENO ";

                            break;
                        case "2"://信用卡
                            sqlStr = "SELECT  STORENO FROM THD WHERE CREDITNO IS NOT NULL GROUP BY  SERIAL_NO,TDATE,TERM_NO,STORENO ";
                            break;
                        case "3": //HAPPY GO
                            sqlStr = "SELECT  STORENO FROM THD WHERE GCARDNO IS NOT NULL GROUP BY  SERIAL_NO,TDATE,TERM_NO,STORENO ";
                            break;
                    }
                    break;
                default:
                    return "''";
            }
            return sqlStr;
        }
      



		
	}
}
