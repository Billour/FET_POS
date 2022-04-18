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

public partial class VSS_CONS_CON02_Import : Popup
{
    private int errorCount = 0; //錯誤筆數

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            RadioType_CheckedChanged(sender, e);
            bindMasterData(null);
        }
        
    }

    private string  SeetName( int number) {
        string strOut = "";

        if( number == 1)
            strOut = "廠商資料";
        else if( number == 2)
            strOut = "寄銷廠商佣金";
        else if( number == 3)
            strOut = "合作店組資料";
        else if( number == 4)
            strOut = "寄銷商品資料";
        else if( number == 5)
            strOut = "總額抽成";
        else if( number == 6)
            strOut = "金額級距";
        else if( number == 7)
            strOut = "外部廠商商品資料";
        else if( number == 8)
            strOut = "信用卡手續費";
         
            return strOut;
    }

    #region //check 匯入資料的分頁
    //廠商資料
    protected void bindMasterData(ASPxGridView gv)
    {
        if (gv == null) return;
        DataTable dtResult = new DataTable();
        dtResult = Session[StringUtil.CStr(gv.ID)] as DataTable ;
        gv.DataSource = dtResult;
        gv.DataBind();
    }
    #endregion

    protected void ASPxPageControl1_ActiveTabChanged(object source, DevExpress.Web.ASPxTabControl.TabControlEventArgs e)
    {
        int a = this.ASPxPageControl1.ActiveTabIndex;
        
        switch (a)
        {
            case 0:
                bindMasterData(gvCSM_SUPPLIER);
                break;

            case 1:
                bindMasterData(gvCSM_SUPP_COMMISSION);
                break;

            case 2:
                bindMasterData(gvCSM_SUPPSTORE);
                break;

            case 3:
                bindMasterData(null);
                break;

            case 4:
                bindMasterData(gvCSM_SUPP_COMMISSION2);
                break;

            case 5:
                bindMasterData(gvCSM_SUP_AMT_LEVEL);
                break;

            case 6:
                bindMasterData(gvCSM_SUP_PROD);
                break;

            default:
                bindMasterData(gvCSM_CREDIT_CARD_PROCE_RATE);
                break;
        }
    }

    protected void RadioType_CheckedChanged(object sender, EventArgs e)
    {
        for (int a = 1; a <= 8; a++)
        {
            ASPxCheckBox chk = (ASPxCheckBox)FindControl("CheckBox" + StringUtil.CStr(a));
            ASPxTextBox txb = (ASPxTextBox)FindControl("TextBox" + StringUtil.CStr(a));
            chk.Checked = false;
            txb.Text = "";
        }

        if (RadioConsignment.Checked)
        {

            for (int i = 1; i <= 3; i++)
            {
                ASPxCheckBox chk = (ASPxCheckBox)FindControl("CheckBox" + StringUtil.CStr(i));
                ASPxTextBox txb = (ASPxTextBox)FindControl("TextBox" + StringUtil.CStr(i));
                chk.Checked = true;
                chk.Enabled = true;
                txb.Enabled = true;
            }
            for (int j = 4; j <= 8; j++)
            {
                ASPxCheckBox chk = (ASPxCheckBox)FindControl("CheckBox" + StringUtil.CStr(j));
                ASPxTextBox txb = (ASPxTextBox)FindControl("TextBox" + StringUtil.CStr(j));
                chk.Enabled = false;
                chk.Checked = false;
                txb.Enabled = false;
            }
        }
        else
        {
            CheckBox1.Checked = true;
            TextBox1.Enabled = true;

            for (int i = 5; i <= 8; i++)
            {
                ASPxCheckBox chk = (ASPxCheckBox)FindControl("CheckBox" + StringUtil.CStr(i));
                ASPxTextBox txb = (ASPxTextBox)FindControl("TextBox" + StringUtil.CStr(i));
                chk.Checked = true;
                chk.Enabled = true;
                txb.Enabled = true;

            }

            for (int j = 2; j <= 4; j++)
            {
                ASPxCheckBox chk = (ASPxCheckBox)FindControl("CheckBox" + StringUtil.CStr(j));
                ASPxTextBox txb = (ASPxTextBox)FindControl("TextBox" + StringUtil.CStr(j));
                chk.Enabled = false;
                chk.Checked = false;
                txb.Enabled = false;
            }
        }

    }

    protected void btnCalcel_Click(object sender, EventArgs e)
    {

    }

    protected void btnImport_Click(object sender, EventArgs e)
    {        
        CON02_CSM_SUPPLIER.CSM_SUPPLIERDataTable dtSupplier = null;
        CON02_CSM_SUPPLIER.CSM_SUPP_COMMISSIONDataTable dtCommission, dtCommission2 = null;
        CON02_CSM_SUPPLIER.CSM_SUPPSTOREDataTable dtStore = null;
        CON02_CSM_SUPPLIER.CSM_SUP_AMT_LEVELDataTable dtAMT = null;
        CON02_CSM_SUPPLIER.CSM_CREDIT_CARD_PROCE_RATEDataTable dtCard = null;        
        
        CON02_Facade CON02_F = new CON02_Facade();
        if (RadioConsignment.Checked)
        {
            try
            {
                dtSupplier = doSUPPLIER((DataTable)Session["gvCSM_SUPPLIER"]);
                dtCommission = doInsertCommission((DataTable)Session["gvCSM_SUPP_COMMISSION"]);
                dtStore = doInsertSuppstore((DataTable)Session["gvCSM_SUPPSTORE"]);
                CON02_F.SaveOut(dtSupplier, dtCommission, dtStore);
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this, GetType(), "AlertMessage", "alert('匯入資料失敗!! "
                    + ex.ToString() + "');", true);
            }
                 
        }
        else
        {//SaveOutFactory
            try{
            dtSupplier = doSUPPLIER((DataTable)Session["gvCSM_SUPPLIER"]);
            dtCommission2 = doInsertCommission((DataTable)Session["gvCSM_SUPP_COMMISSION2"]);
            dtAMT = doInsertSupAmtLevel((DataTable)Session["gvCSM_SUP_AMT_LEVEL"]);

            dtCard = doInsertCard((DataTable)Session["gvCSM_CREDIT_CARD_PROCE_RATE"]);
            CON02_F.SaveOutFactory(dtSupplier, dtCommission2, dtStore, dtAMT, null, dtCard);
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this, GetType(), "AlertMessage", "alert('匯入資料失敗!! "
                    + ex.ToString() + "');", true);
            }
        }
        SetReturnValue(string.Empty);
        //ScriptManager.RegisterClientScriptBlock("Import", "<script>alert('匯入完成，請重新查詢資料!');</script>");
    }

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
            ScriptManager.RegisterClientScriptBlock(this, typeof(string), "EXCEL匯入", "alert('請勾選廠商資料!');", true);
            return;
        }

        for (int i = 1; i <= 8; i++)
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
        if (RadioConsignment.Checked)
        {
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
                    sheet = workbook.GetSheetAt(int.Parse(txb.Text) - 1) as HSSFSheet;
                    SaveSheetData(sheet, i);
                }
            }
        }
        else
        {

            if (CheckBox1.Checked)
            {
                if (string.IsNullOrEmpty(TextBox1.Text))
                {
                    TextBox1.Focus();
                    ScriptManager.RegisterClientScriptBlock(this, typeof(string), "EXCEL匯入", "alert('" + SeetName(1) +
                        "工作表請輸入數字!');", true);
                    return;
                }

                sheet = workbook.GetSheetAt(int.Parse(TextBox1.Text) - 1) as HSSFSheet;
                SaveSheetData(sheet, 1);
            }
            for (int i = 5; i <= 8; i++)
            {
                ASPxCheckBox chk = (ASPxCheckBox)FindControl("CheckBox" + StringUtil.CStr(i));
                ASPxTextBox txb = (ASPxTextBox)FindControl("TextBox" + StringUtil.CStr(i));
                if (chk.Checked)
                {
                    if (string.IsNullOrEmpty(txb.Text))
                    {
                        txb.Focus();
                        ScriptManager.RegisterClientScriptBlock(this, typeof(string), "EXCEL匯入", "alert('" + SeetName(i)
                            + "工作表請輸入數字!');", true);
                        return;
                    }

                    sheet = workbook.GetSheetAt(int.Parse(txb.Text) - 1) as HSSFSheet;
                    SaveSheetData(sheet, i);
                }

            }
        }
        workbook = null;
        sheet = null;

    }

    private void SaveSheetData(HSSFSheet sheet , int intSP)
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
        string strErrorTable = ""; //存錯誤的table 名稱

        try
        {
            string SP_Name = "";
            ORD09_DropShipment CON08_DTO = new ORD09_DropShipment();
            ORD09_DropShipment.UPLOAD_TEMPDataTable dt = CON08_DTO.UPLOAD_TEMP;
            ORD09_Facade facade = new ORD09_Facade();

            Session["SUDD"] = GuidNo.getUUID();
            ASPxGridView gvCSM = new ASPxGridView(); //存要顯示的GridView

            foreach (DataRow Importdr in dtExcel.Rows)
            {
                ORD09_DropShipment.UPLOAD_TEMPRow dr = dt.NewUPLOAD_TEMPRow();

                switch (j)
                {
                    #region
                    case 1:
                        dr.BATCH_NO = StringUtil.CStr(Session["SUDD"]);
                        //存舊的UUID 方便在DB 內查詢
                        Session["C_BATCH_NO"] = StringUtil.CStr(Session["SUDD"]);

                        dr.USER_ID = logMsg.CREATE_USER;
                        dr.FINC_ID = "CON02_IMPORT";
                        dr.STATUS = "";
                        dr[6] = GuidNo.getUUID();
                        dr[7] = StringUtil.CStr(Importdr["廠商類別"]);
                        dr[8] = StringUtil.CStr(Importdr["廠商代號"]);
                        dr[9] = Importdr["廠商名稱"].ToString();
                        dr[10] = Importdr["公司地址"].ToString();
                        dr[11] = Importdr["聯絡人"].ToString();
                        dr[12] = Importdr["聯絡電話"].ToString();
                        dr[13] = Importdr["合作起日"].ToString();
                        dr[14] = Importdr["合作訖日"].ToString();
                        dr[15] = Importdr["合約號碼"].ToString();
                        dr[16] = Importdr["結算日"].ToString();
                        dr[17] = Importdr["統一編號"].ToString();
                        dr[18] = Importdr["負責人"].ToString();
                        dr[19] = Importdr["電話號碼"].ToString();
                        dr[20] = Importdr["傳真"].ToString();
                        dr[21] = Importdr["電子信箱"].ToString();
                        dr[22] = Importdr["總金額底限"].ToString();
                        dr[23] = Importdr["總金額底限勾選"].ToString();
                        dr[24] = Importdr["備註"].ToString();
                        dr[25] = Importdr["會計科目1"].ToString();
                        dr[26] = Importdr["會計科目2"].ToString();
                        dr[27] = Importdr["會計科目3"].ToString();
                        dr[28] = Importdr["會計科目4"].ToString();
                        dr[29] = Importdr["會計科目5"].ToString();
                        dr[30] = Importdr["會計科目6"].ToString();
                        dr[31] = Importdr["遠傳聯絡窗口"].ToString();
                        SP_Name = "SP_CHECK_CSM_SUPPLIER";
                        gvCSM = gvCSM_SUPPLIER;
                        dt.Rows.Add(dr);
                        CON08_DTO.AcceptChanges();
                        strErrorTable = "廠商資料";
                        break;

                    case 2:
                        dr.BATCH_NO = StringUtil.CStr(Session["SUDD"]);
                        dr.USER_ID = logMsg.CREATE_USER;
                        dr.FINC_ID = "CON02_IMPORT";
                        dr.STATUS = "";
                        dr[6] = GuidNo.getUUID();

                        dr[7] = Importdr["廠商代號"].ToString().Trim().ToUpper();
                        dr[8] = Importdr["佣金比率"].ToString();
                        dr[9] = Importdr["起始月份"].ToString();
                        dr[10] = Importdr["結束月份"].ToString();
                        SP_Name = "SP_CHECK_CSM_SUPP_COMMISSION";
                        gvCSM = gvCSM_SUPP_COMMISSION;
                        dt.Rows.Add(dr);
                        CON08_DTO.AcceptChanges();

                        strErrorTable = "廠商資料";
                        break;

                    case 3:
                        dr.BATCH_NO = StringUtil.CStr(Session["SUDD"]);
                        dr.USER_ID = logMsg.CREATE_USER;
                        dr.FINC_ID = "CON02_IMPORT";
                        dr.STATUS = "";
                        dr[6] = GuidNo.getUUID();
                        SP_Name = "SP_CSM_SUPPSTORE";
                        gvCSM = gvCSM_SUPPSTORE;

                        dr[7] = Importdr["廠商代號"].ToString().Trim().ToUpper();
                        dr[8] = Importdr["門市代號"].ToString().Trim().ToUpper();

                        strErrorTable = "合作店組資料";
                        dt.Rows.Add(dr);
                        CON08_DTO.AcceptChanges();
                        break;


                    case 4:
                    //dr.BATCH_NO = StringUtil.CStr(Session["SUDD"]);
                    //dr.USER_ID = logMsg.CREATE_USER;
                    //dr.FINC_ID = "CHECK_CSM_SUPPLIER";
                    //dr.STATUS = "";
                    //dr[6] = GuidNo.getUUID();
                    //gvCSM = 
                    //dr[7] = Importdr["廠商代號"].ToString().Trim().ToUpper();
                    //dr[8] = Importdr["門市代號"].ToString();
                    //dt.Rows.Add(dr);
                    //CON08_DTO.AcceptChanges();
                    //strErrorTable = "寄銷商品資料";
                    //break;

                    case 5:
                        dr.BATCH_NO = StringUtil.CStr(Session["SUDD"]);
                        dr.USER_ID = logMsg.CREATE_USER;
                        dr.FINC_ID = "CON02_IMPORT";
                        dr.STATUS = "";
                        dr[6] = GuidNo.getUUID();
                        SP_Name = "SP_CHECK_CSM_SUPP_COMMISSION";
                        gvCSM = gvCSM_SUPP_COMMISSION2;

                        dr[7] = Importdr["廠商代號"].ToString().Trim().ToUpper();
                        dr[8] = Importdr["佣金比率"].ToString();
                        dr[9] = Importdr["起始日"].ToString();
                        dr[10] = Importdr["結束日"].ToString();
                        dt.Rows.Add(dr);
                        CON08_DTO.AcceptChanges();
                        strErrorTable = "總額抽成";
                        break;
                    case 6:
                        dr.BATCH_NO = StringUtil.CStr(Session["SUDD"]);
                        dr.USER_ID = logMsg.CREATE_USER;
                        dr.FINC_ID = "CON02_IMPORT";
                        dr.STATUS = "";
                        dr[6] = GuidNo.getUUID();
                        SP_Name = "SP_CSM_SUP_AMT_LEVEL";
                        gvCSM = gvCSM_SUP_AMT_LEVEL;

                        dr[7] = Importdr["廠商代號"].ToString().Trim().ToUpper();
                        dr[8] = Importdr["級距項次"].ToString();
                        dr[9] = Importdr["起-金額級距"].ToString();
                        dr[10] = Importdr["訖-金額級距"].ToString();
                        dr[11] = Importdr["佣金比率"].ToString();
                        dr[12] = Importdr["開始日期"].ToString();
                        dr[13] = Importdr["結束日期"].ToString();
                        dt.Rows.Add(dr); 
                        CON08_DTO.AcceptChanges();
                        strErrorTable = "金額級距";
                        break;

                    case 7:
                        dr.BATCH_NO = StringUtil.CStr(Session["SUDD"]);
                        dr.USER_ID = logMsg.CREATE_USER;
                        dr.FINC_ID = "CON02_IMPORT";
                        dr.STATUS = "";
                        dr[6] = GuidNo.getUUID();
                        SP_Name = "SP_CSM_SUP_PROD";
                        gvCSM = gvCSM_SUP_PROD;

                        dr[7] = Importdr["廠商代號"].ToString().Trim().ToUpper();
                        dr[8] = Importdr["商品料號"].ToString();

                        strErrorTable = "外部廠商商品資料";
                        dt.Rows.Add(dr);
                        CON08_DTO.AcceptChanges();
                        break;
                    default:
                        dr.BATCH_NO = StringUtil.CStr(Session["SUDD"]);
                        dr.USER_ID = logMsg.CREATE_USER;
                        dr.FINC_ID = "CON02_IMPORT";
                        dr.STATUS = "";
                        dr[6] = GuidNo.getUUID();
                        SP_Name = "SP_CSM_CREDIT_CARD_PROCE_RATE";
                        gvCSM = gvCSM_CREDIT_CARD_PROCE_RATE;


                        dr[7] = Importdr["項次"].ToString();
                        dr[8] = Importdr["信用卡別"].ToString();
                        dr[9] = Importdr["手續費"].ToString();

                        strErrorTable = "信用卡手續費";
                        dt.Rows.Add(dr);
                        CON08_DTO.AcceptChanges();
                        break;

                    #endregion
                }
                
            }

            //更新資料庫
            facade.AddNew_UPLoad(CON08_DTO);
            
            checkdata(dt, StringUtil.CStr(Session["SUDD"]), gvCSM , SP_Name , j);
        }
        catch (Exception)
        {
            btnImport.Visible = false;
            ScriptManager.RegisterClientScriptBlock(this, typeof(string), "cmd", "alert(" + strErrorTable + "'格式錯誤，請檢查!!');", true);
            
        }
    }

    //check data
    protected void checkdata(DataTable dt, string SID, ASPxGridView gv, string SP_Name, int intLabel)
    {
        //btnCommitUpload.Enabled = true;
        CON02_Facade facade09 = new CON02_Facade();
        string C_BATCH_ID = "";
        if(SP_Name != "SP_CHECK_CSM_SUPPLIER")
            C_BATCH_ID = StringUtil.CStr(Session["C_BATCH_NO"]);
        DataTable table = CON02_Facade.SP_CHECK_CONSIGNMENT_GOODS_PS(SP_Name, SID, C_BATCH_ID, logMsg.OPERATOR, "CON02_IMPORT");

        Session[gv.ID.ToString()] = table;
        gv.DataSource = table;
        gv.DataBind();

        //設定 資料筆數:  錯誤筆數:
        SetAboutCout( table ,  gv ,  intLabel);

        if (errorCount == 0)
            btnImport.Visible = true;
    }
    
    //設定 資料筆數:  錯誤筆數:
    private int SetAboutCout(DataTable table , ASPxGridView gv , int intLabel)
    {
        int index = (intLabel * 2 - 1);

        ((ASPxLabel)gv.FindTitleTemplateControl("ASPxLabel" + StringUtil.CStr(index))).Text = "資料筆數：" +
            StringUtil.CStr(table.Rows.Count) + " 筆";

        DataRow[] drs = table.Select(" ERR_DESC <>''");
        if (drs.Length > 0)//失敗筆數 第一個分頁的Grid
        {
            ((ASPxLabel)gv.FindTitleTemplateControl("ASPxLabel" + StringUtil.CStr(index + 1) ) ).Text = "錯誤筆數：" +
                StringUtil.CStr(table.Rows.Count) + " 筆";
        }

        if (drs.Length == 0)
            errorCount += drs.Length;
            
        return errorCount;
    }

    #region do Insert

    //外部廠商主檔
    private CON02_CSM_SUPPLIER.CSM_SUPPLIERDataTable doSUPPLIER(DataTable dtTable)
    {
        //1寄銷廠商 2外部廠商
        CON02_CSM_SUPPLIER DtoSupplier = new CON02_CSM_SUPPLIER();
        CON02_CSM_SUPPLIER.CSM_SUPPLIERDataTable dtSupplier = null;
        CON02_CSM_SUPPLIER.CSM_SUPPLIERRow drSupplier;

        dtSupplier = new CON02_CSM_SUPPLIER.CSM_SUPPLIERDataTable();
        drSupplier = dtSupplier.NewCSM_SUPPLIERRow();
        if (dtTable.Rows.Count > 0)
        {
            foreach (DataRow dr in dtTable.Rows)
            {
                string UUID = GuidNo.getUUID();
                drSupplier["SUPP_ID"] = UUID;
                Session["SUPPLIER_SUPP_ID"] = UUID;//存共用insert 的UUID

                if (dr["SUPP_NO"] != null)
                    drSupplier["SUPP_NO"] = dr["SUPP_NO"];

                if (dr["CSM_TYPE"] != null)
                    drSupplier["CSM_TYPE"] = dr["CSM_TYPE"];

                if (dr["FET_CONTACE_USER"] != null)
                    drSupplier["FET_CONTACE_USER"] = dr["FET_CONTACE_USER"];

                if (dr["BOSS_NAME"] != null)
                    drSupplier["BOSS_NAME"] = dr["BOSS_NAME"];

                if (dr["SUPP_NAME"] != null)
                    drSupplier["SUPP_NAME"] = dr["SUPP_NAME"];

                if (dr["SUPP_ADDRESS"] != null)
                    drSupplier["SUPP_ADDRESS"] = dr["SUPP_ADDRESS"];

                if (dr["CONTACE"] != null)
                    drSupplier["CONTACE"] = dr["CONTACE"];

                if (dr["TELNO"] != null)
                    drSupplier["TELNO"] = dr["TELNO"];

                if (dr["S_DATE"] != null)
                    drSupplier["S_DATE"] = DateTime.Parse(StringUtil.CStr(dr["S_DATE"])).ToString("yyyy/MM/dd");

                if (dr["E_DATE"] != null)
                    drSupplier["E_DATE"] = DateTime.Parse(StringUtil.CStr(dr["E_DATE"])).ToString("yyyy/MM/dd");

                if (dr["CONTRACTNO"] != null)
                    drSupplier["CONTRACTNO"] = dr["CONTRACTNO"];
                
                drSupplier["CLOSEDAY"] = 30;
                if (dr["COMPANY_ID"] != null)
                    drSupplier["COMPANY_ID"] = dr["COMPANY_ID"];

                if (dr["BOSS_NAME"] != null)
                    drSupplier["BOSS_NAME"] = dr["BOSS_NAME"];

                if (dr["BOSS_TEL_NO"] != null)
                    drSupplier["BOSS_TEL_NO"] = dr["BOSS_TEL_NO"];

                if (dr["FAX"] != null)
                    drSupplier["FAX"] = dr["FAX"];

                if (dr["EMAIL"] != null)
                    drSupplier["EMAIL"] = dr["EMAIL"];

                if (dr["AMOUNT_MAX"] != null)
                    drSupplier["AMOUNT_MAX"] = dr["AMOUNT_MAX"];

                if (dr["ACCOUNTCODE"] != null)
                    drSupplier["ACCOUNTCODE"] = dr["ACCOUNTCODE"];

                if (dr["MEMO"] != null)
                    drSupplier["MEMO"] = dr["MEMO"];

                drSupplier["STATUS"] = "1";

                drSupplier["MODI_USER"] = logMsg.MODI_USER;
                drSupplier["MODI_DTM"] = Convert.ToDateTime(System.DateTime.Now);
                drSupplier["CREATE_USER"] = logMsg.MODI_USER;
                drSupplier["CREATE_DTM"] = Convert.ToDateTime(System.DateTime.Now);
                dtSupplier.Rows.Add(drSupplier);
            } 
        }

        return dtSupplier;
    }

    //佣金比率設定檔
    private CON02_CSM_SUPPLIER.CSM_SUPP_COMMISSIONDataTable doInsertCommission(DataTable dtTable)
    {
        CON02_CSM_SUPPLIER.CSM_SUPP_COMMISSIONDataTable dtCommission = null;
        dtCommission = new CON02_CSM_SUPPLIER.CSM_SUPP_COMMISSIONDataTable();
        CON02_CSM_SUPPLIER.CSM_SUPP_COMMISSIONRow drCommission;
        
        if (dtTable.Rows.Count > 0)
        {
            foreach (DataRow dr in dtTable.Rows)
            {
                drCommission = dtCommission.NewCSM_SUPP_COMMISSIONRow();
                drCommission["CSC_ID"] = GuidNo.getUUID();//dr["CSC_ID"];
                drCommission["SUPP_ID"] = Session["SUPPLIER_SUPP_ID"];

                if (dr["SEQNO"] != null)
                    drCommission["SEQNO"] = dr["SEQNO"];

                if (dr["COMMISSION"] != null)
                    drCommission["COMMISSION"] = dr["COMMISSION"].ToString();

                if (dr["S_DATE"] != null)
                    drCommission["S_DATE"] = DateTime.Parse(StringUtil.CStr(dr["S_DATE"])).ToString("yyyy/MM");

                if (dr["E_DATE"] != null)
                    drCommission["E_DATE"] = DateTime.Parse(StringUtil.CStr(dr["E_DATE"])).ToString("yyyy/MM");

                drCommission["MODI_USER"] = logMsg.MODI_USER;
                drCommission["MODI_DTM"] = Convert.ToDateTime(System.DateTime.Now);
                drCommission["CREATE_USER"] = drCommission["MODI_USER"];
                drCommission["CREATE_DTM"] = drCommission["MODI_DTM"];
                dtCommission.Rows.Add(drCommission);
            }
        }
        return dtCommission;
    }

    //廠商指定店組
    private CON02_CSM_SUPPLIER.CSM_SUPPSTOREDataTable doInsertSuppstore(DataTable dtTable)
    {
        CON02_CSM_SUPPLIER.CSM_SUPPSTOREDataTable dtSuppstore = null;

        CON02_CSM_SUPPLIER.CSM_SUPPSTORERow drSuppstore;

        dtSuppstore = new CON02_CSM_SUPPLIER.CSM_SUPPSTOREDataTable();
        if (dtTable.Rows.Count > 0)
        {
            foreach (DataRow dr in dtTable.Rows)
            {
                drSuppstore = dtSuppstore.NewCSM_SUPPSTORERow();
                drSuppstore["CS_STORE_ID"] = GuidNo.getUUID();//dr["CS_STORE_ID"];

                if(dr["STORE_NO"] != null)
                    drSuppstore["STORE_NO"] = dr["STORE_NO"];

                drSuppstore["SUPP_ID"] = Session["SUPPLIER_SUPP_ID"];

                drSuppstore["MODI_USER"] = logMsg.MODI_USER;
                drSuppstore["MODI_DTM"] = Convert.ToDateTime(System.DateTime.Now);
                drSuppstore["CREATE_USER"] = drSuppstore["MODI_USER"];
                drSuppstore["CREATE_DTM"] = drSuppstore["MODI_DTM"];
                dtSuppstore.Rows.Add(drSuppstore);
            }
        }
        return dtSuppstore;

    }
    //金額級距
    private CON02_CSM_SUPPLIER.CSM_SUP_AMT_LEVELDataTable doInsertSupAmtLevel(DataTable dtTable)
    {
        CON02_CSM_SUPPLIER.CSM_SUP_AMT_LEVELDataTable dtSupAmtLevel = null;

        dtSupAmtLevel = new CON02_CSM_SUPPLIER.CSM_SUP_AMT_LEVELDataTable();
        CON02_CSM_SUPPLIER.CSM_SUP_AMT_LEVELRow drSupAmtLevel;

        if (dtTable.Rows.Count > 0)
        {
            foreach (DataRow dr in dtTable.Rows)
            {
                drSupAmtLevel = dtSupAmtLevel.NewCSM_SUP_AMT_LEVELRow();
                drSupAmtLevel["CSAL_ID"] = GuidNo.getUUID();//dr["CSAL_ID"];
                drSupAmtLevel["SUPP_ID"] = Session["SUPPLIER_SUPP_ID"];

                if (dr["SEQNO"] != null)
                    drSupAmtLevel["SEQNO"] = dr["SEQNO"];

                if (dr["S_AMT"] != null)
                    drSupAmtLevel["S_AMT"] = dr["S_AMT"];

                if (dr["E_AMT"] != null)
                    drSupAmtLevel["E_AMT"] = dr["E_AMT"];

                if (dr["COMMISION_RATE"] != null)
                    drSupAmtLevel["COMMISION_RATE"] = dr["COMMISION_RATE"].ToString();

                if (dr["S_DATE"] != null)
                    drSupAmtLevel["S_DATE"] = Convert.ToDateTime(dr["S_DATE"].ToString());

                if (dr["E_DATE"] != null)
                    drSupAmtLevel["E_DATE"] = Convert.ToDateTime(dr["E_DATE"].ToString());

                drSupAmtLevel["MODI_USER"] = logMsg.MODI_USER;
                drSupAmtLevel["MODI_DTM"] = Convert.ToDateTime(System.DateTime.Now);
                drSupAmtLevel["CREATE_USER"] = drSupAmtLevel["MODI_USER"];
                drSupAmtLevel["CREATE_DTM"] = drSupAmtLevel["MODI_DTM"];
                dtSupAmtLevel.Rows.Add(drSupAmtLevel);
            }
        }
        return dtSupAmtLevel;
    }
    //外部廠商商品編號設定
    private CON02_CSM_SUPPLIER.CSM_SUP_PRODDataTable doInsertProd(DataTable dtTable)
    {
        CON02_CSM_SUPPLIER.CSM_SUP_PRODDataTable dtSupProd = null;

        dtSupProd = new CON02_CSM_SUPPLIER.CSM_SUP_PRODDataTable();
        CON02_CSM_SUPPLIER.CSM_SUP_PRODRow drSupProd;
        //DateTime sDate ;
        //DateTime eDate ;

        if (dtTable.Rows.Count > 0)
        {
            foreach (DataRow dr in dtTable.Rows)
            {
                drSupProd = dtSupProd.NewCSM_SUP_PRODRow();

                drSupProd["CSP_ID"] = GuidNo.getUUID();//dr["CSP_ID"];
                drSupProd["SUPP_ID"] = Session["SUPPLIER_SUPP_ID"];

                if (dr["SEQNO"] != null)
                    drSupProd["SEQNO"] = dr["SEQNO"];

                if (dr["ACCOUNT_CODE"] != null)
                    drSupProd["ACCOUNT_CODE"] = dr["ACCOUNT_CODE"].ToString();

                if (dr["S_YYMM"] != null)
                    drSupProd["S_YYMM"] = DateTime.Parse(dr["S_YYMM"].ToString() + "/01").ToString("yyyyMM");

                if (dr["E_YYMM"] != null)
                    drSupProd["E_YYMM"] = DateTime.Parse(dr["E_YYMM"].ToString() + "/01").ToString("yyyyMM");

                drSupProd["MODI_USER"] = logMsg.MODI_USER;
                drSupProd["MODI_DTM"] = Convert.ToDateTime(System.DateTime.Now);
                drSupProd["CREATE_USER"] = drSupProd["MODI_USER"];
                drSupProd["CREATE_DTM"] = drSupProd["MODI_DTM"];
                dtSupProd.Rows.Add(drSupProd);
            }
        }
        return dtSupProd;
    }
    //信用卡手續費率
    private CON02_CSM_SUPPLIER.CSM_CREDIT_CARD_PROCE_RATEDataTable doInsertCard(DataTable dtTable)
    {
        CON02_CSM_SUPPLIER.CSM_CREDIT_CARD_PROCE_RATEDataTable dtSubCard = null;

        dtSubCard = new CON02_CSM_SUPPLIER.CSM_CREDIT_CARD_PROCE_RATEDataTable();
        CON02_CSM_SUPPLIER.CSM_CREDIT_CARD_PROCE_RATERow drCard;

        if (dtTable.Rows.Count > 0)
        {
            foreach (DataRow dr in dtTable.Rows)
            {
                drCard = dtSubCard.NewCSM_CREDIT_CARD_PROCE_RATERow();

                drCard["CCPR_ID"] = GuidNo.getUUID();//dr["CCPR_ID"];
                drCard["SUPP_ID"] = Session["SUPPLIER_SUPP_ID"];

                if (dr["CREDIT_CARD_TYPE_ID"] != null)
                    drCard["CREDIT_CARD_TYPE_ID"] = dr["CREDIT_CARD_TYPE_ID"];

                if (dr["CHARGE_RATE"] != null)
                    drCard["CHARGE_RATE"] = dr["CHARGE_RATE"];
                //drCard["COMMISION_RATE"] = dr["COMMISION_RATE"].ToString();
                drCard["S_DATE"] = "2011/01/01";
                //drCard["E_DATE"] = dr["E_DATE"].ToString();

                drCard["MODI_USER"] = logMsg.MODI_USER;
                drCard["MODI_DTM"] = Convert.ToDateTime(System.DateTime.Now);
                drCard["CREATE_USER"] = drCard["MODI_USER"];
                drCard["CREATE_DTM"] = drCard["MODI_DTM"];
                dtSubCard.Rows.Add(drCard);
            }
        }
        return dtSubCard;
    }
    #endregion

    //private Dictionary<int, string> getSheet(string strType)
    //{
    //    Dictionary<int, string> dtary = new Dictionary<int, string>();
    //    try
    //    {
    //        if (strType == "1") //寄銷廠商
    //        {
    //            if (CheckBox1.Checked == true)
    //                dtary.Add(int.Parse(TextBox1.Text), "SUPPLIER");
    //            if (CheckBox2.Checked == true)
    //                dtary.Add(int.Parse(TextBox2.Text), "COMMISSION");
    //            if (CheckBox3.Checked == true)
    //                dtary.Add(int.Parse(TextBox3.Text), "STORE");
    //        }
    //        else
    //        {
    //            if (CheckBox1.Checked == true)
    //                dtary.Add(int.Parse(TextBox1.Text), "SUPPLIER");
    //            if (CheckBox5.Checked == true)
    //                dtary.Add(int.Parse(TextBox5.Text), "COMMISSION");
    //            if (CheckBox6.Checked == true)
    //                dtary.Add(int.Parse(TextBox6.Text), "AMT");
    //            if (CheckBox7.Checked == true)
    //                dtary.Add(int.Parse(TextBox7.Text), "SUPPROD");
    //            if (CheckBox8.Checked == true)
    //                dtary.Add(int.Parse(TextBox8.Text), "CARD");
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        dtary.Add(-1, "error");
    //    }
    //    return dtary;
    //}

    //class LabelCount
    //{
    //    public string GridViewName { get; set; }

    //    public string AllCount { get; set; }

    //    public string ErrorCount { get; set; }


    //}
}
