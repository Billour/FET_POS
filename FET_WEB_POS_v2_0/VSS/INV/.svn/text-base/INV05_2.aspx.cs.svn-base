using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Advtek.Utility;
public partial class VSS_INV_INV05_2 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        bindMasterData();
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
        dtResult.Columns.Add("後續代號", typeof(string));
        dtResult.Columns.Add("後續名稱", typeof(string));

        DataRow NewRow = dtResult.NewRow();
        NewRow["後續代號"] = "後續代號1";
        NewRow["後續名稱"] = "後續名稱1";
        dtResult.Rows.Add(NewRow);

        NewRow = dtResult.NewRow();
        NewRow["後續代號"] = "後續代號2";
        NewRow["後續名稱"] = "後續名稱2";
        dtResult.Rows.Add(NewRow);

        NewRow = dtResult.NewRow();
        NewRow["後續代號"] = "後續代號3";
        NewRow["後續名稱"] = "後續名稱3";
        dtResult.Rows.Add(NewRow);

        NewRow = dtResult.NewRow();
        NewRow["後續代號"] = "後續代號4";
        NewRow["後續名稱"] = "後續名稱4";
        dtResult.Rows.Add(NewRow);

        NewRow = dtResult.NewRow();
        NewRow["後續代號"] = "後續代號5";
        NewRow["後續名稱"] = "後續名稱5";
        dtResult.Rows.Add(NewRow);

        NewRow = dtResult.NewRow();
        NewRow["後續代號"] = "後續代號6";
        NewRow["後續名稱"] = "後續名稱6";
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

}
