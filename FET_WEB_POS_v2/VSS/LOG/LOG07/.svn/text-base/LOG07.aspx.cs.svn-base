using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.Web;
using DevExpress.Web.ASPxGridView;
using DevExpress.Web.ASPxPager;
using System.Data;

using Advtek.Utility;
using FET.POS.Model.Common;
using FET.POS.Model.DTO;
using FET.POS.Model.Facade;
using FET.POS.Model.Facade.FacadeImpl;

public partial class VSS_LOG_LOG07 : BasePage
{
    //private string desKey = "AAAAAAAA";
    //private string desIv = "AAAAAAAA";
    private string desKey = StringUtil.CStr(System.Configuration.ConfigurationManager.AppSettings["ConnStrKey"]);
    private string desIv = StringUtil.CStr(System.Configuration.ConfigurationManager.AppSettings["ConnStrIV"]);

    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack)
        {
            bool blCheckLogIn = new LOG07_Facade().CheckLogIn(logMsg.STORENO, logMsg.MODI_USER);
            if (!blCheckLogIn)
            {
                txtOldPassword.Enabled = false;
                txtNewPassword.Enabled = false;
                txtConfirmNewPassword.Enabled = false;
                btnCancel.Enabled = false;
                btnCommit.Enabled = false;
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Alert", "alert('非此門市店長無法使用此功能')", true);
            }
            else
            {
                GetNowPassWord();
            }
        }
    }


    /// <summary>
    /// 更新密碼
    /// </summary>
    private void UpdateData()
    {
        try
        {
            string NewStorePw = DESCryptography.EncryptDES(txtNewPassword.Text, desKey, desIv);
            int intResult = new LOG07_Facade().UpdatePassWord(logMsg.STORENO, NewStorePw);
            if (intResult > 0)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Alert", "alert('存檔成功');", true);
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Alert", "alert('存檔失敗');", true);
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    /// <summary>
    /// 確認資料是否正確
    /// </summary>
    /// <returns></returns>
    private bool CheckInputData()
    {
        bool result = false;
        string checkdate = System.DateTime.Now.ToString("yyyy/MM/dd");
        string StoreNowPw = Session["STORE_PW"] as string;
        if (txtOldPassword.Text == "")
        {
            lblMessage.Text = "舊密碼不得為空白";
        }
        else if (txtOldPassword.Text != StoreNowPw)
        {
            lblMessage.Text = "舊密碼有誤";
        }
        else if (txtNewPassword.Text == "")
        {
            lblMessage.Text = "新密碼不得為空白";
        }
        else if (txtOldPassword.Text == txtNewPassword.Text)
        {
            lblMessage.Text = "新舊碼密不得相同";
        }
        else if (txtConfirmNewPassword.Text == "")
        {
            lblMessage.Text = "確認新密碼不得為空白";
        }
        else if (txtNewPassword.Text != txtConfirmNewPassword.Text)
        {
            lblMessage.Text = "確認密碼錯誤，請重新輸入";
        }
        else
        {
            lblMessage.Text = "";
            result = true;
        }

        return result;
    }
    /// <summary>
    /// 取目前的密碼
    /// </summary>
    protected void GetNowPassWord()
    {
        string DecryptDesStorePw = new LOG07_Facade().QueryOldPassword(logMsg.STORENO);
        Session["STORE_PW"] = DESCryptography.DecryptDES(DecryptDesStorePw, desKey, desIv);
    }

    protected void btnCommit_Click(object sender, EventArgs e)
    {
        if (!CheckInputData())
        {
            return;
        }
        else
        {
            UpdateData();
            GetNowPassWord();
        }
        

    }


}
