using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using NPOI.HSSF.UserModel;
using Advtek.Utility;
using FET.POS.Model.Facade.FacadeImpl;
using FET.POS.Model.DTO;
using DevExpress.Web.ASPxGridView;
using System.Collections.Specialized;

public partial class VSS_DIS_DIS01_DIS01_Import_byDisType : Popup
{
    /// <summary>
    /// 折扣料號類別代碼
    /// </summary>
    private string qDisType
    {
        get
        {
            string DisType = "";
            //**2011/04/25 Tina：前頁傳遞參數時，會先以加密處理，在此要解密。
            if (!string.IsNullOrEmpty(Request.QueryString["Param"]))
            {
                NameValueCollection qscoll = Utils.Param_UrlDecode(Request.QueryString["Param"]);
                foreach (string key in qscoll.AllKeys)
                {
                    if (key == "cbDisType")
                    {
                        DisType = string.Join(",", qscoll.GetValues(key));
                        break;
                    }
                }
            }

            return DisType;

           //return (Request.QueryString["cbDisType"] ?? "");
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        hdDisType.Value = qDisType;

        if (!IsPostBack && IsCallback)
        {
            ViewState.Clear();
        }
    }

    protected void btnImport_Click(object sender, EventArgs e)
    {
        DIS01_Facade Facade = new DIS01_Facade();
        DIS01_DiscountMasterDataSet_DTO dto = new DIS01_DiscountMasterDataSet_DTO();

        if (!this.FileUpload.HasFile) { return; }
        String fileName = this.FileUpload.FileName;

        string[] Names = fileName.Trim().Split('.');

        if (Names[Names.Length-1].ToLower() != "xls")
        {
            ScriptManager.RegisterClientScriptBlock(this, typeof(string), "EXCEL匯入", "alert('檔案格式錯誤!');", true);
            return;
        }
        HSSFWorkbook workbook = new HSSFWorkbook(this.FileUpload.FileContent);

        //本批次號
        string BATCH_NO = GuidNo.getUUID();
        this.hdUploadBatchNo.Value = BATCH_NO;
        //匯入暫存用Table
        DIS01_DiscountMasterDataSet_DTO.UPLOAD_TEMPDataTable Table = dto.UPLOAD_TEMP;

        for (int index = 0; index < workbook.NumberOfSheets; index++)
        {
            HSSFSheet sheet = workbook.GetSheetAt(index) as HSSFSheet;
            DataTable TmpTable = getSheetFormat(sheet);  
            getSheetData(BATCH_NO, TmpTable, sheet.SheetName, ref Table);
        }
        
        try
        {
            //將匯入的資料存入Temp Table中
            Facade.Import(Table, qDisType, BATCH_NO, logMsg.OPERATOR);
        }
        catch (Exception ex)
        {
            throw ex;
        }

        //顯示匯入後的資料
        bindData(BATCH_NO, "ALL", null);
    }

    protected void btnCommit_Click(object sender, EventArgs e)
    {
        string BATCH_NO = this.hdUploadBatchNo.Value;
        string Msg = "";
        string Code = "";
        string Message = "";
        if (!string.IsNullOrEmpty(BATCH_NO))
        {
            //更新資料庫
            DIS01_Facade Facade = new DIS01_Facade();
            Facade.Commit(qDisType, BATCH_NO, ref Code, ref Message);
            if (Code == "000")
            {
                Msg = "匯入完成";
                this.btnCommit.Enabled = false;
                SetReturnValue(string.Empty);
                ScriptManager.RegisterClientScriptBlock(this, typeof(string), "匯入", "alert('" + Msg + "!');", true);
                SetReturnOKScript();
            }
            else
            {
                Msg = "匯入失敗, " + Message.Replace("'", "-").Replace("\"", " ").Replace("\r", "").Replace("\n", "");
                ScriptManager.RegisterClientScriptBlock(this, typeof(string), "匯入", "alert('" + Msg + "!');", true);
            }

        }
    }

    private void bindData(string BATCH_NO, string TableName, ASPxGridView gv)
    {
        DIS01_Facade facade = new DIS01_Facade();

        if (TableName != "ALL")
        {
            gv.DataSource = (DataTable)ViewState[TableName];
            gv.DataBind();
        }
        else
        {
            DataSet ds = facade.Query_AllImportData_ByBatchNo(BATCH_NO);
            this.gvHeader0.DataSource = ViewState["Header0"] = ds.Tables["Header0"];
            this.gvHeader1.DataSource = ViewState["Header1"] = ds.Tables["Header1"];
            this.gvRatePlan.DataSource = ViewState["RatePlan"] = ds.Tables["RatePlan"];
            this.gvProduct.DataSource = ViewState["Product"] = ds.Tables["Product"];
            this.gvStore.DataSource = ViewState["Store"] = ds.Tables["Store"];
            this.gvPromotion.DataSource = ViewState["Promotion"] = ds.Tables["Promotion"];
            this.gvCustomer.DataSource = ViewState["Customer"] = ds.Tables["Customer"];
            this.gvList.DataSource = ViewState["List"] = ds.Tables["List"];
            this.gvCCD.DataSource = ViewState["CCD"] = ds.Tables["CCD"];

            this.gvHeader0.DataBind();
            this.gvHeader1.DataBind();
            this.gvRatePlan.DataBind();
            this.gvProduct.DataBind();
            this.gvStore.DataBind();
            this.gvPromotion.DataBind();
            this.gvCustomer.DataBind();
            this.gvList.DataBind();
            this.gvCCD.DataBind();
        }

        //查詢是否有異常原因，若有則btnCommit Disabled
        int Count = facade.GetErrorCount(BATCH_NO);
        if (Count > 0)
        {
            this.btnCommit.ClientEnabled = false;
        }
        else
        {
            this.btnCommit.ClientEnabled = true;
        }
    }

    private DataTable getSheetFormat(HSSFSheet sheet)
    {
        HSSFRow headerRow = sheet.GetRow(0) as HSSFRow;
        
        int cellCount = headerRow.LastCellNum;
        int rowCount = sheet.LastRowNum;
        DataTable TmpTable = new DataTable();
        string strColumnValue = "";

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
                    {
                        NPOI.SS.UserModel.Cell cell = row.GetCell(j);
                        strColumnValue = StringUtil.CStr(cell);

                        switch (qDisType)
                        {
                            case "1":
                                #region  一般
                                if (j == 7 || j == 8) //cell[7]=有效日期_起, cell[8]=有效日期_迄
                                {
                                    strColumnValue = Utils.ExcelColumnValueToString(cell);
                                }
                                #endregion
                                break;
                            case "2":
                                #region  舊機回收
                                if (j == 4 || j == 5) //cell[4]=有效日期_起, cell[5]=有效日期_迄
                                {
                                    strColumnValue = Utils.ExcelColumnValueToString(cell);
                                }
                                #endregion
                                break;
                            default:
                                break;
                        }

                        dataRow[j] = strColumnValue;
                    }
                        
                }

                TmpTable.Rows.Add(dataRow);
            }
        }

        return TmpTable;
    }

    private void getSheetData(string BATCH_NO, DataTable TmpTable, string SheetName, ref DIS01_DiscountMasterDataSet_DTO.UPLOAD_TEMPDataTable Table)
    {
        string FINC_ID = SheetName;
        for (int i = 0; i <= TmpTable.Rows.Count - 1; i++)
        {
            DIS01_DiscountMasterDataSet_DTO.UPLOAD_TEMPRow dataRow = Table.NewUPLOAD_TEMPRow();

            switch (qDisType)
            {
                case "1":
                    #region  一般
                    switch (SheetName)
                    {
                        case "Header":
                            dataRow.F1 = StringUtil.CStr(TmpTable.Rows[i][0]);  //序號
                            dataRow.F2 = StringUtil.CStr(TmpTable.Rows[i][1]);  //折扣名稱
                            dataRow.F3 = StringUtil.CStr(TmpTable.Rows[i][2]);  //折扣金額
                            dataRow.F4 = StringUtil.CStr(TmpTable.Rows[i][3]);  //商品折扣比率
                            dataRow.F5 = StringUtil.CStr(TmpTable.Rows[i][4]);  //會計科目
                            dataRow.F6 = StringUtil.CStr(TmpTable.Rows[i][5]);  //折扣上限次數類型
                            dataRow.F7 = StringUtil.CStr(TmpTable.Rows[i][6]);  //折扣上限次數
                            dataRow.F8 = StringUtil.CStr(TmpTable.Rows[i][7]);  //有效日期(起)
                            dataRow.F9 = StringUtil.CStr(TmpTable.Rows[i][8]);  //有效日期(迄)
                            break;
                        case "Tab_RatePlan":
                            dataRow.F1 = StringUtil.CStr(TmpTable.Rows[i][0]);  //序號
                            dataRow.F2 = StringUtil.CStr(TmpTable.Rows[i][1]);  //費率
                            dataRow.F3 = StringUtil.CStr(TmpTable.Rows[i][2]);  //GA
                            dataRow.F4 = StringUtil.CStr(TmpTable.Rows[i][3]);  //Loyalty
                            dataRow.F5 = StringUtil.CStr(TmpTable.Rows[i][4]);  //2轉3
                            dataRow.F6 = StringUtil.CStr(TmpTable.Rows[i][5]);  //MNP
                            break;
                        case "Tab_Product":
                            dataRow.F1 = StringUtil.CStr(TmpTable.Rows[i][0]);  //序號
                            dataRow.F2 = StringUtil.CStr(TmpTable.Rows[i][1]);  //商品料號
                            break;
                        case "Tab_Store":
                            dataRow.F1 = StringUtil.CStr(TmpTable.Rows[i][0]);  //序號
                            dataRow.F2 = StringUtil.CStr(TmpTable.Rows[i][1]);  //門市編號
                            dataRow.F3 = StringUtil.CStr(TmpTable.Rows[i][2]);  //折扣上限次數
                            break;
                        case "Tab_Promotion":
                            dataRow.F1 = StringUtil.CStr(TmpTable.Rows[i][0]);  //序號
                            dataRow.F2 = StringUtil.CStr(TmpTable.Rows[i][1]);  //促銷代號
                            break;
                        case "Tab_Customer":
                            dataRow.F1 = StringUtil.CStr(TmpTable.Rows[i][0]);  //序號
                            dataRow.F2 = StringUtil.CStr(TmpTable.Rows[i][1]);  //ARPB金額(起)
                            dataRow.F3 = StringUtil.CStr(TmpTable.Rows[i][2]);  //ARPB金額(迄)
                            break;
                        case "Tab_List":
                            dataRow.F1 = StringUtil.CStr(TmpTable.Rows[i][0]);  //序號
                            dataRow.F2 = StringUtil.CStr(TmpTable.Rows[i][1]);  //客戶門號
                            break;
                        case "Tab_CCD":
                            dataRow.F1 = StringUtil.CStr(TmpTable.Rows[i][0]);  //序號
                            dataRow.F2 = StringUtil.CStr(TmpTable.Rows[i][1]);  //成本中心
                            dataRow.F3 = StringUtil.CStr(TmpTable.Rows[i][2]);  //商品分類
                            dataRow.F4 = StringUtil.CStr(TmpTable.Rows[i][3]);  //會計科目
                            dataRow.F5 = StringUtil.CStr(TmpTable.Rows[i][4]);  //金額
                            dataRow.F6 = StringUtil.CStr(TmpTable.Rows[i][5]);  //備註
                            break;
                    }

                    #endregion
                    break;
                case "2":
                    #region 舊機回收
                    dataRow.F1 = StringUtil.CStr(TmpTable.Rows[i][0]);  //折扣料號
                    dataRow.F2 = StringUtil.CStr(TmpTable.Rows[i][1]);  //折扣名稱
                    dataRow.F3 = StringUtil.CStr(TmpTable.Rows[i][2]);  //折扣金額
                    dataRow.F4 = StringUtil.CStr(TmpTable.Rows[i][3]);  //商品折扣比率
                    //dataRow.F5 = StringUtil.CStr(TmpTable.Rows[i][4]);  //折扣上限次數類型
                    //dataRow.F6 = StringUtil.CStr(TmpTable.Rows[i][5]);  //折扣上限次數
                    dataRow.F7 = StringUtil.CStr(TmpTable.Rows[i][4]);  //有效日期(起)
                    dataRow.F8 = StringUtil.CStr(TmpTable.Rows[i][5]);  //有效日期(迄)
                    #endregion
                    break;
                default:
                    break;
            }
            dataRow.BATCH_NO = BATCH_NO;
            dataRow.FINC_ID = FINC_ID;
            dataRow.USER_ID = logMsg.OPERATOR;
            Table.Rows.Add(dataRow);
            Table.AcceptChanges();
        }

    }

    protected void gvHeader0_PageIndexChanged(object sender, EventArgs e)
    {
        bindData(this.hdUploadBatchNo.Value, "Header0", gvHeader0);
    }
    protected void gvHeader1_PageIndexChanged(object sender, EventArgs e)
    {
        bindData(this.hdUploadBatchNo.Value, "Header1", gvHeader1);
    }
    protected void gvRatePlan_PageIndexChanged(object sender, EventArgs e)
    {
        bindData(this.hdUploadBatchNo.Value, "RatePlan", gvRatePlan);
    }
    protected void gvProduct_PageIndexChanged(object sender, EventArgs e)
    {
        bindData(this.hdUploadBatchNo.Value, "Product", gvProduct);
    }
    protected void gvStore_PageIndexChanged(object sender, EventArgs e)
    {
        bindData(this.hdUploadBatchNo.Value, "Store", gvStore);
    }
    protected void gvPromotion_PageIndexChanged(object sender, EventArgs e)
    {
        bindData(this.hdUploadBatchNo.Value, "Promotion", gvPromotion);
    }
    protected void gvCustomer_PageIndexChanged(object sender, EventArgs e)
    {
        bindData(this.hdUploadBatchNo.Value, "Customer", gvCustomer);
    }
    protected void gvList_PageIndexChanged(object sender, EventArgs e)
    {
        bindData(this.hdUploadBatchNo.Value, "List", gvList);
    }
    protected void gvCCD_PageIndexChanged(object sender, EventArgs e)
    {
        bindData(this.hdUploadBatchNo.Value, "CCD", gvCCD);
    }
}
