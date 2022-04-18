using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.Web;
using DevExpress.Web.ASPxGridView;
using DevExpress.Web.ASPxPager;

public partial class VSS_CHK_CHK02 : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            this.ASPxDateEdit1.Text = DateTime.Now.AddDays(-1).ToString("yyyy/MM/dd");

            Label7.Text = DateTime.Now.ToString("yyyy/MM/dd");
            Label6.Text = DateTime.Now.ToString("yyyy/MM/dd HH:mm");
            Button2.Enabled = true;
        }
    }
    protected void btnConfirm_Click(object sender, EventArgs e)
    {
        this.Button2.Enabled = false;
        this.Label3.Text = "讀檔確認";

    }
}
