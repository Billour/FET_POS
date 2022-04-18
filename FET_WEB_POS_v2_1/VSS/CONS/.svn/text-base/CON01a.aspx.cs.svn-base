using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
public partial class VSS_CONS_CON01a : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            gvMaster.DataSource = new DataTable();
            gvMaster.DataBind();
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
        dtResult.Columns.Add("合作模式", typeof(string));
        dtResult.Columns.Add("合作起日", typeof(string));
        dtResult.Columns.Add("合作訖日", typeof(string));
        dtResult.Columns.Add("聯絡人員", typeof(string));
        dtResult.Columns.Add("聯絡電話", typeof(string));
        dtResult.Columns.Add("更新人員", typeof(string));
        dtResult.Columns.Add("更新日期", typeof(string));

        for (int i = 1; i < 20; i++)
        {
            DataRow NewRow = dtResult.NewRow();
            NewRow["廠商編號"] = "AC0000" + i;
            NewRow["廠商名稱"] = "廠商名稱" + i;
            NewRow["廠商類別"] = "類別" + i;
            if (i % 3 == 0) NewRow["統一編號"] = "8011123" + i%6;
            else NewRow["統一編號"] = "1630166" + i%8;
            NewRow["合作模式"] = "專櫃租金";
            NewRow["合作起日"] = System.DateTime.Now.AddMonths(i).AddDays(i*2).ToString("yyyy/MM/dd");
            NewRow["合作訖日"] = "2012/12/31";
            NewRow["聯絡人員"] = "王老闆";
            NewRow["聯絡電話"] = "0912555666";
            NewRow["更新人員"] = "王小明";
            NewRow["更新日期"] = "2010/07/13";
            dtResult.Rows.Add(NewRow);
        }
        return dtResult;
    }
}
