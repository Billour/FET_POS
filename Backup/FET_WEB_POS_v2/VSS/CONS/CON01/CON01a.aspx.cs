using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Advtek.Utility;
using DevExpress.Web;
using DevExpress.Web.ASPxGridView;
using DevExpress.Web.ASPxPager;
using FET.POS.Model.Facade.FacadeImpl;
using FET.POS.Model.Common;
using FET.POS.Model.DTO;
using System.Web.Configuration;

public partial class VSS_CONS_CON01a : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            if (logMsg.ROLE_TYPE == System.Configuration.ConfigurationManager.AppSettings["DefaultRoleHQ"].ToString())
            {
                //彈跳視窗
                ScriptManager.RegisterClientScriptBlock(this, GetType(), "AlertMessage", "alert('非門市人員無法使用此功能!!');", true);
                gvMaster.Enabled = false;
                DropType.Enabled = false;
                popSupplierNo.Enabled = false;
                popSupplierName.Enabled = false;
                txtCompanyId.Enabled = false;
                btnSearch.Enabled = false;
                btnClear.Enabled = false;
                return;
            }

            bindMasterData(false);
        }
        else
        {
            //DataTable dtGvMaster = ViewState["gvMaster"] as DataTable;
            //if (dtGvMaster != null)
            //{
            //    gvMaster.DataSource = dtGvMaster;
            //    gvMaster.DataBind();
            //}
        }
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        bindMasterData(false);         
    }

    protected void bindMasterData(bool bEmptyData)
    {
        DataTable dtResult = new DataTable();
        string strType = DropType.Value.ToString();
        if (strType == "select" || strType == "ALL")
        {
            strType = "";
        }
        if (!bEmptyData)
            dtResult = new CON02_Facade().GetSelectData(strType, popSupplierNo.Text, popSupplierName.Text, txtCompanyId.Text);
        else
            dtResult = new CON02_Facade().GetSelectData("dfsdfs", "", "", "");

        //ViewState["gvMaster"] = dtResult;
        gvMaster.DataSource = dtResult;
        gvMaster.DataBind();
    }

    protected void btnClear_Click(object sender, EventArgs e)
    {
        DropType.SelectedIndex = 0;
        popSupplierNo.Text = "";
        popSupplierName.Text = "";
        txtCompanyId.Text = "";

        bindMasterData(true);
    }

    //ajax 呼當前網頁的方式
    [System.Web.Services.WebMethod()]
    [System.Web.Script.Services.ScriptMethod()]
    static public string checkINVcode(string strINV)
    {
        string strInfo = "";
        Advtek.Utility.Check_ID subCheck = new Check_ID();
        if (!string.IsNullOrEmpty(strINV))
        {
            //如果不足8碼
            if (subCheck.Check_TW_INV(strINV) == 3)
            {
                strInfo = "false";
            }
        }
        
        return strInfo;
    }
    protected void gvMaster_PageIndexChanged(object sender, EventArgs e)
    {        
        gvMaster.FocusedRowIndex = -1;
        gvMaster.PageIndex = 0;
        bindMasterData(false);

    }
}
