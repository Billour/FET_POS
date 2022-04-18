using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.Web;
using DevExpress.Web.ASPxGridView;
using DevExpress.Web.ASPxPager;
using DevExpress.Web.ASPxEditors;
using Advtek.Utility;
using FET.POS.Model.Facade.FacadeImpl;
using FET.POS.Model.Common;
using FET.POS.Model.DTO;

public partial class VSS_CHK_CHK01 : BasePage
{
    bool OpenChkData = false ; //是否顯示比對資料
    protected void Page_Load(object sender, EventArgs e)
    {
        Literal27.Visible = txtGift.Visible = false;
        Literal28.Visible = txtDebitCard.Visible = false;
        //this.btnExec.Enabled = false;//結算確認
        //Label3.Text = "";//狀態
        txtCashTotal.ForeColor = System.Drawing.ColorTranslator.FromHtml("#000000");
        txtCreditCard.ForeColor = System.Drawing.ColorTranslator.FromHtml("#000000");
        txtInstallment.ForeColor = System.Drawing.ColorTranslator.FromHtml("#000000");
        txtGift.ForeColor = System.Drawing.ColorTranslator.FromHtml("#000000");
        txtDebitCard.ForeColor = System.Drawing.ColorTranslator.FromHtml("#000000");
        if (!Page.IsPostBack)
        {
            try
            {
                //是否顯示比對資料
                if (!OpenChkData)
                {
                    t1.ClientVisible = false;
                    t2.ClientVisible = false;
                    t3.ClientVisible = false;
                    t4.ClientVisible = false;
                    t5.ClientVisible = false;
                }

                string sSTORE_NO = "";
                string sDate = "";
                //取得部門NO

                DataTable dt = new Employee_Facade().Query_STORENO(logMsg.OPERATOR);
                if (dt.Rows.Count > 0)
                {
                    //sSTORE_NO = StringUtil.CStr(dt.Rows[0]["STORENO"]);
                    sSTORE_NO = logMsg.STORENO;

                    Label1.Text = sSTORE_NO;
                    Label9.Text = logMsg.OPERATOR + " " + new Employee_Facade().GetEmpName(logMsg.MODI_USER);
                }
                if (sSTORE_NO == "")
                    throw new Exception("非正式員工");
                //**2011/03/17 Tina：移除只有店長才可以執行門市日結作業的判斷。
                //if (logMsg.ROLE_TYPE != "1")
                //    throw new Exception("非店長");
                //取得最後日結營業日
                //sDate= new CHK01_Facade().Query_TRADEDATE(sSTORE_NO);
                dt = new CHK01_Facade().Query_DAY_CLOSE_DATE(sSTORE_NO);
                if (dt.Rows.Count > 0)
                {
                    sDate = StringUtil.CStr(dt.Rows[0]["DAY_CLOSE_DATE"]);
                    Label7.Text = sDate;
                }

                if (sDate == "") throw new Exception("無最後日結營業日");

                //取得該門市 當日的訂單
                string WorkDate = OracleDBUtil.WorkDay(logMsg.STORENO); //營業日

                //檢查是否為營業中部門 ?
                this.S_DATE.Text = WorkDate;
                btnConfirm_Click(this, EventArgs.Empty);
                //日期
                Label6.Text = DateTime.Now.ToString("yyyy/MM/dd HH:mm");
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Common", @"alert('" + ex.Message.Replace("\n", "\\n") + "');", true);
                this.btnExec.Enabled = false;
            }


        }
    }

    protected void btnConfirm_Click(object sender, EventArgs e)
    {
        setDetailData(Label1.Text, S_DATE.Text);
    }

