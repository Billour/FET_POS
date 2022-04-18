using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Advtek.Utility;
public partial class VSS_INV_INV18 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            // 繫結空的資料表，以顯示表頭欄位
            bindMasterEmptyData();
        }
    }

    protected void bindMasterEmptyData()
    {
        DataTable dtResult = new DataTable();
        ViewState["gvEmptyMaster"] = dtResult;
        gvMaster.DataSource = dtResult;
        gvMaster.DataBind();
    }
    protected void bindMasterData()
    {
        DataTable dtGvMaster = new DataTable();
        dtGvMaster = getMasterData();
        ViewState["gvMaster"] = dtGvMaster;
        gvMaster.DataSource = dtGvMaster;
        gvMaster.DataBind();
    }


    private DataTable getMasterData()
    {
        DataTable dtMaster = new DataTable();
        dtMaster.Columns.Clear();
        dtMaster.Columns.Add("調整單號", typeof(string));
        dtMaster.Columns.Add("門市編號", typeof(string));
        dtMaster.Columns.Add("門市名稱", typeof(string));
        dtMaster.Columns.Add("調整日期", typeof(string));
        dtMaster.Columns.Add("狀態", typeof(string));
        dtMaster.Columns.Add("更新人員", typeof(string));
        dtMaster.Columns.Add("更新日期", typeof(string));

        string[] str = { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z" };
        string[] array1 = { "暫存", "暫存" };

        for (int i = 0; i < 30; i++)
        {
            DataRow dtMasterRow = dtMaster.NewRow();
            // "SA" + 店代號 + "-" + "YYMM" + 流水號(3碼) 

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
            dtMasterRow["調整單號"] = "SA2011-" + DateTime.Now.Year.ToString().Substring(2, 2) + DateTime.Now.ToString("MM") + strLen0 + Convert.ToString(i + 1).ToString(); //"BBA0000" + i;

            dtMasterRow["門市編號"] = "GA0000" + i;
            dtMasterRow["門市名稱"] = "門市" + str[i > 25 ? i - 26 : i];
            dtMasterRow["調整日期"] = Convert.ToDateTime("2010/08/01").AddDays(i).ToShortDateString();
            dtMasterRow["狀態"] = array1[i % 2];
            dtMasterRow["更新人員"] = "王小明";
            dtMasterRow["更新日期"] = "2010/07/12";
            dtMaster.Rows.Add(dtMasterRow);
        }
        return dtMaster;

    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        bindMasterData();
        //div1.Visible = true;
    }

    protected void gvMaster_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            HyperLink hlkDNO = (HyperLink)e.Row.Cells[0].FindControl("HyperLink1");
            hlkDNO.NavigateUrl = "~/VSS/INV/INV18_1.aspx?dno=" + hlkDNO.Text;

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
}
