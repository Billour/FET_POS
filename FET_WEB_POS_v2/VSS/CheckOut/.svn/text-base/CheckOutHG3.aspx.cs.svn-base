using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Advtek.Utility;
using System.Collections.Specialized;

public partial class VSS_CheckOut_CheckOutHG3 : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            string HG_REDEEM_POINT = "";
            string HG_LEFT_POINT = "";
            string HG_CARD_NO = "";

            //**2011/04/26 Tina：前頁傳遞參數時，會先以加密處理，在此要解密。
            if (!string.IsNullOrEmpty(Request.QueryString["Param"]))
            {
                NameValueCollection qscoll = Utils.Param_UrlDecode(Request.QueryString["Param"]);
                foreach (string key in qscoll.AllKeys)
                {
                    if (key == "HG_REDEEM_POINT")
                    {
                        HG_REDEEM_POINT = string.Join(",", qscoll.GetValues(key));
                    }
                    else if (key == "HG_LEFT_POINT")
                    {
                        HG_LEFT_POINT = string.Join(",", qscoll.GetValues(key));
                    }
                    else if (key == "HG_CARD_NO")
                    {
                        HG_CARD_NO = string.Join(",", qscoll.GetValues(key));
                    }

                }
            }


            ClientScript.RegisterHiddenField("hiddenSTORENO", logMsg.STORENO);

            if (HG_REDEEM_POINT != "")
                lblHG_REDEEM_POINT.Text = HG_REDEEM_POINT;
            if (HG_LEFT_POINT != "")
                lblHG_LEFT_POINT.Text = HG_LEFT_POINT;
            if (HG_CARD_NO != "")
                lblHG_CAR_NO.Text = HG_CARD_NO;
        }
    }
 
}
