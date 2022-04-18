using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using DevExpress.Web;
using DevExpress.Web.ASPxGridView;
using DevExpress.Web.ASPxPager;
using DevExpress.Web.ASPxEditors;

using Advtek.Utility;
using FET.POS.Model.Common;
using FET.POS.Model.DTO;
using FET.POS.Model.Facade;
using FET.POS.Model.Facade.FacadeImpl;

public partial class VSS_CONS_CON03a : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {

        if (logMsg.ROLE_TYPE == System.Configuration.ConfigurationManager.AppSettings["DefaultRoleHQ"].ToString())
        {
            //彈跳視窗
            ScriptManager.RegisterClientScriptBlock(this, GetType(), "AlertMessage", "alert('非門市人員無法使用此功能!!');", true);
            PanelPage.Enabled = false;
            return;
        }
        if (!Page.IsPostBack)
        {
            //商品類別下拉
            bindDdlValTxt(ddlProductCategory, PRODUCT_PageHelper.GetCsmProDTypeNo(true), "PRODTYPE_NO", "PRODTYPE_NAME");
            aspComboBoxDefaultSetting();
            bindMasterData();
        }
        else
        {
            DataTable dtGvMaster = ViewState["gvMaster"] as DataTable;
            if (dtGvMaster != null)
            {
                gvMaster.DataSource = dtGvMaster;
                gvMaster.DataBind();
            }
        }

    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        bindMasterData();
    }

    protected void bindMasterData()
    {
        DataTable dtResult = new DataTable();
        dtResult = getMasterData();
        ViewState["gvMaster"] = dtResult;
        gvMaster.DataSource = dtResult;
        gvMaster.DataBind();
    }
    private DataTable getMasterData()
    {
        DataTable dtResult = new DataTable();
        dtResult = new CON04_Facade().Query_CsmSupplierGrid(txtSupplierNo.Text, ConsignmentVendorsPopup1.Text,
            ddlProductCategory.SelectedItem.Value.ToString(), ProductsPopup1.Text, ProductsPopup2.Text,
            dateFrom.Text, dateTo.Text, SupportStartDateFrom.Text,SupportStartDateTo.Text,SupportExpiryDateFrom.Text,
            SupportExpiryDateTo.Text);
        return dtResult;
    }
   protected void bindDdlValTxt(ASPxComboBox AspCB, object dataSrc, string valCol, string txtCol)
    {
        AspCB.DataSource = dataSrc;
        AspCB.ValueField = valCol;
        AspCB.TextField = txtCol;
        AspCB.DataBind();
    }
    /// <summary>
    /// 下拉選單初始值
    /// </summary>
   protected void aspComboBoxDefaultSetting()
   {
       //若都沒選,將Index設為0
       //判斷下拉式選單預設值-999,並替換為空字串""
       if (ddlProductCategory.SelectedIndex == -1)
       {
           ddlProductCategory.SelectedIndex = 0;
           ddlProductCategory.SelectedItem.Value = "";
           //設定值完後,SelectedItem會變Null,要再次指定SelectedIndex,超機車XD!
           ddlProductCategory.SelectedIndex = 0;
       }

   }

}
 