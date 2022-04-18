using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Advtek.Utility;
using DevExpress.Web.ASPxGridView;
using System.Collections.Specialized;

using FET.POS.Model.Facade.FacadeImpl;
using FET.POS.Model.DTO;
using FET.POS.Model.Common;

public partial class VSS_SAL_OSAL041 : BasePage
{
	OrderedDictionary QryArgs = new OrderedDictionary();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (logMsg.STORENO.ToUpper() != StringUtil.CStr(System.Web.Configuration.WebConfigurationManager.AppSettings["DefaultStore"]))
            pcSTORENO.Enabled = false;

        if (!Page.IsPostBack && !Page.IsCallback)
        {
            setDefault();
        }
    }

	void setDefault()
	{
		pcSTORENO.Text = logMsg.STORENO;
	}

    void setQueryParams()
    {
        //取得查詢條件值
        QryArgs["MACHINE_ID"] = txtMACHINE_ID.Text;
        QryArgs["SALE_NO"] = txtSALE_NO.Text;//交易序號
        QryArgs["STORENO"] = pcSTORENO.Text;
    }

    #region Button 觸發事件

	protected void btnCancelDetail_Click(object sender, EventArgs e)
	{
        //判斷是否有資料
        OSAL04_Facade facade = new OSAL04_Facade();
        string store_no = pcSTORENO.Text;
        string MACHINE_ID = txtMACHINE_ID.Text;
        string SALE_NO = txtSALE_NO.Text;
        string trade_no = txtTRADE_DATE.Text;
        string POSUUID_MASTER = facade.CheckData(MACHINE_ID, SALE_NO, trade_no, store_no);
        if (!string.IsNullOrEmpty(POSUUID_MASTER))
        {
            string encryptUrl = Utils.Param_Encrypt(string.Format("UUID={0}", POSUUID_MASTER));
            Response.Redirect("~/VSS/SAL/OSAL04/OSAL04.aspx?Param=" +encryptUrl);
        }
        else
        {
            Response.Write("<script>alert('查無資料!!');</script>");
        }
	}
	
    #endregion
}
