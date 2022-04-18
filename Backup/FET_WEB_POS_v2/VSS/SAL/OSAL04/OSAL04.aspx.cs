using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Text;
using DevExpress.Web.ASPxGridView;
using DevExpress.Web.ASPxEditors;
using FET.POS.Model.Facade.FacadeImpl;
using System.Web.UI.HtmlControls;
using DevExpress.Web.ASPxPopupControl;
using Advtek.Utility;
using System.Data.OracleClient;
using FET.POS.Model.Common;
using System.Collections.Specialized;
public partial class VSS_SAL_OSAL04 : BasePage
{
    [Serializable]
    class MTempTable
    {
        public int STORE_REC_TOTAL_AMOUNT = 0;//門市應收總金額   
        public int STORE_PAY_TOTAL_AMOUNT = 0;//門市應付總金額
        public int STORE_CHANGE_AMOUNT = 0;   //找零
        public DataTable Head,
                         Detail;
        #region 存取畫面上之資料
        public void SaveSale_Head_Temp(VSS_SAL_OSAL04 p)
        {
            foreach (DataRow dr in Head.Rows)
            {
                dr["TRADE_DATE"] = p.lbTran_Date.Text;//交易日期
                dr["SALE_NO"] = p.lbSALE_NO.Text;//交易序號
                dr["REMARK"] = p.lblREMARK_VALUE.Text;
            }
            Head.AcceptChanges();
        }
        public void SaleHeadDataBind(VSS_SAL_OSAL04 p,string UUID)
        {
            DataTable dt = p.Facade.getSale_Head(UUID);
            if (dt != null && dt.Rows.Count > 0)
            {
                Head = dt;
                DataRow dr = dt.Rows[0];
                p.lblCancelNo.Text = Advtek.Utility.SerialNo.GenNo("SC");
                p.lblCancelDate.Text = DateTime.Now.ToString("yyyy/MM/dd");
                p.lbTran_Date.Text = Convert.ToDateTime(dr["TRADE_DATE"]).ToString("yyyy/MM/dd");
                p.lbSALE_NO.Text = StringUtil.CStr(dr["SALE_NO"]);
                p.lblREMARK_VALUE.Text = StringUtil.CStr(dr["REMARK"]);
                p.lbTOTAL_AMOUNT.Text = StringUtil.CStr(dr["SALE_TOTAL_AMOUNT"]); //已收金額
                p.lbPayAmount.Text = p.lbTOTAL_AMOUNT.Text; //應退金額應等於已收金額

                Employee_Facade empFacade = new Employee_Facade();
                p.lbMODI_USER.Text = p.logMsg.MODI_USER + " " + empFacade.GetEmpName(p.logMsg.MODI_USER);
                p.lbMODI_DTM.Text = p.logMsg.MODI_DTM.ToString("yyyy/MM/dd HH:mm:ss");
            }
        }
        #endregion
    }
    string _SRC_TYPE
    {
        get
        {
            string r = StringUtil.CStr(Request.QueryString["SRC_TYPE"]);
            return r;
        }
    }
    string _PKEY
    {
        get
        {
            string key = Request.QueryString["PKEY"] as string; //統一要給 POSUUID_MASTER;
            return key;
        }
    }
    string _STOCK
    {
        get
        {
            return Common_PageHelper.GetGoodLOCUUID();
        }
    }

    MTempTable TempTables
    {
        get
        {
            MTempTable r = Session["TempTable"] as MTempTable;
            if (r == null)
            {
                r = new MTempTable();
                Session["TempTable"] = r;
            }
            return r;
        }
    }
    OSAL04_Facade Facade
    {
        get
        {
            return new OSAL04_Facade();
        }

    }

    #region 資料載入處理
    /// <summary>
    /// 從查詢頁面載入舊交易資料
    /// </summary>
    void LoadData1()
    {
        string UUID = "";
        #region 讀取變數

        if (!string.IsNullOrEmpty(Request.QueryString["Param"]))
        {
            NameValueCollection qscoll = Utils.Param_UrlDecode(Request.QueryString["Param"]);
            foreach (string key in qscoll.AllKeys)
            {
                if (key == "UUID")
                {
                    UUID = string.Join(",", qscoll.GetValues(key));
                    break;
                }
            }
        }
        #endregion
        LoadInvalData(UUID); //載入需作廢資料

        gvMaster.DataSource = TempTables.Detail;
        gvMaster.DataBind();
        Session["TempTable"] = TempTables;
        //SALE_IMEI資料動態才給
        TempTables.SaleHeadDataBind(this, UUID); //顯示表頭資訊
        btnConfirmCancel.Enabled = true;
    }

