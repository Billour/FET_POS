using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Resources;

using Advtek.Utility;
using DevExpress.Web;
using DevExpress.Web.ASPxGridView;
using DevExpress.Web.ASPxPager;
using DevExpress.Web.ASPxEditors;

using FET.POS.Model.DTO;
using FET.POS.Model.Common;
using FET.POS.Model.Facade.FacadeImpl;

using System.Collections.Specialized;

public partial class VSS_ORD_ORD02_ORD02_2 : BasePage
{


    /// <summary>
    /// 暫存的預訂單明細資料
    /// </summary>
    private DataTable PreOrderDetailDt
    {
        get
        {
            if (Session[SessionID] == null)
            {
                DataTable dt = new DataTable();

                dt.Columns.Add("PRE_ORDER_SEQNO");
                dt.Columns.Add("ProductNo");
                dt.Columns.Add("ProductName");
                dt.Columns.Add("PreOrderQty");
                dt.Columns.Add("PurchQTY");
                dt.Columns.Add("REAL_ORDER_QTY");
                dt.Columns.Add("FAIL_REASON");
                dt.Columns.Add("OneToOneSID");
                //dt.Columns.Add("GiftProducts", typeof(DataTable));

                DataColumn[] PrimaryCol = new DataColumn[1];

                PrimaryCol[0] = dt.Columns["PRE_ORDER_SEQNO"];

                dt.PrimaryKey = PrimaryCol;

                Session[SessionID] = dt;
            }

            return (DataTable)Session[SessionID];
        }

        set
        {
            Session[SessionID] = value;
        }
    }

