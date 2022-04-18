using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OracleClient;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Newtonsoft.Json.Linq;
using Advtek.Utility;
using FET.POS.Model.Common;
using FET.POS.Model.Facade.FacadeImpl;
using DevExpress.Web.ASPxPopupControl;
using System.Collections.Specialized;

public partial class VSS_SAL_TSAL11_TSAL11 : BasePage
{
    /// <summary>
    /// 來源類型
    /// </summary>
    private string SRC_TYPE
    {
        get
        {
            string encryptUrl = "";

            //**2011/04/21 Tina：前頁傳遞參數時，會先以加密處理，在此要解密。
            if (!string.IsNullOrEmpty(Request.QueryString["Param"]))
            {
                NameValueCollection qscoll = Utils.Param_UrlDecode(Request.QueryString["Param"]);
                foreach (string key in qscoll.AllKeys)
                {
                    if (key == "SRC_TYPE")
                    {
                        encryptUrl = string.Join(",", qscoll.GetValues(key));
                        break;
                    }
                }
            }

            return encryptUrl;
        }
    }

    /// <summary>
    /// KEY_UUID
    /// </summary>
    private string PKEY
    {
        get
        {
            string encryptUrl = "";

            //**2011/04/21 Tina：前頁傳遞參數時，會先以加密處理，在此要解密。
            if (!string.IsNullOrEmpty(Request.QueryString["Param"]))
            {
                NameValueCollection qscoll = Utils.Param_UrlDecode(Request.QueryString["Param"]);
                foreach (string key in qscoll.AllKeys)
                {
                    if (key == "PKEY")
                    {
                        encryptUrl = string.Join(",", qscoll.GetValues(key));
                        break;
                    }
                }
            }

            return encryptUrl;
        }
    }


