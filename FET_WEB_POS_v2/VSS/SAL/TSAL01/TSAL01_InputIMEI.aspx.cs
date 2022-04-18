using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class VSS_SAL_TSAL01_TSAL01_InputIMEI : BasePage
{
	protected void Page_Load(object sender, EventArgs e)
	{
		if (!IsCallback && !IsPostBack)
		{
			hfRowID.Value = Request["rowid"];
		}
		RegisterScript();
	}

	private void RegisterScript()
	{
		StringBuilder script = new StringBuilder();
		script.AppendFormat("var imgLoadingUrl = '{0}';\n", this.ResolveClientUrl("~/Images/Loading.gif"));
		script.AppendFormat("var hfRowID = $('#{0}');\n", hfRowID.ClientID);
		ClientScript.RegisterStartupScript(GetType(), GetType().ToString() + "_RegisterScript", script.ToString(), true);
	}

}
