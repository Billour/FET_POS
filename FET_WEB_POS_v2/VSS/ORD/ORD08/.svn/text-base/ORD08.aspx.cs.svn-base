using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Advtek.Utility;
using System.Collections;
using DevExpress.Web;
using DevExpress.Web.ASPxEditors;
using DevExpress.Web.ASPxCallbackPanel;
using DevExpress.Web.ASPxGridView;
using DevExpress.Web.ASPxGridView.Rendering;
using DevExpress.Web.ASPxPager;
using FET.POS.Model.Facade;
using FET.POS.Model.Facade.FacadeImpl;
using FET.POS.Model.DTO;
using FET.POS.Model.Common;
using System.Runtime.Serialization;
using System.Web.UI.HtmlControls;
using DevExpress.Web.ASPxPopupControl;
using System.Collections.Specialized;

public partial class VSS_ORD_ORD08 : BasePage
{
    string NDS_NO
    {
        get
        {
            string r = "";
           
            if (!string.IsNullOrEmpty(Request.QueryString["Param"]))
            {
                NameValueCollection qscoll = Utils.Param_UrlDecode(Request.QueryString["Param"]);
                foreach (string key in qscoll.AllKeys)
                {
                    if (key == "dno")
                    {
                        r = string.Join(",", qscoll.GetValues(key));
                        break;
                    }
                }
            }
            return r;
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack)
        {
            //if (logMsg.ROLE_TYPE != StringUtil.CStr(System.Configuration.ConfigurationManager.AppSettings["DefaultRoleHQ"]))
            //{
            //    //彈跳視窗
            //    ScriptManager.RegisterClientScriptBlock(this, GetType(), "AlertMessage", "alert('非總部人員無法使用此功能!!');", true);
            //    this.gvMaster.Enabled = false;
            //    this.gvDetail.Enabled = false;
            //    this.btnQueryEdit.Enabled = false;
            //    return;
            //}


            this.bntCommitUpload.Enabled = false;
            this.btnDelete.Enabled = false;
            lblUpDate.Text = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");
            lblUpUser.Text = logMsg.OPERATOR + " " + new Employee_Facade().GetEmpName(logMsg.OPERATOR);

            if (string.IsNullOrEmpty(NDS_NO))  //不是從查詢頁面過來的資料
            {
                lblNDS_Status.Text = "未存檔";

                ////先判斷當天的主配作業是否有不為3 (已轉門市訂單)的資料
                //DataTable dtStatus = ORD08_PageHelper.GetHQNDSORDERM_STATUS();
                //if (dtStatus.Rows.Count == 0)  //都為3，則可以再新增主配作業
                //{
                hdNDS_MID_UUID.Text = GuidNo.getUUID();  //主配作業的UUID
                //}
                //else //否，表示有尚未處理的主配資料，因此不允許登入者再新增主配作業
                //{
                //    gvMaster.Enabled = false;
                //    gvDetail.Enabled = false;
                //    gvDetail.PagerBarEnabled = true;
                //    //彈跳視窗
                //    ScriptManager.RegisterClientScriptBlock(this, GetType(), "AlertMessage", "alert('前一項主配作業尚未轉門市訂單，故無法再新增主配作業!!');", true);
                //    return;
                //}

                Session["gvMasterORD08"] = null;
                Session["gvMasterOLD"] = null;
                BindData("Page_Load");

            }
            else
            {
                this.bntCommitUpload.Enabled = true;
                this.btnDelete.Enabled = true;
                Session["gvMasterORD08"] = null;
                Session["gvMasterOLD"] = null;
                BindData("NDS_NO");
                GetNDS_Status(NDS_NO);
                gvMasterStatus();
            }

        }
        else
        {
            if (Request["__EVENTARGUMENT"] == "AAA")
            {
                gvMaster.FocusedRowIndex = -1;
                this.txtBatchNO.Value = StringUtil.CStr(Session["UploadBatchNo"]);
                hdNDS_MID_UUID.Text = this.txtBatchNO.Value;  //主配作業的UUID
                Session["gvMasterORD08"] = null;
                Session["gvMasterOLD"] = null;
                BindData("btnImport");
                divShow.Visible = true;
                if (gvMaster.VisibleRowCount > 0)
                {
                    gvMaster.FocusedRowIndex = 0;
                }
            }
        }

    }

    private void gvMasterStatus()
    {
        if (this.lblNDS_Status.Text == "已上傳" || this.lblNDS_Status.Text == "已轉門市訂單" || !gvMaster.Enabled)
        {
            gvMaster.Enabled = false;
            //gvMaster.FindChildControl<ASPxButton>("btnAddM").Enabled = false;
            //gvMaster.FindChildControl<ASPxButton>("btnDeleteM").Enabled = false;
            //gvMaster.FindChildControl<ASPxButton>("btnImport").Enabled = false;
            //gvMaster.FindChildControl<ASPxPopupControl>("ASPxPopupControl1").Enabled = false;
            //gvMaster.Columns[0].Visible = false;
            gvMaster.Columns[1].Visible = false;  //編輯
        }
        else
        {
            gvMaster.Enabled = true;
        }

        gvMaster.PagerBarEnabled = true;
    }

    private void gvDetailStatus()
    {
        if (gvMaster.FocusedRowIndex > -1)
        {
            if (this.lblNDS_Status.Text == "已上傳" || this.lblNDS_Status.Text == "已轉門市訂單" || gvMaster.FindChildControl<ASPxButton>("btnAddM") == null)
            {
                gvDetail.Enabled = false;
                gvDetail.Columns[1].Visible = false;  //編輯
            }
            else
            {
                gvDetail.Enabled = true;
                gvDetail.Columns[1].Visible = true;  //編輯

            }

            gvDetail.PagerBarEnabled = true;
        }
    }

    private void BindData(string Status)
    {
        DataTable dtMaster = new DataTable();
        DataTable dtDetail = new DataTable();
        if (Session["gvMasterORD08"] == null)
        {
            switch (Status)
            {
                case "Page_Load":  //頁面Page_Load，不顯示資料，只取得空資料表格式
                    dtMaster = new ORD08_Facade().QueryNDSMasterMethodData("");
                    dtDetail = new ORD08_Facade().QueryNDSDetailMethodData("", true);
                    gvDetail.DataSource = dtDetail;
                    gvDetail.DataBind();
                    gvDetail.Selection.UnselectAll();
                    gvDetail.Enabled = false;
                    this.lblNDS_Status.Text = "未存檔";
                    this.lblNDS_MID.Text = "";
                    break;
                case "btnImport":  //匯入資料成功後，重新載入資料
                    dtMaster = ORD08_PageHelper.GetNDSMasterData_Temp(txtBatchNO.Value);
                    dtDetail = ORD08_PageHelper.GetNDSDetailData_Temp(txtBatchNO.Value, "");  //取得主配作業門市所有資料
                    gvDetail.DataSource = null;
                    gvDetail.DataBind();
                    gvDetail.Selection.UnselectAll();
                    gvDetail.Enabled = false;
                    lblNDS_Status.Text = "未存檔";
                    //lblNDS_MID.Text = "";
                    break;
                case "NDS_NO":     //從查詢頁面過來的主配單號，依據主配單號取得主配資料
                    dtMaster = ORD08_PageHelper.GetNDSMasterMethodData(NDS_NO);
                    dtDetail = new ORD08_Facade().QueryNDSDetailMethodData(NDS_NO, false);  //取得主配作業門市所有資料
                    Session["gvMasterOLD"] = dtDetail;
                    divShow.Visible = true;
                    break;
                default:
                    break;
            }
        }
        else
        {
            dtMaster = Session["gvMasterORD08"] as DataTable;
            dtDetail = Session["gvDetailORD08"] as DataTable;
        }

        Session["gvMasterORD08"] = dtMaster;
        Session["gvDetailORD08"] = dtDetail;
        BindMasterData();
    }

