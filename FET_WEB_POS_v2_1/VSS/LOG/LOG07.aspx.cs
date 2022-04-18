using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class VSS_LOG_LOG07 : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void btnCommit_Click(object sender, EventArgs e)
    {
        if (Password1.Value != "" && Password2.Value != "")
        {
            if (Password1.Value == Password2.Value && Password2.Value.Length == 8 && Password1.Value.Length == 8)
            {
                // Page.ClientScript.RegisterClientScriptBlock(this.GetType(),"close", "<script>window.close();</script>");
                Page.ClientScript.RegisterStartupScript(this.GetType(), "page", "<script type='text/javascript'>window.close();</script>");
            }
            else
            {
                lblMessage.Text = "新密碼檢核錯誤。<br/>請確認無誤後，重新輸入。";
            }
        }
        else
        {
            lblMessage.Text = "新密碼檢核錯誤。<br/>請確認無誤後，重新輸入。";
        }
    }
}
