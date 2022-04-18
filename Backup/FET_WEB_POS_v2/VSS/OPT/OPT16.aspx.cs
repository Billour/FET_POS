using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using DevExpress.Web;
using DevExpress.Web.ASPxGridView;
using DevExpress.Web.ASPxPager;

using Advtek.Utility;
using FET.POS.Model.DTO;
using FET.POS.Model.Facade;
using System.Web.UI.HtmlControls;
using FET.POS.Model.Facade.FacadeImpl;
using NPOI.HSSF.UserModel;
using FET.POS.Model.Common;
using System.Collections.Specialized;

public partial class VSS_OPT_OPT16 : Popup
{
    private string QryFUNCID
    {
        get
        {
            string strFUNCID = "";
            //**2011/04/27 Tina：前頁傳遞參數時，會先以加密處理，在此要解密。
            if (!string.IsNullOrEmpty(Request.QueryString["Param"]))
            {
                NameValueCollection qscoll = Utils.Param_UrlDecode(Request.QueryString["Param"]);
                foreach (string key in qscoll.AllKeys)
                {
                    if (key == "FUNCID")
                    {
                        strFUNCID = string.Join(",", qscoll.GetValues(key));
                        break;
                    }
                }
            }

            return strFUNCID;
        }
    }

    private string QryUUID
    {
        get
        {
            string strUUID = "";
            //**2011/04/27 Tina：前頁傳遞參數時，會先以加密處理，在此要解密。
            if (!string.IsNullOrEmpty(Request.QueryString["Param"]))
            {
                NameValueCollection qscoll = Utils.Param_UrlDecode(Request.QueryString["Param"]);
                foreach (string key in qscoll.AllKeys)
                {
                    if (key == "UUID")
                    {
                        strUUID = string.Join(",", qscoll.GetValues(key));
                        break;
                    }
                }
            }

            return strUUID;
        }
    }

