using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using DevExpress.Web;
using DevExpress.Web.ASPxGridView;
using DevExpress.Web.ASPxPager;

using Advtek.Utility;
using FET.POS.Model.DTO;
using FET.POS.Model.Facade;
using FET.POS.Model.Facade.FacadeImpl;
using FET.POS.Model.Common;
using DevExpress.Web.ASPxEditors;

public partial class VSS_OPT_OPT18 : BasePage
{
    private string QryUSER;

    protected void Page_Load(object sender, EventArgs e)
    {
        QryUSER = logMsg.OPERATOR;
        if (!Page.IsPostBack && !Page.IsCallback)
        {
            //取得空的資料表
            gvMaster.DataSource = new OPT18_StoreSpecialDis_DTO().STORE_SPECIAL_DIS_M;
            gvMaster.DataBind();

            gvDetail.Visible = false;

            gvDetail.DataSource = new OPT18_StoreSpecialDis_DTO().STORE_SPECIAL_DIS_D;
            gvDetail.DataBind();
            gvDetail.FindChildControl<ASPxButton>("btnAddNewD").ClientEnabled = false;

        }
        else
        {
            if (Request["__EVENTARGUMENT"] == "AAA")
            {
                string UBatchNo = StringUtil.CStr(Session["UploadBatchNo"]);
                if (!string.IsNullOrEmpty(UBatchNo))
                {
                    //this.lblImportStatus.Text = "資料已匯入";
                }
            }
        }
    }

    protected void BindMasterData()
    {
        gvMaster.DataSource = new OPT18_Facade().Query_StoreSpecialDisM(this.txtStoreNo.Text,
            this.txtStoreName.Text, this.txtDiscountsMonth_S.Text, this.txtDiscountsMonth_E.Text);
        gvMaster.DataBind();
        gvMaster.Selection.UnselectAll();
    }

    protected void BindDetailData()
    {
        string UUID = StringUtil.CStr(gvMaster.GetRowValues(gvMaster.FocusedRowIndex, gvMaster.KeyFieldName));
        gvDetail.DataSource = new OPT18_Facade().Query_StoreSpecialDisD(UUID);
        gvDetail.DataBind();
        gvDetail.Selection.UnselectAll();
    }

    private void BinRole()
    {
        ASPxComboBox ddlRole = gvDetail.FindEditRowCellTemplateControl((GridViewDataColumn)gvDetail.Columns["ROLE_NAME"], "ddlRole") as ASPxComboBox;
        ddlRole.TextField = "ROLE_NAME";
        ddlRole.ValueField = "ROLE_ID";
        ddlRole.DataSource = OPT18_PageHelper.GetRole();
        ddlRole.DataBind();
    }

