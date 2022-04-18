using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Web.UI.HtmlControls;
using DevExpress.Web;
using DevExpress.Web.ASPxGridView;
using DevExpress.Web.ASPxPager;

public partial class VSS_OPT_OPT03 : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
       
        if (!Page.IsPostBack)
        {
            string[] ary = { "ALL", "台灣銀行", "遠東銀行", "中國信託", "台新銀行" };
            ddlCardBank.DataSource = ary;
            ddlCardBank.DataBind();
         
            

           string[] ary2 = { "ALL", "行銷部", "HRS","通路管理部" };
           ddlCostCenter.DataSource = ary2;
           ddlCostCenter.DataBind();
           ddlCostCenter.SelectedIndex = 0;
          

            gvMaster.DataSource = GetEmptyDataTable1();
            gvMaster.DataBind();

            gvDetail.DataSource = GetEmptyDataTable2();
            gvDetail.DataBind();

           
            

           
        }
    }
    protected void bindMasterData( )
    {
        DataTable dtGvMaster = new DataTable();
        dtGvMaster = getMasterData();
        ViewState["gvMaster"] = dtGvMaster;
        gvMaster.DataSource = dtGvMaster;
        gvMaster.DataBind();
    }

    private DataTable GetEmptyDataTable1()
    {
        DataTable dt = new DataTable();
        dt.Columns.Add("項次", typeof(string));
        dt.Columns.Add("狀態", typeof(string));
        dt.Columns.Add("發卡銀行", typeof(string));
        dt.Columns.Add("分期期數", typeof(string));
        dt.Columns.Add("分期利率", typeof(string));
        dt.Columns.Add("成本中心", typeof(string));
        dt.Columns.Add("成本中心拆帳比率", typeof(string));
        dt.Columns.Add("開始日期", typeof(string));
        dt.Columns.Add("結束日期", typeof(string));
        dt.Columns.Add("更新日期", typeof(string));
        dt.Columns.Add("更新人員", typeof(string));
        return dt;
    }

    private DataTable getMasterData( )
    {
        DataTable dtMaster = new DataTable();
        dtMaster.Columns.Clear();
        dtMaster.Columns.Add("項次", typeof(string));
        dtMaster.Columns.Add("狀態", typeof(string));
        dtMaster.Columns.Add("發卡銀行", typeof(string));
        dtMaster.Columns.Add("分期期數", typeof(string));
        dtMaster.Columns.Add("分期利率", typeof(string));
        dtMaster.Columns.Add("成本中心", typeof(string));
       
        dtMaster.Columns.Add("開始日期", typeof(string));
        dtMaster.Columns.Add("結束日期", typeof(string));
        dtMaster.Columns.Add("更新日期", typeof(string));
        dtMaster.Columns.Add("更新人員", typeof(string));

       
        string[] array2 = { "台灣銀行", "遠東銀行", "中國信託", "台新銀行" };
        string[] array3 = { "3", "6", "12" };
        for (int i = 1; i < 12; i++)
        {
            DataRow dtMasterRow = dtMaster.NewRow();
            dtMasterRow["項次"] = i;
            dtMasterRow["狀態"] = "有效";
            dtMasterRow["發卡銀行"] = array2[i % 4];
            dtMasterRow["分期期數"] = array3[i % 3];
            dtMasterRow["分期利率"] = (Convert.ToDouble(i)-0.4);
            dtMasterRow["成本中心"] = "成本中心" + i;
            
            dtMasterRow["開始日期"] = "2010/08/01";
            if (i == 2) dtMasterRow["結束日期"] = "";
            else dtMasterRow["結束日期"] = "2010/12/31";
            dtMasterRow["更新日期"] = DateTime.Now.AddDays(-i).AddMinutes(i*32).ToString("yyyy/MM/dd HH:mm:ss");
            dtMasterRow["更新人員"] = "王小明";
            dtMaster.Rows.Add(dtMasterRow);
        }

        DataRow dtMasterRow5 = dtMaster.NewRow();
        dtMasterRow5["項次"] = 5;
        dtMasterRow5["狀態"] = "尚未生效";
        dtMasterRow5["發卡銀行"] = "花旗銀行";
        dtMasterRow5["分期期數"] = "6";
        dtMasterRow5["分期利率"] = "3.6";
        dtMasterRow5["成本中心"] = "成本中心5";
        
        dtMasterRow5["開始日期"] = "2010/10/01";
        dtMasterRow5["結束日期"] = "2011/12/31";
        dtMasterRow5["更新日期"] = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");
        dtMasterRow5["更新人員"] = "王小明";
        dtMaster.Rows.Add(dtMasterRow5);

        return dtMaster;
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        bindMasterData();
        //btnNew.Visible = true;
        //btnDelete.Visible = true;
        //Div1.Visible = true;
    }


    protected void gvMaster_RowEditing(object sender, GridViewEditEventArgs e)
    {
        //GridView gridview = sender as GridView;
        ////設定編輯欄位
        //gridview.EditIndex = e.NewEditIndex;
        ////Bind原查詢資料
        //DataTable dt = ViewState["gvMaster"] as DataTable;
        //gridview.DataSource = dt;
        //gridview.DataBind();

        //TextBox tbSdate = (TextBox)gridview.Rows[e.NewEditIndex].Cells[6].FindControl("tbSDate");


        //if (DateTime.Compare(Convert.ToDateTime(tbSdate.Text), DateTime.Now) > 0)
        //{
        //    Label lbSDate = (Label)gridview.Rows[e.NewEditIndex].Cells[6].FindControl("lbSDate");
        //    lbSDate.Visible = false;
        //    tbSdate.Visible = true;
        //}

        ////DropDownList ddlBank = (DropDownList)gridview.Rows[e.NewEditIndex].Cells[3].FindControl("ddlBank");
        ////Label lbBank = (Label)gridview.Rows[e.NewEditIndex].Cells[3].FindControl("lbBank");
        ////ddlBank.SelectedValue = lbBank.Text;

    }
    protected void gvMaster_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        //GridView gridview = sender as GridView;

        ////取得資料

        ////更新資料庫

        ////取消編輯狀態
        //gridview.EditIndex = -1;

        ////Bind新資料(重取資料)
        //bindMasterData();
    }

    protected void bindDetailData()
    {
        DataTable dtgvDetail = new DataTable();
        dtgvDetail = getDetailData();
        ViewState["gvDetail"] = dtgvDetail;
        gvDetail.DataSource = dtgvDetail;
        gvDetail.DataBind();
    }

    private DataTable GetEmptyDataTable2()
    {
        DataTable dt = new DataTable();
        dt.Columns.Add("項次", typeof(string));
        dt.Columns.Add("成本中心", typeof(string));
        dt.Columns.Add("成本中心拆帳比率", typeof(string));
        
        return dt;
    }

    private DataTable getDetailData()
    {
        DataTable gvDetail = new DataTable();
        gvDetail.Columns.Clear();
        gvDetail.Columns.Add("項次", typeof(string));
        gvDetail.Columns.Add("成本中心", typeof(string));
        gvDetail.Columns.Add("成本中心拆帳比率", typeof(string));

        DataRow gvDetailRow = gvDetail.NewRow();
        gvDetailRow["項次"] = 1;
        gvDetailRow["成本中心"] = "行銷部";
        gvDetailRow["成本中心拆帳比率"] = "0.3";
        gvDetail.Rows.Add(gvDetailRow);

        gvDetailRow = gvDetail.NewRow();
        gvDetailRow["項次"] = 2;
        gvDetailRow["成本中心"] = "通路管理部";
        gvDetailRow["成本中心拆帳比率"] = "0.3";
        gvDetail.Rows.Add(gvDetailRow);

        return gvDetail;
    }

    /// <summary>
    /// 新增-顯示Footer
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnAdd1_Click(object sender, EventArgs e)
    {
        gvMaster.AddNewRow();
       
        //gvMaster.ShowFooter = true;
        //gvMaster.ShowFooterWhenEmpty = true;
        //HtmlTableRow tr = gvMaster.FindChildControl<HtmlTableRow>("trEmptyData");
        //if (tr != null)
        //{
        //    tr.Visible = false;
        //}
        //else
        //{
        //    bindMasterData();
        //}
    }

    /// <summary>
    /// 取消新增-隱藏Footer
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnCancel1_Click(object sender, EventArgs e)
    {
        //gvMaster.ShowFooterWhenEmpty = false;
        //gvMaster.ShowFooter = false;
        //if (gvMaster.Rows.Count == 0)
        //{
        //    gvMaster.DataSource = GetEmptyDataTable1();
        //    gvMaster.DataBind();
        //}
        //else
        //{
        //    bindMasterData();
        //}
    }

    //protected void Card_Click(object sender, EventArgs e)
    //{
    //    gvDetail.Visible = true;
    //    bindDetailData();
       
    //}

    protected void gvMaster_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        //GridView gridview = sender as GridView;
        //gridview.EditIndex = -1;
        ////Bind原查詢資料
        //DataTable dt = ViewState["gvMaster"] as DataTable;
        //gridview.DataSource = dt;
        //gridview.DataBind();
    }
    protected void gvMaster_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "select")
        {
            //dvDetail.Style["display"] = "";
            bindDetailData();
            this.gvDetail.Visible = true;
            //Button1.Visible = Button2.Visible = true;
        }
    }

    /// <summary>
    /// 新增-顯示Footer
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnAdd2_Click(object sender, EventArgs e)
    {
        gvDetail.AddNewRow();
        //gvDetail.ShowFooter = true;
        //gvDetail.ShowFooterWhenEmpty = true;
        //HtmlTableRow tr = gvDetail.FindChildControl<HtmlTableRow>("trEmptyData");
        //if (tr != null)
        //{
        //    tr.Visible = false;
        //}
        //else
        //{
        //    bindDetailData();
        //}
    }

    /// <summary>
    /// 取消新增-隱藏Footer
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnCancel2_Click(object sender, EventArgs e)
    {
        //gvDetail.ShowFooterWhenEmpty = false;
        //gvDetail.ShowFooter = false;
        //if (gvDetail.Rows.Count == 0)
        //{
        //    gvDetail.DataSource = GetEmptyDataTable2();
        //    gvDetail.DataBind();
        //}
        //else
        //{
        //    bindDetailData();
        //}
    }
    protected void gvDetail_RowEditing(object sender, GridViewEditEventArgs e)
    {
        //GridView grDetail = sender as GridView;
        ////設定編輯欄位
        //grDetail.EditIndex = e.NewEditIndex;
        ////Bind原查詢資料
        //DataTable dt = ViewState["gvDetail"] as DataTable;
        //grDetail.DataSource = dt;
        //grDetail.DataBind();
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
    protected void gvDetail_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        //GridView grDetail = sender as GridView;

        ////取得資料

        ////更新資料庫

        ////取消編輯狀態
        //grDetail.EditIndex = -1;

        ////Bind新資料(重取資料)
        //bindDetailData();
    }
    /// <summary>
    /// The HtmlRowPrepared event is raised for each grid row (data row, group row, etc.) 
    /// within the ASPxGridView. 
    /// You can handle this event to change the style settings of individual rows.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
 

    protected void gvMaster_HtmlDataCellPrepared(object sender, ASPxGridViewTableDataCellEventArgs e)
    {
    }
    protected void grid_HtmlRowCreated(object sender, ASPxGridViewTableRowEventArgs e)
    {
        //if (e.RowType == GridViewRowType.Detail)
        //{
        //    // 繫結明細資料表           
        //    ASPxGridView detailGrid = e.Row.FindChildControl<ASPxGridView>("detailGrid");
        //    detailGrid.DataSource = GetDetailData();
        //    detailGrid.DataBind();
        //}
    }

    /// <summary>
    /// 主GridView的分頁變更事件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void grid_PageIndexChanged(object sender, EventArgs e)
    {
        ASPxGridView grid = sender as ASPxGridView;
        grid.DataSource = getMasterData();
        grid.DataBind();
    }

    protected void gvMaster_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
    {
        //若不使用DataSourceID時UPDATE INSERT 和DELPETE 都要自己控制 以下為UPDATE時需自行控制
        ASPxGridView grid = (ASPxGridView)sender;
        DataTable dt = ViewState["gvMaster"] as DataTable ?? null;
        
        grid.CancelEdit();
        e.Cancel = true;
        grid.DataSource = dt;
        grid.DataBind();
    }

    protected void gvMaster_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
    {
        //若不使用DataSourceID時UPDATE INSERT 和DELPETE 都要自己控制 以下為UPDATE時需自行控制
        ASPxGridView grid = (ASPxGridView)sender;
        DataTable dt = ViewState["gvMaster"] as DataTable ?? null;

        grid.CancelEdit();
        e.Cancel = true;
        grid.DataSource = dt;
        grid.DataBind();
    }

    protected void gvDetail_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
    {
        //若不使用DataSourceID時UPDATE INSERT 和DELPETE 都要自己控制 以下為UPDATE時需自行控制
        ASPxGridView grid = (ASPxGridView)sender;
        DataTable ab = ViewState["gvDetail"] as DataTable ?? null;

        grid.CancelEdit();
        e.Cancel = true;
        grid.DataSource = ab;
        grid.DataBind();
    }

    protected void gvDetail_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
    {
        //若不使用DataSourceID時UPDATE INSERT 和DELPETE 都要自己控制 以下為UPDATE時需自行控制
        ASPxGridView grid = (ASPxGridView)sender;
        DataTable ab = ViewState["gvDetail"] as DataTable ?? null;

        grid.CancelEdit();
        e.Cancel = true;
        grid.DataSource = ab;
        grid.DataBind();
    }

    protected void gvMaster_FocusedRowChanged(object sender, EventArgs e)
    {
        if (gvMaster.FocusedRowIndex >= 0)
        {
            gvDetail.Visible = true;
            bindDetailData();
        }
    }
}
