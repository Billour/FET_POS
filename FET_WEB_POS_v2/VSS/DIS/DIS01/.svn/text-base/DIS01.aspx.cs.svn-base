using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using DevExpress.Web.ASPxGridView;
using DevExpress.Web.ASPxEditors;
using System.Web.UI.HtmlControls;
using System.IO;
using Advtek.Utility;

using DevExpress.Web;
using DevExpress.Web.ASPxPager;
using DevExpress.Web.ASPxTabControl;
using FET.POS.Model.Facade.FacadeImpl;
using FET.POS.Model.Common;

using FET.POS.Model.DTO;
using FET.POS.Model.Facade;
using System.Collections.Specialized;

public partial class VSS_DIS_DIS01 : BasePage
{
    private DIS01_Facade _DIS01_Facade;
    private DIS01_DiscountMasterDataSet_DTO _DiscountMasterDataSet_DTO;

    /// <summary>
    /// 折扣料號主檔UUID
    /// </summary>
    private string qDiscountCode
    {
        get
        {
            string encryptUrl = "";
            //return (Request.QueryString["DiscountCodeUUID"] ?? "");

            //**2011/04/19 Tina：前頁傳遞參數時，會先以加密處理，在此要解密。
            if (!string.IsNullOrEmpty(Request.QueryString["Param"]))
            {
                NameValueCollection qscoll = Utils.Param_UrlDecode(Request.QueryString["Param"]);
                foreach (string key in qscoll.AllKeys)
                 {
                     if (key == "DiscountCodeUUID")
                     {
                         encryptUrl = string.Join(",", qscoll.GetValues(key));
                         break;
                     }
                 }
            }

            return encryptUrl;
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack && !Page.IsCallback)
        {
            Session["RatePlan"] = null;
            Session["Product"] = null;
            Session["Store"] = null;
            Session["Promotion"] = null;
            Session["Customer1"] = null;
            Session["Customer2"] = null;
            Session["CostCenter"] = null;
            Session["SetProduct"] = null;
            Session["AddProduct"] = null;
            Session["BATCH_NO"] = null;
            Session["MASTERID_ADD"] = null;

            if (!string.IsNullOrEmpty(qDiscountCode))   //由查詢頁面過來
            {
                //查詢所有資料，並且記錄於Session中
                getDataGUI(e);
                Session["MASTERID_ADD"] = qDiscountCode;  //for Delete
            }
            else  //直接至折扣設定作業頁面
            {
                //畫面預設控制
                defaultGUI(e);
            }

            PRODUCT_DISCOUNT1.bindMasterDataProduct();
            STORE_DISCOUNT1.bindMasterDataStore();
            PROMOTION_DISCOUNT1.bindMasterDataPromotion();
            CUST_LEVE_DISCOUNT1.bindMasterDataCustomer1();
            CUST_LEVE_DISCOUNT1.bindMasterDataCustomer2();
            COST_CENTER_DISCOUNT1.bindMasterDataCostCenter();
            GIFT_DISCOUNT1.bindMaster1();
            ADD_IN_PROD_DISCOUNT1.bindMaster2();
        }
    }

    #region Check ProdNO && DiscountCode IsExit
    private bool checkProduct(string DNO)
    {
        bool IsExist = false;
        DataTable dt = new Product_Facade().Query_Product(DNO); // DIS01_PageHelper.GetProdDataByKey(DNO);
        if (dt.Rows.Count > 0) //表示此料號已存在於PRODUCT TABLE中
        {
            if (dt.Rows.Count > 1)  //而且不止一筆
            {
                IsExist = true;
            }
            else  //如果只有一筆，就要判斷是不是從"查詢修改"過來的UUID
            {
                //修改時，要先判斷此重複的ProdNo於DISCOUNT_MASTER的UUID是否為目前正在修改的UUID
                _DIS01_Facade = new DIS01_Facade();
                DataTable dt2 = _DIS01_Facade.Query_DiscountMasterByKey(qDiscountCode);

                if (dt2.Rows.Count > 0)
                {
                    IsExist = false;
                }
                else
                {
                    IsExist = true;
                }

                if (dt.Rows.Count == 0 && dt2.Rows.Count == 0)
                {
                    IsExist = false;
                }
            }
        }
        return IsExist;
    }
    private bool checkHaveDISCOUNT_NO(string DNO, string sDate)
    {
        bool chkstatus;

        if (checkProduct(DNO))
        {
            //商品編號存在則不能新增折扣
            chkstatus = false;
        }
        else
        {
            chkstatus = true;
        }

        return chkstatus;
    }
    #endregion

