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
using System.Web.Configuration;
using System.Collections.Specialized;


public partial class VSS_INV_INV18_1 : BasePage
{
    private string msgStr = "";
    //調整單號
    private string _ADJNO = "";

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
                    if (key == "dno")
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
        if (logMsg.ROLE_TYPE != WebConfigurationManager.AppSettings["DefaultRoleHQ"])
        {
            ScriptManager.RegisterClientScriptBlock(this, GetType(), "AlertMessage", "alert('非總部人員無法使用此功能!!');", true);
            txtSTORE_NO.Enabled = false;
            Button3.Enabled = false;
            btnSave.Enabled = false;
            btnCancel.Enabled = false;
            txtRemark.Enabled = false;
            txtADJDATE.Enabled = false;
            txtADJDATE.ControlStyle.BackColor = System.Drawing.Color.LightGray;
            gvMaster.Enabled = false;
            return;
        }

        this.ViewState["dno"] = qDno;
        if (StringUtil.CStr(Session["DOIMPOT"]) == "true")  //從匯入頁面過來；匯入成功後，先把頁面轉到【查詢修改】的頁面
        {
            Response.Redirect("~/VSS/INV/INV18_1/INV18.aspx", true);
        }

        if (!IsPostBack && !Page.IsCallback)
        {
            Session["gvMaster"] = null;
            if (StringUtil.CStr(this.ViewState["dno"]) == "")  //直接進入庫存調整作業的頁面
            {
                string strSTNO = SerialNo.GenNo("ST{0}-"); //"ST{0}-100815001";            
                strSTNO = string.Format(strSTNO, logMsg.STORENO); //{0} 帶入登入者的門市編號            
                this.hdSTNO.Value = strSTNO;
                //取得空的資料表            
                BindEmptyMasterData();
                Session["gvMaster"] = gvMaster.DataSource;
                Button_Enabled(false);
                this.lblSTATUS.Text = "00 未存檔".Substring(3);
                this.txtADJDATE.Text = DateTime.Now.ToString("yyyy/MM/dd");
                this.lblMOUSER.Text = logMsg.OPERATOR + " " + new Employee_Facade().GetEmpName(logMsg.MODI_USER);
                lblMODTM.Text = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");
            }
            else //從查詢修改頁面過來
            {
                lblADJNO.Text = qDno;
                INV18_1_Facade _INV18_1_Facade = new INV18_1_Facade();
                //重新bind資料-Master
                ViewState["GetADJM"] = _INV18_1_Facade.GetADJM(lblADJNO.Text);
                doSelectADJM();
                //重新bind資料-Detail
                ViewState["GetADJD"] = _INV18_1_Facade.GetADJD(lblADJNO.Text);
                doSelectADJD();
                //50-以傳輸

                if (this.lblSTATUS.Text.Trim().Contains("已存檔"))
                {
                    this.lblSTATUS.Text = "已存檔";
                    Button_Enabled(false);
                    gvMaster.FindChildControl<ASPxButton>("btnNew").ClientEnabled = false;
                    gvMaster.FindChildControl<ASPxButton>("btnDelete").ClientEnabled = false;
                    this.txtADJDATE.Enabled = false;
                    this.txtSTORE_NO.Enabled = false;
                    this.txtRemark.Enabled = false;
                }
                if (this.lblSTATUS.Text.Trim().Contains("暫存"))
                {
                    this.lblSTATUS.Text = "暫存";
                    Button_Enabled(true);
                    this.txtADJDATE.Enabled = false;
                    this.txtSTORE_NO.Enabled = false;
                    this.txtRemark.Enabled = false;
                }

            }

        }
    }

    private void BindEmptyMasterData()
    {
        DataTable dt = new DataTable();
        if (Session["gvMaster"] == null)
        {
            dt.Columns.Add("項次", typeof(string));
            dt.Columns.Add("PRODNO", typeof(string));
            dt.Columns.Add("PRODNAME", typeof(string));
            dt.Columns.Add("INV_OnHandQty", typeof(Int64));
            dt.Columns.Add("ADJQTY", typeof(Int64));
            dt.Columns.Add("STOCKADJ_REASON_CODE", typeof(string));
            dt.Columns.Add("IMEI_QTY", typeof(string));
            dt.Columns.Add("IMEI_FLAG", typeof(string));
            dt.Columns.Add("IMEI", typeof(string));
            dt.Columns.Add("ADJREASON", typeof(string));
            dt.Columns.Add("STOCKADJD_ID", typeof(string));


            dt.PrimaryKey = new DataColumn[] { dt.Columns["STNO"], dt.Columns["SEQNO"] };
            gvMaster.DataSource = dt;

        }
        else
            gvMaster.DataSource = Session["gvMaster"];

        gvMaster.DataBind();

    }

    private void Button_Enabled(bool v_Enabled)
    {
        ASPxButton v_btnSave = (ASPxButton)btnSave;
        v_btnSave.ClientEnabled = v_Enabled;
        ASPxButton v_btnCancel = (ASPxButton)btnCancel;
        v_btnCancel.ClientEnabled = v_Enabled;
    }

    private void doSelectADJM()
    {
        DataTable dtAdjM = (DataTable)ViewState["GetADJM"];

        if (dtAdjM.Rows.Count > 0)
        {
            this.lblADJNO.Text = StringUtil.CStr(dtAdjM.Rows[0]["ADJNO"]);
            this.txtADJDATE.Text = StringUtil.CStr(dtAdjM.Rows[0]["ADJDATE"]);
            this.lblSTATUS.Text = StringUtil.CStr(dtAdjM.Rows[0]["STATUS"]);
            this.txtSTORE_NO.Text = StringUtil.CStr(dtAdjM.Rows[0]["STORE_NO"]);
            this.lblStoreName.Text = StringUtil.CStr(dtAdjM.Rows[0]["STORENAME"]);
            this.txtRemark.Text = StringUtil.CStr(dtAdjM.Rows[0]["REMARK"]);
            this.lblMODTM.Text = Convert.ToDateTime(dtAdjM.Rows[0]["MODI_DTM"]).ToString("yyyy/MM/dd hh:mm:ss");
            this.lblMOUSER.Text = StringUtil.CStr(dtAdjM.Rows[0]["MODI_USER"]) + " " + new Employee_Facade().GetEmpName(StringUtil.CStr(dtAdjM.Rows[0]["MODI_USER"]));
        }
    }

    private void doSelectADJD()
    {
        DataTable dtAdjD = (DataTable)ViewState["GetADJD"];

        if (dtAdjD.Rows.Count > 0)
        {
            DataTable dtResult = new DataTable();
            dtResult.Columns.Add("項次", typeof(string));
            dtResult.Columns.Add("PRODNO", typeof(string));
            dtResult.Columns.Add("PRODNAME", typeof(string));
            dtResult.Columns.Add("INV_OnHandQty", typeof(string));
            dtResult.Columns.Add("ADJQTY", typeof(string));
            dtResult.Columns.Add("STOCKADJ_REASON_CODE", typeof(string));
            dtResult.Columns.Add("ADJREASON", typeof(string));
            dtResult.Columns.Add("STOCKADJD_ID", typeof(string));
            dtResult.Columns.Add("IMGIMEI", typeof(string));
            dtResult.Columns.Add("IMEI", typeof(string));
            //dtResult.Columns.Add("IMEI1", typeof(string));
            dtResult.Columns.Add("IMEI_QTY", typeof(string));
            dtResult.Columns.Add("IMEI_FLAG", typeof(string));

            foreach (DataRow dr in dtAdjD.Rows)
            {
                DataRow NewRow = dtResult.NewRow();
                NewRow["項次"] = StringUtil.CStr(dr["項次"]);
                NewRow["PRODNO"] = StringUtil.CStr(dr["PRODNO"]);
                NewRow["PRODNAME"] = StringUtil.CStr(dr["PRODNAME"]);
                NewRow["INV_OnHandQty"] = StringUtil.CStr(dr["INV_OnHandQty"]);
                NewRow["ADJQTY"] = StringUtil.CStr(dr["ADJQTY"]);
                NewRow["STOCKADJ_REASON_CODE"] = StringUtil.CStr(dr["STOCKADJ_REASON_CODE"]);
                NewRow["ADJREASON"] = StringUtil.CStr(dr["ADJREASON"]);
                NewRow["STOCKADJD_ID"] = StringUtil.CStr(dr["STOCKADJD_ID"]);

                NewRow["IMGIMEI"] = StringUtil.CStr(dr["IMGIMEI"]);
                NewRow["IMEI"] = StringUtil.CStr(dr["IMEI"]);
                NewRow["IMEI_FLAG"] = StringUtil.CStr(dr["IMEI_FLAG"]);
                NewRow["IMEI_QTY"] = StringUtil.CStr(dr["IMEI_QTY"]);

                dtResult.Rows.Add(NewRow);

            }

            Session["gvMaster"] = dtResult;
            gvMaster.DataSource = dtResult;
            gvMaster.DataBind();
        }
    }

    private void BindMasterData()
    {
        gvMaster.DataSource = (DataTable)Session["gvMaster"];
        gvMaster.DataBind();
    }

    protected INV18_StockADJ_DTO.STOCKADJMDataTable doInsertADJM()
    {
        INV18_StockADJ_DTO.STOCKADJMDataTable dtADJM = null;
        INV18_StockADJ_DTO _INV18_StockADJ_DTO = new INV18_StockADJ_DTO();
        INV18_StockADJ_DTO.STOCKADJMRow drADJM;

        dtADJM = _INV18_StockADJ_DTO.Tables["STOCKADJM"] as INV18_StockADJ_DTO.STOCKADJMDataTable;
        drADJM = dtADJM.NewSTOCKADJMRow();

        //調整單號
        if (lblADJNO.Text.Trim() == "")
        { _ADJNO = "SA" + txtSTORE_NO.Text + SerialNo.GenNo("SA"); }
        else
        { _ADJNO = lblADJNO.Text.Trim(); }

        //異動的欄位
        drADJM["ADJNO"] = _ADJNO;
        drADJM["ADJDATE"] = this.txtADJDATE.Text;
        drADJM["ADJUSRNO"] = logMsg.MODI_USER;
        drADJM["REMARK"] = txtRemark.Text;
        drADJM["STORE_NO"] = txtSTORE_NO.Text;
        drADJM["STATUS"] = "20";
        drADJM["ADJLOC"] = Common_PageHelper.GetGoodLOCUUID();
        drADJM["MODI_USER"] = logMsg.OPERATOR;
        drADJM["MODI_DTM"] = Convert.ToDateTime(System.DateTime.Now);
        drADJM["CREATE_USER"] = drADJM["MODI_USER"];
        drADJM["CREATE_DTM"] = drADJM["MODI_DTM"];

        dtADJM.Rows.Add(drADJM);

        return dtADJM;
    }

    protected INV18_StockADJ_DTO.STOCKADJDDataTable doInsertADJD()
    {
        INV18_StockADJ_DTO.STOCKADJDDataTable dtADJD = null;

        DataTable dtProd = Session["gvMaster"] as DataTable;

        dtADJD = new INV18_StockADJ_DTO.STOCKADJDDataTable();
        INV18_StockADJ_DTO.STOCKADJDRow drADJD;

        if (dtProd.Rows.Count > 0)
        {
            foreach (DataRow dr in dtProd.Rows)
            {
                drADJD = dtADJD.NewSTOCKADJDRow();
                drADJD["ADJNO"] = _ADJNO;
                drADJD["ADJQTY"] = StringUtil.CStr(dr["ADJQTY"]);
                drADJD["ADJREASON"] = StringUtil.CStr(dr["ADJREASON"]);
                drADJD["PRODNO"] = StringUtil.CStr(dr["PRODNO"]);
                drADJD["STOCKADJ_REASON_CODE"] = StringUtil.CStr(dr["STOCKADJ_REASON_CODE"]);
                drADJD["STOCKADJD_ID"] = StringUtil.CStr(dr["STOCKADJD_ID"]);
                drADJD["MODI_USER"] = logMsg.OPERATOR;
                drADJD["MODI_DTM"] = Convert.ToDateTime(System.DateTime.Now);
                drADJD["CREATE_USER"] = drADJD["MODI_USER"];
                drADJD["CREATE_DTM"] = drADJD["MODI_DTM"];
                dtADJD.Rows.Add(drADJD);
            }
        }

        return dtADJD;
    }

    public bool isnumeric(string str)
    {
        char[] ch = new char[str.Length];
        ch = str.ToCharArray();
        for (int i = 0; i < ch.Length; i++)
        {
            if (ch[i] < 48 || ch[i] > 57)
                return false;
        }
        return true;
    }

    #region Button 觸發事件

    protected void btnNew_Click(object sender, EventArgs e)
    {
        gvMaster.Selection.UnselectAll();

        if (this.txtSTORE_NO.Text.Trim() == "")
        {
            ScriptManager.RegisterClientScriptBlock(this, typeof(string), "checkSaveData", "alert('【門市編號】不可為空白，請選擇門市編號!!');", true);
            return;
        }
        else
        {
            if (getStore(txtSTORE_NO.Text) != "")
            {
                this.txtSTORE_NO.Enabled = false;
                gvMaster.AddNewRow();
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this, typeof(string), "checkSaveData", "alert('此門市編號不存在，請重新輸入!!');", true);
                return;
            }
        }
    }

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        if (Session["gvMaster"] != null)
        {
            DataTable dt = Session["gvMaster"] as DataTable;

            List<object> keyValues = this.gvMaster.GetSelectedFieldValues(gvMaster.KeyFieldName);
            foreach (string skey in keyValues)
            {
                //**2011/02/17 Tina：註解起來，點擊【儲存】之後，才真正去刪除Table的資料
                //if (this.lblSTATUS.Text.Trim().Contains("暫存"))
                //{
                //    new INV18_1_Facade().Del_ADJD(skey);
                //}
                DataRow[] dr = dt.Select("STOCKADJD_ID='" + skey + "'");
                if (dr.Length > 0)
                {
                    dt.Rows.Remove(dr[0]);
                    dt.AcceptChanges();
                }
            }
            Session["gvMaster"] = dt;
            gvMaster.DataSource = Session["gvMaster"];
            BindMasterData();
        }
        DataTable dt1 = (DataTable)Session["gvMaster"];
        if (Session["gvMaster"] == null || dt1.Rows.Count <= 0)
        {
            this.txtSTORE_NO.Enabled = true;
            txtRemark.Enabled = true;
            txtADJDATE.Enabled = true;
            Button_Enabled(false);
        }
        else
        {
            this.txtSTORE_NO.Enabled = false;
            Button_Enabled(true);
        }
    }

    protected void btnClear_Click(object sender, EventArgs e)
    {
        if (this.lblSTATUS.Text.Trim().Contains("暫存"))
        {
            INV18_StockADJ_DTO.STOCKADJMDataTable dtADJM = doInsertADJM();
            INV18_StockADJ_DTO.STOCKADJDDataTable dtADJD = doInsertADJD();
            //刪除資料庫                    
            new INV18_1_Facade().DeleteOne_MethodSet(dtADJM, dtADJD);

            msgStr = "刪除完成!";

        }
        lblADJNO.Text = "";
        txtADJDATE.Text = DateTime.Now.ToString("yyyy/MM/dd");
        txtSTORE_NO.Text = "";
        lblStoreName.Text = "";
        txtRemark.Text = "";
        lblSTATUS.Text = "00 未存檔".Substring(3);
        lblMOUSER.Text = logMsg.MODI_USER + " " + new Employee_Facade().GetEmpName(logMsg.MODI_USER);
        lblMODTM.Text = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");
        Session["gvMaster"] = null;
        BindEmptyMasterData();
        Session["gvMaster"] = gvMaster.DataSource;
        Button_Enabled(true);
        this.txtADJDATE.Enabled = true;
        this.txtSTORE_NO.Enabled = true;
        this.txtRemark.Enabled = true;
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        string strResult = "";
        DataTable dt = Session["gvMaster"] as DataTable;
        if (Session["gvMaster"] == null || dt.Rows.Count <= 0)
        {
            ScriptManager.RegisterClientScriptBlock(this, typeof(string), "checkSaveData", "ShowMessage('無輸入資料無法儲存!!');", true);
            return;
        }

        INV18_1_Facade _INV18_1_Facade = new INV18_1_Facade();
        INV18_StockADJ_DTO.STOCKADJMDataTable dtADJM = doInsertADJM();
        INV18_StockADJ_DTO.STOCKADJDDataTable dtADJD = doInsertADJD();

        if (this.lblSTATUS.Text.Trim().Contains("未存檔"))
        {
            try
            {
                strResult = _INV18_1_Facade.SaveADJ(dtADJM, dtADJD, true, logMsg.MACHINE_ID);
            }
            catch (Exception ex)
            {
                string exstring = ex.Message;
                if (exstring.Substring(0, 3) == "999")
                {
                    exstring = exstring.Substring(4, exstring.Length - 4);
                }
                else
                {
                    exstring = ex.Message.Replace("'", "-").Replace("\"", " ").Replace("\r", "").Replace("\n", ""); ;
                }
                ScriptManager.RegisterClientScriptBlock(this, typeof(string), "checkSaveData", "ShowMessage('" + exstring + "');", true);
                return;
            }

        }
        if (this.lblSTATUS.Text.Trim().Contains("暫存"))
        {
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    if (int.Parse(StringUtil.CStr(dr["ADJQTY"])) < 0)
                    {
                        int _ADJQTY = -int.Parse(StringUtil.CStr(dr["ADJQTY"]));
                        if (_ADJQTY > int.Parse(StringUtil.CStr(dr["INV_OnHandQty"])))
                        {
                            ScriptManager.RegisterClientScriptBlock(this, typeof(string), "checkSaveData", "ShowMessage('庫存量不足!!');", true);
                            return;
                        }
                    }
                }
            }
            strResult = _INV18_1_Facade.SaveADJ(dtADJM, dtADJD, false, logMsg.MACHINE_ID);

        }
        if (!string.IsNullOrEmpty(strResult))
        {
            ScriptManager.RegisterClientScriptBlock(this, typeof(string), "SaveDataError", "ShowMessage('" + strResult + "!!');", true);
            return;
        }
        else
        {
            this.lblADJNO.Text = _ADJNO;
            this.lblSTATUS.Text = "20 已存檔".Substring(3);
            //更新日期,更新人員
            this.lblMODTM.Text = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");
            this.lblMOUSER.Text = logMsg.MODI_USER + " " + new Employee_Facade().GetEmpName(logMsg.MODI_USER);

            //異動庫存
            Button_Enabled(false);
            msgStr = "存檔完成!";
            ViewState["GetADJD"] = _INV18_1_Facade.GetADJD(lblADJNO.Text);
            doSelectADJD();
            gvMaster.DataSource = Session["gvMaster"];
            gvMaster.DataBind();
            //取得空的資料表
            ScriptManager.RegisterClientScriptBlock(this, typeof(string), "儲存", "ShowMessage('" + msgStr + "!');", true);
            gvMaster.FindChildControl<ASPxButton>("btnNew").ClientEnabled = false;
            gvMaster.FindChildControl<ASPxButton>("btnDelete").ClientEnabled = false;
        }

    }

    #endregion

    #region gvMaster 觸發事件

    protected void gvMaster_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
    {
        gvMaster.SettingsPager.PageSize = int.Parse(e.Parameters);
        gvMaster.DataBind();
    }

    protected void gvMaster_HtmlRowCreated(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewTableRowEventArgs e)
    {
        if (e.RowType == GridViewRowType.Data)
        {

            ASPxLabel lblIMEI = e.Row.FindChildControl<ASPxLabel>("lblIMEI");
            string strID = hdSTNO.Value + "|" + hdSEQNO.Value;

            string _UUID = StringUtil.CStr(e.GetValue("STOCKADJD_ID"));

            lblIMEI.Attributes["onmouseover"] = string.Format("show('{0}');", IMEIContent("STOCKADJ_D_IMEI", _UUID, ""));
            lblIMEI.Attributes["onmouseout"] = "hide();";

            int intC_IMEI = int.Parse(StringUtil.CStr(e.GetValue("ADJQTY")) == "" ? "0" : StringUtil.CStr(e.GetValue("ADJQTY")));
            int intS_IMEI = int.Parse(StringUtil.CStr(e.GetValue("IMEI_QTY")) == "" ? "0" : StringUtil.CStr(e.GetValue("IMEI_QTY")));
            ASPxImage imgIMEI = e.Row.FindChildControl<ASPxImage>("imgIMEI");

            if (intC_IMEI == 0)
            {
                HtmlGenericControl divIMEI = e.Row.FindChildControl<HtmlGenericControl>("divIMEI");
                divIMEI.Visible = false;
                imgIMEI.ClientVisible = false;
            }

            if (StringUtil.CStr(e.GetValue("IMEI_FLAG")) == "3" || StringUtil.CStr(e.GetValue("IMEI_FLAG")) == "4")
            {
                if (imgIMEI.ClientVisible)
                {
                    imgIMEI.ImageUrl = ((intC_IMEI < 0 ? -intC_IMEI : intC_IMEI) == intS_IMEI ? "~/Icon/check.png" : "~/Icon/non_complete.png");
                }
            }
            else
            {
                imgIMEI.ImageUrl = "~/Icon/check.png";
            }

            PopupControl lblShowIMEI = e.Row.FindChildControl<PopupControl>("lblShowIMEI");
            lblShowIMEI.Enabled = false;

        }

        if (e.RowType == GridViewRowType.InlineEdit)
        {
            string IMEIFlag = e.Row.FindChildControl<ASPxTextBox>("txtIMEIFlag").Text;
            string ProdNo = ((PopupControl)gvMaster.FindEditRowCellTemplateControl((GridViewDataColumn)gvMaster.Columns["PRODNO"], "txtProdNo")).Text;
            string TRANOUTQTY = e.Row.FindChildControl<ASPxTextBox>("txtAdjuestmentQuantity").Text;
            string IMEI_QTY = e.Row.FindChildControl<ASPxTextBox>("lblIMEI_QTY").Text;

            //設定顯示IMEI圖示的ImageURL
            int intC_IMEI = int.Parse(TRANOUTQTY == "" ? "0" : TRANOUTQTY);
            int intS_IMEI = int.Parse(IMEI_QTY == "" ? "0" : IMEI_QTY);
            ASPxImage imgIMEI = e.Row.FindChildControl<ASPxImage>("imgIMEI");

            if (imgIMEI.ClientVisible && (IMEIFlag == "3" || IMEIFlag == "4"))
            {
                imgIMEI.ImageUrl = (intC_IMEI == intS_IMEI ? "~/Icon/check.png" : "~/Icon/non_complete.png");
            }

            //設定IMEI視窗的URL

            ASPxTextBox lblIMEI_QTY = e.Row.FindChildControl<ASPxTextBox>("lblIMEI_QTY");
            string _UUID = e.KeyValue == null ? StringUtil.CStr(ViewState["STOCKADJD_ID"]) : StringUtil.CStr(e.KeyValue);

            ASPxTextBox STOCKADJD_ID = e.Row.FindChildControl<ASPxTextBox>("STOCKADJD_ID");
            STOCKADJD_ID.Text = _UUID;

            // 繫結明細資料表  
            PopupControl txtIMEI = e.Row.FindChildControl<PopupControl>("txtIMEI");
            ASPxPopupControl ASPxPopupControl1 = txtIMEI.FindChildControl<ASPxPopupControl>("ASPxPopupControl1");

            lblIMEI_QTY.Attributes["onmouseover"] = string.Format("show('{0}');", IMEIContent("STOCKADJ_D_IMEI", _UUID, ""));
            lblIMEI_QTY.Attributes["onmouseout"] = "hide();";

            txtIMEI.KeyFieldValue1 = "STOCKADJ_D_IMEI;" + _UUID + ";" + ProdNo + ";" + TRANOUTQTY.Replace("-", "") + ";" + TRANOUTQTY + ";" + txtSTORE_NO.Text;

            //ASPxPopupControl1.ContentUrl = "~/VSS/SAL/SAL01/SAL01_inputIMEIData.aspx?SysDate=Date()&KeyFieldValue1=" + txtIMEI.KeyFieldValue1;

            //**2011/04/26 Tina：傳遞參數時，要先以加密處理。
            string encryptUrl = Utils.Param_Encrypt("SysDate=Date()&KeyFieldValue1=" + txtIMEI.KeyFieldValue1);
            ASPxPopupControl1.ContentUrl = string.Format("~/VSS/SAL/SAL01/SAL01_inputIMEIData.aspx?Param={0}", encryptUrl);

            if (!string.IsNullOrEmpty(IMEIFlag) && (IMEIFlag == "3" || IMEIFlag == "4"))
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
        ViewState["STOCKADJD_ID"] = GuidNo.getUUID();

        ASPxLabel lblIMEI = gvMaster.FindEditRowCellTemplateControl((GridViewDataColumn)gvMaster.Columns["IMEI"], "lbIMEI_QTY") as ASPxLabel;
        PopupControl txtIMEI = gvMaster.FindEditRowCellTemplateControl((GridViewDataColumn)gvMaster.Columns["IMEI"], "txtIMEI") as PopupControl;
        ASPxImage imgIMEI = gvMaster.FindEditRowCellTemplateControl((GridViewDataColumn)gvMaster.Columns["imgIMEI"], "imgIMEI") as ASPxImage;
        imgIMEI.ClientVisible = false;
        txtIMEI.Enabled = false;
        Button_Enabled(false);
    }

    protected void gvMaster_CommandButtonInitialize(object sender, ASPxGridViewCommandButtonEventArgs e)
    {
        if (e.VisibleIndex > -1 && gvMaster.Visible == true)
        {
            if (e.ButtonType == ColumnCommandButtonType.Edit)
            {
                if (this.lblSTATUS.Text.Trim().Contains("已存檔"))
                {
                    e.Enabled = false;

                }//已存檔
            }
        }
    }

    protected void gvMaster_CancelRowEditing(object sender, DevExpress.Web.Data.ASPxStartRowEditingEventArgs e)
    {
        DataTable dt = (DataTable)Session["gvMaster"];
        if (Session["gvMaster"] == null || dt.Rows.Count <= 0)
        {
            this.txtSTORE_NO.Enabled = true;
        }
        else
        {
            this.txtSTORE_NO.Enabled = false;
        }

        Button_Enabled(true); //可點擊 "確定移出" 和 "取消" 按鈕

    }

    protected void gvMaster_RowValidating(object sender, DevExpress.Web.Data.ASPxDataValidationEventArgs e)
    {
        string ProdNo = StringUtil.CStr(e.NewValues["PRODNO"]);
        ViewState["PRODNO"] = StringUtil.CStr(e.NewValues["PRODNO"]);
        string INV_OnHandQty = StringUtil.CStr(e.NewValues["INV_OnHandQty"]);
        string ADJQTY = StringUtil.CStr(e.NewValues["ADJQTY"]);
        string ADJREASON = StringUtil.CStr(e.NewValues["ADJREASON"]);
        string STOCKADJ_REASON_CODE = StringUtil.CStr(e.NewValues["STOCKADJ_REASON_CODE"]);

        ASPxTextBox IMEI_QTY = gvMaster.FindEditRowCellTemplateControl((GridViewDataColumn)gvMaster.Columns["IMEI"], "lblIMEI_QTY") as ASPxTextBox;
        ASPxTextBox IMEI_FLAG = gvMaster.FindEditRowCellTemplateControl((GridViewDataColumn)gvMaster.Columns["IMEI"], "txtIMEIFlag") as ASPxTextBox;

        if (!string.IsNullOrEmpty(ProdNo))
        {
            DataTable dtProd = new Product_Facade().Query_PRODUCT_STOCK(ProdNo, txtSTORE_NO.popTextBox.Text);
            if (dtProd.Rows.Count == 0)
            {
                e.RowError += "查無此商品料號!!";
                return;
            }
            else
            {
                if (Session["gvMaster"] != null)
                {
                    DataTable dt = Session["gvMaster"] as DataTable;

                    string expression;
                    expression = "PRODNO = '" + ProdNo + "'";
                    DataRow[] data = dt.Select(expression);
                    for (int i = 0; i < data.Length; i++)
                    {
                        string strSEQNO_Table = e.Keys["STOCKADJD_ID"] == null ? StringUtil.CStr(ViewState["STOCKADJD_ID"]) : StringUtil.CStr(e.Keys["STOCKADJD_ID"]);
                        string strSEQNO = StringUtil.CStr(data[i]["STOCKADJD_ID"]);
                        if (!string.IsNullOrEmpty(strSEQNO_Table) && strSEQNO_Table != strSEQNO)
                        {
                            e.RowError += "商品料號資料已存在,請重新輸入!!";
                            return;
                        }

                    }
                }
            }
        }


        if (string.IsNullOrEmpty(ADJQTY.Trim()))
        {
            e.RowError += "調整量不允許空白，請重新輸入!!";
            return;
        }
        if (string.IsNullOrEmpty(ADJREASON.Trim()))
        {
            e.RowError += "調整原因不允許空白，請重新輸入!!";
            return;
        }
        else
        {
            DataTable dt = INV18_1_Facade.getAdjNAME(ADJREASON);
            //string r = "";
            if (dt.Rows.Count <= 0 || string.IsNullOrEmpty(STOCKADJ_REASON_CODE))
            {
                e.RowError += "此調整原因代碼不存在，請重新輸入!!";
                return;
            }
        }
        decimal _ADJQTY = int.Parse(ADJQTY.Trim());
        if (_ADJQTY < 0)
        {
            _ADJQTY = -_ADJQTY;
            if (int.Parse(INV_OnHandQty.Trim()) < _ADJQTY)
            {
                e.RowError += "調整量不可大於庫存數量!!";
                return;
            }
        }
        if (IMEI_FLAG.Text == "3" || IMEI_FLAG.Text == "4")
        {
            if ((string.IsNullOrEmpty(IMEI_QTY.Text) || int.Parse(IMEI_QTY.Text) != _ADJQTY))
            {
                e.RowError += "請輸入正確的IMEI數!!";
                return;
            }
        }

        if (!string.IsNullOrEmpty(ADJQTY.Trim()))
        {
            if (isnumeric(ADJQTY.Trim()) == false)
            {
                if (ADJQTY.Trim().IndexOfAny(new char[] { '-' }) == -1)
                {
                    e.RowError += "輸入字串不為數字格式，請重新輸入!!";
                    return;
                }
            }
            else
            {
                if (!string.IsNullOrEmpty(ADJQTY.Trim()) && !string.IsNullOrEmpty(INV_OnHandQty.Trim()))
                {
                    if (int.Parse(ADJQTY.Trim()) == 0)
                    {
                        e.RowError += "調整量不允許為0，請重新輸入!!";
                        return;
                    }
                }
                else
                {
                    e.RowError += "請先輸入'調整門市'及'商品料號'，以利查詢'庫存量'!!";
                    return;
                }
            }
        }
        else
        {
            e.RowError += "調整量不允許空白，請重新輸入!!";
            return;
        }

    }

    protected void gvMaster_CellEditorInitialize(object sender, ASPxGridViewEditorEventArgs e)
    {
        if (!gvMaster.IsEditing || e.Column.FieldName != "PRODNO") return;

        GridViewDataTextColumn col_PRODNO = (GridViewDataTextColumn)gvMaster.Columns["PRODNO"];
        PopupControl strProdNo00 = ((PopupControl)gvMaster.FindEditRowCellTemplateControl(col_PRODNO, "txtProdNo"));
        string strProdNo = strProdNo00.Text;
        strProdNo00.popTextBox.Border.BorderStyle = System.Web.UI.WebControls.BorderStyle.None;
    }

    protected void gvMaster_StartRowEditing(object sender, DevExpress.Web.Data.ASPxStartRowEditingEventArgs e)
    {
        gvMaster.Selection.UnselectAll();

        foreach (GridViewColumn col in gvMaster.Columns)
        {
            if (col is GridViewDataColumn)
            {

                GridViewDataColumn dataCol = (GridViewDataColumn)col;
                Type typeCol = dataCol.GetType();
                switch (dataCol.FieldName)
                {
                    case "PRODNO":
                        dataCol.ReadOnly = true;
                        dataCol.PropertiesEdit.Style.Border.BorderStyle = System.Web.UI.WebControls.BorderStyle.None;

                        GridViewDataTextColumn col_PRODNO = (GridViewDataTextColumn)gvMaster.Columns["PRODNO"];
                        PopupControl strProdNo00 = ((PopupControl)gvMaster.FindEditRowCellTemplateControl(col_PRODNO, "txtProdNo"));
                        strProdNo00.popTextBox.Border.BorderStyle = System.Web.UI.WebControls.BorderStyle.None;
                        strProdNo00.Enabled = false;
                        col_PRODNO.ReadOnly = false;
                        col_PRODNO.PropertiesEdit.Style.Border.BorderStyle = System.Web.UI.WebControls.BorderStyle.None;
                        break;
                }

            }
        }

        this.txtSTORE_NO.Enabled = false;
        Button_Enabled(false);

    }

    protected void gvMaster_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
    {

        DataTable dtSYS;

        if (Session["gvMaster"] == null || StringUtil.CStr(Session["gvMaster"]) == "")
        { dtSYS = new DataTable(); }
        else
        { dtSYS = (DataTable)Session["gvMaster"]; }

        if (Session["gvMaster"] != null)
        {
            DataTable dt = Session["gvMaster"] as DataTable;
            DataRow[] dr = dt.Select("STOCKADJD_ID='" + ((ASPxTextBox)gvMaster.FindEditRowCellTemplateControl((GridViewDataColumn)gvMaster.Columns["IMEI"], "STOCKADJD_ID")).Text + "'");

            if (dr.Length > 0)
            {

                dr[0]["PRODNO"] = StringUtil.CStr(e.NewValues["PRODNO"]);
                dr[0]["PRODNAME"] = StringUtil.CStr(e.NewValues["PRODNAME"]);
                dr[0]["INV_OnHandQty"] = StringUtil.CStr(e.NewValues["INV_OnHandQty"]);
                dr[0]["ADJQTY"] = StringUtil.CStr(e.NewValues["ADJQTY"]);
                dr[0]["IMEI_QTY"] = ((ASPxTextBox)gvMaster.FindEditRowCellTemplateControl((GridViewDataColumn)gvMaster.Columns["IMEI"], "lblIMEI_QTY")).Text;
                dr[0]["IMEI_FLAG"] = ((ASPxTextBox)gvMaster.FindEditRowCellTemplateControl((GridViewDataColumn)gvMaster.Columns["IMEI"], "txtIMEIFlag")).Text;
                dr[0]["IMEI"] = ((PopupControl)gvMaster.FindEditRowCellTemplateControl((GridViewDataColumn)gvMaster.Columns["IMEI"], "txtIMEI")).Text;
                dr[0]["ADJREASON"] = StringUtil.CStr(e.NewValues["ADJREASON"]);
                dr[0]["STOCKADJ_REASON_CODE"] = StringUtil.CStr(e.NewValues["STOCKADJ_REASON_CODE"]);

                //**2011/02/17 Tina：註解起來，點擊【儲存】之後，才真正去更新Table的資料
                //if (this.lblSTATUS.Text.Trim().Contains("暫存"))
                //{
                //    INV18_StockADJ_DTO.STOCKADJDDataTable dtADJD = null;
                //    dtADJD = new INV18_StockADJ_DTO.STOCKADJDDataTable();
                //    INV18_StockADJ_DTO.STOCKADJDRow drADJD;
                //    drADJD = dtADJD.NewSTOCKADJDRow();
                //    drADJD["ADJNO"] = lblADJNO.Text.Trim(); //_ADJNO;
                //    drADJD["ADJQTY"] = StringUtil.CStr(e.NewValues["ADJQTY"]);
                //    drADJD["ADJREASON"] = StringUtil.CStr(e.NewValues["ADJREASON"]);
                //    drADJD["PRODNO"] = StringUtil.CStr(e.NewValues["PRODNO"]);
                //    drADJD["STOCKADJ_REASON_CODE"] = StringUtil.CStr(e.NewValues["STOCKADJ_REASON_CODE"]);
                //    drADJD["STOCKADJD_ID"] = StringUtil.CStr(e.NewValues["STOCKADJD_ID"]);
                //    drADJD["MODI_USER"] = logMsg.OPERATOR;
                //    drADJD["MODI_DTM"] = Convert.ToDateTime(System.DateTime.Now);
                //    dtADJD.Rows.Add(drADJD);
                //    new INV18_1_Facade().Update_ADJD(dtADJD);
                //}

                dt.AcceptChanges();
            }

            gvMaster.CancelEdit();
            e.Cancel = true;

            BindMasterData();

            //可點擊 "存檔" 和 "取消" 按鈕
            Button_Enabled(true);
        }

    }

    protected void gvMaster_PageIndexChanged(object sender, EventArgs e)
    {
        BindMasterData();
    }

    protected void gvMaster_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
    {
        DataTable dtSYS;
        if (Session["gvMaster"] == null || StringUtil.CStr(Session["gvMaster"]) == "")
        { dtSYS = new DataTable(); }
        else
        { dtSYS = (DataTable)Session["gvMaster"]; }

        if (Session["gvMaster"] != null)
        {
            DataTable dt = Session["gvMaster"] as DataTable;

            DataRow dr = dt.NewRow();
            dr["項次"] = StringUtil.CStr(dtSYS.Rows.Count);
            dr["PRODNO"] = StringUtil.CStr(e.NewValues["PRODNO"]);
            dr["PRODNAME"] = StringUtil.CStr(e.NewValues["PRODNAME"]);
            dr["INV_OnHandQty"] = StringUtil.CStr(e.NewValues["INV_OnHandQty"]);
            dr["ADJQTY"] = StringUtil.CStr(e.NewValues["ADJQTY"]);
            dr["IMEI_QTY"] = ((ASPxTextBox)gvMaster.FindEditRowCellTemplateControl((GridViewDataColumn)gvMaster.Columns["IMEI"], "lblIMEI_QTY")).Text;
            dr["IMEI_FLAG"] = ((ASPxTextBox)gvMaster.FindEditRowCellTemplateControl((GridViewDataColumn)gvMaster.Columns["IMEI"], "txtIMEIFlag")).Text;
            dr["IMEI"] = ((PopupControl)gvMaster.FindEditRowCellTemplateControl((GridViewDataColumn)gvMaster.Columns["IMEI"], "txtIMEI")).Text;
            dr["ADJREASON"] = StringUtil.CStr(e.NewValues["ADJREASON"]);
            dr["STOCKADJD_ID"] = StringUtil.CStr(ViewState["STOCKADJD_ID"]);
            dr["STOCKADJ_REASON_CODE"] = StringUtil.CStr(e.NewValues["STOCKADJ_REASON_CODE"]);

            dt.Rows.Add(dr);
            dt.AcceptChanges();

            gvMaster.CancelEdit();
            e.Cancel = true;

            BindMasterData();

            //可點擊 "確定移出" 和 "取消" 按鈕
            Button_Enabled(true);
        }

    }

    #endregion


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

    [System.Web.Services.WebMethod()]
    [System.Web.Script.Services.ScriptMethod()]
    static public string getStore(string STORE_NO)
    {
        DataTable dt = new Store_Facade().Query_StoreInfo(STORE_NO);
        string r = "";
        if (dt.Rows.Count > 0)
        {
            r = StringUtil.CStr(dt.Rows[0]["STORENAME"]);
            //取得該門市 當日的訂單
            r = r + ";" + OracleDBUtil.WorkDay(STORE_NO); //營業日
             
        }
        return r;
    }

    [System.Web.Services.WebMethod()]
    [System.Web.Script.Services.ScriptMethod()]
    static public string getAdjNAME(string ADJREASON)
    {
        DataTable dt = INV18_1_Facade.getAdjNAME(ADJREASON);
        string r = "";
        if (dt.Rows.Count > 0)
            r = StringUtil.CStr(dt.Rows[0]["STOCKADJ_REASON_CODE"]);



        return r;
    }

    //ajax 呼當前網頁的方式
    [System.Web.Services.WebMethod()]
    [System.Web.Script.Services.ScriptMethod()]
    static public string getProductInfo(string PRODUCT_NO, string STORE_NO)
    {
        string strInfo = "";
        if (!string.IsNullOrEmpty(PRODUCT_NO))
        {
            DataTable dt = new Product_Facade().Query_PRODUCT_STOCK(PRODUCT_NO, STORE_NO);
            if (dt.Rows.Count > 0)
            {
                DataRow dr = dt.Rows[0];
                strInfo = StringUtil.CStr(dr["PRODNO"]) + ";" + StringUtil.CStr(dr["PRODNAME"]) + ";" + StringUtil.CStr(dr["INV_OnHandQty"]) + ";" + StringUtil.CStr(dr["IMEI_FLAG"]);
            }
        }

        return strInfo;
    }
    [System.Web.Services.WebMethod()]
    [System.Web.Script.Services.ScriptMethod()]
    static public void delIMEI(string UUID)
    {
      
      new  INV18_1_Facade().DeleteIMEI(UUID);      

    }
}

