using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Advtek.Utility;
using System.Collections;
using DevExpress.Web;
using DevExpress.Web.ASPxEditors;
using DevExpress.Web.ASPxGridView;
using DevExpress.Web.ASPxPager;
using FET.POS.Model.Facade.FacadeImpl;
using FET.POS.Model.Common;
using FET.POS.Model.DTO;

public partial class VSS_CONS_CON02 : BasePage
{
    //UUID
    private static string gropStrUUID;
    
    //合作起訖日
    private static string gropCooSDate;
    private static string gropCooEDate;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack && !IsCallback)
        {
            
            if (logMsg.ROLE_TYPE != System.Configuration.ConfigurationManager.AppSettings["DefaultRoleHQ"].ToString())
            {
                //彈跳視窗
                ScriptManager.RegisterClientScriptBlock(this, GetType(), "AlertMessage", "alert('非總部人員無法使用此功能!!');", true);
                PnlCont.Enabled = false;
                return;
            }
                        
            #region //initial 表單
            txtUpdateTime.Text = System.DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");  //更新日期
            txtUpdater.Text = logMsg.OPERATOR + " " + new Employee_Facade().GetEmpName(this.logMsg.OPERATOR);     //更新人員
            lblStatus.Text = "未存檔";

            CooperationDateRangeFrom.Text = System.DateTime.Today.AddDays(1).ToString("yyyy/MM/dd");
            CooperationDateRangeFrom.MinDate = System.DateTime.Today.AddDays(1);
            CooperationDateRangeTo.MinDate = System.DateTime.Today.AddDays(1);
            #endregion

            string strSuppNo = Request.QueryString["No"];
            if (string.IsNullOrEmpty(strSuppNo)) //不是從查詢來的
            {
                TabContainer1.Visible = false;
                TabContainer2.Visible = false;
                bindMasterData();
            }
            else
            {
                try
                {
                    DataTable dt = new CON02_Facade().GetDataFromDB(strSuppNo);
                    initialInDBData(dt);//初始化從查詢來的資料
                }
                catch //(Exception ex)
                {
                    TabContainer1.Visible = false;
                    TabContainer2.Visible = false;
                    bindMasterData();
                    //throw ex;
                }
                btnDelete.Enabled = true;
            }

            BindZoneType("ZoneTypeList");

            BindZoneType("ZTypeLis");
        }
        else
        {
            SetGridViewDataBindBySession(gvMaster);
            SetGridViewDataBindBySession(gvCommission);
            SetGridViewDataBindBySession(gvAmtLevel);
            SetGridViewDataBindBySession(gvProduct);
            SetGridViewDataBindBySession(gvCard);
        }

        if (this.popEmployees.Text == "")
        {
            lblDepartment.Text = "";
        }
        else
        { lblDepartment.Text = ""; }
        
    }

    //初始化從查詢來的資料
    public void initialInDBData(DataTable dt)
    {
        if (dt.Rows.Count > 0)
        {
            DataRow dr = dt.Rows[0];
            if (string.IsNullOrEmpty(dr["CSM_TYPE"] as string) ||
                string.IsNullOrEmpty(dr["SUPP_ID"] as string))
            {
                ScriptManager.RegisterClientScriptBlock(this, GetType(), "AlertMessage", "alert('資料有誤，請聯絡資訊人員!!');", true);
                //PnlCont.Enabled = false;
                return;
            }

            string suppType = dr["CSM_TYPE"].ToString().Trim();
            string suppID = dr["SUPP_ID"].ToString().Trim();

            if (!string.IsNullOrEmpty(dr["CSM_TYPE"].ToString()))//廠商類別
            {
                try
                {
                    vendorTypeComboBox.SelectedIndex = int.Parse(suppType);
                    CooperationDateRangeFrom.Text = DateTime.Parse(dr["S_DATE"].ToString()).ToString("yyyy/MM/dd");//合作起訖日
                    if (!string.IsNullOrEmpty(dr["E_DATE"].ToString()))
                        CooperationDateRangeTo.Text = DateTime.Parse(dr["E_DATE"].ToString()).ToString("yyyy/MM/dd");//合作起訖日
                }
                catch //(Exception ex)
                {
                    ScriptManager.RegisterClientScriptBlock(this, GetType(), "AlertMessage", "alert('資料有誤，請聯絡資訊人員!!');", true);
                    return;
                }
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this, GetType(), "AlertMessage", "alert('資料有誤，請聯落資訊人員!!');", true);
                return;
            }

            if (!string.IsNullOrEmpty(dr["ACCOUNTCODE"].ToString())) //01 23 456789 012345 6789 0123
            {
                try
                {
                    txtAcct1.Text = dr["ACCOUNTCODE"].ToString().Trim().Substring(0, 2);//會計科目 欄位數: 2 2 6 6 4 4 
                    txtAcct2.Text = dr["ACCOUNTCODE"].ToString().Trim().Substring(2, 2);
                    txtAcct3.Text = dr["ACCOUNTCODE"].ToString().Trim().Substring(4, 6);
                    txtAcct4.Text = dr["ACCOUNTCODE"].ToString().Trim().Substring(10, 6);
                    txtAcct5.Text = dr["ACCOUNTCODE"].ToString().Trim().Substring(16, 4);
                    txtAcct6.Text = dr["ACCOUNTCODE"].ToString().Trim().Substring(20, 4);
                }
                catch //(Exception ex)
                {
                    ScriptManager.RegisterClientScriptBlock(this, GetType(), "AlertMessage", "alert('會計科目資料有誤，請聯絡資訊人員!!');", true);
                    return;
                }

            }

            //Session["ReturnUrl"] = Request.UrlReferrer.ToString();
            lblSupplierNo.Text = Request.QueryString["No"];//廠商編號
            txtSupplierName.Text = dr["SUPP_NAME"].ToString().Trim();//廠商名稱
            this.popEmployees.Text = dr["FET_CONTACE_USER"].ToString().Trim();//遠傳聯絡窗口
            txtContact.Text = dr["CONTACE"].ToString().Trim();//聯絡人
            txtPhone.Text = dr["TELNO"].ToString().Trim();//聯絡電話
            txtAddress.Text = dr["SUPP_ADDRESS"].ToString().Trim();//公司地址
            txtContractNo.Text = dr["CONTRACTNO"].ToString().Trim();//合約號碼
            if (dr["CLOSEDAY"].ToString().Trim() == "EN")//EN表示月底
            {
                cutoffDateRadioButtonList.SelectedIndex = 0;//結算日
            }
            else
            {
                cutoffDateRadioButtonList.SelectedIndex = 1;//結算日
                cutoffDayTextBox.Text = dr["CLOSEDAY"].ToString().Trim();
            }

            txtUnifiedBusinessNo.Text = dr["COMPANY_ID"].ToString().Trim();//統一編號
            txtSupplierCode.Text = lblSupplierNo.Text;//廠商代碼
            txtOwner.Text = dr["BOSS_NAME"].ToString().Trim();//負責人
            txtOwnerPhone.Text = dr["BOSS_TEL_NO"].ToString().Trim();//電話號碼

            txtEmail.Text = dr["EMAIL"].ToString().Trim();//電子信箱
            txtFax.Text = dr["FAX"].ToString().Trim();//傳真
            txtMinAmt.Text = dr["AMOUNT_MAX"].ToString().Trim();//最低訂單金額
            txtMemo.Text = dr["MEMO"].ToString().Trim();//備註
            vendorTypeComboBox_SelectedIndexChanged(vendorTypeComboBox, EventArgs.Empty);

            #region //Grid view 賠償項目 折扣項目
            CON02_Facade fac = new CON02_Facade();
            // 廠商類型:
            //1:寄銷廠商
            //2:外部廠商
            if (suppType == "1")
            {
                gvMaster.DataSource = fac.GetCOMMISSIONs(suppID);//佣金比率設定
                gvMaster.DataBind();//StoreList

                //合作店組設定
                StoreListInp storeInp = new StoreListInp();
                storeInp.strSelect = "ZoneTypeList";
                storeInp.suppID = suppID;
                fromQueryAdd(storeInp);

            }
            else if (suppType == "2")
            {
                gvCommission.DataSource = fac.GetCOMMISSIONs(suppID);
                gvCommission.DataBind();

                gvAmtLevel.DataSource = fac.GetAmtLevel(suppID);
                gvAmtLevel.DataBind();

                gvProduct.DataSource = fac.GetSupProd(suppID);
                gvProduct.DataBind();

                //合作店組設定
                StoreListInp storeInp = new StoreListInp();
                storeInp.strSelect = "ZTypeLis";
                storeInp.suppID = suppID;
                fromQueryAdd(storeInp);

                gvCard.DataSource = fac.GetCard(suppID);
                gvCard.DataBind();
            }
            #endregion

            //合作日期-起
            if (DateTime.Parse(CooperationDateRangeFrom.Text) < DateTime.Parse(DateTime.Now.ToString("yyyy/MM/dd")))
            {
                lblStatus.Text = "已生效";
                SetEnableFalse(false);
                btnDelete.Visible = false;
                btnClear.Visible = false;
            }
            else
            {
                lblStatus.Text = "未生效";
                btnDelete.Visible = true;
                btnClear.Visible = true;
            }
        }
    }

    /// <summary>
    /// 設定已生效的資料為enable true false
    /// </summary>
    private void SetEnableFalse(bool bl)
    {
        vendorTypeComboBox.Enabled = bl;
        txtSupplierName.Enabled = bl;
        this.popEmployees.Enabled = bl;
        txtContact.Enabled = bl;
        txtPhone.Enabled = bl;
        txtAddress.Enabled = bl;
        txtContractNo.Enabled = bl;
        cutoffDateRadioButtonList.Enabled = bl;
        txtUnifiedBusinessNo.Enabled = bl;
        txtSupplierCode.Enabled = bl;
        txtOwner.Enabled = bl;
        txtOwnerPhone.Enabled = bl;
        txtEmail.Enabled = bl;
        txtFax.Enabled = bl;
        txtOwnerPhone.Enabled = bl;
        txtMinAmt.Enabled = bl;
        lblStatus.Enabled = bl;
        txtMemo.Enabled = bl;

        CooperationDateRangeFrom.Enabled = bl;
        CooperationDateRangeTo.Enabled = bl;

        txtAcct1.Enabled = bl;
        txtAcct2.Enabled = bl;
        txtAcct3.Enabled = bl;
        txtAcct4.Enabled = bl;
        txtAcct5.Enabled = bl;
        txtAcct6.Enabled = bl;

        //gvMaster.Enabled = bl;
        //gvAmtLevel.Enabled = bl;
        //gvCommission.Enabled = bl;
        //gvProduct.Enabled = bl;
        //gvCard.Enabled = bl;
    }

    private void SetClear()
    {
        vendorTypeComboBox.Text = "";
        txtSupplierName.Text = "";
        this.popEmployees.Text = "";
        txtContact.Text = "";

        txtPhone.Text = "";
        txtAddress.Text = "";
        txtContractNo.Text = "";

        cutoffDateRadioButtonList.SelectedIndex  = 0;
        txtUnifiedBusinessNo.Text = "";
        txtSupplierCode.Text = "";

        txtOwner.Text = "";
        txtOwnerPhone.Text = "";
        txtEmail.Text = "";
        txtFax.Text = "";

        txtOwnerPhone.Text = "";
        txtMinAmt.Text = "";
        lblStatus.Text = "";
        txtMemo.Text = "";

        lblSupplierNo.Text = "";
        CooperationDateRangeFrom.Text = "";
        CooperationDateRangeTo.Text = "";

        txtAcct1.Text = "";
        txtAcct2.Text = "";
        txtAcct3.Text = "";
        txtAcct4.Text = "";
        txtAcct5.Text = "";
        txtAcct6.Text = "";

    }

    /// <summary>
    /// 取得Session 內的資料並填入GridView內
    /// </summary>
    /// <param name="gv"></param>
    private void SetGridViewDataBindBySession(ASPxGridView gv)
    {
        DataTable dtGvMaster = Session[gv.ID] as DataTable;
        if (dtGvMaster != null)
        {
            gv.DataSource = dtGvMaster;
            gv.DataBind();
        }
    }

    protected void bindMasterData()
    {
        DataTable dtResult = new DataTable();
        dtResult = getMasterData();
        Session["gvMaster"] = dtResult;
        gvMaster.DataSource = dtResult;
        gvMaster.DataBind();

        Session["gvCommission"] = getCommissionData();
        gvCommission.DataSource = (DataTable)Session["gvCommission"];
        gvCommission.DataBind();

        Session["gvAmtLevel"] = getgvAmtLevelData();
        gvAmtLevel.DataSource = (DataTable)Session["gvAmtLevel"];
        gvAmtLevel.DataBind();

        Session["gvProduct"] = getgvProductData();
        gvProduct.DataSource = (DataTable)Session["gvProduct"];
        gvProduct.DataBind();

        Session["gvCard"] = getgvCardData();
        gvCard.DataSource = (DataTable)Session["gvCard"];
        gvCard.DataBind();
    }

    #region //所有 Grid Data 初始 getMasterData
    private DataTable getMasterData()
    {
        DataTable dtResult = new DataTable();
        dtResult.Columns.Add("CSC_ID", typeof(string));
        dtResult.Columns.Add("COMMISSIONRATE", typeof(string));
        dtResult.Columns.Add("S_DATE", typeof(string));
        dtResult.Columns.Add("E_DATE", typeof(string));
        return dtResult;
    }

    private DataTable getCommissionData()
    {
        DataTable dtResult = new DataTable();
        dtResult.Columns.Add("CSC_ID", typeof(string));
        dtResult.Columns.Add("COMMISSIONRATE", typeof(string));
        dtResult.Columns.Add("S_DATE", typeof(string));
        dtResult.Columns.Add("E_DATE", typeof(string));
        
        return dtResult;
    }

    private DataTable getgvAmtLevelData()
    {
        DataTable dtResult = new DataTable();
        dtResult.Columns.Add("CSAL_ID", typeof(string));
        dtResult.Columns.Add("SEQNO", typeof(int));
        dtResult.Columns.Add("S_AMT", typeof(int));
        dtResult.Columns.Add("E_AMT", typeof(int));
        dtResult.Columns.Add("COMMISION_RATE", typeof(string));
        dtResult.Columns.Add("S_DATE", typeof(string));
        dtResult.Columns.Add("E_DATE", typeof(string));
        dtResult.PrimaryKey = new DataColumn[] { dtResult.Columns["CSAL_ID"] };
        return dtResult;
    }

    private DataTable getgvProductData()
    {
        DataTable dtResult = new DataTable();
        dtResult.Columns.Add("CSP_ID", typeof(string));
        dtResult.Columns.Add("PRODNO", typeof(string));
        dtResult.Columns.Add("PRODNAME", typeof(string));
        dtResult.Columns.Add("ACCOUNT_CODE", typeof(string));
        dtResult.Columns.Add("S_YYMM", typeof(string));
        dtResult.Columns.Add("E_YYMM", typeof(string));
        return dtResult;
    }

    private DataTable getgvCardData()
    {
        DataTable dtResult = new DataTable();

        dtResult.Columns.Add("CCPR_ID", typeof(string));
        dtResult.Columns.Add("ITEMS", typeof(string));
        dtResult.Columns.Add("TYPE", typeof(string));
        dtResult.Columns.Add("RATE", typeof(string));
        return dtResult;
    }
    #endregion

    /// <summary>
    /// 依廠商類別設定 Enable : true false
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void vendorTypeComboBox_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (vendorTypeComboBox.SelectedIndex == 0)
            return;

        TabContainer1.Visible = false;
        TabContainer2.Visible = false;
        txtMinAmt.Enabled = true;
        txtMinAmt.Style.Clear();
        for (int i = 1; i <= 6; i++)
        {
            ASPxTextBox tb = Page.Master.FindChildControl<ASPxTextBox>("txtAcct" + i);
            tb.Text = "";
            tb.Style.Clear();
        }
        //cbMax.Enabled = cbMin.Enabled = true;
        //cbMax.Checked = cbMin.Checked = false;


        /* 寄售廠商         總額抽成      金額級距    */

        switch (vendorTypeComboBox.SelectedIndex)
        {
            case 1: // 寄售廠商可編輯總金額上限、會計科目
                for (int i = 1; i <= 6; i++)
                {
                    ASPxTextBox tb = Page.Master.FindChildControl<ASPxTextBox>("txtAcct" + i);
                    tb.Text = "";
                    tb.Style.Clear();
                    tb.Enabled = true;
                }
                txtMinAmt.Style.Clear();
                TabContainer1.Visible = true;
                break;

            case 2:   //外部廠商不可編輯總金額底限、會計科目
                for (int i = 1; i <= 6; i++)
                {
                    ASPxTextBox tb = Page.Master.FindChildControl<ASPxTextBox>("txtAcct" + i);
                    //外部廠商 不可輸入會計科目
                    tb.Enabled = false;
                }
                txtMinAmt.Enabled = false;

                TabContainer2.Visible = true;
                if (vendorTypeComboBox.SelectedIndex != 2)
                {
                    TabContainer2.Visible = false;
                }
                break;
        }
        btnSave.Enabled = true;
        btnDelete.Enabled = false;

    }

    /// <summary>
    /// 結算日
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void cutoffDateRadioButtonList_SelectedIndexChanged(object sender, EventArgs e)
    {
        cutoffDayTextBox.Enabled = false;
        if (cutoffDateRadioButtonList.SelectedIndex == 1)
        {
            cutoffDayTextBox.Enabled = true;
        }
    }

    protected void txtFETOwner_TextChanged(Object sender, EventArgs e)
    {
        //lblDepartment.Text = "";// "行銷部";
    }

    #region 選擇店組相關函式

    /// <summary>
    /// 繫結區域別資料
    /// </summary>
    private void BindZoneType(string strSelect)
    {
        ASPxComboBox cb = new ASPxComboBox();
        if (strSelect == "ZoneTypeList")
            cb = ddlSubZone;
        else
            cb = ddlSubZnLis;

        cb.TextField = INV05_PageHelper.TextField;
        cb.ValueField = INV05_PageHelper.ValueField;
        cb.DataSource = INV05_PageHelper.GetZoneTypes();
        cb.DataBind();

        if (cb.SelectedIndex == -1)
        {
            cb.SelectedIndex = 0;
            cb.SelectedIndex = 0;
            if (strSelect == "ZoneTypeList")
                BindZoneTypeList("", "ZoneTypeList");
            else
                BindZoneTypeList("", "ZTypeLis");
        }

    }

    private void BindZoneTypeList(string strSubZone, string strSelect)
    {
        ListBox LB = new ListBox();
        if (strSelect == "ZoneTypeList")
            LB = ZoneTypeList;
        else
            LB = ZTypeLis;

        LB.Items.Clear();

        DataTable dtStore = new Store_Facade().Query_Store("", "", strSubZone);
        Session["StoreNo"] = dtStore;

        if (dtStore.Rows.Count > 0)
        {
            for (int i = 0; i < dtStore.Rows.Count - 1; i++)
            {
                LB.Items.Add(dtStore.Rows[i].ItemArray[0].ToString() + "-" + dtStore.Rows[i].ItemArray[1].ToString());
            }
        }

    }

    //從查詢來的 Add
    protected void fromQueryAdd(StoreListInp storeInp)
    {
        ArrayList itemList = new ArrayList();
        ListBox LBSore = new ListBox();
        if (storeInp.strSelect == "ZoneTypeList")
        {
            LBSore = StoreList;
        }
        else
        {
            //"ZTypeLis"
            LBSore = SList;
        }

        DataTable dtStore ;
        dtStore = new CON02_Facade().GetStore(storeInp.suppID);

        if (dtStore.Rows.Count > 0)
        {
            for (int i = 0; i < dtStore.Rows.Count ; i++)
            {
                LBSore.Items.Add(dtStore.Rows[i].ItemArray[0].ToString() + "-" + dtStore.Rows[i].ItemArray[1].ToString());
            }
        }

    }

    protected void ddlSubZone_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlSubZone.SelectedIndex > 0)
        {
            BindZoneTypeList(ddlSubZone.SelectedItem.Value.ToString(), "ZoneTypeList");
        }
    }
    
    //使用者按Add
    protected void ddlAdd(string strSelect)
    {
        ArrayList itemList = new ArrayList();
        ListBox LB = new ListBox();
        ListBox LBSore = new ListBox();
        if (strSelect == "ZoneTypeList")
        {
            LB = ZoneTypeList;
            LBSore = StoreList;
        }
        else
        {
            //"ZTypeLis"
            LB = ZTypeLis;
            LBSore = SList;
        }

        foreach (ListItem x in LB.Items)
        {
            if (x.Selected)
            {
                LBSore.Items.Add(x);
                itemList.Add(x);
            }
        }
        foreach (object i in itemList)
        {
            LB.Items.Remove((ListItem)i);
        }
    }

    //使用者按Back
    protected void ddlBack(string strSelect)
    {
        ArrayList itemList = new ArrayList();
        ListBox LB = new ListBox();
        ListBox LBSore = new ListBox();
        if (strSelect == "ZoneTypeList")
        {
            LB = ZoneTypeList;
            LBSore = StoreList;
        }
        else
        {
            //"ZTypeLis"
            LB = ZTypeLis;
            LBSore = SList;
        }
        foreach (ListItem x in LBSore.Items)
        {
            if (x.Selected)
            {
                LB.Items.Insert(0, x);
                itemList.Add(x);
            }
        }
        foreach (object i in itemList)
        {
            LBSore.Items.Remove((ListItem)i);
        }
    }

    protected void btnAdd_Click(object sender, ImageClickEventArgs e)
    {
        ddlAdd("ZoneTypeList");
    }

    protected void btnBack_Click(object sender, ImageClickEventArgs e)
    {
        ddlBack("ZoneTypeList");
    }

    protected void ddlSubZnLis_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlSubZnLis.SelectedIndex > 0)
        {
            BindZoneTypeList(ddlSubZnLis.SelectedItem.Value.ToString(), "ZTypeLis");
        }
    }

    protected void btnAdd_Click2(object sender, ImageClickEventArgs e)
    {//"ZTypeLis"
        ddlAdd("ZTypeLis");
    }

    protected void btnBack_Click2(object sender, ImageClickEventArgs e)
    {
        ddlBack("ZTypeLis");
    }
    #endregion

    #region 寄銷廠商維護作業 gvMaster 編輯/更新/取消 相關觸發事件
    protected void gvMaster_CancelRowEditing(object sender, DevExpress.Web.Data.ASPxStartRowEditingEventArgs e)
    {
        btnSave.Enabled = true;
        btnClear.Enabled = true;
        btnDelete.Enabled = false;
    }

    protected void btngvMasterDelete_Click(object sender, EventArgs e)
    {
        //控制所有Grid 的Delete 方法。
        subControlGridDelete(sender, e, gvMaster, "gvMaster", "CSC_ID");
    }

    protected void btngvMasterAdd_Click(object sender, EventArgs e)
    {
        gvMaster.AddNewRow();
    }

    protected void gvMaster_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
    {
        DataTable dtSYS;
        if (Session["gvMaster"] == null || Session["gvMaster"].ToString() == "")
        {
            dtSYS = getMasterData();
        }
        else
        {
            dtSYS = Session["gvMaster"] as DataTable;
        }

        DataRow NewRow = dtSYS.NewRow();
        NewRow["CSC_ID"] = GuidNo.getUUID();
        NewRow["COMMISSIONRATE"] = e.NewValues["COMMISSIONRATE"].ToString();
        NewRow["S_DATE"] = e.NewValues["S_DATE"].ToString();
        NewRow["E_DATE"] = e.NewValues["E_DATE"].ToString();
        dtSYS.Rows.Add(NewRow);

        ((ASPxGridView)sender).CancelEdit();
        e.Cancel = true;
        Session["gvMaster"] = dtSYS;
        gvMaster.DataSource = dtSYS;
        gvMaster.DataBind();

    }

    protected void gvMaster_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
    {
        DataTable dtSYS;
        if (Session["gvMaster"] == null || Session["gvMaster"].ToString() == "")
        {
            dtSYS = getMasterData();
        }
        else
        {
            dtSYS = Session["gvMaster"] as DataTable;
        }

        string sNewComm = e.NewValues["COMMISSIONRATE"].ToString();
        string sNewSDate = e.NewValues["S_DATE"].ToString();
        string sNewEDate = e.NewValues["E_DATE"].ToString();

        string sOldComm = e.OldValues["COMMISSIONRATE"].ToString();
        string sOldSDate = e.OldValues["S_DATE"].ToString();
        string sOldEDate = e.OldValues["E_DATE"].ToString();
        for (int i = 0; i < dtSYS.Rows.Count; i++)
        {
            DataRow dr = dtSYS.Rows[i];
            if (dr["COMMISSIONRATE"].ToString().CompareTo(sOldComm) == 0)
            {
                dr["COMMISSIONRATE"] = sNewComm.Trim();
                dr["S_DATE"] = sNewSDate.Trim();
                dr["E_DATE"] = sNewEDate.Trim();
                dtSYS.AcceptChanges();
                break;
            }
        }

        gvMaster.CancelEdit();
        e.Cancel = true;
        Session["gvMaster"] = dtSYS;
        gvMaster.DataSource = dtSYS;
        gvMaster.DataBind();

    }
    //需修改
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
        //btnClear.Enabled = false;
        //btnDelete.Enabled = false;
    }

    protected void gvMaster_PageIndexChanged(object sender, EventArgs e)
    {
        ASPxGridView grid = sender as ASPxGridView;
        grid.DataSource = Session["gvMaster"];
        grid.DataBind();
    }

    protected void gvMaster_RowValidating(object sender, DevExpress.Web.Data.ASPxDataValidationEventArgs e)
    {

        if (!gvMaster.IsEditing)
            return;

        CheckMethod_PageHelper Ck = new CheckMethod_PageHelper();//check Method
        if (!Ck.IsNumber( StringUtil.CStr(e.NewValues["COMMISSIONRATE"])))
        {
            e.RowError = "佣金比率請輸入數字!!";
            return;
        }
        if (string.IsNullOrEmpty( StringUtil.CStr(e.NewValues["COMMISSIONRATE"])))
        {
            e.RowError = "佣金比率不允許空值，請重新輸入!!";
            return;
        }
        CheckData sDaInp = new CheckData();//傳入的參數
        sDaInp.SDate = StringUtil.CStr(e.NewValues["S_DATE"]);
        sDaInp.EDate = StringUtil.CStr(e.NewValues["E_DATE"]);

        sDaInp.IsNewRowEditing = gvMaster.IsNewRowEditing;
        sDaInp.PkName = gvMaster.KeyFieldName;

        if (!gvMaster.IsNewRowEditing)//是update資料模式
        {
            string SID = "";
            if (e.Keys[gvMaster.KeyFieldName] != null)
            {
                SID =  StringUtil.CStr(e.Keys[gvMaster.KeyFieldName]);
            }
            sDaInp.PkData = SID;
        }

        CheckData CkData = CheckCommssion(sDaInp, gvMaster);
        if (!CkData.State.IsSuccess)
        {
            e.RowError = CkData.State.ErrorMessage;
            return;
        }

    }

    #endregion

    #region gvCommission 編輯/更新/取消 相關觸發事件
    protected void btnGvCommissionAdd_Click(object sender, EventArgs e)
    {
        gvCommission.AddNewRow();
    }

    protected void btnGvCommissionDelete_Click(object sender, EventArgs e)
    {
        //控制所有Grid 的Delete 方法。
        subControlGridDelete(sender, e, gvCommission, "gvCommission", "CSC_ID");
    }

    protected void gvCommission_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
    {

        DataTable dtSYS;
        if (Session["gvCommission"] == null || Session["gvCommission"].ToString() == "")
        {
            dtSYS = getCommissionData();
        }
        else
        {
            dtSYS = Session["gvCommission"] as DataTable;
        }

        DataRow NewRow = dtSYS.NewRow();
        NewRow["CSC_ID"] = GuidNo.getUUID();
        NewRow["COMMISSIONRATE"] = e.NewValues["COMMISSIONRATE"].ToString();
        NewRow["S_DATE"] = e.NewValues["S_DATE"].ToString();
        NewRow["E_DATE"] = e.NewValues["E_DATE"].ToString();
        dtSYS.Rows.Add(NewRow);

        ((ASPxGridView)sender).CancelEdit();
        e.Cancel = true;
        Session["gvCommission"] = dtSYS;
        gvCommission.DataSource = dtSYS;
        gvCommission.DataBind();
    }

    protected void gvCommission_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
    {
        DataTable dtSYS;
        if (Session["gvCommission"] == null || Session["gvCommission"].ToString() == "")
        {
            dtSYS = getCommissionData();
        }
        else
        {
            dtSYS = Session["gvCommission"] as DataTable;
        }

        string sNewComm = e.NewValues["COMMISSIONRATE"].ToString();
        string sNewSDate = e.NewValues["S_DATE"].ToString();
        string sNewEDate = e.NewValues["E_DATE"].ToString();

        string sOldComm = e.OldValues["COMMISSIONRATE"].ToString();
        string sOldSDate = e.OldValues["S_DATE"].ToString();
        string sOldEDate = e.OldValues["E_DATE"].ToString();
        for (int i = 0; i < dtSYS.Rows.Count; i++)
        {
            DataRow dr = dtSYS.Rows[i];
            if (dr["COMMISSIONRATE"].ToString().CompareTo(sOldComm) == 0)
            {
                dr["COMMISSIONRATE"] = sNewComm.Trim();
                dr["S_DATE"] = sNewSDate.Trim();
                dr["E_DATE"] = sNewEDate.Trim();
                dtSYS.AcceptChanges();
                break;
            }
        }

        gvCommission.CancelEdit();
        e.Cancel = true;
        Session["gvCommission"] = dtSYS;
        gvCommission.DataSource = dtSYS;
        gvCommission.DataBind();
    }

    protected void gvCommission_CancelRowEditing(object sender, DevExpress.Web.Data.ASPxStartRowEditingEventArgs e)
    {
        btnSave.Enabled = true;
        btnClear.Enabled = true;
        btnDelete.Enabled = false;
    }

    protected void gvCommission_PageIndexChanged(object sender, EventArgs e)
    {
        ASPxGridView grid = sender as ASPxGridView;
        grid.DataSource = Session["gvCommission"];
        grid.DataBind();
    }

    protected void gvCommission_CommandButtonInitialize(object sender, ASPxGridViewCommandButtonEventArgs e)
    {
        if (e.VisibleIndex > -1)
        {
            if (e.ButtonType == ColumnCommandButtonType.Edit || e.ButtonType == ColumnCommandButtonType.SelectCheckbox)
            {
                //  string status1 = gvMaster.GetRowValues(e.VisibleIndex, "STATUS").ToString();
            }
        }
    }

    protected void gvCommission_InitNewRow(object sender, DevExpress.Web.Data.ASPxDataInitNewRowEventArgs e)
    {

    }

    protected void gvCommission_RowValidating(object sender, DevExpress.Web.Data.ASPxDataValidationEventArgs e)
    {
        if (!gvCommission.IsEditing)
            return;
        CheckMethod_PageHelper Ck = new CheckMethod_PageHelper();//check Method

        if (!Ck.IsNumber( StringUtil.CStr(e.NewValues["COMMISSIONRATE"])))
        {
            e.RowError = "佣金比率請輸入數字!!";
            return;
        }

        if (string.IsNullOrEmpty( StringUtil.CStr(e.NewValues["COMMISSIONRATE"])))
        {
            e.RowError = "佣金比率不允許空值，請重新輸入!!";
            return;
        }

        CheckData sDaInp = new CheckData();//傳入的參數
        sDaInp.SDate = StringUtil.CStr(e.NewValues["S_DATE"]);
        sDaInp.EDate = StringUtil.CStr(e.NewValues["E_DATE"]);
        sDaInp.IsNewRowEditing = gvCommission.IsNewRowEditing;

        sDaInp.PkName = gvCommission.KeyFieldName;

        if (!gvCommission.IsNewRowEditing)//是update資料模式
        {
            string SID = "";
            if (e.Keys[gvCommission.KeyFieldName] != null)
            {
                SID =  StringUtil.CStr(e.Keys[gvCommission.KeyFieldName]);
            }
            sDaInp.PkData = SID;
        }

        CheckData CkData = CheckCommssion(sDaInp, gvCommission);
        if (!CkData.State.IsSuccess)
        {
            e.RowError = CkData.State.ErrorMessage;
            return;
        }

    }
    #endregion

    #region gvAmtLevel 編輯/更新/取消 相關觸發事件
    protected void gvAmtAdd_Click(object sender, EventArgs e)
    {
        gvAmtLevel.AddNewRow();
    }

    protected void gvAmtDelete_Click(object sender, EventArgs e)
    {
        //控制所有Grid 的Delete 方法。
        subControlGridDelete(sender, e, gvAmtLevel, "gvAmtLevel", "CSAL_ID");
    }

    protected void gvAmtLevel_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
    {
        DataTable dtSYS;
        if (Session["gvAmtLevel"] == null || Session["gvAmtLevel"].ToString() == "")
        {
            dtSYS = getgvAmtLevelData();
        }
        else
        {
            dtSYS = Session["gvAmtLevel"] as DataTable;
        }

        DataRow NewRow = dtSYS.NewRow();
        NewRow["CSAL_ID"] = GuidNo.getUUID();
        NewRow["S_AMT"] = e.NewValues["S_AMT"].ToString();
        NewRow["E_AMT"] = e.NewValues["E_AMT"].ToString();
        NewRow["COMMISION_RATE"] = e.NewValues["COMMISION_RATE"].ToString();
        NewRow["S_DATE"] = e.NewValues["S_DATE"].ToString();
        NewRow["E_DATE"] = e.NewValues["E_DATE"].ToString();
        dtSYS.Rows.Add(NewRow);

        ((ASPxGridView)sender).CancelEdit();
        e.Cancel = true;
        Session["gvAmtLevel"] = dtSYS;
        gvAmtLevel.DataSource = dtSYS;
        gvAmtLevel.DataBind();
    }

    protected void gvAmtLevel_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
    {
        DataTable dtSYS;
        if (Session["gvAmtLevel"] == null || Session["gvAmtLevel"].ToString() == "")
        {
            dtSYS = getgvAmtLevelData();
        }
        else
        {
            dtSYS = Session["gvAmtLevel"] as DataTable;
        }

        string sNewComm = e.NewValues["COMMISION_RATE"].ToString();
        string sNewSDate = e.NewValues["S_DATE"].ToString();
        string sNewEDate = e.NewValues["E_DATE"].ToString();
        string sNewSAmt = e.NewValues["S_AMT"].ToString();
        string sNewEAmt = e.NewValues["E_AMT"].ToString();

        string sOldComm = e.OldValues["COMMISION_RATE"].ToString();
        string sOldSDate = e.OldValues["S_DATE"].ToString();
        string sOldEDate = e.OldValues["E_DATE"].ToString();
        for (int i = 0; i < dtSYS.Rows.Count; i++)
        {
            DataRow dr = dtSYS.Rows[i];
            if (dr["COMMISION_RATE"].ToString().CompareTo(sOldComm) == 0)
            {
                dr["COMMISION_RATE"] = sNewComm.Trim();
                dr["S_DATE"] = sNewSDate.Trim();
                dr["E_DATE"] = sNewEDate.Trim();
                dr["S_AMT"] = sNewSAmt.Trim();
                dr["E_AMT"] = sNewEAmt.Trim();

                dtSYS.AcceptChanges();
                break;
            }
        }

        gvAmtLevel.CancelEdit();
        e.Cancel = true;
        Session["gvAmtLevel"] = dtSYS;
        gvAmtLevel.DataSource = dtSYS;
        gvAmtLevel.DataBind();
    }

    protected void gvAmtLevel_PageIndexChanged(object sender, EventArgs e)
    {
        ASPxGridView grid = sender as ASPxGridView;
        grid.DataSource = Session["gvAmtLevel"];
        grid.DataBind();
    }

    protected void gvAmtLevel_RowValidating(object sender, DevExpress.Web.Data.ASPxDataValidationEventArgs e)
    {
        if (!gvAmtLevel.IsEditing)
            return;
        CheckMethod_PageHelper Ck = new CheckMethod_PageHelper();//check Method
        if (!Ck.IsNumber(StringUtil.CStr(e.NewValues["COMMISION_RATE"])))
        {
            e.RowError = "佣金比率請輸入數字!!";
            return;
        }
        if (!Ck.IsNumber(StringUtil.CStr(e.NewValues["S_AMT"])))
        {
            e.RowError = "起-金額級距請輸入數字!!";
            return;
        }
        if (!Ck.IsNumber( StringUtil.CStr(e.NewValues["E_AMT"])))
        {
            e.RowError = "訖-金額級距請輸入數字!!";
            return;
        }
        if (string.IsNullOrEmpty(StringUtil.CStr(e.NewValues["S_AMT"])))
        {
            e.RowError = "起-金額級距不允許空值，請重新輸入!!";
            return;
        }
        if (string.IsNullOrEmpty( StringUtil.CStr(e.NewValues["COMMISION_RATE"])))
        {
            e.RowError = "佣金比率不允許空值，請重新輸入!!";
            return;
        }
        if (int.Parse( StringUtil.CStr(e.NewValues["S_AMT"])) > 
            int.Parse( StringUtil.CStr(e.NewValues["E_AMT"]))
            )
        {
            e.RowError = "「訖-金額級距」應大於「起-金額級距」，請重新輸入!!";
            return;
        }

        CheckData sDaInp = new CheckData();//傳入的參數
        sDaInp.SDate =  StringUtil.CStr(e.NewValues["S_DATE"]);
        sDaInp.EDate =  StringUtil.CStr(e.NewValues["E_DATE"]);
        sDaInp.IsNewRowEditing = gvAmtLevel.IsNewRowEditing;

        sDaInp.PkName = gvAmtLevel.KeyFieldName;

        if (!gvAmtLevel.IsNewRowEditing)//是update資料模式
        {
            string SID = "";
            if (e.Keys[gvAmtLevel.KeyFieldName] != null)
            {
                SID =  StringUtil.CStr(e.Keys[gvAmtLevel.KeyFieldName]);
            }
            sDaInp.PkData = SID;
        }

        CheckData CkData = CheckCommssion(sDaInp, gvAmtLevel);
        if (!CkData.State.IsSuccess)
        {
            e.RowError = CkData.State.ErrorMessage;
            return;
        }


    }

    protected void gvAmtLevel_CommandButtonInitialize(object sender, ASPxGridViewCommandButtonEventArgs e)
    {
        if (e.VisibleIndex > -1)
        {
            if (e.ButtonType == ColumnCommandButtonType.Edit || e.ButtonType == ColumnCommandButtonType.SelectCheckbox)
            {
                //  string status1 = gvMaster.GetRowValues(e.VisibleIndex, "STATUS").ToString();
            }
        }
    }

    #endregion

    #region gvProduct 編輯/更新/取消 相關觸發事件
    protected void btnGvProductAdd_Click(object sender, EventArgs e)
    {
        gvProduct.AddNewRow();
    }

    protected void gvProduct_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
    {
        DataTable dtSYS;
        if (Session["gvProduct"] == null || Session["gvProduct"].ToString() == "")
        {
            dtSYS = getgvProductData();
        }
        else
        {
            dtSYS = Session["gvProduct"] as DataTable;
        }

        DataRow NewRow = dtSYS.NewRow();
        NewRow["PRODNO"] = e.NewValues["PRODNO"].ToString();
        NewRow["ACCOUNT_CODE"] = e.NewValues["ACCOUNT_CODE"].ToString();
        NewRow["S_YYMM"] = e.NewValues["S_YYMM"].ToString();
        NewRow["E_YYMM"] = e.NewValues["E_YYMM"].ToString();
        NewRow["PRODNAME"] = e.NewValues["PRODNAME"].ToString();
        dtSYS.Rows.Add(NewRow);

        ((ASPxGridView)sender).CancelEdit();
        e.Cancel = true;
        Session["gvProduct"] = dtSYS;
        gvProduct.DataSource = dtSYS;
        gvProduct.DataBind();
    }

    protected void gvProduct_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
    {
        DataTable dtSYS;
        if (Session["gvProduct"] == null || Session["gvProduct"].ToString() == "")
        {
            dtSYS = getgvProductData();
        }
        else
        {
            dtSYS = Session["gvProduct"] as DataTable;
        }
        string sNewProdNo = e.NewValues["PRODNO"].ToString();
        string sNewAccount = e.NewValues["ACCOUNT_CODE"].ToString();
        string sNewSDate = e.NewValues["S_YYMM"].ToString();
        string sNewEDate = e.NewValues["E_YYMM"].ToString();

        string sOldProdNo = e.OldValues["PRODNO"].ToString();
        string sOldAccount = e.OldValues["ACCOUNT_CODE"].ToString();
        string sOldSDate = e.OldValues["S_YYMM"].ToString();
        string sOldEDate = e.OldValues["E_YYMM"].ToString();
        for (int i = 0; i < dtSYS.Rows.Count; i++)
        {
            DataRow dr = dtSYS.Rows[i];
            if (dr["PRODNO"].ToString().CompareTo(sOldProdNo) == 0)
            {
                dr["PRODNO"] = sNewProdNo.Trim();
                dr["ACCOUNT_CODE"] = sNewAccount.Trim();
                dr["S_YYMM"] = sNewSDate.Trim();
                dr["E_YYMM"] = sNewEDate.Trim();
                dtSYS.AcceptChanges();
                break;
            }
        }

        gvProduct.CancelEdit();
        e.Cancel = true;
        Session["gvProduct"] = dtSYS;
        gvProduct.DataSource = dtSYS;
        gvProduct.DataBind();
    }

    protected void gvProduct_RowValidating(object sender, DevExpress.Web.Data.ASPxDataValidationEventArgs e)
    {//商品料號是否可以輸入
        if (!gvProduct.IsEditing)
            return;
        //check Method
        CheckMethod_PageHelper Ck = new CheckMethod_PageHelper();

        if (string.IsNullOrEmpty( StringUtil.CStr(e.NewValues["PRODNO"])))
        {
            e.RowError = "商品料號不允許空值，請重新輸入!!";
            return;
        }

        if (string.IsNullOrEmpty( StringUtil.CStr(e.NewValues["S_YYMM"])))
        {
            e.RowError = "起始月份不允許空值，請重新輸入!!";
            return;
        }

        if (string.IsNullOrEmpty( StringUtil.CStr(e.NewValues["S_YYMM"])) &&
            string.IsNullOrEmpty( StringUtil.CStr(e.NewValues["E_YYMM"]))
            )
            return;

        CheckData sDaInp = new CheckData();//傳入的參數
        sDaInp.SDate = StringUtil.CStr(e.NewValues["S_YYMM"]);
        sDaInp.EDate = StringUtil.CStr(e.NewValues["E_YYMM"]);

        sDaInp.ProductNO =  StringUtil.CStr(e.NewValues["PRODNO"]);
        sDaInp.IsNewRowEditing = gvProduct.IsNewRowEditing;

        sDaInp.PkName = gvProduct.KeyFieldName;

        if (!gvProduct.IsNewRowEditing)//是update資料模式
        {
            string SID = "";
            if (e.Keys[gvProduct.KeyFieldName] != null)
            {
                SID =  StringUtil.CStr(e.Keys[gvProduct.KeyFieldName]);
            }
            sDaInp.PkData = SID;
        }

        CheckData CkData = CheckCommssion(sDaInp, gvProduct);
        if (!CkData.State.IsSuccess)
        {
            e.RowError = CkData.State.ErrorMessage;
            return;
        }
    }

    protected void gvProduct_PageIndexChanged(object sender, EventArgs e)
    {
        ASPxGridView grid = sender as ASPxGridView;
        grid.DataSource = Session["gvProduct"];
        grid.DataBind();
    }

    protected void btnGvProductDelete_Click(object sender, EventArgs e)
    {
        //控制所有Grid 的Delete 方法。
        subControlGridDelete(sender, e, gvProduct, "gvProduct", "PRODNO");
    }

    #endregion

    #region gvCard 編輯/更新/取消 相關觸發事件
    protected void gvCardAdd_Click(object sender, EventArgs e)
    {
        gvCard.AddNewRow();
    }

    protected void gvCardDelete_Click(object sender, EventArgs e)
    {
        //控制所有Grid 的Delete 方法。
        subControlGridDelete(sender, e, gvCard, "gvCard", "ITEMS");
    }

    protected void gvCard_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
    {
        DataTable dtSYS;
        if (Session["gvCard"] == null || Session["gvCard"].ToString() == "")
        {
            dtSYS = getgvCardData();
        }
        else
        {
            dtSYS = Session["gvCard"] as DataTable;
        }

        DataRow NewRow = dtSYS.NewRow();
        NewRow["CCPR_ID"] = GuidNo.getUUID();
        NewRow["TYPE"] = e.NewValues["TYPE"].ToString();
        NewRow["RATE"] = e.NewValues["RATE"].ToString();
        dtSYS.Rows.Add(NewRow);

        ((ASPxGridView)sender).CancelEdit();
        e.Cancel = true;
        Session["gvCard"] = dtSYS;
        gvCard.DataSource = dtSYS;
        gvCard.DataBind();
    }

    protected void gvCard_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
    {
        DataTable dtSYS;
        if (Session["gvCard"] == null || Session["gvCard"].ToString() == "")
        {
            dtSYS = getgvCardData();
        }
        else
        {
            dtSYS = Session["gvCard"] as DataTable;
        }
        string sNewCCPR_ID = e.NewValues["CCPR_ID"].ToString();
        string sNewAccount = e.NewValues["ITEMS"].ToString();
        string sNewSDate = e.NewValues["TYPE"].ToString();
        string sNewEDate = e.NewValues["RATE"].ToString();

        string sOldCCPR_ID = e.OldValues["CCPR_ID"].ToString();
        string sOldAccount = e.OldValues["ITEMS"].ToString();
        string sOldSDate = e.OldValues["TYPE"].ToString();
        string sOldEDate = e.OldValues["RATE"].ToString();

        for (int i = 0; i < dtSYS.Rows.Count; i++)
        {
            DataRow dr = dtSYS.Rows[i];
            if (dr["CCPR_ID"].ToString().CompareTo(sOldCCPR_ID) == 0)
            {
                dr["CCPR_ID"] = sNewCCPR_ID.Trim();
                dr["ITEMS"] = sNewAccount.Trim();
                dr["TYPE"] = sNewSDate.Trim();
                dr["RATE"] = sNewEDate.Trim();
                dtSYS.AcceptChanges();
                break;
            }
        }

        gvCard.CancelEdit();
        e.Cancel = true;
        Session["gvCard"] = dtSYS;
        gvCard.DataSource = dtSYS;
        gvCard.DataBind();
    }

    protected void gvCard_RowValidating(object sender, DevExpress.Web.Data.ASPxDataValidationEventArgs e)
    {
        if (!gvCard.IsEditing)
            return;

        if (string.IsNullOrEmpty(StringUtil.CStr(e.NewValues["RATE"])))
        {
            e.RowError = "手續費不允許空值，請重新輸入!!";
            return;
        }

        //check Method
        CheckMethod_PageHelper Ck = new CheckMethod_PageHelper();

        if (!Ck.IsNumber(StringUtil.CStr(e.NewValues["RATE"])))
        {
            e.RowError = "手續費請輸入數字!!";
            return;
        }

        CheckData pChDate = new CheckData();
        pChDate.CardType = StringUtil.CStr(e.NewValues["TYPE"]);
        pChDate.SessionName = "gvCard";
        pChDate.IsNewRowEditing = gvCard.IsNewRowEditing;

        pChDate.PkName = gvCard.KeyFieldName;

        if (!gvCard.IsNewRowEditing)//是update資料模式
        {
            string SID = "";
            if (e.Keys[gvCard.KeyFieldName] != null)
            {
                SID = StringUtil.CStr(e.Keys[gvCard.KeyFieldName]);
            }
            pChDate.PkData = SID;
        }

        CheckData CkeckCard = CheckRedundant(pChDate);
        if (!CkeckCard.State.IsSuccess)
        {
            e.RowError = CkeckCard.State.ErrorMessage;
            return;
        }
    }
    #endregion

    #region 存檔 清除 刪除 按鈕事件
    protected void btnSave_Click(object sender, EventArgs e)
    {        
        #region Check

        string strInfo = "";
        CheckMethod_PageHelper Ck = new CheckMethod_PageHelper();

        if (vendorTypeComboBox.SelectedIndex == 0)
        {
            ScriptManager.RegisterClientScriptBlock(this, GetType(), "AlertMessage", "alert('請選擇廠商類別!');", true);
            return;
        }

        if (!string.IsNullOrEmpty(txtUnifiedBusinessNo.Text))
        {
            strInfo = Ck.CheckINVFunction(txtUnifiedBusinessNo.Text);
        }
        if (strInfo == "false")
        {
            ScriptManager.RegisterClientScriptBlock(this, GetType(), "AlertMessage", "alert('統一編號請輸入8碼，請重新輸入');", true);
            return;
        }
        else if (strInfo == "notInteger")
        {
            ScriptManager.RegisterClientScriptBlock(this, GetType(), "AlertMessage", "alert('統一編號請輸入數字，請重新輸入!');", true);
            return;
        }

        #endregion

        CON02_Facade CON02_F = new CON02_Facade();
        int intResult = 0;
        CON02_CSM_SUPPLIER.CSM_SUPPLIERDataTable dtSupplier = null;
        CON02_CSM_SUPPLIER.CSM_SUPP_COMMISSIONDataTable dtCommission = null;
        CON02_CSM_SUPPLIER.CSM_SUPPSTOREDataTable dtSuppstore = null;
        CON02_CSM_SUPPLIER.CSM_SUP_AMT_LEVELDataTable dtSupAmtLevel = null;
        CON02_CSM_SUPPLIER.CSM_SUP_PRODDataTable dtProd = null;
        CON02_CSM_SUPPLIER.CSM_CREDIT_CARD_PROCE_RATEDataTable dtCard = null;

        //寄銷廠商
        if (vendorTypeComboBox.SelectedIndex == 1)
        {
            dtSupplier = doInsertSUPPLIER();
            dtCommission = doInsertCommission(true);
            dtSuppstore = doInsertSuppstore(true);

            intResult = CON02_F.SaveOut(dtSupplier, dtCommission, dtSuppstore);
        }
        else if (vendorTypeComboBox.SelectedIndex == 2)
        {//外部廠商
            dtSupplier = doInsertSUPPLIER();//外部廠商主檔
            dtCommission = doInsertCommission(false);//佣金比率設定檔
            dtSuppstore = doInsertSuppstore(false);//廠商指定店組
            dtSupAmtLevel = doInsertSupAmtLevel();//金額級距
            dtProd = doInsertProd();//外部廠商商品編號設定
            dtCard = doInsertCard();//信用卡
            intResult = CON02_F.SaveOutFactory(dtSupplier, dtCommission, dtSuppstore, dtSupAmtLevel, dtProd, dtCard);
        }
        //更新日期,更新人員
        txtUpdateTime.Text = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");
        txtUpdater.Text = logMsg.MODI_USER + " " + new Employee_Facade().GetEmpName(logMsg.MODI_USER);

        ScriptManager.RegisterClientScriptBlock(this, typeof(string), "儲存", "alert('存檔完成!');", true);
        btnClear_Click(sender, e);


    }

    protected void btnClear_Click(object sender, EventArgs e)
    {
        //vendorTypeComboBox.SelectedIndex = 0;
        //txtSupplierName.Text = "";
        //this.popEmployees.Text = "";
        //txtContact.Text = "";
        //txtPhone.Text = "";
        //txtAddress.Text = "";
        //txtContractNo.Text = "";
        //cutoffDateRadioButtonList.SelectedIndex = 0;
        //txtUnifiedBusinessNo.Text = "";
        //txtSupplierCode.Text = "";
        //txtOwner.Text = "";
        //txtOwnerPhone.Text = "";
        //txtEmail.Text = "";
        //txtFax.Text = "";
        //txtOwnerPhone.Text = "";
        //txtMinAmt.Text = "";
        //lblStatus.Text = "";
        //txtMemo.Text = "";

        //CooperationDateRangeFrom.Text = "";
        //CooperationDateRangeTo.Text = "";

        //txtAcct1.Text = "";
        //txtAcct2.Text = "";
        //txtAcct3.Text = "";
        //txtAcct4.Text = "";
        //txtAcct5.Text = "";
        //txtAcct6.Text = "";

        //bindMasterData();

        //TabContainer1.Visible = false;
        //TabContainer2.Visible = false;
        //btnDelete.Enabled = false;
    }

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        CON02_Facade fd = new CON02_Facade();
        CheckData ck = new CheckData();

        string supNo = lblSupplierNo.Text.Trim();//廠商代碼
        string strMessage = "";

        if (string.IsNullOrEmpty(supNo))
        { return; }

        try
        {
            fd.DeleteSupplier(supNo);
            strMessage = "刪除成功";
            ck.State.IsSuccess = true;
        }
        catch //(Exception ex)
        {
            strMessage = " 刪除失敗，請與相關人員聯絡";
            ck.State.IsSuccess = false;
        }

        //刪除成功初始化資料。
        if (ck.State.IsSuccess)
        {
            SetEnableFalse(true);
            SetClear();

            TabContainer1.Visible = false;
            TabContainer2.Visible = false;
            bindMasterData();
        }

        ScriptManager.RegisterClientScriptBlock(this, GetType(), "AlertMessage", "alert('" + strMessage + "');", true);
    }

    #endregion

    #region      webService
    //ajax ckeck 統一編號
    [System.Web.Services.WebMethod()]
    [System.Web.Script.Services.ScriptMethod()]
    static public string checkINVcode(string strINV)
    {
        string strInfo = "";
        CheckMethod_PageHelper Ck = new CheckMethod_PageHelper();
        if (!string.IsNullOrEmpty(strINV))
        {
            strInfo = Ck.CheckINVFunction(strINV);
        }
        return strInfo;
    }

    //ajax 取得Product Name 
    [System.Web.Services.WebMethod()]
    [System.Web.Script.Services.ScriptMethod()]
    static public string getPRODNAME(string productNO)
    {
        string strInfo = "";
        DataTable dt = new Product_Facade().Query_ProductInfo(productNO);
        if (dt.Rows.Count > 0)
        {
            strInfo = dt.Rows[0]["PRODNAME"].ToString();
        }
        else
        {
            strInfo = "fail";
        }

        return strInfo;
    }

    //ajax 取得Product 的會計科目
    [System.Web.Services.WebMethod()]
    [System.Web.Script.Services.ScriptMethod()]
    static public string getAccountCode(string productNO)
    {
        string strInfo = "";
        DataTable dt  = new CON02_Facade().GetProdDataByKey(productNO);
        if (dt.Rows.Count > 0)
        {
            strInfo = dt.Rows[0]["ACCOUNTCODE"].ToString();
        }
        else
        {
            strInfo = "fail";
        }

        return strInfo;
    }

    //ajax check SuppNO是否重覆
    [System.Web.Services.WebMethod()]
    [System.Web.Script.Services.ScriptMethod()]
    static public string checkSuppNO(string NO)
    {
        string strInfo = "";
        CheckMethod_PageHelper Ck = new CheckMethod_PageHelper();
        bool bReturn = Ck.IsSuppNOInDB(NO);
        if (bReturn)
        {
            strInfo = "fail";
        }
        return strInfo;
    }
    #endregion

    #region doInsert 區
    
    //外部廠商主檔
    private CON02_CSM_SUPPLIER.CSM_SUPPLIERDataTable doInsertSUPPLIER()
    {
        //1寄銷廠商 2外部廠商
        CON02_CSM_SUPPLIER DtoSupplier = new CON02_CSM_SUPPLIER();
        CON02_CSM_SUPPLIER.CSM_SUPPLIERDataTable dtSupplier = null;
        //dtSupplier = DtoSupplier.Tables["CSM_SUPPLIER"] as CON02_CSM_SUPPLIER.CSM_SUPPLIERDataTable;
        CON02_CSM_SUPPLIER.CSM_SUPPLIERRow drSupplier;

        dtSupplier = DtoSupplier.Tables["CSM_SUPPLIER"] as CON02_CSM_SUPPLIER.CSM_SUPPLIERDataTable;
        drSupplier = dtSupplier.NewCSM_SUPPLIERRow();

        //單PK strUUID
        string strUUID = GuidNo.getUUID();
        gropStrUUID = strUUID; //存UUID

        //異動的欄位
        drSupplier["SUPP_ID"] = strUUID;
        drSupplier["SUPP_NO"] = txtSupplierCode.Text.ToUpper();
        drSupplier["CSM_TYPE"] = vendorTypeComboBox.SelectedItem.Value.ToString();

        if (popEmployees.Text != null)
            drSupplier["FET_CONTACE_USER"] = popEmployees.Text.ToString().Trim();

        if (txtSupplierName.Text != null)
            drSupplier["SUPP_NAME"] = txtSupplierName.Text.ToString().Trim();

        if (txtAddress.Text != null)
            drSupplier["SUPP_ADDRESS"] = txtAddress.Text.ToString().Trim();

        if (txtContact.Text != null)
            drSupplier["CONTACE"] = txtContact.Text.ToString().Trim();

        if (txtPhone.Text != null)
            drSupplier["TELNO"] = txtPhone.Text.ToString().Trim();

        if (CooperationDateRangeFrom.Text != "")
        {
            drSupplier["S_DATE"] = CooperationDateRangeFrom.Text.ToString().Trim();
            gropCooSDate = CooperationDateRangeFrom.Text.ToString().Trim();
        }

        if (CooperationDateRangeTo.Text != "")
        {
            drSupplier["E_DATE"] = CooperationDateRangeTo.Text.ToString().Trim();
            gropCooEDate = CooperationDateRangeTo.Text.ToString().Trim();
        }

        if (txtContractNo.Text != null)
            drSupplier["CONTRACTNO"] = txtContractNo.Text.ToString().Trim().ToUpper();//合約號碼

        if (cutoffDayTextBox.Text != null)
            drSupplier["CLOSEDAY"] = cutoffDayTextBox.Text.ToString().Trim();//結算日

        if (txtUnifiedBusinessNo.Text != null)
            drSupplier["COMPANY_ID"] = txtUnifiedBusinessNo.Text.ToString().Trim();

        if (txtOwner.Text != null)
            drSupplier["BOSS_NAME"] = txtOwner.Text.ToString().Trim();

        if (txtOwnerPhone.Text != null)
            drSupplier["BOSS_TEL_NO"] = txtOwnerPhone.Text.ToString().Trim();

        if (txtFax.Text != null)
            drSupplier["FAX"] = txtFax.Text.ToString().Trim();

        if (txtEmail.Text != null)
            drSupplier["EMAIL"] = txtEmail.Text.ToString().Trim();

        if (txtMinAmt.Text != null)
            drSupplier["AMOUNT_MAX"] = txtMinAmt.Text.ToString().Trim();

        //會計科目
        string strAccountcode = txtAcct1.Text.ToString().Trim() +
                                txtAcct2.Text.ToString().Trim() +
                                txtAcct3.Text.ToString().Trim() +
                                txtAcct4.Text.ToString().Trim() +
                                txtAcct5.Text.ToString().Trim() +
                                txtAcct6.Text.ToString().Trim();
        drSupplier["ACCOUNTCODE"] = strAccountcode;

        if (txtMemo.Text != null)
            drSupplier["MEMO"] = txtMemo.Text.ToString().Trim();
        drSupplier["STATUS"] = "1";//TAXNO CLOSTDAY

        if (cutoffDateRadioButtonList.SelectedIndex == 0)
        {
            //結算日 月底日 為EN
            drSupplier["CLOSEDAY"] = "EN";
        }
        else
        {
            drSupplier["CLOSEDAY"] = cutoffDayTextBox.Text;
        }

        drSupplier["MODI_USER"] = logMsg.MODI_USER;
        drSupplier["MODI_DTM"] = Convert.ToDateTime(System.DateTime.Now);
        if (CreateUser.Value == "")
        {
            CreateUser.Value = logMsg.MODI_USER;
        }
        drSupplier["CREATE_USER"] = CreateUser.Value;
        if (CreateDTM.Value == "")
        {
            CreateDTM.Value = Convert.ToDateTime(System.DateTime.Now).ToString();
        }
        drSupplier["CREATE_DTM"] = Convert.ToDateTime(CreateDTM.Value);

        dtSupplier.Rows.Add(drSupplier);

        return dtSupplier;
    }
    
    //佣金比率設定檔
    private CON02_CSM_SUPPLIER.CSM_SUPP_COMMISSIONDataTable doInsertCommission(bool bFirstTable)
    {
        CON02_CSM_SUPPLIER.CSM_SUPP_COMMISSIONDataTable dtCommission = null;

        DataTable dtProd = null;

        if (bFirstTable)
        {
            dtProd = (DataTable)Session["gvMaster"];
        }
        else
        {
            dtProd = (DataTable)Session["gvCommission"];
        }

        dtCommission = new CON02_CSM_SUPPLIER.CSM_SUPP_COMMISSIONDataTable();
        CON02_CSM_SUPPLIER.CSM_SUPP_COMMISSIONRow drCommission;

        if (dtProd.Rows.Count > 0)
        {
            foreach (DataRow dr in dtProd.Rows)
            {
                drCommission = dtCommission.NewCSM_SUPP_COMMISSIONRow();
                drCommission["CSC_ID"] = GuidNo.getUUID();
                drCommission["SUPP_ID"] = gropStrUUID;
                drCommission["SEQNO"] = 1;

                drCommission["COMMISSION"] = dr["COMMISSIONRATE"].ToString();
                drCommission["S_DATE"] = dr["S_DATE"].ToString();
                drCommission["E_DATE"] = dr["E_DATE"].ToString();

                drCommission["MODI_USER"] = logMsg.MODI_USER;
                drCommission["MODI_DTM"] = Convert.ToDateTime(System.DateTime.Now);

                drCommission["CREATE_USER"] = drCommission["MODI_USER"];
                drCommission["CREATE_DTM"] = drCommission["MODI_DTM"];
                dtCommission.Rows.Add(drCommission);
            }
        }
        return dtCommission;
    }
    
    //廠商指定店組
    private CON02_CSM_SUPPLIER.CSM_SUPPSTOREDataTable doInsertSuppstore(bool bFistListBox)
    {
        CON02_CSM_SUPPLIER.CSM_SUPPSTOREDataTable dtSuppstore = null;

        CON02_CSM_SUPPLIER.CSM_SUPPSTORERow drSuppstore;

        dtSuppstore = new CON02_CSM_SUPPLIER.CSM_SUPPSTOREDataTable();
        ListBox liBox = null;

        if (bFistListBox)
        {
            liBox = StoreList;
        }
        else
        {
            liBox = SList;
        }

        for (int i = 0; i < liBox.Items.Count; i++)
        {
            drSuppstore = dtSuppstore.NewCSM_SUPPSTORERow();
            drSuppstore["STORE_NO"] = liBox.Items[i].ToString().Trim().Substring(0, liBox.Items[i].ToString().Trim().IndexOf('-'));
            drSuppstore["SUPP_ID"] = gropStrUUID;

            drSuppstore["CSM_STORE_ID"] = GuidNo.getUUID();
            drSuppstore["MODI_USER"] = logMsg.MODI_USER;
            drSuppstore["MODI_DTM"] = Convert.ToDateTime(System.DateTime.Now);

            drSuppstore["CREATE_USER"] = drSuppstore["MODI_USER"];
            drSuppstore["CREATE_DTM"] = drSuppstore["MODI_DTM"];
            dtSuppstore.Rows.Add(drSuppstore);
        }

        return dtSuppstore;

    }
    
    //金額級距
    private CON02_CSM_SUPPLIER.CSM_SUP_AMT_LEVELDataTable doInsertSupAmtLevel()
    {
        CON02_CSM_SUPPLIER.CSM_SUP_AMT_LEVELDataTable dtSupAmtLevel = null;

        DataTable dtProd = null;

        dtProd = (DataTable)Session["gvAmtLevel"];

        dtSupAmtLevel = new CON02_CSM_SUPPLIER.CSM_SUP_AMT_LEVELDataTable();
        CON02_CSM_SUPPLIER.CSM_SUP_AMT_LEVELRow drSupAmtLevel;

        if (dtProd.Rows.Count > 0)
        {
            foreach (DataRow dr in dtProd.Rows)
            {
                drSupAmtLevel = dtSupAmtLevel.NewCSM_SUP_AMT_LEVELRow();
                drSupAmtLevel["CSAL_ID"] = dr["CSAL_ID"]; //GuidNo.getUUID();
                drSupAmtLevel["SUPP_ID"] = gropStrUUID;

                drSupAmtLevel["SEQNO"] = 1;
                drSupAmtLevel["S_AMT"] = dr["S_AMT"];
                drSupAmtLevel["E_AMT"] = dr["E_AMT"];

                drSupAmtLevel["COMMISION_RATE"] = dr["COMMISION_RATE"].ToString();
                drSupAmtLevel["S_DATE"] = dr["S_DATE"].ToString();
                drSupAmtLevel["E_DATE"] = dr["E_DATE"].ToString();

                drSupAmtLevel["MODI_USER"] = logMsg.MODI_USER;
                drSupAmtLevel["MODI_DTM"] = Convert.ToDateTime(System.DateTime.Now);

                drSupAmtLevel["CREATE_USER"] = drSupAmtLevel["MODI_USER"];
                drSupAmtLevel["CREATE_DTM"] = drSupAmtLevel["MODI_DTM"];
                dtSupAmtLevel.Rows.Add(drSupAmtLevel);
            }
        }
        return dtSupAmtLevel;
    }
    
    //外部廠商商品編號設定
    private CON02_CSM_SUPPLIER.CSM_SUP_PRODDataTable doInsertProd()
    {
        CON02_CSM_SUPPLIER.CSM_SUP_PRODDataTable dtSupProd = null;

        DataTable dtProd = null;

        dtProd = (DataTable)Session["gvProduct"];

        dtSupProd = new CON02_CSM_SUPPLIER.CSM_SUP_PRODDataTable();
        CON02_CSM_SUPPLIER.CSM_SUP_PRODRow drSupProd;

        if (dtProd.Rows.Count > 0)
        {
            foreach (DataRow dr in dtProd.Rows)
            {
                drSupProd = dtSupProd.NewCSM_SUP_PRODRow();

                DateTime sDate = DateTime.Parse(dr["S_YYMM"].ToString() + "/01");
                DateTime eDate = new DateTime();
                if (!string.IsNullOrEmpty(dr["E_YYMM"].ToString()))
                {
                    //每個月的最後一天
                    eDate = DateTime.Parse(dr["E_YYMM"].ToString() + "/01").AddMonths(1).AddDays(-1);
                }

                drSupProd["CSP_ID"] = GuidNo.getUUID();
                drSupProd["SUPP_ID"] = gropStrUUID;
                drSupProd["SEQNO"] = 1;

                drSupProd["ACCOUNT_CODE"] = dr["ACCOUNT_CODE"].ToString();
                drSupProd["PRODNO"] = dr["PRODNO"].ToString();
                drSupProd["S_YYMM"] = sDate.ToString("yyyyMM");

                if (!string.IsNullOrEmpty(dr["E_YYMM"].ToString()))
                    drSupProd["E_YYMM"] = eDate.ToString("yyyyMM");

                drSupProd["MODI_USER"] = logMsg.MODI_USER;
                drSupProd["MODI_DTM"] = Convert.ToDateTime(System.DateTime.Now);
                drSupProd["CREATE_USER"] = drSupProd["MODI_USER"];
                drSupProd["CREATE_DTM"] = drSupProd["MODI_DTM"];
                dtSupProd.Rows.Add(drSupProd);
            }
        }
        return dtSupProd;
    }
    
    //信用卡手續費率
    private CON02_CSM_SUPPLIER.CSM_CREDIT_CARD_PROCE_RATEDataTable doInsertCard()
    {
        CON02_CSM_SUPPLIER.CSM_CREDIT_CARD_PROCE_RATEDataTable dtSubCard = null;

        DataTable dtCard = null;

        dtCard = (DataTable)Session["gvCard"];

        dtSubCard = new CON02_CSM_SUPPLIER.CSM_CREDIT_CARD_PROCE_RATEDataTable();
        CON02_CSM_SUPPLIER.CSM_CREDIT_CARD_PROCE_RATERow drCard;

        if (dtCard.Rows.Count > 0)
        {
            foreach (DataRow dr in dtCard.Rows)
            {
                drCard = dtSubCard.NewCSM_CREDIT_CARD_PROCE_RATERow();

                drCard["CCPR_ID"] = GuidNo.getUUID();
                drCard["SUPP_ID"] = gropStrUUID;

                drCard["CREDIT_CARD_TYPE_ID"] = dr["TYPE"];
                drCard["CHARGE_RATE"] = dr["RATE"];

                if (!string.IsNullOrEmpty(gropCooSDate))
                    drCard["S_DATE"] = gropCooSDate;

                if (!string.IsNullOrEmpty(gropCooEDate))
                    drCard["E_DATE"] = gropCooEDate;

                drCard["MODI_USER"] = logMsg.MODI_USER;
                drCard["MODI_DTM"] = Convert.ToDateTime(System.DateTime.Now);

                drCard["CREATE_USER"] = drCard["MODI_USER"];
                drCard["CREATE_DTM"] = drCard["MODI_DTM"];
                dtSubCard.Rows.Add(drCard);
            }
        }
        return dtSubCard;
    }

    #endregion

    /// <summary>
    /// 控制所有Grid 的Delete 方法。
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    /// <param name="gridName"></param>
    /// <param name="strSessionName"></param>
    /// <param name="strSQLDeletekey"></param> 要delete sql 的key 值
    private void subControlGridDelete(object sender,
        EventArgs e,
        ASPxGridView gridName,
        string strSessionName,
        string strSQLDeletekey)
    {
        List<object> gvPKValues = gridName.GetSelectedFieldValues(gridName.KeyFieldName);
        string pkFName = gridName.KeyFieldName;

        DataTable dtSYS;

        if (Session[strSessionName] == null || Session[strSessionName].ToString() == "")
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
                string strKey = key.ToString();
                DataRow drSYS = dtSYS.Select(strSQLDeletekey + "='" + key.ToString() + "'")[0];
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
        if (Session[gridName.ID] == null || Session[gridName.ID].ToString() == "")
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
            if (dr[PkColumnName].ToString().Trim() == 
                DataInp.Find(delegate(UpdateDataInp p) {
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

    #region //check DataTable 內的資料不可重覆

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
        DateTime ssDate = new DateTime();//起始時間
        DateTime seDate = new DateTime();//結束時間

        if (CheckData.IsNewRowEditing)
        {
            drING = dtSYS.Select();
        }
        else
        {//此為編輯資料，排除自已Session 的資料

            drING = dtSYS.Select(" " + CheckData.PkName + " <> '" + CheckData.PkData + "'");
        }
        
        //for (int i = 0; i < dtSYS.Rows.Count; i++)
        for (int i = 0; i < drING.Length; i++)
        {
            DataRow dr = drING[i];
            if (retVal.State.IsSuccess == true)
            {//如果沒有錯誤就check

                if (CheckData.SessionName == "gvProduct" &&
                    CheckData.SessionName != "gvCard")//判斷 //信用卡gvCard 沒有時間起迄
                {                   
                    ssDate = DateTime.Parse(StringUtil.CStr(dr["S_YYMM"]).Trim());

                    if(string.IsNullOrEmpty(StringUtil.CStr(dr["E_YYMM"])))
                        seDate = DateTime.Parse("9999/12/31");
                    else
                        seDate = DateTime.Parse(StringUtil.CStr(dr["E_YYMM"]).Trim());
                }
                else if (CheckData.SessionName != "gvCard")
                {
                    ssDate = DateTime.Parse( StringUtil.CStr(dr["S_DATE"]).Trim());

                    if (string.IsNullOrEmpty(StringUtil.CStr(dr["E_DATE"])))
                        seDate = DateTime.Parse("9999/12/31");
                    else
                        seDate = DateTime.Parse( StringUtil.CStr(dr["E_DATE"]).Trim());
                }

                if (CheckData.SessionName != "gvCard")
                {
                    //if (ssDate == CheckData.SDate &&
                    //    seDate == CheckData.EDate
                    //    )//信用卡gvCard 沒有時間起迄
                    //{

                    //    retVal.State.IsSuccess = false;
                    //    retVal.State.ErrorMessage = "時間區間重覆!";
                    //}

                    //取最大的Date
                    DateTime dtMaxDate = ssDate > DateTime.Parse(CheckData.SDate) ? ssDate : DateTime.Parse(CheckData.SDate);
                    //取最小的Date
                    DateTime dtMinDate = seDate < DateTime.Parse(CheckData.EDate) ? seDate : DateTime.Parse(CheckData.EDate);
                    if (dtMaxDate <= dtMinDate)
                    {
                        retVal.State.IsSuccess = false;
                        retVal.State.ErrorMessage = "時間區間重覆!";
                    }

                }

                if (retVal.State.IsSuccess == false)
                    continue;//已經有錯誤了

                if (!string.IsNullOrEmpty(CheckData.ProductNO))
                {
                    if (dr["PRODNO"].ToString().Trim() == CheckData.ProductNO)
                    {
                        retVal.State.IsSuccess = false;
                        retVal.State.ErrorMessage = "商品料號重覆!";
                    }
                }

                if (retVal.State.IsSuccess == false)
                    continue;//已經有錯誤了

                if (!string.IsNullOrEmpty(CheckData.CardType))
                {
                    if (dr["TYPE"].ToString().Trim() == CheckData.CardType)
                    {
                        retVal.State.IsSuccess = false;
                        retVal.State.ErrorMessage = "信用卡別重覆!";
                    }
                }

                if (retVal.State.IsSuccess == false)
                    continue;//已經有錯誤了
            }
        }
        return retVal;
    }

    /// <summary>
    /// Check Grid View 內的欄位是否有重覆
    /// </summary>
    /// <param name="pChDate"></param>
    /// <param name="gv"></param>
    /// <returns></returns>
    public CheckData CheckCommssion(CheckData pChDate, ASPxGridView gv)
    {
        CheckData retVal = new CheckData();
        retVal.State.IsSuccess = true;

        CheckMethod_PageHelper Ck = new CheckMethod_PageHelper();//check Method
        pChDate.SessionName = gv.ID;// "gvCommission";

        if (gv.IsEditing)
        {
            if (string.IsNullOrEmpty(pChDate.SDate))
            {
                retVal.State.IsSuccess = false;
                retVal.State.ErrorMessage = "起始月份不允許空值，請重新輸入!!";
                return retVal;
            }

            CheckData inCk = CheckRedundant(pChDate);//Check 的羅輯 [ Session]  資料不可重覆
            if (!inCk.State.IsSuccess)
            {//資料重覆

                retVal.State.IsSuccess = false;
                retVal.State.ErrorMessage = inCk.State.ErrorMessage;
                return retVal;
            }

            if (!Ck.CheckSdateEdate<string, string>(pChDate.SDate, pChDate.EDate))
            {
                retVal.State.IsSuccess = false;
                retVal.State.ErrorMessage = Ck.State.ErrorMessage;
                return retVal;
            }

        }

        return retVal;
    }
    
    #endregion

    #region 所有的小class

    //合作店組設定
    public class StoreListInp
    { 
        /// <summary>
        /// 指定目標List Box
        /// </summary>
        public string strSelect{get;set;}

        /// <summary>
        /// 要新增的資料的 UUID
        /// </summary>
        public string suppID { get; set; }
    }

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

        ///// <summary>
        ///// PK 數值
        ///// </summary>
        //public string PkValue { get; set; }

        ///// <summary>
        ///// Pk 名字
        ///// </summary>
        //public string PkName { get; set; }
    }

    #endregion
}
