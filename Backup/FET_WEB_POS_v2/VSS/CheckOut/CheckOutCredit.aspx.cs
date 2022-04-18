using System;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Advtek.Utility;
using System.Collections.Specialized;

public partial class VSS_CheckOut_CheckOutCredit : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        RegisterScript();
        ClientScript.RegisterHiddenField("hiddenSTORENO", logMsg.STORENO);


        if (!IsPostBack)
        {
            string ShouldPayAmt = "";
            string Type = "";

            //**2011/04/26 Tina：前頁傳遞參數時，會先以加密處理，在此要解密。
            if (!string.IsNullOrEmpty(Request.QueryString["Param"]))
            {
                NameValueCollection qscoll = Utils.Param_UrlDecode(Request.QueryString["Param"]);
                foreach (string key in qscoll.AllKeys)
                {
                    if (key == "ShouldPayAmt")
                    {
                        ShouldPayAmt = string.Join(",", qscoll.GetValues(key));
                    }
                    else if (key == "Type")
                    {
                        Type = string.Join(",", qscoll.GetValues(key));
                    }

                }
            }

            if (ShouldPayAmt != "")
                txtAMOUNT.Text = ShouldPayAmt;

            if (Type == "2")
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
    }

	private void RegisterScript()
	{
		StringBuilder script = new StringBuilder();
		script.AppendFormat("var txtAMOUNT = $('#{0}');\n", txtAMOUNT.ClientID);

		ClientScript.RegisterStartupScript(GetType(), StringUtil.CStr(GetType()) + "_StartupScript", StringUtil.CStr(script), true);
	}

	protected void btnStep1_OK_Click(object sender, EventArgs e)
	{
		//  divStep1.Style["display"] = "none";
		//  divStep2_1.Style["display"] = "";
	}
	protected void btnStep0_OK_Click(object sender, EventArgs e)
	{
		//divStep0.Style["display"] = "none";
		//divStep1.Style["display"] = "";
	}
}
