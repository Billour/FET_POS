using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Advtek.Utility;

public partial class VSS_LEA02_LEA02 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        

        if (!Page.IsPostBack)
        {
            // 繫結空的資料表，以顯示表頭欄位
            //gvMaster.DataSource = GetEmptyDataTable();
            //gvMaster.DataBind();
            bindMasterData();
            this.btnSearch.Attributes.Add("style", "text-decoration:color:blue;");
        }

       
    }



    protected void bindMasterData()
    {
        DataTable dtResult = new DataTable();
        
        dtResult = getMasterData();
        ViewState["gvMaster"] = dtResult;
        gvMaster.DataSource = dtResult;
        gvMaster.DataBind();
    }
    protected void bindData2(string stockLocal, string mobileType)
    {
        DataTable dtResult = new DataTable();
        dtResult = getData2(stockLocal, mobileType);
        gv2.DataSource = dtResult;
        gv2.DataBind();
    }

   
    private DataTable getMasterData()
    {
        DataTable dtResult = new DataTable();
        dtResult.Columns.Add("庫存地點", typeof(string));
        dtResult.Columns.Add("手機類型", typeof(string));
        dtResult.Columns.Add("庫存量", typeof(string));


        DataRow NewRow = dtResult.NewRow();
        NewRow["庫存地點"] = "台中";
        NewRow["手機類型"] = "手機類型1";
        NewRow["庫存量"] = "30";
        dtResult.Rows.Add(NewRow);

        NewRow = dtResult.NewRow();
        NewRow["庫存地點"] = "台北";
        NewRow["手機類型"] = "手機類型2";
        NewRow["庫存量"] = "13";
        dtResult.Rows.Add(NewRow);


        return dtResult;
    }

    private DataTable getData2(string stockLocal, string mobileType)
    {
        DataTable dtResult = new DataTable();
        dtResult.Columns.Add("庫存地點", typeof(string));
        dtResult.Columns.Add("手機類型", typeof(string));
        dtResult.Columns.Add("手機序號", typeof(string));
        dtResult.Columns.Add("狀態", typeof(string));

        string[] array1 = { "已預約", "未預約" };
        for (int i = 0; i < 10; i++)
        {
            DataRow NewRow = dtResult.NewRow();
            NewRow["庫存地點"] = stockLocal;
            NewRow["手機類型"] = mobileType;
            NewRow["手機序號"] = Guid.NewGuid();
            NewRow["狀態"] = "未預約";
            dtResult.Rows.Add(NewRow);
        }
        return dtResult;
    }



    protected void btnSearch_Click(object sender, EventArgs e)
    {
        gvMaster.Visible = true;
    }



    protected void gvMaster_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "select")
        {
            int index = ((GridViewRow)((Button)e.CommandSource).NamingContainer).RowIndex;
            string strStockLocal = gvMaster.Rows[index].Cells[1].Text;
            string strMobileType = gvMaster.Rows[index].Cells[2].Text;
            bindData2(strStockLocal, strMobileType);
        }
        
    }

    protected void gv2_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            string strStockLocal = e.Row.Cells[1].Text;
            string strMobileType = e.Row.Cells[2].Text;
            string strMobileNo = e.Row.Cells[3].Text;
            string strStatus = e.Row.Cells[4].Text;


            //LinkButton btnReserve = (LinkButton)(e.Row.FindControl("btnReserve"));
            //btnReserve.PostBackUrl = "../LEA/LEA04.aspx?StockLocal=" + strStockLocal + "&MobileType=" + strMobileType + "&MobileNo=" + strMobileNo;
            Button btnReserve = (Button)(e.Row.FindControl("btnReserve"));
            btnReserve.PostBackUrl = "../LEA/LEA05.aspx?StockLocal=" + strStockLocal + "&MobileType=" + strMobileType + "&MobileNo=" + strMobileNo;

            //HyperLink HyperLink1 = (HyperLink)(e.Row.FindControl("HyperLink1"));
            //HyperLink1.NavigateUrl = "../LEA/LEA04.aspx?StockLocal=" + strStockLocal + "&MobileType=" + strMobileType + "&MobileNo=" + strMobileNo;
           
 
        }
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
    protected void gv2_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
}
