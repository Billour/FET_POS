using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using DevExpress.Web.ASPxGridView;
using DevExpress.Web.ASPxEditors;
using FET.POS.Model.Facade.FacadeImpl;
using FET.POS.Model.Common;
using FET.POS.Model.DTO;
using Advtek.Utility;
using AdvTek.CustomControls;
public partial class VSS_CONS_CON06 : BasePage
{

    //寄銷訂單編號
    private string _ORDNO = "";
    //寄銷訂單PK _CMS_ORDERM_ID_UUID
    private static string _CMS_ORDERM_ID_UUID;

    //*********************************************************************************************************
    protected void Page_Load(object sender, EventArgs e)
    {
        string dno = Request.QueryString["dno"] == null ? "" : Request.QueryString["dno"].ToString().Trim();
        this.Session["dno"] = dno;

        if (!IsPostBack)
        {
            if (this.Session["dno"].ToString() == "")
            {
                Reset(); //初始值

            }
            else
            {
                lblOrderNo.Text = this.Session["dno"].ToString();   //訂單編號
                DataTable CSM_ORDERM_dt = new CON06_Facade().Query_CsmOrderM(lblOrderNo.Text);

                if (CSM_ORDERM_dt.Rows.Count > 0)
                {
                    DataTable SUPP_dt = new Supplier_Facade().Query_SuppData_ID(CSM_ORDERM_dt.Rows[0]["SUPP_ID"].ToString());
                    txtSuppNo.Text = SUPP_dt.Rows[0]["SUPP_NO"].ToString();  //廠商編號
                    txtSuppName.Text = SUPP_dt.Rows[0]["SUPP_NAME"].ToString(); //廠商名稱
                    lbSuppId.Text = CSM_ORDERM_dt.Rows[0]["SUPP_ID"].ToString(); //廠商ID
                    txtSuppNo.Enabled = false;
                    txtSuppName.Enabled = false;
                  
                    txtMemo.Text = CSM_ORDERM_dt.Rows[0]["REMARK"].ToString(); //備註          
                    lbStoreNo.Text = CSM_ORDERM_dt.Rows[0]["STORE_NO"].ToString(); //門市

                    string strStatus = CSM_ORDERM_dt.Rows[0]["STATUS"].ToString();
                    switch (strStatus)
                    {
                        case "0":
                            lblStatus.Text = "已存檔";
                            this.btnSave.Enabled = true;
                            this.btnDrop.Enabled = true;
                            ((ASPxButton)gvMaster.FindTitleTemplateControl("btnSaleToOrder")).Enabled = true;
                            ((ASPxButton)gvMaster.FindTitleTemplateControl("btnAddNew")).Enabled = true;
                            ((ASPxButton)gvMaster.FindTitleTemplateControl("btnDelete")).Enabled = true;
                            break;
                        case "1":
                            lblStatus.Text = "轉單中";
                            this.btnSave.Enabled = false;
                            this.btnDrop.Enabled = false;
                            ((ASPxButton)gvMaster.FindTitleTemplateControl("btnSaleToOrder")).Enabled = false;
                            ((ASPxButton)gvMaster.FindTitleTemplateControl("btnAddNew")).Enabled = false;
                            ((ASPxButton)gvMaster.FindTitleTemplateControl("btnDelete")).Enabled = false;
                            break;
                        case "2":
                            lblStatus.Text = "已成單";
                            this.btnSave.Enabled = false;
                            this.btnDrop.Enabled = false;
                            ((ASPxButton)gvMaster.FindTitleTemplateControl("btnSaleToOrder")).Enabled = false;
                            ((ASPxButton)gvMaster.FindTitleTemplateControl("btnAddNew")).Enabled = false;
                            ((ASPxButton)gvMaster.FindTitleTemplateControl("btnDelete")).Enabled = false;
                            break;
                        case "3":
                            lblStatus.Text = "待進貨";
                            this.btnSave.Enabled = false;
                            this.btnDrop.Enabled = false;
                            ((ASPxButton)gvMaster.FindTitleTemplateControl("btnSaleToOrder")).Enabled = false;
                            ((ASPxButton)gvMaster.FindTitleTemplateControl("btnAddNew")).Enabled = false;
                            ((ASPxButton)gvMaster.FindTitleTemplateControl("btnDelete")).Enabled = false;
                            break;
                        case "4":
                            lblStatus.Text = "已驗收";
                            this.btnSave.Enabled = false;
                            this.btnDrop.Enabled = false;
                            ((ASPxButton)gvMaster.FindTitleTemplateControl("btnSaleToOrder")).Enabled = false;
                            ((ASPxButton)gvMaster.FindTitleTemplateControl("btnAddNew")).Enabled = false;
                            ((ASPxButton)gvMaster.FindTitleTemplateControl("btnDelete")).Enabled = false;
                            break;
                        default:
                            break;
                    }

                    lblAMOUNT_MAX_tmp.Text = SUPP_dt.Rows[0]["AMOUNT_MAX"].ToString(); //最低訂單金額暫存

                    lblUpdUser.Text = CSM_ORDERM_dt.Rows[0]["MODI_USER"].ToString() + " " + new Employee_Facade().GetEmpName(CSM_ORDERM_dt.Rows[0]["MODI_USER"].ToString()); //更新人員
                    lblUpdDateTime.Text = DateTime.Parse(CSM_ORDERM_dt.Rows[0]["MODI_DTM"].ToString()).ToString("yyyy/MM/dd HH:mm:ss"); //更新日期
                    //lblOrderDate.Text =CSM_ORDERM_dt.Rows[0]["ORDDATE"].ToString(); //訂單日期
                    lblOrderDate.Text = DateTime.ParseExact(CSM_ORDERM_dt.Rows[0]["ORDDATE"].ToString().Substring(0, 8), "yyyyMMdd", System.Globalization.CultureInfo.InvariantCulture).ToString("yyyy/MM/dd"); //訂單日期

                    DataTable dtMaster = new CON06_Facade().Query_CsmOrderD(CSM_ORDERM_dt.Rows[0]["CSM_ORDERM_ID"].ToString());
                    Session["gvMaster"] = dtMaster;
                    gvMaster.DataSource = dtMaster;
                    gvMaster.DataBind();

                }
            }

        }
        else
        {
            if (this.Session["dno"].ToString() != "")
            {
                txtSuppNo.Enabled = false;
                txtSuppName.Enabled = false;
              
            }
        }


    } 

