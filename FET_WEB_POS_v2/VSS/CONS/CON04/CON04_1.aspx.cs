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

public partial class VSS_CONS_CON04_1 : Popup
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            
            bindMaster();
            bindDetailData();
        }
    }
    #region 初始BIND 與GET GRIDVIEW
    protected void bindMaster()
    {
        DataTable dtResult = new DataTable();
        dtResult = getMaster();
        Session["gvMaster"] = dtResult;
        gvMaster.DataSource = dtResult;
        gvMaster.DataBind();
    }
    private DataTable getMaster()
    {
        DataTable dtResult = new DataTable();
        dtResult.Columns.Add("SUPPNO", typeof(string));
        dtResult.Columns.Add("PRODNO", typeof(string));
        dtResult.Columns.Add("PRODTYPENO", typeof(string));
        dtResult.Columns.Add("PRODNAME", typeof(string));
        dtResult.Columns.Add("S_DATE", typeof(string));
        dtResult.Columns.Add("E_DATE", typeof(string));
        dtResult.Columns.Add("CEASEDATE", typeof(string));
        dtResult.Columns.Add("ACCT1", typeof(string));
        dtResult.Columns.Add("ACCT2", typeof(string));
        dtResult.Columns.Add("ACCT3", typeof(string));
        dtResult.Columns.Add("ACCT4", typeof(string));
        dtResult.Columns.Add("ACCT5", typeof(string));
        dtResult.Columns.Add("ACCT6", typeof(string));
        dtResult.Columns.Add("ERR_DESC", typeof(string));
        dtResult.Columns.Add("SUPP_ID", typeof(string));


        return dtResult;
    }
    protected void bindDetailData()
    {
        DataTable dtResult = new DataTable();
        dtResult = getDetailData();
        Session["gvDetail"] = dtResult;
        gvDetail.DataSource = dtResult;
        gvDetail.DataBind();
    }
    private DataTable getDetailData()
    {
        DataTable dtResult = new DataTable();
        dtResult.Columns.Add("SUPPNO", typeof(string));
        dtResult.Columns.Add("COMMISSION", typeof(string));
        dtResult.Columns.Add("S_DATE", typeof(string));
        dtResult.Columns.Add("E_DATE", typeof(string));
        dtResult.Columns.Add("SUPP_ID", typeof(string));
        return dtResult;
    }
    #endregion


    /// <summary>
    /// 確認按鈕
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnOK_Click(object sender, EventArgs e)
    {
        if (!this.FileUpload.HasFile) { return; }
        string FileExten = System.IO.Path.GetExtension(this.FileUpload.PostedFile.FileName);
        if (FileExten.ToLower() != ".xls")
        { return; }
        try
        {
            int success_count = 0;
            int failed_count = 0;

            HSSFWorkbook workbook = null;

            workbook = new HSSFWorkbook(this.FileUpload.FileContent);
            ViewState["BATCH_NO2"] = "";
            ViewState["BATCH_NO"] = "";

            //抓寄銷商品資料
            if (ckProductSheetNo.Checked == true)
            {
                int iSheetNo = 0;

                if (string.IsNullOrEmpty(txtProductSheetNo.Text))
                {
                    ScriptManager.RegisterClientScriptBlock(this, GetType(), "AlertMessage", "alert('寄銷商品資料工作表欄位不得為空值!!');", true);
                    return;
                }
                else
                {
                    if (!int.TryParse(txtProductSheetNo.Text, out iSheetNo))
                    {
                        ScriptManager.RegisterClientScriptBlock(this, GetType(), "AlertMessage", "alert('寄銷商品資料工作表欄位格式有誤!!!!');", true);
                        return;

                    }
                }
                DataTable dtTable = new DataTable();
                //Product_no  需改
                HSSFSheet sheet = workbook.GetSheetAt(iSheetNo - 1) as HSSFSheet;

                HSSFRow headerRow = sheet.GetRow(0) as HSSFRow;
                int cellCount = headerRow.LastCellNum;
                int rowCount = sheet.LastRowNum;
                //EXCEL表頭存入DATATABLE
                for (int i = headerRow.FirstCellNum; i < cellCount; i++)
                {
                    DataColumn column = new DataColumn(headerRow.GetCell(i).StringCellValue);
                    dtTable.Columns.Add(column);
                }

                //    //匯入暫存用Table
                ORD09_DropShipment CON04_DTO = new ORD09_DropShipment();
                ORD09_DropShipment.UPLOAD_TEMPDataTable TableUPLOAD_TEMP = CON04_DTO.UPLOAD_TEMP;
                ORD09_Facade facade = new ORD09_Facade();

                //本批次號
                ViewState["BATCH_NO"] = GuidNo.getUUID();
                string FINC_ID = "CON04_IMPORT";
                string USER_ID = logMsg.MODI_USER;

                //將exl的每個值抓出來Product_no
                for (int i = (sheet.FirstRowNum + 1); i <= sheet.LastRowNum; i++)
                {
                    HSSFRow row = sheet.GetRow(i) as HSSFRow;

                    DataRow dataRow1 = dtTable.NewRow();

                    for (int j = row.FirstCellNum; j < cellCount; j++)
                    {
                        if (row.GetCell(j) != null)
                            dataRow1[j] = row.GetCell(j).ToString();
                    }

                    //      dataRow[cellCount] = "";
                    dtTable.Rows.Add(dataRow1);

                }

                //    //表頭部份!
                foreach (DataRow row in dtTable.Rows)
                {

                    ORD09_DropShipment.UPLOAD_TEMPRow dataRow = TableUPLOAD_TEMP.NewUPLOAD_TEMPRow();
                    dataRow.F1 = row["廠商代號"].ToString();
                    dataRow.F2 = row["商品料號"].ToString();
                    dataRow.F3 = row["商品類別"].ToString();
                    dataRow.F4 = row["商品名稱"].ToString();
                    dataRow.F5 = row["單位"].ToString();
                    dataRow.F6 = row["上架日"].ToString();
                    dataRow.F7 = row["下架日"].ToString();
                    dataRow.F8 = row["停止訂購日"].ToString();
                    dataRow.F9 = row["會計科目1"].ToString();
                    dataRow.F10 = row["會計科目2"].ToString();
                    dataRow.F11 = row["會計科目3"].ToString();
                    dataRow.F12 = row["會計科目4"].ToString();
                    dataRow.F13 = row["會計科目5"].ToString();
                    dataRow.F14 = row["會計科目6"].ToString();
                    dataRow.BATCH_NO = ViewState["BATCH_NO"].ToString();
                    dataRow.FINC_ID = FINC_ID;
                    dataRow.USER_ID = USER_ID;

                    TableUPLOAD_TEMP.Rows.Add(dataRow);
                    TableUPLOAD_TEMP.AcceptChanges();

                }

                //匯入資料到UPLOAD_TEMP TABLE
                facade.AddNew_UPLoad(CON04_DTO);
                //將匯入的資料存入Temp Table中
                //需撰寫預存程式並回傳dt，最後一列為錯誤訊息
                //DataTable tempDt = CON04_Facade.SP_CHECK_CSM_PRODUCT("9AD5CEA792454F89AA8E0393E5929DC8", logMsg.OPERATOR, FINC_ID);
                DataTable tempDt = CON04_Facade.SP_CHECK_CSM_PRODUCT(ViewState["BATCH_NO"].ToString(), logMsg.OPERATOR, FINC_ID);
                Session["gvMaster"] = tempDt;
                gvMaster.DataSource = tempDt;
                gvMaster.DataBind();
                foreach (DataRow dr in tempDt.Rows)
                {
                    if (string.IsNullOrEmpty(dr["ERR_DESC"].ToString()))//判斷是否有錯誤訊息!
                    {
                        success_count++;
                    }
                    else
                    {
                        failed_count++;
                    }
                }


            }
            ASPxLabel lblOk = (ASPxLabel)gvMaster.FindTitleTemplateControl("lblOkCount");
            ASPxLabel lblError = (ASPxLabel)gvMaster.FindTitleTemplateControl("lblErrorCount");
            lblOk.Text  = "資料筆數： " + (success_count + failed_count).ToString() +" 筆";
            lblError.Text = "錯誤筆數：" + failed_count.ToString() + " 筆";
            int success_count2 = 0;
            int failed_count2 = 0;
            //----------------------------------------抓佣金比率資料------------------------

            if (ckCommissionSheetNo.Checked == true)
            {
                int iSheetNo = 0;

                if (string.IsNullOrEmpty(txtCommissionSheetNo.Text))
                {
                    ScriptManager.RegisterClientScriptBlock(this, GetType(), "AlertMessage", "alert('寄銷廠商佣金工作表欄位不得為空值!!');", true);
                    return;
                }
                else
                {
                    if (!int.TryParse(txtCommissionSheetNo.Text, out iSheetNo))
                    {
                        ScriptManager.RegisterClientScriptBlock(this, GetType(), "AlertMessage", "alert('寄銷廠商佣金工作表欄位格式有誤!!!!');", true);
                        return;

                    }
                }
                DataTable dtTable = new DataTable();
                //Product_no  需改
                HSSFSheet sheet = workbook.GetSheetAt(iSheetNo - 1) as HSSFSheet;

                HSSFRow headerRow = sheet.GetRow(0) as HSSFRow;
                int cellCount = headerRow.LastCellNum;
                int rowCount = sheet.LastRowNum;
                //EXCEL表頭存入DATATABLE
                for (int i = headerRow.FirstCellNum; i < cellCount; i++)
                {
                    DataColumn column = new DataColumn(headerRow.GetCell(i).StringCellValue);
                    dtTable.Columns.Add(column);
                }

                //    //匯入暫存用Table
                ORD09_DropShipment CON04_DTO = new ORD09_DropShipment();
                ORD09_DropShipment.UPLOAD_TEMPDataTable TableUPLOAD_TEMP = CON04_DTO.UPLOAD_TEMP;
                ORD09_Facade facade = new ORD09_Facade();

                //本批次號
                ViewState["BATCH_NO2"] = GuidNo.getUUID();
                string FINC_ID = "CON04_IMPORT2";
                string USER_ID = logMsg.MODI_USER;

                //將exl的每個值抓出來Product_no
                for (int i = (sheet.FirstRowNum + 1); i <= sheet.LastRowNum; i++)
                {
                    HSSFRow row = sheet.GetRow(i) as HSSFRow;

                    DataRow dataRow1 = dtTable.NewRow();

                    for (int j = row.FirstCellNum; j < cellCount; j++)
                    {
                        if (row.GetCell(j) != null)
                            dataRow1[j] = row.GetCell(j).ToString();
                    }

                    //      dataRow[cellCount] = "";
                    dtTable.Rows.Add(dataRow1);

                }

                //    //表頭部份!
                foreach (DataRow row in dtTable.Rows)
                {

                    ORD09_DropShipment.UPLOAD_TEMPRow dataRow = TableUPLOAD_TEMP.NewUPLOAD_TEMPRow();
                    dataRow.F1 = row["廠商代號"].ToString();
                    dataRow.F2 = row["商品料號"].ToString();
                    dataRow.F3 = row["佣金比率"].ToString();
                    dataRow.F4 = row["起始月份"].ToString();
                    dataRow.F5 = row["結束月份"].ToString();
                    dataRow.BATCH_NO = ViewState["BATCH_NO2"].ToString();
                    dataRow.FINC_ID = FINC_ID;
                    dataRow.USER_ID = USER_ID;
                    TableUPLOAD_TEMP.Rows.Add(dataRow);
                    TableUPLOAD_TEMP.AcceptChanges();
                }

                //匯入資料到UPLOAD_TEMP TABLE
                facade.AddNew_UPLoad(CON04_DTO);
                //將匯入的資料存入Temp Table中
                //需撰寫預存程式並回傳dt，最後一列為錯誤訊息
                // ERROR "82AA1CB3B05F42E6A7121114AF2D70B9" 
                // OK ""9AD5CEA792454F89AA8E0393E5929DC8""
                DataTable tempDt = CON04_Facade.SP_CHECK_CSM_PROD_COMMISSION(ViewState["BATCH_NO2"].ToString(), logMsg.OPERATOR, FINC_ID, ViewState["BATCH_NO"].ToString(), "CON04_IMPORT");
                //DataTable tempDt = CON04_Facade.SP_CHECK_CSM_PRODUCT(ViewState["BATCH_NO"].ToString(), logMsg.OPERATOR, "CON04_IMPORT");
                Session["gvDetail"] = tempDt;
                gvDetail.DataSource = tempDt;
                gvDetail.DataBind();


                foreach (DataRow dr in tempDt.Rows)
                {
                    if (string.IsNullOrEmpty(dr["ERR_DESC"].ToString()))//判斷是否有錯誤訊息!
                    {
                        success_count2++;
                    }
                    else
                    {
                        failed_count2++;
                    }
                }

                
            }

            ASPxLabel lblOk2 = (ASPxLabel)gvDetail.FindTitleTemplateControl("lblOkCount2");
            ASPxLabel lblError2 = (ASPxLabel)gvDetail.FindTitleTemplateControl("lblErrorCount2");
            lblOk2.Text = "資料筆數： " + (success_count + failed_count).ToString() + " 筆";
            lblError2.Text = "錯誤筆數：" + failed_count.ToString() + " 筆";

            if (failed_count + failed_count2> 0)
            {
                btnOK.Enabled = true;
                btnImport.Enabled = false;
                btnImport.Visible = true;
                btnCalcel.Visible = true;
            }
            else
            {
                btnOK.Enabled = true;
                btnCalcel.Enabled = true;
                btnImport.Enabled = true;
                btnImport.Visible = true;
                btnCalcel.Visible = true;
            }
        }
        catch //(Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(this, GetType(), "AlertMessage", "alert('匯入的內容格式不正確!!');", true);
        }
    }
    /// <summary>
    /// 匯入
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnImport_Click(object sender, EventArgs e)
    {

        CON04_Facade CON04_Facade = new CON04_Facade();
        CON04_CSM_PROD.PRODUCTDataTable dtProduct = doInsertProduct();
        CON04_CSM_PROD.CSM_PROD_COMMISSIONDataTable dtCommission = doInsertCsmProdCommission();
        int intResult = CON04_Facade.InsertCsmProdData(dtProduct, dtCommission);
        if (intResult > 0)
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), "Import", "<script>alert('匯入完成，請重新查詢資料!');</script>");
        }
        else
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), "Import", "<script>alert('匯入失敗，請重新確認資料!');</script>");
        }
    }
   
    #region Grid事件
    protected void gvMaster_HtmlDataCellPrepared(object sender, ASPxGridViewTableDataCellEventArgs e)
    {
        if (e.DataColumn.FieldName == "ERR_DESC")
        {
            if (!string.IsNullOrEmpty(e.CellValue.ToString()))
            {
                e.Cell.Style[HtmlTextWriterStyle.Color] = "red";
                //取得控制項裏的值出來
                //GridViewDataColumn col = new GridViewDataColumn();
                //col = (GridViewDataColumn)((ASPxGridView)sender).Columns["DIS_QTY"];
                //ASPxTextBox t = gvMaster.FindRowCellTemplateControl(e.VisibleIndex, col, "DIS_QTY") as ASPxTextBox;
                //t.ForeColor = System.Drawing.Color.Red;
            }
        }
    }

    protected void gvMaster_HtmlRowCreated(object sender, ASPxGridViewTableRowEventArgs e)
    {
        //if (e.RowType == GridViewRowType.Data)
        //{
        //    ASPxTextBox DIS_QTY = e.Row.FindChildControl<ASPxTextBox>("DIS_QTY");
        //    if (DIS_QTY != null)
        //    {
        //        DIS_QTY.Validate();
        //    }
        //}
    }

    protected void gvMaster_PageIndexChanged(object sender, EventArgs e)
    {
        gvMaster.DataSource = Session["gvMaster"];
        gvMaster.DataBind();

    }

    protected void gvDetail_HtmlDataCellPrepared(object sender, ASPxGridViewTableDataCellEventArgs e)
    {
        if (e.DataColumn.FieldName == "ERR_DESC")
        {
            if (!string.IsNullOrEmpty(e.CellValue.ToString()))
            {
                e.Cell.Style[HtmlTextWriterStyle.Color] = "red";
            }
        }
    }

    protected void gvDetail_HtmlRowCreated(object sender, ASPxGridViewTableRowEventArgs e)
    {
        //if (e.RowType == GridViewRowType.Data)
        //{
        //    ASPxTextBox DIS_QTY = e.Row.FindChildControl<ASPxTextBox>("DIS_QTY");
        //    if (DIS_QTY != null)
        //    {
        //        DIS_QTY.Validate();
        //    }
        //}
    }

    protected void gvDetail_PageIndexChanged(object sender, EventArgs e)
    {
        gvDetail.DataSource = Session["gvDetail"];
        gvDetail.DataBind();

    }
    #endregion
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

    /// <summary>
    /// 組INSERT PRODUCT DATATABLE
    /// </summary>
    /// <returns></returns>
    private CON04_CSM_PROD.PRODUCTDataTable doInsertProduct()
    {
        CON04_CSM_PROD _CON04_CSM_PROD = new CON04_CSM_PROD();
        CON04_CSM_PROD.PRODUCTDataTable dtProduct = null;
        DataTable dtGvMaster = (DataTable)Session["gvMaster"];

        dtProduct = _CON04_CSM_PROD.Tables["PRODUCT"] as CON04_CSM_PROD.PRODUCTDataTable;
        foreach (DataRow dr in dtGvMaster.Rows)
        {
            CON04_CSM_PROD.PRODUCTRow drProduct;
            drProduct = dtProduct.NewPRODUCTRow();

            //異動的欄位

            //商品料號
            drProduct["PRODNO"] = dr["PRODNO"].ToString();
            //商品類別
            drProduct["PRODTYPENO"] = dr["PRODTYPENO"].ToString();
            //商口名稱
            drProduct["PRODNAME"] = dr["PRODNAME"].ToString();
            //單位
            drProduct["UNIT"] = dr["UNIT"].ToString();
            //是否為寄銷商品
            drProduct["ISCONSIGNMENT"] = "1";
            //商品狀態
            drProduct["STATUS"] = "1";
            //會計科目
            drProduct["ACCOUNTCODE"] = dr["ACCT1"].ToString() + dr["ACCT2"].ToString() +
                                        dr["ACCT3"].ToString() + dr["ACCT4"].ToString() +
                                        dr["ACCT5"].ToString() + dr["ACCT6"].ToString();
            //是否扣庫存
            drProduct["ISSTOCK"] = "1";
            //是否POS自訂價格
            drProduct["IS_POS_DEF_PRICE"] = "Y";
            //建立人員
            drProduct["CREATE_USER"] = logMsg.MODI_USER;
            //建立時間
            drProduct["CREATE_DTM"] = Advtek.Utility.DateUtil.NullDateFormat(DateTime.Now);
            //廠商ID
            drProduct["SUPP_ID"] = dr["SUPP_ID"].ToString();
            //上架日期
            drProduct["S_DATE"] = Advtek.Utility.DateUtil.NullDateFormat(dr["S_DATE"].ToString());
            //下架日期
            string strEDate = dr["E_DATE"].ToString();
            string strCeaseDate = dr["CEASEDATE"].ToString().Replace("/", "");
            if (!string.IsNullOrEmpty(strEDate) || strEDate != " ")
            {
                drProduct["E_DATE"] = Advtek.Utility.DateUtil.NullDateFormat(strEDate); ;
            }

            if (!string.IsNullOrEmpty(strCeaseDate) || strCeaseDate != " ")
            {
                //失效日期
                drProduct["CEASEDATE"] = strCeaseDate;
            }

            //刪除註記
            drProduct["DEL_FLAG"] = "N";

            dtProduct.Rows.Add(drProduct);

        }


        return dtProduct;
    }
    /// <summary>
    /// 組INSERT CSM_PROD_COMMISSION DATATABLE
    /// </summary>
    /// <returns></returns>
    private CON04_CSM_PROD.CSM_PROD_COMMISSIONDataTable doInsertCsmProdCommission()
    {
        CON04_CSM_PROD.CSM_PROD_COMMISSIONDataTable dtCsmProdCommission = null;
        DataTable dtProd = (DataTable)Session["gvDetail"];
        dtCsmProdCommission = new CON04_CSM_PROD.CSM_PROD_COMMISSIONDataTable();
        CON04_CSM_PROD.CSM_PROD_COMMISSIONRow drCsmProdCommission;
        //Supplier_Facade Supplier_Facade = new Supplier_Facade();
        //string SuppId = Supplier_Facade.GetSuppId(txtSupplierNo.Text.ToString());
        if (dtProd.Rows.Count > 0)
        {
            int i = 1;
            foreach (DataRow dr in dtProd.Rows)
            {

                drCsmProdCommission = dtCsmProdCommission.NewCSM_PROD_COMMISSIONRow();
                drCsmProdCommission["CPC_ID"] = GuidNo.getUUID();
                drCsmProdCommission["SEQNO"] = i;
                drCsmProdCommission["PRODNO"] = dr["PRODNO"].ToString();
                drCsmProdCommission["SUPP_ID"] = dr["SUPP_ID"].ToString();
                drCsmProdCommission["COMMISSION"] = dr["COMMISSION"];
                drCsmProdCommission["S_DATE"] = dr["S_DATE"];
                drCsmProdCommission["E_DATE"] = dr["E_DATE"];
                drCsmProdCommission["CREATE_USER"] = logMsg.MODI_USER;
                drCsmProdCommission["CREATE_DTM"] = DateTime.Now;
                dtCsmProdCommission.Rows.Add(drCsmProdCommission);
                i++;
            }
        }
        Session["gvMaster"] = dtCsmProdCommission;
        return dtCsmProdCommission;
    }
}