    #region 呼當前網頁的方式 PageMethods for Return Value to GridView
    //ajax 呼當前網頁的方式
    [System.Web.Services.WebMethod()]
    [System.Web.Script.Services.ScriptMethod()]
    static public string getProductsInfo(string ProdNo)
    {
        string strInfo = "";
        if (!string.IsNullOrEmpty(ProdNo))
        {
            DataTable dtProd = DIS01_PageHelper.GetProdDataByKey(ProdNo);
            if (dtProd.Rows.Count > 0)
            {
                strInfo = StringUtil.CStr(dtProd.Rows[0]["PRODNAME"]);
            }
        }

        return strInfo;
    }
    [System.Web.Services.WebMethod()]
    [System.Web.Script.Services.ScriptMethod()]
    static public string getStoreInfo(string STORE_NO)
    {
        string strInfo = "";
        if (!string.IsNullOrEmpty(STORE_NO))
        {
            DataTable dtStore = DIS01_PageHelper.GetStoreDataByKey(STORE_NO);
            if (dtStore.Rows.Count > 0)
            {
                strInfo = StringUtil.CStr(dtStore.Rows[0]["STORENAME"]) + ";" + StringUtil.CStr(dtStore.Rows[0]["ZONE_NAME"]);
            }
        }

        return strInfo;
    }
    [System.Web.Services.WebMethod()]
    [System.Web.Script.Services.ScriptMethod()]
    static public string getPromoInfo(string Promo)
    {
        string strInfo = "";
        if (!string.IsNullOrEmpty(Promo))
        {
            DataTable dtProd = DIS01_PageHelper.GetPromoDataByKey(Promo);
            if (dtProd.Rows.Count > 0)
            {
                strInfo = StringUtil.CStr(dtProd.Rows[0]["PROMO_NAME"]);
            }
        }

        return strInfo;
    }
    [System.Web.Services.WebMethod()]
    [System.Web.Script.Services.ScriptMethod()]
    static public string getSetProductsInfo(string ProdNo)
    {
        string strInfo = "";
        if (!string.IsNullOrEmpty(ProdNo))
        {
            DataTable dtProd = DIS01_PageHelper.GetProdDataByKey(ProdNo);
            if (dtProd.Rows.Count > 0)
            {
                strInfo = StringUtil.CStr(dtProd.Rows[0]["PRODNAME"]);
            }
        }

        return strInfo;
    }
    [System.Web.Services.WebMethod()]
    [System.Web.Script.Services.ScriptMethod()]
    static public string getAddProdInfo(string ProdNo)
    {
        string strInfo = "";
        if (!string.IsNullOrEmpty(ProdNo))
        {
            DataTable dtProd = DIS01_PageHelper.GetProdDataByKey(ProdNo);
            if (dtProd.Rows.Count > 0)
            {
                strInfo = StringUtil.CStr(dtProd.Rows[0]["PRODNAME"]);
            }
        }

        return strInfo;
    }
    [System.Web.Services.WebMethod()]
    [System.Web.Script.Services.ScriptMethod()]
    static public string getAccountInfo(string PRODTYPENO)
    {
        string strInfo = "";
        if (!string.IsNullOrEmpty(PRODTYPENO))
        {
            //DataTable dt = new DIS01_Facade().Query_ProdType_Account_ByKey(PRODTYPENO);

            //if (dt.Rows.Count > 0)
            //{
            //    strInfo = StringUtil.CStr(dt.Rows[0]["ACCOUNT"]);
            //}

            //**2011/04/22 Tina：從商品分類取得會計科目的參照Table改成CCN_ACCOUNT
            DataTable dt = new DIS01_Facade().Query_Account_ByProdType(PRODTYPENO);

            if (dt.Rows.Count > 0)
            {
                strInfo = StringUtil.CStr(dt.Rows[0]["CCN"]) + "," + StringUtil.CStr(dt.Rows[0]["ACCOUNT"]);
            }

        }

        return strInfo;
    }
    [System.Web.Services.WebMethod()]
    [System.Web.Script.Services.ScriptMethod()]
    static public string getProd_CategInfo(string CCNO)
    {
        string strInfo = "";
        if (!string.IsNullOrEmpty(CCNO))
        {
           DataTable dt = new DataTable();
           dt = new CostCenter_Facade().Query_CostCenterByNo(CCNO);
           if (dt.Rows.Count == 0)
           {
               strInfo = "NoFound";
           }
           else
           {
               dt = new DIS01_Facade().Query_ProductType(CCNO);
               if (dt.Rows.Count == 0)
               {
                   strInfo = "NoItems";
               }
               foreach (DataRow dr in dt.Rows)
               {
                   strInfo += dr["PRODTYPENO"] + "|" + dr["PRODTYPENAME"] + ",";
               }
           }
        }
        return strInfo;
    }
    [System.Web.Services.WebMethod()]
    [System.Web.Script.Services.ScriptMethod()]
    static public string getPRODINFOExtraSale(string PRODNO)
    {
        DataTable dt = new Product_Facade().Query_ProductInfo(PRODNO);
        string r = "";
        if (dt.Rows.Count > 0)
        {
            r = StringUtil.CStr(dt.Rows[0]["PRODNAME"]);
        }
        if (r != "")
        {
            dt = new Product_Facade().getPRODExtraSale(PRODNO);
            if (dt.Rows.Count > 0)
            {
                r = StringUtil.CStr(dt.Rows[0]["PRODNAME"]) + ";" + StringUtil.CStr(dt.Rows[0]["PRICE"]);
            }
            else
                r = "fail";
        }
        return r;
    }
    [System.Web.Services.WebMethod()]
    [System.Web.Script.Services.ScriptMethod()]
    static public string getDisCountCode()
    {
        string strInfo = "";
        DataTable dt = new DIS01_Facade().Query_MAX_DISCOUNT_CODE();
        if (dt != null && dt.Rows.Count > 0)
        {
            strInfo = StringUtil.CStr(dt.Rows[0][0]).Trim();
        }

        return strInfo;
    }
    [System.Web.Services.WebMethod()]
    [System.Web.Script.Services.ScriptMethod()]
    static public string getAccountCodeBySYSPARA(string PARA_KEY)
    {
        //**2011/4/8 Tina：【Happy Go】及【舊機回收】的會計科目是固定的 
        string strInfo = "";
        DataTable dt = new DIS01_Facade().GetSYS_PARA(PARA_KEY);
        if (dt != null && dt.Rows.Count > 0)
        {
            strInfo = StringUtil.CStr(dt.Rows[0]["PARA_VALUE"]).Trim();
        }

        return strInfo;
    }
    #endregion

