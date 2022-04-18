using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.OracleClient;
using System.Data;
using Advtek.Utility;
namespace FET.POS.Model.Facade.FacadeImpl
{
    public class Discount_Facade
    {
        public struct Discount_Conditions
        {
            public string discount_type;
            public string prodno;
            public string data;
            public string voice;
            public string r_rate;
            public string msisdn;
            public string promotion_code;
            public string trans_type;
            public string mnp;
            public string bundle_type;
            public string employee_id;
            public string sys_id;
            public string store_id;
            public string uni_price;
            public string modi_user;
            public string posuuid_detail;
            public string total_amount;
        }

        public static DataTable get_add_in_prod_discount_test(string prodno, string store_no, string employee_id, string posuuid_detail)
        {
            DataTable list = new DataTable();
            try
            {
                Hashtable table = get_add_in_prod_discount(prodno, store_no, employee_id, posuuid_detail);
                bool first = true;
                foreach (DictionaryEntry de in table)
                {
                    DataTable dt = de.Value as DataTable;
                    if (first)
                    {
                        if (dt != null && dt.Rows.Count > 0)
                        {
                            list = de.Value as DataTable;
                            first = false;
                        }
                    }
                    else
                    {

                        if (dt != null && dt.Rows.Count > 0)
                        {
                            list.Merge(de.Value as DataTable);
                        }
                    }

                }
            }
            catch
            {
            }
            return list;
        }

        public static Hashtable get_add_in_prod_discount(string prodno, string store_no, string employee_id, string posuuid_detail)
        {
            string sqlstr = "";
            OracleCommand cmd = null;
            OracleDataReader dr = null;
            OracleDataAdapter da = null;
            DataTable itemDt = new DataTable();
            Discount_Conditions conditions = new Discount_Conditions();
            conditions.discount_type = "7";
            Hashtable list = new Hashtable();
            OracleConnection conn = null;
            try
            {
                conn = OracleDBUtil.GetConnection();

                if (!string.IsNullOrEmpty(posuuid_detail))
                {
                    #region 折扣
                    sqlstr = "select * from TO_CLOSE_HEAD where posuuid_detail = " + OracleDBUtil.SqlStr(posuuid_detail);
                    cmd = new OracleCommand(sqlstr, conn);
                    dr = cmd.ExecuteReader();
                    if (dr.Read())
                    {
                        conditions.posuuid_detail = posuuid_detail;
                        conditions.r_rate = dr["r_rate"].ToString();
                        conditions.data = dr["data"].ToString();
                        conditions.voice = dr["voice"].ToString();
                        conditions.trans_type = dr["trans_type"].ToString();
                        conditions.mnp = dr["mnp"].ToString();
                        conditions.bundle_type = dr["BUNDLE_TYPE"].ToString();
                        conditions.employee_id = dr["MODI_USER"].ToString();
                        conditions.sys_id = dr["SERVICE_TYPE"].ToString();
                        conditions.store_id = dr["store_no"].ToString();
                    }
                    dr.Close();

                    sqlstr = "select * from TO_CLOSE_ITEM where posuuid_detail = " + OracleDBUtil.SqlStr(posuuid_detail);
                    cmd = new OracleCommand(sqlstr, conn);
                    da = new OracleDataAdapter(cmd);
                    itemDt = new DataTable();
                    da.Fill(itemDt);

                    foreach (DataRow row in itemDt.Rows)
                    {
                        conditions.promotion_code = row["PROMOTION_CODE"].ToString();
                        conditions.msisdn = row["MSISDN"].ToString();
                        conditions.prodno = row["prodno"].ToString();

                        //抓出折扣
                        DataTable dt = get_discount_by_type(conditions);

                        //利用discount_master_id 抓出商品
                        DataTable add_prod_dt = new DataTable();
                        sqlstr = "select p.PRODNO,p.PRODNAME,ap.UNIT_PRICE as UNI_PRICE,(ap.UNIT_PRICE - ap.DIS_AMT) as SALE_PRICE,'7' as ITEM_TYPE from ADD_IN_PROD_DISCOUNT ap join PRODUCT p on ap.prodno=p.prodno where ap.discount_master_id = :discount_master_id";
                        cmd = new OracleCommand(sqlstr, conn);
                        cmd.Parameters.Add(":discount_master_id", OracleType.VarChar);
                        foreach (DataRow dRow in dt.Rows)
                        {
                            string discount_master_id = dRow["discount_master_id"].ToString();
                            string discount_code = dRow["discount_code"].ToString();
                            if (!list.ContainsKey(discount_master_id))
                            {
                                cmd.Parameters[":discount_master_id"].Value = discount_master_id;
                                da = new OracleDataAdapter(cmd);
                                da.Fill(add_prod_dt);
                                if (add_prod_dt.Rows.Count > 0)
                                {
                                    list.Add(discount_code, add_prod_dt);
                                }
                            }
                        }
                    }

                    #endregion
                }
                else
                {
                    #region 單品
                    conditions.promotion_code = "";
                    conditions.msisdn = "";
                    conditions.employee_id = employee_id;
                    conditions.store_id = store_no;
                    conditions.prodno = prodno;
                    conditions.posuuid_detail = "";
                    conditions.r_rate = "";
                    conditions.data = "";
                    conditions.voice = "";
                    conditions.trans_type = "";
                    conditions.mnp = "";
                    conditions.bundle_type = "";

                    conditions.sys_id = "";

                    //抓出折扣
                    DataTable dt = get_discount_by_type(conditions);

                    //利用discount_master_id 抓出商品
                    DataTable add_prod_dt = new DataTable();
                    sqlstr = "select p.PRODNO,p.PRODNAME,ap.UNIT_PRICE as UNI_PRICE,(ap.UNIT_PRICE - ap.DIS_AMT) as SALE_PRICE,'7' as ITEM_TYPE from ADD_IN_PROD_DISCOUNT ap join PRODUCT p on ap.prodno=p.prodno where ap.discount_master_id = :discount_master_id";
                    cmd = new OracleCommand(sqlstr, conn);
                    cmd.Parameters.Add(":discount_master_id", OracleType.VarChar);
                    foreach (DataRow dRow in dt.Rows)
                    {
                        string discount_master_id = dRow["discount_master_id"].ToString();
                        string discount_code = dRow["discount_code"].ToString();
                        if (!list.ContainsKey(discount_master_id))
                        {
                            cmd.Parameters[":discount_master_id"].Value = discount_master_id;
                            da = new OracleDataAdapter(cmd);
                            da.Fill(add_prod_dt);
                            if (add_prod_dt.Rows.Count > 0)
                            {
                                list.Add(discount_code, add_prod_dt);
                            }
                        }
                    }
                    #endregion
                }
                //抓取商品
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (conn.State == ConnectionState.Open) conn.Close();
                OracleConnection.ClearPool(conn);
            }

            return list;
        }


