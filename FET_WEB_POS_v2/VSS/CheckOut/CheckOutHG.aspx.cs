using System;
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

public partial class VSS_CheckOut_CheckOutHG : BasePage
{
    //private string QrySTORE_NO = "R2121";
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

    int RequestTotalAmount
    {
        get
        {
            string TOTAL_AMOUNT = "";

            //**2011/04/26 Tina：前頁傳遞參數時，會先以加密處理，在此要解密。
            if (!string.IsNullOrEmpty(Request.QueryString["Param"]))
            {
                NameValueCollection qscoll = Utils.Param_UrlDecode(Request.QueryString["Param"]);
                foreach (string key in qscoll.AllKeys)
                {
                    if (key == "TOTAL_AMOUNT")
                    {
                        TOTAL_AMOUNT = string.Join(",", qscoll.GetValues(key));
                        break;
                    }
                }
            }


            int r = Convert.ToInt32(TOTAL_AMOUNT == "" ? "0" : TOTAL_AMOUNT);
            return r;
        }
    }

    string strTRAN_DATE
    {
        get
        {
            string TRAN_DATE = "";

            //**2011/04/26 Tina：前頁傳遞參數時，會先以加密處理，在此要解密。
            if (!string.IsNullOrEmpty(Request.QueryString["Param"]))
            {
                NameValueCollection qscoll = Utils.Param_UrlDecode(Request.QueryString["Param"]);
                foreach (string key in qscoll.AllKeys)
                {
                    if (key == "TRAN_DATE")
                    {
                        TRAN_DATE = string.Join(",", qscoll.GetValues(key));
                        break;
                    }
                }
            }

            return TRAN_DATE;

            //string r = Request["TRAN_DATE"] == null ? "" : StringUtil.CStr(Request["TRAN_DATE"]);
            //if (r == null)
            //{
            //    r = "";
            //}
            //return r;
        }
    }

    string[] addProdList
    {
        get
        {
            string addProdList = "";

            //**2011/04/26 Tina：前頁傳遞參數時，會先以加密處理，在此要解密。
            if (!string.IsNullOrEmpty(Request.QueryString["Param"]))
            {
                NameValueCollection qscoll = Utils.Param_UrlDecode(Request.QueryString["Param"]);
                foreach (string key in qscoll.AllKeys)
                {
                    if (key == "addProdList")
                    {
                        addProdList = string.Join(",", qscoll.GetValues(key));
                        break;
                    }
                }
            }

            return addProdList.Split("^".ToCharArray());

            //string r = Request["addProdList"] == null ? "" : StringUtil.CStr(Request["addProdList"]);
            //if (r == null)
            //{
            //    r = "";
            //}
            //return r.Split("^".ToCharArray());
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ClientScript.RegisterHiddenField("TOTAL_AMOUNT", StringUtil.CStr(RequestTotalAmount));
        }
    }