    #region All GridView Insert Data To DB Events
    protected DataTable InsertDISCOUNT_MASTER(string MASTER_ID)
    {
        DIS01_DiscountMasterDataSet_DTO.DISCOUNT_MASTERDataTable dtMaster;
        DIS01_DiscountMasterDataSet_DTO.DISCOUNT_MASTERRow drMaster;
        dtMaster = (DIS01_DiscountMasterDataSet_DTO.DISCOUNT_MASTERDataTable)_DiscountMasterDataSet_DTO.Tables["DISCOUNT_MASTER"];
        drMaster = dtMaster.NewDISCOUNT_MASTERRow();

        try
        {
            drMaster.DISCOUNT_MASTER_ID = MASTER_ID;
            drMaster.DISCOUNT_TYPE = decimal.Parse(StringUtil.CStr(this.cbDisType.Value));
            drMaster.DISCOUNT_CODE = this.txtDisCode.Text.Trim();
            drMaster.DISCOUNT_NAME = this.txtDisName.Text.Trim();
            if (this.txtDisAmt.Text.Trim() == "")
                drMaster["DISCOUNT_MONEY"] = DBNull.Value;
            else
                drMaster.DISCOUNT_MONEY = decimal.Parse(this.txtDisAmt.Text.Trim());
            if (this.txtDisRate.Text.Trim() == "")
                drMaster["DISCOUNT_RATE"] = DBNull.Value;
            else
                drMaster.DISCOUNT_RATE = decimal.Parse(this.txtDisRate.Text.Trim());
            // drMaster.DISCOUNT_RATE = decimal.Parse(this.txtDisRate.Text.Trim()) / 100;
            drMaster.DIS_USE_TYPE = StringUtil.CStr(this.cbLimitTNDis.Value);
            if (this.txtLTNDis.Text.Trim() == "")
                drMaster["DIS_USE_MONEY_UBOND"] = DBNull.Value;
            else
                drMaster.DIS_USE_MONEY_UBOND = decimal.Parse(this.txtLTNDis.Text.Trim());
            drMaster.S_DATE = DateUtil.NullDateFormat(StringUtil.CStr(this.SupportStartDateFrom.Value));
            drMaster.E_DATE = DateUtil.NullDateFormat(this.SupportStartDateTo.Value == null ? null : StringUtil.CStr(this.SupportStartDateTo.Value));

            //ACCOUNT_CODE        
            string ACCOUNT_CODE = null;

            ACCOUNT_CODE += this.txtAcct1.Text.Trim();
            ACCOUNT_CODE += this.txtAcct2.Text.Trim();
            ACCOUNT_CODE += this.txtAcct3.Text.Trim();
            ACCOUNT_CODE += this.txtAcct4.Text.Trim();
            ACCOUNT_CODE += this.txtAcct5.Text.Trim();
            ACCOUNT_CODE += this.txtAcct6.Text.Trim();

            drMaster.ACCOUNT_CODE = ACCOUNT_CODE;
            if (qDiscountCode == MASTER_ID)  //由查詢頁面過來的
            {
                DataTable dt = _DIS01_Facade.Query_DiscountMasterByKey(qDiscountCode);
                drMaster.CREATE_USER = StringUtil.CStr(dt.Rows[0]["CREATE_USER"]);
                drMaster.CREATE_DTM = Convert.ToDateTime(StringUtil.CStr(dt.Rows[0]["CREATE_DTM"]));
            }
            else
            {
                drMaster.CREATE_USER = logMsg.CREATE_USER;
                drMaster.CREATE_DTM = DateTime.Now;
            }
            drMaster.MODI_USER = logMsg.MODI_USER;
            drMaster.MODI_DTM = DateTime.Now;
            drMaster.DEL_FLAG = "N";
            drMaster.STATUS = "10";    //狀態:00->未存檔 10->已存檔

            dtMaster.Rows.Add(drMaster);
            _DiscountMasterDataSet_DTO.AcceptChanges();

        }
        catch (Exception ex)
        {
            Logger.Log.Error(ex.Message, ex);
            throw ex;
        }
        return dtMaster;
    }
    protected DataTable InsertRATE_PLAN_DISCOUNT(string MASTER_ID)
    {
        DIS01_DiscountMasterDataSet_DTO.RATE_PLAN_DISCOUNTDataTable dtRPD;
        DIS01_DiscountMasterDataSet_DTO.RATE_PLAN_DISCOUNTRow drRPD;
        dtRPD = (DIS01_DiscountMasterDataSet_DTO.RATE_PLAN_DISCOUNTDataTable)_DiscountMasterDataSet_DTO.Tables["RATE_PLAN_DISCOUNT"];

        try
        {
            foreach (Control cl1 in DISItemChargesAndApply1.Controls)
            {
                string strClType = cl1.GetType().Name;
                if (strClType == "CheckBoxList")
                {
                    CheckBoxList cbl1 = (CheckBoxList)cl1;

                    foreach (ListItem lt1 in cbl1.Items)
                    {
                        drRPD = dtRPD.NewRATE_PLAN_DISCOUNTRow();
                        drRPD.UI_CTRID = cbl1.ID;
                        switch (cbl1.ID)
                        {
                            case "cbRate":
                                drRPD.SA_TYPE = "1";
                                break;
                            case "cbGAG1":
                                drRPD.SA_TYPE = "2";
                                break;
                            case "cbGAG2":
                                drRPD.SA_TYPE = "2";
                                break;
                            case "cbLoyalty":
                                drRPD.SA_TYPE = "3";
                                break;
                            case "cb223G1":
                                drRPD.SA_TYPE = "4";
                                break;
                            case "cb223G2":
                                drRPD.SA_TYPE = "4";
                                break;
                            case "cbMNP":
                                drRPD.SA_TYPE = "5";
                                break;
                        }
                        drRPD.DISCOUNT_MASTER_ID = MASTER_ID;
                        drRPD.RATE_PLAN_DIS_ID = GuidNo.getUUID();
                        drRPD.M_TYPE = StringUtil.CStr(lt1.Value);
                        drRPD.VALUE = (lt1.Selected ? "Y" : "N");
                        dtRPD.Rows.Add(drRPD);
                    }
                }
            }

            _DiscountMasterDataSet_DTO.AcceptChanges();

        }
        catch (Exception ex)
        {
            Logger.Log.Error(ex.Message, ex);
            throw ex;
        }
        return dtRPD;
    }
    protected DataTable InsertPRODUCT_DISCOUNT(string MASTER_ID)
    {
        DIS01_DiscountMasterDataSet_DTO.PRODUCT_DISCOUNTDataTable dtProd;
        DIS01_DiscountMasterDataSet_DTO.PRODUCT_DISCOUNTRow drProd;
        dtProd = (DIS01_DiscountMasterDataSet_DTO.PRODUCT_DISCOUNTDataTable)_DiscountMasterDataSet_DTO.Tables["PRODUCT_DISCOUNT"];
        if (Session["Product"] == null) { return dtProd; }
        DataTable dtData = new DataTable();

        if (Session["Product"] != null) { dtData = (DataTable)Session["Product"]; }
        try
        {
            for (int i = 0; i < dtData.Rows.Count; i++)
            {
                drProd = dtProd.NewPRODUCT_DISCOUNTRow();
                drProd.DISCOUNT_MASTER_ID = MASTER_ID;
                drProd.PROD_DIS_ID = GuidNo.getUUID();
                drProd.PRODNO = StringUtil.CStr(dtData.Rows[i]["PRODNO"]);
                drProd.CREATE_USER = logMsg.CREATE_USER;
                drProd.CREATE_DTM = DateTime.Now;
                drProd.MODI_USER = logMsg.MODI_USER;
                drProd.MODI_DTM = DateTime.Now;
                dtProd.Rows.Add(drProd);
            }

            _DiscountMasterDataSet_DTO.AcceptChanges();

            //寫回資料庫
            //_DIS01_Facade.InsertDISCOUNTALLDataSet(_DiscountMasterDataSet_DTO, dtProd.TableName);
        }
        catch (Exception ex)
        {
            Logger.Log.Error(ex.Message, ex);
            throw ex;
        }
        return dtProd;
    }
    protected DataTable InsertSTORE_DISCOUNT(string MASTER_ID)
    {
        DIS01_DiscountMasterDataSet_DTO.STORE_DISCOUNTDataTable dtStore;
        DIS01_DiscountMasterDataSet_DTO.STORE_DISCOUNTRow drStore;
        dtStore = (DIS01_DiscountMasterDataSet_DTO.STORE_DISCOUNTDataTable)_DiscountMasterDataSet_DTO.Tables["STORE_DISCOUNT"];
        if (Session["Store"] == null) { return dtStore; }
        DataTable dtData = new DataTable();

        if (Session["Store"] != null) { dtData = (DataTable)Session["Store"]; }
        try
        {
            for (int i = 0; i < dtData.Rows.Count; i++)
            {
                drStore = dtStore.NewSTORE_DISCOUNTRow();
                drStore.DISCOUNT_MASTER_ID = MASTER_ID;
                drStore.STORE_DIS_ID = GuidNo.getUUID();
                drStore.STORE_NO = StringUtil.CStr(dtData.Rows[i]["STORE_NO"]);
                drStore.DIS_USE_COUNT = decimal.Parse(StringUtil.CStr(dtData.Rows[i]["DIS_USE_COUNT"]));
                drStore.CREATE_USER = logMsg.CREATE_USER;
                drStore.CREATE_DTM = DateTime.Now;
                drStore.MODI_USER = logMsg.MODI_USER;
                drStore.MODI_DTM = DateTime.Now;
                dtStore.Rows.Add(drStore);
            }

            _DiscountMasterDataSet_DTO.AcceptChanges();

            //寫回資料庫
            //_DIS01_Facade.InsertDISCOUNTALLDataSet(_DiscountMasterDataSet_DTO, dtStore.TableName);
        }
        catch (Exception ex)
        {
            Logger.Log.Error(ex.Message, ex);
            throw ex;
        }
        return dtStore;
    }
    protected DataTable InsertPROMOTION_DISCOUNT(string MASTER_ID)
    {
        DIS01_DiscountMasterDataSet_DTO.PROMOTION_DISCOUNTDataTable dtPromo;
        DIS01_DiscountMasterDataSet_DTO.PROMOTION_DISCOUNTRow drPromo;
        dtPromo = (DIS01_DiscountMasterDataSet_DTO.PROMOTION_DISCOUNTDataTable)_DiscountMasterDataSet_DTO.Tables["PROMOTION_DISCOUNT"];
        if (Session["Promotion"] == null) { return dtPromo; }
        DataTable dtData = new DataTable();

        if (Session["Promotion"] != null) { dtData = (DataTable)Session["Promotion"]; }
        try
        {
            for (int i = 0; i < dtData.Rows.Count; i++)
            {
                drPromo = dtPromo.NewPROMOTION_DISCOUNTRow();
                drPromo.DISCOUNT_MASTER_ID = MASTER_ID;
                drPromo.PROM_DIS_ID = GuidNo.getUUID();
                drPromo.PROMOTION_CODE = StringUtil.CStr(dtData.Rows[i]["PROMO_NO"]);
                drPromo.CREATE_USER = logMsg.CREATE_USER;
                drPromo.CREATE_DTM = DateTime.Now;
                drPromo.MODI_USER = logMsg.MODI_USER;
                drPromo.MODI_DTM = DateTime.Now;
                dtPromo.Rows.Add(drPromo);
            }

            _DiscountMasterDataSet_DTO.AcceptChanges();

        }
        catch (Exception ex)
        {
            Logger.Log.Error(ex.Message, ex);
            throw ex;
        }
        return dtPromo;
    }
    protected DataTable InsertCUST_LEVEL_DISCOUNT(string MASTER_ID)
    {
        DIS01_DiscountMasterDataSet_DTO.CUST_LEVE_DISCOUNTDataTable dtCLD;
        DIS01_DiscountMasterDataSet_DTO.CUST_LEVE_DISCOUNTRow drCLD;
        dtCLD = (DIS01_DiscountMasterDataSet_DTO.CUST_LEVE_DISCOUNTDataTable)_DiscountMasterDataSet_DTO.Tables["CUST_LEVE_DISCOUNT"];
        if (Session["Customer1"] == null && Session["Customer2"] == null) { return dtCLD; }

        DataTable dtData1 = new DataTable();
        DataTable dtData2 = new DataTable();

        if (Session["Customer1"] != null) { dtData1 = (DataTable)Session["Customer1"]; }

        if (Session["Customer2"] != null) { dtData2 = (DataTable)Session["Customer2"]; }
        try
        {
            if (dtData1 != null)
            {
                for (int i = 0; i < dtData1.Rows.Count; i++)
                {
                    drCLD = dtCLD.NewCUST_LEVE_DISCOUNTRow();
                    drCLD.DISCOUNT_MASTER_ID = MASTER_ID;
                    drCLD.CUST_LEVEL_ID = StringUtil.CStr(dtData1.Rows[i]["CUST_LEVEL_ID"]);
                    drCLD.ARPB_S = decimal.Parse(StringUtil.CStr(dtData1.Rows[i]["ARPB_S"]));
                    drCLD.ARPB_E = decimal.Parse(StringUtil.CStr(dtData1.Rows[i]["ARPB_E"]));
                    drCLD.USE_TYPE = StringUtil.CStr(dtData1.Rows[i]["USE_TYPE"]);
                    dtCLD.Rows.Add(drCLD);
                }
            }
            if (dtData2 != null)
            {
                for (int i = 0; i < dtData2.Rows.Count; i++)
                {
                    drCLD = dtCLD.NewCUST_LEVE_DISCOUNTRow();
                    drCLD.DISCOUNT_MASTER_ID = MASTER_ID;
                    drCLD.CUST_LEVEL_ID = StringUtil.CStr(dtData2.Rows[i]["CUST_LEVEL_ID"]);
                    drCLD.MSISDN = StringUtil.CStr(dtData2.Rows[i]["MSISDN"]);
                    drCLD.USE_TYPE = StringUtil.CStr(dtData2.Rows[i]["USE_TYPE"]);
                    dtCLD.Rows.Add(drCLD);
                }
            }

            _DiscountMasterDataSet_DTO.AcceptChanges();

            //寫回資料庫
            //_DIS01_Facade.InsertDISCOUNTALLDataSet(_DiscountMasterDataSet_DTO, dtCLD.TableName);
        }
        catch (Exception ex)
        {
            Logger.Log.Error(ex.Message, ex);
            throw ex;
        }
        return dtCLD;
    }
    protected DataTable InsertCOST_CENTER_DISCOUNT(string MASTER_ID)
    {
        DIS01_DiscountMasterDataSet_DTO.COST_CENTER_DISCOUNTDataTable dtCCD;
        DIS01_DiscountMasterDataSet_DTO.COST_CENTER_DISCOUNTRow drCCD;
        dtCCD = (DIS01_DiscountMasterDataSet_DTO.COST_CENTER_DISCOUNTDataTable)_DiscountMasterDataSet_DTO.Tables["COST_CENTER_DISCOUNT"];
        if (Session["CostCenter"] == null) { return dtCCD; }

        DataTable dtData = new DataTable();


        if (Session["CostCenter"] != null) { dtData = (DataTable)Session["CostCenter"]; }
        try
        {
            for (int i = 0; i < dtData.Rows.Count; i++)
            {
                drCCD = dtCCD.NewCOST_CENTER_DISCOUNTRow();
                drCCD.DISCOUNT_MASTER_ID = MASTER_ID;
                drCCD.COSTCENTER_DIS_ID = StringUtil.CStr(dtData.Rows[i]["COSTCENTER_DIS_ID"]);
                drCCD.COST_CENTER_NO = StringUtil.CStr(dtData.Rows[i]["COST_CENTER_NO"]);
                drCCD.PROD_CATEG = StringUtil.CStr(dtData.Rows[i]["PROD_CATEG"]);
                drCCD.ACCOUNTCODE = StringUtil.CStr(dtData.Rows[i]["ACCOUNTCODE"]);
                string AMT = StringUtil.CStr(dtData.Rows[i]["AMT"]);
                drCCD.AMT = decimal.Parse(AMT == "" ? "0" : AMT);
                drCCD.REMARK = StringUtil.CStr(dtData.Rows[i]["REMARK"]);
                dtCCD.Rows.Add(drCCD);
            }

            _DiscountMasterDataSet_DTO.AcceptChanges();

            //寫回資料庫
            //_DIS01_Facade.InsertDISCOUNTALLDataSet(_DiscountMasterDataSet_DTO, dtCCD.TableName);
        }
        catch (Exception ex)
        {
            Logger.Log.Error(ex.Message, ex);
            throw ex;
        }
        return dtCCD;
    }
    protected DataTable InsertGIF_DISCOUNT(string MASTER_ID)
    {
        DIS01_DiscountMasterDataSet_DTO.GIFT_DISCOUNTDataTable dtGif;
        DIS01_DiscountMasterDataSet_DTO.GIFT_DISCOUNTRow drGif;
        dtGif = (DIS01_DiscountMasterDataSet_DTO.GIFT_DISCOUNTDataTable)_DiscountMasterDataSet_DTO.Tables["GIFT_DISCOUNT"];
        if (Session["SetProduct"] == null) { return dtGif; }

        DataTable dtData = new DataTable();

        if (Session["SetProduct"] != null) { dtData = (DataTable)Session["SetProduct"]; }
        try
        {
            for (int i = 0; i < dtData.Rows.Count; i++)
            {
                drGif = dtGif.NewGIFT_DISCOUNTRow();
                drGif.DISCOUNT_MASTER_ID = MASTER_ID;
                drGif.PROD_DIS_ID = GuidNo.getUUID();
                drGif.PRODNO = StringUtil.CStr(dtData.Rows[i]["PRODNO"]);
                drGif.AMT = decimal.Parse(StringUtil.CStr(dtData.Rows[i]["AMT"]));
                drGif.CREATE_USER = logMsg.CREATE_USER;
                drGif.CREATE_DTM = DateTime.Now;
                drGif.MODI_USER = logMsg.MODI_USER;
                drGif.MODI_DTM = DateTime.Now;
                dtGif.Rows.Add(drGif);
            }

            _DiscountMasterDataSet_DTO.AcceptChanges();

            //寫回資料庫
            //_DIS01_Facade.InsertDISCOUNTALLDataSet(_DiscountMasterDataSet_DTO, dtGif.TableName);
        }
        catch (Exception ex)
        {
            Logger.Log.Error(ex.Message, ex);
            throw ex;
        }
        return dtGif;
    }
    protected DataTable InsertADD_IN_PROD_DISCOUNT(string MASTER_ID)
    {

        DIS01_DiscountMasterDataSet_DTO.ADD_IN_PROD_DISCOUNTDataTable dtAdd;
        DIS01_DiscountMasterDataSet_DTO.ADD_IN_PROD_DISCOUNTRow drAdd;
        dtAdd = (DIS01_DiscountMasterDataSet_DTO.ADD_IN_PROD_DISCOUNTDataTable)_DiscountMasterDataSet_DTO.Tables["ADD_IN_PROD_DISCOUNT"];
        if (Session["AddProduct"] == null) { return dtAdd; }

        DataTable dtData = new DataTable();


        if (Session["AddProduct"] != null) { dtData = (DataTable)Session["AddProduct"]; }
        try
        {
            for (int i = 0; i < dtData.Rows.Count; i++)
            {
                drAdd = dtAdd.NewADD_IN_PROD_DISCOUNTRow();
                drAdd.DISCOUNT_MASTER_ID = MASTER_ID;
                drAdd.PROD_DIS_ID = GuidNo.getUUID();
                drAdd.PRODNO = StringUtil.CStr(dtData.Rows[i]["PRODNO"]);
                drAdd.DIS_AMT = decimal.Parse(StringUtil.CStr(dtData.Rows[i]["DIS_AMT"]));
                drAdd.UNIT_PRICE = decimal.Parse(StringUtil.CStr(dtData.Rows[i]["UNIT_PRICE"]));
                drAdd.CREATE_USER = logMsg.CREATE_USER;
                drAdd.CREATE_DTM = DateTime.Now;
                drAdd.MODI_USER = logMsg.MODI_USER;
                drAdd.MODI_DTM = DateTime.Now;
                dtAdd.Rows.Add(drAdd);
            }

            _DiscountMasterDataSet_DTO.AcceptChanges();

            //寫回資料庫
            //_DIS01_Facade.InsertDISCOUNTALLDataSet(_DiscountMasterDataSet_DTO, dtAdd.TableName);
        }
        catch (Exception ex)
        {
            Logger.Log.Error(ex.Message, ex);
            throw ex;
        }
        return dtAdd;
    }
    //新增商品料號到PRODUCT
    private DataTable InserPRODUCT(string MASTER_ID)
    {

        OPT10_Product_DTO _OPT10_Product_DTO = new OPT10_Product_DTO();
        OPT10_Product_DTO.PRODUCTDataTable dtSYS;
        OPT10_Product_DTO.PRODUCTRow drSYS;

        dtSYS = (OPT10_Product_DTO.PRODUCTDataTable)_OPT10_Product_DTO.Tables["PRODUCT"];
        try
        {
            drSYS = dtSYS.NewPRODUCTRow();

            //ACCOUNT_CODE        
            string ACCOUNT_CODE = null;

            ACCOUNT_CODE += this.txtAcct1.Text.Trim();
            ACCOUNT_CODE += this.txtAcct2.Text.Trim();
            ACCOUNT_CODE += this.txtAcct3.Text.Trim();
            ACCOUNT_CODE += this.txtAcct4.Text.Trim();
            ACCOUNT_CODE += this.txtAcct5.Text.Trim();
            ACCOUNT_CODE += this.txtAcct6.Text.Trim();

            //異動的欄位
            drSYS["PRODNO"] = this.txtDisCode.Text.Trim();
            drSYS["PRODNAME"] = this.txtDisName.Text.Trim();

            if (this.txtDisAmt.Text.Trim() != "")
                drSYS["PRICE"] = decimal.Parse(this.txtDisAmt.Text.Trim());

            drSYS["ISSTOCK"] = "N";
            drSYS["ACCOUNTCODE"] = ACCOUNT_CODE;
            drSYS["PRODTYPENO"] = decimal.Parse(StringUtil.CStr(this.cbDisType.Value));
            drSYS["COMPANYCODE"] = "01";
            drSYS["IS_DISCOUNT"] = "Y";
            drSYS.IS_POS_DEF_PRICE = "N";
            drSYS.TAXABLE = "Y";
            drSYS.TAXRATE = 5;
            drSYS.DEL_FLAG = "N";
            drSYS.DS_FLAG = "N";
            drSYS["IMEI_FLAG"] = "1";

            drSYS.S_DATE = DateUtil.NullDateFormat(StringUtil.CStr(this.SupportStartDateFrom.Value));
            drSYS.E_DATE = DateUtil.NullDateFormat(this.SupportStartDateTo.Value == null ? null : StringUtil.CStr(this.SupportStartDateTo.Value));

            drSYS["MODI_USER"] = logMsg.MODI_USER;
            drSYS["MODI_DTM"] = System.DateTime.Now;
            if (qDiscountCode == MASTER_ID)  //由查詢頁面過來的
            {
                DataTable dt = _DIS01_Facade.Query_DiscountMasterByKey(qDiscountCode);
                drSYS["CREATE_USER"] = StringUtil.CStr(dt.Rows[0]["CREATE_USER"]);
                drSYS["CREATE_DTM"] = StringUtil.CStr(dt.Rows[0]["CREATE_DTM"]);
            }
            else
            {
                drSYS["CREATE_USER"] = drSYS["MODI_USER"];
                drSYS["CREATE_DTM"] = drSYS["MODI_DTM"];
            }

            dtSYS.AddPRODUCTRow(drSYS);
            _OPT10_Product_DTO.AcceptChanges();

        }
        catch (Exception ex)
        {
            Logger.Log.Error(ex.Message, ex);
            throw ex;
        }

        return dtSYS;
    }
    #endregion

