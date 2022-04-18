using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Advtek.Utility;
using FET.POS.Model.Common;
using FET.POS.Model.DTO;
using FET.POS.Model.Facade;
using FET.POS.Model.Facade.FacadeImpl;
using DevExpress.Data;
using System.Data;

public partial class VSS_RPT_RPL057 : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack && !Page.IsCallback)
        {
            btnReset_Click(null, null);
            GetCboProductCategory();
        }
    }
    /// <summary>
    /// 取得商品類別下拉選單datasource
    /// </summary>
    protected void GetCboProductCategory()
    {
        cboProductCategory.TextField = "CATE_NAME";
        cboProductCategory.ValueField = "CATE_NO";
        cboProductCategory.DataSource = new RPL_PageHelper().GetProductCategory();
        cboProductCategory.DataBind();
        cboProductCategory.SelectedIndex = 0;
        
    }
    private void BindMasterData()
    {
        this.gvMaster.DataSource = new RPL_Facade().RPL057(this.cboProductCategory.Text, this.ASPxTextBox2.Text, this.ASPxTextBox1.Text,
                                                           this.txtOrdDateStart.Text, this.txtOrdDateEnd.Text, StringUtil.CStr(logMsg.STORENO));
        this.gvMaster.DataBind();

        //if (StringUtil.CStr(logMsg.STORENO) != System.Web.Configuration.WebConfigurationManager.AppSettings["DefaultRoleHQ"])
        //{
        //    this.txtStoreNo.Enabled = false; this.txtStoreNo.Text = StringUtil.CStr(logMsg.STORENO);
        //}
    }

    private void BindDropDownList()
    {

    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        BindMasterData();
        gvMaster.PageIndex = 0;
    }

    protected void btnExport_Click(object sender, EventArgs e)
    {
        BindMasterData();
        new Output().ExportXLS(800, "", Resources.WebResources.RPL057, dtHeader(), this.ASPxGridViewExporter1, null, null);
        Response.Redirect(Utils.CreateTamperProofDownloadXlsURL("RPL057.xls"));
    }

    private DataTable dtHeader()
    {
        DataTable dtHeader = new DataTable();
        dtHeader.Columns.Add("Text", typeof(string));
        dtHeader.Columns.Add("Align", typeof(string));
        dtHeader.Columns.Add("FontSize", typeof(string));
        dtHeader.Columns.Add("BackColor", typeof(string));

        DataRow NewRow = dtHeader.NewRow();
        NewRow["Text"] = "商品類別：" + cboProductCategory.Text
            + "|商品編號：" + this.ASPxTextBox2.Text
            + "|商品名稱：" + this.ASPxTextBox1.Text
            + "|交易日期：" + this.txtOrdDateStart.Text + "～" + this.txtOrdDateEnd.Text;
        NewRow["Align"] = "LEFT";
        NewRow["FontSize"] = "11";
        NewRow["BackColor"] = "WHITE";
        dtHeader.Rows.Add(NewRow);

        NewRow = dtHeader.NewRow();
        NewRow["Text"] = "列印日期：" + DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss")
            + "|列印人員：" + logMsg.MODI_USER + " " + new Employee_Facade().GetEmpName(logMsg.MODI_USER)
            + "|頁　　次：" + "1"
            + "|總筆數：" + (this.gvMaster.VisibleRowCount - 1); // 減掉一筆合計資料
        NewRow["Align"] = "LEFT";
        NewRow["FontSize"] = "11";
        NewRow["BackColor"] = "WHITE";
        dtHeader.Rows.Add(NewRow);

        return dtHeader;

    }

    protected void gvMaster_PageIndexChanged(object sender, EventArgs e)
    {
        BindMasterData();
    }

    protected void StoreNo_Changed(object sender, EventArgs e)
    {
        BindDropDownList();
    }

    protected void btnReset_Click(object sender, EventArgs e)
    {

        //if (logMsg.ROLE_TYPE != System.Web.Configuration.WebConfigurationManager.AppSettings["DefaultRoleHQ"])
        //{
        //    ScriptManager.RegisterClientScriptBlock(this, GetType(), "AlertMessage", "alert('非總部人員無法使用此功能!!');", true);

        //    cboProductCategory.Enabled = false;
        //    ASPxTextBox2.Enabled = false;
        //    ASPxTextBox1.Enabled = false;
        //    txtOrdDateStart.Enabled = false;
        //    txtOrdDateEnd.Enabled = false;

        //    btnSearch.Enabled = false;
        //    btnReset.Enabled = false;
        //    btnExport.Enabled = false;
        //    return;
        //}

        //查詢條件清空，下拉選單回復預設值

        cboProductCategory.Text = null;
        ASPxTextBox2.Text = null;
        ASPxTextBox1.Text = null;
        txtOrdDateStart.Text = null;
        txtOrdDateEnd.Text = null;


        gvMaster.DataSource = null;
        gvMaster.DataBind();

        this.txtOrdDateStart.Text = DateTime.Now.ToString("yyyy/MM/dd");
        this.txtOrdDateEnd.Text = DateTime.Now.ToString("yyyy/MM/dd");

        //if (StringUtil.CStr(logMsg.STORENO) != System.Web.Configuration.WebConfigurationManager.AppSettings["DefaultRoleHQ"])
        //{
        //    this.txtStoreNo.Enabled = false; this.txtStoreNo.Text = StringUtil.CStr(logMsg.STORENO);
        //}

        BindDropDownList();
    }
}
