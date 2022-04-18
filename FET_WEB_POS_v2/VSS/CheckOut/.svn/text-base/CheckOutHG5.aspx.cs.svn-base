using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Advtek.Utility;
using System.Collections.Specialized;

public partial class VSS_CheckOut_CheckOutHG5 : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string HG_CARD_NO = "";
        string TOTAL_AMOUNT = "";

        //**2011/04/27 Tina：前頁傳遞參數時，會先以加密處理，在此要解密。
        if (!string.IsNullOrEmpty(Request.QueryString["Param"]))
        {
            NameValueCollection qscoll = Utils.Param_UrlDecode(Request.QueryString["Param"]);
            foreach (string key in qscoll.AllKeys)
            {
                if (key == "HG_CARD_NO")
                {
                    HG_CARD_NO = string.Join(",", qscoll.GetValues(key));
                }
                else if (key == "TOTAL_AMOUNT")
                {
                    TOTAL_AMOUNT = string.Join(",", qscoll.GetValues(key));
                }
            }
        }
		StringBuilder script = new StringBuilder();
        script.AppendFormat("var CARD_NO = '{0}';\n", HG_CARD_NO);
        script.AppendFormat("var AMOUNT = '{0}';\n", TOTAL_AMOUNT);
		script.AppendFormat("var STORE_NO = '{0}';\n", logMsg.STORENO);
		script.Append("var LEFT_POINT = '0';\n");
		Page.ClientScript.RegisterClientScriptBlock(GetType(), GetType().ToString() + "_StartupScript", script.ToString(), true);
    }
}