    #region Button 觸發事件
        //儲存鈕
        protected void btnSave_Click(object sender, EventArgs e)
        {

            DataTable dtMaster;
            if (Session["gvMaster"] == null || StringUtil.CStr(Session["gvMaster"]) == "")
            {
                dtMaster = new DataTable();
            }
            else
            {
                dtMaster = Session["gvMaster"] as DataTable;
            }

            if (dtMaster.Rows.Count < 1)
            {
                ScriptManager.RegisterClientScriptBlock(this, typeof(string), "SaveError", "alert('未選擇商品不可存檔!!');", true); //[存檔訊息]
                return;
            }

       
            try
            {


                //寄銷訂單編號
                if (lblOrderNo.Text.ToString().Trim() == "")
                {
                    _ORDNO = SerialNo.GenNo("CS{0}"); //訂單編號規則:CS+廠商代碼+YYYYMMDD+2碼流水序號;
                    _ORDNO = string.Format(_ORDNO, this.txtSuppNo.Text);
                }
                else
                { _ORDNO = lblOrderNo.Text.ToString().Trim(); }

                _CMS_ORDERM_ID_UUID = GuidNo.getUUID();

                DataSet CSM_ORDER = new DataSet();

                //寄銷訂單主檔
                CSM_ORDER.Tables.Add("CSM_ORDERM");
                CSM_ORDER.Tables["CSM_ORDERM"].Columns.Add("CSM_ORDERM_ID", typeof(string)); //UUID
                CSM_ORDER.Tables["CSM_ORDERM"].Columns.Add("ORDNO", typeof(string));  //寄銷訂單編號
                CSM_ORDER.Tables["CSM_ORDERM"].Columns.Add("STORE_NO", typeof(string)); //門市代碼
                CSM_ORDER.Tables["CSM_ORDERM"].Columns.Add("ORDDATE", typeof(string)); //訂購日期
                CSM_ORDER.Tables["CSM_ORDERM"].Columns.Add("AMOUNT", typeof(int)); //訂單總價
                CSM_ORDER.Tables["CSM_ORDERM"].Columns.Add("REMARK", typeof(string)); //備註
                CSM_ORDER.Tables["CSM_ORDERM"].Columns.Add("STATUS", typeof(string)); //狀態  0:已存檔 1:轉單中 2:已成單 3:待進貨 4:已驗收
                CSM_ORDER.Tables["CSM_ORDERM"].Columns.Add("CREATE_USER", typeof(string)); //建立人員
                CSM_ORDER.Tables["CSM_ORDERM"].Columns.Add("CREATE_DTM", typeof(DateTime)); //建立時間
                CSM_ORDER.Tables["CSM_ORDERM"].Columns.Add("MODI_USER", typeof(string)); //異動人員
                CSM_ORDER.Tables["CSM_ORDERM"].Columns.Add("MODI_DTM", typeof(DateTime)); //異動時間
                CSM_ORDER.Tables["CSM_ORDERM"].Columns.Add("SUPP_ID", typeof(string)); //外部廠商ID_UUID

                DataRow ORDERM_NewRow = CSM_ORDER.Tables["CSM_ORDERM"].NewRow();
                ORDERM_NewRow["CSM_ORDERM_ID"] = _CMS_ORDERM_ID_UUID;
                ORDERM_NewRow["ORDNO"] = _ORDNO;
                ORDERM_NewRow["STORE_NO"] = this.logMsg.STORENO;
                ORDERM_NewRow["ORDDATE"] = DateTime.Now.ToString("yyyyMMdd");
                ORDERM_NewRow["AMOUNT"] = int.Parse(((ASPxLabel)gvMaster.FindFooterRowTemplateControl("lblToAMOUNT")).Text.Trim());
                ORDERM_NewRow["REMARK"] = this.txtMemo.Text.Trim();
                ORDERM_NewRow["STATUS"] = "0";
                ORDERM_NewRow["CREATE_USER"] = this.logMsg.CREATE_USER.ToString().Trim();
                ORDERM_NewRow["CREATE_DTM"] = this.logMsg.CREATE_DTM.ToString().Trim();
                ORDERM_NewRow["MODI_USER"] = this.logMsg.MODI_USER.ToString().Trim();
                ORDERM_NewRow["MODI_DTM"] = this.logMsg.MODI_DTM.ToString().Trim();
                ORDERM_NewRow["SUPP_ID"] = this.lbSuppId.Text.Trim();

                CSM_ORDER.Tables["CSM_ORDERM"].Rows.Add(ORDERM_NewRow);

                //寄銷訂單明細
                CSM_ORDER.Tables.Add("CSM_ORDERD");
                CSM_ORDER.Tables["CSM_ORDERD"].Columns.Add("CSM_ORDERD_ID", typeof(string)); //UUID
                CSM_ORDER.Tables["CSM_ORDERD"].Columns.Add("CSM_ORDERM_ID", typeof(string)); //UUID
                CSM_ORDER.Tables["CSM_ORDERD"].Columns.Add("SEQNO", typeof(string)); //訂購項次
                CSM_ORDER.Tables["CSM_ORDERD"].Columns.Add("ADVISEQTY", typeof(int)); //建議訂購量
                CSM_ORDER.Tables["CSM_ORDERD"].Columns.Add("ORDQTY", typeof(int)); //實際訂購量
                //CSM_ORDER.Tables["CSM_ORDERD"].Columns.Add("APPROVEQTY", typeof(int)); //核准量
                //CSM_ORDER.Tables["CSM_ORDERD"].Columns.Add("IN_QTY", typeof(int)); //驗收量
                //CSM_ORDER.Tables["CSM_ORDERD"].Columns.Add("OENO", typeof(string)); //訂貨單號
                CSM_ORDER.Tables["CSM_ORDERD"].Columns.Add("PRODNO", typeof(string)); //商品料號
                CSM_ORDER.Tables["CSM_ORDERD"].Columns.Add("PRODTYPENO", typeof(string)); //商品種類編號
                CSM_ORDER.Tables["CSM_ORDERD"].Columns.Add("CREATE_USER", typeof(string)); //建立人員
                CSM_ORDER.Tables["CSM_ORDERD"].Columns.Add("CREATE_DTM", typeof(DateTime)); //建立時間
                CSM_ORDER.Tables["CSM_ORDERD"].Columns.Add("MODI_USER", typeof(string)); //異動人員
                CSM_ORDER.Tables["CSM_ORDERD"].Columns.Add("MODI_DTM", typeof(DateTime)); //異動時間       
               
                int AMOUNT_tmp=0;
                for (int i = 0; i < dtMaster.Rows.Count; i++)
                {
                    DataRow ORDERD_NewRow = CSM_ORDER.Tables["CSM_ORDERD"].NewRow();
                    ORDERD_NewRow["CSM_ORDERD_ID"] = GuidNo.getUUID();
                    ORDERD_NewRow["CSM_ORDERM_ID"] = _CMS_ORDERM_ID_UUID;
                    ORDERD_NewRow["SEQNO"] = i + 1;
                    ORDERD_NewRow["ADVISEQTY"] = int.Parse(dtMaster.Rows[i]["ADVISEQTY"].ToString().Trim());
                    ORDERD_NewRow["ORDQTY"] = int.Parse(dtMaster.Rows[i]["ORDQTY"].ToString().Trim());
                    ORDERD_NewRow["PRODNO"] = dtMaster.Rows[i]["PRODNO"].ToString().Trim();
                    ORDERD_NewRow["PRODTYPENO"] = dtMaster.Rows[i]["PRODTYPENO"].ToString().Trim();
                    ORDERD_NewRow["CREATE_USER"] = this.logMsg.CREATE_USER.ToString().Trim();
                    ORDERD_NewRow["CREATE_DTM"] = this.logMsg.CREATE_DTM.ToString().Trim();
                    ORDERD_NewRow["MODI_USER"] = this.logMsg.MODI_USER.ToString().Trim();
                    ORDERD_NewRow["MODI_DTM"] = this.logMsg.MODI_DTM.ToString().Trim();
                    CSM_ORDER.Tables["CSM_ORDERD"].Rows.Add(ORDERD_NewRow);
                    AMOUNT_tmp +=  int.Parse(dtMaster.Rows[i]["AMOUNT"].ToString().Trim());
                }


                if (int.Parse(this.lblAMOUNT_MAX_tmp.Text) > AMOUNT_tmp)
                { 
                ScriptManager.RegisterClientScriptBlock(this, typeof(string), "SaveError", "alert('訂單總價低於最低訂單金額!!');", true); //[存檔訊息]

                }
                    

                int intResult = new CON06_Facade().SaveOrderData(CSM_ORDER, lblOrderNo.Text.ToString().Trim());
                if (intResult == 2)
                {
                    lblOrderNo.Text = _ORDNO;//訂單編號
                    lblUpdUser.Text = logMsg.OPERATOR + " " + new Employee_Facade().GetEmpName(logMsg.OPERATOR);
                    lblUpdDateTime.Text = this.logMsg.CREATE_DTM.ToString("yyyy/MM/dd HH:mm:ss");
                    lblOrderDate.Text = this.logMsg.CREATE_DTM.ToString("yyyy/MM/dd");
                    lblStatus.Text = "已存檔"; //狀態
                    blAMOUNT_tmp.Text = AMOUNT_tmp.ToString();

                    ScriptManager.RegisterClientScriptBlock(this, typeof(string), "Save", "alert('已存檔完成!!');", true); //[存檔訊息]
                    btnDrop.ClientEnabled = true; //作廢
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this, typeof(string), "SaveError", "alert('存檔失敗!!');", true); //[存檔訊息]

                }
       


            }
            catch (Exception)
            {

                ScriptManager.RegisterClientScriptBlock(this, typeof(string), "SaveError", "alert('存檔失敗!!');", true); //[存檔訊息]

            }
        }

        //作廢鈕
        protected void btnDrop_Click(object sender, EventArgs e)
        {
            if (lblOrderNo.Text.Trim() == "")
            {
                ScriptManager.RegisterClientScriptBlock(this, typeof(string), "DropError", "alert('無此訂單，無法作廢!!');", true); //[作廢訊息]
                return;
            }

            try
            {
                new CON06_Facade().DropOrderData(lblOrderNo.Text.ToString().Trim());
                ScriptManager.RegisterClientScriptBlock(this, typeof(string), "DropError", "alert('作廢完成!!');", true); //[作廢訊息]
                Reset(); //回復初始值

            }
            catch (Exception)
            {

                ScriptManager.RegisterClientScriptBlock(this, typeof(string), "DropError", "alert('作廢失敗!!');", true); //[作廢訊息]
            }

        }

        //清空鈕
        protected void btnClear_Click(object sender, EventArgs e)
        {
            Reset(); //回復初始值
        }

        //刪除鈕
        protected void btnDelete_Click(object sender, EventArgs e)
        {
            List<object> gvPKValues = gvMaster.GetSelectedFieldValues(gvMaster.KeyFieldName);
            string pkFName = gvMaster.KeyFieldName;

            DataTable dtMaster;
            if (Session["gvMaster"] == null || StringUtil.CStr(Session["gvMaster"]) == "")
            {
                dtMaster = new DataTable();
            }
            else
            {
                dtMaster = Session["gvMaster"] as DataTable;
            }

            if (dtMaster.Rows.Count > 0)
            {

                List<object> keyValues = this.gvMaster.GetSelectedFieldValues(gvMaster.KeyFieldName);
                foreach (string key in keyValues)
                {
                    DataRow drSYS = dtMaster.Select("PRODNO='" + key.ToString() + "'")[0];
                    dtMaster.Rows.Remove(drSYS);

                }

                gvMaster.Selection.UnselectAll();
                Session["gvMaster"] = dtMaster;
                gvMaster.DataSource = Session["gvMaster"];
                gvMaster.DataBind();

            }

        }

        //新增鈕
        protected void btnAddNew_Click(object sender, EventArgs e)
        {
            gvMaster.AddNewRow();

        }

        //銷售轉訂單鈕
        protected void btnSaleToOrder_Click(object sender, EventArgs e)
        {
            DataTable dtMaster;
            if (Session["gvMaster"] == null || StringUtil.CStr(Session["gvMaster"]) == "")
            {
                dtMaster = new DataTable();
            }
            else
            {
                dtMaster = Session["gvMaster"] as DataTable;
            }

            if (dtMaster.Rows.Count < 1)
            {
                ScriptManager.RegisterClientScriptBlock(this, typeof(string), "SaleToOrderError", "alert('未選擇商品無法銷售轉訂單!!');", true); //[存檔訊息]
                return;
            }

            bool chORDQTY = false;
            for (int i = 0; i < dtMaster.Rows.Count; i++)
            {
                int ORDQTY = new CON06_Facade().SaleToOrder(dtMaster.Rows[i]["PRODNO"].ToString(), this.logMsg.STORENO, this.lbSuppId.Text);
                if (ORDQTY > 0)
                {
                    dtMaster.Rows[i]["ORDQTY"] = ORDQTY;
                    dtMaster.Rows[i]["AMOUNT"] = int.Parse(dtMaster.Rows[i]["PRICE"].ToString()) * ORDQTY;
                    chORDQTY = true;
                }
            
            }

            if (chORDQTY)
            {
                dtMaster.AcceptChanges();

                Session["gvMaster"] = dtMaster;
                gvMaster.DataSource = dtMaster;
                gvMaster.DataBind();
            }
        }
    #endregion

    #region gvMaster事件
        protected void gvMaster_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
        {

            DataTable dtMaster;
            if (Session["gvMaster"] == null || StringUtil.CStr(Session["gvMaster"]) == "")
            {
                dtMaster = new DataTable();
            }
            else
            {
                dtMaster = Session["gvMaster"] as DataTable;
            }
            int ToAMOUNT = 0;

            DataRow NewRow = dtMaster.NewRow();
            NewRow["PRODNO"] = e.NewValues["PRODNO"].ToString().Trim();
            NewRow["PRODNAME"] = e.NewValues["PRODNAME"].ToString().Trim();
            NewRow["PRODTYPENO"] = e.NewValues["PRODTYPENO"].ToString().Trim();
            NewRow["PRODTYPENAME"] = e.NewValues["PRODTYPENAME"].ToString().Trim();
            NewRow["ADVISEQTY"] = e.NewValues["ADVISEQTY"].ToString().Trim();
            NewRow["ORDQTY"] = e.NewValues["ORDQTY"].ToString().Trim();
            NewRow["PRICE"] = e.NewValues["PRICE"].ToString().Trim();
            NewRow["AMOUNT"] = e.NewValues["AMOUNT"].ToString().Trim();

            dtMaster.Rows.Add(NewRow);
            foreach (DataRow dr in dtMaster.Rows)
            { 
            ToAMOUNT += int.Parse(dr["AMOUNT"].ToString());
            }
            blAMOUNT_tmp.Text = ToAMOUNT.ToString();
            ((ASPxLabel)gvMaster.FindFooterRowTemplateControl("lblToAMOUNT")).Text = ToAMOUNT.ToString();
            gvMaster.CancelEdit();
            e.Cancel = true;
            Session["gvMaster"] = dtMaster;
            gvMaster.DataSource = dtMaster;
            gvMaster.DataBind();

        }

        protected void gvMaster_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
        {
            DataTable dtMaster;
            if (Session["gvMaster"] == null || StringUtil.CStr(Session["gvMaster"]) == "")
            {
                dtMaster = new DataTable();
            }
            else
            {
                dtMaster = Session["gvMaster"] as DataTable;
            }


            for (int i = 0; i < dtMaster.Rows.Count; i++)
            {
                DataRow dr = dtMaster.Rows[i];
                if (dr["PRODNO"].ToString().CompareTo(e.OldValues["PRODNO"].ToString()) == 0)
                {

                    dr["PRODNO"] = e.NewValues["PRODNO"].ToString().Trim();
                    dr["PRODNAME"] = e.NewValues["PRODNAME"].ToString().Trim();
                    dr["PRODTYPENO"] = e.NewValues["PRODTYPENO"].ToString().Trim();
                    dr["PRODTYPENAME"] = e.NewValues["PRODTYPENAME"].ToString().Trim();
                    dr["ADVISEQTY"] = e.NewValues["ADVISEQTY"].ToString().Trim();
                    dr["ORDQTY"] = e.NewValues["ORDQTY"].ToString().Trim();
                    dr["PRICE"] = e.NewValues["PRICE"].ToString().Trim();
                    dr["AMOUNT"] = e.NewValues["AMOUNT"].ToString().Trim();

                    dtMaster.AcceptChanges();
                    break;
                }

            }


            gvMaster.CancelEdit();
            e.Cancel = true;
            Session["gvMaster"] = dtMaster;
            gvMaster.DataSource = dtMaster;
            gvMaster.DataBind();
        }

        protected void gvMaster_RowValidating(object sender, DevExpress.Web.Data.ASPxDataValidationEventArgs e)
        {
            string ProdNo = e.NewValues["PRODNO"].ToString();

            if (!string.IsNullOrEmpty(ProdNo))
            {
                DataTable dtProd = CON06_PageHelper.GetProdDataByKey(ProdNo, lbSuppId.Text);
                if (dtProd.Rows.Count > 0)
                {
                    if (gvMaster.IsNewRowEditing)
                    {
                        DataTable dt = (DataTable)Session["gvMaster"];
                        int rowCount = dt.Rows.Count;
                        for (int i = 0; i < rowCount; i++)
                        {
                            if (ProdNo == dt.Rows[i].ItemArray[1].ToString())
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

        protected void gvMaster_StartRowEditing(object sender, DevExpress.Web.Data.ASPxStartRowEditingEventArgs e)
        {

            gvMaster.Selection.UnselectAll();
        }

        protected void gvMaster_HtmlRowPrepared(object sender, ASPxGridViewTableRowEventArgs e)
        {

            //給最低訂單金額
            ((ASPxLabel)gvMaster.FindFooterRowTemplateControl("lblAMOUNT_MAX")).Text = lblAMOUNT_MAX_tmp.Text;

            e.Row.Attributes["canSelect"] = "true";

            string STATUS = lblStatus.Text;
            if (e.RowType == GridViewRowType.Data)
            {
                if (STATUS == "已存檔" || STATUS == "未存檔")
                {
                    e.Row.Attributes["canSelect"] = "true";
                }
                else
                {
                    e.Row.Attributes["canSelect"] = "false";

                }
            }

            if (e.RowType == GridViewRowType.Data || e.RowType == GridViewRowType.InlineEdit)
            {

                //string s = gvMaster.GetRowValues(e.VisibleIndex, "STATUS").ToString();

                //給訂單總價值
                //((ASPxLabel)gvMaster.FindFooterRowTemplateControl("lblToAMOUNT")).Text = (int.Parse(((ASPxLabel)gvMaster.FindFooterRowTemplateControl("lblToAMOUNT")).Text) + int.Parse(gvMaster.GetRowValues(e.VisibleIndex, "AMOUNT").ToString())).ToString();
                ((ASPxLabel)gvMaster.FindFooterRowTemplateControl("lblToAMOUNT")).Text = blAMOUNT_tmp.Text;

            }
            if (e.RowType == GridViewRowType.InlineEdit)
            {
                PopupControl txtPRODNO = e.Row.FindChildControl<PopupControl>("txtPRODNO");
                ASPxPopupControl ASPxPopupControl1 = txtPRODNO.FindChildControl<ASPxPopupControl>("ASPxPopupControl1");
                ASPxPopupControl1.ContentUrl = "~/VSS/Common/ProductsPopup3.aspx?SysDate=Date()&KeyFieldValue1=" + txtPRODNO.KeyFieldValue1 + "&KeyFieldValue2=" + this.lbSuppId.Text;

            }

        }

        protected void gvMaster_PageIndexChanged(object sender, EventArgs e)
        {

            gvMaster.DataSource = Session["gvMaster"];
            gvMaster.DataBind();
        }

        protected void gvMaster_CancelRowEditing(object sender, DevExpress.Web.Data.ASPxStartRowEditingEventArgs e)
        {
            this.btnSave.ClientEnabled = true;
            this.btnDrop.ClientEnabled = true;
            this.btnClear.ClientEnabled = true;

        }

        protected void gvMaster_InitNewRow(object sender, DevExpress.Web.Data.ASPxDataInitNewRowEventArgs e)
        {

            //PopupControl txtPRODNO = e.Row.FindChildControl<PopupControl>("txtPRODNO");
            //ASPxPopupControl ASPxPopupControl1 = txtPRODNO.FindChildControl<ASPxPopupControl>("ASPxPopupControl1");
            //ASPxPopupControl1.ContentUrl = "~/VSS/Common/ProductsPopup3.aspx?SysDate=Date()&KeyFieldValue2=" + this.lbSuppId.Text;
            this.btnSave.ClientEnabled = false;
            this.btnDrop.ClientEnabled = false;
            this.btnClear.ClientEnabled = false;

        }

        protected void gvMaster_CommandButtonInitialize(object sender, ASPxGridViewCommandButtonEventArgs e)
        {
            if (e.VisibleIndex > -1)
            {
                string STATUS = lblStatus.Text;

                if (e.ButtonType == ColumnCommandButtonType.Edit || e.ButtonType == ColumnCommandButtonType.SelectCheckbox)
                {
                    if (STATUS == "已存檔" || STATUS == "未存檔")
                    {
                        e.Enabled = true;
                    }
                    else
                    {
                        e.Enabled = false;
                    }
                }
            }
        }

        protected void gvMaster_DataBound(object sender, EventArgs e)
        {
            DataTable dtMaster;
            if (Session["gvMaster"] == null || StringUtil.CStr(Session["gvMaster"]) == "")
            {
                dtMaster = new DataTable();
            }
            else
            {
                dtMaster = Session["gvMaster"] as DataTable;
            }

            //計算[訂單總價]
            int tmpToAMOUNT = 0;

            for (int i = 0; i < dtMaster.Rows.Count; i++)
            {
                tmpToAMOUNT += int.Parse(dtMaster.Rows[i]["AMOUNT"].ToString());
            }
            blAMOUNT_tmp.Text = tmpToAMOUNT.ToString();

        }

    #endregion

    #region ajax 呼當前網頁
        //ajax 呼當前網頁的方式
        [System.Web.Services.WebMethod()]
        [System.Web.Script.Services.ScriptMethod()]
        static public string getProductsInfo(string strData)
        {
            string strInfo = "";
            try
            {
                string[] pa = strData.ToString().Split(new char[] { ';' });
                string ProdNo = "";
                ProdNo = pa[0].ToString();
                if (!string.IsNullOrEmpty(ProdNo))
                {

                    DataTable dtProd = CON06_PageHelper.GetProdDataByKey(ProdNo, pa[2].ToString());
                    if (dtProd.Rows.Count > 0)
                    {
                        //商品名稱∩商品類別∩建議訂購量∩單價

                        //串商品名稱
                        strInfo = dtProd.Rows[0]["PRODNAME"].ToString();

                        //串商品類別
                        DataTable dtProdType = new CON06_Facade().Query_CsmProductType(dtProd.Rows[0]["PRODTYPENO"].ToString());
                        if (dtProdType.Rows.Count > 0)
                            strInfo = strInfo + "∩" + dtProd.Rows[0]["PRODTYPENO"].ToString() + "_" + dtProdType.Rows[0]["PRODTYPENAME"].ToString();
                        //strInfo = strInfo + "∩" + dtProdType.Rows[0]["PRODTYPENAME"].ToString();

                        else
                            strInfo = strInfo + "∩";

                        //串建議訂購量              
                        string strStoreAtr = new CON06_Facade().Query_StoreAtr(ProdNo, pa[1].ToString()); //帶入 商品料號,門市編號
                        strInfo = strInfo + "∩" + strStoreAtr;

                        //串單價
                        strInfo = strInfo + "∩" + dtProd.Rows[0]["PRICE"].ToString();
                    }
                }
            }
            catch (Exception)
            {
                strInfo = "";
            }
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

                    DataTable dtSupp = CON06_PageHelper.GetSuppDataByKey(SuppNo);
                    if (dtSupp.Rows.Count > 0)
                    {
                        strInfo = dtSupp.Rows[0]["SUPP_ID"].ToString() + "∩" + dtSupp.Rows[0]["SUPP_NAME"].ToString() + "∩" + dtSupp.Rows[0]["AMOUNT_MAX"].ToString();
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

    //前端回傳處理後端資料
    protected void ac1_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
    {

        // 繫結空的資料表，以顯示表頭欄位
        DataTable dtGvMaster = new DataTable();
        dtGvMaster = GetEmptyDataTable();
        Session["gvMaster"] = dtGvMaster;
        gvMaster.DataSource = dtGvMaster;
        gvMaster.DataBind();
        gvMaster.Selection.UnselectAll();
        gvMaster.CancelEdit();
    }

    //組空DataTable
    private DataTable GetEmptyDataTable()
    {
        DataTable dtMaster = new DataTable();
        dtMaster.Columns.Clear();
        dtMaster.Columns.Add("ITEMS", typeof(int));
        dtMaster.Columns.Add("PRODNO", typeof(string));
        dtMaster.Columns.Add("PRODNAME", typeof(string));
        dtMaster.Columns.Add("PRODTYPENO", typeof(string));
        dtMaster.Columns.Add("PRODTYPENAME", typeof(string));
        dtMaster.Columns.Add("ADVISEQTY", typeof(int));
        dtMaster.Columns.Add("ORDQTY", typeof(int));
        dtMaster.Columns.Add("PRICE", typeof(int));
        dtMaster.Columns.Add("AMOUNT", typeof(int));
        return dtMaster;
    }

    //回復初始
    protected void Reset()
    {
        txtSuppNo.Text = "";  //廠商編號
        txtSuppName.Text = ""; //廠商名稱
        lbSuppId.Text = ""; //廠商ID
        txtMemo.Text = ""; //備註
        lblOrderNo.Text = "";  //訂單編號
        lbStoreNo.Text = logMsg.STORENO; //門市
        lblStatus.Text = "未存檔"; //狀態
        lblAMOUNT_MAX_tmp.Text = "0"; //最低訂單金額暫存
        lblUpdUser.Text = logMsg.OPERATOR + " " + new Employee_Facade().GetEmpName(logMsg.OPERATOR); //更新人員
        lblUpdDateTime.Text = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"); //更新日期
        lblOrderDate.Text = DateTime.Now.ToString("yyyy/MM/dd"); //訂單日期
        // 繫結空的資料表，以顯示表頭欄位
        DataTable dtGvMaster = new DataTable();
        dtGvMaster = GetEmptyDataTable();
        Session["gvMaster"] = dtGvMaster;
        gvMaster.DataSource = dtGvMaster;
        gvMaster.DataBind();
        this.btnSave.Enabled = true;
        this.btnDrop.Enabled = true;
        ((ASPxButton)gvMaster.FindTitleTemplateControl("btnSaleToOrder")).Enabled = true;
        ((ASPxButton)gvMaster.FindTitleTemplateControl("btnAddNew")).Enabled = true;
        ((ASPxButton)gvMaster.FindTitleTemplateControl("btnDelete")).Enabled = true;
        txtSuppNo.Enabled = true;
        txtSuppName.Enabled = true;
       
    }
}
