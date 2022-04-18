using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;
using DevExpress.Web.ASPxGridView;
using System.Web.UI.HtmlControls;
using DevExpress.Web.ASPxEditors;
using iTextSharp.text;
using iTextSharp.text.pdf;

using Advtek.Utility;
using FET.POS.Model.Common;
using FET.POS.Model.DTO;
using FET.POS.Model.Facade;
using FET.POS.Model.Facade.FacadeImpl;
using AdvTek.CustomControls;
using System.Web.Configuration;

public partial class VSS_INV_INV26 : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack && !Page.IsCallback)
        {
            BindMasterData();
        }
    }

    protected void BindMasterData()
    {
        gvMaster.DataSource = new INV25_Facade().Query_StoreTransferM_ByINV26(this.txtStno.Text,
            this.txtProdNo.Text, this.txtProductName.Text, StringUtil.CStr(this.ddlTStatus.SelectedItem.Value),
            this.txtTSTDate_S.Text, this.txtTSTDate_E.Text, this.txtFromStoreNo.Text, this.txtFromStoreName.Text, 
            this.logMsg.ROLE_TYPE, this.logMsg.STORENO, WebConfigurationManager.AppSettings["DefaultRoleHQ"]);
        gvMaster.DataBind();

        gvMaster.DetailRows.CollapseAllRows();
    }

    private void BindDetailData(string STNO)
    {
        DevExpress.Web.ASPxGridView.ASPxGridView detailGrid = gvMaster.FindChildControl<DevExpress.Web.ASPxGridView.ASPxGridView>("gvDetail");
        detailGrid.DataSource = new INV25_Facade().Query_StoreTransferD(STNO);
        detailGrid.DataBind();
        
    }

    #region Button 觸發事件

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        BindMasterData();
        this.gvMaster.PageIndex = 0;
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        DevExpress.Web.ASPxGridView.ASPxGridView gvDetail = gvMaster.FindChildControl<DevExpress.Web.ASPxGridView.ASPxGridView>("gvDetail");
        if (gvDetail != null)
        {
            INV25_StoreTransfer_DTO INV25_DTO = new INV25_StoreTransfer_DTO();
            INV25_StoreTransfer_DTO.STORETRANSFER_MDataTable dtM = INV25_DTO.STORETRANSFER_M;
            INV25_StoreTransfer_DTO.STORETRANSFER_MRow drM = dtM.NewSTORETRANSFER_MRow();

            INV25_StoreTransfer_DTO.STORETRANSFER_DDataTable dtD = INV25_DTO.STORETRANSFER_D;

            string strSTNO = StringUtil.CStr(gvDetail.GetRowValues(0, "STNO"));
            DataRow dr = INV25_PageHelper.Query_StoreTransferM_ByKey(strSTNO).Rows[0];

            drM.STNO = strSTNO; //調撥單號
            drM.TSTATUS = "30";//調撥單狀態：  00 未存檔,10 暫存(預留), 20 在途, 30 已撥入
            drM.TO_STORE_NO = StringUtil.CStr(dr["TO_STORE_NO"]);
            drM.MODI_USER = this.logMsg.OPERATOR;//異動人員
            drM.MODI_DTM = System.DateTime.Now;//異動時間
            drM.TSTDATE = System.DateTime.Now.ToString("yyyy/MM/dd");//OracleDBUtil.WorkDay(this.logMsg.STORENO); //System.DateTime.Today.ToString("yyyy/MM/dd"); //調入日期
            drM.TSTUSRNO = this.logMsg.OPERATOR;//調入端驗收人員
            drM.TUPDDATE = OracleDBUtil.WorkDay(this.logMsg.STORENO); //System.DateTime.Today.ToString("yyyy/MM/dd");//調入端異動時間
            drM.FROM_STORE_NO = StringUtil.CStr(dr["FROM_STORE_NO"]);

            dtM.Rows.Add(drM);
            INV25_DTO.AcceptChanges();
            for (int i = 0; i <= gvDetail.VisibleRowCount - 1; i++)
            {
                INV25_StoreTransfer_DTO.STORETRANSFER_DRow drD = dtD.NewSTORETRANSFER_DRow();

                drD.STNO = strSTNO;
                drD.STORETRANSFER_D_ID = StringUtil.CStr(gvDetail.GetRowValues(i, "STORETRANSFER_D_ID"));
                drD.SEQNO = StringUtil.CStr(gvDetail.GetRowValues(i, "SEQNO"));
                drD.PRODNO = StringUtil.CStr(gvDetail.GetRowValues(i, "PRODNO"));
                drD.TRANINQTY = Convert.ToInt64(StringUtil.CStr(gvDetail.GetRowValues(i, "TRANOUTQTY"))); //撥入數量
                drD.STDATE = OracleDBUtil.WorkDay(this.logMsg.STORENO); //System.DateTime.Today.ToString("yyyy/MM/dd");//調入驗收日期
                drD.MODI_USER = this.logMsg.OPERATOR;//異動人員
                drD.MODI_DTM = System.DateTime.Now;//異動時間

                dtD.Rows.Add(drD);
                INV25_DTO.AcceptChanges();
            }
            try
            {
                INV25_Facade facade = new INV25_Facade();

                //更新資料庫
                facade.UpdateOne_StoreTransfer(INV25_DTO, logMsg.MACHINE_ID);

                BindMasterData();
            }
            catch (Exception ex)
            {
                string exstring = ex.Message;
                if (exstring.Substring(0, 3) != "000")
                {
                    exstring = exstring.Substring(4, exstring.Length - 4);
                }
                else
                {
                    exstring = ex.Message;
                }
                ScriptManager.RegisterClientScriptBlock(this, typeof(string), "checkSaveData", "alert('" + exstring.Replace("'", "-").Replace("\"", " ").Replace("\r", "").Replace("\n", "") +"');", true);
                return;
            }

         
        }
    }

    #endregion

    #region gvMaster 觸發事件

    protected void gvMaster_HtmlRowCreated(object sender, ASPxGridViewTableRowEventArgs e)
    {
        if (e.RowType == GridViewRowType.Detail)
        {
            // 繫結明細資料表           
            BindDetailData(StringUtil.CStr(e.GetValue(gvMaster.KeyFieldName)));
        }
    }

    protected void gvMaster_HtmlRowPrepared(object sender, ASPxGridViewTableRowEventArgs e)
    {
        if (e.RowType == GridViewRowType.Data)
        {
            ASPxLabel lblStatus = gvMaster.FindRowCellTemplateControl(e.VisibleIndex, (GridViewDataColumn)gvMaster.Columns["TSTATUS"], "lblStatus") as ASPxLabel;
        
            switch (StringUtil.CStr(e.GetValue("TSTATUS")))
            {
                case "20":
                    e.Row.ForeColor = System.Drawing.Color.Red;
                    lblStatus.Text = "在途";
                    break;
                case "30":
                    lblStatus.Text = "已撥入";
                    break;

            }
        }


        if (e.RowType == GridViewRowType.Detail)
        {

            ASPxButton btnSave = gvMaster.FindDetailRowTemplateControl(e.VisibleIndex, "btnSave") as ASPxButton;
            //ASPxButton btnCancel = gvMaster.FindDetailRowTemplateControl(e.VisibleIndex, "btnCancel") as ASPxButton;

            if (StringUtil.CStr(this.ddlTStatus.SelectedItem.Value) == "30")
            {
                btnSave.Enabled = false;
                //btnCancel.Enabled = false;
            }

        }
    }

    protected void gvMaster_PageIndexChanged(object sender, EventArgs e)
    {
        BindMasterData();
    }

    #endregion

    #region gvDetail 觸發事件

    protected void gvDetail_HtmlRowCreated(object sender, ASPxGridViewTableRowEventArgs e)
    {

        if (e.RowType == GridViewRowType.Data)
        {

            ASPxTextBox lblIMEI = e.Row.FindChildControl<ASPxTextBox>("lblIMEI_QTY");
            ASPxTextBox SEQNO = e.Row.FindChildControl<ASPxTextBox>("SEQNO");
            PopupControl txtIMEI = e.Row.FindChildControl<PopupControl>("txtIMEI");
            ASPxTextBox txtControl = txtIMEI.FindChildControl<ASPxTextBox>("txtControl");
            txtControl.ReadOnly = true;

            string strID = StringUtil.CStr(e.GetValue("STORETRANSFER_D_ID"));
            string strProdNo = StringUtil.CStr(e.GetValue("PRODNO"));

            // 繫結明細資料表           
            lblIMEI.Attributes["onmouseover"] = string.Format("show('{0}');", IMEIContent("STORETRANSFER_IMEI", strID, strProdNo));
            lblIMEI.Attributes["onmouseout"] = "hide();";
            
            ASPxPopupControl ASPxPopupControl1 = txtIMEI.FindChildControl<ASPxPopupControl>("ASPxPopupControl1");
            txtIMEI.KeyFieldValue1 = "STORETRANSFER_IMEI;" + strID + ";" + strProdNo + ";";
            
            //ASPxPopupControl1.ContentUrl = "~/VSS/INV/INV26/INV26_inputIMEIData.aspx?SysDate=Date()&KeyFieldValue1=" + txtIMEI.KeyFieldValue1;

            //**2011/04/27 Tina：傳遞參數時，要先以加密處理。
            string encryptUrl = Utils.Param_Encrypt("SysDate=Date()&KeyFieldValue1=" + txtIMEI.KeyFieldValue1);
            ASPxPopupControl1.ContentUrl = string.Format("~/VSS/INV/INV26/INV26_inputIMEIData.aspx?Param={0}", encryptUrl);


            int intC_IMEI = int.Parse(StringUtil.CStr(e.GetValue("TRANOUTQTY")) == "" ? "0" : StringUtil.CStr(e.GetValue("TRANOUTQTY")));
            int intS_IMEI = int.Parse(StringUtil.CStr(e.GetValue("IMEI_QTY")));
            ASPxImage imgIMEI = e.Row.FindChildControl<ASPxImage>("imgIMEI");

            //if (intC_IMEI == 0)
            if (StringUtil.CStr(e.GetValue("IMEI_FLAG")) != "3" && StringUtil.CStr(e.GetValue("IMEI_FLAG")) != "4")
            {
                HtmlGenericControl divIMEI = e.Row.FindChildControl<HtmlGenericControl>("divIMEI");
                divIMEI.Visible = false;
                imgIMEI.Visible = false;
            }

            if (imgIMEI.Visible)
            {
                imgIMEI.ImageUrl = (intC_IMEI == intS_IMEI ? "~/Icon/check.png" : "~/Icon/non_complete.png");
            }

            //PopupControl btnIMEI = e.Row.FindChildControl<PopupControl>("txtIMEI");

            //if ((intC_IMEI == 1) && (intC_IMEI - intS_IMEI == 1))
            //{
            //    btnIMEI.Enabled = false;
            //}

        }
    }

    protected void gvDetail_PageIndexChanged(object sender, EventArgs e)
    {
        DevExpress.Web.ASPxGridView.ASPxGridView detailGrid = gvMaster.FindChildControl<DevExpress.Web.ASPxGridView.ASPxGridView>("gvDetail");
        string STNO = StringUtil.CStr(detailGrid.GetRowValues(0, "STNO"));
        BindDetailData(STNO);
    }

    #endregion

    //ajax IMEI清單
    [System.Web.Services.WebMethod()]
    [System.Web.Script.Services.ScriptMethod()]
    static public string IMEIContent(string TABLENAME, string ID, string PRODNO)
    {
        DataTable dt = new IMEI_Facade().getINV_IMEI(TABLENAME, ID, PRODNO);

        string IMEI_FORMAT = "<table border=\"1\">";
        foreach (DataRow dr in dt.Rows)
        {
            IMEI_FORMAT += "<tr><td>" + StringUtil.CStr(dr["IMEI"]) + "</td></tr>";
        }
        IMEI_FORMAT += "</table>";

        return IMEI_FORMAT;
    }

    //ajax 呼當前網頁的方式
    [System.Web.Services.WebMethod()]
    [System.Web.Script.Services.ScriptMethod()]
    static public string getStoreInfo(string STORE_NO)
    {
        string strInfo = "";
        if (!string.IsNullOrEmpty(STORE_NO))
        {
            DataTable dt = new Store_Facade().Query_StoreInfo(STORE_NO);
            if (dt.Rows.Count > 0)
            {
                strInfo = StringUtil.CStr(dt.Rows[0]["STORENAME"]);
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
            DataTable dt = new Product_Facade().Query_ProductInfo(PRODNO);
            if (dt.Rows.Count > 0)
            {
                strInfo = StringUtil.CStr(dt.Rows[0]["PRODNAME"]);
            }
        }

        return strInfo;
    }
}
