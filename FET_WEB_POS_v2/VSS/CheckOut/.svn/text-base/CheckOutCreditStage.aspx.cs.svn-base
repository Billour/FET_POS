using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FET.POS.Model.Facade.FacadeImpl;
using FET.POS.Model.DTO;
using System.Data;
using DevExpress.Web.ASPxEditors;
using Advtek.Utility;
using System.Collections.Specialized;

public partial class VSS_CheckOut_CheckOutCreditStage : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        ClientScript.RegisterHiddenField("hiddenSTORENO", logMsg.STORENO);
        if (!IsPostBack)
        {
            string ShouldPayAmt = "";

            //**2011/04/26 Tina：前頁傳遞參數時，會先以加密處理，在此要解密。
            if (!string.IsNullOrEmpty(Request.QueryString["Param"]))
            {
                NameValueCollection qscoll = Utils.Param_UrlDecode(Request.QueryString["Param"]);
                foreach (string key in qscoll.AllKeys)
                {
                    if (key == "ShouldPayAmt")
                    {
                        ShouldPayAmt = string.Join(",", qscoll.GetValues(key));
                        break;
                    }
                }
            }

            if (ShouldPayAmt != "")
                txtAMOUNT.Text = ShouldPayAmt;
            DataTable dt = new SAL01_Facade().getBankData();
            cbBank.TextField = "BANK_NAME";
            cbBank.ValueField = "BANK_ID";
            cbBank.DataSource = dt;
            cbBank.DataBind();
            DevExpress.Web.ASPxEditors.ListEditItem lei = new DevExpress.Web.ASPxEditors.ListEditItem("--請選擇--", "");
            cbBank.Items.Insert(0, lei);
            cbBank.SelectedIndex = 0;
            int limitAmt = new SAL01_Facade().getCreditDivLimitAmount();
            limitAmount.Text = StringUtil.CStr(limitAmt);
        }
    }

    protected void cbBank_SelectedIndexChanged(object sender, EventArgs e)
    {
        DataTable dt = new SAL01_Facade().getCreditDivPeriod(StringUtil.CStr(cbBank.SelectedItem.Value));
        if (dt != null && dt.Rows.Count > 0)
        {
            cbIssue.TextField = "pay_seqment";
            cbIssue.ValueField = "pay_seqment";
            cbIssue.DataSource = dt;
            cbIssue.DataBind();
            DevExpress.Web.ASPxEditors.ListEditItem lei = new DevExpress.Web.ASPxEditors.ListEditItem("--請選擇--", "");
            cbIssue.Items.Insert(0, lei);
            cbIssue.SelectedIndex = 0;
        }
        //UpdatePanel1.Update();
    }
}
