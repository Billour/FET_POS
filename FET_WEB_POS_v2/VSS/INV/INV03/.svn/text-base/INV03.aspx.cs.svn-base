using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Advtek.Utility;
using DevExpress.Web;
using DevExpress.Web.ASPxEditors;
using DevExpress.Web.ASPxGridView;
using DevExpress.Web.ASPxGridView.Rendering;
using DevExpress.Web.ASPxPager;
using FET.POS.Model.Facade;
using FET.POS.Model.Facade.FacadeImpl;
using FET.POS.Model.DTO;
using FET.POS.Model.Common;
using System.Runtime.Serialization;

public partial class VSS_INV_INV03 : BasePage
{
    #region Class Varibles

    //private INV03_Facade _INV03_Facade;
    //private INV03_STOCKSet_DTO _STOCKSet_DTO;

    string strZone
    {
        set
        {
            ViewState["strZone"] = value;
        }
        get
        {
            if (ViewState["strZone"] == null)
                return string.Empty;

            return (string)ViewState["strZone"];
        }
    }

    string strStoreNo
    {
        set
        {
            ViewState["strStoreNo"] = value;
        }
        get
        {
            if (ViewState["strStoreNo"] == null)
                return string.Empty;

            return (string)ViewState["strStoreNo"];
        }
    }

    string strStoreName
    {
        set
        {
            ViewState["strStoreName"] = value;
        }
        get
        {
            if (ViewState["strStoreName"] == null)
                return string.Empty;

            return (string)ViewState["strStoreName"];
        }
    }

    string strProdType
    {
        set
        {
            ViewState["strProdType"] = value;
        }
        get
        {
            if (ViewState["strProdType"] == null)
                return string.Empty;

            return (string)ViewState["strProdType"];
        }
    }

    string strProdNo
    {
        set
        {
            ViewState["strProdNo"] = value;
        }
        get
        {
            if (ViewState["strProdNo"] == null)
                return string.Empty;

            return (string)ViewState["strProdNo"];
        }
    }

    string strProdName
    {
        set
        {
            ViewState["strProdName"] = value;
        }
        get
        {
            if (ViewState["strProdName"] == null)
                return string.Empty;

            return (string)ViewState["strProdName"];
        }
    }

    string strLoc
    {
        set
        {
            ViewState["strLoc"] = value;
        }
        get
        {
            if (ViewState["strLoc"] == null)
                return string.Empty;

            return (string)ViewState["strLoc"];
        }
    }

    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        //2011/02/09 Tina：加入此判斷，否則畫面PostBack後，Store的TextBox會Enabled
        if (logMsg.ROLE_TYPE != StringUtil.CStr(System.Configuration.ConfigurationManager.AppSettings["DefaultRoleHQ"]))
        {
            txtStoreNo.Enabled = false;
        }

        if (!IsPostBack && !IsCallback)
        {
            GetZoneInf0();
            GetLocInf0();
            bindCategory();

            if (logMsg.ROLE_TYPE != StringUtil.CStr(System.Configuration.ConfigurationManager.AppSettings["DefaultRoleHQ"]))
            {
                txtStoreNo.Text = logMsg.STORENO;
                DataTable dt = new Store_Facade().Query_StoreZone_ByKey(logMsg.STORENO);
                if (dt.Rows.Count > 0)
                {
                    txtStoreName.Text = StringUtil.CStr(dt.Rows[0]["STORENAME"]);

                }

                DataTable dte = new Employee_Facade().Query_SALESCD(logMsg.OPERATOR);
                DataTable dtZ = new Store_Facade().Query_StoreZone_ByKey(dte.Rows[0]["SALESCD"].ToString());

                if (dtZ.Rows.Count > 0)
                {
                    cbbZone.SelectedIndex = cbbZone.Items.IndexOfValue(StringUtil.CStr(dtZ.Rows[0]["ZONE"]));
                }
                
  
                cbbZone.Enabled = false;
                txtStoreName.Enabled = false;
            }
        }
    }

    private void GetZoneInf0()
    {
        DataTable dt = new DataTable();
        dt = Common_PageHelper.getZone(true);
        // cbbZone.Items.Add(" ");
        cbbZone.DataSource = dt;
        cbbZone.TextField = "ZONE_NAME";
        cbbZone.ValueField = "ZONE";
        cbbZone.DataBind();

        cbbZone.SelectedIndex = 0;
    }

    private void GetLocInf0()
    {
        DataTable dt = new DataTable();
        dt = INV03_PageHelper.GetLocInfoMethodData(true);

        cbbLoc.DataSource = dt;
        cbbLoc.TextField = "STOCK_NAME";
        cbbLoc.ValueField = "LOC_ID";
        cbbLoc.DataBind();

        cbbLoc.SelectedIndex = cbbLoc.Items.IndexOfText("銷售倉");
    }

    private void bindCategory()
    {
        DataTable dt = new DataTable();
        dt = PRODUCT_PageHelper.GetProDTypeNo(true);
        cboCategory.DataSource = dt;
        cboCategory.TextField = "PRODTYPE_NAME";
        cboCategory.ValueField = "PRODTYPE_NO";
        cboCategory.DataBind();
    }

    private DataTable GetGvMasterData()
    {
        DataTable dt = new DataTable();

        dt = INV03_PageHelper.GetStockMethodData
                     (strZone,
                     strStoreNo,
                     strStoreName,
                     strProdType,
                     strProdNo,
                     strProdName,
                     strLoc);

        return dt;
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        strZone = StringUtil.CStr(cbbZone.SelectedItem.Value);
        strStoreNo = txtStoreNo.Text.Trim();
        strStoreName = txtStoreName.Text.Trim();
        strProdType = StringUtil.CStr(cboCategory.SelectedItem.Value);
        strProdNo = txtProdNo.Text.Trim();
        strProdName = txtProdName.Text.Trim();
        strLoc = StringUtil.CStr(cbbLoc.SelectedItem.Value);

        DataTable dtQuery = new DataTable();

        dtQuery = GetGvMasterData();

        gvMaster.DataSource = dtQuery;
        gvMaster.DataBind();
        gvMaster.PageIndex = 0;
    }

    protected void gvMaster_PageIndexChanged(object sender, EventArgs e)
    {
        ASPxGridView gvMaster = sender as ASPxGridView;
        gvMaster.DataSource = GetGvMasterData();
        gvMaster.DataBind();
    }

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
    static public string getStoreInfo(string StoreNO)
    {
        string strInfo = "";
        if (!string.IsNullOrEmpty(StoreNO))
        {
            DataTable dt = new Store_Facade().Query_StoreInfo(StoreNO);
            if (dt.Rows.Count > 0)
            {
                strInfo = StringUtil.CStr(dt.Rows[0]["STORENAME"]);
            }
        }

        return strInfo;
    }

}
