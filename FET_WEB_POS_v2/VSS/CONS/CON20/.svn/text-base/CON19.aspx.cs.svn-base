using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using DevExpress.Web.ASPxGridView;
using DevExpress.Web.ASPxEditors;

using Advtek.Utility;
using FET.POS.Model.DTO;
using FET.POS.Model.Facade;
using System.Web.UI.HtmlControls;
using FET.POS.Model.Facade.FacadeImpl;
//using AdvTek.CustomControls;
using System.Web.Configuration;


public partial class VSS_CONS_CON19 : BasePage
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
        gvMaster.DataSource = new CON20_Facade().Query_CsmStoreTransferM(this.txtStno.Text,
            this.txtProdNo.Text, txtProductName.Text, this.ddlTStatus.SelectedItem.Value.ToString(),
            this.txtSTDate_S.Text, this.txtSTDate_E.Text,"", this.txtToStoreName.Text,
            this.logMsg.ROLE_TYPE, this.logMsg.STORENO, WebConfigurationManager.AppSettings["DefaultRoleHQ"]);
        gvMaster.DataBind();

        gvMaster.DetailRows.CollapseAllRows();

    }

    private void BindDetailData(string CsmStoretransferDId,string strStno)
    {


        DevExpress.Web.ASPxGridView.ASPxGridView detailGrid = gvMaster.FindChildControl<DevExpress.Web.ASPxGridView.ASPxGridView>("gvDetail");
        detailGrid.Caption = string.Format("<DIV align='left' style='font-size:10pt'>移撥編號：{0}</DIV>", strStno);
        detailGrid.DataSource = new CON20_Facade().Query_CsmStoreTransferD(CsmStoretransferDId);
        detailGrid.DataBind();
    }
 

    #region Button 觸發事件

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        BindMasterData();
        this.gvMaster.PageIndex = 0;
    }

    protected void btnClear_Click(object sender, EventArgs e)
    {
        this.txtStno.Text = "";
        //this.txtProductName.Text = "";
        this.ddlTStatus.SelectedIndex = 0;
        this.txtSTDate_S.Text = null;
        this.txtSTDate_S.IsValid = true;
        this.txtSTDate_E.Text = null;
        this.txtToStoreName.Text = "";
        txtProdNo.Text = "";
    }

    #endregion


    
    #region gvMaster 觸發事件

    protected void gvMaster_HtmlRowCreated(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewTableRowEventArgs e)
    {
        if (e.RowType == GridViewRowType.Detail)
        {
            // 繫結明細資料表   
            BindDetailData(e.GetValue(gvMaster.KeyFieldName).ToString(),e.GetValue("STNO").ToString());
        }
      
    }

    protected void gvMaster_HtmlRowPrepared(object sender, ASPxGridViewTableRowEventArgs e)
    {
        if (e.RowType == GridViewRowType.Data)
        {
            ASPxLabel lblStatus = gvMaster.FindRowCellTemplateControl(e.VisibleIndex, (GridViewDataColumn)gvMaster.Columns["TSTATUS"], "lblStatus") as ASPxLabel;

            switch (e.GetValue("TSTATUS").ToString())
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
            //PopupControl txtIMEI = e.Row.FindChildControl<PopupControl>("txtIMEI");
            //ASPxPopupControl ASPxPopupControl1 = txtIMEI.FindChildControl<ASPxPopupControl>("ASPxPopupControl1");
            //ASPxTextBox txtControl = txtIMEI.FindChildControl<ASPxTextBox>("txtControl");
            //txtControl.ReadOnly = true;

            //string strID = e.GetValue("CSM_STORETRANSFER_D_ID").ToString();

            // 繫結明細資料表           
            //lblIMEI.Attributes["onmouseover"] = string.Format("show('{0}');", IMEIContent("STORETRANSFER_IMEI", strID, e.GetValue("PRODNO").ToString()));
            //lblIMEI.Attributes["onmouseout"] = "hide();";

            //txtIMEI.KeyFieldValue1 = "STORETRANSFER_IMEI;" + strID + ";" + e.GetValue("PRODNO").ToString() + ";";
            //SPxPopupControl1.ContentUrl = "~/VSS/INV/INV26/INV26_inputIMEIData.aspx?SysDate=Date()&KeyFieldValue1=" + txtIMEI.KeyFieldValue1;

            //int intC_IMEI = int.Parse(e.GetValue("TRANOUTQTY").ToString() == "" ? "0" : e.GetValue("TRANOUTQTY").ToString());
            //int intS_IMEI = int.Parse(e.GetValue("IMEI_QTY").ToString() == "" ? "0" : e.GetValue("IMEI_QTY").ToString());
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


            //PopupControl lblShowIMEI = e.Row.FindChildControl<PopupControl>("lblShowIMEI");
            // lblShowIMEI.Enabled = false;

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
   
    

}
