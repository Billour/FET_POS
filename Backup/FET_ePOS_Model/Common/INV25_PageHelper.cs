using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Advtek.Utility;
using System.Data.OracleClient;
using System.Data;

namespace FET.POS.Model.Common
{
    public class INV25_PageHelper
    {

        /// <summary>
        /// 取得商品料號的現有庫存數 (倉別 = 銷售倉)
        /// </summary>
        /// <returns></returns>
        public static long GetInvOnHandCurrent(string PRODNO, string STORENO)
        {
            DataTable dt = null;
            OracleConnection objConn = null;

            try
            {
                StringBuilder sb = new StringBuilder();
                sb.AppendLine(@"SELECT ON_HAND_QTY 
                                FROM INV_ON_HAND_CURRENT 
                                WHERE 1 =1 
                                AND STOCK_ID IN (select INV_GoodLOCUUID() from dual)
                                AND PRODNO = " + OracleDBUtil.SqlStr(PRODNO) + @" 
                                AND TRIM(STORE_NO) = " + OracleDBUtil.SqlStr(STORENO)
                              );

                objConn = OracleDBUtil.GetConnection();
                dt = OracleDBUtil.GetDataSet(objConn, sb.ToString()).Tables[0];

                long Qty = 0;
                if (dt.Rows.Count > 0)
                {
                    Qty = Convert.ToInt64(dt.Rows[0]["ON_HAND_QTY"].ToString());
                }

                return Qty;
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

        }

        /// <summary>
        /// 取得查詢移撥作業(Master Data)的一筆資料
        /// </summary>
        /// <param name="STNO">UUID</param>
        /// <returns>查詢結果</returns>
        public static DataTable Query_StoreTransferM_ByKey(string STNO)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(@"SELECT * 
                            FROM STORETRANSFER_M 
                            WHERE STNO = " + OracleDBUtil.SqlStr(STNO)
                         );

            DataTable dt = OracleDBUtil.Query_Data(sb.ToString());
            return dt;
        }

        /// <summary>
        /// 取得查詢移撥作業(Detail Data)的一筆資料
        /// </summary>
        /// <param name="STNO">主檔_UUID</param>
        /// <param name="SEQNO">項次</param>
        /// <returns>查詢結果</returns>
        public static DataTable Query_StoreTransferD_ByKey(string STNO, string SEQNO)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(@"SELECT *
                            FROM STORETRANSFER_D 
                            WHERE SEQNO = " + OracleDBUtil.SqlStr(SEQNO) + @" 
                            AND STNO = " + OracleDBUtil.SqlStr(STNO)
                        );

            DataTable dt = OracleDBUtil.Query_Data(sb.ToString());
            return dt;
        }

    }
}
