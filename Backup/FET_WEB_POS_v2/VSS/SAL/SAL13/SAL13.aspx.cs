using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Advtek.Utility;
using System.Data;
using DevExpress.Web;
using DevExpress.Web.ASPxGridView;
using DevExpress.Web.ASPxPager;
using DevExpress.Web.ASPxEditors;
using FET.POS.Model.Facade.FacadeImpl;
using FET.POS.Model.Common;
using FET.POS.Model.DTO;

public partial class VSS_SAL_SAL13 : BasePage
{
    string DISCOUNT_MASTER_ID
    {
        set
        {
            ViewState["DISCOUNT_MASTER_ID"] = value;
        }
        get
        {
            if (ViewState["DISCOUNT_MASTER_ID"] == null)
                return string.Empty;

            return (string)ViewState["DISCOUNT_MASTER_ID"];
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack && !IsCallback)
        {
            //隱藏字定控制項中所有GA、223
            Control _ctr = DISItemChargesAndApply1.FindControl("cbGAAll");
            if (_ctr != null) 
            {
                ((ASPxCheckBox)_ctr).Visible = false;
            }
            _ctr = DISItemChargesAndApply1.FindControl("cb223All");
            if (_ctr != null)
            {
                ((ASPxCheckBox)_ctr).Visible = false;
            }
       }
        
    }

    private DataTable GetMasterData()
    {
        string sSA_TYPE = "";
        string sM_TYPE = "";
        string sSTORE_NO = "";
        string sMSISDN = "";
        string sMM = "";
        string sMM_NAME = "";
        string sPROD_NO = "";
        string sPROD_NAME = "";
        string sARPB = "";
        
        //if (chkVOICE.Checked == true || chkDATA.Checked == true)
        //    sSA_TYPE += "1,";
        if (chkGA.Checked == true)
            sSA_TYPE += "2,";
        if (chkLOYALTY.Checked == true)
            sSA_TYPE += "3,";
        if (chk223.Checked == true)
            sSA_TYPE += "4,";
        if (chkMNP.Checked == true)
            sSA_TYPE += "5,";
        if(!string.IsNullOrEmpty(sSA_TYPE))
           sSA_TYPE = sSA_TYPE.Substring(0, sSA_TYPE.Length - 1);

        if(chkVOICE.Checked==true)
            sM_TYPE += "VOICE,";
        if(chkDATA.Checked==true)
            sM_TYPE += "DATA,";
        if (!(string.IsNullOrEmpty(sM_TYPE)))
            sM_TYPE = sM_TYPE.Substring(0, sM_TYPE.Length - 1);

        if(!(string.IsNullOrEmpty(popSTORENO.Text.Trim())))
            sSTORE_NO = popSTORENO.Text.Trim();

        if(!(string.IsNullOrEmpty(txtMSISDN.Text.Trim())))
            sMSISDN = txtMSISDN.Text.Trim();

        if (!(string.IsNullOrEmpty(txtARPB.Text.Trim())))
            sARPB = txtARPB.Text.Trim();

        if (!(string.IsNullOrEmpty(txtMM.Text.Trim())))
            sMM = txtMM.Text.Trim();

        if (!(string.IsNullOrEmpty(txtMMNAME.Text.Trim())))
            sMM_NAME = txtMMNAME.Text.Trim();

        if (!(string.IsNullOrEmpty(popPRODNO.Text.Trim())))
            sPROD_NO = popPRODNO.Text.Trim();

        if (!(string.IsNullOrEmpty(txtPRODNAME.Text.Trim())))
            sPROD_NAME = txtPRODNAME.Text.Trim();

        return new SAL13_Facade().Query_SP_DisCountQuery(sSA_TYPE, sM_TYPE, sSTORE_NO, sMSISDN, sMM, sMM_NAME, sPROD_NO, sPROD_NAME, sARPB);
    }

    private DataTable GetMasterData2()
    {
        return new SAL13_Facade().Query_DISCOUNT_MASTER();
    }

