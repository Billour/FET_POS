using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using FET.POS.Model.Facade;
using FET.POS.Model.Facade.FacadeImpl;
using DevExpress.Web.ASPxGridView;
using DevExpress.Web.ASPxEditors;
using Advtek.Utility;

public partial class VSS_CONS_CON12 : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string dno = Request.QueryString["dno"] == null ? "" : Request.QueryString["dno"].ToString().Trim();
        this.Session["dno"] = dno;

        if (!IsPostBack)
        {
            if (this.Session["dno"].ToString() == "")
            {


                comRtnNo.ValueField = "CSM_RTNM_UUID";
                comRtnNo.TextField = "RTNNO";
                DataTable DT = new CON12_Facade().get_RTNNO(logMsg.STORENO);
                //DT.Rows[0][1] = "-請選擇-";
                //DT.AcceptChanges();
                comRtnNo.SelectedIndex = 0;
                comRtnNo.DataSource = DT;// Supplier_Facade.GetSupplierNo(true);
                comRtnNo.DataBind();

                this.lbStatus.Text = "0";
                this.Status1.Text = getStatus(lbStatus.Text);

                this.lbModifiedBy.Text = logMsg.OPERATOR + " " + new Employee_Facade().GetEmpName(logMsg.OPERATOR);
                this.lbModifiedDate.Text = System.DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");  //更新日期

                this.btnSave.ClientEnabled = false;
                this.btnPrint.ClientEnabled = false;

                // 繫結空的資料表，以顯示表頭欄位
                gvMaster.DataSource = GetEmptyDataTable();
                gvMaster.DataBind();
            }
            else
            {
                comRtnNo.ValueField = "CSM_RTNM_UUID";
                comRtnNo.TextField = "RTNNO";

                DataTable dt = new DataTable();
                dt.Columns.Add("CSM_RTNM_UUID");
                dt.Columns.Add("RTNNO");

                DataRow dr = dt.NewRow();
                dr["CSM_RTNM_UUID"] = new CON12_Facade().get_RTNMUUID(dno);
                dr["RTNNO"] = dno;
                dt.Rows.Add(dr);
                dt.AcceptChanges();

                comRtnNo.SelectedIndex = 0;
                comRtnNo.DataSource = dt;// Supplier_Facade.GetSupplierNo(true);
                comRtnNo.DataBind();
                comRtnNo.ClientEnabled = false;

                comRtnNo_SelectedIndexChanged(null, null);


            }
        }
        else
        {


            doChangeMaster();
            //DataTable dtGvMaster = Session["gvMaster"] as DataTable;
            //if (dtGvMaster != null)
            //{
            //    gvMaster.DataSource = dtGvMaster;
            //    gvMaster.DataBind();
            //}
        }
    }

    #region gvMaster 觸發事件

    protected void gvMaster_PageIndexChanged(object sender, EventArgs e)
    {

        gvMaster.DataSource = Session["gvMaster"];
        gvMaster.DataBind();
    }

    protected void gvMaster_HtmlRowPrepared(object sender, ASPxGridViewTableRowEventArgs e)
    {

        string STATUS = this.lbStatus.Text;
        if (e.RowType == GridViewRowType.Data)
        {
            ASPxTextBox _RTNQTY = (ASPxTextBox)gvMaster.FindRowCellTemplateControl(e.VisibleIndex, (GridViewDataColumn)gvMaster.Columns["RTNQTY"], "txtRTNQTY");

            if (STATUS == "50" || STATUS == "60")
                _RTNQTY.ClientEnabled = false;
            else
                _RTNQTY.ClientEnabled = true;


        }


    }

    #endregion

    #region 按鈕觸發事件
    protected void btnSave_Click(object sender, EventArgs e)
    {
        DataTable dtMaster = (DataTable)Session["gvMaster"];
        if (dtMaster.Rows.Count > 0)
        {
            string strRTNM_ID_UUID = comRtnNo.SelectedItem.Value.ToString();
            DataSet CSM_RTN = new DataSet();

            ////寄銷退倉設定主檔
            //CSM_RTN.Tables.Add("CSM_RTNM");
            //CSM_RTN.Tables["CSM_RTNM"].Columns.Add("CSM_RTNM_UUID", typeof(string)); //RTNM_UUID
            //CSM_RTN.Tables["CSM_RTNM"].Columns.Add("MODI_USER", typeof(string)); //異動人員
            //CSM_RTN.Tables["CSM_RTNM"].Columns.Add("MODI_DTM", typeof(DateTime)); //異動時間
            //CSM_RTN.Tables["CSM_RTNM"].Columns.Add("STATUS", typeof(string)); //狀態

            //DataRow CSM_RTNM_NewRow = CSM_RTN.Tables["CSM_RTNM"].NewRow();
            //CSM_RTNM_NewRow["CSM_RTNM_UUID"] = strRTNM_ID_UUID;
            //CSM_RTNM_NewRow["MODI_USER"] = this.logMsg.MODI_USER.ToString().Trim();
            //CSM_RTNM_NewRow["MODI_DTM"] = this.logMsg.MODI_DTM.ToString().Trim();
            //CSM_RTNM_NewRow["STATUS"] = "50";
            //CSM_RTN.Tables["CSM_RTNM"].Rows.Add(CSM_RTNM_NewRow);

            //寄銷退倉明細
            CSM_RTN.Tables.Add("CSM_RTND_UP");
            CSM_RTN.Tables["CSM_RTND_UP"].Columns.Add("CSM_RTNM_UUID", typeof(string)); //RTND_UP_UUID           
            CSM_RTN.Tables["CSM_RTND_UP"].Columns.Add("PRODNO", typeof(string)); //商品料號
            CSM_RTN.Tables["CSM_RTND_UP"].Columns.Add("STOCKQTY", typeof(int)); //庫存量
            CSM_RTN.Tables["CSM_RTND_UP"].Columns.Add("RTNQTY", typeof(int)); //退倉量
            CSM_RTN.Tables["CSM_RTND_UP"].Columns.Add("RTNDATE", typeof(string)); //退倉日期    
            CSM_RTN.Tables["CSM_RTND_UP"].Columns.Add("MODI_USER", typeof(string)); //異動人員
            CSM_RTN.Tables["CSM_RTND_UP"].Columns.Add("MODI_DTM", typeof(DateTime)); //異動時間   
            CSM_RTN.Tables["CSM_RTND_UP"].Columns.Add("STORE_NO", typeof(string)); //門市代碼         
            CSM_RTN.Tables["CSM_RTND_UP"].Columns.Add("STATUS", typeof(string)); //狀態
            for (int i = 0; i < dtMaster.Rows.Count; i++)
            {
                DataRow CSM_RTND_UP_NewRow = CSM_RTN.Tables["CSM_RTND_UP"].NewRow();
                CSM_RTND_UP_NewRow["CSM_RTNM_UUID"] = strRTNM_ID_UUID;
                CSM_RTND_UP_NewRow["PRODNO"] = dtMaster.Rows[i]["PRODNO"].ToString().Trim();
                CSM_RTND_UP_NewRow["STOCKQTY"] = dtMaster.Rows[i]["STOCKQTY"].ToString().Trim();
                CSM_RTND_UP_NewRow["RTNQTY"] = dtMaster.Rows[i]["RTNQTY"].ToString().Trim();
                CSM_RTND_UP_NewRow["RTNDATE"] = DateTime.Now.ToString("yyyyMMdd");
                CSM_RTND_UP_NewRow["MODI_USER"] = this.logMsg.MODI_USER.ToString().Trim();
                CSM_RTND_UP_NewRow["MODI_DTM"] = this.logMsg.MODI_DTM.ToString().Trim();
                CSM_RTND_UP_NewRow["STORE_NO"] = this.logMsg.STORENO.ToString().Trim();
                CSM_RTND_UP_NewRow["STATUS"] = "60";
                CSM_RTN.Tables["CSM_RTND_UP"].Rows.Add(CSM_RTND_UP_NewRow);
            }
            int intResult = new CON12_Facade().SaveOrderData(CSM_RTN);
            if (intResult == 1)
            {
                //退倉日期
                lblReturnDate.Text = DateTime.Now.ToString("yyyy/MM/dd");

                lbStatus.Text = "60";
                Status1.Text = getStatus(lbStatus.Text);
                ////更新日期,更新人員
                lbModifiedDate.Text = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");
                lbModifiedBy.Text = logMsg.MODI_USER + " " + new Employee_Facade().GetEmpName(logMsg.MODI_USER);

                this.btnSave.ClientEnabled = false;
                this.btnPrint.ClientEnabled = true;

                ScriptManager.RegisterClientScriptBlock(this, typeof(string), "Save", "alert('已存檔完成!!');", true); //[存檔訊息]
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this, typeof(string), "SaveError", "alert('存檔失敗!!');", true); //[存檔訊息]

            }
        }
    }
    #endregion

    protected void comRtnNo_SelectedIndexChanged(object sender, EventArgs e)
    {

        string strRtnNo;
        strRtnNo = comRtnNo.SelectedItem.Value.ToString();
        DataTable dtRtnm = new CON12_Facade().get_RtnmData(strRtnNo, logMsg.STORENO);

        if (strRtnNo != "SELECT")
        {
            if (dtRtnm.Rows.Count > 0)
            {
                string strReturnDate = "";
                strReturnDate = dtRtnm.Rows[0]["RTNDATE"].ToString();
                if (strReturnDate != "")
                    strReturnDate = strReturnDate.Substring(0, 4) + "/" + strReturnDate.Substring(4, 2) + "/" + strReturnDate.Substring(6, 2);
                else
                    strReturnDate = StringUtil.CStr(DateTime.Now.ToString("yyyy/MM/dd"));
                this.lblReturnDate.Text = strReturnDate;

                this.lbStatus.Text = dtRtnm.Rows[0]["STATUS"].ToString();
                this.Status1.Text = getStatus(lbStatus.Text);

                if (lbStatus.Text == "50" || lbStatus.Text == "60")
                {
                    btnSave.ClientEnabled = false;
                }
                else
                {
                    btnSave.ClientEnabled = true;

                }
               
                this.btnPrint.ClientEnabled = true;

                string strB_DATE = "";
                strB_DATE = dtRtnm.Rows[0]["B_DATE"].ToString();
                strB_DATE = strB_DATE.Substring(0, 4) + "/" + strB_DATE.Substring(4, 2) + "/" + strB_DATE.Substring(6, 2);
                this.lbRTNM_BDate.Text = strB_DATE;

                string strE_DATE = "";
                strE_DATE = dtRtnm.Rows[0]["E_DATE"].ToString();
                strE_DATE = strE_DATE.Substring(0, 4) + "/" + strE_DATE.Substring(4, 2) + "/" + strE_DATE.Substring(6, 2);
                this.lbRTNM_EDate.Text = strE_DATE;


                this.lbModifiedBy.Text = dtRtnm.Rows[0]["MODI_USER"].ToString() + " " + new Employee_Facade().GetEmpName(dtRtnm.Rows[0]["MODI_USER"].ToString());
                this.lbModifiedDate.Text = DateTime.Parse(dtRtnm.Rows[0]["MODI_DTM"].ToString()).ToString("yyyy/MM/dd hh:mm:ss");
                bindMasterData(dtRtnm.Rows[0]["CSM_RTNM_UUID"].ToString());

            }
        }
        else
        {
            this.lblReturnDate.Text = ""; 
            this.lbStatus.Text = "0";
            this.Status1.Text = getStatus(lbStatus.Text);
            this.lbRTNM_BDate.Text = "";
            this.lbRTNM_EDate.Text = "";
            this.lbModifiedBy.Text = logMsg.OPERATOR + " " + new Employee_Facade().GetEmpName(logMsg.OPERATOR);
            this.lbModifiedDate.Text = System.DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");  //更新日期
            this.btnSave.ClientEnabled = false;
            this.btnPrint.ClientEnabled = false;

            Session["gvMaster"] = GetEmptyDataTable();
            gvMaster.DataSource = (DataTable)Session["gvMaster"];
            gvMaster.DataBind();
        }


        //divButtons.Visible = true;
    }

    #region 列印觸發事件
    protected void btnPrint_Click(object sender, EventArgs e)
    {

        DataTable dtheader = new DataTable();
        dtheader.Columns.Add("header1", typeof(string));
        dtheader.Columns.Add("header2", typeof(string));
        //dtheader.Columns.Add("header3", typeof(string));
        DataRow NewRowHeader = dtheader.NewRow();
        NewRowHeader["header1"] = "";
        NewRowHeader["header2"] = "收貨單位：";
        dtheader.Rows.Add(NewRowHeader);

        NewRowHeader = dtheader.NewRow();
        NewRowHeader["header1"] = "退倉單代號：" + comRtnNo.SelectedItem.Text.Trim();
        NewRowHeader["header2"] = "列印日期：" + DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");
        dtheader.Rows.Add(NewRowHeader);

        NewRowHeader = dtheader.NewRow();
        NewRowHeader["header1"] = "退倉日期：" + lblReturnDate.Text;
        NewRowHeader["header2"] = "列印人員：" + logMsg.OPERATOR + " " + new Employee_Facade().GetEmpName(logMsg.OPERATOR);
        dtheader.Rows.Add(NewRowHeader);

        NewRowHeader = dtheader.NewRow();
        NewRowHeader["header1"] = "";
        NewRowHeader["header2"] = "頁  次：1";
        dtheader.Rows.Add(NewRowHeader);

        DataTable dtfooter = new DataTable();
        dtfooter.Columns.Add("footer1", typeof(string));
        dtfooter.Columns.Add("footer2", typeof(string));
        DataRow NewRowFooter = dtfooter.NewRow();
        NewRowFooter["footer1"] = " ";
        NewRowFooter["footer2"] = " ";
        dtfooter.Rows.Add(NewRowFooter);

        NewRowFooter = dtfooter.NewRow();
        NewRowFooter["footer1"] = "退倉門市：_________________";
        NewRowFooter["footer2"] = "退倉人員：_________________";
        dtfooter.Rows.Add(NewRowFooter);

        DataTable dt = new CON12_Facade().Query_PrintRtn(comRtnNo.SelectedItem.Value.ToString().Trim(),
            this.logMsg.STORENO);
        int iRTNQTY = 0;

        if (dt.Rows.Count > 0)
        {
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                iRTNQTY += int.Parse(dt.Rows[i]["退倉數量"].ToString());

            }
        }

        DataRow dr_NewRow = dt.NewRow();
        dr_NewRow["商品類別"] = "總計";
        dr_NewRow["退倉數量"] = iRTNQTY;

        dt.Rows.Add(dr_NewRow);



        string filename = new Output().Print("PDF", "寄銷商品退倉簽收單", dtheader, dt, dtfooter);

        // Response.Redirect(Utils.CreateTamperProofDownloadXlsURL("寄銷商品退倉簽收單.xls")); 
        ProcessRequest(filename);
    }
    private void ProcessRequest(string filename)
    {
        string filePath = (new FET.POS.Model.Facade.FacadeImpl.SAL01_Facade()).getUploadPath();
        if (!string.IsNullOrEmpty(filePath))
        {

            ScriptManager.RegisterClientScriptBlock(this,
                                                   this.GetType(),
                                                   "test",
                                                   "document.getElementById('" + fDownload.ClientID + "').src='" + Request.ApplicationPath + filePath + "/" + filename + "';",
                                                   true);

            //ScriptManager.RegisterClientScriptBlock(this,
            //                                        this.GetType(),
            //                                        "Print",
            //                                       "$('#" + fDownload.ClientID + "').attr({src: '" + Request.ApplicationPath + filePath + "/" + filename + "'});",
            //                                        true);           

        }
        else
        {
            ScriptManager.RegisterClientScriptBlock(this, typeof(string), "PrintInvoiceError", "alert('寄銷商品退倉簽收單列印失敗!!');", true);
        }
    }
    #endregion

    #region 涵式
    private void doChangeMaster()
    {
        if (gvMaster.VisibleRowCount > 0)
        {
            DataTable dtMaster = (DataTable)Session["gvMaster"];
            DataRow drMaster;
            for (int i = 0; i < gvMaster.VisibleRowCount; i++)
            {
                ASPxLabel PRODNO = ((ASPxLabel)gvMaster.FindRowCellTemplateControl(i, (GridViewDataColumn)gvMaster.Columns["PRODNO"], "txtPRODNO"));
                if (PRODNO != null)
                {
                    if (PRODNO.Text != "")
                    {
                        drMaster = dtMaster.Select("PRODNO='" + PRODNO.Text + "'")[0];
                        string txtRTNQTY = ((ASPxTextBox)gvMaster.FindRowCellTemplateControl(i, (GridViewDataColumn)gvMaster.Columns["RTNQTY"], "txtRTNQTY")).Text;
                        drMaster["RTNQTY"] = txtRTNQTY.ToString();

                    }
                }
            }
            dtMaster.AcceptChanges();
            Session["gvMaster"] = dtMaster;
        }
    }

    protected void bindMasterData(string strRtnmUUID)
    {
        DataTable dtGvMaster = new CON12_Facade().get_MasterData(strRtnmUUID, logMsg.STORENO);

        Session["gvMaster"] = dtGvMaster;
        gvMaster.DataSource = dtGvMaster;
        gvMaster.DataBind();
    }

    private DataTable GetEmptyDataTable()
    {

        DataTable dtMaster = new DataTable();
        dtMaster.Columns.Clear();
        dtMaster.Columns.Add("SEQNO", typeof(string));
        dtMaster.Columns.Add("PRODNO", typeof(string));
        dtMaster.Columns.Add("PRODNAME", typeof(string));
        dtMaster.Columns.Add("SUPPNO", typeof(string));
        dtMaster.Columns.Add("SUPPNAME", typeof(string));
        dtMaster.Columns.Add("STOCKQTY", typeof(int));
        dtMaster.Columns.Add("RTNQTY", typeof(int));
        return dtMaster;
    }

    protected string getStatus(string StatusNo)
    {
        string strStatus = "";
        switch (StatusNo)
        {
            case "00":
                strStatus = "未存檔";
                break;
            case "10":
                strStatus = "已存檔";
                break;
            case "50":
                strStatus = "已傳輸";
                break;
            case "60":
                strStatus = "已完成";
                break;
            default:
                strStatus = "未存檔";
                break;

        }
        return strStatus;
    }

    #endregion
}
