using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Collections;
using DevExpress.Web.ASPxGridView;
using DevExpress.Web.ASPxPager;

using Advtek.Utility;
using FET.POS.Model.DTO;
using FET.POS.Model.Facade;
using System.Web.UI.HtmlControls;
using FET.POS.Model.Facade.FacadeImpl;
using FET.POS.Model.Common;

public partial class VSS_DIS_DIS08 : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //取得空的資料表
            gvMaster.DataSource = new DIS08_PROD_TRANS_VALUE_DTO().DataTable;
            gvMaster.DataBind();

            bindCategoryList();
        }
    }

    private void bindCategoryList()
    {
        DataTable dt = DIS08_PageHelper.GetCategoryList();
        cbbPs_Type.DataSource = dt;
        cbbPs_Type.TextField = "CATE_NAME";
        cbbPs_Type.ValueField = "CATE_NO";
        cbbPs_Type.DataBind();

        if (dt.Rows.Count > 0)
        {
            cbbPs_Type.SelectedIndex = 0;
        }
    }

    private void bindMasterData()
    {
        DataTable dtQuery = ViewState["gvMaster"] as DataTable;
        if (dtQuery == null)
        {
            string sSelectClassType = "";
            string sPs_Type = "";
            string sCategory = "";
            string sRangeS_BeginDate = "";
            string sRangeS_EndDate = "";
            string sRangeE_BeginDate = "";
            string sRangeE_EndDate = "";

            if (cbbSelectClassType.Value != null)
                sSelectClassType = StringUtil.CStr(cbbSelectClassType.Value);

            if (cbbPs_Type.Value != null && StringUtil.CStr(cbbPs_Type.Value) != "ALL")
                sPs_Type = StringUtil.CStr(cbbPs_Type.Value);

            if (txtPRODNO.Text != null)
                sCategory = txtPRODNO.Text;

            if (txtSDate_S.Text != null)
                sRangeS_BeginDate = txtSDate_S.Text;

            if (txtSDate_E.Text != null)
                sRangeS_EndDate = txtSDate_E.Text;

            if (txtEDate_S.Text != null)
                sRangeE_BeginDate = txtEDate_S.Text;

            if (txtEDate_E.Text != null)
                sRangeE_EndDate = txtEDate_E.Text;

            dtQuery = new DIS08_Facade().Query_PROD_TRANS_VALUE(sSelectClassType, sPs_Type, sCategory, sRangeS_BeginDate, sRangeS_EndDate, sRangeE_BeginDate, sRangeE_EndDate);
            ViewState["gvMaster"] = dtQuery;
        }
        gvMaster.DataSource = dtQuery;
        gvMaster.DataBind();
    }

    private bool checkInputData()
    {
        bool result = false;

        if ((txtSDate_S.Text.Trim() != "" && txtSDate_E.Text.Trim() != "") &&
            (DateTime.Parse(txtSDate_S.Text.Trim()) > DateTime.Parse(txtSDate_E.Text.Trim())))
        {
            ScriptManager.RegisterClientScriptBlock(this, typeof(string), "生效起日", "alert('[生效起日訖值]不允許小於[生效起日起值]，請重新輸入!');", true);
        }
        else if ((txtEDate_S.Text.Trim() != "" && txtEDate_E.Text.Trim() != "") &&
            (DateTime.Parse(txtEDate_S.Text.Trim()) > DateTime.Parse(txtEDate_E.Text.Trim())))
        {
            ScriptManager.RegisterClientScriptBlock(this, typeof(string), "生效訖日", "alert('[生效訖日起值]不允許大於[生效訖日訖值]，請重新輸入!');", true);
        }
        else
        {
            result = true;
        }

        return result;
    }

    protected void btnQuery_Click(object sender, EventArgs e)
    {
        if (checkInputData())
        {
            ViewState.Remove("gvMaster");
            gvMaster.PageIndex = 0;
            gvMaster.FocusedRowIndex = -1;
            bindMasterData();
        }
    }

    protected void gvMaster_PageIndexChanged(object sender, EventArgs e)
    {
        if (ViewState["gvMaster"] != null)
        {
            gvMaster.FocusedRowIndex = -1;
            bindMasterData();
        }   
    }
}
