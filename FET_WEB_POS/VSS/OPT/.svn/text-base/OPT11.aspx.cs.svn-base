using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class VSS_OPT11_OPT11 : Advtek.Utility.BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            // 繫結空的資料表，以顯示表頭欄位
            gvMaster.DataSource = GetEmptyDataTable();
            gvMaster.DataBind();
        }
    }

    protected void bindMasterData( )
    {
        DataTable dtGvMaster = new DataTable();
        dtGvMaster = getMasterData();
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

    private DataTable getMasterData( )
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

    protected void btnSave_Click(object sender, EventArgs e)
    {
//        lblOrderNo.Text = "1234-01-100912345";
    }

    //protected void btnCheck_Click()
    //{
    //    if (gvMaster.HeaderRow.Cells[0].Text == "True")
    //    {
    //        gvMaster.Rows[0].Cells[0].Text = "True";
    //    }
    //}

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
        bindMasterData( );
        gvMaster.Visible = true;
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
