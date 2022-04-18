using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Net;
using System.Text;
using Advtek.Utility;
using DevExpress.Web.ASPxGridView;
using DevExpress.Web.ASPxEditors;
using FET.POS.Model.Facade.FacadeImpl;
using FET.POS.Model.DTO;
using System.Data.OracleClient;
using System.Web.UI.HtmlControls;
using DevExpress.Web.ASPxPopupControl;
using FET.POS.Model.Common;
using System.Collections.Specialized;

public partial class VSS_SAL_SA11 : BasePage
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
        public void SaveSale_Head_Temp(VSS_SAL_SA11 p)
        {
            foreach (DataRow dr in Head.Rows)
            {
                dr["INVOICE_NO"] = p.lbINVOICE_NO.Text;
                dr["INVOICE_TYPE"] = p.rbINVOICE_TYPE.Value;
                if (p.txtInv_Date.Text != "" && DateUtil.IsDate(p.txtInv_Date.Text))
                    dr["INVOICE_DATE"] = Convert.ToDateTime(p.txtInv_Date.Text).ToString("yyyy/MM/dd");
                else
                    dr["INVOICE_DATE"] = Convert.ToDateTime(DateTime.Today).ToString("yyyy/MM/dd");
                dr["UNI_NO"] = p.txtUNI_NO.Text;
                dr["UNI_TITLE"] = p.txtUNI_TITLE.Text;
                dr["TRADE_DATE"] = p.lbTran_Date.Text;//交易日期
                dr["SALE_NO"] = p.lbSALE_NO.Text;//交易序號
                int saleTotalAmt = 0;
                if (dr["SALE_TOTAL_AMOUNT"] != null && StringUtil.CStr(dr["SALE_TOTAL_AMOUNT"]) != ""
                    && NumberUtil.IsNumeric(StringUtil.CStr(dr["SALE_TOTAL_AMOUNT"])))
                    saleTotalAmt = int.Parse(StringUtil.CStr(dr["SALE_TOTAL_AMOUNT"]));
                if (saleTotalAmt <= 0)
                {
                    //金額小於等於0,開立收據
                    dr["INVOICE_TYPE"] = "0"; //無
                }
                else
                {
                    dr["INVOICE_TYPE"] = p.rbINVOICE_TYPE.Value;
                }
                dr["REMARK"] = p.txtREMARK.Text;
            }
            Head.AcceptChanges();
        }

        public void SaleHeadDataBind(VSS_SAL_SA11 p)
        {
            if (Head.Rows.Count == 0)
            {
                DataRow Newdr = Head.NewRow();
                try
                {
                    Newdr["TRADE_DATE"] = Advtek.Utility.OracleDBUtil.WorkDay(p.logMsg.STORENO);
                }
                catch //(Exception ex)
                {
                    Newdr["TRADE_DATE"] = DateTime.Today.ToString("yyyy/MM/dd");
                }
                Newdr["INVOICE_DATE"] = DateTime.Today.ToString("yyyy/MM/dd");
                Newdr["POSUUID_MASTER"] = StringUtil.CStr(Advtek.Utility.GuidNo.getUUID());
                Newdr["SALE_TOTAL_AMOUNT"] = 0;
                Newdr["SALE_BEFORE_TAX"] = 0;
                Newdr["SALE_TAX"] = 0;
                Newdr["DISCOUNT_TOTAL_AMOUNT"] = 0;
                Newdr["DISCOUNT_BEFORE_TAX"] = 0;
                Newdr["DISCOUNT_TAX"] = 0;
                Newdr["SALE_PERSON"] = p.logMsg.OPERATOR;
                Newdr["SALE_TYPE"] = "1";
                Newdr["SALE_STATUS"] = "8";  //交易補登未結
                Newdr["VOUCHER_TYPE"] = "2"; //手開發票
                Head.Rows.Add(Newdr);
                Head.AcceptChanges();
            }

            DataRow dr = Head.Rows[0];
            if (dr["TRADE_DATE"] != null && StringUtil.CStr(dr["TRADE_DATE"]) != "" && DateUtil.IsDate(StringUtil.CStr(dr["TRADE_DATE"])))
                p.lbTran_Date.Text = Convert.ToDateTime(dr["TRADE_DATE"]).ToString("yyyy/MM/dd");
            if (dr["INVOICE_DATE"] != null && dr["INVOICE_DATE"] != DBNull.Value && StringUtil.CStr(dr["INVOICE_DATE"]) != ""
                && DateUtil.IsDate(StringUtil.CStr(dr["INVOICE_DATE"])))
                p.txtInv_Date.Text = Convert.ToDateTime(dr["INVOICE_DATE"]).ToString("yyyy/MM/dd");
            else
                p.txtInv_Date.Text = Convert.ToDateTime(DateTime.Today).ToString("yyyy/MM/dd");
            p.lbINVOICE_NO.Text = StringUtil.CStr(dr["INVOICE_NO"]);
            if (StringUtil.CStr(Head.Rows[0]["SALE_TYPE"]) == "2")
            {   //代收交易,預設為不開發票開收據
                p.rbINVOICE_TYPE.Value = "0";
                dr["INVOICE_TYPE"] = "0";
                p.txtVOUCHER_TYPE.Text = "收據";
                p.lbINVOICE_NO.ClientEnabled = false;
                p.txtUNI_NO.ClientEnabled = false;
                p.txtUNI_TITLE.ClientEnabled = false;
                p.txtInv_Date.ClientEnabled = false;
            }
            else
            {
                if (StringUtil.CStr(dr["INVOICE_TYPE"]) == "")
                {
                    if (StringUtil.CStr(dr["INVOICE_NO"]) == "")
                    {
                        p.rbINVOICE_TYPE.Value = "0";
                        dr["INVOICE_TYPE"] = "0";
                        p.txtVOUCHER_TYPE.Text = "收據";
                    }
                    else
                    {
                        p.rbINVOICE_TYPE.Value = "03";
                        dr["INVOICE_TYPE"] = "03";
                        p.txtVOUCHER_TYPE.Text = "二聯式發票";
                    }
                }
                else
                {
                    p.rbINVOICE_TYPE.Value = StringUtil.CStr(dr["INVOICE_TYPE"]);
                    if (StringUtil.CStr(dr["INVOICE_TYPE"]) == "03")
                        p.txtVOUCHER_TYPE.Text = "二聯式發票";
                    else if (StringUtil.CStr(dr["INVOICE_TYPE"]) == "04")
                        p.txtVOUCHER_TYPE.Text = "三聯式發票";
                    else
                        p.txtVOUCHER_TYPE.Text = "收據";
                }
            }
            p.lbSALE_NO.Text = StringUtil.CStr(dr["SALE_NO"]);
            p.txtUNI_NO.Text = StringUtil.CStr(dr["UNI_NO"]);
            p.txtUNI_TITLE.Text = StringUtil.CStr(dr["UNI_TITLE"]);
            p.txtREMARK.Text = StringUtil.CStr(dr["REMARK"]);
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
            //string VOUCHER_TYPE_NAME = "";
            DataTable dtInvoiceType = p.Facade.getINVOICE_TYPE();

            //switch (StringUtil.CStr(dr["VOUCHER_TYPE"]))
            //{
            //    case "1":
            //    case "2":
            //        VOUCHER_TYPE_NAME = "發票";
            //        if (dtInvoiceType != null && dtInvoiceType.Rows.Count > 0)
            //        {
            //            VOUCHER_TYPE_NAME = StringUtil.CStr(dtInvoiceType.Rows[0]["INVOICE_TYPE_NAME"]);
            //            DataRow[] drs = dtInvoiceType.Select(" INVOICE_TYPE_ID = '" + StringUtil.CStr(dr["INVOICE_TYPE"]).Replace("'", "-") + "' ");
            //            if (drs != null && drs.Length > 0)
            //                VOUCHER_TYPE_NAME = StringUtil.CStr(drs[0]["INVOICE_TYPE_NAME"]);
            //        }
            //        break;
            //    case "3": VOUCHER_TYPE_NAME = "收據"; break;//只有貨品往來時才開收據
            //}
            //p.lbVOUCHER_TYPE.Text = VOUCHER_TYPE_NAME;
            Employee_Facade empFacade = new Employee_Facade();
            p.lbMODI_USER.Text = p.logMsg.MODI_USER + " " + empFacade.GetEmpName(p.logMsg.MODI_USER);
            p.lbMODI_DTM.Text = p.logMsg.MODI_DTM.ToString("yyyy/MM/dd  HH:mm:ss");
            if (p._SRC_TYPE == "SAL02")
            {
                p.lbINVOICE_NO.ClientEnabled = false;
                p.rbINVOICE_TYPE.ClientEnabled = false;
                p.txtUNI_NO.ClientEnabled = false;
                p.txtUNI_TITLE.ClientEnabled = false;
                p.txtInv_Date.ClientEnabled = false;
                p.txtREMARK.ClientEnabled = false;
            }
        }

        public void SaveSale_Detail_Temp(ASPxGridView gv)
        {
            // PopupControl noPRODNO = null;
            //就算是新增單品,也要設定該品項的POSUUID_DETAIL,一張單為同一個ID
            string posuuid_detail = "";
            DataRow[] drs = Detail.Select("ITEM_TYPE IN ('1','3','4','7','8','9','10','13','14') ");
            if (drs != null && drs.Length != 0 && drs[0]["POSUUID_DETAIL"] != null && StringUtil.CStr(drs[0]["POSUUID_DETAIL"]) != "")
            {
                posuuid_detail = StringUtil.CStr(drs[0]["POSUUID_DETAIL"]);
            }
            else if (drs == null || drs.Length == 0)
            {
                drs = Detail.Select("ITEM_TYPE = '2' AND SOURCE_TYPE = '11' ");
                if (drs != null && drs.Length != 0 && drs[0]["POSUUID_DETAIL"] != null && StringUtil.CStr(drs[0]["POSUUID_DETAIL"]) != "")
                    posuuid_detail = StringUtil.CStr(drs[0]["POSUUID_DETAIL"]);
            }
            if (posuuid_detail == "")
                posuuid_detail = StringUtil.CStr(Advtek.Utility.GuidNo.getUUID());
            for (int i = 0; i < Detail.Rows.Count; i++)
            {
                PopupControl txtPRODNO = gv.FindRowCellTemplateControl(i, (GridViewDataColumn)gv.Columns["PRODNO"], "txtPRODNO") as PopupControl;
                ASPxTextBox txtPRODNAME = gv.FindRowCellTemplateControl(i, (GridViewDataColumn)gv.Columns["PRODNAME"], "txtPRODNAME") as ASPxTextBox;
                ASPxTextBox txtQUANTITY = gv.FindRowCellTemplateControl(i, (GridViewDataColumn)gv.Columns["QUANTITY"], "txtQUANTITY") as ASPxTextBox;
                ASPxTextBox txtUNIT_PRICE = gv.FindRowCellTemplateControl(i, (GridViewDataColumn)gv.Columns["UNIT_PRICE"], "txtUNIT_PRICE") as ASPxTextBox;//單價
                ASPxTextBox txtTOTAL_AMOUNT = gv.FindRowCellTemplateControl(i, (GridViewDataColumn)gv.Columns["TOTAL_AMOUNT"], "txtTOTAL_AMOUNT") as ASPxTextBox;//總價
                ASPxTextBox hidIS_OPEN_PRICE = gv.FindRowCellTemplateControl(i, (GridViewDataColumn)gv.Columns["UNIT_PRICE"], "hidIS_OPEN_PRICE") as ASPxTextBox;//是否自訂價格
                ASPxTextBox hd_ID = gv.FindRowCellTemplateControl(i, (GridViewDataColumn)gv.Columns["IMEI_QTY"], "hd_ID") as ASPxTextBox;
                PopupControl InputIMEIData = gv.FindRowCellTemplateControl(i, (GridViewDataColumn)gv.Columns["IMEI"], "InputIMEIData") as PopupControl;//IMEI
                if (txtPRODNO != null && hd_ID != null && hd_ID.Text != "")
                {
                    DataRow[] drsID = Detail.Select(" ID = '" + hd_ID.Text + "'");
                    if (drsID != null && drsID.Length > 0)
                    {
                        DataRow dr = drsID[0];
                        dr["PRODNO"] = txtPRODNO.Text;
                        dr["PRODNAME"] = txtPRODNAME.Text;
                        dr["QUANTITY"] = txtQUANTITY.Text;
                        if (txtUNIT_PRICE.Text != "" && NumberUtil.IsNumeric(txtUNIT_PRICE.Text))
                            dr["UNIT_PRICE"] = txtUNIT_PRICE.Text;
                        else
                            dr["UNIT_PRICE"] = 0;
                        if (txtTOTAL_AMOUNT.Text != "" && NumberUtil.IsNumeric(txtTOTAL_AMOUNT.Text))
                            dr["TOTAL_AMOUNT"] = txtTOTAL_AMOUNT.Text;
                        else
                            dr["TOTAL_AMOUNT"] = 0;
                        dr["IS_OPEN_PRICE"] = hidIS_OPEN_PRICE.Text;
                        dr["IMEI"] = InputIMEIData.Text;
                        dr["POSUUID_MASTER"] = StringUtil.CStr(Head.Rows[0]["POSUUID_MASTER"]);
                        if (dr["POSUUID_DETAIL"] == null || string.IsNullOrEmpty(StringUtil.CStr(dr["POSUUID_DETAIL"])))
                        {
                            dr["POSUUID_DETAIL"] = posuuid_detail;
                        }
                        DataTable PROD_DT = new SAL01_Facade().getProduct(txtPRODNO.Text);
                        if (PROD_DT.Rows.Count > 0) //取得稅額計算方式
                        {
                            dr["TAXABLE"] = PROD_DT.Rows[0]["TAXABLE"];
                            dr["TAXRATE"] = PROD_DT.Rows[0]["TAXRATE"];
                            dr["ISSTOCK"] = PROD_DT.Rows[0]["ISSTOCK"];
                            dr["INV_TYPE"] = PROD_DT.Rows[0]["INV_TYPE"];
                        }
                    }
                }
            }
            Detail.AcceptChanges();
            gv.DataSource = Detail;
            gv.DataBind();
        }

        public int Delete_Detail_Temp(VSS_SAL_SA11 p, ASPxGridView gv)
        {
            List<object> li = gv.GetSelectedFieldValues(gv.KeyFieldName);
            if (li.Count > 0)
            {
                string where = "";
                foreach (string key in li)
                {
                    where += "'" + key + "',";
                }
                if (where.Length > 0)
                    where = where.Substring(0, where.Length - 1);
                else where = "''";
                DataRow[] dra = Detail.Select("ID in(" + where + ")");
                foreach (DataRow dr in dra)
                {
                    //刪除交易明細時，順便刪除已記錄在Sale_IMEI_Log中的IMEI紀錄
                    if (dr["ID"] != null && StringUtil.CStr(dr["ID"]) != "")
                        p.Facade.DeleteSale_IMEI_LOG(StringUtil.CStr(dr["ID"]));
                    Detail.Rows.Remove(dr);
                }
                gv.DataSource = Detail;
                gv.DataBind();

                calTotalAmount(p);
            }
            return li.Count;
        }

        public void SavePaid_Detail_Temp(ASPxGridView gv)
        {
            for (int i = 0; i < Paid.Rows.Count; i++)
            {
                ASPxLabel lbPAID_AMOUNT = gv.FindRowCellTemplateControl(i, (GridViewDataColumn)gv.Columns["PAID_AMOUNT"], "lbPAID_AMOUNT") as ASPxLabel;//總價
                ASPxLabel lbDESCRIPTION = gv.FindRowCellTemplateControl(i, (GridViewDataColumn)gv.Columns["DESCRIPTION"], "lbDESCRIPTION") as ASPxLabel;//總價
                Paid.Rows[i]["PAID_AMOUNT"] = lbPAID_AMOUNT.Text;
                Paid.Rows[i]["DESCRIPTION"] = lbDESCRIPTION.Text;
                Paid.Rows[i]["POSUUID_MASTER"] = StringUtil.CStr(Head.Rows[0]["POSUUID_MASTER"]);
            }
            Paid.AcceptChanges();
            gv.DataSource = Paid;
            gv.DataBind();
        }

        public int Delete_Paid_Temp(ASPxGridView gv)
        {
            string strID = gv.FindChildControl<ASPxTextBox>("hdDeleteIDs").Text;

            if (!string.IsNullOrEmpty(strID))
            {
                string where = "";
                where = strID;
                DataRow[] dra = Paid.Select("ID in(" + where + ")");
                foreach (DataRow dr in dra)
                {
                    Paid.Rows.Remove(dr);
                }
                gv.DataSource = Paid;
                gv.DataBind();
            }
            return strID.Split(',').Length;
        }

        public void SaveTempTable(VSS_SAL_SA11 p)
        {
            //把畫面資料存進Session
            SaveSale_Head_Temp(p);
            SaveSale_Detail_Temp(p.gvMaster);
            if (Discount != null && Discount.Rows.Count > 0)
            {
                if (Head != null && Head.Rows.Count > 0 && Head.Rows[0]["POSUUID_MASTER"] != null &&
                    !string.IsNullOrEmpty(StringUtil.CStr(Head.Rows[0]["POSUUID_MASTER"])))
                {
                    foreach (DataRow dr in Discount.Rows)
                    {
                        dr["POSUUID_MASTER"] = StringUtil.CStr(Head.Rows[0]["POSUUID_MASTER"]);
                    }
                    Discount.AcceptChanges();
                }
            }
            SavePaid_Detail_Temp(p.gvCheckOut); //未實作
        }

        #endregion
        #region 新增商品資料
        /// <summary>
        /// 單品資料
        /// </summary>
        /// <param name="gv"></param>
        public void NewRow_Detail(ASPxGridView gv)
        {
            SaveSale_Detail_Temp(gv);
            DataRow dr = Detail.NewRow();
            dr["POSUUID_MASTER"] = StringUtil.CStr(Head.Rows[0]["POSUUID_MASTER"]);
            dr["POSUUID_DETAIL"] = "";
            dr["ID"] = StringUtil.CStr(Advtek.Utility.GuidNo.getUUID());
            dr["IMEI_QTY"] = 0;
            dr["QUANTITY"] = 1;
            dr["ITEM_TYPE"] = "1";
            dr["ITEM_TYPE_NAME"] = "單";
            dr["SEQNO"] = Detail.Rows.Count + 1;
            dr["SOURCE_TYPE"] = "11"; //新增單品時, SOURCE_TYPE需設為 11
            Detail.Rows.InsertAt(dr, 0);
            Detail.AcceptChanges();
            gv.DataSource = Detail;
            gv.DataBind();
        }

        /// <summary>
        /// 組合促銷資料
        /// </summary>
        /// <param name="gv"></param>
        public void NewRecord_Detail_MixPromotion(VSS_SAL_SA11 p, ASPxGridView gv, string[] args)
        {
            SaveSale_Detail_Temp(gv);
            DataRow[] drs = Detail.Select(" PROMOTION_CODE = '" + p.hidPromotion_Code.Value + "' And POSUUID_DETAIL = '" + p.hidPosuuid_Detail.Value + "'");
            bool changeItem = false;
            if (p.hidPromotion_Code.Value != null && StringUtil.CStr(p.hidPromotion_Code.Value) != "" && drs != null && drs.Length > 0)
            {   //若選定從未結清單帶過來的銷售細項要換促銷組合商品時,才將原來的銷售細項刪除
                changeItem = true;
                foreach (DataRow dr in drs)
                {
                    Detail.Rows.Remove(dr);
                    Detail.AcceptChanges();
                }
            }
            if (args != null && args.Length > 0)
            {
                //args[0] is "MixPromotion";
                //args[1] is Promotion_Code^Promotion_Name
                string[] valArray = args[1].Split('^');
                string returnPromoCode = "";
                string returnPromoName = "";
                if (valArray != null && valArray.Length > 1)
                {
                    returnPromoCode = valArray[0];
                    returnPromoName = valArray[1];
                }

                for (int i = 2; i < args.Length; i++)
                {
                    valArray = args[i].Split('^');
                    if (valArray != null && valArray.Length > 2)
                    {
                        DataRow dr = Detail.NewRow();
                        dr["POSUUID_MASTER"] = StringUtil.CStr(Head.Rows[0]["POSUUID_MASTER"]);
                        dr["ID"] = StringUtil.CStr(Advtek.Utility.GuidNo.getUUID());
                        dr["PRODNO"] = valArray[0];
                        dr["PRODNAME"] = valArray[1];
                        dr["IMEI_QTY"] = 0;
                        dr["QUANTITY"] = 1;
                        dr["UNIT_PRICE"] = valArray[2];
                        dr["TOTAL_AMOUNT"] = valArray[2];
                        if (changeItem)
                        {
                            dr["ITEM_TYPE"] = "2";
                            dr["ITEM_TYPE_NAME"] = "促";
                            dr["POSUUID_DETAIL"] = p.hidPosuuid_Detail.Value;
                            dr["PROMOTION_CODE"] = p.hidPromotion_Code.Value;
                            dr["PROMO_NAME"] = p.hidPromotion_Name.Value;
                            if (p.hidSOURCE_TYPE.Value != null)
                                dr["SOURCE_TYPE"] = p.hidSOURCE_TYPE.Value;
                        }
                        else
                        {
                            dr["ITEM_TYPE"] = "2";
                            dr["ITEM_TYPE_NAME"] = "促";
                            dr["PROMOTION_CODE"] = returnPromoCode;
                            dr["PROMO_NAME"] = returnPromoName;
                            dr["SOURCE_TYPE"] = "11"; //新增單品時, SOURCE_TYPE需設為 11
                        }
                        
                        dr["SEQNO"] = Detail.Rows.Count + 1;
                        Detail.Rows.InsertAt(dr, 0);
                        Detail.AcceptChanges();
                    }
                }
                gv.DataSource = Detail;
                gv.DataBind();
                calTotalAmount(p);
            }
        }

        /// <summary>
        /// 授信通聯資料
        /// </summary>
        /// <param name="gv"></param>
        public void NewRow_Detail_CreditCommunication(VSS_SAL_SA11 p, ASPxGridView gv, string[] args)
        {
            SaveSale_Detail_Temp(gv);
            if (args != null && args.Length > 0)
            {
                for (int i = 0; i < args.Length; i++)
                {
                    string[] val = args[i].Split(new char[] { '^' });
                    if (val != null && val.Length > 3)
                    {
                        DataRow dr = Detail.NewRow();
                        dr["POSUUID_MASTER"] = StringUtil.CStr(Head.Rows[0]["POSUUID_MASTER"]);
                        dr["POSUUID_DETAIL"] = "";
                        dr["ID"] = StringUtil.CStr(Advtek.Utility.GuidNo.getUUID());
                        dr["PRODNO"] = val[1];
                        dr["PRODNAME"] = "授信通聯";
                        dr["MSISDN"] = val[2];
                        dr["IMEI_QTY"] = 0;
                        dr["QUANTITY"] = 1;
                        dr["UNIT_PRICE"] = val[3];
                        dr["TOTAL_AMOUNT"] = val[3];
                        dr["ITEM_TYPE"] = "9";
                        dr["ITEM_TYPE_NAME"] = "單";
                        dr["SEQNO"] = Detail.Rows.Count + 1;
                        dr["SOURCE_TYPE"] = "11"; //新增單品時, SOURCE_TYPE需設為 11
                        Detail.Rows.InsertAt(dr, 0);
                        Detail.AcceptChanges();
                        gv.DataSource = Detail;
                        gv.DataBind();
                    }
                }
                DataRow drHead = Head.Rows[0];
                drHead["SALE_TYPE"] = "1"; //一般交易
                Head.AcceptChanges();
                calTotalAmount(p);
            }
        }
        #endregion

        #region 新增折扣
        /// <summary>
        /// 折扣
        /// </summary>
        /// <param name="gv"></param>
        public void NewRow_Detail_ItemDiscount(VSS_SAL_SA11 p, ASPxGridView gv)
        {
            #region 處理Discount及贈品
            StringBuilder sb = new StringBuilder("");
            string strMSIDSN = "";
            string strRateAmt = "";
            string strDATA = "";
            string strVOICE = "";
            string strTrans_type = "";
            string strMNP = "";
            string strBUNDLE_TYPE = "";
            string strSOURCET_TYPE = "";

            int itemAmt = 0;
            DataRow[] drs = Detail.Select(" SOURCE_TYPE = '11' ");
            if (drs != null && drs.Length > 0)
            {
                foreach (DataRow drDetail in drs)
                {
                    sb.Append(StringUtil.CStr(drDetail["PRODNO"])).Append("|");
                    if (drDetail["MSISDN"] != null && StringUtil.CStr(drDetail["MSISDN"]) != "")
                        strMSIDSN = StringUtil.CStr(drDetail["MSISDN"]);
                    if (drDetail["R_RATE"] != null && StringUtil.CStr(drDetail["R_RATE"]) != "")
                        strRateAmt = StringUtil.CStr(drDetail["R_RATE"]);
                    if (drDetail["DATA"] != null && StringUtil.CStr(drDetail["DATA"]) != "")
                        strDATA = StringUtil.CStr(drDetail["DATA"]);
                    if (drDetail["VOICE"] != null && StringUtil.CStr(drDetail["VOICE"]) != "")
                        strVOICE = StringUtil.CStr(drDetail["VOICE"]);
                    if (drDetail["TRANS_TYPE"] != null && StringUtil.CStr(drDetail["TRANS_TYPE"]) != "")
                        strTrans_type = StringUtil.CStr(drDetail["TRANS_TYPE"]);
                    if (drDetail["MNP"] != null && StringUtil.CStr(drDetail["MNP"]) != "")
                        strMNP = StringUtil.CStr(drDetail["MNP"]);
                    if (drDetail["BUNDLE_TYPE"] != null && StringUtil.CStr(drDetail["BUNDLE_TYPE"]) != "")
                        strBUNDLE_TYPE = StringUtil.CStr(drDetail["BUNDLE_TYPE"]);
                    if (drDetail["SOURCE_TYPE"] != null && StringUtil.CStr(drDetail["SOURCE_TYPE"]) != "")
                        strSOURCET_TYPE = StringUtil.CStr(drDetail["SOURCE_TYPE"]);

                    if (drDetail["TOTAL_AMOUNT"] != null && StringUtil.CStr(drDetail["TOTAL_AMOUNT"]) != ""
                        && NumberUtil.IsNumeric(StringUtil.CStr(drDetail["TOTAL_AMOUNT"])))
                        itemAmt += int.Parse(StringUtil.CStr(drDetail["TOTAL_AMOUNT"]));
                }

                string prodStr = StringUtil.CStr(sb);
                prodStr = prodStr.Substring(0, prodStr.Length - 1);

                DataTable dtDiscount = p.Facade.getMixPromotion_ItemDiscount(p.logMsg.STORENO, p.logMsg.OPERATOR, StringUtil.CStr(p.hidPromotion_Code.Value),
                                                                             strMSIDSN, strRateAmt, strDATA, strVOICE, strTrans_type, strMNP, strBUNDLE_TYPE,
                                                                             strSOURCET_TYPE, prodStr);
                if (dtDiscount != null && dtDiscount.Rows.Count > 0)
                {
                    int curItemTolAmt = itemAmt;
                    foreach (DataRow drDis in dtDiscount.Rows)
                    {
                        DataRow dr = Discount.NewRow();
                        dr["POSUUID_MASTER"] = StringUtil.CStr(Head.Rows[0]["POSUUID_MASTER"]);
                        dr["POSUUID_DETAIL"] = StringUtil.CStr(Advtek.Utility.GuidNo.getUUID());
                        dr["ID"] = StringUtil.CStr(Advtek.Utility.GuidNo.getUUID());
                        dr["PRODNO"] = drDis["DISCOUNT_CODE"];
                        dr["PRODNAME"] = drDis["DISCOUNT_NAME"];
                        dr["IMEI_QTY"] = 0;
                        dr["QUANTITY"] = 1;
                        int price = 0;
                        if (drDis["DISCOUNT_MONEY"] != null && StringUtil.CStr(drDis["DISCOUNT_MONEY"]) != ""
                            && NumberUtil.IsNumeric(StringUtil.CStr(drDis["DISCOUNT_MONEY"])))
                            price = int.Parse(StringUtil.CStr(drDis["DISCOUNT_MONEY"]));
                        if (price == 0)
                        {
                            double disRate = 0;
                            if (drDis["DISCOUNT_RATE"] != null && StringUtil.CStr(drDis["DISCOUNT_RATE"]) != ""
                                && NumberUtil.IsNumeric(StringUtil.CStr(drDis["DISCOUNT_RATE"])))
                                disRate = double.Parse(StringUtil.CStr(drDis["DISCOUNT_RATE"]));
                            price = (int)(itemAmt * disRate);
                        }
                        if (curItemTolAmt + price >= 0)
                        {
                            dr["UNIT_PRICE"] = price;
                            dr["TOTAL_AMOUNT"] = price;
                            curItemTolAmt += price;
                        }
                        else
                        {
                            dr["UNIT_PRICE"] = -1 * curItemTolAmt;
                            dr["TOTAL_AMOUNT"] = -1 * curItemTolAmt;
                            curItemTolAmt = 0;
                        }
                        dr["ORI_UNIT_PRICE"] = price;
                        dr["ITEM_TYPE"] = "5";
                        dr["ITEM_TYPE_NAME"] = "折";
                        dr["TAXABLE"] = "Y";
                        dr["TAXRATE"] = "5";
                        dr["SOURCE_TYPE"] = "11";
                        dr["SEQNO"] = Discount.Rows.Count + 1;
                        Discount.Rows.InsertAt(dr, 0);
                        Discount.AcceptChanges();
                    }
                }

                gv.DataSource = Discount;
                gv.DataBind();
                gv.Visible = true;
            #endregion 處理Discount及贈品
                calTotalAmount(p);
            }
        }
        #endregion

        #region 新增付款方式

        /// <summary>
        /// 付現金
        /// </summary>
        /// <param name="gv"></param>
        /// <param name="args">
        /// [0] = TYPE
        /// [1] = 金額
        /// </param>
        public void NewRow_Paid_Cash(ASPxGridView gv, string[] args)
        {
            SavePaid_Detail_Temp(gv);
            DataRow dr = Paid.NewRow();
            dr["POSUUID_MASTER"] = Head.Rows[0]["POSUUID_MASTER"];
            dr["ID"] = StringUtil.CStr(Advtek.Utility.GuidNo.getUUID());
            dr["PAID_AMOUNT"] = args[1];
            dr["DESCRIPTION"] = "現金";
            dr["PAID_MODE"] = 1;
            dr["PAID_MODE_NAME"] = "現金";
            Paid.Rows.InsertAt(dr, 0);
            Paid.AcceptChanges();
            gv.DataSource = Paid;
            gv.DataBind();
        }

        /// <summary>
        /// 付離線信用卡
        /// </summary>
        /// <param name="gv"></param>
        /// <param name="args"></param>
        public void NewRow_Paid_OffLineCredit(ASPxGridView gv, string[] args)
        {
            SavePaid_Detail_Temp(gv);
            DataRow dr = Paid.NewRow();
            dr["ID"] = StringUtil.CStr(Advtek.Utility.GuidNo.getUUID());
            dr["POSUUID_MASTER"] = Head.Rows[0]["POSUUID_MASTER"];
            dr["PAID_AMOUNT"] = args[1];
            dr["DESCRIPTION"] = "信用卡號:" + args[2] + ",授權碼:" + args[3];
            dr["CREDIT_TYPE"] = 3; //11: 一般2: 分期3: 離線
            dr["PAID_MODE"] = 3;
            dr["PAID_MODE_NAME"] = "離線信用卡";
            dr["CREDIT_CARD_NO"] = args[2];//信用卡卡號
            dr["CREDIT_CARD_AUTH_NO"] = args[3];//授權碼
            string cardType = CreditCard_Facade.CheckCardType(args[2]);
            DataTable dtCardType = new SAL01_Facade().getCreditCardType(cardType);
            if (dtCardType != null && dtCardType.Rows.Count > 0 && dtCardType.Rows[0]["CREDIT_CARD_TYPE_ID"] != null)
            {
                dr["CREDIT_CARD_TYPE_ID"] = StringUtil.CStr(dtCardType.Rows[0]["CREDIT_CARD_TYPE_ID"]);
                DataTable dtRate = new SAL01_Facade().getCreditDivRate(StringUtil.CStr(dtCardType.Rows[0]["CREDIT_CARD_TYPE_ID"]));
                if (dtRate != null && dtRate.Rows.Count > 0 && dtRate.Rows[0]["charge_rate"] != null &&
                    NumberUtil.IsNumeric(StringUtil.CStr(dtRate.Rows[0]["charge_rate"])))
                {
                    dr["CREDIT_CARD_CHARGE_RATE"] = double.Parse(StringUtil.CStr(dtRate.Rows[0]["charge_rate"]));
                    if (args[1] != null && args[1] != "" && NumberUtil.IsNumeric(args[1]))
                        dr["CREDIT_CARD_FEE"] = Math.Round(int.Parse(args[1]) * double.Parse(StringUtil.CStr(dtRate.Rows[0]["charge_rate"])) / 100, 2);
                }
            }
            Paid.Rows.InsertAt(dr, 0);
            Paid.AcceptChanges();
            gv.DataSource = Paid;
            gv.DataBind();
        }
        #endregion

        #region DB存取
        /// <summary>
        /// 付款時將資料存入Cache
        /// </summary>
        /// <param name="p"></param>
        public void Save_Sale_Data2Cache(VSS_SAL_SA11 p)
        {
            SAL01_Facade Facade = new SAL01_Facade();
            #region 設定預設值
            foreach (DataRow dr in Head.Rows)
            {
                dr["STORE_NO"] = p.logMsg.STORENO;
                //string STORENAME = "";
                DataTable dt = new Store_Facade().Query_StoreInfo(p.logMsg.STORENO);
                if (dt.Rows.Count > 0)
                    dr["STORE_NAME"] = dt.Rows[0]["STORENAME"];
                if (p.txtInv_Date.Text != "" && DateUtil.IsDate(p.txtInv_Date.Text))
                    dr["INVOICE_DATE"] = Convert.ToDateTime(p.txtInv_Date.Text).ToString("yyyy/MM/dd");
                else
                    dr["INVOICE_DATE"] = Convert.ToDateTime(DateTime.Today).ToString("yyyy/MM/dd");
                dr["MACHINE_ID"] = p.logMsg.MACHINE_ID;
                dr["HOST_ID"] = p.logMsg.HOST_IP;
                dr["CREATE_DTM"] = p.logMsg.CREATE_DTM;
                dr["CREATE_USER"] = p.logMsg.CREATE_USER;
                dr["MODI_DTM"] = p.logMsg.MODI_DTM;
                dr["MODI_USER"] = p.logMsg.MODI_USER;
                dr["ORIGINAL_ID"] = dr["POSUUID_MASTER"]; //最原始的ID, 從未作廢的會與ID同，退換貨時會一路帶下來
            }
            Head.AcceptChanges();
            foreach (DataRow dr in Detail.Rows)
            {
                if (dr["POSUUID_DETAIL"] == null || string.IsNullOrEmpty(StringUtil.CStr(dr["POSUUID_DETAIL"])))
                    dr["POSUUID_DETAIL"] = StringUtil.CStr(Advtek.Utility.GuidNo.getUUID());
                dr["CREATE_DTM"] = p.logMsg.CREATE_DTM;
                dr["CREATE_USER"] = p.logMsg.CREATE_USER;
                dr["MODI_DTM"] = p.logMsg.MODI_DTM;
                dr["MODI_USER"] = p.logMsg.MODI_USER;
            }
            Detail.AcceptChanges();

            foreach (DataRow dr in Discount.Rows)
            {
                if (dr["POSUUID_DETAIL"] == null || string.IsNullOrEmpty(StringUtil.CStr(dr["POSUUID_DETAIL"])))
                    dr["POSUUID_DETAIL"] = StringUtil.CStr(Advtek.Utility.GuidNo.getUUID());
                dr["CREATE_DTM"] = p.logMsg.CREATE_DTM;
                dr["CREATE_USER"] = p.logMsg.CREATE_USER;
                dr["MODI_DTM"] = p.logMsg.MODI_DTM;
                dr["MODI_USER"] = p.logMsg.MODI_USER;
            }
            Discount.AcceptChanges();
            foreach (DataRow dr in Paid.Rows)
            {
                dr["CREATE_DTM"] = p.logMsg.CREATE_DTM;
                dr["CREATE_USER"] = p.logMsg.CREATE_USER;
                dr["MODI_DTM"] = p.logMsg.MODI_DTM;
                dr["MODI_USER"] = p.logMsg.MODI_USER;
            }
            Paid.AcceptChanges();
            #endregion
            OracleConnection objConn = OracleDBUtil.GetConnection();
            OracleTransaction objTX = objConn.BeginTransaction();
            try
            {
                Facade.InsertSale_Head(Head);
                Facade.InsertSale_Detail(Detail);
                Facade.InsertSale_Detail(Discount);
                Facade.InsertPaid_Detail(Paid);
                objTX.Commit();
            }
            catch (Exception ex)
            {
                objTX.Rollback();
                throw ex;
            }
            finally
            {
                if (objConn.State == ConnectionState.Open)
                    objConn.Close();
                objConn.Dispose();
                OracleConnection.ClearAllPools();
            }
            Mode_PayCash(p);
        }

        /// <summary>
        /// 結帳時將資料存入DB
        /// </summary>
        /// <param name="p"></param>
        /// <param name="objTX">外部傳入Transaction</param>
        public void Save_Sale_Data2Cache(VSS_SAL_SA11 p, OracleTransaction objTX)
        {
            SAL01_Facade Facade = new SAL01_Facade();
            #region 設定預設值
            foreach (DataRow dr in Head.Rows)
            {
                dr["STORE_NO"] = p.logMsg.STORENO;
                //string STORENAME = "";
                DataTable dt = new Store_Facade().Query_StoreInfo(p.logMsg.STORENO);
                if (dt.Rows.Count > 0)
                    dr["STORE_NAME"] = dt.Rows[0]["STORENAME"];
                if (p.txtInv_Date.Text != "" && DateUtil.IsDate(p.txtInv_Date.Text))
                    dr["INVOICE_DATE"] = Convert.ToDateTime(p.txtInv_Date.Text).ToString("yyyy/MM/dd");
                else
                    dr["INVOICE_DATE"] = Convert.ToDateTime(DateTime.Today).ToString("yyyy/MM/dd");
                dr["MACHINE_ID"] = p.logMsg.MACHINE_ID;
                dr["HOST_ID"] = p.logMsg.HOST_IP;
                dr["CREATE_DTM"] = p.logMsg.CREATE_DTM;
                dr["CREATE_USER"] = p.logMsg.CREATE_USER;
                dr["MODI_DTM"] = p.logMsg.MODI_DTM;
                dr["MODI_USER"] = p.logMsg.MODI_USER;
                dr["ORIGINAL_ID"] = dr["POSUUID_MASTER"]; //最原始的ID, 從未作廢的會與ID同，退換貨時會一路帶下來
            }
            Head.AcceptChanges();
            foreach (DataRow dr in Detail.Rows)
            {
                if (dr["POSUUID_DETAIL"] == null || string.IsNullOrEmpty(StringUtil.CStr(dr["POSUUID_DETAIL"])))
                    dr["POSUUID_DETAIL"] = StringUtil.CStr(Advtek.Utility.GuidNo.getUUID());
                dr["CREATE_DTM"] = p.logMsg.CREATE_DTM;
                dr["CREATE_USER"] = p.logMsg.CREATE_USER;
                dr["MODI_DTM"] = p.logMsg.MODI_DTM;
                dr["MODI_USER"] = p.logMsg.MODI_USER;
            }
            Detail.AcceptChanges();

            foreach (DataRow dr in Discount.Rows)
            {
                if (dr["POSUUID_DETAIL"] == null || string.IsNullOrEmpty(StringUtil.CStr(dr["POSUUID_DETAIL"])))
                    dr["POSUUID_DETAIL"] = StringUtil.CStr(Advtek.Utility.GuidNo.getUUID());
                dr["CREATE_DTM"] = p.logMsg.CREATE_DTM;
                dr["CREATE_USER"] = p.logMsg.CREATE_USER;
                dr["MODI_DTM"] = p.logMsg.MODI_DTM;
                dr["MODI_USER"] = p.logMsg.MODI_USER;
            }
            Discount.AcceptChanges();
            foreach (DataRow dr in Paid.Rows)
            {
                dr["CREATE_DTM"] = p.logMsg.CREATE_DTM;
                dr["CREATE_USER"] = p.logMsg.CREATE_USER;
                dr["MODI_DTM"] = p.logMsg.MODI_DTM;
                dr["MODI_USER"] = p.logMsg.MODI_USER;
            }
            Paid.AcceptChanges();
            #endregion

            try
            {
                Facade.InsertSale_Head(Head, objTX);
                Facade.InsertSale_Detail(Detail, objTX);
                Facade.InsertSale_Detail(Discount, objTX);
                Facade.InsertPaid_Detail(Paid, objTX);
            }
            catch (Exception ex)
            {
                objTX.Rollback();
                throw ex;
            }
            Mode_PayCash(p);
        }

        /// <summary>
        /// 清空至無付款資料時取消Cache 
        /// </summary>
        /// <param name="p"></param>
        public void Delete_Sale_DataFromCache(VSS_SAL_SA11 p)
        {
            SAL01_Facade Facade = new SAL01_Facade();
            Facade.delSaleData(StringUtil.CStr(Head.Rows[0]["POSUUID_MASTER"]));
            DataRow[] drs = Discount.Select(" SOURCE_TYPE = '11' ");
            if (drs != null && drs.Length > 0)
            {
                foreach (DataRow drDis in drs)
                {
                    Discount.Rows.Remove(drDis);
                    Discount.AcceptChanges();
                }
            }
            Mode_HavePROD(p);//回到可以使用
        }

        /// <summary>
        /// 結帳時先刪除暫存資料 
        /// </summary>
        /// <param name="p"></param>
        public void Delete_Sale_DataFromCache(VSS_SAL_SA11 p, OracleTransaction objTX)
        {
            try
            {
                SAL01_Facade Facade = new SAL01_Facade();
                Facade.DeleteSale_Detail(Detail, objTX);
                Facade.DeleteSale_Detail(Discount, objTX);
                Facade.DeletePaid_DetailByPOSUUID_MASTER(StringUtil.CStr(Head.Rows[0]["POSUUID_MASTER"]), objTX);
                Facade.DeleteSale_Head(Head, objTX);
            }
            catch (Exception ex)
            {
                objTX.Rollback();
                throw ex;
            }
            Mode_HavePROD(p);//回到可以使用
        }

        /// <summary>
        ///結帳時將資料存入DB從未結清單
        /// </summary>
        /// <param name="p"></param>
        public void Save_Sale_Data2DBFromSAL05(VSS_SAL_SA11 p, OracleTransaction objTX)
        {
            SAL01_Facade Facade = new SAL01_Facade();
            string SALE_NO = Store_SerialNo.GenNo("SALE", p.logMsg.STORENO, p.logMsg.MACHINE_ID);
            foreach (DataRow dr in Head.Rows)
            {
                dr["INVOICE_NO"] = p.lbINVOICE_NO.Text;
                dr["INVOICE_TYPE"] = p.rbINVOICE_TYPE.Value;
                dr["SALE_STATUS"] = "2"; //結帳
                dr["SALE_NO"] = SALE_NO;
            }
            Head.AcceptChanges();

            //更新資料
            Facade.CheckOutFromUnClose(Head, Detail, p.logMsg.MODI_USER, objTX);
            SetInvoiceOrReceipt(objTX, p.logMsg.HOST_IP, p);//設定發票資料
            SaleHeadDataBind(p);
        }

        /// <summary>
        ///結帳時將資料存入DB從銷貨單新增
        /// </summary>
        /// <param name="p"></param>
        public void Save_Sale_Data2DBFromSAL11(VSS_SAL_SA11 p, OracleTransaction objTX)
        {
            SAL01_Facade Facade = new SAL01_Facade();
            string SAL_NO = Store_SerialNo.GenNo("SALE", p.logMsg.STORENO, p.logMsg.MACHINE_ID);
            foreach (DataRow dr in Head.Rows)
            {
                dr["INVOICE_NO"] = p.lbINVOICE_NO.Text;
                dr["INVOICE_TYPE"] = p.rbINVOICE_TYPE.Value;
                dr["SALE_STATUS"] = "2"; //結帳
                dr["SALE_NO"] = SAL_NO;
            }
            Head.AcceptChanges();

            //更新資料
            SetInvoiceOrReceipt(objTX, p.logMsg.HOST_IP, p);
            SaleHeadDataBind(p);
        }

        /// <summary>
        ///結帳時將資料從Cache存入DB
        /// </summary>
        /// <param name="p"></param>
        public void Save_Sale_Data2DBFromCache(VSS_SAL_SA11 p, OracleTransaction objTX)
        {
            SAL01_Facade Facade = new SAL01_Facade();
            string SAL_NO = Store_SerialNo.GenNo("SALE", p.logMsg.STORENO, p.logMsg.MACHINE_ID);
            foreach (DataRow dr in Head.Rows)
            {
                dr["INVOICE_NO"] = p.lbINVOICE_NO.Text;
                dr["INVOICE_TYPE"] = p.rbINVOICE_TYPE.Value;
                dr["SALE_STATUS"] = "2"; //結帳
                dr["SALE_NO"] = SAL_NO;
            }

            Head.AcceptChanges();
            //更新資料
            //判斷原來銷售交易是否來自未結清單
            DataRow[] drs = Detail.Select(" ITEM_TYPE IN ('2','5','11','12') ");
            if (drs != null && drs.Length > 0)
                Facade.CheckOutFromUnClose(Head, Detail, p.logMsg.MODI_USER, objTX);
            SetInvoiceOrReceipt(objTX, p.logMsg.HOST_IP, p);
            SaleHeadDataBind(p);
        }

        /// <summary>
        /// 檢查開立發票的原則 依貨品資料INV_TYPE去開立發票,
        /// </summary>
        public void SetInvoiceOrReceipt(OracleTransaction objTX, string HOST_ID, VSS_SAL_SA11 p)
        {
            DataRow[] INV_TAX_DRA;       //發票 應稅
            //DataRow[] INV_TAX_DRA_ZERO;  //發票 應稅 0稅
            //DataRow[] INV_NO_TAX_DRA;    //發票 免稅
            DataRow[] RECDT;             //收據

            DataTable dtInv = Detail.Clone();
            dtInv.Merge(Detail);
            dtInv.Merge(Discount);

            int saleTotalAmt = 0;
            if (Head.Rows[0]["SALE_TOTAL_AMOUNT"] != null && StringUtil.CStr(Head.Rows[0]["SALE_TOTAL_AMOUNT"]) != ""
                && NumberUtil.IsNumeric(StringUtil.CStr(Head.Rows[0]["SALE_TOTAL_AMOUNT"])))
                saleTotalAmt = int.Parse(StringUtil.CStr(Head.Rows[0]["SALE_TOTAL_AMOUNT"]));
            if (StringUtil.CStr(p.rbINVOICE_TYPE.Value) != "0" && saleTotalAmt > 0)
            {
                INV_TAX_DRA = dtInv.Select(" 1=1 ");     //發票 應稅
                //手開發票
                p.Facade.InsertManualInvoice(Head, INV_TAX_DRA, "1", HOST_ID, objTX);
            }
            else
            {
                RECDT = dtInv.Select(" 1=1 ");
                p.Facade.InsertReceipt(Head, RECDT, objTX);
            }
        }
        #endregion
        #region 當前模式
        /// <summary>
        /// 進入有貨品資料,無付款資料時
        /// </summary>
        /// <param name="p"></param>
        public void Mode_HavePROD(VSS_SAL_SA11 p)
        {
            p.btnOrderCheckOut.Enabled = false;
            //p.btnOrderCancel.Enabled = true;
            p.gvMaster.Enabled = true;
            p.gvDetail.Enabled = true;
            p.gvCheckOut.Enabled = true;
            p.txtREMARK.Enabled = true;
            p.txtUNI_NO.ClientEnabled = true;
            p.txtUNI_TITLE.ClientEnabled = true;

            if (Head.Rows[0]["SALE_TYPE"] != null && StringUtil.CStr(Head.Rows[0]["SALE_TYPE"]) == "2")
            {   //代收交易不可與其他交易合併結帳，所以不可以再新增任何交易細項與折扣
                p.gvMaster.FindChildControl<ASPxButton>("btnItemAdd").Enabled = false;
                p.gvMaster.FindChildControl<ASPxButton>("btnMixPromotion").Enabled = false;
                p.gvMaster.FindChildControl<ASPxButton>("btnItemDel").Enabled = false;
                p.gvMaster.FindChildControl<ASPxButton>("btnCreditCommunication").Enabled = false;
            }
            else
            {
                p.gvMaster.FindChildControl<ASPxButton>("btnItemAdd").Enabled = true;
                p.gvMaster.FindChildControl<ASPxButton>("btnMixPromotion").Enabled = true;
                p.gvMaster.FindChildControl<ASPxButton>("btnCreditCommunication").Enabled = true;
            }

            p.btnConfirm.Enabled = true;
            p.btnCancel.Enabled = false;
            p.gvCheckOut.FindChildControl<ASPxButton>("btnCash").Enabled = false;
            p.gvCheckOut.FindChildControl<ASPxButton>("btnOffLineCredit").Enabled = false;
            p.rbINVOICE_TYPE.ReadOnly = false;
            if (StringUtil.CStr(p.rbINVOICE_TYPE.Value) == "0")
            {
                p.lbINVOICE_NO.ClientEnabled = false;
                p.txtInv_Date.ClientEnabled = false;
                p.txtUNI_NO.ClientEnabled = false;
                p.txtUNI_TITLE.ClientEnabled = false;
            }
            else
            {
                p.lbINVOICE_NO.ClientEnabled = true;
                p.txtInv_Date.ClientEnabled = true;
                p.txtUNI_NO.ClientEnabled = true;
                p.txtUNI_TITLE.ClientEnabled = true;
            }
            p.lbTOTAL_AMOUNT.Text = "";
            p.lbChange.Text = "";
        }
        /// <summary>
        /// 進入沒有輸入貨品時
        /// </summary>
        /// <param name="p"></param>
        public void Mode_NoPROD(VSS_SAL_SA11 p)
        {
            p.btnOrderCheckOut.Enabled = false;
            //p.btnOrderCancel.Enabled = false;
            p.btnConfirm.Enabled = false;
            p.btnCancel.Enabled = false;
            p.gvMaster.Enabled = true;
            p.gvDetail.Enabled = true;
            p.gvCheckOut.Enabled = false;
            p.txtREMARK.Enabled = true;
            p.txtUNI_NO.Enabled = true;
            p.txtUNI_TITLE.Enabled = true;
            
            p.gvMaster.FindChildControl<ASPxButton>("btnMixPromotion").Enabled = true;
            p.gvMaster.FindChildControl<ASPxButton>("btnItemAdd").Enabled = true;
            p.gvMaster.FindChildControl<ASPxButton>("btnCreditCommunication").Enabled = true;
            p.rbINVOICE_TYPE.ReadOnly = false;
            if (StringUtil.CStr(p.rbINVOICE_TYPE.Value) == "0")
            {
                p.lbINVOICE_NO.ClientEnabled = false;
                p.txtUNI_TITLE.ClientEnabled = false;
                p.txtUNI_NO.ClientEnabled = false;
                p.txtInv_Date.ClientEnabled = false;
            }
            else
            {
                p.lbINVOICE_NO.ClientEnabled = true;
                p.txtUNI_TITLE.ClientEnabled = true;
                p.txtUNI_NO.ClientEnabled = true;
                p.txtInv_Date.ClientEnabled = true;
            }
        }
        /// <summary>
        /// 進入付款模式
        /// </summary>
        /// <param name="p"></param>
        public void Mode_PayCash(VSS_SAL_SA11 p)
        {
            p.btnConfirm.Enabled = false;
            if (Paid.Rows.Count > 0)
            {
                p.btnCancel.Enabled = false;
                if (p.lbPayAmount.Text == "0")
                    p.btnOrderCheckOut.Enabled = true;
            }
            else
                p.btnCancel.Enabled = true;
            p.gvMaster.Enabled = false;
            p.gvCheckOut.Enabled = true;
            p.txtREMARK.Enabled = false;
            p.txtUNI_NO.ClientEnabled = false;
            p.txtUNI_TITLE.ClientEnabled = false;

            DataRow drM = Head.Rows[0];
            p.gvCheckOut.FindChildControl<ASPxButton>("btnCash").Enabled = false;
            p.gvCheckOut.FindChildControl<ASPxButton>("btnOffLineCredit").Enabled = false;

            DataTable dtPayMode = p.Facade.getValidPaymentMode();
            if (dtPayMode != null && dtPayMode.Rows.Count > 0)
            {
                foreach (DataRow drPayMode in dtPayMode.Rows)
                {
                    if (drPayMode[0] != null && StringUtil.CStr(drPayMode[0]) == "1")
                        p.gvCheckOut.FindChildControl<ASPxButton>("btnCash").Enabled = true;
                    else if (drPayMode[0] != null && StringUtil.CStr(drPayMode[0]) == "3")
                        p.gvCheckOut.FindChildControl<ASPxButton>("btnOffLineCredit").Enabled = true;
                }
            }
            if (StringUtil.CStr(drM["SALE_TYPE"]) == "2")    //HappyGo支付只適用【帳單代收】
            {
                DataRow[] drs = Detail.Select(" PRODNO = '" + p.hidETCProdNo + "'");
                if (drs != null && drs.Length > 0)
                {   //結帳目中有ETC/NCIC/SEEDNET等只能使用現金結帳
                    p.gvCheckOut.FindChildControl<ASPxButton>("btnOffLineCredit").Enabled = false;
                }
                drs = Detail.Select(" BARCODE1 is not null AND BARCODE2 is not null AND BARCODE3 is not null ");
                if (drs != null && drs.Length > 0)
                {   //結帳目中有ETC/NCIC/SEEDNET等只能使用現金結帳
                    p.gvCheckOut.FindChildControl<ASPxButton>("btnOffLineCredit").Enabled = false;
                }
            }
            p.rbINVOICE_TYPE.ReadOnly = true;
            p.lbINVOICE_NO.ClientEnabled = false;
            p.txtInv_Date.ClientEnabled = false;
            p.txtUNI_NO.ClientEnabled = false;
            p.txtUNI_TITLE.ClientEnabled = false;
            //信用卡只能支付一次
            DataRow[] drsCredit = Paid.Select(" PAID_MODE = '3' ");
            if (drsCredit != null && drsCredit.Length > 0)
            {
                p.gvCheckOut.FindChildControl<ASPxButton>("btnOffLineCredit").Enabled = false;
            }
        }

        /// <summary>
        /// 進入已結帳模式
        /// </summary>
        /// <param name="p"></param>
        public void Mode_CheckOut(VSS_SAL_SA11 p)
        {
            p.btnOrderCheckOut.Enabled = false;
            //p.btnOrderCancel.Enabled = false;
            p.btnConfirm.Enabled = false;
            p.btnCancel.Enabled = false;
            p.gvMaster.Enabled = false;
            p.gvCheckOut.Enabled = false;
            p.txtREMARK.Enabled = false;
            p.txtUNI_NO.Enabled = false;
            p.txtUNI_TITLE.Enabled = false;
            p.gvMaster.FindChildControl<ASPxButton>("btnItemAdd").Enabled = true;
            p.rbINVOICE_TYPE.ReadOnly = true;
            p.lbINVOICE_NO.ClientEnabled = false;
            p.txtUNI_TITLE.ClientEnabled = false;
            p.txtUNI_NO.ClientEnabled = false;
            p.txtInv_Date.ClientEnabled = false;
        }
        #endregion
        #region 計算總金額
        public void calTotalAmount(VSS_SAL_SA11 p)
        {
            int SALE_AFTER_TOTAL_AMOUNT = 0;
            foreach (DataRow dr in Detail.Rows)//明細資料 含 ITME 1,2,3應收 和 5,6折扣類 不含5未結清單折扣 
            {  //總金額計算
                if (dr["TOTAL_AMOUNT"] != DBNull.Value && dr["TOTAL_AMOUNT"] != null
                            && StringUtil.CStr(dr["TOTAL_AMOUNT"]) != "" && NumberUtil.IsNumeric(StringUtil.CStr(dr["TOTAL_AMOUNT"])))
                    SALE_AFTER_TOTAL_AMOUNT += int.Parse(StringUtil.CStr(dr["TOTAL_AMOUNT"]));
            }
            foreach (DataRow dr in Discount.Rows) //未結轉入才有的資料 ITEM_TYPE = 5;
            {
                if (dr["TOTAL_AMOUNT"] != DBNull.Value && dr["TOTAL_AMOUNT"] != null
                    && StringUtil.CStr(dr["TOTAL_AMOUNT"]) != "" && NumberUtil.IsNumeric(StringUtil.CStr(dr["TOTAL_AMOUNT"])))
                    SALE_AFTER_TOTAL_AMOUNT += int.Parse(StringUtil.CStr(dr["TOTAL_AMOUNT"]));
            }
            if (Head != null && Head.Rows.Count > 0)
            {
                Head.Rows[0]["SALE_TOTAL_AMOUNT"] = SALE_AFTER_TOTAL_AMOUNT;
                Head.AcceptChanges();
            }
            STORE_REC_TOTAL_AMOUNT = SALE_AFTER_TOTAL_AMOUNT;
            p.lbTOTAL_AMOUNT.Text = StringUtil.CStr(STORE_REC_TOTAL_AMOUNT);
            p.lbPayAmount.Text = "";
            p.lbChange.Text = "";
        }
        #endregion 計算總金額
        #region 稅額計算
        public void CalculationTax(VSS_SAL_SA11 p)
        {
            #region 設定稅額預設金額
            if (Detail.Columns.IndexOf("INV_TYPE") < 0)
                Detail.Columns.Add("INV_TYPE");
            if (Discount.Columns.IndexOf("INV_TYPE") < 0)
                Discount.Columns.Add("INV_TYPE");
            if (Head.Rows[0]["SALE_TYPE"] != null && StringUtil.CStr(Head.Rows[0]["SALE_TYPE"]) == "2")
            {   //代收交易
                StringBuilder invProdLis = new StringBuilder("");
                DataTable dtInvProd = p.Facade.getInvoiceProduct();
                if (dtInvProd != null && dtInvProd.Rows.Count > 0)
                {
                    foreach (DataRow dr in dtInvProd.Rows)
                    {
                        if (dr[0] != null && StringUtil.CStr(dr[0]) != "")
                            invProdLis.Append(StringUtil.CStr(dr[0])).Append(",");
                    }
                }
                foreach (DataRow dr in Discount.Rows)
                {
                    if (StringUtil.CStr(invProdLis).IndexOf(StringUtil.CStr(dr["PRODNO"])) > -1)
                    {   //代收交易需要開發票貨品
                        DataTable PRODDT = new SAL01_Facade().getProduct(StringUtil.CStr(dr["PRODNO"]));
                        if (PRODDT.Rows.Count > 0)
                        {
                            dr["TAXABLE"] = StringUtil.CStr(PRODDT.Rows[0]["TAXABLE"]);  //課稅別
                            dr["TAXRATE"] = StringUtil.CStr(PRODDT.Rows[0]["TAXRATE"]);  //稅率
                        }
                        if (StringUtil.CStr(dr["TAXABLE"]) != "Y")
                            dr["TAXABLE"] = "Y";
                        dr["INV_TYPE"] = "1";
                    }
                    else
                    {   //代收交易不開發票貨品,一律開收據,稅算為0
                        dr["TAXABLE"] = "N";  //課稅別
                        dr["TAXRATE"] = 0;    //稅率
                        dr["TAX"] = 0;                                         //稅額
                        dr["BEFORE_TAX"] = dr["TOTAL_AMOUNT"];                 //稅前金額
                        dr["INV_TYPE"] = "3";
                    }
                }
                Discount.AcceptChanges();
                foreach (DataRow dr in Detail.Rows)
                {
                    if (StringUtil.CStr(invProdLis).IndexOf(StringUtil.CStr(dr["PRODNO"])) > -1)
                    {   //代收交易需要開發票貨品
                        DataTable PRODDT = new SAL01_Facade().getProduct(StringUtil.CStr(dr["PRODNO"]));
                        if (PRODDT.Rows.Count > 0)
                        {
                            dr["TAXABLE"] = StringUtil.CStr(PRODDT.Rows[0]["TAXABLE"]);  //課稅別
                            dr["TAXRATE"] = StringUtil.CStr(PRODDT.Rows[0]["TAXRATE"]);  //稅率
                        }
                        if (StringUtil.CStr(dr["TAXABLE"]) != "Y")
                            dr["TAXABLE"] = "Y";
                        dr["INV_TYPE"] = "1";
                    }
                    else
                    {   //代收交易不開發票貨品,一律開收據,稅算為0
                        dr["TAXABLE"] = "N";  //課稅別
                        dr["TAXRATE"] = 0;    //稅率
                        dr["TAX"] = 0;                                         //稅額
                        dr["BEFORE_TAX"] = dr["TOTAL_AMOUNT"];                 //稅前金額
                        dr["INV_TYPE"] = "3";
                    }
                }
                Detail.AcceptChanges();
            }
            else
            {   //一般交易
                StringBuilder nonInvProdLis = new StringBuilder("");
                DataTable dtNonInvProd = p.Facade.getNonInvoiceProduct();
                if (dtNonInvProd != null && dtNonInvProd.Rows.Count > 0)
                {
                    foreach (DataRow dr in dtNonInvProd.Rows)
                    {
                        if (dr[0] != null && StringUtil.CStr(dr[0]) != "")
                            nonInvProdLis.Append(StringUtil.CStr(dr[0])).Append(",");
                    }
                }
                foreach (DataRow dr in Discount.Rows)
                {
                    if (StringUtil.CStr(nonInvProdLis).IndexOf(StringUtil.CStr(dr["PRODNO"])) > -1)
                    {   //一般交易不開發票貨品,一律開收據,稅算為0
                        dr["TAXABLE"] = "N";  //課稅別
                        dr["TAXRATE"] = 0;    //稅率
                        dr["TAX"] = 0;                                         //稅額
                        dr["BEFORE_TAX"] = dr["TOTAL_AMOUNT"];                 //稅前金額
                        dr["INV_TYPE"] = "3";
                    }
                    else
                    {   //一般交易非不開發票貨品
                        DataTable PRODDT = new SAL01_Facade().getProduct(StringUtil.CStr(dr["PRODNO"]));
                        if (PRODDT.Rows.Count > 0)
                        {
                            dr["TAXABLE"] = StringUtil.CStr(PRODDT.Rows[0]["TAXABLE"]);  //課稅別
                            dr["TAXRATE"] = StringUtil.CStr(PRODDT.Rows[0]["TAXRATE"]);  //稅率
                        }
                        if (StringUtil.CStr(dr["TAXABLE"]) != "Y")
                        {
                            dr["TAX"] = 0;                                          //稅額
                            dr["BEFORE_TAX"] = dr["TOTAL_AMOUNT"];                  //稅前金額
                        }
                        dr["INV_TYPE"] = "1";
                    }
                }
                Discount.AcceptChanges();
                foreach (DataRow dr in Detail.Rows)
                {
                    if (StringUtil.CStr(nonInvProdLis).IndexOf(StringUtil.CStr(dr["PRODNO"])) > -1)
                    {   //一般交易不開發票貨品,一律開收據,稅算為0
                        dr["TAXABLE"] = "N";  //課稅別
                        dr["TAXRATE"] = 0;    //稅率
                        dr["TAX"] = 0;                                         //稅額
                        dr["BEFORE_TAX"] = dr["TOTAL_AMOUNT"];                 //稅前金額
                        dr["INV_TYPE"] = "3";
                    }
                    else
                    {   //一般交易非不開發票貨品
                        DataTable PRODDT = new SAL01_Facade().getProduct(StringUtil.CStr(dr["PRODNO"]));
                        if (PRODDT.Rows.Count > 0)
                        {
                            dr["TAXABLE"] = StringUtil.CStr(PRODDT.Rows[0]["TAXABLE"]);  //課稅別
                            dr["TAXRATE"] = StringUtil.CStr(PRODDT.Rows[0]["TAXRATE"]);  //稅率
                        }
                        if (StringUtil.CStr(dr["TAXABLE"]) != "Y")
                        {
                            dr["TAX"] = 0;                                          //稅額
                            dr["BEFORE_TAX"] = dr["TOTAL_AMOUNT"];                  //稅前金額
                        }
                        dr["INV_TYPE"] = "1";
                    }
                }
                Detail.AcceptChanges();
            }
            #region 所有在Product產品主檔中,可以查到商品料號的產品,開立發票與否,都依據INV_TYPE欄位的設定
            Product_Facade prodFacade = new Product_Facade();
            if (Detail != null && Detail.Rows.Count > 0)
            {
                foreach (DataRow dr in Detail.Rows)
                {
                    if (dr["PRODNO"] != null && !string.IsNullOrEmpty(StringUtil.CStr(dr["PRODNO"])))
                    {
                        DataTable dtProd = prodFacade.Query_ProductAllInfo_By_ProdNo(StringUtil.CStr(dr["PRODNO"]));
                        if (dtProd != null && dtProd.Rows.Count > 0)
                        {
                            dr["INV_TYPE"] = dtProd.Rows[0]["INV_TYPE"];
                        }
                    }
                }
                Detail.AcceptChanges();
            }
            if (Discount != null && Discount.Rows.Count > 0)
            {
                foreach (DataRow dr in Discount.Rows)
                {
                    if (dr["PRODNO"] != null && !string.IsNullOrEmpty(StringUtil.CStr(dr["PRODNO"])))
                    {
                        DataTable dtProd = prodFacade.Query_ProductAllInfo_By_ProdNo(StringUtil.CStr(dr["PRODNO"]));
                        if (dtProd != null && dtProd.Rows.Count > 0)
                        {
                            dr["INV_TYPE"] = dtProd.Rows[0]["INV_TYPE"];
                        }
                    }
                }
                Discount.AcceptChanges();
            }
            #endregion 所有在Product產品主檔中,可以查到商品料號的產品,開立發票與否,都依據INV_TYPE欄位的設定
            #endregion
            //ITEM_TYPE = 1,2,3之項目+項資料 ,其餘為折扣項目-項
            #region 總金額計算       2010/11/27
            int TAXRATE = 5; //照這算法TAXRATE 就沒作用了
            int SALE_AFTER_TOTAL_AMOUNT = 0;         ///銷售稅後總金額  含稅+不稅之應收總金額
            int SALE_AFTER_TAX_TOTAL_AMOUNT = 0;     ///銷售應課稅產品總金額 只有含稅之應收總金額
            int SALE_TOTAL_TAX = 0;                  ///總稅額   SALE_AFTER_TAX_TOTAL_AMOUNT * (5/105)
            int SALE_BEFORE_TOTAL_AMOUNT = 0;        ///銷售稅前金額 =  SALE_AFTER_TAX_TOTAL_AMOUNT - SALE_TOTAL_TAX         
            int DISCOUNT_AFTER_TOTAL_AMOUNT = 0;     ///折扣稅後總金額
            int DISCOUNT_AFTER_TAX_TOTAL_AMOUNT = 0; ///折扣應課稅產品總金額  
            int DISCOUNT_TOTAL_TAX = 0;              ///總稅額   DISCOUNT_AFTER_TAX_TOTAL_AMOUNT * (5/105)
            int DISCOUNT_BEFORE_TOTAL_AMOUNT = 0;    ///銷售稅前金額 =  DISCOUNT_AFTER_TAX_TOTAL_AMOUNT - DISCOUNT_TOTAL_TAX

            foreach (DataRow dr in Detail.Rows)//明細資料 含 ITME 1,2,3應收 和 5,6折扣類 不含5未結清單折扣 
            {  //總金額計算
                switch (StringUtil.CStr(dr["ITEM_TYPE"]))
                {
                    case "1"://單品
                    case "2"://服務系統,組合促銷等來自於未結明細的資料
                    case "3"://預購轉銷售
                    case "7"://加價購商品
                    case "8"://HG來店禮
                    case "9"://授信通聯
                    case "10"://租賃
                    case "13"://贈品
                    case "14"://Happy Go加價購
                        if (StringUtil.CStr(dr["TAXABLE"]) == "Y") //Y:應稅N:免稅;Y:應稅，taxrate為 0，為零稅
                            if (dr["TOTAL_AMOUNT"] != DBNull.Value && dr["TOTAL_AMOUNT"] != null
                                && StringUtil.CStr(dr["TOTAL_AMOUNT"]) != "" && NumberUtil.IsNumeric(StringUtil.CStr(dr["TOTAL_AMOUNT"])))
                                SALE_AFTER_TAX_TOTAL_AMOUNT += int.Parse(StringUtil.CStr(dr["TOTAL_AMOUNT"]));
                        break;
                    case "4"://Happy Go折抵
                    case "6"://店長折扣
                        if (dr["TOTAL_AMOUNT"] != DBNull.Value && dr["TOTAL_AMOUNT"] != null
                            && StringUtil.CStr(dr["TOTAL_AMOUNT"]) != "" && NumberUtil.IsNumeric(StringUtil.CStr(dr["TOTAL_AMOUNT"])))
                            DISCOUNT_AFTER_TOTAL_AMOUNT += int.Parse(StringUtil.CStr(dr["TOTAL_AMOUNT"]));
                        if (StringUtil.CStr(dr["TAXABLE"]) == "Y") //Y:應稅N:免稅;Y:應稅，taxrate為 0，為零稅
                            if (dr["TOTAL_AMOUNT"] != DBNull.Value && dr["TOTAL_AMOUNT"] != null
                                && StringUtil.CStr(dr["TOTAL_AMOUNT"]) != "" && NumberUtil.IsNumeric(StringUtil.CStr(dr["TOTAL_AMOUNT"])))
                                DISCOUNT_AFTER_TAX_TOTAL_AMOUNT += int.Parse(StringUtil.CStr(dr["TOTAL_AMOUNT"]));
                        break;
                }
            }

            foreach (DataRow dr in Discount.Rows) //未結轉入才有的資料 ITEM_TYPE = 5,11,12;
            {
                if (dr["TOTAL_AMOUNT"] != DBNull.Value && dr["TOTAL_AMOUNT"] != null
                    && StringUtil.CStr(dr["TOTAL_AMOUNT"]) != "" && NumberUtil.IsNumeric(StringUtil.CStr(dr["TOTAL_AMOUNT"])))
                    DISCOUNT_AFTER_TOTAL_AMOUNT += int.Parse(StringUtil.CStr(dr["TOTAL_AMOUNT"]));
                if (StringUtil.CStr(dr["TAXABLE"]) == "Y") //Y:應稅N:免稅;Y:應稅，taxrate為 0，為零稅
                    if (dr["TOTAL_AMOUNT"] != DBNull.Value && dr["TOTAL_AMOUNT"] != null
                        && StringUtil.CStr(dr["TOTAL_AMOUNT"]) != "" && NumberUtil.IsNumeric(StringUtil.CStr(dr["TOTAL_AMOUNT"])))
                        DISCOUNT_AFTER_TAX_TOTAL_AMOUNT += int.Parse(StringUtil.CStr(dr["TOTAL_AMOUNT"]));
            }

            if (Head.Rows[0]["SALE_TOTAL_AMOUNT"] != null && StringUtil.CStr(Head.Rows[0]["SALE_TOTAL_AMOUNT"]) != ""
                && NumberUtil.IsNumeric(StringUtil.CStr(Head.Rows[0]["SALE_TOTAL_AMOUNT"])))
                SALE_AFTER_TOTAL_AMOUNT = int.Parse(StringUtil.CStr(Head.Rows[0]["SALE_TOTAL_AMOUNT"]));

            if (SALE_AFTER_TOTAL_AMOUNT > 0)
            {
                if (TAXRATE >= 0)
                {
                    SALE_TOTAL_TAX = (int)Math.Round((double)((SALE_AFTER_TAX_TOTAL_AMOUNT + DISCOUNT_AFTER_TAX_TOTAL_AMOUNT) * TAXRATE / (100 + TAXRATE)));//四捨五入
                    SALE_BEFORE_TOTAL_AMOUNT = SALE_AFTER_TOTAL_AMOUNT - SALE_TOTAL_TAX;
                    //折扣
                    DISCOUNT_TOTAL_TAX = (int)Math.Round((double)(DISCOUNT_AFTER_TAX_TOTAL_AMOUNT * (TAXRATE / (100 + TAXRATE)))); //四捨五入
                    DISCOUNT_BEFORE_TOTAL_AMOUNT = DISCOUNT_AFTER_TOTAL_AMOUNT - DISCOUNT_TOTAL_TAX;
                }
                else
                {
                    SALE_TOTAL_TAX = 0;
                    SALE_BEFORE_TOTAL_AMOUNT = SALE_AFTER_TOTAL_AMOUNT;
                    //折扣
                    DISCOUNT_TOTAL_TAX = 0;
                    DISCOUNT_BEFORE_TOTAL_AMOUNT = DISCOUNT_AFTER_TOTAL_AMOUNT;
                }
            }
            else
            {   //交易總金額小於等於0,不開發票,稅視為0
                SALE_TOTAL_TAX = DISCOUNT_TOTAL_TAX = 0;
                SALE_BEFORE_TOTAL_AMOUNT = SALE_AFTER_TOTAL_AMOUNT;
                DISCOUNT_BEFORE_TOTAL_AMOUNT = DISCOUNT_AFTER_TOTAL_AMOUNT;
            }

            foreach (DataRow dr in Head.Rows)
            {
                dr["SALE_TOTAL_AMOUNT"] = SALE_AFTER_TOTAL_AMOUNT;
                dr["SALE_TAX"] = SALE_TOTAL_TAX;
                dr["SALE_BEFORE_TAX"] = SALE_BEFORE_TOTAL_AMOUNT;
                dr["DISCOUNT_TOTAL_AMOUNT"] = DISCOUNT_AFTER_TOTAL_AMOUNT;
                dr["DISCOUNT_TAX"] = DISCOUNT_TOTAL_TAX;
                dr["DISCOUNT_BEFORE_TAX"] = DISCOUNT_BEFORE_TOTAL_AMOUNT;
            }

            Head.AcceptChanges();
            STORE_REC_TOTAL_AMOUNT = SALE_AFTER_TOTAL_AMOUNT;
            #endregion
            #region 計算明細稅額
            DataTable totalDetal = new DataTable();
            totalDetal.Merge(Detail);
            totalDetal.Merge(Discount);
            totalDetal = CalDetailTax(totalDetal);
            DataRow[] drsDetail = totalDetal.Select(" ITEM_TYPE IN ('1','2','3','4','6','7','8','9','10','13','14') ");
            if (drsDetail != null && drsDetail.Length > 0)
                Detail = drsDetail.CopyToDataTable();
            DataRow[] drsDiscount = totalDetal.Select(" ITEM_TYPE IN ('5','11','12') ");
            if (drsDiscount != null && drsDiscount.Length > 0)
                Discount = drsDiscount.CopyToDataTable();
            //--------------------------------------
            #endregion
            #region 付款金額 找零金
            p.lbPayAmount.Text = "";
            p.lbChange.Text = "";
            #endregion
        }
        #endregion
        #region 計算明細稅 CalDetailTax
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Detail"></param>
        /// <returns></returns>
        private DataTable CalDetailTax(DataTable Detail)
        {


            //明細總價
            double DETAIL_TOTALE_AMOUNT = 0;
            //明細總稅
            double DETAIL_TOTAL_TAX = 0;
            //明細稅
            double DETAIL_TAX = 0;

            double DETAIL_AMOUNT = 0;
            //
            double DETAIL_BEFORE_TOTAL_AMOUNT = 0;
            //
            double DETAIL_BEFORE_AMOUNT = 0;


            //單價計算
            foreach (DataRow row in Detail.Rows)
            {
                string INV_TYPE = StringUtil.CStr(row["INV_TYPE"]);
                string TAXABLE = StringUtil.CStr(row["TAXABLE"]);
                string TOTAL_AMOUNT = StringUtil.CStr(row["TOTAL_AMOUNT"]);
                if (INV_TYPE == "1" && TAXABLE == "Y")
                {
                    if (TOTAL_AMOUNT != "" && NumberUtil.IsNumeric(TOTAL_AMOUNT))
                        DETAIL_AMOUNT = int.Parse(TOTAL_AMOUNT);
                    else
                        DETAIL_AMOUNT = 0;
                    //稅前 = 單價 * 100 / 105
                    DETAIL_BEFORE_AMOUNT = (int)Math.Round((double)(DETAIL_AMOUNT * 100 / 105), MidpointRounding.AwayFromZero);// [sale_detail].sale_tax= [sale_head].sale_tax * ([sale_detail].total_amount/X)                    
                    DETAIL_TAX = DETAIL_AMOUNT - DETAIL_BEFORE_AMOUNT;

                    DETAIL_BEFORE_TOTAL_AMOUNT += DETAIL_BEFORE_AMOUNT;
                    DETAIL_TOTAL_TAX += DETAIL_TAX;
                    DETAIL_TOTALE_AMOUNT += DETAIL_AMOUNT;
                    row.BeginEdit();
                    row["TAX"] = DETAIL_TAX;
                    row["BEFORE_TAX"] = DETAIL_BEFORE_AMOUNT;
                    row.EndEdit();
                }
                else
                {
                    row.BeginEdit();
                    row["TAX"] = 0;
                    row["BEFORE_TAX"] = 0;
                    row.EndEdit();
                }
            }

            double SALE_BEFORE_TOTAL_TAX = (int)Math.Round((double)(DETAIL_TOTALE_AMOUNT * 100 / 105), MidpointRounding.AwayFromZero);
            double SALE_TOTAL_TAX = DETAIL_TOTALE_AMOUNT - SALE_BEFORE_TOTAL_TAX;
            //除稅
            if (Detail.Rows.Count > 0)
            {
                if (DETAIL_BEFORE_TOTAL_AMOUNT != SALE_BEFORE_TOTAL_TAX)
                {
                    double before_amount = SALE_BEFORE_TOTAL_TAX - DETAIL_BEFORE_TOTAL_AMOUNT;
                    //抓出金額最高的來做除稅

                    DataRow row = Detail.Rows[Detail.Rows.Count - 1];
                    row.BeginEdit();
                    row["BEFORE_TAX"] = Convert.ToInt32(row["BEFORE_TAX"]) + before_amount;
                    row.EndEdit();
                }
            }
            Detail.AcceptChanges();
            if (Head != null && Head.Rows.Count > 0)
            {
                DataRow drHead = Head.Rows[0];
                drHead["SALE_BEFORE_TAX"] = SALE_BEFORE_TOTAL_TAX;
                drHead["SALE_TAX"] = SALE_TOTAL_TAX;
                drHead["INVOICE_TOTAL_AMOUNT"] = DETAIL_TOTALE_AMOUNT;
                drHead["SALE_BEFORE_TAX"] = SALE_BEFORE_TOTAL_TAX;
            }
            Head.AcceptChanges();
            return Detail;
        }
        #endregion
        #region 計算應付金額,找零金
        public void CalPayCharge(VSS_SAL_SA11 p)
        {
            #region 門市應收總金額 付款金額 找零金
            int STORE_PAY_TOTAL_AMOUNT = 0;
            foreach (DataRow dr in Paid.Rows)
                if (dr["PAID_AMOUNT"] != DBNull.Value && dr["PAID_AMOUNT"] != null)
                    STORE_PAY_TOTAL_AMOUNT += int.Parse(StringUtil.CStr(dr["PAID_AMOUNT"]));
            int STORE_REC_TOTAL_AMOUNT = 0;
            if (p.lbTOTAL_AMOUNT.Text != "" && NumberUtil.IsNumeric(p.lbTOTAL_AMOUNT.Text))
                STORE_REC_TOTAL_AMOUNT = int.Parse(p.lbTOTAL_AMOUNT.Text);
            int STORE_CHANGE_AMOUNT = STORE_PAY_TOTAL_AMOUNT - STORE_REC_TOTAL_AMOUNT;
            if (Paid.Rows.Count > 0)
            {
                int shouldPay = STORE_REC_TOTAL_AMOUNT - STORE_PAY_TOTAL_AMOUNT;
                if (shouldPay < 0)
                    shouldPay = 0;
                p.lbPayAmount.Text = StringUtil.CStr(shouldPay);
                if (shouldPay == 0)
                    p.btnOrderCheckOut.Enabled = true;
                else
                    p.btnOrderCheckOut.Enabled = false;

                if (STORE_CHANGE_AMOUNT > 0)
                    p.lbChange.Text = StringUtil.CStr(STORE_CHANGE_AMOUNT);
                else
                    p.lbChange.Text = "0";
                DataRow[] drsPaid = Paid.Select(" PAID_MODE IN ('1','5','6','7') ");
                if (drsPaid == null || drsPaid.Length == 0)
                {   //純信用卡類付款，不可找零
                    p.lbChange.Text = "0";
                }
            }
            else
            {
                p.lbPayAmount.Text = p.lbTOTAL_AMOUNT.Text;
                p.lbChange.Text = "0";
            }
            #endregion
        }
        #endregion
    }
    string _SRC_TYPE
    {
        get
        {
            return Session["SALE_SRC_TYPE"] as string;
        }
    }
    string _PKEY
    {
        get
        {
            //return Request.QueryString["PKEY"] as string;

            //**2011/04/21 Tina：前頁傳遞參數時，會先以加密處理，在此要解密。
            string s = "";
            if (!string.IsNullOrEmpty(Request.QueryString["Param"]))
            {
                NameValueCollection qscoll = Utils.Param_UrlDecode(Request.QueryString["Param"]);
                foreach (string key in qscoll.AllKeys)
                {
                    if (key == "PKEY")
                    {
                        s = string.Join(",", qscoll.GetValues(key));
                        break;
                    }
                }
            }

            return s;
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
    SAL01_Facade Facade
    {
        get
        {
            return new SAL01_Facade();
        }

    }
    string __EVENTTARGET
    {
        get
        {
            string r = Request["__EVENTTARGET"] as string;
            return r == null ? "" : r;
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
    /// 從未結清單頁面載入
    /// </summary>
    void LoadSAL05Data()
    {
        TempTables.Head = Facade.getTO_CLOSE_HEAD(_PKEY);
        Double totAmt = 0;
        if (TempTables.Head.Rows.Count > 0)
        {
            string POSUUID_MASTER = StringUtil.CStr(Advtek.Utility.GuidNo.getUUID());
            foreach (DataRow headRow in TempTables.Head.Rows)
            {
                headRow["TRADE_DATE"] = Advtek.Utility.OracleDBUtil.WorkDay(logMsg.STORENO);
                headRow["POSUUID_MASTER"] = POSUUID_MASTER;//先取出MASTER_UUID
                headRow["INVOICE_TYPE"] = rbINVOICE_TYPE.Value;
            }
            TempTables.Head.AcceptChanges();

            //取得表身資料取得多筆 未結清表身資料  1-2 ,1-3,1-1 取得 2+3+1 = 5筆資料
            TempTables.Detail = Facade.getTO_CLOSE_ITEM(_PKEY, "2");//未結清單表身
            TempTables.Detail.Columns.Add("ON_HAND_QTY");
            Session["HGDISTempTable"] = TempTables.Detail; //for HappyGo折抵時，查詢【促銷商品折抵活動】，以及【單商品折抵活動】使用。
            TempTables.Discount = Facade.getTO_CLOSE_ITEM(_PKEY, "5");//未結折扣資料

            foreach (DataRow dr in TempTables.Detail.Rows)
            {
                dr["POSUUID_MASTER"] = TempTables.Head.Rows[0];
                dr["ON_HAND_QTY"] = 999;
                if (dr["TOTAL_AMOUNT"] != DBNull.Value && dr["TOTAL_AMOUNT"] != null)
                    totAmt += Convert.ToDouble(dr["TOTAL_AMOUNT"]);
            }
            TempTables.Detail.AcceptChanges();
            foreach (DataRow dr in TempTables.Discount.Rows)
            {
                string discountType = Facade.getDISCOUNT_TYPE(StringUtil.CStr(dr["PRODNO"]));
                if (discountType == "2")
                    dr["ITEM_TYPE"] = "12"; //舊機回收折扣
                else if (discountType == "3")
                    dr["ITEM_TYPE"] = "11"; //租賃折扣
                dr["POSUUID_MASTER"] = TempTables.Head.Rows[0];
                if (dr["TOTAL_AMOUNT"] != DBNull.Value && dr["TOTAL_AMOUNT"] != null)
                    totAmt += Convert.ToDouble(dr["TOTAL_AMOUNT"]);
            }
            TempTables.Discount.AcceptChanges();
            lbTOTAL_AMOUNT.Text = StringUtil.CStr(totAmt);


            gvMaster.DataSource = TempTables.Detail; //貨品資料
            gvMaster.DataBind();

            gvDetail.Visible = true;//只有未結而來折扣資料才會放在這裏 未結折扣資料
            gvDetail.DataSource = TempTables.Discount;
            gvDetail.DataBind();

            TempTables.Paid = Facade.getPAID_DETAIL("");
            gvCheckOut.DataSource = TempTables.Paid;
            gvCheckOut.DataBind();
            Session["TempTable"] = TempTables;
            //not TEST            
            TempTables.calTotalAmount(this);
            TempTables.SaleHeadDataBind(this);

            //設定畫面為有商品狀態
            this.TempTables.Mode_HavePROD(this);

            //將目前POSUUID_MASTER回填TO_CLOSE_HEAD,狀態設為未結
            Facade.UpdateUnCloseHead(TempTables.Head, "1", logMsg.MODI_USER);

            //商品料號為SIM，要提醒使用者選取真實的商品料號
            DataRow[] drsSIM = TempTables.Detail.Select(" PRODNO = 'SIM' ");
            if (drsSIM != null && drsSIM.Length > 0)
            {
                ScriptManager.RegisterClientScriptBlock(this, typeof(string), "SIMPROD", "alert('請選取實際的SIM卡商品料號!');", true);
            }
        }
    }

    /// <summary>
    /// 從銷售查詢頁面載入資料
    /// </summary>
    void LoadSAL02Data()
    {
        string POSUUID_MASTER = _PKEY;
        TempTables.Head = Facade.getSale_Head(POSUUID_MASTER);
        TempTables.Detail = Facade.getSale_Detail(POSUUID_MASTER, "1,2,3,4,6,7,8,9,10,13", logMsg.STORENO, this._STOCK);
        Session["HGDISTempTable"] = TempTables.Detail; //for HappyGo折抵時，查詢【促銷商品折抵活動】，以及【單商品折抵活動】使用。
        TempTables.Discount = Facade.getSale_Detail(POSUUID_MASTER, "5,11,12");
        TempTables.Paid = Facade.getPaid_Detail(POSUUID_MASTER);
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
        TempTables.calTotalAmount(this);
        TempTables.CalculationTax(this);
        TempTables.SaleHeadDataBind(this);
        TempTables.Mode_CheckOut(this);
    }

    /// <summary>
    /// 直接從銷售單新增資料時
    /// </summary>
    void LoadSAL11Data()
    {
        string POSUUID_MASTER = "";
        TempTables.Head = Facade.getSale_Head(POSUUID_MASTER);
        TempTables.Detail = Facade.getSale_Detail(POSUUID_MASTER, "1,2,3,4,6,7,8,9,10,13", logMsg.STORENO, this._STOCK);
        Session["HGDISTempTable"] = TempTables.Detail; //for HappyGo折抵時，查詢【促銷商品折抵活動】，以及【單商品折抵活動】使用。
        TempTables.Discount = Facade.getSale_Detail(POSUUID_MASTER, "5,11,12");
        TempTables.Paid = Facade.getPaid_Detail(POSUUID_MASTER);
        gvMaster.DataSource = TempTables.Detail;
        gvMaster.DataBind();
        gvDetail.DataSource = TempTables.Discount;
        gvDetail.DataBind();
        gvCheckOut.DataSource = TempTables.Paid;
        gvCheckOut.DataBind();
        Session["TempTable"] = TempTables;
        TempTables.calTotalAmount(this);
        TempTables.SaleHeadDataBind(this);
        //SALE_IMEI資料動態才給
        TempTables.Mode_NoPROD(this);
    }

    /// <summary>
    /// 載入Cache 資料 當不正常關閉時且已產生金額資料時 回載原資料
    /// </summary>
    void LoadFromCache(string POSUUID_MASTER)
    {
        TempTables.Head = Facade.getSale_Head(POSUUID_MASTER);
        TempTables.Detail = Facade.getSale_Detail(POSUUID_MASTER, "1,2,3,4,6,7,8,9,10,13", logMsg.STORENO, this._STOCK);
        TempTables.Discount = Facade.getSale_Detail(POSUUID_MASTER, "5,11,12");
        TempTables.Paid = Facade.getPaid_Detail(POSUUID_MASTER);
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
        TempTables.calTotalAmount(this);
        TempTables.CalculationTax(this);
        TempTables.SaleHeadDataBind(this);
        //cache時一定是在付款模式
        TempTables.Mode_PayCash(this);
        TempTables.CalPayCharge(this);
        if (lbPayAmount.Text == "0" && TempTables.Paid.Rows.Count > 0)
            btnOrderCheckOut.Enabled = true;
        if (TempTables.Discount != null && TempTables.Discount.Rows.Count > 0)
            gvDetail.Visible = true;
    }

    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack && !IsCallback)
        {
            //Session["SALE_SRC_TYPE"] = Request.QueryString["SRC_TYPE"];

            //**2011/04/21 Tina：前頁傳遞參數時，會先以加密處理，在此要解密。
            if (!string.IsNullOrEmpty(Request.QueryString["Param"]))
            {
                NameValueCollection qscoll = Utils.Param_UrlDecode(Request.QueryString["Param"]);
                foreach (string key in qscoll.AllKeys)
                {
                    if (key == "SRC_TYPE")
                    {
                        Session["SALE_SRC_TYPE"] = string.Join(",", qscoll.GetValues(key));
                        break;
                    }
                }
            }
            else
            {
                Session["SALE_SRC_TYPE"] = null;
            }

            this.hidStore_No.Value = logMsg.STORENO;
            this.txtInv_Date.MaxDate = DateTime.Today;
            //已付款但未完成結帳,將重新載入最後一次未正常結帳資料
            DataTable CacheDT = Facade.CheckSaleCacheData(logMsg.STORENO, logMsg.OPERATOR, "8");
            if (CacheDT.Rows.Count == 0)
            {
                switch (_SRC_TYPE)
                {
                    case "SAL05": LoadSAL05Data(); break;//取得未結清單來的資料
                    case "SAL02": LoadSAL02Data(); break;//
                    default: 
                        LoadSAL11Data();
                        this.Page.FindChildControl<ASPxPopupControl>("pcSAL11").ShowOnPageLoad = true; 
                        break;
                }
            }
            else //已付款但未正常結帳資料
            {
                LoadFromCache(StringUtil.CStr(CacheDT.Rows[0]["POSUUID_MASTER"]));
                ScriptManager.RegisterClientScriptBlock(this, typeof(string), "LoadCache", "alert('系統偵測發現有已付款但未正常結帳資料,將載入未結帳資料!');", true);
                //將銷售單資料來源設為Cache
                Session["SALE_SRC_TYPE"] = "Cache";
            }
        }
    }

    protected void ASPxButton1_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/VSS/SAL/SAL02/SAL02.aspx");
    }

    protected void ASPxButton2_Click(object sender, EventArgs e)
    {
       // Response.Redirect("~/VSS/SAL/TSAL05/TSAL05.aspx?s=1");

        //**2011/04/21 Tina：傳遞參數時，要先以加密處理。
        string encryptUrl = Utils.Param_Encrypt("s=1");
        Response.Redirect(string.Format("~/VSS/SAL/TSAL05/TSAL05.aspx?Param={0}", encryptUrl));
    }

    protected void btnItemAdd_Click(object sender, EventArgs e)
    {
        TempTables.SaveTempTable(this);
        TempTables.NewRow_Detail(gvMaster);
        //新增單品時,SOURCE_TYPE設為 11 POS, 因為此為必要欄位
        TempTables.Mode_HavePROD(this);

        PopupControl txtPRODNO = gvMaster.FindRowCellTemplateControl(0, (GridViewDataColumn)gvMaster.Columns["PRODNO"], "txtPRODNO") as PopupControl;
        if (txtPRODNO != null)
        {
            ASPxTextBox txtControl = txtPRODNO.FindControl("txtControl") as ASPxTextBox;
            txtControl.Focus();
        }
    }

    protected void btnMixPromotion_Click(object sender, EventArgs e)
    {
        TempTables.SaveTempTable(this);
        string[] args = __EVENTARGUMENT.Split(new char[] { ';' });
        TempTables.NewRecord_Detail_MixPromotion(this, gvMaster, args);
        TempTables.Mode_HavePROD(this);
    }

    protected void btnCreditCommunication_Click(object sender, EventArgs e)
    {
        TempTables.SaveTempTable(this);
        string[] args = __EVENTARGUMENT.Split(new char[] { '|' });
        TempTables.NewRow_Detail_CreditCommunication(this, gvMaster, args);
        TempTables.Mode_HavePROD(this);
    }

    protected void btnOrderCheckOut_Click(object sender, EventArgs e)
    {
        OracleConnection objConn = OracleDBUtil.GetConnection();
        OracleTransaction objTX = objConn.BeginTransaction();
        bool bCheckOut = true; //是否可結帳
        try
        {
            btnOrderCheckOut.Enabled = false;
            //處理找零
            if (lbChange.Text != "" && lbChange.Text != "0" && NumberUtil.IsNumeric(lbChange.Text))
            {
                int change = int.Parse(lbChange.Text);
                if (change > 0)
                {
                    Facade.deletePaid_Detail(StringUtil.CStr(TempTables.Head.Rows[0]["POSUUID_MASTER"]), "8");
                    DataTable dtPaid = new DataTable();
                    dtPaid = TempTables.Paid.Clone();
                    DataRow drPaid = dtPaid.NewRow();
                    drPaid["POSUUID_MASTER"] = TempTables.Head.Rows[0]["POSUUID_MASTER"];
                    drPaid["ID"] = StringUtil.CStr(Advtek.Utility.GuidNo.getUUID());
                    drPaid["PAID_AMOUNT"] = -1 * change;
                    drPaid["DESCRIPTION"] = "找零";
                    drPaid["PAID_MODE"] = 8;
                    drPaid["PAID_MODE_NAME"] = "找零";
                    dtPaid.Rows.Add(drPaid);
                    Facade.InsertPaid_Detail(dtPaid);
                }
            }

            switch (_SRC_TYPE)
            {
                case "SAL05"://未結清單
                    TempTables.Save_Sale_Data2DBFromSAL05(this, objTX);
                    break;
                case "Cache"://未正常關閉且已輸入付款資料
                    TempTables.Save_Sale_Data2DBFromCache(this, objTX);
                    break;
                default://銷貨單直接新增
                    TempTables.Save_Sale_Data2DBFromSAL11(this, objTX);
                    break;
            }

            //若有折扣資料 則更新累計折抵次數
            if (TempTables.Discount.Rows.Count > 0)
            {
                DataRow[] Sale_DiscountRows = TempTables.Discount.Select("ITEM_TYPE IN ('5','11','12')");
                if (Sale_DiscountRows.Length > 0)
                {
                    foreach (DataRow dr in Sale_DiscountRows)
                    {
                        if (dr["PRODNO"] != null && !string.IsNullOrEmpty(StringUtil.CStr(dr["PRODNO"])))
                        {
                            if (Facade.addStoreDiscountCount(logMsg.STORENO, StringUtil.CStr(dr["PRODNO"]), objTX) == -1)
                            {
                                bCheckOut = false;
                                if (dr["PRODNAME"] != null && !string.IsNullOrEmpty(StringUtil.CStr(dr["PRODNAME"])))
                                    ScriptManager.RegisterClientScriptBlock(this, typeof(string), "ERROR_MESSAGE", "alert('" + StringUtil.CStr(dr["PRODNAME"]) + " 累計折扣次數失敗');", true);
                                else
                                    ScriptManager.RegisterClientScriptBlock(this, typeof(string), "ERROR_MESSAGE", "alert('" + StringUtil.CStr(dr["PRODNO"]) + " 累計折扣次數失敗');", true);
                                objTX.Rollback();
                                break;
                            }
                        }
                    }
                }
            }

            //若有選取 HappyGo折扣 則需進行HappyGo刷卡折抵點數的動作，並且Update兌點項目累計折抵次數
            DataRow[] Sale_DetailRows = TempTables.Detail.Select("ITEM_TYPE = '4'");
            if (bCheckOut && Sale_DetailRows.Length > 0)
            {
                foreach (DataRow dr in Sale_DetailRows)
                {
                    if (dr["PRODNO"] != null && !string.IsNullOrEmpty(StringUtil.CStr(dr["PRODNO"])))
                    {
                        if (bCheckOut && !Facade.chkHGDiscountCount(StringUtil.CStr(dr["PRODNO"]), objTX))
                        {
                            bCheckOut = false;
                            if (dr["PRODNAME"] != null && !string.IsNullOrEmpty(StringUtil.CStr(dr["PRODNAME"])))
                                ScriptManager.RegisterClientScriptBlock(this, typeof(string), "ERROR_MESSAGE", "alert('" + StringUtil.CStr(dr["PRODNAME"]) + " HG折扣次數已達上限');", true);
                            else
                                ScriptManager.RegisterClientScriptBlock(this, typeof(string), "ERROR_MESSAGE", "alert('" + StringUtil.CStr(dr["PRODNO"]) + " HG折扣次數已達上限');", true);
                            objTX.Rollback();
                            break;
                        }

                        if (bCheckOut && !string.IsNullOrEmpty(StringUtil.CStr(dr["MSISDN"])))
                        {
                            int AffectRow = 0;
                            AffectRow = Facade.UpdateMemberList(objTX, StringUtil.CStr(dr["PRODNO"]), StringUtil.CStr(dr["MSISDN"]));
                            if (AffectRow == 0)
                            {
                                bCheckOut = false;
                                ScriptManager.RegisterClientScriptBlock(this, typeof(string), "ERROR_MESSAGE", "alert('變更兌點項目累計折抵次數失敗!請進行HappyGo卡退點動作');", true);
                                objTX.Rollback();
                            }
                        }
                    }
                }
            }

            //若有特殊抱怨折扣，回寫特殊抱怨已折扣金額
            DataRow[] Sale_DetailRows2 = TempTables.Detail.Select("ITEM_TYPE = '6'");
            if (bCheckOut && Sale_DetailRows2.Length > 0)
            {
                string USED_AMOUNT = "0";
                USED_AMOUNT = StringUtil.CStr(Sale_DetailRows2[0]["TOTAL_AMOUNT"]);//取得特殊抱怨折扣金額

                if (Facade.chkStoreSpecDisAmt(logMsg.STORENO, logMsg.ROLE_TYPE, objTX))
                {
                    //回寫特殊抱怨已折扣金額
                    int AffectRow = 0;
                    string strYYMM = OracleDBUtil.WorkDay(logMsg.STORENO); //營業日
                    if (StringUtil.CStr(logMsg.ROLE_TYPE) == "1" || StringUtil.CStr(logMsg.ROLE_TYPE) == "2")   //當角色為1:店長或2:店員
                    {
                        AffectRow = Facade.UpdateStoreSpecialDIS(objTX, logMsg.STORENO, strYYMM.Substring(0, 7), USED_AMOUNT, logMsg.ROLE_TYPE);
                        if (AffectRow == 0)
                        {
                            bCheckOut = false;
                            if (Sale_DetailRows != null && Sale_DetailRows.Length > 0)
                                ScriptManager.RegisterClientScriptBlock(this, typeof(string), "ERROR_MESSAGE", "alert('變更特殊抱怨已折扣金額失敗!請記得進行HappyGo卡退點動作');", true);
                            else
                                ScriptManager.RegisterClientScriptBlock(this, typeof(string), "ERROR_MESSAGE", "alert('變更特殊抱怨已折扣金額失敗!');", true);
                            objTX.Rollback();
                        }
                    }
                }
                else
                {
                    bCheckOut = false;
                    if (logMsg.ROLE_TYPE == "1")
                        ScriptManager.RegisterClientScriptBlock(this, typeof(string), "ERROR_MESSAGE", "alert('門市特殊折扣店長額度已達上限');", true);
                    else
                        ScriptManager.RegisterClientScriptBlock(this, typeof(string), "ERROR_MESSAGE", "alert('門市特殊折扣店員額度已達上限');", true);
                    objTX.Rollback();
                }
            }

            //銷售扣庫存
            INVENTORY_Facade Inventory = new INVENTORY_Facade();
            DataRow[] Sale_DetailRows5 = TempTables.Detail.Select("ITEM_TYPE IN ('1','2','3','7','8','9','10','13','14')");
            if (bCheckOut && Sale_DetailRows5.Length > 0)
            {
                foreach (DataRow dr in Sale_DetailRows5)
                {
                    string Code = "";
                    string Message = "";
                    string STOCK = this._STOCK;

                    if (!string.IsNullOrEmpty(StringUtil.CStr(dr["PRODNO"])) && StringUtil.CStr(dr["ISSTOCK"]) == "1")
                    {
                        try
                        {
                            Inventory.PK_INVENTORY_SALE(objTX, "1", StringUtil.CStr(dr["PRODNO"]),
                               logMsg.STORENO, STOCK, StringUtil.CStr(TempTables.Head.Rows[0]["SALE_NO"]),
                               Convert.ToInt32(StringUtil.CStr(dr["QUANTITY"])), logMsg.MODI_USER, StringUtil.CStr(dr["ID"]), ref Code, ref Message);
                            if (Code != "000")
                            {
                                bCheckOut = false;
                                ScriptManager.RegisterClientScriptBlock(this, typeof(string), "ERROR_MESSAGE", "alert('銷售商品扣庫存失敗，請做銷售扣庫存動作!!');", true);
                                objTX.Rollback();
                                btnOrderCheckOut.Enabled = true;
                                return;
                            }
                        }
                        catch (Exception)
                        {
                            bCheckOut = false;
                            ScriptManager.RegisterClientScriptBlock(this, typeof(string), "ERROR_MESSAGE", "alert('銷售商品扣庫存失敗，請做銷售扣庫存動作!!');", true);
                            objTX.Rollback();
                            btnOrderCheckOut.Enabled = true;
                            return;
                        }
                    }
                }
            }

            //IMEI_Log
            if (bCheckOut)
            {
                string strMessage = Facade.IMEISale_Log(objTX, StringUtil.CStr(TempTables.Head.Rows[0]["POSUUID_MASTER"]), logMsg.MODI_USER);
                string[] strMsg = strMessage.Split('|');
                if (strMsg[0] != "000") //表示失敗
                {
                    bCheckOut = false;
                    ScriptManager.RegisterClientScriptBlock(this, typeof(string), "ERROR_MESSAGE", @"alert('IMEI_LOG失敗, " + strMsg[1].Replace("'", "-").Replace("\""," ") + "');", true);
                    objTX.Rollback();
                }
            }

            //GL 分錄
            //if (bCheckOut)
            //{
            //    string strMessage = new SAL01_Facade().runGL(objTX, StringUtil.CStr(TempTables.Head.Rows[0]["POSUUID_MASTER"]));
            //    string[] strMsg = strMessage.Split('|');
            //    if (strMsg[0] == "999") //表示失敗
            //    {
            //        bCheckOut = false;
            //        ScriptManager.RegisterClientScriptBlock(this, typeof(string), "ERROR_MESSAGE", @"alert('GL 分錄失敗, " + strMsg[1].Replace("'", "-").Replace("\"", " ") + "');", true);
            //        objTX.Rollback();
            //    }
            //}

            if (bCheckOut)  //所有動作都有正常執行才可commit
            {
                Facade.UpdateSaleHead(objTX, StringUtil.CStr(TempTables.Head.Rows[0]["SALE_NO"]),
                    StringUtil.CStr(TempTables.Head.Rows[0]["SALE_STATUS"]), StringUtil.CStr(TempTables.Head.Rows[0]["POSUUID_MASTER"]), logMsg.MODI_USER);
                objTX.Commit();
                ScriptManager.RegisterClientScriptBlock(this, typeof(string), "CheckOut", "alert('結帳完成!');", true);
            }

        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(this, typeof(string), "ERROR_MESSAGE", "alert('" + ex.Message.Replace("'", "-").Replace("\"", " ") + "');", true);
            bCheckOut = false;
            objTX.Rollback();
            //throw;
        }
        finally
        {
            if (!bCheckOut)
            {   //結帳失敗,恢復交易狀態為交易補登未結帳
                new SAL01_Facade().UpdateSaleHead("", "8", StringUtil.CStr(this.TempTables.Head.Rows[0]["POSUUID_MASTER"]));
            }
        }

        OtherService(StringUtil.CStr(TempTables.Head.Rows[0]["POSUUID_MASTER"]));
        collection(StringUtil.CStr(TempTables.Head.Rows[0]["POSUUID_MASTER"]));
        //bool printInvoice = false;
        try
        {
            if (bCheckOut)
            {
                //刪除未結清單
                Facade.delTO_CLOSE(StringUtil.CStr(TempTables.Head.Rows[0]["POSUUID_MASTER"]));
                TempTables.Mode_CheckOut(this);
                //取得外部系統SALE_DETAIL
                DataTable dtDetail = Facade.getSale_Detail(StringUtil.CStr(this.TempTables.Head.Rows[0]["POSUUID_MASTER"]), "2");
                if (dtDetail != null && dtDetail.Rows.Count > 0)
                {
                    string prePosuuidDetail = "";
                    string posuuitDetail = "";
                    foreach (DataRow dr in dtDetail.Rows)
                    {
                        string sysID = "";
                        if (dr["POSUUID_DETAIL"] != null && (!string.IsNullOrEmpty(StringUtil.CStr(dr["POSUUID_DETAIL"]))))
                            posuuitDetail = StringUtil.CStr(dr["POSUUID_DETAIL"]);

                        if (dr["SOURCE_TYPE"] != null && (!string.IsNullOrEmpty(StringUtil.CStr(dr["SOURCE_TYPE"]))))
                        {
                            switch (StringUtil.CStr(dr["SOURCE_TYPE"]))
                            {
                                case "1": sysID = "IA"; break;
                                case "2": sysID = "LOY"; break;
                                case "3": sysID = "PY"; break;
                                case "4": sysID = "SSI"; break;
                                case "5": sysID = "OLR"; break;
                                case "10": sysID = "ES"; break;
                                default: break;
                            }
                        }

                        if (sysID != "" && posuuitDetail != "" && posuuitDetail != prePosuuidDetail)
                        {
                            prePosuuidDetail = posuuitDetail;
                            if (dr["SERVICE_SYS_ID"] != null && (!string.IsNullOrEmpty(StringUtil.CStr(dr["SERVICE_SYS_ID"])))
                                && dr["POSUUID_DETAIL"] != null && (!string.IsNullOrEmpty(StringUtil.CStr(dr["POSUUID_DETAIL"]))))
                                Facade.CommitOuterSystem(sysID, StringUtil.CStr(dr["SERVICE_SYS_ID"]),
                                                                StringUtil.CStr(this.TempTables.Head.Rows[0]["POSUUID_MASTER"]), StringUtil.CStr(dr["POSUUID_DETAIL"]),
                                                                StringUtil.CStr(dr["BARCODE1"]), logMsg.OPERATOR, logMsg.STORENO, StringUtil.CStr(dr["BUNDLE_ID"]), StringUtil.CStr(dr["BARCODE2"]), StringUtil.CStr(dr["BARCODE3"]));
                        }
                    }
                }
            }
        }
        catch //(Exception ex)
        {
            
        }
        finally
        {
            if (bCheckOut)
            {
                LoadSAL11Data();
                Session["SALE_SRC_TYPE"] = null;
            }
            else
                btnOrderCheckOut.Enabled = true;
        }
    }

    //protected void btnOrderCancel_Click(object sender, EventArgs e)
    //{
    //    bool OrderCancel = true;
    //    try
    //    {
    //        if (TempTables.Head.Rows[0]["POSUUID_MASTER"] != null &&
    //            (!string.IsNullOrEmpty(StringUtil.CStr(TempTables.Head.Rows[0]["POSUUID_MASTER"]))))
    //        {   //有交易表頭檔UID,刪除資料庫中交易資料
    //            Facade.invalidSaleIMEILog(StringUtil.CStr(TempTables.Head.Rows[0]["POSUUID_MASTER"]));
    //            int ret = Facade.delSaleData(StringUtil.CStr(TempTables.Head.Rows[0]["POSUUID_MASTER"]));
    //            if (ret < 0)
    //                OrderCancel = false;
                
    //            if (OrderCancel)
    //            {
    //                DataRow[] drs = TempTables.Detail.Select(" ITEM_TYPE = '2' ");
    //                if (drs != null && drs.Length > 0)
    //                {
    //                    foreach (DataRow dr in drs)
    //                    {
    //                        //將TO_CLOSE_HEAD 更新為 取消中狀態, 並回填關連的POSUUID_MASTER
    //                        Facade.UpdateUnCloseHead(StringUtil.CStr(dr["POSUUID_DETAIL"]), StringUtil.CStr(TempTables.Head.Rows[0]["POSUUID_MASTER"]), "3", logMsg.MODI_USER);
    //                    }

    //                    string prePosuuidDetail = "";
    //                    string posuuitDetail = "";
    //                    DataTable dt = Facade.getCancleTO_CLOSE_DATA(StringUtil.CStr(TempTables.Head.Rows[0]["POSUUID_MASTER"]));
    //                    foreach (DataRow dr in dt.Rows)
    //                    {
    //                        if (dr["POSUUID_DETAIL"] != null && (!string.IsNullOrEmpty(StringUtil.CStr(dr["POSUUID_DETAIL"]))))
    //                            posuuitDetail = StringUtil.CStr(dr["POSUUID_DETAIL"]);

    //                        if (posuuitDetail != "" && posuuitDetail != prePosuuidDetail)
    //                        {
    //                            prePosuuidDetail = posuuitDetail;
    //                            ret = Facade.CancelOuterSystem(StringUtil.CStr(dr["POSUUID_DETAIL"]), StringUtil.CStr(dr["SERVICE_TYPE"]),
    //                                                            StringUtil.CStr(dr["SERVICE_SYS_ID"]), StringUtil.CStr(dr["BUNDLE_ID"]),
    //                                                            StringUtil.CStr(dr["STORE_NO"]), StringUtil.CStr(dr["SALE_PERSON"]),
    //                                                            StringUtil.CStr(dr["BARCODE1"]), StringUtil.CStr(dr["BARCODE2"]),
    //                                                            StringUtil.CStr(dr["BARCODE3"]), StringUtil.CStr(dr["AMOUNT"]));
    //                            if (ret == 0)
    //                            {
    //                                //取消交易,commit外部系統成功才刪除未結清單中資料
    //                                StringBuilder posuuid_detailList = new StringBuilder("");
    //                                posuuid_detailList.Append(OracleDBUtil.SqlStr(StringUtil.CStr(dr["POSUUID_DETAIL"])));
    //                                Facade.delTO_CLOSE(posuuid_detailList);
    //                            }
    //                            else
    //                            {
    //                                Facade.InsertDataUploadLog(StringUtil.CStr(dr["POSUUID_DETAIL"]));
    //                                OrderCancel = false;
    //                            }
    //                        }
    //                    }
    //                }
    //            }
    //        }
    //        else
    //        {
    //            TempTables.SaveTempTable(this);
    //        }
    //        if (OrderCancel)
    //        {
    //            string SALE_NO = StringUtil.CStr(TempTables.Head.Rows[0]["SALE_NO"]);
    //            if (SALE_NO == "")
    //                SALE_NO = Store_SerialNo.GenNo("SALE", logMsg.STORENO, logMsg.MACHINE_ID);
    //            ScriptManager.RegisterClientScriptBlock(this, typeof(string), "OrderCancel", "alert('您已取消[" + SALE_NO + "]交易，請同步取消或更正該門號於業務園地的狀態!');", true);
    //            LoadSAL11Data();
    //        }
    //        else
    //        {
    //            ScriptManager.RegisterClientScriptBlock(this, typeof(string), "OrderCancelError", "alert('交易取消失敗!');", true);
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        ScriptManager.RegisterClientScriptBlock(this, typeof(string), "OrderCancelError", "alert('交易取消失敗，錯誤訊息[" + ex.Message.Replace("'", "-").Replace("\"", " ") + "]!');", true);
    //    }
    //}

    protected void btnItemDel_Click(object sender, EventArgs e)
    {
        if (TempTables.Delete_Detail_Temp(this, gvMaster) == 0)
        {
            ScriptManager.RegisterClientScriptBlock(this, typeof(string), "NoSelect", "alert('請選取要刪除的資料!');", true);
        }
        else
        {
            if (TempTables.Detail.Rows.Count == 0) //全刪完了不能打付款資料           
                TempTables.Mode_NoPROD(this);
            else
                TempTables.Mode_HavePROD(this);
        }
    }

    protected void btnPayDEL_Click(object sender, EventArgs e)
    {
        TempTables.Delete_Paid_Temp(gvCheckOut);
        CheckPaidData();
    }

    protected void btnConfirm_Click(object sender, EventArgs e)
    {
        if ((StringUtil.CStr(rbINVOICE_TYPE.Value) == "03" || StringUtil.CStr(rbINVOICE_TYPE.Value) == "04") && lbINVOICE_NO.Text == "")
        {
            ScriptManager.RegisterClientScriptBlock(this, typeof(string), "NoINVNo", "alert('發票格式為二聯式或三聯式時，發票號碼不可為空白!');", true);
            return;
        }
        else if (StringUtil.CStr(rbINVOICE_TYPE.Value) == "04" && txtUNI_NO.Text == "")
        {
            ScriptManager.RegisterClientScriptBlock(this, typeof(string), "NoUNINo", "alert('發票格式為三聯式時，統一編號不可為空值，請重新輸入。');", true);
            return;
        }
        else if (StringUtil.CStr(rbINVOICE_TYPE.Value) == "03" || StringUtil.CStr(rbINVOICE_TYPE.Value) == "04") 
        {
            string chkStr = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            if (chkStr.IndexOf(lbINVOICE_NO.Text.Substring(0, 1).ToUpper()) < 0 || chkStr.IndexOf(lbINVOICE_NO.Text.Substring(1, 1).ToUpper()) < 0)
            {
                ScriptManager.RegisterClientScriptBlock(this, typeof(string), "ErrorINVFormat", "alert('發票格式錯誤!');", true);
                return;
            }
            else if (lbINVOICE_NO.Text.Length != 10)
            {
                ScriptManager.RegisterClientScriptBlock(this, typeof(string), "ErrorINVFormat", "alert('發票格式錯誤!');", true);
                return;
            }
            else if (!NumberUtil.IsNumeric(lbINVOICE_NO.Text.Substring(2, 8)))
            {
                ScriptManager.RegisterClientScriptBlock(this, typeof(string), "ErrorINVFormat", "alert('發票格式錯誤!');", true);
                return;
            }
        }
        if (lbINVOICE_NO.Text != "")
        {
            if (!Facade.IsValidINVNo(logMsg.STORENO, lbINVOICE_NO.Text, StringUtil.CStr(rbINVOICE_TYPE.Value).Substring(1)))
            {
                ScriptManager.RegisterClientScriptBlock(this, typeof(string), "ErrorINVNo", "alert('發票號碼不存在或不允許使用，不允許結帳!');", true);
                return;
            }
        }
        if (TempTables.Discount.Select("SOURCE_TYPE = '11'") == null || TempTables.Discount.Select("SOURCE_TYPE = '11'").Length == 0) //新增Discount
            TempTables.NewRow_Detail_ItemDiscount(this, gvDetail);
        TempTables.SaveTempTable(this);
        //交易確認時就不能再新增貨品資料了,將HEAD ,DETAIL DISCOUNT 和 PAID的資料存入CACHE
        TempTables.Head.Rows[0]["SALE_STATUS"] = "8";    //避免結帳失敗後，使用者取消付款明細，又按確定，而Session Table中的狀態停留在狀態2，已結帳
        TempTables.calTotalAmount(this);    //計算總銷售金額
        TempTables.CalculationTax(this);    //計算稅額
        TempTables.Save_Sale_Data2Cache(this);
        TempTables.Mode_PayCash(this);  //進入付款模式
        lbTOTAL_AMOUNT.Text = StringUtil.CStr(TempTables.Head.Rows[0]["SALE_TOTAL_AMOUNT"]);
        TempTables.CalPayCharge(this); //計算應付金額及找零金
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        TempTables.Delete_Sale_DataFromCache(this);
        TempTables.Mode_HavePROD(this); //進入有商品模式
    }

    protected void btnCash_Click(object sender, EventArgs e)
    {
        string[] args = __EVENTARGUMENT.Split(new char[] { ',' });
        TempTables.NewRow_Paid_Cash(gvCheckOut, args);
        //付款一有資料時就不能再新增貨品資料了,並且要新增 DATA_CASH的資料來
        CheckPaidData();
    }

    protected void btnOffLineCredit_Click(object sender, EventArgs e)
    {
        string[] args = __EVENTARGUMENT.Split(new char[] { ',' });
        TempTables.NewRow_Paid_OffLineCredit(gvCheckOut, args);
        //付款一有資料時就不能再新增貨品資料了,並且要新增 DATA_CASH的資料來
        CheckPaidData();
        //信用卡交易刷完卡後不允許取消交易
        //this.btnOrderCancel.Enabled = false;
    }

    protected void gvMaster_HtmlRowCreated(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewTableRowEventArgs e)
    {
        if (e.RowType == GridViewRowType.Data)
        {
            ASPxTextBox lblIMEI = e.Row.FindChildControl<ASPxTextBox>("lbIMEI_QTY");
            ASPxTextBox txtProdName = e.Row.FindChildControl<ASPxTextBox>("txtPRODNAME");
            HtmlGenericControl divIMEI_QTY = e.Row.FindChildControl<HtmlGenericControl>("divIMEI_QTY");
            PopupControl txtPRODNO = gvMaster.FindRowCellTemplateControl(e.VisibleIndex, (GridViewDataColumn)gvMaster.Columns["PRODNO"], "txtPRODNO") as PopupControl;
            string STATUS = StringUtil.CStr(gvMaster.GetRowValues(e.VisibleIndex, "ITEM_TYPE"));
            if (txtPRODNO.Text == "SIM" && STATUS == "2")
            {
                txtPRODNO.KeyFieldValue1 = "simcard";
            }

            //取得IMEI數量
            string PO_OE_NO = StringUtil.CStr(gvMaster.GetRowValues(e.VisibleIndex, "ID"));
            string PRODNO = StringUtil.CStr(gvMaster.GetRowValues(e.VisibleIndex, "PRODNO"));
            lblIMEI.Text = StringUtil.CStr(getPROD_IMEI_COUNT("SALE_IMEI_LOG", PO_OE_NO, PRODNO));

            // 繫結明細資料表    
            string strIMEI = IMEIContent("SALE_IMEI_LOG", StringUtil.CStr(e.GetValue("ID")), StringUtil.CStr(e.GetValue("PRODNO")));
            divIMEI_QTY.Attributes["onmouseover"] = string.Format("show('{0}');", strIMEI);
            divIMEI_QTY.Attributes["onmouseout"] = "hide();";

            int intC_IMEI = int.Parse(StringUtil.CStr(e.GetValue("QUANTITY")) == "" ? "0" : StringUtil.CStr(e.GetValue("QUANTITY")));
            int intS_IMEI = int.Parse(StringUtil.CStr(e.GetValue("IMEI_QTY")) == "" ? "0" : lblIMEI.Text);
            ASPxImage imgIMEI = e.Row.FindChildControl<ASPxImage>("imgIMEI");
            PopupControl InputIMEIData = e.Row.FindChildControl<PopupControl>("InputIMEIData");


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
                imgIMEI.ClientVisible = false;
                InputIMEIData.Enabled = false;
            }

            if (imgIMEI.Visible)
            {
                imgIMEI.ImageUrl = (intC_IMEI == intS_IMEI ? "~/Icon/check.png" : "~/Icon/non_complete.png");
                ASPxPopupControl ASPxPopupControl1 = InputIMEIData.FindChildControl<ASPxPopupControl>("ASPxPopupControl1");

                if (strIMEI.IndexOf("<tr><td>") >= 0)
                {
                    string[] stringSeparators = new string[] { "<tr><td>" };
                    string[] result = strIMEI.Split(stringSeparators, System.StringSplitOptions.RemoveEmptyEntries);
                    string[] stringSeparators2 = new string[] { "</td></tr>" };
                    string[] result2 = result[1].Split(stringSeparators2, System.StringSplitOptions.RemoveEmptyEntries);

                    InputIMEIData.Text = result2[0];

                }
                InputIMEIData.KeyFieldValue1 = "SALE_IMEI_LOG;" + StringUtil.CStr(e.GetValue("ID")) + ";" + e.GetValue("PRODNO") + ";" + e.GetValue("QUANTITY") + ";" + IMEI_Flag;
                string strDateTime = System.DateTime.Now.ToString("yyyy/MM/ddHHmmss");
                //ASPxPopupControl1.ContentUrl = "~/VSS/SAL/SAL01/SAL01_inputIMEIData.aspx?SysDate=" + strDateTime + "&KeyFieldValue1=" + InputIMEIData.KeyFieldValue1;

                //**2011/04/26 Tina：傳遞參數時，要先以加密處理。
                string encryptUrl = Utils.Param_Encrypt("SysDate=" + strDateTime + "&KeyFieldValue1=" + InputIMEIData.KeyFieldValue1);
                ASPxPopupControl1.ContentUrl = string.Format("~/VSS/SAL/SAL01/SAL01_inputIMEIData.aspx?Param={0}", encryptUrl);

                if (string.IsNullOrEmpty(StringUtil.CStr(e.GetValue("PRODNO"))))
                {
                    InputIMEIData.Enabled = false;
                }
            }
        }
    }

    protected void gvMaster_HtmlRowPrepared(object sender, ASPxGridViewTableRowEventArgs e)
    {
        if (gvMaster.Enabled)
            e.Row.Attributes["canSelect"] = "true";
        else
            e.Row.Attributes["canSelect"] = "false";
        if (e.RowType == GridViewRowType.Data)
        {
            PopupControl txtPRODNO = gvMaster.FindRowCellTemplateControl(e.VisibleIndex, (GridViewDataColumn)gvMaster.Columns["PRODNO"], "txtPRODNO") as PopupControl; // e.Row.FindChildControl<PopupControl>("txtPRODNO");
            ASPxTextBox txtQUANTITY = gvMaster.FindRowCellTemplateControl(e.VisibleIndex, (GridViewDataColumn)gvMaster.Columns["QUANTITY"], "txtQUANTITY") as ASPxTextBox;  //e.Row.FindChildControl<ASPxTextBox>("txtQUANTITY");
            ASPxTextBox txtUNIT_PRICE = gvMaster.FindRowCellTemplateControl(e.VisibleIndex, (GridViewDataColumn)gvMaster.Columns["UNIT_PRICE"], "txtUNIT_PRICE") as ASPxTextBox;  //e.Row.FindChildControl<ASPxTextBox>("txtUNIT_PRICE");
            ASPxTextBox txtOpenPrice = gvMaster.FindRowCellTemplateControl(e.VisibleIndex, (GridViewDataColumn)gvMaster.Columns["UNIT_PRICE"], "txtOpenPrice") as ASPxTextBox;
            ASPxTextBox hidIS_OPEN_PRICE = gvMaster.FindRowCellTemplateControl(e.VisibleIndex, (GridViewDataColumn)gvMaster.Columns["UNIT_PRICE"], "hidIS_OPEN_PRICE") as ASPxTextBox;  
            PopupControl InputIMEIData = gvMaster.FindRowCellTemplateControl(e.VisibleIndex, (GridViewDataColumn)gvMaster.Columns["IMEI_QTY"], "InputIMEIData") as PopupControl; //e.Row.FindChildControl<PopupControl>("InputIMEIData").Visible = false;

            string STATUS = StringUtil.CStr(gvMaster.GetRowValues(e.VisibleIndex, "ITEM_TYPE"));
            if (STATUS == "5" || STATUS == "2" || STATUS == "11" || STATUS == "12") //2 未結明細,5 未結折扣, 11 租賃折扣, 12 舊機換新機折扣
            {
                if (txtPRODNO.Text == "SIM" && STATUS == "2")
                {
                    txtPRODNO.Enabled = true;
                    txtPRODNO.KeyFieldValue1 = "simcard";
                }
                else
                {
                    txtPRODNO.Enabled = false;
                }
                txtQUANTITY.ReadOnly = true;
                txtUNIT_PRICE.ReadOnly = true;

                e.Row.Attributes["canSelect"] = "false";
            }

            if (hidIS_OPEN_PRICE.Text == "Y")
            {
                txtUNIT_PRICE.ReadOnly = false;
            }
            else
            {
                txtUNIT_PRICE.ReadOnly = true;
            }
        }
    }
    protected void gvMaster_CommandButtonInitialize(object sender, ASPxGridViewCommandButtonEventArgs e)
    {
        string STATUS = StringUtil.CStr(gvMaster.GetRowValues(e.VisibleIndex, "ITEM_TYPE"));
        if (STATUS == "5" || STATUS == "2" || STATUS == "11" || STATUS == "12") //5.未結明細,2未結折扣, 11 租賃折扣, 12 舊機換新機折扣
        {
            if (e.ButtonType == ColumnCommandButtonType.SelectCheckbox) { e.Enabled = false; }
        }
    }

    protected void gvMaster_PageIndexChanged(object sender, EventArgs e)
    {
        DataTable dt = (DataTable) gvMaster.DataSource;
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
            gvMaster.Selection.UnselectAll();
            TempTables.Mode_HavePROD(this);
        }
    }

    protected void gvMaster_StartRowEditing(object sender, DevExpress.Web.Data.ASPxStartRowEditingEventArgs e)
    {
        gvMaster.Selection.UnselectAll();
    }

    protected void gvDetail_PageIndexChanged(object sender, EventArgs e)
    {
        DataTable dt = (DataTable)gvDetail.DataSource;
        if (dt == null || dt.Rows.Count == 0)
        {
            gvDetail.DataSource = TempTables.Discount;
            gvDetail.DataBind();
            TempTables.Mode_HavePROD(this);
        }
    }

    protected void gvCheckOut_PageIndexChanged(object sender, EventArgs e)
    {
        DataTable dt = (DataTable)gvCheckOut.DataSource;
        if (dt == null || dt.Rows.Count == 0)
        {
            gvCheckOut.DataSource = TempTables.Paid;
            gvCheckOut.DataBind();
            gvCheckOut.Selection.UnselectAll();
            TempTables.Mode_HavePROD(this);
        }
    }

    protected void gvCheckOut_StartRowEditing(object sender, DevExpress.Web.Data.ASPxStartRowEditingEventArgs e)
    {
        gvCheckOut.Selection.UnselectAll();
    }

    //ajax 呼當前網頁的方式
    [System.Web.Services.WebMethod()]
    [System.Web.Script.Services.ScriptMethod()]
    static public string getPRODINFO(string PRODNO, string STORE_NO)
    {
        string STOCK = Common_PageHelper.GetGoodLOCUUID();
        DataTable dt = new Product_Facade().Query_ProductInfo(PRODNO, STORE_NO, STOCK);
        string r = "";
        if (dt.Rows.Count > 0)
        {
            r = StringUtil.CStr(dt.Rows[0]["PRODNAME"]) + ";";
            r += StringUtil.CStr(dt.Rows[0]["IMEI_FLAG"]) + ";";
            if (dt.Rows[0]["PRICE"] != null && dt.Rows[0]["PRICE"] != DBNull.Value && StringUtil.CStr(dt.Rows[0]["PRICE"]) != "")
                r += StringUtil.CStr(dt.Rows[0]["PRICE"]);
            else
                r += "null";

            r += ";";
            if (dt.Rows[0]["ISSTOCK"] != null && dt.Rows[0]["ISSTOCK"] != DBNull.Value && StringUtil.CStr(dt.Rows[0]["ISSTOCK"]) != "")
                r += StringUtil.CStr(dt.Rows[0]["ISSTOCK"]);
            else
                r += "0";
            r += ";";
            if (dt.Rows[0]["ON_HAND_QTY"] != null && dt.Rows[0]["ON_HAND_QTY"] != DBNull.Value && StringUtil.CStr(dt.Rows[0]["ON_HAND_QTY"]) != "")
                r += StringUtil.CStr(dt.Rows[0]["ON_HAND_QTY"]);
            else
                r += "0";
            r += ";";
            if (dt.Rows[0]["IS_OPEN_PRICE"] != null && dt.Rows[0]["IS_OPEN_PRICE"] != DBNull.Value && StringUtil.CStr(dt.Rows[0]["IS_OPEN_PRICE"]) != "")
                r += StringUtil.CStr(dt.Rows[0]["IS_OPEN_PRICE"]);
            else
                r += "N";
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

    /// <summary>
    /// 付款行為時所需執行的動作
    /// </summary>
    private void CheckPaidData()
    {
        //若有付款行為發生時要將交易存入Cache裏
        if (TempTables.Paid.Rows.Count > 0)
        {
            //是否已存進Cache,不能用Sale_Head判斷, 因為會去Join 發票檔而此時相關發票資料尚未產生
            DataTable dt = Facade.getPaid_Detail(StringUtil.CStr(TempTables.Head.Rows[0]["POSUUID_MASTER"]));
            if (dt != null && dt.Rows.Count > 0)
            { //若已存入Cache只維護 PAID部份
                Facade.DeletePaid_DetailByPOSUUID_MASTER(StringUtil.CStr(TempTables.Head.Rows[0]["POSUUID_MASTER"]));
                Facade.InsertPaid_Detail(TempTables.Paid);
                //DELETE + INSERT = UPDATE
            }
            else
            {   
                Facade.InsertPaid_Detail(TempTables.Paid);
            }
            TempTables.CalPayCharge(this); //計算應付金額,找零金
            TempTables.Mode_PayCash(this);  //維持在付款模式
        }
        else
        {
            TempTables.Delete_Sale_DataFromCache(this);
            TempTables.CalPayCharge(this); //計算應付金額,找零金
            TempTables.Mode_HavePROD(this); //進入有商品模式
        }
    }

    protected void ASPxCallback1_Callback(object source, DevExpress.Web.ASPxCallback.CallbackEventArgs e)
    {
        int i = 0;
        string[] pa = e.Parameter.Split(new char[] { ';' });
        string[] vv = pa[0].Split(new char[] { '_' });
        i = int.Parse(vv[3].Replace("cell", ""));
        TempTables.Detail.Rows[i]["PRODNO"] = pa[1];
        if (pa[2] == null || (!NumberUtil.IsNumeric(pa[2])))
            TempTables.Detail.Rows[i]["QUANTITY"] = 0;
        else
            TempTables.Detail.Rows[i]["QUANTITY"] = pa[2];
        if (pa[2] == null || (!NumberUtil.IsNumeric(pa[3])))
            TempTables.Detail.Rows[i]["UNIT_PRICE"] = 0;
        else
            TempTables.Detail.Rows[i]["UNIT_PRICE"] = pa[3];
        if (pa[2] == null || (!NumberUtil.IsNumeric(pa[4])))
            TempTables.Detail.Rows[i]["TOTAL_AMOUNT"] = 0;
        else
            TempTables.Detail.Rows[i]["TOTAL_AMOUNT"] = pa[4];
        if (pa[5] != null)
            TempTables.Detail.Rows[i]["IS_OPEN_PRICE"] = pa[5];
        TempTables.calTotalAmount(this);//計算稅額
        TempTables.Detail.AcceptChanges();
        e.Result = StringUtil.CStr(TempTables.STORE_REC_TOTAL_AMOUNT) + ";;";
    }

    /// <summary>
    /// 其他服務
    /// </summary>
    /// <param name="posuuid_master"></param>
    private void OtherService(string posuuid_master)
    {
        string sale_type = TSAL01_Facade.get_sale_type_by_sale_head(posuuid_master);
        OracleConnection conn = null;
        OracleCommand cmd = null;
        try
        {
            conn = OracleDBUtil.GetConnection();
            Logger.Log.Info("交易補登開始拆折扣，單號:" + posuuid_master);
            if (sale_type == "1")
            {
                cmd = new OracleCommand("SP_SALE_DISCOUNT_DISPATCH_ONE", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("I_POSUUID_MASTER", OracleType.VarChar, 32).Value = posuuid_master;
                cmd.Parameters.Add("O_RT_CODE", OracleType.VarChar, 4000).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("O_RT_MESSAGE", OracleType.VarChar, 4000).Direction = ParameterDirection.Output;
                cmd.ExecuteNonQuery();

                string code = StringUtil.CStr(cmd.Parameters["O_RT_CODE"].Value);
                string message = StringUtil.CStr(cmd.Parameters["O_RT_MESSAGE"].Value);


            }
            else if (sale_type == "2")
            {
                cmd = new OracleCommand("PK_BILL.PayModeSplitOne", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("I_MASTER_ID", OracleType.VarChar, 32).Value = posuuid_master;
                cmd.ExecuteNonQuery();

            }

            cmd = new OracleCommand("SP_SUPPLIER_OUT_DISPATCH", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("I_MASTER_ID", OracleType.VarChar, 32).Value = posuuid_master;
            cmd.Parameters.Add("O_RT_CODE", OracleType.VarChar, 4000).Direction = ParameterDirection.Output;
            cmd.Parameters.Add("O_RT_MESSAGE", OracleType.VarChar, 4000).Direction = ParameterDirection.Output;
            cmd.ExecuteNonQuery();
            Logger.Log.Info("交易補登拆折扣成功，單號:" + posuuid_master);
        }
        catch (Exception ex) 
        {
            Logger.Log.Info("交易補登拆折扣失敗，單號:" + posuuid_master + ",Error Message=" + ex.Message);
        }
        finally
        {
            if (conn.State == ConnectionState.Open) conn.Close();
            OracleConnection.ClearPool(conn);
        }
    }

    #region 收據
    //private string Collection_Receipt(string posuuid_master, string printName)
    //{
    //    string return_url = "";
    //    List<List<string>> dir = new List<List<string>>();
    //    OracleConnection conn = null;
    //    OracleCommand cmd = null;
    //    OracleDataAdapter da = null;
    //    OracleDataReader dr = null;
    //    string trade_date = "";
    //    string store_no = "";
    //    string machice_id = "";
    //    string RECEIPT_NO = "";
    //    string total_amount = "";
    //    string barcode1 = "";
    //    string barcode2 = "";
    //    string barcode3 = "";
    //    string CARD_NO = "";
    //    string msisdn = "";
    //    string SALE_PERSON = "";
    //    string hg_card_no = "";
    //    string pay_mode1 = ""; //現金
    //    string pay_mode2 = ""; //信用卡
    //    string pay_mode7 = "";//HappyGo折抵
    //    string pay_mode8 = "";//找零金
    //    string sqlstr = "select sh.trade_date,SH.STORE_NO,SH.MACHINE_ID,RH.RECEIPT_NO,SH.SALE_PERSON,sh.hg_card_no from sale_head sh  join RECEIPT_HEAD rh on RH.POSUUID_MASTER = SH.POSUUID_MASTER where  sh.posuuid_master = " + OracleDBUtil.SqlStr(posuuid_master);
    //    try
    //    {
    //        conn = OracleDBUtil.GetConnection();
    //        cmd = new OracleCommand(sqlstr, conn);
    //        dr = cmd.ExecuteReader();
    //        if (dr.Read())
    //        {
    //            trade_date = dr.IsDBNull(0) ? "" : dr.GetDateTime(0).ToString("yyyy-MM-dd hh:mm");//StringUtil.CStr(dr[0]);
    //            store_no = StringUtil.CStr(dr[1]);
    //            machice_id = StringUtil.CStr(dr[2]);
    //            RECEIPT_NO = StringUtil.CStr(dr[3]);
    //            SALE_PERSON = StringUtil.CStr(dr[4]);
    //            hg_card_no = StringUtil.CStr(dr[5]);
    //        }
    //        dr.Read();

    //        //抓取detal
    //        sqlstr = "select barcode1,msisdn,total_amount,id,fun_id,CARD_NO from sale_detail where source_type = 3 and  posuuid_master = " + OracleDBUtil.SqlStr(posuuid_master);
    //        DataTable detail = new DataTable();
    //        cmd = new OracleCommand(sqlstr, conn);
    //        da = new OracleDataAdapter(cmd);
    //        da.Fill(detail);
    //        foreach (DataRow row in detail.Rows)
    //        {
    //            string sale_detail_id = StringUtil.CStr(row["id"]);
    //            barcode1 = StringUtil.CStr(row["barcode1"]);
    //            barcode2 = StringUtil.CStr(row["barcode2"]);
    //            barcode3 = StringUtil.CStr(row["barcode3"]);
    //            CARD_NO = StringUtil.CStr(row["CARD_NO"]);
    //            msisdn = StringUtil.CStr(row["msisdn"]);
    //            total_amount = StringUtil.CStr(row["total_amount"]);
    //            string fun_id = StringUtil.CStr(row["fun_id"]);

    //            //抓出拆分金額
    //            //抓現金
    //            sqlstr = "select sum(amount) from bill_dispatch where pay_mode_id = 1 and sale_detail_id = " + OracleDBUtil.SqlStr(sale_detail_id);
    //            cmd = new OracleCommand(sqlstr, conn);
    //            pay_mode1 = cmd.ExecuteScalar() == null ? "0" : StringUtil.CStr(cmd.ExecuteScalar());

    //            //抓信用卡
    //            sqlstr = "select sum(amount) from bill_dispatch where pay_mode_id in(2,3,4) and sale_detail_id = " + OracleDBUtil.SqlStr(sale_detail_id);
    //            cmd = new OracleCommand(sqlstr, conn);
    //            pay_mode2 = cmd.ExecuteScalar() == null ? "0" : StringUtil.CStr(cmd.ExecuteScalar());

    //            //找零金
    //            sqlstr = "select sum(amount) from bill_dispatch where pay_mode_id = 8 and sale_detail_id = " + OracleDBUtil.SqlStr(sale_detail_id);
    //            cmd = new OracleCommand(sqlstr, conn);
    //            pay_mode8 = cmd.ExecuteScalar() == null ? "0" : StringUtil.CStr(cmd.ExecuteScalar());

    //            //happyGo
    //            sqlstr = "select sum(amount) from bill_dispatch where pay_mode_id = 7 and sale_detail_id = " + OracleDBUtil.SqlStr(sale_detail_id);
    //            cmd = new OracleCommand(sqlstr, conn);
    //            pay_mode7 = cmd.ExecuteScalar() == null ? "0" : StringUtil.CStr(cmd.ExecuteScalar());
    //            //1: 遠傳帳單
    //            //2: 和信帳單
    //            //3: Seednet帳單
    //            //4: 遠通帳單(有單)
    //            //5: 遠通帳單(無單)
    //            //6: 速博帳單
    //            //判斷類型
    //            List<string> list = new List<string>();
    //            switch (fun_id)
    //            {
    //                case "1":
    //                    //FET

    //                    list.Add("4");
    //                    list.Add(trade_date);
    //                    list.Add(store_no);
    //                    list.Add(machice_id);
    //                    list.Add(RECEIPT_NO);
    //                    list.Add("遠傳電信帳單");
    //                    list.Add(total_amount);
    //                    list.Add(barcode1);
    //                    list.Add(msisdn);
    //                    list.Add(pay_mode1);
    //                    list.Add(pay_mode2);
    //                    list.Add(pay_mode7);
    //                    list.Add("0");
    //                    list.Add(hg_card_no);
    //                    list.Add(SALE_PERSON);
    //                    dir.Add(list);
    //                    break;
    //                case "2"://KGT

    //                    list.Add("5");
    //                    list.Add(trade_date);
    //                    list.Add(store_no);
    //                    list.Add(machice_id);
    //                    list.Add(RECEIPT_NO);
    //                    list.Add("和信電信帳單");
    //                    list.Add(total_amount);
    //                    list.Add(barcode1);
    //                    list.Add(pay_mode1);
    //                    list.Add(pay_mode2);
    //                    list.Add(pay_mode7);
    //                    list.Add(pay_mode8);
    //                    list.Add(hg_card_no);
    //                    list.Add(SALE_PERSON);
    //                    dir.Add(list);
    //                    break;
    //                case "3"://Seednet
    //                    list.Add("8");
    //                    list.Add(trade_date);
    //                    list.Add(store_no);
    //                    list.Add(machice_id);
    //                    list.Add(RECEIPT_NO);
    //                    list.Add("Seednet帳單");
    //                    list.Add(total_amount);
    //                    list.Add(barcode1);
    //                    list.Add(barcode2);
    //                    list.Add(barcode3);
    //                    list.Add(pay_mode1);
    //                    list.Add(SALE_PERSON);
    //                    dir.Add(list);
    //                    break;
    //                case "4"://ETC(有單
    //                    list.Add("3");
    //                    list.Add(trade_date);
    //                    list.Add(store_no);
    //                    list.Add(machice_id);
    //                    list.Add(RECEIPT_NO);
    //                    list.Add("ETC 代收");
    //                    list.Add(total_amount);
    //                    list.Add(barcode1);
    //                    list.Add(barcode2);
    //                    list.Add(barcode3);
    //                    list.Add(pay_mode1);
    //                    list.Add(barcode2);
    //                    list.Add(SALE_PERSON);
    //                    dir.Add(list);
    //                    break;
    //                case "5"://ETC(無單
    //                    list.Add("3");
    //                    list.Add(trade_date);
    //                    list.Add(store_no);
    //                    list.Add(machice_id);
    //                    list.Add(RECEIPT_NO);
    //                    list.Add("ETC 代收");
    //                    list.Add(total_amount);
    //                    list.Add(barcode1);
    //                    list.Add(barcode2);
    //                    list.Add(barcode3);
    //                    list.Add(pay_mode1);
    //                    list.Add(pay_mode2);
    //                    list.Add(SALE_PERSON);
    //                    dir.Add(list);
    //                    break;
    //                case "6"://NCIC
    //                    list.Add("6");
    //                    list.Add(trade_date);
    //                    list.Add(store_no);
    //                    list.Add(machice_id);
    //                    list.Add(RECEIPT_NO);
    //                    list.Add("速博電信帳單");
    //                    list.Add(total_amount);
    //                    list.Add(barcode1);
    //                    list.Add(barcode2);
    //                    list.Add(barcode3);
    //                    list.Add(pay_mode1);

    //                    list.Add(SALE_PERSON);
    //                    dir.Add(list);
    //                    break;

    //            }

    //        }

    //        string fileName = "";
    //        if (dir.Count > 0)
    //        {
    //            SAL01_Facade facade = new SAL01_Facade();
    //            string filePath = facade.getUploadPath(posuuid_master);
    //            IRClass pri = new PriReceipt();
    //            fileName = pri.Print("M", null, dir, printName);

    //            if (fileName == null || string.IsNullOrEmpty(fileName))
    //            {
    //                throw new Exception("列印收據失敗，請重印收據!!");
    //            }
    //            else
    //            {
    //                return_url = Request.ApplicationPath + "/Downloads/Receipt/" + fileName;
    //            }
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        throw ex;
    //    }
    //    finally
    //    {
    //        if (conn.State == ConnectionState.Open) conn.Close();
    //        OracleConnection.ClearPool(conn);
    //    }
    //    return return_url;
    //}

    #endregion

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
                string barcode1 = StringUtil.CStr(row["barcode1"]);
                string fun_id = StringUtil.CStr(row["fun_id"]);
                //FET                                                                                                                                                                                                                                              
                #region 判斷代收類型

                switch (fun_id)
                {
                    case "1":

                        FET_BILLING_INSPAYMENTTRX fet = new FET_BILLING_INSPAYMENTTRX();
                        postString = fet.DOMain(POSUUID_MASTER);
                        bill_dispatch_url = get_bill_dispatch_url("FET") + "/posapp/InsPaymentTrx";
                        //bill_dispatch_url = "http://192.168.8.223/HRS_WS/Default2.aspx";                                                                                                                                                                         


                        break;
                    case "3":
                        SEEDNET_BILLING.Instant_SEEDNET_BILLING seed = new SEEDNET_BILLING.Instant_SEEDNET_BILLING();
                        postString = "req=" + seed.DOMain(POSUUID_MASTER);
                        bill_dispatch_url = get_bill_dispatch_url("SEEDNET");
                        //bill_dispatch_url = "http://localhost:51009/FET_WEB_POS_v2/Default2.aspx";                                                                                                                                                               
                        Logger.Log.Info("FET XML:" + bill_dispatch_url + "?" + postString);

                        break;
                }

                #endregion


                if (fun_id == "3")
                {
                    if (!string.IsNullOrEmpty(postString))
                    {
                        byte[] postData = Encoding.Default.GetBytes(postString);
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
                            Logger.Log.Info("SEEDNET Response XML:" + responseString);
                        }
                        string status = "";
                        string message = "";
                        string send_flag = "";
                        string sqlstr = "";
                        OracleCommand cmd = new OracleCommand();
                        if (!string.IsNullOrEmpty(responseString))
                        {
                            System.IO.StringReader sr = new System.IO.StringReader(responseString);
                            DataSet ds = new DataSet();
                            ds.ReadXml(sr);

                            DataTable xmldt = ds.Tables["Responce"];
                            foreach (DataRow xmldr in xmldt.Rows)
                            {
                                if (xmldr["StatusCode"].ToString() == "00")
                                {
                                    send_flag = "S";
                                }
                                else
                                {
                                    send_flag = "X";
                                }
                            }

                        }
                        sqlstr = "update SEEDNET_BILLING_M set SEND_STATUS = :SEND_STATUS,SEND_DTM = SYSDATE ";
                        sqlstr += " WHERE SEEDNET_BFM_ID IN (SELECT SEEDNET_BFM_ID FROM SEEDNET_BILLING_D WHERE BILL_DISPATCH_ID ";
                        sqlstr += " IN(SELECT BILL_DISPATCH_ID FROM BILL_DISPATCH WHERE POSUUID_MASTER =:posuuid_master))";
                        cmd = new OracleCommand(sqlstr, conn);
                        cmd.Parameters.Add(":SEND_STATUS", OracleType.NVarChar).Value = send_flag;
                        cmd.Parameters.Add(":posuuid_master", OracleType.NVarChar).Value = POSUUID_MASTER;
                        cmd.ExecuteNonQuery();
                    }
                }
                else if (fun_id == "1")
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
                        sqlstr = "update FET_BILLING_FILE set send_flag = :send_flag,FET_PROCESS_STATUS = :FET_PROCESS_STATUS,ERROR_MESSAGE=:ERROR_MESSAGE where pos_key in (select posuuid_detail from sale_detail where posuuid_master = :posuuid_master)";
                        cmd = new OracleCommand(sqlstr, conn);
                        cmd.Parameters.Add(":send_flag", OracleType.NVarChar).Value = send_flag;
                        cmd.Parameters.Add(":FET_PROCESS_STATUS", OracleType.NVarChar).Value = status;
                        cmd.Parameters.Add(":ERROR_MESSAGE", OracleType.NVarChar).Value = message;
                        cmd.Parameters.Add(":posuuid_master", OracleType.NVarChar).Value = POSUUID_MASTER;
                        cmd.ExecuteNonQuery();
                    }
                }




                //if (!string.IsNullOrEmpty(postString))                                                                                                                                                                                                           
                //{                                                                                                                                                                                                                                                
                //    byte[] postData = Encoding.Default.GetBytes(postString);                                                                                                                                                                                     
                //    HttpWebRequest req = (HttpWebRequest)HttpWebRequest.Create(bill_dispatch_url);                                                                                                                                                               
                //    req.Method = "POST";                                                                                                                                                                                                                         
                //    req.ContentType = "application/x-www-form-urlencoded";                                                                                                                                                                                       
                //    req.ContentLength = postData.Length;                                                                                                                                                                                                         
                //    using (System.IO.Stream reqStream = req.GetRequestStream())                                                                                                                                                                                  
                //    {                                                                                                                                                                                                                                            
                //        reqStream.Write(postData, 0, postData.Length);                                                                                                                                                                                           
                //    }                                                                                                                                                                                                                                            

                //    string responseString = "";                                                                                                                                                                                                                  
                //    using (WebResponse wr = req.GetResponse())                                                                                                                                                                                                   
                //    {                                                                                                                                                                                                                                            
                //        System.IO.Stream strm = wr.GetResponseStream();                                                                                                                                                                                          
                //        System.IO.StreamReader sr = new System.IO.StreamReader(strm);                                                                                                                                                                            
                //        responseString = sr.ReadToEnd();                                                                                                                                                                                                         
                //        Logger.Log.Info("FET Response XML:" + responseString);                                                                                                                                                                                   
                //    }                                                                                                                                                                                                                                            
                //    string status = "";                                                                                                                                                                                                                          
                //    string message = "";                                                                                                                                                                                                                         
                //    string send_flag = "";                                                                                                                                                                                                                       
                //    string sqlstr = "";                                                                                                                                                                                                                          
                //    OracleCommand cmd = new OracleCommand();                                                                                                                                                                                                     
                //    switch (fun_id)                                                                                                                                                                                                                              
                //    {                                                                                                                                                                                                                                            
                //        case "1":                                                                                                                                                                                                                                


                //            if (!string.IsNullOrEmpty(responseString))                                                                                                                                                                                           
                //            {                                                                                                                                                                                                                                    
                //                using (System.Xml.XmlReader reader = System.Xml.XmlReader.Create(responseString))                                                                                                                                                
                //                {                                                                                                                                                                                                                                
                //                    reader.ReadToFollowing("fet-pos-pay-create-res");                                                                                                                                                                            
                //                    reader.ReadToFollowing("Process-Status");                                                                                                                                                                                    
                //                    status = reader.Value;                                                                                                                                                                                                       

                //                    if (status == "0")                                                                                                                                                                                                           
                //                    {                                                                                                                                                                                                                            
                //                        reader.ReadToFollowing("Error-Message");                                                                                                                                                                                 
                //                        message = reader.Value;                                                                                                                                                                                                  
                //                        send_flag = "X";                                                                                                                                                                                                         
                //                    }                                                                                                                                                                                                                            
                //                    else                                                                                                                                                                                                                         
                //                    {                                                                                                                                                                                                                            
                //                        send_flag = "S";                                                                                                                                                                                                         
                //                    }                                                                                                                                                                                                                            
                //                }                                                                                                                                                                                                                                
                //            }                                                                                                                                                                                                                                    

                //            sqlstr = "update FET_BILLING_FILE set send_flag = :send_flag,FET_PROCESS_STATUS = :FET_PROCESS_STATUS,ERROR_MESSAGE=:ERROR_MESSAGE where pos_key in (select posuuid_detail from sale_detail where posuuid_master = :posuuid_master)";
                //            cmd = new OracleCommand(sqlstr, conn);                                                                                                                                                                                               
                //            cmd.Parameters.Add(":send_flag", OracleType.NVarChar).Value = send_flag;                                                                                                                                                             
                //            cmd.Parameters.Add(":FET_PROCESS_STATUS", OracleType.NVarChar).Value = status;                                                                                                                                                       
                //            cmd.Parameters.Add(":ERROR_MESSAGE", OracleType.NVarChar).Value = message;                                                                                                                                                           
                //            cmd.Parameters.Add(":posuuid_master", OracleType.NVarChar).Value = POSUUID_MASTER;                                                                                                                                                   
                //            cmd.ExecuteNonQuery();                                                                                                                                                                                                               
                //            break;                                                                                                                                                                                                                               
                //        case "3":                                                                                                                                                                                                                                
                //            if (!string.IsNullOrEmpty(responseString))                                                                                                                                                                                           
                //            {                                                                                                                                                                                                                                    
                //                using (System.Xml.XmlReader reader = System.Xml.XmlReader.Create(responseString))                                                                                                                                                
                //                {                                                                                                                                                                                                                                
                //                    reader.ReadToFollowing("Responce");                                                                                                                                                                                          
                //                    reader.ReadToFollowing("StatusCode");                                                                                                                                                                                        
                //                    status = reader.Value;                                                                                                                                                                                                       
                //                    if (status == "00")                                                                                                                                                                                                          
                //                    {                                                                                                                                                                                                                            
                //                        send_flag = "X";                                                                                                                                                                                                         
                //                    }                                                                                                                                                                                                                            
                //                    else                                                                                                                                                                                                                         
                //                    {                                                                                                                                                                                                                            
                //                        send_flag = "S";                                                                                                                                                                                                         
                //                    }                                                                                                                                                                                                                            
                //                }                                                                                                                                                                                                                                
                //            }                                                                                                                                                                                                                                    
                //            sqlstr = "update SEEDNET_BILLING_M set SEND_STATUS = :SEND_STATUS,SEND_DTM = SYSDATE ";                                                                                                                                              
                //            sqlstr += " where WHERE SEEDNET_BFM_ID IN (SELECT SEEDNET_BFM_ID FROM SEEDNET_BILLING_D WHERE BILL_DISPATCH_ID ";                                                                                                                    
                //            sqlstr += " IN(SELECT BILL_DISPATCH_ID FROM BILL_DISPATCH WHERE POSUUID_MASTER =:posuuid_master))";                                                                                                                                  
                //            cmd = new OracleCommand(sqlstr, conn);                                                                                                                                                                                               
                //            cmd.Parameters.Add(":SEND_STATUS", OracleType.NVarChar).Value = send_flag;                                                                                                                                                           
                //            cmd.Parameters.Add(":posuuid_master", OracleType.NVarChar).Value = POSUUID_MASTER;                                                                                                                                                   
                //            cmd.ExecuteNonQuery();                                                                                                                                                                                                               
                //            break;                                                                                                                                                                                                                               
                //    }                                                                                                                                                                                                                                            
                //}                                                                                                                                                                                                                                                
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
}
