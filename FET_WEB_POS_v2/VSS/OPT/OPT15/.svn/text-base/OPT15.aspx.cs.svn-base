using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Advtek.Utility;
using System.Collections;
using DevExpress.Web;
using DevExpress.Web.ASPxGridView;
using DevExpress.Web.ASPxPager;
using FET.POS.Model.Facade.FacadeImpl;
using FET.POS.Model.Common;
using DevExpress.Web.ASPxEditors;

using FET.POS.Model.DTO;
using FET.POS.Model.Facade;
using System.Web.UI.HtmlControls;
using DevExpress.Web.ASPxClasses;

public partial class VSS_OPT_OPT15 : BasePage
{
    private string QryUSER;

    protected void Page_Load(object sender, EventArgs e)
    {
        QryUSER = logMsg.OPERATOR;
        if (!IsPostBack && !Page.IsCallback)
        {
            //取得空的資料表
            gvMaster.DataSource = new OPT15_Facade().Query_HgConvertibleGiftM(this.txtSDate_S.Text, this.txtSDate_E.Text, "xxx", "xxx", "xxx");
            gvMaster.DataBind();
        }
        else
        {
            if (Request["__EVENTARGUMENT"] == "AAA")
            {
                this.txtBatchNO.Text = StringUtil.CStr(Session["UploadBatchNo"]);
                if (!string.IsNullOrEmpty(this.txtBatchNO.Text))
                {
                    this.lblImportStatus.Text = "名單已匯入";
                }
                else
                {
                    this.lblImportStatus.Text = "名單尚未匯入";
                }
            }
        }
    }

    private void BindZoneType()
    {
        ASPxComboBox ddlZone = gvDetail.FindChildControl<ASPxComboBox>("ddlZone");
        if (ddlZone != null)
        {
            ddlZone.TextField = "ZONE_NAME";
            ddlZone.ValueField = "ZONE";
            ddlZone.DataSource = Common_PageHelper.getZone(true);
            ddlZone.DataBind();
            ddlZone.SelectedIndex = 0;
        }
    }

    protected void BindMasterData()
    {
        gvMaster.DataSource = new OPT15_Facade().Query_HgConvertibleGiftM(this.txtSDate_S.Text,
            this.txtSDate_E.Text, this.txtPartNumberOfDiscountS.Text, this.txtPartNumberOfDiscountE.Text, this.txtDiscountName.Text);
        gvMaster.DataBind();
        gvMaster.Selection.UnselectAll();
        gvDetail.Selection.UnselectAll();
    }

    private void BinDetailData()
    {
        if (gvMaster.FocusedRowIndex > -1)
        {
            string ACTIVITY_ID = StringUtil.CStr(gvMaster.GetRowValues(gvMaster.FocusedRowIndex, gvMaster.KeyFieldName));
            string cbMEMBER_CHECK_FLAG = StringUtil.CStr(gvMaster.GetRowValues(gvMaster.FocusedRowIndex, "MEMBER_CHECK_FLAG"));

            gvDetail.DataSource = new OPT15_Facade().Query_HgConvertGiftD(ACTIVITY_ID);
            gvDetail.DataBind();

            //1. 開始日期還沒到，都可以編輯、刪除
            //2. 開始日期到了之後就不能編輯、刪除

            //MantisBT 筆記回覆 0000083 開始日期(可為系統日) #414
            DateTime S_Date = Convert.ToDateTime(gvMaster.GetRowValues(gvMaster.FocusedRowIndex, "S_DATE"));
            if (S_Date.AddDays(1) < DateTime.Now || cbMEMBER_CHECK_FLAG == "1")
            {
                gvDetail.Enabled = false;
                gvDetail.PagerBarEnabled = true;
            }
            else
            {
                gvDetail.Enabled = true;
                gvDetail.PagerBarEnabled = true;
            }
        }
        else
        {
            gvDetail.DataSource = new DataTable();
            gvDetail.DataBind();
        }
        gvDetail.Selection.UnselectAll();

    }

    private void SaveTempData()
    {
        DataTable dt = new DataTable();
        dt.Columns.Add("ACTIVITY_NO", typeof(string));
        dt.Columns.Add("ACTIVITY_NAME", typeof(string));
        dt.Columns.Add("MEMBER_CHECK_FLAG", typeof(bool));

        DataRow dr = dt.NewRow();

        PopupControl pop1 = (PopupControl)gvMaster.FindEditRowCellTemplateControl((GridViewDataColumn)gvMaster.Columns["ACTIVITY_NO"], "ACTIVITY_NO");
        ASPxTextBox txtDiscount = gvMaster.FindEditRowCellTemplateControl((GridViewDataColumn)gvMaster.Columns["ACTIVITY_NAME"], "txtDiscountName") as ASPxTextBox;
        ASPxDateEdit txtS_DATE = gvMaster.FindEditRowCellTemplateControl((GridViewDataColumn)gvMaster.Columns["S_DATE"], "txtS_DATE") as ASPxDateEdit;
        ASPxDateEdit txtE_DATE = gvMaster.FindEditRowCellTemplateControl((GridViewDataColumn)gvMaster.Columns["E_DATE"], "txtE_DATE") as ASPxDateEdit;
        ASPxTextBox txtDIVIDABLE_POINT = gvMaster.FindEditRowCellTemplateControl((GridViewDataColumn)gvMaster.Columns["DIVIDABLE_POINT"], "txtDIVIDABLE_POINT") as ASPxTextBox;
        ASPxTextBox txtUSE_COUNT = gvMaster.FindEditRowCellTemplateControl((GridViewDataColumn)gvMaster.Columns["USE_COUNT"], "txtUSE_COUNT") as ASPxTextBox;
        ASPxComboBox ddlTYPE = gvMaster.FindEditRowCellTemplateControl((GridViewDataColumn)gvMaster.Columns["TYPE"], "ddlType") as ASPxComboBox;
        ASPxCheckBox cbMEMBER_CHECK_FLAG = gvMaster.FindEditRowCellTemplateControl((GridViewDataColumn)gvMaster.Columns["MEMBER_CHECK_FLAG"], "cbMEMBER_CHECK_FLAG") as ASPxCheckBox;
        PopupControl txtPRODNO = gvMaster.FindEditRowCellTemplateControl((GridViewDataColumn)gvMaster.Columns["PRODNO"], "txtPRODNO") as PopupControl;

        dr["ACTIVITY_NO"] = pop1.popTextBox.Text;
        dr["ACTIVITY_NAME"] = txtDiscount.Text;
        dr["MEMBER_CHECK_FLAG"] = cbMEMBER_CHECK_FLAG.Checked;
        dt.Rows.Add(dr);

        ViewState["TempData"] = dt;
    }

