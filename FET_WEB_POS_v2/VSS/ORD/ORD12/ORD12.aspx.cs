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

public partial class VSS_ORD_ORD12_ORD12 : BasePage
{
    //訂貨時間
    private int Order_STime = 2;
    private int Order_ETime = 4;
    private string LAST_ATR_TRAN_TO_STORE = "";

    /// <summary>
    /// 暫存的預訂單明細資料
    /// </summary>
    private DataTable PreOrderDetailDt
    {
        get
        {
            if (Session[SessionID] == null)
            {
                DataTable dt = new DataTable();

                dt.Columns.Add("PRE_ORDER_SEQNO");
                dt.Columns.Add("ProductNo");
                dt.Columns.Add("ProductName");
                dt.Columns.Add("PreOrderQty");
                dt.Columns.Add("PurchQTY");
                dt.Columns.Add("CurrentInvQTY");
                dt.Columns.Add("EStoreBookQTY");
                dt.Columns.Add("hasGiftProducts", typeof(bool));
                dt.Columns.Add("OneToOneSID");
                dt.Columns.Add("GiftProducts", typeof(DataTable));

                DataColumn[] PrimaryCol = new DataColumn[1];

                PrimaryCol[0] = dt.Columns["PRE_ORDER_SEQNO"];

                dt.PrimaryKey = PrimaryCol;

                Session[SessionID] = dt;
            }

            return (DataTable)Session[SessionID];
        }

        set
        {
            Session[SessionID] = value;
        }
    }

    /// <summary>
    /// 預訂單主檔ID
    /// </summary>
    private string qPreOrderID
    {
        get
        {
            return (Request.QueryString["preordid"] ?? "");
        }
    }

    /// <summary>
    /// 是否為新增模式
    /// </summary>
    private bool IsAddMode
    {
        get
        {
            return (qPreOrderID == "");
        }
    }