    /// <summary>
    /// 載入需作廢資料
    /// <param name="POSUUID_MASTER">POSUUID_MASTER</param>
    /// </summary>
    void LoadInvalData(string UUID)
    {
        TempTables.Detail = Facade.getSale_Detail(UUID);  //舊POS交易的明細
    }
    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack && !IsCallback)
        {

            LoadData1();
            
        }
    }

    protected void btnConfirmCancel_Click(object sender, EventArgs e)
    {
        string UUID = "";
    

        #region 讀取變數

        if (!string.IsNullOrEmpty(Request.QueryString["Param"]))
        {
            NameValueCollection qscoll = Utils.Param_UrlDecode(Request.QueryString["Param"]);
            foreach (string key in qscoll.AllKeys)
            {
                if (key == "UUID")
                {
                    UUID = string.Join(",", qscoll.GetValues(key));
                    break;
                }

              
            }
        }
        #endregion
        bool bCheckOut = true;
        OracleConnection objConn = null;
        OracleTransaction objTX = null;
        try
        {
            btnConfirmCancel.Enabled = false;
            objConn = Advtek.Utility.OracleDBUtil.GetConnection();
            objTX = objConn.BeginTransaction();
            INVENTORY_Facade Inventory = new INVENTORY_Facade();
            //作廢退回庫存
            if (TempTables.Detail.Rows.Count > 0)
            {
                foreach (DataRow dr in TempTables.Detail.Rows)
                {
               
                    string Stock = FET.POS.Model.Common.Common_PageHelper.GetGoodLOCUUID();
                    string PRODNO = StringUtil.CStr(dr["PRODNO"]);
                    if (!string.IsNullOrEmpty(PRODNO) && OSAL04_Facade.CheckISSTOCK(PRODNO))
                    {
                        try
                        {
                          
                            string QTY = dr["QUANTITY"] == null ? "0" : dr["QUANTITY"].ToString();
                            //判斷是否扣庫存
                            bool result = OSAL04_Facade.INVENTORY_RETURN(PRODNO, this.logMsg.STORENO, Stock, QTY, this.logMsg.MODI_USER, objTX);
                            //Inventory.PK_INVENTORY_RETURN(objTX, "1", StringUtil.CStr(dr["PRODNO"]),
                            //   logMsg.STORENO, Stock, StringUtil.CStr(TempTables.Head.Rows[0]["SALE_NO"]),
                            //   Convert.ToInt32(StringUtil.CStr(dr["QUANTITY"])), logMsg.MODI_USER, StringUtil.CStr(dr["ID"]), ref Code, ref Message);
                            if (!result)
                            {
                                bCheckOut = false;
                                ScriptManager.RegisterClientScriptBlock(this, typeof(string), "ERROR_MESSAGE", "alert('作廢舊POS交易貨品退庫存失敗!!');", true);
                                objTX.Rollback();
                            }
                        }
                        catch (Exception ex)
                        {
                            bCheckOut = false;
                            ScriptManager.RegisterClientScriptBlock(this, typeof(string), "ERROR_MESSAGE", "alert('作廢舊POS交易貨品退庫存失敗!!" + ex.Message.Replace("'", "-").Replace("\"", " ").Replace("\r", "").Replace("\n", "") + "');", true);
                            objTX.Rollback();
                        }
                    }
                }
            }

            //IMEI_Log
            if (bCheckOut)
            {
                //string strMessage = new OSAL04_Facade().IMEIRETURN_Log(objTX, this._PKEY);
                //string[] strMsg = strMessage.Split('|');
                //if (strMsg[0] != "000") //表示失敗
                //{
                //    bCheckOut = false;
                //    ScriptManager.RegisterClientScriptBlock(this, typeof(string), "ERROR_MESSAGE", @"alert('作廢舊POS交易IMEI_LOG失敗, " + strMsg[1].Replace("'", "-").Replace("\"", " ") + "');", true);
                //    objTX.Rollback();
                //}
            }

            //作廢原交易
            if (bCheckOut)
            {
               
                int AffectRow = 0;
                AffectRow = new OSAL04_Facade().invalidOldTransactionPeriod(objTX, UUID, this.logMsg.MODI_USER, this.logMsg.MACHINE_ID);
                if (AffectRow < 1)
                {
                    bCheckOut = false;
                    ScriptManager.RegisterClientScriptBlock(this, typeof(string), "ERROR_MESSAGE", "alert('作廢舊POS交易失敗!!');", true);
                    objTX.Rollback();
                }
            }

            if (bCheckOut)  //所有動作都有正常執行才可commit
            {
                objTX.Commit();
                ScriptManager.RegisterClientScriptBlock(this, typeof(string), "OrderCancel", "alert('作廢舊POS交易完成!');window.location.href='OSAL041.aspx';", true);
            }
        }
        catch (Exception ex)
        {
            bCheckOut = false;
            Logger.Log.Info("作廢舊POS交易作業失敗，單號:" + StringUtil.CStr(TempTables.Head.Rows[0]["SALE_NO"]) + ",POSUUID_MASTER=" + this._PKEY + ",原因:" + ex.Message);
            ScriptManager.RegisterClientScriptBlock(this, typeof(string), "ERROR_MESSAGE", "alert('" + ex.Message.Replace("'", "-").Replace("\"", " ") + "');", true);
            objTX.Rollback();
            return;
        }
        finally
        {
            if (bCheckOut)
                btnConfirmCancel.Enabled = false;
            else
                btnConfirmCancel.Enabled = true;
            if (objConn.State == ConnectionState.Open)
                objConn.Close();
            objConn.Dispose();
            OracleConnection.ClearAllPools();
        }
    }

    protected void gvMaster_HtmlRowCreated(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewTableRowEventArgs e)
    {
        if (e.RowType == GridViewRowType.Data)
        {
            ASPxTextBox txtProdName = e.Row.FindChildControl<ASPxTextBox>("txtPRODNAME");
            ASPxTextBox txtIMEI = e.Row.FindChildControl<ASPxTextBox>("txtIMEI");

            string PRODNO = StringUtil.CStr(gvMaster.GetRowValues(e.VisibleIndex, "PRODNO"));

            //取得IMEI_Flag
            DataTable dtFlag = new Product_Facade().Query_ProductInfo(PRODNO);
            string IMEI_Flag = "";
            if (dtFlag.Rows.Count > 0)
            {
                IMEI_Flag = StringUtil.CStr(dtFlag.Rows[0]["IMEI_FLAG"]);
                txtProdName.Text = StringUtil.CStr(dtFlag.Rows[0]["PRODNAME"]);
            }
        }
    }

    protected void gvMaster_HtmlRowPrepared(object sender, ASPxGridViewTableRowEventArgs e)
    {
        e.Row.Attributes["canSelect"] = "true";
        if (e.RowType == GridViewRowType.Data)
        {
            PopupControl txtPRODNO = gvMaster.FindRowCellTemplateControl(e.VisibleIndex, (GridViewDataColumn)gvMaster.Columns["PRODNO"], "txtPRODNO") as PopupControl; // e.Row.FindChildControl<PopupControl>("txtPRODNO");
            ASPxTextBox txtQUANTITY = gvMaster.FindRowCellTemplateControl(e.VisibleIndex, (GridViewDataColumn)gvMaster.Columns["QUANTITY"], "txtQUANTITY") as ASPxTextBox;  //e.Row.FindChildControl<ASPxTextBox>("txtQUANTITY");
            ASPxTextBox txtUNIT_PRICE = gvMaster.FindRowCellTemplateControl(e.VisibleIndex, (GridViewDataColumn)gvMaster.Columns["UNIT_PRICE"], "txtUNIT_PRICE") as ASPxTextBox;  //e.Row.FindChildControl<ASPxTextBox>("txtUNIT_PRICE");
            ASPxTextBox txtIMEI = gvMaster.FindRowCellTemplateControl(e.VisibleIndex, (GridViewDataColumn)gvMaster.Columns["IMEI_QTY"], "txtIMEI") as ASPxTextBox;  //e.Row.FindChildControl<ASPxTextBox>("txtUNIT_PRICE");

            txtPRODNO.Enabled = false;
            txtQUANTITY.Enabled = false;
            txtUNIT_PRICE.ReadOnly = true;
            txtIMEI.Enabled = true;
        }
    }

    protected void gvMaster_CommandButtonInitialize(object sender, ASPxGridViewCommandButtonEventArgs e)
    {
        e.Enabled = false;
    }

    protected void gvCheckOut_HtmlRowPrepared(object sender, ASPxGridViewTableRowEventArgs e)
    {
        //string ITEM_STATUS = "";
        //if (e.VisibleIndex != -1)
        //    e.Row.Enabled = false;
    }

    //ajax 呼當前網頁的方式
    [System.Web.Services.WebMethod()]
    [System.Web.Script.Services.ScriptMethod()]
    static public string getPRODINFO(string PRODNO)
    {
        DataTable dt = new Product_Facade().Query_ProductInfo(PRODNO);
        string r = "";
        if (dt.Rows.Count > 0)
        {
            r = StringUtil.CStr(dt.Rows[0]["PRODNAME"]) + ";";
            r += StringUtil.CStr(dt.Rows[0]["IMEI_FLAG"]) + ";";
            if (dt.Rows[0]["PRICE"] != null && dt.Rows[0]["PRICE"] != DBNull.Value && StringUtil.CStr(dt.Rows[0]["PRICE"]) != "")
                r += StringUtil.CStr(dt.Rows[0]["PRICE"]);
            else
                r += "0";
        }
        return r;
    }

    protected void gvMaster_PageIndexChanged(object sender, EventArgs e)
    {
        DataTable dt = (DataTable)gvMaster.DataSource;
        if (dt == null || dt.Rows.Count == 0)
        {
            foreach (DataRow dr in TempTables.Detail.Rows)
            {
                if (dr["PRODNAME"] == null || string.IsNullOrEmpty(StringUtil.CStr(dr["PRODNAME"])))
                {
                    if (dr["PRODNO"] != null && (!string.IsNullOrEmpty(StringUtil.CStr(dr["PRODNO"]))))
                    {
                        DataTable dtProduct = new Product_Facade().Query_ProductInfo(StringUtil.CStr(dr["PRODNO"]));
                        if (dtProduct != null && dtProduct.Rows.Count > 0)
                            dr["PRODNAME"] = StringUtil.CStr(dtProduct.Rows[0]["PRODNAME"]);
                    }
                }
            }
            gvMaster.DataSource = TempTables.Detail;
            gvMaster.DataBind();
        }
    }
}
