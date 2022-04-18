using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Advtek.Utility;
using DevExpress.Web.ASPxGridView;
using System.Drawing;
using DevExpress.Web.ASPxEditors;
using System.Collections.Specialized;
using FET.POS.Model.DTO;
using FET.POS.Model.Common;
using FET.POS.Model.Facade.FacadeImpl;

public partial class VSS_ORD_ORD01_ORD01 : BasePage
{
    /// <summary>
    /// 門市訂貨單ID_UUID
    /// </summary>
    private string ORDER_TEMP_ID
    {
        get
        {
            if (Session["ORDER_TEMP_ID"] == null)
            {
                Session["ORDER_TEMP_ID"] = "";
            }

            return (string)Session["ORDER_TEMP_ID"];
        }
        set
        {
            Session["ORDER_TEMP_ID"] = value;
        }
    }

    /// <summary>
    /// 訂單主檔ID
    /// </summary>
    private string qOrderID
    {
        get
        {
            string Result = string.Empty;
            if (!string.IsNullOrEmpty(Request.QueryString["Param"]))
            {
                NameValueCollection qscoll = Utils.Param_UrlDecode(Request.QueryString["Param"]);
                foreach (string key in qscoll.AllKeys)
                {
                    if (key == "ordid")
                    {
                        Result = string.Join(",", qscoll.GetValues(key));
                        break;
                    }
                }
            }
            return Result;
        }
    }

    /// <summary>
    /// 訂單主檔TYPE
    /// </summary>
    private string qOrderType
    {
      get
        {
            string Result = string.Empty;
            if (!string.IsNullOrEmpty(Request.QueryString["Param"]))
            {
                NameValueCollection qscoll = Utils.Param_UrlDecode(Request.QueryString["Param"]);
                foreach (string key in qscoll.AllKeys)
                {
                    if (key == "order_type")
                    {
                        Result = string.Join(",", qscoll.GetValues(key));
                        break;
                    }
                }
            }
            return Result;
        }
        
    }


