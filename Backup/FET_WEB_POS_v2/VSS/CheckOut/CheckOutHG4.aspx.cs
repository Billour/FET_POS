using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Advtek.Utility;
using System.Collections.Specialized;

public partial class VSS_CheckOut_CheckOutHG4 : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            string HG_CARD_NO = "";
            string HG_LEFT_POINT = "";
            string TOTAL_AMOUNT = "";
            string HG_REDEEM_POINT = "";

            //**2011/04/27 Tina：前頁傳遞參數時，會先以加密處理，在此要解密。
            if (!string.IsNullOrEmpty(Request.QueryString["Param"]))
            {
                NameValueCollection qscoll = Utils.Param_UrlDecode(Request.QueryString["Param"]);
                foreach (string key in qscoll.AllKeys)
                {
                    if (key == "HG_CARD_NO")
                    {
                        HG_CARD_NO = string.Join(",", qscoll.GetValues(key));
                    }
                    else if (key == "HG_LEFT_POINT")
                    {
                        HG_LEFT_POINT = string.Join(",", qscoll.GetValues(key));
                    }
                    else if (key == "TOTAL_AMOUNT")
                    {
                        TOTAL_AMOUNT = string.Join(",", qscoll.GetValues(key));
                    }
                    else if (key == "HG_REDEEM_POINT")
                    {
                        HG_REDEEM_POINT = string.Join(",", qscoll.GetValues(key));
                    }
                }
            }

            this.hidStoreNo.Value = logMsg.STORENO;
            this.lblFinHG_CAR_NO.Text = HG_CARD_NO;
            this.lblFinHG_LEFT_POINT.Text = HG_LEFT_POINT;
            this.lblFinHG_REDEEM_POINT.Text = TOTAL_AMOUNT + "元(" + HG_REDEEM_POINT + "點)";
            this.hidRedeemPoint.Value = HG_REDEEM_POINT;
        }
    }
 
}
