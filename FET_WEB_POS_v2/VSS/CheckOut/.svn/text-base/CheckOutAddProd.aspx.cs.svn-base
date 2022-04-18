using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using DevExpress.Web.ASPxEditors;
using FET.POS.Model.Facade.FacadeImpl;
using DevExpress.Web.ASPxGridView;
using Advtek.Utility;
using System.Collections.Specialized;

public partial class VSS_CheckOut_CheckOutAddProd : BasePage
{
    DataTable TempTables
    {
        get
        {
            DataTable r = Session["HGDISTempTable"] as DataTable;
            if (r == null)
            {
                r = new DataTable();
            }
            return r;
        }
    }

    string[] addProdList
    {
        get
        {
            //string r = Request["addProdList"] == null ? "" : StringUtil.CStr(Request["addProdList"]);
            //if (r == null)
            //{
            //    r = "";
            //}
            //return r.Split("^".ToCharArray());

            string r = "";

            //**2011/04/27 Tina：前頁傳遞參數時，會先以加密處理，在此要解密。
            if (!string.IsNullOrEmpty(Request.QueryString["Param"]))
            {
                NameValueCollection qscoll = Utils.Param_UrlDecode(Request.QueryString["Param"]);
                foreach (string key in qscoll.AllKeys)
                {
                    if (key == "addProdList")
                    {
                        r = string.Join(",", qscoll.GetValues(key));
                        break;
                    }
                }
            }

            return r.Split("^".ToCharArray());
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack && !IsCallback)
        {
            if (TempTables == null || TempTables.Rows.Count == 0)
            {
                ScriptManager.RegisterClientScriptBlock(this, typeof(string), "NoProductError", "alert('查無商品項目，無法執行加價購作業!');window.close();';", true);
            }
            else
            {
                hidStore_No.Value = logMsg.STORENO;
                hidEmployId.Value = logMsg.OPERATOR;
                bindGridData();
            }
        }
    }

    private void bindGridData()
    {
        DataTable dtMaster = new DataTable();

        foreach (DataRow dr in TempTables.Rows)
        {
            Hashtable listAddProd = Discount_Facade.get_add_in_prod_discount(StringUtil.CStr(dr["PRODNO"]), logMsg.STORENO, logMsg.OPERATOR,
                                                                                StringUtil.CStr(dr["POSUUID_DETAIL"]));
            Hashtable listGift = Discount_Facade.get_gift_discount(StringUtil.CStr(dr["PRODNO"]), logMsg.STORENO, logMsg.OPERATOR,
                                                                    StringUtil.CStr(dr["POSUUID_DETAIL"]));
            if (dtMaster.Columns.IndexOf("DISCOUNT_CODE") < 0)
                dtMaster.Columns.Add("DISCOUNT_CODE");
            if (dtMaster.Columns.IndexOf("MappingProdNo") < 0)
                dtMaster.Columns.Add("MappingProdNo");
            if (dtMaster.Columns.IndexOf("POSUUID_DETAIL") < 0)
                dtMaster.Columns.Add("POSUUID_DETAIL");
            if (dtMaster.Columns.IndexOf("ITEM_TYPE") < 0)
                dtMaster.Columns.Add("ITEM_TYPE");
            if (dtMaster.Columns.IndexOf("PRODNO") < 0)
                dtMaster.Columns.Add("PRODNO");
            if (dtMaster.Columns.IndexOf("PRODNAME") < 0)
                dtMaster.Columns.Add("PRODNAME");
            if (dtMaster.Columns.IndexOf("UNIT_PRICE") < 0)
                dtMaster.Columns.Add("UNIT_PRICE", Type.GetType("System.Int32"));
            if (dtMaster.Columns.IndexOf("DISCOUNT_PRICE") < 0)
                dtMaster.Columns.Add("DISCOUNT_PRICE", Type.GetType("System.Int32"));
            if (dtMaster.Columns.IndexOf("ItemQty") < 0)
                dtMaster.Columns.Add("ItemQty", Type.GetType("System.Int32"));
            if (dtMaster.Columns.IndexOf("Qty") < 0)
                dtMaster.Columns.Add("Qty", Type.GetType("System.Int32"));
            if (dtMaster.Columns.IndexOf("Total_Amt") < 0)
                dtMaster.Columns.Add("Total_Amt", Type.GetType("System.Int32"));

            makeGridMasterTable(listAddProd, StringUtil.CStr(dr["PRODNO"]), StringUtil.CStr(dr["POSUUID_DETAIL"]), ref dtMaster, "7");
            makeGridMasterTable(listGift, StringUtil.CStr(dr["PRODNO"]), StringUtil.CStr(dr["POSUUID_DETAIL"]), ref dtMaster, "13");
        }
        if (addProdList != null && addProdList.Length > 0)
        {
            for (int i = 0; i < this.addProdList.Length; i++)
            {
                bool hasProdExist = false;

                foreach (DataRow dr in TempTables.Rows)
                {
                    if (StringUtil.CStr(dr["PRODNO"]) == addProdList[i])
                    {
                        hasProdExist = true;
                        break;
                    }
                }

                if (!hasProdExist)
                {
                    Hashtable listAddProd = Discount_Facade.get_add_in_prod_discount(addProdList[i], logMsg.STORENO, logMsg.OPERATOR, "");
                    Hashtable listGift = Discount_Facade.get_gift_discount(addProdList[i], logMsg.STORENO, logMsg.OPERATOR, "");
                    makeGridMasterTable(listAddProd, addProdList[i], "", ref dtMaster, "7");
                    makeGridMasterTable(listGift, addProdList[i], "", ref dtMaster, "13");
                }
            }
        }
        
        gvMaster.DataSource = dtMaster;
        gvMaster.DataBind();
    }

