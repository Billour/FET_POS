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

public partial class VSS_INV_INV23 : BasePage
{
    //private INV23_Facade _INV23_Facade;
    //private INV23_LOCSet_DTO _INV23_LOCSet_DTO;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack && !Page.IsCallback)
        {
            bindMasterData();
        }
    }

    private void bindMasterData()
    {
        gvMaster.CancelEdit();
        DataTable dtQuery = new DataTable();
        dtQuery = GetGvMasterData();

        gvMaster.DataSource = dtQuery;
        gvMaster.DataBind();

        gvMaster.Selection.UnselectAll();
        ViewState["gvMaster"] = dtQuery;
    }

    private DataTable GetGvMasterData()
    {
        DataTable dt = new DataTable();

        dt = INV23_PageHelper.GetLocMethodData();
        dt.PrimaryKey = new DataColumn[] { dt.Columns["LOC_ID"]};

        return dt;
    }

    #region Button 觸發事件

    protected void btnNew_Click(object sender, EventArgs e)
    {
        gvMaster.AddNewRow();
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        DataTable dt = new DataTable();
        dt = GetGvMasterData();

        DataTable dt1 = new DataTable();
        dt1 = INV23_PageHelper.GetCheckImeiTypeMethodData();

        INV23_Facade _INV23_Facade = new INV23_Facade();
        INV23_LOCSet_DTO _INV23_LOCSet_DTO = new INV23_LOCSet_DTO();

        INV23_LOCSet_DTO.LOCDataTable dtLOCSet;
        INV23_LOCSet_DTO.LOCRow drLOCSet;

        dtLOCSet = (INV23_LOCSet_DTO.LOCDataTable)_INV23_LOCSet_DTO.Tables["LOC"];

        for (int i = 0; i < dt.Rows.Count; i++)
        {
            drLOCSet = dtLOCSet.NewLOCRow();
            drLOCSet.LOC_ID = StringUtil.CStr(gvMaster.GetRowValues(i, gvMaster.KeyFieldName));
            drLOCSet.COMPANY_CODE = "01";
            drLOCSet.STOCK_NAME = StringUtil.CStr(gvMaster.GetRowValues(i, "STOCK_NAME"));

            ASPxCheckBox cb1 = (ASPxCheckBox)gvMaster.FindRowCellTemplateControl(i, (GridViewDataColumn)gvMaster.Columns[3], "chkSFLAG");
            drLOCSet.SALES_FLAG = (cb1.Checked ? "1" : "0");

            ASPxComboBox cbb1 = (ASPxComboBox)gvMaster.FindRowCellTemplateControl(i, (GridViewDataColumn)gvMaster.Columns[4], "cbbCITN");
            drLOCSet.CHECK_IMEI_TYPE = StringUtil.CStr(cbb1.Value);


            drLOCSet.CREATE_USER = StringUtil.CStr(dt.Rows[i]["CREATE_USER"]);
            drLOCSet.CREATE_DTM = DateTime.Parse(StringUtil.CStr(dt.Rows[i]["CREATE_DTM"]));
            drLOCSet.MODI_USER = logMsg.OPERATOR;
            drLOCSet.MODI_DTM = DateTime.Now;

            dtLOCSet.Rows.Add(drLOCSet);
            dtLOCSet.AcceptChanges();

            //update database
            _INV23_Facade.UpdateLocMethodData(_INV23_LOCSet_DTO);
        }

        gvMaster.DataSource = GetGvMasterData();
        gvMaster.DataBind();
    }

    #endregion

    #region gvMaster 觸發事件

    protected void gvMaster_HtmlRowCreated(object sender, ASPxGridViewTableRowEventArgs e)
    {
        if (e.RowType == GridViewRowType.Data)
        {
            ASPxCheckBox chkSFLAG = (ASPxCheckBox)gvMaster.FindRowCellTemplateControl(e.VisibleIndex, (GridViewDataColumn)gvMaster.Columns[3], "chkSFLAG");
            chkSFLAG.Checked = bool.Parse(StringUtil.CStr(gvMaster.GetRowValues(e.VisibleIndex, "SALES_FLAG")));
        }
    }

    protected void gvMaster_CellEditorInitialize(object sender, ASPxGridViewEditorEventArgs e)
    {
        if (e.Column.FieldName != "CHECK_IMEI_TYPE_NAME") return;
        ASPxComboBox combo = e.Editor as ASPxComboBox;

        combo.DataSource = INV23_PageHelper.GetCheckImeiTypeMethodData();
        combo.ValueField = "CHECK_IMEI_TYPE";
        combo.TextField = "CHECK_IMEI_TYPE_NAME";
        combo.ReadOnly = false;
        combo.DataBind();

        if (gvMaster.IsNewRowEditing)
        {
            combo.SelectedIndex = 0;
        }
        else if (gvMaster.IsEditing)
        {
            combo.Text = StringUtil.CStr(e.Value);
        }
    }

    protected void gvMaster_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
    {
        ASPxGridView grid = (ASPxGridView)sender;

        INV23_Facade _INV23_Facade = new INV23_Facade();
        INV23_LOCSet_DTO _INV23_LOCSet_DTO = new INV23_LOCSet_DTO();

        INV23_LOCSet_DTO.LOCDataTable dtLOCSet;
        INV23_LOCSet_DTO.LOCRow drLOCSet;

        dtLOCSet = (INV23_LOCSet_DTO.LOCDataTable)_INV23_LOCSet_DTO.Tables["LOC"];
        drLOCSet = dtLOCSet.NewLOCRow();

        drLOCSet.LOC_ID = GuidNo.getUUID();
        drLOCSet.COMPANY_CODE = "01";  //SALES_FLAG default = "01"
        drLOCSet.STOCK_NAME = StringUtil.CStr(e.NewValues["STOCK_NAME"]); //STOCK_NAME
        drLOCSet.SALES_FLAG = ((e.NewValues["SALES_FLAG"] == null) ? "0" : "1");  //SALES_FLAG
        drLOCSet.CHECK_IMEI_TYPE = StringUtil.CStr(e.NewValues["CHECK_IMEI_TYPE_NAME"]);
        drLOCSet.CREATE_USER = logMsg.OPERATOR; //CREATE_USER 
        drLOCSet.CREATE_DTM = DateTime.Now; //CREATE_DTM  
        drLOCSet.MODI_USER = logMsg.OPERATOR;  //MODI_USER 
        drLOCSet.MODI_DTM = DateTime.Now;  //MODI_DTM 

        dtLOCSet.Rows.Add(drLOCSet);

        _INV23_LOCSet_DTO.AcceptChanges();

        //Insert database
        _INV23_Facade.InsertLocMethodData(_INV23_LOCSet_DTO);

        grid.CancelEdit();
        e.Cancel = true;
        grid.DataSource = GetGvMasterData();
        grid.DataBind();
    }

    protected void gvMaster_RowValidating(object sender, DevExpress.Web.Data.ASPxDataValidationEventArgs e)
    {
        ASPxGridView gv = (ASPxGridView)sender;
        string newSTOCK_NAME;
        string oldSTOCK_NAME;
        if (e.NewValues["STOCK_NAME"] != null)
        {
            newSTOCK_NAME = StringUtil.CStr(e.NewValues["STOCK_NAME"]).Trim();
            oldSTOCK_NAME = (e.OldValues["STOCK_NAME"] == null) ? "" : StringUtil.CStr(e.OldValues["STOCK_NAME"]).Trim();
            int bytes = System.Text.Encoding.UTF8.GetBytes(newSTOCK_NAME).Length;
            if (bytes > 50)
            {
                e.RowError = "倉別名稱太長，請重新輸入";
                return;
            }
            else if (newSTOCK_NAME != "")
            {
                DataTable dt = ViewState["gvMaster"] as DataTable;

                int iCount = new INV23_Facade().QueryLocNameCount(newSTOCK_NAME);

                if (gv.IsNewRowEditing && iCount > 0)
                {
                    e.RowError = "倉別名稱已存在，請重新輸入";
                    return;
                }
                else if (gv.IsEditing)
                {
                    if (newSTOCK_NAME.CompareTo(oldSTOCK_NAME) != 0 && iCount > 0)
                    {
                        e.RowError = "倉別名稱已存在，請重新輸入";
                        return;
                    }
                }
            }
            else
            {
                e.RowError = "倉別名稱不為空白，請重新輸入";
                return;
            }
        }
        else
        {
            e.RowError = "倉別名稱為null";
            return;
        }
    }

    protected void gvMaster_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
    {
        DataTable dt = new DataTable();
        dt = GetGvMasterData();

        DataTable dt1 = new DataTable();
        dt1 = INV23_PageHelper.GetCheckImeiTypeMethodData();

        INV23_Facade _INV23_Facade = new INV23_Facade();
        INV23_LOCSet_DTO _INV23_LOCSet_DTO = new INV23_LOCSet_DTO();

        INV23_LOCSet_DTO.LOCDataTable dtLOCSet;
        INV23_LOCSet_DTO.LOCRow drLOCSet;

        dtLOCSet = (INV23_LOCSet_DTO.LOCDataTable)_INV23_LOCSet_DTO.Tables["LOC"];
        DataRow dr = dt.Rows.Find(e.Keys["LOC_ID"]);
        drLOCSet = dtLOCSet.NewLOCRow();
        drLOCSet.LOC_ID = StringUtil.CStr(dr["LOC_ID"]);
        drLOCSet.COMPANY_CODE = StringUtil.CStr(dr["COMPANY_CODE"]);
        drLOCSet.STOCK_NAME = StringUtil.CStr(e.NewValues["STOCK_NAME"]).Trim();
        drLOCSet.SALES_FLAG = (bool.Parse(StringUtil.CStr(e.NewValues["SALES_FLAG"])) ? "1" : "0");
        drLOCSet.CHECK_IMEI_TYPE = StringUtil.CStr(e.NewValues["CHECK_IMEI_TYPE_NAME"]);
        drLOCSet.CREATE_USER = StringUtil.CStr(dr["CREATE_USER"]); 
        drLOCSet.CREATE_DTM = (DateTime)dr["CREATE_DTM"];
        drLOCSet.MODI_USER = logMsg.OPERATOR;
        drLOCSet.MODI_DTM = DateTime.Now;

        dtLOCSet.Rows.Add(drLOCSet);
        dtLOCSet.AcceptChanges();

        //update database
        _INV23_Facade.UpdateLocMethodData(_INV23_LOCSet_DTO);

        gvMaster.CancelEdit();
        e.Cancel = true;
        gvMaster.DataSource = GetGvMasterData();
        gvMaster.DataBind();
    }

    protected void gvMaster_PageIndexChanged(object sender, EventArgs e)
    {
        bindMasterData();
    }

    #endregion

}