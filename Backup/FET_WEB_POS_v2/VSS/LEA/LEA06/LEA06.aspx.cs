using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Advtek.Utility;
using FET.POS.Model.Facade;
using FET.POS.Model.Facade.FacadeImpl;

public partial class VSS_LEA_LEA06 : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ComStoreNo.TextField = "STORENAME";
            ComStoreNo.ValueField = "STORE_NO";
            DataTable DT = new Store_Facade().Get_Store("");
            ComStoreNo.SelectedIndex = 0;
            ComStoreNo.DataSource = DT;// Supplier_Facade.GetSupplierNo(true);
            ComStoreNo.DataBind();
            //bindEmptyData();
        }
    }

    protected void gvMaster_PageIndexChanged(object sender, EventArgs e)
    {
        gvMaster.DataSource = Session["dtResult"];
        gvMaster.DataBind();
    }

    protected void btnExport_Click(object sender, EventArgs e)
    {
        DataTable dtExport;
        //總部人員可查詢各門市資料, 門市人員只能查詢自己的門市
        dtExport = new LEA06_Facade().GetExportData(ComStoreNo.Value == null ? "" : StringUtil.CStr(ComStoreNo.Value), SRDATE.Text, ERDATE.Text);
        string filename = new Output().Print("XLS", "租賃收費明細表查詢<td></td><td>匯出時間：" + DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss") + "</td>", null, dtExport, null);
        Response.Redirect(Utils.CreateTamperProofDownloadXlsURL("RENT_M_" + DateTime.Now.ToString("yyyy/MM/dd") + ".xls"));

    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        DataTable dtResult = new DataTable();
        dtResult = new LEA06_Facade().GetSelectData(ComStoreNo.Value == null ? "" : StringUtil.CStr(ComStoreNo.Value), SRDATE.Text, ERDATE.Text);
        Session["dtResult"] = dtResult;
        gvMaster.DataSource = dtResult;
        gvMaster.DataBind();
    }
}
