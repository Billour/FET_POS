using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class VSS_SAL_SAL01_inputPreOrderNumber : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //btnCommit.Visible = btnCancel.Visible = false;
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        bindMasterData();
    }

    protected void bindMasterData()
    {
        DataTable dtResult = new DataTable();
        dtResult = getMasterData();
        ViewState["gvMaster"] = dtResult;
        gvMaster.DataSource = dtResult;
        gvMaster.DataBind();
        btnCommit.Visible = btnCancel.Visible = true;
    }
    private DataTable getMasterData()
    {
        DataTable dtResult = new DataTable();
        dtResult.Columns.Add("預購單號", typeof(string));
        dtResult.Columns.Add("客戶身分證號", typeof(string));
        dtResult.Columns.Add("客戶姓名", typeof(string));
        dtResult.Columns.Add("客戶門號", typeof(string));
        dtResult.Columns.Add("預購金額", typeof(string));


        DataRow NewRow = dtResult.NewRow();
        NewRow["預購單號"] = "PR-1234-01-100912345";
        NewRow["客戶身分證號"] = "A10294XXXX";
        NewRow["客戶姓名"] = "林X明";
        NewRow["客戶門號"] = "0972XXX839";
        NewRow["預購金額"] = "1000";
        dtResult.Rows.Add(NewRow);

        NewRow = dtResult.NewRow();
        NewRow["預購單號"] = "PR-1234-01-100912399";
        NewRow["客戶身分證號"] = "H20282XXXX";
        NewRow["客戶姓名"] = "吳X薇";
        NewRow["客戶門號"] = "0912XXX678";
        NewRow["預購金額"] = "6800";
        dtResult.Rows.Add(NewRow);

        NewRow = dtResult.NewRow();
        NewRow["預購單號"] = "PR-1234-01-101012399";
        NewRow["客戶身分證號"] = "Q20214XXXX";
        NewRow["客戶姓名"] = "吳X民";
        NewRow["客戶門號"] = "0972XXX839";
        NewRow["預購金額"] = "2800";
        dtResult.Rows.Add(NewRow);

        return dtResult;
    }
    protected void radioChoose_CheckedChanged(object sender, EventArgs e)
    {

    }
}
