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
using DevExpress.Web.Data;
using FET.POS.Model.Facade.FacadeImpl;
using FET.POS.Model.DTO;
using DevExpress.Web.ASPxEditors;


public partial class VSS_INV_INV27 : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (logMsg.STORENO != StringUtil.CStr(System.Web.Configuration.WebConfigurationManager.AppSettings["DefaultStore"]))
            {
                ScriptManager.RegisterClientScriptBlock(this, GetType(), "AlertMessage", "alert('非總部人員無法使用此功能!!');", true);
                grid.Enabled = false;
                btnSearch.Enabled = false;
                btnClear.Enabled = false;
                txtS_Date.Enabled = false;
                txtS_Date.ControlStyle.BackColor = System.Drawing.Color.LightGray;
                txtE_Date.Enabled = false;
                txtE_Date.ControlStyle.BackColor = System.Drawing.Color.LightGray;
                txtS_PRODNO.Enabled = false;
                txtE_PRODNO.Enabled = false;
                return;
            }
            else
            {
                grid.DataSource = new INV27_Facade().GetTakeOff_MData("", "", "NODATA", "NODATA");
                grid.DataBind();
            }
        }
    }

    void getZone()
    {
        ASPxComboBox cb = (ASPxComboBox)detailGrid.FindTitleTemplateControl("districtComboBox");
        if (cb != null)
        {
            DataTable dt = INV27_Facade.getZone();

            cb.Items.Clear();
            foreach (DataRow dr in dt.Rows)
            {
                ListEditItem lei = new ListEditItem(StringUtil.CStr(dr["ZONE_NAME"]), StringUtil.CStr(dr["ZONE"]));
                cb.Items.Add(lei);
            }
            cb.DropDownButton.Enabled = true;
            if (cb.SelectedIndex == -1)
                cb.SelectedIndex = 0;
            cb.DataBind();
        }
    }

    private DataTable getGridViewData()
    {
        DataTable dt = new DataTable();

        if (grid.FocusedRowIndex > -1)
        {
            string key = "";
            key = StringUtil.CStr(grid.GetRowValues(grid.FocusedRowIndex, "SEAL_OFF_PROD_ID"));

            DateTime SEAL_OFF_DATE = Convert.ToDateTime(grid.GetRowValues(grid.FocusedRowIndex, "SEAL_OFF_DATE"));
            DateTime E_Date = Convert.ToDateTime(grid.GetRowValues(grid.FocusedRowIndex, "E_DATE"));
            //4.2.2.4.2	已生效(拆封日期≦系統日≦展示訖日)之資料列，僅展示訖日可以修改，明細不允許編輯
            if ((SEAL_OFF_DATE <= DateTime.Now && DateTime.Now <= E_Date) || E_Date < DateTime.Now)
            {
                detailGrid.Enabled = false;
            }
            else
            {
                detailGrid.Enabled = true;
            }

            detailGrid.PagerBarEnabled = true;

            dt = new INV27_Facade().GetTakeOff_DData(key);
        }

        return dt;
    }

    private void bindMasterData()
    {
        grid.DataSource = new INV27_Facade().GetTakeOff_MData(txtS_Date.Text, txtE_Date.Text, txtS_PRODNO.Text, txtE_PRODNO.Text);
        grid.DataBind();
    }

    #region Button 觸發事件

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        bindMasterData();

        if (grid.DataSource != null && (grid.DataSource as DataTable).Rows.Count > 0)
            grid.FocusedRowIndex = -1;

        grid.Selection.UnselectAll();
    }

    protected void AddButton_Click(object sender, EventArgs e)
    {
        grid.AddNewRow();
        grid.Selection.UnselectAll();
        tabPage.Visible = true;
        detailGrid.DataSource = new INV27_Facade().GetTakeOff_DData("");
        detailGrid.DataBind();
        detailGrid.CancelEdit();
    }

    protected void deleteButton_Click(object sender, EventArgs e)
    {
        new INV27_Facade().DeleteTakeOff_MData(this.grid.GetSelectedFieldValues(grid.KeyFieldName));
        bindMasterData();

        grid_FocusedRowChanged(grid, new EventArgs());
        detailGrid.Enabled = false;
    }

    protected void btnDetailDelete_Click(object sender, EventArgs e)
    {
        new INV27_Facade().DeleteTakeOff_DData(this.detailGrid.GetSelectedFieldValues(detailGrid.KeyFieldName));
        new INV27_Facade().DeleteTakeOff_DIMEIData(this.detailGrid.GetSelectedFieldValues(detailGrid.KeyFieldName));
        grid_FocusedRowChanged(grid, new EventArgs());
    }

    protected void Button6_Click(object sender, EventArgs e)
    {
        if (grid.FocusedRowIndex != -1)
        {
            ASPxComboBox cb = (ASPxComboBox)detailGrid.FindTitleTemplateControl("districtComboBox");
            if (cb.SelectedIndex < 1)
                ScriptManager.RegisterClientScriptBlock(this, typeof(string), "AREA_STORE_IMPORT_SELECT", "alert('請選取區域!');", true);
            else
            {
                string v = cb.ClientValue;
                new INV27_Facade().InsertTakeOff_DData(StringUtil.CStr(grid.GetRowValues(grid.FocusedRowIndex, "SEAL_OFF_PROD_ID")), v, logMsg.MODI_USER);
                grid_FocusedRowChanged(grid, new EventArgs());
                ScriptManager.RegisterClientScriptBlock(this, typeof(string), "AREA_STORE_IMPORT", "alert('分區資料輸入完成!');", true);
                detailGrid.PageIndex = 0;
                detailGrid.FocusedRowIndex = -1;
            }
        }
    }

    #endregion

    #region gvMaster 觸發事件

    protected void grid_PageIndexChanged(object sender, EventArgs e)
    {
        bindMasterData();
        detailGrid.Enabled = false;
        grid.FocusedRowIndex = -1;
    }

    protected void grid_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
    {
        new INV27_Facade().InsertTakeOff_MData(e.NewValues, logMsg.MODI_USER);
        bindMasterData();
        ((ASPxGridView)sender).CancelEdit();
        e.Cancel = true;
    }

    protected void grid_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
    {
        e.NewValues.Add("SEAL_OFF_PROD_ID", StringUtil.CStr(e.Keys[0]));
        new INV27_Facade().UpdateTakeOff_MData(e.NewValues, logMsg.MODI_USER);
        ((ASPxGridView)sender).CancelEdit();
        bindMasterData();
        e.Cancel = true;
    }

    protected void grid_HtmlRowPrepared(object sender, ASPxGridViewTableRowEventArgs e)
    {
        e.Row.Attributes["canSelect"] = "true";
        if (e.RowType == GridViewRowType.Data)
        {
            string date = StringUtil.CStr(grid.GetRowValues(e.VisibleIndex, "SEAL_OFF_DATE"));
            if (Convert.ToDateTime(date) < DateTime.Now)
            {
                e.Row.Attributes["canSelect"] = "false";
            }
        }
        if (grid.IsEditing)
        {
            DateTime SEAL_OFF_DATE = Convert.ToDateTime(grid.GetRowValuesByKeyValue(e.KeyValue, "SEAL_OFF_DATE"));
            DateTime E_Date = Convert.ToDateTime(grid.GetRowValuesByKeyValue(e.KeyValue, "E_DATE"));
            PopupControl pc = grid.FindChildControl<PopupControl>("pcPRODNO");

            if (SEAL_OFF_DATE <= DateTime.Now && DateTime.Now <= E_Date)
            {
                pc.Enabled = false;
            }
            else
            {
                pc.Enabled = true;
            }
        }
    }

    protected void grid_FocusedRowChanged(object sender, EventArgs e)
    {
        ASPxGridView gv = sender as ASPxGridView;
        tabPage.Visible = true;
        detailGrid.DataSource = getGridViewData();
        detailGrid.DataBind();
        detailGrid.CancelEdit();
        getZone();
    }

    protected void grid_CommandButtonInitialize(object sender, ASPxGridViewCommandButtonEventArgs e)
    {

        if (e.VisibleIndex > -1)
        {
            if (e.ButtonType == ColumnCommandButtonType.Edit || e.ButtonType == ColumnCommandButtonType.SelectCheckbox)
            {
                DateTime SEAL_OFF_DATE = Convert.ToDateTime(StringUtil.CStr(grid.GetRowValues(e.VisibleIndex, "SEAL_OFF_DATE")));
                DateTime E_DATE = Convert.ToDateTime(StringUtil.CStr(grid.GetRowValues(e.VisibleIndex, "E_DATE")));

                if (E_DATE < DateTime.Now.AddDays(1))
                {
                    e.Enabled = false;
                }
                else if (SEAL_OFF_DATE <= DateTime.Now && DateTime.Now <= E_DATE)
                {
                    if (e.ButtonType == ColumnCommandButtonType.SelectCheckbox) { e.Enabled = false; }
                }
            }
        }
    }

    protected void grid_StartRowEditing(object sender, ASPxStartRowEditingEventArgs e)
    {
        grid.Selection.UnselectAll();
        if (grid.IsEditing)
        {
            DateTime SEAL_OFF_DATE = Convert.ToDateTime(grid.GetRowValuesByKeyValue(e.EditingKeyValue, "SEAL_OFF_DATE"));
            DateTime E_Date = Convert.ToDateTime(grid.GetRowValuesByKeyValue(e.EditingKeyValue, "E_DATE"));
            GridViewDataDateColumn colE_DATE = (GridViewDataDateColumn)grid.Columns["E_DATE"];
            GridViewDataDateColumn colS_DATE = (GridViewDataDateColumn)grid.Columns["S_DATE"];
            GridViewDataDateColumn colSEAL_OFF_DATE = (GridViewDataDateColumn)grid.Columns["SEAL_OFF_DATE"];
            GridViewDataColumn colSEAL_OFF_QTY = (GridViewDataColumn)grid.Columns["SEAL_OFF_QTY"];
            GridViewDataComboBoxColumn colDISCOUNT_TYPE = (GridViewDataComboBoxColumn)grid.Columns["DISCOUNT_TYPE"];
            GridViewDataTextColumn colUNIT_PRICE = (GridViewDataTextColumn)grid.Columns["DISCOUNT_PRICE"];
            if (SEAL_OFF_DATE <= DateTime.Now && DateTime.Now <= E_Date)
            {
                colE_DATE.ReadOnly = false;
                colSEAL_OFF_DATE.ReadOnly = true;
                colS_DATE.ReadOnly = true;
                colSEAL_OFF_QTY.ReadOnly = true;
                colDISCOUNT_TYPE.ReadOnly = true;
                colUNIT_PRICE.ReadOnly = true;
            }
            else
            {
                colE_DATE.ReadOnly = false;
                colSEAL_OFF_DATE.ReadOnly = false;
                colSEAL_OFF_QTY.ReadOnly = false;
                colDISCOUNT_TYPE.ReadOnly = false;
                colUNIT_PRICE.ReadOnly = false;
            }
        }
    }

    protected void grid_RowValidating(object sender, ASPxDataValidationEventArgs e)
    {
        if (e.Keys.Count == 0) e.Keys.Add("SEAL_OFF_PROD_ID", "");
        if (grid.IsNewRowEditing)
        {
            DataTable dtProduct = new Product_Facade().Query_ProductInfo(StringUtil.CStr(e.NewValues["PRODNO"]));
            if (e.NewValues["SEAL_OFF_DATE"] == null)
                e.RowError = "拆封日期不允許為空白，請重新輸入!";
            else if (Convert.ToDateTime(e.NewValues["SEAL_OFF_DATE"]) <= DateTime.Now.Date)
                e.RowError = "拆封日期不允許小於等於系統日，請重新輸入!";
            else if (e.NewValues["S_DATE"] == null)
                e.RowError = "展示起日不允許為空白，請重新輸入!";
            else if (e.NewValues["E_DATE"] == null)
                e.RowError = "展示訖日不允許為空白，請重新輸入!";
            else if (string.IsNullOrEmpty(StringUtil.CStr(e.NewValues["PRODNO"])))
                e.RowError = "商品料號必須輸入";
            else if (dtProduct == null || dtProduct.Rows.Count == 0)
                e.RowError = "商品料號不存在，請重新輸入!";
            else if (INV27_Facade.Check_Start_End_PROD(StringUtil.CStr(e.Keys[0]), StringUtil.CStr(e.NewValues["PRODNO"]),
                                                      Convert.ToDateTime(e.NewValues["S_DATE"]).ToString("yyyy/MM/dd"),
                                                      Convert.ToDateTime(e.NewValues["E_DATE"]).ToString("yyyy/MM/dd")).Rows.Count > 0)
                e.RowError = "商品料號已存在，請重新輸入";
            else if (e.NewValues["SEAL_OFF_QTY"] == null)
                e.RowError = "拆封數量必須輸入";
            else if (Convert.ToInt32(e.NewValues["SEAL_OFF_QTY"]) <= 0)
                e.RowError = "拆封數量不允許小於等於0，請重新輸入!";
            else if (e.NewValues["DISCOUNT_TYPE"] == null)
                e.RowError = "折扣方式必須輸入";
            else if (e.NewValues["DISCOUNT_PRICE"] == null)
                e.RowError = "金額/佔比必須輸入!";
            else if (Convert.ToDateTime(e.NewValues["S_DATE"]) > Convert.ToDateTime(e.NewValues["E_DATE"]))
                e.RowError = "展示訖日不允許小於展示起日，請重新輸入!";
            else if (Convert.ToDateTime(e.NewValues["E_DATE"]) < DateTime.Now.Date)
                e.RowError = "展示訖日不允許小於系統日，請重新輸入!";
            else if (Convert.ToDateTime(e.NewValues["S_DATE"]) <= Convert.ToDateTime(e.NewValues["SEAL_OFF_DATE"]))
                e.RowError = "展示起日不允許小於等於拆封日，請重新輸入!";
            else if (Convert.ToInt32(e.NewValues["DISCOUNT_PRICE"]) < 0)
                e.RowError = "金額/佔比不允許小於0，請重新輸入!";
            else if (StringUtil.CStr(e.NewValues["DISCOUNT_TYPE"]) == "2" && Convert.ToInt32(e.NewValues["UNIT_PRICE"]) > 99)
                e.RowError = "金額/佔比不允許大於100%，請重新輸入!";
            else if (Convert.ToInt32(e.NewValues["SEAL_OFF_QTY"]) < 0)
                e.RowError = "拆封數量不允許小於0，請重新輸入!";
        }
        else
        {
            DataTable dtProduct = new Product_Facade().Query_ProductInfo(StringUtil.CStr(e.NewValues["PRODNO"]));

            if (e.NewValues["E_DATE"] == null)
                e.RowError = "展示訖日不允許為空白，請重新輸入!";
            else if (string.IsNullOrEmpty(StringUtil.CStr(e.NewValues["PRODNO"])))
                e.RowError = "商品料號必須輸入";
            else if (dtProduct == null || dtProduct.Rows.Count == 0)
                e.RowError = "商品料號不存在，請重新輸入!";
            else if (INV27_Facade.Check_Start_End_PROD(StringUtil.CStr(e.Keys[0]), StringUtil.CStr(e.NewValues["PRODNO"]),
                                                      Convert.ToDateTime(e.NewValues["S_DATE"]).ToString("yyyy/MM/dd"),
                                                      Convert.ToDateTime(e.NewValues["E_DATE"]).ToString("yyyy/MM/dd")).Rows.Count > 0)
                e.RowError = "商品料號已存在，請重新輸入";
            else if (e.NewValues["SEAL_OFF_QTY"] == null)
                e.RowError = "拆封數量必須輸入";
            else if (Convert.ToInt32(e.NewValues["SEAL_OFF_QTY"]) <= 0)
                e.RowError = "拆封數量不允許小於等於0，請重新輸入!";
            else if (e.NewValues["DISCOUNT_TYPE"] == null)
                e.RowError = "折扣方式必須輸入";
            else if (e.NewValues["DISCOUNT_PRICE"] == null)
                e.RowError = "金額/佔比必須輸入!";
            else if (Convert.ToDateTime(e.NewValues["S_DATE"]) > Convert.ToDateTime(e.NewValues["E_DATE"]))
                e.RowError = "展示訖日不允許小於展示起日，請重新輸入!";
            else if (Convert.ToDateTime(e.NewValues["E_DATE"]) < DateTime.Now.Date)
                e.RowError = "展示訖日不允許小於系統日，請重新輸入!";
            else if (Convert.ToInt32(e.NewValues["DISCOUNT_PRICE"]) < 0)
                e.RowError = "金額/佔比不允許小於0，請重新輸入!";
            else if (StringUtil.CStr(e.NewValues["DISCOUNT_TYPE"]) == "2" && Convert.ToInt32(e.NewValues["UNIT_PRICE"]) > 99)
                e.RowError = "金額/佔比不允許大於100%，請重新輸入!";
            else if (Convert.ToInt32(e.NewValues["SEAL_OFF_QTY"]) < 0)
                e.RowError = "拆封數量不允許小於0，請重新輸入!";

        }
    }

    protected void grid_InitNewRow(object sender, ASPxDataInitNewRowEventArgs e)
    {
        ((GridViewDataDateColumn)grid.Columns["E_DATE"]).ReadOnly = false;
        ((GridViewDataDateColumn)grid.Columns["S_DATE"]).ReadOnly = false;
        ((GridViewDataDateColumn)grid.Columns["SEAL_OFF_DATE"]).ReadOnly = false;
        ((GridViewDataColumn)grid.Columns["SEAL_OFF_QTY"]).ReadOnly = false;
        ((GridViewDataComboBoxColumn)grid.Columns["DISCOUNT_TYPE"]).ReadOnly = false;
        ((GridViewDataTextColumn)grid.Columns["DISCOUNT_PRICE"]).ReadOnly = false;
    }

    #endregion

    #region gvDetail 觸發事件

    protected void detailGrid_PageIndexChanged(object sender, EventArgs e)
    {
        grid_FocusedRowChanged(grid, new EventArgs());
    }

    protected void detailGrid_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
    {
        ASPxGridView dgv = ((ASPxGridView)sender);
        e.NewValues["SEAL_OFF_PROD_ID"] = StringUtil.CStr(grid.GetRowValues(grid.FocusedRowIndex, "SEAL_OFF_PROD_ID"));
        new INV27_Facade().InsertTakeOff_DData(e.NewValues, logMsg.MODI_USER);
        grid_FocusedRowChanged(grid, new EventArgs());
        dgv.CancelEdit();
        e.Cancel = true;
    }

    protected void detailGrid_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
    {
        e.NewValues["SEAL_OFF_STORE_ID"] = e.Keys["SEAL_OFF_STORE_ID"];
        new INV27_Facade().UpdateTakeOff_DData(e.NewValues, logMsg.MODI_USER);
        e.Cancel = true;
        ((ASPxGridView)sender).CancelEdit();
        //重LOAD資料
        grid_FocusedRowChanged(grid, new EventArgs());
    }

    protected void detailGrid_DataSelect(object sender, EventArgs e)
    {
        Session["移撥單號"] = (sender as ASPxGridView).GetMasterRowKeyValue();
    }

    protected void detailGrid_PageIndexChanged1(object sender, EventArgs e)
    {
        grid_FocusedRowChanged(grid, new EventArgs());
    }

    protected void detailGrid_InitNewRow(object sender, ASPxDataInitNewRowEventArgs e)
    {
        detailGrid.Selection.UnselectAll();
    }

    protected void detailGrid_PreRender(object sender, EventArgs e)
    {
        getZone();
    }

    protected void detailGrid_StartRowEditing(object sender, ASPxStartRowEditingEventArgs e)
    {
        detailGrid.Selection.UnselectAll();
    }

    protected void detailGrid_RowValidating(object sender, ASPxDataValidationEventArgs e)
    {
        if (e.Keys.Count == 0) e.Keys.Add("SEAL_OFF_STORE_ID", "");
        if (e.NewValues["STORE_NO"] == null)
            e.RowError = "請輸入門市編號!";
        else
        {
            DataTable DT = new Store_Facade().Query_StoreInfo(StringUtil.CStr(e.NewValues["STORE_NO"]));
            if (DT.Rows.Count == 0)
                e.RowError = "門市代號不存在!";
            else
            {
                if (e.Keys.Count > 0)
                {
                    DT = INV27_Facade.getTakeOffStore(StringUtil.CStr(grid.GetRowValues(grid.FocusedRowIndex, "SEAL_OFF_PROD_ID")),
                                                         StringUtil.CStr(e.Keys[0]), StringUtil.CStr(e.NewValues["STORE_NO"]));

                }
                else
                {
                    DT = INV27_Facade.getTakeOffStore(StringUtil.CStr(grid.GetRowValues(grid.FocusedRowIndex, "SEAL_OFF_PROD_ID")),
                                                         "", StringUtil.CStr(e.NewValues["STORE_NO"]));
                }
                if (DT.Rows.Count > 0)
                    e.RowError = "此門市己輸入，請重新輸入!";
            }
        }

    }

    #endregion

    //ajax 呼當前網頁的方式
    [System.Web.Services.WebMethod()]
    [System.Web.Script.Services.ScriptMethod()]
    static public string getPRODNAME(string PRODNO)
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

    [System.Web.Services.WebMethod()]
    [System.Web.Script.Services.ScriptMethod()]
    static public string getTakeOffStore(string SEAL_OFF_PROD_ID, string STORE_NO)
    {
        DataTable dt = INV27_Facade.getTakeOffStore(SEAL_OFF_PROD_ID, STORE_NO);
        string r = "";
        if (dt.Rows.Count > 0)
            r = StringUtil.CStr(dt.Rows[0]["STORE_NO"]);
        return r;
    }

    [System.Web.Services.WebMethod()]
    [System.Web.Script.Services.ScriptMethod()]
    static public string getStore(string STORE_NO)
    {
        DataTable dt = new Store_Facade().Query_StoreInfo(STORE_NO);
        string r = "";
        if (dt.Rows.Count > 0)
            r = StringUtil.CStr(dt.Rows[0]["STORENAME"]);
        return r;
    }

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
    protected void grid_CellEditorInitialize(object sender, ASPxGridViewEditorEventArgs e)
    {
        
    }
    protected void grid_HtmlRowCreated(object sender, ASPxGridViewTableRowEventArgs e)
    {
        GridViewDataDateColumn colSEAL_OFF_DATE = (GridViewDataDateColumn)grid.Columns["SEAL_OFF_DATE"];
        GridViewDataDateColumn colS_DATE = (GridViewDataDateColumn)grid.Columns["S_DATE"];
        GridViewDataDateColumn colE_DATE = (GridViewDataDateColumn)grid.Columns["E_DATE"];
        colSEAL_OFF_DATE.PropertiesDateEdit.MinDate = DateTime.Now.Date.AddDays(1);
        colS_DATE.PropertiesDateEdit.MinDate = DateTime.Now.Date.AddDays(2);
        colE_DATE.PropertiesDateEdit.MinDate = DateTime.Now.Date.AddDays(2);
    }
}
