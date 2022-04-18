using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using NPOI.HSSF.UserModel;
using FET.POS.Model.Facade.FacadeImpl;
using FET.POS.Model.DTO;
using Advtek.Utility;
using FET.POS.Model.Common;

public partial class VSS_Common_NonDropShipmentExportPopup : Popup
{
    string NDS_NO
    {
        get
        {
            string r = "";
            if (Request.QueryString["NDNO"] != null)
            {
                r = StringUtil.CStr(Request.QueryString["NDNO"]).Trim();
            }

            return r;
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack && !IsCallback)
        {
            //取得空的資料表
            gvMaster.DataSource = new DataTable();
            gvMaster.DataBind();
        }
    }

    protected void gvMasterDV_HtmlDataCellPrepared(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewTableDataCellEventArgs e)
    {
        if (e.DataColumn.FieldName == "EXCEPTIOB_CAUSE")
        {
            if (!string.IsNullOrEmpty(StringUtil.CStr(e.CellValue)))
            {
                e.Cell.Style[HtmlTextWriterStyle.Color] = "red";
                this.btnUpdate.Enabled = false;
            }
        }
    }

    protected void btnImport_Click(object sender, EventArgs e)
    {
        if (this.FileUpload1.HasFile)
        {
            String fileName = this.FileUpload1.FileName;

            if ((fileName.Trim().Substring(fileName.Trim().IndexOf('.')) != ".xls"))
            {
                ScriptManager.RegisterClientScriptBlock(this, typeof(string), "EXCEL匯入", "alert('檔案格式錯誤!');", true);
                return;
            }

            HSSFWorkbook workbook = new HSSFWorkbook(this.FileUpload1.FileContent);
            HSSFSheet sheet = workbook.GetSheetAt(0) as HSSFSheet;

            DataTable table = new DataTable();

            HSSFRow headerRow = sheet.GetRow(0) as HSSFRow;
            int cellCount = headerRow.LastCellNum;

            for (int i = headerRow.FirstCellNum; i < cellCount; i++)
            {
                DataColumn column = new DataColumn(headerRow.GetCell(i).StringCellValue);
                table.Columns.Add(column);
            }

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

                table.Rows.Add(dataRow);
            }

            workbook = null;
            sheet = null;

            SaveTempData(table);  //將匯入的資料存入Temp Table中
        }
    }   
    
    /// <summary>
    /// 將匯入的資料存入Temp Table中
    /// </summary>
    /// <param name="dtExcel">匯入資料</param>
    private void SaveTempData(DataTable dtExcel)
    {
        ORD08_HQNDSORDERMSet_DTO ORD08_DTO = new ORD08_HQNDSORDERMSet_DTO();
        ORD08_HQNDSORDERMSet_DTO.UPLOAD_TEMPDataTable dt = ORD08_DTO.UPLOAD_TEMP;

        ORD08_Facade facade = new ORD08_Facade();

        string UploadBatchNo = GuidNo.getUUID();   //上傳批號_UUID
        this.hdUploadBatchNo.Value = UploadBatchNo;

        try
        {
            foreach (DataRow Importdr in dtExcel.Rows)
            {

                ORD08_HQNDSORDERMSet_DTO.UPLOAD_TEMPRow dr = dt.NewUPLOAD_TEMPRow();

                dr.SID = GuidNo.getUUID();
                dr.BATCH_NO = this.hdUploadBatchNo.Value;   //上傳批號_UUID
                dr.USER_ID = logMsg.OPERATOR;
                dr.FINC_ID = "ORD08_IMPORT";
                dr.F1 = StringUtil.CStr(Importdr["門市編號"]);
                dr.F2 = StringUtil.CStr(Importdr["商品料號"]);
                dr.F3 = StringUtil.CStr(Importdr["主配量"]);
                dt.Rows.Add(dr);

                ORD08_DTO.AcceptChanges();
            }
            //更新資料庫
            facade.AddNew_UploadTemp(ORD08_DTO);

            string New_UploadBatchNo = GuidNo.getUUID();   //上傳批號_UUID
            ORD08_PageHelper.Check_ImportData(this.hdUploadBatchNo.Value, New_UploadBatchNo, NDS_NO);
            this.hdUploadBatchNo.Value = New_UploadBatchNo;

            this.gvMaster.DataSource = facade.Query_UploadTemp(this.hdUploadBatchNo.Value);
            this.gvMaster.DataBind();
        }
        catch (Exception ex)
        {
            string strErr = ex.Message.Replace("'", "").Replace("\"", "").Replace("\n", "");
            //彈跳視窗
            ScriptManager.RegisterClientScriptBlock(this, GetType(), "AlertMessage", "alert('" + strErr + "');", true);  //error: " + strErr + ".
            return;
        }

    }
   
    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(this.hdUploadBatchNo.Value))
        {
            Session["UploadBatchNo"] = this.hdUploadBatchNo.Value;
            SetReturnValue(this.hdUploadBatchNo.Value);
        }

        SetReturnOKScript();

    }
    protected void gvMasterDV_PageIndexChanged(object sender, EventArgs e)
    {
        BindvMasterData();
    }
    protected void BindvMasterData()
    {
        DataTable dtNew = new ORD08_Facade().GetImportTempData(this.hdUploadBatchNo.Value, "ORD08_IMPORT", logMsg.MODI_USER);
        this.gvMaster.DataSource = dtNew;
        this.gvMaster.DataBind();
    }
}
