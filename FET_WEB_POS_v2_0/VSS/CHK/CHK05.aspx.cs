using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using DevExpress.Web;
using DevExpress.Web.ASPxGridView;
using DevExpress.Web.ASPxPager;

public partial class VSS_CHK05_CHK05 : Advtek.Utility.BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            grid.DataSource = GetEmptyMasterData();
            grid.DataBind();
        }
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        BindData();
    }

    protected void AddButton_Click(object sender, EventArgs e)
    {
        grid.AddNewRow();
    }

    protected void BindData()
    {
        grid.DataSource = GetMasterData();
        grid.DataBind();
    }

    private DataTable GetEmptyMasterData()
    {
        DataTable dtResult = new DataTable();
        dtResult.Columns.Add("交易日期", typeof(string));
        dtResult.Columns.Add("機台編號", typeof(string));
        dtResult.Columns.Add("批次", typeof(string));
        dtResult.Columns.Add("繳大鈔", typeof(int));
        dtResult.Columns.Add("更新人員", typeof(string));
        dtResult.Columns.Add("更新日期", typeof(string));
        return dtResult;
    }


    private DataTable GetMasterData()
    {
        DataTable dtResult = GetEmptyMasterData();
        Random rnd = new Random();

        for (int i = 1; i <= 11; i++)
        {
            DataRow NewRow = dtResult.NewRow();
            NewRow["交易日期"] = DateTime.Now.ToString("yyyy/MM/dd");
            NewRow["機台編號"] = rnd.Next(1, 5).ToString("00");
            NewRow["批次"] = i;
            //NewRow["繳大鈔"] = rnd.Next(5, 30) * 1000;
            NewRow["繳大鈔"] = "20000";
            NewRow["更新人員"] = "Jackey";
            NewRow["更新日期"] = DateTime.Now;
            dtResult.Rows.Add(NewRow);
        }
        return dtResult;
    }

    protected void grid_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
    {
        ASPxGridView grid = sender as ASPxGridView;
        grid.CancelEdit();
        e.Cancel = true;
        BindData();
    }

    protected void grid_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
    {
        ASPxGridView grid = sender as ASPxGridView;
        e.Cancel = true;
        grid.CancelEdit();
        BindData();
    }

    protected void grid_PageIndexChanged(object sender, EventArgs e)
    {
        BindData();
    }
}
