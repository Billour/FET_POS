using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Advtek.Utility;
using System.Data;

public partial class VSS_INV01_INV01 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
           bindEmptyData();

        }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        bindMasterData();
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
        dtResult.Columns.Add("移撥單號", typeof(string));
        dtResult.Columns.Add("狀態", typeof(string));
        dtResult.Columns.Add("移出門市", typeof(string));
        dtResult.Columns.Add("移出日期", typeof(string));
        dtResult.Columns.Add("撥入門市", typeof(string));
        dtResult.Columns.Add("撥入日期", typeof(string));
        dtResult.Columns.Add("更新人員", typeof(string));
        dtResult.Columns.Add("更新日期", typeof(string));

        string[] array0 = { "在途","已撥入" };

        for (int i = 1; i < 7; i++)
        {
           DataRow NewRow = dtResult.NewRow();
           NewRow["移撥單號"] = "ST2013-10071200" + i;
           NewRow["狀態"] = array0[i % 2]; 
           NewRow["移出門市"] = "2103";
           NewRow["移出日期"] = "2010/07/12";
           NewRow["撥入門市"] = "2104";
           if (i % 2 == 1) NewRow["撥入日期"] = "2010/07/12";
           else NewRow["撥入日期"] = "";

           NewRow["更新人員"] = "劉光俊";
           NewRow["更新日期"] = "2010/07/12";
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
        gvDetail.Columns.Add("商品料號", typeof(string));
        gvDetail.Columns.Add("商品名稱", typeof(string));
        gvDetail.Columns.Add("IMEI控管", typeof(string));
        gvDetail.Columns.Add("移出數量", typeof(string));
        gvDetail.Columns.Add("移出IMEI", typeof(string));
        gvDetail.Columns.Add("撥入數量", typeof(string));
        gvDetail.Columns.Add("移入IMEI", typeof(string));


        for (int i = 1; i < 7; i++)
        {
            DataRow gvDetailRow = gvDetail.NewRow();
            gvDetailRow["商品料號"] = "商品料號" + i;
            gvDetailRow["商品名稱"] = "商品名稱" + i;
            gvDetailRow["IMEI控管"] = "7780-9440-5640-7860";
            gvDetailRow["移出數量"] = i % 2;
            gvDetailRow["移出IMEI"] = "7780-9440-5640-7860";
            gvDetailRow["撥入數量"] = i % 2;
            gvDetailRow["移入IMEI"] = "7780-9440-5640-7860";

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