    private void BindGridViewData(string step)
    {
        //if (ViewState["dtPromotion"] == null || ViewState["dtProduct"] == null || ViewState["dtConvert"] == null || ViewState["dtExtraSale"] == null)
        //{
            DataTable dt1 = GetGridView1EmptyData();
            DataTable dt2 = GetGridView2EmptyData();
            DataTable dt3 = GetGridView3EmptyData();
            DataTable dt4 = GetGridView4EmptyData();

            #region 舊的程式 //註解
            //DataTable dtPromos = TempTables.DefaultView.ToTable(true, "PROMOTION_CODE", "MSISDN");
            //foreach (DataRow drTemp in dtPromos.Rows)
            //{
            //    if (!string.IsNullOrEmpty(StringUtil.CStr(drTemp["PROMOTION_CODE"]))) //未結清單的資料
            //    {
            //        #region 處理促銷活動的折抵活動
            //        DataTable dtPromotion = new SAL01_Facade().getPromotionAction(StringUtil.CStr(drTemp["PROMOTION_CODE"]), logMsg.STORENO, strTRAN_DATE);
            //        bool UsePromo = true;  //是否可折抵此促銷活動

            //        //有查詢到資料，取促銷活動
            //        if (dtPromotion.Rows.Count > 0)
            //        {
            //            #region 取得促銷活動
            //            foreach (DataRow drPromo in dtPromotion.Rows)
            //            {
            //                DataRow NewRow = dt2.NewRow();

            //                //為Y，需判斷門號是否為會員
            //                if (StringUtil.CStr(drPromo["MEMBER_CHECK_FLAG"]) == "Y")
            //                {
            //                    //若是會員，則回傳已折抵的次數
            //                    DataTable dtUSE_COUNT = new SAL01_Facade().getUSE_COUNT(StringUtil.CStr(drPromo["ACTIVITY_ID"]), StringUtil.CStr(drTemp["MSISDN"]));
            //                    if (dtUSE_COUNT.Rows.Count > 0)
            //                    {
            //                        string strUSE_COUNT = StringUtil.CStr(dtUSE_COUNT.Rows[0]["USE_COUNT"]);
            //                        strUSE_COUNT = string.IsNullOrEmpty(strUSE_COUNT) ? "0" : strUSE_COUNT;

            //                        if (!string.IsNullOrEmpty(StringUtil.CStr(drPromo["USE_COUNT"])))
            //                        {
            //                            //累計折抵次數需小於最大折抵次數，才能繼續折抵
            //                            if (Convert.ToInt16(strUSE_COUNT) < Convert.ToInt16(drPromo["USE_COUNT"]))
            //                            {
            //                                UsePromo = true;
            //                                NewRow["門號"] = StringUtil.CStr(drTemp["MSISDN"]);  //銷售結帳時，需依此來Update HG_CONVERT_MEMBER_LIST 的折抵次數(Use_Count)
            //                            }
            //                            else  //已到達最大折抵次數，所以不能使用這個HappyGo折抵(進行查詢 單商品折抵活動)
            //                            {
            //                                UsePromo = false;
            //                            }
            //                        }
            //                        else  //沒有最大折抵次數，表示可以無限次數折抵
            //                        {
            //                            UsePromo = true;
            //                        }
            //                    }
            //                    else  //要為會員才能有折抵，但此門號並不是會員，所以不能使用這個HappyGo折抵(進行查詢 單商品折抵活動)
            //                    {
            //                        UsePromo = false;
            //                    }
            //                }
            //                else  //不需判斷是否為會員
            //                {
            //                    UsePromo = true;
            //                }

            //                if (UsePromo)
            //                {
            //                    NewRow["折扣料號"] = StringUtil.CStr(drPromo["PROMOTION_CODE"]);
            //                    NewRow["折扣名稱"] = StringUtil.CStr(drPromo["promo_name"]);
            //                    NewRow["活動代碼"] = StringUtil.CStr(drPromo["ACTIVITY_ID"]);
            //                    NewRow["活動名稱"] = StringUtil.CStr(drPromo["ACTIVITY_NAME"]);
            //                    NewRow["項目名稱"] = StringUtil.CStr(drPromo["EXCHANGE_NAME"]);
            //                    NewRow["兌換點數"] = StringUtil.CStr(drPromo["DIVIDABLE_POINT"]);
            //                    NewRow["兌換金額"] = StringUtil.CStr(drPromo["CONVERT_CURRENCY"]);
            //                    NewRow["活動折抵金額上限"] = StringUtil.CStr(drPromo["U_BOUND"]);
            //                    NewRow["數量"] = "0";
            //                    dt2.Rows.Add(NewRow);

            //                }
            //            }
            //            #endregion 取得促銷活動
            //        }
            //        else  //查無資料，進行查詢 單商品折抵活動
            //        {
            //            UsePromo = false;
            //        }

            //        if (!UsePromo)  //該促銷方案無對應的兌點規則，則查詢該促銷方案下的"商品"有無適用的兌點規則
            //        {
            //            DataRow[] dtProds = TempTables.Select("PROMOTION_CODE = '" + StringUtil.CStr(drTemp["PROMOTION_CODE"]) + "'");
            //            foreach (DataRow drP in dtProds)
            //            {
            //                #region 取得單商品折抵活動
            //                DataTable dtProduct = new SAL01_Facade().getProductAction(StringUtil.CStr(drP["PRODNO"]), logMsg.STORENO, strTRAN_DATE);
            //                foreach (DataRow drProd in dtProduct.Rows)
            //                {
            //                    DataRow NewRow2 = dt1.NewRow();
            //                    NewRow2["折扣料號"] = StringUtil.CStr(drProd["ACTIVITY_ID"]);
            //                    NewRow2["折扣名稱"] = StringUtil.CStr(drProd["ACTIVITY_NAME"]);
            //                    NewRow2["活動代碼"] = StringUtil.CStr(drProd["ACTIVITY_ID"]);
            //                    NewRow2["活動名稱"] = StringUtil.CStr(drProd["ACTIVITY_NAME"]);
            //                    NewRow2["項目名稱"] = StringUtil.CStr(drProd["EXCHANGE_NAME"]);
            //                    NewRow2["兌換點數"] = StringUtil.CStr(drProd["DIVIDABLE_POINT"]);
            //                    NewRow2["兌換金額"] = StringUtil.CStr(drProd["CONVERT_CURRENCY"]);
            //                    NewRow2["活動折抵金額上限"] = StringUtil.CStr(drProd["U_BOUND"]);
            //                    NewRow2["數量"] = "0";
            //                    dt1.Rows.Add(NewRow2);

            //                }
            //                #endregion 取得單商品折抵活動
            //            }
            //        }
            //        #endregion
            //    }
            //}
            #endregion 舊的程式

            // 第一步:先取得PROMOTION_CODE,不可重複
            DataTable dtPromotions = TempTables.DefaultView.ToTable(true, "PROMOTION_CODE");
            foreach (DataRow drTemp in dtPromotions.Rows)
            {
                if (!string.IsNullOrEmpty(StringUtil.CStr(drTemp["PROMOTION_CODE"]))) //未結清單的資料
                {
                    if (step == "exchange")
                    {
                        #region 處理促銷的兌點活動
                        bool UsePromo = true;  //是否可兌點此促銷
                        DataTable dtPromotion = new SAL01_Facade().getPromotionAction(StringUtil.CStr(drTemp["PROMOTION_CODE"]), logMsg.STORENO, strTRAN_DATE);
                        if (dtPromotion.Rows.Count > 0)
                        {
                            #region 取得促銷資料
                            foreach (DataRow drPromo in dtPromotion.Rows)
                            {
                                DataRow NewRow = dt2.NewRow();
                                //為Y，需做名單檢核
                                if (StringUtil.CStr(drPromo["MEMBER_CHECK_FLAG"]) == "Y")
                                {
                                    //判斷此門號是否在名單內
                                    DataTable dtPromosMSISDN = TempTables.DefaultView.ToTable(true, "PROMOTION_CODE", "MSISDN");
                                    foreach (DataRow drTempMSISDN in dtPromosMSISDN.Rows)
                                    {
                                        if (StringUtil.CStr(drTemp["PROMOTION_CODE"]) == StringUtil.CStr(drTempMSISDN["PROMOTION_CODE"]))
                                        {
                                            DataTable dtUSE_COUNT = new SAL01_Facade().getUSE_COUNT(StringUtil.CStr(drPromo["ACTIVITY_ID"]), StringUtil.CStr(drTempMSISDN["MSISDN"]));
                                            if (dtUSE_COUNT.Rows.Count > 0)
                                            {
                                                UsePromo = true;
                                                break;
                                            }
                                            else  //但此門號不在名單內，所以不能使用這個HappyGo折抵(進行查詢 單商品折抵活動)
                                            {
                                                UsePromo = false;
                                            }
                                        }   //不屬於此PROMOTION_CODE
                                        else
                                        {
                                            UsePromo = false;
                                        }
                                    }
                                }
                                else
                                {
                                    UsePromo = true;
                                }

                                if (UsePromo)
                                {
                                    NewRow["折扣料號"] = StringUtil.CStr(drPromo["ACTIVITY_NO"]);
                                    NewRow["折扣名稱"] = StringUtil.CStr(drPromo["ACTIVITY_NAME"]);
                                    NewRow["促銷代碼"] = StringUtil.CStr(drPromo["PROMOTION_CODE"]);
                                    NewRow["促銷名稱"] = StringUtil.CStr(drPromo["promo_name"]);
                                    NewRow["項目名稱"] = StringUtil.CStr(drPromo["EXCHANGE_NAME"]);
                                    NewRow["兌換點數"] = StringUtil.CStr(drPromo["DIVIDABLE_POINT"]);
                                    NewRow["兌換金額"] = StringUtil.CStr(drPromo["CONVERT_CURRENCY"]);
                                    NewRow["活動折抵金額上限"] = StringUtil.CStr(drPromo["U_BOUND"]);
                                    NewRow["數量"] = "0";
                                    NewRow["折扣方式"] = StringUtil.CStr(drPromo["PAY_OFF_TYPE"]);
                                    dt2.Rows.Add(NewRow);
                                }
                            }
                            #endregion 取得促銷資料
                        }
                        else  //查無資料，進行查詢 單商品兌點活動
                        {
                            UsePromo = false;
                        }

                        if (!UsePromo)  //該促銷方案無對應的兌點規則，則查詢該促銷方案下的"商品"有無適用的兌點規則
                        {
                            DataRow[] dtProds = TempTables.Select("PROMOTION_CODE = '" + StringUtil.CStr(drTemp["PROMOTION_CODE"]) + "'");
                            foreach (DataRow drP in dtProds)
                            {
                                #region 取得單商品兌點活動
                                DataTable dtProduct = new SAL01_Facade().getProductAction(StringUtil.CStr(drP["PRODNO"]), logMsg.STORENO, strTRAN_DATE);
                                foreach (DataRow drProd in dtProduct.Rows)
                                {
                                    bool UseExtraSaleProd = true;
                                    DataRow NewRow2 = dt1.NewRow();
                                    //需判斷可折抵次數>已折抵次數
                                    int CONSUME_COUNT = drProd["CONSUME_COUNT"] != DBNull.Value ? Convert.ToInt32(drProd["CONSUME_COUNT"]) : 0;
                                    int USE_COUNT = Convert.ToInt32(drProd["USE_COUNT"]);
                                    if (USE_COUNT == 0 || USE_COUNT > CONSUME_COUNT)
                                    {
                                        //為Y，需做名單檢核
                                        if (StringUtil.CStr(drProd["MEMBER_CHECK_FLAG"]) == "Y")
                                        {
                                            //取得門號
                                            DataTable dtPromosMSISDN = TempTables.DefaultView.ToTable(true, "PROMOTION_CODE", "MSISDN");
                                            foreach (DataRow drTempMSISDN in dtPromosMSISDN.Rows)
                                            {
                                                if (StringUtil.CStr(drTemp["PROMOTION_CODE"]) == StringUtil.CStr(drTempMSISDN["PROMOTION_CODE"]))
                                                {
                                                    //判斷此門號是否在名單內
                                                    DataTable dtUSE_COUNT = new SAL01_Facade().getUSE_COUNT(StringUtil.CStr(drProd["ACTIVITY_NO"]), StringUtil.CStr(drTempMSISDN["MSISDN"]));
                                                    if (dtUSE_COUNT.Rows.Count > 0)
                                                    {
                                                        UseExtraSaleProd = true;
                                                        break;
                                                    }
                                                    else
                                                    {
                                                        UseExtraSaleProd = false;
                                                    }
                                                }
                                                else   //不屬於此PROMOTION_CODE
                                                {
                                                    UseExtraSaleProd = false;
                                                }
                                            }
                                        }
                                        else
                                        {
                                            UseExtraSaleProd = true;
                                        }
                                    }
                                    else
                                    {                                        
                                        UseExtraSaleProd = false;
                                    }
                                    if (UseExtraSaleProd)
                                    {
                                        NewRow2["折扣料號"] = StringUtil.CStr(drProd["ACTIVITY_NO"]);
                                        NewRow2["折扣名稱"] = StringUtil.CStr(drProd["ACTIVITY_NAME"]);
                                        NewRow2["活動代碼"] = StringUtil.CStr(drProd["ACTIVITY_NO"]);
                                        NewRow2["活動名稱"] = StringUtil.CStr(drProd["ACTIVITY_NAME"]);
                                        NewRow2["項目名稱"] = StringUtil.CStr(drProd["EXCHANGE_NAME"]);
                                        NewRow2["兌換點數"] = StringUtil.CStr(drProd["DIVIDABLE_POINT"]);
                                        NewRow2["兌換金額"] = StringUtil.CStr(drProd["CONVERT_CURRENCY"]);
                                        NewRow2["活動折抵金額上限"] = StringUtil.CStr(drProd["U_BOUND"]);
                                        NewRow2["數量"] = "0";
                                        NewRow2["折扣方式"] = StringUtil.CStr(drProd["PAY_OFF_TYPE"]);
                                        dt1.Rows.Add(NewRow2);
                                    }

                                }
                                #endregion 取得單商品折抵活動
                            }
                        }
                        #endregion 處理促銷的兌點活動
                    }
                    else if (step == "extrasale")
                    {
                        #region 處理促銷的加價購
                        bool UseExtraSalePromo = true;  //是否可加價購此促銷
                        DataTable dtExtraSalePromotion = new SAL01_Facade().getExtraSalePromotionAction(StringUtil.CStr(drTemp["PROMOTION_CODE"]), logMsg.STORENO, strTRAN_DATE);
                        if (dtExtraSalePromotion.Rows.Count > 0)
                        {
                            #region 取得促銷資料
                            foreach (DataRow drExtraSalePromo in dtExtraSalePromotion.Rows)
                            {
                                DataRow NewRow4 = dt4.NewRow();
                                //為Y，需做名單檢核
                                if (StringUtil.CStr(drExtraSalePromo["MEMBER_CHECK_FLAG"]) == "Y")
                                {
                                    //判斷此門號是否在名單內
                                    DataTable dtPromosMSISDN = TempTables.DefaultView.ToTable(true, "PROMOTION_CODE", "MSISDN");
                                    foreach (DataRow drTempMSISDN in dtPromosMSISDN.Rows)
                                    {
                                        if (StringUtil.CStr(drTemp["PROMOTION_CODE"]) == StringUtil.CStr(drTempMSISDN["PROMOTION_CODE"]))
                                        {
                                            DataTable dtUSE_COUNT = new SAL01_Facade().getUSE_COUNT(StringUtil.CStr(drExtraSalePromo["ACTIVITY_ID"]), StringUtil.CStr(drTempMSISDN["MSISDN"]));
                                            if (dtUSE_COUNT.Rows.Count > 0)
                                            {
                                                UseExtraSalePromo = true;
                                                break;
                                            }
                                            else
                                            {
                                                UseExtraSalePromo = false;
                                            }
                                        }
                                        else   //不屬於此PROMOTION_CODE
                                        {
                                            UseExtraSalePromo = false;
                                        }
                                    }
                                }
                                else
                                {
                                    UseExtraSalePromo = true;
                                }
                                if (UseExtraSalePromo)
                                {
                                    NewRow4["折扣料號"] = StringUtil.CStr(drExtraSalePromo["ACTIVITY_ID"]);
                                    NewRow4["折扣名稱"] = StringUtil.CStr(drExtraSalePromo["ACTIVITY_NAME"]);
                                    NewRow4["商品料號"] = StringUtil.CStr(drExtraSalePromo["PRODNO"]);
                                    NewRow4["商品名稱"] = StringUtil.CStr(drExtraSalePromo["PRODNAME"]);
                                    NewRow4["兌換點數"] = StringUtil.CStr(drExtraSalePromo["DIVIDABLE_POINT"]);
                                    NewRow4["加購價"] = StringUtil.CStr(drExtraSalePromo["EXTRA_SALE_PRICE"]);
                                    NewRow4["活動折抵金額上限"] = StringUtil.CStr(drExtraSalePromo["U_BOUND"]);
                                    NewRow4["數量"] = 0;
                                    NewRow4["折扣方式"] = StringUtil.CStr(drExtraSalePromo["PAY_OFF_TYPE"]);
                                    dt4.Rows.Add(NewRow4);
                                }
                                #region 因為不需判斷門號的折抵次數  //註解
                                //// 第二步:名單檢核,檢查折抵次數,利用"MSISDN"(門號)
                                //DataTable dtPromosInMSIDN = TempTables.DefaultView.ToTable(true, "PROMOTION_CODE", "MSISDN");
                                //foreach (DataRow drTempMSIDN in dtPromosInMSIDN.Rows)
                                //{
                                //    if (!string.IsNullOrEmpty(StringUtil.CStr(drTempMSIDN["PROMOTION_CODE"]))
                                //        && (StringUtil.CStr(drTemp["PROMOTION_CODE"]) == StringUtil.CStr(drTempMSIDN["PROMOTION_CODE"]))) //未結清單的資料
                                //    {
                                //        DataTable dtExtraSalePromotionInMSIDN = new SAL01_Facade().getExtraSalePromotionAction(StringUtil.CStr(drTempMSIDN["PROMOTION_CODE"]), logMsg.STORENO, strTRAN_DATE);
                                //        foreach (DataRow drExtraSalePromoInMSIDN in dtExtraSalePromotionInMSIDN.Rows)
                                //        {
                                //            //為Y，需判斷門號是否為會員
                                //            if (StringUtil.CStr(drExtraSalePromoInMSIDN["MEMBER_CHECK_FLAG"]) == "Y")
                                //            {
                                //                //判斷此門號是否在名單內 
                                //                DataTable dtUSE_COUNT = new SAL01_Facade().getUSE_COUNT(StringUtil.CStr(drExtraSalePromoInMSIDN["ACTIVITY_ID"]), StringUtil.CStr(drTempMSIDN["MSISDN"]));
                                //                if (dtUSE_COUNT.Rows.Count > 0)
                                //                {
                                //                    string strUSE_COUNT = StringUtil.CStr(dtUSE_COUNT.Rows[0]["USE_COUNT"]);
                                //                    strUSE_COUNT = string.IsNullOrEmpty(strUSE_COUNT) ? "0" : strUSE_COUNT;

                                //                    if (!string.IsNullOrEmpty(StringUtil.CStr(drExtraSalePromoInMSIDN["USE_COUNT"])))
                                //                    {
                                //                        //累計折抵次數需小於最大折抵次數，才能繼續折抵
                                //                        if (Convert.ToInt16(strUSE_COUNT) < Convert.ToInt16(drExtraSalePromoInMSIDN["USE_COUNT"]))
                                //                        {
                                //                            UseExtraSalePromo = true;
                                //                            NewRow4["門號"] += StringUtil.CStr(drTempMSIDN["MSISDN"]) + ",";  //銷售結帳時，需依此來Update HG_CONVERT_MEMBER_LIST 的折抵次數(Use_Count)
                                //                        }
                                //                        else  //已到達最大折抵次數，所以不能使用這個HappyGo加價購(進行查詢 單商品加價購)
                                //                        {
                                //                            UseExtraSalePromo = false;
                                //                        }
                                //                    }
                                //                    else  //沒有最大折抵次數，表示可以無限次數折抵
                                //                    {
                                //                        UseExtraSalePromo = true;
                                //                        NewRow4["門號"] += ",";
                                //                    }
                                //                }
                                //                else  //要為會員才能有加價購，但此門號並不是會員，所以不能使用這個HappyGo加價購(進行查詢 單商品加價購)
                                //                {
                                //                    UseExtraSalePromo = false;
                                //                }
                                //            }
                                //            else  //不需判斷是否為會員
                                //            {
                                //                UseExtraSalePromo = true;
                                //                NewRow4["門號"] += ",";
                                //            }
                                //        }
                                //    }
                                //} 
                                //if (UseExtraSalePromo)
                                //{               
                                //    dt4.Rows.Add(NewRow4);
                                //}
                                #endregion 因為不需判斷門號的折抵次數
                            }
                            #endregion 取得促銷活動
                        }
                        else  //查無資料，進行查詢 單商品折抵活動
                        {
                            UseExtraSalePromo = false;
                        }

                        if (!UseExtraSalePromo)  //該促銷方案無對應的加價購，則查詢該促銷方案下的"商品"有無適用的加價購
                        {
                            DataRow[] dtProds = TempTables.Select("PROMOTION_CODE = '" + StringUtil.CStr(drTemp["PROMOTION_CODE"]) + "'");
                            foreach (DataRow drP in dtProds)
                            {
                                #region 取得單商品加價購商品
                                DataTable dtExtraSaleProduct = new SAL01_Facade().geteExtraSaleAction(StringUtil.CStr(drP["PRODNO"]), logMsg.STORENO, strTRAN_DATE);
                                foreach (DataRow drExtraSaleProd in dtExtraSaleProduct.Rows)
                                {
                                    bool UseExtraSaleProd = true;
                                    DataRow NewRow4 = dt4.NewRow();
                                    //需判斷可折抵次數>已折抵次數
                                    int CONSUME_COUNT = drExtraSaleProd["CONSUME_COUNT"] != DBNull.Value ? Convert.ToInt32(drExtraSaleProd["CONSUME_COUNT"]) : 0;
                                    int USE_COUNT = Convert.ToInt32(drExtraSaleProd["USE_COUNT"]);
                                    if (USE_COUNT == 0 || USE_COUNT > CONSUME_COUNT)
                                    {
                                        //為Y，需做名單檢核
                                        if (StringUtil.CStr(drExtraSaleProd["MEMBER_CHECK_FLAG"]) == "Y")
                                        {
                                            //取得門號
                                            DataTable dtPromosMSISDN = TempTables.DefaultView.ToTable(true, "PROMOTION_CODE", "MSISDN");
                                            foreach (DataRow drTempMSISDN in dtPromosMSISDN.Rows)
                                            {
                                                if (StringUtil.CStr(drTemp["PROMOTION_CODE"]) == StringUtil.CStr(drTempMSISDN["PROMOTION_CODE"]))
                                                {
                                                    //判斷此門號是否在名單內
                                                    DataTable dtUSE_COUNT = new SAL01_Facade().getUSE_COUNT(StringUtil.CStr(drExtraSaleProd["ACTIVITY_ID"]), StringUtil.CStr(drTempMSISDN["MSISDN"]));
                                                    if (dtUSE_COUNT.Rows.Count > 0)
                                                    {
                                                        UseExtraSaleProd = true;
                                                        break;
                                                    }
                                                    else
                                                    {
                                                        UseExtraSaleProd = false;
                                                    }
                                                }
                                                else    //不屬於此PROMOTION_CODE
                                                {
                                                    UseExtraSaleProd = false;
                                                }
                                            }
                                        }
                                        else
                                        {
                                            UseExtraSaleProd = true;
                                        }
                                    }
                                    else
                                    {
                                        UseExtraSaleProd = false;
                                    }
                                    if (UseExtraSaleProd)
                                    {
                                        NewRow4["折扣料號"] = StringUtil.CStr(drExtraSaleProd["ACTIVITY_ID"]);
                                        NewRow4["折扣名稱"] = StringUtil.CStr(drExtraSaleProd["ACTIVITY_NAME"]);
                                        NewRow4["商品料號"] = StringUtil.CStr(drExtraSaleProd["PRODNO"]);
                                        NewRow4["商品名稱"] = StringUtil.CStr(drExtraSaleProd["PRODNAME"]);
                                        NewRow4["兌換點數"] = StringUtil.CStr(drExtraSaleProd["DIVIDABLE_POINT"]);
                                        NewRow4["加購價"] = StringUtil.CStr(drExtraSaleProd["EXTRA_SALE_PRICE"]);
                                        NewRow4["活動折抵金額上限"] = StringUtil.CStr(drExtraSaleProd["U_BOUND"]);
                                        NewRow4["數量"] = 0;
                                        NewRow4["折扣方式"] = StringUtil.CStr(drExtraSaleProd["PAY_OFF_TYPE"]);
                                        dt4.Rows.Add(NewRow4);
                                    }
                                }
                                #endregion 取得單商品加價購商品
                            }
                        }
                        #endregion 處理促銷的加價購
                    }
                }
            }


            if (this.addProdList.Length > 0)
            {   //銷售作業新增單品時銷售商品料號處理
                for (int i = 0; i < this.addProdList.Length; i++)
                {
                    if (step == "exchange")
                    {
                        #region 取得單商品折抵活動
                    // 舊的程式 //註解
                    //DataTable dtProduct = new SAL01_Facade().getProductAction(addProdList[i], logMsg.STORENO, strTRAN_DATE);
                    //foreach (DataRow drProd in dtProduct.Rows)
                    //{
                        //    DataRow[] drs = dt1.Select("活動代碼='" + StringUtil.CStr(drProd["ACTIVITY_ID"]) + "' And 項目名稱='" + StringUtil.CStr(drProd["EXCHANGE_NAME"]) + "'");
                    //    if (drs == null || drs.Length == 0)
                    //    {   //單商品折抵活動尚未存在則加入
                    //        DataRow NewRow2 = dt1.NewRow();
                    //        NewRow2["折扣料號"] = StringUtil.CStr(drProd["ACTIVITY_ID"]);
                    //        NewRow2["折扣名稱"] = StringUtil.CStr(drProd["ACTIVITY_NAME"]);
                    //        NewRow2["活動代碼"] = StringUtil.CStr(drProd["ACTIVITY_ID"]);
                    //        NewRow2["活動名稱"] = StringUtil.CStr(drProd["ACTIVITY_NAME"]);
                    //        NewRow2["項目名稱"] = StringUtil.CStr(drProd["EXCHANGE_NAME"]);
                    //        NewRow2["兌換點數"] = StringUtil.CStr(drProd["DIVIDABLE_POINT"]);
                    //        NewRow2["兌換金額"] = StringUtil.CStr(drProd["CONVERT_CURRENCY"]);
                    //        NewRow2["活動折抵金額上限"] = StringUtil.CStr(drProd["U_BOUND"]);
                    //        NewRow2["數量"] = "0";
                    //        NewRow["折扣方式"] = StringUtil.CStr(drPromo["PAY_OFF_TYPE"]);
                    //        dt1.Rows.Add(NewRow2);
                    //    }
                    //}

                    // 新
                    DataTable dtProduct = new SAL01_Facade().getProductAction(addProdList[i], logMsg.STORENO, strTRAN_DATE);
                    foreach (DataRow drProd in dtProduct.Rows)
                    {
                        DataRow[] drs = dt1.Select("活動代碼='" + StringUtil.CStr(drProd["ACTIVITY_NO"]) + "' And 項目名稱='" + StringUtil.CStr(drProd["EXCHANGE_NAME"]) + "'");
                        if (drs == null || drs.Length == 0)
                        {   //單商品兌點活動尚未存在則加入
                            bool UseExchangeProd = true;
                            DataRow NewRow2 = dt1.NewRow();
                            NewRow2["折扣料號"] = StringUtil.CStr(drProd["ACTIVITY_NO"]);
                            NewRow2["折扣名稱"] = StringUtil.CStr(drProd["ACTIVITY_NAME"]);
                            NewRow2["活動代碼"] = StringUtil.CStr(drProd["ACTIVITY_NO"]);
                            NewRow2["活動名稱"] = StringUtil.CStr(drProd["ACTIVITY_NAME"]);
                            NewRow2["項目名稱"] = StringUtil.CStr(drProd["EXCHANGE_NAME"]);
                            NewRow2["兌換點數"] = StringUtil.CStr(drProd["DIVIDABLE_POINT"]);
                            NewRow2["兌換金額"] = StringUtil.CStr(drProd["CONVERT_CURRENCY"]);
                            NewRow2["活動折抵金額上限"] = StringUtil.CStr(drProd["U_BOUND"]);
                            NewRow2["數量"] = "0";
                            NewRow2["折扣方式"] = StringUtil.CStr(drProd["PAY_OFF_TYPE"]);
                            //需判斷可折抵次數>已折抵次數
                            int CONSUME_COUNT = drProd["CONSUME_COUNT"] != DBNull.Value ? Convert.ToInt32(drProd["CONSUME_COUNT"]) : 0;
                            int USE_COUNT = Convert.ToInt32(drProd["USE_COUNT"]);
                            if (USE_COUNT == 0 || USE_COUNT > CONSUME_COUNT)
                            {
                                //為Y，需做名單檢核
                                if (StringUtil.CStr(drProd["MEMBER_CHECK_FLAG"]) == "Y")
                                {
                                    // 還需判斷此門號是否在名單內(少了門號["MSISDN"])?
                                    //DataTable dtUSE_COUNT = new SAL01_Facade().getUSE_COUNT(StringUtil.CStr(drProd["ACTIVITY_ID"]), StringUtil.CStr(drTemp["MSISDN"]));
                                    //if (dtUSE_COUNT.Rows.Count > 0)
                                    //{
                                    //    // 還需判斷已折抵次數<可折抵次數?
                                    UseExchangeProd = true;
                                    //}
                                    //else
                                    //{
                                    //    UseExchangeProd = false;
                                    //}
                                }
                                else
                                {
                                    // 還需判斷已折抵次數<可折抵次數?
                                    UseExchangeProd = true;
                                }
                            }
                            else
                            {
                                UseExchangeProd = false;
                            }
                            if (UseExchangeProd)
                            {
                                dt1.Rows.Add(NewRow2);
                            }
                        }
                    }
                    #endregion 取得單商品折抵活動
                    }
                    else if (step == "extrasale")
                    {
                        #region 取得單商品加價購商品
                    DataTable dtExtraSaleProduct = new SAL01_Facade().geteExtraSaleAction(addProdList[i], logMsg.STORENO, strTRAN_DATE);
                    foreach (DataRow drExtraSaleProd in dtExtraSaleProduct.Rows)
                    {
                        DataRow[] drs = dt4.Select("折扣料號='" + StringUtil.CStr(drExtraSaleProd["ACTIVITY_ID"]) + "' And 商品料號='" + StringUtil.CStr(drExtraSaleProd["PRODNO"]) 
                            + "' And 兌換點數='" + StringUtil.CStr(drExtraSaleProd["DIVIDABLE_POINT"]) + "' And 加購價='" + StringUtil.CStr(drExtraSaleProd["EXTRA_SALE_PRICE"]) + "'");
                        if (drs == null || drs.Length == 0)
                        {
                            bool UseExtraSaleProd = true;
                            DataRow NewRow4 = dt4.NewRow();
                            NewRow4["折扣料號"] = StringUtil.CStr(drExtraSaleProd["ACTIVITY_ID"]);
                            NewRow4["折扣名稱"] = StringUtil.CStr(drExtraSaleProd["ACTIVITY_NAME"]);
                            NewRow4["商品料號"] = StringUtil.CStr(drExtraSaleProd["PRODNO"]);
                            NewRow4["商品名稱"] = StringUtil.CStr(drExtraSaleProd["PRODNAME"]);
                            NewRow4["兌換點數"] = StringUtil.CStr(drExtraSaleProd["DIVIDABLE_POINT"]);
                            NewRow4["加購價"] = StringUtil.CStr(drExtraSaleProd["EXTRA_SALE_PRICE"]);
                            NewRow4["活動折抵金額上限"] = StringUtil.CStr(drExtraSaleProd["U_BOUND"]);
                            NewRow4["數量"] = 0;
                            NewRow4["折扣方式"] = StringUtil.CStr(drExtraSaleProd["PAY_OFF_TYPE"]);
                            //需判斷可折抵次數>已折抵次數
                            int CONSUME_COUNT = drExtraSaleProd["CONSUME_COUNT"] != DBNull.Value ? Convert.ToInt32(drExtraSaleProd["CONSUME_COUNT"]) : 0;
                            int USE_COUNT = Convert.ToInt32(drExtraSaleProd["USE_COUNT"]);
                            if (USE_COUNT == 0 || USE_COUNT > CONSUME_COUNT)
                            {
                                //為Y，需做名單檢核
                                if (StringUtil.CStr(drExtraSaleProd["MEMBER_CHECK_FLAG"]) == "Y")
                                {
                                    // 還需判斷此門號是否在名單內(少了門號["MSISDN"])?
                                    //DataTable dtUSE_COUNT = new SAL01_Facade().getUSE_COUNT(StringUtil.CStr(drExtraSaleProd["ACTIVITY_ID"]), StringUtil.CStr(drTemp["MSISDN"]));
                                    //if (dtUSE_COUNT.Rows.Count > 0)
                                    //{
                                    //    // 還需判斷已折抵次數<可折抵次數?
                                    UseExtraSaleProd = true;
                                    //}
                                    //else
                                    //{
                                    //    UseExtraSaleProd = false;
                                    //}
                                }
                                else
                                {
                                    UseExtraSaleProd = true;
                                }
                            }
                            else
                            {
                                UseExtraSaleProd = false;
                            }
                            if (UseExtraSaleProd)
                            {
                                dt4.Rows.Add(NewRow4);
                            }
                        }
                    }
                    #endregion 取得單商品加價購商品
                    }
                }
            }

            if (step == "exchange")
            {
                #region 取得一般兌點通則
                DataTable dtConvert = new SAL01_Facade().getConvertAction(strTRAN_DATE, "1");
                foreach (DataRow drConvert in dtConvert.Rows)
                {
                    DataRow NewRow = dt3.NewRow();
                    NewRow["折扣料號"] = StringUtil.CStr(drConvert["DISCOUNT_CODE"]);
                    NewRow["折扣名稱"] = StringUtil.CStr(drConvert["DISCOUNT_NAME"]);
                    NewRow["活動代碼"] = StringUtil.CStr(drConvert["CONVERT_NO"]);
                    NewRow["項目名稱"] = StringUtil.CStr(drConvert["CONVERT_NAME"]);
                    NewRow["兌換點數"] = StringUtil.CStr(drConvert["DIVIDABLE_POINT"]);
                    NewRow["兌換金額"] = StringUtil.CStr(drConvert["CONVERT_CURRENCY"]);
                    NewRow["數量"] = "0";
                    dt3.Rows.Add(NewRow);
                }
                #endregion  取得一般兌點通則
            }

            CalPoint(ref dt1,ref dt2,ref dt3); //剩餘點數兌換

            if (step == "exchange")
            {
                ViewState["dtProduct"] = dt1;
                ViewState["dtPromotion"] = dt2;
                ViewState["dtConvert"] = dt3;
            }
            else if (step == "extrasale")
            {
                ViewState["dtExtraSale"] = dt4;
            }
        //}
        if (step == "exchange")
        {
            bindGridView1Data();
            bindGridView2Data();
            bindGridView3Data();
        }
        else if (step == "extrasale")
        {
            bindGridView4Data();
        }
    }

