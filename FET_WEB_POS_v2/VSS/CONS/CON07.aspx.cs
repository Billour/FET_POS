using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class VSS_CONS_CON07 : BasePage
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
        dtResult.Columns.Add("主配單號", typeof(string));
        dtResult.Columns.Add("主配日期", typeof(string));
        dtResult.Columns.Add("廠商編號", typeof(string));
        dtResult.Columns.Add("廠商名稱", typeof(string));
        dtResult.Columns.Add("狀態", typeof(string));
        dtResult.Columns.Add("更新人員", typeof(string));
        dtResult.Columns.Add("更新日期", typeof(string));
        return dtResult;
    }

    private DataTable getMasterData()
    {
        DataTable dtResult = GetEmptyDataTable();

        string[] array1 = { "已存檔", "轉單中", "已成單", "待進貨", "已驗收" };


        for (int i = 0; i < 10; i++)
        {
            DataRow NewRow = dtResult.NewRow();

            NewRow["主配單號"] = "CAAC1201007280" + i;
            NewRow["主配日期"] = "2010/07/20";
            NewRow["廠商編號"] = "AC0000" + i;
            NewRow["廠商名稱"] = "廠商名稱" + i;
            NewRow["狀態"] = array1[i % 5];
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
        //gvDetail.Columns.Add("項次", typeof(string));
        gvDetail.Columns.Add("商品類別", typeof(string));
        gvDetail.Columns.Add("商品料號", typeof(string));
        gvDetail.Columns.Add("商品名稱", typeof(string));
        gvDetail.Columns.Add("實際訂購量", typeof(string));
        gvDetail.Columns.Add("門市編號", typeof(string));
        gvDetail.Columns.Add("門市名稱", typeof(string));
        gvDetail.Columns.Add("備註", typeof(string));


        for (int i = 1; i < 5; i++)
        {
            DataRow gvDetailRow = gvDetail.NewRow();
            //gvDetailRow["項次"] = i;
            gvDetailRow["商品類別"] = "商品類別" + i;
            gvDetailRow["商品料號"] = "商品料號" + i;
            gvDetailRow["商品名稱"] = "商品名稱" + i;
            gvDetailRow["實際訂購量"] = i % 2;
            gvDetailRow["門市編號"] = "門市編號" + i;
            gvDetailRow["門市名稱"] = "門市名稱" + i;
            gvDetailRow["備註"] = "備註" + i;

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

        else  if (e.CommandName == "View")
        {
            Page.Response.Redirect("CON08.aspx?dno=CAAC12010072800");
        }

        

    }
    protected void gvMaster_RowDataBound(object sender, GridViewRowEventArgs e)
    {
       
        if (e.Row.RowIndex != -1)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (e.Row.Cells[5].Text == "已存檔")
                {
                    //Button fix = (Button)e.Row.Cells[0].FindControl("btnfix");
                    //fix.Visible = true;
                    //fix.Enabled = true;
                    ////Button fix = (Button)e.Row.Cells[0].FindControl("btnfix");

                    Button btnView = (Button)e.Row.Cells[0].FindControl("btnEdit");
                    btnView.Enabled = true;
                }
                else
                {
                    Button btnView = (Button)e.Row.Cells[0].FindControl("btnEdit");
                    btnView.Enabled = false;
                }
            }
        }
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
