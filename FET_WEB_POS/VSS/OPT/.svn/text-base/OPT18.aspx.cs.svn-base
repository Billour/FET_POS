using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

public partial class VSS_OPT_OPT18 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
          
            gvMaster.DataSource = GetEmptyDataTable1();
            gvMaster.DataBind();

            gvDetails.DataSource = GetEmptyDataTable2();
            gvDetails.DataBind();
        }        
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        bindMasterData();
        BindDetailsData();
    }

    protected void bindMasterData()
    {
        DataTable dtResult = new DataTable();
        dtResult = getMasterData();
        ViewState["gvMaster"] = dtResult;
        gvMaster.DataSource = dtResult;
        gvMaster.DataBind();
    }

    private DataTable GetEmptyDataTable1()
    {
        DataTable dt = new DataTable();
        dt.Columns.Add("項次", typeof(string));
        dt.Columns.Add("門市編號", typeof(string));
        dt.Columns.Add("門市名稱", typeof(string));
        dt.Columns.Add("折扣月份", typeof(string));
        dt.Columns.Add("折扣總額", typeof(int));
        dt.Columns.Add("更新人員", typeof(string));
        dt.Columns.Add("更新日期", typeof(DateTime));
        return dt;
    }

    private DataTable getMasterData()
    {
        DataTable dtResult = GetEmptyDataTable1();

        for (int i = 1; i < 11; i++)
        {
            DataRow NewRow = dtResult.NewRow();
            NewRow["項次"] = i;
            NewRow["門市編號"] = "R2101";
            NewRow["門市名稱"] = "遠企";
            NewRow["折扣月份"] = "07/2010";
            NewRow["折扣總額"] = 25000;
            NewRow["更新人員"] = "Jackey";
            NewRow["更新日期"] = DateTime.Now;
            dtResult.Rows.Add(NewRow);
        }
        return dtResult;
    }

    protected void BindDetailsData()
    {
        DataTable dtResult = new DataTable();
        dtResult = GetDetailsData();
        ViewState["gvDetails"] = dtResult;
        gvDetails.DataSource = dtResult;
        gvDetails.DataBind();        
    }

    private DataTable GetEmptyDataTable2()
    {
        DataTable dt = new DataTable();
        dt.Columns.Add("項次", typeof(string));
        dt.Columns.Add("角色", typeof(string));
        dt.Columns.Add("金額", typeof(string));
        dt.Columns.Add("比率", typeof(int));
        dt.Columns.Add("折扣上限金額", typeof(int));
        return dt;
    }

    private DataTable GetDetailsData()
    {
        DataTable dtResult = GetEmptyDataTable2();

        string[] roles = { "店員", "店長" };

        Random rnd = new Random();
        for (int i = 1; i < 3; i++)
        {
            DataRow NewRow = dtResult.NewRow();
            NewRow["項次"] = i;
            NewRow["角色"] = roles[rnd.Next(0,1)];
            NewRow["金額"] = 10000*i;
            NewRow["比率"] = 50 + i*10;
            NewRow["折扣上限金額"] = 25000;           
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
    protected void btnAdd1_Click(object sender, EventArgs e)
    {
        gvMaster.ShowFooter = true;
        gvMaster.ShowFooterWhenEmpty = true;
        HtmlTableRow tr = gvMaster.FindChildControl<HtmlTableRow>("trEmptyData");
        if (tr != null)
        {
            tr.Visible = false;
        }
        else
        {
            bindMasterData();
        }
    }

    /// <summary>
    /// 取消新增-隱藏Footer
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnCancel1_Click(object sender, EventArgs e)
    {
        gvMaster.ShowFooterWhenEmpty = false;
        gvMaster.ShowFooter = false;
        if (gvMaster.Rows.Count == 0)
        {
            gvMaster.DataSource = GetEmptyDataTable1();
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

    #region gvDetails 新增/編輯/更新/取消 相關觸發事件
    /// <summary>
    /// 新增-顯示Footer
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnAdd2_Click(object sender, EventArgs e)
    {
        gvDetails.ShowFooter = true;
        gvDetails.ShowFooterWhenEmpty = true;
        HtmlTableRow tr = gvDetails.FindChildControl<HtmlTableRow>("trEmptyData");
        if (tr != null)
        {
            tr.Visible = false;
        }
        else
        {
            BindDetailsData();
        }
    }

    /// <summary>
    /// 取消新增-隱藏Footer
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnCancel2_Click(object sender, EventArgs e)
    {
        gvDetails.ShowFooterWhenEmpty = false;
        gvDetails.ShowFooter = false;
        if (gvDetails.Rows.Count == 0)
        {
            gvDetails.DataSource = GetEmptyDataTable2();
            gvDetails.DataBind();
        }
        else
        {
            BindDetailsData();
        }        
    }

    protected void gvDetails_RowEditing(object sender, GridViewEditEventArgs e)
    {
        GridView gridview = sender as GridView;
        //設定編輯欄位
        gridview.EditIndex = e.NewEditIndex;
        //Bind原查詢資料
        DataTable dt = ViewState["gvDetails"] as DataTable;
        gridview.DataSource = dt;
        gridview.DataBind();
    }

    protected void gvDetails_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        GridView gridview = sender as GridView;
        //取消編輯狀態
        gridview.EditIndex = -1;

        //Bind新資料(重取資料)
        BindDetailsData();
    }

    protected void gvDetails_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        GridView gridview = sender as GridView;
        gridview.EditIndex = -1;
        //Bind原查詢資料
        DataTable dt = ViewState["gvDetails"] as DataTable;
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
