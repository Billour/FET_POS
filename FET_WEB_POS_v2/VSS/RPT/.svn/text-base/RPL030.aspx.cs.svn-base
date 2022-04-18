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

public partial class VSS_RPT_RPL030 : BasePage
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
        this.gvMaster.DataSource = new RPL_Facade().RPL030(this.txtSTORE_NO_S.Text, this.txtSTORE_NO_E.Text,
            this.txtTranDateStart.Text, this.txtTranDateEnd.Text, this.txtPRODTYPENO_S.Text,
            this.txtPRODTYPENO_E.Text, this.txtPRODNO_S.Text, this.txtPRODNO_E.Text);
        this.gvMaster.DataBind();

        this.gvExport.DataSource = new RPL_Facade().RPL030_TOTAL(this.txtSTORE_NO_S.Text, this.txtSTORE_NO_E.Text,
            this.txtTranDateStart.Text, this.txtTranDateEnd.Text, this.txtPRODTYPENO_S.Text,
            this.txtPRODTYPENO_E.Text, this.txtPRODNO_S.Text, this.txtPRODNO_E.Text);
        this.gvExport.DataBind();

        if (StringUtil.CStr(logMsg.STORENO) != System.Web.Configuration.WebConfigurationManager.AppSettings["DefaultRoleHQ"])
        {
            txtSTORE_NO_S.Text = StringUtil.CStr(logMsg.STORENO); txtSTORE_NO_S.Enabled = false;
            txtSTORE_NO_E.Text = StringUtil.CStr(logMsg.STORENO); txtSTORE_NO_E.Enabled = false;
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
        new Output().ExportXLS(800, "", Resources.WebResources.RPL030, dtHeader(), this.ASPxGridViewExporter1, null, null);
        Response.Redirect(Utils.CreateTamperProofDownloadXlsURL("RPL030.xls"));
    }

    private DataTable dtHeader()
    {
        DataTable dtHeader = new DataTable();
        dtHeader.Columns.Add("Text", typeof(string));
        dtHeader.Columns.Add("Align", typeof(string));
        dtHeader.Columns.Add("FontSize", typeof(string));
        dtHeader.Columns.Add("BackColor", typeof(string));

        DataRow NewRow = dtHeader.NewRow();
        NewRow["Text"] = "門市編號：" + this.txtSTORE_NO_S.Text + "～" + this.txtSTORE_NO_E.Text
            + "|商品類別：" + this.txtPRODTYPENO_S.Text + "～" + this.txtPRODTYPENO_E.Text
            + "|商品料號：" + this.txtPRODNO_S.Text + "～" + this.txtPRODNO_E.Text
            + "|交易日期：" + this.txtTranDateStart.Text + "～" + this.txtTranDateEnd.Text;
        NewRow["Align"] = "LEFT";
        NewRow["FontSize"] = "11";
        NewRow["BackColor"] = "WHITE";
        dtHeader.Rows.Add(NewRow);

        DataRow NewRow2 = dtHeader.NewRow();
        NewRow2["Text"] = "列印日期：" + DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss")
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
        //if (logMsg.ROLE_TYPE != System.Web.Configuration.WebConfigurationManager.AppSettings["DefaultRoleHQ"])
        //{
        //    ScriptManager.RegisterClientScriptBlock(this, GetType(), "AlertMessage", "alert('非總部人員無法使用此功能!!');", true);

        //    txtSTORE_NO_S.Enabled = false;
        //    txtSTORE_NO_E.Enabled = false;
        //    txtTranDateStart.Enabled = false;
        //    txtTranDateEnd.Enabled = false;
        //    txtPRODTYPENO_S.Enabled = false;
        //    txtPRODTYPENO_E.Enabled = false;

        //    txtPRODNO_S.Enabled = false;
        //    txtPRODNO_E.Enabled = false;

        //    btnSearch.Enabled = false;
        //    btnReset.Enabled = false;
        //    btnExport.Enabled = false;
        //    return;
        //}
        BindDropDownList();
        
        //查詢條件清空，下拉選單回復預設值

        this.txtSTORE_NO_S.Text = null;
        this.txtSTORE_NO_E.Text = null;
        this.txtPRODTYPENO_S.Text = null;
        this.txtPRODTYPENO_E.Text = null;
        this.txtPRODNO_S.Text = null;
        this.txtPRODNO_E.Text = null;
        this.txtTranDateStart.Text = null;
        this.txtTranDateEnd.Text = null;

        gvMaster.DataSource = null;
        gvMaster.DataBind();

        if (StringUtil.CStr(logMsg.STORENO) != System.Web.Configuration.WebConfigurationManager.AppSettings["DefaultRoleHQ"])
        {
            txtSTORE_NO_S.Text = StringUtil.CStr(logMsg.STORENO); txtSTORE_NO_S.Enabled = false;
            txtSTORE_NO_E.Text = StringUtil.CStr(logMsg.STORENO); txtSTORE_NO_E.Enabled = false;
        }
    }
}
