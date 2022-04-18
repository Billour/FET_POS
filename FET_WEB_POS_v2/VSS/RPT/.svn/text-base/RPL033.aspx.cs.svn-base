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

public partial class VSS_RPT_RPL033 : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (logMsg.ROLE_TYPE != WebConfigurationManager.AppSettings["DefaultRoleHQ"])
        {
            ScriptManager.RegisterClientScriptBlock(this, GetType(), "AlertMessage", "alert('非總部人員無法使用此功能!!');", true);
            txtSTK_DATE.Enabled = false;
            txtProductTypeNoS.Enabled = false;
            txtProductTypeNoE.Enabled = false;
            txtPRODNO_S.Enabled = false;
            txtPRODNO_E.Enabled = false;
            btnSearch.Enabled = false;
            btnReset.Enabled = false;
            btnExport.Enabled = false;
            return;
        }

        if (!IsPostBack && !Page.IsCallback)
        {
            txtSTK_DATE.Text = DateTime.Now.ToString("yyyy/MM");
            BindMasterData();
        }
    }

    private void BindMasterData()
    {
        DataTable dt = new RPL_Facade().RPL033(this.txtSTK_DATE.Text, txtProductTypeNoS.Text, txtProductTypeNoE.Text, this.txtPRODNO_S.Text, this.txtPRODNO_E.Text);
        //組出column
        
        if (dt.Columns.Count > 3)
        {
            gvMaster.Columns.Clear();
            gvMaster.Columns.Add(new GridViewDataColumn("ITEMCODE"));
            gvMaster.Columns[0].Width = Unit.Pixel(80);
            gvMaster.Columns.Add(new GridViewDataColumn("品名"));


            //增加全區
            gvMaster.Columns.Add(new GridViewDataColumn("全區"));

            for (int i = 3; i < dt.Columns.Count; i++)
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
        NewRow = dtHeader.NewRow();
        NewRow["Text"] = "日期：" + this.txtSTK_DATE.Text;
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

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        if ((txtProductTypeNoS.Text.Trim() != "" && txtProductTypeNoE.Text.Trim() != "") &&
            (string.Compare(txtProductTypeNoS.Text.Trim(), txtProductTypeNoE.Text.Trim()) > 0))
        {
            ScriptManager.RegisterClientScriptBlock(this, GetType(), "AlertMessage", "alert('[商品類別起值]不允許大於[商品類別訖值]，請重新輸入!');", true);
        }
        else
        {
            gvMaster.PageIndex = 0;
            BindMasterData();
        }
    }

    protected void btnExport_Click(object sender, EventArgs e)
    {
        BindMasterData();
        new Output().ExportXLS(800, "", Resources.WebResources.RPL033, dtHeader(), this.ASPxGridViewExporter1, null, null);
        Response.Redirect(Utils.CreateTamperProofDownloadXlsURL("RPL033.xls")); 
    }

    protected void gvMaster_PageIndexChanged(object sender, EventArgs e)
    {
        BindMasterData();
    }
}
