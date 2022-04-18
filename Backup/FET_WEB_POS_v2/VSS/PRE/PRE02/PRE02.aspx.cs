using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Advtek.Utility;
using DevExpress.Web;
using DevExpress.Web.ASPxGridView;
using DevExpress.Web.ASPxPager;
using DevExpress.Web.ASPxEditors;

using System.Collections.Specialized;

using FET.POS.Model.Common;
using FET.POS.Model.DTO;
using FET.POS.Model.Facade;
using FET.POS.Model.Facade.FacadeImpl;
using AdvTek.CustomControls;
using System.Web.Configuration;
public partial class VSS_PRE_PRE02 : BasePage
{
    OrderedDictionary QryArgs = new OrderedDictionary();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (logMsg.STORENO.ToUpper() != StringUtil.CStr(System.Web.Configuration.WebConfigurationManager.AppSettings["DefaultStore"]))
            pcSTORENO.Enabled = false;
        if (!IsPostBack)
        {
            setDefault();
            txtLogSTORE_NO.Text = logMsg.STORENO;
            //// 繫結空的資料表，以顯示表頭欄位
            //gvMaster.DataSource = GetEmptyDataTable();
            //gvMaster.DataBind();
            //this.gvMaster.Visible = true;
        }

        // 繫結預收款單號  Popup選單
        ASPxPopupControl ASPxPopupControl1 = pcPreNo.FindChildControl<ASPxPopupControl>("ASPxPopupControl1");
        ASPxPopupControl1.ContentUrl = "~/VSS/Common/OddNumberPopup.aspx?SysDate=Date()&KeyFieldValue1=PREPAY_HEAD&KeyFieldValue2=" + logMsg.STORENO;

    }

    void setDefault()
    {
        getSALE_PERSON();
        pcSTORENO.Text = logMsg.STORENO;

    }


    void getSALE_PERSON()
    {
        DataTable dtSalePerson = new PRE02_Facade().getSalePersonInPrepayHead(logMsg.STORENO);
        comSalePerson.DataSource = dtSalePerson;
        comSalePerson.ValueField = "EMPNO";
        comSalePerson.TextField = "EMP_SHOWNAME";
        comSalePerson.DataBind();
        comSalePerson.Items.Insert(0, new DevExpress.Web.ASPxEditors.ListEditItem("ALL", ""));
        DataRow[] drs = null;
        if (dtSalePerson != null && dtSalePerson.Rows.Count > 0)
            drs = dtSalePerson.Select(" EMPNO = '" + logMsg.OPERATOR + "'");
        if (drs == null || drs.Length == 0)
        {
            Employee_Facade empFacade = new Employee_Facade();
            string empName = empFacade.GetEmpName(logMsg.MODI_USER);
            comSalePerson.Items.Insert(1, new DevExpress.Web.ASPxEditors.ListEditItem(logMsg.MODI_USER, logMsg.MODI_USER + "-" + empName));
        }
        comSalePerson.SelectedIndex = comSalePerson.Items.IndexOfValue(logMsg.OPERATOR);
    }

    void setQueryParams()
    {
        //取得查詢條件值
        if (chkStoreQuery.Checked)
            QryArgs["STORE_NO"] = "";
        else
            QryArgs["STORE_NO"] = pcSTORENO.Text; //門市編號

        QryArgs["CUST_NAME"] = txtCustName.Text; //客戶姓名
        QryArgs["PREPAY_STATUS"] = comPreStatus.SelectedItem.Value;  //狀態
        QryArgs["PREPAY_NO"] = pcPreNo.Text; //預收款單號
        QryArgs["ID_NO"] = txtCustId.Text;//客戶身份證號
        QryArgs["SALE_PERSON"] = comSalePerson.SelectedItem.Value;//銷售人員

        QryArgs["TRADE_DATE_S"] = deTradeDate_S.Text;//預購日期-起
        QryArgs["TRADE_DATE_E"] = deTradeDate_E.Text;//預購日期-迄
        QryArgs["MSISDN"] = txtMsisdn.Text; //客戶門號
        QryArgs["UNI_NO"] = txtUniNo.Text; //統一編號
        QryArgs["CONTACT_PHONE"] = txtPhone.Text; //聯絡電話
        QryArgs["INVOICE_NO"] = txtInvoiceNo.Text; //發票號碼


    }

    protected void bindMasterData()
    {
        setQueryParams();
        DataTable dtResult = new PRE02_Facade().getPREPAY_HEAD(QryArgs);
        if (dtResult.Rows.Count > 0)
        {
            foreach (DataRow dr in dtResult.Rows)
            {
                dr["CUST_NAME"] = DataHide(StringUtil.CStr(dr["CUST_NAME"]), 1, 1);
            }
        }
        //if (dtResult.Rows.Count == 0)
        //{
        //    //判斷條件影響
        //    string result = new SAL02_Facade().checkData(QryArgs);
        //    if (!string.IsNullOrEmpty(result.Trim()))
        //    {
        //        ScriptManager.RegisterClientScriptBlock(this, typeof(string), "selectCheckData", string.Format(@"alert('{0}');", result), true);
        //    }
        //}

        Session["gvMaster"] = dtResult;
        gvMaster.DataSource = (DataTable)Session["gvMaster"];

        gvMaster.DataBind();
        gvMaster.PageIndex = 0;
        gvMaster.Selection.UnselectAll();
        gvMaster.FocusedRowIndex = -1;
    }

    protected void gvMaster_PageIndexChanged(object sender, EventArgs e)
    {
        gvMaster.DataSource = (DataTable)Session["gvMaster"];
        gvMaster.DataBind();
    }

    protected void btnQuery_Click(object sender, EventArgs e)
    {
        bindMasterData();
        this.gvMaster.Visible = true;
        //this.Button1.Visible = true;
    }


    public string DataHide(string strData, int intHide_S, int intHide_E)
    {
        string retData = "";
        string ReplaceTmpData = "";
        string tmpData = "";
        try
        {


            if (strData.Length > intHide_S && (strData.Length - intHide_S) > intHide_E)
            {
                ReplaceTmpData = strData.Substring(intHide_S, strData.Length - (intHide_S - 1) - intHide_E - 1);
                for (int i = 0; i <= ReplaceTmpData.Length; i++)
                    tmpData += "*";

                retData = strData.Remove(intHide_S, strData.Length - (intHide_S - 1) - intHide_E - 1);
                retData = retData.Insert(intHide_S, tmpData);
            }
            else
            {
                intHide_S = 1;
                ReplaceTmpData = strData.Substring(intHide_S, strData.Length - 1);
                for (int i = 0; i <= ReplaceTmpData.Length; i++)
                    tmpData += "*";

                retData = strData.Remove(intHide_S, strData.Length - 1);
                retData = retData.Insert(intHide_S, tmpData);
            }

        }
        catch (Exception)
        {


        }
        return retData;
    }
    protected void btnExport_Click(object sender, EventArgs e)
    {


        setQueryParams();
        DataTable dtResult = new PRE02_Facade().getPREPAY_HEAD(QryArgs);

        if (dtResult.Rows.Count > 0)
        {
            foreach (DataRow dr in dtResult.Rows)
            {
                dr["CUST_NAME"] = DataHide(StringUtil.CStr(dr["CUST_NAME"]), 1, 1);
            }


            //改爛位名稱
            dtResult.Columns["ITEMS"].ColumnName = "項次";
            dtResult.Columns.Remove("PREPAY_STATUS");
            dtResult.Columns["PREPAY_STATUS_NAME"].ColumnName = "狀態";
            dtResult.Columns["TRADE_DATE"].ColumnName = "預收款日期";
            dtResult.Columns["PREPAY_NO"].ColumnName = "預收款單號";
            dtResult.Columns["CUST_NAME"].ColumnName = "客戶姓名";
            dtResult.Columns["MSISDN"].ColumnName = "客戶門號";
            dtResult.Columns["CONTACT_PHONE"].ColumnName = "聯絡電話";
            dtResult.Columns["AR_AMOUNT"].ColumnName = "預收款金額";
            dtResult.Columns["STORE_NO"].ColumnName = "門市編號";
            dtResult.Columns["STORE_NAME"].ColumnName = "門市名稱";
            dtResult.Columns.Remove("SALE_PERSON");
            dtResult.Columns["SALE_PERSON_NAME"].ColumnName = "銷售人員";

            //DataTable dtheader = new DataTable();
            //dtheader.Columns.Add("header1", typeof(string));
            // dtheader.Columns.Add("header2", typeof(string));
            // dtheader.Columns.Add("header3", typeof(string));
            //DataRow NewRowHeader = dtheader.NewRow();
            //NewRowHeader["header1"] = "預收款查詢"  ;
            //NewRowHeader["header2"] = "";
            // NewRowHeader["header3"] = "報表產出時間： " + DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");
            //dtheader.Rows.Add(NewRowHeader);
            //"預收款查詢<td></td><td>報表產出時間時間：" + DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss") + "</td>"
            string filename = new Output().Print("XLS", "預收款查詢<td></td><td>報表產出時間時間：" + DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss") + "</td>", null, dtResult, null);
            Response.Redirect(Utils.CreateTamperProofDownloadXlsURL("PREPAY.xls"));
        }



    }
    //預收款資料
    protected void btnView_Click(object sender, EventArgs e)
    {
        if (gvMaster.FocusedRowIndex > -1)
        {
            string s = gvMaster.GetRowValues(gvMaster.FocusedRowIndex, "PREPAY_NO") as string;
            Response.Redirect("~/VSS/PRE/PRE01/PRE01.aspx?SRC_TYPE=PRE02&PREPAY_NO=" + s);
        }
        else
        {
            ScriptManager.RegisterClientScriptBlock(this, typeof(string), "selectSALE_NO", "alert('請選取預收款資料!');", true);
        }
    }
}