    private string SessionID
    {
        get
        {
            return (string)Session["SessionID"];
        }
        set
        {
            Session["SessionID"] = value;
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Session["ProductNo"] = null;
            Session["PreOrderQty"] = null;

            Session["gvMasterDV"] = null;
            Session["PreOrderMID"] = null;
            Session["STATUS"] = null;
            string UserName = logMsg.CREATE_USER;
            lblUpdateUser.Text = "";// logMsg.CREATE_USER;
            string StoreNo = logMsg.STORENO;
            txtSTORE_NO.Text = logMsg.STORENO;

            //**2011/03/14 Tina：註解起來，門市判斷依照"訂貨作業"(ORD01)
            ////判斷門市是否暫停營業
            //if (string.IsNullOrEmpty(new Store_Facade().GetStoreName(txtSTORE_NO.Text)))
            //{
            //    ScriptManager.RegisterClientScriptBlock(this, GetType(), "AlertMessage", "alert('現在為門市暫停營業期間，無法訂貨!!');", true);
            //    gvMasterDV.Enabled = false;
            //    btnSave.Enabled = false;

            //    return;
            //}

            //判斷門市是否暫停營業
            int storeStatus = new Store_Facade().Query_StoreStatus(logMsg.STORENO);
            if (storeStatus != 0)
            {
                gvMasterDV.Enabled = false;
                btnSave.Enabled = false;
                if (storeStatus == 1)
                    ScriptManager.RegisterClientScriptBlock(this, typeof(string), "selectSALE_NO", "alert('現在為門市暫停營業期間，無法訂貨!');", true);
                else if (storeStatus == 2)
                    ScriptManager.RegisterClientScriptBlock(this, typeof(string), "selectSALE_NO", "alert('此門市已關閉，無法訂貨!');", true);
                return;
            }

            if (IsAddMode)
            {
                if (IsAddMode && ORD12_PageHelper.HasOldPreOrder(txtSTORE_NO.Text))
                {
                    string PreOrderMID = new ORD12_Facade().GetPreOrderMID(txtSTORE_NO.Text);
                    Session["PreOrderMID"] = PreOrderMID;

                    //載入預訂單
                    SessionID = GuidNo.getUUID();

                    DataTable MasterDt = new ORD12_Facade().GetPreOrderM(PreOrderMID);
                    DataTable DetailDt = new ORD12_Facade().GetPreOrderD(PreOrderMID, txtSTORE_NO.Text);
                    Session["STATUS"] = StringUtil.CStr(MasterDt.Rows[0]["STATUS"]);
                    txtSTORE_NO.Text = StringUtil.CStr(MasterDt.Rows[0]["STORE_NO"]);
                    //主檔載入至UI
                    DataRow MasterDr = MasterDt.Rows[0];
                    lblOrderNo.Text = StringUtil.CStr(MasterDr["PRE_ORDER_NO"]);
                    txtMemo.Text = StringUtil.CStr(MasterDr["REMARK"]);
                    lblUpdateUser.Text = StringUtil.CStr(MasterDr["MODI_USER"]) + " " + new Employee_Facade().GetEmpName(StringUtil.CStr(MasterDr["MODI_USER"]));
                    lblUpdDateTime.Text = StringUtil.CStr(MasterDr["MODI_DTM"]);
                    lblOrderDate.Text = DateTime.Now.ToString("yyyy/MM/dd");
                    int i = 1;

                    foreach (DataRow dr in DetailDt.Rows)
                    {
                        DataRow DRNew = PreOrderDetailDt.NewRow();

                        DRNew["PRE_ORDER_SEQNO"] = i++;
                        DRNew["ProductNo"] = dr["PRODNO"];
                        DRNew["ProductName"] = dr["PRODNAME"];
                        DRNew["PreOrderQty"] = dr["ORDQTY"];
                        DRNew["PurchQTY"] = dr["INWAYQTY"];
                        DRNew["CurrentInvQTY"] = ORD12_PageHelper.GetCurrentInvQTY(StoreNo, StringUtil.CStr(dr["PRODNO"]));
                        DRNew["EStoreBookQTY"] = ORD12_PageHelper.GetEStoreBookQTY(StoreNo, StringUtil.CStr(dr["PRODNO"]));
                        DRNew["hasGiftProducts"] = false;

                        PreOrderDetailDt.Rows.Add(DRNew);
                    }
                    Session["gvMasterDV"] = PreOrderDetailDt;
                }
                else
                {
                    Order_STime = int.Parse(ORD12_PageHelper.GetSTORE_ORDER_SE_DTM("STORE_ORDER_S_DTM").Substring(0, 2));
                    Order_ETime = int.Parse(ORD12_PageHelper.GetSTORE_ORDER_SE_DTM("STORE_ORDER_E_DTM").Substring(0, 2));
                    LAST_ATR_TRAN_TO_STORE = ORD12_PageHelper.GetSTORE_ORDER_SE_DTM("LAST_ATR_TRAN_TO_STORE").Substring(0, 8);
                    //檢查是否為訂貨時間
                    int NowH = DateTime.Now.Hour;
                    if (DateTime.Now.ToString("yyyyMMdd") == LAST_ATR_TRAN_TO_STORE)
                    {
                        if (NowH >= Order_STime && NowH < Order_ETime)
                        {
                            ScriptManager.RegisterClientScriptBlock(this, GetType(), "AlertMessage", "alert('現在為訂貨時間，請至訂貨作業下單!!');location.href='../ORD01/ORD01.aspx';", true);
                            return;
                        }
                    }
                    

                    DataRow DRNew = PreOrderDetailDt.NewRow();


                    //產生新的預訂單
                    SessionID = GuidNo.getUUID();

                    lblUpdateUser.Text = logMsg.OPERATOR + " " + new Employee_Facade().GetEmpName(logMsg.OPERATOR);
                    lblOrderDate.Text = DateTime.Now.ToString("yyyy/MM/dd");
                    lblUpdDateTime.Text = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");
                    this.btnSave.ClientEnabled = false;
                    this.btnDrop.ClientEnabled = false;
                }
            }
            else
            {
                //載入預訂單
                SessionID = GuidNo.getUUID();

                DataTable MasterDt = new ORD12_Facade().GetPreOrderM(qPreOrderID);
                DataTable DetailDt = new ORD12_Facade().GetPreOrderD(qPreOrderID, txtSTORE_NO.Text);
                Session["STATUS"] = StringUtil.CStr(MasterDt.Rows[0]["STATUS"]);
                txtSTORE_NO.Text = StringUtil.CStr(MasterDt.Rows[0]["STORE_NO"]);
                //StoreNo = txtSTORE_NO.Text;
                //主檔載入至UI
                DataRow MasterDr = MasterDt.Rows[0];
                lblOrderNo.Text = StringUtil.CStr(MasterDr["PRE_ORDER_NO"]);
                txtMemo.Text = StringUtil.CStr(MasterDr["REMARK"]);

                lblUpdateUser.Text = StringUtil.CStr(MasterDr["MODI_USER"]) + " " + new Employee_Facade().GetEmpName(StringUtil.CStr(MasterDr["MODI_USER"]));
                lblUpdDateTime.Text = StringUtil.CStr(MasterDr["MODI_DTM"]);
                lblOrderDate.Text = StringUtil.CStr(MasterDr["ORDDATE"]).Substring(0, 4) + "/" + StringUtil.CStr(MasterDr["ORDDATE"]).Substring(4, 2) + "/" + StringUtil.CStr(MasterDr["ORDDATE"]).Substring(6, 2);
                int i = 1;

                foreach (DataRow dr in DetailDt.Rows)
                {
                    DataRow DRNew = PreOrderDetailDt.NewRow();

                    DRNew["PRE_ORDER_SEQNO"] = i++;
                    DRNew["ProductNo"] = dr["PRODNO"];
                    DRNew["ProductName"] = dr["PRODNAME"];
                    DRNew["PreOrderQty"] = dr["ORDQTY"];
                    DRNew["PurchQTY"] = dr["INWAYQTY"];
                    //DRNew["CurrentInvQTY"] = "0";
                    //DRNew["EStoreBookQTY"] = "0";
                    DRNew["CurrentInvQTY"] = ORD12_PageHelper.GetCurrentInvQTY(StoreNo, StringUtil.CStr(dr["PRODNO"])); //"0";
                    DRNew["EStoreBookQTY"] = ORD12_PageHelper.GetEStoreBookQTY(StoreNo, StringUtil.CStr(dr["PRODNO"]));// "0";

                    DRNew["hasGiftProducts"] = false;

                    PreOrderDetailDt.Rows.Add(DRNew);
                }
                Session["gvMasterDV"] = PreOrderDetailDt;
            }

            bindgvMaster();

            if ((Session["STATUS"] == null ? "" : StringUtil.CStr(Session["STATUS"])) == "50")
            {
                this.btnSave.ClientEnabled = false;
                this.btnDrop.ClientEnabled = false;
            }
        }
        if (gvMasterDV.IsEditing)
        {
            Session["ProductNo"] = ((PopupControl)gvMasterDV.FindEditRowCellTemplateControl((GridViewDataColumn)gvMasterDV.Columns["ProductNo"], "txtProductNo")).Text;
            Session["PreOrderQty"] = ((ASPxTextBox)gvMasterDV.FindEditRowCellTemplateControl((GridViewDataColumn)gvMasterDV.Columns["PreOrderQty"], "txtPreOrderQty")).Text;
        }
    }

