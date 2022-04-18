using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NPOI.HSSF.UserModel;
using System.Data;
using DevExpress.Web.ASPxGridView;
using DevExpress.Web.ASPxEditors;

using Advtek.Utility;
using FET.POS.Model.DTO;
using FET.POS.Model.Facade;
using FET.POS.Model.Facade.FacadeImpl;
using FET.POS.Model.Common;

public partial class VSS_INV_INV05_Import : Popup
{
    private static string BATCH_NO;
    private static string FINC_ID = "INV05_IMPORT";

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            btnCommit.Enabled = false;
            //取得空的資料表
            gvProduct.DataSource = getMasterEmptyProdData();
            gvProduct.DataBind();

            gvStore.DataSource = getMasterEmptyStoreData();
            gvStore.DataBind();
        }
    }

    private DataTable getMasterEmptyProdData()
    {
        DataTable dtResult = new DataTable();
        dtResult.Columns.Add("商品料號", typeof(string));
        dtResult.Columns.Add("商品名稱", typeof(string));
        dtResult.Columns.Add("異常原因", typeof(string));
        return dtResult;
    }
    private DataTable getMasterEmptyStoreData()
    {
        DataTable dtResult = new DataTable();
        dtResult.Columns.Add("門市編號", typeof(string));
        dtResult.Columns.Add("門市名稱", typeof(string));
        dtResult.Columns.Add("異常原因", typeof(string));
        return dtResult;
    }

    protected void BindProductData()
    {
        DataTable dtNew = new INV05_Facade().GetImportTempData_P(this.hdUploadBatchNo.Value, FINC_ID, logMsg.MODI_USER, "PRODUCT");
        this.gvProduct.DataSource = dtNew;
        this.gvProduct.DataBind();
    }
    protected void BindStoreData()
    {
        DataTable dtNewS = new INV05_Facade().GetImportTempData_S(this.hdUploadBatchNo.Value, FINC_ID, logMsg.MODI_USER, "STORE");
        this.gvStore.DataSource = dtNewS;
        this.gvStore.DataBind();
    }

    protected void btnImport_Click(object sender, EventArgs e)
    {
        if (!this.FileUpload.HasFile) { return; }

        String fileName = this.FileUpload.FileName;

        if ((fileName.Trim().Substring(fileName.Trim().IndexOf('.')).ToLower() != ".xls") )
        {
            ScriptManager.RegisterClientScriptBlock(this, typeof(string), "EXCEL匯入", "alert('檔案格式錯誤!');", true);
            return;
        }

        HSSFWorkbook workbook = new HSSFWorkbook(this.FileUpload.FileContent);
        try
        {
            //Product_no
            HSSFSheet sheet = workbook.GetSheet("Product_no") as HSSFSheet;

            if (sheet == null)
            {
                ScriptManager.RegisterClientScriptBlock(this, typeof(string), "EXCEL匯入", "alert('查無名為 Product_no 之活頁簿名稱!');", true);
                return;
            }

            HSSFRow headerRow = sheet.GetRow(0) as HSSFRow;

            int cellCount = headerRow.LastCellNum;
            int rowCount = sheet.LastRowNum;

            DataTable TmpTable = new DataTable();

            if (cellCount != 1)
            {
                ScriptManager.RegisterClientScriptBlock(this, typeof(string), "cmd", "alert('Product_no活頁格式錯誤!!');", true);
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
                            dataRow[j] = StringUtil.CStr(row.GetCell(j));
                    }

                    TmpTable.Rows.Add(dataRow);
                }

            }

            //匯入暫存用Table
            //DataTable Table = new DataTable();
            INV05_RTNM_DTO.UPLOAD_TEMPDataTable Table = new INV05_RTNM_DTO.UPLOAD_TEMPDataTable();

            //本批次號
            BATCH_NO = GuidNo.getUUID();
            string USER_ID = logMsg.MODI_USER;
            this.hdUploadBatchNo.Value = BATCH_NO;

            //將exl的每個值抓出來Product_no
            for (int i = 0; i <= TmpTable.Rows.Count-1; i++)
            {
                    INV05_RTNM_DTO.UPLOAD_TEMPRow dataRow = Table.NewUPLOAD_TEMPRow();

                    string PRODNO = StringUtil.CStr(TmpTable.Rows[i][0]); //.StringCellValue;
                    dataRow["F1"] = "PRODUCT";
                    dataRow["F2"] = PRODNO;
                    dataRow["BATCH_NO"] = BATCH_NO;
                    dataRow["FINC_ID"] = FINC_ID;
                    dataRow["USER_ID"] = USER_ID;

                    Table.Rows.Add(dataRow);
                    Table.AcceptChanges();
            }
            if (TmpTable.Rows.Count == 0)
            {
                ScriptManager.RegisterClientScriptBlock(this, typeof(string), "EXCEL匯入", "alert('商品料號為空白!');", true);
                return;
            }


            //IVR_Code
            HSSFSheet sheet1 = workbook.GetSheet("IVR_Code") as HSSFSheet;

            if (sheet1 == null)
            {
                ScriptManager.RegisterClientScriptBlock(this, typeof(string), "EXCEL匯入", "alert('查無名為 IVR_Code 之活頁簿名稱!');", true);
                return;
            }

            HSSFRow headerRow1 = sheet1.GetRow(0) as HSSFRow;

            int cellCount1 = headerRow1.LastCellNum;
            int rowCount1 = sheet1.LastRowNum;

            DataTable TmpTable1 = new DataTable();

            if (cellCount1 != 1)
            {
                ScriptManager.RegisterClientScriptBlock(this, typeof(string), "cmd", "alert('IVR_Code活頁格式錯誤!!');", true);
                return;
            }

            for (int i = headerRow1.FirstCellNum; i < cellCount1; i++)
            {
                DataColumn column1 = new DataColumn(headerRow1.GetCell(i).StringCellValue);
                TmpTable1.Columns.Add(column1);
            }



            for (int i = (sheet1.FirstRowNum + 1); i <= sheet1.LastRowNum; i++)
            {
                HSSFRow row1 = sheet1.GetRow(i) as HSSFRow;
                DataRow dataRow1 = TmpTable1.NewRow();
                if (row1 != null)
                {
                    for (int j = row1.FirstCellNum; j < cellCount1; j++)
                    {
                        if (row1.GetCell(j) != null)
                            dataRow1[j] = StringUtil.CStr(row1.GetCell(j));
                    }

                    TmpTable1.Rows.Add(dataRow1);
                }

            }

            //匯入暫存用Table
            //DataTable Table1 = new DataTable();
            INV05_RTNM_DTO.UPLOAD_TEMPDataTable Table1 = new INV05_RTNM_DTO.UPLOAD_TEMPDataTable();

            //將exl的每個值抓出來IVR_Code
            for (int i = 0; i <= TmpTable1.Rows.Count - 1; i++)
            {
                INV05_RTNM_DTO.UPLOAD_TEMPRow dataRow1 = Table1.NewUPLOAD_TEMPRow();

                string STORE = StringUtil.CStr(TmpTable1.Rows[i][0]); //.StringCellValue;
                dataRow1["F1"] = "STORE";
                dataRow1["F2"] = STORE;
                dataRow1["BATCH_NO"] = BATCH_NO;
                dataRow1["FINC_ID"] = FINC_ID;
                dataRow1["USER_ID"] = USER_ID;

                Table1.Rows.Add(dataRow1);
                Table1.AcceptChanges();

                if (string.IsNullOrEmpty(STORE)) { break; }

            }
            if (TmpTable1.Rows.Count == 0)
            {
                ScriptManager.RegisterClientScriptBlock(this, typeof(string), "EXCEL匯入", "alert('門市編號為空白!');", true);
                return;
            }


            //將匯入的資料存入Temp Table中
            try
            {
                new INV05_Facade().ImportHeadQuarter(Table, Table1);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            DataTable dtNew = new INV05_Facade().GetImportTempData_P(BATCH_NO, FINC_ID, USER_ID, "PRODUCT");
            this.gvProduct.DataSource = dtNew;
            this.gvProduct.DataBind();

            DataTable dtNewS = new INV05_Facade().GetImportTempData_S(BATCH_NO, FINC_ID, USER_ID, "STORE");
            this.gvStore.DataSource = dtNewS;
            this.gvStore.DataBind();

            int success_count = 0;
            int failed_count = 0;

            foreach (DataRow dr in dtNew.Rows)
            {
                if (string.IsNullOrEmpty(StringUtil.CStr(dr["異常原因"])))
                {
                    success_count++;
                }
                else
                {
                    failed_count++;
                }
            }

            foreach (DataRow dr in dtNewS.Rows)
            {
                if (string.IsNullOrEmpty(StringUtil.CStr(dr["異常原因"])))
                {
                    success_count++;
                }
                else
                {
                    failed_count++;
                }
            }

       
            if (failed_count > 0)
            {
                btnCommit.Enabled = false;
            }
            else
            {
                btnCommit.Enabled = true;
            }

        }
        catch
        {
            ScriptManager.RegisterClientScriptBlock(this, typeof(string), "EXCEL匯入", "alert('匯入格式錯誤!');", true);
            return;
        }
    }
    protected void btnCommit_Click(object sender, EventArgs e)
    {

        if (!string.IsNullOrEmpty(this.hdUploadBatchNo.Value))
        {
            //更新資料庫
            new INV05_Facade().UpdateOne_UpLoadTempMethodSet(BATCH_NO, "INV05_IMPORT", logMsg.MODI_USER);
            Session["BATCH_NO"] = this.hdUploadBatchNo.Value;
            this.btnCommit.Enabled = false;
            SetReturnValue(string.Empty);
            ClientScript.RegisterClientScriptBlock(this.GetType(), "Import", "<script>alert('匯入完成!');</script>");
            SetReturnOKScript();
        }
    }

    protected void gvProduct_HtmlRowPrepared(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewTableRowEventArgs e)
    {
        if (e.RowType != GridViewRowType.Data) return;
        if (StringUtil.CStr(e.GetValue("異常原因")) != string.Empty)
            e.Row.ForeColor = System.Drawing.Color.Red;

    }
    protected void gvProduct_PageIndexChanged(object sender, EventArgs e)
    {
        BindProductData();
    }

    protected void gvStore_HtmlRowPrepared(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewTableRowEventArgs e)
    {
        if (e.RowType != GridViewRowType.Data) return;
        if (StringUtil.CStr(e.GetValue("異常原因")) != string.Empty)
            e.Row.ForeColor = System.Drawing.Color.Red;
    }
    protected void gvStore_PageIndexChanged(object sender, EventArgs e)
    {
        BindStoreData();
    }
}
