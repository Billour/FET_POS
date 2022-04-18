using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections.Specialized;

public partial class VSS_SAL_SAL01_checkIDNumber : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        lblMessage.Text = "";
        if (!IsPostBack)
        {
            //string INVOICE_NO = Request.QueryString["INVOICE_NO"] as string;

            string INVOICE_NO = "";

            //**2011/04/25 Tina：前頁傳遞參數時，會先以加密處理，在此要解密。
            if (!string.IsNullOrEmpty(Request.QueryString["Param"]))
            {
                NameValueCollection qscoll = Utils.Param_UrlDecode(Request.QueryString["Param"]);
                foreach (string key in qscoll.AllKeys)
                {
                    if (key == "INVOICE_NO")
                    {
                        INVOICE_NO = string.Join(",", qscoll.GetValues(key));
                        break;
                    }
                }
            }

            txtUNI_NO1.Text = INVOICE_NO;
            txtUNI_NO2.Focus();
        }
    }
    protected void btnCommit_Click(object sender, EventArgs e)
    {

       //if (Password1.Text != "" && Password2.Text != "")
       // {
       //    if (Password1.Text == Password2.Text && Password2.Text.Length == 8 && Password1.Text.Length == 8)
       //     {
       //        // Page.ClientScript.RegisterClientScriptBlock(this.GetType(),"close", "<script>window.close();</script>");
       //         Page.ClientScript.RegisterStartupScript(this.GetType(), "page", "<script type='text/javascript'>window.close();</script>");
       //     }
       //     else
       //     {
       //         lblMessage.Text = "統一編號檢查碼錯誤。<br/>請確認無誤後，重新輸入。";
       //     }
       //    if (Password1.Text != Password2.Text)
       //     {
       //         lblMessage.Text = "統一編號輸入不相符，請重新輸入";
       //     }
       // }
    }
    
}
