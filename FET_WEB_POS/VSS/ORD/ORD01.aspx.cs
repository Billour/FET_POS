using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Advtek.Utility;

public partial class VSS_ORD01_ORD01 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            bindEmptyData();

            divContent.Style["display"] = "";
            bindgvMaster();

            btnSave.Visible = true;
            btnDrop.Visible = true;

            DataTable dtProdDetail = new DataTable();
            dtProdDetail.Columns.Clear();
            dtProdDetail.Columns.Add("商品編號", typeof(string));
            dtProdDetail.Columns.Add("商品名稱", typeof(string));
            dtProdDetail.Columns.Add("數量", typeof(int));
            gvDetail.DataSource = dtProdDetail;
            gvDetail.DataBind();

            BindData();

            //ModalPopup.Show();
        }
    }

    private void BindData()
    {
        DataTable orders = new DataTable();
        orders.Columns.Clear();
        orders.Columns.Add("商品編號", typeof(string));
        orders.Columns.Add("商品名稱", typeof(string));
        orders.Columns.Add("需求量", typeof(int));
        orders.Columns.Add("門市庫存量", typeof(int));
        orders.Columns.Add("訂購量", typeof(int));

        DataRow row = orders.NewRow();
        row["商品編號"] = "100100100";
        row["商品名稱"] = "A手機";
        row["需求量"] = 10;
        row["門市庫存量"] = 5;
        row["訂購量"] = 5;
        orders.Rows.Add(row);

        row = orders.NewRow();
        row["商品編號"] = "100100101";
        row["商品名稱"] = "B手機";
        row["需求量"] = 10;
        row["門市庫存量"] = 0;
        row["訂購量"] = 10;
        orders.Rows.Add(row);


        row = orders.NewRow();
        row["商品編號"] = "100100102";
        row["商品名稱"] = "C手機";
        row["需求量"] = 10;
        row["門市庫存量"] = 2;
        row["訂購量"] = 8;
        orders.Rows.Add(row);

        GridView1.DataSource = orders;
        GridView1.DataBind();
    }

    protected void bindEmptyData()
    {
        DataTable dtResult = new DataTable();

        gvMaster.DataSource = dtResult;
        gvMaster.DataBind();        
    }
    protected void bindgvMaster()
    {
        DataTable dtGvMaster = new DataTable();
        dtGvMaster = getMasterData();
        ViewState["gvMaster"] = dtGvMaster;
        gvMaster.DataSource = dtGvMaster;
        gvMaster.DataBind();
    }

    private DataTable getMasterData()
    {        
        DataTable dtMaster = new DataTable();
        dtMaster.Columns.Clear();
        dtMaster.Columns.Add("項次", typeof(int));
        dtMaster.Columns.Add("商品編號", typeof(string));
        dtMaster.Columns.Add("商品名稱", typeof(string));
        dtMaster.Columns.Add("建議訂購量", typeof(int));
        dtMaster.Columns.Add("網購需求量", typeof(int));
        dtMaster.Columns.Add("門市庫存量", typeof(int));
        dtMaster.Columns.Add("在途量", typeof(int));        
        dtMaster.Columns.Add("當日訂購量", typeof(int));
        dtMaster.Columns.Add("當日總訂購量", typeof(int));
        //dtMaster.Columns.Add("業助調整數量", typeof(string));

        for (int i = 1; i < 12; i++)
        {
            DataRow dtMasterRow = dtMaster.NewRow();
            dtMasterRow["項次"] = i;
            dtMasterRow["商品編號"] = "A000" + i;
            dtMasterRow["商品名稱"] = "商品名稱" + i;
            dtMasterRow["建議訂購量"] = 10 * (i + 1)/2;
            dtMasterRow["門市庫存量"] = (i + 1);
            dtMasterRow["在途量"] = 1 * (i + 1);
            dtMasterRow["網購需求量"] = 4 * (i + 1) - 7;
            dtMasterRow["當日訂購量"] = (4 * i) % 3;
            dtMasterRow["當日總訂購量"] = 0;
            //Convert.ToInt32(dtMasterRow["建議訂購量"]) - Convert.ToInt32(dtMasterRow["在途量"]) - Convert.ToInt32(dtMasterRow["今日已訂購量"]);
            //dtMasterRow["業助調整數量"] = "";//(i*10)%6;
            dtMaster.Rows.Add(dtMasterRow);

            dtMaster.Rows[i - 1]["當日總訂購量"] = Convert.ToInt32(dtMaster.Rows[i - 1]["建議訂購量"])
                - Convert.ToInt32(dtMaster.Rows[i - 1]["當日訂購量"])
                - Convert.ToInt32(dtMaster.Rows[i - 1]["網購需求量"])
                - Convert.ToInt32(dtMaster.Rows[i - 1]["在途量"]);
        }
        return dtMaster;

    }

    protected void bindgvDetail()
    {
        DataTable dtGvProdDetail = new DataTable();
        dtGvProdDetail = getGvProdDetailData();
        gvDetail.DataSource = dtGvProdDetail;
        gvDetail.DataBind();
    }

    private DataTable getGvProdDetailData()
    {
        DataTable dtProdDetail = new DataTable();
        dtProdDetail.Columns.Clear();
        dtProdDetail.Columns.Add("商品編號", typeof(string));
        dtProdDetail.Columns.Add("商品名稱", typeof(string));
        //dtProdDetail.Columns.Add("搭配量", typeof(int));
        dtProdDetail.Columns.Add("訂購量", typeof(int));

        for (int i = 1; i < 2; i++)
        {
            DataRow dtProdDetailRow = dtProdDetail.NewRow();
            dtProdDetailRow["商品編號"] = "A000" + i;
            dtProdDetailRow["商品名稱"] = "商品名稱" + i;
            //dtProdDetailRow["搭配量"] = 1;//100 * (i + 1);
            dtProdDetailRow["訂購量"] = 2;
            dtProdDetail.Rows.Add(dtProdDetailRow);
        }
        return dtProdDetail;
    }

    protected void btnNew_Click(object sender, EventArgs e)
    {
        divContent.Style["display"] = "";
        bindgvMaster();
        
        btnSave.Visible = true;
        btnDrop.Visible = true;

    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        //lblOrderNo.Text = "GG000001";
        Label2.Text = "10-正式訂單";
    }

    protected void gvMaster_OnRowDataBound(object sender, GridViewRowEventArgs e)
    {
        GridViewRow row = e.Row;
        if (e.Row.RowType == DataControlRowType.DataRow)
        {            
            Button b = e.Row.FindControl("btnSelect") as Button;
            if (row.DataItemIndex >= 3)
            {
                b.Enabled = false;
            }
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
        bindgvMaster();
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
    
    protected void gvMaster_SelectedIndexChanged(Object sender, EventArgs e)
    {
        //GridViewRow row = gvMaster.SelectedRow;
        bindgvDetail();        
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
