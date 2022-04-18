using System; 
using System.Collections; 
using System.ComponentModel; 
using System.Web; 
using System.Web.SessionState; 
using System.Web.UI; 

public class Global : System.Web.HttpApplication 
{ 
    private System.ComponentModel.IContainer components = null; 
    public Global() 
    { 
        InitializeComponent(); 
    } 

    public static System.Web.UI.Control GetPostBackControl(System.Web.UI.Page page) 
    {
        if (!page.IsPostBack && !page.IsCallback)
        {
            return null;
        }

        Control control = null;
        string ctrlname = string.IsNullOrEmpty(page.Request.Params["__EVENTTARGET"]) ? 
            page.Request.Params["__CALLBACKID"] : string.Empty;        
        string param = page.Request.Params["__CALLBACKPARAM"];
       
        if (ctrlname != null && ctrlname != String.Empty) 
        {            
            control = page.FindControl(ctrlname);            
        } 
        // if __EVENTTARGET is null, the control is a button type and we need to 
        // iterate over the form collection to find it 
        else 
        { 
            string ctrlStr = String.Empty; 
            Control c = null; 
            foreach (string ctl in page.Request.Form) 
            {
                if (string.IsNullOrEmpty(ctl))
                    continue;

                if (ctl.EndsWith(".x") || ctl.EndsWith(".y")) 
                { 
                    ctrlStr = ctl.Substring(0,ctl.Length-2);                    
                    c = page.FindControl(ctrlStr);                   
                } 
                else 
                {                   
                    c = page.FindControl(ctl);                   
                } 
                
                if (c is DevExpress.Web.ASPxGridView.ASPxGridView 
                    || c is System.Web.UI.WebControls.Button 
                    || c is System.Web.UI.WebControls.ImageButton 
                    || c is DevExpress.Web.ASPxEditors.ASPxButton) 
                { 
                    control = c; 
                    break; 
                } 
            } 
        } 
        return control; 
    } 

    private void InitializeComponent() 
    { 
        this.components = new System.ComponentModel.Container(); 
    } 
} 
