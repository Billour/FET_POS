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

public partial class VSS_RPT_RPL019 : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {

        /*Author：宗佑
          Date：100.02.15
          Description:載入頁面時判斷是否為總部人員之權限
        */
        if (!IsPostBack)
        {
            btnReset_Click(null, null);
        }

        /*if (!IsPostBack && !Page.IsCallback)
        {
            
            BindMasterData();
        }*/

    }

    /*
     Author：宗佑
     Date：100.02.15
     Description:將查詢條件套入SQL指令以查詢資料
    */
    private void BindMasterData()
    {
        this.gvMaster.DataSource = new RPL_Facade().RPL019(StringUtil.CStr(this.ddltype.SelectedItem.Value ), this.txtno.Text,
            StringUtil.CStr(this.ddlerror.SelectedItem.Value ));
        this.gvMaster.DataBind();
    }


    /*
     Author：宗佑
     Date：100.02.15
     Description:按下查詢按鈕後依查詢條件查詢資料並預設GridView為第1頁
    */
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        BindMasterData();
        gvMaster.PageIndex = 0;
    }


    /*
     Author：宗佑
     Date：100.02.15
     Description:查詢條件清空，下拉選單回復預設值
    */
    protected void btnReset_Click(object sender, EventArgs e)
    {
        if (logMsg.ROLE_TYPE != WebConfigurationManager.AppSettings["DefaultRoleHQ"])
        {
            ScriptManager.RegisterClientScriptBlock(this, GetType(), "AlertMessage", "alert('非總部人員無法使用此功能!!');", true);
            ddltype.Enabled = false;
            txtno.Enabled = false;
            ddlerror.Enabled = false;

            btnSearch.Enabled = false;
            btnReset.Enabled = false;
            btnExport.Enabled = false;
            return;
        }

        //查詢條件清空，下拉選單回復預設值
        this.ddltype.SelectedIndex = 0;
        this.txtno.Text = null;
        this.ddlerror.SelectedIndex = 0;
        gvMaster.DataSource = null;
        gvMaster.DataBind();
    }


    /*
     Author：宗佑
     Date：100.02.15
     Description:按下匯出按鈕匯出成Excel表
    */
    protected void btnExport_Click(object sender, EventArgs e)
    {
        //匯出至EXCEL
        BindMasterData();
        new Output().ExportXLS(880, "", Resources.WebResources.RPL019, dtHeader(), this.ASPxGridViewExporter1, null, null);
        Response.Redirect(Utils.CreateTamperProofDownloadXlsURL("RPL019.xls"));
    }


    /*
     Author：宗佑
     Date：100.02.15
     Description:當GridView換頁時重新BindData
    */
    protected void gvMaster_PageIndexChanged(object sender, EventArgs e)
    {
        BindMasterData();
    }


    /*
     Author：宗佑
     Date：100.02.15
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
            + "折扣類別：" + this.ddltype.SelectedItem.Text
            + "|折扣料號：" + this.txtno.Text
            + "|是否失效：" + this.ddlerror.SelectedItem.Text
            + "";
        NewRow["Align"] = "LEFT";
        NewRow["FontSize"] = "11";
        NewRow["BackColor"] = "WHITE";
        dtHeader.Rows.Add(NewRow);

        DataRow NewRow2 = dtHeader.NewRow();
        NewRow2["Text"] = "列印日期：" + DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss")
            + "|列印人員：" + logMsg.MODI_USER + " " + new Employee_Facade().GetEmpName(logMsg.MODI_USER)
            + "|頁　　次：" + "1"
            + "|總筆數：" + this.gvMaster.VisibleRowCount;
        NewRow2["Align"] = "LEFT";
        NewRow2["FontSize"] = "11";
        NewRow2["BackColor"] = "WHITE";
        dtHeader.Rows.Add(NewRow2);

        return dtHeader;

    }

}
