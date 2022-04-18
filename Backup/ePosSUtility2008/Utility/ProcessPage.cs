using System; 
using System.Configuration; 
using System.Data; 
using System.Web.UI; 
using System.Text; 
using System.Xml;

namespace Advtek.Utility 
{ 
	public class ProcessPage : BasePage 
	{


        protected static string __VIRTUAL_DIR = ConfigurationManager.AppSettings["Virtial_Directory"];
        protected static string __LOGIN_PAGE = ConfigurationManager.AppSettings["Login_Page"]; 
		private static LogUtil Logger = new LogUtil(typeof(ProcessPage)); 
        
		protected override void Render(System.Web.UI.HtmlTextWriter output) 
		{ 
			if ((Session["UserID"] == null) | (StringUtil.CStr(Session["UserID"]).Trim().Length == 0)) 
			{ 
				output.Write(loginScript()); 
			} 
			else 
			{ 
				base.Render(output); 
			} 
			output.Flush(); 
		} 
        
		private string loginScript() 
		{ 
            
			string _result = ""; 
			_result = "<Script Language=\"JavaScript\">" + "\r\n" ; 
			_result += "top.location.href='/" + __VIRTUAL_DIR + __LOGIN_PAGE + "';" + "\r\n" ; 
			_result += "</Script>" + "\r\n" ; 
			return _result; 
		} 
        
	} 
} 