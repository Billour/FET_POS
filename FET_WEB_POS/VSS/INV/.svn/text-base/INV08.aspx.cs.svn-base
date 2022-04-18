using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class VSS_INV08_INV08 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        bindMasterData();
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        bindMasterData();
    }

    protected void bindMasterData()
    {
        DataTable dtResult = new DataTable();
        dtResult = getMasterData();
        ViewState["gvMaster"] = dtResult;
        gvMaster.DataSource = dtResult;
        gvMaster.DataBind();
    }
    private DataTable getMasterData()
    {
        DataTable dtResult = new DataTable();
        dtResult.Columns.Add("OE_NO", typeof(string));
        dtResult.Columns.Add("訂單編號", typeof(string));
        dtResult.Columns.Add("驗收單編號", typeof(string));
        dtResult.Columns.Add("門市編號", typeof(string));
        dtResult.Columns.Add("門市名稱", typeof(string));
        dtResult.Columns.Add("驗收狀態", typeof(string));
        dtResult.Columns.Add("驗收日期", typeof(string));
        dtResult.Columns.Add("人員", typeof(string));
        dtResult.Columns.Add("日期", typeof(string));

        string[] array1 = { "未驗收", "部份驗收", "已結案" };
        DataRow NewRow = dtResult.NewRow();
        NewRow["OE_NO"] = "001";
        NewRow["訂單編號"] = "HR1007001";
        NewRow["驗收單編號"] = "";
        NewRow["門市編號"] = "2103";
        NewRow["門市名稱"] = "永和";
        NewRow["驗收狀態"] = "未驗收";
        NewRow["驗收日期"] = "";
        NewRow["人員"] = "";
        NewRow["日期"] = "";
        dtResult.Rows.Add(NewRow);

        NewRow = dtResult.NewRow();
        NewRow["OE_NO"] = "001-1";
        NewRow["訂單編號"] = "HR1007002";
        NewRow["驗收單編號"] = "SR2104-1007001";
        NewRow["門市編號"] = "2104";
        NewRow["門市名稱"] = "永和";
        NewRow["驗收狀態"] = "部份驗收";
        NewRow["驗收日期"] = "2010/07/01";
        NewRow["人員"] = "12345-王曉明";
        NewRow["日期"] = "2010/07/01";
        dtResult.Rows.Add(NewRow);
        
        return dtResult;
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
    protected void gvMaster_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (e.Row.Cells[5].Text != "已結案")
            {
                for (int i = 1; i <= e.Row.Cells.Count - 1; i++)
                {
                    e.Row.Cells[i].ForeColor = System.Drawing.Color.Red;
                }
            }
        }
    }
}
