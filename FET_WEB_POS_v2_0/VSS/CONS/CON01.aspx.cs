using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
public partial class VSS_CON01_CON01 : Advtek.Utility.BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            bindMasterData();
        }
        else
        {
            DataTable dtGvMaster = ViewState["gvMaster"] as DataTable;
            if (dtGvMaster != null)
            {
                gvMaster.DataSource = dtGvMaster;
                gvMaster.DataBind();
            }
        }
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
    }

    private DataTable getMasterData()
    {
        DataTable dtResult = new DataTable();
        dtResult.Columns.Add("廠商編號", typeof(string));
        dtResult.Columns.Add("廠商名稱", typeof(string));
        dtResult.Columns.Add("廠商類別", typeof(string));
        dtResult.Columns.Add("統一編號", typeof(string));
        dtResult.Columns.Add("合作起日", typeof(string));
        dtResult.Columns.Add("合作訖日", typeof(string));
        dtResult.Columns.Add("負責人", typeof(string));
        dtResult.Columns.Add("電話號碼", typeof(string));
        dtResult.Columns.Add("更新人員", typeof(string));
        dtResult.Columns.Add("更新日期", typeof(string));

        Random rnd = new Random();
        string[] ss = { "寄售廠商", "外部廠商" };


        for (int i = 0; i <= 16; i++)
        {
            DataRow NewRow = dtResult.NewRow();
            NewRow["廠商編號"] = "AC" + (i+1).ToString();
            NewRow["廠商名稱"] = "廠商" + Convert.ToChar(65 + i);
            NewRow["廠商類別"] = ss[rnd.Next(0, 10) % 2];
            NewRow["統一編號"] = rnd.Next(1111, 9999).ToString() + rnd.Next(1111, 9999).ToString();
            NewRow["合作起日"] = DateTime.Today.AddDays(-rnd.Next(0, 30)).ToString("yyyy/MM/dd");
            NewRow["合作訖日"] = DateTime.Parse(NewRow["合作起日"].ToString()).AddDays(rnd.Next(30, 90)).ToString("yyyy/MM/dd");
            NewRow["負責人"] = Convert.ToChar(65 + i) + "先生";
            NewRow["電話號碼"] = "02-" + rnd.Next(2000, 2999).ToString() + rnd.Next(1000, 9999).ToString();
            NewRow["更新人員"] = "陳國華";
            NewRow["更新日期"] = "2010/07/13";
            dtResult.Rows.Add(NewRow);
        }
        return dtResult;
    }
}
