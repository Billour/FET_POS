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

public partial class VSS_ORD_ORD13_ORD13_Import : Popup
{   
    protected void Page_Load(object sender, EventArgs e)
    {
    }

    protected void btnImport_Click(object sender, EventArgs e)
    {
        if (!this.FileUpload.HasFile) { return; }
        string FileExten = System.IO.Path.GetExtension(this.FileUpload.PostedFile.FileName);
        if (FileExten.ToLower() != ".xls")
        { return; }
        try
        {
            HSSFWorkbook workbook = null;

            workbook = new HSSFWorkbook(this.FileUpload.FileContent);

            DataTable table = new DataTable();
            //Product_no
            HSSFSheet sheet = workbook.GetSheetAt(0) as HSSFSheet;

            HSSFRow headerRow = sheet.GetRow(0) as HSSFRow;

            int cellCount = headerRow.LastCellNum;
            int rowCount = sheet.LastRowNum;

            for (int i = headerRow.FirstCellNum; i < cellCount; i++)
            {
                DataColumn column = new DataColumn(headerRow.GetCell(i).StringCellValue);
                table.Columns.Add(column);
            }

            //匯入暫存用Table
            INV05_RTNM_DTO.UPLOAD_TEMPDataTable Table = new INV05_RTNM_DTO.UPLOAD_TEMPDataTable();

            //本批次號
            ViewState["BATCH_NO"] = GuidNo.getUUID();
            string FINC_ID = "ORD13_IMPORT";
            string USER_ID = logMsg.MODI_USER;

            //將exl的每個值抓出來
            for (int i = (sheet.FirstRowNum + 1); i <= sheet.LastRowNum; i++)
            {
                HSSFRow row = sheet.GetRow(i) as HSSFRow;

                //2011/04/14 bill 取得空的 row 就跳過
                if (row == null)
                    continue;

                DataRow dataRow1 = table.NewRow();

                for (int j = row.FirstCellNum; j < cellCount; j++)
                {
                    if (row.GetCell(j) != null)
                    {
                        NPOI.SS.UserModel.Cell cell = row.GetCell(j);

                        if (j == 2 || j == 3) //cell[2]=開始日期, cell[3]=結束日期
                        {
                            dataRow1[j] = Utils.ExcelColumnValueToString(cell);
                            //**2011/04/20 Tina：將下列程式碼移至共用class
                            //    if (cell.CellType == NPOI.SS.UserModel.CellType.NUMERIC)
                            //    {
                            //        DateTime Datetime;
                            //        string strDt = StringUtil.CStr(cell);
                            //        if (DateTime.TryParse(strDt, System.Globalization.DateTimeFormatInfo.InvariantInfo, System.Globalization.DateTimeStyles.None, out Datetime))
                            //        {
                            //            dataRow1[j] = Datetime.ToString("yyyy/MM/dd");
                            //        }
                            //        else
                            //        {
                            //            dataRow1[j] = StringUtil.CStr(cell);
                            //        }
                            //    }
                            //    else
                            //    {
                            //        dataRow1[j] = StringUtil.CStr(cell);
                            //    }
                        }
                        else
                        {
                            dataRow1[j] = StringUtil.CStr(cell);
                        }
                    }
                }

                table.Rows.Add(dataRow1);

            }


            foreach (DataRow row in table.Rows)
            {
                INV05_RTNM_DTO.UPLOAD_TEMPRow dataRow = Table.NewUPLOAD_TEMPRow();
                dataRow.F1 = StringUtil.CStr(row["門市編號"]);
                dataRow.F3 = StringUtil.CStr(row["卡片群組"]);
                dataRow.F4 = StringUtil.CStr(row["安全庫存量"]);
                dataRow.F5 = StringUtil.CStr(row["最低庫存量"]);
                dataRow.F7 = StringUtil.CStr(row["開始日期"]);
                dataRow.F8 = StringUtil.CStr(row["結束日期"]);
                dataRow.BATCH_NO = StringUtil.CStr(ViewState["BATCH_NO"]);
                dataRow.FINC_ID = FINC_ID;
                dataRow.USER_ID = USER_ID;

                Table.Rows.Add(dataRow);
                Table.AcceptChanges();

            }

            //將匯入的資料存入Temp Table中
            new ORD13_Facade().ImportTemp(Table);

            INV05_RTNM_DTO.UPLOAD_TEMPDataTable tempDt = new ORD13_Facade().GetTemp(StringUtil.CStr(ViewState["BATCH_NO"]));
            gvMaster.DataSource = tempDt;
            gvMaster.DataBind();

            int success_count = 0;
            int failed_count = 0;

            foreach (INV05_RTNM_DTO.UPLOAD_TEMPRow dr in tempDt.Rows)
            {
                if (dr.IsF6Null())
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
                btnUpdate.Enabled = false;
                btnUpdate.Visible = true;
                btnCalcel.Visible = true;
            }
            else
            {
                btnUpdate.Enabled = true;
                btnCalcel.Enabled = true;
                btnUpdate.Visible = true;
                btnCalcel.Visible = true;
            }
        }
        catch(Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(this, GetType(), "AlertMessage", "alert('匯入異常!!" + ex.Message.Replace("'", "-").Replace("\"", " ").Replace("\r", "").Replace("\n", "") + "');", true);
        }
    }

    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        new ORD13_Facade().TempToData(StringUtil.CStr(ViewState["BATCH_NO"]), logMsg.MODI_USER);

        SetReturnValue(string.Empty);
        ClientScript.RegisterClientScriptBlock(this.GetType(),"Import", "<script>alert('匯入完成，請重新查詢資料!');</script>");
    }

    protected void gvMaster_PageIndexChanged(object sender, EventArgs e)
    {
        INV05_RTNM_DTO.UPLOAD_TEMPDataTable tempDt = new ORD13_Facade().GetTemp(StringUtil.CStr(ViewState["BATCH_NO"]));
        gvMaster.DataSource = tempDt;
        gvMaster.DataBind();
    }
}
