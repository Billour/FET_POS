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

public partial class VSS_RPT_RPL060 : BasePage
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
        DataTable DT = new RPL_Facade().RPL060(this.txtTranDateStart.Text, this.txtTranDateEnd.Text, this.txtMSISDN.Text, StringUtil.CStr(logMsg.STORENO));
        totalCount = DT.Rows.Count;
        gvMaster.Templates.PagerBar = new CustomPagerBarTemplate(totalCount);

        this.gvMaster.DataSource = new RPL_Facade().RPL060_SUM(this.txtTranDateStart.Text, this.txtTranDateEnd.Text, this.txtMSISDN.Text, StringUtil.CStr(logMsg.STORENO));
        this.gvMaster.DataBind();
    }

    private void BindDropDownList()
    {
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        BindMasterData();
    }

    protected void btnExport_Click(object sender, EventArgs e)
    {
        BindMasterData();
        new Output().ExportXLS(800, "", Resources.WebResources.RPL060, dtHeader(), this.ASPxGridViewExporter1, null, null);
        Response.Redirect(Utils.CreateTamperProofDownloadXlsURL("RPL060.xls"));
    }

    private DataTable dtHeader()
    {
        DataTable dtHeader = new DataTable();
        dtHeader.Columns.Add("Text", typeof(string));
        dtHeader.Columns.Add("Align", typeof(string));
        dtHeader.Columns.Add("FontSize", typeof(string));
        dtHeader.Columns.Add("BackColor", typeof(string));

        DataRow NewRow = dtHeader.NewRow();
        NewRow["Text"] = "交易日期：" + this.txtTranDateStart.Text + "～" + this.txtTranDateEnd.Text
            + "|門號：" + this.txtMSISDN.Text;
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
        //    this.cbTranType.Enabled = false;
        //    this.cbCardType.Enabled = false;
        //    this.txtTranAmount1.Enabled = false;
        //    this.txtTranAmount2.Enabled = false;
        //    this.txtTranDateStart.Enabled = false;
        //    this.txtTranDateEnd.Enabled = false;

        //    gvMaster.Enabled = false;
        //    gvSum.Enabled = false;
        //    gvExport.Enabled = false;

        //    btnSearch.Enabled = false;
        //    btnReset.Enabled = false;
        //    btnExport.Enabled = false;
        //    return;
        //}

        //查詢條件清空，下拉選單回復預設值
        //Default
        this.txtTranDateStart.Text = DateTime.Now.ToString("yyyy/MM/dd");
        this.txtTranDateEnd.Text = DateTime.Now.ToString("yyyy/MM/dd");
        this.txtMSISDN.Text = "";

        BindDropDownList();

        gvMaster.DataSource = null;
        gvMaster.DataBind();

    }

}

