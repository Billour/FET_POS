using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Advtek.Utility;
using System.Collections.Specialized;

public partial class VSS_CheckOut_CheckOutCredit2 : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            string CREDIT_CARD_NO = "";
            string PAID_AMOUNT = "";

            //**2011/04/26 Tina：前頁傳遞參數時，會先以加密處理，在此要解密。
            if (!string.IsNullOrEmpty(Request.QueryString["Param"]))
            {
                NameValueCollection qscoll = Utils.Param_UrlDecode(Request.QueryString["Param"]);
                foreach (string key in qscoll.AllKeys)
                {
                    if (key == "CREDIT_CARD_NO")
                    {
                        CREDIT_CARD_NO = string.Join(",", qscoll.GetValues(key));
                    }
                    else if (key == "PAID_AMOUNT")
                    {
                        PAID_AMOUNT = string.Join(",", qscoll.GetValues(key));
                    }
                }
            }

            this.lblSTORE_NO.Text = logMsg.STORENO;
            this.lblCREDIT_CARD_NO.Text = CREDIT_CARD_NO;
            this.lblPAID_AMOUNT.Text = PAID_AMOUNT;
        }
    }
 
}
