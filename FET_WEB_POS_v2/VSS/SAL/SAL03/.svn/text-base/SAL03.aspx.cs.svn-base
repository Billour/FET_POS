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
using System.Collections;

public partial class VSS_SAL_SAL03 : BasePage
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
        public void SaveSale_Head_Temp(VSS_SAL_SAL03 p)
        {
            foreach (DataRow dr in Head.Rows)
            {
                dr["UNI_NO"] = p.txtUNI_NO.Text;
                dr["UNI_TITLE"] = p.txtUNI_TITLE.Text;
                dr["TRADE_DATE"] = p.lbTran_Date.Text;//交易日期
                dr["SALE_NO"] = p.lbSALE_NO.Text;//交易序號
                dr["HG_CARD_NO"] = p.lbHG_CARD_NO.Text; //Happy Go累點點數
                dr["HG_REMAIN_POINT"] = string.IsNullOrEmpty(p.lbHG_REMAIN_POINT.Text) ? "0" : p.lbHG_REMAIN_POINT.Text; //Happy Go剩餘點數
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
        public void SaleHeadDataBind(VSS_SAL_SAL03 p)
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
                Newdr["SALE_STATUS"] = "7";     //換貨未結
                Newdr["VOUCHER_TYPE"] = "1";
                Head.Rows.Add(Newdr);
                Head.AcceptChanges();
            }

            DataRow dr = Head.Rows[0];
            p.lbTran_Date.Text = Convert.ToDateTime(dr["TRADE_DATE"]).ToString("yyyy/MM/dd");
            p.lbSALE_NO.Text = StringUtil.CStr(dr["SALE_NO"]);
            p.txtUNI_NO.Text = StringUtil.CStr(dr["UNI_NO"]);
            p.txtUNI_TITLE.Text = StringUtil.CStr(dr["UNI_TITLE"]);
            p.lbINVOICE_NO.Text = StringUtil.CStr(dr["INVOICE_NO"]);
            p.lbVOUCHER_TYPE.Text = StringUtil.CStr(dr["INVOICE_TYPE"]);
            p.txtREMARK.Text = StringUtil.CStr(dr["REMARK"]);
            p.lbHG_CARD_NO.Text = StringUtil.CStr(dr["HG_CARD_NO"]);
            p.lbHG_REMAIN_POINT.Text = StringUtil.CStr(dr["HG_REMAIN_POINT"]); //Happy Go剩餘點數
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
        public void SaveSale_Detail_Temp(ASPxGridView gv)
        {
            if (Detail != null && Detail.Columns.Count > 0 && Detail.Columns.IndexOf("WRITE_OFF_DATE") < 0)
                Detail.Columns.Add("WRITE_OFF_DATE", Type.GetType("System.DateTime"));
            if (Detail != null && Detail.Columns.Count > 0 && Detail.Columns.IndexOf("APPROVAL_DATE") < 0)
                Detail.Columns.Add("APPROVAL_DATE", Type.GetType("System.DateTime"));
            if (Discount != null && Discount.Columns.Count > 0 && Discount.Columns.IndexOf("WRITE_OFF_DATE") < 0)
                Discount.Columns.Add("WRITE_OFF_DATE", Type.GetType("System.DateTime"));
            if (Discount != null && Discount.Columns.Count > 0 && Discount.Columns.IndexOf("APPROVAL_DATE") < 0)
                Discount.Columns.Add("APPROVAL_DATE", Type.GetType("System.DateTime"));
            //就算是新增單品,也要設定該品項的POSUUID_DETAIL,一張單為同一個ID
            string posuuid_detail = "";
            DataRow[] drs = null;
            if (Detail != null && Detail.Rows.Count > 0) 
                drs = Detail.Select("ITEM_TYPE IN ('1','3','4','6','7','8','9','10','12','13','14','15','16','17') And ITEM_STATUS <> '作廢' ");
            if (drs != null && drs.Length != 0 && drs[0]["POSUUID_DETAIL"] != null && StringUtil.CStr(drs[0]["POSUUID_DETAIL"]) != "")
            {
                posuuid_detail = StringUtil.CStr(drs[0]["POSUUID_DETAIL"]);
            }
            else if (drs == null || drs.Length == 0)
            {
                drs = Detail.Select("ITEM_TYPE = '2' AND SOURCE_TYPE = '11' And ITEM_STATUS <> '作廢' ");
                if (drs != null && drs.Length != 0 && drs[0]["POSUUID_DETAIL"] != null && StringUtil.CStr(drs[0]["POSUUID_DETAIL"]) != "")
                    posuuid_detail = StringUtil.CStr(drs[0]["POSUUID_DETAIL"]);
            }
            if (posuuid_detail == "")
                posuuid_detail = StringUtil.CStr(Advtek.Utility.GuidNo.getUUID());
            if (Detail != null && Detail.Rows.Count > 0)
            {
                for (int i = 0; i < Detail.Rows.Count; i++)
                {
                    PopupControl txtPRODNO = gv.FindRowCellTemplateControl(i, (GridViewDataColumn)gv.Columns["PRODNO"], "txtPRODNO") as PopupControl;
                    ASPxTextBox txtITEM_STATUS = gv.FindRowCellTemplateControl(i, (GridViewDataColumn)gv.Columns["ITEM_STATUS"], "txtITEM_STATUS") as ASPxTextBox;
                    ASPxTextBox txtPRODNAME = gv.FindRowCellTemplateControl(i, (GridViewDataColumn)gv.Columns["PRODNAME"], "txtPRODNAME") as ASPxTextBox;
                    ASPxTextBox txtQUANTITY = gv.FindRowCellTemplateControl(i, (GridViewDataColumn)gv.Columns["QUANTITY"], "txtQUANTITY") as ASPxTextBox;
                    ASPxTextBox txtUNIT_PRICE = gv.FindRowCellTemplateControl(i, (GridViewDataColumn)gv.Columns["UNIT_PRICE"], "txtUNIT_PRICE") as ASPxTextBox;//單價
                    ASPxTextBox txtTOTAL_AMOUNT = gv.FindRowCellTemplateControl(i, (GridViewDataColumn)gv.Columns["TOTAL_AMOUNT"], "txtTOTAL_AMOUNT") as ASPxTextBox;//總價
                    ASPxTextBox hidIS_OPEN_PRICE = gv.FindRowCellTemplateControl(i, (GridViewDataColumn)gv.Columns["UNIT_PRICE"], "hidIS_OPEN_PRICE") as ASPxTextBox;//是否自訂價格
                    ASPxTextBox hd_ID = gv.FindRowCellTemplateControl(i, (GridViewDataColumn)gv.Columns["IMEI_QTY"], "hd_ID") as ASPxTextBox;//是否自訂價格
                    PopupControl InputIMEIData = gv.FindRowCellTemplateControl(i, (GridViewDataColumn)gv.Columns["IMEI"], "InputIMEIData") as PopupControl;//總價
                    if (txtITEM_STATUS != null && txtITEM_STATUS.Text != "作廢" && txtPRODNO != null && hd_ID != null && hd_ID.Text != "")
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
                            if (StringUtil.CStr(dr["TAXABLE"]) == "")
                            {
                                DataTable PROD_DT = new SAL01_Facade().getProduct(txtPRODNO.Text);
                                if (PROD_DT.Rows.Count > 0) //取得稅額計算方式
                                {
                                    dr["TAXABLE"] = PROD_DT.Rows[0]["TAXABLE"];
                                    dr["TAXRATE"] = PROD_DT.Rows[0]["TAXRATE"];
                                }
                            }
                        }
                    }
                }
                Detail.AcceptChanges();
            }
            DataTable dtDetail = new DataTable();
            DataRow[] drsResort = null;
            if (Detail != null && Detail.Rows.Count > 0)
                drsResort = Detail.Select(" ITEM_STATUS = '作廢' ", "ITEMS");
            if (drsResort != null && drsResort.Length > 0)
                if (dtDetail != null && dtDetail.Rows.Count > 0)
                    dtDetail.Merge(drsResort.CopyToDataTable());
                else
                    dtDetail = drsResort.CopyToDataTable();
            if (Detail != null && Detail.Rows.Count > 0)
                drsResort = Detail.Select(" ITEM_STATUS <> '作廢' ", "ITEMS");
            if (drsResort != null && drsResort.Length > 0)
                if (dtDetail != null && dtDetail.Rows.Count > 0)
                    dtDetail.Merge(drsResort.CopyToDataTable());
                else
                    dtDetail = drsResort.CopyToDataTable();
                
            Detail = dtDetail;
            gv.DataSource = Detail;
            gv.DataBind();
        }
        public int Delete_Detail_Temp(VSS_SAL_SAL03 p, ASPxGridView gv)
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
                        p.Facade01.DeleteSale_IMEI_LOG(StringUtil.CStr(dr["ID"]));
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

            if (!string.IsNullOrEmpty(strID))
            {
                string where = "";
                where = strID;
                DataRow[] dra = Paid.Select("ID in(" + where + ") And ITEM_STATUS <> '作廢'");
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
        public void SaveTempTable(VSS_SAL_SAL03 p)
        {
            //把畫面資料存進Session
            SaveSale_Head_Temp(p);
            SaveSale_Detail_Temp(p.gvMaster);
            if (Discount != null && Discount.Rows.Count > 0)
            {
                DataRow[] drs = Discount.Select(" ITEM_STATUS <> '作廢' ");
                if (drs != null && drs.Length > 0)
                {
                    if (Head != null && Head.Rows.Count > 0 && Head.Rows[0]["POSUUID_MASTER"] != null &&
                        !string.IsNullOrEmpty(StringUtil.CStr(Head.Rows[0]["POSUUID_MASTER"])))
                    {
                        foreach (DataRow dr in drs)
                        {
                            dr["POSUUID_MASTER"] = StringUtil.CStr(Head.Rows[0]["POSUUID_MASTER"]);
                        }
                        Discount.AcceptChanges();
                    }
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
            dr["ITEM_STATUS"] = "";
            dr["POSUUID_MASTER"] = StringUtil.CStr(Head.Rows[0]["POSUUID_MASTER"]);
            dr["POSUUID_DETAIL"] = "";
            dr["ID"] = StringUtil.CStr(Advtek.Utility.GuidNo.getUUID());
            dr["IMEI_QTY"] = 0;
            dr["QUANTITY"] = 1;
            dr["ITEM_TYPE"] = "1";
            dr["ITEM_TYPE_NAME"] = "單";
            dr["ITEMS"] = Detail.Select("ITEM_STATUS <> '作廢'").Length +1;
            dr["SOURCE_TYPE"] = "11"; //新增單品時, SOURCE_TYPE需設為 11
            Detail.Rows.InsertAt(dr, Detail.Rows.Count);
            Detail.AcceptChanges();
            gv.DataSource = Detail;
            gv.DataBind();
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
        public void NewRow_Detail_HappyGoNet(VSS_SAL_SAL03 p, ASPxGridView gv, string[] args)
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
            dr["ITEMS"] = Detail.Rows.Count + 1;
            dr["HG_CARD_NO"] = p.hdHG_CARD_NO.Text = args[4];  //HG卡號
            dr["HG_REDEEM_POINT"] = args[5]; //兌換點數
            dr["MSISDN"] = args[6]; //門號
            dr["ITEM_STATUS"] = "";
            dr["SOURCE_TYPE"] = "11"; //新增單品時, SOURCE_TYPE需設為 11
            p.hdHG_LEFT_POINT.Text = args[7]; //剩餘點數
            p.hdHG_CARD_NO.Text = args[4];
            p.lbHG_CARD_NO.Value = args[4];
            p.lbHG_REMAIN_POINT.Value = args[7];

            Detail.Rows.InsertAt(dr, Detail.Rows.Count);
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
        public void NewRow_Detail_AddProd(VSS_SAL_SAL03 p, ASPxGridView gv, string[] args)
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
            dr["ITEM_STATUS"] = "";
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
            else if (args[6] == "15")
                dr["ITEM_TYPE_NAME"] = "折";
            else if (args[6] == "16")
                dr["ITEM_TYPE_NAME"] = "折";
            else if (args[6] == "7")
                dr["ITEM_TYPE_NAME"] = "加";
            dr["UNIT_PRICE"] = unitPrice;
            dr["TOTAL_AMOUNT"] = totalAmt;   //加價金額
            dr["ORI_UNIT_PRICE"] = oriPrice;
            dr["ITEMS"] = Detail.Rows.Count + 1;
            dr["SOURCE_TYPE"] = "11"; //新增單品時, SOURCE_TYPE需設為 11
            Detail.Rows.InsertAt(dr, Detail.Rows.Count);
            Detail.AcceptChanges();
            gv.DataSource = Detail;
            gv.DataBind();
            calTotalAmount(p);
        }

        /// <summary>
        /// 店長折扣
        /// </summary>
        /// <param name="gv"></param>
        /// <param name="args">
        /// [1] = 折扣料號
        /// [2] = 折扣名稱
        /// [3] = 店長折抵金額
        /// [4] = 店長折扣折抵比率
        /// [5] = 店長折扣折抵原因
        /// [6] = 店長折扣折抵原因內容 
        /// [7] = 門市特殊折扣使用角色代碼
        /// </param>
        public void NewRow_Detail_StoreDiscount(VSS_SAL_SAL03 p, ASPxGridView gv, string[] args)
        {
            SaveSale_Detail_Temp(gv);
            DataRow dr = Detail.NewRow();
            int unitPrice = 0;
            if (!string.IsNullOrEmpty(args[1]))
                if ((!string.IsNullOrEmpty(args[3])) && args[3] != "" && NumberUtil.IsNumeric(args[3]))
                    unitPrice = int.Parse(args[3]);
            dr["ITEM_STATUS"] = "";
            dr["POSUUID_MASTER"] = StringUtil.CStr(Head.Rows[0]["POSUUID_MASTER"]);
            dr["POSUUID_DETAIL"] = "";//未結清單,由此新增沒有
            dr["ID"] = StringUtil.CStr(Advtek.Utility.GuidNo.getUUID());
            dr["PRODNO"] = args[1];
            dr["PRODNAME"] = args[2];
            dr["IMEI_QTY"] = 0;
            dr["QUANTITY"] = 1;
            dr["UNIT_PRICE"] = -1 * unitPrice;
            dr["TOTAL_AMOUNT"] = -1 * unitPrice;
            dr["ITEM_TYPE"] = "6";
            dr["ITEM_TYPE_NAME"] = "折";
            dr["ITEMS"] = Detail.Select("ITEM_STATUS <> '作廢'").Length + 1;
            dr["SH_DISCOUNT_RATE"] = (string.IsNullOrEmpty(args[4]) ? "0" : args[4]); //店長折扣折抵比率
            dr["SH_DISCOUNT_REASON"] = (string.IsNullOrEmpty(args[5]) ? "0" : args[5]);  //店長折扣折抵原因
            if (args[7] == "1" && p.logMsg.ROLE_TYPE != "1")
                dr["SH_DISCOUNT_DESC"] = "[使用店長權限]" + (string.IsNullOrEmpty(args[6]) ? "" : args[6]); //店長折扣折抵原因內容
            else
                dr["SH_DISCOUNT_DESC"] = (string.IsNullOrEmpty(args[6]) ? "" : args[6]); //店長折扣折抵原因內容
            dr["SOURCE_TYPE"] = "11"; //新增單品時, SOURCE_TYPE需設為 11
            Detail.Rows.InsertAt(dr, Detail.Rows.Count);
            Detail.AcceptChanges();
            gv.DataSource = Detail;
            gv.DataBind();
            calTotalAmount(p);
        }

        #endregion

        /// <summary>
        /// 單品贈品
        /// </summary>
        /// <param name="gv"></param>
        public void NewRow_Detail_ItemGift(VSS_SAL_SAL03 p, ASPxGridView gv)
        {
            #region 處理Discount及贈品
            DataRow[] drs = Detail.Select(" ITEM_STATUS <> '作廢' AND SOURCE_TYPE = '11' AND ITEM_TYPE Not In ('6','12','15','16','17') ");
            if (drs != null && drs.Length > 0)
            {
                foreach (DataRow drDetail in drs)
                {
                    //產生贈品
                    #region CreateGift
                    if (drDetail["ITEM_TYPE"] != null && StringUtil.CStr(drDetail["ITEM_TYPE"]) == "1")
                    {
                        Hashtable table = Discount_Facade.get_gift_discount(StringUtil.CStr(drDetail["PRODNO"]), p.logMsg.STORENO, p.logMsg.MODI_USER, "");
                        if (table.Count > 0)
                        {
                            foreach (DictionaryEntry t in table)
                            {
                                string sale_detail_id = GuidNo.getUUID();
                                string discount_code = t.Key.ToString();
                                DataTable prodDt = t.Value as DataTable;
                                if (prodDt != null && prodDt.Rows.Count > 0)
                                {
                                    string gift_prodno = prodDt.Rows[0][0].ToString();

                                    //判斷庫存
                                    Product_Facade facade = new Product_Facade();

                                    string stock = Common_PageHelper.GetGoodLOCUUID();

                                    DataTable product = facade.Query_ProductInfo(gift_prodno, p.logMsg.STORENO, stock);

                                    if (product != null && product.Rows.Count > 0)
                                    {
                                        //庫存
                                        string ON_HAND_QTY = string.IsNullOrEmpty(StringUtil.CStr(product.Rows[0]["ON_HAND_QTY"])) ? "0" : StringUtil.CStr(product.Rows[0]["ON_HAND_QTY"]);

                                        if (ON_HAND_QTY != "0")
                                        {
                                            #region Insert 贈品
                                            DataRow drGift = Detail.NewRow();
                                            drGift["PRODNO"] = StringUtil.CStr(product.Rows[0]["prodno"]);
                                            drGift["QUANTITY"] = 1;
                                            drGift["UNIT_PRICE"] = Convert.ToInt32(product.Rows[0]["price"]);

                                            drGift["TOTAL_AMOUNT"] = Convert.ToInt32(product.Rows[0]["price"]);
                                            drGift["POSUUID_DETAIL"] = drDetail["POSUUID_DETAIL"];
                                            drGift["ITEM_TYPE"] = "13";

                                            drGift["ORI_UNIT_PRICE"] = StringUtil.CStr(product.Rows[0]["price"]);

                                            drGift["SOURCE_TYPE"] = "11";

                                            drGift["SEQNO"] = Detail.Select("ITEM_STATUS <> '作廢'").Length + 1;
                                            drGift["ID"] = sale_detail_id;
                                            drGift["ITEM_STATUS"] = "";
                                            drGift["POSUUID_MASTER"] = StringUtil.CStr(Head.Rows[0]["POSUUID_MASTER"]);
                                            drGift["IMEI_QTY"] = 0;
                                            drGift["ITEM_TYPE_NAME"] = "贈";
                                            drGift["ITEMS"] = Detail.Select("ITEM_STATUS <> '作廢'").Length + 1;
                                            Detail.Rows.InsertAt(drGift, Detail.Rows.Count);
                                            #endregion

                                            #region Insert 贈品折扣
                                            sale_detail_id = GuidNo.getUUID();
                                            DataRow drGiftDiscount = Detail.NewRow();
                                            drGiftDiscount["PRODNO"] = discount_code;
                                            drGiftDiscount["PRODNAME"] = Discount_Facade.get_discount_name(discount_code);
                                            drGiftDiscount["QUANTITY"] = 1;
                                            drGiftDiscount["UNIT_PRICE"] = Convert.ToInt32(product.Rows[0]["price"]) * -1;

                                            drGiftDiscount["TOTAL_AMOUNT"] = Convert.ToInt32(product.Rows[0]["price"]) * -1;
                                            drGiftDiscount["POSUUID_DETAIL"] = drDetail["POSUUID_DETAIL"];
                                            drGiftDiscount["ITEM_TYPE"] = "16";

                                            drGiftDiscount["ORI_UNIT_PRICE"] = StringUtil.CStr(product.Rows[0]["price"]);

                                            drGiftDiscount["SOURCE_TYPE"] = "11";

                                            drGiftDiscount["SEQNO"] = Detail.Select("ITEM_STATUS <> '作廢'").Length + 1;
                                            drGiftDiscount["ID"] = sale_detail_id;
                                            drGiftDiscount["ITEM_STATUS"] = "";
                                            drGiftDiscount["POSUUID_MASTER"] = StringUtil.CStr(Head.Rows[0]["POSUUID_MASTER"]);
                                            drGiftDiscount["IMEI_QTY"] = 0;
                                            drGiftDiscount["ITEM_TYPE_NAME"] = "折";
                                            drGiftDiscount["ITEMS"] = Detail.Select("ITEM_STATUS <> '作廢'").Length + 1;
                                            Detail.Rows.InsertAt(drGiftDiscount, Detail.Rows.Count);
                                            Detail.AcceptChanges();
                                            #endregion
                                        }
                                    } //end of if (product != null && product.Rows.Count > 0)
                                } // end of if (prodDt != null && prodDt.Rows.Count > 0)
                            } // end of foreach (DictionaryEntry t in table)
                        } // end of if (table.Count > 0)
                    }
                    #endregion
                }

                gv.DataSource = Detail;
                gv.DataBind();
                gv.Visible = true;
            #endregion 處理Discount及贈品
                calTotalAmount(p);
            }
        }

        #region 新增折扣
        /// <summary>
        /// 折扣
        /// </summary>
        /// <param name="gv"></param>
        public void NewRow_Detail_ItemDiscount(VSS_SAL_SAL03 p, ASPxGridView gv)
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
            DataRow[] drs = Detail.Select(" ITEM_STATUS <> '作廢' AND SOURCE_TYPE = '11' AND ITEM_TYPE Not In ('6','12','15','16','17') ");
            if (drs != null && drs.Length > 0)
            {
                foreach (DataRow drDetail in drs)
                {
                    sb.Append(StringUtil.CStr(drDetail["PRODNO"])).Append(",");
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

                DataTable dtDiscount = p.Facade01.getMixPromotion_ItemDiscount(p.logMsg.STORENO, p.logMsg.OPERATOR, StringUtil.CStr(p.hidPromotion_Code.Value),
                                                                             strMSIDSN, strRateAmt, strDATA, strVOICE, strTrans_type, strMNP, strBUNDLE_TYPE,
                                                                             strSOURCET_TYPE, prodStr);
                if (dtDiscount != null && dtDiscount.Rows.Count > 0)
                {
                    int curItemTolAmt = itemAmt;
                    foreach (DataRow drDis in dtDiscount.Rows)
                    {
                        DataRow dr = Discount.NewRow();
                        dr["ITEM_STATUS"] = "";
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
                        if (Discount != null && Discount.Rows.Count > 0 && Discount.Select(" ITEM_STATUS <> '作廢' ") != null && Discount.Select(" ITEM_STATUS <> '作廢' ").Length > 0)
                        {
                            dr["ITEMS"] = Discount.Select(" ITEM_STATUS <> '作廢' ").Length + 1;
                            dr["SEQNO"] = Discount.Select(" ITEM_STATUS <> '作廢' ").Length + 1;
                        }
                        else
                        {
                            dr["ITEMS"] = 1;
                            dr["SEQNO"] = 1;
                        }
                        dr["ITEM_STATUS"] = "";
                        if (Discount != null && Discount.Rows.Count > 0)
                            Discount.Rows.InsertAt(dr, Discount.Rows.Count);
                        else
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

        /// <summary>
        /// 折扣
        /// </summary>
        /// <param name="gv"></param>
        public void NewRow_Detail_MixPromotionDiscount(VSS_SAL_SAL03 p, ASPxGridView gv, string SOURCE_TYPE)
        {
            #region 處理 MixPromotion Discount及贈品
            StringBuilder sb = new StringBuilder("");
            string strMSIDSN = "";
            string strRateAmt = "";
            string strDATA = "";
            string strVOICE = "";
            string strTrans_type = "";
            string strMNP = "";
            string strBUNDLE_TYPE = "";
            string strSOURCET_TYPE = "";
            string strPromotionCode = "";

            int itemAmt = 0;
            DataRow[] drs = Detail.Select(" ITEM_STATUS <> '作廢' AND SOURCE_TYPE = " + OracleDBUtil.SqlStr(SOURCE_TYPE));
            if (drs != null && drs.Length > 0)
            {
                foreach (DataRow drDetail in drs)
                {
                    sb.Append(StringUtil.CStr(drDetail["PRODNO"])).Append(",");
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

                    if (drDetail["PROMOTION_CODE"] != null && StringUtil.CStr(drDetail["PROMOTION_CODE"]) != "")
                        strPromotionCode = StringUtil.CStr(drDetail["PROMOTION_CODE"]);
                }

                string prodStr = StringUtil.CStr(sb);
                prodStr = prodStr.Substring(0, prodStr.Length - 1);

                DataTable dtDiscount = p.Facade01.getMixPromotion_ItemDiscount(p.logMsg.STORENO, p.logMsg.OPERATOR, strPromotionCode,
                                                                             strMSIDSN, strRateAmt, strDATA, strVOICE, strTrans_type, strMNP, strBUNDLE_TYPE,
                                                                             strSOURCET_TYPE, prodStr);
                if (dtDiscount != null && dtDiscount.Rows.Count > 0)
                {
                    int curItemTolAmt = itemAmt;
                    foreach (DataRow drDis in dtDiscount.Rows)
                    {
                        DataRow dr = Discount.NewRow();
                        dr["ITEM_STATUS"] = "";
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
                        dr["SOURCE_TYPE"] = SOURCE_TYPE;
                        if (Discount != null && Discount.Rows.Count > 0 && Discount.Select(" ITEM_STATUS <> '作廢' ") != null && Discount.Select(" ITEM_STATUS <> '作廢' ").Length > 0)
                        {
                            dr["ITEMS"] = Discount.Select(" ITEM_STATUS <> '作廢' ").Length + 1;
                            dr["SEQNO"] = Discount.Select(" ITEM_STATUS <> '作廢' ").Length + 1;
                        }
                        else
                        {
                            dr["ITEMS"] = 1;
                            dr["SEQNO"] = 1;
                        }
                        dr["ITEM_STATUS"] = "";
                        dr["PROMOTION_CODE"] = strPromotionCode;
                        if (Discount != null && Discount.Rows.Count > 0)
                            Discount.Rows.InsertAt(dr, Discount.Rows.Count);
                        else
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
            dr["ITEM_STATUS"] = "";
            dr["POSUUID_MASTER"] = Head.Rows[0]["POSUUID_MASTER"];
            dr["ID"] = StringUtil.CStr(Advtek.Utility.GuidNo.getUUID());
            dr["PAID_AMOUNT"] = args[1];
            dr["DESCRIPTION"] = "現金";
            dr["PAID_MODE"] = 1;
            dr["PAID_MODE_NAME"] = "現金";
            Paid.Rows.InsertAt(dr, Paid.Rows.Count);
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
        public void NewRow_Paid_CreditCard(ASPxGridView gv, string[] args, VSS_SAL_SAL03 p)
        {
            SavePaid_Detail_Temp(gv);
            DataRow dr = Paid.NewRow();
            dr["ITEM_STATUS"] = "";
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
                dtCardType = p.Facade01.getCreditCardType(args[8]);
            } 
            else 
            {
                dtCardType = p.Facade01.getCreditCardType(CreditCard_Facade.CheckCardType(args[2]));
            }
            if (dtCardType != null && dtCardType.Rows.Count > 0 && dtCardType.Rows[0]["CREDIT_CARD_TYPE_ID"] != null)
            {
                dr["CREDIT_CARD_TYPE_ID"] = StringUtil.CStr(dtCardType.Rows[0]["CREDIT_CARD_TYPE_ID"]);
                DataTable dtRate = p.Facade01.getCreditDivRate(StringUtil.CStr(dtCardType.Rows[0]["CREDIT_CARD_TYPE_ID"]));
                if (dtRate != null && dtRate.Rows.Count > 0 && dtRate.Rows[0]["charge_rate"] != null &&
                    NumberUtil.IsNumeric(StringUtil.CStr(dtRate.Rows[0]["charge_rate"])))
                {
                    dr["CREDIT_CARD_CHARGE_RATE"] = double.Parse(StringUtil.CStr(dtRate.Rows[0]["charge_rate"]));
                    if (args[1] != null && args[1] != "" && NumberUtil.IsNumeric(args[1]))
                        dr["CREDIT_CARD_FEE"] = Math.Round(int.Parse(args[1]) * double.Parse(StringUtil.CStr(dtRate.Rows[0]["charge_rate"])) / 100, 2);
                }
            }
            Paid.Rows.InsertAt(dr, Paid.Rows.Count);
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
        public void NewRow_Paid_DivCredit(ASPxGridView gv, string[] args, VSS_SAL_SAL03 p)
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
            dr["ITEM_STATUS"] = "";
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
                dtCardType = p.Facade01.getCreditCardType(args[8]);
            } 
            else 
            {
                dtCardType = p.Facade01.getCreditCardType(CreditCard_Facade.CheckCardType(args[2]));
            }
            if (dtCardType != null && dtCardType.Rows.Count > 0 && dtCardType.Rows[0]["CREDIT_CARD_TYPE_ID"] != null)
            {
                dr["CREDIT_CARD_TYPE_ID"] = StringUtil.CStr(dtCardType.Rows[0]["CREDIT_CARD_TYPE_ID"]);
                DataTable dtInstallmentId = p.Facade01.getCreditCardInstallmentId(args[6], period);
                if (dtInstallmentId != null && dtInstallmentId.Rows.Count > 0)
                {
                    dr["INSTALLMENT_ID"] = StringUtil.CStr(dtInstallmentId.Rows[0]["INSTALLMENT_ID"]);
                    if (dr["INSTALLMENT_ID"] != null && StringUtil.CStr(dr["INSTALLMENT_ID"]) != "")
                    {
                        DataTable dtInterestRate = p.Facade01.getCreditCardInterestRate(StringUtil.CStr(dr["INSTALLMENT_ID"]));
                        if (dtInterestRate != null && dtInterestRate.Rows.Count > 0 && dtInterestRate.Rows[0]["seqment_rate"] != null
                            && NumberUtil.IsNumeric(StringUtil.CStr(dtInterestRate.Rows[0]["seqment_rate"])))
                        {
                            dr["INTEREST_RATE"] = double.Parse(StringUtil.CStr(dtInterestRate.Rows[0]["seqment_rate"]));
                        }

                        DataTable dtSettlementRate = p.Facade01.getCreditCardSettlementRate(StringUtil.CStr(dr["INSTALLMENT_ID"]));
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
            Paid.Rows.InsertAt(dr, Paid.Rows.Count);
            Paid.AcceptChanges();
            gv.DataSource = Paid;
            gv.DataBind();
        }

        /// <summary>
        /// 金融卡
        /// </summary>
        /// <param name="gv"></param>
        /// <param name="args">
        /// [0] = TYPE
        /// [1] = 金額
        /// [2] = 金融卡卡號
        /// [3] = 授權碼
        /// </param>
        public void NewRow_Paid_VisaDebit(ASPxGridView gv, string[] args)
        {
            SavePaid_Detail_Temp(gv);
            DataRow dr = Paid.NewRow();
            dr["ITEM_STATUS"] = "";
            dr["ID"] = StringUtil.CStr(Advtek.Utility.GuidNo.getUUID());
            dr["POSUUID_MASTER"] = Head.Rows[0]["POSUUID_MASTER"];
            dr["DESCRIPTION"] = "金融卡卡號:" + args[2] + ",授權碼:" + args[3];
            dr["PAID_MODE"] = 6;
            dr["PAID_MODE_NAME"] = "金融卡";
            dr["PAID_AMOUNT"] = args[1];  //金額
            Paid.Rows.InsertAt(dr, Paid.Rows.Count);
            Paid.AcceptChanges();
            gv.DataSource = Paid;
            gv.DataBind();
        }

        /// <summary>
        /// 組合促銷資料
        /// </summary>
        /// <param name="gv"></param>
        public void NewRecord_Detail_MixPromotion(VSS_SAL_SAL03 p, ASPxGridView gv, ASPxGridView gvDiscount, string[] args)
        {
            SaveSale_Detail_Temp(gv);
            int promotionAmt = 0;
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
                DataRow[] drs = Detail.Select(" ITEM_STATUS <> '作廢' AND PROMOTION_CODE = '" + p.hidPromotion_Code.Value + "' And POSUUID_DETAIL = '" + p.hidPosuuid_Detail.Value
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

                    for (int i = startInd; i < sameLength; i++)
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
                            promotionAmt += price * qty;
                            Detail.AcceptChanges();
                            if (dr["ID"] != null && !string.IsNullOrEmpty(StringUtil.CStr(dr["ID"])))
                            {
                                new IMEI_Facade().CleanINV_IMEI("SALE_IMEI_LOG", StringUtil.CStr(dr["ID"]));
                                dr["IMEI"] = "";
                            }
                            dr["ID"] = StringUtil.CStr(Advtek.Utility.GuidNo.getUUID());
                        }
                    }
                    #region 明細中組合促銷資料筆數小於回報筆數
                    if (args.Length - 2 > drs.Length - startInd)
                    {
                        for (int i = drs.Length - startInd + 2; i < args.Length; i++)
                        {
                            valArray = args[i].Split('^');
                            DataRow dr = Detail.NewRow();
                            dr["ITEM_STATUS"] = "";
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
                            dr["ITEMS"] = Detail.Select(" ITEM_STATUS <> '作廢' ").Length + 1;
                            if (p.hidSOURCE_TYPE.Value != null)
                                dr["SOURCE_TYPE"] = p.hidSOURCE_TYPE.Value;
                            else
                                dr["SOURCE_TYPE"] = "11"; //新增單品時, SOURCE_TYPE需設為 11
                            int price = 0;
                            if (valArray[2] != "" && NumberUtil.IsNumeric(valArray[2]))
                                price = int.Parse(valArray[2]);
                            promotionAmt += price;
                            Detail.Rows.InsertAt(dr, Detail.Rows.Count + 1);
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
                            dr["ITEMS"] = Detail.Select(" ITEM_STATUS <> '作廢' ").Length + 1;
                            if (p.hidSOURCE_TYPE.Value != null)
                                dr["SOURCE_TYPE"] = p.hidSOURCE_TYPE.Value;
                            int price = 0;
                            if (valArray[2] != "" && NumberUtil.IsNumeric(valArray[2]))
                                price = int.Parse(valArray[2]);
                            promotionAmt += price;
                            Detail.Rows.InsertAt(dr, Detail.Rows.Count + 1);
                            Detail.AcceptChanges();
                        }
                    }
                }
                gv.DataSource = Detail;
                gv.DataBind();
                #endregion 處理促銷商品資料
                #region 處理Discount及贈品
                if (Detail != null && Detail.Rows.Count > 0)
                    drs = Detail.Select(" ITEM_STATUS <> '作廢' AND PROMOTION_CODE = '" + p.hidPromotion_Code.Value + "' And POSUUID_DETAIL = '" + p.hidPosuuid_Detail.Value
                                            + "'", "ITEMS");
                DataRow[] drsDis = null;
                if (Discount != null && Discount.Rows.Count > 0)
                    drsDis = Discount.Select(" PROMOTION_CODE = '" + p.hidPromotion_Code.Value + "' And POSUUID_DETAIL = '" + p.hidPosuuid_Detail.Value
                                            + "'", "ITEMS");
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
                    sb.Append(StringUtil.CStr(drDetail["PRODNO"])).Append(",");
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

                DataTable dtDiscount = p.Facade01.getMixPromotion_ItemDiscount(p.logMsg.STORENO, p.logMsg.OPERATOR, StringUtil.CStr(p.hidPromotion_Code.Value),
                                                                             strMSIDSN, strRateAmt, strDATA, strVOICE, strTrans_type, strMNP, strBUNDLE_TYPE, 
                                                                             strSOURCET_TYPE, prodStr);
                if (dtDiscount != null && dtDiscount.Rows.Count > 0)
                {
                    foreach (DataRow drDis in dtDiscount.Rows)
                    {
                        DataRow dr = Discount.NewRow();
                        dr["ITEM_STATUS"] = "";
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
                        dr["ORI_UNIT_PRICE"] = price;
                        if (promotionAmt + price >= 0)
                        {
                            promotionAmt += price;
                        }
                        else
                        {
                            price = -1 * promotionAmt;
                            promotionAmt = 0;
                        }
                        dr["UNIT_PRICE"] = price;
                        dr["TOTAL_AMOUNT"] = price;
                        dr["ITEM_TYPE"] = "5";
                        dr["ITEM_TYPE_NAME"] = "促";
                        dr["PROMOTION_CODE"] = p.hidPromotion_Code.Value;
                        dr["PROMO_NAME"] = p.hidPromotion_Name.Value;
                        if (Discount != null && Discount.Rows.Count > 0 && Discount.Select(" ITEM_STATUS <> '作廢' ") != null && Discount.Select(" ITEM_STATUS <> '作廢' ").Length > 0)
                        {
                            dr["ITEMS"] = Discount.Select(" ITEM_STATUS <> '作廢' ").Length + 1;
                            dr["SEQNO"] = Discount.Select(" ITEM_STATUS <> '作廢' ").Length + 1;
                        }
                        else
                        {
                            dr["ITEMS"] = 1;
                            dr["SEQNO"] = 1;
                        }
                        if (p.hidSOURCE_TYPE.Value != null)
                            dr["SOURCE_TYPE"] = p.hidSOURCE_TYPE.Value;
                        else
                            dr["SOURCE_TYPE"] = "11";
                        if (Discount != null && Discount.Rows.Count > 0)
                            Discount.Rows.InsertAt(dr, Discount.Rows.Count);
                        else 
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
            dr["ITEM_STATUS"] = "";
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

            Paid.Rows.InsertAt(dr, Paid.Rows.Count);
            Paid.AcceptChanges();
            gv.DataSource = Paid;
            gv.DataBind();
        }

        /// <summary>
        /// CP 禮劵
        /// </summary>
        /// <param name="gv"></param>
        /// <param name="args">
        /// [0] = TYPE
        /// [1] = 禮券金額
        /// [2] = HG禮券名稱
        /// [3] = 禮券號碼
        /// </param>
        public void NewRow_Paid_CP(ASPxGridView gv, string[] args)
        {
            SavePaid_Detail_Temp(gv);
            DataRow dr = Paid.NewRow();
            dr["ITEM_STATUS"] = "";
            dr["ID"] = StringUtil.CStr(Advtek.Utility.GuidNo.getUUID());
            dr["POSUUID_MASTER"] = Head.Rows[0]["POSUUID_MASTER"];
            dr["DESCRIPTION"] = "禮券名稱:" + args[2] + ",禮券號碼:" + args[3];
            dr["PAID_MODE"] = 5;
            dr["PAID_MODE_NAME"] = "CP 禮劵";
            dr["PAID_AMOUNT"] = args[1];//禮券金額
            dr["coupon_id"] = args[2];//選取的禮券名稱所代表的id
            dr["coupon_no"] = args[3];//禮券號碼
            Paid.Rows.InsertAt(dr, Paid.Rows.Count);
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
            dr["ITEM_STATUS"] = "";
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
            Paid.Rows.InsertAt(dr, Paid.Rows.Count);
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
        public void Save_Sale_Data2Cache(VSS_SAL_SAL03 p)
        {
            SAL01_Facade Facade = new SAL01_Facade();
            #region 設定預設值
            if (Head != null && Head.Rows.Count > 0)
            {
                foreach (DataRow dr in Head.Rows)
                {
                    dr["STORE_NO"] = p.logMsg.STORENO;
                    DataTable dt = new Store_Facade().Query_StoreInfo(p.logMsg.STORENO);
                    if (dt.Rows.Count > 0)
                        dr["STORE_NAME"] = dt.Rows[0]["STORENAME"];
                    dr["MACHINE_ID"] = p.logMsg.MACHINE_ID;
                    dr["HOST_ID"] = p.logMsg.HOST_IP;
                    dr["CREATE_DTM"] = p.logMsg.CREATE_DTM;
                    dr["CREATE_USER"] = dr["SALE_PERSON"] = p.logMsg.CREATE_USER;
                    dr["MODI_DTM"] = p.logMsg.MODI_DTM;
                    dr["MODI_USER"] = p.logMsg.MODI_USER;

                    dr["INVALID_ID"] = p._PKEY; //原作廢的POSUUID_MASTER
                    dr["ORIGINAL_ID"] = Facade.getORIGINAL_ID(p._PKEY); //最原始的ID, 從未作廢的會與ID同，退換貨時會一路帶下來
                }
                Head.AcceptChanges();
            }

            if (Detail != null && Detail.Rows.Count > 0 && Detail.Select("ITEM_STATUS <> '作廢'") != null &&
                Detail.Select("ITEM_STATUS <> '作廢'").Length > 0)
            {
                foreach (DataRow dr in Detail.Select("ITEM_STATUS <> '作廢'"))
                {
                    if (dr["POSUUID_DETAIL"] == null || string.IsNullOrEmpty(StringUtil.CStr(dr["POSUUID_DETAIL"])))
                        dr["POSUUID_DETAIL"] = StringUtil.CStr(Advtek.Utility.GuidNo.getUUID());
                    dr["CREATE_DTM"] = p.logMsg.CREATE_DTM;
                    dr["CREATE_USER"] = p.logMsg.CREATE_USER;
                    dr["MODI_DTM"] = p.logMsg.MODI_DTM;
                    dr["MODI_USER"] = p.logMsg.MODI_USER;
                }
                Detail.AcceptChanges();
            }

            if (Discount != null && Discount.Rows.Count > 0 && Discount.Select("ITEM_STATUS <> '作廢'") != null &&
                Discount.Select("ITEM_STATUS <> '作廢'").Length > 0)
            {
                foreach (DataRow dr in Discount.Select("ITEM_STATUS <> '作廢'"))
                {
                    if (dr["POSUUID_DETAIL"] == null || string.IsNullOrEmpty(StringUtil.CStr(dr["POSUUID_DETAIL"])))
                        dr["POSUUID_DETAIL"] = StringUtil.CStr(Advtek.Utility.GuidNo.getUUID());
                    dr["CREATE_DTM"] = p.logMsg.CREATE_DTM;
                    dr["CREATE_USER"] = p.logMsg.CREATE_USER;
                    dr["MODI_DTM"] = p.logMsg.MODI_DTM;
                    dr["MODI_USER"] = p.logMsg.MODI_USER;
                }
                Discount.AcceptChanges();
            }

            if (Paid != null && Paid.Rows.Count > 0 && Paid.Select("ITEM_STATUS <> '作廢'") != null &&
                Paid.Select("ITEM_STATUS <> '作廢'").Length > 0)
            {
                foreach (DataRow dr in Paid.Select("ITEM_STATUS <> '作廢'"))
                {
                    dr["CREATE_DTM"] = p.logMsg.CREATE_DTM;
                    dr["CREATE_USER"] = p.logMsg.CREATE_USER;
                    dr["MODI_DTM"] = p.logMsg.MODI_DTM;
                    dr["MODI_USER"] = p.logMsg.MODI_USER;
                }
                Paid.AcceptChanges();
            }
            #endregion
            OracleConnection objConn = OracleDBUtil.GetConnection();
            OracleTransaction objTX = objConn.BeginTransaction();
            try
            {
                if (Head != null && Head.Rows.Count > 0)
                    Facade.InsertSale_Head(Head);
                if (Detail != null && Detail.Rows.Count > 0 && Detail.Select("ITEM_STATUS <> '作廢'") != null && 
                    Detail.Select("ITEM_STATUS <> '作廢'").Length > 0)
                    Facade.InsertSale_Detail(Detail.Select("ITEM_STATUS <> '作廢'").CopyToDataTable());
                if (Discount != null && Discount.Rows.Count > 0 && Discount.Select("ITEM_STATUS <> '作廢'") != null && 
                    Discount.Select("ITEM_STATUS <> '作廢'").Length > 0)
                    Facade.InsertSale_Detail(Discount.Select("ITEM_STATUS <> '作廢'").CopyToDataTable());
                //Facade.InsertSale_IMEI_LOG(IMEI_LOG);
                if (Paid != null && Paid.Rows.Count > 0 && Paid.Select("ITEM_STATUS <> '作廢'") != null && 
                    Paid.Select("ITEM_STATUS <> '作廢'").Length > 0)
                    Facade.InsertPaid_Detail(Paid.Select("ITEM_STATUS <> '作廢'").CopyToDataTable());
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
        /// 結帳時將資料存入資料庫
        /// </summary>
        /// <param name="p"></param>
        /// <param name="objTX">外部傳入Transaction</param>
        public void Save_Sale_Data2Cache(VSS_SAL_SAL03 p, OracleTransaction objTX)
        {
            SAL01_Facade Facade = new SAL01_Facade();
            #region 設定預設值
            if (Head != null && Head.Rows.Count > 0)
            {
                foreach (DataRow dr in Head.Rows)
                {
                    dr["STORE_NO"] = p.logMsg.STORENO;
                    DataTable dt = new Store_Facade().Query_StoreInfo(p.logMsg.STORENO);
                    if (dt.Rows.Count > 0)
                        dr["STORE_NAME"] = dt.Rows[0]["STORENAME"];
                    dr["MACHINE_ID"] = p.logMsg.MACHINE_ID;
                    dr["HOST_ID"] = p.logMsg.HOST_IP;
                    dr["CREATE_DTM"] = p.logMsg.CREATE_DTM;
                    dr["CREATE_USER"] = dr["SALE_PERSON"] = p.logMsg.CREATE_USER;
                    dr["MODI_DTM"] = p.logMsg.MODI_DTM;
                    dr["MODI_USER"] = p.logMsg.MODI_USER;

                    dr["INVALID_ID"] = p._PKEY; //原作廢的POSUUID_MASTER
                    dr["ORIGINAL_ID"] = Facade.getORIGINAL_ID(p._PKEY); //最原始的ID, 從未作廢的會與ID同，退換貨時會一路帶下來
                }
                Head.AcceptChanges();
            }

            if (Detail != null && Detail.Rows.Count > 0 && Detail.Select("ITEM_STATUS <> '作廢'") != null &&
                Detail.Select("ITEM_STATUS <> '作廢'").Length > 0)
            {
                foreach (DataRow dr in Detail.Select("ITEM_STATUS <> '作廢'"))
                {
                    if (dr["POSUUID_DETAIL"] == null || string.IsNullOrEmpty(StringUtil.CStr(dr["POSUUID_DETAIL"])))
                        dr["POSUUID_DETAIL"] = StringUtil.CStr(Advtek.Utility.GuidNo.getUUID());
                    dr["CREATE_DTM"] = p.logMsg.CREATE_DTM;
                    dr["CREATE_USER"] = p.logMsg.CREATE_USER;
                    dr["MODI_DTM"] = p.logMsg.MODI_DTM;
                    dr["MODI_USER"] = p.logMsg.MODI_USER;
                }
                Detail.AcceptChanges();
            }

            if (Discount != null && Discount.Rows.Count > 0 && Discount.Select("ITEM_STATUS <> '作廢'") != null &&
                Discount.Select("ITEM_STATUS <> '作廢'").Length > 0)
            {
                foreach (DataRow dr in Discount.Select("ITEM_STATUS <> '作廢'"))
                {
                    if (dr["POSUUID_DETAIL"] == null || string.IsNullOrEmpty(StringUtil.CStr(dr["POSUUID_DETAIL"])))
                        dr["POSUUID_DETAIL"] = StringUtil.CStr(Advtek.Utility.GuidNo.getUUID());
                    dr["CREATE_DTM"] = p.logMsg.CREATE_DTM;
                    dr["CREATE_USER"] = p.logMsg.CREATE_USER;
                    dr["MODI_DTM"] = p.logMsg.MODI_DTM;
                    dr["MODI_USER"] = p.logMsg.MODI_USER;
                }
                Discount.AcceptChanges();
            }

            if (Paid != null && Paid.Rows.Count > 0 && Paid.Select("ITEM_STATUS <> '作廢'") != null &&
                Paid.Select("ITEM_STATUS <> '作廢'").Length > 0)
            {
                foreach (DataRow dr in Paid.Select("ITEM_STATUS <> '作廢'"))
                {
                    dr["CREATE_DTM"] = p.logMsg.CREATE_DTM;
                    dr["CREATE_USER"] = p.logMsg.CREATE_USER;
                    dr["MODI_DTM"] = p.logMsg.MODI_DTM;
                    dr["MODI_USER"] = p.logMsg.MODI_USER;
                }
                Paid.AcceptChanges();
            }
            #endregion
            try
            {
                if (Head != null && Head.Rows.Count > 0)
                    Facade.InsertSale_Head(Head, objTX);
                if (Detail != null && Detail.Rows.Count > 0 && Detail.Select("ITEM_STATUS <> '作廢'") != null && 
                    Detail.Select("ITEM_STATUS <> '作廢'").Length > 0)
                    Facade.InsertSale_Detail(Detail.Select("ITEM_STATUS <> '作廢'").CopyToDataTable(), objTX);
                if (Discount != null && Discount.Rows.Count > 0 && Discount.Select("ITEM_STATUS <> '作廢'") != null && 
                    Discount.Select("ITEM_STATUS <> '作廢'").Length > 0)
                    Facade.InsertSale_Detail(Discount.Select("ITEM_STATUS <> '作廢'").CopyToDataTable(), objTX);
                //Facade.InsertSale_IMEI_LOG(IMEI_LOG);
                if (Paid != null && Paid.Rows.Count > 0 && Paid.Select("ITEM_STATUS <> '作廢'") != null && 
                    Paid.Select("ITEM_STATUS <> '作廢'").Length > 0)
                    Facade.InsertPaid_Detail(Paid.Select("ITEM_STATUS <> '作廢'").CopyToDataTable(), objTX);
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
        public void Delete_Sale_DataFromCache(VSS_SAL_SAL03 p)
        {
            OracleConnection objConn = OracleDBUtil.GetConnection();
            OracleTransaction objTX = objConn.BeginTransaction();
            try
            {
                SAL01_Facade Facade = new SAL01_Facade();
                //Facade.DeleteSale_IMEI_LOG(IMEI_LOG);
                if (Detail != null && Detail.Rows.Count > 0 && Detail.Select("ITEM_STATUS <> '作廢'") != null && 
                    Detail.Select("ITEM_STATUS <> '作廢'").Length > 0)
                    Facade.DeleteSale_Detail(Detail.Select("ITEM_STATUS <> '作廢'").CopyToDataTable());
                if (Discount != null && Discount.Rows.Count > 0 && Discount.Select("ITEM_STATUS <> '作廢'") != null && 
                    Discount.Select("ITEM_STATUS <> '作廢'").Length > 0)
                    Facade.DeleteSale_Detail(Discount.Select("ITEM_STATUS <> '作廢'").CopyToDataTable());
                Facade.DeletePaid_DetailByPOSUUID_MASTER(StringUtil.CStr(Head.Rows[0]["POSUUID_MASTER"]));
                //Facade.DeletePaid_Detail(Paid);
                Facade.DeleteSale_Head(Head);
                objTX.Commit();
            }
            catch (Exception ex)
            {
                objTX.Rollback();
                throw ex;
            }
            DataRow[] drs = null;
            if (Discount != null && Discount.Rows.Count > 0) 
                drs = Discount.Select(" ITEM_STATUS <> '作廢' AND SOURCE_TYPE = '11' ");
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
        public void Delete_Sale_DataFromCache(VSS_SAL_SAL03 p, OracleTransaction objTX)
        {
            try
            {
                SAL01_Facade Facade = new SAL01_Facade();
                //Facade.DeleteSale_IMEI_LOG(IMEI_LOG, objTX);
                if (Detail != null && Detail.Rows.Count > 0 && Detail.Select("ITEM_STATUS <> '作廢'") != null && 
                    Detail.Select("ITEM_STATUS <> '作廢'").Length > 0)
                    Facade.DeleteSale_Detail(Detail.Select("ITEM_STATUS <> '作廢'").CopyToDataTable(), objTX);
                if (Discount != null && Discount.Rows.Count > 0 && Discount.Select("ITEM_STATUS <> '作廢'") != null && 
                    Discount.Select("ITEM_STATUS <> '作廢'").Length > 0)
                    Facade.DeleteSale_Detail(Discount.Select("ITEM_STATUS <> '作廢'").CopyToDataTable(), objTX);
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
        ///結帳時將資料存入DB從銷貨單新增
        /// </summary>
        /// <param name="p"></param>
        public void Save_Sale_Data2DBFromSAL01(VSS_SAL_SAL03 p, OracleTransaction objTX)
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
            if (Detail.Select("ITEM_STATUS <> '作廢'") != null && Detail.Select("ITEM_STATUS <> '作廢'").Length > 0)
                Facade.CheckOutFromUnClose(Head, Detail.Select(" ITEM_STATUS <> '作廢'").CopyToDataTable(), p.logMsg.MODI_USER, objTX);
            SetInvoiceOrReceipt(objTX, p.logMsg.HOST_IP, p);
            SaleHeadDataBind(p);
            //  Mode_CheckOut(p);
        }
        /// <summary>
        ///結帳時將資料從Cache存入DB
        /// </summary>
        /// <param name="p"></param>
        public void Save_Sale_Data2DBFromCache(VSS_SAL_SAL03 p, OracleTransaction objTX)
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
            if (Detail.Select("ITEM_STATUS <> '作廢'") != null && Detail.Select("ITEM_STATUS <> '作廢'").Length > 0)
                Facade.CheckOutFromUnClose(Head, Detail.Select(" ITEM_STATUS <> '作廢'").CopyToDataTable(), p.logMsg.MODI_USER, objTX);
            SetInvoiceOrReceipt(objTX, p.logMsg.HOST_IP, p);
            SaleHeadDataBind(p);
            //Mode_CheckOut(p);
        }
        /// <summary>
        /// 檢查開立發票的原則 依貨品資料INV_TYPE去開立發票,
        /// </summary>
        public void SetInvoiceOrReceipt(OracleTransaction objTX, string HOST_ID, VSS_SAL_SAL03 p)
        {
            SAL01_Facade Facade = new SAL01_Facade();
            DataRow[] INV_TAX_DRA;       //發票 應稅
            DataRow[] INV_TAX_DRA_ZERO;  //發票 應稅 0稅
            DataRow[] INV_NO_TAX_DRA;    //發票 免稅
            DataRow[] RECDT;             //收據

            DataTable dtInv = Detail.Clone();
            dtInv.Merge(Detail);
            dtInv.Merge(Discount);

            int invoiceTotalAmt = 0;
            if (Head.Rows[0]["INVOICE_TOTAL_AMOUNT"] != null && StringUtil.CStr(Head.Rows[0]["INVOICE_TOTAL_AMOUNT"]) != ""
                && NumberUtil.IsNumeric(StringUtil.CStr(Head.Rows[0]["INVOICE_TOTAL_AMOUNT"])))
                invoiceTotalAmt = int.Parse(StringUtil.CStr(Head.Rows[0]["INVOICE_TOTAL_AMOUNT"]));
            if (invoiceTotalAmt <= 0)
            {
                RECDT = dtInv.Select(" 1 = 1 ");
                Facade.InsertReceipt(Head, RECDT, objTX);                                             //收據
            }
            else
            {
                INV_TAX_DRA = dtInv.Select(" ITEM_STATUS <> '作廢' AND INV_TYPE = '1' AND TAXABLE = 'Y' AND TAXRATE <> 0 ");     //發票 應稅
                INV_TAX_DRA_ZERO = dtInv.Select(" ITEM_STATUS <> '作廢' AND INV_TYPE = '1' AND TAXABLE = 'Y' AND TAXRATE = 0 "); //發票 應稅 0稅
                INV_NO_TAX_DRA = dtInv.Select(" ITEM_STATUS <> '作廢' AND INV_TYPE = '1' AND TAXABLE = 'N' ");                   //發票 免稅
                RECDT = dtInv.Select(" ITEM_STATUS <> '作廢' AND INV_TYPE = '3' ");                                              //收據

                Facade.InsertInvoice(Head, INV_TAX_DRA, "1", objTX, HOST_ID);
                Facade.InsertInvoice(Head, INV_TAX_DRA_ZERO, "2", objTX, HOST_ID);
                Facade.InsertInvoice(Head, INV_NO_TAX_DRA, "3", objTX, HOST_ID);
                Facade.InsertReceipt(Head, RECDT, objTX);
            }
        }
        #endregion
        #region 當前模式
        /// <summary>
        /// 進入有貨品資料,無付款資料時
        /// </summary>
        /// <param name="p"></param>
        public void Mode_HavePROD(VSS_SAL_SAL03 p)
        {
            p.btnExchangeGoodsCheckOut.Enabled = false;
            p.btnExchangeGoodsCancel.Enabled = true;
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
                p.gvMaster.FindChildControl<ASPxButton>("btnStoreDiscount").Enabled = false;
                p.gvMaster.FindChildControl<ASPxButton>("btnHappyGoNet").Enabled = false;
            }
            else
            {
                p.gvMaster.FindChildControl<ASPxButton>("btnItemAdd").Enabled = true;
                p.gvMaster.FindChildControl<ASPxButton>("btnMixPromotion").Enabled = false;
                p.gvMaster.FindChildControl<ASPxButton>("btnStoreDiscount").Enabled = true;
                p.gvMaster.FindChildControl<ASPxButton>("btnHappyGoNet").Enabled = true;

                DataRow[] dra = Detail.Select("ITEM_TYPE = '6' And ITEM_STATUS <> '作廢'");
                if (dra != null && dra.Length > 0)  //貨品資料裡有特殊抱怨折扣項目，進入有特殊抱怨折扣模式
                {
                    Mode_HaveStoreDiscount(p);
                }

                DataRow[] drb = Detail.Select("ITEM_TYPE IN ('4','14') And ITEM_STATUS <> '作廢'");
                DataRow drM = Head.Rows[0];
                if ((drb != null && drb.Length > 0) || StringUtil.CStr(drM["SALE_TYPE"]) == "2")  //貨品資料裡沒有HappyGo折扣項目且交易不屬【帳單代收】，才可點選
                {
                    p.gvMaster.FindChildControl<ASPxButton>("btnHappyGoNet").Enabled = false;
                    p.gvMaster.FindChildControl<ASPxButton>("btnItemAdd").Enabled = false;
                }

                DataRow[] drc = Detail.Select("ITEM_TYPE IN ('7','13') And ITEM_STATUS <> '作廢'");
                if (drc != null && drc.Length > 0)  //貨品資料裡沒有普通商品加價購及贈品，才可點選
                {
                    p.gvMaster.FindChildControl<ASPxButton>("btnAddProd").Enabled = false;
                    p.gvMaster.FindChildControl<ASPxButton>("btnItemAdd").Enabled = false;
                }

                DataRow[] drd = Detail.Select("PROMOTION_CODE is not null");
                if (drd != null && drd.Length > 0)  //貨品資料裡有促銷代碼
                {
                    p.gvMaster.FindChildControl<ASPxButton>("btnMixPromotion").Enabled = true;
                }
            }

            DataRow[] dre = Detail.Select("SOURCE_TYPE = '10' ");
            if (dre != null && dre.Length > 0)
            {   //網購不能換貨
                p.btnConfirm.Enabled = false;
                p.btnExchangeGoodsCheckOut.Enabled = false;
            }
            else
            {
                p.btnConfirm.Enabled = true;
            }
            p.btnCancel.Enabled = false;

            p.gvCheckOut.FindChildControl<ASPxButton>("btnCash").Enabled = false;
            p.gvCheckOut.FindChildControl<ASPxButton>("btnPayDEL").Enabled = false;
            p.gvCheckOut.FindChildControl<ASPxButton>("btnCredit").Enabled = false;
            p.gvCheckOut.FindChildControl<ASPxButton>("btnDivCredit").Enabled = false;
            p.gvCheckOut.FindChildControl<ASPxButton>("btnOffLineCredit").Enabled = false;
            p.lbChange.Text = "";
            p.lbPayAmount.Text = "";
        }
        /// <summary>
        /// 進入沒有輸入貨品時
        /// </summary>
        /// <param name="p"></param>
        public void Mode_NoPROD(VSS_SAL_SAL03 p)
        {
            p.btnExchangeGoodsCheckOut.Enabled = false;
            p.btnExchangeGoodsCancel.Enabled = false;
            p.gvMaster.Enabled = true;
            p.gvDetail.Enabled = true;
            p.gvCheckOut.Enabled = false;
            p.txtREMARK.Enabled = true;
            p.txtUNI_NO.Enabled = true;
            p.txtUNI_TITLE.Enabled = true;
            p.btnConfirm.Enabled = false;
            p.btnCancel.Enabled = false;
            p.gvCheckOut.FindChildControl<ASPxButton>("btnCash").Enabled = false;
            p.gvCheckOut.FindChildControl<ASPxButton>("btnPayDEL").Enabled = false;
            p.gvMaster.FindChildControl<ASPxButton>("btnStoreDiscount").Enabled = false;
            p.gvMaster.FindChildControl<ASPxButton>("btnHappyGoNet").Enabled = false;
            p.gvMaster.FindChildControl<ASPxButton>("btnMixPromotion").Enabled = false;
            p.gvMaster.FindChildControl<ASPxButton>("btnItemAdd").Enabled = true;
            p.gvMaster.FindChildControl<ASPxButton>("btnAddProd").Enabled = false;
            p.lbChange.Text = "";
            p.lbPayAmount.Text = "";
        }
        /// <summary>
        /// 進入付款模式
        /// </summary>
        /// <param name="p"></param>
        public void Mode_PayCash(VSS_SAL_SAL03 p)
        {
            p.btnExchangeGoodsCheckOut.Enabled = false;
            p.btnExchangeGoodsCancel.Enabled = false;
            p.gvMaster.Enabled = false;
            p.gvCheckOut.Enabled = true;
            p.txtREMARK.Enabled = false;
            p.txtUNI_NO.Enabled = false;
            p.txtUNI_TITLE.Enabled = false;
            p.btnConfirm.Enabled = false;
            if (Paid.Select(" ITEM_STATUS <> '作廢' ") != null && Paid.Select(" ITEM_STATUS <> '作廢' ").Length > 0)
            {
                p.btnCancel.Enabled = false;
                if (p.lbPayAmount.Text != "" && NumberUtil.IsNumeric(p.lbPayAmount.Text) && p.lbPayAmount.Text == "0")
                    p.btnExchangeGoodsCheckOut.Enabled = true;
                else
                    p.btnExchangeGoodsCheckOut.Enabled = false;
            }
            else
            {
                p.btnCancel.Enabled = true;
                p.btnExchangeGoodsCheckOut.Enabled = false;
            }
            p.gvMaster.FindChildControl<ASPxButton>("btnStoreDiscount").Enabled = false;
            p.gvMaster.FindChildControl<ASPxButton>("btnHappyGoNet").Enabled = false;

            DataRow drM = Head.Rows[0];
            p.gvCheckOut.FindChildControl<ASPxButton>("btnPayDEL").Enabled = true;
            p.gvCheckOut.FindChildControl<ASPxButton>("btnCash").Enabled = false;
            p.gvCheckOut.FindChildControl<ASPxButton>("btnCredit").Enabled = false;
            p.gvCheckOut.FindChildControl<ASPxButton>("btnDivCredit").Enabled = false;
            p.gvCheckOut.FindChildControl<ASPxButton>("btnOffLineCredit").Enabled = false;

            DataTable dtPayMode = p.Facade01.getValidPaymentMode();
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
                DataRow[] drs = Detail.Select(" PRODNO = '" + p.hidETCProdNo + "'");
                if (drs != null && drs.Length > 0)
                {   //結帳目中有ETC/NCIC/SEEDNET等只能使用現金結帳
                    p.gvCheckOut.FindChildControl<ASPxButton>("btnCredit").Enabled = false;
                    p.gvCheckOut.FindChildControl<ASPxButton>("btnDivCredit").Enabled = false;
                    p.gvCheckOut.FindChildControl<ASPxButton>("btnOffLineCredit").Enabled = false;
                }
            }
            int limitAmt = p.Facade01.getCreditDivLimitAmount();
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
            DataRow[] drsCredit = Paid.Select(" ITEM_STATUS <> '作廢' AND PAID_MODE in ('2', '3', '4') ");
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
        public void Mode_CheckOut(VSS_SAL_SAL03 p)
        {
            p.btnExchangeGoodsCheckOut.Enabled = false;
            p.btnExchangeGoodsCancel.Enabled = false;
            p.gvMaster.Enabled = false;
            p.gvCheckOut.Enabled = false;
            p.txtREMARK.Enabled = false;
            p.txtUNI_NO.Enabled = false;
            p.txtUNI_TITLE.Enabled = false;
            p.gvMaster.FindChildControl<ASPxButton>("btnStoreDiscount").Enabled = false;
            p.gvMaster.FindChildControl<ASPxButton>("btnHappyGoNet").Enabled = false;
            p.gvMaster.FindChildControl<ASPxButton>("btnItemAdd").Enabled = true;
        }
        /// <summary>
        /// 進入有輸入店長折扣時
        /// </summary>
        /// <param name="p"></param>
        public void Mode_HaveStoreDiscount(VSS_SAL_SAL03 p)
        {
            p.gvMaster.FindChildControl<ASPxButton>("btnItemAdd").Enabled = false;
            p.gvMaster.FindChildControl<ASPxButton>("btnHappyGoNet").Enabled = false;
            p.gvMaster.FindChildControl<ASPxButton>("btnMixPromotion").Enabled = false;
            p.gvMaster.FindChildControl<ASPxButton>("btnStoreDiscount").Enabled = false;
            p.gvMaster.FindChildControl<ASPxButton>("btnAddProd").Enabled = false;
        }
        #endregion
        #region 計算總金額
        public void calTotalAmount(VSS_SAL_SAL03 p)
        {
            int SALE_AFTER_TOTAL_AMOUNT = 0;
            int oringinalTotalAmount = 0;
            int returnAmount = 0;
            if (Detail != null && Detail.Rows.Count > 0 && Detail.Select(" ITEM_STATUS <> '作廢' ") != null &&
                Detail.Select(" ITEM_STATUS <> '作廢' ").Length > 0)
            {
                foreach (DataRow dr in Detail.Select(" ITEM_STATUS <> '作廢' "))//明細資料 含 ITME 1,2,3應收 和 5,6折扣類 不含5未結清單折扣 
                {  //總金額計算
                    if (dr["TOTAL_AMOUNT"] != DBNull.Value && dr["TOTAL_AMOUNT"] != null
                                && StringUtil.CStr(dr["TOTAL_AMOUNT"]) != "" && NumberUtil.IsNumeric(StringUtil.CStr(dr["TOTAL_AMOUNT"])))
                        SALE_AFTER_TOTAL_AMOUNT += int.Parse(StringUtil.CStr(dr["TOTAL_AMOUNT"]));
                }
            }

            if (Discount != null && Discount.Rows.Count > 0 && Discount.Select(" ITEM_STATUS <> '作廢' ") != null &&
                Discount.Select(" ITEM_STATUS <> '作廢' ").Length > 0)
            {
                foreach (DataRow dr in Discount.Select(" ITEM_STATUS <> '作廢' ")) //未結轉入才有的資料 ITEM_TYPE = 5,11,12;
                {
                    if (dr["TOTAL_AMOUNT"] != DBNull.Value && dr["TOTAL_AMOUNT"] != null
                        && StringUtil.CStr(dr["TOTAL_AMOUNT"]) != "" && NumberUtil.IsNumeric(StringUtil.CStr(dr["TOTAL_AMOUNT"])))
                        SALE_AFTER_TOTAL_AMOUNT += int.Parse(StringUtil.CStr(dr["TOTAL_AMOUNT"]));
                }
            }

            if (Head != null && Head.Rows.Count > 0)
            {
                Head.Rows[0]["SALE_TOTAL_AMOUNT"] = SALE_AFTER_TOTAL_AMOUNT;
                Head.AcceptChanges();
            }
            STORE_REC_TOTAL_AMOUNT = SALE_AFTER_TOTAL_AMOUNT;
            if (p.lblOriginalTOTAL_AMOUNT.Text != "" && NumberUtil.IsNumeric(p.lblOriginalTOTAL_AMOUNT.Text))
                oringinalTotalAmount = int.Parse(p.lblOriginalTOTAL_AMOUNT.Text);
            p.lbTOTAL_AMOUNT.Text = StringUtil.CStr(STORE_REC_TOTAL_AMOUNT);
            if (STORE_REC_TOTAL_AMOUNT - oringinalTotalAmount >= 0)
            {
                p.lblDiffAmtDesc.Text = "應補差額";
                returnAmount = STORE_REC_TOTAL_AMOUNT - oringinalTotalAmount;
                p.lblReturnAmount.Text = StringUtil.CStr(returnAmount);
            }
            else
            {
                p.lblDiffAmtDesc.Text = "應退差額";
                returnAmount = oringinalTotalAmount - STORE_REC_TOTAL_AMOUNT;
                p.lblReturnAmount.Text = StringUtil.CStr(returnAmount);
            }
            
            p.lbPayAmount.Text = "";
            p.lbChange.Text = "";
        }
        #endregion 計算總金額
        #region 稅額計算
        public void CalculationTax(VSS_SAL_SAL03 p)
        {
            #region 設定稅額預設金額
            SAL01_Facade SAL01Facade = new SAL01_Facade();
            if (Head.Rows[0]["SALE_TYPE"] != null && StringUtil.CStr(Head.Rows[0]["SALE_TYPE"]) == "2")
            {   //代收交易
                StringBuilder invProdLis = new StringBuilder("");
                DataTable dtInvProd = SAL01Facade.getInvoiceProduct();
                if (dtInvProd != null && dtInvProd.Rows.Count > 0)
                {
                    foreach (DataRow dr in dtInvProd.Rows)
                    {
                        if (dr[0] != null && StringUtil.CStr(dr[0]) != "")
                            invProdLis.Append(StringUtil.CStr(dr[0])).Append(",");
                    }
                }

                if (Discount != null && Discount.Rows.Count > 0)
                {
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
                }

                if (Detail != null && Detail.Rows.Count > 0)
                {
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
                        }
                        else
                        {   //代收交易不開發票貨品,一律開收據,稅算為0
                            dr["TAXABLE"] = "N";  //課稅別
                            dr["TAXRATE"] = 0;    //稅率
                            dr["TAX"] = 0;                                         //稅額
                            dr["BEFORE_TAX"] = dr["TOTAL_AMOUNT"];                 //稅前金額
                        }
                    }
                    Detail.AcceptChanges();
                }
            }
            else
            {   //一般交易
                StringBuilder nonInvProdLis = new StringBuilder("");
                DataTable dtNonInvProd = SAL01Facade.getNonInvoiceProduct();
                if (dtNonInvProd != null && dtNonInvProd.Rows.Count > 0)
                {
                    foreach (DataRow dr in dtNonInvProd.Rows)
                    {
                        if (dr[0] != null && StringUtil.CStr(dr[0]) != "")
                            nonInvProdLis.Append(StringUtil.CStr(dr[0])).Append(",");
                    }
                }

                if (Discount != null && Discount.Rows.Count > 0)
                {
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
                }

                if (Detail != null && Detail.Rows.Count > 0)
                {
                    foreach (DataRow dr in Detail.Rows)
                    {
                        if (StringUtil.CStr(nonInvProdLis).IndexOf(StringUtil.CStr(dr["PRODNO"])) > -1)
                        {   //一般交易不開發票貨品,一律開收據,稅算為0
                            dr["TAXABLE"] = "N";  //課稅別
                            dr["TAXRATE"] = 0;    //稅率
                            dr["TAX"] = 0;                                         //稅額
                            dr["BEFORE_TAX"] = dr["TOTAL_AMOUNT"];                 //稅前金額
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
                        }
                    }
                    Detail.AcceptChanges();
                }
            }
            #region 所有在Product產品主檔中,可以查到商品料號的產品,開立發票與否,都依據INV_TYPE欄位的設定
            Product_Facade prodFacade = new Product_Facade();
            DataRow[] drs = null;
            if (Detail != null && Detail.Rows.Count > 0) 
                drs = Detail.Select(" ITEM_STATUS <> '作廢' ");
            if (drs != null && drs.Length > 0)
            {
                foreach (DataRow dr in drs)
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

            drs = null;
            if (Discount != null && Discount.Rows.Count > 0)
                drs = Discount.Select(" ITEM_STATUS <> '作廢' ");
            if (drs != null && drs.Length > 0)
            {
                foreach (DataRow dr in drs)
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

            if (Detail != null && Detail.Rows.Count > 0 && Detail.Select(" ITEM_STATUS <> '作廢' ") != null &&
                Detail.Select(" ITEM_STATUS <> '作廢' ").Length > 0)
            {
                foreach (DataRow dr in Detail.Select(" ITEM_STATUS <> '作廢' "))//明細資料 含 ITME 1,2,3應收 和 5,6折扣類 不含5未結清單折扣 
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
            }

            if (Discount != null && Discount.Rows.Count > 0 && Discount.Select(" ITEM_STATUS <> '作廢' ") != null &&
                Discount.Select(" ITEM_STATUS <> '作廢' ").Length > 0)
            {
                foreach (DataRow dr in Discount.Select(" ITEM_STATUS <> '作廢' ")) //未結轉入才有的資料 ITEM_TYPE = 5,11,12;
                {
                    if (dr["TOTAL_AMOUNT"] != DBNull.Value && dr["TOTAL_AMOUNT"] != null
                        && StringUtil.CStr(dr["TOTAL_AMOUNT"]) != "" && NumberUtil.IsNumeric(StringUtil.CStr(dr["TOTAL_AMOUNT"])))
                        DISCOUNT_AFTER_TOTAL_AMOUNT += int.Parse(StringUtil.CStr(dr["TOTAL_AMOUNT"]));
                    if (StringUtil.CStr(dr["TAXABLE"]) == "Y") //Y:應稅N:免稅;Y:應稅，taxrate為 0，為零稅
                        if (dr["TOTAL_AMOUNT"] != DBNull.Value && dr["TOTAL_AMOUNT"] != null
                            && StringUtil.CStr(dr["TOTAL_AMOUNT"]) != "" && NumberUtil.IsNumeric(StringUtil.CStr(dr["TOTAL_AMOUNT"])))
                            DISCOUNT_AFTER_TAX_TOTAL_AMOUNT += int.Parse(StringUtil.CStr(dr["TOTAL_AMOUNT"]));
                }
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
            DataRow[] drsNew = null;
            if (Detail != null && Detail.Rows.Count > 0)
                drsNew = Detail.Select(" ITEM_STATUS <> '作廢' ");
            if (drsNew != null && drsNew.Length > 0)
                totalDetal.Merge(drsNew.CopyToDataTable());

            drsNew = null;
            if (Discount != null && Discount.Rows.Count > 0)
                drsNew = Discount.Select(" ITEM_STATUS <> '作廢' ");
            if (drsNew != null && drsNew.Length > 0)
                totalDetal.Merge(drsNew.CopyToDataTable());
            totalDetal = CalDetailTax(totalDetal);
            DataRow[] drsDetail = null;
            if (Detail != null && Detail.Rows.Count > 0)
            {
                drsDetail = Detail.Select(" ITEM_STATUS = '作廢' ");
                Detail = null;
            }
            if (drsDetail != null && drsDetail.Length > 0)
                Detail = drsDetail.CopyToDataTable();
            drsDetail = totalDetal.Select(" ITEM_TYPE IN ('1','2','3','4','6','7','8','9','10','12','13','14','15','16','17') ");
            if (drsDetail != null && drsDetail.Length > 0)
                if (Detail != null)
                    Detail.Merge(drsDetail.CopyToDataTable());
                else
                    Detail = drsDetail.CopyToDataTable();

            DataRow[] drsDiscount = null;
            if (Discount != null && Discount.Rows.Count > 0)
            {
                drsDiscount = Discount.Select(" ITEM_STATUS = '作廢' ");
                Discount = null;
            }
            if (drsDiscount != null && drsDiscount.Length > 0)
                Discount = drsDiscount.CopyToDataTable();
            drsDiscount = totalDetal.Select(" ITEM_TYPE IN ('5','11') ");
            if (drsDiscount != null && drsDiscount.Length > 0)
                if (Discount != null) 
                    Discount.Merge(drsDiscount.CopyToDataTable());
                else
                    Discount = drsDiscount.CopyToDataTable();
            //--------------------------------------
            #endregion
            int oringinalTotalAmount = 0;
            int returnAmount = 0;
            if (p.lblOriginalTOTAL_AMOUNT.Text != "" && NumberUtil.IsNumeric(p.lblOriginalTOTAL_AMOUNT.Text))
                oringinalTotalAmount = int.Parse(p.lblOriginalTOTAL_AMOUNT.Text);
            if (SALE_AFTER_TOTAL_AMOUNT - oringinalTotalAmount >= 0)
            {
                p.lblDiffAmtDesc.Text = "應補差額";
                returnAmount = SALE_AFTER_TOTAL_AMOUNT - oringinalTotalAmount;
                p.lblReturnAmount.Text = StringUtil.CStr(returnAmount);
            }
            else
            {
                p.lblDiffAmtDesc.Text = "應退差額";
                returnAmount = oringinalTotalAmount - SALE_AFTER_TOTAL_AMOUNT;
                p.lblReturnAmount.Text = StringUtil.CStr(returnAmount);
            }
            #region 付款金額 找零金
            p.lbPayAmount.Text = "";
            p.lbChange.Text = "";
            #endregion
        }
        #endregion
        #region 計算折扣明細稅額
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

            DataRow[] drsNew = Detail.Select(" ITEM_STATUS <> '作廢' ");
            //單價計算
            foreach (DataRow row in drsNew)
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
            if (drsNew != null && drsNew.Length > 0)
            {
                if (DETAIL_BEFORE_TOTAL_AMOUNT != SALE_BEFORE_TOTAL_TAX)
                {
                    double before_amount = SALE_BEFORE_TOTAL_TAX - DETAIL_BEFORE_TOTAL_AMOUNT;
                    //抓出金額最高的來做除稅

                    DataRow row = drsNew[drsNew.Length - 1];
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
        #endregion
        #region 付款金額 找零金
        public void calPayTotal_Change(VSS_SAL_SAL03 p)
        {
            STORE_PAY_TOTAL_AMOUNT = 0;
            foreach (DataRow dr in Paid.Select("ITEM_STATUS <> '作廢'"))
                STORE_PAY_TOTAL_AMOUNT += int.Parse(StringUtil.CStr(dr["PAID_AMOUNT"]));
            
            if (Paid != null && Paid.Rows.Count > 0 && Paid.Select("ITEM_STATUS <> '作廢'") != null && 
                Paid.Select("ITEM_STATUS <> '作廢'").Length > 0)
            {
                int shouldPay = 0;
                shouldPay = STORE_REC_TOTAL_AMOUNT - STORE_PAY_TOTAL_AMOUNT;
                if (shouldPay < 0)
                    shouldPay = 0;
                if (shouldPay == 0)
                    p.btnExchangeGoodsCheckOut.Enabled = true;
                else
                    p.btnExchangeGoodsCheckOut.Enabled = false;
                p.lbPayAmount.Text = StringUtil.CStr(shouldPay);
                STORE_CHANGE_AMOUNT = STORE_PAY_TOTAL_AMOUNT - STORE_REC_TOTAL_AMOUNT;
                if (STORE_CHANGE_AMOUNT < 0)
                    STORE_CHANGE_AMOUNT = 0;
                p.lbChange.Text = StringUtil.CStr(STORE_CHANGE_AMOUNT);
                DataRow[] drsPaid = Paid.Select(" ITEM_STATUS <> '作廢' AND PAID_MODE IN ('1','5','6','7') ");
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
        }
        #endregion
    }
    string _SRC_TYPE
    {
        get
        {
            return Session["EXCHANGE_SRC_TYPE"] as string;
        }
    }
    string _PKEY
    {
        get
        {
            string key = Session["ExchangeOrderPKEY"] as string; //統一要給 POSUUID_MASTER;
            return key;
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
    /// 從查詢頁面 or 從SSI載入舊交易及新交易資料
    /// </summary>
    void LoadData1()
    {
        LoadInvalData(_PKEY); //載入需作廢資料
        if (hidSALE_TYPE.Text == "2")
        {   //代收交易不可以換貨
            btnExchangeGoodsCheckOut.Enabled = false;
            btnExchangeGoodsCancel.Enabled = false;
            gvMaster.Enabled = false;
            gvDetail.Enabled = false;
            gvCheckOut.Enabled = false;
            txtREMARK.Enabled = false;
            txtUNI_NO.Enabled = false;
            txtUNI_TITLE.Enabled = false;

            gvMaster.FindChildControl<ASPxButton>("btnItemAdd").Enabled = false;
            gvMaster.FindChildControl<ASPxButton>("btnMixPromotion").Enabled = false;
            gvMaster.FindChildControl<ASPxButton>("btnStoreDiscount").Enabled = false;
            gvMaster.FindChildControl<ASPxButton>("btnHappyGoNet").Enabled = false;

            btnConfirm.Enabled = false;
            gvCheckOut.FindChildControl<ASPxButton>("btnCredit").Enabled = false;
            gvCheckOut.FindChildControl<ASPxButton>("btnDivCredit").Enabled = false;
            gvCheckOut.FindChildControl<ASPxButton>("btnOffLineCredit").Enabled = false;
            ScriptManager.RegisterClientScriptBlock(this, typeof(string), "NonExchange", "alert('代收交易不可以透過換貨作業來換貨!');", true);
            return;
        }
        string ETCProdNo = new SAL041_Facade().getAllFETCProuductNo();
        DataRow[] drsETC = TempTables.Detail.Select(" PRODNO in (" + ETCProdNo + ")");
        if (drsETC != null && drsETC.Length > 0)
        {
            //ETC儲值-e通卡交易不可以透過換貨作業來換貨
            btnExchangeGoodsCheckOut.Enabled = false;
            btnExchangeGoodsCancel.Enabled = false;
            gvMaster.Enabled = false;
            gvDetail.Enabled = false;
            gvCheckOut.Enabled = false;
            txtREMARK.Enabled = false;
            txtUNI_NO.Enabled = false;
            txtUNI_TITLE.Enabled = false;

            gvMaster.FindChildControl<ASPxButton>("btnItemAdd").Enabled = false;
            gvMaster.FindChildControl<ASPxButton>("btnMixPromotion").Enabled = false;
            gvMaster.FindChildControl<ASPxButton>("btnStoreDiscount").Enabled = false;
            gvMaster.FindChildControl<ASPxButton>("btnHappyGoNet").Enabled = false;

            btnConfirm.Enabled = false;
            gvCheckOut.FindChildControl<ASPxButton>("btnCredit").Enabled = false;
            gvCheckOut.FindChildControl<ASPxButton>("btnDivCredit").Enabled = false;
            gvCheckOut.FindChildControl<ASPxButton>("btnOffLineCredit").Enabled = false;
            ScriptManager.RegisterClientScriptBlock(this, typeof(string), "ETCMessage", "alert('ETC儲值-e通卡交易不可以透過換貨作業來換貨!');", true);
            return;
        }
        DataRow[] drs = TempTables.Detail.Select(" SOURCE_TYPE = '10' ");
        if ((drs == null || drs.Length == 0) && hidOldTranStatus.Value == "2")
        {   //非網購及要作廢交易狀態為結帳才可以換貨
            string strPOSUUID_DETAIL = "";
            foreach (DataRow dr in TempTables.Detail.Rows)
            {
                strPOSUUID_DETAIL += StringUtil.CStr(dr["POSUUID_DETAIL"]) + ";";
            }
            LoadNewData(strPOSUUID_DETAIL, _PKEY); //載入未結清單資料(新交易)
            //商品料號為SIM，要提醒使用者選取真實的商品料號
            DataRow[] drsSIM = TempTables.Detail.Select(" ITEM_STATUS <> '作廢' And PRODNO = 'SIM' ");
            if (drsSIM != null && drsSIM.Length > 0)
            {
                ScriptManager.RegisterClientScriptBlock(this, typeof(string), "SIMPROD", "alert('請選取實際的SIM卡商品料號!');", true);
            }
            gvMaster.DataSource = TempTables.Detail;
            gvMaster.DataBind();
            gvDetail.DataSource = TempTables.Discount;
            gvDetail.DataBind();
            gvCheckOut.DataSource = TempTables.Paid;
            gvCheckOut.DataBind();
            Session["TempTable"] = TempTables;
            //SALE_IMEI資料動態才給
            TempTables.calTotalAmount(this);   //計算金額
            TempTables.SaleHeadDataBind(this); //顯示表頭資訊
            TempTables.Mode_HavePROD(this);
        }
        else if (hidOldTranStatus.Value != "2")
        {   //作廢交易不可以換貨
            btnExchangeGoodsCheckOut.Enabled = false;
            btnExchangeGoodsCancel.Enabled = false;
            gvMaster.Enabled = false;
            gvDetail.Enabled = false;
            gvCheckOut.Enabled = false;
            txtREMARK.Enabled = false;
            txtUNI_NO.Enabled = false;
            txtUNI_TITLE.Enabled = false;

            gvMaster.FindChildControl<ASPxButton>("btnItemAdd").Enabled = false;
            gvMaster.FindChildControl<ASPxButton>("btnMixPromotion").Enabled = false;
            gvMaster.FindChildControl<ASPxButton>("btnStoreDiscount").Enabled = false;
            gvMaster.FindChildControl<ASPxButton>("btnHappyGoNet").Enabled = false;

            btnConfirm.Enabled = false;
            gvCheckOut.FindChildControl<ASPxButton>("btnCredit").Enabled = false;
            gvCheckOut.FindChildControl<ASPxButton>("btnDivCredit").Enabled = false;
            gvCheckOut.FindChildControl<ASPxButton>("btnOffLineCredit").Enabled = false;
            ScriptManager.RegisterClientScriptBlock(this, typeof(string), "NonExchange", "alert('作廢交易不可以透過換貨作業來換貨!');", true);
        }
        else
        {   //網購不可以換貨
            btnExchangeGoodsCheckOut.Enabled = false;
            btnExchangeGoodsCancel.Enabled = false;
            gvMaster.Enabled = false;
            gvDetail.Enabled = false;
            gvCheckOut.Enabled = false;
            txtREMARK.Enabled = false;
            txtUNI_NO.Enabled = false;
            txtUNI_TITLE.Enabled = false;

            gvMaster.FindChildControl<ASPxButton>("btnItemAdd").Enabled = false;
            gvMaster.FindChildControl<ASPxButton>("btnMixPromotion").Enabled = false;
            gvMaster.FindChildControl<ASPxButton>("btnStoreDiscount").Enabled = false;
            gvMaster.FindChildControl<ASPxButton>("btnHappyGoNet").Enabled = false;

            btnConfirm.Enabled = false;
            gvCheckOut.FindChildControl<ASPxButton>("btnCredit").Enabled = false;
            gvCheckOut.FindChildControl<ASPxButton>("btnDivCredit").Enabled = false;
            gvCheckOut.FindChildControl<ASPxButton>("btnOffLineCredit").Enabled = false;
            ScriptManager.RegisterClientScriptBlock(this, typeof(string), "NonExchange", "alert('網購不可以透過換貨作業來換貨!');", true);
        }
    }

    /// <summary>
    /// 載入空資料
    /// </summary>
    void LoadEmptyData()
    {
        LoadInvalData(""); //載入需作廢資料
        string strPOSUUID_DETAIL = "";
        foreach (DataRow dr in TempTables.Detail.Rows)
        {
            strPOSUUID_DETAIL += StringUtil.CStr(dr["POSUUID_DETAIL"]) + ";";
        }
        LoadNewData("", ""); //載入未結清單資料(新交易)

        gvMaster.DataSource = TempTables.Detail;
        gvMaster.DataBind();
        gvDetail.DataSource = TempTables.Discount;
        gvDetail.DataBind();
        gvCheckOut.DataSource = TempTables.Paid;
        gvCheckOut.DataBind();
        Session["TempTable"] = TempTables;
        lblReturnAmount.Text = "0";
        lbChange.Text = "0";
        TempTables.Mode_NoPROD(this);
    }

    /// <summary>
    /// 載入Cache 資料 當不正常關閉時且已產生金額資料時 回載原資料
    /// </summary>
    void LoadFromCache(string POSUUID_MASTER)
    {
        TempTables.Head = Facade01.getSale_Head(POSUUID_MASTER);
        string InvalPOSUUID_MASTER = StringUtil.CStr(TempTables.Head.Rows[0]["INVALID_ID"]);  //作廢的ID
        LoadInvalData(InvalPOSUUID_MASTER); //載入需作廢資料
        string strPOSUUID_DETAIL = "";
        foreach (DataRow dr in TempTables.Detail.Rows)
        {
            strPOSUUID_DETAIL += StringUtil.CStr(dr["POSUUID_DETAIL"]) + ";";
        }

        LoadCacheData(strPOSUUID_DETAIL, POSUUID_MASTER);  //載入不正常關閉的Cache資料

        DataTable dtPaid = Facade03.getCachePaid_Detail(POSUUID_MASTER);
        //將清單的付費方式資訊放在最後列
        if (dtPaid.Columns.IndexOf("ITEM_STATUS") < 0)
            dtPaid.Columns.Add("ITEM_STATUS");
        foreach (DataRow PaidRow in dtPaid.Rows)
        {
            PaidRow["ITEM_STATUS"] = "";
            dtPaid.AcceptChanges();
        }
        TempTables.Paid.Merge(dtPaid);
        TempTables.Paid.AcceptChanges();
        //TempTables.IMEI_LOG = Facade03.getSale_IMEI_LOG("");


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
        TempTables.calTotalAmount(this);    //計算金額
        TempTables.CalculationTax(this);
        TempTables.SaleHeadDataBind(this);  //顯示表頭資訊

        this.hdHG_REDEEM_POINT.Text = StringUtil.CStr(TempTables.Detail.Compute("Sum(HG_REDEEM_POINT)", "ITEM_TYPE IN ('4','14') AND ITEM_STATUS <> '作廢'"));  //總兌換點數
        this.hdTOTAL_AMOUNT.Text = StringUtil.CStr(TempTables.Detail.Compute("Sum(TOTAL_AMOUNT)", "ITEM_TYPE = '4' AND ITEM_STATUS <> '作廢'"));        //總兌點金額
        DataRow[] drCardNo = TempTables.Detail.Select("ITEM_TYPE IN ('4','14') AND ITEM_STATUS <> '作廢'");
        this.hdHG_CARD_NO.Text = drCardNo.Length <= 0 ? "" : StringUtil.CStr(drCardNo[0]["HG_CARD_NO"]);               //HG卡號

        //cache時一定是在付款模式
        TempTables.Mode_PayCash(this);
        TempTables.calPayTotal_Change(this);
        if (lbPayAmount.Text == "0" && TempTables.Paid.Rows.Count > 0)
            btnExchangeGoodsCheckOut.Enabled = true;
    }

    /// <summary>
    /// 載入未結清單資料(新交易)
    /// <param name="POSUUID_DETAIL">POSUUID_DETAIL</param>
    /// </summary>
    void LoadNewData(string POSUUID_DETAIL, string INVALID_ID)
    {
        DataTable dtHead = Facade03.getTO_CLOSE_HEAD(POSUUID_DETAIL);
        TempTables.Head = dtHead.Copy();
        Double totAmt = 0;
        if (TempTables.Head != null && TempTables.Head.Rows.Count > 0)
        {
            string POSUUID_MASTER = StringUtil.CStr(Advtek.Utility.GuidNo.getUUID());
            foreach (DataRow headRow in TempTables.Head.Rows)
            {
                headRow["TRADE_DATE"] = OracleDBUtil.WorkDay(logMsg.STORENO);
                headRow["POSUUID_MASTER"] = POSUUID_MASTER;//先取出MASTER_UUID
                headRow["INVALID_ID"] = INVALID_ID; //對應作廢的POSUUID_MASTER
            }
            TempTables.Head.AcceptChanges();
            //將目前POSUUID_MASTER回填TO_CLOSE_HEAD,狀態設為未結
            Facade01.UpdateUnCloseHead(TempTables.Head, "1", logMsg.MODI_USER); 
        }
        else
        {
            DataRow Newdr = TempTables.Head.NewRow();
            Newdr["TRADE_DATE"] = Advtek.Utility.OracleDBUtil.WorkDay(logMsg.STORENO);
            Newdr["POSUUID_MASTER"] = StringUtil.CStr(Advtek.Utility.GuidNo.getUUID());
            Newdr["SALE_TOTAL_AMOUNT"] = 0;
            Newdr["SALE_BEFORE_TAX"] = 0;
            Newdr["SALE_TAX"] = 0;
            Newdr["DISCOUNT_TOTAL_AMOUNT"] = 0;
            Newdr["DISCOUNT_BEFORE_TAX"] = 0;
            Newdr["DISCOUNT_TAX"] = 0;

            Newdr["SALE_TYPE"] = "1";
            Newdr["SALE_STATUS"] = "7"; //換貨未結帳
            Newdr["INVALID_ID"] = INVALID_ID;   //對應作廢的POSUUID_MASTER
            Newdr["VOUCHER_TYPE"] = "1";
            TempTables.Head.Rows.Add(Newdr);
            TempTables.Head.AcceptChanges();
        }

        //取得表身資料取得多筆 未結清表身資料  1-2 ,1-3,1-1 取得 2+3+1 = 5筆資料
        DataTable dtItem = Facade03.getTO_CLOSE_ITEM_For_Exchange(POSUUID_DETAIL, "2", logMsg.STORENO, this._STOCK);//未結清單表身
        string strNewPOSUUID_DETAIL = "";
        if (dtItem != null && dtItem.Rows.Count > 0)
        {
            foreach (DataRow dr in dtItem.Rows)
            {
                if (!string.IsNullOrEmpty(StringUtil.CStr(dr["POSUUID_DETAIL"])))
                {
                    strNewPOSUUID_DETAIL += "'" + StringUtil.CStr(dr["POSUUID_DETAIL"]) + "',";
                }
            }
        }
        else if (dtHead != null && dtHead.Rows.Count > 0)
        {   //取消促代，又有合併結帳的狀況下，取消促代會走換貨作業這條路，取消促代的單子，不可再帶入新交易中
            strNewPOSUUID_DETAIL = "'" + StringUtil.CStr(dtHead.Rows[0]["POSUUID_DETAIL"]) + "',";
        }

        if (TempTables.Detail != null && TempTables.Detail.Rows.Count > 0) //如果有作廢資料
        {
            OracleConnection objConn = OracleDBUtil.GetConnection();
            OracleTransaction objTX = objConn.BeginTransaction();
            string strWhere = "";
            bool insertIMEI = true;
            if (!string.IsNullOrEmpty(strNewPOSUUID_DETAIL))
            {
                strWhere = "ITEM_TYPE NOT IN ('6','4','7','12','13','14','15','16','17') And POSUUID_DETAIL NOT IN (" + strNewPOSUUID_DETAIL.Substring(0, strNewPOSUUID_DETAIL.Length - 1) + ")";
            }
            else
            {
                strWhere = "ITEM_TYPE NOT IN ('6','4','7','12','13','14','15','16','17')";  //門市特殊抱怨折扣, HG折抵, HG加價購不帶入, 單品加價購, 贈品不帶入
            }

            //若TO_CLOSE(未結清單)有異動的POSUUID_DETAIL，除了將異動的項目加至新交易中，還要從SALE補上沒有異動的POSUUID_DETAIL
            DataRow[] drTemp = TempTables.Detail.Select(strWhere, "SEQNO");
            foreach (DataRow ItemRow in drTemp)
            {
                DataRow NewRow = TempTables.Detail.NewRow();
                NewRow.ItemArray = ItemRow.ItemArray;
                NewRow["ITEM_STATUS"] = "";
                NewRow["POSUUID_MASTER"] = TempTables.Head.Rows[0]["POSUUID_MASTER"];
                NewRow["ID"] = StringUtil.CStr(Advtek.Utility.GuidNo.getUUID());
                NewRow["IMEI_QTY"] = 0;
                int ItemRowsCount = TempTables.Detail.Rows.Count;
                TempTables.Detail.Rows.InsertAt(NewRow, ItemRowsCount);
                if (NewRow["TOTAL_AMOUNT"] != null && StringUtil.CStr(NewRow["TOTAL_AMOUNT"]) != ""
                    && NumberUtil.IsNumeric(StringUtil.CStr(NewRow["TOTAL_AMOUNT"])))
                    totAmt += Convert.ToDouble(NewRow["TOTAL_AMOUNT"]);
                //如果原先交易明細有綁IMEI,則將舊的IMEI,連同新的SALE_DETAIL_ID,新增一筆到SALE_IMEI_LOG資料表中,狀態為7,換貨未結
                if (insertIMEI)
                {
                    int ret = Facade03.ADD_SALE_IMEI_Log(StringUtil.CStr(NewRow["ID"]), StringUtil.CStr(ItemRow["ID"]), logMsg.CREATE_USER, logMsg.MODI_USER, objTX);
                    if (ret < 0)
                        insertIMEI = false;
                }
                TempTables.Detail.AcceptChanges();
            }

            //將TO_CLOSE(未結清單)有異動的POSUUID_DETAIL新交易資訊放在最後列
            if (dtItem != null && dtItem.Rows.Count > 0 && dtItem.Columns.IndexOf("ITEM_STATUS") < 0)
                dtItem.Columns.Add("ITEM_STATUS");
            if (dtItem != null && dtItem.Rows.Count > 0 && dtItem.Columns.IndexOf("POSUUID_MASTER") < 0)
                dtItem.Columns.Add("POSUUID_MASTER");

            if (dtItem != null && dtItem.Rows.Count > 0)
            {
                foreach (DataRow ItemRow in dtItem.Rows)
                {
                    ItemRow["ITEM_STATUS"] = "";
                    ItemRow["POSUUID_MASTER"] = TempTables.Head.Rows[0]["POSUUID_MASTER"];
                    if (ItemRow["TOTAL_AMOUNT"] != null && StringUtil.CStr(ItemRow["TOTAL_AMOUNT"]) != ""
                        && NumberUtil.IsNumeric(StringUtil.CStr(ItemRow["TOTAL_AMOUNT"])))
                        totAmt += Convert.ToDouble(ItemRow["TOTAL_AMOUNT"]);
                    if (insertIMEI)
                    {
                        int ret = Facade03.ADD_SALE_IMEI_Log(StringUtil.CStr(ItemRow["ID"]), StringUtil.CStr(ItemRow["POSUUID_DETAIL"]),
                                                                StringUtil.CStr(ItemRow["PRODNO"]), logMsg.CREATE_USER, logMsg.MODI_USER, objTX);
                        if (ret < 0)
                            insertIMEI = false;
                    }
                    dtItem.AcceptChanges();
                }
            }

            if (insertIMEI)
                objTX.Commit();
            TempTables.Detail.Merge(dtItem);
            TempTables.Detail.AcceptChanges();
        }
        else
        {
            if (dtItem != null && dtItem.Rows.Count > 0)
            {
                TempTables.Detail = dtItem;
                foreach (DataRow dr in TempTables.Detail.Rows)
                {
                    dr["POSUUID_MASTER"] = TempTables.Head.Rows[0];
                    if (dr["TOTAL_AMOUNT"] != null && StringUtil.CStr(dr["TOTAL_AMOUNT"]) != ""
                        && NumberUtil.IsNumeric(StringUtil.CStr(dr["TOTAL_AMOUNT"])))
                        totAmt += Convert.ToDouble(dr["TOTAL_AMOUNT"]);
                }
            }
        }

        DataTable dtHGTable = null;
        
        if (TempTables.Detail.Select("ITEM_STATUS <> '作廢'") != null && TempTables.Detail.Select("ITEM_STATUS <> '作廢'").Length > 0)
            dtHGTable = TempTables.Detail.Select("ITEM_STATUS <> '作廢'").CopyToDataTable();

        Session["HGDISTempTable"] = dtHGTable; //for HappyGo折抵時，查詢【促銷商品折抵活動】，以及【單商品折抵活動】使用。

        //有促銷商品，組組合促銷商品清單，供組合促銷使用
        DataRow[] drsPromotion = TempTables.Detail.Select(" ITEM_STATUS <> '作廢' AND Promotion_Code is not null ");
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

        //SSI 呼叫換貨作業時不會帶入Discount相關資料給 TO_CLOSE_DISCOUNT, 需手動新增
        string[] posuuid_detailList = POSUUID_DETAIL.Split(new char[] { ';' });
        string prePossuid_Detail = "";
        if (posuuid_detailList != null && posuuid_detailList.Length > 0)
        {
            for (int i = 0; i < posuuid_detailList.Length; i++)
            {
                if (prePossuid_Detail == "" || prePossuid_Detail != posuuid_detailList[i])
                {
                    Discount_Facade.CreateDiscount(posuuid_detailList[i]);
                    prePossuid_Detail = posuuid_detailList[i];
                }
            }
        }
        
        //取得表身資料取得多筆 未結清表身資料  1-2 ,1-3,1-1 取得 2+3+1 = 5筆資料
        DataTable dtDiscount = Facade03.getTO_CLOSE_ITEM_For_Exchange(POSUUID_DETAIL, "5", logMsg.STORENO, this._STOCK);//未結折扣資料
        strNewPOSUUID_DETAIL = "";
        if (dtDiscount != null && dtDiscount.Rows.Count > 0)
        {
            foreach (DataRow dr in dtDiscount.Rows)
            {
                string discountType = Facade01.getDISCOUNT_TYPE(StringUtil.CStr(dr["PRODNO"]));
                if (discountType == "2")
                    dr["ITEM_TYPE"] = "12"; //舊機回收折扣
                else if (discountType == "3")
                    dr["ITEM_TYPE"] = "11"; //租賃折扣
                dr["ITEM_STATUS"] = "";
                dr["POSUUID_MASTER"] = TempTables.Head.Rows[0]["POSUUID_MASTER"];
                if (!string.IsNullOrEmpty(StringUtil.CStr(dr["POSUUID_DETAIL"])))
                {
                    strNewPOSUUID_DETAIL += "'" + StringUtil.CStr(dr["POSUUID_DETAIL"]) + "',";
                }
            }
        }

        if (TempTables.Discount != null && TempTables.Discount.Rows.Count > 0) //如果有作廢資料
        {
            string strWhere = "";
            if (!string.IsNullOrEmpty(strNewPOSUUID_DETAIL))
            {
                strWhere = "POSUUID_DETAIL NOT IN (" + strNewPOSUUID_DETAIL.Substring(0, strNewPOSUUID_DETAIL.Length - 1) + ")";
            }
            else
            {
                strWhere = "1 <> 1";
            }

            ////若TO_CLOSE(未結清單)有異動的POSUUID_DETAIL，除了將異動的項目加至新交易中，還要從SALE補上沒有異動的POSUUID_DETAIL
            //DataRow[] drTemp = TempTables.Discount.Select(strWhere);
            //foreach (DataRow discountRow in drTemp)
            //{
            //    DataRow NewRow = TempTables.Discount.NewRow();
            //    NewRow.ItemArray = discountRow.ItemArray;
            //    NewRow["ITEM_STATUS"] = "";
            //    NewRow["POSUUID_MASTER"] = TempTables.Head.Rows[0]["POSUUID_MASTER"];
            //    NewRow["ID"] = StringUtil.CStr(Advtek.Utility.GuidNo.getUUID());
            //    NewRow["IMEI_QTY"] = 0;
            //    int discountRowsCount = TempTables.Discount.Rows.Count;
            //    TempTables.Discount.Rows.InsertAt(NewRow, discountRowsCount);
            //    totAmt += Convert.ToDouble(NewRow["TOTAL_AMOUNT"]);
            //    TempTables.Discount.AcceptChanges();
            //}

            //將TO_CLOSE(未結清單)有異動的POSUUID_DETAIL新交易資訊放在最後列
            if (dtDiscount != null && dtDiscount.Rows.Count > 0)
            {
                TempTables.Discount.Merge(dtDiscount);
                foreach (DataRow discountRow in dtDiscount.Rows)
                {
                    if (discountRow["TOTAL_AMOUNT"] != null && StringUtil.CStr(discountRow["TOTAL_AMOUNT"]) != ""
                        && NumberUtil.IsNumeric(StringUtil.CStr(discountRow["TOTAL_AMOUNT"])))
                        totAmt += Convert.ToDouble(discountRow["TOTAL_AMOUNT"]);
                }
            }
        }
        else
        {
            if (dtDiscount != null && dtDiscount.Rows.Count > 0)
            {
                TempTables.Discount = dtDiscount;
                foreach (DataRow dr in TempTables.Discount.Rows)
                {
                    if (dr["TOTAL_AMOUNT"] != null && StringUtil.CStr(dr["TOTAL_AMOUNT"]) != ""
                            && NumberUtil.IsNumeric(StringUtil.CStr(dr["TOTAL_AMOUNT"])))
                        totAmt += Convert.ToDouble(dr["TOTAL_AMOUNT"]);
                }
            }
        }
      
        TempTables.Discount.AcceptChanges();
        lbTOTAL_AMOUNT.Text = StringUtil.CStr(totAmt);

        gvDetail.Visible = true;                        //只有未結而來折扣資料才會放在這裏 未結折扣資料
    }

    /// <summary>
    /// 載入需作廢資料
    /// <param name="POSUUID_MASTER">POSUUID_MASTER</param>
    /// </summary>
    void LoadInvalData(string POSUUID_MASTER)
    {
        TempTables.Detail = Facade03.getSale_Detail(POSUUID_MASTER, "1,2,3,4,6,7,8,9,10,13", logMsg.STORENO, this._STOCK);  //舊交易的明細
        TempTables.Discount = Facade03.getSale_Detail(POSUUID_MASTER, "5,11,12", logMsg.STORENO, this._STOCK);
        TempTables.Paid = Facade03.getPaid_Detail(POSUUID_MASTER);                 //舊交易的付費資訊
       // TempTables.IMEI_LOG = Facade03.getSale_IMEI_LOG("");                       //手機的IMEI資料，為空
        DataTable dt = Facade03.getSale_Head(POSUUID_MASTER);

        if (dt != null && dt.Rows.Count > 0)
        {
            this.lblOriginalTOTAL_AMOUNT.Text = StringUtil.CStr(dt.Rows[0]["SALE_TOTAL_AMOUNT"]);  //原交易金額
            hidSALE_TYPE.Text = StringUtil.CStr(dt.Rows[0]["SALE_TYPE"]);
            hidOldTranStatus.Value = StringUtil.CStr(dt.Rows[0]["SALE_STATUS"]);
        }
        else
        {
            this.lblOriginalTOTAL_AMOUNT.Text = "0";
            hidSALE_TYPE.Text = "1";
        }
        DataTable dtInv = new SAL041_Facade().GetINVOICE(this._PKEY);
        if (dtInv.Rows.Count > 0)
        {
            DateTime transDate = DateTime.Now;
            int S_MM = 0;   //期數的起
            int E_MM = 0;   //期數的迄
            transDate = DateTime.Parse(StringUtil.CStr(dtInv.Rows[0]["INVOICE_DATE"]).Trim());
            if (transDate.Year != DateTime.Now.Year)
            {
                hidIsInv.Value = "Y";
                hidIsDebit.Value = "Y";
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
                    hidIsInv.Value = "Y";
                }
                else
                {
                    hidIsInv.Value = "Y";
                    hidIsDebit.Value = "Y";
                }
            }
        }
    }

    /// <summary>
    /// 載入不正常關閉的Cache資料
    /// </summary>
    /// <param name="POSUUID_DETAIL">POSUUID_DETAIL</param>
    void LoadCacheData(string POSUUID_DETAIL)
    {
        if (!string.IsNullOrEmpty(POSUUID_DETAIL))
        {
            Double totAmt = 0;
            //取得表身資料取得多筆 未結清表身資料  1-2 ,1-3,1-1 取得 2+3+1 = 5筆資料
            DataTable dtItem = Facade03.getCacheSale_Detail(_PKEY, "'1','2','3','4','6','7','8','9','10','12','13','14','15','16','17'", logMsg.STORENO, this._STOCK);//清單表身

            if (TempTables.Detail.Rows.Count > 0) //如果有作廢資料
            {
                //將清單中的POSUUID_DETAIL新交易資訊放在最後列
                if (dtItem.Columns.IndexOf("ITEM_STATUS") < 0)
                    dtItem.Columns.Add("ITEM_STATUS");
                foreach (DataRow ItemRow in dtItem.Rows)
                {
                    ItemRow["ITEM_STATUS"] = "";
                    totAmt += Convert.ToDouble(ItemRow["TOTAL_AMOUNT"]);
                    dtItem.AcceptChanges();
                }
                TempTables.Detail.Merge(dtItem);
                TempTables.Detail.AcceptChanges();
            }
            else
            {
                TempTables.Detail = dtItem;
            }

            if (TempTables.Detail.Select("ITEM_STATUS <> '作廢'") != null && TempTables.Detail.Select("ITEM_STATUS <> '作廢'").Length > 0)
            {
                DataTable dtHGTable = TempTables.Detail.Select("ITEM_STATUS <> '作廢'").CopyToDataTable();

                Session["HGDISTempTable"] = dtHGTable; //for HappyGo折抵時，查詢【促銷商品折抵活動】，以及【單商品折抵活動】使用。
            }
            else
            {
                Session["HGDISTempTable"] = null;
            }

            TempTables.Discount = Facade03.getCacheSale_Detail(_PKEY, "'5','11'", logMsg.STORENO, this._STOCK);//折扣資料   
            if (TempTables.Discount != null && TempTables.Discount.Rows.Count > 0)
            {
                foreach (DataRow dr in TempTables.Discount.Rows)
                {
                    dr["POSUUID_MASTER"] = TempTables.Head.Rows[0];
                    totAmt += Convert.ToDouble(dr["TOTAL_AMOUNT"]);
                }
                TempTables.Discount.AcceptChanges();
            }
            lbTOTAL_AMOUNT.Text = StringUtil.CStr(totAmt);
            Session["TempTable"] = TempTables;
            gvDetail.Visible = true;                        //只有未結而來折扣資料才會放在這裏 未結折扣資料
            TempTables.calTotalAmount(this);
            TempTables.CalculationTax(this);
            TempTables.SaleHeadDataBind(this);
            //cache時一定是在付款模式
            TempTables.Mode_PayCash(this);
            TempTables.calPayTotal_Change(this);
            if (lbPayAmount.Text == "0" && TempTables.Paid.Rows.Count > 0)
                btnExchangeGoodsCheckOut.Enabled = true;
            if (TempTables.Discount != null && TempTables.Discount.Rows.Count > 0)
                gvDetail.Visible = true;
        }
    }

    /// <summary>
    /// 載入不正常關閉的Cache資料
    /// </summary>
    /// <param name="POSUUID_DETAIL">POSUUID_DETAIL</param>
    void LoadCacheData(string POSUUID_DETAIL, string POSUUID_MASTER)
    {
        if (!string.IsNullOrEmpty(POSUUID_DETAIL))
        {
            Double totAmt = 0;
            //取得表身資料取得多筆 未結清表身資料  1-2 ,1-3,1-1 取得 2+3+1 = 5筆資料
            DataTable dtItem = Facade03.getCacheSale_Detail(POSUUID_MASTER, "'1','2','3','4','6','7','8','9','10','12','13','14','15','16','17'", logMsg.STORENO, this._STOCK);//清單表身

            if (TempTables.Detail != null && TempTables.Detail.Rows.Count > 0) //如果有作廢資料
            {
                //將清單中的POSUUID_DETAIL新交易資訊放在最後列
                if (dtItem.Columns.IndexOf("ITEM_STATUS") < 0)
                    dtItem.Columns.Add("ITEM_STATUS");
                foreach (DataRow ItemRow in dtItem.Rows)
                {
                    ItemRow["ITEM_STATUS"] = "";
                    totAmt += Convert.ToDouble(ItemRow["TOTAL_AMOUNT"]);
                    dtItem.AcceptChanges();
                }
                TempTables.Detail.Merge(dtItem);
                TempTables.Detail.AcceptChanges();
            }
            else
            {
                TempTables.Detail = dtItem;
                foreach (DataRow dr in TempTables.Detail.Rows)
                {
                    dr["POSUUID_MASTER"] = TempTables.Head.Rows[0];
                    totAmt += Convert.ToDouble(dr["TOTAL_AMOUNT"]);
                }
            }
            TempTables.Detail.AcceptChanges();

            if (TempTables.Detail.Select("ITEM_STATUS <> '作廢'") != null && TempTables.Detail.Select("ITEM_STATUS <> '作廢'").Length > 0)
            {
                DataTable dtHGTable = TempTables.Detail.Select("ITEM_STATUS <> '作廢'").CopyToDataTable();

                Session["HGDISTempTable"] = dtHGTable; //for HappyGo折抵時，查詢【促銷商品折抵活動】，以及【單商品折抵活動】使用。
            }
            else
            {
                Session["HGDISTempTable"] = null;
            }

            //有促銷商品，組組合促銷商品清單，供組合促銷使用
            DataRow[] drsPromotion = TempTables.Detail.Select(" ITEM_STATUS <> '作廢' AND Promotion_Code is not null ", " Promotion_Code, SEQNO");
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

            //取得表身資料取得多筆 未結清表身資料  1-2 ,1-3,1-1 取得 2+3+1 = 5筆資料
            DataTable dtDiscount = Facade03.getCacheSale_Detail(POSUUID_MASTER, "'5','11'", logMsg.STORENO, this._STOCK);//清單表身

            if (TempTables.Discount != null && TempTables.Discount.Rows.Count > 0) //如果有作廢資料
            {
                //將清單中的POSUUID_DETAIL新交易資訊放在最後列
                if (dtDiscount != null && dtDiscount.Columns.Count > 0 && dtDiscount.Columns.IndexOf("ITEM_STATUS") < 0)
                    dtDiscount.Columns.Add("ITEM_STATUS");
                if (dtDiscount != null && dtDiscount.Rows.Count > 0)
                {
                    foreach (DataRow DiscountRow in dtDiscount.Rows)
                    {
                        DiscountRow["ITEM_STATUS"] = "";
                        totAmt += Convert.ToDouble(DiscountRow["TOTAL_AMOUNT"]);
                        dtDiscount.AcceptChanges();
                    }
                    TempTables.Discount.Merge(dtDiscount);
                    TempTables.Discount.AcceptChanges();
                }
            }
            else
            {
                if (dtDiscount != null && dtDiscount.Rows.Count > 0)
                {
                    TempTables.Discount = dtDiscount;
                    foreach (DataRow dr in TempTables.Discount.Rows)
                    {
                        dr["POSUUID_MASTER"] = TempTables.Head.Rows[0];
                        totAmt += Convert.ToDouble(dr["TOTAL_AMOUNT"]);
                    }
                }
            }
            TempTables.Discount.AcceptChanges();

            lbTOTAL_AMOUNT.Text = StringUtil.CStr(totAmt);
            Session["TempTable"] = TempTables;
            gvDetail.Visible = true;                        //只有未結而來折扣資料才會放在這裏 未結折扣資料
            TempTables.calTotalAmount(this);
            TempTables.CalculationTax(this);
            TempTables.SaleHeadDataBind(this);
            //cache時一定是在付款模式
            TempTables.Mode_PayCash(this);
            TempTables.calPayTotal_Change(this);
            if (lbPayAmount.Text == "0" && TempTables.Paid.Rows.Count > 0)
                btnExchangeGoodsCheckOut.Enabled = true;
            DataTable dtDetail = new DataTable();
            DataRow[] drs = TempTables.Detail.Select(" ITEM_STATUS = '作廢' ", "ITEMS");
            if (drs != null && drs.Length > 0)
                dtDetail.Merge(drs.CopyToDataTable());
            drs = TempTables.Detail.Select(" ITEM_STATUS <> '作廢' ", "ITEMS");
            if (drs != null && drs.Length > 0)
                dtDetail.Merge(drs.CopyToDataTable());
            TempTables.Detail = dtDetail;

            DataTable dtInv = new SAL041_Facade().GetINVOICE(StringUtil.CStr(TempTables.Head.Rows[0]["INVALID_ID"]));
            if (dtInv.Rows.Count > 0)
            {
                DateTime transDate = DateTime.Now;
                int S_MM = 0;   //期數的起
                int E_MM = 0;   //期數的迄
                transDate = DateTime.Parse(StringUtil.CStr(dtInv.Rows[0]["INVOICE_DATE"]).Trim());
                if (transDate.Year != DateTime.Now.Year)
                {
                    hidIsInv.Value = "Y";
                    hidIsDebit.Value = "Y";
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
                        hidIsInv.Value = "Y";
                    }
                    else
                    {
                        hidIsInv.Value = "Y";
                        hidIsDebit.Value = "Y";
                    }
                }
            }
        }
    }
    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack && !IsCallback)
        {
            //Session["EXCHANGE_SRC_TYPE"] = Request.QueryString["SRC_TYPE"];
            //if (Request.QueryString["PKEY"] != null)
            //    Session["ExchangeOrderPKEY"] = Request.QueryString["PKEY"];

            //**2011/04/21 Tina：前頁傳遞參數時，會先以加密處理，在此要解密。
            if (!string.IsNullOrEmpty(Request.QueryString["Param"]))
            {
                NameValueCollection qscoll = Utils.Param_UrlDecode(Request.QueryString["Param"]);
                foreach (string key in qscoll.AllKeys)
                {
                    if (key == "SRC_TYPE")
                    {
                        Session["EXCHANGE_SRC_TYPE"] = string.Join(",", qscoll.GetValues(key));
                    }
                    else if (key == "PKEY")
                    {
                        Session["ExchangeOrderPKEY"] = string.Join(",", qscoll.GetValues(key));
                    }
                }
            }
            else
            {
                Session["EXCHANGE_SRC_TYPE"] = null;
                Session["ExchangeOrderPKEY"] = null;
            }
            TempTables.Detail = null;
            TempTables.Discount = null;
            this.hidStore_No.Value = logMsg.STORENO;
            InvPrintName.Value = System.Configuration.ConfigurationManager.AppSettings["Invoice_PDFPrinterName"].ToString();//web.config中設定
            ReceiptPrintName.Value = System.Configuration.ConfigurationManager.AppSettings["Receipt_PDFPrinterName"].ToString();//web.config中設定
            DebitPrintName.Value = System.Configuration.ConfigurationManager.AppSettings["Credit_PDFPrinterName"].ToString();//web.config中設定
            //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "PrintInvoice", " Call_DetectPrinterName();", true);
            ScriptManager.RegisterStartupScript(this, this.GetType(), "PrintInvoice", " Call_DetectPrinterName();", true);

            //已付款但未完成結帳,將重新載入最後一次未正常結帳資料
            DataTable CacheDT = new SAL03_Facade().CheckExchangeCacheData(logMsg.STORENO, logMsg.OPERATOR);
            if (CacheDT.Rows.Count == 0)
            {
                LoadData1();
            }
            else //已付款但未正常結帳資料
            {
                Session["ExchangeOrderPKEY"] = StringUtil.CStr(CacheDT.Rows[0]["POSUUID_MASTER"]);
                LoadFromCache(this._PKEY);
                this.hdHG_REDEEM_POINT.Text = StringUtil.CStr(TempTables.Detail.Compute("Sum(HG_REDEEM_POINT)", "ITEM_TYPE IN ('4','14')"));  //總兌換點數
                this.hdTOTAL_AMOUNT.Text = StringUtil.CStr(TempTables.Detail.Compute("Sum(TOTAL_AMOUNT)", "ITEM_TYPE = '4'"));        //總兌點金額
                DataRow[] drCardNo = TempTables.Detail.Select("ITEM_TYPE IN ('4','14') ");
                this.hdHG_CARD_NO.Text = drCardNo.Length <= 0 ? "" : StringUtil.CStr(drCardNo[0]["HG_CARD_NO"]);               //HG卡號
                btnConfirm.Enabled = false;
                ScriptManager.RegisterClientScriptBlock(this, typeof(string), "LoadCache", "alert('系統偵測發現有已付款但未正常結帳資料,將載入換貨未結帳資料!');", true);
                Session["EXCHANGE_SRC_TYPE"] = "Cache";
            }
        }
    }

    protected void ASPxButton1_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/VSS/SAL/SAL02/SAL02.aspx");
    }

    protected void btnItemAdd_Click(object sender, EventArgs e)
    {
        TempTables.SaveTempTable(this);
        TempTables.NewRow_Detail(gvMaster);
        DataTable dtHGTable = null;
        if (TempTables.Detail.Select("ITEM_STATUS <> '作廢'") != null && TempTables.Detail.Select("ITEM_STATUS <> '作廢'").Length > 0)
            dtHGTable = TempTables.Detail.Select("ITEM_STATUS <> '作廢'").CopyToDataTable();
        Session["HGDISTempTable"] = dtHGTable; //for HappyGo折抵時，查詢【促銷商品折抵活動】，以及【單商品折抵活動】使用。
        TempTables.Mode_HavePROD(this);

        PopupControl txtPRODNO = gvMaster.FindRowCellTemplateControl(TempTables.Detail.Rows.Count - 1, (GridViewDataColumn)gvMaster.Columns["PRODNO"], "txtPRODNO") as PopupControl;
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

    protected void btnStoreDiscount_Click(object sender, EventArgs e)
    {
        TempTables.SaveTempTable(this);
        string[] args = __EVENTARGUMENT.Split(new char[] { ',' });
        TempTables.NewRow_Detail_StoreDiscount(this, gvMaster, args);
        DataTable dtHGTable = null;
        if (TempTables.Detail != null && TempTables.Detail.Rows.Count > 0 && TempTables.Detail.Select("ITEM_STATUS <> '作廢'") != null && 
            TempTables.Detail.Select("ITEM_STATUS <> '作廢'").Length > 0)
            dtHGTable = TempTables.Detail.Select("ITEM_STATUS <> '作廢'").CopyToDataTable();
        Session["HGDISTempTable"] = dtHGTable; //for HappyGo折抵時，查詢【促銷商品折抵活動】，以及【單商品折抵活動】使用。
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
        DataTable dtHGTable = null;
        if (TempTables.Detail != null && TempTables.Detail.Rows.Count > 0 && TempTables.Detail.Select("ITEM_STATUS <> '作廢'") != null && 
            TempTables.Detail.Select("ITEM_STATUS <> '作廢'").Length > 0)
            dtHGTable = TempTables.Detail.Select("ITEM_STATUS <> '作廢'").CopyToDataTable();
        Session["HGDISTempTable"] = dtHGTable; //for HappyGo折抵時，查詢【促銷商品折抵活動】，以及【單商品折抵活動】使用。
        TempTables.Mode_HavePROD(this);

        this.hdHG_REDEEM_POINT.Text = StringUtil.CStr(TempTables.Detail.Compute("Sum(HG_REDEEM_POINT)", "ITEM_TYPE IN ('4','14') AND ITEM_STATUS <> '作廢'"));  //總兌換點數
        this.hdTOTAL_AMOUNT.Text = StringUtil.CStr(TempTables.Detail.Compute("Sum(TOTAL_AMOUNT)", "ITEM_TYPE = '4' AND ITEM_STATUS <> '作廢'"));        //總兌點金額
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

    protected void btnExchangeGoodsCheckOut_Click(object sender, EventArgs e)
    {
        OracleConnection objConn = OracleDBUtil.GetConnection();
        OracleTransaction objTX = objConn.BeginTransaction();
        bool bCheckOut = true; //是否可結帳
        string posuuid_master = "";
        string sale_status = "";
        string saleno = "";
        DataRow[] Sale_DetailRows2 = null;
        bool isCrossMonth = false;
        bool needDrawBackCreditCard = false;
        bool needDrawBackHGCard = false;
        bool IsDebit = false;
        bool runIMEILog = false;

        try
        {
            //處理找零
            if (lbChange.Text != "" && lbChange.Text != "0" && NumberUtil.IsNumeric(lbChange.Text))
            {
                int change = int.Parse(lbChange.Text);
                if (change > 0)
                {
                    Facade01.deletePaid_Detail(StringUtil.CStr(TempTables.Head.Rows[0]["POSUUID_MASTER"]), "8");
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
                    Facade01.InsertPaid_Detail(dtPaid);
                }
            }

            DataRow[] drsCreditCard = null;
            if (TempTables.Paid != null && TempTables.Paid.Rows.Count > 0)
                drsCreditCard = TempTables.Paid.Select(" ITEM_STATUS = '作廢' And Paid_Mode in ('2','3','4') ");
            if (drsCreditCard != null && drsCreditCard.Length > 0)
                needDrawBackCreditCard = true;

            DataRow[] drsHGCard = null;
            if (TempTables.Detail != null && TempTables.Detail.Rows.Count > 0)
                drsHGCard = TempTables.Detail.Select(" ITEM_STATUS = '作廢' And ITEM_TYPE in ('4','14') ");
            if (drsHGCard != null && drsHGCard.Length > 0)
                needDrawBackHGCard = true;

            switch (_SRC_TYPE)
            {
                case "Cache"://未正常關閉且已輸入付款資料
                    TempTables.Save_Sale_Data2DBFromCache(this, objTX);
                    break;
                default://銷貨單直接新增
                    TempTables.Save_Sale_Data2DBFromSAL01(this, objTX);
                    break;
            }

            DataTable dtInValid;
            if (StringUtil.CStr(TempTables.Head.Rows[0]["POSUUID_MASTER"]) == _PKEY)
            {   //Load from casch, old posuuid_master is invalid_id
                dtInValid = Facade01.getSale_Head(StringUtil.CStr(TempTables.Head.Rows[0]["INVALID_ID"]));
            }
            else
            {   //Not Load from casch
                dtInValid = Facade01.getSale_Head(this._PKEY);
            }
            DateTime oldTransDate = DateTime.Now.AddDays(-1);
            if (dtInValid != null && dtInValid.Rows.Count > 0 && dtInValid.Rows[0]["TRADE_DATE"] != null)
            {
                try
                {
                    oldTransDate = DateTime.Parse(StringUtil.CStr(dtInValid.Rows[0]["TRADE_DATE"]).Trim());
                }
                catch (Exception)
                {

                }
            }
            DateTime oldInvoiceDate = DateTime.Now.AddDays(-1);
            if (dtInValid != null && dtInValid.Rows.Count > 0 && dtInValid.Rows[0]["INVOICE_DATE"] != null)
            {
                try
                {
                    oldInvoiceDate = DateTime.Parse(StringUtil.CStr(dtInValid.Rows[0]["INVOICE_DATE"]).Trim());
                }
                catch (Exception)
                {

                }
            }

            if (TempTables.Head.Rows[0]["POSUUID_MASTER"] != null)
            {
                posuuid_master = StringUtil.CStr(TempTables.Head.Rows[0]["POSUUID_MASTER"]);
                sale_status = StringUtil.CStr(TempTables.Head.Rows[0]["SALE_STATUS"]);
                saleno = StringUtil.CStr(TempTables.Head.Rows[0]["SALE_NO"]);
            }

            //若有折扣資料 則更新累計折抵次數
            if (TempTables.Discount != null && TempTables.Discount.Rows.Count > 0)
            {
                DataRow[] Sale_DiscountRows = TempTables.Discount.Select("ITEM_TYPE IN ('5','11') AND ITEM_STATUS <> '作廢'");
                if (Sale_DiscountRows.Length > 0)
                {
                    foreach (DataRow dr in Sale_DiscountRows)
                    {
                        if (dr["PRODNO"] != null && !string.IsNullOrEmpty(StringUtil.CStr(dr["PRODNO"])))
                        {
                            if (Facade01.addStoreDiscountCount(logMsg.STORENO, StringUtil.CStr(dr["PRODNO"]), objTX) == -1)
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
            DataRow[] Sale_DetailRows = TempTables.Detail.Select("ITEM_TYPE = '4' AND ITEM_STATUS <> '作廢'");
            if (bCheckOut && Sale_DetailRows.Length > 0)
            {
                foreach (DataRow dr in Sale_DetailRows)
                {
                    if (dr["PRODNO"] != null && !string.IsNullOrEmpty(StringUtil.CStr(dr["PRODNO"])))
                    {
                        if (bCheckOut && !Facade01.chkHGDiscountCount(StringUtil.CStr(dr["PRODNO"]), objTX))
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
                            AffectRow = Facade01.UpdateMemberList(objTX, StringUtil.CStr(dr["PRODNO"]), StringUtil.CStr(dr["MSISDN"]));
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
            Sale_DetailRows2 = TempTables.Detail.Select("ITEM_TYPE = '6' AND ITEM_STATUS <> '作廢'");
            if (bCheckOut && Sale_DetailRows2.Length > 0)
            {
                string USED_AMOUNT = "0";
                USED_AMOUNT = StringUtil.CStr(Sale_DetailRows2[0]["TOTAL_AMOUNT"]);//取得特殊抱怨折扣金額
                string useRoleType = "2";
                if (logMsg.ROLE_TYPE == "1")
                    useRoleType = "1";
                else if (StringUtil.CStr(Sale_DetailRows2[0]["SH_DISCOUNT_DESC"]).IndexOf("使用店長權限") >= 0)
                    useRoleType = "1";
                if (Facade01.chkStoreSpecDisAmt(logMsg.STORENO, useRoleType, objTX))
                {
                    //回寫特殊抱怨已折扣金額
                    int AffectRow = 0;
                    string strYYMM = OracleDBUtil.WorkDay(logMsg.STORENO); //營業日
                    if (StringUtil.CStr(logMsg.ROLE_TYPE) == "1" || StringUtil.CStr(logMsg.ROLE_TYPE) == "2")   //當角色為1:店長或2:店員
                    {
                        AffectRow = Facade01.UpdateStoreSpecialDIS(objTX, logMsg.STORENO, strYYMM.Substring(0, 7), USED_AMOUNT, useRoleType);
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

            INVENTORY_Facade Inventory = new INVENTORY_Facade();
            //換貨作廢退回庫存
            DataRow[] Sale_DetailRows4 = TempTables.Detail.Select(" ITEM_TYPE IN ('1','2','3','7','8','9','10','13','14') AND ITEM_STATUS = '作廢'");
            if (bCheckOut && Sale_DetailRows4.Length > 0)
            {
                string oldPosuuid_Master = "";
                if (_SRC_TYPE == "Cache")
                    oldPosuuid_Master = StringUtil.CStr(TempTables.Head.Rows[0]["INVALID_ID"]);
                else
                    oldPosuuid_Master = this._PKEY;
                DataTable dtOldHead = new SAL01_Facade().getSale_Head(oldPosuuid_Master);
                string oldSale_No = "";
                if (dtOldHead != null && dtOldHead.Rows.Count > 0 &&
                    StringUtil.CStr(dtOldHead.Rows[0]["SALE_NO"]) != null && (!string.IsNullOrEmpty(StringUtil.CStr(dtOldHead.Rows[0]["SALE_NO"]))))
                    oldSale_No = StringUtil.CStr(dtOldHead.Rows[0]["SALE_NO"]);
                foreach (DataRow dr in Sale_DetailRows4)
                {
                    string Code = "";
                    string Message = "";
                    string Stock = Common_PageHelper.GetGoodLOCUUID();

                    if (!string.IsNullOrEmpty(StringUtil.CStr(dr["PRODNO"])) && StringUtil.CStr(dr["ISSTOCK"]) == "1")
                    {
                        try
                        {
                            Inventory.PK_INVENTORY_CHANGE(objTX, "1", StringUtil.CStr(dr["PRODNO"]),
                               logMsg.STORENO, Stock, oldSale_No, Convert.ToInt32(StringUtil.CStr(dr["QUANTITY"])), logMsg.MODI_USER, StringUtil.CStr(dr["ID"]), 
                               ref Code, ref Message);
                            if (Code != "000")
                            {
                                bCheckOut = false;
                                if (Sale_DetailRows2 != null && Sale_DetailRows2.Length > 0)
                                    ScriptManager.RegisterClientScriptBlock(this, typeof(string), "ERROR_MESSAGE", "alert('換貨商品扣庫存失敗!請記得進行HappyGo卡退點動作');", true);
                                else
                                    ScriptManager.RegisterClientScriptBlock(this, typeof(string), "ERROR_MESSAGE", "alert('換貨商品扣庫存失敗!!');", true);
                                objTX.Rollback();
                                //btnExchangeGoodsCheckOut.Enabled = true;
                                return;
                            }
                        }
                        catch (Exception)
                        {
                            bCheckOut = false;
                            if (Sale_DetailRows2 != null && Sale_DetailRows2.Length > 0)
                                ScriptManager.RegisterClientScriptBlock(this, typeof(string), "ERROR_MESSAGE", "alert('換貨商品退庫存失敗!請記得進行HappyGo卡退點動作');", true);
                            else
                                ScriptManager.RegisterClientScriptBlock(this, typeof(string), "ERROR_MESSAGE", "alert('換貨商品退庫存失敗!!');", true);
                            objTX.Rollback();
                            //btnExchangeGoodsCheckOut.Enabled = true;
                            return;
                        }
                    }
                }
            }

            DataRow[] Sale_DetailRows5 = TempTables.Detail.Select(" ITEM_TYPE IN ('1','2','3','7','8','9','10','13','14') AND ITEM_STATUS <> '作廢'");
            if (bCheckOut && Sale_DetailRows5.Length > 0)
            {
                string Stock = Common_PageHelper.GetGoodLOCUUID();
                Product_Facade prodFacade = new Product_Facade();
                foreach (DataRow dr in Sale_DetailRows5)
                {
                    string Code = "";
                    string Message = "";
                    if (!string.IsNullOrEmpty(StringUtil.CStr(dr["PRODNO"])))
                    {
                        DataTable dtProduct = prodFacade.Query_ProductInfo(StringUtil.CStr(dr["PRODNO"]));
                        if (dtProduct != null && dtProduct.Rows.Count > 0 && dtProduct.Rows[0]["ISSTOCK"] != null
                            && (!string.IsNullOrEmpty(StringUtil.CStr(dtProduct.Rows[0]["ISSTOCK"])))
                            && StringUtil.CStr(dtProduct.Rows[0]["ISSTOCK"]) == "1")
                        {
                            try
                            {
                                Inventory.PK_INVENTORY_SALE(objTX, "1", StringUtil.CStr(dr["PRODNO"]),
                                   logMsg.STORENO, Stock, StringUtil.CStr(TempTables.Head.Rows[0]["SALE_NO"]),
                                   Convert.ToInt32(StringUtil.CStr(dr["QUANTITY"])), logMsg.MODI_USER, StringUtil.CStr(dr["ID"]), ref Code, ref Message);
                                if (Code != "000")
                                {
                                    bCheckOut = false;
                                    if (Sale_DetailRows != null && Sale_DetailRows.Length > 0)
                                        ScriptManager.RegisterClientScriptBlock(this, typeof(string), "ERROR_MESSAGE", "alert('換貨商品扣庫存失敗，請做換貨扣庫存動作!!請記得進行HappyGo卡退點動作');", true);
                                    else
                                        ScriptManager.RegisterClientScriptBlock(this, typeof(string), "ERROR_MESSAGE", "alert('換貨商品扣庫存失敗，請做換貨扣庫存動作!!');", true);
                                    objTX.Rollback();
                                }
                            }
                            catch (Exception)
                            {
                                bCheckOut = false;
                                ScriptManager.RegisterClientScriptBlock(this, typeof(string), "ERROR_MESSAGE", "alert('換貨商品扣庫存失敗，請做換貨扣庫存動作!!');", true);
                                objTX.Rollback();
                            }
                        }
                    }
                }
            }

            if (oldTransDate.Month != DateTime.Now.Month)
            {
                isCrossMonth = true; 
            }

            //作廢發票/填入折讓單號
            if (bCheckOut)
            {
                int AffectRow = 0;
                string creditNoteNo = "";
                if (hidIsDebit.Value == "Y")
                {
                    creditNoteNo = Store_SerialNo.GenNo("CN", logMsg.STORENO, logMsg.MACHINE_ID);
                    IsDebit = true;
                }
                AffectRow = Facade03.invalidOldInvoice1(objTX, this._PKEY, !IsDebit, creditNoteNo);
                if (AffectRow < 1)
                {
                    bCheckOut = false;
                    if (Sale_DetailRows2 != null && Sale_DetailRows2.Length > 0)
                        if (isCrossMonth)
                            ScriptManager.RegisterClientScriptBlock(this, typeof(string), "ERROR_MESSAGE", "alert('開立折讓單失敗!請記得進行HappyGo卡退點動作');", true);
                        else 
                            ScriptManager.RegisterClientScriptBlock(this, typeof(string), "ERROR_MESSAGE", "alert('作廢發票失敗!請記得進行HappyGo卡退點動作');", true);
                    else
                        if (isCrossMonth)
                            ScriptManager.RegisterClientScriptBlock(this, typeof(string), "ERROR_MESSAGE", "alert('開立折讓單失敗!!');", true);
                        else 
                            ScriptManager.RegisterClientScriptBlock(this, typeof(string), "ERROR_MESSAGE", "alert('作廢發票失敗!!');", true);
                    objTX.Rollback();
                }
            }

            //IMEI_Log
            if (bCheckOut)
            {
                string strMessage = "";
                runIMEILog = true;
                if (StringUtil.CStr(TempTables.Head.Rows[0]["POSUUID_MASTER"]) == _PKEY)
                {   //Load from casch, old posuuid_master is invalid_id
                    strMessage = Facade03.IMEICHANGE_Log(objTX, StringUtil.CStr(TempTables.Head.Rows[0]["POSUUID_MASTER"]),
                                                                    StringUtil.CStr(TempTables.Head.Rows[0]["INVALID_ID"]), logMsg.MODI_USER);
                }
                else
                {   //Not Load from casch
                    strMessage = Facade03.IMEICHANGE_Log(objTX, StringUtil.CStr(TempTables.Head.Rows[0]["POSUUID_MASTER"]), _PKEY, logMsg.MODI_USER);
                }

                string[] strMsg = strMessage.Split('|');
                if (strMsg[0] != "000") //表示失敗
                {
                    bCheckOut = false;
                    if (Sale_DetailRows2 != null && Sale_DetailRows2.Length > 0)
                        ScriptManager.RegisterClientScriptBlock(this, typeof(string), "ERROR_MESSAGE", "alert('IMEI_LOG失敗!請記得進行HappyGo卡退點動作');", true);
                    else
                        ScriptManager.RegisterClientScriptBlock(this, typeof(string), "ERROR_MESSAGE", @"alert('IMEI_LOG失敗, " + strMsg[1].Replace("'", "-").Replace("\"", " ") + "');", true);
                    objTX.Rollback();
                }
            }

            //作廢原交易
            Logger.Log.Info("銷售換貨作業作廢原交易開始，POSUUID_MASTER=" + this._PKEY + " by POSUUID_MASTER=" + posuuid_master);
            if (bCheckOut)
            {
                int AffectRow = 0;
                //AffectRow = Facade03.invalidOldTransaction(objTX, this._PKEY, oldTransDate, logMsg.MODI_USER);
                AffectRow = Facade03.invalidOldTransactionPeriod(objTX, this._PKEY, oldInvoiceDate, logMsg.MODI_USER);
                if (AffectRow < 1)
                {
                    bCheckOut = false;
                    if (Sale_DetailRows2 != null && Sale_DetailRows2.Length > 0)
                        ScriptManager.RegisterClientScriptBlock(this, typeof(string), "ERROR_MESSAGE", "alert('作廢原交易失敗!請記得進行HappyGo卡退點動作');", true);
                    else
                        ScriptManager.RegisterClientScriptBlock(this, typeof(string), "ERROR_MESSAGE", "alert('作廢原交易失敗!!');", true);
                    objTX.Rollback();
                }
            }
            Logger.Log.Info("銷售換貨作業作廢原交易結束，POSUUID_MASTER=" + this._PKEY + " by POSUUID_MASTER=" + posuuid_master);

            if (bCheckOut)  //所有動作都有正常執行才可commit
            {
                if (saleno == "")
                    saleno = Store_SerialNo.GenNo("SALE", logMsg.STORENO, logMsg.MACHINE_ID);
                Facade01.UpdateSaleHead(objTX, saleno, sale_status, posuuid_master, logMsg.MODI_USER);
                objTX.Commit();
                TempTables.Mode_CheckOut(this);
            }

        }
        catch (Exception ex)
        {
            if (Sale_DetailRows2 != null && Sale_DetailRows2.Length > 0)
                ScriptManager.RegisterClientScriptBlock(this, typeof(string), "ERROR_MESSAGE", "alert('" + ex.Message.Replace("'", "-").Replace("\"", " ") + "，請記得進行HappyGo卡退點動作');", true);
            else
                ScriptManager.RegisterClientScriptBlock(this, typeof(string), "ERROR_MESSAGE", "alert('" + ex.Message.Replace("'","-").Replace("\"", " ") + "');", true);
            bCheckOut = false;
            objTX.Rollback();
        }

        if (!bCheckOut)
        {   //結帳失敗,恢復交易狀態為換貨未結帳
            Facade01.UpdateSaleHead("", "7", posuuid_master);
            if (runIMEILog)
            {   //因為 transaction roll back 後, IMIE 狀態不會跟著回復, 所以用逆轉方式處理
                OracleConnection objImeiConn = OracleDBUtil.GetConnection();
                OracleTransaction objImeiTX = objImeiConn.BeginTransaction();
                string strMessage = "";
                try
                {
                    if (StringUtil.CStr(TempTables.Head.Rows[0]["POSUUID_MASTER"]) == _PKEY)
                    {   //Load from casch, old posuuid_master is invalid_id
                        strMessage = Facade03.IMEICHANGE_Log(objImeiTX, StringUtil.CStr(TempTables.Head.Rows[0]["INVALID_ID"]),
                                                                StringUtil.CStr(TempTables.Head.Rows[0]["POSUUID_MASTER"]), logMsg.MODI_USER);
                    }
                    else
                    {   //Not Load from casch
                        strMessage = Facade03.IMEICHANGE_Log(objTX, _PKEY, StringUtil.CStr(TempTables.Head.Rows[0]["POSUUID_MASTER"]), logMsg.MODI_USER);
                    }
                    string[] strMsg = strMessage.Split('|');
                    if (strMsg[0] != "000") //表示失敗
                        objImeiTX.Rollback();
                    else 
                        objImeiTX.Commit();
                }
                catch
                {
                    objImeiTX.Rollback();
                }
            }
        }
        OtherService(posuuid_master);
        bool printInvoice = false;
        string noticeMsg = "";
        string invURL = "";
        string RECEIPT_URL = "";
        string DEBIT_URL = "";
        try
        {
            if (bCheckOut)
            {
                int invoiceTotalAmt = 0;
                if (TempTables.Head.Rows[0]["INVOICE_TOTAL_AMOUNT"] != null && StringUtil.CStr(TempTables.Head.Rows[0]["INVOICE_TOTAL_AMOUNT"]) != ""
                    && NumberUtil.IsNumeric(StringUtil.CStr(TempTables.Head.Rows[0]["INVOICE_TOTAL_AMOUNT"])))
                    invoiceTotalAmt = int.Parse(StringUtil.CStr(TempTables.Head.Rows[0]["INVOICE_TOTAL_AMOUNT"]));
                
                noticeMsg = "alert('換貨結帳完成!');";
                if (needDrawBackCreditCard)
                    noticeMsg += "alert('請記得刷退信用卡金額!');";
                if (needDrawBackHGCard)
                    noticeMsg += "alert('請記得刷退Happy Go已兌換點數!');";
                DataRow[] drs = TempTables.Detail.Select(" ITEM_STATUS <> '作廢' AND INV_TYPE = '1' ");
                if (invoiceTotalAmt > 0 && drs != null && drs.Length > 0)
                {
                    //列印發票
                    Receipt myReceipt = new Receipt();
                    string filePath = Facade01.getUploadPath(StringUtil.CStr(posuuid_master));
                    if (!string.IsNullOrEmpty(filePath))
                    {
                        string fileName = "";
                        fileName = myReceipt.generateReceiptfortest(posuuid_master, InvPrintName.Value);
                        if (fileName == null || string.IsNullOrEmpty(fileName))
                        {
                            noticeMsg += "alert('列印發票失敗，請重印發票!!');";
                        }
                        else
                        {
                            /* ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "PrintInvoice", "alert('換貨結帳完成!');document.getElementById('" +
                                                                        fDownload.ClientID + "').src='" + Request.ApplicationPath + filePath + "/" + fileName + "';", true); */
                            invURL = Request.ApplicationPath + filePath + "/" + fileName;
                            printInvoice = true;
                        }
                    }
                    else
                    {
                        noticeMsg += "alert('列印發票失敗，請重印發票!!');";
                    }
                }

                drs = TempTables.Detail.Select(" ITEM_STATUS <> '作廢' AND INV_TYPE = '3' ");
                if (invoiceTotalAmt == 0 || (drs != null && drs.Length > 0))
                {
                    //ScriptManager.RegisterClientScriptBlock(this, typeof(string), "PrintInvoiceError", "alert('結帳完成!');alert('列印收據!!');", true);
                    RECEIPT_URL = Collection_Receipt(posuuid_master, ReceiptPrintName.Value);
                    printInvoice = true;
                }

                if (IsDebit)
                {
                    Logger.Log.Info("銷售換貨作業列印折讓單開始，POSUUID_MASTER=" + StringUtil.CStr(TempTables.Head.Rows[0]["INVALID_ID"]));
                    Receipt myReceipt = new Receipt();

                    string filePath = Facade01.getUploadPath(this._PKEY);
                    Logger.Log.Info("銷售換貨作業取得折讓單路徑，POSUUID_MASTER=" + StringUtil.CStr(TempTables.Head.Rows[0]["INVALID_ID"]));
                    //  filePath += "/" + DateTime.Now.ToString("yyyyMMdd")+"/"+logMsg.STORENO;
                    if (!string.IsNullOrEmpty(filePath))
                    {

                        string fileName = "";
                        fileName = myReceipt.getnerateDebitNote(StringUtil.CStr(TempTables.Head.Rows[0]["INVALID_ID"]), DebitPrintName.Value);
                        Logger.Log.Info("銷售換貨作業產生折讓單完成，POSUUID_MASTER=" + StringUtil.CStr(TempTables.Head.Rows[0]["INVALID_ID"]));
                        if (fileName == null || string.IsNullOrEmpty(fileName))
                        {
                            noticeMsg += "alert('列印折讓單失敗，請重印折讓單!!');";
                        }
                        else
                        {
                            noticeMsg += "alert('自動開立折讓單!');";
                            DEBIT_URL = Request.ApplicationPath + filePath + "/" + fileName;
                            printInvoice = true;
                        }
                    }
                    else
                    {
                        noticeMsg += "alert('列印折讓單失敗，請重印折讓單!!');";
                    }
                } // end of IsDebit

                TempTables.Mode_CheckOut(this);
                //取得外部系統SALE_DETAIL
                DataTable dtDetail = Facade01.getSale_Detail(posuuid_master, "2");
                string posuuidList = "";
                if (dtDetail != null && dtDetail.Rows.Count > 0)
                {
                    foreach (DataRow dr in dtDetail.Rows)
                    {
                        if (posuuidList == "" || posuuidList.IndexOf(StringUtil.CStr(dr["POSUUID_DETAIL"])) < 0)
                            posuuidList += StringUtil.CStr(dr["POSUUID_DETAIL"]) + ";";
                    }
                    if (posuuidList.Length > 0)
                        posuuidList = posuuidList.Substring(0, posuuidList.Length - 1);
                }

                dtDetail = Facade03.getTO_CLOSE_ITEM(posuuidList, "2");
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
                                Facade01.CommitOuterSystem(sysID, StringUtil.CStr(dr["SERVICE_SYS_ID"]),
                                                                StringUtil.CStr(this.TempTables.Head.Rows[0]["POSUUID_MASTER"]), posuuidDetail,
                                                                StringUtil.CStr(dr["BARCODE1"]), logMsg.OPERATOR, logMsg.STORENO, StringUtil.CStr(dr["BUNDLE_ID"]), StringUtil.CStr(dr["BARCODE2"]), StringUtil.CStr(dr["BARCODE3"]));
                        }
                    }
                }
                else
                {
                    posuuidList = Facade03.getPOSUUID_DetailByPOSUUID_Master(StringUtil.CStr(TempTables.Head.Rows[0]["INVALID_ID"]));
                    if (posuuidList != "")
                    {
                        DataTable dtHead = Facade03.getTO_CLOSE_HEAD(posuuidList);
                        if (dtHead != null && dtHead.Rows.Count > 0)
                        {   //取消促代交易只會有未結表頭,沒有未結表身
                            string prePosuuidDetail = "";
                            string posuuidDetail = "";
                            foreach (DataRow dr in dtHead.Rows)
                            {
                                string sysID = "";
                                if (dr["POSUUID_DETAIL"] != null && (!string.IsNullOrEmpty(StringUtil.CStr(dr["POSUUID_DETAIL"]))))
                                    posuuidDetail = StringUtil.CStr(dr["POSUUID_DETAIL"]);
                                if (dr["Service_TYPE"] != null && (!string.IsNullOrEmpty(StringUtil.CStr(dr["Service_TYPE"]))))
                                {
                                    switch (StringUtil.CStr(dr["Service_TYPE"]))
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
                                        Facade01.CommitOuterSystem(sysID, StringUtil.CStr(dr["SERVICE_SYS_ID"]),
                                                                        StringUtil.CStr(this.TempTables.Head.Rows[0]["POSUUID_MASTER"]), posuuidDetail,
                                                                        "", logMsg.OPERATOR, logMsg.STORENO, "", "", "");
                                }
                            }
                        }
                        //刪除未結清單
                        Facade01.delTO_CLOSE(StringUtil.CStr(TempTables.Head.Rows[0]["INVALID_ID"]));
                    }
                }
                //刪除未結清單
                Facade01.delTO_CLOSE(posuuid_master);

                if (printInvoice)
                {
                    noticeMsg += "printInvAndReceipt('" + invURL + "','" + RECEIPT_URL + "','" + DEBIT_URL + "');";
                }
            }
        }
        catch (Exception ex)
        {
            if (!printInvoice)
            {
                noticeMsg += "alert('列印票據失敗，請重印票據[" + ex.Message.Replace("'", "-").Replace("\"", " ") + "]!!');";
            }
            else
            {
                noticeMsg += "printInvAndReceipt('" + invURL + "','" + RECEIPT_URL + "','" + DEBIT_URL + "');";
            }
        }
        finally
        {
            if (bCheckOut)
            {
                Session["EXCHANGE_SRC_TYPE"] = null;
                LoadEmptyData();
                ScriptManager.RegisterClientScriptBlock(this, typeof(string), "FinalMessage", noticeMsg, true);
            }
        }
    }

    protected void btnExchangeGoodsCancel_Click(object sender, EventArgs e)
    {
        SAL01_Facade Facade = new SAL01_Facade();
        bool ExchangeGoodsCancel = true;
        string processFlag = "";
        try
        {
            if (TempTables.Head.Rows[0]["POSUUID_MASTER"] != null &&
                (!string.IsNullOrEmpty(StringUtil.CStr(TempTables.Head.Rows[0]["POSUUID_MASTER"]))))
            {   //有交易表頭檔UID,刪除資料庫中交易資料
                Facade.invalidSaleIMEILog(StringUtil.CStr(TempTables.Head.Rows[0]["POSUUID_MASTER"]));
                int ret = Facade.delSaleData(StringUtil.CStr(TempTables.Head.Rows[0]["POSUUID_MASTER"]));
                if (ret < 0)
                    ExchangeGoodsCancel = false;

                if (ExchangeGoodsCancel)
                {
                    DataRow[] drs = TempTables.Detail.Select(" ITEM_TYPE = '2' AND ITEM_STATUS <> '作廢' ");
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
                                    posuuid_detailList.Append(OracleDBUtil.SqlStr(StringUtil.CStr(dr["POSUUID_DETAIL"])));
                                    processFlag = "Delete To Close Data";
                                    Facade.delTO_CLOSE(posuuid_detailList);
                                }
                                else
                                {
                                    processFlag = "Insert Data Upload Log";
                                    ExchangeGoodsCancel = false;
                                    Facade.InsertDataUploadLog(StringUtil.CStr(dr["POSUUID_DETAIL"]));
                                }
                            }
                        }
                    }
                }
            }
            else
            {
                TempTables.SaveTempTable(this);
            }
            if (ExchangeGoodsCancel)
            {
                string SALE_NO = StringUtil.CStr(TempTables.Head.Rows[0]["SALE_NO"]);
                if (SALE_NO == "")
                    SALE_NO = Store_SerialNo.GenNo("SALE", logMsg.STORENO, logMsg.MACHINE_ID);
                ScriptManager.RegisterClientScriptBlock(this, typeof(string), "OrderCancel", "alert('您已取消[" + SALE_NO + "]交易，請同步取消或更正該門號於業務園地的狀態!');", true);
                LoadEmptyData();
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this, typeof(string), "OrderCancelError", "alert('交易取消失敗!');", true);
            }
        }
        catch (Exception ex)
        {
            if (ExchangeGoodsCancel)
            {
                ScriptManager.RegisterClientScriptBlock(this, typeof(string), "OrderCancel", "alert('交易取消成功!但後續動作[" + processFlag + "]失敗!');", true);
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
            ScriptManager.RegisterClientScriptBlock(this, typeof(string), "NoSelect", "alert('請選取要刪除的資料!');", true);
        }
        else
        {
            if (TempTables.Detail.Rows.Count == 0) //全刪完了不能打付款資料           
                TempTables.Mode_NoPROD(this);
            else
                TempTables.Mode_HavePROD(this);
        }
        if (TempTables.Detail.Select("ITEM_STATUS <> '作廢'").Length > 0)
        {
            //for HappyGo折抵時，查詢【促銷商品折抵活動】，以及【單商品折抵活動】使用。
            Session["HGDISTempTable"] = TempTables.Detail.Select("ITEM_STATUS <> '作廢'").CopyToDataTable();
        }
        else
        {
            Session["HGDISTempTable"] = null;
        }
    }

    protected void btnPayDEL_Click(object sender, EventArgs e)
    {
        TempTables.Delete_Paid_Temp(gvCheckOut);
        CheckPaidData();
    }

    protected void btnCash_Click(object sender, EventArgs e)
    {
        //TempTables.SavePaid_Detail_Temp(gvCheckOut);
        string[] args = __EVENTARGUMENT.Split(new char[] { ',' });
        TempTables.NewRow_Paid_Cash(gvCheckOut, args);
        //付款一有資料時就不能再新增貨品資料了,並且要新增 DATA_CASH的資料來
        CheckPaidData();
    }

    protected void btnCredit_Click(object sender, EventArgs e)
    {
        //TempTables.SavePaid_Detail_Temp(gvCheckOut);
        string[] args = __EVENTARGUMENT.Split(new char[] { ',' });
        TempTables.NewRow_Paid_CreditCard(gvCheckOut, args, this);
        //付款一有資料時就不能再新增貨品資料了,並且要新增 DATA_CASH的資料來
        CheckPaidData();
        //信用卡交易刷完卡後不允許取消交易
        this.btnExchangeGoodsCancel.Enabled = false;
    }

    protected void btnDivCredit_Click(object sender, EventArgs e)
    {
        //TempTables.SavePaid_Detail_Temp(gvCheckOut);
        string[] args = __EVENTARGUMENT.Split(new char[] { ',' });
        TempTables.NewRow_Paid_DivCredit(gvCheckOut, args, this);
        //付款一有資料時就不能再新增貨品資料了,並且要新增 DATA_CASH的資料來
        CheckPaidData();
        //信用卡交易刷完卡後不允許取消交易
        this.btnExchangeGoodsCancel.Enabled = false;
    }

    protected void btnOffLineCredit_Click(object sender, EventArgs e)
    {
        //TempTables.SavePaid_Detail_Temp(gvCheckOut);
        string[] args = __EVENTARGUMENT.Split(new char[] { ',' });
        TempTables.NewRow_Paid_OffLineCredit(gvCheckOut, args);
        //付款一有資料時就不能再新增貨品資料了,並且要新增 DATA_CASH的資料來
        CheckPaidData();
        //信用卡交易刷完卡後不允許取消交易
        this.btnExchangeGoodsCancel.Enabled = false;
    }

    protected void btnConfirm_Click(object sender, EventArgs e)
    {
        OracleConnection objConn = OracleDBUtil.GetConnection();
        OracleTransaction objTX = objConn.BeginTransaction();

        TempTables.SaveTempTable(this);
        #region 判斷新增單品中，是否有舊機折扣的商品；若有，更新明細內容
        if (TempTables.Detail != null && TempTables.Detail.Rows.Count > 0)
        {
            DataRow[] drsDetail = TempTables.Detail.Select(" ITEM_STATUS <> '作廢' AND ITEM_TYPE Not In ('6','12') AND SOURCE_TYPE = '11' ");
            if (drsDetail != null && drsDetail.Length > 0)
            {
                foreach (DataRow drDetail in drsDetail)
                {
                    string discountType = Facade01.getDISCOUNT_TYPE(StringUtil.CStr(drDetail["PRODNO"]));
                    if (discountType == "2")
                    {
                        DataRow[] drsOuterSystem = TempTables.Detail.Select(" ITEM_STATUS <> '作廢' AND ITEM_TYPE Not In ('6','12') AND SOURCE_TYPE In ('1','2','4') ");
                        if (drsOuterSystem != null && drsOuterSystem.Length > 0)
                        {
                            drDetail["ITEM_TYPE"] = "12"; //舊機回收折扣
                            drDetail["ITEM_TYPE_NAME"] = "舊";
                            drDetail["ORI_UNIT_PRICE"] = drDetail["TOTAL_AMOUNT"];
                        }
                        else
                        {   //舊機回收折扣一定要跟著外部系統IA,Loyalty, SSI交易
                            TempTables.Detail.Rows.Remove(drDetail);
                        }
                        TempTables.Detail.AcceptChanges();
                    }
                }
            }
        }
        #endregion 判斷新增單品中，是否有舊機折扣的商品；若有，更新明細內容
        #region 新增單品相關贈品
        if (TempTables.Detail != null && TempTables.Detail.Rows.Count > 0 &&
            (TempTables.Detail.Select("ITEM_STATUS <> '作廢' AND SOURCE_TYPE = '11' AND ITEM_TYPE = '13' ") == null ||
                TempTables.Detail.Select("ITEM_STATUS <> '作廢' AND SOURCE_TYPE = '11' AND ITEM_TYPE = '13' ").Length == 0)) //新增Discount
            TempTables.NewRow_Detail_ItemGift(this, gvMaster);
        #endregion 新增單品相關贈品
        #region 新增單品相關折扣
        if (TempTables.Discount != null && TempTables.Discount.Rows.Count > 0 && 
            (TempTables.Discount.Select("ITEM_STATUS <> '作廢' AND SOURCE_TYPE = '11'") == null ||
                TempTables.Discount.Select("ITEM_STATUS <> '作廢' AND SOURCE_TYPE = '11'").Length == 0)) //新增Discount
            TempTables.NewRow_Detail_ItemDiscount(this, gvDetail);
        #endregion 新增單品相關折扣
        #region 新增促銷相關折扣
        if (TempTables.Discount != null && TempTables.Discount.Rows.Count > 0 && 
            (TempTables.Discount.Select("ITEM_STATUS <> '作廢' AND SOURCE_TYPE = '1' ") == null ||
                TempTables.Discount.Select("ITEM_STATUS <> '作廢' AND SOURCE_TYPE = '1' ").Length == 0)) //新增Discount
            TempTables.NewRow_Detail_MixPromotionDiscount(this, gvDetail, "1");
        if (TempTables.Discount != null && TempTables.Discount.Rows.Count > 0 && 
            (TempTables.Discount.Select("ITEM_STATUS <> '作廢' AND SOURCE_TYPE = '2' ") == null ||
            TempTables.Discount.Select("ITEM_STATUS <> '作廢' AND SOURCE_TYPE = '2' ").Length == 0)) //新增Discount
            TempTables.NewRow_Detail_MixPromotionDiscount(this, gvDetail, "2");
        #endregion 新增促銷相關折扣
        #region 新增完單品折扣及促銷折扣後，重算總應收及店長折扣與舊機折扣
        int totalAmt = 0;
        if (TempTables.Detail != null && TempTables.Detail.Rows.Count > 0)
        {
            DataRow[] drsDetail = TempTables.Detail.Select(" ITEM_STATUS <> '作廢' AND ITEM_TYPE Not In ('6','12') ");
            if (drsDetail != null && drsDetail.Length > 0)
            {
                foreach (DataRow drDetail in drsDetail)
                {
                    if (drDetail["TOTAL_AMOUNT"] != null && StringUtil.CStr(drDetail["TOTAL_AMOUNT"]) != ""
                        && NumberUtil.IsNumeric(StringUtil.CStr(drDetail["TOTAL_AMOUNT"])))
                        totalAmt += int.Parse(StringUtil.CStr(drDetail["TOTAL_AMOUNT"]));
                }
            }
        }

        if (TempTables.Discount != null && TempTables.Discount.Rows.Count > 0)
        {
            DataRow[] drsDiscount = TempTables.Discount.Select(" ITEM_STATUS <> '作廢' ");
            if (drsDiscount != null && drsDiscount.Length > 0)
            {
                foreach (DataRow drDiscount in drsDiscount)
                {
                    if (drDiscount["TOTAL_AMOUNT"] != null && StringUtil.CStr(drDiscount["TOTAL_AMOUNT"]) != ""
                        && NumberUtil.IsNumeric(StringUtil.CStr(drDiscount["TOTAL_AMOUNT"])))
                        totalAmt += int.Parse(StringUtil.CStr(drDiscount["TOTAL_AMOUNT"]));
                }
            }
        }

        if (totalAmt < 0)
            totalAmt = 0;

        if (TempTables.Detail != null && TempTables.Detail.Rows.Count > 0)
        {
            DataRow[] drsDetailDiscount = TempTables.Detail.Select(" ITEM_STATUS <> '作廢' AND ITEM_TYPE In ('6','12') ");
            if (drsDetailDiscount != null && drsDetailDiscount.Length > 0)
            {
                foreach (DataRow drDetailDiscount in drsDetailDiscount)
                {
                    if (drDetailDiscount["ITEM_TYPE"] != null && StringUtil.CStr(drDetailDiscount["ITEM_TYPE"]) == "6") //店長折扣重新計算
                    {
                        int unitPrice = 0;
                        if (drDetailDiscount["SH_DISCOUNT_RATE"] != null && StringUtil.CStr(drDetailDiscount["SH_DISCOUNT_RATE"]) != ""
                            && NumberUtil.IsNumeric(StringUtil.CStr(drDetailDiscount["SH_DISCOUNT_RATE"])))
                            unitPrice = -1 * ((int)Math.Round(((double)totalAmt) * 
                                                ((double)int.Parse(StringUtil.CStr(drDetailDiscount["SH_DISCOUNT_RATE"])) / 100), 0));

                        if (unitPrice == 0)
                        {
                            if (drDetailDiscount["TOTAL_AMOUNT"] != null && StringUtil.CStr(drDetailDiscount["TOTAL_AMOUNT"]) != ""
                                && NumberUtil.IsNumeric(StringUtil.CStr(drDetailDiscount["TOTAL_AMOUNT"])))
                                unitPrice = int.Parse(StringUtil.CStr(drDetailDiscount["TOTAL_AMOUNT"]));
                        }

                        if (totalAmt + unitPrice < 0)
                        {
                            unitPrice = -1 * totalAmt;
                            totalAmt = 0;
                        }
                        drDetailDiscount["ORI_UNIT_PRICE"] = drDetailDiscount["TOTAL_AMOUNT"];
                        drDetailDiscount["UNIT_PRICE"] = unitPrice;
                        drDetailDiscount["TOTAL_AMOUNT"] = unitPrice;
                        TempTables.Detail.AcceptChanges();
                    }
                    else if (drDetailDiscount["ITEM_TYPE"] != null && StringUtil.CStr(drDetailDiscount["ITEM_TYPE"]) == "12")
                    {   //舊機折扣重新計算
                        if (drDetailDiscount["TOTAL_AMOUNT"] != null && StringUtil.CStr(drDetailDiscount["TOTAL_AMOUNT"]) != ""
                            && NumberUtil.IsNumeric(StringUtil.CStr(drDetailDiscount["TOTAL_AMOUNT"])))
                            if (totalAmt + int.Parse(StringUtil.CStr(drDetailDiscount["TOTAL_AMOUNT"])) < 0)
                            {
                                drDetailDiscount["ORI_UNIT_PRICE"] = drDetailDiscount["TOTAL_AMOUNT"];
                                drDetailDiscount["TOTAL_AMOUNT"] = -1 * totalAmt;
                                totalAmt = 0;
                                TempTables.Detail.AcceptChanges();
                            }
                    }
                }
            }
        }
        #endregion 新增完單品折扣及促銷折扣後，重算總應收及店長折扣與舊機折扣
        #region 新增保證金現金支付條件(後端算帳用)
        if (TempTables.Detail != null && TempTables.Detail.Rows.Count > 0 && TempTables.Detail.Select(" ITEM_STATUS <> '作廢' AND ITEM_TYPE In ('1','2') ") != null
            && TempTables.Detail.Select(" ITEM_STATUS <> '作廢' AND ITEM_TYPE In ('1','2') ").Length > 0)
        {
            DataRow[] drsProd = TempTables.Detail.Select(" ITEM_STATUS <> '作廢' AND ITEM_TYPE In ('1','2') ");
            foreach (DataRow drPod in drsProd)
            {
                if (drPod["PRODNO"] != null && StringUtil.CStr(drPod["PRODNO"]) != "")
                {
                    if (TSAL01_Facade.CheckGUARANTEE(StringUtil.CStr(drPod["PRODNO"])))
                    {   //保證金料號
                        if (drPod["TOTAL_AMOUNT"] != null && StringUtil.CStr(drPod["TOTAL_AMOUNT"]) != "" &&
                            NumberUtil.IsNumeric(StringUtil.CStr(drPod["TOTAL_AMOUNT"])) &&
                            int.Parse(StringUtil.CStr(drPod["TOTAL_AMOUNT"])) < 0)
                        {   //退保證金
                            string[] cashArgs = new string[2];
                            cashArgs[0] = "CASH";
                            cashArgs[1] = StringUtil.CStr(drPod["TOTAL_AMOUNT"]);
                            TempTables.NewRow_Paid_Cash(gvCheckOut, cashArgs);
                            cashArgs[1] = StringUtil.CStr(-1 * int.Parse(StringUtil.CStr(drPod["TOTAL_AMOUNT"])));
                            TempTables.NewRow_Paid_Cash(gvCheckOut, cashArgs);
                        }
                    }
                }
            }
        }
        #endregion 新增保證金現金支付條件(後端算帳用)
        #region 新增舊交易現金支付的支付條件
        DataRow[] drs = null;
        if (TempTables.Paid != null && TempTables.Paid.Rows.Count > 0)
            drs = TempTables.Paid.Select(" PAID_MODE In ('1','5','8')");
        if (drs != null && drs.Length > 0)
        {
            int tolAmt = 0;
            foreach (DataRow drOld in drs)
            {
                if (drOld["PAID_AMOUNT"] != null && StringUtil.CStr(drOld["PAID_AMOUNT"]) != ""
                    && NumberUtil.IsNumeric(StringUtil.CStr(drOld["PAID_AMOUNT"])))
                    tolAmt += int.Parse(StringUtil.CStr(drOld["PAID_AMOUNT"]));
            }
            if (tolAmt != 0)
            {
                string[] cashArgs = new string[2];
                cashArgs[0] = "CASH";
                cashArgs[1] = StringUtil.CStr(tolAmt);
                TempTables.NewRow_Paid_Cash(gvCheckOut, cashArgs);
            }
        }

        gvCheckOut.DataSource = TempTables.Paid;
        gvCheckOut.DataBind();
        #endregion 新增舊交易現金支付的支付條件

        TempTables.calTotalAmount(this);    //計算總應收金額
        TempTables.CalculationTax(this);    //計算稅額

        //TempTables.calPayTotal_Change(this);
        TempTables.SavePaid_Detail_Temp(gvCheckOut);
        TempTables.Head.Rows[0]["SALE_STATUS"] = "7";    //避免結帳失敗後，使用者取消付款明細，又按確定，而Session Table中的狀態停留在狀態2，已結帳
        try
        {
            TempTables.Save_Sale_Data2Cache(this, objTX);
            objTX.Commit();
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            if (objConn.State == ConnectionState.Open)
                objConn.Close();
            objConn.Dispose();
            OracleConnection.ClearAllPools();
        }
        TempTables.Mode_PayCash(this);  //進入付款模式
        lbTOTAL_AMOUNT.Text = StringUtil.CStr(TempTables.Head.Rows[0]["SALE_TOTAL_AMOUNT"]);
        TempTables.calPayTotal_Change(this); //計算應付金額及找零金
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        TempTables.Delete_Sale_DataFromCache(this);
        TempTables.Mode_HavePROD(this);
    }

    protected void gvMaster_HtmlRowCreated(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewTableRowEventArgs e)
    {
        if (e.RowType == GridViewRowType.Data)
        {
            ASPxTextBox lblIMEI = e.Row.FindChildControl<ASPxTextBox>("lbIMEI_QTY");
            ASPxTextBox txtProdName = e.Row.FindChildControl<ASPxTextBox>("txtPRODNAME");
            ASPxTextBox hdIMEIFlag = e.Row.FindChildControl<ASPxTextBox>("hdIMEIFlag");
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
            int intS_IMEI = int.Parse(StringUtil.CStr(e.GetValue("IMEI_QTY")) == "" ? "0" : lblIMEI.Text); //StringUtil.CStr(e.GetValue("IMEI_QTY"))
            ASPxImage imgIMEI = e.Row.FindChildControl<ASPxImage>("imgIMEI");
            PopupControl InputIMEIData = e.Row.FindChildControl<PopupControl>("InputIMEIData");


            //取得IMEI_Flag
            DataTable dtFlag = new Product_Facade().Query_ProductInfo(PRODNO);
            string IMEI_Flag = "";
            if (dtFlag.Rows.Count > 0)
            {
                IMEI_Flag = StringUtil.CStr(dtFlag.Rows[0]["IMEI_FLAG"]);
                txtProdName.Text = StringUtil.CStr(dtFlag.Rows[0]["PRODNAME"]);
                hdIMEIFlag.Text = IMEI_Flag;
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
            ASPxTextBox lblIMEI = gvMaster.FindRowCellTemplateControl(e.VisibleIndex, (GridViewDataColumn)gvMaster.Columns["IMEI_QTY"], "lbIMEI_QTY") as ASPxTextBox;
            ASPxImage imgIMEI = gvMaster.FindRowCellTemplateControl(e.VisibleIndex, (GridViewDataColumn)gvMaster.Columns["imgIMEI"], "imgIMEI") as ASPxImage; 

            string ITEM_STATUS = StringUtil.CStr(gvMaster.GetRowValues(e.VisibleIndex, "ITEM_STATUS"));
            if (ITEM_STATUS == "作廢")
            {
                txtPRODNO.Enabled = false;
                txtQUANTITY.ReadOnly = true;
                txtUNIT_PRICE.ReadOnly = true;
                InputIMEIData.Enabled = false;

                e.Row.Attributes["canSelect"] = "false";
            }
            else
            {
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
                else if (STATUS == "4" || STATUS == "6")  //4 HappyGo折扣, 6 店長折扣
                {
                    txtPRODNO.Enabled = false;
                    txtQUANTITY.ReadOnly = true;
                    ((ASPxImage)gvMaster.FindRowCellTemplateControl(e.VisibleIndex, (GridViewDataColumn)gvMaster.Columns["imgIMEI"], "imgIMEI")).Visible = false; //e.Row.FindChildControl<ASPxImage>("imgIMEI").Visible = false;
                    ((ASPxTextBox)gvMaster.FindRowCellTemplateControl(e.VisibleIndex, (GridViewDataColumn)gvMaster.Columns["IMEI_QTY"], "lbIMEI_QTY")).Visible = false; //e.Row.FindChildControl<ASPxTextBox>("lbIMEI_QTY").Visible = false;
                    InputIMEIData.Visible = false;
                    if (STATUS == "6")
                    {
                        //已有店長折扣，則不能再選取
                        gvMaster.FindChildControl<ASPxButton>("btnStoreDiscount").Enabled = false;
                    }

                    if (STATUS == "4")
                    {
                        //已有HappyGo折扣，則不能再選取
                        gvMaster.FindChildControl<ASPxButton>("btnHappyGoNet").Enabled = false;
                    }

                }

                //若有選取 店長折扣或HappyGo折扣或普通商品加價購及贈品 則不能再變更原本的商品項目
                DataRow[] Sale_DetailRows = TempTables.Detail.Select("ITEM_TYPE IN('4','6','7','13') AND ITEM_STATUS <> '作廢'");
                if (Sale_DetailRows.Length > 0 && STATUS != "4" && STATUS != "6")
                {
                    txtPRODNO.Enabled = false;
                    txtQUANTITY.ReadOnly = true;
                    e.Row.Attributes["canSelect"] = "false";
                    if (STATUS == "7" && STATUS == "13" && STATUS == "15" && STATUS == "16")
                    {
                        e.Row.Attributes["canSelect"] = "true";
                    }
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
            if (InputIMEIData.Text != "")
            {
                if (lblIMEI.Text == "0")
                    lblIMEI.Text = txtQUANTITY.Text;
                imgIMEI.ImageUrl = "~/Icon/check.png";
            }
        }
    }
    protected void gvMaster_CommandButtonInitialize(object sender, ASPxGridViewCommandButtonEventArgs e)
    {
        string itemType = StringUtil.CStr(gvMaster.GetRowValues(e.VisibleIndex, "ITEM_TYPE"));
        if (itemType == "5" || itemType == "2" || itemType == "11" || itemType == "12") //5.未結折扣,2未結明細,11 租賃折扣, 12 舊機換新機折扣
        {
            if (e.ButtonType == ColumnCommandButtonType.SelectCheckbox) { e.Enabled = false; }
        }

        string STATUS = StringUtil.CStr(gvMaster.GetRowValues(e.VisibleIndex, "ITEM_STATUS"));
        if (STATUS == "作廢") //要作廢的交易內容
        {
            if (e.ButtonType == ColumnCommandButtonType.SelectCheckbox) { e.Enabled = false; }
        }

        DataRow[] Sale_DetailRows = TempTables.Detail.Select("ITEM_TYPE IN('4','6','7','13','15','16') AND ITEM_STATUS <> '作廢'"); //若有選取 店長折扣或HappyGo折扣或普通商品加價購及贈品 則不能再變更原本的商品項目
        if (Sale_DetailRows.Length > 0 && itemType != "4" && itemType != "6" && itemType != "7" && itemType != "13" && itemType != "15" && itemType != "16")
        {
            e.Enabled = false;
        }

    }
    protected void gvCheckOut_HtmlRowPrepared(object sender, ASPxGridViewTableRowEventArgs e)
    {
        string ITEM_STATUS = "";
        if (gvCheckOut.Enabled)
            e.Row.Attributes["canSelect"] = "true";
        else
            e.Row.Attributes["canSelect"] = "false";
        if (e.VisibleIndex != -1 && (!string.IsNullOrEmpty(StringUtil.CStr(gvCheckOut.GetRowValues(e.VisibleIndex, "ITEM_STATUS")))))
            ITEM_STATUS = StringUtil.CStr(gvCheckOut.GetRowValues(e.VisibleIndex, "ITEM_STATUS"));
        if (ITEM_STATUS == "作廢")
        {
            e.Row.Enabled = false;
            e.Row.Attributes["canSelect"] = "false";
        }
    }
    //ajax 呼當前網頁的方式
    [System.Web.Services.WebMethod()]
    [System.Web.Script.Services.ScriptMethod()]
    static public string getPRODINFO(string PRODNO, string STORE_NO, string oldProdNo, string SALE_DETAIL_ID)
    {
        if (oldProdNo != "" && oldProdNo != PRODNO)
        {
            int ret = new IMEI_Facade().CleanINV_IMEI("SALE_IMEI_LOG", SALE_DETAIL_ID);
        }

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
                r += "0";

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
        if (TempTables.Paid.Select(" ITEM_STATUS <> '作廢' ").Length > 0)
        {
            //是否已存進Cache,不能用Sale_Head判斷, 因為會去Join 發票檔而此時相關發票資料尚未產生
            DataTable dt = Facade01.getPaid_Detail(StringUtil.CStr(TempTables.Head.Rows[0]["POSUUID_MASTER"]));
            if (dt != null && dt.Rows.Count > 0)
            { //若已存入Cache只維護 PAID部份
                Facade01.DeletePaid_DetailByPOSUUID_MASTER(StringUtil.CStr(TempTables.Head.Rows[0]["POSUUID_MASTER"]));
                Facade01.InsertPaid_Detail(TempTables.Paid.Select(" ITEM_STATUS <> '作廢' ").CopyToDataTable());
                //DELETE + INSERT = UPDATE
            }
            else
            {
                Facade01.InsertPaid_Detail(TempTables.Paid.Select(" ITEM_STATUS <> '作廢' ").CopyToDataTable());
            }
            TempTables.calPayTotal_Change(this);//計算應付金額找零金
            TempTables.Mode_PayCash(this);  //維持在付款模式
        }
        else
        {
            TempTables.Delete_Sale_DataFromCache(this);
            TempTables.calPayTotal_Change(this);//計算應付金額找零金
            TempTables.Mode_HavePROD(this); //進入有商品模式
        }
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

    protected void ASPxCallback1_Callback(object source, DevExpress.Web.ASPxCallback.CallbackEventArgs e)
    {
        TempTables.calTotalAmount(this);//計算稅額
        TempTables.Detail.AcceptChanges();

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

        if (TempTables.Detail.Columns.IndexOf("IS_OPEN_PRICE") < 0)
            TempTables.Detail.Columns.Add("IS_OPEN_PRICE");
        if (pa[5] != null)
            TempTables.Detail.Rows[i]["IS_OPEN_PRICE"] = pa[5];
        TempTables.calTotalAmount(this);//計算銷售總金額
        TempTables.Detail.AcceptChanges();

        int ReturnAmount = 0;
        ReturnAmount = Convert.ToInt32(StringUtil.CStr(TempTables.STORE_REC_TOTAL_AMOUNT)) - Convert.ToInt32(this.lblOriginalTOTAL_AMOUNT.Text);
        if (ReturnAmount >= 0)
            e.Result = StringUtil.CStr(TempTables.STORE_REC_TOTAL_AMOUNT) + ";" +
                        StringUtil.CStr(TempTables.STORE_PAY_TOTAL_AMOUNT) + ";" +
                        StringUtil.CStr(TempTables.STORE_CHANGE_AMOUNT) + ";" +
                        StringUtil.CStr(ReturnAmount) + ";應補差額";
        else
        {
            ReturnAmount = Convert.ToInt32(this.lblOriginalTOTAL_AMOUNT.Text) - Convert.ToInt32(StringUtil.CStr(TempTables.STORE_REC_TOTAL_AMOUNT));
            e.Result = StringUtil.CStr(TempTables.STORE_REC_TOTAL_AMOUNT) + ";" +
                        StringUtil.CStr(TempTables.STORE_PAY_TOTAL_AMOUNT) + ";" +
                        StringUtil.CStr(TempTables.STORE_CHANGE_AMOUNT) + ";" +
                        StringUtil.CStr(ReturnAmount) + ";應退差額";
        }

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
            Logger.Log.Info("換貨作業開始拆折扣，單號:" + posuuid_master);
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
            Logger.Log.Info("換貨作業拆折扣成功，單號:" + posuuid_master);
        }
        catch (Exception ex)
        {
            Logger.Log.Info("換貨作業拆折扣失敗，單號:" + posuuid_master + ",Error Message=" + ex.Message);
        }
        finally
        {
            if (conn.State == ConnectionState.Open) conn.Close();
            OracleConnection.ClearPool(conn);
        }
    }

    #region 收據
    private string Collection_Receipt(string posuuid_master, string receiptname)
    {
        string return_url = "";
        List<List<string>> dir = new List<List<string>>();
        OracleConnection conn = null;
        OracleCommand cmd = null;
        OracleDataAdapter da = null;
        OracleDataReader dr = null;
        string trade_date = "";
        string store_no = "";
        string machice_id = "";
        string RECEIPT_NO = "";
        string total_amount = "";
        string barcode1 = "";
        string barcode2 = "";
        string barcode3 = "";
        string CARD_NO = "";
        string msisdn = "";
        string SALE_PERSON = "";
        string hg_card_no = "";
        string pay_mode1 = ""; //現金
        string pay_mode2 = ""; //信用卡
        string pay_mode7 = "";//HappyGo折抵
        string pay_mode8 = "";//找零金
        string source_type = "";
        string prodno = "";
        string sqlstr = "select sh.trade_date,SH.STORE_NO,SH.MACHINE_ID,RH.RECEIPT_NO,SH.SALE_PERSON,sh.hg_card_no from sale_head sh  join RECEIPT_HEAD rh on RH.POSUUID_MASTER = SH.POSUUID_MASTER where  sh.posuuid_master = " + OracleDBUtil.SqlStr(posuuid_master);
        try
        {
            conn = OracleDBUtil.GetConnection();
            cmd = new OracleCommand(sqlstr, conn);
            dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                trade_date = dr.IsDBNull(0) ? "" : dr.GetDateTime(0).ToString("yyyy-MM-dd hh:mm");//StringUtil.CStr(dr[0]);
                store_no = StringUtil.CStr(dr[1]);
                machice_id = StringUtil.CStr(dr[2]);
                RECEIPT_NO = StringUtil.CStr(dr[3]);
                SALE_PERSON = StringUtil.CStr(dr[4]);
                hg_card_no = StringUtil.CStr(dr[5]);
            }
            dr.Read();

            
            //抓取detal
            sqlstr = "select barcode1,barcode2,barcode3,msisdn,total_amount,id,fun_id,CARD_NO,prodno,source_type from sale_detail where posuuid_master = " + OracleDBUtil.SqlStr(posuuid_master);
            DataTable detail = new DataTable();
            cmd = new OracleCommand(sqlstr, conn);
            da = new OracleDataAdapter(cmd);
            da.Fill(detail);
            foreach (DataRow row in detail.Rows)
            {
                string sale_detail_id = StringUtil.CStr(row["id"]);
                barcode1 = StringUtil.CStr(row["barcode1"]);
                barcode2 = StringUtil.CStr(row["barcode2"]);
                barcode3 = StringUtil.CStr(row["barcode3"]);
                CARD_NO = StringUtil.CStr(row["CARD_NO"]);
                msisdn = StringUtil.CStr(row["msisdn"]);
                total_amount = StringUtil.CStr(row["total_amount"]);
                source_type = StringUtil.CStr(row["source_type"]);
                prodno = StringUtil.CStr(row["prodno"]);
                string fun_id = StringUtil.CStr(row["fun_id"]);

                //抓出拆分金額
                //抓現金
                sqlstr = "select sum(amount) from bill_dispatch where pay_mode_id = 1 and sale_detail_id = " + OracleDBUtil.SqlStr(sale_detail_id);
                cmd = new OracleCommand(sqlstr, conn);
                pay_mode1 = cmd.ExecuteScalar() == null ? "0" : StringUtil.CStr(cmd.ExecuteScalar());

                //抓信用卡
                sqlstr = "select sum(amount) from bill_dispatch where pay_mode_id in(2,3,4) and sale_detail_id = " + OracleDBUtil.SqlStr(sale_detail_id);
                cmd = new OracleCommand(sqlstr, conn);
                pay_mode2 = cmd.ExecuteScalar() == null ? "0" : StringUtil.CStr(cmd.ExecuteScalar());

                //找零金
                sqlstr = "select sum(amount) from bill_dispatch where pay_mode_id = 8 and sale_detail_id = " + OracleDBUtil.SqlStr(sale_detail_id);
                cmd = new OracleCommand(sqlstr, conn);
                pay_mode8 = cmd.ExecuteScalar() == null ? "0" : StringUtil.CStr(cmd.ExecuteScalar());

                //happyGo
                sqlstr = "select sum(amount) from bill_dispatch where pay_mode_id = 7 and sale_detail_id = " + OracleDBUtil.SqlStr(sale_detail_id);
                cmd = new OracleCommand(sqlstr, conn);
                pay_mode7 = cmd.ExecuteScalar() == null ? "0" : StringUtil.CStr(cmd.ExecuteScalar());
                //1: 遠傳帳單
                //2: 和信帳單
                //3: Seednet帳單
                //4: 遠通帳單(有單)
                //5: 遠通帳單(無單)
                //6: 速博帳單
                //判斷類型
                List<string> list = new List<string>();
                if (source_type == "3" && prodno != "700300010")
                {
                    #region 代收
                    switch (fun_id)
                    {
                        case "1":
                            //FET

                            list.Add("4");
                            list.Add(trade_date);
                            list.Add(store_no);
                            list.Add(machice_id);
                            list.Add(RECEIPT_NO);
                            list.Add("遠傳電信帳單");
                            list.Add(total_amount);
                            list.Add(barcode1);
                            list.Add(msisdn);
                            list.Add(pay_mode1);
                            list.Add(pay_mode2);
                            list.Add(pay_mode7);
                            list.Add("0");
                            list.Add(hg_card_no);
                            list.Add(SALE_PERSON);
                            dir.Add(list);
                            break;
                        case "2"://KGT

                            list.Add("5");
                            list.Add(trade_date);
                            list.Add(store_no);
                            list.Add(machice_id);
                            list.Add(RECEIPT_NO);
                            list.Add("和信電信帳單");
                            list.Add(total_amount);
                            list.Add(barcode1);
                            list.Add(pay_mode1);
                            list.Add(pay_mode2);
                            list.Add(pay_mode7);
                            list.Add(pay_mode8);
                            list.Add(hg_card_no);
                            list.Add(SALE_PERSON);
                            dir.Add(list);
                            break;
                        case "3"://Seednet
                            list.Add("8");
                            list.Add(trade_date);
                            list.Add(store_no);
                            list.Add(machice_id);
                            list.Add(RECEIPT_NO);
                            list.Add("Seednet帳單");
                            list.Add(total_amount);
                            list.Add(barcode1);
                            list.Add(barcode2);
                            list.Add(barcode3);
                            list.Add(pay_mode1);
                            list.Add(SALE_PERSON);
                            dir.Add(list);
                            break;
                        case "4"://ETC(有單
                            list.Add("3");
                            list.Add(trade_date);
                            list.Add(store_no);
                            list.Add(machice_id);
                            list.Add(RECEIPT_NO);
                            list.Add("ETC 代收");
                            list.Add(total_amount);
                            list.Add(barcode1);
                            list.Add(barcode2);
                            list.Add(barcode3);
                            list.Add(pay_mode1);
                            list.Add(barcode2);
                            list.Add(SALE_PERSON);
                            dir.Add(list);
                            break;
                        case "5"://ETC(無單
                            list.Add("3");
                            list.Add(trade_date);
                            list.Add(store_no);
                            list.Add(machice_id);
                            list.Add(RECEIPT_NO);
                            list.Add("ETC 代收");
                            list.Add(total_amount);
                            list.Add(barcode1);
                            list.Add(barcode2);
                            list.Add(barcode3);
                            list.Add(pay_mode1);
                            list.Add(pay_mode2);
                            list.Add(SALE_PERSON);
                            dir.Add(list);
                            break;
                        case "6"://NCIC
                            list.Add("6");
                            list.Add(trade_date);
                            list.Add(store_no);
                            list.Add(machice_id);
                            list.Add(RECEIPT_NO);
                            list.Add("速博電信帳單");
                            list.Add(total_amount);
                            list.Add(barcode1);
                            list.Add(barcode2);
                            list.Add(barcode3);
                            list.Add(pay_mode1);

                            list.Add(SALE_PERSON);
                            dir.Add(list);
                            break;
                        default:

                            break;
                    }
                    #endregion
                }
                else
                {
                    
                    if (TSAL01_Facade.CheckGUARANTEE(prodno))
                    {
                        list.Add("2");
                        list.Add(trade_date);
                        list.Add(store_no);
                        list.Add(machice_id);
                        list.Add(RECEIPT_NO);
                        list.Add("保證金");
                        list.Add(total_amount);
                        list.Add(msisdn);
                        list.Add(SALE_PERSON);
                        dir.Add(list);
                    }
               
                }
             
            }

            string fileName = "";
            if (dir.Count > 0)
            {
                SAL01_Facade facade = new SAL01_Facade();
                string filePath = facade.getUploadPath(posuuid_master);
                IRClass pri = new PriReceipt();
                fileName = pri.Print("M", null, dir, receiptname);

                if (fileName == null || string.IsNullOrEmpty(fileName))
                {
                    throw new Exception("列印收據失敗，請重印收據!!");
                }
                else
                {
                    return_url = Request.ApplicationPath + "/Downloads/Receipt/" + fileName;
                }
            }
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
        return return_url;
    }

    #endregion
}
