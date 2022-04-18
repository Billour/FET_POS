using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Resources;

using Advtek.Utility;
using DevExpress.Web;
using DevExpress.Web.ASPxGridView;
using DevExpress.Web.ASPxPager;
using DevExpress.Web.ASPxEditors;

using FET.POS.Model.DTO;
using FET.POS.Model.Common;
using FET.POS.Model.Facade.FacadeImpl;

public partial class VSS_ORD_ORD13_ORD13 : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack && !IsCallback)
        {
            //bindEmptyData();
            //**2011/03/31 Tina：一進入頁面後，列出所有的設定門市。
            //btnSearch_Click(this, null);
            bindMasterData();
        }
    }

    protected void bindEmptyData()
    {
        gvMasterDV.Selection.UnselectAll();
        gvMasterDV.CancelEdit();
        gvDetail.Selection.UnselectAll();
        gvMasterDV.DataSource = new ORD13_SCQC_DTO().SCQC_M;
        gvMasterDV.DataBind();
    }

    protected void bindMasterData()
    {
        gvMasterDV.Selection.UnselectAll();
        gvDetail.Selection.UnselectAll();
        gvMasterDV.CancelEdit();
        gvDetail.Selection.UnselectAll();

        DataTable dt = new ORD13_Facade().GetMasterData(txtSStoreNO.Text, txtEStoreNO.Text, txtStoreName.Text);
        gvMasterDV.DataSource = dt;
        gvMasterDV.DataBind();
        if (dt.Rows.Count == 0)
        {
            ASPxPageControl1.Visible = false;
        }
    }

    private void BindDetailData()
    {
        DataTable dt = new ORD13_Facade().GetDetailData(gvMasterDV.GetRowValues(gvMasterDV.FocusedRowIndex, "MASTER_ID"));
        gvDetail.DataSource = dt;
        gvDetail.DataBind();
    }

    private void SetDateEditProperties(ASPxGridViewEditorEventArgs e, string Status)
    {
        if (e.Editor is ASPxDateEdit)
        {
            ASPxDateEdit obj = e.Editor as ASPxDateEdit;
            string Date = "";
            if (e.Value != null)
            {
                Date = StringUtil.CStr(e.Value);
            }
            obj.Text = string.IsNullOrEmpty(Date) ? "" : Convert.ToDateTime(Date).ToString("yyyy/MM/dd");
            obj.EditFormatString = "yyyy/MM/dd";
            obj.EditFormat = EditFormat.Custom;

            if (Status == "有效" && e.Column.FieldName == "S_DATE")
            {
                obj.ClientEnabled = false;
            }
            else
            {
                obj.ClientEnabled = true;
                if (e.Column.FieldName == "S_DATE")
                {
                    obj.MinDate = DateTime.Now.Date.AddDays(1);
                }
                else
                {
                    //**2011/04/01 Tina：結束日期可以等於系統日。
                    obj.MinDate = DateTime.Now.Date.AddDays(0);
                }
            }

        }
    }

    #region Button 觸發事件

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        gvMasterDV.Selection.UnselectAll();
        gvDetail.Selection.UnselectAll();

        gvMasterDV.FocusedRowIndex = -1;
        gvMasterDV.PageIndex = 0;

        gvDetail.FocusedRowIndex = -1;
        gvDetail.PageIndex = 0;
    }

    protected void btnAddNew_Click(object sender, EventArgs e)
    {
        gvMasterDV.Selection.UnselectAll();
        gvDetail.Selection.UnselectAll();

        gvMasterDV.FocusedRowIndex = -1;
        gvMasterDV.PageIndex = 0;

        gvDetail.FocusedRowIndex = -1;
        gvDetail.PageIndex = 0;
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        bindMasterData();

        gvMasterDV.FocusedRowIndex = -1;
        gvMasterDV.PageIndex = 0;

        gvDetail.FocusedRowIndex = -1;
        gvDetail.PageIndex = 0;
    }
    protected void btnExport_Click(object sender, EventArgs e)
    {

        DataTable dtExport = new ORD13_Facade().QueryExportExcel(txtSStoreNO.Text, txtEStoreNO.Text, txtStoreName.Text);
        string filename = new Output().Print("XLS", "卡片安全庫存量暨最低庫存量設定<td></td><td>匯出時間：" + DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss") + "</td>", null, dtExport, null);
        Response.Redirect(Utils.CreateTamperProofDownloadXlsURL("SIM_GROUP.xls"));

    }

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        if (gvMasterDV.Selection.Count < 0) { return; }

        DataTable dt = new DataTable("SCQC_M");
        dt.Columns.Add("SCQC_M_ID");


        foreach (object key in gvMasterDV.GetSelectedFieldValues(gvMasterDV.KeyFieldName))
        {
            DataRow dr = dt.NewRow();
            dr[0] = key;
            dt.Rows.Add(dr);
        }

        dt.AcceptChanges();

        try
        {

            new ORD13_Facade().DeleteMaster(dt);
        
        }
        catch (Exception ex)
        {
            throw ex;
        }

        gvMasterDV.FocusedRowIndex = -1;
        gvMasterDV.PageIndex = 0;

        gvDetail.FocusedRowIndex = -1;
        gvDetail.PageIndex = 0;

        bindMasterData();
    }

    protected void btnDelete2_Click(object sender, EventArgs e)
    {
        if (gvDetail.Selection.Count < 0) { return; }

        DataTable dt = new DataTable("SCQC_D");
        dt.Columns.Add("SCQC_D_ID");

        foreach (object key in gvDetail.GetSelectedFieldValues(gvDetail.KeyFieldName))
        {
            DataRow dr = dt.NewRow();
            dr[0] = key;
            dt.Rows.Add(dr);
        }

        dt.AcceptChanges();

        try
        {
            new ORD13_Facade().DeleteDetail(dt);
        }
        catch (Exception ex)
        {
            throw ex;
        }

        BindDetailData();
    }

    #endregion

    #region gvMasterDV 觸發的事件

    protected void gvMasterDV_PageIndexChanged(object sender, EventArgs e)
    {
        bindMasterData();
        gvDetail.FocusedRowIndex = -1;
        gvDetail.PageIndex = 0;
    }

    protected void gvMasterDV_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
    {
        ORD13_SCQC_DTO ds = new ORD13_SCQC_DTO();

        ORD13_SCQC_DTO.SCQC_MDataTable dt = ds.SCQC_M;

        foreach (DataColumn col in dt.Columns)
        {
            col.AllowDBNull = true;
        }

        ORD13_SCQC_DTO.SCQC_MRow dr = dt.NewSCQC_MRow();

        dr.MODI_USER = logMsg.OPERATOR;
        dr.MODI_DTM = DateTime.Now;
        dr.SCQC_M_ID = StringUtil.CStr(e.Keys[0]);
        dr.STORE_NO = StringUtil.CStr(e.NewValues["STORE_NO"]);

        dt.AddSCQC_MRow(dr);
        ds.AcceptChanges();

        new ORD13_Facade().UpdateMaster(ds);

        ((ASPxGridView)sender).CancelEdit();
        e.Cancel = true;

        bindMasterData();

    }

    protected void gvMasterDV_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
    {
        ORD13_SCQC_DTO ds = new ORD13_SCQC_DTO();
        ORD13_SCQC_DTO.SCQC_MDataTable dt = ds.SCQC_M;

        ORD13_SCQC_DTO.SCQC_MRow MasterDr = dt.NewSCQC_MRow();

        MasterDr.MODI_USER = logMsg.CREATE_USER;
        MasterDr.MODI_DTM = DateTime.Now;

        MasterDr.SCQC_M_ID = GuidNo.getUUID();
        MasterDr.STORE_NO = StringUtil.CStr(e.NewValues["STORE_NO"]);

        MasterDr.CREATE_USER = MasterDr.MODI_USER;
        MasterDr.CREATE_DTM = MasterDr.MODI_DTM;

        dt.AddSCQC_MRow(MasterDr);
        ds.AcceptChanges();

        new ORD13_Facade().InsertMaster(ds);

        gvMasterDV.CancelEdit();
        e.Cancel = true;

        bindMasterData();

        gvMasterDV.FocusedRowIndex = -1;
        gvMasterDV.PageIndex = 0;

        gvDetail.FocusedRowIndex = -1;
        gvDetail.PageIndex = 0;
    }

    protected void gvMasterDV_FocusedRowChanged(object sender, EventArgs e)
    {
        if (!IsPostBack && !IsCallback)
        {
            gvMasterDV.FocusedRowIndex = -1;
        }
        else
        {
            if (!gvMasterDV.IsEditing && gvMasterDV.FocusedRowIndex > -1)
            {
                BindDetailData();
                gvDetail.Selection.UnselectAll();
                if (gvDetail.IsEditing) gvDetail.CancelEdit();
                ASPxPageControl1.Visible = true;
            }
            else
            {
                ASPxPageControl1.Visible = false;

            }
        }
    }

    protected void gvMasterDV_StartRowEditing(object sender, DevExpress.Web.Data.ASPxStartRowEditingEventArgs e)
    {
        gvMasterDV.Selection.UnselectAll();
        gvDetail.Selection.UnselectAll();
        gvMasterDV.FocusedRowIndex = -1;
        //gvMasterDV.PageIndex = 0;

        gvDetail.FocusedRowIndex = -1;
        gvDetail.PageIndex = 0;
    }

    protected void gvMasterDV_HtmlRowPrepared(object sender, ASPxGridViewTableRowEventArgs e)
    {
        if (e.RowType == GridViewRowType.Data)
        {
            List<object> keyValues = this.gvMasterDV.GetSelectedFieldValues(gvMasterDV.KeyFieldName);
            foreach (string key in keyValues)
            {
                if (key == StringUtil.CStr(e.GetValue(gvMasterDV.KeyFieldName)))
                {
                    if (key == StringUtil.CStr(gvMasterDV.GetRowValues(e.VisibleIndex, gvMasterDV.KeyFieldName)))
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

    protected void gvMasterDV_RowValidating(object sender, DevExpress.Web.Data.ASPxDataValidationEventArgs e)
    {
        if (string.IsNullOrEmpty(StringUtil.CStr(e.NewValues["STORE_NO"])))
        {
            e.RowError = "【門市編號】不允許空值，請重新輸入";
            return;
        }
        if (e.IsNewRow && gvMasterDV.IsEditing)
        {// insert check
            if (ORD13_PageHelper.CheckStore(StringUtil.CStr(e.NewValues["STORE_NO"])))
            {
                e.RowError = StringUtil.CStr(e.NewValues["STORE_NO"]) + "已存在，請重新輸入";
                return;
            }
        }
        if (!e.IsNewRow && gvMasterDV.IsEditing)
        {//update check 排除自已的店號
            if (ORD13_PageHelper.CheckStore(StringUtil.CStr(e.NewValues["STORE_NO"]), StringUtil.CStr(e.OldValues["STORE_NO"])))
            {
                e.RowError = StringUtil.CStr(e.NewValues["STORE_NO"]) + "已存在，請重新輸入";
                return;
            }
        }

        if (string.IsNullOrEmpty(ORD13_PageHelper.GetStoreName(StringUtil.CStr(e.NewValues["STORE_NO"]))))
        {
            e.RowError = StringUtil.CStr(e.NewValues["STORE_NO"]) + "不存在，請重新輸入";
            return;
        }

        if (!string.IsNullOrEmpty(new Store_Facade().GetCloseStoreName(StringUtil.CStr(e.NewValues["STORE_NO"]))))
        {
            e.RowError = StringUtil.CStr(e.NewValues["STORE_NO"]) + "閉店門市不可新增，請重新輸入";
            return;
        }
    }

    protected void gvMasterDV_InitNewRow(object sender, DevExpress.Web.Data.ASPxDataInitNewRowEventArgs e)
    {
        gvMasterDV.FocusedRowIndex = -1;
        gvMasterDV.Selection.UnselectAll();
        gvDetail.Selection.UnselectAll();
    }

    #endregion

    #region gvDetail 觸發的事件

    protected void gvDetail_PageIndexChanged(object sender, EventArgs e)
    {
        BindDetailData();
        gvMasterDV.Selection.UnselectAll();
    }

    protected void gvDetail_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
    {
        ORD13_SCQC_DTO ds = new ORD13_SCQC_DTO();

        ORD13_SCQC_DTO.SCQC_DDataTable dt = ds.SCQC_D;

        foreach (DataColumn col in dt.Columns)
        {
            col.AllowDBNull = true;
        }

        ORD13_SCQC_DTO.SCQC_DRow dr = dt.NewSCQC_DRow();

        dr.MODI_USER = logMsg.OPERATOR;
        dr.MODI_DTM = DateTime.Now;
        dr.SCQC_D_ID = StringUtil.CStr(e.Keys[0]);
        dr.SIM_GROUP_ID = StringUtil.CStr(e.NewValues["SIM_GROUP_ID"]);
        dr.SAFE_QTY = Convert.ToDecimal(e.NewValues["SAFE_QTY"]);
        dr.L_BOUND = Convert.ToDecimal(e.NewValues["L_BOUND"]);
        dr.ORDER_QTY = Convert.ToDecimal(0);

        //**2011/03/22 Tina：已補貨量 = 安全庫存量舊值 - 安全庫存量新值。
        //**2011/03/31 Tina：已補貨量改回欄位值，不為安全庫存量舊值 - 安全庫存量新值。
        //string strIN_QTY = StringUtil.CStr(ViewState["IN_QTY"]);
        //dr.IN_QTY = Convert.ToDecimal(string.IsNullOrEmpty(strIN_QTY) ? "0" : strIN_QTY);
        dr.IN_QTY = Convert.ToDecimal(0);

        if (e.NewValues["E_DATE"] == null)
        {
            dr.E_DATE = DateUtil.NullDateFormat(null);
        }
        else
        {
            if (StringUtil.CStr(e.NewValues["E_DATE"]) == "")
            {
                dr.E_DATE = DateUtil.NullDateFormat(null);
            }
            else
            {
                dr.E_DATE = Convert.ToDateTime(StringUtil.CStr(e.NewValues["E_DATE"]));
            }
        }

        dr.S_DATE = Convert.ToDateTime(StringUtil.CStr(e.NewValues["S_DATE"]));

        dt.AddSCQC_DRow(dr);
        ds.AcceptChanges();

        new ORD13_Facade().UpdateDetail(ds);

        ((ASPxGridView)sender).CancelEdit();
        e.Cancel = true;

        BindDetailData();
    }

    protected void gvDetail_RowValidating(object sender, DevExpress.Web.Data.ASPxDataValidationEventArgs e)
    {
        //ViewState["IN_QTY"] = "";
        if (string.IsNullOrEmpty(StringUtil.CStr(gvMasterDV.GetRowValues(gvMasterDV.FocusedRowIndex, "MASTER_ID"))))
        {
            gvDetail.CancelEdit();
            ASPxPageControl1.Visible = false;
            return;
        }

        if (string.IsNullOrEmpty(e.RowError) && string.IsNullOrEmpty(StringUtil.CStr(e.NewValues["SAFE_QTY"])))
        {
            e.RowError = "【安全庫存量】不允許空值!!";
        }

        if (string.IsNullOrEmpty(e.RowError) && string.IsNullOrEmpty(StringUtil.CStr(e.NewValues["L_BOUND"])))
        {
            e.RowError = "【最低庫存量】不允許空值!!";
        }

        string sS_DATE = Convert.ToDateTime(StringUtil.CStr(e.NewValues["S_DATE"])).ToString("yyyy/MM/dd");
        string sE_DATE = (e.NewValues["E_DATE"] == null) ? "" : Convert.ToDateTime(StringUtil.CStr(e.NewValues["E_DATE"])).ToString("yyyy/MM/dd");

        //ASPxDateEdit txtS_DATE = gvDetail.FindEditRowCellTemplateControl((GridViewDataColumn)gvDetail.Columns["S_DATE"], "txtS_DATE") as ASPxDateEdit;
        //ASPxDateEdit txtE_DATE = gvDetail.FindEditRowCellTemplateControl((GridViewDataColumn)gvDetail.Columns["E_DATE"], "txtE_DATE") as ASPxDateEdit;
        //string sS_DATE = txtS_DATE.Text;
        //string sE_DATE = txtE_DATE.Text;

        string sNow = DateTime.Now.ToString("yyyy/MM/dd");

        string status = StringUtil.CStr(gvDetail.GetRowValues(gvDetail.EditingRowVisibleIndex, "STATUS"));

        if (string.IsNullOrEmpty(e.RowError) && status != "有效" && sS_DATE.CompareTo(sNow) <= 0)
        {
            e.RowError = "開始日期不允許小於等於系統日，請重新輸入";
        }

        if (string.IsNullOrEmpty(e.RowError) && !string.IsNullOrEmpty(sE_DATE))
        {
            //**2011/04/01 Tina：結束日期可以等於開始日或系統日。
            if (sE_DATE.CompareTo(sNow) < 0 || sE_DATE.CompareTo(sS_DATE) < 0)
            {
                e.RowError = "結束日期不允許小於系統日及開始日期，請重新輸入";
            }
        }

        try
        {
            int intNewSAFE_QTY = int.Parse(StringUtil.CStr(e.NewValues["SAFE_QTY"]));
            int intL_BOUND = int.Parse(StringUtil.CStr(e.NewValues["L_BOUND"]));
            //檢查 安全庫存量 最低庫存量  
            if (string.IsNullOrEmpty(e.RowError) && intNewSAFE_QTY <= 0)
            {
                e.RowError = "安全庫存量不允許小於0，請重新輸入";
            }

            if (string.IsNullOrEmpty(e.RowError) && intL_BOUND <= 0)
            {
                e.RowError = "最低庫存量不允許小於0，請重新輸入";
            }

            //檢查 安全庫存量 最低庫存量  
            if (string.IsNullOrEmpty(e.RowError) && intNewSAFE_QTY <= intL_BOUND)
            {
                e.RowError = "安全庫存量不可小於等於最低庫存量";
            }

            //**2011/03/22 Tina：已補貨量 = 安全庫存量舊值 - 安全庫存量新值
            //**2011/03/31 Tina：已補貨量改回欄位值，不為安全庫存量舊值 - 安全庫存量新值。
            //if (string.IsNullOrEmpty(e.RowError) && e.OldValues["SAFE_QTY"] != null)
            //{
            //    int intOldSAFE_QTY = int.Parse(StringUtil.CStr(e.OldValues["SAFE_QTY"]));
            //    ViewState["IN_QTY"] = StringUtil.CStr(intOldSAFE_QTY - intNewSAFE_QTY);
            //}
        }
        catch //(Exception ex)
        {
            e.RowError = "安全庫存量/最低庫存量，請輸入數值";
        }


        if (string.IsNullOrEmpty(e.RowError))
        {
            string SIM_GROUP_ID = StringUtil.CStr(e.NewValues["SIM_GROUP_ID"]);
            string strGroupName = new ORD13_Facade().getSIM_GROUP_NAME(SIM_GROUP_ID);

            string MASTER_ID = StringUtil.CStr(gvMasterDV.GetRowValues(gvMasterDV.FocusedRowIndex, "MASTER_ID"));
            string DETAIL_ID = StringUtil.CStr(e.Keys["DETAIL_ID"]);

            if (ORD13_PageHelper.CheckGroup(MASTER_ID, SIM_GROUP_ID, DETAIL_ID, sS_DATE, sE_DATE))
            {
                e.RowError = strGroupName + "已存在，請重新輸入";
            }
        }

        if (!string.IsNullOrEmpty(e.RowError))
        {
            if (e.IsNewRow)
            {
                BindDetailData();
            }
            return;
        }
    }

    protected void gvDetail_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
    {
        ASPxGridView grid = (ASPxGridView)sender;

        ORD13_SCQC_DTO ds = new ORD13_SCQC_DTO();
        ORD13_SCQC_DTO.SCQC_DDataTable dt = ds.SCQC_D;

        ORD13_SCQC_DTO.SCQC_DRow dr = dt.NewSCQC_DRow();


        dr.SCQC_D_ID = GuidNo.getUUID();
        dr.SCQC_M_ID = StringUtil.CStr(gvMasterDV.GetRowValues(gvMasterDV.FocusedRowIndex, "MASTER_ID"));
        dr.SIM_GROUP_ID = StringUtil.CStr(e.NewValues["SIM_GROUP_ID"]);
        dr.SAFE_QTY = Convert.ToDecimal(e.NewValues["SAFE_QTY"]);
        dr.L_BOUND = Convert.ToDecimal(e.NewValues["L_BOUND"]);
        dr.ORDER_QTY = Convert.ToDecimal(0);
        dr.IN_QTY = Convert.ToDecimal(0);

        if (e.NewValues["E_DATE"] == null)
        {
            dr.E_DATE = DateUtil.NullDateFormat(null);
        }
        else
        {
            if (StringUtil.CStr(e.NewValues["E_DATE"]) == "")
            {
                dr.E_DATE = DateUtil.NullDateFormat(null);
            }
            else
            {
                dr.E_DATE = Convert.ToDateTime(StringUtil.CStr(e.NewValues["E_DATE"]));
            }
        }

        dr.S_DATE = Convert.ToDateTime(StringUtil.CStr(e.NewValues["S_DATE"]));
        dr.CREATE_USER = logMsg.OPERATOR;
        dr.CREATE_DTM = DateTime.Now;
        dr.MODI_USER = logMsg.OPERATOR;
        dr.MODI_DTM = DateTime.Now;

        dt.AddSCQC_DRow(dr);
        ds.AcceptChanges();

        new ORD13_Facade().InsertDetail(ds);

        grid.CancelEdit();
        e.Cancel = true;

        BindDetailData();
    }

    protected void gvDetail_StartRowEditing(object sender, DevExpress.Web.Data.ASPxStartRowEditingEventArgs e)
    {
        gvMasterDV.Selection.UnselectAll();
        gvDetail.Selection.UnselectAll();

        //GridViewDataDateColumn colE_DATE = (GridViewDataDateColumn)gvDetail.Columns["E_DATE"];

    }

    protected void gvDetail_InitNewRow(object sender, DevExpress.Web.Data.ASPxDataInitNewRowEventArgs e)
    {
        gvMasterDV.Selection.UnselectAll();
        gvDetail.Selection.UnselectAll();
    }

    protected void gvDetail_CellEditorInitialize(object sender, ASPxGridViewEditorEventArgs e)
    {
        if (e.Column.FieldName == "SIM_GROUP_ID")
        {
            ASPxComboBox ddlUSE_TYPE = e.Editor as ASPxComboBox;
            ddlUSE_TYPE.DataSource = new ORD13_Facade().TopDatatable1("", "", false);
            ddlUSE_TYPE.DataBind();
        }

        //else if (e.Column.FieldName == "S_DATE")
        //{
        //    ASPxDateEdit txtS_DATE = gvDetail.FindEditRowCellTemplateControl((GridViewDataColumn)gvDetail.Columns["S_DATE"], "txtS_DATE") as ASPxDateEdit;
        //    string Date = "";
        //    if (e.Value != null)
        //    {
        //        Date = StringUtil.CStr(e.Value);
        //    }
        //    txtS_DATE.Text = string.IsNullOrEmpty(Date) ? "" : Convert.ToDateTime(Date).ToString("yyyy/MM/dd");
        //}


        if (!gvDetail.IsNewRowEditing) //編輯
        {
            string status = StringUtil.CStr(gvDetail.GetRowValues(e.VisibleIndex, "STATUS"));
            if (status == "有效")
            {
                if (e.Editor is ASPxTextBox)
                {
                    (e.Editor as ASPxTextBox).ClientEnabled = false;
                }
                else if (e.Editor is ASPxComboBox)
                {
                    (e.Editor as ASPxComboBox).ClientEnabled = false;
                }
                else if (e.Editor is ASPxDateEdit)
                {
                    SetDateEditProperties(e, status);
                }
            }
            else  //尚未生效
            {
                SetDateEditProperties(e, status);
            }
        }
        else  //新增
        {
            SetDateEditProperties(e, "新增");
        }

        // e.Editor.ReadOnly = false;
    }

    protected void gvDetail_CommandButtonInitialize(object sender, ASPxGridViewCommandButtonEventArgs e)
    {
        if (e.VisibleIndex > -1)
        {
            if (e.ButtonType == ColumnCommandButtonType.Edit || e.ButtonType == ColumnCommandButtonType.SelectCheckbox)
            {
                e.Enabled = true;

                //**2011/03/16 Tina：先判斷該卡片群組名稱在【卡片群組設定】中是否為有效
                string group_status = StringUtil.CStr(gvDetail.GetRowValues(e.VisibleIndex, "GROUP_STATUS"));
                if (group_status != "有效")
                {
                    e.Enabled = false;
                }
                else
                {
                    string status = StringUtil.CStr(gvDetail.GetRowValues(e.VisibleIndex, "STATUS"));

                    if (e.ButtonType == ColumnCommandButtonType.Edit)
                    {
                        if (status == "已過期")
                        {
                            e.Enabled = false;
                        }
                    }

                    if (e.ButtonType == ColumnCommandButtonType.SelectCheckbox)
                    {
                        if (status == "尚未生效")
                            e.Enabled = true;
                        else
                            e.Enabled = false;
                    }
                }
            }
        }
    }

    protected void gvDetail_HtmlRowPrepared(object sender, ASPxGridViewTableRowEventArgs e)
    {
        if (e.RowType == GridViewRowType.Data)
        {
            e.Row.Attributes["canSelect"] = "true";
            //**2011/03/16 Tina：先判斷該卡片群組名稱在【卡片群組設定】中是否為有效
            string group_status = StringUtil.CStr(e.GetValue("GROUP_STATUS"));
            if (group_status != "有效")
            {
                e.Row.Attributes["canSelect"] = "false";
            }
            else
            {
                string status = StringUtil.CStr(e.GetValue("STATUS"));

                switch (status)
                {
                    case "有效":
                        e.Row.Attributes["canSelect"] = "false";
                        break;
                    case "已過期":
                        e.Row.Attributes["canSelect"] = "false";
                        break;
                }
            }
        }
    }

    #endregion

    //ajax 呼當前網頁的方式
    [System.Web.Services.WebMethod()]
    [System.Web.Script.Services.ScriptMethod()]
    static public string getStoreInfo(string PRODNO)
    {
        string strInfo = "";
        if (!string.IsNullOrEmpty(PRODNO))
        {
            strInfo = ORD13_PageHelper.GetStoreName(PRODNO);
        }

        return strInfo;
    }

}

