using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Advtek.Utility;
using DevExpress.Web;
using DevExpress.Web.ASPxGridView;
using DevExpress.Web.ASPxPager;

public partial class VSS_OPT_OPT05 : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {

        if (!Page.IsPostBack)
        {
            gvMaster.DataSource = GetEmptyDataTable();
            gvMaster.DataBind();
        }
    }
    protected void bindMasterData()
    {
        DataTable dtGvMaster = new DataTable();
        dtGvMaster = getMasterData();
        ViewState["gvMaster"] = dtGvMaster;
        gvMaster.DataSource = dtGvMaster;
        gvMaster.DataBind();
    }
    protected void bindDetailData()
    {
        DataTable dtgvDetail = new DataTable();
        dtgvDetail = getDetailData();
        ViewState["gvDetail"] = dtgvDetail;
        gvDetail.DataSource = dtgvDetail;
        gvDetail.DataBind();
    }

    private DataTable GetEmptyDataTable()
    {
        DataTable dtResult = new DataTable();
        dtResult.Columns.Add("項次", typeof(string));
        dtResult.Columns.Add("門市編號", typeof(string));
        dtResult.Columns.Add("門市名稱", typeof(string));
        dtResult.Columns.Add("用途", typeof(string));
        dtResult.Columns.Add("所屬年月起", typeof(string));
        dtResult.Columns.Add("所屬年月訖", typeof(string));
        dtResult.Columns.Add("字軌", typeof(string));
        dtResult.Columns.Add("起始編號", typeof(string));
        dtResult.Columns.Add("終止編號", typeof(string));
        dtResult.Columns.Add("目前編號", typeof(string));
        dtResult.Columns.Add("目前開立號碼", typeof(string));
        dtResult.Columns.Add("發票張數", typeof(string));
        dtResult.Columns.Add("更新人員", typeof(string));
        dtResult.Columns.Add("更新日期", typeof(string));
        return dtResult;
    }

    private DataTable getMasterData()
    {
        DataTable dtMaster = new DataTable();
        dtMaster.Columns.Clear();
        dtMaster.Columns.Add("項次", typeof(string));
        dtMaster.Columns.Add("門市編號", typeof(string));
        dtMaster.Columns.Add("門市名稱", typeof(string));
        dtMaster.Columns.Add("用途", typeof(string));
        dtMaster.Columns.Add("所屬年月(起)", typeof(string));
        dtMaster.Columns.Add("所屬年月(訖)", typeof(string));
        dtMaster.Columns.Add("字軌", typeof(string));
        dtMaster.Columns.Add("起始編號", typeof(string));
        dtMaster.Columns.Add("終止編號", typeof(string));
        dtMaster.Columns.Add("目前編號", typeof(string));
        dtMaster.Columns.Add("目前開立號碼", typeof(string));
        dtMaster.Columns.Add("發票張數", typeof(string));
        dtMaster.Columns.Add("更新日期", typeof(string));
        dtMaster.Columns.Add("更新人員", typeof(string));

        string[] array1 = { "離線", "連線" };
        string[] array2 = { "現金", "信用卡", "禮券", "金融卡", "HappyGo" };



        //for (int i = 0; i < 6; i++)
        //{
        //    DataRow dtMasterRow = dtMaster.NewRow();
        //    dtMasterRow["項次"] =  i;
        //    dtMasterRow["門市編號"] = "門市編號" + i;
        //    dtMasterRow["門市名稱"] = "門市名稱" + i;
        //    dtMasterRow["用途"] = array1[i % 2];
        //    dtMasterRow["所屬年月(起)"] = "2010/07";
        //    dtMasterRow["所屬年月(訖)"] = "2010/08";
        //    dtMasterRow["字軌"] = "字軌" + i;
        //    dtMasterRow["起始編號"] = i + "00001";
        //    dtMasterRow["終止編號"] = i + "00204";
        //    dtMasterRow["目前編號"] = "目前編號" + i;
        //    dtMasterRow["目前開立號碼"] = "SW00124457" + i;
        //    dtMasterRow["發票張數"] = 100 * i + 20 * (i + 1);
        //    dtMasterRow["更新日期"] = DateTime.Now.AddDays(-i).AddMinutes(i * 32).ToString("yyyy/MM/dd");
        //    dtMasterRow["更新人員"] = "王小明";
        //    dtMaster.Rows.Add(dtMasterRow);
        //}


        DataRow dtMasterRow = dtMaster.NewRow();
        dtMasterRow["項次"] = "1";
        dtMasterRow["門市編號"] = "2101";
        dtMasterRow["門市名稱"] = "遠企";
        dtMasterRow["用途"] = "連線";
        dtMasterRow["所屬年月(起)"] = "2010/07";
        dtMasterRow["所屬年月(訖)"] = "2010/08";
        dtMasterRow["字軌"] = "QK";
        dtMasterRow["起始編號"] = "00000001";
        dtMasterRow["終止編號"] = "00000500";
        dtMasterRow["目前編號"] = "00000001";
        dtMasterRow["目前開立號碼"] = "QK00000001";
        dtMasterRow["發票張數"] = "500";
        dtMasterRow["更新日期"] = DateTime.Now.AddDays(-1).AddMinutes(1 * 32).ToString("yyyy/MM/dd HH:mm:ss");
        dtMasterRow["更新人員"] = "王小明";
        dtMaster.Rows.Add(dtMasterRow);

        dtMasterRow = dtMaster.NewRow();
        dtMasterRow["項次"] = "2";
        dtMasterRow["門市編號"] = "2101";
        dtMasterRow["門市名稱"] = "遠企";
        dtMasterRow["用途"] = "離線";
        dtMasterRow["所屬年月(起)"] = "2010/07";
        dtMasterRow["所屬年月(訖)"] = "2010/08";
        dtMasterRow["字軌"] = "QK";
        dtMasterRow["起始編號"] = "00000501";
        dtMasterRow["終止編號"] = "00001000";

        dtMasterRow["目前開立號碼"] = "QK00000501";
        dtMasterRow["發票張數"] = "500";
        dtMasterRow["更新日期"] = DateTime.Now.AddDays(-2).AddMinutes(2 * 32).ToString("yyyy/MM/dd HH:mm:ss");
        dtMasterRow["更新人員"] = "王小明";
        dtMaster.Rows.Add(dtMasterRow);

        dtMasterRow = dtMaster.NewRow();
        dtMasterRow["項次"] = "3";
        dtMasterRow["門市編號"] = "2101";
        dtMasterRow["門市名稱"] = "遠企";
        dtMasterRow["用途"] = "連線";
        dtMasterRow["所屬年月(起)"] = "2010/09";
        dtMasterRow["所屬年月(訖)"] = "2010/10";
        dtMasterRow["字軌"] = "QY";
        dtMasterRow["起始編號"] = "00001001";
        dtMasterRow["終止編號"] = "00001500";
        dtMasterRow["目前編號"] = "00001501";
        dtMasterRow["目前開立號碼"] = "QY00001001";
        dtMasterRow["發票張數"] = "500";
        dtMasterRow["更新日期"] = DateTime.Now.AddDays(-3).AddMinutes(3 * 32).ToString("yyyy/MM/dd HH:mm:ss");
        dtMasterRow["更新人員"] = "王小明";
        dtMaster.Rows.Add(dtMasterRow);

        dtMasterRow = dtMaster.NewRow();
        dtMasterRow["項次"] = "4";
        dtMasterRow["門市編號"] = "2101";
        dtMasterRow["門市名稱"] = "遠企";
        dtMasterRow["用途"] = "離線";
        dtMasterRow["所屬年月(起)"] = "2010/09";
        dtMasterRow["所屬年月(訖)"] = "2010/10";
        dtMasterRow["字軌"] = "QY";
        dtMasterRow["起始編號"] = "00001501";
        dtMasterRow["終止編號"] = "00002000";

        dtMasterRow["目前開立號碼"] = "QY00001501";
        dtMasterRow["發票張數"] = "500";
        dtMasterRow["更新日期"] = DateTime.Now.AddDays(-4).AddMinutes(4 * 32).ToString("yyyy/MM/dd HH:mm:ss");
        dtMasterRow["更新人員"] = "王小明";
        dtMaster.Rows.Add(dtMasterRow);

        return dtMaster;
    }

    private DataTable getDetailData()
    {
        DataTable gvDetail = new DataTable();
        gvDetail.Columns.Clear();
        gvDetail.Columns.Add("項次", typeof(string));
        gvDetail.Columns.Add("機台號碼", typeof(string));
        gvDetail.Columns.Add("起始編號", typeof(string));
        gvDetail.Columns.Add("終止編號", typeof(string));

        gvDetail.Columns.Add("張數", typeof(string));
        gvDetail.Columns.Add("發票分配日期", typeof(string));

        DataRow gvDetailRow = gvDetail.NewRow();
        gvDetailRow["項次"] = "1";
        gvDetailRow["機台號碼"] = "1";
        gvDetailRow["起始編號"] = "00000001";
        gvDetailRow["終止編號"] = "00000100";
        gvDetailRow["張數"] = 100;
        gvDetailRow["發票分配日期"] = "2010/07/01";
        gvDetail.Rows.Add(gvDetailRow);


        gvDetailRow = gvDetail.NewRow();
        gvDetailRow["項次"] = "2";
        gvDetailRow["機台號碼"] = "2";
        gvDetailRow["起始編號"] = "00000101";
        gvDetailRow["終止編號"] = "00000200";
        gvDetailRow["張數"] = "100";
        gvDetailRow["發票分配日期"] = "2010/07/01";
        gvDetail.Rows.Add(gvDetailRow);

        gvDetailRow = gvDetail.NewRow();
        gvDetailRow["項次"] = "3";
        gvDetailRow["機台號碼"] = "3";
        gvDetailRow["起始編號"] = "00000201";
        gvDetailRow["終止編號"] = "00000300";
        gvDetailRow["張數"] = "100";
        gvDetailRow["發票分配日期"] = "2010/07/01";
        gvDetail.Rows.Add(gvDetailRow);

        gvDetailRow = gvDetail.NewRow();
        gvDetailRow["項次"] = "4";
        gvDetailRow["機台號碼"] = "4";
        gvDetailRow["起始編號"] = "00000301";
        gvDetailRow["終止編號"] = "00000400";
        gvDetailRow["張數"] = "100";
        gvDetailRow["發票分配日期"] = "2010/07/01";
        gvDetail.Rows.Add(gvDetailRow);

        gvDetailRow = gvDetail.NewRow();
        gvDetailRow["項次"] = "5";
        gvDetailRow["機台號碼"] = "5";
        gvDetailRow["起始編號"] = "00000401";
        gvDetailRow["終止編號"] = "00000500";
        gvDetailRow["張數"] = "100";
        gvDetailRow["發票分配日期"] = "2010/07/01";
        gvDetail.Rows.Add(gvDetailRow);
        return gvDetail;
    }


    protected void btnSearch_Click(object sender, EventArgs e)
    {
        bindMasterData();

        //btnNew.Visible = true;
        //btnDelete.Visible = true;
        //btnImport0.Visible = true;
        //Div1.Visible = true;
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

    protected void gvMaster_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "select")
        {
            bindDetailData();
            this.gvDetail.Visible = true;

        }
    }
    protected void gvDetail_RowEditing(object sender, GridViewEditEventArgs e)
    {
        GridView grDetail = sender as GridView;
        //設定編輯欄位
        grDetail.EditIndex = e.NewEditIndex;
        //Bind原查詢資料
        DataTable dt = ViewState["gvDetail"] as DataTable;
        grDetail.DataSource = dt;
        grDetail.DataBind();
    }
    protected void gvDetail_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        GridView grDetail = sender as GridView;
        grDetail.EditIndex = -1;
        //Bind原查詢資料
        DataTable dt = ViewState["gvDetail"] as DataTable;
        grDetail.DataSource = dt;
        grDetail.DataBind();
    }
    protected void gvDetail_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        GridView grDetail = sender as GridView;


        //取得資料

        //更新資料庫

        //取消編輯狀態
        grDetail.EditIndex = -1;

        //Bind新資料(重取資料)
        bindDetailData();
    }

    //protected void CommandButton_Click(Object sender, CommandEventArgs e)
    //{

    //    gvMaster.Selection.UnselectAll();
    //    gvMaster.Selection.SetSelectionByKey(e.CommandArgument, true);
    //    gvMaster.SettingsBehavior.AllowFocusedRow = true;
    //    gvDetail.Visible = true;
    //    bindDetailData();
        
    //}

    #region gvMaster 新增/編輯/更新/取消 相關觸發事件
    /// <summary>
    /// 新增-顯示Footer
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        gvMaster.AddNewRow();
        //gvMaster.ShowFooter = true;
        //gvMaster.ShowFooterWhenEmpty = true;
        //System.Web.UI.HtmlControls.HtmlTableRow tr =
        //    gvMaster.FindChildControl<System.Web.UI.HtmlControls.HtmlTableRow>("trEmptyData");
        //if (tr != null)
        //{
        //    // 隱藏顯示文字訊息的表格列
        //    tr.Visible = false;
        //}
        //else
        //{
        //    // 重新繫結資料
        //    bindMasterData();
        //}
    }

    /// <summary>
    /// 取消新增-隱藏Footer
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        //gvMaster.ShowFooterWhenEmpty = false;
        //gvMaster.ShowFooter = false;
        //// 重新繫結資料
        //if (gvMaster.Rows.Count == 0)
        //{
        //    gvMaster.DataSource = GetEmptyDataTable();
        //    gvMaster.DataBind();
        //}
        //else
        //{
        //    bindMasterData();
        //}
    }

    protected void StoreNo_Click(object sender, EventArgs e)
    {
        //gvDetail.Visible = true;
        //bindDetailData();
    }

    protected void gvMaster_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowIndex != -1)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label ctrLbl = (Label)e.Row.Cells[5].FindControl("Label6");
                string testttttttt = ctrLbl.Text;


                //if (e.Row.Cells[5].Text == "離線")
                if (testttttttt == "離線")
                {
                    ////Button fix = (Button)e.Row.Cells[0].FindControl("btnfix");
                    ////fix.Visible = true;
                    ////fix.Enabled = true;
                    //Button btnView = (Button)e.Row.Cells[0].FindControl("Label2");
                    //btnView.Enabled = true;

                    LinkButton linBtn = (LinkButton)e.Row.Cells[3].FindControl("Label2");
                    linBtn.Enabled = true;
                }
                else
                {
                    //Button btnView = (Button)e.Row.Cells[0].FindControl("Label2");
                    //btnView.Enabled = false;
                    LinkButton linBtn = (LinkButton)e.Row.Cells[1].FindControl("Label2");
                    linBtn.Enabled = false;
                }
            }
        }
    }

    #endregion


    protected void gvMaster_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    /// <summary>
    /// The HtmlRowPrepared event is raised for each grid row (data row, group row, etc.) 
    /// within the ASPxGridView. 
    /// You can handle this event to change the style settings of individual rows.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void grid_HtmlRowPrepared(object sender, ASPxGridViewTableRowEventArgs e)
    {
        DevExpress.Web.ASPxGridView.GridViewDataTextColumn col = new GridViewDataTextColumn();
        DevExpress.Web.ASPxGridView.GridViewDataComboBoxColumn co2 = new GridViewDataComboBoxColumn();
        col = (GridViewDataTextColumn)((ASPxGridView)sender).Columns["門市編號"];
        co2 = (GridViewDataComboBoxColumn)((ASPxGridView)sender).Columns["用途"];
        HyperLink hl = gvMaster.FindRowCellTemplateControl(e.VisibleIndex, col, "Label2") as HyperLink;
        Label rb = gvMaster.FindRowCellTemplateControl(e.VisibleIndex, co2, "Label6") as Label;
        if (hl != null)
        {
            if (rb.Text == "離線")
            {
                hl.Enabled = false;
            }
        }
        //  hl.Enabled = (e.CellValue == "離線") ? true:false;

        if (e.RowType == GridViewRowType.Data)
        {
            List<object> keyValues = this.gvMaster.GetSelectedFieldValues(gvMaster.KeyFieldName);
            foreach (string key in keyValues)
            {
                if (key == e.GetValue(gvMaster.KeyFieldName).ToString())
                {
                    if (key == this.hdNo.Value)
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

    protected void grid_HtmlRowCreated(object sender, ASPxGridViewTableRowEventArgs e)
    {
        //if (e.RowType == GridViewRowType.Detail)
        //{
        //    // 繫結明細資料表           
        //    ASPxGridView detailGrid = e.Row.FindChildControl<ASPxGridView>("detailGrid");
        //    detailGrid.DataSource = GetDetailData();
        //    detailGrid.DataBind();
        //}
        //取得控制項裏的值出來


    }

    /// <summary>
    /// 主GridView的分頁變更事件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void grid_PageIndexChanged(object sender, EventArgs e)
    {
        ASPxGridView grid = sender as ASPxGridView;       
        grid.DataSource = getMasterData();
        grid.DataBind();
    }

    protected void grid_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
    {
        ASPxGridView grid = (ASPxGridView)sender;
        grid.CancelEdit();
        e.Cancel = true;
        grid.DataSource = getMasterData();
        grid.DataBind();

    }

    protected void grid_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
    {
        //若不使用DataSourceID時UPDATE INSERT 和DELPETE 都要自己控制 以下為UPDATE時需自行控制
        ASPxGridView grid = (ASPxGridView)sender;
        DataTable dt = ViewState["gvMaster"] as DataTable ?? null;
        //DataRow[] DRSelf = dt.Select("項次='" +  e.Keys[0].ToString().Trim() + "'");
        //if (DRSelf.Length > 0)
        //{

        //    DRSelf[0]["類別"] = e.NewValues["類別"];
        //    DRSelf[0]["兑點代號"] = e.NewValues["兑點代號"];
        //    DRSelf[0]["兑點名稱"] = e.NewValues["兑點名稱"];
        //    DRSelf[0]["開始日期"] = e.NewValues["開始日期"];
        //    DRSelf[0]["結束日期"] = e.NewValues["結束日期"];
        //    DRSelf[0]["點數"] = e.NewValues["點數"];
        //    DRSelf[0]["兑換金額"] = e.NewValues["兑換金額"];
        //}
        grid.CancelEdit();
        e.Cancel = true;
        grid.DataSource = dt;
        grid.DataBind();
    }
    protected void gvMaster_HtmlDataCellPrepared(object sender, ASPxGridViewTableDataCellEventArgs e)
    {
        DevExpress.Web.ASPxGridView.GridViewDataTextColumn col = new GridViewDataTextColumn();
        DevExpress.Web.ASPxGridView.GridViewDataComboBoxColumn co2 = new GridViewDataComboBoxColumn();
        col = (GridViewDataTextColumn)((ASPxGridView)sender).Columns["門市編號"];
        co2 = (GridViewDataComboBoxColumn)((ASPxGridView)sender).Columns["用途"];
        LinkButton hl = gvMaster.FindRowCellTemplateControl(e.VisibleIndex, col, "Label2") as LinkButton;
        Label rb = gvMaster.FindRowCellTemplateControl(e.VisibleIndex, co2, "Label6") as Label;
        if (hl != null)
        {
            if (rb.Text == "離線")
            {
                hl.Enabled = true;
            }
            else
                hl.Enabled = false;
        }
    }

    protected void gvMaster_FocusedRowChanged(object sender, EventArgs e)
    {
        if (gvMaster.FocusedRowIndex >= 0)
        {
            this.hdNo.Value = gvMaster.GetRowValues(gvMaster.FocusedRowIndex, gvMaster.KeyFieldName).ToString();

            gvDetail.Visible = true;
            bindDetailData();
        }
    }
}
