using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class VSS_LOG_SearchFunCode : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            bindEmptyData();
        }
    }

    protected void bindEmptyData()
    {
        DataTable dtResult = new DataTable();
        gvMaster.DataSource = dtResult;
        gvMaster.DataBind();
    }
    
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        bindMasterData();
        btnCommit.Visible = true;
        Button1.Visible = true;
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
        dtResult.Columns.Add("功能代碼", typeof(string));
        dtResult.Columns.Add("功能名稱", typeof(string));


        for (int i = 0; i < 9; i++)
        {
            DataRow NewRow = dtResult.NewRow();
            NewRow["功能代碼"] = "A0000" + i;
            NewRow["功能名稱"] = "功能名稱" + i;

            dtResult.Rows.Add(NewRow);
        }
        return dtResult;
    }
}
