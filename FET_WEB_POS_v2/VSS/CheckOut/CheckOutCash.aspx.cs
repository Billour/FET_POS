using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Advtek.Utility;
using System.Collections.Specialized;

public partial class VSS_CheckOut_CheckOutCash : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack && !IsCallback)
        {
            string ShouldPayAmt = "";

            //**2011/04/26 Tina：前頁傳遞參數時，會先以加密處理，在此要解密。
            if (!string.IsNullOrEmpty(Request.QueryString["Param"]))
            {
                NameValueCollection qscoll = Utils.Param_UrlDecode(Request.QueryString["Param"]);
                foreach (string key in qscoll.AllKeys)
                {
                    if (key == "ShouldPayAmt")
                    {
                        ShouldPayAmt = string.Join(",", qscoll.GetValues(key));
                        break;
                    }
                }
            }

            if (ShouldPayAmt != "")
            {
                txtCash.Text = ShouldPayAmt;
            }
        }
    }
}
