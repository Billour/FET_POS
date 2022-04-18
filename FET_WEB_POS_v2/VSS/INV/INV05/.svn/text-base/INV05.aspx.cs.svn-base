using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Collections;
using DevExpress.Web;
using DevExpress.Web.ASPxGridView;
using DevExpress.Web.ASPxPager;
using DevExpress.Web.ASPxEditors;

using Advtek.Utility;
using FET.POS.Model.Common;
using FET.POS.Model.DTO;
using FET.POS.Model.Facade;
using FET.POS.Model.Facade.FacadeImpl;
using System.Collections.Specialized;


public partial class VSS_INV_INV05 : BasePage
{
    private string msgStr = "";

    //退倉單號
    private string _RTNNO = "";
    //退倉單PK _RTNN_ID_UUID
    private static string _RTNN_ID_UUID;

    private string qDno
    {
        get
        {
            string Result = string.Empty;
            if (!string.IsNullOrEmpty(Request.QueryString["Param"]))
            {
                NameValueCollection qscoll = Utils.Param_UrlDecode(Request.QueryString["Param"]);
                foreach (string key in qscoll.AllKeys)
                {
                    if (key == "dno")
                    {
                        Result = string.Join(",", qscoll.GetValues(key));
                        break;
                    }
                }
            }
            return Result.Trim();
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {       
        if (!IsPostBack && !Page.IsCallback)
        {
            if (logMsg.STORENO != StringUtil.CStr(System.Configuration.ConfigurationManager.AppSettings["DefaultRoleHQ"]))
            {
                //彈跳視窗
                ScriptManager.RegisterClientScriptBlock(this, GetType(), "AlertMessage", "alert('非總部人員無法使用此功能!!');", true);
                this.btnSave.Enabled = false;
                this.btnQueryEdit.Enabled = false;
                this.btnCancel.Enabled = false;
                this.btnImport.Enabled = false;
                ASPxPopupControl1.Enabled = false;    //匯入Popup
                this.ASPxPageControl1.Enabled = false;
                ReceiptDate.Enabled = false;    //開單日期
                ReceiptDate.ControlStyle.BackColor = System.Drawing.Color.LightGray;
                RTNM_BDate.Enabled = false;     //退倉起日
                RTNM_BDate.ControlStyle.BackColor = System.Drawing.Color.LightGray;
                RTNM_EDate.Enabled = false;     //退倉訖日
                RTNM_EDate.ControlStyle.BackColor = System.Drawing.Color.LightGray;
                RETURN_REASON_CODE.Enabled = false;
                AFTER_PROCESS_CODE.Enabled = false;
                Remark1.Enabled = false;
                return;
            }
            else
            {
                //退倉原因GetRtRsCo
                bindDdlValTxt(RETURN_REASON_CODE, INV05_PageHelper.GetRtRsCo(true), "RETURN_REASON_CODE", "RETURN_DESCRIPTION");
                //後續處理GetAftProCo
                bindDdlValTxt(AFTER_PROCESS_CODE, INV05_PageHelper.GetAftProCo(true), "AFTER_PROCESS_CODE", "DESCRIPTION");

                aspComboBoxDefaultSetting();

                //區域別
                BindZoneType();

                //更新日期,更新人員
                ModifiedDate.Text = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");
                ModifiedBy.Text = logMsg.MODI_USER + " " + new Employee_Facade().GetEmpName(logMsg.MODI_USER);

                //開單日期
                ReceiptDate.Text = DateTime.Now.ToString("yyyy/MM/dd");

                if (string.IsNullOrEmpty(qDno) && this.lblError.Text.Trim() == "")
                {
                    this.lbRTNNo.Text = "";
                    RTNM_BDate.MinDate = DateTime.Today;
                    RTNM_EDate.MinDate = DateTime.Today;
                    RTNM_BDate.Text = DateTime.Today.AddDays(+1).ToString("yyyy/MM/dd");
                    RTNM_EDate.Text = DateTime.Today.AddDays(+1).ToString("yyyy/MM/dd");

                    //取得空的資料表
                    gvMaster.DataSource = getMasterEmptyData();
                    gvMaster.DataBind();
                    Session["gvMaster"] = gvMaster.DataSource;
                }
                else
                {
                    lbRTNNo.Text = qDno;

                    INV05_Facade INV05_Facade = new INV05_Facade();
                    DataTable dtt = INV05_Facade.GetRTNM(qDno);
                    foreach (DataRow xx in dtt.Rows)
                    {
                        CreateUser.Value = StringUtil.CStr(xx["CREATE_USER"]);
                        CreateDTM.Value = StringUtil.CStr(Convert.ToDateTime(xx["CREATE_DTM"]));
                    }
                    //重新bind資料-Master
                    Session["GetRTNM"] = INV05_Facade.GetRTNM(lbRTNNo.Text);
                    doSelectRTNM();

                    //重新bind資料-Detail
                    Session["GetRTNS"] = INV05_Facade.GetRTNS(_RTNN_ID_UUID);
                    Session["GetRTNP"] = INV05_Facade.GetRTNP(_RTNN_ID_UUID);
                    doSelectRTNS();
                    doSelectRTNP();

                    //50-以傳輸
                    if (Status1.Text.Trim() == "已傳輸")
                    {
                        this.btnSave.Enabled = false;
                        this.btnImport.ClientEnabled = false;
                    }

                }
            }

        }
        else
        {
            if (Request["__EVENTARGUMENT"] == "AAA")
            {
                if (Session["BATCH_NO"] != null)
                {
                    this.lblError.Text = StringUtil.CStr(Session["BATCH_NO"]);

                    INV05_Facade INV05_Facade = new INV05_Facade();
                    this.StoreList.Items.Clear();
                    //bind匯入之資料-Detail
                    Session["Get_UploadTemp_RTNS"] = INV05_Facade.Get_UploadTemp_RTNS(this.lblError.Text);
                    Session["Get_UploadTemp_RTNP"] = INV05_Facade.Get_UploadTemp_RTNP(this.lblError.Text);
                    doSelect_UploadTemp_RTNS();
                    doSelect_UploadTemp_RTNP();


                }
            }
        }
    }

    private void doSelectRTNM()
    {
        DataTable dtProd = (DataTable)Session["GetRTNM"];

        if (dtProd.Rows.Count > 0)
        {
            string xx = CreateDTM.Value.Substring(0, 10);
            lbRTNNo.Text = StringUtil.CStr(dtProd.Rows[0]["RTNNO"]);
            ReceiptDate.Text = StringUtil.CStr(dtProd.Rows[0]["CREATE_DTM1"]);
            Status1.Text = StringUtil.CStr(dtProd.Rows[0]["STATUS"]);
            RTNM_BDate.Text = StringUtil.CStr(dtProd.Rows[0]["B_DATE"]);
            RTNM_EDate.Text = StringUtil.CStr(dtProd.Rows[0]["E_DATE"]);
            ModifiedDate.Text = StringUtil.CStr(dtProd.Rows[0]["MODI_DTM"]);
            ModifiedBy.Text = StringUtil.CStr(dtProd.Rows[0]["MODI_USER"]) + " " + new Employee_Facade().GetEmpName(StringUtil.CStr(dtProd.Rows[0]["MODI_USER"]));
            Remark1.Text = StringUtil.CStr(dtProd.Rows[0]["REMARK"]);
            RETURN_REASON_CODE.Text = StringUtil.CStr(dtProd.Rows[0]["RETURN_DESCRIPTION"]);
            AFTER_PROCESS_CODE.Text = StringUtil.CStr(dtProd.Rows[0]["DESCRIPTION"]);
            _RTNN_ID_UUID = StringUtil.CStr(dtProd.Rows[0]["RTNN_ID"]);
            txtuuid.Text = StringUtil.CStr(dtProd.Rows[0]["RTNN_ID"]);
        }
    }

    private void doSelectRTNS()
    {
        DataTable dtSTORE = (DataTable)Session["GetRTNS"];

        if (dtSTORE.Rows.Count > 0)
        {

            for (int i = 0; i < dtSTORE.Rows.Count; i++)
            {
                this.StoreList.Items.Add(StringUtil.CStr(dtSTORE.Rows[i]["STORE_NO"]) + "-" + StringUtil.CStr(dtSTORE.Rows[i]["STORENAME"]));
            }
        }

    }

    private void doSelectRTNP()
    {
        DataTable dtRTNP = (DataTable)Session["GetRTNP"];

        if (dtRTNP.Rows.Count > 0)
        {
            DataTable dtResult = new DataTable();
            dtResult.Columns.Add("PRODNO", typeof(string));
            dtResult.Columns.Add("PRODNAME", typeof(string));

            foreach (DataRow dr in dtRTNP.Rows)
            {
                DataRow NewRow = dtResult.NewRow();
                NewRow["PRODNO"] = StringUtil.CStr(dr["PRODNO"]);
                NewRow["PRODNAME"] = StringUtil.CStr(dr["PRODNAME"]);
                dtResult.Rows.Add(NewRow);
            }

            Session["gvMaster"] = dtResult;
            gvMaster.DataSource = Session["gvMaster"];
            gvMaster.DataBind();
        }
    }

    //匯入之資料
    private void doSelect_UploadTemp_RTNS()
    {
        DataTable dtSTORE = (DataTable)Session["Get_UploadTemp_RTNS"];

        if (dtSTORE.Rows.Count > 0)
        {
            for (int i = 0; i < dtSTORE.Rows.Count; i++)
            {
                this.StoreList.Items.Add(StringUtil.CStr(dtSTORE.Rows[i]["STORE_NO"]) + "-" + StringUtil.CStr(dtSTORE.Rows[i]["STORENAME"]));
            }
        }

    }
    
    //匯入之資料
    private void doSelect_UploadTemp_RTNP()
    {
        DataTable dtRTNP = (DataTable)Session["Get_UploadTemp_RTNP"];

        if (dtRTNP.Rows.Count > 0)
        {
            DataTable dtResult = new DataTable();
            dtResult.Columns.Add("PRODNO", typeof(string));
            dtResult.Columns.Add("PRODNAME", typeof(string));

            foreach (DataRow dr in dtRTNP.Rows)
            {
                DataRow NewRow = dtResult.NewRow();
                NewRow["PRODNO"] = StringUtil.CStr(dr["PRODNO"]);
                NewRow["PRODNAME"] = StringUtil.CStr(dr["PRODNAME"]);
                dtResult.Rows.Add(NewRow);
            }

            Session["gvMaster"] = dtResult;
            gvMaster.DataSource = dtResult;
            gvMaster.DataBind();
        }
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
        if (RETURN_REASON_CODE.SelectedIndex == -1)
        {
            RETURN_REASON_CODE.SelectedIndex = 0;
        }

        if (AFTER_PROCESS_CODE.SelectedIndex == -1)
        {
            AFTER_PROCESS_CODE.SelectedIndex = 0;
        }

    }

    private DataTable getMasterEmptyData()
    {
        DataTable dtResult = new DataTable();
        dtResult.Columns.Add("PRODNO", typeof(string));
        dtResult.Columns.Add("PRODNAME", typeof(string));
        return dtResult;
    }

    private INV05_RTNM_DTO.RTNMDataTable doInsertRTNM()
    {
        INV05_RTNM_DTO _INV05_RTNM_DTO = new INV05_RTNM_DTO();
        INV05_RTNM_DTO.RTNMDataTable dtRTNM = null;

        //DateTime CREATEDTM = new DateTime();

        INV05_RTNM_DTO.RTNMRow drRTNM;

        dtRTNM = _INV05_RTNM_DTO.Tables["RTNM"] as INV05_RTNM_DTO.RTNMDataTable;
        drRTNM = dtRTNM.NewRTNMRow();
        //退倉單號
        if (lbRTNNo.Text.Trim() == "")
        { _RTNNO = SerialNo.GenNo("HR"); }
        else
        { _RTNNO = lbRTNNo.Text.Trim(); }
        //退倉單PK _RTNN_ID_UUID
        _RTNN_ID_UUID = GuidNo.getUUID();

        //異動的欄位
        drRTNM["RTNN_ID"] = _RTNN_ID_UUID;
        drRTNM["RTNNO"] = _RTNNO;

        if (RTNM_BDate.Text != null)
        { drRTNM["B_DATE"] = DateTime.Parse(RTNM_BDate.Text); }
        else
        { drRTNM["B_DATE"] = DateUtil.NullDateFormat(RTNM_BDate.Text); }

        if (RTNM_EDate.Text != null)
        {
            drRTNM["E_DATE"] = DateTime.Parse(RTNM_EDate.Text);
        }
        else
        { drRTNM["E_DATE"] = DateUtil.NullDateFormat(RTNM_EDate.Text); }

        //備註
        drRTNM["REMARK"] = Remark1.Text.Trim();
        drRTNM["AFTER_PROCESS_CODE"] = StringUtil.CStr(AFTER_PROCESS_CODE.SelectedItem.Value);
        drRTNM["RETURN_REASON_CODE"] = StringUtil.CStr(RETURN_REASON_CODE.SelectedItem.Value);
        drRTNM["STATUS"] = "10";

        drRTNM["MODI_USER"] = logMsg.MODI_USER;
        drRTNM["MODI_DTM"] = Convert.ToDateTime(System.DateTime.Now);
        if (CreateUser.Value == "")
        {
            CreateUser.Value = logMsg.MODI_USER;
        }
        drRTNM["CREATE_USER"] = CreateUser.Value;
        if (CreateDTM.Value == "")
        {
            CreateDTM.Value = StringUtil.CStr(Convert.ToDateTime(System.DateTime.Now));
        }
        drRTNM["CREATE_DTM"] = Convert.ToDateTime(CreateDTM.Value);

        dtRTNM.Rows.Add(drRTNM);

        return dtRTNM;
    }

    private INV05_RTNM_DTO.RTND_STOREDataTable doInsertStore()
    {
        INV05_RTNM_DTO.RTND_STOREDataTable dtRTNS = null;

        INV05_RTNM_DTO.RTND_STORERow drRTNS;

        dtRTNS = new INV05_RTNM_DTO.RTND_STOREDataTable();

        for (int i = 0; i < StoreList.Items.Count; i++)
        {
            drRTNS = dtRTNS.NewRTND_STORERow();
            drRTNS["STORE_NO"] = StringUtil.CStr(StoreList.Items[i]).Trim().Substring(0, StringUtil.CStr(StoreList.Items[i]).Trim().IndexOf('-'));
            drRTNS["RTNN_ID"] = _RTNN_ID_UUID;
            drRTNS["RTND_STORE_ID"] = GuidNo.getUUID();
            drRTNS["MODI_USER"] = logMsg.MODI_USER;
            drRTNS["MODI_DTM"] = Convert.ToDateTime(System.DateTime.Now);
            drRTNS["CREATE_USER"] = drRTNS["MODI_USER"];
            drRTNS["CREATE_DTM"] = drRTNS["MODI_DTM"];
            drRTNS["STATUS"] = "10";
            dtRTNS.Rows.Add(drRTNS);
        }

        return dtRTNS;

    }

    private INV05_RTNM_DTO.RTND_PRODDataTable doInsertProd()
    {
        INV05_RTNM_DTO.RTND_PRODDataTable dtRTNP = null;

        DataTable dtProd = (DataTable)Session["gvMaster"];

        dtRTNP = new INV05_RTNM_DTO.RTND_PRODDataTable();
        INV05_RTNM_DTO.RTND_PRODRow drRTNP;

        if (dtProd.Rows.Count > 0)
        {
            foreach (DataRow dr in dtProd.Rows)
            {

                drRTNP = dtRTNP.NewRTND_PRODRow();
                drRTNP["PRODNO"] = StringUtil.CStr(dr["PRODNO"]);
                drRTNP["RTNN_ID"] = _RTNN_ID_UUID;
                drRTNP["RTND_PROD_ID"] = GuidNo.getUUID();
                drRTNP["MODI_USER"] = logMsg.MODI_USER;
                drRTNP["MODI_DTM"] = Convert.ToDateTime(System.DateTime.Now);
                drRTNP["CREATE_USER"] = drRTNP["MODI_USER"];
                drRTNP["CREATE_DTM"] = drRTNP["MODI_DTM"];
                dtRTNP.Rows.Add(drRTNP);
            }
        }

        return dtRTNP;

    }

    #region gvMaster 觸發事件

    protected void gvMaster_PageIndexChanged(object sender, EventArgs e)
    {
        ASPxGridView grid = sender as ASPxGridView;
        grid.DataSource = Session["gvMaster"];
        grid.DataBind();
    }

    protected void gvMaster_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
    {

        DataTable dtSYS;
        if (Session["gvMaster"] == null)
        {
            //取得空的資料表    
            dtSYS = new DataTable();
            dtSYS = getMasterEmptyData();
        }
        else
        { 
            dtSYS = Session["gvMaster"] as DataTable; 
        }


            DataRow NewRow = dtSYS.NewRow();
            NewRow["PRODNO"] = StringUtil.CStr(e.NewValues["PRODNO"]);
            NewRow["PRODNAME"] = StringUtil.CStr(e.NewValues["PRODNAME"]);
            dtSYS.Rows.Add(NewRow);

            ((ASPxGridView)sender).CancelEdit();
            e.Cancel = true;
            Session["gvMaster"] = dtSYS;
            gvMaster.DataSource = dtSYS;
            gvMaster.DataBind();

    }

    protected void gvMaster_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
    {
        DataTable dtSYS;
        if (Session["gvMaster"] == null)
        {
            //取得空的資料表    
            dtSYS = new DataTable();
            dtSYS = getMasterEmptyData();
        }
        else
        { 
            dtSYS = Session["gvMaster"] as DataTable; 
        }

            string sNewProdNo = StringUtil.CStr(e.NewValues["PRODNO"]);
            string sNewProdName = StringUtil.CStr(e.NewValues["PRODNAME"]);
            string sOldProdNo = StringUtil.CStr(e.OldValues["PRODNO"]);
            string sOldProdName = StringUtil.CStr(e.OldValues["PRODNAME"]);
            for (int i = 0; i < dtSYS.Rows.Count; i++)
            {
                DataRow dr = dtSYS.Rows[i];
                if (StringUtil.CStr(dr["PRODNO"]).CompareTo(sOldProdNo) == 0)
                {
                    dr["PRODNO"] = sNewProdNo.Trim();
                    dr["PRODNAME"] = sNewProdName.Trim();
                    dtSYS.AcceptChanges();
                    break;
                }
            }

            gvMaster.CancelEdit();
            e.Cancel = true;
            Session["gvMaster"] = dtSYS;
            gvMaster.DataSource = dtSYS;
            gvMaster.DataBind();

    }

    protected void gvMaster_RowValidating(object sender, DevExpress.Web.Data.ASPxDataValidationEventArgs e)
    {
            string ProdNo = StringUtil.CStr(e.NewValues["PRODNO"]);

            if (!string.IsNullOrEmpty(ProdNo))
            {
                DataTable dtProd = new Product_Facade().Query_ProductInfo(ProdNo);
                if (dtProd.Rows.Count > 0)
                {
                    if (gvMaster.IsNewRowEditing)
                    {
                        DataTable dt = (DataTable)Session["gvMaster"];
                        int rowCount = dt.Rows.Count;
                        for (int i = 0; i < rowCount; i++)
                        {
                            if (ProdNo == StringUtil.CStr(dt.Rows[i].ItemArray[0]))
                            {
                                e.RowError += "商品料號資料已存在,請重新輸入!!";
                                return;
                            }
                        }
                    }

                }
                else
                {
                    e.RowError += "查無此商品料號!!";
                    return;
                }
            }
    }

    protected void gvMaster_CancelRowEditing(object sender, DevExpress.Web.Data.ASPxStartRowEditingEventArgs e)
    {
        this.btnSave.Enabled = true;
        this.btnCancel.Enabled = true;
        this.btnImport.ClientEnabled = true;
    }

    protected void gvMaster_InitNewRow(object sender, DevExpress.Web.Data.ASPxDataInitNewRowEventArgs e)
    {
        this.btnSave.Enabled = false;
        this.btnCancel.Enabled = false;
        this.btnImport.ClientEnabled = false;
    }

    protected void gvMaster_CommandButtonInitialize(object sender, ASPxGridViewCommandButtonEventArgs e)
    {
        if (e.VisibleIndex > -1)
        {
            if (e.ButtonType == ColumnCommandButtonType.Edit || e.ButtonType == ColumnCommandButtonType.SelectCheckbox)
            {
              

                //if (Status1.Text == "已存檔" || Status1.Text == "已傳輸" || Status1.Text == "已完成")
                if ((Status1.Text == "已存檔" && RTNM_BDate.Date.Day <= DateTime.Now.Date.Day) || Status1.Text == "已傳輸" || Status1.Text == "已完成")
                {

                    e.Enabled = false;
                    lbRTNNo.ReadOnly = true;
                    ReceiptDate.ReadOnly = true;
                    Status1.ReadOnly = true;
                    RTNM_BDate.ClientEnabled = false;
                    RTNM_EDate.ClientEnabled = false;
                    ModifiedDate.ClientEnabled = false;
                    ModifiedBy.ClientEnabled = false;
                    Remark1.ReadOnly = true;
                    RETURN_REASON_CODE.ClientEnabled = false;
                    AFTER_PROCESS_CODE.ClientEnabled = false;
                    txtuuid.ReadOnly = true;
                    btnSave.Enabled = false;
                    btnCancel.Enabled = false;
                    btnImport.Enabled = false;
                    btnAdd.Enabled = false;
                    btnBack.Enabled = false;
                    ASPxPopupControl1.Enabled = false;
                    ddlZone.Enabled = false;

                    //  ASPxButton = (ASPxButton)gvMaster
                    ASPxButton cb = (ASPxButton)gvMaster.FindTitleTemplateControl("btnAddNew");
                    ASPxButton del = (ASPxButton)gvMaster.FindTitleTemplateControl("btnDelete");
                    cb.Enabled = false;
                    del.Enabled = false;
                    
                }

            }

        }
    }

    #endregion

    #region 選擇店組相關函式

    /// <summary>
    /// 繫結區域別資料
    /// </summary>
    private void BindZoneType()
    {
        ddlZone.TextField = INV05_PageHelper.TextField;
        ddlZone.ValueField = INV05_PageHelper.ValueField;
        ddlZone.DataSource = INV05_PageHelper.GetZoneTypes();
        ddlZone.DataBind();

        if (ddlZone.SelectedIndex == -1)
        {
            ddlZone.SelectedIndex = 0;
            ddlZone.SelectedIndex = 0;
            BindZoneTypeList("");
        }

    }

    private void BindZoneTypeList(string strSubZone)
    {
        this.ZoneTypeList.Items.Clear();

        DataTable dtStore = new Store_Facade().Query_Store("", "", strSubZone);
        Session["StoreNo"] = dtStore;

        if (dtStore.Rows.Count > 0)
        {
            for (int i = 0; i <= dtStore.Rows.Count - 1; i++)
            {
                this.ZoneTypeList.Items.Add(StringUtil.CStr(dtStore.Rows[i][0]) + "-" + StringUtil.CStr(dtStore.Rows[i][1]));
            }
        }

    }

    protected void ddlSubZone_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlZone.SelectedIndex > 0)
        {
            BindZoneTypeList(StringUtil.CStr(ddlZone.SelectedItem.Value));
        }
    }

