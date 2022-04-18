using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using DevExpress.Web.ASPxGridView;
using DevExpress.Web.ASPxEditors;
using DevExpress.Web.ASPxPager;

public partial class VSS_INV_INV26 : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            // 繫結空的資料表，以顯示表頭欄位
            //gvMaster.DataSource = GetEmptyDataTable();
            //gvMaster.DataBind();
            bindMasterData();
        }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        bindMasterData();
    }

    protected void bindMasterData()
    {
        DataTable dtResult = new DataTable();
        dtResult = getMasterData();
        ViewState["gvMaster"] = dtResult;
        gvMaster.DataSource = dtResult;
        gvMaster.DataBind();
    }
    private DataTable GetEmptyDataTable()
    {
        DataTable dtResult = new DataTable();
        dtResult.Columns.Add("移撥單號", typeof(string));
        dtResult.Columns.Add("移出門市", typeof(string));
        dtResult.Columns.Add("移出日期", typeof(string));
        dtResult.Columns.Add("撥入日期", typeof(string));
        dtResult.Columns.Add("移撥狀態", typeof(string));
        dtResult.Columns.Add("更新人員", typeof(string));
        dtResult.Columns.Add("更新日期", typeof(string));
        return dtResult;

    }
    private DataTable getMasterData()
    {
        DataTable dtResult = GetEmptyDataTable();

        for (int i = 1; i < 16; i++)
        {
            DataRow NewRow = dtResult.NewRow();
            NewRow["移撥單號"] = "ST2101-100815" + i.ToString("000");
            NewRow["移出門市"] = "移出門市" + i.ToString("000");
            NewRow["移出日期"] = "2010/08/15";
            NewRow["撥入日期"] = "";
            NewRow["移撥狀態"] = "在途";
            NewRow["更新人員"] = "王小明";
            NewRow["更新日期"] = "2010/07/13";
            dtResult.Rows.Add(NewRow);
        }
        return dtResult;

    }
    protected void bindDetailData()
    {
        DataTable dtgvDetail = new DataTable();
        dtgvDetail = getDetailData();
        ViewState["gvDetail"] = dtgvDetail;
        //gvDetail.DataSource = dtgvDetail;
        //gvDetail.DataBind();
    }
    private DataTable getDetailData()
    {
        DataTable gvDetail = new DataTable();
        gvDetail.Columns.Clear();
        gvDetail.Columns.Add("商品料號", typeof(string));
        gvDetail.Columns.Add("商品名稱", typeof(string));
        gvDetail.Columns.Add("移出數量", typeof(string));
        gvDetail.Columns.Add("撥入數量", typeof(string));
        gvDetail.Columns.Add("CHECK", typeof(bool));
        //gvDetail.Columns.Add("IMEI控管", typeof(string));
        gvDetail.Columns.Add("IMEI", typeof(string));
        for (int i = 1; i < 7; i++)
        {
            DataRow gvDetailRow = gvDetail.NewRow();
            gvDetailRow["商品料號"] = "10010010" + i;
            gvDetailRow["商品名稱"] = "商品名稱" + i.ToString("000");
            gvDetailRow["移出數量"] = i;
            gvDetailRow["撥入數量"] = ((i % 2) == 0) ? "0" : "5";
            gvDetailRow["CHECK"] = ((i % 2) == 0) ? true : false;
            //gvDetailRow["IMEI控管"] = ((i % 2) == 0) ? "0" : "1";
            gvDetailRow["IMEI"] = ((i % 2) == 0) ? "0" : "5";
            gvDetail.Rows.Add(gvDetailRow);
        }
        return gvDetail;
    }


    protected void gvMaster_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "select")
        {
            bindDetailData();
            DIVdetail.Visible = true;
            //this.gvDetail.Visible = true;
        }
        else if (e.CommandName == "View")
        {
            Page.Response.Redirect("CON06.aspx");
        }
    }

    protected void gvMaster_HtmlRowCreated1(object sender, ASPxGridViewTableRowEventArgs e)
    {
        if (e.RowType == GridViewRowType.Detail)
        {
            // 繫結明細資料表           
            ASPxGridView detailGrid = e.Row.FindChildControl<ASPxGridView>("gvDetail");
            detailGrid.DataSource = getDetailData();
            detailGrid.DataBind();
        }
    }

    protected void gvMaster_HtmlRowPrepared(object sender, ASPxGridViewTableRowEventArgs e)
    {
        if (e.RowType == GridViewRowType.Data)
        {
            if (e.GetValue("移撥狀態").ToString() == "在途")
            {
                e.Row.ForeColor = System.Drawing.Color.Red;
            }
        }
    }

    protected void gvDetail_HtmlRowCreated(object sender, ASPxGridViewTableRowEventArgs e)
    {

        if (e.RowType == GridViewRowType.Data)
        {

            //HiddenField hfIMEI = e.Row.FindChildControl<HiddenField>("hidIMEI");
            Label lblIMEI = e.Row.FindChildControl<Label>("Label34");
            //CheckBox cbIMEI = e.Row.FindChildControl<CheckBox>("CheckBox3");
            ASPxButton btnIMEI = e.Row.FindChildControl<ASPxButton>("Button10");
            //cbIMEI.Checked = (Convert.ToInt16(hfIMEI.Value) > 0) ? true : false;
            //btnIMEI.Enabled = (cbIMEI.Checked == true) ? true : false;

            // 繫結明細資料表           
            lblIMEI.Attributes["onmouseover"] = string.Format("show('{0}');", IMEIContent(Convert.ToInt16(lblIMEI.Text)));
            lblIMEI.Attributes["onmouseout"] = "hide();";

            //ASPxGridView detailGrid = e.Row.FindChildControl<ASPxGridView>("gvDetail");
            //ASPxTextBox intC_IMEI = e.Row.FindChildControl<ASPxTextBox>("TextBox2");
            //ASPxLabel intS_IMEI = e.Row.FindChildControl<ASPxLabel>("Label34");

            int intC_IMEI = int.Parse(e.GetValue("撥入數量").ToString());
            int intS_IMEI = int.Parse(e.GetValue("IMEI").ToString());
            ASPxImage imgIMEI = e.Row.FindChildControl<ASPxImage>("imgIMEI");

            //int intC_IMEI = int.Parse(detailGrid.GetRowValuesByKeyValue(e.VisibleIndex, "撥入數量").ToString());
            //int intS_IMEI = int.Parse(detailGrid.GetRowValues(e.VisibleIndex, "IMEI").ToString());
            //ASPxImage imgIMEI = (ASPxImage)detailGrid.FindRowCellTemplateControl(e.VisibleIndex, (GridViewDataColumn)detailGrid.Columns[4], "imgIMEI");

            //img status
            //intC_IMEI - intS_IMEI = 0
            if (( intC_IMEI  -  intS_IMEI ) == 0)
            {
                imgIMEI.ImageUrl = "~/Icon/check.png";
            }
            //intC_IMEI - intS_IMEI > 1
            if (( intC_IMEI  -  intS_IMEI ) > 0)
            {
                imgIMEI.ImageUrl = "~/Icon/non_complete.png";
            }



        }
    }

    private string IMEIContent(int Count)
    {
        string IMEI_FORMAT = "<table border=\"1\">";
        for (int i = 1; i <= Count; i++)
        {
            IMEI_FORMAT += "<tr><td>778194415641786" + i.ToString() + "</td></tr>";
        }
        IMEI_FORMAT += "</tr></table>";

        return IMEI_FORMAT;
    }
}
