using System;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FET.POS.Model.Facade.FacadeImpl;

public partial class VSS_SAL_TSAL01_TSAL01_AddProds : BasePage
{
	string prodNo = string.Empty;
	string storeNo = string.Empty;
	string posuuid_detail = string.Empty;
	protected void Page_Load(object sender, EventArgs e)
	{
		RegisterScript();
	}

	private void RegisterScript()
	{
		StringBuilder script = new StringBuilder();
		script.AppendFormat("var ParentRow = '{0}';\n", Request["pno"]);
		script.AppendFormat("var StoreNo = '{0}';\n", logMsg.STORENO);

		ClientScript.RegisterStartupScript(GetType(), GetType().ToString() + "_RegisterScript", script.ToString(), true);
	}
}
