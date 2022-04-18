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
using DevExpress.Web.ASPxGridView;

public partial class VSS_RPT_RPL054 : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack && !Page.IsCallback)
        {
            btnReset_Click(null, null);
        }
    }

    /*author：宗佑
      Date：100.2.17
      description：RPL056 後端程式
    */
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        //查詢
        BindMasterData();
        gvMaster.PageIndex = 0;
    }

    protected void btnExport_Click(object sender, EventArgs e)
    {
        //匯出
        BindMasterData();
        new Output().ExportXLS(600, "", Resources.WebResources.RPL054, dtHeader(), this.ASPxGridViewExporter1, null, null);
        Response.Redirect(Utils.CreateTamperProofDownloadXlsURL("RPL054.xls"));
    }

    protected void gvMaster_PageIndexChanged(object sender, EventArgs e)
    {
        BindMasterData();
    }


    private void BindMasterData()
    {
        string LS_STORE_NO = StringUtil.CStr(logMsg.STORENO);

        //判斷是否為一般商品或寄銷商品

        this.gvMaster.DataSource = new RPL_Facade().RPL054(StringUtil.CStr(rbCheck.SelectedItem.Value), this.txtTRADE_DATE_S.Text, this.txtTRADE_DATE_E.Text,
            this.pupTYPE_S.Text, this.pupTYPE_E.Text, this.txtPRODNO_S.Text, this.txtPRODNO_E.Text,
            StringUtil.CStr(this.ddlSALE_STATUS.SelectedItem.Value), LS_STORE_NO);
        this.gvExporter.DataSource = new RPL_Facade().RPL054_SUM(StringUtil.CStr(rbCheck.SelectedItem.Value), this.txtTRADE_DATE_S.Text, this.txtTRADE_DATE_E.Text,
            this.pupTYPE_S.Text, this.pupTYPE_E.Text, this.txtPRODNO_S.Text, this.txtPRODNO_E.Text,
            StringUtil.CStr(this.ddlSALE_STATUS.SelectedItem.Value), LS_STORE_NO);

        this.gvMaster.DataBind();
        this.gvExporter.DataBind();

    }

    private DataTable dtHeader()
    {
        //報表表頭
        DataTable dtHeader = new DataTable();
        dtHeader.Columns.Add("Text", typeof(string));
        dtHeader.Columns.Add("Align", typeof(string));
        dtHeader.Columns.Add("FontSize", typeof(string));
        dtHeader.Columns.Add("BackColor", typeof(string));

        DataRow NewRow = dtHeader.NewRow();
        NewRow["Text"] = "門市編號：" + ((logMsg.STORENO.Trim() == "HQ") ? "ALL" : logMsg.STORENO)
            + "|查詢條件：" + this.rbCheck.SelectedItem.Text
            + "|交易日期：" + this.txtTRADE_DATE_S.Text + "～" + this.txtTRADE_DATE_E.Text
            + "|商品類別：" + this.pupTYPE_S.Text + "～" + this.pupTYPE_E.Text
            + "|商品料號：" + this.txtPRODNO_S.Text + "～" + this.txtPRODNO_E.Text
            + "|銷售型態：" + this.ddlSALE_STATUS.SelectedItem.Text;

        NewRow["Align"] = "LEFT";
        NewRow["FontSize"] = "11";
        NewRow["BackColor"] = "WHITE";
        dtHeader.Rows.Add(NewRow);

        NewRow = dtHeader.NewRow();
        NewRow["Text"] = "列印日期：" + DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss")
            + "|列印人員：" + logMsg.MODI_USER + " " + new Employee_Facade().GetEmpName(logMsg.MODI_USER)
            + "|頁　　次：" + "1"
            + "|總筆數：" + this.gvMaster.VisibleRowCount
            + "||";
        NewRow["Align"] = "LEFT";
        NewRow["FontSize"] = "11";
        NewRow["BackColor"] = "WHITE";
        dtHeader.Rows.Add(NewRow);

        return dtHeader;

    }

    private DataTable dtFooter()
    {
        ////報表表尾
        //DataTable dt = new RPL_Facade().RPL054_SUM(rbCheck.SelectedItem.Value.ToString(), this.txtTRADE_DATE_S.Text, this.txtTRADE_DATE_E.Text,
        //    this.pupTYPE_S.Text, this.pupTYPE_E.Text, this.txtPRODNO_S.Text, this.txtPRODNO_E.Text,
        //    this.ddlSALE_STATUS.SelectedItem.Value.ToString());

        DataTable dtFooter = new DataTable();
        //dtFooter.Columns.Add("Text", typeof(string));
        //dtFooter.Columns.Add("Align", typeof(string));
        //dtFooter.Columns.Add("FontSize", typeof(string));
        //dtFooter.Columns.Add("BackColor", typeof(string));

        //DataRow dr1 = dtFooter.NewRow();
        //DataRow dr2 = dtFooter.NewRow();
        //DataRow dr3 = dtFooter.NewRow();

        //dr1["Text"] = "||";
        //dr2["Text"] = "||數量";
        //dr3["Text"] = "||金額";

        //foreach (DataRow dr in dt.Rows)
        //{
        //    dr1["Text"] = dr1["Text"].ToString() + "|" + dr["銷售型態"].ToString();
        //    dr2["Text"] = dr2["Text"].ToString() + "|" + dr["數量"].ToString();
        //    dr3["Text"] = dr3["Text"].ToString() + "|" + dr["金額"].ToString();
        //}

        //dr1["Align"] = "LEFT"; dr1["FontSize"] = "11"; dr1["BackColor"] = "WHITE"; dtFooter.Rows.Add(dr1);
        //dr2["Align"] = "LEFT"; dr2["FontSize"] = "11"; dr2["BackColor"] = "WHITE"; dtFooter.Rows.Add(dr2);
        //dr3["Align"] = "LEFT"; dr3["FontSize"] = "11"; dr3["BackColor"] = "WHITE"; dtFooter.Rows.Add(dr3);
        return dtFooter;
    }


    protected void btnReset_Click(object sender, EventArgs e)
    {
        //if (StringUtil.CStr(logMsg.ROLE_TYPE) == StringUtil.CStr(System.Configuration.ConfigurationManager.AppSettings["DefaultRoleHQ"]))
        //{
        //    //彈跳視窗
        //    ScriptManager.RegisterClientScriptBlock(this, GetType(), "AlertMessage", "alert('非門市人員無法使用此功能!!');", true);

        //    this.txtTRADE_DATE_S.Enabled = false;
        //    this.txtTRADE_DATE_E.Enabled = false;
        //    this.pupTYPE_S.Enabled = false;
        //    this.pupTYPE_E.Enabled = false;
        //    this.txtPRODNO_S.Enabled = false;
        //    this.txtPRODNO_E.Enabled = false;
        //    this.ddlSALE_STATUS.SelectedIndex = 0;
        //    this.rbCheck.SelectedIndex = 0;
        //    this.ddlSALE_STATUS.Enabled = false;
        //    this.rbCheck.Enabled = false;

        //    gvMaster.Enabled = false;
        //    gvExporter.Enabled = false;

        //    btnSearch.Enabled = false;
        //    btnReset.Enabled = false;
        //    btnExport.Enabled = false;
        //    return;
        //}

        //查詢條件清空，下拉選單回復預設值

        this.txtTRADE_DATE_S.Text = DateTime.Now.ToString("yyyy/MM/dd");
        this.txtTRADE_DATE_E.Text = DateTime.Now.ToString("yyyy/MM/dd");
        this.pupTYPE_S.Text = null;
        this.pupTYPE_E.Text = null;
        this.txtPRODNO_S.Text = null;
        this.txtPRODNO_E.Text = null;
        this.ddlSALE_STATUS.SelectedIndex = 0;
        this.rbCheck.SelectedIndex = 0;

        gvMaster.DataSource = null;
        gvMaster.DataBind();
        gvExporter.DataSource = null;
        gvExporter.DataBind();

    }

}
