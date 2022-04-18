using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class VSS_Common_ChooseConsignVendor : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        bindMasterData();
        btnCommit.Visible = true;
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
        dtResult.Columns.Add("廠商編號", typeof(string));
        dtResult.Columns.Add("廠商名稱", typeof(string));


        for (int i = 1; i < 5; i++)
        {
            DataRow NewRow = dtResult.NewRow();
            NewRow["廠商編號"] = "AC00" + i;
            NewRow["廠商名稱"] = "廠商名稱" + i;

            dtResult.Rows.Add(NewRow);
        }
        return dtResult;
    }
    protected void radioChoose_CheckedChanged(object sender, EventArgs e)
    {

        RadioButton rbData = (RadioButton)sender;

        foreach (GridViewRow gvrFor in gvMaster.Rows)
            ((RadioButton)gvrFor.Cells[0].Controls[1]).Checked = false;

        rbData.Checked = true;

    }
}
