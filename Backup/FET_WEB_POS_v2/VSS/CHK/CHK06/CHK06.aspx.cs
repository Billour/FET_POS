using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Globalization;
using System.Collections;
using DevExpress.Web;
using DevExpress.Web.ASPxGridView;
using DevExpress.Web.ASPxPager;
using NPOI.HSSF.UserModel;
using System.Data.OleDb;

using Advtek.Utility;
using FET.POS.Model.DTO;
using FET.POS.Model.Facade;
using FET.POS.Model.Facade.FacadeImpl;
using FET.POS.Model.Common;

public partial class VSS_CHK_CHK06 : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack && !Page.IsCallback)
        {

            if (logMsg.STORENO != StringUtil.CStr(System.Web.Configuration.WebConfigurationManager.AppSettings["DefaultStore"]))
            {
                ScriptManager.RegisterClientScriptBlock(this, GetType(), "AlertMessage", "alert('非總部人員無法使用此功能!!');", true);
                this.gvMaster.Enabled = false;
                this.gvDetail.Enabled = false;
                this.txtTRADE_DATE_S.Enabled = false;
                this.txtTRADE_DATE_E.Enabled = false;
                this.btnCancel.Enabled = false;
                this.btnImport.Enabled = false;
                this.btnSave.Enabled = false;
                this.btnClear.Enabled = false;
                this.FileUpload1.Enabled = false;
                this.FileUpload2.Enabled = false;
                return;
            }
            else
            {
                this.hdIsSave.Value = "0";

                gvMaster.DataSource = new DataTable();
                gvMaster.DataBind();

                gvDetail.DataSource = new DataTable();
                gvDetail.DataBind();
            }



        }
    }

    protected void BindMasterData()
    {
        if (this.hdIsSave.Value == "1")
        {
            gvMaster.DataSource = new CHK06_Facade().Query_BankCashM(this.hdUploadBatchNo.Value);
        }
        else
        {
            gvMaster.DataSource = new CHK06_Facade().Query_BankCashTempM(this.hdUploadBatchNo.Value);
        }
        gvMaster.DataBind();
    }

    protected void BindDetailData()
    {
        //DataTable dt = (DataTable)gvMaster.DataSource;

        if (gvMaster.FocusedRowIndex > -1)
        {
            if (this.hdIsSave.Value == "1")
            {
                gvDetail.DataSource = new CHK06_Facade().Query_BankCashD(this.hdUploadBatchNo.Value, StringUtil.CStr(this.gvMaster.GetRowValues(gvMaster.FocusedRowIndex, "TRADE_DATE")));
            }
            else
            {
                gvDetail.DataSource = new CHK06_Facade().Query_BankCashTempD(this.hdUploadBatchNo.Value, StringUtil.CStr(this.gvMaster.GetRowValues(gvMaster.FocusedRowIndex, "TRADE_DATE")));
            }
            gvDetail.DataBind();
        }
    }

    #region Button 觸發事件

    protected void btnImport_Click(object sender, EventArgs e)
    {
        gvMaster.DataSource = new DataTable();
        gvMaster.DataBind();

        gvDetail.DataSource = new DataTable();
        gvDetail.DataBind();

        CHK06_Facade CHK06_Facade = new CHK06_Facade();
        string SET_DAY = CHK06_Facade.GetDay();
        string SDATE = txtTRADE_DATE_S.Text;
        string EDATE = txtTRADE_DATE_E.Text;
        string today = DateTime.Now.ToString("yyyy/mm/dd");
        string mm = StringUtil.CStr(DateTime.Now.Month);
        string md = DateTime.Now.ToString("mm/dd");
        string checkdd = "";
        string SMonth = SDATE.Substring(5, 2);

        string checkmd = mm + "/" + "01";
        DateTime test1 = Convert.ToDateTime(checkmd).AddDays(Convert.ToInt32(SET_DAY));
        DateTime todaydate = DateTime.Now;
        int check = DateTime.Compare(test1, todaydate);

        int checkm = Math.Abs(Convert.ToInt32(mm) - Convert.ToInt32(SMonth));
        if (check != 1)
        {
            checkdd = "1";
        }
        if (checkm == 1 && checkdd=="1")
        {
            ScriptManager.RegisterClientScriptBlock(this, GetType(), "AlertMessage", "alert('無法查詢跨月資料!!');", true);
            return;
        }

        if (checkm > 1 && checkdd!="1")
        {
            ScriptManager.RegisterClientScriptBlock(this, GetType(), "AlertMessage", "alert('僅允許查詢上個月之資料!!');", true);
            return;
        }



        string UploadBatchNo = GuidNo.getUUID();   //上傳批號_UUID
        this.hdUploadBatchNo.Value = UploadBatchNo;
        try
        {

            DataTable dtBankAccounted = UpLoadFile(this.FileUpload1, "合庫");
            DataTable dtNcccCredit = UpLoadFile(this.FileUpload2, "NCCC信用卡");
            if (dtBankAccounted != null)
            {
                if (chkBankAccounted(dtBankAccounted))
                {
                    InsertToTempTable(dtBankAccounted);
                }
                else
                {
                    return;
                }
            }

            if (dtNcccCredit != null)
            {
                if (chkNcccCredit(dtNcccCredit))
                {
                    InsertToTempTable(dtNcccCredit);
                }
                else
                {
                    return;
                }
            }

            new CHK06_Facade().AddNew_BankCashTemp(this.hdUploadBatchNo.Value, this.txtTRADE_DATE_S.Text, this.txtTRADE_DATE_E.Text);
            BindMasterData();
            this.gvMaster.FocusedRowIndex = -1;
            this.gvMaster.PageIndex = 0;
            BindDetailData();
            this.gvDetail.PageIndex = 0;
            
            //取得所有明細資料再進一步判斷異常原因, 若有"帳差不平以外"的異常原因，就不允許[資料儲存]
            DataTable dt = new CHK06_Facade().Query_BankCashTempD(this.hdUploadBatchNo.Value, "");
            DataRow[] drs = dt.Select("FLG_TO_SAVE='N'");
            this.btnSave.Enabled = true;
            if (drs.Length > 0)
            {
                this.btnSave.Enabled = false;
            }
        }
        catch (Exception)
        {
            ScriptManager.RegisterClientScriptBlock(this, typeof(string), "EXCEL匯入", "alert('匯入格式錯誤或檔案大小超過上限500K');", true);
        }


    }

    //檢查合庫資料
    protected bool chkBankAccounted(DataTable dtBankAccounted)
    {
        bool chkData = true;
        try
        {
            foreach (DataRow dr in dtBankAccounted.Rows)
            {
                string strDate = StringUtil.CStr(dr["入帳日期"]);
                if (strDate.Length != 7)
                {
                    chkData = false;
                    break;
                }

                strDate = StringUtil.CStr((int.Parse(strDate.Substring(0, 3)) + 1911)) + "/" + strDate.Substring(3, 2) + "/" + strDate.Substring(5, 2);
                DateTime DT = Convert.ToDateTime(strDate);

            }
        }
        catch (Exception)
        {
            chkData = false;
        }

        this.lblBankAccountedError.Visible = !chkData;

        return chkData;
    }

    //檢查NCCC信用卡資料
    protected bool chkNcccCredit(DataTable dtNcccCredit)
    {
        bool chkData = true;
        try
        {
            foreach (DataRow dr in dtNcccCredit.Rows)
            {
                string strDate = StringUtil.CStr(dr["日期"]);
                if (strDate.Length != 7)
                {
                    chkData = false;
                    break;
                }

                strDate = StringUtil.CStr((int.Parse(strDate.Substring(0, 3)) + 1911)) + "/" + strDate.Substring(3, 2) + "/" + strDate.Substring(5, 2);
                DateTime DT = Convert.ToDateTime(strDate);
                int tmp = int.Parse(StringUtil.CStr(dr["請款金額"]).Replace(",", ""));
            }

        }
        catch (Exception)
        {
            chkData = false;
        }
        this.lblNcccCreditError.Visible = !chkData;

        return chkData;
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(this.hdUploadBatchNo.Value))
        {
            new CHK06_Facade().AddNew_BankCash(this.hdUploadBatchNo.Value);
            this.hdIsSave.Value = "1";
        }
    }

    #endregion

    #region gvMaster 觸發事件

    protected void gvMaster_PageIndexChanged(object sender, EventArgs e)
    {
        BindMasterData();
    }

    protected void gvMaster_FocusedRowChanged(object sender, EventArgs e)
    {
        BindDetailData();
    }

    protected void gvMaster_HtmlDataCellPrepared(object sender, ASPxGridViewTableDataCellEventArgs e)
    {
        if (e.DataColumn.FieldName == "EXCEPTION_CAUSE")
        {
            if (!string.IsNullOrEmpty(StringUtil.CStr(e.GetValue("EXCEPTION_CAUSE"))))
            {
                e.Cell.Style[HtmlTextWriterStyle.Color] = "red";
            }
        }
    }

    #endregion

    #region gvDetail 觸發事件

    protected void gvDetail_PageIndexChanged(object sender, EventArgs e)
    {
        BindDetailData();
    }

    protected void gvDetail_HtmlDataCellPrepared(object sender, ASPxGridViewTableDataCellEventArgs e)
    {
        if (e.DataColumn.FieldName == "EXCEPTION_CAUSE")
        {
            if (!string.IsNullOrEmpty(StringUtil.CStr(e.GetValue("EXCEPTION_CAUSE"))))
            {
                e.Cell.Style[HtmlTextWriterStyle.Color] = "red";
            }
        }

    }

    #endregion

    /// <summary>
    /// 取得Excel的資料
    /// </summary>
    /// <param name="xlsPath">檔案路徑</param>
    /// <param name="sheetName">Sheet Name</param>
    /// <returns>資料表</returns>
    private DataTable getDataTable(FileUpload File, string sheetName)
    {

        DataTable TmpTable = new DataTable();
        try
        {
            HSSFWorkbook workbook = new HSSFWorkbook(File.FileContent);
            HSSFSheet sheet;
            HSSFRow headerRow;
            try
            {
                sheet = workbook.GetSheet(sheetName) as HSSFSheet;
                headerRow = sheet.GetRow(0) as HSSFRow;
            }
            catch (Exception)
            {
                sheet = workbook.GetSheetAt(0) as HSSFSheet;
                headerRow = sheet.GetRow(0) as HSSFRow;
            }

            int cellCount = headerRow.LastCellNum;
            int rowCount = sheet.LastRowNum;



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

        }
        catch //(Exception ex)
        {
            // ScriptManager.RegisterClientScriptBlock(this, typeof(string), "EXCEL匯入", "alert('匯入格式錯誤!');", true);
            throw;

        }

        return TmpTable;
    }

    /// <summary>
    /// 檔案上傳
    /// </summary>
    /// <param name="File">FileUpload Control</param>
    /// <param name="FileName">應上傳的檔案名稱</param>
    private DataTable UpLoadFile(FileUpload File, string FileName)
    {
        DataTable table = null;
        if (File.HasFile)
        {
            //table = getDataTable(File.PostedFile.FileName, "明細");
            table = getDataTable(File, "明細");
            if (table.Rows.Count > 0)
            {
                if (FileName == "合庫")
                {
                    if (!table.Columns.Contains("入帳日期") || !table.Columns.Contains("門市編號") || !table.Columns.Contains("小計"))
                    {
                        this.lblBankAccountedError.Visible = true;
                        table = null;
                    }
                    else
                    {
                        this.lblBankAccountedError.Visible = false;
                        table.TableName = "dtBankAccounted";
                    }
                }
                else
                {
                    if (!table.Columns.Contains("日期") || !table.Columns.Contains("門市代號") || !table.Columns.Contains("請款金額"))
                    {
                        this.lblNcccCreditError.Visible = true;
                        table = null;
                    }
                    else
                    {
                        this.lblNcccCreditError.Visible = false;
                        table.TableName = "dtNcccCredit";
                    }

                }//end if (FileName == "合庫")
            }//end if (table.Rows.Count > 0)
        }//end if (File.HasFile) 

        return table;
    }

    private void InsertToTempTable(DataTable table)
    {
        SaveTempData(table);  //將匯入的資料存入Temp Table中
    }

    /// <summary>
    /// 將匯入的資料存入Temp Table中
    /// </summary>
    /// <param name="dtExcel">匯入資料</param>
    private void SaveTempData(DataTable dtExcel)
    {
        CHK06_BankCash_DTO CHK06_DTO = new CHK06_BankCash_DTO();
        CHK06_BankCash_DTO.UPLOAD_TEMPDataTable dt = CHK06_DTO.UPLOAD_TEMP;

        CHK06_Facade facade = new CHK06_Facade();

        foreach (DataRow Importdr in dtExcel.Rows)
        {
            string strType = "";
            string strDate = "";
            string strStore = "";
            string strAmount = "";
            if (dtExcel.TableName == "dtBankAccounted")   //合庫入帳
            {
                if (!string.IsNullOrEmpty(StringUtil.CStr(Importdr["入帳日期"])))
                {
                    try
                    {
                        strDate = StringUtil.CStr(Importdr["入帳日期"]);
                        strDate = StringUtil.CStr((int.Parse(strDate.Substring(0, 3)) + 1911)) + "/" + strDate.Substring(3, 2) + "/" + strDate.Substring(5, 2);
                        DateTime DT = Convert.ToDateTime(strDate);
                    }
                    catch
                    {

                        this.lblBankAccountedError.Visible = true;
                        return;
                    }
                }
                strType = "0";
                // strStore = CHK06_PageHelper.GetStoreNoByACCOUNT(StringUtil.CStr(Importdr["虛擬帳號"]));  //虛擬帳號透過STORE_ACCOUNT取得門市編號
                strStore = StringUtil.CStr(Importdr["門市編號"]);
                strAmount = StringUtil.CStr(Importdr["小計"]).Replace(",", "");
            }
            else //NCCC信用卡入帳
            {
                if (!string.IsNullOrEmpty(StringUtil.CStr(Importdr["日期"])))
                {
                    try
                    {
                        strDate = StringUtil.CStr(Importdr["日期"]);
                        strDate = StringUtil.CStr((int.Parse(strDate.Substring(0, 3)) + 1911)) + "/" + strDate.Substring(3, 2) + "/" + strDate.Substring(5, 2);
                        DateTime DT = Convert.ToDateTime(strDate);
                    }
                    catch
                    {
                        this.lblNcccCreditError.Visible = true;
                        return;
                    }
                }
                strType = "1";
                strStore = StringUtil.CStr(Importdr["門市代號"]);
                strAmount = StringUtil.CStr(Importdr["請款金額"]).Replace(",", "");
            }

            if (!string.IsNullOrEmpty(strDate))
            {
                CHK06_BankCash_DTO.UPLOAD_TEMPRow dr = dt.NewUPLOAD_TEMPRow();

                dr.SID = GuidNo.getUUID();
                dr.BATCH_NO = this.hdUploadBatchNo.Value;   //上傳批號_UUID
                dr.USER_ID = logMsg.OPERATOR;
                dr.FINC_ID = "CHK06_IMPORT";
                dr.STATUS = "1";               //失效 0:有效
                dr.F1 = strType;
                dr.F2 = Convert.ToDateTime(strDate).ToString("yyyy/MM/dd");
                dr.F3 = strStore;
                dr.F4 = strAmount;

                dt.Rows.Add(dr);
                CHK06_DTO.AcceptChanges();
            }
        }

        //更新資料庫
        facade.AddNew_UploadTemp(CHK06_DTO, this.hdUploadBatchNo.Value);

    }

}
