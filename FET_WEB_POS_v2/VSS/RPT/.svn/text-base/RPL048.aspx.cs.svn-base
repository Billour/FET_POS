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

public partial class VSS_RPT_RPL048 : BasePage
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
        this.gvMaster.DataSource = new RPL_Facade().RPL048(StringUtil.CStr(this.cbTYPE1.SelectedItem.Value),
            StringUtil.CStr(this.cbTYPE2.SelectedItem.Value), StringUtil.CStr(this.txtStoreNo.Text),
            this.txtTranDateStart.Text, this.txtTranDateEnd.Text, this.txtTransactionNo.Text,
            this.txtHGCardNo.Text, StringUtil.CStr(this.cbCashNo.SelectedItem.Value ) );
        this.gvMaster.DataBind();

        if (StringUtil.CStr(logMsg.STORENO) != System.Web.Configuration.WebConfigurationManager.AppSettings["DefaultRoleHQ"])
        {
            this.txtStoreNo.Enabled = false; this.txtStoreNo.Text = StringUtil.CStr(logMsg.STORENO);
        }
    }

    private void BindDropDownList()
    {
        this.cbCashNo.DataSource = RPL_PageHelper.GetMACHINE_ID(StringUtil.CStr(this.txtStoreNo.Text ));
        cbCashNo.TextField = "MACHINE_NAME";
        cbCashNo.ValueField = "MACHINE_ID";
        this.cbCashNo.DataBind();
        this.cbCashNo.SelectedIndex = 0;

    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        BindMasterData();
        gvMaster.PageIndex = 0;
    }

    protected void btnExport_Click(object sender, EventArgs e)
    {
        BindMasterData();
        new Output().ExportXLS(800, "", Resources.WebResources.RPL048, dtHeader(), this.ASPxGridViewExporter1, null, null);
        Response.Redirect(Utils.CreateTamperProofDownloadXlsURL("RPL048.xls"));
    }

    private DataTable dtHeader()
    {
        DataTable dtHeader = new DataTable();
        dtHeader.Columns.Add("Text", typeof(string));
        dtHeader.Columns.Add("Align", typeof(string));
        dtHeader.Columns.Add("FontSize", typeof(string));
        dtHeader.Columns.Add("BackColor", typeof(string));

        DataRow NewRow = dtHeader.NewRow();
        NewRow["Text"] = "類別：" + this.cbTYPE1.SelectedItem.Text
            + "|分類：" + this.cbTYPE2.SelectedItem.Text
            + "|門市編號：" + this.txtStoreNo.Text
            + "|交易日期：" + this.txtTranDateStart.Text + "～" + this.txtTranDateEnd.Text
            + "|交易序號：" + this.txtTransactionNo.Text
            + "|Happy Go卡號：" + this.txtHGCardNo.Text
            + "|機台編號：" + this.cbCashNo.Text;
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
        //查詢條件清空，下拉選單回復預設值        

        txtStoreNo.Text = null;
        txtTranDateStart.Text = null;
        txtTranDateEnd.Text = null;
        txtTransactionNo.Text = null;
        txtHGCardNo.Text = null;

        gvMaster.DataSource = null;
        gvMaster.DataBind();

        this.txtTranDateStart.Text = DateTime.Now.ToString("yyyy/MM/dd");
        this.txtTranDateEnd.Text = DateTime.Now.ToString("yyyy/MM/dd");

        if (StringUtil.CStr(logMsg.STORENO) != System.Web.Configuration.WebConfigurationManager.AppSettings["DefaultRoleHQ"])
        {
            this.txtStoreNo.Enabled = false; this.txtStoreNo.Text = StringUtil.CStr(logMsg.STORENO);
        }

        BindDropDownList();

        cbTYPE1.SelectedIndex = 0;
        cbTYPE2.SelectedIndex = 0;
        cbCashNo.SelectedIndex = 0;
    }
}
