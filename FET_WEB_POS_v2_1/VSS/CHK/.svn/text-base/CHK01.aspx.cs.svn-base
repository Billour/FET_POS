using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.Web;
using DevExpress.Web.ASPxGridView;
using DevExpress.Web.ASPxPager;

public partial class VSS_CHK_CHK01 : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            this.ASPxDateEdit1.Text = DateTime.Now.AddDays(-1).ToString("yyyy/MM/dd");

            Label7.Text = DateTime.Now.ToString("yyyy/MM/dd");
            Label6.Text = DateTime.Now.ToString("yyyy/MM/dd HH:mm");
            this.Button4.Enabled = true ;

        }
    }
    protected void btnConfirm_Click(object sender, EventArgs e)
    {
        this.Button4.Enabled = false;
        this.Label3.Text = "結算完成";
        this.TextBox4.Text = "1000";
        this.TextBox5.Text = "1000";
        this.TextBox6.Text = "1000";
        this.TextBox7.Text = "1000";
        this.TextBox8.Text = "1000";
    }
}
