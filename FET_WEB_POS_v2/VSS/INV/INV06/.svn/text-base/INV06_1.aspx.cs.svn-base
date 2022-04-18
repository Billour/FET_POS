using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using DevExpress.Web.ASPxGridView;
using DevExpress.Web.ASPxPager;
using DevExpress.Web.ASPxEditors;
using System.Web.UI.HtmlControls;
using Resources;

using Advtek.Utility;
using FET.POS.Model.Common;
using FET.POS.Model.DTO;
using FET.POS.Model.Facade;
using FET.POS.Model.Facade.FacadeImpl;
using AdvTek.CustomControls;
using System.Collections.Specialized;

public partial class VSS_INV_INV06_INV06_1 : BasePage
{
    private INV06_Facade _INV06_Facade;
    private INV06_RTNM _INV06_RTNM_DTO;
    private string qRtnnId;
    private string qRtnNo;
    private string qStoreId;
    private string qEmployeeId;
    private string qMachineID;
    private string qRole;
    /// <summary>
    /// 取url參數
    /// </summary>
    private void GetRequestString() 
    {

        //**2011/04/21 Kristy：前頁傳遞參數時，會先以加密處理，在此要解密。
        if (!string.IsNullOrEmpty(Request.QueryString["Param"]))
        {
            NameValueCollection qscoll = Utils.Param_UrlDecode(Request.QueryString["Param"]);
            foreach (string key in qscoll.AllKeys)
            {
                if (key == "RTNN_ID")
                {
                    qRtnnId = string.Join(",", qscoll.GetValues(key));
                }
                else if (key == "RTNNO")
                {
                    qRtnNo = string.Join(",", qscoll.GetValues(key));
                }
                else if (key == "storeId")
                {
                    qStoreId = string.Join(",", qscoll.GetValues(key));
                }
                else if (key == "EmployeeId")
                {
                    qEmployeeId = string.Join(",", qscoll.GetValues(key));
                }
                else if (key == "Machine_ID")
                {
                    qMachineID = string.Join(",", qscoll.GetValues(key));
                }
                else if (key == "role")
                {
                    qRole = string.Join(",", qscoll.GetValues(key));
                }
            }
        }

    }
    protected void Page_Load(object sender, EventArgs e)
    {
        GetRequestString();
        doChangeMaster();
        if (!IsPostBack)
        {
            
            txtWorkDate.Text = OracleDBUtil.WorkDay(logMsg.STORENO); //營業日
            bindMasterData();
        }     
    }

    //換頁時暫存
    private void doChangeMaster()
    {
        if (gvMaster.VisibleRowCount > 0)
        {
            DataTable dt = Session["gvMaster"] as DataTable;
            DataRow dr;

            for (int i = 0; i < gvMaster.VisibleRowCount; i++)
            {
                if (((ASPxTextBox)gvMaster.FindRowCellTemplateControl(i, (GridViewDataColumn)gvMaster.Columns["RTND_UP_ID"], "txtRTND_UP_ID")) != null)
                {
                    string RTND_UP_ID_KEY = ((ASPxTextBox)gvMaster.FindRowCellTemplateControl(i, (GridViewDataColumn)gvMaster.Columns["RTND_UP_ID"], "txtRTND_UP_ID")).Text;
                    string txtRTNQTY = ((ASPxTextBox)gvMaster.FindRowCellTemplateControl(i, (GridViewDataColumn)gvMaster.Columns["RTNQTY"], "txtRTNQTY")).Text;
                    if (txtRTNQTY != "0")
                    {
                        dr = dt.Select("RTND_UP_ID='" + RTND_UP_ID_KEY + "'")[0];
                        string txtUNOPENQTY = ((ASPxTextBox)gvMaster.FindRowCellTemplateControl(i, (GridViewDataColumn)gvMaster.Columns["UNOPENQTY"], "txtUNOPENQTY")).Text;
                        string txtOPENQTY = ((ASPxTextBox)gvMaster.FindRowCellTemplateControl(i, (GridViewDataColumn)gvMaster.Columns["OPENQTY"], "txtOPENQTY")).Text;

                        string lblIMEI_QTY = ((ASPxTextBox)gvMaster.FindRowCellTemplateControl(i, (GridViewDataColumn)gvMaster.Columns["IMEI_QTY"], "lblIMEI_QTY")).Text;

                        dr["UNOPENQTY"] = txtUNOPENQTY;
                        dr["OPENQTY"] = txtOPENQTY;
                        dr["RTNQTY"] = txtRTNQTY;
                        dr["IMEI_QTY"] = lblIMEI_QTY;

                    }
                }
            }
            dt.AcceptChanges();
            Session["gvMaster"] = dt;
        }
    }

