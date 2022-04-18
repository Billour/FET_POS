using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

/// <summary>
/// Popup 的摘要描述
/// </summary>
public class Popup : System.Web.UI.Page
{
    public Popup()
	{
		//
		// TODO: 在此加入建構函式的程式碼
		//       
	}

    private void RegisterScript(string key, string script)
    {
        if (ScriptManager.GetCurrent(this) != null)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), key, script, true);
        }
        else
        {
            ClientScript.RegisterStartupScript(this.GetType(), key, script, true);
        }
    }

    protected void ReturnValueToOpener(string value)
    {
        RegisterScript("ReturnValue", @"
        (function() {
            var parentWindow = window.opener;
            if (parentWindow && !parentWindow.closed) {            
                if (parentWindow.popupArguments) {
                    var controlToAssign = parentWindow.popupArguments.controlToAssign;
                    if (controlToAssign) {
                        parentWindow.document.getElementById(controlToAssign).value = '" + value + @"';
                    }
                }                       
            }
            window.close();
        })();");
    }
}
