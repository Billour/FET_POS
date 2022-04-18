using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class VSS_OPT_OPT13_choseStore : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {


            // 繫結空的資料表，以顯示表頭欄位
            gvMaster.DataSource = GetEmptyDataTable();
            gvMaster.DataBind();
        }
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        bindMasterData();
        btnCommit.Visible = true;
    }

    private DataTable GetEmptyDataTable()
    {
        DataTable dtResult = new DataTable();
        dtResult.Columns.Add("門市編號", typeof(string));
        dtResult.Columns.Add("門市名稱", typeof(string));
        dtResult.Columns.Add("區域別", typeof(string));

        return dtResult;
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
        dtResult.Columns.Add("門市編號", typeof(string));
        dtResult.Columns.Add("門市名稱", typeof(string));
        dtResult.Columns.Add("區域別", typeof(string));

        for (int i = 0; i < 9; i++)
        {
            DataRow NewRow = dtResult.NewRow();
            NewRow["門市編號"] = "A0000" + i;
            NewRow["門市名稱"] = "門市名稱" + i;
            NewRow["區域別"] = "區域別" + i;
            dtResult.Rows.Add(NewRow);
        }
        return dtResult;
    }
}
