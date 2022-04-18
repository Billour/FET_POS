using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Advtek.Utility;
using System.Data;
using System.Data.OracleClient;

namespace FET.POS.Model.Facade.FacadeImpl
{
    public class Store_Facade
    {
        /// <summary>
        /// 共用條件
        /// 一、 不包含已關閉的門市, 
        /// 二、 不包含暫停營業的門市
        /// </summary>
        private string Conditions = @" AND TO_CHAR(SYSDATE,'yyyyMMdd') <= NVL(S.CLOSEDATE,'99991231') AND (TO_CHAR(SYSDATE,'yyyyMMdd') < NVL(S.STOP_BDATE, TO_CHAR(SYSDATE+1,'yyyyMMdd')) 
                OR TO_CHAR(SYSDATE,'yyyyMMdd') > NVL(S.STOP_EDATE,TO_CHAR(SYSDATE-1,'yyyyMMdd'))) ";

        /// <summary>
        /// 取得查詢門市資料(不包含已停止及暫停營業的門市)
        /// </summary>
        /// <param name="STORE_NO">門市編號</param>
        /// <param name="STORENAME">門市名稱</param>
        /// <param name="Zone">區域別</param>
        /// <returns>查詢結果</returns>
        public DataTable Query_Store(string STORE_NO, string STORENAME, string Zone)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("SELECT S.STORE_NO, S.STORENAME ");
            sb.AppendLine("FROM STORE S ");
            sb.AppendLine("WHERE 1 =1 ");
            sb.AppendLine(Conditions);

            if (!string.IsNullOrEmpty(STORE_NO))
            {
                sb.AppendLine(" AND S.STORE_NO LIKE " + OracleDBUtil.LikeStr(STORE_NO));
            }

            if (!string.IsNullOrEmpty(STORENAME))
            {
                sb.AppendLine(" AND S.STORENAME LIKE " + OracleDBUtil.LikeStr(STORENAME));
            }

            if (!string.IsNullOrEmpty(Zone))
            {
                sb.AppendLine(" AND S.ZONE = " + OracleDBUtil.SqlStr(Zone));
            }

            sb.AppendLine(" ORDER BY S.STORE_NO");