    #region "畫面預設控制"
    private void defaultGUI(EventArgs e)
    {
        BindDisType(); //類別

        this.btnDelete.ClientEnabled = false;//刪除
        lblStatus.Text = "未存檔";//狀態
        cbLimitTNDis.SelectedIndex = 0;//折扣上限次數
        SupportStartDateFrom.Text = DateTime.Now.ToString("yyyy/MM/dd");//有效期間(起)
        this.SupportStartDateFrom.MinDate = System.DateTime.Today;
        this.SupportStartDateTo.MinDate = System.DateTime.Today;
    }
    private void getDataGUI(EventArgs e)
    {
        _DIS01_Facade = new DIS01_Facade();
        DataTable dtMaster = new DataTable();
        dtMaster = _DIS01_Facade.Query_DiscountMasterByKey(qDiscountCode);
        if (dtMaster.Rows.Count > 0)
        {
            DataRow dr = dtMaster.Rows[0];
            BindDisType();                                            //類別
            string sDisType = StringUtil.CStr(dr["DISCOUNT_TYPE"]);
            //折扣類別
            cbDisType.Value = sDisType;                               

            if (sDisType == "99")
            {
                cbDisType.Text = "網購折扣";
            }
            
            txtDisCode.Text = StringUtil.CStr(dr["DISCOUNT_CODE"]);   //折扣料號
            txtDisCode.Enabled = false; //不可編輯折扣料號，避免更新Product Table時發生問題
            txtDisName.Text = StringUtil.CStr(dr["DISCOUNT_NAME"]);   //折扣名稱
            lblStatus.Text = StringUtil.CStr(dr["STATUS"]);           //狀態
            txtDisAmt.Text = StringUtil.CStr(dr["DISCOUNT_MONEY"]);   //折扣金額
            txtDisRate.Text = StringUtil.CStr(dr["DISCOUNT_RATE"]);   //折扣比率
            lblDate.Text = StringUtil.CStr(dr["MODI_DTM"]);           //異動日期
            //會計科目
            string strAccount =  StringUtil.CStr(dr["ACCOUNT_CODE"]);
            if (!string.IsNullOrEmpty(strAccount))
            {
                txtAcct1.Text = strAccount.Substring(0, 2);
                txtAcct2.Text = strAccount.Substring(2, 3);
                txtAcct3.Text = strAccount.Substring(5, 4);
                txtAcct4.Text = strAccount.Substring(9, 6);
                txtAcct5.Text = strAccount.Substring(15, 4);
                txtAcct6.Text = strAccount.Substring(19, 4);
            }
            cbLimitTNDis.Value = StringUtil.CStr(dr["DIS_USE_TYPE"]);             //折扣上限次數使用方式
            txtLTNDis.Text = StringUtil.CStr(dr["DIS_USE_MONEY_UBOND"]);          //折扣上限設定金額
            lblStaff.Text = StringUtil.CStr(dr["MODI_USER"]) + " " + new Employee_Facade().GetEmpName(StringUtil.CStr(dr["MODI_USER"]));  //異動人員
            SupportStartDateFrom.Text = StringUtil.CStr(dr["S_DATE"]);            //有效期間(起)
            SupportStartDateTo.Text = StringUtil.CStr(dr["E_DATE"]);              //有效期間(迄)

            if (StringUtil.CStr(cbLimitTNDis.Value) == "1")
            {
                txtLTNDis.ClientEnabled = false;//折扣上線次數
            }

            DataSet dsDetail = _DIS01_Facade.Query_AllDetailData_ByMasterUUID(qDiscountCode);
            Session["RatePlan"] = dsDetail.Tables["RatePlan"];
            Session["Product"] = dsDetail.Tables["Product"];
            Session["Store"] = dsDetail.Tables["Store"];
            Session["Promotion"] = dsDetail.Tables["Promotion"];
            Session["Customer1"] = dsDetail.Tables["Customer1"];
            Session["Customer2"] = dsDetail.Tables["Customer2"];
            Session["CostCenter"] = dsDetail.Tables["CostCenter"];
            Session["SetProduct"] = dsDetail.Tables["SetProduct"];
            Session["AddProduct"] = dsDetail.Tables["AddProduct"];

            string strStatus = StringUtil.CStr(dr["STATUS_DATE"]);
            this.hdSTATUS_DATE.Text = strStatus;
            if (sDisType == "99")
            {
                //**2011/05/04 Tina：類別為【網購折扣】，只能檢視不能修改
                EnabledPage(false);
                SupportStartDateTo.ClientEnabled = false;
                btnSave.ClientEnabled = false;
            }
            else
            {
                if (strStatus == "有效")
                {
                    //**有效的折扣料號只能修改"有效期間_訖"
                    EnabledPage(false);
                    this.SupportStartDateTo.MinDate = System.DateTime.Today;
                }
            }

            txtLTNDis.ClientEnabled = false;
        }
    }
    private void BindDisType()
    {
        cbDisType.DataSource = getDiscountCategory();//類別
        cbDisType.TextField = "Field";
        cbDisType.ValueField = "Value";
        cbDisType.DataBind();
        cbDisType.SelectedIndex = 0;
    }
    #endregion