    protected void btnAdd_Click(object sender, ImageClickEventArgs e)
    {
        ArrayList itemList = new ArrayList();
        foreach (ListItem x in ZoneTypeList.Items)
        {
            if (x.Selected)
            {
                StoreList.Items.Add(x);
                itemList.Add(x);
            }
        }
        foreach (object i in itemList)
        {
            ZoneTypeList.Items.Remove((ListItem)i);
        }
    }

    protected void btnBack_Click(object sender, ImageClickEventArgs e)
    {
        ArrayList itemList = new ArrayList();
        foreach (ListItem x in StoreList.Items)
        {
            if (x.Selected)
            {
                ZoneTypeList.Items.Insert(0, x);
                itemList.Add(x);
            }
        }
        foreach (object i in itemList)
        {
            StoreList.Items.Remove((ListItem)i);
        }
    }

    #endregion

    #region Button 觸發事件

    protected void btnAddNew_Click(object sender, EventArgs e)
    { 
        gvMaster.AddNewRow(); 
    }

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        List<object> gvPKValues = gvMaster.GetSelectedFieldValues(gvMaster.KeyFieldName);
        string pkFName = gvMaster.KeyFieldName;

        DataTable dtSYS;
        if (Session["gvMaster"] == null)
        {
            //取得空的資料表    
            dtSYS = new DataTable();
            dtSYS = getMasterEmptyData();
        }
        else
        {
            dtSYS = Session["gvMaster"] as DataTable;
        }
         
