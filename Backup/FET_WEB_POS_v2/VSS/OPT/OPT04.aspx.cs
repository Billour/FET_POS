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

public partial class VSS_OPT_OPT04 : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            string[] ary2 = { "遠東百貨", "愛買", "Sogo" };
            ddlCompany.DataSource = ary2;
            ddlCompany.DataBind();
            ddlCompany.SelectedIndex = 0;

            gvMaster.DataSource = GetEmptyDataTable();
            gvMaster.DataBind();
            //bindMasterData();
            //btnNew.Visible = true;
            //btnDelete.Visible = true;
            //Div1.Visible = true;
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

    private DataTable GetEmptyDataTable()
    {
        DataTable dtResult = new DataTable();
        dtResult.Columns.Add("項次", typeof(string));
        dtResult.Columns.Add("狀態", typeof(string));
        dtResult.Columns.Add("公司別", typeof(string));
        dtResult.Columns.Add("禮券名稱", typeof(string));
        dtResult.Columns.Add("手續費", typeof(string));
        dtResult.Columns.Add("是否輸入禮券序號", typeof(string));
        dtResult.Columns.Add("開始日期", typeof(string));
        dtResult.Columns.Add("結束日期", typeof(string));
        dtResult.Columns.Add("更新日期", typeof(string));
        dtResult.Columns.Add("更新人員", typeof(string));
        return dtResult;
    }

    private DataTable getMasterData()
    {
        DataTable dtMaster = new DataTable();
        dtMaster.Columns.Clear();
        dtMaster.Columns.Add("項次", typeof(string));
        dtMaster.Columns.Add("狀態", typeof(string));
        dtMaster.Columns.Add("公司別", typeof(string));
        dtMaster.Columns.Add("禮券名稱", typeof(string));
        dtMaster.Columns.Add("手續費", typeof(string));
        dtMaster.Columns.Add("是否輸入禮券序號", typeof(string));
        dtMaster.Columns.Add("開始日期", typeof(string));
        dtMaster.Columns.Add("結束日期", typeof(string));
        dtMaster.Columns.Add("更新日期", typeof(string));
        dtMaster.Columns.Add("更新人員", typeof(string));

        string[] array1 = { "有效", "過期", "已停止" };
        string[] array2 = { "現金", "信用卡", "禮券", "金融卡", "HappyGo" };
        string[] array3 = { "遠東百貨", "愛買", "Sogo" };
        string[] array4 = { "酬賓禮券", "高級禮券" };
        for (int i = 1; i < 6; i++)
        {
            DataRow dtMasterRow = dtMaster.NewRow();
            dtMasterRow["項次"] = i;
            dtMasterRow["狀態"] = array1[i % 3];
            dtMasterRow["公司別"] = array3[i % 3];
            dtMasterRow["禮券名稱"] = array4[i % 2];
            dtMasterRow["手續費"] = "0." + i;
            dtMasterRow["開始日期"] = "2010/08/01";
            if (i == 2) dtMasterRow["結束日期"] = "";
            else dtMasterRow["結束日期"] = "2010/12/31";
            dtMasterRow["更新日期"] = DateTime.Now.AddDays(-i).AddMinutes(i * 32).ToString("yyyy/MM/dd hh:mm:ss");
            dtMasterRow["是否輸入禮券序號"] = ((i % 2) == 0) ? true : false;
            dtMasterRow["更新人員"] = "王小明";
            dtMaster.Rows.Add(dtMasterRow);
        }
        return dtMaster;
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        bindMasterData();
        //btnNew.Visible = true;
        //btnDelete.Visible = true;
        //Div1.Visible = true;
    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        gvMaster.AddNewRow();
    }

    protected void gvMaster_RowEditing(object sender, GridViewEditEventArgs e)
    {
        //GridView gridview = sender as GridView;
        ////設定編輯欄位
        //gridview.EditIndex = e.NewEditIndex;
        ////Bind原查詢資料
        //DataTable dt = ViewState["gvMaster"] as DataTable;
        //gridview.DataSource = dt;
        //gridview.DataBind();
    }
    protected void gvMaster_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        //GridView gridview = sender as GridView;

        ////取得資料

        ////更新資料庫

        ////取消編輯狀態
        //gridview.EditIndex = -1;

        ////Bind新資料(重取資料)
        //bindMasterData();
    }
    protected void gvMaster_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        //GridView gridview = sender as GridView;
        //gridview.EditIndex = -1;
        ////Bind原查詢資料
        //DataTable dt = ViewState["gvMaster"] as DataTable;
        //gridview.DataSource = dt;
        //gridview.DataBind();
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

    protected void grid_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
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
