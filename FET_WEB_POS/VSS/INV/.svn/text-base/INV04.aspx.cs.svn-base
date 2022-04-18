using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class VSS_INV04_INV04 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            // 繫結空的資料表，以顯示表頭欄位
            gvMaster.DataSource = GetEmptyDataTable();
            gvMaster.DataBind();
           // bindMasterEmptyData();
        }
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        bindMasterData();
        divContent.Visible = true;
    }
    protected void bindMasterData()
    {
        DataTable dtResult = new DataTable();
        dtResult = getMasterData();
        ViewState["gvMaster"] = dtResult;
        gvMaster.DataSource = dtResult;
        gvMaster.DataBind();
    }
    private DataTable  GetEmptyDataTable()
    {
        DataTable dtResult = new DataTable();
        dtResult.Columns.Add("退倉單號", typeof(string));
        dtResult.Columns.Add("退倉狀態", typeof(string));
        dtResult.Columns.Add("退倉開始日", typeof(string));
        dtResult.Columns.Add("退倉結束日", typeof(string));
        dtResult.Columns.Add("退倉原因", typeof(string));
        dtResult.Columns.Add("更新人員", typeof(string));
        dtResult.Columns.Add("更新日期", typeof(string));
        return dtResult;
    }

    private DataTable getMasterData()
    {
        DataTable dtResult = GetEmptyDataTable();
        
        for (int i = 0; i <= 5; i++)
        {
            string strLen0 = "";
            switch ((i + 1).ToString().Length)
            {
                case 1:
                    strLen0 = "00";
                    break;
                case 2:
                    strLen0 = "0";
                    break;
                case 3:
                    strLen0 = "";
                    break;
            }


            DataRow NewRow = dtResult.NewRow();
            NewRow["退倉單號"] = "HR" + DateTime.Now.Year.ToString().Substring(2, 2) + DateTime.Now.ToString("MM") + Convert.ToDateTime(DateTime.Now).ToString("dd") + strLen0 + Convert.ToString(i + 1).ToString();
            NewRow["退倉狀態"] = "已存檔";
            NewRow["退倉開始日"] = "2010/07/15";
            NewRow["退倉結束日"] = "2010/07/20";
            NewRow["退倉原因"] = "退倉原因" + i;

            NewRow["更新人員"] = "更新人員" + i;
            NewRow["更新日期"] = "2010/07/14";
            dtResult.Rows.Add(NewRow);
        }
        return dtResult;
    }

    protected void gvMaster_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            HyperLink hlkDNO = (HyperLink)e.Row.Cells[0].FindControl("HyperLink1");
            hlkDNO.NavigateUrl = "~/VSS/INV/INV05.aspx?dno=" + hlkDNO.Text;

        }
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

    protected void bindMasterEmptyData()
    {
        DataTable dtResult = new DataTable();
        ViewState["gvEmptyMaster"] = dtResult;
        gvMaster.DataSource = dtResult;
        gvMaster.DataBind();
    }
}
