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
using DevExpress.Web.Data;

using FET.POS.Model.DTO;
using FET.POS.Model.Common;
using FET.POS.Model.Facade.FacadeImpl;
using System.Web.Configuration;

public partial class VSS_OPT_OPT05_OPT05a : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string DefaultStore = StringUtil.CStr(WebConfigurationManager.AppSettings["DefaultStore"]);
        //畫面PostBack就要重新設置為true，不然會變回預設的false
        //logMsg.ROLE_TYPE == WebConfigurationManager.AppSettings["DefaultRoleHQ"]
        if (logMsg.STORENO == DefaultStore)
        {
            this.txtStroeNo.Enabled = true;
        }

        if (!IsPostBack && !IsCallback)
        {
            //**2011/04/07 Tina：改成用登入門市來判斷是否為總部
            //logMsg.ROLE_TYPE != WebConfigurationManager.AppSettings["DefaultRoleHQ"] ||
            if (logMsg.STORENO != DefaultStore) //總部人員可查詢各門市資料, 門市人員只能查詢自己的門市
            {
                this.txtStroeNo.Text = logMsg.STORENO;
                this.txtStoreName.Text = getStoreInfo(logMsg.STORENO);
                this.lblStoreName.Text = this.txtStoreName.Text;
                this.txtStoreName.ClientEnabled = false;
            }
            else
            {
                this.btnSave.ClientEnabled = false;
                this.btnCancel.ClientEnabled = false;
            }

            //取得空的資料表(只有結構描述)
            gvMaster.DataSource = new OPT05_HqInvoiceAssign_DTO().HQ_INVOICE_ASSIGN;
            gvMaster.DataBind();
        }
    }

    private void BindMasterData()
    {
        string strStoreNO = "";

        //**2011/04/07 Tina：改成用登入門市來判斷是否為總部
        //logMsg.ROLE_TYPE == WebConfigurationManager.AppSettings["DefaultRoleHQ"]
        if (logMsg.STORENO == StringUtil.CStr(System.Web.Configuration.WebConfigurationManager.AppSettings["DefaultStore"])) //總部人員可查詢各門市資料, 門市人員只能查詢自己的門市
        {
            strStoreNO = this.txtStroeNo.Text;
        }
        else
        {
            strStoreNO = logMsg.STORENO;
        }

        DataTable dt = new OPT05_PageHelper().GetHeadQuarterInvoice(
            strStoreNO
            , this.txtStoreName.Text
            , ""
            , txtSDate.Text
            , txtEDate.Text);

        gvMaster.DataSource = dt;
        gvMaster.DataBind();
        gvMaster.FocusedRowIndex = -1;
        gvMaster.Selection.UnselectAll();

        if (dt.Rows.Count == 0)
        {
            gvDetail.Visible = false;
        }

    }

    private void BindDetailData()
    {
        if (gvMaster.FocusedRowIndex != -1)
        {
            DataTable dt = new DataTable();

            string ASSIGN_ID = StringUtil.CStr(gvMaster.GetRowValues(gvMaster.FocusedRowIndex, "ASSIGN_ID"));
            string STORE_NO = StringUtil.CStr(gvMaster.GetRowValues(gvMaster.FocusedRowIndex, "STORE_NO"));
            
            if (ViewState["dtAssignNo"] != null)
            {
                dt = ViewState["dtAssignNo"] as DataTable;

                DataRow[] dr = dt.Select("ASSIGN_ID='" + ASSIGN_ID + "' AND STORE_NO='" + STORE_NO + "'");
                if (!(ASSIGN_ID == StringUtil.CStr(dr[0]["ASSIGN_ID"]) && STORE_NO == StringUtil.CStr(dr[0]["STORE_NO"])))
                {
                    dt = new OPT05_PageHelper().GetStoreMachineInvoiceData(ASSIGN_ID, STORE_NO);
                }
                else
                {
                    dt = ViewState["dtAssignNo"] as DataTable;
                }
            }
            else
            {
                dt = new OPT05_PageHelper().GetStoreMachineInvoiceData(ASSIGN_ID, STORE_NO);
            }

            ViewState["dtAssignNo"] = dt;
            gvDetail.DataSource = dt;
            gvDetail.DataBind();
            gvDetail.FocusedRowIndex = -1;

        }
    }

    #region gvMaster 觸發事件

    protected void gvMaster_HtmlRowPrepared(object sender, ASPxGridViewTableRowEventArgs e)
    {
        e.Row.Attributes["canSelect"] = "false";
        if (e.RowType == GridViewRowType.Data)
        {
            if (e.VisibleIndex > -1)
            {
                //所屬年月的起訖時間未包含本月或是本月以前的手開發票的資料才能被刪除。
                //用途為"連線"的發票資料無法被刪除或編輯，因此，該筆資料的[編輯]按鈕會反灰，無法點選。此外，該筆資料也無法被勾選並刪除。
                //用途為"離線"的發票資料無法被刪除或是直接編輯，但是使用者可以點選該筆資料的項次，系統會在畫面下方產生機台發票明細設定，使用者可以編輯這筆資料所對應的機台發票明細設定。
                string USE_TYPE = StringUtil.CStr(gvMaster.GetRowValues(e.VisibleIndex, "USE_TYPE"));
                ASPxLabel lblItemIndex = gvMaster.FindRowCellTemplateControl(e.VisibleIndex, (GridViewDataColumn)gvMaster.Columns["ItemIndex"], "lblItemIndex") as ASPxLabel;
                ASPxButton btnCommand = gvMaster.FindRowCellTemplateControl(e.VisibleIndex, (GridViewDataColumn)gvMaster.Columns["ItemIndex"], "btnCommand") as ASPxButton;
                if (lblItemIndex != null && btnCommand != null)
                {
                    lblItemIndex.ClientVisible = true;
                    btnCommand.ClientVisible = false;
                }

                if (USE_TYPE == "1" || USE_TYPE == "2")  //1：連線，2：離線
                {
                    if (USE_TYPE == "2" && lblItemIndex != null && btnCommand != null)
                    {
                        lblItemIndex.ClientVisible = false;
                        btnCommand.ClientVisible = true;
                    }
                }
                else  //3：手開
                {
                    string SDate = StringUtil.CStr(gvMaster.GetRowValues(e.VisibleIndex, "S_USE_YM")) + "/01";
                    string EDate = StringUtil.CStr(gvMaster.GetRowValues(e.VisibleIndex, "E_USE_YM")) + "/01";
                     string SysDate = DateTime.Now.Year + "/" + DateTime.Now.Month + "/01";
                     //string status = StringUtil.CStr(gvMaster.GetRowValues(e.VisibleIndex, "CAN_EDIT"));
                    //if (status == "1")

                     if (Convert.ToDateTime(SDate) > Convert.ToDateTime(SysDate))
                     {
                         e.Row.Attributes["canSelect"] = "true";
                         //<ClientSideEvents RowDblClick="function(s, e) { gvMaster.StartEditRow(e.visibleIndex); 
                         //gvMaster.ClientSideEvents.RowDblClick = "function(s, e) {gvMaster.StartEditRow(" + e.VisibleIndex + "); }";
                     }


                }

            }
        
        }
    }

    protected void gvMaster_RowCommand(object sender, ASPxGridViewRowCommandEventArgs e)
    {
        if (StringUtil.CStr(gvMaster.GetRowValues(e.VisibleIndex, "USE_TYPE")) == "2")
        {
            if (!gvMaster.IsEditing)
            {
                ViewState["dtAssignNo"] = null;
                gvMaster.FocusedRowIndex = e.VisibleIndex;
                BindDetailData();

                gvDetail.Visible = true;
                showFooterBtn.Visible = true;
            }
           
        }
        else
        {
            gvDetail.Visible = false;
            showFooterBtn.Visible = false;
        }
    }

    protected void gvMaster_RowInserting(object sender, ASPxDataInsertingEventArgs e)
    {
        if (!string.IsNullOrEmpty(this.txtStroeNo.Text.Trim()))
        {
            OPT05_HqInvoiceAssign_DTO ds = new OPT05_HqInvoiceAssign_DTO();
            OPT05_HqInvoiceAssign_DTO.HQ_INVOICE_ASSIGNDataTable dt = ds.HQ_INVOICE_ASSIGN;
            OPT05_HqInvoiceAssign_DTO.HQ_INVOICE_ASSIGNRow dr = dt.NewHQ_INVOICE_ASSIGNRow();

            //**2011/03/14 Tina：USE_TYPE值非固定值"3"，而是看發票格式 =>手開二連式發票="3"；手開三連式發票="4"
            ASPxComboBox ddlInvoiceType = gvMaster.FindEditRowCellTemplateControl((GridViewDataColumn)gvMaster.Columns["INVOICE_TYPE_ID"], "ddlInvoiceType") as ASPxComboBox;

            dr["ASSIGN_ID"] = GuidNo.getUUID();
            dr["USE_TYPE"] = StringUtil.CStr(ddlInvoiceType.Value) == "03" ? "3" : "4";
            dr["INVOICE_TYPE_ID"] = ddlInvoiceType.Value;
            dr["S_USE_YM"] = Convert.ToDateTime(e.NewValues["S_USE_YM"]).ToString("yyyy/MM");
            dr["E_USE_YM"] = Convert.ToDateTime(e.NewValues["E_USE_YM"]).ToString("yyyy/MM");
            dr["LEADER_CODE"] = e.NewValues["LEADER_CODE"];
            dr["INIT_NO"] = e.NewValues["INIT_NO"];
            dr["END_NO"] = e.NewValues["END_NO"];
            dr["SHEET_COUNT"] = Convert.ToDecimal(e.NewValues["END_NO"]) - Convert.ToDecimal(e.NewValues["INIT_NO"]) + 1;
            dr["STORE_NO"] = this.txtStroeNo.Text;
            dr["MODI_USER"] = logMsg.OPERATOR;
            dr["MODI_DTM"] = DateTime.Now;
            dr["CREATE_USER"] = dr["MODI_USER"];
            dr["CREATE_DTM"] = dr["MODI_DTM"];

            dt.AddHQ_INVOICE_ASSIGNRow(dr);

            ds.AcceptChanges();

            try
            {
                new OPT05_Facade().InsertHeadQuarterInvoice(ds);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            gvMaster.CancelEdit();
            e.Cancel = true;

            BindMasterData();
        }

    }

    protected void gvMaster_RowUpdating(object sender, ASPxDataUpdatingEventArgs e)
    {
        OPT05_HqInvoiceAssign_DTO ds = new OPT05_HqInvoiceAssign_DTO();
        OPT05_HqInvoiceAssign_DTO.HQ_INVOICE_ASSIGNDataTable dt = ds.HQ_INVOICE_ASSIGN;
        OPT05_HqInvoiceAssign_DTO.HQ_INVOICE_ASSIGNRow dr = dt.NewHQ_INVOICE_ASSIGNRow();

        DataRow dirtyDr = OPT05_PageHelper.Query_HeadQuarterInvoice_ByKey(StringUtil.CStr(e.Keys["ASSIGN_ID"]));

        //**2011/03/14 Tina：USE_TYPE值非固定值"3"，而是看發票格式 =>手開二連式發票="3"；手開三連式發票="4"
        ASPxComboBox ddlInvoiceType = gvMaster.FindEditRowCellTemplateControl((GridViewDataColumn)gvMaster.Columns["INVOICE_TYPE_ID"], "ddlInvoiceType") as ASPxComboBox;

        dr["ASSIGN_ID"] = StringUtil.CStr(e.Keys["ASSIGN_ID"]);
        dr["USE_TYPE"] = StringUtil.CStr(ddlInvoiceType.Value) == "03" ? "3" : "4";
        dr["INVOICE_TYPE_ID"] = ddlInvoiceType.Value;
        dr["S_USE_YM"] = ((ASPxDateEdit)gvMaster.FindEditRowCellTemplateControl((GridViewDataColumn)gvMaster.Columns["S_USE_YM"], "txtS_DATE")).Text;
        dr["E_USE_YM"] = ((ASPxDateEdit)gvMaster.FindEditRowCellTemplateControl((GridViewDataColumn)gvMaster.Columns["E_USE_YM"], "txtE_DATE")).Text;
        dr["LEADER_CODE"] = e.NewValues["LEADER_CODE"];
        dr["INIT_NO"] = e.NewValues["INIT_NO"];
        dr["END_NO"] = e.NewValues["END_NO"];
        dr["SHEET_COUNT"] = Convert.ToDecimal(e.NewValues["END_NO"]) - Convert.ToDecimal(e.NewValues["INIT_NO"]) + 1;
        dr["STORE_NO"] = dirtyDr["STORE_NO"];
        dr["MODI_USER"] = logMsg.OPERATOR;
        dr["MODI_DTM"] = DateTime.Now;
        dr["CREATE_USER"] = dirtyDr["CREATE_USER"];
        dr["CREATE_DTM"] = dirtyDr["CREATE_DTM"];

        dt.AddHQ_INVOICE_ASSIGNRow(dr);

        ds.AcceptChanges();

        try
        {
            new OPT05_Facade().UpdateHeadQuarterInvoiceData(ds);
        }
        catch (Exception ex)
        {
            throw ex;
        }

        gvMaster.CancelEdit();
        e.Cancel = true;

        BindMasterData();
    }

    protected void gvMaster_CommandButtonInitialize(object sender, ASPxGridViewCommandButtonEventArgs e)
    {
        if (e.VisibleIndex > -1)
        {
            if (e.ButtonType == ColumnCommandButtonType.Edit || e.ButtonType == ColumnCommandButtonType.SelectCheckbox)
            {
                //只有所屬年月區間不是本月或是本月以前的手開發票資料，才能按下[編輯]按鈕。
                //系統不允許使用者編輯已過期或正在使用的手開發票設定資料，因此，該筆資料的[編輯]按鈕會反灰，無法點選。
                  
                gvDetail.Visible = false;
                showFooterBtn.Visible = false;

                string status1 = StringUtil.CStr(gvMaster.GetRowValues(e.VisibleIndex, "USE_TYPE"));        //使用用途： 連線, 離線, 手開
                string status2 = StringUtil.CStr(gvMaster.GetRowValues(e.VisibleIndex, "CAN_EDIT"));        //0：本月以前  1：未來月份
                string CURRENT_NO = StringUtil.CStr(gvMaster.GetRowValues(e.VisibleIndex, "CURRENT_NO"));   //目前編號

                e.Enabled = ((status1 == "3" || status1 == "4") && status2 == "1" && string.IsNullOrEmpty(CURRENT_NO));

                //if (e.ButtonType == ColumnCommandButtonType.SelectCheckbox)
                //{ 
                
                //    //所屬年月的起訖時間未包含本月或是本月以前的手開發票的資料才能被刪除。
                //    //用途為"連線"的發票資料無法被刪除或編輯，因此，該筆資料的[編輯]按鈕會反灰，無法點選。此外，該筆資料也無法被勾選並刪除。
                //    //用途為"離線"的發票資料無法被刪除或是直接編輯，但是使用者可以點選該筆資料的項次，系統會在畫面下方產生機台發票明細設定，使用者可以編輯這筆資料所對應的機台發票明細設定。

                //}
            }
        }
    }

    protected void gvMaster_RowValidating(object sender, ASPxDataValidationEventArgs e)
    {
        string sYM = ((ASPxDateEdit)gvMaster.FindEditRowCellTemplateControl((GridViewDataColumn)gvMaster.Columns["S_USE_YM"], "txtS_DATE")).Text;
        string eYM = ((ASPxDateEdit)gvMaster.FindEditRowCellTemplateControl((GridViewDataColumn)gvMaster.Columns["E_USE_YM"], "txtE_DATE")).Text;

        int s_use_m = Convert.ToInt32(sYM.Replace("/", ""));
        int e_use_m = Convert.ToInt32(eYM.Replace("/", ""));
        int sysYM = Convert.ToInt32(System.DateTime.Today.ToString("yyyy/MM").Replace("/", ""));

        if (e_use_m - s_use_m != 1 ||
            s_use_m % 2 != 1 ||
            e_use_m % 2 != 0
            //|| s_use_m < sysYM
            //|| e_use_m < sysYM
            )
        {
            e.RowError = "所屬年月設定錯誤。";
            return;
        }

        //**2011/02/16 Tina：只能設定當月(含)之後的發票資訊
        if (s_use_m < sysYM || e_use_m < sysYM)
        {
            if (s_use_m != sysYM && e_use_m < sysYM)
            {
                e.RowError = "月份不可小於系統月份。";
                return;
            }
        }

        if ((Convert.ToInt32(e.NewValues["END_NO"]) - Convert.ToInt32(e.NewValues["INIT_NO"])) <= 0)
        {
            e.RowError = "編號設定錯誤。";
            return;
        }

        //**2011/03/31 Tina：手開發票，起始編號~終止編號區間輸入的張數一定要50，因為一本手開發票區間為50。
        //**2011/04/13 Bill： end - star + 1 包涵起始號碼
        if ((Convert.ToInt32(e.NewValues["END_NO"]) - Convert.ToInt32(e.NewValues["INIT_NO"]))+1 != 50)
        {
            e.RowError = "發票張數須設定為50張。";
            return;
        }


        string Key = "";
        if (e.Keys["ASSIGN_ID"] != null)
        {
            Key = StringUtil.CStr(e.Keys["ASSIGN_ID"]);
        }

        if (e.IsNewRow)
        {
            if (OPT05_Facade.CheckStoreInvoiceNO_New(
                Key,
              this.txtStroeNo.Text.Trim(),
              "3",
              StringUtil.CStr(e.NewValues["LEADER_CODE"]),
              sYM,
              eYM,
              StringUtil.CStr(e.NewValues["INIT_NO"]),
              StringUtil.CStr(e.NewValues["END_NO"])
              ))
            {
                e.RowError = "同一所屬年度區間中不可有二筆以上的相同字軌、重複編號區間的發票設定。";
                return;
            }

        }
        else
        {
            if (OPT05_Facade.CheckStoreInvoiceNO_UPD(
                Key,
              this.txtStroeNo.Text.Trim(),
              "3",
              StringUtil.CStr(e.NewValues["LEADER_CODE"]),
              sYM,
              eYM,
              StringUtil.CStr(e.NewValues["INIT_NO"]),
              StringUtil.CStr(e.NewValues["END_NO"])
              ))
            {
                e.RowError = "同一所屬年度區間中不可有二筆以上的相同字軌、重複編號區間的發票設定。";
                return;
            }
        }

    }

    protected void gvMaster_PageIndexChanged(object sender, EventArgs e)
    {
        gvDetail.Visible = false;
        showFooterBtn.Visible = false;

        BindMasterData();
    }
  
    protected void gvMaster_InitNewRow(object sender, ASPxDataInitNewRowEventArgs e)
    {
       // ASPxTextBox txtDisUseCount = gvStore.FindEditRowCellTemplateControl((GridViewDataColumn)gvStore.Columns["DIS_USE_COUNT"], "txtDisUseCount") as ASPxTextBox;
        ASPxDateEdit SDate = gvMaster.FindEditRowCellTemplateControl((GridViewDataColumn)gvMaster.Columns["S_USE_YM"], "txtS_DATE") as ASPxDateEdit;
        ASPxDateEdit EDate = gvMaster.FindEditRowCellTemplateControl((GridViewDataColumn)gvMaster.Columns["E_USE_YM"], "txtE_DATE") as ASPxDateEdit;
        SDate.Text = DateTime.Now.ToString("yyyy/MM");
        EDate.Text = DateTime.Now.AddMonths(1).ToString("yyyy/MM");

        //txtDisUseCount.Text = txtLTNDis.Text;
      //  txtDisUseCount.Value = txtLTNDis.Text;
    }

    protected void gvMaster_StartRowEditing(object sender, ASPxStartRowEditingEventArgs e)
    {
        gvMaster.Selection.UnselectAll();
    }

    protected void gvMaster_PreRender(object sender, EventArgs e)
    {
        if (txtStroeNo.Text == "")
        {
            gvMaster.FindChildControl<ASPxButton>("btnAdd").ClientEnabled = false;
            gvMaster.FindChildControl<ASPxButton>("btnDelete").ClientEnabled = false;
        }
    }

    #endregion
    
    #region Button 觸發事件

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        if (getStoreInfo(txtStroeNo.Text) != "")
        {
            SetInitState_gvMaster();
            gvDetail.Visible = false;

            this.gvMaster.AddNewRow();
        }
        else
        {
            gvMaster.FindChildControl<ASPxButton>("btnAdd").ClientEnabled = false;
            gvMaster.FindChildControl<ASPxButton>("btnDelete").ClientEnabled = false;
            btnSave.ClientEnabled = false;
        }
    }

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        if (gvMaster.Selection.Count < 0) { return; }

        DataTable dt = new DataTable("HQ_INVOICE_ASSIGN");
        dt.Columns.Add("ASSIGN_ID");

        foreach (object key in gvMaster.GetSelectedFieldValues(gvMaster.KeyFieldName))
        {
            DataRow dr = dt.NewRow();
            dr[0] = key;
            dt.Rows.Add(dr);
        }
        gvDetail.Visible = false;
        showFooterBtn.Visible = false;

        dt.AcceptChanges();

        try
        {
            new OPT05_Facade().DeleteHeadQuarterInvoiceData(dt);
        }
        catch (Exception ex)
        {
            throw ex;
        }

        BindMasterData();
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        BindMasterData();
        gvMaster.PageIndex = 0;

    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (ViewState["dtAssignNo"] != null && string.IsNullOrEmpty(hdError.Text))
        {
            DataTable AssignDt = ViewState["dtAssignNo"] as DataTable;

            foreach (DataRow dr in AssignDt.Rows)
            {
                if ((string.IsNullOrEmpty(StringUtil.CStr(dr["START_NO"])) && !string.IsNullOrEmpty(StringUtil.CStr(dr["END_NO"])))
                       || (string.IsNullOrEmpty(StringUtil.CStr(dr["END_NO"])) && !string.IsNullOrEmpty(StringUtil.CStr(dr["START_NO"]))))
                {
                    //彈跳視窗，提示編號必須有始有終
                    ScriptManager.RegisterClientScriptBlock(this, GetType(), "AlertPrint", "alert('機台明細的起始編號和終止編號不得有一項有值，另一項空值 !!');", true);
                    return;
                }

                if (StringUtil.CStr(dr["START_NO"]) != StringUtil.CStr(dr["OLDSTART_NO"]) || StringUtil.CStr(dr["END_NO"]) != StringUtil.CStr(dr["OLDEND_NO"]))   //有變更編號才進行Table Row更新
                {
                    dr["IsModify"] = "T";
                    if (string.IsNullOrEmpty(StringUtil.CStr(dr["START_NO"])) || string.IsNullOrEmpty(StringUtil.CStr(dr["END_NO"])))
                    {
                        dr["START_NO"] = "";
                        dr["END_NO"] = "";
                        dr["MODI_DTM"] = DBNull.Value;
                    }
                    else
                    {
                        dr["START_NO"] = dr["START_NO"];
                        dr["END_NO"] = dr["END_NO"];
                        dr["SHEET_COUNT"] = Convert.ToDecimal(dr["END_NO"]) - Convert.ToDecimal(dr["START_NO"]) + 1;
                        dr["MODI_DTM"] = Convert.ToDateTime(System.DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"));
                    }

                    dr["MACHINE_ID"] = dr["MACHINE_ID"];
                    dr["ASSIGN_ID"] = StringUtil.CStr(gvMaster.GetRowValues(gvMaster.FocusedRowIndex, "ASSIGN_ID"));
                    dr["STORE_NO"] = logMsg.STORENO;
                    dr["MODI_USER"] = logMsg.OPERATOR;

                    AssignDt.AcceptChanges();
                }
            }

            ViewState["dtAssignNo"] = null;

            new OPT05_Facade().UpdateStoreMachineInvoiceData(AssignDt);

            BindDetailData();

            //彈跳視窗，提示存檔成功
            ScriptManager.RegisterClientScriptBlock(this, GetType(), "AlertPrint", "alert('存檔完成!!');", true);

        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {        
        ViewState["dtAssignNo"] = null;

        gvDetail.DataSource = null;
        gvDetail.DataBind();
        
        BindDetailData();
    }

    #endregion

    protected void gvDetail_PageIndexChanged(object sender, EventArgs e)
    {
        BindDetailData();
    }

    protected void gvDetail_HtmlRowCreated(object sender, ASPxGridViewTableRowEventArgs e)
    {

        if (e.RowType == GridViewRowType.Data)
        {

            string CURRENT_NO = StringUtil.CStr(e.GetValue("CURRENT_NO"));

            ASPxTextBox txtSTART_NO = e.Row.FindChildControl<ASPxTextBox>("txtSTART_NO");
            ASPxTextBox txtEND_NO = e.Row.FindChildControl<ASPxTextBox>("txtEND_NO");

            txtSTART_NO.Validate();
            txtEND_NO.Validate();

            if (!string.IsNullOrEmpty(CURRENT_NO))
            {
                txtSTART_NO.ClientEnabled = false;
                txtEND_NO.ClientEnabled = false;
            }
          
        }
    }

    protected void txtSTART_NO_TextChanged(object sender, EventArgs e)
    {
        if (this.gvDetail.FocusedRowIndex > -1)
        {

            ASPxTextBox control = sender as ASPxTextBox;
            DataTable dt = ViewState["dtAssignNo"] as DataTable;
            DataRow[] dr = dt.Select("HOST_NO='" + StringUtil.CStr(this.gvDetail.GetRowValues(this.gvDetail.FocusedRowIndex, this.gvDetail.KeyFieldName)) + "'");
            dr[0]["START_NO"] = control.Text;
            dt.AcceptChanges();
            ViewState["dtAssignNo"] = dt;

            checkAssignNo(control, "START_NO");
        }
    }

    protected void txtEND_NO_TextChanged(object sender, EventArgs e)
    {
        if (this.gvDetail.FocusedRowIndex > -1)
        {
            ASPxTextBox control = sender as ASPxTextBox;

            DataTable dt = ViewState["dtAssignNo"] as DataTable;
            DataRow[] dr = dt.Select("HOST_NO='" + StringUtil.CStr(this.gvDetail.GetRowValues(this.gvDetail.FocusedRowIndex, this.gvDetail.KeyFieldName)) + "'");
            dr[0]["END_NO"] = control.Text;
            dt.AcceptChanges();
            ViewState["dtAssignNo"] = dt;

            checkAssignNo(control, "END_NO");
        }
    }

    private void checkAssignNo(ASPxTextBox control, string NO)
    {
        hdError.Text = "";

        if (!string.IsNullOrEmpty(control.Text))
        {
            int M_SNO = Convert.ToInt32(this.gvMaster.GetRowValues(gvMaster.FocusedRowIndex, "INIT_NO"));
            int M_ENO = Convert.ToInt32(this.gvMaster.GetRowValues(gvMaster.FocusedRowIndex, "END_NO"));
            int sValue = Convert.ToInt32(control.Text);

            string strInfo = "";

            if (!(sValue >= M_SNO && sValue <= M_ENO))
            {
                strInfo = "編號必須落在主檔的編號區間，請重新輸入";
            }
            else
            {
                    string SNO = StringUtil.CStr(this.gvDetail.GetRowValues(this.gvDetail.FocusedRowIndex, "START_NO"));
                    string ENO = StringUtil.CStr(this.gvDetail.GetRowValues(this.gvDetail.FocusedRowIndex, "END_NO"));

                    DataTable dt1 = ViewState["dtAssignNo"] as DataTable ?? null;

                    DataRow[] DRSelf = dt1.Select("HOST_NO='" + StringUtil.CStr(this.gvDetail.GetRowValues(this.gvDetail.FocusedRowIndex, "HOST_NO")).Trim() + "'");
                    if (DRSelf.Length > 0)
                    {
                        SNO = StringUtil.CStr(DRSelf[0]["START_NO"]).Trim();
                        ENO = StringUtil.CStr(DRSelf[0]["END_NO"]).Trim();
                    }

                    if (!string.IsNullOrEmpty(SNO) && !string.IsNullOrEmpty(ENO))
                    {
                        if (Convert.ToInt32(ENO) < Convert.ToInt32(SNO))
                        {
                            strInfo = "終止編號不可小於起始編號，請重新輸入";
                        } //end-if (D_ENO < D_SNO) 
                    } //end-if (!string.IsNullOrEmpty(D_SNO) && !string.IsNullOrEmpty(D_ENO)) 

                    if (string.IsNullOrEmpty(strInfo))
                    {

                        string HOST = StringUtil.CStr(this.gvDetail.GetRowValues(this.gvDetail.FocusedRowIndex, this.gvDetail.KeyFieldName));
                        DataTable dt = ViewState["dtAssignNo"] as DataTable;
                        foreach (DataRow dr in dt.Rows)
                        {
                            string HOST_NO = StringUtil.CStr(dr["HOST_NO"]);
                            string D_SNO = StringUtil.CStr(dr["START_NO"]);
                            string D_ENO = StringUtil.CStr(dr["END_NO"]);

                            if (!string.IsNullOrEmpty(D_SNO) || !string.IsNullOrEmpty(D_ENO))
                            {
                                if (string.IsNullOrEmpty(D_SNO)) { D_SNO = "00000000"; }
                                if (string.IsNullOrEmpty(D_ENO)) { D_ENO = "100000000"; }

                                if (sValue >= Convert.ToInt32(D_SNO) && sValue <= Convert.ToInt32(D_ENO) && HOST != HOST_NO)
                                {
                                    strInfo = "編號區間重疊，請重新輸入";
                                    break;
                                }
                            }
                        }
                    }
            } //end- if (!(sValue >= M_SNO && controlValue <= M_ENO))

            if (!string.IsNullOrEmpty(strInfo))
            {
                DataTable dt = ViewState["dtAssignNo"] as DataTable;
                DataRow[] dr = dt.Select("HOST_NO='" + StringUtil.CStr(this.gvDetail.GetRowValues(this.gvDetail.FocusedRowIndex, this.gvDetail.KeyFieldName)) + "'");
                dr[0][NO] = "";
                dt.AcceptChanges();
                ViewState["dtAssignNo"] = dt;

                control.Text = "";
                control.Focus();
                //彈跳視窗
                ScriptManager.RegisterClientScriptBlock(this, GetType(), "AlertMessage", "alert('" + strInfo + "');", true);
                hdError.Text = "strInfo";
                return;
            }
        }

    }

    //ajax 呼當前網頁的方式
    [System.Web.Services.WebMethod()]
    [System.Web.Script.Services.ScriptMethod()]
    static public string getStoreInfo(string StoreNO)
    {
        string strInfo = "";
        if (!string.IsNullOrEmpty(StoreNO))
        {
            DataTable dt = new Store_Facade().Query_StoreInfo(StoreNO);
            if (dt.Rows.Count > 0)
            {
                strInfo = StringUtil.CStr(dt.Rows[0]["STORENAME"]);
            }
        }

        return strInfo;
    }

    private void SetInitState_gvMaster()
    {
        gvMaster.PageIndex = 0;
        gvMaster.FocusedRowIndex = -1;
        gvMaster.Selection.UnselectAll();
        gvMaster.CancelEdit();
    }
}
