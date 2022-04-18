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

public partial class VSS_LOG_LOG04 : BasePage
{
    private string ed_status = "Select"; 

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            bindDdlValTxt(DropDownList2, LOG04_PageHelper.GetSysParaTypeId(true, ed_status), "SYS_PARA_TYPE_ID", "SYS_PARA_TYPE_NAME");

            aspComboBoxDefaultSetting();

            gvMaster.DataSource = new LOG04_SysPara_DTO().SYS_PARA;
            gvMaster.DataBind();
        }
    }

    private void bindMasterData()
    {
        gvMaster.DataSource = getMasterData();
        gvMaster.DataBind();
    }

    private void bindMasterDataAfterWrite()
    {
        DataTable dtGvMaster = new DataTable();
        dtGvMaster = getMasterData();
        //ViewState["gvMaster"] = dtGvMaster;
        gvMaster.DataSource = dtGvMaster;
        gvMaster.DataBind();
    }

    private void bindDdlValTxt(ASPxComboBox AspCB, object dataSrc, string valCol, string txtCol)
    {
        AspCB.DataSource = dataSrc;
        AspCB.ValueField = valCol;
        AspCB.TextField = txtCol;
        AspCB.DataBind();
    }

    private void aspComboBoxDefaultSetting()
    {
        //若都沒選,將Index設為0
        //判斷下拉式選單預設值-999,並替換為空字串""
        if (DropDownList2.SelectedIndex == -1)
        {
            DropDownList2.SelectedIndex = 0;
            DropDownList2.SelectedItem.Value = "";
            DropDownList2.SelectedIndex = 0;
        }

    }

    private DataTable getMasterData()
    {
        DataTable dtMaster = new DataTable();


        LOG04_Facade _LOG04_Facade = new LOG04_Facade();

        dtMaster = _LOG04_Facade.Query_SysPara(TextBox1.Text,
                                           TextBox2.Text,
                                           StringUtil.CStr(DropDownList2.SelectedItem.Value));

        return dtMaster;
    }

    private bool DataCheck(DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
    {
        string msgStr = "";
        bool UpdateData = false; 


        if (e.NewValues["SYS_PARA_TYPE_NAME"] == null || StringUtil.CStr(e.NewValues["SYS_PARA_TYPE_NAME"]).Trim() == "")
        { msgStr = "參數分類不能為空!"; }
        else if (e.NewValues["PARA_KEY"] == null || StringUtil.CStr(e.NewValues["PARA_KEY"]).Trim() == "")
        { msgStr = "參數代碼不能為空!"; }
        else if (e.NewValues["PARA_NAME"] == null || StringUtil.CStr(e.NewValues["PARA_NAME"]).Trim() == "")
        { msgStr = "參數名稱不能為空!"; }
        else if (e.NewValues["PARA_VALUE"] == null || StringUtil.CStr(e.NewValues["PARA_VALUE"]).Trim() == "")
        { msgStr = "值不能為空!"; }
        else UpdateData = true;

        if (!UpdateData) { ScriptManager.RegisterClientScriptBlock(this, typeof(string), "RowUpdating", "alert('" + msgStr + "!');", true); }
        return UpdateData;
    }

    #region Button 觸發的事件

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        gvMaster.AddNewRow();
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        bindMasterData();
        gvMaster.CancelEdit();
        gvMaster.PageIndex = 0;
    }

    #endregion

    #region gvMaster 觸發的事件

    protected void gvMaster_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
    {
        LOG04_SysPara_DTO _LOG04_SysPara_DTO = new LOG04_SysPara_DTO();
        LOG04_SysPara_DTO.SYS_PARADataTable dtSYS;
        LOG04_SysPara_DTO.SYS_PARARow drSYS;

        dtSYS = (LOG04_SysPara_DTO.SYS_PARADataTable)_LOG04_SysPara_DTO.Tables["SYS_PARA"];
        drSYS = dtSYS.NewSYS_PARARow();

        drSYS["PARA_ID"] = GuidNo.getUUID();
        drSYS["PARA_KEY"] = StringUtil.CStr(e.NewValues["PARA_KEY"]);
        drSYS["PARA_VALUE"] = StringUtil.CStr(e.NewValues["PARA_VALUE"]);
        drSYS["PARA_NAME"] = StringUtil.CStr(e.NewValues["PARA_NAME"]);
        drSYS["PARA_DESC"] = (e.NewValues["PARA_DESC"] == null ? "" : StringUtil.CStr(e.NewValues["PARA_DESC"]));
        drSYS["SYS_PARA_TYPE_ID"] = StringUtil.CStr(e.NewValues["SYS_PARA_TYPE_ID"]);

        drSYS["MODI_USER"] = logMsg.MODI_USER;// +"-" + new Employee_Facade().GetEmpName(logMsg.MODI_USER);
        drSYS["MODI_DTM"] = Convert.ToDateTime(System.DateTime.Now);

        drSYS["CREATE_USER"] = drSYS["MODI_USER"];
        drSYS["CREATE_DTM"] = drSYS["MODI_DTM"];

        dtSYS.AddSYS_PARARow(drSYS);
        _LOG04_SysPara_DTO.AcceptChanges();

        new LOG04_Facade().AddNewOne_SysPara(_LOG04_SysPara_DTO);

        ((ASPxGridView)sender).CancelEdit();
        e.Cancel = true;

        bindMasterDataAfterWrite();
    }

    protected void gvMaster_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
    {
        LOG04_SysPara_DTO _LOG04_SysPara_DTO = new LOG04_SysPara_DTO();
        LOG04_SysPara_DTO.SYS_PARADataTable dtSYS;
        LOG04_SysPara_DTO.SYS_PARARow drSYS;

        dtSYS = _LOG04_SysPara_DTO.Tables["SYS_PARA"] as LOG04_SysPara_DTO.SYS_PARADataTable;

        dtSYS.Columns["PARA_KEY"].AllowDBNull = true;
        dtSYS.Columns["SYS_PARA_TYPE_ID"].AllowDBNull = true;
        dtSYS.Columns["CREATE_USER"].AllowDBNull = true;
        dtSYS.Columns["CREATE_DTM"].AllowDBNull = true;

        drSYS = dtSYS.NewSYS_PARARow();


        drSYS["PARA_ID"] = StringUtil.CStr(e.Keys["PARA_ID"]);

        drSYS["PARA_VALUE"] = StringUtil.CStr(e.NewValues["PARA_VALUE"]);
        drSYS["PARA_NAME"] = StringUtil.CStr(e.NewValues["PARA_NAME"]);
        drSYS["PARA_DESC"] = (e.NewValues["PARA_DESC"] == null ? "" : StringUtil.CStr(e.NewValues["PARA_DESC"]));

        drSYS["MODI_USER"] = logMsg.MODI_USER;// +"-" + new Employee_Facade().GetEmpName(logMsg.MODI_USER);
        drSYS["MODI_DTM"] = Convert.ToDateTime(System.DateTime.Now);

        dtSYS.AddSYS_PARARow(drSYS);
        _LOG04_SysPara_DTO.AcceptChanges();

        //更新資料庫
        new LOG04_Facade().UpdateOne_SysPara(_LOG04_SysPara_DTO);

        ((ASPxGridView)sender).CancelEdit();
        e.Cancel = true;

        bindMasterDataAfterWrite();
    }

    protected void gvMaster_CellEditorInitialize(object sender, ASPxGridViewEditorEventArgs e)
    {
        if (!gvMaster.IsNewRowEditing || e.Column.FieldName != "SYS_PARA_TYPE_ID") return;

        ed_status = "Edit";
        ASPxComboBox combo = e.Editor as ASPxComboBox;

        combo.DataSource = LOG04_PageHelper.GetSysParaTypeId(true, ed_status);
        combo.ValueField = "SYS_PARA_TYPE_ID";
        combo.TextField = "SYS_PARA_TYPE_NAME";
        combo.DataBind();

        combo.SelectedIndex = 0;
    }

    protected void gvMaster_StartRowEditing(object sender, DevExpress.Web.Data.ASPxStartRowEditingEventArgs e)
    {

        if (gvMaster.IsEditing)
        {
            foreach (GridViewColumn col in gvMaster.Columns)
            {
                if (col is GridViewDataColumn)
                {

                    GridViewDataColumn dataCol = (GridViewDataColumn)col;
                    Type typeCol = dataCol.GetType();

                    if (dataCol.FieldName == "SYS_PARA_TYPE_ID")
                    {
                        dataCol.ReadOnly = true;
                        dataCol.PropertiesEdit.Style.Border.BorderStyle = System.Web.UI.WebControls.BorderStyle.None;

                        //!!!Show名稱
                        dataCol.FieldName = "SYS_PARA_TYPE_NAME";
                    }

                    if (typeCol.Name == "GridViewDataComboBoxColumn")
                    {

                        GridViewDataComboBoxColumn cbCol = (GridViewDataComboBoxColumn)dataCol;
                        cbCol.PropertiesComboBox.ValidationSettings.RequiredField.IsRequired = false;
                        cbCol.PropertiesComboBox.DropDownButton.Enabled = false;
                        cbCol.PropertiesComboBox.DropDownButton.Visible = false;
                        cbCol.ReadOnly = true;
                    }

                    if (dataCol.FieldName == "PARA_KEY")
                    {
                        dataCol.ReadOnly = true;
                        dataCol.PropertiesEdit.Style.Border.BorderStyle = System.Web.UI.WebControls.BorderStyle.None;
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

                    if (dataCol.FieldName == "SYS_PARA_TYPE_ID")
                    {
                        dataCol.ReadOnly = false;
                        dataCol.PropertiesEdit.Style.Border.BorderStyle = System.Web.UI.WebControls.BorderStyle.Solid;
                    }

                    if (typeCol.Name == "GridViewDataComboBoxColumn")
                    {
                        GridViewDataComboBoxColumn cbCol = (GridViewDataComboBoxColumn)dataCol;
                        cbCol.PropertiesComboBox.ValidationSettings.RequiredField.IsRequired = true;
                        cbCol.PropertiesComboBox.ValidationSettings.RequiredField.ErrorText = "必填欄位";
                        cbCol.PropertiesComboBox.DropDownButton.Enabled = true;
                        cbCol.PropertiesComboBox.DropDownButton.Visible = true;
                    }
                }
            }
        }
    }

    protected void gvMaster_RowValidating(object sender, DevExpress.Web.Data.ASPxDataValidationEventArgs e)
    {
        if (e.IsNewRow)
        {
            LOG04_Facade _LOG04_Facade = new LOG04_Facade();

            DataTable dt = _LOG04_Facade.GetSYS_PARA(StringUtil.CStr(e.NewValues["PARA_KEY"]));

            if (dt.Rows.Count > 0)
            {
                e.RowError += "此參數代碼已存在，請重新輸入!";
                return;
            }
        }
    }

    #endregion

    protected void grid_PageIndexChanged(object sender, EventArgs e)
    {
        ASPxGridView grid = sender as ASPxGridView;
        grid.DataSource = getMasterData();
        grid.DataBind();
        grid.CancelEdit();
    }

}
