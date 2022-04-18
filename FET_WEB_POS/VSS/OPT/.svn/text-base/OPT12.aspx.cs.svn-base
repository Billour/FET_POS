using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Advtek.Utility;

public partial class VSS_OPT12_OPT12 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

            bindEmptyData();
            //bindMemberData();

        }
    }

    protected void bindEmptyData()
    {

        DataTable dtResult = new DataTable();

        gvMaster.DataSource = dtResult;
        gvMaster.DataBind();

        gvMember.DataSource = dtResult;
        gvMember.DataBind();

        gvCondition.DataSource = dtResult;
        gvCondition.DataBind();
    }


    protected void bindMasterData()
    {
        DataTable dtResult = new DataTable();

        dtResult = getMasterData();
        ViewState["gvMaster"] = dtResult;
        gvMaster.DataSource = dtResult;
        gvMaster.DataBind();
    }

    protected void bindMemberData()
    {
        DataTable dtResult = new DataTable();

        dtResult = getMemberData();
        ViewState["gvMember"] = dtResult;
        gvMember.DataSource = dtResult;
        gvMember.DataBind();
    }

    protected void bindData2()
    {
        DataTable dtResult = new DataTable();

        dtResult = getData2();
        ViewState["gvCondition"] = dtResult;
        gvCondition.DataSource = dtResult;
        gvCondition.DataBind();
    }


    private DataTable getMasterData()
    {
        DataTable dtMaster = new DataTable();
        dtMaster.Columns.Clear();
        dtMaster.Columns.Add("項次", typeof(int));
        dtMaster.Columns.Add("累點代號", typeof(string));
        dtMaster.Columns.Add("累點金額", typeof(string));
        dtMaster.Columns.Add("累點名稱", typeof(string));
        dtMaster.Columns.Add("開始日期", typeof(string));
        dtMaster.Columns.Add("結束日期", typeof(string));
        dtMaster.Columns.Add("更新人員", typeof(string));
        dtMaster.Columns.Add("更新日期", typeof(string));
        dtMaster.Columns.Add("累點點數", typeof(string));

        string[] ary1 = { "50", "100", "500", "1000","2000","5000" };
        string[] ary2 = { "A01", "A02", "A03", "A04", "S01", "S02", "S03", "S04" };
        string[] ary3 = { "50元累績2點", "100元累績5點", "500元累績30點", "1000元累績100點", "2000元累績250點", "5000元累績600點" };
        string[] ary4 = { "50", "100", "500", "1000", "2000", "5000" }; // 點數

        for (int i = 1; i < 10; i++)
        {
            DataRow dtMasterRow = dtMaster.NewRow();
            dtMasterRow["項次"] = i;
            dtMasterRow["累點代號"] = ary2[i % 8];
            dtMasterRow["累點金額"] = ary1[i % 6];
            dtMasterRow["累點名稱"] = ary3[i % 6];
            dtMasterRow["累點點數"] = ary4[i % 6];
            dtMasterRow["開始日期"] = "2010/09/01";
            dtMasterRow["結束日期"] = "2010/09/01";
            dtMasterRow["更新人員"] = "王大明";

            dtMasterRow["更新日期"] = Convert.ToDateTime("2010/09/01" + DateTime.Now.ToLongTimeString()).ToString("yyyy/MM/dd HH:mm:ss");

            dtMaster.Rows.Add(dtMasterRow);
        }


        return dtMaster;
    }

    private DataTable getMemberData()
    {
        DataTable dtResult = new DataTable();
        dtResult.Columns.Add("項次", typeof(int));
        dtResult.Columns.Add("會員起日", typeof(string));
        dtResult.Columns.Add("會員訖日", typeof(string));

        for (int i = 1; i < 5; i++)
        {
            DataRow NewRow = dtResult.NewRow();
            NewRow["項次"] = i;
            NewRow["會員起日"] = "2010/09/01";
            NewRow["會員訖日"] = "2010/09/01";
            dtResult.Rows.Add(NewRow);
        }
        return dtResult;
    }
    
    private DataTable getData2()
    {
        DataTable dtResult = new DataTable();
        dtResult.Columns.Add("項次", typeof(int));
        dtResult.Columns.Add("商品料號", typeof(string));
        dtResult.Columns.Add("商品名稱", typeof(string));

        string[] ary1 = { "100100101", "100100102", "157200001", "157200002", "157200003" };
        string[] ary2 = { "Nokia5210", "Nokia6230", "Iphone3 8g", "Iphone4 16g", "Iphone3 16g"};
        for (int i = 1; i < 5; i++)
        {
            DataRow NewRow = dtResult.NewRow();
            NewRow["項次"] = i;
            NewRow["商品料號"] = ary1[i % 5];
            NewRow["商品名稱"] = ary2[i % 5];
            dtResult.Rows.Add(NewRow);
        }
        return dtResult;
    }


    //protected void Button1_Click(object sender, EventArgs e)
    //{
    //    //Member的新增鈕
    //    //bindMemberData(); 

    //}
    /// <summary>
    /// 新增-顯示Footer
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Button1_Click(object sender, EventArgs e)
    {
        gvMember.ShowFooter = true;
        gvMember.ShowFooterWhenEmpty = true;
        System.Web.UI.HtmlControls.HtmlTableRow tr =
            gvMember.FindChildControl<System.Web.UI.HtmlControls.HtmlTableRow>("trEmptyData");
        if (tr != null)
        {
            // 隱藏顯示文字訊息的表格列
            tr.Visible = false;
        }
        else
        {
            // 重新繫結資料
            bindMemberData();
        }
    }
    /// <summary>
    /// 取消新增-隱藏Footer
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnCancel_Click1(object sender, EventArgs e)
    {
        gvMember.ShowFooterWhenEmpty = false;
        gvMember.ShowFooter = false;
        // 重新繫結資料
        if (gvMember.Rows.Count == 0)
        {
            //gvMaster.DataSource = GetEmptyDataTable();
            gvMember.DataBind();
        }
        else
        {
            bindMemberData();
        }
    }
    
    
    
    //protected void Button3_Click(object sender, EventArgs e)
    //{
    //    //排外條件
    //    //bindData2();
    //}
    /// <summary>
    /// 新增-顯示Footer
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Button3_Click(object sender, EventArgs e)
    {
        gvCondition.ShowFooter = true;
        gvCondition.ShowFooterWhenEmpty = true;
        System.Web.UI.HtmlControls.HtmlTableRow tr =
            gvCondition.FindChildControl<System.Web.UI.HtmlControls.HtmlTableRow>("trEmptyData");
        if (tr != null)
        {
            // 隱藏顯示文字訊息的表格列
            tr.Visible = false;
        }
        else
        {
            // 重新繫結資料
            bindData2();
        }
    }
    /// <summary>
    /// 取消新增-隱藏Footer
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnCancel_Click2(object sender, EventArgs e)
    {
        gvCondition.ShowFooterWhenEmpty = false;
        gvCondition.ShowFooter = false;
        // 重新繫結資料
        if (gvCondition.Rows.Count == 0)
        {
            //gvMaster.DataSource = GetEmptyDataTable();
            gvCondition.DataBind();
        }
        else
        {
            bindData2();
        }
    }



    protected void btnSearch_Click(object sender, EventArgs e)
    {
        bindMasterData();

        //Member的新增鈕
        bindMemberData(); 
        //排外條件
        bindData2();

    }

    //protected void btnAddNew_Click(object sender, EventArgs e)
    //{
    //    bindMasterData();
    //    gvMaster.Visible = true;

    //}
        /// <summary>
    /// 新增-顯示Footer
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnAddNew_Click(object sender, EventArgs e)
    {
        gvMaster.ShowFooter = true;
        gvMaster.ShowFooterWhenEmpty = true;
        System.Web.UI.HtmlControls.HtmlTableRow tr =
            gvMaster.FindChildControl<System.Web.UI.HtmlControls.HtmlTableRow>("trEmptyData");
        if (tr != null)
        {
            // 隱藏顯示文字訊息的表格列
            tr.Visible = false;
        }
        else
        {
            // 重新繫結資料
            bindMasterData();
        }
    }

    /// <summary>
    /// 取消新增-隱藏Footer
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        gvMaster.ShowFooterWhenEmpty = false;
        gvMaster.ShowFooter = false;
        // 重新繫結資料
        if (gvMaster.Rows.Count == 0)
        {
            //gvMaster.DataSource = GetEmptyDataTable();
            gvMaster.DataBind();
        }
        else
        {
            bindMasterData();
        }
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

    #region gvMember 編輯/更新/取消 相關觸發事件
    protected void gvMember_RowEditing(object sender, GridViewEditEventArgs e)
    {
        GridView gridview = sender as GridView;
        //設定編輯欄位
        gridview.EditIndex = e.NewEditIndex;
        //Bind原查詢資料
        DataTable dt = ViewState["gvMember"] as DataTable;
        gridview.DataSource = dt;
        gridview.DataBind();
    }

    protected void gvMember_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        GridView gridview = sender as GridView;

        //取得資料

        //更新資料庫

        //取消編輯狀態
        gridview.EditIndex = -1;

        //Bind新資料(重取資料)
        bindMemberData();
    }

    protected void gvMember_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        GridView gridview = sender as GridView;
        gridview.EditIndex = -1;
        //Bind原查詢資料
        DataTable dt = ViewState["gvMember"] as DataTable;
        gridview.DataSource = dt;
        gridview.DataBind();
    }
    #endregion

    #region gvCondition 編輯/更新/取消 相關觸發事件
    protected void gvCondition_RowEditing(object sender, GridViewEditEventArgs e)
    {
        GridView gridview = sender as GridView;
        //設定編輯欄位
        gridview.EditIndex = e.NewEditIndex;
        //Bind原查詢資料
        DataTable dt = ViewState["gvCondition"] as DataTable;
        gridview.DataSource = dt;
        gridview.DataBind();
    }

    protected void gvCondition_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        GridView gridview = sender as GridView;

        //取得資料

        //更新資料庫

        //取消編輯狀態
        gridview.EditIndex = -1;

        //Bind新資料(重取資料)
        bindData2();
    }

    protected void gvCondition_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        GridView gridview = sender as GridView;
        gridview.EditIndex = -1;
        //Bind原查詢資料
        DataTable dt = ViewState["gvCondition"] as DataTable;
        gridview.DataSource = dt;
        gridview.DataBind();
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

}