    private void CalPoint(ref DataTable dt1, ref DataTable dt2, ref DataTable dt3)
    {
        int SumPoint = 0;   //總兌換點數
        int SumAmount = 0;  //總兌換金額
        int happyGoPoint = Convert.ToInt32(string.IsNullOrEmpty(this.lblHG_LEFT_POINT.Text) ? "0" : this.lblHG_LEFT_POINT.Text);
        
        //先用促銷活動折抵
        ChangeQuantity(happyGoPoint, ref dt2, ref SumPoint, ref SumAmount, true);
        //其次用單品折扣折抵
        if (happyGoPoint > SumPoint && RequestTotalAmount  > SumAmount)
            ChangeQuantity(happyGoPoint, ref dt1, ref SumPoint, ref SumAmount, true);
        //最後用一般通則折抵
        if (happyGoPoint > SumPoint && RequestTotalAmount > SumAmount)
            ChangeQuantity(happyGoPoint, ref dt3, ref SumPoint, ref SumAmount, false);

        this.hdSumPoint.Text = StringUtil.CStr(SumPoint);
        this.hdSumAmount.Text = StringUtil.CStr(SumAmount);
        this.lblHG_REDEEM_POINT.Text = StringUtil.CStr(SumAmount) + "元(" + StringUtil.CStr(SumPoint) + "點)";
        this.lblHG_LEFT_POINT.Text = StringUtil.CStr((Convert.ToInt32(lblHG_LEFT_POINT.Text) - SumPoint));
    }

