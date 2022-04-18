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

using Advtek.Utility;
using FET.POS.Model.Common;
using FET.POS.Model.DTO;
using FET.POS.Model.Facade;
using FET.POS.Model.Facade.FacadeImpl;

public partial class VSS_OPT_OPT10 : BasePage
{
    #region Class Variables

    public static string e_status = "1";

    string dataStatusValue
    {
        set
        {
            ViewState["dataStatusValue"] = value;
        }
        get
        {
            if (ViewState["dataStatusValue"] == null)
                return string.Empty;

            return (string)ViewState["dataStatusValue"];
        }
    }

    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            bindDdlValTxt(ASPxComboBox1, PRODUCT_PageHelper.GetProDTypeNo(true), "PRODTYPE_NO", "PRODTYPE_NAME");
            bindDdlValTxt(ASPxComboBox3, OPT10_PageHelper.GetChkImeiTypeNo(true, "Select"), "CHECK_IMEI_TYPE", "CHECK_IMEI_TYPE_NAME");

            aspComboBoxDefaultSetting();

            //為取得空資料表的欄位格式，先丟不會找出資料的索引值給Method.
            gvMaster.DataSource = new OPT10_Facade().Query_ProductMethodSet("1", "2", "3", "4", "5");
            gvMaster.DataBind();
        }
    }

    protected void bindMasterData()
    {
        gvMaster.DataSource = getMasterData();
        gvMaster.DataBind();
    }

    protected void bindMasterDataAfterWrite()
    {
        DataTable dtGvMaster = new DataTable();
        dtGvMaster = getMasterData();
        gvMaster.DataSource = dtGvMaster;
        gvMaster.DataBind();
    }

    protected void bindDdlValTxt(ASPxComboBox AspCB, object dataSrc, string valCol, string txtCol)
    {
        AspCB.DataSource = dataSrc;
        AspCB.ValueField = valCol;
        AspCB.TextField = txtCol;
        AspCB.DataBind();
    }

    protected void aspComboBoxDefaultSetting()
    {
        //若都沒選,將Index設為0
        if (ASPxComboBox1.SelectedIndex == -1)
        {
            ASPxComboBox1.SelectedIndex = 0;
        }

        if (ASPxComboBox3.SelectedIndex == -1)
        {
            ASPxComboBox3.SelectedIndex = 0;
        }

    }

    private DataTable getMasterData()
    {
        DataTable dtMaster = new DataTable();

        OPT10_Facade OPT10_Facade = new OPT10_Facade();

        dtMaster = OPT10_Facade.Query_ProductMethodSet(StringUtil.CStr(ASPxComboBox1.SelectedItem.Value),
                                           TextBox1.Text,
                                           TextBox2.Text,
                                           StringUtil.CStr(ASPxComboBox3.SelectedItem.Value),
                                           StringUtil.CStr(ASPxComboBox2.SelectedItem.Value));

        return dtMaster;
    }

    #region Button 觸發事件

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        gvMaster.AddNewRow();
        gvMaster.Selection.UnselectAll();
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        bindMasterData();
        gvMaster.Selection.UnselectAll();
        gvMaster.PageIndex = 0;
        if (gvMaster.IsEditing) gvMaster.CancelEdit();
    }

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        //get values
        List<object> gvPKValues = gvMaster.GetSelectedFieldValues(gvMaster.KeyFieldName);
        string pkFName = gvMaster.KeyFieldName;

        OPT10_Product_DTO OPT10_Product_DTO = new OPT10_Product_DTO();

        OPT10_Product_DTO.PRODUCTDataTable dtSYS;
        OPT10_Product_DTO.PRODUCTRow drSYS;

        dtSYS = OPT10_Product_DTO.Tables["PRODUCT"] as OPT10_Product_DTO.PRODUCTDataTable;


        //不變動
        dtSYS.Columns["STATUS"].AllowDBNull = true;
        dtSYS.Columns["PRODNO"].AllowDBNull = true;
        dtSYS.Columns["PRODNAME"].AllowDBNull = true;
        dtSYS.Columns["PRODTYPENO"].AllowDBNull = true;
        dtSYS.Columns["UNIT"].AllowDBNull = true;
        dtSYS.Columns["PRICE"].AllowDBNull = true;
        dtSYS.Columns["S_DATE"].AllowDBNull = true;
        dtSYS.Columns["E_DATE"].AllowDBNull = true;
        dtSYS.Columns["ISSTOCK"].AllowDBNull = true;
        dtSYS.Columns["ACCOUNTCODE"].AllowDBNull = true;
        dtSYS.Columns["IS_OPEN_PRICE"].AllowDBNull = true;
        dtSYS.Columns["IMEI_FLAG"].AllowDBNull = true;
        dtSYS.Columns["CREATE_USER"].AllowDBNull = true;
        dtSYS.Columns["CREATE_DTM"].AllowDBNull = true;
        dtSYS.Columns["COMPANYCODE"].AllowDBNull = true;
        dtSYS.Columns["DEL_FLAG"].AllowDBNull = true;
        dtSYS.Columns["TAXABLE"].AllowDBNull = true;
        dtSYS.Columns["TAXRATE"].AllowDBNull = true;
        dtSYS.Columns["DS_FLAG"].AllowDBNull = true;


        for (int i = 0; i < gvPKValues.Count; i++)
        {
            drSYS = dtSYS.NewPRODUCTRow();

            //PK值
            drSYS["PRODNO"] = StringUtil.CStr(gvPKValues[i]);

            drSYS["MODI_USER"] = logMsg.MODI_USER;
            drSYS["MODI_DTM"] = Convert.ToDateTime(System.DateTime.Today);

            drSYS["DEL_FLAG"] = "Y";


            dtSYS.AddPRODUCTRow(drSYS);
            OPT10_Product_DTO.AcceptChanges();

            //更新資料庫
            new OPT10_Facade().UpdateOne_ProductMethodSet(OPT10_Product_DTO);

        }

        gvMaster.DataSource = getMasterData();
        gvMaster.DataBind();

    }
    
    #endregion

    #region gvMaster 觸發事件

    protected void gvMaster_CellEditorInitialize(object sender, ASPxGridViewEditorEventArgs e)
    {
        if (!gvMaster.IsNewRowEditing)
        {
            string status = StringUtil.CStr(gvMaster.GetRowValuesByKeyValue(e.KeyValue, "STATUS"));
            if (status == "有效") return;
        }

        if (e.Column.FieldName == "PRODTYPENO")
        {
            ASPxComboBox combo = e.Editor as ASPxComboBox;

            //商品類別
            combo.DataSource = PRODUCT_PageHelper.GetProDTypeNo(false);
            combo.ValueField = "PRODTYPE_NO";
            combo.TextField = "PRODTYPE_NAME";
            combo.DataBind();
        }

        if (e.Column.FieldName == "IMEI_FLAG")
        {
            ASPxComboBox combo1 = e.Editor as ASPxComboBox;

            //檢核IMEI
            combo1.DataSource = OPT10_PageHelper.GetChkImeiTypeNo(true, "Edit");
            combo1.ValueField = "CHECK_IMEI_TYPE";
            combo1.TextField = "CHECK_IMEI_TYPE_NAME";
            combo1.DataBind();
        }
    }

    protected void gvMaster_PageIndexChanged(object sender, EventArgs e)
    {
        bindMasterData();
        gvMaster.Selection.UnselectAll();
        if (gvMaster.IsEditing) gvMaster.CancelEdit();
    }

    protected void gvMaster_CommandButtonInitialize(object sender, ASPxGridViewCommandButtonEventArgs e)
    {
        if (e.VisibleIndex > -1)
        {
            string STATUS = StringUtil.CStr(gvMaster.GetRowValues(e.VisibleIndex, "STATUS"));
            string PRODNO = StringUtil.CStr(gvMaster.GetRowValues(e.VisibleIndex, "PRODNO"));

            if (e.ButtonType == ColumnCommandButtonType.Edit)  //「編輯」的Button
            {
                if ((STATUS == "有效" || STATUS == "尚未生效"))
                {
                    if (PRODNO.Length <= 8)
                    {
                        e.Enabled = true;
                    }
                    else
                    {
                        //**2011/02/24 Tina：PRODNO.Length == 9，於尚未生效時可編輯"檢核IMEI"、"自訂價格"
                        //**2011/03/10 Vivian: PRODNO.Length == 10，always不可編輯
                        if (STATUS == "尚未生效" && PRODNO.Length == 9)
                        {
                            e.Enabled = true;
                        }
                        else  //PRODNO.Length == 9，於有效時仍不可編輯。PRODNO.Length == 10，always不可編輯。
                        {
                            e.Enabled = false;
                        }
                    }
                }
                else
                {
                    e.Enabled = false;
                }
            }

            if (e.ButtonType == ColumnCommandButtonType.SelectCheckbox)  //「刪除」的CheckBox
            {
                if ((STATUS == "尚未生效") && PRODNO.Length <= 8)
                {
                    if (gvMaster.IsEditing)
                    {
                        e.Enabled = false;
                    }
                    else
                    {
                        e.Enabled = true;
                    }
                }
                else
                {
                    e.Enabled = false;
                }
            }
        }

    }

    protected void gvMaster_HtmlRowCreated(object sender, ASPxGridViewTableRowEventArgs e)
    {
        if (e.RowType == GridViewRowType.InlineEdit)
        {
            string checkdate = System.DateTime.Now.ToString("yyyy/MM/dd");
            ASPxGridView gv = (ASPxGridView)sender;
            ASPxDateEdit dStart = gvMaster.FindEditRowCellTemplateControl((GridViewDataColumn)gvMaster.Columns["S_DATE"], "txtSDATE") as ASPxDateEdit;
            ASPxDateEdit dEtart = gvMaster.FindEditRowCellTemplateControl((GridViewDataColumn)gvMaster.Columns["E_DATE"], "txtEDATE") as ASPxDateEdit;

            ASPxLabel lblStatus = gv.FindEditRowCellTemplateControl((GridViewDataColumn)gv.Columns["STATUS"], "lblSTATUS") as ASPxLabel;

            string PRODNO = "";
            if (e.VisibleIndex >= 0)
            {
                PRODNO = StringUtil.CStr(gvMaster.GetRowValues(e.VisibleIndex, "PRODNO"));
            }

            switch (lblStatus.Text)
            {
                case "過期":
                    dStart.ReadOnly = false;
                    dStart.ClientEnabled = false;
                    break;
                case "有效":
                    dStart.ReadOnly = true;
                    dStart.ClientEnabled = false;
                    break;
                case "尚未生效":
                    dStart.ReadOnly = false;
                    if (PRODNO.Length > 8)
                    {
                        dStart.ReadOnly = true;
                        dStart.ClientEnabled = false;
                        dEtart.ReadOnly = true;
                        dEtart.ClientEnabled = false;
                    }
                    break;
                default:
                    break;
            }
        }

    }

    protected void gvMaster_HtmlRowPrepared(object sender, ASPxGridViewTableRowEventArgs e)
    {
        e.Row.Attributes["canSelect"] = "true";

        if (e.RowType == GridViewRowType.Data)
        {
            string STATUS = StringUtil.CStr(gvMaster.GetRowValues(e.VisibleIndex, "STATUS"));
            string PRODNO = StringUtil.CStr(gvMaster.GetRowValues(e.VisibleIndex, "PRODNO"));

            if (STATUS == "過期" || STATUS == "有效" || PRODNO.Length > 8 || gvMaster.IsEditing)
            {
                e.Row.Attributes["canSelect"] = "false";
            }
        }
    }

    protected void gvMaster_StartRowEditing(object sender, DevExpress.Web.Data.ASPxStartRowEditingEventArgs e)
    {
        string PRODNO = StringUtil.CStr(gvMaster.GetRowValuesByKeyValue(e.EditingKeyValue, "PRODNO"));

        gvMaster.Selection.UnselectAll();
        string status = StringUtil.CStr(gvMaster.GetRowValuesByKeyValue(e.EditingKeyValue, "STATUS"));
        dataStatusValue = status;

        foreach (GridViewColumn col in gvMaster.Columns)
        {
            if (col is GridViewDataColumn)
            {
                GridViewDataColumn dataCol = (GridViewDataColumn)col;
                Type typeCol = dataCol.GetType();

                if (dataCol.FieldName == "PRODNO")
                {
                    dataCol.ReadOnly = true;
                    dataCol.PropertiesEdit.Style.Border.BorderStyle = System.Web.UI.WebControls.BorderStyle.None;
                }

                if (status == "尚未生效")
                {
                    //if (dataCol.FieldName == "PRODTYPENO")
                    //{
                    //    GridViewDataComboBoxColumn cbCol = (GridViewDataComboBoxColumn)dataCol;
                    //    cbCol.PropertiesComboBox.ValidationSettings.RequiredField.IsRequired = true;
                    //    cbCol.PropertiesComboBox.DropDownButton.Enabled = true;
                    //    cbCol.PropertiesComboBox.DropDownButton.Visible = true;
                    //    cbCol.PropertiesComboBox.Width = 200;
                    //    cbCol.ReadOnly = false;

                    //    dataCol.ReadOnly = false;
                    //    dataCol.PropertiesEdit.Style.Border.BorderStyle = System.Web.UI.WebControls.BorderStyle.Solid;

                    //    dataCol.FieldName = "PRODTYPENO";
                    //}
                    //if (dataCol.FieldName == "IMEI_FLAG")
                    //{
                    //    GridViewDataComboBoxColumn cbCol = (GridViewDataComboBoxColumn)dataCol;
                    //    cbCol.PropertiesComboBox.ValidationSettings.RequiredField.IsRequired = true;
                    //    cbCol.PropertiesComboBox.DropDownButton.Enabled = true;
                    //    cbCol.PropertiesComboBox.DropDownButton.Visible = true;
                    //    cbCol.PropertiesComboBox.Width = 120;
                    //    cbCol.ReadOnly = false;
                    //    dataCol.ReadOnly = false;
                    //    dataCol.PropertiesEdit.Style.Border.BorderStyle = System.Web.UI.WebControls.BorderStyle.Solid;

                    //    dataCol.FieldName = "IMEI_FLAG";
                    //}

                    //**2011/02/24 Tina：PRODNO.Length == 9，於尚未生效時可編輯"檢核IMEI"、"自訂價格"
                    if (PRODNO.Length > 8)
                    {
                        dataCol.ReadOnly = true;
                        dataCol.PropertiesEdit.Style.Border.BorderStyle = System.Web.UI.WebControls.BorderStyle.None;

                        if (typeCol.Name.ToUpper() == "GRIDVIEWDATACOMBOBOXCOLUMN")
                        {
                            GridViewDataComboBoxColumn cbCol = (GridViewDataComboBoxColumn)dataCol;
                            cbCol.PropertiesComboBox.DropDownButton.Enabled = false;
                            cbCol.PropertiesComboBox.DropDownButton.Visible = false;

                            if (dataCol.FieldName == "PRODTYPENO") { dataCol.FieldName = "PRODTYPENAME"; }          //為唯讀時是顯示Text，而非Value，進而取消DropDown的功能
                        }

                        if (dataCol.FieldName == "IS_OPEN_PRICE")
                        {
                            dataCol.ReadOnly = false;
                        }

                        if (dataCol.FieldName == "IMEI_FLAG")
                        {
                            GridViewDataComboBoxColumn cbCol = dataCol as GridViewDataComboBoxColumn;
                            cbCol.PropertiesComboBox.DropDownButton.Enabled = true;
                            cbCol.PropertiesComboBox.DropDownButton.Visible = true;
                            cbCol.PropertiesComboBox.Width = 120;
                            dataCol.ReadOnly = false;
                            dataCol.PropertiesEdit.Style.Border.BorderStyle = System.Web.UI.WebControls.BorderStyle.Solid;
                        }
                    }
                }
                else
                {
                    dataCol.ReadOnly = true;
                    dataCol.PropertiesEdit.Style.Border.BorderStyle = System.Web.UI.WebControls.BorderStyle.None;

                    if (typeCol.Name.ToUpper() == "GRIDVIEWDATACOMBOBOXCOLUMN")
                    {
                        GridViewDataComboBoxColumn cbCol = (GridViewDataComboBoxColumn)dataCol;
                        cbCol.PropertiesComboBox.ValidationSettings.RequiredField.IsRequired = false;
                        cbCol.PropertiesComboBox.DropDownButton.Enabled = false;
                        cbCol.PropertiesComboBox.DropDownButton.Visible = false;

                        if (dataCol.FieldName == "PRODTYPENO") { dataCol.FieldName = "PRODTYPENAME"; }          //為唯讀時是顯示Text，而非Value，進而取消DropDown的功能
                        if (dataCol.FieldName == "IMEI_FLAG") { dataCol.FieldName = "CHECK_IMEI_TYPE_NAME"; }   //為唯讀時是顯示Text，而非Value，進而取消DropDown的功能
                    }

                }

            }
        }

    }

    protected void gvMaster_InitNewRow(object sender, DevExpress.Web.Data.ASPxDataInitNewRowEventArgs e)
    {
        if (gvMaster.IsNewRowEditing)
        {
            foreach (GridViewColumn col in gvMaster.Columns)
            {
                if (col is GridViewDataColumn)
                {
                    GridViewDataColumn dataCol = (GridViewDataColumn)col;
                    Type typeCol = dataCol.GetType();

                    if (dataCol.FieldName == "PRODNO")
                    {
                        dataCol.ReadOnly = false;
                        dataCol.PropertiesEdit.Style.Border.BorderStyle = System.Web.UI.WebControls.BorderStyle.Solid;
                    }

                    if (dataCol.FieldName == "S_DATE")
                    {
                        ASPxDateEdit dStart = gvMaster.FindEditRowCellTemplateControl((GridViewDataDateColumn)gvMaster.Columns["S_DATE"], "txtSDATE") as ASPxDateEdit;
                        dStart.ClientEnabled = true;
                        if (e.NewValues["S_DATE"] == null)
                        {
                            dStart.Value = Convert.ToDateTime(System.DateTime.Now.AddDays(1).ToString("yyyy/MM/dd"));
                        }
                    }

                }
            }
        }
    }

    protected void gvMaster_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
    {
        OPT10_Product_DTO OPT10_Product_DTO = new OPT10_Product_DTO();
        OPT10_Product_DTO.PRODUCTDataTable dtSYS;
        OPT10_Product_DTO.PRODUCTRow drSYS;

        dtSYS = OPT10_Product_DTO.Tables["PRODUCT"] as OPT10_Product_DTO.PRODUCTDataTable;
        drSYS = dtSYS.NewPRODUCTRow();

        //ACCOUNT_CODE        
        string ACCOUNT_CODE = null;

        ACCOUNT_CODE += StringUtil.CStr(e.NewValues["ACC1"]).Trim();
        ACCOUNT_CODE += StringUtil.CStr(e.NewValues["ACC2"]).Trim();
        ACCOUNT_CODE += StringUtil.CStr(e.NewValues["ACC3"]).Trim();
        ACCOUNT_CODE += StringUtil.CStr(e.NewValues["ACC4"]).Trim();
        ACCOUNT_CODE += StringUtil.CStr(e.NewValues["ACC5"]).Trim();
        ACCOUNT_CODE += StringUtil.CStr(e.NewValues["ACC6"]).Trim();

        //異動的欄位
        drSYS["STATUS"] = e_status;
        drSYS["PRODNO"] = StringUtil.CStr(e.NewValues["PRODNO"]);
        drSYS["PRODNAME"] = StringUtil.CStr(e.NewValues["PRODNAME"]);
        drSYS["UNIT"] = StringUtil.CStr(e.NewValues["UNIT"]);
        drSYS["PRICE"] = StringUtil.CStr(e.NewValues["PRICE"]);
        if (e.NewValues["S_DATE"] != null)
        {
            drSYS["S_DATE"] = DateTime.Parse(StringUtil.CStr(e.NewValues["S_DATE"]));
        }
        else
        { drSYS["S_DATE"] = Advtek.Utility.DateUtil.NullDateFormat("S_DATE"); }

        if (e.NewValues["E_DATE"] != null)
        {
            drSYS["E_DATE"] = DateTime.Parse(StringUtil.CStr(e.NewValues["E_DATE"]));
        }
        else
        { drSYS["E_DATE"] = Advtek.Utility.DateUtil.NullDateFormat("E_DATE"); }

        if (e.NewValues["ISSTOCK"] == null)
        { drSYS["ISSTOCK"] = "N"; }
        else
        { drSYS["ISSTOCK"] = (StringUtil.CStr(e.NewValues["ISSTOCK"]) == "True") ? "Y" : "N"; }

        drSYS["ACCOUNTCODE"] = ACCOUNT_CODE;

        drSYS["PRODTYPENO"] = (e.NewValues["PRODTYPENO"] == null ? "" : StringUtil.CStr(e.NewValues["PRODTYPENO"]));
        drSYS["IMEI_FLAG"] = StringUtil.CStr(e.NewValues["IMEI_FLAG"]);
        //default
        if (e.NewValues["IS_OPEN_PRICE"] == null)
        { drSYS["IS_OPEN_PRICE"] = "N"; }
        else
        { drSYS["IS_OPEN_PRICE"] = (StringUtil.CStr(e.NewValues["IS_OPEN_PRICE"]) == "True") ? "Y" : "N"; }

        drSYS["COMPANYCODE"] = "01";
        drSYS["IS_DISCOUNT"] = "N";
        drSYS["DEL_FLAG"] = "N";
        drSYS["TAXABLE"] = "Y";
        drSYS["TAXRATE"] = "5";
        drSYS["DS_FLAG"] = "N";
        drSYS["MODI_USER"] = logMsg.MODI_USER;
        drSYS["MODI_DTM"] = Convert.ToDateTime(System.DateTime.Now);

        drSYS["CREATE_USER"] = drSYS["MODI_USER"];
        drSYS["CREATE_DTM"] = drSYS["MODI_DTM"];

        dtSYS.AddPRODUCTRow(drSYS);
        OPT10_Product_DTO.AcceptChanges();

        //更新資料庫
        new OPT10_Facade().AddNewOne_ProductMethodSet(OPT10_Product_DTO);

        ((ASPxGridView)sender).CancelEdit();
        e.Cancel = true;
        bindMasterData();
        gvMaster.Selection.UnselectAll();
        gvMaster.PageIndex = 0;

        bindMasterDataAfterWrite();

    }

    protected void gvMaster_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
    {
        OPT10_Product_DTO OPT10_Product_DTO = new OPT10_Product_DTO();
        OPT10_Product_DTO.PRODUCTDataTable dtSYS;
        OPT10_Product_DTO.PRODUCTRow drSYS;

        dtSYS = OPT10_Product_DTO.Tables["PRODUCT"] as OPT10_Product_DTO.PRODUCTDataTable;

        if (StringUtil.CStr(e.Keys["PRODNO"]).Length > 8)
        {
            //不變動的欄位
            dtSYS.Columns["STATUS"].AllowDBNull = true;
            dtSYS.Columns["PRODNO"].AllowDBNull = true;
            dtSYS.Columns["PRODNAME"].AllowDBNull = true;
            dtSYS.Columns["UNIT"].AllowDBNull = true;
            dtSYS.Columns["PRICE"].AllowDBNull = true;
            dtSYS.Columns["S_DATE"].AllowDBNull = true;
            dtSYS.Columns["E_DATE"].AllowDBNull = true;
            dtSYS.Columns["ISSTOCK"].AllowDBNull = true;
            dtSYS.Columns["ACCOUNTCODE"].AllowDBNull = true;
            dtSYS.Columns["PRODTYPENO"].AllowDBNull = true;
            dtSYS.Columns["COMPANYCODE"].AllowDBNull = true;
            dtSYS.Columns["IS_DISCOUNT"].AllowDBNull = true;
            dtSYS.Columns["DEL_FLAG"].AllowDBNull = true;
            dtSYS.Columns["TAXABLE"].AllowDBNull = true;
            dtSYS.Columns["TAXRATE"].AllowDBNull = true;
            dtSYS.Columns["DS_FLAG"].AllowDBNull = true;
            dtSYS.Columns["CREATE_USER"].AllowDBNull = true;
            dtSYS.Columns["CREATE_DTM"].AllowDBNull = true;

            drSYS = dtSYS.NewPRODUCTRow();

            //PK值
            drSYS["PRODNO"] = StringUtil.CStr(e.Keys["PRODNO"]);

            //異動的欄位
            drSYS["IMEI_FLAG"] = StringUtil.CStr(e.NewValues["IMEI_FLAG"]);
            drSYS["IS_OPEN_PRICE"] = (StringUtil.CStr(e.NewValues["IS_OPEN_PRICE"]) == "True") ? "Y" : "N";
            drSYS["MODI_USER"] = logMsg.MODI_USER;
            drSYS["MODI_DTM"] = Convert.ToDateTime(System.DateTime.Now);

            dtSYS.AddPRODUCTRow(drSYS);

        }
        else
        {
            //不變動
            dtSYS.Columns["STATUS"].AllowDBNull = true;
            dtSYS.Columns["PRODNO"].AllowDBNull = true;
            //**2011/03/10 Tina：IS_OPEN_PRICE(自訂價格) 於「尚未生效」時仍可以編輯，「有效」時只能變更結束日期，所以在此要開放可以變動自訂價格
            //dtSYS.Columns["IS_OPEN_PRICE"].AllowDBNull = true;
            dtSYS.Columns["CREATE_USER"].AllowDBNull = true;
            dtSYS.Columns["CREATE_DTM"].AllowDBNull = true;

            string _status = StringUtil.CStr(e.NewValues["STATUS"]);

            if (_status == "有效")
            {
                dtSYS.Columns["PRODTYPENO"].AllowDBNull = true;
                dtSYS.Columns["IMEI_FLAG"].AllowDBNull = true;
            }


            drSYS = dtSYS.NewPRODUCTRow();

            //PK值
            drSYS["PRODNO"] = StringUtil.CStr(e.Keys["PRODNO"]);

            //ACCOUNT_CODE        
            string ACCOUNT_CODE = null;

            ACCOUNT_CODE += StringUtil.CStr(e.NewValues["ACC1"]).Trim();
            ACCOUNT_CODE += StringUtil.CStr(e.NewValues["ACC2"]).Trim();
            ACCOUNT_CODE += StringUtil.CStr(e.NewValues["ACC3"]).Trim();
            ACCOUNT_CODE += StringUtil.CStr(e.NewValues["ACC4"]).Trim();
            ACCOUNT_CODE += StringUtil.CStr(e.NewValues["ACC5"]).Trim();
            ACCOUNT_CODE += StringUtil.CStr(e.NewValues["ACC6"]).Trim();

            //異動的欄位
            drSYS["PRODNAME"] = StringUtil.CStr(e.NewValues["PRODNAME"]);
            drSYS["UNIT"] = StringUtil.CStr(e.NewValues["UNIT"]);
            drSYS["PRICE"] = StringUtil.CStr(e.NewValues["PRICE"]);
            if (e.NewValues["S_DATE"] != null)
            {
                drSYS["S_DATE"] = DateTime.Parse(StringUtil.CStr(e.NewValues["S_DATE"]));  //S_DATE 
            }
            else
            { drSYS["S_DATE"] = Advtek.Utility.DateUtil.NullDateFormat("S_DATE"); }

            if (e.NewValues["E_DATE"] != null)
            {
                drSYS["E_DATE"] = DateTime.Parse(StringUtil.CStr(e.NewValues["E_DATE"]));  //E_DATE 
            }
            else
            { drSYS["E_DATE"] = Advtek.Utility.DateUtil.NullDateFormat("E_DATE"); }


            drSYS["ISSTOCK"] = (StringUtil.CStr(e.NewValues["ISSTOCK"]) == "True") ? "Y" : "N";
            drSYS["ACCOUNTCODE"] = ACCOUNT_CODE;

            if (_status != "有效")
            {
                drSYS["PRODTYPENO"] = StringUtil.CStr(e.NewValues["PRODTYPENO"]);
                drSYS["IMEI_FLAG"] = StringUtil.CStr(e.NewValues["IMEI_FLAG"]);
            }

            if (e.NewValues["IS_OPEN_PRICE"] == null)
            { drSYS["IS_OPEN_PRICE"] = "N"; }
            else
            { drSYS["IS_OPEN_PRICE"] = (StringUtil.CStr(e.NewValues["IS_OPEN_PRICE"]) == "True") ? "Y" : "N"; }

            //default
            drSYS["COMPANYCODE"] = "01";
            drSYS["DEL_FLAG"] = "N";
            drSYS["TAXABLE"] = "Y";
            drSYS["TAXRATE"] = "5";
            drSYS["DS_FLAG"] = "N";

            drSYS["MODI_USER"] = logMsg.MODI_USER;
            drSYS["MODI_DTM"] = Convert.ToDateTime(System.DateTime.Now);

            dtSYS.AddPRODUCTRow(drSYS);
        }

        OPT10_Product_DTO.AcceptChanges();


        //更新資料庫
        new OPT10_Facade().UpdateOne_ProductMethodSet(OPT10_Product_DTO);

        ((ASPxGridView)sender).CancelEdit();
        e.Cancel = true;

        bindMasterDataAfterWrite();
        bindMasterData();
        gvMaster.Selection.UnselectAll();
        gvMaster.PageIndex = 0;

    }

    protected void gvMaster_RowValidating(object sender, DevExpress.Web.Data.ASPxDataValidationEventArgs e)
    {
        string ProdNo = StringUtil.CStr(e.NewValues["PRODNO"]);
        string UNIT = StringUtil.CStr(e.NewValues["UNIT"]);
        string PRICE = StringUtil.CStr(e.NewValues["PRICE"]);

        string _ACC1 = StringUtil.CStr(e.NewValues["ACC1"]);
        string _ACC2 = StringUtil.CStr(e.NewValues["ACC2"]);
        string _ACC3 = StringUtil.CStr(e.NewValues["ACC3"]);
        string _ACC4 = StringUtil.CStr(e.NewValues["ACC4"]);
        string _ACC5 = StringUtil.CStr(e.NewValues["ACC5"]);
        string _ACC6 = StringUtil.CStr(e.NewValues["ACC6"]);

        if (e.NewValues["S_DATE"] == null || StringUtil.CStr(e.NewValues["S_DATE"]).Trim() == "")
        {
            e.RowError = "【開始日期】不允許空值，請重新輸入";
            return;
        }
        else
        {
            if (Convert.ToDateTime(e.NewValues["S_DATE"]).Date < DateTime.Now.Date)
            {
                if (StringUtil.CStr(e.NewValues["STATUS"]) != "有效")
                {
                    e.RowError = "【開始日期】必須大於系統日期";
                    return;
                }

            }
            else
            {
                if (e.NewValues["E_DATE"] != null && StringUtil.CStr(e.NewValues["E_DATE"]).Trim() != "")
                {
                    if (StringUtil.CStr(e.NewValues["E_DATE"]).Substring(0, 4) != "0001" && Convert.ToDateTime(e.NewValues["E_DATE"]).Date <= DateTime.Now.Date)
                    {
                        e.RowError = "【結束日期】必須大於系統日期";
                        return;
                    }
                    else
                    {
                        if (StringUtil.CStr(e.NewValues["E_DATE"]).Substring(0, 4) != "0001" && Convert.ToDateTime(e.NewValues["E_DATE"]).Date < Convert.ToDateTime(e.NewValues["S_DATE"]).Date)
                        {
                            e.RowError = "【結束日期】必須>=【開始日期】";
                            return;
                        }

                    }
                }
            }
        }



        if (int.Parse(StringUtil.CStr(e.NewValues["PRICE"])) < 0)
        {
            e.RowError = "【單機價格】不可小於零，請重新輸入";
            return;

        }

        if (gvMaster.IsNewRowEditing)
        {
            string log = new Product_Facade().Check_Id(StringUtil.CStr(e.NewValues["PRODNO"]));
            if (log != "")
            {
                e.RowError = log;
                return;
            }
        }

        if (e.IsNewRow)
        {

            if (ProdNo.Trim().Length != 8)
            {
                e.RowError = "商品料號請輸入8碼!!";
                return;
            }

            if (UNIT.Trim() == "")
            {
                e.RowError = "請輸入'單位'!!";
                return;
            }
            if (PRICE.Trim() == "")
            {
                e.RowError = "請輸入'單機價格'!!";
                return;
            }

            double Num;
            bool isNum1 = double.TryParse(_ACC1.Trim(), out Num);
            bool isNum2 = double.TryParse(_ACC2.Trim(), out Num);
            bool isNum3 = double.TryParse(_ACC3.Trim(), out Num);
            bool isNum4 = double.TryParse(_ACC4.Trim(), out Num);
            bool isNum5 = double.TryParse(_ACC5.Trim(), out Num);
            bool isNum6 = double.TryParse(_ACC6.Trim(), out Num);
            if (!isNum1 || !isNum2 || !isNum3 || !isNum4 || !isNum5 || !isNum6)
            {
                e.RowError = "'科目'欄位不可輸入非數值!!";
                return;
            }
        }


    }

    #endregion



}
