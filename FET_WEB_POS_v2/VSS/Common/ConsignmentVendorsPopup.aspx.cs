using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.Web;
using DevExpress.Web.ASPxGridView;
using DevExpress.Web.ASPxPager;
using FET.POS.Model.Common;
using FET.POS.Model.Facade.FacadeImpl;
using Advtek.Utility;

public partial class VSS_Common_ConsignmentVendorsPopup : Popup
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack && !IsCallback)
        {
            gvMaster.DataSource = null ;
            gvMaster.DataBind();
        }
    }

    protected void bindCategory()
    {
        DataTable dtResult = Supplier_Facade.GetSupplierNo(false);
        gvMaster.DataSource = dtResult;
        gvMaster.DataBind();
    }

    private DataTable getMasterData()
    {
        string strSuppNo = this.TextBox1.Text.Trim();
        string strSuppName = this.TextBox2.Text.Trim();
        DataTable dt = new DataTable();
        dt = new Supplier_Facade().Query_CsmSupplier(strSuppNo, strSuppName);
        return dt;
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        BindData();
        gvMaster.FocusedRowIndex = -1;
        gvMaster.PageIndex = 0;
    }
    protected void BindData()
    {
        gvMaster.DataSource = getMasterData();
        gvMaster.DataBind();
    }
    protected void OkButton_Click(object sender, EventArgs e)
    {
        if (gvMaster.FocusedRowIndex > -1)
        {
            object key = gvMaster.GetRowValues(gvMaster.FocusedRowIndex, KeyFieldValue2.Trim());           
            SetReturnValue(StringUtil.CStr(key));
        }
        else
        {
            SetReturnValue(string.Empty);
        }
    }

    protected void grid_PageIndexChanged(object sender, EventArgs e)
    {
        ASPxGridView grid = sender as ASPxGridView;
        grid.DataSource = getMasterData();
        grid.DataBind();
    }
}
