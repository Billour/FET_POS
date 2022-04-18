using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Advtek.Utility;
public partial class VSS_INV_INV18_1 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string dno = Request.QueryString["dno"] == null ? "" : Request.QueryString["dno"].ToString().Trim();
        this.ViewState["dno"] = dno;

        if (!IsPostBack)
        {

            if (this.ViewState["dno"] == "")
            {
                this.lblOrderNo.Text = "";
                bindEmptyData();
            }
            else 
            {
                // "SA2011-1009003";
                lblOrderNo.Text = dno.ToString();
                bindgvMaster();
                btnSave.Visible = true;
                btnDrop.Visible = true;
            }
        }
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
        dtMaster.Columns.Add("庫存量", typeof(string));
        dtMaster.Columns.Add("調整量", typeof(int));
        dtMaster.Columns.Add("調整原因", typeof(string));


        for (int i = 1; i < 12; i++)
        {
            DataRow dtMasterRow = dtMaster.NewRow();
            dtMasterRow["項次"] = i;
            dtMasterRow["商品編號"] = "A000" + i;
            dtMasterRow["商品名稱"] = "商品名稱" + i;
            dtMasterRow["庫存量"] = 1500 * (i + 1);
            dtMasterRow["調整量"] = 1000 * (i + 1);
            dtMasterRow["調整原因"] = "調整原因" + i;

            dtMaster.Rows.Add(dtMasterRow);
        }
        return dtMaster;

    }
    

    private DataTable getGvProdDetailData()
    {
        DataTable dtProdDetail = new DataTable();
        //dtProdDetail.Columns.Clear();
        //dtProdDetail.Columns.Add("商品編號", typeof(string));
        //dtProdDetail.Columns.Add("商品名稱", typeof(string));
        //dtProdDetail.Columns.Add("數量", typeof(int));

        //for (int i = 1; i < 2; i++)
        //{
        //    DataRow dtProdDetailRow = dtProdDetail.NewRow();
        //    dtProdDetailRow["商品編號"] = "A000" + i;
        //    dtProdDetailRow["商品名稱"] = "商品名稱" + i;
        //    dtProdDetailRow["數量"] = 100 * (i + 1);
        //    dtProdDetail.Rows.Add(dtProdDetailRow);
        //}
        return dtProdDetail;

    }


    /// <summary>
    /// 新增-顯示Footer
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnNew_Click(object sender, EventArgs e)
    {
        ////divContent.Style["display"] = "";
        //bindgvMaster();

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
            bindgvMaster();
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
            bindEmptyData();
            //gvMaster.DataSource = GetEmptyDataTable();
            //gvMaster.DataBind();
        }
        else
        {
            bindgvMaster();
        }
    }



    protected void btnSave_Click(object sender, EventArgs e)
    {
        lblOrderNo.Text = "SA2011-1008001";
        //"GG000001";

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
