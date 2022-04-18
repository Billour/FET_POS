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

using Advtek.Utility;
using FET.POS.Model.Common;
using FET.POS.Model.DTO;
using FET.POS.Model.Facade;
using FET.POS.Model.Facade.FacadeImpl;
using DevExpress.Web.ASPxEditors;

public partial class VSS_OPT_OPT12 : BasePage
{
    private string QryUSER = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        QryUSER = logMsg.OPERATOR;
        if (!IsPostBack && !IsCallback)
        {
            //進入HappyGo點數累點設定預設帶出已生效的資料
            gvMaster.DataSource = new OPT12_Facade().Query_HgAccumulate("-1","","", "", "", "", "");
                //new OPT12_Facade().Query_HgAccumulate_Effective();
            gvMaster.DataBind();
        }
    }

    private void BindMasterData()
    {
        gvMaster.Selection.UnselectAll();
        gvMaster.DataSource = new OPT12_Facade().Query_HgAccumulate(this.txtAccuName.Text,
            this.txtSDateS.Text, this.txtSDateE.Text, this.txtDividablePointS.Text,
            this.txtDividablePointE.Text, this.txtAccuCurrencyS.Text, this.txtAccuCurrencyE.Text);
        gvMaster.DataBind();

    }

    private void BindConditionData()
    {
        gvCondition.Selection.UnselectAll();
        gvCondition.DataSource = new OPT12_Facade().Query_HgAccuExcludeProd();
        gvCondition.DataBind();
    }

    private void BinDiscountProduct()
    {
        ASPxComboBox ddlDiscount = gvMaster.FindEditFormTemplateControl("ddlDiscount") as ASPxComboBox;
        ddlDiscount.DataSource = new Product_Facade().Query_DiscountProduct();
        ddlDiscount.DataBind();
    }

    #region Button 觸發事件

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        gvMaster.Selection.UnselectAll();
        BindMasterData();
    }

    protected void btnAddNewM_Click(object sender, EventArgs e)
    {
        gvMaster.Selection.UnselectAll();
        gvMaster.AddNewRow();
    }

    protected void btnAddNewC_Click(object sender, EventArgs e)
    {
        gvCondition.Selection.UnselectAll();
        gvCondition.AddNewRow(); 

    }

    protected void btnDeleteM_Click(object sender, EventArgs e)
    {
        OPT12_HgAccumulate_DTO OPT12_DTO = new OPT12_HgAccumulate_DTO();
        OPT12_HgAccumulate_DTO.HG_ACCUMULATEDataTable dt = OPT12_DTO.HG_ACCUMULATE;
        OPT12_Facade facade = new OPT12_Facade();

        List<object> keyValues = this.gvMaster.GetSelectedFieldValues(gvMaster.KeyFieldName);
        foreach (string key in keyValues)
        {
            OPT12_HgAccumulate_DTO.HG_ACCUMULATERow dr = dt.NewHG_ACCUMULATERow();

            DataRow dirtyDr = OPT12_PageHelper.Query_HgAccumulate_ByKey(key).Rows[0];

            dr.ACCU_ID = StringUtil.CStr(dirtyDr["ACCU_ID"]);
            dr.ACCU_NO = StringUtil.CStr(dirtyDr["ACCU_NO"]);
            dr.ACCU_NAME = StringUtil.CStr(dirtyDr["ACCU_NAME"]);
            dr.S_DATE = Convert.ToDateTime(dirtyDr["S_DATE"]);
            if (!string.IsNullOrEmpty(StringUtil.CStr(dirtyDr["E_DATE"])))
            {
                dr.E_DATE = Convert.ToDateTime(dirtyDr["E_DATE"]);
            }
            dr.ACCU_CURRENCY = Convert.ToInt64(dirtyDr["ACCU_CURRENCY"]);
            dr.DIVIDABLE_POINT = Convert.ToInt64(dirtyDr["DIVIDABLE_POINT"]);
            dr.CREATE_USER = StringUtil.CStr(dirtyDr["CREATE_USER"]);
            dr.CREATE_DTM = Convert.ToDateTime(dirtyDr["CREATE_DTM"]);
            dr.MODI_USER = this.QryUSER;
            dr.MODI_DTM = System.DateTime.Now;
            dr.DELETE_FLAG = "Y";
            dt.Rows.Add(dr);

            OPT12_DTO.AcceptChanges();
        }

        if (OPT12_DTO.HG_ACCUMULATE.Rows.Count > 0)
        {
            //更新資料庫
            facade.Delete_HgAccumulate(OPT12_DTO);
            BindMasterData();
        }
    }

    protected void btnDeleteC_Click(object sender, EventArgs e)
    {
        OPT12_HgAccumulate_DTO OPT12_DTO = new OPT12_HgAccumulate_DTO();
        OPT12_HgAccumulate_DTO.HG_ACCU_EXCLUDE_PRODDataTable dt = OPT12_DTO.HG_ACCU_EXCLUDE_PROD;
        OPT12_Facade facade = new OPT12_Facade();

        List<object> keyValues = this.gvCondition.GetSelectedFieldValues(gvCondition.KeyFieldName);
        foreach (string key in keyValues)
        {
            OPT12_HgAccumulate_DTO.HG_ACCU_EXCLUDE_PRODRow dr = dt.NewHG_ACCU_EXCLUDE_PRODRow();

            DataRow dirtyDr = OPT12_PageHelper.Query_HgAccuExcludeProd_ByKey(key).Rows[0];

            dr.SID = StringUtil.CStr(dirtyDr["SID"]);
            dr.PRODNO = StringUtil.CStr(dirtyDr["PRODNO"]);
            dr.CREATE_USER = StringUtil.CStr(dirtyDr["CREATE_USER"]);
            dr.CREATE_DTM = Convert.ToDateTime(dirtyDr["CREATE_DTM"]);
            dr.MODI_USER = this.QryUSER;
            dr.MODI_DTM = System.DateTime.Now;
            dr.DELETE_FLAG = "Y";
            dt.Rows.Add(dr);

            OPT12_DTO.AcceptChanges();
        }

        if (OPT12_DTO.HG_ACCU_EXCLUDE_PROD.Rows.Count > 0)
        {
            //更新資料庫
            facade.Delete_HgAccuExcludeProd(OPT12_DTO);
            BindConditionData();
        }
    }

    protected void btnClear_Click(object sender, EventArgs e)
    {
        gvMaster.Selection.UnselectAll();
        gvCondition.Selection.UnselectAll();
        this.txtAccuName.Text = "";
        this.txtSDateS.Text = "";
        this.txtSDateE.Text = "";
        this.txtDividablePointS.Text = "";
        this.txtDividablePointE.Text = "";
        this.txtAccuCurrencyS.Text = "";
        this.txtAccuCurrencyE.Text = "";
        
        gvMaster.DataSource = null;
        gvMaster.DataBind();

        gvCondition.DataSource = null;
        gvCondition.DataBind();

    }

    #endregion

    #region gvMaster 觸發事件

    protected void gvMaster_PageIndexChanged(object sender, EventArgs e)
    {
        ((ASPxGridView)sender).CancelEdit();
        gvMaster.Selection.UnselectAll();
        BindMasterData();
    }

    protected void gvMaster_HtmlRowPrepared(object sender, ASPxGridViewTableRowEventArgs e)
    {
        if (e.RowType == GridViewRowType.Data)
        {
            e.Row.Attributes["canSelect"] = "true";

            //已生效就不可勾選刪除此筆資料
            string date = StringUtil.CStr(gvMaster.GetRowValues(e.VisibleIndex, "S_DATE"));
            if (!string.IsNullOrEmpty(date) && Convert.ToDateTime(date) <= DateTime.Now)
            {
                e.Row.Attributes["canSelect"] = "false";
            }
        }

        if (gvMaster.IsEditing)
        {
            //ASPxComboBox ddlDiscount = gvMaster.FindEditFormTemplateControl("ddlDiscount") as ASPxComboBox;
            PopupControl ddlDiscount = gvMaster.FindEditFormTemplateControl("ddlDiscount") as PopupControl;
            ASPxDateEdit txtSDate = gvMaster.FindEditFormTemplateControl("txtSDate") as ASPxDateEdit;
            ASPxDateEdit txtEDate = gvMaster.FindEditFormTemplateControl("txtEDate") as ASPxDateEdit;
            ASPxTextBox txtAccuCurrency = gvMaster.FindEditFormTemplateControl("txtAccuCurrency") as ASPxTextBox;
            ASPxTextBox txtDividablePoint = gvMaster.FindEditFormTemplateControl("txtDividablePoint") as ASPxTextBox;

            ddlDiscount.Text = StringUtil.CStr(e.GetValue("ACCU_NO"));
            //只允許刪除未生效之資料(開始日期還沒到)
            //編輯：
            //1. 開始日期還沒到，都可以編輯
            //2. 開始日期到了，但還沒結束，只能編輯結束日期
            //3. 活動結束後，就不能編輯
            DateTime SystemDate = DateTime.Today;
            if (!string.IsNullOrEmpty(txtSDate.Text) && Convert.ToDateTime(txtSDate.Text) <= SystemDate)
            {
                ddlDiscount.Enabled = false;
                txtSDate.ReadOnly = true;
                txtSDate.Enabled = false;
                txtEDate.ReadOnly = false;
                txtAccuCurrency.ReadOnly = true;
                txtAccuCurrency.Enabled = false;
                txtDividablePoint.ReadOnly = true;
                txtDividablePoint.Enabled = false;
            }
        }
    }

    protected void gvMaster_HtmlEditFormCreated(object sender, ASPxGridViewEditFormEventArgs e)
    {
        //BinDiscountProduct(); //繫結 折扣商品
        ((ASPxDateEdit)e.EditForm.Controls[0].FindControl("txtSDate")).MinDate = DateTime.Today.AddDays(1);
        ((ASPxDateEdit)e.EditForm.Controls[0].FindControl("txtEDate")).MinDate = DateTime.Today.AddDays(1);
    }

    protected void gvMaster_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
    {
        OPT12_HgAccumulate_DTO OPT12_DTO = new OPT12_HgAccumulate_DTO();
        OPT12_HgAccumulate_DTO.HG_ACCUMULATEDataTable dt = OPT12_DTO.HG_ACCUMULATE;
        OPT12_HgAccumulate_DTO.HG_ACCUMULATERow dr = dt.NewHG_ACCUMULATERow();

        OPT12_Facade facade = new OPT12_Facade();

        //ASPxComboBox ddlDiscount = gvMaster.FindEditFormTemplateControl("ddlDiscount") as ASPxComboBox;
        PopupControl ddlDiscount = gvMaster.FindEditFormTemplateControl("ddlDiscount") as PopupControl;

        dr.ACCU_ID = GuidNo.getUUID();
        dr.ACCU_NO = ddlDiscount.Text; //StringUtil.CStr(e.NewValues["ACCU_NO"]);
        DataTable dtAccuName = new Product_Facade().Query_DiscountProduct(dr.ACCU_NO);
        if (dtAccuName.Rows.Count > 0)
        {
            string strAccuName = StringUtil.CStr(dtAccuName.Rows[0]["PRODNAME"]);
            if (strAccuName.Length > 50)
            {
                dr.ACCU_NAME = strAccuName.Substring(0, 50);
            }
            else
            {
                dr.ACCU_NAME = strAccuName;
            }
        }
        dr.S_DATE = Convert.ToDateTime(e.NewValues["S_DATE"]);
        if (e.NewValues["E_DATE"] != null && !string.IsNullOrEmpty(StringUtil.CStr(e.NewValues["E_DATE"])))
        {
            dr.E_DATE = Convert.ToDateTime(e.NewValues["E_DATE"]);
        }
        dr.ACCU_CURRENCY = Convert.ToInt64(e.NewValues["ACCU_CURRENCY"]);
        dr.DIVIDABLE_POINT = Convert.ToInt64(e.NewValues["DIVIDABLE_POINT"]);
        dr.CREATE_USER = this.QryUSER;
        dr.CREATE_DTM = System.DateTime.Now;
        dr.MODI_USER = this.QryUSER;
        dr.MODI_DTM = System.DateTime.Now;
        dr.DELETE_FLAG = "N";

        dt.Rows.Add(dr);
        OPT12_DTO.AcceptChanges();

        //更新資料庫
        facade.AddNewOne_HgAccumulate(OPT12_DTO);

        gvMaster.CancelEdit();
        e.Cancel = true;
        BindMasterData();
    }

    protected void gvMaster_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
    {
        OPT12_HgAccumulate_DTO OPT12_DTO = new OPT12_HgAccumulate_DTO();
        OPT12_HgAccumulate_DTO.HG_ACCUMULATEDataTable dt = OPT12_DTO.HG_ACCUMULATE;
        OPT12_HgAccumulate_DTO.HG_ACCUMULATERow dr = dt.NewHG_ACCUMULATERow();

        OPT12_Facade facade = new OPT12_Facade();

        DataRow dirtyDr = OPT12_PageHelper.Query_HgAccumulate_ByKey(StringUtil.CStr(e.Keys[gvMaster.KeyFieldName])).Rows[0];

        dr.ACCU_ID = StringUtil.CStr(dirtyDr["ACCU_ID"]);
        //ASPxComboBox ddlDiscount = gvMaster.FindEditFormTemplateControl("ddlDiscount") as ASPxComboBox;
        PopupControl ddlDiscount = gvMaster.FindEditFormTemplateControl("ddlDiscount") as PopupControl;

        dr.ACCU_NO = ddlDiscount.Text; //StringUtil.CStr(e.NewValues["ACCU_NO"]);
        dr.ACCU_NAME = StringUtil.CStr(e.NewValues["ACCU_NAME"]);
        
        dr.S_DATE = Convert.ToDateTime(e.NewValues["S_DATE"]);
        if (e.NewValues["E_DATE"] != null && !string.IsNullOrEmpty(StringUtil.CStr(e.NewValues["E_DATE"])))
        {
            dr.E_DATE = Convert.ToDateTime(e.NewValues["E_DATE"]);
        }
        dr.ACCU_CURRENCY = Convert.ToInt64(e.NewValues["ACCU_CURRENCY"]);
        dr.DIVIDABLE_POINT = Convert.ToInt64(e.NewValues["DIVIDABLE_POINT"]);
        dr.CREATE_USER = StringUtil.CStr(dirtyDr["CREATE_USER"]);
        dr.CREATE_DTM = Convert.ToDateTime(dirtyDr["CREATE_DTM"]);
        dr.MODI_USER = this.QryUSER;
        dr.MODI_DTM = System.DateTime.Now;
        dr.DELETE_FLAG = StringUtil.CStr(dirtyDr["DELETE_FLAG"]);

        dt.Rows.Add(dr);
        OPT12_DTO.AcceptChanges();

        //更新資料庫
        facade.UpdateOne_HgAccumulate(OPT12_DTO);

        gvMaster.CancelEdit();
        e.Cancel = true;
        BindMasterData();
    }

    protected void gvMaster_RowValidating(object sender, DevExpress.Web.Data.ASPxDataValidationEventArgs e)
    {
        if (e.IsNewRow || gvMaster.IsEditing)
        {
            string AccuID = "";
            if (e.Keys[gvMaster.KeyFieldName] != null)
            {
                AccuID = StringUtil.CStr(e.Keys[gvMaster.KeyFieldName]);
            }
            //string txtAccuNo = ((ASPxComboBox)gvMaster.FindEditFormTemplateControl("ddlDiscount")).SelectedItem.Text.Trim();
            string txtAccuNo = ((PopupControl)gvMaster.FindEditFormTemplateControl("ddlDiscount")).Text.Trim();
            if (string.IsNullOrEmpty(txtAccuNo))
            {
                e.RowError = "折扣料號不允許空值，請重新輸入!!";
                return;
            }
            int DateCount = 0;
            ASPxDateEdit EditSDate = gvMaster.FindEditFormTemplateControl("txtSDate") as ASPxDateEdit;
            string strSDate = EditSDate.Text.Trim();
            string strEDate = ((ASPxDateEdit)gvMaster.FindEditFormTemplateControl("txtEDate")).Text.Trim();
            if (string.IsNullOrEmpty(strSDate))
            {
                e.RowError = "開始日期不允許空值，請重新輸入!!";
                return;
            }
            DateTime SystemDate = DateTime.Today;
            DateTime dtS = Convert.ToDateTime(strSDate);
            if (!EditSDate.ReadOnly)
            {
                if (e.IsNewRow)
                {
                    if (dtS <= SystemDate)
                    {
                        e.RowError = "開始日期不允許小於或等於[系統日]，請重新輸入!!";
                        return;
                    }
                }
            }

            if (!string.IsNullOrEmpty(strEDate))
            {
                DateTime dtE = Convert.ToDateTime(((ASPxDateEdit)gvMaster.FindEditFormTemplateControl("txtEDate")).Text);

                if (dtE < dtS || dtE < SystemDate)
                {
                    e.RowError = "結束日期不允許小於[開始日期]及[系統日]，請重新輸入!!";
                    return;
                }

                DateCount += OPT12_PageHelper.Query_HgAccumulate_ByDate(AccuID, txtAccuNo, StringUtil.CStr(e.NewValues["S_DATE"]).Trim(), (e.NewValues["E_DATE"] == null) ? "" : StringUtil.CStr(e.NewValues["E_DATE"]).Trim());
            }

            DateCount += OPT12_PageHelper.Query_HgAccumulate_ByDate(AccuID, txtAccuNo, StringUtil.CStr(e.NewValues["S_DATE"]).Trim(), (e.NewValues["E_DATE"] == null) ? "" : StringUtil.CStr(e.NewValues["E_DATE"]).Trim());
            if (DateCount > 0)
            {
                e.RowError = "同一個折扣料號，其【開始日期】及【結束日期】不允許重疊有效!!";
                return;
            }

            string strName = OPT12_PageHelper.GetDiscountMaster(txtAccuNo);
            if (string.IsNullOrEmpty(strName))
            {
                e.RowError = "折扣料號不允許設定!!";
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
                {
                    string date = StringUtil.CStr(gvMaster.GetRowValues(e.VisibleIndex, "S_DATE"));
                    if (!string.IsNullOrEmpty(date) && Convert.ToDateTime(date) <= DateTime.Now)
                    {
                        e.Enabled = false;
                    }

                }
            }
        }
    }

    protected void gvMaster_StartRowEditing(object sender, DevExpress.Web.Data.ASPxStartRowEditingEventArgs e)
    {
        gvMaster.Selection.UnselectAll();
    }

    #endregion

    #region gvCondition 觸發事件

    protected void gvCondition_PageIndexChanged(object sender, EventArgs e)
    {
        ((ASPxGridView)sender).CancelEdit();
        gvCondition.Selection.UnselectAll();
        BindConditionData();
    }

    protected void gvCondition_HtmlRowPrepared(object sender, ASPxGridViewTableRowEventArgs e)
    {
    }

    protected void gvCondition_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
    {
        OPT12_HgAccumulate_DTO OPT12_DTO = new OPT12_HgAccumulate_DTO();
        OPT12_HgAccumulate_DTO.HG_ACCU_EXCLUDE_PRODDataTable dt = OPT12_DTO.HG_ACCU_EXCLUDE_PROD;
        OPT12_HgAccumulate_DTO.HG_ACCU_EXCLUDE_PRODRow dr = dt.NewHG_ACCU_EXCLUDE_PRODRow();

        OPT12_Facade facade = new OPT12_Facade();

        dr.SID = GuidNo.getUUID();
        dr.PRODNO = StringUtil.CStr(e.NewValues["PRODNO"]);
        dr.CREATE_USER = this.QryUSER;
        dr.CREATE_DTM = System.DateTime.Now;
        dr.MODI_USER = this.QryUSER;
        dr.MODI_DTM = System.DateTime.Now;
        dr.DELETE_FLAG = "N";

        dt.Rows.Add(dr);
        OPT12_DTO.AcceptChanges();

        //更新資料庫
        facade.AddNewOne_HgAccuExcludeProd(OPT12_DTO);

        gvCondition.CancelEdit();
        e.Cancel = true;
        BindConditionData();
    }

    protected void gvCondition_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
    {
        OPT12_HgAccumulate_DTO OPT12_DTO = new OPT12_HgAccumulate_DTO();
        OPT12_HgAccumulate_DTO.HG_ACCU_EXCLUDE_PRODDataTable dt = OPT12_DTO.HG_ACCU_EXCLUDE_PROD;
        OPT12_HgAccumulate_DTO.HG_ACCU_EXCLUDE_PRODRow dr = dt.NewHG_ACCU_EXCLUDE_PRODRow();

        OPT12_Facade facade = new OPT12_Facade();

        DataRow dirtyDr = OPT12_PageHelper.Query_HgAccuExcludeProd_ByKey(StringUtil.CStr(e.Keys[gvCondition.KeyFieldName])).Rows[0];

        dr.SID = StringUtil.CStr(dirtyDr["SID"]);
        dr.PRODNO = StringUtil.CStr(e.NewValues["PRODNO"]);
        dr.CREATE_USER = StringUtil.CStr(dirtyDr["CREATE_USER"]);
        dr.CREATE_DTM = Convert.ToDateTime(dirtyDr["CREATE_DTM"]);
        dr.MODI_USER = this.QryUSER;
        dr.MODI_DTM = System.DateTime.Now;
        dr.DELETE_FLAG = StringUtil.CStr(dirtyDr["DELETE_FLAG"]);

        dt.Rows.Add(dr);
        OPT12_DTO.AcceptChanges();

        //更新資料庫
        facade.UpdateOne_HgAccuExcludeProd(OPT12_DTO);

        gvCondition.CancelEdit();
        e.Cancel = true;
        BindConditionData();
    }

    protected void gvCondition_RowValidating(object sender, DevExpress.Web.Data.ASPxDataValidationEventArgs e)
    {
            string SID = "";
            if (e.Keys[gvCondition.KeyFieldName] != null)
            {
                SID = StringUtil.CStr(e.Keys[gvCondition.KeyFieldName]);
            }
            string txtProdNo = ((PopupControl)gvCondition.FindEditRowCellTemplateControl((GridViewDataColumn)gvCondition.Columns["PRODNO"], "txtProdNo")).Text.Trim();

            int DataCount = OPT12_PageHelper.Query_HgAccuExcludeProd_ByProdNo(txtProdNo, SID);
            if (DataCount > 0)
            {
                e.RowError = "商品料號重複!!";
                return;
            }

            DataTable dtName = new Product_Facade().Query_ProductInfo(txtProdNo);
            if (dtName.Rows.Count == 0)
            {
                e.RowError = "商品料號不存在!!";
                return;
            }
    }

    protected void gvCondition_StartRowEditing(object sender, DevExpress.Web.Data.ASPxStartRowEditingEventArgs e)
    {
        gvCondition.Selection.UnselectAll();
    }

    #endregion

    protected void ASPxPageControl1_ActiveTabChanged(object source, DevExpress.Web.ASPxTabControl.TabControlEventArgs e)
    {
        switch (this.ASPxPageControl1.ActiveTabIndex)
        {
            case 0:
                break;

            case 1:
                BindConditionData();
                break;

            default:
                break;
        }
    }

    //ajax 呼當前網頁的方式
    [System.Web.Services.WebMethod()]
    [System.Web.Script.Services.ScriptMethod()]
    static public string getPRODINFOExtraSale(string PRODNO)
    {
        DataTable dt = new Product_Facade().Query_DiscountProduct(PRODNO);
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

    [System.Web.Services.WebMethod()]
    [System.Web.Script.Services.ScriptMethod()]
    static public string getInfo(string No)
    {
        string strInfo = "";
        if (!string.IsNullOrEmpty(No))
        {
            DataTable dt = new Product_Facade().Query_ProductInfo(No);
            if (dt.Rows.Count > 0)
            {
                strInfo = StringUtil.CStr(dt.Rows[0]["PRODNAME"]);
            }
        }

        return strInfo;
    }


}