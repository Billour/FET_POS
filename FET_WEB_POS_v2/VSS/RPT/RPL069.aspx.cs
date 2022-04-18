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

public partial class VSS_RPT_RPL069 : BasePage
{
    private int totalCount;
    protected void Page_Load(object sender, EventArgs e)
    {
        //if (logMsg.ROLE_TYPE == System.Configuration.ConfigurationManager.AppSettings["DefaultRoleHQ"].ToString())
        //{
        //    //彈跳視窗
        //    ScriptManager.RegisterClientScriptBlock(this, GetType(), "AlertMessage", "alert('非門市人員無法使用此功能!!');", true);
        //    this.txtPromotionCode1.Enabled = false;
        //    this.txtPromotionCode2.Enabled = false;
        //    this.txtOrdDateStart.Enabled = false;
        //    this.txtOrdDateEnd.Enabled = false;

        //    gvMaster.Enabled = false;
        //    gvExpoter.Enabled = false;

        //    btnSearch.Enabled = false;
        //    btnReset.Enabled = false;
        //    btnExport.Enabled = false;
        //    return;
        //}
        if (!IsPostBack && !Page.IsCallback)
        {
            btnReset_Click(null, null);
        }
    }

    private void BindMasterData()
    {
        string LS_STORE_NO = StringUtil.CStr(logMsg.STORENO);

        DataTable dt = new RPL_Facade().RPL069(this.txtPromotionCode1.Text, this.txtPromotionCode2.Text,
                                               this.txtOrdDateStart.Text, this.txtOrdDateEnd.Text, LS_STORE_NO);

        totalCount = dt.Rows.Count;
        gvMaster.Templates.PagerBar = new CustomPagerBarTemplate(totalCount);

        dt = new RPL_Facade().RPL069_TOTAL(this.txtPromotionCode1.Text, this.txtPromotionCode2.Text,
                                           this.txtOrdDateStart.Text, this.txtOrdDateEnd.Text, LS_STORE_NO);
        this.gvMaster.DataSource = dt;
        this.gvExpoter.DataSource = dt;
        
        this.gvMaster.DataBind();
        this.gvExpoter.DataBind();
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
        new Output().ExportXLS(800, "", Resources.WebResources.RPL069, dtHeader(), this.ASPxGridViewExporter1, null, null);
        Response.Redirect(Utils.CreateTamperProofDownloadXlsURL("RPL069.xls"));
    }

    private DataTable dtHeader()
    {
        DataTable dtHeader = new DataTable();
        dtHeader.Columns.Add("Text", typeof(string));
        dtHeader.Columns.Add("Align", typeof(string));
        dtHeader.Columns.Add("FontSize", typeof(string));
        dtHeader.Columns.Add("BackColor", typeof(string));

        DataRow NewRow = dtHeader.NewRow();
        NewRow["Text"] = "促銷代碼：" + this.txtPromotionCode1.Text + "～" + this.txtPromotionCode2.Text
            + "|交易日期：" + this.txtOrdDateStart.Text + "～" + this.txtOrdDateEnd.Text;
        NewRow["Align"] = "LEFT";
        NewRow["FontSize"] = "11";
        NewRow["BackColor"] = "WHITE";
        dtHeader.Rows.Add(NewRow);

        NewRow = dtHeader.NewRow();
        NewRow["Text"] = "列印日期：" + DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss")
            + "|列印人員：" + logMsg.MODI_USER + " " + new Employee_Facade().GetEmpName(logMsg.MODI_USER)
            + "|頁　　次：" + "1"
            + "|總筆數：" + totalCount; // this.gvMaster.VisibleRowCount;
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

    protected void btnReset_Click(object sender, EventArgs e)
    {
        //if (logMsg.ROLE_TYPE == StringUtil.CStr(System.Configuration.ConfigurationManager.AppSettings["DefaultRoleHQ"]))
        //{
        //    //彈跳視窗
        //    ScriptManager.RegisterClientScriptBlock(this, GetType(), "AlertMessage", "alert('非門市人員無法使用此功能!!');", true);
        //    this.txtPromotionCode1.Enabled = false;
        //    this.txtPromotionCode2.Enabled = false;
        //    this.txtOrdDateStart.Enabled = false;
        //    this.txtOrdDateEnd.Enabled = false;

        //    gvMaster.Enabled = false;
        //    gvExpoter.Enabled = false;

        //    btnSearch.Enabled = false;
        //    btnReset.Enabled = false;
        //    btnExport.Enabled = false;
        //    return;
        //}
        
        //查詢條件清空，下拉選單回復預設值        

        txtPromotionCode1.Text = null;
        txtPromotionCode2.Text = null;
        txtOrdDateStart.Text = null;
        txtOrdDateEnd.Text = null;

        gvMaster.DataSource = null;
        gvMaster.DataBind();

        gvExpoter.DataSource = null;
        gvExpoter.DataBind();

        BindDropDownList();
    }
}
