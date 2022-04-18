using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Advtek.Utility;
using FET.POS.Model.Common;
using FET.POS.Model.DTO;
using FET.POS.Model.Facade;
using FET.POS.Model.Facade.FacadeImpl;
using DevExpress.Data;
using System.Data;

public partial class VSS_RPT_RPL047 : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            btnReset_Click(null, null);
        }
    }

    private void BindMasterData()
    {
        string Search_list = "";
        if (this.RadioButton1.Checked)
        {
            Search_list = "ALL";
        }
        else if (this.RadioButton2.Checked)
        {
            Search_list = "0";
        }
        else if (this.RadioButton3.Checked)
        {
            Search_list = "1";
        }

        this.gvMaster.DataSource = new RPL_Facade().RPL047(this.txtSTORE_NO_S.Text, this.txtSTORE_NO_E.Text, //門市編號
                                                           this.CancelDate_S.Text, this.CancelDate_E.Text, //作廢日期
                                                           this.InvoiceNo_S.Text, this.InvoiceNo_E.Text, //發票號碼
                                                           this.InvoiceAmount_S.Text, this.InvoiceAmount_E.Text, //發票金額
                                                           this.EmployeeNo_S.Text, this.EmployeeNo_E.Text, //員工編號
                                                           Search_list); //當月/跨月
        this.gvMaster.DataBind();

        if (StringUtil.CStr(logMsg.STORENO) != System.Web.Configuration.WebConfigurationManager.AppSettings["DefaultRoleHQ"])
        {
            txtSTORE_NO_S.Text = StringUtil.CStr(logMsg.STORENO); txtSTORE_NO_S.Enabled = false;
            txtSTORE_NO_E.Text = StringUtil.CStr(logMsg.STORENO); txtSTORE_NO_E.Enabled = false;
        }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        BindMasterData();
        gvMaster.PageIndex = 0;

        if (StringUtil.CStr(logMsg.STORENO) != System.Web.Configuration.WebConfigurationManager.AppSettings["DefaultRoleHQ"])
        {
            txtSTORE_NO_S.Text = StringUtil.CStr(logMsg.STORENO); txtSTORE_NO_S.Enabled = false;
            txtSTORE_NO_E.Text = StringUtil.CStr(logMsg.STORENO); txtSTORE_NO_E.Enabled = false;
        }

    }

    protected void btnReset_Click(object sender, EventArgs e)
    {

        //if (logMsg.ROLE_TYPE != System.Web.Configuration.WebConfigurationManager.AppSettings["DefaultRoleHQ"])
        //{
        //    ScriptManager.RegisterClientScriptBlock(this, GetType(), "AlertMessage", "alert('非總部人員無法使用此功能!!');", true);

        //    txtSTORE_NO_S.Enabled = false;
        //    txtSTORE_NO_E.Enabled = false;
        //    CancelDate_S.Enabled = false;
        //    CancelDate_E.Enabled = false;
        //    InvoiceNo_S.Enabled = false;
        //    InvoiceNo_E.Enabled = false;
        //    InvoiceAmount_S.Enabled = false;
        //    InvoiceAmount_E.Enabled = false;
        //    EmployeeNo_S.Enabled = false;
        //    EmployeeNo_E.Enabled = false;
        //    RadioButton1.Enabled = false;
        //    RadioButton2.Enabled = false;
        //    RadioButton3.Enabled = false;

        //    btnSearch.Enabled = false;
        //    btnReset.Enabled = false;
        //    btnExport.Enabled = false;
        //    return;
        //}

        //查詢條件清空，回復預設值
        this.txtSTORE_NO_S.Text = null;
        this.txtSTORE_NO_E.Text = null;
        this.CancelDate_S.Text = null;
        this.CancelDate_E.Text = null;
        this.InvoiceNo_S.Text = null;
        this.InvoiceNo_E.Text = null;
        this.InvoiceAmount_S.Text = null;
        this.InvoiceAmount_E.Text = null;
        this.EmployeeNo_S.Text = null;
        this.EmployeeNo_E.Text = null;
        gvMaster.DataSource = null;
        gvMaster.DataBind();

        if (StringUtil.CStr(logMsg.STORENO) != System.Web.Configuration.WebConfigurationManager.AppSettings["DefaultRoleHQ"])
        {
            txtSTORE_NO_S.Text = StringUtil.CStr(logMsg.STORENO); txtSTORE_NO_S.Enabled = false;
            txtSTORE_NO_E.Text = StringUtil.CStr(logMsg.STORENO); txtSTORE_NO_E.Enabled = false;
        }

        CancelDate_S.Text = DateTime.Now.ToString("yyyy/MM/dd");
        CancelDate_E.Text = DateTime.Now.ToString("yyyy/MM/dd");
        RadioButton2.Checked = true;
    }

    protected void btnExport_Click(object sender, EventArgs e)
    {
        //匯出至EXCEL
        BindMasterData();
        new Output().ExportXLS(1253, "", Resources.WebResources.RPL047, dtHeader(), this.ASPxGridViewExporter1, null, null);
        Response.Redirect(Utils.CreateTamperProofDownloadXlsURL("RPL047.xls"));
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
            + "門市編號：" + this.txtSTORE_NO_S.Text + "～" + this.txtSTORE_NO_E.Text
            + "|作廢日期：" + this.CancelDate_S.Text + "～" + this.CancelDate_E.Text
            + "|發票號碼：" + this.InvoiceNo_S.Text + "～" + this.InvoiceNo_E.Text
            + "|發票金額：" + this.InvoiceAmount_S.Text + "～" + this.InvoiceAmount_E.Text
            + "|員工編號：" + this.EmployeeNo_S.Text + "～" + this.EmployeeNo_E.Text
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

        if (StringUtil.CStr(logMsg.STORENO) != System.Web.Configuration.WebConfigurationManager.AppSettings["DefaultRoleHQ"])
        {
            txtSTORE_NO_S.Text = StringUtil.CStr(logMsg.STORENO); txtSTORE_NO_S.Enabled = false;
            txtSTORE_NO_E.Text = StringUtil.CStr(logMsg.STORENO); txtSTORE_NO_E.Enabled = false;
        }
    
    }

}
