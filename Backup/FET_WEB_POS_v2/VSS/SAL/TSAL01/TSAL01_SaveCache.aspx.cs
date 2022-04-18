using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.OracleClient;
using Advtek.Utility;
using Newtonsoft.Json.Linq;
using System.Text;
using System.Collections;
using FET.POS.Model.Facade.FacadeImpl;
using FET.POS.Model.Common;


public partial class VSS_SAL_TSAL01_TSAL01_SaveCache : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        String json_str = Request.Form["json"];
        JObject o = JObject.Parse(json_str);
        string POSUUID_MASTER = (String)o["POSUUID_MASTER"];
        JArray items = (JArray)o["SALE_DETAIL"];
        JArray discount = (JArray)o["DISCOUNT"];
        string SAVE_CACHE = (String)o["SAVE_CACHE"];
        string TRADE_DATE = (String)o["TRADE_DATE"];
        string SALE_STATUS = (String)o["SALE_STATUS"];
        string RATE = o["RATE"] == null ? "" : (String)o["RATE"];
        if (string.IsNullOrEmpty(SALE_STATUS)) SALE_STATUS = "1";
        if (SAVE_CACHE.Equals("1"))
        {
            Save(POSUUID_MASTER, TRADE_DATE, items, discount, SALE_STATUS, RATE);
        }
        else
        {
            Delete(POSUUID_MASTER);
        }

    }

    private void Delete(string POSUUID_MASTER)
    {
        OracleConnection conn = null;
        OracleTransaction trans = null;
        try
        {
            conn = OracleDBUtil.GetConnection();
            trans = conn.BeginTransaction();

            string sqlstr = "delete from SALE_HEAD where POSUUID_MASTER = " + OracleDBUtil.SqlStr(POSUUID_MASTER);
            OracleCommand cmd = new OracleCommand(sqlstr, conn, trans);
            cmd.ExecuteNonQuery();

            sqlstr = "delete from SALE_DETAIL where POSUUID_MASTER = " + OracleDBUtil.SqlStr(POSUUID_MASTER);
            cmd = new OracleCommand(sqlstr, conn, trans);
            cmd.ExecuteNonQuery();
            trans.Commit();
            Response.Clear();
            Response.ContentType = "text/plain";
            Response.Write("{ RESULT: '1' }");
        }
        catch (Exception ex)
        {
            trans.Rollback();
            Response.Clear();
            Response.ContentType = "text/plain";
            Response.Write(string.Format("{{ RESULT: '{0}'}}", ex.Message.Replace("\n", "\\n")));
        }
        finally
        {
            if (conn.State == ConnectionState.Open) conn.Close();
            OracleConnection.ClearAllPools();
        }
    }

    private void Save(string POSUUID_MASTER, string TRADE_DATE, JArray items, JArray items2, string SALE_STATUS, string RATE)
    {
        OracleConnection conn = null;
        OracleTransaction trans = null;
        try
        {
            conn = OracleDBUtil.GetConnection();
            trans = conn.BeginTransaction();
            InsertSaleHead(POSUUID_MASTER, TRADE_DATE, trans, SALE_STATUS);
            DataTable dt = InsertSaleDetail(items, POSUUID_MASTER, trans);
            InsertSaleDiscount(items2, POSUUID_MASTER, dt, trans);
            UpdateLineSeq(POSUUID_MASTER, trans);
            //判斷店長折扣比率
            if (!string.IsNullOrEmpty(RATE))
            {
                set_store_rate(POSUUID_MASTER, RATE, trans);
            }
            //驗證庫存
            CheckInventory(POSUUID_MASTER, trans);
            //平衡金額
            UpdateSaleDetail(POSUUID_MASTER, trans);
            trans.Commit();
            Response.Clear();
            Response.ContentType = "text/plain";
            Response.Write(string.Format("{{ RESULT:'1', Discount:{0}}}", GetJsonData(getCatchDiscount(POSUUID_MASTER))));
        }
        catch (Exception ex)
        {
            trans.Rollback();
            Response.Clear();
            Response.ContentType = "text/plain";
            Response.Write(string.Format("{{ RESULT : '{0}'}}", ex.Message));
        }
        finally
        {
            if (conn.State == ConnectionState.Open) conn.Close();
            OracleConnection.ClearAllPools();
        }
    }



    private void CheckInput(string posuuid_master, OracleTransaction trans)
    {
        //判斷是否有舊機折扣
        string sqlstr = "select * from SALE_DETAIL where item_type = '12' and posuuid_master = " + OracleDBUtil.SqlStr(posuuid_master);

    }



    private void set_store_rate(string posuuid_master, string RATE, OracleTransaction trans)
    {
        double sale_total_amount = 0;
        double store_rate = Convert.ToDouble(RATE);
        string sqlstr = "select sale_total_amount from sale_head where posuuid_master = " + OracleDBUtil.SqlStr(posuuid_master);
        OracleCommand cmd = new OracleCommand(sqlstr, trans.Connection, trans);
        sale_total_amount = cmd.ExecuteScalar() == null ? 0 : Convert.ToDouble(cmd.ExecuteScalar());
        if (sale_total_amount > 0)
        {
            //抓出折扣元金額
            sqlstr = "select total_amount from sale_detail where item_type='6' and posuuid_master = " + OracleDBUtil.SqlStr(posuuid_master);
            cmd = new OracleCommand(sqlstr, trans.Connection, trans);
            double store_price = cmd.ExecuteScalar() == null ? 0 : Convert.ToDouble(cmd.ExecuteScalar());
            sale_total_amount -= store_price;
            sale_total_amount = Math.Round(sale_total_amount * store_rate, MidpointRounding.AwayFromZero) * -1;
            sqlstr = string.Format("update SALE_DETAIL set unit_price = {0},total_amount = {0} where item_type = '6' and posuuid_master = {1}", sale_total_amount, OracleDBUtil.SqlStr(posuuid_master));
            cmd = new OracleCommand(sqlstr, trans.Connection, trans);
            cmd.ExecuteNonQuery();

            //重算總金額
            sqlstr = "update SALE_HEAD sh set sale_total_amount = (select sum(total_amount) from sale_detail sd where sh.posuuid_master = sd.posuuid_master) where sh.posuuid_master = " + OracleDBUtil.SqlStr(posuuid_master);
            cmd = new OracleCommand(sqlstr, trans.Connection, trans);
            cmd.ExecuteNonQuery();

            sqlstr = "update SALE_HEAD sh set discount_total_amount = (select sum(total_amount) from sale_detail sd where sh.posuuid_master = sd.posuuid_master and total_amount < 0)  where sh.posuuid_master = " + OracleDBUtil.SqlStr(posuuid_master);
            cmd = new OracleCommand(sqlstr, trans.Connection, trans);
            cmd.ExecuteNonQuery();
        }
    }

    private void UpdateLineSeq(string posuuid_master, OracleTransaction trans)
    {
        string sqlstr = string.Format("select ID from sale_detail where posuuid_master = '{0}' order by item_type", posuuid_master);
        OracleCommand cmd = new OracleCommand(sqlstr, trans.Connection, trans);
        OracleDataAdapter da = new OracleDataAdapter(cmd);
        DataTable dt = new DataTable();
        da.Fill(dt);
        sqlstr = "update SALE_DETAIL set LINE_SEQ = :LINE_SEQ where ID=:ID";
        cmd = new OracleCommand(sqlstr, trans.Connection, trans);
        cmd.Parameters.Add(":ID", OracleType.NVarChar);
        cmd.Parameters.Add(":LINE_SEQ", OracleType.Number);
        int i = 1;
        foreach (DataRow row in dt.Rows)
        {
            cmd.Parameters[":ID"].Value = StringUtil.CStr(row[0]);
            cmd.Parameters[":LINE_SEQ"].Value = i++;
            cmd.ExecuteNonQuery();
        }
    }

    private void CheckInventory(string posuuid_master, OracleTransaction trans)
    {
        string sqlstr = string.Format("select sd.prodno,sum(nvl(sd.quantity,1)) as count from sale_detail sd join product p on sd.prodno=p.prodno where sd.posuuid_master = {0} and p.isstock='1'  group by sd.prodno", OracleDBUtil.SqlStr(posuuid_master));
        OracleCommand cmd = new OracleCommand(sqlstr, trans.Connection, trans);
        OracleDataAdapter da = new OracleDataAdapter(cmd);
        DataTable dt = new DataTable();
        da.Fill(dt);
        foreach (DataRow row in dt.Rows)
        {
            string prodno = StringUtil.CStr(row[0]);
            long quantity = Convert.ToInt32(row[1]);
            long inv_count = INV25_PageHelper.GetInvOnHandCurrent(prodno, this.logMsg.STORENO);
            if (quantity > inv_count)
            {

                throw new Exception(string.Format("料號:{0}庫存量不足{1}", prodno, (quantity - inv_count).ToString()));
            }
        }
    }

    private void InsertSaleHead(string POSUUID_MASTER, string TRADE_DATE, OracleTransaction trans, string SALE_STATUS)
    {
        #region 參數

        //string SALE_STATUS = "1";//Request.Form["SALE_STATUS"]; //交易狀態 1:未結帳，2:以結帳
        string SALE_TYPE = "1";// Request.Form["SALE_TYPE"]; //1.銷售交易
        string STORE_NO = this.logMsg.STORENO;//Request.Form["STORE_NO"];
        string STORE_NAME = new Store_Facade().GetStoreName(STORE_NO);//Request.Form["STORE_NAME"];
        string MACHINE_ID = this.logMsg.MACHINE_ID;// Request.Form["MACHINE_ID"];
        #region 註解
        //string UNI_TITLE = Request.Form["UNI_TITLE"];
        //string UNI_NO = Request.Form["UNI_NO"];
        //string SALE_BEFORE_TAX = Request.Form["SALE_BEFORE_TAX"];
        //string SALE_TAX = Request.Form["SALE_TAX"];
        //string SALE_TOTAL_AMOUNT = Request.Form["SALE_TOTAL_AMOUNT"];
        //string DISCOUNT_BEFORE_TAX = Request.Form["DISCOUNT_BEFORE_TAX"];
        //string DISCOUNT_TOTAL_AMOUNT = Request.Form["DISCOUNT_TOTAL_AMOUNT"];
        //string HG_PRODNO = Request.Form["HG_PRODNO"];
        //string HG_CARD_NO = Request.Form["HG_CARD_NO"];
        //string HG_AWARD_POINT = Request.Form["HG_AWARD_POINT"];
        //string HG_REMAIN_POINT = Request.Form["HG_REMAIN_POINT"];
        //string INVALID_ID = Request.Form["INVALID_ID"];
        //string INVALID_REASON = Request.Form["INVALID_REASON"];
        //string ORIGINAL_ID = Request.Form["ORIGINAL_ID"];
        //string SALE_PERSON = Request.Form["SALE_PERSON"];
        //string CREATE_DTM = Request.Form["CREATE_DTM"];
        string MODI_USER = this.logMsg.MODI_USER;// Request.Form["MODI_USER"];
        //string MODI_DTM = Request.Form["MODI_DTM"];
        //string REMARK = Request.Form["REMARK"];
        //string PAPER_AUTH_CODE = !string.IsNullOrEmpty(Request.Form["PAPER_AUTH_CODE"]) ? Request.Form["PAPER_AUTH_CODE"] : "";
        //string rental = !string.IsNullOrEmpty(Request.Form["rental"]) ? Request.Form["rental"] : "";
        #endregion

        #endregion

        OracleConnection conn = trans.Connection;
        OracleCommand cmd = null;
        string sqlstr = "";



        sqlstr = "delete from SALE_HEAD where POSUUID_MASTER = " + OracleDBUtil.SqlStr(POSUUID_MASTER);
        cmd = new OracleCommand(sqlstr, conn, trans);
        cmd.ExecuteNonQuery();

        sqlstr = "delete from SALE_DETAIL where POSUUID_MASTER = " + OracleDBUtil.SqlStr(POSUUID_MASTER);
        cmd = new OracleCommand(sqlstr, conn, trans);
        cmd.ExecuteNonQuery();

        sqlstr = "insert into SALE_HEAD(POSUUID_MASTER,SALE_STATUS,SALE_TYPE,STORE_NO,STORE_NAME,MACHINE_ID,CREATE_DTM,MODI_USER,TRADE_DATE,CREATE_USER,SALE_PERSON,MODI_DTM,ORIGINAL_ID) ";
        sqlstr += "VALUES(:POSUUID_MASTER,:SALE_STATUS,:SALE_TYPE,:STORE_NO,:STORE_NAME,:MACHINE_ID,SYSDATE,:MODI_USER,:TRADE_DATE,:CREATE_USER,:SALE_PERSON,SYSDATE,:POSUUID_MASTER)";
        cmd = new OracleCommand(sqlstr, conn, trans);
        cmd.Parameters.Add(":POSUUID_MASTER", OracleType.NVarChar, 32).Value = POSUUID_MASTER;
        cmd.Parameters.Add(":SALE_STATUS", OracleType.NVarChar, 1).Value = SALE_STATUS;
        cmd.Parameters.Add(":SALE_TYPE", OracleType.Number, 2).Value = OracleNumber.Parse(SALE_TYPE);
        cmd.Parameters.Add(":STORE_NO", OracleType.NVarChar, 20).Value = STORE_NO;
        cmd.Parameters.Add(":STORE_NAME", OracleType.NVarChar, 50).Value = STORE_NAME;
        cmd.Parameters.Add(":MACHINE_ID", OracleType.NVarChar, 50).Value = MACHINE_ID;
        cmd.Parameters.Add(":TRADE_DATE", OracleType.DateTime).Value = TRADE_DATE;
        #region 註解
        //cmd.Parameters.Add(":UNI_TITLE", OracleType.NVarChar, 50).Value = UNI_TITLE;
        //cmd.Parameters.Add(":UNI_NO", OracleType.NVarChar, 10).Value = UNI_NO;
        //cmd.Parameters.Add(":SALE_BEFORE_TAX", OracleType.Number).Value = Convert.ToDecimal(SALE_BEFORE_TAX);
        //cmd.Parameters.Add(":SALE_TAX", OracleType.Number).Value = Convert.ToDecimal(SALE_TAX);
        //cmd.Parameters.Add(":SALE_TOTAL_AMOUNT", OracleType.Number).Value = SALE_TOTAL_AMOUNT;
        //cmd.Parameters.Add(":DISCOUNT_BEFORE_TAX", OracleType.Number).Value = DISCOUNT_BEFORE_TAX;
        //cmd.Parameters.Add(":DISCOUNT_TOTAL_AMOUNT", OracleType.Number).Value = DISCOUNT_TOTAL_AMOUNT;
        //cmd.Parameters.Add(":HG_PRODNO", OracleType.NVarChar, 20).Value = HG_PRODNO;
        //cmd.Parameters.Add(":HG_CARD_NO", OracleType.NVarChar, 20).Value = HG_CARD_NO;
        //cmd.Parameters.Add(":HG_AWARD_POINT", OracleType.Number).Value = HG_AWARD_POINT;

        //cmd.Parameters.Add(":HG_REMAIN_POINT", OracleType.NVarChar, 50).Value = HG_REMAIN_POINT;
        //cmd.Parameters.Add(":INVALID_ID", OracleType.NVarChar, 32).Value = INVALID_ID;
        //cmd.Parameters.Add(":INVALID_REASON", OracleType.NVarChar, 50).Value = INVALID_REASON;
        //cmd.Parameters.Add(":ORIGINAL_ID", OracleType.NVarChar, 32).Value = ORIGINAL_ID;
        cmd.Parameters.Add(":SALE_PERSON", OracleType.NVarChar, 20).Value = MODI_USER;

        //cmd.Parameters.Add(":CREATE_DTM", OracleType.DateTime).Value = CREATE_DTM;
        cmd.Parameters.Add(":MODI_USER", OracleType.NVarChar).Value = MODI_USER;
        cmd.Parameters.Add(":CREATE_USER", OracleType.NVarChar).Value = MODI_USER;
        //cmd.Parameters.Add(":MODI_DTM", OracleType.DateTime).Value = MODI_DTM;
        //cmd.Parameters.Add(":REMARK", OracleType.NVarChar, 150).Value = REMARK;
        #endregion
        cmd.ExecuteNonQuery();
    }


    private DataTable InsertSaleDetail(JArray items, string POSUUID_MASTER, OracleTransaction trans)
    {
        DataTable discountDt = null;
        OracleConnection conn = trans.Connection;
        OracleCommand cmd = null;

        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        sb.Append(" insert into SALE_DETAIL( ");
        sb.Append(" ID, ");
        sb.Append(" PRODNO, ");
        sb.Append(" POSUUID_MASTER, ");
        sb.Append(" QUANTITY, ");
        sb.Append(" UNIT_PRICE, ");
        sb.Append(" CREATE_USER, ");
        sb.Append(" CREATE_DTM, ");
        sb.Append(" ITEM_TYPE, ");
        sb.Append(" POSUUID_DETAIL, ");
        sb.Append(" SEQNO, ");
        sb.Append(" MODI_DTM, ");
        sb.Append(" SOURCE_TYPE, ");
        sb.Append(" SERVICE_SYS_ID,");
        sb.Append(" BARCODE1,");
        sb.Append(" BARCODE2,");
        sb.Append(" BARCODE3,");
        sb.Append(" BUNDLE_ID,");
        sb.Append(" HG_CARD_NO, ");
        sb.Append(" HG_RULE, ");
        sb.Append(" HG_REDEEM_POINT, ");
        sb.Append(" HG_RULE_PROMOTION, ");
        sb.Append(" HG_RULE_PRODUCT, ");
        sb.Append(" HG_RULE_COMMON, ");
        sb.Append(" MSISDN, ");
        sb.Append(" SIM_CARD_NO, ");
        sb.Append(" PROMOTION_CODE, ");
        sb.Append(" TOTAL_AMOUNT, ");
        sb.Append(" TAX, ");
        sb.Append(" BEFORE_TAX, ");
        sb.Append(" ORI_UNIT_PRICE, ");
        sb.Append(" SH_DISCOUNT_REASON, ");
        sb.Append(" SH_DISCOUNT_DESC, ");
        sb.Append(" FUN_ID, ");
        sb.Append(" BILLING_ACCOUNT_ID, ");
        sb.Append(" SUBSCRIBE_NO, ");
        sb.Append(" HRS_NO, ");
        sb.Append("  CARD_NO, ");
        sb.Append("  TRANS_TYPE, ");
        sb.Append("  DATA, ");
        sb.Append("  VOICE, ");
        sb.Append("  R_RATE ");
        sb.Append(" ) VALUES ( ");
        sb.Append(" :ID, ");
        sb.Append(" :PRODNO, ");
        sb.Append(" :POSUUID_MASTER, ");
        sb.Append(" :QUANTITY, ");
        sb.Append(" :UNIT_PRICE, ");
        sb.Append(" :CREATE_USER, ");
        sb.Append(" SYSDATE, ");
        sb.Append(" :ITEM_TYPE, ");
        sb.Append(" :POSUUID_DETAIL, ");
        sb.Append(" :SEQNO, ");
        sb.Append(" SYSDATE, ");
        sb.Append(" :SOURCE_TYPE, ");
        sb.Append(" :SERVICE_SYS_ID,");
        sb.Append(" :BARCODE1,");
        sb.Append(" :BARCODE2,");
        sb.Append(" :BARCODE3,");
        sb.Append(" :BUNDLE_ID,");
        sb.Append(" :HG_CARD_NO, ");
        sb.Append(" :HG_RULE, ");
        sb.Append(" :HG_REDEEM_POINT, ");
        sb.Append(" :HG_RULE_PROMOTION, ");
        sb.Append(" :HG_RULE_PRODUCT, ");
        sb.Append(" :HG_RULE_COMMON, ");
        sb.Append(" :MSISDN, ");
        sb.Append(" :SIM_CARD_NO, ");
        sb.Append(" :PROMOTION_CODE, ");
        sb.Append(" :TOTAL_AMOUNT, ");
        sb.Append(" 0, ");
        sb.Append(" :TOTAL_AMOUNT, ");
        sb.Append(" :ORI_UNIT_PRICE, ");
        sb.Append(" :SH_DISCOUNT_REASON, ");
        sb.Append(" :SH_DISCOUNT_DESC, ");
        sb.Append(" :FUN_ID, ");
        sb.Append(" :BILLING_ACCOUNT_ID, ");
        sb.Append(" :SUBSCRIBE_NO, ");
        sb.Append(" :HRS_NO, ");
        sb.Append(" :CARD_NO, ");
        sb.Append(" :TRANS_TYPE, ");
        sb.Append(" :DATA, ");
        sb.Append(" :VOICE, ");
        sb.Append(" :R_RATE ");
        sb.Append(" ) ");
        cmd = new OracleCommand(StringUtil.CStr(sb), conn, trans);

        cmd.Parameters.Add(":ID", OracleType.NVarChar, 32);
        cmd.Parameters.Add(":PRODNO", OracleType.NVarChar, 32);
        cmd.Parameters.Add(":POSUUID_MASTER", OracleType.NVarChar, 32).Value = POSUUID_MASTER;
        cmd.Parameters.Add(":POSUUID_DETAIL", OracleType.NVarChar, 32);
        cmd.Parameters.Add(":QUANTITY", OracleType.Number);
        cmd.Parameters.Add(":ITEM_TYPE", OracleType.Number);
        cmd.Parameters.Add(":SEQNO", OracleType.Number);
        cmd.Parameters.Add(":UNIT_PRICE", OracleType.Number);
        cmd.Parameters.Add(":SOURCE_TYPE", OracleType.Number);
        cmd.Parameters.Add(":SERVICE_SYS_ID", OracleType.VarChar, 50);
        cmd.Parameters.Add(":BARCODE1", OracleType.VarChar, 50);
        cmd.Parameters.Add(":BARCODE2", OracleType.VarChar, 50);
        cmd.Parameters.Add(":BARCODE3", OracleType.VarChar, 50);
        cmd.Parameters.Add(":BUNDLE_ID", OracleType.VarChar, 50);
        cmd.Parameters.Add(":CREATE_USER", OracleType.NVarChar).Value = this.logMsg.MODI_USER;

        cmd.Parameters.Add(":HG_CARD_NO", OracleType.NVarChar);
        cmd.Parameters.Add(":HG_RULE", OracleType.NVarChar);
        cmd.Parameters.Add(":HG_REDEEM_POINT", OracleType.Number);
        cmd.Parameters.Add(":HG_RULE_PROMOTION", OracleType.Number);
        cmd.Parameters.Add(":HG_RULE_PRODUCT", OracleType.Number);
        cmd.Parameters.Add(":HG_RULE_COMMON", OracleType.Number);

        cmd.Parameters.Add(":MSISDN", OracleType.NVarChar);
        cmd.Parameters.Add(":SIM_CARD_NO", OracleType.NVarChar);
        cmd.Parameters.Add(":PROMOTION_CODE", OracleType.NVarChar);

        cmd.Parameters.Add(":TOTAL_AMOUNT", OracleType.Number);
        cmd.Parameters.Add(":ORI_UNIT_PRICE", OracleType.Number);

        cmd.Parameters.Add(":SH_DISCOUNT_REASON", OracleType.NVarChar);
        cmd.Parameters.Add(":SH_DISCOUNT_DESC", OracleType.NVarChar);
        cmd.Parameters.Add(":CARD_NO", OracleType.NVarChar, 32);
        cmd.Parameters.Add(":FUN_ID", OracleType.NVarChar, 32);
        cmd.Parameters.Add(":BILLING_ACCOUNT_ID", OracleType.NVarChar, 50);
        cmd.Parameters.Add(":SUBSCRIBE_NO", OracleType.NVarChar, 50);
        cmd.Parameters.Add(":HRS_NO", OracleType.NVarChar, 50);
        //TO_CLOSE
        cmd.Parameters.Add(":TRANS_TYPE", OracleType.NVarChar, 50);
        cmd.Parameters.Add(":DATA", OracleType.NVarChar, 1);
        cmd.Parameters.Add(":VOICE", OracleType.NVarChar, 1);
        cmd.Parameters.Add(":R_RATE", OracleType.Number);

        int i = 1;
        int SALE_TOTAL_AMOUNT = 0;
        int TOTAL_AMOUNT = 0;
        foreach (object o in items)
        {
            JObject jo = (JObject)o;
            string ID = (String)jo["ID"];
            string PRODNO = (String)jo["PRODNO"];
            string QUANTITY = (String)jo["QUANTITY"];
            string UNIT_PRICE = (String)jo["PRICE"];
            string ITEM_TYPE = (String)jo["ITEM_TYPE"];
            string ORI_UNIT_PRICE = (String)jo["ORI_UNIT_PRICE"];
            //HG
            string HG_CARD_NO = (String)jo["HG_CARD_NO"];
            string HG_RULE = (String)jo["HG_RULE"];

            string HG_REDEEM_POINT = (String)jo["HG_REDEEM_POINT"];

            string HG_RULE_PROMOTION = (String)jo["HG_RULE_PROMOTION"];
            string HG_RULE_PRODUCT = (String)jo["HG_RULE_PRODUCT"];
            string HG_RULE_COMMON = (String)jo["HG_RULE_COMMON"];
            string FUN_ID = (String)jo["FUN_ID"];
            string BILLING_ACCOUNT_ID = (String)jo["BILLING_ACCOUNT_ID"];
            string SUBSCRIBE_NO = (String)jo["SUBSCRIBE_NO"];
            string HRS_NO = (String)jo["HRS_NO"];
            string TRANS_TYPE = (String)jo["TRANS_TYPE"];
            string DATA = (String)jo["DATA"];
            string VOICE = (String)jo["VOICE"];
            string R_RATE = (String)jo["R_RATE"];

            if (string.IsNullOrEmpty(ID))
                ID = GuidNo.getUUID();
            string POSUUID_DETAIL = (String)jo["POSUUID_DETAIL"];
            if (string.IsNullOrEmpty(POSUUID_DETAIL))
                POSUUID_DETAIL = GuidNo.getUUID();
            cmd.Parameters[":PRODNO"].Value = PRODNO;
            cmd.Parameters[":QUANTITY"].Value = OracleNumber.Parse(QUANTITY);
            cmd.Parameters[":UNIT_PRICE"].Value = OracleNumber.Parse(UNIT_PRICE);
            TOTAL_AMOUNT = Convert.ToInt32(QUANTITY) * Convert.ToInt32(UNIT_PRICE);
            cmd.Parameters[":TOTAL_AMOUNT"].Value = TOTAL_AMOUNT;
            cmd.Parameters[":POSUUID_DETAIL"].Value = POSUUID_DETAIL;
            cmd.Parameters[":ITEM_TYPE"].Value = ITEM_TYPE;
            if (string.IsNullOrEmpty(ORI_UNIT_PRICE))
            {
                cmd.Parameters[":ORI_UNIT_PRICE"].Value = OracleNumber.Parse(UNIT_PRICE);
            }
            else
            {
                cmd.Parameters[":ORI_UNIT_PRICE"].Value = OracleNumber.Parse(ORI_UNIT_PRICE);
            }

            if (!string.IsNullOrEmpty((String)jo["SOURCE_TYPE"]))
                cmd.Parameters[":SOURCE_TYPE"].Value = OracleNumber.Parse((String)jo["SOURCE_TYPE"]);
            else
                cmd.Parameters[":SOURCE_TYPE"].Value = DBNull.Value;

            if (!string.IsNullOrEmpty((String)jo["SERVICE_SYS_ID"]))
                cmd.Parameters[":SERVICE_SYS_ID"].Value = (String)jo["SERVICE_SYS_ID"];
            else
                cmd.Parameters[":SERVICE_SYS_ID"].Value = DBNull.Value;

            if (!string.IsNullOrEmpty((String)jo["MSISDN"]))
                cmd.Parameters[":MSISDN"].Value = (String)jo["MSISDN"];
            else
                cmd.Parameters[":MSISDN"].Value = DBNull.Value;

            if (!string.IsNullOrEmpty((String)jo["SIM_CARD_NO"]))
                cmd.Parameters[":SIM_CARD_NO"].Value = (String)jo["SIM_CARD_NO"];
            else
                cmd.Parameters[":SIM_CARD_NO"].Value = DBNull.Value;

            if (!string.IsNullOrEmpty((String)jo["PROMOTION_CODE"]))
                cmd.Parameters[":PROMOTION_CODE"].Value = (String)jo["PROMOTION_CODE"];
            else
                cmd.Parameters[":PROMOTION_CODE"].Value = DBNull.Value;

            if (!string.IsNullOrEmpty((String)jo["PROMOTION_CODE"]))
                cmd.Parameters[":PROMOTION_CODE"].Value = (String)jo["PROMOTION_CODE"];
            else
                cmd.Parameters[":PROMOTION_CODE"].Value = DBNull.Value;

            if (ITEM_TYPE == "6")
            {

                if (!string.IsNullOrEmpty((String)jo["SH_DISCOUNT_REASON"]))
                    cmd.Parameters[":SH_DISCOUNT_REASON"].Value = (String)jo["SH_DISCOUNT_REASON"];
                else
                    cmd.Parameters[":SH_DISCOUNT_REASON"].Value = DBNull.Value;

                if (!string.IsNullOrEmpty((String)jo["SH_DISCOUNT_DESC"]))
                    cmd.Parameters[":SH_DISCOUNT_DESC"].Value = (String)jo["SH_DISCOUNT_DESC"];
                else
                    cmd.Parameters[":SH_DISCOUNT_DESC"].Value = DBNull.Value;

            }
            else
            {
                cmd.Parameters[":SH_DISCOUNT_REASON"].Value = DBNull.Value;
                cmd.Parameters[":SH_DISCOUNT_DESC"].Value = DBNull.Value;
            }
            //TO_CLOSE
            cmd.Parameters[":BARCODE1"].Value = (String)jo["BARCODE1"];
            cmd.Parameters[":BARCODE2"].Value = (String)jo["BARCODE2"];
            cmd.Parameters[":BARCODE3"].Value = (String)jo["BARCODE3"];
            cmd.Parameters[":BUNDLE_ID"].Value = (String)jo["BUNDLE_ID"];
            cmd.Parameters[":TRANS_TYPE"].Value = TRANS_TYPE;
            cmd.Parameters[":DATA"].Value = DATA;
            cmd.Parameters[":VOICE"].Value = VOICE;
            if (string.IsNullOrEmpty(R_RATE))
            {
                cmd.Parameters[":R_RATE"].Value = DBNull.Value;
            }
            else
            {
                cmd.Parameters[":R_RATE"].Value = R_RATE;
            }
            cmd.Parameters[":SEQNO"].Value = StringUtil.CStr(i);
            cmd.Parameters[":ID"].Value = ID;

            cmd.Parameters[":CARD_NO"].Value = (String)jo["CARD_NO"];
            cmd.Parameters[":FUN_ID"].Value = FUN_ID;
            cmd.Parameters[":BILLING_ACCOUNT_ID"].Value = BILLING_ACCOUNT_ID;
            cmd.Parameters[":SUBSCRIBE_NO"].Value = SUBSCRIBE_NO;
            cmd.Parameters[":HRS_NO"].Value = HRS_NO;

            //HG
            cmd.Parameters[":HG_REDEEM_POINT"].Value = OracleNumber.Parse(string.IsNullOrEmpty(HG_REDEEM_POINT) ? "0" : HG_REDEEM_POINT);
            cmd.Parameters[":HG_RULE_PROMOTION"].Value = OracleNumber.Parse(string.IsNullOrEmpty(HG_RULE_PROMOTION) ? "0" : HG_RULE_PROMOTION);
            cmd.Parameters[":HG_RULE_PRODUCT"].Value = OracleNumber.Parse(string.IsNullOrEmpty(HG_RULE_PRODUCT) ? "0" : HG_RULE_PRODUCT);
            cmd.Parameters[":HG_RULE_COMMON"].Value = OracleNumber.Parse(string.IsNullOrEmpty(HG_RULE_COMMON) ? "0" : HG_RULE_COMMON);

            cmd.Parameters[":HG_CARD_NO"].Value = HG_CARD_NO;
            cmd.Parameters[":HG_RULE"].Value = HG_RULE;
            cmd.ExecuteNonQuery();

            if (Discount_Facade.CheckCreateDiscount(PRODNO))
            {
                //產生單品折扣
                if (ITEM_TYPE == "1")
                {

                    if (discountDt == null)
                    {
                        discountDt = TSAL01_Facade.get_discount(PRODNO, this.logMsg.STORENO, TOTAL_AMOUNT.ToString(), Convert.ToInt32(QUANTITY), POSUUID_DETAIL);
                    }
                    else
                    {
                        discountDt.Merge(TSAL01_Facade.get_discount(PRODNO, this.logMsg.STORENO, TOTAL_AMOUNT.ToString(), Convert.ToInt32(QUANTITY), POSUUID_DETAIL));
                    }
                }

            }

            //產生贈品
            #region CreateGift
            if (ITEM_TYPE == "1")
            {
                Hashtable table = Discount_Facade.get_gift_discount(PRODNO, this.logMsg.STORENO, this.logMsg.MODI_USER, "");
                if (table.Count > 0)
                {
                    string posuuid_detail = GuidNo.getUUID();
                    foreach (DictionaryEntry t in table)
                    {
                        string sale_detail_id = GuidNo.getUUID();
                        string discount_code = t.Key.ToString();
                        DataTable prodDt = t.Value as DataTable;
                        if (prodDt != null && prodDt.Rows.Count > 0)
                        {
                            string gift_prodno = prodDt.Rows[0][0].ToString();

                            //判斷庫存
                            Product_Facade facade = new Product_Facade();

                            string stock = Common_PageHelper.GetGoodLOCUUID();

                            DataTable product = facade.Query_ProductInfo(gift_prodno, this.logMsg.STORENO, stock);

                            if (product != null && product.Rows.Count > 0)
                            {
                                //庫存
                                string ON_HAND_QTY = string.IsNullOrEmpty(StringUtil.CStr(product.Rows[0]["ON_HAND_QTY"])) ? "0" : StringUtil.CStr(product.Rows[0]["ON_HAND_QTY"]);

                                if (ON_HAND_QTY != "0")
                                {
                                    #region Insert 贈品
                                    cmd.Parameters[":PRODNO"].Value = StringUtil.CStr(product.Rows[0]["prodno"]);
                                    cmd.Parameters[":QUANTITY"].Value = 1;
                                    cmd.Parameters[":UNIT_PRICE"].Value = Convert.ToDouble(product.Rows[0]["price"]);

                                    cmd.Parameters[":TOTAL_AMOUNT"].Value = Convert.ToDouble(product.Rows[0]["price"]);
                                    cmd.Parameters[":POSUUID_DETAIL"].Value = posuuid_detail;
                                    cmd.Parameters[":ITEM_TYPE"].Value = "13";

                                    cmd.Parameters[":ORI_UNIT_PRICE"].Value = StringUtil.CStr(product.Rows[0]["price"]);


                                    cmd.Parameters[":SOURCE_TYPE"].Value = "11";

                                    cmd.Parameters[":SERVICE_SYS_ID"].Value = DBNull.Value;


                                    cmd.Parameters[":MSISDN"].Value = DBNull.Value;


                                    cmd.Parameters[":SIM_CARD_NO"].Value = DBNull.Value;


                                    cmd.Parameters[":PROMOTION_CODE"].Value = DBNull.Value;


                                    cmd.Parameters[":PROMOTION_CODE"].Value = DBNull.Value;


                                    cmd.Parameters[":SH_DISCOUNT_REASON"].Value = DBNull.Value;
                                    cmd.Parameters[":SH_DISCOUNT_DESC"].Value = DBNull.Value;

                                    //TO_CLOSE
                                    cmd.Parameters[":BARCODE1"].Value = DBNull.Value;
                                    cmd.Parameters[":BARCODE2"].Value = DBNull.Value;
                                    cmd.Parameters[":BARCODE3"].Value = DBNull.Value;
                                    cmd.Parameters[":BUNDLE_ID"].Value = DBNull.Value;
                                    cmd.Parameters[":TRANS_TYPE"].Value = DBNull.Value;
                                    cmd.Parameters[":DATA"].Value = DBNull.Value;
                                    cmd.Parameters[":VOICE"].Value = DBNull.Value;

                                    cmd.Parameters[":R_RATE"].Value = DBNull.Value;

                                    cmd.Parameters[":SEQNO"].Value = StringUtil.CStr(i++);
                                    cmd.Parameters[":ID"].Value = sale_detail_id;

                                    cmd.Parameters[":CARD_NO"].Value = DBNull.Value;
                                    cmd.Parameters[":FUN_ID"].Value = DBNull.Value;
                                    cmd.Parameters[":BILLING_ACCOUNT_ID"].Value = DBNull.Value;
                                    cmd.Parameters[":SUBSCRIBE_NO"].Value = DBNull.Value;
                                    cmd.Parameters[":HRS_NO"].Value = DBNull.Value;

                                    //HG
                                    cmd.Parameters[":HG_REDEEM_POINT"].Value = DBNull.Value;
                                    cmd.Parameters[":HG_RULE_PROMOTION"].Value = DBNull.Value;
                                    cmd.Parameters[":HG_RULE_PRODUCT"].Value = DBNull.Value;
                                    cmd.Parameters[":HG_RULE_COMMON"].Value = DBNull.Value;

                                    cmd.Parameters[":HG_CARD_NO"].Value = DBNull.Value;
                                    cmd.Parameters[":HG_RULE"].Value = DBNull.Value;
                                    cmd.ExecuteNonQuery();
                                    #endregion

                                    #region Insert 贈品折扣
                                    sale_detail_id = GuidNo.getUUID();
                                    cmd.Parameters[":PRODNO"].Value = discount_code;
                                    cmd.Parameters[":QUANTITY"].Value = 1;
                                    cmd.Parameters[":UNIT_PRICE"].Value = Convert.ToDouble(product.Rows[0]["price"]) * -1;

                                    cmd.Parameters[":TOTAL_AMOUNT"].Value = Convert.ToDouble(product.Rows[0]["price"]) * -1;
                                    cmd.Parameters[":POSUUID_DETAIL"].Value = posuuid_detail;
                                    cmd.Parameters[":ITEM_TYPE"].Value = "16";

                                    cmd.Parameters[":ORI_UNIT_PRICE"].Value = StringUtil.CStr(product.Rows[0]["price"]);


                                    cmd.Parameters[":SOURCE_TYPE"].Value = "11";

                                    cmd.Parameters[":SERVICE_SYS_ID"].Value = DBNull.Value;


                                    cmd.Parameters[":MSISDN"].Value = DBNull.Value;


                                    cmd.Parameters[":SIM_CARD_NO"].Value = DBNull.Value;


                                    cmd.Parameters[":PROMOTION_CODE"].Value = DBNull.Value;


                                    cmd.Parameters[":PROMOTION_CODE"].Value = DBNull.Value;


                                    cmd.Parameters[":SH_DISCOUNT_REASON"].Value = DBNull.Value;
                                    cmd.Parameters[":SH_DISCOUNT_DESC"].Value = DBNull.Value;

                                    //TO_CLOSE
                                    cmd.Parameters[":BARCODE1"].Value = DBNull.Value;
                                    cmd.Parameters[":BARCODE2"].Value = DBNull.Value;
                                    cmd.Parameters[":BARCODE3"].Value = DBNull.Value;
                                    cmd.Parameters[":BUNDLE_ID"].Value = DBNull.Value;
                                    cmd.Parameters[":TRANS_TYPE"].Value = DBNull.Value;
                                    cmd.Parameters[":DATA"].Value = DBNull.Value;
                                    cmd.Parameters[":VOICE"].Value = DBNull.Value;

                                    cmd.Parameters[":R_RATE"].Value = DBNull.Value;

                                    cmd.Parameters[":SEQNO"].Value = StringUtil.CStr(i++);
                                    cmd.Parameters[":ID"].Value = sale_detail_id;

                                    cmd.Parameters[":CARD_NO"].Value = DBNull.Value;
                                    cmd.Parameters[":FUN_ID"].Value = DBNull.Value;
                                    cmd.Parameters[":BILLING_ACCOUNT_ID"].Value = DBNull.Value;
                                    cmd.Parameters[":SUBSCRIBE_NO"].Value = DBNull.Value;
                                    cmd.Parameters[":HRS_NO"].Value = DBNull.Value;

                                    //HG
                                    cmd.Parameters[":HG_REDEEM_POINT"].Value = DBNull.Value;
                                    cmd.Parameters[":HG_RULE_PROMOTION"].Value = DBNull.Value;
                                    cmd.Parameters[":HG_RULE_PRODUCT"].Value = DBNull.Value;
                                    cmd.Parameters[":HG_RULE_COMMON"].Value = DBNull.Value;

                                    cmd.Parameters[":HG_CARD_NO"].Value = DBNull.Value;
                                    cmd.Parameters[":HG_RULE"].Value = DBNull.Value;
                                    cmd.ExecuteNonQuery();
                                    #endregion

                                }
                            }
                        }
                    }

                }
            }
            #endregion

            SALE_TOTAL_AMOUNT += TOTAL_AMOUNT;
            i++;
        }

        //更新SALE_HEAD SALE_TOTAL_AMOUNT
        sb = new System.Text.StringBuilder();
        sb.Append(" update SALE_HEAD set ");
        sb.Append(" SALE_TOTAL_AMOUNT = :SALE_TOTAL_AMOUNT ");
        sb.Append(" where posuuid_master = " + OracleDBUtil.SqlStr(POSUUID_MASTER));

        cmd = new OracleCommand(StringUtil.CStr(sb), trans.Connection, trans);
        cmd.Parameters.Add(":SALE_TOTAL_AMOUNT", OracleType.Number).Value = SALE_TOTAL_AMOUNT;
        cmd.ExecuteNonQuery();


        //更新SALE_HEAD SALE_TYPE
        sb = new System.Text.StringBuilder();
        sb.Append(" select source_type from SALE_DETAIL ");
        sb.Append(" where posuuid_master = " + OracleDBUtil.SqlStr(POSUUID_MASTER));
        sb.Append(" and source_type = 3");

        cmd = new OracleCommand(StringUtil.CStr(sb), trans.Connection, trans);

        OracleDataReader dr = cmd.ExecuteReader();
        bool hasrow = dr.HasRows;
        dr.Close();
        if (hasrow)
        {
            sb = new System.Text.StringBuilder();
            sb.Append(" update SALE_HEAD set ");
            sb.Append(" SALE_TYPE = 2 ");
            sb.Append(" where posuuid_master = " + OracleDBUtil.SqlStr(POSUUID_MASTER));

            cmd = new OracleCommand(StringUtil.CStr(sb), trans.Connection, trans);
            cmd.ExecuteNonQuery();
        }

        return discountDt;
    }


    private void InsertSaleDiscount(JArray items, string POSUUID_MASTER, DataTable discountDt, OracleTransaction trans)
    {
        string sqlstr = "";
        int DISCOUNT_TOTAL_AMOUNT = 0;
        OracleCommand cmd = null;
        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        //讀取
        sb.Append(" insert into SALE_DETAIL( ");
        sb.Append(" ID, ");
        sb.Append(" PRODNO, ");
        sb.Append(" POSUUID_MASTER, ");
        sb.Append(" QUANTITY, ");
        sb.Append(" UNIT_PRICE, ");
        sb.Append(" ORI_UNIT_PRICE, ");
        sb.Append(" CREATE_USER, ");
        sb.Append(" CREATE_DTM, ");
        sb.Append(" ITEM_TYPE, ");
        sb.Append(" POSUUID_DETAIL, ");
        sb.Append(" SEQNO, ");
        sb.Append(" MODI_USER, ");
        sb.Append(" MODI_DTM, ");
        sb.Append(" SOURCE_TYPE, ");
        sb.Append(" TOTAL_AMOUNT, ");
        sb.Append(" TAX, ");
        sb.Append(" BEFORE_TAX, ");
        sb.Append(" SOURCE_REFERENCE_KEY");
        //sb.Append(" SOURCE_TYPE ");
        sb.Append(" ) VALUES ( ");
        sb.Append(" pos_uuid, ");
        sb.Append(" :PRODNO, ");
        sb.Append(" :POSUUID_MASTER, ");
        sb.Append(" :QUANTITY, ");
        sb.Append(" :UNIT_PRICE, ");
        sb.Append(" :UNIT_PRICE, ");
        sb.Append(" :CREATE_USER, ");
        sb.Append(" SYSDATE, ");
        sb.Append(" :ITEM_TYPE, ");
        sb.Append(" :POSUUID_DETAIL, ");
        sb.Append(" :SEQNO, ");
        sb.Append(" :MODI_USER, ");
        sb.Append(" SYSDATE, ");
        sb.Append(" :SOURCE_TYPE, ");
        sb.Append(" :TOTAL_AMOUNT, ");
        sb.Append(" 0, ");
        sb.Append(" :TOTAL_AMOUNT, ");
        sb.Append(" :SOURCE_REFERENCE_KEY ");
        //sb.Append(" 11 ");
        sb.Append(" ) ");

        cmd = new OracleCommand(StringUtil.CStr(sb), trans.Connection, trans);


        cmd.Parameters.Add(":PRODNO", OracleType.NVarChar, 32);
        cmd.Parameters.Add(":POSUUID_MASTER", OracleType.NVarChar, 32).Value = POSUUID_MASTER;
        cmd.Parameters.Add(":POSUUID_DETAIL", OracleType.NVarChar, 32);
        cmd.Parameters.Add(":QUANTITY", OracleType.Number);
        cmd.Parameters.Add(":ITEM_TYPE", OracleType.Number);
        cmd.Parameters.Add(":SEQNO", OracleType.Number);
        cmd.Parameters.Add(":UNIT_PRICE", OracleType.Number);
        cmd.Parameters.Add(":SOURCE_TYPE", OracleType.Number).Value = "11";
        cmd.Parameters.Add(":CREATE_USER", OracleType.NVarChar).Value = this.logMsg.MODI_USER;
        cmd.Parameters.Add(":MODI_USER", OracleType.NVarChar).Value = this.logMsg.MODI_USER;
        cmd.Parameters.Add(":SOURCE_REFERENCE_KEY", OracleType.NVarChar);
        cmd.Parameters.Add(":TOTAL_AMOUNT", OracleType.Number);

        int TOTAL_AMOUNT = 0;
        int i = 1;

        if (items != null && items.Count > 0)
        {
            #region 有傳入折扣
            foreach (object o in items)
            {
                JObject jo = (JObject)o;
                string ID = (String)jo["ID"];
                string PRODNO = (String)jo["DISCOUNT_ID"];
                string QUANTITY = (String)jo["QUANTITY"];
                string UNIT_PRICE = (String)jo["DISCOUNT_PRICE"];
                string ITEM_TYPE = "5";
                string SEQNO = (String)jo["SEQNO"];
                string POSUUID_DETAIL = (String)jo["POSUUID_DETAIL"];
                if (string.IsNullOrEmpty(POSUUID_DETAIL)) POSUUID_DETAIL = GuidNo.getUUID();
                cmd.Parameters[":PRODNO"].Value = PRODNO;
                cmd.Parameters[":QUANTITY"].Value = OracleNumber.Parse(QUANTITY);
                cmd.Parameters[":UNIT_PRICE"].Value = OracleNumber.Parse(UNIT_PRICE);
                cmd.Parameters[":POSUUID_DETAIL"].Value = POSUUID_DETAIL;
                cmd.Parameters[":ITEM_TYPE"].Value = ITEM_TYPE;
                cmd.Parameters[":SOURCE_TYPE"].Value = DBNull.Value;
                cmd.Parameters[":SEQNO"].Value = SEQNO;
                cmd.Parameters[":SOURCE_REFERENCE_KEY"].Value = ID;
                TOTAL_AMOUNT = Convert.ToInt32(QUANTITY) * Convert.ToInt32(UNIT_PRICE);
                cmd.Parameters[":TOTAL_AMOUNT"].Value = TOTAL_AMOUNT;
                cmd.ExecuteNonQuery();
                DISCOUNT_TOTAL_AMOUNT += TOTAL_AMOUNT;
                i++;
            }
            #endregion
        }
        else
        {
            #region 無傳入折扣就重新抓出折扣
            sqlstr = string.Format("select posuuid_detail from SALE_DETAIL where item_type=2 and source_type != 3 and posuuid_master = {0} group by posuuid_detail ", OracleDBUtil.SqlStr(POSUUID_MASTER));
            OracleCommand cmd2 = new OracleCommand(sqlstr, trans.Connection, trans);
            OracleDataAdapter da = new OracleDataAdapter(cmd2);
            DataTable dt = new DataTable();
            da.Fill(dt);
            foreach (DataRow row in dt.Rows)
            {
                string posuuid_detail = StringUtil.CStr(row[0]);
                sqlstr = string.Format("select T1.*, T2.PRODNAME AS DISCOUNT_NAME,'1' as QUANTITY from TO_CLOSE_DISCOUNT T1, product T2 where T1.posuuid_detail = '{0}' And T1.discount_id=T2.prodno order by  T1.SEQNO", posuuid_detail);
                DataTable dt2 = new DataTable();
                cmd2 = new OracleCommand(sqlstr, trans.Connection, trans);
                da = new OracleDataAdapter(cmd2);
                da.Fill(dt2);

                if (dt2.Rows.Count > 0)
                {
                    #region TO_CLOSE_DISCOUNT 有資料直接Insert SALE_DETAIL
                    foreach (DataRow row2 in dt2.Rows)
                    {
                        string ID = GuidNo.getUUID();
                        string PRODNO = row2["DISCOUNT_ID"].ToString();
                        string QUANTITY = "1";
                        string UNIT_PRICE = row2["DISCOUNT_PRICE"].ToString();
                        string ITEM_TYPE = "5";
                        string SEQNO = i.ToString();
                        string POSUUID_DETAIL = posuuid_detail;
                        cmd.Parameters[":PRODNO"].Value = PRODNO;
                        cmd.Parameters[":QUANTITY"].Value = OracleNumber.Parse(QUANTITY);
                        cmd.Parameters[":UNIT_PRICE"].Value = OracleNumber.Parse(UNIT_PRICE);
                        cmd.Parameters[":POSUUID_DETAIL"].Value = POSUUID_DETAIL;
                        cmd.Parameters[":ITEM_TYPE"].Value = ITEM_TYPE;
                        cmd.Parameters[":SOURCE_TYPE"].Value = DBNull.Value;
                        cmd.Parameters[":SEQNO"].Value = SEQNO;
                        cmd.Parameters[":SOURCE_REFERENCE_KEY"].Value = posuuid_detail;
                        TOTAL_AMOUNT = Convert.ToInt32(QUANTITY) * Convert.ToInt32(UNIT_PRICE);
                        cmd.Parameters[":TOTAL_AMOUNT"].Value = TOTAL_AMOUNT;
                        cmd.ExecuteNonQuery();
                        DISCOUNT_TOTAL_AMOUNT += TOTAL_AMOUNT;
                        i++;
                    }
                    #endregion
                }
                else
                {
                    #region 無資料就先產生折扣再Insert SALE_DETAIL
                    Discount_Facade.CreateDiscount(posuuid_detail);
                    da.Fill(dt2);
                    foreach (DataRow row2 in dt2.Rows)
                    {
                        string ID = GuidNo.getUUID();
                        string PRODNO = row2["DISCOUNT_ID"].ToString();
                        string QUANTITY = row2["QUANTITY"].ToString();
                        string UNIT_PRICE = row2["DISCOUNT_PRICE"].ToString();
                        string ITEM_TYPE = "5";
                        string SEQNO = i.ToString();
                        string POSUUID_DETAIL = posuuid_detail;
                        cmd.Parameters[":PRODNO"].Value = PRODNO;
                        cmd.Parameters[":QUANTITY"].Value = OracleNumber.Parse(QUANTITY);
                        cmd.Parameters[":UNIT_PRICE"].Value = OracleNumber.Parse(UNIT_PRICE);
                        cmd.Parameters[":POSUUID_DETAIL"].Value = POSUUID_DETAIL;
                        cmd.Parameters[":ITEM_TYPE"].Value = ITEM_TYPE;
                        cmd.Parameters[":SOURCE_TYPE"].Value = DBNull.Value;
                        cmd.Parameters[":SEQNO"].Value = SEQNO;
                        cmd.Parameters[":SOURCE_REFERENCE_KEY"].Value = posuuid_detail;
                        TOTAL_AMOUNT = Convert.ToInt32(QUANTITY) * Convert.ToInt32(UNIT_PRICE);
                        cmd.Parameters[":TOTAL_AMOUNT"].Value = TOTAL_AMOUNT;
                        cmd.ExecuteNonQuery();
                        DISCOUNT_TOTAL_AMOUNT += TOTAL_AMOUNT;
                        i++;
                    }
                    #endregion
                }
            }
            #endregion
        }

        #region 單品折扣
        if (discountDt != null)
        {
            foreach (DataRow row in discountDt.Rows)
            {

                string ID = GuidNo.getUUID();
                string PRODNO = row["DISCOUNT_CODE"].ToString();
                string QUANTITY = row["QUANTITY"].ToString();
                string UNIT_PRICE = row["DISCOUNT_PRICE"].ToString();
                string ITEM_TYPE = "5";
                string SEQNO = i.ToString();
                string POSUUID_DETAIL = row["POSUUID_DETAIL"].ToString();

                cmd.Parameters[":PRODNO"].Value = PRODNO;
                cmd.Parameters[":QUANTITY"].Value = OracleNumber.Parse(QUANTITY);
                cmd.Parameters[":UNIT_PRICE"].Value = OracleNumber.Parse(UNIT_PRICE);
                cmd.Parameters[":POSUUID_DETAIL"].Value = POSUUID_DETAIL;
                cmd.Parameters[":ITEM_TYPE"].Value = ITEM_TYPE;
                cmd.Parameters[":SOURCE_TYPE"].Value = DBNull.Value;
                cmd.Parameters[":SEQNO"].Value = SEQNO;
                cmd.Parameters[":SOURCE_REFERENCE_KEY"].Value = ID;
                TOTAL_AMOUNT = Convert.ToInt32(QUANTITY) * Convert.ToInt32(UNIT_PRICE);
                cmd.Parameters[":TOTAL_AMOUNT"].Value = TOTAL_AMOUNT;
                cmd.ExecuteNonQuery();
                DISCOUNT_TOTAL_AMOUNT += TOTAL_AMOUNT;
                i++;

            }
        }
        #endregion

        sqlstr = "update SALE_HEAD set SALE_TOTAL_AMOUNT = SALE_TOTAL_AMOUNT + :DISCOUNT_TOTAL_AMOUNT ,DISCOUNT_TOTAL_AMOUNT = :DISCOUNT_TOTAL_AMOUNT where POSUUID_MASTER =  " + OracleDBUtil.SqlStr(POSUUID_MASTER);
        cmd = new OracleCommand(sqlstr, trans.Connection, trans);
        cmd.Parameters.Add(":DISCOUNT_TOTAL_AMOUNT", OracleType.Number).Value = DISCOUNT_TOTAL_AMOUNT;
        cmd.ExecuteNonQuery();
    }
    //{ RESULT : '0' }
    //{ RESULT : 'Error Message', 

    public void UpdateSaleDetail(string POSUUID_MASTER, OracleTransaction trans)
    {
        //確認銷售金額是否為負
        string sqlstr = "select sale_total_amount,discount_total_amount from SALE_HEAD where sale_total_amount < 0 and posuuid_master=" + OracleDBUtil.SqlStr(POSUUID_MASTER);
        OracleCommand cmd = new OracleCommand(sqlstr, trans.Connection, trans);
        OracleDataReader dr = cmd.ExecuteReader();
        bool hasRow = false;
        double discount_total_amount = 0;
        double sale_total_amount = 0;
        double sale_amount = 0;
        if (dr.Read())
        {
            hasRow = true;
            sale_total_amount = dr.GetDouble(0);
            discount_total_amount = dr.GetDouble(1);
        }
        dr.Close();

        if (hasRow)
        {
            sale_amount = sale_total_amount - discount_total_amount;
            double temp_amount = sale_amount + discount_total_amount;


            sqlstr = string.Format("select * from sale_detail where posuuid_master = {0} and item_type in ('6','12') order by total_amount", OracleDBUtil.SqlStr(POSUUID_MASTER));
            cmd = new OracleCommand(sqlstr, trans.Connection, trans);
            OracleDataAdapter da = new OracleDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);

            sqlstr = "update SALE_DETAIL set TOTAL_AMOUNT = :TOTAL_AMOUNT,UNIT_PRICE = :TOTAL_AMOUNT where ID=:ID";
            cmd = new OracleCommand(sqlstr, trans.Connection, trans);
            cmd.Parameters.Add(":TOTAL_AMOUNT", OracleType.Number);
            cmd.Parameters.Add(":ID", OracleType.NVarChar);
            cmd.Parameters.Add(":SEQ_NO", OracleType.Number);
            foreach (DataRow row in dt.Rows)
            {
                if (temp_amount != 0)
                {
                    cmd.Parameters[":ID"].Value = row["ID"];
                    double discount_amount = Convert.ToDouble(row["total_amount"]);
                    if (discount_amount > temp_amount)
                    {
                        temp_amount += discount_amount;
                        cmd.Parameters[":TOTAL_AMOUNT"].Value = 0;
                    }
                    else
                    {
                        row["total_amount"] = discount_amount - temp_amount;
                        cmd.Parameters[":TOTAL_AMOUNT"].Value = discount_amount - temp_amount;
                        temp_amount = 0;
                    }
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }

    private string GetJsonData(DataTable dt)
    {
        if (dt == null)
            return "[]";
        StringBuilder sb = new StringBuilder("[");
        int idx = 0;
        foreach (DataRow dr in dt.Rows)
        {
            idx++;
            sb.Append("{");
            foreach (DataColumn dc in dt.Columns)
            {
                sb.AppendFormat("{0}:'{1}'", dc.ColumnName.ToUpper(), dr[dc]);
                if (dc.Ordinal != dt.Columns.Count - 1)
                    sb.Append(",");
            }
            sb.Append("}");
            if (idx != dt.Rows.Count)
                sb.Append(",");
        }
        sb.Append("]");
        return StringUtil.CStr(sb);
    }

    #region SALE_DETAIL  = 5
    private DataTable getCatchDiscount(string POSUUID_MASTER)
    {
        if (POSUUID_MASTER.Length == 0)
            return null;
        DataTable dt = new DataTable();
        if (!string.IsNullOrEmpty(POSUUID_MASTER))
        {
            OracleConnection conn = null;
            string sqlstr = string.Format("select T1.SOURCE_REFERENCE_KEY as ID,rownum as SEQNO,T1.PRODNO AS DISCOUNT_ID, T2.PRODNAME AS DISCOUNT_NAME, T1.QUANTITY,T1.UNIT_PRICE as DISCOUNT_PRICE ,T1.TOTAL_AMOUNT as DISCOUNT_AMOUNT from SALE_DETAIL T1, product T2 where T1.posuuid_master = {0} And T1.prodno = T2.prodno and T1.item_type='5'  order by T1.POSUUID_DETAIL, T1.SEQNO", OracleDBUtil.SqlStr(POSUUID_MASTER));
            try
            {
                conn = OracleDBUtil.GetConnection();
                OracleDataAdapter da = new OracleDataAdapter(sqlstr, conn);
                da.Fill(dt);

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (conn.State == ConnectionState.Open) conn.Close();
                OracleConnection.ClearAllPools();
            }
        }
        return dt;
    }
    #endregion
}
