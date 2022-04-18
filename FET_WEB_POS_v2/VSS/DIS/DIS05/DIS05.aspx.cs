using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Collections;
using DevExpress.Web.ASPxGridView;

using Advtek.Utility;
using FET.POS.Model.Facade;
using FET.POS.Model.Facade.FacadeImpl;
using FET.POS.Model.Common;
using DevExpress.Web.ASPxEditors;

public partial class VSS_DIS_DIS05 : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack && !IsCallback)
        {
            // 繫結空的資料表
            gvMaster.DataSource = new DataTable();
            gvMaster.DataBind();
        }
    }

    private void BindMasterData()
    {
        gvMaster.DataSource = new DIS05_Facade().Query_MM(this.txtPromoNo.Text, 
            this.txtPromoName.Text, this.txtBDate_S.Text, this.txtBDate_E.Text, 
            StringUtil.CStr(this.ddlProdConfigType.SelectedItem.Value), 
            StringUtil.CStr(this.ddlNeedToPricing.SelectedItem.Value), 
            this.txtProdNo_S.Text, this.txtProdNo_E.Text,  this.txtProdName.Text);
        gvMaster.DataBind();
    }
    private void BindDetailData_CATEGORY()
    {
        gvDetail1.DataSource = new DIS05_Facade().Query_MM_CATEGORY(StringUtil.CStr(gvMaster.GetRowValues(gvMaster.FocusedRowIndex, gvMaster.KeyFieldName)));
        gvDetail1.DataBind();
    }
    private void BindDetailData_PLU()
    {
        gvDetail2.DataSource = new DIS05_Facade().Query_MM_PLU(StringUtil.CStr(gvMaster.GetRowValues(gvMaster.FocusedRowIndex, gvMaster.KeyFieldName)));
        gvDetail2.DataBind();
    }
    private void BindDetailData_STORE()
    {
        gvDetail3.DataSource = new DIS05_Facade().Query_MM_STORE(StringUtil.CStr(gvMaster.GetRowValues(gvMaster.FocusedRowIndex, gvMaster.KeyFieldName)));
        gvDetail3.DataBind();
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        gvMaster.PageIndex = 0;
        BindMasterData();
        gvMaster.FocusedRowIndex = -1;
      
        ASPxPageControl1.Visible = false;
    }

    protected void ASPxPageControl1_ActiveTabChanged(object source, DevExpress.Web.ASPxTabControl.TabControlEventArgs e)
    {
        switch (this.ASPxPageControl1.ActiveTabIndex)
        {
            case 0:
                BindDetailData_CATEGORY();
                break;

            case 1:
                BindDetailData_PLU();
                break;

            case 2:
                BindDetailData_STORE();
                break;

            default:
                break;
        }
    }

    protected void gvMaster_FocusedRowChanged(object sender, EventArgs e)
    {
        if (gvMaster.FocusedRowIndex > -1)
        {
            ASPxPageControl1.ActiveTabIndex = 0;
            ASPxPageControl1.Visible = true;
            ASPxPageControl1_ActiveTabChanged(this, null);
        }
        else
        {
            ASPxPageControl1.Visible = false;
        }
    }

    protected void gvMaster_PageIndexChanged(object sender, EventArgs e)
    {
        BindMasterData();
    }

    protected void gvMaster_HtmlRowPrepared(object sender, ASPxGridViewTableRowEventArgs e)
    {
        if (e.RowType == GridViewRowType.Data)
        {
            ASPxLabel lblProdConfigType = gvMaster.FindRowCellTemplateControl(e.VisibleIndex, (GridViewDataColumn)gvMaster.Columns["PROD_CONFIG_TYPE"], "lblProdConfigType") as ASPxLabel;

            switch (StringUtil.CStr(e.GetValue("PROD_CONFIG_TYPE")))
            {
                case "1":
                    lblProdConfigType.Text = "指定商品";
                    break;
                case "2":
                    lblProdConfigType.Text = "一般商品";
                    break;

            }

            ASPxLabel lblNeedToPricing = gvMaster.FindRowCellTemplateControl(e.VisibleIndex, (GridViewDataColumn)gvMaster.Columns["NEED_TO_PRICING"], "lblNeedToPricing") as ASPxLabel;

            switch (StringUtil.CStr(e.GetValue("NEED_TO_PRICING")))
            {
                case "Y":
                    lblNeedToPricing.Text = "變價";
                    break;
                case "N":
                    lblNeedToPricing.Text = "不變價";
                    break;

            }
        }
    }

    protected void gvDetail1_HtmlDataCellPrepared(object sender, ASPxGridViewTableDataCellEventArgs e)
    {
        ASPxLabel lbl2G = e.Cell.FindChildControl<ASPxLabel>("lbl2G");
        ASPxLabel lbl3G = e.Cell.FindChildControl<ASPxLabel>("lbl3G");
        ASPxLabel lbl35G = e.Cell.FindChildControl<ASPxLabel>("lbl35G");
        ASPxLabel lblNetbook = e.Cell.FindChildControl<ASPxLabel>("lblNetbook");
        ASPxLabel lblDatacard = e.Cell.FindChildControl<ASPxLabel>("lblDatacard");
        ASPxLabel lblOther = e.Cell.FindChildControl<ASPxLabel>("lblOther");

        if (lbl2G != null) lbl2G.Text = StringUtil.CStr(e.GetValue("A")).Trim() == "" ? " " : "●";
        if (lbl3G != null) lbl3G.Text = StringUtil.CStr(e.GetValue("B")).Trim() == "" ? " " : "●";
        if (lbl35G != null) lbl35G.Text = StringUtil.CStr(e.GetValue("C")).Trim() == "" ? " " : "●";
        if (lblNetbook != null) lblNetbook.Text = StringUtil.CStr(e.GetValue("D")).Trim() == "" ? " " : "●";
        if (lblDatacard != null) lblDatacard.Text = StringUtil.CStr(e.GetValue("E")).Trim() == "" ? " " : "●";
        if (lblOther != null) lblOther.Text = StringUtil.CStr(e.GetValue("F")).Trim() == "" ? " " : "●";

    }

    protected void gvDetail1_PageIndexChanged(object sender, EventArgs e)
    {
        BindDetailData_CATEGORY();
    }

    protected void gvDetail2_PageIndexChanged(object sender, EventArgs e)
    {
        BindDetailData_PLU();
    }

    protected void gvDetail3_PageIndexChanged(object sender, EventArgs e)
    {
        BindDetailData_STORE();
    }


    //ajax 呼當前網頁的方式
    [System.Web.Services.WebMethod()]
    [System.Web.Script.Services.ScriptMethod()]
    static public int getProdInfo(string PRODNO)
    {
        int ProdNoCount = 0;
        if (!string.IsNullOrEmpty(PRODNO))
        {
            ProdNoCount = new Product_Facade().Query_ProductInfo(PRODNO).Rows.Count;
        }

        return ProdNoCount;
    }

}
