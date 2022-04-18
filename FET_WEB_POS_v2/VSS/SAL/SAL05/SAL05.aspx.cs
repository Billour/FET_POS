using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Text;
using DevExpress.Web.ASPxGridView;
using System.Collections.Specialized;
using System.Data.OracleClient;
using FET.POS.Model.Facade.FacadeImpl;
using FET.POS.Model.DTO;
using DevExpress.Web.ASPxEditors;
using Advtek.Utility;

public partial class VSS_SAL_SAL05 : BasePage
{
    OrderedDictionary QryArgs = new OrderedDictionary();
    //string POSUUID_DETAIL = "";

    /// <summary>
    /// 是否由交易補登資料而來, 是："1", 否：""
    /// </summary>
    private string qS
    {
        get
        {
            string s = "";

            //**2011/04/21 Tina：前頁傳遞參數時，會先以加密處理，在此要解密。
            if (!string.IsNullOrEmpty(Request.QueryString["Param"]))
            {
                NameValueCollection qscoll = Utils.Param_UrlDecode(Request.QueryString["Param"]);
                foreach (string key in qscoll.AllKeys)
                {
                    if (key == "s")
                    {
                        s = string.Join(",", qscoll.GetValues(key));
                        break;
                    }
                }
            }

            return s;
        }
    }


    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack)
        {
            getSALE_PERSON();
            cbSALE_PERSON.Value = this.logMsg.OPERATOR;//操作人員
            //由其他系統來
            //POSUUID_DETAIL = Request.QueryString["POSUUID_DETAIL"] as string;
            //if (!string.IsNullOrEmpty(POSUUID_DETAIL))
            //{
            //    getMasterDataByUUID(POSUUID_DETAIL);
            //}
            if (qS == "1")
            {   //UAT_AL_B195, 2011-04-07 從交易補登進入時,交易取消按鈕設為Disable
                btnCancelTran.ClientEnabled = false;
            }

            if (!string.IsNullOrEmpty(this.logMsg.OPERATOR))
            {
                bindMasterData();
            }            
        }
        
      ClientScript.RegisterClientScriptBlock(this.GetType(), "GV_OPERATOR", "var GV_OPERATOR = '" + logMsg.OPERATOR + "';", true);

    }

    void getSALE_PERSON()
    {
        DataTable dtSalePerson = new SAL05_Facade().getEmployee(logMsg.STORENO, logMsg.OPERATOR);
        cbSALE_PERSON.DataSource = dtSalePerson;
        cbSALE_PERSON.ValueField = "EMPNO";
        cbSALE_PERSON.TextField = "EMPNAME";
        cbSALE_PERSON.DataBind();
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

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        gvMaster.FocusedRowIndex = -1;
        gvMaster.PageIndex = 0;
        bindMasterData();
        divContent.Visible = true;
    }

    protected void bindMasterData()
    {
        DataTable dtMaster = getMasterData();
     
        gvMaster.DataSource = dtMaster;
        gvMaster.DataBind();
    }

    private DataTable getMasterData()
    {
        setQueryParams();
        DataTable dtResult = new SAL05_Facade().getTO_CLOSE_HEAD(QryArgs);
        if (dtResult != null && dtResult.Rows.Count > 0)
        {
            DataRow[] drs = dtResult.Select(" SERVICE_TYPE = 4 ");
            if (drs != null && drs.Length > 0)
            {
                foreach (DataRow dr in drs)
                {
                    if (StringUtil.CStr(dr["FUN_ID"]) == "12")
                        dr["SERVICE_TYPENAME"] = "換卡";
                    else if (StringUtil.CStr(dr["FUN_ID"]) == "13")
                        dr["SERVICE_TYPENAME"] = "換號";
                    else if (StringUtil.CStr(dr["FUN_ID"]) == "150")
                        dr["SERVICE_TYPENAME"] = "一退一租";
                    else if (StringUtil.CStr(dr["FUN_ID"]) == "8")
                        dr["SERVICE_TYPENAME"] = "加值服務-資費高改低";
                    else if (StringUtil.CStr(dr["FUN_ID"]) == "11")
                        dr["SERVICE_TYPENAME"] = "暫時停機";
                    else if (StringUtil.CStr(dr["FUN_ID"]) == "180")
                        dr["SERVICE_TYPENAME"] = "保証金退現及查詢";
                    else if (StringUtil.CStr(dr["FUN_ID"]) == "1")
                        dr["SERVICE_TYPENAME"] = "合約資料-提前解約";
                    else if (StringUtil.CStr(dr["FUN_ID"]) == "121" || StringUtil.CStr(dr["FUN_ID"]) == "123")
                        dr["SERVICE_TYPENAME"] = "合約資料-變更促代";
                    else if (StringUtil.CStr(dr["FUN_ID"]) == "122" || StringUtil.CStr(dr["FUN_ID"]) == "124")
                        dr["SERVICE_TYPENAME"] = "合約資料-取消促代";
                    else if (StringUtil.CStr(dr["FUN_ID"]) == "25")
                        dr["SERVICE_TYPENAME"] = "全球卡換卡";
                    else if (StringUtil.CStr(dr["FUN_ID"]) == "2")
                        dr["SERVICE_TYPENAME"] = "Nonstop-換卡";
                    else if (StringUtil.CStr(dr["FUN_ID"]) == "505")
                        dr["SERVICE_TYPENAME"] = "Prepaid-換卡";
                }
                dtResult.AcceptChanges();
            }
        }
        return dtResult;
    }

    private void getMasterDataByUUID(string Detail_UUID)
    {
        DataTable dtResult = new SAL05_Facade().getTO_CLOSE_HEADByUUID(Detail_UUID, logMsg.STORENO);
        gvMaster.DataSource = dtResult;
        gvMaster.DataBind();
    }


    //private DataTable GetDetailData()
    //{
    //    OrderedDictionary dArg;
    //    return new SAL05_Facade().getTO_CLOSE_ITEM(dArg);
    //}

    protected void detailGrid_DataSelect(object sender, EventArgs e)
    {
        // Session["項次"] = (sender as ASPxGridView).GetMasterRowKeyValue();
    }

    protected void gvMaster_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
    {
        //GridPageSize = int.Parse(e.Parameters);
        gvMaster.SettingsPager.PageSize = int.Parse(e.Parameters);
        gvMaster.DataBind();
    }

    protected void gvMaster_HtmlRowPrepared(object sender, ASPxGridViewTableRowEventArgs e)
    {
        e.Row.Attributes["canSelect"] = "true";
        if (e.RowType == GridViewRowType.Data)
        {
            string STATUS = StringUtil.CStr(gvMaster.GetRowValues(e.VisibleIndex, "STATUS"));
            if (STATUS == "3" || STATUS == "2")
            {
                e.Row.Attributes["canSelect"] = "false";
            }
        }

        DevExpress.Web.ASPxEditors.ASPxButton btnDiscount = e.Row.FindChildControl<DevExpress.Web.ASPxEditors.ASPxButton>("btnDiscount");
        if (btnDiscount != null)
        {
            DataTable dt = new SAL05_Facade().getTO_CLOSE_DISCOUNT(StringUtil.CStr(e.KeyValue));
            if (dt.Rows.Count > 0)
                btnDiscount.Enabled = true;
            else
            {
                btnDiscount.Enabled = false;
                DevExpress.Web.ASPxPopupControl.ASPxPopupControl ASPxPopupControl1 = e.Row.FindChildControl<DevExpress.Web.ASPxPopupControl.ASPxPopupControl>("ASPxPopupControl1");
                ASPxPopupControl1.Enabled = false;
            }
        }
    }

    protected void gvMaster_HtmlRowCreated(object sender, ASPxGridViewTableRowEventArgs e)
    {
        if (e.RowType == GridViewRowType.Detail)
        {
            // 繫結明細資料表           
            ASPxGridView detailGrid = e.Row.FindChildControl<ASPxGridView>("detailGrid");

            detailGrid.DataSource = new SAL05_Facade().getTO_CLOSE_ITEM(StringUtil.CStr(e.KeyValue));
            detailGrid.DataBind();
        }
    }

    protected void gvMaster_PageIndexChanged(object sender, EventArgs e)
    {
        ASPxGridView grid = sender as ASPxGridView;
        grid.DataSource = getMasterData();
        grid.DataBind();
        //**2011/04/05 Tina：合併結帳要能跨頁選取。
        //grid.Selection.UnselectAll();
    }

    protected void gvMaster_SelectionChanged(object sender, EventArgs e)
    {
        if (gvMaster.Selection.Count > 0)
        {
            List<object> li = this.gvMaster.GetSelectedFieldValues(gvMaster.KeyFieldName);

            //  DevExpress.Web.Data.WebDescriptorRowBase wdsrb = gvMaster.Selection.

        }
   }

    protected void gvMaster_CommandButtonInitialize(object sender, ASPxGridViewCommandButtonEventArgs e)
    {
        string STATUS = StringUtil.CStr(gvMaster.GetRowValues(e.VisibleIndex, "STATUS"));
        if (STATUS == "3" || STATUS == "2") //交昜取消資料 和已結帳
        {
            if (e.ButtonType == ColumnCommandButtonType.SelectCheckbox) { e.Enabled = false; }
        }
    }

    protected void detailGrid_PageIndexChanged(object sender, EventArgs e)
    {
        //  ASPxGridView grid = sender as ASPxGridView;
        //  grid.DataSource = GetDetailData();
        //  grid.DataBind();
    }

    protected void combinedPaymentButton_Click(object sender, EventArgs e)
    {
        //鎖住合併結帳按鈕
        ((ASPxButton)sender).ClientEnabled = false;
        List<object> li = this.gvMaster.GetSelectedFieldValues(gvMaster.KeyFieldName);
        SAL05_Facade SAL05 = new SAL05_Facade();
        INVENTORY_Facade Inventory = new INVENTORY_Facade();
        string Stock = FET.POS.Model.Common.Common_PageHelper.GetGoodLOCUUID();
        string havePaymentBill = "";
        string haveEStore = "";
        string haveETC = "";
        string curServiceType = "";
        string POSUUID_DETAIL = "";

        if (li.Count > 0)
        {
            foreach (string key in li)
            {
                if (POSUUID_DETAIL.IndexOf(key) < 0) 
                {
                    POSUUID_DETAIL += key + ";";
                    DataTable dt = SAL05.getTO_CLOSE_ITEM(key);
                    DataTable dtHead = SAL05.getTO_CLOSE_HEADByUUID(key, logMsg.STORENO);
                    if (dtHead != null && dtHead.Rows.Count > 0)
                    {
                        try
                        {
                            curServiceType = StringUtil.CStr(dtHead.Rows[0]["SERVICE_TYPE"]);
                        }
                        catch
                        {

                        }
                        if (havePaymentBill == "")
                            if (curServiceType == "3")
                                havePaymentBill = "YES";
                            else
                                havePaymentBill = "NO";
                        else
                            if ((curServiceType == "3" && havePaymentBill == "NO")
                                || (curServiceType != "3" && havePaymentBill == "YES"))
                            {
                                ScriptManager.RegisterClientScriptBlock(this, typeof(string), "havePaymentBillData", "alert('帳單代收交易與銷售交易不允許合併結帳!');", true);
                                //恢復合併結帳按鈕
                                ((ASPxButton)sender).ClientEnabled = true;
                                return;
                            }
                        if (haveEStore == "")
                            if (curServiceType == "10")
                                haveEStore = "YES";
                            else
                                haveEStore = "NO";
                        else
                            if ((curServiceType == "10" && haveEStore == "NO")
                                || (curServiceType != "10" && haveEStore == "YES"))
                            {
                                ScriptManager.RegisterClientScriptBlock(this, typeof(string), "haveEStore", "alert('網購交易與銷售交易不允許合併結帳!');", true);
                                //恢復合併結帳按鈕
                                ((ASPxButton)sender).ClientEnabled = true;
                                return;
                            }
                    }

                    if (dt != null && dt.Rows.Count > 0)
                    {
                        foreach (DataRow dr in dt.Rows)
                        {
                            if (!string.IsNullOrEmpty(StringUtil.CStr(dr["AMOUNT"])))
                            {
                                try
                                {
                                    int.Parse(StringUtil.CStr(dr["AMOUNT"]));
                                }
                                catch
                                {
                                    ScriptManager.RegisterClientScriptBlock(this, typeof(string), "noPriceProductData", "alert('商品單價不存在,不允許結帳!');", true);
                                    //恢復合併結帳按鈕
                                    ((ASPxButton)sender).ClientEnabled = true;
                                    return;
                                }
                            }
                            else
                            {
                                ScriptManager.RegisterClientScriptBlock(this, typeof(string), "getProductPriceError", "alert('讀取商品單價資料失敗,不允許結帳!');", true);
                                //恢復合併結帳按鈕
                                ((ASPxButton)sender).ClientEnabled = true;
                                return;
                            }

                            try
                            {
                                if (StringUtil.CStr(dr["ISSTOCK"]) == "1")
                                {
                                    int qty = new SAL01_Facade().getINV_ON_HAND_CURRENT(StringUtil.CStr(dr["PRODNO"]), logMsg.STORENO);
                                    if (qty <= 0)
                                    {
                                        ScriptManager.RegisterClientScriptBlock(this, typeof(string), "noINVProductData", "alert('商品無庫存量，該筆交易不允許結帳!');", true);
                                        //恢復合併結帳按鈕
                                        ((ASPxButton)sender).ClientEnabled = true;
                                        return;
                                    }
                                }
                            }
                            catch
                            {
                                ScriptManager.RegisterClientScriptBlock(this, typeof(string), "getINVDataError", "alert('讀取庫存商品的資料失敗,不允許結帳!');", true);
                                //恢復合併結帳按鈕
                                ((ASPxButton)sender).ClientEnabled = true;
                                return;
                            }

                            string barcode1 = "";
                            string barcode2 = "";
                            string barcode3 = "";
                            if (dr["BARCODE1"] != null)
                                barcode1 = StringUtil.CStr(dr["BARCODE1"]);
                            if (dr["BARCODE2"] != null)
                                barcode2 = StringUtil.CStr(dr["BARCODE2"]);
                            if (dr["BARCODE3"] != null)
                                barcode3 = StringUtil.CStr(dr["BARCODE3"]);
                            if (barcode1 != "" && barcode1.Length > 3 && barcode1.Substring(barcode1.Length - 3) == "6F2"
                                && barcode2 != "" && barcode3 != "")
                            {
                                if (haveETC == "")
                                {
                                    if (havePaymentBill == "YES")
                                    {
                                        haveETC = "YES";
                                    }
                                }
                                else if (haveETC == "NO")
                                {
                                    ScriptManager.RegisterClientScriptBlock(this, typeof(string), "notAllowMerge", "alert('ETC帳單與其它交易不允許合併結帳!');", true);
                                    //恢復合併結帳按鈕
                                    ((ASPxButton)sender).ClientEnabled = true;
                                    return;
                                }
                            }
                            else
                            {
                                if (haveETC == "")
                                {
                                    if (havePaymentBill == "YES")
                                    {
                                        haveETC = "NO";
                                    }
                                }
                                else if (haveETC == "YES")
                                {
                                    ScriptManager.RegisterClientScriptBlock(this, typeof(string), "notAllowMerge", "alert('ETC帳單與其它交易不允許合併結帳!');", true);
                                    //恢復合併結帳按鈕
                                    ((ASPxButton)sender).ClientEnabled = true;
                                    return;
                                }
                            }
                        }
                    }
                }
            }

            //**2011/04/21 Tina：傳遞參數時，要先以加密處理。
            string encryptUrl = "";

            if (qS == "1") //由交易補登資料而來傳回交易補登
            {
                //Response.Redirect("~/VSS/SAL/SAL11/SAL11.aspx?s=1&SRC_TYPE=SAL05&PKEY=" + POSUUID_DETAIL);
                encryptUrl = Utils.Param_Encrypt("s=1&SRC_TYPE=SAL05&PKEY=" + POSUUID_DETAIL);
                Response.Redirect("~/VSS/SAL/SAL11/SAL11.aspx?Param=" + encryptUrl, true);
            }
            else
            {
                //Response.Redirect("~/VSS/SAL/SAL01/SAL01.aspx?SRC_TYPE=SAL05&PKEY=" + POSUUID_DETAIL);
                encryptUrl = Utils.Param_Encrypt("SRC_TYPE=SAL05&PKEY=" + POSUUID_DETAIL);
                Response.Redirect("~/VSS/SAL/SAL01/SAL01.aspx?Param=" + encryptUrl, true);

            }
        }
        else ScriptManager.RegisterClientScriptBlock(this, typeof(string), "selectTranData", "alert('請選取未結交易資料!');", true);
        //恢復合併結帳按鈕
        ((ASPxButton)sender).ClientEnabled = true;
    }

    void clearQueryParams()
    {
        txtS_Date.Text = "";
        txtE_Date.Text = "";
        txtMSISDN.Text = "";
        if (cbSALE_PERSON.Items.Count > 0)
            cbSALE_PERSON.SelectedIndex = 0;
        if (cbSERVICE_TYPE.Items.Count > 0)
            cbSERVICE_TYPE.SelectedIndex = 0;
        if (cbSTATUS.Items.Count > 0)
            cbSTATUS.SelectedIndex = 0;
        if (cbSALE_PERSON.SelectedItem != null)
        {
            QryArgs["SALE_PERSON"] = cbSALE_PERSON.SelectedItem.Value;
            cbSALE_PERSON.Value = logMsg.OPERATOR;
        }

    }

    void setQueryParams()
    {
        //取得查詢條件值
        try
        {
            QryArgs["STORE_NO"] = logMsg.STORENO;
            QryArgs["S_DATE"] = txtS_Date.Text;
            QryArgs["E_DATE"] = txtE_Date.Text;
            QryArgs["STATUS"] = cbSTATUS.SelectedItem.Value;
            QryArgs["SERVICE_TYPE"] = cbSERVICE_TYPE.SelectedItem.Value;
            QryArgs["MSISDN"] = txtMSISDN.Text;//客戶門號
            if (cbSALE_PERSON.SelectedItem != null)
                QryArgs["SALE_PERSON"] = cbSALE_PERSON.SelectedItem.Value;
        }
        catch //(Exception e) 
        { }
    }

    protected void btnClear_Click(object sender, EventArgs e)
    {
        clearQueryParams();
        setQueryParams();
    }

    protected void btnCancelTran_Click(object sender, EventArgs e)
    {
        List<object> li = this.gvMaster.GetSelectedFieldValues(gvMaster.KeyFieldName);
        //string POSUUID_DETAIL = "";
        SAL05_Facade Facade = new SAL05_Facade();
        SAL01_Facade Facade01 = new SAL01_Facade();
        if (li.Count > 0)
        {
            Facade.updateCancelTrancation(li, logMsg.MODI_USER);
            DataTable dt = Facade.getCancleTO_CLOSE_DATA(li);

            if (dt != null && dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    int ret = Facade01.CancelOuterSystem(StringUtil.CStr(dr["POSUUID_DETAIL"]), StringUtil.CStr(dr["SERVICE_TYPE"]),
                                                     StringUtil.CStr(dr["SERVICE_SYS_ID"]), StringUtil.CStr(dr["BUNDLE_ID"]),
                                                     StringUtil.CStr(dr["STORE_NO"]), StringUtil.CStr(dr["SALE_PERSON"]),
                                                     StringUtil.CStr(dr["BARCODE1"]), StringUtil.CStr(dr["BARCODE2"]),
                                                     StringUtil.CStr(dr["BARCODE3"]), StringUtil.CStr(dr["AMOUNT"]));
                    if (ret == 0)
                    {
                        //取消交易,commit外部系統成功才刪除未結清單中資料
                        StringBuilder posuuid_detailList = new StringBuilder("");
                        posuuid_detailList.Append(OracleDBUtil.SqlStr(StringUtil.CStr(dr["POSUUID_DETAIL"]))).Append(",");
                        Facade01.delTO_CLOSE(posuuid_detailList);
                    }
                    else
                    {
                        Facade01.InsertDataUploadLog(StringUtil.CStr(dr["POSUUID_DETAIL"]));
                    }
                }
            }
            ScriptManager.RegisterClientScriptBlock(this, typeof(string), "CancelTrancation", "alert('交易已取消!');", true);
            //update回原來的系統 目前未寫
            bindMasterData();
        }else ScriptManager.RegisterClientScriptBlock(this, typeof(string), "selectTranData", "alert('請選取未結交易資料!');", true);
    }

    public string TransferURL(string strValue)
    {
        //**2011/04/27 Tina：傳遞參數時，要先以加密處理。
        string encryptUrl = Utils.Param_Encrypt(strValue);
        return string.Format("Param={0}", encryptUrl);
    }

}
