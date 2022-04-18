using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Advtek.Utility;
using DevExpress.Web.ASPxGridView;
using DevExpress.Web;
using DevExpress.Web.ASPxPager;
using DevExpress.Web.ASPxEditors;

using FET.POS.Model.Common;
using FET.POS.Model.DTO;
using FET.POS.Model.Facade;
using FET.POS.Model.Facade.FacadeImpl;

public partial class VSS_LEA_LEA01 : BasePage
{
    
    //UUID LEASE_M
    private static string gropStrUUID;

    protected void Page_Load(object sender, EventArgs e)
    {

        bindDdlValTxt(DropProduct, PRODUCT_PageHelper.GetProDTypeNo(true), "PRODTYPE_NO", "PRODTYPE_NAME");
        string LID = Request.QueryString["LEASE"] == null ? "" : StringUtil.CStr(Request.QueryString["LEASE"]).Trim();

        //LID 是 LEASE_M 設備租賃設定主檔
        this.ViewState["LID"] = LID;

        /*
            設定狀態有以下四種狀態:
             1. 未存檔：資料正在設定，尚未存檔
             2. 尚未生效：資料已存檔，但有效期間的起日還未到
             3. 有效：目前的時間還在有效期間的區間內
             4. 已過期：目前的時間 > 有效期間的訖日
         */

        setSataName(postbackDate_Start.Text, postbackDate_End.Text,true);
                
        //不需要初始化程式就跳走
        if (Page.IsPostBack || Page.IsCallback)
        {//!Page.IsPostBack && !Page.IsCallback
            return;
        }

        if (string.IsNullOrEmpty(LID)) //不是從查詢來的
        {
            #region 初始化程式
            initalPageFromNotData();
            #endregion

            //btnDelete.Enabled = true;
        }
        else //從查詢來的
        {
            try
            {
                //初始化由查詢來的資料。
                initalPageFromSearch();
                btnDelete.Enabled = true;
            }
            catch //(Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this, typeof(string), "資料錯誤", "alert('資料有誤，請聯絡資訊人員!');", true);
            }
        }

