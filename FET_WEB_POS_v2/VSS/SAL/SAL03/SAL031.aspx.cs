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

public partial class VSS_SAL_SAL031 : BasePage
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
		getSALE_PERSON();
		txtS_Date.Date = Convert.ToDateTime(StringUtil.CStr(DateTime.Now.Year) + "/" + StringUtil.CStr(DateTime.Now.Month) + "/01");
		txtE_Date.Date = DateTime.Now;
		pcSTORENO.Text = logMsg.STORENO;
	}

	void getSALE_PERSON()
	{
        DataTable dtSalePerson = new SAL02_Facade().getSalePersonInSaleHead(logMsg.STORENO);
        cbSALE_PERSON.DataSource = dtSalePerson;
		cbSALE_PERSON.ValueField = "EMPNO";
		cbSALE_PERSON.TextField = "EMP_SHOWNAME";
		cbSALE_PERSON.DataBind();
		cbSALE_PERSON.Items.Insert(0, new DevExpress.Web.ASPxEditors.ListEditItem("ALL", ""));
        DataRow[] drs = null;
        if (dtSalePerson != null && dtSalePerson.Rows.Count > 0)
            drs = dtSalePerson.Select(" EMPNO = '" + logMsg.OPERATOR + "'");
        if (drs == null || drs.Length == 0)
        {
            Employee_Facade empFacade = new Employee_Facade();
            string empName = empFacade.GetEmpName(logMsg.MODI_USER);
            cbSALE_PERSON.Items.Insert(1, new DevExpress.Web.ASPxEditors.ListEditItem(empName + "-" + logMsg.OPERATOR, logMsg.OPERATOR));
        }
		cbSALE_PERSON.SelectedIndex = cbSALE_PERSON.Items.IndexOfValue(logMsg.OPERATOR);
	}

    void setQueryParams()
    {
        //取得查詢條件值
        QryArgs["S_DATE"] = txtS_Date.Text;
        QryArgs["E_DATE"] = txtE_Date.Text;
        QryArgs["SALE_STATUS"] = cbSTATUS.SelectedItem.Value;
        QryArgs["MSISDN"] = txtMSISDN.Text;//客戶門號
        QryArgs["MACHINE_ID"] = txtMACHINE_ID.Text;
        QryArgs["SALE_NO"] = txtSALE_NO.Text;//交易序號
        QryArgs["INVOICE_NO"] = txtInv_No.Text;
        QryArgs["PRODNO"] = pcPRODNO.Text;
        QryArgs["STORENO"] = pcSTORENO.Text;
        QryArgs["PROMOTION_CODE"] = txtPROMOTION_CODE.Text;
        QryArgs["PAY_METHOD"] = "";
        QryArgs["SALE_PERSON"] = "";
        if (cbPAY_METHOD.SelectedItem != null)
            QryArgs["PAY_METHOD"] = cbPAY_METHOD.SelectedItem.Value;
        if (cbSALE_PERSON.SelectedIndex != 0)
            QryArgs["SALE_PERSON"] = cbSALE_PERSON.SelectedItem.Value;
    }

	protected void bindMasterData()
	{
		setQueryParams();
		DataTable dtResult = new SAL02_Facade().getSALE_HEAD(QryArgs);
        //20110311 Mark by Tyan, 查詢過SA,SD文件,詢問過Victor,Jesson,沒人要求要做此檢查;多做此檢查導致客戶回應訊息不明確issue
        //if (dtResult.Rows.Count == 0)
        //{
        //    //判斷條件影響
        //    string result = new SAL02_Facade().checkData(QryArgs);
        //    if (!string.IsNullOrEmpty(result.Trim()))
        //    {
        //        ScriptManager.RegisterClientScriptBlock(this, typeof(string), "selectCheckData", string.Format(@"alert('{0}');", result), true);
        //    }
        //}

        gvMaster.DataSource = dtResult;
        gvMaster.DataBind();
        gvMaster.PageIndex = 0;
    }

    #region Button 觸發事件

    protected void btnSearch_Click(object sender, EventArgs e)
	{
        gvMaster.FocusedRowIndex = -1;
		DateTime dt1, dt2;
		if (!DateTime.TryParse(txtS_Date.Text, out dt1))
		{
			ScriptManager.RegisterClientScriptBlock(this, typeof(string), "selectSALE_NO", string.Format("alert('交易日期格式錯誤({1})!');{0}.focus();", txtS_Date.ClientID, Resources.WebResources.Start), true);
			return;
		}
		if (!DateTime.TryParse(txtE_Date.Text, out dt2))
		{
			ScriptManager.RegisterClientScriptBlock(this, typeof(string), "selectSALE_NO", string.Format("alert('交易日期格式錯誤({1})!');{0}.focus();", txtE_Date.ClientID, Resources.WebResources.End), true);
			return;
		}
		if (dt1.CompareTo(dt2) > 0)
		{
			ScriptManager.RegisterClientScriptBlock(this, typeof(string), "selectSALE_NO", string.Format("alert('「交易日期起值」不允許大於「交易日期訖值」!');{0}.focus();", txtS_Date.ClientID), true);
			return;
		}

		bindMasterData();
		btnChangeProd.Visible = true;
	}

	protected void btnChangeProd_Click(object sender, EventArgs e)
	{
		if (gvMaster.FocusedRowIndex > -1)
		{
			string s = gvMaster.GetRowValues(gvMaster.FocusedRowIndex, "POSUUID_MASTER") as string;
            //Response.Redirect("~/VSS/SAL/SAL03/SAL03.aspx?SRC_TYPE=SAL031&PKEY=" + s);

            //**2011/04/21 Tina：傳遞參數時，要先以加密處理。
            string encryptUrl = Utils.Param_Encrypt("SRC_TYPE=SAL031&PKEY=" + s);
            Response.Redirect("~/VSS/SAL/SAL03/SAL03.aspx?Param=" + encryptUrl, true);

		}
		else
		{
			ScriptManager.RegisterClientScriptBlock(this, typeof(string), "selectSALE_NO", "alert('請選取銷售資料!');", true);
		}
	}
    #endregion

    protected void gvMaster_PageIndexChanged(object sender, EventArgs e)
	{
		setQueryParams();
		gvMaster.DataSource = new SAL02_Facade().getSALE_HEAD(QryArgs);
		gvMaster.DataBind();
	}

    //ajax 呼當前網頁的方式
    [System.Web.Services.WebMethod()]
    [System.Web.Script.Services.ScriptMethod()]
    static public int getProdInfo(string PRODNO)
    {
        int ProdNoCount = 0;
        if (!string.IsNullOrEmpty(PRODNO))
        {
            //** 2011/03/08 Tina：註解，因為銷售交易查詢不需判斷選取的商品是否為折扣商品(IS_DISCOUNT = 'Y') => Issue 663
            //ProdNoCount = new Product_Facade().Query_DiscountProduct(PRODNO).Rows.Count;
            ProdNoCount = new Product_Facade().Query_ProductInfo(PRODNO).Rows.Count;
        }

        return ProdNoCount;
    }

  
}
