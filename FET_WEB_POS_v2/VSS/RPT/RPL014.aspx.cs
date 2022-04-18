using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

using Advtek.Utility;
using FET.POS.Model.Common;
using FET.POS.Model.DTO;
using FET.POS.Model.Facade;
using FET.POS.Model.Facade.FacadeImpl;
using DevExpress.Data;

public partial class VSS_RPT_RPL014 : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack && !Page.IsCallback)
        {
            btnReset_Click(null, null);
            txtOrdDateStart.Text = DateTime.Now.ToString("yyyy/MM/dd");
            txtOrdDateEnd.Text = DateTime.Now.ToString("yyyy/MM/dd");
        }
    }

    private void BindMasterData()
    {
        this.gvMaster.DataSource = new RPL_Facade().RPL014(this.txtStoreNo_S.Text, this.txtStoreNo_E.Text,
            this.txtOrdDateStart.Text, this.txtOrdDateEnd.Text, this.ASPxComboBox3.SelectedItem.Text,
            this.INVOICE_S.Text, this.INVOICE_E.Text, this.AMOUNT_S.Text, this.AMOUNT_E.Text,
            this.popEMPLOYEE_S.Text, this.popEMPLOYEE_E.Text);
        this.gvMaster.DataBind();
        if (StringUtil.CStr(logMsg.STORENO) != System.Web.Configuration.WebConfigurationManager.AppSettings["DefaultRoleHQ"])
        {
            txtStoreNo_S.Text = StringUtil.CStr(logMsg.STORENO); txtStoreNo_S.Enabled = false;
            txtStoreNo_E.Text = StringUtil.CStr(logMsg.STORENO); txtStoreNo_E.Enabled = false;
        }
    }

    private void BindDropDownList()
    {

    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        BindMasterData();
        gvMaster.PageIndex = 0;
    }

    protected void btnExport_Click(object sender, EventArgs e)
    {
        BindMasterData();
        new Output().ExportXLS(800, "", Resources.WebResources.RPL014, dtHeader(), this.ASPxGridViewExporter1, null, null);
        Response.Redirect(Utils.CreateTamperProofDownloadXlsURL("RPL014.xls"));
    }

    private DataTable dtHeader()
    {
        DataTable dtHeader = new DataTable();
        dtHeader.Columns.Add("Text", typeof(string));
        dtHeader.Columns.Add("Align", typeof(string));
        dtHeader.Columns.Add("FontSize", typeof(string));
        dtHeader.Columns.Add("BackColor", typeof(string));

        DataRow NewRow = dtHeader.NewRow();
        NewRow["Text"] = "門市名稱：" + this.txtStoreNo_S.Text + "～" + this.txtStoreNo_E.Text
            + "|發票日期：" + this.txtOrdDateStart.Text + "～" + this.txtOrdDateEnd.Text
            + "|發票號碼：" + this.INVOICE_S.Text + "～" + this.INVOICE_E.Text
            + "|發票金額：" + this.AMOUNT_S.Text + "～" + this.AMOUNT_E.Text
            + "|員工編號：" + this.popEMPLOYEE_S.Text + "～" + this.popEMPLOYEE_E.Text
            + "|課稅別：" + this.ASPxComboBox3.SelectedItem.Text;
        NewRow["Align"] = "LEFT";
        NewRow["FontSize"] = "11";
        NewRow["BackColor"] = "WHITE";
        dtHeader.Rows.Add(NewRow);

        NewRow = dtHeader.NewRow();
        NewRow["Text"] = "列印日期：" + DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss")
            + "|列印人員：" + logMsg.MODI_USER + " " + new Employee_Facade().GetEmpName(logMsg.MODI_USER)
            + "|頁　　次：" + "1"
            + "|總筆數：" + this.gvMaster.VisibleRowCount;
        NewRow["Align"] = "LEFT";
        NewRow["FontSize"] = "11";
        NewRow["BackColor"] = "WHITE";
        dtHeader.Rows.Add(NewRow);

        return dtHeader;

    }

    protected void gvMaster_PageIndexChanged(object sender, EventArgs e)
    {
        BindMasterData();
    }

    protected void StoreNo_Changed(object sender, EventArgs e)
    {
        BindDropDownList();
    }

    protected void btnReset_Click(object sender, EventArgs e)
    {

        //if (logMsg.ROLE_TYPE != System.Web.Configuration.WebConfigurationManager.AppSettings["DefaultRoleHQ"])
        //{
        //    ScriptManager.RegisterClientScriptBlock(this, GetType(), "AlertMessage", "alert('非總部人員無法使用此功能!!');", true);

        //    txtStoreNo_S.Enabled = false;
        //    txtStoreNo_E.Enabled = false;
        //    txtOrdDateStart.Enabled = false;
        //    txtOrdDateEnd.Enabled = false;
        //    ASPxComboBox3.Enabled = false;

        //    btnSearch.Enabled = false;
        //    btnReset.Enabled = false;
        //    btnExport.Enabled = false;
        //    return;
        //}

        //查詢條件清空，下拉選單回復預設值        

        txtStoreNo_S.Text = null;
        txtStoreNo_E.Text = null;
        txtOrdDateStart.Text = null;
        txtOrdDateEnd.Text = null;
        INVOICE_S.Text = null;
        INVOICE_E.Text = null;
        AMOUNT_S.Text = null;
        AMOUNT_E.Text = null;
        popEMPLOYEE_S.Text = null;
        popEMPLOYEE_E.Text = null;

        ASPxComboBox3.SelectedIndex = 0;

        gvMaster.DataSource = null;
        gvMaster.DataBind();

        BindDropDownList();

        if (StringUtil.CStr(logMsg.STORENO) != System.Web.Configuration.WebConfigurationManager.AppSettings["DefaultRoleHQ"])
        {
            txtStoreNo_S.Text = StringUtil.CStr(logMsg.STORENO); txtStoreNo_S.Enabled = false;
            txtStoreNo_E.Text = StringUtil.CStr(logMsg.STORENO); txtStoreNo_E.Enabled = false;
        }

    }
}