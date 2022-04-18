using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class VSS_LOG_LOG03a : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            gvMobileStock.DataSource = getData3();
            gvMobileStock.DataBind();
        }
    }

    private DataTable getData3()
    {
        DataTable dtResult = new DataTable();
        dtResult.Columns.Add("門市編號", typeof(string));
        dtResult.Columns.Add("門市名稱", typeof(string));
        dtResult.Columns.Add("員工編號", typeof(string));
        dtResult.Columns.Add("員工姓名", typeof(string));

        DataRow NewRow = dtResult.NewRow();
        NewRow["門市編號"] = "NU001";
        NewRow["門市名稱"] = "內湖門市";
        NewRow["員工編號"] = "1234567";
        NewRow["員工姓名"] = "王小寶";
        dtResult.Rows.Add(NewRow);

        return dtResult;
    }
}
