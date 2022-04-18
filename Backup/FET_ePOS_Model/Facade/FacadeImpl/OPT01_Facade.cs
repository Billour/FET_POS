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
    public class OPT01_Facade 
    {

        public DataTable Query_PaymentMethodSet(string AccountCode, string PayModeId, string Status, string SDate_S, string SDate_E, string EDate_S, string EDate_E)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(@" SELECT rownum ITEMNO
                                    , M.PAY_MODE_NAME PAY_MODE_NAME
                                    , GetPayModleStatus(S.PAYMENT_METHOD_ID) as Status
                                    , S.PAYMENT_METHOD_ID PAYMENT_METHOD_ID
                                    , S.PAY_MODE_ID PAY_MODE_ID
                                    , substr(S.ACCOUNT_CODE,0,2) ACC1
                                    , substr(S.ACCOUNT_CODE,3,3) ACC2
                                    , substr(S.ACCOUNT_CODE,6,4) ACC3
                                    , substr(S.ACCOUNT_CODE,10,6) ACC4
                                    , substr(S.ACCOUNT_CODE,16,4) ACC5
                                    , substr(S.ACCOUNT_CODE,20,4) ACC6
                                    , S.S_DATE S_DATE
                                    , S.E_DATE E_DATE
                                    , S.CREATE_USER CREATE_USER
                                    , S.CREATE_DTM CREATE_DTM
                                    , S.MODI_USER MODI_USER
                                    , E.EMPNAME MODI_USER_NAME
                                    , S.MODI_DTM MODI_DTM
                            FROM PAYMENT_METHOD_SET S,PAY_MODE M, EMPLOYEE E
                            WHERE S.PAY_MODE_ID = M.PAY_MODE_ID(+) 
                            AND S.MODI_USER = E.EMPNO(+) 
                            AND S.PAY_MODE_ID <>'8'
                        ");

            if (!string.IsNullOrEmpty(AccountCode))
            {
                sb.AppendLine(" AND S.ACCOUNT_CODE like " + OracleDBUtil.LikeStr(AccountCode));
            }
            if (!string.IsNullOrEmpty(PayModeId))
            {
                sb.AppendLine(" AND S.PAY_MODE_ID = " + OracleDBUtil.SqlStr(PayModeId));
            }
            if (!string.IsNullOrEmpty(Status))
            {
                sb.AppendLine("AND GetPayModleStatus(S.PAYMENT_METHOD_ID) = " + OracleDBUtil.SqlStr(Status));
            }
            if (!string.IsNullOrEmpty(SDate_S))
            {
                sb.AppendLine(" and S_DATE >= " + OracleDBUtil.DateStr(SDate_S));
            }
            if (!string.IsNullOrEmpty(SDate_E))
            {
                sb.AppendLine(" and S_DATE <= " + OracleDBUtil.DateStr(SDate_E));
            }
            if (!string.IsNullOrEmpty(EDate_S))
            {
                sb.AppendLine(" and E_DATE >=  " + OracleDBUtil.DateStr(EDate_S));
            }
            if (!string.IsNullOrEmpty(EDate_E))
            {
                sb.AppendLine(" and E_DATE <= " + OracleDBUtil.DateStr(EDate_E));
            }

            DataTable dt = OracleDBUtil.Query_Data(sb.ToString());
            return dt;

        }

        public int Query_PAY_MODE_Count(string PAY_MODE_ID, string sS_DATE, string sE_DATE, string sPAYMENT_METHOD_ID)
        {
            StringBuilder sb = new StringBuilder();
            DateTime dStart = DateTime.Parse(sS_DATE);
            DateTime dStop = DateTime.Parse((string.IsNullOrEmpty(sE_DATE)) ? "9999/12/31" : sE_DATE);

            sb.AppendLine(@"SELECT count(1)
                            FROM PAYMENT_METHOD_SET S, PAY_MODE M
                            WHERE S.PAY_MODE_ID = M.PAY_MODE_ID
                              AND S.PAY_MODE_ID = " + OracleDBUtil.SqlStr(PAY_MODE_ID)
                         + @" AND (maxdate(TO_CHAR (s_date, 'yyyy/mm/dd'), '" + dStart.ToString("yyyy/MM/dd") + @"') 
                            <= mindate(TO_CHAR (nvl(e_date,to_date('9999/12/31','yyyy/MM/dd')), 'yyyy/MM/dd'), '" + dStop.ToString("yyyy/MM/dd") + "') )");
            if (sPAYMENT_METHOD_ID != "")
            {
                sb.AppendLine("  AND PAYMENT_METHOD_ID not in ('" + sPAYMENT_METHOD_ID + "')");
            }

            DataTable dt = OracleDBUtil.Query_Data(sb.ToString());

            if (dt.Rows.Count > 0)
                return Convert.ToInt32(dt.Rows[0][0]);
            else
                return 0;
        }

        public void AddNewOne_PaymentMethodSet(OPT01_PaymentMethodSet_DTO ds)
        {
            OracleConnection objConn = null;
            OracleTransaction objTX = null;

            try
            {
                objConn = OracleDBUtil.GetConnection();
                objTX = objConn.BeginTransaction();

                OracleDBUtil.Insert(objTX, ds.Tables["PAYMENT_METHOD_SET"]);
                OracleDBUtil.Insert(objTX, ds.Tables["PAY_MODE"]);

                objTX.Commit();
            }
            catch (Exception ex)
            {
                objTX.Rollback();
                throw ex;
            }
            finally
            {
                objTX.Dispose();

                if (objConn.State == ConnectionState.Open) objConn.Close();
                objConn.Dispose();
                OracleConnection.ClearAllPools();
            }

        }

        public void UpdateOne_PaymentMethodSet(OPT01_PaymentMethodSet_DTO ds)
        {
            //edit can't edit the pay_mode table value
            //so not update PAY_MODE
            UPDDATEByUUIDME(ds.Tables["PAYMENT_METHOD_SET"], "PAYMENT_METHOD_ID");
            //OracleDBUtil.UPDDATEByUUID(ds.Tables["PAY_MODE"], "PAY_MODE_ID");
        }

        public void Delete_PAYMENT_METHOD_SET(DataTable dt, string eKey)
        {
            OracleDBUtil.DELETEByUUID(dt, eKey);
        }

        /// <summary>
        /// 跟共用程式不同，自已改寫for update
        /// </summary>
        /// <param name="_DT">資料結構</param>
        /// <param name="_strUUIDField">UUID的欄位名稱</param>
        /// <returns>int</returns>
        public static int UPDDATEByUUIDME(DataTable _DT, string _strUUIDField)
        {
            int intResult = 0;
            OracleConnection objConn = null;
            OracleTransaction objTX = null;

            try
            {
                objConn = OracleDBUtil.GetConnection();
                objTX = objConn.BeginTransaction();
                intResult = UPDDATEByUUID(objTX, _DT, _strUUIDField);
                objTX.Commit();

            }
            catch (Exception ex)
            {
                objTX.Rollback();
                throw ex;
            }
            finally
            {
                objTX.Dispose();

                if (objConn.State == ConnectionState.Open) objConn.Close();
                objConn.Dispose();
                OracleConnection.ClearAllPools();
            }
            return intResult;
        }

        public static int UPDDATEByUUID(OracleTransaction objTX, DataTable _DT, string _strUUIDField)
        {
            int intResult = 0;
            StringBuilder _sbScript;
            StringBuilder _sbWhereCondition;

            try
            {
                foreach (DataRow dr in _DT.Rows)
                {
                    _sbScript = new StringBuilder();
                    _sbWhereCondition =

                    _sbScript.Append(" UPDATE " + _DT.TableName.ToString());
                    _sbScript.Append(" SET ");

                    for (int i = 0; i <= _DT.Columns.Count - 1; i++)
                    {
                        if (Convert.IsDBNull(dr[i]) == false || _DT.Columns[i].ColumnName.ToString() == "E_DATE")
                        {
                            //取欄位名稱
                            _sbScript.Append(_DT.Columns[i].ColumnName.ToString() + "=");

                            //取欄位值
                            switch (_DT.Columns[i].DataType.Name.ToUpper())
                            {
                                case "DATETIME":
                                case "DATE":
                                    _sbScript.Append(OracleDBUtil.DateFormate(dr[i]));
                                    break;

                                default:
                                    _sbScript.Append(OracleDBUtil.SqlStr(StringUtil.CStr(dr[i])));
                                    break;
                            }
                            if (i != _DT.Columns.Count - 1) _sbScript.Append(",");
                        }
                        
                    }

                    string tempSQL = _sbScript.ToString();
                    int intLength = tempSQL.Length;
                    string strCol = tempSQL.Substring(intLength - 1, 1);
                    if (strCol == ",") tempSQL = tempSQL.Substring(0, intLength - 1);

                    string strSQL = tempSQL.ToString() + " Where  " + _strUUIDField + "=" + OracleDBUtil.SqlStr(StringUtil.CStr(dr[_strUUIDField]));

                    intResult = OracleDBUtil.ExecuteSql(objTX, strSQL);

                    if (intResult == 0) throw new Exception("UPDATE SQL Execute 失敗. ");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {

            }
            return intResult;
        }


    }
}