    private void ChangeQuantity(int happyGoPoint, ref DataTable dt, ref int SumPoint, ref int SumAmount, bool isU_BOUND)
    {
        if (happyGoPoint > 0 && happyGoPoint > SumPoint && RequestTotalAmount > SumAmount && dt.Rows.Count > 0)
        {
            //先找出最高級兌換條件，取金額/點數最大值的條件
            double maxCp = 0.0;
            double valueCp = 0.0;
            int chgPoint = 0;
            int chgAmt = 0;
            int selIndex = 0;
            int ind = 0;
            int totalAmount = 0;
            string payOffType = string.Empty;

            foreach (DataRow dr in dt.Rows)
            {
                int iPoint = Convert.ToInt32(string.IsNullOrEmpty(StringUtil.CStr(dr["兌換點數"])) ? "0" : StringUtil.CStr(dr["兌換點數"]));
                int iAmt = Convert.ToInt32(string.IsNullOrEmpty(StringUtil.CStr(dr["兌換金額"])) ? "0" : StringUtil.CStr(dr["兌換金額"]));
                if (isU_BOUND)
                {
                    payOffType = StringUtil.CStr(dr["折扣方式"]);
                    if (payOffType == "0001")
                        totalAmount = Convert.ToInt32(dr["活動折抵金額上限"]);
                    else
                        totalAmount = Convert.ToInt32(RequestTotalAmount * (Convert.ToInt32(dr["活動折抵金額上限"]) * 0.01));
                }
                //當兌換點數條件不大於目前剩餘HappyGo點數且兌換金額加上以兌換金額不大於應收總金額時，將此條件列入考慮
                if (iPoint != 0 && iPoint <= happyGoPoint - SumPoint && iAmt + SumAmount <= totalAmount)
                    valueCp = (double)iAmt / iPoint;
                else
                    valueCp = 0.0;

                if (valueCp > maxCp)
                {
                    maxCp = valueCp;
                    chgPoint = iPoint;
                    chgAmt = iAmt;
                    selIndex = ind;
                }
                ind++;
            }

            if (chgPoint > 0)
            {
                int usedCnt = (int)((happyGoPoint - SumPoint) / chgPoint);
                int changedAmt = usedCnt * chgAmt;
                if (changedAmt + SumAmount > totalAmount)
                {
                    if (chgAmt > 0)
                        usedCnt = (int)((totalAmount - SumAmount) / chgAmt);
                    else
                        usedCnt = 0;
                    changedAmt = usedCnt * chgAmt;
                }

                SumAmount += changedAmt;
                SumPoint += usedCnt * chgPoint;
                dt.Rows[selIndex]["數量"] = usedCnt;
            }
        }
    }

