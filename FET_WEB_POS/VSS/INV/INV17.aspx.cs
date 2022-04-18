using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Advtek.Utility;

public partial class VSS_INV17_INV17 : System.Web.UI.Page
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
        dtMaster.Columns.Add("關帳年月", typeof(string));
        dtMaster.Columns.Add("關帳日", typeof(string));
        dtMaster.Columns.Add("更新人員", typeof(string));
        dtMaster.Columns.Add("更新日期", typeof(string));
        //dtMaster.Columns.Add("門市庫存量", typeof(int));
        //dtMaster.Columns.Add("在途量", typeof(int));
        //dtMaster.Columns.Add("今日已訂購量", typeof(int));
        //dtMaster.Columns.Add("實際訂購量", typeof(int));
        //dtMaster.Columns.Add("剩餘可訂購量", typeof(int));

        int year = DateTime.Now.Year;
        int month = 0;
        for (int i = 12; i > 0 ; i--)
        {   
            month = i;
            DataRow dtMasterRow = dtMaster.NewRow();
            dtMasterRow["關帳年月"] = year.ToString() + "/" + month.ToString().PadLeft(2, '0');
            dtMasterRow["關帳日"] = year.ToString() + "/" + month.ToString().PadLeft(2, '0') + "/" + DateTime.DaysInMonth(year, month);// DateTime("2010/08/01").AddDays(i).ToShortDateString();
            dtMasterRow["更新人員"] = "更新人員" + i;
            dtMasterRow["更新日期"] = year.ToString() + "/" + month.ToString().PadLeft(2, '0') + "/" + (DateTime.DaysInMonth(year, month) - 1) + " " + DateTime.Now.Hour + ":" + DateTime.Now.Minute + ":" + DateTime.Now.Second;
            //dtMasterRow["門市庫存量"] = 100 * (i + 1);
            //dtMasterRow["在途量"] = 100 * (i + 1);
            //dtMasterRow["今日已訂購量"] = 400 * (i + 1);
            //dtMasterRow["實際訂購量"] = 300 * (i + 1);
            //dtMasterRow["剩餘可訂購量"] = 100 * (i + 1);
            dtMaster.Rows.Add(dtMasterRow);
        }
        return dtMaster;

    }

    private DataTable getGvProdDetailData()
    {
        DataTable dtProdDetail = new DataTable();
        //dtProdDetail.Columns.Clear();
        //dtProdDetail.Columns.Add("關帳日", typeof(string));
        //dtProdDetail.Columns.Add("更新人員", typeof(string));
        //dtProdDetail.Columns.Add("數量", typeof(int));

        
        //int year = DateTime.Now.Year;
        //int month = DateTime.Now.Month;
        //for (int i = 1; i < 2; i++)
        //{
        //    month = i;
        //    DataRow dtProdDetailRow = dtProdDetail.NewRow();
        //    dtProdDetailRow["關帳日"] = year + "/" + month + "/" + DateTime.DaysInMonth(year, month);//Convert.ToDateTime("2010/08/01").AddDays(i).ToShortDateString();
        //    dtProdDetailRow["更新人員"] = "更新人員" + i;
        //    dtProdDetailRow["數量"] = 100 * (i + 1);
        //    dtProdDetail.Rows.Add(dtProdDetailRow);
        //}
        return dtProdDetail;

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
    
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        bindgvMaster();
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {

    }
    protected void gvMaster_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        DataRowView view = e.Row.DataItem as DataRowView;
        if (view!=null)
        {
         
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (e.Row.RowIndex != -1)
                {

                    if (Convert.ToDateTime(view.Row["關帳日"].ToString()) < DateTime.Now)
                   {
                       if (e.Row.FindControl("btnEdit") != null)
                       {
                           Button btnEdit = (Button)(e.Row.FindControl("btnEdit"));
                           btnEdit.Enabled = false;
                           //LinkButton LkEdit = (LinkButton)(e.Row.FindControl("LkEdit"));
                           //LkEdit.Visible = false;
                       }
                       else
                       {
                           Button btnEdit = (Button)(e.Row.FindControl("btnEdit"));
                           btnEdit.Enabled = true ;
                       }
                       //if (e.Row.FindControl("btnChoose") != null)
                       //{
                       //    Button btnChoose = (Button)(e.Row.FindControl("btnChoose"));
                       //    btnChoose.Visible = false;
                       //}
                   }
                }
            }
          
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
    
}
