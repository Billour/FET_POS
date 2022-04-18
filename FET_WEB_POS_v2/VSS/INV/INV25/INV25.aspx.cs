using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;
using DevExpress.Web.ASPxGridView;
using System.Web.UI.HtmlControls;
using DevExpress.Web.ASPxEditors;
using iTextSharp.text;
using iTextSharp.text.pdf;

using Advtek.Utility;
using FET.POS.Model.Common;
using FET.POS.Model.DTO;
using FET.POS.Model.Facade;
using FET.POS.Model.Facade.FacadeImpl;
using AdvTek.CustomControls;

public partial class VSS_INV_INV25 : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        this.lblError.Text = "";

        if (!IsPostBack && !Page.IsCallback)
        {
            Session["gvMaster"] = null;
            //取得空的資料表
            BindEmptyMasterData();

            this.btnPrint.ClientEnabled = false;
            this.btnSave.ClientEnabled = false;
            this.btnDrop.ClientEnabled = false;
            this.lblStatus.Text = "00 未存檔".Substring(3);
            this.lblDateTime.Text = System.DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");
            this.lblUser.Text = logMsg.OPERATOR + " " + new Employee_Facade().GetEmpName(logMsg.OPERATOR);
        }
    }

    private void BindEmptyMasterData()
    {
        DataTable dt = new DataTable();
        if (Session["gvMaster"] == null)
        {
            dt.Columns.Add("STORETRANSFER_D_ID", typeof(string));
            dt.Columns.Add("STNO", typeof(string));
            dt.Columns.Add("SEQNO", typeof(string));
            dt.Columns.Add("PRODNO", typeof(string));
            dt.Columns.Add("PRODNAME", typeof(string));
            dt.Columns.Add("TRANOUTQTY", typeof(string));
            dt.Columns.Add("IMEI_QTY", typeof(string));
            dt.Columns.Add("IMEI_FLAG", typeof(string));
            dt.Columns.Add("IMEI", typeof(string));
            dt.PrimaryKey = new DataColumn[] { dt.Columns["STORETRANSFER_D_ID"] };
        }
        else
        {
            dt = Session["gvMaster"] as DataTable;
        }

        Session["gvMaster"] = dt;
        gvMaster.DataSource = dt;
        gvMaster.DataBind();

    }

    private void BindMasterData()
    {
        gvMaster.DataSource = Session["gvMaster"] as DataTable;
        gvMaster.DataBind();
    }

    #region Button 觸發事件

    protected void btnAddNew_Click(object sender, EventArgs e)
    {
        gvMaster.AddNewRow();

        this.btnPrint.ClientEnabled = false;
    }

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        if (Session["gvMaster"] != null)
        {
            DataTable dt = Session["gvMaster"] as DataTable;
            string where = "";
            List<object> keyValues = this.gvMaster.GetSelectedFieldValues(gvMaster.KeyFieldName);
            foreach (string skey in keyValues)
            {
                where += "'" + skey + "',";
            }
            if (where.Length > 0)
                where = where.Substring(0, where.Length - 1);
            else where = "''";

            DataRow[] dra = dt.Select("STORETRANSFER_D_ID in(" + where + ")");

            foreach (DataRow dr in dra)
            {
                dt.Rows.Remove(dr);
                dt.AcceptChanges();
            }

            BindMasterData();
            DisableButton(false);
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Session["gvMaster"] = null;
        //取得空的資料表
        BindEmptyMasterData();
        if (gvMaster.IsEditing)
        {
            gvMaster.CancelEdit();
        }
        this.txtToStoreNO.Text = "";
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (Session["gvMaster"] != null)
        {

            DataTable dtData = Session["gvMaster"] as DataTable;

            if (dtData.Rows.Count > 0)
            {
                string strError = "";
                //撥入門市要和移出門市不一樣
                string ToStoreNo = this.txtToStoreNO.Text; //調入門市

                if (ToStoreNo == logMsg.STORENO)
                {
                    strError = "撥入門市要和移出門市不一樣!!\n";
                    //Page.ClientScript.RegisterStartupScript(this.GetType(), "ToStoreNoMessage", "alert('撥入門市要和移出門市不一樣!!');");
                    this.lblError.Text = strError;
                    return;
                }


                INV25_StoreTransfer_DTO INV25_DTO = new INV25_StoreTransfer_DTO();
                INV25_StoreTransfer_DTO.STORETRANSFER_MDataTable dtM = INV25_DTO.STORETRANSFER_M;
                INV25_StoreTransfer_DTO.STORETRANSFER_MRow drM = dtM.NewSTORETRANSFER_MRow();

                INV25_StoreTransfer_DTO.STORETRANSFER_DDataTable dtD = INV25_DTO.STORETRANSFER_D;

                //string strSTNO = this.hdSTNO.Value;
                string strSTNO = SerialNo.GenNo("ST{0}-"); //"ST{0}-100815001";
                strSTNO = string.Format(strSTNO, logMsg.STORENO); //{0} 帶入登入者的門市編號

                drM.STNO = strSTNO; //調撥單號
                //    drM.STDATE = OracleDBUtil.WorkDay(logMsg.STORENO); //移出日期
                drM.STDATE = System.DateTime.Now.ToString("yyyy/MM/dd");//移出日期
                drM.TSTATUS = "20";//調撥單狀態：  00 未存檔,10 暫存(預留), 20 在途, 30 已撥入
                drM.FROM_STORE_NO = logMsg.STORENO;//移出門市
                //   drM.UPDDATE = OracleDBUtil.WorkDay(logMsg.STORENO); //調出端異動時間
                drM.UPDDATE = System.DateTime.Now.ToString("yyyy/MM/dd");//調出端異動時間
                drM.STUSRNO = logMsg.CREATE_USER; //調出端-調出人員
                drM.TO_STORE_NO = this.txtToStoreNO.Text; //調入門市
                drM.CREATE_USER = logMsg.CREATE_USER; //建立人員
                drM.CREATE_DTM = System.DateTime.Now;//建立時間
                drM.MODI_USER = logMsg.MODI_USER;//異動人員
                drM.MODI_DTM = System.DateTime.Now;//異動時間

                dtM.Rows.Add(drM);
                INV25_DTO.AcceptChanges();

                foreach (DataRow dr in dtData.Rows)
                {
                    string strProdNo = StringUtil.CStr(dr["PRODNO"]);

                    long HandQty = INV25_PageHelper.GetInvOnHandCurrent(strProdNo, logMsg.STORENO);  //庫存量
                    long TranOutQty = Convert.ToInt64(StringUtil.CStr(dr["TRANOUTQTY"]));  //移出量
                    if (TranOutQty > HandQty)
                    {
                        strError = "資料列中移出數量多於現在庫存數!!";
                        this.lblError.Text = strError;
                        return;
                    }


                    INV25_StoreTransfer_DTO.STORETRANSFER_DRow drD = dtD.NewSTORETRANSFER_DRow();

                    drD.STORETRANSFER_D_ID = StringUtil.CStr(dr["STORETRANSFER_D_ID"]);
                    drD.STNO = strSTNO;
                    drD.SEQNO = StringUtil.CStr(dr["SEQNO"]);
                    drD.PRODNO = strProdNo;
                    drD.TRANOUTQTY = TranOutQty;
                    drD.CREATE_USER = logMsg.CREATE_USER;//異動人員
                    drD.CREATE_DTM = System.DateTime.Now;//建立時間
                    drD.MODI_USER = logMsg.MODI_USER;//異動人員
                    drD.MODI_DTM = System.DateTime.Now;//異動時間

                    dtD.Rows.Add(drD);
                    INV25_DTO.AcceptChanges();
                }

                try
                {
                    INV25_Facade facade = new INV25_Facade();

                    //更新資料庫
                    facade.AddNewOne_StoreTransfer(INV25_DTO, logMsg.MACHINE_ID);

                    this.lblOrderNo.Text = strSTNO;
                    this.lblStatus.Text = "20 在途".Substring(3);
                    this.btnPrint.ClientEnabled = true;
                    gvMaster.FindChildControl<ASPxButton>("btnAddNew").ClientEnabled = false;
                    gvMaster.FindChildControl<ASPxButton>("btnDelete").ClientEnabled = false;
                    this.btnSave.ClientEnabled = false;
                    this.btnDrop.ClientEnabled = false;
                    this.gvMaster.Enabled = false;

                    //彈跳視窗，提示移出端要列印移出單
                    ScriptManager.RegisterClientScriptBlock(this, GetType(), "AlertPrint", "alert('請列印移出單!!');", true);

                    Session["gvMaster"] = null;
                }
                catch (Exception ex)
                {
                    string exstring = ex.Message;
                    if (exstring.Substring(0, 3) != "000")
                    {
                        exstring = exstring.Substring(4, exstring.Length - 4);
                    }
                    else
                    {
                        exstring = ex.Message;
                    }
                    ScriptManager.RegisterClientScriptBlock(this, typeof(string), "checkSaveData", "alert('" + exstring.Replace("'", "-").Replace("\"", " ").Replace("\r", "").Replace("\n", "") +"');", true);
                    return;
                }
            }
        }

    }

    protected void btnPrint_Click(object sender, EventArgs e)
    {
        DataTable dtheader = new DataTable();
        dtheader.Columns.Add("header1", typeof(string));
        dtheader.Columns.Add("header2", typeof(string));
        DataRow NewRowHeader = dtheader.NewRow();
        NewRowHeader["header1"] = "移撥單號： " + this.lblOrderNo.Text;
        NewRowHeader["header2"] = "" ;
        dtheader.Rows.Add(NewRowHeader);

        NewRowHeader = dtheader.NewRow();
        NewRowHeader["header1"] = "移出門市： " + logMsg.STORENO + " " + new Store_Facade().GetStoreName(this.logMsg.STORENO);
        NewRowHeader["header2"] = "撥入門市： " + this.txtToStoreNO.Text + " " + new Store_Facade().GetStoreName(this.txtToStoreNO.Text);   
        dtheader.Rows.Add(NewRowHeader);

        NewRowHeader = dtheader.NewRow();
        NewRowHeader["header1"] = "移出日期： " + System.DateTime.Today.ToString("yyyy/MM/dd");
        NewRowHeader["header2"] = "撥入日期：_________________";
        dtheader.Rows.Add(NewRowHeader);

        DataTable dtfooter = new DataTable();
        dtfooter.Columns.Add("footer1", typeof(string));
        DataRow NewRowFooter = dtfooter.NewRow();
        NewRowFooter["footer1"] = " ";
        dtfooter.Rows.Add(NewRowFooter);

        NewRowFooter = dtfooter.NewRow();
        NewRowFooter["footer1"] = "移出人員：_________________";
        dtfooter.Rows.Add(NewRowFooter);

        NewRowFooter = dtfooter.NewRow();
        NewRowFooter["footer1"] = " ";
        dtfooter.Rows.Add(NewRowFooter);

        NewRowFooter = dtfooter.NewRow();
        NewRowFooter["footer1"] = "撥入人員：_________________";
        dtfooter.Rows.Add(NewRowFooter);

        DataTable dt = new INV25_Facade().Query_PrintStkChk(this.lblOrderNo.Text);

        //資料格式轉換
        int totalCount = dt.Rows.Count;
        DataTable dtOutput = new DataTable();
        foreach (DataColumn column in dt.Columns)
        {
            dtOutput.Columns.Add(column.ColumnName, column.DataType);
        }

        //string keyValue1 = null;
        //string keyValue2 = null;
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            DataRow row = dt.Rows[i];
            //string currentKeyValue1 = getDataKeyValue(row, "STDATE");
            //string currentKeyValue2 = getDataKeyValue(row, "STDATE", "STNO");

            DataRow newRow = dtOutput.NewRow();
            #region key 不重覆才要填值
            ////key 不重覆才要填值
            //if (keyValue1 != currentKeyValue1)
            //{
            //    newRow["STDATE"] = row["STDATE"];
            //    keyValue1 = currentKeyValue1;
            //}
            //if (keyValue2 != currentKeyValue2)
            //{
            //    newRow["STNO"] = row["STNO"];
            //    newRow["FROM_STORENAME"] = row["FROM_STORENAME"];
            //    newRow["TO_STORENAME"] = row["TO_STORENAME"];
            //    newRow["FROM_EMPNAME"] = row["FROM_EMPNAME"];
            //    newRow["TO_EMPNAME"] = row["TO_EMPNAME"];

            //    keyValue2 = currentKeyValue2;
            //}

            #endregion
            //fill data
            newRow["商品料號"] = row["商品料號"];
            newRow["商品名稱"] = row["商品名稱"];
            newRow["數量"] = row["數量"];
            newRow["IMEI"] = StringUtil.CStr(row["IMEI"]).Trim().Replace(";", "\r\n");

            dtOutput.Rows.Add(newRow);
        }

        //string filename = new Output().Print("PDF", "移出單", dtheader, dt, dtfooter);
        string filename = new Output().Print("PDF", "移出單", dtheader, dtOutput, dtfooter);
        //Response.Redirect(Utils.CreateTamperProofDownloadURL(filename));
        ProcessRequest(filename);
    }

    private static string getDataKeyValue(DataRow row, params string[] keyFieldNames)
    {
        //將每個鍵值加入 List
        List<string> keyValues = new List<string>();
        for (int i = 0; i < keyFieldNames.Length; i++)
        {
            keyValues.Add(StringUtil.CStr(row[keyFieldNames[i]]));
        }

        //將鍵值轉為逗號分隔的字串
        string dataKeyValue = string.Empty;
        if (keyFieldNames.Length > 0)
        {
            dataKeyValue = string.Join(",", keyValues.ToArray());
        }

        return dataKeyValue;
    }

    public void ProcessRequest(string filename)
    {
        string filePath = (new FET.POS.Model.Facade.FacadeImpl.SAL01_Facade()).getUploadPath();
        ScriptManager.RegisterClientScriptBlock(this,
                                               this.GetType(),
                                               "test",
                                               "document.getElementById('" + fDownload.ClientID + "').src='" + Request.ApplicationPath + filePath + "/" + filename + "';",
                                               true);
    }

    #endregion

    #region gvMaster 觸發事件

    protected void gvMaster_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
    {
        if (Session["gvMaster"] != null)
        {
            DataTable dt = Session["gvMaster"] as DataTable;

            DataRow dr = dt.NewRow();

            dr["STORETRANSFER_D_ID"] = StringUtil.CStr(Session["STORETRANSFER_D_ID"]);
            dr["SEQNO"] = dt.Rows.Count + 1;
            dr["STNO"] = "";
            dr["PRODNO"] = ((PopupControl)gvMaster.FindEditRowCellTemplateControl((GridViewDataColumn)gvMaster.Columns["PRODNO"], "txtProductCode")).Text;
            dr["PRODNAME"] = ((ASPxTextBox)gvMaster.FindEditRowCellTemplateControl((GridViewDataColumn)gvMaster.Columns["ProductName"], "lblProductName")).Text;
            dr["TRANOUTQTY"] = Convert.ToInt64(((ASPxTextBox)gvMaster.FindEditRowCellTemplateControl((GridViewDataColumn)gvMaster.Columns["TRANOUTQTY"], "txtTranOutQty")).Text);
            dr["IMEI_QTY"] = ((ASPxTextBox)gvMaster.FindEditRowCellTemplateControl((GridViewDataColumn)gvMaster.Columns["IMEI"], "lblIMEI_QTY")).Text;
            dr["IMEI_FLAG"] = ((ASPxTextBox)gvMaster.FindEditRowCellTemplateControl((GridViewDataColumn)gvMaster.Columns["IMEI"], "txtIMEIFlag")).Text;
            dr["IMEI"] = ((PopupControl)gvMaster.FindEditRowCellTemplateControl((GridViewDataColumn)gvMaster.Columns["IMEI"], "txtIMEI")).Text;

            dt.Rows.Add(dr);
            dt.AcceptChanges();

            gvMaster.CancelEdit();
            e.Cancel = true;

            BindMasterData();
            Session["STORETRANSFER_D_ID"] = null;
            DisableButton(false); //可點擊 "確定移出" 和 "取消" 按鈕
        }


    }

    protected void gvMaster_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
    {
        if (Session["gvMaster"] != null)
        {
            DataTable dt = Session["gvMaster"] as DataTable;

            DataRow dr = dt.Rows.Find(new object[]{ StringUtil.CStr(e.Keys["STORETRANSFER_D_ID"]) } );

            dr["PRODNO"] = ((PopupControl)gvMaster.FindEditRowCellTemplateControl((GridViewDataColumn)gvMaster.Columns["PRODNO"], "txtProductCode")).Text;
            dr["PRODNAME"] = ((ASPxTextBox)gvMaster.FindEditRowCellTemplateControl((GridViewDataColumn)gvMaster.Columns["ProductName"], "lblProductName")).Text;
            dr["TRANOUTQTY"] = Convert.ToInt64(((ASPxTextBox)gvMaster.FindEditRowCellTemplateControl((GridViewDataColumn)gvMaster.Columns["TRANOUTQTY"], "txtTranOutQty")).Text);
            dr["IMEI_QTY"] = ((ASPxTextBox)gvMaster.FindEditRowCellTemplateControl((GridViewDataColumn)gvMaster.Columns["IMEI"], "lblIMEI_QTY")).Text;
            dr["IMEI_FLAG"] = ((ASPxTextBox)gvMaster.FindEditRowCellTemplateControl((GridViewDataColumn)gvMaster.Columns["IMEI"], "txtIMEIFlag")).Text;
            dr["IMEI"] = ((PopupControl)gvMaster.FindEditRowCellTemplateControl((GridViewDataColumn)gvMaster.Columns["IMEI"], "txtIMEI")).Text;

            dt.AcceptChanges();

            gvMaster.CancelEdit();
            e.Cancel = true;

            BindMasterData();
            DisableButton(false); //可點擊 "確定移出" 和 "取消" 按鈕

        }
    }

    protected void gvMaster_HtmlRowCreated(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewTableRowEventArgs e)
    {
        if (e.RowType == GridViewRowType.Data)
        {
            ASPxLabel lblIMEI = e.Row.FindChildControl<ASPxLabel>("lblIMEI");

            string strID = StringUtil.CStr(e.GetValue("STORETRANSFER_D_ID"));

            // 繫結明細資料表           
            lblIMEI.Attributes["onmouseover"] = string.Format("show('{0}');", IMEIContent("STORETRANSFER_IMEI", strID, StringUtil.CStr(e.GetValue("PRODNO"))));
            lblIMEI.Attributes["onmouseout"] = "hide();";

            int intC_IMEI = int.Parse(StringUtil.CStr(e.GetValue("TRANOUTQTY")) == "" ? "0" : StringUtil.CStr(e.GetValue("TRANOUTQTY")));
            int intS_IMEI = int.Parse(StringUtil.CStr(e.GetValue("IMEI_QTY")) == "" ? "0" : StringUtil.CStr(e.GetValue("IMEI_QTY")));
            ASPxImage imgIMEI = e.Row.FindChildControl<ASPxImage>("imgIMEI");

            //if (intC_IMEI == 0)
            if (StringUtil.CStr(e.GetValue("IMEI_FLAG")) != "3" && StringUtil.CStr(e.GetValue("IMEI_FLAG")) != "4")
            {
                HtmlGenericControl divIMEI = e.Row.FindChildControl<HtmlGenericControl>("divIMEI");
                divIMEI.Visible = false;
                imgIMEI.Visible = false;
            }

            if (imgIMEI.Visible)
            {
               imgIMEI.ImageUrl = (intC_IMEI == intS_IMEI? "~/Icon/check.png": "~/Icon/non_complete.png");
            }

            PopupControl lblShowIMEI = e.Row.FindChildControl<PopupControl>("lblShowIMEI");
            lblShowIMEI.Enabled = false;

        }

        if (e.RowType == GridViewRowType.InlineEdit)
        {
            string IMEIFlag = e.Row.FindChildControl<ASPxTextBox>("txtIMEIFlag").Text; 
            string ProdNo = ((PopupControl)gvMaster.FindEditRowCellTemplateControl((GridViewDataColumn)gvMaster.Columns["PRODNO"], "txtProductCode")).Text;
            string TRANOUTQTY = e.Row.FindChildControl<ASPxTextBox>("txtTranOutQty").Text; 
            string IMEI_QTY = e.Row.FindChildControl<ASPxTextBox>("lblIMEI_QTY").Text; 

            //設定顯示IMEI圖示的ImageURL
            int intC_IMEI=0;
            bool isNum = int.TryParse(TRANOUTQTY, out intC_IMEI);
            //int intC_IMEI = int.Parse(TRANOUTQTY == "" ? "0" :  ? isNum ? TRANOUTQTY: "0");
            int intS_IMEI = int.Parse(IMEI_QTY == "" ? "0" : IMEI_QTY);
            ASPxImage imgIMEI = e.Row.FindChildControl<ASPxImage>("imgIMEI");

            if (imgIMEI.Visible)
            {
                imgIMEI.ImageUrl = (intC_IMEI == intS_IMEI ? "~/Icon/check.png" : "~/Icon/non_complete.png");
            }


            //設定IMEI視窗的URL
            ASPxTextBox lblIMEI_QTY = e.Row.FindChildControl<ASPxTextBox>("lblIMEI_QTY");
            string strID = e.KeyValue == null ? StringUtil.CStr(Session["STORETRANSFER_D_ID"]) : StringUtil.CStr(e.KeyValue);

            ASPxTextBox hdSTORETRANSFER_D_ID = e.Row.FindChildControl<ASPxTextBox>("hdSTORETRANSFER_D_ID");
            hdSTORETRANSFER_D_ID.Text = strID;

            // 繫結明細資料表  
            lblIMEI_QTY.Attributes["onmouseover"] = string.Format("show('{0}');", IMEIContent("STORETRANSFER_IMEI", strID, ""));
            lblIMEI_QTY.Attributes["onmouseout"] = "hide();";

            PopupControl txtIMEI = e.Row.FindChildControl<PopupControl>("txtIMEI");
            ASPxPopupControl ASPxPopupControl1 = txtIMEI.FindChildControl<ASPxPopupControl>("ASPxPopupControl1");
            txtIMEI.KeyFieldValue1 = "STORETRANSFER_IMEI;" + strID + ";" + ProdNo + ";" + TRANOUTQTY;
            //ASPxPopupControl1.ContentUrl = "~/VSS/SAL/SAL01/SAL01_inputIMEIData.aspx?SysDate=Date()&KeyFieldValue1=" + txtIMEI.KeyFieldValue1;

            //**2011/04/26 Tina：傳遞參數時，要先以加密處理。
            string encryptUrl = Utils.Param_Encrypt("SysDate=Date()&KeyFieldValue1=" + txtIMEI.KeyFieldValue1);
            ASPxPopupControl1.ContentUrl = string.Format("~/VSS/SAL/SAL01/SAL01_inputIMEIData.aspx?Param={0}", encryptUrl);

            if (IMEIFlag == "3" || IMEIFlag == "4")
            {
                txtIMEI.Enabled = true;
                imgIMEI.ClientVisible = true;
  
            }
            else
            {
                txtIMEI.Enabled = false;
                imgIMEI.ClientVisible = false;

            }

        }
        
    }

    protected void gvMaster_InitNewRow(object sender, DevExpress.Web.Data.ASPxDataInitNewRowEventArgs e)
    {
        Session["STORETRANSFER_D_ID"] = GuidNo.getUUID();

        ASPxLabel lblIMEI = gvMaster.FindEditRowCellTemplateControl((GridViewDataColumn)gvMaster.Columns["IMEI"], "lbIMEI_QTY") as ASPxLabel;
        PopupControl txtIMEI = gvMaster.FindEditRowCellTemplateControl((GridViewDataColumn)gvMaster.Columns["IMEI"], "txtIMEI") as PopupControl;
        ASPxImage imgIMEI = gvMaster.FindEditRowCellTemplateControl((GridViewDataColumn)gvMaster.Columns["imgIMEI"], "imgIMEI") as ASPxImage;
        imgIMEI.ClientVisible = false;
        txtIMEI.Enabled = false;

        DisableButton(true); //不可點擊 "確定移出" 和 "取消" 按鈕
    }

    protected void gvMaster_CancelRowEditing(object sender, DevExpress.Web.Data.ASPxStartRowEditingEventArgs e)
    {
        DisableButton(false); //可點擊 "確定移出" 和 "取消" 按鈕
    }

    protected void gvMaster_CommandButtonInitialize(object sender, ASPxGridViewCommandButtonEventArgs e)
    {
        //if (e.ButtonType == ColumnCommandButtonType.Edit || e.ButtonType == ColumnCommandButtonType.Update || e.ButtonType == ColumnCommandButtonType.Cancel)
        //{
        //    e.Visible = false;
        //}
    }

    protected void gvMaster_RowValidating(object sender, DevExpress.Web.Data.ASPxDataValidationEventArgs e)
    {
        string strProdNo = ((PopupControl)gvMaster.FindEditRowCellTemplateControl((GridViewDataColumn)gvMaster.Columns["PRODNO"], "txtProductCode")).Text.Trim();

        if (Session["gvMaster"] != null)
        {
            DataTable dt = Session["gvMaster"] as DataTable;

            string expression;
            expression = "PRODNO = '" + strProdNo + "'";
            DataRow[] data = dt.Select(expression);
            for (int i = 0; i < data.Length; i++)
            {
                string strSEQNO_Table = e.Keys["STORETRANSFER_D_ID"] == null ? StringUtil.CStr(Session["STORETRANSFER_D_ID"]) : StringUtil.CStr(e.Keys["STORETRANSFER_D_ID"]);
                string strSEQNO = StringUtil.CStr(data[i]["STORETRANSFER_D_ID"]);
                if (!string.IsNullOrEmpty(strSEQNO_Table) && strSEQNO_Table != strSEQNO)
                {
                    e.RowError += "商品料號重複!!";
                }

            } 
        }

        ASPxTextBox txtIMEIFlag = gvMaster.FindEditRowCellTemplateControl((GridViewDataColumn)gvMaster.Columns["IMEI"], "txtIMEIFlag") as ASPxTextBox;
        ASPxTextBox TRANOUTQTY = gvMaster.FindEditRowCellTemplateControl((GridViewDataColumn)gvMaster.Columns["TRANOUTQTY"], "txtTranOutQty") as ASPxTextBox;
        ASPxTextBox IMEI_QTY = gvMaster.FindEditRowCellTemplateControl((GridViewDataColumn)gvMaster.Columns["IMEI"], "lblIMEI_QTY") as ASPxTextBox;

        if (string.IsNullOrEmpty(e.RowError) && (txtIMEIFlag.Text == "3" || txtIMEIFlag.Text == "4") && (string.IsNullOrEmpty(IMEI_QTY.Text) || TRANOUTQTY.Text != IMEI_QTY.Text))
        {
            e.RowError += "請輸入正確的IMEI數!!";
        }

        DataTable dtProdNo = new Product_Facade().Query_ProductInfo(strProdNo);
        if (string.IsNullOrEmpty(e.RowError) && dtProdNo.Rows.Count <= 0)
        {
            e.RowError += "商品料號不存在或已失效!!";
        }

        long HandQty = INV25_PageHelper.GetInvOnHandCurrent(strProdNo, logMsg.STORENO);
        long TranOutQty = Convert.ToInt64(((ASPxTextBox)gvMaster.FindEditRowCellTemplateControl((GridViewDataColumn)gvMaster.Columns["TRANOUTQTY"], "txtTranOutQty")).Text.Trim());
        if (string.IsNullOrEmpty(e.RowError) && TranOutQty > HandQty)
        {
            e.RowError += "移出數量不可多於現在庫存數!!";
        }

        if (!string.IsNullOrEmpty(e.RowError))
        {
            if (e.IsNewRow)
            {
                BindMasterData();
            }
            return;
        }
    }

    protected void gvMaster_PageIndexChanged(object sender, EventArgs e)
    {
        BindMasterData();
    }

    protected void gvMaster_StartRowEditing(object sender, DevExpress.Web.Data.ASPxStartRowEditingEventArgs e)
    {
        gvMaster.Selection.UnselectAll();
        DisableButton(true); //不可點擊 "確定移出" 和 "取消" 按鈕
    }

    protected void btnAddNewM_Click(object sender, EventArgs e)
    {
        if (getStoreInfo(txtToStoreNO.Text) != "")
        {
            gvMaster.Selection.UnselectAll();
            gvMaster.AddNewRow();
        }
        else
        {
            gvMaster.FindChildControl<ASPxButton>("btnAddNew").ClientEnabled = false;
            gvMaster.FindChildControl<ASPxButton>("btnDelete").ClientEnabled = false;
            btnSave.ClientEnabled = false;
        }
    }

    #endregion

    /// <summary>
    /// 點擊 / 無法點擊 "確定移出" 和 "取消" 按鈕
    /// </summary>
    /// <param name="IsDisabled">不可編輯?</param>
    private void DisableButton(bool IsDisabled)
    {
        if (this.gvMaster.VisibleRowCount > 0 && !IsDisabled)
        {
            this.btnSave.ClientEnabled = true;
        }

        this.btnDrop.ClientEnabled = !IsDisabled;
    }

    //ajax 呼當前網頁的方式
    [System.Web.Services.WebMethod()]
    [System.Web.Script.Services.ScriptMethod()]
    static public string getProductInfo(string PRODNO)
    {
        string strInfo = "";
        if (!string.IsNullOrEmpty(PRODNO))
        {
            DataTable dt = new Product_Facade().Query_ProductInfo(PRODNO);
            if (dt.Rows.Count >0)
            {
                strInfo = StringUtil.CStr(dt.Rows[0]["PRODNO"]) + ";" + StringUtil.CStr(dt.Rows[0]["PRODNAME"]) + ";" + StringUtil.CStr(dt.Rows[0]["IMEI_FLAG"]);
            }
        }

        return strInfo;
    }

    //ajax 呼當前網頁的方式
    [System.Web.Services.WebMethod()]
    [System.Web.Script.Services.ScriptMethod()]
    static public string getStoreInfo(string StoreNO)
    {
        string strInfo = "";
        if (!string.IsNullOrEmpty(StoreNO))
        {
            DataTable dt = new Store_Facade().Query_StoreInfo(StoreNO);
            if (dt.Rows.Count > 0)
            {
                strInfo = StringUtil.CStr(dt.Rows[0]["STORENAME"]);
            }
        }

        return strInfo;
    }

    //ajax IMEI清單
    [System.Web.Services.WebMethod()]
    [System.Web.Script.Services.ScriptMethod()]
    static public string IMEIContent(string TABLENAME, string ID, string PRODNO)
    {
        DataTable dt = new IMEI_Facade().getINV_IMEI(TABLENAME, ID, PRODNO);

        string IMEI_FORMAT = "<table border=\"1\">";
        foreach (DataRow dr in dt.Rows)
        {
            IMEI_FORMAT += "<tr><td>" + StringUtil.CStr(dr["IMEI"]) + "</td></tr>";
        }
        IMEI_FORMAT += "</table>";

        return IMEI_FORMAT;
    }
}
