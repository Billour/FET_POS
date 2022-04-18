using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class VSS_CheckOut_CheckOutCredit : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
       if (Request.QueryString["Type"] == "2")
       {
          divStep0.Style["display"] = "none";
          divStep1.Style["display"] = "none";
          divStep2_1.Style["display"] = "none";
       }
       else
       {
        divStep1.Style["display"] = "none";
        divsal03_2.Style["display"] = "none";
       }
    }
    protected void btnStep1_OK_Click(object sender, EventArgs e)
    {
        divStep1.Style["display"] = "none";
        divStep2_1.Style["display"] = "";
    }
    protected void btnStep0_OK_Click(object sender, EventArgs e)
    {
        divStep0.Style["display"] = "none";
        divStep1.Style["display"] = "";
    }
}
