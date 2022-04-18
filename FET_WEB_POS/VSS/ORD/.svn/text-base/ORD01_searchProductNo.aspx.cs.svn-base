using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
public partial class VSS_ORD_ORD01_searchProductNo : Popup//System.Web.UI.Page
{

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            //Button1.Visible = false;
            //Button21.Visible = false;

            // 繫結空的資料表，以顯示表頭欄位
            gvMaster.DataSource = GetEmptyDataTable();
            gvMaster.DataBind();
        }

    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        bindMasterData();
        //btnCommit.Visible = true;
        //btnCancel.Visible = true;
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
        dtResult.Columns.Add("商品編號", typeof(string));
        dtResult.Columns.Add("商品名稱", typeof(string));
        //dtResult.Columns.Add("庫存", typeof(string));
        //dtResult.Columns.Add("價格", typeof(string));
        return dtResult;
    }
    private DataTable getMasterData()
    {
        DataTable dtResult = GetEmptyDataTable();

        for (int i = 0; i < 9; i++)
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
    protected void btnCommit_Click(object sender, EventArgs e)
    {
        foreach (GridViewRow r in gvMaster.Rows)
        {
            RadioButton c = r.FindControl("radioChoose") as RadioButton;
            if (c.Checked)
            {
                ReturnValueToOpener(r.Cells[1].Text);
                return;
            }
        }


        ReturnValueToOpener("OK");
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
}