    private void BindMasterData()
    {
        if (Session["gvMasterORD08"] != null)
        {
            DataTable dt = new DataTable();
            dt = Session["gvMasterORD08"] as DataTable;
            gvMaster.DataSource = dt;
            gvMaster.DataBind();
            gvMaster.Selection.UnselectAll();
        }

    }

    private void BindDetailData()
    {
        if (Session["gvDetailORD08"] != null)
        {
            DataTable dt = new DataTable();
            dt = Session["gvDetailORD08"] as DataTable;

            string strWhere = " 1 <> 1";
            if (gvMaster.GetRowValues(gvMaster.FocusedRowIndex, "PRODNO") != null)
            {
                strWhere = "PRODNO = '" + StringUtil.CStr(gvMaster.GetRowValues(gvMaster.FocusedRowIndex, "PRODNO")) + "'";
            }
            DataRow[] dr = dt.Select(strWhere);
            if (dr.Length > 0)
            {
                dt = dr.CopyToDataTable();
            }
            else  //該商品料號查無明細，就取得空資料表格式
            {
                dt = new ORD08_Facade().QueryNDSDetailMethodData("", true);
            }

            gvDetail.DataSource = dt;
            gvDetail.DataBind();
            gvDetail.Selection.UnselectAll();

            gvDetailStatus();

        }
    }

    private void BindZoneType()
    {
        ASPxComboBox ddlZone = gvDetail.FindChildControl<ASPxComboBox>("ddlZone");
        if (ddlZone != null)
        {
            ddlZone.TextField = "ZONE_NAME";
            ddlZone.ValueField = "ZONE";
            ddlZone.DataSource = Common_PageHelper.getZone(true);
            ddlZone.DataBind();
            ddlZone.SelectedIndex = 0;
        }
    }

    private void GetNDS_Status(string sNDS_NO)
    {
        if (Session["gvMasterORD08"] == null) return;

        DataTable dt = Session["gvMasterORD08"] as DataTable;

        lblNDS_MID.Text = sNDS_NO;

        hdNDS_MID_UUID.Text = StringUtil.CStr(dt.Rows[0]["HQ_ORDER_M_ID"]);
        switch (StringUtil.CStr(dt.Rows[0]["STATUS"]))
        {
            case "1":
                lblNDS_Status.Text = "已存檔";
                divShow.Visible = true;
                btnSave.Enabled = true;
                btnCancel.Enabled = false;
                btnExport.Enabled = false;
                bntCommitUpload.Enabled = true;
                btnDelete.Enabled = true;
                break;
            case "2":
                lblNDS_Status.Text = "已上傳";
                divShow.Visible = true;
                btnSave.Enabled = false;
                btnCancel.Enabled = false;
                btnExport.Enabled = true;
                bntCommitUpload.Enabled = false;
                btnDelete.Enabled = false;
                lblUpDate.Text = StringUtil.CStr(dt.Rows[0]["MODI_DTM"]);
                lblUpUser.Text = StringUtil.CStr(dt.Rows[0]["MODI_USER"]) + " " + new Employee_Facade().GetEmpName(StringUtil.CStr(dt.Rows[0]["MODI_USER"]));
                break;
            case "3":
                lblNDS_Status.Text = "已轉門市訂單";
                divShow.Visible = true;
                btnSave.Enabled = false;
                btnCancel.Enabled = false;
                btnExport.Enabled = false;
                bntCommitUpload.Enabled = false;
                btnDelete.Enabled = false;
                lblUpDate.Text = StringUtil.CStr(dt.Rows[0]["MODI_DTM"]);
                lblUpUser.Text = StringUtil.CStr(dt.Rows[0]["MODI_USER"]) + " " + new Employee_Facade().GetEmpName(StringUtil.CStr(dt.Rows[0]["MODI_USER"]));
                break;
            default:
                lblNDS_Status.Text = StringUtil.CStr(dt.Rows[0]["STATUS"]);
                break;
        }
    }

    #region Button 觸發事件

    protected void btnQueryEdit_Click(object sender, EventArgs e)
    {
        Response.Redirect("ORD07.aspx");
    }

    protected void btnAddM_Click(object sender, EventArgs e)
    {
        gvMaster.Selection.UnselectAll();
        gvMaster.AddNewRow();
        gvMaster.FocusedRowIndex = -1;

    }

    protected void btnAddD_Click(object sender, EventArgs e)
    {
        if (gvMaster.FocusedRowIndex > -1)
        {
            divShow.Visible = true;
            gvDetail.Selection.UnselectAll();
            gvDetail.AddNewRow();
            gvDetail.FocusedRowIndex = -1;
        }
    }

