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
using System.Data.OracleClient;

public partial class VSS_RPT_RPL046 : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack && !Page.IsCallback)
        {
            btnReset_Click(null, null);
        }
    }

    private DataTable RPL046_DETAIL()
    {
        return new RPL_Facade().RPL046_DETAIL(
                                            this.txtSTORE_S.Text, this.txtSTORE_E.Text,
                                            this.txtTranDateStart.Text, this.txtTranDateEnd.Text,
                                            StringUtil.CStr(this.cbCashNo.SelectedItem.Value),
                                            this.ASPxTextBox1.Text,
                                            this.ASPxComboBox1.Text, this.ASPxComboBox2.Text,
                                            this.ASPxComboBox3.Text, this.ASPxComboBox4.Text,
                                            this.ASPxTextBox2.Text, this.ASPxTextBox3.Text,
                                            this.ASPxTextBox4.Text, this.ASPxTextBox5.Text,
                                            this.ASPxComboBox5.Text, this.ASPxComboBox6.Text
                                             );
    }

    private DataTable RPL046_DETAIL_TOTAL()
    {
        return new RPL_Facade().RPL046_DETAIL_TOTAL(
                                            this.txtSTORE_S.Text, this.txtSTORE_E.Text,
                                            this.txtTranDateStart.Text, this.txtTranDateEnd.Text,
                                           StringUtil.CStr(this.cbCashNo.SelectedItem.Value),
                                            this.ASPxTextBox1.Text,
                                            this.ASPxComboBox1.Text, this.ASPxComboBox2.Text,
                                            this.ASPxComboBox3.Text, this.ASPxComboBox4.Text,
                                            this.ASPxTextBox2.Text, this.ASPxTextBox3.Text,
                                            this.ASPxTextBox4.Text, this.ASPxTextBox5.Text,
                                            this.ASPxComboBox5.Text, this.ASPxComboBox6.Text
                                             );
    }

    private DataTable RPL046_SUM_TOTAL()
    {
        DataTable dt = new RPL_Facade().RPL046_SUM_TOTAL(
                                            this.txtSTORE_S.Text, this.txtSTORE_E.Text,
                                            this.txtTranDateStart.Text, this.txtTranDateEnd.Text,
                                            StringUtil.CStr(this.cbCashNo.SelectedItem.Value),
                                            this.ASPxTextBox1.Text,
                                            this.ASPxComboBox1.Text, this.ASPxComboBox2.Text,
                                            this.ASPxComboBox3.Text, this.ASPxComboBox4.Text,
                                            this.ASPxTextBox2.Text, this.ASPxTextBox3.Text,
                                            this.ASPxTextBox4.Text, this.ASPxTextBox5.Text,
                                            this.ASPxComboBox5.Text, this.ASPxComboBox6.Text,
                                            StringUtil.CStr(logMsg.STORENO) == System.Web.Configuration.WebConfigurationManager.AppSettings["DefaultRoleHQ"]
                                             );
        ViewState["RPL046_SUM"] = dt;

        return dt;
    }

    private void BindMasterData()
    {
        switch (ASPxRadioButtonList1.SelectedIndex)
        {
            case 0:
                DataTable dt = RPL046_SUM_TOTAL();
                this.gvSUM.DataSource = dt;
                this.gvSUM.DataBind();
                this.gvSUM.Visible = true;

                this.gvMaster.DataSource = null;
                this.gvMaster.DataBind();
                this.gvMaster.Visible = false;
                SetTITLE();
                break;
            case 1:
            default:
                this.gvMaster.DataSource = RPL046_DETAIL_TOTAL();//  RPL046_DETAIL_TOTAL();RPL046_DETAIL();
                this.gvMaster.DataBind();
                this.gvMaster.Visible = true;

                this.gvSUM.DataSource = null;
                this.gvSUM.DataBind();
                this.gvSUM.Visible = false;
                break;
        }


        if (StringUtil.CStr(logMsg.STORENO) != System.Web.Configuration.WebConfigurationManager.AppSettings["DefaultRoleHQ"])
        {
            this.txtSTORE_S.Enabled = false; this.txtSTORE_S.Text = StringUtil.CStr(logMsg.STORENO);
            this.txtSTORE_E.Enabled = false; this.txtSTORE_E.Text = StringUtil.CStr(logMsg.STORENO);
        }
    }

    private void BindDropDownList()
    {
        if (StringUtil.CStr(logMsg.STORENO) != System.Web.Configuration.WebConfigurationManager.AppSettings["DefaultRoleHQ"])
            this.cbCashNo.DataSource = RPL_PageHelper.GetMACHINE_ID(StringUtil.CStr(logMsg.STORENO));
        else
            this.cbCashNo.DataSource = RPL_PageHelper.GetMACHINE_ID(null);
        cbCashNo.TextField = "MACHINE_NAME";
        cbCashNo.ValueField = "MACHINE_ID";
        this.cbCashNo.DataBind();
        this.cbCashNo.SelectedIndex = 0;

    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        BindMasterData();
        gvMaster.PageIndex = 0;
        gvSUM.PageIndex = 0;
    }

    protected void btnExport_Click(object sender, EventArgs e)
    {
        DataTable dt = null;
        string filename = "";
        BindMasterData();
        switch (ASPxRadioButtonList1.SelectedIndex)
        {
            case 0:
                dt = RPL046_SUM_TOTAL();
                this.gvSUM.DataSource = dt;
                this.gvSUM.DataBind();
                this.DivSUM.Visible = true;

                this.gvMaster.DataSource = null;
                this.gvMaster.DataBind();
                this.gvMaster.Visible = false;

                SetTITLE();

                filename = new Output().PrintRPL046_SUM("RPL046_SUM", "門市銷售日報表(總表)", dtHeader(), dt, null);
                Response.Redirect(Utils.CreateTamperProofDownloadXlsURL("RPL046_SUM.xls"));
                break;

            case 1:
            default:
               // DataTable dtDetail = RPL046_DETAIL_TOTAL();
               // dt = RPL046_DETAIL();
                dt = RPL046_DETAIL_TOTAL();
                this.gvMaster.DataSource = dt;
                this.gvMaster.DataBind();
                this.gvMaster.Visible = true;

                this.gvSUM.DataSource = null;
                this.gvSUM.DataBind();
                this.DivSUM.Visible = false;

                filename = new Output().PrintRPL046("RPL046", "門市銷售日報表(明細表)", dtHeader(), dt, null);
                Response.Redirect(Utils.CreateTamperProofDownloadXlsURL("RPL046_DETAIL.xls"), true);
                break;
        }
    }

    private DataTable dtHeader()
    {
        DataTable dtheader = new DataTable();
        dtheader.Columns.Add("header1", typeof(string));
        dtheader.Columns.Add("header2", typeof(string));
        DataRow NewRowHeader = dtheader.NewRow();
        NewRowHeader["header1"] = "查詢條件： " + this.ASPxRadioButtonList1.SelectedItem.Text;
        NewRowHeader["header2"] = "列印日期： " + DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");
        dtheader.Rows.Add(NewRowHeader);

        NewRowHeader = dtheader.NewRow();
        NewRowHeader["header1"] = "門市編號： " + this.txtSTORE_S.Text + "～" + this.txtSTORE_E.Text;
        NewRowHeader["header2"] = "列印人員： " + logMsg.MODI_USER + " " + new Employee_Facade().GetEmpName(logMsg.MODI_USER);
        dtheader.Rows.Add(NewRowHeader);

        NewRowHeader = dtheader.NewRow();
        NewRowHeader["header1"] = "交易日期： " + this.txtTranDateStart.Text + "～" + this.txtTranDateEnd.Text;
        NewRowHeader["header2"] = "頁　　次：1 ";
        dtheader.Rows.Add(NewRowHeader);

        NewRowHeader = dtheader.NewRow();
        NewRowHeader["header1"] = "機台編號： " + this.cbCashNo.Text;
        NewRowHeader["header2"] = "總 筆 數： " + Convert.ToInt64(this.gvMaster.VisibleRowCount + this.gvSUM.VisibleRowCount);
        dtheader.Rows.Add(NewRowHeader);

        NewRowHeader = dtheader.NewRow();
        NewRowHeader["header1"] = "門號： " + this.ASPxTextBox1.Text;
        NewRowHeader["header2"] = "";
        dtheader.Rows.Add(NewRowHeader);

        NewRowHeader = dtheader.NewRow();
        NewRowHeader["header1"] = "商品類別： " + this.ASPxComboBox1.Text + "～" + this.ASPxComboBox2.Text;
        NewRowHeader["header2"] = "";
        dtheader.Rows.Add(NewRowHeader);

        NewRowHeader = dtheader.NewRow();
        NewRowHeader["header1"] = "商品料號： " + this.ASPxComboBox3.Text + "～" + this.ASPxComboBox4.Text;
        NewRowHeader["header2"] = "";
        dtheader.Rows.Add(NewRowHeader);

        NewRowHeader = dtheader.NewRow();
        NewRowHeader["header1"] = "發票號碼： " + this.ASPxTextBox2.Text + "～" + this.ASPxTextBox3.Text;
        NewRowHeader["header2"] = "";
        dtheader.Rows.Add(NewRowHeader);

        NewRowHeader = dtheader.NewRow();
        NewRowHeader["header1"] = "交易金額： " + this.ASPxTextBox4.Text + "～" + this.ASPxTextBox5.Text;
        NewRowHeader["header2"] = "";
        dtheader.Rows.Add(NewRowHeader);

        NewRowHeader = dtheader.NewRow();
        NewRowHeader["header1"] = "員工編號： " + this.ASPxComboBox5.Text + "～" + this.ASPxComboBox6.Text;
        NewRowHeader["header2"] = "";
        dtheader.Rows.Add(NewRowHeader);

        return dtheader;

    }

    protected void gvMaster_PageIndexChanged(object sender, EventArgs e)
    {
        BindMasterData();
    }

    protected void gvSUM_PageIndexChanged(object sender, EventArgs e)
    {

        gvSUM.DataSource = (DataTable)ViewState["RPL046_SUM"];
        this.gvSUM.DataBind();

        this.DivSUM.Visible = true;

        this.gvMaster.DataSource = null;
        this.gvMaster.DataBind();
        this.gvMaster.Visible = false;

        SetTITLE();

    }

    private void SetTITLE()
    {
        String str = gvSUM.FindChildControl<HiddenField>("MACHINE_ID").Value;

        switch (str)
        {
            case "-1":
                gvSUM.FindChildControl<Label>("txtSUM_TITLE").Text = "門市總計";
                break;
            default:
                gvSUM.FindChildControl<Label>("txtSUM_TITLE").Text = gvSUM.FindChildControl<HiddenField>("MACHINE_ID").Value + " 機台小計";
                break;
        }
    }

    protected void btnReset_Click(object sender, EventArgs e)
    {
        //查詢條件清空，下拉選單回復預設值        

        ASPxRadioButtonList1.SelectedIndex = 1;

        txtSTORE_S.Text = null;
        txtSTORE_E.Text = null;
        txtTranDateStart.Text = DateTime.Now.ToString("yyyy/MM/dd");
        txtTranDateEnd.Text = DateTime.Now.ToString("yyyy/MM/dd");

        cbCashNo.SelectedIndex = 0;
        ASPxTextBox1.Text = null;

        ASPxComboBox1.Text = null;
        ASPxComboBox2.Text = null;
        ASPxComboBox3.Text = null;
        ASPxComboBox4.Text = null;

        ASPxTextBox2.Text = null;
        ASPxTextBox3.Text = null;
        ASPxTextBox4.Text = null;
        ASPxTextBox5.Text = null;

        ASPxComboBox5.Text = null;
        ASPxComboBox6.Text = null;

        gvMaster.DataSource = null;
        gvMaster.DataBind();
        gvMaster.Visible = true;

        gvSUM.DataSource = null;
        gvSUM.DataBind();
        gvSUM.Visible = false;

        if (StringUtil.CStr(logMsg.STORENO) != System.Web.Configuration.WebConfigurationManager.AppSettings["DefaultRoleHQ"])
        {
            this.txtSTORE_S.Enabled = false; this.txtSTORE_S.Text = StringUtil.CStr(logMsg.STORENO);
            this.txtSTORE_E.Enabled = false; this.txtSTORE_E.Text = StringUtil.CStr(logMsg.STORENO);
        }

        BindDropDownList();
    }

}
