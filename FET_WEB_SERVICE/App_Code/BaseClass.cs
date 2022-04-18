using System; 
using System.IO; 
using System.Configuration; 
using System.Data; 
using System.Web.UI; 
using System.Data.OracleClient; 
//'Imports Microsoft.VisualBasic 

namespace Advtek.Utility 
{ 
    
	public class BaseClass 
	{
        public static readonly string CONNECT_STRING = ConfigurationManager.AppSettings["Connection_String"]; 
		public static LogUtil Logger = new LogUtil(typeof(BaseClass)); 
        
	} 
    
} 

