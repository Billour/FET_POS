using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Text;
using DevExpress.Web.ASPxGridView;
using DevExpress.Web.ASPxEditors;
using FET.POS.Model.Facade.FacadeImpl;
using System.Web.UI.HtmlControls;
using DevExpress.Web.ASPxPopupControl;
using Advtek.Utility;
using System.Data.OracleClient;
using FET.POS.Model.Common;
using System.Collections.Specialized;
using System.Net;

public partial class VSS_SAL_SAL04 : BasePage
{
    [Serializable]
    class MTempTable
    {
        public int STORE_REC_TOTAL_AMOUNT = 0;//門市應收總金額   
        public int STORE_PAY_TOTAL_AMOUNT = 0;//門市應付總金額
        public int STORE_CHANGE_AMOUNT = 0;   //找零
        public DataTable Head,
                         Detail, //IMEI_LOG,
                         Discount,
                         Paid;
        #region 存取畫面上之資料
        public void SaveSale_Head_Temp(VSS_SAL_SAL04 p)
        {
            foreach (DataRow dr in Head.Rows)
            {
                dr["UNI_NO"] = p.lblUNI_NO_VALUE.Text;
                dr["UNI_TITLE"] = p.lblUNI_TITLE_VALUE.Text;
                dr["TRADE_DATE"] = p.lbTran_Date.Text;//交易日期
                dr["SALE_NO"] = p.lbSALE_NO.Text;//交易序號
                dr["HG_CARD_NO"] = p.lbHG_CARD_NO.Text; //Happy Go累點點數
                dr["HG_REMAIN_POINT"] = string.IsNullOrEmpty(p.lbHG_REMAIN_POINT.Text) ? "0" : p.lbHG_REMAIN_POINT.Text; //Happy Go剩餘點數
                dr["REMARK"] = p.lblREMARK_VALUE.Text;
            }
            Head.AcceptChanges();
        }
        public void SaleHeadDataBind(VSS_SAL_SAL04 p, string POSUUID_MASTER)
        {
            DataTable dt = p.Facade01.getSale_Head(POSUUID_MASTER);
            if (dt != null && dt.Rows.Count > 0)
            {
                Head = dt;
                DataRow dr = dt.Rows[0];
                p.lblCancelNo.Text = Advtek.Utility.SerialNo.GenNo("SC");
                p.lblCancelDate.Text = DateTime.Now.ToString("yyyy/MM/dd");
                p.lbTran_Date.Text = Convert.ToDateTime(dr["TRADE_DATE"]).ToString("yyyy/MM/dd");
                p.lbSALE_NO.Text = StringUtil.CStr(dr["SALE_NO"]);
                p.lbINVOICE_NO.Text = StringUtil.CStr(dr["INVOICE_NO"]);
                p.lblUNI_NO_VALUE.Text = StringUtil.CStr(dr["UNI_NO"]);
                p.lblUNI_TITLE_VALUE.Text = StringUtil.CStr(dr["UNI_TITLE"]);
                p.lblREMARK_VALUE.Text = StringUtil.CStr(dr["REMARK"]);
                p.lbHG_CARD_NO.Text = StringUtil.CStr(dr["HG_CARD_NO"]);
                p.lbHG_REMAIN_POINT.Text = StringUtil.CStr(dr["HG_REMAIN_POINT"]); //Happy Go剩餘點數
                p.lbTOTAL_AMOUNT.Text = StringUtil.CStr(dr["SALE_TOTAL_AMOUNT"]); //已收金額
                p.lbPayAmount.Text = p.lbTOTAL_AMOUNT.Text; //應退金額應等於已收金額

                //string SALE_TYPE_NAME = "";
                //switch (StringUtil.CStr(dr["SALE_TYPE"]).Trim())
                //{
                //    case "1": SALE_TYPE_NAME = "銷售交易"; break;
                //    case "2": SALE_TYPE_NAME = "帳單代收"; break;
                //    case "3": SALE_TYPE_NAME = "直接交易"; break;
                //}
                string SALE_STATUS_NAME = "";

                switch (StringUtil.CStr(dr["SALE_STATUS"]).Trim())
                {
                    case "1": SALE_STATUS_NAME = "未結帳"; break;
                    case "2": SALE_STATUS_NAME = "已結帳"; break;
                    case "3": SALE_STATUS_NAME = "退貨作廢"; break;
                    case "4": SALE_STATUS_NAME = "跨月退貨作廢"; break;
                    case "5": SALE_STATUS_NAME = "換貨作廢"; break;
                    case "6": SALE_STATUS_NAME = "跨月換貨作廢"; break;
                    case "7": SALE_STATUS_NAME = "換貨未結帳"; break;
                    case "8": SALE_STATUS_NAME = "交易補登未結帳"; break;
                    case "9": SALE_STATUS_NAME = "紙本授權未結帳"; break;
                }
                p.lbSALE_STATUS.Text = SALE_STATUS_NAME;
                if (StringUtil.CStr(dr["SALE_STATUS"]).Trim() == "2")
                    p.btnConfirmCancel.Enabled = true;
                else
                    p.btnConfirmCancel.Enabled = false;

                string VOUCHER_TYPE_NAME = "";
                DataTable dtInvoiceType = p.Facade01.getINVOICE_TYPE();

                switch (StringUtil.CStr(dr["VOUCHER_TYPE"]))
                {
                    case "1":
                    case "2":
                        VOUCHER_TYPE_NAME = "發票";
                        if (dtInvoiceType != null && dtInvoiceType.Rows.Count > 0)
                        {
                            VOUCHER_TYPE_NAME = StringUtil.CStr(dtInvoiceType.Rows[0]["INVOICE_TYPE_NAME"]);
                            DataRow[] drs = dtInvoiceType.Select(" INVOICE_TYPE_ID = '" + StringUtil.CStr(dr["INVOICE_TYPE"]).Replace("'", "-") + "' ");
                            if (drs != null && drs.Length > 0)
                                VOUCHER_TYPE_NAME = StringUtil.CStr(drs[0]["INVOICE_TYPE_NAME"]);
                        }
                        break;
                    case "3": VOUCHER_TYPE_NAME = "收據"; break;//只有貨品往來時才開收據
                }
                p.lbVOUCHER_TYPE.Text = VOUCHER_TYPE_NAME;
                Employee_Facade empFacade = new Employee_Facade();
                p.lbMODI_USER.Text = p.logMsg.MODI_USER + " " + empFacade.GetEmpName(p.logMsg.MODI_USER);
                p.lbMODI_DTM.Text = p.logMsg.MODI_DTM.ToString("yyyy/MM/dd HH:mm:ss");
            }
        }
        #endregion
    }
    string _SRC_TYPE
    {
        get
        {
            //string r = StringUtil.CStr(Request.QueryString["SRC_TYPE"]);

            string r = "";
            //**2011/04/21 Tina：前頁傳遞參數時，會先以加密處理，在此要解密。
            if (!string.IsNullOrEmpty(Request.QueryString["Param"]))
            {
                NameValueCollection qscoll = Utils.Param_UrlDecode(Request.QueryString["Param"]);
                foreach (string key in qscoll.AllKeys)
                {
                    if (key == "SRC_TYPE")
                    {
                        r = string.Join(",", qscoll.GetValues(key));
                        break;
                    }
                }
            }

            return r;
        }
    }
    string _PKEY
    {
        get
        {
            //string key = Request.QueryString["PKEY"] as string; //統一要給 POSUUID_MASTER;

            string r = "";
            //**2011/04/21 Tina：前頁傳遞參數時，會先以加密處理，在此要解密。
            if (!string.IsNullOrEmpty(Request.QueryString["Param"]))
            {
                NameValueCollection qscoll = Utils.Param_UrlDecode(Request.QueryString["Param"]);
                foreach (string key in qscoll.AllKeys)
                {
                    if (key == "PKEY")
                    {
                        r = string.Join(",", qscoll.GetValues(key));
                        break;
                    }
                }
            }

            return r;
        }
    }
    string _STOCK
    {
        get
        {
            return Common_PageHelper.GetGoodLOCUUID();
        }
    }
    MTempTable TempTables
    {
        get
        {
            MTempTable r = Session["TempTable"] as MTempTable;
            if (r == null)
            {
                r = new MTempTable();
                Session["TempTable"] = r;
            }
            return r;
        }
    }
    SAL03_Facade Facade03
    {
        get
        {
            return new SAL03_Facade();
        }

    }
    SAL01_Facade Facade01
    {
        get
        {
            return new SAL01_Facade();
        }

    }

