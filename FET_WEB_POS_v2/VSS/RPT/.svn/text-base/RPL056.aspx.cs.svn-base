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

public partial class VSS_RPT_RPL056 : BasePage
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
        this.gvMaster.DataSource = new RPL_Facade().RPL056((this.RadioButton1.Checked ? "TOTAL_AMOUNT" : "QUANTITY"),
                                                           StringUtil.CStr(this.ASPxComboBox3.SelectedItem.Value), this.ASPxTextBox2.Text, this.ASPxTextBox3.Text,
                                                           this.ASPxComboBox1.Text, this.ASPxComboBox2.Text,
                                                           this.ASPxComboBox4.Text, this.ASPxComboBox5.Text,
                                                           this.txtOrdDateStart.Text, this.txtOrdDateEnd.Text);
        this.gvMaster.DataBind();
        if (StringUtil.CStr(logMsg.STORENO) != System.Web.Configuration.WebConfigurationManager.AppSettings["DefaultRoleHQ"])
        {
            ASPxTextBox2.Text = StringUtil.CStr(logMsg.STORENO); ASPxTextBox2.Enabled = false;
            ASPxTextBox3.Text = StringUtil.CStr(logMsg.STORENO); ASPxTextBox3.Enabled = false;
            DefaultZone();
        }
    }

    private void BindDropDownList()
    {
        #region 區域別
        this.ASPxComboBox3.DataSource = RPL_PageHelper.GetZONE();
        this.ASPxComboBox3.TextField = "ZONE_NAME";
        this.ASPxComboBox3.ValueField = "ZONE";
        this.ASPxComboBox3.DataBind();
        this.ASPxComboBox3.SelectedIndex = 0;
        #endregion
    }

    private void DefaultZone()
    {
        string ZONE = RPL_PageHelper.GetZONE(logMsg.STORENO);

        for (int i = 0; i < ASPxComboBox3.Items.Count; i++)
        {
            if (StringUtil.CStr(ASPxComboBox3.Items[i].Value ) == ZONE)
            {
                ASPxComboBox3.SelectedIndex = i;
                break;
            }
        }

    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        BindMasterData();
        gvMaster.PageIndex = 0;
    }

    protected void btnExport_Click(object sender, EventArgs e)
    {
        BindMasterData();
        new Output().ExportXLS(920, "", Resources.WebResources.RPL056, dtHeader(), this.ASPxGridViewExporter1, null, null);
        Response.Redirect(Utils.CreateTamperProofDownloadXlsURL("RPL056.xls"));
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
            + "排序方式：" + (RadioButton1.Checked ? "金額" : "數量")
            + "|區域別：" + this.ASPxComboBox3.SelectedItem.Text
            + "|門市編號：" + this.ASPxTextBox2.Text + "～" + this.ASPxTextBox3.Text
            + "|商品類別：" + this.ASPxComboBox1.Text + "～" + this.ASPxComboBox2.Text
            + "|商品料號：" + this.ASPxComboBox4.Text + "～" + this.ASPxComboBox5.Text
            + "|交易日期：" + this.txtOrdDateStart.Text + "～" + this.txtOrdDateEnd.Text
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

    protected void btnReset_Click(object sender, EventArgs e)
    {
        //查詢條件清空，下拉選單回復預設值
        BindDropDownList();

        this.RadioButton1.Checked = true;

        this.ASPxComboBox1.Text = null;
        this.ASPxComboBox2.Text = null;
        this.ASPxComboBox3.SelectedIndex = 0;
        this.ASPxComboBox4.Text = null;
        this.ASPxComboBox5.Text = null;

        this.ASPxTextBox2.Text = "";
        this.ASPxTextBox3.Text = "";

        this.txtOrdDateStart.Text = null;
        this.txtOrdDateEnd.Text = null;

        gvMaster.DataSource = null;
        gvMaster.DataBind();

        if (StringUtil.CStr(logMsg.STORENO) != System.Web.Configuration.WebConfigurationManager.AppSettings["DefaultRoleHQ"])
        {
            ASPxTextBox2.Text = StringUtil.CStr(logMsg.STORENO); ASPxTextBox2.Enabled = false;
            ASPxTextBox3.Text = StringUtil.CStr(logMsg.STORENO); ASPxTextBox3.Enabled = false;
            DefaultZone();
        }
    }
}
