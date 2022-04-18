using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Drawing;

public partial class VSS_INV10_INV10 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            // 繫結空的資料表，以顯示表頭欄位
            bindMasterData();
            //gvMaster.DataSource = GetEmptyDataTable();
            //gvMaster.DataBind();
           
        }
        else
        {
            //bindMasterData();
        }
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

    private DataTable GetEmptyDataTable()
    {
        DataTable dtResult = new DataTable();
        dtResult.Columns.Add("盤點單號", typeof(string));
        dtResult.Columns.Add("盤點日期", typeof(string));
        dtResult.Columns.Add("盤點類型", typeof(string));
        dtResult.Columns.Add("盤點狀態", typeof(string));
        dtResult.Columns.Add("盤點人員", typeof(string));
        dtResult.Columns.Add("更新人員", typeof(string));
        dtResult.Columns.Add("更新日期", typeof(string));
         return dtResult;
       

    }
    private DataTable getMasterData()
    {
        DataTable dtResult = GetEmptyDataTable();
        string[] array1 = { "已盤點","已盤點","已盤點","已盤點","未盤點"};
        for (int i = 1; i <= 4; i++)
        {
            DataRow NewRow = dtResult.NewRow();
            NewRow["盤點單號"] = "SC2013-1007001";
            NewRow["盤點日期"] = DateTime.Today.ToString("yyyy/MM/dd");
            NewRow["盤點類型"] = "重盤";
            NewRow["盤點狀態"] = "未盤點";
            NewRow["盤點人員"] = "王小明";
            NewRow["更新人員"] = "王小明";
            NewRow["更新日期"] = DateTime.Today.ToString("yyyy/MM/dd");
            dtResult.Rows.Add(NewRow);
        }
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
        if (e.Row.RowIndex != -1)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (e.Row.Cells[3].Text == "已盤點")
                {
                    for (int i =1; i < e.Row.Cells.Count;i++)
                    e.Row.Cells[i].ForeColor = Color.Red;
                }
            }
        }
    }
}