    private void makeGridMasterTable(Hashtable list, string prodno, string POSUUID_DETAIL, ref DataTable dt, string ITEM_TYPE)
    {
        if (prodno != "")
        {
            foreach (DictionaryEntry de in list)
            {
                DataRow dr = dt.NewRow();

                dr["DISCOUNT_CODE"] = StringUtil.CStr(de.Key);
                dr["POSUUID_DETAIL"] = POSUUID_DETAIL;
                dr["MappingProdNo"] = prodno;
                dr["ITEM_TYPE"] = ITEM_TYPE;
                dr["PRODNO"] = "";
                dr["PRODNAME"] = "";
                dr["UNIT_PRICE"] = 0;
                dr["DISCOUNT_PRICE"] = 0;
                dr["ItemQty"] = 1;
                dr["Qty"] = 0;
                dr["Total_Amt"] = 0;
                dt.Rows.Add(dr);
            }
            dt.AcceptChanges();
        }
    }

    protected void gvMaster_HtmlRowCreated(object sender, ASPxGridViewTableRowEventArgs e)
    {
        if (e.RowType == GridViewRowType.Data)
        {
            ASPxComboBox cboPRODNO = e.Row.FindChildControl<ASPxComboBox>("cboPRODNO");
            ASPxTextBox hidMappingProdNo = e.Row.FindChildControl<ASPxTextBox>("hidMappingProdNo");
            ASPxTextBox hidPOSUUID_DETAIL = e.Row.FindChildControl<ASPxTextBox>("hidPOSUUID_DETAIL");
            ASPxTextBox hidDiscount_Code = e.Row.FindChildControl<ASPxTextBox>("hidDiscount_Code");

            if (cboPRODNO != null && hidMappingProdNo != null && hidPOSUUID_DETAIL != null && hidDiscount_Code != null
                && logMsg.STORENO != null && logMsg.OPERATOR != null)
            {
                Hashtable listAddProd = Discount_Facade.get_add_in_prod_discount(hidMappingProdNo.Text, logMsg.STORENO, logMsg.OPERATOR, hidPOSUUID_DETAIL.Text);
                Hashtable listGift = Discount_Facade.get_gift_discount(hidMappingProdNo.Text, logMsg.STORENO, logMsg.OPERATOR, hidPOSUUID_DETAIL.Text);
                DataTable dtProd = bindComboProdNo(listAddProd, listGift);

                if (dtProd != null && dtProd.Rows.Count > 0)
                {
                    cboPRODNO.DataSource = dtProd;
                    cboPRODNO.TextField = "PRODNO";
                    cboPRODNO.ValueField = "PRODNO";
                    cboPRODNO.DataBind();
                    cboPRODNO.SelectedIndex = 0;
                }
            }
        }
    }

