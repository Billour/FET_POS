using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.OracleClient;
using FET.POS.Model.Helper;
using FET.POS.Model.DTO;
using Advtek.Utility;
using FET.POS.Model.Facade.FacadeImpl;

namespace FET.POS.Model.Common
{
    public class INV06_PageHelper
    {
        public static DataTable GetRTNDMethodData(string STORENO)
        {
            INV06_Facade _INV06_Facade = new INV06_Facade();

            //營業日
            string sDate=OracleDBUtil.WorkDay(STORENO);
            DataTable dt = new DataTable();
            dt = _INV06_Facade.QueryRTNDData("", "", "", false, STORENO, "", "", "10",sDate);

            return dt;
        }

        public static DataTable GetRTNDDetailData(string STORENO,string RTNNO)
        {
            INV06_Facade _INV06_Facade = new INV06_Facade();

            DataTable dt = new DataTable();
            dt = _INV06_Facade.QueryRTNDDetailData(STORENO, RTNNO);

            return dt;
        }

        public static DataTable GetRTNDTopData(string STORENO, string RTNNO)
        {
            INV06_Facade _INV06_Facade = new INV06_Facade();

            DataTable dt = new DataTable();
            dt = _INV06_Facade.QueryRTNMTopData(STORENO, RTNNO);

            return dt;
        }

        public static DataTable GetRTNDBottomData(string STORENO, string RTNNO)
        {
            INV06_Facade _INV06_Facade = new INV06_Facade();

            DataTable dt = new DataTable();
            dt = _INV06_Facade.QueryRTNMBottomData(STORENO, RTNNO);

            return dt;
        }

        public static DataTable GETRTND(string SID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(" SELECT * FROM RTND_UP WHERE 1=1");
            strSql.Append(" AND RTND_UP_ID = " + OracleDBUtil.SqlStr(SID));

            DataTable dt = OracleDBUtil.Query_Data(strSql.ToString());
            return dt;
        }

        public static DataTable GETRTNM(string SID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(" SELECT *  FROM RTNM WHERE 1=1");
            strSql.Append(" AND RTNNO = " + OracleDBUtil.SqlStr(SID));

            DataTable dt = OracleDBUtil.Query_Data(strSql.ToString());
            return dt;
        }

        public static DataTable GETRTNMByRTNN_ID(string SID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(" SELECT * FROM RTNM WHERE 1=1");
            strSql.Append(" AND RTNN_ID = " + OracleDBUtil.SqlStr(SID));

            DataTable dt = OracleDBUtil.Query_Data(strSql.ToString());
            return dt;
        }

    }
}
