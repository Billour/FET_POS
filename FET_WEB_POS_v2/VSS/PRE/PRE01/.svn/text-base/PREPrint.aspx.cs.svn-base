using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Advtek.Utility;
using System.Collections.Specialized;

public partial class VSS_PRE_PREPrint : BasePage
{
    string _Receipt
    {
        get
        {
            //**2011/04/27 Tina：前頁傳遞參數時，會先以加密處理，在此要解密。
            string strReceipt = "";
            if (!string.IsNullOrEmpty(Request.QueryString["Param"]))
            {
                NameValueCollection qscoll = Utils.Param_UrlDecode(Request.QueryString["Param"]);
                foreach (string key in qscoll.AllKeys)
                {
                    if (key == "Receipt")
                    {
                        strReceipt = string.Join(",", qscoll.GetValues(key));
                        break;
                    }
                }
            }
            return strReceipt;

            //return Request.QueryString["Receipt"] == null ? "" : StringUtil.CStr(Request.QueryString["Receipt"]).Trim();
        }
    }
    string _Invoice
    {
        get
        {
            //**2011/04/27 Tina：前頁傳遞參數時，會先以加密處理，在此要解密。
            string strInvoice = "";
            if (!string.IsNullOrEmpty(Request.QueryString["Param"]))
            {
                NameValueCollection qscoll = Utils.Param_UrlDecode(Request.QueryString["Param"]);
                foreach (string key in qscoll.AllKeys)
                {
                    if (key == "Invoice")
                    {
                        strInvoice = string.Join(",", qscoll.GetValues(key));
                        break;
                    }
                }
            }
            return strInvoice;

            //return Request.QueryString["Invoice"] == null ? "" : StringUtil.CStr(Request.QueryString["Invoice"]).Trim();
        }
    }

    string _Discount
    {
        get
        {
            //**2011/04/27 Tina：前頁傳遞參數時，會先以加密處理，在此要解密。
            string strDiscount = "";
            if (!string.IsNullOrEmpty(Request.QueryString["Param"]))
            {
                NameValueCollection qscoll = Utils.Param_UrlDecode(Request.QueryString["Param"]);
                foreach (string key in qscoll.AllKeys)
                {
                    if (key == "Discount")
                    {
                        strDiscount = string.Join(",", qscoll.GetValues(key));
                        break;
                    }
                }
            }
            return strDiscount;

            //return Request.QueryString["Discount"] == null ? "" : StringUtil.CStr(Request.QueryString["Discount"]).Trim();
        }
    }


    

    protected void Page_Load(object sender, EventArgs e)
    {

        //string A = StringUtil.CStr(Request.QueryString["PREPAY_NO"]).Trim();

        if (_Invoice == null || _Invoice == "")
        {
            Invoice.ClientVisible = false;
        }

        if (_Receipt == null || _Receipt == "")
        {
            Receipt.ClientVisible = false;
        }
        if (_Discount == null || _Discount == "")
        {
            Discount.ClientVisible = false;
        }

    }

    public void ProcessRequest(string filename)
    {
        ScriptManager.RegisterClientScriptBlock(this,
                                               this.GetType(),
                                               "test",
                                               "document.getElementById('" + fDownload.ClientID + "').src='" + filename + "';",
                                               true);
    }

    protected void BtnPri_Click(object sender, EventArgs e)
    {
        //列印預收單
        //((DevExpress.Web.ASPxEditors.ASPxButton)(sender)).ID
        switch (((DevExpress.Web.ASPxEditors.ASPxButton)(sender)).ID)
        {
            case "Receipt":
                ProcessRequest(_Receipt);
                break;
            case "Invoice":
                ProcessRequest(_Invoice);
                break;
            case "Discount":
                ProcessRequest(_Discount);
                break;
            default: 
                break ;

        }
    }
}