    string __EVENTARGUMENT
    {
        get
        {
            string r = hdPaidInfo.Text; //Request["__EVENTARGUMENT"] as string;
            return r == null ? "" : r;
        }
    }

    #region 資料載入處理
    /// <summary>
    /// 從查詢頁面 or 從SSI載入舊交易及新交易資料
    /// </summary>
    void LoadData1()
    {
        LoadInvalData(_PKEY); //載入需作廢資料

        //string ETCProdNo = new SAL01_Facade().getFETCProuductNo();
        //DataRow[] drs = TempTables.Detail.Select(" PRODNO = '" + ETCProdNo + "'");
        //if (drs != null && drs.Length > 0)
        //{
        //    btnConfirmCancel.Enabled = false;
        //    ScriptManager.RegisterClientScriptBlock(this, typeof(string), "ETCMessage", "alert('ETC儲值-e通卡交易不得作廢!');", true);
        //}
        //else
        //{
        //    DataRow[] drsOuter = TempTables.Detail.Select(" SOURCE_TYPE <> '11' ");
        //    if (drsOuter != null && drsOuter.Length > 0 && this._SRC_TYPE != "CT")
        //    {
        //        btnConfirmCancel.Enabled = false;
        //        ScriptManager.RegisterClientScriptBlock(this, typeof(string), "OUTERSystemMessage", "alert('外部系統銷售交易，如果不是透過SSI呼叫不得作廢!');", true);
        //    }
        //    else
        //    {
        gvMaster.DataSource = TempTables.Detail;
        gvMaster.DataBind();
        if (TempTables.Discount != null && TempTables.Discount.Rows.Count > 0)
        {
            gvDetail.Visible = true;
            gvDetail.DataSource = TempTables.Discount;
            gvDetail.DataBind();
        }
        gvCheckOut.DataSource = TempTables.Paid;
        gvCheckOut.DataBind();
        Session["TempTable"] = TempTables;
        //SALE_IMEI資料動態才給
        TempTables.SaleHeadDataBind(this, _PKEY); //顯示表頭資訊
        btnConfirmCancel.Enabled = true;
        //    }
        //}
    }