        if (dtSYS.Rows.Count > 0)
        {

            List<object> keyValues = this.gvMaster.GetSelectedFieldValues(gvMaster.KeyFieldName);
            foreach (string key in keyValues)
            {
                DataRow drSYS = dtSYS.Select("PRODNO='" + StringUtil.CStr(key) + "'")[0];
                dtSYS.Rows.Remove(drSYS);

            }

            gvMaster.Selection.UnselectAll();
            Session["gvMaster"] = dtSYS;
            gvMaster.DataSource = Session["gvMaster"];
            gvMaster.DataBind();

        }

    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (RTNM_BDate.Text.Trim() != "" && RTNM_EDate.Text.Trim() != "")
        {
            if (string.IsNullOrEmpty(qDno))
            {
                if (DateTime.Now.Date > DateTime.Parse(RTNM_BDate.Text.Trim()).Date)
                {
                    ScriptManager.RegisterClientScriptBlock(this, typeof(string), "退倉日期", "alert('退倉起日不允許小於系統日，請重新輸入!');", true);
                    return;
                }

            }
            if (DateTime.Parse(RTNM_BDate.Text.Trim()) > DateTime.Parse(RTNM_EDate.Text.Trim()))
            {
                ScriptManager.RegisterClientScriptBlock(this, typeof(string), "退倉日期", "alert('退倉訖日不允許小於退倉起日，請重新輸入!');", true);
                return;
            }
        }
        DataTable dtProd = (DataTable)Session["gvMaster"];
        if (dtProd.Rows.Count <= 0)
        {
            ScriptManager.RegisterClientScriptBlock(this, typeof(string), "商品", "alert('未選擇商品不可存檔!');", true);
            return;
        }

