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

public partial class VSS_RPT_RPL006 : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            btnReset_Click(null, null);
        }
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        BindMasterData();
        gvMaster.PageIndex = 0;
    }

    private void BindDropDownList()
    {
        //分期銀行
        this.ASPxComboBox2.DataSource = RPL_PageHelper.GetStagingBank();
        ASPxComboBox2.TextField = "BANK_NAME";
        ASPxComboBox2.ValueField = "BANK_ID";
        this.ASPxComboBox2.DataBind();
        this.ASPxComboBox2.SelectedIndex = 0;

        //分期期數
        this.ASPxComboBox1.DataSource = RPL_PageHelper.GeInstallmentsQty(StringUtil.CStr(ASPxComboBox2.SelectedItem.Value));
        ASPxComboBox1.TextField = "PAY_SEQMENT_NAME";
        ASPxComboBox1.ValueField = "PAY_SEQMENT";
        this.ASPxComboBox1.DataBind();
        this.ASPxComboBox1.SelectedIndex = 0;   

    }
    
    /*Author：宗佑
      Date：100.02.17
      Description：清除按鈕清空查詢條件
    */
    protected void btnReset_Click(object sender, EventArgs e)
    {

        if (logMsg.ROLE_TYPE != System.Web.Configuration.WebConfigurationManager.AppSettings["DefaultRoleHQ"])
        {
            ScriptManager.RegisterClientScriptBlock(this, GetType(), "AlertMessage", "alert('非總部人員無法使用此功能!!');", true);

            ASPxTextBox1.Enabled = false;
            ASPxTextBox4.Enabled = false;
            txtOrdDateStart.Enabled = false;
            txtOrdDateEnd.Enabled = false;
            ASPxComboBox2.Enabled = false;
            ASPxComboBox1.Enabled = false;

            btnSearch.Enabled = false;
            btnReset.Enabled = false;
            btnExport.Enabled = false;
            return;
        }

        //查詢條件清空，下拉選單回復預設值
        this.ASPxTextBox1.Text = null;
        this.ASPxTextBox4.Text = null;
        this.txtOrdDateStart.Text = null;
        this.txtOrdDateEnd.Text = null;
        this.ASPxComboBox2.SelectedIndex = 0;
        this.ASPxComboBox1.SelectedIndex = 0;
        gvMaster.DataSource = null;
        gvMaster.DataBind();
        BindDropDownList();
    }
    

    protected void btnExport_Click(object sender, EventArgs e)
    {
        BindMasterData();
        new Output().ExportXLS(1450, "", Resources.WebResources.RPL006, dtHeader(), this.ASPxGridViewExporter1, null, null);
        Response.Redirect(Utils.CreateTamperProofDownloadXlsURL("RPL006.xls"));

    }
    protected void gvMaster_PageIndexChanged(object sender, EventArgs e)
    {
        BindMasterData();
    }
    private void BindMasterData()
    {
        this.gvMaster.DataSource = new RPL_Facade().RPL006(this.ASPxTextBox1.Text, this.ASPxTextBox4.Text,
            this.txtOrdDateStart.Text, this.txtOrdDateEnd.Text, StringUtil.CStr(this.ASPxComboBox2.SelectedItem.Value ),
            StringUtil.CStr(this.ASPxComboBox1.SelectedItem.Value));
        this.gvMaster.DataBind();
    }
    private DataTable dtHeader()
    {
        DataTable dtHeader = new DataTable();
        dtHeader.Columns.Add("Text", typeof(string));
        dtHeader.Columns.Add("Align", typeof(string));
        dtHeader.Columns.Add("FontSize", typeof(string));
        dtHeader.Columns.Add("BackColor", typeof(string));

        DataRow NewRow = dtHeader.NewRow();
        NewRow["Text"] = "門市編號：" + this.ASPxTextBox1.Text + "～" + this.ASPxTextBox4.Text
            + "|交易日期：" + this.txtOrdDateStart.Text + "～" + this.txtOrdDateEnd.Text
            + "|分期銀行：" + this.ASPxComboBox2.SelectedItem.Text
            + "|分期期數：" + this.ASPxComboBox1.SelectedItem.Text;
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
    protected void ASPxComboBox2_SelectedIndexChanged(object sender, EventArgs e)
    {
        //分期期數
        this.ASPxComboBox1.DataSource = RPL_PageHelper.GeInstallmentsQty(StringUtil.CStr(ASPxComboBox2.SelectedItem.Value ));
        ASPxComboBox1.TextField = "PAY_SEQMENT_NAME";
        ASPxComboBox1.ValueField = "PAY_SEQMENT";
        this.ASPxComboBox1.DataBind();
        this.ASPxComboBox1.SelectedIndex = 0;   
    }
}