    private void IsImported()
    {
        this.lblImportStatus.Text = "名單尚未匯入";
        if (this.gvMaster.FocusedRowIndex > -1)
        {
            int DataCount = new OPT15_Facade().Query_HgConvertMemberList(StringUtil.CStr(this.gvMaster.GetRowValues(this.gvMaster.FocusedRowIndex, "ACTIVITY_NO")));

            if (DataCount > 0)
            {
                this.lblImportStatus.Text = "名單已匯入";
            }
        }
    }

    private void IsEffect()
    {
        this.btnImport.ClientVisible = false;

        if (this.gvMaster.FocusedRowIndex > -1)
        {
            string sid = StringUtil.CStr(gvMaster.GetRowValues(gvMaster.FocusedRowIndex, "ACTIVITY_ID"));
            int DataCount = new OPT15_Facade().Query_HgConvertMemberListByID(sid);
            if (DataCount > 0)
            {
                this.btnImport.ClientVisible = true;
            }
        }
    }
    
    protected void cbMEMBER_CHECK_FLAG_CheckedChanged(object sender, System.EventArgs e)
    {
        ASPxCheckBox cb = gvMaster.FindEditRowCellTemplateControl((GridViewDataColumn)gvMaster.Columns["MEMBER_CHECK_FLAG"], "cbMEMBER_CHECK_FLAG") as ASPxCheckBox;
        if (cb.Checked)
        {
            if (!gvMaster.IsNewRowEditing && this.gvDetail.VisibleRowCount > 0)
            {
                cb.Checked = false;
                //彈跳視窗
                ScriptManager.RegisterClientScriptBlock(this, GetType(), "AlertMessage", "alert('已指定門市，故無法勾選');", true);

            }
            else
            {
                this.ASPxPageControl1.ActiveTabIndex = 1;
                ContentControl1.Enabled = false;
                //彈跳視窗
                //ScriptManager.RegisterClientScriptBlock(this, GetType(), "AlertMessage", "alert('此活動須檢核名單，不指定門市');", true);
            }
        }
        else
        {
            ContentControl1.Enabled = true;
        }
    }

    protected void ASPxPageControl1_ActiveTabChanged(object source, DevExpress.Web.ASPxTabControl.TabControlEventArgs e)
    {
        int a = this.ASPxPageControl1.ActiveTabIndex;
        gvDetail.Selection.UnselectAll();
        gvMaster.CancelEdit();
        gvDetail.CancelEdit();
        switch (a)
        {
            case 0:
                bool cbTemp;
                if (gvMaster.IsEditing)
                {
                    ASPxCheckBox cbMEMBER_CHECK_FLAG = gvMaster.FindEditRowCellTemplateControl((GridViewDataColumn)gvMaster.Columns["MEMBER_CHECK_FLAG"], "cbMEMBER_CHECK_FLAG") as ASPxCheckBox;
                    cbTemp = cbMEMBER_CHECK_FLAG.Checked;
                }
                else
                {
                    cbTemp = StringUtil.CStr(gvMaster.GetRowValues(gvMaster.FocusedRowIndex, "MEMBER_CHECK_FLAG")) == "1" ? true : false;
                }
                // 

                if (cbTemp)
                {
                    ContentControl1.Enabled = false;
                    //彈跳視窗
                    ScriptManager.RegisterClientScriptBlock(this, GetType(), "AlertMessage", "alert('此活動須檢核名單，不指定門市');", true);
                }
                else
                {
                    ContentControl1.Enabled = true;
                }
                BinDetailData();
                break;

            case 1:
                IsImported();
                IsEffect();
                break;

            default:
                break;
        }
        BindZoneType();
    }

    protected void ASPxPageControl1_PreRender(object sender, EventArgs e)
    {
        BindZoneType();
    }

