using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class VSS_CON21_CON21 : Advtek.Utility.BasePage
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
        dtResult.Columns.Add("移出時間", typeof(string));
        dtResult.Columns.Add("撥入門市", typeof(string));
        dtResult.Columns.Add("撥入時間", typeof(string));
        dtResult.Columns.Add("移撥狀態", typeof(string));
        dtResult.Columns.Add("更新人員", typeof(string));
        dtResult.Columns.Add("更新日期", typeof(string));

        string[] array1 = {  "在途中", "已撥入"};
        string[] array2 = { "2103", "2104", "2108", "2113" };


        for (int i = 0; i < 10; i++)
        {
            DataRow NewRow = dtResult.NewRow();

            NewRow["移撥單號"] = "CAAC1201007280" + i;
            NewRow["移出門市"] = array2[i % 4];
            NewRow["移出時間"] = Convert.ToDateTime("2010/09/01" + DateTime.Now.ToLongTimeString()).ToString("yyyy/MM/dd HH:mm");
            NewRow["撥入門市"] = "2101";
            NewRow["撥入時間"] = "";
            NewRow["移撥狀態"] = "在途中";
            NewRow["更新人員"] = "王小明";
            NewRow["更新日期"] = Convert.ToDateTime("2010/09/01" + DateTime.Now.ToLongTimeString()).ToString("yyyy/MM/dd");

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
        gvDetail.Columns.Add("項次", typeof(string));
        gvDetail.Columns.Add("商品類別", typeof(string));
        gvDetail.Columns.Add("商品料號", typeof(string));
        gvDetail.Columns.Add("商品名稱", typeof(string));
        gvDetail.Columns.Add("撥出數量", typeof(string));
        gvDetail.Columns.Add("撥入數量", typeof(string));        


        for (int i = 1; i < 5; i++)
        {
            DataRow gvDetailRow = gvDetail.NewRow();
            gvDetailRow["項次"] = i;
            gvDetailRow["商品類別"] = "商品類別" + i;
            gvDetailRow["商品料號"] = "商品料號" + i;
            gvDetailRow["商品名稱"] = "商品名稱" + i;
            gvDetailRow["撥出數量"] = 1;
            gvDetailRow["撥入數量"] = 1;
            
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

        else //if (e.CommandName == "View")
        {
            bindDetailData();
            DIVdetail.Visible = false;
            this.gvDetail.Visible = false;
        }



    }
    protected void gvMaster_RowDataBound(object sender, GridViewRowEventArgs e)
    {

      
    }
    protected void gvDetail_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        GridView grDetail = sender as GridView;
        grDetail.EditIndex = -1;
        //Bind原查詢資料
        DataTable dt = ViewState["gvDetail"] as DataTable;
        grDetail.DataSource = dt;
        grDetail.DataBind();
    }
    protected void gvDetail_RowEditing(object sender, GridViewEditEventArgs e)
    {
        GridView grDetail = sender as GridView;
        grDetail.EditIndex = e.NewEditIndex;
        DataTable dt = ViewState["gvDetail"] as DataTable;
        grDetail.DataSource = dt;
        grDetail.DataBind();
    }

    protected void gvDetail_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        GridView gridview = sender as GridView;

        //取得資料

        //更新資料庫

        //取消編輯狀態


        //Bind新資料(重取資料)
        
    }
    protected void gvDetail_RowCreated(object sender, GridViewRowEventArgs e)
    {

    }
}
