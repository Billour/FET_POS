using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Advtek.Utility;
using System.Collections;
using DevExpress.Web.ASPxGridView;
using DevExpress.Web.ASPxEditors;
using FET.POS.Model.Facade;
using FET.POS.Model.Facade.FacadeImpl;
using FET.POS.Model.Common;
using iTextSharp.text;
using iTextSharp.text.pdf;


using System.IO;

using System.ComponentModel;
using System.Text;

public partial class VSS_PRE_PRE01 : BasePage
{
    DataTable TempTables
    {
        get
        {
            DataTable dt = new DataTable();
            if (Session["gvMaster"] == null)
            {
                dt.Columns.Add("ID", typeof(string));
                dt.Columns.Add("PRODNO", typeof(string));
                dt.Columns.Add("PRODNAME", typeof(string));
                dt.Columns.Add("UNIT_PRICE", typeof(int));
                dt.Columns.Add("QUANTITY", typeof(int));
                dt.Columns.Add("DESCRIPTION", typeof(string));
                dt.Columns.Add("AMOUNT", typeof(int));
                dt.Columns.Add("REMARK", typeof(string));
            }
            else
            {
                dt = Session["gvMaster"] as DataTable;
            }

            return dt;
        }
    }

    DataTable PayTables
    {
        get
        {
            DataTable dt = new DataTable();
            if (Session["gvPAID"] == null)
            {
                dt.Columns.Add("ID", typeof(string));
                dt.Columns.Add("PAID_MODE", typeof(string));
                dt.Columns.Add("PAID_MODE_NAME", typeof(string));
                dt.Columns.Add("PAID_AMOUNT", typeof(int));

                dt.Columns.Add("DESCRIPTION", typeof(string));
                dt.Columns.Add("CREDIT_TYPE", typeof(string));
                dt.Columns.Add("CREDIT_CARD_NO", typeof(string));
                dt.Columns.Add("CREDIT_CARD_AUTH_NO", typeof(string));

                dt.Columns.Add("CREDIT_CARD_TYPE_ID", typeof(string));
                dt.Columns.Add("CREDID_CARD_TYPE_NAME", typeof(string));
            }
            else
            {
                dt = Session["gvPAID"] as DataTable;
            }

            return dt;
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

    string _PREPAY_NO
    {
        get
        {
            return Request.QueryString["PREPAY_NO"] == null ? "" : StringUtil.CStr(Request.QueryString["PREPAY_NO"]).Trim();
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack)
        {
            //查詢當日是否有未結資料
            DataTable PAID_CACHE = new PRE01_Facade().Query_PREPAY_PAID_CACHE(logMsg.MACHINE_ID, logMsg.STORENO);
            txtCOUNT.Text = StringUtil.CStr(PAID_CACHE.Rows.Count);

            Session["gvMaster"] = null;
            Session["gvPAID"] = null;
            gvMaster.Settings.ShowFooter = false;
            gvPAID.Settings.ShowFooter = false;
            this.lblMOUSER.Text = logMsg.OPERATOR + " " + new Employee_Facade().GetEmpName(logMsg.MODI_USER);
            lblMODTM.Text = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");
            txtPREPAY_STATUS.Text = "1-未存檔";
            txtSTORE_NO.Text = logMsg.STORENO;
            BtnCelOrd.ClientEnabled = false;
            BtnPri.ClientEnabled = false;
            if (_PREPAY_NO != "")
            {
                //帶入資料非空白查詢PREPAY_HEAD
                DataTable dt = new PRE01_Facade().Query_PREPAY_HEAD(_PREPAY_NO);
                txtPREPAY_NO.Text = StringUtil.CStr(dt.Rows[0]["PREPAY_NO"]);
                txtINVOICE_NO.Text = StringUtil.CStr(dt.Rows[0]["INVOICE_NO"]);
                txtUNI_TITLE.Text = StringUtil.CStr(dt.Rows[0]["UNI_TITLE"]);
                txtUNI_NO.Text = StringUtil.CStr(dt.Rows[0]["UNI_NO"]);
                txtSTART_TYPE.Value = dt.Rows[0]["START_TYPE"];
                txtID_NO.Text = StringUtil.CStr(dt.Rows[0]["ID_NO"]);
                txtCUST_NAME.Text = StringUtil.CStr(dt.Rows[0]["CUST_NAME"]);
                txtMSISDN.Text = StringUtil.CStr(dt.Rows[0]["MSISDN"]);
                txtCONTACT_PHONE.Text = StringUtil.CStr(dt.Rows[0]["CONTACT_PHONE"]);
                txtEMAIL.Text = StringUtil.CStr(dt.Rows[0]["EMAIL"]);
                txtPREPAY_STATUS.Text = StringUtil.CStr(dt.Rows[0]["PREPAY_STATUS"]) + "-" + StringUtil.CStr(dt.Rows[0]["PREPAY_STATUS_NAME"]);
                this.lblMOUSER.Text = StringUtil.CStr(dt.Rows[0]["MODI_USER"]) + " " + new Employee_Facade().GetEmpName(StringUtil.CStr(dt.Rows[0]["MODI_USER"]));
                lblMODTM.Text = StringUtil.CStr(dt.Rows[0]["MODI_DTM"]);
                txtUUID.Text = StringUtil.CStr(dt.Rows[0]["ID"]);

                //PREPAY_ITEM
                Session["gvMaster"] = new PRE01_Facade().Query_PREPAY_ITEM(StringUtil.CStr(dt.Rows[0]["ID"]));
                BindMasterData();
                //PREPAY_PAID
                Session["gvPAID"] = new PRE01_Facade().Query_PREPAY_PAID(StringUtil.CStr(dt.Rows[0]["ID"]));
                BindPAIDData();



                switch (StringUtil.CStr(dt.Rows[0]["PREPAY_STATUS"]))
                {
                    //未存檔
                    case "1":
                        BtnCheckOut.ClientEnabled = true;
                        BtnCel.ClientEnabled = true;
                        BtnCelOrd.ClientEnabled = false;
                        BtnPri.ClientEnabled = false;
                        break;
                    //已結帳
                    case "2":
                        txtUNI_TITLE.ReadOnly = true;
                        txtUNI_NO.ReadOnly = true;
                        txtSTART_TYPE.ReadOnly = true;
                        txtID_NO.ReadOnly = true;
                        txtCUST_NAME.ReadOnly = true;
                        txtMSISDN.ReadOnly = true;
                        txtCONTACT_PHONE.ReadOnly = true;
                        txtEMAIL.ReadOnly = true;

                        gvMaster.Settings.ShowFooter = true;
                        gvPAID.Settings.ShowFooter = true;

                        gvMaster.FindChildControl<ASPxButton>("btnNew").ClientEnabled = false;
                        gvMaster.FindChildControl<ASPxButton>("btnDelete").ClientEnabled = false;

                        gvPAID.FindChildControl<ASPxButton>("btnCash").ClientEnabled = false;
                        gvPAID.FindChildControl<ASPxButton>("btnCredit").ClientEnabled = false;
                        gvPAID.FindChildControl<ASPxButton>("btnVisaDebit").ClientEnabled = false;
                        gvPAID.FindChildControl<ASPxButton>("btnOffLineCredit").ClientEnabled = false;
                        gvPAID.FindChildControl<ASPxButton>("btnPayDel").ClientEnabled = false;
                        BtnCheckOut.ClientEnabled = false;
                        BtnCel.ClientEnabled = false;
                        BtnCelOrd.ClientEnabled = true;
                        BtnPri.ClientEnabled = true;
                        break;
                    //已作廢
                    case "3":
                        txtUNI_TITLE.ReadOnly = true;
                        txtUNI_NO.ReadOnly = true;
                        txtSTART_TYPE.ReadOnly = true;
                        txtID_NO.ReadOnly = true;
                        txtCUST_NAME.ReadOnly = true;
                        txtMSISDN.ReadOnly = true;
                        txtCONTACT_PHONE.ReadOnly = true;
                        txtEMAIL.ReadOnly = true;
                        gvMaster.Settings.ShowFooter = true;
                        gvPAID.Settings.ShowFooter = true;
                        gvMaster.FindChildControl<ASPxButton>("btnNew").ClientEnabled = false;
                        gvMaster.FindChildControl<ASPxButton>("btnDelete").ClientEnabled = false;
                        gvPAID.FindChildControl<ASPxButton>("btnCash").ClientEnabled = false;
                        gvPAID.FindChildControl<ASPxButton>("btnCredit").ClientEnabled = false;
                        gvPAID.FindChildControl<ASPxButton>("btnVisaDebit").ClientEnabled = false;
                        gvPAID.FindChildControl<ASPxButton>("btnOffLineCredit").ClientEnabled = false;
                        gvPAID.FindChildControl<ASPxButton>("btnPayDel").ClientEnabled = false;
                        BtnCheckOut.ClientEnabled = false;
                        BtnCel.ClientEnabled = false;
                        BtnCelOrd.ClientEnabled = false;
                        BtnPri.ClientEnabled = false;
                        break;
                    default:
                        break;
                }
            }
        }
        else
        {
            Save_TempTables();
        }
    }

    protected void gvMaster_HtmlRowPrepared(object sender, ASPxGridViewTableRowEventArgs e)
    {
        e.Row.Attributes["canSelect"] = "true";
        if (txtPREPAY_STATUS.Text != "1-未存檔")
        {
            if (e.RowType == GridViewRowType.Data)
            {
                e.Row.Attributes["canSelect"] = "false";
                PopupControl txtPRODNO = gvMaster.FindRowCellTemplateControl(e.VisibleIndex, (GridViewDataColumn)gvMaster.Columns["PRODNO"], "txtPRODNO") as PopupControl;
                ASPxTextBox txtDESCRIPTION = gvMaster.FindRowCellTemplateControl(e.VisibleIndex, (GridViewDataColumn)gvMaster.Columns["DESCRIPTION"], "txtDESCRIPTION") as ASPxTextBox;
                ASPxTextBox txtQUANTITY = gvMaster.FindRowCellTemplateControl(e.VisibleIndex, (GridViewDataColumn)gvMaster.Columns["QUANTITY"], "txtQUANTITY") as ASPxTextBox;
                ASPxTextBox txtREMARK = gvMaster.FindRowCellTemplateControl(e.VisibleIndex, (GridViewDataColumn)gvMaster.Columns["REMARK"], "txtREMARK") as ASPxTextBox;
                txtPRODNO.Enabled = false;
                txtDESCRIPTION.ReadOnly = true;
                txtQUANTITY.ReadOnly = true;
                txtREMARK.ReadOnly = true;
            }
        }
    }

    protected void gvPAID_HtmlRowPrepared(object sender, ASPxGridViewTableRowEventArgs e)
    {
        e.Row.Attributes["canSelect"] = "true";
        if (txtPREPAY_STATUS.Text != "1-未存檔")
        {
            if (e.RowType == GridViewRowType.Data)
            {
                e.Row.Attributes["canSelect"] = "false";
            }
        }
    }

    protected void gvMaster_CommandButtonInitialize(object sender, ASPxGridViewCommandButtonEventArgs e)
    {
        if (txtPREPAY_STATUS.Text != "1-未存檔")
        {
            if (e.ButtonType == ColumnCommandButtonType.SelectCheckbox)
            {
                e.Enabled = false;

            }
        }


    }

    //每當資料變動時要存目前畫面上的資料
    protected void Save_TempTables()
    {
        DataTable dt = TempTables;
        for (int i = 0; i < gvMaster.VisibleRowCount; i++)
        {
            ASPxTextBox txtID = (ASPxTextBox)gvMaster.FindRowCellTemplateControl(i, (GridViewDataColumn)gvMaster.Columns["ID"], "txtID");
            if (txtID != null)
            {
                PopupControl txtPRODNO = gvMaster.FindRowCellTemplateControl(i, (GridViewDataColumn)gvMaster.Columns["PRODNO"], "txtPRODNO") as PopupControl;
                ASPxTextBox txtPRODNAME = gvMaster.FindRowCellTemplateControl(i, (GridViewDataColumn)gvMaster.Columns["PRODNAME"], "txtPRODNAME") as ASPxTextBox;
                ASPxTextBox txtDESCRIPTION = gvMaster.FindRowCellTemplateControl(i, (GridViewDataColumn)gvMaster.Columns["DESCRIPTION"], "txtDESCRIPTION") as ASPxTextBox;
                ASPxTextBox txtUNIT_PRICE = gvMaster.FindRowCellTemplateControl(i, (GridViewDataColumn)gvMaster.Columns["UNIT_PRICE"], "txtUNIT_PRICE") as ASPxTextBox;
                ASPxTextBox txtQUANTITY = gvMaster.FindRowCellTemplateControl(i, (GridViewDataColumn)gvMaster.Columns["QUANTITY"], "txtQUANTITY") as ASPxTextBox;
                ASPxTextBox txtAMOUNT = gvMaster.FindRowCellTemplateControl(i, (GridViewDataColumn)gvMaster.Columns["AMOUNT"], "txtAMOUNT") as ASPxTextBox;
                ASPxTextBox txtREMARK = gvMaster.FindRowCellTemplateControl(i, (GridViewDataColumn)gvMaster.Columns["REMARK"], "txtREMARK") as ASPxTextBox;
                DataRow[] dr = dt.Select("ID='" + txtID.Text + "'");
                if (dr.Length > 0)
                {
                    dr[0]["PRODNO"] = txtPRODNO.Text;
                    dr[0]["PRODNAME"] = txtPRODNAME.Text;
                    dr[0]["DESCRIPTION"] = txtDESCRIPTION.Text;
                    dr[0]["QUANTITY"] = txtQUANTITY.Text;
                    dr[0]["UNIT_PRICE"] = txtUNIT_PRICE.Text;
                    dr[0]["AMOUNT"] = txtAMOUNT.Text;
                    dr[0]["REMARK"] = txtREMARK.Text;
                    dt.AcceptChanges();
                }
            }

        }
    }

    //新增一個付款方式時
    protected void Save_PayTables()
    {
        string[] args = __EVENTARGUMENT.Split(new char[] { ',' });
        DataTable dt = PayTables;
        DataRow dr = dt.NewRow();

        switch (args[0])
        {
            case "CASH":
                dr["ID"] = StringUtil.CStr(Advtek.Utility.GuidNo.getUUID());
                dr["PAID_AMOUNT"] = args[1];
                dr["DESCRIPTION"] = "現金";
                dr["PAID_MODE"] = 1;
                dr["PAID_MODE_NAME"] = "現金";
                break;
            case "CreditCard":
                dr["ID"] = StringUtil.CStr(Advtek.Utility.GuidNo.getUUID());
                dr["PAID_AMOUNT"] = args[1];
                dr["DESCRIPTION"] = "信用卡號:" + args[2] + ",序號:" + args[3] + "調閱編號:" + args[4];
                dr["CREDIT_TYPE"] = 1; //1: 一般2: 分期3: 離線
                dr["PAID_MODE"] = 2;
                dr["PAID_MODE_NAME"] = "信用卡";
                dr["CREDIT_CARD_NO"] = args[2];//信用卡卡號
                break;
            case "VisaDebit":
                dr["ID"] = StringUtil.CStr(Advtek.Utility.GuidNo.getUUID());
                dr["DESCRIPTION"] = "金融卡卡號:" + args[2] + ",授權碼:" + args[3];
                dr["PAID_MODE"] = 6;
                dr["PAID_MODE_NAME"] = "金融卡";
                dr["PAID_AMOUNT"] = args[1];  //金額
                break;
            case "OFF_LINE_CREDIT":
                dr["ID"] = StringUtil.CStr(Advtek.Utility.GuidNo.getUUID());
                dr["PAID_AMOUNT"] = args[1];
                dr["DESCRIPTION"] = "信用卡號:" + args[2] + ",授權碼:" + args[3];
                dr["CREDIT_TYPE"] = 3; //11: 一般2: 分期3: 離線
                dr["PAID_MODE"] = 3;
                dr["PAID_MODE_NAME"] = "離線信用卡";
                dr["CREDIT_CARD_NO"] = args[2];//信用卡卡號
                dr["CREDIT_CARD_AUTH_NO"] = args[3];//授權碼
                break;
            default:
                return;
        }
        dt.Rows.InsertAt(dr, 0);
        dt.AcceptChanges();
        Session["gvPAID"] = dt;
        gvPAID.Settings.ShowFooter = true;
        BindPAIDData();
        //儲存PREPAY_PAID_CACHE
        new PRE01_Facade().Insert_PREPAY_PAID_CACHE(PREPAY_PAID_CACHE(dr));
    }

    //新增一筆產品資料
    protected void btnNew_Click(object sender, EventArgs e)
    {
        gvMaster.PageIndex = 0;
        // Save_TempTables();
        DataTable dt = TempTables;
        DataRow dr = dt.NewRow();
        dr["ID"] = GuidNo.getUUID();
        dr["PRODNO"] = "";
        dr["PRODNAME"] = "";
        dr["DESCRIPTION"] = "";
        dr["QUANTITY"] = 1;
        dr["UNIT_PRICE"] = 0;
        dr["AMOUNT"] = 0;
        dr["REMARK"] = "";
        dt.Rows.InsertAt(dr, 0);    //新增在第一個欄位 
        //dt.Rows.Add(dr);

        dt.AcceptChanges();
        Session["gvMaster"] = dt;
        gvMaster.Settings.ShowFooter = true;

        BindMasterData();

    }

    //計算產品金額
    protected void gvMaster_HtmlFooterCellPrepared(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewTableFooterCellEventArgs e)
    {
        if (e.Column.Caption == "商品料號")
        {
            ASPxLabel txtAR_AMOUNT = (ASPxLabel)gvMaster.FindFooterCellTemplateControl(e.Column, "txtAR_AMOUNT");
            DataTable dt = TempTables;
            int AMOUNT = 0;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                //ASPxTextBox txtAMOUNT = gvMaster.FindRowCellTemplateControl(i, (GridViewDataColumn)gvMaster.Columns["AMOUNT"], "txtAMOUNT") as ASPxTextBox;
                AMOUNT += int.Parse(StringUtil.CStr(dt.Rows[i]["AMOUNT"]));
            }
            if (dt.Rows.Count > 0)
            {
                txtAR_AMOUNT.ClientInstanceName = "txtAR_AMOUNT";
                txtAR_AMOUNT.Text = "應收總金額：" + AMOUNT;
            }
            else
            {
                txtAR_AMOUNT.Text = "";
            }

        }
    }

    //計算訂金金額
    protected void gvPAID_HtmlFooterCellPrepared(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewTableFooterCellEventArgs e)
    {


        if (e.Column.Caption == "付款方式")
        {
            ASPxLabel txtPA_AMOUNT = (ASPxLabel)gvPAID.FindFooterCellTemplateControl(e.Column, "txtPA_AMOUNT");
            DataTable dt = PayTables;
            int AMOUNT = 0;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                //ASPxTextBox txtAMOUNT = gvMaster.FindRowCellTemplateControl(i, (GridViewDataColumn)gvMaster.Columns["AMOUNT"], "txtAMOUNT") as ASPxTextBox;
                AMOUNT += int.Parse(StringUtil.CStr(dt.Rows[i]["PAID_AMOUNT"]));
            }
            if (dt.Rows.Count > 0)
            {
                txtPA_AMOUNT.Text = "訂金金額：" + AMOUNT;
            }
            else
            {
                txtPA_AMOUNT.Text = "";
            }

        }
    }

