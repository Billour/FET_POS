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
using FET.POS.Model.Facade.FacadeImpl;


public partial class VSS_SAL_TSAL03_TSAL03_SaveCache : BasePage
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

		if (SAVE_CACHE.Equals("1"))
		{
            Save(POSUUID_MASTER, TRADE_DATE, items, discount);
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

	private void Save(string POSUUID_MASTER, string TRADE_DATE, JArray items,JArray items2)
	{
		OracleConnection conn = null;
		OracleTransaction trans = null;
		try
		{
			conn = OracleDBUtil.GetConnection();
			trans = conn.BeginTransaction();
			InsertSaleHead(POSUUID_MASTER, TRADE_DATE, trans);
			InsertSaleDetail(items, POSUUID_MASTER, trans);
            InsertSaleDiscount(items2, POSUUID_MASTER, trans);
			trans.Commit();
			Response.Clear();
			Response.ContentType = "text/plain";
			Response.Write("{ RESULT:'1', Discount:[] }");
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

	private void InsertSaleHead(string POSUUID_MASTER, string TRADE_DATE, OracleTransaction trans)
	{
		#region 參數

		string SALE_STATUS = "1";//Request.Form["SALE_STATUS"]; //交易狀態 1:未結帳，2:以結帳
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

		sqlstr = "insert into SALE_HEAD(POSUUID_MASTER,SALE_STATUS,SALE_TYPE,STORE_NO,STORE_NAME,MACHINE_ID,CREATE_DTM,MODI_USER,TRADE_DATE,CREATE_USER,SALE_PERSON,MODI_DTM) ";
		sqlstr += "VALUES(:POSUUID_MASTER,:SALE_STATUS,:SALE_TYPE,:STORE_NO,:STORE_NAME,:MACHINE_ID,SYSDATE,:MODI_USER,:TRADE_DATE,:CREATE_USER,:SALE_PERSON,SYSDATE)";
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


	private void InsertSaleDetail(JArray items, string POSUUID_MASTER, OracleTransaction trans)
	{
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
        sb.Append(" SOURCE_TYPE ");
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
        sb.Append(" :SOURCE_TYPE ");
        sb.Append(" ) ");
        cmd = new OracleCommand(sb.ToString(), conn, trans);

		cmd.Parameters.Add(":ID", OracleType.NVarChar, 32);
		cmd.Parameters.Add(":PRODNO", OracleType.NVarChar, 32);
		cmd.Parameters.Add(":POSUUID_MASTER", OracleType.NVarChar, 32).Value = POSUUID_MASTER;
		cmd.Parameters.Add(":POSUUID_DETAIL", OracleType.NVarChar, 32);
		cmd.Parameters.Add(":QUANTITY", OracleType.Number);
		cmd.Parameters.Add(":ITEM_TYPE", OracleType.Number);
		cmd.Parameters.Add(":SEQNO", OracleType.Number);
		cmd.Parameters.Add(":UNIT_PRICE", OracleType.Number);
		cmd.Parameters.Add(":SOURCE_TYPE", OracleType.Number);
		cmd.Parameters.Add(":CREATE_USER", OracleType.NVarChar).Value = this.logMsg.MODI_USER;

		int i = 1;
		foreach (object o in items)
		{
			JObject jo = (JObject)o;
			string ID = (String)jo["ID"];
			string PRODNO = (String)jo["PRODNO"];
			string QUANTITY = (String)jo["QUANTITY"];
			string UNIT_PRICE = (String)jo["PRICE"];
			string ITEM_TYPE = (String)jo["ITEM_TYPE"];
			if (ITEM_TYPE == "6")
				ID = GuidNo.getUUID();
			string POSUUID_DETAIL = (String)jo["POSUUID_DETAIL"];
			cmd.Parameters[":PRODNO"].Value = PRODNO;
			cmd.Parameters[":QUANTITY"].Value = OracleNumber.Parse(QUANTITY);
			cmd.Parameters[":UNIT_PRICE"].Value = OracleNumber.Parse(UNIT_PRICE);
			cmd.Parameters[":POSUUID_DETAIL"].Value = POSUUID_DETAIL;
			cmd.Parameters[":ITEM_TYPE"].Value = ITEM_TYPE;
			if (!string.IsNullOrEmpty((String)jo["SOURCE_TYPE"]))
				cmd.Parameters[":SOURCE_TYPE"].Value = OracleNumber.Parse((String)jo["SOURCE_TYPE"]);
			else
				cmd.Parameters[":SOURCE_TYPE"].Value = DBNull.Value;
			cmd.Parameters[":SEQNO"].Value = i.ToString();
			cmd.Parameters[":ID"].Value = ID;
			cmd.ExecuteNonQuery();
			i++;
		}
	}


    private void InsertSaleDiscount(JArray items, string POSUUID_MASTER, OracleTransaction trans)
    {
        
        OracleCommand cmd = null;
        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        //讀取
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
        sb.Append(" MODI_USER, ");
        sb.Append(" MODI_DTM, ");
        sb.Append(" SOURCE_TYPE, ");
        sb.Append(" SOURCE_REFERENCE_KEY ");
        //sb.Append(" DISCOUNT_B_DATE ");
        //sb.Append(" DISCOUNT_E_DATE ");
        //sb.Append(" FUN_ID ");
        sb.Append(" DISCOUNT_E_DATE ");
        sb.Append(" ) VALUES ( ");
        sb.Append(" pos_uuid, ");
        sb.Append(" :PRODNO, ");
        sb.Append(" :POSUUID_MASTER, ");
        sb.Append(" :QUANTITY, ");
        sb.Append(" :UNIT_PRICE, ");
        sb.Append(" :CREATE_USER, ");
        sb.Append(" SYSDATE, ");
        sb.Append(" :ITEM_TYPE, ");
        sb.Append(" :POSUUID_DETAIL, ");
        sb.Append(" :SEQNO, ");
        sb.Append(" :MODI_USER, ");
        sb.Append(" SYSDATE, ");
        sb.Append(" :SOURCE_TYPE, ");
        sb.Append(" :SOURCE_REFERENCE_KEY ");
        //sb.Append(" :DISCOUNT_B_DATE ");
        //sb.Append(" :DISCOUNT_E_DATE ");
        //sb.Append(" :FUN_ID ");
        sb.Append(" ) ");

        cmd = new OracleCommand(sb.ToString(), trans.Connection, trans);

 
        cmd.Parameters.Add(":PRODNO", OracleType.NVarChar, 32);
        cmd.Parameters.Add(":POSUUID_MASTER", OracleType.NVarChar, 32).Value = POSUUID_MASTER;
        cmd.Parameters.Add(":POSUUID_DETAIL", OracleType.NVarChar, 32);
        cmd.Parameters.Add(":QUANTITY", OracleType.Number);
        cmd.Parameters.Add(":ITEM_TYPE", OracleType.Number);
        cmd.Parameters.Add(":SEQNO", OracleType.Number);
        cmd.Parameters.Add(":UNIT_PRICE", OracleType.Number);
        cmd.Parameters.Add(":SOURCE_TYPE", OracleType.Number);
        cmd.Parameters.Add(":CREATE_USER", OracleType.NVarChar).Value = this.logMsg.MODI_USER;
        cmd.Parameters.Add(":MODI_USER", OracleType.NVarChar).Value = this.logMsg.MODI_USER;
        cmd.Parameters.Add(":SOURCE_REFERENCE_KEY", OracleType.NVarChar);
        int i = 1;
        foreach (object o in items)
        {
            JObject jo = (JObject)o;
            string ID = (String)jo["ID"];
            string PRODNO = (String)jo["DISCOUNT_ID"];
            string QUANTITY = (String)jo["QUANTITY"];
            string UNIT_PRICE = (String)jo["DISCOUNT_PRICE"];
            string ITEM_TYPE = "5";
          
            string POSUUID_DETAIL = (String)jo["POSUUID_DETAIL"];
            cmd.Parameters[":PRODNO"].Value = PRODNO;
            cmd.Parameters[":QUANTITY"].Value = OracleNumber.Parse(QUANTITY);
            cmd.Parameters[":UNIT_PRICE"].Value = OracleNumber.Parse(UNIT_PRICE);
            cmd.Parameters[":POSUUID_DETAIL"].Value = POSUUID_DETAIL;
            cmd.Parameters[":ITEM_TYPE"].Value = ITEM_TYPE;
            cmd.Parameters[":SOURCE_TYPE"].Value = DBNull.Value;
            cmd.Parameters[":SEQNO"].Value = i.ToString();
            cmd.Parameters[":SOURCE_REFERENCE_KEY"].Value = ID;

            cmd.ExecuteNonQuery();
            i++;
        }
    }
	//{ RESULT : '0' }
	//{ RESULT : 'Error Message', 
}
