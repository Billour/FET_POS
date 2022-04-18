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
    public class LOG06_Facade
    {
        public DataTable getStoreDiscountPWD(string STORE_NO)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(" SELECT STORE_PW                 ");
            sb.Append("  FROM store         ");
            sb.Append("  WHERE to_date(to_char(sysdate, 'yyyy/mm/dd'),'yyyy/mm/dd') ");
            sb.Append("         Between to_date(NVL(STARTDATE, '20000101'),'yyyymmdd') And to_date(NVL(CLOSEDATE,'30000101'),'yyyymmdd') ");
            if (!string.IsNullOrEmpty(STORE_NO))
            {
                sb.Append(" AND STORE_NO = " + OracleDBUtil.SqlStr(STORE_NO));
            }

            DataTable dt = OracleDBUtil.Query_Data(sb.ToString());
            return dt;
        }

        public void updStoreDiscountPWD(string STORE_NO, string strEncPwd)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(" Update store Set STORE_PW = " + OracleDBUtil.SqlStr(strEncPwd));
            sb.Append("  WHERE to_date(to_char(sysdate, 'yyyy/mm/dd'),'yyyy/mm/dd') ");
            sb.Append("         Between to_date(NVL(STARTDATE, '20000101'),'yyyymmdd') And to_date(NVL(CLOSEDATE,'30000101'),'yyyymmdd') ");
            if (!string.IsNullOrEmpty(STORE_NO))
            {
                sb.Append(" AND STORE_NO = " + OracleDBUtil.SqlStr(STORE_NO));
            }

            OracleConnection objConn = null;
            try
            {
                objConn = OracleDBUtil.GetConnection();
                OracleDBUtil.ExecuteSql(objConn, sb.ToString());
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (objConn.State == ConnectionState.Open)
                    objConn.Close();
                objConn.Dispose();
                OracleConnection.ClearAllPools();
            }
        }

        public DataTable Query_SYSPara(string EMPNO, string USERID)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(" SELECT VALID_START_DATE                 ");
            sb.Append("       ,VALID_END_DATE                   ");
            sb.Append("       ,ENCRYPT_PASSWORD, SID            ");
            sb.Append("  FROM STORE_HEADER_DISCOUNT_PWD         ");
            sb.Append("  WHERE 1 = 1 AND STATUS='1'             ");
            if (!string.IsNullOrEmpty(EMPNO))
            {
                sb.Append(" AND EMPNO = " + OracleDBUtil.SqlStr(EMPNO));
            }

            if (!string.IsNullOrEmpty(USERID))
            {
                sb.Append(" AND USERID = " + OracleDBUtil.SqlStr(USERID));
            }

            DataTable dt = OracleDBUtil.Query_Data(sb.ToString());
            return dt;
        }

        private DataTable getCCIStatus(DataTable dt, string Status)
        {
            var enumDuring =
                dt.AsEnumerable().Select(
                                            dr =>
                                                 new
                                                 {
                                                     Status = "有效",
                                                     BANK_ID = dr.Field<string>("BANK_ID"),
                                                     BANK_NAME = dr.Field<string>("BANK_NAME"),
                                                     INSTELLMENT_ID = dr.Field<string>("INSTELLMENT_ID"),
                                                     PAY_SEQMENT = dr.Field<decimal>("PAY_SEQMENT"),
                                                     SEQMENT_RATE = dr.Field<decimal>("SEQMENT_RATE"),
                                                     S_DATE = dr.Field<DateTime>("S_DATE"),
                                                     E_DATE = dr.Field<DateTime>("E_DATE"),
                                                     CREATE_DTM = dr.Field<DateTime>("CREATE_DTM"),
                                                     CREATE_USER = dr.Field<string>("CREATE_USER"),
                                                     MODI_DTM = dr.Field<DateTime>("MODI_DTM"),
                                                     MODI_USER = dr.Field<string>("MODI_USER")
                                                 }
                                        ).Where((dr => Convert.ToDateTime(System.DateTime.Today) >= dr.S_DATE
                                                    && Convert.ToDateTime(System.DateTime.Today) <= dr.E_DATE));

            var enumOver =
            dt.AsEnumerable().Select(
                                        dr =>
                                             new
                                             {
                                                 Status = "已過期",
                                                 BANK_ID = dr.Field<string>("BANK_ID"),
                                                 BANK_NAME = dr.Field<string>("BANK_NAME"),
                                                 INSTELLMENT_ID = dr.Field<string>("INSTELLMENT_ID"),
                                                 PAY_SEQMENT = dr.Field<decimal>("PAY_SEQMENT"),
                                                 SEQMENT_RATE = dr.Field<decimal>("SEQMENT_RATE"),
                                                 S_DATE = dr.Field<DateTime>("S_DATE"),
                                                 E_DATE = dr.Field<DateTime>("E_DATE"),
                                                 CREATE_DTM = dr.Field<DateTime>("CREATE_DTM"),
                                                 CREATE_USER = dr.Field<string>("CREATE_USER"),
                                                 MODI_DTM = dr.Field<DateTime>("MODI_DTM"),
                                                 MODI_USER = dr.Field<string>("MODI_USER")
                                             }
                                    ).Where((dr => Convert.ToDateTime(System.DateTime.Today) > dr.E_DATE));

            var enumNotYet =
            dt.AsEnumerable().Select(
                                        dr =>
                                             new
                                             {
                                                 Status = "尚未生效",
                                                 BANK_ID = dr.Field<string>("BANK_ID"),
                                                 BANK_NAME = dr.Field<string>("BANK_NAME"),
                                                 INSTELLMENT_ID = dr.Field<string>("INSTELLMENT_ID"),
                                                 PAY_SEQMENT = dr.Field<decimal>("PAY_SEQMENT"),
                                                 SEQMENT_RATE = dr.Field<decimal>("SEQMENT_RATE"),
                                                 S_DATE = dr.Field<DateTime>("S_DATE"),
                                                 E_DATE = dr.Field<DateTime>("E_DATE"),
                                                 CREATE_DTM = dr.Field<DateTime>("CREATE_DTM"),
                                                 CREATE_USER = dr.Field<string>("CREATE_USER"),
                                                 MODI_DTM = dr.Field<DateTime>("MODI_DTM"),
                                                 MODI_USER = dr.Field<string>("MODI_USER")
                                             }
                                    ).Where((dr => Convert.ToDateTime(System.DateTime.Today) < dr.S_DATE));

            var UnionEnum = enumDuring.Union(enumNotYet).Union(enumOver);

            if (Status != "請選擇")
            {
                var enumStatus = UnionEnum.Where(u => u.Status == Status);

                return enumStatus.ToList().ToDataTable();
            }

            return UnionEnum.ToList().ToDataTable();
        }

        public void AddNewOne_SysPara(LOG06_StoreHeaderDiscountPWD ds)
        {
            OracleDBUtil.Insert(ds.Tables["STORE_HEADER_DISCOUNT_PWD"]);
        }

        public void UpdateOne_SysPara(LOG06_StoreHeaderDiscountPWD ds)
        {
            OracleDBUtil.UPDDATEByUUID(ds.Tables["STORE_HEADER_DISCOUNT_PWD"], "SID");
        }

        public DataTable Query_CreditCardSettlement(string InstallmentId)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT CCS.SETTLEMENT_ID, CCS.INSTELLMENT_ID, CCS.LINE_NO, ");
            sb.Append(" CCS.SETTLEMENT_RATE, CCS.CREATE_USER, CCS.CREATE_DTM, ");
            sb.Append(" CCS.MODI_USER, CCS.MODI_DTM, CCS.COST_CENTER_NO, CC.COST_CENTER_NAME ");
            sb.Append(" FROM CREDIT_CARD_SETTLEMMENT CCS ");
            sb.Append(" INNER JOIN COST_CENTER CC ");
            sb.Append(" ON CCS.COST_CENTER_NO = CC.COST_CENTER_NO");
            sb.Append(" WHERE 1 = 1");

            if (!string.IsNullOrEmpty(InstallmentId))
            {
                sb.Append(" AND CCS.INSTELLMENT_ID = " + OracleDBUtil.SqlStr(InstallmentId));
            }

            DataTable dt = OracleDBUtil.Query_Data(sb.ToString());
            return dt;
        }

        public void AddNew_CreditCardSettlements(FET.POS.Model.DTO.OPT03_CreditCardInstallment_DTO ds)
        {
            OracleDBUtil.Insert(ds.Tables["CREDIT_CARD_SETTLEMMENT"]);
        }

        public void Update_CreditCardSettlements(FET.POS.Model.DTO.OPT03_CreditCardInstallment_DTO ds)
        {
            OracleDBUtil.UPDDATEByUUID(ds.Tables["CREDIT_CARD_SETTLEMMENT"], "SETTLEMENT_ID");
        }

    }
}
