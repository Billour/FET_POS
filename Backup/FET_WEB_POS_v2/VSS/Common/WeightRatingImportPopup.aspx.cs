using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Web.UI.HtmlControls;
using DevExpress.Web.ASPxEditors;
using DevExpress.Web.ASPxGridView;
using FET.POS.Model.Facade.FacadeImpl;
using FET.POS.Model.DTO;
using Advtek.Utility;

using NPOI.HSSF.UserModel;
public partial class VSS_Common_WeightRatingImportPopup : Popup
{
    
    protected void Page_Load(object sender, EventArgs e)
    {

        if (!Page.IsPostBack)
        {
            btnCommit.Enabled = false;
            gvMaster.DataSource = GetEmptyDataTable1();
            gvMaster.DataBind();

        }
    }
    private void UploadData()
    {
        if (this.FileUpload.HasFile)
        {
            if (FileUpload.FileName.Substring(FileUpload.FileName.Length - 4, 4) != ".xls")
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), "Error", "<script>alert('輸入需為EXCEL格式檔案!');</script>");
               
                return;
            }


            try 
            {
                HSSFWorkbook workbook = new HSSFWorkbook(this.FileUpload.FileContent);
                HSSFSheet sheet = workbook.GetSheetAt(0) as HSSFSheet;
                DataTable table = new DataTable();
                HSSFRow headerRow = sheet.GetRow(0) as HSSFRow;
                int cellCount = headerRow.LastCellNum;

                for (int i = headerRow.FirstCellNum; i < cellCount; i++)
                {
                    DataColumn column = new DataColumn(headerRow.GetCell(i).StringCellValue);
                    table.Columns.Add(column);
                }

                DataColumn columnStoreName = new DataColumn("門市名稱");
                table.Columns.Add(columnStoreName);

                DataColumn columnLast = new DataColumn("異常原因");
                table.Columns.Add(columnLast);

                int rowCount = sheet.LastRowNum;

                for (int i = (sheet.FirstRowNum + 1); i <= sheet.LastRowNum; i++)
                {
                    HSSFRow row = sheet.GetRow(i) as HSSFRow;
                    DataRow dataRow = table.NewRow();

                    for (int j = row.FirstCellNum; j < cellCount; j++)
                    {
                        if (row.GetCell(j) != null)
                            dataRow[j] = StringUtil.CStr(row.GetCell(j));
                    }
                    if (StringUtil.CStr(dataRow["門市編號"]).Trim() == "" || new Store_Facade().GetStoreName(StringUtil.CStr(dataRow["門市編號"])) == "")
                        dataRow["異常原因"] = "店組不存在";
                    else if (StringUtil.CStr(dataRow["門市編號"]).Trim() != "" && (new Store_Facade().GetStoreName(StringUtil.CStr(dataRow["門市編號"]))==""))
                        dataRow["異常原因"] = "門市已閉店";
                    else if (StringUtil.CStr(dataRow["比率"]).Trim() == "")
                        dataRow["異常原因"] = "比率不可為空值";
                    else if (Convert.ToInt16(dataRow["比率"]) < 1)
                        dataRow["異常原因"] = "比率應大於0";
                    else
                    {
                        dataRow["門市名稱"] = new Store_Facade().GetStoreName(StringUtil.CStr(dataRow["門市編號"]));
                        dataRow["異常原因"] = "";
                    }
                    table.Rows.Add(dataRow);
                }

                btnCommit.Enabled = true;
                foreach (DataRow dr in table.Rows)
                {
                    if (StringUtil.CStr(dr["異常原因"]).Trim() != "")
                    { btnCommit.Enabled = false; break; }
                }
                workbook = null;
                sheet = null;

                ViewState["ImportTable"] = table;
                this.gvMaster.DataSource = table;
                this.gvMaster.DataBind();
            }
            catch (Exception ex) 
            {
                DataTable dt = GetEmptyDataTable1();
                DataRow dr = dt.NewRow();
                dr["異常原因"] = StringUtil.CStr(ex);
                dt.Rows.Add(dr);
                ViewState["ImportTable"] = dt;
                this.gvMaster.DataSource = dt;
                this.gvMaster.DataBind();
            }
            
        }
    }
    protected void btnImport_Click(object sender, EventArgs e)
    {
        UploadData();
    }
    //protected void BindData()
    //{
    //    gvMaster.DataSource = GetData();
    //    gvMaster.DataBind();
    //}
    //private DataTable GetData()
    //{
    //    DataTable dtResult = GetEmptyDataTable1();

    //    DataRow NewRow = dtResult.NewRow();
    //    NewRow["門市編號"] = 2101;
    //    NewRow["門市名稱"] = "";
    //    NewRow["異常原因"] = "店組不存在";
    //    dtResult.Rows.Add(NewRow);

    //    NewRow = dtResult.NewRow();
    //    NewRow["門市編號"] = 2102;
    //    NewRow["門市名稱"] = "遠企";
    //    NewRow["異常原因"] = "比率不可為空值";
    //    dtResult.Rows.Add(NewRow);

    //    NewRow = dtResult.NewRow();
    //    NewRow["門市編號"] = 2103;
    //    NewRow["門市名稱"] = "西門";
    //    NewRow["比率"] = 0;
    //    NewRow["異常原因"] = "比率應大於0";
    //    dtResult.Rows.Add(NewRow);

    //    NewRow = dtResult.NewRow();
    //    NewRow["門市編號"] = 2105;
    //    NewRow["門市名稱"] = "華納";
    //    NewRow["比率"] = 90;
    //    dtResult.Rows.Add(NewRow);

    //    NewRow = dtResult.NewRow();
    //    NewRow["門市編號"] = 2106;
    //    NewRow["門市名稱"] = "站前";
    //    NewRow["比率"] = 8;
    //    dtResult.Rows.Add(NewRow);

    //    return dtResult;
    //}

    private DataTable GetEmptyDataTable1()
    {
        DataTable dtResult = new DataTable();
        dtResult.Columns.Add("門市編號", typeof(int));
        dtResult.Columns.Add("門市名稱", typeof(string));
        dtResult.Columns.Add("比率", typeof(int));
        dtResult.Columns.Add("異常原因", typeof(string));
        return dtResult;
    }
    protected void btnCommit_Click(object sender, EventArgs e)
    {
        DataTable dt = ViewState["ImportTable"] as DataTable;
        new ORD10_Facade().ImportWeightDistribute(dt, logMsg.MODI_USER);
        
        //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "RunTime", "<script>cancelWindow();</script>", true);        
        //RegisterClientScriptBlock("Import", "<script>alert('匯入完成，請重新查詢資料!');</script>");
        ClientScript.RegisterClientScriptBlock(this.GetType(), "Runtime", "<script>cancelWindow();</script>");
    }

    protected void gvMaster_HtmlDataCellPrepared(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewTableDataCellEventArgs e)
    {
        if (e.DataColumn.FieldName == "異常原因")
        {
            if (!string.IsNullOrEmpty(StringUtil.CStr(e.CellValue)))
            {
                e.Cell.Style[HtmlTextWriterStyle.Color] = "red";
            }
        }
    }
    protected void gvMaster_HtmlFooterCellPrepared(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewTableFooterCellEventArgs e)
    {
       // btnCommit.Enabled = false;
        if (gvMaster.VisibleRowCount > 0)
        {
            int TotRate = 0;
            DataTable dt = ViewState["ImportTable"] as DataTable;
            foreach (DataRow dr in dt.Rows)
            {
                try
                {
                    TotRate += Convert.ToInt16(dr["比率"]);
                }
                catch //(Exception ex)
                {
                    TotRate += 0;
                }
            }
            ASPxLabel labelTotal = (ASPxLabel)gvMaster.FindFooterCellTemplateControl(e.Column, "lblFooterTotal");
            if (e.Column.Caption == "比率")
            {
                labelTotal.Text = StringUtil.CStr(TotRate);
            }
            else if (e.Column.Caption == "異常原因")
            {
                if (TotRate != 100)
                {
                    labelTotal.Text = "加總比率非100%";
                    btnCommit.Enabled = false;

                    e.Cell.Style[HtmlTextWriterStyle.Color] = "red";
                }
            }
            else
            {
                labelTotal.Visible = false;
            //    btnCommit.Enabled = true;
            }
        }  
    }
    protected void Button3_Click(object sender, EventArgs e)
    {
    }
    protected void btnCancel_Click(object sender, EventArgs e) 
    {
        gvMaster.DataSource = null;
        gvMaster.DataBind();
    }
    
}
