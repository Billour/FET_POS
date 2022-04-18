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

public partial class VSS_CON_CON16 : BasePage
{

    protected void Page_Load(object sender, EventArgs e)
    {
        if (logMsg.ROLE_TYPE == System.Configuration.ConfigurationManager.AppSettings["DefaultRoleHQ"].ToString())
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
        getWorkDate = OracleDBUtil.WorkDay(this.logMsg.STORENO);//營業日

        string dno = Request.QueryString["InventoryNo"] == null ? "" : Request.QueryString["InventoryNo"].ToString().Trim();
        this.ViewState["InventoryNo"] = dno;
        
        if (!Page.IsPostBack && !Page.IsCallback)
        {
            gvMaster.Visible = false;
            divBTN.Visible = false;
            this.rbStkChkType.Items[0].Enabled = true;  //重盤
            this.rbStkChkType.Items[1].Enabled = true;  //全盤
            this.rbStkChkType.Items[2].Enabled = true; //關帳日盤點
            string WorkDate = getWorkDate;// //營業日

            if (string.IsNullOrEmpty(this.ViewState["InventoryNo"].ToString()))  //不是由【查詢修改】點選過來
            {
                //判斷：
                //1. 營業日當天是否有資料
                //2. 是否為關帳日
                DataTable dt = CON16_PageHelper.GetStockChkM(this.logMsg.STORENO, WorkDate); //判斷當天是否已經盤點過了
                if (dt.Rows.Count > 0)  //資料表裡資料，已經盤點過了，按下【確定】後直接帶出資料
                {
                    DataRow dr = dt.Rows[0];
                    this.rbStkChkType.Enabled = false;
                    this.rbStkChkType.SelectedValue = dr["STKCHK_TYPE"].ToString();
                    this.lblStkchkNo.Text = dr["STKCHKNO"].ToString();      //盤點單號
                    this.lblStkchkDate.Text = dr["STKCHKDATE"].ToString();  //盤點日期
                    this.lblStkchkUserNo.Text = dr["EMPNO"].ToString() + " " +  dr["EMPNAME"].ToString();   //盤點人員
                    this.rbStkChkType.SelectedValue = dr["STKCHK_TYPE"].ToString();
                    BindMasterData();
                    this.lblModiUser.Text = dr["MODI_USER"].ToString() + " " + dr["MODI_NAME"].ToString();   //更新人員
                    this.lblModiDTM.Text = CON16_PageHelper.GetModiDate(dr["CSM_STOCKCHKM_ID"].ToString());
                }
                else   //尚未盤點過
                {
                    string CutOffDate = CON16_PageHelper.GetCutOffDate(WorkDate.Substring(0, 7));
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
                this.lblStkchkNo.Text = dno;   //盤點單號
                DataTable dt = new CON16_Facade().Query_StockChkM_ByKey(this.lblStkchkNo.Text); //判斷當天是否已經盤點過了
                if (dt.Rows.Count > 0)
                {
                    DataRow dr = dt.Rows[0];
                    this.rbStkChkType.Enabled = false;
                    this.rbStkChkType.SelectedValue = "2";
                    this.rbStkChkType.SelectedValue = dr["STKCHK_TYPE"].ToString();
                    this.lblStkchkUserNo.Text = dr["EMPNO"].ToString() + " " + dr["EMPNAME"].ToString();   //盤點人員
                    this.lblStkchkNo.Text = dr["STKCHKNO"].ToString();      //盤點單號
                    this.lblStkchkDate.Text = dr["STKCHKDATE"].ToString();  //盤點日期
                    this.lblModiUser.Text = dr["MODI_USER"].ToString() + " " + dr["MODI_NAME"].ToString();   //更新人員
                    BindMasterData();
                    this.lblModiDTM.Text = CON16_PageHelper.GetModiDate(dr["CSM_STOCKCHKM_ID"].ToString());
                    if (dr["STKCHKDATE"].ToString() != System.DateTime.Now.ToString("yyyy/MM/dd"))
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
            if( string.IsNullOrEmpty(this.lblModiDTM.Text))
                this.lblModiDTM.Text = System.DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");  //更新日期
            if (string.IsNullOrEmpty(this.lblModiUser.Text) || string.IsNullOrEmpty(this.lblStkchkNo.Text))
                this.lblModiUser.Text = logMsg.OPERATOR + " " + new Employee_Facade().GetEmpName(this.logMsg.OPERATOR);     //更新人員
            

        }

        if (string.IsNullOrEmpty(this.lblStkchkNo.Text) && !(this.rbStkChkType.Items[2].Selected))
        {
            this.rbStkChkType.Items[0].Enabled = true;  //重盤
            this.rbStkChkType.Items[1].Enabled = true;  //全盤
            this.rbStkChkType.Items[2].Enabled = false; //關帳日盤點
        }

    }

    protected void BindMasterData()
    {
        gvMaster.Visible = true;
        DataTable dt = new CON16_Facade().Query_StockChkD(this.lblStkchkNo.Text);
        gvMaster.DataSource = dt;
        gvMaster.DataBind();

        divBTN.Visible = true;
        btnSave.Enabled = false;
        btnCancel.Enabled = false;
        btnDelete.Enabled = true;
    }


    #region Button 觸發事件

    protected void btnOK_Click(object sender, EventArgs e)
    {
        bool bCanCreateYN = true;
        string WorkDate = getWorkDate;////營業日
        DataTable dt = CON16_PageHelper.GetStockChkM(this.logMsg.STORENO, WorkDate); //判斷當天是否已經盤點過了
        if (dt.Rows.Count > 0)  //資料表裡資料，已經盤點過了，按下【確定】後直接帶出資料
        { bCanCreateYN = false; }

        string strStkChkNo = this.lblStkchkNo.Text;
        if (string.IsNullOrEmpty(this.lblStkchkNo.Text))
        {
            if (bCanCreateYN)
            {
                strStkChkNo = CreateSTOCKCHK();     //建立新的盤點單
                this.rbStkChkType.Enabled = false;
            }
            else {
                ScriptManager.RegisterClientScriptBlock(this, GetType(), "寄銷盤點單", "alert('寄銷商品盤點單一天只能建立一張，請確認!');", true);
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
            if (gvMaster.VisibleRowCount > 0)
            { 
             this.btnSave.Enabled = true;
            }
           
            this.rbStkChkType.Enabled = false;
            
        }

    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            #region 數量不符，請確認 的訊息
            if (this.gvMaster.VisibleRowCount > 0)
            {
                bool bAltYN = false;
                string txtChk = "";
                string txtQty = "";
                long lChk = 0;
                long lQty = 0;
                try
                {
                    for (int i = 0; i <= this.gvMaster.VisibleRowCount - 1; i++)
                    {
                        if (!bAltYN)
                        {
                            lChk = 0;
                            lQty = 0;
                            txtChk = ((ASPxTextBox)gvMaster.FindRowCellTemplateControl(i, (GridViewDataColumn)gvMaster.Columns["STKCHKQTY"], "txtStkchkQty")).Text;
                            txtQty = ((ASPxTextBox)gvMaster.FindRowCellTemplateControl(i, (GridViewDataColumn)gvMaster.Columns["STKQTY"], "txtStkQty")).Text;
                            if (!string.IsNullOrEmpty(txtChk))
                            {
                                lChk = Convert.ToInt64(txtChk);
                            }
                            if (!string.IsNullOrEmpty(txtQty))
                            {
                                lQty = Convert.ToInt64(txtQty);
                            }
                            if (lQty != lChk)
                            {
                                bAltYN = true;
                            }
                        }
                    }
                }
                catch //(Exception ex)
                { }
                if (bAltYN)
                    ScriptManager.RegisterClientScriptBlock(this, GetType(), "數量不符", "alert('數量不符，請確認!');", true);
            }
            #endregion
            
            CONS16_CSM_STOCKCHK_M CON16_DTO = new CONS16_CSM_STOCKCHK_M();
            CONS16_CSM_STOCKCHK_M.CSM_STOCKCHK_DDataTable dtD = CON16_DTO.CSM_STOCKCHK_D;
            CON16_Facade facade = new CON16_Facade();
            string STKCHK_D_ID = "";
            string STKCHK_M_ID = "";
            for (int i = 0; i <= this.gvMaster.VisibleRowCount - 1; i++)
            {
                CONS16_CSM_STOCKCHK_M.CSM_STOCKCHK_DRow drD = dtD.NewCSM_STOCKCHK_DRow();
                STKCHK_D_ID = gvMaster.GetRowValues(i, gvMaster.KeyFieldName).ToString();
                DataRow dr = INV11_PageHelper.Query_StockChkD_ByKey(STKCHK_D_ID).Rows[0];

                long STKCHKQTY = 0;
                string txtStkchkQty = ((ASPxTextBox)gvMaster.FindRowCellTemplateControl(i, (GridViewDataColumn)gvMaster.Columns["STKCHKQTY"], "txtStkchkQty")).Text;
                if (!string.IsNullOrEmpty(txtStkchkQty))
                {
                    STKCHKQTY = Convert.ToInt64(txtStkchkQty);
                }


                drD.STKQTY = Convert.ToInt64(dr["STKQTY"].ToString());  //庫存量
                drD.CSM_STOCKCHKD_ID = dr["CSM_STOCKCHKD_ID"].ToString();         //門市盤點單明細ID_UUID
                drD.STKCHKQTY = STKCHKQTY;

                long DIFF_STKQTY = Convert.ToInt64(drD.STKQTY) - STKCHKQTY;
                drD.DIFF_STKQTY = DIFF_STKQTY;

                //判斷若有變更門市盤點量，則要變更盤點人員，否則保持原來的盤點人員
                if (STKCHKQTY.ToString() != dr["STKCHKQTY"].ToString())
                {
                    drD.CHK_PERSON = this.logMsg.OPERATOR;
                }
                else
                {
                    drD.CHK_PERSON = dr["CHK_PERSON"].ToString();
                }
                drD.PRODNO = dr["PRODNO"].ToString();
                drD.MODI_USER = this.logMsg.OPERATOR;
                drD.MODI_DTM = System.DateTime.Now;
                drD.CSM_STOCKCHKM_ID = dr["CSM_STOCKCHKM_ID"].ToString();
                STKCHK_M_ID = dr["CSM_STOCKCHKM_ID"].ToString();
                drD.INV_ONHAND_CURRENT_ID = dr["INV_ONHAND_CURRENT_ID"].ToString();

                dtD.Rows.Add(drD);
                CON16_DTO.AcceptChanges();
            }
            
            if (dtD.Rows.Count > 0)
            {
                //更新資料庫
                facade.UpdateOne_StockCHK(CON16_DTO);
                facade.UPDATE_StockChkM(STKCHK_M_ID, this.logMsg.OPERATOR);
                BindMasterData();

                //彈跳視窗
                ScriptManager.RegisterClientScriptBlock(this, GetType(), "AlertMessage", "alert('盤點中結果已儲存，可繼續修改寄銷商品盤點量');", true);

                setTitle();
            }
        }
        catch //(Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(this, typeof(string), "InsertAllDBError", "eSave('存檔失敗!');", true); //消除[存檔中訊息]
        }
        finally {
            ScriptManager.RegisterClientScriptBlock(this, typeof(string), "InsertAllDBError", "eSave('');", true); //消除[存檔中訊息]
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
            CON16_Facade facade = new CON16_Facade();
            facade.Delete_StockCHK(this.lblStkchkNo.Text);
            this.lblStkchkNo.Text = "";


            this.gvMaster.Visible = false;
            this.rbStkChkType.Enabled = true;
            string WorkDate = getWorkDate; //營業日
            string CutOffDate = CON16_PageHelper.GetCutOffDate(WorkDate.Substring(0, 7));
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
            btnSave.Enabled = true;
            btnCancel.Enabled = true;
            btnDelete.Enabled = true;
        }
        setTitle();
    }

    #endregion

    #region gvMaster 觸發事件

    //gvMaster_HtmlRowPrepared執行動作與gvMaster_HtmlRowCreated一樣，所以註解gvMaster_HtmlRowPrepared的執行動作
    protected void gvMaster_HtmlRowPrepared(object sender, ASPxGridViewTableRowEventArgs e)
    {
        //if (e.RowType == GridViewRowType.Data)
        //{
        //    ASPxTextBox txtStkchkQty = gvMaster.FindChildControl<ASPxTextBox>("txtStkchkQty");
        //    string WorkDate = getWorkDate; //營業日

        //    if (WorkDate != this.lblStkchkDate.Text)  //盤點日 == 營業日 才可修改門市盤點量
        //    {
        //        txtStkchkQty.ReadOnly = true;
        //    }
        //    else
        //    {
        //        divBTN.Visible = true;
        //        btnSave.Enabled = true;
        //        btnCancel.Enabled = true;
        //        btnDelete.Enabled = true;
        //    }
        //}
    }

    protected void gvMaster_PageIndexChanged(object sender, EventArgs e)
    {
        BindMasterData();
    }

    protected void gvMaster_HtmlRowCreated(object sender, ASPxGridViewTableRowEventArgs e)
    {
        
        if (e.RowType == GridViewRowType.Data)
        {
            ASPxTextBox txtStkchkQty = gvMaster.FindRowCellTemplateControl(e.VisibleIndex, (GridViewDataColumn)gvMaster.Columns["STKCHKQTY"], "txtStkchkQty") as ASPxTextBox;

            string WorkDate = getWorkDate; //營業日

            if (WorkDate != this.lblStkchkDate.Text)  //盤點日 == 營業日 才可修改門市盤點量
            {
                txtStkchkQty.ReadOnly = true;
                txtStkchkQty.ClientEnabled = false;
            }
            else
            {
                divBTN.Visible = true;
                btnSave.Enabled = true;
                btnCancel.Enabled = true;
                btnDelete.Enabled = true;
            }
        }

    }

    protected void gvMaster_FocusedRowChanged(object sender, EventArgs e)
    {
        if (gvMaster.FocusedRowIndex < 0) { return; }

        //BindDetailData();
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
        NewRowHeader["header1"] = "盤點日期： "+ this.lblStkchkDate.Text;
        dtheader.Rows.Add(NewRowHeader);

        DataTable dtfooter = new DataTable();
        dtfooter.Columns.Add("footer1", typeof(string));
        DataRow NewRowFooter = dtfooter.NewRow();
        NewRowFooter["footer1"] = "盤點人員： " + this.lblStkchkUserNo.Text;
        dtfooter.Rows.Add(NewRowFooter);

        DataTable dt = new CON16_Facade().GetExportData(strStkChkNo);
        string filename = new Output().Print("INV11", "空白寄銷盤點單", dtheader, dt, dtfooter);
        
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
        CONS16_CSM_STOCKCHK_M CON16_DTO = new CONS16_CSM_STOCKCHK_M();
        CONS16_CSM_STOCKCHK_M.CSM_STOCKCHK_MDataTable dtM = CON16_DTO.CSM_STOCKCHK_M;
        CONS16_CSM_STOCKCHK_M.CSM_STOCKCHK_MRow drM = dtM.NewCSM_STOCKCHK_MRow();

        string strStkChkNo = SerialNo.GenNo("CS{0}"); //"CS21012011022305 ";
        strStkChkNo = string.Format(strStkChkNo, this.logMsg.STORENO);

        drM.STKCHKNO = strStkChkNo;                                     //盤點單號
        drM.CSM_STOCKCHKM_ID = GuidNo.getUUID();                             //門市盤點單ID_UUID
        drM.STKCHKDATE = getWorkDate;//盤點日期(營業日)
        drM.STKCHK_USERNO = this.logMsg.OPERATOR;                       //盤點人員
        drM.STKCHK_TYPE = this.rbStkChkType.SelectedValue.ToString();   //盤點類型 1:重盤 2:全盤 3.CLOSE DAY
        drM.STORE_NO = this.logMsg.STORENO;                             //門市代碼
        drM.CREATE_USER = this.logMsg.OPERATOR;                         //建立人員
        drM.CREATE_DTM = System.DateTime.Now;                         //建立時間
        drM.MODI_USER = this.logMsg.OPERATOR;                           //異動人員
        drM.MODI_DTM = System.DateTime.Now;                           //異動時間

        dtM.Rows.Add(drM);
        CON16_DTO.AcceptChanges();

        CON16_Facade facade = new CON16_Facade();

        //更新資料庫
        facade.AddNewOne_StockCHK(CON16_DTO);

        return strStkChkNo;
    }

    /// <summary>
    /// 如果已有寄銷商品盤點單將會撈出單子。
    /// </summary>
    public void setTitle()
    {
        string WorkDate = getWorkDate; //營業日
        DataTable dt = CON16_PageHelper.GetStockChkM(this.logMsg.STORENO, WorkDate); //判斷當天是否已經盤點過了
        if (dt.Rows.Count > 0)  //資料表裡資料，已經盤點過了，按下【確定】後直接帶出資料
        {
            DataRow dr = dt.Rows[0];
            this.rbStkChkType.Enabled = false;
            this.rbStkChkType.SelectedValue = dr["STKCHK_TYPE"].ToString();
            this.lblStkchkNo.Text = dr["STKCHKNO"].ToString();      //盤點單號
            this.lblStkchkDate.Text = dr["STKCHKDATE"].ToString();  //盤點日期
            this.lblStkchkUserNo.Text = dr["EMPNO"].ToString() + " " + dr["EMPNAME"].ToString();   //盤點人員
            BindMasterData();
            this.lblModiUser.Text = dr["MODI_USER"].ToString() + " " + dr["MODI_NAME"].ToString();   //更新人員
            this.lblModiDTM.Text = CON16_PageHelper.GetModiDate(dr["CSM_STOCKCHKM_ID"].ToString());
        }
    }

    //存營業日 共用方法 
    public string getWorkDate
    {
        get { return ViewState["WorkDate"].ToString();}
        set { ViewState["WorkDate"] = value; }
    }
       
    
}