        // 租賃手機庫存
        bindMobileStock();

    }

    /// <summary>
    /// 不是從查詢來的 初始化程式
    /// </summary>
    private void initalPageFromNotData()
    {
        postbackDate_Start.MinDate = System.DateTime.Today.AddDays(1);
        postbackDate_Start.Text = System.DateTime.Today.AddDays(1).ToString("yyyy/MM/dd");
        postbackDate_End.MinDate = System.DateTime.Today.AddDays(1);

        lblModiDTM.Text = DateTime.Now.ToString("yyyy/MM/dd HH:mm");
        lblModiUser.Text = logMsg.OPERATOR + " " + new Employee_Facade().GetEmpName(this.logMsg.OPERATOR);     //更新人員

        bindMasterData();
        bindgvDiscountItem();
    }

    /// <summary>
    /// 設定狀態 bNotSave 未存檔
    /// </summary>
    /// <param name="strSDate"></param> 起始時間
    /// <param name="strEDate"></param> 終止時間
    /// <param name="bNotSave"></param>未存檔
    private void setSataName(string strSDate , string strEDate , bool bNotSave)
    {
        /*
            設定狀態有以下四種狀態:
             1. 未存檔：資料正在設定，尚未存檔
             2. 尚未生效：資料已存檔，但有效期間的起日還未到
             3. 有效：目前的時間還在有效期間的區間內
             4. 已過期：目前的時間 > 有效期間的訖日
           */
        if (bNotSave)
        {
            labState.Text = "未存檔";
            return;
        }
        DateTime sDate = new DateTime();
        DateTime eDate = new DateTime();

        if (string.IsNullOrEmpty(strSDate))
        { sDate = DateTime.Now.AddYears(-999); }
        else
        { sDate = DateTime.Parse(strSDate); }

        if (string.IsNullOrEmpty(strEDate))
        { eDate = DateTime.Now.AddYears(999); }
        else
        { eDate = DateTime.Parse(strEDate); }

        string strState = ""; //狀態
        if (sDate <= DateTime.Now &&
            DateTime.Now <= eDate)
        {
            strState = "有效";
            validGeneralEnable(false);
            postbackDate_End.Enabled = true;//E_DATE
        }
        else if (eDate <= DateTime.Now)
        {
            strState = "已過期";
            validGeneralEnable(false);
        }
        else if (DateTime.Now <= sDate)
        {
            strState = "尚未生效";
            validGeneralEnable(true);
        }
        labState.Text = strState;
    }

    //初始化由查詢來的資料。
    private void initalPageFromSearch()
    {
        //LID 是 LEASE_M 設備租賃設定主檔
        string LID = this.ViewState["LID"] as string;

        DataTable dt = new LEA01_Facade().GetLeaseData(LID);

        #region //上面欄位的資料顯示
        if (dt.Rows.Count > 0)  //資料表裡資料，已經盤點過了，按下【確定】後直接帶出資料
        {
            DataRow dr = dt.Rows[0];
            // DropProduct ProductsPopup2 labState
            txtSupplierNo.Text = new LEA01_Facade().getSuppNO(StringUtil.CStr(dr["SUPP_ID"]));//外部廠商代碼
            RadioButtonList1.SelectedIndex = int.Parse(StringUtil.CStr(dr["LEASE_TYPE"])) - 1;
            DropProduct.Text = StringUtil.CStr(dr["PRODTYPENO"]);//商品類別
            //ProductsPopup2.Text = StringUtil.CStr(dr["MODI_USER"]);//產品名稱
            txtRentNO.Text = StringUtil.CStr(dr["RENT_PRODNO"]);//租金料號
            txtDailyRent.Text = StringUtil.CStr(dr["DAILY_RENT_PRICE"]);//日租金額
            txtRentDepositNO.Text = StringUtil.CStr(dr["EARNEST_PRODNO"]);//保證金料號
            txtRentDepositNumber.Text = StringUtil.CStr(dr["EARNEST_MONEY"]);//保證金
            txtCompensationNO.Text = StringUtil.CStr(dr["INDEMNITY_PRODNO"]);//賠償金料號
            if (!string.IsNullOrEmpty(StringUtil.CStr(dr["S_DATE"])))
                postbackDate_Start.Text = DateTime.Parse(StringUtil.CStr(dr["S_DATE"])).ToString("yyyy/MM/dd");//S_DATE
            if (!string.IsNullOrEmpty(StringUtil.CStr(dr["E_DATE"])))
                postbackDate_End.Text = DateTime.Parse(StringUtil.CStr(dr["E_DATE"])).ToString("yyyy/MM/dd");//E_DATE
            memo.Text = StringUtil.CStr(dr["REMARK"]);//備註
            /*
             設定狀態有以下四種狀態:
              1. 未存檔：資料正在設定，尚未存檔
              2. 尚未生效：資料已存檔，但有效期間的起日還未到
              3. 有效：目前的時間還在有效期間的區間內
              4. 已過期：目前的時間 > 有效期間的訖日
            */
            setSataName(StringUtil.CStr(dr["S_DATE"]) , StringUtil.CStr(dr["E_DATE"]),false );

            lblModiUser.Text = StringUtil.CStr(dr["MODI_USER"])+ " "
                    + new Employee_Facade().GetEmpName(StringUtil.CStr(dr["MODI_USER"]));//更新人員
            this.lblModiDTM.Text = StringUtil.CStr(dr["MODI_DTM"]);
        }
        #endregion

        #region //Grid view 賠償項目 折扣項目
        LEA01_Facade fac = new LEA01_Facade();

        gvMaster.DataSource = fac.GetRentIndemnifyItems(LID);
        gvMaster.DataBind();

        gvDiscountItem.DataSource = fac.GetRentDiscountItems(LID);
        gvDiscountItem.DataBind();
        #endregion

    }

    private void bindDdlValTxt(ASPxComboBox AspCB, object dataSrc, string valCol, string txtCol)
    {
        AspCB.DataSource = dataSrc;
        AspCB.ValueField = valCol;
        AspCB.TextField = txtCol;
        AspCB.DataBind();
    }

    private void bindMasterData()
    {
        DataTable dtResult = new DataTable();
        dtResult = getDtproduct();
        Session["gvMaster"] = dtResult;
        gvMaster.DataSource = dtResult;
        gvMaster.DataBind();
    }

    private void bindgvDiscountItem()
    {
        DataTable dtResult = new DataTable();
        dtResult = getDtDiscount();
        Session["gvDiscountItem"] = dtResult;
        gvDiscountItem.DataSource = dtResult;
        gvDiscountItem.DataBind();
    }

    /// <summary>
    /// 租賃手機庫存
    /// </summary>
    private void bindMobileStock()
    {
        DataTable dtResult = new DataTable();
        dtResult = getDtStock();
        gvMobileStock.DataSource = new LEA01_Facade().getDtStock();
        gvMobileStock.DataBind();
    }

    private DataTable getDtproduct()
    {
        DataTable dtResult = new DataTable();
        dtResult.Columns.Add("RENT_INDEMNIFY_ITEMS", typeof(string));
        dtResult.Columns.Add("IND_ITEM_NAME", typeof(string));
        dtResult.Columns.Add("IND_UNIT_PRICE", typeof(string));
        return dtResult;
    }

    private DataTable getDtDiscount()
    {
        DataTable dtResult = new DataTable();
        dtResult.Columns.Add("RENT_DISCOUNT_ID", typeof(string));
        dtResult.Columns.Add("PRODNO", typeof(string));
        dtResult.Columns.Add("PRODNAME", typeof(string));
        dtResult.Columns.Add("DISCOUNT_AMT", typeof(string));
        dtResult.Columns.Add("DISCOUNT_RATE", typeof(string));
        dtResult.Columns.Add("COST_CENTER_NO", typeof(string));
        dtResult.Columns.Add("ACCOUNT_CODE", typeof(string));
        return dtResult;
    }

    private DataTable getDtStock()
    {
        DataTable dtResult = new DataTable();
        dtResult.Columns.Add("STORE_NO", typeof(string));
        dtResult.Columns.Add("STORE_NAME", typeof(string));
        dtResult.Columns.Add("SERIAL_NO", typeof(string));

        return dtResult;
    }

    #region Button 觸發的事件

    protected void btnSave_Click(object sender, EventArgs e)
    {
        LEA01_Facade LEA01_F = new LEA01_Facade();
        int intResult = 0;
        LEA01_LEASE_M.LEASE_MDataTable dtLEASE = null;
        LEA01_LEASE_M.RENT_INDEMNIFY_ITEMSDataTable dtInd = null;
        LEA01_LEASE_M.RENT_DISCOUNT_ITEMSDataTable dtDiscount = null;
        try
        {
            string strAltrMemo = "";//記錄異動過的欄位

            if (string.IsNullOrEmpty(StringUtil.CStr(this.ViewState["LID"]))) //不是從查詢來的
            {
                strAltrMemo = "類別\r\n商品類別\r\n商品名稱\r\n外部廠商代碼\r\n外部廠商名稱\r\n租金料號\r\n";
                strAltrMemo += "日租金額\r\n保證金料號\r\n保證金\r\n賠償金料號\r\n備註\r\n賠償項目\r\n";
                strAltrMemo += "折扣項目";
            }
            else { strAltrMemo = findAlterColumn(); }

            dtLEASE = insertLeaseM();
            dtInd = InsertIndemnify();
            dtDiscount = InsertDiscount();
            intResult = LEA01_F.SaveOut(dtLEASE, dtInd, dtDiscount);

            string strEmpName = new Employee_Facade().GetEmpName(logMsg.OPERATOR);
            //更新日期,更新人員 
            lblModiDTM.Text = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");
            lblModiUser.Text = logMsg.OPERATOR + " " + strEmpName;
            libEmpNO.Text = logMsg.OPERATOR;
            libEmpName.Text = strEmpName;
            libTime.Text = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");
            /*
               設定狀態有以下四種狀態:
                1. 未存檔：資料正在設定，尚未存檔
                2. 尚未生效：資料已存檔，但有效期間的起日還未到
                3. 有效：目前的時間還在有效期間的區間內
                4. 已過期：目前的時間 > 有效期間的訖日
           */
            setSataName(postbackDate_Start.Text, postbackDate_End.Text, false);

            Panel2.Visible = true;

            libMemo.Text = strAltrMemo;


            //btnSave.Enabled = btnCancel.Enabled = btnDelete.Enabled = btnImport.Enabled = false;
            ScriptManager.RegisterClientScriptBlock(this, typeof(string), "儲存", "alert('存檔完成!');", true);
        }
        catch //(Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(this, typeof(string), "儲存", "alert('存檔失敗!');", true);
        }

    }

    protected void btnDiscounDelete_Click(object sender, EventArgs e)
    {
        //控制所有Grid 的Delete 方法。
        subControlGridDelete( gvDiscountItem, "gvDiscountItem", "RENT_DISCOUNT_ID");
    }

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        LEA01_Facade fd = new LEA01_Facade();
        CheckData ck = new CheckData();

        string supNo = this.ViewState["LID"] as string;
        string strMessage = "";

        if (string.IsNullOrEmpty(supNo))
        { return; }

        try
        {
            fd.DeleteGroup(supNo);
            strMessage = "刪除成功";
            ck.State.IsSuccess = true;
        }
        catch //(Exception ex)
        {
            strMessage = " 刪除失敗，請與相關人員聯絡";
            ck.State.IsSuccess = false;
        }

        gvMaster.CancelEdit();
        gvDiscountItem.CancelEdit();

        //刪除成功初始化資料。
        if (ck.State.IsSuccess)
        {
            #region 清除為初始值
            RadioButtonList1.SelectedIndex = 1;
            txtSupplierNo.Text = "";
            DropProduct.Text = "";//商品類別
            ProductsPopup2.Text = "";//產品名稱
            txtRentNO.Text = "";//租金料號
            txtDailyRent.Text = "";//日租金額
            txtRentDepositNO.Text = "";//保證金料號
            txtRentDepositNumber.Text = "";//保證金
            txtCompensationNO.Text = "";//賠償金料號
            postbackDate_Start.Text = "";//S_DATE
            postbackDate_End.Text = "";//E_DATE
            memo.Text = "";
            #endregion

            initalPageFromNotData();

            bindMasterData();
            bindgvDiscountItem();

            validGeneralEnable(true);
        }

        ScriptManager.RegisterClientScriptBlock(this, GetType(), "AlertMessage", "alert('" + strMessage + "');", true);

    }

    /// <summary>
    /// 查找異動欄位。
    /// </summary>
    /// <returns></returns>
    private string  findAlterColumn()
    {
        //LID 是 LEASE_M 設備租賃設定主檔
        string LID = this.ViewState["LID"] as string;

        DataTable dt = new LEA01_Facade().GetLeaseData(LID);
        string retVal = "";
        

        if (dt.Rows.Count > 0)  //資料表裡資料，已經盤點過了，按下【確定】後直接帶出資料
        {
            DataRow dr = dt.Rows[0];
            // DropProduct ProductsPopup2 labState
            if (txtSupplierNo.Text != new LEA01_Facade().getSuppNO(StringUtil.CStr(dr["SUPP_ID"])))
                retVal += "外部廠商代碼\r\n";

            if (RadioButtonList1.SelectedIndex != int.Parse(StringUtil.CStr(dr["LEASE_TYPE"])) - 1)
                retVal += "類別\r\n";

            if (DropProduct.Text != StringUtil.CStr(dr["PRODTYPENO"]))//商品類別
                retVal += "類別\r\n";

            //ProductsPopup2.Text = StringUtil.CStr(dr["MODI_USER"]);//產品名稱
            if (txtRentNO.Text != StringUtil.CStr(dr["RENT_PRODNO"]))
                retVal += "租金料號\r\n";

            if(txtDailyRent.Text != StringUtil.CStr(dr["DAILY_RENT_PRICE"]))
                retVal += "日租金額\r\n";
            if(txtRentDepositNO.Text != StringUtil.CStr(dr["EARNEST_PRODNO"]))
                retVal += "保證金料號\r\n";
            if(txtRentDepositNumber.Text != StringUtil.CStr(dr["EARNEST_MONEY"]))
                retVal += "保證金\r\n";

            if(txtCompensationNO.Text != StringUtil.CStr(dr["INDEMNITY_PRODNO"]))
                retVal += "賠償金料號\r\n";

            if (postbackDate_Start.Text != DateTime.Parse(StringUtil.CStr(dr["S_DATE"])).ToString("yyyy/MM/dd"))
                retVal += "有效期間(起)\r\n";

            if (postbackDate_End.Text != DateTime.Parse(StringUtil.CStr(dr["E_DATE"])).ToString("yyyy/MM/dd"))
                retVal += "有效期間(訖)\r\n";

            if(memo.Text != StringUtil.CStr(dr["REMARK"]))
                retVal += "備註\r\n";
        }
        return retVal;

    }

    #endregion

    #region gvMaster 觸發的事件

    protected void btngvMasterDelete_Click(object sender, EventArgs e)
    {
        //控制所有Grid 的Delete 方法。
        subControlGridDelete(gvMaster, "gvMaster", "RENT_INDEMNIFY_ITEMS");
    }

    protected void btngvMasterAdd_Click(object sender, EventArgs e)
    {
        gvMaster.AddNewRow();
    }

    protected void gvMaster_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {

    }

    protected void gvMaster_CancelRowEditing(object sender, DevExpress.Web.Data.ASPxStartRowEditingEventArgs e)
    {
        //btnSave.Enabled = false;
        //btnCancel.Enabled = false;
        //btnDelete.Enabled = false;
    }

    protected void gvMaster_CommandButtonInitialize(object sender, ASPxGridViewCommandButtonEventArgs e)
    {
        if (e.VisibleIndex > -1)
        {
            if (e.ButtonType == ColumnCommandButtonType.Edit || e.ButtonType == ColumnCommandButtonType.SelectCheckbox)
            {

            }
        }
    }

    protected void gvMaster_InitNewRow(object sender, DevExpress.Web.Data.ASPxDataInitNewRowEventArgs e)
    {
        //btnSave.Enabled = false;
        //btnCancel.Enabled = false;
        //btnDelete.Enabled = false;
    }

    protected void gvMaster_PageIndexChanged(object sender, EventArgs e)
    {
        ASPxGridView grid = sender as ASPxGridView;
        grid.DataSource = Session["gvMaster"];
        grid.DataBind();
    }

    protected void gvMaster_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
    {
        DataTable dtSYS;
        if (Session["gvMaster"] == null || Session["gvMaster"] == "")
        {
            dtSYS = getDtproduct(); ;
        }
        else
        {
            dtSYS = Session["gvMaster"] as DataTable;
        }

        DataRow NewRow = dtSYS.NewRow();
        NewRow["RENT_INDEMNIFY_ITEMS"] = GuidNo.getUUID();
        NewRow["IND_ITEM_NAME"] = StringUtil.CStr(e.NewValues["IND_ITEM_NAME"]);
        NewRow["IND_UNIT_PRICE"] = StringUtil.CStr(e.NewValues["IND_UNIT_PRICE"]);
        dtSYS.Rows.Add(NewRow);

        ((ASPxGridView)sender).CancelEdit();
        e.Cancel = true;
        Session["gvMaster"] = dtSYS;
        gvMaster.DataSource = dtSYS;
        gvMaster.DataBind();

        btnSave.Enabled = true;
        btnCancel.Enabled = true;
        btnDelete.Enabled = true;
    }

    protected void gvMaster_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
    {
        DataTable dtSYS;
        if (Session["gvMaster"] == null || Session["gvMaster"] == "")
        {
            dtSYS = new DataTable();
        }
        else
        {
            dtSYS = Session["gvMaster"] as DataTable;
        }
        string sNewName = StringUtil.CStr(e.NewValues["IND_ITEM_NAME"]);
        string sNewLEASE_ID = StringUtil.CStr(e.NewValues["IND_UNIT_PRICE"]);

        string sOldName = StringUtil.CStr(e.OldValues["IND_ITEM_NAME"]);
        string sOldLEASE_ID = StringUtil.CStr(e.OldValues["IND_UNIT_PRICE"]);
        for (int i = 0; i < dtSYS.Rows.Count; i++)
        {
            DataRow dr = dtSYS.Rows[i];
            if (StringUtil.CStr(dr["IND_ITEM_NAME"]).CompareTo(sOldName) == 0)
            {
                dr["IND_ITEM_NAME"] = sNewName.Trim();
                dr["IND_UNIT_PRICE"] = sNewLEASE_ID.Trim();
                dtSYS.AcceptChanges();
                break;
            }
        }

        gvMaster.CancelEdit();
        e.Cancel = true;
        Session["gvMaster"] = dtSYS;
        gvMaster.DataSource = dtSYS;
        gvMaster.DataBind();

        btnSave.Enabled = true;
        btnCancel.Enabled = true;
        btnDelete.Enabled = true;
    }

    protected void gvMaster_RowValidating(object sender, DevExpress.Web.Data.ASPxDataValidationEventArgs e)
    {
        if (!gvMaster.IsEditing)
            return;

        string strItemName = StringUtil.CStr(e.NewValues["IND_ITEM_NAME"]);
        string strPrice = StringUtil.CStr(e.NewValues["IND_UNIT_PRICE"]);

        CheckMethod_PageHelper Ck = new CheckMethod_PageHelper();//check Method

        if (string.IsNullOrEmpty(strItemName))
        {
            e.RowError = "輸入賠償項目";
            return;
        }

        if (string.IsNullOrEmpty(strPrice))
        {
            e.RowError = "輸入金額";
            return;
        }

        if (!Ck.IsNumber(strPrice))
        {
            e.RowError = "金額欄位有誤";
            return;
        }

        CheckData retVal = new CheckData();
        retVal.State.IsSuccess = true;
        CheckData pChDate = new CheckData();

        pChDate.SessionName = gvMaster.ID;
        pChDate.PkData = strItemName;
        pChDate.PkName = "IND_ITEM_NAME";
        pChDate.IsNewRowEditing = gvMaster.IsNewRowEditing; //是否update 驗證
        pChDate.IndItemName = strItemName; // 賠償項目

        CheckData inCk = CheckRedundant(pChDate);//Check 的羅輯 [ Session]  資料不可重覆
        if (!inCk.State.IsSuccess)
        {//資料重覆
            retVal.State.IsSuccess = false;
            e.RowError = inCk.State.ErrorMessage;
        }

    }

    #endregion

    #region gvDiscountItem 觸發的事件

    protected void gvDiscountItem_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
    {
        DataTable dtSYS;
        if (Session["gvDiscountItem"] == null || Session["gvDiscountItem"] == "")
        {
            dtSYS = getDtDiscount();
        }
        else
        {
            dtSYS = Session["gvDiscountItem"] as DataTable;
        }

        DataRow NewRow = dtSYS.NewRow();
        NewRow["RENT_DISCOUNT_ID"] = GuidNo.getUUID();
        NewRow["PRODNO"] = StringUtil.CStr(e.NewValues["PRODNO"]);
        NewRow["PRODNAME"] = StringUtil.CStr(e.NewValues["PRODNAME"]);
        NewRow["DISCOUNT_AMT"] = StringUtil.CStr(e.NewValues["DISCOUNT_AMT"]);
        NewRow["DISCOUNT_RATE"] = StringUtil.CStr(e.NewValues["DISCOUNT_RATE"]);
        NewRow["COST_CENTER_NO"] = StringUtil.CStr(e.NewValues["COST_CENTER_NO"]);
        NewRow["ACCOUNT_CODE"] = StringUtil.CStr(e.NewValues["ACCOUNT_CODE"]);
        dtSYS.Rows.Add(NewRow);

        ((ASPxGridView)sender).CancelEdit();
        e.Cancel = true;
        Session["gvDiscountItem"] = dtSYS;
        gvDiscountItem.DataSource = dtSYS;
        gvDiscountItem.DataBind();
    }

    protected void gvDiscountItem_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
    {
        DataTable dtSYS;
        if (Session["gvDiscountItem"] == null || Session["gvDiscountItem"] == "")
        {
            dtSYS = new DataTable();
        }
        else
        {
            dtSYS = Session["gvDiscountItem"] as DataTable;
        }
        string sNewID = StringUtil.CStr(e.NewValues["RENT_DISCOUNT_ID"]);
        string sNewPROD = StringUtil.CStr(e.NewValues["PRODNO"]);
        string sNewPDNAME = StringUtil.CStr(e.NewValues["PRODNAME"]);
        string sNewAMT = StringUtil.CStr(e.NewValues["DISCOUNT_AMT"]);
        string sNewRATE = StringUtil.CStr(e.NewValues["DISCOUNT_RATE"]);
        string sNewCENTER = StringUtil.CStr(e.NewValues["COST_CENTER_NO"]);
        string sNewCODE = StringUtil.CStr(e.NewValues["ACCOUNT_CODE"]);

        string sOldID = StringUtil.CStr(e.OldValues["RENT_DISCOUNT_ID"]);

        for (int i = 0; i < dtSYS.Rows.Count; i++)
        {
            DataRow dr = dtSYS.Rows[i];
            if (StringUtil.CStr(dr["RENT_DISCOUNT_ID"]).CompareTo(sOldID) == 0)
            {
                dr["PRODNO"] = sNewPROD.Trim();
                dr["PRODNAME"] = sNewPDNAME.Trim();
                dr["DISCOUNT_AMT"] = sNewAMT.Trim();
                dr["DISCOUNT_RATE"] = sNewRATE.Trim();
                dr["COST_CENTER_NO"] = sNewCENTER.Trim();
                dr["ACCOUNT_CODE"] = sNewCODE.Trim();
                dtSYS.AcceptChanges();
                break;
            }
        }

        gvDiscountItem.CancelEdit();
        e.Cancel = true;
        Session["gvDiscountItem"] = dtSYS;
        gvDiscountItem.DataSource = dtSYS;
        gvDiscountItem.DataBind();
    }

    protected void gvDiscountItem_CancelRowEditing(object sender, DevExpress.Web.Data.ASPxStartRowEditingEventArgs e)
    {
        btnSave.Enabled = true;
        //btnClear.Enabled = true;
        btnDelete.Enabled = true;
    }

    protected void gvDiscountItem_PageIndexChanged(object sender, EventArgs e)
    {
        ASPxGridView grid = sender as ASPxGridView;
        grid.DataSource = Session["gvDiscountItem"];
        grid.DataBind();
    }

    protected void gvDiscountItem_CommandButtonInitialize(object sender, ASPxGridViewCommandButtonEventArgs e)
    {
        if (e.VisibleIndex > -1)
        {
            if (e.ButtonType == ColumnCommandButtonType.Edit || e.ButtonType == ColumnCommandButtonType.SelectCheckbox)
            {
            }
        }
    }

    protected void gvDiscountItem_RowValidating(object sender, DevExpress.Web.Data.ASPxDataValidationEventArgs e)
    {
        if (!gvDiscountItem.IsEditing)
            return;

        string strProdNo = StringUtil.CStr(e.NewValues["PRODNO"]);
        string strAmt = StringUtil.CStr(e.NewValues["DISCOUNT_AMT"]);
        string strRate = StringUtil.CStr(e.NewValues["DISCOUNT_RATE"]);
        string strCenterNo = StringUtil.CStr(e.NewValues["COST_CENTER_NO"]);
        string strAccCode = StringUtil.CStr(e.NewValues["ACCOUNT_CODE"]);

        CheckMethod_PageHelper Ck = new CheckMethod_PageHelper();//check Method

        if (string.IsNullOrEmpty( strProdNo))
        {
            e.RowError = "【折扣料號】不允許空值，請重新輸入!!";
            return;
        }

        if (string.IsNullOrEmpty(strAmt) &&
            string.IsNullOrEmpty(strRate)
            )
        {
            e.RowError = "【折扣金額】與【折扣比率】，請設定其中之一!!";
            return;
        }

        if ( !string.IsNullOrEmpty(strAmt) &&
              !string.IsNullOrEmpty(strRate)
              
           )
        {
            e.RowError = "【折扣金額】與【折扣比率】，兩個欄位不可同時設定!!";
            return;
        }

        if ( !string.IsNullOrEmpty(strAmt) &&
             !Ck.IsNumber(strAmt)
           )
        {
            e.RowError = "折扣金額欄位有誤";
            return;
        }

        if ( !string.IsNullOrEmpty(strRate) &&
             !Ck.IsNumber(strRate)
           )
        {
            e.RowError = "折扣比率欄位有誤";
            return;
        }

        if (string.IsNullOrEmpty(strCenterNo))
        {
            e.RowError = "【成本中心】不允許空值，請重新輸入!!";
            return;
        }

        if (string.IsNullOrEmpty(strAccCode))
        {
            e.RowError = "【會計科目】不允許空值，請重新輸入!!";
            return;
        }

        CheckData retVal = new CheckData();
        retVal.State.IsSuccess = true;
        CheckData pChDate = new CheckData();

        pChDate.SessionName = gvDiscountItem.ID;
        pChDate.PkData = strProdNo;
        pChDate.PkName = "PRODNO";
        pChDate.IsNewRowEditing = gvDiscountItem.IsNewRowEditing; //是否update 驗證
        pChDate.ProductNO = strProdNo; // 折扣料號

        CheckData inCk = CheckRedundant(pChDate);//Check 的羅輯 [ Session]  資料不可重覆
        if (!inCk.State.IsSuccess)
        {//資料重覆
            retVal.State.IsSuccess = false;
            e.RowError = inCk.State.ErrorMessage;
        }
    }

    protected void btngvDiscountItemAdd_Click(object sender, EventArgs e)
    {
        gvDiscountItem.AddNewRow();
    }

    #endregion

    #region gvMobileStock 觸發的事件

    protected void gvMobileStock_PageIndexChanged(object sender, EventArgs e)
    {
        ASPxGridView grid = sender as ASPxGridView;
        grid.DataSource = new LEA01_Facade().getDtStock();
        grid.DataBind();
    }

    #endregion

    #region doInsert 區
    private LEA01_LEASE_M.LEASE_MDataTable insertLeaseM()
    {
        LEA01_LEASE_M DtoLease = new LEA01_LEASE_M();
        LEA01_LEASE_M.LEASE_MDataTable dtLease = null;
        LEA01_LEASE_M.LEASE_MRow drLease;

        dtLease = DtoLease.Tables["LEASE_M"] as LEA01_LEASE_M.LEASE_MDataTable;
        drLease = dtLease.NewLEASE_MRow();

        //單PK strUUID
        string strUUID = GuidNo.getUUID();
        gropStrUUID = strUUID; //存UUID

        //異動的欄位
        drLease["LEASE_ID"] = strUUID;
        //if (DropProduct.Value != null)
        //    drLease["PRODTYPENO"] = StringUtil.CStr(DropProduct.Value).Trim();

        drLease["LEASE_TYPE"] = RadioButtonList1.SelectedItem.Value;
        if (!string.IsNullOrEmpty(txtSupplierNo.Text))
            drLease["SUPP_ID"] = new LEA01_Facade().getSuppID(StringUtil.CStr(txtSupplierNo.Text).Trim());
        if (!string.IsNullOrEmpty(txtRentNO.Text))
            drLease["RENT_PRODNO"] = StringUtil.CStr(txtRentNO.Text).Trim();

        string strDailyRent = "0";
        if (!string.IsNullOrEmpty(txtDailyRent.Text))
            strDailyRent = StringUtil.CStr(txtDailyRent.Text).Trim();
        drLease["DAILY_RENT_PRICE"] = strDailyRent;

        if (!string.IsNullOrEmpty(txtRentDepositNO.Text))
            drLease["EARNEST_PRODNO"] = txtRentDepositNO.Text.Trim();
        if (!string.IsNullOrEmpty(txtRentDepositNumber.Text))
            drLease["EARNEST_MONEY"] = txtRentDepositNumber.Text.Trim();
        if (!string.IsNullOrEmpty(txtCompensationNO.Text))
            drLease["INDEMNITY_PRODNO"] = txtCompensationNO.Text.Trim();
        if (!string.IsNullOrEmpty(postbackDate_Start.Text))
            drLease["S_DATE"] = postbackDate_Start.Text.Trim();
        if (!string.IsNullOrEmpty(postbackDate_End.Text))
            drLease["E_DATE"] = postbackDate_End.Text.Trim();
        if (!string.IsNullOrEmpty(memo.Text))
            drLease["REMARK"] = memo.Text.Trim();

        drLease["MODI_USER"] = logMsg.MODI_USER;
        drLease["MODI_DTM"] = Convert.ToDateTime(System.DateTime.Now);
        if (lblModiUser.Value == null)
        {
            lblModiUser.Value = logMsg.MODI_USER;
        }
        drLease["CREATE_USER"] = lblModiUser.Value;
        lblModiDTM.Value = StringUtil.CStr(Convert.ToDateTime(System.DateTime.Now));
        drLease["CREATE_DTM"] = Convert.ToDateTime(lblModiDTM.Value);
        dtLease.Rows.Add(drLease);

        return dtLease;
    }
    //賠償項目
    private LEA01_LEASE_M.RENT_INDEMNIFY_ITEMSDataTable InsertIndemnify()
    {
        LEA01_LEASE_M.RENT_INDEMNIFY_ITEMSDataTable dtIndy = null;
        DataTable dtProd = null;
        dtProd = (DataTable)Session["gvMaster"];

        dtIndy = new LEA01_LEASE_M.RENT_INDEMNIFY_ITEMSDataTable();
        LEA01_LEASE_M.RENT_INDEMNIFY_ITEMSRow drIndy;

        if (dtProd.Rows.Count > 0)
        {
            foreach (DataRow dr in dtProd.Rows)
            {
                drIndy = dtIndy.NewRENT_INDEMNIFY_ITEMSRow();
                drIndy["RENT_INDEMNIFY_ITEMS"] = StringUtil.CStr(dr["RENT_INDEMNIFY_ITEMS"]);
                drIndy["LEASE_ID"] = gropStrUUID;
                drIndy["IND_ITEM_CODE"] = "1";
                drIndy["SEQNO"] = 1;

                drIndy["IND_ITEM_NAME"] = StringUtil.CStr(dr["IND_ITEM_NAME"]);
                drIndy["IND_UNIT_PRICE"] = StringUtil.CStr(dr["IND_UNIT_PRICE"]);
                drIndy["MODI_USER"] = logMsg.MODI_USER;
                drIndy["MODI_DTM"] = Convert.ToDateTime(System.DateTime.Now);
                drIndy["CREATE_USER"] = drIndy["MODI_USER"];
                drIndy["CREATE_DTM"] = drIndy["MODI_DTM"];
                dtIndy.Rows.Add(drIndy);
            }
        }
        return dtIndy;
    }
    //折扣項目
    private LEA01_LEASE_M.RENT_DISCOUNT_ITEMSDataTable InsertDiscount()
    {
        LEA01_LEASE_M.RENT_DISCOUNT_ITEMSDataTable dtIndy = null;
        DataTable dtProd = null;
        dtProd = (DataTable)Session["gvDiscountItem"];

        dtIndy = new LEA01_LEASE_M.RENT_DISCOUNT_ITEMSDataTable();
        LEA01_LEASE_M.RENT_DISCOUNT_ITEMSRow drIndy;

        if (dtProd.Rows.Count > 0)
        {
            foreach (DataRow dr in dtProd.Rows)
            {
                drIndy = dtIndy.NewRENT_DISCOUNT_ITEMSRow();
                drIndy["RENT_DISCOUNT_ID"] = StringUtil.CStr(dr["RENT_DISCOUNT_ID"]);
                drIndy["LEASE_ID"] = gropStrUUID;
                drIndy["SEQNO"] = 1;

                drIndy["PRODNO"] = StringUtil.CStr(dr["PRODNO"]);
                drIndy["DISCOUNT_AMT"] = StringUtil.CStr(dr["DISCOUNT_AMT"]) == "" ? "0" : StringUtil.CStr(dr["DISCOUNT_AMT"]);
                drIndy["DISCOUNT_RATE"] = StringUtil.CStr(dr["DISCOUNT_RATE"]) == "" ? "0" : StringUtil.CStr(dr["DISCOUNT_RATE"]);
                drIndy["COST_CENTER_NO"] = StringUtil.CStr(dr["COST_CENTER_NO"]);
                drIndy["ACCOUNT_CODE"] = StringUtil.CStr(dr["ACCOUNT_CODE"]);
                drIndy["MODI_USER"] = logMsg.MODI_USER;
                drIndy["MODI_DTM"] = Convert.ToDateTime(System.DateTime.Now);
                drIndy["CREATE_USER"] = drIndy["MODI_USER"];
                drIndy["CREATE_DTM"] = drIndy["MODI_DTM"];
                dtIndy.Rows.Add(drIndy);
            }
        }
        return dtIndy;
    }
    #endregion

    #region //ajax 呼當前網頁的方式

    //ajax 呼當前網頁的方式
    [System.Web.Services.WebMethod()]
    [System.Web.Script.Services.ScriptMethod()]
    static public string getPRODINFO(string PRODNO)
    {

        DataTable dt = new Product_Facade().Query_DiscountProduct(PRODNO);
        string r = "";
        if (dt.Rows.Count > 0)
        {
            r = StringUtil.CStr(dt.Rows[0]["PRODNAME"]);
        }
        return r;
    }

    //ajax 呼當前網頁的方式
    [System.Web.Services.WebMethod()]
    [System.Web.Script.Services.ScriptMethod()]
    static public string getSupplierId(string strSupplierNo)
    {
        //廠商ID
        string SuppId = new Supplier_Facade().GetSuppId(strSupplierNo);
        return SuppId;
    }

    //ajax 取得Product 的會計科目
    [System.Web.Services.WebMethod()]
    [System.Web.Script.Services.ScriptMethod()]
    static public string getAccountCode(string productNO)
    {
        string strInfo = "";
        DataTable dt = new CON02_Facade().GetProdDataByKey(productNO);
        if (dt.Rows.Count > 0)
        {
            strInfo = StringUtil.CStr(dt.Rows[0]["ACCOUNTCODE"]);
        }
        else
        {
            strInfo = "fail";
        }

        return strInfo;
    }

    #endregion

    /// <summary>
    /// Session 資料不可重覆
    /// </summary>
    /// <param name="CheckData"></param>
    /// <returns></returns>
    public CheckData CheckRedundant(CheckData CheckData)
    {
        DataTable dtSYS; //要check Data 
        CheckData retVal = new CheckData();
        DataRow[] drING;
        retVal.State.IsSuccess = true;
        if (Session[CheckData.SessionName] == null)
        {
            retVal.State.IsSuccess = false;
            retVal.State.ErrorMessage = "找不到資料!";
            return retVal;
        }
        else
        {
            dtSYS = Session[CheckData.SessionName] as DataTable;
        }

        if (CheckData.IsNewRowEditing)
        {
            drING = dtSYS.Select();
        }
        else
        {//此為編輯資料，排除自已Session 的資料

            drING = dtSYS.Select(" " + CheckData.PkName + " <> '" + CheckData.PkData + "'");
        }

        for (int i = 0; i < drING.Length; i++)
        {
            DataRow dr = drING[i];
            if (retVal.State.IsSuccess == true)
            {
                //如果沒有錯誤就check

                if (!string.IsNullOrEmpty(CheckData.IndItemName))
                {
                    if (StringUtil.CStr(dr["IND_ITEM_NAME"]).Trim() == CheckData.IndItemName)
                    {
                        retVal.State.IsSuccess = false;
                        retVal.State.ErrorMessage = "賠償項目重覆!";
                    }
                }

                if (retVal.State.IsSuccess == false)
                    continue;//已經有錯誤了

                if (!string.IsNullOrEmpty(CheckData.ProductNO))
                {
                    if (StringUtil.CStr(dr["PRODNO"]).Trim() == CheckData.ProductNO)
                    {
                        retVal.State.IsSuccess = false;
                        retVal.State.ErrorMessage = "折扣料號重覆!";
                    }
                }

                if (retVal.State.IsSuccess == false)
                    continue;//已經有錯誤了
            }
        }
        return retVal;
    }

    protected void TabContainer1_ActiveTabChanged(object source, DevExpress.Web.ASPxTabControl.TabControlEventArgs e)
    {
        //e.Cancel = true;
        gvMaster.CancelEdit();

        //e.Cancel = true;
        gvDiscountItem.CancelEdit();

        int iIndex = this.TabContainer1.ActiveTabIndex;
        int fIndex = gvMaster.FocusedRowIndex;
        bool blCheck = true;
        if (iIndex == 1 && txtRentNO.Text == "")
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Alert", "alert('請先選擇租金料號');", true);
            this.TabContainer1.ActiveTabIndex = 0;
            blCheck = false;
        }
        if (iIndex == 1 && txtDailyRent.Text == "")
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Alert", "alert('請先選擇日租金額');", true);
            this.TabContainer1.ActiveTabIndex = 0;
            blCheck = false;
        }
        if (iIndex == 1 && txtCompensationNO.Text == "")
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Alert", "alert('請先選擇賠償金料號');", true);
            this.TabContainer1.ActiveTabIndex = 0;
            blCheck = false;
        }

        if (iIndex == 1 && blCheck)
        {
            bindMasterData();
        }
    }

    #region //共用控制所有Grid

    /// <summary>
    /// 控制所有Grid 的Delete 方法。
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    /// <param name="gridName"></param>
    /// <param name="strSessionName"></param>
    /// <param name="strSQLDeletekey"></param> 要delete sql 的key 值
    private void subControlGridDelete(
        ASPxGridView gridName,
        string strSessionName,
        string strSQLDeletekey)
    {
        List<object> gvPKValues = gridName.GetSelectedFieldValues(gridName.KeyFieldName);
        string pkFName = gridName.KeyFieldName;

        DataTable dtSYS;

        if (Session[strSessionName] == null || Session[strSessionName] == "")
        {
            dtSYS = new DataTable();
        }
        else
        {
            dtSYS = Session[strSessionName] as DataTable;
        }

        if (dtSYS.Rows.Count > 0)
        {
            List<object> keyValues = gridName.GetSelectedFieldValues(gridName.KeyFieldName);
            foreach (object key in keyValues)
            {
                string strKey = StringUtil.CStr(key);
                DataRow drSYS = dtSYS.Select(strSQLDeletekey + "='" + StringUtil.CStr(key) + "'")[0];
                dtSYS.Rows.Remove(drSYS);
            }

            gridName.Selection.UnselectAll();
            Session[strSessionName] = dtSYS;
            gridName.DataSource = Session[strSessionName];
            gridName.DataBind();
        }
    }

    /// <summary>
    ///  控制所有Grid 的Update 方法。
    /// </summary>
    private void subControlGridUpdate(ASPxGridView gridName,
        List<UpdateDataInp> DataInp,
        string PkColumnName)
    {
        DataTable dtSYS;
        if (Session[gridName.ID] == null || Session[gridName.ID] == "")
        {
            dtSYS = new DataTable();
        }
        else
        {
            dtSYS = Session[gridName.ID] as DataTable;
        }

        foreach (DataRow dr in dtSYS.Rows)
        {
            //比較是否為相同PK值, 如果為 true 就update
            if (StringUtil.CStr(dr[PkColumnName]).Trim() ==
                DataInp.Find(delegate(UpdateDataInp p)
            {
                return p.ColumnName == PkColumnName;
            }).ColumnValue.Trim()
                )
            {

                //要被Update 的資料
                foreach (UpdateDataInp da in DataInp)
                {
                    dr[da.ColumnName] = da.ColumnValue.Trim();
                }

                dtSYS.AcceptChanges();
                break;
            }

        }

        gridName.CancelEdit();
        //e.Cancel = true;
        Session[gridName.ID] = dtSYS;
        gridName.DataSource = dtSYS;
        gridName.DataBind();
    }

    #endregion

    #region 所有的小class

    //Update 時傳入的參數
    public class UpdateDataInp
    {
        /// <summary>
        /// 指定欄位
        /// </summary>
        public string ColumnName { get; set; }

        /// <summary>
        /// 指定欄位的數值
        /// </summary>
        public string ColumnValue { get; set; }
    }

    #endregion

    /// <summary>
    /// 設定畫面的enbale
    /// </summary>
    /// <param name="bEnb"></param>
    private void validGeneralEnable(bool bEnb)
    {
        txtSupplierNo.Enabled = bEnb;
        RadioButtonList1.SelectedIndex = 0;
        RadioButtonList1.Enabled = bEnb;
        DropProduct.Enabled = bEnb;//商品類別
        //ProductsPopup2.Text = StringUtil.CStr(dr["MODI_USER"]);//產品名稱
        txtRentNO.Enabled = bEnb;//租金料號
        txtDailyRent.Enabled = bEnb;//日租金額
        txtRentDepositNO.Enabled = bEnb;//保證金料號
        txtRentDepositNumber.Enabled = bEnb;//保證金
        txtCompensationNO.Enabled = bEnb;//賠償金料號
        postbackDate_Start.Enabled = bEnb;//S_DATE
        postbackDate_End.Enabled = bEnb;//E_DATE
        memo.Enabled = bEnb;//備註

        gvDiscountItem.Enabled = bEnb;
        gvMaster.Enabled = bEnb;
    }

}
