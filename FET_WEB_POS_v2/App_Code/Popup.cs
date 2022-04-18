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
using System.Collections.Specialized;

/// <summary>
/// Popup 的摘要描述
/// </summary>
public class Popup : BasePage //System.Web.UI.Page
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

    /// <summary>
    /// 查詢主KEY1
    /// </summary>
    public string KeyFieldValue1
    {
        get
        {
            //object o = Request.QueryString["KeyFieldValue1"];
            //return o != null ? o.ToString() : "";

            string encryptUrl = "";
            //**2011/04/26 Tina：前頁傳遞參數時，會先以加密處理，在此要解密。
            if (!string.IsNullOrEmpty(Request.QueryString["Param"]))
            {
                NameValueCollection qscoll = Utils.Param_UrlDecode(Request.QueryString["Param"]);
                foreach (string key in qscoll.AllKeys)
                {
                    if (key == "KeyFieldValue1")
                    {
                        encryptUrl = string.Join(",", qscoll.GetValues(key));
                        break;
                    }
                }
            }

            return encryptUrl;
            
        }
    }

    /// <summary>
    /// 查詢主KEY2
    /// </summary>
    public string KeyFieldValue2
    {
        get
        {
            //object o = Request.QueryString["KeyFieldValue2"];
            //return o != null ? o.ToString() : "";
            string encryptUrl = "";
            //**2011/04/26 Tina：前頁傳遞參數時，會先以加密處理，在此要解密。
            if (!string.IsNullOrEmpty(Request.QueryString["Param"]))
            {
                NameValueCollection qscoll = Utils.Param_UrlDecode(Request.QueryString["Param"]);
                foreach (string key in qscoll.AllKeys)
                {
                    if (key == "KeyFieldValue2")
                    {
                        encryptUrl = string.Join(",", qscoll.GetValues(key));
                        break;
                    }
                }
            }

            return encryptUrl;
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
        //value += Convert.ToChar(13).ToString();

        RegisterScript("SetReturnValue", @"                        
            var outerFrame = getOuterFrame();  

            if (outerFrame !== null) {
                var controlToAssign = outerFrame.popupArguments.controlToAssign;                    
                if (controlToAssign) {    
                   controlToAssign.Focus();               
                    if (controlToAssign.SetText)
                    {
                        controlToAssign.SetText('" + value + @"');
                    }
                    else
                    {
                        controlToAssign.value = '" + value + @"';
                    }
                   controlToAssign.OnValidation();
                   //controlToAssign.OnTextChanged();
                   controlToAssign.TextChanged.FireEvent(controlToAssign ,null);

                   //OR OnValueChanged() by Tina
                   //controlToAssign.SaveChangedValue()
                   //ASPxClientEdit.prototype.OnValueChanged.call(controlToAssign);
                }                
            }
        ");

        if (!popupVisible)
        {
            HidePopupWindow();
        }
    }


    //jasonChen 20101109 回傳給指定物件的值
    protected void SetReturnControlValue(string value)
    {
        SetReturnControlValue(value, false);
    }

    protected void SetReturnControlValue(string value, bool popupVisible)
    {

        RegisterScript("SetReturnControlValue", @"             
            var outerFrame = getOuterFrame(); 
            if (outerFrame !== null) {
                  var assignToControl = outerFrame.popupArguments.assignToControl;   
                if (assignToControl) {                
                    if (assignToControl.SetText)
                    {
                        assignToControl.SetText('" + value + @"');
                    }
                    else 
                    {
                          assignToControl.innerHTML = '" + value + @"';                      
                          assignToControl.value = '" + value + @"';
                    }   
                  assignToControl.TextChanged.FireEvent(assignToControl ,null);
                }                
            }
        ");

        if (!popupVisible)
        {
           HidePopupWindow();
        }
    }

    protected void SetReturnValue2(string value)
    {
        SetReturnValue2(value, false);
    }

    protected void SetReturnValue2(string value, bool popupVisible)
    {
        //value += Convert.ToChar(13).ToString();

        RegisterScript("SetReturnValue2", @"                        
            var outerFrame = getOuterFrame();                                       
            if (outerFrame !== null) {
                var controlToAssign2 = outerFrame.popupArguments.controlToAssign2;                    
                if (controlToAssign2) {                    
                    if (controlToAssign2.SetText)
                        controlToAssign2.SetText('" + value + @"');
                    else
                        controlToAssign2.value = '" + value + @"';

                     controlToAssign2.Focus();
                }                
            }
        ");

        if (!popupVisible)
        {
            HidePopupWindow();
        }
    }

    protected void SetReturnValue3(string value)
    {
        SetReturnValue3(value, false);
    }

    protected void SetReturnValue3(string value, bool popupVisible)
    {
        //value += Convert.ToChar(13).ToString();

        RegisterScript("SetReturnValue3", @"                        
            var outerFrame = getOuterFrame();                                       
            if (outerFrame !== null) {
                var controlToAssign3 = outerFrame.popupArguments.controlToAssign3;                    
                if (controlToAssign3) {                    
                    if (controlToAssign3.SetText)
                        controlToAssign3.SetText('" + value + @"');
                    else
                        controlToAssign3.value = '" + value + @"';

                     controlToAssign3.Focus();
                }                
            }
        ");

        if (!popupVisible)
        {
            HidePopupWindow();
        }
    }

    protected void SetReturnOKScript()
    {
        SetReturnOKScript(false);
    }

    protected void SetReturnOKScript(bool popupVisible)
    {
        //value += Convert.ToChar(13).ToString();

        RegisterScript("SetReturnOKScript", @"                        
            var outerFrame = getOuterFrame();                                       
            if (outerFrame !== null) {
                var okscript = outerFrame.popupArguments.okscript;                    
                if (okscript) {                    
                        outerFrame.popupArguments.okscript();    
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
