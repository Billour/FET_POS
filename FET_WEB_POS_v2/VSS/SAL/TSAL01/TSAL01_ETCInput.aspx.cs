using System;
using System.Data;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FET.POS.Model.Facade.FacadeImpl;

public partial class VSS_SAL_TSAL01_TSAL01_ETCInput : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
		RegisterScript();
		if (!IsPostBack && !IsCallback)
        {
            //Get ETC Product No.
			hfETC_ProdNo.Value = TSAL01_Facade.getFETCProuductNo();
			hfMinLimitAmount.Value = TSAL01_Facade.getFETCLowLimitAmt().ToString();
        }
    }

	private void RegisterScript()
	{
		StringBuilder script = new StringBuilder();
		script.AppendFormat("var hfETC_ProdNo = $('#{0}');\n", hfETC_ProdNo.ClientID);
		script.AppendFormat("var hfMinLimitAmount = $('#{0}');\n", hfMinLimitAmount.ClientID);

		ClientScript.RegisterStartupScript(GetType(), GetType().ToString() + "_RegisterScript", script.ToString(), true);
	}
}
