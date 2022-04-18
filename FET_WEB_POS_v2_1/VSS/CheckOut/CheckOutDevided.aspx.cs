using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class VSS_CheckOut_CheckOutDevided : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void btnStep1_OK_Click(object sender, EventArgs e)
    {
        divStep1.Style["display"] = "none";
        divStep2.Style["display"] = "";
    }
}