    /// <summary>
    /// 是否為新增模式
    /// </summary>
    private bool IsAddMode
    {
        get
        {
            return (qOrderID == "");
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        //先取得該門市資訊，若查無資料，表示該門市不存在或暫停營業
        //DataTable dtStore = new Store_Facade().Query_StoreName(logMsg.STORENO);
        int storeStatus = new Store_Facade().Query_StoreStatus(logMsg.STORENO);
        if (storeStatus != 0)
        {
            btnSave.Visible = false;
            btnDrop.Visible = false;
            btnQueryEdit.Visible = false;
            btnTransfer.Visible = false;
            if (storeStatus == 1)
                ScriptManager.RegisterClientScriptBlock(this, typeof(string), "selectSALE_NO", "alert('現在為門市暫停營業期間，無法訂貨!');", true);
            else if (storeStatus == 2)
                ScriptManager.RegisterClientScriptBlock(this, typeof(string), "selectSALE_NO", "alert('此門市已關閉，無法訂貨!');", true);
            return;
        }

        for (int i = 0; i < drMasterDV.VisibleRowCount; i++)
        {
            if (((ASPxTextBox)drMasterDV.FindRowCellTemplateControl(i, (GridViewDataColumn)drMasterDV.Columns["ORDQTY"], "txtOrdqty")) != null)
            {
                DataTable dt1 = new DataTable();
                dt1 = Session["gvMaster"] as DataTable;

                if (((ASPxLabel)drMasterDV.FindRowCellTemplateControl(i, (GridViewDataColumn)drMasterDV.Columns["ITEMNO"], "lblITEMNO")).Text != "")
                {
                    DataRow[] drs = dt1.Select("ITEMNO='" + int.Parse(((ASPxLabel)drMasterDV.FindRowCellTemplateControl(i, (GridViewDataColumn)drMasterDV.Columns["ITEMNO"], "lblITEMNO")).Text) + "'");

                    if (drs.Length > 0)
                    {
                        drs[0]["ORDQTY"] = (((ASPxTextBox)drMasterDV.FindRowCellTemplateControl(i, (GridViewDataColumn)drMasterDV.Columns["ORDQTY"], "txtOrdqty")).Text == "" ? "0" : ((ASPxTextBox)drMasterDV.FindRowCellTemplateControl(i, (GridViewDataColumn)drMasterDV.Columns["ORDQTY"], "txtOrdqty")).Text);

                    }

                    dt1.AcceptChanges();

                    Session["gvMaster"] = dt1;
                }
            }

        }

        if (!IsPostBack)
        {
            ORD01_Facade _facade = new ORD01_Facade();

            divContent.Style["display"] = "";

            btnSave.Visible = true;
            btnDrop.Visible = true;
            btnTransfer.Visible = true;
            //繫結主檔
            DataRow item = null;

            if (!IsAddMode)  //由查詢頁面過來
            {
                ORDER_TEMP_ID = qOrderID;
                if (qOrderType == "1")
                {
                    ORDER_TEMP_ID = _facade.GetOrderId(qOrderID, logMsg.STORENO);
                    item = _facade.GetOrderMTempBy(ORDER_TEMP_ID).Rows[0];
                }
                else
                {
                    item = _facade.GetOrderMTempBy1(ORDER_TEMP_ID).Rows[0];
                    btnSave.Visible = false;
                    btnDrop.Visible = false;
                    btnQueryEdit.Visible = false;
                    btnTransfer.Visible = false;
                }

            }
            else //直接至訂單頁面
            {
                //門市ATR最後轉入時間一定要是當天
                string LastAtrTranToStore = _facade.GetParaValue("LAST_ATR_TRAN_TO_STORE");
                bool IsToday = System.DateTime.ParseExact(LastAtrTranToStore.Substring(0, 8), "yyyyMMdd", System.Globalization.CultureInfo.InvariantCulture).Date == DateTime.Now.Date;
                DateTime StoreOrderStartTime = System.DateTime.ParseExact(LastAtrTranToStore.Substring(0, 8) + _facade.GetParaValue("STORE_ORDER_S_DTM"), "yyyyMMddHHmm", System.Globalization.CultureInfo.InvariantCulture);
                DateTime StoreOrderEndTime = System.DateTime.ParseExact(LastAtrTranToStore.Substring(0, 8) + _facade.GetParaValue("STORE_ORDER_E_DTM"), "yyyyMMddHHmm", System.Globalization.CultureInfo.InvariantCulture);
                bool IsDuration = (StoreOrderStartTime <= DateTime.Now && StoreOrderEndTime >= DateTime.Now);
                if (!(IsToday && IsDuration))
                {
                    //@@@@@@@@@@@
                    if (Session["isRunOrd01"] != null && StringUtil.CStr(Session["isRunOrd01"]) == "Y")
                    {
                        ScriptManager.RegisterClientScriptBlock(this, GetType(), "AlertMessage", "alert('現在為預訂時間，請至預訂貨作業進行預訂!!');", true);
                        Session.Remove("isRunOrd01");
                    }
                    else
                    {
                        Session["isRunOrd01"] = "Y";
                        ScriptManager.RegisterClientScriptBlock(this, GetType(), "AlertMessage", "alert('現在為預訂時間，請至預訂貨作業進行預訂!!');location.href='../ORD12/ORD12.aspx';", true);
                    }
                    btnSave.ClientEnabled = false;
                    btnDrop.ClientEnabled = false;
                    btnQueryEdit.ClientEnabled = false;
                    btnTransfer.ClientEnabled = false;
                    return;
                    //@@@@@@@@@@@@@
                }

                //取得該門市 當日的訂單
                string WorkDate = OracleDBUtil.WorkDay(logMsg.STORENO); //營業日
                if (_facade.IsExist_OrderMDOver(logMsg.STORENO, WorkDate.Replace("/", "")))
                {
                    btnSave.Visible = false;
                    btnDrop.Visible = false;
                    btnQueryEdit.Visible = false;
                    btnTransfer.Visible = false;
                    if (Session["UPOK"] == null)
                    {
                        ScriptManager.RegisterClientScriptBlock(this, GetType(), "AlertMessage", "alert('今日已訂貨完成，無法訂貨!!');location.href='../ORD02/ORD02.aspx';", true);
                    }
                    Session.Remove("UPOK");
                }
                item = _facade.GetTodayOrder(logMsg.STORENO, WorkDate.Replace("/", ""), logMsg.CREATE_USER).Rows[0];
                lbStoreNo.Text = logMsg.STORENO;
            }

            if (item != null)
            {
                lblOrderNo.Text = StringUtil.CStr(item["ORDER_NO"]);
                string tmpDate = StringUtil.CStr(item["ORDDATE"]);
                lblOrderDate.Text = tmpDate.Substring(0, 4) + "/" + tmpDate.Substring(4, 2) + "/" + tmpDate.Substring(6, 2);
                txtMemo.Text = StringUtil.CStr(item["REMARK"]);
                if (StringUtil.CStr(item.ItemArray[10]) != "")
                    lblUpdDateTime.Text = Convert.ToDateTime(item["MODI_DTM"]).ToString("yyyy/MM/dd HH:mm:ss");
                else
                    lblUpdDateTime.Text = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");
                if (StringUtil.CStr(item["MODI_USER"]) != "")
                    lblUpdUser.Text = StringUtil.CStr(item["MODI_USER"]) + " " + new Employee_Facade().GetEmpName(StringUtil.CStr(item["MODI_USER"]));
                else
                    lblUpdUser.Text = logMsg.OPERATOR + " " + new Employee_Facade().GetEmpName(logMsg.OPERATOR);
                ORDER_TEMP_ID = StringUtil.CStr(item["ORDER_TEMP_ID"]);

                //繫結明細
                bindgvMaster();

                if (StringUtil.CStr(item["ISOK"]) == "1")
                {
                    btnSave.Enabled = false;
                    btnQueryEdit.Enabled = false;
                }
            }
        }
    }

    private void bindgvMaster()
    {
        DataTable dt = new DataTable();
        if (IsAddMode) //直接至訂單頁面
        {
            dt = new ORD01_Facade().GetTodayAvailableOrderItems(logMsg.STORENO, lblOrderDate.Text.Replace("/", ""), logMsg.CREATE_USER);
            drMasterDV.DataSource = dt;
        }
        else
        {
            if (qOrderType == "1")
            {
                dt = new ORD01_Facade().GetOrderMDTempBy(ORDER_TEMP_ID, logMsg.STORENO);
            }
            else
            {
                dt = new ORD01_Facade().GetOrderMDTempBy1(ORDER_TEMP_ID, logMsg.STORENO);
            }

            drMasterDV.DataSource = dt;
        }

        //如果沒資料 不可存檔
        if (dt.Rows.Count < 1) 
        {
            btnSave.ClientEnabled = false;
            btnDrop.ClientEnabled = false;           
            btnTransfer.ClientEnabled = false;
        }

        Session["gvMaster"] = dt;
        drMasterDV.DataBind();
    }

    private void CheckIsOkState(ref ASPxGridViewTableRowEventArgs e)
    {
        if (btnSave.Enabled == false)
        {
            if (btnSave.Enabled == true)
                btnSave.Enabled = false;
            if (btnQueryEdit.Enabled == true)
                btnQueryEdit.Enabled = false;

            ASPxTextBox Ctl_ASPxTextBox = e.Row.FindChildControl<ASPxTextBox>("txtOrdqty");
            if (Ctl_ASPxTextBox != null)
                Ctl_ASPxTextBox.ReadOnly = true;
        }
    }

    #region Button 觸發事件

    protected void btnSave_Click(object sender, EventArgs e)
    {
        ORD01_OrderMD_DTO ds = new ORD01_OrderMD_DTO();
        ORD01_OrderMD_DTO.ORDER_M_TEMPDataTable MasterTable = ds.ORDER_M_TEMP;
        ORD01_OrderMD_DTO.ORDER_D_TEMPDataTable DetailTable = ds.ORDER_D_TEMP;

        foreach (DataColumn col in MasterTable.Columns)
        {
            col.AllowDBNull = true;
        }

        foreach (DataColumn col in DetailTable.Columns)
        {
            col.AllowDBNull = true;
        }


        //更新註解(主檔)
        ORD01_OrderMD_DTO.ORDER_M_TEMPRow MasterRow = MasterTable.NewORDER_M_TEMPRow();

        MasterRow.ORDER_TEMP_ID = ORDER_TEMP_ID;
        MasterRow.REMARK = txtMemo.Text;
        MasterRow.MODI_DTM = DateTime.Now;
        MasterRow.MODI_USER = logMsg.CREATE_USER;
        MasterRow.ORDER_NO = new ORD01_Facade().GetParaValue("STORE_ORDER_NO");
        MasterRow.STATUS = "10";
        MasterRow.ISOK = "0";
        if (((ASPxButton)sender).CommandName == "commit")  //傳輸
        {
            MasterRow.ISOK = "1";
            MasterRow.STATUS = "50";
        }

        MasterTable.AddORDER_M_TEMPRow(MasterRow);

        DataTable dtMaster = Session["gvMaster"] as DataTable;

        int end = dtMaster.Rows.Count;

        for (int i = 0; i < end; i++)
        {
            ORD01_OrderMD_DTO.ORDER_D_TEMPRow DetailRow = DetailTable.NewORDER_D_TEMPRow();
            DataRow[] drs = DetailTable.Select("ORDER_D_TEMP_ID='" + StringUtil.CStr(dtMaster.Rows[i]["ORDER_D_TEMP_ID"]) + "'");
            if (drs.Length == 0)
            {

                DetailRow.ORDER_D_TEMP_ID = StringUtil.CStr(dtMaster.Rows[i]["ORDER_D_TEMP_ID"]);
                DetailRow.ORDQTY = Convert.ToDecimal(StringUtil.CStr(dtMaster.Rows[i]["ORDQTY"]));
                DetailRow.MODI_USER = logMsg.CREATE_USER;
                DetailRow.MODI_DTM = DateTime.Now;

                DetailTable.AddORDER_D_TEMPRow(DetailRow);
            }
            // 搭贈
            string prdno = StringUtil.CStr(dtMaster.Rows[i]["PRODNO"]);
            ORD01_Facade _facade = new ORD01_Facade();

            DataTable dt = new DataTable();
            dt = _facade.GetGiftORDER_D_TEMP_ID(ORDER_TEMP_ID, prdno, logMsg.STORENO, lblOrderDate.Text.Replace("/", ""), StringUtil.CStr(dtMaster.Rows[i]["ORDER_D_TEMP_ID"]));

            //***20110417拿掉檢查StroeAtr的量
            //string OneToOne_prdno = "";
            //OneToOne_prdno = new ORD01_Facade().Check_StoreAtr(prdno, logMsg.STORENO, StringUtil.CStr(dtMaster.Rows[i]["ORDQTY"]), this.lblOrderDate.Text);
            //if (!string.IsNullOrEmpty(OneToOne_prdno)) //商品料號,門市編號,輸入數量 # 搭贈商品ATR量檢查                      
            //{
            //    ScriptManager.RegisterClientScriptBlock(this, GetType(), "AlertMessage", "alert('【" + prdno + "】" + OneToOne_prdno + "');", true);
            //    return;
            //}
            //***

           if ((Convert.ToInt32(dtMaster.Rows[i]["CHECK_IN_QTY"]) + Convert.ToInt32(dtMaster.Rows[i]["ORDQTY"]))> Math.Ceiling(Convert.ToInt32(dtMaster.Rows[i]["ADVISEQTY"]) * 1.5))
            {
              ScriptManager.RegisterClientScriptBlock(this, GetType(), "AlertMessage", "alert('【" + prdno + "】當日訂購量不允許大於[建議訂購量1.5倍(無條件進位)-當日總訂購量]，請重新輸入');", true);
                return;
            }
            if (dt.Rows.Count > 0)
            {

                for (int j = 0; j < dt.Rows.Count; j++)
                {
                    DataRow[] drs1 = DetailTable.Select("ORDER_D_TEMP_ID='" + StringUtil.CStr(dt.Rows[j]["ORDER_D_TEMP_ID"]) + "'");
                    if (drs1.Length == 0)
                    {
                        DetailRow = DetailTable.NewORDER_D_TEMPRow();
                        DetailRow.ORDER_D_TEMP_ID = StringUtil.CStr(dt.Rows[j]["ORDER_D_TEMP_ID"]);
                        //DetailRow.PRE_ORDER_D_ID = StringUtil.CStr(dt.Rows[j]["SID"]);
                        DetailRow.ORDQTY = Convert.ToDecimal(StringUtil.CStr(dtMaster.Rows[i]["ORDQTY"]));
                        DetailRow.MODI_USER = logMsg.CREATE_USER;
                        DetailRow.MODI_DTM = DateTime.Now;

                        DetailTable.AddORDER_D_TEMPRow(DetailRow);
                    }
                }

            }
        }

        //變更DS
        ds.AcceptChanges();

        if (((ASPxButton)sender).CommandName == "save")
        {
            //儲存
            new ORD01_Facade().Update_TodayAvailableOrderItems(ds, logMsg.STORENO, lblOrderDate.Text.Replace("/", ""), logMsg.CREATE_USER);


            Session["IsSave"] = "1";
            if (IsAddMode)
            {
                ScriptManager.RegisterClientScriptBlock(this, GetType(), "AlertMessage", "alert('作業完成!!');location.href='../ORD01/ORD01.aspx';", true);
            }
            else
            {
                string encryptUrl = Utils.Param_Encrypt("ordid=" + qOrderID );
                string Url = string.Format("../ORD01/ORD01.aspx?Param={0}", encryptUrl);
                ScriptManager.RegisterClientScriptBlock(this, GetType(), "AlertMessage", "alert('作業完成!!');location.href='" + Url  + "';", true);
            }
        }
        else
        {
            //若未按儲存, 需先執行儲存動作
            if (Session["IsSave"] == null)
            {
                new ORD01_Facade().Update_TodayAvailableOrderItems(ds, logMsg.STORENO, lblOrderDate.Text.Replace("/", ""), logMsg.CREATE_USER);

                Session["IsSave"] = "1";
            }
            //傳輸
            if (Session["IsSave"] != null && StringUtil.CStr(Session["IsSave"]) == "1")
            {
                //取得該門市 當日的訂單
                string WorkDate = OracleDBUtil.WorkDay(logMsg.STORENO); //營業日
                new ORD01_Facade().CommitTodayOrderItems(ds, logMsg.STORENO, WorkDate.Replace("/", ""), logMsg.CREATE_USER);
                Session.Remove("IsSave");
                Session["UPOK"] = "IsSave";
                lblStatus.Text = "已傳輸";
            }
            //else
            //{
            //    ScriptManager.RegisterClientScriptBlock(this, GetType(), "AlertMessage", "alert('請先執行【儲存】!!');", true);
            //    return;
            //}
            if (IsAddMode)
            {
                Response.Redirect("ORD01.aspx", true);
            }
            else
            {
                string encryptUrl = Utils.Param_Encrypt("ordid=" + qOrderID);
                string Url = string.Format("ORD01.aspx?Param={0}", encryptUrl);

                Response.Redirect(Url, true);
            }
        }


    }

    //20110421 拿掉 因為取消按鈕只是回覆登入值 而不是恢復初始值 by wayne
    //protected void btnDrop_Click(object sender, EventArgs e)
    //{
    //    ORD01_OrderMD_DTO ds = new ORD01_OrderMD_DTO();
    //    ORD01_OrderMD_DTO.ORDER_M_TEMPDataTable MasterTable = ds.ORDER_M_TEMP;
    //    ORD01_OrderMD_DTO.ORDER_D_TEMPDataTable DetailTable = ds.ORDER_D_TEMP;

    //    foreach (DataColumn col in MasterTable.Columns)
    //    {
    //        col.AllowDBNull = true;
    //    }

    //    foreach (DataColumn col in DetailTable.Columns)
    //    {
    //        col.AllowDBNull = true;
    //    }


    //    //更新註解(主檔)
    //    ORD01_OrderMD_DTO.ORDER_M_TEMPRow MasterRow = MasterTable.NewORDER_M_TEMPRow();

    //    MasterRow.ORDER_TEMP_ID = ORDER_TEMP_ID;
    //    MasterRow.REMARK = txtMemo.Text;
    //    MasterRow.MODI_DTM = DateTime.Now;
    //    MasterRow.MODI_USER = logMsg.CREATE_USER;
    //    MasterRow.ORDER_NO = new ORD01_Facade().GetParaValue("STORE_ORDER_NO");
    //    MasterRow.STATUS = "";
    //    MasterRow.ISOK = "0";
    //    MasterTable.AddORDER_M_TEMPRow(MasterRow);

    //    DataTable dtMaster = Session["gvMaster"] as DataTable;

    //    int end = dtMaster.Rows.Count;

    //    for (int i = 0; i < end; i++)
    //    {

    //        ORD01_OrderMD_DTO.ORDER_D_TEMPRow DetailRow = DetailTable.NewORDER_D_TEMPRow();
    //        DataRow[] drs = DetailTable.Select("ORDER_D_TEMP_ID='" + StringUtil.CStr(dtMaster.Rows[i]["ORDER_D_TEMP_ID"]) + "'");
    //        if (drs.Length == 0)
    //        {
    //            DetailRow.ORDER_D_TEMP_ID = StringUtil.CStr(dtMaster.Rows[i]["ORDER_D_TEMP_ID"]);
    //            DetailRow.ORDQTY =  Convert.ToDecimal(StringUtil.CStr(dtMaster.Rows[i]["ADVISEQTY"]));
    //            DetailRow.MODI_USER = logMsg.CREATE_USER;
    //            DetailRow.MODI_DTM = DateTime.Now;

    //            DetailTable.AddORDER_D_TEMPRow(DetailRow);
    //        }
    //        // 搭贈
    //        string prdno = StringUtil.CStr(dtMaster.Rows[i]["PRODNO"]);
    //        ORD01_Facade _facade = new ORD01_Facade();

    //        DataTable dt = new DataTable();
    //        //dt = _facade.GetGiftProducts(prdno, this.lblOrderDate.Text, StringUtil.CStr(dtMaster.Rows[i]["ORDQTY"]));
    //        dt = _facade.GetGiftORDER_D_TEMP_ID(ORDER_TEMP_ID, prdno, logMsg.STORENO, lblOrderDate.Text.Replace("/", ""), StringUtil.CStr(dtMaster.Rows[i]["ORDER_D_TEMP_ID"]));
    //        if (dt.Rows.Count > 0)
    //        {
    //            for (int j = 0; j < dt.Rows.Count; j++)
    //            {
    //                DataRow[] drs1 = DetailTable.Select("ORDER_D_TEMP_ID='" + StringUtil.CStr(dt.Rows[j]["ORDER_D_TEMP_ID"]) + "'");
    //                if (drs1.Length == 0)
    //                {
    //                    DetailRow = DetailTable.NewORDER_D_TEMPRow();
    //                    DetailRow.ORDER_D_TEMP_ID = StringUtil.CStr(dt.Rows[j]["ORDER_D_TEMP_ID"]);
    //                    //DetailRow.PRE_ORDER_D_ID = StringUtil.CStr(dt.Rows[j]["SID"]);
    //                    DetailRow.ORDQTY =  Convert.ToDecimal(StringUtil.CStr(dtMaster.Rows[i]["ADVISEQTY"]));
    //                    DetailRow.MODI_USER = logMsg.CREATE_USER;
    //                    DetailRow.MODI_DTM = DateTime.Now;

    //                    DetailTable.AddORDER_D_TEMPRow(DetailRow);
    //                }
    //            }

    //        }
    //    }

    //    //變更DS
    //    ds.AcceptChanges();

    //    new ORD01_Facade().Update_TodayAvailableOrderItems(ds, logMsg.STORENO, lblOrderDate.Text.Replace("/", ""), logMsg.CREATE_USER);

    //    Response.Redirect("ORD01.aspx", true);
    //}

    #endregion

    #region gvMaster 觸發事件

    protected void drMasterDV_PageIndexChanged(object sender, EventArgs e)
    {
        drMasterDV.DetailRows.CollapseAllRows();

        ((ASPxGridView)sender).DataSource = Session["gvMaster"] as DataTable;
        ((ASPxGridView)sender).DataBind();
    }

    //protected void drMasterDV_RowValidating(object sender, DevExpress.Web.Data.ASPxDataValidationEventArgs e)
    //{
    //    if (Convert.ToInt32(e.NewValues["CHECK_IN_QTY"]) + Convert.ToInt32(e.NewValues["ORDQTY"])
    //        > Math.Ceiling(Convert.ToInt32(e.NewValues["ADVISEQTY"]) * 1.5))
    //        e.RowError = "當日訂購量不允許大於[建議訂購量1.5倍(無條件進位)-當日總訂購量]，請重新輸入";
    //}

    protected void drMasterDV_HtmlRowCreated(object sender, ASPxGridViewTableRowEventArgs e)
    {
        if (e.RowType == GridViewRowType.Data)
        {
            //搭贈按鈕
            ORD01_Facade _facade = new ORD01_Facade();
            DataTable dt = _facade.GetGiftProducts(StringUtil.CStr(e.GetValue("PRODNO")), this.lblOrderDate.Text, "");
            if (dt.Rows.Count == 0)
            {
                ASPxButton btnOnetoone = e.Row.FindChildControl<ASPxButton>("btnOnetoone");
                btnOnetoone.Enabled = false;
            }
            if (qOrderType != "1" && !IsAddMode)
            {
                ASPxTextBox txtOrdqty = e.Row.FindChildControl<ASPxTextBox>("txtOrdqty");
                txtOrdqty.Enabled = false;
            }
            if (Session["gvMaster"] != null)
            {
                DataTable dt1 = Session["gvMaster"] as DataTable;

                DataRow[] drs = dt1.Select("ITEMNO='" + StringUtil.CStr(e.GetValue("ITEMNO")) + "'");
                if (drs.Length > 0)
                {
                    e.Row.FindChildControl<ASPxTextBox>("txtOrdqty").Text = StringUtil.CStr(drs[0]["ORDQTY"]);
                }
                else
                {
                    e.Row.FindChildControl<ASPxTextBox>("txtOrdqty").Text = "0";

                }
            }
        }
        else if (e.RowType == GridViewRowType.Detail)
        {
            ORD01_Facade _facade = new ORD01_Facade();
            string detail_BASE_QTY = string.Empty;
            // 繫結明細資料表
            ASPxGridView detailGrid = e.Row.FindChildControl<ASPxGridView>("gvDetailDV");

            if (Session["gvMaster"] != null)
            {
                DataTable dt1 = Session["gvMaster"] as DataTable;

                DataRow[] drs = dt1.Select("ITEMNO='" + StringUtil.CStr(e.GetValue("ITEMNO")) + "'");
                if (drs.Length > 0)
                {
                    detail_BASE_QTY = StringUtil.CStr(drs[0]["ORDQTY"]);
                }
                else
                {
                    detail_BASE_QTY = "0";

                }
            }
            ((ASPxTextBox)drMasterDV.FindRowCellTemplateControl(e.VisibleIndex, (GridViewDataColumn)drMasterDV.Columns["ORDQTY"], "txtOrdqty")).Text = detail_BASE_QTY;
            DataTable dt = _facade.GetGiftProducts(StringUtil.CStr(e.GetValue("PRODNO")), this.lblOrderDate.Text, detail_BASE_QTY);

            detailGrid.DataSource = dt;
            detailGrid.DataBind();
        }

        CheckIsOkState(ref e);
    }

    protected void drMasterDV_RowCommand(object sender, ASPxGridViewRowCommandEventArgs e)
    {
        if (drMasterDV.DetailRows.IsVisible(e.VisibleIndex))
        {
            drMasterDV.DetailRows.CollapseAllRows();
        }
        else
        {
            drMasterDV.DetailRows.CollapseAllRows();
            drMasterDV.DetailRows.ExpandRow(e.VisibleIndex);
        }
    }

    protected void drMasterDV_HtmlCommandCellPrepared(object sender, ASPxGridViewTableCommandCellEventArgs e)
    {
        //設定自定名令BUTTON
        if (e.VisibleIndex >= 3)
            e.Cell.Enabled = false;
    }

    #endregion

    protected void gvDetailDV_CustomCallback(object source, DevExpress.Web.ASPxGridView.ASPxGridViewCustomCallbackEventArgs e)
    {
        string[] pa = e.Parameters.Split(new char[] { ';' });
        string[] tRow = pa[2].Split(new char[] { '_' });

        ASPxGridView detailGrid = drMasterDV.FindDetailRowTemplateControl(int.Parse(pa[2].Replace("cell", "")), "gvDetailDV") as ASPxGridView;

        if (detailGrid != null)
        {
            ORD01_Facade _facade = new ORD01_Facade();

            DataTable dt = new DataTable();
            dt = _facade.GetGiftProducts(pa[0], this.lblOrderDate.Text, pa[1]);
            detailGrid.DataSource = dt;
            detailGrid.DataBind();
        }


    }

    //***20110417拿掉檢查StroeAtr的量
    //ajax 呼當前網頁的方式
    //[System.Web.Services.WebMethod()]
    //[System.Web.Script.Services.ScriptMethod()]
    //static public string getStorAtr(string strData)
    //{
    //    string reInfo = "";
    //    if (!string.IsNullOrEmpty(strData))
    //    {
    //        string[] pa = strData.Split(new char[] { ';' });
    //        reInfo = new ORD01_Facade().Check_StoreAtr(pa[0], pa[1], pa[2], pa[3]); //商品料號,門市編號,輸入數量 # 搭贈商品ATR量檢查                      

    //    }

    //    return reInfo;
    //}
    //***
}
