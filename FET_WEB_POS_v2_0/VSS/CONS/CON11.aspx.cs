using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using DevExpress.Web.ASPxGridView;

public partial class VSS_CON11_CON11 : Advtek.Utility.BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            // 繫結空的資料表，以顯示表頭欄位
            gvMaster.DataSource = GetEmptyDataTable();
            gvMaster.DataBind();
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
        dtResult.Columns.Add("退倉單號", typeof(string));
        dtResult.Columns.Add("退倉起日", typeof(string));
        dtResult.Columns.Add("退倉訖日", typeof(string));
        dtResult.Columns.Add("退倉日期", typeof(string));
        dtResult.Columns.Add("更新人員", typeof(string));
        dtResult.Columns.Add("更新日期", typeof(string));
        dtResult.Columns.Add("狀態", typeof(string));
        dtResult.Columns.Add("備註", typeof(string));
        return dtResult;
    }

    private DataTable getMasterData()
    {
        DataTable dtResult = GetEmptyDataTable();

        string[] ary1 = { "已存檔", "轉單中", "已成單", "已驗退" };

        for (int i = 1; i < 10; i++)
        {
            DataRow NewRow = dtResult.NewRow();
            NewRow["退倉單號"] = "COR201007300" + i;
            NewRow["退倉起日"] = "2010/07/01";
            NewRow["退倉訖日"] = "2011/07/01";
            NewRow["退倉日期"] = "2011/07/01";
            NewRow["更新人員"] = "王小明";
            NewRow["更新日期"] = "2010/07/13";
            NewRow["狀態"] = ary1[i % 4];
            NewRow["備註"] = "";
            dtResult.Rows.Add(NewRow);
        }
        return dtResult;
    }

    private DataTable getDetailData()
    {
        DataTable gvDetail = new DataTable();
        gvDetail.Columns.Clear();
        gvDetail.Columns.Add("項次", typeof(string));
        gvDetail.Columns.Add("商品料號", typeof(string));
        gvDetail.Columns.Add("商品名稱", typeof(string));
        gvDetail.Columns.Add("廠商編號", typeof(string));
        gvDetail.Columns.Add("廠商名稱", typeof(string));
        gvDetail.Columns.Add("庫存數量", typeof(string));
        gvDetail.Columns.Add("實際退倉數量", typeof(string));
        for (int i = 1; i < 5; i++)
        {
            DataRow gvDetailRow = gvDetail.NewRow();
            gvDetailRow["項次"] = i;
            gvDetailRow["商品料號"] = "商品料號" + i;
            gvDetailRow["商品名稱"] = "商品名稱" + i;
            gvDetailRow["廠商編號"] = "廠商編號" + i;
            gvDetailRow["廠商名稱"] = "廠商名稱" + i;
            gvDetailRow["庫存數量"] = i % 2;
            gvDetailRow["實際退倉數量"] = i % 2;
            gvDetail.Rows.Add(gvDetailRow);
        }
        return gvDetail;
    }

    protected void gvMaster_DetailRowExpandedChanged(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewDetailRowEventArgs e)
    {
        ASPxGridView grid = (ASPxGridView)gvMaster.FindDetailRowTemplateControl(e.VisibleIndex, "gvDetail");
        if (grid != null)
        {
            DataTable dtgvDetail = new DataTable();
            dtgvDetail = getDetailData();
            ViewState["gvDetail"] = dtgvDetail;

            grid.DataSource = dtgvDetail;
            grid.DataBind();
        }
    }
    protected void gvMaster_HtmlDataCellPrepared(object sender, ASPxGridViewTableDataCellEventArgs e)
    {
        if (e.DataColumn.FieldName != "退倉單號") return;
        DevExpress.Web.ASPxEditors.Internal.HyperLinkDisplayControl linkControl;
        linkControl = (DevExpress.Web.ASPxEditors.Internal.HyperLinkDisplayControl)e.Cell.Controls[0];
        if (linkControl != null)
        {
            if (gvMaster.GetRowValues(e.VisibleIndex, "狀態").ToString() == "已存檔")
            {
                linkControl.NavigateUrl = "CON12.aspx?dno=COR2010073001";
            }
            else
            {
                linkControl.Enabled = false;
            }
        }


    }
}
