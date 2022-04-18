using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class VSS_ORD07_ORD07 : System.Web.UI.Page
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
        dtResult.Columns.Add("主配單號", typeof(string));
        dtResult.Columns.Add("主配日期", typeof(string));
        dtResult.Columns.Add("出貨倉別", typeof(string));
        dtResult.Columns.Add("狀態", typeof(string));
        dtResult.Columns.Add("更新人員", typeof(string));
        dtResult.Columns.Add("更新日期", typeof(string));

        string[] array0 = { "未儲存","已存檔","配送確認","已作廢" };

        for (int i = 0; i < 10; i++)
        {
            DataRow NewRow = dtResult.NewRow();
            NewRow["主配單號"] = "HO10081700" + i;
            NewRow["主配日期"] = "2010/07/12";
            NewRow["出貨倉別"] = "Retail-北";
            NewRow["狀態"] = "已存檔";
            NewRow["更新人員"] = "王小明";
            NewRow["更新日期"] = "2010/07/13";
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
       if (e.Row.RowType == DataControlRowType.DataRow)
       {
          HyperLink hlkDNO = (HyperLink)e.Row.Cells[0].FindControl("hlkdno");
          hlkDNO.NavigateUrl = "~/VSS/ORD/ORD08.aspx?dno=" + hlkDNO.Text;

       }
    }
}
