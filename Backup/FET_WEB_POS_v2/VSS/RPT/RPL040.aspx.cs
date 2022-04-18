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


public partial class VSS_RPT_RPL040 : BasePage 
{
    protected void Page_Load(object sender, EventArgs e)
    {
        /*if (logMsg.ROLE_TYPE != WebConfigurationManager.AppSettings["DefaultRoleHQ"])
        {
            ScriptManager.RegisterClientScriptBlock(this, GetType(), "AlertMessage", "alert('非總部人員無法使用此功能!!');", true);
            txtTRADE_DATE_S.Enabled = false;
            txtTRADE_DATE_E.Enabled = false;
            

            btnSearch.Enabled = false;
            btnReset.Enabled = false;
            btnExport.Enabled = false;
            return;
        }*/

        if (!IsPostBack && !Page.IsCallback)
        {
            btnReset_Click(null, null);
        }

        
    }
    
    /*
     Author：宗佑
     Date：100.02.14
     Description:將查詢條件套入SQL指令以查詢資料
    */
    private void BindMasterData()
    {
        this.gvMaster.DataSource = new RPL_Facade().RPL040(this.txtTRADE_DATE_S.Text, this.txtTRADE_DATE_E.Text);
        this.gvMaster.DataBind();
    }
    
    
    /*
     Author：宗佑
     Date：100.02.14
     Description:按下查詢按鈕後依查詢條件查詢資料並預設GridView為第1頁
    */
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        BindMasterData();
        gvMaster.PageIndex = 0;
    }
    
    
    /*
     Author：宗佑
     Date：100.02.14
     Description:查詢條件清空
    */
    protected void btnReset_Click(object sender, EventArgs e)
    {
        if (logMsg.ROLE_TYPE != WebConfigurationManager.AppSettings["DefaultRoleHQ"])
        {
            ScriptManager.RegisterClientScriptBlock(this, GetType(), "AlertMessage", "alert('非總部人員無法使用此功能!!');", true);
            txtTRADE_DATE_S.Enabled = false;
            txtTRADE_DATE_E.Enabled = false;


            btnSearch.Enabled = false;
            btnReset.Enabled = false;
            btnExport.Enabled = false;
            return;
        }

        //查詢條件清空，下拉選單回復預設值        

        this.txtTRADE_DATE_S.Text = "";
        this.txtTRADE_DATE_E.Text = "";
        gvMaster.DataSource = null;
        gvMaster.DataBind();
    }
    
    
    /*
     Author：宗佑
     Date：100.02.14
     Description:按下匯出按鈕匯出成Excel表
    */
    protected void btnExport_Click(object sender, EventArgs e)
    {
        BindMasterData();
        new Output().ExportXLS(420, "", Resources.WebResources.RPL040, dtHeader(), this.ASPxGridViewExporter1, null, null);
        Response.Redirect(Utils.CreateTamperProofDownloadXlsURL("RPL040.xls"));
    }
    
    
    /*
     Author：宗佑
     Date：100.02.14
     Description:當GridView換頁時重新BindData
    */
    protected void gvMaster_PageIndexChanged(object sender, EventArgs e)
    {
        BindMasterData();
    }
    
    
    /*
     Author：宗佑
     Date：100.02.14
     Description:匯出Excel之表頭設定
    */
    private DataTable dtHeader()
    {
        DataTable dtHeader = new DataTable();
        dtHeader.Columns.Add("Text", typeof(string));
        dtHeader.Columns.Add("Align", typeof(string));
        dtHeader.Columns.Add("FontSize", typeof(string));
        dtHeader.Columns.Add("BackColor", typeof(string));

        DataRow NewRow = dtHeader.NewRow();
        NewRow["Text"] = ""
            + "銷售日期："  + this.txtTRADE_DATE_S.Text + "～" + this.txtTRADE_DATE_E.Text
            + "";
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
    
}