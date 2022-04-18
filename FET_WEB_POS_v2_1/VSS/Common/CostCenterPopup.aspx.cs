using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using DevExpress.Web.ASPxGridView;

public partial class VSS_Common_CostCenterPopup : Popup
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            grid.DataSource = new DataTable();
            grid.DataBind();
        }
    }


    protected void BindData()
    {
        grid.DataSource = GetMasterData();
        grid.DataBind();
    }
    private DataTable GetEmptyDataTable()
    {
        DataTable dtResult = new DataTable();
        dtResult.Columns.Add("部門代碼", typeof(string));
        dtResult.Columns.Add("會計科目", typeof(string));
        return dtResult;
    }
    private DataTable GetMasterData()
    {
        DataTable dtResult = GetEmptyDataTable();

        for (int i = 0; i <= 10; i++)
        {
            DataRow NewRow = dtResult.NewRow();
            NewRow["部門代碼"] = "A0000" + i;
            NewRow["會計科目"] = "會計科目" + i;
            dtResult.Rows.Add(NewRow);
        }
        return dtResult;
    }
    protected void okButton_Click(object sender, EventArgs e)
    {
        if (grid.FocusedRowIndex > -1)
        {
            object key = grid.GetRowValues(grid.FocusedRowIndex, grid.KeyFieldName);
            SetReturnValue(key.ToString());
        }
        else
        {
            SetReturnValue(string.Empty);
        }
    }
    protected void grid_PageIndexChanged(object sender, EventArgs e)
    {
        ASPxGridView grid = sender as ASPxGridView;
        grid.DataSource = GetMasterData();
        grid.DataBind();
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        BindData();
    }
}
