using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Collections;
using System.Collections.Specialized;
using System.IO;
using Advtek.Utility;
using DevExpress.Web;
using DevExpress.Web.ASPxGridView;
using DevExpress.Web.ASPxPager;
using iTextSharp.text;
using iTextSharp.text.pdf;

using FET.POS.Model.Common;
using FET.POS.Model.DTO;
using FET.POS.Model.Facade;
using FET.POS.Model.Facade.FacadeImpl;
using System.Xml;
using System.Threading;
using DevExpress.Web.ASPxEditors;

public partial class VSS_INV_INV11 : BasePage
{
    private string qDno
    {
        get
        {
            string Result = string.Empty;
            if (!string.IsNullOrEmpty(Request.QueryString["Param"]))
            {
                NameValueCollection qscoll = Utils.Param_UrlDecode(Request.QueryString["Param"]);
                foreach (string key in qscoll.AllKeys)
                {
                    if (key == "InventoryNo")
                    {
                        Result = string.Join(",", qscoll.GetValues(key));
                        break;
                    }
                }
            }
            return Result.Trim();
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack && !Page.IsCallback)
        {
            if (logMsg.ROLE_TYPE == StringUtil.CStr(System.Configuration.ConfigurationManager.AppSettings["DefaultRoleHQ"]))
            {
                //彈跳視窗
                ScriptManager.RegisterClientScriptBlock(this, GetType(), "AlertMessage", "alert('非門市人員無法使用此功能!!');", true);
                this.rbStkChkType.Enabled = false;
                this.rbActivityType.Enabled = false;
                btnOK.Enabled = false;
                divBTN.Visible = false;
                btnQueryEdit.Enabled = false;
                return;
            }

            this.ViewState["InventoryNo"] = qDno;

            gvMaster.Visible = false;
            divBTN.Visible = false;
            this.rbStkChkType.Items[0].Enabled = true;  //重盤
            this.rbStkChkType.Items[1].Enabled = true;  //全盤
            this.rbStkChkType.Items[2].Enabled = true; //關帳日盤點
            string WorkDate = OracleDBUtil.WorkDay(this.logMsg.STORENO); //營業日
            this.ViewState["WorkDate"] = WorkDate.Trim();

            if (string.IsNullOrEmpty(StringUtil.CStr(this.ViewState["InventoryNo"])))  //不是由【查詢修改】點選過來
            {
                //判斷：
                //1. 營業日當天是否有資料
                //2. 是否為關帳日
                DataTable dt = INV11_PageHelper.GetStockChkM(this.logMsg.STORENO, WorkDate); //判斷當天是否已經盤點過了
                if (dt.Rows.Count > 0)  //資料表裡資料，已經盤點過了，按下【確定】後直接帶出資料
                {
                    DataRow dr = dt.Rows[0];
                    this.rbStkChkType.Enabled = false;
                    this.rbStkChkType.SelectedValue = StringUtil.CStr(dr["STKCHK_TYPE"]);
                    this.lblStkchkNo.Text = StringUtil.CStr(dr["STKCHKNO"]);      //盤點單號
                    this.lblStkchkDate.Text = StringUtil.CStr(dr["STKCHKDATE"]);  //盤點日期
                    this.lblStatus.Text = StringUtil.CStr(dr["STATUS"]);          //盤點狀態
                    this.lblStkchkUserNo.Text = StringUtil.CStr(dr["EMPNO"]) + " " + StringUtil.CStr(dr["EMPNAME"]);   //盤點人員
                    this.rbStkChkType.SelectedValue = StringUtil.CStr(dr["STKCHK_TYPE"]);
                    BindMasterData();
                    this.lblModiUser.Text = StringUtil.CStr(dr["MODI_USER"]) + " " + StringUtil.CStr(dr["MODI_NAME"]);   //更新人員
                    this.lblModiDTM.Text = INV11_PageHelper.GetModiDate(StringUtil.CStr(dr["STKCHK_M_ID"]));
                }
                else   //尚未盤點過
                {
                    string CutOffDate = INV11_PageHelper.GetCutOffDate(WorkDate.Substring(0, 7));
                    if (WorkDate == CutOffDate)  //營業日 等於 關帳日
                    {
                        this.rbStkChkType.Items[0].Enabled = false;  //重盤
                        this.rbStkChkType.Items[1].Enabled = false;  //全盤
                        this.rbStkChkType.SelectedValue = "3"; //關帳日盤點
                        this.rbStkChkType.Items[2].Selected = true;
                    }
                    else   //營業日 不等於 關帳日
                    {
                        this.rbStkChkType.Items[2].Enabled = false;  //關帳日盤點
                        this.rbStkChkType.SelectedValue = "1";
                    }
                    this.lblStkchkDate.Text = WorkDate;         //盤點日期
                    this.lblStkchkNo.Text = "";                 //盤點單號
                    this.lblStkchkUserNo.Text = logMsg.OPERATOR + " " + new Employee_Facade().GetEmpName(this.logMsg.OPERATOR);   //盤點人員

                }
                btnOK.Enabled = true;
            }
            else  //由【查詢修改】點選過來
            {
                this.lblStkchkNo.Text = qDno;   //盤點單號
                DataTable dt = new INV11_Facade().Query_StockChkM_ByKey(this.lblStkchkNo.Text); //判斷當天是否已經盤點過了
                if (dt.Rows.Count > 0)
                {
                    DataRow dr = dt.Rows[0];
                    this.rbStkChkType.Enabled = false;
                    this.rbStkChkType.SelectedValue = "2";
                    this.rbStkChkType.SelectedValue = StringUtil.CStr(dr["STKCHK_TYPE"]);
                    this.lblStkchkUserNo.Text = StringUtil.CStr(dr["EMPNO"]) + " " + StringUtil.CStr(dr["EMPNAME"]);   //盤點人員
                    this.lblStkchkNo.Text = StringUtil.CStr(dr["STKCHKNO"]);      //盤點單號
                    this.lblStkchkDate.Text = StringUtil.CStr(dr["STKCHKDATE"]);  //盤點日期
                    this.lblStatus.Text = StringUtil.CStr(dr["STATUS"]);          //盤點狀態
                    this.lblModiUser.Text = StringUtil.CStr(dr["MODI_USER"]) + " " + StringUtil.CStr(dr["MODI_NAME"]);   //更新人員
                    BindMasterData();
                    this.lblModiDTM.Text = INV11_PageHelper.GetModiDate(StringUtil.CStr(dr["STKCHK_M_ID"]));
                    if (StringUtil.CStr(dr["STKCHKDATE"]) != System.DateTime.Now.ToString("yyyy/MM/dd"))
                    {//非營業日當天不可修改資料
                        btnOK.Enabled = false;
                        divBTN.Visible = false;
                    }
                    else
                    {
                        btnOK.Enabled = true;
                        divBTN.Visible = true;
                    }
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this, GetType(), "查無資料", "alert('查無此店號請確認!');", true);
                    this.rbStkChkType.Enabled = false;
                    this.rbStkChkType.SelectedValue = "";
                    this.lblStkchkUserNo.Text = "";
                    this.lblStkchkNo.Text = "";
                    this.lblStkchkDate.Text = "";
                }
            }

            this.lblStoreNo.Text = this.logMsg.STORENO;                //門市代號
            this.lblStoreName.Text = new Store_Facade().GetStoreName(this.logMsg.STORENO);   //門市名稱
            if (string.IsNullOrEmpty(this.lblModiDTM.Text))
                this.lblModiDTM.Text = System.DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");  //更新日期
            if (string.IsNullOrEmpty(this.lblModiUser.Text) || string.IsNullOrEmpty(this.lblStkchkNo.Text))
                this.lblModiUser.Text = logMsg.OPERATOR + " " + new Employee_Facade().GetEmpName(this.logMsg.OPERATOR);     //更新人員

            PrintName.Value = System.Configuration.ConfigurationManager.AppSettings["PDFPrinterName"].ToString();//web.config中設定
            ScriptManager.RegisterStartupScript(this, this.GetType(), "PrintInvoice", " Call_DetectPrinterName();", true);
        }

