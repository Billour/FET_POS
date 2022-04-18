using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Advtek.Utility;

public partial class VSS_LEA_LEA04 : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            string strStockLocal = "";
            string strMobileType = "";
            string strMobileNo = "";
            if (this.Request.QueryString["StockLocal"] != null)
            {
                strStockLocal = StringUtil.CStr(this.Request.QueryString["StockLocal"]);

            }
            else
            {
                strStockLocal = "";
            }
            if (this.Request.QueryString["MobileType"] != null)
            {
                strMobileType = StringUtil.CStr(this.Request.QueryString["MobileType"]);

            }
            else
            {
                strMobileType = "";
            }
            if (this.Request.QueryString["MobileNo"] != null)
            {
                strMobileNo = StringUtil.CStr(this.Request.QueryString["MobileNo"]);
                lblMobileNo.Text = strMobileNo;
            }
            else
            {
                strMobileNo = "";
            }

            if (this.Request.QueryString["ResDate"] != null)
            {
                lblResDate.Text = StringUtil.CStr(this.Request.QueryString["ResDate"]);
            }
            if (this.Request.QueryString["LEANo"] != null)
            {
                lblLEANo.Text = StringUtil.CStr(this.Request.QueryString["LEANo"]);
            }
            if (this.Request.QueryString["CusName"] != null)
            {
                tbCusName.Text = StringUtil.CStr(this.Request.QueryString["CusName"]);
            }
            if (this.Request.QueryString["CusPhNumber"] != null)
            {
                tbCustPhNumber.Text = StringUtil.CStr(this.Request.QueryString["CusPhNumber"]);
            }
            if (this.Request.QueryString["Sex"] != null)
            {
                ddlSex.SelectedIndex = ddlSex.Items.IndexOf(ddlSex.Items.FindByValue(StringUtil.CStr(this.Request.QueryString["Sex"])));
                lblMobileNo.Text = strMobileNo;
            }
            if (this.Request.QueryString["ResTakeDate"] != null)
            {
                tbxResTakeDate.Text = StringUtil.CStr(this.Request.QueryString["ResTakeDate"]);
            }
            if (this.Request.QueryString["ResReturnDate"] != null)
            {
                tbxResReturnDate.Text = StringUtil.CStr(this.Request.QueryString["ResReturnDate"]);
            }
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        this.lblLEANo.Text = "TT-0001111-332";
    }
}
