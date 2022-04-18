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

    protected override void OnPreRender(EventArgs e)
    {
        base.OnPreRender(e);
        if (!ClientScript.IsStartupScriptRegistered(this.GetType(), "Common"))
        {
            RegisterScript("Common", @"
                function getOuterFrame() {   
                    var iframes = window.parent.document.getElementsByTagName(""iframe"");                       
                    for(var i = 0, len = iframes.length; i < len; i++) {                   
                       var doc = iframes[i].contentDocument || iframes[i].contentWindow.document;
                       if (doc === document) {
                          return iframes[i];
                       }
                    }
                    return null;
                }
                function hidePopupWindow() {
                    var outerFrame = getOuterFrame();                                       
                    if (outerFrame !== null) {                                   
                        outerFrame.popupArguments.popupContainer.Hide();
                    }
                }
            ");
        }

    }

    protected void SetReturnValue(string value)
    {
        SetReturnValue(value, false);
    }

    protected void SetReturnValue(string value, bool popupVisible)
    {
        RegisterScript("SetReturnValue", @"                        
            var outerFrame = getOuterFrame();                                       
            if (outerFrame !== null) {
                var controlToAssign = outerFrame.popupArguments.controlToAssign;                    
                if (controlToAssign) {                    
                    if (controlToAssign.SetText)
                        controlToAssign.SetText('" + value + @"');
                    else
                        controlToAssign.value = '" + value + @"';
                }                
            }
        ");

        if (!popupVisible)
        {
            HidePopupWindow();
        }
    }

    protected void HidePopupWindow()
    {
        RegisterScript("HidePopupWindow", @"            
            hidePopupWindow();
        ");
    }


}
