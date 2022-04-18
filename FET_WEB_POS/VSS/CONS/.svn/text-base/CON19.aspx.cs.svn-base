using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class VSS_CON19_CON19 : Advtek.Utility.BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
       if (!Page.IsPostBack)
       {
          bindMasterData();
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
    private DataTable getMasterData()
    {
        DataTable dtResult = new DataTable();
        dtResult.Columns.Add("移撥單號", typeof(string));
        dtResult.Columns.Add("移出門市", typeof(string));
        dtResult.Columns.Add("移出日期", typeof(string));
        dtResult.Columns.Add("撥入門市", typeof(string));
        dtResult.Columns.Add("撥入日期", typeof(string));
        dtResult.Columns.Add("移撥狀態", typeof(string));
        dtResult.Columns.Add("更新人員", typeof(string));
        dtResult.Columns.Add("更新日期", typeof(string));

        string[] array1 = {  "在途中", "已撥入"};


        for (int i = 0; i < 15; i++)
        {
            DataRow NewRow = dtResult.NewRow();

            NewRow["移撥單號"] = "CST201007010" + i;
            NewRow["移出門市"] = "門市" + i ;
            NewRow["移出日期"] = "2010/07/01";
            NewRow["撥入門市"] = "門市" + i;
            NewRow["撥入日期"] = "2010/07/01";
            NewRow["移撥狀態"] = "在途中";
            NewRow["更新人員"] = "王小明";
            NewRow["更新日期"] = Convert.ToDateTime("2010/07/20" + DateTime.Now.ToLongTimeString()).ToString("yyyy/MM/dd HH:mm:ss");

            dtResult.Rows.Add(NewRow);
        }
        return dtResult;
    }
    protected void bindDetailData()
    {
        DataTable dtgvDetail = new DataTable();
        dtgvDetail = getDetailData();
        ViewState["gvDetail"] = dtgvDetail;
        gvDetail.DataSource = dtgvDetail;
        gvDetail.DataBind();
    }
    private DataTable getDetailData()
    {
        DataTable gvDetail = new DataTable();
        gvDetail.Columns.Clear();
        gvDetail.Columns.Add("商品類別", typeof(string));
        gvDetail.Columns.Add("商品料號", typeof(string));
        gvDetail.Columns.Add("商品名稱", typeof(string));
        gvDetail.Columns.Add("撥出數量", typeof(string));
       


        for (int i = 1; i < 5; i++)
        {
            DataRow gvDetailRow = gvDetail.NewRow();
            gvDetailRow["商品類別"] = "商品類別" + i;
            gvDetailRow["商品料號"] = "商品料號" + i;
            gvDetailRow["商品名稱"] = "商品名稱" + i;
            gvDetailRow["撥出數量"] = i % 2;
            

            gvDetail.Rows.Add(gvDetailRow);
        }
        return gvDetail;
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
    //protected void Button1_Click(object sender, EventArgs e)
    //{

    //}
    //protected void TextBox9_TextChanged(object sender, EventArgs e)
    //{

    //}
    //protected void gvMaster_SelectedIndexChanged(object sender, EventArgs e)
    //{

    //}
    protected void gvMaster_RowCommand(object sender, GridViewCommandEventArgs e)
    {

        if (e.CommandName == "select")
        {
            bindDetailData();
            DIVdetail.Visible = true;
            this.gvDetail.Visible = true;
        }

        //else //if (e.CommandName == "View")
        //{
        //    Page.Response.Redirect("CON08.aspx");
        //}
    }
    protected void gvMaster_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        //if (e.Row.RowIndex != -1)
        //{
        //    if (e.Row.RowType == DataControlRowType.DataRow)
        //    {
        //        if (e.Row.Cells[5].Text == "已存檔")
        //        {
        //            Button fix = (Button)e.Row.Cells[0].FindControl("btnfix");
        //            fix.Visible = true;
        //            fix.Enabled = true;
        //            //Button fix = (Button)e.Row.Cells[0].FindControl("btnfix");

        //            Button btnView = (Button)e.Row.Cells[0].FindControl("btnView");
        //            btnView.Enabled = true;
        //            btnView.Visible = false;

        //        }
        //    }
        //}
    }
    protected void gvDetail_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {

        //GridView grDetail = sender as GridView;
        //grDetail.EditIndex = -1;
        ////Bind原查詢資料
        //DataTable dt = ViewState["gvDetail"] as DataTable;
        //grDetail.DataSource = dt;
        //grDetail.DataBind();
    }
    protected void gvDetail_RowEditing(object sender, GridViewEditEventArgs e)
    {
        //GridView grDetail = sender as GridView;

        ////取得資料

        ////更新資料庫

        ////取消編輯狀態
        //grDetail.EditIndex = -1;

        ////Bind新資料(重取資料)
        //bindDetailData();
    }

    protected void gvDetail_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {

    }
}