        public static Hashtable get_gift_discount(string prodno, string store_no, string employee_id, string posuuid_detail)
        {
            string sqlstr = "";
            OracleCommand cmd = null;
            OracleDataReader dr = null;
            OracleDataAdapter da = null;
            DataTable itemDt = new DataTable();
            Discount_Conditions conditions = new Discount_Conditions();
            conditions.discount_type = "6";
            Hashtable list = new Hashtable();
            OracleConnection conn = null;
            try
            {
                conn = OracleDBUtil.GetConnection();

                if (!string.IsNullOrEmpty(posuuid_detail))
                {
                    #region 折扣
                    sqlstr = "select * from TO_CLOSE_HEAD where posuuid_detail = " + OracleDBUtil.SqlStr(posuuid_detail);
                    cmd = new OracleCommand(sqlstr, conn);
                    dr = cmd.ExecuteReader();
                    if (dr.Read())
                    {
                        conditions.posuuid_detail = posuuid_detail;
                        conditions.r_rate = dr["r_rate"].ToString();
                        conditions.data = dr["data"].ToString();
                        conditions.voice = dr["voice"].ToString();
                        conditions.trans_type = dr["trans_type"].ToString();
                        conditions.mnp = dr["mnp"].ToString();
                        conditions.bundle_type = dr["BUNDLE_TYPE"].ToString();
                        conditions.employee_id = dr["MODI_USER"].ToString();
                        conditions.sys_id = dr["SERVICE_TYPE"].ToString();
                        conditions.store_id = dr["store_no"].ToString();
                    }
                    dr.Close();

                    sqlstr = "select * from TO_CLOSE_ITEM where posuuid_detail = " + OracleDBUtil.SqlStr(posuuid_detail);
                    cmd = new OracleCommand(sqlstr, conn);
                    da = new OracleDataAdapter(cmd);
                    itemDt = new DataTable();
                    da.Fill(itemDt);

                    foreach (DataRow row in itemDt.Rows)
                    {
                        conditions.promotion_code = row["PROMOTION_CODE"].ToString();
                        conditions.msisdn = row["MSISDN"].ToString();
                        conditions.prodno = row["prodno"].ToString();

                        //抓出折扣
                        DataTable dt = get_discount_by_type(conditions);

                        //利用discount_master_id 抓出贈品
                        DataTable add_prod_dt = new DataTable();
                        sqlstr = "select p.PRODNO,p.PRODNAME,'0' as UNI_PRICE,'0' as SALE_PRICE,'13' as ITEM_TYPE from GIFT_DISCOUNT gd join PRODUCT p on gd.prodno=p.prodno where gd.discount_master_id = :discount_master_id";
                        cmd = new OracleCommand(sqlstr, conn);
                        cmd.Parameters.Add(":discount_master_id", OracleType.VarChar);
                        foreach (DataRow dRow in dt.Rows)
                        {
                            string discount_master_id = dRow["discount_master_id"].ToString();
                            string discount_code = dRow["discount_code"].ToString();
                            if (!list.ContainsKey(discount_master_id))
                            {
                                cmd.Parameters[":discount_master_id"].Value = discount_master_id;
                                da = new OracleDataAdapter(cmd);
                                da.Fill(add_prod_dt);
                                if (add_prod_dt.Rows.Count > 0)
                                {
                                    list.Add(discount_code, add_prod_dt);
                                }
                            }
                        }
                    }

                    #endregion
                }
                else
                {
                    #region 單品
                    conditions.promotion_code = "";
                    conditions.msisdn = "";
                    conditions.employee_id = employee_id;
                    conditions.store_id = store_no;
                    conditions.prodno = prodno;
                    conditions.posuuid_detail = "";
                    conditions.r_rate = "";
                    conditions.data = "";
                    conditions.voice = "";
                    conditions.trans_type = "";
                    conditions.mnp = "";
                    conditions.bundle_type = "";

                    conditions.sys_id = "";

                    //抓出折扣
                    DataTable dt = get_discount_by_type(conditions);

                    //利用discount_master_id 抓出商品
                    DataTable add_prod_dt = new DataTable();
                    sqlstr = "select p.PRODNO,p.PRODNAME,'0' as UNI_PRICE,'0' as SALE_PRICE,'13' as ITEM_TYPE from GIFT_DISCOUNT gd join PRODUCT p on gd.prodno=p.prodno where gd.discount_master_id = :discount_master_id";
                    cmd = new OracleCommand(sqlstr, conn);
                    cmd.Parameters.Add(":discount_master_id", OracleType.VarChar);
                    foreach (DataRow dRow in dt.Rows)
                    {
                        string discount_master_id = dRow["discount_master_id"].ToString();
                        string discount_code = dRow["discount_code"].ToString();
                        if (!list.ContainsKey(discount_master_id))
                        {
                            cmd.Parameters[":discount_master_id"].Value = discount_master_id;
                            da = new OracleDataAdapter(cmd);
                            da.Fill(add_prod_dt);
                            if (add_prod_dt.Rows.Count > 0)
                            {
                                list.Add(discount_code, add_prod_dt);
                            }
                        }
                    }
                    #endregion
                }
                //抓取商品
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (conn.State == ConnectionState.Open) conn.Close();
                OracleConnection.ClearPool(conn);
            }

            return list;
        }


