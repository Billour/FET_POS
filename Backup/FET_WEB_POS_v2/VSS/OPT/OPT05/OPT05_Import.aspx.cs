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
using System.Data.OracleClient;
using Advtek.Utility;
using FET.POS.Model.DTO;
using FET.POS.Model.Facade;
using FET.POS.Model.Facade.FacadeImpl;
using FET.POS.Model.Common;
public partial class VSS_OPT_OPT05_OPT05_Import : Popup
{
    /// <summary>
    /// 使用者
    /// </summary>
    private string UserName = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        UserName = logMsg.OPERATOR;
        gvMaster.Settings.ShowVerticalScrollBar = true;
        gvMaster.Settings.VerticalScrollBarStyle = GridViewVerticalScrollBarStyle.Virtual;
    }

    protected void btnImport_Click(object sender, EventArgs e)
    {
        if (!this.FileUpload1.HasFile) { return; }

        String fileName = this.FileUpload1.FileName;

        if ((fileName.Trim().Substring(fileName.Trim().IndexOf('.')) != ".xls"))
        {
            ScriptManager.RegisterClientScriptBlock(this, typeof(string), "EXCEL匯入", "alert('檔案格式錯誤!');", true);
            return;
        }
       
        HSSFWorkbook workbook = new HSSFWorkbook(this.FileUpload1.FileContent);
        HSSFSheet sheet = workbook.GetSheetAt(0) as HSSFSheet;
        HSSFRow headerRow = sheet.GetRow(0) as HSSFRow;
        DataTable table = new DataTable();
        DataTable checktable = new DataTable();
        checktable.Columns.Add("StoreNo");
        checktable.Columns.Add("Year");
        checktable.Columns.Add("UseType");
        OPT05_HqInvoiceAssign_DTO ds = new OPT05_HqInvoiceAssign_DTO();

        //匯入暫存用Table
        OPT05_HqInvoiceAssign_DTO.HQ_INVOICE_ASSIGN_TEMPDataTable dt = ds.HQ_INVOICE_ASSIGN_TEMP;

        ////本批次號
        //string BATCH_NO = GuidNo.getUUID();
        //string tolStore_No = "";
        //string tolType = "";
        //string tolYear = "";

        int cellCount = headerRow.LastCellNum;
        int rowCount = sheet.LastRowNum;

        for (int i = headerRow.FirstCellNum; i < cellCount; i++)
        {
            string colName = headerRow.GetCell(i).StringCellValue;
            if (!string.IsNullOrEmpty(colName))
            {
                DataColumn column = new DataColumn();
                table.Columns.Add(column);
            }
        }

        cellCount = table.Columns.Count;

        if (cellCount != 11)
        {
            ScriptManager.RegisterClientScriptBlock(this, typeof(string), "EXCEL匯入", "alert('匯入格式錯誤!');", true);
            return;
        }


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

        SaveTempData(table);  //將匯入的資料存入UpLoad_Temp Table中


        #region 2011/03/07 Tina：註解舊程式，將欄位值檢查移至SP
        /*
        //將exl的每個值抓出來
        string check1 = "";
        string tempstoreno = "";
        string checkstore = "",checkyear="",checktype="";
        string changestore = "", changeyear = "", changetype = "";
        string  tempyear = "";
        string tempusetype = "";
        string templeadecode, tempinitno, tempendno;
        for (int i = (sheet.FirstRowNum + 1); i <= sheet.LastRowNum; i++)
        {  HSSFRow row = sheet.GetRow(i) as HSSFRow;
                DataRow dataRow = table.NewRow();

                for (int j = row.FirstCellNum; j < cellCount; j++)
                {
                    if (row.GetCell(j) != null)
                        dataRow[j] = StringUtil.CStr(row.GetCell(j));
                }

                table.Rows.Add(dataRow);
        }
        OracleConnection objConn = null;
        objConn = OracleDBUtil.GetConnection();
        foreach(DataRow row in table.Rows)
        {   
            DataRow drr = checktable.NewRow();
            string ROW_EXCEPTION_CAUSE = "";
            string STORE_NO = StringUtil.CStr(row[0]);
            string Type = StringUtil.CStr(row[1]);
            string Year = StringUtil.CStr(row[2]);

            if (string.IsNullOrEmpty(STORE_NO)) { break; }
           
            if (tempstoreno != STORE_NO)
            {
                changestore = "0";
            }
            if (tolStore_No.Trim().Length == 0)
            {
                tolStore_No = STORE_NO;
            }
            else
            {
                tolStore_No += "," + STORE_NO;
               
            }

            if (STORE_NO != tempstoreno && tempstoreno != "")
            {
                check1 = "1";
            }
            else
            {
                tempstoreno = STORE_NO;
            }
            drr[0] = STORE_NO;
            
            string yyy = StringUtil.CStr(DateTime.Now.Year);
            string xxx = StringUtil.CStr(DateTime.Now.Month);
            string USE_TYPE = (StringUtil.CStr(row[1]) == "連線") ? "1" : "2";
            drr[2] = USE_TYPE;
            string USE_TYPE_NAME = StringUtil.CStr(row[1]);
            string YEAR = StringUtil.CStr(row[2]);
            
            if (tempyear == YEAR)
            {
                checkyear = "1";
            }
            else
            {
                changeyear = "1";
            }

            if (tolType.Trim().Length == 0)
            {
                tolType = StringUtil.CStr(row[1]);
            }
            else
            {
                tolType += "," + StringUtil.CStr(row[1]);
            }

            if (tolYear.Trim().Length == 0)
            {
                tolYear = YEAR;
            }
            else
            {
                tolYear += "," + YEAR;
            }

            if (YEAR.Trim().Length == 0 || YEAR.Trim().Length != 4)
            {
                ROW_EXCEPTION_CAUSE += "年度格式錯誤";
            }
            else
            {
                try
                {
                    int intYear = int.Parse(YEAR);
                    if(intYear < Convert.ToInt32(YEAR))
                    {
                       ROW_EXCEPTION_CAUSE += "匯入年度不可小於系統年度";
                    }
                }
                catch (Exception ce)
                {
                    ROW_EXCEPTION_CAUSE += "年度格式錯誤";
                }
            }
            drr[1] = YEAR;
            foreach (DataRow checkrow in checktable.Rows)
            {
                if (StringUtil.CStr(checkrow[0]) == STORE_NO && StringUtil.CStr(checkrow[1]) == YEAR && StringUtil.CStr(checkrow[2]) == USE_TYPE)
                {
                    ROW_EXCEPTION_CAUSE += "門市代號重複";
                }
            }
            string LEADER_CODE = "";
            string INIT_NO = StringUtil.CStr(row[9]);
            string END_NO = StringUtil.CStr(row[10]);
            string S_YM = "";
            string E_YM = "";
            checktable.Rows.Add(drr);
            for (int j = 3; j <= 8; j++)
            {
                string EXCEPTION_CAUSE = "";
                if (StringUtil.CStr(row[j]) == null) { continue; }

                LEADER_CODE = StringUtil.CStr(row[j]);

                if (string.IsNullOrEmpty(LEADER_CODE)) { continue; }

                string S_USE_YM = YEAR + "/";
                string E_USE_YM = YEAR + "/";
                tempyear = YEAR;
                
                //取欄位值
                switch (j)
                {
                    case 3:
                        S_USE_YM += "01";
                        E_USE_YM += "02";
                        S_YM = "01";
                        E_YM = "02";
                        break;
                    case 4:
                        S_USE_YM += "03";
                        E_USE_YM += "04";
                        S_YM ="03";
                        E_YM ="04";
                        break;
                    case 5:
                        S_USE_YM += "05";
                        E_USE_YM += "06";
                        S_YM = "05";
                        E_YM = "06";
                        break;
                    case 6:
                        S_USE_YM += "07";
                        E_USE_YM += "08";
                        S_YM = "07";
                        E_YM = "08";
                        break;
                    case 7:
                        S_USE_YM += "09";
                        E_USE_YM += "10";
                        S_YM = "09";
                        E_YM = "10";
                        break;
                    case 8:
                        S_USE_YM += "11";
                        E_USE_YM += "12";
                        S_YM = "11";
                        E_YM = "12";
                        break;
                    default:
                        break;
                }

                OPT05_HqInvoiceAssign_DTO.HQ_INVOICE_ASSIGN_TEMPRow dr = 
                    dt.NewHQ_INVOICE_ASSIGN_TEMPRow();

                dr.ASSIGN_ID = GuidNo.getUUID();
                dr.BATCH_NO = BATCH_NO;
                dr.USE_TYPE = USE_TYPE;
                if ("1" == dr.USE_TYPE)
                {
                    dr.INVOICE_TYPE_ID = "01";
                }
                else
                {
                    dr.INVOICE_TYPE_ID = "02";
                }
                
                tempusetype = dr.INVOICE_TYPE_ID;
                dr.S_USE_YM = S_USE_YM;
                dr.E_USE_YM = E_USE_YM;

                if (dr.S_USE_YM.Length != 7)
                {
                    EXCEPTION_CAUSE += "所屬年月錯誤.";
                }

                if (dr.S_USE_YM.Length == 7)
                {
                    if (Convert.ToInt32(yyy) == Convert.ToInt32(YEAR))
                    {
                        if (Convert.ToInt32(S_YM) < Convert.ToInt32(xxx) || Convert.ToInt32(E_YM) < Convert.ToInt32(xxx))
                        {
                            if (Convert.ToInt32(S_YM) != Convert.ToInt32(xxx) && Convert.ToInt32(E_YM) != Convert.ToInt32(xxx))
                            {
                                EXCEPTION_CAUSE += "匯入月份不可小於系統月份。";
                            }
                        }
                    }

                }
                dr.LEADER_CODE = LEADER_CODE.ToUpper();

                if (dr.LEADER_CODE.Length != 2)
                {
                    if (dr.LEADER_CODE.Length > 2)
                    {
                        dr.LEADER_CODE = dr.LEADER_CODE.Substring(0,2);
                    }

                    EXCEPTION_CAUSE += "字軌錯誤.";
                }
                templeadecode = LEADER_CODE.ToUpper();
                dr.INIT_NO = INIT_NO;

                if (dr.INIT_NO.Length != 8)
                {
                    if (dr.INIT_NO.Length > 8)
                    {
                        dr.INIT_NO = dr.INIT_NO.Substring(0, 8);
                    }

                    EXCEPTION_CAUSE += "起始編號錯誤.";
                }
                tempinitno = INIT_NO;
                dr.END_NO = END_NO;

                if (dr.END_NO.Length != 8)
                {
                    if (dr.END_NO.Length > 8)
                    {
                        dr.END_NO = dr.END_NO.Substring(0, 8);
                    }

                    EXCEPTION_CAUSE += "終止編號錯誤.";
                }
                tempendno = END_NO;
              
              
                changetype = "0";
               
                if (EXCEPTION_CAUSE == "")
                {
                    if (OPT05_Facade.CheckInvoiceNO_Import(
                      tempstoreno,
                     tempusetype,
                     templeadecode,
                      Convert.ToDateTime(S_USE_YM).ToString("yyyy/MM"),
                      Convert.ToDateTime(E_USE_YM).ToString("yyyy/MM"),
                      tempinitno,
                      tempendno, objConn,dr.BATCH_NO,dr.ASSIGN_ID
                      ))
                    {
                        EXCEPTION_CAUSE += "匯入資料同一所屬年度區間中不可有二筆以上的相同字軌、重複編號區間的發票設定。";
                    }

                }

                dr.SHEET_COUNT = Convert.ToDecimal(dr.END_NO) - Convert.ToDecimal(dr.INIT_NO) + 1;
                dr.STORE_NO = STORE_NO;

                if (ROW_EXCEPTION_CAUSE.Trim().Length != 0 || EXCEPTION_CAUSE != "")
                {
                    dr.STATUS = "F";
                    dr.EXCEPTION_CAUSE = ROW_EXCEPTION_CAUSE + EXCEPTION_CAUSE;
                }
                else
                {
                    dr.STATUS = "C";
                }

                dt.AddHQ_INVOICE_ASSIGN_TEMPRow(dr);
               
               
            }
            checktype = "";
            checkstore = "";
            checkyear = "";
            changestore = "";
            changeyear = "0";
        }
      
        workbook = null;
        sheet = null;

        ds.AcceptChanges();

        try
        {
            new OPT05_Facade().ImportHeadQuarterInvoice(ds);
        }
        catch (Exception ex)
        {
            throw ex;
        }
        */
        #endregion
    }

    /// <summary>
    /// 將匯入的資料存入Upload_Temp Table中
    /// </summary>
    /// <param name="dtExcel">匯入資料</param>
    private void SaveTempData(DataTable dtExcel)
    {

        OPT05_HqInvoiceAssign_DTO OPT05_DTO = new OPT05_HqInvoiceAssign_DTO();
        OPT05_HqInvoiceAssign_DTO.UPLOAD_TEMPDataTable dt = OPT05_DTO.UPLOAD_TEMP;

        OPT05_Facade facade = new OPT05_Facade();

        string UploadBatchNo = GuidNo.getUUID();   //上傳批號_UUID
        ViewState["BATCH_NO"] = UploadBatchNo;

        foreach (DataRow Importdr in dtExcel.Rows)
        {

            OPT05_HqInvoiceAssign_DTO.UPLOAD_TEMPRow dr = dt.NewUPLOAD_TEMPRow();

            dr.SID = GuidNo.getUUID();
            dr.BATCH_NO = UploadBatchNo;   //上傳批號_UUID
            dr.USER_ID = logMsg.OPERATOR;
            dr.FINC_ID = "OPT05_IMPORT";
            dr.F1 = StringUtil.CStr(Importdr[0]);
            dr.F2 = StringUtil.CStr(Importdr[1]);
            dr.F3 = StringUtil.CStr(Importdr[2]);
            dr.F4 = StringUtil.CStr(Importdr[3]);
            dr.F5 = StringUtil.CStr(Importdr[4]);
            dr.F6 = StringUtil.CStr(Importdr[5]);
            dr.F7 = StringUtil.CStr(Importdr[6]);
            dr.F8 = StringUtil.CStr(Importdr[7]);
            dr.F9 = StringUtil.CStr(Importdr[8]);
            dr.F10 = StringUtil.CStr(Importdr[9]);
            dr.F11 = StringUtil.CStr(Importdr[10]);
            dt.Rows.Add(dr);

            OPT05_DTO.AcceptChanges();
        }

        //更新資料庫
        facade.AddNew_UploadTemp(OPT05_DTO, UploadBatchNo);

        DataTable dtNew = new OPT05_PageHelper().GetImportTempData(UploadBatchNo);

        this.gvMaster.DataSource = dtNew;
        this.gvMaster.DataBind();

        int success_count = 0;
        int failed_count = 0;

        foreach (DataRow dr in dtNew.Rows)
        {
            if (string.IsNullOrEmpty(StringUtil.CStr(dr["EXCEPTION_CAUSE"])))
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
            btnCommit.Visible = false;
            btnCalcel.Visible = false;
        }
        else
        {
            btnCommit.Visible = true;
            btnCalcel.Visible = true;
        }

        Literal6.Text = StringUtil.CStr(success_count);
        Literal7.Text = StringUtil.CStr(failed_count);

    }

    protected void gvMaster_HtmlDataCellPrepared(object sender, ASPxGridViewTableDataCellEventArgs e)
    {
        if (e.DataColumn.FieldName == "EXCEPTION_CAUSE")
        {
            e.Cell.Style[HtmlTextWriterStyle.Color] = "red";
        }
        else if (e.DataColumn.FieldName == "USE_TYPE")
        {
            e.Cell.Text = (StringUtil.CStr(e.CellValue) == "1")? "連線":"離線";
        }
    }

    /// <summary>
    /// 確認匯入
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnCommit_Click(object sender, EventArgs e)
    {
        try
        {
            new OPT05_Facade().ImportHeadQuarterInvoice(StringUtil.CStr(ViewState["BATCH_NO"]), UserName);
        }
        catch (Exception ex)
        {
            throw ex;
        }

        SetReturnValue(string.Empty);
        ClientScript.RegisterClientScriptBlock(this.GetType(), "Import", "<script>alert('匯入完成，請重新查詢資料!');</script>");
    }
}
