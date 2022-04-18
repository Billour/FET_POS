using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
public partial class VSS_OPT17_OPT17 : Advtek.Utility.BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            // 繫結空的資料表，以顯示表頭欄位
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
        dtResult.Columns.Add("項次", typeof(string));
        dtResult.Columns.Add("門市編號", typeof(string));
        dtResult.Columns.Add("門市名稱", typeof(string));
        dtResult.Columns.Add("發票格式", typeof(string));
        dtResult.Columns.Add("所屬年月起", typeof(string));
        dtResult.Columns.Add("所屬年月訖", typeof(string));
        dtResult.Columns.Add("字軌", typeof(string));
        dtResult.Columns.Add("起始編號", typeof(string));
        dtResult.Columns.Add("終止編號", typeof(string));
        dtResult.Columns.Add("發票張數", typeof(string));
        dtResult.Columns.Add("更新人員", typeof(string));
        dtResult.Columns.Add("更新日期", typeof(string));
        return dtResult;
    }

    private DataTable getMasterData()
    {
        DataTable dtResult = GetEmptyDataTable();

        string[] ary = { "手開二聯式", "手開三聯式" };
        Random rnd = new Random();

        for (int i = 1; i < 11; i++)
        {
            DataRow NewRow = dtResult.NewRow();
            NewRow["項次"] = i;
            NewRow["門市編號"] = "R2101";
            NewRow["門市名稱"] = "遠企";
            NewRow["發票格式"] =  ary[rnd.Next(1, 10) % 2];
            NewRow["所屬年月起"] = DateTime.Now.ToString("yyyy/MM/dd");
            NewRow["所屬年月訖"] = DateTime.Now.ToString("yyyy/MM/dd");
            NewRow["字軌"] = "AA";
            NewRow["起始編號"] = i.ToString("00");
            NewRow["終止編號"] = (i + 10).ToString("00");
            NewRow["發票張數"] = i;
            NewRow["更新人員"] = "Jackey";
            NewRow["更新日期"] = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");
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

        DropDownList ddlInvoice = (DropDownList)gridview.Rows[e.NewEditIndex].Cells[5].FindControl("ddlInvoice");
        Label lbInvoice1 = (Label)gridview.Rows[e.NewEditIndex].Cells[5].FindControl("lbInvoice1");
        ddlInvoice.SelectedValue = lbInvoice1.Text;
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
