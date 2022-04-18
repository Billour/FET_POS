using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class MasterPage : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void Page_PreRender(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            this.ViewState["PostBackCount"] = -1;
        }
        else
        {
            if (!this.ToolkitScriptManager1.IsInAsyncPostBack)
            {
                this.ViewState["PostBackCount"] = Convert.ToInt16(this.ViewState["PostBackCount"]) - 1;
            }
        }

        ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Common",
                       @"function goBack() {
                            history.go(" + Convert.ToString(this.ViewState["PostBackCount"]) + @");
                        }
                        function isPostBack() {
                            return " + IsPostBack.ToString().ToLower() + @";
                        }", true);

    }
}