        public static DataTable get_discount_by_type(Discount_Conditions conditions)
        {
            DataTable discountDt = new DataTable();
            OracleConnection conn = null;
            try
            {
                string sp_name = "";
                conn = OracleDBUtil.GetConnection();
                if (conditions.discount_type == "7")
                {
                    sp_name = "sp_query_add_in_prod";
                }
                else
                {
                    sp_name = "sp_query_Gift";
                }

                OracleCommand discountCmd = new OracleCommand(sp_name, conn);
                discountCmd.CommandType = CommandType.StoredProcedure;
          
                discountCmd.Parameters.Add("v_msisdn", OracleType.NVarChar).Value = conditions.msisdn;
                discountCmd.Parameters.Add("v_r_rate", OracleType.NVarChar).Value = conditions.r_rate;
                discountCmd.Parameters.Add("v_promotion_code", OracleType.NVarChar).Value = conditions.promotion_code;
                discountCmd.Parameters.Add("v_item_code", OracleType.NVarChar).Value = conditions.prodno;
                discountCmd.Parameters.Add("v_data", OracleType.NVarChar).Value = conditions.data;
                discountCmd.Parameters.Add("v_voice", OracleType.NVarChar).Value = conditions.voice;
                discountCmd.Parameters.Add("v_trans_type", OracleType.NVarChar).Value = conditions.trans_type;
                discountCmd.Parameters.Add("v_mnp", OracleType.NVarChar).Value = conditions.mnp;
                discountCmd.Parameters.Add("v_bundle_type", OracleType.NVarChar).Value = conditions.bundle_type;
                discountCmd.Parameters.Add("v_employee_id", OracleType.NVarChar).Value = conditions.employee_id;
                discountCmd.Parameters.Add("v_sys_id", OracleType.NVarChar).Value = conditions.sys_id;
                discountCmd.Parameters.Add("v_store_id", OracleType.NVarChar).Value = conditions.store_id;
                discountCmd.Parameters.Add("v_msgcode", OracleType.VarChar, 4000).Direction = ParameterDirection.Output;
                discountCmd.Parameters.Add("v_message", OracleType.VarChar, 4000).Direction = ParameterDirection.Output;
                discountCmd.Parameters.Add("o_data", OracleType.Cursor).Direction = ParameterDirection.Output;
                discountCmd.ExecuteNonQuery();
                string v_msgcode = discountCmd.Parameters["v_msgcode"].Value.ToString();
                OracleDataReader dr = (OracleDataReader)discountCmd.Parameters["o_data"].Value;

                discountDt.Columns.Add("discount_master_id");
                discountDt.Columns.Add("discount_code");
                discountDt.Columns.Add("discount_name");
                discountDt.Columns.Add("DISCOUNT_MONEY");
                discountDt.Columns.Add("DISCOUNT_RATE");

                discountDt.Columns.Add("S_DATE");
                discountDt.Columns.Add("E_DATE");
                while (dr.Read())
                {
                    string discount_master_id = dr.IsDBNull(0) ? "" : dr[0].ToString();
                    if (Discount_Facade.check_store_discount_count(discount_master_id, conditions.store_id))
                    {
                        DataRow row = discountDt.NewRow();

                        row["discount_master_id"] = discount_master_id;
                        row["discount_code"] = dr.IsDBNull(1) ? "" : dr[1].ToString();
                        row["discount_name"] = dr.IsDBNull(2) ? "" : dr[2].ToString();
                        row["DISCOUNT_MONEY"] = dr.IsDBNull(3) ? "0" : dr[3].ToString();
                        row["DISCOUNT_RATE"] = dr.IsDBNull(4) ? "0" : dr[4].ToString();
                        row["S_DATE"] = dr.IsDBNull(5) ? "" : dr[5].ToString();
                        row["E_DATE"] = dr.IsDBNull(6) ? "" : dr[6].ToString();
                        discountDt.Rows.Add(row);
                    }
                }
                dr.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (conn.State == ConnectionState.Open) conn.Close();
                OracleConnection.ClearPool(conn);
            }
            return discountDt;
        }

