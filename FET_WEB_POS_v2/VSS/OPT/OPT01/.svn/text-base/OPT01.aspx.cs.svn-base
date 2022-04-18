using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using DevExpress.Web.ASPxGridView;
using DevExpress.Web.ASPxPager;
using DevExpress.Web.ASPxEditors;
using FET.POS.Model.Facade;
using FET.POS.Model.Facade.FacadeImpl;
using FET.POS.Model.DTO;
using FET.POS.Model.Common;
using System.Runtime.Serialization;
using Advtek.Utility;

public partial class VSS_OPT_OPT01 : BasePage
{
    #region Class Varibles

    string AccountName
    {
        set
        {
            ViewState["AccountName"] = value;
        }
        get
        {
            if (ViewState["AccountName"] == null)
                return string.Empty;

            return (string)ViewState["AccountName"];
        }
    }

    string cbPayModeValue
    {
        set
        {
            ViewState["cbPayModeValue"] = value;
        }
        get
        {
            if (ViewState["cbPayModeValue"] == null)
                return string.Empty;

            return (string)ViewState["cbPayModeValue"];
        }
    }

    string cbStatusValue
    {
        set
        {
            ViewState["cbStatusValue"] = value;
        }
        get
        {
            if (ViewState["cbStatusValue"] == null)
                return string.Empty;

            return (string)ViewState["cbStatusValue"];
        }
    }

    //string dataStatusValue
    //{
    //    set
    //    {
    //        ViewState["dataStatusValue"] = value;
    //    }
    //    get
    //    {
    //        if (ViewState["dataStatusValue"] == null)
    //            return string.Empty;

