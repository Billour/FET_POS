using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class VSS_SAL_SAL01_checkIDNumber : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        lblMessage.Text = "";
    }
    protected void btnCommit_Click(object sender, EventArgs e)
    {

        if (Password1.Value != "" && Password2.Value != "")
        {
            if (Password1.Value == Password2.Value && Password2.Value.Length==8 && Password1.Value.Length==8)
            {
               // Page.ClientScript.RegisterClientScriptBlock(this.GetType(),"close", "<script>window.close();</script>");
                Page.ClientScript.RegisterStartupScript(this.GetType(), "page", "<script type='text/javascript'>window.close();</script>");
            }
            else
            {
                lblMessage.Text = "統一編號檢查碼錯誤。<br/>請確認無誤後，重新輸入。";
            }
            if (Password1.Value != Password2.Value)
            {
                lblMessage.Text = "統一編號輸入不相符，請重新輸入";
            }
        }
    }
    
}
