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
using DevExpress.Web.Data;

public partial class VSS_Common_ProductCategoryPopup : Popup
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack && !IsCallback)
        {
            // 繫結空的資料表，以顯示表頭欄位
            grid.DataSource = new DataTable();
            grid.DataBind();
        }
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        BindData();
        //btnCommit.Visible = true;
    }

    protected void BindData()
    {
        grid.DataSource = GetMasterData();
        grid.DataBind();
    }
    private DataTable GetMasterData()
    {
        DataTable dtResult = new DataTable();
        dtResult.Columns.Add("類別編號", typeof(string));
        dtResult.Columns.Add("類別名稱", typeof(string));

        string[] array1 = { "10", "11", "15", "20", "30" };
        string[] array2 = { "Handset-Selling", "Handset-Rental/Spare Part", "3G Handset", "SIM Card-Selling", "Accessory-Selling" };


        for (int i = 0; i < 9; i++)
        {
            DataRow NewRow = dtResult.NewRow();
            NewRow["類別編號"] = array1[i % 5];
            NewRow["類別名稱"] = array2[i % 5];

            dtResult.Rows.Add(NewRow);
        }
        return dtResult;
    }

    protected string GetFieldValue(object item)
    {
        WebDescriptorRowBase row = item as WebDescriptorRowBase;
        return grid.GetRowValues(row.VisibleIndex, grid.KeyFieldName).ToString();
    }

    protected string GetFieldChecked(object item)
    {
        if (Session["FocusedRow"] == null)
            return string.Empty;

        WebDescriptorRowBase row = item as WebDescriptorRowBase;
        object obj = grid.GetRowValues(row.VisibleIndex, grid.KeyFieldName);
        return Session["FocusedRow"] == obj ? "checked" : string.Empty;
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

}
