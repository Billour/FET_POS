﻿using System;
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


public partial class VSS_RPT_RPL011 : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            btnReset_Click(null, null);
        }
    }

    private void BindDropDownList()
    {
        this.ddlPAID_MODE.DataSource = RPL_PageHelper.GetPiadMode();
        ddlPAID_MODE.TextField = "PAID_NAME";
        ddlPAID_MODE.ValueField = "PAID_NAME";
        this.ddlPAID_MODE.DataBind();
        this.ddlPAID_MODE.SelectedIndex = 0;
    }

    private void BindMasterData()
    {
        this.gvMaster.DataSource = new RPL_Facade().RPL011(this.txtOrdDateStart.Text, this.txtOrdDateEnd.Text,
            this.ASPxTextBox1.Text, this.ASPxTextBox4.Text,
            StringUtil.CStr(this.ASPxComboBox3.SelectedItem.Value ),
            StringUtil.CStr(this.ddlPAID_MODE.SelectedItem.Value ));
        this.gvMaster.DataBind();
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        BindMasterData();
        gvMaster.PageIndex = 0;
    }

    protected void btnReset_Click(object sender, EventArgs e)
    {
        if (logMsg.ROLE_TYPE != System.Web.Configuration.WebConfigurationManager.AppSettings["DefaultRoleHQ"])
        {
            ScriptManager.RegisterClientScriptBlock(this, GetType(), "AlertMessage", "alert('非總部人員無法使用此功能!!');", true);

            txtOrdDateStart.Enabled = false;
            txtOrdDateEnd.Enabled = false;
            ASPxComboBox3.Enabled = false;
            ddlPAID_MODE.Enabled = false;
            ASPxTextBox1.Enabled = false;
            ASPxTextBox4.Enabled = false;

            btnSearch.Enabled = false;
            btnReset.Enabled = false;
            btnExport.Enabled = false;
            return;
        }

        BindDropDownList();

        //查詢條件清空，下拉選單回復預設值
        this.txtOrdDateStart.Text = null;
        this.txtOrdDateEnd.Text = null;
        this.ASPxTextBox1.Text = "";
        this.ASPxTextBox4.Text = "";
        this.ASPxComboBox3.SelectedIndex = 0;
        this.ddlPAID_MODE.SelectedIndex = 0;
        gvMaster.DataSource = null;
        gvMaster.DataBind();
    }

    protected void btnExport_Click(object sender, EventArgs e)
    {
        //匯出至EXCEL
        BindMasterData();
        new Output().ExportXLS(580, "", Resources.WebResources.RPL011, dtHeader(), this.ASPxGridViewExporter1, null, null);
        Response.Redirect(Utils.CreateTamperProofDownloadXlsURL("RPL011.xls"));
    }

    private DataTable dtHeader()
    {
        DataTable dtHeader = new DataTable();
        dtHeader.Columns.Add("Text", typeof(string));
        dtHeader.Columns.Add("Align", typeof(string));
        dtHeader.Columns.Add("FontSize", typeof(string));
        dtHeader.Columns.Add("BackColor", typeof(string));

        DataRow NewRow = dtHeader.NewRow();
        NewRow["Text"] = ""
            + "交易日期：" + this.txtOrdDateStart.Text + "～" + this.txtOrdDateEnd.Text
            + "|門市編號：" + this.ASPxTextBox1.Text + "～" + this.ASPxTextBox4.Text
            + "|代收類別：" + this.ASPxComboBox3.SelectedItem.Text
            + "|付款方式：" + this.ddlPAID_MODE.SelectedItem.Text
            + "";
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
}
