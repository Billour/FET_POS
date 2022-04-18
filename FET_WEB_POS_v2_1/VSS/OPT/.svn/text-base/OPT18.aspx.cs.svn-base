using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using DevExpress.Web;
using DevExpress.Web.ASPxGridView;
using DevExpress.Web.ASPxPager;

public partial class VSS_OPT_OPT18 : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack && !Page.IsCallback)
        {
            grid.DataSource = GetEmptyMasterData();
            grid.DataBind();

            detailGrid.DataSource = GetEmptyDetailData();
            detailGrid.DataBind();
        }
    }

    protected void SearchButton_Click(object sender, EventArgs e)
    {
        BindMasterData();
        BindDetailData();
    }

    protected void AddButton_Click(object sender, EventArgs e)
    {
        grid.AddNewRow();
    }

    protected void BindMasterData()
    {
        grid.DataSource = GetMasterData();
        grid.DataBind();
    }

    private DataTable GetEmptyMasterData()
    {
        DataTable dtResult = new DataTable();
        dtResult.Columns.Add("項次", typeof(string));
        dtResult.Columns.Add("門市編號", typeof(string));
        dtResult.Columns.Add("門市名稱", typeof(string));
        dtResult.Columns.Add("折扣月份", typeof(string));
        dtResult.Columns.Add("折扣總額", typeof(int));
        dtResult.Columns.Add("更新人員", typeof(string));
        dtResult.Columns.Add("更新日期", typeof(DateTime));
        return dtResult;
    }

    private DataTable GetMasterData()
    {
        DataTable dtResult = GetEmptyMasterData();
        string[] ary1 = {"2101","2102","2103","2104","2105" };
        string[] ary2 = { "遠企", "華納", "忠孝", "天母","西門" };

        for (int i = 1; i < 11; i++)
        {
            DataRow NewRow = dtResult.NewRow();
            NewRow["項次"] = i;
            NewRow["門市編號"] = ary1[i % 5];
            NewRow["門市名稱"] = ary2[i % 5];
            NewRow["折扣月份"] = "07/2010";
            NewRow["折扣總額"] = 25000;
            NewRow["更新人員"] = "Jackey";
            NewRow["更新日期"] = DateTime.Now;
            dtResult.Rows.Add(NewRow);
        }
        return dtResult;
    }

    protected void BindDetailData()
    {
        detailGrid.DataSource = GetDetailData();
        detailGrid.DataBind();
    }

    private DataTable GetEmptyDetailData()
    {
        DataTable dtResult = new DataTable();
        dtResult.Columns.Add("項次", typeof(int));
        dtResult.Columns.Add("角色", typeof(string));
        dtResult.Columns.Add("金額", typeof(int));
        dtResult.Columns.Add("比率", typeof(string));
        dtResult.Columns.Add("折扣上限金額", typeof(int));
        return dtResult;
    }

    private DataTable GetDetailData()
    {
        DataTable dtResult = GetEmptyDetailData();

        string[] roles = { "店員", "店長" };
        Random rnd = new Random();
        for (int i = 1; i < 3; i++)
        {
            DataRow NewRow = dtResult.NewRow();
            NewRow["項次"] = i;
            NewRow["角色"] = roles[rnd.Next(0, 1)].ToString();
            NewRow["金額"] = 10000 * i;
            NewRow["比率"] = (50 + i * 10).ToString();
            NewRow["折扣上限金額"] = 25000;
            dtResult.Rows.Add(NewRow);
        }
        return dtResult;
    }

    protected void grid_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
    {
        ASPxGridView grid = sender as ASPxGridView;
        grid.CancelEdit();
        e.Cancel = true;
        //BindMasterData();
    }

    protected void grid_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
    {
        ASPxGridView grid = sender as ASPxGridView;
        e.Cancel = true;
        grid.CancelEdit();
        BindMasterData();
    }

    protected void grid_PageIndexChanged(object sender, EventArgs e)
    {
        BindMasterData();
    }

    protected void detailGrid_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
    {
        ASPxGridView grid = sender as ASPxGridView;
        grid.CancelEdit();
        e.Cancel = true;
        //BindDetailData();
    }

    protected void detailGrid_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
    {
        ASPxGridView grid = sender as ASPxGridView;
        e.Cancel = true;
        grid.CancelEdit();
        BindDetailData();
    }

    protected void detailGrid_PageIndexChanged(object sender, EventArgs e)
    {
        BindDetailData();
    }
}
