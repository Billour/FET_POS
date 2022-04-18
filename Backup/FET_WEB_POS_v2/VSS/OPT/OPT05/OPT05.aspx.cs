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
using System.Web.Configuration;

public partial class VSS_OPT_OPT05_OPT05 : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {

        if (!Page.IsPostBack)
        {
            if (logMsg.STORENO != StringUtil.CStr(System.Web.Configuration.WebConfigurationManager.AppSettings["DefaultStore"]))
            {
                ScriptManager.RegisterClientScriptBlock(this, GetType(), "AlertMessage", "alert('非總部人員無法使用此功能!!');", true);
                gvMaster.Enabled = false;
                btnSearch.Enabled = false;
                btnClear.Enabled = false;
                txtStoreNo.Enabled = false;
                txtStoreName.Enabled = false;
                ddlUSE_TYPE.Enabled = false;
                txtSDate.Enabled = false;
                txtEDate.Enabled = false;
                return;
            }
            

            //Bind狀態選單
            testStore.Value = logMsg.STORENO;
            //ddlUSE_TYPE.Items.Insert(0, new ListEditItem("ALL", null));
            //ddlUSE_TYPE.SelectedIndex = 0;

            //取得空的資料表(只有結構描述)
            gvMaster.DataSource = new OPT05_HqInvoiceAssign_DTO().HQ_INVOICE_ASSIGN;
            gvMaster.DataBind();
        }
    }

    /// <summary>
    /// 繫結狀態選單
    /// </summary>
    /// <param name="ddlUSE_TYPE"></param>
    private void bindUSE_TYPE(ASPxComboBox ddlUSE_TYPE)
    {
        //throw new NotImplementedException();
        ddlUSE_TYPE.Width = 50;
        OPT05_PageHelper PageHelper = new OPT05_PageHelper();
        ddlUSE_TYPE.TextField = PageHelper.USE_TYPE_TextField;
        ddlUSE_TYPE.ValueField = PageHelper.USE_TYPE_ValueField;
        ddlUSE_TYPE.DataSource = PageHelper.GetUSE_TYPEs();
        ddlUSE_TYPE.DataBind();
    }

    protected void bindMasterData()
    {
        string TYPEE = "";
        if(ddlUSE_TYPE.Value == null)
        {
            TYPEE="";
        }
        else
        {
            TYPEE = StringUtil.CStr(ddlUSE_TYPE.Value);
        }
        gvMaster.DataSource = new OPT05_PageHelper().GetHeadQuarterInvoiceHQ(
            txtStoreNo.Text, 
            txtStoreName.Text, 
            TYPEE, 
            txtSDate.Text, 
            txtEDate.Text);
        gvMaster.DataBind();
      
        gvMaster.FocusedRowIndex = -1;
        gvMaster.Selection.UnselectAll();
        gvMaster.CancelEdit();
        gvDetail.Visible = false;
    }

    #region Button 觸發事件

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        bindMasterData();
        gvMaster.PageIndex = 0;
    }

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        //if (gvMaster.Selection.Count < 0) { return; }

        //DataTable dt = new DataTable("HQ_INVOICE_ASSIGN");
        //dt.Columns.Add("ASSIGN_ID");

        //foreach (object key in gvMaster.GetSelectedFieldValues(gvMaster.KeyFieldName))
        //{
        //    DataRow dr = dt.NewRow();
        //    dr[0] = key;
        //    dt.Rows.Add(dr);
        //}

        //dt.AcceptChanges();

        //try
        //{
        //    new OPT05_Facade().DeleteHeadQuarterInvoiceData(dt);
        //}
        //catch (Exception ex)
        //{
        //    throw ex;
        //}
        new OPT05_Facade().DeleteTakeOff_MData(this.gvMaster.GetSelectedFieldValues(gvMaster.KeyFieldName));
        bindMasterData();
    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        this.gvMaster.FocusedRowIndex = -1;
        this.gvMaster.Selection.UnselectAll();
        gvMaster.AddNewRow();
        if (gvDetail.Visible) gvDetail.Visible = false;

    }

    #endregion

    #region gvMaster 觸發事件

    protected void gvMaster_HtmlRowPrepared(object sender, ASPxGridViewTableRowEventArgs e)
    {
        e.Row.Attributes["canSelect"] = "true";
        if (e.RowType == GridViewRowType.Data)
        {
            string date = StringUtil.CStr(gvMaster.GetRowValues(e.VisibleIndex, "S_USE_YM"));
            string storeno = StringUtil.CStr(gvMaster.GetRowValues(e.VisibleIndex, "STORE_NO"));
            string USE_TYPE = StringUtil.CStr(gvMaster.GetRowValues(e.VisibleIndex, "USE_TYPE"));
            string CURRENT_NO = StringUtil.CStr(gvMaster.GetRowValues(e.VisibleIndex, "CURRENT_NO"));
            string LEADER_CODE = StringUtil.CStr(gvMaster.GetRowValues(e.VisibleIndex, "LEADER_CODE"));
            string INIT_NO = StringUtil.CStr(gvMaster.GetRowValues(e.VisibleIndex, "INIT_NO"));
            string strS_USE_YM = StringUtil.CStr(gvMaster.GetRowValues(e.VisibleIndex, "S_USE_YM"));
            string strE_USE_YM = StringUtil.CStr(gvMaster.GetRowValues(e.VisibleIndex, "E_USE_YM"));

            int s_use_m = Convert.ToInt32((date.Substring(5, 2)));
            int s_use_y = Convert.ToInt32((date.Substring(0, 4)));
            string yyy = StringUtil.CStr(DateTime.Now.Year);
            string xxx = StringUtil.CStr(DateTime.Now.Month);
            if (s_use_y < Convert.ToInt32(yyy))
            {
                e.Row.Attributes["canSelect"] = "false";      
            }

            if (s_use_m < Convert.ToInt32(xxx))
            {
                e.Row.Attributes["canSelect"] = "false";
            }

            if (logMsg.STORENO == null)
            { 
                e.Row.Attributes["canSelect"] = "false";
            }

            if (logMsg.STORENO != StringUtil.CStr(System.Web.Configuration.WebConfigurationManager.AppSettings["DefaultStore"]))
            {
                e.Row.Attributes["canSelect"] = "false";
            }

            //手開資料不允許勾選
            if (USE_TYPE == "3")
            {
                e.Row.Attributes["canSelect"] = "false";
            }

            if (s_use_m == Convert.ToInt32(xxx) &&  !string.IsNullOrEmpty(CURRENT_NO))  //當月的資料只要沒有【目前編號】還是可以刪除、編輯
            {
                e.Row.Attributes["canSelect"] = "false";
            }

            if (OPT05_Facade.CheckInvoiceNOIsUsed(LEADER_CODE, strS_USE_YM, strE_USE_YM, INIT_NO))  // 已展發票不允許刪除
            {
                e.Row.Attributes["canSelect"] = "false";
            }

        }
    }

    protected void gvMaster_PageIndexChanged(object sender, EventArgs e)
    {
        bindMasterData();
    }

    protected void gvMaster_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
    {
        OPT05_HqInvoiceAssign_DTO ds = new OPT05_HqInvoiceAssign_DTO();

        OPT05_HqInvoiceAssign_DTO.HQ_INVOICE_ASSIGNDataTable dt = ds.HQ_INVOICE_ASSIGN;

        OPT05_HqInvoiceAssign_DTO.HQ_INVOICE_ASSIGNRow dr = dt.NewHQ_INVOICE_ASSIGNRow();

        dr["ASSIGN_ID"] = GuidNo.getUUID();
        dr["USE_TYPE"] = e.NewValues["USE_TYPE"];

        if ("1" == StringUtil.CStr(e.NewValues["USE_TYPE"]))
        {
            dr["INVOICE_TYPE_ID"] = "01";
        }
        else
        {
            dr["INVOICE_TYPE_ID"] = "02";
        }

        dr["S_USE_YM"] = Convert.ToDateTime(e.NewValues["S_USE_YM"]).ToString("yyyy/MM");
        dr["E_USE_YM"] = Convert.ToDateTime(e.NewValues["E_USE_YM"]).ToString("yyyy/MM");
        dr["LEADER_CODE"] = e.NewValues["LEADER_CODE"];
        dr["INIT_NO"] = e.NewValues["INIT_NO"];
        dr["END_NO"] = e.NewValues["END_NO"];
        dr["SHEET_COUNT"] = Convert.ToDecimal(e.NewValues["END_NO"]) -Convert.ToDecimal(e.NewValues["INIT_NO"]) + 1;
        dr["STORE_NO"] = StringUtil.CStr(e.NewValues["STORE_NO"]).ToUpper();

        dr["MODI_USER"] = logMsg.MODI_USER;
        dr["MODI_DTM"] = DateTime.Now;
        
        dr["CREATE_USER"] = dr["MODI_USER"];
        dr["CREATE_DTM"] = dr["MODI_DTM"];
        dr["STATUS"] = "C";
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

        ((ASPxGridView)sender).CancelEdit();
        e.Cancel = true;

        bindMasterData();
    }

    protected void gvMaster_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
    {
        OPT05_HqInvoiceAssign_DTO ds = new OPT05_HqInvoiceAssign_DTO();

        OPT05_HqInvoiceAssign_DTO.HQ_INVOICE_ASSIGNDataTable dt = ds.HQ_INVOICE_ASSIGN;

        dt.Columns["CREATE_USER"].AllowDBNull = true;
        dt.Columns["CREATE_DTM"].AllowDBNull = true;

        OPT05_HqInvoiceAssign_DTO.HQ_INVOICE_ASSIGNRow dr = dt.NewHQ_INVOICE_ASSIGNRow();

        dr["ASSIGN_ID"] = e.Keys["ASSIGN_ID"];
        dr["USE_TYPE"] = e.NewValues["USE_TYPE"];

        if ("1" == StringUtil.CStr(e.NewValues["USE_TYPE"]))
        {
            dr["INVOICE_TYPE_ID"] = "01";
        }
        else
        {
            dr["INVOICE_TYPE_ID"] = "02";
        }

        dr["S_USE_YM"] = Convert.ToDateTime(e.NewValues["S_USE_YM"]).ToString("yyyy/MM");
        dr["E_USE_YM"] = Convert.ToDateTime(e.NewValues["E_USE_YM"]).ToString("yyyy/MM");
        dr["LEADER_CODE"] = e.NewValues["LEADER_CODE"];
        dr["INIT_NO"] = e.NewValues["INIT_NO"];
        dr["END_NO"] = e.NewValues["END_NO"];
        dr["SHEET_COUNT"] = Convert.ToDecimal(e.NewValues["END_NO"]) - Convert.ToDecimal(e.NewValues["INIT_NO"]) + 1;
        dr["STORE_NO"] = StringUtil.CStr(e.NewValues["STORE_NO"]).ToUpper();

        dr["MODI_USER"] = logMsg.MODI_USER;
        dr["MODI_DTM"] = DateTime.Now;

        dt.AddHQ_INVOICE_ASSIGNRow(dr);

        ds.AcceptChanges();

        new OPT05_Facade().UpdateHeadQuarterInvoiceData(ds);
        ((ASPxGridView)sender).CancelEdit();
        e.Cancel = true;

        bindMasterData();
    }

    protected void gvMaster_HtmlDataCellPrepared(object sender, ASPxGridViewTableDataCellEventArgs e)
    {
        DevExpress.Web.ASPxGridView.GridViewDataTextColumn col = new GridViewDataTextColumn();
        DevExpress.Web.ASPxGridView.GridViewDataComboBoxColumn co2 = new GridViewDataComboBoxColumn();
        GridViewCommandColumn ccc = new GridViewCommandColumn();
        col = (GridViewDataTextColumn)((ASPxGridView)sender).Columns["門市編號"];
        co2 = (GridViewDataComboBoxColumn)((ASPxGridView)sender).Columns["用途"];
        LinkButton hl = gvMaster.FindRowCellTemplateControl(e.VisibleIndex, col, "Label2") as LinkButton;
        LinkButton hl1 = gvMaster.FindRowCellTemplateControl(e.VisibleIndex, col, "Label1") as LinkButton;
        Label R1 = gvMaster.FindRowCellTemplateControl(e.VisibleIndex, col, "test") as Label;
        Label rb = gvMaster.FindRowCellTemplateControl(e.VisibleIndex, co2, "Label2") as Label;

        if (hl1 != null)
        {
            if (rb.Text == "離線")
            {
                R1.Visible = false;
            }
            else
            {
                hl1.Visible = false;
            }
        }
        //if (hl != null)
        //{
        //    //if (rb.Text == "離線")
        //    //{
        //    //    hl.Enabled = true;
        //    //}
        //    //else
        //    //    hl.Enabled = false;
        //}
        
    }

    protected void gvMaster_CellEditorInitialize(object sender, ASPxGridViewEditorEventArgs e)
    {
        if (e.Column.FieldName == "USE_TYPE")
        {
            bindUSE_TYPE(e.Editor as ASPxComboBox);
        }
    }

    protected void gvMaster_CommandButtonInitialize(object sender, ASPxGridViewCommandButtonEventArgs e)
    {
        if (e.VisibleIndex > -1)
        {
            if (e.ButtonType == ColumnCommandButtonType.Edit || e.ButtonType == ColumnCommandButtonType.SelectCheckbox)
            {
                string status1 = StringUtil.CStr(gvMaster.GetRowValues(e.VisibleIndex, "CAN_EDIT"));
                string status2 = StringUtil.CStr(gvMaster.GetRowValues(e.VisibleIndex, "STORE_NO"));
                DateTime S_USE_YM = Convert.ToDateTime(StringUtil.CStr(gvMaster.GetRowValues(e.VisibleIndex, "S_USE_YM")) + "/01");
                DateTime sysMonth = Convert.ToDateTime(DateTime.Today.ToString("yyyy/MM") + "/01");
                string USE_TYPE = StringUtil.CStr(gvMaster.GetRowValues(e.VisibleIndex, "USE_TYPE"));
                string CURRENT_NO = StringUtil.CStr(gvMaster.GetRowValues(e.VisibleIndex, "CURRENT_NO"));
                string LEADER_CODE = StringUtil.CStr(gvMaster.GetRowValues(e.VisibleIndex, "LEADER_CODE"));
                string INIT_NO = StringUtil.CStr(gvMaster.GetRowValues(e.VisibleIndex, "INIT_NO"));
                string strS_USE_YM = StringUtil.CStr(gvMaster.GetRowValues(e.VisibleIndex, "S_USE_YM"));
                string strE_USE_YM = StringUtil.CStr(gvMaster.GetRowValues(e.VisibleIndex, "E_USE_YM"));

                if (logMsg.STORENO != StringUtil.CStr(System.Web.Configuration.WebConfigurationManager.AppSettings["DefaultStore"]))
                {
                    e.Enabled = false;
                }

                if ((status1 == "0" && S_USE_YM < sysMonth) || !string.IsNullOrEmpty(CURRENT_NO))  //當月的資料只要沒有【目前編號】還是可以刪除、編輯
                {
                    e.Enabled = false;
                }
                else
                {
                    // 已展發票不允許刪除
                    if (e.ButtonType == ColumnCommandButtonType.SelectCheckbox
                        && OPT05_Facade.CheckInvoiceNOIsUsed(LEADER_CODE, strS_USE_YM, strE_USE_YM, INIT_NO))
                        e.Enabled = false;
                    else
                        e.Enabled = true;

                }

                //手開資料不允許編輯及勾選
                if (USE_TYPE == "3")
                {
                    e.Enabled = false;
                }

            }
        }

    }

    protected void gvMaster_RowValidating(object sender, DevExpress.Web.Data.ASPxDataValidationEventArgs e)
    {

        if (StringUtil.CStr(e.NewValues["STORE_NO"]) == "")
        {
            e.RowError = "請輸入門市編號。";
            return;
        }

        if (e.NewValues["S_USE_YM"] == null || StringUtil.CStr(e.NewValues["S_USE_YM"]) == "" || e.NewValues["E_USE_YM"] == null || StringUtil.CStr(e.NewValues["E_USE_YM"]) == "")
        {
            e.RowError = "請輸入起訖年月";
            return;
        }

        string test = StringUtil.CStr(e.NewValues["S_USE_YM"]);
        int s_use_m = Convert.ToInt32((StringUtil.CStr(e.NewValues["S_USE_YM"]).Substring(5, 2)));
        int e_use_m = Convert.ToInt32((StringUtil.CStr(e.NewValues["E_USE_YM"]).Substring(5, 2)));
        int s_use_y = Convert.ToInt32((StringUtil.CStr(e.NewValues["S_USE_YM"]).Substring(0, 4)));
        int e_use_y = Convert.ToInt32((StringUtil.CStr(e.NewValues["E_USE_YM"]).Substring(0, 4)));

        if (e_use_m - s_use_m != 1 ||
            s_use_m % 2 != 1 ||
            e_use_m % 2 != 0)
        {
            e.RowError = "所屬年月設定錯誤。";
            return;
        }

        string yyy = StringUtil.CStr(DateTime.Now.Year);
        string xxx = StringUtil.CStr(DateTime.Now.Month);

        if (s_use_y < Convert.ToInt32(yyy) || e_use_y < Convert.ToInt32(yyy))
        {
            e.RowError = "年度不可小於系統年度。";
            return;
        }
        if (s_use_y != e_use_y)
        {
            e.RowError = "訖年度須等於起始年度。";
            return;
        }

        if (s_use_y > e_use_y)
        {
            e.RowError = "訖年度不可小於起始年度";
            return;
        }

        if (s_use_y == Convert.ToInt32(yyy))
        {
            if (s_use_m < Convert.ToInt32(xxx) || e_use_m < Convert.ToInt32(xxx))
            {
                if (s_use_m != Convert.ToInt32(xxx) && e_use_m != Convert.ToInt32(xxx))
                {
                    e.RowError = "月份不可小於系統月份。";
                    return;
                }
            }
        }

        if (s_use_m > e_use_m)
        {
            e.RowError = "訖月份不可小於起始月份";
            return;
        }

        if ((Convert.ToInt32(e.NewValues["END_NO"]) - Convert.ToInt32(e.NewValues["INIT_NO"])) <= 0)
        {
            e.RowError = "編號設定錯誤。";
            return;
        }

        int noValue  = (Convert.ToInt32(e.NewValues["END_NO"]) - Convert.ToInt32(e.NewValues["INIT_NO"])) + 1;
        //**2011/04/13 Bill： 發票不得超過二萬張
        if (noValue > 30000)
        {
            e.RowError = "發票張數不得超過三萬張。";
            return;
        }
        //**2011/04/13 Bill： end - star + 1 包涵起始號碼 必需是十的倍數
        if ((noValue % 10) != 0)
        {
            e.RowError = "發票張數須設定為10張的倍數。";
            return;
        }

        string Key = "";
        if (e.Keys["ASSIGN_ID"] != null)
        {
            Key = StringUtil.CStr(e.Keys["ASSIGN_ID"]);
        }
        if (StringUtil.CStr(e.NewValues["LEADER_CODE"]).Length != 2)
        {
            e.RowError = "字軌必須為兩碼";
            return;
        }

        if (e.IsNewRow)
        {
            if (OPT05_Facade.CheckInvoiceNO_New(
                Key,
              StringUtil.CStr(e.NewValues["STORE_NO"]),
              StringUtil.CStr(e.NewValues["USE_TYPE"]),
              StringUtil.CStr(e.NewValues["LEADER_CODE"]),
              Convert.ToDateTime(e.NewValues["S_USE_YM"]).ToString("yyyy/MM"),
              Convert.ToDateTime(e.NewValues["E_USE_YM"]).ToString("yyyy/MM"),
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
       
            if (OPT05_Facade.CheckInvoiceNO_UPD(
                Key,
              StringUtil.CStr(e.NewValues["STORE_NO"]),
              StringUtil.CStr(e.NewValues["USE_TYPE"]),
              StringUtil.CStr(e.NewValues["LEADER_CODE"]),
              Convert.ToDateTime(e.NewValues["S_USE_YM"]).ToString("yyyy/MM"),
              Convert.ToDateTime(e.NewValues["E_USE_YM"]).ToString("yyyy/MM"),
              StringUtil.CStr(e.NewValues["INIT_NO"]),
              StringUtil.CStr(e.NewValues["END_NO"])
              ))
            {
                e.RowError = "同一所屬年度區間中不可有二筆以上的相同字軌、重複編號區間的發票設定。";
                return;
            }
        }

    }

    protected void gvMaster_RowCommand(object sender, ASPxGridViewRowCommandEventArgs e)
    {
        if (!gvMaster.IsEditing)
        {
            this.gvMaster.Selection.UnselectAll();
            if ("2" == StringUtil.CStr(gvMaster.GetRowValues(gvMaster.FocusedRowIndex, "USE_TYPE")))
            {
                gvDetail.DataSource = new OPT05_PageHelper().GetStoreMachineInvoiceData(
                    StringUtil.CStr(gvMaster.GetRowValues(gvMaster.FocusedRowIndex, "ASSIGN_ID"))
                    , StringUtil.CStr(gvMaster.GetRowValues(gvMaster.FocusedRowIndex, "STORE_NO"))
                );
                gvDetail.DataBind();
                gvDetail.Visible = true;
            }
            else
            {
                gvDetail.Visible = false;
            }


        }
    }

    protected void gvMaster_StartRowEditing(object sender, DevExpress.Web.Data.ASPxStartRowEditingEventArgs e)
    {
        this.gvMaster.Selection.UnselectAll();
        if (gvDetail.Visible) gvDetail.Visible = false;
    }

    #endregion

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

}
