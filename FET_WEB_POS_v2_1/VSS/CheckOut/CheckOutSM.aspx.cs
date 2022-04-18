using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class VSS_CheckOut_CheckOutSM : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
    {
       if (DropDownList1.SelectedItem.Text == "其它")
       {
          this.TextBox4.Enabled = true;
       }
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
       this.DropDownList1.Enabled = true;
       this.TextBox2.Enabled = true;
       this.TextBox3.Enabled = true;
    }
}