    protected void btnExport_Click(object sender, EventArgs e)
    {
        DataTable dt = new ORD08_Facade().Export_HQNDSOrder(this.lblNDS_MID.Text);
        string filename = new Output().Print("XLS", "Non-DropShipment主配作業<td></td><td>匯出時間：" + DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss") + "</td>", null, dt, null);
        Response.Redirect(Utils.CreateTamperProofDownloadXlsURL("Non-DropShipment.xls"));
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (Session["gvMasterORD08"] != null && Session["gvDetailORD08"] != null)
        {
            string HQ_NDS_ORDER_NO = "";  //NDS主配單號
            if (string.IsNullOrEmpty(lblNDS_MID.Text))
            {
               // ORD09_Facade facade09 = new ORD09_Facade();
                HQ_NDS_ORDER_NO = SerialNo.GenNo("HRORDER");
                    //facade09.GetSysPara("");

                //ORD08_PageHelper.GetNDS_ORDER_NO();
            }
            else
            {
                HQ_NDS_ORDER_NO = lblNDS_MID.Text;
            }

            if (string.IsNullOrEmpty(HQ_NDS_ORDER_NO))
            {
                //彈跳視窗
                ScriptManager.RegisterClientScriptBlock(this, GetType(), "AlertMessage", "alert('NDS主配單號為空!!');", true);
                return;
            }
            else
            {
                DataTable dtM = Session["gvMasterORD08"] as DataTable;
                DataTable dtD = Session["gvDetailORD08"] as DataTable;

                foreach (DataRow dr in dtM.Rows)
                {
                    string ATR_QTY = StringUtil.CStr(dr["ATR_QTY"]);
                    string DIS_QTY = StringUtil.CStr(dr["DIS_QTY"]);

                    int intATR = int.Parse(string.IsNullOrEmpty(ATR_QTY) ? "0" : ATR_QTY);
                    int intDISQ = int.Parse(string.IsNullOrEmpty(DIS_QTY) ? "0" : DIS_QTY);

                    if (intATR - intDISQ < 0)
                    {
                        //彈跳視窗
                        ScriptManager.RegisterClientScriptBlock(this, GetType(), "AlertMessage", "alert('【" + StringUtil.CStr(dr["PRODNO"]) + "】ATR不足，不允許主配，請重新輸入');", true);
                        return;
                    }


                }

                DataTable dtDetail = new ORD08_Facade().QueryNDSDetailMethodData(lblNDS_MID.Text, false);  //取得主配作業門市所有資料
                Session["gvMasterOLD"] = dtDetail;

                //進行存檔
                SavaData(HQ_NDS_ORDER_NO, "1");
                dtM.Rows[0]["STATUS"] = "1";
                dtM.AcceptChanges();
                Session["gvMasterORD08"] = dtM;
                //Session["gvMasterOLD"] = dtD;
                this.bntCommitUpload.Enabled = true;
                this.btnDelete.Enabled = true;
                GetNDS_Status(HQ_NDS_ORDER_NO);

                //彈跳視窗
                ScriptManager.RegisterClientScriptBlock(this, GetType(), "AlertMessage", "alert('儲存成功!!');", true);
                return;
            }//end-if (string.IsNullOrEmpty(HQ_NDS_ORDER_NO))

        }
    }

    protected void bntCommitUpload_Click(object sender, EventArgs e)
    {
        //判斷主配商品的主配量有沒有等於主配門市的主動配貨量總合
        //如果相等，變更STATUS為2(已上傳)
        //如果不相等，不做變更並且告知User有數量不相等
        if (Session["gvMasterORD08"] != null && Session["gvDetailORD08"] != null)
        {
            string HQ_NDS_ORDER_NO = "";  //NDS主配單號
            if (string.IsNullOrEmpty(lblNDS_MID.Text))
            {
                //**2011/04/17 Tina：NDS單號的由SEQSEGMENT TABLE計算而來。 
                HQ_NDS_ORDER_NO = SerialNo.GenNo("HRORDER");
                //HQ_NDS_ORDER_NO = ORD08_PageHelper.GetNDS_ORDER_NO();
            }
            else
            {
                HQ_NDS_ORDER_NO = lblNDS_MID.Text;
            }

            if (string.IsNullOrEmpty(HQ_NDS_ORDER_NO))
            {
                //彈跳視窗
                ScriptManager.RegisterClientScriptBlock(this, GetType(), "AlertMessage", "alert('NDS主配單號為空!!');", true);
                return;
            }
            else
            {

                DataTable dtMaster = Session["gvMasterORD08"] as DataTable;
                DataTable dtDetail = Session["gvDetailORD08"] as DataTable;

                for (int i = 0; i < dtMaster.Rows.Count; i++)
                {
                    string HQ_ORDER_D = StringUtil.CStr(dtMaster.Rows[i]["HQ_ORDER_D"]);
                    string DisQty = StringUtil.CStr(dtMaster.Rows[i]["DIS_QTY"]);
                    string ASSIGN_QTY = StringUtil.CStr(dtDetail.Compute("Sum(ASSIGN_QTY)", "HQ_ORDER_D = '" + HQ_ORDER_D + "'"));  //主配門市的總配貨量
                    Int64 iDisQty = Convert.ToInt64(string.IsNullOrEmpty(DisQty) ? "0" : DisQty);
                    if (iDisQty <= 0)
                    {
                        //彈跳視窗
                        ScriptManager.RegisterClientScriptBlock(this, GetType(), "AlertMessage", "alert('主配量不允許小於等於0');", true);
                        return;
                    }
                    else
                    {
                        string ATR_QTY = StringUtil.CStr(dtMaster.Rows[i]["ATR_QTY"]);
                        string DIS_QTY = StringUtil.CStr(dtMaster.Rows[i]["DIS_QTY"]);

                        int intATR = int.Parse(string.IsNullOrEmpty(ATR_QTY) ? "0" : ATR_QTY);
                        int intDISQ = int.Parse(string.IsNullOrEmpty(DIS_QTY) ? "0" : DIS_QTY);

                        if (intATR - intDISQ < 0)
                        {
                            //彈跳視窗
                            ScriptManager.RegisterClientScriptBlock(this, GetType(), "AlertMessage", "alert('【" + StringUtil.CStr(dtMaster.Rows[i]["PRODNO"]) + "】ATR不足，不允許主配，請重新輸入');", true);
                            return;
                        }
                    }
                }

                SavaData(HQ_NDS_ORDER_NO, "2");

                dtMaster.Rows[0]["STATUS"] = "2";
                dtMaster.Rows[0]["MODI_USER"] = logMsg.OPERATOR;
                dtMaster.Rows[0]["MODI_DTM"] = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");
                dtMaster.AcceptChanges();
                Session["gvMasterORD08"] = dtMaster;

                GetNDS_Status(HQ_NDS_ORDER_NO);
                gvMasterStatus();
                gvDetailStatus();

                //彈跳視窗
                ScriptManager.RegisterClientScriptBlock(this, GetType(), "AlertMessage", "alert('上傳成功!!');", true);
                return;
            }

        }
    }

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(this.lblNDS_MID.Text))  //從查詢頁面過來的，所以還要刪除資料表中的資料
        {
            ORD08_HQNDSORDERMSet_DTO ORD08_DTO = new ORD08_HQNDSORDERMSet_DTO();
            ORD08_HQNDSORDERMSet_DTO.HQ_NDS_ORDER_MDataTable dtH = ORD08_DTO.HQ_NDS_ORDER_M;

            ORD08_HQNDSORDERMSet_DTO.HQ_NDS_ORDER_MRow drH = dtH.NewHQ_NDS_ORDER_MRow();

            drH.HQ_NDS_ORDER_NO = this.lblNDS_MID.Text;
            drH.HQ_ORDER_M_ID = this.hdNDS_MID_UUID.Text;
            drH.MODI_DTM = System.DateTime.Now;
            drH.MODI_USER = logMsg.MODI_USER;
            drH.CREATE_DTM = System.DateTime.Now;
            drH.CREATE_USER = logMsg.CREATE_USER;
            drH.STATUS = "1"; //狀態 1:已存檔 2:已上傳 3.已轉門市訂單

            dtH.Rows.Add(drH);

            ORD08_Facade facade = new ORD08_Facade();
            facade.Delete_NDSMMethodData(ORD08_DTO, (DataTable)Session["gvMasterOLD"]);
        }
        Session["gvMasterORD08"] = null;
        Session["gvMasterOLD"] = null;
        Session["gvDetailORD08"] = null;
        BindData("Page_Load");

