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
using NPOI.HSSF.UserModel;
using Advtek.Utility;
using FET.POS.Model.DTO;
using FET.POS.Model.Facade;
using FET.POS.Model.Facade.FacadeImpl;
using FET.POS.Model.Common;

public partial class VSS_DIS_DIS01_Gift_Import : Popup
{
   #region Class Variables
   private DIS01_Facade _DIS01_Facade;
   private DIS01_DiscountMasterDataSet_DTO _DIS01_DiscountMasterDataSet_DTO;
   private static string BATCH_NO;
   private static string USER_ID = "";
   #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        USER_ID = logMsg.OPERATOR;
       if (!Page.IsPostBack)
       {
           btnCommit.Enabled = false;
          //取得空的資料表
          gvProduct.DataSource = getGridViewDataProduct();
          gvProduct.DataBind();

       }
    }

    private DataTable getGridViewDataProduct()
    {
       DataTable dtResult = new DataTable();
       dtResult.Columns.Add("PRODNO", typeof(string));
       dtResult.Columns.Add("PRODNAME", typeof(string));
       dtResult.Columns.Add("AMT", typeof(string));
       dtResult.Columns.Add("RESULT", typeof(string));
       dtResult.PrimaryKey = new DataColumn[] { dtResult.Columns["PRODNO"] };
       return dtResult;
    }

    protected void bindMasterDataProduct()
    {
       DataTable dtResult = new DataTable();
       if (ViewState["ImportTable_P"] == null)
       {
          dtResult = getGridViewDataProduct();
       }
       else
       {
           dtResult = (DataTable)ViewState["ImportTable_P"];
       }
       gvProduct.DataSource = dtResult;
       gvProduct.DataBind();
    }

    protected void btnImport_Click(object sender, EventArgs e)
    {
        try
        {
           _DIS01_Facade = new DIS01_Facade();
           _DIS01_DiscountMasterDataSet_DTO = new DIS01_DiscountMasterDataSet_DTO();

           if (!this.FileUpload.HasFile) { return; }
           String fileName = this.FileUpload.FileName;

           if ((fileName.Trim().Substring(fileName.Trim().IndexOf('.')) != ".xls"))
           {
               ScriptManager.RegisterClientScriptBlock(this, typeof(string), "EXCEL匯入", "alert('檔案格式錯誤!');", true);
               return;
           }
           HSSFWorkbook workbook = new HSSFWorkbook(this.FileUpload.FileContent);

           //Product_no
           HSSFSheet sheet = workbook.GetSheetAt(0) as HSSFSheet;

           HSSFRow headerRow = sheet.GetRow(0) as HSSFRow;

           int cellCount = headerRow.LastCellNum;
           int rowCount = sheet.LastRowNum;
           DataTable TmpTable = new DataTable();

           if (cellCount != 2)
           {
               ScriptManager.RegisterClientScriptBlock(this, typeof(string), "cmd", "alert('格式錯誤!!');", true);
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
           DIS01_DiscountMasterDataSet_DTO.UPLOAD_TEMPDataTable Table;
           Table = (DIS01_DiscountMasterDataSet_DTO.UPLOAD_TEMPDataTable)_DIS01_DiscountMasterDataSet_DTO.Tables["UPLOAD_TEMP"];

           //本批次號
           BATCH_NO = GuidNo.getUUID();
           Session["BATCH_NO"] = "GIFT;" + BATCH_NO;
           string FINC_ID = "DIS01_PROD_IMPORT";
           this.hdUploadBatchNo.Value = BATCH_NO;

           //將exl的每個值抓出來Product_no
           for (int i = 0; i <= TmpTable.Rows.Count - 1; i++)
           {
              string PRODNO = StringUtil.CStr(TmpTable.Rows[i][0]);
              if (string.IsNullOrEmpty(PRODNO)) { continue; }

              //HSSFRow row = sheet.GetRow(i) as HSSFRow;
              //DataRow dataRow = Table.NewRow();
              DIS01_DiscountMasterDataSet_DTO.UPLOAD_TEMPRow dataRow = Table.NewUPLOAD_TEMPRow();

              //string PRODNO = row.Cells[0].StringCellValue;
              string AMT = StringUtil.CStr(TmpTable.Rows[i][1]);
              dataRow.F1 = "GIFT";
              dataRow.F2 = PRODNO;
              dataRow.F4 = AMT;
              dataRow.BATCH_NO = BATCH_NO;
              dataRow.FINC_ID = FINC_ID;
              dataRow.USER_ID = USER_ID;

              Table.Rows.Add(dataRow);
              _DIS01_DiscountMasterDataSet_DTO.AcceptChanges();


           }

           //將匯入的資料存入Temp Table中
           try
           {
               _DIS01_Facade.ImportHeadQuarter(_DIS01_DiscountMasterDataSet_DTO, "GIFT", "");
           }
           catch (Exception ex)
           {
              throw ex;
           }

           DataTable dtNew = _DIS01_Facade.GetImportTempData(BATCH_NO, FINC_ID, USER_ID, "GIFT");
           ViewState["ImportTable_P"] = dtNew;
           this.gvProduct.DataSource = dtNew;
           this.gvProduct.DataBind();

           int success_count = 0;
           int failed_count = 0;

           foreach (DataRow dr in dtNew.Rows)
           {
              if (string.IsNullOrEmpty(StringUtil.CStr(dr["RESULT"])))
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

           ViewState["BATCH_NO"] = BATCH_NO;
        }
        catch (Exception)
        {
            ScriptManager.RegisterClientScriptBlock(this, typeof(string), "匯入", "alert('匯入格式錯誤!');", true);

        }

    }

    protected void btnCommit_Click(object sender, EventArgs e)
    {
       if (!string.IsNullOrEmpty(this.hdUploadBatchNo.Value))
       {
           try
           {
              //更新資料庫
              _DIS01_Facade = new DIS01_Facade();
              _DIS01_Facade.UpdateOne_UpLoadTempMethodSet(BATCH_NO, "DIS01_PROD_IMPORT", USER_ID);

              this.btnCommit.Enabled = false;
              SetReturnValue(string.Empty);
              ScriptManager.RegisterClientScriptBlock(this, typeof(string), "匯入", "alert('匯入完成!');", true);
              SetReturnOKScript();
           }
           catch (Exception)
           {
               ScriptManager.RegisterClientScriptBlock(this, typeof(string), "匯入", "alert('匯入失敗!');", true);
           }
       }

      
    }

    protected void btnReset_Click(object sender, EventArgs e) 
    {
        if(ViewState["ImportTable_P"]!=null)
           ViewState["ImportTable_P"] = null;
        this.gvProduct.DataSource = null;
        this.gvProduct.DataBind();
        btnCommit.Enabled = false;
    }

    protected void gvProduct_HtmlRowPrepared(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewTableRowEventArgs e)
    {
        if (e.RowType != GridViewRowType.Data) return;
        if (StringUtil.CStr(e.GetValue("RESULT")) != string.Empty)
            e.Row.ForeColor = System.Drawing.Color.Red;

    }

    protected void gvProduct_PageIndexChanged(object sender, EventArgs e)
    {
        bindMasterDataProduct();
    }
}