    protected void bindGridView1Data()
    {
        DataTable dtResult = new DataTable();
        dtResult = ViewState["dtProduct"] as DataTable;
        GridView1.DataSource = dtResult;
        GridView1.DataBind();
    }

    protected void bindGridView2Data()
    {
        DataTable dtResult = new DataTable();
        dtResult = ViewState["dtPromotion"] as DataTable;
        GridView2.DataSource = dtResult;
        GridView2.DataBind();
    }

    protected void bindGridView3Data()
    {
        DataTable dtResult = new DataTable();
        dtResult = ViewState["dtConvert"] as DataTable;
        GridView3.DataSource = dtResult;
        GridView3.DataBind();
    }

    protected void bindGridView4Data()
    {
        DataTable dtResult = new DataTable();
        dtResult = ViewState["dtExtraSale"] as DataTable;
        GridView4.DataSource = dtResult;
        GridView4.DataBind();
    }

    private DataTable GetGridView1EmptyData()
    {
        DataTable dtResult = new DataTable();
        dtResult.Columns.Add("折扣料號", typeof(string));
        dtResult.Columns.Add("折扣名稱", typeof(string));
        dtResult.Columns.Add("活動代碼", typeof(string));
        dtResult.Columns.Add("活動名稱", typeof(string));
        dtResult.Columns.Add("項目名稱", typeof(string));
        dtResult.Columns.Add("兌換點數", typeof(string));
        dtResult.Columns.Add("兌換金額", typeof(string));
        dtResult.Columns.Add("活動折抵金額上限", typeof(string));
        dtResult.Columns.Add("門號", typeof(string));
        dtResult.Columns.Add("數量", typeof(string));
        dtResult.Columns.Add("折扣方式", typeof(string));  
        return dtResult;
    }

