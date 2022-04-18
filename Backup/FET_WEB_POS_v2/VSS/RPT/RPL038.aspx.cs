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

public partial class VSS_RPT_RPL038 : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack && !Page.IsCallback)
        {
            if (logMsg.ROLE_TYPE != System.Web.Configuration.WebConfigurationManager.AppSettings["DefaultRoleHQ"])
            {
                ScriptManager.RegisterClientScriptBlock(this, GetType(), "AlertMessage", "alert('非總部人員無法使用此功能!!');", true);
                txtB_DATE_S.Enabled = false;
                txtB_DATE_E.Enabled = false;
                txtPROMO_NO_S.Enabled = false;
                txtPROMO_NO_E.Enabled = false;
                ddlPRODTYPE.Enabled = false;
                btnSearch.Enabled = false;
                btnReset.Enabled = false;
                btnExport.Enabled = false;
                return;
            }
            BindDropDownList();
            btnReset_Click(null, null);
        }
    }

    private void BindMasterData()
    {
        this.gvMaster.DataSource = new RPL_Facade().RPL038(this.txtB_DATE_S.Text, this.txtB_DATE_E.Text,
            this.txtPROMO_NO_S.Text, this.txtPROMO_NO_E.Text, ddlPRODTYPE.Text);
        this.gvMaster.DataBind();
    }

    private void BindDropDownList()
    {

    }

    private DataTable dtHeader()
    {
        DataTable dtHeader = new DataTable();
        dtHeader.Columns.Add("Text", typeof(string));
        dtHeader.Columns.Add("Align", typeof(string));
        dtHeader.Columns.Add("FontSize", typeof(string));
        dtHeader.Columns.Add("BackColor", typeof(string));

        DataRow NewRow = dtHeader.NewRow();
        NewRow["Text"] = "促銷生效日期：" + this.txtB_DATE_S.Text + " ~ " + this.txtB_DATE_E.Text
            + "|促銷代碼：" + this.txtPROMO_NO_S.Text + " ~ " + this.txtPROMO_NO_E.Text
            + "|商品類別：" + this.ddlPRODTYPE.Text;
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

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        BindMasterData();
        gvMaster.PageIndex = 0;
    }

    protected void btnExport_Click(object sender, EventArgs e)
    {
        BindMasterData();
        new Output().ExportXLS(800, "", Resources.WebResources.RPL038, dtHeader(), this.ASPxGridViewExporter1, null, null);
        Response.Redirect(Utils.CreateTamperProofDownloadXlsURL("RPL038.xls")); 
    }

    protected void gvMaster_PageIndexChanged(object sender, EventArgs e)
    {
        BindMasterData();
    }

    protected void btnReset_Click(object sender, EventArgs e)
    {
        //查詢條件清空，下拉選單回復預設值        

        txtB_DATE_S.Text = null;
        txtB_DATE_E.Text = null;
        txtPROMO_NO_S.Text = null;
        txtPROMO_NO_E.Text = null;
        ddlPRODTYPE.Text = null;

        gvMaster.DataSource = null;
        gvMaster.DataBind();
    }
}
