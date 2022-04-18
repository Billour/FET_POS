using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Advtek.Utility;
using System.Collections;
using DevExpress.Web;
using DevExpress.Web.ASPxGridView;
using DevExpress.Web.ASPxPager;

public partial class VSS_INV_INV27 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            
        }
    }
    protected void bindMasterData(int TempCount)
    {
        DataTable dtResult = new DataTable();
        dtResult = GetMasterData();
        //ViewState["gvMaster"] = dtResult;
        //gvMaster.DataSource = dtResult;
        //gvMaster.DataBind();

        //ViewState["gvDetail"] = getGridViewData();
        //gvDetail.DataSource = (DataTable)ViewState["gvDetail"];
        //gvDetail.DataBind();

       
    }
    private DataTable GetMasterData()
    {
        DataTable dtResult = new DataTable();
        dtResult.Columns.Add("拆封日期", typeof(string));
        dtResult.Columns.Add("展示起日", typeof(string));
        dtResult.Columns.Add("展示訖日", typeof(string));
        dtResult.Columns.Add("商品料號", typeof(string));
        dtResult.Columns.Add("商品名稱", typeof(string));
        dtResult.Columns.Add("拆封數量", typeof(string));
        dtResult.Columns.Add("折扣方式", typeof(string));
        dtResult.Columns.Add("金額/占比", typeof(string));
        dtResult.Columns.Add("更新日期", typeof(string));
        dtResult.Columns.Add("更新人員", typeof(string));
       
        for (int i = 1; i < 4; i++)
        {
            DataRow NewRow = dtResult.NewRow();
            NewRow["拆封日期"] = "2010/09/0" + i;
            NewRow["展示起日"] = "2010/09/1" + i;
            NewRow["展示訖日"] = "2010/09/30";
            NewRow["商品料號"] = "15720000"+i;
            NewRow["商品名稱"] = "商品名稱"+i;
            NewRow["拆封數量"] = i - 1;
            NewRow["折扣方式"] = "2%";
            NewRow["金額/占比"] = "100";
            NewRow["更新日期"] = "2010/08/31";
            NewRow["更新人員"] = "王大同";
            dtResult.Rows.Add(NewRow);
        }

        return dtResult;
    }

    private DataTable getGridViewData()
    {
        DataTable dtResult = new DataTable();
        dtResult.Columns.Add("門市編號", typeof(string));
        dtResult.Columns.Add("門市名稱", typeof(string));
        dtResult.Columns.Add("區域別", typeof(string));

        DataRow NewRow = dtResult.NewRow();
        NewRow["門市編號"] = "2103";
        NewRow["門市名稱"] = "永和門市";
        NewRow["區域別"] = "北一區";

        dtResult.Rows.Add(NewRow);
        return dtResult;
    }

    protected void CommandButton_Click(Object sender, CommandEventArgs e)
    {
        tabPage.Visible = true;
        //grid.Selection.UnselectAll();
        //grid.Selection.SetSelectionByKey(e.CommandArgument, true);
        detailGrid.DataSource = getGridViewData();
        detailGrid.DataBind();
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        bindMasterData(1);
       // Button1.Visible = true;
       // Button2.Visible = true;

        grid.DataSource = GetMasterData();
        grid.DataBind();


        //TabContainer1.Visible = false;
        btnSave.Visible = false;
        btnDiscard.Visible = false;
        Button8.Visible = false;
    }

    protected void AddButton_Click(object sender, EventArgs e)
    {
        grid.AddNewRow();
    }

    protected void Button4_Click(object sender, EventArgs e)
    {

    }
    protected void btnSave_Click(object sender, EventArgs e)
    {

    }

    protected void Grid_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
    {
        //GridPageSize = int.Parse(e.Parameters);
        grid.SettingsPager.PageSize = int.Parse(e.Parameters);
        grid.DataBind();
    }

    protected void detailGrid_DataSelect(object sender, EventArgs e)
    {
        Session["移撥單號"] = (sender as ASPxGridView).GetMasterRowKeyValue();
    }

    /// <summary>
    /// The HtmlRowPrepared event is raised for each grid row (data row, group row, etc.) 
    /// within the ASPxGridView. 
    /// You can handle this event to change the style settings of individual rows.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void grid_HtmlRowPrepared(object sender, ASPxGridViewTableRowEventArgs e)
    {

        /*
        if (e.RowType == GridViewRowType.Detail)
        {
            // 繫結明細資料表           
            ASPxGridView detailGrid = e.Row.FindChildControl<ASPxGridView>("detailGrid");
            detailGrid.DataSource = GetDetailData();
            detailGrid.DataBind();            
        }
        */
    }

    protected void grid_HtmlRowCreated(object sender, ASPxGridViewTableRowEventArgs e)
    {
        if (e.RowType == GridViewRowType.Detail)
        {
             
            
        }
    }

    /// <summary>
    /// 主GridView的分頁變更事件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void grid_PageIndexChanged(object sender, EventArgs e)
    {
        ASPxGridView grid = sender as ASPxGridView;
        grid.DataSource = GetMasterData();
        grid.DataBind();
    }

    /// <summary>
    /// 明細GridView的分頁變更事件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void detailGrid_PageIndexChanged(object sender, EventArgs e)
    {
        ASPxGridView grid = sender as ASPxGridView;
        //grid.DataSource = GetDetailData();
        grid.DataBind();
    }

    protected void grid_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
    {
        ASPxGridView grid = sender as ASPxGridView;
        grid.CancelEdit();
        e.Cancel = true;
        grid.DataSource = GetMasterData();
        grid.DataBind();
    }

    protected void grid_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
    {
        ASPxGridView grid = sender as ASPxGridView;
        e.Cancel = true;
        grid.CancelEdit();

        grid.DataSource = GetMasterData();
        grid.DataBind();
    }

    protected void detailGrid_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
    {
        ASPxGridView grid = sender as ASPxGridView;
        grid.CancelEdit();
        e.Cancel = true;
        grid.DataSource = getGridViewData();
        grid.DataBind();
    }


    protected void detailGrid_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
    {
        ASPxGridView grid = sender as ASPxGridView;
        e.Cancel = true;
        grid.CancelEdit();

        grid.DataSource = getGridViewData();
        grid.DataBind();
    }
}
