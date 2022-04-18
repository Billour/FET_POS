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

public partial class VSS_OPT_OPT13_1 : BasePage
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
        dtResult.Columns.Add("促銷代號", typeof(string));
        dtResult.Columns.Add("促銷名稱", typeof(string));
        dtResult.Columns.Add("開始日期", typeof(DateTime));
        dtResult.Columns.Add("結束日期", typeof(DateTime));
        dtResult.Columns.Add("名單檢核", typeof(bool));
        dtResult.Columns.Add("折抵方式", typeof(string));
        dtResult.Columns.Add("折抵上限", typeof(int));
        dtResult.Columns.Add("折抵次數", typeof(int));
        dtResult.Columns.Add("更新日期", typeof(string));
        dtResult.Columns.Add("更新人員", typeof(string));
        return dtResult;
    }

    private DataTable GetMasterData()
    {
        DataTable dtResult = GetEmptyDataTable();
        string[] ary1 = { "單商品", "促銷活動" };
        string[] arr = { "500", "5%" };
        Random rnd = new Random();

        for (int i = 1; i <= 6; i++)
        {
            DataRow NewRow = dtResult.NewRow();
            NewRow["折扣料號"] = "00" + i;
            NewRow["折扣名稱"] = "折扣名稱" + i;
            NewRow["促銷代號"] = rnd.Next(1000, 9999).ToString() + rnd.Next(1000, 9999).ToString();
            NewRow["促銷名稱"] = "促銷" + Convert.ToChar(64 + i);
            NewRow["開始日期"] = DateTime.Today.Date.AddDays(-10);
            NewRow["結束日期"] = DateTime.Today.Date;
            NewRow["名單檢核"] = ((i % 2) == 0);
            NewRow["折抵方式"] = arr[rnd.Next(1, 10) % 2];
            NewRow["折抵上限"] = 100;
            NewRow["折抵次數"] = rnd.Next(1, 10);
            NewRow["更新日期"] = DateTime.Now.AddDays(-i).AddMinutes(i * 32).ToString("yyyy/MM/dd HH:mm:ss");
            NewRow["更新人員"] = "王大同";
            dtResult.Rows.Add(NewRow);
        }

        return dtResult;
    }

    private DataTable GetEmptyDataTable1()
    {
        DataTable dtResult = new DataTable();
        dtResult.Columns.Add("項次", typeof(int));
        dtResult.Columns.Add("兌點名稱", typeof(string));
        dtResult.Columns.Add("點數", typeof(int));
        dtResult.Columns.Add("兌換金額", typeof(int));
        return dtResult;
    }

    protected void bindGridView1Data()
    {
        ViewState["gvDetail1"] = getGridView1Data();
        gvDetail1.DataSource = (DataTable)ViewState["gvDetail1"];
        gvDetail1.DataBind();
    }

    protected void bindGridView2Data()
    {
        ViewState["gvDetail2"] = getGridView2Data();
        gvDetail2.DataSource = (DataTable)ViewState["gvDetail2"];
        gvDetail2.DataBind();
    }

    protected void bindGridView3Data()
    {
        ViewState["gvDetail3"] = getGridView3Data();
        gvDetail3.DataSource = (DataTable)ViewState["gvDetail3"];
        gvDetail3.DataBind();
    }

    private DataTable getGridView1Data()
    {
        DataTable dtResult = GetEmptyDataTable1();
        DataRow NewRow = dtResult.NewRow();
        NewRow["項次"] = 1;
        NewRow["兌點名稱"] = "180點兌換100元";
        NewRow["點數"] = "180";
        NewRow["兌換金額"] = "100";
        dtResult.Rows.Add(NewRow);
        return dtResult;
    }

    private DataTable GetEmptyDataTable2()
    {
        DataTable dtResult = new DataTable();
        dtResult.Columns.Add("門市編號", typeof(string));
        dtResult.Columns.Add("門市名稱", typeof(string));
        dtResult.Columns.Add("區域別", typeof(string));
        return dtResult;
    }

    private DataTable getGridView2Data()
    {
        DataTable dtResult = GetEmptyDataTable2();
        DataRow NewRow = dtResult.NewRow();
        NewRow["門市編號"] = "2103";
        NewRow["門市名稱"] = "永和門市";
        NewRow["區域別"] = "北一區";

        dtResult.Rows.Add(NewRow);
        return dtResult;
    }

    private DataTable GetEmptyDataTable3()
    {
        DataTable dtResult = new DataTable();
        dtResult.Columns.Add("商品料號", typeof(string));
        dtResult.Columns.Add("商品名稱", typeof(string));
        dtResult.Columns.Add("兌換點數", typeof(int));
        dtResult.Columns.Add("加購價", typeof(int));
        return dtResult;
    }

    private DataTable getGridView3Data()
    {
        DataTable dtResult = GetEmptyDataTable3();
        DataRow NewRow = dtResult.NewRow();
        NewRow["商品料號"] = "157200002";
        NewRow["商品名稱"] = "Iphone 16G";
        NewRow["兌換點數"] = "100";
        NewRow["加購價"] = "100";
        dtResult.Rows.Add(NewRow);
        return dtResult;
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        Session["gvMaster"] = GetMasterData();
        bindMasterData();
    }


    //protected void lbtnActivityNo_Click(object sender, EventArgs e)
    //{
    //    //LinkButton KeyNo = (LinkButton)sender;
    //    //gvDetail.DataSource = GetDetailData(KeyNo.Text);

    //    ViewState["gvDetail1"] = getGridView1Data();
    //    gvDetail1.DataSource = (DataTable)ViewState["gvDetail1"];
    //    gvDetail1.DataBind();

    //    ViewState["gvDetail2"] = getGridView2Data();
    //    gvDetail2.DataSource = (DataTable)ViewState["gvDetail2"];
    //    gvDetail2.DataBind();

    //    ViewState["gvDetail3"] = getGridView3Data();
    //    gvDetail3.DataSource = (DataTable)ViewState["gvDetail3"];
    //    gvDetail3.DataBind();
    //    ASPxGridView1.DataSource = (DataTable)ViewState["gvDetail3"];
    //    ASPxGridView1.DataBind();

    //    ASPxPageControl1.Visible = true;
    //}





    #region gvMaster 編輯/更新/取消 相關觸發事件
    protected void gvMaster_RowEditing(object sender, GridViewEditEventArgs e)
    {
    }
    protected void gvMaster_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
    }
    protected void gvMaster_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
    }
    protected void gvMaster_RowCommand(object sender, GridViewCommandEventArgs e)
    {
    }

    protected void gvMaster_RowCreated(Object sender, GridViewRowEventArgs e)
    {
    }

    protected void gvMaster_RowDataBound(object sender, GridViewRowEventArgs e)
    {
    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        gvMaster.AddNewRow();
    }

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
    #endregion

    #region gvDetail1 編輯/更新/取消 相關觸發事件
    protected void gvDetail1_RowEditing(object sender, GridViewEditEventArgs e)
    {
        //GridView gridview = sender as GridView;
        ////設定編輯欄位
        //gridview.EditIndex = e.NewEditIndex;
        ////Bind原查詢資料
        //DataTable dt = ViewState["gvDetail1"] as DataTable;
        //gridview.DataSource = dt;
        //gridview.DataBind();
    }
    protected void gvDetail1_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        //GridView gridview = sender as GridView;
        ////取消編輯狀態
        //gridview.EditIndex = -1;

        ////Bind新資料(重取資料)
        //bindGridView1Data();
    }
    protected void gvDetail1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        //GridView gridview = sender as GridView;
        //gridview.EditIndex = -1;
        ////Bind原查詢資料
        //DataTable dt = ViewState["gvDetail1"] as DataTable;
        //gridview.DataSource = dt;
        //gridview.DataBind();
    }

    protected void gvDetail1_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
    {
        //若不使用DataSourceID時UPDATE INSERT 和DELPETE 都要自己控制 以下為UPDATE時需自行控制
        ASPxGridView grid = (ASPxGridView)sender;
        DataTable dt = ViewState["gvDetail1"] as DataTable ?? null;
        gvDetail1.CancelEdit();
        e.Cancel = true;
        grid.DataSource = dt;
        grid.DataBind();
        ////e.NewValues["TemplateID"] = Guid.NewGuid(); // I set this PK myself.
    }

    protected void gvDetail1_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
    {
        //若不使用DataSourceID時UPDATE INSERT 和DELPETE 都要自己控制 以下為UPDATE時需自行控制
        ASPxGridView grid = (ASPxGridView)sender;
        DataTable dt = ViewState["gvDetail1"] as DataTable ?? null;
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
        gvDetail1.CancelEdit();
        e.Cancel = true;
        grid.DataSource = dt;
        grid.DataBind();
    }

    #endregion

    #region gvDetail2 編輯/更新/取消 相關觸發事件
    protected void gvDetail2_RowEditing(object sender, GridViewEditEventArgs e)
    {
        //GridView gridview = sender as GridView;
        ////設定編輯欄位
        //gridview.EditIndex = e.NewEditIndex;
        ////Bind原查詢資料
        //DataTable dt = ViewState["gvDetail2"] as DataTable;
        //gridview.DataSource = dt;
        //gridview.DataBind();
    }
    protected void gvDetail2_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        //GridView gridview = sender as GridView;
        ////取消編輯狀態
        //gridview.EditIndex = -1;

        ////Bind新資料(重取資料)
        //bindGridView2Data();
    }
    protected void gvDetail2_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        //GridView gridview = sender as GridView;
        //gridview.EditIndex = -1;
        ////Bind原查詢資料
        //DataTable dt = ViewState["gvDetail2"] as DataTable;
        //gridview.DataSource = dt;
        //gridview.DataBind();
    }

    protected void gvDetail2_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
    {
        //若不使用DataSourceID時UPDATE INSERT 和DELPETE 都要自己控制 以下為UPDATE時需自行控制
        ASPxGridView grid = (ASPxGridView)sender;
        DataTable dt = ViewState["gvDetail2"] as DataTable ?? null;
        gvDetail2.CancelEdit();
        e.Cancel = true;
        grid.DataSource = dt;
        grid.DataBind();
        ////e.NewValues["TemplateID"] = Guid.NewGuid(); // I set this PK myself.
    }

    protected void gvDetail2_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
    {
        //若不使用DataSourceID時UPDATE INSERT 和DELPETE 都要自己控制 以下為UPDATE時需自行控制
        ASPxGridView grid = (ASPxGridView)sender;
        DataTable dt = ViewState["gvDetail2"] as DataTable ?? null;
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
        gvDetail2.CancelEdit();
        e.Cancel = true;
        grid.DataSource = dt;
        grid.DataBind();
    }

    #endregion


    #region gvDetail3 編輯/更新/取消 相關觸發事件
    protected void gvDetail3_RowEditing(object sender, GridViewEditEventArgs e)
    {
    }

    protected void gvDetail3_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
    }

    protected void gvDetail3_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
    }

    protected void gvDetail3_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
    {
        //若不使用DataSourceID時UPDATE INSERT 和DELPETE 都要自己控制 以下為UPDATE時需自行控制
        ASPxGridView grid = (ASPxGridView)sender;
        DataTable dt = ViewState["gvDetail3"] as DataTable ?? null;
        grid.CancelEdit();
        e.Cancel = true;
        grid.DataSource = dt;
        grid.DataBind();
    }

    protected void gvDetail3_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
    {
        //若不使用DataSourceID時UPDATE INSERT 和DELPETE 都要自己控制 以下為UPDATE時需自行控制
        ASPxGridView grid = (ASPxGridView)sender;
        DataTable dt = ViewState["gvDetail3"] as DataTable ?? null;

        grid.CancelEdit();
        e.Cancel = true;
        grid.DataSource = dt;
        grid.DataBind();
    }

    #endregion

    #region 分頁相關 (共用)
    protected void GridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        //此函式可共用
        GridView gridview = sender as GridView;
        gridview.PageIndex = e.NewPageIndex;

        DataTable dt = ViewState[gridview.ID] as DataTable ?? null;//GridView的資料要存於ViewState以方便共用此Function
        gridview.DataSource = dt;
        gridview.DataBind();
    }
    protected void btnGoToIndex_Click(object sender, EventArgs e)
    {
        //此函式可共用
        GridView gridview = (sender as Button).Parent.Parent.Parent.Parent as GridView;
        TextBox textbox = (sender as Button).Parent.FindControl("tbGoToIndex") as TextBox;
        string strIndex = textbox.Text.Trim();
        int index = 0;
        if (int.TryParse(strIndex, out index))
        {
            index = index - 1;
            if (index >= 0 && index <= gridview.PageCount - 1)
            {
                gridview.PageIndex = index;
                DataTable dt = ViewState[gridview.ID] as DataTable ?? null;//GridView的資料要存於ViewState以方便共用此Function
                gridview.DataSource = dt;
                gridview.DataBind();
            }
            else
            {
                textbox.Text = string.Empty;
            }
        }
        else
        {
            textbox.Text = string.Empty;
        }
    }
    #endregion

    protected void ASPxButton1_Click(object sender, EventArgs e)
    {
        Response.Redirect("OPT16.aspx");
    }
    protected void gvMaster_FocusedRowChanged(object sender, EventArgs e)
    {
        if (gvMaster.FocusedRowIndex >= 0)
        {
            this.hdNo.Value = gvMaster.GetRowValues(gvMaster.FocusedRowIndex, gvMaster.KeyFieldName).ToString();

            ViewState["gvDetail1"] = getGridView1Data();
            gvDetail1.DataSource = (DataTable)ViewState["gvDetail1"];
            gvDetail1.DataBind();

            ViewState["gvDetail2"] = getGridView2Data();
            gvDetail2.DataSource = (DataTable)ViewState["gvDetail2"];
            gvDetail2.DataBind();

            ViewState["gvDetail3"] = getGridView3Data();
            gvDetail3.DataSource = (DataTable)ViewState["gvDetail3"];
            gvDetail3.DataBind();
            ASPxGridView1.DataSource = (DataTable)ViewState["gvDetail3"];
            ASPxGridView1.DataBind();

            ASPxPageControl1.Visible = true;

        }
    }
}
