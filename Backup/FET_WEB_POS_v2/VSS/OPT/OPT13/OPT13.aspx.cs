using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using DevExpress.Web;
using DevExpress.Web.ASPxGridView;
using DevExpress.Web.ASPxPager;
using DevExpress.Web.ASPxEditors;
using System.Reflection;
using Microsoft.VisualBasic;
using Advtek.Utility;

using FET.POS.Model.DTO;
using FET.POS.Model.Common;
using FET.POS.Model.Facade;
using FET.POS.Model.Facade.FacadeImpl;
using FET.POS.Model.Helper;

using System.Web.UI.HtmlControls;
using NPOI.HSSF.UserModel;


public partial class VSS_OPT_OPT13 : BasePage
{

    protected void Page_Load(object sender, EventArgs e)
    {   
        if (!IsPostBack)
        {
            // 繫結空的資料表，以顯示表頭欄位
            gvActivity.DataSource = GetEmptyDataTable();
            gvActivity.DataBind();
        }
        else
        {
            if (ViewState["ACTIVITY_ID"] != null && StringUtil.CStr(ViewState["ACTIVITY_ID"]) != "")
            {
                if (OPT13_Facade.Query_HgMemberList(StringUtil.CStr(ViewState["ACTIVITY_ID"])) > 0)
                    lblImportStatus.Text = "名單已匯入";
                else
                    lblImportStatus.Text = "名單尚未匯入";
            }
        }      
    }

    public static bool IsNumber(string strNumber)
    {
        System.Text.RegularExpressions.Regex r = new System.Text.RegularExpressions.Regex(@"^\d+(\.)?\d*$"); return r.IsMatch(strNumber);
    }

    private DataTable GetEmptyDataTable()
    {
        DataTable dtResult = new DataTable();
        dtResult.Columns.Add("折扣料號", typeof(string));
        dtResult.Columns.Add("折扣名稱", typeof(string));
        dtResult.Columns.Add("商品料號", typeof(string));
        dtResult.Columns.Add("商品名稱", typeof(string));
        dtResult.Columns.Add("開始日期", typeof(DateTime));
        dtResult.Columns.Add("結束日期", typeof(DateTime));
        dtResult.Columns.Add("名單檢核", typeof(bool));
        dtResult.Columns.Add("折抵方式", typeof(string));
        dtResult.Columns.Add("折抵上限", typeof(int));
        dtResult.Columns.Add("折抵次數", typeof(int));
        dtResult.Columns.Add("更新日期", typeof(string));
        dtResult.Columns.Add("更新人員", typeof(string));
        return dtResult;
    }

    private DataTable getHgConvertDiscountActivity()
    {
        OPT13_Facade OPT13_Facade = new OPT13_Facade();

        return OPT13_Facade.Query_HgRestProdAct(PopupControl1.Text.Trim(), txtActivityName.Text,
                                                             dateEditSDate.Text, dateEditEDate.Text,
                                                             popupSPRODNO.Text, popupEPRODNO.Text,
                                                             txtProdName.Text);

    }

    private DataTable getHgConvertDisActProduct()
    {
        OPT13_Facade OPT13_Facade = new OPT13_Facade();
        return OPT13_Facade.Query_HgConvertDisActProduct(StringUtil.CStr(ViewState["ACTIVITY_ID"]));
    }

    private DataTable getHgConvertRestExchange()
    {
        OPT13_Facade OPT13_Facade = new OPT13_Facade();
        return OPT13_Facade.Query_HgConvertRestExchange(StringUtil.CStr(ViewState["ACTIVITY_ID"]));
    }

    private DataTable getHgConvertRestStore()
    {
        OPT13_Facade OPT13_Facade = new OPT13_Facade();
        return OPT13_Facade.Query_HgConvertRestStore(StringUtil.CStr(ViewState["ACTIVITY_ID"]));
    }

    private DataTable getHgConvertExtraSale()
    {
        OPT13_Facade OPT13_Facade = new OPT13_Facade();
        return OPT13_Facade.Query_HgConvertExtraSale(StringUtil.CStr(ViewState["ACTIVITY_ID"]));
    }

    protected void bindHgConvertDiscountActivity()
    {
        gvActivity.DataSource = getHgConvertDiscountActivity();
        gvActivity.DataBind();
    }

    protected void bindZoneDistrict()
    {
        ASPxComboBox ddlDistrict = gvHCR_Store.FindTitleTemplateControl("ddlDistrict") as ASPxComboBox;
        if (ddlDistrict != null)
        {
            ddlDistrict.DataSource = Common_PageHelper.getZone(true);
            ddlDistrict.TextField = "ZONE_NAME";
            ddlDistrict.ValueField = "ZONE";
            ddlDistrict.DataBind();
            ddlDistrict.SelectedIndex = 0;
        }

    }

    protected void bindHgConvertDisActProduct()
    {
        DataTable DT = getHgConvertDisActProduct();
        gvProduct.DataSource = DT;
        gvProduct.DataBind();
    }

    protected void bindHgConvertRestExchange()
    {
        gvHCR_Exchange.DataSource = getHgConvertRestExchange();
        gvHCR_Exchange.DataBind();
    }

    protected void bindHgConvertRestStore()
    {
        gvHCR_Store.DataSource = getHgConvertRestStore();
        gvHCR_Store.DataBind();
    }

    protected void bindHgConvertExtraSale()
    {
        gvHC_ExtraSale.DataSource = getHgConvertExtraSale();
        gvHC_ExtraSale.DataBind();
    }

    protected void ASPxPageControl1_PreRender(object sender, EventArgs e)
    {
        bindHgConvertDisActProduct();
        bindHgConvertRestExchange();
        bindHgConvertExtraSale();
        bindHgConvertRestStore();
    }

    protected void ASPxPageControl1_ActiveTabChanged(object source, DevExpress.Web.ASPxTabControl.TabControlEventArgs e)
    {//MantisBT #413 開始日期等於系統日應可接受
        if (Convert.ToDateTime(gvActivity.GetRowValues(gvActivity.FocusedRowIndex, "S_DATE")).AddDays(1) < DateTime.Now)
        {
            gvProduct.Enabled = false;
            gvHCR_Exchange.Enabled = false;
            gvHCR_Store.Enabled = false;
            gvHC_ExtraSale.Enabled = false;
            btnImport.Enabled = false;
            ASPxPopupControl1.Enabled = false;

            gvProduct.PagerBarEnabled = true;
            gvHCR_Exchange.PagerBarEnabled = true;
            gvHCR_Store.PagerBarEnabled = true;
            gvHC_ExtraSale.PagerBarEnabled = true;
        }
        else
        {
            gvProduct.Enabled = true;
            gvHCR_Exchange.Enabled = true;
            if (StringUtil.CStr(gvActivity.GetRowValues(gvActivity.FocusedRowIndex, "MEMBER_CHECK_FLAG")) == "True")
                gvHCR_Store.Enabled = false;
            else
                gvHCR_Store.Enabled = true;
            gvHC_ExtraSale.Enabled = true;
            btnImport.Enabled = true;
            ASPxPopupControl1.Enabled = true;

            gvProduct.PagerBarEnabled = true;
            gvHCR_Exchange.PagerBarEnabled = true;
            gvHCR_Store.PagerBarEnabled = true;
            gvHC_ExtraSale.PagerBarEnabled = true;
        }

        int a = this.ASPxPageControl1.ActiveTabIndex;
        if (a == 2 && StringUtil.CStr(gvActivity.GetRowValues(gvActivity.FocusedRowIndex, "MEMBER_CHECK_FLAG")) == "True")
        {
            gvHCR_Store.Enabled = false;
            gvHCR_Store.PagerBarEnabled = true;
            ScriptManager.RegisterClientScriptBlock(this, typeof(string), "CHECKPROD", "alert('此活動須檢核名單，不指定門市!');", true);
        }
        SetInitState_gvProduct();
        SetInitState_gvHCR_Exchange();
        SetInitState_gvHCR_Store();
        SetInitState_gvHC_ExtraSale();
    }