    private DataTable GetGridView2EmptyData()
    {
        DataTable dtResult = new DataTable();
        dtResult.Columns.Add("折扣料號", typeof(string));
        dtResult.Columns.Add("折扣名稱", typeof(string));
        dtResult.Columns.Add("促銷代碼", typeof(string));
        dtResult.Columns.Add("促銷名稱", typeof(string));
        dtResult.Columns.Add("項目名稱", typeof(string));
        dtResult.Columns.Add("兌換點數", typeof(string));
        dtResult.Columns.Add("兌換金額", typeof(string));
        dtResult.Columns.Add("活動折抵金額上限", typeof(string));
        dtResult.Columns.Add("門號", typeof(string));
        dtResult.Columns.Add("數量", typeof(string));
        dtResult.Columns.Add("折扣方式", typeof(string));
        return dtResult;
    }

    private DataTable GetGridView3EmptyData()
    {
        DataTable dtResult = new DataTable();
        dtResult.Columns.Add("折扣料號", typeof(string));
        dtResult.Columns.Add("折扣名稱", typeof(string));
        dtResult.Columns.Add("活動代碼", typeof(string));
        dtResult.Columns.Add("項目名稱", typeof(string));
        dtResult.Columns.Add("兌換點數", typeof(string));
        dtResult.Columns.Add("兌換金額", typeof(string));
        dtResult.Columns.Add("門號", typeof(string));
        dtResult.Columns.Add("數量", typeof(string));
        return dtResult;
    }