    private void BindDetailData2(string sDISCOUNT_MASTER_ID)
    {
        ClearDISItemChargesAndApply1();
        //費率及申辦類型
        DataTable dt = new SAL13_Facade().Query_RATE_PLAN_DISCOUNT(sDISCOUNT_MASTER_ID);
        if (dt.Rows.Count > 0) 
        {
            foreach (Control _ctr in DISItemChargesAndApply1.Controls)
            {
                string sTmp = StringUtil.CStr(_ctr.GetType());
                if (sTmp == "System.Web.UI.WebControls.CheckBoxList") {
                    CheckBoxList _cbo = (CheckBoxList)_ctr;
                    if (_cbo.Items.Count > 0) 
                    {
                        for(int i=0;i<_cbo.Items.Count;i++)
                        {
                            if (!(string.IsNullOrEmpty(_cbo.Items[i].Value))) 
                            {
                                DataRow[] drs = dt.Select("M_TYPE='" + _cbo.Items[i].Value  + "'");
                                if (drs.Length > 0) 
                                {
                                    _cbo.Items[i].Selected = (StringUtil.CStr(drs[0]["VALUE"]) == "Y") ? true : false;
                                    _cbo.Enabled = false;
                                }
                            }
                        }
                    }
                }
            }
        }
        DataTable dtResult = new DataTable();
        DataTable dtResult1 = new DataTable();
        DataTable dtResult2 = new DataTable();
        DataTable dtResult3 = new DataTable();
        DataTable dtResult4 = new DataTable();
        DataTable dtResult5 = new DataTable();
        DataTable dtResult6 = new DataTable();
        DataTable dtResult7 = new DataTable();

        dtResult = DesignatedGoods(sDISCOUNT_MASTER_ID);//指定商品
        dtResult1 = SpecifyStore(sDISCOUNT_MASTER_ID);//指定門市
        dtResult2 = PromotionCode(sDISCOUNT_MASTER_ID);//促銷代號
        dtResult3 = CostCenter(sDISCOUNT_MASTER_ID);//成本中心
        dtResult4 = newdata2(sDISCOUNT_MASTER_ID);//贈品設定
        dtResult5 = getGridViewDataCustomer1(sDISCOUNT_MASTER_ID);//加價購
        dtResult6 = getGridViewDataCustomer2(sDISCOUNT_MASTER_ID);//客戶對象
        dtResult7 = getGridViewDataCustomer3(sDISCOUNT_MASTER_ID);//名單

        //指定商品
        gvMaster.DataSource = dtResult;
        gvMaster.DataBind();
        //指定門市
        gvStore.DataSource = dtResult1;
        gvStore.DataBind();
        //促銷代號
        gvPromo.DataSource = dtResult2;
        gvPromo.DataBind();
        //成本中心
        gvCostCenter.DataSource = dtResult3;
        gvCostCenter.DataBind();
        //贈品設定
        gvGifDisc.DataSource = dtResult4;
        gvGifDisc.DataBind();
        //加價購
        gvAddIn.DataSource = dtResult5;
        gvAddIn.DataBind();
        //客戶對象_客戶等級
        gvARPB.DataSource = dtResult6;
        gvARPB.DataBind();
        //客戶對象_名單
        gvMsisdn.DataSource = dtResult7;
        gvMsisdn.DataBind();
    }

    private DataTable DesignatedGoods(string sDISCOUNT_MASTER_ID)
    {
        //指定商品
        return new SAL13_Facade().Query_PRODUCT_DISCOUNT(sDISCOUNT_MASTER_ID);
    }

    private DataTable SpecifyStore(string sDISCOUNT_MASTER_ID)
    {
        //指定門市
        return new SAL13_Facade().Query_STORE_DISCOUNT(sDISCOUNT_MASTER_ID);
    }

    private DataTable PromotionCode(string sDISCOUNT_MASTER_ID)
    {
        //促銷代號
        return new SAL13_Facade().Query_PROMOTION_DISCOUNT(sDISCOUNT_MASTER_ID);
    }
    
    private DataTable CostCenter(string sDISCOUNT_MASTER_ID)
    {
        //成本中心
        return new SAL13_Facade().Query_COST_CENTER_DISCOUNT(sDISCOUNT_MASTER_ID);
    }
    
    private DataTable newdata2(string sDISCOUNT_MASTER_ID)
    {
        //贈品設定
        return new SAL13_Facade().Query_GIFT_DISCOUNT(sDISCOUNT_MASTER_ID);
    }

    private DataTable getGridViewDataCustomer1(string sDISCOUNT_MASTER_ID)
    {
        //加價購
        return new SAL13_Facade().Query_ADD_IN_PROD_DISCOUNT(sDISCOUNT_MASTER_ID);
    }

    private DataTable getGridViewDataCustomer2(string sDISCOUNT_MASTER_ID)
    {
        //客戶對象
        return new SAL13_Facade().Query_CUST_LEVE_DISCOUNT(sDISCOUNT_MASTER_ID,"C");
    }
    
    private DataTable getGridViewDataCustomer3(string sDISCOUNT_MASTER_ID)
    {
        //名單
        return new SAL13_Facade().Query_CUST_LEVE_DISCOUNT(sDISCOUNT_MASTER_ID,"M");
    }

    private void ClearDISItemChargesAndApply1()
    {
        foreach (Control _ctr in DISItemChargesAndApply1.Controls)
        {
            string sTmp = StringUtil.CStr(_ctr.GetType());
            if (sTmp == "System.Web.UI.WebControls.CheckBoxList")
            {
                CheckBoxList _cbo = (CheckBoxList)_ctr;
                if (_cbo.Items.Count > 0)
                {
                    for (int i = 0; i < _cbo.Items.Count; i++)
                    {
                        if (!(string.IsNullOrEmpty(_cbo.Items[i].Value)))
                        {

                            _cbo.Items[i].Selected = false;
                            _cbo.Enabled = false;
                        }
                    }
                }
            }
        }

    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        //grid.DataSource = new SAL13_Facade().Query_SP_DisCountQuery("fsdfssfs", "", "fsdfssfs", "", "", "", "", "", "");
        //grid.DataBind();
        ClearDISItemChargesAndApply1();

        // 繫結主要的資料表
        //grid.DataSource = GetMasterData2();
        grid.DataSource = GetMasterData();
        grid.DataBind();
        grid.FocusedRowIndex = -1;
        grid.PageIndex = 0;
        BindDetailData2("GDGDFS");
        ASPxPageControl1.ActiveTabIndex = 0;
    }

