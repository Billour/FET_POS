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

public partial class VSS_RPT_RPL062 : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack && !Page.IsCallback)
        {
            //BindMasterData();
        }
    }
    protected void btnExport_Click(object sender, EventArgs e)
    {
        BindMasterData();
        new Output().ExportXLS(600, "", Resources.WebResources.RPL062, dtHeader(), this.ASPxGridViewExporter1, dtFooter(), null);
        Response.Redirect(Utils.CreateTamperProofDownloadXlsURL("RPL062.xls")); 

    }
    private void BindMasterData()
    {
        this.gvMaster.DataSource = new RPL_Facade().RPL062(txtTransferSlipNo.Text, txtOUT_STORE_NO_S.Text,
            txtOUT_STORE_NO_E.Text, txtOutDate.Text, txtIN_STORE_NO_S.Text,
            txtIN_STORE_NO_E.Text);
        this.gvMaster.DataBind();
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        BindMasterData();
        gvMaster.PageIndex = 0;
    }

    private DataTable dtHeader()
    {
        DataTable dtHeader = new DataTable();
        dtHeader.Columns.Add("Text", typeof(string));
        dtHeader.Columns.Add("Align", typeof(string));
        dtHeader.Columns.Add("FontSize", typeof(string));
        dtHeader.Columns.Add("BackColor", typeof(string));
        string sOUT_STORE_NO_E = string.Empty;

        if (!string.IsNullOrEmpty(txtOUT_STORE_NO_E.Text))
        {
            sOUT_STORE_NO_E = "~" + txtOUT_STORE_NO_E.Text;
        }

        DataRow NewRow = dtHeader.NewRow();
        NewRow["Text"] = "移撥單號：" + txtTransferSlipNo.Text
            + "|調出門市：" + txtOUT_STORE_NO_S.Text + sOUT_STORE_NO_E
            + "|調出日期：" + txtOutDate.Text
            + "|撥入門市：" + txtIN_STORE_NO_S.Text;
        NewRow["Align"] = "LEFT";
        NewRow["FontSize"] = "11";
        NewRow["BackColor"] = "WHITE";
        dtHeader.Rows.Add(NewRow);

        NewRow = dtHeader.NewRow();
        NewRow["Text"] = "列印日期：" + DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss")
            + "|列印人員：" + logMsg.MODI_USER + " " + new Employee_Facade().GetEmpName(logMsg.MODI_USER)
            + "|頁　　次：1";
        NewRow["Align"] = "LEFT";
        NewRow["FontSize"] = "11";
        NewRow["BackColor"] = "WHITE";
        dtHeader.Rows.Add(NewRow);
        return dtHeader;

    }

    private DataTable dtFooter()
    {
        DataTable dtFooter = new DataTable();
        dtFooter.Columns.Add("Text", typeof(string));
        dtFooter.Columns.Add("Align", typeof(string));
        dtFooter.Columns.Add("FontSize", typeof(string));
        dtFooter.Columns.Add("BackColor", typeof(string));

        DataRow NewRow = dtFooter.NewRow();
        NewRow["Text"] = "移出人員：__________";
        NewRow["Align"] = "LEFT";
        NewRow["FontSize"] = "11";
        NewRow["BackColor"] = "WHITE";
        dtFooter.Rows.Add(NewRow);

        //空的
        NewRow = dtFooter.NewRow();
        NewRow["Text"] = "   ";
        NewRow["Align"] = "LEFT";
        NewRow["FontSize"] = "11";
        NewRow["BackColor"] = "WHITE";
        dtFooter.Rows.Add(NewRow);

        NewRow = dtFooter.NewRow();
        NewRow["Text"] = "撥入人員：__________";
        NewRow["Align"] = "LEFT";
        NewRow["FontSize"] = "11";
        NewRow["BackColor"] = "WHITE";
        dtFooter.Rows.Add(NewRow);
        return dtFooter;
    }
    protected void gvMaster_PageIndexChanged(object sender, EventArgs e)
    {
        BindMasterData();
    }
    protected void btnReset_Click(object sender, EventArgs e)
    {
        txtTransferSlipNo.Text = "";
        txtOUT_STORE_NO_S.Text = "";
        txtOUT_STORE_NO_E.Text = "";
        txtOutDate.Text = "";
        txtIN_STORE_NO_S.Text = "";
        txtIN_STORE_NO_E.Text = "";
        this.gvMaster.DataSource = new RPL_Facade().RPL062("dfsdfsdfsfsdfsd", "",
            "", "", "",
            "");
        this.gvMaster.DataBind();
    }
}
