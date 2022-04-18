using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using DevExpress.Web;
using DevExpress.Web.ASPxGridView;
using DevExpress.Web.ASPxPager;
using System.Collections.Specialized;

public partial class VSS_SAL_SAL11_Transaction : Popup
{
    /// <summary>
    /// 折扣料號主檔UUID
    /// </summary>
    private string s
    {
        get
        {
            string s = "";

            //**2011/04/21 Tina：前頁傳遞參數時，會先以加密處理，在此要解密。
            if (!string.IsNullOrEmpty(Request.QueryString["Param"]))
            {
                NameValueCollection qscoll = Utils.Param_UrlDecode(Request.QueryString["Param"]);
                foreach (string key in qscoll.AllKeys)
                {
                    if (key == "s")
                    {
                        s = string.Join(",", qscoll.GetValues(key));
                        break;
                    }
                }
            }

            return s;
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        //**2011/04/21 Tina：傳遞參數時，要先以加密處理。
        string encryptUrl = "";
        string url = "";

        if (s == "2")
        {
            encryptUrl = Utils.Param_Encrypt("s=2");
            url = string.Format("../TSAL05/TSAL05.aspx?Param={0}", encryptUrl);
        }
        else
        {
            encryptUrl = Utils.Param_Encrypt("s=1");
            //url = string.Format("../SAL05/SAL05.aspx?Param={0}", encryptUrl);
            url = string.Format("../TSAL05/TSAL05.aspx?Param={0}", encryptUrl);
        }

        this.btnCommit.ClientSideEvents.Click = @"function(s, e) {
                                                   if (radio1.GetChecked()) {
                                                       parent.location = '" + url + @"';
                                                    }
                                                    hidePopupWindow();
                                                  }
                                                ";
    }
}
