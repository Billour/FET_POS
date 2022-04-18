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

using System.Web.Configuration;


public partial class VSS_INV_INV01 : BasePage
{
    string sQueryUserID =  "";
    string sQueryStoreID = "";
    string sRole = "";
    string sMachine_id = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        sQueryUserID = logMsg.OPERATOR;
        sQueryStoreID = logMsg.STORENO;
        sMachine_id = logMsg.MACHINE_ID;
        sRole = logMsg.ROLE_TYPE;
        
        if (!IsPostBack && !IsCallback)
        {
            if (logMsg.ROLE_TYPE != StringUtil.CStr(System.Configuration.ConfigurationManager.AppSettings["DefaultRoleHQ"]))
            {
                //彈跳視窗
                ScriptManager.RegisterClientScriptBlock(this, GetType(), "AlertMessage", "alert('非總部人員無法使用此功能!!');", true);
                grid.Enabled = false;
                txtTransferNo.ClientEnabled = false;    //移撥單號
                ProductsPopup.Enabled = false;    //商品料號
                transferOutStartDate.Enabled = false;    //移出日期起
                transferOutStartDate.ControlStyle.BackColor = System.Drawing.Color.LightGray;
                transferOutStartEndDate.Enabled = false;    //移出日期訖
                transferOutStartEndDate.ControlStyle.BackColor = System.Drawing.Color.LightGray;
                transferOutStorePopup.Enabled = false;    //移出門市代碼
                lblStoareFromName.Enabled = false;    //移出門市名稱  
                transferInStartDate.Enabled = false;    //撥入日期起
                transferInStartDate.ControlStyle.BackColor = System.Drawing.Color.LightGray;
                transferInEndDate.Enabled = false;    //撥入日期訖
                transferInEndDate.ControlStyle.BackColor = System.Drawing.Color.LightGray;
                transferInStorePopup.Enabled = false;    //撥入門市代碼
                lblStoreToName.Enabled = false;    //撥入門市名稱
                cobStatus.Enabled = false;    //狀態
                btnSearch.Enabled = false;
                resetButton.Enabled = false;
                return;
            }
        }               
    }
    
    private DataTable GetMasterData()
    {
        DataTable dtResult = new DataTable();
        INV01_Facade cInv01 = new INV01_Facade();
        dtResult = cInv01.Query_STORETRANSFER_M(txtTransferNo.Text.Trim()
                                              , ProductsPopup.Text.Trim()
                                              , StringUtil.CStr(cobStatus.SelectedItem.Value)
                                              , transferOutStartDate.Text.Trim()
                                              , transferOutStartEndDate.Text.Trim()
                                              , transferInStartDate.Text.Trim()
                                              , transferInEndDate.Text.Trim()
                                              , transferOutStorePopup.Text.Trim()
                                              , transferInStorePopup.Text.Trim()
                                              , lblStoareFromName.Text.Trim()
                                              , lblStoreToName.Text.Trim());
        return dtResult;
    }

    private DataTable GetDetailData( string strSTNO)
    {
        tempstoreNO.Value = "";
        DataTable gvDetail = new DataTable();
        INV01_Facade cInv01 = new INV01_Facade();
        gvDetail=cInv01.Query_STORETRANSFER_D(strSTNO);
               
        return gvDetail;

    }

    #region Button 觸發事件

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        
        try
        {
            if (!(String.IsNullOrEmpty(transferOutStartDate.Text)) && !(String.IsNullOrEmpty(transferOutStartEndDate.Text)) && Convert.ToDateTime(transferOutStartDate.Text) > Convert.ToDateTime(transferOutStartEndDate.Text))
            {
                transferOutStartEndDate.Focus();
                throw new Exception("移出日期訖不允許小於移出日期起，請重新輸入");
            }
            if (!(String.IsNullOrEmpty(transferInStartDate.Text)) && !(String.IsNullOrEmpty(transferInEndDate.Text)) && Convert.ToDateTime(transferInStartDate.Text) > Convert.ToDateTime(transferInEndDate.Text)) 
            {
                transferInEndDate.Focus();
                throw new Exception("撥入日期訖不允許小於撥入日期起，請重新輸入");
            }
            
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Common", @"alert('" + ex.Message + "');", true);
            return;
        }
        Session["DataSource"] = GetMasterData();
        // 繫結主要的資料表
        grid.DataSource = GetMasterData();        
        grid.DataBind();
        grid.PageIndex = 0;
        grid.FocusedRowIndex = -1;
    }

    protected void btnXlsExport_Click(object sender, EventArgs e)
    {
        if (grid.FocusedRowIndex > -1)
        {
            object key = grid.GetRowValues(grid.FocusedRowIndex, grid.KeyFieldName);
            ExportExcelData eel = new ExportExcelData();
            this.Page.Controls.Add(eel);
            DataTable dtTmp = new INV01_Facade().ExportSTORETRANSFER_M_D(StringUtil.CStr(key));
            //清除重複主檔資料
            string sSTNO = "", sPRODNO = "";
            for (int i = 0; i < dtTmp.Rows.Count; i++)
            {
                if (sSTNO == StringUtil.CStr(dtTmp.Rows[i][0]))
                {
                    for (int j = 0; j < 8; j++)
                    {
                        dtTmp.Rows[i][j] = DBNull.Value;
                    }
                }
                else
                {
                    sSTNO = StringUtil.CStr(dtTmp.Rows[i][0]);
                }

                if (sPRODNO == StringUtil.CStr(dtTmp.Rows[i][8]))
                {
                    for (int j = 8; j < 11; j++)
                    {
                        dtTmp.Rows[i][j] = DBNull.Value;
                    }
                }
                else
                {
                    sPRODNO = StringUtil.CStr(dtTmp.Rows[i][8]);
                }

                if (StringUtil.CStr(dtTmp.Rows[i][11]) != "")
                    dtTmp.Rows[i][11] = "'" + StringUtil.CStr(dtTmp.Rows[i][11]);
                if (StringUtil.CStr(dtTmp.Rows[i][13]) != "")
                    dtTmp.Rows[i][13] = "'" + StringUtil.CStr(dtTmp.Rows[i][13]);
            }
            eel.ExportExcel(dtTmp);

        }
        else
        {
            ScriptManager.RegisterClientScriptBlock(this, GetType(), "AlertMessage", "alert('請先選擇欲匯出之資料!!');", true);
        
        }


    }

    #endregion

    #region gvMaster 觸發事件

    protected void grid_HtmlRowPrepared(object sender, ASPxGridViewTableRowEventArgs e)
    {
        if (e.RowType == GridViewRowType.Data)
        {
            if (StringUtil.CStr(e.GetValue("TSTATUS")) == "在途")
            {
                e.Row.ForeColor = System.Drawing.Color.Red;
            }
       
        }
    }

    protected void grid_HtmlRowCreated(object sender, ASPxGridViewTableRowEventArgs e)
    {
        if (e.RowType == GridViewRowType.Detail)
        {
            // 繫結明細資料表           
            ASPxGridView detailGrid = e.Row.FindChildControl<ASPxGridView>("detailGrid");
            string sSTNO=StringUtil.CStr(e.GetValue(grid.KeyFieldName));
            detailGrid.DataSource = GetDetailData(sSTNO);
            detailGrid.DataBind();
            Label lblTmp = (Label)detailGrid.FindTitleTemplateControl("Label5");
            if (lblTmp != null) 
            {
                lblTmp.Text = sSTNO;
            }
        }
    }
    
    protected void grid_PageIndexChanged(object sender, EventArgs e)
    {
        ASPxGridView grid = sender as ASPxGridView;        
        grid.DataSource = GetMasterData();            
        grid.DataBind();
    }

    #endregion

    #region gvDetail 觸發事件

    protected void detailGrid_DataSelect(object sender, EventArgs e)
    {
        Session["移撥單號"] = (sender as ASPxGridView).GetMasterRowKeyValue();
    }

    protected void detailGrid_PageIndexChanged(object sender, EventArgs e)
    {
        ASPxGridView grid = sender as ASPxGridView;
        DataTable dt = GetDetailData(StringUtil.CStr(grid.GetRowValues(grid.FocusedRowIndex, grid.KeyFieldName)));
        grid.DataSource = dt;
        grid.DataBind();
    }

    protected void detailGrid_HtmlRowCreated(object sender, ASPxGridViewTableRowEventArgs e)
    {
        if (e.RowType == GridViewRowType.Data)
        {
            ASPxLabel lblOUTIMEI = e.Row.FindChildControl<ASPxLabel>("lblOUTIMEI");
            ASPxLabel lblINIMEI = e.Row.FindChildControl<ASPxLabel>("lblINIMEI");

            string strSTNO = StringUtil.CStr(e.GetValue("STNO"));

            // 繫結IMEI data 
            string imeiHtml = string.Format("show('{0}');", IMEIContent(strSTNO));
            lblOUTIMEI.Attributes["onmouseover"] = imeiHtml;
            lblOUTIMEI.Attributes["onmouseout"] = "hide()";
            lblINIMEI.Attributes["onmouseover"] = imeiHtml;
            lblINIMEI.Attributes["onmouseout"] = "hide();";

            int intC_IMEI = int.Parse(StringUtil.CStr(e.GetValue("OUTQTY")) == "" ? "0" : StringUtil.CStr(e.GetValue("OUTQTY")));
            int intS_IMEI = int.Parse(StringUtil.CStr(e.GetValue("IMEI_QTY")) == "" ? "0" : StringUtil.CStr(e.GetValue("IMEI_QTY")));
            ASPxImage imgOUTIMEI = e.Row.FindChildControl<ASPxImage>("imgOUTIMEI");
            ASPxImage imgINIMEI = e.Row.FindChildControl<ASPxImage>("imgINIMEI");

            if (StringUtil.CStr(e.GetValue("IMEI_FLAG")) != "3" && StringUtil.CStr(e.GetValue("IMEI_FLAG")) != "4")
            {
                HtmlGenericControl divOUTIMEI = e.Row.FindChildControl<HtmlGenericControl>("divOUTIMEI");
                divOUTIMEI.Visible = false;
                imgOUTIMEI.Visible = false;

                HtmlGenericControl divINIMEI = e.Row.FindChildControl<HtmlGenericControl>("divINIMEI");
                divINIMEI.Visible = false;
                imgINIMEI.Visible = false;
            }

            if (imgOUTIMEI.Visible)
            {
                imgOUTIMEI.ImageUrl = (intC_IMEI == intS_IMEI ? "~/Icon/check.png" : "~/Icon/non_complete.png");
            }
            if (imgINIMEI.Visible)
            {
                imgINIMEI.ImageUrl = (intC_IMEI == intS_IMEI ? "~/Icon/check.png" : "~/Icon/non_complete.png");
            }

            PopupControl lblOUTShowIMEI = e.Row.FindChildControl<PopupControl>("lblOUTShowIMEI");
            lblOUTShowIMEI.Enabled = false;
            PopupControl lblShowINIMEI = e.Row.FindChildControl<PopupControl>("lblShowINIMEI");
            lblShowINIMEI.Enabled = false;

            ASPxTextBox Txt_PRODNO = e.Row.FindChildControl<ASPxTextBox>("Txt_PRODNO");
            ASPxTextBox PRODNAME = e.Row.FindChildControl<ASPxTextBox>("Txt_PRODNAME");
            ASPxTextBox Txt_OUTQTY = e.Row.FindChildControl<ASPxTextBox>("Txt_OUTQTY");
            ASPxTextBox Txt_OUTIMEI = e.Row.FindChildControl<ASPxTextBox>("Txt_OUTIMEI");
            ASPxTextBox Txt_INIMEI = e.Row.FindChildControl<ASPxTextBox>("Txt_INIMEI");
            
            if (tempstoreNO.Value != Txt_PRODNO.Text)
            {
                
            }
            else
            {
                e.Row.Visible = false;
                //e.Row.Cells[0].Text = "";
                //e.Row.Cells[1].Text = "";
                //e.Row.Cells[2].Text = "";
            }

            if (lblOUTShowIMEI.Text == "")
                e.Row.Cells[4].Text = "";
            if (lblShowINIMEI.Text == "")
                e.Row.Cells[7].Text = "";

            if (StringUtil.CStr(e.GetValue("INQTY")) == "")
            {
                e.Row.Cells[5].Text = "";
                e.Row.Cells[6].Text = "";
                e.Row.Cells[7].Text = "";
            }

            if (tempstoreNO.Value == "" || tempstoreNO.Value != Txt_PRODNO.Text)
            {
                tempstoreNO.Value = Txt_PRODNO.Text;
            }

        }
    }

    #endregion

    //ajax 呼當前網頁的方式
    [System.Web.Services.WebMethod()]
    [System.Web.Script.Services.ScriptMethod()]
    static public string getProductInfo(string PRODUCT_NO)
    {
        string strInfo = "";
        if (!string.IsNullOrEmpty(PRODUCT_NO))
        {
            DataTable _dt = new Product_Facade().Query_ProductInfo(PRODUCT_NO);
            if (_dt.Rows.Count > 0) 
            {
                strInfo = StringUtil.CStr(_dt.Rows[0]["PRODNAME"]);
            }
        }

        return strInfo;
    }

    //ajax 呼當前網頁的方式
    [System.Web.Services.WebMethod()]
    [System.Web.Script.Services.ScriptMethod()]
    static public string getStoreInfo(string STORE_NO)
    {
        string strInfo = "";
        if (!string.IsNullOrEmpty(STORE_NO))
        {
            DataTable _dt = new Store_Facade().Query_StoreInfo(STORE_NO);
            if (_dt.Rows.Count > 0)
            {
                strInfo = StringUtil.CStr(_dt.Rows[0]["STORENAME"]);
            }
        }

        return strInfo;
    }

    private string IMEIContent(string strSTNO)
    {
        DataTable dt = new INV01_Facade().Query_IMEIList(strSTNO);

        string IMEI_FORMAT = "<table border=\"1\">";
        foreach (DataRow dr in dt.Rows)
        {
            IMEI_FORMAT += "<tr><td>" + StringUtil.CStr(dr["IMEI"]) + "</td></tr>";
        }
        IMEI_FORMAT += "</table>";

        return IMEI_FORMAT;
    }
}
