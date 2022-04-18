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

public partial class VSS_RPT_RPL041 : BasePage
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
        this.gvMaster.DataSource = new RPL_Facade().RPL041(this.txtTRADE_DATE_S.Text, this.txtTRADE_DATE_E.Text, //銷售日期
                                                           this.txtSTORE_NO_S.Text, this.txtSTORE_NO_E.Text); //門市編號
                                                           
        this.gvMaster.DataBind();
        if (StringUtil.CStr(logMsg.STORENO) != System.Web.Configuration.WebConfigurationManager.AppSettings["DefaultRoleHQ"])
        {
            this.txtSTORE_NO_S.Enabled = false;
            this.txtSTORE_NO_E.Enabled = false;
            this.txtSTORE_NO_S.Text = StringUtil.CStr(logMsg.STORENO);
            this.txtSTORE_NO_E.Text = StringUtil.CStr(logMsg.STORENO);
        }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        BindMasterData();
        gvMaster.PageIndex = 0;
    }

    protected void btnReset_Click(object sender, EventArgs e)
    {

        if (logMsg.ROLE_TYPE != System.Web.Configuration.WebConfigurationManager.AppSettings["DefaultRoleHQ"])
        {
            ScriptManager.RegisterClientScriptBlock(this, GetType(), "AlertMessage", "alert('非總部人員無法使用此功能!!');", true);

            txtTRADE_DATE_S.Enabled = false;
            txtTRADE_DATE_E.Enabled = false;
            txtSTORE_NO_S.Enabled = false;
            txtSTORE_NO_E.Enabled = false;

            btnSearch.Enabled = false;
            btnReset.Enabled = false;
            btnExport.Enabled = false;
            return;
        }

        //查詢條件清空，下拉選單回復預設值
        this.txtTRADE_DATE_S.Text = null;
        this.txtTRADE_DATE_E.Text = null;
        this.txtSTORE_NO_S.Text = null;
        this.txtSTORE_NO_E.Text = null;
        gvMaster.DataSource = null;
        gvMaster.DataBind();

        if (StringUtil.CStr(logMsg.STORENO)  != System.Web.Configuration.WebConfigurationManager.AppSettings["DefaultRoleHQ"])
        {
            this.txtSTORE_NO_S.Enabled = false;
            this.txtSTORE_NO_E.Enabled = false;
            this.txtSTORE_NO_S.Text = StringUtil.CStr(logMsg.STORENO);
            this.txtSTORE_NO_E.Text = StringUtil.CStr(logMsg.STORENO);
        }
    }

    protected void btnExport_Click(object sender, EventArgs e)
    {
        //匯出至EXCEL
        BindMasterData();
        new Output().ExportXLS(500, "", Resources.WebResources.RPL041, dtHeader(), this.ASPxGridViewExporter1, null, null);
        Response.Redirect(Utils.CreateTamperProofDownloadXlsURL("RPL041.xls"));
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
            + "銷售日期：" + this.txtTRADE_DATE_S.Text + "～" + this.txtTRADE_DATE_E.Text
            + "|門市編號：" + this.txtSTORE_NO_S.Text + "～" + this.txtSTORE_NO_E.Text
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
}
