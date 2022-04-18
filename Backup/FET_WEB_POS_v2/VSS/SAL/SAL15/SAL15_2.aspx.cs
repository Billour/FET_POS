using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Advtek.Utility;
using System.Collections;
using DevExpress.Web;
using DevExpress.Web.ASPxGridView;
using DevExpress.Web.ASPxPager;
using FET.POS.Model.Facade.FacadeImpl;
using FET.POS.Model.Common;
using DevExpress.Web.ASPxEditors;
using System.Text;
using FET.POS.Model.DTO;
using FET.POS.Model.Facade;
using System.Web.UI.HtmlControls;
using DevExpress.Web.ASPxClasses;
using System.Data.OracleClient;
using System.Collections.Specialized;
public partial class VSS_SAL_SAL015_2 : BasePage
{
    private string QryUSER;
    private int SumPointCount;//總累點數要四捨五入
    protected void Page_Load(object sender, EventArgs e)
    {
        QryUSER = logMsg.OPERATOR;
        if (!IsPostBack && !Page.IsCallback)
        {
            string strHGCardNo = "";
            string strHGCardCount = "";

            //**2011/04/27 Tina：前頁傳遞參數時，會先以加密處理，在此要解密。
            if (!string.IsNullOrEmpty(Request.QueryString["Param"]))
            {
                NameValueCollection qscoll = Utils.Param_UrlDecode(Request.QueryString["Param"]);
                foreach (string key in qscoll.AllKeys)
                {
                    if (key == "HGCardNo")
                    {
                        strHGCardNo = string.Join(",", qscoll.GetValues(key));
                    }
                    else if (key == "HGCardCount")
                    {
                        strHGCardCount = string.Join(",", qscoll.GetValues(key));
                    }

                }
            }

            Session["gvMaster"] = null;
            BindMasterData();
            this.gvMaster.FocusedRowIndex = -1;
            if (!string.IsNullOrEmpty(strHGCardNo))
            {
                lblHGNo.Text = strHGCardNo;
            }
            if (!string.IsNullOrEmpty(strHGCardCount))
            {
                txtHGCardCount.Text = strHGCardCount;
            }
            lblStatus.Text = "00-未存檔";
            string WorkDate = OracleDBUtil.WorkDay(this.logMsg.STORENO); //營業日
            lblTradeDate.Text = WorkDate;
            lblModiDate.Text = DateTime.Now.ToString("yyyy/MM/dd HH:ss");
            lblModiUser.Text = logMsg.MODI_USER + " " + new Employee_Facade().GetEmpName(logMsg.MODI_USER);
            StringBuilder script = new StringBuilder();
            script.AppendFormat("var CARD_NO = '{0}';\n", Request["HG_CARD_NO"]);
            script.AppendFormat("var AMOUNT = '{0}';\n", Request["TOTAL_AMOUNT"]);
            script.AppendFormat("var STORE_NO = '{0}';\n", logMsg.STORENO);
            script.Append("var LEFT_POINT = '0';\n");
            Page.ClientScript.RegisterClientScriptBlock(GetType(), GetType().ToString() + "_StartupScript", script.ToString(), true);

        }

    }



    protected void BindMasterData()
    {

        string ActivityId = Session["ACTIVITY_ID"] as string;
        DataTable dt = new SAL15_Facade().Query_HgConvertibleGiftM(ActivityId);
        Session["gvMaster"] = dt;
        gvMaster.DataSource = dt;
        gvMaster.DataBind();
        //gvMaster.Selection.UnselectAll();
        bool MsisdnEnabled = false;
        DataRow[] drMemberCheckFlag = dt.Select("MEMBER_CHECK_FLAG='1'");
        MsisdnEnabled = drMemberCheckFlag.Length > 0 ? true : false;
        txtmsisdn.ClientEnabled = MsisdnEnabled;
    }


    #region Button 觸發事件

    protected void btnSave_Click(object sender, EventArgs e)
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

