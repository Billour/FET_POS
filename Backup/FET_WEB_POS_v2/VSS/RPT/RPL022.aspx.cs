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

public partial class VSS_RPT_RPL022 : BasePage
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
        this.gvMaster.DataSource = new RPL_Facade().RPL022(this.txtOrdDateStart.Text, this.txtOrdDateEnd.Text, //交易日期
                                                           this.ASPxTextBox1.Text, //廠商編號
                                                           this.ASPxTextBox2.Text, //廠商名稱
                                                           this.ASPxTextBox3.Text, //商品料號
                                                           this.ASPxComboBox3.SelectedItem.Text); //服務類型
        this.gvMaster.DataBind();
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        BindMasterData();
        gvMaster.PageIndex = 0;
    }

    protected void gvMaster_PageIndexChanged(object sender, EventArgs e)
    {
        BindMasterData();
    }

    protected void btnReset_Click(object sender, EventArgs e)
    {

        if (logMsg.ROLE_TYPE != System.Web.Configuration.WebConfigurationManager.AppSettings["DefaultRoleHQ"])
        {
            ScriptManager.RegisterClientScriptBlock(this, GetType(), "AlertMessage", "alert('非總部人員無法使用此功能!!');", true);

            txtOrdDateStart.Enabled = false;
            txtOrdDateEnd.Enabled = false;
            ASPxTextBox1.Enabled = false;
            ASPxTextBox2.Enabled = false;
            ASPxTextBox3.Enabled = false;
            ASPxComboBox3.Enabled = false;

            btnSearch.Enabled = false;
            btnReset.Enabled = false;
            btnExport.Enabled = false;
            return;
        }

        //查詢條件清空，下拉選單回復預設值
        this.txtOrdDateStart.Text = null;
        this.txtOrdDateEnd.Text = null;
        this.ASPxTextBox1.Text = null;
        this.ASPxTextBox2.Text = null;
        this.ASPxTextBox3.Text = null;
        this.ASPxComboBox3.SelectedIndex = 0;
        gvMaster.DataSource = null;
        gvMaster.DataBind();
    }

    protected void btnExport_Click(object sender, EventArgs e)
    {
        //匯出至EXCEL
        BindMasterData();
        new Output().ExportXLS(1253, "", Resources.WebResources.RPL022, dtHeader(), this.ASPxGridViewExporter1, null, null);
        Response.Redirect(Utils.CreateTamperProofDownloadXlsURL("RPL022.xls"));
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
            + "|廠商編號：" + this.ASPxTextBox1.Text
            + "|廠商名稱：" + this.ASPxTextBox2.Text
            + "|商品料號：" + this.ASPxTextBox3.Text
            + "|服務類型：" + this.ASPxComboBox3.SelectedItem.Text
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
}
