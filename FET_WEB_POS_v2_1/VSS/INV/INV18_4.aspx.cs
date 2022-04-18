using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Advtek.Utility;
public partial class VSS_INV_INV18_4 : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            Button1.Visible = false;
            Button21.Visible = false;
        }
        //bindMasterData();
    }
    protected void bindMasterData()
    {
        DataTable dtResult = new DataTable();
        dtResult = getMasterData();
        gvMaster.DataSource = dtResult;
        gvMaster.DataBind();
    }
    private DataTable getMasterData()
    {
        DataTable dtResult = new DataTable();
        dtResult.Columns.Add("員工編號", typeof(string));
        dtResult.Columns.Add("員工名稱", typeof(string));
       
        DataRow NewRow = dtResult.NewRow();
        NewRow["員工編號"] = "60111";
        NewRow["員工名稱"] = "王小毛";   
        dtResult.Rows.Add(NewRow);

        NewRow = dtResult.NewRow();
        NewRow["員工編號"] = "60112";
        NewRow["員工名稱"] = "王小明";
        dtResult.Rows.Add(NewRow);

        NewRow = dtResult.NewRow();
        NewRow["員工編號"] = "60113";
        NewRow["員工名稱"] = "王大明";
        dtResult.Rows.Add(NewRow);

        NewRow = dtResult.NewRow();
        NewRow["員工編號"] = "60114";
        NewRow["員工名稱"] = "王大寶";
        dtResult.Rows.Add(NewRow);

        NewRow = dtResult.NewRow();
        NewRow["員工編號"] = "60115";
        NewRow["員工名稱"] = "孫小毛";
        dtResult.Rows.Add(NewRow);

        NewRow = dtResult.NewRow();
        NewRow["員工編號"] = "60116";
        NewRow["員工名稱"] = "孫大毛";
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
    protected void GridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        //此函式可共用
        GridView gridview = sender as GridView;
        gridview.PageIndex = e.NewPageIndex;

        DataTable dt = ViewState[gridview.ID] as DataTable ?? null;//GridView的資料要存於ViewState以方便共用此Function
        gridview.DataSource = dt;
        gridview.DataBind();
    }

    protected void Button3_Click(object sender, EventArgs e)
    {
        bindMasterData();
        Button1.Visible = true;
        Button21.Visible = true;
    }
}
