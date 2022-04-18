using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FET.POS.Model.Facade.FacadeImpl;
using System.Data;
using Advtek.Utility;

public partial class VSS_RPT_RPL065 : BasePage
{
    private bool isSearch = false;
    private int totalCount;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack && !Page.IsCallback)
        {

        }
    }

    private void BindMasterData()
    {
        DataTable dt = new RPL_Facade().RPL065(txtSDate.Text, txtEDate.Text, txtInvoiceNo.Text, pupProdNo.Text, txtSALE_NO.Text, pupEmployeeNo.Text);
        totalCount = dt.Rows.Count;
        gvMaster.Templates.PagerBar = new CustomPagerBarTemplate(totalCount);

        DataTable dtOutput = generateReportData(dt);
        this.gvMaster.DataSource = dtOutput;
        this.gvMaster.DataBind();
    }

    /// <summary>
    /// 產生報表資料 - 
    /// </summary>
    /// <param name="dt">原始資料的 DataTable</param>
    /// <returns>傳回有群組小計的 DataTable</returns>
    private static DataTable generateReportData(DataTable dt)
    {
        DataTable dtOutput = new DataTable();
        foreach (DataColumn column in dt.Columns)
        {
            if (column.ColumnName == "TRADE_DATE" || column.ColumnName == "INVOICE_DATE")
            {
                dtOutput.Columns.Add(column.ColumnName, typeof(string));
            }
            else
            {
                dtOutput.Columns.Add(column.ColumnName, column.DataType);
            }
        }

        //依 '交易型態' 作分組小計, '交易型態' 若為 '換貨作廢', 須與 '銷售' 群組在一起
        //string[] paidModeName = new string[] { "現金", "信用卡", "離線信用卡", "分期付款", "禮券", "金融卡", "Happy Go" };
        //decimal[] sumInvoicePrice = new decimal[7];
        //Dictionary<string, decimal> sumInvoicePrice = new Dictionary<string, decimal>();
        //string currentGroupFieldValue = "";
        //string currentGroupSaleNoValue = "";
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            DataRow row = dt.Rows[i];

            //fill data
            DataRow dataRow = dtOutput.NewRow();
            dataRow["SALE_STATUS"] = row["SALE_STATUS"];
            dataRow["TRADE_DATE"] = (row["TRADE_DATE"] is System.DBNull ? "" : Convert.ToDateTime(row["TRADE_DATE"]).ToString("yyyy/MM/dd"));
            dataRow["INVOICE_NO"] = row["INVOICE_NO"];
            dataRow["MACHINE_ID"] = row["MACHINE_ID"];
            dataRow["INVOICE_DATE"] = (row["INVOICE_DATE"] is System.DBNull ? "" : Convert.ToDateTime(row["INVOICE_DATE"]).ToString("yyyy/MM/dd"));
            dataRow["SALE_NO"] = row["SALE_NO"];
            dataRow["PRODNO"] = row["PRODNO"];
            dataRow["PRODNAME"] = row["PRODNAME"];
            dataRow["BEFORE_TAX"] = row["BEFORE_TAX"];
            dataRow["TAX"] = row["TAX"];
            dataRow["INVOICE_PRICE"] = row["INVOICE_PRICE"];
            dataRow["PAID_MODE"] = row["PAID_MODE"];
            dataRow["SALE_PERSON"] = row["SALE_PERSON"];
            dtOutput.Rows.Add(dataRow);

            //sumInvoicePriceByPaidMode(sumInvoicePrice, row);

            //插入群組小計列: 如果是最後一筆或不同 '交易狀態'(除了 '換貨作廢','退貨作廢') 就插入群組小計列
            //(((currentGroupFieldValue != "換貨作廢") && (currentGroupFieldValue != "退貨作廢") && (currentGroupFieldValue != "跨月退貨作廢")) && (currentGroupFieldValue != StringUtil.CStr(dt.Rows[i + 1]["SALE_STATUS"])))
            if (i == (dt.Rows.Count - 1))
            {
                 DataRow groupRow = dtOutput.NewRow();
                //List<string> groupSummaryText = new List<string>();
                //foreach (var item in sumInvoicePrice)
                //{
                //    groupSummaryText.Add(item.Key + item.Value);
                //}
                groupRow["SALE_STATUS"] = "總計：";
                groupRow["PAID_MODE"] = row["PAID_AMOUNT"];
                dtOutput.Rows.Add(groupRow);
                //sumInvoicePrice = new Dictionary<string, decimal>();
            }
            else
            {
                if ((StringUtil.CStr(dt.Rows[i + 1]["SALE_STATUS"]) != StringUtil.CStr(row["SALE_STATUS"])) ||
                    (StringUtil.CStr(dt.Rows[i + 1]["SALE_NO"]) != StringUtil.CStr(row["SALE_NO"])))
                {
                     DataRow groupRow = dtOutput.NewRow();
                    //List<string> groupSummaryText = new List<string>();
                    //foreach (var item in sumInvoicePrice)
                    //{
                    //    groupSummaryText.Add(item.Key + item.Value);
                    //}
                    groupRow["SALE_STATUS"] = "總計：";
                    groupRow["PAID_MODE"] = row["PAID_AMOUNT"];
                    dtOutput.Rows.Add(groupRow);
                    //sumInvoicePrice = new Dictionary<string, decimal>();
                }
            }


            //    ||
            //    (StringUtil.CStr(dt.Rows[i+1]["SALE_STATUS"]) != StringUtil.CStr(row["SALE_STATUS"]) && currentGroupFieldValue != "") ||
            //    (StringUtil.CStr(dt.Rows[i+1]["SALE_NO"]) != StringUtil.CStr(row["SALE_NO"]) && currentGroupSaleNoValue != ""))
            //{

            //}
            //currentGroupFieldValue = StringUtil.CStr(row["SALE_STATUS"]);
            //currentGroupSaleNoValue = StringUtil.CStr(row["SALE_NO"]);
        }
        return dtOutput;
    }

    /// <summary>
    /// 分組小計: 依不同付款方式個別小計
    /// </summary>
    private static void sumInvoicePriceByPaidMode(Dictionary<string, decimal> sumInvoicePrice, DataRow row)
    {
        decimal invoicePrice; //row["SALE_STATUS"].ToString()
        if ((StringUtil.CStr(row["SALE_STATUS"]) == "換貨作廢") || (StringUtil.CStr(row["SALE_STATUS"]) == "退貨作廢") || (StringUtil.CStr(row["SALE_STATUS"]) == "跨月退貨作廢"))
        {
            //invoicePrice = (decimal)row["INVOICE_PRICE"] * -1;
            invoicePrice = (decimal)row["PAID_AMOUNT"];
        }
        else
        {
            invoicePrice = (decimal)row["PAID_AMOUNT"];
        }

        if (!sumInvoicePrice.ContainsKey(StringUtil.CStr(row["PAID_MODE"])))
        {
            sumInvoicePrice.Add(StringUtil.CStr(row["PAID_MODE"]), invoicePrice);
        }
        else
        {
            sumInvoicePrice[StringUtil.CStr(row["PAID_MODE"])] += invoicePrice;
        }
    }

    private DataTable dtHeader()
    {
        DataTable dtHeader = new DataTable();
        dtHeader.Columns.Add("Text", typeof(string));
        dtHeader.Columns.Add("Align", typeof(string));
        dtHeader.Columns.Add("FontSize", typeof(string));
        dtHeader.Columns.Add("BackColor", typeof(string));

        DataRow NewRow = dtHeader.NewRow();
        NewRow["Text"] = "退換貨日期：" + txtSDate.Text + " ~ " + txtEDate.Text
            + "|發票號碼：" + txtInvoiceNo.Text
            + "|商品料號：" + pupProdNo.Text
            + "|交易序號：" + txtSALE_NO.Text
            + "|員工編號：" + pupEmployeeNo.Text;
        NewRow["Align"] = "LEFT";
        NewRow["FontSize"] = "11";
        NewRow["BackColor"] = "WHITE";
        dtHeader.Rows.Add(NewRow);

        NewRow = dtHeader.NewRow();
        NewRow["Text"] = "列印日期：" + System.DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss")
            + "|列印人員：" + logMsg.MODI_USER + " " + new Employee_Facade().GetEmpName(logMsg.MODI_USER)
            + "|頁　　次：1"
            + "|總 筆 數：" + StringUtil.CStr(totalCount);
        NewRow["Align"] = "LEFT";
        NewRow["FontSize"] = "11";
        NewRow["BackColor"] = "WHITE";
        dtHeader.Rows.Add(NewRow);
        return dtHeader;

    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        isSearch = true;
        BindMasterData();
        gvMaster.PageIndex = 0;
    }

    protected void btnExport_Click(object sender, EventArgs e)
    {
        BindMasterData();
        new Output().ExportXLS(1094, "", Resources.WebResources.RPL065, dtHeader(), this.ASPxGridViewExporter1, null, null);
        Response.Redirect(Utils.CreateTamperProofDownloadXlsURL("RPL065.xls"));
    }

    protected void gvMaster_PageIndexChanged(object sender, EventArgs e)
    {
        if (!isSearch)
        {
            BindMasterData();
        }
    }
    protected void gvMaster_DetailRowExpandedChanged(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewDetailRowEventArgs e)
    {
        BindMasterData();
    }

    protected void btnReset_Clicked(object sender, EventArgs e)
    {
        gvMaster.DataSource = null;
        gvMaster.DataBind();
        gvMaster.PageIndex = 0;
        txtSDate.Text = "";
        txtEDate.Text = "";
        txtInvoiceNo.Text = "";
        pupProdNo.Text = "";
        txtSALE_NO.Text = "";
        pupEmployeeNo.Text = "";
    }

    private static string getDataKeyValue(DataRow row, params string[] keyFieldNames)
    {
        //將每個鍵值加入 List
        List<string> keyValues = new List<string>();
        for (int i = 0; i < keyFieldNames.Length; i++)
        {
            keyValues.Add(StringUtil.CStr(row[keyFieldNames[i]]));
        }

        //將鍵值轉為逗號分隔的字串
        string dataKeyValue = string.Empty;
        if (keyFieldNames.Length > 0)
        {
            dataKeyValue = string.Join(",", keyValues.ToArray());
        }

        return dataKeyValue;
    }
}
