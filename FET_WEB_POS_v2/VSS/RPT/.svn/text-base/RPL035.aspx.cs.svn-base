using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

using Advtek.Utility;
using FET.POS.Model.Common;
using FET.POS.Model.DTO;
using FET.POS.Model.Facade;
using FET.POS.Model.Facade.FacadeImpl;
using DevExpress.Data;
using System.Runtime.Serialization;


public partial class VSS_RPT_RPL035 : BasePage
{
    #region 宗佑
    /*Author：宗佑
      Date：100.2.18
      Description：新增限定總部使用權限
    */
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack && !Page.IsCallback)
        {
            //BindDropDownList();
            //BindMasterData();
            btnReset_Click(null, null);
        }
    }
    #endregion
    private void BindMasterData()
    {
        this.gvMaster.DataSource = new RPL_Facade().RPL035(this.cbPromotionCode.Text
                                                          , this.cbProductNo.Text
                                                          , this.txtTranDateStart.Text
                                                          , this.txtTranDateEnd.Text);
        this.gvMaster.DataBind();
    }

    /*private void BindDropDownList()
    {
        this.cbPromotionCode.DataSource = RPL_PageHelper.GetPROMO_NO();
        cbPromotionCode.TextField = "PROMO_NAME";
        cbPromotionCode.ValueField = "PROMO_NO";
        this.cbPromotionCode.DataBind();
        this.cbPromotionCode.SelectedIndex = 0;

        this.cbProductNo.DataSource = RPL_PageHelper.GetPROD_NO();
        cbProductNo.TextField = "PROD_NAME";
        cbProductNo.ValueField = "PROD_NO";
        this.cbProductNo.DataBind();
        this.cbProductNo.SelectedIndex = 0;

    }*/

    #region 宗佑
    /*Author：宗佑
      Date：100.02.18
      Description：新增清除按鈕
    */
    protected void btnReset_Click(object sender, EventArgs e)
    {
        if (logMsg.ROLE_TYPE != System.Web.Configuration.WebConfigurationManager.AppSettings["DefaultRoleHQ"])
        {
            ScriptManager.RegisterClientScriptBlock(this, GetType(), "AlertMessage", "alert('非總部人員無法使用此功能!!');", true);

            cbPromotionCode.Enabled = false;
            cbProductNo.Enabled = false;
            txtTranDateStart.Enabled = false;
            txtTranDateEnd.Enabled = false;


            btnSearch.Enabled = false;
            btnReset.Enabled = false;
            btnExport.Enabled = false;
            return;
        }

        //查詢條件清空，下拉選單回復預設值
        this.cbProductNo.Text = null;
        this.cbPromotionCode.Text = null;
        this.txtTranDateStart.Text = "";
        this.txtTranDateEnd.Text = "";

        gvMaster.DataSource = null;
        gvMaster.DataBind();
    }
    #endregion

    protected void btnSearch_Click(object sender, EventArgs e)
    {

        if (this.cbPromotionCode.Text.Trim() == "" && cbProductNo.Text.Trim() == "")
        {
            ScriptManager.RegisterClientScriptBlock(this, GetType(), "AlertMessage", "alert('[促銷代碼]及[商品料號]請至少輸入一項!');", true);
        }
        else
        {
            BindMasterData();
            gvMaster.PageIndex = 0;
        }
    }

    protected void btnExport_Click(object sender, EventArgs e)
    {
        BindMasterData();
        new Output().ExportXLS(800, "", Resources.WebResources.RPL035, dtHeader(), this.ASPxGridViewExporter1, null, null);
        Response.Redirect(Utils.CreateTamperProofDownloadXlsURL("RPL035.xls"));
    }

    private DataTable dtHeader()
    {
        DataTable dtHeader = new DataTable();
        dtHeader.Columns.Add("Text", typeof(string));
        dtHeader.Columns.Add("Align", typeof(string));
        dtHeader.Columns.Add("FontSize", typeof(string));
        dtHeader.Columns.Add("BackColor", typeof(string));

        DataRow NewRow = dtHeader.NewRow();
        NewRow["Text"] = "促銷代號：" + this.cbPromotionCode.Text
            + "|商品料號：" + this.cbProductNo.Text
            + "|交易日期：" + this.txtTranDateStart.Text + "～" + this.txtTranDateEnd.Text;
        NewRow["Align"] = "LEFT";
        NewRow["FontSize"] = "11";
        NewRow["BackColor"] = "WHITE";
        dtHeader.Rows.Add(NewRow);

        DataRow NewRow2 = dtHeader.NewRow();
        NewRow2["Text"] =
            ""
            + "列印日期：" + DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss")
            + "|列印人員：" + logMsg.MODI_USER + " " + new Employee_Facade().GetEmpName(logMsg.MODI_USER)
            + "|頁　　次：" + "1"
            + "|總筆數：" + this.gvMaster.VisibleRowCount;
        NewRow2["Align"] = "LEFT";
        NewRow2["FontSize"] = "11";
        NewRow2["BackColor"] = "WHITE";
        dtHeader.Rows.Add(NewRow2);

        return dtHeader;

    }

    protected void gvMaster_PageIndexChanged(object sender, EventArgs e)
    {
        BindMasterData();
    }

}
