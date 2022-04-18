using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Advtek.Utility;
using DevExpress.Web.ASPxGridView;
using DevExpress.Web.ASPxEditors;



public partial class VSS_ORD_ORD12 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

        string orderNo = Request.QueryString["OrderNo"] == null ? "" : Request.QueryString["OrderNo"].ToString().Trim();

        this.ViewState["orderNo"] = orderNo;

        if (!IsPostBack)

            if (this.ViewState["orderNo"] != "")
            {
                gvMasterDV.Settings.ShowTitlePanel = false;
              //  this.showBtnFooter.Visible = (true);

                bindEmptyData();

                divContent.Style["display"] = "";
                bindgvMaster();

                DataTable dtProdDetail = new DataTable();
                dtProdDetail.Columns.Clear();
                dtProdDetail.Columns.Add("商品編號", typeof(string));
                dtProdDetail.Columns.Add("商品名稱", typeof(string));
                dtProdDetail.Columns.Add("數量", typeof(int));
                //  gvDetail.DataSource = dtProdDetail;
                //  gvDetail.DataBind();

                BindData();

                //ModalPopup.Show();
            }
            else
            {
                gvMasterDV.Settings.ShowTitlePanel = true;
               // this.showBtnFooter.Visible = (false);
                // this.showDetailGv.Visible = (false);
                this.lblOrderNo.Text = "";
                bindEmptyData();
            }
    }

    // this.showEditCommand.Visible = (true);

    private void BindData()
    {
        DataTable orders = new DataTable();
        orders.Columns.Clear();
        orders.Columns.Add("商品編號", typeof(string));
        orders.Columns.Add("商品名稱", typeof(string));
        orders.Columns.Add("需求量", typeof(int));
        orders.Columns.Add("門市庫存量", typeof(int));
        orders.Columns.Add("訂購量", typeof(int));

        DataRow row = orders.NewRow();
        row["商品編號"] = "100100100";
        row["商品名稱"] = "A手機";
        row["需求量"] = 10;
        row["門市庫存量"] = 5;
        row["訂購量"] = 5;
        orders.Rows.Add(row);

        row = orders.NewRow();
        row["商品編號"] = "100100101";
        row["商品名稱"] = "B手機";
        row["需求量"] = 10;
        row["門市庫存量"] = 0;
        row["訂購量"] = 10;
        orders.Rows.Add(row);


        row = orders.NewRow();
        row["商品編號"] = "100100102";
        row["商品名稱"] = "C手機";
        row["需求量"] = 10;
        row["門市庫存量"] = 2;
        row["訂購量"] = 8;
        orders.Rows.Add(row);

        GridView1.DataSource = orders;
        GridView1.DataBind();
    }

    protected void bindEmptyData()
    {
        DataTable dtResult = new DataTable();

        // gvMaster.DataSource = dtResult;
        // gvMaster.DataBind();
        gvMasterDV.DataSource = dtResult;
        gvMasterDV.DataBind();
    }
    protected void bindgvMaster()
    {
        DataTable dtGvMaster = new DataTable();
        dtGvMaster = getMasterData(false);
        ViewState["gvMaster"] = dtGvMaster;
        // gvMaster.DataSource = dtGvMaster;
        // gvMaster.DataBind();
        gvMasterDV.DataSource = dtGvMaster;
        gvMasterDV.DataBind();

        gvMasterDV.DetailRows.ExpandRow(0);
    }

    private DataTable getMasterData(bool isEmpty)
    {
        DataTable dtMaster = new DataTable();
        dtMaster.Columns.Clear();
        dtMaster.Columns.Add("項次", typeof(int));
        dtMaster.Columns.Add("商品編號", typeof(string));
        dtMaster.Columns.Add("商品名稱", typeof(string));
        dtMaster.Columns.Add("建議訂購量", typeof(int));
        dtMaster.Columns.Add("網購需求量", typeof(int));
        dtMaster.Columns.Add("門市庫存量", typeof(int));
        dtMaster.Columns.Add("在途量", typeof(int));
        dtMaster.Columns.Add("預訂量", typeof(int));

        if (!isEmpty)
        {
            for (int i = 1; i < 12; i++)
            {
                DataRow dtMasterRow = dtMaster.NewRow();
                dtMasterRow["項次"] = i;
                dtMasterRow["商品編號"] = "A000" + i;
                dtMasterRow["商品名稱"] = "商品名稱" + i;
                dtMasterRow["建議訂購量"] = 10 * (i + 1) / 2;
                dtMasterRow["門市庫存量"] = (i + 1);
                dtMasterRow["在途量"] = 1 * (i + 1);
                dtMasterRow["網購需求量"] = 4 * (i + 1) - 7;
                dtMasterRow["預訂量"] = (4 * i) % 3;
                dtMaster.Rows.Add(dtMasterRow);
            }
        }
        return dtMaster;

    }

    protected void bindgvDetail()
    {
        DataTable dtGvProdDetail = new DataTable();
        dtGvProdDetail = getGvProdDetailData();

        //  gvDetail.DataSource = dtGvProdDetail;
        //  gvDetail.DataBind();
    }

    //private DataTable getGvProdDetailData()
    //{
    private DataTable GetEmptyDataTable()
    {
        DataTable dtResult = new DataTable();
        //DataTable dtProdDetail = new DataTable();
        dtResult.Columns.Clear();
        dtResult.Columns.Add("商品編號", typeof(string));
        dtResult.Columns.Add("商品名稱", typeof(string));
        dtResult.Columns.Add("搭配量", typeof(int));
        dtResult.Columns.Add("訂購量", typeof(int));
        return dtResult;
    }

    private DataTable getGvProdDetailData()
    {
        DataTable dtProdDetail = GetEmptyDataTable();

        for (int i = 1; i < 2; i++)
        {
            DataRow dtProdDetailRow = dtProdDetail.NewRow();
            dtProdDetailRow["商品編號"] = "A000" + i;
            dtProdDetailRow["商品名稱"] = "商品名稱" + i;
            dtProdDetailRow["搭配量"] = 1;//100 * (i + 1);
            dtProdDetailRow["訂購量"] = 2;
            dtProdDetail.Rows.Add(dtProdDetailRow);
        }
        return dtProdDetail;
    }

    protected void btnNew_Click(object sender, EventArgs e)
    {
        gvMasterDV.DataSource = getMasterData(true);
        gvMasterDV.DataBind();
        gvMasterDV.AddNewRow();
 

    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        //lblOrderNo.Text = "GG000001";
        Label2.Text = "10-正式訂單";
    }

    protected void gvMaster_OnRowDataBound(object sender, GridViewRowEventArgs e)
    {
        GridViewRow row = e.Row;
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Button b = e.Row.FindControl("btnSelect") as Button;
            if (row.DataItemIndex >= 3)
            {
                b.Enabled = false;
            }
        }

    }

    protected void gvMaster_RowEditing(object sender, GridViewEditEventArgs e)
    {
        GridView gridview = sender as GridView;
        //設定編輯欄位
        gridview.EditIndex = e.NewEditIndex;
        //Bind原查詢資料
        DataTable dt = ViewState["gvMaster"] as DataTable;
        gridview.DataSource = dt;
        gridview.DataBind();
        

    }
    protected void gvMaster_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        GridView gridview = sender as GridView;

        //取得資料

        //更新資料庫

        //取消編輯狀態
        gridview.EditIndex = -1;

        //Bind新資料(重取資料)
        bindgvMaster();
    }
    protected void gvMaster_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        GridView gridview = sender as GridView;
        gridview.EditIndex = -1;
        //Bind原查詢資料
        DataTable dt = ViewState["gvMaster"] as DataTable;
        gridview.DataSource = dt;
        gridview.DataBind();
    }

    protected void gvMaster_SelectedIndexChanged(Object sender, EventArgs e)
    {
        //GridViewRow row = gvMaster.SelectedRow;
        bindgvDetail();
    }

    protected void gvMaster_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "frSelect")
        {
            bindgvDetail();
        }
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        //  gvMaster.ShowFooterWhenEmpty = false;
        //  gvMaster.ShowFooter = false;
        // 重新繫結資料
        // if (gvMaster.Rows.Count == 0)
        // {
        //     bindEmptyData();
        //     //gvMaster.DataSource = GetEmptyDataTable();
        //     //gvMaster.DataBind();
        ////     showDetailGv.Visible = false;
        // }
        // else
        // {
        //     BindData();
        // }
    }
    protected void gvMasterDV_HtmlRowCreated(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewTableRowEventArgs e)
    { 
        if (e.RowType == GridViewRowType.Detail)
        {
            // 繫結明細資料表           
            ASPxGridView detailGrid = e.Row.FindChildControl<ASPxGridView>("gvDetailDV");
            detailGrid.DataSource = getGvProdDetailData();
            detailGrid.DataBind();
        }
    }
    protected void gvMasterDV_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
    {
        //若不使用DataSourceID時UPDATE INSERT 和DELPETE 都要自己控制以下為UPDATE時需自行控制
        ASPxGridView grid = (ASPxGridView)sender;
        DataTable dt = ViewState["gvMaster"] as DataTable ?? null;
        DataRow[] DRSelf = dt.Select("項次='" + e.Keys[0].ToString().Trim() + "'");
        if (DRSelf.Length > 0)
        {
            DRSelf[0]["商品編號"] = e.NewValues["商品編號"];
            DRSelf[0]["預訂量"] = 1;// e.NewValues["預訂量"];
        }
        grid.CancelEdit();
        e.Cancel = true;
        grid.DataSource = dt;
        grid.DataBind();
    }

    protected void ASPxCheckBox1_CheckedChanged(object sender, EventArgs e)
    {
        ////取得控制項裏的值出來
        //DevExpress.Web.ASPxGridView.GridViewDataTextColumn col = new GridViewDataTextColumn();
        //col = (GridViewDataTextColumn)gvMasterDV.Columns[0];
        //for (int i = 0; i < gvMasterDV.VisibleRowCount - 1; i++)
        //{
        //    object obj = gvMasterDV.FindRowCellTemplateControl(i, col, "ASPxCheckBox1");
        //    if (obj != null) ((ASPxCheckBox)obj).Checked = ((ASPxCheckBox)sender).Checked;
        //}
    }

    protected void gvMasterDV_InitNewRow(object sender, DevExpress.Web.Data.ASPxDataInitNewRowEventArgs e)
    {
        //e.NewValues["項次"] = "100";
        //e.NewValues["網購需求量"] = "1";
        //e.NewValues["商品名稱"] = "New商品名稱";
        //e.NewValues["門市庫存量"] = "5";
        //e.NewValues["在途量"] = "0";
        //e.NewValues["預訂量"] = "1";
    }
    protected void gvMasterDV_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
    {
        ASPxGridView grid = (ASPxGridView)sender;
        DataTable dt = ViewState["gvMaster"] as DataTable ?? getMasterData(true);
        
        //DataRow DRNew = dt.NewRow();
        //DRNew["項次"] = e.NewValues["項次"];
        //DRNew["網購需求量"] = Convert.ToInt32(e.NewValues["網購需求量"]);
        //DRNew["商品編號"] = e.NewValues["商品編號"];
        //DRNew["商品名稱"] = e.NewValues["商品名稱"];
        //DRNew["門市庫存量"] = e.NewValues["門市庫存量"];
        //DRNew["在途量"] = e.NewValues["在途量"];
        //DRNew["預訂量"] = e.NewValues["預訂量"];

        //dt.Rows.Add(DRNew);
        grid.CancelEdit();
        e.Cancel = true;
        grid.DataSource = dt;
        grid.DataBind();
        ViewState["gvMaster"] = dt;
    }
}