    #region Protected Method : void Page_Load(object sender, EventArgs e)
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            string POSUUID_MASTER;
            if (string.IsNullOrEmpty(SRC_TYPE))
            {
                // 一般銷售
                POSUUID_MASTER = CheckData();
                if (string.IsNullOrEmpty(POSUUID_MASTER))
                {
                    //**2011/04/20 Tina：傳遞參數時，要先以加密處理。
                    string encryptUrl = Utils.Param_Encrypt("s=2");
                    this.Page.FindChildControl<ASPxPopupControl>("pcSAL11").ContentUrl = string.Format("~/VSS/SAL/SAL11/SAL11_Transaction.aspx?Param={0}", encryptUrl);

                    // 無快取
                    //this.Page.FindChildControl<ASPxPopupControl>("pcSAL11").ContentUrl = "~/VSS/SAL/SAL11/SAL11_Transaction.aspx?s=2";
                    this.Page.FindChildControl<ASPxPopupControl>("pcSAL11").ShowOnPageLoad = true; 
                    hfPOSUUID_MASTER.Value = GuidNo.getUUID();
                    hfActionType.Value = "0";
                }
                else
                {
                    // 快取
                    hfPOSUUID_MASTER.Value = POSUUID_MASTER;
                    hfPOSUUID_DETAIL.Value = get_posuuid_detail(POSUUID_MASTER);
                    hfActionType.Value = "1";
                }
                labTitle.Text += string.Format("<span style=\"font-family:Arial;font-size:9pt;\"> - M:{0} / D:{1} ({2})</span>", hfPOSUUID_MASTER.Value, hfPOSUUID_DETAIL.Value, hfActionType.Value);
            }
            else
            {
                // 由其他網頁帶進來

                switch (SRC_TYPE.ToUpper())
                {
                    case "TSAL05":
                        // 未結清單
                        hfPOSUUID_MASTER.Value = GuidNo.getUUID();
                        hfPOSUUID_DETAIL.Value = PKEY;
                        hfActionType.Value = "2";
                        hfSALE_TYPE.Value = TSAL01_Facade.get_sale_type(PKEY); // 取得 SALE_TYPE
                        break;
                    default:
                        break;
                }
                labTitle.Text += string.Format("<span style=\"font-family:Arial;font-size:9pt;\"> - M:{0} / D:{1} ({2})</span>", hfPOSUUID_MASTER.Value, hfPOSUUID_DETAIL.Value, hfActionType.Value);
            }
            labTRADE_DATE.InnerText = DateTime.Now.ToString("yyyy/MM/dd");
            labMODI_DTM.Text = DateTime.Now.ToString("yyyy/MM/dd");
            labMODI_USER.Text = logMsg.OPERATOR;
            txtInv_Date.Text = DateTime.Now.ToString("yyyy/MM/dd");
            txtInv_Date.MaxDate = DateTime.Today;
        }
        RegisterScript();
    }
    #endregion

    #region Private Method : void RegisterScript()
    private void RegisterScript()
    {
        StringBuilder script = new StringBuilder();
        script.AppendFormat("var baseUrl = '{0}';\n", Request.ApplicationPath);
        script.AppendFormat("var isCheckInvSetting = {0};\n", TSAL01_Facade.chkINVSetting(logMsg.STORENO).ToString().ToLower());
        script.AppendFormat("var imgLoadingUrl = '{0}';\n", this.ResolveClientUrl("~/Images/Loading.gif"));
        script.AppendFormat("var stockID = '{0}';\n", Common_PageHelper.GetGoodLOCUUID());
        script.AppendFormat("var storeNo = '{0}';\n", logMsg.STORENO);
        script.AppendFormat("var EmpID = '{0}';\n", logMsg.OPERATOR);
        script.AppendFormat("var ETC_ProdNo = '{0}';\n", TSAL01_Facade.getFETCProuductNo());
        script.AppendFormat("var creditDivLimitAmount = {0};\n", TSAL01_Facade.getCreditDivLimitAmount());
        script.AppendFormat("var StoreDiscountProd = {{ PRODNO:'{0}', PRODNAME:'{1}' }};\n", TSAL01_Facade.getStoreDiscountProdNoAndName().Split(','));
        script.AppendFormat("var CommunicationsProdNo = {0};\n", GetCommunicationsProdNo());
        script.AppendFormat("var allowPayMode = [{0}];\n", string.Join(",", TSAL01_Facade.getAllowPayMode()));

        script.AppendFormat("var txtUniNo = $('#{0}');\n", txtUNI_NO.ClientID);
        script.AppendFormat("var hfPOSUUID_MASTER = $('#{0}');\n", hfPOSUUID_MASTER.ClientID);
        script.AppendFormat("var hfOriginal_MASTER = $('#{0}');\n", hfOriginal_MASTER.ClientID);
        script.AppendFormat("var hfPOSUUID_DETAIL = $('#{0}');\n", hfPOSUUID_DETAIL.ClientID);
        script.AppendFormat("var hfActionType = $('#{0}');\n", hfActionType.ClientID);
        script.AppendFormat("var hfSALE_TYPE = $('#{0}');\n", hfSALE_TYPE.ClientID);

        ClientScript.RegisterStartupScript(GetType(), GetType().ToString() + "_RegisterScript", script.ToString(), true);
    }
    #endregion

    #region Private Method : string get_posuuid_detail(string posuuid_master)
    private string get_posuuid_detail(string posuuid_master)
    {
        string result = "";
        OracleConnection conn = null;
        string sqlstr = "select posuuid_detail from sale_detail where posuuid_detail is not null and  posuuid_master= " + OracleDBUtil.SqlStr(posuuid_master);

        try
        {
            conn = OracleDBUtil.GetConnection();
            OracleCommand cmd = new OracleCommand(sqlstr, conn);
            OracleDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                result += dr.GetString(0) + ";";
            }
            dr.Close();
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            if (conn.State == ConnectionState.Open) conn.Close();
            OracleConnection.ClearAllPools();
        }

        return result;
    }
    #endregion

    #region Private Method : string CheckData()
    private string CheckData()
    {
        // 檢查是否有快取資料
        string result = "";
        string sqlstr = string.Format("select POSUUID_MASTER from SALE_HEAD where MODI_USER={0} and STORE_NO = {1} and SALE_STATUS = '8'", OracleDBUtil.SqlStr(this.logMsg.MODI_USER), OracleDBUtil.SqlStr(this.logMsg.STORENO));
        OracleConnection conn = null;
        try
        {
            conn = OracleDBUtil.GetConnection();
            OracleCommand cmd = new OracleCommand(sqlstr, conn);
            OracleDataReader dr = cmd.ExecuteReader();
            if (dr.Read())
                result = dr.GetString(0);
            dr.Close();
        }
        catch
        {
            result = "";
        }
        finally
        {
            if (conn.State == System.Data.ConnectionState.Open) conn.Close();
            OracleConnection.ClearAllPools();
        }
        return result;
    }
    #endregion

    #region Private Method : string GetCommunicationsProdNo()
    private string GetCommunicationsProdNo()
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
        return string.Format("{{ PRODNO:'{0}', BASIC_PRODNO:'{1}' }}", strCOMM_PRODNO, strCOMM_BASICPRODNO);
    }
    #endregion

    #region Button Action
    protected void btnUnClose_Click(object sender, EventArgs e)
    {
        //Response.Redirect("~/VSS/SAL/TSAL05/TSAL05.aspx?s=2");

        //**2011/04/21 Tina：傳遞參數時，要先以加密處理。
        string encryptUrl = Utils.Param_Encrypt("s=2");
        Response.Redirect(string.Format("~/VSS/SAL/TSAL05/TSAL05.aspx?Param={0}", encryptUrl));

    }
    #endregion
}