    private void setTitleData(string sSTORE_NO, string sDate)
    {
        CHK01_Facade cCHK01 = new CHK01_Facade();

        DataTable dt1 = cCHK01.Query_TOTAL_AMOUNT(sSTORE_NO, sDate);//銷售總額
        DataTable dt2 = cCHK01.Query_TOTAL_AMOUNT_CANCEL(sSTORE_NO, sDate); //銷退總額
        DataTable dt3 = cCHK01.Query_TOTAL_AMOUNT_2(sSTORE_NO, sDate); //代收總額
        DataTable dt4 = cCHK01.Query_TOTAL_AMOUNT_2_CANCEL(sSTORE_NO, sDate);//代收作廢總額
        DataTable dt5 = cCHK01.Query_TOTAL_AMOUNT_HG_POINT(sSTORE_NO, sDate);//快樂購點數
        DataTable dt6 = cCHK01.Query_TOTAL_AMOUNT_HG_AMOUNT(sSTORE_NO, sDate); //快樂購金額
        DataTable dt7 = cCHK01.Query_SAIL_DATA(sSTORE_NO, sDate);
        int iTotal, iTotal_Cancel, iTotal2, iTotal2_Cancel, iTotal_HG_POINT, iTotal_HG_AMOUNT, iNum;
        string sTRADE_DATE = "";
        iTotal = 0;
        iTotal_Cancel = 0;
        iTotal2 = 0;
        iTotal2_Cancel = 0;
        iTotal_HG_POINT = 0;
        iTotal_HG_AMOUNT = 0;
        iNum = 0;
        if (dt1.Rows.Count > 0) iTotal = Convert.ToInt32(dt1.Rows[0]["TOTAL_AMOUNT"]);
        if (dt2.Rows.Count > 0) iTotal_Cancel = Convert.ToInt32(dt2.Rows[0]["TOTAL_AMOUNT"]);
        if (dt3.Rows.Count > 0) iTotal2 = Convert.ToInt32(dt3.Rows[0]["TOTAL_AMOUNT"]);
        if (dt4.Rows.Count > 0) iTotal2_Cancel = Convert.ToInt32(dt4.Rows[0]["TOTAL_AMOUNT"]);
        if (dt5.Rows.Count > 0) iTotal_HG_POINT = Convert.ToInt32(dt5.Rows[0]["TOTAL_AMOUNT"]);
        if (dt6.Rows.Count > 0) iTotal_HG_AMOUNT = Convert.ToInt32(dt6.Rows[0]["TOTAL_AMOUNT"]);
        if (dt7.Rows.Count > 0)
        {
            iNum = dt7.Rows.Count;
            sTRADE_DATE = ((DateTime)dt7.Rows[0]["TRADE_DATE"]).ToString("yyyy/MM/dd");
        }
        Label11.Text = StringUtil.CStr(iTotal); //銷售總額
        Label13.Text = StringUtil.CStr(iTotal_Cancel); //銷退總額
        Label32.Text = StringUtil.CStr(iTotal2); //代收總額
        Label33.Text = StringUtil.CStr(iTotal2_Cancel); //代收作廢總額
        Label12.Text = StringUtil.CStr(iTotal_HG_POINT); //快樂購點數
        Label14.Text = StringUtil.CStr(iTotal_HG_AMOUNT); //快樂購金額
        Label5.Text = StringUtil.CStr(((iTotal + iTotal2) - (iTotal_Cancel + iTotal2_Cancel))); //營業總額=(銷售總額+代收總額)-(銷退總額+代收作廢總額)
        //Label5.Text = StringUtil.CStr((iTotal + iTotal2-iTotal_Cancel )); //營業總額=銷售總額+代收總額

        Label8.Text = StringUtil.CStr(iNum);//交易數
        Label7.Text = sTRADE_DATE;//營業日期

        setMachineStatus(sSTORE_NO, sDate);

    }