    private DataTable GetGridView4EmptyData()
    {
        DataTable dtResult = new DataTable();
        dtResult.Columns.Add("折扣料號", typeof(string));
        dtResult.Columns.Add("折扣名稱", typeof(string));
        dtResult.Columns.Add("商品料號", typeof(string));
        dtResult.Columns.Add("商品名稱", typeof(string));
        dtResult.Columns.Add("兌換點數", typeof(string));
        dtResult.Columns.Add("加購價", typeof(string));
        dtResult.Columns.Add("活動折抵金額上限", typeof(string));
        dtResult.Columns.Add("門號", typeof(string));
        dtResult.Columns.Add("數量", typeof(string));
        dtResult.Columns.Add("折扣方式", typeof(string));
        return dtResult;
    }

    protected void Wizard1_PreviousButtonClick(object seder, WizardNavigationEventArgs e)
    {
        switch (e.NextStepIndex)
        {
            case 1:
                this.lblOriginalHG_LEFT_POINT.Text = this.Wizard1.FindChildControl<ASPxTextBox>("hdHG_LEFT_POINT").Text;

                int OriginalPoint = Convert.ToInt32(this.lblOriginalHG_LEFT_POINT.Text);
                int SumPoint = 0;
                int SumAmount = 0;
                ASPxGridView gv = this.GridView4;
                for (int i = 0; i < gv.VisibleRowCount; i++)
                {
                    object[] keyValue = gv.GetRowValues(i, "商品料號", "商品名稱", "兌換點數", "加購價", "門號") as object[];
                    ASPxTextBox txtQuantity = gv.FindRowCellTemplateControl(i, (GridViewDataColumn)gv.Columns["數量"], "txtQuantity") as ASPxTextBox;
                    int iQty = string.IsNullOrEmpty(txtQuantity.Text) ? 0 : Convert.ToInt16(txtQuantity.Text);
                    SumPoint += Convert.ToInt32(keyValue[2]) * iQty;   //總兌換點數
                    SumAmount += Convert.ToInt32(keyValue[3]) * iQty;  //總兌點金額
                    this.hdSumPoint2.Text = StringUtil.CStr(SumPoint);
                    this.hdSumAmount2.Text = StringUtil.CStr(SumAmount);
                    this.lblHG_REDEEM_POINT2.Text = StringUtil.CStr(SumAmount) + "元(" + StringUtil.CStr(SumPoint) + "點)";
                    this.lblHG_LEFT_POINT2.Text = StringUtil.CStr((OriginalPoint - SumPoint));
                }
                break;
            default:
                break;
        }
    }

    protected void Wizard1_NextButtonClick(object sender, WizardNavigationEventArgs e)
    {
        switch (e.NextStepIndex)
        {
            case 1:  //第一畫面刷卡成功後，載入可折抵的商品項目
                this.lblHG_CAR_NO.Text = this.Wizard1.FindChildControl<ASPxTextBox>("hdHG_CAR_NO").Text;
                this.lblHG_LEFT_POINT.Text = this.Wizard1.FindChildControl<ASPxTextBox>("hdHG_LEFT_POINT").Text;
                BindGridViewData("extrasale");
                this.lblHG_CAR_NO2.Text = this.Wizard1.FindChildControl<ASPxTextBox>("hdHG_CAR_NO").Text;
                this.lblHG_REDEEM_POINT2.Text = "0元(0點)";
                this.lblHG_LEFT_POINT2.Text = this.Wizard1.FindChildControl<ASPxTextBox>("hdHG_LEFT_POINT").Text;
                this.lblOriginalHG_LEFT_POINT.Text = this.Wizard1.FindChildControl<ASPxTextBox>("hdHG_LEFT_POINT").Text; 
                break;
            case 2:
                int extraSalePoint = 0, leftPoint = 0;
                if (this.Wizard1.FindChildControl<ASPxTextBox>("hdExtraSaleSumPoint").Text != "")
                    extraSalePoint = Convert.ToInt32(this.Wizard1.FindChildControl<ASPxTextBox>("hdExtraSaleSumPoint").Text);
                leftPoint = Convert.ToInt32(this.Wizard1.FindChildControl<ASPxTextBox>("hdHG_LEFT_POINT").Text);
                this.lblHG_LEFT_POINT.Text = this.lblNextOriginalHG_LEFT_POINT.Text = StringUtil.CStr((leftPoint - extraSalePoint));
                BindGridViewData("exchange");
                break;
            default:
                break;
        }
    }

