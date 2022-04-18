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

public partial class VSS_SAL_OSAL042 : BasePage
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
        btnExportExcel.Visible = true;
	}

    protected void btnExportExcel_Click(object sender, EventArgs e)
	{
        DataTable dtExport;
        OSAL04_Facade Facade = new OSAL04_Facade();
        //總部人員可查詢各門市資料, 門市人員只能查詢自己的門市
        dtExport = Facade.Export_StoreOrders(
            txtS_Date.Text,
            txtE_Date.Text,
            logMsg.STORENO
            );
        string filename = new Output().Print("XLS", "作廢舊POS交易明細表<td></td><td>匯出時間：" + DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss") + "</td>", null, dtExport, null);
        Response.Redirect(Utils.CreateTamperProofDownloadXlsURL("InvalidOldPOSList.xls"));
	}
	
    #endregion

    protected void gvMaster_PageIndexChanged(object sender, EventArgs e)
	{
		setQueryParams();
		gvMaster.DataSource = new SAL02_Facade().getSALE_HEAD(QryArgs);
		gvMaster.DataBind();
	}
}
