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

using FET.POS.Model.DTO;
using FET.POS.Model.Common;
using FET.POS.Model.Facade.FacadeImpl;

using System.Collections.Specialized;

public partial class VSS_ORD_ORD02_ORD02_1 : BasePage
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
            string encryptUrl = "";

            //**2011/04/22 Tina：前頁傳遞參數時，會先以加密處理，在此要解密。
            if (!string.IsNullOrEmpty(Request.QueryString["Param"]))
            {
                NameValueCollection qscoll = Utils.Param_UrlDecode(Request.QueryString["Param"]);
                foreach (string key in qscoll.AllKeys)
                {
                    if (key == "ordid")
                    {
                        encryptUrl = string.Join(",", qscoll.GetValues(key));
                        break;
                    }
                }
            }

            return encryptUrl;

            //return (Request.QueryString["ordid"] ?? "");
        }
    }

    /// <summary>
    /// 訂單主檔TYPE
    /// </summary>
    private string qOrderType
    {
        get
        {
            string encryptUrl = "";

            //**2011/04/22 Tina：前頁傳遞參數時，會先以加密處理，在此要解密。
            if (!string.IsNullOrEmpty(Request.QueryString["Param"]))
            {
                NameValueCollection qscoll = Utils.Param_UrlDecode(Request.QueryString["Param"]);
                foreach (string key in qscoll.AllKeys)
                {
                    if (key == "order_type")
                    {
                        encryptUrl = string.Join(",", qscoll.GetValues(key));
                        break;
                    }
                }
            }

            return encryptUrl;

            //return (Request.QueryString["order_type"] ?? "");
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
        if (!IsPostBack)
        {
            ORD02_Facade _facade = new ORD02_Facade();

            divContent.Style["display"] = "";

           
            //繫結主檔
            DataTable dt = new DataTable();
            DataRow item = null;

            if (!IsAddMode)  //由查詢頁面過來
            {
                ORDER_TEMP_ID = qOrderID;
                if (qOrderType == "1")
                {
                    ORDER_TEMP_ID = _facade.GetOrderId(qOrderID, logMsg.STORENO);
                    dt = _facade.GetOrderMTempBy(ORDER_TEMP_ID);
                   if (dt.Rows.Count > 0) item = dt.Rows[0];
                }
                else
                {
                    dt = _facade.GetOrderMTempBy1(ORDER_TEMP_ID);
                    if (dt.Rows.Count > 0) item = dt.Rows[0];
                 
                    //btnQueryEdit.Visible = false;
                }
            }
          
            if (dt.Rows.Count >0)
            {
                lblOrderNo.Text = StringUtil.CStr(item["ORDER_NO"]);
                string tmpDate = StringUtil.CStr(item["ORDDATE"]);
                lblOrderDate.Text = tmpDate.Substring(0, 4) + "/" + tmpDate.Substring(4, 2) + "/" + tmpDate.Substring(6, 2);
                txtMemo.Text = StringUtil.CStr(item["REMARK"]);
                if (StringUtil.CStr(item["MODI_USER"]) != "")
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

              
            }
        }
    }

    private void bindgvMaster()
    {
        DataTable dt = new DataTable();
     
            if (qOrderType == "1")
            {
                dt = new ORD02_Facade().GetOrderMDTempBy(ORDER_TEMP_ID, logMsg.STORENO);
            }
            else
            {
                dt = new ORD02_Facade().GetOrderMDTempBy1(ORDER_TEMP_ID, logMsg.STORENO);
            }

            drMasterDV.DataSource = dt;
     
        Session["gvMaster"] = dt;
        drMasterDV.DataBind();
    }

   

    #region gvMaster 觸發事件

    protected void drMasterDV_PageIndexChanged(object sender, EventArgs e)
    {
        drMasterDV.DetailRows.CollapseAllRows();

        ((ASPxGridView)sender).DataSource = Session["gvMaster"] as DataTable;
        ((ASPxGridView)sender).DataBind();
    }

   

    protected void drMasterDV_HtmlRowCreated(object sender, ASPxGridViewTableRowEventArgs e)
    {
        if (e.RowType == GridViewRowType.Data)
        {
            //搭贈按鈕
            ORD02_Facade _facade = new ORD02_Facade();
            DataTable dt = _facade.GetGiftProducts(StringUtil.CStr(e.GetValue("PRODNO")), this.lblOrderDate.Text, "","","");
            if (dt.Rows.Count == 0)
            {
                ASPxButton btnOnetoone = e.Row.FindChildControl<ASPxButton>("btnOnetoone");
                btnOnetoone.Enabled = false;
            }
         
           
        }
        else if (e.RowType == GridViewRowType.Detail)
        {
            ORD02_Facade _facade = new ORD02_Facade();
            string detail_ORDQTY = string.Empty;
            string detail_APPROVEQTY = string.Empty;
            string detail_CHECK_IN_QTY = string.Empty;
            // 繫結明細資料表
            ASPxGridView detailGrid = e.Row.FindChildControl<ASPxGridView>("gvDetailDV");

            if (Session["gvMaster"] != null)
            {
                DataTable dt1 = Session["gvMaster"] as DataTable;

                DataRow[] drs = dt1.Select("ITEMNO='" + StringUtil.CStr(e.GetValue("ITEMNO")) + "'");
                if (drs.Length > 0)
                {
                    detail_ORDQTY = StringUtil.CStr(drs[0]["ORDQTY"]);
                    detail_APPROVEQTY = StringUtil.CStr(drs[0]["APPROVEQTY"]);
                    detail_CHECK_IN_QTY = StringUtil.CStr(drs[0]["CHECK_IN_QTY"]);
                }
                else
                {
                    detail_ORDQTY = "0";
                    detail_APPROVEQTY = "0";
                    detail_CHECK_IN_QTY ="0";

                }
            }
           // ((ASPxTextBox)drMasterDV.FindRowCellTemplateControl(e.VisibleIndex, (GridViewDataColumn)drMasterDV.Columns["ORDQTY"], "txtOrdqty")).Text = detail_BASE_QTY;
            DataTable dt = _facade.GetGiftProducts(StringUtil.CStr(e.GetValue("PRODNO")), this.lblOrderDate.Text, detail_ORDQTY, detail_APPROVEQTY, detail_CHECK_IN_QTY);

            detailGrid.DataSource = dt;
            detailGrid.DataBind();
        }

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



    
}
