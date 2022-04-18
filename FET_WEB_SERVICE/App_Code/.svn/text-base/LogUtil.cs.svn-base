using System; 
using System.IO; 
using System.Configuration; 
using System.Data; 
using System.Web.UI; 
using System.Reflection; 
using System.Data.OracleClient; 
//using Microsoft.VisualBasic; 
//將來要把log4net去除時, 要把Import拿掉 
using log4net;

namespace Advtek.Utility 
{ 
	public class LogUtil 
	{ 
		//將來要把log4net去除時,這裡改成宣告為ILog即可 
		//private log4net.ILog Logger = null; 
        private IMyLog Logger = null; 
       
		public LogUtil(Type RefType) 
		{ 
			
			//Logger = LogManager.GetLogger(RefType); 
            Logger = MyLogManager.GetLogger(RefType); 
			//將來要把log4net去除時,這裡改成用DummyLogger, 再把Utility\ILog.vb和DummyLogger.vb加入專案即可 
			//Logger = New DummyLogger 
		}

        public IMyLog Log  //ILog
		{ 
			get { return Logger; } 
		} 
	} 
} 


