using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
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

public partial class VSS_SAL_SA01 : BasePage
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
        public void SaveSale_Head_Temp(VSS_SAL_SA01 p)
        {
            foreach (DataRow dr in Head.Rows)
            {
                dr["UNI_NO"] = p.txtUNI_NO.Text;
                dr["UNI_TITLE"] = p.txtUNI_TITLE.Text;
                dr["TRADE_DATE"] = p.lbTran_Date.Text;//交易日期
                dr["SALE_NO"] = p.lbSALE_NO.Text;//交易序號
                dr["HG_CARD_NO"] = p.lbHG_CARD_NO.Text; //Happy Go累點點數
                dr["HG_REMAIN_POINT"] = p.lbHG_REMAIN_POINT.Text; //Happy Go剩餘點數
                int saleTotalAmt = 0;
                if (dr["SALE_TOTAL_AMOUNT"] != null && StringUtil.CStr(dr["SALE_TOTAL_AMOUNT"]) != ""
                    && NumberUtil.IsNumeric(StringUtil.CStr(dr["SALE_TOTAL_AMOUNT"])))
                    saleTotalAmt = int.Parse(StringUtil.CStr(dr["SALE_TOTAL_AMOUNT"]));
                if (saleTotalAmt <= 0)
                {
                    //金額小於等於0,開立收據
                    dr["INVOICE_TYPE"] = "03"; //收據
                }
                else
                {
                    dr["INVOICE_TYPE"] = "01"; //連線電子發票
                }
                
                dr["REMARK"] = p.txtREMARK.Text;
            }
            Head.AcceptChanges();
        }

        public void SaleHeadDataBind(VSS_SAL_SA01 p)
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
                    Newdr["TRADE_DATE"] = DateTime.Today;
                }
                Newdr["POSUUID_MASTER"] = StringUtil.CStr(Advtek.Utility.GuidNo.getUUID());
                Newdr["SALE_TOTAL_AMOUNT"] = 0;
                Newdr["SALE_BEFORE_TAX"] = 0;
                Newdr["SALE_TAX"] = 0;
                Newdr["DISCOUNT_TOTAL_AMOUNT"] = 0;
                Newdr["DISCOUNT_BEFORE_TAX"] = 0;
                Newdr["DISCOUNT_TAX"] = 0;
                Newdr["SALE_PERSON"] = p.logMsg.OPERATOR;
                Newdr["SALE_TYPE"] = "1";
                Newdr["SALE_STATUS"] = "1";
                Newdr["VOUCHER_TYPE"] = "1";
                Newdr["INVOICE_TYPE"] = "01"; //連線電子發票
                Head.Rows.Add(Newdr);
                Head.AcceptChanges();
            }

            DataRow dr = Head.Rows[0];
            p.lbTran_Date.Text = Convert.ToDateTime(dr["TRADE_DATE"]).ToString("yyyy/MM/dd");
            p.lbSALE_NO.Text = StringUtil.CStr(dr["SALE_NO"]);
            p.txtUNI_NO.Text = StringUtil.CStr(dr["UNI_NO"]);
            p.txtUNI_TITLE.Text = StringUtil.CStr(dr["UNI_TITLE"]);
            p.txtREMARK.Text = StringUtil.CStr(dr["REMARK"]);
            p.lbINVOICE_NO.Text = StringUtil.CStr(dr["INVOICE_NO"]);
            p.lbHG_CARD_NO.Text = StringUtil.CStr(dr["HG_CARD_NO"]);
            p.lbHG_REMAIN_POINT.Text = StringUtil.CStr(dr["HG_REMAIN_POINT"]); //Happy Go剩餘點數
            //   p.lbTOTAL_AMOUNT.Text = StringUtil.CStr(dr["TOTAL_AMOUNT"]);
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
            string VOUCHER_TYPE_NAME = "";
            DataTable dtInvoiceType = p.Facade.getINVOICE_TYPE();

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
            //  p.lbSALE_STATUS.Text = StringUtil.CStr(Head.Rows[0]["STATUS_NAME"]);
            Employee_Facade empFacade = new Employee_Facade();
            p.lbMODI_USER.Text = p.logMsg.MODI_USER + " " + empFacade.GetEmpName(p.logMsg.MODI_USER);
            p.lbMODI_DTM.Text = p.logMsg.MODI_DTM.ToString("yyyy/MM/dd HH:mm:ss");
            //p.lbSALE_TYPE.Text = StringUtil.CStr(Head.Rows[0]["SALE_TYPE"]) + " " +
            //                    StringUtil.CStr(Head.Rows[0]["SALE_TYPE_NAME"]);//系統依交易類別，顯示定字“發票”或“收據” 
            if (p._SRC_TYPE == "SAL02")
            {
                p.txtUNI_NO.Enabled = false;
                p.txtUNI_TITLE.Enabled = false;
                p.txtREMARK.Enabled = false;
            }
        }

        public void SaveSale_Detail_Temp(ASPxGridView gv)
        {
            // PopupControl noPRODNO = null;
            //就算是新增單品,也要設定該品項的POSUUID_DETAIL,一張單為同一個ID
            string posuuid_detail = "";
            DataRow[] drs = Detail.Select("ITEM_TYPE IN('1','3','4','6')");
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
                PopupControl InputIMEIData = gv.FindRowCellTemplateControl(i, (GridViewDataColumn)gv.Columns["IMEI"], "InputIMEIData") as PopupControl;//IMEI
                if (txtPRODNO != null)
                {
                    Detail.Rows[i]["PRODNO"] = txtPRODNO.Text;
                    Detail.Rows[i]["PRODNAME"] = txtPRODNAME.Text;
                    Detail.Rows[i]["QUANTITY"] = txtQUANTITY.Text;
                    Detail.Rows[i]["UNIT_PRICE"] = txtUNIT_PRICE.Text;
                    Detail.Rows[i]["TOTAL_AMOUNT"] = txtTOTAL_AMOUNT.Text;
                    Detail.Rows[i]["IMEI"] = InputIMEIData.Text;
                    Detail.Rows[i]["POSUUID_MASTER"] = StringUtil.CStr(Head.Rows[0]["POSUUID_MASTER"]);
                    if (Detail.Rows[i]["POSUUID_DETAIL"] == null || string.IsNullOrEmpty(StringUtil.CStr(Detail.Rows[i]["POSUUID_DETAIL"])))
                    {
                        Detail.Rows[i]["POSUUID_DETAIL"] = posuuid_detail;
                    }
                    DataTable PROD_DT = new SAL01_Facade().getProduct(txtPRODNO.Text);
                    if (PROD_DT.Rows.Count > 0) //取得稅額計算方式
                    {
                        Detail.Rows[i]["TAXABLE"] = PROD_DT.Rows[0]["TAXABLE"];
                        Detail.Rows[i]["TAXRATE"] = PROD_DT.Rows[0]["TAXRATE"];
                        Detail.Rows[i]["ISSTOCK"] = PROD_DT.Rows[0]["ISSTOCK"];
                    }
                }
            }
            Detail.AcceptChanges();
            gv.DataSource = Detail;
            gv.DataBind();
            //if (noPRODNO != null)
            //{
            //    noPRODNO.Focus();
            //    ScriptManager.RegisterClientScriptBlock(gv, typeof(string), "NoPRODNO", "alert('請選取請輸入品號!');", true);
            //}
        }

        public int Delete_Detail_Temp(VSS_SAL_SA01 p, ASPxGridView gv)
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
                bool isStoreDIS = false;  //欲刪除的項目中，是否包含店長折扣
                bool isHGDIS = false;     //欲刪除的項目中，是否包含HappyGo折扣
                foreach (DataRow dr in dra)
                {
                    if (StringUtil.CStr(dr["ITEM_TYPE"]) == "6") isStoreDIS = true;
                    if (StringUtil.CStr(dr["ITEM_TYPE"]) == "4")
                    {
                        isHGDIS = true;

                        p.hdHG_REDEEM_POINT.Text = StringUtil.CStr(Convert.ToInt32(p.hdHG_REDEEM_POINT.Text) - Convert.ToInt32(StringUtil.CStr(dr["HG_REDEEM_POINT"]))); //總兌換點數
                        p.hdTOTAL_AMOUNT.Text = StringUtil.CStr(Convert.ToInt32(p.hdTOTAL_AMOUNT.Text) - Convert.ToInt32(StringUtil.CStr(dr["TOTAL_AMOUNT"]))); //總兌點金額
                    }
                    //刪除交易明細時，順便刪除已記錄在Sale_IMEI_Log中的IMEI紀錄
                    if (dr["ID"] != null && StringUtil.CStr(dr["ID"]) != "")
                        p.Facade.DeleteSale_IMEI_LOG(StringUtil.CStr(dr["ID"]));
                    Detail.Rows.Remove(dr);
                }
                gv.DataSource = Detail;
                gv.DataBind();

                calTotalAmount(p);

                if (isStoreDIS)
                {
                    //若刪除原有的店長折扣，則能重新選取店長折扣
                    p.gvMaster.FindChildControl<ASPxButton>("btnStoreDiscount").Enabled = true;
                }

                if (isHGDIS)
                {
                    //若刪除原有的HappyGo折扣，則能重新選取HappyGo折扣
                    p.gvMaster.FindChildControl<ASPxButton>("btnHappyGoNet").Enabled = true;
                }
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

            //List<object> li = gv.GetSelectedFieldValues(gv.KeyFieldName);
            //if (li.Count > 0)
            if (!string.IsNullOrEmpty(strID))
            {
                string where = "";
                //foreach (string key in li)
                //{
                //    where += "'" + key + "',";
                //}
                //if (where.Length > 0)
                //    where = where.Substring(0, where.Length - 1);
                //else where = "''";
                where = strID;
                DataRow[] dra = Paid.Select("ID in(" + where + ")");
                foreach (DataRow dr in dra)
                {
                    Paid.Rows.Remove(dr);
                }
                gv.DataSource = Paid;
                gv.DataBind();
            }
            //return li.Count;
            return strID.Split(',').Length;
        }

        public void SaveTempTable(VSS_SAL_SA01 p)
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
        /// 預收轉鎖售
        /// </summary>
        /// <param name="gv"></param>
        public void NewRow_Detail_OrdToSal(ASPxGridView gv)
        {
            SaveSale_Detail_Temp(gv);
            DataRow dr = Detail.NewRow();
            dr["POSUUID_MASTER"] = StringUtil.CStr(Head.Rows[0]["POSUUID_MASTER"]);
            dr["POSUUID_DETAIL"] = "";
            dr["ID"] = StringUtil.CStr(Advtek.Utility.GuidNo.getUUID());
            dr["IMEI_QTY"] = 0;
            dr["QUANTITY"] = 0;
            dr["UNIT_PRICE"] = 0;
            dr["TOTAL_AMOUNT"] = 0;
            dr["ITEM_TYPE"] = "3";
            dr["ITEM_TYPE_NAME"] = "預";
            dr["SOURCE_TYPE"] = "11"; //新增單品時, SOURCE_TYPE需設為 11
            dr["SEQNO"] = Detail.Rows.Count + 1;
            Detail.Rows.InsertAt(dr, 0);
            Detail.AcceptChanges();
            gv.DataSource = Detail;
            gv.DataBind();
        }

        /// <summary>
        /// 組合促銷資料
        /// </summary>
        /// <param name="gv"></param>
        public void NewRecord_Detail_MixPromotion(VSS_SAL_SA01 p, ASPxGridView gv, ASPxGridView gvDiscount, string[] args)
        {
            SaveSale_Detail_Temp(gv);
            if (args != null && args.Length > 0)
            {   //確實有回傳組合促銷商品資料
                //args[0] is "MixPromotion";
                //args[1] is Promotion_Code^Promotion_Name
                string[] valArray = args[1].Split('^');
                if (valArray != null && valArray.Length > 1)
                {
                    p.hidPromotion_Code.Value = valArray[0];
                    p.hidPromotion_Name.Value = valArray[1];
                }
                #region 處理促銷商品資料
                DataRow[] drs;

                if (p._SRC_TYPE == "Cache")
                    drs = Detail.Select(" PROMOTION_CODE = '" + p.hidPromotion_Code.Value + "' And POSUUID_DETAIL = '" + p.hidPosuuid_Detail.Value
                                                + "'", "SEQNO DESC");
                else
                    drs = Detail.Select(" PROMOTION_CODE = '" + p.hidPromotion_Code.Value + "' And POSUUID_DETAIL = '" + p.hidPosuuid_Detail.Value
                                                + "'", "SEQNO");
                if (drs != null && drs.Length > 0)
                {
                    #region 明細中有組合促銷資料
                    int sameLength = drs.Length;
                    int startInd = 0;

                    if (drs[0]["SIM_CARD_NO"] != null && StringUtil.CStr(drs[0]["SIM_CARD_NO"]) != "")
                        startInd = 1;

                    if (drs.Length - startInd > args.Length - 2)
                    {   //明細中組合促銷資料筆數大於回報筆數
                        sameLength = args.Length - 2;
                        for (int i = args.Length - 2 + startInd; i < drs.Length; i++)
                        {
                            DataRow dr = drs[i];
                            Detail.Rows.Remove(dr);
                            Detail.AcceptChanges();
                        }
                    }

                    for (int i = startInd; i < sameLength + startInd; i++)
                    {   //為避免遺失資料,所以使用更新的方式,來更新資料
                        valArray = args[i - startInd + 2].Split('^');
                        if (valArray != null && valArray.Length > 2)
                        {
                            DataRow dr = drs[i];
                            dr["PRODNO"] = valArray[0];
                            dr["PRODNAME"] = valArray[1];
                            dr["UNIT_PRICE"] = valArray[2];
                            int price = 0;
                            if (valArray[2] != null && valArray[2] != "" && NumberUtil.IsNumeric(valArray[2]))
                                price = int.Parse(valArray[2]);
                            int qty = 0;
                            if (dr["QUANTITY"] != null && StringUtil.CStr(dr["QUANTITY"]) != "" && NumberUtil.IsNumeric(StringUtil.CStr(dr["QUANTITY"])))
                                qty = int.Parse(StringUtil.CStr(dr["QUANTITY"]));
                            dr["TOTAL_AMOUNT"] = StringUtil.CStr(price * qty);
                            Detail.AcceptChanges();
                        }
                    }
                    #region 明細中組合促銷資料筆數小於回報筆數
                    if (args.Length - 2 > drs.Length - startInd)
                    {
                        for (int i = drs.Length - startInd + 2; i < args.Length; i++)
                        {
                            valArray = args[i].Split('^');
                            DataRow dr = Detail.NewRow();
                            dr["POSUUID_MASTER"] = StringUtil.CStr(Head.Rows[0]["POSUUID_MASTER"]);
                            dr["POSUUID_DETAIL"] = p.hidPosuuid_Detail.Value;
                            dr["ID"] = StringUtil.CStr(Advtek.Utility.GuidNo.getUUID());
                            dr["PRODNO"] = valArray[0];
                            dr["PRODNAME"] = valArray[1];
                            dr["IMEI_QTY"] = 0;
                            dr["QUANTITY"] = 1;
                            dr["UNIT_PRICE"] = valArray[2];
                            dr["TOTAL_AMOUNT"] = valArray[2];
                            dr["ITEM_TYPE"] = "2";
                            dr["ITEM_TYPE_NAME"] = "促";
                            dr["PROMOTION_CODE"] = p.hidPromotion_Code.Value;
                            dr["PROMO_NAME"] = p.hidPromotion_Name.Value;
                            dr["SEQNO"] = Detail.Rows.Count + 1;
                            if (p.hidSOURCE_TYPE.Value != null)
                                dr["SOURCE_TYPE"] = p.hidSOURCE_TYPE.Value;
                            Detail.Rows.InsertAt(dr, 0);
                            Detail.AcceptChanges();
                        }
                    }   
                    #endregion 明細中組合促銷資料筆數小於回報筆數
                    #endregion 明細中有組合促銷資料
                }
                else
                {   //明細中無組合促銷資料
                    for (int i = 2; i < args.Length; i++)
                    {
                        valArray = args[i].Split('^');
                        if (valArray != null && valArray.Length > 2)
                        {
                            DataRow dr = Detail.NewRow();
                            dr["POSUUID_MASTER"] = StringUtil.CStr(Head.Rows[0]["POSUUID_MASTER"]);
                            dr["POSUUID_DETAIL"] = p.hidPosuuid_Detail.Value;
                            dr["ID"] = StringUtil.CStr(Advtek.Utility.GuidNo.getUUID());
                            dr["PRODNO"] = valArray[0];
                            dr["PRODNAME"] = valArray[1];
                            dr["IMEI_QTY"] = 0;
                            dr["QUANTITY"] = 1;
                            dr["UNIT_PRICE"] = valArray[2];
                            dr["TOTAL_AMOUNT"] = valArray[2];
                            dr["ITEM_TYPE"] = "2";
                            dr["ITEM_TYPE_NAME"] = "促";
                            dr["PROMOTION_CODE"] = p.hidPromotion_Code.Value;
                            dr["PROMO_NAME"] = p.hidPromotion_Name.Value;
                            dr["SEQNO"] = Detail.Rows.Count + 1;
                            if (p.hidSOURCE_TYPE.Value != null)
                                dr["SOURCE_TYPE"] = p.hidSOURCE_TYPE.Value;
                            Detail.Rows.InsertAt(dr, 0);
                            Detail.AcceptChanges();
                        }
                    }
                }
                gv.DataSource = Detail;
                gv.DataBind();
                #endregion 處理促銷商品資料
                #region 處理Discount及贈品
                drs = Detail.Select(" PROMOTION_CODE = '" + p.hidPromotion_Code.Value + "' And POSUUID_DETAIL = '" + p.hidPosuuid_Detail.Value
                                    + "'", "SEQNO");
                DataRow[] drsDis = Discount.Select(" PROMOTION_CODE = '" + p.hidPromotion_Code.Value + "' And POSUUID_DETAIL = '" + p.hidPosuuid_Detail.Value
                                                    + "'", "SEQNO");
                if (drsDis != null && drsDis.Length > 0)
                {
                    foreach (DataRow drDis in drsDis)
                    {
                        Discount.Rows.Remove(drDis);
                        Discount.AcceptChanges();
                    }
                }

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
                    foreach (DataRow drDis in dtDiscount.Rows)
                    {
                        DataRow dr = Discount.NewRow();
                        dr["POSUUID_MASTER"] = StringUtil.CStr(Head.Rows[0]["POSUUID_MASTER"]);
                        dr["POSUUID_DETAIL"] = p.hidPosuuid_Detail.Value;
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
                        dr["UNIT_PRICE"] = price;
                        dr["TOTAL_AMOUNT"] = price;
                        dr["ITEM_TYPE"] = "5";
                        dr["ITEM_TYPE_NAME"] = "折";
                        dr["PROMOTION_CODE"] = p.hidPromotion_Code.Value;
                        dr["PROMO_NAME"] = p.hidPromotion_Name.Value;
                        dr["SEQNO"] = Discount.Rows.Count + 1;
                        if (p.hidSOURCE_TYPE.Value != null)
                            dr["SOURCE_TYPE"] = p.hidSOURCE_TYPE.Value;
                        Discount.Rows.InsertAt(dr, 0);
                        Discount.AcceptChanges();
                    }
                }

                gvDiscount.DataSource = Discount;
                gvDiscount.DataBind();
                #endregion 處理Discount及贈品
                calTotalAmount(p);
            }
        }

        #region 新增折扣
        /// <summary>
        /// 折扣
        /// </summary>
        /// <param name="gv"></param>
        public void NewRow_Detail_ItemDiscount(VSS_SAL_SA01 p, ASPxGridView gv)
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

        /// <summary>
        /// ETC加值資料
        /// </summary>
        /// <param name="gv"></param>
        public void NewRow_Detail_ETCAdd(VSS_SAL_SA01 p, ASPxGridView gv, string[] args)
        {
            SaveSale_Detail_Temp(gv);
            DataRow dr = Detail.NewRow();
            dr["POSUUID_MASTER"] = StringUtil.CStr(Head.Rows[0]["POSUUID_MASTER"]);
            dr["POSUUID_DETAIL"] = "";
            dr["ID"] = StringUtil.CStr(Advtek.Utility.GuidNo.getUUID());
            dr["PRODNO"] = p.hidETCProdNo.Value;
            dr["PRODNAME"] = "ETC加值";
            dr["IMEI_QTY"] = 0;
            dr["QUANTITY"] = 1;
            dr["UNIT_PRICE"] = args[1];
            dr["TOTAL_AMOUNT"] = args[1];
            dr["ITEM_TYPE"] = "2";
            dr["ITEM_TYPE_NAME"] = "服務系統";
            dr["SEQNO"] = Detail.Rows.Count + 1;
            dr["SOURCE_TYPE"] = "3"; //新增ETC加值時, SOURCE_TYPE需設為 3 代收
            Detail.Rows.InsertAt(dr, 0);
            Detail.AcceptChanges();
            gv.DataSource = Detail;
            gv.DataBind();
            DataRow drHead = Head.Rows[0];
            drHead["SALE_TYPE"] = "2"; //代收交易
            Head.AcceptChanges();
            calTotalAmount(p);
            p.hidETCAMT.Value = args[1];
        }

        /// <summary>
        /// Happy Go 折抵
        /// </summary>
        /// <param name="args">
        /// [1] = 折扣料號
        /// [2] = 折扣名稱
        /// [3] = 兌點金額
        /// [4] = HG卡號
        /// [5] = 兌換點數
        /// [6] = 門號
        /// [7] = 剩餘點數
        /// [8] = 兌點規則 
        /// [9] = 數量
        /// [10] = 單價
        /// </param>
        /// <param name="gv"></param>
        public void NewRow_Detail_HappyGoNet(VSS_SAL_SA01 p, ASPxGridView gv, string[] args)
        {
            SaveSale_Detail_Temp(gv);
            DataRow dr = Detail.NewRow();
            int unitPrice = 0;
            int totalAmt = 0;
            int qty = 0;
            if (!string.IsNullOrEmpty(args[1])) 
                if ((!string.IsNullOrEmpty(args[10])) && NumberUtil.IsNumeric(args[10]))
                    unitPrice = int.Parse(args[10]);
            if (!string.IsNullOrEmpty(args[1]))
                if ((!string.IsNullOrEmpty(args[3])) && NumberUtil.IsNumeric(args[3]))
                    totalAmt = int.Parse(args[3]);
            if (!string.IsNullOrEmpty(args[1]))
                if ((!string.IsNullOrEmpty(args[9])) && NumberUtil.IsNumeric(args[9]))
                    qty = int.Parse(args[9]);
            dr["POSUUID_MASTER"] = StringUtil.CStr(Head.Rows[0]["POSUUID_MASTER"]);
            dr["POSUUID_DETAIL"] = "";//未結清單,由此新增沒有
            dr["ID"] = StringUtil.CStr(Advtek.Utility.GuidNo.getUUID());
            dr["PRODNO"] = args[1];
            dr["PRODNAME"] = args[2];
            dr["IMEI_QTY"] = 0;
            dr["QUANTITY"] = qty;
            if (args[8] == "")
            {
                //HG 加價購
                dr["ITEM_TYPE"] = "14";
                dr["ITEM_TYPE_NAME"] = "加";
                dr["UNIT_PRICE"] = unitPrice;
                dr["TOTAL_AMOUNT"] = totalAmt;   //加價金額
                DataTable dtProdPrice = p.Facade.getProductPrice(args[1]);
                if (dtProdPrice != null && dtProdPrice.Rows.Count > 0)
                {
                    dr["ORI_UNIT_PRICE"] = dtProdPrice.Rows[0][0];
                }
            }
            else
            {
                dr["QUANTITY"] = 1;
                dr["ITEM_TYPE"] = "4";
                dr["ITEM_TYPE_NAME"] = "折";
                dr["UNIT_PRICE"] = -1 * unitPrice;
                dr["TOTAL_AMOUNT"] = -1 * totalAmt;   //兌點金額
                if (args[8] == "PROMOTION")
                    dr["HG_RULE_PROMOTION"] = -1 * totalAmt;
                else if (args[8] == "PRODUCT")
                    dr["HG_RULE_PRODUCT"] = -1 * totalAmt;
                else if (args[8] == "COMMON")
                    dr["HG_RULE_COMMON"] = -1 * totalAmt;
            }
            dr["SEQNO"] = Detail.Rows.Count + 1;
            dr["HG_CARD_NO"] = p.hdHG_CARD_NO.Text = args[4];  //HG卡號
            dr["HG_REDEEM_POINT"] = args[5]; //兌換點數
            dr["MSISDN"] = args[6]; //門號
            p.hdHG_LEFT_POINT.Text = args[7]; //剩餘點數
            p.hdHG_CARD_NO.Text = args[4];
            p.lbHG_CARD_NO.Value = args[4];
            p.lbHG_REMAIN_POINT.Value = args[7];

            Detail.Rows.InsertAt(dr, 0);
            Detail.AcceptChanges();
            gv.DataSource = Detail;
            gv.DataBind();
            calTotalAmount(p);
        }

        /// <summary>
        /// 普通商品加價購及贈品
        /// </summary>
        /// <param name="args">
        /// [0] = 商品料號
        /// [1] = 商品名稱
        /// [2] = 原價
        /// [3] = 加購價
        /// [4] = 數量
        /// [5] = 小計
        /// [6] = ITEM_TYPE
        /// </param>
        /// <param name="gv"></param>
        public void NewRow_Detail_AddProd(VSS_SAL_SA01 p, ASPxGridView gv, string[] args)
        {
            SaveSale_Detail_Temp(gv);
            DataRow dr = Detail.NewRow();
            int unitPrice = 0;
            int totalAmt = 0;
            int qty = 0;
            int oriPrice = 0;
            if (!string.IsNullOrEmpty(args[0]))
                if ((!string.IsNullOrEmpty(args[2])) && NumberUtil.IsNumeric(args[2]))
                    oriPrice = int.Parse(args[2]);
            if (!string.IsNullOrEmpty(args[0]))
                if ((!string.IsNullOrEmpty(args[3])) && NumberUtil.IsNumeric(args[3]))
                    unitPrice = int.Parse(args[3]);
            if (!string.IsNullOrEmpty(args[0]))
                if ((!string.IsNullOrEmpty(args[5])) && NumberUtil.IsNumeric(args[5]))
                    totalAmt = int.Parse(args[5]);
            if (!string.IsNullOrEmpty(args[0]))
                if ((!string.IsNullOrEmpty(args[4])) && NumberUtil.IsNumeric(args[4]))
                    qty = int.Parse(args[4]);
            dr["POSUUID_MASTER"] = StringUtil.CStr(Head.Rows[0]["POSUUID_MASTER"]);
            dr["POSUUID_DETAIL"] = "";//未結清單,由此新增沒有
            dr["ID"] = StringUtil.CStr(Advtek.Utility.GuidNo.getUUID());
            dr["PRODNO"] = args[0];
            dr["PRODNAME"] = args[1];
            dr["IMEI_QTY"] = 0;
            dr["QUANTITY"] = qty;
            dr["ITEM_TYPE"] = args[6];
            if (args[6] == "13")
                dr["ITEM_TYPE_NAME"] = "贈";
            else
                dr["ITEM_TYPE_NAME"] = "加";
            dr["UNIT_PRICE"] = unitPrice;
            dr["TOTAL_AMOUNT"] = totalAmt;   //加價金額
            dr["ORI_UNIT_PRICE"] = oriPrice;
            dr["SEQNO"] = Detail.Rows.Count + 1;

            Detail.Rows.InsertAt(dr, 0);
            Detail.AcceptChanges();
            gv.DataSource = Detail;
            gv.DataBind();
            calTotalAmount(p);
        }

        /// <summary>
        /// 門市特殊折扣
        /// </summary>
        /// <param name="gv"></param>
        /// <param name="args">
        /// [1] = 折扣料號
        /// [2] = 折扣名稱
        /// [3] = 門市特殊折扣折抵金額
        /// [4] = 門市特殊折扣折抵比率
        /// [5] = 門市特殊折扣折抵原因
        /// [6] = 門市特殊折扣折抵原因內容 
        /// [7] = 門市特殊折扣使用角色代碼 
        /// </param>
        public void NewRow_Detail_StoreDiscount(VSS_SAL_SA01 p, ASPxGridView gv, string[] args)
        {
            SaveSale_Detail_Temp(gv);
            DataRow dr = Detail.NewRow();
            int unitPrice = 0;
            dr["POSUUID_MASTER"] = StringUtil.CStr(Head.Rows[0]["POSUUID_MASTER"]);
            dr["POSUUID_DETAIL"] = "";//未結清單,由此新增沒有
            dr["ID"] = StringUtil.CStr(Advtek.Utility.GuidNo.getUUID());
            dr["PRODNO"] = args[1];
            dr["PRODNAME"] = args[2];
            dr["IMEI_QTY"] = 0;
            dr["QUANTITY"] = 1;
            if (!string.IsNullOrEmpty(args[1]))
                if ((!string.IsNullOrEmpty(args[3])) && args[3] != "" && NumberUtil.IsNumeric(args[3]))
                    unitPrice = int.Parse(args[3]);
            dr["UNIT_PRICE"] = -1 * unitPrice;
            dr["TOTAL_AMOUNT"] = -1 * unitPrice;
            dr["ITEM_TYPE"] = "6";
            dr["ITEM_TYPE_NAME"] = "折";
            dr["SEQNO"] = Detail.Rows.Count + 1;
            dr["SH_DISCOUNT_RATE"] = (string.IsNullOrEmpty(args[4]) ? "0" : args[4]); //店長折扣折抵比率
            dr["SH_DISCOUNT_REASON"] = (string.IsNullOrEmpty(args[5]) ? "0" : args[5]);  //店長折扣折抵原因
            if (args[7] == "1" && p.logMsg.ROLE_TYPE != "1")
                dr["SH_DISCOUNT_DESC"] = "[使用店長權限]" + (string.IsNullOrEmpty(args[6]) ? "" : args[6]); //店長折扣折抵原因內容
            else 
                dr["SH_DISCOUNT_DESC"] = (string.IsNullOrEmpty(args[6]) ? "" : args[6]); //店長折扣折抵原因內容
            Detail.Rows.InsertAt(dr, 0);
            Detail.AcceptChanges();
            gv.DataSource = Detail;
            gv.DataBind();
            calTotalAmount(p);
        }

        /// <summary>
        /// 授信通聯資料
        /// </summary>
        /// <param name="gv"></param>
        public void NewRow_Detail_CreditCommunication(VSS_SAL_SA01 p, ASPxGridView gv, string[] args)
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
                        if (i == 0)
                            dr["PRODNAME"] = "授信通聯申請費";
                        else
                            dr["PRODNAME"] = "授信通聯費用";
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

        /// <summary>
        /// 易通卡資料
        /// </summary>
        /// <param name="gv"></param>
        public void NewRow_Detail_ETCCard(VSS_SAL_SA01 p, ASPxGridView gv, string[] args)
        {
            SaveSale_Detail_Temp(gv);
            if (args != null && args.Length > 0)
            {
                for (int i = 0; i < args.Length; i++)
                {
                    string[] val = args[i].Split(new char[] { '^' });
                    if (val != null && val.Length > 1)
                    {
                        DataRow dr = Detail.NewRow();
                        dr["POSUUID_MASTER"] = StringUtil.CStr(Head.Rows[0]["POSUUID_MASTER"]);
                        dr["POSUUID_DETAIL"] = "";
                        dr["ID"] = StringUtil.CStr(Advtek.Utility.GuidNo.getUUID());
                        dr["PRODNO"] = val[0];
                        dr["CARD_NO"] = val[1];
                        if (i == 0)
                            dr["PRODNAME"] = "ETC加值卡";
                        else
                            dr["PRODNAME"] = "ETC加值卡保證金";
                        dr["IMEI_QTY"] = 0;
                        dr["QUANTITY"] = 1;
                        dr["UNIT_PRICE"] = val[2];
                        dr["TOTAL_AMOUNT"] = val[2];
                        dr["ITEM_TYPE"] = "1";
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
            dr["POSUUID_MASTER"] = StringUtil.CStr(Head.Rows[0]["POSUUID_MASTER"]);
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
        /// 付信用卡
        /// </summary>
        /// <param name="gv"></param>
        /// <param name="args">
        /// [0] = TYPE
        /// [1] = 金額
        /// [2] = 信用卡號
        /// [3] = 序號
        /// [4[ = 調閱編號
        /// [5] = 授權碼
        /// [6] = 銀行別
        /// [7] = 分期期數
        /// [8] = 信用卡別名稱
        /// </param>
        public void NewRow_Paid_CreditCard(ASPxGridView gv, string[] args, VSS_SAL_SA01 p)
        {
            SavePaid_Detail_Temp(gv);
            DataRow dr = Paid.NewRow();
            dr["ID"] = StringUtil.CStr(Advtek.Utility.GuidNo.getUUID());
            dr["POSUUID_MASTER"] = Head.Rows[0]["POSUUID_MASTER"];
            dr["PAID_AMOUNT"] = args[1];
            dr["DESCRIPTION"] = "信用卡號:" + args[2] + ",序號:" + args[3] + "調閱編號:" + args[4];
            dr["CREDIT_TYPE"] = 1; //1: 一般2: 分期3: 離線
            dr["PAID_MODE"] = 2;
            dr["PAID_MODE_NAME"] = "信用卡";
            dr["CREDIT_CARD_NO"] = args[2];//信用卡卡號
            dr["NCCC_REF_NO"] = args[3];//序號
            dr["NCCC_INV_NO"] = args[4];//調閱編號
            dr["CREDIT_BANK_ID"] = args[6];//銀行別
            DataTable dtCardType = null;
            if (args.Length > 8 && args[8] != null)
            {
                dtCardType = p.Facade.getCreditCardType(args[8]);
            } 
            else 
            {
                dtCardType = p.Facade.getCreditCardType(CreditCard_Facade.CheckCardType(args[2]));
            }
            if (dtCardType != null && dtCardType.Rows.Count > 0 && dtCardType.Rows[0]["CREDIT_CARD_TYPE_ID"] != null)
            {
                dr["CREDIT_CARD_TYPE_ID"] = StringUtil.CStr(dtCardType.Rows[0]["CREDIT_CARD_TYPE_ID"]);
                DataTable dtRate = p.Facade.getCreditDivRate(StringUtil.CStr(dtCardType.Rows[0]["CREDIT_CARD_TYPE_ID"]));
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

        /// <summary>
        /// 分期付款
        /// </summary>
        /// <param name="gv"></param>
        /// <param name="args">
        /// [0] = TYPE
        /// [1] = 金額
        /// [2] = 信用卡號
        /// [3] = 序號
        /// [4[ = 調閱編號
        /// [5] = 授權碼
        /// [6] = 銀行別
        /// [7] = 分期期數
        /// [8] = 信用卡別名稱
        /// </param>
        public void NewRow_Paid_DivCredit(ASPxGridView gv, string[] args, VSS_SAL_SA01 p)
        {
            SavePaid_Detail_Temp(gv);
            int period = 0;
            try
            {
                period = int.Parse(args[7]);
            }
            catch
            {
                return;
            }
            DataRow dr = Paid.NewRow();
            dr["ID"] = StringUtil.CStr(Advtek.Utility.GuidNo.getUUID());
            dr["POSUUID_MASTER"] = Head.Rows[0]["POSUUID_MASTER"];
            dr["DESCRIPTION"] = "信用卡號:" + args[2] + ",序號:" + args[3] + ",調閱編號:" + args[4] + ",銀行別" + args[6] + ",分期期數:" + args[7];
            dr["CREDIT_TYPE"] = 2; //1: 一般, 2: 分期, 3: 離線
            dr["PAID_MODE"] = 4;
            dr["PAID_MODE_NAME"] = "信用卡分期支付";
            dr["PAID_AMOUNT"] = args[1];
            dr["CREDIT_CARD_NO"] = args[2];//信用卡卡號
            dr["NCCC_REF_NO"] = args[3];//序號
            dr["NCCC_INV_NO"] = args[4];//調閱編號
            dr["CREDIT_BANK_ID"] = args[6];//銀行別
            dr["CREDIT_INSTALLMENT"] = period;//分期數
            DataTable dtCardType = null;
            if (args.Length > 8 && args[8] != null)
            {
                dtCardType = p.Facade.getCreditCardType(args[8]);
            } 
            else 
            {
                dtCardType = p.Facade.getCreditCardType(CreditCard_Facade.CheckCardType(args[2]));
            }
            if (dtCardType != null && dtCardType.Rows.Count > 0 && dtCardType.Rows[0]["CREDIT_CARD_TYPE_ID"] != null)
            {
                dr["CREDIT_CARD_TYPE_ID"] = StringUtil.CStr(dtCardType.Rows[0]["CREDIT_CARD_TYPE_ID"]);
                DataTable dtInstallmentId = p.Facade.getCreditCardInstallmentId(args[6], period);
                if (dtInstallmentId != null && dtInstallmentId.Rows.Count > 0)
                {
                    dr["INSTALLMENT_ID"] = StringUtil.CStr(dtInstallmentId.Rows[0]["INSTALLMENT_ID"]);
                    if (dr["INSTALLMENT_ID"] != null && StringUtil.CStr(dr["INSTALLMENT_ID"]) != "")
                    {
                        DataTable dtInterestRate = p.Facade.getCreditCardInterestRate(StringUtil.CStr(dr["INSTALLMENT_ID"]));
                        if (dtInterestRate != null && dtInterestRate.Rows.Count > 0 && dtInterestRate.Rows[0]["seqment_rate"] != null 
                            && NumberUtil.IsNumeric(StringUtil.CStr(dtInterestRate.Rows[0]["seqment_rate"])))
                        {
                            dr["INTEREST_RATE"] = double.Parse(StringUtil.CStr(dtInterestRate.Rows[0]["seqment_rate"]));
                        }

                        DataTable dtSettlementRate = p.Facade.getCreditCardSettlementRate(StringUtil.CStr(dr["INSTALLMENT_ID"]));
                        if (dtSettlementRate != null && dtSettlementRate.Rows.Count > 0 && dtSettlementRate.Rows[0]["settlement_rate"] != null
                            && NumberUtil.IsNumeric(StringUtil.CStr(dtSettlementRate.Rows[0]["settlement_rate"])))
                        {
                            dr["STORE_SETTLEMENT_RATE"] = double.Parse(StringUtil.CStr(dtSettlementRate.Rows[0]["settlement_rate"]));
                            if (args[1] != null && args[1] != "" && NumberUtil.IsNumeric(args[1]))
                                dr["STORE_SETTLEMENT_AMOUNT"] = Math.Round(int.Parse(args[1]) * double.Parse(StringUtil.CStr(dtSettlementRate.Rows[0]["settlement_rate"])) / 100, 2);
                        }
                    }
                }
            }
            Paid.Rows.InsertAt(dr, 0);
            Paid.AcceptChanges();
            gv.DataSource = Paid;
            gv.DataBind();
        }

        /// <summary>
        /// Happy GO
        /// </summary>
        /// <param name="gv"></param>
        /// <param name="args">
        /// [1] = 兌點金額
        /// [2] = HG卡號
        /// [3] = 兌換點數
        /// [4] = 剩餘點數
        /// [5] = 兌點規則 (保留)
        /// </param>
        public void NewRow_Paid_HappyGo(ASPxGridView gv, string[] args)
        {
            SavePaid_Detail_Temp(gv);
            DataRow dr = Paid.NewRow();
            dr["ID"] = StringUtil.CStr(Advtek.Utility.GuidNo.getUUID());
            dr["POSUUID_MASTER"] = Head.Rows[0]["POSUUID_MASTER"];
            dr["DESCRIPTION"] = "HG卡號:" + args[2] + ",兌換點數:" + args[3] + ",剩餘點數:" + args[4];
            dr["PAID_MODE"] = 7; //Happy Go兌點
            dr["PAID_MODE_NAME"] = "Happy Go兌點";
            dr["PAID_AMOUNT"] = args[1];    //兌點金額
            dr["HG_CARD_NO"] = args[2];     //HG卡號
            dr["HG_REDEEM_POINT"] = args[3];   //兌換點數
            dr["HG_LEFT_POINT"] = args[4];  //剩餘點數
            dr["HG_RULE"] = args[5];        //兌點規則

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
        public void Save_Sale_Data2Cache(VSS_SAL_SA01 p)
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
                dr["MACHINE_ID"] = p.logMsg.MACHINE_ID;
                dr["HOST_ID"] = p.logMsg.HOST_IP;
                dr["CREATE_DTM"] = p.logMsg.CREATE_DTM;
                dr["CREATE_USER"] = p.logMsg.CREATE_USER;
                dr["MODI_DTM"] = p.logMsg.MODI_DTM;
                dr["MODI_USER"] = p.logMsg.MODI_USER;
                if (NumberUtil.IsNumeric(p.lbTOTAL_AMOUNT.Text) && int.Parse(p.lbTOTAL_AMOUNT.Text) <= 0)
                {
                    //金額小於等於0,開立收據
                    dr["INVOICE_TYPE"] = "03";      //收據
                }
                else
                {
                    dr["INVOICE_TYPE"] = "01";      //連線電子發票
                }
                dr["ORIGINAL_ID"] = dr["POSUUID_MASTER"]; //最原始的ID, 從未作廢的會與ID同，退換貨時會一路帶下來
            }
            Head.AcceptChanges();
            foreach (DataRow dr in Detail.Rows)
            {
                dr["CREATE_DTM"] = p.logMsg.CREATE_DTM;
                dr["CREATE_USER"] = p.logMsg.CREATE_USER;
                dr["MODI_DTM"] = p.logMsg.MODI_DTM;
                dr["MODI_USER"] = p.logMsg.MODI_USER;
            }
            Detail.AcceptChanges();

            foreach (DataRow dr in Discount.Rows)
            {
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
                Facade.InsertSale_Detail(Detail.Select(" 1 = 1", "SEQNO Desc").CopyToDataTable());
                Facade.InsertSale_Detail(Discount);
                // Facade.InsertSale_IMEI_LOG(IMEI_LOG);
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
        public void Save_Sale_Data2Cache(VSS_SAL_SA01 p, OracleTransaction objTX)
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
                dr["CREATE_DTM"] = p.logMsg.CREATE_DTM;
                dr["CREATE_USER"] = p.logMsg.CREATE_USER;
                dr["MODI_DTM"] = p.logMsg.MODI_DTM;
                dr["MODI_USER"] = p.logMsg.MODI_USER;
            }
            Detail.AcceptChanges();

            foreach (DataRow dr in Discount.Rows)
            {
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
                //Facade.InsertSale_IMEI_LOG(IMEI_LOG, objTX);
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
        public void Delete_Sale_DataFromCache(VSS_SAL_SA01 p)
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
        public void Delete_Sale_DataFromCache(VSS_SAL_SA01 p, OracleTransaction objTX)
        {
            try
            {
                SAL01_Facade Facade = new SAL01_Facade();
                //Facade.DeleteSale_IMEI_LOG(IMEI_LOG, objTX);
                Facade.DeleteSale_Detail(Detail, objTX);
                Facade.DeleteSale_Detail(Discount, objTX);
                Facade.DeletePaid_DetailByPOSUUID_MASTER(StringUtil.CStr(Head.Rows[0]["POSUUID_MASTER"]), objTX);
                //Facade.DeletePaid_Detail(Paid);
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
        public void Save_Sale_Data2DBFromSAL05(VSS_SAL_SA01 p, OracleTransaction objTX)
        {
            SAL01_Facade Facade = new SAL01_Facade();
            string SALE_NO = Store_SerialNo.GenNo("SALE", p.logMsg.STORENO, p.logMsg.MACHINE_ID);
            foreach (DataRow dr in Head.Rows)
            {
                dr["SALE_STATUS"] = "2"; //結帳
                dr["SALE_NO"] = SALE_NO;
            }
            Head.AcceptChanges();

            //更新資料
            //Delete_Sale_DataFromCache(p, objTX);
            //Save_Sale_Data2Cache(p);
            //更新資料
            Facade.CheckOutFromUnClose(Head, Detail, p.logMsg.MODI_USER, objTX);
            SetInvoiceOrReceipt(objTX, p.logMsg.HOST_IP, p);//設定發票資料
            SaleHeadDataBind(p);
        }

        /// <summary>
        ///結帳時將資料存入DB從銷貨單新增
        /// </summary>
        /// <param name="p"></param>
        public void Save_Sale_Data2DBFromSAL01(VSS_SAL_SA01 p, OracleTransaction objTX)
        {
            SAL01_Facade Facade = new SAL01_Facade();
            string SAL_NO = Store_SerialNo.GenNo("SALE", p.logMsg.STORENO, p.logMsg.MACHINE_ID);
            foreach (DataRow dr in Head.Rows)
            {
                dr["SALE_STATUS"] = "2"; //結帳
                dr["SALE_NO"] = SAL_NO;
            }
            Head.AcceptChanges();

            //更新資料
            //Delete_Sale_DataFromCache(p);
            //Save_Sale_Data2Cache(p);
            //更新資料
            // Facade.CheckOutFromSAL01(Head);
            SetInvoiceOrReceipt(objTX, p.logMsg.HOST_IP, p);
            SaleHeadDataBind(p);
        }

        /// <summary>
        ///結帳時將資料從Cache存入DB
        /// </summary>
        /// <param name="p"></param>
        public void Save_Sale_Data2DBFromCache(VSS_SAL_SA01 p, OracleTransaction objTX)
        {

            SAL01_Facade Facade = new SAL01_Facade();
            string SAL_NO = Store_SerialNo.GenNo("SALE", p.logMsg.STORENO, p.logMsg.MACHINE_ID);
            foreach (DataRow dr in Head.Rows)
            {
                dr["SALE_STATUS"] = "2"; //結帳
                dr["SALE_NO"] = SAL_NO;
            }

            Head.AcceptChanges();
            //更新資料
            //Delete_Sale_DataFromCache(p);
            //Save_Sale_Data2Cache(p);
            //更新資料
            // Facade.CheckOutFromCache(Head);
            //判斷原來銷售交易是否來自未結清單
            DataRow[] drs = Detail.Select(" ITEM_TYPE IN ('2','5','11','12') ");
            if (drs != null && drs.Length > 0)
                Facade.CheckOutFromUnClose(Head, Detail, p.logMsg.MODI_USER, objTX);
            SetInvoiceOrReceipt(objTX, p.logMsg.HOST_IP, p);
            SaleHeadDataBind(p);
        }
        /// <summary>
        /// 檢查開立發票的原則 
        /// 1. 應付金額小於等於0，只開收據
        /// 2. [slae_head].sale_type = 2 (代收)一律開收據，但要檢查若sale_item裡有代表「作業處理費」的料號時，則要針對「作業處理費」另外開發票，
        /// 「作業處理費」是應稅發票，一樣會依taxable跟taxrate去判斷。
        /// 3. [slae_head].sale_type = 1 (一般交易)排除掉sale_item裡只開收據的項目，其餘一律開發票依taxable跟taxrate去判斷。
        /// </summary>
        public void SetInvoiceOrReceipt(OracleTransaction objTX, string HOST_ID, VSS_SAL_SA01 p)
        {
            DataRow[] INV_TAX_DRA;       //發票 應稅
            DataRow[] INV_TAX_DRA_ZERO;  //發票 應稅 0稅
            DataRow[] INV_NO_TAX_DRA;    //發票 免稅
            DataRow[] RECDT;             //收據

            DataTable dtInv = Detail.Clone();
            dtInv.Merge(Detail);
            dtInv.Merge(Discount);

            int saleTotalAmt = 0;
            if (Head.Rows[0]["SALE_TOTAL_AMOUNT"] != null && StringUtil.CStr(Head.Rows[0]["SALE_TOTAL_AMOUNT"]) != ""
                && NumberUtil.IsNumeric(StringUtil.CStr(Head.Rows[0]["SALE_TOTAL_AMOUNT"])))
                saleTotalAmt = int.Parse(StringUtil.CStr(Head.Rows[0]["SALE_TOTAL_AMOUNT"]));
            if (saleTotalAmt <= 0)
            {
                RECDT = dtInv.Select(" 1 = 1 ");
                p.Facade.InsertReceipt(Head, RECDT, objTX);                                             //收據
            }
            else
            {
                INV_TAX_DRA = dtInv.Select(" INV_TYPE = '1' AND TAXABLE = 'Y' AND TAXRATE <> 0 ");     //發票 應稅
                INV_TAX_DRA_ZERO = dtInv.Select(" INV_TYPE = '1' AND TAXABLE = 'Y' AND TAXRATE = 0 "); //發票 應稅 0稅
                INV_NO_TAX_DRA = dtInv.Select(" INV_TYPE = '1' AND TAXABLE = 'N' ");                   //發票 免稅
                RECDT = dtInv.Select(" INV_TYPE = '3' ");                                              //收據

                p.Facade.InsertInvoice(Head, INV_TAX_DRA, "1", objTX, HOST_ID);
                p.Facade.InsertInvoice(Head, INV_TAX_DRA_ZERO, "2", objTX, HOST_ID);
                p.Facade.InsertInvoice(Head, INV_NO_TAX_DRA, "3", objTX, HOST_ID);
                p.Facade.InsertReceipt(Head, RECDT, objTX);
            }
        }
        #endregion
        #region 當前模式
        /// <summary>
        /// 進入有貨品資料,無付款資料時
        /// </summary>
        /// <param name="p"></param>
        public void Mode_HavePROD(VSS_SAL_SA01 p)
        {
            p.btnOrderCheckOut.Enabled = false;
            p.btnOrderCancel.Enabled = true;
            p.btnReprint.Enabled = false;
            p.gvMaster.Enabled = true;
            p.gvDetail.Enabled = true;
            p.gvCheckOut.Enabled = true;
            p.txtREMARK.Enabled = true;
            p.txtUNI_NO.Enabled = true;
            p.txtUNI_TITLE.Enabled = true;

            if (Head.Rows[0]["SALE_TYPE"] != null && StringUtil.CStr(Head.Rows[0]["SALE_TYPE"]) == "2")
            {   //代收交易不可與其他交易合併結帳，所以不可以再新增任何交易細項與折扣
                p.gvMaster.FindChildControl<ASPxButton>("btnItemAdd").Enabled = false;
                p.gvMaster.FindChildControl<ASPxButton>("btnMixPromotion").Enabled = false;
                p.gvMaster.FindChildControl<ASPxButton>("btnOrdToSale").Enabled = false;
                p.gvMaster.FindChildControl<ASPxButton>("btnETCAdd").Enabled = false;
                p.gvMaster.FindChildControl<ASPxButton>("btnStoreDiscount").Enabled = false;
                p.gvMaster.FindChildControl<ASPxButton>("btnHappyGoNet").Enabled = false;
                p.gvMaster.FindChildControl<ASPxButton>("btnItemDel").Enabled = false;
                p.gvMaster.FindChildControl<ASPxButton>("btnCreditCommunication").Enabled = false;
            }
            else
            {
                p.gvMaster.FindChildControl<ASPxButton>("btnItemAdd").Enabled = true;
                p.gvMaster.FindChildControl<ASPxButton>("btnMixPromotion").Enabled = false;
                p.gvMaster.FindChildControl<ASPxButton>("btnOrdToSale").Enabled = false;
                p.gvMaster.FindChildControl<ASPxButton>("btnETCAdd").Enabled = false;
                p.gvMaster.FindChildControl<ASPxButton>("btnStoreDiscount").Enabled = true;
                p.gvMaster.FindChildControl<ASPxButton>("btnHappyGoNet").Enabled = true;
                p.gvMaster.FindChildControl<ASPxButton>("btnCreditCommunication").Enabled = true;

                DataRow[] dra = Detail.Select("ITEM_TYPE = '6'");
                if (dra != null && dra.Length > 0)  //貨品資料裡有特殊抱怨折扣項目，進入有特殊抱怨折扣模式
                {
                    Mode_HaveStoreDiscount(p);
                }

                DataRow[] drb = Detail.Select("ITEM_TYPE IN ('4','14')");
                DataRow drM = Head.Rows[0];
                if ((drb != null && drb.Length > 0) || StringUtil.CStr(drM["SALE_TYPE"]) == "2")  //貨品資料裡沒有HappyGo折扣項目且交易不屬【帳單代收】，才可點選
                {
                    p.gvMaster.FindChildControl<ASPxButton>("btnHappyGoNet").Enabled = false;
                }

                DataRow[] drc = Detail.Select("ITEM_TYPE IN ('7','13')");
                if (drc != null && drc.Length > 0)  //貨品資料裡沒有普通商品加價購及贈品，才可點選
                {
                    p.gvMaster.FindChildControl<ASPxButton>("btnAddProd").Enabled = false;
                }

                DataRow[] drd = Detail.Select("PROMOTION_CODE is not null");
                if (drd != null && drd.Length > 0)  //貨品資料裡有促銷代碼
                {
                    p.gvMaster.FindChildControl<ASPxButton>("btnMixPromotion").Enabled = true;
                }
            }
            p.btnConfirm.Enabled = true;
            p.btnCancel.Enabled = false;
            p.gvCheckOut.FindChildControl<ASPxButton>("btnCash").Enabled = false;
            p.gvCheckOut.FindChildControl<ASPxButton>("btnHappyGo").Enabled = false;
            p.gvCheckOut.FindChildControl<ASPxButton>("btnCredit").Enabled = false;
            p.gvCheckOut.FindChildControl<ASPxButton>("btnDivCredit").Enabled = false;
            p.gvCheckOut.FindChildControl<ASPxButton>("btnOffLineCredit").Enabled = false;
            p.gvCheckOut.FindChildControl<ASPxButton>("btnPayDEL").Enabled = false;
            p.lbChange.Text = "";
            p.lbPayAmount.Text = "";
        }
        /// <summary>
        /// 進入沒有輸入貨品時
        /// </summary>
        /// <param name="p"></param>
        public void Mode_NoPROD(VSS_SAL_SA01 p)
        {
            p.btnOrderCheckOut.Enabled = false;
            p.btnOrderCancel.Enabled = false;
            p.btnReprint.Enabled = false;
            p.btnConfirm.Enabled = false;
            p.btnCancel.Enabled = false;
            p.gvMaster.Enabled = true;
            p.gvDetail.Enabled = true;
            p.gvCheckOut.Enabled = false;
            p.txtREMARK.Enabled = true;
            p.txtUNI_NO.Enabled = true;
            p.txtUNI_TITLE.Enabled = true;
            p.gvMaster.FindChildControl<ASPxButton>("btnStoreDiscount").Enabled = false;
            p.gvMaster.FindChildControl<ASPxButton>("btnHappyGoNet").Enabled = false;
            p.gvMaster.FindChildControl<ASPxButton>("btnOrdToSale").Enabled = false;
            p.gvMaster.FindChildControl<ASPxButton>("btnMixPromotion").Enabled = false;
            p.gvMaster.FindChildControl<ASPxButton>("btnETCAdd").Enabled = true;
            p.gvMaster.FindChildControl<ASPxButton>("btnItemAdd").Enabled = true;
            p.gvMaster.FindChildControl<ASPxButton>("btnCreditCommunication").Enabled = true;
            p.gvMaster.FindChildControl<ASPxButton>("btnAddProd").Enabled = false;
        }
        /// <summary>
        /// 進入付款模式
        /// </summary>
        /// <param name="p"></param>
        public void Mode_PayCash(VSS_SAL_SA01 p)
        {
            p.btnReprint.Enabled = false;
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
            p.txtUNI_NO.Enabled = false;
            p.txtUNI_TITLE.Enabled = false;
            p.gvMaster.FindChildControl<ASPxButton>("btnETCAdd").Enabled = false;
            p.gvMaster.FindChildControl<ASPxButton>("btnStoreDiscount").Enabled = false;
            p.gvMaster.FindChildControl<ASPxButton>("btnHappyGoNet").Enabled = false;

            DataRow drM = Head.Rows[0];
            p.gvCheckOut.FindChildControl<ASPxButton>("btnCash").Enabled = false;
            p.gvCheckOut.FindChildControl<ASPxButton>("btnHappyGo").Enabled = false;
            p.gvCheckOut.FindChildControl<ASPxButton>("btnCredit").Enabled = false;
            p.gvCheckOut.FindChildControl<ASPxButton>("btnDivCredit").Enabled = false;
            p.gvCheckOut.FindChildControl<ASPxButton>("btnOffLineCredit").Enabled = false;

            DataTable dtPayMode = p.Facade.getValidPaymentMode();
            if (dtPayMode != null && dtPayMode.Rows.Count > 0)
            {
                foreach (DataRow drPayMode in dtPayMode.Rows)
                {
                    if (drPayMode[0] != null && StringUtil.CStr(drPayMode[0]) == "1")
                        p.gvCheckOut.FindChildControl<ASPxButton>("btnCash").Enabled = true;
                    else if (drPayMode[0] != null && StringUtil.CStr(drPayMode[0]) == "2")
                        p.gvCheckOut.FindChildControl<ASPxButton>("btnCredit").Enabled = true;
                    else if (drPayMode[0] != null && StringUtil.CStr(drPayMode[0]) == "3")
                        p.gvCheckOut.FindChildControl<ASPxButton>("btnOffLineCredit").Enabled = true;
                    else if (drPayMode[0] != null && StringUtil.CStr(drPayMode[0]) == "4")
                        p.gvCheckOut.FindChildControl<ASPxButton>("btnDivCredit").Enabled = true;
                }
            }
            
            if (StringUtil.CStr(drM["SALE_TYPE"]) == "2")    //HappyGo支付只適用【帳單代收】
            {
                if (dtPayMode != null && dtPayMode.Rows.Count > 0)
                {
                    DataRow[] drsPayMode = dtPayMode.Select(" pay_mode_id = '7' ");
                    if (drsPayMode != null && drsPayMode.Length > 0)
                        p.gvCheckOut.FindChildControl<ASPxButton>("btnHappyGo").Enabled = true;
                }
                DataRow[] drs = Detail.Select(" PRODNO = '" + p.hidETCProdNo.Value + "'");
                if (drs != null && drs.Length > 0)
                {   //結帳目中有ETC/NCIC/SEEDNET等只能使用現金結帳
                    p.gvCheckOut.FindChildControl<ASPxButton>("btnHappyGo").Enabled = false;
                    p.gvCheckOut.FindChildControl<ASPxButton>("btnCredit").Enabled = false;
                    p.gvCheckOut.FindChildControl<ASPxButton>("btnDivCredit").Enabled = false;
                    p.gvCheckOut.FindChildControl<ASPxButton>("btnOffLineCredit").Enabled = false;
                }
                drs = Detail.Select(" BARCODE1 is not null AND BARCODE2 is not null AND BARCODE3 is not null ");
                if (drs != null && drs.Length > 0)
                {   //結帳目中有ETC/NCIC/SEEDNET等只能使用現金結帳
                    p.gvCheckOut.FindChildControl<ASPxButton>("btnHappyGo").Enabled = false;
                    p.gvCheckOut.FindChildControl<ASPxButton>("btnCredit").Enabled = false;
                    p.gvCheckOut.FindChildControl<ASPxButton>("btnDivCredit").Enabled = false;
                    p.gvCheckOut.FindChildControl<ASPxButton>("btnOffLineCredit").Enabled = false;
                }
            }

            DataRow[] drsETC = Detail.Select(" PRODNO in ('" + p.hidETCCardProdNo.Value + "','" + p.hidETCCardPromiseProdNo.Value + "')");
            if (drsETC != null && drsETC.Length > 0)
            {   //結帳目中有易通卡或易通卡保證金等只能使用現金結帳
                p.gvCheckOut.FindChildControl<ASPxButton>("btnCredit").Enabled = false;
                p.gvCheckOut.FindChildControl<ASPxButton>("btnDivCredit").Enabled = false;
                p.gvCheckOut.FindChildControl<ASPxButton>("btnOffLineCredit").Enabled = false;
            }

            int limitAmt = p.Facade.getCreditDivLimitAmount();
            int shouldPayAmt = 0;
            if (p.lbPayAmount.Text == "")
            {
                if (Head.Rows[0]["SALE_TOTAL_AMOUNT"] != null && StringUtil.CStr(Head.Rows[0]["SALE_TOTAL_AMOUNT"]) != ""
                    && NumberUtil.IsNumeric(StringUtil.CStr(Head.Rows[0]["SALE_TOTAL_AMOUNT"])))
                    shouldPayAmt = int.Parse(StringUtil.CStr(Head.Rows[0]["SALE_TOTAL_AMOUNT"]));
            }
            else
            {
                if (NumberUtil.IsNumeric(p.lbPayAmount.Text))
                    shouldPayAmt = int.Parse(p.lbPayAmount.Text);
            }
            if (shouldPayAmt < limitAmt)    //消費金額需超過一定門檻值才可以使用信用卡分期支付
                p.gvCheckOut.FindChildControl<ASPxButton>("btnDivCredit").Enabled = false;
            //信用卡只能支付一次
            DataRow[] drsCredit = Paid.Select(" PAID_MODE in ('2', '3', '4') ");
            if (drsCredit != null && drsCredit.Length > 0)
            {
                p.gvCheckOut.FindChildControl<ASPxButton>("btnCredit").Enabled = false;
                p.gvCheckOut.FindChildControl<ASPxButton>("btnDivCredit").Enabled = false;
                p.gvCheckOut.FindChildControl<ASPxButton>("btnOffLineCredit").Enabled = false;
            }
        }
        /// <summary>
        /// 進入已結帳模式
        /// </summary>
        /// <param name="p"></param>
        public void Mode_CheckOut(VSS_SAL_SA01 p)
        {
            p.btnOrderCheckOut.Enabled = false;
            p.btnOrderCancel.Enabled = false;
            if (Head.Rows[0]["SALE_STATUS"] != null && StringUtil.CStr(Head.Rows[0]["SALE_STATUS"]) != "2")
            {
                //狀態非已結帳狀態不可列印單據
                p.btnReprint.Enabled = false;
            }
            else
            {
                p.btnReprint.Enabled = true;
            }
            p.btnConfirm.Enabled = false;
            p.btnCancel.Enabled = false;
            p.gvMaster.Enabled = false;
            p.gvCheckOut.Enabled = false;
            p.txtREMARK.Enabled = false;
            p.txtUNI_NO.Enabled = false;
            p.txtUNI_TITLE.Enabled = false;
            p.gvMaster.FindChildControl<ASPxButton>("btnStoreDiscount").Enabled = false;
            p.gvMaster.FindChildControl<ASPxButton>("btnHappyGoNet").Enabled = false;
            p.gvMaster.FindChildControl<ASPxButton>("btnETCAdd").Enabled = true;
            p.gvMaster.FindChildControl<ASPxButton>("btnItemAdd").Enabled = true;
        }
        /// <summary>
        /// 進入有輸入店長折扣時
        /// </summary>
        /// <param name="p"></param>
        public void Mode_HaveStoreDiscount(VSS_SAL_SA01 p)
        {
            p.gvMaster.FindChildControl<ASPxButton>("btnItemAdd").Enabled = false;
            p.gvMaster.FindChildControl<ASPxButton>("btnHappyGoNet").Enabled = false;
            p.gvMaster.FindChildControl<ASPxButton>("btnOrdToSale").Enabled = false;
            p.gvMaster.FindChildControl<ASPxButton>("btnMixPromotion").Enabled = false;
            p.gvMaster.FindChildControl<ASPxButton>("btnETCAdd").Enabled = false;
            p.gvMaster.FindChildControl<ASPxButton>("btnStoreDiscount").Enabled = false;
            p.gvMaster.FindChildControl<ASPxButton>("btnCreditCommunication").Enabled = false;
            p.gvMaster.FindChildControl<ASPxButton>("btnAddProd").Enabled = false;
        }
        /// <summary>
        /// 進入有輸入ETC加值時
        /// </summary>
        /// <param name="p"></param>
        public void Mode_HaveETCAdd(VSS_SAL_SA01 p)
        {
            p.gvMaster.FindChildControl<ASPxButton>("btnItemAdd").Enabled = false;
            p.gvMaster.FindChildControl<ASPxButton>("btnHappyGoNet").Enabled = true;
            p.gvMaster.FindChildControl<ASPxButton>("btnOrdToSale").Enabled = false;
            p.gvMaster.FindChildControl<ASPxButton>("btnMixPromotion").Enabled = false;
            p.gvMaster.FindChildControl<ASPxButton>("btnETCAdd").Enabled = false;
            p.gvMaster.FindChildControl<ASPxButton>("btnStoreDiscount").Enabled = false;
            p.gvMaster.FindChildControl<ASPxButton>("btnCreditCommunication").Enabled = false;
            p.gvMaster.FindChildControl<ASPxButton>("btnAddProd").Enabled = false;
        }
        #endregion
        #region 計算總金額
        public void calTotalAmount(VSS_SAL_SA01 p)
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
        public void CalculationTax(VSS_SAL_SA01 p)
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
                            dr["BEFORE_TAX"] = StringUtil.CStr(dr["TOTAL_AMOUNT"]);                  //稅前金額
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
                    SALE_TOTAL_TAX = (int)Math.Round((double)((SALE_AFTER_TAX_TOTAL_AMOUNT+DISCOUNT_AFTER_TAX_TOTAL_AMOUNT) * TAXRATE / (100 + TAXRATE)));//四捨五入
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
            if (SALE_AFTER_TOTAL_AMOUNT > 0)
            {
                //int DETAIL_TOTALE_AMOUNT = 0,///稅前金額，商品、銷售折扣、店長折扣、Happy Go折抵由稅額計算規則算出
                //    DETAIL_TAX = 0,          ///稅額，商品、銷售折扣、店長折扣、Happy Go折抵由稅額計算規則算出
                //    DETAIL_BEFORE_TAX = 0;   ///稅後金額，商品、銷售折扣、店長折扣、Happy Go折抵QUANTITY*UNIT_PRICE
                //取出須算稅額的資料
                DataRow[] Sale_DetailRows = Detail.Select("ITEM_TYPE IN ('1','2','3','7','8','9','10','13','14') AND TAXABLE = 'Y'", "TOTAL_AMOUNT"); //Y:應稅N:免稅;Y:應稅，taxrate為 0，為零稅
                DataRow[] Sale_Discount_DetailRows = Detail.Select("ITEM_TYPE IN ('4','6') AND TAXABLE = 'Y'", "TOTAL_AMOUNT");//Y:應稅N:免稅;Y:應稅，taxrate為 0，為零稅
                DataRow[] Discount_DetailRows = Discount.Select("ITEM_TYPE IN ('5','11','12') AND TAXABLE = 'Y'", "TOTAL_AMOUNT");//Y:應稅N:免稅;Y:應稅，taxrate為 0，為零稅

                CalDetailTax(SALE_TOTAL_TAX, SALE_AFTER_TOTAL_AMOUNT, DISCOUNT_TOTAL_TAX, DISCOUNT_AFTER_TOTAL_AMOUNT, 
                                ref Sale_DetailRows, ref Discount_DetailRows, ref Sale_Discount_DetailRows);
            }
            else
            {   //交易總金額小於等於0,不開發票,稅視為0
                foreach (DataRow dr in Detail.Rows)//明細資料 含 ITME 1,2,3應收 和 5,6折扣類 不含5,11,12未結清單折扣 
                {
                    dr["TAX"] = 0;
                    dr["BEFORE_TAX"] = dr["TOTAL_AMOUNT"];
                }

                foreach (DataRow dr in Discount.Rows) //未結轉入才有的資料 ITEM_TYPE = 5,11,12;
                {
                    dr["TAX"] = 0;
                    dr["BEFORE_TAX"] = dr["TOTAL_AMOUNT"];
                }
            }
            Discount.AcceptChanges();
            Detail.AcceptChanges();
            //--------------------------------------
            #endregion
            #region 付款金額 找零金
            p.lbPayAmount.Text = "";
            p.lbChange.Text = "";
            #endregion
        }
        #endregion
        #region 計算明細稅額
        public void CalDetailTax(int SALE_TOTAL_TAX, int SALE_AFTER_TOTAL_AMOUNT, int DISCOUNT_TOTAL_TAX, int DISCOUNT_AFTER_TOTAL_AMOUNT, 
                                 ref DataRow[] drsSaleDetail, ref DataRow[] drsSaleDiscount, ref DataRow[] drsDiscount)
        {
            int tempTotal_Tax = SALE_TOTAL_TAX - DISCOUNT_TOTAL_TAX;
            int DETAIL_TOTALE_AMOUNT = 0;
            int DETAIL_TAX = 0;
            int DETAIL_BEFORE_TAX = 0;
            for (int i = 0; i < drsSaleDiscount.Length; i++)//明細資料 含 ITME 1,2,3應收 和 5,6折扣類 不含5未結清單折扣 
            {
                DataRow dr = drsSaleDiscount[i];
                if (dr["TOTAL_AMOUNT"] != DBNull.Value && dr["TOTAL_AMOUNT"] != null
                    && StringUtil.CStr(dr["TOTAL_AMOUNT"]) != "" && NumberUtil.IsNumeric(StringUtil.CStr(dr["TOTAL_AMOUNT"])))
                    DETAIL_TOTALE_AMOUNT = int.Parse(StringUtil.CStr(dr["TOTAL_AMOUNT"]));
                else
                    DETAIL_TOTALE_AMOUNT = 0;
                if (DISCOUNT_AFTER_TOTAL_AMOUNT != 0)
                    DETAIL_TAX = (int)Math.Round((double)(DISCOUNT_TOTAL_TAX * (DETAIL_TOTALE_AMOUNT / DISCOUNT_AFTER_TOTAL_AMOUNT)));// [sale_detail].sale_tax= [sale_head].sale_tax * ([sale_detail].total_amount/X)                    
                else
                    DETAIL_TAX = 0;
                DETAIL_BEFORE_TAX = DETAIL_TOTALE_AMOUNT - DETAIL_TAX; //Y1 = X - Y3 - Y2 = 57 - 5 - 24 = 28
                dr["TAX"] = DETAIL_TAX;
                dr["BEFORE_TAX"] = DETAIL_BEFORE_TAX;
                dr["TOTAL_AMOUNT"] = DETAIL_TOTALE_AMOUNT;
            }

            for (int i = 0; i < drsDiscount.Length; i++)//明細資料 含 ITME 1,2,3應收 和 5,6折扣類 不含5未結清單折扣 
            {
                DataRow dr = drsDiscount[i];
                if (dr["TOTAL_AMOUNT"] != DBNull.Value && dr["TOTAL_AMOUNT"] != null
                    && StringUtil.CStr(dr["TOTAL_AMOUNT"]) != "" && NumberUtil.IsNumeric(StringUtil.CStr(dr["TOTAL_AMOUNT"])))
                    DETAIL_TOTALE_AMOUNT = int.Parse(StringUtil.CStr(dr["TOTAL_AMOUNT"]));
                else
                    DETAIL_TOTALE_AMOUNT = 0;
                if (DISCOUNT_AFTER_TOTAL_AMOUNT != 0)
                    DETAIL_TAX = (int)Math.Round((double)(DISCOUNT_TOTAL_TAX * (DETAIL_TOTALE_AMOUNT / DISCOUNT_AFTER_TOTAL_AMOUNT)));// [sale_detail].sale_tax= [sale_head].sale_tax * ([sale_detail].total_amount/X)                    
                else
                    DETAIL_TAX = 0;
                DETAIL_BEFORE_TAX = DETAIL_TOTALE_AMOUNT - DETAIL_TAX; //Y1 = X - Y3 - Y2 = 57 - 5 - 24 = 28
                dr["TAX"] = DETAIL_TAX;
                dr["BEFORE_TAX"] = DETAIL_BEFORE_TAX;
                dr["TOTAL_AMOUNT"] = DETAIL_TOTALE_AMOUNT;
            }

            for (int i = 0; i < drsSaleDetail.Length; i++) //未結轉入才有的資料 ITEM_TYPE = 5;
            {
                DataRow dr = drsSaleDetail[i];
                if (dr["TOTAL_AMOUNT"] != DBNull.Value && dr["TOTAL_AMOUNT"] != null
                    && StringUtil.CStr(dr["TOTAL_AMOUNT"]) != "" && NumberUtil.IsNumeric(StringUtil.CStr(dr["TOTAL_AMOUNT"])))
                    DETAIL_TOTALE_AMOUNT = int.Parse(StringUtil.CStr(dr["TOTAL_AMOUNT"]));
                else
                    DETAIL_TOTALE_AMOUNT = 0;
                if (SALE_AFTER_TOTAL_AMOUNT != 0)
                    DETAIL_TAX = (int)Math.Round((double)((SALE_TOTAL_TAX - DISCOUNT_TOTAL_TAX) * (DETAIL_TOTALE_AMOUNT / (SALE_AFTER_TOTAL_AMOUNT - DISCOUNT_AFTER_TOTAL_AMOUNT))));// [sale_detail].sale_tax= [sale_head].sale_tax * ([sale_detail].total_amount/X)                    
                else
                    DETAIL_TAX = 0;    
                DETAIL_BEFORE_TAX = DETAIL_TOTALE_AMOUNT - DETAIL_TAX;

                if (i + 1 == drsSaleDetail.Length) //最後一筆 
                {
                    DETAIL_BEFORE_TAX = DETAIL_TOTALE_AMOUNT - tempTotal_Tax; //Y1 = X - Y3 - Y2 = 57 - 5 - 24 = 28
                    DETAIL_TAX = tempTotal_Tax;
                }
                
                dr["TAX"] = DETAIL_TAX;
                dr["BEFORE_TAX"] = DETAIL_BEFORE_TAX;
                dr["TOTAL_AMOUNT"] = DETAIL_TOTALE_AMOUNT;
                tempTotal_Tax -= DETAIL_TAX;
            }
        }
        #endregion
        #region 計算應付金額,找零金
        public void CalPayCharge(VSS_SAL_SA01 p)
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

            string encryptUrl = "";

            //**2011/04/21 Tina：前頁傳遞參數時，會先以加密處理，在此要解密。
            if (!string.IsNullOrEmpty(Request.QueryString["Param"]))
            {
                NameValueCollection qscoll = Utils.Param_UrlDecode(Request.QueryString["Param"]);
                foreach (string key in qscoll.AllKeys)
                {
                    if (key == "PKEY")
                    {
                        encryptUrl = string.Join(",", qscoll.GetValues(key));
                        break;
                    }
                }
            }

            return encryptUrl;
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
            }
            TempTables.Head.AcceptChanges();

            //取得表身資料取得多筆 未結清表身資料  1-2 ,1-3,1-1 取得 2+3+1 = 5筆資料
            TempTables.Detail = Facade.getTO_CLOSE_ITEM(_PKEY, "2");//未結清單表身
            TempTables.Detail.Columns.Add("ON_HAND_QTY");
            Session["HGDISTempTable"] = TempTables.Detail; //for HappyGo折抵時，查詢【促銷商品折抵活動】，以及【單商品折抵活動】使用。
            TempTables.Discount = Facade.getTO_CLOSE_ITEM(_PKEY, "5");//未結折扣資料

            foreach (DataRow dr in TempTables.Detail.Rows)
            {
                string discountType = Facade.getDISCOUNT_TYPE(StringUtil.CStr(dr["PRODNO"]));
                if (discountType == "2")
                    dr["ITEM_TYPE"] = "12"; //舊機回收折扣
                else if (discountType == "3")
                    dr["ITEM_TYPE"] = "11"; //租賃折扣

                dr["POSUUID_MASTER"] = TempTables.Head.Rows[0];
                dr["ON_HAND_QTY"] = 999;
                if (dr["TOTAL_AMOUNT"] != DBNull.Value && dr["TOTAL_AMOUNT"] != null)
                    totAmt += Convert.ToDouble(dr["TOTAL_AMOUNT"]);
            }
            TempTables.Detail.AcceptChanges();
            foreach (DataRow dr in TempTables.Discount.Rows)
            {
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

            // TempTables.IMEI_LOG = Facade.getSale_IMEI_LOG("");
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

            //有促銷商品，組組合促銷商品清單，供組合促銷使用
            DataRow[] drsPromotion = TempTables.Detail.Select(" Promotion_Code is not null ");
            if (drsPromotion != null && drsPromotion.Length > 0)
            {
                string prePromotionCode = "";
                StringBuilder proProdList = new StringBuilder("");
                foreach (DataRow dr in drsPromotion)
                {
                    if (StringUtil.CStr(dr["PROMOTION_CODE"]) != prePromotionCode)
                    {
                        if (prePromotionCode == "")
                            proProdList.Append(StringUtil.CStr(dr["PROMOTION_CODE"])).Append("|").Append(StringUtil.CStr(dr["PRODNO"]));
                        else
                            proProdList.Append(";").Append(StringUtil.CStr(dr["PROMOTION_CODE"])).Append("|").Append(StringUtil.CStr(dr["PRODNO"]));
                        prePromotionCode = StringUtil.CStr(dr["PROMOTION_CODE"]);
                    }
                    else
                        proProdList.Append("^").Append(StringUtil.CStr(dr["PRODNO"]));
                }
                hidPromotionProdList.Value = StringUtil.CStr(proProdList);
            }
        }
        else
        {
            if (Facade.IsFinishJob(_PKEY))
            {
                string JobFinishPage = System.Web.Configuration.WebConfigurationManager.AppSettings["JobFinishPage"];
                this.Response.Redirect(JobFinishPage);
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
        //TempTables.IMEI_LOG = Facade.getSale_IMEI_LOG("");
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
        TempTables.CalPayCharge(this);
        TempTables.SaleHeadDataBind(this);
        TempTables.Mode_CheckOut(this);
    }

    /// <summary>
    /// 直接從銷售單新增資料時
    /// </summary>
    void LoadSAL01Data()
    {
        string POSUUID_MASTER = "";
        TempTables.Head = Facade.getSale_Head(POSUUID_MASTER);
        TempTables.Detail = Facade.getSale_Detail(POSUUID_MASTER, "1,2,3,4,6,7,8,9,10,13", logMsg.STORENO, this._STOCK);
        Session["HGDISTempTable"] = TempTables.Detail; //for HappyGo折抵時，查詢【促銷商品折抵活動】，以及【單商品折抵活動】使用。
        TempTables.Discount = Facade.getSale_Detail(POSUUID_MASTER, "5,11,12");
        TempTables.Paid = Facade.getPaid_Detail(POSUUID_MASTER);
        //TempTables.IMEI_LOG = Facade.getSale_IMEI_LOG("");
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
        Session["HGDISTempTable"] = TempTables.Detail; //for HappyGo折抵時，查詢【促銷商品折抵活動】，以及【單商品折抵活動】使用。
        TempTables.Discount = Facade.getSale_Detail(POSUUID_MASTER, "5,11,12");
        TempTables.Paid = Facade.getPaid_Detail(POSUUID_MASTER);
        //TempTables.IMEI_LOG = Facade.getSale_IMEI_LOG(POSUUID_MASTER);
        gvMaster.DataSource = TempTables.Detail.Select(" 1 = 1","SEQNO DESC").CopyToDataTable();
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
        //有促銷商品，組組合促銷商品清單，供組合促銷使用
        DataRow[] drsPromotion = TempTables.Detail.Select(" Promotion_Code is not null ", " Promotion_Code, SEQNO DESC");
        if (drsPromotion != null && drsPromotion.Length > 0)
        {
            string prePromotionCode = "";
            StringBuilder proProdList = new StringBuilder("");
            foreach (DataRow dr in drsPromotion)
            {
                if (StringUtil.CStr(dr["PROMOTION_CODE"]) != prePromotionCode)
                {
                    if (prePromotionCode == "")
                        proProdList.Append(StringUtil.CStr(dr["PROMOTION_CODE"])).Append("|").Append(StringUtil.CStr(dr["PRODNO"]));
                    else
                        proProdList.Append(";").Append(StringUtil.CStr(dr["PROMOTION_CODE"])).Append("|").Append(StringUtil.CStr(dr["PRODNO"]));
                    prePromotionCode = StringUtil.CStr(dr["PROMOTION_CODE"]);
                }
                else
                    proProdList.Append("^").Append(StringUtil.CStr(dr["PRODNO"]));
            }
            hidPromotionProdList.Value = StringUtil.CStr(proProdList);
        }
        if (TempTables.Discount != null && TempTables.Discount.Rows.Count > 0)
            gvDetail.Visible = true;
    }

    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack && !IsCallback)
        {
            if (_SRC_TYPE != "SAL02")
            {
                //Check Invoice Setting
                DataTable dtChkInvoice = Facade.chkINVSetting(logMsg.STORENO);
                if (dtChkInvoice == null || dtChkInvoice.Rows.Count <= 0)
                {
                    ScriptManager.RegisterClientScriptBlock(this, typeof(string), "INVSettingError", "alert('門市未設定電子發票，請與總部聯絡!');window.location='" + Request.ApplicationPath + "/content.htm';", true);
                    return;
                }
            }

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

            this.hidStore_No.Value = logMsg.STORENO;

            //Get ETC Product No.
            this.hidETCProdNo.Value = Facade.getFETCProuductNo();
            //Get ETC Card Product No.
            this.hidETCCardProdNo.Value = Facade.getETCCardProuductNo();
            //Get ETC Card Promise Amount Product No.
            this.hidETCCardPromiseProdNo.Value = Facade.getETCCardPromiseProuductNo();
            //已付款但未完成結帳,將重新載入最後一次未正常結帳資料
            DataTable CacheDT = Facade.CheckSaleCacheData(logMsg.STORENO, logMsg.OPERATOR);
            if (CacheDT.Rows.Count == 0)
            {
                switch (_SRC_TYPE)
                {
                    case "SAL05": LoadSAL05Data(); break;//取得未結清單來的資料
                    case "SAL02": LoadSAL02Data(); break;//
                    default: LoadSAL01Data(); break;
                }
            }
            else //已付款但未正常結帳資料
            {
                LoadFromCache(StringUtil.CStr(CacheDT.Rows[0]["POSUUID_MASTER"]));
                this.hdHG_REDEEM_POINT.Text = StringUtil.CStr(TempTables.Detail.Compute("Sum(HG_REDEEM_POINT)", "ITEM_TYPE IN ('4','14')"));  //總兌換點數
                this.hdTOTAL_AMOUNT.Text = StringUtil.CStr(TempTables.Detail.Compute("Sum(TOTAL_AMOUNT)", "ITEM_TYPE = '4'"));        //總兌點金額
                DataRow[] drCardNo = TempTables.Detail.Select("ITEM_TYPE IN ('4', '14')");
                this.hdHG_CARD_NO.Text = drCardNo.Length <= 0 ? "" : StringUtil.CStr(drCardNo[0]["HG_CARD_NO"]);               //HG卡號
                ScriptManager.RegisterClientScriptBlock(this, typeof(string), "LoadCache", "alert('系統偵測發現有已付款但未正常結帳資料,將載入未結帳資料!');", true);
                //將銷售單資料來源設為Cache
                Session["SALE_SRC_TYPE"] = "Cache";
            }
            PrintName.Value = System.Configuration.ConfigurationManager.AppSettings["Invoice_PDFPrinterName"].ToString();//web.config中設定
            //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "PrintInvoice", " Call_DetectPrinterName();", true);
            ScriptManager.RegisterStartupScript(this, this.GetType(), "PrintInvoice", " Call_DetectPrinterName();", true);
        }
    }

    protected void ASPxButton1_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/VSS/SAL/SAL02/SAL02.aspx");
    }

    protected void ASPxButton2_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/VSS/SAL/SAL05/SAL05.aspx");
    }

    protected void btnItemAdd_Click(object sender, EventArgs e)
    {
        TempTables.SaveTempTable(this);
        TempTables.NewRow_Detail(gvMaster);
        //新增單品時,SOURCE_TYPE設為 11 POS, 因為此為必要欄位

        Session["HGDISTempTable"] = TempTables.Detail; //for HappyGo折抵時，查詢【促銷商品折抵活動】，以及【單商品折抵活動】使用。
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
        TempTables.NewRecord_Detail_MixPromotion(this, gvMaster, gvDetail, args);
        TempTables.Mode_HavePROD(this);
    }

    protected void btnETCAdd_Click(object sender, EventArgs e)
    {
        TempTables.SaveTempTable(this);
        string[] args = __EVENTARGUMENT.Split(new char[] { '^' });
        TempTables.NewRow_Detail_ETCAdd(this, gvMaster, args);
        TempTables.Mode_HavePROD(this);
        hidETCProdNoExist.Value = "true";
    }

    protected void btnStoreDiscount_Click(object sender, EventArgs e)
    {
        TempTables.SaveTempTable(this);
        string[] args = __EVENTARGUMENT.Split(new char[] { ',' });
        TempTables.NewRow_Detail_StoreDiscount(this, gvMaster, args);
        Session["HGDISTempTable"] = TempTables.Detail; //for HappyGo折抵時，查詢【促銷商品折抵活動】，以及【單商品折抵活動】使用。
        TempTables.Mode_HavePROD(this);
    }

    protected void btnHappyGoNet_Click(object sender, EventArgs e)
    {
        TempTables.SaveTempTable(this);
        string[] args = __EVENTARGUMENT.Split(new char[] { '|' });
        foreach (string arg in args)
        {
            if (!string.IsNullOrEmpty(arg))
            {
                TempTables.NewRow_Detail_HappyGoNet(this, gvMaster, arg.Split(new char[] { ',' }));
            }
        }
        Session["HGDISTempTable"] = TempTables.Detail; //for HappyGo折抵時，查詢【促銷商品折抵活動】，以及【單商品折抵活動】使用。
        TempTables.Mode_HavePROD(this);

        this.hdHG_REDEEM_POINT.Text = StringUtil.CStr(TempTables.Detail.Compute("Sum(HG_REDEEM_POINT)", "ITEM_TYPE IN ('4','14')"));  //總兌換點數
        this.hdTOTAL_AMOUNT.Text = StringUtil.CStr(TempTables.Detail.Compute("Sum(TOTAL_AMOUNT)", "ITEM_TYPE = '4'"));        //總兌點金額
    }

    protected void btnAddProd_Click(object sender, EventArgs e)
    {
        TempTables.SaveTempTable(this);
        string[] args = __EVENTARGUMENT.Split(new char[] { '|' });
        foreach (string arg in args)
        {
            if (!string.IsNullOrEmpty(arg))
            {
                TempTables.NewRow_Detail_AddProd(this, gvMaster, arg.Split(new char[] { ',' }));
            }
        }
        TempTables.Mode_HavePROD(this);
    }

    protected void btnCreditCommunication_Click(object sender, EventArgs e)
    {
        TempTables.SaveTempTable(this);
        string[] args = __EVENTARGUMENT.Split(new char[] { '|' });
        TempTables.NewRow_Detail_CreditCommunication(this, gvMaster, args);
        TempTables.Mode_HavePROD(this);
    }

    protected void btnETCCard_Click(object sender, EventArgs e)
    {
        TempTables.SaveTempTable(this);
        string[] args = __EVENTARGUMENT.Split(new char[] { '|' });
        TempTables.NewRow_Detail_ETCCard(this, gvMaster, args);
        TempTables.Mode_HavePROD(this);
    }

    protected void btnOrderCheckOut_Click(object sender, EventArgs e)
    {
        OracleConnection objConn = OracleDBUtil.GetConnection();
        OracleTransaction objTX = objConn.BeginTransaction();
        bool bCheckOut = true; //是否可結帳
        DataRow[] Sale_DetailRows = null;
        DataRow[] Sale_DiscountRows = null;
        string processFlag = "";
        try
        {
            btnOrderCheckOut.Enabled = false;
            //處理ETC加值回傳資訊
            processFlag = "Update ETCAdd";
            if (hidETCProdNoExist.Value == "true")
            {
                DataRow[] drs = TempTables.Detail.Select(" PRODNO = '" + hidETCProdNo.Value + "'");
                if (drs != null && drs.Length > 0)
                {
                    string[] args = hidETCLoadInfo.Value.Split(new char[] { ',' });
                    drs[0]["ETC_AFTER_CHARGE"] = args[4];
                    drs[0]["ETC_SERNO"] = args[2];
                    int ETCAddAmt = 0;
                    if (hidETCAMT.Value != "" && NumberUtil.IsNumeric(hidETCAMT.Value))
                        ETCAddAmt = int.Parse(hidETCAMT.Value);
                    if (args[4] != null && args[4] != "" && NumberUtil.IsNumeric(args[4]))
                        drs[0]["ETC_BEFORE_CHARGE"] = int.Parse(args[4]) - ETCAddAmt;
                    int ret = Facade.updateETCAddInfo(drs[0]);
                    if (ret < 0)
                    {
                        ScriptManager.RegisterClientScriptBlock(this, typeof(string), "ERROR_MESSAGE", "alert('紀錄ETC加值資訊失敗');", true);
                        return;
                    }
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this, typeof(string), "ERROR_MESSAGE", "alert('找不到ETC加值商品');", true);
                    return;
                }
            }

            //處理找零
            processFlag = "Insert change";
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

            processFlag = "Save temp talbe";
            switch (_SRC_TYPE)
            {
                case "SAL05"://未結清單
                    TempTables.Save_Sale_Data2DBFromSAL05(this, objTX);
                    break;
                case "Cache"://未正常關閉且已輸入付款資料
                    TempTables.Save_Sale_Data2DBFromCache(this, objTX);
                    break;
                default://銷貨單直接新增
                    TempTables.Save_Sale_Data2DBFromSAL01(this, objTX);
                    break;
            }

            processFlag = "Discount";
            //若有折扣資料 則更新累計折抵次數
            if (TempTables.Discount.Rows.Count > 0)
            {
                Sale_DiscountRows = TempTables.Discount.Select("ITEM_TYPE IN ('5','11','12')");
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

            processFlag = "Happy Go Discount";
            //若有選取 HappyGo折扣 則需進行HappyGo刷卡折抵點數的動作，並且Update兌點項目累計折抵次數
            Sale_DetailRows = TempTables.Detail.Select("ITEM_TYPE = '4'");
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

            processFlag = "Store special Discount";
            //若有特殊抱怨折扣，回寫特殊抱怨已折扣金額
            DataRow[] Sale_DetailRows2 = TempTables.Detail.Select("ITEM_TYPE = '6'");
            if (bCheckOut && Sale_DetailRows2.Length > 0)
            {
                string USED_AMOUNT = "0";
                USED_AMOUNT = StringUtil.CStr(Sale_DetailRows2[0]["TOTAL_AMOUNT"]);//取得特殊抱怨折扣金額
                string useRoleType = "2";
                if (logMsg.ROLE_TYPE == "1")
                    useRoleType = "1";
                else if (StringUtil.CStr(Sale_DetailRows2[0]["SH_DISCOUNT_DESC"]).IndexOf("使用店長權限") >= 0)
                    useRoleType = "1";
                if (Facade.chkStoreSpecDisAmt(logMsg.STORENO, useRoleType, objTX))
                {
                    //回寫特殊抱怨已折扣金額
                    int AffectRow = 0;
                    string strYYMM = OracleDBUtil.WorkDay(logMsg.STORENO); //營業日
                    if (StringUtil.CStr(logMsg.ROLE_TYPE) == "1" || StringUtil.CStr(logMsg.ROLE_TYPE) == "2")   //當角色為1:店長或2:店員
                    {
                        AffectRow = Facade.UpdateStoreSpecialDIS(objTX, logMsg.STORENO, strYYMM.Substring(0, 7), USED_AMOUNT, useRoleType);
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
                    if (useRoleType == "1")
                        ScriptManager.RegisterClientScriptBlock(this, typeof(string), "ERROR_MESSAGE", "alert('門市特殊折扣店長額度已達上限');", true);
                    else
                        ScriptManager.RegisterClientScriptBlock(this, typeof(string), "ERROR_MESSAGE", "alert('門市特殊折扣店員額度已達上限');", true);
                    objTX.Rollback();
                }
            }

            processFlag = "Maintain Inventory";
            //銷售扣庫存
            INVENTORY_Facade Inventory = new INVENTORY_Facade();
            DataRow[] Sale_DetailRows5 = TempTables.Detail.Select("ITEM_TYPE IN ('1','2','3','7','8','9','10','13','14')");
            if (bCheckOut && Sale_DetailRows5.Length > 0)
            {
                string STOCK = this._STOCK;
                foreach (DataRow dr in Sale_DetailRows5)
                {
                    string Code = "";
                    string Message = "";

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
                                if (Sale_DetailRows != null && Sale_DetailRows.Length > 0)
                                    ScriptManager.RegisterClientScriptBlock(this, typeof(string), "ERROR_MESSAGE", "alert('銷售商品扣庫存失敗，請做銷售扣庫存動作!!請記得進行HappyGo卡退點動作');", true);
                                else
                                    ScriptManager.RegisterClientScriptBlock(this, typeof(string), "ERROR_MESSAGE", "alert('銷售商品扣庫存失敗，請做銷售扣庫存動作!!');", true);
                                objTX.Rollback();
                            }
                        }
                        catch (Exception)
                        {
                            bCheckOut = false;
                            ScriptManager.RegisterClientScriptBlock(this, typeof(string), "ERROR_MESSAGE", "alert('銷售商品扣庫存失敗，請做銷售扣庫存動作!!');", true);
                            objTX.Rollback();
                        }
                    }
                }
            }

            processFlag = "IMIE log";
            //IMEI_Log
            if (bCheckOut)
            {
                string strMessage = new SAL01_Facade().IMEISale_Log(objTX, StringUtil.CStr(TempTables.Head.Rows[0]["POSUUID_MASTER"]), logMsg.MODI_USER);
                string[] strMsg = strMessage.Split('|');
                if (strMsg[0] != "000") //表示失敗
                {
                    bCheckOut = false;
                    if (Sale_DetailRows != null && Sale_DetailRows.Length > 0)
                        ScriptManager.RegisterClientScriptBlock(this, typeof(string), "ERROR_MESSAGE", @"alert('IMEI_LOG失敗, " + strMsg[1].Replace("'", "-").Replace("\""," ") + ",請記得進行HappyGo卡退點動作');", true);
                    else
                        ScriptManager.RegisterClientScriptBlock(this, typeof(string), "ERROR_MESSAGE", @"alert('IMEI_LOG失敗, " + strMsg[1].Replace("'", "-").Replace("\"", " ") + "');", true);
                    objTX.Rollback();
                }
            }

            processFlag = "Run GL";
            //GL 分錄
            //if (bCheckOut)
            //{
            //    string strMessage = new SAL01_Facade().runGL(objTX, StringUtil.CStr(TempTables.Head.Rows[0]["POSUUID_MASTER"]));
            //    string[] strMsg = strMessage.Split('|');
            //    if (strMsg[0] == "999") //表示失敗
            //    {
            //        bCheckOut = false;
            //        if (Sale_DetailRows != null && Sale_DetailRows.Length > 0)
            //            ScriptManager.RegisterClientScriptBlock(this, typeof(string), "ERROR_MESSAGE", @"alert('GL 分錄失敗, " + strMsg[1].Replace("'", "-").Replace("\"", " ") + ",請記得進行HappyGo卡退點動作');", true);
            //        else
            //            ScriptManager.RegisterClientScriptBlock(this, typeof(string), "ERROR_MESSAGE", @"alert('GL 分錄失敗, " + strMsg[1].Replace("'", "-").Replace("\"", " ") + "');", true);
            //        objTX.Rollback();
            //    }
            //}

            if (bCheckOut)  //所有動作都有正常執行才可commit
            {
                SAL01_Facade SAL01Facade = new SAL01_Facade();
                SAL01Facade.UpdateSaleHead(objTX, StringUtil.CStr(TempTables.Head.Rows[0]["SALE_NO"]),
                    StringUtil.CStr(TempTables.Head.Rows[0]["SALE_STATUS"]), StringUtil.CStr(TempTables.Head.Rows[0]["POSUUID_MASTER"]), logMsg.MODI_USER);
                objTX.Commit();
                //ScriptManager.RegisterClientScriptBlock(this, typeof(string), "CheckOut", "alert('結帳完成!');", true);
            }

        }
        catch (Exception ex)
        {
            if (Sale_DetailRows != null && Sale_DetailRows.Length > 0)
                ScriptManager.RegisterClientScriptBlock(this, typeof(string), "ERROR_MESSAGE", "alert('Error step:["+ processFlag +"], message:" + ex.Message.Replace("'","-").Replace("\"", " ") + "，請記得進行HappyGo卡退點動作');", true);
            else
                ScriptManager.RegisterClientScriptBlock(this, typeof(string), "ERROR_MESSAGE", "alert('Error step:[" + processFlag + "], message:" + ex.Message.Replace("'", "-").Replace("\"", " ") + "');", true);
            bCheckOut = false;
            objTX.Rollback();
            //throw;
        }
        finally
        {
            if (!bCheckOut)
            {   //結帳失敗,恢復交易狀態為未結帳
                new SAL01_Facade().UpdateSaleHead("", "1", StringUtil.CStr(this.TempTables.Head.Rows[0]["POSUUID_MASTER"]));
            }
        }

        bool printInvoice = false;
        try
        {
            if (bCheckOut)
            {
                if (StringUtil.CStr(TempTables.Head.Rows[0]["SALE_TYPE"]) == "2")    //【帳單代收】要分配付款明細到帳單明細中
                {
                    Facade.collection(StringUtil.CStr(this.TempTables.Head.Rows[0]["POSUUID_MASTER"]));
                }

                SAL01_Facade SAL01Facade = new SAL01_Facade();
                processFlag = "Print invoice";
                int saleTotalAmt = 0;
                if (TempTables.Head.Rows[0]["SALE_TOTAL_AMOUNT"] != null && StringUtil.CStr(TempTables.Head.Rows[0]["SALE_TOTAL_AMOUNT"]) != ""
                    && NumberUtil.IsNumeric(StringUtil.CStr(TempTables.Head.Rows[0]["SALE_TOTAL_AMOUNT"])))
                    saleTotalAmt = int.Parse(StringUtil.CStr(TempTables.Head.Rows[0]["SALE_TOTAL_AMOUNT"]));
                DataRow[] drs = TempTables.Detail.Select(" INV_TYPE = '1' ");
                if (saleTotalAmt > 0 && drs != null && drs.Length > 0)
                {
                    //列印發票
                    Receipt myReceipt = new Receipt();
                    string filePath = SAL01Facade.getUploadPath(StringUtil.CStr(this.TempTables.Head.Rows[0]["POSUUID_MASTER"]));
                    if (!string.IsNullOrEmpty(filePath))
                    {
                        string fileName = myReceipt.generateReceiptfortest(StringUtil.CStr(this.TempTables.Head.Rows[0]["POSUUID_MASTER"]),PrintName.Value);
                        if (fileName == null || string.IsNullOrEmpty(fileName))
                        {
                            ScriptManager.RegisterClientScriptBlock(this, typeof(string), "PrintInvoiceError", "alert('結帳完成!');alert('列印發票失敗，請重印發票!!');", true);
                        }
                        else
                        {
                            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "PrintInvoice", "alert('結帳完成!');document.getElementById('" +
                                                                        fDownload.ClientID + "').src='" + Request.ApplicationPath + filePath + "/" + fileName + "';", true);
                            printInvoice = true;
                        }
                    }
                    else
                    {
                        ScriptManager.RegisterClientScriptBlock(this, typeof(string), "PrintInvoiceError", "alert('結帳完成!');alert('列印發票失敗，請重印發票!!');", true);
                    }
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this, typeof(string), "PrintInvoiceError", "alert('結帳完成!');alert('列印收據!!');", true);
                }
                processFlag = "Delete To Close";
                //刪除未結清單
                SAL01Facade.delTO_CLOSE(StringUtil.CStr(TempTables.Head.Rows[0]["POSUUID_MASTER"]));
                TempTables.Mode_CheckOut(this);
                //取得外部系統SALE_DETAIL
                DataTable dtDetail = SAL01Facade.getSale_Detail(StringUtil.CStr(this.TempTables.Head.Rows[0]["POSUUID_MASTER"]), "2");
                if (dtDetail != null && dtDetail.Rows.Count > 0)
                {
                    string prePosuuidDetail = "";
                    string posuuidDetail = "";
                    foreach (DataRow dr in dtDetail.Rows)
                    {
                        string sysID = "";
                        if (dr["POSUUID_DETAIL"] != null && (!string.IsNullOrEmpty(StringUtil.CStr(dr["POSUUID_DETAIL"]))))
                            posuuidDetail = StringUtil.CStr(dr["POSUUID_DETAIL"]);

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

                        if (sysID != "" && posuuidDetail != "" && posuuidDetail != prePosuuidDetail)
                        {
                            prePosuuidDetail = posuuidDetail;
                            if (dr["SERVICE_SYS_ID"] != null && (!string.IsNullOrEmpty(StringUtil.CStr(dr["SERVICE_SYS_ID"])))
                                && dr["POSUUID_DETAIL"] != null && (!string.IsNullOrEmpty(StringUtil.CStr(dr["POSUUID_DETAIL"]))))
                                SAL01Facade.CommitOuterSystem(sysID, StringUtil.CStr(dr["SERVICE_SYS_ID"]),
                                                                StringUtil.CStr(this.TempTables.Head.Rows[0]["POSUUID_MASTER"]), posuuidDetail,
                                                                StringUtil.CStr(dr["BARCODE1"]), logMsg.OPERATOR, logMsg.STORENO,StringUtil.CStr(dr["BUNDLE_ID"]),StringUtil.CStr(dr["BARCODE2"]),StringUtil.CStr(dr["BARCODE3"]));
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            if (!printInvoice)
                ScriptManager.RegisterClientScriptBlock(this, typeof(string), "PrintInvoiceError", "alert('結帳完成!');alert('Error step:[" + processFlag + "], 列印單據失敗，請重印[" + ex.Message.Replace("'", "-").Replace("\"", " ") + "]!!');", true);
        }
        finally
        {
            if (bCheckOut)
                LoadSAL01Data();
            else
                btnOrderCheckOut.Enabled = true;
        }
    }

    protected void btnReprint_Click(object sender, EventArgs e)
    {
        //重印發票
        try
        {
            int saleTotalAmt = 0;
            if (TempTables.Head.Rows[0]["SALE_TOTAL_AMOUNT"] != null && StringUtil.CStr(TempTables.Head.Rows[0]["SALE_TOTAL_AMOUNT"]) != ""
                && NumberUtil.IsNumeric(StringUtil.CStr(TempTables.Head.Rows[0]["SALE_TOTAL_AMOUNT"])))
                saleTotalAmt = int.Parse(StringUtil.CStr(TempTables.Head.Rows[0]["SALE_TOTAL_AMOUNT"]));
            DataRow[] drs = TempTables.Detail.Select(" INV_TYPE = '1' ");
            if (saleTotalAmt > 0 && drs != null && drs.Length > 0)
            {
                //列印發票
                Receipt myReceipt = new Receipt();
                string filePath = Facade.getUploadPath(StringUtil.CStr(this.TempTables.Head.Rows[0]["POSUUID_MASTER"]));
                if (!string.IsNullOrEmpty(filePath))
                {
                    string fileName = myReceipt.generateReceiptretest(StringUtil.CStr(this.TempTables.Head.Rows[0]["POSUUID_MASTER"]), "Reprint",PrintName.Value);
                    if (fileName == null || string.IsNullOrEmpty(fileName))
                    {
                        ScriptManager.RegisterClientScriptBlock(this, typeof(string), "PrintInvoiceError", "alert('重印發票失敗，無法取得電子發票檔名!!');", true);
                    }
                    else
                    {
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "PrintInvoice", "alert('重印發票完成!');document.getElementById('" +
                                                                    fDownload.ClientID + "').src='" + Request.ApplicationPath + filePath + "/" + fileName + "';", true);
                    }
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this, typeof(string), "PrintInvoiceError", "alert('重印發票失敗，請先在系統參數資料表中，設定電子發票檔路徑參數!!');", true);
                }
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this, typeof(string), "PrintInvoiceError", "alert('重印收據!!');", true);
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(this, typeof(string), "PrintInvoiceError", "alert('重印單據失敗，" + ex.Message.Replace("'", "-").Replace("\"", " ") + "!!');", true);
        }
    }

    protected void btnOrderCancel_Click(object sender, EventArgs e)
    {
        bool OrderCancel = true;
        string processFlag = "";
        try
        {
            if (TempTables.Head.Rows[0]["POSUUID_MASTER"] != null &&
                (!string.IsNullOrEmpty(StringUtil.CStr(TempTables.Head.Rows[0]["POSUUID_MASTER"]))))
            {   //有交易表頭檔UID,刪除資料庫中交易資料
                Facade.invalidSaleIMEILog(StringUtil.CStr(TempTables.Head.Rows[0]["POSUUID_MASTER"]));
                int ret = Facade.delSaleData(StringUtil.CStr(TempTables.Head.Rows[0]["POSUUID_MASTER"]));
                if (ret < 0)
                    OrderCancel = false;
                
                if (OrderCancel)
                {
                    DataRow[] drs = TempTables.Detail.Select(" ITEM_TYPE = '2' ");
                    if (drs != null && drs.Length > 0)
                    {
                        foreach (DataRow dr in drs)
                        {
                            //將TO_CLOSE_HEAD 更新為 取消中狀態, 並回填關連的POSUUID_MASTER
                            Facade.UpdateUnCloseHead(StringUtil.CStr(dr["POSUUID_DETAIL"]), StringUtil.CStr(TempTables.Head.Rows[0]["POSUUID_MASTER"]), "3", logMsg.MODI_USER);
                        }

                        string prePosuuidDetail = "";
                        string posuuitDetail = "";
                        DataTable dt = Facade.getCancleTO_CLOSE_DATA(StringUtil.CStr(TempTables.Head.Rows[0]["POSUUID_MASTER"]));
                        foreach (DataRow dr in dt.Rows)
                        {
                            if (dr["POSUUID_DETAIL"] != null && (!string.IsNullOrEmpty(StringUtil.CStr(dr["POSUUID_DETAIL"]))))
                                posuuitDetail = StringUtil.CStr(dr["POSUUID_DETAIL"]);

                            if (posuuitDetail != "" && posuuitDetail != prePosuuidDetail)
                            {
                                prePosuuidDetail = posuuitDetail;
                                ret = Facade.CancelOuterSystem(StringUtil.CStr(dr["POSUUID_DETAIL"]), StringUtil.CStr(dr["SERVICE_TYPE"]),
                                                                StringUtil.CStr(dr["SERVICE_SYS_ID"]), StringUtil.CStr(dr["BUNDLE_ID"]),
                                                                StringUtil.CStr(dr["STORE_NO"]), StringUtil.CStr(dr["SALE_PERSON"]),
                                                                StringUtil.CStr(dr["BARCODE1"]), StringUtil.CStr(dr["BARCODE2"]),
                                                                StringUtil.CStr(dr["BARCODE3"]), StringUtil.CStr(dr["AMOUNT"]));
                                if (ret == 0)
                                {
                                    //取消交易,commit外部系統成功才刪除未結清單中資料
                                    StringBuilder posuuid_detailList = new StringBuilder("");
                                    posuuid_detailList.Append(OracleDBUtil.SqlStr(StringUtil.CStr(dr["POSUUID_DETAIL"]))).Append(",");
                                    processFlag = "Delete To Close Data";
                                    Facade.delTO_CLOSE(posuuid_detailList);
                                }
                                else
                                {
                                    processFlag = "Insert Data Upload Log";
                                    OrderCancel = false;
                                    Facade.InsertDataUploadLog(StringUtil.CStr(dr["POSUUID_DETAIL"]));
                                }
                            }
                        }
                    }
                }
            }
            if (OrderCancel)
            {
                processFlag = "Get Sale No";
                string SALE_NO = StringUtil.CStr(TempTables.Head.Rows[0]["SALE_NO"]);
                if (SALE_NO == "")
                    SALE_NO = Store_SerialNo.GenNo("SALE", logMsg.STORENO, logMsg.MACHINE_ID);
                ScriptManager.RegisterClientScriptBlock(this, typeof(string), "OrderCancel", "alert('您已取消[" + SALE_NO + "]交易，請同步取消或更正該門號於業務園地的狀態!');", true);
                processFlag = "Load SAL01 Data";
                LoadSAL01Data();
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this, typeof(string), "OrderCancelError", "alert('交易取消失敗!');", true);
            }
        }
        catch (Exception ex)
        {
            if (OrderCancel)
            {
                ScriptManager.RegisterClientScriptBlock(this, typeof(string), "OrderCancel", "alert('交易取消成功!但後續動作[" + processFlag + "]失敗');", true);
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this, typeof(string), "OrderCancelError", "alert('交易取消失敗，錯誤訊息[" + ex.Message.Replace("'", "-").Replace("\"", " ") + "]!');", true);
            }
        }
    }

    protected void btnItemDel_Click(object sender, EventArgs e)
    {
        if (TempTables.Delete_Detail_Temp(this, gvMaster) == 0)
        {
            ScriptManager.RegisterClientScriptBlock(this, typeof(string), "NoSelect", "alert('請選取要刪除的資料!);", true);
        }
        else
        {
            if (TempTables.Detail.Rows.Count == 0) //全刪完了不能打付款資料           
                TempTables.Mode_NoPROD(this);
            else
                TempTables.Mode_HavePROD(this);
        }

        Session["HGDISTempTable"] = TempTables.Detail; //for HappyGo折抵時，查詢【促銷商品折抵活動】，以及【單商品折抵活動】使用。
    }

    protected void btnPayDEL_Click(object sender, EventArgs e)
    {
        TempTables.Delete_Paid_Temp(gvCheckOut);
        CheckPaidData();
    }

    protected void btnConfirm_Click(object sender, EventArgs e)
    {
        if (TempTables.Discount.Select("SOURCE_TYPE = '11'") == null || TempTables.Discount.Select("SOURCE_TYPE = '11'").Length == 0) //新增Discount
            TempTables.NewRow_Detail_ItemDiscount(this, gvDetail);
        TempTables.SaveTempTable(this);
        //交易確認時就不能再新增貨品資料了,將HEAD ,DETAIL DISCOUNT 和 PAID的資料存入CACHE
        TempTables.Head.Rows[0]["SALE_STATUS"] = "1";    //避免結帳失敗後，使用者取消付款明細，又按確定，而Session Table中的狀態停留在狀態2，已結帳
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

    protected void btnCredit_Click(object sender, EventArgs e)
    {
        string[] args = __EVENTARGUMENT.Split(new char[] { ',' });
        TempTables.NewRow_Paid_CreditCard(gvCheckOut, args, this);
        //付款一有資料時就不能再新增貨品資料了,並且要新增 DATA_CASH的資料來
        CheckPaidData();
        //信用卡交易刷完卡後不允許取消交易
        this.btnOrderCancel.Enabled = false;
    }

    protected void btnDivCredit_Click(object sender, EventArgs e)
    {
        string[] args = __EVENTARGUMENT.Split(new char[] { ',' });
        TempTables.NewRow_Paid_DivCredit(gvCheckOut, args, this);
        //付款一有資料時就不能再新增貨品資料了,並且要新增 DATA_CASH的資料來
        CheckPaidData();
        //信用卡交易刷完卡後不允許取消交易
        this.btnOrderCancel.Enabled = false;
    }

    protected void btnHappyGo_Click(object sender, EventArgs e)
    {
        string[] args = __EVENTARGUMENT.Split(new char[] { ',' });
        TempTables.NewRow_Paid_HappyGo(gvCheckOut, args);
        //付款一有資料時就不能再新增貨品資料了,並且要新增 DATA_CASH的資料來
        CheckPaidData();
        //Happy Go卡交易刷完卡後不允許取消交易
        this.btnOrderCancel.Enabled = false;
    }

    protected void btnOffLineCredit_Click(object sender, EventArgs e)
    {
        string[] args = __EVENTARGUMENT.Split(new char[] { ',' });
        TempTables.NewRow_Paid_OffLineCredit(gvCheckOut, args);
        //付款一有資料時就不能再新增貨品資料了,並且要新增 DATA_CASH的資料來
        CheckPaidData();
        //信用卡交易刷完卡後不允許取消交易
        this.btnOrderCancel.Enabled = false;
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
            PopupControl InputIMEIData = gvMaster.FindRowCellTemplateControl(e.VisibleIndex, (GridViewDataColumn)gvMaster.Columns["IMEI_QTY"], "InputIMEIData") as PopupControl; //e.Row.FindChildControl<PopupControl>("InputIMEIData").Visible = false;

            string STATUS = StringUtil.CStr(gvMaster.GetRowValues(e.VisibleIndex, "ITEM_TYPE"));
            if (STATUS == "5" || STATUS == "2" || STATUS == "11" || STATUS == "12") //2 未結明細,5 未結折扣,11 租賃折扣,12 舊機換新機折扣
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
            //else if (STATUS == "4" || STATUS == "6")  //4 HappyGo折扣, 6 店長折扣
            //{
            //    txtPRODNO.Enabled = false;
            //    txtQUANTITY.ReadOnly = true;
            //    ((ASPxImage)gvMaster.FindRowCellTemplateControl(e.VisibleIndex, (GridViewDataColumn)gvMaster.Columns["imgIMEI"], "imgIMEI")).Visible = false; //e.Row.FindChildControl<ASPxImage>("imgIMEI").Visible = false;
            //    ((ASPxTextBox)gvMaster.FindRowCellTemplateControl(e.VisibleIndex, (GridViewDataColumn)gvMaster.Columns["IMEI_QTY"], "lbIMEI_QTY")).Visible = false; //e.Row.FindChildControl<ASPxTextBox>("lbIMEI_QTY").Visible = false;
            //    InputIMEIData.Visible = false;
            //    if (STATUS == "6")
            //    {
            //        //已有店長折扣，則不能再選取
            //        gvMaster.FindChildControl<ASPxButton>("btnStoreDiscount").Enabled = false;
            //    }

            //    if (STATUS == "4")
            //    {
            //        //已有HappyGo折扣，則不能再選取
            //        gvMaster.FindChildControl<ASPxButton>("btnHappyGoNet").Enabled = false;
            //    }

            //}

            //若有選取 店長折扣或HappyGo折扣或普通商品加價購及贈品 則不能再變更原本的商品項目
            DataRow[] Sale_DetailRows = TempTables.Detail.Select("ITEM_TYPE IN('4','6','7','13')");
            if (Sale_DetailRows.Length > 0 && STATUS != "4" && STATUS != "6" && STATUS != "7" && STATUS != "13")
            {
                txtPRODNO.Enabled = false;
                txtQUANTITY.ReadOnly = true;
                InputIMEIData.Visible = false;
                e.Row.Attributes["canSelect"] = "false";
            }

            if (txtPRODNO.Text == this.hidETCProdNo.Value)
            {   //ETC加值不能再變更原本的商品項目
                txtPRODNO.Enabled = false;
                txtQUANTITY.ReadOnly = true;
                e.Row.Attributes["canSelect"] = "false";
            }
        }
    }

    protected void gvMaster_CommandButtonInitialize(object sender, ASPxGridViewCommandButtonEventArgs e)
    {
        string STATUS = StringUtil.CStr(gvMaster.GetRowValues(e.VisibleIndex, "ITEM_TYPE"));
        if (STATUS == "5" || STATUS == "2" || STATUS == "11" || STATUS == "12") //2 未結明細,5 未結折扣,11 租賃折扣,12 舊機換新機折扣
        {
            if (e.ButtonType == ColumnCommandButtonType.SelectCheckbox) { e.Enabled = false; }
        }

        if (TempTables.Detail != null)
        {
            DataRow[] Sale_DetailRows = TempTables.Detail.Select("ITEM_TYPE IN('4','6','7','13')"); //若有選取 店長折扣或HappyGo折扣或普通商品加價購及贈品 則不能再變更原本的商品項目
            if (Sale_DetailRows.Length > 0 && STATUS != "4" && STATUS != "6" && STATUS != "7" && STATUS != "8" && STATUS != "13")
            {
                e.Enabled = false;
            }
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
        TempTables.calTotalAmount(this);//計算總金額
        TempTables.Detail.AcceptChanges();
        e.Result = StringUtil.CStr(TempTables.STORE_REC_TOTAL_AMOUNT) + ";;";
    }
}
