using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Advtek.Utility;

public partial class VSS_CON10_chooseSupplierNo : System.Web.UI.Page
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
        else
        {
            DataTable dtResult = ViewState["gvMaster"] as DataTable;
            if (dtResult != null)
            {
                gvMaster.DataSource = dtResult;
                gvMaster.DataBind();
            }
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
        dtResult.Columns.Add("廠商代號", typeof(string));
        dtResult.Columns.Add("廠商名稱", typeof(string));
        return dtResult;
    }
    private DataTable getMasterData()
    {
        DataTable dtResult = GetEmptyDataTable();
       
        DataRow NewRow = dtResult.NewRow();
        NewRow["廠商代號"] = "2101";
        NewRow["廠商名稱"] = "廠商1";   
        dtResult.Rows.Add(NewRow);

        NewRow = dtResult.NewRow();
        NewRow["廠商代號"] = "2103";
        NewRow["廠商名稱"] = "廠商2";
        dtResult.Rows.Add(NewRow);

        NewRow = dtResult.NewRow();
        NewRow["廠商代號"] = "2104";
        NewRow["廠商名稱"] = "廠商3";
        dtResult.Rows.Add(NewRow);

        NewRow = dtResult.NewRow();
        NewRow["廠商代號"] = "2106";
        NewRow["廠商名稱"] = "廠商4";
        dtResult.Rows.Add(NewRow);

        NewRow = dtResult.NewRow();
        NewRow["廠商代號"] = "2107";
        NewRow["廠商名稱"] = "廠商5";
        dtResult.Rows.Add(NewRow);

        NewRow = dtResult.NewRow();
        NewRow["廠商代號"] = "2108";
        NewRow["廠商名稱"] = "廠商6";
        dtResult.Rows.Add(NewRow);

        return dtResult;
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        bindMasterData();
    }
}
