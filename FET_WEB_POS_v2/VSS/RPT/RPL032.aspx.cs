using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Advtek.Utility;
using FET.POS.Model.Common;
using FET.POS.Model.DTO;
using FET.POS.Model.Facade;
using FET.POS.Model.Facade.FacadeImpl;
using DevExpress.Data;
using System.Data;
using System.Text;

public partial class VSS_RPT_RPL032 : BasePage
{
	protected void Page_Load(object sender, EventArgs e)
	{
		if (!IsPostBack && !IsCallback)
			setDefault();
	}

	private void setDefault()
	{
		btnReset_Clicked(null, null);
	}

	private void BindMasterData()
	{
		if ((txtSTORE_NO_S.Text.Length == 0 && txtSTORE_NO_E.Text.Length != 0) || (txtSTORE_NO_S.Text.Length != 0 && txtSTORE_NO_E.Text.Length == 0))
		{
			ScriptManager.RegisterClientScriptBlock(this, typeof(string), "MissStoreNo", "alert('門市編號起訖兩欄位必須全填或是全空白!');", true);
			return;
		}
		if ((txtTRADE_DATE_S.Text.Length == 0 && txtTRADE_DATE_E.Text.Length != 0) || (txtTRADE_DATE_S.Text.Length != 0 && txtTRADE_DATE_E.Text.Length == 0))
		{
			ScriptManager.RegisterClientScriptBlock(this, typeof(string), "MissTradeDate", "alert('交易日期起訖兩欄位必須全填或是全空白!');", true);
			return;
		}
		if ((txtPRODNO_S.Text.Length == 0 && txtPRODNO_E.Text.Length != 0) || (txtPRODNO_S.Text.Length != 0 && txtPRODNO_E.Text.Length == 0))
		{
			ScriptManager.RegisterClientScriptBlock(this, typeof(string), "MissProdNo", "alert('商品料號起訖兩欄位必須全填或是全空白!');", true);
			return;
		}

        gvMaster.DataSource = new RPL_Facade().RPL032(txtSTORE_NO_S.Text, txtSTORE_NO_E.Text, txtPRODNO_S.Text, txtPRODNO_E.Text, txtTRADE_DATE_S.Text, txtTRADE_DATE_E.Text, logMsg.STORENO);
		gvMaster.DataBind();
	}

	private DataTable dtHeader()
	{
		DataTable dtHeader = new DataTable();
		dtHeader.Columns.Add("Text", typeof(string));
		dtHeader.Columns.Add("Align", typeof(string));
		dtHeader.Columns.Add("FontSize", typeof(string));
		dtHeader.Columns.Add("BackColor", typeof(string));

		DataRow NewRow = dtHeader.NewRow();
		StringBuilder sb = new StringBuilder();
		// 門市名稱
		sb.AppendFormat("{0}：", Resources.WebResources.StoreName);
		if (txtSTORE_NO_S.Text.Length == 0)
			sb.Append("ALL");
		else
			sb.AppendFormat("{0} {1} ~ {2} {3}", txtSTORE_NO_S.Text, new Store_Facade().GetStoreName(txtSTORE_NO_S.Text), txtSTORE_NO_E.Text, new Store_Facade().GetStoreName(txtSTORE_NO_E.Text));
		// 商品編號
		sb.AppendFormat("|{0}：", Resources.WebResources.ProductCode);
		if (txtPRODNO_S.Text.Length == 0)
			sb.Append("ALL");
		else
			sb.AppendFormat("{0}~{1}", txtPRODNO_S.Text, txtPRODNO_E.Text);
		// 訂單日期
		sb.AppendFormat("|{0}：", Resources.WebResources.OrderDate);
		if (txtTRADE_DATE_S.Text.Length == 0)
			sb.Append("ALL");
		else
			sb.AppendFormat("{0}~{1}", txtTRADE_DATE_S.Text, txtTRADE_DATE_E.Text);
		NewRow["Text"] = StringUtil.CStr(sb );

		NewRow["Align"] = "LEFT";
		NewRow["FontSize"] = "11";
		NewRow["BackColor"] = "WHITE";
		dtHeader.Rows.Add(NewRow);

		NewRow = dtHeader.NewRow();
		NewRow["Text"] = "列印日期：" + DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss")
			+ "|列印人員：" + logMsg.MODI_USER + " " + new Employee_Facade().GetEmpName(logMsg.MODI_USER)
			+ "|頁　　次：1"
			+ "|總筆數：" + this.gvMaster.VisibleRowCount;
		NewRow["Align"] = "LEFT";
		NewRow["FontSize"] = "11";
		NewRow["BackColor"] = "WHITE";
		dtHeader.Rows.Add(NewRow);

		return dtHeader;

	}
	protected void btnReset_Clicked(object sender, EventArgs e)
	{
		gvMaster.DataSource = null;
		gvMaster.DataBind();
		gvMaster.PageIndex = 0;
		txtSTORE_NO_S.Text = "";
		txtSTORE_NO_E.Text = "";
		txtTRADE_DATE_S.Text = "";
		txtTRADE_DATE_E.Text = "";
		txtPRODNO_S.Text = "";
		txtPRODNO_E.Text = "";
	}

	protected void btnSearch_Clicked(object sender, EventArgs e)
	{
		BindMasterData();
        gvMaster.PageIndex = 0;
	}

	protected void btnExport_Click(object sender, EventArgs e)
	{
		BindMasterData();
		new Output().ExportXLS(1400, "", Resources.WebResources.RPL032, dtHeader(), this.ASPxGridViewExporter1, null, null);
		Response.Redirect(Utils.CreateTamperProofDownloadXlsURL("RPL032.xls"));
	}

	protected void gvMaster_PageIndexChanged(object sender, EventArgs e)
	{
		BindMasterData();
	}
}
