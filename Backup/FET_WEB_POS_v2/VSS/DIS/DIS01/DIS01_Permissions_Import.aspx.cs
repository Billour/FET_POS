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

public partial class VSS_DIS_DIS01_Permissions_Import : Popup
{
   #region Class Variables
   private DIS01_Facade _DIS01_Facade;
   private DIS01_DiscountMasterDataSet_DTO _DIS01_DiscountMasterDataSet_DTO;
   private static string BATCH_NO;
   #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
       if (!Page.IsPostBack)
       {
           btnCommit.Enabled = false;
          //取得空的資料表
          gvProduct.DataSource = getGridViewDataPROMO();
          gvProduct.DataBind();

       }    
    }

    private DataTable getGridViewDataPROMO()
    {
       DataTable dtResult = new DataTable();
       dtResult.Columns.Add("PROMO", typeof(string));
       dtResult.Columns.Add("PROMONAME", typeof(string));
       dtResult.Columns.Add("RESULT", typeof(string));
       dtResult.PrimaryKey = new DataColumn[] { dtResult.Columns["PROMO"] };
       return dtResult;
    }

    protected void bindMasterDataProduct()
    {
       DataTable dtResult = new DataTable();
       if (ViewState["ImportTable_Pr"] == null)
       {
          dtResult = getGridViewDataPROMO();
       }
       else
       {
           dtResult = (DataTable)ViewState["ImportTable_Pr"];
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

           if (cellCount != 1)
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
           Session["BATCH_NO"] = "PROMO;" + BATCH_NO;
           string FINC_ID = "DIS01_PROMO_IMPORT";
           string USER_ID = logMsg.MODI_USER;
           this.hdUploadBatchNo.Value = BATCH_NO;

           //將exl的每個值抓出來Product_no
           for (int i = 0; i <= TmpTable.Rows.Count - 1; i++)
           {
              string PROMO = StringUtil.CStr(TmpTable.Rows[i][0]);
              if (string.IsNullOrEmpty(PROMO)) { continue; }

              //HSSFRow row = sheet.GetRow(i) as HSSFRow;
              //DataRow dataRow = Table.NewRow();
              DIS01_DiscountMasterDataSet_DTO.UPLOAD_TEMPRow dataRow = Table.NewUPLOAD_TEMPRow();

              dataRow.F1 = "PROMO";
              dataRow.F2 = PROMO;
              dataRow.BATCH_NO = BATCH_NO;
              dataRow.FINC_ID = FINC_ID;
              dataRow.USER_ID = USER_ID;

              Table.Rows.Add(dataRow);
              _DIS01_DiscountMasterDataSet_DTO.AcceptChanges();


           }

           //將匯入的資料存入Temp Table中
           try
           {
              _DIS01_Facade.ImportHeadQuarter(_DIS01_DiscountMasterDataSet_DTO, "PROMO", "");
           }
           catch (Exception ex)
           {
              throw ex;
           }

           DataTable dtNew = _DIS01_Facade.GetImportTempData(BATCH_NO, FINC_ID, USER_ID, "PROMO");
           ViewState["ImportTable_Pr"] = dtNew;
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
              _DIS01_Facade.UpdateOne_UpLoadTempMethodSet(BATCH_NO, "DIS01_PROMO_IMPORT", logMsg.MODI_USER);

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

       //SetReturnOKScript();
    }

    protected void btnReset_Click(object sender, EventArgs e)
    {
        if (ViewState["ImportTable_Pr"] != null)
            ViewState["ImportTable_Pr"] = null;
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
