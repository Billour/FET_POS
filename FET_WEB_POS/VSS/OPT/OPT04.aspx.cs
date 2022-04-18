using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class VSS_OPT_OPT04 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
           string[] ary2 = { "遠東百貨", "愛買", "Sogo" };
           ddlCompany.DataSource = ary2;
           ddlCompany.DataBind();

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
        dtResult.Columns.Add("序號", typeof(string));
        dtResult.Columns.Add("狀態", typeof(string));
        dtResult.Columns.Add("公司別", typeof(string));
        dtResult.Columns.Add("禮券名稱", typeof(string));
        dtResult.Columns.Add("手續費", typeof(string));
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
        dtMaster.Columns.Add("序號", typeof(string));
        dtMaster.Columns.Add("狀態", typeof(string));
        dtMaster.Columns.Add("公司別", typeof(string));
        dtMaster.Columns.Add("禮券名稱", typeof(string));
        dtMaster.Columns.Add("手續費", typeof(string));
        dtMaster.Columns.Add("開始日期", typeof(string));
        dtMaster.Columns.Add("結束日期", typeof(string));
        dtMaster.Columns.Add("更新日期", typeof(string));
        dtMaster.Columns.Add("更新人員", typeof(string));

        string[] array1 = { "有效", "過期", "已停止" };
        string[] array2 = { "現金", "信用卡", "禮券", "金融卡", "HappyGo" };
        string[] array3 = { "遠東百貨", "愛買", "Sogo" };
        for (int i = 0; i < 6; i++)
        {
            DataRow dtMasterRow = dtMaster.NewRow();
            dtMasterRow["序號"] = i;
            dtMasterRow["狀態"] = array1[i % 3];
            dtMasterRow["公司別"] = array3[i % 3];
            dtMasterRow["禮券名稱"] = "酬賓禮券";
            dtMasterRow["手續費"] = "0.4";
            dtMasterRow["開始日期"] = "2010/08/01";
            if (i == 2) dtMasterRow["結束日期"] = "";
            else dtMasterRow["結束日期"] = "2010/12/31";
            dtMasterRow["更新日期"] = DateTime.Now.AddDays(-i).AddMinutes(i * 32).ToString("yyyy/MM/dd");
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

    /// <summary>
    /// 新增-顯示Footer
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnAdd_Click(object sender, EventArgs e)
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
            gvMaster.DataSource = GetEmptyDataTable();
            gvMaster.DataBind();
        }
        else
        {
            bindMasterData();
        }
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
