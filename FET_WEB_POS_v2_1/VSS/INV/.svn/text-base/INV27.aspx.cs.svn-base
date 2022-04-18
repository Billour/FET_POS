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
using DevExpress.Web.Data;

public partial class VSS_INV_INV27 : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
    }

    protected void bindMasterData(int TempCount)
    {
        DataTable dtResult = new DataTable();
        dtResult = GetMasterData();

    }

    private DataTable GetMasterData()
    {
        DataTable dtResult = new DataTable();
        dtResult.Columns.Add("拆封日期", typeof(string));
        dtResult.Columns.Add("展示起日", typeof(string));
        dtResult.Columns.Add("展示訖日", typeof(string));
        dtResult.Columns.Add("商品料號", typeof(string));
        dtResult.Columns.Add("商品名稱", typeof(string));
        dtResult.Columns.Add("拆封數量", typeof(int));
        dtResult.Columns.Add("折扣方式", typeof(string));
        dtResult.Columns.Add("金額/占比", typeof(string));
        dtResult.Columns.Add("更新日期", typeof(string));
        dtResult.Columns.Add("更新人員", typeof(string));

        for (int i = 1; i < 5; i++)
        {
            DataRow NewRow = dtResult.NewRow();
            NewRow["拆封日期"] = "2010/09/0" + i;
            NewRow["展示起日"] = "2010/09/1" + i;
            NewRow["展示訖日"] = "2010/09/30";
            NewRow["商品料號"] = "15720000" + i;
            NewRow["商品名稱"] = "商品名稱" + i;
            NewRow["拆封數量"] = i;
            NewRow["折扣方式"] = ((i.ToString() == "2") || (i.ToString() == "3")) ? "金額" : "百分比";
            NewRow["金額/占比"] = ((i.ToString() == "1") || (i.ToString() == "4")) ? "10%" : "100";
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

    protected void Grid_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
    {
        //GridPageSize = int.Parse(e.Parameters);
        grid.SettingsPager.PageSize = int.Parse(e.Parameters);
        grid.DataBind();
    }

    #region Button事件

    //protected void CommandButton_Click(Object sender, CommandEventArgs e)
    //{
    //    tabPage.Visible = true;
    //    detailGrid.DataSource = getGridViewData();
    //    detailGrid.DataBind();      
    //}

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        bindMasterData(1);
        grid.DataSource = GetMasterData();
        grid.DataBind();


        btnSave.Visible = false;
        btnDiscard.Visible = false;
        Button8.Visible = false;
    }

    protected void AddButton_Click(object sender, EventArgs e)
    {
        grid.AddNewRow();
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {

    }

    #endregion

    #region 主GirdView 事件

    protected void grid_PageIndexChanged(object sender, EventArgs e)
    {
        ASPxGridView grid = sender as ASPxGridView;
        grid.DataSource = GetMasterData();
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

    protected void grid_HtmlRowPrepared(object sender, ASPxGridViewTableRowEventArgs e)
    {
        if (e.RowType == GridViewRowType.Data)
        {
            List<object> keyValues = this.grid.GetSelectedFieldValues(grid.KeyFieldName);
            foreach (string key in keyValues)
            {
                if (key == e.GetValue(grid.KeyFieldName).ToString())
                {
                    if (key == this.hdNo.Value)
                    {
                        e.Row.BackColor = System.Drawing.ColorTranslator.FromHtml("#999999");
                        e.Row.ForeColor = System.Drawing.ColorTranslator.FromHtml("#FFFFFF");
                    }
                    else
                    {
                        e.Row.BackColor = System.Drawing.ColorTranslator.FromHtml("#FFFFFF");
                        e.Row.ForeColor = System.Drawing.ColorTranslator.FromHtml("#000000");
                    }

                }
            }
        }
    }

    protected void grid_FocusedRowChanged(object sender, EventArgs e)
    {
        ASPxGridView grid = sender as ASPxGridView;

        if (grid.FocusedRowIndex >= 0)
        {
            this.hdNo.Value = grid.GetRowValues(grid.FocusedRowIndex, grid.KeyFieldName).ToString();

            tabPage.Visible = true;
            detailGrid.DataSource = getGridViewData();
            detailGrid.DataBind();
        }

    }

    protected void grid_CustomCallback(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewCustomCallbackEventArgs e)
    {
        
    }

    #endregion

    #region 明細GridView 事件

    protected void detailGrid_PageIndexChanged(object sender, EventArgs e)
    {
        ASPxGridView grid = sender as ASPxGridView;
        //grid.DataSource = GetDetailData();
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

    protected void detailGrid_DataSelect(object sender, EventArgs e)
    {
        Session["移撥單號"] = (sender as ASPxGridView).GetMasterRowKeyValue();
    }

    #endregion

}
