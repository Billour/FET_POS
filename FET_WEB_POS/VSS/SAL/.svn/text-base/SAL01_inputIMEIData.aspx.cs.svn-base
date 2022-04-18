using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class VSS_SAL_SAL01_inputIMEIData : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            bindMasterData();
            btnCommit.Visible = true;
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
    private DataTable getMasterData()
    {
        DataTable dtResult = new DataTable();
        dtResult.Columns.Add("IMEI", typeof(string));

        for (int i = 0; i < 5; i++)
        {
            DataRow NewRow = dtResult.NewRow();
            NewRow["IMEI"] = string.Format("778{0}-944{0}-564{0}-786{0}", i);
            dtResult.Rows.Add(NewRow);
        }
        return dtResult;
    }
}
