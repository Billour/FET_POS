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

public partial class VSS_LOG_LOG03 : System.Web.UI.Page
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

        gvMaster.DataSource = dtResult;
        gvMaster.DataBind();
    }
    protected void bindData()
    {

        gvMaster.DataSource = getMasterData();
        gvMaster.DataBind();
    }
    private DataTable getData3()
    {
        DataTable dtResult = new DataTable();
        dtResult.Columns.Add("門市編號", typeof(string));
        dtResult.Columns.Add("門市名稱", typeof(string));
        dtResult.Columns.Add("員工編號", typeof(string));
        dtResult.Columns.Add("員工姓名", typeof(string));



        DataRow NewRow = dtResult.NewRow();
        NewRow["門市編號"] = "NU001";
        NewRow["門市名稱"] = "內湖門市";
        NewRow["員工編號"] = "1234567";
        NewRow["員工姓名"] = "王小寶";
        dtResult.Rows.Add(NewRow);


        return dtResult;
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
        dtResult.Columns.Add("順序", typeof(string));
        dtResult.Columns.Add("角色代碼", typeof(string));
        dtResult.Columns.Add("角色名稱", typeof(string));
        dtResult.Columns.Add("系統別", typeof(string));
        dtResult.Columns.Add("模組名稱", typeof(string));
        dtResult.Columns.Add("功能代碼", typeof(string));
        dtResult.Columns.Add("功能名稱", typeof(string));
        dtResult.Columns.Add("URL", typeof(string));
        

        string[] array1 = { "區經理", "店長", "店員"};
        string[] array2 = { "銷售交易查詢", "系統參數設定", "HG點數兌換-來店禮" };

        for (int i = 0; i <= 10; i++)
        {
            DataRow NewRow = dtResult.NewRow();
            NewRow["順序"] = i;
            NewRow["角色代碼"] = "角色代碼" + i;
            NewRow["角色名稱"]= array1 [i % 3];
            NewRow["系統別"] = "系統別" + i;
            NewRow["模組名稱"] = "模組名稱" + i;
            NewRow["功能代碼"] = "功能代碼" + i;
            NewRow["功能名稱"] = array2[i % 3];
            NewRow["URL"] = "url" + i;
         
            dtResult.Rows.Add(NewRow);
        }
        return dtResult;
    }

    #region gvMaster 編輯/更新/取消 相關觸發事件
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
    #endregion

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        bindMasterData();
    }
    protected void Button11_Click(object sender, EventArgs e)
    {
        bindData();
    }

    protected void PermissionsHiddenField_ValueChanged(Object sender, EventArgs e)
    {
        bindData();
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
    
}
