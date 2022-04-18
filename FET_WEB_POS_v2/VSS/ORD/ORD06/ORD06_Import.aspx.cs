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

public partial class VSS_ORD_ORD06_Import : Popup
{
    #region Class Variables
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
        dtResult.Columns.Add("RESULT", typeof(string));
        dtResult.PrimaryKey = new DataColumn[] { dtResult.Columns["PRODNO"] };
        return dtResult;
    }

    protected void bindMasterDataProduct()
    {
        DataTable dtResult = new DataTable();
        if (ViewState["Product"] == null)
        {
            dtResult = getGridViewDataProduct();
        }
        else
        {
            dtResult = (DataTable)ViewState["Product"];
        }
        gvProduct.DataSource = dtResult;
        gvProduct.DataBind();
    }

    protected void btnImport_Click(object sender, EventArgs e)
    {
        try
        {
            if (!this.FileUpload.HasFile) { return; }

            String fileName = this.FileUpload.FileName;

            if ((fileName.Trim().Substring(fileName.Trim().IndexOf('.')) != ".xls"))
            {
                ScriptManager.RegisterClientScriptBlock(this, typeof(string), "EXCEL匯入", "alert('檔案格式錯誤!');", true);
                return;
            }

            ORD06_Facade Facade = new ORD06_Facade();
            ORD06_OneToOne_DTO ORD06_DTO = new ORD06_OneToOne_DTO();

            HSSFWorkbook workbook = new HSSFWorkbook(this.FileUpload.FileContent);

            //Product_no
            HSSFSheet sheet = workbook.GetSheetAt(0) as HSSFSheet;

            HSSFRow headerRow = sheet.GetRow(0) as HSSFRow;

            int cellCount = headerRow.LastCellNum;
            int rowCount = sheet.LastRowNum;

            DataTable TmpTable = new DataTable();

            if (cellCount != 4)
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
            ORD06_OneToOne_DTO.UPLOAD_TEMPDataTable Table;
            Table = ORD06_DTO.Tables["UPLOAD_TEMP"] as ORD06_OneToOne_DTO.UPLOAD_TEMPDataTable;

            DataTable dt = new DataTable();
            dt.Columns.Add("PM_PRODNO", typeof(string));
            dt.Columns.Add("PD_PRODNO", typeof(string));

            //本批次號
            BATCH_NO = GuidNo.getUUID();
            Session["BATCH_NO"] = "PRODUCT;" + BATCH_NO;
            string FINC_ID = "ORD06_PROD_IMPORT";
            this.hdUploadBatchNo.Value = BATCH_NO;
            int success_count = 0;
            int failed_count = 0;

            //將exl的每個值抓出來Product_no
            for (int i = 0; i <= TmpTable.Rows.Count - 1; i++)
            {

                DataRow dr = dt.NewRow();
                ORD06_OneToOne_DTO.UPLOAD_TEMPRow dataRow = Table.NewUPLOAD_TEMPRow();

                dataRow.F1 = "PRODUCT";
                dataRow.F2 = StringUtil.CStr(TmpTable.Rows[i][0]);
                dr["PM_PRODNO"] = StringUtil.CStr(TmpTable.Rows[i][0]);

                DataTable dtProduct = new Product_Facade().Query_ProductInfo(StringUtil.CStr(TmpTable.Rows[i][0]));
                if (dtProduct != null && dtProduct.Rows.Count > 0 && dtProduct.Rows[0]["PRODNAME"] != null)
                {
                    dataRow.F3 = StringUtil.CStr(dtProduct.Rows[0]["PRODNAME"]);
                }
                else
                {
                    dataRow.F3 = "";
                    dataRow.F8 = "【主商品料號】不存在";
                }
                dataRow.F4 = StringUtil.CStr(TmpTable.Rows[i][1]);
                dr["PD_PRODNO"] = StringUtil.CStr(TmpTable.Rows[i][1]);

                dtProduct = new Product_Facade().Query_ProductInfo(StringUtil.CStr(TmpTable.Rows[i][1]));
                if (dtProduct != null && dtProduct.Rows.Count > 0 && dtProduct.Rows[0]["PRODNAME"] != null)
                {
                    dataRow.F5 = StringUtil.CStr(dtProduct.Rows[0]["PRODNAME"]);
                }
                else
                {
                    dataRow.F5 = "";
                    dataRow.F8 = "【搭配料號】不存在";
                }
                dataRow.F6 = StringUtil.CStr(TmpTable.Rows[i][2]);
                dataRow.F7 = StringUtil.CStr(TmpTable.Rows[i][3]);

                bool CheckDate = true;
                if (dataRow.F6 == null || dataRow.F6.Trim() == "")
                {
                    dataRow.F8 = "【開始日期】不允許空值，請重新輸入";
                    CheckDate = false;
                }
                else
                {
                    try
                    {
                        if (Convert.ToDateTime(dataRow.F6).Date <= DateTime.Now.Date)
                        {
                            dataRow.F8 = "【開始日期】必須大於系統日期";
                            CheckDate = false;
                        }
                        else
                        {
                            if (dataRow.F7 != null && dataRow.F7.Trim() != "")
                            {
                                if (dataRow.F7.Substring(0, 4) != "0001" && Convert.ToDateTime(dataRow.F7).Date <= DateTime.Now.Date)
                                {
                                    dataRow.F8 = "【結束日期】必須大於系統日期";
                                    CheckDate = false;
                                }
                                else
                                {
                                    if (dataRow.F7.Substring(0, 4) != "0001" && Convert.ToDateTime(dataRow.F7).Date < Convert.ToDateTime(dataRow.F6).Date)
                                    {
                                        dataRow.F8 = "【結束日期】必須>=【開始日期】";
                                        CheckDate = false;
                                    }

                                }
                            }
                        }
                    }
                    catch (Exception)
                    {
                        dataRow.F8 = "【日期】格式錯誤";
                        CheckDate = false;
                    }
                }
                if (CheckDate)
                {
                    DataRow[] DRSelf1 = dt.Select("PM_PRODNO='" + StringUtil.CStr(TmpTable.Rows[i][1]) + "'");
                    DataRow[] DRSelf2 = dt.Select("PM_PRODNO='" + StringUtil.CStr(TmpTable.Rows[i][0]) + "' and PD_PRODNO='" + StringUtil.CStr(TmpTable.Rows[i][1]) + "'");
                    if (dataRow.F4 == null || dataRow.F4.Trim() == "")
                    { dataRow.F8 = "【搭配商品編號】不允許空值，請重新輸入!"; }
                    else
                        if (dataRow.F2 == null || dataRow.F2.Trim() == "")
                        { dataRow.F8 = "【主商品編號】不允許空值，請重新輸入!"; }
                        else
                            if (dataRow.F3 == "")
                            { dataRow.F8 = "【主商品編號】不存在!"; }
                            else
                                if (dataRow.F5 == "")
                                { dataRow.F8 = "【搭配商品編號】不存在!"; }
                                else
                                    if (dataRow.F2.Trim().ToUpper() == dataRow.F4.Trim().ToUpper())
                                        dataRow.F8 = "主商品不允許與搭配商品相同，請重新輸入";
                                    else if (DRSelf1.Length > 0 && dt.Rows.Count > 0)
                                    { dataRow.F8 = "搭配商品己設定為己生效組合的主商品，請重新輸入"; }
                                    else if (ORD06_Facade.CheckMainProduct(dataRow.F4, dataRow.F2, "INSERT", dataRow.F6, dataRow.F7) != "")
                                    { dataRow.F8 = "搭配商品己設定為己生效組合的主商品，請重新輸入"; }
                                    else if (DRSelf2.Length > 0 && dt.Rows.Count > 0)
                                    { dataRow.F8 = "一搭一組合己存在，請重新輸入"; }
                                    else if (ORD06_Facade.CheckMainDetailProduct(dataRow.F2, dataRow.F4, dataRow.F6, dataRow.F7) != "")
                                    { dataRow.F8 = "一搭一組合己存在，請重新輸入"; }
                }

                dataRow.BATCH_NO = BATCH_NO;
                dataRow.FINC_ID = FINC_ID;
                dataRow.USER_ID = USER_ID;


                Table.Rows.Add(dataRow);
                dt.Rows.Add(dr);
                ORD06_DTO.AcceptChanges();
            }

            //將匯入的資料存入Temp Table中
            Facade.ImportHeadQuarter(ORD06_DTO, "PRODUCT");

            DataTable dtNew = Facade.GetImportTempData(BATCH_NO, FINC_ID, USER_ID);
            ViewState["ImportTable_P"] = dtNew;
            this.gvProduct.DataSource = dtNew;
            this.gvProduct.DataBind();


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
            workbook = null;
            sheet = null;
        }
        catch (Exception)
        {
            ScriptManager.RegisterClientScriptBlock(this, typeof(string), "cmd", "alert('格式錯誤!!');", true);
        }
    }

    protected void btnCommit_Click(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(this.hdUploadBatchNo.Value))
        {

            //更新資料庫
            ORD06_Facade Facade = new ORD06_Facade();
            Facade.UpdateOne_UpLoadTempMethodSet(BATCH_NO, "ORD06_PROD_IMPORT", USER_ID);

            string FINC_ID = "ORD06_PROD_IMPORT";
            DataTable dtNew = Facade.GetImportTempData(BATCH_NO, FINC_ID, USER_ID);

            foreach (DataRow dr in dtNew.Rows)
            {
                new ORD06_Facade().InsertOneToOneMethodData(StringUtil.CStr(dr["MPRODNO"]),
                        StringUtil.CStr(dr["DPRODNO"]), StringUtil.CStr(dr["SDATE"]), StringUtil.CStr(dr["EDATE"]), logMsg.MODI_USER);
            }


            this.btnCommit.Enabled = false;
            SetReturnValue(string.Empty);
        }

        SetReturnOKScript();
    }

    protected void gvProduct_HtmlRowPrepared(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewTableRowEventArgs e)
    {
        if (e.RowType != GridViewRowType.Data) return;
        if (StringUtil.CStr(e.GetValue("RESULT")) != string.Empty)
            e.Row.ForeColor = System.Drawing.Color.Red;

    }
}
