using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class EntryPoint : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string actionID = Request.QueryString["ActionId"];
        if (actionID != null)
        {
            if (actionID.Equals("UC", StringComparison.InvariantCultureIgnoreCase))
            {
                Response.Redirect("~/VSS/SAL/SAL05.aspx");
            }
            else if (actionID.Equals("SC", StringComparison.InvariantCultureIgnoreCase))
            {
                Response.Redirect("~/VSS/SAL/SAL01.aspx");

            }
        }
    }
}
