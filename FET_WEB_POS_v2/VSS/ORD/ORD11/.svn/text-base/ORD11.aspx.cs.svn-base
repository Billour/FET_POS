using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using DevExpress.Web.ASPxGridView;
using FET.POS.Model.Facade.FacadeImpl;
using FET.POS.Model.DTO;
using DevExpress.Web.ASPxEditors;
using FET.POS.Model.Common;
using Advtek.Utility;


public partial class VSS_ORD_ORD11 : BasePage
{
    bool IsgvMasterError = false;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            bindMasterData2();
        }
    }

    protected void bindMasterData()
    {
        DataTable dtResult = new ORD11_Facade().GetPORQMethodData(txtPRODNO.Text, txtPRODNAME.Text);
        ViewState["gvMaster"] = dtResult;
        gvMaster.DataSource = dtResult;
        gvMaster.DataBind();
        gvMaster.SettingsBehavior.AllowFocusedRow = true;
        gvMaster.FocusedRowIndex = -1;
    }

    protected void bindMasterData2()
    {
        ViewState["gvMaster2"] = null;
        DataTable dtResult = new ORD11_Facade().GetPORQMethodData2();
        ViewState["gvMaster2"] = dtResult;
        gvHead.DataSource = dtResult;
        gvHead.DataBind();
    }

    private string CheckSAFETY_VALUE(string v)
    {   
        string r = "";
        try
        {
            Decimal i = Convert.ToDecimal(v);
            if (i <= 0) r = "安全系數不允許小於等於0，請重新輸入";
        }
        catch //(Exception ex)
        {
             r = "全係數不允許空白";
        }

        return r;
    }

    #region Button 觸發事件

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        bindMasterData();
        gvMaster.Selection.UnselectAll();
        gvMaster.CancelEdit();
        gvMaster.PageIndex = 0;
        if (gvHead.IsEditing) gvHead.CancelEdit();
    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        Session["IsAddClick"] = "1";
        gvMaster.Selection.UnselectAll();
        if (ViewState["gvMaster"] == null)
        {
            DataTable dtResult = new FET.POS.Model.Facade.FacadeImpl.ORD11_Facade().GetPORQMethodData("NODATA", "NODATA");
            gvMaster.DataSource = dtResult;
            gvMaster.DataBind();
        }
        gvMaster.AddNewRow();
    }

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        ORD11_PORQ_DTO ds = new ORD11_PORQ_DTO();
        ORD11_PORQ_DTO.PROD_ORDER_RECOMMANDED_QTYDataTable dtm = ds.PROD_ORDER_RECOMMANDED_QTY;

        List<object> keyValues = this.gvMaster.GetSelectedFieldValues(gvMaster.KeyFieldName);
        foreach (DataColumn dc in dtm.Columns)
        { dc.AllowDBNull = true; }

        foreach (string key in keyValues)
        {
            DataTable dt = ViewState["gvMaster"] as DataTable ?? null;
            string[] keys = key.Split(new char[] { '|' });
            DataRow[] DRSelf = dt.Select("SID='" + keys[0] + "' AND PRODNO ='" + keys[1] + "' ");
                if (DRSelf.Length > 0)
                {
                    ORD11_PORQ_DTO.PROD_ORDER_RECOMMANDED_QTYRow drm = dtm.NewPROD_ORDER_RECOMMANDED_QTYRow();
                    drm.SID = keys[0];
                    drm.PRODNO = keys[1];
                    dtm.AddPROD_ORDER_RECOMMANDED_QTYRow(drm);
                }
        }
        ds.AcceptChanges();
        new ORD11_Facade().DeletePORQMethodData(ds);
        bindMasterData();
    }

    protected void btnClear_Click(object sender, EventArgs e)
    {
        Response.Redirect("ORD11.aspx");
    }

    #endregion

    #region gvHead 觸發的事件

    protected void gvHead_HtmlRowCreated(object sender, ASPxGridViewTableRowEventArgs e)
    {
        if (e.VisibleIndex == gvHead.FocusedRowIndex
            && e.RowType == GridViewRowType.InlineEdit
            && e.RowType != GridViewRowType.EditingErrorRow)
        {
            ASPxComboBox iSALE_REGION = gvHead.FindChildControl<ASPxComboBox>("cbSALE_REGION2");
            ASPxDateEdit colS_DATE = gvHead.FindChildControl<ASPxDateEdit>("S_DATE");
            ASPxDateEdit colE_DATE = gvHead.FindChildControl<ASPxDateEdit>("E_DATE");
            if (StringUtil.CStr(iSALE_REGION.SelectedItem.Value) == "2")
            {
                colS_DATE.ClientEnabled = true;
                colE_DATE.ClientEnabled = true;
                colS_DATE.ControlStyle.BackColor = System.Drawing.Color.White;
                colE_DATE.ControlStyle.BackColor = System.Drawing.Color.White;
            }
            else
            {
                colS_DATE.ClientEnabled = false;
                colE_DATE.ClientEnabled = false;
                colS_DATE.ControlStyle.BackColor = System.Drawing.Color.LightGray;
                colE_DATE.ControlStyle.BackColor = System.Drawing.Color.LightGray;
            }
        }
    }

    protected void gvHead_InitNewRow(object sender, DevExpress.Web.Data.ASPxDataInitNewRowEventArgs e)
    {
        ASPxGridView gv = (ASPxGridView)sender;
        ASPxComboBox cbSALE_REGION = gv.FindChildControl<ASPxComboBox>("cbSALE_REGION2");
        cbSALE_REGION.SelectedIndex = 0;
        ASPxTextBox txtSAFETY_VALUE = gv.FindChildControl<ASPxTextBox>("txtSAFETY_VALUE2");
    }

    protected void gvHead_StartRowEditing(object sender, DevExpress.Web.Data.ASPxStartRowEditingEventArgs e)
    {
        gvMaster.CancelEdit();
        gvMaster.Selection.UnselectAll();
        gvMaster.FocusedRowIndex = -1;
    }

    protected void gvHead_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
    {

        e.NewValues.Add("SID", StringUtil.CStr(ViewState["KeyValue"]));
        new ORD11_Facade().UpdatePORQMethodData2(e.NewValues, logMsg.MODI_USER);
        ((ASPxGridView)sender).CancelEdit();
        bindMasterData2();
        e.Cancel = true;
    }

    protected void gvHead_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
    {
        new ORD11_Facade().InsertPREGMethodData(e.NewValues, logMsg.MODI_USER);
        bindMasterData2();
        
        ((ASPxGridView)sender).CancelEdit();
        e.Cancel = true;
        
    }

    protected void gvHead_RowValidating(object sender, DevExpress.Web.Data.ASPxDataValidationEventArgs e)
    {
        ASPxGridView gv = (ASPxGridView)sender;
        ASPxComboBox iSALE_REGION = gv.FindChildControl<ASPxComboBox>("cbSALE_REGION2");
        ASPxTextBox iSAFETY_VALUE = gv.FindChildControl<ASPxTextBox>("txtSAFETY_VALUE2");
        string SID = "";
        if (!e.IsNewRow)
        {
            ViewState["KeyValue"] = e.Keys[0];
            SID = StringUtil.CStr(e.Keys[0]);
        }
        //check data:安全係數(SAFETY_VALUE)
        int iTmp;
        if (StringUtil.CStr(e.NewValues["SAFETY_VALUE"]).Trim() == "")
        {
            e.RowError += "安全係數不允許空白，請重新輸入!";
        }
        if (!Int32.TryParse(StringUtil.CStr(e.NewValues["SAFETY_VALUE"]), out iTmp))
        {
            e.RowError += "輸入字串不符合數字格式，請重新輸入!";
        }
        try
        {
            Decimal i = Convert.ToDecimal(StringUtil.CStr(e.NewValues["SAFETY_VALUE"]));
            if (i <= 0) e.RowError += "安全系數不允許小於等於0，請重新輸入";
        }
        catch //(Exception ex)
        {
            e.RowError += "安全係數不允許空白";
        }
        //檢查起訖期間是否有重疊
        string checkdate = System.DateTime.Now.ToString("yyyy/MM/dd");
        if (StringUtil.CStr(e.NewValues["SALE_REGION"]) == "2")
        {
            if (e.NewValues["S_DATE"] == null || string.IsNullOrEmpty(StringUtil.CStr(e.NewValues["S_DATE"]).Trim()))
            {
                e.RowError += "開始日期不允許空白";
                return;
            }
            else if (e.NewValues["E_DATE"] == null || string.IsNullOrEmpty(StringUtil.CStr(e.NewValues["E_DATE"]).Trim()))
            {
                e.RowError += "結束日期不允許空白";
                return;
            }
            else if (System.DateTime.Parse(StringUtil.CStr(e.NewValues["S_DATE"])) <= System.DateTime.Parse(checkdate))
            {
                e.RowError += "開始日期須大於系統日期!!";
            }
            else if (System.DateTime.Parse(StringUtil.CStr(e.NewValues["E_DATE"])) < System.DateTime.Parse(checkdate))
            {
                e.RowError += "結束日期須大於或等於系統日期!!";
            }
            else if (System.DateTime.Parse(StringUtil.CStr(e.NewValues["S_DATE"])) >= System.DateTime.Parse(StringUtil.CStr(e.NewValues["E_DATE"])))
            {
                e.RowError += "開始日期須小於結束日期!!";
            }
        }
     
    }

    protected void gvHead_CancelRowEditing(object sender, DevExpress.Web.Data.ASPxStartRowEditingEventArgs e)
    {
        gvMaster.Selection.UnselectAll();
        gvMaster.SettingsBehavior.AllowFocusedRow = true;
        gvMaster.FocusedRowIndex = -1;
    }

    #endregion

    #region gvMaster 觸發的事件

    protected void gvMaster_HtmlRowCreated(object sender, ASPxGridViewTableRowEventArgs e)
    {
        if (gvMaster.IsNewRowEditing)
        {
            ASPxGridView gv = (ASPxGridView)sender;
            ASPxComboBox cbSALE_REGION = gv.FindChildControl<ASPxComboBox>("cbSALE_REGION");
            if (!IsgvMasterError && StringUtil.CStr(Session["IsAddClick"]) == "1")
            {
                cbSALE_REGION.SelectedIndex = 0;
            }
            else
            {
                string prodNo = gvMaster.FindChildControl<PopupControl>("txtPROD_NO").Text;
                gvMaster.FindChildControl<ASPxLabel>("lblPRODNAME").Text = getProductInfo(prodNo);
            }
            ASPxTextBox txtSAFETY_VALUE = gv.FindChildControl<ASPxTextBox>("txtSAFETY_VALUE");
            ASPxDateEdit colS_DATE = gvMaster.FindChildControl<ASPxDateEdit>("S_DATE");
            ASPxDateEdit colE_DATE = gvMaster.FindChildControl<ASPxDateEdit>("E_DATE");
            if (cbSALE_REGION.SelectedIndex == -1)
            {
                colS_DATE.ClientEnabled = false;
                colE_DATE.ClientEnabled = false;
                colS_DATE.ControlStyle.BackColor = System.Drawing.Color.LightGray;
                colE_DATE.ControlStyle.BackColor = System.Drawing.Color.LightGray;
            }
            else
            {
                if (StringUtil.CStr(cbSALE_REGION.SelectedItem.Value) == "2")
                {
                    colS_DATE.ClientEnabled = true;
                    colE_DATE.ClientEnabled = true;
                    colS_DATE.ControlStyle.BackColor = System.Drawing.Color.White;
                    colE_DATE.ControlStyle.BackColor = System.Drawing.Color.White;
                }
                else
                {
                    colS_DATE.ClientEnabled = false;
                    colE_DATE.ClientEnabled = false;
                    colS_DATE.ControlStyle.BackColor = System.Drawing.Color.LightGray;
                    colE_DATE.ControlStyle.BackColor = System.Drawing.Color.LightGray;
                }
            }
        }
        else if (e.RowType == GridViewRowType.InlineEdit)
        {
            ASPxComboBox iSALE_REGION = gvMaster.FindChildControl<ASPxComboBox>("cbSALE_REGION");
            ASPxDateEdit colS_DATE = gvMaster.FindChildControl<ASPxDateEdit>("S_DATE");
            ASPxDateEdit colE_DATE = gvMaster.FindChildControl<ASPxDateEdit>("E_DATE");
            if (StringUtil.CStr(iSALE_REGION.SelectedItem.Value) == "2")
            {
                colS_DATE.ClientEnabled = true;
                colE_DATE.ClientEnabled = true;
                colS_DATE.ControlStyle.BackColor = System.Drawing.Color.White;
                colE_DATE.ControlStyle.BackColor = System.Drawing.Color.White;
            }
            else
            {
                colS_DATE.ClientEnabled = false;
                colE_DATE.ClientEnabled = false;
                colS_DATE.ControlStyle.BackColor = System.Drawing.Color.LightGray;
                colE_DATE.ControlStyle.BackColor = System.Drawing.Color.LightGray;
            }
        } 
    }

    protected void gvMaster_HtmlRowPrepared(object sender, ASPxGridViewTableRowEventArgs e)
    {
        if (e.RowType == GridViewRowType.Data)
        {
            List<object> keyValues = this.gvMaster.GetSelectedFieldValues(gvMaster.KeyFieldName);
            foreach (string key in keyValues)
            {
                string[] KeyFieldList = gvMaster.KeyFieldName.Split(';');
                string KeyFieldName = KeyFieldList[0];
                if (key == StringUtil.CStr(e.GetValue(KeyFieldName)))
                {
                    if (key == StringUtil.CStr(gvMaster.GetRowValues(gvMaster.FocusedRowIndex, KeyFieldName)))
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

    protected void gvMaster_PageIndexChanged(object sender, EventArgs e)
    {
        gvHead.CancelEdit();
        DataTable dt = ViewState["gvMaster"] as DataTable;
        gvMaster.DataSource = dt;
        gvMaster.DataBind();
    }

    protected void gvMaster_InitNewRow(object sender, DevExpress.Web.Data.ASPxDataInitNewRowEventArgs e)
    {
        gvMaster.FocusedRowIndex = -1;
        gvMaster.SettingsBehavior.AllowFocusedRow = false;
        if (gvHead.IsEditing) gvHead.CancelEdit();
    }

    protected void gvMaster_StartRowEditing(object sender, DevExpress.Web.Data.ASPxStartRowEditingEventArgs e)
    {
        gvHead.CancelEdit();
        gvMaster.FocusedRowIndex = -1;
        gvMaster.Selection.UnselectAll();
        gvMaster.SettingsBehavior.AllowFocusedRow = false;
    }

    protected void gvMaster_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
    {

        e.NewValues.Add("SID", StringUtil.CStr(ViewState["KeyValue"]));
        new ORD11_Facade().UpdatePORQMethodData(e.NewValues, logMsg.MODI_USER);
        ((ASPxGridView)sender).CancelEdit();
        bindMasterData();

        e.Cancel = true;
    }

    protected void gvMaster_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
    {
        new ORD11_Facade().InsertPREQMethodData(e.NewValues, logMsg.MODI_USER);
        bindMasterData();
        ((ASPxGridView)sender).CancelEdit();
        e.Cancel = true;
        gvMaster.PageIndex = gvMaster.PageCount - 1;
    }

    protected void gvMaster_RowValidating(object sender, DevExpress.Web.Data.ASPxDataValidationEventArgs e)
    {
        ASPxGridView gv = (ASPxGridView)sender;
        PopupControl iPRODNO = gv.FindChildControl<PopupControl>("txtPROD_NO");
        ASPxComboBox iSALE_REGION = gv.FindChildControl<ASPxComboBox>("cbSALE_REGION");
        ASPxTextBox iSAFETY_VALUE = gv.FindChildControl<ASPxTextBox>("txtSAFETY_VALUE");
        string SID = "";
        if (!e.IsNewRow)
        {
            ViewState["KeyValue"] = e.Keys[0];
            SID = StringUtil.CStr(e.Keys[0]);
        }
        //check data:安全係數(SAFETY_VALUE)
        int iTmp;
        if (StringUtil.CStr(e.NewValues["SAFETY_VALUE"]).Trim() == "")
        {
            e.RowError += "安全係數不允許空白，請重新輸入!";
        }
        if (!Int32.TryParse(StringUtil.CStr(e.NewValues["SAFETY_VALUE"]), out iTmp))
        {
            e.RowError += "輸入字串不符合數字格式，請重新輸入!";
        }
        try
        {
            Decimal i = Convert.ToDecimal(StringUtil.CStr(e.NewValues["SAFETY_VALUE"]));
            if (i <= 0) e.RowError += "安全系數不允許小於等於0，請重新輸入";
        }
        catch //(Exception ex)
        {
            e.RowError += "安全係數不允許空白";
        }

        DataTable dt = new Product_Facade().Query_ProductInfo(iPRODNO.Text);
        if (dt.Rows.Count == 0)
        {
            e.RowError += "查無貨品資料,請重新輸入!";
        }
        else
        {
            if (ORD11_Facade.CheckProductQTY(SID, iPRODNO.Text) != "")
            {
                e.RowError += "商品料號資料已存在,請重新輸入!";
            }
            else if (CheckSAFETY_VALUE(iSAFETY_VALUE.Text) != "") 
            { 
                e.RowError = CheckSAFETY_VALUE(iSAFETY_VALUE.Text); 
            }
            else if (iSALE_REGION.SelectedIndex == 2)
            {
                string checkdate = System.DateTime.Now.ToString("yyyy/MM/dd");
                if (e.NewValues["S_DATE"] == null || string.IsNullOrEmpty(StringUtil.CStr(e.NewValues["S_DATE"]).Trim()))
                {
                    e.RowError += "開始日期不允許空白";
                }
                else if (e.NewValues["E_DATE"] == null || string.IsNullOrEmpty(StringUtil.CStr(e.NewValues["E_DATE"]).Trim()))
                {
                    e.RowError += "結束日期不允許空白";
                }
                else if (System.DateTime.Parse(StringUtil.CStr(e.NewValues["S_DATE"])) >= System.DateTime.Parse(StringUtil.CStr(e.NewValues["E_DATE"])))
                {
                    e.RowError += "開始日期須小於結束日期!!";
                }
            }
        }
        if (e.RowError != "")
        {
            IsgvMasterError = true;
            Session["IsAddClick"] = "0";
        }
        else
            IsgvMasterError = false;
    }

    protected void gvMaster_CancelRowEditing(object sender, DevExpress.Web.Data.ASPxStartRowEditingEventArgs e)
    {
        gvMaster.Selection.UnselectAll();
        gvMaster.SettingsBehavior.AllowFocusedRow = true;
        gvMaster.FocusedRowIndex = -1;
    }

    #endregion

    //ajax 呼當前網頁的方式
    [System.Web.Services.WebMethod()]
    [System.Web.Script.Services.ScriptMethod()]
    static public string getProductInfo(string PRODUCT_NO)
    {
        string strInfo = "";
        if (!string.IsNullOrEmpty(PRODUCT_NO))
        {
            DataTable _dt = new Product_Facade().Query_ProductInfo(PRODUCT_NO);
            if (_dt.Rows.Count > 0)
            {
                strInfo = StringUtil.CStr(_dt.Rows[0]["PRODNAME"]);
            }
        }

        return strInfo;
    }

    protected void cbSALE_REGION2_SelectedIndexChanged(object sender, EventArgs e)
    {
        ASPxComboBox iSALE_REGION = gvHead.FindChildControl<ASPxComboBox>("cbSALE_REGION2");
        ASPxDateEdit colS_DATE = gvHead.FindChildControl<ASPxDateEdit>("S_DATE");
        ASPxDateEdit colE_DATE = gvHead.FindChildControl<ASPxDateEdit>("E_DATE");
        if (StringUtil.CStr(iSALE_REGION.SelectedItem.Value) == "2")
        {
            colS_DATE.ClientEnabled = true;
            colE_DATE.ClientEnabled = true;
            colS_DATE.ControlStyle.BackColor = System.Drawing.Color.White;
            colE_DATE.ControlStyle.BackColor = System.Drawing.Color.White;
        }
        else
        {
            colS_DATE.ClientEnabled = false;
            colE_DATE.ClientEnabled = false;
            colS_DATE.Text = "";
            colE_DATE.Text = "";
            colS_DATE.ControlStyle.BackColor = System.Drawing.Color.LightGray;
            colE_DATE.ControlStyle.BackColor = System.Drawing.Color.LightGray;
        }
    }
    protected void cbSALE_REGION_SelectedIndexChanged(object sender, EventArgs e)
    {
        string prodNo = gvMaster.FindChildControl<PopupControl>("txtPROD_NO").Text;
        gvMaster.FindChildControl<ASPxLabel>("lblPRODNAME").Text = getProductInfo(prodNo);
        ASPxComboBox iSALE_REGION = gvMaster.FindChildControl<ASPxComboBox>("cbSALE_REGION");
        ASPxDateEdit colS_DATE = gvMaster.FindChildControl<ASPxDateEdit>("S_DATE");
        ASPxDateEdit colE_DATE = gvMaster.FindChildControl<ASPxDateEdit>("E_DATE");
        if (StringUtil.CStr(iSALE_REGION.SelectedItem.Value) == "2")
        {
            colS_DATE.ClientEnabled = true;
            colE_DATE.ClientEnabled = true;
            colS_DATE.ControlStyle.BackColor = System.Drawing.Color.White;
            colE_DATE.ControlStyle.BackColor = System.Drawing.Color.White;
        }
        else
        {
            colS_DATE.ClientEnabled = false;
            colE_DATE.ClientEnabled = false;
            colS_DATE.Text = "";
            colE_DATE.Text = "";
            colS_DATE.ControlStyle.BackColor = System.Drawing.Color.LightGray;
            colE_DATE.ControlStyle.BackColor = System.Drawing.Color.LightGray;
        }
    }
}

