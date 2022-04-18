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
using System.Web.Configuration;

public partial class VSS_ORD_ORD02_ORD02 : BasePage
{

    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        if (txtProductNO.Text.Length > 0)
        {
            int intPointPos = txtProductNO.Text.IndexOf(".");
            if (intPointPos > 0)
            {
                ScriptManager.RegisterClientScriptBlock(this, typeof(string), "selectTranData", "alert('不可為非正整數或非數字!');", true);
                return;
            }
            if (!NumberUtil.IsNumeric(txtProductNO.Text))
            {
                ScriptManager.RegisterClientScriptBlock(this, typeof(string), "selectTranData", "alert('不可為非正整數或非數字!');", true);
                return;
            }

            if (int.Parse(txtProductNO.Text) < 0)
            {
                ScriptManager.RegisterClientScriptBlock(this, typeof(string), "selectTranData", "alert('不可為非正整數或非數字!');", true);
                return;
            }
        }

        ViewState.Remove("gvMaster");

        gvMasterDV.FocusedRowIndex = -1;
        gvMasterDV.PageIndex = 0;

        DataTable dtQuery;
        ORD02_Facade Facade = new ORD02_Facade();

        //總部人員可查詢各門市資料, 門市人員只能查詢自己的門市
        dtQuery = Facade.Query_StoreOrders(
            txtSDate.Text,
            txtEDate.Text,
            txtProductNO.Text,
            logMsg.STORENO,
            logMsg.ROLE_TYPE == WebConfigurationManager.AppSettings["DefaultRoleHQ"]
            );
        ViewState["gvMaster"] = dtQuery;

        bindMasterData();
    }

    private void bindMasterData()
    {
        DataTable dtQuery;
        dtQuery = ViewState["gvMaster"] as DataTable;
        if (dtQuery != null)
        {
            gvMasterDV.DataSource = dtQuery;
            gvMasterDV.DataBind();
        }
    }

    protected void gvMasterDV_PageIndexChanged(object sender, EventArgs e)
    {
        bindMasterData();
    }

    protected void btnXlsExport_Click(object sender, EventArgs e)
    {
        DataTable dtExport;
        ORD02_Facade Facade = new ORD02_Facade();
        //總部人員可查詢各門市資料, 門市人員只能查詢自己的門市
        dtExport = Facade.Export_StoreOrders(
            txtSDate.Text,
            txtEDate.Text,
            txtProductNO.Text,
            logMsg.STORENO,
            logMsg.ROLE_TYPE == WebConfigurationManager.AppSettings["DefaultRoleHQ"]
            );
        string filename = new Output().Print("XLS", "(預)訂單查詢作業<td></td><td>匯出時間：" + DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss") + "</td>", null, dtExport, null);
        Response.Redirect(Utils.CreateTamperProofDownloadXlsURL("OrderSearch.xls"));
    }

    public string TransferURL(string strValue)
    {
        //**2011/04/22 Tina：傳遞參數時，要先以加密處理。
        string encryptUrl = Utils.Param_Encrypt(strValue);
        return string.Format("Param={0}", encryptUrl);   
    }
}
