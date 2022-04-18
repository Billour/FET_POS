using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class VSS_LOG_ChooseFunctions : Popup
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            bindMasterData();
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
        dtResult.Columns.Add("項次", typeof(int));
        dtResult.Columns.Add("模組名稱", typeof(string));
        dtResult.Columns.Add("功能名稱", typeof(string));

        DataRow NewRow = dtResult.NewRow();
        NewRow["項次"] = 1;
        NewRow["模組名稱"] = "銷售管理";
        NewRow["功能名稱"] = "銷售作業";
        dtResult.Rows.Add(NewRow);

        NewRow = dtResult.NewRow();
        NewRow["項次"] = 2;
        NewRow["模組名稱"] = "銷售管理";
        NewRow["功能名稱"] = "換貨作業";
        dtResult.Rows.Add(NewRow);

        NewRow = dtResult.NewRow();
        NewRow["項次"] = 3;
        NewRow["模組名稱"] = "訂貨管理";
        NewRow["功能名稱"] = "訂貨作業";
        dtResult.Rows.Add(NewRow);

        NewRow = dtResult.NewRow();
        NewRow["項次"] = 4;
        NewRow["模組名稱"] = "訂貨管理";
        NewRow["功能名稱"] = "預訂貨作業";
        dtResult.Rows.Add(NewRow);

        NewRow = dtResult.NewRow();
        NewRow["項次"] = 5;
        NewRow["模組名稱"] = "訂貨管理";
        NewRow["功能名稱"] = "主配作業";
        dtResult.Rows.Add(NewRow);

        return dtResult;
    }

    protected void btnOk_Click(object sender, EventArgs e)
    {
        /*
        foreach (GridViewRow r in gvMaster.Rows)
        {
            RadioButton rb = r.FindControl("RadioButton1") as RadioButton;
            if (rb.Checked)
            {
                ReturnValueToOpener(r.Cells[1].Text);
                return;
            }
        }
        */
        SetReturnValue("OK");
    }
}
