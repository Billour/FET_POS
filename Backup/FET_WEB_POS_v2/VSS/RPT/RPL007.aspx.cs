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
using DevExpress.Web.ASPxEditors;

public partial class VSS_RPT_RPL007 : BasePage
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
        this.gvMaster.DataSource = new RPL_Facade().RPL007(this.ASPxDateEdit1.Text, this.ASPxDateEdit2.Text,
                                                           this.txtStore_S.Text, this.txtStore_E.Text,
                                                           this.txtProdNo_S.Text, this.txtProdNo_E.Text, this.txtPromoNo.Text,
                                                           StringUtil.CStr(this.cbSaleType.SelectedItem.Value),
                                                           StringUtil.CStr(this.cbTradeType.SelectedItem.Value),
                                                           StringUtil.CStr(this.cbServiceType.SelectedItem.Value),
                                                           StringUtil.CStr(this.cbSource_Type.SelectedItem.Value));
        this.gvMaster.DataBind();
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        BindMasterData(); gvMaster.PageIndex = 0;
    }

    protected void btnReset_Click(object sender, EventArgs e)
    {
        if (logMsg.ROLE_TYPE != System.Web.Configuration.WebConfigurationManager.AppSettings["DefaultRoleHQ"])
        {
            ScriptManager.RegisterClientScriptBlock(this, GetType(), "AlertMessage", "alert('非總部人員無法使用此功能!!');", true);

            ASPxDateEdit1.Enabled = false;
            ASPxDateEdit2.Enabled = false;
            txtStore_S.Enabled = false;
            txtStore_E.Enabled = false;
            txtProdNo_S.Enabled = false;
            txtProdNo_E.Enabled = false;
            txtPromoNo.Enabled = false;

            cbServiceType.Enabled = false;
            cbTradeType.Enabled = false;
            cbSaleType.Enabled = false;
            cbSource_Type.Enabled = false;

            btnSearch.Enabled = false;
            btnReset.Enabled = false;
            btnExport.Enabled = false;
            return;
        }

        //查詢條件清空，下拉選單回復預設值
        this.ASPxDateEdit1.Text = "";
        this.ASPxDateEdit2.Text = "";
        this.txtStore_S.Text = "";
        this.txtStore_E.Text = "";
        this.txtProdNo_S.Text = "";
        this.txtProdNo_E.Text = "";
        this.txtPromoNo.Text = "";
        this.cbServiceType.SelectedIndex = 0;
        this.cbTradeType.SelectedIndex = 0;
        this.cbSaleType.SelectedIndex = 0;
        this.cbSource_Type.SelectedIndex = 0;
        gvMaster.DataSource = null;
        gvMaster.DataBind();
    }

    protected void btnExport_Click(object sender, EventArgs e)
    {
        //匯出至EXCEL
        BindMasterData();
        new Output().ExportXLS(1312, "", Resources.WebResources.RPL07, dtHeader(), this.ASPxGridViewExporter1, null, null);
        Response.Redirect(Utils.CreateTamperProofDownloadXlsURL("RPL007.xls"));
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
            + "交易日期：" + this.ASPxDateEdit1.Text + "～" + this.ASPxDateEdit2.Text
            + "|門市編號：" + this.txtStore_S.Text + "～" + this.txtStore_E.Text
            + "|商品料號：" + this.txtProdNo_S.Text + "～" + this.txtProdNo_E.Text
            + "|促銷代碼：" + this.txtPromoNo.Text
            + "|交易類型：" + this.cbSaleType.SelectedItem.Text
            + "|交易類別：" + this.cbTradeType.SelectedItem.Text
            + "|服務類型：" + this.cbServiceType.SelectedItem.Text
            + "|資料來源：" + this.cbSource_Type.SelectedItem.Text
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
    protected void cbSaleType_OnChanged(object sender, EventArgs e)
    {
        //ALL
        if (this.cbSaleType.SelectedIndex == 0)
        {
            this.cbServiceType.Items.Clear();
            this.cbServiceType.Items.Add("ALL");
            this.cbServiceType.Items.Add("單品");
            this.cbServiceType.Items.Add("組合促銷");
            this.cbServiceType.Items.Add("Happy Go折抵");
            this.cbServiceType.Items.Add("銷售折扣");
            this.cbServiceType.Items.Add("門市特殊客訴折扣");
            this.cbServiceType.Items.Add("加價購商品");
            this.cbServiceType.Items.Add("HG來店禮");
            this.cbServiceType.Items.Add("授信通聯");
            this.cbServiceType.Items.Add("租賃");
            this.cbServiceType.Items.Add("租賃折扣");
            this.cbServiceType.Items.Add("舊機換新機折扣");
            this.cbServiceType.Items.Add("贈品");
            this.cbServiceType.Items.Add("Happy Go加價購");
            this.cbServiceType.Items.Add("遠傳帳單");
            this.cbServiceType.Items.Add("和信帳單");
            this.cbServiceType.Items.Add("Seednet帳單");
            this.cbServiceType.Items.Add("遠通帳單(有單)");
            this.cbServiceType.Items.Add("遠通帳單(無單)");
            this.cbServiceType.Items.Add("速博帳單");
            this.cbServiceType.Items[0].Text = "ALL";
            this.cbServiceType.Items[0].Value = "ALL";
            this.cbServiceType.Items[1].Text = "單品";
            this.cbServiceType.Items[1].Value = "1";
            this.cbServiceType.Items[2].Text = "組合促銷";
            this.cbServiceType.Items[2].Value = "2";
            this.cbServiceType.Items[3].Text = "Happy Go折抵";
            this.cbServiceType.Items[3].Value = "3";
            this.cbServiceType.Items[4].Text = "銷售折扣";
            this.cbServiceType.Items[4].Value = "4";
            this.cbServiceType.Items[5].Text = "門市特殊客訴折扣";
            this.cbServiceType.Items[5].Value = "5";
            this.cbServiceType.Items[6].Text = "加價購商品";
            this.cbServiceType.Items[6].Value = "6";
            this.cbServiceType.Items[7].Text = "HG來店禮";
            this.cbServiceType.Items[7].Value = "7";
            this.cbServiceType.Items[8].Text = "授信通聯";
            this.cbServiceType.Items[8].Value = "8";
            this.cbServiceType.Items[9].Text = "租賃";
            this.cbServiceType.Items[9].Value = "9";
            this.cbServiceType.Items[10].Text = "租賃折扣";
            this.cbServiceType.Items[10].Value = "10";
            this.cbServiceType.Items[11].Text = "舊機換新機折扣";
            this.cbServiceType.Items[11].Value = "11";
            this.cbServiceType.Items[12].Text = "贈品";
            this.cbServiceType.Items[12].Value = "12";
            this.cbServiceType.Items[13].Text = "Happy Go加價購";
            this.cbServiceType.Items[13].Value = "13";

            this.cbServiceType.Items[14].Text = "遠傳帳單";
            this.cbServiceType.Items[14].Value = "A1";
            this.cbServiceType.Items[15].Text = "和信帳單";
            this.cbServiceType.Items[15].Value = "A2";
            this.cbServiceType.Items[16].Text = "Seednet帳單";
            this.cbServiceType.Items[16].Value = "A3";
            this.cbServiceType.Items[17].Text = "遠通帳單(有單)";
            this.cbServiceType.Items[17].Value = "A4";
            this.cbServiceType.Items[18].Text = "遠通帳單(無單)";
            this.cbServiceType.Items[18].Value = "A5";
            this.cbServiceType.Items[19].Text = "速博帳單";
            this.cbServiceType.Items[19].Value = "A6";

        }
        //銷售
        else if (this.cbSaleType.SelectedIndex == 1)
        {
            this.cbServiceType.Items.Clear();
            this.cbServiceType.Items.Add("ALL");
            this.cbServiceType.Items.Add("單品");
            this.cbServiceType.Items.Add("組合促銷");
            this.cbServiceType.Items.Add("Happy Go折抵");
            this.cbServiceType.Items.Add("銷售折扣");
            this.cbServiceType.Items.Add("門市特殊客訴折扣");
            this.cbServiceType.Items.Add("加價購商品");
            this.cbServiceType.Items.Add("HG來店禮");
            this.cbServiceType.Items.Add("授信通聯");
            this.cbServiceType.Items.Add("租賃");
            this.cbServiceType.Items.Add("租賃折扣");
            this.cbServiceType.Items.Add("舊機換新機折扣");
            this.cbServiceType.Items.Add("贈品");
            this.cbServiceType.Items.Add("Happy Go加價購");
            this.cbServiceType.Items[0].Text = "ALL";
            this.cbServiceType.Items[0].Value = "ALL";
            this.cbServiceType.Items[1].Text = "單品";
            this.cbServiceType.Items[1].Value = "1";
            this.cbServiceType.Items[2].Text = "組合促銷";
            this.cbServiceType.Items[2].Value = "2";
            this.cbServiceType.Items[3].Text = "Happy Go折抵";
            this.cbServiceType.Items[3].Value = "3";
            this.cbServiceType.Items[4].Text = "銷售折扣";
            this.cbServiceType.Items[4].Value = "4";
            this.cbServiceType.Items[5].Text = "門市特殊客訴折扣";
            this.cbServiceType.Items[5].Value = "5";
            this.cbServiceType.Items[6].Text = "加價購商品";
            this.cbServiceType.Items[6].Value = "6";
            this.cbServiceType.Items[7].Text = "HG來店禮";
            this.cbServiceType.Items[7].Value = "7";
            this.cbServiceType.Items[8].Text = "授信通聯";
            this.cbServiceType.Items[8].Value = "8";
            this.cbServiceType.Items[9].Text = "租賃";
            this.cbServiceType.Items[9].Value = "9";
            this.cbServiceType.Items[10].Text = "租賃折扣";
            this.cbServiceType.Items[10].Value = "10";
            this.cbServiceType.Items[11].Text = "舊機換新機折扣";
            this.cbServiceType.Items[11].Value = "11";
            this.cbServiceType.Items[12].Text = "贈品";
            this.cbServiceType.Items[12].Value = "12";
            this.cbServiceType.Items[13].Text = "Happy Go加價購";
            this.cbServiceType.Items[13].Value = "13";
        }
        //代收
        else if (this.cbSaleType.SelectedIndex == 2)
        {
            this.cbServiceType.Items.Clear();
            this.cbServiceType.Items.Add("ALL");
            this.cbServiceType.Items.Add("遠傳帳單");
            this.cbServiceType.Items.Add("和信帳單");
            this.cbServiceType.Items.Add("Seednet帳單");
            this.cbServiceType.Items.Add("遠通帳單(有單)");
            this.cbServiceType.Items.Add("遠通帳單(無單)");
            this.cbServiceType.Items.Add("速博帳單");
            this.cbServiceType.Items[0].Text = "ALL";
            this.cbServiceType.Items[0].Value = "ALL";
            this.cbServiceType.Items[1].Text = "遠傳帳單";
            this.cbServiceType.Items[1].Value = "A1";
            this.cbServiceType.Items[2].Text = "和信帳單";
            this.cbServiceType.Items[2].Value = "A2";
            this.cbServiceType.Items[3].Text = "Seednet帳單";
            this.cbServiceType.Items[3].Value = "A3";
            this.cbServiceType.Items[4].Text = "遠通帳單(有單)";
            this.cbServiceType.Items[4].Value = "A4";
            this.cbServiceType.Items[5].Text = "遠通帳單(無單)";
            this.cbServiceType.Items[5].Value = "A5";
            this.cbServiceType.Items[6].Text = "速博帳單";
            this.cbServiceType.Items[6].Value = "A6";
        }

        this.cbServiceType.SelectedIndex = 0;

    }
}
