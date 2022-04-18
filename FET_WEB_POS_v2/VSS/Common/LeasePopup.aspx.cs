using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using DevExpress.Web;
using DevExpress.Web.ASPxGridView;
using DevExpress.Web.ASPxPager;
using FET.POS.Model.Facade.FacadeImpl;
using FET.POS.Model.Common;
using Advtek.Utility;

public partial class VSS_Common_LeasePopup : Popup
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            grid.DataSource = new DataTable();
            grid.DataBind();
        }
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
    private DataTable GetEmptyDataTable()
    {
        DataTable dtResult = new DataTable();
        dtResult.Columns.Add("PRODNO", typeof(string));
        dtResult.Columns.Add("PRODNAME", typeof(string));
        return dtResult;
    }
    private DataTable GetMasterData()
    {
        string sProdNo = this.TextBox1.Text.Trim();
        string sProName = this.TextBox6.Text.Trim();
        DataTable dtResult = GetEmptyDataTable();
        Product_Facade cProductFacase = new Product_Facade();
        dtResult = cProductFacase.Query_LeaseDiscountProduct(sProdNo, sProName);
        return dtResult;
    }
    protected void grid_SelectionChanged(object sender, EventArgs e)
    {
        List<object> keys = grid.GetSelectedFieldValues("PRODNO");
        int i = grid.FocusedRowIndex;
    }
    protected void grid_PageIndexChanged(object sender, EventArgs e)
    {
        ASPxGridView grid = sender as ASPxGridView;
        grid.DataSource = GetMasterData();
        grid.DataBind();
    }
    protected void OkButton_Click(object sender, EventArgs e)
    {
        if (grid.FocusedRowIndex > -1)
        {
            object key = grid.GetRowValues(grid.FocusedRowIndex, grid.KeyFieldName);
            SetReturnValue(StringUtil.CStr(key));
            object key2 = grid.GetRowValues(grid.FocusedRowIndex, "PRODNAME");
            if (key2 != null) { SetReturnValue2(StringUtil.CStr(key)); };

            SetReturnOKScript();
        }
        else
        {
            SetReturnValue(string.Empty);
            SetReturnValue2(string.Empty);
        }
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        grid.DataSource = null;
        grid.DataBind();
        clearFilter();
    }
    private void clearFilter()
    {
        TextBox1.Text = "";
        TextBox6.Text = "";
    }
}
