using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using AdvTek.CustomControls;

public partial class VSS_CONS_CON05 : BasePage
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
        dtResult.Columns.Add("訂單日期", typeof(string));
        dtResult.Columns.Add("廠商編號", typeof(string));
        dtResult.Columns.Add("廠商名稱", typeof(string));
        dtResult.Columns.Add("區域", typeof(string));
        dtResult.Columns.Add("訂單編號", typeof(string));
        dtResult.Columns.Add("狀態", typeof(string));
        dtResult.Columns.Add("更新人員", typeof(string));
        dtResult.Columns.Add("更新日期", typeof(string));
        return dtResult;
    }

    private DataTable getMasterData()
    {
        DataTable dtResult = GetEmptyDataTable();

        string[] ary1 = { "已存檔", "轉單中", "已成單", "待進貨", "已驗收" };

        for (int i = 1; i < 16; i++)
        {
            DataRow NewRow = dtResult.NewRow();
            NewRow["訂單日期"] = "2010/07/08";
            NewRow["廠商編號"] = "V0001";
            NewRow["廠商名稱"] = "廠商名稱" + i;
            NewRow["區域"] = "北一區";
            NewRow["訂單編號"] = "10190007" + i;
            NewRow["狀態"] = ary1[i % 5];
            NewRow["更新人員"] = "王小明";
            NewRow["更新日期"] = "2010/07/13";
            dtResult.Rows.Add(NewRow);
        }
        return dtResult;
    }

    private DataTable getDetailData()
    {
        DataTable gvDetail = new DataTable();
        gvDetail.Columns.Clear();
        gvDetail.Columns.Add("項次", typeof(string));
        gvDetail.Columns.Add("廠商名稱", typeof(string));
        gvDetail.Columns.Add("商品料號", typeof(string));

        gvDetail.Columns.Add("商品名稱", typeof(string));
        gvDetail.Columns.Add("商品類別", typeof(string));
        gvDetail.Columns.Add("建議訂購量", typeof(string));
        gvDetail.Columns.Add("實際訂購量", typeof(string));
        gvDetail.Columns.Add("核准數量", typeof(string));
        gvDetail.Columns.Add("驗收量", typeof(string));

        string[] ary1 = { "3G Handset", "SIM Card", "3G Accessory", "On Line Recharge", "SIM Card", "3G Accessory" };

        for (int i = 1; i < 5; i++)
        {
            DataRow gvDetailRow = gvDetail.NewRow();
            gvDetailRow["項次"] = i;
            gvDetailRow["廠商名稱"] = "廠商名稱" + i;
            gvDetailRow["商品料號"] = "商品料號" + i;

            //gvDetailRow["狀態"] = array1[i % 3];
            gvDetailRow["商品名稱"] = "商品名稱" + i;
            gvDetailRow["商品類別"] = ary1[i % 3];
            gvDetailRow["建議訂購量"] = i;
            gvDetailRow["實際訂購量"] = i % 2;
            gvDetailRow["核准數量"] = i % 2;
            gvDetailRow["驗收量"] = i % 2;
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
    protected void gvMaster_HtmlDataCellPrepared(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewTableDataCellEventArgs e)
    {
        if (e.DataColumn.FieldName != "訂單編號") return;
        DevExpress.Web.ASPxEditors.Internal.HyperLinkDisplayControl linkControl;
        linkControl = (DevExpress.Web.ASPxEditors.Internal.HyperLinkDisplayControl)e.Cell.Controls[0];
        if (linkControl != null)
        {
            if (gvMaster.GetRowValues(e.VisibleIndex, "狀態").ToString() == "已存檔")
            {
                linkControl.NavigateUrl = "CON06.aspx?dno=101900073";
            }
            else
            {
                linkControl.Enabled = false;
            }
        }


    }
}
