using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

using Advtek.Utility;
using FET.POS.Model.Common;
using FET.POS.Model.DTO;
using FET.POS.Model.Facade;
using FET.POS.Model.Facade.FacadeImpl;
using DevExpress.Data;

public partial class VSS_RPT_RPL049 : BasePage
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
        this.gvMaster.DataSource = new RPL_Facade().RPL049(this.ASPxTextBox2.Text, this.ASPxTextBox3.Text,
            this.txtOrdDateStart.Text, this.txtOrdDateEnd.Text,
            this.rbPAID_MODE.SelectedItem.Text, this.txtCARDNO.Text);
        this.gvMaster.DataBind();

        this.gvExpoter.DataSource = new RPL_Facade().RPL049_TOTAL(this.ASPxTextBox2.Text, this.ASPxTextBox3.Text,
            this.txtOrdDateStart.Text, this.txtOrdDateEnd.Text,
            this.rbPAID_MODE.SelectedItem.Text, this.txtCARDNO.Text);
        this.gvExpoter.DataBind();
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
        new Output().ExportXLS(800, "", Resources.WebResources.RPL049, dtHeader(), this.ASPxGridViewExporter1, null, null);
        Response.Redirect(Utils.CreateTamperProofDownloadXlsURL("RPL049.xls"));
    }

    private DataTable dtHeader()
    {
        DataTable dtHeader = new DataTable();
        dtHeader.Columns.Add("Text", typeof(string));
        dtHeader.Columns.Add("Align", typeof(string));
        dtHeader.Columns.Add("FontSize", typeof(string));
        dtHeader.Columns.Add("BackColor", typeof(string));

        DataRow NewRow = dtHeader.NewRow();
        NewRow["Text"] = "門市編號：" + this.ASPxTextBox2.Text + "～" + this.ASPxTextBox3.Text
            + "|交易日期：" + this.txtOrdDateStart.Text + "～" + this.txtOrdDateEnd.Text
            + "|刷卡類型：" + this.rbPAID_MODE.SelectedItem.Text
            + "|信用卡卡號：" + this.txtCARDNO.Text;
        NewRow["Align"] = "LEFT";
        NewRow["FontSize"] = "11";
        NewRow["BackColor"] = "WHITE";
        dtHeader.Rows.Add(NewRow);

        NewRow = dtHeader.NewRow();
        NewRow["Text"] = "列印日期：" + DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss")
            + "|列印人員：" + logMsg.MODI_USER + " " + new Employee_Facade().GetEmpName(logMsg.MODI_USER)
            + "|頁　　次：" + "1"
            + "|總筆數：" + this.gvMaster.VisibleRowCount;
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

    protected void btnReset_Click(object sender, EventArgs e)
    {
        //查詢條件清空，下拉選單回復預設值        

        ASPxTextBox2.Text = null;
        ASPxTextBox3.Text = null;
        txtOrdDateStart.Text = null;
        txtOrdDateEnd.Text = null;
        rbPAID_MODE.SelectedIndex = 0;
        txtCARDNO.Text = null;

        gvMaster.DataSource = null;
        gvMaster.DataBind();

        gvExpoter.DataSource = null;
        gvExpoter.DataBind();

        BindDropDownList();
    }
}