    private void setDetailData(string sSTORE_NO, string sDate)
    {
        //檢查DAY_CLOSE是否有資料
        CHK01_Facade cCHK01 = new CHK01_Facade();
        //現金總額
        t1.Text = StringUtil.CStr(Convert.ToInt32(cCHK01.Query_TOTAL_AMOUNT_CASH_AMOUNT(sSTORE_NO, sDate).Rows[0]["TOTAL_AMOUNT"].ToString()) - Convert.ToInt32(cCHK01.Query_TOTAL_AMOUNT_CANCLE_CASH_AMOUNT(sSTORE_NO, sDate).Rows[0]["TOTAL_AMOUNT"].ToString()));
        //信用卡額
        t2.Text = StringUtil.CStr(Convert.ToInt32(cCHK01.Query_TOTAL_AMOUNT_CREDIT_AMOUNT(sSTORE_NO, sDate).Rows[0]["TOTAL_AMOUNT"].ToString()) - Convert.ToInt32(cCHK01.Query_TOTAL_AMOUNT_CANCLE_CREDIT_AMOUNT(sSTORE_NO, sDate).Rows[0]["TOTAL_AMOUNT"].ToString()));
        //分期付款
        t3.Text = StringUtil.CStr(Convert.ToInt32(cCHK01.Query_TOTAL_AMOUNT_TERM_AMOUNT(sSTORE_NO, sDate).Rows[0]["TOTAL_AMOUNT"].ToString()) - Convert.ToInt32(cCHK01.Query_TOTAL_AMOUNT_CANCLE_TERM_AMOUNT(sSTORE_NO, sDate).Rows[0]["TOTAL_AMOUNT"].ToString()));
        //-禮券總額
        t4.Text = cCHK01.Query_TOTAL_AMOUNT_VOUCHER_AMOUNT(sSTORE_NO, sDate).Rows[0]["TOTAL_AMOUNT"].ToString();
        //金融卡額
        t5.Text = cCHK01.Query_TOTAL_AMOUNT_DEBIT_AMOUNT(sSTORE_NO, sDate).Rows[0]["TOTAL_AMOUNT"].ToString();
        DataTable dt = cCHK01.Query_VW_CHK01_DAY_CLOSE_STATUS(sSTORE_NO, sDate);
        if (dt.Rows.Count > 0) //有
        {
            DataTable dtMachine = new CHK01_Facade().Query_SAIL_MACHINE(sSTORE_NO, sDate);
            if (dtMachine.Rows.Count > 0)
            {
                DataRow dr = dt.Rows[0];
                lblDayCloseKey.Text = StringUtil.CStr(dr["ID"]);

                Label11.Text = StringUtil.CStr(dr["SALE_AMOUNT"]);//銷售總額
                if (Label11.Text == "") Label11.Text = "0";

                Label13.Text = StringUtil.CStr(dr["RMA_AMOUNT"]); //銷退總額
                if (Label13.Text == "") Label13.Text = "0";

                Label32.Text = StringUtil.CStr(dr["AGENCY_RECEIPT_AMOUNT"]);//代收總額
                if (Label32.Text == "") Label32.Text = "0";

                Label33.Text = StringUtil.CStr(dr["AGENCY_RECEIPT_INVALID_AMOUNT"]);//代收作廢總額
                if (Label33.Text == "") Label33.Text = "0";

                Label12.Text = StringUtil.CStr(dr["HG_POINT"]);//快樂購點數
                if (Label12.Text == "") Label12.Text = "0";

                Label14.Text = StringUtil.CStr(dr["HG_AMOUNT"]);//快樂購金額
                if (Label14.Text == "") Label14.Text = "0";
                DataTable dt7 = cCHK01.Query_SAIL_DATA(sSTORE_NO, sDate);
                if (dt7.Rows.Count > 0)
                {
                    Label8.Text = StringUtil.CStr(dt7.Rows.Count);
                }
                if (Label8.Text == "") Label8.Text = "0";
                Label5.Text = StringUtil.CStr((Convert.ToInt32(Label11.Text) + Convert.ToInt32(Label32.Text) - Convert.ToInt32(Label13.Text) - Convert.ToInt32(Label33.Text))); //營業總額

                txtCashTotal.Text = StringUtil.CStr(dr["CASH_AMOUNT"]); //現金總額
                txtCreditCard.Text = StringUtil.CStr(dr["CREDIT_AMOUNT"]);//信用卡額
                txtInstallment.Text = StringUtil.CStr(dr["TERMPAY_AMOUNT"]);//分期付款
                txtGift.Text = StringUtil.CStr(dr["GIFT_VOUCHER_AMOUNT"]);//-禮券總額
                txtDebitCard.Text = StringUtil.CStr(dr["DEBIT_CARD_AMOUNT"]);//金融卡額
                Label31.Text = ((DateTime)dr["DC_DATETIME"]).ToString("yyyy/MM/dd HH:mm:ss");//日結時間
                Label7.Text = ((DateTime)dr["SALE_DATE"]).ToString("yyyy/MM/dd");

                Label3.ForeColor = System.Drawing.ColorTranslator.FromHtml("#0200A0");
                if (StringUtil.CStr(dr["DC_STATUS"]) == "1")
                {
                    Label3.Text = "日結完成";//狀態
                    Label3.ForeColor = System.Drawing.ColorTranslator.FromHtml("#FA0300");
                    this.btnExec.Enabled = true;
                }
                else if (StringUtil.CStr(dr["DC_STATUS"]) == "2")
                {
                    Label3.Text = "結算完成";//狀態
                    this.btnExec.Enabled = false;
                }
                else
                {
                    Label3.Text = "系統結算完成";//狀態
                    this.btnExec.Enabled = false;
                }

                setMachineStatus(sSTORE_NO, sDate);
                checkData();
            }
            else
            {
                Label3.Text = "沒有機台資料";//狀態
                this.btnExec.Enabled = false;
            }
        }
        else  //無
        {
            setTitleData(sSTORE_NO, sDate);
            Label3.Text = "";//"沒有日結資料";//狀態
            this.btnExec.Enabled = true;
            txtCashTotal.Text = "0"; //現金總額
            txtCreditCard.Text = "0";//信用卡額
            txtInstallment.Text = "0";//分期付款
            txtGift.Text = "0";//-禮券總額
            txtDebitCard.Text = "0";//金融卡額
            Label31.Text = "";//日結時間
        }

        //**2011/04/30 Tina：新增「舊POS銷售作廢總額」「舊POS代收作廢總額」顯示。日結作業不需代入機台編號
        CHK02_Facade CHK02_Facade = new CHK02_Facade();
        string Date = this.S_DATE.Text;
        this.lblSaleCancelTotalAmount.Text = CHK02_Facade.GetCancelTotalAmount(Date, logMsg.STORENO, "", "1");
        this.lblInsteadCancelTotalAmount.Text = CHK02_Facade.GetCancelTotalAmount(Date, logMsg.STORENO, "", "2");

    }