    #region Button 觸發事件

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        this.gvMaster.Visible = true;
        BindMasterData();
        this.gvMaster.FocusedRowIndex = -1;
        gvMaster.PageIndex = 0;
        BinDetailData();
        gvDetail.PageIndex = 0;
        this.ASPxPageControl1.Visible = false;
    }

    protected void btnAddM_Click(object sender, EventArgs e)
    {
        if (!gvMaster.IsEditing)
        {
            gvMaster.Selection.UnselectAll();
            gvMaster.AddNewRow();
            this.gvMaster.FocusedRowIndex = -1;
            BinDetailData();
            this.ASPxPageControl1.Visible = false;
            
        }
    }

    protected void btnDeleteM_Click(object sender, EventArgs e)
    {
        OPT15_HgConvertibleGiftM_DTO OPT15_DTO = new OPT15_HgConvertibleGiftM_DTO();
        OPT15_HgConvertibleGiftM_DTO.HG_CONVERTIBLE_GIFT_MDataTable dt = OPT15_DTO.HG_CONVERTIBLE_GIFT_M;
        OPT15_Facade facade = new OPT15_Facade();

        List<object> keyValues = this.gvMaster.GetSelectedFieldValues(gvMaster.KeyFieldName);
        foreach (string key in keyValues)
        {
            //DataTable dtDetail = facade.Query_HgConvertGiftD(key, "All");

            // if (dtDetail.Rows.Count > 0)
            // {
            //     gvMaster.FindChildControl<ASPxLabel>("lblError").Text = "選取的項目中還包含子資料，所以無法刪除！";
            //     return;
            // }
            // else
            // {
            OPT15_HgConvertibleGiftM_DTO.HG_CONVERTIBLE_GIFT_MRow dr = dt.NewHG_CONVERTIBLE_GIFT_MRow();

            DataRow dirtyDr = OPT15_PageHelper.Query_HgConvertibleGiftM_ByKey(key).Rows[0];

            dr.ACTIVITY_ID = StringUtil.CStr(dirtyDr["ACTIVITY_ID"]);
            dr.ACTIVITY_NO = StringUtil.CStr(dirtyDr["ACTIVITY_NO"]);
            dr.ACTIVITY_NAME = StringUtil.CStr(dirtyDr["ACTIVITY_NAME"]);
            dr.S_DATE = Convert.ToDateTime(dirtyDr["S_DATE"]);
            string strEDate = StringUtil.CStr(dirtyDr["E_DATE"]);
            if (!string.IsNullOrEmpty(strEDate))
            {
                dr.E_DATE = Convert.ToDateTime(strEDate);
            }
            string strDIVIDABLE_POINT = StringUtil.CStr(dirtyDr["DIVIDABLE_POINT"]);
            if (!string.IsNullOrEmpty(strDIVIDABLE_POINT))
            {
                dr.DIVIDABLE_POINT = Convert.ToDecimal(strDIVIDABLE_POINT);
            }
            dr.CREATE_USER = StringUtil.CStr(dirtyDr["CREATE_USER"]);
            dr.CREATE_DTM = Convert.ToDateTime(dirtyDr["CREATE_DTM"]);
            dr.USE_COUNT = Convert.ToDecimal(dirtyDr["USE_COUNT"]);
            dr.MODI_USER = this.QryUSER;
            dr.MODI_DTM = System.DateTime.Today;
            dr.TYPE = StringUtil.CStr(dirtyDr["TYPE"]);
            dr.MEMBER_CHECK_FLAG = StringUtil.CStr(dirtyDr["MEMBER_CHECK_FLAG"]);
            dr.PRODNO = StringUtil.CStr(dirtyDr["PRODNO"]);
            dr.DEL_FLAG = "Y";

            dt.Rows.Add(dr);

            OPT15_DTO.AcceptChanges();
            //}
        }

        if (OPT15_DTO.HG_CONVERTIBLE_GIFT_M.Rows.Count > 0)
        {
            //更新資料庫
            facade.Delete_HgConvertibleGiftM(OPT15_DTO);

            BindMasterData();
            this.gvMaster.FocusedRowIndex = -1;
            this.ASPxPageControl1.Visible = false;
        }

    }

    protected void btnImportM_Click(object sender, EventArgs e)
    {
        Response.Redirect("../OPT16.aspx");
    }

    protected void btnAddD_Click(object sender, EventArgs e)
    {

        if (gvMaster.FocusedRowIndex > -1)
        {
            if (!gvDetail.IsEditing)
            {
                gvDetail.AddNewRow();
                gvDetail.Selection.UnselectAll();
               
            }

        }
    }

    protected void btnDeleteD_Click(object sender, EventArgs e)
    {
        OPT15_HgConvertibleGiftM_DTO OPT15_DTO = new OPT15_HgConvertibleGiftM_DTO();
        OPT15_HgConvertibleGiftM_DTO.HG_CONVERT_GIFT_DDataTable dt = OPT15_DTO.HG_CONVERT_GIFT_D;
        OPT15_Facade facade = new OPT15_Facade();

        List<object> keyValues = this.gvDetail.GetSelectedFieldValues(gvDetail.KeyFieldName);
        foreach (string key in keyValues)
        {
            OPT15_HgConvertibleGiftM_DTO.HG_CONVERT_GIFT_DRow dr = dt.NewHG_CONVERT_GIFT_DRow();

            DataRow dirtyDr = OPT15_PageHelper.Query_HgConvertibleGiftD_ByKey(key);

            dr.SID = StringUtil.CStr(dirtyDr["SID"]);
            dr.ACTIVITY_ID = StringUtil.CStr(dirtyDr["ACTIVITY_ID"]);
            dr.CREATE_USER = StringUtil.CStr(dirtyDr["CREATE_USER"]);
            dr.CREATE_DTM = Convert.ToDateTime(dirtyDr["CREATE_DTM"]);
            dr.MODI_USER = this.QryUSER;
            dr.MODI_DTM = System.DateTime.Today;
            dr.STORE_NO = StringUtil.CStr(dirtyDr["STORE_NO"]);
            dr.DEL_FLAG = "Y";

            dt.Rows.Add(dr);

            OPT15_DTO.AcceptChanges();
        }

        if (OPT15_DTO.HG_CONVERT_GIFT_D.Rows.Count > 0)
        {
            //更新資料庫
            facade.Delete_HgConvertibleGiftD(OPT15_DTO);

            BinDetailData();
        }

    }

    protected void btnConfirm_Click(object sender, EventArgs e)
    {
        if (gvMaster.FocusedRowIndex > -1)
        {
            string ACTIVITY_ID = StringUtil.CStr(gvMaster.GetRowValues(gvMaster.FocusedRowIndex, gvMaster.KeyFieldName));
            ASPxComboBox ddlZone = gvDetail.FindTitleTemplateControl("ddlZone") as ASPxComboBox;
            if (ddlZone.SelectedItem != null)
            {
                string strZone = StringUtil.CStr(ddlZone.SelectedItem.Value);
                //if (!string.IsNullOrEmpty(strZone))  //**2011/03/09 Tina：註解此判斷，因為選取"ALL"，表示所有區域都加入
               // {
                    new OPT15_Facade().AddNew_HgConvertGiftD(ACTIVITY_ID, this.QryUSER, StringUtil.CStr(ddlZone.SelectedItem.Value));
                    BinDetailData();
                    gvDetail.PageIndex = 0;
               // }
            }
            this.gvDetail.Selection.UnselectAll();
        }
    }

    #endregion

    #region gvMaster 觸發事件

    protected void gvMaster_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
    {

            OPT15_HgConvertibleGiftM_DTO OPT15_DTO = new OPT15_HgConvertibleGiftM_DTO();
            OPT15_HgConvertibleGiftM_DTO.HG_CONVERTIBLE_GIFT_MDataTable dt = OPT15_DTO.HG_CONVERTIBLE_GIFT_M;
            OPT15_HgConvertibleGiftM_DTO.HG_CONVERTIBLE_GIFT_MRow dr = dt.NewHG_CONVERTIBLE_GIFT_MRow();

            OPT15_Facade facade = new OPT15_Facade();

            dr.ACTIVITY_ID = GuidNo.getUUID();

            PopupControl pop1 = (PopupControl)gvMaster.FindEditRowCellTemplateControl((GridViewDataColumn)gvMaster.Columns["ACTIVITY_NO"], "ACTIVITY_NO");
            dr.ACTIVITY_NO = pop1.popTextBox.Text;
            string txtDiscount = ((ASPxTextBox)gvMaster.FindEditRowCellTemplateControl((GridViewDataColumn)gvMaster.Columns["ACTIVITY_NAME"], "txtDiscountName")).Text;
            dr.ACTIVITY_NAME = txtDiscount;
            dr.S_DATE = Convert.ToDateTime(((ASPxDateEdit)gvMaster.FindEditRowCellTemplateControl((GridViewDataColumn)gvMaster.Columns["S_DATE"], "txtS_DATE")).Text);
            string strEDate = ((ASPxDateEdit)gvMaster.FindEditRowCellTemplateControl((GridViewDataColumn)gvMaster.Columns["E_DATE"], "txtE_DATE")).Text.Trim();
            if (!string.IsNullOrEmpty(strEDate))
            {
                dr.E_DATE = Convert.ToDateTime(strEDate);
            }
            string strDIVIDABLE_POINT = ((ASPxTextBox)gvMaster.FindEditRowCellTemplateControl((GridViewDataColumn)gvMaster.Columns["DIVIDABLE_POINT"], "txtDIVIDABLE_POINT")).Text.Trim();
            if (!string.IsNullOrEmpty(strDIVIDABLE_POINT))
            {
                dr.DIVIDABLE_POINT = Convert.ToDecimal(strDIVIDABLE_POINT);
            }
            dr.CREATE_USER = this.QryUSER;
            dr.CREATE_DTM = System.DateTime.Now;
            dr.USE_COUNT = Convert.ToInt32(((ASPxTextBox)gvMaster.FindEditRowCellTemplateControl((GridViewDataColumn)gvMaster.Columns["USE_COUNT"], "txtUSE_COUNT")).Text);
            dr.MODI_USER = this.QryUSER;
            dr.MODI_DTM = System.DateTime.Now;
            dr.TYPE = StringUtil.CStr(((ASPxComboBox)gvMaster.FindEditRowCellTemplateControl((GridViewDataColumn)gvMaster.Columns["TYPE"], "ddlType")).Value);  // 1:點數 2:商品
            ASPxCheckBox cbMEMBER_CHECK_FLAG = gvMaster.FindEditRowCellTemplateControl((GridViewDataColumn)gvMaster.Columns["MEMBER_CHECK_FLAG"], "cbMEMBER_CHECK_FLAG") as ASPxCheckBox;
            dr.MEMBER_CHECK_FLAG = cbMEMBER_CHECK_FLAG.Checked ? "1" : "0";                 // 1:是 0:否
            dr.PRODNO = ((PopupControl)gvMaster.FindEditRowCellTemplateControl((GridViewDataColumn)gvMaster.Columns["PRODNO"], "txtPRODNO")).Text;
            dr.DEL_FLAG = "N";

            dt.Rows.Add(dr);
            OPT15_DTO.AcceptChanges();

            //更新資料庫
            facade.AddNewOne_HgConvertibleGiftM(OPT15_DTO);

            //名單檢核勾選時將下面頁簽跳至名單頁簽
            if (!cbMEMBER_CHECK_FLAG.Checked)
                ASPxPageControl1.ActiveTabIndex = 1;
        gvMaster.SettingsBehavior.ProcessFocusedRowChangedOnServer = true;

        gvMaster.CancelEdit();
        e.Cancel = true;

        BindMasterData();
    }

    protected void gvMaster_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
    {

        OPT15_HgConvertibleGiftM_DTO OPT15_DTO = new OPT15_HgConvertibleGiftM_DTO();
        OPT15_HgConvertibleGiftM_DTO.HG_CONVERTIBLE_GIFT_MDataTable dt = OPT15_DTO.HG_CONVERTIBLE_GIFT_M;
        OPT15_HgConvertibleGiftM_DTO.HG_CONVERTIBLE_GIFT_MRow dr = dt.NewHG_CONVERTIBLE_GIFT_MRow();
        OPT15_Facade facade = new OPT15_Facade();

        DataRow dirtyDr = OPT15_PageHelper.Query_HgConvertibleGiftM_ByKey(StringUtil.CStr(e.Keys[gvMaster.KeyFieldName])).Rows[0];

        dr.ACTIVITY_ID = StringUtil.CStr(dirtyDr["ACTIVITY_ID"]);

        PopupControl pop1 = (PopupControl)gvMaster.FindEditRowCellTemplateControl((GridViewDataColumn)gvMaster.Columns["ACTIVITY_NO"], "ACTIVITY_NO");
        string txtDiscount = ((ASPxTextBox)gvMaster.FindEditRowCellTemplateControl((GridViewDataColumn)gvMaster.Columns["ACTIVITY_NAME"], "txtDiscountName")).Text;
        string strEDate = ((ASPxDateEdit)gvMaster.FindEditRowCellTemplateControl((GridViewDataColumn)gvMaster.Columns["E_DATE"], "txtE_DATE")).Text.Trim();
        string strDIVIDABLE_POINT = ((ASPxTextBox)gvMaster.FindEditRowCellTemplateControl((GridViewDataColumn)gvMaster.Columns["DIVIDABLE_POINT"], "txtDIVIDABLE_POINT")).Text.Trim();
        ASPxCheckBox cbMEMBER_CHECK_FLAG = gvMaster.FindEditRowCellTemplateControl((GridViewDataColumn)gvMaster.Columns["MEMBER_CHECK_FLAG"], "cbMEMBER_CHECK_FLAG") as ASPxCheckBox;
        PopupControl Prodno = gvMaster.FindEditRowCellTemplateControl((GridViewDataColumn)gvMaster.Columns["PRODNO"], "txtPRODNO") as PopupControl;

        dr.ACTIVITY_ID = StringUtil.CStr(e.Keys[gvMaster.KeyFieldName]);
        dr.ACTIVITY_NO = pop1.popTextBox.Text;
        dr.ACTIVITY_NAME = txtDiscount;
        dr.S_DATE = Convert.ToDateTime(((ASPxDateEdit)gvMaster.FindEditRowCellTemplateControl((GridViewDataColumn)gvMaster.Columns["S_DATE"], "txtS_DATE")).Text);
        if (!string.IsNullOrEmpty(strEDate))
        {
            dr["E_DATE"] = Convert.ToDateTime(strEDate);
        }
        else
        {
            dr["E_DATE"] = DBNull.Value;
        }
        if (!string.IsNullOrEmpty(strDIVIDABLE_POINT))
        {
            dr.DIVIDABLE_POINT = Convert.ToDecimal(strDIVIDABLE_POINT);
        }
        dr.CREATE_USER = StringUtil.CStr(dirtyDr["CREATE_USER"]);
        dr.CREATE_DTM = Convert.ToDateTime(dirtyDr["CREATE_DTM"]);
        dr.USE_COUNT = Convert.ToInt32(((ASPxTextBox)gvMaster.FindEditRowCellTemplateControl((GridViewDataColumn)gvMaster.Columns["USE_COUNT"], "txtUSE_COUNT")).Text);
        dr.MODI_USER = this.QryUSER;
        dr.MODI_DTM = System.DateTime.Now;
        dr.TYPE = StringUtil.CStr(((ASPxComboBox)gvMaster.FindEditRowCellTemplateControl((GridViewDataColumn)gvMaster.Columns["TYPE"], "ddlType")).Value);  // 1:點數 2:商品
        dr.MEMBER_CHECK_FLAG = cbMEMBER_CHECK_FLAG.Checked ? "1" : "0";                 // 1:是 0:否
        dr.PRODNO = Prodno.Text;
        dr.DEL_FLAG = StringUtil.CStr(dirtyDr["DEL_FLAG"]);

        dt.Rows.Add(dr);
        OPT15_DTO.AcceptChanges();

        //更新資料庫
        facade.UpdateOne_HgConvertibleGiftM(OPT15_DTO);

        //名單檢核勾選時將下面頁簽跳至名單頁簽
        if (!cbMEMBER_CHECK_FLAG.Checked)
            ASPxPageControl1.ActiveTabIndex = 1;
        gvMaster.SettingsBehavior.ProcessFocusedRowChangedOnServer = true;
        gvMaster.CancelEdit();
        e.Cancel = true;
        BindMasterData();
        BinDetailData();
    }

    protected void gvMaster_PageIndexChanged(object sender, EventArgs e)
    {
        BindMasterData();
        ASPxPageControl1.Visible = false;
    }

    protected void gvMaster_FocusedRowChanged(object sender, EventArgs e)
    {
        if (gvMaster.FocusedRowIndex > -1)
        {
            int focusedRowIndex = gvMaster.FocusedRowIndex;
            BinDetailData();
            IsImported();
            IsEffect();
            ASPxPageControl1.Visible = true;
            ASPxPageControl1.ActiveTabIndex = 0;
            //ASPxPopupControl1.ContentUrl = "~/VSS/OPT/OPT16.aspx?FUNCID=OPT15&UUID=" + gvMaster.GetRowValues(focusedRowIndex, gvMaster.KeyFieldName);

            //**2011/04/27 Tina：傳遞參數時，要先以加密處理。
            string encryptUrl = Utils.Param_Encrypt("FUNCID=OPT15&UUID=" + gvMaster.GetRowValues(focusedRowIndex, gvMaster.KeyFieldName));
            ASPxPopupControl1.ContentUrl = string.Format("~/VSS/OPT/OPT16.aspx?Param={0}", encryptUrl);
        
            gvDetail.FocusedRowIndex = -1;
            gvDetail.PageIndex = 0;
            gvDetail.Selection.UnselectAll();
            gvDetail.CancelEdit();
        }
    }

    protected void gvMaster_HtmlRowPrepared(object sender, ASPxGridViewTableRowEventArgs e)
    {
        e.Row.Attributes["canSelect"] = "true";

        if (e.RowType == GridViewRowType.Data)
        {
            if (gvMaster.FocusedRowIndex != -1)
            {
                List<object> keyValues = this.gvMaster.GetSelectedFieldValues(gvMaster.KeyFieldName);
                foreach (string key in keyValues)
                {
                    if (key == StringUtil.CStr(e.GetValue(gvMaster.KeyFieldName)))
                    {
                        if (key == StringUtil.CStr(gvMaster.GetRowValues(gvMaster.FocusedRowIndex, gvMaster.KeyFieldName)))
                        {
                            e.Row.BackColor = System.Drawing.ColorTranslator.FromHtml("#999999");
                            e.Row.ForeColor = System.Drawing.ColorTranslator.FromHtml("#FFFFFF");
                        }
                        else
                        {
                            e.Row.BackColor = System.Drawing.ColorTranslator.FromHtml("#FFFFFF");
                            e.Row.ForeColor = System.Drawing.ColorTranslator.FromHtml("#000000");
                        }

                    }
                }
            }

            if (e.VisibleIndex > -1)
            {
                string date = StringUtil.CStr(gvMaster.GetRowValues(e.VisibleIndex, "S_DATE"));
                //MantisBT 筆記回覆 0000083 開始日期(可為系統日) #414
                if (!string.IsNullOrEmpty(date) && Convert.ToDateTime(date).AddDays(1) < DateTime.Now)
                {
                    e.Row.Attributes["canSelect"] = "false";
                }
            }

            GridViewDataColumn colTYPE = new GridViewDataColumn();
            colTYPE = (GridViewDataColumn)((ASPxGridView)sender).Columns["TYPE"];
            ASPxLabel lblTYPE = gvMaster.FindRowCellTemplateControl(e.VisibleIndex, colTYPE, "lblTYPE") as ASPxLabel;
            if (lblTYPE != null)
            {
                lblTYPE.Text = StringUtil.CStr(e.GetValue("TYPE")) == "1" ? "點數" : "商品";
            }
            GridViewDataColumn colMEMBER_CHECK_FLAG = new GridViewDataColumn();
            colMEMBER_CHECK_FLAG = (GridViewDataColumn)((ASPxGridView)sender).Columns["MEMBER_CHECK_FLAG"];
            ASPxCheckBox cbMEMBER_CHECK_FLAGRow = gvMaster.FindRowCellTemplateControl(e.VisibleIndex, colMEMBER_CHECK_FLAG, "cbMEMBER_CHECK_FLAG") as ASPxCheckBox;
            if (cbMEMBER_CHECK_FLAGRow != null)
            {
                cbMEMBER_CHECK_FLAGRow.Checked = StringUtil.CStr(e.GetValue("MEMBER_CHECK_FLAG")) == "0" ? false : true;
            }
        }

    }

    protected void gvMaster_HtmlEditFormCreated(object sender, ASPxGridViewEditFormEventArgs e)
    {
        //BinDiscountProduct(); //繫結 折扣商品
        ASPxCheckBox chk = gvMaster.FindEditRowCellTemplateControl((GridViewDataColumn)gvMaster.Columns["MEMBER_CHECK_FLAG"], "cbMEMBER_CHECK_FLAG") as ASPxCheckBox;
    }

    protected void gvMaster_RowValidating(object sender, DevExpress.Web.Data.ASPxDataValidationEventArgs e)
    {
        if (e.IsNewRow || gvMaster.IsEditing)
        {
            gvMaster.Selection.UnselectAll();
            DateTime SystemDate = DateTime.Today;
            DateTime dtS = Convert.ToDateTime(((ASPxDateEdit)gvMaster.FindEditRowCellTemplateControl((GridViewDataColumn)gvMaster.Columns["S_DATE"], "txtS_DATE")).Text);
            string sdata = ((ASPxDateEdit)gvMaster.FindEditRowCellTemplateControl((GridViewDataColumn)gvMaster.Columns["S_DATE"], "txtS_DATE")).Text;
            string edata = (((ASPxDateEdit)gvMaster.FindEditRowCellTemplateControl((GridViewDataColumn)gvMaster.Columns["E_DATE"], "txtE_DATE")) == null) ? "" : ((ASPxDateEdit)gvMaster.FindEditRowCellTemplateControl((GridViewDataColumn)gvMaster.Columns["E_DATE"], "txtE_DATE")).Text ;

            string strEDate = ((ASPxDateEdit)gvMaster.FindEditRowCellTemplateControl((GridViewDataColumn)gvMaster.Columns["E_DATE"], "txtE_DATE")).Text.Trim();
            PopupControl pop1 = (PopupControl)gvMaster.FindEditRowCellTemplateControl((GridViewDataColumn)gvMaster.Columns["ACTIVITY_NO"], "ACTIVITY_NO");

            if (pop1.popTextBox.Enabled)
            {
              
                string strKey = pop1.popTextBox.Text.Trim();
                string strName = OPT15_PageHelper.GetDiscountMaster(strKey);
                if (string.IsNullOrEmpty(strName))
                {
                    e.RowError = "折扣料號不允許設定!!";
                    return;
                }

                int NOCount = OPT15_PageHelper.Query_HgConvertibleGiftM_ByActivityNo(strKey, sdata, edata, e.OldValues["ACTIVITY_NO"] == null ? "" : StringUtil.CStr(e.OldValues["ACTIVITY_NO"]));
                if (NOCount > 0)
                {
                    e.RowError = "折扣料號已存在，請重新輸入!!";
                    return;
                }

                if (StringUtil.CStr(e.NewValues["S_DATE"]) !=  (e.OldValues["S_DATE"] != null ? StringUtil.CStr(e.OldValues["S_DATE"]):""))
                {//MantisBT 筆記回覆 0000083 開始日期(可為系統日) #414
                    if (dtS < SystemDate)
                    {
                        e.RowError = "開始日期不允許小於[系統日]，請重新輸入!!";
                        return;
                    }
                }
            }

            if (!string.IsNullOrEmpty(strEDate))
            {
                DateTime dtE = Convert.ToDateTime(strEDate);

                if (dtE < dtS || dtE < SystemDate)
                {
                    e.RowError = "結束日期不允許小於[開始日期]及[系統日]，請重新輸入!!";
                    return;
                }
            }

            string lblDiscountName = ((ASPxTextBox)gvMaster.FindEditRowCellTemplateControl((GridViewDataColumn)gvMaster.Columns["ACTIVITY_NAME"], "txtDiscountName")).Text.Trim();
            if (string.IsNullOrEmpty(lblDiscountName))
            {
                e.RowError = "折扣料號不存在或已失效!!";
            }

            string Type = StringUtil.CStr(((ASPxComboBox)gvMaster.FindEditRowCellTemplateControl((GridViewDataColumn)gvMaster.Columns["TYPE"], "ddlType")).Value);  // 1:點數 2:商品
            string strDIVIDABLE_POINT = ((ASPxTextBox)gvMaster.FindEditRowCellTemplateControl((GridViewDataColumn)gvMaster.Columns["DIVIDABLE_POINT"], "txtDIVIDABLE_POINT")).Text.Trim();
            string strProdNo = ((PopupControl)gvMaster.FindEditRowCellTemplateControl((GridViewDataColumn)gvMaster.Columns["PRODNO"], "txtPRODNO")).Text.Trim();

            if (Type == "1")
            {
                if (string.IsNullOrEmpty(strDIVIDABLE_POINT))
                    e.RowError = "點數不允許空值，請重新輸入||";
            }
            else
            {
                if (string.IsNullOrEmpty(strProdNo))
                    e.RowError = "商品料號不允許空值，請重新輸入||";
            }

            DataTable dt = new Product_Facade().Query_ProductInfo(strProdNo);
            if (!string.IsNullOrEmpty(strProdNo) && dt.Rows.Count == 0)
            {
                e.RowError = "商品料號不存在或已失效!!";
            }

            if (!string.IsNullOrEmpty(e.RowError))
            {

                SaveTempData();
                return;
            }
        }
    }

    protected void gvMaster_CommandButtonInitialize(object sender, ASPxGridViewCommandButtonEventArgs e)
    {
        //只允許刪除未生效之資料(開始日期還沒到)
        //編輯：
        //1. 開始日期還沒到，都可以編輯
        //2. 開始日期到了，只能編輯結束日期
        //3. 活動結束後，就不能編輯

        if (e.VisibleIndex > -1)
        {
            if (e.ButtonType == ColumnCommandButtonType.Edit || e.ButtonType == ColumnCommandButtonType.SelectCheckbox)
            {
                string E_DATE = StringUtil.CStr(gvMaster.GetRowValues(e.VisibleIndex, "E_DATE"));
                if (!string.IsNullOrEmpty(E_DATE) && Convert.ToDateTime(E_DATE) < DateTime.Today)
                {
                    e.Enabled = false;
                }

                if (e.ButtonType == ColumnCommandButtonType.SelectCheckbox)
                {//MantisBT 筆記回覆 0000083 開始日期(可為系統日) #414
                    string date = StringUtil.CStr(gvMaster.GetRowValues(e.VisibleIndex, "S_DATE"));
                    if (!string.IsNullOrEmpty(date) && Convert.ToDateTime(date).AddDays(1) < DateTime.Now)
                    {
                        e.Enabled = false;
                    }

                }
            }
        }
    }

    protected void gvMaster_HtmlRowCreated(object sender, ASPxGridViewTableRowEventArgs e)
    {
        //ASPxComboBox ddlPartNumberOfDiscount = gvMaster.FindEditRowCellTemplateControl((GridViewDataColumn)gvMaster.Columns["ACTIVITY_NO"], "ddlPartNumberOfDiscount") as ASPxComboBox;
        ASPxCheckBox cbCHECK_FLAG = gvMaster.FindEditRowCellTemplateControl((GridViewDataColumn)gvMaster.Columns["MEMBER_CHECK_FLAG"], "cbMEMBER_CHECK_FLAG") as ASPxCheckBox;
        //BinDiscountProduct(); //繫結 折扣商品
        PopupControl pop1 = (PopupControl)gvMaster.FindEditRowCellTemplateControl((GridViewDataColumn)gvMaster.Columns["ACTIVITY_NO"], "ACTIVITY_NO");

        if (ViewState["TempData"] != null)
        {
            DataTable dt = ViewState["TempData"] as DataTable;
            DataRow dr = dt.Rows[0];

            if (pop1 != null && pop1.popTextBox.Text != "")
            {
                pop1.popTextBox.Text = StringUtil.CStr(dr["ACTIVITY_NO"]);
            }

            if (cbCHECK_FLAG != null)
            {
                cbCHECK_FLAG.Checked = Convert.ToBoolean(dr["MEMBER_CHECK_FLAG"]);
            }

            ViewState["TempData"] = null;
        }

        if (e.VisibleIndex == gvMaster.FocusedRowIndex
            && e.RowType == GridViewRowType.InlineEdit
            && e.RowType != GridViewRowType.EditingErrorRow)
        {

            if (pop1.popTextBox.Text != "")
            {
                pop1.popTextBox.Text = StringUtil.CStr(e.GetValue("ACTIVITY_NO"));
            }

            if (cbCHECK_FLAG != null)
            {
                cbCHECK_FLAG.Checked = StringUtil.CStr(e.GetValue("MEMBER_CHECK_FLAG")) == "1" ? true : false;
            }

            ASPxDateEdit txtS_DATE = gvMaster.FindEditRowCellTemplateControl((GridViewDataColumn)gvMaster.Columns["S_DATE"], "txtS_DATE") as ASPxDateEdit;
            ASPxDateEdit txtE_DATE = gvMaster.FindEditRowCellTemplateControl((GridViewDataColumn)gvMaster.Columns["E_DATE"], "txtE_DATE") as ASPxDateEdit;
            ASPxComboBox ddlType = gvMaster.FindEditRowCellTemplateControl((GridViewDataColumn)gvMaster.Columns["TYPE"], "ddlType") as ASPxComboBox;
            PopupControl txtPRODNO = gvMaster.FindEditRowCellTemplateControl((GridViewDataColumn)gvMaster.Columns["PRODNO"], "txtPRODNO") as PopupControl;
            ASPxTextBox txtDIVIDABLE_POINT = gvMaster.FindEditRowCellTemplateControl((GridViewDataColumn)gvMaster.Columns["DIVIDABLE_POINT"], "txtDIVIDABLE_POINT") as ASPxTextBox;
            ASPxTextBox txtUSE_COUNT = gvMaster.FindEditRowCellTemplateControl((GridViewDataColumn)gvMaster.Columns["USE_COUNT"], "txtUSE_COUNT") as ASPxTextBox;

            //只允許刪除未生效之資料(開始日期還沒到)
            //編輯：
            //1. 開始日期還沒到，都可以編輯
            //2. 開始日期到了，但還沒結束，只能編輯結束日期
            //3. 活動結束後，就不能編輯
            DateTime SystemDate = DateTime.Today;
            //MantisBT 筆記回覆 0000083 開始日期(可為系統日) #414
            if (!string.IsNullOrEmpty(txtS_DATE.Text) && Convert.ToDateTime(txtS_DATE.Text).AddDays(1) < SystemDate)
            {
                txtE_DATE.ClientEnabled = true;
                txtS_DATE.ClientEnabled = false;
                ddlType.ClientEnabled = false;
                txtPRODNO.Enabled = false;
                txtDIVIDABLE_POINT.ClientEnabled = false;
                cbCHECK_FLAG.ClientEnabled = false;
                txtUSE_COUNT.ClientEnabled = false;
                //ddlPartNumberOfDiscount.ReadOnly = true;@@
                GridViewDataTextColumn col_ACTIVITY_NO = (GridViewDataTextColumn)gvMaster.Columns["ACTIVITY_NO"];
                PopupControl stACTIVITY_NO_00 = ((PopupControl)gvMaster.FindEditRowCellTemplateControl(col_ACTIVITY_NO, "ACTIVITY_NO"));

                stACTIVITY_NO_00.Enabled = false;
            }
            else
            {
                if (StringUtil.CStr(ddlType.SelectedItem.Value) == "1")
                {
                    txtDIVIDABLE_POINT.ClientEnabled = true;
                    txtPRODNO.Enabled = false;
                    txtPRODNO.Text = "";
                }
                else
                {
                    txtPRODNO.Enabled = true;
                    txtDIVIDABLE_POINT.ClientEnabled = false;
                    txtDIVIDABLE_POINT.Text = "";
                }
            }

        }
    }

    protected void gvMaster_InitNewRow(object sender, DevExpress.Web.Data.ASPxDataInitNewRowEventArgs e)
    {

        ASPxGridView gv = (ASPxGridView)sender;

        //gv.SettingsBehavior.AllowFocusedRow = false;
        gv.SettingsBehavior.ProcessFocusedRowChangedOnServer = false;
        //BinDiscountProduct(); //繫結 折扣商品
    }

    protected void gvMaster_StartRowEditing(object sender, DevExpress.Web.Data.ASPxStartRowEditingEventArgs e)
    {
        ASPxGridView gv = (ASPxGridView)sender;

        //gv.SettingsBehavior.AllowFocusedRow = false;
        gv.SettingsBehavior.ProcessFocusedRowChangedOnServer = false;

        gv.Selection.UnselectAll();

        //gv.ProcessFocusedRowChangedOnServer;

        //ASPxCheckBox cbMEMBER_CHECK_FLAG = gvMaster.FindEditRowCellTemplateControl((GridViewDataColumn)gvMaster.Columns["MEMBER_CHECK_FLAG"], "cbMEMBER_CHECK_FLAG") as ASPxCheckBox;
        //if (ASPxPageControl1.ActiveTabIndex == 0 && cbMEMBER_CHECK_FLAG.Checked)
        //    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", "alert('此項不可指定門市');", true);

    }

    protected void gvMaster_CancelRowEditing(object sender, DevExpress.Web.Data.ASPxStartRowEditingEventArgs e)
    {
        ASPxGridView gv = (ASPxGridView)sender;

        //gv.SettingsBehavior.AllowFocusedRow = true;
        gv.SettingsBehavior.ProcessFocusedRowChangedOnServer = true;
    }


    #endregion

    #region gvDetail 觸發事件

    protected void gvDetail_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
    {
        if (gvMaster.FocusedRowIndex > -1)
        {
            OPT15_Facade facade = new OPT15_Facade();
            string sStoreNo = ((PopupControl)gvDetail.FindEditRowCellTemplateControl((GridViewDataColumn)gvDetail.Columns["STORE_NO"], "txtStoreNo")).Text;
            string sActivityID = StringUtil.CStr(gvMaster.GetRowValues(gvMaster.FocusedRowIndex, gvMaster.KeyFieldName));

            OPT15_HgConvertibleGiftM_DTO OPT15_DTO = new OPT15_HgConvertibleGiftM_DTO();
            OPT15_HgConvertibleGiftM_DTO.HG_CONVERT_GIFT_DDataTable dt = OPT15_DTO.HG_CONVERT_GIFT_D;
            OPT15_HgConvertibleGiftM_DTO.HG_CONVERT_GIFT_DRow dr = dt.NewHG_CONVERT_GIFT_DRow();

            dr.SID = GuidNo.getUUID();
            dr.ACTIVITY_ID = sActivityID;
            dr.CREATE_USER = this.QryUSER;
            dr.CREATE_DTM = System.DateTime.Now;
            dr.MODI_USER = this.QryUSER;
            dr.MODI_DTM = System.DateTime.Now;
            //dr.STORE_NO = ((PopupControl)gvDetail.FindEditRowCellTemplateControl((GridViewDataColumn)gvDetail.Columns["STORE_NO"], "txtStoreNo")).Text;
            dr.STORE_NO = sStoreNo;
            //((PopupControl)gvDetail.FindEditFormTemplateControl("txtStoreNo")).Text;
            dr.DEL_FLAG = "N";
            dt.Rows.Add(dr);

            OPT15_DTO.AcceptChanges();

            //更新資料庫
            facade.AddNewOne_HgConvertGiftD(OPT15_DTO);
            gvDetail.CancelEdit();
            e.Cancel = true;

            BinDetailData();

        }
    }

    protected void gvDetail_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
    {

        OPT15_HgConvertibleGiftM_DTO OPT15_DTO = new OPT15_HgConvertibleGiftM_DTO();
        OPT15_HgConvertibleGiftM_DTO.HG_CONVERT_GIFT_DDataTable dt = OPT15_DTO.HG_CONVERT_GIFT_D;
        OPT15_HgConvertibleGiftM_DTO.HG_CONVERT_GIFT_DRow dr = dt.NewHG_CONVERT_GIFT_DRow();
        OPT15_Facade facade = new OPT15_Facade();

        DataRow dirtyDr = OPT15_PageHelper.Query_HgConvertibleGiftD_ByKey(StringUtil.CStr(e.Keys[gvDetail.KeyFieldName]));
        string sActivityID = StringUtil.CStr(dirtyDr["ACTIVITY_ID"]);
        string sStore_No = ((PopupControl)gvDetail.FindEditRowCellTemplateControl((GridViewDataColumn)gvDetail.Columns["STORE_NO"], "txtStoreNo")).Text;
        string old_Store_No = StringUtil.CStr(dirtyDr["STORE_NO"]);

        dr.SID = StringUtil.CStr(dirtyDr["SID"]);
        dr.ACTIVITY_ID = sActivityID;
        dr.CREATE_USER = StringUtil.CStr(dirtyDr["CREATE_USER"]);
        dr.CREATE_DTM = Convert.ToDateTime(dirtyDr["CREATE_DTM"]);
        dr.MODI_USER = this.QryUSER;
        dr.MODI_DTM = System.DateTime.Now;
        dr.STORE_NO = sStore_No;
        //((PopupControl)gvDetail.FindEditFormTemplateControl("txtStoreNo")).Text;
        dr.DEL_FLAG = StringUtil.CStr(dirtyDr["DEL_FLAG"]);

        dt.Rows.Add(dr);

        OPT15_DTO.AcceptChanges();

        //更新資料庫
        facade.UpdateOne_HgConvertGiftD(OPT15_DTO);

        gvDetail.CancelEdit();
        e.Cancel = true;

        BinDetailData();

    }

    protected void gvDetail_PageIndexChanged(object sender, EventArgs e)
    {
        BinDetailData();
    }

    protected void gvDetail_PreRender(object sender, EventArgs e)
    {
        //BindZoneType();
    }

    protected void gvDetail_RowValidating(object sender, DevExpress.Web.Data.ASPxDataValidationEventArgs e)
    {
        string strStoreNo = ((PopupControl)gvDetail.FindEditRowCellTemplateControl((GridViewDataColumn)gvDetail.Columns["STORE_NO"], "txtStoreNo")).Text;
        //((PopupControl)gvDetail.FindEditFormTemplateControl("txtStoreNo")).Text.Trim();
        DataTable dtStore = new Store_Facade().Query_StoreZone_ByKey(strStoreNo);
        string lblStoreName = "";
        if (dtStore.Rows.Count > 0)
        {
            lblStoreName = StringUtil.CStr(dtStore.Rows[0]["STORENAME"]);
        }
        if (string.IsNullOrEmpty(lblStoreName))
        {
            e.RowError = "門市編號不存在!!";
            return;
        }

        string sStoreNo = ((PopupControl)gvDetail.FindEditRowCellTemplateControl((GridViewDataColumn)gvDetail.Columns["STORE_NO"], "txtStoreNo")).Text;
        string sActivityID = StringUtil.CStr(gvMaster.GetRowValues(gvMaster.FocusedRowIndex, gvMaster.KeyFieldName));
        string SID = e.Keys["SID"] == null ? "" : StringUtil.CStr(e.Keys["SID"]);
        int iCount = new OPT15_Facade().Query_HgConvertibleGiftD(sActivityID, sStoreNo, SID);
        if (iCount > 0)
        {
            e.RowError = "門市重複，請重新輸入!!";
            return;

        }
        gvDetail.Selection.UnselectAll();
    }

    protected void gvDetail_OnStartRowEditing(object sender, DevExpress.Web.Data.ASPxStartRowEditingEventArgs e)
    {
        gvDetail.Selection.UnselectAll();
    }

    #endregion

    #region ajax 呼當前網頁
    //ajax 呼當前網頁的方式
    [System.Web.Services.WebMethod()]
    [System.Web.Script.Services.ScriptMethod()]
    static public string getStoreInfo(string STORE_NO)
    {
        string strInfo = "";
        if (!string.IsNullOrEmpty(STORE_NO))
        {
            DataTable dtStore = new Store_Facade().Query_StoreZone_ByKey(STORE_NO);
            if (dtStore.Rows.Count > 0)
            {
                strInfo = StringUtil.CStr(dtStore.Rows[0]["STORENAME"]) + ";" + StringUtil.CStr(dtStore.Rows[0]["ZONE_NAME"]);
            }
        }

        return strInfo;
    }

    //ajax 呼當前網頁的方式
    [System.Web.Services.WebMethod()]
    [System.Web.Script.Services.ScriptMethod()]
    static public string getProductInfo(string PRODNO)
    {
        string strInfo = "";
        if (!string.IsNullOrEmpty(PRODNO))
        {
            DataTable dt = new Product_Facade().Query_DiscountProduct(PRODNO);
            if (dt.Rows.Count > 0)
            {
                strInfo = StringUtil.CStr(dt.Rows[0]["PRODNAME"]);
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

    protected void ddlType_SelectedIndexChanged(object sender, EventArgs e)
    {
        ASPxComboBox iType = gvMaster.FindChildControl<ASPxComboBox>("ddlType");
        PopupControl txtPRODNO = gvMaster.FindChildControl<PopupControl>("txtPRODNO");
        ASPxTextBox txtDIVIDABLE_POINT = gvMaster.FindChildControl<ASPxTextBox>("txtDIVIDABLE_POINT");
        if (StringUtil.CStr(iType.SelectedItem.Value) == "1")
        {
            txtDIVIDABLE_POINT.ClientEnabled = true;
            txtPRODNO.Enabled = false;
            txtPRODNO.Text = "";
        }
        else
        {
            txtPRODNO.Enabled = true;
            txtDIVIDABLE_POINT.ClientEnabled = false;
            txtDIVIDABLE_POINT.Text = "";
        }
    }
    #endregion

}
