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

public partial class VSS_RPT_RPL037 : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack && !Page.IsCallback)
        {
            if (logMsg.ROLE_TYPE != System.Web.Configuration.WebConfigurationManager.AppSettings["DefaultRoleHQ"])
            {
                ScriptManager.RegisterClientScriptBlock(this, GetType(), "AlertMessage", "alert('非總部人員無法使用此功能!!');", true);
                
                txtOrdDateStart.Enabled = false;
                txtOrdDateEnd.Enabled = false;
                pupPRODTYPENO_S.Enabled = false;
                pupPRODTYPENO_E.Enabled = false;
                PopupControl1.Enabled = false;
                PopupControl2.Enabled = false;

                btnSearch.Enabled = false;
                btnReset.Enabled = false;
                btnExport.Enabled = false;
                return;
            }
            BindDropDownList();
            btnReset_Click(null, null);
        }
    }

    private void BindMasterData()
    {
        this.gvMaster.DataSource = new RPL_Facade().RPL037(this.txtOrdDateStart.Text, this.txtOrdDateEnd.Text,
            this.pupPRODTYPENO_S.Text, this.pupPRODTYPENO_E.Text, PopupControl1.Text, PopupControl2.Text);
        this.gvMaster.DataBind();
    }

    private void BindDropDownList()
    {

    }

    private DataTable dtHeader()
    {
        DataTable dtHeader = new DataTable();
        dtHeader.Columns.Add("Text", typeof(string));
        dtHeader.Columns.Add("Align", typeof(string));
        dtHeader.Columns.Add("FontSize", typeof(string));
        dtHeader.Columns.Add("BackColor", typeof(string));

        DataRow NewRow = dtHeader.NewRow();
        NewRow["Text"] = "促銷生效日期：" + this.txtOrdDateStart.Text + " ~ " + this.txtOrdDateEnd.Text
            + "|促銷代碼：" + this.pupPRODTYPENO_S.Text + " ~ " + this.pupPRODTYPENO_E.Text
            + "|商品類別：" + this.PopupControl1.Text + " ~ " + this.PopupControl2.Text;
        NewRow["Align"] = "LEFT";
        NewRow["FontSize"] = "11";
        NewRow["BackColor"] = "WHITE";
        dtHeader.Rows.Add(NewRow);

        NewRow = dtHeader.NewRow();
        NewRow["Text"] = "列印日期：" + DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss")
            + "|列印人員：" + logMsg.MODI_USER + " " + new Employee_Facade().GetEmpName(logMsg.MODI_USER)
            + "|頁　　次：" + "1"
            + "|總筆數：" + this.gvMaster.VisibleRowCount;
        NewRow["Align"] = "LEFT";
        NewRow["FontSize"] = "11";
        NewRow["BackColor"] = "WHITE";
        dtHeader.Rows.Add(NewRow);
        return dtHeader;

    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        BindMasterData();
        gvMaster.PageIndex = 0;
    }

    protected void btnExport_Click(object sender, EventArgs e)
    {
        BindMasterData();
        new Output().ExportXLS(800, "", Resources.WebResources.RPL037, dtHeader(), this.ASPxGridViewExporter1, null, null);
        Response.Redirect(Utils.CreateTamperProofDownloadXlsURL("RPL037.xls"));
    }

    protected void gvMaster_PageIndexChanged(object sender, EventArgs e)
    {
        BindMasterData();
    }

    protected void btnReset_Click(object sender, EventArgs e)
    {
        //查詢條件清空，下拉選單回復預設值        

        txtOrdDateStart.Text = null;
        txtOrdDateEnd.Text = null;
        pupPRODTYPENO_S.Text = null;
        pupPRODTYPENO_E.Text = null;
        PopupControl1.Text = null;
        PopupControl2.Text = null;

        gvMaster.DataSource = null;
        gvMaster.DataBind();
    }
}
