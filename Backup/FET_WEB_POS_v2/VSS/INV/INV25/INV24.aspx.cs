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
using AdvTek.CustomControls;
using System.Web.Configuration;

public partial class VSS_INV_INV24 : BasePage
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
        gvMaster.DataSource = new INV25_Facade().Query_StoreTransferM(this.txtStno.Text, 
            this.txtProdNo.Text, this.txtProductName.Text, StringUtil.CStr(this.ddlTStatus.SelectedItem.Value),
            this.txtSTDate_S.Text, this.txtSTDate_E.Text,"", this.txtToStoreName.Text,
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

    private string IMEIContent(string TABLENAME, string ID, string PRODNO)
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

    #region Button 觸發事件

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        BindMasterData();
        this.gvMaster.PageIndex = 0;
    }

    protected void btnClear_Click(object sender, EventArgs e)
    {
        this.txtStno.Text = "";
        this.txtProductName.Text = "";
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
            PopupControl txtIMEI = e.Row.FindChildControl<PopupControl>("txtIMEI");
            ASPxPopupControl ASPxPopupControl1 = txtIMEI.FindChildControl<ASPxPopupControl>("ASPxPopupControl1");
            ASPxTextBox txtControl = txtIMEI.FindChildControl<ASPxTextBox>("txtControl");
            txtControl.ReadOnly = true;

            string strID = StringUtil.CStr(e.GetValue("STORETRANSFER_D_ID"));

            // 繫結明細資料表           
            lblIMEI.Attributes["onmouseover"] = string.Format("show('{0}');", IMEIContent("STORETRANSFER_IMEI", strID, StringUtil.CStr(e.GetValue("PRODNO"))));
            lblIMEI.Attributes["onmouseout"] = "hide();";

            txtIMEI.KeyFieldValue1 = "STORETRANSFER_IMEI;" + strID + ";" + StringUtil.CStr(e.GetValue("PRODNO")) + ";";
            //ASPxPopupControl1.ContentUrl = "~/VSS/INV/INV26/INV26_inputIMEIData.aspx?SysDate=Date()&KeyFieldValue1=" + txtIMEI.KeyFieldValue1;

            //**2011/04/27 Tina：傳遞參數時，要先以加密處理。
            string encryptUrl = Utils.Param_Encrypt("SysDate=Date()&KeyFieldValue1=" + txtIMEI.KeyFieldValue1);
            ASPxPopupControl1.ContentUrl = string.Format("~/VSS/INV/INV26/INV26_inputIMEIData.aspx?Param={0}", encryptUrl);

            int intC_IMEI = int.Parse(StringUtil.CStr(e.GetValue("TRANOUTQTY")) == "" ? "0" : StringUtil.CStr(e.GetValue("TRANOUTQTY")));
            int intS_IMEI = int.Parse(StringUtil.CStr(e.GetValue("IMEI_QTY")) == "" ? "0" : StringUtil.CStr(e.GetValue("IMEI_QTY")));
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

           
            //PopupControl lblShowIMEI = e.Row.FindChildControl<PopupControl>("lblShowIMEI");
           // lblShowIMEI.Enabled = false;

        }
    }

    protected void gvDetail_PageIndexChanged(object sender, EventArgs e)
    {
        DevExpress.Web.ASPxGridView.ASPxGridView detailGrid = gvMaster.FindChildControl<DevExpress.Web.ASPxGridView.ASPxGridView>("gvDetail");
        string STNO = StringUtil.CStr(detailGrid.GetRowValues(0, "STNO"));
        BindDetailData(STNO);
    }

    #endregion

  

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