    private void setMachineStatus(string sSTORE_NO, string sDate)
    {
        DataTable dt7 = new CHK01_Facade().Query_SAIL_MACHINE(sSTORE_NO, sDate);
        for (int i = 0; i < dt7.Rows.Count; i++)
        {
            Literal lt = new Literal();
            lt.Text = "<br/>";
            Label ltlMachine = new Label();
            ltlMachine.ID = "ltlMachine" + StringUtil.CStr((i + 1));
            //Label ltlMachineID = new Label();
            //ltlMachineID.ID = "ltlMachineID" + StringUtil.CStr((i + 1));
            TextBox txtMachine = new TextBox();
            txtMachine.ID = "txtMachine" + StringUtil.CStr((i + 1));
            txtMachine.ReadOnly = true;
            if (dt7.Rows[i] != null)
            {
                ltlMachine.Visible = true;
                txtMachine.Visible = true;
                txtMachine.Enabled = true;

                ltlMachine.Text = StringUtil.CStr(dt7.Rows[i]["MACHINE_NAME"]);
                // ltlMachineID.Text = StringUtil.CStr(dt7.Rows[i]["MACHINE_ID2"]);
                if (dt7.Rows[i]["MACHINE_ID1"] == DBNull.Value)
                    txtMachine.BackColor = System.Drawing.ColorTranslator.FromHtml("#FA0300");
                else
                    txtMachine.BackColor = System.Drawing.ColorTranslator.FromHtml("#00BE02");
            }
            else
            {
                ltlMachine.Visible = false;
                txtMachine.Visible = false;
                txtMachine.Enabled = false;
            }
            Panel1.Controls.Add(ltlMachine);
            //Panel1.Controls.Add(ltlMachineID);
            Panel1.Controls.Add(txtMachine);
            Panel1.Controls.Add(lt);
        }
    }

