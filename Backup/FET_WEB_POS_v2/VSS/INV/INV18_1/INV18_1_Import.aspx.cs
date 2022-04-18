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

public partial class VSS_INV_INV18_1_Import : Popup
{
    protected void Page_Load(object sender, EventArgs e)
    {
        logMsg.MODI_USER = logMsg.MODI_USER;
        Session["DOIMPOT"] = "false";
    }

    protected void gvMaster_PageIndexChanged(object sender, EventArgs e)
    {
        ASPxGridView grid = sender as ASPxGridView;
        gvMaster.DataSource = Session["tempDt"];
        gvMaster.DataBind();
    }

    protected void btnImport_Click(object sender, EventArgs e)
    {
        String fileName = this.FileUpload.FileName;
        DataTable table = new DataTable();
        //匯入暫存用Table
        INV18_StockADJ_DTO.UPLOAD_TEMPDataTable Table = new INV18_StockADJ_DTO.UPLOAD_TEMPDataTable();
        try
        {
            if (!this.FileUpload.HasFile) { return; }

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
            if (cellCount > 5)
            {
                ScriptManager.RegisterClientScriptBlock(this, typeof(string), "EXCEL匯入", "alert('檔案欄位數過多!');", true);
                return;
            }


            for (int i = headerRow.FirstCellNum; i < cellCount; i++)
            {
                DataColumn column = new DataColumn(headerRow.GetCell(i).StringCellValue);
                table.Columns.Add(column);
            }

            //本批次號
            ViewState["BATCH_NO"] = GuidNo.getUUID();
            string FINC_ID = "INV18_1_IMPORT";
            string USER_ID = logMsg.MODI_USER;

            //將exl的每個值抓出來Product_no
            for (int i = (sheet.FirstRowNum + 1); i <= sheet.LastRowNum; i++)
            {
                HSSFRow row = sheet.GetRow(i) as HSSFRow;
                DataRow dataRow1 = table.NewRow();

                for (int j = row.FirstCellNum; j < cellCount; j++)
                {
                    if (row.GetCell(j) != null)
                        dataRow1[j] = StringUtil.CStr(row.GetCell(j));
                }

                table.Rows.Add(dataRow1);

            }


            foreach (DataRow row in table.Rows)
            {
                INV18_StockADJ_DTO.UPLOAD_TEMPRow dataRow = Table.NewUPLOAD_TEMPRow();
                dataRow.F9 = StringUtil.CStr(row["門市編號"]);
                dataRow.F1 = StringUtil.CStr(row["商品料號"]);
                dataRow.F4 = StringUtil.CStr(row["調整量"]);
                dataRow.F5 = StringUtil.CStr(row["調整原因"]);
                dataRow.F7 = (StringUtil.CStr(row["IMEI"]).Trim() == "" ? "0" : StringUtil.CStr(StringUtil.CStr(row["IMEI"]).Split(';').Length));
                dataRow.F8 = StringUtil.CStr(row["IMEI"]).Replace(";", ",");  //**2011/02/18 Tina：Issue590 將分隔符號【;】置換成【,】
                dataRow.BATCH_NO = StringUtil.CStr(ViewState["BATCH_NO"]);
                dataRow.FINC_ID = FINC_ID;
                dataRow.USER_ID = USER_ID;
                Table.Rows.Add(dataRow);
                Table.AcceptChanges();

            }
            
        }
        catch
        {
            ScriptManager.RegisterClientScriptBlock(this, typeof(string), "EXCEL匯入", "alert('檔案格式錯誤!');", true);
            return;
        }

        //將匯入的資料存入Temp Table中
        try
        {
            new INV18_1_Facade().ImportTemp(Table);
        }
        catch (Exception ex)
        {
            throw ex;
        }

        INV18_StockADJ_DTO.UPLOAD_TEMPDataTable tempDt = new INV18_1_Facade().GetTemp(StringUtil.CStr(ViewState["BATCH_NO"]));
        Session["tempDt"] = tempDt;
        gvMaster.DataSource = tempDt;
        gvMaster.DataBind();
        gvMaster.PageIndex = 0;
        int success_count = 0;
        int failed_count = 0;

        foreach (INV18_StockADJ_DTO.UPLOAD_TEMPRow dr in tempDt.Rows)
        {
            if (dr.IsF10Null())
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
        }
        else
        {
            btnUpdate.Enabled = true;
        }
    }

    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        INV18_1_Facade _INV18_1_Facade = new INV18_1_Facade();

