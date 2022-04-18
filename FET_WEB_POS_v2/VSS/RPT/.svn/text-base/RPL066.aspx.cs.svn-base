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

public partial class VSS_RPT_RPL066 : BasePage
{
    private int totalCount;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack && !Page.IsCallback)
        {
            btnReset_Click(null, null);
            
            ////Default
            //if (StringUtil.CStr(logMsg.STORENO) != System.Web.Configuration.WebConfigurationManager.AppSettings["DefaultRoleHQ"])
            //{
            //    Labelx.Text = logMsg.STORENO;Labelx.Enabled = false;
            //}

            //BindDropDownList();
            //gvMaster.DataSource = null;
            //gvMaster.DataBind();
        }
    }

    private void BindMasterData()
    {
        DataTable DT = new RPL_Facade().RPL066(this.Labelx.Text, this.txtHRS_NO.Text, this.Textbox3.Text,
                                               this.cbMaintenance.SelectedItem.Text, this.txtOrdDateStart.Text, this.txtOrdDateEnd.Text,
                                               this.ASPxDateEdit1.Text, this.ASPxDateEdit2.Text, StringUtil.CStr(this.cbSALE_PERSON.SelectedItem.Value));

        totalCount = DT.Rows.Count;
        gvMaster.Templates.PagerBar = new CustomPagerBarTemplate(totalCount);


        this.gvMaster.DataSource = new RPL_Facade().RPL066_SUM(this.Labelx.Text, this.txtHRS_NO.Text, this.Textbox3.Text,
                                                           this.cbMaintenance.SelectedItem.Text, this.txtOrdDateStart.Text, this.txtOrdDateEnd.Text,
                                                           this.ASPxDateEdit1.Text, this.ASPxDateEdit2.Text, StringUtil.CStr( this.cbSALE_PERSON.SelectedItem.Value));
        this.gvMaster.DataBind();
        if (StringUtil.CStr(logMsg.STORENO) != System.Web.Configuration.WebConfigurationManager.AppSettings["DefaultRoleHQ"])
        {
            Labelx.Text = logMsg.STORENO; Labelx.Enabled = false;
        }

    }

    private void BindDropDownList()
    {
        //維修廠商
        this.cbMaintenance.DataSource = new RPL_PageHelper().getStoreMaintenance();
        cbMaintenance.TextField = "FIX_BRAND";
        cbMaintenance.ValueField = "FIX_BRAND";
        this.cbMaintenance.DataBind();
        this.cbMaintenance.SelectedIndex = 0;
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        BindMasterData();
        gvMaster.PageIndex = 0;
    }

    protected void btnExport_Click(object sender, EventArgs e)
    {
        BindMasterData();
        new Output().ExportXLS(800, "", Resources.WebResources.RPL066, dtHeader(), this.ASPxGridViewExporter1, null, null);
        Response.Redirect(Utils.CreateTamperProofDownloadXlsURL("RPL066.xls"));
    }

    private DataTable dtHeader()
    {
        DataTable dtHeader = new DataTable();
        dtHeader.Columns.Add("Text", typeof(string));
        dtHeader.Columns.Add("Align", typeof(string));
        dtHeader.Columns.Add("FontSize", typeof(string));
        dtHeader.Columns.Add("BackColor", typeof(string));

        DataRow NewRow = dtHeader.NewRow();
        NewRow["Text"] = "門市編號：" + this.Labelx.Text
            + "|維修單號：" + this.txtHRS_NO.Text
            + "|IMEI：" + this.Textbox3.Text
            + "|維護廠商：" + this.cbMaintenance.SelectedItem.Text
            + "|維修日期：" + this.txtOrdDateStart.Text + "～" + this.txtOrdDateEnd.Text
            + "|發票日期：" + this.ASPxDateEdit1.Text + "～" + this.ASPxDateEdit2.Text
            + "|處理人員：" + this.cbSALE_PERSON.SelectedItem.Text;
        NewRow["Align"] = "LEFT";
        NewRow["FontSize"] = "11";
        NewRow["BackColor"] = "WHITE";
        dtHeader.Rows.Add(NewRow);

        DataRow NewRow2 = dtHeader.NewRow();
        NewRow2["Text"] = "列印日期：" + DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss")
            + "|列印人員：" + logMsg.MODI_USER + " " + new Employee_Facade().GetEmpName(logMsg.MODI_USER)
            + "|頁　　次：" + "1"
            + "|總筆數：" + StringUtil.CStr(totalCount);
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

    protected void btnReset_Click(object sender, EventArgs e)
    {
        //if (logMsg.ROLE_TYPE == StringUtil.CStr(System.Configuration.ConfigurationManager.AppSettings["DefaultRoleHQ"]))
        //{
        //    //彈跳視窗
        //    ScriptManager.RegisterClientScriptBlock(this, GetType(), "AlertMessage", "alert('非門市人員無法使用此功能!!');", true);
        //    this.cbSALE_PERSON.Enabled = false;
        //    this.txtHRS_NO.Enabled = false;
        //    this.Textbox3.Enabled = false;
        //    this.cbMaintenance.Enabled = false;
        //    this.txtOrdDateStart.Enabled = false;
        //    this.txtOrdDateEnd.Enabled = false;
        //    this.ASPxDateEdit1.Enabled = false;
        //    this.ASPxDateEdit2.Enabled = false;

        //    gvMaster.Enabled = false;

        //    btnSearch.Enabled = false;
        //    btnReset.Enabled = false;
        //    btnExport.Enabled = false;
        //    return;
        //}

        //查詢條件清空，下拉選單回復預設值

        this.cbSALE_PERSON.SelectedIndex = 0;
        this.txtHRS_NO.Text = null;
        this.Textbox3.Text = null;
        this.cbMaintenance.SelectedIndex = 0;
        this.txtOrdDateStart.Text = null;
        this.txtOrdDateEnd.Text = null;
        this.ASPxDateEdit1.Text = null;
        this.ASPxDateEdit2.Text = null;

        getSALE_PERSON();
        BindDropDownList();

        gvMaster.DataSource = null;
        gvMaster.DataBind();

        if (StringUtil.CStr(logMsg.STORENO) != System.Web.Configuration.WebConfigurationManager.AppSettings["DefaultRoleHQ"])
        {
            Labelx.Text = logMsg.STORENO; Labelx.Enabled = false;
        }
    }

    //處理人員
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
}
