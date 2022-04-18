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

public partial class VSS_LOG_LOG05 : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            gvMaster.DataSource = GetEmptyDataTable();
            gvMaster.DataBind();
            this.btnSave.Visible = false;
            this.Button2.Visible = false;
          
        }
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
        dtResult.Columns.Add("狀態", typeof(string));
        dtResult.Columns.Add("系統別", typeof(string));
        dtResult.Columns.Add("模組名稱", typeof(string));
        dtResult.Columns.Add("功能代碼", typeof(string));
        dtResult.Columns.Add("功能名稱", typeof(string));
        dtResult.Columns.Add("URL", typeof(string));
        dtResult.Columns.Add("更新日期", typeof(string));
        dtResult.Columns.Add("更新人員", typeof(string));
        return dtResult;
    }

    private DataTable getMasterData()
    {
        DataTable dtResult = GetEmptyDataTable();

        //string[] status = { "有效", "已失效" };
        Random rnd = new Random();
        for (int i = 1; i <= 11; i++)
        {
            DataRow NewRow = dtResult.NewRow();
            NewRow["狀態"] = "有效";
            NewRow["系統別"] = "Online";
            NewRow["模組名稱"] = "模組" + rnd.Next(1, 5);
            NewRow["功能代碼"] = "功能代碼" + i;
            NewRow["功能名稱"] = "功能名稱" + i;
            NewRow["URL"] = "www.google.com";
            NewRow["更新日期"] = Convert.ToDateTime(DateTime.Now).ToString("yyyy/MM/dd HH:mm:ss");
            NewRow["更新人員"] = "更新人員" +i;
            dtResult.Rows.Add(NewRow);
        }
        return dtResult;
    }

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
        //{'
        //    gvMaster.DataSource = GetEmptyDataTable();
        //    gvMaster.DataBind();
        //}
        //else
        //{
        //    bindMasterData();
        //}
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
    #endregion
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        bindMasterData();
        //this.div1.Visible = true;
        this.btnSave.Visible = true;
        this.Button2.Visible = true;
    }
    protected void btnNew_Click(object sender, EventArgs e)
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
        /*
        if (e.RowType == GridViewRowType.Detail)
        {
            // 繫結明細資料表           
            ASPxGridView detailGrid = e.Row.FindChildControl<ASPxGridView>("detailGrid");
            detailGrid.DataSource = GetDetailData();
            detailGrid.DataBind();            
        }
        */
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
    protected void grid_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
    {
        //若不使用DataSourceID時UPDATE INSERT 和DELPETE 都要自己控制 以下為UPDATE時需自行控制
        ASPxGridView grid = (ASPxGridView)sender;
        DataTable dt = ViewState["gvMaster"] as DataTable ?? null;
        //DataRow[] DRSelf = dt.Select("項次='" +  StringUtil.CStr(e.Keys[0]).Trim() + "'");
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

   
}
