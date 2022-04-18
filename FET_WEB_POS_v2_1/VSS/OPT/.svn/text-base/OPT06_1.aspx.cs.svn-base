using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class VSS_OPT_OPT06_1 : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        bindMasterData();
        btnCommit.Visible = true;
        btnCalcel.Visible = true;
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
        dtResult.Columns.Add("項次", typeof(string));
        dtResult.Columns.Add("門市代碼", typeof(string));
        dtResult.Columns.Add("門市名稱", typeof(string));
     

        for (int i = 1; i < 6; i++)
        {
            DataRow NewRow = dtResult.NewRow();
            NewRow["項次"] = i;
            NewRow["門市代碼"] = "1210" + i;
            NewRow["門市名稱"] = "門市名稱" + i;
            dtResult.Rows.Add(NewRow);
        }
        return dtResult;
    }
}
