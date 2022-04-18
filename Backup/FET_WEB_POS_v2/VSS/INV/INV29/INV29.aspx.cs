using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Globalization;
using Advtek.Utility;
using System.Collections;
using DevExpress.Web;
using DevExpress.Web.ASPxGridView;
using DevExpress.Web.ASPxPager;

using FET.POS.Model.DTO;
using FET.POS.Model.Facade;
using FET.POS.Model.Facade.FacadeImpl;
using FET.POS.Model.Common;
using NPOI.HSSF.UserModel;
using System.Data.OleDb;

public partial class VSS_INV_INV29 : BasePage
{
    private static string BATCH_NO;
    private static string FINC_ID = "INV29_IMPORT";

    string sHostID = System.Web.HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];

    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void bindMasterData()
    {
        //DataTable dt = getMasterData();

        //if (dt.Select("STATUS='E' or STATUS='T' or STATUS is null ").Length > 0)
        //    this.btnSave.Enabled = false;
        //else
        //    this.btnSave.Enabled = true;

        //gvMaster.DataSource = dt;
        //gvMaster.DataBind();
    }

    private DataTable getMasterData()
    {
        DataTable dtResult = new DataTable();
        dtResult = new INV29_Facade().Query_IMEI_UPLOAD_TEMP(this.hdUploadBatchNo.Value, logMsg.OPERATOR);
        return dtResult;
    }

    protected void btnImport_Click(object sender, EventArgs e)
    {
        if (!this.FileUpload.HasFile) { return; }

        String fileName = this.FileUpload.FileName;

        if ((fileName.Trim().Substring(fileName.Trim().IndexOf('.')) != ".xls"))
        {
            ScriptManager.RegisterClientScriptBlock(this, typeof(string), "EXCEL匯入", "alert('檔案格式錯誤!');", true);
            return;
        }

        HSSFWorkbook workbook = new HSSFWorkbook(this.FileUpload.FileContent);
        try
        {
            //Product_no
            HSSFSheet sheet = workbook.GetSheet("IMEI") as HSSFSheet;

            if (sheet == null)
            {
                ScriptManager.RegisterClientScriptBlock(this, typeof(string), "EXCEL匯入", "alert('查無名為 IMEI 之活頁簿名稱!');", true);
                return;
            }

            HSSFRow headerRow = sheet.GetRow(0) as HSSFRow;

            int cellCount = headerRow.LastCellNum;
            int rowCount = sheet.LastRowNum;

            DataTable TmpTable = new DataTable();

            if (cellCount != 1)
            {
                ScriptManager.RegisterClientScriptBlock(this, typeof(string), "cmd", "alert('IMEI活頁格式錯誤!!');", true);
                return;
            }

            for (int i = headerRow.FirstCellNum; i < cellCount; i++)
            {
                DataColumn column = new DataColumn(headerRow.GetCell(i).StringCellValue);
                TmpTable.Columns.Add(column);
            }

            for (int i = (sheet.FirstRowNum + 1); i <= sheet.LastRowNum; i++)
            {
                HSSFRow row = sheet.GetRow(i) as HSSFRow;
                DataRow dataRow = TmpTable.NewRow();
                if (row != null)
                {
                    for (int j = row.FirstCellNum; j < cellCount; j++)
                    {
                        if (row.GetCell(j) != null)
                            dataRow[j] = row.GetCell(j).ToString();
                    }

                    TmpTable.Rows.Add(dataRow);
                }

            }

            //匯入暫存用Table
            //DataTable Table = new DataTable();
            INV29_IMEIUpload_DTO ds = new INV29_IMEIUpload_DTO();
            //INV29_IMEIUpload_DTO.UPLOAD_TEMPDataTable Table = new INV29_IMEIUpload_DTO.UPLOAD_TEMPDataTable();

            //本批次號
            BATCH_NO = GuidNo.getUUID();
            string USER_ID = logMsg.MODI_USER;
            this.hdUploadBatchNo.Value = BATCH_NO;

            //將exl的每個值抓出來Product_no
            for (int i = 0; i <= TmpTable.Rows.Count - 1; i++)
            {
                INV29_IMEIUpload_DTO.UPLOAD_TEMPRow dataRow = ds.UPLOAD_TEMP.NewUPLOAD_TEMPRow();

                dataRow["F1"] = TmpTable.Rows[i][0].ToString();
                dataRow["F2"] = logMsg.STORENO;
                dataRow["BATCH_NO"] = BATCH_NO;
                dataRow["FINC_ID"] = FINC_ID;
                dataRow["USER_ID"] = USER_ID;
                dataRow["SID"] = GuidNo.getUUID();
                ds.UPLOAD_TEMP.Rows.Add(dataRow);
                ds.UPLOAD_TEMP.AcceptChanges();
            }
            if (TmpTable.Rows.Count == 0)
            {
                ScriptManager.RegisterClientScriptBlock(this, typeof(string), "EXCEL匯入", "alert('商品料號為空白!');", true);
                return;
            }

           

            //將匯入的資料存入Temp Table中
            try
            {
                new INV29_Facade().Delete_IMEI_UPLOAD_TEMP(ds, USER_ID, FINC_ID);
                new INV29_Facade().AddNew_IMEI_UPLOAD_TEMP(ds);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            DataTable dtNew = new INV29_Facade().SP_CHECK_IMEI_UPLOAD(BATCH_NO, USER_ID, FINC_ID);
            gvMaster.PageIndex = 0;
            Session["gvMaster"] = dtNew;
            gvMaster.DataSource = dtNew;
            gvMaster.DataBind();


            //int success_count = 0;
            //int failed_count = 0;

            //foreach (DataRow dr in dtNew.Rows)
            //{
            //    if (string.IsNullOrEmpty(dr["異常原因"].ToString()))
            //    {
            //        success_count++;
            //    }
            //    else
            //    {
            //        failed_count++;
            //    }
            //}


        }
        catch
        {
            ScriptManager.RegisterClientScriptBlock(this, typeof(string), "EXCEL匯入", "alert('匯入格式錯誤!');", true);
            return;
        }
    }

    protected void grid_PageIndexChanged(object sender, EventArgs e)
    {

        gvMaster.DataSource = Session["gvMaster"];
        gvMaster.DataBind();
        

    }





}
