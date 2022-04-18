using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Advtek.Utility;

public partial class VSS_CheckOut_CheckOutHG6 : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (IsPostBack)
        {
            //Response.Redirect(hidUrl.Value);

            string[] urls = hidUrl.Value.Split('?');
            string url = urls[0];
            string Param = "";
            if (urls.Length > 1)
            {
                Param = urls[1];
            }
            //**2011/04/27 Tina：傳遞參數時，要先以加密處理。
            string encryptUrl = Utils.Param_Encrypt(Param);
            Response.Redirect(url + string.Format("?Param={0}", encryptUrl));
        }

		StringBuilder script = new StringBuilder();
		script.AppendFormat("var CARD_NO = '{0}';\n", Request["HG_CARD_NO"]);
		script.AppendFormat("var AMOUNT = '{0}';\n", Request["TOTAL_AMOUNT"]);
		script.AppendFormat("var STORE_NO = '{0}';\n", logMsg.STORENO);
		script.Append("var LEFT_POINT = '0';\n");
		Page.ClientScript.RegisterClientScriptBlock(GetType(), GetType().ToString() + "_StartupScript", script.ToString(), true);
    }
}
