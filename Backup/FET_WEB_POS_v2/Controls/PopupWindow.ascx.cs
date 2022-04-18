using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

public partial class PopupWindow : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {        
        if (!string.IsNullOrEmpty(PopupButtonID))
        {
            WebControl ctrl = NamingContainer.FindControl(PopupButtonID) as WebControl;            
            ctrl.Attributes.Add("onclick", "javascript:" +
                string.Format("return popupWindow('{0}','{1}', '{2}', '{3}');",
                ResolveUrl(NavigateUrl), Name, GetFeatures(), NamingContainer.FindControl(TargetControlID).ClientID));
        }
    }

    private void RegisterScript(string key, string script)
    {
        if (ScriptManager.GetCurrent(Page) != null)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), key, script, true);
        }
        else
        {
            Page.ClientScript.RegisterStartupScript(Page.GetType(), key, script, true);
        }
    }

    protected void Page_PreRender(object sender, EventArgs e)
    {      
        if (!Page.ClientScript.IsStartupScriptRegistered(Page.GetType(), "PopupWindow"))
        {
            RegisterScript("PopupWindow", @"
                window.popupArguments = {};
                window.popupArguments.onOkScript = " + (string.IsNullOrEmpty(OnOkScript) ? "undefined" : OnOkScript) + @";
                function popupWindow(url, name, features, controlToAssign) {
                    if (controlToAssign && controlToAssign != '')
                        window.popupArguments.controlToAssign = controlToAssign;

                    var newWindow = window.open(url, name, features);
                    if (window.focus) 
                        newWindow.focus(); 
                    
                    return false;
                }
            ");
            /*
            Page.ClientScript.RegisterStartupScript(this.GetType(), "PopupWindow", @"
                window.popupArguments = {};
                window.popupArguments.onOkScript = " + (string.IsNullOrEmpty(OnOkScript) ? "undefined" : OnOkScript) + @";
                function popupWindow(url, name, features, controlToAssign) {
                    if (controlToAssign && controlToAssign != '')
                        window.popupArguments.controlToAssign = controlToAssign;

                    var newWindow = window.open(url, name, features);
                    if (window.focus) 
                        newWindow.focus(); 
                    
                    return false;
                }
            ", true);
            */

        }
    }

    private string GetFeatures()
    {
        return string.Format("width={0},height={1},top={2},left={3},directories={4},resizable={5},menubar={6},scrollbars={7},location={8},toolbar={9},status={10}",
            Width,
            Height,
            Top,
            Left,
            ConvertBoolToYesNo(ShowDirectories),
            ConvertBoolToYesNo(Resizable),
            ConvertBoolToYesNo(ShowMenuBar),
            ConvertBoolToYesNo(ShowScrollBars),
            ConvertBoolToYesNo(ShowLocation),
            ConvertBoolToYesNo(ShowToolBar),
            ConvertBoolToYesNo(ShowStatus));                
    }

    public string Name
    {
        get
        {
            if (ViewState["Name"] == null)
                ViewState["Name"] = "_blank";

            return (string)ViewState["Name"];
        }

        set
        {
            ViewState["Name"] = value;
        }
    }
    
    public int? Width
    {
        get
        {
            return (int?)ViewState["Width"];
        }

        set
        {
            ViewState["Width"] = value;
        }
    }

    public int? Height
    {
        get
        {
            return (int?)ViewState["Height"];
        }

        set
        {
            ViewState["Height"] = value;
        }
    }

    public int? Top
    {
        get
        {
            return (int?)ViewState["Top"];
        }

        set
        {
            ViewState["Top"] = value;
        }
    }

    public int? Left
    {
        get
        {
            return (int?)ViewState["Left"];
        }

        set
        {
            ViewState["Left"] = value;
        }
    }

    public bool? Resizable
    {
        get
        {           
            return (bool?)ViewState["Resizable"];
        }

        set
        {
            ViewState["Resizable"] = value;
        }
    }

    public bool? ShowDirectories
    {
        get
        {
            return (bool?)ViewState["ShowDirectories"];
        }

        set
        {
            ViewState["ShowDirectories"] = value;
        }
    }

    public bool? ShowMenuBar
    {
        get
        {
            return (bool?)ViewState["ShowMenuBar"];
        }

        set
        {
            ViewState["ShowMenuBar"] = value;
        }
    }

    public bool? ShowScrollBars
    {
        get
        {
            return (bool?)ViewState["ShowScrollBars"];
        }

        set
        {
            ViewState["ShowScrollBars"] = value;
        }
    }

    public bool? ShowToolBar
    {
        get
        {
            return (bool?)ViewState["ShowToolBar"];
        }

        set
        {
            ViewState["ShowToolBar"] = value;
        }
    }

    public bool? ShowStatus
    {
        get
        {
            return (bool?)ViewState["ShowStatus"];
        }

        set
        {
            ViewState["ShowStatus"] = value;
        }
    }

    public bool? ShowLocation
    {
        get
        {
            return (bool?)ViewState["ShowLocation"];
        }

        set
        {
            ViewState["ShowLocation"] = value;
        }
    }

    public string NavigateUrl
    {
        get
        {
            return (string)ViewState["NavigateUrl"];
        }

        set
        {
            ViewState["NavigateUrl"] = value;
        }
    }

    public string TargetControlID
    {
        get
        {

            return (string)ViewState["TargetControlID"];
        }

        set
        {
            ViewState["TargetControlID"] = value;
        }
    }

    public string PopupButtonID
    {
        get
        {
            return (string)ViewState["PopupButtonID"];
        }

        set
        {
            ViewState["PopupButtonID"] = value;
        }
    }

    public string OnOkScript
    {
        get
        {
            return (string)ViewState["OnOkScript"];
        }

        set
        {
            ViewState["OnOkScript"] = value;
        }
    }

    public string ConvertBoolToYesNo(bool? value) 
    {
        return value.GetValueOrDefault() ? "yes" : "no";
    }    
}
