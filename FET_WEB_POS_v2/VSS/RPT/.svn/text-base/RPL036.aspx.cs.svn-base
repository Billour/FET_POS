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

public partial class VSS_RPT_RPL036 : BasePage 
{
    protected void Page_Load(object sender, EventArgs e)
    {        
        /*Author：宗佑
          Date：100.02.16
          Description:載入頁面時判斷是否為總部人員之權限
        */
        if (!IsPostBack)
        {
            btnReset_Click(null, null);
        }
    }

    
    /*
     Author：宗佑
     Date：100.02.16
     Description:將查詢條件套入SQL指令以查詢資料
    */
    private void BindMasterData()
    {
        this.gvMaster.DataSource = new RPL_Facade().RPL036(this.txtOrdDateStart.Text, this.txtOrdDateEnd.Text,
            this.pupPRODTYPENO_S.Text, this.pupPRODTYPENO_E.Text, this.pupPRODNAME.Text, this.pupNO_S.Text,
            this.pupNO_E.Text, this.pupPROMONAME.Text);
        this.gvMaster.DataBind();
    }
    
    
    /*
     Author：宗佑
     Date：100.02.16
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
        NewRow["Text"] = "銷售日期：" + this.txtOrdDateStart.Text + "～" + this.txtOrdDateEnd.Text
                      + "|商品料號：" + this.pupPRODTYPENO_S.Text + "～" + this.pupPRODTYPENO_E.Text
                      + "|商品名稱：" + this.pupPRODNAME.Text
                      + "|促銷代碼：" + this.pupNO_S.Text + "～" + this.pupNO_E.Text
                      + "|促銷名稱：" + this.pupPROMONAME.Text
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
    
    
    /*
     Author：宗佑
     Date：100.02.16
     Description:按下查詢按鈕後依查詢條件查詢資料並預設GridView為第1頁
    */
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        BindMasterData();
        gvMaster.PageIndex = 0;
    }
    
    
    /*
     Author：宗佑
     Date：100.02.16
     Description:按下匯出按鈕匯出成Excel表
    */
    protected void btnExport_Click(object sender, EventArgs e)
    {
        BindMasterData();
        new Output().ExportXLS(800, "", Resources.WebResources.RPL036, dtHeader(), this.ASPxGridViewExporter1, null, null);
        Response.Redirect(Utils.CreateTamperProofDownloadXlsURL("RPL036.xls"));
    }
    
    
    /*
     Author：宗佑
     Date：100.02.16
     Description:當GridView換頁時重新BindData
    */
    protected void gvMaster_PageIndexChanged(object sender, EventArgs e)
    {
       BindMasterData();
    }
    
    
    /*
     Author：宗佑
     Date：100.02.16
     Description:查詢條件清空，下拉選單回復預設值
    */
    protected void btnReset_Click(object sender, EventArgs e)
    {
        if (logMsg.ROLE_TYPE != WebConfigurationManager.AppSettings["DefaultRoleHQ"])
        {
            ScriptManager.RegisterClientScriptBlock(this, GetType(), "AlertMessage", "alert('非總部人員無法使用此功能!!');", true);
            txtOrdDateStart.Enabled = false;
            txtOrdDateEnd.Enabled = false;
            pupPRODTYPENO_S.Enabled = false;
            pupPRODTYPENO_E.Enabled = false;
            pupPRODNAME.Enabled = false;
            pupNO_S.Enabled = false;
            pupNO_E.Enabled = false;
            pupPROMONAME.Enabled = false;

            btnSearch.Enabled = false;
            btnReset.Enabled = false;
            btnExport.Enabled = false;
            return;
        }
        
        //查詢條件清空，下拉選單回復預設值
        txtOrdDateStart.Text = DateTime.Now.ToString("yyyy/MM/dd");
        txtOrdDateEnd.Text = DateTime.Now.ToString("yyyy/MM/dd");
        pupPRODTYPENO_S.Text = null;
        pupPRODTYPENO_E.Text = null;
        pupPRODNAME.Text = null;
        pupNO_S.Text = null;
        pupNO_E.Text = null;
        pupPROMONAME.Text = null;

        gvMaster.DataSource = null;
        gvMaster.DataBind();
    }
    
}
