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

public partial class VSS_RPT_RPL051 : BasePage
{
    private int totalCount;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack && !Page.IsCallback)
        {
            //BindMasterData();
            #region 宗佑
            /*
             author：宗佑
             Date：100.2.16
             description：設定總部及門市權限
            */
            if (StringUtil.CStr(logMsg.STORENO) != System.Web.Configuration.WebConfigurationManager.AppSettings["DefaultRoleHQ"])
            {
                pupSTORE_S.Text = logMsg.STORENO;
                pupSTORE_E.Text = logMsg.STORENO;
                pupSTORE_S.Enabled = false;
                pupSTORE_E.Enabled = false;
            }


            gvMaster.DataSource = null;
            gvMaster.DataBind();
            #endregion
        }
    }

    private void BindMasterData()
    {
        #region 宗佑
        /*
             author：宗佑
             Date：100.2.16
             description：取得查詢條件值
            */

        DataTable dt = new RPL_Facade().RPL051(this.pupSTORE_S.Text, this.pupSTORE_E.Text,
            this.txtTranDateStart.Text, this.txtTranDateEnd.Text, StringUtil.CStr(this.cbPaidMode.SelectedItem.Value));
        totalCount = dt.Rows.Count - 1;
        this.gvMaster.Templates.PagerBar = new CustomPagerBarTemplate(totalCount);
        this.gvMaster.DataSource = dt;
        this.gvMaster.DataBind();

        if (StringUtil.CStr(logMsg.STORENO) != System.Web.Configuration.WebConfigurationManager.AppSettings["DefaultRoleHQ"])
        {
            pupSTORE_S.Text = logMsg.STORENO;
            pupSTORE_E.Text = logMsg.STORENO;
            pupSTORE_S.Enabled = false;
            pupSTORE_E.Enabled = false;
        }
        #endregion
    }

    private void BindDropDownList()
    {
        //this.ddlReason.DataSource = RPL_PageHelper.GetReason();
        //ddlReason.TextField = "REASON";
        //ddlReason.ValueField = "REASONID";
        //this.ddlReason.DataBind();
        //this.ddlReason.SelectedIndex = 0;

        //this.ddlStock.DataSource = RPL_PageHelper.GetStock();
        //ddlStock.TextField = "STOCK_NAME";
        //ddlStock.ValueField = "LOC_ID";
        //this.ddlStock.DataBind();
        //this.ddlReason.SelectedIndex = 0;

    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        BindMasterData();
        gvMaster.PageIndex = 0;
    }

    protected void btnExport_Click(object sender, EventArgs e)
    {
        BindMasterData();
        new Output().ExportXLS(800, "", Resources.WebResources.RPL051, dtHeader(), this.ASPxGridViewExporter1, null, null);
        Response.Redirect(Utils.CreateTamperProofDownloadXlsURL("RPL051.xls"));
    }

    private DataTable dtHeader()
    {
        DataTable dtHeader = new DataTable();
        dtHeader.Columns.Add("Text", typeof(string));
        dtHeader.Columns.Add("Align", typeof(string));
        dtHeader.Columns.Add("FontSize", typeof(string));
        dtHeader.Columns.Add("BackColor", typeof(string));

        DataRow NewRow = dtHeader.NewRow();
        NewRow["Text"] = "門市編號：" + this.pupSTORE_S.Text + "～" + this.pupSTORE_E.Text
            + "|交易日期：" + this.txtTranDateStart.Text + "～" + this.txtTranDateEnd.Text
            + "|儲值類別：" + this.cbPaidMode.SelectedItem.Text;
        NewRow["Align"] = "LEFT";
        NewRow["FontSize"] = "11";
        NewRow["BackColor"] = "WHITE";
        dtHeader.Rows.Add(NewRow);

        NewRow = dtHeader.NewRow();
        NewRow["Text"] = "列印日期：" + DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss")
            + "|列印人員：" + logMsg.MODI_USER + " " + new Employee_Facade().GetEmpName(logMsg.MODI_USER)
            + "|頁　　次：" + "1"
            + "|總筆數：" + StringUtil.CStr(this.gvMaster.VisibleRowCount - 1);
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

    #region 宗佑
    /*
             author：宗佑
             Date：100.2.16
             description：查詢條件清空，下拉選單回復預設值
            */
    protected void btnReset_Click(object sender, EventArgs e)
    {


        this.pupSTORE_S.Text = "";
        this.pupSTORE_E.Text = "";

        this.txtTranDateStart.Text = "";
        this.txtTranDateEnd.Text = "";
        this.cbPaidMode.SelectedIndex = 0;

        gvMaster.DataSource = null;
        gvMaster.DataBind();

        //gvSum.DataSource = null;
        //gvSum.DataBind();

        if (StringUtil.CStr(logMsg.STORENO) != System.Web.Configuration.WebConfigurationManager.AppSettings["DefaultRoleHQ"])
        {
            pupSTORE_S.Text = logMsg.STORENO;
            pupSTORE_E.Text = logMsg.STORENO;
            pupSTORE_S.Enabled = false;
            pupSTORE_E.Enabled = false;
        }
    }
    #endregion


}


