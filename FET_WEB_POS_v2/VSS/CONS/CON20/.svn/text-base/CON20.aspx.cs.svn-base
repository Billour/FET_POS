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


public partial class VSS_CONS_CON20 : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack && !Page.IsCallback)
        {
            Session["gvMaster"] = null;
            //取得空的資料表
            bindMasterEmptyData();

            this.btnPrint.ClientEnabled = false;
            this.btnSave.ClientEnabled = false;
            this.btnDrop.ClientEnabled = false;
            this.lblStatus.Text = "00 未存檔".Substring(3);
            this.lblDateTime.Text = System.DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");
            this.lblUser.Text = logMsg.OPERATOR + " " + new Employee_Facade().GetEmpName(logMsg.OPERATOR);

        }
        else
        {
            DataTable dtGvMaster = Session["gvMaster"] as DataTable;
            if (dtGvMaster != null)
            {
                gvMaster.DataSource = dtGvMaster;
                gvMaster.DataBind();
            }
        }

    }
    protected void bindMasterEmptyData()
    {
        DataTable dtResult = new DataTable();
        if (Session["gvMaster"] == null)
        {
            dtResult.Columns.Add("PRODTYPENO", typeof(string));
            dtResult.Columns.Add("PRODTYPENAME", typeof(string));
            dtResult.Columns.Add("PRODNO", typeof(string));
            dtResult.Columns.Add("PRODNAME", typeof(string));
            dtResult.Columns.Add("TRANOUTQTY", typeof(string));
        }
        else
        {
            dtResult = Session["gvMaster"] as DataTable;
        }

        Session["gvMaster"] = dtResult;
        gvMaster.DataSource = dtResult;
        gvMaster.DataBind();


    }
    protected void bindMasterData()
    {
        gvMaster.DataSource = (DataTable)Session["gvMaster"];
        gvMaster.DataBind();
    }


    #region 按扭事件

    /// <summary>
    /// 點擊 / 無法點擊 "確定移出" 和 "取消" 按鈕
    /// </summary>
    /// <param name="IsDisabled">不可編輯?</param>
    private void DisableButton(bool IsDisabled)
    {
        
        this.btnSave.ClientEnabled = !IsDisabled;
        this.btnDrop.ClientEnabled = !IsDisabled;
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
                    this.lblError.Text = strError;
                    return;
                }


                CON20_CSM_StoreTransfer_DTO CON20_DTO = new CON20_CSM_StoreTransfer_DTO();
                CON20_CSM_StoreTransfer_DTO.CSM_STORETRANSFER_MDataTable dtM = CON20_DTO.CSM_STORETRANSFER_M;
                CON20_CSM_StoreTransfer_DTO.CSM_STORETRANSFER_MRow drM = dtM.NewCSM_STORETRANSFER_MRow();
                CON20_CSM_StoreTransfer_DTO.CSM_STORETRANSFER_DDataTable dtD = CON20_DTO.CSM_STORETRANSFER_D;

                //string strSTNO = this.hdSTNO.Value;
                string strSTNO = SerialNo.GenNo("ST{0}-"); //"ST{0}-100815001";
                strSTNO = string.Format(strSTNO, logMsg.STORENO); //{0} 帶入登入者的門市編號
                Session["CSM_STORETRANSFERM_ID"] = GuidNo.getUUID();
                drM.STNO = strSTNO; //調撥單號
                //    drM.STDATE = OracleDBUtil.WorkDay(logMsg.STORENO); //移出日期
                drM.STDATE = System.DateTime.Now;//移出日期
                drM.TSTATUS = "20";//調撥單狀態：  00 未存檔,10 暫存(預留), 20 在途, 30 已撥入
                drM.FROM_STORE_NO = logMsg.STORENO;//移出門市
                //   drM.UPDDATE = OracleDBUtil.WorkDay(logMsg.STORENO); //調出端異動時間
                drM.UPDDATE = System.DateTime.Now;//調出端異動時間
                drM.STUSRNO = logMsg.CREATE_USER; //調出端-調出人員
                drM.TO_STORE_NO = this.txtToStoreNO.Text; //調入門市
                drM.CREATE_USER = logMsg.CREATE_USER; //建立人員
                drM.CREATE_DTM = System.DateTime.Now;//建立時間
                drM.MODI_USER = logMsg.MODI_USER;//異動人員
                drM.MODI_DTM = System.DateTime.Now;//異動時間
                drM.CSM_STORETRANSFERM_ID = Session["CSM_STORETRANSFERM_ID"].ToString();
                dtM.Rows.Add(drM);
                CON20_DTO.AcceptChanges();
                int i = 1;
                foreach (DataRow dr in dtData.Rows)
                {
                    string strProdNo = dr["PRODNO"].ToString();

                    long HandQty = INV25_PageHelper.GetInvOnHandCurrent(strProdNo, logMsg.STORENO);  //庫存量
                    long TranOutQty = Convert.ToInt64(dr["TRANOUTQTY"].ToString());  //移出量
                    if (TranOutQty > HandQty)
                    {
                        strError = "資料列中移出數量多於現在庫存數!!";
                        this.lblError.Text = strError;
                        return;
                    }

                    CON20_CSM_StoreTransfer_DTO.CSM_STORETRANSFER_DRow drD = dtD.NewCSM_STORETRANSFER_DRow();
                    drD.SUPP_ID = new Supplier_Facade().GetSuppId2(strProdNo);
                    drD.CSM_STORETRANSFER_D_ID = GuidNo.getUUID();
                    //drD.STNO = strSTNO;
                    drD.SEQNO = i.ToString();
                    drD.PRODNO = strProdNo;
                    drD.TRANOUTQTY = TranOutQty;
                    drD.CREATE_USER = logMsg.CREATE_USER;//異動人員
                    drD.CREATE_DTM = System.DateTime.Now;//建立時間
                    drD.MODI_USER = logMsg.MODI_USER;//異動人員
                    drD.MODI_DTM = System.DateTime.Now;//異動時間
                    drD.CSM_STORETRANSFERM_ID = Session["CSM_STORETRANSFERM_ID"].ToString();
                    dtD.Rows.Add(drD);
                    CON20_DTO.AcceptChanges();
                    i++;
                }


                CON20_Facade facade = new CON20_Facade();

                //更新資料庫
                facade.AddNewOne_StoreTransfer(CON20_DTO, logMsg.MACHINE_ID);

                this.lblOrderNo.Text = strSTNO;
                this.lblStatus.Text = "20 在途".Substring(3);
                this.btnPrint.ClientEnabled = true;
                gvMaster.FindChildControl<ASPxButton>("btnAddNewRow").ClientEnabled = false;
                gvMaster.FindChildControl<ASPxButton>("btnDeleteRow").ClientEnabled = false;
                this.btnSave.ClientEnabled = false;
                this.btnDrop.ClientEnabled = false;
                this.gvMaster.Enabled = false;

                //彈跳視窗，提示移出端要列印移出單
                ScriptManager.RegisterClientScriptBlock(this, GetType(), "AlertPrint", "alert('請列印移出單!!');", true);

                Session["gvMaster"] = null;


            }
        }

    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Session["gvMaster"] = null;
        //取得空的資料表
        bindMasterEmptyData();
        if (gvMaster.IsEditing)
        {
            gvMaster.CancelEdit();
        }
        this.txtToStoreNO.Text = "";
    }
    protected void btnAddRow_Click(object sender, EventArgs e)
    {
        if (getStoreInfo(txtToStoreNO.Text) != "")
        {
            gvMaster.Selection.UnselectAll();
            gvMaster.AddNewRow();
            // this.btnPrint.ClientEnabled = false;
        }
        else
        {
            gvMaster.FindChildControl<ASPxButton>("btnAddNewRow").ClientEnabled = false;
            gvMaster.FindChildControl<ASPxButton>("btnDeleteRow").ClientEnabled = false;
            //btnSave.ClientEnabled = false;
        }
    }

    protected void btnDeleteRow_Click(object sender, EventArgs e)
    {

        DataTable dtSYS;
        if (Session["gvMaster"] == null || StringUtil.CStr(Session["gvMaster"]) == "")
        {
            dtSYS = new DataTable();
        }
        else
        {
            dtSYS = Session["gvMaster"] as DataTable;
        }



        if (dtSYS.Rows.Count > 0)
        {

            List<object> keyValues = this.gvMaster.GetSelectedFieldValues(gvMaster.KeyFieldName);
            foreach (object key in keyValues)
            {
                DataRow drSYS = dtSYS.Select("PRODNO='" + key.ToString() + "'")[0];
                dtSYS.Rows.Remove(drSYS);
            }
            gvMaster.Selection.UnselectAll();
            Session["gvMaster"] = dtSYS;
            gvMaster.DataSource = Session["gvMaster"];
            gvMaster.DataBind();

        }
    }


    protected void btnPrint_Click(object sender, EventArgs e)
    {
        DataTable dtheader = new DataTable();
        dtheader.Columns.Add("header1", typeof(string));
        dtheader.Columns.Add("header2", typeof(string));
        DataRow NewRowHeader = dtheader.NewRow();
        NewRowHeader["header1"] = "移撥單號： " + this.lblOrderNo.Text;
        NewRowHeader["header2"] = "";
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

        DataTable dt = new CON20_Facade().Query_PrintStkChk(Session["CSM_STORETRANSFERM_ID"].ToString());

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
            newRow["商品類別"] = row["商品類別"];
            newRow["商品料號"] = row["商品料號"];
            newRow["商品名稱"] = row["商品名稱"];
            newRow["數量"] = row["數量"];
            dtOutput.Rows.Add(newRow);
        }


        string filename = new Output().Print("PDF", "寄銷商品移出單", dtheader, dtOutput, dtfooter);
        //Response.Redirect(Utils.CreateTamperProofDownloadURL(filename));
        ProcessRequest(filename);
    }
    #endregion
    public void ProcessRequest(string filename)
    {
        string filePath = (new FET.POS.Model.Facade.FacadeImpl.SAL01_Facade()).getUploadPath();
        ScriptManager.RegisterClientScriptBlock(this,
                                               this.GetType(),
                                               "test",
                                               "document.getElementById('" + fDownload.ClientID + "').src='" + Request.ApplicationPath + filePath + "/" + filename + "';",
                                               true);
    }
    
    #region grid事件
    protected void gvMaster_CommandButtonInitialize(object sender, ASPxGridViewCommandButtonEventArgs e)
    {
        if (e.ButtonType == ColumnCommandButtonType.Edit || e.ButtonType == ColumnCommandButtonType.Update || e.ButtonType == ColumnCommandButtonType.Cancel)
        {
            e.Visible = false;
        }
    }
    protected void gvMaster_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
    {

        DataTable dtResult = new DataTable();
        if (Session["gvMaster"] == null)
        {
            dtResult.Columns.Add("PRODTYPENO", typeof(string));
            dtResult.Columns.Add("PRODTYPENAME", typeof(string));
            dtResult.Columns.Add("PRODNO", typeof(string));
            dtResult.Columns.Add("PRODNAME", typeof(string));
            dtResult.Columns.Add("TRANOUTQTY", typeof(string));
        }
        else
        {
            dtResult = Session["gvMaster"] as DataTable;
        }


        DataRow dr = dtResult.NewRow();
        dr["PRODNO"] = ((PopupControl)gvMaster.FindEditRowCellTemplateControl((GridViewDataColumn)gvMaster.Columns["PRODNO"], "txtProductCode")).Text;
        dr["PRODTYPENAME"] = ((ASPxTextBox)gvMaster.FindEditRowCellTemplateControl((GridViewDataColumn)gvMaster.Columns["PRODTYPENAME"], "lblProductTypeName")).Text;
        dr["PRODTYPENO"] = ((ASPxTextBox)gvMaster.FindEditRowCellTemplateControl((GridViewDataColumn)gvMaster.Columns["PRODTYPENAME"], "lblProductTypeNo")).Text;
        dr["PRODNAME"] = ((ASPxTextBox)gvMaster.FindEditRowCellTemplateControl((GridViewDataColumn)gvMaster.Columns["PRODNAME"], "lblProductName")).Text;
        dr["TRANOUTQTY"] = Convert.ToInt64(((ASPxTextBox)gvMaster.FindEditRowCellTemplateControl((GridViewDataColumn)gvMaster.Columns["TRANOUTQTY"], "txtTranOutQty")).Text);

        dtResult.Rows.Add(dr);
        dtResult.AcceptChanges();
        Session["gvMaster"] = dtResult;
        gvMaster.CancelEdit();
        e.Cancel = true;
        gvMaster.DataSource = dtResult;
        gvMaster.DataBind();
        DisableButton(false); //可點擊 "確定移出" 和 "取消" 按鈕
        
    }
    protected void gvMaster_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
    {
        DataTable dt;

        if (Session["gvMaster"] == null || StringUtil.CStr(Session["gvMaster"]) == "")
        {
            dt = new DataTable();
        }
        else
        {
            dt = Session["gvMaster"] as DataTable;
        }

        string sProdNo = e.NewValues["PRODNO"].ToString();
        string sProdTypeName = e.NewValues["PRODTYPENAME"].ToString();
        string sProdTypeNo = e.NewValues["PRODTYPENO"].ToString();
        string sProdName = e.NewValues["PRODNAME"].ToString();
        string sTranOutQty = e.NewValues["TRANOUTQTY"].ToString();
        string sOldProdNo = e.OldValues["PRODNO"].ToString();
        string sOldProdTypeName = e.OldValues["PRODTYPENAME"].ToString();
        string sOldProdTypeNo = e.OldValues["PRODTYPENO"].ToString();
        string sOldProdName = e.OldValues["PRODNAME"].ToString();
        string sOldTranOutQty = e.OldValues["TRANOUTQTY"].ToString();
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            DataRow dr = dt.Rows[i];
            if (dr["PRODNO"].ToString().CompareTo(sOldProdNo) == 0)
            {
                dr["PRODNO"] = sProdNo.Trim();
                dr["PRODTYPENAME"] = sProdTypeName.Trim();
                dr["PRODTYPENO"] = sProdTypeNo.Trim();
                dr["PRODNAME"] = sProdName.Trim();
                dr["TRANOUTQTY"] = sTranOutQty.Trim();
                dt.AcceptChanges();
                break;
            }
        }

        gvMaster.CancelEdit();
        e.Cancel = true;
        Session["gvMaster"] = dt;
        gvMaster.DataSource = dt;
        gvMaster.DataBind();


        DisableButton(false); //可點擊 "確定移出" 和 "取消" 按鈕



    }
    protected void gvMaster_PageIndexChanged(object sender, EventArgs e)
    {
        bindMasterData();
    }
    protected void gvMaster_CancelRowEditing(object sender, DevExpress.Web.Data.ASPxStartRowEditingEventArgs e)
    {
        DisableButton(false); //可點擊 "確定移出" 和 "取消" 按鈕
    }
    protected void gvMaster_RowValidating(object sender, DevExpress.Web.Data.ASPxDataValidationEventArgs e)
    {
        string sProdNo = e.NewValues["PRODNO"].ToString();
        if (Session["gvMaster"] != null)
        {
            //string sOldProdNo = e.Keys["PRODNO"].ToString();
            DataTable dt = Session["gvMaster"] as DataTable;
            string expression;
            expression = "PRODNO = '" + sProdNo + "'";

            DataRow[] data = dt.Select(expression);
            if (data.Length > 0 && gvMaster.IsNewRowEditing)
            {
                e.RowError += "商品料號資料已存在,請重新輸入!!";
                return;
            }
            else if (data.Length > 1 && gvMaster.IsEditing)
            {
                e.RowError += "商品料號資料已存在,請重新輸入!!";
                return;
            }
            
        }

        DataTable dtProdNo = new Product_Facade().Query_ProductInfo(sProdNo);
        if (dtProdNo.Rows.Count <= 0)
        {
            e.RowError += "商品料號不存在或已失效!!";
            return;
        }

        string sTranOutQty = e.NewValues["TRANOUTQTY"].ToString();
        long HandQty = INV25_PageHelper.GetInvOnHandCurrent(sProdNo, logMsg.STORENO);
        long TranOutQty = Convert.ToInt64(sTranOutQty);
        if (TranOutQty > HandQty)
        {
            e.RowError += "移出數量不可多於現在庫存數!!";
            return;
        }
    }
    protected void gvMaster_InitNewRow(object sender, DevExpress.Web.Data.ASPxDataInitNewRowEventArgs e)
    {
        PopupControl tb1 = gvMaster.FindChildControl<PopupControl>("txtProductCode");
        tb1.Focus();

        DisableButton(true); //不可點擊 "確定移出" 和 "取消" 按鈕
    }
    protected void gvMaster_StartRowEditing(object sender, DevExpress.Web.Data.ASPxStartRowEditingEventArgs e)
    {
        DisableButton(true); //不可點擊 "確定移出" 和 "取消" 按鈕
    }
    
    #endregion


    #region 呼叫前端ajax

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
                strInfo = dt.Rows[0]["STORENAME"].ToString();
            }
        }

        return strInfo;
    }

    [System.Web.Services.WebMethod()]
    [System.Web.Script.Services.ScriptMethod()]
    static public string getProductInfo(string PRODNO)
    {
        string strInfo = "";
        if (!string.IsNullOrEmpty(PRODNO))
        {
            DataTable dt = new Product_Facade().Query_ProductConsignmentSale(PRODNO, "", "");
            if (dt.Rows.Count > 0)
            {
                strInfo = dt.Rows[0]["PRODNO"].ToString() + ";" + dt.Rows[0]["PRODNAME"].ToString() + ";" + dt.Rows[0]["PRODTYPENAME"].ToString() + ";" + dt.Rows[0]["PRODTYPENO"].ToString();
            }
        }

        return strInfo;
    }
    #endregion
}
