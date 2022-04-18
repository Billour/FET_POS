using System;
using System.Web;
using System.Data;
using System.Text;

using Advtek.Utility;
using FET.POS.Model.DTO;
using System.Data.OracleClient;

namespace FET.POS.Model.Common
{
    public class OPT05_PageHelper
    {
        public string USE_TYPE_TextField = "CNAME";
        public string USE_TYPE_ValueField = "PARA_NO";

        /// <summary>
        /// 取得發票設定檔使用用途集合
        /// </summary>
        /// <returns></returns>
        public DataTable GetUSE_TYPEs()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(@"SELECT " + USE_TYPE_TextField + ", " + USE_TYPE_ValueField + @" 
                          FROM COMMON_CODE
                            WHERE TYPE_NO='1' 
                            AND SYSTEM_NO='WEB_POS' 
                            AND PARA_NO NOT IN('3','4')");

            DataTable dt = OracleDBUtil.Query_Data(sb.ToString());
            return dt;
        }

        /// <summary>
        /// 總部發票設定資料查詢  
        /// </summary>
        /// <returns></returns>
        public DataTable GetHeadQuarterInvoice(string STORE_NO, string STORENAME, string USE_TYPE
            , string S_USE_YM, string E_USE_YM)
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine("SELECT * FROM VW_OPT05 ");
            sb.AppendLine("WHERE 1 = 1");

            if (!string.IsNullOrEmpty(STORE_NO))
            {
                sb.AppendLine(" AND STORE_NO LIKE " + OracleDBUtil.LikeStr(STORE_NO.Trim()));
            }

            if (!string.IsNullOrEmpty(STORENAME))
            {
                sb.AppendLine(" AND STORENAME LIKE " + OracleDBUtil.LikeStr(STORENAME.Trim()));
            }

            if (!string.IsNullOrEmpty(USE_TYPE))
            {
                sb.AppendLine(" AND USE_TYPE = " + OracleDBUtil.SqlStr(USE_TYPE.Trim()));
            }

            if (!string.IsNullOrEmpty(S_USE_YM))
            {
                sb.AppendLine(" AND S_USE_YM >= " + OracleDBUtil.SqlStr(S_USE_YM.Trim()));
            }

            if (!string.IsNullOrEmpty(E_USE_YM))
            {
                sb.AppendLine(" AND E_USE_YM <= " + OracleDBUtil.SqlStr(E_USE_YM.Trim()));
            }

            DataTable dt = OracleDBUtil.Query_Data(sb.ToString());
            return dt;
        }

        public DataTable GetHeadQuarterInvoiceHQ(string STORE_NO, string STORENAME, string USE_TYPE
           , string S_USE_YM, string E_USE_YM)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("SELECT * FROM VW_OPT05 ");
            sb.AppendLine("WHERE 1 = 1 "); //and USE_TYPE <> 3 

            if (!string.IsNullOrEmpty(STORE_NO))
            {
                sb.AppendLine(" AND STORE_NO LIKE " + OracleDBUtil.LikeStr(STORE_NO.Trim()));
            }

            if (!string.IsNullOrEmpty(STORENAME))
            {
                sb.AppendLine(" AND STORENAME LIKE " + OracleDBUtil.LikeStr(STORENAME.Trim()));
            }

            if (!string.IsNullOrEmpty(USE_TYPE))
            {
                sb.AppendLine(" AND USE_TYPE = " + OracleDBUtil.SqlStr(USE_TYPE.Trim()));
            }

            if (!string.IsNullOrEmpty(S_USE_YM))
            {
                sb.AppendLine(" AND S_USE_YM >= " + OracleDBUtil.SqlStr(S_USE_YM.Trim()));
            }

            if (!string.IsNullOrEmpty(E_USE_YM))
            {
                sb.AppendLine(" AND E_USE_YM <= " + OracleDBUtil.SqlStr(E_USE_YM.Trim()));
            }

            DataTable dt = OracleDBUtil.Query_Data(sb.ToString());
            return dt;
        }

        /// <summary>
        /// 門市離線發票設定明細查詢
        /// </summary>
        /// <param name="ASSIGN_ID"></param>
        /// <param name="STORE_NO"></param>
        /// <returns></returns>
        public DataTable GetStoreMachineInvoiceData(string ASSIGN_ID, string STORE_NO)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(@"SELECT 'F' AS IsModify
                            , A.MACHINE_ID
                            , A.HOST_NO
                            , B.ASSIGN_ID
                            , B.ASSIGN_ID AS oldassign_id
                            , B.STORE_NO
                            , B.start_no
                            , B.end_no
                            , B.start_no AS oldstart_no
                            , B.end_no AS oldend_no
                            , B.sheet_count
                            , B.current_no
                            , B.modi_user
                            , TO_CHAR(B.modi_dtm, 'yyyy/mm/dd hh24:mi:ss') modi_dtm   
                                  FROM (
                                        SELECT MACHINE_ID, HOST_NO, STORE_NO
                                        FROM STORE_TERMINATING_MACHINE
                                        WHERE STORE_NO=':STORE_NO'
                                    ) A
                                    ,(
                                        SELECT *
                                        FROM   STORE_INVOICE_ASSIGN
                                        WHERE ASSIGN_ID=':ASSIGN_ID' 
                                    ) B
                                    WHERE A.MACHINE_ID=B.MACHINE_ID(+)
                                ORDER BY HOST_NO");

            sb.Replace(":STORE_NO", STORE_NO);
            sb.Replace(":ASSIGN_ID", ASSIGN_ID);

            DataTable dt = OracleDBUtil.Query_Data(sb.ToString());
            return dt;
        }

        /// <summary>
        /// 取得暫存的匯入資料
        /// </summary>
        /// <param name="BATCH_NO"></param>
        /// <returns></returns>
        public DataTable GetImportTempData(string BATCH_NO)
        {

            DataTable dt = new DataTable();

            StringBuilder sb = new StringBuilder();

            sb.AppendLine(@"SELECT A.*, B.STORENAME AS STORE_NAME 
                FROM HQ_INVOICE_ASSIGN_TEMP A,STORE B  
                WHERE A.STORE_NO=B.STORE_NO(+) 
                AND A.BATCH_NO=" + OracleDBUtil.SqlStr(BATCH_NO));

            dt = OracleDBUtil.Query_Data(sb.ToString());

            return dt;
        }

        /// <summary>
        /// 取得門市離線設定主檔特定的一筆資料
        /// </summary>
        /// <param name="ASSIGN_ID">UUID</param>
        /// <returns>查詢結果</returns>
        public static DataRow Query_HeadQuarterInvoice_ByKey(string ASSIGN_ID)
        {
            OracleConnection Connection = null;
            try
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("SELECT * FROM VW_OPT05 ");
                sb.Append("WHERE ASSIGN_ID = " + OracleDBUtil.SqlStr(ASSIGN_ID));

                Connection = OracleDBUtil.GetConnection();
                DataTable dt = OracleDBUtil.GetDataSet(Connection, sb.ToString()).Tables[0];

                return dt.Rows[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (Connection.State == ConnectionState.Open) Connection.Close();
                Connection.Dispose();
                OracleConnection.ClearAllPools();
            }
        }


    }
}
