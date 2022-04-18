using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class EncryptPage : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Cache.SetCacheability(HttpCacheability.NoCache);

        if (!string.IsNullOrEmpty(Request["URLParam"]))
        {
            string encryptUrl = Utils.Param_Encrypt(Request["URLParam"]);
            Response.Write(encryptUrl);
        }
    }
}
