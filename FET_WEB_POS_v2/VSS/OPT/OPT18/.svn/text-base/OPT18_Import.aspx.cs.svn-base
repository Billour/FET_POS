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

public partial class VSS_OPT_OPT18_Import : Popup
{
    private string QryUSER;

    protected void Page_Load(object sender, EventArgs e)
    {
        QryUSER = logMsg.OPERATOR;
        if (!IsPostBack && !IsCallback)
        {
            btnUpload.Enabled = false;
            //取得空的資料表
            gvMaster.DataSource = new OPT18_StoreSpecialDis_DTO().STORE_SPECIAL_DISCOUNT_TEMP;
            gvMaster.DataBind();
        }
    }

    protected void btnImport_Click(object sender, EventArgs e)
    {
        try
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
        catch (Exception)
        {
            ScriptManager.RegisterClientScriptBlock(this, typeof(string), "匯入", "alert('匯入格式錯誤!');", true);
        }
    }

    /// <summary>
    /// 將匯入的資料存入Temp Table中
    /// </summary>
    /// <param name="dtExcel">匯入資料</param>
    private void SaveTempData(DataTable dtExcel)
    {
        OPT18_StoreSpecialDis_DTO OPT18_DTO = new OPT18_StoreSpecialDis_DTO();
        OPT18_StoreSpecialDis_DTO.UPLOAD_TEMPDataTable dt = OPT18_DTO.UPLOAD_TEMP;

        OPT18_Facade facade = new OPT18_Facade();

        string UploadBatchNo = GuidNo.getUUID();   //上傳批號_UUID
        this.hdUploadBatchNo.Value = UploadBatchNo;

        foreach (DataRow Importdr in dtExcel.Rows)
        {

            OPT18_StoreSpecialDis_DTO.UPLOAD_TEMPRow dr = dt.NewUPLOAD_TEMPRow();

            dr.SID = GuidNo.getUUID();
            dr.BATCH_NO = this.hdUploadBatchNo.Value;   //上傳批號_UUID
            dr.USER_ID = this.QryUSER;
            dr.FINC_ID = "OPT18_IMPORT";
            dr.F1 = StringUtil.CStr(Importdr["門市編號"]);
            dr.F2 = StringUtil.CStr(Importdr["角色"]);
            dr.F3 = StringUtil.CStr(Importdr["折扣月份"]);
            dr.F4 = StringUtil.CStr(Importdr["折扣總額"]);
            dr.F5 = StringUtil.CStr(Importdr["折扣金額"]);
            dr.F6 = StringUtil.CStr(Importdr["折扣比率"]);
            dr.F7 = StringUtil.CStr(Importdr["折扣上限金額"]);
            dt.Rows.Add(dr);

            OPT18_DTO.AcceptChanges();
        }

        //更新資料庫
        facade.AddNew_UploadTemp(OPT18_DTO, this.hdUploadBatchNo.Value);

        DataTable Qrydt = facade.Query_StoreSpecialDiscountTemp(this.hdUploadBatchNo.Value);
        this.gvMaster.DataSource = Qrydt;
        this.gvMaster.DataBind();


        if (Qrydt.Rows.Count > 0)
        {
            this.lblSuccess.Text = "成功筆數： " + Qrydt.Select("EXCEPTIOB_CAUSE IS NULL").Length + " 筆";
            this.lblFail.Text = "失敗筆數： " + Qrydt.Select("EXCEPTIOB_CAUSE IS NOT NULL").Length + " 筆";
            if (Qrydt.Select("EXCEPTIOB_CAUSE IS NOT NULL").Length==0)
            this.btnUpload.Enabled = true;
        }

    }

    protected void gvMaster_HtmlDataCellPrepared(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewTableDataCellEventArgs e)
    {
        if (e.DataColumn.FieldName == "EXCEPTIOB_CAUSE")
        {
            if (!string.IsNullOrEmpty(StringUtil.CStr(e.GetValue("EXCEPTIOB_CAUSE"))))
            {
                e.Cell.Style[HtmlTextWriterStyle.Color] = "red";
                if (this.btnUpload.Enabled)
                {
                    this.btnUpload.Enabled = false;
                              }
            }
        }
    }

    protected void btnUpload_Click(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(this.hdUploadBatchNo.Value))
        {
            OPT18_Facade facade = new OPT18_Facade();
            //更新資料庫
            facade.AddNew_StoreSpecialDiscount(this.hdUploadBatchNo.Value);

            //this.btnUpload.Enabled = false;

            Session["UploadBatchNo"] = this.hdUploadBatchNo.Value;
            SetReturnValue(this.hdUploadBatchNo.Value);

        }

        SetReturnOKScript();

    }

    protected void BindStoreSpecialDiscountData()
    {
        OPT18_Facade facade = new OPT18_Facade();
        DataTable Qrydt = facade.Query_StoreSpecialDiscountTemp(this.hdUploadBatchNo.Value);
        this.gvMaster.DataSource = Qrydt;
        this.gvMaster.DataBind();
    }

    protected void gvMaster_PageIndexChanged(object sender, EventArgs e)
    {
        BindStoreSpecialDiscountData();
    }

    protected void btnReset_Click(object sender, EventArgs e)
    {

        this.gvMaster.DataSource = null;
        this.gvMaster.DataBind();
        this.btnUpload.Enabled = false;
    }
}