    #region gvActivity 相關觸發事件

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        bindHgConvertDiscountActivity();
        gvActivity.FocusedRowIndex = -1;
        gvActivity.PageIndex = 0;
        ASPxPageControl1.Visible = false;
    }    

    protected void btnAddNewActivity_Click(object sender, EventArgs e)
    {
        this.gvActivity.Selection.UnselectAll();
        if (gvActivity.VisibleRowCount == 0)
        {             
            DataTable dt = new DataTable();            
            dt.Columns.Add("ITEMNO", typeof(string));
            dt.Columns.Add("ACTIVITY_NO", typeof(string));
            dt.Columns.Add("ACTIVITY_NAME", typeof(string));
            dt.Columns.Add("S_DATE", typeof(DateTime));
            dt.Columns.Add("E_DATE", typeof(DateTime));
            dt.Columns.Add("MEMBER_CHECK_FLAG", typeof(Boolean));
            dt.Columns.Add("PAY_OFF_TYPE_NAME", typeof(string));
            dt.Columns.Add("U_BOUND", typeof(decimal));
            dt.Columns.Add("USE_COUNT", typeof(string));
            dt.Columns.Add("MODI_DTM", typeof(DateTime));
            dt.Columns.Add("MODI_USER", typeof(string));

            gvActivity.DataSource = dt;
            gvActivity.DataBind();        
        }

        gvActivity.AddNewRow();
    }

    protected void btnDelActivities_Click(object sender, EventArgs e)
    {
        OPT13_HgConvertibleRestricted_DTO OPT13_DTO = new OPT13_HgConvertibleRestricted_DTO();

        DataSet ds = new DataSet();
        DataTable dtHGCR = new DataTable();
        dtHGCR.TableName = OPT13_DTO.HG_CONVERTIBLE_RESTRICTED.TableName;
        dtHGCR.Columns.Add("ACTIVITY_ID");

        List<object> HGCRList = gvActivity.GetSelectedFieldValues(new string[] { "ACTIVITY_ID" });

        foreach (object li in HGCRList)
        {
            DataRow dr = dtHGCR.NewRow();
            dr["ACTIVITY_ID"] = StringUtil.CStr(li);
            dtHGCR.Rows.Add(dr);
        }

        DataTable dtHGCRE = new DataTable();
        dtHGCRE.TableName = OPT13_DTO.HG_CONVERT_REST_EXCHANGE.TableName;
        dtHGCRE.Columns.Add("ACTIVITY_ID");

        List<object> HGCREList = gvActivity.GetSelectedFieldValues(new string[] { "ACTIVITY_ID" });

        foreach (object li in HGCREList)
        {
            DataRow dr = dtHGCRE.NewRow();
            dr["ACTIVITY_ID"] = StringUtil.CStr(li);
            dtHGCRE.Rows.Add(dr);
        }

        DataTable dtHGCRS = new DataTable();
        dtHGCRS.TableName = OPT13_DTO.HG_CONVERT_REST_STORE.TableName;
        dtHGCRS.Columns.Add("ACTIVITY_ID");

        List<object> HGCRSList = gvActivity.GetSelectedFieldValues(new string[] { "ACTIVITY_ID" });

        foreach (object li in HGCRSList)
        {
            DataRow dr = dtHGCRS.NewRow();
            dr["ACTIVITY_ID"] = StringUtil.CStr(li);
            dtHGCRS.Rows.Add(dr);
        }

        DataTable dtHGCES = new DataTable();
        dtHGCES.TableName = OPT13_DTO.HG_CONVERT_EXTRA_SALE.TableName;
        dtHGCES.Columns.Add("ACTIVITY_ID");

        List<object> HGCESList = gvActivity.GetSelectedFieldValues(new string[] { "ACTIVITY_ID" });

        foreach (object li in HGCESList)
        {
            DataRow dr = dtHGCES.NewRow();
            dr["ACTIVITY_ID"] = StringUtil.CStr(li);
            dtHGCES.Rows.Add(dr);
        }

        DataTable dtHGCRP = new DataTable();
        dtHGCRP.TableName = OPT13_DTO.HG_CONVERT_REST_PROD.TableName;
        dtHGCRP.Columns.Add("ACTIVITY_ID");

        List<object> HGCRPList = gvActivity.GetSelectedFieldValues(new string[] { "ACTIVITY_ID" });

        foreach (object li in HGCRPList)
        {
            DataRow dr = dtHGCRP.NewRow();
            dr["ACTIVITY_ID"] = StringUtil.CStr(li);
            dtHGCRP.Rows.Add(dr);
        }

        DataTable dtHGCML = new DataTable();
        dtHGCML.TableName = OPT13_DTO.HG_CONVERT_MEMBER_LIST.TableName;
        dtHGCML.Columns.Add("ACTIVITY_ID");

        List<object> HGCMLList = gvActivity.GetSelectedFieldValues(new string[] { "ACTIVITY_ID" });

        foreach (object li in HGCMLList)
        {
            DataRow dr = dtHGCML.NewRow();
            dr["ACTIVITY_ID"] = StringUtil.CStr(li);
            dtHGCML.Rows.Add(dr);
        }

        //ds.Tables.Add(dtHGCRP);
        ds.Tables.Add(dtHGCR);
        ds.Tables.Add(dtHGCRE);
        ds.Tables.Add(dtHGCRS);
        ds.Tables.Add(dtHGCES);
        ds.Tables.Add(dtHGCRP);
        ds.Tables.Add(dtHGCML);
        ds.AcceptChanges();

        OPT13_Facade OPT13_Facade = new OPT13_Facade();
        OPT13_Facade.DeleteOne_HgRestProdAct(ds, "1");

        bindHgConvertDiscountActivity();

        gvActivity.FocusedRowIndex = -1;
        gvActivity.PageIndex = 0;
        ASPxPageControl1.Visible = false;
    }
    
    protected void gvActivity_HtmlRowPrepared(object sender, ASPxGridViewTableRowEventArgs e)
    {
        e.Row.Attributes["canSelect"] = "true";
        if (e.RowType == GridViewRowType.Data)
        {//MantisBT #413 開始日期等於系統日應可接受
            string date = StringUtil.CStr(gvActivity.GetRowValues(e.VisibleIndex, "S_DATE"));
            if (Convert.ToDateTime(date).AddDays(1) < DateTime.Now)
            {
                e.Row.Attributes["canSelect"] = "false";
            }
        }

        if (e.RowType == GridViewRowType.Data)
        {
            List<object> keyValues = this.gvActivity.GetSelectedFieldValues(gvActivity.KeyFieldName);
            foreach (string key in keyValues)
            {
                if (key == StringUtil.CStr(e.GetValue(gvActivity.KeyFieldName)))
                {
                    if (key == StringUtil.CStr(gvActivity.GetRowValues(gvActivity.FocusedRowIndex, gvActivity.KeyFieldName)))
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
    
    }

    protected void gvActivity_StartRowEditing(object sender, DevExpress.Web.Data.ASPxStartRowEditingEventArgs  e)
    {
        this.gvActivity.Selection.UnselectAll();
        if (gvActivity.IsEditing)
        {
            GridViewDataTextColumn col_ACTIVITY_NAME = (GridViewDataTextColumn)gvActivity.Columns["ACTIVITY_NAME"];
            DateTime S_Date = Convert.ToDateTime(gvActivity.GetRowValuesByKeyValue(e.EditingKeyValue, "S_DATE"));
            GridViewDataDateColumn colS_DATE = (GridViewDataDateColumn)gvActivity.Columns["S_DATE"];
            GridViewDataCheckColumn col_MEMBER_CHECK_FLAG = (GridViewDataCheckColumn)gvActivity.Columns["MEMBER_CHECK_FLAG"];
            GridViewDataComboBoxColumn col_PAY_OFF_TYPE_NAME = (GridViewDataComboBoxColumn)gvActivity.Columns["PAY_OFF_TYPE_NAME"];
            GridViewDataTextColumn col_U_BOUND = (GridViewDataTextColumn)gvActivity.Columns["U_BOUND"];
            GridViewDataTextColumn col_USE_COUNT = (GridViewDataTextColumn)gvActivity.Columns["USE_COUNT"];
            //MantisBT #413 開始日期等於系統日應可接受
            col_ACTIVITY_NAME.ReadOnly = true;
            if (S_Date.AddDays(1) < DateTime.Now && S_Date != null)
            {         
                colS_DATE.ReadOnly = true;
                col_MEMBER_CHECK_FLAG.ReadOnly = true;
                col_PAY_OFF_TYPE_NAME.ReadOnly = true;
                col_PAY_OFF_TYPE_NAME.PropertiesComboBox.ValidationSettings.RequiredField.IsRequired = false;
                col_PAY_OFF_TYPE_NAME.PropertiesComboBox.DropDownButton.Enabled = false;
                col_PAY_OFF_TYPE_NAME.PropertiesComboBox.DropDownButton.Visible = false;
                col_U_BOUND.ReadOnly = true;
                col_USE_COUNT.ReadOnly = true;

                if (StringUtil.CStr(gvActivity.GetRowValuesByKeyValue(e.EditingKeyValue, "E_DATE")).Trim() != "")
                {
                    DateTime E_Date = Convert.ToDateTime(gvActivity.GetRowValuesByKeyValue(e.EditingKeyValue, "E_DATE"));
                    if (E_Date < DateTime.Now && E_Date.ToShortDateString() != "0001/1/1")
                    {
                        GridViewDataDateColumn colE_DATE = (GridViewDataDateColumn)gvActivity.Columns["E_DATE"];
                        colE_DATE.ReadOnly = true;
                    }
                }
                gvProduct.Enabled = false;
                gvProduct.PagerBarEnabled = true;

            }else{
                colS_DATE.ReadOnly = false;
                col_MEMBER_CHECK_FLAG.ReadOnly = false;
                col_PAY_OFF_TYPE_NAME.ReadOnly = false;
                col_PAY_OFF_TYPE_NAME.PropertiesComboBox.ValidationSettings.RequiredField.IsRequired = false;
                col_PAY_OFF_TYPE_NAME.PropertiesComboBox.DropDownButton.Enabled = true;
                col_PAY_OFF_TYPE_NAME.PropertiesComboBox.DropDownButton.Visible = true;
                col_U_BOUND.ReadOnly = false;
                col_USE_COUNT.ReadOnly = false;
                gvProduct.Enabled = true;
                gvProduct.PagerBarEnabled = true;
            }
        }
    }

    protected void gvActivity_HtmlRowCreated(object sender, ASPxGridViewTableRowEventArgs e)
    {
        if (e.VisibleIndex == gvActivity.FocusedRowIndex
            && e.RowType == GridViewRowType.EditForm
            && e.RowType != GridViewRowType.EditingErrorRow)
        {
            DateTime S_Date = Convert.ToDateTime(e.GetValue("S_DATE"));
            GridViewDataTextColumn col_ACTIVITY_NO = (GridViewDataTextColumn)gvActivity.Columns["ACTIVITY_NO"];
            PopupControl stACTIVITY_NO_00 = ((PopupControl)gvActivity.FindEditRowCellTemplateControl(col_ACTIVITY_NO, "ACTIVITY_NO"));

            stACTIVITY_NO_00.Enabled = false;

        }

    }

    protected void gvActivity_FocusedRowChanged(object sender, EventArgs e)
    {        
        if (gvActivity.FocusedRowIndex >= 0)
        {
            int focusedRowIndex = gvActivity.FocusedRowIndex;
            ViewState["ACTIVITY_ID"] = gvActivity.GetRowValues(focusedRowIndex, gvActivity.KeyFieldName);
            string key = StringUtil.CStr(gvActivity.GetRowValues(gvActivity.FocusedRowIndex, "ACTIVITY_ID"));
            OPT13_Facade OPT13_Facade = new OPT13_Facade();
            DataTable dt = OPT13_Facade.Query_Flag(key);
            bindHgConvertDisActProduct();
            bindHgConvertRestExchange();
            bindHgConvertRestStore();
            bindHgConvertExtraSale();

            ASPxPageControl1.Visible = true;//MantisBT #413 開始日期等於系統日應可接受
            if (Convert.ToDateTime(gvActivity.GetRowValues(focusedRowIndex, "S_DATE")).AddDays(1) < DateTime.Now)
            {
                gvProduct.Enabled = false;
                gvHCR_Exchange.Enabled = false;
                gvHCR_Store.Enabled = false;
                gvHC_ExtraSale.Enabled = false;
                btnImport.Enabled = false;
                ASPxPopupControl1.Enabled = false;

                gvProduct.PagerBarEnabled = true;
                gvHCR_Exchange.PagerBarEnabled = true;
                gvHCR_Store.PagerBarEnabled = true;
                gvHC_ExtraSale.PagerBarEnabled = true;
            }
            else
            {
                gvProduct.Enabled = true;
                gvProduct.PagerBarEnabled = true;
                gvHCR_Exchange.Enabled = true;
                if (StringUtil.CStr(gvActivity.GetRowValues(focusedRowIndex, "MEMBER_CHECK_FLAG")) == "True")
                    gvHCR_Store.Enabled = false;
                else
                    gvHCR_Store.Enabled = true;
                gvHC_ExtraSale.Enabled = true;
                btnImport.Enabled = true;
                ASPxPopupControl1.Enabled = true;

                gvProduct.PagerBarEnabled = true;
                gvHCR_Exchange.PagerBarEnabled = true;
                gvHCR_Store.PagerBarEnabled = true;
                gvHC_ExtraSale.PagerBarEnabled = true;
            }
            ASPxPageControl1.ActiveTabIndex = 0;
            //ASPxPopupControl1.ContentUrl = "~/VSS/OPT/OPT16.aspx?FUNCID=OPT13_1&DISCOUNTID=" + gvActivity.GetRowValues(focusedRowIndex, "ACTIVITY_NO") + "&UUID=" + gvActivity.GetRowValues(focusedRowIndex, gvActivity.KeyFieldName);

            //**2011/04/27 Tina：傳遞參數時，要先以加密處理。
            string encryptUrl = Utils.Param_Encrypt("FUNCID=OPT13_1&DISCOUNTID=" + gvActivity.GetRowValues(focusedRowIndex, "ACTIVITY_NO") + "&UUID=" + gvActivity.GetRowValues(focusedRowIndex, gvActivity.KeyFieldName));
            ASPxPopupControl1.ContentUrl = string.Format("~/VSS/OPT/OPT16.aspx?Param={0}", encryptUrl);

            SetInitState_gvProduct();
            SetInitState_gvHCR_Exchange();
            SetInitState_gvHCR_Store();
            SetInitState_gvHC_ExtraSale();
        }
    }

    protected void gvActivity_PageIndexChanged(object sender, EventArgs e)
    {
        ASPxGridView grid = sender as ASPxGridView;
        grid.DataSource = getHgConvertDiscountActivity();
        grid.DataBind();
        grid.FocusedRowIndex = -1;
        ASPxPageControl1.Visible = false;
        grid.Selection.UnselectAll();
        grid.CancelEdit();
    }

    protected void gvActivity_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
    {
        OPT13_HgConvertibleRestricted_DTO OPT13_DTO = new OPT13_HgConvertibleRestricted_DTO();

        ASPxGridView grid = (ASPxGridView)sender;

        OPT13_HgConvertibleRestricted_DTO.HG_CONVERT_REST_PRODDataTable dtHGCRP = OPT13_DTO.HG_CONVERT_REST_PROD;
        OPT13_HgConvertibleRestricted_DTO.HG_CONVERTIBLE_RESTRICTEDDataTable dtHGCR = OPT13_DTO.HG_CONVERTIBLE_RESTRICTED;

        string SID = GuidNo.getUUID();
        string ACTIVITY_ID = GuidNo.getUUID();
        string PROMO_ID = GuidNo.getUUID();

        OPT13_HgConvertibleRestricted_DTO.HG_CONVERT_REST_PRODRow drHGCRP = dtHGCRP.NewHG_CONVERT_REST_PRODRow();
        OPT13_HgConvertibleRestricted_DTO.HG_CONVERTIBLE_RESTRICTEDRow drHGCR = dtHGCR.NewHG_CONVERTIBLE_RESTRICTEDRow();

        //dtHGCRP collect properties
        drHGCRP.SID = SID;
        drHGCRP.ACTIVITY_ID = ACTIVITY_ID;
        drHGCRP.PRODNO = StringUtil.CStr(e.NewValues["ACTIVITY_NO"]).Trim();
        drHGCRP.MODI_DTM = Convert.ToDateTime(System.DateTime.Now);
        drHGCRP.MODI_USER = this.logMsg.OPERATOR;
        drHGCRP.CREATE_DTM = Convert.ToDateTime(System.DateTime.Now);
        drHGCRP.MODI_USER = this.logMsg.OPERATOR;

        dtHGCRP.Rows.Add(drHGCRP);

        //dtHGCR collect properties        
        drHGCR.ACTIVITY_ID = ACTIVITY_ID;
        drHGCR.ACTIVITY_NAME = StringUtil.CStr(e.NewValues["ACTIVITY_NAME"]);

        drHGCR.PAY_OFF_TYPE = StringUtil.CStr(e.NewValues["PAY_OFF_TYPE_NAME"]);

        drHGCR.MEMBER_CHECK_FLAG = (StringUtil.CStr(e.NewValues["MEMBER_CHECK_FLAG"]) == "True") ? "Y" : "N";
        drHGCR.CREATE_DTM = Convert.ToDateTime(System.DateTime.Now);
        drHGCR.CREATE_USER = this.logMsg.OPERATOR;
        drHGCR.S_DATE = Convert.ToDateTime(e.NewValues["S_DATE"]);
        if (e.NewValues["E_DATE"] != null && !string.IsNullOrEmpty(StringUtil.CStr(e.NewValues["E_DATE"])))
        {
            drHGCR.E_DATE = Convert.ToDateTime(e.NewValues["E_DATE"]);
        }
        drHGCR.MODI_DTM = Convert.ToDateTime(System.DateTime.Now);
        drHGCR.MODI_USER = this.logMsg.OPERATOR;
        drHGCR.PRODNO = StringUtil.CStr(e.NewValues["ACTIVITY_NO"]).Trim();
        drHGCR.ACTIVITY_NO = StringUtil.CStr(e.NewValues["ACTIVITY_NO"]).Trim();
        drHGCR.PROMO_ID = PROMO_ID;
        drHGCR.U_BOUND = Convert.ToDecimal(e.NewValues["U_BOUND"]);
        if (e.NewValues["USE_COUNT"] == null || StringUtil.CStr(e.NewValues["USE_COUNT"]).Trim() == "")
            drHGCR.USE_COUNT = Convert.ToDecimal("0");
        else
            drHGCR.USE_COUNT = Convert.ToDecimal(e.NewValues["USE_COUNT"]);

        drHGCR.CONVERT_REST_TYPE = "1";
        dtHGCR.Rows.Add(drHGCR);


        OPT13_DTO.AcceptChanges();

        OPT13_Facade Facade = new OPT13_Facade();
        Facade.AddNewOne_HgRestProdAct(OPT13_DTO);

        gvActivity.CancelEdit();
        e.Cancel = true;

        bindHgConvertDiscountActivity();
        gvActivity.PageIndex = gvActivity.PageCount - 1;

    }

    protected void gvAcitvity_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
    {
        OPT13_HgConvertibleRestricted_DTO OPT13_DTO = new OPT13_HgConvertibleRestricted_DTO();

        ASPxGridView grid = (ASPxGridView)sender;

        OPT13_HgConvertibleRestricted_DTO.HG_CONVERTIBLE_RESTRICTEDDataTable dtHGCR = OPT13_DTO.HG_CONVERTIBLE_RESTRICTED;

        OPT13_HgConvertibleRestricted_DTO.HG_CONVERTIBLE_RESTRICTEDRow drHGCR = dtHGCR.NewHG_CONVERTIBLE_RESTRICTEDRow();

        DataRow dirtyDrHGCR = OPT13_PageHelper.GetHGCRByActivityId(StringUtil.CStr(e.Keys["ACTIVITY_ID"])).Rows[0];

        drHGCR.ACTIVITY_ID = StringUtil.CStr(dirtyDrHGCR["ACTIVITY_ID"]);
        drHGCR.ACTIVITY_NAME = StringUtil.CStr(e.NewValues["ACTIVITY_NAME"]);
        if (StringUtil.CStr(e.NewValues["PAY_OFF_TYPE_NAME"]) == "金額")
            drHGCR.PAY_OFF_TYPE = "0001";
        else if (StringUtil.CStr(e.NewValues["PAY_OFF_TYPE_NAME"]) == "百分比")
            drHGCR.PAY_OFF_TYPE = "0002";
        else
            drHGCR.PAY_OFF_TYPE = StringUtil.CStr(e.NewValues["PAY_OFF_TYPE_NAME"]);
        drHGCR.MEMBER_CHECK_FLAG = (StringUtil.CStr(e.NewValues["MEMBER_CHECK_FLAG"]) == "True") ? "Y" : "N";
        drHGCR.CREATE_DTM = Convert.ToDateTime(dirtyDrHGCR["CREATE_DTM"]);
        drHGCR.CREATE_USER = StringUtil.CStr(dirtyDrHGCR["CREATE_USER"]);
        drHGCR.S_DATE = Convert.ToDateTime(e.NewValues["S_DATE"]);
        if (e.NewValues["E_DATE"] != null && !string.IsNullOrEmpty(StringUtil.CStr(e.NewValues["E_DATE"])))
        {
            drHGCR.E_DATE = Convert.ToDateTime(e.NewValues["E_DATE"]);
        }
        else
            drHGCR["E_DATE"] = DBNull.Value;
        drHGCR.MODI_DTM = Convert.ToDateTime(System.DateTime.Now);
        drHGCR.MODI_USER = this.logMsg.OPERATOR;
        drHGCR.ACTIVITY_NO = StringUtil.CStr(e.NewValues["ACTIVITY_NO"]).Trim();
        drHGCR.PRODNO = StringUtil.CStr(dirtyDrHGCR["PRODNO"]);
        drHGCR.PROMO_ID = StringUtil.CStr(dirtyDrHGCR["PROMO_ID"]);
        drHGCR.U_BOUND = Convert.ToDecimal(e.NewValues["U_BOUND"]);
        if (e.NewValues["USE_COUNT"] == null || StringUtil.CStr(e.NewValues["USE_COUNT"]).Trim() == "")
            drHGCR.USE_COUNT = Convert.ToDecimal("0");
        else
            drHGCR.USE_COUNT = Convert.ToDecimal(e.NewValues["USE_COUNT"]);
        drHGCR.CONVERT_REST_TYPE = "1";
        dtHGCR.Rows.Add(drHGCR);


        OPT13_DTO.AcceptChanges();

        OPT13_Facade Facade = new OPT13_Facade();

        Facade.UpdateOne_HgRestProdAct(OPT13_DTO);

        gvActivity.CancelEdit();
        e.Cancel = true;

        bindHgConvertDiscountActivity();
        gvActivity.PageIndex = gvActivity.PageCount - 1;
    }

    protected void gvActivity_InitNewRow(object sender, DevExpress.Web.Data.ASPxDataInitNewRowEventArgs e)
    {
        e.NewValues["USE_COUNT"] = "";
        e.NewValues["MEMBER_CHECK_FLAG"] = false;

        GridViewDataTextColumn col_ACTIVITY_NAME = (GridViewDataTextColumn)gvActivity.Columns["ACTIVITY_NAME"];
        GridViewDataDateColumn colS_DATE = (GridViewDataDateColumn)gvActivity.Columns["S_DATE"];
        GridViewDataCheckColumn col_MEMBER_CHECK_FLAG = (GridViewDataCheckColumn)gvActivity.Columns["MEMBER_CHECK_FLAG"];
        GridViewDataComboBoxColumn col_PAY_OFF_TYPE_NAME = (GridViewDataComboBoxColumn)gvActivity.Columns["PAY_OFF_TYPE_NAME"];
        GridViewDataTextColumn col_U_BOUND = (GridViewDataTextColumn)gvActivity.Columns["U_BOUND"];
        GridViewDataTextColumn col_USE_COUNT = (GridViewDataTextColumn)gvActivity.Columns["USE_COUNT"];

        col_ACTIVITY_NAME.ReadOnly = true;
        colS_DATE.ReadOnly = false;
        col_MEMBER_CHECK_FLAG.ReadOnly = false;
        col_PAY_OFF_TYPE_NAME.ReadOnly = false;
        col_PAY_OFF_TYPE_NAME.PropertiesComboBox.ValidationSettings.RequiredField.IsRequired = true;
        col_PAY_OFF_TYPE_NAME.PropertiesComboBox.ValidationSettings.RequiredField.ErrorText = "必填欄位";
        col_PAY_OFF_TYPE_NAME.PropertiesComboBox.DropDownButton.Enabled = true;
        col_PAY_OFF_TYPE_NAME.PropertiesComboBox.DropDownButton.Visible = true;

        col_U_BOUND.ReadOnly = false;
        col_USE_COUNT.ReadOnly = false;
       
    }

    protected void gvActivity_CellEditorInitialize(object sender, ASPxGridViewEditorEventArgs e)
    {
        GridViewDataDateColumn colS_DATE = (GridViewDataDateColumn)gvActivity.Columns["S_DATE"];
        GridViewDataDateColumn colE_DATE = (GridViewDataDateColumn)gvActivity.Columns["E_DATE"];
        colS_DATE.PropertiesDateEdit.MinDate = DateTime.Now.Date.AddDays(0);
        colE_DATE.PropertiesDateEdit.MinDate = DateTime.Now.Date.AddDays(1);

        if (e.Column.FieldName == "PAY_OFF_TYPE_NAME")
        {
            
            ASPxComboBox combo = e.Editor as ASPxComboBox;

            combo.DataSource = OPT13_PageHelper.Get_PAY_OFF_TYPE();
            combo.ValueField = "PAY_OFF_TYPE";
            combo.TextField = "PAY_OFF_TYPE_NAME";
            combo.DataBind();

            if (!gvActivity.IsNewRowEditing)
            {
                if (StringUtil.CStr(gvActivity.GetRowValuesByKeyValue(e.KeyValue, "E_DATE")) != "")
                {
                    DateTime S_Date = Convert.ToDateTime(gvActivity.GetRowValuesByKeyValue(e.KeyValue, "S_DATE"));
                    DateTime E_Date = StringUtil.CStr(gvActivity.GetRowValuesByKeyValue(e.KeyValue, "E_DATE")) != "0001/1/1 上午 12:00:00" ? Convert.ToDateTime(gvActivity.GetRowValuesByKeyValue(e.KeyValue, "E_DATE")) : DateTime.MaxValue;
                    if (S_Date.AddDays(1) < DateTime.Now.Date && E_Date >= DateTime.Now.Date)
                        combo.Enabled = false;//MantisBT #413 開始日期等於系統日應可接受
                }
            }
        }
       
    }

    protected void gvActivity_CommandButtonInitialize(object sender, ASPxGridViewCommandButtonEventArgs e)
    {
        if (e.VisibleIndex > -1)
        {
           
            if (e.ButtonType == ColumnCommandButtonType.Edit)
            {

                if (!string.IsNullOrEmpty(StringUtil.CStr(gvActivity.GetRowValues(e.VisibleIndex, "E_DATE")))
                    && StringUtil.CStr(gvActivity.GetRowValues(e.VisibleIndex, "E_DATE")).Length > 4) // && StringUtil.CStr(gvActivity.GetRowValues(e.VisibleIndex, "E_DATE")).Substring(0,4) != "0001")
                {
                    if (StringUtil.CStr(gvActivity.GetRowValues(e.VisibleIndex, "E_DATE")).Substring(0, 4) != "0001")
                    {
                        if (Convert.ToDateTime(gvActivity.GetRowValues(e.VisibleIndex, "E_DATE")) < DateTime.Now)
                        {
                            e.Enabled = false;
                        }
                    }
                }
            }
            if (e.ButtonType == ColumnCommandButtonType.SelectCheckbox)
            {
                if (StringUtil.CStr(gvActivity.GetRowValues(e.VisibleIndex, "E_DATE")) != "")
                {//MantisBT #413 開始日期等於系統日應可接受
                    if (Convert.ToDateTime(gvActivity.GetRowValues(e.VisibleIndex, "S_DATE")).AddDays(1) < DateTime.Now)
                    {
                        e.Enabled = false;
                    }
                }
            }
        }
    }

    protected void gvActivity_RowValidating(object sender, DevExpress.Web.Data.ASPxDataValidationEventArgs e)
    {
        if (StringUtil.CStr(e.NewValues["ACTIVITY_NO"]) == "" || StringUtil.CStr(e.NewValues["ACTIVITY_NAME"]) == "")
        {
            e.RowError = "無此【折扣料號】，請重新入";
            return;
        }

        if (IsNumber(StringUtil.CStr(e.NewValues["U_BOUND"])))
        {
            string strName = OPT12_PageHelper.GetDiscountMaster(StringUtil.CStr(e.NewValues["ACTIVITY_NO"]));
            if (string.IsNullOrEmpty(strName))
            {
                e.RowError = "折扣料號不允許設定!!";
                return;
            }

            if (Convert.ToDecimal(StringUtil.CStr(e.NewValues["U_BOUND"])) <= 0)
            {
                e.RowError = "折抵上限不允許小於等於0，請重新輸入!!";
                return;
            }
            if (StringUtil.CStr(e.NewValues["PAY_OFF_TYPE_NAME"]) == "0002" || StringUtil.CStr(e.NewValues["PAY_OFF_TYPE_NAME"]) == "百分比")
            {
                if (Convert.ToInt32(e.NewValues["U_BOUND"]) > 100)
                {
                    e.RowError = "折抵上限不允許大於100，請重新輸入!!";
                    return;
                }
            }
            if (e.NewValues["USE_COUNT"] != null && StringUtil.CStr(e.NewValues["USE_COUNT"]).Trim() != "")
            {
                if (!IsNumber(StringUtil.CStr(e.NewValues["USE_COUNT"])))
                {
                    e.RowError = "【折扣次數】輸入字串非數字格式，請重新入";
                    return;
                }
                if (Convert.ToDecimal(StringUtil.CStr(e.NewValues["USE_COUNT"])) <= 0)
                {
                    e.RowError = "折扣次數不允許小於等於0，請重新輸入!!";
                    return;
                }
            }
            else
            {
                e.NewValues["USE_COUNT"] = "0";
            }

            string checkdate = System.DateTime.Now.ToString("yyyy/MM/dd");
            if (gvActivity.IsEditing)
            {
                GridViewDataDateColumn colS_DATE = (GridViewDataDateColumn)gvActivity.Columns["S_DATE"];
                if (gvActivity.IsNewRowEditing || colS_DATE.ReadOnly == false)
                {
                    if (System.DateTime.Parse(StringUtil.CStr(e.NewValues["S_DATE"])).AddDays(1) < System.DateTime.Parse(checkdate))
                    {//MantisBT #413 開始日期等於系統日應可接受
                        e.RowError = "起始日期須大於等於系統日期!!";
                        return;
                    }
                }
                if (e.NewValues["E_DATE"] != null && StringUtil.CStr(e.NewValues["E_DATE"]) != "")
                {
                    if (System.DateTime.Parse(StringUtil.CStr(e.NewValues["E_DATE"])) < System.DateTime.Parse(checkdate))
                    {
                        e.RowError = "結束日期須大於或等於系統日期!!";
                        return;
                    }
                    else if (System.DateTime.Parse(StringUtil.CStr(e.NewValues["S_DATE"])) >= System.DateTime.Parse(StringUtil.CStr(e.NewValues["E_DATE"])))
                    {
                        e.RowError = "起始日期須小於結束日期!!";
                        return;
                    }
                }
            }
            string EndDate = (e.NewValues["E_DATE"] != null && StringUtil.CStr(e.NewValues["E_DATE"]) != "") ? StringUtil.CStr(e.NewValues["E_DATE"]) : "";
            int DataCount = 0;
            if (gvActivity.IsNewRowEditing)
                DataCount = OPT13_Facade.Query_HgRestProdAct_ByProdNo(StringUtil.CStr(e.NewValues["ACTIVITY_NO"]), StringUtil.CStr(e.NewValues["S_DATE"]), EndDate, "", "1");
            else
                DataCount = OPT13_Facade.Query_HgRestProdAct_ByProdNo(StringUtil.CStr(e.NewValues["ACTIVITY_NO"]), StringUtil.CStr(e.NewValues["S_DATE"]), EndDate, StringUtil.CStr(ViewState["ACTIVITY_ID"]), "1");
            if (DataCount > 0)
            {
                e.RowError = "折扣料號重複!!";
                return;
            }
        }
        else
        {
            e.RowError = "【折扣上限】輸入字串非數字格式，請重新入";
            return;
        }
    }

    #endregion

    #region gvProduct 相關觸發事件

    protected void gvProduct_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
    {
        OPT13_HgConvertibleRestricted_DTO OPT13_DTO = new OPT13_HgConvertibleRestricted_DTO();
        OPT13_Facade OPT13_Facade = new OPT13_Facade();

            ASPxGridView grid = (ASPxGridView)sender;
            
            OPT13_HgConvertibleRestricted_DTO.HG_CONVERT_REST_PRODDataTable dtHGCRP = OPT13_DTO.HG_CONVERT_REST_PROD;
            OPT13_HgConvertibleRestricted_DTO.HG_CONVERT_REST_PRODRow drHGCRP = dtHGCRP.NewHG_CONVERT_REST_PRODRow();

            drHGCRP.SID = GuidNo.getUUID();
            drHGCRP.ACTIVITY_ID = StringUtil.CStr(ViewState["ACTIVITY_ID"]);
            drHGCRP.CREATE_DTM = Convert.ToDateTime(System.DateTime.Now);
            drHGCRP.CREATE_USER = this.logMsg.OPERATOR;
            drHGCRP.MODI_DTM = Convert.ToDateTime(System.DateTime.Now);
            drHGCRP.MODI_USER = this.logMsg.OPERATOR;
            drHGCRP.PRODNO = ((PopupControl)this.gvProduct.FindEditFormTemplateControl("tpopupPRODNO")).Text;

            dtHGCRP.Rows.Add(drHGCRP);
            OPT13_DTO.AcceptChanges();

            OPT13_Facade.AddNewOne_HgConvertDisActProduct(OPT13_DTO);

            grid.CancelEdit();
            e.Cancel = true;

            bindHgConvertDisActProduct();
    }

    protected void gvProduct_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
    {
        OPT13_HgConvertibleRestricted_DTO OPT13_DTO = new OPT13_HgConvertibleRestricted_DTO();
        OPT13_Facade OPT13_Facade = new OPT13_Facade();

        //若不使用DataSourceID時UPDATE INSERT 和DELPETE 都要自己控制 以下為UPDATE時需自行控制
        ASPxGridView grid = (ASPxGridView)sender;

        OPT13_HgConvertibleRestricted_DTO.HG_CONVERT_REST_PRODDataTable dtHGCRP = OPT13_DTO.HG_CONVERT_REST_PROD;
        OPT13_HgConvertibleRestricted_DTO.HG_CONVERT_REST_PRODRow drHGCRP = dtHGCRP.NewHG_CONVERT_REST_PRODRow();

        DataRow drDirty = OPT13_Facade.Query_HgConvertDisActProduct(StringUtil.CStr(ViewState["ACTIVITY_ID"])).AsEnumerable()
                                                            .Single(dr => dr.Field<string>("SID") == StringUtil.CStr(e.Keys["SID"]));

        drHGCRP.SID = StringUtil.CStr(e.Keys["SID"]);
        drHGCRP.ACTIVITY_ID = StringUtil.CStr(ViewState["ACTIVITY_ID"]);
        drHGCRP.MODI_DTM = Convert.ToDateTime(System.DateTime.Now);
        drHGCRP.MODI_USER = this.logMsg.OPERATOR;
        drHGCRP.PRODNO = ((PopupControl)this.gvProduct.FindEditFormTemplateControl("tpopupPRODNO")).Text;
        drHGCRP.CREATE_USER = StringUtil.CStr(drDirty["CREATE_USER"]) != "" ? StringUtil.CStr(drDirty["CREATE_USER"]) : logMsg.OPERATOR;
        drHGCRP.CREATE_DTM = StringUtil.CStr(drDirty["CREATE_DTM"]) != "" ? Convert.ToDateTime(drDirty["CREATE_DTM"]) : Convert.ToDateTime(System.DateTime.Now);

        dtHGCRP.Rows.Add(drHGCRP);
        OPT13_DTO.AcceptChanges();

        OPT13_Facade.UpdateOne_HgConvertDisActProduct(OPT13_DTO);

        grid.CancelEdit();
        e.Cancel = true;

        bindHgConvertDisActProduct();
    }

    protected void btnAddDisActProd_click(object sender, EventArgs e)
    {
        gvProduct.Selection.UnselectAll();
        if (gvProduct.VisibleRowCount == 0)
        {
            DataTable dt = new DataTable();

            dt.Columns.Add("ITEMNO", typeof(string));
            dt.Columns.Add("PRODNO", typeof(string));
            dt.Columns.Add("PRODNAME", typeof(string));

            gvProduct.DataSource = dt;
            gvProduct.DataBind();
        }

        gvProduct.AddNewRow();
    }

    protected void btnDelDisActProd_click(object sender, EventArgs e)
    {
            List<object> keyValues = this.gvProduct.GetSelectedFieldValues(gvProduct.KeyFieldName);



            OPT13_HgConvertibleRestricted_DTO OPT13_DTO = new OPT13_HgConvertibleRestricted_DTO();

            DataTable dt = new DataTable("HG_CONVERT_REST_PROD");
            dt.Columns.Add("SID");

            foreach (string key in keyValues)
            {
                DataRow dr = dt.NewRow();
                dr[0] = key;
                dt.Rows.Add(dr);
            }

            OPT13_Facade OPT13_Facade = new OPT13_Facade();
            OPT13_Facade.DeleteOne_HgConvertDisActProduct(dt);

            bindHgConvertDisActProduct();

    }

    protected void gvProduct_ProductsPopup_TextChanged(object sender, EventArgs e)
    {
        string strPRODNO = ((PopupControl)gvProduct.FindEditFormTemplateControl("tpopupPRODNO")).popTextBox.Text.Trim(); 

        if (!string.IsNullOrEmpty(strPRODNO))
        {
            DataTable dtProdNameNo = new Product_Facade().Query_ProductInfo(strPRODNO);

            try
            {
                DataRow drPRODNAME = dtProdNameNo.AsEnumerable().Single(
                                                                        dr => dr.Field<string>("PRODNO") == strPRODNO
                                                                        );
                if (drPRODNAME != null)
                {
                    ((Literal)gvProduct.FindEditFormTemplateControl("litPRODNAME")).Text = drPRODNAME.Field<string>("PRODNAME");
                }
                else
                {
                    ((Literal)gvProduct.FindEditFormTemplateControl("litPRODNAME")).Text = "對應不到商品代碼!";
                    return;
                }
            }
            catch (Exception)
            {
                ((PopupControl)gvProduct.FindEditFormTemplateControl("popupPRODNO")).Text = "對應不到商品代碼!";
                ((Literal)gvProduct.FindEditFormTemplateControl("litPRODNAME")).Text = "對應不到商品代碼!";
                return;
            }            
            
        }
    }

    protected void gvProduct_PageIndexChanged(object sender, EventArgs e)
    {
        bindHgConvertDisActProduct();
        gvProduct.FocusedRowIndex = -1;
        gvProduct.Selection.UnselectAll();
        gvProduct.CancelEdit();
    }

    protected void gvProduct_RowValidating(object sender, DevExpress.Web.Data.ASPxDataValidationEventArgs e)
    {
        DataTable dtPROD = new Product_Facade().Query_ProductInfo(StringUtil.CStr(e.NewValues["PRODNO"]).Trim());
        if (dtPROD.Rows.Count <= 0)
        {
            e.RowError = "商品料號不存在!!";
            return;
        }
        dtPROD = new Product_Facade().getPRODExtraSale(StringUtil.CStr(e.NewValues["PRODNO"]).Trim());
        if (dtPROD.Rows.Count <= 0)
        {
            e.RowError = "商品料號不允許設定!!";
            return;
        }

        int DataCount = 0;
        if (!gvProduct.IsNewRowEditing && gvProduct.IsEditing)
            DataCount = OPT13_Facade.Query_HgRestPROD(StringUtil.CStr(ViewState["ACTIVITY_ID"]), StringUtil.CStr(e.NewValues["PRODNO"]).Trim(), StringUtil.CStr(e.Keys[0]));
        else
            DataCount = OPT13_Facade.Query_HgRestPROD(StringUtil.CStr(ViewState["ACTIVITY_ID"]), StringUtil.CStr(e.NewValues["PRODNO"]).Trim(), "");
        if (DataCount > 0)
        {
            e.RowError = "此商品料號已存在，不可重複";
            return;
        }
    }

    protected void gvProduct_StartRowEditing(object sender, DevExpress.Web.Data.ASPxStartRowEditingEventArgs e)
    {
        this.gvProduct.Selection.UnselectAll();
    }

    #endregion

    #region gvHCR_Exchange 相關觸發事件

    protected void btnAddHGCRE_click(object sender, EventArgs e)
    {
        gvHCR_Exchange.Selection.UnselectAll();
        if (gvHCR_Exchange.VisibleRowCount == 0)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("ITEMNO", typeof(string));
            dt.Columns.Add("EXCHANGE_NAME", typeof(string));
            dt.Columns.Add("DIVIDABLE_POINT", typeof(string));
            dt.Columns.Add("CONVERT_CURRENCY", typeof(string));

            gvHCR_Exchange.DataSource = dt;
            gvHCR_Exchange.DataBind();
        }

        gvHCR_Exchange.AddNewRow();
    }

    protected void btnDelHGCRE_click(object sender, EventArgs e)
    {
        OPT13_HgConvertibleRestricted_DTO OPT13_DTO = new OPT13_HgConvertibleRestricted_DTO();

        DataSet ds = new DataSet();

        DataTable dtHGCRE = new DataTable();
        dtHGCRE.TableName = OPT13_DTO.HG_CONVERT_REST_EXCHANGE.TableName;
        dtHGCRE.Columns.Add("SID");

        List<object> HGCREList = this.gvHCR_Exchange.GetSelectedFieldValues(new string[] { "SID" });

        foreach (object li in HGCREList)
        {
            DataRow dr = dtHGCRE.NewRow();
            dr["SID"] = StringUtil.CStr(li);
            dtHGCRE.Rows.Add(dr);
        }

        OPT13_Facade OPT13_Facade = new OPT13_Facade();
        OPT13_Facade.DeleteOne_HgConvertRestExchange(dtHGCRE);

        bindHgConvertRestExchange();
    }

    protected void gvHCR_Exchange_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
    {
        OPT13_HgConvertibleRestricted_DTO OPT13_DTO = new OPT13_HgConvertibleRestricted_DTO();
        OPT13_Facade OPT13_Facade = new OPT13_Facade();

            ASPxGridView grid = (ASPxGridView)sender;

            OPT13_HgConvertibleRestricted_DTO.HG_CONVERT_REST_EXCHANGEDataTable dtHGCRE = OPT13_DTO.HG_CONVERT_REST_EXCHANGE;
            OPT13_HgConvertibleRestricted_DTO.HG_CONVERT_REST_EXCHANGERow drHGCRE = dtHGCRE.NewHG_CONVERT_REST_EXCHANGERow();

            drHGCRE.ACTIVITY_ID = StringUtil.CStr(ViewState["ACTIVITY_ID"]);
            drHGCRE.CONVERT_CURRENCY = Convert.ToDecimal(e.NewValues["CONVERT_CURRENCY"]);
            drHGCRE.DIVIDABLE_POINT = Convert.ToDecimal(e.NewValues["DIVIDABLE_POINT"]);
            if (e.NewValues["EXCHANGE_NAME"] == null)
            {
                drHGCRE.EXCHANGE_NAME = "";
            }
            else
            {
                drHGCRE.EXCHANGE_NAME = StringUtil.CStr(e.NewValues["EXCHANGE_NAME"]);
            }
            drHGCRE.MODI_DTM = Convert.ToDateTime(System.DateTime.Now);
            drHGCRE.MODI_USER = this.logMsg.OPERATOR;
            drHGCRE.CREATE_DTM = Convert.ToDateTime(System.DateTime.Now);
            drHGCRE.CREATE_USER = this.logMsg.OPERATOR;
            drHGCRE.SID = GuidNo.getUUID();

            dtHGCRE.Rows.Add(drHGCRE);

            OPT13_DTO.AcceptChanges();

            OPT13_Facade.AddNewOne_HgConvertRestExchange(OPT13_DTO);

            grid.CancelEdit();
            e.Cancel = true;

            bindHgConvertRestExchange();
    }

    protected void gvHCR_Exchange_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
    {
        OPT13_HgConvertibleRestricted_DTO OPT13_DTO = new OPT13_HgConvertibleRestricted_DTO();
        OPT13_Facade OPT13_Facade = new OPT13_Facade();

            ASPxGridView grid = (ASPxGridView)sender;

            OPT13_HgConvertibleRestricted_DTO.HG_CONVERT_REST_EXCHANGEDataTable dtHGCRE = OPT13_DTO.HG_CONVERT_REST_EXCHANGE;
            OPT13_HgConvertibleRestricted_DTO.HG_CONVERT_REST_EXCHANGERow drHGCRE = dtHGCRE.NewHG_CONVERT_REST_EXCHANGERow();

            DataTable dtDirty = OPT13_Facade.Query_HgConvertRestExchange(StringUtil.CStr(ViewState["ACTIVITY_ID"]));
            DataRow drDirty = dtDirty.AsEnumerable().Single(dr => dr.Field<string>("SID") == StringUtil.CStr(e.Keys["SID"]));            

            drHGCRE.ACTIVITY_ID = StringUtil.CStr(ViewState["ACTIVITY_ID"]);
            drHGCRE.CONVERT_CURRENCY = Convert.ToDecimal(e.NewValues["CONVERT_CURRENCY"]);
            drHGCRE.DIVIDABLE_POINT = Convert.ToDecimal(e.NewValues["DIVIDABLE_POINT"]);
            drHGCRE.EXCHANGE_NAME = StringUtil.CStr(e.NewValues["EXCHANGE_NAME"]);
            drHGCRE.MODI_DTM = Convert.ToDateTime(System.DateTime.Now);
            drHGCRE.MODI_USER = this.logMsg.OPERATOR;                        
            drHGCRE.SID = StringUtil.CStr(e.Keys["SID"]);
            drHGCRE.CREATE_DTM = Convert.ToDateTime(drDirty["CREATE_DTM"]);
            drHGCRE.CREATE_USER = StringUtil.CStr(drDirty["CREATE_USER"]);


            dtHGCRE.Rows.Add(drHGCRE);

            OPT13_DTO.AcceptChanges();

            OPT13_Facade.UpdateOne_HgConvertRestExchange(OPT13_DTO);

            grid.CancelEdit();
            e.Cancel = true;

            bindHgConvertRestExchange();
    }

    protected void gvHCR_Exchange_PageIndexChanged(object sender, EventArgs e)
    {
        bindHgConvertRestExchange();
        gvHCR_Exchange.FocusedRowIndex = -1;
        gvHCR_Exchange.Selection.UnselectAll();
        gvHCR_Exchange.CancelEdit();
    }

    protected void gvHCR_Exchange_RowValidating(object sender, DevExpress.Web.Data.ASPxDataValidationEventArgs e)
    {
        if (e.NewValues["DIVIDABLE_POINT"] != null && StringUtil.CStr(e.NewValues["DIVIDABLE_POINT"]).Trim() != "")
        {
            if (!IsNumber(StringUtil.CStr(e.NewValues["DIVIDABLE_POINT"])))
            {
                e.RowError = "【檢核點數】輸入字串非數字格式，請重新輸入!!";
                return;
            }
            else if (Convert.ToDecimal(e.NewValues["DIVIDABLE_POINT"]) <= 0)
            {
                e.RowError = "檢核點數不允許小於等於0，請重新輸入!!";
                return;
            }
        }
        if (e.NewValues["CONVERT_CURRENCY"] != null && StringUtil.CStr(e.NewValues["CONVERT_CURRENCY"]).Trim() != "")
        {
            if (!IsNumber(StringUtil.CStr(e.NewValues["CONVERT_CURRENCY"])))
            {
                e.RowError = "【兌換金額】輸入字串非數字格式，請重新輸入!!";
                return;
            }
            else if (Convert.ToDecimal(e.NewValues["CONVERT_CURRENCY"]) <= 0)
            {
                e.RowError = "兌換金額不允許小於等於0，請重新輸入!!";
                return;
            }
        }
        int DataCount = 0;
        if (!gvHCR_Exchange.IsNewRowEditing && gvHCR_Exchange.IsEditing)
            DataCount = OPT13_Facade.Query_HgRestExchange(StringUtil.CStr(ViewState["ACTIVITY_ID"]), Convert.ToInt32(e.NewValues["DIVIDABLE_POINT"]), StringUtil.CStr(e.Keys[0]));
        else
            DataCount = OPT13_Facade.Query_HgRestExchange(StringUtil.CStr(ViewState["ACTIVITY_ID"]), Convert.ToInt32(e.NewValues["DIVIDABLE_POINT"]), "");
        if (DataCount > 0)
        {
            e.RowError = "檢核點數重複!!";
            return;
        }
    }

    protected void gvHCR_Exchange_StartRowEditing(object sender, DevExpress.Web.Data.ASPxStartRowEditingEventArgs e)
    {
        gvHCR_Exchange.Selection.UnselectAll();
    }

    #endregion

    #region gvHC_ExtraSale 相關觸發事件

    protected void btnAddHGCES_click(object sender, EventArgs e)
    {
        gvHC_ExtraSale.Selection.UnselectAll();
        if (gvHC_ExtraSale.VisibleRowCount == 0)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("ITEMNO", typeof(string));
            dt.Columns.Add("PRODNO", typeof(string));
            dt.Columns.Add("PRODNAME", typeof(string));
            dt.Columns.Add("DIVIDABLE_POINT", typeof(string));
            dt.Columns.Add("EXTRA_SALE_PRICE", typeof(string));

            gvHC_ExtraSale.DataSource = dt;
            gvHC_ExtraSale.DataBind();
        }

        gvHC_ExtraSale.AddNewRow();
    }

    protected void btnDelHGCES_click(object sender, EventArgs e)
    {
        OPT13_HgConvertibleRestricted_DTO OPT13_DTO = new OPT13_HgConvertibleRestricted_DTO();

        DataSet ds = new DataSet();

        DataTable dtHGCES = new DataTable();
        dtHGCES.TableName = OPT13_DTO.HG_CONVERT_EXTRA_SALE.TableName;
        dtHGCES.Columns.Add("SID");

        List<object> HGCESList = gvHC_ExtraSale.GetSelectedFieldValues(new string[] { "SID" });

        foreach (object li in HGCESList)
        {
            DataRow dr = dtHGCES.NewRow();
            dr["SID"] = StringUtil.CStr(li);
            dtHGCES.Rows.Add(dr);
        }

        OPT13_Facade OPT13_Facade = new OPT13_Facade();
        OPT13_Facade.DeleteOne_HgConvertExtraSale(dtHGCES);

        bindHgConvertExtraSale();
    }

    protected void gvHC_ExtraSale_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
    {
        OPT13_HgConvertibleRestricted_DTO OPT13_DTO = new OPT13_HgConvertibleRestricted_DTO();
        OPT13_Facade OPT13_Facade = new OPT13_Facade();

            ASPxGridView grid = (ASPxGridView)sender;

            OPT13_HgConvertibleRestricted_DTO.HG_CONVERT_EXTRA_SALEDataTable dtHGCES = OPT13_DTO.HG_CONVERT_EXTRA_SALE;
            OPT13_HgConvertibleRestricted_DTO.HG_CONVERT_EXTRA_SALERow drHGCES = dtHGCES.NewHG_CONVERT_EXTRA_SALERow();

            drHGCES.ACTIVITY_ID = StringUtil.CStr(ViewState["ACTIVITY_ID"]);
            drHGCES.CREATE_DTM = Convert.ToDateTime(System.DateTime.Now);
            drHGCES.CREATE_USER = logMsg.OPERATOR;
            drHGCES.DIVIDABLE_POINT = Convert.ToDecimal(((ASPxTextBox)(grid.FindEditFormTemplateControl("txtDIVIDABLE_POINT"))).Text);
            drHGCES.EXTRA_SALE_PRICE = Convert.ToDecimal(((ASPxTextBox)(grid.FindEditFormTemplateControl("txtEXTRA_SALE_PRICE"))).Text);
            drHGCES.MODI_DTM = Convert.ToDateTime(System.DateTime.Now);
            drHGCES.MODI_USER = logMsg.OPERATOR;
            drHGCES.PRODNO = ((PopupControl)grid.FindEditFormTemplateControl("popupPRODNO")).Text;
            drHGCES.SID = GuidNo.getUUID();

            dtHGCES.Rows.Add(drHGCES);

            OPT13_DTO.AcceptChanges();

            OPT13_Facade.AddNewOne_HgConvertExtraSale(OPT13_DTO);

            grid.CancelEdit();
            e.Cancel = true;

            bindHgConvertExtraSale();
    }

    protected void gvHC_ExtraSale_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
    {
        OPT13_HgConvertibleRestricted_DTO OPT13_DTO = new OPT13_HgConvertibleRestricted_DTO();
        OPT13_Facade OPT13_Facade = new OPT13_Facade();

            ASPxGridView grid = (ASPxGridView)sender;

            OPT13_HgConvertibleRestricted_DTO.HG_CONVERT_EXTRA_SALEDataTable dtHGCES = OPT13_DTO.HG_CONVERT_EXTRA_SALE;
            OPT13_HgConvertibleRestricted_DTO.HG_CONVERT_EXTRA_SALERow drHGCES = dtHGCES.NewHG_CONVERT_EXTRA_SALERow();

            DataTable dtDirty = OPT13_Facade.Query_HgConvertExtraSale(StringUtil.CStr(ViewState["ACTIVITY_ID"]));
            DataRow drDirty = dtDirty.AsEnumerable().Single(dr => dr.Field<string>("SID") == StringUtil.CStr(e.Keys["SID"]));

            drHGCES.ACTIVITY_ID = StringUtil.CStr(ViewState["ACTIVITY_ID"]);
            drHGCES.DIVIDABLE_POINT = Convert.ToDecimal(((ASPxTextBox)(grid.FindEditFormTemplateControl("txtDIVIDABLE_POINT"))).Text);
            drHGCES.EXTRA_SALE_PRICE = Convert.ToDecimal(((ASPxTextBox)(grid.FindEditFormTemplateControl("txtEXTRA_SALE_PRICE"))).Text);
            drHGCES.MODI_DTM = Convert.ToDateTime(System.DateTime.Now);
            drHGCES.MODI_USER = logMsg.OPERATOR;
            drHGCES.PRODNO = ((PopupControl)grid.FindEditFormTemplateControl("popupPRODNO")).Text;
            drHGCES.SID = StringUtil.CStr(e.Keys["SID"]);
            drHGCES.CREATE_USER = StringUtil.CStr(drDirty["CREATE_USER"]);
            drHGCES.CREATE_DTM = Convert.ToDateTime(drDirty["CREATE_DTM"]);


            dtHGCES.Rows.Add(drHGCES);

            OPT13_DTO.AcceptChanges();

            OPT13_Facade.UpdateOne_HgConvertExtraSale(OPT13_DTO);

            grid.CancelEdit();
            e.Cancel = true;

            bindHgConvertExtraSale();
    }

    protected void gvHC_ExtraSale_PageIndexChanged(object sender, EventArgs e)
    {
        bindHgConvertExtraSale();
        gvHC_ExtraSale.FocusedRowIndex = -1;
        gvHC_ExtraSale.Selection.UnselectAll();
        gvHC_ExtraSale.CancelEdit();
    }

    protected void gvHC_ExtraSale_RowValidating(object sender, DevExpress.Web.Data.ASPxDataValidationEventArgs e)
    {
        if (e.NewValues["DIVIDABLE_POINT"] != null && StringUtil.CStr(e.NewValues["DIVIDABLE_POINT"]).Trim() != "")
        {
            if (!IsNumber(StringUtil.CStr(e.NewValues["DIVIDABLE_POINT"])))
            {
                e.RowError = "【兌換點數】輸入字串非數字格式，請重新輸入!!";
                return;
            }
            else if (Convert.ToDecimal(e.NewValues["DIVIDABLE_POINT"]) <= 0)
            {
                e.RowError = "兌換點數不允許小於等於0，請重新輸入!!";
                return;
            }
        }
        if (e.NewValues["EXTRA_SALE_PRICE"] != null && StringUtil.CStr(e.NewValues["EXTRA_SALE_PRICE"]).Trim() != "")
        {
            if (!IsNumber(StringUtil.CStr(e.NewValues["EXTRA_SALE_PRICE"])))
            {
                e.RowError = "加購價輸入字串非數字格式，請重新輸入!!";
                return;
            }
            else if (Convert.ToDecimal(e.NewValues["EXTRA_SALE_PRICE"]) <= 0)
            {
                e.RowError = "加購價不允許小於等於0，請重新輸入!!";
                return;
            }
        }
        DataTable dtPROD = new Product_Facade().Query_ProductInfo(StringUtil.CStr(e.NewValues["PRODNO"]).Trim());
        if (dtPROD.Rows.Count <= 0)
        {
            e.RowError = "商品料號不存在!!";
            return;
        }
        dtPROD = new Product_Facade().getPRODExtraSale(StringUtil.CStr(e.NewValues["PRODNO"]).Trim());
        if (dtPROD.Rows.Count <= 0)
        {
            e.RowError = "商品料號不允許設定!!";
            return;
        }
        int DataCount = 0;
        if (!gvHC_ExtraSale.IsNewRowEditing && gvHC_ExtraSale.IsEditing)
            DataCount = OPT13_Facade.Query_HgExtraSale(StringUtil.CStr(ViewState["ACTIVITY_ID"]), StringUtil.CStr(e.NewValues["PRODNO"]).Trim(), StringUtil.CStr(e.Keys[0]));
        else
            DataCount = OPT13_Facade.Query_HgExtraSale(StringUtil.CStr(ViewState["ACTIVITY_ID"]), StringUtil.CStr(e.NewValues["PRODNO"]).Trim(), "");
        if (DataCount > 0)
        {
            e.RowError = "此商品已存在，不可重複!!";
            return;
        }
    }

    protected void gvHC_ExtraSale_StartRowEditing(object sender, DevExpress.Web.Data.ASPxStartRowEditingEventArgs e)
    {
        gvHC_ExtraSale.Selection.UnselectAll();
    }

    #endregion

    #region gvHCR_Store 相關觸發事件

    protected void btnAddHGCRS_click(object sender, EventArgs e)
    {
        gvHCR_Store.Selection.UnselectAll();
        if (gvHCR_Store.VisibleRowCount == 0)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("ITEMNO", typeof(string));
            dt.Columns.Add("PRODNO", typeof(string));
            dt.Columns.Add("PRODNAME", typeof(string));
            dt.Columns.Add("DIVIDABLE_POINT", typeof(string));
            dt.Columns.Add("EXTRA_SALE_PRICE", typeof(string));

            gvHCR_Store.DataSource = dt;
            gvHCR_Store.DataBind();
        }

        gvHCR_Store.AddNewRow();
    }

    protected void btnDelHGCRS_click(object sender, EventArgs e)
    {
        OPT13_HgConvertibleRestricted_DTO OPT13_DTO = new OPT13_HgConvertibleRestricted_DTO();

        DataSet ds = new DataSet();

        DataTable dtHGCRS = new DataTable();
        dtHGCRS.TableName = OPT13_DTO.HG_CONVERT_REST_STORE.TableName;
        dtHGCRS.Columns.Add("SID");

        List<object> HGCESList = this.gvHCR_Store.GetSelectedFieldValues(new string[] { "SID" });

        foreach (object li in HGCESList)
        {
            DataRow dr = dtHGCRS.NewRow();
            dr["SID"] = StringUtil.CStr(li);
            dtHGCRS.Rows.Add(dr);
        }

        OPT13_Facade OPT13_Facade = new OPT13_Facade();
        OPT13_Facade.DeleteOne_HgConvertRestStore(dtHGCRS);

        bindHgConvertRestStore();
    }

    protected void btnDistrictSubmit_click(object sender, EventArgs e)
    {
        ASPxComboBox ddlDistrict = gvHCR_Store.FindTitleTemplateControl("ddlDistrict") as ASPxComboBox;

        OPT13_PageHelper.InsertStroeByZone(StringUtil.CStr(ddlDistrict.SelectedItem.Value),
                                           GuidNo.getUUID(),
                                           StringUtil.CStr(ViewState["ACTIVITY_ID"]),
                                           this.logMsg.OPERATOR, System.DateTime.Now,
                                           this.logMsg.OPERATOR, System.DateTime.Now);
        bindHgConvertRestStore();
        gvHCR_Store.PageIndex = 0;
        ScriptManager.RegisterClientScriptBlock(this, typeof(string), "CHECKPROD", "alert('門市匯入完成');", true);
    }

    protected void gvHCR_Store_PreRender(object sender, EventArgs e)
    {
        bindZoneDistrict();
    }

    protected void gvHCR_Store_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
    {
        OPT13_HgConvertibleRestricted_DTO OPT13_DTO = new OPT13_HgConvertibleRestricted_DTO();
        OPT13_Facade OPT13_Facade = new OPT13_Facade();

            ASPxGridView grid = (ASPxGridView)sender;

            OPT13_HgConvertibleRestricted_DTO.HG_CONVERT_REST_STOREDataTable dtHGCRS = OPT13_DTO.HG_CONVERT_REST_STORE;
            OPT13_HgConvertibleRestricted_DTO.HG_CONVERT_REST_STORERow drHGCRS = dtHGCRS.NewHG_CONVERT_REST_STORERow();

            drHGCRS.ACTIVITY_ID = StringUtil.CStr(ViewState["ACTIVITY_ID"]);
            drHGCRS.CREATE_DTM = Convert.ToDateTime(System.DateTime.Now);
            drHGCRS.CREATE_USER = this.logMsg.OPERATOR;
            drHGCRS.MODI_DTM = Convert.ToDateTime(System.DateTime.Now);
            drHGCRS.MODI_USER = this.logMsg.OPERATOR;
            drHGCRS.STORE_NO = StringUtil.CStr(e.NewValues["STORE_NO"]);
            drHGCRS.SID = GuidNo.getUUID();

            dtHGCRS.Rows.Add(drHGCRS);

            OPT13_DTO.AcceptChanges();

            OPT13_Facade.AddNewOne_HgConvertRestStore(OPT13_DTO);

            grid.CancelEdit();
            e.Cancel = true;


            bindHgConvertRestStore();
    }

    protected void gvHCR_Store_StartRowEditing(object sender, DevExpress.Web.Data.ASPxStartRowEditingEventArgs e)
    {
        gvHCR_Store.Selection.UnselectAll();
    }

    protected void gvHCR_Store_RowValidating(object sender, DevExpress.Web.Data.ASPxDataValidationEventArgs e)
    {
        if (StringUtil.CStr(e.NewValues["STORE_NO"]) == "")
        {
            e.RowError = "門市編號不存在!!";
            return;
        }
        DataTable dtStore = new Store_Facade().Query_StoreInfo(StringUtil.CStr(e.NewValues["STORE_NO"]));
        if (dtStore.Rows.Count <= 0)
        {
            e.RowError = "門市編號不存在!!";
            return;
        }
        int DataCount = 0;
        if (!gvHCR_Store.IsNewRowEditing && gvHCR_Store.IsEditing)
            DataCount = OPT13_Facade.Query_HgRestStore(StringUtil.CStr(ViewState["ACTIVITY_ID"]), StringUtil.CStr(e.NewValues["STORE_NO"]), StringUtil.CStr(e.Keys[0]));
        else
            DataCount = OPT13_Facade.Query_HgRestStore(StringUtil.CStr(ViewState["ACTIVITY_ID"]), StringUtil.CStr(e.NewValues["STORE_NO"]), "");
        if (DataCount > 0)
        {
            e.RowError = "此門市己存在，請重新輸入!!";
            return;
        }
    }

    protected void gvHCR_Store_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
    {
        OPT13_HgConvertibleRestricted_DTO OPT13_DTO = new OPT13_HgConvertibleRestricted_DTO();
        OPT13_Facade OPT13_Facade = new OPT13_Facade();

            ASPxGridView grid = (ASPxGridView)sender;

            OPT13_HgConvertibleRestricted_DTO.HG_CONVERT_REST_STOREDataTable dtHGCRS = OPT13_DTO.HG_CONVERT_REST_STORE;
            OPT13_HgConvertibleRestricted_DTO.HG_CONVERT_REST_STORERow drHGCRS = dtHGCRS.NewHG_CONVERT_REST_STORERow();

            DataRow drDirty =OPT13_Facade.Query_HgConvertRestStore(StringUtil.CStr(ViewState["ACTIVITY_ID"])).AsEnumerable()
                                                                .Single(dr => dr.Field<string>("SID") == StringUtil.CStr(e.Keys["SID"]));

            drHGCRS.ACTIVITY_ID = StringUtil.CStr(ViewState["ACTIVITY_ID"]);
            drHGCRS.MODI_DTM = Convert.ToDateTime(System.DateTime.Now);
            drHGCRS.MODI_USER = this.logMsg.OPERATOR;
            drHGCRS.STORE_NO = StringUtil.CStr(e.NewValues["STORE_NO"]);
            drHGCRS.SID = StringUtil.CStr(e.Keys["SID"]);
            drHGCRS.CREATE_USER = StringUtil.CStr(drDirty["CREATE_USER"]);
            drHGCRS.CREATE_DTM = Convert.ToDateTime(drDirty["CREATE_DTM"]);            

            dtHGCRS.Rows.Add(drHGCRS);

            OPT13_DTO.AcceptChanges();

            OPT13_Facade.UpdateOne_HgConvertRestStore(OPT13_DTO);

            grid.CancelEdit();
            e.Cancel = true;

            bindHgConvertRestStore();
    }

    protected void gvHCR_Store_PageIndexChanged(object sender, EventArgs e)
    {
        bindHgConvertRestStore();
        gvHCR_Store.FocusedRowIndex = -1;
        gvHCR_Store.Selection.UnselectAll();
        gvHCR_Store.CancelEdit();
    }

    #endregion

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
            DataTable dt = new Product_Facade().Query_ProductInfo(PRODNO);
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

    private void SetInitState_gvProduct()
    {
        gvProduct.PageIndex = 0;
        gvProduct.FocusedRowIndex = -1;
        gvProduct.Selection.UnselectAll();
        gvProduct.CancelEdit();
    }
    private void SetInitState_gvHCR_Exchange()
    {
        gvHCR_Exchange.PageIndex = 0;
        gvHCR_Exchange.FocusedRowIndex = -1;
        gvHCR_Exchange.Selection.UnselectAll();
        gvHCR_Exchange.CancelEdit();
    }
    private void SetInitState_gvHCR_Store()
    {
        gvHCR_Store.PageIndex = 0;
        gvHCR_Store.FocusedRowIndex = -1;
        gvHCR_Store.Selection.UnselectAll();
        gvHCR_Store.CancelEdit();
    }
    private void SetInitState_gvHC_ExtraSale()
    {
        gvHC_ExtraSale.PageIndex = 0;
        gvHC_ExtraSale.FocusedRowIndex = -1;
        gvHC_ExtraSale.Selection.UnselectAll();
        gvHC_ExtraSale.CancelEdit();
    }
}
