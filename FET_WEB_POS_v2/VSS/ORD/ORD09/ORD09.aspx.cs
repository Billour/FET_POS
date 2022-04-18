using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.Web.ASPxGridView;
using DevExpress.Web.ASPxEditors;
using System.Data;
using NPOI.HSSF.UserModel;

using Advtek.Utility;
using FET.POS.Model.DTO;
using FET.POS.Model.Facade;
using FET.POS.Model.Facade.FacadeImpl;
using System.Web.UI.HtmlControls;
using FET.POS.Model.Common;

public partial class VSS_ORD_ORD09_ORD09 : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            btnCommitUpload.Enabled = false;
        }
        hidSubmit.Value = "";
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
            if (cellCount != 3)
            {
                ScriptManager.RegisterClientScriptBlock(this, typeof(string), "EXCEL匯入", "alert('匯入格式錯誤!');", true);
                return;
            }
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

    protected void btnCommitUpload_Click(object sender, EventArgs e)
    {
        ORD09_Facade facade09 = new ORD09_Facade();
        Tcheckagain.Value = "";
        string SID = hdUploadBatchNo.Value;
        ORD09_DropShipment ORD09_DTO = new ORD09_DropShipment();

        ORD09_DropShipment.UPLOAD_TEMPDataTable Showdt = ORD09_DTO.UPLOAD_TEMP;
        DataTable dtResult = new DataTable();
        dtResult.Columns.Add("STORENO", typeof(string));
        dtResult.Columns.Add("STORE_NAME", typeof(string));
        dtResult.Columns.Add("PRODNO", typeof(string));
        dtResult.Columns.Add("PRODNAME", typeof(string));
        dtResult.Columns.Add("DIS_QTY", typeof(string));
        dtResult.Columns.Add("Result", typeof(string));
        dtResult.Columns.Add("ATRQTY", typeof(string));
        if (Temp_Check.Value != "1")
        {
            checkagain(SID);

            if (Tcheckagain.Value != "1")
            {
                hidSubmit.Value = "Y";
            }
        }
        else
        {
            DataTable table = ORD09_Facade.geteditTable(SID);
            ORD09_Facade facade = new ORD09_Facade();
            ORD09_DropShipment ORD09_DTO1 = new ORD09_DropShipment();
            ORD09_DropShipment.UPLOAD_TEMPDataTable dtt;

            dtt = ORD09_DTO1.Tables["Upload_TEMP"] as ORD09_DropShipment.UPLOAD_TEMPDataTable;
            int x = 0;
            foreach (DataRow dr in table.Rows)
            {

                ORD09_DropShipment.UPLOAD_TEMPRow drr;
                drr = dtt.NewUPLOAD_TEMPRow();
                ORD09_DropShipment.UPLOAD_TEMPRow dtnewrow = Showdt.NewUPLOAD_TEMPRow();
                GridViewDataColumn col_QTY = new GridViewDataColumn();
                GridViewDataColumn col_StoreNO = new GridViewDataColumn();
                GridViewDataColumn col_ProdNO = new GridViewDataColumn();
                col_QTY = (GridViewDataColumn)gvMaster.Columns["DIS_QTY"];
                col_StoreNO = (GridViewDataColumn)gvMaster.Columns["STORE_NO"];
                col_ProdNO = (GridViewDataColumn)gvMaster.Columns["PRODNO"];
                ASPxTextBox t = gvMaster.FindRowCellTemplateControl(x, col_QTY, "ASPxTextBox1") as ASPxTextBox;
                PopupControl P = gvMaster.FindRowCellTemplateControl(x, col_StoreNO, "PopupControl1") as PopupControl;
                PopupControl P2 = gvMaster.FindRowCellTemplateControl(x, col_ProdNO, "PopupControl1") as PopupControl;
                string C_Prodno = P2.Text;
                string C_storeno = P.Text;
                string C_QTY = t.Text;
                drr.BATCH_NO = StringUtil.CStr(dr[0]);
                drr.USER_ID = StringUtil.CStr(dr[1]);
                drr.FINC_ID = StringUtil.CStr(dr[2]);
                drr.F1 = C_storeno;
                drr.F2 = "";
                if (!string.IsNullOrEmpty(C_storeno))
                {
                    drr.F2 = new Store_Facade().GetStoreName(C_storeno);
                }
                drr.F3 = C_Prodno;
                drr.F4 = "";
                if (!string.IsNullOrEmpty(C_Prodno))
                {
                    DataTable dtProdName = new Product_Facade().Query_ProductInfo(C_Prodno);
                    if (dtProdName.Rows.Count > 0)
                    {
                        drr.F4 = StringUtil.CStr(dtProdName.Rows[0]["PRODNAME"]);
                    }
                }
                drr.F5 = C_QTY;
                drr.F6 = StringUtil.CStr(dr["F6"]);

                dtt.Rows.Add(drr);
                x = x + 1;
            }
            facade09.UpdateOne_UPLOAD(ORD09_DTO1);
            Temp_Check.Value = "";
            checkdata(dtt, SID);
        }

    }

    protected void btnSave1_Click(object sender, EventArgs e)
    {
        doSaveData();
    }

    private void doSaveData()
    {
        ORD09_Facade facade09 = new ORD09_Facade();
        string SID = hdUploadBatchNo.Value;
        ORD09_DropShipment ORD09_DTO = new ORD09_DropShipment();

        ORD09_DropShipment.UPLOAD_TEMPDataTable Showdt = ORD09_DTO.UPLOAD_TEMP;
        DataTable dtResult = new DataTable();
        dtResult.Columns.Add("STORENO", typeof(string));
        dtResult.Columns.Add("STORE_NAME", typeof(string));
        dtResult.Columns.Add("PRODNO", typeof(string));
        dtResult.Columns.Add("PRODNAME", typeof(string));
        dtResult.Columns.Add("DIS_QTY", typeof(string));
        dtResult.Columns.Add("Result", typeof(string));
        dtResult.Columns.Add("ATRQTY", typeof(string));

        DataTable table = ORD09_Facade.SP_CHECK_DISTRIBUTES_ORDER(SID, logMsg.OPERATOR, "ORD09_IMPORT");

        int x = 0;
        foreach (DataRow dr in table.Rows)
        {
            GridViewDataColumn col_QTY = new GridViewDataColumn();
            GridViewDataColumn col_StoreNO = new GridViewDataColumn();
            GridViewDataColumn col_ProdNO = new GridViewDataColumn();
            col_QTY = (GridViewDataColumn)gvMaster.Columns["DIS_QTY"];
            col_StoreNO = (GridViewDataColumn)gvMaster.Columns["STORE_NO"];
            col_ProdNO = (GridViewDataColumn)gvMaster.Columns["PRODNO"];
            ASPxTextBox t = gvMaster.FindRowCellTemplateControl(x, col_QTY, "ASPxTextBox1") as ASPxTextBox;
            PopupControl P = gvMaster.FindRowCellTemplateControl(x, col_StoreNO, "PopupControl1") as PopupControl;
            PopupControl P2 = gvMaster.FindRowCellTemplateControl(x, col_ProdNO, "PopupControl1") as PopupControl;
            string C_Prodno = P2.Text;
            string C_storeno = P.Text;
            string C_QTY = t.Text;
            DataRow NewRow = dtResult.NewRow();
            NewRow["STORENO"] = C_storeno;
            NewRow["STORE_NAME"] = StringUtil.CStr(dr[4]);
            NewRow["PRODNO"] = C_Prodno;
            NewRow["PRODNAME"] = StringUtil.CStr(dr[6]);
            NewRow["DIS_QTY"] = C_QTY;
            NewRow["Result"] = StringUtil.CStr(dr[8]);
            dtResult.Rows.Add(NewRow);
            x = x + 1;
        }

        facade09.Insert_DISTRIBUTES_ORDER(dtResult, logMsg.OPERATOR);
        dtResult.Clear();
        ViewState["gvMaster"] = dtResult;
        gvMaster.DataSource = dtResult;
        gvMaster.DataBind();
        btnCommitUpload.Enabled = false; 
    }

    private void checkagain(string SID)
    {
        DataTable table = ORD09_Facade.geteditTable(SID);
        ORD09_Facade facade = new ORD09_Facade();
        ORD09_DropShipment ORD09_DTO1 = new ORD09_DropShipment();
        ORD09_DropShipment.UPLOAD_TEMPDataTable dtt;

        dtt = ORD09_DTO1.Tables["Upload_TEMP"] as ORD09_DropShipment.UPLOAD_TEMPDataTable;
        int x = 0;
        foreach (DataRow dr in table.Rows)
        {

            ORD09_DropShipment.UPLOAD_TEMPRow drr;
            drr = dtt.NewUPLOAD_TEMPRow();
            GridViewDataColumn col_QTY = new GridViewDataColumn();
            GridViewDataColumn col_StoreNO = new GridViewDataColumn();
            GridViewDataColumn col_ProdNO = new GridViewDataColumn();
            col_QTY = (GridViewDataColumn)gvMaster.Columns["DIS_QTY"];
            col_StoreNO = (GridViewDataColumn)gvMaster.Columns["STORE_NO"];
            col_ProdNO = (GridViewDataColumn)gvMaster.Columns["PRODNO"];
            ASPxTextBox t = gvMaster.FindRowCellTemplateControl(x, col_QTY, "ASPxTextBox1") as ASPxTextBox;
            PopupControl P = gvMaster.FindRowCellTemplateControl(x, col_StoreNO, "PopupControl1") as PopupControl;
            PopupControl P2 = gvMaster.FindRowCellTemplateControl(x, col_ProdNO, "PopupControl1") as PopupControl;
            string C_Prodno = P2.Text;
            string C_storeno = P.Text;
            string C_QTY = t.Text;
            drr.BATCH_NO = StringUtil.CStr(dr[0]);
            drr.USER_ID = StringUtil.CStr(dr[1]);
            drr.FINC_ID = StringUtil.CStr(dr[2]);
            drr.F1 = C_storeno;
            drr.F2 = "";
            if (!string.IsNullOrEmpty(C_storeno))
            {
                drr.F2 = new Store_Facade().GetStoreName(C_storeno);
            }
            drr.F3 = C_Prodno;
            drr.F4 = "";
            if (!string.IsNullOrEmpty(C_Prodno))
            {
                DataTable dtProdName = new Product_Facade().Query_ProductInfo(C_Prodno);
                if (dtProdName.Rows.Count > 0)
                {
                    drr.F4 = StringUtil.CStr(dtProdName.Rows[0]["PRODNAME"]);
                }
            }
            drr.F5 = C_QTY;
            drr.F6 = StringUtil.CStr(dr["F6"]);

            dtt.Rows.Add(drr);
            x = x + 1;
        }
        facade.UpdateOne_UPLOAD(ORD09_DTO1);

        checkdata(dtt, SID);

    }

    private void SaveTempData(DataTable dtExcel)
    {
        try
        {
            ORD09_DropShipment ORD09_DTO = new ORD09_DropShipment();
            ORD09_DropShipment.UPLOAD_TEMPDataTable dt = ORD09_DTO.UPLOAD_TEMP;
            ORD09_Facade facade = new ORD09_Facade();

            string UploadBatchNo = GuidNo.getUUID();   //上傳批號_UUID
            this.hdUploadBatchNo.Value = UploadBatchNo;
            string SIDD = GuidNo.getUUID();
            hdUploadBatchNo.Value = SIDD;

            //string strError = "";  //異常原因
            foreach (DataRow Importdr in dtExcel.Rows)
            {
                string getedituuid = GuidNo.getUUID();
                ORD09_DropShipment.UPLOAD_TEMPRow dr = dt.NewUPLOAD_TEMPRow();
                dr.BATCH_NO = SIDD;
                dr.USER_ID = logMsg.CREATE_USER;
                dr.FINC_ID = "ORD09_IMPORT";
                dr.STATUS = "";
                dr.F1 = StringUtil.CStr(Importdr["門市編號"]);
                dr.F2 = "";
                if (!string.IsNullOrEmpty(StringUtil.CStr(Importdr["門市編號"])))
                {
                    dr.F2 = new Store_Facade().GetStoreName(StringUtil.CStr(Importdr["門市編號"]));
                }
                dr.F3 = StringUtil.CStr(Importdr["商品料號"]);
                dr.F4 = "";
                if (!string.IsNullOrEmpty(StringUtil.CStr(Importdr["商品料號"])))
                {
                    DataTable dtProdName = new Product_Facade().Query_ProductInfo(StringUtil.CStr(Importdr["商品料號"]));
                    if (dtProdName.Rows.Count > 0)
                    {
                        dr.F4 = StringUtil.CStr(dtProdName.Rows[0]["PRODNAME"]);
                    }
                }

                dr.F5 = StringUtil.CStr(Importdr["主配量"]);
                dr.F6 = getedituuid;

                dt.Rows.Add(dr);
                ORD09_DTO.AcceptChanges();
            }

            //更新資料庫
            facade.AddNew_UPLoad(ORD09_DTO);

            checkdata(dt, SIDD);
        }
        catch (Exception)
        {
            ScriptManager.RegisterClientScriptBlock(this, typeof(string), "cmd", "alert('格式錯誤，請檢查!!');", true);
        }
    }

    protected void checkdata(DataTable dt, string SID)
    {
        btnCommitUpload.Enabled = true;
        ORD09_Facade facade09 = new ORD09_Facade();

        DataTable table = ORD09_Facade.SP_CHECK_DISTRIBUTES_ORDER(SID, logMsg.OPERATOR, "ORD09_IMPORT");
        DataTable checktable = ORD09_Facade.checknum(SID);

        ORD09_DropShipment ORD09_DTO = new ORD09_DropShipment();
        ORD09_DropShipment.UPLOAD_TEMPDataTable Showdt = ORD09_DTO.UPLOAD_TEMP;
        DataTable dtResult = new DataTable();
        dtResult.Columns.Add("STORE_NO", typeof(string));
        dtResult.Columns.Add("STORE_NAME", typeof(string));
        dtResult.Columns.Add("PRODNO", typeof(string));
        dtResult.Columns.Add("PRODNAME", typeof(string));
        dtResult.Columns.Add("DIS_QTY", typeof(string));
        dtResult.Columns.Add("Result", typeof(string));
        int x = 0;
        foreach (DataRow dr in table.Rows)
        {
            GridViewDataColumn col = new GridViewDataColumn();
            col = (GridViewDataColumn)gvMaster.Columns["DIS_QTY"];
            ASPxTextBox t = gvMaster.FindRowCellTemplateControl(x, col, "ASPxTextBox1") as ASPxTextBox;

            DataRow NewRow = dtResult.NewRow();
            NewRow["STORE_NO"] = StringUtil.CStr(dr[3]);
            NewRow["STORE_NAME"] = StringUtil.CStr(dr[4]);
            NewRow["PRODNO"] = StringUtil.CStr(dr[5]);
            NewRow["PRODNAME"] = StringUtil.CStr(dr[6]);
            NewRow["DIS_QTY"] = StringUtil.CStr(dr[7]);
            if (StringUtil.CStr(dr[8]) != "")
            {
                Temp_Check.Value = "1";
                Tcheckagain.Value = "1";
            }
            string CheckResult = "";
            foreach (DataRow drr in checktable.Rows)
            {
                if (StringUtil.CStr(dr[3]) == StringUtil.CStr(drr["F1"]) && StringUtil.CStr(dr[5]) == StringUtil.CStr(drr["F3"]))
                {
                    CheckResult = " 匯入資料重複";
                    Temp_Check.Value = "1";
                    Tcheckagain.Value = "1";
                }
            }
            NewRow["Result"] = StringUtil.CStr(dr[8]) + CheckResult;
            dtResult.Rows.Add(NewRow);
            CheckResult = "";
        }
        dtResult.DefaultView.Sort = "STORE_NO ASC";
        ViewState["gvMaster"] = dtResult;
        gvMaster.DataSource = dtResult;
        gvMaster.DataBind();


    }

    protected void gvMasterDV_HtmlDataCellPrepared(object sender, ASPxGridViewTableDataCellEventArgs e)
    {
        if (e.DataColumn.FieldName == "Result")
        {
            if (!string.IsNullOrEmpty(StringUtil.CStr(e.CellValue)))
            {
               // e.Cell.Style[HtmlTextWriterStyle.Color] = "red";
                ASPxTextBox T_txtResult = gvMaster.FindRowCellTemplateControl(e.VisibleIndex, (GridViewDataColumn)((ASPxGridView)sender).Columns["Result"], "txtResult") as ASPxTextBox;
                T_txtResult.ForeColor = System.Drawing.Color.Red;
                //取得控制項裏的值出來
                GridViewDataColumn col = new GridViewDataColumn();
                col = (GridViewDataColumn)((ASPxGridView)sender).Columns["DIS_QTY"];
                ASPxTextBox t = gvMaster.FindRowCellTemplateControl(e.VisibleIndex, col, "ASPxTextBox1") as ASPxTextBox;
                t.ForeColor = System.Drawing.Color.Red;

                         
            }
        }
    }

    protected void gvMaster_HtmlRowPrepared(object sender, ASPxGridViewTableRowEventArgs e)
    {
      
        if (e.RowType == GridViewRowType.Data)
        {
           
            ASPxTextBox T_txtResult = gvMaster.FindRowCellTemplateControl(e.VisibleIndex, (GridViewDataColumn)((ASPxGridView)sender).Columns["Result"], "txtResult") as ASPxTextBox;
            if (T_txtResult.Text.Length == 0)
            {
                PopupControl P_STORE_NO = gvMaster.FindRowCellTemplateControl(e.VisibleIndex, (GridViewDataColumn)((ASPxGridView)sender).Columns["STORE"], "PopupControl1") as PopupControl;
                P_STORE_NO.Enabled = false;

                PopupControl P_PRODNO = gvMaster.FindRowCellTemplateControl(e.VisibleIndex, (GridViewDataColumn)((ASPxGridView)sender).Columns["PRODNO"], "PopupControl1") as PopupControl;
                P_PRODNO.Enabled = false;

                ASPxTextBox T_DIS_QTY = gvMaster.FindRowCellTemplateControl(e.VisibleIndex, (GridViewDataColumn)((ASPxGridView)sender).Columns["DIS_QTY"], "ASPxTextBox1") as ASPxTextBox;
                T_DIS_QTY.ClientEnabled = false;
            }
        }

    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        gvMaster.DataSource = "";
        gvMaster.DataBind();
    }

    //ajax 呼當前網頁的方式
    [System.Web.Services.WebMethod()]
    [System.Web.Script.Services.ScriptMethod()]
    static public string getPRODINFO(string PRODNO)
    {

        //DataTable dt = new Product_Facade().Query_VW_PRODUCT(PRODNO);
        //2011/04/17 Tina：商品料號存在於FETT_DROPSHIPITEM_ONBOARD TABLE中，才是DropShipment主配商品
        //2011/04/18 Tina：取消4/17的判斷，改成DropShipment主配須判斷 PRODUCT 的 DS_FLAG = 'Y'
        DataTable dt = new Product_Facade().Query_DSProdInfo(PRODNO);
        string r = "";
        if (dt.Rows.Count > 0)
        {
            r = StringUtil.CStr(dt.Rows[0]["PRODNAME"]);
        }
        return r;
    }

    //ajax 呼當前網頁的方式
    [System.Web.Services.WebMethod()]
    [System.Web.Script.Services.ScriptMethod()]
    static public string getStoreInfo(string STORE_NO)
    {
        string strInfo = "";
        if (!string.IsNullOrEmpty(STORE_NO))
        {
            DataTable _dt = new Store_Facade().Query_StoreInfo(STORE_NO);
            if (_dt.Rows.Count > 0)
            {
                strInfo = StringUtil.CStr(_dt.Rows[0]["STORENAME"]);
            }
        }

        return strInfo;
    }
}
