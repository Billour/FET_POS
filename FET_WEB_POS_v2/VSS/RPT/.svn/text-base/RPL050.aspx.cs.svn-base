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

public partial class VSS_RPT_RPL050 : BasePage
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
        this.gvMaster.DataSource = new RPL_Facade().RPL050(this.ASPxTextBox2.Text, this.ASPxTextBox3.Text,
                                                           this.txtOrdDateStart.Text, this.txtOrdDateEnd.Text);
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
        new Output().ExportXLS(800, "", Resources.WebResources.RPL050, dtHeader(), this.ASPxGridViewExporter1, null, null);
        Response.Redirect(Utils.CreateTamperProofDownloadXlsURL("RPL050.xls"));
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
                       + "|銷退日期：" + this.txtOrdDateStart.Text + "～" + this.txtOrdDateEnd.Text;

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
        //    ASPxTextBox2.Enabled = false;
        //    ASPxTextBox3.Enabled = false;
    
        //    btnSearch.Enabled = false;
        //    btnReset.Enabled = false;
        //    btnExport.Enabled = false;
        //    return;
        //}

        //查詢條件清空，下拉選單回復預設值        

        txtOrdDateStart.Text = null;
        txtOrdDateEnd.Text = null;
        ASPxTextBox2.Text = null;
        ASPxTextBox3.Text = null;

        gvMaster.DataSource = null;
        gvMaster.DataBind();

        BindDropDownList();

        if (StringUtil.CStr(logMsg.STORENO) != System.Web.Configuration.WebConfigurationManager.AppSettings["DefaultRoleHQ"])
        {
            ASPxTextBox2.Text = StringUtil.CStr(logMsg.STORENO); ASPxTextBox2.Enabled = false;
            ASPxTextBox3.Text = StringUtil.CStr(logMsg.STORENO); ASPxTextBox3.Enabled = false;
        }
    }
}