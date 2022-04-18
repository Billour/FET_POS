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
                if (TargetElementID == null)
                    throw new NullReferenceException("Element2 not found.");

                scriptInit.Append(@"                                                                   
                    var targetElementId = '" + targetElement.ClientID + @"';                                                                                        
                    iframe.popupArguments.controlToAssign = controlCollection.Get(targetElementId) 
                        || document.getElementById(targetElementId);
                    "
                );
            }

            if (!string.IsNullOrEmpty(TargetElementID2))
            {
               Control targetElement2 = NamingContainer.FindControl(TargetElementID2);
               if (targetElement2 == null)
                  throw new NullReferenceException("Element2 not found.");

               scriptInit.Append(@"                                                                   
                    var targetElementId2 = '" + targetElement2.ClientID + @"';                                                                                        
                    iframe.popupArguments.controlToAssign2 = controlCollection.Get(targetElementId2) 
                        || document.getElementById(targetElementId2);
                    "
               );
            }

            if (!string.IsNullOrEmpty(TargetElementID3))
            {
               Control targetElement3 = NamingContainer.FindControl(TargetElementID3);
               if (targetElement3 == null)
                  throw new NullReferenceException("Element3 not found.");

               scriptInit.Append(@"                                                                   
                    var targetElementId3 = '" + targetElement3.ClientID + @"';                                                                                        
                    iframe.popupArguments.controlToAssign3 = controlCollection.Get(targetElementId3) 
                        || document.getElementById(targetElementId3);
                    "
               );
            }

            //jasonChen 20101101
            if (!string.IsNullOrEmpty(AssignToControlId))
            {
                //Control assignToControlId = NamingContainer.FindControl(AssignToControlId);
                Control assignToControlId = this.Parent.Parent.FindControl(AssignToControlId);
                if (assignToControlId == null)
                    throw new NullReferenceException("指定物件不存在.");

                scriptInit.Append(@"                                                                   
                    var assignToControlId = '" + assignToControlId.ClientID + @"';                                                                                        
                    iframe.popupArguments.assignToControl = controlCollection.Get(assignToControlId) 
                        || document.getElementById(assignToControlId);
                    "
                );
            }

            if (!string.IsNullOrEmpty(onOKScript))
            {
               if (onOKScript == null)
                  throw new NullReferenceException("onOKScript not found.");

               scriptInit.Append(@"                                                                   
                    var onOKScript = "+ onOKScript + @";                                                                                        
                    iframe.popupArguments.okscript = onOKScript;
                    "
               );
            }

            
            scriptInit.Append("}");
            this.ClientSideEvents.Init = scriptInit.ToString();
        }

        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);
            ASPxWebControl.RegisterBaseScript(Page);
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

        /// <summary>
        ///指定回傳放值值的元年
        /// </summary>
        public string AssignToControlId
        {
            get
            {
                return (string)ViewState["AssignToControlId"];
            }

            set
            {
                ViewState["AssignToControlId"] = value;
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

        public string TargetElementID2
        {
           get
           {
              return (string)ViewState["TargetElementID2"];
           }

           set
           {
              ViewState["TargetElementID2"] = value;
           }
        }

        public string TargetElementID3
        {
           get
           {
              return (string)ViewState["TargetElementID3"];
           }

           set
           {
              ViewState["TargetElementID3"] = value;
           }
        }

        public string onOKScript
        {
           get
           {
              return (string)ViewState["onOKScript"];
           }

           set
           {
              ViewState["onOKScript"] = value;
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