    private bool checkData()
    {
        bool bRet = false;
        CHK01_Facade cCHK01 = new CHK01_Facade();
        string sStoreNo = Label1.Text;
        string sDate = S_DATE.Text.Trim();
        try
        {
            int iSALE_AMOUNT, iRMA_AMOUNT, iAGENCY_RECEIPT_AMOUNT, iAGENCY_RECEIPT_INVALID_AMOUNT, iHG_PO;
            int iHG_AMOUNT, iSALE_TOTAL, iCASH_AMOUNT, iCREDIT_AMOUNT, iTERMPAY_AMOUNT, iGIFT_VOUCHER_AMOUNT, iDEBIT_CARD_AMOUNT;

            iSALE_AMOUNT = Convert.ToInt32(Label11.Text);//銷售總額
            iRMA_AMOUNT = Convert.ToInt32(Label13.Text); //銷退總額
            iAGENCY_RECEIPT_AMOUNT = Convert.ToInt32(Label32.Text);//代收總額
            iAGENCY_RECEIPT_INVALID_AMOUNT = Convert.ToInt32(Label33.Text);//代收作廢總額
            iHG_PO = Convert.ToInt32(Label12.Text);//快樂購點數
            iHG_AMOUNT = Convert.ToInt32(Label14.Text);//快樂購金額
            iSALE_TOTAL = Convert.ToInt32(Label5.Text); //營業總額
            iCASH_AMOUNT = 0;
            if (checkInteger(txtCashTotal.Text.Trim()))
                iCASH_AMOUNT = Convert.ToInt32(txtCashTotal.Text.Trim()); //現金總額

            if (checkInteger(txtCreditCard.Text.Trim()))
                iCREDIT_AMOUNT = Convert.ToInt32(txtCreditCard.Text.Trim());//信用卡額
            else
                throw new Exception("信用卡額非數字格式");
            if (checkInteger(txtInstallment.Text.Trim()))
                iTERMPAY_AMOUNT = Convert.ToInt32(txtInstallment.Text.Trim());//分期付款
            else
                throw new Exception("分期付款非數字格式");

            if (checkInteger(txtGift.Text.Trim()))
                iGIFT_VOUCHER_AMOUNT = Convert.ToInt32(txtGift.Text.Trim());//-禮券總額
            else
                throw new Exception("禮券總額非數字格式");

            if (checkInteger(txtDebitCard.Text.Trim()))
                iDEBIT_CARD_AMOUNT = Convert.ToInt32(txtDebitCard.Text.Trim());//金融卡額
            else
                throw new Exception("金融卡額非數字格式");


            //int iTotal1 = iSALE_AMOUNT - iRMA_AMOUNT + iAGENCY_RECEIPT_AMOUNT - iAGENCY_RECEIPT_INVALID_AMOUNT;
            int iTotal1 = iSALE_TOTAL;

            int iTotal2 = iCASH_AMOUNT + iCREDIT_AMOUNT + iTERMPAY_AMOUNT + iGIFT_VOUCHER_AMOUNT + iDEBIT_CARD_AMOUNT;
            if ((iTotal1 == iTotal2))
                bRet = true;
            else
                bRet = false;
            if (Convert.ToDecimal(cCHK01.Query_TOTAL_AMOUNT_CASH_AMOUNT(sStoreNo, sDate).Rows[0]["TOTAL_AMOUNT"]) - Convert.ToDecimal(cCHK01.Query_TOTAL_AMOUNT_CANCLE_CASH_AMOUNT(sStoreNo, sDate).Rows[0]["TOTAL_AMOUNT"]) != iCASH_AMOUNT)
            {
                bRet = false;//現金總額
                txtCashTotal.ForeColor = System.Drawing.ColorTranslator.FromHtml("#FA0300");
            }
            if (Convert.ToDecimal(cCHK01.Query_TOTAL_AMOUNT_CREDIT_AMOUNT(sStoreNo, sDate).Rows[0]["TOTAL_AMOUNT"]) - Convert.ToDecimal(cCHK01.Query_TOTAL_AMOUNT_CANCLE_CREDIT_AMOUNT(sStoreNo, sDate).Rows[0]["TOTAL_AMOUNT"]) != iCREDIT_AMOUNT)
            {
                bRet = false;//信用卡額
                txtCreditCard.ForeColor = System.Drawing.ColorTranslator.FromHtml("#FA0300");
            }
            if (Convert.ToDecimal(cCHK01.Query_TOTAL_AMOUNT_TERM_AMOUNT(sStoreNo, sDate).Rows[0]["TOTAL_AMOUNT"]) - Convert.ToDecimal(cCHK01.Query_TOTAL_AMOUNT_CANCLE_TERM_AMOUNT(sStoreNo, sDate).Rows[0]["TOTAL_AMOUNT"]) != iTERMPAY_AMOUNT)
            {
                bRet = false;//分期付款
                txtInstallment.ForeColor = System.Drawing.ColorTranslator.FromHtml("#FA0300");
            }
            if (Convert.ToDecimal(cCHK01.Query_TOTAL_AMOUNT_VOUCHER_AMOUNT(sStoreNo, sDate).Rows[0]["TOTAL_AMOUNT"]) != iGIFT_VOUCHER_AMOUNT)
            {
                bRet = false;//-禮券總額
                txtGift.ForeColor = System.Drawing.ColorTranslator.FromHtml("#FA0300");
            }
            if (Convert.ToDecimal(cCHK01.Query_TOTAL_AMOUNT_DEBIT_AMOUNT(sStoreNo, sDate).Rows[0]["TOTAL_AMOUNT"]) != iDEBIT_CARD_AMOUNT)
            {
                bRet = false;//金融卡額
                txtDebitCard.ForeColor = System.Drawing.ColorTranslator.FromHtml("#FA0300");
            }
            //if (Convert.ToDecimal(cCHK01.Query_TOTAL_AMOUNT_CASH_AMOUNT(sStoreNo, sDate).Rows[0]["TOTAL_AMOUNT"]) == iCASH_AMOUNT)
            //    bRet = true;//現金總額
            //else
            //    txtCashTotal.ForeColor = System.Drawing.ColorTranslator.FromHtml("#FA0300");

            //if (Convert.ToDecimal(cCHK01.Query_TOTAL_AMOUNT_CREDIT_AMOUNT(sStoreNo, sDate).Rows[0]["TOTAL_AMOUNT"]) == iCREDIT_AMOUNT)
            //    bRet = true;//信用卡額
            //else
            //    txtCreditCard.ForeColor = System.Drawing.ColorTranslator.FromHtml("#FA0300");

            //if (Convert.ToDecimal(cCHK01.Query_TOTAL_AMOUNT_TERM_AMOUNT(sStoreNo, sDate).Rows[0]["TOTAL_AMOUNT"]) == iTERMPAY_AMOUNT)
            //    bRet = true;//分期付款
            //else
            //    txtInstallment.ForeColor = System.Drawing.ColorTranslator.FromHtml("#FA0300");

            //if (Convert.ToDecimal(cCHK01.Query_TOTAL_AMOUNT_VOUCHER_AMOUNT(sStoreNo, sDate).Rows[0]["TOTAL_AMOUNT"]) == iGIFT_VOUCHER_AMOUNT)
            //    bRet = true;//-禮券總額
            //else
            //    txtGift.ForeColor = System.Drawing.ColorTranslator.FromHtml("#FA0300");

            //if (Convert.ToDecimal(cCHK01.Query_TOTAL_AMOUNT_DEBIT_AMOUNT(sStoreNo, sDate).Rows[0]["TOTAL_AMOUNT"]) == iDEBIT_CARD_AMOUNT)
            //    bRet = true;//金融卡額
            //else
            //    txtDebitCard.ForeColor = System.Drawing.ColorTranslator.FromHtml("#FA0300");
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Common", @"alert('" + ex.Message.Replace("\n", "\\n") + "');", true);
            throw ex;
        }

        return bRet;
    }

