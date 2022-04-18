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


public partial class VSS_RPT_RPL024 : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack && !Page.IsCallback)
        {
            BindDropDownList();
            //BindMasterData();
        }
    }

    private void BindMasterData()
    {
        this.gvMaster.DataSource = new RPL_Facade().RPL024(this.txtSTORE_NO_S.Text, this.txtSTORE_NO_E.Text, 
            this.txtADJDATE_S.Text, this.txtADJDATE_E.Text,StringUtil.CStr( this.ddlStock.SelectedItem.Value ), 
           StringUtil.CStr( this.ddlReason.SelectedItem.Value ), this.txtPRODNO_S.Text, this.txtPRODNO_E.Text);
        this.gvMaster.DataBind();
    }

    private void BindDropDownList()
    {
        this.ddlReason.DataSource = RPL_PageHelper.GetReason();
        ddlReason.TextField = "REASON";
        ddlReason.ValueField = "REASONID";
        this.ddlReason.DataBind();
        this.ddlReason.SelectedIndex = 0;

        this.ddlStock.DataSource = RPL_PageHelper.GetStock();
        ddlStock.TextField = "STOCK_NAME";
        ddlStock.ValueField = "LOC_ID";
        this.ddlStock.DataBind();
        this.ddlStock.SelectedIndex = 0;

    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        BindMasterData();
        this.gvMaster.PageIndex = 0;
    }

    protected void btnExport_Click(object sender, EventArgs e)
    {
        BindMasterData();
        new Output().ExportXLS(800, "", Resources.WebResources.RPL024, dtHeader(), this.ASPxGridViewExporter1, null, null);
        Response.Redirect(Utils.CreateTamperProofDownloadXlsURL("RPL024.xls")); 
    }

    private DataTable dtHeader()
    {
        DataTable dtHeader = new DataTable();
        dtHeader.Columns.Add("Text", typeof(string));
        dtHeader.Columns.Add("Align", typeof(string));
        dtHeader.Columns.Add("FontSize", typeof(string));
        dtHeader.Columns.Add("BackColor", typeof(string));

        DataRow NewRow = dtHeader.NewRow();
        NewRow["Text"] = "調整門市：" + this.txtSTORE_NO_S.Text + "~" + this.txtSTORE_NO_E.Text
            +"|調整日期："+this.txtADJDATE_S.Text +"~" + this.txtADJDATE_E.Text 
            +"|調整倉別："+this.ddlStock.SelectedItem.Text
            +"|調整原因："+this.ddlReason.SelectedItem.Text
            +"|商品編號：" + this.txtPRODNO_S.Text +"~"+ this.txtPRODNO_E.Text;
        NewRow["Align"] = "LEFT";
        NewRow["FontSize"] = "11";
        NewRow["BackColor"] = "WHITE";
        dtHeader.Rows.Add(NewRow);
        
        NewRow = dtHeader.NewRow();
        NewRow["Text"] = "列印日期：" + DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss") + "|列印人員：" + this.logMsg.MODI_USER + " " + new Employee_Facade().GetEmpName(this.logMsg.MODI_USER)
            + "|頁　　次：1"
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
}
