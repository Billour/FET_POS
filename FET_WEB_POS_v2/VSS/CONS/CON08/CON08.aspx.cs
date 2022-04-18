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

using System.Web.UI.HtmlControls;
using FET.POS.Model.Facade.FacadeImpl;
using FET.POS.Model.Common;
public partial class VSS_CONS_CON08 : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        doChangeMaster();
        if (!IsPostBack)
        {
            btnCommitUpload.Enabled = false;
        }
    }

    private void doChangeMaster()
    {
        if (gvMaster.VisibleRowCount > 0)
        {
            DataTable dtINVD = Session["gvMaster"] as DataTable;
            DataRow drINVD;
            for (int i = 0; i < gvMaster.VisibleRowCount; i++)
            {
                if (((ASPxLabel)gvMaster.FindRowCellTemplateControl(i, (GridViewDataColumn)gvMaster.Columns["UUID"], "txtUUID")) != null)
                {
                    string UUID_KEY = ((ASPxLabel)gvMaster.FindRowCellTemplateControl(i, (GridViewDataColumn)gvMaster.Columns["UUID"], "txtUUID")).Text;
                    drINVD = dtINVD.Select("UUID='" + UUID_KEY + "'")[0];
                    string C_Prodno = ((PopupControl)gvMaster.FindRowCellTemplateControl(i, (GridViewDataColumn)gvMaster.Columns["PRODNO"], "PopupControl1")).Text;
                    string C_storeno = ((PopupControl)gvMaster.FindRowCellTemplateControl(i, (GridViewDataColumn)gvMaster.Columns["STORENO"], "PopupControl1")).Text;
                    string C_SUPPNO = ((PopupControl)gvMaster.FindRowCellTemplateControl(i, (GridViewDataColumn)gvMaster.Columns["SUPPNO"], "PopupControl1")).Text;
                    string C_QTY = ((ASPxTextBox)gvMaster.FindRowCellTemplateControl(i, (GridViewDataColumn)gvMaster.Columns["DIS_QTY"], "DIS_QTY")).Text;
                    string C_SUPPNAME = ((ASPxTextBox)gvMaster.FindRowCellTemplateControl(i, (GridViewDataColumn)gvMaster.Columns["SUPPNAME"], "txtSuppName")).Text;
                    string C_STORENAME = ((ASPxTextBox)gvMaster.FindRowCellTemplateControl(i, (GridViewDataColumn)gvMaster.Columns["STORENAME"], "txtStoreName")).Text;
                    string C_PRODNAME = ((ASPxTextBox)gvMaster.FindRowCellTemplateControl(i, (GridViewDataColumn)gvMaster.Columns["PRODNAME"], "txtProdName")).Text;

                    drINVD["SUPPNO"] = C_SUPPNO.ToString();
                    drINVD["SUPPNAME"] = C_SUPPNAME.ToString();
                    drINVD["STORENO"] = C_storeno.ToString();
                    drINVD["STORENAME"] = C_STORENAME.ToString();
                    drINVD["PRODNO"] = C_Prodno.ToString();
                    drINVD["PRODNAME"] = C_PRODNAME.ToString();
                    drINVD["DIS_QTY"] = C_QTY.ToString();
                }
            }
            dtINVD.AcceptChanges();
            Session["gvMaster"] = dtINVD;
        }
    }

    protected void btnImport_Click(object sender, EventArgs e)
    {
        if (this.FileUpload.HasFile)
        {
            String fileName = this.FileUpload.FileName;

            if ((fileName.Trim().Substring(fileName.Trim().IndexOf('.')) != ".xls"))
            {
                ScriptManager.RegisterClientScriptBlock(this, typeof(string), "EXCEL匯入", "alert('檔案格式錯誤!');", true);
                return;
            }
            HSSFWorkbook workbook = new HSSFWorkbook(this.FileUpload.FileContent);
            HSSFSheet sheet = workbook.GetSheetAt(0) as HSSFSheet;
            DataTable table = new DataTable();
            HSSFRow headerRow = sheet.GetRow(0) as HSSFRow;
            int cellCount = headerRow.LastCellNum;
            if (cellCount != 4)
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
                        dataRow[j] = row.GetCell(j).ToString();
                }
                table.Rows.Add(dataRow);
            }
            workbook = null;
            sheet = null;
            SaveTempData(table);  //將匯入的資料存入Temp Table中
        }
    }

    private void SaveTempData(DataTable dtExcel)
    {
        try
        {

            ORD09_DropShipment CON08_DTO = new ORD09_DropShipment();
            ORD09_DropShipment.UPLOAD_TEMPDataTable dt = CON08_DTO.UPLOAD_TEMP;
            ORD09_Facade facade = new ORD09_Facade();
            Session["SUDD"] = GuidNo.getUUID();
            foreach (DataRow Importdr in dtExcel.Rows)
            {

                string getedituuid = GuidNo.getUUID();
                ORD09_DropShipment.UPLOAD_TEMPRow dr = dt.NewUPLOAD_TEMPRow();
                dr.BATCH_NO = Session["SUDD"].ToString();
                dr.USER_ID = logMsg.CREATE_USER;
                dr.FINC_ID = "CON08_IMPORT";
                dr.STATUS = "";
                dr.F1 = Importdr["廠商代號"].ToString();
                dr.F2 = "";
                if (!string.IsNullOrEmpty(Importdr["廠商代號"].ToString()))
                {
                    DataTable dtSuppData = new Supplier_Facade().Query_SuppData(Importdr["廠商代號"].ToString());
                    if (dtSuppData.Rows.Count > 0)
                    {
                        dr.F2 = dtSuppData.Rows[0]["SUPP_NAME"].ToString();
                    }
                }
                dr.F3 = Importdr["店組代碼"].ToString();
                dr.F4 = "";
                if (!string.IsNullOrEmpty(Importdr["店組代碼"].ToString()))
                {
                    dr.F4 = new Store_Facade().GetStoreName(Importdr["店組代碼"].ToString());
                }
                dr.F5 = Importdr["商品料號"].ToString();
                dr.F6 = "";
                if (!string.IsNullOrEmpty(Importdr["商品料號"].ToString()))
                {
                    DataTable dtProdName = new Product_Facade().Query_ProductInfo(Importdr["商品料號"].ToString());
                    if (dtProdName.Rows.Count > 0)
                    {
                        dr.F6 = dtProdName.Rows[0]["PRODNAME"].ToString();
                    }
                }

                dr.F7 = Importdr["主配量"].ToString();
                dr.F8 = getedituuid;

                dt.Rows.Add(dr);
                CON08_DTO.AcceptChanges();
            }

            //更新資料庫
            facade.AddNew_UPLoad(CON08_DTO);

            checkdata(dt, Session["SUDD"].ToString());
        }
        catch (Exception)
        {

            ScriptManager.RegisterClientScriptBlock(this, typeof(string), "cmd", "alert('格式錯誤，請檢查!!');", true);
        }
    }

    protected void checkdata(DataTable dt, string SID)
    {
        btnCommitUpload.Enabled = true;
        CON08_Facade facade09 = new CON08_Facade();
        DataTable table = CON08_Facade.SP_CHECK_CONSIGNMENT_GOODS_PS(SID, logMsg.OPERATOR, "CON08_IMPORT");
        Session["gvMaster"] = table;
        gvMaster.DataSource = table;
        gvMaster.DataBind();
    }

    protected void btnCommitUpload_Click(object sender, EventArgs e)
    {
        CON08_Facade facade = new CON08_Facade();

        string SID = Session["SUDD"].ToString();
        ORD09_DropShipment CON08_DTO = new ORD09_DropShipment();

        ORD09_DropShipment.UPLOAD_TEMPDataTable Showdt = CON08_DTO.UPLOAD_TEMP;
        DataTable dtResult = new DataTable();
        dtResult.Columns.Add("STORENO", typeof(string));
        dtResult.Columns.Add("STORE_NAME", typeof(string));
        dtResult.Columns.Add("PRODNO", typeof(string));
        dtResult.Columns.Add("PRODNAME", typeof(string));
        dtResult.Columns.Add("DIS_QTY", typeof(string));
        dtResult.Columns.Add("ERR_DESC", typeof(string));

        DataTable table = ORD09_Facade.geteditTable(SID);
        ORD09_DropShipment CON08_DTO1 = new ORD09_DropShipment();
        ORD09_DropShipment.UPLOAD_TEMPDataTable dtt;

        dtt = CON08_DTO1.Tables["Upload_TEMP"] as ORD09_DropShipment.UPLOAD_TEMPDataTable;
        int x = 0;
        foreach (DataRow dr in table.Rows)
        {

            ORD09_DropShipment.UPLOAD_TEMPRow drr;
            drr = dtt.NewUPLOAD_TEMPRow();
            ORD09_DropShipment.UPLOAD_TEMPRow dtnewrow = Showdt.NewUPLOAD_TEMPRow();
            string C_Prodno = ((PopupControl)gvMaster.FindRowCellTemplateControl(x, (GridViewDataColumn)gvMaster.Columns["PRODNO"], "PopupControl1")).Text;
            string C_storeno = ((PopupControl)gvMaster.FindRowCellTemplateControl(x, (GridViewDataColumn)gvMaster.Columns["STORENO"], "PopupControl1")).Text;
            string C_SUPPNO = ((PopupControl)gvMaster.FindRowCellTemplateControl(x, (GridViewDataColumn)gvMaster.Columns["SUPPNO"], "PopupControl1")).Text;
            string C_QTY = ((ASPxTextBox)gvMaster.FindRowCellTemplateControl(x, (GridViewDataColumn)gvMaster.Columns["DIS_QTY"], "DIS_QTY")).Text;

            drr.BATCH_NO = dr[0].ToString();
            drr.USER_ID = dr[1].ToString();
            drr.FINC_ID = dr[2].ToString();
            drr.F1 = C_SUPPNO;
            drr.F2 = "";
            if (!string.IsNullOrEmpty(C_SUPPNO))
            {
                DataTable dtSuppData = new Supplier_Facade().Query_SuppData(C_SUPPNO);
                if (dtSuppData.Rows.Count > 0)
                {
                    drr.F2 = dtSuppData.Rows[0]["SUPP_NAME"].ToString();
                }
            }
            drr.F3 = C_storeno;
            drr.F4 = "";
            if (!string.IsNullOrEmpty(C_storeno))
            {
                drr.F4 = new Store_Facade().GetStoreName(C_storeno);
            }
            drr.F5 = C_Prodno;
            drr.F6 = "";
            if (!string.IsNullOrEmpty(C_Prodno))
            {
                DataTable dtProdName = new Product_Facade().Query_ProductInfo(C_Prodno);
                if (dtProdName.Rows.Count > 0)
                {
                    drr.F6 = dtProdName.Rows[0]["PRODNAME"].ToString();
                }
            }

            drr.F7 = C_QTY;
            drr.F8 = dr["F8"].ToString();
            dtt.Rows.Add(drr);
            x = x + 1;
        }
        facade.UpdateOne_UPLOAD(CON08_DTO1);

        checkdata(dtt, SID);
        DataTable DT = (DataTable)Session["gvMaster"];
        if (DT.Select("ERR_DESC NOT IS NULL").Count() == 0)
        {

            string strSTNO = SerialNo.GenNo("CA{0}"); //"ST{0}-100815001";            
            strSTNO = string.Format(strSTNO, DT.Rows[0]["SUPPNO"].ToString()); //{0} 帶入廠商編號
            facade.Insert_CSM_DISTRIBUTES_ORDER_M(DT, logMsg.OPERATOR, strSTNO);
            ScriptManager.RegisterClientScriptBlock(this, typeof(string), "MESSAGE", "alert('存檔完成!');", true);
            btnCommitUpload.Enabled = false;
        }
    }

    protected void gvMaster_HtmlRowPrepared(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewTableRowEventArgs e)
    {
        if (e.RowType != GridViewRowType.Data) return;
        if (e.GetValue("ERR_DESC").ToString() != string.Empty)
            e.Row.ForeColor = System.Drawing.Color.Red;
    }

    protected void gvMaster_HtmlDataCellPrepared(object sender, ASPxGridViewTableDataCellEventArgs e)
    {
        if (e.DataColumn.FieldName == "ERR_DESC")
        {
            if (!string.IsNullOrEmpty(e.CellValue.ToString()))
            {
                e.Cell.Style[HtmlTextWriterStyle.Color] = "red";
                //取得控制項裏的值出來
                GridViewDataColumn col = new GridViewDataColumn();
                col = (GridViewDataColumn)((ASPxGridView)sender).Columns["DIS_QTY"];
                ASPxTextBox t = gvMaster.FindRowCellTemplateControl(e.VisibleIndex, col, "DIS_QTY") as ASPxTextBox;
                t.ForeColor = System.Drawing.Color.Red;
            }
        }
    }

    protected void gvMaster_HtmlRowCreated(object sender, ASPxGridViewTableRowEventArgs e)
    {
        if (e.RowType == GridViewRowType.Data)
        {
            ASPxTextBox DIS_QTY = e.Row.FindChildControl  <ASPxTextBox>("DIS_QTY");
            if (DIS_QTY != null) { 
                DIS_QTY.Validate(); 
            }
        }
    }

    protected void gvMaster_PageIndexChanged(object sender, EventArgs e)
    {
        gvMaster.DataSource = Session["gvMaster"];
        gvMaster.DataBind();

    }

    //ajax 呼當前網頁的方式
    [System.Web.Services.WebMethod()]
    [System.Web.Script.Services.ScriptMethod()]
    static public string getPRODINFO(string PRODNO)
    {
        DataTable dt = new Product_Facade().Query_ProductInfo(PRODNO);
        string r = "";
        if (dt.Rows.Count > 0)
        {
            r = dt.Rows[0]["PRODNAME"].ToString();
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
                strInfo = _dt.Rows[0]["STORENAME"].ToString();
            }
        }

        return strInfo;
    }

    //ajax 呼當前網頁的方式
    [System.Web.Services.WebMethod()]
    [System.Web.Script.Services.ScriptMethod()]
    static public string getSUPPINFO(string SUPPNO)
    {
        string strInfo = "";
        if (!string.IsNullOrEmpty(SUPPNO))
        {
            DataTable _dt = new Supplier_Facade().Query_SuppData(SUPPNO);
            if (_dt.Rows.Count > 0)
            {
                strInfo = _dt.Rows[0]["SUPP_NAME"].ToString();
            }
        }

        return strInfo;
    }
}
