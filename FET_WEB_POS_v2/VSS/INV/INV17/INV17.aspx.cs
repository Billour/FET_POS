using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Advtek.Utility;
using DevExpress.Web;
using DevExpress.Web.ASPxEditors;
using DevExpress.Web.ASPxGridView;
using DevExpress.Web.ASPxGridView.Rendering;
using DevExpress.Web.ASPxPager;
using FET.POS.Model.Facade;
using FET.POS.Model.Facade.FacadeImpl;
using FET.POS.Model.DTO;
using FET.POS.Model.Common;
using System.Runtime.Serialization;

public partial class VSS_INV_INV17 : BasePage
{
    #region Class Varibles

    string S_CUT_YYMM
    {
        set
        {
            ViewState["S_CUT_YYMM"] = value;
        }
        get
        {
            if (ViewState["S_CUT_YYMM"] == null)
                return string.Empty;

            return (string)ViewState["S_CUT_YYMM"];
        }
    }

    string E_CUT_YYMM
    {
        set
        {
            ViewState["E_CUT_YYMM"] = value;
        }
        get
        {
            if (ViewState["E_CUT_YYMM"] == null)
                return string.Empty;

            return (string)ViewState["E_CUT_YYMM"];
        }
    }

    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack && !IsCallback)
        {
            if (logMsg.STORENO != StringUtil.CStr(System.Configuration.ConfigurationManager.AppSettings["DefaultRoleHQ"]))
            {
                //彈跳視窗
                ScriptManager.RegisterClientScriptBlock(this, GetType(), "AlertMessage", "alert('非總部人員無法使用此功能!!');", true);
                gvMaster.FindChildControl<ASPxButton>("btnNew").ClientEnabled = false;
                gvMaster.FindChildControl<ASPxButton>("btnDelete").ClientEnabled = false;
                this.btnSearch.ClientEnabled = false;
                this.btnClear.ClientEnabled = false;
                this.txtSDate.ClientEnabled = false;
                txtSDate.ControlStyle.BackColor = System.Drawing.Color.LightGray;
                this.txtEDate.ClientEnabled = false;
                txtEDate.ControlStyle.BackColor = System.Drawing.Color.LightGray;
                return;
            }
            else
            {
                GetEmptyGvMaster();
            }
        }
    }

    private void GetEmptyGvMaster()
    {
        DataTable dt = new DataTable();
        dt = INV17_PageHelper.GetEmptyCutOffDateMethodData();

        gvMaster.DataSource = dt;
        gvMaster.DataBind();
    }

    private DataTable GetGvMasterData()
    {
        DataTable dt = new DataTable();
        dt = INV17_PageHelper.GetCutOffDateMethodData(S_CUT_YYMM, E_CUT_YYMM);
        return dt;
    }

    #region Button 觸發事件

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        gvMaster.Selection.UnselectAll();
        DataTable dtQuery = new DataTable();

        S_CUT_YYMM = this.txtSDate.Text.Trim();
        E_CUT_YYMM = this.txtEDate.Text.Trim();

        if (!string.IsNullOrEmpty(S_CUT_YYMM))
        {
            S_CUT_YYMM += "/01";
        }

        if (!string.IsNullOrEmpty(E_CUT_YYMM))
        {
            DateTime endDate;
            endDate = DateTime.ParseExact(this.txtEDate.Text.Trim(), "yyyy/MM", null);
            E_CUT_YYMM += "/" + DateTime.DaysInMonth(endDate.Year, endDate.Month).ToString("00");
        }

        dtQuery = GetGvMasterData();

        gvMaster.DataSource = dtQuery;
        gvMaster.DataBind();
    }

    protected void btnNew_Click(object sender, EventArgs e)
    {
        gvMaster.AddNewRow();
    }

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        List<object> gvPKValues = gvMaster.GetSelectedFieldValues(gvMaster.KeyFieldName);
        string pkFName = gvMaster.KeyFieldName;

        INV17_CUTOFFDATESet_DTO _CUTOFFDATESet_DTO = new INV17_CUTOFFDATESet_DTO();

        DataTable CustomDataTable = new DataTable();
        CustomDataTable.TableName = StringUtil.CStr(_CUTOFFDATESet_DTO.CUT_OFF_DATE.TableName);
        CustomDataTable.Columns.Add(pkFName, typeof(string));

        DataTable dtOld = INV17_PageHelper.GetCutOffDateMethodData(S_CUT_YYMM, E_CUT_YYMM);

        for (int i = 0; i < gvPKValues.Count; i++)
        {
            if (dtOld.AsEnumerable().Any(dr => dr["CUT_OFF_DATE_ID"].Equals(StringUtil.CStr(gvPKValues[i]))))
            {
                DataRow CustomDataRow = CustomDataTable.NewRow();
                CustomDataRow[pkFName] = StringUtil.CStr(gvPKValues[i]);
                CustomDataTable.Rows.Add(CustomDataRow);
            }
        }

        //Delete database
        INV17_PageHelper.DeleteCutOffDateMethodData(CustomDataTable, pkFName);

        gvMaster.DataSource = GetGvMasterData();
        gvMaster.DataBind();

    }

    #endregion

    #region gvMaster 觸發事件

    protected void gvMaster_PageIndexChanged(object sender, EventArgs e)
    {
        ASPxGridView gvMaster = sender as ASPxGridView;
        gvMaster.Selection.UnselectAll();
        gvMaster.CancelEdit();

        gvMaster.DataSource = GetGvMasterData();
        gvMaster.DataBind();
    }

    protected void gvMaster_CommandButtonInitialize(object sender, ASPxGridViewCommandButtonEventArgs e)
    {
        if (e.VisibleIndex > -1)
        {
            if (e.ButtonType == ColumnCommandButtonType.Edit || e.ButtonType == ColumnCommandButtonType.SelectCheckbox)
            {
                string date = StringUtil.CStr(gvMaster.GetRowValues(e.VisibleIndex, "CUT_OFF_DATE"));
                if (Convert.ToDateTime(StringUtil.CStr(date)) < DateTime.Now)
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
            string date = StringUtil.CStr(gvMaster.GetRowValues(e.VisibleIndex, "CUT_OFF_DATE"));
            if (Convert.ToDateTime(StringUtil.CStr(date)) < DateTime.Now)
            {
                e.Row.Attributes["canSelect"] = "false";
            }
        }

    }

    protected void gvMaster_OnHtmlRowCreated(object sender, ASPxGridViewTableRowEventArgs e)
    {
        if (e.RowType != DevExpress.Web.ASPxGridView.GridViewRowType.Data) return;

        int iCount = e.Row.Cells.Count;
        object obj = e.Row.Cells[0].Controls[0];
        if (obj != null)
        {
            CheckBox chk = obj as CheckBox;
            chk.Checked = false;
        }
    }

    protected void gvMaster_StartRowEditing(object sender, DevExpress.Web.Data.ASPxStartRowEditingEventArgs e)
    {
        gvMaster.Selection.UnselectAll();
    }

    protected void gvMaster_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
    {
        ASPxGridView grid = (ASPxGridView)sender;

        INV17_Facade _INV17_Facade = new INV17_Facade();
        INV17_CUTOFFDATESet_DTO _CUTOFFDATESet_DTO = new INV17_CUTOFFDATESet_DTO();

        INV17_CUTOFFDATESet_DTO.CUT_OFF_DATEDataTable dtCUTOFFDATESet;
        INV17_CUTOFFDATESet_DTO.CUT_OFF_DATERow drCUTOFFDATESet;

        dtCUTOFFDATESet = (INV17_CUTOFFDATESet_DTO.CUT_OFF_DATEDataTable)_CUTOFFDATESet_DTO.Tables["CUT_OFF_DATE"];
        drCUTOFFDATESet = dtCUTOFFDATESet.NewCUT_OFF_DATERow();

        drCUTOFFDATESet.CUT_OFF_DATE_ID = GuidNo.getUUID();
        drCUTOFFDATESet.CUT_YYMM = DateTime.Parse(StringUtil.CStr(e.NewValues["CUT_YYMM"])).ToString("yyyy/MM");  //CUT_YYMM
        drCUTOFFDATESet.CUT_YEAR = StringUtil.CStr(DateTime.Parse(StringUtil.CStr(e.NewValues["CUT_OFF_DATE"])).Year);//CUT_YEAR
        drCUTOFFDATESet.CUT_OFF_MM = StringUtil.CStr(DateTime.Parse(StringUtil.CStr(e.NewValues["CUT_OFF_DATE"])).Month);//CUT_OFF_MM
        drCUTOFFDATESet.CUT_OFF_DATE = DateTime.Parse(StringUtil.CStr(e.NewValues["CUT_OFF_DATE"]));  //CUT_OFF_DATE
        drCUTOFFDATESet.CREATE_USER = logMsg.OPERATOR; //CREATE_USER 
        drCUTOFFDATESet.CREATE_DTM = DateTime.Now; //CREATE_DTM  
        drCUTOFFDATESet.MODI_USER = logMsg.OPERATOR;  //MODI_USER 
        drCUTOFFDATESet.MODI_DTM = DateTime.Now;  //MODI_DTM 

        dtCUTOFFDATESet.Rows.Add(drCUTOFFDATESet);

        _CUTOFFDATESet_DTO.AcceptChanges();

        //Insert database
        _INV17_Facade.InsertCutOffDateMethodData(_CUTOFFDATESet_DTO);


        grid.CancelEdit();
        e.Cancel = true;
        grid.DataSource = GetGvMasterData();
        grid.DataBind();
    }

    protected void gvMaster_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
    {
        ASPxGridView grid = (ASPxGridView)sender;

        INV17_Facade _INV17_Facade = new INV17_Facade();
        INV17_CUTOFFDATESet_DTO _CUTOFFDATESet_DTO = new INV17_CUTOFFDATESet_DTO();

        INV17_CUTOFFDATESet_DTO.CUT_OFF_DATEDataTable dtCUTOFFDATESet;
        INV17_CUTOFFDATESet_DTO.CUT_OFF_DATERow drCUTOFFDATESet;

        dtCUTOFFDATESet = (INV17_CUTOFFDATESet_DTO.CUT_OFF_DATEDataTable)_CUTOFFDATESet_DTO.Tables["CUT_OFF_DATE"];
        drCUTOFFDATESet = dtCUTOFFDATESet.NewCUT_OFF_DATERow();

        //get edit row by key to datatable
        DataTable dtOld = INV17_PageHelper.GetCutOffDateMethodDataByKey(StringUtil.CStr(e.Keys["CUT_OFF_DATE_ID"]));

        drCUTOFFDATESet.CUT_OFF_DATE_ID = StringUtil.CStr(e.Keys["CUT_OFF_DATE_ID"]);
        drCUTOFFDATESet.CUT_YYMM = StringUtil.CStr(e.NewValues["CUT_YYMM"]);  //CUT_YYMM
        drCUTOFFDATESet.CUT_YEAR = StringUtil.CStr(DateTime.Parse(StringUtil.CStr(e.NewValues["CUT_OFF_DATE"])).Year);//CUT_YEAR
        drCUTOFFDATESet.CUT_OFF_MM = StringUtil.CStr(DateTime.Parse(StringUtil.CStr(e.NewValues["CUT_OFF_DATE"])).Month);//CUT_OFF_MM
        drCUTOFFDATESet.CUT_OFF_DATE = DateTime.Parse(StringUtil.CStr(e.NewValues["CUT_OFF_DATE"]));  //CUT_OFF_DATE
        drCUTOFFDATESet.CREATE_USER = StringUtil.CStr(dtOld.Rows[0]["CREATE_USER"]); //CREATE_USER 
        drCUTOFFDATESet.CREATE_DTM = DateTime.Parse(StringUtil.CStr(dtOld.Rows[0]["CREATE_DTM"])); //CREATE_DTM  
        drCUTOFFDATESet.MODI_USER = logMsg.OPERATOR;  //MODI_USER 
        drCUTOFFDATESet.MODI_DTM = DateTime.Now;  //MODI_DTM 

        dtCUTOFFDATESet.Rows.Add(drCUTOFFDATESet);

        _CUTOFFDATESet_DTO.AcceptChanges();

        //update database
        _INV17_Facade.UpdateCutOffDateMethodData(_CUTOFFDATESet_DTO);

        grid.CancelEdit();
        e.Cancel = true;
        grid.DataSource = GetGvMasterData();
        grid.DataBind();
    }

    protected void gvMaster_InitNewRow(object sender, DevExpress.Web.Data.ASPxDataInitNewRowEventArgs e)
    {
        gvMaster.Selection.UnselectAll();
    }

    protected void gvMaster_RowValidating(object sender, DevExpress.Web.Data.ASPxDataValidationEventArgs e)
    {
        ASPxGridView gv = sender as ASPxGridView;

        GridViewDataTextColumn col_YYMM = (GridViewDataTextColumn)gv.Columns["CUT_YYMM"];
        DataTable dtVaild = INV17_PageHelper.GetCutOffDateMethodData(S_CUT_YYMM, E_CUT_YYMM);
        string oldValue = (e.OldValues["CUT_YYMM"] == null ? "" : StringUtil.CStr(e.OldValues["CUT_YYMM"]));

        if (e.NewValues["CUT_YYMM"] == null || e.NewValues["CUT_OFF_DATE"] == null)
        {
            e.RowError += "【欄位名稱】不允許空值，請重新輸入!!";
            ASPxDateEdit d1 = gv.FindChildControl<ASPxDateEdit>("CUT_YYMM");
            d1 = (ASPxDateEdit)gv.FindEditRowCellTemplateControl(col_YYMM, "CUT_YYMM");
            d1 = (ASPxDateEdit)gv.FindEmptyDataRowTemplateControl("CUT_YYMM");
            d1 = (ASPxDateEdit)gv.FindRowTemplateControl(3, "CUT_YYMM");
            object o = e.GetType();
            return;
        }

        if (StringUtil.CStr(e.NewValues["CUT_YYMM"]) != oldValue)
        {
            if (dtVaild.AsEnumerable().Any(dr => dr["CUT_YYMM"].Equals(e.NewValues["CUT_YYMM"])))
            {
                e.RowError += "關帳年月巳存在!!";
                return;
            }
        }

        string strCYM = StringUtil.CStr(e.NewValues["CUT_YYMM"]);
        string strCOD = StringUtil.CStr(e.NewValues["CUT_OFF_DATE"]);

        DateTime CYM = DateTime.Parse(strCYM);
        DateTime COD = DateTime.Parse(strCOD);

        if (CYM.ToString("yyyyMM").CompareTo(COD.ToString("yyyyMM")) != 0)
        {
             e.RowError += "【關帳日】未介於關帳年月區間，請重新輸入!!";
             return;
        }
        if (CYM.ToString("yyyyMM").CompareTo(DateTime.Now.ToString("yyyyMM")) < 0)
        {
            e.RowError += "【關帳年月】不允許小於系統日，請重新輸入!!";
            return;
        }
        if (COD.ToString("yyyyMMdd").CompareTo(DateTime.Now.ToString("yyyyMMdd")) < 0)
        {
            e.RowError += "【關帳日】不允許小於系統日，請重新輸入!!";
            return;
        }

    }

    #endregion
}