        public static bool check_store_discount_count(string discount_master_id, string store_no)
        {
            bool result = true;
            OracleConnection conn = null;
            string sqlstr = string.Format("select NVL(DIS_USE_COUNT,0),NVL(DIS_CONSUME_COUNT,0) from store_discount where discount_master_id={0} and store_no = {1}", OracleDBUtil.SqlStr(discount_master_id), store_no);

            try
            {

                conn = OracleDBUtil.GetConnection();
                OracleCommand cmd = new OracleCommand(sqlstr, conn);
                OracleDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    decimal DIS_USE_COUNT = dr.GetDecimal(0);
                    decimal DIS_CONSUME_COUNT = dr.GetDecimal(1);
                    if (DIS_USE_COUNT != 0)
                    {
                        if (DIS_CONSUME_COUNT >= DIS_USE_COUNT)
                        {
                            result = false;
                        }
                    }
                }
                dr.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (conn.State == ConnectionState.Open) conn.Close();
                OracleConnection.ClearPool(conn);
            }

            return result;
        }

        public static void CreateDiscount(string posuuid_detail)
        {
            CreateDiscount(posuuid_detail, 1);
        }

        public static void CreateDiscount(string posuuid_detail,int quantity)
        {
            //判斷是否TO_CLOSE_DISCOUNT
            string sqlstr = "select posuuid_detail from TO_CLOSE_DISCOUNT where posuuid_detail = " + OracleDBUtil.SqlStr(posuuid_detail);
            DataTable dtt = OracleDBUtil.Query_Data(sqlstr);
            if (dtt.Rows.Count > 0)
                return;

            Discount_Conditions conditions = new Discount_Conditions();
            OracleConnection conn = null;
            OracleTransaction trans = null;
            string fun_id = "";
            bool hasRow = false;
            try
            {
                conn = OracleDBUtil.GetConnection();
                trans = conn.BeginTransaction();

                sqlstr = "select * from TO_CLOSE_HEAD where posuuid_detail = " + OracleDBUtil.SqlStr(posuuid_detail);
                OracleCommand cmd = new OracleCommand(sqlstr, conn, trans);
                OracleDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    conditions.posuuid_detail = posuuid_detail;
                    conditions.r_rate = dr["r_rate"].ToString();
                    conditions.data = dr["data"].ToString();
                    conditions.voice = dr["voice"].ToString();
                    conditions.trans_type = dr["trans_type"].ToString();
                    conditions.mnp = dr["mnp"].ToString();
                    conditions.bundle_type = dr["BUNDLE_TYPE"].ToString();
                    conditions.employee_id = dr["MODI_USER"].ToString();
                    conditions.sys_id = dr["SERVICE_TYPE"].ToString();
                    conditions.store_id = dr["store_no"].ToString();
                    fun_id = dr["fun_id"].ToString();
                    conditions.total_amount = get_total_amount(posuuid_detail);
                    hasRow = true;
                }
                dr.Close();

                if (!hasRow)
                {
                    trans.Commit();
                    return;
                }

                //排除
                if (conditions.sys_id == "4" && (fun_id == "180" || fun_id == "150" || fun_id == "11"))
                {
                    trans.Commit();
                    return;
                }

                sqlstr = "select * from TO_CLOSE_ITEM where posuuid_detail = " + OracleDBUtil.SqlStr(posuuid_detail);
                cmd = new OracleCommand(sqlstr, conn, trans);
                OracleDataAdapter da = new OracleDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                int i = 1;
                conditions.prodno = "";
                conditions.promotion_code = "";
                conditions.msisdn = "";
                foreach (DataRow row in dt.Rows)
                {
                    if (string.IsNullOrEmpty(conditions.promotion_code))
                    {
                        conditions.promotion_code = row["PROMOTION_CODE"].ToString();
                    }

                    if (string.IsNullOrEmpty(conditions.prodno))
                    {
                        conditions.msisdn = StringUtil.CStr(row["MSISDN"]);
                    }
                    conditions.prodno += row["prodno"].ToString() + "|";


                }
                conditions.prodno = conditions.prodno.TrimEnd('|');
                //新增折扣
                InsertToCloseDiscount(conditions, trans, ref i);

                trans.Commit();
            }
            catch (Exception ex)
            {
                trans.Rollback();
                throw ex;
            }
            finally
            {
                if (conn.State == ConnectionState.Open) conn.Close();
                OracleConnection.ClearPool(conn);
            }
        }

        public static string get_total_amount(string posuuid_detail)
        {
            string result = "0";
            OracleConnection conn = null;
            string sqlstr = "select sum(amount) from to_close_item where prodno not in(select GUARANTEE_PRODNO from GUARANTEE_PROD_MAPPING ) and posuuid_detail = " + OracleDBUtil.SqlStr(posuuid_detail);
            try
            {
                conn = OracleDBUtil.GetConnection();
                OracleCommand cmd = new OracleCommand(sqlstr, conn);
                OracleDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    result = StringUtil.CStr(dr[0]);
                }
                dr.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (conn.State == ConnectionState.Open) conn.Close();
                OracleConnection.ClearPool(conn);
            }

            return result;
        }

        public static DataTable get_Discount(Discount_Conditions conditions, int quantity)
        {
            DataTable discountDt = new DataTable();
            OracleConnection conn = null;
            try
            {
                conn = OracleDBUtil.GetConnection();
                #region 讀取Discount Master
                OracleCommand discountCmd = new OracleCommand("sp_query_discount_ws");
                discountCmd.CommandType = CommandType.StoredProcedure;
                discountCmd.Connection = conn;
                if (conditions.msisdn != null)
                    discountCmd.Parameters.Add("v_msisdn", OracleType.NVarChar).Value = conditions.msisdn;
                else
                    discountCmd.Parameters.Add("v_msisdn", OracleType.NVarChar).Value = DBNull.Value;

                if (conditions.r_rate != null)
                    discountCmd.Parameters.Add("v_r_rate", OracleType.NVarChar).Value = conditions.r_rate;
                else
                    discountCmd.Parameters.Add("v_r_rate", OracleType.NVarChar).Value = DBNull.Value;

                if (conditions.promotion_code != null)
                    discountCmd.Parameters.Add("v_promotion_code", OracleType.NVarChar).Value = conditions.promotion_code;
                else
                    discountCmd.Parameters.Add("v_promotion_code", OracleType.NVarChar).Value = DBNull.Value;

                if (conditions.prodno != null)
                    discountCmd.Parameters.Add("v_item_code", OracleType.NVarChar).Value = conditions.prodno;
                else
                    discountCmd.Parameters.Add("v_item_code", OracleType.NVarChar).Value = DBNull.Value;

                if (conditions.data != null)
                    discountCmd.Parameters.Add("v_data", OracleType.NVarChar).Value = conditions.data;
                else
                    discountCmd.Parameters.Add("v_data", OracleType.NVarChar).Value = DBNull.Value;

                if (conditions.voice != null)
                    discountCmd.Parameters.Add("v_voice", OracleType.NVarChar).Value = conditions.voice;
                else
                    discountCmd.Parameters.Add("v_voice", OracleType.NVarChar).Value = DBNull.Value;

                if (conditions.trans_type != null)
                    discountCmd.Parameters.Add("v_trans_type", OracleType.NVarChar).Value = conditions.trans_type;
                else
                    discountCmd.Parameters.Add("v_trans_type", OracleType.NVarChar).Value = DBNull.Value;

                if (conditions.mnp != null)
                    discountCmd.Parameters.Add("v_mnp", OracleType.NVarChar).Value = conditions.mnp;
                else
                    discountCmd.Parameters.Add("v_mnp", OracleType.NVarChar).Value = DBNull.Value;

                if (conditions.bundle_type != null)
                    discountCmd.Parameters.Add("v_bundle_type", OracleType.NVarChar).Value = conditions.bundle_type;
                else
                    discountCmd.Parameters.Add("v_bundle_type", OracleType.NVarChar).Value = DBNull.Value;

                if (conditions.employee_id != null)
                    discountCmd.Parameters.Add("v_employee_id", OracleType.NVarChar).Value = conditions.employee_id;
                else
                    discountCmd.Parameters.Add("v_employee_id", OracleType.NVarChar).Value = DBNull.Value;

                if (conditions.sys_id != null)
                    discountCmd.Parameters.Add("v_sys_id", OracleType.NVarChar).Value = conditions.sys_id;
                else
                    discountCmd.Parameters.Add("v_sys_id", OracleType.NVarChar).Value = DBNull.Value;

                if (conditions.store_id != null)
                    discountCmd.Parameters.Add("v_store_id", OracleType.NVarChar).Value = conditions.store_id;
                else
                    discountCmd.Parameters.Add("v_store_id", OracleType.NVarChar).Value = DBNull.Value;


                discountCmd.Parameters.Add("o_data", OracleType.Cursor).Direction = ParameterDirection.Output;
                discountCmd.Parameters.Add("v_msgcode", OracleType.VarChar, 4000).Direction = ParameterDirection.Output;
                discountCmd.Parameters.Add("v_message", OracleType.VarChar, 4000).Direction = ParameterDirection.Output;
                discountCmd.ExecuteNonQuery();
                string v_msgcode = discountCmd.Parameters["v_msgcode"].Value.ToString();
                string v_message = discountCmd.Parameters["v_message"].Value.ToString();
                OracleDataReader dr = (OracleDataReader)discountCmd.Parameters["o_data"].Value;

                discountDt.Columns.Add("discount_master_id");
                discountDt.Columns.Add("discount_code");
                discountDt.Columns.Add("discount_id");
                discountDt.Columns.Add("discount_name");
                discountDt.Columns.Add("DISCOUNT_MONEY");
                discountDt.Columns.Add("DISCOUNT_RATE");
                discountDt.Columns.Add("DISCOUNT_PRICE");
                discountDt.Columns.Add("DISCOUNT_AMOUNT");
                discountDt.Columns.Add("DISCOUNT_B_DATE");
                discountDt.Columns.Add("quantity");
                discountDt.Columns.Add("S_DATE");
                discountDt.Columns.Add("POSUUID_DETAIL");
                discountDt.Columns.Add("DISCOUNT_E_DATE");
                discountDt.Columns.Add("E_DATE");

                double discount_total_amount = 0;
                if (v_msgcode == "000")
                {
                    while (dr.Read())
                    {
                        string discount_master_id = dr.IsDBNull(0) ? "" : dr[0].ToString();
                        if (Discount_Facade.check_store_discount_count(discount_master_id, conditions.store_id))
                        {
                            DataRow row = discountDt.NewRow();
                            row["discount_master_id"] = discount_master_id;
                            row["discount_code"] = dr.IsDBNull(1) ? "" : dr[1].ToString();
                            row["discount_id"] = dr.IsDBNull(1) ? "" : dr[1].ToString();
                            row["discount_name"] = dr.IsDBNull(2) ? "" : dr[2].ToString();
                            row["DISCOUNT_MONEY"] = dr.IsDBNull(3) ? "" : dr[3].ToString();
                            row["DISCOUNT_RATE"] = dr.IsDBNull(4) ? "" : dr[4].ToString();
                            row["quantity"] = quantity;
                            if (!string.IsNullOrEmpty(StringUtil.CStr(row["DISCOUNT_MONEY"])))
                            {
                                double price = Convert.ToDouble(row["DISCOUNT_MONEY"]);
                                row["DISCOUNT_PRICE"] = price;
                                row["DISCOUNT_AMOUNT"] = price * quantity;
                                discount_total_amount += Convert.ToDouble(row["DISCOUNT_AMOUNT"]);
                            }
                            else
                            {
                                double rate = Convert.ToDouble(row["DISCOUNT_RATE"]) * 0.01;
                                double t_amount = Convert.ToDouble(conditions.uni_price);
                                row["DISCOUNT_PRICE"] = t_amount * rate;
                                row["DISCOUNT_AMOUNT"] = (t_amount * rate) * quantity;
                                discount_total_amount += Convert.ToDouble(row["DISCOUNT_AMOUNT"]);
                            }


                            row["DISCOUNT_B_DATE"] = dr.IsDBNull(5) ? "" : dr[5].ToString();
                            row["S_DATE"] = dr.IsDBNull(5) ? "" : dr[5].ToString();
                            row["E_DATE"] = dr.IsDBNull(6) ? "" : dr[6].ToString();
                            row["DISCOUNT_E_DATE"] = dr.IsDBNull(6) ? "" : dr[6].ToString();
                            row["POSUUID_DETAIL"] = conditions.posuuid_detail;
                            discountDt.Rows.Add(row);
                        }
                    }
                    dr.Close();

                    //平衡價格
                    double total_amount = String.IsNullOrEmpty(conditions.total_amount) ? 0 : Convert.ToDouble(conditions.total_amount);
                    
                    if (discount_total_amount < 0 && Math.Abs(discount_total_amount) > total_amount)
                    {
                     
                        double temp_amount = total_amount + discount_total_amount;
                        foreach (DataRow row in discountDt.Rows)
                        {
                            if (temp_amount != 0)
                            {
                                double discount_amount = Convert.ToDouble(row["DISCOUNT_PRICE"]);
                                if (discount_amount > temp_amount)
                                {
                                    temp_amount -= discount_amount;
                                    row.BeginEdit();
                                    row["DISCOUNT_PRICE"] = 0;
                                    row["DISCOUNT_AMOUNT"] = 0;
                                    row.EndEdit();

                                }
                                else
                                {
                                    row.BeginEdit();
                                    row["DISCOUNT_PRICE"] = (temp_amount - discount_amount) * -1;
                                    row["DISCOUNT_AMOUNT"] = (temp_amount - discount_amount) * -1;
                                    temp_amount = 0;
                                    row.EndEdit();
                                }
                                row.AcceptChanges();
                            }
                        }
                        discountDt.AcceptChanges();
                    }

                }
                else
                {
                    throw new Exception(v_message);
                }
                #endregion
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (conn.State == ConnectionState.Open) conn.Close();
                OracleConnection.ClearPool(conn);
            }
            return discountDt;
        }

        public static void InsertToCloseDiscount(Discount_Conditions conditions, OracleTransaction TransPOS, ref int i)
        {

            InsertToCloseDiscount(conditions, TransPOS, ref i, 1);

        }

        public static void InsertToCloseDiscount(Discount_Conditions conditions, OracleTransaction TransPOS, ref int i,int quantity)
        {

          
            DataTable discountDt = get_Discount(conditions, quantity);


            #region Insert To_Close_Discount
            StringBuilder sb_item = new StringBuilder();
            sb_item.Append(" insert into TO_CLOSE_DISCOUNT (");
            sb_item.Append(" ID,");
            sb_item.Append(" SEQNO,");
            sb_item.Append(" DISCOUNT_ID,");
            sb_item.Append(" DISCOUNT_PRICE,");
            sb_item.Append(" DISCOUNT_AMOUNT,");
            sb_item.Append(" DISCOUNT_B_DATE,");
            sb_item.Append(" CREATE_USER,");
            sb_item.Append(" CREATE_DTM,");
            sb_item.Append(" MODI_USER,");
            sb_item.Append(" MODI_DTM,");
            sb_item.Append(" POSUUID_DETAIL,");
            sb_item.Append(" DISCOUNT_E_DATE ");
            sb_item.Append(" )VALUES(");
            sb_item.Append(" POS_UUID(),");
            sb_item.Append(" :SEQNO,");
            sb_item.Append(" :DISCOUNT_ID,");
            sb_item.Append(" :DISCOUNT_PRICE,");
            sb_item.Append(" :DISCOUNT_AMOUNT,");
            sb_item.Append(" :DISCOUNT_B_DATE,");
            sb_item.Append(" :CREATE_USER,");
            sb_item.Append(" SYSDATE,");
            sb_item.Append(" :MODI_USER,");
            sb_item.Append(" SYSDATE,");
            sb_item.Append(" :POSUUID_DETAIL,");
            sb_item.Append(" :DISCOUNT_E_DATE");
            sb_item.Append(")");

            OracleCommand cmd = new OracleCommand(sb_item.ToString(), TransPOS.Connection, TransPOS);
            cmd.Parameters.Add(":SEQNO", OracleType.VarChar, 32);
            cmd.Parameters.Add(":DISCOUNT_ID", OracleType.VarChar, 50);
            cmd.Parameters.Add(":DISCOUNT_PRICE", OracleType.Number);
            cmd.Parameters.Add(":DISCOUNT_AMOUNT", OracleType.Number);
            cmd.Parameters.Add(":DISCOUNT_B_DATE", OracleType.DateTime);
            cmd.Parameters.Add(":CREATE_USER", OracleType.VarChar, 50).Value = conditions.employee_id;
            cmd.Parameters.Add(":DISCOUNT_E_DATE", OracleType.DateTime);
            cmd.Parameters.Add(":MODI_USER", OracleType.VarChar, 50).Value = conditions.employee_id;
            cmd.Parameters.Add(":POSUUID_DETAIL", OracleType.VarChar, 32).Value = conditions.posuuid_detail;



            foreach (DataRow row in discountDt.Rows)
            {
                int qty = Convert.ToInt32(row["quantity"]);
                cmd.Parameters[":SEQNO"].Value = i++;
                cmd.Parameters[":DISCOUNT_ID"].Value = row["discount_code"].ToString();

                cmd.Parameters[":DISCOUNT_PRICE"].Value = row["DISCOUNT_PRICE"].ToString();
                cmd.Parameters[":DISCOUNT_AMOUNT"].Value = row["DISCOUNT_AMOUNT"].ToString();
             


                if (row.IsNull("S_DATE") || string.IsNullOrEmpty(row["S_DATE"].ToString()))
                {
                    cmd.Parameters[":DISCOUNT_B_DATE"].Value = DBNull.Value;
                }
                else
                {
                    cmd.Parameters[":DISCOUNT_B_DATE"].Value = row["S_DATE"];
                }
                if (row.IsNull("E_DATE") || string.IsNullOrEmpty(row["E_DATE"].ToString()))
                {
                    cmd.Parameters[":DISCOUNT_E_DATE"].Value = DBNull.Value;
                }
                else
                {
                    cmd.Parameters[":DISCOUNT_E_DATE"].Value = row["E_DATE"];
                }
                cmd.ExecuteNonQuery();
            }
            #endregion

        }


        public static bool CheckCreateDiscount(string PRODNO)
        {
            bool result = false;
            //9碼料號才需要產生折扣
            if (PRODNO.Length != 9)
            {
                return result;
            }

            OracleConnection conn = null;
            string sqlstr = "select prodno from product where (is_discount='Y' or  isstock = '0') and prodno = "+ OracleDBUtil.SqlStr(PRODNO);
            try
            {
                conn = OracleDBUtil.GetConnection();
                OracleCommand cmd = new OracleCommand(sqlstr, conn);
                OracleDataReader dr = cmd.ExecuteReader();
                result = !dr.HasRows;
                dr.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (conn.State == ConnectionState.Open) conn.Close();
                OracleConnection.ClearPool(conn);
            }

            return result;
        }

        public static int get_add_prod_dis_amt(string discount_code, string prodno)
        {
            int disAmt = 0;
            OracleConnection conn = null;
            string sqlstr = @"select dis_amt from add_in_prod_discount 
                                where discount_master_id = (select discount_master_id from discount_master where discount_code = " 
                             + OracleDBUtil.SqlStr(discount_code) + ") and prodno = " + OracleDBUtil.SqlStr(prodno);
            try
            {
                conn = OracleDBUtil.GetConnection();
                OracleCommand cmd = new OracleCommand(sqlstr, conn);
                OracleDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    if (dr[0] != null && StringUtil.CStr(dr[0]) != "" && NumberUtil.IsNumeric(StringUtil.CStr(dr[0])))
                        disAmt = int.Parse(StringUtil.CStr(dr[0]));
                }
                dr.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (conn.State == ConnectionState.Open) conn.Close();
                OracleConnection.ClearPool(conn);
            }

            return disAmt;
        }

        public static int get_gift_dis_amt(string discount_code, string prodno)
        {
            int disAmt = 0;
            OracleConnection conn = null;
            string sqlstr = @"select amt from gift_discount  
                                where discount_master_id = (select discount_master_id from discount_master where discount_code = " 
                             + OracleDBUtil.SqlStr(discount_code) + ") and prodno = " + OracleDBUtil.SqlStr(prodno);
            try
            {
                conn = OracleDBUtil.GetConnection();
                OracleCommand cmd = new OracleCommand(sqlstr, conn);
                OracleDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    if (dr[0] != null && StringUtil.CStr(dr[0]) != "" && NumberUtil.IsNumeric(StringUtil.CStr(dr[0])))
                        disAmt = int.Parse(StringUtil.CStr(dr[0]));
                }
                dr.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (conn.State == ConnectionState.Open) conn.Close();
                OracleConnection.ClearPool(conn);
            }

            return disAmt;
        }

        public static string get_discount_name(string discount_code)
        {
            string discountName = "";
            OracleConnection conn = null;
            string sqlstr = @"select discount_name from discount_master where discount_code = " + OracleDBUtil.SqlStr(discount_code);
            try
            {
                conn = OracleDBUtil.GetConnection();
                OracleCommand cmd = new OracleCommand(sqlstr, conn);
                OracleDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    if (dr[0] != null && StringUtil.CStr(dr[0]) != "")
                        discountName = StringUtil.CStr(dr[0]);
                }
                dr.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (conn.State == ConnectionState.Open) conn.Close();
                OracleConnection.ClearPool(conn);
            }

            return discountName;
        }
    }


}
