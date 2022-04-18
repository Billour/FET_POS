using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.Web.ASPxEditors;
using System.Data;
using Advtek.Utility;
using FET.POS.Model.Facade.FacadeImpl;

public partial class VSS_CheckOut_ETCInput : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack && !IsCallback)
        {
            //Get ETC Product No.
            this.hidLowLimitAmt.Value = StringUtil.CStr(new SAL01_Facade().getFETCLowLimitAmt());
            this.lblLowLimitAmt.Text = this.hidLowLimitAmt.Value;
        }

    }
}
