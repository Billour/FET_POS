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

public partial class VSS_ORD_ORD10_Import : Popup
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void gvMaster_PageIndexChanged(object sender, EventArgs e)
    {
        gvMaster.DataSource = ViewState["tempDt"] as DataTable;
        gvMaster.DataBind();
    }

    protected void btnImport_Click(object sender, EventArgs e)
    {
        String fileName = this.FileUpload.FileName;
        DataTable table = new DataTable();
        //匯入暫存用Table
        ORD10_WeightDistribute_DTO.UPLOAD_TEMPDataTable Table = new ORD10_WeightDistribute_DTO.UPLOAD_TEMPDataTable();
        try
        {
            if (!this.FileUpload.HasFile) { return; }
            if ((fileName.Trim().Substring(fileName.Trim().IndexOf('.')) != ".xls"))
            {
                ScriptManager.RegisterClientScriptBlock(this, typeof(string), "EXCEL匯入", "alert('檔案格式錯誤!');", true);
                return;
            }

            HSSFWorkbook workbook = new HSSFWorkbook(this.FileUpload.FileContent);
            HSSFSheet sheet = workbook.GetSheetAt(0) as HSSFSheet;
            HSSFRow headerRow = sheet.GetRow(0) as HSSFRow;
            int cellCount = headerRow.LastCellNum;
            int rowCount = sheet.LastRowNum;
            if (cellCount > 2)
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
            string FINC_ID = "ORD10_Import";
            string USER_ID = logMsg.MODI_USER;

            for (int i = (sheet.FirstRowNum + 1); i <= sheet.LastRowNum; i++)
            {
                HSSFRow row = sheet.GetRow(i) as HSSFRow;
                DataRow dataRow1 = table.NewRow();
                if (row != null)
                {
                    for (int j = row.FirstCellNum; j < cellCount; j++)
                    {
                        if (row.GetCell(j) != null)
                            dataRow1[j] = StringUtil.CStr(row.GetCell(j));
                    }
                    table.Rows.Add(dataRow1);
                }
            }

            foreach (DataRow row in table.Rows)
            {
                DataTable Zone = new Store_Facade().Query_StoreInfo(StringUtil.CStr(row["門市編號"]));

                ORD10_WeightDistribute_DTO.UPLOAD_TEMPRow dataRow = Table.NewUPLOAD_TEMPRow();
                dataRow.F1 = StringUtil.CStr(row["門市編號"]);
                dataRow.F2 = StringUtil.CStr(row["比率"]);
                dataRow.BATCH_NO = StringUtil.CStr(ViewState["BATCH_NO"]);
                dataRow.FINC_ID = FINC_ID;
                dataRow.USER_ID = USER_ID;
                Table.Rows.Add(dataRow);
                Table.AcceptChanges();
            }

            //float iTmp;
            //foreach (DataRow row in table.Rows)
            //{
            //    if (StringUtil.CStr(row["門市編號"]) != "" && StringUtil.CStr(row["門市編號"]) != null)
            //    {
            //        DataTable Zone = new Store_Facade().Query_StoreInfo(StringUtil.CStr(row["門市編號"]));

            //        ORD10_WeightDistribute_DTO.UPLOAD_TEMPRow dataRow = Table.NewUPLOAD_TEMPRow();
            //        dataRow.F1 = StringUtil.CStr(row["門市編號"]);
            //        dataRow.F3 = StringUtil.CStr(row["比率"]);
            //        if (StringUtil.CStr(row["門市編號"]).Trim() == "" || new Store_Facade().GetStoreName(StringUtil.CStr(row["門市編號"])) == "")
            //            dataRow.F4 = "店組不存在";
            //        else if (StringUtil.CStr(row["門市編號"]).Trim() != "" && (new Store_Facade().GetStoreName(StringUtil.CStr(row["門市編號"])) == ""))
            //            dataRow.F4 = "門市已閉店";
            //        else if (StringUtil.CStr(Zone.Rows[0]["ZONE"]) == "")
            //            dataRow.F4 = "門市查無區域";
            //        else if (Table.Select("F1='" + StringUtil.CStr(row["門市編號"]) + "'").Count() > 0)
            //            dataRow.F4 = "門市編號重複";
            //        else if (StringUtil.CStr(row["比率"]).Trim() == "")
            //            dataRow.F4 = "比率不可為空值";
            //        else if (!float.TryParse(StringUtil.CStr(row["比率"]).Trim(), out iTmp))
            //            dataRow.F4 = "比率不符合數字格式，請重新輸入!";
            //        else if (Convert.ToDecimal(row["比率"]) < 1)
            //            dataRow.F4 = "比率應大於0";
            //        dataRow.F2 = new Store_Facade().GetStoreName(StringUtil.CStr(row["門市編號"]));
            //        dataRow.BATCH_NO = StringUtil.CStr(ViewState["BATCH_NO"]);
            //        dataRow.FINC_ID = FINC_ID;
            //        dataRow.USER_ID = USER_ID;
            //        Table.Rows.Add(dataRow);
            //        Table.AcceptChanges();
            //    }
            //}
        }
        catch(Exception ex)
        {
            string strMsg = ex.Message;
            ScriptManager.RegisterClientScriptBlock(this, typeof(string), "EXCEL匯入", "alert('檔案格式錯誤, " + strMsg + "');", true);
            return;
        }

        //將匯入的資料存入Temp Table中
        new ORD10_Facade().ImportTemp(Table, StringUtil.CStr(ViewState["BATCH_NO"]));

        ORD10_WeightDistribute_DTO.UPLOAD_TEMPDataTable tempDt = new ORD10_Facade().GetTemp(StringUtil.CStr(ViewState["BATCH_NO"]));
        ViewState["tempDt"] = tempDt;
        gvMaster.DataSource = tempDt;
        gvMaster.DataBind();
        gvMaster.PageIndex = 0;


    }

    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        DataTable dt = ViewState["tempDt"] as DataTable;
        ORD10_WeightDistribute_DTO.STORE_WEIGHT_DISTRIBUTEDataTable STORE_WD = null;
        STORE_WD = new ORD10_WeightDistribute_DTO.STORE_WEIGHT_DISTRIBUTEDataTable();
        ORD10_WeightDistribute_DTO.STORE_WEIGHT_DISTRIBUTERow STORE_WR;

        foreach (DataRow dr in dt.Rows)
        {
            STORE_WR = STORE_WD.NewSTORE_WEIGHT_DISTRIBUTERow();
            DataTable Zone = new Store_Facade().Query_StoreInfo(StringUtil.CStr(dr["F1"]));

            STORE_WR["ZONE"] = (Zone.Rows.Count > 0 ? StringUtil.CStr(Zone.Rows[0]["ZONE"]) : "");
            STORE_WR["WEIGHT"] = float.Parse(StringUtil.CStr(dr["F3"]).Replace("%", ""));
            STORE_WR["STORE_NO"] = StringUtil.CStr(dr["F1"]);
            STORE_WR["MODI_USER"] = logMsg.OPERATOR;
            STORE_WR["MODI_DTM"] = Convert.ToDateTime(System.DateTime.Now);
            STORE_WR["CREATE_USER"] = logMsg.MODI_USER;
            STORE_WR["CREATE_DTM"] = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");
            STORE_WD.Rows.Add(STORE_WR);
        }

        new ORD10_Facade().SaveWeightDistribute(STORE_WD);

        SetReturnValue(string.Empty);
        SetReturnOKScript();
        Session["DOIMPOT"] = "true";


    }

    protected void gvMaster_HtmlFooterCellPrepared(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewTableFooterCellEventArgs e)
    {
        if (gvMaster.VisibleRowCount > 0)
        {
            decimal TotRate = 0;
            DataTable dt = ViewState["tempDt"] as DataTable;
            foreach (DataRow dr in dt.Rows)
            {
                try
                {
                    TotRate += Convert.ToDecimal(StringUtil.CStr(dr["F3"]).Replace("%", ""));
                }
                catch //(Exception ex)
                {
                    TotRate += 0;
                }
            }
            ASPxLabel labelTotal = (ASPxLabel)gvMaster.FindFooterCellTemplateControl(e.Column, "lblFooterTotal");

            if (e.Column.Caption == "比率")
                labelTotal.Text = StringUtil.CStr(TotRate) + "%";
            else if (e.Column.Caption == "異常原因")
            {
                //int i =  ;
                bool err = true;
                if (TotRate != 100)
                {
                    labelTotal.Text = ",加總比率非100%";
                    btnUpdate.Enabled = false;
                    e.Cell.Style[HtmlTextWriterStyle.Color] = "red";
                    err = false;
                }
                if (dt.Select("RESULT<>''").Count() > 0)
                {
                    labelTotal.Text += ",有錯誤的資料請檢查";
                    btnUpdate.Enabled = false;
                    e.Cell.Style[HtmlTextWriterStyle.Color] = "red";
                    err = false;
                }
                if (err)
                {
                    labelTotal.Visible = false;
                    btnUpdate.Enabled = true;
                }
                else
                {
                    labelTotal.Text = labelTotal.Text.Substring(1, labelTotal.Text.Length - 1);
                    btnUpdate.Enabled = false;
                }

            }
        }
    }
}

