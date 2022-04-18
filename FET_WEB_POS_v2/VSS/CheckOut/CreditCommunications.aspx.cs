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

public partial class VSS_CheckOut_CreditCommunications : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        GetCommunicationsProdNo();
        GetCommunicationsInfo();
        lblDays.Text = "0 天";
        lblTotalAmount.Text = "0 元";
    }

    private void GetCommunicationsInfo()
    {
        string strCOMM_BASIC_COSTS = "";
        string strCOMM_DAILY_COSTS = "";
        // 申請費
        DataTable dtCOMM_BASIC_COSTS = new SAL01_Facade().getProduct(hdBasicProdno.Text);
        if (dtCOMM_BASIC_COSTS != null && dtCOMM_BASIC_COSTS.Rows.Count > 0)
        {
            foreach (DataRow dr in dtCOMM_BASIC_COSTS.Rows)
            {
                if (dr["PRICE"] != null && StringUtil.CStr(dr["PRICE"]) != "")
                    strCOMM_BASIC_COSTS = StringUtil.CStr(dr["PRICE"]);
            }
        }
        // 查詢費
        DataTable dtCOMM_DAILY_COSTS = new SAL01_Facade().getProduct(hdProdno.Text);
        if (dtCOMM_DAILY_COSTS != null && dtCOMM_DAILY_COSTS.Rows.Count > 0)
        {
            foreach (DataRow dr in dtCOMM_DAILY_COSTS.Rows)
            {
                if (dr["PRICE"] != null && StringUtil.CStr(dr["PRICE"]) != "")
                    strCOMM_DAILY_COSTS = StringUtil.CStr(dr["PRICE"]);
            }
        }

        lblInfo.Text = "(查詢費:$" + strCOMM_DAILY_COSTS + "元/天;申請費:$" + strCOMM_BASIC_COSTS + "元/次)"; 
        hdDaily.Text = strCOMM_DAILY_COSTS;
        hdBasic.Text = strCOMM_BASIC_COSTS;
    }

    private void GetCommunicationsProdNo()
    {
        string strCOMM_PRODNO = "";
        string strCOMM_BASICPRODNO = "";
        // 查詢費料號
        DataTable dtCOMM_PRODNO = new SAL01_Facade().getParaValue("COMM_DAY");
        if (dtCOMM_PRODNO != null && dtCOMM_PRODNO.Rows.Count > 0)
        {
            foreach (DataRow dr in dtCOMM_PRODNO.Rows)
            {
                if (dr[0] != null && StringUtil.CStr(dr[0]) != "")
                    strCOMM_PRODNO = StringUtil.CStr(dr[0]);
            }
        }
        // 申請費料號
        DataTable dtCOMM_BASICPRODNO = new SAL01_Facade().getParaValue("COMM_SEQ");
        if (dtCOMM_BASICPRODNO != null && dtCOMM_BASICPRODNO.Rows.Count > 0)
        {
            foreach (DataRow dr in dtCOMM_BASICPRODNO.Rows)
            {
                if (dr[0] != null && StringUtil.CStr(dr[0]) != "")
                    strCOMM_BASICPRODNO = StringUtil.CStr(dr[0]);
            }
        }
        hdProdno.Text = strCOMM_PRODNO;
        hdBasicProdno.Text = strCOMM_BASICPRODNO;
    }
}
