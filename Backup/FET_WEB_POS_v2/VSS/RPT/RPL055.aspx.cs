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

public partial class VSS_RPT_RPL055 : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindDropDownList();
            btnReset_Click(null, null);
            if (StringUtil.CStr(logMsg.STORENO) != System.Web.Configuration.WebConfigurationManager.AppSettings["DefaultRoleHQ"])
            {
                ASPxTextBox2.Text = StringUtil.CStr(logMsg.STORENO); ASPxTextBox2.Enabled = false;
                ASPxTextBox3.Text = StringUtil.CStr(logMsg.STORENO); ASPxTextBox3.Enabled = false;
            }
        }
    }

    private void BindMasterData()
    {
        this.gvMaster.DataSource = new RPL_Facade().RPL055(this.ASPxTextBox2.Text, this.ASPxTextBox3.Text,      // 門市編號
                                                           this.ASPxComboBox1.Text, this.ASPxComboBox2.Text,    // 商品類別
                                                           this.ASPxComboBox4.Text, this.ASPxComboBox5.Text,    // 商品料號
                                                           this.txtOrdDateStart.Text, this.txtOrdDateEnd.Text,  // 交易日期
                                                           this.ASPxTextBox1.Text, this.ASPxTextBox4.Text,      // 單品金額
                                                           this.ASPxTextBox5.Text, this.ASPxTextBox6.Text);     // 銷售金額
        this.gvMaster.DataBind();
        if (StringUtil.CStr(logMsg.STORENO) != System.Web.Configuration.WebConfigurationManager.AppSettings["DefaultRoleHQ"])
        {
            ASPxTextBox2.Text = StringUtil.CStr(logMsg.STORENO); ASPxTextBox2.Enabled = false;
            ASPxTextBox3.Text = StringUtil.CStr(logMsg.STORENO); ASPxTextBox3.Enabled = false;
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
        new Output().ExportXLS(920, "", Resources.WebResources.RPL055, dtHeader(), this.ASPxGridViewExporter1, null, null);
        Response.Redirect(Utils.CreateTamperProofDownloadXlsURL("RPL055.xls"));
    }
    // 門市編號
    // 商品類別
    // 商品料號
    // 交易日期
    // 單品金額
    // 銷售金額

    private DataTable dtHeader()
    {
        DataTable dtHeader = new DataTable();
        dtHeader.Columns.Add("Text", typeof(string));
        dtHeader.Columns.Add("Align", typeof(string));
        dtHeader.Columns.Add("FontSize", typeof(string));
        dtHeader.Columns.Add("BackColor", typeof(string));

        DataRow NewRow = dtHeader.NewRow();
        NewRow["Text"] = ""
            + "門市編號：" + this.ASPxTextBox2.Text + "～" + this.ASPxTextBox3.Text
            + "|商品類別：" + this.ASPxComboBox1.Text + "～" + this.ASPxComboBox2.Text
            + "|商品料號：" + this.ASPxComboBox4.Text + "～" + this.ASPxComboBox5.Text
            + "|交易日期：" + this.txtOrdDateStart.Text + "～" + this.txtOrdDateEnd.Text
            + "|單品金額：" + this.ASPxTextBox1.Text + "～" + this.ASPxTextBox4.Text
            + "|銷售金額：" + this.ASPxTextBox5.Text + "～" + this.ASPxTextBox6.Text
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

        this.ASPxTextBox2.Text = null;
        this.ASPxTextBox3.Text = null;
        this.ASPxComboBox1.Text = null;
        this.ASPxComboBox2.Text = null;
        this.ASPxComboBox4.Text = null;
        this.ASPxComboBox5.Text = null;
        this.txtOrdDateStart.Text = null;
        this.txtOrdDateEnd.Text = null;
        this.ASPxTextBox1.Text = null;
        this.ASPxTextBox4.Text = null;
        this.ASPxTextBox5.Text = null;
        this.ASPxTextBox6.Text = null;
        this.txtOrdDateStart.Text = DateTime.Now.ToString("yyyy/MM/dd");
        this.txtOrdDateEnd.Text = DateTime.Now.ToString("yyyy/MM/dd");

        gvMaster.DataSource = null;
        gvMaster.DataBind();

        if (StringUtil.CStr(logMsg.STORENO) != System.Web.Configuration.WebConfigurationManager.AppSettings["DefaultRoleHQ"])
        {
            ASPxTextBox2.Text = StringUtil.CStr(logMsg.STORENO); ASPxTextBox2.Enabled = false;
            ASPxTextBox3.Text = StringUtil.CStr(logMsg.STORENO); ASPxTextBox3.Enabled = false;
        }
    }
}
