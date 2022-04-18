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

public partial class VSS_RPT_RPL067 : BasePage
{
    private int totalCount;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack && !Page.IsCallback)
        {
            btnReset_Click(null, null);
        }
    }

    private void BindMasterData()
    {
        DataTable DT = new RPL_Facade().RPL067(
                                                this.ASPxTextBox1.Text, this.ASPxTextBox2.Text,
                                                this.ASPxDateEdit1.Text, this.ASPxDateEdit2.Text,
                                                this.ASPxDateEdit3.Text, this.ASPxDateEdit4.Text,
                                                StringUtil.CStr(this.ASPxComboBox1.SelectedItem.Value), this.cbSALE_PERSON.SelectedItem.Text,
                                                this.popEmployee.Text
                                                );
        totalCount = DT.Rows.Count;
        gvMaster.Templates.PagerBar = new CustomPagerBarTemplate(totalCount);

        this.gvMaster.DataSource = new RPL_Facade().RPL067_SUM(
                                                            this.ASPxTextBox1.Text, this.ASPxTextBox2.Text,
                                                            this.ASPxDateEdit1.Text, this.ASPxDateEdit2.Text,
                                                            this.ASPxDateEdit3.Text, this.ASPxDateEdit4.Text,
                                                            StringUtil.CStr(this.ASPxComboBox1.SelectedItem.Value), this.cbSALE_PERSON.SelectedItem.Text,
                                                            this.popEmployee.Text
                                                            );
        this.gvMaster.DataBind();

        if (StringUtil.CStr(logMsg.STORENO) != System.Web.Configuration.WebConfigurationManager.AppSettings["DefaultRoleHQ"])
        {
            this.ASPxTextBox1.Enabled = false; this.ASPxTextBox1.Text = StringUtil.CStr(logMsg.STORENO);
            this.ASPxTextBox2.Enabled = false; this.ASPxTextBox2.Text = StringUtil.CStr(logMsg.STORENO);
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
        new Output().ExportXLS(800, "", "設備賠償明細表", dtHeader(), this.ASPxGridViewExporter1, null, null);
        Response.Redirect(Utils.CreateTamperProofDownloadXlsURL("RPL067.xls"));
    }

    private DataTable dtHeader()
    {
        DataTable dtHeader = new DataTable();
        dtHeader.Columns.Add("Text", typeof(string));
        dtHeader.Columns.Add("Align", typeof(string));
        dtHeader.Columns.Add("FontSize", typeof(string));
        dtHeader.Columns.Add("BackColor", typeof(string));

        DataRow NewRow = dtHeader.NewRow();
        NewRow["Text"] = "門市編號：" + this.ASPxTextBox1.Text + "～" + this.ASPxTextBox2.Text
            + "|設備租借日期：" + this.ASPxDateEdit1.Text + "～" + this.ASPxDateEdit2.Text
            + "|發票日期：" + this.ASPxDateEdit3.Text + "～" + this.ASPxDateEdit4.Text
            + "|租借類別：" + this.ASPxComboBox1.SelectedItem.Text
            + "|處理人員：" + this.cbSALE_PERSON.SelectedItem.Text
            + "|員工編號：" + this.popEmployee.Text
            ;
        NewRow["Align"] = "LEFT";
        NewRow["FontSize"] = "11";
        NewRow["BackColor"] = "WHITE";
        dtHeader.Rows.Add(NewRow);

        NewRow = dtHeader.NewRow();
        NewRow["Text"] = "列印日期：" + DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss")
            + "|列印人員：" + logMsg.MODI_USER + " " + new Employee_Facade().GetEmpName(logMsg.MODI_USER)
            + "|頁　　次：" + "1"
            + "|總筆數：" + StringUtil.CStr(totalCount);
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
        //查詢條件清空，下拉選單回復預設值        

        ASPxTextBox1.Text = null;
        ASPxTextBox2.Text = null;
        ASPxDateEdit1.Text = null;
        ASPxDateEdit2.Text = null;
        ASPxDateEdit3.Text = null;
        ASPxDateEdit4.Text = null;
        ASPxDateEdit3.Text = DateTime.Now.ToString("yyyy/MM/") + "01";
        ASPxDateEdit4.Text = DateTime.Now.ToString("yyyy/MM/dd");

        ASPxComboBox1.SelectedIndex = 0;
        popEmployee.Text = null;

        gvMaster.DataSource = null;
        gvMaster.DataBind();

        if (StringUtil.CStr(logMsg.STORENO) != System.Web.Configuration.WebConfigurationManager.AppSettings["DefaultRoleHQ"])
        {
            this.ASPxTextBox1.Enabled = false; this.ASPxTextBox1.Text = StringUtil.CStr(logMsg.STORENO);
            this.ASPxTextBox2.Enabled = false; this.ASPxTextBox2.Text = StringUtil.CStr(logMsg.STORENO);
        }

        getSALE_PERSON();
        BindDropDownList();
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