    protected void Wizard1_FinishButtonClick(object sender, WizardNavigationEventArgs e)
    {
        CalAmountAndPoint();
    }

    /// <summary>
    /// 計算每個折扣料號的總兌點金額和總兌換點數
    /// </summary>
    private void CalAmountAndPoint()
    {
        int arrayIndex = 0;
        string javascript = "";
        javascript += "var Infos = new Array(); ";

        //單商品折抵活動
        BuildReturnValues(ref arrayIndex, ref javascript, this.GridView1, "product");
        //促銷商品折抵活動
        BuildReturnValues(ref arrayIndex, ref javascript, this.GridView2, "promotion");
        //一般兌點通則
        BuildReturnValues(ref arrayIndex, ref javascript, this.GridView3, "common");
        //加價購
        BuildReturnValues(ref arrayIndex, ref javascript, this.GridView4, "extrasale");

        javascript += "returnValue = Infos;";
        javascript += "window.close();";

        Page.ClientScript.RegisterStartupScript(this.GetType(), "CloseWindows", javascript, true);
    }

    private void BuildReturnValues(ref int arrayIndex, ref string javascript, ASPxGridView gv, string TypeName)
    {
        //判斷User輸入兌換數量的所有活動代碼
        //同一個活動代碼，兌點金額和兌換點數要加總起來
        //組成要回傳銷售畫面的陣列資訊

        string strValues = "";
        string ActivityId = "";
        int SumPoint = 0;
        int SumAmount = 0;
        int PreviousiQty = 0;
        for (int i = 0; i < gv.VisibleRowCount; i++)
        {
            object[] keyValue;
            if (TypeName == "promotion")
                keyValue = gv.GetRowValues(i, "折扣料號", "折扣名稱", "兌換點數", "兌換金額", "門號", "促銷代碼", "促銷名稱") as object[];
            else if (TypeName == "extrasale")
                keyValue = gv.GetRowValues(i, "商品料號", "商品名稱", "兌換點數", "加購價", "門號") as object[];
            else
                keyValue = gv.GetRowValues(i, "折扣料號", "折扣名稱", "兌換點數", "兌換金額", "門號") as object[];
            ASPxTextBox txtQuantity = gv.FindRowCellTemplateControl(i, (GridViewDataColumn)gv.Columns["數量"], "txtQuantity") as ASPxTextBox;
            int iQty = string.IsNullOrEmpty(txtQuantity.Text) ? 0 : Convert.ToInt16(txtQuantity.Text);

            if (StringUtil.CStr(keyValue[0]) == ActivityId)  //同一個活動代碼，就累計兌換點數和金額
            {
                try
                {
                    SumPoint += Convert.ToInt32(keyValue[2]) * iQty;   //總兌換點數
                    SumAmount += Convert.ToInt32(keyValue[3]) * iQty;  //總兌點金額
                }
                catch (Exception)
                {
                    
                }
            }
            else  //不同的折扣料號，重新累計
            {
                if (SumPoint > 0)  //先把上一筆折扣料號的資訊記錄起來
                {
                    object[] lastKeyValue;
                    if (TypeName == "promotion")
                        lastKeyValue = gv.GetRowValues(i - 1, "折扣料號", "折扣名稱", "兌換點數", "兌換金額", "門號", "促銷代碼", "促銷名稱") as object[];
                    else if (TypeName == "extrasale")
                        lastKeyValue = gv.GetRowValues(i - 1, "商品料號", "商品名稱", "兌換點數", "加購價", "門號") as object[];
                    else
                        lastKeyValue = gv.GetRowValues(i - 1, "折扣料號", "折扣名稱", "兌換點數", "兌換金額", "門號") as object[];
                    strValues = StringUtil.CStr(lastKeyValue[0]) + ","    //活動代碼
                       + StringUtil.CStr(lastKeyValue[1]) + ","           //活動名稱
                       + StringUtil.CStr(SumAmount) + ","                 //兌點金額
                       + this.lblHG_CAR_NO.Text + ","                     //HG卡號
                       + StringUtil.CStr(SumPoint) + ","                  //兌換點數
                       + StringUtil.CStr(lastKeyValue[4]) + ","           //門號
                       + this.lblHG_LEFT_POINT.Text + ","                 //剩餘點數
                       + (TypeName != "extrasale" ? TypeName : "") + ","   //兌點規則 
                       + (TypeName != "extrasale" ? "1" : StringUtil.CStr(PreviousiQty)) + ","                        //數量
                       + (TypeName != "extrasale" ? StringUtil.CStr(SumAmount) : StringUtil.CStr(keyValue[3])) + ","  //兌點單價
                       + (TypeName != "promotion" ? "" : StringUtil.CStr(lastKeyValue[5])) + ","                      //促銷代碼
                       + (TypeName != "promotion" ? "" : StringUtil.CStr(lastKeyValue[6]));                           //促銷名稱
                    javascript += " Infos[" + arrayIndex + "] = 'HGDISCOUNT," + strValues + "';";
                    arrayIndex += 1;
                }

                SumPoint = 0;
                SumAmount = 0;
                try
                {
                    SumPoint += Convert.ToInt32(keyValue[2]) * iQty;   //總兌換點數
                    SumAmount += Convert.ToInt32(keyValue[3]) * iQty;  //總兌點金額
                }
                catch (Exception)
                {

                }

                ActivityId = StringUtil.CStr(keyValue[0]);
            }

            if (i == gv.VisibleRowCount - 1 && SumPoint > 0) //如果是最後一筆資料，直接記錄資訊
            {
                strValues = StringUtil.CStr(keyValue[0]) + ","    //活動代碼
                    + StringUtil.CStr(keyValue[1]) + ","          //活動名稱
                    + StringUtil.CStr(SumAmount) + ","            //兌點總金額
                    + this.lblHG_CAR_NO.Text + ","                //HG卡號
                    + StringUtil.CStr(SumPoint) + ","             //兌換總點數
                    + StringUtil.CStr(keyValue[4]) + ","          //門號
                    + this.lblHG_LEFT_POINT.Text + ","            //剩餘點數
                    + (TypeName != "extrasale" ? TypeName : "") + ","   //兌點規則
                    + (TypeName != "extrasale" ? "1" : StringUtil.CStr(iQty)) + ","                                //數量
                    + (TypeName != "extrasale" ? StringUtil.CStr(SumAmount) : StringUtil.CStr(keyValue[3])) + ","  //兌點單價
                    + (TypeName != "promotion" ? "" : StringUtil.CStr(keyValue[5])) + ","                      //促銷代碼
                    + (TypeName != "promotion" ? "" : StringUtil.CStr(keyValue[6]));                           //促銷名稱
                javascript += " Infos[" + arrayIndex + "] = 'HGDISCOUNT," + strValues + "';";
                arrayIndex++;
            }
            PreviousiQty = iQty;
        }
    }

}
