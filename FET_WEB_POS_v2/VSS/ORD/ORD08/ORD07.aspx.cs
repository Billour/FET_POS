using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

using DevExpress.Web.ASPxGridView;
using FET.POS.Model.Facade.FacadeImpl;
using FET.POS.Model.Common;
using Advtek.Utility;

public partial class VSS_ORD_ORD07 : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack && !Page.IsCallback)
        {
            gvMaster.DataSource = new DataTable();
            gvMaster.DataBind();

            BindddlLOC_ID();
        }
    }

    private void BindddlLOC_ID()
    {
        ddlLOC_ID.DataSource = ORD08_PageHelper.GetLOC_ID();
        ddlLOC_ID.TextField = "TEXT";
        ddlLOC_ID.ValueField = "VALUE";
        ddlLOC_ID.DataBind();
        ddlLOC_ID.SelectedIndex = 0;
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        bindMasterData();
    }

    protected void bindMasterData()
    {
        DataTable dtResult = new ORD08_Facade().Query_HQNDSORDER(this.txtHQ_NDS_ORDER_NO.Text
            , StringUtil.CStr(this.ddlLOC_ID.SelectedItem.Value)
            , StringUtil.CStr(this.ddlSTATUS.SelectedItem.Value)
            , this.txtCREATE_DATE_S.Text
            , this.txtCREATE_DATE_E.Text
            , this.txtPRODNO.Text
            , this.txtMODI_USER.Text);

        gvMaster.DataSource = dtResult;
        gvMaster.DataBind();
    }

    protected void gvMaster_PageIndexChanged(object sender, EventArgs e)
    {
        bindMasterData();
    }

    protected void gvMaster_HtmlRowPrepared(object sender, ASPxGridViewTableRowEventArgs e)
    {
        //取得控制項裏的值出來
        DevExpress.Web.ASPxGridView.GridViewDataTextColumn col = new GridViewDataTextColumn();
        col = (GridViewDataTextColumn)((ASPxGridView)sender).Columns["HQ_NDS_ORDER_NO"];       
        HyperLink hl = gvMaster.FindRowCellTemplateControl(e.VisibleIndex, col, "hlkdno1") as HyperLink;
        if (hl != null)
        {
            string encryptUrl = Utils.Param_Encrypt("dno=" + hl.Text);
            string Url = string.Format("~/VSS/ORD/ORD08/ORD08.aspx?Param={0}", encryptUrl);

            hl.NavigateUrl = Url;
        }
         
    }

}
