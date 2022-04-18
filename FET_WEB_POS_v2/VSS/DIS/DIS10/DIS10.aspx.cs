using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Advtek.Utility;
using System.Collections;
using FET.POS.Model.Facade;
using FET.POS.Model.Facade.FacadeImpl;
using FET.POS.Model.Common;

public partial class VSS_DIS_DIS10 : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack && !IsCallback)
        {
            // 繫結空的資料表
            gvMaster.DataSource = new DataTable();
            gvMaster.DataBind();

            BindCategory();
        }
    }

    private void BindMasterData()
    {
        DataTable dtQuery = ViewState["gvMaster"] as DataTable;
        if (dtQuery == null)
        {
            dtQuery = new DIS10_Facade().Query_ProdMapping(StringUtil.CStr(this.ddlProductCategory.SelectedItem.Value), this.txtErpAttribute1.Text);
            ViewState["gvMaster"] = dtQuery;
        }
        gvMaster.DataSource = dtQuery;
        gvMaster.DataBind();
        
    }

    private void BindCategory()
    {
        this.ddlProductCategory.DataSource = DIS10_PageHelper.GetProductCategory();
        this.ddlProductCategory.TextField = "CATE_NAME";
        this.ddlProductCategory.ValueField = "CATE_NO";
        this.ddlProductCategory.DataBind();
        this.ddlProductCategory.SelectedIndex = 0;
    }

    protected void btnQuery_Click(object sender, EventArgs e)
    {
        ViewState.Remove("gvMaster");
        gvMaster.PageIndex = 0;
        gvMaster.FocusedRowIndex = -1;
        BindMasterData();

    }

    protected void gvMaster_PageIndexChanged(object sender, EventArgs e)
    {
        if (ViewState["gvMaster"] != null)
        {
            gvMaster.FocusedRowIndex = -1;
            BindMasterData();
        }
    }
}
