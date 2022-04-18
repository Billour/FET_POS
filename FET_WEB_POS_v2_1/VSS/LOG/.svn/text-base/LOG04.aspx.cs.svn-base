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

public partial class VSS_LOG_LOG04 : BasePage
{

    private string[] ary = { "請選擇", "系統管理", "基本資料設定", "日結管理", "庫存管理", "訂貨管理", "寄銷管理", "預購管理", "銷售管理", "租賃管理", "商品管理" };


    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {

            //ary1();
            DropDownList2.DataSource = ary;
            DropDownList2.DataBind();
            DropDownList2.SelectedIndex = 0;

            gvMaster.DataSource = GetEmptyDataTable();
            gvMaster.DataBind();
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
        dtResult.Columns.Add("參數分類", typeof(string));
        dtResult.Columns.Add("參數代碼", typeof(string));
        dtResult.Columns.Add("參數名稱", typeof(string));
        dtResult.Columns.Add("值", typeof(string));
        dtResult.Columns.Add("備註說明", typeof(string));
        dtResult.Columns.Add("更新日期", typeof(string));
        dtResult.Columns.Add("更新人員", typeof(string));
        return dtResult;
    }

    private DataTable getMasterData()
    {
        DataTable dtResult = GetEmptyDataTable();



        for (int i = 1; i <= 10; i++)
        {
            DataRow NewRow = dtResult.NewRow();
            NewRow["狀態"] = i;
            NewRow["參數分類"] = ary[i];
            NewRow["參數代碼"] = "參數代碼" + i;
            NewRow["參數名稱"] = "參數名稱" + i;
            NewRow["值"] = "值" + i;
            NewRow["備註說明"] = "備註說明" + i;
            NewRow["更新日期"] = Convert.ToDateTime(DateTime.Now).ToString("yyyy/MM/dd HH:mm:ss");
            NewRow["更新人員"] = "汪曉民";
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
        //    System.Web.UI.HtmlControls.HtmlTableRow tr =
        //        gvMaster.FindChildControl<System.Web.UI.HtmlControls.HtmlTableRow>("trEmptyData");
        //    if (tr != null)
        //    {
        //        // 隱藏顯示文字訊息的表格列
        //        tr.Visible = false;
        //    }
        //    else
        //    {
        //        // 重新繫結資料
        //        bindMasterData();
        //    }
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

    //protected void gvMaster_StartRowEditing(object sender, DevExpress.Web.Data.ASPxStartRowEditingEventArgs e)
    //{

    //    //string[] ary1 = { "請選擇", "系統管理", "基本資料設定", "日結管理", "庫存管理", "訂貨管理", "寄銷管理", "預購管理", "銷售管理", "租賃管理", "商品管理" };



    //    if (gvMaster.IsEditing)
    //    {
            
            
    //        GridViewDataComboBoxColumn h1 = (GridViewDataComboBoxColumn)gvMaster.Columns[1];
    //        h1.PropertiesComboBox.DataSource = ary;
            
            
            
          
    //    }
    //}
    protected void gvMaster_CellEditorInitialize(object sender, ASPxGridViewEditorEventArgs e)
    {
        if (e.Column.FieldName=="參數分類")
        {
            ASPxComboBox h1 = e.Editor as ASPxComboBox;
            h1.DataSource = ary;
            h1.DataBind();
        }
    }
}