    protected void btnSave_Click(object sender, EventArgs e)
    {
        _DIS01_Facade = new DIS01_Facade();
        _DiscountMasterDataSet_DTO = new DIS01_DiscountMasterDataSet_DTO();
        DataSet ds = new DataSet();
        try
        {
            if (!checkSaveData()) return;  //欄位檢查

            string MASTER_ID = GuidNo.getUUID();
            if (!string.IsNullOrEmpty(qDiscountCode)) //由查詢頁過來的
            {
                MASTER_ID = qDiscountCode;
            }
            DateTime sdate = (DateTime)this.SupportStartDateFrom.Value;
            string strDate = sdate.ToString("yyyy/MM/dd");
            //折扣設定主檔
            if (checkHaveDISCOUNT_NO(this.txtDisCode.Text.Trim(), strDate))
            {
                ds.Tables.Add(InsertDISCOUNT_MASTER(MASTER_ID).Copy());
            }
            else
            { 
                //折扣料號已重複
                ScriptManager.RegisterClientScriptBlock(this, typeof(string), "InsertAllDBError", "ShowMessage('此折扣料號不允許設定!!');", true); //消除[存檔中訊息]
                return;
            }

            switch (StringUtil.CStr(cbDisType.Value))
            {
                case "1":  //一般
                    #region 一般
                    //費率及申辦類型
                    ds.Tables.Add(InsertRATE_PLAN_DISCOUNT(MASTER_ID).Copy());
                    //指定商品
                    ds.Tables.Add(InsertPRODUCT_DISCOUNT(MASTER_ID).Copy());
                    //指定門市
                    ds.Tables.Add(InsertSTORE_DISCOUNT(MASTER_ID).Copy());
                    //指定促銷
                    ds.Tables.Add(InsertPROMOTION_DISCOUNT(MASTER_ID).Copy());
                    //客戶對像
                    ds.Tables.Add(InsertCUST_LEVEL_DISCOUNT(MASTER_ID).Copy());
                    //成本中心
                    ds.Tables.Add(InsertCOST_CENTER_DISCOUNT(MASTER_ID).Copy());
                    //贈品設定
                    ds.Tables.Add(new DIS01_DiscountMasterDataSet_DTO.GIFT_DISCOUNTDataTable());
                    //加購價
                    ds.Tables.Add(new DIS01_DiscountMasterDataSet_DTO.ADD_IN_PROD_DISCOUNTDataTable());
                    #endregion
                    break;
                case "6":  //贈品設定
                    #region 贈品設定
                    //費率及申辦類型
                    ds.Tables.Add(InsertRATE_PLAN_DISCOUNT(MASTER_ID).Copy());
                    //指定商品
                    ds.Tables.Add(InsertPRODUCT_DISCOUNT(MASTER_ID).Copy());
                    //指定門市
                    ds.Tables.Add(InsertSTORE_DISCOUNT(MASTER_ID).Copy());
                    //指定促銷
                    ds.Tables.Add(InsertPROMOTION_DISCOUNT(MASTER_ID).Copy());
                    //客戶對像
                    ds.Tables.Add(InsertCUST_LEVEL_DISCOUNT(MASTER_ID).Copy());
                    //成本中心
                    ds.Tables.Add(InsertCOST_CENTER_DISCOUNT(MASTER_ID).Copy());
                    //贈品設定
                    ds.Tables.Add(InsertGIF_DISCOUNT(MASTER_ID).Copy());
                    //加購價
                    ds.Tables.Add(new DIS01_DiscountMasterDataSet_DTO.ADD_IN_PROD_DISCOUNTDataTable());
                    #endregion
                    break;
                case "7":  //加價購
                    #region 加價購
                    //費率及申辦類型
                    ds.Tables.Add(InsertRATE_PLAN_DISCOUNT(MASTER_ID).Copy());
                    //指定商品
                    ds.Tables.Add(InsertPRODUCT_DISCOUNT(MASTER_ID).Copy());
                    //指定門市
                    ds.Tables.Add(InsertSTORE_DISCOUNT(MASTER_ID).Copy());
                    //指定促銷
                    ds.Tables.Add(InsertPROMOTION_DISCOUNT(MASTER_ID).Copy());
                    //客戶對象
                    ds.Tables.Add(InsertCUST_LEVEL_DISCOUNT(MASTER_ID).Copy());
                    //成本中心
                    ds.Tables.Add(InsertCOST_CENTER_DISCOUNT(MASTER_ID).Copy());
                    //贈品設定
                    ds.Tables.Add(new DIS01_DiscountMasterDataSet_DTO.GIFT_DISCOUNTDataTable());
                    //加購價
                    ds.Tables.Add(InsertADD_IN_PROD_DISCOUNT(MASTER_ID).Copy());
                    #endregion
                    break;
                //**2011/04/11 Tina：類別為「舊機回收」、「租賃」、「特殊折扣」時，頁籤全部隱藏。
                //**2011/04/14 Tina：SA文件->1.6.1.3 選取類別為「HapptGo折扣」，頁籤全部為Disable，使用者不可輸入。
                case "2":  //舊機回收
                case "3":  //租賃
                case "4":  //特殊折扣
                case "5":  //HappyGo折扣
                default:
                    break;
            }

            //**2011/04/22 Tina：如果「指定門市」頁籤有輸入門市，但其它頁籤都沒有輸入值(不包含成本中心)，就不能存檔。
            if (ds.Tables["STORE_DISCOUNT"] != null && ds.Tables["STORE_DISCOUNT"].Rows.Count > 0)
            {
                if (ds.Tables["PRODUCT_DISCOUNT"].Rows.Count == 0 &&        //指定商品
                    ds.Tables["PROMOTION_DISCOUNT"].Rows.Count == 0 &&      //指定促銷
                    ds.Tables["CUST_LEVE_DISCOUNT"].Rows.Count == 0 &&      //客戶對象
                    //ds.Tables["COST_CENTER_DISCOUNT"].Rows.Count == 0 &&    //成本中心
                    ds.Tables["GIFT_DISCOUNT"].Rows.Count == 0 &&           //贈品設定
                    ds.Tables["ADD_IN_PROD_DISCOUNT"].Rows.Count == 0)      //加價購
                {
                    ScriptManager.RegisterClientScriptBlock(this, typeof(string), "InsertAllDBError", "ShowMessage('「指定門市」有輸入門市，其它頁籤都沒有輸入值，不允許設定!!');", true); //消除[存檔中訊息]
                    return;
                }
            }

            //加入商品主檔
            ds.Tables.Add(InserPRODUCT(MASTER_ID).Copy());

            Session["MASTERID_ADD"] = MASTER_ID;
            if (string.IsNullOrEmpty(qDiscountCode))  //非查詢頁面過來的，Insert...
            {
                _DIS01_Facade.InsertDISCOUNTALLDataSet(ds);
            }                                         //由查詢頁過來的，Update...
            else
            {
                //只有折扣主檔(DISCOUNT_MASTER)和商品主檔(PRODUCT)是採用Update，其餘則採用Delete + Insert
                _DIS01_Facade.UpdateDISCOUNTALLDataSet(ds, MASTER_ID);
            }

            //新增或修改折扣料號設定資料，須發送Mail通知建單人員、異動人員，以及Accounting人員。
            _DIS01_Facade.SEND_MAIL(MASTER_ID);

            //this.btnSave.Enabled = false;
            this.btnDelete.ClientEnabled = true;

            if (string.IsNullOrEmpty(qDiscountCode))  //非查詢頁面過來的，Insert...
            {
                //**2011/04/21 Tina：傳遞參數時，要先以加密處理。
                ScriptManager.RegisterClientScriptBlock(this, typeof(string), "InsertAllDBOk", "CloseMessage(); alert('折扣設定儲存完成!!'); window.location='DIS01.aspx?Param=" + Utils.Param_Encrypt("DiscountCodeUUID=" + MASTER_ID) + "';", true); //消除[存檔中訊息]
            }                                         //由查詢頁過來的，Update...
            else
            {
                ScriptManager.RegisterClientScriptBlock(this, typeof(string), "InsertAllDBOk", "CloseMessage(); alert('折扣設定儲存完成!!');", true); //消除[存檔中訊息]
            }

            //ScriptManager.RegisterClientScriptBlock(this, typeof(string), "InsertAllDBOk", "CloseMessage(); alert('折扣設定儲存完成!!'); window.location='DIS01.aspx';", true); //消除[存檔中訊息]
            this.lblStatus.Text = "已存檔";
            this.lblDate.Text = DateTime.Now.ToString("yyyy/MM/dd");
            this.lblStaff.Text = logMsg.CREATE_USER;
        }
        catch (Exception ex)
        {
            string strError = ex.Message.Replace("'", "-").Replace("\"", " ").Replace("\r", "").Replace("\n", "");
            ScriptManager.RegisterClientScriptBlock(this, typeof(string), "InsertAllDBError", "CloseMessage(); alert('" + strError + "');", true); //消除[存檔中訊息]
            Logger.Log.Error(ex.Message, ex);
        }

    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("DIS01.aspx");
    }

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        _DIS01_Facade = new DIS01_Facade();

