using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class VSS_PRE_PRE01_SelectActivity : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {

        }   
       
    }
    protected void ddlActivity_SelectedIndexChanged(object sender, EventArgs e)
    {
        lbPrepaid.Text = Convert.ToString((ddlActivity.SelectedIndex + 1) * 5000);
    }
}
