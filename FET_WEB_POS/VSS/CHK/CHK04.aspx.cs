using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
public partial class VSS_CHK04_CHK04 : Advtek.Utility.BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            gvMaster.DataSource = GetEmptyDataTable();
            gvMaster.DataBind();
        }
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        bindMasterData();
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
        dtResult.Columns.Add("交易日期", typeof(string));
        dtResult.Columns.Add("機台編號", typeof(string));
        dtResult.Columns.Add("批次", typeof(string));
        dtResult.Columns.Add("找零金", typeof(string));
        dtResult.Columns.Add("更新人員", typeof(string));
        dtResult.Columns.Add("更新日期", typeof(string));
        return dtResult;
    }

    private DataTable getMasterData()
    {
        DataTable dtResult = GetEmptyDataTable();

        Random rnd = new Random();

        for (int i = 1; i < 11; i++)
        {
            DataRow NewRow = dtResult.NewRow();
            NewRow["交易日期"] = DateTime.Now.ToString("yyyy/MM/dd");
            NewRow["機台編號"] = rnd.Next(1, 5).ToString("00");
            NewRow["批次"] = i;
            NewRow["找零金"] = "7000";
            NewRow["更新人員"] = "Jackey";
            NewRow["更新日期"] = DateTime.Now;
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
