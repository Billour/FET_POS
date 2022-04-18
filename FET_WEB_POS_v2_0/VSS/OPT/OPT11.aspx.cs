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
using DevExpress.Web.ASPxEditors;

public partial class VSS_OPT11_OPT11 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack && !IsCallback)
        {
            // 繫結空的資料表，以顯示表頭欄位
            gvMaster.DataSource = GetEmptyDataTable();
            gvMaster.DataBind();
            //grid.DetailRows.ExpandRow(0);
        }

    }

    protected void bindMasterData( )
    {
        DataTable dtGvMaster = new DataTable();
        dtGvMaster = GetMasterData();
        ViewState["gvMaster"] = dtGvMaster;
        gvMaster.DataSource = dtGvMaster;
        gvMaster.DataBind();
    }

    private DataTable GetEmptyDataTable()
    {
        DataTable dtMaster = new DataTable();
        dtMaster.Columns.Clear();
        dtMaster.Columns.Add("項次", typeof(int));
        dtMaster.Columns.Add("類別", typeof(string));
        dtMaster.Columns.Add("兑點代號", typeof(string));
        dtMaster.Columns.Add("兑換金額", typeof(string));
        dtMaster.Columns.Add("兑點名稱", typeof(string));
        dtMaster.Columns.Add("開始日期", typeof(string));
        dtMaster.Columns.Add("結束日期", typeof(string));
        dtMaster.Columns.Add("更新人員", typeof(string));
        dtMaster.Columns.Add("更新日期", typeof(string));
        dtMaster.Columns.Add("點數", typeof(string));
        return dtMaster;
    }

    private DataTable GetMasterData( )
    {
        DataTable dtMaster = GetEmptyDataTable();

        string [] ary1= {"代收","銷售"};
        string[] ary2 = { "A01","A02","A03","A04","S01","S02","S03","S04"};
        string[] ary3 = { "180點兌換50元", "350點兌換100元", "1000點兌換350元", "1700點兌換500元", "180點兌換50元", "350點兌換100元", "1700點兌換500元", "180點兌換50元" };
        string[] ary4 = { "180", "350", "1000", "1700", "180", "350" };
        string[] ary5 = { "50", "100", "350", "500" };

        for (int i = 1; i < 7; i++)
        {
            DataRow dtMasterRow = dtMaster.NewRow();
            dtMasterRow["項次"] = i;
            dtMasterRow["類別"] = ary1[i%2];
            dtMasterRow["兑點代號"] = ary2[i % 8];
            dtMasterRow["兑換金額"] = ary5[i % 4];
            dtMasterRow["兑點名稱"] =   ary3[i % 8];
            dtMasterRow["點數"] = ary4[i % 6];
            dtMasterRow["開始日期"] = "2010/09/01";
            dtMasterRow["結束日期"] = "2010/09/01";
            dtMasterRow["更新人員"] = "王大明";

            dtMasterRow["更新日期"] = Convert.ToDateTime("2010/09/01" + DateTime.Now.ToLongTimeString()).ToString("yyyy/MM/dd HH:mm:ss");
 
            dtMaster.Rows.Add(dtMasterRow);
        }

       
        return dtMaster;

    }

    protected void btnAddNew_Click(object sender, EventArgs e)
    {
        gvMaster.AddNewRow();
    }


    #region gvMaster 編輯/更新/取消 相關觸發事件
    protected void Grid_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
    {
        //GridPageSize = int.Parse(e.Parameters);
        gvMaster.SettingsPager.PageSize = int.Parse(e.Parameters);
        gvMaster.DataBind();
    }

    protected void detailGrid_DataSelect(object sender, EventArgs e)
    {
        //Session["項次"] = (sender as ASPxGridView).GetMasterRowKeyValue();
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
        grid.DataSource = GetMasterData();
        grid.DataBind();
    }

    /// <summary>
    /// 明細GridView的分頁變更事件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void detailGrid_PageIndexChanged(object sender, EventArgs e)
    {
        //ASPxGridView grid1 = sender as ASPxGridView;
        //grid1.DataSource = GetDetailData();
        //grid1.DataBind();
    }

    protected void grid_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
    {
        //若不使用DataSourceID時UPDATE INSERT 和DELPETE 都要自己控制 以下為UPDATE時需自行控制
        ASPxGridView grid = (ASPxGridView)sender;
        DataTable dt = ViewState["gvMaster"] as DataTable ?? null;
        gvMaster.CancelEdit();
        e.Cancel = true;
        grid.DataSource = dt;
        grid.DataBind();
        ////e.NewValues["TemplateID"] = Guid.NewGuid(); // I set this PK myself.
    }

    protected void grid_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
    {
        //若不使用DataSourceID時UPDATE INSERT 和DELPETE 都要自己控制 以下為UPDATE時需自行控制
        ASPxGridView grid = (ASPxGridView)sender;
        DataTable dt = ViewState["gvMaster"] as DataTable ?? null;
        //// DataRow[] DRSelf = dt.Select("項次='" + e.Keys[0].ToString().Trim() + "'");
        // if (DRSelf.Length > 0)
        // {

        //     DRSelf[0]["類別"] = e.NewValues["類別"];
        //     DRSelf[0]["兑點代號"] = e.NewValues["兑點代號"];
        //     DRSelf[0]["兑點名稱"] = e.NewValues["兑點名稱"];
        //     DRSelf[0]["開始日期"] = e.NewValues["開始日期"];
        //     DRSelf[0]["結束日期"] = e.NewValues["結束日期"];
        //     DRSelf[0]["點數"] = e.NewValues["點數"];
        //     DRSelf[0]["兑換金額"] = e.NewValues["兑換金額"];
        // }
        gvMaster.CancelEdit();
        e.Cancel = true;
        grid.DataSource = dt;
        grid.DataBind();
    }
    #endregion

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        Session["DataSource"] = GetMasterData();
        // 繫結主要的資料表
        bindMasterData();
    }

}
