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

public partial class VSS_SAL_SAL11_Transaction : Popup
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {

        }
    }


    protected void btnCommit_Click(object sender, EventArgs e)
    {
       // if (Radio1.Checked == true)
       // {
       //     Response.Redirect("SAL05.aspx");
       // }
       // else
       //  {    
       //     Response.Redirect("SAL11.aspx");
       //}
    }

}
