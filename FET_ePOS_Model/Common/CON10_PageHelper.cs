using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Data;
using System.Data.OracleClient;
using Advtek.Utility;

namespace FET.POS.Model.Common
{
    public class CON10_PageHelper
    {
        public static string TextField = "ZONE_NAME";

        public static string ValueField = "ZONE";

        /// <summary>
        /// 取得區域別設定資料
        /// </summary>
        /// <returns></returns>
        public static DataTable GetZoneTypes()
        {
            DataTable dt = null;
            OracleConnection objConn = null;

            try
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append(" SELECT " + TextField + ", " + ValueField);
                strSql.Append("   FROM  ZONE ");

                objConn = OracleDBUtil.GetConnection();
                dt = OracleDBUtil.GetDataSet(objConn, strSql.ToString()).Tables[0];

                DataRow dr = dt.NewRow();
                dr[TextField] = "請選擇";
                dr[ValueField] = "";
                dt.Rows.InsertAt(dr, 0);
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


    }
}
