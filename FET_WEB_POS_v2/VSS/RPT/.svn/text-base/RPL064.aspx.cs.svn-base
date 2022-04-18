using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FET.POS.Model.Facade.FacadeImpl;
using System.Data;
using System.Web.Configuration;

public partial class VSS_RPT_RPL064 : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack && !Page.IsCallback)
        {
            // BindMasterData();
        }

    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        BindMasterData();
    }
    private void BindMasterData()
    {
        string storeNo = string.Empty;
        if (logMsg.ROLE_TYPE != WebConfigurationManager.AppSettings["DefaultRoleHQ"])
        {
            //非總部人員只能查自己門市的資料
            storeNo = logMsg.STORENO;
        }
     
        Session["Data"] = new RPL_Facade().RPL064("", this.txtRtnNo.Text,
            this.txtOrdDateStart.Text, this.txtOrdDateEnd.Text,
             this.txtSProdNo.Text, this.txtEProdNo.Text, storeNo);
        this.gvMaster.DataSource = Session["Data"];
        this.gvMaster.DataBind();
        gvMaster.PageIndex = 0;
    }
    protected void btnExport_Click(object sender, EventArgs e)
    {
        BindMasterData();
        new Output().ExportXLS(800, "", Resources.WebResources.RPL064, dtHeader(), this.ASPxGridViewExporter1, null, null);
        Response.Redirect(Utils.CreateTamperProofDownloadXlsURL("RPL064.xls"));
    }

    private DataTable dtHeader()
    {
        DataTable dtHeader = new DataTable();
        dtHeader.Columns.Add("Text", typeof(string));
        dtHeader.Columns.Add("Align", typeof(string));
        dtHeader.Columns.Add("FontSize", typeof(string));
        dtHeader.Columns.Add("BackColor", typeof(string));

        DataRow NewRow = dtHeader.NewRow();
        NewRow["Text"] = (this.rType1.Checked ? this.rType1.Text : this.rType2.Text)
            + "|退倉單號：" + this.txtRtnNo.Text
            + "|退倉日期：" + this.txtOrdDateStart.Text + "~" + this.txtOrdDateEnd.Text
            + "|商品料號：" + this.txtSProdNo.Text + "~" + this.txtEProdNo.Text;
        NewRow["Align"] = "LEFT";
        NewRow["FontSize"] = "11";
        NewRow["BackColor"] = "WHITE";
        dtHeader.Rows.Add(NewRow);

        NewRow = dtHeader.NewRow();
        NewRow["Text"] = "列印日期：" + DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss") + "|列印人員：" + this.logMsg.MODI_USER + " " + new Employee_Facade().GetEmpName(this.logMsg.MODI_USER)
            + "|頁　　次：1"
            + "|總筆數：" + this.gvMaster.VisibleRowCount;
        NewRow["Align"] = "LEFT";
        NewRow["FontSize"] = "11";
        NewRow["BackColor"] = "WHITE";
        dtHeader.Rows.Add(NewRow);
        return dtHeader;

    }

    protected void gvMaster_PageIndexChanged(object sender, EventArgs e)
    {
        this.gvMaster.DataSource = Session["Data"];
        this.gvMaster.DataBind();
    }


    protected void btnReset_Click(object sender, EventArgs e)
    {
        this.txtRtnNo.Text = "";
        this.txtOrdDateStart.Text = "";
        this.txtOrdDateEnd.Text = "";
        this.txtSProdNo.Text = "";
        this.txtEProdNo.Text = "";
        this.rType1.Checked = true;
        this.rType2.Checked = false;
        gvMaster.DataSource = null;
        gvMaster.DataBind();

    }
}