        if (StoreList.Items.Count <= 0)
        {
            ScriptManager.RegisterClientScriptBlock(this, typeof(string), "門市", "alert('未選擇門市不可存檔!');", true);
            return;
        }



        int bytes = System.Text.Encoding.UTF8.GetBytes(Remark1.Text).Length;
        if (bytes > 100)
        {
            ScriptManager.RegisterClientScriptBlock(this, typeof(string), "備註", "alert('備註太長，請重新輸入!');", true);
            return;
        }

        bool bShowMessage = false;

        if (lbRTNNo.Text.Trim() == "")
        {

            INV05_Facade INV05_Facade = new INV05_Facade();
            INV05_RTNM_DTO INV05_RTNM_DTO = new INV05_RTNM_DTO();

            INV05_RTNM_DTO.RTNMDataTable dtRTNM = doInsertRTNM();
            INV05_RTNM_DTO.RTND_STOREDataTable dtRTNS = doInsertStore();
            INV05_RTNM_DTO.RTND_PRODDataTable dtRTNP = doInsertProd();

            int intResult = INV05_Facade.SaveRTNM(dtRTNM, dtRTNS, dtRTNP);

            bShowMessage = true;

        }
        else
        {
            //刪除資料庫
            new INV05_Facade().DeleteOne_MethodSet(txtuuid.Text);


            INV05_Facade NV05_Facade = new INV05_Facade();
            INV05_RTNM_DTO INV05_RTNM_DTO = new INV05_RTNM_DTO();

            INV05_RTNM_DTO.RTNMDataTable dtRTNM = doInsertRTNM();
            INV05_RTNM_DTO.RTND_STOREDataTable dtRTNS = doInsertStore();
            INV05_RTNM_DTO.RTND_PRODDataTable dtRTNP = doInsertProd();

            //更新資料庫
            int intResult = NV05_Facade.SaveRTNM(dtRTNM, dtRTNS, dtRTNP);

            bShowMessage = true;

        }

