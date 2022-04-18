using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using DevExpress.Web;
using DevExpress.Web.ASPxGridView;
using DevExpress.Web.ASPxPager;
using FET.POS.Model.Facade.FacadeImpl;
using FET.POS.Model.Common;
using FET.POS.Model.Facade.ConvertApp;
using System.Web.Configuration;

public partial class VSS_TSAL01_ProductsPopup : BasePage
{
	protected void Page_Load(object sender, EventArgs e)
	{
		if (!IsPostBack && !IsCallback)
		{
			grid.DataSource = new DataTable();
			grid.DataBind();
			bindCategory();
		}
	}

	private void bindCategory()
	{
		DataTable dt = new DataTable();

		dt = PRODUCT_PageHelper.GetProDTypeNo(true);

		cboCategory.DataSource = dt;
		cboCategory.TextField = "PRODTYPE_NAME";
		cboCategory.ValueField = "PRODTYPE_NO";
		cboCategory.DataBind();
	}

	protected void btnSearch_Click(object sender, EventArgs e)
	{
		BindData();
		grid.FocusedRowIndex = -1;
		grid.PageIndex = 0;
	}

	protected void BindData()
	{
		grid.DataSource = GetMasterData();
		grid.DataBind();
	}

	private DataTable GetMasterData()
	{
		string sProdNo = this.TextBox1.Text.Trim();
		string sProName = this.TextBox6.Text.Trim();
		string sProdType = this.cboCategory.SelectedItem.Value.ToString();
        string store_no = this.logMsg.STORENO;
        string stock = Common_PageHelper.GetGoodLOCUUID();
        return new Product_Facade().Query_Product(sProdNo, sProName, sProdType, store_no, stock, "HQ");
	}


	protected void OkButton_Click(object sender, EventArgs e)
	{
		if (grid.FocusedRowIndex > -1)
		{
			object prodNo = grid.GetRowValues(grid.FocusedRowIndex, grid.KeyFieldName);
			object prodName = grid.GetRowValues(grid.FocusedRowIndex, "PRODNAME");
			object prodType = grid.GetRowValues(grid.FocusedRowIndex, "PRODTYPENAME");
			StringBuilder json = new StringBuilder();
			json.AppendFormat("{{ ProdNo : \\'{0}\\'", prodNo);
			if (prodName != null)
				json.AppendFormat(", ProdName : \\'{0}\\'", prodName);
			if (prodName != null)
				json.AppendFormat(", ProdType : \\'{0}\\'", prodType);
			json.Append("}");
			SetReturnValue(json.ToString());
		}
		else
		{
			SetReturnValue(string.Empty);
		}
	}

	protected void grid_PageIndexChanged(object sender, EventArgs e)
	{
		BindData();
	}

	protected void SetReturnValue(string value)
	{
		StringBuilder script = new StringBuilder();
		script.AppendFormat("returnChoosed('{0}');", value);
		ClientScript.RegisterStartupScript(GetType(), GetType().ToString() + "_ReturnChoosed", script.ToString(), true);
	}

    protected void btnReset_Click(object sender, EventArgs e)
    {
        Response.Redirect("ProductsPopup.aspx");
    }
}
