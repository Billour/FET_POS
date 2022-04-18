using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class VSS_CheckOut_CheckOutDebitCard : BasePage
{
   protected void Page_Load(object sender, EventArgs e)
   {
      if (!IsPostBack)
      {
          txtAMOUNT.Focus();
      //   if (Request.QueryString["Type"] == "2")//刷退時
      //   {
      //      MultiView1.ActiveViewIndex = 3;
      //   }
      //   else
      //   {
      //      MultiView1.ActiveViewIndex = 0;
      //   }
      }
   }
   
}
