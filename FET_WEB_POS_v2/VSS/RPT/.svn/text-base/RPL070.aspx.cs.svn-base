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

public partial class VSS_RPT_RPL070 : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //Product_Type
        //if (logMsg.ROLE_TYPE == StringUtil.CStr(System.Configuration.ConfigurationManager.AppSettings["DefaultRoleHQ"]))
        //{
        //    //彈跳視窗
        //    ScriptManager.RegisterClientScriptBlock(this, GetType(), "AlertMessage", "alert('非門市人員無法使用此功能!!');", true);
        //    this.TradeDate_S.Enabled = false;
        //    this.TradeDate_E.Enabled = false;
        //    this.Promotions_No.Enabled = false;
        //    this.Product_Type.Enabled = false;
        //    this.Product_Code.Enabled = false;
        //    this.TradeMo_S.Enabled = false;
        //    this.TradeMo_E.Enabled = false;
        //    this.Employee_No.Enabled = false;

        //    gvMaster.Enabled = false;

        //    btnSearch.Enabled = false;
        //    btnReset.Enabled = false;
        //    btnExport.Enabled = false;
        //    return;
        //}
        if (!IsPostBack)
        {
            btnReset_Click(null, null);
            GetCboProductCategory();
        }
    }

    /// <summary>
    /// 取得商品類別下拉選單datasource
    /// </summary>
    protected void GetCboProductCategory()
    {
        cboProductCategory.TextField = "CATE_NAME";
        cboProductCategory.ValueField = "CATE_NO";
        cboProductCategory.DataSource = new RPL_PageHelper().GetProductCategory();
        cboProductCategory.DataBind();
        cboProductCategory.SelectedIndex = 0;
    }

    private void BindMasterData()
    {
        string LS_STORE_NO = StringUtil.CStr(logMsg.STORENO);

        string ProductCategory = "";
        if (this.cboProductCategory.Text != "")
        {
            if (StringUtil.CStr(this.cboProductCategory.SelectedItem.Value) != "ALL")
                ProductCategory = StringUtil.CStr(this.cboProductCategory.SelectedItem.Value);
        }

        string PERSON = "";
        if(StringUtil.CStr(this.cbSALE_PERSON.SelectedItem.Value) != "ALL")
            PERSON = StringUtil.CStr(this.cbSALE_PERSON.SelectedItem.Value);

        this.gvMaster.DataSource = new RPL_Facade().RPL070(this.TradeDate_S.Text, this.TradeDate_E.Text, //交易日期
                                                           this.Promotions_No.Text, //促銷代號
                                                           ProductCategory, //商品類別
                                                           this.Product_Code.Text, //商品料號
                                                           this.TradeMo_S.Text, this.TradeMo_E.Text, //交易金額
                                                           PERSON,//銷售人員
                                                           LS_STORE_NO);            //登入門市編號
        this.gvMaster.DataBind();
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        BindMasterData();
        gvMaster.PageIndex = 0;
    }

    protected void btnReset_Click(object sender, EventArgs e)
    {
        //查詢條件清空，回復預設值
        this.TradeDate_S.Text = null;
        this.TradeDate_E.Text = null;
        this.Promotions_No.Text = null;
        this.cboProductCategory.Text = null;
        this.Product_Code.Text = null;
        this.TradeMo_S.Text = null;
        this.TradeMo_E.Text = null;
        this.cbSALE_PERSON.Text = null;//銷售人員
        gvMaster.DataSource = null;
        gvMaster.DataBind();
        getSALE_PERSON();
    }

    protected void btnExport_Click(object sender, EventArgs e)
    {
        //匯出至EXCEL
        BindMasterData();
        new Output().ExportXLS(1253, "", Resources.WebResources.RPL070, dtHeader(), this.ASPxGridViewExporter1, null, null);
        Response.Redirect(Utils.CreateTamperProofDownloadXlsURL("RPL070.xls"));
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
            + "交易日期：" + this.TradeDate_S.Text + "～" + this.TradeDate_E.Text
            + "|促銷代號：" + this.Promotions_No.Text
            + "|商品類別：" + this.cboProductCategory.Text
            + "|商品料號：" + this.Product_Code.Text
            + "|交易金額：" + this.TradeMo_S.Text + "～" + this.TradeMo_E.Text
            + "|銷售人員：" + this.cbSALE_PERSON.SelectedItem.Text //銷售人員
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

    //處理人員
    void getSALE_PERSON()
    {
        DataTable dtSalePerson = new SAL02_Facade().getSalePersonInSaleHead(logMsg.STORENO);
        cbSALE_PERSON.DataSource = dtSalePerson;
        cbSALE_PERSON.ValueField = "EMPNO";
        cbSALE_PERSON.TextField = "EMP_SHOWNAME";
        cbSALE_PERSON.DataBind();
        cbSALE_PERSON.Items.Insert(0, new DevExpress.Web.ASPxEditors.ListEditItem("ALL", ""));
        DataRow[] drs = null;
        if (dtSalePerson != null && dtSalePerson.Rows.Count > 0)
            drs = dtSalePerson.Select(" EMPNO = '" + logMsg.OPERATOR + "'");
        if (drs == null || drs.Length == 0)
        {
            Employee_Facade empFacade = new Employee_Facade();
            string empName = empFacade.GetEmpName(logMsg.MODI_USER);
            cbSALE_PERSON.Items.Insert(1, new DevExpress.Web.ASPxEditors.ListEditItem(empName + "-" + logMsg.OPERATOR, logMsg.OPERATOR));
        }
        cbSALE_PERSON.SelectedIndex = cbSALE_PERSON.Items.IndexOfValue(logMsg.OPERATOR);
    }
}
