using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using FET.POS.Model.Facade.FacadeImpl;
using DevExpress.Web.ASPxGridView;
using DevExpress.Web.ASPxEditors;
using Advtek.Utility;
using System.Collections.Specialized;

public partial class VSS_SAL_SAL01_choosePromotions : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
       if (!Page.IsPostBack && !Page.IsCallback)
       {
           string Promotion_Code = "";
           string Posuuid_Detail = "";
           string PromotionProdList = "";

           //**2011/04/25 Tina：前頁傳遞參數時，會先以加密處理，在此要解密。
           if (!string.IsNullOrEmpty(Request.QueryString["Param"]))
           {
               NameValueCollection qscoll = Utils.Param_UrlDecode(Request.QueryString["Param"]);
               foreach (string key in qscoll.AllKeys)
               {
                   if (key == "Promotion_Code")
                   {
                       Promotion_Code = string.Join(",", qscoll.GetValues(key));
                   }
                   else if (key == "Posuuid_Detail")
                   {
                       Posuuid_Detail = string.Join(",", qscoll.GetValues(key));
                   }
                   else if (key == "PromotionProdList")
                   {
                       PromotionProdList = string.Join(",", qscoll.GetValues(key));
                   }
               }
           }

           ddlProdList1.Enabled = false;
           ddlProdList2.Enabled = false;
           ddlProdList3.Enabled = false;
           ddlProdList4.Enabled = false;
           ddlProdList5.Enabled = false;
           ddlProdList6.Enabled = false;
           SAL01_Facade Facade01 = new SAL01_Facade();
           Promontions_Facade PromoFacade = new Promontions_Facade();
           DataTable dtPromo = null;
           if (Promotion_Code != "" && Posuuid_Detail != "")
           {
               hidPromotion_Code.Value = StringUtil.CStr(Promotion_Code);
               dtPromo = PromoFacade.Query_Promontions(hidPromotion_Code.Value, "");
               if (dtPromo != null && dtPromo.Rows.Count > 0)
               {
                   gvMaster.DataSource = dtPromo;
                   gvMaster.DataBind();
                   hidSelPromotion_Name.Value = StringUtil.CStr(dtPromo.Rows[0]["PROMO_NAME"]);
                   queryDiv.Visible = false;
                   queryTable.Visible = false;
               }
               hidSelPromotion_Code.Value = hidPromotion_Code.Value;
               if (PromotionProdList != "")
               {
                   string[] valList = PromotionProdList.Split(';');
                   for (int i = 0; i < valList.Length; i++)
                   {
                       string[] pProdList = valList[i].Split('|');
                       if (pProdList[0] == hidPromotion_Code.Value)
                       {
                           hidOldProdList.Value = pProdList[1];
                       }
                   }
               }
           }
           else
           {
               hidPromotion_Code.Value = "";
               dtPromo = PromoFacade.Query_Promontions(hidPromotion_Code.Value, "");
               if (dtPromo != null && dtPromo.Rows.Count > 0)
               {
                   gvMaster.DataSource = dtPromo;
                   gvMaster.DataBind();
               }
           }
       }
    }

    #region 拆解外部傳入的Prod List
    /// <summary>
    /// 拆解外部傳入的Prod List,將商品分別列在所屬層級的下拉式選單上
    /// </summary>
    private void showOldProdList()
    {
        Product_Facade prodFacade = new Product_Facade();
        DataTable dtProd;
        string[] prodList = hidOldProdList.Value.Split('^');
        for (int j = 0; j < prodList.Length; j++)
        {
            switch (j)
            {
                case 0:
                    if (ddlProdList1.Items.FindByValue(prodList[0]) == null)
                    {
                        dtProd = prodFacade.Query_ProductInfo(prodList[0]);
                        if (dtProd != null && dtProd.Rows.Count > 0 && dtProd.Rows[0]["PRODNAME"] != null)
                            ddlProdList1.Items.Add(new ListItem(StringUtil.CStr(dtProd.Rows[0]["PRODNAME"]), prodList[0]));
                        else
                            ddlProdList1.Items.Add(new ListItem(prodList[0], prodList[0]));
                        ddlProdList1.SelectedValue = prodList[0];
                    }
                    else
                    {
                        ddlProdList1.SelectedValue = prodList[0];
                        bindProductChooseInfo(1, "", ddlProdList1, ddlProdList2, lblProdName1, lblInvQty1, lblPrice1, hidSelectProd1);
                        ddlProdList3.Enabled = false;
                        ddlProdList4.Enabled = false;
                        ddlProdList5.Enabled = false;
                        ddlProdList6.Enabled = false;
                    }
                    break;
                case 1:
                    if (ddlProdList2.Items.FindByValue(prodList[1]) == null)
                    {
                        dtProd = prodFacade.Query_ProductInfo(prodList[1]);
                        if (dtProd != null && dtProd.Rows.Count > 0 && dtProd.Rows[0]["PRODNAME"] != null)
                            ddlProdList2.Items.Add(new ListItem(StringUtil.CStr(dtProd.Rows[0]["PRODNAME"]), prodList[1]));
                        else
                            ddlProdList2.Items.Add(new ListItem(prodList[1], prodList[1]));
                        if (ddlProdList2.Items.Count == 1)
                        {
                            ddlProdList2.Items.Insert(0, new ListItem("--請選擇--", ""));
                            ddlProdList2.Enabled = true;
                            ddlProdList2.SelectedIndex = 0;
                        }
                        ddlProdList2.SelectedValue = prodList[1];
                    }
                    else
                    {
                        ddlProdList2.SelectedValue = prodList[1];
                        bindProductChooseInfo(2, prodList[0], ddlProdList2, ddlProdList3, lblProdName2, lblInvQty2, lblPrice2, hidSelectProd2);
                        ddlProdList4.Enabled = false;
                        ddlProdList5.Enabled = false;
                        ddlProdList6.Enabled = false;
                    }
                    break;
                case 2:
                    if (ddlProdList3.Items.FindByValue(prodList[2]) == null)
                    {
                        dtProd = prodFacade.Query_ProductInfo(prodList[2]);
                        if (dtProd != null && dtProd.Rows.Count > 0 && dtProd.Rows[0]["PRODNAME"] != null)
                            ddlProdList3.Items.Add(new ListItem(StringUtil.CStr(dtProd.Rows[0]["PRODNAME"]), prodList[2]));
                        else
                            ddlProdList3.Items.Add(new ListItem(prodList[2], prodList[2]));
                        if (ddlProdList3.Items.Count == 1)
                        {
                            ddlProdList3.Items.Insert(0, new ListItem("--請選擇--", ""));
                            ddlProdList3.Enabled = true;
                            ddlProdList3.SelectedIndex = 0;
                        }
                        ddlProdList3.SelectedValue = prodList[2];
                    }
                    else
                    {
                        ddlProdList3.SelectedValue = prodList[2];
                        bindProductChooseInfo(3, prodList[0] + "|" + prodList[1], ddlProdList3, ddlProdList4, lblProdName3, lblInvQty3, lblPrice3,
                                                 hidSelectProd3);
                        ddlProdList5.Enabled = false;
                        ddlProdList6.Enabled = false;
                    }
                    break;
                case 3:
                    if (ddlProdList4.Items.FindByValue(prodList[3]) == null)
                    {
                        dtProd = prodFacade.Query_ProductInfo(prodList[3]);
                        if (dtProd != null && dtProd.Rows.Count > 0 && dtProd.Rows[0]["PRODNAME"] != null)
                            ddlProdList4.Items.Add(new ListItem(StringUtil.CStr(dtProd.Rows[0]["PRODNAME"]), prodList[3]));
                        else
                            ddlProdList4.Items.Add(new ListItem(prodList[3], prodList[3]));
                        if (ddlProdList4.Items.Count == 1)
                        {
                            ddlProdList4.Items.Insert(0, new ListItem("--請選擇--", ""));
                            ddlProdList4.Enabled = true;
                            ddlProdList4.SelectedIndex = 0;
                        }
                        ddlProdList4.SelectedValue = prodList[3];
                    }
                    else
                    {
                        ddlProdList4.SelectedValue = prodList[3];
                        bindProductChooseInfo(4, prodList[0] + "|" + prodList[1] + "|" + prodList[2], ddlProdList4, ddlProdList5, lblProdName4,
                                                 lblInvQty4, lblPrice4, hidSelectProd4);
                        if (ddlProdList5.Items.Count == 1)
                        {
                            ddlProdList5.Items.Insert(0, new ListItem("--請選擇--", ""));
                            ddlProdList5.Enabled = true;
                            ddlProdList5.SelectedIndex = 0;
                        }
                        ddlProdList6.Enabled = false;
                    }
                    break;
                case 4:
                    if (ddlProdList5.Items.FindByValue(prodList[4]) == null)
                    {
                        dtProd = prodFacade.Query_ProductInfo(prodList[4]);
                        if (dtProd != null && dtProd.Rows.Count > 0 && dtProd.Rows[0]["PRODNAME"] != null)
                            ddlProdList5.Items.Add(new ListItem(StringUtil.CStr(dtProd.Rows[0]["PRODNAME"]), prodList[4]));
                        else
                            ddlProdList5.Items.Add(new ListItem(prodList[4], prodList[4]));
                        if (ddlProdList6.Items.Count == 1)
                        {
                            ddlProdList6.Items.Insert(0, new ListItem("--請選擇--", ""));
                            ddlProdList6.Enabled = true;
                            ddlProdList6.SelectedIndex = 0;
                        }
                        ddlProdList5.SelectedValue = prodList[4];
                    }
                    else
                    {
                        ddlProdList5.SelectedValue = prodList[4];
                        bindProductChooseInfo(5, prodList[0] + "|" + prodList[1] + "|" + prodList[2] + "|" + prodList[3], ddlProdList5, ddlProdList6,
                                                 lblProdName5, lblInvQty5, lblPrice5, hidSelectProd5);
                    }
                    break;
                case 5:
                    if (ddlProdList6.Items.FindByValue(prodList[5]) == null)
                    {
                        dtProd = prodFacade.Query_ProductInfo(prodList[5]);
                        if (dtProd != null && dtProd.Rows.Count > 0 && dtProd.Rows[0]["PRODNAME"] != null)
                            ddlProdList6.Items.Add(new ListItem(StringUtil.CStr(dtProd.Rows[0]["PRODNAME"]), prodList[5]));
                        else
                            ddlProdList6.Items.Add(new ListItem(prodList[5], prodList[5]));
                        ddlProdList6.SelectedValue = prodList[5];
                    }
                    else
                    {
                        ddlProdList6.SelectedValue = prodList[5];
                        bindProductChooseInfo(6, prodList[0] + "|" + prodList[1] + "|" + prodList[2] + "|" + prodList[3] + "|" + prodList[4],
                                                 ddlProdList6, null, lblProdName6, lblInvQty6, lblPrice6, hidSelectProd6);
                    }
                    break;
            }
        }
    }
    #endregion

    #region 針對DropDownList系列的Bind資料相關函式
    /// <summary>
    /// 下拉選單選了之後要Bind後面的商品資料
    /// </summary>
    /// <param name="MyDropDownList"></param>
    /// <param name="lblName"></param>
    /// <param name="lblStock"></param>
    /// <param name="lblPrice"></param>
    private void bindProductChooseInfo(int level, string prevProdSelList, DropDownList ddlCurrent, DropDownList ddlNext, 
                                        Label lblProdName, Label lblInvQty, Label lblPrice, HiddenField hidSelectProd)
    {
        lblProdName.Text = "";
        lblInvQty.Text = "";
        lblPrice.Text = "";
        SAL01_Facade Facade01 = new SAL01_Facade();
        lblProdName.Text = ddlCurrent.SelectedItem.Text;
        string strViewStatInd = "MixPromotionLevel" + StringUtil.CStr(level);
        DataTable dtProd = null;
        if (ViewState[strViewStatInd] != null)
            dtProd = (DataTable)ViewState[strViewStatInd];
        else
            dtProd = Facade01.getMixPromotion_Item(logMsg.STORENO, hidSelPromotion_Code.Value, prevProdSelList);

        if (dtProd != null && dtProd.Rows.Count > 0)
        {
            if (ddlCurrent.SelectedIndex - 1 < dtProd.Rows.Count)
            {
                if (dtProd.Rows[ddlCurrent.SelectedIndex - 1]["INVENTORY_AMOUNT"] != null)
                    lblInvQty.Text = StringUtil.CStr(dtProd.Rows[ddlCurrent.SelectedIndex - 1]["INVENTORY_AMOUNT"]);
                if (dtProd.Rows[ddlCurrent.SelectedIndex - 1]["ITEM_PRICE"] != null)
                    lblPrice.Text = StringUtil.CStr(dtProd.Rows[ddlCurrent.SelectedIndex - 1]["ITEM_PRICE"]);

                hidSelectProd.Value = ddlCurrent.SelectedItem.Value + "^" + ddlCurrent.SelectedItem.Text + "^" + lblPrice.Text;

                ddlNext.Items.Clear();
                if (level < 6 && dtProd.Rows[ddlCurrent.SelectedIndex - 1]["EOF_STATUS"] != null &&
                    StringUtil.CStr(dtProd.Rows[ddlCurrent.SelectedIndex - 1]["EOF_STATUS"]) != "Y")
                {
                    DataTable dtProdNext = null;
                    if (prevProdSelList == "")
                    {
                        if (hidSelPromotion_Code.Value == "")
                            dtProdNext = Facade01.getMixPromotion_Item(logMsg.STORENO, hidPromotion_Code.Value, ddlCurrent.SelectedItem.Value);
                        else
                            dtProdNext = Facade01.getMixPromotion_Item(logMsg.STORENO, hidSelPromotion_Code.Value, ddlCurrent.SelectedItem.Value);
                    }
                    else
                    {
                        if (hidSelPromotion_Code.Value == "")
                            dtProdNext = Facade01.getMixPromotion_Item(logMsg.STORENO, hidPromotion_Code.Value, prevProdSelList + "|" + ddlCurrent.SelectedItem.Value);
                        else
                            dtProdNext = Facade01.getMixPromotion_Item(logMsg.STORENO, hidSelPromotion_Code.Value, prevProdSelList + "|" + ddlCurrent.SelectedItem.Value);
                    }

                    if (dtProdNext != null && dtProdNext.Rows.Count > 0)
                    {
                        foreach (DataRow dr in dtProdNext.Rows)
                        {
                            if (dr["ITEM_CODE"] != null && StringUtil.CStr(dr["ITEM_CODE"]) != ""
                                && dr["ITEM_NAME"] != null && StringUtil.CStr(dr["ITEM_CODE"]) != "")
                                ddlNext.Items.Add(new ListItem(StringUtil.CStr(dr["ITEM_NAME"]), StringUtil.CStr(dr["ITEM_CODE"])));
                        }
                        ddlNext.Items.Insert(0, "--請選擇--");
                        ddlNext.Enabled = true;
                        ddlNext.SelectedIndex = 0;
                    }
                }
            }
        }
    }
    #endregion

    protected void gvMaster_HtmlRowCreated(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewTableRowEventArgs e)
    {
        if (e.RowType == GridViewRowType.Data)
        {
            ASPxRadioButton rdoButton = e.Row.FindChildControl<ASPxRadioButton>("rdoButton");
            if (e.VisibleIndex == 0 && hidPromotion_Code.Value != null && hidPromotion_Code.Value != "")
                rdoButton.Checked = true;
        }
    }

    protected void gvMaster_HtmlRowPrepared(object sender, ASPxGridViewTableRowEventArgs e)
    {
        if (e.RowType == GridViewRowType.Data && e.VisibleIndex == gvMaster.FocusedRowIndex)
        {
            ASPxRadioButton rdoButton = gvMaster.FindRowCellTemplateControl(e.VisibleIndex, (GridViewDataColumn)gvMaster.Columns[0], "rdoButton") as ASPxRadioButton;
            rdoButton.Checked = true;
        }
    }

    protected void gvMaster_PageIndexChanged(object sender, EventArgs e)
    {
        clearAllLable();
        DataTable dt = (DataTable)gvMaster.DataSource;
        if (dt == null || dt.Rows.Count == 0)
        {
            Promontions_Facade PromoFacade = new Promontions_Facade();
            if (txtQueryPromotion_Code.Visible)
                dt = PromoFacade.Query_Promontions(txtQueryPromotion_Code.Text, txtQueryPromotion_Name.Text, true);
            else 
                dt = PromoFacade.Query_Promontions("", "");
            if (dt != null && dt.Rows.Count > 0)
            {
                gvMaster.DataSource = dt;
                gvMaster.DataBind();
            }
            gvMaster.Selection.UnselectAll();
        }
    }

    protected void gvMaster_FocusedRowChanged(object sender, EventArgs e)
    {
        if (gvMaster.FocusedRowIndex > -1)
        {
            clearAllLable();
            object key = gvMaster.GetRowValues(gvMaster.FocusedRowIndex, gvMaster.KeyFieldName);
            if (key != null)
            {
                Promontions_Facade promoFacade = new Promontions_Facade();
                SAL01_Facade Facade01 = new SAL01_Facade();
                DataTable dtPromo = promoFacade.Query_Promontions(StringUtil.CStr(key));

                if (dtPromo != null && dtPromo.Rows.Count > 0)
                {
                    hidSelPromotion_Code.Value = StringUtil.CStr(dtPromo.Rows[0]["PROMO_NO"]);
                    hidSelPromotion_Name.Value = StringUtil.CStr(dtPromo.Rows[0]["PROMO_NAME"]);
                    DataTable dtProd = Facade01.getMixPromotion_Item(logMsg.STORENO, hidSelPromotion_Code.Value, "");
                    if (dtProd != null && dtProd.Rows.Count > 0)
                    {
                        ddlProdList1.Items.Clear();
                        foreach (DataRow dr in dtProd.Rows)
                        {
                            if (dr["ITEM_CODE"] != null && StringUtil.CStr(dr["ITEM_CODE"]) != ""
                                && dr["ITEM_NAME"] != null && StringUtil.CStr(dr["ITEM_CODE"]) != "")
                                ddlProdList1.Items.Add(new ListItem(StringUtil.CStr(dr["ITEM_NAME"]), StringUtil.CStr(dr["ITEM_CODE"])));
                        }
                        ddlProdList1.Items.Insert(0, new ListItem("--請選擇--", ""));
                        ddlProdList1.Enabled = true;
                        ddlProdList1.SelectedIndex = 0;
                    }
                }
                ddlProdList2.Enabled = false;
                ddlProdList3.Enabled = false;
                ddlProdList4.Enabled = false;
                ddlProdList5.Enabled = false;
                ddlProdList6.Enabled = false;
                if (hidOldProdList.Value != "")
                    showOldProdList();
            }   
        }
    }

    protected void ddlProdList1_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlProdList1.SelectedItem.Value != null && ddlProdList1.SelectedItem.Value != "")
        {
            bindProductChooseInfo(1, "", ddlProdList1, ddlProdList2, lblProdName1, lblInvQty1, lblPrice1, hidSelectProd1);
            ddlProdList3.Items.Clear();
            ddlProdList4.Items.Clear();
            ddlProdList5.Items.Clear();
            ddlProdList6.Items.Clear();
            ddlProdList3.Enabled = false;
            ddlProdList4.Enabled = false;
            ddlProdList5.Enabled = false;
            ddlProdList6.Enabled = false;
        }
        else
        {
            lblProdName1.Text = "";
            lblInvQty1.Text = "";
            lblPrice1.Text = "";
        }
    }
    protected void ddlProdList2_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlProdList2.SelectedItem.Value != null && ddlProdList2.SelectedItem.Value != "")
        {
            bindProductChooseInfo(2, ddlProdList1.SelectedItem.Value, ddlProdList2, ddlProdList3, lblProdName2, lblInvQty2, lblPrice2, hidSelectProd2);
            ddlProdList4.Items.Clear();
            ddlProdList5.Items.Clear();
            ddlProdList6.Items.Clear();
            ddlProdList4.Enabled = false;
            ddlProdList5.Enabled = false;
            ddlProdList6.Enabled = false;
        }
        else
        {
            lblProdName2.Text = "";
            lblInvQty2.Text = "";
            lblPrice2.Text = "";
        }
    }
    protected void ddlProdList3_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlProdList3.SelectedItem.Value != null && ddlProdList3.SelectedItem.Value != "")
        {
            string prevSelProdList = ddlProdList1.SelectedItem.Value + "|" + ddlProdList2.SelectedItem.Value;
            bindProductChooseInfo(3, prevSelProdList, ddlProdList3, ddlProdList4, lblProdName3, lblInvQty3, lblPrice3, hidSelectProd3);
            ddlProdList5.Items.Clear();
            ddlProdList6.Items.Clear();
            ddlProdList5.Enabled = false;
            ddlProdList6.Enabled = false;
        }
        else
        {
            lblProdName3.Text = "";
            lblInvQty3.Text = "";
            lblPrice3.Text = "";
        }
    }
    protected void ddlProdList4_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlProdList4.SelectedItem.Value != null && ddlProdList4.SelectedItem.Value != "")
        {
            string prevSelProdList = ddlProdList1.SelectedItem.Value + "|" + ddlProdList2.SelectedItem.Value + "|" + ddlProdList3.SelectedItem.Value;
            bindProductChooseInfo(4, prevSelProdList, ddlProdList4, ddlProdList5, lblProdName4, lblInvQty4, lblPrice4, hidSelectProd4);
            ddlProdList6.Items.Clear();
            ddlProdList6.Enabled = false;
        }
        else
        {
            lblProdName4.Text = "";
            lblInvQty4.Text = "";
            lblPrice4.Text = "";
        }
    }
    protected void ddlProdList5_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlProdList5.SelectedItem.Value != null && ddlProdList5.SelectedItem.Value != "")
        {
            string prevSelProdList = ddlProdList1.SelectedItem.Value + "|" + ddlProdList2.SelectedItem.Value + "|" + ddlProdList3.SelectedItem.Value
                                        + "|" + ddlProdList4.SelectedItem.Value;
            bindProductChooseInfo(5, prevSelProdList, ddlProdList5, ddlProdList6, lblProdName5, lblInvQty5, lblPrice5, hidSelectProd5);
        }
        else
        {
            lblProdName5.Text = "";
            lblInvQty5.Text = "";
            lblPrice5.Text = "";
        }
    }

    protected void ddlProdList6_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlProdList6.SelectedItem.Value != null && ddlProdList6.SelectedItem.Value != "")
        {
            string prevSelProdList = ddlProdList1.SelectedItem.Value + "|" + ddlProdList2.SelectedItem.Value + "|" + ddlProdList3.SelectedItem.Value
                                        + "|" + ddlProdList4.SelectedItem.Value + "|" + ddlProdList5.SelectedItem.Value;
            bindProductChooseInfo(6, prevSelProdList, ddlProdList6, null, lblProdName6, lblInvQty6, lblPrice6, hidSelectProd6);
        }
        else
        {
            lblProdName6.Text = "";
            lblInvQty6.Text = "";
            lblPrice6.Text = "";
        }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        clearAllLable();
        DataTable dtPromotion = new Promontions_Facade().Query_Promontions(txtQueryPromotion_Code.Text, txtQueryPromotion_Name.Text, true);
        gvMaster.DataSource = dtPromotion;
        gvMaster.DataBind();
        if (dtPromotion != null && dtPromotion.Rows.Count > 0)
        {
            Promontions_Facade promoFacade = new Promontions_Facade();
            SAL01_Facade Facade01 = new SAL01_Facade();
            if (gvMaster.FocusedRowIndex > -1)
            {
                hidSelPromotion_Code.Value = StringUtil.CStr(dtPromotion.Rows[gvMaster.FocusedRowIndex]["PROMO_NO"]);
                hidSelPromotion_Name.Value = StringUtil.CStr(dtPromotion.Rows[gvMaster.FocusedRowIndex]["PROMO_NAME"]);
            }
            else
            {
                hidSelPromotion_Code.Value = "";
                hidSelPromotion_Name.Value = "";
            }
            DataTable dtProd = Facade01.getMixPromotion_Item(logMsg.STORENO, hidSelPromotion_Code.Value, "");
            if (dtProd != null && dtProd.Rows.Count > 0)
            {
                ddlProdList1.Items.Clear();
                foreach (DataRow dr in dtProd.Rows)
                {
                    if (dr["ITEM_CODE"] != null && StringUtil.CStr(dr["ITEM_CODE"]) != ""
                        && dr["ITEM_NAME"] != null && StringUtil.CStr(dr["ITEM_CODE"]) != "")
                        ddlProdList1.Items.Add(new ListItem(StringUtil.CStr(dr["ITEM_NAME"]), StringUtil.CStr(dr["ITEM_CODE"])));
                }
                ddlProdList1.Items.Insert(0, new ListItem("--請選擇--", ""));
                ddlProdList1.Enabled = true;
                ddlProdList1.SelectedIndex = 0;
            }
            ddlProdList2.Enabled = false;
            ddlProdList3.Enabled = false;
            ddlProdList4.Enabled = false;
            ddlProdList5.Enabled = false;
            ddlProdList6.Enabled = false;
            if (hidOldProdList.Value != "")
                showOldProdList();
        }
    }

    private void clearAllLable()
    {
        ddlProdList1.Items.Clear();
        ddlProdList2.Items.Clear();
        ddlProdList3.Items.Clear();
        ddlProdList4.Items.Clear();
        ddlProdList5.Items.Clear();
        ddlProdList6.Items.Clear();
        lblInvQty1.Text = "";
        lblInvQty2.Text = "";
        lblInvQty3.Text = "";
        lblInvQty4.Text = "";
        lblInvQty5.Text = "";
        lblInvQty6.Text = "";
        lblPrice1.Text = "";
        lblPrice2.Text = "";
        lblPrice3.Text = "";
        lblPrice4.Text = "";
        lblPrice5.Text = "";
        lblPrice6.Text = "";
        lblProdName1.Text = "";
        lblProdName2.Text = "";
        lblProdName3.Text = "";
        lblProdName4.Text = "";
        lblProdName5.Text = "";
        lblProdName6.Text = "";
    }
}