    //        return (string)ViewState["dataStatusValue"];
    //    }
    //}

    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack && !Page.IsCallback)
        {
            GetEmptygvMasterSet();
            GetcbPayMode();
        }

        //GridViewDataComboBoxColumn PAY_MODE = gvMaster.Columns["PAY_MODE_ID"] as GridViewDataComboBoxColumn;
        //PAY_MODE.PropertiesComboBox.DataSource = OPT01_PageHelper.GetPaymentModeName(false);
        //PAY_MODE.PropertiesComboBox.TextField = "PayModeName";
        //PAY_MODE.PropertiesComboBox.ValueField = "PayModeId";
      
    }

    private void GetcbPayMode()
    {
        cbPayMode.DataSource = OPT01_PageHelper.GetPaymentModeName(true);
        cbPayMode.ValueField = "PayModeId";
        cbPayMode.TextField = "PayModeName";
        cbPayMode.DataBind();
    }

    private void GetEmptygvMasterSet()
    {
        DataTable dt = new DataTable();
        dt = OPT01_PageHelper.Query_EmptyPaymentMethodSet();

        gvMaster.DataSource = dt;
        gvMaster.DataBind();
    }

    private void bindMasterData()
    {
        gvMaster.CancelEdit();
        OPT01_Facade OPT01_Facade = new OPT01_Facade();

        DataTable dtQuery = new DataTable();
        AccountName = txtAccountName.Text.Trim();
        cbPayModeValue = StringUtil.CStr(cbPayMode.SelectedItem.Value);
        cbStatusValue = StringUtil.CStr(cbStatus.SelectedItem.Value);

        dtQuery = OPT01_Facade.Query_PaymentMethodSet(AccountName, 
                                                    cbPayModeValue, 
                                                    cbStatusValue,
                                                    txtSDate_S.Text,
                                                    txtSDate_E.Text,
                                                    txtEDate_S.Text,
                                                    txtEDate_E.Text);
        gvMaster.DataSource = dtQuery;
        gvMaster.DataBind();
        gvMaster.Selection.UnselectAll();
        ViewState["gvMaster"] = dtQuery;
    }

    #region Button 觸發事件

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        //try
        //{
        //    if (!(String.IsNullOrEmpty(StartDate.Text)) && !(String.IsNullOrEmpty(EndDate.Text)) && Convert.ToDateTime(StartDate.Text) > Convert.ToDateTime(EndDate.Text))
        //    {
        //        EndDate.Focus();
        //        throw new Exception("結束日期不允許小於開始日期，請重新輸入");
        //    }
            

        //}
        //catch (Exception ex)
        //{
        //    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Common", @"alert('" + ex.Message + "');", true);
        //    return;
        //}
        bindMasterData();
        gvMaster.FocusedRowIndex = -1;
        gvMaster.Selection.UnselectAll();
        gvMaster.PageIndex = 0;
    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        gvMaster.AddNewRow();
    }

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        try
        {
            List<object> gvPKValues = gvMaster.GetSelectedFieldValues(gvMaster.KeyFieldName);
            string pkFName = gvMaster.KeyFieldName;

            OPT01_PaymentMethodSet_DTO PaymentMethodSet_DTO = new OPT01_PaymentMethodSet_DTO();

            DataTable PaymentDataTable = new DataTable();
            PaymentDataTable.TableName = PaymentMethodSet_DTO.PAYMENT_METHOD_SET.TableName;

            PaymentDataTable.Columns.Add(pkFName, typeof(string));

            if (ViewState["gvMaster"] == null) { return; }
            DataTable dt = new DataTable();
            if (ViewState["gvMaster"] != null) { dt = (DataTable)ViewState["gvMaster"]; };

            for (int i = 0; i < gvPKValues.Count; i++)
            {
                if (dt.AsEnumerable().Any(dr => dr.Field<string>("PAYMENT_METHOD_ID").Equals(StringUtil.CStr(gvPKValues[i]))))
                {
                    DataRow PaymentDataRow = PaymentDataTable.NewRow();
                    PaymentDataRow[pkFName] = StringUtil.CStr(gvPKValues[i]);
                    PaymentDataTable.Rows.Add(PaymentDataRow);

                    DataRow dr1 = dt.AsEnumerable().Single(dr => dr.Field<string>("PAYMENT_METHOD_ID") == StringUtil.CStr(gvPKValues[i]));
                    dt.Rows.Remove(dr1);
                }
            }

            new OPT01_Facade().Delete_PAYMENT_METHOD_SET(PaymentDataTable, pkFName);

            bindMasterData();
        }
        catch (Exception ex)
        {
            if (ex.Message != "DELETE SQL Execute 失敗. ")
            {
                Logger.Log.Error(ex.Message, ex);
            }
        }

    }

    #endregion
    
    #region gvMaster 觸發事件

    protected void gvMaster_PageIndexChanged(object sender, EventArgs e)
    {
        bindMasterData();
    }

    protected void gvMaster_CommandButtonInitialize(object sender, ASPxGridViewCommandButtonEventArgs e)
    {
        if (e.VisibleIndex > -1)
        {
            if (e.ButtonType == ColumnCommandButtonType.Edit)
            {
                string status1 = StringUtil.CStr(gvMaster.GetRowValues(e.VisibleIndex, "STATUS"));

                if (status1 == "已過期")
                {
                    e.Enabled = false;
                }
            }
            if (e.ButtonType == ColumnCommandButtonType.SelectCheckbox)
            {
                string status1 = StringUtil.CStr(gvMaster.GetRowValues(e.VisibleIndex, "STATUS"));

                if (status1 == "尚未生效")
                {
                    e.Enabled = true;
                }
                else
                {
                    e.Enabled = false;
                }
            }
        }

    }

    protected void gvMaster_HtmlRowPrepared(object sender, ASPxGridViewTableRowEventArgs e)
    {
        e.Row.Attributes["canSelect"] = "true";
        if (e.RowType == GridViewRowType.Data)
        {
            string status1 = StringUtil.CStr(gvMaster.GetRowValues(e.VisibleIndex, "STATUS"));
            if (status1 == "已過期" || status1 == "有效")
            {
                e.Row.Attributes["canSelect"] = "false";
            }
        }
    }

    protected void gvMaster_InitNewRow(object sender, DevExpress.Web.Data.ASPxDataInitNewRowEventArgs e)
    {
        gvMaster.Selection.UnselectAll();
        foreach (GridViewColumn col in gvMaster.Columns)
        {
            if (col is GridViewDataColumn)
            {
                GridViewDataColumn dataCol = (GridViewDataColumn)col;
                Type typeCol = dataCol.GetType();

                dataCol.ReadOnly = false;
                dataCol.PropertiesEdit.Style.Border.BorderStyle = System.Web.UI.WebControls.BorderStyle.Solid;

                if (typeCol.Name == "GridViewDataComboBoxColumn")
                {
                    GridViewDataComboBoxColumn cbCol = (GridViewDataComboBoxColumn)dataCol;
                    cbCol.PropertiesComboBox.ValidationSettings.RequiredField.IsRequired = true;
                    cbCol.PropertiesComboBox.ValidationSettings.RequiredField.ErrorText = "必填欄位";
                    cbCol.PropertiesComboBox.DropDownButton.Enabled = true;
                    cbCol.PropertiesComboBox.DropDownButton.Visible = true;
                }

                if (typeCol.Name == "GridViewDataDateColumn")
                {
                    GridViewDataDateColumn dateCol = (GridViewDataDateColumn)dataCol;
                    dateCol.ReadOnly = false;
                    dateCol.PropertiesDateEdit.ValidationSettings.RequiredField.IsRequired = true;
                    dateCol.PropertiesDateEdit.ValidationSettings.RequiredField.ErrorText = "必填欄位";
                    dateCol.PropertiesDateEdit.Style.Border.BorderStyle = System.Web.UI.WebControls.BorderStyle.Solid;
                    dateCol.PropertiesDateEdit.DropDownButton.Visible = true;
                    dateCol.PropertiesDateEdit.DropDownButton.Enabled = true;
                }
                if (dataCol.FieldName == "ITEMNO")
                {
                    GridViewDataTextColumn textCol = (GridViewDataTextColumn)gvMaster.Columns["ITEMNO"];
                    textCol.ReadOnly = true;
                    textCol.PropertiesEdit.Style.Border.BorderStyle = System.Web.UI.WebControls.BorderStyle.None;
                }
                if (dataCol.FieldName == "STATUS")
                {
                    GridViewDataTextColumn textCol = (GridViewDataTextColumn)gvMaster.Columns["STATUS"];
                    textCol.ReadOnly = true;
                    textCol.PropertiesEdit.Style.Border.BorderStyle = System.Web.UI.WebControls.BorderStyle.None;
                }
                if (dataCol.FieldName == "MODI_USER_NAME")
                {
                    GridViewDataTextColumn textCol = (GridViewDataTextColumn)gvMaster.Columns["MODI_USER_NAME"];
                    textCol.ReadOnly = true;
                    textCol.PropertiesEdit.Style.Border.BorderStyle = System.Web.UI.WebControls.BorderStyle.None;
                }

                if (dataCol.FieldName == "MODI_DTM")
                {
                    GridViewDataDateColumn col_MODI_DTM = (GridViewDataDateColumn)gvMaster.Columns["MODI_DTM"];
                    col_MODI_DTM.ReadOnly = true;
                    col_MODI_DTM.PropertiesDateEdit.ValidationSettings.RequiredField.IsRequired = false;
                    col_MODI_DTM.PropertiesDateEdit.Style.Border.BorderStyle = System.Web.UI.WebControls.BorderStyle.Solid;
                    col_MODI_DTM.PropertiesDateEdit.DropDownButton.Visible = false;
                    col_MODI_DTM.PropertiesDateEdit.DropDownButton.Enabled = false;
                }

                if (dataCol.FieldName == "S_DATE")
                {
                    GridViewDataDateColumn col_S_DATE = (GridViewDataDateColumn)gvMaster.Columns["S_DATE"];
                    col_S_DATE.PropertiesDateEdit.ValidationSettings.RequiredField.IsRequired = true;
                    col_S_DATE.PropertiesDateEdit.ValidationSettings.RequiredField.ErrorText = "必填欄位";
                }
                if (dataCol.FieldName == "E_DATE")
                {
                    GridViewDataDateColumn col_E_DATE = (GridViewDataDateColumn)gvMaster.Columns["E_DATE"];
                    col_E_DATE.PropertiesDateEdit.ValidationSettings.RequiredField.IsRequired = false;
                }
            }
        }
        e.NewValues["S_DATE"] = DateTime.Now.AddDays(1).ToString("yyyy/MM/dd");
    }

    protected void gvMaster_StartRowEditing(object sender, DevExpress.Web.Data.ASPxStartRowEditingEventArgs e)
    {
        gvMaster.Selection.UnselectAll();
        string status = StringUtil.CStr(gvMaster.GetRowValuesByKeyValue(e.EditingKeyValue, "STATUS"));

        if (gvMaster.IsEditing)
        {
            foreach (GridViewColumn col in gvMaster.Columns)
            {
                if (col is GridViewDataColumn)
                {
                    GridViewDataColumn dataCol = (GridViewDataColumn)col;
                    Type typeCol = dataCol.GetType();

                    dataCol.ReadOnly = true;
                    dataCol.PropertiesEdit.Style.Border.BorderStyle = System.Web.UI.WebControls.BorderStyle.None;

                    if (typeCol.Name == "GridViewDataComboBoxColumn")
                    {
                        GridViewDataComboBoxColumn cbCol = (GridViewDataComboBoxColumn)dataCol;
                        cbCol.PropertiesComboBox.ValidationSettings.RequiredField.IsRequired = false;
                        cbCol.PropertiesComboBox.DropDownButton.Enabled = false;
                        cbCol.PropertiesComboBox.DropDownButton.Visible = false;
                    }

                    if (typeCol.Name == "GridViewDataDateColumn")
                    {
                        GridViewDataDateColumn DateCol = (GridViewDataDateColumn)dataCol;
                        DateCol.ReadOnly = true;
                        DateCol.PropertiesDateEdit.DropDownButton.Visible = false;
                        DateCol.PropertiesDateEdit.DropDownButton.Enabled = false;
                    }

                    if (status == "有效")
                    {
                        if (dataCol.FieldName == "E_DATE")
                        {
                            GridViewDataDateColumn col_E_DATE = (GridViewDataDateColumn)gvMaster.Columns["E_DATE"];
                            col_E_DATE.ReadOnly = false;
                            col_E_DATE.PropertiesDateEdit.ValidationSettings.RequiredField.IsRequired = false;
                            col_E_DATE.PropertiesDateEdit.Style.Border.BorderStyle = System.Web.UI.WebControls.BorderStyle.Solid;
                            col_E_DATE.PropertiesDateEdit.DropDownButton.Visible = true;
                            col_E_DATE.PropertiesDateEdit.DropDownButton.Enabled = true;
                        }
                    }
                    if (status == "尚未生效")
                    {
                        if (dataCol.FieldName == "S_DATE")
                        {
                            GridViewDataDateColumn col_S_DATE = (GridViewDataDateColumn)gvMaster.Columns["S_DATE"];
                            col_S_DATE.ReadOnly = false;
                            col_S_DATE.PropertiesDateEdit.ValidationSettings.RequiredField.IsRequired = true;
                            col_S_DATE.PropertiesDateEdit.ValidationSettings.RequiredField.ErrorText = "必填欄位";
                            col_S_DATE.PropertiesDateEdit.Style.Border.BorderStyle = System.Web.UI.WebControls.BorderStyle.Solid;
                            col_S_DATE.PropertiesDateEdit.DropDownButton.Visible = true;
                            col_S_DATE.PropertiesDateEdit.DropDownButton.Enabled = true;
                        }
                        if (dataCol.FieldName == "E_DATE")
                        {
                            GridViewDataDateColumn col_E_DATE = (GridViewDataDateColumn)gvMaster.Columns["E_DATE"];
                            col_E_DATE.ReadOnly = false;
                            col_E_DATE.PropertiesDateEdit.ValidationSettings.RequiredField.IsRequired = false;
                            col_E_DATE.PropertiesDateEdit.Style.Border.BorderStyle = System.Web.UI.WebControls.BorderStyle.Solid;
                            col_E_DATE.PropertiesDateEdit.DropDownButton.Visible = true;
                            col_E_DATE.PropertiesDateEdit.DropDownButton.Enabled = true;
                        }
                        if (dataCol.FieldName == "PAY_MODE_ID")
                        {
                            GridViewDataComboBoxColumn col_PAY_MODE_NAME = (GridViewDataComboBoxColumn)gvMaster.Columns["PAY_MODE_ID"];
                            col_PAY_MODE_NAME.PropertiesComboBox.ValidationSettings.RequiredField.IsRequired = false;
                            col_PAY_MODE_NAME.PropertiesComboBox.DropDownButton.Enabled = true;
                            col_PAY_MODE_NAME.PropertiesComboBox.DropDownButton.Visible = true;
                            col_PAY_MODE_NAME.PropertiesEdit.Style.Border.BorderStyle = System.Web.UI.WebControls.BorderStyle.NotSet;
                            col_PAY_MODE_NAME.ReadOnly = false;
                        }

                        if (dataCol.FieldName.Substring(0,3) == "ACC")
                        {
                            GridViewDataTextColumn col_ACC = (GridViewDataTextColumn)gvMaster.Columns[dataCol.FieldName];
                            col_ACC.ReadOnly = false;
                            col_ACC.PropertiesEdit.Style.Border.BorderStyle = System.Web.UI.WebControls.BorderStyle.NotSet;
                            col_ACC.Width = 100;
                        }
                        
                    }
                }
            }
        }
    }

    protected void gvMaster_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
    {

        ASPxGridView grid = (ASPxGridView)sender;
        OPT01_Facade OPT01_Facade = new OPT01_Facade();
        OPT01_PaymentMethodSet_DTO PaymentMethodSet_DTO = new OPT01_PaymentMethodSet_DTO();
        OPT01_PaymentMethodSet_DTO.PAYMENT_METHOD_SETDataTable dtPaymentMethodSet;
        OPT01_PaymentMethodSet_DTO.PAYMENT_METHOD_SETRow drPaymentMethodSet;
        dtPaymentMethodSet = PaymentMethodSet_DTO.Tables["PAYMENT_METHOD_SET"] as OPT01_PaymentMethodSet_DTO.PAYMENT_METHOD_SETDataTable;
        dtPaymentMethodSet.Columns["E_DATE"].AllowDBNull = true;

        drPaymentMethodSet = dtPaymentMethodSet.NewPAYMENT_METHOD_SETRow();

        //get edit row by key to datatable
        DataTable dtOld = OPT01_PageHelper.Query_PaymentMethodSetByKey(StringUtil.CStr(e.Keys["PAYMENT_METHOD_ID"]));
        //ACCOUNT_CODE        
        string ACCOUNT_CODE = null;

        ACCOUNT_CODE += StringUtil.CStr(e.NewValues["ACC1"]).Trim();
        ACCOUNT_CODE += StringUtil.CStr(e.NewValues["ACC2"]).Trim();
        ACCOUNT_CODE += StringUtil.CStr(e.NewValues["ACC3"]).Trim();
        ACCOUNT_CODE += StringUtil.CStr(e.NewValues["ACC4"]).Trim();
        ACCOUNT_CODE += StringUtil.CStr(e.NewValues["ACC5"]).Trim();
        ACCOUNT_CODE += StringUtil.CStr(e.NewValues["ACC6"]).Trim();

        //ACCOUNT CODE LENGTH 2 3 4 6 4 4 Totoal 23 bytes
        drPaymentMethodSet.PAYMENT_METHOD_ID = StringUtil.CStr(e.Keys["PAYMENT_METHOD_ID"]);  //PAYMENT_METHOD_ID 
        drPaymentMethodSet.PAY_MODE_ID = StringUtil.CStr(e.NewValues["PAY_MODE_ID"]);   //PAY_MODE_ID
        drPaymentMethodSet.ACCOUNT_CODE = ACCOUNT_CODE;  //ACCOUNT_CODE

        //string dataStatusValue = StringUtil.CStr(e..GetRowValuesByKeyValue(e.EditingKeyValue, "STATUS"));

        //if (dataStatusValue == "尚未生效")
        //{
            drPaymentMethodSet.S_DATE = DateTime.Parse(StringUtil.CStr(e.NewValues["S_DATE"]));  //S_DATE 
        //}
        //else
        //{
        //     drPaymentMethodSet.S_DATE = DateTime.Parse(StringUtil.CStr(dtOld.Rows[0]["S_DATE"]));  //S_DATE 
        //}

        // if (dataStatusValue == "有效" || dataStatusValue == "尚未生效")
        //{
            if (e.NewValues["E_DATE"] != null)
            {
                drPaymentMethodSet.E_DATE = DateTime.Parse(StringUtil.CStr(e.NewValues["E_DATE"]));  //E_DATE                    
            }
            else
            {
                drPaymentMethodSet["E_DATE"] = DBNull.Value;// DateTime.Parse("9999/12/31");
            }

        //}
        //else
        //{
        //    drPaymentMethodSet.E_DATE = DateTime.Parse(StringUtil.CStr(dtOld.Rows[0]["E_DATE"]));  //E_DATE 
        //}


        drPaymentMethodSet.CREATE_USER = StringUtil.CStr(dtOld.Rows[0]["CREATE_USER"]); //CREATE_USER 
        drPaymentMethodSet.CREATE_DTM = DateTime.Parse(StringUtil.CStr(dtOld.Rows[0]["CREATE_DTM"])); //CREATE_DTM  
        drPaymentMethodSet.MODI_USER = logMsg.OPERATOR;  //MODI_USER 
        drPaymentMethodSet.MODI_DTM = DateTime.Now;  //MODI_DTM 

        dtPaymentMethodSet.Rows.Add(drPaymentMethodSet);

        PaymentMethodSet_DTO.AcceptChanges();

        //update database
         OPT01_Facade.UpdateOne_PaymentMethodSet(PaymentMethodSet_DTO);

        grid.CancelEdit();
        e.Cancel = true;

        bindMasterData();
    }

    protected void gvMaster_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
    {
        ASPxGridView grid = (ASPxGridView)sender;
        OPT01_Facade OPT01_Facade = new OPT01_Facade();
        OPT01_PaymentMethodSet_DTO PaymentMethodSet_DTO = new OPT01_PaymentMethodSet_DTO();
        OPT01_PaymentMethodSet_DTO.PAYMENT_METHOD_SETDataTable dtPaymentMethodSet;
        OPT01_PaymentMethodSet_DTO.PAYMENT_METHOD_SETRow drPaymentMethodSet;
        dtPaymentMethodSet = PaymentMethodSet_DTO.Tables["PAYMENT_METHOD_SET"] as OPT01_PaymentMethodSet_DTO.PAYMENT_METHOD_SETDataTable;
        drPaymentMethodSet = dtPaymentMethodSet.NewPAYMENT_METHOD_SETRow();


        //ACCOUNT_CODE        
        string ACCOUNT_CODE = null;

        ACCOUNT_CODE += StringUtil.CStr(e.NewValues["ACC1"]).Trim();
        ACCOUNT_CODE += StringUtil.CStr(e.NewValues["ACC2"]).Trim();
        ACCOUNT_CODE += StringUtil.CStr(e.NewValues["ACC3"]).Trim();
        ACCOUNT_CODE += StringUtil.CStr(e.NewValues["ACC4"]).Trim();
        ACCOUNT_CODE += StringUtil.CStr(e.NewValues["ACC5"]).Trim();
        ACCOUNT_CODE += StringUtil.CStr(e.NewValues["ACC6"]).Trim();

        //ACCOUNT CODE LENGTH 2 3 4 6 4 4 Totoal 23 bytes
        drPaymentMethodSet.PAYMENT_METHOD_ID = GuidNo.getUUID();  //PAYMENT_METHOD_ID 
        drPaymentMethodSet.PAY_MODE_ID = StringUtil.CStr(e.NewValues["PAY_MODE_ID"]);   //PAY_MODE_ID
        drPaymentMethodSet.ACCOUNT_CODE = ACCOUNT_CODE;  //ACCOUNT_CODE
        drPaymentMethodSet.S_DATE = DateTime.Parse(StringUtil.CStr(e.NewValues["S_DATE"]));  //S_DATE 
        if (e.NewValues["E_DATE"] != null)
        {
            drPaymentMethodSet.E_DATE = DateTime.Parse(StringUtil.CStr(e.NewValues["E_DATE"]));  //E_DATE 
        }
        drPaymentMethodSet.CREATE_USER = logMsg.OPERATOR; //CREATE_USER 
        drPaymentMethodSet.CREATE_DTM = DateTime.Now.Date; //CREATE_DTM  
        drPaymentMethodSet.MODI_USER = logMsg.OPERATOR;  //MODI_USER 
        drPaymentMethodSet.MODI_DTM = DateTime.Now;  //MODI_DTM 

        dtPaymentMethodSet.Rows.Add(drPaymentMethodSet);

        PaymentMethodSet_DTO.AcceptChanges();

        //Insert database
        OPT01_Facade.AddNewOne_PaymentMethodSet(PaymentMethodSet_DTO);

        grid.CancelEdit();
        e.Cancel = true;
        bindMasterData();
    }

    protected void gvMaster_RowValidating(object sender, DevExpress.Web.Data.ASPxDataValidationEventArgs e)
    {
        string editStatus = "";

        if (e.IsNewRow)
        {

            int iCount = new OPT01_Facade().Query_PAY_MODE_Count(StringUtil.CStr(e.NewValues["PAY_MODE_ID"]), StringUtil.CStr(e.NewValues["S_DATE"]).Trim(), (e.NewValues["E_DATE"] == null) ? "" : StringUtil.CStr(e.NewValues["E_DATE"]).Trim(), "");
            if (iCount > 0)
            {
                e.RowError = "同一付款方式在同一時間區間不可有二筆以上的設定!!";
                return;
            }

        }
        else
        {
            int iCount = new OPT01_Facade().Query_PAY_MODE_Count(StringUtil.CStr(e.NewValues["PAY_MODE_ID"]), StringUtil.CStr(e.NewValues["S_DATE"]).Trim(), (e.NewValues["E_DATE"] == null) ? "" : StringUtil.CStr(e.NewValues["E_DATE"]).Trim(), StringUtil.CStr(e.Keys["PAYMENT_METHOD_ID"]).Trim());
            if (iCount > 0)
            {
                e.RowError = "同一付款方式在同一時間區間不可有二筆以上的設定!!";
                return;
            }
        }

        //if (e.NewValues["S_DATE"] == null || StringUtil.CStr(e.NewValues["S_DATE"]) == "" || e.NewValues["E_DATE"] == null || StringUtil.CStr(e.NewValues["E_DATE"]) == "")
        //{
        //    e.RowError = "請輸入起訖日期";
        //    return;
        //}
        if (e.NewValues["STATUS"] != null)
            editStatus = StringUtil.CStr(e.NewValues["STATUS"]);

        DateTime SDATE = DateTime.Parse(StringUtil.CStr(e.NewValues["S_DATE"]));
        DateTime EDATE = DateTime.Parse((e.NewValues["E_DATE"] == null ? "9999/12/31" : StringUtil.CStr(e.NewValues["E_DATE"])));
        DateTime TToday = DateTime.Parse(DateTime.Now.ToString("yyyy/MM/dd"));
        if (StringUtil.CStr(EDATE.Date) != "9999/12/31")
        {
            if (EDATE <= SDATE)
            {
                e.RowError = "結束日期不可小於等於開始日期!!\n";
                return;
            }
        }
        if (editStatus.Trim() != "有效")
        {
            if (SDATE < DateTime.Now)
            {
                e.RowError = "開始日期必須大於今天日期!!\n";
                return;
            }
        }
        if (EDATE < TToday)
        {
            e.RowError = "結束日期必須大於系統日期!!\n";
            return;
        }
       
        if (EDATE < DateTime.Now)
        {
            e.RowError = "結束日期必須大於等於今天日期!!\n";
            return;
        }
       
    }

    protected void gvMaster_CellEditorInitialize(object sender, ASPxGridViewEditorEventArgs e)
    {
        GridViewDataDateColumn colS_DATE = (GridViewDataDateColumn)gvMaster.Columns["S_DATE"];
        GridViewDataDateColumn colE_DATE = (GridViewDataDateColumn)gvMaster.Columns["E_DATE"];
        colS_DATE.PropertiesDateEdit.MinDate = DateTime.Now.Date.AddDays(1);
        colE_DATE.PropertiesDateEdit.MinDate = DateTime.Now.Date.AddDays(1);

        if (gvMaster.IsEditing && e.Column.FieldName == "PAY_MODE_ID")
        {
            ASPxComboBox combo = e.Editor as ASPxComboBox;
            combo.DataSource = OPT01_PageHelper.GetPaymentModeName(false);
            combo.ValueField = "PayModeId";
            combo.TextField = "PayModeName";
            combo.DataBind();
        }
    }

    #endregion
}
