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

public partial class VSS_RPT_RPL073  : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack && !Page.IsCallback)
        {
            btnReset_Click(null, null);
        }
    }

    private void BindMasterData()
    {
        this.gvMaster.DataSource = new RPL_Facade().RPL073(this.ASPxTextBox1.Text, this.ASPxTextBox4.Text,
            this.txtTranDateStart.Text, this.txtTranDateEnd.Text, StringUtil.CStr(this.cbDiscountClass.SelectedItem.Value),
           StringUtil.CStr(this.cbChangeClass.SelectedItem.Value), StringUtil.CStr(this.cbServiceType.SelectedItem.Value));

        this.gvMaster.DataBind();

        if (StringUtil.CStr(logMsg.STORENO) != System.Web.Configuration.WebConfigurationManager.AppSettings["DefaultRoleHQ"])
        {
            this.ASPxTextBox1.Enabled = false; this.ASPxTextBox1.Text = StringUtil.CStr(logMsg.STORENO);
            this.ASPxTextBox4.Enabled = false; this.ASPxTextBox4.Text = StringUtil.CStr(logMsg.STORENO);
        }
    
    }

    private void BindDropDownList()
    {
        //this.ddlReason.DataSource = RPL_PageHelper.GetReason();
        //ddlReason.TextField = "REASON";
        //ddlReason.ValueField = "REASONID";
        //this.ddlReason.DataBind();
        //this.ddlReason.SelectedIndex = 0;

        //this.ddlStock.DataSource = RPL_PageHelper.GetStock();
        //ddlStock.TextField = "STOCK_NAME";
        //ddlStock.ValueField = "LOC_ID";
        //this.ddlStock.DataBind();
        //this.ddlReason.SelectedIndex = 0;

    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        BindMasterData();
        gvMaster.PageIndex = 0;
    }

    protected void btnExport_Click(object sender, EventArgs e)
    {
        BindMasterData();
        new Output().ExportXLS(800, "", Resources.WebResources.RPL073, dtHeader(), this.ASPxGridViewExporter1, null, null);
        Response.Redirect(Utils.CreateTamperProofDownloadXlsURL("RPL073.xls"));
    }

    private DataTable dtHeader()
    {
        DataTable dtHeader = new DataTable();
        dtHeader.Columns.Add("Text", typeof(string));
        dtHeader.Columns.Add("Align", typeof(string));
        dtHeader.Columns.Add("FontSize", typeof(string));
        dtHeader.Columns.Add("BackColor", typeof(string));

        DataRow NewRow = dtHeader.NewRow();
        NewRow["Text"] = "門市編號：" + this.ASPxTextBox1.Text + "~" + this.ASPxTextBox4.Text
            + "|交易日期：" + this.txtTranDateStart.Text + "~" + this.txtTranDateEnd.Text
            + "|折扣類別：" + this.cbDiscountClass.SelectedItem.Text
            + "|服務類型：" + this.cbServiceType.SelectedItem.Text
            + "|兌點類別：" + this.cbChangeClass.SelectedItem.Text ;
        NewRow["Align"] = "LEFT";
        NewRow["FontSize"] = "11";
        NewRow["BackColor"] = "WHITE";
        dtHeader.Rows.Add(NewRow);
        DataRow NewRow2 = dtHeader.NewRow();
        NewRow2["Text"] =
            ""
            + "列印日期：" + DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss")
            + "|列印人員：" + logMsg.MODI_USER + " " + new Employee_Facade().GetEmpName(logMsg.MODI_USER)
            + "|頁　　次：" + "1"
            + "|總筆數：" + this.gvMaster.VisibleRowCount;
        NewRow2["Align"] = "LEFT";
        NewRow2["FontSize"] = "11";
        NewRow2["BackColor"] = "WHITE";
        dtHeader.Rows.Add(NewRow2);
        return dtHeader;

    }

    protected void gvMaster_PageIndexChanged(object sender, EventArgs e)
    {
        BindMasterData();
    }

    protected void on_SelectedChanged(object sender, EventArgs e)
    {

    }

    protected void btnReset_Click(object sender, EventArgs e)
    {

        //if (logMsg.ROLE_TYPE != System.Web.Configuration.WebConfigurationManager.AppSettings["DefaultRoleHQ"])
        //{
        //    ScriptManager.RegisterClientScriptBlock(this, GetType(), "AlertMessage", "alert('非總部人員無法使用此功能!!');", true);

        //    txtTranDateStart.Enabled = false;
        //    txtTranDateEnd.Enabled = false;
        //    ASPxTextBox1.Enabled = false;
        //    ASPxTextBox4.Enabled = false;
        //    cbDiscountClass.Enabled = false;
        //    cbServiceType.Enabled = false;
        //    cbChangeClass.Enabled = false;

        //    btnSearch.Enabled = false;
        //    btnReset.Enabled = false;
        //    btnExport.Enabled = false;
        //    return;
        //}

        //查詢條件清空，下拉選單回復預設值
        this.txtTranDateStart.Text = null;
        this.txtTranDateEnd.Text = null;
        this.ASPxTextBox1.Text = "";
        this.ASPxTextBox4.Text = "";
        this.cbDiscountClass.SelectedIndex = 0;
        this.cbServiceType.SelectedIndex = 0;
        this.cbChangeClass.SelectedIndex = 0;
        gvMaster.DataSource = null;
        gvMaster.DataBind();

        if (StringUtil.CStr(logMsg.STORENO) != System.Web.Configuration.WebConfigurationManager.AppSettings["DefaultRoleHQ"])
        {
            this.ASPxTextBox1.Enabled = false; this.ASPxTextBox1.Text = StringUtil.CStr(logMsg.STORENO);
            this.ASPxTextBox4.Enabled = false; this.ASPxTextBox4.Text = StringUtil.CStr(logMsg.STORENO);
        }
    
    }
}