    private void BindMasterData()
    {
        gvMaster.DataSource = TempTables;
        gvMaster.DataBind();
    }

    private void BindPAIDData()
    {
        gvPAID.DataSource = PayTables;
        gvPAID.DataBind();
    }

    //刪除產品資料
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        Save_TempTables();
        if (Session["gvMaster"] != null)
        {
            DataTable dt = TempTables;
            List<object> keyValues = this.gvMaster.GetSelectedFieldValues(gvMaster.KeyFieldName);
            foreach (string skey in keyValues)
            {
                DataRow[] dr = dt.Select("ID='" + skey + "'");
                if (dr.Length > 0)
                {
                    dt.Rows.Remove(dr[0]);
                    dt.AcceptChanges();
                }
            }
            Session["gvMaster"] = dt;
            BindMasterData();
        }
    }

    //刪除付款方式
    protected void btnPayDel_Click(object sender, EventArgs e)
    {
        if (Session["gvPAID"] != null)
        {
            DataTable dt = PayTables;

            List<object> keyValues = this.gvPAID.GetSelectedFieldValues(gvPAID.KeyFieldName);
            foreach (string skey in keyValues)
            {
                DataRow[] dr = dt.Select("ID='" + skey + "'");
                if (dr.Length > 0)
                {
                    if (StringUtil.CStr(dr[0]["PAID_MODE"]) == "2") //如果是信用卡就要跳出
                    {
                        Session["dkey"] = skey;
                        Session["gvPAID"] = dt;
                        BindPAIDData();
                        ScriptManager.RegisterClientScriptBlock(this, typeof(string), "Pay_DATA_EXIST", "confirmPaidnput('請刷退信用卡?');", true);
                        return;
                    }
                    else
                    {
                        dt.Rows.Remove(dr[0]);
                        dt.AcceptChanges();
                        new PRE01_Facade().Del_PREPAY_PAID_CACHE("ONE", skey, "", "");
                    }

                }
            }
            Session["gvPAID"] = dt;
            BindPAIDData();
        }
    }

    //刪除一筆PREPAY_PAID_CACHE
    protected void btnPayDel1_Click(object sender, EventArgs e)
    {
        DataTable dt = PayTables;
        DataRow[] dr = dt.Select("ID='" + Session["dkey"] + "'");
        if (dr.Length > 0)
        {
            dt.Rows.Remove(dr[0]);
            dt.AcceptChanges();
            new PRE01_Facade().Del_PREPAY_PAID_CACHE("ONE", StringUtil.CStr(Session["dkey"]), "", "");
        }
        Session["dkey"] = null;
        Session["gvPAID"] = dt;
        BindPAIDData();
    }

    //刪除當日PREPAY_PAID_CACHE
    protected void btnPayDelALL_Click(object sender, EventArgs e)
    {
        new PRE01_Facade().Del_PREPAY_PAID_CACHE("ALL", "", logMsg.MACHINE_ID, logMsg.STORENO);
    }

    protected void btnPay_Click(object sender, EventArgs e)
    {
        Save_PayTables();
    }

    protected void BtnCheckOut_Click(object sender, EventArgs e)
    {
        Advtek.Utility.Check_ID subCheck = new Check_ID();
        Advtek.Utility.Check_Internet InCheck = new Check_Internet();
        if (!string.IsNullOrEmpty(txtUNI_NO.Text))
        {
            if (subCheck.Check_TW_INV(txtUNI_NO.Text) != 0)
            {
                ScriptManager.RegisterClientScriptBlock(this, typeof(string), "ERROR_MESSAGE", "alert('統一編號編碼不正確，請重新輸入！!');", true);
                txtUNI_NO.Focus();
                return;
            }
        }

        if (!string.IsNullOrEmpty(txtID_NO.Text))
        {
            if (subCheck.Check_TW_ID(txtID_NO.Text) != 0)
            {
                ScriptManager.RegisterClientScriptBlock(this, typeof(string), "ERROR_MESSAGE", "alert('客戶身分證號編碼不正確，請重新輸入！!');", true);
                txtID_NO.Focus();
                return;
            }
        }

        if (!string.IsNullOrEmpty(txtEMAIL.Text))
        {
            if (InCheck.Check_Email(txtEMAIL.Text) != 0)
            {
                ScriptManager.RegisterClientScriptBlock(this, typeof(string), "ERROR_MESSAGE", "alert('e-Mail編碼不正確，請重新輸入！!');", true);
                txtEMAIL.Focus();
                return;
            }
        }


        if (TempTables.Rows.Count == 0)
        {
            ScriptManager.RegisterClientScriptBlock(this, typeof(string), "ERROR_MESSAGE", "alert('未輸入任何商品料號!');", true);
            return;
        }
        if (PayTables.Rows.Count == 0)
        {
            ScriptManager.RegisterClientScriptBlock(this, typeof(string), "ERROR_MESSAGE", "alert('未輸入任何付款方式!');", true);
            return;
        }

        if (TempTables.Select("PRODNO=''").Count() > 0)
        {
            ScriptManager.RegisterClientScriptBlock(this, typeof(string), "ERROR_MESSAGE", "alert('商品料號不可為空白!');", true);
            return;
        }
        int AR_AMOUNT = 0;
        int PA_AMOUNT = 0;
        for (int i = 0; i < PayTables.Rows.Count; i++)
        {
            PA_AMOUNT += int.Parse(StringUtil.CStr(PayTables.Rows[i]["PAID_AMOUNT"]));
        }

        for (int i = 0; i < TempTables.Rows.Count; i++)
        {
            if (TempTables.Select("PRODNO='" + StringUtil.CStr(TempTables.Rows[i]["PRODNO"]) + "'").Count() > 1)
            {
                ScriptManager.RegisterClientScriptBlock(this, typeof(string), "ERROR_MESSAGE", "alert('商品料號不可重複!');", true);
                return;
            }
            AR_AMOUNT += int.Parse(StringUtil.CStr(TempTables.Rows[i]["AMOUNT"]));
        }

        if (AR_AMOUNT != PA_AMOUNT)
        {
            ScriptManager.RegisterClientScriptBlock(this, typeof(string), "ERROR_MESSAGE", "alert('應收總金額不等於訂金金額!');", true);
            return;
        }
        string UUID = GuidNo.getUUID();
        string INV_UUID = GuidNo.getUUID();

        txtPREPAY_NO.Text = "PR-" + SerialNo.GenNo("PR{0}-{1}").Replace("PR{0}", txtSTORE_NO.Text).Replace("{1}", logMsg.MACHINE_ID); //"L{0}";   
        txtPREPAY_STATUS.Text = "2-已結帳";
        new PRE01_Facade().AddNew_PREPAY(PREPAY_HEAD(UUID, AR_AMOUNT, PA_AMOUNT, "2"), PREPAY_ITEM(UUID), PREPAY_PAID(UUID), INVOICE_HEAD(UUID, INV_UUID, AR_AMOUNT), INVOICE_ITEM(UUID, INV_UUID));

        //Response.Redirect("~/VSS/PRE/PRE01/PRE01.aspx?SRC_TYPE=PRE02&PREPAY_NO=" + txtPREPAY_NO.Text);
        txtUNI_TITLE.ReadOnly = true;
        txtUNI_NO.ReadOnly = true;
        txtSTART_TYPE.ReadOnly = true;
        txtID_NO.ReadOnly = true;
        txtCUST_NAME.ReadOnly = true;
        txtMSISDN.ReadOnly = true;
        txtCONTACT_PHONE.ReadOnly = true;
        txtEMAIL.ReadOnly = true;
        BindMasterData();
        BindPAIDData();
        gvMaster.Enabled = false;
        gvPAID.Enabled = false;
        BtnCheckOut.ClientEnabled = false;
        BtnCel.ClientEnabled = false;
        BtnCelOrd.ClientEnabled = true;
        BtnPri.ClientEnabled = true;

        //列印預收單
        Receipt("INV");
        //列印發票
        INV(UUID);
        //mergePDF();
        ScriptManager.RegisterStartupScript(this, typeof(string), "", "openDialogWindowByEncrypt('../../PRE/PRE01/PREPrint.aspx?Receipt=" + pdflist[0] + "&Invoice=" + pdflist[1] + "&date=' + Date(), 400, 200);", true);

    }

    private DataTable PREPAY_HEAD(string UUID, int AR_AMOUNT, int PA_AMOUNT, string STATUS)
    {
        DataTable PREPAY_HEAD = new DataTable();
        PREPAY_HEAD.TableName = "PREPAY_HEAD";
        PREPAY_HEAD.Columns.Add("ID", typeof(string));  //ID                          
        PREPAY_HEAD.Columns.Add("PREPAY_NO", typeof(string));  //PREPAY_NO            
        PREPAY_HEAD.Columns.Add("VOUCHER_TYPE", typeof(string));  //VOUCHER_TYPE      
        PREPAY_HEAD.Columns.Add("STORE_NO", typeof(string));  //STORE_NO              
        PREPAY_HEAD.Columns.Add("INVOICE_NO", typeof(string));  //INVOICE_NO          
        PREPAY_HEAD.Columns.Add("STORE_NAME", typeof(string));  //STORE_NAME          
        PREPAY_HEAD.Columns.Add("UNI_TITLE", typeof(string));  //UNI_TITLE            
        PREPAY_HEAD.Columns.Add("MACHINE_ID", typeof(string));  //MACHINE_ID          
        PREPAY_HEAD.Columns.Add("UNI_NO", typeof(string));  //UNI_NO                  
        PREPAY_HEAD.Columns.Add("TRADE_DATE", typeof(DateTime));  //TRADE_DATE            
        PREPAY_HEAD.Columns.Add("START_TYPE", typeof(string));  //START_TYPE          
        PREPAY_HEAD.Columns.Add("ID_NO", typeof(string));  //ID_NO                    
        PREPAY_HEAD.Columns.Add("CUST_NAME", typeof(string));  //CUST_NAME            
        PREPAY_HEAD.Columns.Add("MSISDN", typeof(string));  //MSISDN                  
        PREPAY_HEAD.Columns.Add("CONTACT_PHONE", typeof(string));  //CONTACT_PHONE    
        PREPAY_HEAD.Columns.Add("EMAIL", typeof(string));  //EMAIL                    
        PREPAY_HEAD.Columns.Add("PREPAY_STATUS", typeof(string));  //PREPAY_STATUS    
        PREPAY_HEAD.Columns.Add("AR_AMOUNT", typeof(string));  //AR_AMOUNT            
        PREPAY_HEAD.Columns.Add("CREATE_USER", typeof(string));  //CREATE_USER        
        PREPAY_HEAD.Columns.Add("SALE_PERSON", typeof(string));  //SALE_PERSON        
        PREPAY_HEAD.Columns.Add("CREATE_DTM", typeof(DateTime));  //CREATE_DTM            
        PREPAY_HEAD.Columns.Add("MODI_USER", typeof(string));  //MODI_USER            
        PREPAY_HEAD.Columns.Add("MODI_DTM", typeof(DateTime));  //MODI_DTM                
        PREPAY_HEAD.Columns.Add("FRONT_MONEY", typeof(int));  //FRONT_MONEY        




        DataRow PREPAY_HEAD_NewRow = PREPAY_HEAD.NewRow();
        PREPAY_HEAD_NewRow["ID"] = UUID;
        PREPAY_HEAD_NewRow["PREPAY_NO"] = txtPREPAY_NO.Text;
        PREPAY_HEAD_NewRow["STORE_NO"] = txtSTORE_NO.Text;
        PREPAY_HEAD_NewRow["INVOICE_NO"] = txtINVOICE_NO.Text;
        PREPAY_HEAD_NewRow["STORE_NAME"] = new Store_Facade().GetStoreName(txtSTORE_NO.Text);
        PREPAY_HEAD_NewRow["UNI_TITLE"] = txtUNI_TITLE.Text;
        PREPAY_HEAD_NewRow["MACHINE_ID"] = logMsg.MACHINE_ID;
        PREPAY_HEAD_NewRow["UNI_NO"] = txtUNI_NO.Text;
        PREPAY_HEAD_NewRow["TRADE_DATE"] = StringUtil.CStr(this.logMsg.MODI_DTM).Trim();
        PREPAY_HEAD_NewRow["START_TYPE"] = txtSTART_TYPE.Value;
        PREPAY_HEAD_NewRow["ID_NO"] = txtID_NO.Text;
        PREPAY_HEAD_NewRow["CUST_NAME"] = txtCUST_NAME.Text;
        PREPAY_HEAD_NewRow["MSISDN"] = txtMSISDN.Text;
        PREPAY_HEAD_NewRow["CONTACT_PHONE"] = txtCONTACT_PHONE.Text;
        PREPAY_HEAD_NewRow["EMAIL"] = txtEMAIL.Text;
        PREPAY_HEAD_NewRow["PREPAY_STATUS"] = STATUS;
        PREPAY_HEAD_NewRow["AR_AMOUNT"] = AR_AMOUNT;
        PREPAY_HEAD_NewRow["CREATE_USER"] = StringUtil.CStr(this.logMsg.MODI_USER).Trim();
        PREPAY_HEAD_NewRow["SALE_PERSON"] = StringUtil.CStr(this.logMsg.MODI_USER).Trim();
        PREPAY_HEAD_NewRow["CREATE_DTM"] = StringUtil.CStr(this.logMsg.MODI_DTM).Trim();
        PREPAY_HEAD_NewRow["MODI_USER"] = StringUtil.CStr(this.logMsg.MODI_USER).Trim();
        PREPAY_HEAD_NewRow["MODI_DTM"] = StringUtil.CStr(this.logMsg.MODI_DTM).Trim();
        PREPAY_HEAD_NewRow["FRONT_MONEY"] = PA_AMOUNT;
        PREPAY_HEAD.Rows.Add(PREPAY_HEAD_NewRow);
        return PREPAY_HEAD;
    }

    private DataTable PREPAY_ITEM(string UUID)
    {
        DataTable PREPAY_ITEM = new DataTable();

        PREPAY_ITEM.TableName = "PREPAY_ITEM";


        PREPAY_ITEM.Columns.Add("ID", typeof(string));  //ID               
        PREPAY_ITEM.Columns.Add("PRODNO", typeof(string));  //PRODNO           
        PREPAY_ITEM.Columns.Add("DESCRIPTION", typeof(string));  //DESCRIPTION      
        PREPAY_ITEM.Columns.Add("UNIT_PRICE", typeof(int));  //UNIT_PRICE          
        PREPAY_ITEM.Columns.Add("QUANTITY", typeof(int));  //QUANTITY            
        PREPAY_ITEM.Columns.Add("AMOUNT", typeof(int));  //AMOUNT              
        PREPAY_ITEM.Columns.Add("REMARK", typeof(string));  //REMARK           
        PREPAY_ITEM.Columns.Add("CREATE_USER", typeof(string));  //CREATE_USER      
        PREPAY_ITEM.Columns.Add("CREATE_DTM", typeof(DateTime));  //CREATE_DTM       
        PREPAY_ITEM.Columns.Add("MODI_USER", typeof(string));  //MODI_USER        
        PREPAY_ITEM.Columns.Add("MODI_DTM", typeof(DateTime));  //MODI_DTM         
        PREPAY_ITEM.Columns.Add("PREPAY_HEAD_ID", typeof(string));  //PREPAY_HEAD_ID   



        foreach (DataRow dr in TempTables.Rows)
        {
            DataRow PREPAY_ITEM_NewRow = PREPAY_ITEM.NewRow();
            PREPAY_ITEM_NewRow["ID"] = GuidNo.getUUID();
            PREPAY_ITEM_NewRow["PRODNO"] = dr["PRODNO"];
            PREPAY_ITEM_NewRow["DESCRIPTION"] = dr["DESCRIPTION"];
            PREPAY_ITEM_NewRow["UNIT_PRICE"] = dr["UNIT_PRICE"];
            PREPAY_ITEM_NewRow["QUANTITY"] = dr["QUANTITY"];
            PREPAY_ITEM_NewRow["AMOUNT"] = dr["AMOUNT"];
            PREPAY_ITEM_NewRow["REMARK"] = dr["REMARK"];
            PREPAY_ITEM_NewRow["CREATE_USER"] = StringUtil.CStr(this.logMsg.MODI_USER).Trim();
            PREPAY_ITEM_NewRow["CREATE_DTM"] = StringUtil.CStr(this.logMsg.MODI_DTM).Trim();
            PREPAY_ITEM_NewRow["MODI_USER"] = StringUtil.CStr(this.logMsg.MODI_USER).Trim();
            PREPAY_ITEM_NewRow["MODI_DTM"] = StringUtil.CStr(this.logMsg.MODI_DTM).Trim();
            PREPAY_ITEM_NewRow["PREPAY_HEAD_ID"] = UUID;

            PREPAY_ITEM.Rows.Add(PREPAY_ITEM_NewRow);
        }
        return PREPAY_ITEM;
    }

    private DataTable PREPAY_PAID_CACHE(DataRow dr)
    {
        DataTable PREPAY_PAID_CACHE = new DataTable();
        PREPAY_PAID_CACHE.TableName = "PREPAY_PAID_CACHE";
        PREPAY_PAID_CACHE.Columns.Add("ID", typeof(string));  //ID                     
        PREPAY_PAID_CACHE.Columns.Add("PAID_MODE", typeof(int));  //PAID_MODE              
        PREPAY_PAID_CACHE.Columns.Add("PAID_AMOUNT", typeof(int));  //PAID_AMOUNT            
        PREPAY_PAID_CACHE.Columns.Add("DESCRIPTION", typeof(string));  //DESCRIPTION            
        PREPAY_PAID_CACHE.Columns.Add("CREDIT_TYPE", typeof(int));  //CREDIT_TYPE            
        PREPAY_PAID_CACHE.Columns.Add("CREATE_USER", typeof(string));  //CREATE_USER            
        PREPAY_PAID_CACHE.Columns.Add("CREDIT_CARD_NO", typeof(string));  //CREDIT_CARD_NO         
        PREPAY_PAID_CACHE.Columns.Add("CREATE_DTM", typeof(DateTime));  //CREATE_DTM             
        PREPAY_PAID_CACHE.Columns.Add("CREDIT_CARD_AUTH_NO", typeof(string));  //CREDIT_CARD_AUTH_NO    
        PREPAY_PAID_CACHE.Columns.Add("CREDIT_CARD_TYPE_ID", typeof(string));  //CREDIT_CARD_TYPE_ID    
        PREPAY_PAID_CACHE.Columns.Add("MODI_USER", typeof(string));  //MODI_USER              
        PREPAY_PAID_CACHE.Columns.Add("CREDID_CARD_TYPE_NAME", typeof(string));  //CREDID_CARD_TYPE_NAME  
        PREPAY_PAID_CACHE.Columns.Add("MODI_DTM", typeof(DateTime));  //MODI_DTM         
        PREPAY_PAID_CACHE.Columns.Add("PREPAY_HEAD_ID", typeof(string));  //PREPAY_HEAD_ID    
        PREPAY_PAID_CACHE.Columns.Add("STORE_NO", typeof(string));  //STORE_NO        
        PREPAY_PAID_CACHE.Columns.Add("MACHINE_ID", typeof(string));  //MACHINE_ID        
        PREPAY_PAID_CACHE.Columns.Add("PREPAY_PAID_ID", typeof(string));  //MACHINE_ID       


        DataRow PREPAY_PAID_CACHE_NewRow = PREPAY_PAID_CACHE.NewRow();
        PREPAY_PAID_CACHE_NewRow["ID"] = GuidNo.getUUID();
        PREPAY_PAID_CACHE_NewRow["PAID_MODE"] = dr["PAID_MODE"];
        PREPAY_PAID_CACHE_NewRow["PAID_AMOUNT"] = dr["PAID_AMOUNT"];
        PREPAY_PAID_CACHE_NewRow["DESCRIPTION"] = dr["DESCRIPTION"];
        PREPAY_PAID_CACHE_NewRow["CREDIT_TYPE"] = dr["CREDIT_TYPE"];
        PREPAY_PAID_CACHE_NewRow["CREDIT_CARD_NO"] = dr["CREDIT_CARD_NO"];
        PREPAY_PAID_CACHE_NewRow["CREDIT_CARD_AUTH_NO"] = dr["CREDIT_CARD_AUTH_NO"];
        PREPAY_PAID_CACHE_NewRow["CREDIT_CARD_TYPE_ID"] = dr["CREDIT_CARD_TYPE_ID"];
        PREPAY_PAID_CACHE_NewRow["CREDID_CARD_TYPE_NAME"] = dr["CREDID_CARD_TYPE_NAME"];
        PREPAY_PAID_CACHE_NewRow["PREPAY_HEAD_ID"] = "";
        PREPAY_PAID_CACHE_NewRow["CREATE_USER"] = StringUtil.CStr(this.logMsg.MODI_USER).Trim();
        PREPAY_PAID_CACHE_NewRow["CREATE_DTM"] = StringUtil.CStr(this.logMsg.MODI_DTM).Trim();
        PREPAY_PAID_CACHE_NewRow["MODI_USER"] = StringUtil.CStr(this.logMsg.MODI_USER).Trim();
        PREPAY_PAID_CACHE_NewRow["MODI_DTM"] = StringUtil.CStr(this.logMsg.MODI_DTM).Trim();
        PREPAY_PAID_CACHE_NewRow["STORE_NO"] = txtSTORE_NO.Text;
        PREPAY_PAID_CACHE_NewRow["MACHINE_ID"] = logMsg.MACHINE_ID;
        PREPAY_PAID_CACHE_NewRow["PREPAY_PAID_ID"] = dr["ID"];

        PREPAY_PAID_CACHE.Rows.Add(PREPAY_PAID_CACHE_NewRow);

        return PREPAY_PAID_CACHE;
    }

    private DataTable PREPAY_PAID(string UUID)
    {
        DataTable PREPAY_PAID = new DataTable();
        PREPAY_PAID.TableName = "PREPAY_PAID";
        PREPAY_PAID.Columns.Add("ID", typeof(string));  //ID                     
        PREPAY_PAID.Columns.Add("PAID_MODE", typeof(int));  //PAID_MODE              
        PREPAY_PAID.Columns.Add("PAID_AMOUNT", typeof(int));  //PAID_AMOUNT            
        PREPAY_PAID.Columns.Add("DESCRIPTION", typeof(string));  //DESCRIPTION            
        PREPAY_PAID.Columns.Add("CREDIT_TYPE", typeof(int));  //CREDIT_TYPE            
        PREPAY_PAID.Columns.Add("CREATE_USER", typeof(string));  //CREATE_USER            
        PREPAY_PAID.Columns.Add("CREDIT_CARD_NO", typeof(string));  //CREDIT_CARD_NO         
        PREPAY_PAID.Columns.Add("CREATE_DTM", typeof(DateTime));  //CREATE_DTM             
        PREPAY_PAID.Columns.Add("CREDIT_CARD_AUTH_NO", typeof(string));  //CREDIT_CARD_AUTH_NO    
        PREPAY_PAID.Columns.Add("CREDIT_CARD_TYPE_ID", typeof(string));  //CREDIT_CARD_TYPE_ID    
        PREPAY_PAID.Columns.Add("MODI_USER", typeof(string));  //MODI_USER              
        PREPAY_PAID.Columns.Add("CREDID_CARD_TYPE_NAME", typeof(string));  //CREDID_CARD_TYPE_NAME  
        PREPAY_PAID.Columns.Add("MODI_DTM", typeof(DateTime));  //MODI_DTM         
        PREPAY_PAID.Columns.Add("PREPAY_HEAD_ID", typeof(string));  //PREPAY_HEAD_ID    

        foreach (DataRow dr in PayTables.Rows)
        {
            DataRow PREPAY_PAID_NewRow = PREPAY_PAID.NewRow();
            PREPAY_PAID_NewRow["ID"] = dr["ID"];
            PREPAY_PAID_NewRow["PAID_MODE"] = dr["PAID_MODE"];
            PREPAY_PAID_NewRow["PAID_AMOUNT"] = dr["PAID_AMOUNT"];
            PREPAY_PAID_NewRow["DESCRIPTION"] = dr["DESCRIPTION"];
            PREPAY_PAID_NewRow["CREDIT_TYPE"] = dr["CREDIT_TYPE"];
            PREPAY_PAID_NewRow["CREDIT_CARD_NO"] = dr["CREDIT_CARD_NO"];
            PREPAY_PAID_NewRow["CREDIT_CARD_AUTH_NO"] = dr["CREDIT_CARD_AUTH_NO"];
            PREPAY_PAID_NewRow["CREDIT_CARD_TYPE_ID"] = dr["CREDIT_CARD_TYPE_ID"];
            PREPAY_PAID_NewRow["CREDID_CARD_TYPE_NAME"] = dr["CREDID_CARD_TYPE_NAME"];
            PREPAY_PAID_NewRow["PREPAY_HEAD_ID"] = UUID;
            PREPAY_PAID_NewRow["CREATE_USER"] = StringUtil.CStr(this.logMsg.MODI_USER);
            PREPAY_PAID_NewRow["CREATE_DTM"] = StringUtil.CStr(this.logMsg.MODI_DTM);
            PREPAY_PAID_NewRow["MODI_USER"] = StringUtil.CStr(this.logMsg.MODI_USER);
            PREPAY_PAID_NewRow["MODI_DTM"] = StringUtil.CStr(this.logMsg.MODI_DTM);
            PREPAY_PAID.Rows.Add(PREPAY_PAID_NewRow);
        }
        return PREPAY_PAID;
    }

    public DataTable INVOICE_HEAD(string UUID, string INV_UUID, int AR_AMOUNT)
    {
        DataTable INVOICE_HEAD = new DataTable();
        INVOICE_HEAD.TableName = "INVOICE_HEAD";
        INVOICE_HEAD.Columns.Add("ID", typeof(string));  //ID                     
        INVOICE_HEAD.Columns.Add("UNI_NO", typeof(string));  //UNI_NO              
        INVOICE_HEAD.Columns.Add("TOTAL_AMOUNT", typeof(int));  //TOTAL_AMOUNT            
        INVOICE_HEAD.Columns.Add("TAX", typeof(int));  //TAX            
        INVOICE_HEAD.Columns.Add("STORE_NO", typeof(string));  //STORE_NO            
        INVOICE_HEAD.Columns.Add("POSUUID_MASTER", typeof(string));  //POSUUID_MASTER            
        INVOICE_HEAD.Columns.Add("BUYER", typeof(string));  //BUYER         
        INVOICE_HEAD.Columns.Add("TAX_TYPE", typeof(string));  //TAX_TYPE             
        INVOICE_HEAD.Columns.Add("INVOICE_DATE", typeof(DateTime));  //INVOICE_DATE    
        INVOICE_HEAD.Columns.Add("SALE_AMOUNT", typeof(int));  //SALE_AMOUNT    
        INVOICE_HEAD.Columns.Add("INVOICE_NO", typeof(string));  //INVOICE_NO   

        INVOICE_HEAD.Columns.Add("CREATE_DATE", typeof(DateTime));  //CREATE_DATE         
        INVOICE_HEAD.Columns.Add("CREATE_USER", typeof(string));  //CREATE_USER             
        INVOICE_HEAD.Columns.Add("MODI_DTM", typeof(DateTime));  //MODI_DTM    
        INVOICE_HEAD.Columns.Add("MODI_USER", typeof(string));  //MODI_USER    


        foreach (DataRow dr in PayTables.Rows)
        {
            DataRow INVOICE_HEAD_NewRow = INVOICE_HEAD.NewRow();
            INVOICE_HEAD_NewRow["ID"] = INV_UUID;
            INVOICE_HEAD_NewRow["UNI_NO"] = txtUNI_NO.Text;
            INVOICE_HEAD_NewRow["TOTAL_AMOUNT"] = AR_AMOUNT;
            INVOICE_HEAD_NewRow["TAX"] = AR_AMOUNT - (AR_AMOUNT / 1.05);
            INVOICE_HEAD_NewRow["STORE_NO"] = txtSTORE_NO.Text;
            INVOICE_HEAD_NewRow["POSUUID_MASTER"] = UUID;
            INVOICE_HEAD_NewRow["BUYER"] = txtUNI_TITLE.Text;
            INVOICE_HEAD_NewRow["TAX_TYPE"] = "1";
            INVOICE_HEAD_NewRow["INVOICE_DATE"] = DateTime.Now;
            INVOICE_HEAD_NewRow["SALE_AMOUNT"] = AR_AMOUNT / 1.05;
            INVOICE_HEAD_NewRow["INVOICE_NO"] = "";

            INVOICE_HEAD_NewRow["CREATE_USER"] = StringUtil.CStr(this.logMsg.MODI_USER);
            INVOICE_HEAD_NewRow["CREATE_DATE"] = StringUtil.CStr(this.logMsg.MODI_DTM);
            INVOICE_HEAD_NewRow["MODI_USER"] = StringUtil.CStr(this.logMsg.MODI_USER);
            INVOICE_HEAD_NewRow["MODI_DTM"] = StringUtil.CStr(this.logMsg.MODI_DTM);
            INVOICE_HEAD.Rows.Add(INVOICE_HEAD_NewRow);
        }
        return INVOICE_HEAD;
    }

    public DataTable INVOICE_ITEM(string UUID, string INV_UUID)
    {
        DataTable INVOICE_ITEM = new DataTable();

        INVOICE_ITEM.TableName = "INVOICE_ITEM";


        INVOICE_ITEM.Columns.Add("ID", typeof(string));  //ID               
        INVOICE_ITEM.Columns.Add("INVOICE_HEAD_ID", typeof(string));  //INVOICE_HEAD_ID           
        INVOICE_ITEM.Columns.Add("PRICE", typeof(int));  //PRICE      
        INVOICE_ITEM.Columns.Add("PRODNO", typeof(string));  //PRODNO          
        INVOICE_ITEM.Columns.Add("PROD_NAME", typeof(string));  //PROD_NAME            
        INVOICE_ITEM.Columns.Add("QUANTITY", typeof(int));  //QUANTITY              
        INVOICE_ITEM.Columns.Add("TAX", typeof(int));  //TAX           
        INVOICE_ITEM.Columns.Add("AMOUNT", typeof(int));  //AMOUNT      
        INVOICE_ITEM.Columns.Add("TOTAL_AMOUNT", typeof(int));  //TOTAL_AMOUNT       
        INVOICE_ITEM.Columns.Add("SALE_ITEM_ID", typeof(string));  //SALE_ITEM_ID        
        INVOICE_ITEM.Columns.Add("CREATE_DATE", typeof(DateTime));  //CREATE_DATE         
        INVOICE_ITEM.Columns.Add("CREATE_USER", typeof(string));  //CREATE_USER             
        INVOICE_ITEM.Columns.Add("MODI_DTM", typeof(DateTime));  //MODI_DTM    
        INVOICE_ITEM.Columns.Add("MODI_USER", typeof(string));  //MODI_USER    


        foreach (DataRow dr in TempTables.Rows)
        {
            DataRow INVOICE_ITEM_NewRow = INVOICE_ITEM.NewRow();
            INVOICE_ITEM_NewRow["ID"] = GuidNo.getUUID();
            INVOICE_ITEM_NewRow["INVOICE_HEAD_ID"] = INV_UUID;
            INVOICE_ITEM_NewRow["PRICE"] = dr["UNIT_PRICE"];
            INVOICE_ITEM_NewRow["PRODNO"] = dr["PRODNO"];
            INVOICE_ITEM_NewRow["PROD_NAME"] = dr["PRODNAME"];
            INVOICE_ITEM_NewRow["QUANTITY"] = dr["QUANTITY"];
            INVOICE_ITEM_NewRow["SALE_ITEM_ID"] = UUID;

            INVOICE_ITEM_NewRow["AMOUNT"] = (int.Parse(StringUtil.CStr(dr["AMOUNT"])) / 1.05);
            INVOICE_ITEM_NewRow["TOTAL_AMOUNT"] = dr["AMOUNT"];
            INVOICE_ITEM_NewRow["TAX"] = int.Parse(StringUtil.CStr(dr["AMOUNT"])) - (int.Parse(StringUtil.CStr(dr["AMOUNT"])) / 1.05);

            INVOICE_ITEM_NewRow["CREATE_USER"] = StringUtil.CStr(this.logMsg.MODI_USER).Trim();
            INVOICE_ITEM_NewRow["CREATE_DATE"] = StringUtil.CStr(this.logMsg.MODI_DTM).Trim();
            INVOICE_ITEM_NewRow["MODI_USER"] = StringUtil.CStr(this.logMsg.MODI_USER).Trim();
            INVOICE_ITEM_NewRow["MODI_DTM"] = StringUtil.CStr(this.logMsg.MODI_DTM).Trim();


            INVOICE_ITEM.Rows.Add(INVOICE_ITEM_NewRow);
        }
        return INVOICE_ITEM;
    }

    //取消資料
    protected void BtnCel_Click(object sender, EventArgs e)
    {
        DataTable dt = PayTables;

        DataRow[] dr = dt.Select("PAID_MODE='2'");  //如果是信用卡資料
        if (dr.Length > 0)
        {
            ScriptManager.RegisterClientScriptBlock(this, typeof(string), "Pay_DATA_EXIST", "confirmPaidBtnCel('請刷退信用卡?');", true);
            return;
        }
        else
        {
            //刪除該筆PREPAY_PAID_CACHE
            new PRE01_Facade().Del_PREPAY_PAID_CACHE(dt);
        }
        Response.Redirect("~/VSS/PRE/PRE01/PRE01.aspx");
    }

    //刪除該筆PREPAY_PAID_CACHE
    protected void BtnCelALL_Click(object sender, EventArgs e)
    {
        new PRE01_Facade().Del_PREPAY_PAID_CACHE(PayTables);
        Response.Redirect("~/VSS/PRE/PRE01/PRE01.aspx");
    }

    protected void BtnCelOrd_Click(object sender, EventArgs e)
    {
        new PRE01_Facade().Update_PREPAY(txtUUID.Text);
        Response.Redirect("~/VSS/PRE/PRE01/PRE01.aspx?SRC_TYPE=PRE02&PREPAY_NO=" + txtPREPAY_NO.Text);
    }

    protected void gvMaster_PageIndexChanged(object sender, EventArgs e)
    {
        BindMasterData();
    }

    protected void gvPAID_PageIndexChanged(object sender, EventArgs e)
    {
        BindPAIDData();
    }

    protected void BtnPri_Click(object sender, EventArgs e)
    {
        //列印預收單
        Receipt("");
    }

    //要合併的pdf
    public string[] pdflist = new string[2];
    private void mergePDF()
    {
        string filePath = new PRE01_Facade().getUploadPath("", "");
        string fileName = mergePDFFiles(pdflist);
        ProcessRequest(fileName, filePath);
    }

    /// <summary> 合併PDF檔(集合) </summary> 
    /// <param name="fileList">欲合併PDF檔之集合(一筆以上)</param>
    /// <param name="outMergeFile">合併後的檔名</param> 
    private string mergePDFFiles(string[] fileList)
    {
        PdfReader reader;
        Document document = new Document();
        string filename = StringUtil.CStr(Guid.NewGuid()) + ".pdf";
        FileStream stream = new FileStream(Path.Combine(Server.MapPath("~/Downloads"), filename), FileMode.Create);
        PdfWriter writer = PdfWriter.GetInstance(document, stream);

        document.Open();
        // 加入自動列印指令碼
        writer.AddJavaScript("this.print(false);", true);
        PdfContentByte cb = writer.DirectContent;
        PdfImportedPage newPage;
        for (int i = 0; i < fileList.Length; i++)
        {
            reader = new PdfReader(Server.MapPath(fileList[i]));
            int iPageNum = reader.NumberOfPages;
            for (int j = 1; j <= iPageNum; j++)
            {
                document.NewPage();
                newPage = writer.GetImportedPage(reader, j);
                cb.AddTemplate(newPage, 0, 0);
            }
        }
        document.Close();
        return filename;
    }

    public void Receipt(string PType)
    {
        PriPRE01 myPriPRE01 = new PriPRE01();
        string fileName = myPriPRE01.generateReceipt(StringUtil.CStr(txtPREPAY_NO.Text));
        string filePath = new PRE01_Facade().getUploadPath("", "");
        if (PType == "INV")
        {
            pdflist[0] = Request.ApplicationPath + filePath + "/" + fileName;
        }
        else
        {
            ProcessRequest(fileName, filePath);
        }
    }

    public void INV(string UUID)
    {
        Receipt myReceipt = new Receipt();
        string fileName = myReceipt.generateReceipt(UUID);
        string filePath = new PRE01_Facade().getUploadPath("INV", UUID);
        pdflist[1] = Request.ApplicationPath + filePath + "/" + fileName;
        //ProcessRequest(fileName, filePath);
    }

    public void ProcessRequest(string filename, string filePath)
    {
        ScriptManager.RegisterClientScriptBlock(this,
                                               this.GetType(),
                                               "test",
                                               "document.getElementById('" + fDownload.ClientID + "').src='" + Request.ApplicationPath + filePath + "/" + filename + "';",
                                               true);
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

    //ajax 呼當前網頁的方式
    [System.Web.Services.WebMethod()]
    [System.Web.Script.Services.ScriptMethod()]
    static public string getProductInfo(string PRODUCT_NO, string STORE_NO)
    {
        string strInfo = "";
        if (!string.IsNullOrEmpty(PRODUCT_NO))
        {
            string STOCK = Common_PageHelper.GetGoodLOCUUID();
            DataTable dt = new Product_Facade().Query_ProductInfo(PRODUCT_NO, STORE_NO, STOCK);
            if (dt.Rows.Count > 0)
            {
                DataRow dr = dt.Rows[0];
                strInfo = StringUtil.CStr(dr["PRODNO"]) + ";" + StringUtil.CStr(dr["PRODNAME"]) + ";" + StringUtil.CStr(dr["PRICE"]);
            }
        }
        return strInfo;
    }

}
