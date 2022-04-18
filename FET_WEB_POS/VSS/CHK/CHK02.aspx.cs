using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class VSS_CHK02_CHK02 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
       if (!Page.IsPostBack)
       {
          this.postbackDate_TextBox1.Text = DateTime.Now.AddDays(-1).ToString("yyyy/MM/dd");

          Label7.Text = DateTime.Now.ToString("yyyy/MM/dd");
          Label6.Text = DateTime.Now.ToString("yyyy/MM/dd HH:mm");
       }
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
       this.Button2.Enabled = false;
       this.Label3.Text = "讀檔確認";

    }
}