    protected void gvMaster_HtmlRowPrepared(object sender, ASPxGridViewTableRowEventArgs e)
    {
        if (e.RowType == GridViewRowType.Data)
        {
            ASPxTextBox txtPRODNAME = gvMaster.FindRowCellTemplateControl(e.VisibleIndex, (GridViewDataColumn)gvMaster.Columns["PRODNAME"], "txtPRODNAME") as ASPxTextBox;
            ASPxTextBox txtUNIT_PRICE = gvMaster.FindRowCellTemplateControl(e.VisibleIndex, (GridViewDataColumn)gvMaster.Columns["UNIT_PRICE"], "txtUNIT_PRICE") as ASPxTextBox;
            ASPxTextBox txtDISCOUNT_PRICE = gvMaster.FindRowCellTemplateControl(e.VisibleIndex, (GridViewDataColumn)gvMaster.Columns["DISCOUNT_PRICE"], "txtDISCOUNT_PRICE") as ASPxTextBox;
            ASPxTextBox hidMappingProdNo = gvMaster.FindRowCellTemplateControl(e.VisibleIndex, (GridViewDataColumn)gvMaster.Columns["PRODNO"], "hidMappingProdNo") as ASPxTextBox;
            ASPxTextBox hidPOSUUID_DETAIL = gvMaster.FindRowCellTemplateControl(e.VisibleIndex, (GridViewDataColumn)gvMaster.Columns["PRODNO"], "hidPOSUUID_DETAIL") as ASPxTextBox;
            ASPxComboBox cboPRODNO = gvMaster.FindRowCellTemplateControl(e.VisibleIndex, (GridViewDataColumn)gvMaster.Columns["PRODNO"], "cboPRODNO") as ASPxComboBox;
            ASPxTextBox txtQuantity = gvMaster.FindRowCellTemplateControl(e.VisibleIndex, (GridViewDataColumn)gvMaster.Columns["Qty"], "txtQuantity") as ASPxTextBox;
            ASPxTextBox txtTotal_Amt = gvMaster.FindRowCellTemplateControl(e.VisibleIndex, (GridViewDataColumn)gvMaster.Columns["Total_Amt"], "txtTotal_Amt") as ASPxTextBox;

            string ret = "";
            if (cboPRODNO.SelectedItem != null)
                ret = getADDPRODINFO(hidMappingProdNo.Text, hidPOSUUID_DETAIL.Text, StringUtil.CStr(cboPRODNO.SelectedItem.Value), logMsg.STORENO, logMsg.OPERATOR);
            else
                ret = getADDPRODINFO(hidMappingProdNo.Text, hidPOSUUID_DETAIL.Text, "", logMsg.STORENO, logMsg.OPERATOR);
            if (ret != "")
            {
                string[] val = ret.Split(new char[] {';'});
                txtPRODNAME.Text = val[0];
                txtUNIT_PRICE.Text = val[1];
                txtDISCOUNT_PRICE.Text = val[2];
                int price = 0;
                if (val[2] != "" && NumberUtil.IsNumeric(val[2]))
                    price = int.Parse(val[2]);
                int qty = 0;
                if (txtQuantity != null && txtQuantity.Text != "" && NumberUtil.IsNumeric(txtQuantity.Text))
                    qty = int.Parse(txtQuantity.Text);
                txtTotal_Amt.Text = StringUtil.CStr(price * qty);
            }
        }
    }

