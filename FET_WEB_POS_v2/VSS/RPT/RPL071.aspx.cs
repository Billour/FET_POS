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

public partial class VSS_RPT_RPL071 : BasePage
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
        this.gvMaster.DataSource = new RPL_Facade().RPL071(
            this.txtOrdDateStart.Text, this.txtOrdDateEnd.Text,
            this.popPLU.Text,
            StringUtil.CStr(this.ddlReason.SelectedItem.Value),
            this.popCOSTCENTER.Text,
            this.popEMPLOYEE.Text,
            StringUtil.CStr(logMsg.STORENO)
            );
        this.gvMaster.DataBind();
    }

    private void BindDropDownList()
    {
        this.ddlReason.DataSource = new RPL_PageHelper().getStoreDISReason();
        this.ddlReason.TextField = "STORE_DIS_REASON_DESC";
        this.ddlReason.ValueField = "STORE_DIS_REASON_ID";
        this.ddlReason.DataBind();
        this.ddlReason.SelectedIndex = 0;

    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        BindMasterData();
        gvMaster.PageIndex = 0;
    }

    protected void btnExport_Click(object sender, EventArgs e)
    {
        BindMasterData();
        new Output().ExportXLS(800, "", "折扣明細表", dtHeader(), this.ASPxGridViewExporter1, null, null);
        Response.Redirect(Utils.CreateTamperProofDownloadXlsURL("RPL071.xls"));
    }

    private DataTable dtHeader()
    {
        DataTable dtHeader = new DataTable();
        dtHeader.Columns.Add("Text", typeof(string));
        dtHeader.Columns.Add("Align", typeof(string));
        dtHeader.Columns.Add("FontSize", typeof(string));
        dtHeader.Columns.Add("BackColor", typeof(string));

        DataRow NewRow = dtHeader.NewRow();
        NewRow["Text"] = "日期區間：" + this.txtOrdDateStart.Text + "～" + this.txtOrdDateEnd.Text
            + "|商品料號：" + this.popPLU.Text
            + "|折扣原因：" + this.ddlReason.SelectedItem.Text
            + "|成本中心：" + this.popCOSTCENTER.Text
            + "|員工編號：" + this.popEMPLOYEE.Text;

        NewRow["Align"] = "LEFT";
        NewRow["FontSize"] = "11";
        NewRow["BackColor"] = "WHITE";
        dtHeader.Rows.Add(NewRow);

        NewRow = dtHeader.NewRow();
        NewRow["Text"] = "列印日期：" + DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss")
            + "|列印人員：" + logMsg.MODI_USER + " " + new Employee_Facade().GetEmpName(logMsg.MODI_USER)
            + "|頁　　次：" + "1"
            + "|總筆數：" + (this.gvMaster.VisibleRowCount - 1);
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
        //if (logMsg.ROLE_TYPE != System.Web.Configuration.WebConfigurationManager.AppSettings["DefaultRoleHQ"])
        //{
        //    ScriptManager.RegisterClientScriptBlock(this, GetType(), "AlertMessage", "alert('非總部人員無法使用此功能!!');", true);

        //    txtOrdDateStart.Enabled = false;
        //    txtOrdDateEnd.Enabled = false;
        //    popPLU.Enabled = false;
        //    ddlReason.Enabled = false;
        //    popCOSTCENTER.Enabled = false;
        //    popEMPLOYEE.Enabled = false;

        //    btnSearch.Enabled = false;
        //    btnReset.Enabled = false;
        //    btnExport.Enabled = false;
        //    return;
        //}
        //查詢條件清空，下拉選單回復預設值        

        txtOrdDateStart.Text = null;
        txtOrdDateEnd.Text = null;
        popPLU.Text = null;
        ddlReason.SelectedIndex = 0;
        popCOSTCENTER.Text = null;
        popEMPLOYEE.Text = null;

        gvMaster.DataSource = null;
        gvMaster.DataBind();

        BindDropDownList();
    }
}
