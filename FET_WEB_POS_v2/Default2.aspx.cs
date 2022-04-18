using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Default2 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //Response.Write(Request.ServerVariables["HTTP_X_FORWARDED_FOR"] + "<BR>");
        //Response.Write(Request.ServerVariables["REMOTE_ADDR"] + "<BR>");
        //Response.Write(Request.UserHostAddress + "<BR>");

        string req = Request.Form["req"];
        if (!string.IsNullOrEmpty(req))
        {
            Response.Write("<fet-pos-pay-create-res><Process-Status>1</Process-Status><Error-Message /></fet-pos-pay-create-res>");
        }
        else
        {
            Response.Write("<fet-pos-pay-create-res><Process-Status>0</Process-Status><Error-Message>XXXXXXXXXXX</Error-Message></fet-pos-pay-create-res>");
        }
    }
}
