using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.OracleClient;
using Advtek.Utility;
using FET.POS.Model.DTO;
using FET.POS.Model.Common;

namespace FET.POS.Model.Facade.FacadeImpl
{
    public class HappyGo_Facade
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="posuuid_master"></param>
        /// <param name="trans"></param>
        /// <returns>累點料號,累點點數</returns>
        public List<string> get_hg_award_point(string posuuid_master, OracleTransaction trans)
        {
            List<string> list = new List<string>();
            double result = 0;
            OracleConnection conn = null;
            StringBuilder sb = new StringBuilder();

            conn = trans.Connection;
            //抓出金額
            string sqlstr = "select sum(total_amount) from SALE_DETAIL where item_type in(1,2,3) and prodno not in (select distinct prodno from hg_accu_exclude_prod) and posuuid_master = " + OracleDBUtil.SqlStr(posuuid_master);
            OracleCommand cmd = new OracleCommand(sqlstr, conn, trans);
            double total_amount = cmd.ExecuteScalar() == null ? 0 : Convert.ToDouble(cmd.ExecuteScalar());
            if (total_amount != 0)
            {
                sqlstr = "select ACCU_CURRENCY,DIVIDABLE_POINT,ACCU_NO from HG_ACCUMULATE where sysdate between S_DATE and E_DATE";
                cmd = new OracleCommand(sqlstr, conn, trans);
                double accu_currency = 0;
                double dividable_point = 0;
                OracleDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    accu_currency = dr.GetDouble(0);
                    dividable_point = dr.GetDouble(0);
                    list.Add(dr.GetString(2));
                }
                else
                {
                    list.Add("");
                }
                dr.Close();

                if (accu_currency != 0 && dividable_point != 0)
                {
                    double d = Math.Floor((total_amount / accu_currency));
                    result = d * dividable_point;

                }

                list.Add(result.ToString());
            }



            

            return list;
        }
    }


}