        DataTable dtAdjD = (DataTable)Session["tempDt"];
        if (dtAdjD.Rows.Count > 0)
        {
            INV18_StockADJ_DTO.STOCKADJDDataTable dtADJD = null;
            dtADJD = new INV18_StockADJ_DTO.STOCKADJDDataTable();
            INV18_StockADJ_DTO.STOCKADJDRow drADJD;

            INV18_StockADJ_DTO.STOCKADJMDataTable dtADJM = null;
            INV18_StockADJ_DTO.STOCKADJMRow drADJM;
            dtADJM = new INV18_StockADJ_DTO.STOCKADJMDataTable();

            INV18_StockADJ_DTO.STOCKADJ_D_IMEIDataTable dt_STOCKADJ = null;
            dt_STOCKADJ = new INV18_StockADJ_DTO.STOCKADJ_D_IMEIDataTable();
            INV18_StockADJ_DTO.STOCKADJ_D_IMEIRow dr_STOCKADJ;

            string _ADJNO = "";
            string _Store = "";
            string _D_UUID = "";
            foreach (DataRow dr in dtAdjD.Rows)
            {
                if (_Store != StringUtil.CStr(dr["F9"]))
                {
                    drADJM = dtADJM.NewSTOCKADJMRow();
                    _ADJNO = "SA" + StringUtil.CStr(dr["F9"]) + SerialNo.GenNo("SA");
                    drADJM["ADJNO"] = _ADJNO;
                    drADJM["ADJDATE"] = DateTime.Now.ToString("yyyy/MM/dd");
                    drADJM["ADJUSRNO"] = logMsg.MODI_USER;
                    drADJM["REMARK"] = "";
                    drADJM["STORE_NO"] = StringUtil.CStr(dr["F9"]);
                    drADJM["STATUS"] = "10";
                    drADJM["MODI_USER"] = logMsg.OPERATOR;
                    drADJM["MODI_DTM"] = Convert.ToDateTime(System.DateTime.Now);
                    drADJM["CREATE_USER"] = logMsg.MODI_USER;
                    drADJM["CREATE_DTM"] = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");
                    drADJM["ADJLOC"] = Common_PageHelper.GetGoodLOCUUID();
                    dtADJM.Rows.Add(drADJM);
                }
                drADJD = dtADJD.NewSTOCKADJDRow();
                drADJD["ADJNO"] = _ADJNO;
                drADJD["ADJQTY"] = StringUtil.CStr(dr["F4"]);
                drADJD["ADJREASON"] = StringUtil.CStr(dr["F6"]);
                drADJD["PRODNO"] = StringUtil.CStr(dr["F1"]);
                drADJD["STOCKADJ_REASON_CODE"] = StringUtil.CStr(dr["F5"]);
                _D_UUID = GuidNo.getUUID();
                drADJD["STOCKADJD_ID"] = _D_UUID;
                drADJD["MODI_USER"] = logMsg.OPERATOR;
                drADJD["MODI_DTM"] = Convert.ToDateTime(System.DateTime.Now);
                drADJD["CREATE_USER"] = logMsg.MODI_USER;
                drADJD["CREATE_DTM"] = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");
                dtADJD.Rows.Add(drADJD);

                string[] _IMEI = StringUtil.CStr(dr["F8"]).Split(';');
                if (_IMEI.GetUpperBound(0) > 0)
                {
                    for (int i = 0; i < _IMEI.GetUpperBound(0) + 1; i++)
                    {
                        if (_IMEI[i] != "" && _IMEI[i] != null)
                        {
                            dr_STOCKADJ = dt_STOCKADJ.NewSTOCKADJ_D_IMEIRow(); ;
                            dr_STOCKADJ["STOCKADJ_D_IMEI_ID"] = GuidNo.getUUID();
                            dr_STOCKADJ["STOCKADJD_ID"] = _D_UUID;
                            dr_STOCKADJ["IMEI"] = _IMEI[i];
                            dr_STOCKADJ["CREATE_DTM"] = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");
                            dr_STOCKADJ["CREATE_USER"] = logMsg.MODI_USER;
                            dr_STOCKADJ["MODI_USER"] = logMsg.MODI_USER;
                            dr_STOCKADJ["MODI_DTM"] = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");
                            dt_STOCKADJ.Rows.Add(dr_STOCKADJ);
                        }
                    }
                }
                else
                {
                    if (StringUtil.CStr(dr["F8"]) != "")
                    {
                        dr_STOCKADJ = dt_STOCKADJ.NewSTOCKADJ_D_IMEIRow(); ;
                        dr_STOCKADJ["STOCKADJ_D_IMEI_ID"] = GuidNo.getUUID();
                        dr_STOCKADJ["STOCKADJD_ID"] = _D_UUID;
                        dr_STOCKADJ["IMEI"] = StringUtil.CStr(dr["F8"]);
                        dr_STOCKADJ["CREATE_DTM"] = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");
                        dr_STOCKADJ["CREATE_USER"] = logMsg.MODI_USER;
                        dr_STOCKADJ["MODI_USER"] = logMsg.MODI_USER;
                        dr_STOCKADJ["MODI_DTM"] = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");
                        dt_STOCKADJ.Rows.Add(dr_STOCKADJ);
                    }


                }

                _Store = StringUtil.CStr(dr["F9"]);
            }
            int intResult = _INV18_1_Facade.SaveIMPORTADJ(dtADJM, dtADJD, dt_STOCKADJ);
        }

        SetReturnValue(string.Empty);
        SetReturnOKScript();
        Session["DOIMPOT"] = "true";
    }

}