    #region Button 觸發事件

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        BindMasterData();
        this.gvMaster.FocusedRowIndex = -1;
        gvMaster.PageIndex = 0;
        gvMaster.Selection.UnselectAll();
        gvDetail.DataSource = new OPT18_StoreSpecialDis_DTO().STORE_SPECIAL_DIS_D;
        gvDetail.DataBind();
        if (gvDetail.FindChildControl<ASPxButton>("btnAddNewD") != null)
            gvDetail.FindChildControl<ASPxButton>("btnAddNewD").ClientEnabled = false;
        gvDetail.Selection.UnselectAll();
        gvDetail.Visible = false;
    }

    protected void btnDeleteM_Click(object sender, EventArgs e)
    {
        OPT18_StoreSpecialDis_DTO OPT18_DTO = new OPT18_StoreSpecialDis_DTO();
        OPT18_StoreSpecialDis_DTO.STORE_SPECIAL_DIS_MDataTable dt = OPT18_DTO.STORE_SPECIAL_DIS_M;
        OPT18_Facade facade = new OPT18_Facade();

        bool UnDelete = false;

        List<object> keyValues = this.gvMaster.GetSelectedFieldValues(gvMaster.KeyFieldName);
        foreach (string key in keyValues)
        {
            //DataTable dtDetail = facade.Query_StoreSpecialDisD(key);

            //if (dtDetail.Rows.Count > 0)
            //{
            //    gvMaster.FindChildControl<ASPxLabel>("lblError").Text = "選取的項目中還包含子資料，所以無法刪除！";
            //    return;
            //}
            //else
            //{
            OPT18_StoreSpecialDis_DTO.STORE_SPECIAL_DIS_MRow dr = dt.NewSTORE_SPECIAL_DIS_MRow();

            DataRow dirtyDr = OPT18_PageHelper.Query_StoreSpecialDisM_ByKey(key);

            dr.SSD_ID = StringUtil.CStr(dirtyDr["SSD_ID"]);
            dr.STORE_NO = StringUtil.CStr(dirtyDr["STORE_NO"]);
            dr.YYMM = StringUtil.CStr(dirtyDr["YYMM"]);
            dr.DIS_AMT = Convert.ToInt32(dirtyDr["DIS_AMT"]);
            dr.CREATE_USER = StringUtil.CStr(dirtyDr["CREATE_USER"]);
            dr.CREATE_DTM = Convert.ToDateTime(dirtyDr["CREATE_DTM"]);
            dr.MODI_USER = this.QryUSER;
            dr.MODI_DTM = System.DateTime.Now;
            dr.DEL_FLAG = "Y";

            string Discount_Y = StringUtil.CStr(dr.YYMM).Substring(0, 4);
            string Discount_M = StringUtil.CStr(dr.YYMM).Substring(5, 2);
            if (Convert.ToInt32(Discount_Y) < DateTime.Now.Year || (Convert.ToInt32(Discount_Y) == DateTime.Now.Year && Convert.ToInt32(Discount_M) <= DateTime.Now.Month))
            {
                UnDelete = true;
            }
            else
            {
                dt.Rows.Add(dr);
            }
            OPT18_DTO.AcceptChanges();

            //}
        }

        if (OPT18_DTO.STORE_SPECIAL_DIS_M.Rows.Count > 0)
        {
            //更新資料庫
            facade.Delete_StoreSpecialDisM(OPT18_DTO);

            BindMasterData();
            if (UnDelete)
                gvMaster.FindChildControl<ASPxLabel>("lblError").Text = "當月及歷史資料不可刪除!！";
            this.gvMaster.FocusedRowIndex = -1;
            gvDetail.DataSource = new OPT18_StoreSpecialDis_DTO().STORE_SPECIAL_DIS_D;
            gvDetail.DataBind();
            gvDetail.Visible = false;
        }

    }

    protected void btnDeleteD_Click(object sender, EventArgs e)
    {
        OPT18_Facade facade = new OPT18_Facade();

        List<object> keyValues = this.gvDetail.GetSelectedFieldValues(gvDetail.KeyFieldName);
        foreach (string key in keyValues)
        {
            string Discount_Y = StringUtil.CStr(gvMaster.GetRowValues(gvMaster.FocusedRowIndex, "YYMM")).Substring(0, 4);
            string Discount_M = StringUtil.CStr(gvMaster.GetRowValues(gvMaster.FocusedRowIndex, "YYMM")).Substring(5, 2);
            if (Convert.ToInt16(Discount_Y) < DateTime.Now.Year || (Convert.ToInt16(Discount_Y) == DateTime.Now.Year && Convert.ToInt16(Discount_M) <= DateTime.Now.Month))
            {
                gvDetail.FindChildControl<ASPxLabel>("lblErrorD").Text = "當月及歷史資料不可刪除!！";
                return;
            }
            OPT18_StoreSpecialDis_DTO OPT18_DTO = new OPT18_StoreSpecialDis_DTO();
            OPT18_StoreSpecialDis_DTO.STORE_SPECIAL_DIS_DDataTable dt = OPT18_DTO.STORE_SPECIAL_DIS_D;
            OPT18_StoreSpecialDis_DTO.STORE_SPECIAL_DIS_DRow dr = dt.NewSTORE_SPECIAL_DIS_DRow();
            string[] splitKey = key.Split('|');
            DataRow dirtyDr = OPT18_PageHelper.Query_StoreSpecialDisD_ByKey(key);

            dr.SSDD_ID = StringUtil.CStr(dirtyDr["SSDD_ID"]);
            dr.SSD_ID = StringUtil.CStr(dirtyDr["SSD_ID"]);
            dr.ROLE_ID = StringUtil.CStr(dirtyDr["ROLE_ID"]);
            dr.DIS_AMT = Convert.ToInt64(dirtyDr["DIS_AMT"]);
            dr.DIS_RATE = Convert.ToInt64(dirtyDr["DIS_RATE"]);
            dr.DIS_AMT_UBOND = Convert.ToInt64(dirtyDr["DIS_AMT_UBOND"]);
            dr.CREATE_USER = StringUtil.CStr(dirtyDr["CREATE_USER"]);
            dr.CREATE_DTM = Convert.ToDateTime(dirtyDr["CREATE_DTM"]);
            dr.MODI_USER = this.QryUSER;
            dr.MODI_DTM = System.DateTime.Now;
            dr.STORE_NO = StringUtil.CStr(dirtyDr["STORE_NO"]);
            dr.DEL_FLAG = "Y";
            dt.Rows.Add(dr);

            OPT18_DTO.AcceptChanges();

            //更新資料庫
            facade.Delete_StoreSpecialDisD(OPT18_DTO);

        }

        if (keyValues.Count > 0)
        {
            BindDetailData();
        }

    }

    protected void btnAddNewD_Click(object sender, EventArgs e)
    {
        if (gvMaster.FocusedRowIndex > -1)
        {
            gvDetail.Selection.UnselectAll();
            this.gvDetail.AddNewRow();
        }
    }


    #endregion

    #region gvMaster 觸發事件

    protected void gvMaster_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
    {
        OPT18_StoreSpecialDis_DTO OPT18_DTO = new OPT18_StoreSpecialDis_DTO();
        OPT18_StoreSpecialDis_DTO.STORE_SPECIAL_DIS_MDataTable dt = OPT18_DTO.STORE_SPECIAL_DIS_M;
        OPT18_StoreSpecialDis_DTO.STORE_SPECIAL_DIS_MRow dr = dt.NewSTORE_SPECIAL_DIS_MRow();
        OPT18_Facade facade = new OPT18_Facade();

        dr.SSD_ID = GuidNo.getUUID();
        dr.DISCOUNT_CODE = OPT18_PageHelper.GetDiscountNo();
        dr.STORE_NO = ((PopupControl)gvMaster.FindEditRowCellTemplateControl((GridViewDataColumn)gvMaster.Columns["STORE_NO"], "txtStoreNo")).Text.Trim();
        string strYYYYMM = ((ASPxDateEdit)gvMaster.FindEditRowCellTemplateControl((GridViewDataColumn)gvMaster.Columns["YYMM"], "txtYYYYMM")).Text.Trim();
        if (!string.IsNullOrEmpty(strYYYYMM))
        {
            dr.YYMM = strYYYYMM;
        }
        string strDiscountQuota = ((ASPxTextBox)gvMaster.FindEditRowCellTemplateControl((GridViewDataColumn)gvMaster.Columns["DIS_AMT"], "txtDiscountQuota")).Text.Trim();
        if (!string.IsNullOrEmpty(strDiscountQuota))
        {
            dr.DIS_AMT = Convert.ToInt32(strDiscountQuota);
        }
        dr.CREATE_USER = this.QryUSER;
        dr.CREATE_DTM = System.DateTime.Now;
        dr.MODI_USER = this.QryUSER;
        dr.MODI_DTM = System.DateTime.Now;
        dr.DEL_FLAG = "N";

        dt.Rows.Add(dr);
        OPT18_DTO.AcceptChanges();

        //更新資料庫
        facade.AddNewOne_StoreSpecialDisM(OPT18_DTO);

        gvMaster.CancelEdit();
        e.Cancel = true;

        BindMasterData();
    }

    protected void gvMaster_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
    {
        OPT18_StoreSpecialDis_DTO OPT18_DTO = new OPT18_StoreSpecialDis_DTO();
        OPT18_StoreSpecialDis_DTO.STORE_SPECIAL_DIS_MDataTable dt = OPT18_DTO.STORE_SPECIAL_DIS_M;
        OPT18_StoreSpecialDis_DTO.STORE_SPECIAL_DIS_MRow dr = dt.NewSTORE_SPECIAL_DIS_MRow();
        OPT18_Facade facade = new OPT18_Facade();

        DataRow dirtyDr = OPT18_PageHelper.Query_StoreSpecialDisM_ByKey(StringUtil.CStr(e.Keys[gvMaster.KeyFieldName]));

        dr.SSD_ID = StringUtil.CStr(dirtyDr["SSD_ID"]);
        dr.DISCOUNT_CODE = StringUtil.CStr(dirtyDr["DISCOUNT_CODE"]);
        dr.STORE_NO = ((PopupControl)gvMaster.FindEditRowCellTemplateControl((GridViewDataColumn)gvMaster.Columns["STORE_NO"], "txtStoreNo")).Text.Trim();
        string strYYYYMM = ((ASPxDateEdit)gvMaster.FindEditRowCellTemplateControl((GridViewDataColumn)gvMaster.Columns["YYMM"], "txtYYYYMM")).Text.Trim();
        if (!string.IsNullOrEmpty(strYYYYMM))
        {
            dr.YYMM = strYYYYMM;
        }
        string strDiscountQuota = ((ASPxTextBox)gvMaster.FindEditRowCellTemplateControl((GridViewDataColumn)gvMaster.Columns["DIS_AMT"], "txtDiscountQuota")).Text.Trim();
        if (!string.IsNullOrEmpty(strDiscountQuota))
        {
            dr.DIS_AMT = Convert.ToInt32(strDiscountQuota);
        }
        dr.CREATE_USER = StringUtil.CStr(dirtyDr["CREATE_USER"]);
        dr.CREATE_DTM = Convert.ToDateTime(dirtyDr["CREATE_DTM"]);
        dr.MODI_USER = this.QryUSER;
        dr.MODI_DTM = System.DateTime.Now;
        dr.DEL_FLAG = StringUtil.CStr(dirtyDr["DEL_FLAG"]);

        dt.Rows.Add(dr);
        OPT18_DTO.AcceptChanges();

        //更新資料庫
        facade.UpdateOne_StoreSpecialDisM(OPT18_DTO);

        gvMaster.CancelEdit();
        e.Cancel = true;
        BindMasterData();

    }

    protected void gvMaster_PageIndexChanged(object sender, EventArgs e)
    {
        gvDetail.DataSource = null;
        gvDetail.DataBind();
        // gvDetail.Visible = false;
        BindMasterData();
        gvMaster.CancelEdit();
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

            string date = StringUtil.CStr(gvMaster.GetRowValues(e.VisibleIndex, "YYMM"));
            //string storeno = StringUtil.CStr(gvMaster.GetRowValues(e.VisibleIndex, "STORE_NO"));
            int s_use_m = Convert.ToInt32((date.Substring(5, 2)));
            int s_use_y = Convert.ToInt32((date.Substring(0, 4)));
            string yyy = StringUtil.CStr(DateTime.Now.Year);
            string xxx = StringUtil.CStr(DateTime.Now.Month);
            if (s_use_y < Convert.ToInt32(yyy))
            {
                e.Row.Attributes["canSelect"] = "false";
            }

            if (s_use_y == Convert.ToInt32(yyy) && s_use_m <= Convert.ToInt32(xxx))
            {
                e.Row.Attributes["canSelect"] = "false";
            }
            if (logMsg.STORENO == null)
            {
                e.Row.Attributes["canSelect"] = "false";
            }
            //if (logMsg.STORENO != StringUtil.CStr(System.Web.Configuration.WebConfigurationManager.AppSettings["DefaultStore"]))
            //{
            //    e.Row.Attributes["canSelect"] = "false";
            //}
        }
    }

    protected void gvMaster_FocusedRowChanged(object sender, EventArgs e)
    {
        if (gvMaster.FocusedRowIndex >= 0)
        {
            //string strYYYYMM = ((ASPxDateEdit)gvMaster.FindEditRowCellTemplateControl((GridViewDataColumn)gvMaster.Columns["YYMM"], "txtYYYYMM")).Text.Trim();
            string Discount_Y = StringUtil.CStr(gvMaster.GetRowValues(gvMaster.FocusedRowIndex, "YYMM")).Substring(0, 4);
            string Discount_M = StringUtil.CStr(gvMaster.GetRowValues(gvMaster.FocusedRowIndex, "YYMM")).Substring(5, 2);

            if (Convert.ToInt32(Discount_Y) < DateTime.Now.Year || (Convert.ToInt32(Discount_Y) == DateTime.Now.Year && Convert.ToInt32(Discount_M) <= DateTime.Now.Month))
            {
                gvDetail.Enabled = false;
            }
            else
            {
                gvDetail.Enabled = true;
            }

            gvDetail.PagerBarEnabled = true;
            BindDetailData();

            if (gvDetail.FindChildControl<ASPxButton>("btnAddNewD") != null)
            {
                gvDetail.FindChildControl<ASPxButton>("btnAddNewD").ClientEnabled = true;
            }
            gvDetail.Selection.UnselectAll();
            gvDetail.Visible = true;
        }
    }

    protected void gvMaster_OnInitNewRow(object sender, EventArgs e)
    {
        gvMaster.Selection.UnselectAll();
        gvDetail.Selection.UnselectAll();
    
    }

    protected void gvMaster_RowValidating(object sender, DevExpress.Web.Data.ASPxDataValidationEventArgs e)
    {
        if (e.IsNewRow || gvMaster.IsEditing)
        {
            string strYYYYMM = ((ASPxDateEdit)gvMaster.FindEditRowCellTemplateControl((GridViewDataColumn)gvMaster.Columns["YYMM"], "txtYYYYMM")).Text.Trim();

            if (!string.IsNullOrEmpty(strYYYYMM))
            {
                if (strYYYYMM.Length == 7)
                {
                    try
                    {
                        DateTime dtYYYYMM = Convert.ToDateTime(strYYYYMM + "/01");
                        DateTime SystemModth = Convert.ToDateTime(DateTime.Now.Year + "/" + DateTime.Now.Month + "/01");
                        if (SystemModth >= dtYYYYMM)
                        {
                            e.RowError += "折扣月份不可小於等於系統日!!";
                            return;
                        }

                    }
                    catch
                    {
                        e.RowError += "折扣月份請輸入 YYYY/MM 的格式!!";
                        return;
                    }
                }
                else
                {
                    e.RowError += "折扣月份請輸入 YYYY/MM 的格式!!";
                    return;
                }
            }
            PopupControl PCStoreNo = gvMaster.FindEditRowCellTemplateControl((GridViewDataColumn)gvMaster.Columns["STORE_NO"], "txtStoreNo") as PopupControl;
            string strStoreNo = PCStoreNo.Text.Trim();
            DataTable dtStore = new Store_Facade().Query_StoreInfo(strStoreNo);
            if (dtStore.Rows.Count <= 0)
            {
                e.RowError += "門市編號不存在!!";
                return;
            }

            ASPxDateEdit txtYYYYMM = gvMaster.FindEditRowCellTemplateControl((GridViewDataColumn)gvMaster.Columns["YYMM"], "txtYYYYMM") as ASPxDateEdit;
            if (txtYYYYMM.Enabled)
            {
                string SSD_ID = "";
                if (e.Keys[gvMaster.KeyFieldName] != null)
                {
                    SSD_ID = StringUtil.CStr(e.Keys[gvMaster.KeyFieldName]);
                }

                string strYYMM = txtYYYYMM.Text.Trim();
                int intDataCount = OPT18_PageHelper.Query_StoreSpecialDisM_ByParams(strStoreNo, strYYMM, SSD_ID);
                if (intDataCount > 0)
                {
                    e.RowError += "門市編號及折扣月份重複!!";
                    return;
                }
            }

            ASPxTextBox txtDiscountQuota = gvMaster.FindEditRowCellTemplateControl((GridViewDataColumn)gvMaster.Columns["DIS_AMT"], "txtDiscountQuota") as ASPxTextBox;
            if (Convert.ToInt64(txtDiscountQuota.Text) == 0)
            {
                e.RowError += "折扣總額不可為0";
                return;
            }


            if (gvMaster.IsEditing)
            {
                string SSD_ID = "";
                if (e.Keys[gvMaster.KeyFieldName] != null)
                {
                    SSD_ID = StringUtil.CStr(e.Keys[gvMaster.KeyFieldName]);
                }
                long maxdiscount = Convert.ToInt64(OPT18_PageHelper.GetStoreMax_AMT(SSD_ID, "", ""));
                if (Convert.ToInt64(txtDiscountQuota.Text) < maxdiscount && maxdiscount > 0)
                {
                    e.RowError += "折扣總額不可低於折扣上限";
                    return;
                }


            }
        }
    }

    protected void gvMaster_StartRowEditing(object sender, DevExpress.Web.Data.ASPxStartRowEditingEventArgs e)
    {
        gvMaster.Selection.UnselectAll();
        gvDetail.Selection.UnselectAll();
    }

    protected void gvMaster_CommandButtonInitialize(object sender, ASPxGridViewCommandButtonEventArgs e)
    {
        if (e.VisibleIndex > -1)
        {
            if (e.ButtonType == ColumnCommandButtonType.Edit || e.ButtonType == ColumnCommandButtonType.SelectCheckbox)
            {
                string Discount_Y = StringUtil.CStr(gvMaster.GetRowValues(e.VisibleIndex, "YYMM")).Substring(0, 4);
                string Discount_M = StringUtil.CStr(gvMaster.GetRowValues(e.VisibleIndex, "YYMM")).Substring(5, 2);
                if (Convert.ToInt32(Discount_Y) < DateTime.Now.Year || (Convert.ToInt32(Discount_Y) == DateTime.Now.Year && Convert.ToInt32(Discount_M) <= DateTime.Now.Month))
                {
                    e.Enabled = false;
                }


            }
        }
    }

    #endregion

    #region gvDetail 觸發事件

    protected void gvDetail_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
    {
        if (gvMaster.FocusedRowIndex >= 0)
        {

            OPT18_StoreSpecialDis_DTO OPT18_DTO = new OPT18_StoreSpecialDis_DTO();
            OPT18_StoreSpecialDis_DTO.STORE_SPECIAL_DIS_DDataTable dt = OPT18_DTO.STORE_SPECIAL_DIS_D;
            OPT18_StoreSpecialDis_DTO.STORE_SPECIAL_DIS_DRow dr = dt.NewSTORE_SPECIAL_DIS_DRow();
            OPT18_Facade facade = new OPT18_Facade();

            dr.SSDD_ID = GuidNo.getUUID();
            dr.SSD_ID = StringUtil.CStr(gvMaster.GetRowValues(gvMaster.FocusedRowIndex, gvMaster.KeyFieldName));
            dr.ROLE_ID = StringUtil.CStr(((ASPxComboBox)gvDetail.FindEditRowCellTemplateControl((GridViewDataColumn)gvDetail.Columns["ROLE_NAME"], "ddlRole")).SelectedItem.Value);
            string strMoneyAmt = ((ASPxTextBox)gvDetail.FindEditRowCellTemplateControl((GridViewDataColumn)gvDetail.Columns["DIS_AMT"], "txtMoneyAmt")).Text.Trim();
            if (!string.IsNullOrEmpty(strMoneyAmt))
            {
                dr.DIS_AMT = Convert.ToInt64(strMoneyAmt);
            }
            else
            {
                dr.DIS_AMT = 0;
            }
            string strDisRate = ((ASPxTextBox)gvDetail.FindEditRowCellTemplateControl((GridViewDataColumn)gvDetail.Columns["DIS_RATE"], "txtDisRate")).Text.Trim();
            if (!string.IsNullOrEmpty(strDisRate))
            {
                dr.DIS_RATE = Convert.ToInt64(strDisRate);
            }
            else
            {
                dr.DIS_RATE = 0;
            }
            dr.DIS_AMT_UBOND = Convert.ToInt64(((ASPxTextBox)gvDetail.FindEditRowCellTemplateControl((GridViewDataColumn)gvDetail.Columns["DIS_AMT_UBOND"], "txtDisUpbon")).Text.Trim());
            dr.CREATE_USER = this.QryUSER;
            dr.CREATE_DTM = System.DateTime.Now;
            dr.MODI_USER = this.QryUSER;
            dr.MODI_DTM = System.DateTime.Now;
            dr.STORE_NO = StringUtil.CStr(gvMaster.GetRowValues(gvMaster.FocusedRowIndex, "STORE_NO"));
            dr.DEL_FLAG = "N";
            dt.Rows.Add(dr);

            OPT18_DTO.AcceptChanges();

            //更新資料庫
            facade.AddNewOne_StoreSpecialDisD(OPT18_DTO);

            gvDetail.CancelEdit();
            e.Cancel = true;

            BindDetailData();
        }

    }

    protected void gvDetail_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
    {
        OPT18_StoreSpecialDis_DTO OPT18_DTO = new OPT18_StoreSpecialDis_DTO();
        OPT18_StoreSpecialDis_DTO.STORE_SPECIAL_DIS_DDataTable dt = OPT18_DTO.STORE_SPECIAL_DIS_D;
        OPT18_StoreSpecialDis_DTO.STORE_SPECIAL_DIS_DRow dr = dt.NewSTORE_SPECIAL_DIS_DRow();
        OPT18_Facade facade = new OPT18_Facade();

        DataRow dirtyDr = OPT18_PageHelper.Query_StoreSpecialDisD_ByKey(StringUtil.CStr(e.Keys[gvDetail.KeyFieldName]));

        dr.SSDD_ID = StringUtil.CStr(dirtyDr["SSDD_ID"]);
        dr.SSD_ID = StringUtil.CStr(dirtyDr["SSD_ID"]);
        dr.ROLE_ID = StringUtil.CStr(((ASPxComboBox)gvDetail.FindEditRowCellTemplateControl((GridViewDataColumn)gvDetail.Columns["ROLE_NAME"], "ddlRole")).SelectedItem.Value);
        string strMoneyAmt = ((ASPxTextBox)gvDetail.FindEditRowCellTemplateControl((GridViewDataColumn)gvDetail.Columns["DIS_AMT"], "txtMoneyAmt")).Text.Trim();
        if (!string.IsNullOrEmpty(strMoneyAmt))
        {
            dr.DIS_AMT = Convert.ToInt64(strMoneyAmt);
        }
        else
        {
            dr.DIS_AMT = 0;
        }
        string strDisRate = ((ASPxTextBox)gvDetail.FindEditRowCellTemplateControl((GridViewDataColumn)gvDetail.Columns["DIS_RATE"], "txtDisRate")).Text.Trim();
        if (!string.IsNullOrEmpty(strDisRate))
        {
            dr.DIS_RATE = Convert.ToInt64(strDisRate);
        }
        else
        {
            dr.DIS_RATE = 0;
        }
        dr.DIS_AMT_UBOND = Convert.ToInt64(((ASPxTextBox)gvDetail.FindEditRowCellTemplateControl((GridViewDataColumn)gvDetail.Columns["DIS_AMT_UBOND"], "txtDisUpbon")).Text.Trim());
        dr.CREATE_USER = StringUtil.CStr(dirtyDr["CREATE_USER"]);
        dr.CREATE_DTM = Convert.ToDateTime(dirtyDr["CREATE_DTM"]);
        dr.MODI_USER = this.QryUSER;
        dr.MODI_DTM = System.DateTime.Now;
        dr.STORE_NO = StringUtil.CStr(dirtyDr["STORE_NO"]);
        dr.DEL_FLAG = StringUtil.CStr(dirtyDr["DEL_FLAG"]);

        dt.Rows.Add(dr);

        OPT18_DTO.AcceptChanges();

        //更新資料庫
        facade.UpdateOne_StoreSpecialDisD(OPT18_DTO);

        gvDetail.CancelEdit();
        e.Cancel = true;

        BindDetailData();
    }

    protected void gvDetail_PageIndexChanged(object sender, EventArgs e)
    {
        BindDetailData();
        gvDetail.CancelEdit();
    }

    protected void gvDetail_RowValidating(object sender, DevExpress.Web.Data.ASPxDataValidationEventArgs e)
    {
        if (e.IsNewRow || gvDetail.IsEditing)
        {

            string strMoneyAmt = ((ASPxTextBox)gvDetail.FindEditRowCellTemplateControl((GridViewDataColumn)gvDetail.Columns["DIS_AMT"], "txtMoneyAmt")).Text.Trim();
            string strDisRate = ((ASPxTextBox)gvDetail.FindEditRowCellTemplateControl((GridViewDataColumn)gvDetail.Columns["DIS_RATE"], "txtDisRate")).Text.Trim();

            if (string.IsNullOrEmpty(strMoneyAmt) && string.IsNullOrEmpty(strDisRate))
            {
                e.RowError += "金額和比率請擇一輸入值!!";
                return;
            }
            long intMoneyAmt=0;
            if (!string.IsNullOrEmpty(strMoneyAmt))
            {
                try
                {
                     intMoneyAmt = Convert.ToInt64(strMoneyAmt);
                    long intDisUpbon = Convert.ToInt64(((ASPxTextBox)gvDetail.FindEditRowCellTemplateControl((GridViewDataColumn)gvDetail.Columns["DIS_AMT_UBOND"], "txtDisUpbon")).Text.Trim());

                    if (intMoneyAmt > intDisUpbon)
                    {
                        e.RowError += "金額不可超過折扣上限金額!!";
                        return;
                    }

                    //if (intMoneyAmt == 0)
                    //{
                    //    e.RowError += "金額不可為0";
                    //    return;
                    //}
                }
                catch
                {
                    e.RowError += "金額請輸入數字格式!!";
                    return;
                }
            }
            long intDisRate=0;
            if (!string.IsNullOrEmpty(strDisRate))
            {
                try
                {
                     intDisRate = Convert.ToInt64(strDisRate);
                     if (intDisRate > 100)
                     {
                         e.RowError += "比率不可大於100!";
                         return;
                     }

                }
                catch
                {
                    e.RowError += "比率請輸入數字格式!!";
                    return;
                }
            }

            if (intMoneyAmt == 0 && intDisRate == 0)
            {
                e.RowError += "金額與比率不可同為0";
                return;
            }

            //判斷角色是否重複
            string SSDID = StringUtil.CStr(gvMaster.GetRowValues(gvMaster.FocusedRowIndex, gvMaster.KeyFieldName));  //主檔UUID
            string SSDDID = "";
            if (!e.IsNewRow)
            {
                SSDDID = StringUtil.CStr(e.Keys["SSDD_ID"]);
            }
            string strRoleID = StringUtil.CStr(((ASPxComboBox)gvDetail.FindEditRowCellTemplateControl((GridViewDataColumn)gvDetail.Columns["ROLE_NAME"], "ddlRole")).SelectedItem.Value);
            int intCount = OPT18_PageHelper.GetStoreSpecialDisDByRole(SSDID, SSDDID, strRoleID);
            if (intCount > 0)
            {
                e.RowError += "角色重複!!";
                return;

            }
            //判斷折扣上限金額不可大於折扣總額
            long strMasterMoneyAmt = Convert.ToInt64(gvMaster.GetRowValues(gvMaster.FocusedRowIndex, "DIS_AMT"));
            long SUM_DIS_AMT = Convert.ToInt64(((ASPxTextBox)gvDetail.FindEditRowCellTemplateControl((GridViewDataColumn)gvDetail.Columns["DIS_AMT_UBOND"], "txtDisUpbon")).Text.Trim());
            if (SUM_DIS_AMT > strMasterMoneyAmt)
            {
                e.RowError += "折扣上限金額加總不可大於折扣總額!!";
                return;

            }
        }
    }

    protected void gvDetail_HtmlRowPrepared(object sender, ASPxGridViewTableRowEventArgs e)
    {
        e.Row.Attributes["canSelect"] = "true";
        if (e.RowType == GridViewRowType.Data)
        {
            string Discount_Y = StringUtil.CStr(gvMaster.GetRowValues(gvMaster.FocusedRowIndex, "YYMM")).Substring(0, 4);
            string Discount_M = StringUtil.CStr(gvMaster.GetRowValues(gvMaster.FocusedRowIndex, "YYMM")).Substring(5, 2);

            if (Convert.ToInt32(Discount_Y) < DateTime.Now.Year || (Convert.ToInt32(Discount_Y) == DateTime.Now.Year && Convert.ToInt32(Discount_M) <= DateTime.Now.Month))
            {
                e.Row.Attributes["canSelect"] = "false";
            }
        }
        if (gvDetail.IsNewRowEditing)
        {

        }
        else if (gvDetail.IsEditing)
        {
            ASPxComboBox ddlRole = gvDetail.FindEditRowCellTemplateControl((GridViewDataColumn)gvDetail.Columns["ROLE_NAME"], "ddlRole") as ASPxComboBox;
            ddlRole.Text = StringUtil.CStr(e.GetValue("ROLE_NAME"));
        }

    }

    protected void gvDetail_HtmlEditFormCreated(object sender, ASPxGridViewEditFormEventArgs e)
    {
        BinRole(); //繫結角色資料
    }

    protected void gvDetail_StartRowEditing(object sender, DevExpress.Web.Data.ASPxStartRowEditingEventArgs e)
    {       
        gvDetail.Selection.UnselectAll();
    }

    protected void gvDetail_CommandButtonInitialize(object sender, ASPxGridViewCommandButtonEventArgs e)
    {

        if (e.VisibleIndex > -1)
        {
            if (e.ButtonType == ColumnCommandButtonType.Edit || e.ButtonType == ColumnCommandButtonType.SelectCheckbox)
            {
                string Discount_Y = StringUtil.CStr(gvMaster.GetRowValues(gvMaster.FocusedRowIndex, "YYMM")).Substring(0, 4);
                string Discount_M = StringUtil.CStr(gvMaster.GetRowValues(gvMaster.FocusedRowIndex, "YYMM")).Substring(5, 2);
                if (Convert.ToInt32(Discount_Y) < DateTime.Now.Year || (Convert.ToInt32(Discount_Y) == DateTime.Now.Year && Convert.ToInt32(Discount_M) <= DateTime.Now.Month))
                {
                    e.Enabled = false;
                }
            }
        }
    }

    #endregion

    //ajax 呼當前網頁的方式
    [System.Web.Services.WebMethod()]
    [System.Web.Script.Services.ScriptMethod()]
    static public string getStoreInfo(string StoreNO)
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

}
