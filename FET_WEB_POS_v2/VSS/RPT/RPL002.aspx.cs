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
using DevExpress.Web.ASPxGridView;
using System.Web.Configuration;

public partial class VSS_RPT_RPL002 : BasePage
{
    #region 宗佑
    /*Author：宗佑
  Date：100.02.21
  Description：RPL002門市對帳明細表 後端程式
    */
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack && !Page.IsCallback)
        {
            btnReset_Click(null, null);
        }
    }
    private void BindMasterData()
    {

        this.gvMaster.DataSource = new RPL_Facade().RPL002(this.txtSTORE_NO_S.Text, this.txtSTORE_NO_E.Text, this.txtOrdDateStart.Text, this.txtOrdDateEnd.Text,StringUtil.CStr( this.ASPxComboBox1.Value));


        this.gvMaster.DataBind();
        if (StringUtil.CStr(logMsg.STORENO) != System.Web.Configuration.WebConfigurationManager.AppSettings["DefaultRoleHQ"])
        {
            this.txtSTORE_NO_S.Enabled = false;
            this.txtSTORE_NO_E.Enabled = false;
            this.txtSTORE_NO_S.Text = StringUtil.CStr(logMsg.STORENO);
            this.txtSTORE_NO_E.Text = StringUtil.CStr(logMsg.STORENO);
        }

    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        BindMasterData();
        gvMaster.PageIndex = 0;
    }

    protected void btnExport_Click(object sender, EventArgs e)
    {
        BindMasterData();
        new Output().ExportXLS(800, "", "門市對帳明細表", dtHeader(), this.ASPxGridViewExporter1, null, null);
        Response.Redirect(Utils.CreateTamperProofDownloadXlsURL("RPL002.xls"));
    }

    private DataTable dtHeader()
    {
        DataTable dtHeader = new DataTable();
        dtHeader.Columns.Add("Text", typeof(string));
        dtHeader.Columns.Add("Align", typeof(string));
        dtHeader.Columns.Add("FontSize", typeof(string));
        dtHeader.Columns.Add("BackColor", typeof(string));

        DataRow NewRow = dtHeader.NewRow();
        NewRow["Text"] = "門市編號：" + this.txtSTORE_NO_S.Text + "～" + this.txtSTORE_NO_E.Text
            + "|交易日期：" + this.txtOrdDateStart.Text + "～" + this.txtOrdDateEnd.Text
            + "|付款方式：" + this.ASPxComboBox1.SelectedItem.Text
            + "";
        NewRow["Align"] = "LEFT";
        NewRow["FontSize"] = "11";
        NewRow["BackColor"] = "WHITE";
        dtHeader.Rows.Add(NewRow);

        DataRow NewRow2 = dtHeader.NewRow();
        NewRow2["Text"] = "列印日期：" + DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss")
            + "|列印人員：" + logMsg.MODI_USER + " " + new Employee_Facade().GetEmpName(logMsg.MODI_USER)
            + "|頁　　次：" + "1"
            + "|總筆數：" + (this.gvMaster.VisibleRowCount - 1);
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

    protected void btnReset_Click(object sender, EventArgs e)
    {
        if (logMsg.ROLE_TYPE != WebConfigurationManager.AppSettings["DefaultRoleHQ"])
        {
            ScriptManager.RegisterClientScriptBlock(this, GetType(), "AlertMessage", "alert('非總部人員無法使用此功能!!');", true);
            txtSTORE_NO_S.Enabled = false;
            txtSTORE_NO_E.Enabled = false;
            txtOrdDateStart.Enabled = false;
            txtOrdDateEnd.Enabled = false;
            ASPxComboBox1.Enabled = false;

            btnSearch.Enabled = false;
            btnReset.Enabled = false;
            btnExport.Enabled = false;
            return;
        }

        //查詢條件清空，下拉選單回復預設值
        DateTime yesterday = DateTime.Now.AddDays(-1);
        this.txtSTORE_NO_S.Text = null;
        this.txtSTORE_NO_E.Text = null;
        this.txtOrdDateStart.Text = yesterday.ToShortDateString();
        this.txtOrdDateEnd.Text = yesterday.ToShortDateString();

        this.ASPxComboBox1.SelectedIndex = 0;

        gvMaster.DataSource = null;
        gvMaster.DataBind();


    }
    #endregion
}