    protected void bindgvMaster()
    {
        gvMasterDV.DataSource = PreOrderDetailDt;
        gvMasterDV.DataBind();
        gvMasterDV.Selection.UnselectAll();
    }

    #region Button 觸發事件

    protected void btnNew_Click(object sender, EventArgs e)
    {
        gvMasterDV.AddNewRow();
        this.btnSave.ClientEnabled = false;
        this.btnDrop.ClientEnabled = false;
       
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        //判斷今日是否已有預訂單
        if (IsAddMode && ORD12_PageHelper.HasPreOrder(txtSTORE_NO.Text) && Session["PreOrderMID"] == null)
        {
            ClientScript.RegisterClientScriptBlock(
                this.GetType()
                , "Import"
                , "alert('預訂貨作業限制不能重覆下單，請先查詢預購單再做預購內容輸入或異動');"
                , true);

            ScriptManager.RegisterClientScriptBlock(this, GetType(), "AlertMessage", "alert('預訂貨作業限制不能重覆下單，請先查詢預購單再做預購內容輸入或異動!!');", true);

            return;
        }

        ORD12_PreOrder_DTO ds = new ORD12_PreOrder_DTO();
        ORD12_PreOrder_DTO.PRE_ORDER_MDataTable MasterDt = ds.PRE_ORDER_M;
        ORD12_PreOrder_DTO.PRE_ORDER_DDataTable DetailDt = ds.PRE_ORDER_D;

        foreach (DataColumn col in MasterDt.Columns)
        {
            col.AllowDBNull = true;
        }

        //取得主檔資訊
        ORD12_PreOrder_DTO.PRE_ORDER_MRow MasterDr = MasterDt.NewPRE_ORDER_MRow();

        MasterDr.REMARK = txtMemo.Text;

        MasterDr.MODI_USER = logMsg.MODI_USER;
        MasterDr.MODI_DTM = DateTime.Now;

        if (IsAddMode && Session["PreOrderMID"] == null)
        {
            MasterDr.PRE_ORDER_M_ID = GuidNo.getUUID();
            MasterDr.PRE_ORDER_NO = "PR" + txtSTORE_NO.Text.Replace("R", "") + "-" + SerialNo.GenNo("PRE_ORDER");
            MasterDr.STORE_NO = txtSTORE_NO.Text;
            MasterDr.STATUS = "11";
            MasterDr.ORDDATE = lblOrderDate.Text.Replace("/", "");
            MasterDr.CREATE_USER = MasterDr.MODI_USER;
            MasterDr.CREATE_DTM = MasterDr.MODI_DTM;
        }
        else
        {
            if (Session["PreOrderMID"] != null)
            {
                MasterDr.PRE_ORDER_M_ID = StringUtil.CStr(Session["PreOrderMID"]);
                MasterDr.ORDDATE = lblOrderDate.Text.Replace("/", "");
            }
            else
            {
                MasterDr.PRE_ORDER_M_ID = qPreOrderID;
                MasterDr.ORDDATE = lblOrderDate.Text.Replace("/", "");
            }
        }

        MasterDt.AddPRE_ORDER_MRow(MasterDr);

        //取得明細資訊
        int i = 1;

        foreach (DataRow dr in PreOrderDetailDt.Rows)
        {
            ORD12_PreOrder_DTO.PRE_ORDER_DRow DetailDr = DetailDt.NewPRE_ORDER_DRow();

            DetailDr.PRE_ORDER_D_ID = GuidNo.getUUID();
            DetailDr.PRE_ORDER_SEQNO = i++;
            DetailDr.PRE_ORDER_M_ID = MasterDr.PRE_ORDER_M_ID;
            DetailDr.INWAYQTY = Convert.ToInt32(dr["PurchQTY"]);
            DetailDr.ORDQTY = Convert.ToInt32(dr["PreOrderQty"]);
            DetailDr.PRODNO = StringUtil.CStr(dr["ProductNo"]);
            DetailDr.STOCK_QTY= Convert.ToInt32(dr["CurrentInvQTY"]);
            DetailDr.GIFT_FLAG = "0";

            DetailDr.MODI_USER = logMsg.MODI_USER;
            DetailDr.MODI_DTM = DateTime.Now;

            DetailDr.CREATE_USER = DetailDr.MODI_USER;
            DetailDr.CREATE_DTM = DetailDr.MODI_DTM;

            DetailDt.AddPRE_ORDER_DRow(DetailDr);
            string OneToOneSID = ORD12_PageHelper.GetOneToOneSID(StringUtil.CStr(dr["ProductNo"]));


            //如果有贈品
            if (OneToOneSID != null)
            {
                foreach (DataRow temp_giftDr in ORD12_PageHelper.GetGiftProducts(StringUtil.CStr(dr["ProductNo"]), StringUtil.CStr(dr["PreOrderQty"])).Rows)
                {
                    ORD12_PreOrder_DTO.PRE_ORDER_DRow GiftDr = DetailDt.NewPRE_ORDER_DRow();

                    GiftDr.PRE_ORDER_D_ID = GuidNo.getUUID();
                    GiftDr.PRE_ORDER_SEQNO = i++;
                    GiftDr.PRE_ORDER_M_ID = DetailDr.PRE_ORDER_M_ID;
                    GiftDr.ORDQTY = DetailDr.ORDQTY;
                    GiftDr.PRODNO = StringUtil.CStr(temp_giftDr["PRODNO"]);
                    if (!string.IsNullOrEmpty(StringUtil.CStr(temp_giftDr["S_DATE"])))                  
                        GiftDr.QTY_BDATE = StringUtil.CStr(temp_giftDr["S_DATE"]);

                    if (!string.IsNullOrEmpty(StringUtil.CStr(temp_giftDr["E_DATE"])))
                        GiftDr.QTY_EDATE = StringUtil.CStr(temp_giftDr["E_DATE"]);
                
                    GiftDr.GIFT_FLAG = "1";
                    GiftDr.PRODNO_M = DetailDr.PRODNO;

                    GiftDr.CREATE_USER = DetailDr.CREATE_USER;
                    GiftDr.CREATE_DTM = DetailDr.CREATE_DTM;

                    GiftDr.MODI_USER = GiftDr.CREATE_USER;
                    GiftDr.MODI_DTM = GiftDr.CREATE_DTM;

                    DetailDt.AddPRE_ORDER_DRow(GiftDr);
                }
            }
        }

        ds.AcceptChanges();

        if (IsAddMode && Session["PreOrderMID"] == null)
        {
            //新增預訂單
            new ORD12_Facade().InsertProOrder(ds);
        }
        else
        {
            //更新預訂單
            new ORD12_Facade().UpdateProOrder(ds);
        }

        ScriptManager.RegisterClientScriptBlock(this, GetType(), "AlertMessage", "alert('作業完成!!');location.href='../ORD12/ORD12.aspx';", true);

    }

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        if (gvMasterDV.Selection.Count < 0) { return; }

