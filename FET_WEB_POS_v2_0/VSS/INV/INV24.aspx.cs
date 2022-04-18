using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using DevExpress.Web.ASPxGridView;
using DevExpress.Web.ASPxEditors;

public partial class VSS_INV24_INV24 : Advtek.Utility.BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            // 繫結空的資料表，以顯示表頭欄位
            ////gvMaster.DataSource = GetEmptyDataTable();
            ////gvMaster.DataBind();
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
    private DataTable GetEmptyDataTable()
    {
        DataTable dtResult = new DataTable();
        dtResult.Columns.Add("移撥單號", typeof(string));
        dtResult.Columns.Add("撥入門市", typeof(string));
        dtResult.Columns.Add("移出日期", typeof(string));
        dtResult.Columns.Add("撥入日期", typeof(string));
        dtResult.Columns.Add("移撥狀態", typeof(string));
        dtResult.Columns.Add("更新人員", typeof(string));
        dtResult.Columns.Add("更新日期", typeof(string));
        return dtResult;

    }
    private DataTable getMasterData()
    {
        DataTable dtResult = GetEmptyDataTable();

        //string[] ary1 = {"暫存","在途","巳撥入"};
        for (int i = 1; i < 16; i++)
        {
            DataRow NewRow = dtResult.NewRow();
            NewRow["移撥單號"] = "ST2101-100815" + i.ToString("000");
            NewRow["撥入門市"] = "撥入門市" + i.ToString("000");
            //NewRow["移出日期"] = DateTime.Now.ToString("yyyy/MM/dd");
            NewRow["移出日期"] = "2010/08/15";
            NewRow["撥入日期"] = "";
            NewRow["移撥狀態"] = "在途";
            NewRow["更新人員"] = "王小明";
            NewRow["更新日期"] = "2010/07/13";
            dtResult.Rows.Add(NewRow);
        }
        return dtResult;
    }
    protected void bindDetailData()
    {
        DataTable dtgvDetail = new DataTable();
        dtgvDetail = getDetailData();
        ViewState["gvDetail"] = dtgvDetail;
        //gvDetail.DataSource = dtgvDetail;
        //gvDetail.DataBind();
    }
    private DataTable getDetailData()
    {
        DataTable gvDetail = new DataTable();
        gvDetail.Columns.Clear();
        gvDetail.Columns.Add("商品料號", typeof(string));
        gvDetail.Columns.Add("商品名稱", typeof(string));
        gvDetail.Columns.Add("移出數量", typeof(string));
        gvDetail.Columns.Add("IMEI控管", typeof(string));
        gvDetail.Columns.Add("IMEI", typeof(string));
        gvDetail.Columns.Add("IMEI2", typeof(string));

        //Random rnd = new Random();

        //for (int i = 1; i < 5; i++)
        //{
        //    DataRow gvDetailRow = gvDetail.NewRow();
        //    gvDetailRow["商品料號"] = "ST2101-100815001";
        //    gvDetailRow["商品名稱"] = "商品名稱" +  i.ToString("000");
        //    gvDetailRow["移出數量"] = i;
        //    gvDetailRow["IMEI控管"] = ((i % 2) == 0) ? "0" : "1";
        //    gvDetailRow["IMEI"] = "1234567890123" + i.ToString();
        //    gvDetailRow["IMEI2"] = "," + gvDetailRow["IMEI"];
        //    for (int j = 0; j < 3; j++)
        //    {
        //        gvDetailRow["IMEI2"] = gvDetailRow["IMEI2"].ToString() + "\r\n"
        //            + "1234567890123" + j.ToString();
        //    }

        //    gvDetail.Rows.Add(gvDetailRow);
        //}
        DataRow gvDetailRow = gvDetail.NewRow();
        gvDetailRow["商品料號"] = "152301011";
        gvDetailRow["商品名稱"] = "商品名稱000";
        gvDetailRow["移出數量"] = 1;
        gvDetailRow["IMEI控管"] = "1";
        gvDetailRow["IMEI"] = "1";
        gvDetailRow["IMEI2"] = "12345678901231";
        gvDetail.Rows.Add(gvDetailRow);

        gvDetailRow = gvDetail.NewRow();
        gvDetailRow["商品料號"] = "152301012";
        gvDetailRow["商品名稱"] = "商品名稱001";
        gvDetailRow["移出數量"] = 2;
        gvDetailRow["IMEI控管"] = "0";
        gvDetailRow["IMEI"] = "0";
        gvDetail.Rows.Add(gvDetailRow);

        gvDetailRow = gvDetail.NewRow();
        gvDetailRow["商品料號"] = "152301013";
        gvDetailRow["商品名稱"] = "商品名稱002";
        gvDetailRow["移出數量"] = 3;
        gvDetailRow["IMEI控管"] = "1";
        gvDetailRow["IMEI"] = "3";
        gvDetailRow["IMEI2"] = "12345678901240";
         for (int j = 1; j < 3; j++)
            {
                gvDetailRow["IMEI2"] = gvDetailRow["IMEI2"].ToString() + "\r\n"
                    + "1234567890124" + j.ToString();
            }
        gvDetail.Rows.Add(gvDetailRow);

        gvDetailRow = gvDetail.NewRow();
        gvDetailRow["商品料號"] = "152301014";
        gvDetailRow["商品名稱"] = "商品名稱003";
        gvDetailRow["移出數量"] = 4;
        gvDetailRow["IMEI控管"] = "1";
        gvDetailRow["IMEI"] = "4";
        gvDetailRow["IMEI2"] = "12345678901250";
        for (int j = 1; j < 4; j++)
        {
            gvDetailRow["IMEI2"] = gvDetailRow["IMEI2"].ToString() + "\r\n" + "1234567890125" + j.ToString();
        }
        gvDetail.Rows.Add(gvDetailRow);

        gvDetailRow = gvDetail.NewRow();
        gvDetailRow["商品料號"] = "152301015";
        gvDetailRow["商品名稱"] = "商品名稱004";
        gvDetailRow["移出數量"] = 1;
        gvDetailRow["IMEI控管"] = "0";
        gvDetailRow["IMEI"] = "0";
        gvDetail.Rows.Add(gvDetailRow);
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

    #region 分頁相關 (共用)-Detail
    protected void GridView_PageIndexChanging1(object sender, GridViewPageEventArgs e)
    {
        //此函式可共用
        GridView gridview = sender as GridView;
        gridview.PageIndex = e.NewPageIndex;

        DataTable dt = ViewState[gridview.ID] as DataTable ?? null;//GridView的資料要存於ViewState以方便共用此Function
        gridview.DataSource = dt;
        gridview.DataBind();
    }
    protected void btnGoToIndex_Click1(object sender, EventArgs e)
    {
        //此函式可共用
        GridView gridview = (sender as Button).Parent.Parent.Parent.Parent as GridView;
        TextBox textbox = (sender as Button).Parent.FindControl("tbGoToIndex1") as TextBox;
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


    protected void gvMaster_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowIndex != -1)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (e.Row.Cells[5].Text == "已存檔")
                {
                    Button fix = (Button)e.Row.Cells[0].FindControl("btnfix");
                    fix.Visible = true;
                    fix.Enabled = true;
                    Button btnView = (Button)e.Row.Cells[0].FindControl("btnView");
                    btnView.Enabled = true;
                    btnView.Visible = false;

                }
            }
        }
    }

    protected void gvDetail_RowDataBound(object sender, GridViewRowEventArgs e)
    {
       if (e.Row.RowIndex != -1)
       {
          if (e.Row.RowType == DataControlRowType.DataRow)
          {
             HiddenField hfIMEI = (HiddenField)e.Row.Cells[3].FindControl("hidIMEI");
             CheckBox cbIMEI = (CheckBox)e.Row.Cells[3].FindControl("CheckBox3");
             cbIMEI.Checked = (hfIMEI.Value == "1") ? (true) : (false);
          }
       }
    }

    protected void gvMaster_HtmlRowCreated1(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewTableRowEventArgs e)
    {
        if (e.RowType == GridViewRowType.Detail)
        {
            // 繫結明細資料表           
            ASPxGridView detailGrid = e.Row.FindChildControl<ASPxGridView>("gvDetail");
            detailGrid.DataSource = getDetailData();
            detailGrid.DataBind();
        }
    }
}
