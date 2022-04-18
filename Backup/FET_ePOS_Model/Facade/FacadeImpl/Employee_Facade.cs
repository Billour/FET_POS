using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.OracleClient;
using System.Data;
using Advtek.Utility;

namespace FET.POS.Model.Facade.FacadeImpl
{
    public class Employee_Facade
    {
        /// <summary>
        /// 取得員工名稱
        /// </summary>
        /// <param name="EMPNO">員工代號</param>
        /// <returns>員工名稱</returns>
        public string GetEmpName(string EMPNO)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT EMPNAME ");
            sb.Append("FROM EMPLOYEE ");
            sb.Append("WHERE EMPNO = " + OracleDBUtil.SqlStr(EMPNO));

            DataTable dt = OracleDBUtil.Query_Data(sb.ToString());

            string strName = "";
            if (dt.Rows.Count > 0)
            {
                strName = dt.Rows[0]["EMPNAME"].ToString();
            }
            return strName;
        }

        /// <summary>
        /// 取得員工所屬門市代碼
        /// </summary>
        /// <param name="EMPNO">員工代碼</param>
        /// <returns>員工所屬門市代碼</returns>
        public DataTable Query_STORENO(string EMPNO)
        {
            DataTable dt = new DataTable();

            if (!string.IsNullOrEmpty(EMPNO))
            {
                string sSQL = "SELECT STORENO FROM EMPLOYEE WHERE EMPNO = " + OracleDBUtil.SqlStr(EMPNO);
                dt = OracleDBUtil.Query_Data(sSQL);
            }

            return dt;
        }

        /// <summary>
        /// 取得員工所屬門市代碼 by new Table
        /// </summary>
        /// <param name="EMPNO">員工代碼</param>
        /// <returns>員工所屬門市代碼</returns>
        public DataTable Query_SALESCD(string EMPNO)
        {
            DataTable dt = new DataTable();

            if (!string.IsNullOrEmpty(EMPNO))
            {
                string sSQL = "SELECT SALESCD FROM SALESORG_INTERNAL_ID WHERE USERID = " + OracleDBUtil.SqlStr(EMPNO);
                dt = OracleDBUtil.Query_Data(sSQL);
            }

            return dt;
        }

        public DataTable Query_Employee(string EMPNO, string EMPNAME)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT EMPNO, EMPNAME");
            sb.Append(" FROM EMPLOYEE");
            sb.Append(" WHERE 1=1");

            if (!string.IsNullOrEmpty(EMPNO))
            {
                sb.Append(" AND EMPNO LIKE " + OracleDBUtil.LikeStr(EMPNO));
            }

            if (!string.IsNullOrEmpty(EMPNAME))
            {
                sb.Append(" AND EMPNAME LIKE " + OracleDBUtil.LikeStr(EMPNAME));
            }

            DataTable dt = OracleDBUtil.Query_Data(sb.ToString());
            return dt;
        }

        //寄銷廠商 主檔 遠傳聯絡窗口
        public DataTable Query_EmployeesDepartment(string EMPNO, string EMPNAME)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT EMPNO, EMPNAME");
            sb.Append(" FROM EMPLOYEE");
            sb.Append(" WHERE 1=1");

            if (!string.IsNullOrEmpty(EMPNO))
            {
                sb.Append(" AND EMPNO LIKE " + OracleDBUtil.LikeStr(EMPNO));
            }

            if (!string.IsNullOrEmpty(EMPNAME))
            {
                sb.Append(" AND EMPNAME LIKE " + OracleDBUtil.LikeStr(EMPNAME));
            }

            DataTable dt = OracleDBUtil.Query_Data(sb.ToString());
            return dt;
        }

        //取得門市人員角色
        public string Query_RoleType(string StoreNo, string EmpId)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT POSITIONID ");
            sb.Append(" FROM v_retail_salesperson ");
            sb.Append(" WHERE to_date(to_char(sysdate, 'yyyy/mm/dd'),'yyyy/mm/dd') ");
            sb.Append("       Between NVL(STARTDT, to_date('2000/01/01','YYYY/MM/DD')) And NVL(ENDDT,to_date('3000/01/01','YYYY/MM/DD')) ");

            if (!string.IsNullOrEmpty(StoreNo))
            {
                sb.Append(" AND SALESCD = " + OracleDBUtil.SqlStr(StoreNo));
            }

            if (!string.IsNullOrEmpty(EmpId))
            {
                sb.Append(" AND EMPLOYEEID = " + OracleDBUtil.SqlStr(EmpId));
            }

            DataTable dt = OracleDBUtil.Query_Data(sb.ToString());
            string roleType = "";
            if (dt != null && dt.Rows.Count > 0)
            {
                if (dt.Rows[0][0] != null && StringUtil.CStr(dt.Rows[0][0]) == "8")
                    roleType = "1";
                else
                    roleType = "2";
            }

            return roleType;
        }
    }
}
