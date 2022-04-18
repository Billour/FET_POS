using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class VSS_SA01_SA01NoCheckOut : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void bindMasterData()
    {
        DataTable dtResult = new DataTable();
        dtResult = getMasterData();
        gvMaster.DataSource = dtResult;
        gvMaster.DataBind();
        divContent.Style["display"] = "";
    }

    private DataTable getMasterData()
    {
        DataTable dtResult = new DataTable();
        dtResult.Columns.Add("門號", typeof(string));
        dtResult.Columns.Add("交易日期", typeof(string));
        dtResult.Columns.Add("服務類別", typeof(string)); 

        for (int i = 0; i <= 5; i++)
        {
            DataRow NewRow = dtResult.NewRow();
            NewRow["門號"] = "091112345"+i;
            NewRow["交易日期"] = "2010/07/01";
            NewRow["服務類別"] = "IA"; 
            dtResult.Rows.Add(NewRow);
        }
        return dtResult;
    }


    protected void btnSearch_Click(object sender, EventArgs e)
    {
        bindMasterData();
    }
}