        if (bShowMessage)
        {
            ASPxPageControl1.ActiveTabIndex = 0;

            //退倉單號
            lbRTNNo.Text = _RTNNO;
            //PK
            this.txtuuid.Text = _RTNN_ID_UUID;
            Status1.Text = "已存檔";
            //更新日期,更新人員
            ModifiedDate.Text = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");
            ModifiedBy.Text = logMsg.MODI_USER + " " + new Employee_Facade().GetEmpName(logMsg.MODI_USER);

            //this.btnImport.Enabled = false;
            this.btnImport.ClientEnabled = false;

            msgStr = "存檔完成!";//bill_ou 問題單號 1284   2.當使用者按儲存之動作，提示一個Message Box
            ScriptManager.RegisterClientScriptBlock(this, typeof(string), "儲存", "alert('" + msgStr + "!');", true);

        }

    }

    #endregion

    //ajax 呼當前網頁的方式
    [System.Web.Services.WebMethod()]
    [System.Web.Script.Services.ScriptMethod()]
    static public string getPRODNAME(string PRODUCT_NO)
    {
        string strInfo = "";
        DataTable dt =new Product_Facade().Query_ProductInfo(PRODUCT_NO);
        if (dt.Rows.Count > 0)
        {
            strInfo = StringUtil.CStr(dt.Rows[0]["PRODNAME"]);
        }

        return strInfo;
    }
}
