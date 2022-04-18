using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Advtek.Utility;
using System.Collections.Specialized;

public partial class VSS_CheckOut_ETCCardLoading : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            string PaidAmt = "";
            //**2011/04/27 Tina：前頁傳遞參數時，會先以加密處理，在此要解密。
            if (!string.IsNullOrEmpty(Request.QueryString["Param"]))
            {
                NameValueCollection qscoll = Utils.Param_UrlDecode(Request.QueryString["Param"]);
                foreach (string key in qscoll.AllKeys)
                {
                    if (key == "PaidAmt")
                    {
                        PaidAmt = string.Join(",", qscoll.GetValues(key));
                    }
                }
            }

            this.lblSTORE_NO.Text = logMsg.STORENO;
            this.lblPAID_AMOUNT.Text = PaidAmt;
        }
    }
 
}
