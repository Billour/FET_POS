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
using DevExpress.Web.ASPxGridView;

public partial class VSS_RPT_RPL044 : BasePage
{
    //private string QryUSER = "Tina";

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //if (logMsg.ROLE_TYPE != System.Web.Configuration.WebConfigurationManager.AppSettings["DefaultRoleHQ"])
            //{
            //    ScriptManager.RegisterClientScriptBlock(this, GetType(), "AlertMessage", "alert('非總部人員無法使用此功能!!');", true);

            //    txtB_DATE_S.Enabled = false;
            //    txtB_DATE_E.Enabled = false;
            //    txtPROMO_NO_S.Enabled = false;
            //    txtPROMO_NO_E.Enabled = false;
            //    txtProdName.Enabled = false;

            //    btnSearch.Enabled = false;
            //    btnReset.Enabled = false;
            //    btnExport.Enabled = false;
            //    return;
            //}
            btnReset_Click(null, null);
        }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        BindMasterData();
    }

    protected void btnExport_Click(object sender, EventArgs e)
    {
        BindMasterData();

        new Output().ExportXLS(1500, "", Resources.WebResources.RPL044, dtHeader(), this.ASPxGridViewExporter1, null, null);
        Response.Redirect(Utils.CreateTamperProofDownloadXlsURL("RPL044.xls"));
    }

    protected void gvMaster_PageIndexChanged(object sender, EventArgs e)
    {
        BindMasterData();
    }

    private void BindMasterData()
    {
        DataTable dt = new RPL_Facade().RPL044(this.txtB_DATE_S.Text, this.txtB_DATE_E.Text,
           this.txtPROMO_NO_S.Text, this.txtPROMO_NO_E.Text, this.txtProdName.Text, logMsg.STORENO);

        if (dt.Columns.Count > 0)
        {
            gvMaster.Columns.Clear();
            for (int i = 0; i < dt.Columns.Count; i++)
            {
                GridViewDataColumn col = new GridViewDataColumn(dt.Columns[i].ColumnName);
                col.Caption = dt.Columns[i].ColumnName;

                gvMaster.Columns.Add(col);
            }
        }

        this.gvMaster.DataSource = dt;
        this.gvMaster.DataBind();
    }

    private DataTable dtHeader()
    {
        DataTable dtHeader = new DataTable();
        dtHeader.Columns.Add("Text", typeof(string));
        dtHeader.Columns.Add("Align", typeof(string));
        dtHeader.Columns.Add("FontSize", typeof(string));
        dtHeader.Columns.Add("BackColor", typeof(string));

        DataRow NewRow = dtHeader.NewRow();
        NewRow["Text"] = "促銷生效日：" + this.txtB_DATE_S.Text + "～" + this.txtB_DATE_E.Text
                       + "|促銷代號：" + this.txtPROMO_NO_S.Text + "～" + this.txtPROMO_NO_E.Text
                       + "|商品名稱：" + this.txtProdName.Text;

        NewRow["Align"] = "LEFT";
        NewRow["FontSize"] = "11";
        NewRow["BackColor"] = "WHITE";
        dtHeader.Rows.Add(NewRow);

        NewRow = dtHeader.NewRow();
        NewRow["Text"] =
              "列印日期：" + DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss")
            + "|列印人員：" + logMsg.MODI_USER + " " + new Employee_Facade().GetEmpName(logMsg.MODI_USER)
            + "|頁　　次：1"
            + "|總筆數：" + this.gvMaster.VisibleRowCount;

        NewRow["Align"] = "LEFT";
        NewRow["FontSize"] = "11";
        NewRow["BackColor"] = "WHITE";
        dtHeader.Rows.Add(NewRow);

        return dtHeader;

    }

    protected void btnReset_Click(object sender, EventArgs e)
    {
        //查詢條件清空，下拉選單回復預設值        

        this.txtB_DATE_S.Text = null;
        this.txtB_DATE_E.Text = null;
        this.txtPROMO_NO_S.Text = null;
        this.txtPROMO_NO_E.Text = null;
        this.txtProdName.Text = null;

        gvMaster.DataSource = null;
        gvMaster.DataBind();
    }
}
