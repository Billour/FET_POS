using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class VSS_ORD06_ORD06 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            bindEmptyData();
        }
    }
    protected void bindEmptyData()
    {

        DataTable dtResult = new DataTable();

        gvMaster.DataSource = dtResult;
        gvMaster.DataBind();


    }
    protected void bindMasterData()
    {
        DataTable dtResult = new DataTable();
        dtResult = getMasterData();
        ViewState["gvMaster"] = dtResult;
        gvMaster.DataSource = dtResult;
        gvMaster.DataBind();
    }
    private DataTable getMasterData()
    {
        DataTable dtResult = new DataTable();
        dtResult.Columns.Add("項次", typeof(string));
        dtResult.Columns.Add("狀態", typeof(string));
        dtResult.Columns.Add("主商品編號", typeof(string));
        dtResult.Columns.Add("商品名稱", typeof(string));
        dtResult.Columns.Add("開始日期", typeof(string));
        dtResult.Columns.Add("結束日期", typeof(string));
        dtResult.Columns.Add("更新日期", typeof(string));
        dtResult.Columns.Add("更新人員", typeof(string));

        string[] array1 = { "HTC Hero", "iPhone4", "NOKIA X6", "Sony Ericesson" };

        for (int i = 0; i < 10; i++)
        {
            DataRow NewRow = dtResult.NewRow();
            NewRow["項次"] = i+1;
            NewRow["狀態"] = "過期";
            NewRow["主商品編號"] = "15020000" + i;
            NewRow["商品名稱"] = array1 [i % 4];
            NewRow["開始日期"] = "2010/07/01";
            NewRow["結束日期"] = "2012/07/01";;
            NewRow["更新日期"] = "2010/07/01";
            NewRow["更新人員"] = "王小明";
            dtResult.Rows.Add(NewRow);

        }
        return dtResult;
    }

    protected void bindDetailData(string KeyNo)
    {
       DataTable dtResult = new DataTable();
       dtResult = getDetailData(KeyNo);
       ViewState["gvDetail"] = dtResult;
       gvDetail.DataSource = dtResult;
       gvDetail.DataBind();
    }

    private DataTable getDetailData(string KeyNo)
    {
       DataTable dtResult = new DataTable();
       dtResult.Columns.Add("搭配商品編號", typeof(string));
       dtResult.Columns.Add("商品名稱", typeof(string));

       for (int i = 0; i < 2; i++)
       {
          DataRow NewRow = dtResult.NewRow();
          NewRow["搭配商品編號"] = KeyNo + "_A0" + i;
          NewRow["商品名稱"] = "大雙網促銷";
          dtResult.Rows.Add(NewRow);
       }
       return dtResult;
    }

    protected void btnNew_Click(object sender, EventArgs e)
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
        //gvMaster.Visible = true;
        //bindMasterData();
    }
    protected void btnNew3_Click(object sender, EventArgs e)
    {
       
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {

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

    protected void gvMaster_SelectedIndexChanged(object sender, EventArgs e)
    {
       GridView mygridview = sender as GridView;
       if (mygridview.SelectedIndex >= 0)
       {
          string KeyNo = mygridview.Rows[mygridview.SelectedIndex].Cells[5].Text;
          bindDetailData(KeyNo);

          dviShow.Visible = true;
          gvDetail.Visible = true;
       }
    }

    protected void gvDetail_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
       GridView gridview = sender as GridView;
       gridview.EditIndex = -1;
       //Bind原查詢資料
       DataTable dt = ViewState["gvDetail"] as DataTable;
       gridview.DataSource = dt;
       gridview.DataBind();
    }
    protected void gvDetail_RowEditing(object sender, GridViewEditEventArgs e)
    {
       GridView gridview = sender as GridView;
       //設定編輯欄位
       gridview.EditIndex = e.NewEditIndex;
       //Bind原查詢資料
       DataTable dt = ViewState["gvDetail"] as DataTable;
       gridview.DataSource = dt;
       gridview.DataBind();
    }
    protected void gvDetail_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        GridView gridview = sender as GridView;

        //取得資料

        //更新資料庫

        //取消編輯狀態
        gridview.EditIndex = -1;
      
        //Bind新資料(重取資料)
        bindDetailData("3");
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        gvMaster.ShowFooterWhenEmpty = false;
        gvMaster.ShowFooter = false;
        // 重新繫結資料
        if (gvMaster.Rows.Count == 0)
        {
            bindMasterData();
            //gvMaster.DataSource = GetEmptyDataTable();
            //gvMaster.DataBind();
        }
        else
        {
            bindMasterData();
        }
    }

    protected void btnAddNew_Click(object sender, EventArgs e)
    {
       
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        bindMasterData();
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