    protected void rbCustomer_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (rbCustomer.SelectedValue == "CustLevel")
        {
            this.trgv1.Visible = true;
            this.trgv2.Visible = false;
        }
        else
        {
            this.trgv1.Visible = false;
            this.trgv2.Visible = true;
        }
    }

    protected void grid_OnFocusedRowChanged(object sender, EventArgs e)
    {
        if (grid.FocusedRowIndex < 0) { return; }
        string sDisMasID = StringUtil.CStr(grid.GetRowValues(grid.FocusedRowIndex,"DISCOUNT_MASTER_ID"));
        BindDetailData2(sDisMasID);
        DISCOUNT_MASTER_ID = sDisMasID;
        ASPxPageControl1.ActiveTabIndex = 0;
    }

    #region PageIndexChanged

    protected void grid_PageIndexChanged(object sender, EventArgs e)
    {
        ASPxGridView grid = sender as ASPxGridView;
        grid.DataSource = GetMasterData();
        grid.DataBind();
    }

    protected void gvMaster_PageIndexChanged(object sender, EventArgs e)
    {
        //指定商品
        gvMaster.DataSource = DesignatedGoods(DISCOUNT_MASTER_ID);//指定商品
        gvMaster.DataBind();
    }

    protected void gvStore_PageIndexChanged(object sender, EventArgs e)
    {
        //指定門市
        gvStore.DataSource = SpecifyStore(DISCOUNT_MASTER_ID);//指定門市
        gvStore.DataBind();
    }

    protected void gvPromo_PageIndexChanged(object sender, EventArgs e)
    {
        //促銷代號
        gvPromo.DataSource = PromotionCode(DISCOUNT_MASTER_ID);//促銷代號
        gvPromo.DataBind();
    }

    protected void gvARPB_PageIndexChanged(object sender, EventArgs e)
    {        
        //客戶對象_客戶等級
        gvARPB.DataSource = getGridViewDataCustomer2(DISCOUNT_MASTER_ID);//客戶對象
        gvARPB.DataBind();
    }

    protected void gvMsisdn_PageIndexChanged(object sender, EventArgs e)
    {
        //客戶對象_名單
        gvMsisdn.DataSource = getGridViewDataCustomer3(DISCOUNT_MASTER_ID);//名單
        gvMsisdn.DataBind();
    }

    protected void gvCostCenter_PageIndexChanged(object sender, EventArgs e)
    {
        //成本中心
        gvCostCenter.DataSource = CostCenter(DISCOUNT_MASTER_ID);//成本中心
        gvCostCenter.DataBind();
    }

    protected void gvGifDisc_PageIndexChanged(object sender, EventArgs e)
    {
        //贈品設定
        gvGifDisc.DataSource = newdata2(DISCOUNT_MASTER_ID);//贈品設定
        gvGifDisc.DataBind();
    }

    protected void gvAddIn_PageIndexChanged(object sender, EventArgs e)
    {
        //加價購
        gvAddIn.DataSource = getGridViewDataCustomer1(DISCOUNT_MASTER_ID);//加價購
        gvAddIn.DataBind();
    }

    #endregion

    //ajax 呼當前網頁的方式
    [System.Web.Services.WebMethod()]
    [System.Web.Script.Services.ScriptMethod()]
    static public string wmCheckStoreNO(string StoreNO)
    {
        string strInfo = "";
        if (!string.IsNullOrEmpty(StoreNO))
        {
            DataTable dt = new Store_Facade().Query_StoreInfo(StoreNO);
            if (dt.Rows.Count > 0)
            {
                strInfo = StringUtil.CStr(dt.Rows[0]["STORENAME"]);
            }
        }

        return strInfo;
    }

    //ajax 呼當前網頁的方式
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
                r = StringUtil.CStr(dt.Rows[0]["PRODNAME"]);
            }
            else
                r = "fail";
        }
        return r;
    }

    //ajax 呼當前網頁的方式
    [System.Web.Services.WebMethod()]
    [System.Web.Script.Services.ScriptMethod()]
    static public string getPromoteInfo(string PROMOTE_CODE)
    {
        string strPromoteName = "";
        if (!string.IsNullOrEmpty(PROMOTE_CODE))
        {
            DataTable dtPromote = OPT13_Facade.Query_MMData_ById(PROMOTE_CODE);
            if (dtPromote.Rows.Count > 0)
            {
                strPromoteName = StringUtil.CStr(dtPromote.Rows[0]["PROMO_NAME"]);
            }
        }

        return strPromoteName;
    }
}