    private string QryDISCOUNTID
    {
        get
        {
            string strDISCOUNTID = "";
            //**2011/04/27 Tina：前頁傳遞參數時，會先以加密處理，在此要解密。
            if (!string.IsNullOrEmpty(Request.QueryString["Param"]))
            {
                NameValueCollection qscoll = Utils.Param_UrlDecode(Request.QueryString["Param"]);
                foreach (string key in qscoll.AllKeys)
                {
                    if (key == "DISCOUNTID")
                    {
                        strDISCOUNTID = string.Join(",", qscoll.GetValues(key));
                        break;
                    }
                }
            }

            return strDISCOUNTID;
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (QryFUNCID == "")
        {
            this.btnImport.ClientEnabled = false;
            this.btnUpload.ClientEnabled = false;
        }
        else
        {
            lblSuccess.Text = "0";
            lblFail.Text = "0";

            if (!IsPostBack && !IsCallback)
            {
                //取得空的資料表
                gvMaster.DataSource = new OPT16_HgConvertMemberList_DTO().HG_CONVERT_MEMBER_LIST_TEMP;
                gvMaster.DataBind();
                btnUpload.Enabled = false;
            }
        }
        FileUpload1.Enabled = true;
    }

    protected void btnImport_Click(object sender, EventArgs e)
    {
        try
        {
            if (this.FileUpload1.HasFile)
            {
                string FileExten = System.IO.Path.GetExtension(FileUpload1.PostedFile.FileName);
                if (FileExten.ToLower() == ".xls")
                {
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
                        if (row != null)
                        {
                            int iCount = 0;
                            for (int j = row.FirstCellNum; j < cellCount; j++)
                            {
                                if (row.GetCell(j) != null && !string.IsNullOrEmpty(StringUtil.CStr(row.GetCell(j))))
                                {
                                    dataRow[j] = StringUtil.CStr(row.GetCell(j));
                                    iCount += 1;
                                }

                            }
                            if (iCount == cellCount)
                                table.Rows.Add(dataRow);
                        }
                    }

                    workbook = null;
                    sheet = null;
                    gvMaster.PageIndex = 0;

                    SaveTempData(table);  //將匯入的資料存入Temp Table中
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this, GetType(), "AlertMessage", "alert('匯入的檔案不正確,須為Excel檔!!');", true);
                }
            }
        }
        catch
        {
            ScriptManager.RegisterClientScriptBlock(this, GetType(), "AlertMessage", "alert('匯入的內容格式不正確!!');", true);
        }
    }

    /// <summary>
    /// 將匯入的資料存入Temp Table中
    /// </summary>
    /// <param name="dtExcel">匯入資料</param>
    private void SaveTempData(DataTable dtExcel)
    {
        OPT16_HgConvertMemberList_DTO OPT16_DTO = new OPT16_HgConvertMemberList_DTO();
        OPT16_HgConvertMemberList_DTO.UPLOAD_TEMPDataTable dt = OPT16_DTO.UPLOAD_TEMP;

        OPT16_Facade facade = new OPT16_Facade();

        try
        {
            string UploadBatchNo = "";   //上傳批號_UUID
            if (QryFUNCID == "OPT13_1" || QryFUNCID == "OPT15")  //因為"UploadBatchNo"是用折扣料號的ACTIVITY_ID,故需先刪除UPLOAD_TEMP table BATCH_NO=ACTIVITY_ID的資料
            {                            //及HG_CONVERT_MEMBER_LIST_TEMP table UPLOAD_BATCH_NO=ACTIVITY_ID。否則之前匯入的資料會一直被撈出來
                UploadBatchNo = QryUUID;
                facade.Delete_UPLOAD_TEMP(UploadBatchNo);

            }
            else
                UploadBatchNo = GuidNo.getUUID();
            this.hdUploadBatchNo.Value = UploadBatchNo;

            foreach (DataRow Importdr in dtExcel.Rows)
            {

                OPT16_HgConvertMemberList_DTO.UPLOAD_TEMPRow dr = dt.NewUPLOAD_TEMPRow();

                dr.SID = GuidNo.getUUID();
                dr.BATCH_NO = this.hdUploadBatchNo.Value;   //上傳批號_UUID
                dr.USER_ID = logMsg.CREATE_USER;
                dr.FINC_ID = this.QryFUNCID + "_IMPORT";
                dr.F1 = StringUtil.CStr(Importdr["折扣料號"]);
                dr.F2 = StringUtil.CStr(Importdr["客戶門號"]);
                dt.Rows.Add(dr);

                OPT16_DTO.AcceptChanges();
            }

            //更新資料庫
            facade.AddNew_UploadTemp(OPT16_DTO, this.hdUploadBatchNo.Value, QryFUNCID, QryDISCOUNTID);
            DataTable _dt = facade.Query_HgConvertMemberListTmp(this.hdUploadBatchNo.Value);
            int successCount = _dt.Select("EXCEPTIOB_CAUSE is null").Length;
            int errorCount = _dt.Select("EXCEPTIOB_CAUSE is not null").Length;
            this.gvMaster.DataSource = _dt;
            this.gvMaster.DataBind();
            lblSuccess.Text = StringUtil.CStr(successCount);
            lblFail.Text = StringUtil.CStr(errorCount);

            if (facade.Query_HgConvertMemberListTmpIsEXCEPTIOB_CAUSE(this.hdUploadBatchNo.Value) > 0)
                this.btnUpload.Enabled = false;
            else
                this.btnUpload.Enabled = true;
        }
        catch
        {
            ScriptManager.RegisterClientScriptBlock(this, GetType(), "AlertMessage", "alert('匯入的內容格式不正確!!');", true);
        }

    }

    private void SaveTempDataDiscount(DataTable dtExcel)
    {
        OPT16_HgConvertMemberList_DTO OPT16_DTO = new OPT16_HgConvertMemberList_DTO();
        OPT16_HgConvertMemberList_DTO.UPLOAD_TEMPDataTable dt = OPT16_DTO.UPLOAD_TEMP;

        OPT16_Facade facade = new OPT16_Facade();

        string UploadBatchNo = "";   //上傳批號_UUID
        if (QryFUNCID == "OPT13_1")  //因為"UploadBatchNo"是用折扣料號的ACTIVITY_ID,故需先刪除UPLOAD_TEMP table BATCH_NO=ACTIVITY_ID的資料
        {                            //及HG_CONVERT_MEMBER_LIST_TEMP table UPLOAD_BATCH_NO=ACTIVITY_ID。否則之前匯入的資料會一直被撈出來
            UploadBatchNo = QryUUID;
            facade.Delete_UPLOAD_TEMP(UploadBatchNo);
        }
        else
            UploadBatchNo = GuidNo.getUUID();
        this.hdUploadBatchNo.Value = UploadBatchNo;

        foreach (DataRow Importdr in dtExcel.Rows)
        {

            OPT16_HgConvertMemberList_DTO.UPLOAD_TEMPRow dr = dt.NewUPLOAD_TEMPRow();

            dr.SID = GuidNo.getUUID();
            dr.BATCH_NO = this.hdUploadBatchNo.Value;   //上傳批號_UUID
            dr.USER_ID = logMsg.CREATE_USER;
            dr.FINC_ID = this.QryFUNCID + "_IMPORT";
            dr.F1 = StringUtil.CStr(Importdr["折扣料號"]);
            dr.F2 = StringUtil.CStr(Importdr["客戶門號"]);
            dt.Rows.Add(dr);

            OPT16_DTO.AcceptChanges();
        }

        //更新資料庫
        facade.AddNew_UploadTemp(OPT16_DTO, this.hdUploadBatchNo.Value, QryFUNCID, QryDISCOUNTID);

        this.gvMaster.DataSource = facade.Query_HgConvertMemberListTmp(this.hdUploadBatchNo.Value);
        this.gvMaster.DataBind();

   }

    protected void btnUpload_Click(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(this.hdUploadBatchNo.Value))
        {
            OPT16_Facade facade = new OPT16_Facade();
            //更新資料庫
            facade.Delete_HgConvertMemberList(QryUUID, QryFUNCID);
            facade.AddNew_HgConvertMemberList(this.hdUploadBatchNo.Value, this.QryFUNCID);

            Session["UploadBatchNo"] = this.hdUploadBatchNo.Value;
            SetReturnValue(this.hdUploadBatchNo.Value);
        }

        SetReturnOKScript();

    }

    protected void gvMaster_HtmlDataCellPrepared(object sender, ASPxGridViewTableDataCellEventArgs e)
    {

        if (e.DataColumn.FieldName == "EXCEPTIOB_CAUSE")
        {
            if (!string.IsNullOrEmpty(StringUtil.CStr(e.GetValue("EXCEPTIOB_CAUSE"))))
            {
                e.Cell.Style[HtmlTextWriterStyle.Color] = "red";
                //if (this.btnUpload.Enabled)
                //{
                //    this.btnUpload.Enabled = false;
                //}
            }
        }
    }
    protected void gvMaster_PageIndexChanged(object sender, EventArgs e)
    {
        OPT16_Facade facade = new OPT16_Facade();
        this.gvMaster.DataSource = facade.Query_HgConvertMemberListTmp(this.hdUploadBatchNo.Value);
        this.gvMaster.DataBind();
    }
    protected void btnClear_Click(object sender, EventArgs e)
    {
        //取得空的資料表
        gvMaster.DataSource = new OPT16_HgConvertMemberList_DTO().HG_CONVERT_MEMBER_LIST_TEMP;
        gvMaster.DataBind();
        btnUpload.Enabled = false;
    }
}