    private DataTable bindComboProdNo(Hashtable listProd, Hashtable listGift)
    {
        DataTable dtResult = new DataTable();
        DataTable dt = new DataTable();
        foreach (DictionaryEntry de in listProd)
        {
            dt = de.Value as DataTable;
            if (dt != null)
                if (dtResult == null)
                    dtResult = dt.Copy();
                else
                    dtResult.Merge(dt);
        }
        foreach (DictionaryEntry de in listGift)
        {
            dt = de.Value as DataTable;
            if (dt != null)
                if (dtResult == null)
                    dtResult = dt.Copy();
                else
                    dtResult.Merge(dt);
        }
        
        return dtResult;
    }

    protected void btnConfirm_Click(object sender, EventArgs e)
    {
        string ret = "";
        string javascript = "";

        for (int i = 0; i < gvMaster.VisibleRowCount; i++)
        {
            ASPxComboBox cboPRODNO = gvMaster.FindRowCellTemplateControl(i, (GridViewDataColumn)gvMaster.Columns["PRODNO"], "cboPRODNO") as ASPxComboBox;
            ASPxTextBox txtQuantity = gvMaster.FindRowCellTemplateControl(i, (GridViewDataColumn)gvMaster.Columns["Qty"], "txtQuantity") as ASPxTextBox;
            int qty = 0;
            if (txtQuantity != null && txtQuantity.Text != "" && NumberUtil.IsNumeric(txtQuantity.Text))
                qty = int.Parse(txtQuantity.Text);
            if (qty > 0 && StringUtil.CStr(cboPRODNO.SelectedItem.Value) != "")
            {
                ASPxTextBox txtPRODNAME = gvMaster.FindRowCellTemplateControl(i, (GridViewDataColumn)gvMaster.Columns["PRODNAME"], "txtPRODNAME") as ASPxTextBox;
                ASPxTextBox txtUNIT_PRICE = gvMaster.FindRowCellTemplateControl(i, (GridViewDataColumn)gvMaster.Columns["UNIT_PRICE"], "txtUNIT_PRICE") as ASPxTextBox;
                ASPxTextBox txtDISCOUNT_PRICE = gvMaster.FindRowCellTemplateControl(i, (GridViewDataColumn)gvMaster.Columns["DISCOUNT_PRICE"], "txtDISCOUNT_PRICE") as ASPxTextBox;
                ASPxTextBox txtTotal_Amt = gvMaster.FindRowCellTemplateControl(i, (GridViewDataColumn)gvMaster.Columns["Total_Amt"], "txtTotal_Amt") as ASPxTextBox;
                ASPxTextBox hidItem_Type = gvMaster.FindRowCellTemplateControl(i, (GridViewDataColumn)gvMaster.Columns["PRODNO"], "hidItem_Type") as ASPxTextBox;
                ASPxTextBox hidDiscount_Code = gvMaster.FindRowCellTemplateControl(i, (GridViewDataColumn)gvMaster.Columns["PRODNO"], "hidDiscount_Code") as ASPxTextBox;
                int unitPrice = 0;
                if (txtUNIT_PRICE != null && txtUNIT_PRICE.Text != "" && NumberUtil.IsNumeric(txtUNIT_PRICE.Text))
                    unitPrice = int.Parse(txtUNIT_PRICE.Text);
                string strValues = StringUtil.CStr(cboPRODNO.SelectedItem.Value) + ","      //商品料號
                                    + txtPRODNAME.Text + ","                                //商品名稱
                                    + txtUNIT_PRICE.Text + ","                              //原價
                                    + txtUNIT_PRICE.Text + ","                              //加購價(2011-05-06 因為會出現一對應折扣料號抵掉差額,所以這裡要回傳原價)
                                    + txtQuantity.Text + ","                                //數量
                                    + StringUtil.CStr(unitPrice * qty) + ","                //小計
                                    + hidItem_Type.Text;                                    //ITEM_TYPE
                if (i == 0)
                    ret = strValues;
                else
                    ret += "|" + strValues;

                int disAmt = 0;
                int totalAmt = 0;
                string discoountName = Discount_Facade.get_discount_name(hidDiscount_Code.Text);

                if (hidItem_Type.Text == "13")
                {
                    disAmt = -1 * Discount_Facade.get_gift_dis_amt(hidDiscount_Code.Text, StringUtil.CStr(cboPRODNO.SelectedItem.Value));
                    totalAmt = disAmt * qty;
                    strValues = hidDiscount_Code.Text + ","      //商品料號
                              + discoountName + ","              //商品名稱
                              + StringUtil.CStr(disAmt) + ","    //原價
                              + StringUtil.CStr(disAmt) + ","    //加購價
                              + txtQuantity.Text + ","           //數量
                              + StringUtil.CStr(totalAmt) + ","  //小計
                              + "16";                            //ITEM_TYPE
                }
                else if (hidItem_Type.Text == "7")
                {
                    disAmt = -1 * Discount_Facade.get_add_prod_dis_amt(hidDiscount_Code.Text, StringUtil.CStr(cboPRODNO.SelectedItem.Value));
                    totalAmt = disAmt * qty;
                    strValues = hidDiscount_Code.Text + ","      //商品料號
                              + discoountName + ","              //商品名稱
                              + StringUtil.CStr(disAmt) + ","    //原價
                              + StringUtil.CStr(disAmt) + ","    //加購價
                              + txtQuantity.Text + ","           //數量
                              + StringUtil.CStr(totalAmt) + ","  //小計
                              + "15";                            //ITEM_TYPE
                }
                ret += "|" + strValues;
            }
        }
        javascript += "returnValue = '" + ret + "';";
        javascript += "window.close();";

        Page.ClientScript.RegisterStartupScript(this.GetType(), "CloseWindows", javascript, true);
    }

