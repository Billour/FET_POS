using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Advtek.Utility;
using System.Collections;
using DevExpress.Web;
using DevExpress.Web.ASPxGridView;
using DevExpress.Web.ASPxPager;

public partial class VSS_OPT_OPT15 : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack && !IsCallback)
        {
            // 繫結空的資料表，以顯示表頭欄位
            gvMaster.DataSource = GetEmptyDataTable();
            gvMaster.DataBind();

        }
    }
    protected void bindMasterData()
    {
        DataTable dtResult = new DataTable();
        dtResult = GetMasterData();
        ViewState["gvMaster"] = dtResult;
        gvMaster.DataSource = dtResult;
        gvMaster.DataBind();

    }

    private DataTable GetEmptyDataTable()
    {
        DataTable dtResult = new DataTable();
        dtResult.Columns.Add("折扣料號", typeof(string));
        dtResult.Columns.Add("折扣名稱", typeof(string));
        dtResult.Columns.Add("開始日期", typeof(DateTime));
        dtResult.Columns.Add("結束日期", typeof(DateTime));
        dtResult.Columns.Add("類別", typeof(string));
        dtResult.Columns.Add("商品料號", typeof(string));
        dtResult.Columns.Add("點數", typeof(int));
        dtResult.Columns.Add("名單檢核", typeof(bool));
        dtResult.Columns.Add("兌換次數", typeof(int));
        dtResult.Columns.Add("更新日期", typeof(string));
        dtResult.Columns.Add("更新人員", typeof(string));
        return dtResult;
    }

    private DataTable GetMasterData()
    {
        DataTable dtResult = GetEmptyDataTable();

        string[] ary1 = { "點數", "商品" };


        for (int i = 0; i < 6; i++)
        {
            DataRow NewRow = dtResult.NewRow();
            NewRow["折扣料號"] = "00" + i;
            NewRow["折扣名稱"] = "折扣名稱" + i;
            NewRow["開始日期"] = DateTime.Today.Date.AddDays(-10);
            NewRow["結束日期"] = DateTime.Today.Date;
            NewRow["類別"] = ary1[i % 2];
            NewRow["商品料號"] = (Convert.ToString(NewRow["類別"]) == "點數") ? " " : "15720000" + i;
            NewRow["點數"] = (Convert.ToString(NewRow["類別"]) == "商品") ? 0 : (10 * i);
            NewRow["名單檢核"] = (i % 2 == 0);
            NewRow["兌換次數"] = 1;
            NewRow["更新日期"] = DateTime.Now.AddDays(-i).AddMinutes(i * 32).ToString("yyyy/MM/dd HH:mm:ss");
            NewRow["更新人員"] = "王大同";
            dtResult.Rows.Add(NewRow);
        }

        return dtResult;
    }

    private DataTable GetDetailData()
    {
        DataTable dtResult = new DataTable();
        dtResult.Columns.Add("項次", typeof(int));
        dtResult.Columns.Add("門市編號", typeof(string));
        dtResult.Columns.Add("門市名稱", typeof(string));
        dtResult.Columns.Add("區域別", typeof(string));

        DataRow NewRow = dtResult.NewRow();
        NewRow["項次"] = 1;
        NewRow["門市編號"] = "2103";
        NewRow["門市名稱"] = "永和門市";
        NewRow["區域別"] = "北一區";
        dtResult.Rows.Add(NewRow);

        NewRow = dtResult.NewRow();
        NewRow["項次"] = 2;
        NewRow["門市編號"] = "2105";
        NewRow["門市名稱"] = "中和門市";
        NewRow["區域別"] = "北二區";

        dtResult.Rows.Add(NewRow);
        return dtResult;
    }


    protected void btnSearch_Click(object sender, EventArgs e)
    {
        Session["gvMaster"] = GetMasterData();
        // 繫結主要的資料表
        bindMasterData();
    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        gvMaster.AddNewRow();
    }
    protected void btnAdd_Click_dt(object sender, EventArgs e)
    {
        //ASPxGridView grid = (sender as WebControl).NamingContainer.FindChildControl<ASPxGridView>("gvDetail");
        gvDetail.AddNewRow();
    }

    protected void Grid_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
    {
        //GridPageSize = int.Parse(e.Parameters);
        gvMaster.SettingsPager.PageSize = int.Parse(e.Parameters);
        gvMaster.DataBind();
    }

    protected void detailGrid_DataSelect(object sender, EventArgs e)
    {
        //Session["活動代號"] = (sender as ASPxGridView).GetMasterRowKeyValue();
    }

    /// <summary>
    /// The HtmlRowPrepared event is raised for each grid row (data row, group row, etc.) 
    /// within the ASPxGridView. 
    /// You can handle this event to change the style settings of individual rows.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gvMaster_HtmlRowPrepared(object sender, ASPxGridViewTableRowEventArgs e)
    {
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

    protected void gvMaster_HtmlRowCreated(object sender, ASPxGridViewTableRowEventArgs e)
    {
        //if (e.RowType == GridViewRowType.Detail)
        //{
        //    // 繫結明細資料表
        //    ASPxGridView detailGrid = e.Row.FindChildControl<ASPxGridView>("gvDetail");
        //    detailGrid.DataSource = GetDetailData();
        //    detailGrid.DataBind();
        //}
    }

    //protected void lbtnActivityNo_Click(object sender, EventArgs e)
    //{
    //    //LinkButton KeyNo = (LinkButton)sender;
    //    //gvDetail.DataSource = GetDetailData(KeyNo.Text);

    //    ViewState["gvDetail"] = GetDetailData();
    //    gvDetail.DataSource = (DataTable)ViewState["gvDetail"];
    //    gvDetail.DataBind();
    //    ASPxPageControl1.Visible = true;
    //}

    protected void ASPxButton1_Click(object sender, EventArgs e)
    {
        Response.Redirect("OPT16.aspx");
    }

    #region gvMaster 編輯/更新/取消 相關觸發事件
    protected void gvMaster_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
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

    protected void gvMaster_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
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

    /// <summary>
    /// 主GridView的分頁變更事件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gvMaster_PageIndexChanged(object sender, EventArgs e)
    {
        ASPxGridView grid = sender as ASPxGridView;
        grid.DataSource = GetMasterData();
        grid.DataBind();
    }

    #endregion

    #region gvDetail 編輯/更新/取消 相關觸發事件
    protected void gvDetail_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
    {
        //若不使用DataSourceID時UPDATE INSERT 和DELPETE 都要自己控制 以下為UPDATE時需自行控制
        ASPxGridView grid = (ASPxGridView)sender;
        DataTable dt = ViewState["gvDetail"] as DataTable ?? null;
        gvDetail.CancelEdit();
        e.Cancel = true;
        grid.DataSource = dt;
        grid.DataBind();
        ////e.NewValues["TemplateID"] = Guid.NewGuid(); // I set this PK myself.
    }

    protected void gvDetail_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
    {
        //若不使用DataSourceID時UPDATE INSERT 和DELPETE 都要自己控制 以下為UPDATE時需自行控制
        ASPxGridView grid = (ASPxGridView)sender;
        DataTable dt = ViewState["gvDetail"] as DataTable ?? null;
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
        gvDetail.CancelEdit();
        e.Cancel = true;
        grid.DataSource = dt;
        grid.DataBind();
    }

    /// <summary>
    /// 明細GridView的分頁變更事件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void detailGrid_PageIndexChanged(object sender, EventArgs e)
    {
        ASPxGridView grid1 = sender as ASPxGridView;
        grid1.DataSource = GetDetailData();
        grid1.DataBind();
    }


    #endregion




    protected void gvMaster_FocusedRowChanged(object sender, EventArgs e)
    {
        if (gvMaster.FocusedRowIndex >= 0)
        {
            this.hdNo.Value = gvMaster.GetRowValues(gvMaster.FocusedRowIndex, gvMaster.KeyFieldName).ToString();

            ViewState["gvDetail"] = GetDetailData();
            gvDetail.DataSource = (DataTable)ViewState["gvDetail"];
            gvDetail.DataBind();
            ASPxPageControl1.Visible = true;
        }
    }
}
