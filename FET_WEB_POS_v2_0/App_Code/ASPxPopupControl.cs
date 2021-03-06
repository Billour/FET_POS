using System;
using System.IO;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using DevExpress.Web.ASPxClasses;
using DevExpress.Web.ASPxLoadingPanel;

namespace AdvTek.CustomControls
{
    /// <summary>
    /// ASPxPopupControl 的摘要描述
    /// </summary>
    public class ASPxPopupControl : DevExpress.Web.ASPxPopupControl.ASPxPopupControl
    {
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            StringBuilder scriptInit = new StringBuilder(@"function(s, e) {
                    var iframe = s.GetContentIFrame();                   
                    iframe.popupArguments = {};
                    iframe.contentLoaded = false;
                    var controlCollection = ASPxClientControl.GetControlCollection();                
                    iframe.popupArguments.popupContainer = controlCollection.Get('" + ClientID + @"');"
            );

            if (!string.IsNullOrEmpty(LoadingPanelID))
            {
                ASPxLoadingPanel loadingPanel = NamingContainer.FindControl(LoadingPanelID) as ASPxLoadingPanel;
                if (loadingPanel == null)
                    throw new NullReferenceException("LoadingPanel not found.");

                scriptInit.Append(@"
                    ASPxClientUtils.AttachEventToElement(iframe, 'load', function(e) 
                    {
                        if (!controlCollection.Get('" + ClientID + @"').GetClientVisible()) 
                            return; 
                        controlCollection.Get('" + loadingPanel.ClientID + @"').Hide(); 
                        iframe.contentLoaded = true;
                    });
                ");
                this.ClientSideEvents.Shown = @"function(s, e) {  
                    if (!s.GetContentIFrame().contentLoaded)   
                    ASPxClientControl.GetControlCollection().Get('" + loadingPanel.ClientID + @"')
                        .ShowInElement(s.GetContentIFrame());}";
            }

            if (!string.IsNullOrEmpty(TargetElementID))
            {
                Control targetElement = NamingContainer.FindControl(TargetElementID);
                if (targetElement == null)
                    throw new NullReferenceException("Element not found.");

                scriptInit.Append(@"                                                                   
                    var targetElementId = '" + targetElement.ClientID + @"';                                                                                        
                    iframe.popupArguments.controlToAssign = controlCollection.Get(targetElementId) 
                        || document.getElementById(targetElementId);
                    "
                );
            }
            scriptInit.Append("}");
            this.ClientSideEvents.Init = scriptInit.ToString();
        }

        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);
            ASPxWebControl.RegisterUtilsScript(Page);
        }

        public string LoadingPanelID
        {
            get
            {
                return (string)ViewState["LoadingPanelID"];
            }

            set
            {
                ViewState["LoadingPanelID"] = value;
            }

        }

        public string TargetElementID
        {
            get
            {                
                return (string)ViewState["TargetElementID"];
            }

            set
            {
                ViewState["TargetElementID"] = value;
            }
        }

        //private string ClientContentFrameID
        //{
        //    get
        //    {
        //        return ClientID + "_" + GetContentIFrameID(DefaultWindow);
        //    }
        //}
                 
        public override void RenderControl(HtmlTextWriter writer)
        {
            base.RenderControl(writer);

//            StringBuilder scriptInit = new StringBuilder(@"<script type=""text/javascript""> 
//                (function() {
//                    var frame = document.getElementById('" + ClientContentFrameID + @"'); 
//                    if (frame == null)
//                        return;
//                    frame.popupArguments = {};   
//                    var controlCollection = ASPxClientControl.GetControlCollection();                
//                    frame.popupArguments.popupContainer = controlCollection.Get('" + ClientID + @"');"
//            );

//            if (!string.IsNullOrEmpty(TargetElementID)) {
//                scriptInit.Append(@"                                                                   
//                        var targetElementId = '" + NamingContainer.FindControl(TargetElementID).ClientID + @"';                                                                                        
//                        frame.popupArguments.controlToAssign = controlCollection.Get(targetElementId);
//                        if (frame.popupArguments.controlToAssign == null) {
//                            frame.popupArguments.controlToAssign = document.getElementById(targetElementId);
//                        }"
//                );
//            }                      
//            scriptInit.Append("})();</script>");                
//            writer.Write(scriptInit.ToString());                                        
        }
    }
    
   
}
