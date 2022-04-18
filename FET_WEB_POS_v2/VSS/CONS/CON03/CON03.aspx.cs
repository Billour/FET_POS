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
using DevExpress.Web.ASPxEditors;

using Advtek.Utility;
using FET.POS.Model.Common;
using FET.POS.Model.DTO;
using FET.POS.Model.Facade;
using FET.POS.Model.Facade.FacadeImpl;
using System.Web.Configuration;

public partial class VSS_CONS_CON03 : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {            
            //商品類別下拉
            bindDdlValTxt(ddlProductCategory, PRODUCT_PageHelper.GetCsmProDTypeNo(true), "PRODTYPE_NO", "PRODTYPE_NAME");
            aspComboBoxDefaultSetting();
            bindMasterData();

        }
        else
        {
            DataTable dtGvMaster = ViewState["gvMaster"] as DataTable;
            if (dtGvMaster != null)
            {
                gvMaster.DataSource = dtGvMaster;
                gvMaster.DataBind();
            }
        }
    }
    protected void bindDdlValTxt(ASPxComboBox AspCB, object dataSrc, string valCol, string txtCol)
    {
        AspCB.DataSource = dataSrc;
        AspCB.ValueField = valCol;
        AspCB.TextField = txtCol;
        AspCB.DataBind();
    }
    /// <summary>
    /// 下拉選單初始值
    /// </summary>
    protected void aspComboBoxDefaultSetting()
    {
        //若都沒選,將Index設為0
        //判斷下拉式選單預設值-999,並替換為空字串""
        if (ddlProductCategory.SelectedIndex == -1)
        {
            ddlProductCategory.SelectedIndex = 0;
            ddlProductCategory.SelectedItem.Value = "";
            //設定值完後,SelectedItem會變Null,要再次指定SelectedIndex,超機車XD!
            ddlProductCategory.SelectedIndex = 0;
        }
        //if (ddlSupplierNo.SelectedIndex == -1)
        //{
        //    ddlSupplierNo.SelectedIndex = 0;
        //    ddlSupplierNo.SelectedItem.Value = "";
        //    //設定值完後,SelectedItem會變Null,要再次指定SelectedIndex,超機車XD!
        //    ddlSupplierNo.SelectedIndex = 0;
        //}
    }

    protected void bindMasterData()
    {
        DataTable dtResult = new DataTable();
        dtResult = getMasterData();
        ViewState["gvMaster"] = dtResult;
        gvMaster.DataSource = dtResult;
        gvMaster.DataBind();
    }
    private DataTable getMasterData()
    {
        DataTable dtResult = new DataTable();
        dtResult = new CON04_Facade().Query_CsmSupplierGrid(txtSupplierNo.Text, txtSupplierName.Text,
            ddlProductCategory.SelectedItem.Value.ToString(), ProductsPopup1.Text, txtProductName.Text,
            dateFrom.Text, dateTo.Text, SupportStartDateFrom.Text, SupportStartDateTo.Text, SupportExpiryDateFrom.Text,
            SupportExpiryDateTo.Text);
        return dtResult;
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        bindMasterData();
        Div1.Visible = true;
    }
   
    protected void btnXlsExport_Click(object sender, EventArgs e)
    {
        ORD02_Facade Facade = new ORD02_Facade();
        DataTable dtExport = new DataTable();
        dtExport = new CON04_Facade().Export_CsmSupplierGrid(txtSupplierNo.Text, txtSupplierName.Text,
            ddlProductCategory.SelectedItem.Value.ToString(), ProductsPopup1.Text, txtProductName.Text,
            dateFrom.Text, dateTo.Text, SupportStartDateFrom.Text, SupportStartDateTo.Text, SupportExpiryDateFrom.Text,
            SupportExpiryDateTo.Text);
        string filename = new Output().Print("XLS", "寄銷商品查詢作業<td></td><td>匯出時間：" + DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss") + "</td>", null, dtExport, null);
        Response.Redirect(Utils.CreateTamperProofDownloadXlsURL("CSN_SEARCH.xls"));
      
    }
    protected void gvMaster_HtmlRowPrepared(object sender, ASPxGridViewTableRowEventArgs e)
    {

        if (e.RowType == GridViewRowType.Data)
        {
            DataRowView row = gvMaster.GetRow(e.VisibleIndex) as DataRowView;

            ASPxHyperLink link = (ASPxHyperLink)gvMaster.FindRowCellTemplateControl(e.VisibleIndex, (GridViewDataColumn)gvMaster.Columns[0], "ASPxHyperLink1");
            link.NavigateUrl = "~/VSS/CONS/CON04/CON04.aspx?ProdNo=" + e.GetValue("PRODNO").ToString();
            link.Text = e.GetValue("SUPP_NO").ToString();
        }
    }
}
