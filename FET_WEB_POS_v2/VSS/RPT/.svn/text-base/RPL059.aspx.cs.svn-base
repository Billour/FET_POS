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

public partial class VSS_RPT_RPL059 : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack && !Page.IsCallback)
        {
            btnReset_Click(null, null);
            
            ////Default
            //this.txtTranDateStart.Text = DateTime.Now.ToString("yyyy/MM/dd");
            //this.txtTranDateEnd.Text = DateTime.Now.ToString("yyyy/MM/dd");

            //BindDropDownList();
            //gvMaster.DataSource = null;
            //gvMaster.DataBind();

            //gvSum.DataSource = null;
            //gvSum.DataBind();

            //gvExport.DataSource = null;
            //gvExport.DataBind();

        }
    }

    private void BindMasterData()
    {

        string storeNo = string.Empty;
        if (logMsg.ROLE_TYPE != System.Web.Configuration.WebConfigurationManager.AppSettings["DefaultRoleHQ"])
        {
            //非總部人員只能查自己門市的資料
            storeNo = logMsg.STORENO;
        }

        DataTable dtM = null, dtD = null;

        dtM = new RPL_Facade().RPL059(this.txtTranDateStart.Text, this.txtTranDateEnd.Text,
            this.txtTranAmount1.Text, this.txtTranAmount2.Text, StringUtil.CStr(this.cbTranType.SelectedItem.Text ),
            StringUtil.CStr(this.cbCardType.SelectedItem.Text), storeNo);

        dtD = new RPL_Facade().RPL059_SUM(this.txtTranDateStart.Text, this.txtTranDateEnd.Text,
            this.txtTranAmount1.Text, this.txtTranAmount2.Text, StringUtil.CStr(this.cbTranType.SelectedItem.Text),
            StringUtil.CStr(this.cbCardType.SelectedItem.Text), storeNo);

        DataTable dtExport = dtM.Copy();

        DataRow drM = dtExport.NewRow();

        drM["信用卡號"] = "";
        drM["信用卡金額"] = "金額";
        drM["備註"] = "數量";
        dtExport.Rows.Add(drM);

        foreach (DataRow drD in dtD.Rows)
        {
            drM = dtExport.NewRow();
            drM["信用卡號"] = drD["卡別"];
            drM["信用卡金額"] = StringUtil.CInt( drD["金額"]);
            drM["備註"] = StringUtil.CInt(drD["數量"]);
            dtExport.Rows.Add(drM);
        }


        this.gvMaster.DataSource = dtM;
        this.gvMaster.DataBind();

        this.gvSum.DataSource = dtD;
        this.gvSum.DataBind();

        this.gvExport.DataSource = dtExport;
        this.gvExport.DataBind();
    }

    private void BindDropDownList()
    {
        this.cbCardType.DataSource = RPL_PageHelper.GetCARD_TYPE();
        cbCardType.TextField = "CCT_NAME";
        cbCardType.ValueField = "CCT_NO";
        this.cbCardType.DataBind();
        this.cbCardType.SelectedIndex = 0;
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        BindMasterData();
        gvMaster.PageIndex = 0;
    }

    protected void btnExport_Click(object sender, EventArgs e)
    {
        BindMasterData();
        new Output().ExportXLS(800, "", Resources.WebResources.RPL059, dtHeader(), this.ASPxGridViewExporter1,null, null);
        Response.Redirect(Utils.CreateTamperProofDownloadXlsURL("RPL059.xls"));
    }

    private DataTable dtHeader()
    {
        DataTable dtHeader = new DataTable();
        dtHeader.Columns.Add("Text", typeof(string));
        dtHeader.Columns.Add("Align", typeof(string));
        dtHeader.Columns.Add("FontSize", typeof(string));
        dtHeader.Columns.Add("BackColor", typeof(string));

        DataRow NewRow = dtHeader.NewRow();
        NewRow["Text"] = "門市編號：" + logMsg.STORENO
            + "|交易日期：" + this.txtTranDateStart.Text + "～" + this.txtTranDateEnd.Text
            + "|交易金額：" + this.txtTranAmount1.Text + "～" + this.txtTranAmount2.Text
            + "|卡別：" + this.cbCardType.SelectedItem.Text;
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

        this.cbTranType.SelectedIndex = 0;
        this.txtTranAmount1.Text = null;
        this.txtTranAmount2.Text = null;
        this.cbCardType.SelectedIndex = 0;
        this.txtTranDateStart.Text = DateTime.Now.ToString("yyyy/MM/dd");
        this.txtTranDateEnd.Text = DateTime.Now.ToString("yyyy/MM/dd");

        BindDropDownList();
        
        gvMaster.DataSource = null;
        gvMaster.DataBind();

        gvSum.DataSource = null;
        gvSum.DataBind();

        gvExport.DataSource = null;
        gvExport.DataBind();
    }

}