        if (dtSYS.Rows.Count == 0)
        {
            ScriptManager.RegisterClientScriptBlock(this, typeof(string), "商品", "alert('需輸入一筆資料');", true);
            return;
        }
        string ErrorMessage = CheckData();
        if (!string.IsNullOrEmpty(ErrorMessage))
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "AlertError", "alert('" + ErrorMessage + "');", true);
        }
        else
        {
            SaveCurrentPage();
        }
    }
    protected void btnDeleteRow_Click(object sender, EventArgs e)
    {
        //List<object> gvPKValues = gvMaster.GetSelectedFieldValues(gvMaster.KeyFieldName);

        //string pkFName = gvMaster.KeyFieldName;

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
            foreach (object key in keyValues)
            {
                DataRow drSYS = dtSYS.Select("ACTIVITY_ID='" + key.ToString() + "'")[0];
                dtSYS.Rows.Remove(drSYS);
            }
            gvMaster.Selection.UnselectAll();
            Session["gvMaster"] = dtSYS;
            gvMaster.DataSource = Session["gvMaster"];
            gvMaster.DataBind();

        }
    }
    #endregion

    #region gvMaster 觸發事件



    protected void gvMaster_PageIndexChanged(object sender, EventArgs e)
    {
        BindMasterData();

    }


    protected void gvMaster_HtmlRowPrepared(object sender, ASPxGridViewTableRowEventArgs e)
    {
        e.Row.Attributes["canSelect"] = "true";

        if (e.RowType == GridViewRowType.Data)
        {

            //GridViewDataColumn colTYPE = new GridViewDataColumn();
            //colTYPE = (GridViewDataColumn)((ASPxGridView)sender).Columns["TYPE"];
            //ASPxLabel lblTYPE = gvMaster.FindRowCellTemplateControl(e.VisibleIndex, colTYPE, "lblTYPE") as ASPxLabel;
            //if (lblTYPE != null)
            //{
            //    lblTYPE.Text = StringUtil.CStr(e.GetValue("TYPE")) == "1" ? "點數" : "商品";
            //}
            GridViewDataColumn colMEMBER_CHECK_FLAG = new GridViewDataColumn();
            colMEMBER_CHECK_FLAG = (GridViewDataColumn)((ASPxGridView)sender).Columns["MEMBER_CHECK_FLAG"];
            ASPxCheckBox cbMEMBER_CHECK_FLAGRow = gvMaster.FindRowCellTemplateControl(e.VisibleIndex, colMEMBER_CHECK_FLAG, "cbMEMBER_CHECK_FLAG") as ASPxCheckBox;
            if (cbMEMBER_CHECK_FLAGRow != null)
            {
                cbMEMBER_CHECK_FLAGRow.Checked = StringUtil.CStr(e.GetValue("MEMBER_CHECK_FLAG")) == "0" ? false : true;
            }
        }

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

        string ACTIVITY_ID = StringUtil.CStr(e.Keys["ACTIVITY_ID"]);
        string sNewQty = e.NewValues["QTY"].ToString();
        for (int i = 0; i < dtSYS.Rows.Count; i++)
        {
            DataRow dr = dtSYS.Rows[i];
            if (dr["ACTIVITY_ID"].ToString().CompareTo(ACTIVITY_ID) == 0)
            {
                dr["QTY"] = sNewQty.Trim();
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
    }


    #endregion



    #region ajax 呼當前網頁
    //ajax 呼當前網頁的方式
    [System.Web.Services.WebMethod()]
    [System.Web.Script.Services.ScriptMethod()]
    static public string getStoreInfo(string STORE_NO)
    {
        string strInfo = "";
        if (!string.IsNullOrEmpty(STORE_NO))
        {
            DataTable dtStore = new Store_Facade().Query_StoreZone_ByKey(STORE_NO);
            if (dtStore.Rows.Count > 0)
            {
                strInfo = StringUtil.CStr(dtStore.Rows[0]["STORENAME"]) + ";" + StringUtil.CStr(dtStore.Rows[0]["ZONE_NAME"]);
            }
        }

        return strInfo;
    }

    //ajax 呼當前網頁的方式
    [System.Web.Services.WebMethod()]
    [System.Web.Script.Services.ScriptMethod()]
    static public string getProductInfo(string PRODNO)
    {
        string strInfo = "";
        if (!string.IsNullOrEmpty(PRODNO))
        {
            DataTable dt = new Product_Facade().Query_DiscountProduct(PRODNO);
            if (dt.Rows.Count > 0)
            {
                strInfo = StringUtil.CStr(dt.Rows[0]["PRODNAME"]);
            }
        }

        return strInfo;
    }

    //ajax 呼當前網頁的方式
    [System.Web.Services.WebMethod()]
    [System.Web.Script.Services.ScriptMethod()]
    static public string getPRODINFOExtraSale(string PRODNO)
    {
        DataTable dt = new Product_Facade().Query_ProductInfo(PRODNO);
        string r = "";
        if (dt.Rows.Count > 0)
        {
            r = StringUtil.CStr(dt.Rows[0]["PRODNAME"]);
        }
        if (r != "")
        {
            dt = new Product_Facade().getPRODExtraSale(PRODNO);
            if (dt.Rows.Count > 0)
            {
                r = StringUtil.CStr(dt.Rows[0]["PRODNAME"]);
            }
            else
                r = "fail";
        }
        return r;
    }


    #endregion
    #region 資料處理


    /// <summary>
    /// 確認資料是否正確
    /// </summary>
    protected string CheckData()
    {
        string ErrorMsg = string.Empty;
        int CurPageFirstIndex = this.gvMaster.PageIndex * gvMaster.SettingsPager.PageSize;        //當頁第一筆Index
        int PatgeLastIndex = (this.gvMaster.PageIndex + 1) * gvMaster.SettingsPager.PageSize - 1;
        int LastIndex = this.gvMaster.VisibleRowCount - 1;  //最後一筆資料Index
        int CurPatgeLastIndex = PatgeLastIndex <= LastIndex ? PatgeLastIndex : LastIndex;         //當頁最後一筆Index
        double TotalPointCount = 0;//總累點
        for (int i = CurPageFirstIndex; i <= CurPatgeLastIndex; i++)
        {
            ASPxTextBox txtQty = gvMaster.FindRowCellTemplateControl(i, (GridViewDataColumn)gvMaster.Columns["QTY"], "txtQuantity") as ASPxTextBox;
            if (!txtQty.IsValid)  //當頁只要某一筆數量驗證不通過，就不儲存當頁資料
            {
                break;
            }
            ASPxCheckBox cbMEMBER_CHECK_FLAG = gvMaster.FindRowCellTemplateControl(i, (GridViewDataColumn)gvMaster.Columns["MEMBER_CHECK_FLAG"], "cbMEMBER_CHECK_FLAG") as ASPxCheckBox;
            ASPxLabel lblDiscountName = gvMaster.FindRowCellTemplateControl(i, (GridViewDataColumn)gvMaster.Columns["ACTIVITY_NAME"], "lblDiscountName") as ASPxLabel;
            ASPxTextBox txtTYPE = gvMaster.FindRowCellTemplateControl(i, (GridViewDataColumn)gvMaster.Columns["TYPE"], "txtTYPE") as ASPxTextBox;
            ASPxLabel lblDividablePoint = gvMaster.FindRowCellTemplateControl(i, (GridViewDataColumn)gvMaster.Columns["DIVIDABLE_POINT"], "lblDividablePoint") as ASPxLabel;
            if (txtTYPE.Text == "1")//類別為1則累點
            {
                TotalPointCount += Convert.ToDouble(lblDividablePoint.Text);
            }
            string Sid = string.Empty;
            string ACTITETY_ID = StringUtil.CStr(gvMaster.GetRowValues(i, gvMaster.KeyFieldName));
            if (cbMEMBER_CHECK_FLAG.Checked)
            {
                if (string.IsNullOrEmpty(txtmsisdn.Text))
                {
                    ErrorMsg += "有名單檢核，門號必填";
                    break;
                }

                Sid = new SAL15_Facade().Query_HgConvertMemberList(ACTITETY_ID, txtmsisdn.Text);
                if (string.IsNullOrEmpty(Sid))
                {
                    ErrorMsg += "活動名稱：" + lblDiscountName.Text + "  ，此門號無兌點名單";
                    break;
                }
                if (new SAL15_Facade().CheckMemberGiftExchRepeat(ACTITETY_ID, txtmsisdn.Text))
                {
                    ErrorMsg += "活動名稱：" + lblDiscountName.Text + "  ，此門號不得重複申請";
                    break;
                }
            }
            else
            {
                if (new SAL15_Facade().Check_HgConvertibleGiftD(ACTITETY_ID, logMsg.STORENO))
                {
                    ErrorMsg += "活動名稱：" + lblDiscountName.Text + "  ，此門市無兌換來店禮";
                    break;

                }
            }
            bool blExchangeOk = new SAL15_Facade().CheckExchOk(ACTITETY_ID);
            if (!blExchangeOk)
            {
                ErrorMsg += "活動名稱：" + lblDiscountName.Text + "  ，此活動已兌換完數量";
                break;
            }
        }
        SumPointCount = Convert.ToInt32(NumberUtil.Round(TotalPointCount, 1));
        return ErrorMsg;
    }

    /// <summary>
    /// 儲存當頁資料
    /// </summary>
    protected void SaveCurrentPage()
    {
        string WorkDate = StringUtil.CStr(lblTradeDate.Text).Trim(); //營業日
        //1. GridView可見
        //2. 有資料
        //3. 盤點日 == 營業日 才可修改門市盤點量
        int result = 0;
        int CurPageFirstIndex = this.gvMaster.PageIndex * gvMaster.SettingsPager.PageSize;        //當頁第一筆Index
        int PatgeLastIndex = (this.gvMaster.PageIndex + 1) * gvMaster.SettingsPager.PageSize - 1;
        int LastIndex = this.gvMaster.VisibleRowCount - 1;  //最後一筆資料Index
        int CurPatgeLastIndex = PatgeLastIndex <= LastIndex ? PatgeLastIndex : LastIndex;         //當頁最後一筆Index

        SAL015_HG_GIFT_EXCH_TRANS_DTO SAL015_DTO = new SAL015_HG_GIFT_EXCH_TRANS_DTO();
        SAL015_HG_GIFT_EXCH_TRANS_DTO.HG_GIFT_EXCH_TRANS_LOGDataTable dt = SAL015_DTO.HG_GIFT_EXCH_TRANS_LOG;
        SAL015_HG_GIFT_EXCH_TRANS_DTO.HG_GIFT_EXCH_ITEMSDataTable dtD = SAL015_DTO.HG_GIFT_EXCH_ITEMS;
        SAL15_Facade facade = new SAL15_Facade();

        SAL015_HG_GIFT_EXCH_TRANS_DTO.HG_GIFT_EXCH_TRANS_LOGRow dr = null;
        SAL015_HG_GIFT_EXCH_TRANS_DTO.HG_GIFT_EXCH_ITEMSRow drD = null;
        string SALE_NO = Store_SerialNo.GenNo("SALE", logMsg.STORENO, logMsg.MACHINE_ID);
        string DrUUID = GuidNo.getUUID();
        dr = dt.NewHG_GIFT_EXCH_TRANS_LOGRow();
        dr.CREATE_DTM = DateTime.Now;
        dr.CREATE_USER = logMsg.CREATE_USER;
        dr.HG_CARD_NO = lblHGNo.Text;
        dr.HG_GIFT_LOG_ID = DrUUID;
        dr.TRANS_NO = SALE_NO;
        dr.TRANS_DATE = Convert.ToDateTime(lblTradeDate.Text);
        dr.STATUS = "10";
        dr.REMARK = txtRemark.Text;
        dr.EXCHANGE_STORENO = logMsg.STORENO;
        dt.Rows.Add(dr);
        SAL015_DTO.AcceptChanges();
        for (int i = CurPageFirstIndex; i <= CurPatgeLastIndex; i++)
        {
            drD = dtD.NewHG_GIFT_EXCH_ITEMSRow();
            ASPxTextBox txtQty = gvMaster.FindRowCellTemplateControl(i, (GridViewDataColumn)gvMaster.Columns["QTY"], "txtQuantity") as ASPxTextBox;
            if (!txtQty.IsValid)  //當頁只要某一筆數量驗證不通過，就不儲存當頁資料
            {
                break;
            }
            ASPxCheckBox cbMEMBER_CHECK_FLAG = gvMaster.FindRowCellTemplateControl(i, (GridViewDataColumn)gvMaster.Columns["MEMBER_CHECK_FLAG"], "cbMEMBER_CHECK_FLAG") as ASPxCheckBox;
            string Sid = string.Empty;
            string ACTITETY_ID = StringUtil.CStr(gvMaster.GetRowValues(i, gvMaster.KeyFieldName));
            Sid = cbMEMBER_CHECK_FLAG.Checked ? new SAL15_Facade().Query_HgConvertMemberList(ACTITETY_ID, txtmsisdn.Text) : null;
            string strQty = txtQty.Text.Trim();
            DataRow drD2 = facade.Query_HgConvertibleGiftM(ACTITETY_ID).Rows[0];
            string DividablePoint = StringUtil.CStr(drD2["DIVIDABLE_POINT"]);
            string ProdNo = StringUtil.CStr(drD2["PRODNO"]);
            DividablePoint = !string.IsNullOrEmpty(DividablePoint) ? DividablePoint : "0";
            drD.ACTIVITY_ID = ACTITETY_ID;

            if (!string.IsNullOrEmpty(Sid))
            {
                drD.SID = Sid;
            }
            drD.HG_GIFT_LOG_ID = DrUUID;
            drD.HG_GIF_ITEMS_ID = GuidNo.getUUID();
            drD.CREATE_DTM = DateTime.Now;
            drD.CREATE_USER = logMsg.CREATE_USER;
            if (DividablePoint != "0")
            {
                drD.GIFT_POINT = Convert.ToDecimal(DividablePoint);
            }

            drD.QTY = Convert.ToDecimal(strQty);
            if (!string.IsNullOrEmpty(ProdNo))
            {
                drD.GIFT_PRODNO = StringUtil.CStr(drD2["PRODNO"]);
            }
            drD.SEQ_NO = StringUtil.CStr(i + 1);
            dtD.Rows.Add(drD);
            SAL015_DTO.AcceptChanges();

        }

        try
        {


            //INSERT　DATA
            result += facade.InsertData(SAL015_DTO);

            if (result > 0)
            {
                result = 0;
               
                OracleConnection objConn = null;
                OracleTransaction objTx = null;
                try
                {
                    objConn = OracleDBUtil.GetConnection();
                    objTx = objConn.BeginTransaction();
                    for (int i = CurPageFirstIndex; i <= CurPatgeLastIndex; i++)
                    {
                        string ACTITETY_ID = StringUtil.CStr(gvMaster.GetRowValues(i, gvMaster.KeyFieldName));

                        ASPxCheckBox cbMEMBER_CHECK_FLAG = gvMaster.FindRowCellTemplateControl(i, (GridViewDataColumn)gvMaster.Columns["MEMBER_CHECK_FLAG"], "cbMEMBER_CHECK_FLAG") as ASPxCheckBox;
                        //回押MEMBER LIST 的USE_COUNT =1
                        result += cbMEMBER_CHECK_FLAG.Checked ? new SAL15_Facade().UpdateMemberList(ACTITETY_ID, txtmsisdn.Text) : 0;
                        //回押ConvertibleGiftM 折抵次數加1
                        result += new SAL15_Facade().UpdateConvertibleGiftM(ACTITETY_ID);

                        ASPxTextBox txtQty = gvMaster.FindRowCellTemplateControl(i, (GridViewDataColumn)gvMaster.Columns["QTY"], "txtQuantity") as ASPxTextBox;
                        DataRow drD2 = facade.Query_HgConvertibleGiftM(ACTITETY_ID).Rows[0];
                        string ProdNo = StringUtil.CStr(drD2["PRODNO"]);
                        string Isstock = StringUtil.CStr(drD2["ISSTOCK"]);
                        string STOCK = Common_PageHelper.GetGoodLOCUUID();
                        string Code = "";
                        string Message = "";
                        //扣庫存
                        if (!string.IsNullOrEmpty(ProdNo) && Isstock == "1")
                        {
                            string Id = new SAL15_Facade().GetHgGiftItemsId(DrUUID, ProdNo);
                            new SAL15_Facade().PK_INVENTORY_SALE(objTx, "1", ProdNo,
                               logMsg.STORENO, STOCK, SALE_NO,
                               Convert.ToInt32(txtQty.Text), logMsg.MODI_USER, Id, ref Code, ref Message);
                            if (Code != "000")
                            {
                                throw new Exception(Message);

                            }

                        }
                    }
                    objTx.Commit();
                }
                catch (Exception ex)
                {
                    objTx.Rollback();
                    throw;
                }
                

                //累績點數
                if (SumPointCount > 0)
                {
                    StringBuilder script = new StringBuilder();
                    script.Append(@"var oECR = new ActiveXObject('ProjECR.ECRAPI');
                    			                             var result = oECR.Reward_Transaction(" + SumPointCount + "," + logMsg.STORENO + ", '2', " + lblHGNo.Text + ");");
                    script.Append(@"var arr = result.split(',');
                    		                            if (arr[0] == '0000') {
                    			                            returnValue = 'OK';
                    		                            } else {
                    			                            alert('無法累積點數 !');
                    			                            returnValue = '';
                    		                            }
                    		                            window.close();");

                    ScriptManager.RegisterClientScriptBlock(this, typeof(string), "InsertAllDBOK", StringUtil.CStr(script), true); //消除[存檔中訊息]

                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this, typeof(string), "InsertAllDBOK", "alert('存檔成功'); window.close();", true); //消除[存檔中訊息]
                }


            }

        }
        catch //(Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(this, typeof(string), "InsertAllDBError", "alert('存檔失敗!');", true); //消除[存檔中訊息]
            return;
        }




    }
    #endregion
}
