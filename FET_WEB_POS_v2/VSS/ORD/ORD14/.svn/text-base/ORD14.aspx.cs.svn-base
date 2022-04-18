using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using DevExpress.Web.ASPxGridView;
using DevExpress.Web.ASPxEditors;
using System.Data;

using Advtek.Utility;
using FET.POS.Model.DTO;
using FET.POS.Model.Facade;
using System.Web.UI.HtmlControls;
using FET.POS.Model.Facade.FacadeImpl;
using NPOI.HSSF.UserModel;
using FET.POS.Model.Common;

public partial class VSS_ORD_ORD14_ORD14 : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack && !IsCallback)
        {
            ORD14_Facade ORD14_Facade = new ORD14_Facade();

            DataTable dt = new DataTable();
            dt = ORD14_Facade.TopDatatable1("", "", true);
            gvMaster.DataSource = dt;
            gvMaster.DataBind();
            
            this.ASPxPageControl1.Visible = false;

            gvMaster.FocusedRowIndex = -1;

        }

    }

    private void cancelMaster() 
    {
        gvMaster.Selection.UnselectAll();
        gvMaster.CancelEdit();
        
    }

    private void cancelDetail() 
    {
        gvDetail.Selection.UnselectAll();
        gvDetail.CancelEdit();
        gvDetail.Selection.UnselectAll();
    }

    private void bindMasterData()
    {
        gvMaster.DataSource = new ORD14_Facade().TopDatatable1(ASPxTextBox1.Text, ASPxTextBox3.Text, false);
        gvMaster.DataBind();
        gvMaster.Selection.UnselectAll();
    }

    private void bindDetailData(bool bInitial)
    {
        if (gvMaster.FocusedRowIndex > -1)
        {
            this.gvDetail.DataSource = getGridViewData(bInitial);
            this.gvDetail.DataBind();

            //DateTime SEAL_OFF_DATE = Convert.ToDateTime(gvMaster.GetRowValues(gvMaster.FocusedRowIndex, "S_DATE"));
            //DateTime E_Date = Advtek.Utility.DateUtil.NullDateFormat(gvMaster.GetRowValues(gvMaster.FocusedRowIndex, "E_DATE"));

            string STATUS = StringUtil.CStr(gvMaster.GetRowValues(gvMaster.FocusedRowIndex, "STATUS"));
            ASPxButton btnNew = (ASPxButton)gvDetail.FindTitleTemplateControl("btnNew");
            ASPxButton btnDelete = (ASPxButton)gvDetail.FindTitleTemplateControl("btnDelete");

            //4.2.2.4.2	已生效(拆封日期≦系統日≦展示訖日)之資料列，僅展示訖日可以修改，明細不允許編輯
            //2011/03/31 Tina：SA文件後來新增 4.3.2.1 若主檔為已生效之資料列，可新增商品料號，不可修改、刪除已存在之商品料號。
            //if ((SEAL_OFF_DATE <= DateTime.Now && DateTime.Now <= E_Date) || E_Date < DateTime.Now)
            if (STATUS == "已過期")
            {
                gvDetail.Enabled = false;
            }
            else
            {
                gvDetail.Enabled = true;
            }

            gvDetail.PagerBarEnabled = true;

        }
    }

    private DataTable getGridViewData(bool bInitial)
    {
        string key = "";
        if (!bInitial)
        {
            key = StringUtil.CStr(gvMaster.GetRowValues(gvMaster.FocusedRowIndex, "SIM_GROUP_ID"));
        }
        else { 
            key = ""; 
        }

        return new ORD14_Facade().GetTakeOff_DData(key);
    }

    #region Button 觸發事件

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        ORD14_Facade Facade = new ORD14_Facade();

        bindMasterData();
        gvMaster.FocusedRowIndex = -1;
        gvMaster.PageIndex = 0;

        this.ASPxPageControl1.Visible = false;
        bindDetailData(true);
        gvDetail.Selection.UnselectAll();
        
    }

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        if (this.gvMaster.GetSelectedFieldValues(gvMaster.KeyFieldName).Count != 0) 
        {
            new ORD14_Facade().DeleteTakeOff_MData(this.gvMaster.GetSelectedFieldValues(gvMaster.KeyFieldName));
            bindMasterData();
            gvMaster.FocusedRowIndex = -1;
            gvMaster_FocusedRowChanged(gvMaster, new EventArgs());

            this.ASPxPageControl1.Visible = false;
        }
        
    }

    protected void Button4_Click(object sender, EventArgs e)
    {
        new ORD14_Facade().DeleteTakeOff_DData(this.gvDetail.GetSelectedFieldValues(gvDetail.KeyFieldName));
        gvMaster_FocusedRowChanged(gvMaster, new EventArgs());
    }

    protected void btnNew_Click(object sender, EventArgs e)
    {
        gvMaster.AddNewRow();
        this.ASPxPageControl1.Visible = false;
    }
    #endregion

    #region gvMaster 觸發的事件

    protected void gvMaster_HtmlRowPrepared(object sender, ASPxGridViewTableRowEventArgs e)
    {
        if (e.RowType == GridViewRowType.Data)
        {
            e.Row.Attributes["canSelect"] = "true";

            //DateTime S_DATE = Convert.ToDateTime(StringUtil.CStr(gvMaster.GetRowValues(e.VisibleIndex, "S_DATE")));
            //DateTime E_DATE = Convert.ToDateTime(string.IsNullOrEmpty(StringUtil.CStr(gvMaster.GetRowValues(e.VisibleIndex, "E_DATE"))) ? "9999/12/31" : StringUtil.CStr(gvMaster.GetRowValues(e.VisibleIndex, "E_DATE")));

            string STATUS = StringUtil.CStr(gvMaster.GetRowValues(e.VisibleIndex, "STATUS"));

            //if (S_DATE.ToString("yyyy/MM/dd").CompareTo(DateTime.Now.ToString("yyyy/MM/dd")) < 0 || E_DATE.ToString("yyyy/MM/dd").CompareTo(DateTime.Now.ToString("yyyy/MM/dd")) < 0)
            if (STATUS == "有效" || STATUS == "已過期")
            {
                e.Row.Attributes["canSelect"] = "false";
            }
        }
    }

    protected void gvMaster_FocusedRowChanged(object sender, EventArgs e)
    {
        if (!IsPostBack && !IsCallback)
        {
            gvMaster.FocusedRowIndex = -1;
        }
        else
        {
            if (gvMaster.FocusedRowIndex > -1)
            {
                ASPxGridView gv = sender as ASPxGridView;
                this.ASPxPageControl1.Visible = false;
                this.ASPxPageControl1.Visible = true;
                gvDetail.Visible = true;
                bindDetailData(false);
                gvDetail.CancelEdit();
                gvDetail.Selection.UnselectAll();
                gvDetail.PageIndex = 0;
            }
        }
    }

    protected void gvMaster_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
    {
        e.NewValues.Add("SIM_GROUP_ID", StringUtil.CStr(e.Keys[0]));
        new ORD14_Facade().UpdateTakeOff_MData(e.NewValues,logMsg.MODI_USER);
        ((ASPxGridView)sender).CancelEdit();
        bindMasterData();
        e.Cancel = true;
    }

    protected void gvMaster_CommandButtonInitialize(object sender, ASPxGridViewCommandButtonEventArgs e)
    {
        if (e.VisibleIndex > -1)
        {
            if (e.ButtonType == ColumnCommandButtonType.Edit || e.ButtonType == ColumnCommandButtonType.SelectCheckbox)
            {
                string STATUS = StringUtil.CStr(gvMaster.GetRowValues(e.VisibleIndex, "STATUS"));

                if (STATUS == "已過期")
                {
                    e.Enabled = false;
                }

                if (e.ButtonType == ColumnCommandButtonType.SelectCheckbox)
                {
                    if (STATUS == "有效")
                    {
                        e.Enabled = false;
                    }
                }

                //if (e.ButtonType == ColumnCommandButtonType.Edit)
                //{
                //        e.Enabled = true;
                //}
                
                //DateTime S_DATE = Convert.ToDateTime(StringUtil.CStr(gvMaster.GetRowValues(e.VisibleIndex, "S_DATE")));
                //if (StringUtil.CStr(gvMaster.GetRowValues(e.VisibleIndex, "E_DATE")) == "")
                //{
                //    DateTime E_DATE = DateUtil.NullDateFormat(null);

                //    if (E_DATE.ToString("yyyy/MM/dd").CompareTo(DateTime.Now.ToString("yyyy/MM/dd")) < 0 )
                //    {
                //        if (e.ButtonType == ColumnCommandButtonType.Edit)
                //            e.Enabled = false;
                //    }


                //    if (S_DATE.ToString("yyyy/MM/dd").CompareTo(DateTime.Now.ToString("yyyy/MM/dd")) < 0)
                //    {
                //        if (e.ButtonType == ColumnCommandButtonType.SelectCheckbox)
                //        {
                //            e.Enabled = false;
                //        }
                //    }
                //}
                //else
                //{
                //    DateTime E_DATE = Convert.ToDateTime(StringUtil.CStr(gvMaster.GetRowValues(e.VisibleIndex, "E_DATE")));
                //    if (E_DATE.ToString("yyyy/MM/dd").CompareTo(DateTime.Now.ToString("yyyy/MM/dd")) < 0)
                //    {
                //        if (e.ButtonType == ColumnCommandButtonType.Edit)
                //            e.Enabled = false;
                //    }
                //    if (S_DATE.ToString("yyyy/MM/dd").CompareTo(DateTime.Now.ToString("yyyy/MM/dd")) < 0)
                //    {
                //        if (e.ButtonType == ColumnCommandButtonType.SelectCheckbox)
                //        {
                //            e.Enabled = false;
                //        }
                //    }
                //}
            }
        }
    }

    protected void gvMaster_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
    {
        new ORD14_Facade().AddDatatable2(e.NewValues, logMsg.CREATE_USER);
        DataTable dt = new DataTable();
        ORD14_Facade Facade = new ORD14_Facade();
        dt = Facade.TopDatatable1("", "", false);
        gvMaster.DataSource = dt;
        gvMaster.DataBind();
        ((ASPxGridView)sender).CancelEdit();
        e.Cancel = true;
        gvMaster.Selection.UnselectAll();
        gvMaster.FocusedRowIndex = -1;
    }

    protected void gvMaster_RowValidating(object sender, DevExpress.Web.Data.ASPxDataValidationEventArgs e)
    {
        ORD14_Facade Facade = new ORD14_Facade();
        bool hasError = false;
        if (e.NewValues["S_DATE"] == null)
        {
            e.RowError += "開始日期不允許為空白，請重新輸入;";
            hasError = true;
        }
        if(e.NewValues["SIM_GROUP_NAME"]==null)
        {
            e.RowError += "卡片群組不允許為空白，請重新輸入;";
            hasError = true;
        }

        if(!hasError)
        {
            try
            {
                string sS_DATE = Convert.ToDateTime(StringUtil.CStr(e.NewValues["S_DATE"])).ToString("yyyy/MM/dd");
                string sE_DATE = (e.NewValues["E_DATE"] == null) ? "" : Convert.ToDateTime(StringUtil.CStr(e.NewValues["E_DATE"])).ToString("yyyy/MM/dd");
                string sNow = DateTime.Now.ToString("yyyy/MM/dd");
                string sSIM_GROUP_NAME = StringUtil.CStr(e.NewValues["SIM_GROUP_NAME"]);
                if (gvMaster.IsNewRowEditing)
                {
                    if (sS_DATE.CompareTo(sNow) < 0)
                    {
                        e.RowError += "開始日期不允許小於系統日，請重新輸入;";
                    }
                    if (!string.IsNullOrEmpty(sE_DATE))
                    {
                        if (sE_DATE.CompareTo(sNow) < 0 || sE_DATE.CompareTo(sS_DATE) < 0)
                        {
                            e.RowError += "結束日期不允許小於系統日及開始日期，請重新輸入;";
                        }
                    }
                    if (Facade.GetTakeOff_GROUPData2(sS_DATE, sE_DATE, "", sSIM_GROUP_NAME) > 0)
                    {
                        e.RowError += "卡片群組已存在，請重新輸入;";
                    }

                }
                else if (gvMaster.IsEditing)
                {
                    if (sS_DATE.CompareTo(sNow) > 0)
                    {
                        if (sS_DATE.CompareTo(sNow) < 0)
                        {
                            e.RowError += "開始日期不允許小於系統日，請重新輸入;";
                        }
                    }
                    if (!string.IsNullOrEmpty(sE_DATE))
                    {
                        if (sE_DATE.CompareTo(sNow) < 0 || sE_DATE.CompareTo(sS_DATE) < 0 )
                        {
                            e.RowError += "結束日期不允許小於系統日及開始日期，請重新輸入;";
                        }
                    }
                    if (gvMaster.FocusedRowIndex > -1)
                    {
                        string pk = StringUtil.CStr(gvMaster.GetRowValues(gvMaster.FocusedRowIndex, gvMaster.KeyFieldName));
                        if (Facade.GetTakeOff_GROUPData2(sS_DATE, sE_DATE, pk, sSIM_GROUP_NAME) > 0)
                        {
                            e.RowError += "卡片群組已存在，請重新輸入;";
                        }
                    }
                    
                }

            }
            catch (Exception ex)
            {
                e.RowError += ex.Message;
            }
        }

    }

    protected void gvMaster_PageIndexChanged(object sender, EventArgs e)
    {
        cancelMaster();
        cancelDetail();

        this.ASPxPageControl1.Visible = false;

        ORD14_Facade Facade = new ORD14_Facade();

        string SIMNO = ASPxTextBox1.Text;
        string PRODNO = ASPxTextBox3.Text;
        DataTable dt = new DataTable();
        dt = Facade.TopDatatable1(SIMNO, PRODNO, false);
        gvMaster.DataSource = dt;
        gvMaster.DataBind();
        gvMaster.FocusedRowIndex = -1;
        gvMaster.Selection.UnselectAll();     

    }

    protected void gvMaster_StartRowEditing(object sender, DevExpress.Web.Data.ASPxStartRowEditingEventArgs e)
    {
        gvMaster.Selection.UnselectAll();
        gvDetail.CancelEdit();
        
        if (gvMaster.IsEditing)
        {
            
            string SIM_GROUP_ID = StringUtil.CStr(gvMaster.GetRowValuesByKeyValue(e.EditingKeyValue, "SIM_GROUP_ID"));

            DateTime S_DATE = Convert.ToDateTime(gvMaster.GetRowValuesByKeyValue(e.EditingKeyValue, "S_DATE"));

            GridViewDataDateColumn colE_DATE = (GridViewDataDateColumn)gvMaster.Columns["E_DATE"];
            GridViewDataDateColumn colS_DATE = (GridViewDataDateColumn)gvMaster.Columns["S_DATE"];
            GridViewDataTextColumn codSIMGROUP = (GridViewDataTextColumn)gvMaster.Columns["SIM_GROUP_NAME"];

            codSIMGROUP.ReadOnly = false;
            colS_DATE.ReadOnly = false;
            if (S_DATE <= DateTime.Now)
            {
                codSIMGROUP.ReadOnly = true;
                colS_DATE.ReadOnly = true;
            }
        
        }
    }

    protected void gvMaster_InitNewRow(object sender, DevExpress.Web.Data.ASPxDataInitNewRowEventArgs e)
    {
        gvMaster.Selection.UnselectAll();
        gvMaster.FocusedRowIndex = -1;
        gvDetail.CancelEdit();
        ((GridViewDataDateColumn)gvMaster.Columns["E_DATE"]).ReadOnly = false;
        ((GridViewDataDateColumn)gvMaster.Columns["S_DATE"]).ReadOnly = false;
        ((GridViewDataTextColumn)gvMaster.Columns["SIM_GROUP_NAME"]).ReadOnly = false;
        
    }

    protected void gvMaster_CellEditorInitialize(object sender, ASPxGridViewEditorEventArgs e)
    {
        GridViewDataDateColumn colS_DATE = (GridViewDataDateColumn)gvMaster.Columns["S_DATE"];
        GridViewDataDateColumn colE_DATE = (GridViewDataDateColumn)gvMaster.Columns["E_DATE"];
        colS_DATE.PropertiesDateEdit.MinDate = DateTime.Today;
        colE_DATE.PropertiesDateEdit.MinDate = DateTime.Today;

        if (e.Editor is ASPxTextBox)
        {
            if (e.Column.FieldName.CompareTo("SIM_GROUP_NAME") == 0)
            {
                (e.Editor as ASPxTextBox).MaxLength = 50;//卡片群組長度需限制
            }
            if (e.Editor is ASPxTextBox) 
            {
                e.Editor.ClientEnabled = true;
            }
            if (e.Editor is ASPxDateEdit)
            {
                e.Editor.ClientEnabled = true;
            }
        }
        if (gvMaster.IsEditing) 
        {
            if (!gvMaster.IsNewRowEditing) //EDIT
            {
                object temp = gvMaster.GetRowValues(gvMaster.FocusedRowIndex, "S_DATE");
                if (temp != null && !string.IsNullOrEmpty(StringUtil.CStr(temp))) 
                {
                    DateTime S_DATE = Convert.ToDateTime(StringUtil.CStr(temp));
                    bool flag = true;
                    if (S_DATE.ToString("yyyyMMdd").CompareTo(DateTime.Now.ToString("yyyyMMdd")) < 0)//尚未生效(生效日<今日)，已生效只可修改結束日
                    {
                        flag = false;
                    }
                    if (e.Editor is ASPxTextBox)
                    {
                        (e.Editor as ASPxTextBox).ClientEnabled = flag;
                    }
                    else if (e.Editor is ASPxDateEdit)
                    {
                        if (e.Column.FieldName.CompareTo("S_DATE") == 0) 
                        {
                            (e.Editor as ASPxDateEdit).ClientEnabled = flag;
                        }
                    }
                }
            }
        }
    }

    #endregion

    #region gvDetail 觸發的事件

    protected void gvDetail_HtmlRowPrepared(object sender, ASPxGridViewTableRowEventArgs e)
    {
        if (e.RowType == GridViewRowType.Data && gvMaster.FocusedRowIndex > -1)
        {
            e.Row.Attributes["canSelect"] = "true";

            DateTime S_DATE = Convert.ToDateTime(StringUtil.CStr(gvMaster.GetRowValues(gvMaster.FocusedRowIndex, "S_DATE")));
            if (S_DATE <= DateTime.Now)
            {
                e.Row.Attributes["canSelect"] = "false";
            }
        }
    }

    protected void gvDetail_StartRowEditing(object sender, DevExpress.Web.Data.ASPxStartRowEditingEventArgs e)
    {
        gvDetail.Selection.UnselectAll();
        gvMaster.CancelEdit();
    }

    protected void gvDetail_InitNewRow(object sender, DevExpress.Web.Data.ASPxDataInitNewRowEventArgs e)
    {
        gvDetail.Selection.UnselectAll();
        gvMaster.CancelEdit();
    }

    protected void gvDetail_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
    {
        ASPxGridView dgv = ((ASPxGridView)sender);
        e.NewValues["SIM_GROUP_ID"] = StringUtil.CStr(gvMaster.GetRowValues(gvMaster.FocusedRowIndex, "SIM_GROUP_ID"));
        new ORD14_Facade().ADDPROD(e.NewValues, logMsg.CREATE_USER);
        gvMaster_FocusedRowChanged(gvMaster, new EventArgs());
        gvDetail.Selection.UnselectAll();
        dgv.CancelEdit();
        e.Cancel = true;
        
    }

    protected void gvDetail_CommandButtonInitialize(object sender, ASPxGridViewCommandButtonEventArgs e)
    {
        if (e.VisibleIndex > -1)
        {
            if (e.ButtonType == ColumnCommandButtonType.Edit || e.ButtonType == ColumnCommandButtonType.SelectCheckbox)
            {
                if (gvMaster.FocusedRowIndex > -1) 
                {
                    ASPxButton btnNew = (ASPxButton)gvDetail.FindTitleTemplateControl("btnNew");
                    ASPxButton btnDelete = (ASPxButton)gvDetail.FindTitleTemplateControl("btnDelete");
                    
                    //DateTime S_DATE = Convert.ToDateTime(StringUtil.CStr(gvMaster.GetRowValues(gvMaster.FocusedRowIndex, "S_DATE")));
                    string STATUS = StringUtil.CStr(gvMaster.GetRowValues(gvMaster.FocusedRowIndex, "STATUS"));

                    //2011/03/31 Tina：SA文件後來新增 4.3.2.1 若主檔為已生效之資料列，可新增商品料號，不可修改、刪除已存在之商品料號。
                    //if (S_DATE <= DateTime.Now)
                    if (STATUS == "已過期")
                    {
                        e.Enabled = false;
                        btnNew.Enabled = false;
                        btnDelete.Enabled = false;
                    }
                    else if (STATUS == "有效")
                    {
                        e.Enabled = false;
                        btnNew.Enabled = true;
                        btnDelete.Enabled = false;
                    }
                    else
                    {
                        e.Enabled = true;
                        btnNew.Enabled = true;
                        btnDelete.Enabled = true;
                    }
                }
                
            }
        }
    }

    protected void gvDetail_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
    {
        e.NewValues.Add("SIM_GROUP_PROD", StringUtil.CStr(e.Keys[0]));
        new ORD14_Facade().UpdateTakeOff_DData(e.NewValues, "Kevin");
        ((ASPxGridView)sender).CancelEdit();
        gvDetail.DataSource = getGridViewData(false);
        gvDetail.DataBind();
        gvDetail.FocusedRowIndex = -1;
        e.Cancel = true;
        gvDetail.Selection.UnselectAll();

    }

    protected void gvDetail_RowValidating(object sender, DevExpress.Web.Data.ASPxDataValidationEventArgs e)
    {
        if (e.Keys.Count == 0) e.Keys.Add("SIM_GROUP_PROD", "");

        string SIM_GROUP_ID = StringUtil.CStr(gvMaster.GetRowValues(gvMaster.FocusedRowIndex, "SIM_GROUP_ID"));
        string S_DATE = StringUtil.CStr(gvMaster.GetRowValues(gvMaster.FocusedRowIndex, "S_DATE"));
        string E_DATE = StringUtil.CStr(gvMaster.GetRowValues(gvMaster.FocusedRowIndex, "E_DATE"));

        string SIM_GROUP_PROD = StringUtil.CStr(e.Keys["SIM_GROUP_PROD"]);

        if (gvDetail.IsNewRowEditing)
        {
            if (e.NewValues["PRODNO"] == null)
            {
                e.RowError += "商品料號不允許空值，請重新輸入;";
            }
            else if (e.NewValues["PRODNAME"] == null)
            { 
                e.RowError += "商品名稱不允許空值，請重新輸入;"; 
            }
            else
            {
                string PRODNO = StringUtil.CStr(e.NewValues["PRODNO"]);
                if (string.IsNullOrEmpty(PRODNO))
                {
                    e.RowError += "商品料號不允許空值，請重新輸入;";
                }
                else if (PRODNO.Length < 3) 
                {
                    e.RowError += "商品料號錯誤，請重新輸入;";
                }
                else
                {
                    //string check = test.Substring(0, 2);
                    //if (check != "20" && check != "21" && check != "25" && check != "26")
                    //{
                    //    e.RowError += "商品料號不允許設定，請重新輸入;";
                    //}


                    DataTable dt = new Product_Facade().Query_ProductInfo(PRODNO);
                    if (dt.Rows.Count <= 0)
                    {
                        e.RowError += "無此商品料號，請重新輸入;";

                    }

                    ORD14_Facade Facade = new ORD14_Facade();
                    DataTable dtx = Facade.GetTakeOff_DPData(SIM_GROUP_ID, PRODNO, SIM_GROUP_PROD);
                    if (dtx.Rows.Count > 0)
                    {
                        e.RowError += "此商品已存在，不可重複;";
                    }
                    else
                    {
                        dtx = Facade.CheckTakeOff_DPData(PRODNO, SIM_GROUP_ID, S_DATE, E_DATE);

                        if (dtx.Rows.Count > 0)
                        {
                            e.RowError += string.Format("此商品已存在於{0}群組，不可重複;", StringUtil.CStr(dtx.Rows[0][0]));
                        }
                    }
                }
                
            }
        }
        else
        {
            if (e.NewValues["PRODNO"] == null)
            {
                e.RowError += "商品料號不允許空值，請重新輸入;";
            }
            else if (e.NewValues["PRODNAME"] == null)
            { 
                e.RowError += "商品名稱不允許空值，請重新輸入;"; 
            }
            else
            {
                string PRODNO = StringUtil.CStr(e.NewValues["PRODNO"]);
                if (string.IsNullOrEmpty(PRODNO))
                {
                    e.RowError += "商品料號不允許空值，請重新輸入;";
                }
                else if (PRODNO.Length < 3)
                {
                    e.RowError += "商品料號錯誤，請重新輸入;";
                }
                else 
                {
                    //string check = test.Substring(0, 2);
                    //if (check != "20" && check != "21" && check != "25" && check != "26")
                    //{
                    //    e.RowError += "商品料號不允許設定，請重新輸入;";
                    //}

                    DataTable dt = new Product_Facade().Query_ProductInfo(PRODNO);
                    if (dt.Rows.Count <= 0)
                    {
                        e.RowError += "無此商品料號，請重新輸入;";

                    }

                    ORD14_Facade Facade = new ORD14_Facade();
                    DataTable dtx = new DataTable();
                    dtx = Facade.GetTakeOff_DPData(SIM_GROUP_ID, PRODNO, SIM_GROUP_PROD);
                    if (dtx.Rows.Count > 0)
                    {
                        e.RowError += "商品料號已存在，請重新輸入;";
                    }
                    else
                    {
                        dtx = Facade.CheckTakeOff_DPData(PRODNO, SIM_GROUP_ID, S_DATE, E_DATE);
                        if (dtx.Rows.Count > 0)
                        {
                            e.RowError += string.Format("此商品已存在於{0}群組，不可重複;", StringUtil.CStr(dtx.Rows[0][0]));
                        }
                    }
                }
                
            }   
        }
            
    }

    protected void gvDetail_PageIndexChanged(object sender, EventArgs e) 
    {
        cancelDetail();
        if (gvMaster.FocusedRowIndex >-1)
        {
            this.ASPxPageControl1.Visible = true;
            gvDetail.Visible = true;
            bindDetailData(false);
            gvDetail.FocusedRowIndex = -1;
            gvDetail.Selection.UnselectAll();
        }

    }

    #endregion

    [System.Web.Services.WebMethod()]
    [System.Web.Script.Services.ScriptMethod()]
    static public string getPRODINFO(string PRODNO)
    {

        DataTable dt = new Product_Facade().Query_VW_PRODUCT(PRODNO);
        string r = "";
        if (dt.Rows.Count > 0)
        {
            r = StringUtil.CStr(dt.Rows[0]["PRODNAME"]) ;
        }
        return r;
    }
}
