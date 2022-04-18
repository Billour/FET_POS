using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class VSS_SAL_SAL02_searchDiscountNo : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
       gvMaster.Settings.ShowVerticalScrollBar = true;
        bindMasterData();
        btnCommit.Visible = true;
        btnCancel.Visible = true;
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
        dtResult.Columns.Add("促銷代碼", typeof(string));
        dtResult.Columns.Add("促銷名稱", typeof(string));
        dtResult.Columns.Add("開始日期", typeof(DateTime));
        dtResult.Columns.Add("結束日期", typeof(DateTime));

        for (int i = 0; i < 9; i++)
        {
            DataRow NewRow = dtResult.NewRow();
            NewRow["促銷代碼"] = "促銷代碼" + i;
            NewRow["促銷名稱"] = "促銷名稱" + i;
            NewRow["開始日期"] = DateTime.Today.AddDays(30 - i);
            NewRow["結束日期"] = Convert.ToDateTime(NewRow["開始日期"]).AddMonths(1);
            dtResult.Rows.Add(NewRow);
        }
        return dtResult;
    }
}
