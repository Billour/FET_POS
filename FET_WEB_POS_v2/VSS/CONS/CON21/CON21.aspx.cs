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

public partial class VSS_CONS_CON21 : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack && !Page.IsCallback)
        {
            BindMasterData();
            lblToStoreName.Text = logMsg.STORENO;
        }

    }


    protected void BindMasterData()
    {
        DataTable dt = new CON20_Facade().Query_StoreTransferM_ByCON21(this.txtStno.Text,
            "", txtProductName.Text, StringUtil.CStr(this.ddlTStatus.SelectedItem.Value),
             this.TransferOutDateFrom.Text, this.TransferOutDateTo.Text,
            this.logMsg.ROLE_TYPE, this.logMsg.STORENO, WebConfigurationManager.AppSettings["DefaultRoleHQ"],
            txtFormStoreNo.Text, txtTSTDate_S.Text, txtTSTDate_E.Text);
        gvMaster.DataSource = dt;
        gvMaster.DataBind();
        gvMaster.DetailRows.CollapseAllRows();
        Session["gvMaster"] = dt;
    }
    private void BindDetailData(string CsmStoretransferDId, string strStno)
    {
        Session["gvDetail"] = null;
        DevExpress.Web.ASPxGridView.ASPxGridView detailGrid = gvMaster.FindChildControl<DevExpress.Web.ASPxGridView.ASPxGridView>("gvDetail");
        detailGrid.Caption = string.Format("<DIV align='left' style='font-size:10pt'>移撥編號：{0}</DIV>", strStno);
        DataTable dt = new CON20_Facade().Query_CsmStoreTransferD(CsmStoretransferDId);
        detailGrid.DataSource = dt;
        detailGrid.DataBind();
        Session["gvDetail"] = dt;
    }
    #region Button 觸發事件

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        BindMasterData();
        this.gvMaster.PageIndex = 0;
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        DataTable dtDetail = new DataTable();
        if (Session["gvDetail"] != null)
        {
            dtDetail = Session["gvDetail"] as DataTable;
        }
        if (dtDetail != null)
        {
            CON20_CSM_StoreTransfer_DTO CON20_DTO = new CON20_CSM_StoreTransfer_DTO();
            CON20_CSM_StoreTransfer_DTO.CSM_STORETRANSFER_MDataTable dtM = CON20_DTO.CSM_STORETRANSFER_M;
            CON20_CSM_StoreTransfer_DTO.CSM_STORETRANSFER_MRow drM = dtM.NewCSM_STORETRANSFER_MRow();

            CON20_CSM_StoreTransfer_DTO.CSM_STORETRANSFER_DDataTable dtD = CON20_DTO.CSM_STORETRANSFER_D;
            string strCSM_STORETRANSFERM_ID = StringUtil.CStr(gvMaster.GetRowValues(0, "CSM_STORETRANSFERM_ID"));
            string strSTNO =StringUtil.CStr( gvMaster.GetRowValues(0, "STNO"));
            drM.STNO = strSTNO;
            drM.CSM_STORETRANSFERM_ID = strCSM_STORETRANSFERM_ID;
            drM.TSTATUS = "30";//調撥單狀態：  00 未存檔,10 暫存(預留), 20 在途, 30 已撥入
            drM.TO_STORE_NO = logMsg.STORENO;
            drM.MODI_USER = this.logMsg.OPERATOR;//異動人員
            drM.MODI_DTM = System.DateTime.Today;//異動時間
            drM.TSTDATE = System.DateTime.Now;//OracleDBUtil.WorkDay(this.logMsg.STORENO); //System.DateTime.Today.ToString("yyyy/MM/dd"); //調入日期
            drM.TSTUSRNO = this.logMsg.OPERATOR;//調入端驗收人員
            drM.TUPDDATE = System.DateTime.Now; //System.DateTime.Today.ToString("yyyy/MM/dd");//調入端異動時間

            dtM.Rows.Add(drM);
            CON20_DTO.AcceptChanges();
           
            if(dtDetail.Rows.Count>0)
            {
                int i =1;
                foreach (DataRow dr in dtDetail.Rows )
                {
                    CON20_CSM_StoreTransfer_DTO.CSM_STORETRANSFER_DRow drD = dtD.NewCSM_STORETRANSFER_DRow();
                    drD.PRODNO = StringUtil.CStr(dr["PRODNO"]);
                    drD.CSM_STORETRANSFER_D_ID = StringUtil.CStr(dr["CSM_STORETRANSFER_D_ID"]);
                    drD.SEQNO = StringUtil.CStr(i);
                    drD.TRANINQTY =Convert.ToInt64( StringUtil.CStr(dr["TRANOUTQTY"])); //撥入數量
                    drD.STDATE = System.DateTime.Now; //System.DateTime.Today.ToString("yyyy/MM/dd");//調入驗收日期
                    drD.MODI_USER = this.logMsg.OPERATOR;//異動人員
                    drD.MODI_DTM = System.DateTime.Today;//異動時間
                    drD.SUPP_ID = new Supplier_Facade().GetSuppId2(StringUtil.CStr(dr["PRODNO"]));
                    drD.CSM_STORETRANSFERM_ID = strCSM_STORETRANSFERM_ID;
                    dtD.Rows.Add(drD);
                    CON20_DTO.AcceptChanges();
                    i++;
                }
            }
           
            try
            {
                CON20_Facade facade = new CON20_Facade();

                //更新資料庫
                facade.UpdateOne_StoreTransfer(CON20_DTO, logMsg.MACHINE_ID);
                ScriptManager.RegisterClientScriptBlock(this, typeof(string), "SaveData", "alert('撥入成功');", true);

                BindMasterData();
            }
            catch (Exception ex)
            {
                string exstring = StringUtil.CStr(ex.Message);
                if (exstring.Substring(0, 3) == "999")
                {
                    exstring = exstring.Substring(4, exstring.Length - 4);
                }
                else
                {
                    exstring = ex.Message;
                }
                ScriptManager.RegisterClientScriptBlock(this, typeof(string), "checkSaveData", "alert('" + exstring + "');", true);
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
            BindDetailData(StringUtil.CStr(e.GetValue(gvMaster.KeyFieldName)),StringUtil.CStr( e.GetValue("STNO")));
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
            ASPxButton btnCancel = gvMaster.FindDetailRowTemplateControl(e.VisibleIndex, "btnCancel") as ASPxButton;
            DevExpress.Web.ASPxGridView.ASPxGridView gvDetail = gvMaster.FindChildControl<DevExpress.Web.ASPxGridView.ASPxGridView>("gvDetail");

            ASPxTextBox txtTraninQty = gvDetail.FindRowCellTemplateControl(e.VisibleIndex, (GridViewDataColumn)gvDetail.Columns["TRANINQTY"], "txtTraninQty") as ASPxTextBox;
            if (StringUtil.CStr(this.ddlTStatus.SelectedItem) == "30")
            {
                btnSave.Visible  = false;
                btnCancel.Visible = false;
                txtTraninQty.ReadOnly =true;
                
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

            //ASPxTextBox lblIMEI = e.Row.FindChildControl<ASPxTextBox>("lblIMEI_QTY");
            //ASPxTextBox SEQNO = e.Row.FindChildControl<ASPxTextBox>("SEQNO");
            //PopupControl txtIMEI = e.Row.FindChildControl<PopupControl>("txtIMEI");
            //ASPxTextBox txtControl = txtIMEI.FindChildControl<ASPxTextBox>("txtControl");
            //txtControl.ReadOnly = true;

            //string strID = e.GetValue("STORETRANSFER_D_ID").ToString();
            //string strProdNo = e.GetValue("PRODNO").ToString();

            //// 繫結明細資料表           
            //lblIMEI.Attributes["onmouseover"] = string.Format("show('{0}');", IMEIContent("STORETRANSFER_IMEI", strID, strProdNo));
            //lblIMEI.Attributes["onmouseout"] = "hide();";

            //ASPxPopupControl ASPxPopupControl1 = txtIMEI.FindChildControl<ASPxPopupControl>("ASPxPopupControl1");
            //txtIMEI.KeyFieldValue1 = "STORETRANSFER_IMEI;" + strID + ";" + strProdNo + ";";
            //ASPxPopupControl1.ContentUrl = "~/VSS/INV/INV26/INV26_inputIMEIData.aspx?SysDate=Date()&KeyFieldValue1=" + txtIMEI.KeyFieldValue1;


            //int intC_IMEI = int.Parse(e.GetValue("TRANOUTQTY").ToString() == "" ? "0" : e.GetValue("TRANOUTQTY").ToString());
            //int intS_IMEI = int.Parse(e.GetValue("IMEI_QTY").ToString());
            //ASPxImage imgIMEI = e.Row.FindChildControl<ASPxImage>("imgIMEI");

            ////if (intC_IMEI == 0)
            //if (e.GetValue("IMEI_FLAG").ToString() != "3" && e.GetValue("IMEI_FLAG").ToString() != "4")
            //{
            //    HtmlGenericControl divIMEI = e.Row.FindChildControl<HtmlGenericControl>("divIMEI");
            //    divIMEI.Visible = false;
            //    imgIMEI.Visible = false;
            //}

            //if (imgIMEI.Visible)
            //{
            //    imgIMEI.ImageUrl = (intC_IMEI == intS_IMEI ? "~/Icon/check.png" : "~/Icon/non_complete.png");
            //}

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
        string CSM_STORETRANSFER_ID = detailGrid.GetRowValues(0, "CSM_STORETRANSFERM_ID").ToString();
        string STNO = detailGrid.GetRowValues(0, "STNO").ToString();
        BindDetailData(CSM_STORETRANSFER_ID, STNO);
    }

    #endregion

    #region 呼叫前端ajax

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
                strInfo = dt.Rows[0]["STORENAME"].ToString();
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
                strInfo = dt.Rows[0]["PRODNAME"].ToString();
            }
        }

        return strInfo;
    }
    #endregion
}