            DataTable dt = OracleDBUtil.Query_Data(sb.ToString());
            return dt;
        }

        /// <summary>
        /// 取得查詢門市資訊(不包含已停止及暫停營業的門市)
        /// </summary>
        /// <param name="STORE_NO">門市編號</param>
        /// <returns>DataTable</returns>
        public DataTable Query_StoreInfo(string STORE_NO)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(@"SELECT * 
                            FROM STORE S 
                            WHERE 1 =1 " + Conditions + @"
                            AND S.STORE_NO = " + OracleDBUtil.SqlStr(STORE_NO)
                        );

            DataTable dt = OracleDBUtil.Query_Data(sb.ToString());
            return dt;
        }

        /// <summary>
        /// 取得門市權重比例(不包含已停止及暫停營業的門市)
        /// </summary>
        /// <param name="STORE_NO">門市編號</param>
        /// <returns></returns>
        public DataTable Query_StoreWEIGHT(string STORE_NO)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT S.STORE_NO, S.STORENAME, S.BRANCHNO, NVL(W.WEIGHT,0) ");
            sb.Append("FROM STORE S, STORE_WEIGHT_DISTRIBUTE W ");
            sb.Append("WHERE S.STORE_NO = W.STORE_NO(+) ");
            sb.Append(Conditions);
            sb.Append(" AND S.STORE_NO = " + OracleDBUtil.SqlStr(STORE_NO));

            DataTable dt = OracleDBUtil.Query_Data(sb.ToString());
            return dt;
        }

        /// <summary>
        /// 取得該門市的狀態：1 暫停營業, 2 已關閉
        /// </summary>
        /// <param name="STORE_NO">門市編號</param>
        /// <returns>1 or 2</returns>
        public int Query_StoreStatus(string STORE_NO)
        {
            OracleConnection conn = null;
            int status = 0;
            try
            {
                conn = OracleDBUtil.GetConnection();

                StringBuilder sb = new StringBuilder();
                sb.Append("SELECT * ");
                sb.Append("FROM STORE ");
                sb.Append("WHERE 1 =1 ");
                sb.Append("AND TO_CHAR(SYSDATE,'yyyyMMdd') <= NVL(CLOSEDATE,'99991231') ");   //不包含已關閉的門市
                sb.Append("AND STORE_NO = " + OracleDBUtil.SqlStr(STORE_NO));

                DataTable dt = OracleDBUtil.GetDataSet(conn, sb.ToString()).Tables[0];

                if (dt.Rows.Count == 0)
                {
                    status = 2;    //已關閉的門市
                }
                else
                {
                    dt = null;
                    sb.Length = 0;

                    sb.Append("SELECT * ");
                    sb.Append("FROM STORE ");
                    sb.Append("WHERE 1 =1 ");
                    sb.Append("AND (TO_CHAR(SYSDATE,'yyyyMMdd') < NVL(STOP_BDATE, TO_CHAR(SYSDATE+1,'yyyyMMdd')) ");
                    sb.Append("OR  TO_CHAR(SYSDATE,'yyyyMMdd') > NVL(STOP_EDATE,TO_CHAR(SYSDATE-1,'yyyyMMdd'))) "); //也不包含暫停營業的門市
                    sb.Append("AND STORE_NO = " + OracleDBUtil.SqlStr(STORE_NO));

                    dt = OracleDBUtil.GetDataSet(conn, sb.ToString()).Tables[0];
                    if (dt.Rows.Count == 0)
                        status = 1; //暫停營業的門市

                }

                dt.Dispose();
                return status;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (conn.State == ConnectionState.Open) conn.Close();
                conn.Dispose();
                OracleConnection.ClearAllPools();
            }
        }

        /// <summary>
        /// 取得門市名稱，以及所屬區域名稱(不包含已停止及暫停營業的門市)
        /// </summary>
        /// <param name="STORENO">門市編號</param>
        /// <returns></returns>
        public DataTable Query_StoreZone_ByKey(string STORENO)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT S.STORENAME, ZONE.ZONE, ZONE.ZONE_NAME ");
            sb.Append("FROM STORE S, ZONE ");
            sb.Append("WHERE S.ZONE = ZONE.ZONE ");
            sb.Append("AND S.STORE_NO = " + OracleDBUtil.SqlStr(STORENO));
            sb.Append(Conditions);

            DataTable dt = OracleDBUtil.Query_Data(sb.ToString());
            return dt;
        }

        /// <summary>
        /// 取得某區域底下所有的門市(不包含已停止及暫停營業的門市)
        /// </summary>
        /// <param name="ZONE">區別代碼</param>
        /// <returns></returns>
        public DataTable Query_Store_ByZone(string ZONE)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT * FROM STORE S ");
            sb.Append("WHERE 1=1 ");
            sb.Append(Conditions);
            sb.Append(" AND S.ZONE = " + OracleDBUtil.SqlStr(ZONE));

            return OracleDBUtil.Query_Data(sb.ToString());
        }

        /// <summary>
        /// 取得門市名稱
        /// </summary>
        /// <param name="STORENO">門市編號</param>
        /// <returns>門市名稱</returns>
        public string GetStoreName(string STORENO)
        {
            string str = "";

            StringBuilder sb = new StringBuilder();
            sb.AppendLine(@"SELECT S.STORENAME
                            FROM   STORE S 
                            WHERE 1=1 " + Conditions + @"
                            AND STORE_NO = " + OracleDBUtil.SqlStr(STORENO)
                         );

            DataTable dt = OracleDBUtil.Query_Data(sb.ToString());
            if (dt.Rows.Count > 0)
            {
                str = dt.Rows[0]["STORENAME"].ToString();
            }

            return str;
        }

        /// <summary>
        /// 查詢該門市是否為閉店門市
        /// </summary>
        /// <param name="STORE_NO">門市編號</param>
        /// <returns>門市名稱</returns>
        public string GetCloseStoreName(string STORE_NO)
        {
            string returnValue = "";

            StringBuilder sb = new StringBuilder();
            sb.AppendLine("SELECT STORENAME FROM STORE WHERE 1 = 1 AND  TO_DATE(CLOSEDATE,'YYYYMMDD') < TRUNC(SYSDATE) ");
            sb.AppendLine("AND STORE_NO = " + OracleDBUtil.SqlStr(STORE_NO));

            DataTable dt = OracleDBUtil.Query_Data(sb.ToString());

            if (dt.Rows.Count > 0)
            {
                returnValue = dt.Rows[0]["STORENAME"].ToString();
            }
            return returnValue;
        }


        /// <summary>
        /// 取得門市資料(不包含已停止及暫停營業的門市)
        /// </summary>
        /// <returns>查詢結果</returns>
        public DataTable Get_Store(string Zone)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("SELECT S.STORE_NO||'-'||S.STORENAME STORENAME,S.STORE_NO ");
            sb.AppendLine("FROM STORE S ");
            sb.AppendLine("WHERE 1 =1 ");
            sb.AppendLine(Conditions);
               if (!string.IsNullOrEmpty(Zone))
            {
                sb.AppendLine(" AND S.ZONE = " + OracleDBUtil.SqlStr(Zone));
            }

            sb.AppendLine(" ORDER BY S.STORE_NO");

            DataTable dt = OracleDBUtil.Query_Data(sb.ToString());

            DataRow dr = dt.NewRow();
            dr["STORE_NO"] = "";
            dr["STORENAME"] = "-請選擇-";
            dt.Rows.InsertAt(dr, 0);
            return dt;
        }

    }
}
