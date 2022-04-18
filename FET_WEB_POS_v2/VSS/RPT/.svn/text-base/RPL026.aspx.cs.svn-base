using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

public partial class VSS_RPT_RPL026 : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //cbTradeType_SelectedIndexChanged(null, null);
        if (!IsPostBack && !IsCallback)
            setDefault();
    }

    private void setDefault()
    {
        btnReset_Clicked(null, null);
    }

    private void BindMasterData()
    {
        if ((txtSTORE_NO_S.Text.Length == 0 && txtSTORE_NO_E.Text.Length != 0) || (txtSTORE_NO_S.Text.Length != 0 && txtSTORE_NO_E.Text.Length == 0))
        {
            ScriptManager.RegisterClientScriptBlock(this, typeof(string), "MissStoreNo", "alert('門市編號起訖兩欄位必須全填或是全空白!');", true);
            return;
        }
        if ((txtOUT_STORE_NO_S.Text.Length == 0 && txtOUT_STORE_NO_E.Text.Length != 0) || (txtOUT_STORE_NO_S.Text.Length != 0 && txtOUT_STORE_NO_E.Text.Length == 0))
        {
            ScriptManager.RegisterClientScriptBlock(this, typeof(string), "MissStoreOut", "alert('調出門市起訖兩欄位必須全填或是全空白!');", true);
            return;
        }
        if ((txtIN_STORE_NO_S.Text.Length == 0 && txtIN_STORE_NO_E.Text.Length != 0) || (txtIN_STORE_NO_S.Text.Length != 0 && txtIN_STORE_NO_E.Text.Length == 0))
        {
            ScriptManager.RegisterClientScriptBlock(this, typeof(string), "MissStoreIn", "alert('調入門市起訖兩欄位必須全填或是全空白!');", true);
            return;
        }
        if ((txtTRADE_DATE_S.Text.Length == 0 && txtTRADE_DATE_E.Text.Length != 0) || (txtTRADE_DATE_S.Text.Length != 0 && txtTRADE_DATE_E.Text.Length == 0))
        {
            ScriptManager.RegisterClientScriptBlock(this, typeof(string), "MissTradeDate", "alert('交易日期起訖兩欄位必須全填或是全空白!');", true);
            return;
        }
        if ((txtPRODNO_S.Text.Length == 0 && txtPRODNO_E.Text.Length != 0) || (txtPRODNO_S.Text.Length != 0 && txtPRODNO_E.Text.Length == 0))
        {
            ScriptManager.RegisterClientScriptBlock(this, typeof(string), "MissProdNo", "alert('商品料號起訖兩欄位必須全填或是全空白!');", true);
            return;
        }

        gvMaster.DataSource = new RPL_Facade().RPL026(StringUtil.CStr(cbTradeType.SelectedItem.Value ),
            txtSTORE_NO_S.Text, txtSTORE_NO_E.Text, txtTRADE_DATE_S.Text, txtTRADE_DATE_E.Text,
            txtOUT_STORE_NO_S.Text, txtOUT_STORE_NO_E.Text, txtIN_STORE_NO_S.Text, txtIN_STORE_NO_E.Text,
            txtPRODNO_S.Text, txtPRODNO_E.Text);
        gvMaster.DataBind();
    }

    private DataTable dtHeader()
    {
        DataTable dtHeader = new DataTable();
        dtHeader.Columns.Add("Text", typeof(string));
        dtHeader.Columns.Add("Align", typeof(string));
        dtHeader.Columns.Add("FontSize", typeof(string));
        dtHeader.Columns.Add("BackColor", typeof(string));

        DataRow NewRow = dtHeader.NewRow();
        StringBuilder sb = new StringBuilder();
        // 門市名稱
        sb.AppendFormat("{0}：", Resources.WebResources.StoreName);
        if (txtSTORE_NO_S.Text.Length == 0)
            sb.Append("ALL");
        else
            sb.AppendFormat("{0} {1} ~ {2} {3}", txtSTORE_NO_S.Text, new Store_Facade().GetStoreName(txtSTORE_NO_S.Text), txtSTORE_NO_E.Text, new Store_Facade().GetStoreName(txtSTORE_NO_E.Text));
        // 交易型態
        sb.AppendFormat("|{0}：{1}", Resources.WebResources.TradeType, cbTradeType.SelectedItem.Text);
        // 交易日期
        sb.AppendFormat("|{0}：", Resources.WebResources.TradeDate);
        if (txtTRADE_DATE_S.Text.Length == 0)
        {
            sb.Append("ALL");
        }
        else
        {
            DateTime date_s, date_e;
            string strdate_s = txtTRADE_DATE_S.Text;
            string strdate_e = txtTRADE_DATE_E.Text;
            if (DateTime.TryParse(strdate_s, out date_s))
            {
                strdate_s = date_s.ToString("yyyy/MM/dd");
            }

            if (DateTime.TryParse(strdate_e, out date_e))
            {
                strdate_e = date_e.ToString("yyyy/MM/dd");
            }
            sb.AppendFormat("{0}~{1}", strdate_s, strdate_e);
        }
        // 調出門市
        sb.AppendFormat("|{0}：", Resources.WebResources.OutputStore);
        if (txtOUT_STORE_NO_S.Text.Length == 0)
            sb.Append("ALL");
        else
            sb.AppendFormat("{0} {1} ~ {2} {3}", txtOUT_STORE_NO_S.Text, new Store_Facade().GetStoreName(txtOUT_STORE_NO_S.Text), txtOUT_STORE_NO_E.Text, new Store_Facade().GetStoreName(txtOUT_STORE_NO_E.Text));
        // 調入門市
        sb.AppendFormat("|{0}：", Resources.WebResources.InputStore);
        if (txtIN_STORE_NO_S.Text.Length == 0)
            sb.Append("ALL");
        else
            sb.AppendFormat("{0} {1} ~ {2} {3}", txtIN_STORE_NO_S.Text, new Store_Facade().GetStoreName(txtIN_STORE_NO_S.Text), txtIN_STORE_NO_E.Text, new Store_Facade().GetStoreName(txtIN_STORE_NO_E.Text));
        // 商品編號
        sb.AppendFormat("|{0}：", Resources.WebResources.ProductCode);
        if (txtPRODNO_S.Text.Length == 0)
            sb.Append("ALL");
        else
            sb.AppendFormat("{0}~{1}", txtPRODNO_S.Text, txtPRODNO_E.Text);
        NewRow["Text"] = StringUtil.CStr(sb );

        NewRow["Align"] = "LEFT";
        NewRow["FontSize"] = "11";
        NewRow["BackColor"] = "WHITE";
        dtHeader.Rows.Add(NewRow);

        NewRow = dtHeader.NewRow();
        NewRow["Text"] = "列印日期：" + DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss")
            + "|列印人員：" + logMsg.MODI_USER + " " + new Employee_Facade().GetEmpName(logMsg.MODI_USER)
            + "|頁　　次：1"
            + "|總筆數：" + this.gvMaster.VisibleRowCount;
        NewRow["Align"] = "LEFT";
        NewRow["FontSize"] = "11";
        NewRow["BackColor"] = "WHITE";
        dtHeader.Rows.Add(NewRow);

        return dtHeader;

    }
    protected void btnReset_Clicked(object sender, EventArgs e)
    {
        gvMaster.DataSource = null;
        gvMaster.DataBind();
        //gvMaster.PageIndex = 0;
        txtSTORE_NO_S.Text = "";
        txtSTORE_NO_E.Text = "";
        txtTRADE_DATE_S.Text = "";
        txtTRADE_DATE_E.Text = "";
        txtIN_STORE_NO_S.Text = "";
        txtIN_STORE_NO_E.Text = "";
        txtOUT_STORE_NO_S.Text = "";
        txtOUT_STORE_NO_E.Text = "";
        txtPRODNO_S.Text = "";
        txtPRODNO_E.Text = "";
        cbTradeType.SelectedIndex = 0;
        cbTradeType_SelectedIndexChanged(null, null);
    }

    protected void btnSearch_Clicked(object sender, EventArgs e)
    {
        BindMasterData();
        gvMaster.PageIndex = 0;

        if (cbTradeType.SelectedIndex == 1)
        {
            txtIN_STORE_NO_S.Enabled = false;
            txtIN_STORE_NO_E.Enabled = false;
            txtOUT_STORE_NO_S.Enabled = false;
            txtOUT_STORE_NO_E.Enabled = false;
            txtSTORE_NO_S.Enabled = true;
            txtSTORE_NO_E.Enabled = true;
        }
        else if (cbTradeType.SelectedIndex == 2)
        {
            txtIN_STORE_NO_S.Enabled = true;
            txtIN_STORE_NO_E.Enabled = true;
            txtOUT_STORE_NO_S.Enabled = true;
            txtOUT_STORE_NO_E.Enabled = true;
            txtSTORE_NO_S.Enabled = false;
            txtSTORE_NO_E.Enabled = false;
        }
        else
        {
            txtIN_STORE_NO_S.Enabled = true;
            txtIN_STORE_NO_E.Enabled = true;
            txtOUT_STORE_NO_S.Enabled = true;
            txtOUT_STORE_NO_E.Enabled = true;
            txtSTORE_NO_S.Enabled = true;
            txtSTORE_NO_E.Enabled = true;
        }
    }

    protected void btnExport_Click(object sender, EventArgs e)
    {
        BindMasterData();
        new Output().ExportXLS(1100, "", Resources.WebResources.RPL026, dtHeader(), this.ASPxGridViewExporter1, null, null);
        Response.Redirect(Utils.CreateTamperProofDownloadXlsURL("RPL026.xls"));
    }

    protected void gvMaster_PageIndexChanged(object sender, EventArgs e)
    {
        BindMasterData();
    }


    protected void cbTradeType_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (cbTradeType.SelectedIndex == 1)
        {
            txtIN_STORE_NO_E.Text = string.Empty;
            txtIN_STORE_NO_S.Text = string.Empty;
            txtOUT_STORE_NO_E.Text = string.Empty;
            txtOUT_STORE_NO_S.Text = string.Empty;
            txtIN_STORE_NO_S.Enabled = false;
            txtIN_STORE_NO_E.Enabled = false;
            txtOUT_STORE_NO_S.Enabled = false;
            txtOUT_STORE_NO_E.Enabled = false;
            txtSTORE_NO_S.Enabled = true;
            txtSTORE_NO_E.Enabled = true;
        }
        else if (cbTradeType.SelectedIndex == 2)
        {
            txtIN_STORE_NO_S.Enabled = true;
            txtIN_STORE_NO_E.Enabled = true;
            txtOUT_STORE_NO_S.Enabled = true;
            txtOUT_STORE_NO_E.Enabled = true;
            txtSTORE_NO_S.Enabled = false;
            txtSTORE_NO_E.Enabled = false;
            txtSTORE_NO_E.Text = string.Empty;
            txtSTORE_NO_S.Text = string.Empty;
        }
        else
        {
            txtIN_STORE_NO_E.Text = string.Empty;
            txtIN_STORE_NO_S.Text = string.Empty;
            txtOUT_STORE_NO_E.Text = string.Empty;
            txtOUT_STORE_NO_S.Text = string.Empty;
            txtIN_STORE_NO_S.Enabled = true;
            txtIN_STORE_NO_E.Enabled = true;
            txtOUT_STORE_NO_S.Enabled = true;
            txtOUT_STORE_NO_E.Enabled = true;
            txtSTORE_NO_S.Enabled = true;
            txtSTORE_NO_E.Enabled = true;
            txtSTORE_NO_E.Text = string.Empty;
            txtSTORE_NO_S.Text = string.Empty;
        }
        gvMaster.DataSource = null;
        gvMaster.DataBind();

    }

    protected void gvMaster_HtmlRowPrepared(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewTableRowEventArgs e)
    {
        if (e.RowType == DevExpress.Web.ASPxGridView.GridViewRowType.Data)
        {


        }
    }


    protected void gvMaster_HtmlRowCreated(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewTableRowEventArgs e)
    {
        if (e.RowType == DevExpress.Web.ASPxGridView.GridViewRowType.Data)
        {
            DateTime STDATE;
            if (DateTime.TryParse(StringUtil.CStr(e.GetValue("STDATE") ), out STDATE))
            {

                e.Row.Cells[1].Text = STDATE.ToString("yyyy/MM/dd");
            }

            DateTime ETDATE;
            if (DateTime.TryParse(StringUtil.CStr(e.GetValue("ETDATE")), out ETDATE))
            {

                e.Row.Cells[9].Text = ETDATE.ToString("yyyy/MM/dd");
            }
        }
    }
}
