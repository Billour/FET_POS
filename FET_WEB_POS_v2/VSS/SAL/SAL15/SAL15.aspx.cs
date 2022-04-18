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
using FET.POS.Model.DTO;
using FET.POS.Model.Facade;
using System.Web.UI.HtmlControls;
using DevExpress.Web.ASPxClasses;

public partial class VSS_SAL_SAL015 : BasePage
{
    private string QryUSER;

    protected void Page_Load(object sender, EventArgs e)
    {
        QryUSER = logMsg.OPERATOR;
        if (!IsPostBack && !Page.IsCallback)
        {
            DataTable dt = new SAL15_Facade().Query_HgConvertibleGiftM("");
            //取得空的資料表
            gvMaster.DataSource = dt;
            gvMaster.DataBind();
            this.gvMaster.FocusedRowIndex = -1;

        }
        
      
    }

   

    protected void BindMasterData()
    {
        DataTable dt = new SAL15_Facade().Query_HgConvertibleGiftM("");
        gvMaster.DataSource = dt;
        gvMaster.DataBind();
        gvMaster.Selection.UnselectAll();
    }

   
    #region Button 觸發事件

    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (gvMaster.Selection.Count == 0)
        {
            ScriptManager.RegisterClientScriptBlock(this, typeof(string), "商品", "alert('未勾選來店禮!');", true);
            return;
        }
       
        string ACTIVITY_ID = string.Empty; ;
        List<object> keyValues = this.gvMaster.GetSelectedFieldValues(gvMaster.KeyFieldName);
        foreach (string skey in keyValues)
        {
            ACTIVITY_ID +=  skey + ",";
        }
        
        int intlenght=ACTIVITY_ID.Length -1;
        ACTIVITY_ID = ACTIVITY_ID.Substring(0, intlenght);
        txtActivityId.Text = "";
        Session["ACTIVITY_ID"] = null;
        Session["ACTIVITY_ID"] = ACTIVITY_ID;
        //txtActivityId.Text = ACTIVITY_ID;
        //string HGCardCount = this.txtHGCardCount.Text;
        //string HGCardNo = this.txtHGCardNo.Text;
        //if (!string.IsNullOrEmpty(HGCardNo) && !string.IsNullOrEmpty(HGCardCount))
        //{
        //    var url2 = "SAL15_2.aspx?HGCardNo=" + HGCardNo + "&HGCardCount=" + HGCardCount;
        //    Response.Redirect(url2);
        //}

        //string script = "location.href='../../CheckOut/CheckOutHG6.aspx';";
        //ScriptManager.RegisterClientScriptBlock(this, typeof(string), "商品", script, true);
        Page.Response.Redirect("../../CheckOut/CheckOutHG6.aspx");
        
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

    protected void ddlType_SelectedIndexChanged(object sender, EventArgs e)
    {
        ASPxComboBox iType = gvMaster.FindChildControl<ASPxComboBox>("ddlType");
        PopupControl txtPRODNO = gvMaster.FindChildControl<PopupControl>("txtPRODNO");
        ASPxTextBox txtDIVIDABLE_POINT = gvMaster.FindChildControl<ASPxTextBox>("txtDIVIDABLE_POINT");
        if (StringUtil.CStr(iType.SelectedItem.Value) == "1")
        {
            txtDIVIDABLE_POINT.ClientEnabled = true;
            txtPRODNO.Enabled = false;
            txtPRODNO.Text = "";
        }
        else
        {
            txtPRODNO.Enabled = true;
            txtDIVIDABLE_POINT.ClientEnabled = false;
            txtDIVIDABLE_POINT.Text = "";
        }
    }
    #endregion

}