        try
        {
            string MASTER_ID = (Session["MASTERID_ADD"] == null ? "" : StringUtil.CStr(Session["MASTERID_ADD"]));
            if (MASTER_ID == "") { return; }

            _DIS01_Facade.DeletOne_DISCOUNT_MASTER_MethodSet(MASTER_ID);
            ScriptManager.RegisterClientScriptBlock(this, typeof(string), "InsertAllDBOk", "alert('折扣設定巳刪除!!');", true);
            Session["MASTERID_ADD"] = null;
            //defaultData(); //重回預設值
            Response.Redirect("DIS01.aspx");
        }
        catch (Exception ex)
        {
            Logger.Log.Error(ex.Message, ex);
        }
    }

    protected void btnTemplate_Click(object sender, EventArgs e)
    {
        string strDisType = StringUtil.CStr(cbDisType.Value);
        string filePath = "../../../Downloads/";
        switch (strDisType)
        {
            case "1":  //一般
                filePath += "GENERAL.xls";
                break;
            case "2":  //舊機回收
                filePath += "OLDPHONE.xls";
                break;
            default:
                break;

        }
        ScriptManager.RegisterClientScriptBlock(this,
                                               this.GetType(),
                                               "Template",
                                               "document.getElementById('" + fDownload.ClientID + "').src='" + filePath + "';",
                                               true);
    }

    private bool checkSaveData()
    {
        bool boolTmp = true;
        switch (StringUtil.CStr(cbDisType.Value))
        {
            case "1":  //一般
            case "6":  //贈品設定
            case "7":  //加價購
                #region 商品折扣比率
                if (this.txtDisRate.Text != "")
                {
                    DataTable dtData = new DataTable();

                    if (Session["Product"] != null) { dtData = (DataTable)Session["Product"]; }

                    if (dtData.Rows.Count == 0)
                    {
                        ScriptManager.RegisterClientScriptBlock(this, typeof(string), "checkSaveData", "ShowMessage('【商品折扣比率】有值，請選擇指定商品!!');", true);
                        boolTmp = false;
                    }
                }
                #endregion

                #region 指定門市
                if (this.cbLimitTNDis.SelectedIndex == 2)
                {
                    if (this.txtLTNDis.Text != "")
                    {

                        decimal tmpNum = 0;
                        DataTable dtData = new DataTable();


                        if (Session["Store"] != null) { dtData = (DataTable)Session["Store"]; }

                        if (dtData.Rows.Count > 0)
                        {
                            for (int i = 0; i < dtData.Rows.Count; i++)
                            {
                                tmpNum += decimal.Parse(StringUtil.CStr(dtData.Rows[i]["DIS_USE_COUNT"]));
                            }

                        }

                        if ((Math.Abs(decimal.Parse(this.txtLTNDis.Text)) != tmpNum) || (Math.Abs(decimal.Parse(this.txtLTNDis.Text)) == 0 && tmpNum == 0))
                        {
                            ScriptManager.RegisterClientScriptBlock(this, typeof(string), "checkSaveData", "ShowMessage('指定門市【折扣上限次數】加總需等於折扣上限次數【總量】!!');", true);
                            boolTmp = false;
                        }

                    }
                }
                #endregion

                #region 成本中心
                if (this.txtDisAmt.Text != "")
                {
                    DataTable dtData = new DataTable();
                    decimal tmpAmt = 0;

                    if (Session["CostCenter"] != null) { dtData = (DataTable)Session["CostCenter"]; }

                    if (dtData.Rows.Count > 0)
                    {
                        for (int i = 0; i < dtData.Rows.Count; i++)
                        {
                            tmpAmt += decimal.Parse(StringUtil.CStr(dtData.Rows[i]["AMT"]));
                        }

                    }

                    if ((Math.Abs(decimal.Parse(this.txtDisAmt.Text)) != tmpAmt) || (Math.Abs(decimal.Parse(this.txtDisAmt.Text)) == 0 && tmpAmt == 0))
                    {
                        ScriptManager.RegisterClientScriptBlock(this, typeof(string), "checkSaveData", "ShowMessage('成本中心【金額】加總需等於【折扣金額】!!');", true);
                        boolTmp = false;
                    }

                }
                #endregion
                break;
            case "2":   //舊機回收
            case "3":   //租賃
            case "4":   //特殊折扣
            case "5":   //HappyGo折扣
                break;
            default:
                break;
        }
        return boolTmp;
    }

    private DataTable getDiscountCategory()
    {
        DataTable dt = new DataTable();
        dt.Columns.Add(new DataColumn("Field", typeof(string)));
        dt.Columns.Add(new DataColumn("Value", typeof(string)));
        string[] arrStr = new string[] { 
                "一般", "1",
                "贈品設定", "6", 
                "加價購", "7", 
                "舊機回收","2" ,
                "租賃","3",
                "特殊折扣","4" ,
                "HappyGo折扣","5"};

        DataRow dr;
        for (int i = 0; i < arrStr.Length; i += 2)
        {
            dr = dt.NewRow();
            dr["Field"] = arrStr[i];
            dr["Value"] = arrStr[i + 1];
            dt.Rows.Add(dr);
        }
        return dt;
    }

    private void EnabledPage(bool flag)
    {
        //費率及申辦類型1
        this.DISItemChargesAndApply1.Enabled = flag;
        //指定商品2
        this.PRODUCT_DISCOUNT1.Enabled = flag;
        //指定門市3
        this.STORE_DISCOUNT1.Enabled = flag;
        //指定促銷4
        this.PROMOTION_DISCOUNT1.Enabled = flag;
        //客戶對象5
        this.CUST_LEVE_DISCOUNT1.Enabled = flag;
        //成本中心6
        this.COST_CENTER_DISCOUNT1.Enabled = flag;
        //贈品設定7
        this.GIFT_DISCOUNT1.Enabled = flag;
        //加價購8
        this.ADD_IN_PROD_DISCOUNT1.Enabled = flag;

        this.cbDisType.ClientEnabled = flag;
        this.txtDisName.ClientEnabled = flag;
        this.txtDisAmt.ClientEnabled = flag;
        this.txtDisRate.ClientEnabled = flag;
        this.txtAcct1.ClientEnabled = flag;
        this.txtAcct2.ClientEnabled = flag;
        this.txtAcct3.ClientEnabled = flag;
        this.txtAcct4.ClientEnabled = flag;
        this.txtAcct5.ClientEnabled = flag;
        this.txtAcct6.ClientEnabled = flag;
        this.cbLimitTNDis.ClientEnabled = flag;
        this.txtLTNDis.ClientEnabled = flag;
        this.SupportStartDateFrom.ClientEnabled = flag;
        this.btnDelete.ClientEnabled = flag;

    }

}

