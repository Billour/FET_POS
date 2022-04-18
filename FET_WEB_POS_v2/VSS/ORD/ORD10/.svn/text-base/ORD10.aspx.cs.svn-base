using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FET.POS.Model.Facade.FacadeImpl;
using FET.POS.Model.DTO;
using DevExpress.Web.ASPxGridView;
using FET.POS.Model.Common;
using Advtek.Utility;
using NPOI.HSSF.UserModel;

public partial class VSS_ORD_ORD10 : BasePage
{

    protected void gvMaster_PageIndexChanged(object sender, EventArgs e)
    {
        gvMaster.DataSource = ViewState["gvMaster"];
        gvMaster.DataBind();
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (StringUtil.CStr(Session["DOIMPOT"]) == "true")
        {
            ScriptManager.RegisterClientScriptBlock(this, typeof(string), "checkSaveData", "alert('匯入成功!!');", true);
        }
        Session["DOIMPOT"] = null;
        if (!Page.IsPostBack)
        {
            cbZone.DataSource = Common_PageHelper.getZone(true);
            cbZone.DataBind();

            Literal01.Visible = false;
            Literal02.Visible = false;
            Literal03.Visible = false;
        }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        BindData();
        gvMaster.PageIndex = 0;
        Panel1.Visible = true;

        Literal01.Visible = true;
        Literal02.Visible = true;
        Literal03.Visible = true;
    }

    private void BindData()
    {
        DataTable dt = new ORD10_Facade().GetWeightDistributeMethodData(StringUtil.CStr(cbZone.SelectedItem.Value), SelectStore1.Text);
        ViewState["gvMaster"] = dt;
        gvMaster.DataSource = dt;
        gvMaster.DataBind();
        decimal sum = 0;
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            if (StringUtil.CStr(dt.Rows[i]["WEIGHT"]).Trim().Length > 1)
            {
                sum += Convert.ToDecimal(StringUtil.CStr(dt.Rows[i]["WEIGHT"]).Replace("%", ""));
            }
        }
        Literal03.Text = StringUtil.CStr(sum) + " %";
    }

    protected void btnExport_Click(object sender, EventArgs e)
    {
        DataTable dt = new ORD10_Facade().ExportWeightDistribute(StringUtil.CStr(cbZone.SelectedItem.Value), SelectStore1.Text);
        //string filename = new Output().Print_ORD10("XLS", "", null, dt, null);
        //Response.Redirect(Utils.CreateTamperProofDownloadXlsURL("WeightDistribute" + DateTime.Now.ToString("yyyy/MM/dd") + ".xls"));

        ExportExcel(dt);
    }

    public void ExportExcel(DataTable dt)
    {
        HSSFWorkbook workbook = new HSSFWorkbook();
        System.IO.MemoryStream ms = new System.IO.MemoryStream();
        HSSFSheet sheet = workbook.CreateSheet("Sheet") as HSSFSheet;

        sheet.CreateRow(0);
        //塞入欄位的標題
        for (int i = 0; i < dt.Columns.Count; i++)
        {
            sheet.SetColumnWidth(i, 20 * 256);
            sheet.GetRow(0).CreateCell(i).SetCellValue(dt.Columns[i].ColumnName);
        }

        //塞入欄位值
        for (int i = 1; i <= dt.Rows.Count; i++)
        {
            sheet.CreateRow(i);
            for (int j = 0; j < dt.Columns.Count; j++)
            {
                sheet.GetRow(i).CreateCell(j).SetCellValue(StringUtil.CStr(dt.Rows[i - 1][j]));
            }
        }

        workbook.Write(ms);
        workbook = null;

        Response.Clear();
        Response.Buffer = false;
        Response.ContentEncoding = System.Text.Encoding.GetEncoding("UTF-8");
        Response.ContentType = "application/vnd.ms-excel";
        Response.AppendHeader("Content-Disposition", String.Format("attachment; filename={0}", "WeightDistribute" + DateTime.Now.ToString("yyyy/MM/dd") + ".xls"));
        Response.BinaryWrite(ms.GetBuffer());
        Response.End();
        ms.Close();
        ms.Dispose();
    }

}