    private bool checkInteger(string sValue)
    {
        int iTmp;
        return Int32.TryParse(sValue, out iTmp);
    }

    protected void btnExec_OnClick(object sender, EventArgs e)
    {
        if (txtCashTotal.Text == "0" && txtCreditCard.Text == "0" && txtInstallment.Text == "0" && txtGift.Text == "0" && txtDebitCard.Text == "0")
        {
            //setMachineStatus(Label1.Text, S_DATE.Text.Trim());
            btnConfirm_Click(this, EventArgs.Empty);
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Common", @"alert('輸入金額不可全部都為0');", true);
            return;
        }

         if (Label8.Text == "0")
        {
            //setMachineStatus(Label1.Text, S_DATE.Text.Trim());
            btnConfirm_Click(this, EventArgs.Empty);
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Common", @"alert('無交易資料!!');", true);
            return;
        }


        CHK01_Facade cCHK01 = new CHK01_Facade();
        CHK01_DAY_CLOSE_DTO ds = new CHK01_DAY_CLOSE_DTO();
        CHK01_DAY_CLOSE_DTO.DAY_CLOSEDataTable _dt = ds.DAY_CLOSE;
        CHK01_DAY_CLOSE_DTO.DAY_CLOSERow _dr = _dt.NewDAY_CLOSERow();

        CHK01_DAY_CLOSE_DTO.DAY_CLOSE_STATUSDataTable _dt2 = ds.DAY_CLOSE_STATUS;
        CHK01_DAY_CLOSE_DTO.DAY_CLOSE_STATUSRow _dr2 = _dt2.NewDAY_CLOSE_STATUSRow();

        CHK01_DAY_CLOSE_DTO.STORE_WORKING_DAYDataTable _dt3 = ds.STORE_WORKING_DAY;
        CHK01_DAY_CLOSE_DTO.STORE_WORKING_DAYRow _dr3 = _dt3.NewSTORE_WORKING_DAYRow();
        bool isTrue = checkData();
        if (this.lblDayCloseKey.Text == "")//正常日結作業
        {
            try
            {
                _dr.ID = GuidNo.getUUID();//ID
                _dr.DAY_CLOSE_DATE = Convert.ToDateTime(S_DATE.Text.Trim());//門結日
                _dr.STORE_NO = Label1.Text;//門市代碼
                _dr.SALE_DATE = Convert.ToDateTime(Label7.Text.Trim());//營業日期
                _dr.DC_AMOUNT = Convert.ToDecimal(Label5.Text);//營業總額
                _dr.GUEST_NUM = Convert.ToDecimal(Label8.Text);//交易數
                if (isTrue)
                    _dr.DC_STATUS = 2;//交易狀態**1:日結完成 2:結算完成 3:系統結算完成
                else
                    _dr.DC_STATUS = 1;//交易狀態**1:日結完成 2:結算完成 3:系統結算完成

                _dr.SALE_AMOUNT = Convert.ToDecimal(Label11.Text);//銷售總額
                _dr.RMA_AMOUNT = Convert.ToDecimal(Label13.Text);//銷退總額
                _dr.AGENCY_RECEIPT_AMOUNT = Convert.ToDecimal(Label32.Text);//代收總額
                _dr.AGENCY_RECEIPT_INVALID_AMOUNT = Convert.ToDecimal(Label33.Text);//代收作廢總額
                _dr.HG_POINT = Convert.ToDecimal(Label12.Text);//快樂購點數
                _dr.HG_AMOUNT = Convert.ToDecimal(Label14.Text);//快樂購金額
                _dr.CASH_AMOUNT = Convert.ToDecimal(txtCashTotal.Text);//現金總額
                _dr.CREDIT_AMOUNT = Convert.ToDecimal(txtCreditCard.Text);//信用卡額
                _dr.TERMPAY_AMOUNT = Convert.ToDecimal(txtInstallment.Text);//分期付款
                _dr.GIFT_VOUCHER_AMOUNT = Convert.ToDecimal(txtGift.Text);//-禮券總額
                _dr.DEBIT_CARD_AMOUNT = Convert.ToDecimal(txtDebitCard.Text);//金融卡額
                _dr.DC_DATETIME = DateTime.Now;//門結時間
                _dr.CREATE_USER = Label9.Text;//人員
                _dr.CREATE_DATE = DateTime.Now;
                _dr.MODI_USER = Label9.Text;//人員
                _dr.MODI_DTM = DateTime.Now;
                _dt.Rows.Add(_dr);

                DataTable Dt_DAY_CLOSE_SATUS_ID = cCHK01.Query_DAY_CLOSE_SATUS_ID(Label1.Text, S_DATE.Text.Trim());

                if (Dt_DAY_CLOSE_SATUS_ID.Rows.Count > 0)
                    _dr2.ID = StringUtil.CStr(Dt_DAY_CLOSE_SATUS_ID.Rows[0]["ID"]);//ID
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Common", @"alert('無此日的日結狀態資料,請聯絡系統管理員。');", true);
                    return;
                }

                _dr2.DAY_CLOSE_DATE = Convert.ToDateTime(S_DATE.Text.Trim());// S_DATE.Text.Trim();//門結日
                if (isTrue)
                    _dr2.CLOSE_STATUS = "Y";//結帳狀態**結帳完成
                else
                    _dr2.CLOSE_STATUS = "N";//結帳狀態**尚未結帳
                _dr2.STORE_NO = Label1.Text;//門市代碼
                _dt2.Rows.Add(_dr2);

                if (!isTrue)
                {
                    foreach (DataColumn _dc3 in _dt3.Columns)
                        _dc3.AllowDBNull = true;

                    DataTable Dt_STORE_WORKING_DAY_SID = cCHK01.Query_STORE_WORKING_DAY_SID(Label1.Text);
                    if (Dt_STORE_WORKING_DAY_SID.Rows.Count > 0)
                        _dr3.SID = StringUtil.CStr(Dt_STORE_WORKING_DAY_SID.Rows[0]["SID"]);
                    else
                    {
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Common", @"alert('無此日的日結明細資料,請聯絡系統管理員。');", true);
                        return;
                    }

                    _dr3.WORKING_DATE = Convert.ToDateTime(S_DATE.Text.Trim()).AddDays(1);
                    _dt3.Rows.Add(_dr3);
                }
                ds.AcceptChanges();

                cCHK01.AddNewOne_DayClose(ds);

                if (isTrue)
                {
                    //整理頁面
                    Page.Response.Redirect(StringUtil.CStr(Page.Request.Url), true);
                }
                else
                {
                    btnConfirm_Click(this, EventArgs.Empty);
                    //日期
                    Label6.Text = DateTime.Now.ToString("yyyy/MM/dd HH:mm");
                }
            }
            catch //(Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Common", @"alert('日結狀態未更新,請聯絡系統管理員。');", true);
            }
        }
        else //重新結算日結
        {
            try
            {
                _dr.ID = this.lblDayCloseKey.Text;//ID
                _dr.DAY_CLOSE_DATE = Convert.ToDateTime(S_DATE.Text.Trim());//門結日
                _dr.STORE_NO = Label1.Text;//門市代碼
                _dr.SALE_DATE = Convert.ToDateTime(Label7.Text.Trim());//營業日期
                _dr.DC_AMOUNT = Convert.ToDecimal(Label5.Text);//營業總額
                _dr.GUEST_NUM = Convert.ToDecimal(Label8.Text);//交易數
                if (isTrue)
                    _dr.DC_STATUS = 2;//交易狀態**1:日結完成 2:結算完成 3:系統結算完成
                else
                    _dr.DC_STATUS = 1;//交易狀態**1:日結完成 2:結算完成 3:系統結算完成

                _dr.SALE_AMOUNT = Convert.ToDecimal(Label11.Text);//銷售總額
                _dr.RMA_AMOUNT = Convert.ToDecimal(Label13.Text);//銷退總額
                _dr.AGENCY_RECEIPT_AMOUNT = Convert.ToDecimal(Label32.Text);//代收總額
                _dr.AGENCY_RECEIPT_INVALID_AMOUNT = Convert.ToDecimal(Label33.Text);//代收作廢總額
                _dr.HG_POINT = Convert.ToDecimal(Label12.Text);//快樂購點數
                _dr.HG_AMOUNT = Convert.ToDecimal(Label14.Text);//快樂購金額
                _dr.CASH_AMOUNT = Convert.ToDecimal(txtCashTotal.Text);//現金總額
                _dr.CREDIT_AMOUNT = Convert.ToDecimal(txtCreditCard.Text);//信用卡額
                _dr.TERMPAY_AMOUNT = Convert.ToDecimal(txtInstallment.Text);//分期付款
                _dr.GIFT_VOUCHER_AMOUNT = Convert.ToDecimal(txtGift.Text);//-禮券總額
                _dr.DEBIT_CARD_AMOUNT = Convert.ToDecimal(txtDebitCard.Text);//金融卡額
                _dr.DC_DATETIME = DateTime.Now;//門結時間
                //_dr.CREATE_USER = Label9.Text;//人員
                //_dr.CREATE_DATE = DateTime.Now;
                _dr.MODI_USER = Label9.Text;//人員
                _dr.MODI_DTM = DateTime.Now;
                _dt.Rows.Add(_dr);
                _dr2.STORE_NO = Label1.Text;//門市代碼


                DataTable Dt_DAY_CLOSE_SATUS_ID = cCHK01.Query_DAY_CLOSE_SATUS_ID(Label1.Text, S_DATE.Text.Trim());

                if (Dt_DAY_CLOSE_SATUS_ID.Rows.Count > 0)
                    _dr2.ID = StringUtil.CStr(Dt_DAY_CLOSE_SATUS_ID.Rows[0]["ID"]);//ID
                else
                    _dr2.ID = "";

                _dr2.DAY_CLOSE_DATE = Convert.ToDateTime(S_DATE.Text.Trim());// S_DATE.Text.Trim();//門結日
                if (isTrue)
                    _dr2.CLOSE_STATUS = "Y";//結帳狀態**結帳完成
                else
                    _dr2.CLOSE_STATUS = "N";//結帳狀態**尚未結帳

                _dt2.Rows.Add(_dr2);

                if (!isTrue)
                {
                    foreach (DataColumn _dc3 in _dt3.Columns)
                        _dc3.AllowDBNull = true;


                    DataTable Dt_STORE_WORKING_DAY_SID = cCHK01.Query_STORE_WORKING_DAY_SID(Label1.Text);
                    if (Dt_STORE_WORKING_DAY_SID.Rows.Count > 0)
                        _dr3.SID = StringUtil.CStr(Dt_STORE_WORKING_DAY_SID.Rows[0]["SID"]);
                    else
                        _dr3.SID = "";

                    _dr3.WORKING_DATE = Convert.ToDateTime(S_DATE.Text.Trim()).AddDays(1);
                    _dt3.Rows.Add(_dr3);
                }
                ds.AcceptChanges();

                //cCHK01.AddNewOne_DayClose(ds);
                cCHK01.UpdateOne_DayClose(ds);

                if (isTrue)
                {
                    //整理頁面
                    Page.Response.Redirect(StringUtil.CStr(Page.Request.Url), true);
                }
                else
                {
                    btnConfirm_Click(this, EventArgs.Empty);
                    //日期
                    Label6.Text = DateTime.Now.ToString("yyyy/MM/dd HH:mm");
                }
            }
            catch //(Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Common", @"alert('日結狀態未更新,請聯絡系統管理員。');", true);
            }
        }

    }


}
