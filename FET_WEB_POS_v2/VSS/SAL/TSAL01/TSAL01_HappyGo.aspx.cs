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

public partial class VSS_SAL_TSAL01_TSAL01_HappyGo : BasePage
{
	protected void Page_Load(object sender, EventArgs e)
	{
		String json_str = Request.Form["json"];
		JObject o = JObject.Parse(json_str);
		JArray dataTable = (JArray)o["DATATABLE"];
		string SAVE_TABLE = (String)o["SAVE_TABLE"];

		if (SAVE_TABLE.Equals("1"))
			SaveSession(dataTable);
	}

	private void SaveSession(JArray rows)
	{
		try
		{
			DataTable dt = new DataTable();
			dt.Columns.Add("PRODNO");
			dt.Columns.Add("PROMOTION_CODE");
			dt.Columns.Add("MSISDN");
			dt.AcceptChanges();
			foreach (object o in rows)
			{
				JObject jo = (JObject)o;
				DataRow dr = dt.NewRow();
				dr.BeginEdit();
				dr["PRODNO"] = (String)jo["PRODNO"];
				dr["PROMOTION_CODE"] = (String)jo["PROMOTION_CODE"];
				dr["MSISDN"] = (String)jo["MSISDN"];
				dr.EndEdit();
				dt.Rows.Add(dr);
			}
			dt.AcceptChanges();
			Session["HGDISTempTable"] = dt;
			Response.Clear();
			Response.ContentType = "text/plain";
			Response.Write("{ RESULT: '1' }");
		}
		catch (Exception ex)
		{
			Response.Clear();
			Response.ContentType = "text/plain";
			Response.Write(string.Format("{{ RESULT: '0', ERROR:'{0}' }}", ex.Message.Replace("\n", "\\n")));
		}
	}
}
