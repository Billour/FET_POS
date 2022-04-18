using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class VSS_CONS_CON03 : BasePage
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
        Div1.Visible = true;
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
        dtResult.Columns.Add("商品編號", typeof(string));
        dtResult.Columns.Add("商品名稱", typeof(string));
        dtResult.Columns.Add("商品類別", typeof(string));
        dtResult.Columns.Add("上架日期", typeof(string));
        dtResult.Columns.Add("下架日期", typeof(string));
        dtResult.Columns.Add("停止訂購日", typeof(string));
        dtResult.Columns.Add("人員", typeof(string));
        dtResult.Columns.Add("日期", typeof(string));

        Random rnd = new Random();
        for (int i = 0; i <= 10; i++)
        {
            DataRow NewRow = dtResult.NewRow();
            NewRow["廠商編號"] = "AC" + i;
            NewRow["商品編號"] = rnd.Next(1000, 9999).ToString() + rnd.Next(1000, 9999).ToString();
            NewRow["商品名稱"] = "手機" + Convert.ToChar(65 + i);
            NewRow["商品類別"] = ddlProductCategory.Items[rnd.Next(1, ddlProductCategory.Items.Count-1)].Text;
            NewRow["上架日期"] = "2010/05/01";
            NewRow["下架日期"] = "2011/06/30";
            NewRow["停止訂購日"] = "2010/07/31";
            NewRow["人員"] = "王小明";
            NewRow["日期"] = "2010/07/13";
            dtResult.Rows.Add(NewRow);
        }
        return dtResult;
    }
}
