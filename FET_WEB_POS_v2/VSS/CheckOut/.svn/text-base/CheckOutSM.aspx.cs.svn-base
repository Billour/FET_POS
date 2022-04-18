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
using System.Collections.Specialized;

public partial class VSS_CheckOut_CheckOutSM : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            string TOTAL_AMOUNT = "";
            string TRAN_DATE = "";

            //**2011/04/26 Tina：前頁傳遞參數時，會先以加密處理，在此要解密。
            if (!string.IsNullOrEmpty(Request.QueryString["Param"]))
            {
                NameValueCollection qscoll = Utils.Param_UrlDecode(Request.QueryString["Param"]);
                foreach (string key in qscoll.AllKeys)
                {
                    if (key == "TOTAL_AMOUNT")
                    {
                        TOTAL_AMOUNT = string.Join(",", qscoll.GetValues(key));
                    }
                    else if (key == "TRAN_DATE")
                    {
                        TRAN_DATE = string.Join(",", qscoll.GetValues(key));
                    }
                }
            }

            hidTOTAL_AMOUNT.Value = TOTAL_AMOUNT;
            hidTRAN_DATE.Value = TRAN_DATE;

            //ClientScript.RegisterHiddenField("TOTAL_AMOUNT", TOTAL_AMOUNT);
            //ClientScript.RegisterHiddenField("TRAN_DATE", TRAN_DATE);

            BindDisReason();
            txtPassword.ClientEnabled = false;
            btnEnter.ClientEnabled = false;
            btnEnter.ClientVisible = false;
            getStoreDiscountInfo(logMsg.ROLE_TYPE);
            hidRoleType.Value = logMsg.ROLE_TYPE;
        }

    }

    private void BindDisReason()
    {
        this.ddlSH_DISCOUNT_REASON.DataSource = new SAL01_Facade().getStoreDISReason();
        this.ddlSH_DISCOUNT_REASON.TextField = "STORE_DIS_REASON_DESC";
        this.ddlSH_DISCOUNT_REASON.ValueField = "STORE_DIS_REASON_ID";
        this.ddlSH_DISCOUNT_REASON.DataBind();
        this.ddlSH_DISCOUNT_REASON.SelectedIndex = 0;

        if (this.ddlSH_DISCOUNT_REASON.Text == "其它")
        {
            txSH_DISCOUNT_DESC.ClientEnabled = true;
        }
        else
        {
            txSH_DISCOUNT_DESC.ClientEnabled = false;
        }
    }

    protected void btnEnter_Click(object sender, EventArgs e)
    {
        //【密碼輸入】欄位輸入值檢核正確，系統開放【折抵金額】、【折抵比率】及【折抵原因】供使用者輸入。
        if (!string.IsNullOrEmpty(this.txtPassword.Text.Trim()))
        {
            string Password = this.txtPassword.Text;
            string strKey = StringUtil.CStr(System.Configuration.ConfigurationManager.AppSettings["ConnStrKey"]);
            string strIV = StringUtil.CStr(System.Configuration.ConfigurationManager.AppSettings["ConnStrIV"]);
            string strPassword = DESCryptography.EncryptDES(Password, strKey, strIV);
            string strInfo = new SAL01_Facade().CheckStoreDiscountPassword(logMsg.STORENO, strPassword);
            if (strInfo == "1")
            {
                #region remove
                ////1. 判斷當月店長可折抵的金額是否已達上限(當月店家可折抵的金額 vs. 當月店長累計已折抵的金額)
                ////2. 已達上限，告知User，且無法繼續使用此畫面
                ////3. 未達上限，判斷當月門市可折抵的金額是否已達上限(當月門市可折抵的金額 vs. 當月門市累計已折抵的金額)
                ////4. 已達上限，告知User，且無法繼續使用此畫面
                ////5. 未達上限，取得當月店長剩餘可折抵金額
                //string strYYMM = OracleDBUtil.WorkDay(logMsg.STORENO); //營業日
                //string info = new SAL01_Facade().getSpecDisInfo(logMsg.STORENO, strYYMM.Substring(0, 7), logMsg.ROLE_TYPE);
                //string[] amtArray = info.Split("^".ToCharArray());//已折抵的總金額,當月該門市可折抵金額,該角色當月已折抵的金額,該角色可折抵的金額上限,該角色單次可折抵的金額上限,該角色單次可折抵的折扣比率上限
                //int remainDisAmt = 0;
                ////如果當月門市已折抵金額小於可折抵金額且角色已折抵金額小於可折抵金額
                //if (int.Parse(amtArray[0]) < int.Parse(amtArray[1]) && int.Parse(amtArray[2]) < int.Parse(amtArray[3])) 
                //{
                //    remainDisAmt = int.Parse(amtArray[1]) - int.Parse(amtArray[0]);
                //    //如果當月門市可折抵餘額大於當月角色可折抵餘額，折抵餘額設為當月角色可折抵餘額
                //    if (remainDisAmt > int.Parse(amtArray[3]) - int.Parse(amtArray[2]))
                //        remainDisAmt = int.Parse(amtArray[3]) - int.Parse(amtArray[2]);
                //}
                //this.lblRemainingDiscountAmount.Text = StringUtil.CStr(remainDisAmt);
                //if (this.lblRemainingDiscountAmount.Text == "0")
                //{
                //    this.btnOK.ClientEnabled = false;
                //    ScriptManager.RegisterClientScriptBlock(this, typeof(string), "CheckDisAmount", "alert('無剩餘可折抵金額!');", true);
                //}
                //else
                //{
                //    this.btnOK.ClientEnabled = true;                            
                //    this.ddlSH_DISCOUNT_REASON.ClientEnabled = true;      //折抵原因
                //    this.txtTOTAL_AMOUNT.ClientEnabled = true;            //折抵金額
                //    this.txtSH_DISCOUNT_RATE.ClientEnabled = true;        //折抵比率
                //    this.txtPassword.Text = Password;
                //    this.txtPassword.Enabled = false;
                //    this.hidStoreUsedAmt.Value = amtArray[0];       //當月門市已折抵金額
                //    this.hidStoreDisAmtBound.Value = amtArray[1];   //當月門市可折抵金額
                //    this.hidRoleUsedAmt.Value = amtArray[2];        //當月角色已折抵金額
                //    this.hidRoleDisAmtBound.Value = amtArray[3];    //當月角色可折抵金額
                //    this.hidRoleDisAmt.Value = amtArray[4];         //單次角色最高可折抵金額
                //    this.hidRoleDisRateBound.Value = amtArray[5];   //單次角色最高可折抵比率
                //    this.lblRoleDisAmt.Text = amtArray[4];
                //    this.lblRoleDisRate.Text = amtArray[5];
                //}
                #endregion remove
                getStoreDiscountInfo("1"); //使用店長權限
                this.hidRoleType.Value = "1";
                lblRemainingDiscountAmount.Text = "*****";
                lblRoleDisAmt.Text = "*****";
                lblRoleDisRate.Text = "***";
                this.btnOK.ClientEnabled = true;
            }
            else
            {
                //驗證不通過
                this.ddlSH_DISCOUNT_REASON.ClientEnabled = true;      //折抵原因
                this.txtTOTAL_AMOUNT.ClientEnabled = true;            //折抵金額
                this.txtSH_DISCOUNT_RATE.ClientEnabled = true;        //折抵比率
                ScriptManager.RegisterClientScriptBlock(this, typeof(string), "CheckPassword", "alert('密碼驗證不通過!');", true);
            }
        }
    }

    private void getStoreDiscountInfo(string roleType)
    {
        //1. 判斷當月店長可折抵的金額是否已達上限(當月店家可折抵的金額 vs. 當月店長累計已折抵的金額)
        //2. 已達上限，告知User，且無法繼續使用此畫面
        //3. 未達上限，判斷當月門市可折抵的金額是否已達上限(當月門市可折抵的金額 vs. 當月門市累計已折抵的金額)
        //4. 已達上限，告知User，且無法繼續使用此畫面
        //5. 未達上限，取得當月店長剩餘可折抵金額
        string strYYMM = OracleDBUtil.WorkDay(logMsg.STORENO); //營業日
        string info = new SAL01_Facade().getSpecDisInfo(logMsg.STORENO, strYYMM.Substring(0, 7), roleType);
        string[] amtArray = info.Split("^".ToCharArray());//已折抵的總金額,當月該門市可折抵金額,該角色當月已折抵的金額,該角色可折抵的金額上限,該角色單次可折抵的金額上限,該角色單次可折抵的折扣比率上限
        int remainDisAmt = 0;
        //如果當月門市已折抵金額小於可折抵金額且角色已折抵金額小於可折抵金額
        if (int.Parse(amtArray[0]) < int.Parse(amtArray[1]) && int.Parse(amtArray[2]) < int.Parse(amtArray[3]))
        {
            remainDisAmt = int.Parse(amtArray[1]) - int.Parse(amtArray[0]);
            //如果當月門市可折抵餘額大於當月角色可折抵餘額，折抵餘額設為當月角色可折抵餘額
            if (remainDisAmt > int.Parse(amtArray[3]) - int.Parse(amtArray[2]))
                remainDisAmt = int.Parse(amtArray[3]) - int.Parse(amtArray[2]);
        }
        this.lblRemainingDiscountAmount.Text = StringUtil.CStr(remainDisAmt);
        this.btnOK.ClientEnabled = true;
        this.ddlSH_DISCOUNT_REASON.ClientEnabled = true;      //折抵原因
        this.txtTOTAL_AMOUNT.ClientEnabled = true;            //折抵金額
        this.txtSH_DISCOUNT_RATE.ClientEnabled = true;        //折抵比率
        this.hidStoreUsedAmt.Value = amtArray[0];       //當月門市已折抵金額
        this.hidStoreDisAmtBound.Value = amtArray[1];   //當月門市可折抵金額
        this.hidRoleUsedAmt.Value = amtArray[2];        //當月角色已折抵金額
        this.hidRoleDisAmtBound.Value = amtArray[3];    //當月角色可折抵金額
        this.hidRoleDisAmt.Value = amtArray[4];         //單次角色最高可折抵金額
        this.hidRoleDisRateBound.Value = amtArray[5];   //單次角色最高可折抵比率
        this.lblRoleDisAmt.Text = amtArray[4];
        this.lblRoleDisRate.Text = amtArray[5];
        this.txtPassword.ClientEnabled = false;
        this.btnEnter.ClientVisible = false;
        this.btnEnter.ClientEnabled = false;
    }

    //ajax 呼當前網頁的方式
    [System.Web.Services.WebMethod()]
    [System.Web.Script.Services.ScriptMethod()]
    static public string getDISCOUNTInfo(string TRAN_DATE)
    {
        DataTable dt = new SAL01_Facade().getCheckOutSM_DISCOUNTInfo(TRAN_DATE.Replace("%2f","/"));
        string r = "";
        if (dt.Rows.Count > 0)
        {
            r = StringUtil.CStr(dt.Rows[0]["para_value"]) + ",";
            r += StringUtil.CStr(dt.Rows[0]["para_name"]);
        }
        return r;
    }

}
