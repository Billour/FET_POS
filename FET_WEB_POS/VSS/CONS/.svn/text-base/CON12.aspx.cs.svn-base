using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class VSS_CON12_CON12 : Advtek.Utility.BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string dno = Request.QueryString["dno"] == null ? "" : Request.QueryString["dno"].ToString().Trim();
        this.ViewState["dno"] = dno;

        if (!IsPostBack)
        {
            if (this.ViewState["dno"].ToString() == "")
            {
                this.DropDownList1.SelectedIndex = 0;
                // 繫結空的資料表，以顯示表頭欄位
                gvMaster.DataSource = GetEmptyDataTable();
                gvMaster.DataBind();
            }
            else
            {
                // "COR2010073001"
                this.DropDownList1.SelectedIndex = 1;

                bindMasterData();
                //退倉日期-系統日
                lblReturnDate.Text = DateTime.Now.ToString("yyyy/MM/dd");

            }

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
        DataTable dtMaster = new DataTable();
        dtMaster.Columns.Clear();
        dtMaster.Columns.Add("項次", typeof(string));
        dtMaster.Columns.Add("商品料號", typeof(string));
        dtMaster.Columns.Add("商品名稱", typeof(string));
        dtMaster.Columns.Add("廠商編號", typeof(string));
        dtMaster.Columns.Add("廠商名稱", typeof(string));
        dtMaster.Columns.Add("庫存數量", typeof(string));
        dtMaster.Columns.Add("實際退倉數量", typeof(string));
        return dtMaster;
    }

    private DataTable getMasterData()
    {
        DataTable dtMaster = GetEmptyDataTable();

        for (int i = 1; i < 6; i++)
        {
            DataRow dtMasterRow = dtMaster.NewRow();
            dtMasterRow["項次"] = i;
            dtMasterRow["商品料號"] = "AC1110700" + i;
            dtMasterRow["商品名稱"] = "商品名稱" + i;
            dtMasterRow["廠商編號"] = "GG00001";
            dtMasterRow["廠商名稱"] = "廠商名稱" + i;
            dtMasterRow["庫存數量"] = i;
            dtMasterRow["實際退倉數量"] = "0";
            dtMaster.Rows.Add(dtMasterRow);
        }
        return dtMaster;
    }


    protected void btnSave_Click(object sender, EventArgs e)
    {
        //lblOrderNo.Text = "COR2010073001";
        Label2.Text = "01 已存檔";

    }

    protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
    {

        if (DropDownList1.SelectedIndex > 0)
        {
            bindMasterData();
            //退倉日期-系統日
            lblReturnDate.Text = DateTime.Now.ToString("yyyy/MM/dd");
        }
        else
        {
            // 繫結空的資料表，以顯示表頭欄位
            gvMaster.DataSource = GetEmptyDataTable();
            gvMaster.DataBind();
        }

    }



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
