using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Advtek.Utility;
public partial class VSS_INV_INV18_3 : System.Web.UI.Page
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
        dtResult.Columns.Clear();
        dtResult.Columns.Add("門市編號", typeof(string));
        dtResult.Columns.Add("門市名稱", typeof(string));
        return dtResult;
    }
    private DataTable getMasterData()
    {
        DataTable dtResult = GetEmptyDataTable();
       
        DataRow NewRow = dtResult.NewRow();
        NewRow["門市編號"] = "2101";
        NewRow["門市名稱"] = "遠企";   
        dtResult.Rows.Add(NewRow);

        NewRow = dtResult.NewRow();
        NewRow["門市編號"] = "2103";
        NewRow["門市名稱"] = "永和";
        dtResult.Rows.Add(NewRow);

        NewRow = dtResult.NewRow();
        NewRow["門市編號"] = "2104";
        NewRow["門市名稱"] = "天母";
        dtResult.Rows.Add(NewRow);

        NewRow = dtResult.NewRow();
        NewRow["門市編號"] = "2106";
        NewRow["門市名稱"] = "板橋";
        dtResult.Rows.Add(NewRow);

        NewRow = dtResult.NewRow();
        NewRow["門市編號"] = "2107";
        NewRow["門市名稱"] = "花蓮";
        dtResult.Rows.Add(NewRow);

        NewRow = dtResult.NewRow();
        NewRow["門市編號"] = "2108";
        NewRow["門市名稱"] = "西門";
        dtResult.Rows.Add(NewRow);

        return dtResult;
    }


    protected void gvMaster_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            //RadioButton radio = e.Row.FindControl("radioChoose") as RadioButton;
            //radio.Attributes["name"] = "<STRONG><FONT color=#ff0000>SameRadio</FONT></STRONG>";
        }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        bindMasterData();
    }

    #region 分頁(共用)
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
