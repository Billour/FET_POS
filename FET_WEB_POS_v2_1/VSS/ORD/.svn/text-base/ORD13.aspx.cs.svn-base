using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.Web.ASPxGridView;
using DevExpress.Web.ASPxEditors;

public partial class VSS_ORD_ORD13 : BasePage//System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            bindEmptyData();
        }
    }
    protected void bindEmptyData()
    {

        DataTable dtResult = new DataTable();
    }

    protected void bindMasterData()
    {
        DataTable dtResult = new DataTable();
        dtResult = getMasterData();
        ViewState["gvMaster"] = dtResult;
        gvMasterDV.DataSource = dtResult;
        gvMasterDV.DataBind();
    }
    private DataTable getMasterData()
    {
        DataTable dtResult = new DataTable();
        dtResult.Columns.Add("項次", typeof(string));
        dtResult.Columns.Add("門市編號", typeof(string));
        dtResult.Columns.Add("門市名稱", typeof(string));
        dtResult.Columns.Add("更新人員", typeof(string));
        dtResult.Columns.Add("更新日期", typeof(string));

        List<string[]> dataRows = new List<string[]> { 
            new string[] {"1", "2101", "遠企", "王小明", "2010/8/31 11:11"},
            new string[] {"2", "2102", "華納", "王小明", "2010/8/31 11:11"},
            new string[] {"3", "2103", "永和", "王小明", "2010/8/31 11:11"},
            new string[] {"4", "2201", "站前", "王小明", "2010/8/31 11:11"},
            new string[] {"5", "2202", "中山", "王小明", "2010/8/31 11:11"}                                   
        };

        dataRows.ForEach(dr =>
        {
            DataRow NewRow = dtResult.NewRow();
            NewRow["項次"] = dr[0];
            NewRow["門市編號"] = dr[1];
            NewRow["門市名稱"] = dr[2];
            NewRow["更新人員"] = dr[3];
            NewRow["更新日期"] = dr[4];
            dtResult.Rows.Add(NewRow);
        });

        return dtResult;
    }

    protected void bindDetailData()
    {
        DataTable dtResult = getDetailData();
        ViewState["gvDetail"] = dtResult;
        gvDetail.DataSource = dtResult;
        gvDetail.DataBind();
    }

    private DataTable getDetailData()
    {
        DataTable dtResult = new DataTable();

        dtResult.Columns.Add("項次", typeof(string));
        dtResult.Columns.Add("卡片群組", typeof(string));
        dtResult.Columns.Add("安全庫存量", typeof(int));
        dtResult.Columns.Add("最低庫存量", typeof(int));
        dtResult.Columns.Add("補貨量", typeof(string));
        dtResult.Columns.Add("已補貨量", typeof(string));

        List<string[]> dataRows = new List<string[]> { 
            new string[] {"1", "2G", "1000", "500"},
            new string[] {"2", "3G", "1500", "500"},
            new string[] {"3", "Postpaid", "800", "300"},
            new string[] {"4", "Prepaid", "800", "300"}                              
        };

        dataRows.ForEach(dr =>
        {
            DataRow NewRow = dtResult.NewRow();
            NewRow["項次"] = dr[0];
            NewRow["卡片群組"] = dr[1];
            NewRow["安全庫存量"] = int.Parse(dr[2]);
            NewRow["最低庫存量"] = int.Parse(dr[3]);
            NewRow["補貨量"] = "350";
            NewRow["已補貨量"] = "350";
            dtResult.Rows.Add(NewRow);
        });
                
        return dtResult;
    }

    protected void btnNew_Click(object sender, EventArgs e)
    {
        gvMasterDV.DataSource = getMasterData();
        gvMasterDV.DataBind();
        gvMasterDV.AddNewRow();
    }
    protected void btnNew3_Click(object sender, EventArgs e)
    {
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {

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
        bindMasterData();
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

    protected void gvMaster_SelectedIndexChanged(object sender, EventArgs e)
    {
        GridView mygridview = sender as GridView;
        if (mygridview.SelectedIndex >= 0)
        {
            string KeyNo = mygridview.Rows[mygridview.SelectedIndex].Cells[5].Text;
            bindDetailData();
        }
    }

    protected void gvDetail_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        GridView gridview = sender as GridView;
        gridview.EditIndex = -1;
        //Bind原查詢資料
        DataTable dt = ViewState["gvDetail"] as DataTable;
        gridview.DataSource = dt;
        gridview.DataBind();
    }

    protected void gvDetail_RowEditing(object sender, GridViewEditEventArgs e)
    {
        GridView gridview = sender as GridView;
        //設定編輯欄位
        gridview.EditIndex = e.NewEditIndex;
        //Bind原查詢資料
        DataTable dt = ViewState["gvDetail"] as DataTable;
        gridview.DataSource = dt;
        gridview.DataBind();
    }

    protected void gvDetail_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        GridView gridview = sender as GridView;

        //取得資料

        //更新資料庫

        //取消編輯狀態
        gridview.EditIndex = -1;

        //Bind新資料(重取資料)
        bindDetailData();
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {

    }

    protected void btnAddNew_Click(object sender, EventArgs e)
    {

    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        bindMasterData();
    }

    
    protected void gvMasterDV_PageIndexChanged(object sender, EventArgs e)
    {

        DataTable dt = ViewState["gvMaster"] as DataTable;
        ((ASPxGridView)sender).DataSource = dt;
        ((ASPxGridView)sender).DataBind();
    }

    protected void gvMasterDV_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
    {
        ASPxGridView grid = (ASPxGridView)sender;
        DataTable dt = ViewState["gvMaster"] as DataTable ?? null;
        //DataRow[] DRSelf = dt.Select("項次='" + e.Keys[0].ToString().Trim() + "'");
        //if (DRSelf.Length > 0)
        //{

        //    DRSelf[0]["開始日期"] = e.NewValues["開始日期"].ToString();
        //    DRSelf[0]["結束日期"] = e.NewValues["結束日期"].ToString();
        //}
        grid.CancelEdit();
        e.Cancel = true;
        grid.DataSource = dt;
        grid.DataBind();

    }
    protected void gvDetail_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
    {
        ASPxGridView grid = (ASPxGridView)sender;
        DataTable dt = ViewState["gvDetail"] as DataTable ?? null;       
        grid.CancelEdit();
        e.Cancel = true;
        grid.DataSource = dt;
        grid.DataBind();
    }
    protected void gvMasterDV_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
    {
        ASPxGridView grid = (ASPxGridView)sender;
        DataTable dt = ViewState["gvMaster"] as DataTable ?? getMasterData();


        grid.CancelEdit();
        e.Cancel = true;
        grid.DataSource = dt;
        grid.DataBind();
        ViewState["gvMaster"] = dt;
    }
    protected void gvDetail_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
    {
        ASPxGridView grid = (ASPxGridView)sender;
        DataTable dt = ViewState["gvDetail"] as DataTable ?? getDetailData();


        grid.CancelEdit();
        e.Cancel = true;
        grid.DataSource = dt;
        grid.DataBind();
        ViewState["gvDetail"] = dt;
    }

    protected void gvMasterDV_FocusedRowChanged(object sender, EventArgs e)
    {
        if (gvMasterDV.FocusedRowIndex >= 0)
        {
            bindDetailData();
            ASPxPageControl1.Visible = true;
        }

    }

    protected void gvMasterDV_StartRowEditing(object sender, DevExpress.Web.Data.ASPxStartRowEditingEventArgs e)
    {

    }

    protected void gvMasterDV_HtmlRowPrepared(object sender, ASPxGridViewTableRowEventArgs e)
    {
        if (e.RowType == GridViewRowType.Data)
        {
            List<object> keyValues = this.gvMasterDV.GetSelectedFieldValues(gvMasterDV.KeyFieldName);
            foreach (string key in keyValues)
            {
                if (key == e.GetValue(gvMasterDV.KeyFieldName).ToString())
                {
                    if (key == gvMasterDV.GetRowValues(e.VisibleIndex, gvMasterDV.KeyFieldName).ToString())
                    {
                        e.Row.BackColor = System.Drawing.ColorTranslator.FromHtml("#999999");
                        e.Row.ForeColor = System.Drawing.ColorTranslator.FromHtml("#FFFFFF");
                    }
                    else
                    {
                        e.Row.BackColor = System.Drawing.ColorTranslator.FromHtml("#FFFFFF");
                        e.Row.ForeColor = System.Drawing.ColorTranslator.FromHtml("#000000");
                    }

                }
            }
        }
    }

    //protected void gvMasterDV_InitNewRow(object sender, DevExpress.Web.Data.ASPxDataInitNewRowEventArgs e)
    //{
    //    ASPxGridView gv = (ASPxGridView)sender;
    //    if (gv.IsNewRowEditing)
    //    {
    //        PopupControl pc = ((ASPxGridView)sender).FindChildControl<PopupControl>("PopupControl1");
    //        pc.Text = this.PopupControl1.Text;
    //    }
    //    //e.NewValues["項次"] = ASPxTextBox1.Text;
    //}
    protected void gvMasterDV_HtmlRowCreated(object sender, ASPxGridViewTableRowEventArgs e)
    {

    }

    protected void ASPxTextBox1_DataBound(object sender, EventArgs e)
    {
        //if (gvMasterDV.IsNewRowEditing)
        //{
        //    (sender as ASPxTextBox).Text = ASPxTextBox1.Text;
        //}
    }

    protected void grid_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
    {
        //若不使用DataSourceID時UPDATE INSERT 和DELPETE 都要自己控制 以下為UPDATE時需自行控制
        ASPxGridView grid = (ASPxGridView)sender;
        DataTable dt = ViewState["gvMaster"] as DataTable ?? null;

        grid.CancelEdit();
        e.Cancel = true;
        grid.DataSource = dt;
        grid.DataBind();
    }

    protected void gvDetail_PageIndexChanged(object sender, EventArgs e)
    {
        DataTable dt = ViewState["gvDetail"] as DataTable;
        ((ASPxGridView)sender).DataSource = dt;
        ((ASPxGridView)sender).DataBind();
    }
}
