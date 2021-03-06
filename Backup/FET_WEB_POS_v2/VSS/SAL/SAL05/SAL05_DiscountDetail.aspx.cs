using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using DevExpress.Web.ASPxGridView;
using FET.POS.Model.Facade.FacadeImpl;
using FET.POS.Model.DTO;
using DevExpress.Web.ASPxEditors;
using Advtek.Utility;
using System.Collections.Specialized;

public partial class VSS_SAL_SAL05_DiscountDetail : BasePage
{
    //主檔主KEY值   
    private string POSUUID_DETAIL
    {
        get {

            //**2011/04/27 Tina：前頁傳遞參數時，會先以加密處理，在此要解密。
            string strPOSUUID_DETAIL = "";
            if (!string.IsNullOrEmpty(Request.QueryString["Param"]))
            {
                NameValueCollection qscoll = Utils.Param_UrlDecode(Request.QueryString["Param"]);
                foreach (string key in qscoll.AllKeys)
                {
                    if (key == "POSUUID_DETAIL")
                    {
                        strPOSUUID_DETAIL = string.Join(",", qscoll.GetValues(key));
                        break;
                    }
                }
            }

            return strPOSUUID_DETAIL;

        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {            
            gvMaster.DataSource = new FET.POS.Model.Facade.FacadeImpl.SAL05_Facade().getTO_CLOSE_DISCOUNT(POSUUID_DETAIL == null ? "" : POSUUID_DETAIL);
            gvMaster.DataBind();
        }
    }
    protected void gvMaster_HtmlRowCreated(object sender, ASPxGridViewTableRowEventArgs e)
    {
        if (e.RowType == GridViewRowType.Detail)
        {
            // 繫結明細資料表           

            DataTable dt = new SAL05_Facade().getDiscountDetail(StringUtil.CStr(e.KeyValue));
            if (dt.Rows.Count > 0)
            {
                ASPxLabel lbVALUE = e.Row.FindChildControl<ASPxLabel>("lbVALUE");
                lbVALUE.Text = StringUtil.CStr(dt.Rows[0]["M_TYPE"]);

                ASPxLabel lbSTOREID_NAME = e.Row.FindChildControl<ASPxLabel>("lbSTOREID_NAME");
                lbSTOREID_NAME.Text = StringUtil.CStr(dt.Rows[0]["STORE_NO"]) + " " +
                                     StringUtil.CStr(dt.Rows[0]["STORENAME"]);

                ASPxLabel lbPRODNO_NAME = e.Row.FindChildControl<ASPxLabel>("lbPRODNO_NAME");
                lbPRODNO_NAME.Text = StringUtil.CStr(dt.Rows[0]["PRODNO"]) + " " +
                                     StringUtil.CStr(dt.Rows[0]["PRODNAME"]);

                ASPxLabel lbCUST = e.Row.FindChildControl<ASPxLabel>("lbCUST");
                lbPRODNO_NAME.Text = StringUtil.CStr(dt.Rows[0]["DIS_USE_TYPE"]) + " " +
                                     StringUtil.CStr(dt.Rows[0]["DIS_USE_TYPE_NAME"]);

                ASPxLabel lbPROMOID_NAME = e.Row.FindChildControl<ASPxLabel>("lbPROMOID_NAME");
                lbPROMOID_NAME.Text = StringUtil.CStr(dt.Rows[0]["PROMOTION_CODE"]) + "  " +
                      StringUtil.CStr(dt.Rows[0]["PROMO_NAME"]);
            }
          
        }
    }
}
