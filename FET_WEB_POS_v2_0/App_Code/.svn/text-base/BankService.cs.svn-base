using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Collections.Specialized;
using System.Xml;
using AjaxControlToolkit;


/// <summary>
/// BankService 的摘要描述
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// 若要允許使用 ASP.NET AJAX 從指令碼呼叫此 Web 服務，請取消註解下一行。
[System.Web.Script.Services.ScriptService]
public class BankService : System.Web.Services.WebService
{
    private static XmlDocument _document; 
    private static object _lock = new object();

    public BankService()
    {

        //如果使用設計的元件，請取消註解下行程式碼 
        //InitializeComponent(); 
    }

    public static XmlDocument Document 
    { 
        get 
        { 
            lock (_lock) { 
                if (_document == null) { 
                    _document = new XmlDocument(); 
                    _document.Load(HttpContext.Current.Server.MapPath("~/App_Data/Banks.xml")); 
                } 
            } 
            return _document; 
        } 
    } 
    
 

    [WebMethod]
    public string[] GetBankNameList(string prefixText, int count)
    {
        List<string> suggestions = new List<string>();

        foreach (XmlNode node in Document.SelectNodes("banks/*"))
        {
            string bankName = node.Attributes["name"].Value;
            if (bankName.StartsWith(prefixText, StringComparison.InvariantCultureIgnoreCase))
            {
                suggestions.Add(node.Attributes["name"].Value);
            }
        }


        return suggestions.ToArray();
    }
}