    //ajax 呼當前網頁的方式
    [System.Web.Services.WebMethod()]
    [System.Web.Script.Services.ScriptMethod()]
    static public string getADDPRODINFO(string mappingProdno, string posuuid_detail, string PRODNO, string STORE_NO, string EMP_ID)
    {
        Hashtable listAddProd = Discount_Facade.get_add_in_prod_discount(mappingProdno, STORE_NO, EMP_ID, posuuid_detail);
        Hashtable listGift = Discount_Facade.get_gift_discount(mappingProdno, STORE_NO, EMP_ID, posuuid_detail);
        DataRow drCurrent = null;
        foreach (DictionaryEntry de in listAddProd)
        {
            DataTable dt = de.Value as DataTable;

            if (dt != null && dt.Rows.Count > 0)
                foreach (DataRow dr in dt.Rows)
                {
                    if (PRODNO == StringUtil.CStr(dr["PRODNO"]))
                    {
                        drCurrent = dr;
                        break;
                    }
                }
        }
        if (drCurrent == null)
        {
            foreach (DictionaryEntry de in listGift)
            {
                DataTable dt = de.Value as DataTable;

                if (dt != null && dt.Rows.Count > 0)
                    foreach (DataRow dr in dt.Rows)
                    {
                        if (PRODNO == StringUtil.CStr(dr["PRODNO"]))
                        {
                            drCurrent = dr;
                            break;
                        }
                    }
            }
        }
        string r = "";
        if (drCurrent != null)
        {
            r = StringUtil.CStr(drCurrent["PRODNAME"]) + ";";
            if (drCurrent["UNI_PRICE"] != null && drCurrent["UNI_PRICE"] != DBNull.Value && StringUtil.CStr(drCurrent["UNI_PRICE"]) != "")
                r += StringUtil.CStr(drCurrent["UNI_PRICE"]);

            r += ";";
            if (drCurrent["SALE_PRICE"] != null && drCurrent["SALE_PRICE"] != DBNull.Value && StringUtil.CStr(drCurrent["SALE_PRICE"]) != "")
                r += StringUtil.CStr(drCurrent["SALE_PRICE"]);

            r += ";";
            if (drCurrent["ITEM_TYPE"] != null && drCurrent["ITEM_TYPE"] != DBNull.Value && StringUtil.CStr(drCurrent["ITEM_TYPE"]) != "")
                r += StringUtil.CStr(drCurrent["ITEM_TYPE"]);
        }
        
        return r;
    }
}
