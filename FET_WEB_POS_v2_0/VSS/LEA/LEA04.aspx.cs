using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class VSS_LEA04_LEA04 : System.Web.UI.Page
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
                strStockLocal = this.Request.QueryString["StockLocal"].ToString();

            }
            else
            {
                strStockLocal = "";
            }
            if (this.Request.QueryString["MobileType"] != null)
            {
                strMobileType = this.Request.QueryString["MobileType"].ToString();

            }
            else
            {
                strMobileType = "";
            }
            if (this.Request.QueryString["MobileNo"] != null)
            {
                strMobileNo = this.Request.QueryString["MobileNo"].ToString();
                lblMobileNo.Text = strMobileNo;
            }
            else
            {
                strMobileNo = "";
            }

            if (this.Request.QueryString["ResDate"] != null)
            {
                lblResDate.Text = this.Request.QueryString["ResDate"].ToString();
            }
            if (this.Request.QueryString["LEANo"] != null)
            {
                lblLEANo.Text = this.Request.QueryString["LEANo"].ToString();
            }
            if (this.Request.QueryString["CusName"] != null)
            {
                tbCusName.Text = this.Request.QueryString["CusName"].ToString();
            }
            if (this.Request.QueryString["CusPhNumber"] != null)
            {
                tbCustPhNumber.Text = this.Request.QueryString["CusPhNumber"].ToString();
            }
            if (this.Request.QueryString["Sex"] != null)
            {
                ddlSex.SelectedIndex = ddlSex.Items.IndexOf(ddlSex.Items.FindByValue(this.Request.QueryString["Sex"].ToString()));
                lblMobileNo.Text = strMobileNo;
            }
            if (this.Request.QueryString["ResTakeDate"] != null)
            {
                tbxResTakeDate.Text = this.Request.QueryString["ResTakeDate"].ToString();
            }
            if (this.Request.QueryString["ResReturnDate"] != null)
            {
                tbxResReturnDate.Text = this.Request.QueryString["ResReturnDate"].ToString();
            }
        }
    }


    protected void btnSave_Click(object sender, EventArgs e)
    {
        this.lblLEANo.Text = "TT-0001111-332";
    }
}
