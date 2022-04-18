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
using AdvTek.CustomControls;
using FET.POS.Model.Common;
using FET.POS.Model.DTO;
using FET.POS.Model.Facade;
using FET.POS.Model.Facade.FacadeImpl;

public partial class VSS_CONS_CON10 : BasePage
{
    //private string msgStr = "";

    //寄銷退倉單號
    private string _RTNNO = "";
    //寄銷退倉單PK _RTNM_ID_UUID
    private static string _RTNM_ID_UUID;

    protected void Page_Load(object sender, EventArgs e)
    {
        string dno = Request.QueryString["dno"] == null ? "" : Request.QueryString["dno"].ToString().Trim();
        this.Session["dno"] = dno;

        if (!IsPostBack && !Page.IsCallback)
        {
            if (logMsg.STORENO != System.Configuration.ConfigurationManager.AppSettings["DefaultRoleHQ"].ToString())
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
                RTNM_BDate.Enabled = false;     //寄銷退倉起日
                RTNM_BDate.ControlStyle.BackColor = System.Drawing.Color.LightGray;
                RTNM_EDate.Enabled = false;     //寄銷退倉訖日
                RTNM_EDate.ControlStyle.BackColor = System.Drawing.Color.LightGray;

                return;
            }
            else
            {

                //區域別
                BindZoneType();

                if (this.Session["dno"].ToString() == "" && this.lblError.Text.ToString().Trim() == "")
                {
                    this.lbRTNNo.Text = "";
                    this.lbStatus.Text = "00";
                    Status1.Text = getStatus(lbStatus.Text);
                    //更新日期,更新人員
                    ModifiedDate.Text = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");
                    ModifiedBy.Text = logMsg.MODI_USER + " " + new Employee_Facade().GetEmpName(logMsg.MODI_USER);

                    //開單日期
                    ReceiptDate.Text = DateTime.Now.ToString("yyyy/MM/dd");
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
                    lbRTNNo.Text = this.Session["dno"].ToString();   //退倉單號

                    CON10_Facade _CON10_Facade = new CON10_Facade();
                    DataTable CSM_RTNM_dt = _CON10_Facade.QueryCSM_RTNM(lbRTNNo.Text);

                    if (CSM_RTNM_dt.Rows.Count > 0)
                    {
                        string RtnmUUID = CSM_RTNM_dt.Rows[0]["CSM_RTNM_UUID"].ToString();

                        //更新日期,更新人員
                        ModifiedDate.Text = DateTime.Parse(CSM_RTNM_dt.Rows[0]["MODI_DTM"].ToString()).ToString("yyyy/MM/dd HH:mm:ss");
                        ModifiedBy.Text = CSM_RTNM_dt.Rows[0]["MODI_USER"].ToString() + " " + new Employee_Facade().GetEmpName(CSM_RTNM_dt.Rows[0]["MODI_USER"].ToString());

                        //開單日期
                        ReceiptDate.Text = DateTime.Now.ToString("yyyy/MM/dd");
                        RTNM_BDate.MinDate = DateTime.Today;
                        RTNM_EDate.MinDate = DateTime.Today;
                        string strB_DATE = "";
                        strB_DATE = CSM_RTNM_dt.Rows[0]["B_DATE"].ToString();
                        strB_DATE = strB_DATE.Substring(0, 4) + "/" + strB_DATE.Substring(4, 2) + "/" + strB_DATE.Substring(6, 2);

                        string strE_DATE = "";
                        strE_DATE = CSM_RTNM_dt.Rows[0]["E_DATE"].ToString();
                        strE_DATE = strE_DATE.Substring(0, 4) + "/" + strE_DATE.Substring(4, 2) + "/" + strE_DATE.Substring(6, 2);

                        RTNM_BDate.Text = strB_DATE;
                        RTNM_EDate.Text = strE_DATE;

                        string strReceiptDate = "";
                        strReceiptDate = DateTime.Parse(CSM_RTNM_dt.Rows[0]["CREATE_DTM"].ToString()).ToString("yyyy/MM/dd");

                        ReceiptDate.Text = strReceiptDate;

                        lbStatus.Text = CSM_RTNM_dt.Rows[0]["STATUS"].ToString();
                        Status1.Text = getStatus(lbStatus.Text);


                        _RTNM_ID_UUID = CSM_RTNM_dt.Rows[0]["CSM_RTNM_UUID"].ToString();

                        doSelectRTNS(_RTNM_ID_UUID);
                        doSelectRTNP(_RTNM_ID_UUID);
                    }



                    //50-以傳輸
                    if (lbStatus.Text.Trim() == "50")
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
                    this.lblError.Text = Session["BATCH_NO"].ToString();

                    CON10_Facade _CON10_Facade = new CON10_Facade();
                    this.StoreList.Items.Clear();
                    //bind匯入之資料-Detail
                    Session["Get_UploadTemp_RTNS"] = _CON10_Facade.Get_UploadTemp_RTNS(this.lblError.Text.ToString());
                    Session["Get_UploadTemp_RTNP"] = _CON10_Facade.Get_UploadTemp_RTNP(this.lblError.Text.ToString());
                    doSelect_UploadTemp_RTNS();
                    doSelect_UploadTemp_RTNP();


                }
            }
        }
    }

    private void doSelectRTNS(string strRTNM_ID_UUID)
    {
        DataTable dtSTORE = new CON10_Facade().QueryCSM_RTNS(strRTNM_ID_UUID);

        if (dtSTORE.Rows.Count > 0)
        {

            for (int i = 0; i < dtSTORE.Rows.Count; i++)
            {

                this.StoreList.Items.Add(dtSTORE.Rows[i]["STORE_NO"].ToString() + "-" + dtSTORE.Rows[i]["STORENAME"].ToString());
            }
        }

    }

    private void doSelectRTNP(string strRTNM_ID_UUID)
    {
        DataTable dtRTNP = new CON10_Facade().QueryCSM_RTNP(strRTNM_ID_UUID);

        if (dtRTNP.Rows.Count > 0)
        {
            DataTable dtResult = new DataTable();
            dtResult.Columns.Add("PRODNO", typeof(string));
            dtResult.Columns.Add("PRODNAME", typeof(string));
            dtResult.Columns.Add("SUPPID", typeof(string));
            dtResult.Columns.Add("SUPPNO", typeof(string));
            dtResult.Columns.Add("SUPPNAME", typeof(string));

            foreach (DataRow dr in dtRTNP.Rows)
            {
                DataRow NewRow = dtResult.NewRow();
                NewRow["PRODNO"] = dr["PRODNO"].ToString();
                NewRow["PRODNAME"] = dr["PRODNAME"].ToString();
                NewRow["PRODNO"] = dr["PRODNO"].ToString();
                NewRow["SUPPID"] = dr["SUPP_ID"].ToString();
                NewRow["SUPPNO"] = dr["SUPP_NO"].ToString();
                NewRow["SUPPNAME"] = dr["SUPP_NAME"].ToString();
                dtResult.Rows.Add(NewRow);
            }

            Session["gvMaster"] = dtResult;
            gvMaster.DataSource = Session["gvMaster"];
            gvMaster.DataBind();
        }
    }

    private DataTable getMasterEmptyData()
    {
        DataTable dtResult = new DataTable();
        dtResult.Columns.Add("PRODNO", typeof(string));
        dtResult.Columns.Add("PRODNAME", typeof(string));
        dtResult.Columns.Add("SUPPID", typeof(string));
        dtResult.Columns.Add("SUPPNO", typeof(string));
        dtResult.Columns.Add("SUPPNAME", typeof(string));
        return dtResult;
    }

    private string getStatus(string StatusNo)
    {
        string strStatus = "";
        switch (StatusNo)
        {
            case "00":
                strStatus = "未存檔";
                break;
            case "10":
                strStatus = "已存檔";
                break;
            case "50":
                strStatus = "已傳輸";
                break;
            case "60":
                strStatus = "已完成";
                break;
            default:
                strStatus = "未存檔";
                break;

        }
        return strStatus;
    }

    #region gvMaster 觸發事件

    protected void gvMaster_CommandButtonInitialize(object sender, ASPxGridViewCommandButtonEventArgs e)
    {
        if (e.VisibleIndex > -1)
        {
            if (e.ButtonType == ColumnCommandButtonType.Edit || e.ButtonType == ColumnCommandButtonType.SelectCheckbox)
            {
                //  string status1 = gvMaster.GetRowValues(e.VisibleIndex, "STATUS").ToString();
                if (lbStatus.Text != "00")
                {
                    e.Enabled = false;
                    lbRTNNo.ReadOnly = true;
                    ReceiptDate.ReadOnly = true;
                    Status1.ReadOnly = true;
                    RTNM_BDate.ReadOnly = true;
                    RTNM_EDate.ReadOnly = true;
                    ModifiedDate.ReadOnly = true;
                    ModifiedBy.ReadOnly = true;
                    txtuuid.ReadOnly = true;
                    btnSave.Enabled = false;
                    btnCancel.Enabled = false;
                    btnImport.Enabled = false;
                    btnAdd.Enabled = false;
                    btnBack.Enabled = false;
                    //ASPxPopupControl1.Enabled = false;
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

    protected void gvMaster_PageIndexChanged(object sender, EventArgs e)
    {

        gvMaster.DataSource = Session["gvMaster"];
        gvMaster.DataBind();
    }

    protected void gvMaster_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
    {

        DataTable dtSYS;
        if (Session["gvMaster"] == null || StringUtil.CStr(Session["gvMaster"]) == "")
        {
            dtSYS = new DataTable();
        }
        else
        {
            dtSYS = Session["gvMaster"] as DataTable;
        }


        DataRow NewRow = dtSYS.NewRow();
        NewRow["PRODNO"] = e.NewValues["PRODNO"].ToString();
        NewRow["PRODNAME"] = e.NewValues["PRODNAME"].ToString();
        NewRow["SUPPID"] = e.NewValues["SUPPID"].ToString();
        NewRow["SUPPNO"] = e.NewValues["SUPPNO"].ToString();
        NewRow["SUPPNAME"] = e.NewValues["SUPPNAME"].ToString();
        dtSYS.Rows.Add(NewRow);

        gvMaster.CancelEdit();
        e.Cancel = true;
        Session["gvMaster"] = dtSYS;
        gvMaster.DataSource = dtSYS;
        gvMaster.DataBind();

    }

    protected void gvMaster_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
    {
        DataTable dtSYS;
        if (Session["gvMaster"] == null || StringUtil.CStr(Session["gvMaster"]) == "")
        {
            dtSYS = new DataTable();
        }
        else
        {
            dtSYS = Session["gvMaster"] as DataTable;
        }

        string sNewProdNo = e.NewValues["PRODNO"].ToString();
        string sNewProdName = e.NewValues["PRODNAME"].ToString();
        string sOldProdNo = e.OldValues["PRODNO"].ToString();
        string sOldProdName = e.OldValues["PRODNAME"].ToString();
        for (int i = 0; i < dtSYS.Rows.Count; i++)
        {
            DataRow dr = dtSYS.Rows[i];
            if (dr["PRODNO"].ToString().CompareTo(sOldProdNo) == 0)
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
        string ProdNo = e.NewValues["PRODNO"].ToString();
        string SuppId = e.NewValues["SUPPID"].ToString();
        string SuppNo = e.NewValues["SUPPNO"].ToString();

        if (!string.IsNullOrEmpty(ProdNo))
        {
            DataTable dtProd = new Product_Facade().Query_ProductConsignmentSale(ProdNo, "", "", SuppId);
            // DataTable dtProd = new Product_Facade().Query_ProductConsignmentSale(ProdNo, "", "");

            DataTable dtSupp = new Supplier_Facade().Query_SuppData(SuppNo);
            if (dtProd.Rows.Count > 0)
            {
                if (gvMaster.IsNewRowEditing)
                {
                    DataTable dt = (DataTable)Session["gvMaster"];
                    int rowCount = dt.Rows.Count;
                    for (int i = 0; i < rowCount; i++)
                    {
                        if (ProdNo == dt.Rows[i].ItemArray[0].ToString())
                        {
                            e.RowError += "商品料號資料已存在,請重新輸入!!";
                            return;
                        }
                    }
                }

            }
            else
            {
                e.RowError += "此廠商查無此商品料號!!";
                return;
            }
        }
    }

    protected void gvMaster_HtmlRowPrepared(object sender, ASPxGridViewTableRowEventArgs e)
    {
        e.Row.Attributes["canSelect"] = "true";

        if (e.RowType == GridViewRowType.Data)
        {
            string STATUS = this.lbStatus.Text;
            if (STATUS == "00" || STATUS == "")
                e.Row.Attributes["canSelect"] = "true";
            else
                e.Row.Attributes["canSelect"] = "false";
        }

        //if (e.RowType == GridViewRowType.Data)
        //{
        //    //PopupControl txtPRODNO = e.Row.FindChildControl<PopupControl>("txtPRODNO");
        //    //ASPxTextBox txtSuppId = e.Row.FindChildControl<ASPxTextBox>("txtSuppId");

        //    //ASPxPopupControl ASPxPopupControl1 = txtPRODNO.FindChildControl<ASPxPopupControl>("ASPxPopupControl1");
        //    //ASPxPopupControl1.ContentUrl = "~/VSS/Common/ProductsPopup3.aspx?SysDate=Date()&KeyFieldValue1=consignmentsale&KeyFieldValue2=PRODNO";

        //}

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
        //PopupControl txtPRODNO = gvMaster.FindChildControl<PopupControl>("txtPRODNO");
        //ASPxPopupControl ProdPop = txtPRODNO.FindChildControl<ASPxPopupControl>("ASPxPopupControl1");
        //ProdPop.ContentUrl = "~/VSS/Common/ProductsPopup3.aspx?SysDate=Date()&KeyFieldValue1=consignmentsale&KeyFieldValue2=PRODNO";
    }

    protected void gvMaster_StartRowEditing(object sender, DevExpress.Web.Data.ASPxStartRowEditingEventArgs e)
    {
        gvMaster.Selection.UnselectAll();
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
        if (Session["gvMaster"] == null || StringUtil.CStr(Session["gvMaster"]) == "")
        {
            dtSYS = new DataTable();
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
                string[] keyTmp = key.Split(new char[] { '|' });
                DataRow drSYS = dtSYS.Select("PRODNO='" + keyTmp[0] + "' And " + "SUPPNO='" + keyTmp[1] + "'")[0];
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
            if (Request.QueryString["dno"] == null)
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
        DataTable dtMaster = (DataTable)Session["gvMaster"];
        if (dtMaster.Rows.Count <= 0)
        {
            ScriptManager.RegisterClientScriptBlock(this, typeof(string), "商品", "alert('未選擇商品不可存檔!');", true);
            return;
        }

        if (StoreList.Items.Count <= 0)
        {
            ScriptManager.RegisterClientScriptBlock(this, typeof(string), "門市", "alert('未選擇門市不可存檔!');", true);
            return;
        }


        //寄銷退倉單號
        if (lbRTNNo.Text.ToString().Trim() == "")
        {
            _RTNNO = SerialNo.GenNo("COR"); //退倉單號規則:COR+YYYYMMDD+2碼流水序號;         
        }
        else
        { _RTNNO = lbRTNNo.Text.ToString().Trim(); }

        _RTNM_ID_UUID = GuidNo.getUUID();


        DataSet CSM_RTN = new DataSet();

        //寄銷退倉設定主檔
        CSM_RTN.Tables.Add("CSM_RTNM");
        CSM_RTN.Tables["CSM_RTNM"].Columns.Add("CSM_RTNM_UUID", typeof(string)); //RTNM_UUID
        CSM_RTN.Tables["CSM_RTNM"].Columns.Add("RTNNO", typeof(string)); //退倉單號
        CSM_RTN.Tables["CSM_RTNM"].Columns.Add("B_DATE", typeof(string)); //退倉起日
        CSM_RTN.Tables["CSM_RTNM"].Columns.Add("E_DATE", typeof(string)); //退倉迄日
        CSM_RTN.Tables["CSM_RTNM"].Columns.Add("CREATE_USER", typeof(string)); //建立人員
        CSM_RTN.Tables["CSM_RTNM"].Columns.Add("CREATE_DTM", typeof(DateTime)); //建立時間
        CSM_RTN.Tables["CSM_RTNM"].Columns.Add("MODI_USER", typeof(string)); //異動人員
        CSM_RTN.Tables["CSM_RTNM"].Columns.Add("MODI_DTM", typeof(DateTime)); //異動時間
        CSM_RTN.Tables["CSM_RTNM"].Columns.Add("STATUS", typeof(string)); //狀態 00- 未存檔 10-已存檔 50-已傳輸 60-已完成

        DataRow CSM_RTNM_NewRow = CSM_RTN.Tables["CSM_RTNM"].NewRow();
        CSM_RTNM_NewRow["CSM_RTNM_UUID"] = _RTNM_ID_UUID;
        CSM_RTNM_NewRow["RTNNO"] = _RTNNO;
        CSM_RTNM_NewRow["B_DATE"] = DateTime.Parse(RTNM_BDate.Text).ToString("yyyyMMdd");
        CSM_RTNM_NewRow["E_DATE"] = DateTime.Parse(RTNM_EDate.Text).ToString("yyyyMMdd");
        CSM_RTNM_NewRow["CREATE_USER"] = ModifiedBy.Text.Split(new char[] { ' ' })[0];
        CSM_RTNM_NewRow["CREATE_DTM"] = ReceiptDate.Text;
        CSM_RTNM_NewRow["MODI_USER"] = this.logMsg.MODI_USER.ToString().Trim();
        CSM_RTNM_NewRow["MODI_DTM"] = this.logMsg.MODI_DTM.ToString().Trim();
        CSM_RTNM_NewRow["STATUS"] = "50";
        CSM_RTN.Tables["CSM_RTNM"].Rows.Add(CSM_RTNM_NewRow);

        //寄銷退倉明細
        CSM_RTN.Tables.Add("CSM_RTND_UP");
        CSM_RTN.Tables["CSM_RTND_UP"].Columns.Add("CSM_RTND_UP_ID", typeof(string)); //RTND_UP_UUID
        CSM_RTN.Tables["CSM_RTND_UP"].Columns.Add("SEQNO", typeof(int)); //項次
        CSM_RTN.Tables["CSM_RTND_UP"].Columns.Add("PRODNO", typeof(string)); //商品料號
        CSM_RTN.Tables["CSM_RTND_UP"].Columns.Add("STOCKQTY", typeof(int)); //庫存量
        CSM_RTN.Tables["CSM_RTND_UP"].Columns.Add("RTNQTY", typeof(int)); //退倉量
        //CSM_RTN.Tables["CSM_RTND_UP"].Columns.Add("RTNDATE", typeof(string)); //退倉日期
        CSM_RTN.Tables["CSM_RTND_UP"].Columns.Add("CREATE_USER", typeof(string)); //建立人員
        CSM_RTN.Tables["CSM_RTND_UP"].Columns.Add("CREATE_DTM", typeof(DateTime)); //建立時間
        CSM_RTN.Tables["CSM_RTND_UP"].Columns.Add("MODI_USER", typeof(string)); //異動人員
        CSM_RTN.Tables["CSM_RTND_UP"].Columns.Add("MODI_DTM", typeof(DateTime)); //異動時間
        CSM_RTN.Tables["CSM_RTND_UP"].Columns.Add("SUPP_ID", typeof(string)); //外部廠商id
        CSM_RTN.Tables["CSM_RTND_UP"].Columns.Add("STORE_NO", typeof(string)); //門市代碼
        CSM_RTN.Tables["CSM_RTND_UP"].Columns.Add("CSM_RTNM_UUID", typeof(string)); //RTNM_UUID
        CSM_RTN.Tables["CSM_RTND_UP"].Columns.Add("STATUS", typeof(string)); //狀態 00- 未存檔 10-已存檔 50-已傳輸 60-已完成

        //寄銷退倉商品設定明細
        CSM_RTN.Tables.Add("CSM_RTND_PROD");
        CSM_RTN.Tables["CSM_RTND_PROD"].Columns.Add("CSM_RTND_ID", typeof(string)); //RTND_PROD_UUID
        CSM_RTN.Tables["CSM_RTND_PROD"].Columns.Add("SUPP_ID", typeof(string)); //外部廠商id
        CSM_RTN.Tables["CSM_RTND_PROD"].Columns.Add("PRODNO", typeof(string)); //商品料號
        CSM_RTN.Tables["CSM_RTND_PROD"].Columns.Add("CREATE_USER", typeof(string)); //建立人員
        CSM_RTN.Tables["CSM_RTND_PROD"].Columns.Add("CREATE_DTM", typeof(DateTime)); //建立時間
        CSM_RTN.Tables["CSM_RTND_PROD"].Columns.Add("MODI_USER", typeof(string)); //異動人員
        CSM_RTN.Tables["CSM_RTND_PROD"].Columns.Add("MODI_DTM", typeof(DateTime)); //異動時間
        CSM_RTN.Tables["CSM_RTND_PROD"].Columns.Add("CSM_RTNM_UUID", typeof(string)); //RTNM_UUID

        //寄銷退倉商品設定明細
        CSM_RTN.Tables.Add("CSM_RTND_STORE");
        CSM_RTN.Tables["CSM_RTND_STORE"].Columns.Add("CSM_RTN_STORE_ID", typeof(string)); //RTND_STORE_UUID
        CSM_RTN.Tables["CSM_RTND_STORE"].Columns.Add("STORE_NO", typeof(string)); //門市代碼
        CSM_RTN.Tables["CSM_RTND_STORE"].Columns.Add("CREATE_USER", typeof(string)); //建立人員
        CSM_RTN.Tables["CSM_RTND_STORE"].Columns.Add("CREATE_DTM", typeof(DateTime)); //建立時間
        CSM_RTN.Tables["CSM_RTND_STORE"].Columns.Add("MODI_USER", typeof(string)); //異動人員
        CSM_RTN.Tables["CSM_RTND_STORE"].Columns.Add("MODI_DTM", typeof(DateTime)); //異動時間
        CSM_RTN.Tables["CSM_RTND_STORE"].Columns.Add("CSM_RTNM_UUID", typeof(string)); //RTNM_UUID

        for (int i = 0; i < dtMaster.Rows.Count; i++)
        {
            //string strSupp_id;
            //DataTable dtSupp = new Supplier_Facade().Query_SuppData(dtMaster.Rows[i]["SUPPNO"].ToString().Trim());
            //if (dtSupp.Rows.Count > 0)
            //    strSupp_id = dtSupp.Rows[0]["SUPP_ID"].ToString();
            //else
            //    strSupp_id = "";


            DataRow CSM_RTND_PROD_NewRow = CSM_RTN.Tables["CSM_RTND_PROD"].NewRow();
            CSM_RTND_PROD_NewRow["CSM_RTND_ID"] = GuidNo.getUUID();
            CSM_RTND_PROD_NewRow["SUPP_ID"] = dtMaster.Rows[i]["SUPPID"].ToString().Trim();
            CSM_RTND_PROD_NewRow["PRODNO"] = dtMaster.Rows[i]["PRODNO"].ToString().Trim();
            CSM_RTND_PROD_NewRow["CREATE_USER"] = this.logMsg.CREATE_USER.ToString().Trim();
            CSM_RTND_PROD_NewRow["CREATE_DTM"] = this.logMsg.CREATE_DTM.ToString().Trim();
            CSM_RTND_PROD_NewRow["MODI_USER"] = this.logMsg.MODI_USER.ToString().Trim();
            CSM_RTND_PROD_NewRow["MODI_DTM"] = this.logMsg.MODI_DTM.ToString().Trim();
            CSM_RTND_PROD_NewRow["CSM_RTNM_UUID"] = _RTNM_ID_UUID;
            CSM_RTN.Tables["CSM_RTND_PROD"].Rows.Add(CSM_RTND_PROD_NewRow);
        }

        for (int i = 0; i < StoreList.Items.Count; i++)
        {
            DataRow CSM_RTND_STORE_NewRow = CSM_RTN.Tables["CSM_RTND_STORE"].NewRow();
            CSM_RTND_STORE_NewRow["CSM_RTN_STORE_ID"] = GuidNo.getUUID();
            CSM_RTND_STORE_NewRow["STORE_NO"] = StoreList.Items[i].ToString().Trim().Substring(0, StoreList.Items[i].ToString().Trim().IndexOf('-'));
            CSM_RTND_STORE_NewRow["CREATE_USER"] = this.logMsg.CREATE_USER.ToString().Trim();
            CSM_RTND_STORE_NewRow["CREATE_DTM"] = this.logMsg.CREATE_DTM.ToString().Trim();
            CSM_RTND_STORE_NewRow["MODI_USER"] = this.logMsg.MODI_USER.ToString().Trim();
            CSM_RTND_STORE_NewRow["MODI_DTM"] = this.logMsg.MODI_DTM.ToString().Trim();
            CSM_RTND_STORE_NewRow["CSM_RTNM_UUID"] = _RTNM_ID_UUID;
            CSM_RTN.Tables["CSM_RTND_STORE"].Rows.Add(CSM_RTND_STORE_NewRow);

            for (int j = 0; j < dtMaster.Rows.Count; j++)
            {


                DataRow CSM_RTND_UP_NewRow = CSM_RTN.Tables["CSM_RTND_UP"].NewRow();
                CSM_RTND_UP_NewRow["CSM_RTND_UP_ID"] = GuidNo.getUUID();
                CSM_RTND_UP_NewRow["SEQNO"] = j + 1;
                CSM_RTND_UP_NewRow["PRODNO"] = dtMaster.Rows[j]["PRODNO"].ToString().Trim();
                CSM_RTND_UP_NewRow["STOCKQTY"] = 0;
                CSM_RTND_UP_NewRow["RTNQTY"] = 0;
                //CSM_RTND_UP_NewRow["RTNDATE"] = DateTime.Now.ToString("yyyyMMdd");
                CSM_RTND_UP_NewRow["CREATE_USER"] = this.logMsg.CREATE_USER.ToString().Trim();
                CSM_RTND_UP_NewRow["CREATE_DTM"] = this.logMsg.CREATE_DTM.ToString().Trim();
                CSM_RTND_UP_NewRow["MODI_USER"] = this.logMsg.MODI_USER.ToString().Trim();
                CSM_RTND_UP_NewRow["MODI_DTM"] = this.logMsg.MODI_DTM.ToString().Trim();
                CSM_RTND_UP_NewRow["SUPP_ID"] = dtMaster.Rows[j]["SUPPID"].ToString().Trim();
                CSM_RTND_UP_NewRow["STORE_NO"] = StoreList.Items[i].ToString().Trim().Substring(0, StoreList.Items[i].ToString().Trim().IndexOf('-'));
                CSM_RTND_UP_NewRow["CSM_RTNM_UUID"] = _RTNM_ID_UUID;
                CSM_RTND_UP_NewRow["STATUS"] = "00";
                CSM_RTN.Tables["CSM_RTND_UP"].Rows.Add(CSM_RTND_UP_NewRow);
            }

        }

        int intResult = new CON10_Facade().SaveOrderData(CSM_RTN, _RTNNO);

        if (intResult == 4)
        {
            //寄銷退倉單號
            lbRTNNo.Text = _RTNNO;
            //PK
            this.txtuuid.Text = _RTNM_ID_UUID;


            lbStatus.Text = "50";
            Status1.Text = getStatus(lbStatus.Text);
            //更新日期,更新人員
            ModifiedDate.Text = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");
            ModifiedBy.Text = logMsg.MODI_USER + " " + new Employee_Facade().GetEmpName(logMsg.MODI_USER);
            gvMaster.Enabled = false;

            ScriptManager.RegisterClientScriptBlock(this, typeof(string), "Save", "alert('已存檔完成!!');", true); //[存檔訊息]
        }
        else
        {
            ScriptManager.RegisterClientScriptBlock(this, typeof(string), "SaveError", "alert('存檔失敗!!');", true); //[存檔訊息]

        }
    }

    protected void btnAdd_Click(object sender, ImageClickEventArgs e)
    {
        ArrayList itemList = new ArrayList();


        foreach (ListItem x in ZoneTypeList.Items)
        {
            if (x.Selected)
            {
                bool blCheck = true;

                foreach (ListItem y in StoreList.Items)
                {
                    if (y.Value == x.Value)
                    {
                        blCheck = false;
                        break;
                    }

                }
                if (blCheck)
                {
                    StoreList.Items.Add(x);
                    itemList.Add(x);
                }
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

    #region ajax 呼當前網頁

    //ajax 呼當前網頁的方式
    [System.Web.Services.WebMethod()]
    [System.Web.Script.Services.ScriptMethod()]
    static public ArrayList getPRODNAME(string strData)
    {
        ArrayList strInfo = new ArrayList();
        string[] strDataTmp = strData.Split(new char[] { '∩' });
        //DataTable dt = new Product_Facade().Query_ProductConsignmentSale(strDataTmp[0], "", "", strDataTmp[1]);
        string ProdNo = strDataTmp[0];
        DataTable dt = new Product_Facade().Query_ProductConsignmentSale(ProdNo, "", "");

        if (dt.Rows.Count > 0)
        {
            string ProdName = dt.Rows[0]["PRODNAME"].ToString();
            strInfo.Add(ProdName);
        }
        string SuppId = new Supplier_Facade().GetSuppId2(ProdNo);
        DataTable dtSuppNo = new Supplier_Facade().Query_SuppData_ID(SuppId);
        string SuppNo = string.Empty;
        string SuppName = string.Empty;
        if (dtSuppNo.Rows.Count > 0)
        {
            SuppNo = dtSuppNo.Rows[0]["SUPP_NO"].ToString();
            SuppName = dtSuppNo.Rows[0]["SUPP_NAME"].ToString();
        }

        
        strInfo.Add(SuppId);
        strInfo.Add(SuppNo);
        strInfo.Add(SuppName);


        return strInfo;
    }

    //ajax 呼當前網頁的方式
    [System.Web.Services.WebMethod()]
    [System.Web.Script.Services.ScriptMethod()]
    static public string getSuppInfo(string SuppNo)
    {  
        string strInfo = "";
        try
        {
            if (!string.IsNullOrEmpty(SuppNo))
            {
                DataTable dtSupp = new Supplier_Facade().Query_SuppData(SuppNo);
                if (dtSupp.Rows.Count > 0)
                {
                    strInfo = dtSupp.Rows[0]["SUPP_ID"].ToString() + "∩" + dtSupp.Rows[0]["SUPP_NAME"].ToString();
                }
            }

        }
        catch (Exception)
        {
            strInfo = "";
        }
        return strInfo;
    }
    
    #endregion

    #region 匯入操作
    //匯入之資料-門市
    private void doSelect_UploadTemp_RTNS()
    {
        DataTable dtSTORE = (DataTable)Session["Get_UploadTemp_RTNS"];

        if (dtSTORE.Rows.Count > 0)
        {
            for (int i = 0; i < dtSTORE.Rows.Count; i++)
            {
                this.StoreList.Items.Add(dtSTORE.Rows[i]["STORE_NO"].ToString() + "-" + dtSTORE.Rows[i]["STORENAME"].ToString());
            }
        }

    }

    //匯入之資料-商品與廠商
    private void doSelect_UploadTemp_RTNP()
    {
        DataTable dtRTNP = (DataTable)Session["Get_UploadTemp_RTNP"];

        if (dtRTNP.Rows.Count > 0)
        {
            DataTable dtResult = new DataTable();
            dtResult.Columns.Add("PRODNO", typeof(string));
            dtResult.Columns.Add("PRODNAME", typeof(string));
            dtResult.Columns.Add("SUPPID", typeof(string));
            dtResult.Columns.Add("SUPPNO", typeof(string));
            dtResult.Columns.Add("SUPPNAME", typeof(string));

            foreach (DataRow dr in dtRTNP.Rows)
            {
                DataRow NewRow = dtResult.NewRow();
                NewRow["PRODNO"] = dr["PRODNO"].ToString();
                NewRow["PRODNAME"] = dr["PRODNAME"].ToString();
                NewRow["SUPPID"] = dr["SUPPID"].ToString();
                NewRow["SUPPNO"] = dr["SUPPNO"].ToString();
                NewRow["SUPPNAME"] = dr["SUPPNAME"].ToString();
                dtResult.Rows.Add(NewRow);
            }

            Session["gvMaster"] = dtResult;
            gvMaster.DataSource = dtResult;
            gvMaster.DataBind();
        }
    }
    #endregion

    #region 門市資料處理
    /// <summary>
    /// 繫結區域別資料
    /// </summary>
    private void BindZoneType()
    {
        ddlZone.TextField = CON10_PageHelper.TextField;
        ddlZone.ValueField = CON10_PageHelper.ValueField;
        ddlZone.DataSource = CON10_PageHelper.GetZoneTypes();
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
            for (int i = 0; i < dtStore.Rows.Count - 1; i++)
            {
                string x = dtStore.Rows[i].ItemArray[0].ToString() + "-" + dtStore.Rows[i].ItemArray[1].ToString();
                bool blCheck = true;
                foreach (ListItem y in StoreList.Items)
                {
                    if (y.Value == x)
                    {
                        blCheck = false;
                        break;
                    }

                }
                if (blCheck)
                {
                    this.ZoneTypeList.Items.Add(x);
                }
               
            }


        }

    }

    protected void ddlSubZone_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlZone.SelectedIndex > 0)
        {
            BindZoneTypeList(ddlZone.SelectedItem.Value.ToString());
        }
    }
    #endregion

    protected void gvMaster_HtmlRowCreated(object sender, ASPxGridViewTableRowEventArgs e)
    {
        if (e.RowType == GridViewRowType.InlineEdit)
        {
            PopupControl txtProdNo = gvMaster.FindChildControl<PopupControl>("txtProdNo");
            ASPxPopupControl ASPxPopupControl1 = txtProdNo.FindChildControl<ASPxPopupControl>("ASPxPopupControl1");
            ASPxPopupControl1.ClientInstanceName = "ASPxPopupControl_ProdNo";
            ASPxTextBox txtSuppNo = gvMaster.FindChildControl<ASPxTextBox>("txtSuppId");
            string strSuppNo = txtSuppNo.Text;
            if (!string.IsNullOrEmpty(strSuppNo)) {
                string url = ASPxPopupControl1.ContentUrl;
                url = url.Replace("~/VSS/Common/", "../../Common/");
                string[] url2 = url.Split("?".ToCharArray());
                if (url2.Length > 1)
                {
                    string ur12_1 = url2[0];
                    string url2_2 = url2[1];
                    string[] s = url2_2.Split("&".ToCharArray());
                    if (s.Length > 0)
                    {
                        //SysDate
                        string ordSysDate = string.Empty;
                        string[] SysDate = s[0].Split("=".ToCharArray());
                        if (SysDate.Length > 0)
                        {
                            ordSysDate = SysDate[1];
                        }
                        string newSysDate = StringUtil.CStr(DateTime.Now);
                        if (!string.IsNullOrEmpty(ordSysDate))
                        {
                            url2_2 = url2_2.Replace(ordSysDate, newSysDate);
                        }


                        //KeyFieldValue1
                        string strkeyValue = string.Empty;
                        string[] oldKeyFieldValue1 = s[1].Split("=".ToCharArray());
                        if (oldKeyFieldValue1.Length > 0)
                        {
                            strkeyValue = oldKeyFieldValue1[1];
                        }
                        string newKeyFieldValue1 = "consignmentsale_suppid";
                        if (!string.IsNullOrEmpty(strkeyValue))
                        {
                            url2_2 = url2_2.Replace(strkeyValue, newKeyFieldValue1);
                        }

                        //KeyFieldValue2
                        string strkeyValue2 = string.Empty;
                        string[] oldKeyFieldValue2 = s[2].Split("=".ToCharArray());
                        if (oldKeyFieldValue2.Length > 0)
                        {
                            strkeyValue2 = oldKeyFieldValue2[1];
                        }
                        string newKeyFieldValue2 = strSuppNo;
                        if (!string.IsNullOrEmpty(strkeyValue))
                        {
                            url2_2 = url2_2.Replace(strkeyValue2, newKeyFieldValue2);
                        }
                    }
                   
                    
                    url = ur12_1 +"?"+ url2_2;
                }
                  ASPxPopupControl1.ContentUrl = url;
            }

        }
    }
}
