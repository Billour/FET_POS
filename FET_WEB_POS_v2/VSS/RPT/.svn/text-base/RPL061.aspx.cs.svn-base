using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FET.POS.Model.Facade.FacadeImpl;
using System.Data;
using Advtek.Utility;

public partial class VSS_RPT_RPL061 : BasePage
{
    private bool isSearch = false;
    private int totalCount;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack && !Page.IsCallback)
        {
          
        }
        if (logMsg.STORENO != StringUtil.CStr(System.Web.Configuration.WebConfigurationManager.AppSettings["DefaultStore"]))
        {
            txtToStoreNO_S.Text = logMsg.STORENO;
            txtToStoreNO_E.Text = logMsg.STORENO;
            txtToStoreNO_S.Enabled = false;
            txtToStoreNO_E.Enabled = false;
        }
    }

    private void BindMasterData()
    {
        DataTable dt = new RPL_Facade().RPL061(
            txtSDate.Text, txtEDate.Text, txtToStoreNO_S.Text, txtToStoreNO_E.Text, txtProdNo_S.Text, txtProdNo_E.Text);
        totalCount = dt.Rows.Count;

        #region
        //DataTable dtOutput = new DataTable();
        //foreach (DataColumn column in dt.Columns)
        //{
        //    dtOutput.Columns.Add(column.ColumnName, column.DataType);
        //}

        //string keyValue1 = null;
        //string keyValue2 = null;
        //for (int i = 0; i < dt.Rows.Count; i++)
        //{
        //    DataRow row = dt.Rows[i];
        //    string currentKeyValue1 = getDataKeyValue(row, "STDATE");
        //    string currentKeyValue2 = getDataKeyValue(row, "STDATE", "STNO");

        //    DataRow newRow = dtOutput.NewRow();

        //    //key 不重覆才要填值
        //    if (keyValue1 != currentKeyValue1)
        //    {
        //        newRow["STDATE"] = row["STDATE"];
        //        keyValue1 = currentKeyValue1;
        //    }
        //    if (keyValue2 != currentKeyValue2)
        //    {
        //        newRow["STNO"] = row["STNO"];
        //        newRow["FROM_STORENAME"] = row["FROM_STORENAME"];
        //        newRow["TO_STORENAME"] = row["TO_STORENAME"];
        //        newRow["FROM_EMPNAME"] = row["FROM_EMPNAME"];
        //        newRow["TO_EMPNAME"] = row["TO_EMPNAME"];

        //        keyValue2 = currentKeyValue2;
        //    }
        //    //fill data
        //    newRow["PRODNO"] = row["PRODNO"];
        //    newRow["PRODNAME"] = row["PRODNAME"];
        //    newRow["TRANOUTQTY"] = row["TRANOUTQTY"];
        //    newRow["TRANINQTY"] = row["TRANINQTY"];
        //    newRow["IMEI"] = StringUtil.CStr(row["IMEI"] ).Replace(";", "\r\n");

        //    dtOutput.Rows.Add(newRow);
        //}
        //this.gvMaster.DataSource = dtOutput;
        //this.gvMaster.DataBind();
        #endregion
      
        this.gvMaster.DataSource = dt;
        this.gvMaster.DataBind();
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

    private DataTable dtHeader()
    {
        DataTable dtHeader = new DataTable();
        dtHeader.Columns.Add("Text", typeof(string));
        dtHeader.Columns.Add("Align", typeof(string));
        dtHeader.Columns.Add("FontSize", typeof(string));
        dtHeader.Columns.Add("BackColor", typeof(string));

        DataRow NewRow = dtHeader.NewRow();
        NewRow["Text"] = "調出日期：" + txtSDate.Text + " ~ " + txtEDate.Text
            + "|移入門市：" + txtToStoreNO_S.Text + " ~ " + txtToStoreNO_E.Text
            + "|商品料號：" + txtProdNo_S.Text + " ~ " + txtProdNo_E.Text;
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
        new Output().ExportXLS(800, "", Resources.WebResources.RPL061, dtHeader(), this.ASPxGridViewExporter1, null, null);
        Response.Redirect(Utils.CreateTamperProofDownloadXlsURL("RPL061.xls"));
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
        txtToStoreNO_S.Text = "";
        txtToStoreNO_E.Text = "";
        txtProdNo_S.Text = "";
        txtProdNo_E.Text = "";
    }
}
