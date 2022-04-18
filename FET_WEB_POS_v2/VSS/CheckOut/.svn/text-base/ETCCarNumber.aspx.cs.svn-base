using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using FET.POS.Model.Facade.FacadeImpl;
using Advtek.Utility;

public partial class VSS_CheckOut_ETCCarNumber : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        GetETCProdNo();
        GetETCProdPrice();
    }

    private void GetETCProdNo()
    {
        string strETC_CARD = "";
        string strETC_CARD_BAIL = "";
        // 易通卡料號
        DataTable dtETC_CARD = new SAL01_Facade().getParaValue("ETC_CARD");
        if (dtETC_CARD != null && dtETC_CARD.Rows.Count > 0)
        {
            foreach (DataRow dr in dtETC_CARD.Rows)
            {
                if (dr[0] != null && StringUtil.CStr(dr[0]) != "")
                    strETC_CARD = StringUtil.CStr(dr[0]);
            }
        }
        // 易通卡保證金料號
        DataTable dtETC_CARD_BAIL = new SAL01_Facade().getParaValue("ETC_CARD_BAIL");
        if (dtETC_CARD_BAIL != null && dtETC_CARD_BAIL.Rows.Count > 0)
        {
            foreach (DataRow dr in dtETC_CARD_BAIL.Rows)
            {
                if (dr[0] != null && StringUtil.CStr(dr[0]) != "")
                    strETC_CARD_BAIL = StringUtil.CStr(dr[0]);
            }
        }
        hdETC_CARDProdno.Text = strETC_CARD;
        hdETC_CARD_BAILProdno.Text = strETC_CARD_BAIL;
    }

    private void GetETCProdPrice()
    {
        string strETC_CARD_PRICE = "";
        string strETC_CARD_BAIL_PRICE = "";
        // 易通卡料號單價
        DataTable dtETC_CARD_PRICE = new SAL01_Facade().getProduct(hdETC_CARDProdno.Text);
        if (dtETC_CARD_PRICE != null && dtETC_CARD_PRICE.Rows.Count > 0)
        {
            foreach (DataRow dr in dtETC_CARD_PRICE.Rows)
            {
                if (dr["PRICE"] != null && StringUtil.CStr(dr["PRICE"]) != "")
                    strETC_CARD_PRICE = StringUtil.CStr(dr["PRICE"]);
            }
        }
        // 易通卡保證金料號單價
        DataTable dtETC_CARD_BAIL_PRICE = new SAL01_Facade().getProduct(hdETC_CARD_BAILProdno.Text);
        if (dtETC_CARD_BAIL_PRICE != null && dtETC_CARD_BAIL_PRICE.Rows.Count > 0)
        {
            foreach (DataRow dr in dtETC_CARD_BAIL_PRICE.Rows)
            {
                if (dr["PRICE"] != null && StringUtil.CStr(dr["PRICE"]) != "")
                    strETC_CARD_BAIL_PRICE = StringUtil.CStr(dr["PRICE"]);
            }
        }

        hdETC_CARDPrice.Text = strETC_CARD_PRICE;
        hdETC_CARD_BAILPrice.Text = strETC_CARD_BAIL_PRICE;
    }
}
