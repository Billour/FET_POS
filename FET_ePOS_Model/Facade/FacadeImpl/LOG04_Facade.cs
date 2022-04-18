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
    public class LOG04_Facade  
    {
        public DataTable Query_SysPara(string ParameterCode, string ParameterName, string ParameterCategory)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(" SELECT SPT.SYS_PARA_TYPE_NAME as SYS_PARA_TYPE_NAME  ");
            sb.Append("       ,SP.PARA_KEY   as PARA_KEY                     ");
            sb.Append("       ,SP.PARA_NAME  as PARA_NAME                    ");
            sb.Append("       ,SP.PARA_VALUE as PARA_VALUE                   ");
            sb.Append("       ,SP.PARA_DESC  as PARA_DESC                    ");
            sb.Append("       ,TO_CHAR(SP.MODI_DTM,'yyyy/mm/dd hh24:mi:ss') as MODI_DTM   ");
            sb.Append("       ,EMP.EMPNAME  as MODI_USER                    ");
            sb.Append("       ,SP.PARA_ID    as PARA_ID                      ");
            sb.Append("       ,SP.SYS_PARA_TYPE_ID as SYS_PARA_TYPE_ID       ");
            sb.Append("   FROM SYS_PARA SP ,SYS_PARA_TYPE SPT, EMPLOYEE EMP  ");
            sb.Append("  WHERE 1 = 1                                         ");
            sb.Append("    AND SP.SYS_PARA_TYPE_ID = SPT.SYS_PARA_TYPE_ID(+) ");
            sb.Append("    AND SP.MODI_USER = EMP.EMPNO(+)                      ");

            if (!string.IsNullOrEmpty(ParameterCode))
            {
                sb.Append(" AND SP.PARA_KEY LIKE " + OracleDBUtil.LikeStr(ParameterCode));
            }

            if (!string.IsNullOrEmpty(ParameterName))
            {
                sb.Append(" AND SP.PARA_NAME LIKE " + OracleDBUtil.LikeStr(ParameterName));
            }

            if (!string.IsNullOrEmpty(ParameterCategory))
            {
                sb.Append(" AND SP.SYS_PARA_TYPE_ID = " + OracleDBUtil.SqlStr(ParameterCategory));
            }

            sb.Append("   ORDER BY SP.PARA_KEY ");

            DataTable dt = OracleDBUtil.Query_Data(sb.ToString());
            return dt;
        }

        public void AddNewOne_SysPara(LOG04_SysPara_DTO ds)
        {
            OracleDBUtil.Insert(ds.Tables["SYS_PARA"]);
        }

        public void UpdateOne_SysPara(LOG04_SysPara_DTO ds)
        {
            OracleDBUtil.UPDDATEByUUID(ds.Tables["SYS_PARA"], "PARA_ID");
        }

        public DataTable GetSYS_PARA(string PARA_KEY)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(" SELECT *        ");
            sb.Append("   FROM SYS_PARA ");
            sb.Append("  WHERE 1 = 1    ");

            if (PARA_KEY.Trim() != string.Empty)
            {
                sb.Append(" AND PARA_KEY =  " + OracleDBUtil.SqlStr(PARA_KEY.Trim()));
            }

            DataTable dt = OracleDBUtil.Query_Data(sb.ToString());
            return dt;
        }

    }
}
