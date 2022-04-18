using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Advtek.Utility;
public partial class VSS_INV_INV18_2 : System.Web.UI.Page
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
        dtResult.Columns.Add("原因", typeof(string));
        

        DataRow NewRow = dtResult.NewRow();
        NewRow["原因"] = "公關用";
        
        dtResult.Rows.Add(NewRow);

        NewRow = dtResult.NewRow();
        NewRow["原因"] = "差異調整-Accessory";
        dtResult.Rows.Add(NewRow);

        NewRow = dtResult.NewRow();
        NewRow["原因"] = "差異調整-Handset";
        dtResult.Rows.Add(NewRow);

        NewRow = dtResult.NewRow();
        NewRow["原因"] = "差異調整-Simcard";
        dtResult.Rows.Add(NewRow);

        NewRow = dtResult.NewRow();
        NewRow["原因"] = "配送/贈品-庫存調整";
        dtResult.Rows.Add(NewRow);

        NewRow = dtResult.NewRow();
        NewRow["原因"] = "專案組合-庫存調整";
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
