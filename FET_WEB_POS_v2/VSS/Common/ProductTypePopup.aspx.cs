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
using DevExpress.Web.Data;
using Advtek.Utility;
using FET.POS.Model.Facade.FacadeImpl;
using FET.POS.Model.Common;

public partial class VSS_Common_ProductTypePopup : Popup
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack && !IsCallback)
        {
            // 繫結空的資料表，以顯示表頭欄位
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

    protected void btnReset_Click(object sender, EventArgs e)
    {
        ProductTypeNoTextBox.Text = "";
        ProductTypeNameTextBox.Text = "";
        grid.DataSource = new DataTable();
        grid.DataBind();
    }

    protected void BindData()
    {
        grid.DataSource = new Product_Facade().Query_ProductType(ProductTypeNoTextBox.Text, ProductTypeNameTextBox.Text); 
        grid.DataBind();
    }
    private DataTable GetMasterData()
    {
        DataTable dtResult = new DataTable();
        return dtResult;
    }

    protected string GetFieldValue(object item)
    {
        WebDescriptorRowBase row = item as WebDescriptorRowBase;
        return StringUtil.CStr(grid.GetRowValues(row.VisibleIndex, grid.KeyFieldName));
    }

    protected string GetFieldChecked(object item)
    {
        if (Session["FocusedRow"] == null)
            return string.Empty;

        WebDescriptorRowBase row = item as WebDescriptorRowBase;
        object obj = grid.GetRowValues(row.VisibleIndex, grid.KeyFieldName);
        return Session["FocusedRow"] == obj ? "checked" : string.Empty;
    }


    protected void OkButton_Click(object sender, EventArgs e)
    {
        if (grid.FocusedRowIndex > -1)
        {

            object key = grid.GetRowValues(grid.FocusedRowIndex, grid.KeyFieldName);
            SetReturnValue(StringUtil.CStr(key));
            object key2 = grid.GetRowValues(grid.FocusedRowIndex, "PRODTYPENAME");
            if (key2 != null) { SetReturnValue2(StringUtil.CStr(key)); };

            SetReturnOKScript();

        }
        else
        {
            SetReturnValue(string.Empty);
            SetReturnValue2(string.Empty);
        }
    }


    /// <summary>
    /// 主GridView的分頁變更事件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void grid_PageIndexChanged(object sender, EventArgs e)
    {
        BindData();
    }
    protected void btnClear_Click(object sender, EventArgs e)
    {
        this.ProductTypeNoTextBox.Text = "";
        this.ProductTypeNameTextBox.Text = "";
    }
}