    private DataTable getMasterData()
    {
        _INV06_Facade = new INV06_Facade();
        string RTNNO = StringUtil.CStr(qRtnnId);

        DataTable dt = new DataTable();
        DataTable status = new DataTable();
        status = _INV06_Facade.QueryStatus(RTNNO);
        foreach (DataRow dr in status.Rows)
        {
            tempStatus.Value = StringUtil.CStr(dr["STATUS"]);
        }
        dt = INV06_PageHelper.GetRTNDBottomData(logMsg.STORENO, RTNNO);

        //Distinct
        string[] arrCol = new string[dt.Columns.Count];
        for (int i = 0; i < dt.Columns.Count; i++)
            arrCol[i] = dt.Columns[i].ColumnName;
        DataTable dt2 = dt.DefaultView.ToTable(dt.TableName, true, arrCol);

        //設定畫面
        if (dt2.Rows.Count > 0)
        {
            DataRow dr = dt2.Rows[0];
            lbOrderNo.Text = StringUtil.CStr(dr["RTNNO"]);
            string sTmp = StringUtil.CStr(dr["RTNDATE"]);
            if (sTmp.Length == 8)
                sTmp = sTmp.Substring(0, 4) + "/" + sTmp.Substring(4, 2) + "/" + sTmp.Substring(6, 2);
            lbRtndate.Text = sTmp;
            lbDataStatus.Text = StringUtil.CStr(dr["STATUS"]);
            lbBdate.Text = StringUtil.CStr(dr["B_DATE"]);
            lbReDesc.Text = StringUtil.CStr(dr["RETURN_DESCRIPTION"]);
            lbModidtm.Text = StringUtil.CStr(dr["MODI_DTM"]);
            lbEdate.Text = StringUtil.CStr(dr["E_DATE"]);
            lbDesc.Text = StringUtil.CStr(dr["DESCRIPTION"]);
            lbModiuser.Text = StringUtil.CStr(dr["MODI_USER"]) + " " + new Employee_Facade().GetEmpName(StringUtil.CStr(dr["MODI_USER"]));
            lbStatus2.Text = StringUtil.CStr(dr["REMARK"]);
            if (StringUtil.CStr(dr["STATUS"]) == "10")
            {
                lbStatus.Text = "未完成";

                //退倉訖日<營業日，可執行列印。
                if (Convert.ToDateTime(lbEdate.Text).Date < Convert.ToDateTime(txtWorkDate.Text).Date)
                {
                    //gvMaster.Enabled = false;
                    //gvMaster.PagerBarEnabled = true;
                    btnSave.ClientEnabled = false;
                    btnCancel.ClientEnabled = false;
                }
                else
                {
                    btnPrint.ClientEnabled = false;
                }
            }
            else
            {
                lbStatus.Text = "已完成";
                btnSave.ClientEnabled = false;
                btnCancel.ClientEnabled = false;
            }
        }

        return dt2;
    }

    private void bindMasterData()
    {
        Session["gvMaster"] = getMasterData();
        gvMaster.DataSource = (DataTable)Session["gvMaster"];
        gvMaster.DataBind();
    }

    protected void gvMaster_PageIndexChanged(object sender, EventArgs e)
    {
        gvMaster.DataSource = (DataTable)Session["gvMaster"];
        gvMaster.DataBind();
    }

