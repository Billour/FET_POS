using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using DevExpress.Web.ASPxGridView;
using Advtek.Utility;
using FET.POS.Model.Facade.FacadeImpl;

public partial class VSS_Common_OnlyPromotionsPopup : Popup
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack && !IsCallback)
        {
            grid.DataSource = new DataTable();
            grid.DataBind();
        }
    }


    protected void BindData()
    {
        grid.DataSource = GetMasterData();
        grid.DataBind();
    }

    private DataTable GetMasterData()
    {
        DataTable dtResult = new DataTable();
        dtResult = new Promontions_Facade().Query_Promontions(this.txtPromoNo.Text.Trim(), this.txtPromoName.Text.Trim(), KeyFieldValue1);
        return dtResult;
    }
    protected void okButton_Click(object sender, EventArgs e)
    {
        if (grid.FocusedRowIndex > -1)
        {
            object key = grid.GetRowValues(grid.FocusedRowIndex, grid.KeyFieldName);
            SetReturnValue(StringUtil.CStr(key));
            SetReturnOKScript();
        }
        else
        {
            SetReturnValue(string.Empty);
        }
    }
    protected void grid_PageIndexChanged(object sender, EventArgs e)
    {
        ASPxGridView grid = sender as ASPxGridView;
        grid.DataSource = GetMasterData();
        grid.DataBind();
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        BindData();
        grid.FocusedRowIndex = -1;
        grid.PageIndex = 0;
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        grid.DataSource = null;
        grid.DataBind();
        clearFilter();
    }
    private void clearFilter()
    {
        txtPromoNo.Text = "";
        txtPromoName.Text = "";
    }
}