    /// <summary>
    /// 預訂單主檔ID
    /// </summary>
    private string qPreOrderID
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
                    if (key == "preordid")
                    {
                        encryptUrl = string.Join(",", qscoll.GetValues(key));
                        break;
                    }
                }
            }

            return encryptUrl;

            //return (Request.QueryString["preordid"] ?? "");
        }
    }

    /// <summary>
    /// 是否為新增模式
    /// </summary>
    private bool IsAddMode
    {
        get
        {
            return (qPreOrderID == "");
        }
    }

    private string SessionID
    {
        get
        {
            return (string)Session["SessionID"];
        }
        set
        {
            Session["SessionID"] = value;
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Session["ProductNo"] = null;
            Session["PreOrderQty"] = null;

            Session["gvMasterDV"] = null;
            Session["PreOrderMID"] = null;
            Session["STATUS"] = null;
            string UserName = logMsg.CREATE_USER;
            lblUpdateUser.Text = "";// logMsg.CREATE_USER;
            string StoreNo = logMsg.STORENO;
            txtSTORE_NO.Text = logMsg.STORENO;



            //載入預訂單
            SessionID = GuidNo.getUUID();

            DataTable MasterDt = new ORD02_Facade().GetPreOrderM(qPreOrderID);
            DataTable DetailDt = new ORD02_Facade().GetPreOrderD(qPreOrderID, txtSTORE_NO.Text);
            Session["STATUS"] = StringUtil.CStr(MasterDt.Rows[0]["STATUS"]);
            txtSTORE_NO.Text = StringUtil.CStr(MasterDt.Rows[0]["STORE_NO"]);
            //StoreNo = txtSTORE_NO.Text;
            //主檔載入至UI
            DataRow MasterDr = MasterDt.Rows[0];
            lblOrderNo.Text = StringUtil.CStr(MasterDr["PRE_ORDER_NO"]);
            txtMemo.Text = StringUtil.CStr(MasterDr["REMARK"]);

            lblUpdateUser.Text = StringUtil.CStr(MasterDr["MODI_USER"]) + " " + new Employee_Facade().GetEmpName(StringUtil.CStr(MasterDr["MODI_USER"]));
            lblUpdDateTime.Text = StringUtil.CStr(MasterDr["MODI_DTM"]);
            lblOrderDate.Text = StringUtil.CStr(MasterDr["ORDDATE"]).Substring(0, 4) + "/" + StringUtil.CStr(MasterDr["ORDDATE"]).Substring(4, 2) + "/" + StringUtil.CStr(MasterDr["ORDDATE"]).Substring(6, 2);
            int i = 1;

            foreach (DataRow dr in DetailDt.Rows)
            {
                DataRow DRNew = PreOrderDetailDt.NewRow();
                DRNew["PRE_ORDER_SEQNO"] = i++;
                DRNew["ProductNo"] = dr["PRODNO"];
                DRNew["ProductName"] = dr["PRODNAME"];
                DRNew["PreOrderQty"] = dr["ORDQTY"];
                DRNew["REAL_ORDER_QTY"] = dr["REAL_ORDER_QTY"];
                DRNew["FAIL_REASON"] = dr["FAIL_REASON"];

                PreOrderDetailDt.Rows.Add(DRNew);
            }
            Session["gvMasterDV"] = PreOrderDetailDt;
        }

        bindgvMaster();


    }

    protected void bindgvMaster()
    {
        gvMasterDV.DataSource = PreOrderDetailDt;
        gvMasterDV.DataBind();
        gvMasterDV.Selection.UnselectAll();
    }


    #region gvMasterDV 觸發的事件

    protected void gvMasterDV_HtmlRowCreated(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewTableRowEventArgs e)
    {
        if (e.RowType == GridViewRowType.Data)
        {
            string OneToOneSID = ORD02_Facade.GetOneToOneSID(StringUtil.CStr(e.GetValue("ProductNo")));
            if (OneToOneSID == "")
            {
                ASPxButton btnOnetoone = e.Row.FindChildControl<ASPxButton>("btnOnetoone");
                btnOnetoone.Enabled = false;
            }
            else
            {
                ASPxButton btnOnetoone = e.Row.FindChildControl<ASPxButton>("btnOnetoone");
                btnOnetoone.Enabled = true;
            }
        }
        else if (e.RowType == GridViewRowType.Detail)
        {

            string detail_ORDQTY = string.Empty;
            string detail_REAL_ORDER_QTY = string.Empty;
            string detail_FAIL_REASON = string.Empty;
            // 繫結明細資料表
            ASPxGridView detailGrid = e.Row.FindChildControl<ASPxGridView>("gvDetailDV");

            if (Session["gvMasterDV"] != null)
            {
                DataTable dt1 = Session["gvMasterDV"] as DataTable;

                DataRow[] drs = dt1.Select("PRE_ORDER_SEQNO='" + StringUtil.CStr(e.GetValue("PRE_ORDER_SEQNO")) + "'");
                if (drs.Length > 0)
                {
                    detail_ORDQTY = StringUtil.CStr(drs[0]["PreOrderQty"]);
                    detail_REAL_ORDER_QTY = StringUtil.CStr(drs[0]["REAL_ORDER_QTY"]);
                    detail_FAIL_REASON = StringUtil.CStr(drs[0]["FAIL_REASON"]);
                }
                else
                {
                    detail_ORDQTY = "0";
                    detail_REAL_ORDER_QTY = "0";
                    detail_FAIL_REASON = "";

                }
            }
            // ((ASPxTextBox)drMasterDV.FindRowCellTemplateControl(e.VisibleIndex, (GridViewDataColumn)drMasterDV.Columns["ORDQTY"], "txtOrdqty")).Text = detail_BASE_QTY;
            ORD02_Facade _facade = new ORD02_Facade();

            DataTable dt = _facade.PreGetGiftProducts(StringUtil.CStr(e.GetValue("ProductNo")), detail_ORDQTY, detail_REAL_ORDER_QTY, detail_FAIL_REASON);

            detailGrid.DataSource = dt;
            detailGrid.DataBind();
        }
    }




    protected void gvMasterDV_RowCommand(object sender, ASPxGridViewRowCommandEventArgs e)
    {
        if (gvMasterDV.DetailRows.IsVisible(e.VisibleIndex))
        {
            gvMasterDV.DetailRows.CollapseAllRows();
        }
        else
        {
            gvMasterDV.DetailRows.CollapseAllRows();
            gvMasterDV.DetailRows.ExpandRow(e.VisibleIndex);
        }
    }



    protected void grid_PageIndexChanged(object sender, EventArgs e)
    {
        gvMasterDV.CancelEdit();
        gvMasterDV.DetailRows.CollapseAllRows();
        gvMasterDV.Selection.UnselectAll();
        ASPxGridView grid = sender as ASPxGridView;
        grid.DataSource = Session["gvMasterDV"];
        grid.DataBind();
    }

    #endregion



}
