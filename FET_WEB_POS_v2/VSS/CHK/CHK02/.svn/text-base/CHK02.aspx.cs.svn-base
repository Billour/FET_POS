using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.Web;
using DevExpress.Web.ASPxGridView;
using DevExpress.Web.ASPxPager;
using DevExpress.Web.ASPxEditors;
using System.Data;

using Advtek.Utility;
using FET.POS.Model.Common;
using FET.POS.Model.DTO;
using FET.POS.Model.Facade;
using FET.POS.Model.Facade.FacadeImpl;

public partial class VSS_CHK_CHK02 : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            Literal26.Visible = labTotalCouponAmount.Visible = false;
            Literal28.Visible = labBankCardAmount.Visible = false;
            //取得該門市 當日的訂單
            string WorkDate = OracleDBUtil.WorkDay(logMsg.STORENO); //營業日
            this.ASPxDateEdit1.Text = WorkDate;
            this.btnConfirm2.Enabled = false;

            //門市
            this.labStoreNo.Text = logMsg.STORENO;
            //機台
            this.labCashRegisterNo.Text = logMsg.MACHINE_ID;
        }
    }

    protected void doSelectSALE(DataSet dsSale)
    {
        ClearData();

        if (dsSale.Tables["MasterData"] != null)
        {
            DataTable dtMaster = dsSale.Tables["MasterData"];
            this.labStoreNo.Text = StringUtil.CStr(dtMaster.Rows[0]["STORE_NO"]);
            this.labCashRegisterNo.Text = StringUtil.CStr(dtMaster.Rows[0]["MACHINE_ID"]);
            this.labOperationDate.Text = StringUtil.CStr(dtMaster.Rows[0]["TRADE_DATE"]);
            //this.labTotalTurnover.Text = StringUtil.CStr(dtMaster.Rows[0]["SALE_TOTAL_AMOUNT"]); //營業總額
            this.labTransCount.Text = StringUtil.CStr(dtMaster.Rows[0]["D_COUNT"]);
            this.btnConfirm2.Enabled = true;
        }else{
            this.btnConfirm2.Enabled = false;
            //彈跳視窗
            ScriptManager.RegisterClientScriptBlock(this, GetType(), "AlertMessage", "alert('無交易資料!!');", true);
            return;
        }

        if (dsSale.Tables["OtherData"] != null)
        {
            DataTable dtOther = dsSale.Tables["OtherData"];
            this.labSaveMoney.Text = StringUtil.CStr(dtOther.Rows[0]["繳大鈔"]);
            this.labPettyCash.Text = StringUtil.CStr(dtOther.Rows[0]["找零金"]);
            this.labNumberOfCards.Text = StringUtil.CStr(dtOther.Rows[0]["刷卡張數"]);
            this.labVoidInvoices.Text = StringUtil.CStr(dtOther.Rows[0]["發票作廢張數"]);
            this.labVoidBills.Text = StringUtil.CStr(dtOther.Rows[0]["帳單作廢張數"]);
        }

        Int32 iTOTAL_AMOUNT = 0;
        if (dsSale.Tables["TOTAL_AMOUNT"] != null)
        {
            this.labTotalSales.Text = StringUtil.CStr(dsSale.Tables["TOTAL_AMOUNT"].Rows[0]["銷售總額"]);
            iTOTAL_AMOUNT = Convert.ToInt32(StringUtil.CStr(dsSale.Tables["TOTAL_AMOUNT"].Rows[0]["銷售總額"]));
        }
        Int32 iTOTAL_AMOUNT_CANCEL = 0;
        if (dsSale.Tables["TOTAL_AMOUNT_CANCEL"] != null)
        {
            this.labTotalSalesReturn.Text = StringUtil.CStr(dsSale.Tables["TOTAL_AMOUNT_CANCEL"].Rows[0]["銷退總額"]);
            iTOTAL_AMOUNT_CANCEL = Convert.ToInt32(StringUtil.CStr(dsSale.Tables["TOTAL_AMOUNT_CANCEL"].Rows[0]["銷退總額"]));
        }

        Int32 iTOTAL_AMOUNT_2 = 0;
        if (dsSale.Tables["TOTAL_AMOUNT_2"] != null)
        {
            this.labTotalCollections.Text = StringUtil.CStr(dsSale.Tables["TOTAL_AMOUNT_2"].Rows[0]["代收總額"]);
            iTOTAL_AMOUNT_2 = Convert.ToInt32(StringUtil.CStr(dsSale.Tables["TOTAL_AMOUNT_2"].Rows[0]["代收總額"]));
        }

        Int32 iTOTAL_AMOUNT_2_CANCEL = 0;
        if (dsSale.Tables["TOTAL_AMOUNT_2_CANCEL"] != null)
        {
            this.labTotalVoidCollections.Text = StringUtil.CStr(dsSale.Tables["TOTAL_AMOUNT_2_CANCEL"].Rows[0]["代收作廢總額"]);
            iTOTAL_AMOUNT_2_CANCEL = Convert.ToInt32(StringUtil.CStr(dsSale.Tables["TOTAL_AMOUNT_2_CANCEL"].Rows[0]["代收作廢總額"]));

        }

        int HappyGoPoints1 = 0;
        int HappyGoPoints2 = 0;
        int HappyGoAmount1 = 0;
        int HappyGoAmount2 = 0;
        if (dsSale.Tables["Statistics1"] != null)
        {
            DataTable dtStatistics1 = dsSale.Tables["Statistics1"];
            //this.labTotalSales.Text = StringUtil.CStr(dtStatistics1.Rows[0]["銷售總額"]);
            //this.labTotalSalesReturn.Text = StringUtil.CStr(dtStatistics1.Rows[0]["銷退總額"]);
            //this.labTotalCollections.Text = StringUtil.CStr(dtStatistics1.Rows[0]["代收總額"]);
            //this.labTotalVoidCollections.Text = StringUtil.CStr(dtStatistics1.Rows[0]["代收作廢總額"]);
            HappyGoPoints1 = Convert.ToInt32(StringUtil.CStr(dtStatistics1.Rows[0]["快樂購點數的被加數"]));
            HappyGoAmount1 = Convert.ToInt32(StringUtil.CStr(dtStatistics1.Rows[0]["快樂購金額的被加數"]));
        }


        Int32 iTOTAL_AMOUNT_CASH_AMOUNT = 0;
        if (dsSale.Tables["TOTAL_AMOUNT_CASH_AMOUNT"] != null)
        {
            this.labTotalCash.Text = StringUtil.CStr(Convert.ToInt32(StringUtil.CStr(dsSale.Tables["TOTAL_AMOUNT_CASH_AMOUNT"].Rows[0]["現金總額"])) - Convert.ToInt32(StringUtil.CStr(dsSale.Tables["TOTAL_AMOUNT_CANCLE_CASH_AMOUNT"].Rows[0]["現金銷退"])));
            iTOTAL_AMOUNT_CASH_AMOUNT = Convert.ToInt32(StringUtil.CStr(dsSale.Tables["TOTAL_AMOUNT_CASH_AMOUNT"].Rows[0]["現金總額"])) - Convert.ToInt32(StringUtil.CStr(dsSale.Tables["TOTAL_AMOUNT_CANCLE_CASH_AMOUNT"].Rows[0]["現金銷退"]));
        }

        Int32 iTOTAL_AMOUNT_CREDIT_AMOUNT = 0;
        if (dsSale.Tables["TOTAL_AMOUNT_CREDIT_AMOUNT"] != null)
        {
            this.labCreditCardAmount.Text = StringUtil.CStr(Convert.ToInt32(dsSale.Tables["TOTAL_AMOUNT_CREDIT_AMOUNT"].Rows[0]["信用卡額"]) - Convert.ToInt32(dsSale.Tables["TOTAL_AMOUNT_CREDIT_CANCLE_AMOUNT"].Rows[0]["信用卡銷退"]));
            iTOTAL_AMOUNT_CREDIT_AMOUNT = Convert.ToInt32(dsSale.Tables["TOTAL_AMOUNT_CREDIT_AMOUNT"].Rows[0]["信用卡額"]) - Convert.ToInt32(dsSale.Tables["TOTAL_AMOUNT_CREDIT_CANCLE_AMOUNT"].Rows[0]["信用卡銷退"]);

        }

        Int32 iTOTAL_AMOUNT_TERM_AMOUNT = 0;
        if (dsSale.Tables["TOTAL_AMOUNT_TERM_AMOUNT"] != null)
        {
            this.labInstalmentAmount.Text = StringUtil.CStr(Convert.ToInt32(dsSale.Tables["TOTAL_AMOUNT_TERM_AMOUNT"].Rows[0]["分期付款"]) - Convert.ToInt32(dsSale.Tables["TOTAL_AMOUNT_TERM_CANCLE_AMOUNT"].Rows[0]["分期付款銷退"]));
            iTOTAL_AMOUNT_TERM_AMOUNT = Convert.ToInt32(dsSale.Tables["TOTAL_AMOUNT_TERM_AMOUNT"].Rows[0]["分期付款"]) - Convert.ToInt32(dsSale.Tables["TOTAL_AMOUNT_TERM_CANCLE_AMOUNT"].Rows[0]["分期付款銷退"]);
        }

        Int32 iTOTAL_AMOUNT_VOUCHER_AMOUNT = 0;
        if (dsSale.Tables["TOTAL_AMOUNT_VOUCHER_AMOUNT"] != null)
        {
            this.labTotalCouponAmount.Text = StringUtil.CStr(dsSale.Tables["TOTAL_AMOUNT_VOUCHER_AMOUNT"].Rows[0]["禮券總額"]);
            iTOTAL_AMOUNT_VOUCHER_AMOUNT = Convert.ToInt32(StringUtil.CStr(dsSale.Tables["TOTAL_AMOUNT_VOUCHER_AMOUNT"].Rows[0]["禮券總額"]));
        }

        Int32 iTOTAL_AMOUNT_DEBIT_AMOUNT = 0;
        if (dsSale.Tables["TOTAL_AMOUNT_DEBIT_AMOUNT"] != null)
        {
            this.labBankCardAmount.Text = StringUtil.CStr(dsSale.Tables["TOTAL_AMOUNT_DEBIT_AMOUNT"].Rows[0]["金融卡額"]);
            iTOTAL_AMOUNT_DEBIT_AMOUNT = Convert.ToInt32(StringUtil.CStr(dsSale.Tables["TOTAL_AMOUNT_DEBIT_AMOUNT"].Rows[0]["金融卡額"]));
        }

        Int32 iRE_TOTAL_AMOUNT = 0;
        if (dsSale.Tables["RE_TOTAL_AMOUNT"] != null)
        {
            this.labTotalCollectionCash.Text = StringUtil.CStr(dsSale.Tables["RE_TOTAL_AMOUNT"].Rows[0]["代收現金總額"]);
            iRE_TOTAL_AMOUNT = Convert.ToInt32(StringUtil.CStr(dsSale.Tables["RE_TOTAL_AMOUNT"].Rows[0]["代收現金總額"]));
        }

        Int32 iRE_CARD_TOTAL_AMOUNT = 0;
        if (dsSale.Tables["RE_CARD_TOTAL_AMOUNT"] != null)
        {
            this.labCollectionCreditCardAmount.Text = StringUtil.CStr(dsSale.Tables["RE_CARD_TOTAL_AMOUNT"].Rows[0]["代收信用卡額"]);
            iRE_CARD_TOTAL_AMOUNT = Convert.ToInt32(StringUtil.CStr(dsSale.Tables["RE_CARD_TOTAL_AMOUNT"].Rows[0]["代收信用卡額"]));
        }

        if (dsSale.Tables["Statistics2"] != null)
        {
            DataTable dtStatistics2 = dsSale.Tables["Statistics2"];
            //this.labTotalCash.Text = StringUtil.CStr(dtStatistics2.Rows[0]["現金總額"]);
            //this.labCreditCardAmount.Text = StringUtil.CStr(dtStatistics2.Rows[0]["信用卡額"]);
            //this.labInstalmentAmount.Text = StringUtil.CStr(dtStatistics2.Rows[0]["分期付款"]);
            //this.labTotalCouponAmount.Text = StringUtil.CStr(dtStatistics2.Rows[0]["禮券總額"]);
            //this.labBankCardAmount.Text = StringUtil.CStr(dtStatistics2.Rows[0]["金融卡額"]);
            //this.labTotalCollectionCash.Text = StringUtil.CStr(dtStatistics2.Rows[0]["代收現金總額"]);
            //this.labCollectionCreditCardAmount.Text = StringUtil.CStr(dtStatistics2.Rows[0]["代收信用卡額"]);
            HappyGoPoints2 = Convert.ToInt32(StringUtil.CStr(dtStatistics2.Rows[0]["快樂購點數的加數"]));
            HappyGoAmount2 = Convert.ToInt32(StringUtil.CStr(dtStatistics2.Rows[0]["快樂購金額的加數"]));
        }

        ////銷售總額=現金總額+信用卡額+分期付款+禮券總額-銷退總額
        //this.labTotalSales.Text = StringUtil.CStr(iTOTAL_AMOUNT_CASH_AMOUNT + iTOTAL_AMOUNT_CREDIT_AMOUNT + iTOTAL_AMOUNT_TERM_AMOUNT + iTOTAL_AMOUNT_VOUCHER_AMOUNT + iTOTAL_AMOUNT_DEBIT_AMOUNT - iTOTAL_AMOUNT_CANCEL);
   

        //代收總額=代收現金總額+代收信用卡額-代收作廢總額
        //this.labTotalCollections.Text = StringUtil.CStr(iRE_TOTAL_AMOUNT+iRE_CARD_TOTAL_AMOUNT - iTOTAL_AMOUNT_2_CANCEL);
        //代收總額
        this.labTotalCollections.Text = StringUtil.CStr(iTOTAL_AMOUNT_2);

        //營業總額=銷售總額+代收總額-銷退總額-代收作廢總額
        this.labTotalTurnover.Text = StringUtil.CStr(iTOTAL_AMOUNT + iTOTAL_AMOUNT_2 - iTOTAL_AMOUNT_CANCEL - iTOTAL_AMOUNT_2_CANCEL);

        this.labHappyGoPoints.Text = StringUtil.CStr(HappyGoPoints1 + HappyGoPoints2);
        this.labHappyGoAmount.Text = StringUtil.CStr(HappyGoAmount1 + HappyGoAmount2);

        this.labMoDiFiedDate.Text = DateTime.Now.ToString("yyyy/MM/dd HH:mm");
        this.labModiUser.Text = logMsg.OPERATOR + " " + new Employee_Facade().GetEmpName(logMsg.OPERATOR);

        //**2011/04/30 Tina：新增「舊POS銷售作廢總額」「舊POS代收作廢總額」顯示。
        CHK02_Facade CHK02_Facade = new CHK02_Facade();
        string Date = this.ASPxDateEdit1.Text;
        this.lblSaleCancelTotalAmount.Text = CHK02_Facade.GetCancelTotalAmount(Date, logMsg.STORENO, logMsg.MACHINE_ID, "1");
        this.lblInsteadCancelTotalAmount.Text = CHK02_Facade.GetCancelTotalAmount(Date, logMsg.STORENO, logMsg.MACHINE_ID, "2");

    }

    protected void btnConfirm_Click(object sender, EventArgs e)
    {
        //** 2011/03/10 Tina：SA 1.1.1.4. 系統只允許針對當日的資料做多次機台結帳，非當日的資料不可以做機台[機台結帳確認]按鈕Disable。
        CHK02_Facade _CHK02_Facade = new CHK02_Facade();
        DataSet ds = _CHK02_Facade.Query_SaleMethodSet(this.labStoreNo.Text, this.labCashRegisterNo.Text, this.ASPxDateEdit1.Date.ToString("yyyy/MM/dd"));
        doSelectSALE(ds);

        if (this.ASPxDateEdit1.Date.ToString("yyyy/MM/dd") == DateTime.Now.ToString("yyyy/MM/dd"))
        {
            this.btnConfirm2.Enabled = true;
            this.labStatus.Text = "";
        }
        else if (this.ASPxDateEdit1.Date > DateTime.Today)
        {
            this.btnConfirm2.Enabled = false;
            this.labStatus.Text = "";
        }
        else
        {
            this.btnConfirm2.Enabled = false;
            this.labStatus.Text = "讀帳確認";
        }

    }

    protected void btnConfirm1_Click(object sender, EventArgs e)
    {
        CHK02_Facade _CHK02_Facade = new CHK02_Facade();

        CHK02_SALE_DTO.TERM_CLOSEDataTable dtSALE = doInsertSALE();

        int intResult = _CHK02_Facade.SaveSALE(dtSALE);

        this.labStatus.Text = "讀帳確認";
    }

    protected CHK02_SALE_DTO.TERM_CLOSEDataTable doInsertSALE()
    {
        CHK02_SALE_DTO.TERM_CLOSEDataTable dtSALE = null;

        dtSALE = new CHK02_SALE_DTO.TERM_CLOSEDataTable();
        CHK02_SALE_DTO.TERM_CLOSERow drSALE;

        drSALE = dtSALE.NewTERM_CLOSERow();
        drSALE["ID"] = GuidNo.getUUID();

        if (this.labOperationDate.Text.Trim() == "")
        { drSALE["DAY_CLOSE_DATE"] = DateUtil.NullDateFormat("DAY_CLOSE_DATE"); }
        else
        { drSALE["DAY_CLOSE_DATE"] = this.labOperationDate.Text; }

        drSALE["STORE_NO"] = this.labStoreNo.Text;

        if (this.labOperationDate.Text.Trim() == "")
        { drSALE["SALE_DATE"] = DateUtil.NullDateFormat("SALE_DATE"); }
        else
        { drSALE["SALE_DATE"] = this.labOperationDate.Text; }

        drSALE["MACHINE_ID"] = this.labCashRegisterNo.Text;

        if (this.labTransCount.Text.Trim() == "")
        { drSALE["GUEST_NUM"] = "0"; }
        else
        { drSALE["GUEST_NUM"] = this.labTransCount.Text; }

        drSALE["TC_STATUS"] = "";

        if (this.labTotalSales.Text.Trim() == "")
        { drSALE["SALE_TOTAL_AMOUNT"] = "0"; }
        else
        { drSALE["SALE_TOTAL_AMOUNT"] = this.labTotalSales.Text; }

        if (this.labTotalSalesReturn.Text.Trim() == "")
        { drSALE["RMA_AMOUNT"] = "0"; }
        else
        { drSALE["RMA_AMOUNT"] = this.labTotalSalesReturn.Text; }

        if (this.labTotalCollections.Text.Trim() == "")
        { drSALE["AGENCY_RECEIPT_AMOUNT"] = "0"; }
        else
        { drSALE["AGENCY_RECEIPT_AMOUNT"] = this.labTotalCollections.Text; }

        if (this.labTotalVoidCollections.Text.Trim() == "")
        { drSALE["AGENCY_RECEIPT_INVALID_AMOUNT"] = "0"; }
        else
        { drSALE["AGENCY_RECEIPT_INVALID_AMOUNT"] = this.labTotalVoidCollections.Text; }

        if (this.labTotalCollectionCash.Text.Trim() == "")
        { drSALE["AGENCY_RECEIPT_CASH_AMOUNT"] = "0"; }
        else
        { drSALE["AGENCY_RECEIPT_CASH_AMOUNT"] = this.labTotalCollectionCash.Text; }

        if (this.labCollectionCreditCardAmount.Text.Trim() == "")
        { drSALE["AGENCY_RECEIPT_CREDIT_AMOUNT"] = "0"; }
        else
        { drSALE["AGENCY_RECEIPT_CREDIT_AMOUNT"] = this.labCollectionCreditCardAmount.Text; }

        if (this.labPettyCash.Text.Trim() == "")
        { drSALE["SMALL_CHANGE_AMOUNT"] = "0"; }
        else
        { drSALE["SMALL_CHANGE_AMOUNT"] = this.labPettyCash.Text; }

        if (this.labTotalCash.Text.Trim() == "")
        { drSALE["CASH_AMOUNT"] = "0"; }
        else
        { drSALE["CASH_AMOUNT"] = this.labTotalCash.Text; }

        if (this.labCreditCardAmount.Text.Trim() == "")
        { drSALE["CREDIT_AMOUNT"] = "0"; }
        else
        { drSALE["CREDIT_AMOUNT"] = this.labCreditCardAmount.Text; }

        if (this.labInstalmentAmount.Text.Trim() == "")
        { drSALE["TERMPAY_AMOUNT"] = "0"; }
        else
        { drSALE["TERMPAY_AMOUNT"] = this.labInstalmentAmount.Text; }

        if (this.labTotalCouponAmount.Text.Trim() == "")
        { drSALE["GIFT_VOUCHER_AMOUNT"] = "0"; }
        else
        { drSALE["GIFT_VOUCHER_AMOUNT"] = this.labTotalCouponAmount.Text; }

        if (this.labBankCardAmount.Text.Trim() == "")
        { drSALE["DEBIT_CARD_AMOUNT"] = "0"; }
        else
        { drSALE["DEBIT_CARD_AMOUNT"] = this.labBankCardAmount.Text; }

        if (this.labSaveMoney.Text.Trim() == "")
        { drSALE["BIG_AMOUNT"] = "0"; }
        else
        { drSALE["BIG_AMOUNT"] = this.labSaveMoney.Text; }

        if (this.labHappyGoPoints.Text.Trim() == "")
        { drSALE["HG_POINT"] = "0"; }
        else
        { drSALE["HG_POINT"] = this.labHappyGoPoints.Text; }

        if (this.labHappyGoAmount.Text.Trim() == "")
        { drSALE["HG_AMOUNT"] = "0"; }
        else
        { drSALE["HG_AMOUNT"] = this.labHappyGoAmount.Text; }

        if (this.labNumberOfCards.Text.Trim() == "")
        { drSALE["PAY_BY_CREDIT"] = "0"; }
        else
        { drSALE["PAY_BY_CREDIT"] = this.labNumberOfCards.Text; }

        if (this.labVoidInvoices.Text.Trim() == "")
        { drSALE["INVALID_INVOICE"] = "0"; }
        else
        { drSALE["INVALID_INVOICE"] = this.labVoidInvoices.Text; }

        if (this.labVoidBills.Text.Trim() == "")
        { drSALE["INVALID_BILL"] = "0"; }
        else
        { drSALE["INVALID_BILL"] = this.labVoidBills.Text; }

        drSALE["CREATE_USER"] = logMsg.OPERATOR;
        drSALE["CREATE_DATE"] = Convert.ToDateTime(System.DateTime.Now);
        drSALE["MODI_USER"] = logMsg.OPERATOR;
        drSALE["MODI_DTM"] = Convert.ToDateTime(System.DateTime.Now);

        dtSALE.Rows.Add(drSALE);

        return dtSALE;

    }

    private void ClearData()
    {
        this.labOperationDate.Text = "";
        this.labTotalTurnover.Text = "";
        this.labTransCount.Text = "";
        this.labTotalSales.Text = "";
        this.labTotalSalesReturn.Text = "";
        this.labTotalCash.Text = "";
        this.labCreditCardAmount.Text = "";
        this.labInstalmentAmount.Text = "";
        this.labTotalCouponAmount.Text = "";
        this.labBankCardAmount.Text = "";
        this.labSaveMoney.Text = "";
        this.labTotalCollections.Text = "";
        this.labTotalVoidCollections.Text = "";
        this.labTotalCollectionCash.Text = "";
        this.labCollectionCreditCardAmount.Text = "";
        this.labPettyCash.Text = "";
        this.labHappyGoPoints.Text = "";
        this.labHappyGoAmount.Text = "";
        this.labNumberOfCards.Text = "";
        this.labVoidInvoices.Text = "";
        this.labVoidBills.Text = "";
        this.labMoDiFiedDate.Text = "";
        this.labModiUser.Text = "";
    }


}