    private void ProcessRequest(string filename)
    {
        string filePath = (new SAL01_Facade()).getUploadPath();
        if (!string.IsNullOrEmpty(filePath))
        {
            ScriptManager.RegisterClientScriptBlock(this,
                                                    this.GetType(),
                                                    "Print",
                                                    "document.getElementById('" + fDownload.ClientID + "').src='" + Request.ApplicationPath + filePath + "/" + filename + "';",
                                                    true);
        }
        else
        {
            ScriptManager.RegisterClientScriptBlock(this, typeof(string), "PrintInvoiceError", "alert('退倉單列印失敗!!');", true);
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        _INV06_Facade = new INV06_Facade();
        _INV06_RTNM_DTO = new INV06_RTNM();
        try
        {
            DataTable dt = new DataTable();
            dt = (DataTable)Session["gvMaster"];
            string RTNNO = StringUtil.CStr(qRtnnId);

            DataTable RTNMDT = new DataTable();

            RTNMDT = INV06_PageHelper.GETRTNMByRTNN_ID(RTNNO);

            INV06_RTNM.RTND_UPDataTable dtRTND;
            INV06_RTNM.RTND_UPRow drRTND;

            dtRTND = _INV06_RTNM_DTO.Tables["RTND_UP"] as INV06_RTNM.RTND_UPDataTable;

            foreach (DataRow row in dt.Rows)
            {

                if (StringUtil.CStr(row["IMEI_FLAG"]) == "3")
                {
                    if ((Convert.ToInt32(StringUtil.CStr(row["RTNQTY"]))) != (Convert.ToInt32(StringUtil.CStr(row["IMEI_QTY"]))))
                    {

                        ScriptManager.RegisterClientScriptBlock(this, typeof(string), "退倉數量", "alert('IMEI數量不等於退倉數量，請重新輸入!!!');", true);
                        return;

                    }
                }

                
                drRTND = dtRTND.NewRTND_UPRow();


                //未拆封數量
                string SUnopenQTY = StringUtil.CStr(row["UNOPENQTY"]);
                SUnopenQTY = SUnopenQTY == "" ? "0" : SUnopenQTY;

                //已拆封數量
                string SOpenQTY = StringUtil.CStr(row["OPENQTY"]);
                SOpenQTY = SOpenQTY == "" ? "0" : SOpenQTY;

                //帳上庫存量
                string SONHandQTY = StringUtil.CStr(row["ON_HAND_QTY"]);
                SONHandQTY = SONHandQTY == "" ? "0" : SONHandQTY;

                //退倉數量
                string SReturnQTY = StringUtil.CStr(row["RTNQTY"]);
                SONHandQTY = SONHandQTY == "" ? "0" : SONHandQTY;

                string SID = StringUtil.CStr(row["RTND_UP_ID"]);

                //帳上庫存量
                int Total = Convert.ToInt32(StringUtil.CStr(row["ON_HAND_QTY"]));

                int ENDQTY = Total - (Convert.ToInt32(SOpenQTY) + Convert.ToInt32(SUnopenQTY));
                drRTND.OPENQTY = Convert.ToInt32(SOpenQTY);
                drRTND.UNOPENQTY = Convert.ToInt32(SUnopenQTY);
                drRTND.RTNQTY = Convert.ToInt32(SOpenQTY) + Convert.ToInt32(SUnopenQTY);
                drRTND.ENDQTY = ENDQTY;

                drRTND.RTNDATE = System.DateTime.Now.ToString("yyyyMMdd");
                drRTND.RTND_UP_ID = SID;
                drRTND.STORE_NO = logMsg.STORENO;
                drRTND.RTND_PROD_ID = StringUtil.CStr(row["RTND_PROD_ID"]);
                drRTND.RTND_STORE_ID = StringUtil.CStr(row["RTND_STORE_ID"]);

                drRTND.CREATE_USER = logMsg.OPERATOR;
                drRTND.CREATE_DTM = System.DateTime.Parse(System.DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"));

                dtRTND.Rows.Add(drRTND);
                _INV06_RTNM_DTO.AcceptChanges();

            }
            //更新資料庫

            //***_INV06_RTNM_DTO = new INV06_RTNM();
            INV06_RTNM.RTNMDataTable dtRTNM;
            INV06_RTNM.RTNMRow drRTNM;
            dtRTNM = _INV06_RTNM_DTO.Tables["RTNM"] as INV06_RTNM.RTNMDataTable;
          
            foreach (DataRow row in RTNMDT.Rows)
            {
                drRTNM = dtRTNM.NewRTNMRow();
                drRTNM.RTNN_ID = StringUtil.CStr(row[0]);
                drRTNM.RTNNO = StringUtil.CStr(row[1]);
                drRTNM.B_DATE = System.DateTime.Parse(StringUtil.CStr(row[2]));
                drRTNM.E_DATE = System.DateTime.Parse(StringUtil.CStr(row[3]));
                drRTNM.MODI_DTM = System.DateTime.Parse(System.DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"));
                drRTNM.MODI_USER = logMsg.OPERATOR;
                drRTNM.STATUS = "60";
                dtRTNM.Rows.Add(drRTNM);
            }
          
            _INV06_RTNM_DTO.AcceptChanges();


            //***********************************
            //更新資料庫         

            _INV06_Facade.Add_RTND_Update_RTNM(_INV06_RTNM_DTO, dt, logMsg.OPERATOR);

            bindMasterData();
            Response.Redirect("INV06.aspx");
            //************************************
        }
        catch (Exception ex)
        {
            Logger.Log.Error(ex.Message, ex);
            throw ex;
        }
        finally
        {
            _INV06_Facade = null;
            _INV06_RTNM_DTO = null;

        }
    }

    protected void btnPrint_Click(object sender, EventArgs e)
    {

        DataTable dtheader = new DataTable();
        dtheader.Columns.Add("header1", typeof(string));
        dtheader.Columns.Add("header2", typeof(string));
        dtheader.Columns.Add("header3", typeof(string));
        DataRow NewRowHeader = dtheader.NewRow();
        NewRowHeader["header1"] = "退倉單號：" + lbOrderNo.Text;
        NewRowHeader["header2"] = "退倉起迄日：" + lbBdate.Text + "~" + lbEdate.Text;
        NewRowHeader["header3"] = "狀態：" + lbStatus.Text;   //"狀態：已完成";
        dtheader.Rows.Add(NewRowHeader);

        NewRowHeader = dtheader.NewRow();
        NewRowHeader["header1"] = "門市編號：" + qStoreId.Trim() + " " + new Store_Facade().GetStoreName(qStoreId.Trim());
        NewRowHeader["header2"] = "退倉原因代號：" + lbReDesc.Text;
        NewRowHeader["header3"] = "更新日期：" + lbModidtm.Text;
        dtheader.Rows.Add(NewRowHeader);

        NewRowHeader = dtheader.NewRow();
        NewRowHeader["header1"] = "退倉日期：" + lbRtndate.Text;
        NewRowHeader["header2"] = "後續處理代號：" + lbDesc.Text;
        NewRowHeader["header3"] = "更新人員：" + lbModiuser.Text;
        dtheader.Rows.Add(NewRowHeader);
        DataTable dtfooter = new DataTable();
        dtfooter.Columns.Add("footer1", typeof(string));

        string RTNN_ID = StringUtil.CStr(qRtnnId);
        DataTable dt = new INV06_Facade().Query_PrintRTNDDetailData(logMsg.STORENO, RTNN_ID);


        string filename = new Output().Print("PDF", "退倉單", dtheader, dt, dtfooter);
        //Response.Redirect(Utils.CreateTamperProofDownloadURL(filename));
        ProcessRequest(filename);
    }

    protected void gvMaster_HtmlRowPrepared(object sender, ASPxGridViewTableRowEventArgs e)
    {
        if (e.RowType == GridViewRowType.Data)
        {
            if (e.Row.FindChildControl<ASPxTextBox>("txtIMEIFlag") == null)
                return;

            string IMEIFlag = e.Row.FindChildControl<ASPxTextBox>("txtIMEIFlag").Text;
            string _ProdNo = StringUtil.CStr(e.GetValue("PRODNO"));
            // string _RTNQTY = e.Row.FindChildControl<ASPxTextBox>("txtRTNQTY").Text;
            string _RTNQTY = ((ASPxTextBox)gvMaster.FindRowCellTemplateControl(e.VisibleIndex, (GridViewDataColumn)gvMaster.Columns["RTNQTY"], "txtRTNQTY")).Text;
            string IMEI_QTY = e.Row.FindChildControl<ASPxTextBox>("lblIMEI_QTY").Text;
            string _RTND_UP_ID = e.Row.FindChildControl<ASPxTextBox>("txtRTND_UP_ID").Text;
            //設定顯示IMEI圖示的ImageURL
            //int intC_IMEI = 0;
            //bool isNum = int.TryParse(_RTNQTY, out intC_IMEI);
            int intC_IMEI = int.Parse(_RTNQTY == "" ? "0" : _RTNQTY);

            int intS_IMEI = int.Parse(IMEI_QTY == "" ? "0" : IMEI_QTY);
            ASPxImage imgIMEI = e.Row.FindChildControl<ASPxImage>("imgIMEI");


            if (imgIMEI.Visible && (IMEIFlag == "3"))
            {
                imgIMEI.ImageUrl = (intC_IMEI == intS_IMEI ? "~/Icon/check.png" : "~/Icon/non_complete.png");
            }
            else
            {
                imgIMEI.ImageUrl = "~/Icon/check.png";
                imgIMEI.ClientEnabled = false;
            }


            //設定IMEI視窗的URL
            ASPxTextBox lblIMEI_QTY = e.Row.FindChildControl<ASPxTextBox>("lblIMEI_QTY");

            // 繫結明細資料表  
            lblIMEI_QTY.Attributes["onmouseover"] = string.Format("show('{0}');", IMEIContent("RTND_IMEI", _RTND_UP_ID, ""));
            lblIMEI_QTY.Attributes["onmouseout"] = "hide();";

            PopupControl txtIMEI = e.Row.FindChildControl<PopupControl>("txtIMEI");
            ASPxPopupControl ASPxPopupControl1 = txtIMEI.FindChildControl<ASPxPopupControl>("ASPxPopupControl1");
            txtIMEI.KeyFieldValue1 = "RTND_IMEI;" + _RTND_UP_ID + ";" + _ProdNo + ";" + _RTNQTY;
            
            //ASPxPopupControl1.ContentUrl = "~/VSS/SAL/SAL01/SAL01_inputIMEIData.aspx?SysDate=Date()&KeyFieldValue1=" + txtIMEI.KeyFieldValue1;

            //**2011/04/26 Tina：傳遞參數時，要先以加密處理。
            string encryptUrl = Utils.Param_Encrypt("SysDate=Date()&KeyFieldValue1=" + txtIMEI.KeyFieldValue1);
            ASPxPopupControl1.ContentUrl = string.Format("~/VSS/SAL/SAL01/SAL01_inputIMEIData.aspx?Param={0}", encryptUrl);


            if (IMEIFlag == "3")
            {
                txtIMEI.Enabled = true;
                imgIMEI.ClientVisible = true;

            }
            else
            {
                txtIMEI.Enabled = false;
                imgIMEI.ClientVisible = false;

            }

            if (lbDataStatus.Text == "60" || Convert.ToDateTime(lbEdate.Text).Date < Convert.ToDateTime(txtWorkDate.Text).Date)
            {
                txtIMEI.Enabled = false;
                e.Row.FindChildControl<ASPxTextBox>("txtUNOPENQTY").ReadOnly = true;
                e.Row.FindChildControl<ASPxTextBox>("txtUNOPENQTY").Border.BorderStyle = BorderStyle.None;
                e.Row.FindChildControl<ASPxTextBox>("txtOPENQTY").ReadOnly = true;
                e.Row.FindChildControl<ASPxTextBox>("txtOPENQTY").Border.BorderStyle = BorderStyle.None;
            }

        }

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
