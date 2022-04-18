using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using DevExpress.Web.ASPxEditors;
using Advtek.Utility;
using DevExpress.Web;
using DevExpress.Web.ASPxGridView;
using DevExpress.Web.ASPxPager;
using FET.POS.Model.Facade.FacadeImpl;
using FET.POS.Model.Common;
using FET.POS.Model.DTO;

using System.Web.UI.HtmlControls;
using NPOI.HSSF.UserModel;

public partial class VSS_LEA_LEA01_Import : Popup
{
    private int errorCount = -1; //錯誤筆數

    //UUID LEASE_M
    private static string gropStrUUID;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {            
            //bindMaster();
            //bindDetailData();
        }
    }
    
    private string SeetName(int number)
    {
        string strOut = "";

        if (number == 1)
            strOut = "基本資料";
        else if (number == 2)
            strOut = "賠償項目";
        else if (number == 3)
            strOut = "折扣項目";

        return strOut;
    }

    /// <summary>
    /// 確認按鈕
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnOK_Click(object sender, EventArgs e)
    {

        if (!this.FileUpload.HasFile)
        { return; }
        String fileName = this.FileUpload.FileName;

        #region Check ...

        if ((fileName.Trim().Substring(fileName.Trim().IndexOf('.')) != ".xls"))
        {
            ScriptManager.RegisterClientScriptBlock(this, typeof(string), "EXCEL匯入", "alert('檔案格式錯誤!');", true);
            return;
        }

        if (!CheckBox1.Checked)
        {
            ScriptManager.RegisterClientScriptBlock(this, typeof(string), "EXCEL匯入", "alert('請勾選基本資料!');", true);
            return;
        }

        for (int i = 1; i <= 3; i++)
        {
            ASPxCheckBox chk = (ASPxCheckBox)FindControl("CheckBox" + StringUtil.CStr(i));
            ASPxTextBox txb = (ASPxTextBox)FindControl("TextBox" + StringUtil.CStr(i));
            if (chk.Checked)
            {
                if (string.IsNullOrEmpty(txb.Text))
                {
                    txb.Focus();
                    ScriptManager.RegisterClientScriptBlock(this, typeof(string), "EXCEL匯入", "alert('" + SeetName(i) +
                        "工作表請輸入數字!');", true);
                    return;
                }
            }
        }

        #endregion

        HSSFWorkbook workbook = new HSSFWorkbook(this.FileUpload.FileContent);
        HSSFSheet sheet = workbook.GetSheetAt(0) as HSSFSheet;

        for (int i = 1; i <= 3; i++)
        {
            ASPxCheckBox chk = (ASPxCheckBox)FindControl("CheckBox" + StringUtil.CStr(i));
            ASPxTextBox txb = (ASPxTextBox)FindControl("TextBox" + StringUtil.CStr(i));
            if (chk.Checked)
            {
                if (string.IsNullOrEmpty(txb.Text))
                {
                    txb.Focus();
                    ScriptManager.RegisterClientScriptBlock(this, typeof(string), "EXCEL匯入", "alert('" + SeetName(i) +
                        "工作表請輸入數字!');", true);
                    return;
                }

                try
                {
                    sheet = workbook.GetSheetAt(int.Parse(txb.Text) - 1) as HSSFSheet;
                }
                catch (Exception ex)
                {
                    ScriptManager.RegisterClientScriptBlock(this, typeof(string), "EXCEL匯入", "alert('" + SeetName(i) +
                        "工作表輸入錯誤!');", true);
                }
                SaveSheetData(sheet, i);
            }
        }

        workbook = null;
        sheet = null;

        if (errorCount == 0)
            btnImport.Visible = true;
        else
            btnImport.Visible = false;

    }

    /// <summary>
    /// 匯入
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnImport_Click(object sender, EventArgs e)
    {
        LEA01_Facade LEA01_F = new LEA01_Facade();
        int intResult = 0;
        LEA01_LEASE_M.LEASE_MDataTable dtLEASE = null;
        LEA01_LEASE_M.RENT_INDEMNIFY_ITEMSDataTable dtInd = null;
        LEA01_LEASE_M.RENT_DISCOUNT_ITEMSDataTable dtDiscount = null;
        //try
        //{
        dtLEASE = insertLeaseM();
        dtInd = InsertIndemnify();
        dtDiscount = InsertDiscount();
        intResult = LEA01_F.SaveOut(dtLEASE, dtInd, dtDiscount);

        string strEmpName = new Employee_Facade().GetEmpName(logMsg.MODI_USER);
        
        ScriptManager.RegisterClientScriptBlock(this, typeof(string), "儲存", "alert('存檔完成!');", true);
        //}
        //catch (Exception ex)
        //{
        //    ScriptManager.RegisterClientScriptBlock(this, typeof(string), "儲存", "alert('存檔失敗!');", true);
        //}
    }

    private void SaveSheetData(HSSFSheet sheet, int intSP)
    {
        DataTable table = new DataTable();
        HSSFRow headerRow = sheet.GetRow(0) as HSSFRow;
        int cellCount = headerRow.LastCellNum;
        for (int i = headerRow.FirstCellNum; i < cellCount; i++)
        {
            DataColumn column = new DataColumn(headerRow.GetCell(i).StringCellValue.Trim());
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
        SaveTempData(table, intSP);

    }

    private void SaveTempData(DataTable dtExcel, int j)
    {        
        try
        {
            string SP_Name = "";
            ORD09_DropShipment DTO = new ORD09_DropShipment();
            ORD09_DropShipment.UPLOAD_TEMPDataTable dt = DTO.UPLOAD_TEMP;
            ORD09_Facade facade = new ORD09_Facade();

            Session["SUDD"] = GuidNo.getUUID();
            ASPxGridView gvCSM = new ASPxGridView(); //存要顯示的GridView
            string strFincID = "LEA01_IMPORT";

            foreach (DataRow Importdr in dtExcel.Rows)
            {
                ORD09_DropShipment.UPLOAD_TEMPRow dr = dt.NewUPLOAD_TEMPRow();

                switch (j)
                {
                    #region
                    case 1:
                        //基本資料
                        dr.BATCH_NO = StringUtil.CStr(Session["SUDD"]);
                        //存舊的UUID 方便在DB 內查詢
                        Session["C_BATCH_NO"] = StringUtil.CStr(Session["SUDD"]);

                        dr.USER_ID = logMsg.CREATE_USER;
                        dr.FINC_ID = strFincID;
                        dr.STATUS = "";
                        dr[6] = GuidNo.getUUID();//F3

                        dr[7] = StringUtil.CStr(Importdr["產品類別"]);//F4
                        dr[8] = StringUtil.CStr(Importdr["產品名稱"]);
                        dr[9] = StringUtil.CStr(Importdr["外部廠商代碼"]);
                        dr[10] = StringUtil.CStr(Importdr["外部廠商名稱"]);
                        dr[11] = StringUtil.CStr(Importdr["租金料號"]);
                        dr[12] = StringUtil.CStr(Importdr["日租金額"]);
                        dr[13] = StringUtil.CStr(Importdr["保證金料號"]);
                        dr[14] = StringUtil.CStr(Importdr["保證金"]);
                        dr[15] = StringUtil.CStr(Importdr["賠償金料號"]);
                        dr[16] = StringUtil.CStr(Importdr["有效期間(起)"]);
                        dr[17] = StringUtil.CStr(Importdr["有效期間(訖)"]);
                        
                        SP_Name = "SP_CHECK_LEASE_M";
                        gvCSM = gvLEASE;
                        dt.Rows.Add(dr);
                        DTO.AcceptChanges();
                        break;

                    case 2:
                        //賠償項目
                        dr.BATCH_NO = StringUtil.CStr(Session["SUDD"]);
                        dr.USER_ID = logMsg.CREATE_USER;
                        dr.FINC_ID = strFincID;
                        dr.STATUS = "";
                        dr[6] = GuidNo.getUUID();

                        dr[7] = StringUtil.CStr(Importdr["產品名稱"]);
                        dr[8] = StringUtil.CStr(Importdr["賠償項目"]);
                        dr[9] = StringUtil.CStr(Importdr["金額"]);

                        SP_Name = "SP_CHECK_RENT_INDEMNIFY_ITEMS";
                        gvCSM = gvIndemnify;
                        dt.Rows.Add(dr);
                        DTO.AcceptChanges();

                        break;

                    default:
                    //折扣項目
                        dr.BATCH_NO = StringUtil.CStr(Session["SUDD"]);
                        dr.USER_ID = logMsg.CREATE_USER;
                        dr.FINC_ID = strFincID;
                        dr.STATUS = "";
                        dr[6] = GuidNo.getUUID();
                        SP_Name = "SP_CHECK_RENT_DISCOUNT_ITEMS";
                        gvCSM = gvDiscount;

                        dr[7] = StringUtil.CStr(Importdr["產品名稱"]);
                        dr[8] = StringUtil.CStr(Importdr["折扣料號"]);
                        dr[9] = StringUtil.CStr(Importdr["折扣名稱"]);
                        dr[10] = StringUtil.CStr(Importdr["折扣金額"]);
                        dr[11] = StringUtil.CStr(Importdr["折扣比率"]);
                        dr[12] = StringUtil.CStr(Importdr["成本中心"]);
                        dr[13] = StringUtil.CStr(Importdr["會計科目"]);

                        dt.Rows.Add(dr);
                        DTO.AcceptChanges();
                        break;

                    #endregion
                }

            }

            //更新資料庫
            facade.AddNew_UPLoad(DTO);

            checkdata(dt, StringUtil.CStr(Session["SUDD"]), gvCSM, SP_Name, j);
        }
        catch (Exception)
        {
            //btnImport.Visible = false;
            ScriptManager.RegisterClientScriptBlock(this, typeof(string), "cmd", "alert(" + SeetName(j) + "'格式錯誤，請檢查!!');", true);

        }
    }

    //check data
    protected void checkdata(DataTable dt, string SID, ASPxGridView gv, string SP_Name, int intLabel)
    {
        //btnCommitUpload.Enabled = true;
        string C_BATCH_ID = "";
        if (SP_Name != "SP_CHECK_LEASE_M")
            C_BATCH_ID = StringUtil.CStr(Session["C_BATCH_NO"]);
        DataTable table = LEA01_Facade.SP_CHECK_CONSIGNMENT_GOODS_PS(SP_Name, SID, C_BATCH_ID, logMsg.OPERATOR, "LEA01_IMPORT");

        Session[StringUtil.CStr(gv.ID)] = table;
        gv.DataSource = table;
        gv.DataBind();

        //設定 資料筆數:  錯誤筆數:
        SetAboutCout(table, gv, intLabel);


    }

    //設定 資料筆數:  錯誤筆數:
    private int SetAboutCout(DataTable table, ASPxGridView gv, int intLabel)
    {
        int index = (intLabel * 2 - 1);

        //((ASPxLabel)gv.FindTitleTemplateControl("ASPxLabel" + StringUtil.CStr(index))).Text = "資料筆數：" +
        //    StringUtil.CStr(table.Rows.Count) + " 筆";

        DataRow[] drs = table.Select(" ERR_DESC <>''");
        if (drs.Length > 0)//失敗筆數 第一個分頁的Grid
        {
            //((ASPxLabel)gv.FindTitleTemplateControl("ASPxLabel" + StringUtil.CStr(index + 1))).Text = "錯誤筆數：" +
            //    StringUtil.CStr(table.Rows.Count) + " 筆";
        }

        if (drs.Length == 0)
            errorCount += drs.Length;

        return errorCount;
    }

    #region Grid事件
    protected void gvLEASE_HtmlDataCellPrepared(object sender, ASPxGridViewTableDataCellEventArgs e)
    {
        //if (e.DataColumn.FieldName == "ERR_DESC")
        //{
        //    if (!string.IsNullOrEmpty(StringUtil.CStr(e.CellValue)))
        //    {
        //        e.Cell.Style[HtmlTextWriterStyle.Color] = "red";
        //    }
        //}
    }

    protected void gvLEASE_HtmlRowCreated(object sender, ASPxGridViewTableRowEventArgs e)
    {
    }

    protected void gvLEASE_PageIndexChanged(object sender, EventArgs e)
    {
        gvLEASE.DataSource = Session["gvLEASE"];
        gvLEASE.DataBind();

    }

    protected void gvIndemnify_HtmlDataCellPrepared(object sender, ASPxGridViewTableDataCellEventArgs e)
    {
        //if (e.DataColumn.FieldName == "ERR_DESC")
        //{
        //    if (!string.IsNullOrEmpty(StringUtil.CStr(e.CellValue)))
        //    {
        //        e.Cell.Style[HtmlTextWriterStyle.Color] = "red";
        //    }
        //}
    }

    protected void gvIndemnify_HtmlRowCreated(object sender, ASPxGridViewTableRowEventArgs e)
    {

    }

    protected void gvIndemnify_PageIndexChanged(object sender, EventArgs e)
    {
        gvIndemnify.DataSource = Session["gvIndemnify"];
        gvIndemnify.DataBind();

    }

    protected void gvDiscount_PageIndexChanged(object sender, EventArgs e)
    {
        gvDiscount.DataSource = Session["gvDiscount"];
        gvDiscount.DataBind();
    }
    #endregion

    #region doInsert 區
    private LEA01_LEASE_M.LEASE_MDataTable insertLeaseM()
    {
        LEA01_LEASE_M.LEASE_MDataTable dtLease = null;
        LEA01_LEASE_M.LEASE_MRow drLease;

        dtLease = new LEA01_LEASE_M.LEASE_MDataTable();
        DataTable dtData = (DataTable)Session["gvLEASE"];

        //單PK strUUID
        string strUUID = GuidNo.getUUID();
        gropStrUUID = strUUID; //存UUID

        if (dtData.Rows.Count > 0)
        {
            foreach (DataRow dr in dtData.Rows)
            {
                drLease = dtLease.NewLEASE_MRow();
                drLease["LEASE_ID"] = gropStrUUID;
                //drLease["SEQNO"] = 1;

                //WHEN U.F4 = '1' THEN '漫遊租賃' 
                //             WHEN U.F4 = '2' THEN '維修租賃'
                string strType = StringUtil.CStr(dr["LEASE_TYPE"]) == "漫遊租賃"? "1" : "2" ;
                drLease["LEASE_TYPE"] = strType;
                drLease["SUPP_ID"] = StringUtil.CStr(dr["SUPP_NO"]);
                drLease["RENT_PRODNO"] = StringUtil.CStr(dr["RENT_PRODNO"]);

                drLease["DAILY_RENT_PRICE"] = StringUtil.CStr(dr["DAILY_RENT_PRICE"]);
                drLease["EARNEST_PRODNO"] = StringUtil.CStr(dr["EARNEST_PRODNO"]);

                drLease["EARNEST_MONEY"] = StringUtil.CStr(dr["EARNEST_MONEY"]);

                drLease["INDEMNITY_PRODNO"] = StringUtil.CStr(dr["INDEMNITY_PRODNO"]);

                if (dr["S_DATE"] != null)
                {
                    if (StringUtil.CStr(dr["S_DATE"]).Length < 8)
                    { continue; }
                    string strTmp = StringUtil.CStr(dr["S_DATE"]);
                    string strApl = strTmp.Substring(0,4) + "/" +strTmp.Substring(4,2) + "/" + strTmp.Substring(6,2);
                    drLease["S_DATE"] = DateTime.Parse(strApl);
                }

                if (dr["E_DATE"] != null)
                {
                    if (StringUtil.CStr(dr["E_DATE"]).Length < 8)
                    { continue; }
                    string strTmp = StringUtil.CStr(dr["E_DATE"]);
                    string strApl = strTmp.Substring(0, 4) + "/" + strTmp.Substring(4, 2) + "/" + strTmp.Substring(6, 2);
                    drLease["E_DATE"] = DateTime.Parse(strApl);
                }


                drLease["MODI_USER"] = logMsg.MODI_USER;
                drLease["MODI_DTM"] = Convert.ToDateTime(System.DateTime.Now);
                drLease["CREATE_USER"] = drLease["MODI_USER"];
                drLease["CREATE_DTM"] = drLease["MODI_DTM"];
                dtLease.Rows.Add(drLease);
            }
        }

        return dtLease;
    }
    //賠償項目
    private LEA01_LEASE_M.RENT_INDEMNIFY_ITEMSDataTable InsertIndemnify()
    {
        LEA01_LEASE_M.RENT_INDEMNIFY_ITEMSDataTable dtIndy = null;
        DataTable dtProd = null;
        dtProd = (DataTable)Session["gvIndemnify"];

        dtIndy = new LEA01_LEASE_M.RENT_INDEMNIFY_ITEMSDataTable();
        LEA01_LEASE_M.RENT_INDEMNIFY_ITEMSRow drIndy;

        if (dtProd.Rows.Count > 0)
        {
            foreach (DataRow dr in dtProd.Rows)
            {
                drIndy = dtIndy.NewRENT_INDEMNIFY_ITEMSRow();
                drIndy["RENT_INDEMNIFY_ITEMS"] = StringUtil.CStr(dr["RENT_INDEMNIFY_ITEMS"]);
                drIndy["LEASE_ID"] = gropStrUUID;
                drIndy["IND_ITEM_CODE"] = "1";
                drIndy["SEQNO"] = 1;

                drIndy["IND_ITEM_NAME"] = StringUtil.CStr(dr["IND_ITEM_NAME"]);
                drIndy["IND_UNIT_PRICE"] = StringUtil.CStr(dr["IND_UNIT_PRICE"]);
                drIndy["MODI_USER"] = logMsg.MODI_USER;
                drIndy["MODI_DTM"] = Convert.ToDateTime(System.DateTime.Now);
                drIndy["CREATE_USER"] = drIndy["MODI_USER"];
                drIndy["CREATE_DTM"] = drIndy["MODI_DTM"];
                dtIndy.Rows.Add(drIndy);
            }
        }
        return dtIndy;
    }
    //折扣項目
    private LEA01_LEASE_M.RENT_DISCOUNT_ITEMSDataTable InsertDiscount()
    {
        LEA01_LEASE_M.RENT_DISCOUNT_ITEMSDataTable dtIndy = null;
        DataTable dtProd = null;
        dtProd = (DataTable)Session["gvDiscount"];

        dtIndy = new LEA01_LEASE_M.RENT_DISCOUNT_ITEMSDataTable();
        LEA01_LEASE_M.RENT_DISCOUNT_ITEMSRow drIndy;

        if (dtProd.Rows.Count > 0)
        {
            foreach (DataRow dr in dtProd.Rows)
            {
                drIndy = dtIndy.NewRENT_DISCOUNT_ITEMSRow();
                drIndy["RENT_DISCOUNT_ID"] = StringUtil.CStr(dr["RENT_DISCOUNT_ID"]);
                drIndy["LEASE_ID"] = gropStrUUID;
                drIndy["SEQNO"] = 1;

                drIndy["PRODNO"] = StringUtil.CStr(dr["PRODNO"]);
                drIndy["DISCOUNT_AMT"] = StringUtil.CStr(dr["DISCOUNT_AMT"]);
                drIndy["DISCOUNT_RATE"] = StringUtil.CStr(dr["DISCOUNT_RATE"]);
                drIndy["COST_CENTER_NO"] = StringUtil.CStr(dr["COST_CENTER_NO"]);
                drIndy["ACCOUNT_CODE"] = StringUtil.CStr(dr["ACCOUNT_CODE"]);
                drIndy["MODI_USER"] = logMsg.MODI_USER;
                drIndy["MODI_DTM"] = Convert.ToDateTime(System.DateTime.Now);
                drIndy["CREATE_USER"] = drIndy["MODI_USER"];
                drIndy["CREATE_DTM"] = drIndy["MODI_DTM"];
                dtIndy.Rows.Add(drIndy);
            }
        }
        return dtIndy;
    }
    #endregion

    
}
