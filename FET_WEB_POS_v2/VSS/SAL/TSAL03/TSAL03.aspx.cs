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
using System.Collections.Specialized;

public partial class VSS_SAL_TSAL03_TSAL03 : BasePage
{
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


	protected void Page_Load(object sender, EventArgs e)
	{
		if (!IsPostBack)
		{
			//讀取POSUUID_MASTER
			hfPOSUUID_MASTER.Value = GuidNo.getUUID();
            if (!string.IsNullOrEmpty(PKEY))
			{
                hfOriginal_MASTER.Value = PKEY;
                if (SRC_TYPE.ToUpper() == "TSAL031" || SRC_TYPE.ToUpper() == "TSAL02")
					hfGetOrigItems.Value = "5";
				else
					hfGetOrigItems.Value = "6";
			}
			labTitle.Text += string.Format("<span style=\"font-family:Arial;font-size:8pt;\"> - OM:{0} / NM:{1} / D:{2} ({3})</span>", hfOriginal_MASTER.Value, hfPOSUUID_MASTER.Value, hfPOSUUID_DETAIL.Value , hfGetOrigItems.Value);
			labTRADE_DATE.InnerText = DateTime.Now.ToString("yyyy/MM/dd");
			labMODI_DTM.Text = DateTime.Now.ToString("yyyy/MM/dd");
			labMODI_USER.Text = logMsg.OPERATOR;
		}
		RegisterScript();

	}

	protected void btnSearch_Click(object sender, EventArgs e)
	{
		Response.Redirect("~/VSS/SAL/TSAL03/TSAL03.aspx");
	}

	private void RegisterScript()
	{
		StringBuilder script = new StringBuilder();
		script.AppendFormat("var baseUrl = '{0}';\n", Request.ApplicationPath);
		script.AppendFormat("var isCheckInvSetting = {0};\n", TSAL01_Facade.chkINVSetting(logMsg.STORENO).ToString().ToLower());
		script.AppendFormat("var imgLoadingUrl = '{0}';\n", this.ResolveClientUrl("~/Images/Loading.gif"));
		script.AppendFormat("var stockID = '{0}';\n", Common_PageHelper.GetGoodLOCUUID());
		script.AppendFormat("var storeNo = '{0}';\n", logMsg.STORENO);
		script.AppendFormat("var EmpID = '{0}';\n", logMsg.OPERATOR);
		script.AppendFormat("var creditDivLimitAmount = {0};\n", TSAL01_Facade.getCreditDivLimitAmount());
		script.AppendFormat("var StoreDiscountProd = {{ PRODNO:'{0}', PRODNAME:'{1}' }};\n", TSAL01_Facade.getStoreDiscountProdNoAndName().Split(','));

		script.AppendFormat("var txtUniNo = $('#{0}');\n", txtUNI_NO.ClientID);
		script.AppendFormat("var hfOriginal_MASTER = $('#{0}');\n", hfOriginal_MASTER.ClientID);
		script.AppendFormat("var hfPOSUUID_MASTER = $('#{0}');\n", hfPOSUUID_MASTER.ClientID);
		script.AppendFormat("var hfPOSUUID_DETAIL = $('#{0}');\n", hfPOSUUID_DETAIL.ClientID);
		script.AppendFormat("var hfGetOrigItems = $('#{0}');\n", hfGetOrigItems.ClientID);

		ClientScript.RegisterStartupScript(GetType(), GetType().ToString() + "_RegisterScript", script.ToString(), true);
	}

	private string GetExcludedProdNo()
	{
		StringBuilder sb = new StringBuilder();
		// ETC 料號
		sb.AppendLine(TSAL01_Facade.getFETCProuductNo());

		return sb.Replace(Environment.NewLine, ";").ToString().TrimEnd(';');
	}

	#region 處理 Sql Statement
	private void GetSqlInsertItem(StringBuilder sbitem, string TableName, List<string> Columns, List<string> Values)
	{
		sbitem.AppendFormat("INSERT INTO {0} (", TableName);

		for (int i = 0, iCount = Columns.Count; i < iCount; i++)
		{
			if (i != 0) sbitem.Append(",");
			sbitem.AppendLine(Columns[i]);
		}
		sbitem.AppendLine(") VALUES(");
		for (int i = 0, iCount = Values.Count; i < iCount; i++)
		{
			if (i != 0) sbitem.Append(",");
			sbitem.AppendLine(Values[i]);
		}
		sbitem.AppendLine(")");
	}
	private void ExecuteSql(OracleTransaction TransPOS, StringBuilder SQLStatement, string LogMsg, string EmployeeId, string ProcessId)
	{
		//this.WriteLog("ProcessSSI", string.Format("Process: {0},{1}", ProcessId, LogMsg), "WEB SERVICE", EmployeeId, 0);
		int iImpactRowCount = OracleDBUtil.ExecuteSql(TransPOS, SQLStatement.ToString());
		//this.WriteLog("ProcessSSI", string.Format("ProcessEnd: {0},", ProcessId), "WEB SERVICE", EmployeeId, iImpactRowCount);
	}
	#endregion

}