        divShow.Visible = true;
        btnSave.Enabled = true;
        btnCancel.Enabled = true;
        btnExport.Enabled = true;
        bntCommitUpload.Enabled = false;
        btnDelete.Enabled = false;
    }

    protected void btnDeleteM_Click(object sender, EventArgs e)
    {
        if (Session["gvMasterORD08"] != null)
        {
            DataTable dtMaster = Session["gvMasterORD08"] as DataTable;
            DataTable dtDetail = Session["gvDetailORD08"] as DataTable;

            List<object> keyValues = this.gvMaster.GetSelectedFieldValues(gvMaster.KeyFieldName);
            foreach (string skey in keyValues)
            {
                DataRow[] drD = dtDetail.Select("HQ_ORDER_D = '" + skey + "'");
                foreach (DataRow dr in drD)
                {
                    dtDetail.Rows.Remove(dr);
                }

                DataRow[] drM = dtMaster.Select("HQ_ORDER_D = '" + skey + "'");
                if (drM.Length > 0)
                {
                    dtMaster.Rows.Remove(drM[0]);
                }

                dtMaster.AcceptChanges();
                dtDetail.AcceptChanges();
            }

            Session["gvMasterORD08"] = dtMaster;
            Session["gvDetailORD08"] = dtDetail;

            BindMasterData();
            BindDetailData();
        }
    }

    protected void btnDeleteD_Click(object sender, EventArgs e)
    {
        if (Session["gvDetailORD08"] != null)
        {
            DataTable dtDetail = Session["gvDetailORD08"] as DataTable;

            List<object> keyValues = this.gvDetail.GetSelectedFieldValues(gvDetail.KeyFieldName);
            foreach (string skey in keyValues)
            {
                DataRow[] dr = dtDetail.Select("HQ_ORDER_STORE = '" + skey + "'");
                if (dr.Length > 0)
                {
                    dtDetail.Rows.Remove(dr[0]);
                }

                dtDetail.AcceptChanges();
            }

            Session["gvDetailORD08"] = dtDetail;

            string strAUTO_DIS_FLAG = StringUtil.CStr(gvMaster.GetRowValues(gvMaster.FocusedRowIndex, "AUTO_DIS_FLAG"));
            AssignQTY((strAUTO_DIS_FLAG == "true" ? true : false), false);

            BindMasterData();
            BindDetailData();

        }
    }

    protected void btnConfirm_Click(object sender, EventArgs e)
    {
        if (gvMaster.FocusedRowIndex > -1)
        {
            string HQ_ORDER_D = StringUtil.CStr(gvMaster.GetRowValues(gvMaster.FocusedRowIndex, gvMaster.KeyFieldName));
            string strProdNo = StringUtil.CStr(gvMaster.GetRowValues(gvMaster.FocusedRowIndex, "PRODNO"));

            ASPxComboBox ddlZone = gvDetail.FindTitleTemplateControl("ddlZone") as ASPxComboBox;
            if (ddlZone.SelectedItem != null)
            {
                string strZone = StringUtil.CStr(ddlZone.SelectedItem.Value);
                //if (!string.IsNullOrEmpty(strZone))
                //{
                DataTable dtStore = new ORD08_Facade().AddNew_HqNDSOrderStore(HQ_ORDER_D, StringUtil.CStr(ddlZone.SelectedItem.Value));

                if (dtStore.Rows.Count > 0)
                {
                    DataTable dt = Session["gvDetailORD08"] as DataTable;
                    foreach (DataRow drStore in dtStore.Rows)
                    {
                        //查詢相同的商品料號及主配門市，但key不相同
                        string expression = "PRODNO = '" + strProdNo + "' AND STORE_NO = '" + StringUtil.CStr(drStore["STORE_NO"]) + "'";
                        DataRow[] data = dt.Select(expression);
                        if (data.Length <= 0)  //Session沒有相同的資料，才可新增此主配門市
                        {
                            DataRow dr = dt.NewRow();
                            dr["HQ_ORDER_STORE"] = GuidNo.getUUID();
                            dr["HQ_ORDER_D"] = gvMaster.GetRowValues(gvMaster.FocusedRowIndex, gvMaster.KeyFieldName);
                            dr["LOC_ID"] = StringUtil.CStr(drStore["BRANCHNO"]);
                            dr["STORE_NO"] = StringUtil.CStr(drStore["STORE_NO"]);
                            dr["ASSIGN_QTY"] = "0";
                            dr["PRODNO"] = gvMaster.GetRowValues(gvMaster.FocusedRowIndex, "PRODNO");
                            dr["STORENAME"] = StringUtil.CStr(drStore["STORENAME"]);
                            dr["WEIGHT"] = StringUtil.CStr(drStore["WEIGHT"]);
                            dt.Rows.Add(dr);
                            dt.AcceptChanges();
                        }
                    }

                    Session["gvDetailORD08"] = dt;
                    ASPxCheckBox cbADF = gvMaster.FindRowCellTemplateControl(gvMaster.FocusedRowIndex, (GridViewDataColumn)gvMaster.Columns["AUTO_DIS_FLAG"], "lblchkADF") as ASPxCheckBox;
                    AssignQTY(cbADF.Checked, false);//如果有勾選自動分配，則要將主配門市的分配數量重新計算

                    //彈跳視窗
                    ScriptManager.RegisterClientScriptBlock(this, GetType(), "AlertMessage", "alert('新增成功!!');", true);

                }
                else
                {
                    //彈跳視窗
                    ScriptManager.RegisterClientScriptBlock(this, GetType(), "AlertMessage", "alert('此區域的門市無分配權重!!');", true);
                    return;
                }
                BindDetailData();
                //}
            }
            this.gvDetail.Selection.UnselectAll();
            this.gvDetail.PageIndex = 0;
            divShow.Visible = true;
        }
    }

    #endregion

    #region gvMaster 觸發的事件

    protected void gvMaster_PageIndexChanged(object sender, EventArgs e)
    {
        BindMasterData();
        gvMasterStatus();
        gvDetail.DataSource = null;
        gvDetail.DataBind();
        gvDetailStatus();
    }

    protected void gvMaster_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
    {
        if (Session["gvMasterORD08"] != null)
        {
            ASPxGridView grid = (ASPxGridView)sender;
            ASPxCheckBox cbADF = (ASPxCheckBox)gvMaster.FindEditRowCellTemplateControl((GridViewDataColumn)gvMaster.Columns["AUTO_DIS_FLAG"], "chkADF");
            ASPxTextBox txtDisQty = (ASPxTextBox)gvMaster.FindEditRowCellTemplateControl((GridViewDataColumn)gvMaster.Columns["DIS_QTY"], "txtDisQty");
            if (StringUtil.CStr(cbADF.Checked).ToLower() != StringUtil.CStr(e.OldValues["AUTO_DIS_FLAG"]).ToLower())
            {
                txtDisQty.Text = (cbADF.Checked ? txtDisQty.Text.Trim() : "");
                AssignQTY(cbADF.Checked, true);//如果有勾選自動分配，則要將主配門市的分配數量重新計算
            }

            string[] strErr = getProductInfo(StringUtil.CStr(e.NewValues["PRODNO"])).Split(';');

            DataTable dt = Session["gvMasterORD08"] as DataTable;
            DataRow[] dr = dt.Select("HQ_ORDER_D = '" + StringUtil.CStr(e.Keys["HQ_ORDER_D"]) + "'");
            if (dr.Length > 0)
            {
                dr[0]["PRODNO"] = StringUtil.CStr(e.NewValues["PRODNO"]);
                dr[0]["PRODNAME"] = strErr[0];
                dr[0]["ATR_QTY"] = strErr[1];
                dr[0]["AUTO_DIS_FLAG"] = (cbADF.Checked ? "true" : "false");
                dr[0]["DIS_QTY"] = txtDisQty.Text.Trim();
                dr[0]["REMARK"] = (e.NewValues["REMARK"] == null ? "" : StringUtil.CStr(e.NewValues["REMARK"]));

                dt.AcceptChanges();
            }

            if (StringUtil.CStr(e.NewValues["PRODNO"]) != StringUtil.CStr(e.OldValues["PRODNO"]) && Session["gvDetailORD08"] != null)
            {
                DataTable dtDetail = Session["gvDetailORD08"] as DataTable;
                string strWhere = "HQ_ORDER_D = '" + StringUtil.CStr(e.Keys["HQ_ORDER_D"]) + "'";
                DataRow[] drDs = dtDetail.Select(strWhere);

                foreach (DataRow drD in drDs)
                {
                    drD["PRODNO"] = StringUtil.CStr(e.NewValues["PRODNO"]);
                    dtDetail.AcceptChanges();
                }

                Session["gvDetailORD08"] = dtDetail;
            }

            grid.CancelEdit();
            e.Cancel = true;

            Session["gvMasterORD08"] = dt;
            BindMasterData();
            BindDetailData();

        }
    }

    protected void gvMaster_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
    {
        if (Session["gvMasterORD08"] != null)
        {
            ASPxGridView grid = (ASPxGridView)sender;
            ASPxTextBox txtDisQty = (ASPxTextBox)gvMaster.FindEditRowCellTemplateControl((GridViewDataColumn)gvMaster.Columns["DIS_QTY"], "txtDisQty");
            ASPxCheckBox cbADF = (ASPxCheckBox)gvMaster.FindEditRowCellTemplateControl((GridViewDataColumn)gvMaster.Columns["AUTO_DIS_FLAG"], "chkADF");
            txtDisQty.Text = (cbADF.Checked ? txtDisQty.Text.Trim() : "");

            AssignQTY(cbADF.Checked, true); //如果有勾選自動分配，則要將主配門市的分配數量重新計算

            string[] strErr = getProductInfo(StringUtil.CStr(e.NewValues["PRODNO"])).Split(';');

            DataTable dt = Session["gvMasterORD08"] as DataTable;
            DataRow dr = dt.NewRow();
            dr["HQ_ORDER_M_ID"] = hdNDS_MID_UUID.Text;
            dr["HQ_ORDER_D"] = GuidNo.getUUID();
            dr["HQ_NDS_ORDER_NO"] = NDS_NO;
            dr["PRODNO"] = StringUtil.CStr(e.NewValues["PRODNO"]);
            dr["PRODNAME"] = strErr[0];
            dr["ATR_QTY"] = strErr[1];
            dr["AUTO_DIS_FLAG"] = (cbADF.Checked ? "true" : "false");
            dr["DIS_QTY"] = string.IsNullOrEmpty(txtDisQty.Text.Trim()) ? "0" : txtDisQty.Text.Trim();
            dr["REMARK"] = (e.NewValues["REMARK"] == null ? "" : StringUtil.CStr(e.NewValues["REMARK"]));
            dt.Rows.Add(dr);
            dt.AcceptChanges();

            grid.CancelEdit();
            e.Cancel = true;
            Session["gvMasterORD08"] = dt;
            BindMasterData();
            BindDetailData();

        }
    }

    protected void gvMaster_FocusedRowChanged(object sender, EventArgs e)
    {
        if (gvMaster.IsEditing)
        {
            gvMaster.FocusedRowIndex = -1;
        }
        else
        {
            if (gvMaster.FocusedRowIndex > -1)
            {
                gvMasterStatus();
                BindDetailData();
            }
        }
    }

    protected void gvMaster_RowValidating(object sender, DevExpress.Web.Data.ASPxDataValidationEventArgs e)
    {
        DataTable dt = Session["gvMasterORD08"] as DataTable;

        //查詢相同的商品料號，但key不相同
        PopupControl txtProd = gvMaster.FindEditRowCellTemplateControl((GridViewDataColumn)gvMaster.Columns["PRODNO"], "txtProd") as PopupControl;

        string expression = "PRODNO = '" + txtProd.Text + "'";
        if (e.Keys["HQ_ORDER_D"] != null)
        {
            expression += " AND HQ_ORDER_D <> '" + StringUtil.CStr(e.Keys["HQ_ORDER_D"]) + "'";
        }
        DataRow[] data = dt.Select(expression);
        if (data.Length > 0)  //有查詢到資料，表示商品料號重複
        {
            e.RowError = "此商品已存在此主配單中，請輸入其他商品";
            return;
        }

        string[] strErr = getProductInfo(txtProd.Text).Split(';');
        if (!string.IsNullOrEmpty(strErr[strErr.Length - 1]))
        {
            e.RowError = strErr[strErr.Length - 1];
            return;
        }

    }

    protected void gvMaster_InitNewRow(object sender, DevExpress.Web.Data.ASPxDataInitNewRowEventArgs e)
    {
        ASPxTextBox tbDisQty = gvMaster.FindEditRowCellTemplateControl((GridViewDataColumn)gvMaster.Columns["DIS_QTY"], "txtDisQty") as ASPxTextBox;
        tbDisQty.ClientEnabled = false;

        ASPxCheckBox cbADF = gvMaster.FindEditRowCellTemplateControl((GridViewDataColumn)gvMaster.Columns["AUTO_DIS_FLAG"], "chkADF") as ASPxCheckBox;
        cbADF.Checked = false;

        tbDisQty.Text = "0";

        if (gvDetail.IsEditing)
        {
            gvDetail.CancelEdit();
        }
    }

    protected void gvMaster_StartRowEditing(object sender, DevExpress.Web.Data.ASPxStartRowEditingEventArgs e)
    {
        if (gvDetail.IsEditing)
        {
            gvDetail.CancelEdit();
        }

        gvMaster.Selection.UnselectAll();
    }

    protected void gvMaster_HtmlRowCreated(object sender, ASPxGridViewTableRowEventArgs e)
    {
        if (e.RowType == GridViewRowType.Data)
        {
            ASPxCheckBox cbADF = gvMaster.FindRowCellTemplateControl(e.VisibleIndex, (GridViewDataColumn)gvMaster.Columns["AUTO_DIS_FLAG"], "lblchkADF") as ASPxCheckBox;
            cbADF.ClientEnabled = false;

        }

        if (e.VisibleIndex == gvMaster.FocusedRowIndex && e.RowType == GridViewRowType.InlineEdit)
        {
            bool chk = bool.Parse(StringUtil.CStr(e.GetValue("AUTO_DIS_FLAG")));

            ASPxCheckBox cbADF = gvMaster.FindEditRowCellTemplateControl((GridViewDataColumn)gvMaster.Columns["AUTO_DIS_FLAG"], "chkADF") as ASPxCheckBox;
            cbADF.Checked = chk;

            if (!chk)
            {
                ASPxTextBox txtDISQty = gvMaster.FindEditRowCellTemplateControl((GridViewDataColumn)gvMaster.Columns["DIS_QTY"], "txtDisQty") as ASPxTextBox;
                txtDISQty.ClientEnabled = false;
            }

        }

        if (e.RowType == GridViewRowType.EditingErrorRow)
        {
            ASPxCheckBox cbADF = gvMaster.FindEditRowCellTemplateControl((GridViewDataColumn)gvMaster.Columns["AUTO_DIS_FLAG"], "chkADF") as ASPxCheckBox;

            if (!cbADF.Checked)
            {
                ASPxTextBox txtDISQty = gvMaster.FindEditRowCellTemplateControl((GridViewDataColumn)gvMaster.Columns["DIS_QTY"], "txtDisQty") as ASPxTextBox;
                txtDISQty.ClientEnabled = false;
            }

        }
    }

    protected void gvMaster_HtmlRowPrepared(object sender, ASPxGridViewTableRowEventArgs e)
    {
        if (e.RowType == GridViewRowType.Data)
        {
            ASPxCheckBox chkADF = gvMaster.FindRowCellTemplateControl(e.VisibleIndex, (GridViewDataColumn)gvMaster.Columns["AUTO_DIS_FLAG"], "lblchkADF") as ASPxCheckBox;
            if (chkADF != null)
            {
                chkADF.Checked = Convert.ToBoolean(StringUtil.CStr(e.GetValue("AUTO_DIS_FLAG")));
            }
        }
    }

    protected void gvMaster_PreRender(object sender, EventArgs e)
    {
        gvMasterStatus();
    }

    protected void gvMaster_CommandButtonInitialize(object sender, ASPxGridViewCommandButtonEventArgs e)
    {
        if (e.ButtonType == ColumnCommandButtonType.Edit || e.ButtonType == ColumnCommandButtonType.Update || e.ButtonType == ColumnCommandButtonType.Cancel)
        {
            e.Visible = false;
        }
    }

    #endregion

    #region gvDetail 觸發的事件

    protected void gvDetail_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
    {
        if (Session["gvDetailORD08"] != null)
        {
            ASPxGridView grid = (ASPxGridView)sender;
            DataTable dt = Session["gvDetailORD08"] as DataTable;

            string[] returnData = getStoreInfo(StringUtil.CStr(e.NewValues["STORE_NO"])).Split(';');

            DataRow[] dr = dt.Select("HQ_ORDER_STORE = '" + StringUtil.CStr(e.Keys["HQ_ORDER_STORE"]) + "'");
            dr[0]["LOC_ID"] = returnData[3];
            dr[0]["STORE_NO"] = e.NewValues["STORE_NO"];
            dr[0]["STORENAME"] = returnData[2];
            dr[0]["ASSIGN_QTY"] = e.NewValues["ASSIGN_QTY"];
            dr[0]["PRODNO"] = gvMaster.GetRowValues(gvMaster.FocusedRowIndex, "PRODNO");
            dr[0]["WEIGHT"] = returnData[4];
            dt.AcceptChanges();

            grid.CancelEdit();
            e.Cancel = true;

            Session["gvDetailORD08"] = dt;

            string strAUTO_DIS_FLAG = StringUtil.CStr(gvMaster.GetRowValues(gvMaster.FocusedRowIndex, "AUTO_DIS_FLAG"));
            AssignQTY((strAUTO_DIS_FLAG == "true" ? true : false), false);

            BindMasterData();
            BindDetailData();
        }
    }

    protected void gvDetail_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
    {
        if (Session["gvDetailORD08"] != null)
        {
            ASPxGridView grid = (ASPxGridView)sender;

            DataTable dt = Session["gvDetailORD08"] as DataTable;

            string[] returnData = getStoreInfo(StringUtil.CStr(e.NewValues["STORE_NO"])).Split(';');

            DataRow dr = dt.NewRow();
            dr["HQ_ORDER_STORE"] = GuidNo.getUUID();
            dr["HQ_ORDER_D"] = gvMaster.GetRowValues(gvMaster.FocusedRowIndex, gvMaster.KeyFieldName);
            dr["LOC_ID"] = returnData[3];
            dr["STORE_NO"] = StringUtil.CStr(e.NewValues["STORE_NO"]);
            dr["ASSIGN_QTY"] = StringUtil.CStr(e.NewValues["ASSIGN_QTY"]);
            dr["PRODNO"] = gvMaster.GetRowValues(gvMaster.FocusedRowIndex, "PRODNO");
            dr["STORENAME"] = returnData[2];
            dr["WEIGHT"] = returnData[4];
            dt.Rows.Add(dr);
            dt.AcceptChanges();

            grid.CancelEdit();
            e.Cancel = true;

            Session["gvDetailORD08"] = dt;
            string strAUTO_DIS_FLAG = StringUtil.CStr(gvMaster.GetRowValues(gvMaster.FocusedRowIndex, "AUTO_DIS_FLAG"));
            AssignQTY((strAUTO_DIS_FLAG == "true" ? true : false), false);

            BindMasterData();
            BindDetailData();

        }
    }

    protected void gvDetail_HtmlRowCreated(object sender, ASPxGridViewTableRowEventArgs e)
    {

        if (e.RowType == GridViewRowType.InlineEdit)
        {
            //主配商品的自動分配有勾選，則主配門市的主動配貨量不能讓User手動輸入，系統自動計算
            if (gvMaster.GetRowValues(gvMaster.FocusedRowIndex, "AUTO_DIS_FLAG") != null && StringUtil.CStr(gvMaster.GetRowValues(gvMaster.FocusedRowIndex, "AUTO_DIS_FLAG")) == "true")
            {
                ASPxTextBox txtASSIGN_QTY = gvDetail.FindEditRowCellTemplateControl((GridViewDataColumn)gvDetail.Columns["ASSIGN_QTY"], "txtASSIGN_QTY") as ASPxTextBox;
                if (txtASSIGN_QTY != null)
                {
                    txtASSIGN_QTY.ClientEnabled = false;
                }
            }
        }
    }

    protected void gvDetail_PageIndexChanged(object sender, EventArgs e)
    {
        DataTable dt = Session["gvDetailORD08"] as DataTable;
        BindDetailData();
    }

    protected void gvDetail_InitNewRow(object sender, DevExpress.Web.Data.ASPxDataInitNewRowEventArgs e)
    {
        BindDetailData();

        e.NewValues["ASSIGN_QTY"] = 0;
        if (gvMaster.IsEditing)
        {
            gvMaster.CancelEdit();
        }

    }

    protected void gvDetail_RowValidating(object sender, DevExpress.Web.Data.ASPxDataValidationEventArgs e)
    {
        DataTable dt = Session["gvDetailORD08"] as DataTable;

        string strProdNo = StringUtil.CStr(gvMaster.GetRowValues(gvMaster.FocusedRowIndex, "PRODNO"));
        //查詢相同的商品料號及主配門市，但key不相同
        string expression = "PRODNO = '" + strProdNo + "' AND STORE_NO = '" + StringUtil.CStr(e.NewValues["STORE_NO"]) + "'";
        if (e.Keys["HQ_ORDER_STORE"] != null)
        {
            expression += " AND HQ_ORDER_STORE <> '" + StringUtil.CStr(e.Keys["HQ_ORDER_STORE"]) + "'";
        }
        DataRow[] data = dt.Select(expression);
        if (data.Length > 0)  //有查詢到資料，表示主配門市重複
        {
            e.RowError += "主配門市重複!!";
            return;
        }

        //取得該門市的狀態：1 暫停營業, 2 已關閉
        int iStore = new Store_Facade().Query_StoreStatus(StringUtil.CStr(e.NewValues["STORE_NO"]));
        switch (iStore)
        {
            case 1:
                e.RowError += "此門市暫停營業";
                return;

            case 2:
                e.RowError += "此門市已關閉";
                return;
        }


        //主配商品的自動分配有勾選，才檢查
        if (gvMaster.GetRowValues(gvMaster.FocusedRowIndex, "AUTO_DIS_FLAG") != null && StringUtil.CStr(gvMaster.GetRowValues(gvMaster.FocusedRowIndex, "AUTO_DIS_FLAG")) == "true")
        {
            string returnData = getStoreInfo(StringUtil.CStr(e.NewValues["STORE_NO"]));
            if (string.IsNullOrEmpty(returnData))
            {
                e.RowError += "此門市權重佔比沒有設定";
                return;
            }
        }

    }

    protected void gvDetail_PreRender(object sender, EventArgs e)
    {
        gvDetailStatus();
        BindZoneType();
    }

    protected void gvDetail_StartRowEditing(object sender, DevExpress.Web.Data.ASPxStartRowEditingEventArgs e)
    {
        BindDetailData();

        if (gvMaster.IsEditing)
        {
            gvMaster.CancelEdit();
        }

        gvDetail.Selection.UnselectAll();

    }

    protected void gvDetail_CommandButtonInitialize(object sender, ASPxGridViewCommandButtonEventArgs e)
    {
        if (e.ButtonType == ColumnCommandButtonType.Edit || e.ButtonType == ColumnCommandButtonType.Update || e.ButtonType == ColumnCommandButtonType.Cancel)
        {
            e.Visible = false;
        }
    }

    #endregion

    protected void chkADF_CheckedChanged(object sender, EventArgs e)
    {

    }

    private void AssignQTY(bool IsAutoDISFlag, bool IsCheckBox)
    {
        if (gvMaster.FocusedRowIndex > -1)
        {
            DataTable dtM = Session["gvMasterORD08"] as DataTable;
            DataTable dtD = Session["gvDetailORD08"] as DataTable;

            ASPxTextBox txtDISQty = gvMaster.FindRowCellTemplateControl(gvMaster.FocusedRowIndex, (GridViewDataColumn)gvMaster.Columns["DIS_QTY"], "txtDisQty") as ASPxTextBox;
            if (txtDISQty == null)
            {
                txtDISQty = gvMaster.FindEditRowCellTemplateControl((GridViewDataColumn)gvMaster.Columns["DIS_QTY"], "txtDisQty") as ASPxTextBox;
            }
            Int64 DIS_QTY = Convert.ToInt64(IsAutoDISFlag ? txtDISQty.Text : "0");

            string strProdNo = StringUtil.CStr(gvMaster.GetRowValues(gvMaster.FocusedRowIndex, "PRODNO"));
            var drWeight = dtD.Select("PRODNO = '" + strProdNo + "'").OrderByDescending(r => r["WEIGHT"]); //數字愈小，表示權重愈小

            if (IsAutoDISFlag)  //主配商品有勾選自動分配
            {

                if (drWeight.Count() > 0)  //有主配門市資料，才需計算分配數量
                {
                    foreach (DataRow dr in drWeight)
                    {
                        dr["ASSIGN_QTY"] = 0;  //先將原本的數量歸零
                    }

                    while (DIS_QTY > 0)  //當還有剩餘的數量，就計算累加
                    {
                        foreach (DataRow dr in drWeight)
                        {
                            if (DIS_QTY > 0)
                            {
                                dr["ASSIGN_QTY"] = Convert.ToInt64(StringUtil.CStr(dr["ASSIGN_QTY"])) + 1;
                                DIS_QTY -= 1;

                                dtD.AcceptChanges();

                            }
                            else  //主配商品的數量已扣完，就不再繼續計算累加
                            {
                                break;
                            }
                        }
                    }
                } //end-if (dt.Rows.Count > 0)
            }//end=-if (IsAutoDISFlag)
            else  //主配商品沒有勾選自動分配，則1. 主配量改為0，主配門市的數量也變更為0； 2. 主配商品的主配量 = 主配門市的總數量
            {
                if (IsCheckBox)
                {
                    if (drWeight.Count() > 0)  //有主配門市資料，才需將分配數量歸零
                    {
                        foreach (DataRow dr in drWeight)
                        {
                            dr["ASSIGN_QTY"] = "0";
                            dtD.AcceptChanges();
                        }
                    }
                }

                string strDISQty = StringUtil.CStr(dtD.Compute("Sum(ASSIGN_QTY)", "PRODNO = '" + StringUtil.CStr(gvMaster.GetRowValues(gvMaster.FocusedRowIndex, "PRODNO")) + "'"));

                DataRow[] drD = dtM.Select("HQ_ORDER_D = '" + StringUtil.CStr(gvMaster.GetRowValues(gvMaster.FocusedRowIndex, "HQ_ORDER_D")) + "'");
                drD[0]["DIS_QTY"] = (string.IsNullOrEmpty(strDISQty) ? "0" : strDISQty);
                dtM.AcceptChanges();
                txtDISQty.Text = string.IsNullOrEmpty(strDISQty) ? "0" : strDISQty;


            }

            Session["gvMasterORD08"] = dtM;
            Session["gvDetailORD08"] = dtD;
        }
    }

    private void SavaData(string HQ_NDS_ORDER_NO, string STATUS)
    {
        DataTable dtMaster = Session["gvMasterORD08"] as DataTable;
        DataTable dtDetail = Session["gvDetailORD08"] as DataTable;

        ORD08_HQNDSORDERMSet_DTO ORD08_DTO = new ORD08_HQNDSORDERMSet_DTO();
        ORD08_HQNDSORDERMSet_DTO.HQ_NDS_ORDER_MDataTable dtH = ORD08_DTO.HQ_NDS_ORDER_M;
        ORD08_HQNDSORDERMSet_DTO.HQ_NDS_ORDER_DDataTable dtD = ORD08_DTO.HQ_NDS_ORDER_D;
        ORD08_HQNDSORDERMSet_DTO.HQ_NDS_ORDER_STOREDataTable dtS = ORD08_DTO.HQ_NDS_ORDER_STORE;

        ORD08_HQNDSORDERMSet_DTO.HQ_NDS_ORDER_MRow drH = dtH.NewHQ_NDS_ORDER_MRow();

        DataRow dirtyDr = null;
        if (!string.IsNullOrEmpty(this.lblNDS_MID.Text))  //單號不為空，表示在Table裡已有資料
        {
            dirtyDr = ORD08_PageHelper.Query_HQNDSORDERM_ByKey(this.lblNDS_MID.Text).Rows[0];
        }

        drH.HQ_NDS_ORDER_NO = HQ_NDS_ORDER_NO;
        drH.HQ_ORDER_M_ID = StringUtil.CStr(dtMaster.Rows[0]["HQ_ORDER_M_ID"]);
        drH.MODI_DTM = System.DateTime.Now;
        drH.MODI_USER = logMsg.OPERATOR;
        if (string.IsNullOrEmpty(this.lblNDS_MID.Text))  //單號為空表示是新資料
        {
            drH.CREATE_DTM = System.DateTime.Now;
            drH.CREATE_USER = logMsg.OPERATOR;
        }
        else
        {
            drH.CREATE_USER = StringUtil.CStr(dirtyDr["CREATE_USER"]);
            drH.CREATE_DTM = Convert.ToDateTime(StringUtil.CStr(dirtyDr["CREATE_DTM"]));
        }
        drH.STATUS = STATUS; //狀態 1:已存檔 2:已上傳 3.已轉門市訂單

        dtH.Rows.Add(drH);

        foreach (DataRow dr in dtMaster.Rows)
        {
            ORD08_HQNDSORDERMSet_DTO.HQ_NDS_ORDER_DRow drD = dtD.NewHQ_NDS_ORDER_DRow();

            string strUser = logMsg.OPERATOR;
            DateTime dtCreate = System.DateTime.Now;

            if (!string.IsNullOrEmpty(this.lblNDS_MID.Text))  //單號不為空，表示在Table裡已有資料
            {
                DataTable dtTemp = ORD08_PageHelper.Query_HQNDSORDERD_ByKey(StringUtil.CStr(dr["HQ_ORDER_D"]));
                if (dtTemp.Rows.Count > 0)
                {
                    DataRow dirtyDr_D = dtTemp.Rows[0];
                    strUser = StringUtil.CStr(dirtyDr_D["CREATE_USER"]);
                    dtCreate = Convert.ToDateTime(StringUtil.CStr(dirtyDr_D["CREATE_DTM"]));
                }
            }

            drD.HQ_ORDER_D = StringUtil.CStr(dr["HQ_ORDER_D"]);
            drD.HQ_ORDER_M_ID = StringUtil.CStr(dr["HQ_ORDER_M_ID"]);
            drD.ATR_QTY = Convert.ToInt64(StringUtil.CStr(dr["ATR_QTY"]));
            drD.AUTO_DIS_FLAG = StringUtil.CStr(dr["AUTO_DIS_FLAG"]) == "true" ? "1" : "0";
            drD.DIS_QTY = Convert.ToInt64(StringUtil.CStr(dr["DIS_QTY"]));
            drD.REMARK = StringUtil.CStr(dr["REMARK"]);
            drD.PRODNO = StringUtil.CStr(dr["PRODNO"]);
            drD.CREATE_USER = strUser;
            drD.CREATE_DTM = dtCreate;
            drD.MODI_USER = logMsg.OPERATOR;     //異動人員
            drD.MODI_DTM = System.DateTime.Now;  //異動時間

            dtD.Rows.Add(drD);
        }

        foreach (DataRow dr in dtDetail.Rows)
        {
            ORD08_HQNDSORDERMSet_DTO.HQ_NDS_ORDER_STORERow drS = dtS.NewHQ_NDS_ORDER_STORERow();

            string strUser = logMsg.OPERATOR;
            DateTime dtCreate = System.DateTime.Now;

            if (!string.IsNullOrEmpty(this.lblNDS_MID.Text))  //單號不為空，表示在Table裡已有資料
            {
                DataTable dtTemp = ORD08_PageHelper.Query_HQNDSORDERS_ByKey(StringUtil.CStr(dr["HQ_ORDER_STORE"]));
                if (dtTemp.Rows.Count > 0)
                {
                    DataRow dirtyDr_S = dtTemp.Rows[0];
                    strUser = StringUtil.CStr(dirtyDr_S["CREATE_USER"]);
                    dtCreate = Convert.ToDateTime(StringUtil.CStr(dirtyDr_S["CREATE_DTM"]));
                }
            }

            drS.HQ_ORDER_STORE = StringUtil.CStr(dr["HQ_ORDER_STORE"]);
            drS.ASSIGN_QTY = Convert.ToInt64(StringUtil.CStr(dr["ASSIGN_QTY"]));
            drS.CREATE_USER = strUser;
            drS.CREATE_DTM = dtCreate;
            drS.MODI_USER = logMsg.OPERATOR;     //異動人員
            drS.MODI_DTM = System.DateTime.Now;  //異動時間
            drS.STORE_NO = StringUtil.CStr(dr["STORE_NO"]);
            drS.LOC_ID = StringUtil.CStr(dr["LOC_ID"]);
            drS.HQ_ORDER_D = StringUtil.CStr(dr["HQ_ORDER_D"]);
            drS.STATUS = STATUS; //狀態 1:已存檔 2:已上傳 3.已轉門市訂單
            drS.PRODNO = StringUtil.CStr(dr["PRODNO"]);

            dtS.Rows.Add(drS);
        }

        ORD08_DTO.AcceptChanges();
        ORD08_Facade facade = new ORD08_Facade();

        //更新資料庫
        if (!string.IsNullOrEmpty(this.lblNDS_MID.Text))
        {
            facade.Update_NDSMMethodData(ORD08_DTO, STATUS, (DataTable)Session["gvMasterOLD"]);
            //Delete + Insert = Update
        }
        else   //否則進行Insert
        {
            facade.AddNew_NDSMMethodData(ORD08_DTO, STATUS);
        }

        GetNDS_Status(HQ_NDS_ORDER_NO);
        //this.lblNDS_MID.Text = HQ_NDS_ORDER_NO;
        //lblNDS_Status.Text = "01 已存檔";

    }

    //ajax 呼當前網頁的方式
    [System.Web.Services.WebMethod()]
    [System.Web.Script.Services.ScriptMethod()]
    static public string getProductInfo(string PRODNO)
    {
        string strInfo = "";
        if (!string.IsNullOrEmpty(PRODNO))
        {
            DataTable dt = ORD08_PageHelper.GetNDSMasterMethodDataProdInfo(PRODNO);

            if (dt.Rows.Count > 0)
            {
                strInfo = StringUtil.CStr(dt.Rows[0]["PRODNAME"]) + ";" + StringUtil.CStr(dt.Rows[0]["ATRQTY"]) + ";" + StringUtil.CStr(dt.Rows[0]["ERROR"]);
            }
        }

        return strInfo;

   
    }

    //ajax 呼當前網頁的方式
    [System.Web.Services.WebMethod()]
    [System.Web.Script.Services.ScriptMethod()]
    static public string getStoreInfo(string STORENO)
    {
        string strInfo = "";
        if (!string.IsNullOrEmpty(STORENO))
        {
            string strSTORENAME = "";
            string strBRANCHNO = "";
            float  iWEIGHT = 0;
            int iStore = new Store_Facade().Query_StoreStatus(STORENO);
            DataTable dt = new Store_Facade().Query_StoreWEIGHT(STORENO);

            if (dt.Rows.Count > 0)
            {
                strSTORENAME = StringUtil.CStr(dt.Rows[0]["STORENAME"]);
                strBRANCHNO = StringUtil.CStr(dt.Rows[0]["BRANCHNO"]);
                iWEIGHT = float.Parse(StringUtil.CStr(dt.Rows[0][3]));

            }
            strInfo = StringUtil.CStr(iStore) + ";" + StringUtil.CStr(dt.Rows.Count) + ";" + strSTORENAME + ";" + strBRANCHNO + ";" + StringUtil.CStr(iWEIGHT);

        }

        return strInfo;
    }

    //ajax 呼當前網頁的方式
    [System.Web.Services.WebMethod()]
    [System.Web.Script.Services.ScriptMethod()]
    static public bool checkWeightInfo(bool IsChecked)
    {
        bool bInfo = true;
        if (IsChecked)
        {
            DataTable dtWEIGHT = ORD08_PageHelper.GetStoreWeightDistributeInfo();

            if (dtWEIGHT.Rows.Count > 0)
            {
                bInfo = false;
            }
        }
        return bInfo;
    }

}
