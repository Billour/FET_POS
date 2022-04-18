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

public partial class VSS_Common_ProductsPopup : Popup
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            grid.DataSource = new DataTable();
            grid.DataBind();
        }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        BindData();
        //btnCommit.Visible = true;
        //btnCancel.Visible = true;
    }

    protected void BindData()
    {
        grid.DataSource = GetMasterData();
        grid.DataBind();
    }
    private DataTable GetEmptyDataTable()
    {
        DataTable dtResult = new DataTable();
        dtResult.Columns.Add("商品編號", typeof(string));
        dtResult.Columns.Add("商品名稱", typeof(string));
        //dtResult.Columns.Add("庫存", typeof(string));
        //dtResult.Columns.Add("價格", typeof(string));
        return dtResult;
    }
    private DataTable GetMasterData()
    {
        DataTable dtResult = GetEmptyDataTable();

        for (int i = 0; i <= 10; i++)
        {
            DataRow NewRow = dtResult.NewRow();
            NewRow["商品編號"] = "A0000" + i;
            NewRow["商品名稱"] = "商品名稱" + i;
            //NewRow["庫存"] = i + 3;
            //NewRow["價格"] = 1000 * (i + 1);
            dtResult.Rows.Add(NewRow);
        }
        return dtResult;
    }

    protected void grid_SelectionChanged(object sender, EventArgs e)
    {
        List<object> keys = grid.GetSelectedFieldValues("商品編號");
        int i = grid.FocusedRowIndex;
    }


    protected void OkButton_Click(object sender, EventArgs e)
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
}
