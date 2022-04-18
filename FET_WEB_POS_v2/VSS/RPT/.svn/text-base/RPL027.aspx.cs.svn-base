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
public partial class VSS_RPT_RPL027 : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack && !IsCallback)
        {
        }
    }

    private void BindMasterData()
    {
        this.gvMaster.DataSource = new RPL_Facade().RPL027(txtSTORE_NO.Text, txtOrdDateStart.Text, txtOrdDateEnd.Text, StringUtil.CStr(ddlINV_TRAN_TYPE.SelectedItem.Value), txtPRODNO_S.Text, txtPRODNO_E.Text);
        this.gvMaster.DataBind();
    }


    protected void btnSearch_Click(object sender, EventArgs e)
    {
        this.gvMaster.PageIndex = 0;
        BindMasterData();
    }
    protected void gvMaster_PageIndexChanged(object sender, EventArgs e)
    {
        BindMasterData();
    }
    protected void btnExport_Click(object sender, EventArgs e)
    {
        BindMasterData();
        new Output().ExportXLS(800, "", Resources.WebResources.RPL027, dtHeader(), this.ASPxGridViewExporter1, null, null);
        Response.Redirect(Utils.CreateTamperProofDownloadXlsURL("RPL027.xls")); 
    }

    private DataTable dtHeader()
    {
        DataTable dtHeader = new DataTable();
        dtHeader.Columns.Add("Text", typeof(string));
        dtHeader.Columns.Add("Align", typeof(string));
        dtHeader.Columns.Add("FontSize", typeof(string));
        dtHeader.Columns.Add("BackColor", typeof(string));

        DataRow NewRow = dtHeader.NewRow();
        NewRow["Text"] = "門市：" + this.txtSTORE_NO.Text + " " + new Store_Facade().GetStoreName(this.txtSTORE_NO.Text)
            + "|交易型態：" + ddlINV_TRAN_TYPE.SelectedItem.Text
            + "|商品料號：" + txtPRODNO_S.Text + "~" + txtPRODNO_E.Text
            + "|日期區間：" + txtOrdDateStart.Text + "~" + txtOrdDateEnd.Text;
    
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

    private string getTRAN_TYPE(string type)
    {
        string result = "";
        switch (type)
        {
            case "CI":
                result = "進貨";
                break;
            case "SA":
                result = "銷售";
                break;
            case "SR":
                result = "銷退";
                break;
            case "AI":
                result = "庫存盤點調整";
                break;
            case "AO":
                result = "庫存盤點調整";
                break;
            case "SO":
                result = "門市移出";
                break;
            case "SI":
                result = "門市撥入";
                break;
            case "UO":
                result = "部門領用";
                break;
            case "RO":
                result = "退倉";
                break;
            case "RI":
                result = "驗退";
                break;
            case "TO":
                result = "退倉";
                break;

        }

        return result;
    }
}