        if (string.IsNullOrEmpty(this.lblStkchkNo.Text) && !(this.rbStkChkType.Items[2].Selected))
        {
            this.rbStkChkType.Items[0].Enabled = true;  //重盤
            this.rbStkChkType.Items[1].Enabled = true;  //全盤
            this.rbStkChkType.Items[2].Enabled = false; //關帳日盤點
        }

        //儲存當頁資料
        SaveCurrentPage();
    }

    protected void BindMasterData()
    {
        gvMaster.Visible = true;
        DataTable dt = new INV11_Facade().Query_StockChkD(this.lblStkchkNo.Text);
        gvMaster.DataSource = dt;
        gvMaster.DataBind();

        divBTN.Visible = true;
        if (dt.Rows.Count == 0)
        {
            btnSave.Enabled = false;
        }
        else
        {
            btnSave.Enabled = true;
        }
    }

    #region Button 觸發事件

    protected void btnOK_Click(object sender, EventArgs e)
    {
        bool bCanCreateYN = true;
        string WorkDate = OracleDBUtil.WorkDay(this.logMsg.STORENO); //營業日
        DataTable dt = INV11_PageHelper.GetStockChkM(this.logMsg.STORENO, WorkDate); //判斷當天是否已經盤點過了
        if (dt.Rows.Count > 0)  //資料表裡資料，已經盤點過了，按下【確定】後直接帶出資料
        { 
            bCanCreateYN = false; 
        }

        string strStkChkNo = this.lblStkchkNo.Text;
        if (string.IsNullOrEmpty(this.lblStkchkNo.Text))
        {
            if (bCanCreateYN)
            {
                strStkChkNo = CreateSTOCKCHK();     //建立新的盤點單
                this.rbStkChkType.Enabled = false;
                this.lblStatus.Text = "盤點中";     //盤點狀態
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this, GetType(), "盤點單", "alert('門市盤點單一天只能建立一張，請確認!');", true);
                setTitle();
                return;
            }
        }

        if (rbActivityType.SelectedIndex == 0)  // 列印盤點單
        {
            PrintInventory(strStkChkNo);
            if (string.IsNullOrEmpty(this.lblStkchkNo.Text))
            {
                this.lblStkchkNo.Text = strStkChkNo;
            }
        }
        else                                    //盤點輸入
        {
            this.lblStkchkNo.Text = strStkChkNo;
            BindMasterData();
            this.gvMaster.PageIndex = 0;
            this.rbStkChkType.Enabled = false;
        }

    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        INV11_Facade facade = new INV11_Facade();

        try
        {
            if (this.gvMaster.VisibleRowCount > 0)
            {
                //**2011/04/11 Tina：【儲存】時不判斷資料都要輸入盤量。
                //int intNullData = 0;
                string STKCHKNO = StringUtil.CStr(this.lblStkchkNo.Text);
                //intNullData = facade.GetDetailNullDataCount(STKCHKNO);
                ////是否每一筆資料都已經盤點過
                //if (intNullData > 0)
                //{
                //    ScriptManager.RegisterClientScriptBlock(this, GetType(), "AlertMessage", "alert('尚有資料未輸入盤點量，請確認!');", true);
                //    return;
                //}
                //else
                //{
                    //儲存當頁資料
                    SaveCurrentPage();

                    //更新資料庫狀態
                    facade.UPDATE_StockChkM(STKCHKNO, this.logMsg.OPERATOR);

                    setTitle();
                    //彈跳視窗
                    ScriptManager.RegisterClientScriptBlock(this, GetType(), "AlertMessage", "alert('盤點中結果已儲存，可繼續修改門市盤點量');", true);
                //}
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(this, typeof(string), "AlertMessage", "alert('存檔失敗! " + ex.Message.Replace("'", "-").Replace("\"", " ").Replace("\r", "").Replace("\n", "") + "');", true);
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        gvMaster.Visible = true;
        setTitle();
    }

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(this.lblStkchkNo.Text))
        {
            INV11_Facade facade = new INV11_Facade();
            facade.Delete_StockCHK(this.lblStkchkNo.Text);
            this.lblStkchkNo.Text = "";


            this.gvMaster.Visible = false;
            this.rbStkChkType.Enabled = true;
            string WorkDate = OracleDBUtil.WorkDay(this.logMsg.STORENO); //營業日
            string CutOffDate = INV11_PageHelper.GetCutOffDate(WorkDate.Substring(0, 7));
            if (WorkDate == CutOffDate)  //營業日 等於 關帳日
            {
                this.rbStkChkType.Items[0].Enabled = false;  //重盤
                this.rbStkChkType.Items[1].Enabled = false;  //全盤
                this.rbStkChkType.Items[2].Enabled = false; //關帳日盤點
            }
            else
            {
                this.rbStkChkType.Items[0].Enabled = true;  //重盤
                this.rbStkChkType.Items[1].Enabled = true;  //全盤
                this.rbStkChkType.Items[2].Enabled = false; //關帳日盤點
            }
            divBTN.Visible = false;
        }
        setTitle();
    }

    #endregion

    #region gvMaster 觸發事件

    protected void gvMaster_HtmlRowCreated(object sender, ASPxGridViewTableRowEventArgs e)
    {
        if (e.RowType == GridViewRowType.Data)
        {
            ASPxTextBox txtStkchkQty = gvMaster.FindRowCellTemplateControl(e.VisibleIndex, (GridViewDataColumn)gvMaster.Columns["STKCHKQTY"], "txtStkchkQty") as ASPxTextBox;
            string WorkDate = StringUtil.CStr(this.ViewState["WorkDate"]).Trim(); //營業日

            if (WorkDate != this.lblStkchkDate.Text)  //盤點日 == 營業日 才可修改門市盤點量
            {
                txtStkchkQty.ReadOnly = true;
                txtStkchkQty.ClientEnabled = false;
            }
        }
    }

    protected void gvMaster_PageIndexChanged(object sender, EventArgs e)
    {
        BindMasterData();

        int CurPageFirstIndex = this.gvMaster.PageIndex * gvMaster.SettingsPager.PageSize;        //當頁第一筆Index
        ASPxTextBox txtStkchkQty = gvMaster.FindRowCellTemplateControl(CurPageFirstIndex, (GridViewDataColumn)gvMaster.Columns["STKCHKQTY"], "txtStkchkQty") as ASPxTextBox;
        txtStkchkQty.Focus();
    }

    protected void gvMaster_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
    {
        int Index = gvMaster.EditingRowVisibleIndex;

        try
        {
            INV11_StockCHK_DTO INV11_DTO = new INV11_StockCHK_DTO();
            INV11_StockCHK_DTO.STOCKCHK_DDataTable dtD = INV11_DTO.STOCKCHK_D;
            INV11_Facade facade = new INV11_Facade();
            string STKCHK_D_ID = "";
            string STKCHK_M_ID = "";

            INV11_StockCHK_DTO.STOCKCHK_DRow drD = dtD.NewSTOCKCHK_DRow();
            STKCHK_D_ID = StringUtil.CStr(gvMaster.GetRowValues(Index, gvMaster.KeyFieldName));
            DataRow dr = INV11_PageHelper.Query_StockChkD_ByKey(STKCHK_D_ID).Rows[0];

            long STKCHKQTY = 0;
            string txtStkchkQty = StringUtil.CStr(e.NewValues["STKCHKQTY"]).Trim();
            //((ASPxTextBox)gvMaster.FindEditRowCellTemplateControl((GridViewDataColumn)gvMaster.Columns["STKCHKQTY"], "txtStkchkQty")).Text;
            if (!string.IsNullOrEmpty(txtStkchkQty))
            {
                STKCHKQTY = Convert.ToInt64(txtStkchkQty);
            }


            drD.STKQTY = Convert.ToInt64(StringUtil.CStr(dr["STKQTY"]));  //庫存量
            drD.STKCHK_D_ID = StringUtil.CStr(dr["STKCHK_D_ID"]);         //門市盤點單明細ID_UUID
            drD.STKCHKQTY = STKCHKQTY;

            long DIFF_STKQTY = Convert.ToInt64(drD.STKQTY) - STKCHKQTY;
            drD.DIFF_STKQTY = DIFF_STKQTY;

            //判斷若有變更門市盤點量，則要變更盤點人員，否則保持原來的盤點人員
            if (StringUtil.CStr(STKCHKQTY) != StringUtil.CStr(dr["STKCHKQTY"]))
            {
                drD.CHK_PERSON = this.logMsg.OPERATOR;
            }
            else
            {
                drD.CHK_PERSON = StringUtil.CStr(dr["CHK_PERSON"]);
            }
            drD.PRODNO = StringUtil.CStr(dr["PRODNO"]);
            drD.MODI_USER = this.logMsg.OPERATOR;
            drD.MODI_DTM = System.DateTime.Now;
            drD.STKCHK_M_ID = StringUtil.CStr(dr["STKCHK_M_ID"]);
            STKCHK_M_ID = StringUtil.CStr(dr["STKCHK_M_ID"]);
            drD.INV_ONHAND_CURRENT_ID = StringUtil.CStr(dr["INV_ONHAND_CURRENT_ID"]);

            dtD.Rows.Add(drD);
            INV11_DTO.AcceptChanges();

            if (dtD.Rows.Count > 0)
            {
                //更新資料庫
                facade.UpdateOne_StockCHK(INV11_DTO);

                ((ASPxGridView)sender).CancelEdit();
                e.Cancel = true;

                BindMasterData();

                //彈跳視窗
                //ScriptManager.RegisterClientScriptBlock(this, GetType(), "AlertMessage", "alert('盤點中結果已儲存，可繼續修改門市盤點量');", true);

                setTitle();
                int NextIndex = Index + 1;
                int NextPage = gvMaster.PageIndex + 1;
                //**2011/03/24 Tina：
                //  1. 當頁還有下一筆，則下一筆資料直接進入編輯模式
                //  2. 已經是當頁的最後一筆，就跳到下一頁的第一筆進行編輯
                if (NextIndex % gvMaster.SettingsPager.PageSize > 0)
                {
                    gvMaster.StartEdit(NextIndex);
                }
                else{
                    if (gvMaster.PageCount >= NextPage)
                    {
                        gvMaster.PageIndex = NextPage;
                        gvMaster.StartEdit(NextIndex);
                    }
                }

            }
        }
        catch //(Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(this, typeof(string), "InsertAllDBError", "eSave('存檔失敗!');", true); //消除[存檔中訊息]
        }
        finally
        {
            ScriptManager.RegisterClientScriptBlock(this, typeof(string), "InsertAllDBError", "eSave('');", true); //消除[存檔中訊息]
        }
    }

    protected void gvMaster_StartRowEditing(object sender, DevExpress.Web.Data.ASPxStartRowEditingEventArgs e)
    {
        ASPxTextBox txtStkchkQty = gvMaster.FindEditRowCellTemplateControl((GridViewDataColumn)gvMaster.Columns["STKCHKQTY"], "txtStkchkQty") as ASPxTextBox;
        ASPxLabel lbStkQty = gvMaster.FindEditRowCellTemplateControl((GridViewDataColumn)gvMaster.Columns["STKQTY"], "lbStkQty") as ASPxLabel;
        string WorkDate = StringUtil.CStr(this.ViewState["WorkDate"]).Trim(); //營業日

        if (WorkDate != this.lblStkchkDate.Text)  //盤點日 == 營業日 才可修改門市盤點量
        {
            txtStkchkQty.ReadOnly = true;
            txtStkchkQty.ClientEnabled = false;
        }

        if (txtStkchkQty.Text == "")
        {
            txtStkchkQty.Text = lbStkQty.Text;
        }

        txtStkchkQty.Focus();

    }

    #endregion

    /// <summary>
    /// 列印盤點單
    /// </summary>
    private void PrintInventory(string strStkChkNo)
    {

        DataTable dtheader = new DataTable();
        dtheader.Columns.Add("header1", typeof(string));
        DataRow NewRowHeader = dtheader.NewRow();
        NewRowHeader["header1"] = "盤點類型： " + this.rbStkChkType.SelectedItem.Text;
        dtheader.Rows.Add(NewRowHeader);
        NewRowHeader = dtheader.NewRow();
        NewRowHeader["header1"] = "盤點日期： " + this.lblStkchkDate.Text;
        dtheader.Rows.Add(NewRowHeader);

        DataTable dtfooter = new DataTable();
        dtfooter.Columns.Add("footer1", typeof(string));
        DataRow NewRowFooter = dtfooter.NewRow();
        NewRowFooter["footer1"] = "盤點人員： " + this.lblStkchkUserNo.Text;
        dtfooter.Rows.Add(NewRowFooter);

        //**2011/04/29 Tina：列印空白盤點單，多一個「維修倉」的欄位
        DataTable dt = new INV11_Facade().GetExportData(strStkChkNo);
        string filename = new PriINV11().generateReceipt("空白盤點單", dtheader, dt, dtfooter, PrintName.Value);

        ProcessRequest(filename);
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

    /// <summary>
    /// 建立新的盤點單
    /// </summary>
    /// <returns>盤點單號</returns>
    private string CreateSTOCKCHK()
    {
        INV11_StockCHK_DTO INV11_DTO = new INV11_StockCHK_DTO();
        INV11_StockCHK_DTO.STOCKCHK_MDataTable dtM = INV11_DTO.STOCKCHK_M;
        INV11_StockCHK_DTO.STOCKCHK_MRow drM = dtM.NewSTOCKCHK_MRow();

        string strStkChkNo = SerialNo.GenNo("SC{0}-"); //"SC{0}-1007002";
        strStkChkNo = string.Format(strStkChkNo, this.logMsg.STORENO);

        drM.STKCHKNO = strStkChkNo;                                     //盤點單號
        drM.STKCHK_M_ID = GuidNo.getUUID();                             //門市盤點單ID_UUID
        drM.STKCHKDATE = OracleDBUtil.WorkDay(this.logMsg.STORENO);     //盤點日期(營業日)
        drM.STKCHK_USERNO = this.logMsg.OPERATOR;                       //盤點人員
        drM.STKCHK_TYPE = StringUtil.CStr(this.rbStkChkType.SelectedValue);   //盤點類型 1:重盤 2:全盤 3.CLOSE DAY
        drM.STORE_NO = this.logMsg.STORENO;                             //門市代碼
        drM.CREATE_USER = this.logMsg.OPERATOR;                         //建立人員
        drM.CREATE_DTM = System.DateTime.Now;                           //建立時間
        drM.MODI_USER = this.logMsg.OPERATOR;                           //異動人員
        drM.MODI_DTM = System.DateTime.Now;                             //異動時間
        drM.STATUS = "00";                                              //盤點狀態 => 00:盤點中, 10:已盤點

        dtM.Rows.Add(drM);
        INV11_DTO.AcceptChanges();

        INV11_Facade facade = new INV11_Facade();

        //更新資料庫
        facade.AddNewOne_StockCHK(INV11_DTO);

        return strStkChkNo;
    }

    /// <summary>
    /// 如果已有門市盤點單將會撈出單子。
    /// </summary>
    public void setTitle()
    {
        string WorkDate = OracleDBUtil.WorkDay(this.logMsg.STORENO); //營業日
        DataTable dt = INV11_PageHelper.GetStockChkM(this.logMsg.STORENO, WorkDate); //判斷當天是否已經盤點過了
        if (dt.Rows.Count > 0)  //資料表裡資料，已經盤點過了，按下【確定】後直接帶出資料
        {
            DataRow dr = dt.Rows[0];
            this.rbStkChkType.Enabled = false;
            this.rbStkChkType.SelectedValue = StringUtil.CStr(dr["STKCHK_TYPE"]);
            this.lblStkchkNo.Text = StringUtil.CStr(dr["STKCHKNO"]);      //盤點單號
            this.lblStkchkDate.Text = StringUtil.CStr(dr["STKCHKDATE"]);  //盤點日期
            this.lblStatus.Text = StringUtil.CStr(dr["STATUS"]);          //盤點狀態
            this.lblStkchkUserNo.Text = StringUtil.CStr(dr["EMPNO"]) + " " + StringUtil.CStr(dr["EMPNAME"]);   //盤點人員
            BindMasterData();
            this.lblModiUser.Text = StringUtil.CStr(dr["MODI_USER"]) + " " + StringUtil.CStr(dr["MODI_NAME"]);   //更新人員
            this.lblModiDTM.Text = INV11_PageHelper.GetModiDate(StringUtil.CStr(dr["STKCHK_M_ID"]));
        }
    }

    /// <summary>
    /// 儲存當頁資料
    /// </summary>
    public void SaveCurrentPage()
    {
        string WorkDate = StringUtil.CStr(this.ViewState["WorkDate"]).Trim(); //營業日
        //1. GridView可見
        //2. 有資料
        //3. 盤點日 == 營業日 才可修改門市盤點量
        if (this.gvMaster.Visible && this.gvMaster.VisibleRowCount > 0 && WorkDate == this.lblStkchkDate.Text)
        {
            int CurPageFirstIndex = this.gvMaster.PageIndex * gvMaster.SettingsPager.PageSize;        //當頁第一筆Index
            int PatgeLastIndex = (this.gvMaster.PageIndex + 1) * gvMaster.SettingsPager.PageSize - 1;
            int LastIndex = this.gvMaster.VisibleRowCount - 1;  //最後一筆資料Index
            int CurPatgeLastIndex = PatgeLastIndex <= LastIndex ? PatgeLastIndex : LastIndex;         //當頁最後一筆Index

            INV11_StockCHK_DTO INV11_DTO = new INV11_StockCHK_DTO();
            INV11_StockCHK_DTO.STOCKCHK_DDataTable dtD = INV11_DTO.STOCKCHK_D;
            INV11_Facade facade = new INV11_Facade();
            string STKCHK_D_ID = "";
            string STKCHK_M_ID = "";
            string strStkchkQty = "";
            ASPxTextBox txtStkchkQty = null;
            INV11_StockCHK_DTO.STOCKCHK_DRow drD = null;

            for (int i = CurPageFirstIndex; i <= CurPatgeLastIndex; i++)
            {
                drD = dtD.NewSTOCKCHK_DRow();
                txtStkchkQty = gvMaster.FindRowCellTemplateControl(i, (GridViewDataColumn)gvMaster.Columns["STKCHKQTY"], "txtStkchkQty") as ASPxTextBox;
                if (!txtStkchkQty.IsValid)
                {
                    //當頁只要某一筆數量驗證不通過，就不儲存當筆資料，直接跳入下一筆
                    continue;
                    ////當頁只要某一筆數量驗證不通過，就不儲存當頁資料
                    //break;
                }
                STKCHK_D_ID = StringUtil.CStr(gvMaster.GetRowValues(i, gvMaster.KeyFieldName));
                strStkchkQty = txtStkchkQty.Text.Trim();

                DataRow dr = INV11_PageHelper.Query_StockChkD_ByKey(STKCHK_D_ID).Rows[0];

                long STKCHKQTY = 0;
                if (!string.IsNullOrEmpty(strStkchkQty))
                {
                    STKCHKQTY = Convert.ToInt64(strStkchkQty);
                }

                drD.STKQTY = Convert.ToInt64(StringUtil.CStr(dr["STKQTY"]));  //庫存量
                drD.STKCHK_D_ID = StringUtil.CStr(dr["STKCHK_D_ID"]);         //門市盤點單明細ID_UUID
                if (!string.IsNullOrEmpty(strStkchkQty))
                {
                    drD.STKCHKQTY = STKCHKQTY;
                }
                else
                {
                    drD["STKCHKQTY"] = DBNull.Value;
                }

                long DIFF_STKQTY = Convert.ToInt64(drD.STKQTY) - STKCHKQTY;
                drD.DIFF_STKQTY = DIFF_STKQTY;

                //判斷若有變更門市盤點量，則要變更盤點人員，否則保持原來的盤點人員
                if (StringUtil.CStr(STKCHKQTY) != StringUtil.CStr(dr["STKCHKQTY"]))
                {
                    drD.CHK_PERSON = this.logMsg.OPERATOR;
                }
                else
                {
                    drD.CHK_PERSON = StringUtil.CStr(dr["CHK_PERSON"]);
                }
                drD.PRODNO = StringUtil.CStr(dr["PRODNO"]);
                drD.CREATE_USER = StringUtil.CStr(dr["CREATE_USER"]);
                drD.CREATE_DTM = Convert.ToDateTime(StringUtil.CStr(dr["CREATE_DTM"]));
                drD.MODI_USER = this.logMsg.OPERATOR;
                drD.MODI_DTM = System.DateTime.Now;
                drD.STKCHK_M_ID = StringUtil.CStr(dr["STKCHK_M_ID"]);
                STKCHK_M_ID = StringUtil.CStr(dr["STKCHK_M_ID"]);
                drD.INV_ONHAND_CURRENT_ID = StringUtil.CStr(dr["INV_ONHAND_CURRENT_ID"]);

                dtD.Rows.Add(drD);
                INV11_DTO.AcceptChanges();

            }

            if (dtD.Rows.Count > 0)
            {
                try
                {
                    //更新資料庫
                    facade.UpdateOne_StockCHK(INV11_DTO);
                }
                catch //(Exception ex)
                {
                    ScriptManager.RegisterClientScriptBlock(this, typeof(string), "InsertAllDBError", "eSave('存檔失敗!');", true); //消除[存檔中訊息]
                    return;
                }
                finally
                {
                    ScriptManager.RegisterClientScriptBlock(this, typeof(string), "InsertAllDBError", "eSave('');", true); //消除[存檔中訊息]
                }
            }
        }

    }
}
