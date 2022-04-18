using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Advtek.Utility;
using FET.POS.Model.DTO;
using System.Data.OracleClient;
using System.Data;
using FET.POS.Model.Facade.FacadeImpl;

namespace FET.POS.Model.Common
{
    public class ORD04_PageHelper
    {
        public static DataTable GetORDER(string SID)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT ORDER_ITEMS_ID,ORDER_M.ORDER_ID,ORDER_M.ORDDATE,ORDER_M.ORDER_NO, ");
            sb.Append("       ORDER_M.ISOK,ORDER_M.STORE_NO,STORENAME,");
            sb.Append("       ORDQTY, ");
            sb.Append("       STOCK_QTY, ");
            sb.Append("       HQ_ADJ_ORDER_QTY, ");
            sb.Append("       ORDER_D.REMARK ");
            sb.Append(" FROM   ORDER_M, ORDER_D, STORE ");
            sb.Append(" WHERE  ORDER_M.ORDER_ID = ORDER_D.ORDER_ID(+) AND ORDER_M.STORE_NO = STORE.STORE_NO(+) ");
            sb.Append(" AND ORDER_ITEMS_ID = " + OracleDBUtil.SqlStr(SID));

            DataTable dt = OracleDBUtil.Query_Data(sb.ToString());
            return dt;

        }

        /// <summary>
        /// 依照UUID取得該搭贈商品的主商品資訊
        /// </summary>
        /// <param name="SID">ORDER_ITEMS_ID</param>
        /// <returns></returns>
        public static DataTable GetOneORDER_ITEMS_ID(string SID)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(@"select order_id, prodno_m, gift_falg, ORDER_ITEMS_ID from order_d   
                            where order_id || '^' || prodno_m in  
                                (select order_id || '^' || prodno from order_d where ORDER_ITEMS_ID = " + OracleDBUtil.SqlStr(SID) + @" and gift_falg = '0') 
                            and gift_falg = '1' ");

            DataTable dt = OracleDBUtil.Query_Data(sb.ToString());
            return dt;
        }

        /// <summary>
        /// 依照門市訂貨單UUID和主商品的ProdNo取得該商品的搭贈商品資訊
        /// </summary>
        /// <param name="ORDER_ID">門市訂貨單UUID</param>
        /// <param name="PRODNO">主商品的ProdNo</param>
        /// <returns></returns>
        public static DataTable GetOneORDER_ITEMS_ID(string ORDER_ID, string PRODNO)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(@"select ORDER_ID, PRODNO, GIFT_FALG, ORDER_ITEMS_ID 
                            from ORDER_D 
                            where GIFT_FALG = '1' 
                            and PRODNO_M = " + OracleDBUtil.SqlStr(PRODNO) + @" 
                            and ORDER_ID=" + OracleDBUtil.SqlStr(ORDER_ID));

            DataTable dt = OracleDBUtil.Query_Data(sb.ToString());
            return dt;
        }

    }
}