    /// <summary>
    /// 載入需作廢資料
    /// <param name="POSUUID_MASTER">POSUUID_MASTER</param>
    /// </summary>
    void LoadInvalData(string POSUUID_MASTER)
    {
        TempTables.Detail = Facade03.getSale_Detail(POSUUID_MASTER, "1,2,3,4,6,7,8,9,10,13,14", logMsg.STORENO, this._STOCK);  //舊交易的明細
        TempTables.Discount = Facade03.getSale_Detail(POSUUID_MASTER, "5,11,12", logMsg.STORENO, this._STOCK);  //舊交易的明細
        TempTables.Paid = Facade03.getPaid_Detail(POSUUID_MASTER);                 //舊交易的付費資訊
        DataTable dt = Facade03.getSale_Head(POSUUID_MASTER);
    }
    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack && !IsCallback)
        {
            LoadData1();
            PrintName.Value = System.Configuration.ConfigurationManager.AppSettings["Credit_PDFPrinterName"].ToString();//web.config中設定
            //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "PrintInvoice", " Call_DetectPrinterName();", true);
            ScriptManager.RegisterStartupScript(this, this.GetType(), "PrintInvoice", " Call_DetectPrinterName();", true);
        }
    }

    protected void btnConfirmCancel_Click(object sender, EventArgs e)
    {
        LoadInvalData(_PKEY); //載入需作廢資料
        string ETCProdNo = new SAL041_Facade().getAllFETCProuductNo();
        DataRow[] drs = TempTables.Detail.Select(" PRODNO in (" + ETCProdNo + ")");
        bool nonETCPayment = false;
        if (drs != null && drs.Length > 0)
        {
            ScriptManager.RegisterClientScriptBlock(this, typeof(string), "ETCMessage", "alert('ETC儲值-e通卡交易不得作廢!');", true);
            return;
        }
        else
        {
            //SOURCE_TYPE 2 代收交易
            DataRow[] drsOtherBill = TempTables.Detail.Select(" SOURCE_TYPE = '3' ");
            if (drsOtherBill != null && drsOtherBill.Length > 0)
            {
                foreach (DataRow dr in drsOtherBill)
                {
                    if ((StringUtil.CStr(dr["barcode1"]).Length == 10 || StringUtil.CStr(dr["barcode1"]).Length == 14) &&
                        StringUtil.CStr(dr["barcode2"]) == "" && StringUtil.CStr(dr["barcode3"]) == "")
                    {   //FET,KGT bill
                        nonETCPayment = true;
                        if (DateUtil.DateDiff("M", StringUtil.CStr(TempTables.Head.Rows[0]["TRADE_DATE"]), StringUtil.CStr(DateTime.Today)) > 1)
                        {
                            ScriptManager.RegisterClientScriptBlock(this, typeof(string), "OtherBillMessage", "alert('FET帳單及KGT帳單代收交易，超過一個月不得作廢!');", true);
                            return;
                        }
                    }
                    else if (StringUtil.CStr(dr["barcode1"]) != "" && StringUtil.CStr(dr["barcode2"]) != "" && StringUtil.CStr(dr["barcode3"]) != "")
                    {
                        nonETCPayment = true;
                        if (StringUtil.CStr(TempTables.Head.Rows[0]["TRADE_DATE"]) != "" &&
                            DateUtil.IsDate(StringUtil.CStr(TempTables.Head.Rows[0]["TRADE_DATE"])))
                        {
                            if (DateTime.Parse(StringUtil.CStr(TempTables.Head.Rows[0]["TRADE_DATE"])).Date != DateTime.Today)
                            {
                                ScriptManager.RegisterClientScriptBlock(this, typeof(string), "OtherBillMessage", "alert('非FET帳單及KGT帳單代收交易，只能作廢當日代收交易!');", true);
                                return;
                            }
                        }
                        else
                        {
                            ScriptManager.RegisterClientScriptBlock(this, typeof(string), "OtherBillMessage", "alert('非FET帳單及KGT帳單代收交易，只能作廢當日代收交易!');", true);
                            return;
                        }
                    }
                }
            }

            //SOURCE_TYPE 11 單品, 6 HRS
            DataRow[] drsOuter = TempTables.Detail.Select(" SOURCE_TYPE <> '11' AND SOURCE_TYPE <> '6' ");
            if (drsOuter != null && drsOuter.Length > 0 && this._SRC_TYPE != "CT" && !nonETCPayment)
            {
                ScriptManager.RegisterClientScriptBlock(this, typeof(string), "OUTERSystemMessage", "alert('外部系統銷售交易，如果不是透過SSI呼叫不得作廢!');", true);
                return;
            }
            else
            {
                if (TempTables.Head.Rows[0]["SALE_STATUS"] != null && StringUtil.CStr(TempTables.Head.Rows[0]["SALE_STATUS"]) != "2")
                {
                    ScriptManager.RegisterClientScriptBlock(this, typeof(string), "ErrorStatusMessage", "alert('已作廢交易不得再次作廢!');", true);
                    return;
                }
            }
        }

        DataTable DT = new SAL041_Facade().GetINVOICE(this._PKEY);
        if (DT.Rows.Count > 0)
        {
            DateTime transDate = DateTime.Now;
            int S_MM = 0;   //期數的起
            int E_MM = 0;   //期數的迄
            transDate = DateTime.Parse(StringUtil.CStr(DT.Rows[0]["INVOICE_DATE"]).Trim());
            if (transDate.Year != DateTime.Now.Year)
            {
                Save1(sender, e);
            }
            else
            {
                //判斷目前月數是單雙
                if (DateTime.Now.Month % 2 == 0)
                {
                    S_MM = DateTime.Now.Month - 1;
                    E_MM = DateTime.Now.Month;
                }
                else
                {
                    S_MM = DateTime.Now.Month;
                    E_MM = DateTime.Now.Month + 1;
                }
                //判斷是否跨期
                if (transDate.Month >= S_MM && transDate.Month <= E_MM)
                {
                    //無跨期
                    //若有帶發票則是作廢發票反則為折讓
                    string Rtn = "是否有帶發票？";
                    string javascript = "";
                    /* 原程式 bill 因使用者要 顯示 "是" "否"兩個按鈕而修改
                    javascript += "if (confirm('" + Rtn + "')) {";
                    javascript += " Discount.SendPostBack('Click');";
                    javascript += " Invalid.SendPostBack('Click');";
                    javascript += "}else {Discount.SendPostBack('Click');}  ";
                     */
                    javascript += @"
                                        function mycallbackfunc(v,m,f){
	                                                if(v)
	                                                { Discount.SendPostBack('Click'); Invalid.SendPostBack('Click');
                                                    }
                                                    else
                                                    { Discount.SendPostBack('Click');
                                                    }
                                                } ";

                    javascript += @"  $.prompt(  ' " + Rtn + "' ";
                    javascript += @"  ,{   callback: mycallbackfunc 
                                                     ,buttons: { 是: true, 否: false }
                                                 }
                                              )
                                        ";
                    ScriptManager.RegisterStartupScript(this, typeof(string), "msg", javascript, true);
                    return;
                }
                else
                {
                    //有跨期開折讓單
                    Save1(sender, e);
                }
            }
        }
        else
        {
            if (new SAL041_Facade().IsExistINVOICE(this._PKEY))
            {
                ScriptManager.RegisterClientScriptBlock(this, typeof(string), "OUTERSystemMessage", "alert('發票已作廢不得再次作廢!');", true);
                return;
            }
            else
            {
                ((DevExpress.Web.ASPxEditors.ASPxButton)(sender)).ClientInstanceName = "Receipt";
                Save1(sender, e);
            }
        }
    }

    /// <summary>
    /// 作廢原銷售交易資料
    /// </summary>
    /// <returns></returns>
    protected void Save1(object sender, EventArgs e)
    {
        OracleConnection objConn = OracleDBUtil.GetConnection();
        OracleTransaction objTX = objConn.BeginTransaction();
        bool bCheckOut = true; //是否可作廢
        bool isDiscount = true; //是否為折讓 true 為折讓 false 為作廢
        bool isInv = true; //是否為發票 true 為發票 false 為收據
        bool needDrawBackCreditCard = false;
        bool needDrawBackHGCard = false;
        switch (((DevExpress.Web.ASPxEditors.ASPxButton)(sender)).ClientInstanceName)
        {
            case "Invalid":
                isDiscount = false;
                break;
            case "Receipt":
                isDiscount = false;
                isInv = false;
                break;
        }

        try
        {
            btnConfirmCancel.Enabled = false;
            Logger.Log.Info("銷售作廢作業開始，單號:" + StringUtil.CStr(TempTables.Head.Rows[0]["SALE_NO"]) + ",POSUUID_MASTER=" + this._PKEY);
            Logger.Log.Info("銷售作廢作業退庫存開始，單號:" + StringUtil.CStr(TempTables.Head.Rows[0]["SALE_NO"]) + ",POSUUID_MASTER=" + this._PKEY);

            DataRow[] drsCreditCard = TempTables.Paid.Select(" ITEM_STATUS = '作廢' And Paid_Mode in ('2','3','4') ");
            if (drsCreditCard != null && drsCreditCard.Length > 0)
                needDrawBackCreditCard = true;

            DataRow[] drsHGCard = TempTables.Detail.Select(" ITEM_STATUS = '作廢' And ITEM_TYPE in ('4','14') ");
            if (drsHGCard != null && drsHGCard.Length > 0)
                needDrawBackHGCard = true;

            INVENTORY_Facade Inventory = new INVENTORY_Facade();
            //作廢退回庫存
            DataRow[] Sale_DetailRows4 = TempTables.Detail.Select(" ITEM_TYPE IN ('1','2','3','7','8','9','10','13','14') ");
            if (bCheckOut && Sale_DetailRows4.Length > 0)
            {
                foreach (DataRow dr in Sale_DetailRows4)
                {
                    string Code = "";
                    string Message = "";
                    string Stock = FET.POS.Model.Common.Common_PageHelper.GetGoodLOCUUID();

                    if (!string.IsNullOrEmpty(StringUtil.CStr(dr["PRODNO"])) && StringUtil.CStr(dr["ISSTOCK"]) == "1")
                    {
                        try
                        {
                            Inventory.PK_INVENTORY_RETURN(objTX, "1", StringUtil.CStr(dr["PRODNO"]),
                               logMsg.STORENO, Stock, StringUtil.CStr(TempTables.Head.Rows[0]["SALE_NO"]),
                               Convert.ToInt32(StringUtil.CStr(dr["QUANTITY"])), logMsg.MODI_USER, StringUtil.CStr(dr["ID"]), ref Code, ref Message);
                            if (Code != "000")
                            {
                                bCheckOut = false;
                                ScriptManager.RegisterClientScriptBlock(this, typeof(string), "ERROR_MESSAGE", "alert('作廢貨品退庫存失敗!!" + Message + "');", true);
                                objTX.Rollback();
                            }
                        }
                        catch (Exception ex)
                        {
                            bCheckOut = false;
                            ScriptManager.RegisterClientScriptBlock(this, typeof(string), "ERROR_MESSAGE", "alert('作廢貨品退庫存失敗!!" + ex.Message.Replace("'", "-").Replace("\"", " ").Replace("\r", "").Replace("\n", "") + "');", true);
                            objTX.Rollback();
                        }
                    }
                }
            }

            //作廢發票/填入折讓單號
            if (bCheckOut)
            {
                int ret = 0;
                string creditNoteNo = "";
                if (isDiscount)
                    creditNoteNo = Store_SerialNo.GenNo("CN", logMsg.STORENO, logMsg.MACHINE_ID);
                ret = new SAL03_Facade().invalidOldInvoice1(objTX, this._PKEY, !isDiscount, creditNoteNo);
                if (ret < 0)
                {
                    bCheckOut = false;

                    ScriptManager.RegisterClientScriptBlock(this, typeof(string), "ERROR_MESSAGE", "alert('作廢發票失敗!!');", true);
                    objTX.Rollback();
                }
            }

            Logger.Log.Info("銷售作廢作業作廢原交易開始，單號:" + StringUtil.CStr(TempTables.Head.Rows[0]["SALE_NO"]) + ",POSUUID_MASTER=" + this._PKEY);
            //作廢原交易
            if (bCheckOut)
            {
                DateTime oldInvoiceDate = DateTime.Now.AddDays(-1);
                if (TempTables.Head != null && TempTables.Head.Rows.Count > 0 && TempTables.Head.Rows[0]["INVOICE_DATE"] != null)
                {
                    try
                    {
                        oldInvoiceDate = DateTime.Parse(StringUtil.CStr(TempTables.Head.Rows[0]["INVOICE_DATE"]).Trim());
                    }
                    catch (Exception)
                    {

                    }
                }
                int AffectRow = 0;
                //AffectRow = new SAL041_Facade().invalidOldTransaction1(objTX, this._PKEY, !isDiscount, lblCancelNo.Text);
                AffectRow = new SAL041_Facade().invalidOldTransactionPeriod(objTX, this._PKEY, oldInvoiceDate, lblCancelNo.Text, logMsg.MODI_USER);
                if (AffectRow < 1)
                {
                    bCheckOut = false;
                    ScriptManager.RegisterClientScriptBlock(this, typeof(string), "ERROR_MESSAGE", "alert('作廢原交易失敗!!');", true);
                    objTX.Rollback();
                }
            }
            Logger.Log.Info("銷售作廢作業作廢原交易結束，單號:" + StringUtil.CStr(TempTables.Head.Rows[0]["SALE_NO"]) + ",POSUUID_MASTER=" + this._PKEY);

            //IMEI_Log
            if (bCheckOut)
            {
                string strMessage = new SAL041_Facade().IMEIRETURN_Log(objTX, this._PKEY, logMsg.MODI_USER);
                string[] strMsg = strMessage.Split('|');
                if (strMsg[0] != "000") //表示失敗
                {
                    bCheckOut = false;
                    ScriptManager.RegisterClientScriptBlock(this, typeof(string), "ERROR_MESSAGE", @"alert('IMEI_LOG失敗, " + strMsg[1].Replace("'", "-").Replace("\"", " ") + "');", true);
                    objTX.Rollback();
                }
            }

            Logger.Log.Info("銷售作廢作業作廢原交易結束，單號:" + StringUtil.CStr(TempTables.Head.Rows[0]["SALE_NO"]) + ",POSUUID_MASTER=" + this._PKEY);

            if (bCheckOut)  //所有動作都有正常執行才可commit
            {
                objTX.Commit();
            }

            Logger.Log.Info("銷售作廢作業更新交易狀態完成，單號:" + StringUtil.CStr(TempTables.Head.Rows[0]["SALE_NO"]) + ",POSUUID_MASTER=" + this._PKEY);
        }
        catch (Exception ex)
        {
            bCheckOut = false;
            Logger.Log.Info("銷售作廢作業失敗，單號:" + StringUtil.CStr(TempTables.Head.Rows[0]["SALE_NO"]) + ",POSUUID_MASTER=" + this._PKEY + ",原因:" + ex.Message);
            ScriptManager.RegisterClientScriptBlock(this, typeof(string), "ERROR_MESSAGE", "alert('" + ex.Message.Replace("'", "-").Replace("\"", " ") + "');", true);
            objTX.Rollback();
            return;
        }
        finally
        {

        }

        bool OrderCancel = true;
        SAL01_Facade Facade = new SAL01_Facade();
        try
        {
            if (bCheckOut)
            {
                if (this._PKEY != null && (!string.IsNullOrEmpty(this._PKEY)))
                {
                    if (OrderCancel)
                    {
                        //將TO_CLOSE_HEAD 更新為 取消中狀態, 並回填關連的POSUUID_MASTER
                        Facade.UpdateUnCloseHead(TempTables.Head, "3", logMsg.MODI_USER);
                        //取得外部系統SALE_DETAIL
                        DataTable dtDetail = Facade.getSale_Detail(this._PKEY, "2");
                        DataRow[] drsOuter = null;
                        if (dtDetail != null && dtDetail.Rows.Count > 0)
                        {   //SOURCE_TYPE 11 單品, 6 HRS
                            drsOuter = dtDetail.Select(" SOURCE_TYPE <> '11' AND SOURCE_TYPE <> '6' ");
                        }
                        if (drsOuter != null && drsOuter.Length > 0)
                        {
                            string prePosuuidDetail = "";
                            string posuuidDetail = "";
                            string sysID = "SSI";
                            foreach (DataRow dr in drsOuter)
                            {
                                //抓出SSI的SERVICE_SYS_ID by siwen
                                string service_sys_id = StringUtil.CStr(dr["SERVICE_SYS_ID"]);
                                string sqlstr = "select service_sys_id from to_close_head where posuuid_detail = " + OracleDBUtil.SqlStr(StringUtil.CStr(dr["POSUUID_DETAIL"]));
                                DataTable sdt = OracleDBUtil.Query_Data(sqlstr);
                                if (sdt.Rows.Count > 0) service_sys_id = StringUtil.CStr(sdt.Rows[0][0]);

                                if (dr["POSUUID_DETAIL"] != null && (!string.IsNullOrEmpty(StringUtil.CStr(dr["POSUUID_DETAIL"]))))
                                    posuuidDetail = StringUtil.CStr(dr["POSUUID_DETAIL"]);

                                if (posuuidDetail != "" && posuuidDetail != prePosuuidDetail)
                                {
                                    prePosuuidDetail = posuuidDetail;
                                    if (dr["SERVICE_SYS_ID"] != null && (!string.IsNullOrEmpty(service_sys_id))
                                        && dr["POSUUID_DETAIL"] != null && (!string.IsNullOrEmpty(StringUtil.CStr(dr["POSUUID_DETAIL"]))))
                                    {
                                        Logger.Log.Info("銷售作廢作業呼叫外部SSI系統開始，單號:" + StringUtil.CStr(TempTables.Head.Rows[0]["SALE_NO"]) + ",POSUUID_MASTER=" + this._PKEY + ",POSUUID_DETAIL=" + posuuidDetail);
                                        Facade.CommitOuterSystem(sysID, service_sys_id,
                                                                     StringUtil.CStr(this.TempTables.Head.Rows[0]["POSUUID_MASTER"]), posuuidDetail,
                                                                     StringUtil.CStr(dr["BARCODE1"]), logMsg.OPERATOR, logMsg.STORENO, StringUtil.CStr(dr["BUNDLE_ID"]),
                                                                     StringUtil.CStr(dr["BARCODE2"]), StringUtil.CStr(dr["BARCODE3"]));
                                        Logger.Log.Info("銷售作廢作業呼叫外部SSI系統完成，單號:" + StringUtil.CStr(TempTables.Head.Rows[0]["SALE_NO"]) + ",POSUUID_MASTER=" + this._PKEY + ",POSUUID_DETAIL=" + posuuidDetail);
                                        //刪除未結清單
                                        Facade.delTO_CLOSE(this._PKEY);
                                    } //end of if POSUUID_DETAIL is not null and space block
                                } //end of if posuuidDetail is not space and posuuidDetail != prePosuuidDetail block
                            } //end of foreach datarow loop
                        } // end of if dtDetail had record block 
                    } // end of if OrderCancel block 
                } // end of if this._PKEY is not null and space block
            } // end of if bCheckOut
        }
        catch (Exception ex)
        {
            Logger.Log.Info("交易作廢成功，但呼叫外部SSI作業失敗，單號:" + StringUtil.CStr(TempTables.Head.Rows[0]["SALE_NO"]) + ",POSUUID_MASTER=" + this._PKEY + ",原因:" + ex.Message);
            ScriptManager.RegisterClientScriptBlock(this, typeof(string), "OrderCancelError", "alert('交易作廢失敗!');", true);
            return;
        }
        finally
        {
            if (bCheckOut)
                btnConfirmCancel.Enabled = false;
            else
                btnConfirmCancel.Enabled = true;
        }

        try
        {
            if (OrderCancel)
            {
                string SALE_NO = this.lbSALE_NO.Text;
                if (SALE_NO == "")
                    SALE_NO = Store_SerialNo.GenNo("SALE", logMsg.STORENO, logMsg.MACHINE_ID);
                if (isDiscount)
                {
                    Logger.Log.Info("銷售作廢作業列印折讓單開始，單號:" + StringUtil.CStr(TempTables.Head.Rows[0]["SALE_NO"]) + ",POSUUID_MASTER=" + this._PKEY);
                    Receipt myReceipt = new Receipt();

                    string filePath = Facade.getUploadPath(this._PKEY);
                    Logger.Log.Info("銷售作廢作業取得折讓單路徑，單號:" + StringUtil.CStr(TempTables.Head.Rows[0]["SALE_NO"]) + ",POSUUID_MASTER=" + this._PKEY);
                    //  filePath += "/" + DateTime.Now.ToString("yyyyMMdd")+"/"+logMsg.STORENO;
                    if (!string.IsNullOrEmpty(filePath))
                    {

                        string fileName = "";
                        fileName = myReceipt.getnerateDebitNote(this._PKEY, PrintName.Value);
                        Logger.Log.Info("銷售作廢作業產生折讓單完成，單號:" + StringUtil.CStr(TempTables.Head.Rows[0]["SALE_NO"]) + ",POSUUID_MASTER=" + this._PKEY);
                        if (fileName == null || string.IsNullOrEmpty(fileName))
                        {
                            if (needDrawBackCreditCard && needDrawBackHGCard)
                                ScriptManager.RegisterClientScriptBlock(this, typeof(string), "PrintInvoiceError", "alert('銷售作廢完成!');alert('請記得刷退信用卡金額!');alert('請記得刷退Happy Go已兌換點數!');alert('列印折讓單失敗，請重印折讓單!!');", true);
                            else if (needDrawBackCreditCard)
                                ScriptManager.RegisterClientScriptBlock(this, typeof(string), "PrintInvoiceError", "alert('銷售作廢完成!');alert('請記得刷退信用卡金額!');alert('列印折讓單失敗，請重印折讓單!!');", true);
                            else if (needDrawBackHGCard)
                                ScriptManager.RegisterClientScriptBlock(this, typeof(string), "PrintInvoiceError", "alert('銷售作廢完成!');alert('請記得刷退Happy Go已兌換點數!');alert('列印折讓單失敗，請重印折讓單!!');", true);
                            else
                                ScriptManager.RegisterClientScriptBlock(this, typeof(string), "PrintInvoiceError", "alert('銷售作廢完成!');alert('列印折讓單失敗，請重印折讓單!!');", true);
                        }
                        else
                        {
                            if (needDrawBackCreditCard && needDrawBackHGCard)
                                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "PrintInvoice", "alert('銷售作廢完成，自動開立折讓單!');alert('請記得刷退信用卡金額!');alert('請記得刷退Happy Go已兌換點數!');document.getElementById('" +
                                                                        fDownload.ClientID + "').src='" + Request.ApplicationPath + filePath + "/" + fileName + "';", true);
                            else if (needDrawBackCreditCard)
                                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "PrintInvoice", "alert('銷售作廢完成，自動開立折讓單!');alert('請記得刷退信用卡金額!');document.getElementById('" +
                                                                            fDownload.ClientID + "').src='" + Request.ApplicationPath + filePath + "/" + fileName + "';", true);
                            else if (needDrawBackHGCard)
                                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "PrintInvoice", "alert('銷售作廢完成，自動開立折讓單!');alert('請記得刷退Happy Go已兌換點數!');document.getElementById('" +
                                                                        fDownload.ClientID + "').src='" + Request.ApplicationPath + filePath + "/" + fileName + "';", true);
                            else
                                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "PrintInvoice", "alert('銷售作廢完成，自動開立折讓單!');document.getElementById('" +
                                                                        fDownload.ClientID + "').src='" + Request.ApplicationPath + filePath + "/" + fileName + "';", true);
                        }
                    }
                    else
                    {
                        if (needDrawBackCreditCard && needDrawBackHGCard)
                            ScriptManager.RegisterClientScriptBlock(this, typeof(string), "PrintInvoiceError", "alert('銷售作廢完成!');alert('請記得刷退信用卡金額!');alert('請記得刷退Happy Go已兌換點數!');alert('列印折讓單失敗，請重印折讓單!!');", true);
                        else if (needDrawBackCreditCard)
                            ScriptManager.RegisterClientScriptBlock(this, typeof(string), "PrintInvoiceError", "alert('銷售作廢完成!');alert('請記得刷退信用卡金額!');alert('列印折讓單失敗，請重印折讓單!!');", true);
                        else if (needDrawBackHGCard)
                            ScriptManager.RegisterClientScriptBlock(this, typeof(string), "PrintInvoiceError", "alert('銷售作廢完成!');alert('請記得刷退Happy Go已兌換點數!');alert('列印折讓單失敗，請重印折讓單!!');", true);
                        else
                            ScriptManager.RegisterClientScriptBlock(this, typeof(string), "PrintInvoiceError", "alert('銷售作廢完成!');alert('列印折讓單失敗，請重印折讓單!!');", true);
                    }
                }
                else
                {
                    if (isInv && needDrawBackCreditCard && needDrawBackHGCard)
                        ScriptManager.RegisterClientScriptBlock(this, typeof(string), "OrderCancel", "alert('銷售作廢完成!原發票請收回');alert('請記得刷退信用卡金額!');alert('請記得刷退Happy Go已兌換點數!');window.location.href='SAL041.aspx';", true);
                    else if (isInv && needDrawBackCreditCard)
                        ScriptManager.RegisterClientScriptBlock(this, typeof(string), "OrderCancel", "alert('銷售作廢完成!原發票請收回');alert('請記得刷退信用卡金額!');window.location.href='SAL041.aspx';", true);
                    else if (isInv && needDrawBackHGCard)
                        ScriptManager.RegisterClientScriptBlock(this, typeof(string), "OrderCancel", "alert('銷售作廢完成!原發票請收回');alert('請記得刷退Happy Go已兌換點數!');window.location.href='SAL041.aspx';", true);
                    else if (needDrawBackCreditCard && needDrawBackHGCard)
                        ScriptManager.RegisterClientScriptBlock(this, typeof(string), "OrderCancel", "alert('銷售作廢完成!');alert('請記得刷退信用卡金額!');alert('請記得刷退Happy Go已兌換點數!');window.location.href='SAL041.aspx';", true);
                    else if (isInv)
                        ScriptManager.RegisterClientScriptBlock(this, typeof(string), "OrderCancel", "alert('銷售作廢完成!原發票請收回');window.location.href='SAL041.aspx';", true);
                    else if (needDrawBackCreditCard)
                        ScriptManager.RegisterClientScriptBlock(this, typeof(string), "OrderCancel", "alert('銷售作廢完成!');alert('請記得刷退信用卡金額!');window.location.href='SAL041.aspx';", true);
                    else if (needDrawBackHGCard)
                        ScriptManager.RegisterClientScriptBlock(this, typeof(string), "OrderCancel", "alert('銷售作廢完成!');alert('請記得刷退Happy Go已兌換點數!');window.location.href='SAL041.aspx';", true);
                    else
                        ScriptManager.RegisterClientScriptBlock(this, typeof(string), "OrderCancel", "alert('銷售作廢完成!');window.location.href='SAL041.aspx';", true);
                } // end of isDiscount
                collection(this._PKEY);
            }
            else
            {
                btnConfirmCancel.Enabled = true;
                ScriptManager.RegisterClientScriptBlock(this, typeof(string), "OrderCancelError", "alert('交易作廢失敗!');", true);
            } // end of OrderCancel
        }
        catch (Exception ex)
        {
            Logger.Log.Info("銷售作廢作業失敗，單號:" + StringUtil.CStr(TempTables.Head.Rows[0]["SALE_NO"]) + ",POSUUID_MASTER=" + this._PKEY + ",原因:" + ex.Message);
            ScriptManager.RegisterClientScriptBlock(this, typeof(string), "OrderCancelError", "alert('銷售作廢成功，產生折讓單失敗!');", true);
            return;
        }
        finally
        {
            if (bCheckOut)
                btnConfirmCancel.Enabled = false;
            else
                btnConfirmCancel.Enabled = true;
        }
    }


    /// <summary>
    /// 代收處理
    /// </summary>
    /// <param name="POSUUID_MASTER"></param>
    private void collection(string POSUUID_MASTER)
    {
        OracleConnection conn = null;

        string bill_dispatch_url = "";
        string postString = "";//XML結構                                                                                                                                                                                                                           
        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        sb.Append(" select barcode1,fun_id from SALE_DETAIL where  source_type = 3 and posuuid_master =" + OracleDBUtil.SqlStr(POSUUID_MASTER));
        DataTable dt = OracleDBUtil.Query_Data(StringUtil.CStr(sb));
        try
        {
            conn = OracleDBUtil.GetConnection();

            //1: 遠傳帳單                                                                                                                                                                                                                                          
            //2: 和信帳單                                                                                                                                                                                                                                          
            //3: Seednet帳單                                                                                                                                                                                                                                       
            //4: 遠通帳單(有單)                                                                                                                                                                                                                                    
            //5: 遠通帳單(無單)                                                                                                                                                                                                                                    
            //6: 速博帳單                                                                                                                                                                                                                                          
            foreach (DataRow row in dt.Rows)
            {
                string fun_id = StringUtil.CStr(row["fun_id"]);
                //FET                                                                                                                                                                                                                                              
                #region 判斷代收類型

                switch (fun_id)
                {
                    case "1":

                        FET_BILLING_BACKOUTPAYMENTTRX fet = new FET_BILLING_BACKOUTPAYMENTTRX();
                        postString = fet.DOMain(POSUUID_MASTER);
                        bill_dispatch_url = get_bill_dispatch_url("FET") + "/posapp/InsPaymentTrx";
                        //bill_dispatch_url = "http://192.168.8.223/HRS_WS/Default2.aspx";   
                        break;
                }

                #endregion


                if (fun_id == "1")
                {
                    if (!string.IsNullOrEmpty(postString))
                    {
                        string status = "";
                        string message = "";
                        string send_flag = "";
                        string sqlstr = "";
                        OracleCommand cmd = new OracleCommand();
                        string[] strpost = postString.Split('|');
                        foreach (string text in strpost)
                        {
                            if (text != "")
                            {
                                Logger.Log.Info("FET XML:" + bill_dispatch_url + "?" + text);
                                string postString1 = "req=" + text;
                                byte[] postData = Encoding.Default.GetBytes(postString1);
                                HttpWebRequest req = (HttpWebRequest)HttpWebRequest.Create(bill_dispatch_url);
                                req.Method = "POST";
                                req.ContentType = "application/x-www-form-urlencoded";
                                req.ContentLength = postData.Length;
                                using (System.IO.Stream reqStream = req.GetRequestStream())
                                {
                                    reqStream.Write(postData, 0, postData.Length);
                                }

                                string responseString = "";
                                using (WebResponse wr = req.GetResponse())
                                {
                                    System.IO.Stream strm = wr.GetResponseStream();
                                    System.IO.StreamReader sr = new System.IO.StreamReader(strm);
                                    responseString = sr.ReadToEnd();
                                    Logger.Log.Info("FET Response XML:" + responseString);
                                }

                                if (!string.IsNullOrEmpty(responseString))
                                {
                                    System.IO.StringReader sr = new System.IO.StringReader(responseString);
                                    DataSet ds = new DataSet();
                                    ds.ReadXml(sr);

                                    DataTable xmldt = ds.Tables["fet-pos-pay-create-res"];
                                    foreach (DataRow xmldr in xmldt.Rows)
                                    {
                                        if (xmldr["Process-Status"].ToString() == "1" && send_flag != "X")
                                        {
                                            send_flag = "S";
                                        }
                                        else
                                        {
                                            message += xmldr["Error-Message"].ToString();
                                            send_flag = "X";
                                        }
                                    }
                                }
                            }
                        }
                        sqlstr = "update FET_BILLING_CANCLE_FILE set send_flag = :send_flag,FET_PROCESS_STATUS = :FET_PROCESS_STATUS,ERROR_MESSAGE=:ERROR_MESSAGE where pos_key in (select posuuid_detail from sale_detail where posuuid_master = :posuuid_master)";
                        cmd = new OracleCommand(sqlstr, conn);
                        cmd.Parameters.Add(":send_flag", OracleType.NVarChar).Value = send_flag;
                        cmd.Parameters.Add(":FET_PROCESS_STATUS", OracleType.NVarChar).Value = status;
                        cmd.Parameters.Add(":ERROR_MESSAGE", OracleType.NVarChar).Value = message;
                        cmd.Parameters.Add(":posuuid_master", OracleType.NVarChar).Value = POSUUID_MASTER;
                        cmd.ExecuteNonQuery();
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Logger.Log.Error("TSAL01 Collection Error : " + ex.Message, ex);
            throw ex;
        }
        finally
        {
            if (conn.State == ConnectionState.Open) conn.Close();
            OracleConnection.ClearPool(conn);
        }
    }

    /// <summary>
    /// 產生銷帳連結
    /// </summary>
    /// <param name="type"></param>
    /// <returns></returns>
    private string get_bill_dispatch_url(string type)
    {
        string result = "";
        OracleConnection conn = null;
        OracleCommand cmd = null;
        try
        {
            string key = string.Format("{0}_BILL_DISPATCH_URL", type);
            conn = OracleDBUtil.GetConnection();
            string sqlstr = "select PARA_VALUE from sys_para where para_key = " + OracleDBUtil.SqlStr(key);
            cmd = new OracleCommand(sqlstr, conn);
            result = cmd.ExecuteScalar() == null ? "" : StringUtil.CStr(cmd.ExecuteScalar());

        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            if (conn.State == ConnectionState.Open) conn.Close();
            OracleConnection.ClearPool(conn);
        }
        return result;
    }

    protected void gvMaster_HtmlRowCreated(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewTableRowEventArgs e)
    {
        if (e.RowType == GridViewRowType.Data)
        {
            ASPxTextBox lblIMEI = e.Row.FindChildControl<ASPxTextBox>("lbIMEI_QTY");
            ASPxTextBox txtProdName = e.Row.FindChildControl<ASPxTextBox>("txtPRODNAME");
            ASPxTextBox txtIMEI = e.Row.FindChildControl<ASPxTextBox>("txtIMEI");
            HtmlGenericControl divIMEI_QTY = e.Row.FindChildControl<HtmlGenericControl>("divIMEI_QTY");

            //取得IMEI數量
            string PO_OE_NO = StringUtil.CStr(gvMaster.GetRowValues(e.VisibleIndex, "ID"));
            string PRODNO = StringUtil.CStr(gvMaster.GetRowValues(e.VisibleIndex, "PRODNO"));
            lblIMEI.Text = StringUtil.CStr(getPROD_IMEI_COUNT("SALE_IMEI_LOG", PO_OE_NO, PRODNO));

            // 繫結明細資料表    
            string strIMEI = IMEIContent("SALE_IMEI_LOG", StringUtil.CStr(e.GetValue("ID")), StringUtil.CStr(e.GetValue("PRODNO")));
            divIMEI_QTY.Attributes["onmouseover"] = string.Format("show('{0}');", strIMEI);
            divIMEI_QTY.Attributes["onmouseout"] = "hide();";

            int intC_IMEI = int.Parse(StringUtil.CStr(e.GetValue("QUANTITY")) == "" ? "0" : StringUtil.CStr(e.GetValue("QUANTITY")));
            int intS_IMEI = int.Parse(StringUtil.CStr(e.GetValue("IMEI_QTY")) == "" ? "0" : lblIMEI.Text); //StringUtil.CStr(e.GetValue("IMEI_QTY"));

            //取得IMEI_Flag
            DataTable dtFlag = new Product_Facade().Query_ProductInfo(PRODNO);
            string IMEI_Flag = "";
            if (dtFlag.Rows.Count > 0)
            {
                IMEI_Flag = StringUtil.CStr(dtFlag.Rows[0]["IMEI_FLAG"]);
                txtProdName.Text = StringUtil.CStr(dtFlag.Rows[0]["PRODNAME"]);
            }

            if (intC_IMEI == 0 || IMEI_Flag == "1" || string.IsNullOrEmpty(IMEI_Flag))
            {
                HtmlGenericControl divIMEI = e.Row.FindChildControl<HtmlGenericControl>("divIMEI");
                divIMEI.Style.Add("visibility", "hidden");
            }

            if (strIMEI.IndexOf("<tr><td>") >= 0)
            {
                string[] stringSeparators = new string[] { "<tr><td>" };
                string[] result = strIMEI.Split(stringSeparators, System.StringSplitOptions.RemoveEmptyEntries);
                string[] stringSeparators2 = new string[] { "</td></tr>" };
                string[] result2 = result[1].Split(stringSeparators2, System.StringSplitOptions.RemoveEmptyEntries);

                txtIMEI.Text = result2[0];
            }
        }
    }

    protected void gvMaster_HtmlRowPrepared(object sender, ASPxGridViewTableRowEventArgs e)
    {
        e.Row.Attributes["canSelect"] = "true";
        if (e.RowType == GridViewRowType.Data)
        {
            PopupControl txtPRODNO = gvMaster.FindRowCellTemplateControl(e.VisibleIndex, (GridViewDataColumn)gvMaster.Columns["PRODNO"], "txtPRODNO") as PopupControl; // e.Row.FindChildControl<PopupControl>("txtPRODNO");
            ASPxTextBox txtQUANTITY = gvMaster.FindRowCellTemplateControl(e.VisibleIndex, (GridViewDataColumn)gvMaster.Columns["QUANTITY"], "txtQUANTITY") as ASPxTextBox;  //e.Row.FindChildControl<ASPxTextBox>("txtQUANTITY");
            ASPxTextBox txtUNIT_PRICE = gvMaster.FindRowCellTemplateControl(e.VisibleIndex, (GridViewDataColumn)gvMaster.Columns["UNIT_PRICE"], "txtUNIT_PRICE") as ASPxTextBox;  //e.Row.FindChildControl<ASPxTextBox>("txtUNIT_PRICE");
            ASPxTextBox txtIMEI = gvMaster.FindRowCellTemplateControl(e.VisibleIndex, (GridViewDataColumn)gvMaster.Columns["IMEI_QTY"], "txtIMEI") as ASPxTextBox;  //e.Row.FindChildControl<ASPxTextBox>("txtUNIT_PRICE");

            txtPRODNO.Enabled = false;
            txtQUANTITY.Enabled = false;
            txtUNIT_PRICE.ReadOnly = true;
            txtIMEI.Enabled = false;
        }
    }

    protected void gvMaster_CommandButtonInitialize(object sender, ASPxGridViewCommandButtonEventArgs e)
    {
        e.Enabled = false;
    }

    protected void gvCheckOut_HtmlRowPrepared(object sender, ASPxGridViewTableRowEventArgs e)
    {
        //string ITEM_STATUS = "";
        if (e.VisibleIndex != -1)
            e.Row.Enabled = false;
    }

    //ajax 呼當前網頁的方式
    [System.Web.Services.WebMethod()]
    [System.Web.Script.Services.ScriptMethod()]
    static public string getPRODINFO(string PRODNO)
    {
        DataTable dt = new Product_Facade().Query_ProductInfo(PRODNO);
        string r = "";
        if (dt.Rows.Count > 0)
        {
            r = StringUtil.CStr(dt.Rows[0]["PRODNAME"]) + ";";
            r += StringUtil.CStr(dt.Rows[0]["IMEI_FLAG"]) + ";";
            if (dt.Rows[0]["PRICE"] != null && dt.Rows[0]["PRICE"] != DBNull.Value && StringUtil.CStr(dt.Rows[0]["PRICE"]) != "")
                r += StringUtil.CStr(dt.Rows[0]["PRICE"]);
            else
                r += "0";
        }
        return r;
    }

    //ajax CheckIMEI數
    [System.Web.Services.WebMethod()]
    [System.Web.Script.Services.ScriptMethod()]
    static public int getPROD_IMEI_COUNT(string TABLENAME, string PO_OE_NO, string PRODNO)
    {
        DataTable dt = new IMEI_Facade().getINV_IMEI(TABLENAME, PO_OE_NO, PRODNO);
        return dt.Rows.Count;
    }

    //ajax IMEI清單
    [System.Web.Services.WebMethod()]
    [System.Web.Script.Services.ScriptMethod()]
    static public string IMEIContent(string TABLENAME, string ID, string PRODNO)
    {
        DataTable dt = new IMEI_Facade().getINV_IMEI(TABLENAME, ID, PRODNO);

        string IMEI_FORMAT = "<table border=\"1\">";
        foreach (DataRow dr in dt.Rows)
        {
            IMEI_FORMAT += "<tr><td>" + StringUtil.CStr(dr["IMEI"]) + "</td></tr>";
        }
        IMEI_FORMAT += "</table>";

        return IMEI_FORMAT;
    }

    protected void gvMaster_PageIndexChanged(object sender, EventArgs e)
    {
        DataTable dt = (DataTable)gvMaster.DataSource;
        if (dt == null || dt.Rows.Count == 0)
        {
            foreach (DataRow dr in TempTables.Detail.Rows)
            {
                if (dr["PRODNAME"] == null || string.IsNullOrEmpty(StringUtil.CStr(dr["PRODNAME"])))
                {
                    if (dr["PRODNO"] != null && (!string.IsNullOrEmpty(StringUtil.CStr(dr["PRODNO"]))))
                    {
                        DataTable dtProduct = new Product_Facade().Query_ProductInfo(StringUtil.CStr(dr["PRODNO"]));
                        if (dtProduct != null && dtProduct.Rows.Count > 0)
                            dr["PRODNAME"] = StringUtil.CStr(dtProduct.Rows[0]["PRODNAME"]);
                    }
                }
            }
            gvMaster.DataSource = TempTables.Detail;
            gvMaster.DataBind();
        }
    }

    protected void gvDetail_PageIndexChanged(object sender, EventArgs e)
    {
        DataTable dt = (DataTable)gvDetail.DataSource;
        if (dt == null || dt.Rows.Count == 0)
        {
            gvDetail.DataSource = TempTables.Discount;
            gvDetail.DataBind();
        }
    }

    protected void gvCheckOut_PageIndexChanged(object sender, EventArgs e)
    {
        DataTable dt = (DataTable)gvCheckOut.DataSource;
        if (dt == null || dt.Rows.Count == 0)
        {
            gvCheckOut.DataSource = TempTables.Paid;
            gvCheckOut.DataBind();
        }
    }
}
