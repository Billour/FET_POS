using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections.Specialized;

public partial class VSS_SAL_TSAL01_checkIDNumber : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        lblMessage.Text = "";
        if (!IsPostBack)
        {
            string strINVOICE_NO = "";

            //**2011/04/27 Tina：前頁傳遞參數時，會先以加密處理，在此要解密。
            if (!string.IsNullOrEmpty(Request.QueryString["Param"]))
            {
                NameValueCollection qscoll = Utils.Param_UrlDecode(Request.QueryString["Param"]);
                foreach (string key in qscoll.AllKeys)
                {
                    if (key == "INVOICE_NO")
                    {
                        strINVOICE_NO = string.Join(",", qscoll.GetValues(key));
                        break;
                    }
                }
            }

            txtUNI_NO1.Text = strINVOICE_NO;
            txtUNI_NO2.Focus();
        }
    }    
}
