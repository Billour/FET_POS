using System; 
using System.IO; 
using System.Configuration; 
using System.Data; 
using System.Web.UI; 
using System.Collections;

namespace Advtek.Utility 
{ 
    	public class BasePage : System.Web.UI.Page 
	{

        private ClientCommand _clientCommand = new ClientCommand();


        public ClientCommand ClientCommand
        {
            get { return this._clientCommand; }
        } 
        
		//清除text box 
		public void ClearTextbox(Control parent) 
		{ 
			//Control control1; 
			foreach (Control  control1 in parent.Controls) 
			{ 
				if (control1 is System.Web.UI.WebControls.TextBox) 
				{ 
					((System.Web.UI.WebControls.TextBox)control1).Text = ""; 
				} 
				if (control1.HasControls()) 
				{ 
					this.ClearTextbox(control1); 
				} 
			} 
		}

            protected string GenClientScript()
            {
                ArrayList _jsCmd = _clientCommand.CommonQueue;
                string _result = "";

                _result = "\r\n" + "<Script Language='JavaScript'>" + "\r\n";

                foreach (string jsStmt in _jsCmd)
                {
                    _result += jsStmt + "\r\n";
                }

                _result += "</Script>" + "\r\n";

                return _result;
            } 
        
		//protected override void Render(System.Web.UI.HtmlTextWriter output) 
		//{ 
		//	Response.CacheControl = "no-cache"; 
		//	//防止Cashe，第一行效果最佳(加在程式前面) 
		//	Response.Buffer = true; 
		//	Response.Expires = 0; 
		//	output.Write("<meta http-equiv=\"Pragma\" content=\"no-cache\">" +  "\r\n" ); 
		//	output.Write("<meta http-equiv=\"Cache-control\" content=\"no-cache\">" +  "\r\n" ); 
		//	output.Write("<Script Language='JavaScript' Src='./ClientUtility/Func.Js'></Script>" +  "\r\n" );
            	//	output.Write("<link rel=\"stylesheet\" type=\"text/css\" href=\"" + "./Style/main_style.css\">" + "\r\n"); 
                //	output.Write("<Script Language='JavaScript' Src='../../ClientUtility/jquery.js'></Script>" + "\r\n");
		//	base.Render(output); 
		//	output.Write(GenClientScript()); 
		//	output.Flush(); 
		//} 
        
	} 
    
} 