        DataTable dt = PreOrderDetailDt;

        foreach (object key in gvMasterDV.GetSelectedFieldValues(gvMasterDV.KeyFieldName))
        {
            dt.Rows.Remove(dt.Rows.Find(key));
        }

        //reflash seqno
        int i = 1;
        foreach (DataRow dr in dt.Rows)
        {
            dr["PRE_ORDER_SEQNO"] = i++;
        }

        dt.AcceptChanges();

        PreOrderDetailDt = dt;

        bindgvMaster();
        ASPxButton v_btnSave = (ASPxButton)btnSave;// gvMaster.FindTitleTemplateControl("btnSave");
        v_btnSave.ClientEnabled = true;
        ASPxButton v_btnDrop = (ASPxButton)btnDrop;// gvMaster.FindTitleTemplateControl("btnCancel");
        v_btnDrop.ClientEnabled = true;
    }

    protected void btnDrop_Click(object sender, EventArgs e)
    {
        Response.Redirect("ORD12.aspx", true);
    }

    #endregion

    #region gvMasterDV 觸發的事件

    protected void gvMasterDV_HtmlRowCreated(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewTableRowEventArgs e)
    {
        if (e.RowType == GridViewRowType.InlineEdit)
        {
            if (gvMasterDV.IsNewRowEditing)
            {
                ASPxButton ASPxButton3 = e.Row.FindChildControl<ASPxButton>("ASPxButton3");
                ASPxButton3.ClientEnabled = false;
            }
            else
            {
                string OneToOneSID = ORD12_PageHelper.GetOneToOneSID(StringUtil.CStr(e.GetValue("ProductNo")));
                if (OneToOneSID == "")
                {
                    ASPxButton ASPxButton3 = e.Row.FindChildControl<ASPxButton>("ASPxButton3");
                    ASPxButton3.ClientEnabled = false;
                }
                else
                {
                    ASPxButton ASPxButton3 = e.Row.FindChildControl<ASPxButton>("ASPxButton3");
                    ASPxButton3.ClientEnabled = true;
                }
            }
            ASPxTextBox _PreOrderQty = e.Row.FindChildControl<ASPxTextBox>("txtPreOrderQty");
            PopupControl _ProductNo = e.Row.FindChildControl<PopupControl>("txtProductNo");

            if (Session["PreOrderQty"] != null)
                _PreOrderQty.Text = StringUtil.CStr(Session["PreOrderQty"]);
            if (Session["ProductNo"] != null)
            {
                _ProductNo.Text = StringUtil.CStr(Session["ProductNo"]);
                string newOneToOneSID = ORD12_PageHelper.GetOneToOneSID(StringUtil.CStr(Session["ProductNo"]));
                if (newOneToOneSID == "")
                {
                    ASPxButton ASPxButton3 = e.Row.FindChildControl<ASPxButton>("ASPxButton3");
                    ASPxButton3.ClientEnabled = false;
                }
                else
                {
                    ASPxButton ASPxButton3 = e.Row.FindChildControl<ASPxButton>("ASPxButton3");
                    ASPxButton3.ClientEnabled = true;
                } 
            }
        }
        else if (e.RowType == GridViewRowType.Data)
        {
            string OneToOneSID = ORD12_PageHelper.GetOneToOneSID(StringUtil.CStr(e.GetValue("ProductNo")));
            if (OneToOneSID == "" || gvMasterDV.IsEditing)
            {
                ASPxButton ASPxButton3 = e.Row.FindChildControl<ASPxButton>("ASPxButton3");
                ASPxButton3.ClientEnabled = false;
            }
            else
            {
                ASPxButton ASPxButton3 = e.Row.FindChildControl<ASPxButton>("ASPxButton3");
                ASPxButton3.ClientEnabled = true;
            }
        }
        else if (e.RowType == GridViewRowType.Detail)
        {
            // 繫結明細資料表           
            ASPxGridView detailGrid = e.Row.FindChildControl<ASPxGridView>("gvDetailDV");
            detailGrid.DataSource = PreOrderDetailDt.Rows.Find(e.KeyValue)["GiftProducts"] as DataTable;
            //DataTable dt = PreOrderDetailDt.Rows.Find(e.KeyValue)[9] as DataTable;
            //DataRow dr = PreOrderDetailDt.Rows.Find(e.KeyValue);
            detailGrid.DataBind();
        }
    }

    protected void gvMasterDV_CommandButtonInitialize(object sender, ASPxGridViewCommandButtonEventArgs e)
    {
        if (e.ButtonType == ColumnCommandButtonType.Edit || e.ButtonType == ColumnCommandButtonType.SelectCheckbox)
        {
            if (gvMasterDV.IsEditing)
            {
                e.Enabled = false;
            }
            else
            {
                e.Enabled = true;
            }
        }
        if ((Session["STATUS"] == null ? "" : StringUtil.CStr(Session["STATUS"])) == "50" && (e.ButtonType == ColumnCommandButtonType.Edit || e.ButtonType == ColumnCommandButtonType.SelectCheckbox))
        {
            e.Enabled = false;
            gvMasterDV.FindChildControl<ASPxButton>("btnNew").ClientEnabled = false;
            gvMasterDV.FindChildControl<ASPxButton>("btnDelete").ClientEnabled = false;
            this.btnSave.ClientEnabled = false;
            this.btnDrop.ClientEnabled = false;
        }

    }

    protected void gvMasterDV_StartRowEditing(object sender, DevExpress.Web.Data.ASPxStartRowEditingEventArgs e)
    {
        this.btnSave.ClientEnabled = false;
        this.btnDrop.ClientEnabled = false;
        Session["ProductNo"] = null;
        Session["PreOrderQty"] = null;

        gvMasterDV.Selection.UnselectAll();
        gvMasterDV.DetailRows.CollapseAllRows();
    }

    protected void gvMasterDV_CancelRowEditing(object sender, DevExpress.Web.Data.ASPxStartRowEditingEventArgs e)
    {
        this.btnSave.ClientEnabled = true;
        this.btnDrop.ClientEnabled = true;
        gvMasterDV.DetailRows.CollapseAllRows();
    }

    protected void gvMasterDV_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
    {
        ASPxGridView grid = (ASPxGridView)sender;
        DataTable dt = Session["gvMasterDV"] as DataTable ?? null;
        DataRow[] DRSelf = dt.Select("ProductNo='" + e.OldValues["ProductNo"] + "'");
        if (DRSelf.Length > 0)
        {
            DRSelf[0]["ProductNo"] = e.NewValues["ProductNo"];
            DRSelf[0]["ProductName"] = e.NewValues["ProductName"];
            DRSelf[0]["PreOrderQty"] = e.NewValues["PreOrderQty"];

            //搭配設定SID
            string OneToOneSID = ORD12_PageHelper.GetOneToOneSID(StringUtil.CStr(e.NewValues["ProductNo"]));
            DRSelf[0]["OneToOneSID"] = OneToOneSID;


            if (string.IsNullOrEmpty((string)DRSelf[0]["OneToOneSID"]))
            {
                DRSelf[0]["hasGiftProducts"] = false;
            }
            else
            {
                DRSelf[0]["hasGiftProducts"] = true;
            }

        }
        dt.AcceptChanges();
        PreOrderDetailDt = dt;

        grid.CancelEdit();
        e.Cancel = true;
        bindgvMaster();
        Session["gvMasterDV"] = gvMasterDV.DataSource;
        this.btnSave.ClientEnabled = true;
        this.btnDrop.ClientEnabled = true;

    }

    protected void gvMasterDV_InitNewRow(object sender, DevExpress.Web.Data.ASPxDataInitNewRowEventArgs e)
    {
        gvMasterDV.Selection.UnselectAll();
        gvMasterDV.DetailRows.CollapseAllRows();
        this.btnSave.ClientEnabled = false;
        this.btnDrop.ClientEnabled = false;
        Session["ProductNo"] = null;
        Session["PreOrderQty"] = null;

        ASPxButton v_btnSave = (ASPxButton)btnSave;// gvMaster.FindTitleTemplateControl("btnSave");
        v_btnSave.ClientEnabled = false;
        ASPxButton v_btnDrop = (ASPxButton)btnDrop;// gvMaster.FindTitleTemplateControl("btnCancel");
        v_btnDrop.ClientEnabled = false;
        PopupControl txtPRODNO = gvMasterDV.FindEditRowCellTemplateControl((GridViewDataColumn)gvMasterDV.Columns["ProductNo"], "txtProductNo") as PopupControl;
        //gvMasterDV.FindEditRowCellTemplateControl (0, (GridViewDataColumn)gvMasterDV.Columns["ProductNo"], "txtProductNo") as PopupControl;
        if (txtPRODNO != null)
        {
            ASPxTextBox txtControl = txtPRODNO.FindControl("txtControl") as ASPxTextBox;
            txtControl.Focus();
        }
    }

    protected void gvMasterDV_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
    {
        ASPxGridView grid = (ASPxGridView)sender;
        DataTable dt = PreOrderDetailDt;

        DataRow DRNew = dt.NewRow();
        DRNew["PRE_ORDER_SEQNO"] = dt.Rows.Count + 1;
        DRNew["ProductNo"] = e.NewValues["ProductNo"];
        DRNew["ProductName"] = e.NewValues["ProductName"];
        DRNew["PreOrderQty"] = e.NewValues["PreOrderQty"];
        DRNew["PurchQTY"] = e.NewValues["PurchQTY"];
        DRNew["CurrentInvQTY"] = e.NewValues["CurrentInvQTY"];
        DRNew["EStoreBookQTY"] = e.NewValues["EStoreBookQTY"];

        //搭配設定SID
        string OneToOneSID = ORD12_PageHelper.GetOneToOneSID(StringUtil.CStr(e.NewValues["ProductNo"]));


        DRNew["OneToOneSID"] = OneToOneSID;


        if (string.IsNullOrEmpty((string)DRNew["OneToOneSID"]))
        {
            DRNew["hasGiftProducts"] = false;
        }
        else
        {
            DRNew["hasGiftProducts"] = true;
        }

        dt.Rows.Add(DRNew);
        dt.AcceptChanges();
        PreOrderDetailDt = dt;

        grid.CancelEdit();
        e.Cancel = true;
        bindgvMaster();
        Session["gvMasterDV"] = gvMasterDV.DataSource;
        this.btnSave.ClientEnabled = true;
        this.btnDrop.ClientEnabled = true;

    }

    protected void gvMasterDV_RowCommand(object sender, ASPxGridViewRowCommandEventArgs e)
    {
        bool show_d = true;
        if (gvMasterDV.DetailRows.IsVisible(e.VisibleIndex))
        {
            show_d = true;
        }
        else
        {
            show_d = false;
            gvMasterDV.DetailRows.ExpandRow(e.VisibleIndex);
        }
        if (gvMasterDV.IsEditing)
        {
            Session["ProductNo"] = ((PopupControl)gvMasterDV.FindEditRowCellTemplateControl((GridViewDataColumn)gvMasterDV.Columns["ProductNo"], "txtProductNo")).Text;
            Session["PreOrderQty"] = ((ASPxTextBox)gvMasterDV.FindEditRowCellTemplateControl((GridViewDataColumn)gvMasterDV.Columns["PreOrderQty"], "txtPreOrderQty")).Text;
        }
        else
        {
            for (int i = 0; i < gvMasterDV.VisibleRowCount; i++)
            {
                if (i != int.Parse(StringUtil.CStr(e.KeyValue)) - 1)
                {
                    gvMasterDV.DetailRows.CollapseRow(i);
                }
            }
            Session["ProductNo"] = StringUtil.CStr(gvMasterDV.GetRowValues(int.Parse(StringUtil.CStr(e.KeyValue)) - 1, "ProductNo"));
            Session["PreOrderQty"] = StringUtil.CStr(gvMasterDV.GetRowValues(int.Parse(StringUtil.CStr(e.KeyValue)) - 1, "PreOrderQty"));
        }


        string OneToOneSID = ORD12_PageHelper.GetOneToOneSID(StringUtil.CStr(Session["ProductNo"]));
        ASPxGridView grid = (ASPxGridView)gvMasterDV.FindDetailRowTemplateControl(int.Parse(StringUtil.CStr(e.KeyValue)) - 1, "gvDetailDV");
        grid.DataSource = ORD12_PageHelper.GetGiftProducts(StringUtil.CStr(Session["ProductNo"]), StringUtil.CStr(Session["PreOrderQty"])) as DataTable;
        grid.DataBind();

        if (show_d)
        {
            gvMasterDV.DetailRows.CollapseRow(e.VisibleIndex);
        }
        else
        {
            gvMasterDV.DetailRows.ExpandRow(e.VisibleIndex);
        }

    }

    protected void gvMasterDV_RowValidating(object sender, DevExpress.Web.Data.ASPxDataValidationEventArgs e)
    {

        foreach (DataRow dr in PreOrderDetailDt.Rows)
        {
            if (e.IsNewRow)
            {
                if (StringUtil.CStr(dr["ProductNo"]) == StringUtil.CStr(e.NewValues["ProductNo"]))
                {
                    e.RowError = "[輸入值]己存在，請重新輸入!!";
                    return;
                }
            }
            else
            {
                if (StringUtil.CStr(e.NewValues["ProductNo"]) != StringUtil.CStr(e.NewValues["ProductNo"]))
                {
                    if (StringUtil.CStr(dr["ProductNo"]) == StringUtil.CStr(e.NewValues["ProductNo"]))
                    {
                        e.RowError = "[輸入值]己存在，請重新輸入!!";
                        return;
                    }
                }
            }

        }


        string lblProductName = StringUtil.CStr(e.NewValues["ProductName"]);
        if (string.IsNullOrEmpty(lblProductName))
        {
            e.RowError = "商品編號不存在或不允許設定!!";
            return;
        }

        DataTable dt = new Product_Facade().Query_ProductPreOrderSale(StringUtil.CStr(e.NewValues["ProductNo"]));
        if (dt.Rows.Count == 0)
        {
            e.RowError = "不符合訂貨規則，請重新輸入!!";
            return;
        }
        if (StringUtil.CStr(e.NewValues["PreOrderQty"]) == "")
        {
            e.RowError = "預訂量不可空白!!";
            return;
        }
    }

    protected void grid_PageIndexChanged(object sender, EventArgs e)
    {
        gvMasterDV.CancelEdit();
        gvMasterDV.DetailRows.CollapseAllRows();
        gvMasterDV.Selection.UnselectAll();
        ASPxGridView grid = sender as ASPxGridView;
        grid.DataSource = Session["gvMasterDV"];
        grid.DataBind();
    }

    protected void gvMasterDV_CustomCallback(object source, DevExpress.Web.ASPxGridView.ASPxGridViewCustomCallbackEventArgs e)
    {
        string[] pa = e.Parameters.Split(new char[] { ';' });
        string[] vv = pa[0].Split(new char[] { '_' });
        Session["PreOrderQty"] = pa[1];
        Session["ProductNo"] = pa[0];
        if (!gvMasterDV.IsNewRowEditing && gvMasterDV.DetailRows.IsVisible(int.Parse(pa[2])))
        {
            string OneToOneSID = ORD12_PageHelper.GetOneToOneSID(pa[0]);
            ASPxGridView grid = (ASPxGridView)gvMasterDV.FindDetailRowTemplateControl(int.Parse(pa[2]), "gvDetailDV");
            if (grid != null)
            {
                grid.DataSource = ORD12_PageHelper.GetGiftProducts(pa[0], pa[1]) as DataTable;
                grid.DataBind();
            }
        }
    }

    #endregion

    protected void ASPxCallback1_Callback(object source, DevExpress.Web.ASPxCallback.CallbackEventArgs e)
    {
        if (!gvMasterDV.IsNewRowEditing)
        {
            //int i = 0;
            string[] pa = e.Parameter.Split(new char[] { ';' });
            string[] vv = pa[0].Split(new char[] { '_' });

            Session["PreOrderQty"] = pa[0];
            Session["ProductNo"] = pa[1];
            string OneToOneSID = ORD12_PageHelper.GetOneToOneSID(pa[0]);
            ASPxGridView grid = (ASPxGridView)gvMasterDV.FindDetailRowTemplateControl(int.Parse(pa[2]), "gvDetailDV");
            if (grid != null)
            {
                grid.DataSource = ORD12_PageHelper.GetGiftProducts(pa[0], pa[1]) as DataTable;

                grid.DataBind();
                gvMasterDV.DataBind();
            }
        }

    }

    protected void gvDetailDV_CustomCallback(object source, DevExpress.Web.ASPxGridView.ASPxGridViewCustomCallbackEventArgs e)
    {
        string[] pa = e.Parameters.Split(new char[] { ';' });
        string[] vv = pa[0].Split(new char[] { '_' });
        Session["PreOrderQty"] = pa[1];
        Session["ProductNo"] = pa[0];
        if (!gvMasterDV.IsNewRowEditing)
        {
            string OneToOneSID = ORD12_PageHelper.GetOneToOneSID(pa[0]);
            ASPxGridView grid = (ASPxGridView)gvMasterDV.FindDetailRowTemplateControl(int.Parse(pa[2]), "gvDetailDV");
            if (grid != null)
            {
                grid.DataSource = ORD12_PageHelper.GetGiftProducts(pa[0], pa[1]) as DataTable;
                grid.DataBind();
            }
        }
    }
    
    //ajax 呼當前網頁的方式
    [System.Web.Services.WebMethod()]
    [System.Web.Script.Services.ScriptMethod()]
    static public string getPRODNAME(string PRODUCT_NO, string STORE_NO)
    {
        string strInfo = "";
        if (!string.IsNullOrEmpty(PRODUCT_NO))
        {
            DataTable dt = new Product_Facade().Query_ProductInfo(PRODUCT_NO);
            if (dt.Rows.Count > 0)
            {
                string retProductNo = ORD12_PageHelper.HasSIMCardGroupProduct(PRODUCT_NO, STORE_NO);
                if (retProductNo == "")
                {
                    //商品名稱
                    string PName = StringUtil.CStr(dt.Rows[0]["PRODNAME"]);
                    if (PName != "" && PName != null)
                    {
                        strInfo = "|" + PName + ";";
                        //網購需求量
                        string ESQty = ORD12_PageHelper.GetEStoreBookQTY(STORE_NO, PRODUCT_NO);
                        strInfo = strInfo + (ESQty == "" ? "0" : ESQty) + ";";
                        //門市庫存量
                        string CuQty = ORD12_PageHelper.GetCurrentInvQTY(STORE_NO, PRODUCT_NO);
                        strInfo = strInfo + (CuQty == "" ? "0" : CuQty) + ";";
                        //在途量
                        string PuQty = ORD12_PageHelper.GetPurchQTY(STORE_NO, PRODUCT_NO);
                        strInfo = strInfo + (PuQty == "" ? "0" : PuQty) + ";";
                        //一搭一商品ID
                        string OneToOneSID = ORD12_PageHelper.GetOneToOneSID(PRODUCT_NO);
                        strInfo = strInfo + OneToOneSID + ";";
                    }
                }
                else
                {
                    strInfo = "fail|" +retProductNo;
                }
            }

        }

        return strInfo;
    }

    protected void gvMasterDV_HtmlRowPrepared(object sender, ASPxGridViewTableRowEventArgs e)
    {
        if (gvMasterDV.IsEditing)
        {
            e.Row.Attributes["canSelect"] = "false";
        }
        else
        {
            e.Row.Attributes["canSelect"] = "true";
        }
    }
}
