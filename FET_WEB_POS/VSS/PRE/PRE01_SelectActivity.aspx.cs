using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class VSS_PRE_PRE01_SelectActivity : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            string[] array2 = { "請選擇", "PR-010203 iPhone4G預購", "PR-020103 HTC Desire HD 預購" };
            ddlActivity.DataSource = array2;
            ddlActivity.DataBind();
            ddlActivity.SelectedIndex = 0;
        }   
       
    }
    protected void ddlActivity_SelectedIndexChanged(object sender, EventArgs e)
    {
        //lbPrice.Text = Convert.ToString((ddlActivity.SelectedIndex + 1) * 1500);
        lbPrepaid.Text = Convert.ToString((ddlActivity.SelectedIndex + 1) * 5000);
    }
}
