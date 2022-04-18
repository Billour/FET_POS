using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using DevExpress.Web.ASPxGridView;
using DevExpress.Web.ASPxEditors;
using iTextSharp.text;
using iTextSharp.text.pdf;

using Advtek.Utility;
using FET.POS.Model.Common;
using FET.POS.Model.DTO;
using FET.POS.Model.Facade;
using FET.POS.Model.Facade.FacadeImpl;
using AdvTek.CustomControls;
using System.Collections.Specialized;

public partial class VSS_INV_INV09 : BasePage
{

    private string msgStr = "";

    //驗收單編號
    private string _INVNO = "";
    //是否可以編輯
    private string EDIT_STATUS = "";

    private string qPOOE_NO;
    private string qReceivingNo;
    private string qSTATUS;
    private string qORDER_NO;
    private string qSTORE_NO;
    private void GetRequestString()
    {

        //**2011/04/21 Kristy：前頁傳遞參數時，會先以加密處理，在此要解密。
        if (!string.IsNullOrEmpty(Request.QueryString["Param"]))
        {
            NameValueCollection qscoll = Utils.Param_UrlDecode(Request.QueryString["Param"]);
            foreach (string key in qscoll.AllKeys)
            {
                if (key == "POOE_NO")
                {
                    qPOOE_NO = string.Join(",", qscoll.GetValues(key));
                }
                else if (key == "ReceivingNo")
                {
                    qReceivingNo = string.Join(",", qscoll.GetValues(key));
                }
                else if (key == "STATUS")
                {
                    qSTATUS = string.Join(",", qscoll.GetValues(key));
                }
                else if (key == "ORDER_NO")
                {
                    qORDER_NO = string.Join(",", qscoll.GetValues(key));
                }
                else if (key == "STORE_NO")
                {
                    qSTORE_NO = string.Join(",", qscoll.GetValues(key));
                }
              
            }
        }

    }
    protected void Page_Load(object sender, EventArgs e)
    {
        GetRequestString();
        doChangeMaster();

        string dno = qPOOE_NO;
        this.ViewState["PO/OE_NO"] = dno;
        string ReNo =  qReceivingNo.Trim();
        EDIT_STATUS =  qSTATUS.Trim();
        this.ViewState["ReceivingNo"] = ReNo;
        string ORDER_NO = qORDER_NO.Trim();
        string STORE_NO = qSTORE_NO.Trim();
        if (!IsPostBack)
        {
            INV08_Facade _INV08_Facade = new INV08_Facade();

            Session["N_STORE"] = logMsg.STORENO.Trim();
            this.MUUID.Value = GuidNo.getUUID();
            if (EDIT_STATUS == "10")
            {
                ViewState["GetINVM"] = _INV08_Facade.GetINVM("", StringUtil.CStr(this.ViewState["PO/OE_NO"]), ORDER_NO, STORE_NO);
                doSelectINVM();

                this.btnSave.Enabled = true;
                this.btnCancel.Enabled = true;
                this.btnPrBarCo.Enabled = false;

                Session["gvMaster"] = _INV08_Facade.GetINVD_POOENO(this.labPooeNo.Text, ORDER_NO, STORE_NO);
                gvMaster.DataSource = Session["gvMaster"];
                gvMaster.DataBind();
                this.labReceivingNo.Text = "";
            }
            else
            {
                this.labReceivingNo.Text = ReNo;

                ViewState["GetINVM"] = _INV08_Facade.GetINVM(this.labReceivingNo.Text, StringUtil.CStr(this.ViewState["PO/OE_NO"]), ORDER_NO, STORE_NO);
                doSelectINVM();

                //Bind_Detail
                Session["gvMaster"] = _INV08_Facade.GetINVD_INVANO(this.labReceivingNo.Text);
                gvMaster.DataSource = Session["gvMaster"];
                gvMaster.DataBind();

                //完成驗收OR部份驗收===>已產生驗收單
                this.btnSave.Enabled = false;
                this.btnCancel.Enabled = false;
                this.btnPrBarCo.Enabled = true;
            }

        
            //門市名稱
            if (StringUtil.CStr(Session["N_STORE"]) == StringUtil.CStr(System.Configuration.ConfigurationManager.AppSettings["DefaultRoleHQ"]))
            {
                this.btnSave.Enabled = false;
                this.btnCancel.Enabled = false;
                this.btnPrBarCo.Enabled = false;
            }
           // this.btnPrBarCo.Enabled = true;

        }
    }

    private void doSelectINVM()
    {

        DataTable dtInvM = (DataTable)ViewState["GetINVM"];

        if (dtInvM.Rows.Count > 0)
        {
            labPooeNo.Text = StringUtil.CStr(dtInvM.Rows[0]["PO_OE_NO"]);
            labOrderNo.Text = StringUtil.CStr(dtInvM.Rows[0]["ORDER_NO"]);
            labReceivingNo.Text = StringUtil.CStr(dtInvM.Rows[0]["INV_APPROVE_NO"]);
            labStoreName.Text = StringUtil.CStr(dtInvM.Rows[0]["STORE_NO"]) + " " + StringUtil.CStr(dtInvM.Rows[0]["STORENAME"]);
            labOrderStatus.Text = StringUtil.CStr(dtInvM.Rows[0]["STATUS_CNAME"]);
            labModiDate.Text = StringUtil.CStr(dtInvM.Rows[0]["MODI_DTM"]);
            labModBy.Text = StringUtil.CStr(dtInvM.Rows[0]["MODI_USER"]) + " " + new Employee_Facade().GetEmpName(StringUtil.CStr(dtInvM.Rows[0]["MODI_USER"]));
            txtOSH_ID.Text = StringUtil.CStr(dtInvM.Rows[0]["ORDER_SHIPCON_HEADER_ID"]);
        }

        if (labModiDate.Text.Trim() == "")
        {
            labModiDate.Text = DateTime.Now.ToString("yyyy/MM/dd HH:mm");
            labModBy.Text = logMsg.OPERATOR + " " + new Employee_Facade().GetEmpName(logMsg.OPERATOR);
        }
    }

    private void doChangeMaster()
    {
        if (gvMaster.VisibleRowCount > 0)
        {
            DataTable dtINVD = Session["gvMaster"] as DataTable;
            DataRow drINVD;
            for (int i = 0; i < gvMaster.VisibleRowCount; i++)
            {
                if (((ASPxLabel)gvMaster.FindRowCellTemplateControl(i, (GridViewDataColumn)gvMaster.Columns["DUUID"], "txtDUUID")) != null)
                {
                    string UUID_KEY = ((ASPxLabel)gvMaster.FindRowCellTemplateControl(i, (GridViewDataColumn)gvMaster.Columns["DUUID"], "txtDUUID")).Text;
                    string txtACCEPT_QTY = ((ASPxTextBox)gvMaster.FindRowCellTemplateControl(i, (GridViewDataColumn)gvMaster.Columns["ACCEPT_QTY"], "txtAdjuestmentQuantity")).Text;
                    if (txtACCEPT_QTY != "")
                    {
                        drINVD = dtINVD.Select("DUUID='" + UUID_KEY + "'")[0];
                        string txtON_HAND_QTY = ((ASPxTextBox)gvMaster.FindRowCellTemplateControl(i, (GridViewDataColumn)gvMaster.Columns["ON_HAND_QTY"], "txtOnHandQty")).Text;
                        string lblIMEI_QTY = ((ASPxTextBox)gvMaster.FindRowCellTemplateControl(i, (GridViewDataColumn)gvMaster.Columns["IMEI_QTY"], "lblIMEI_QTY")).Text;
                        drINVD["ACCEPT_QTY"] = txtACCEPT_QTY;
                        drINVD["ON_HAND_QTY"] = txtON_HAND_QTY;
                        drINVD["IMEI_QTY"] = lblIMEI_QTY;
                        
                    }
                }
            }
            dtINVD.AcceptChanges();
            Session["gvMaster"] = dtINVD;
        }
    }

    private void doSelectMaster()
    {

        DataTable dtMaster = Session["gvMaster"] as DataTable;

        if (dtMaster.Rows.Count > 0)
        {
            DataTable dtResult = new DataTable();
            dtResult.Columns.Add("PRODNO", typeof(string));
            dtResult.Columns.Add("PRODNAME", typeof(string));
            dtResult.Columns.Add("IN_QTY", typeof(int));
            dtResult.Columns.Add("ACCEPT_QTY", typeof(int));
            dtResult.Columns.Add("ON_HAND_QTY", typeof(string));
            dtResult.Columns.Add("SUPPNAME", typeof(string));
            dtResult.Columns.Add("SUPPNO", typeof(string));
            dtResult.Columns.Add("PO_OE_NO", typeof(string));
            dtResult.Columns.Add("IMGIMEI", typeof(string));
            dtResult.Columns.Add("IMEI_QTY", typeof(string));
            dtResult.Columns.Add("IMEI_FLAG", typeof(string));
            dtResult.Columns.Add("IMEI", typeof(string));
            dtResult.Columns.Add("DUUID", typeof(string));
            dtResult.Columns.Add("CHECK_IN_QTY", typeof(string));


            foreach (DataRow dr in dtMaster.Rows)
            {
                DataRow NewRow = dtResult.NewRow();
                NewRow["PRODNO"] = StringUtil.CStr(dr["PRODNO"]);
                NewRow["PRODNAME"] = StringUtil.CStr(dr["PRODNAME"]);
                NewRow["IN_QTY"] = StringUtil.CStr(dr["IN_QTY"]);
                NewRow["ACCEPT_QTY"] = StringUtil.CStr(dr["ACCEPT_QTY"]);
                NewRow["ON_HAND_QTY"] = StringUtil.CStr(dr["ON_HAND_QTY"]);
                NewRow["SUPPNAME"] = StringUtil.CStr(dr["SUPPNAME"]);
                NewRow["SUPPNO"] = StringUtil.CStr(dr["SUPPNO"]);
                NewRow["PO_OE_NO"] = StringUtil.CStr(dr["PO_OE_NO"]);
                NewRow["IMEI"] = StringUtil.CStr(dr["IMEI"]);
                NewRow["IMEI_FLAG"] = StringUtil.CStr(dr["IMEI_FLAG"]);
                NewRow["IMEI_QTY"] = StringUtil.CStr(dr["IMEI_QTY"]);
                NewRow["DUUID"] = StringUtil.CStr(dr["DUUID"]);
                NewRow["CHECK_IN_QTY"] = StringUtil.CStr(dr["CHECK_IN_QTY"]);

                dtResult.Rows.Add(NewRow);
            }

            Session["gvMaster"] = dtResult;
            gvMaster.DataSource = dtResult;
            gvMaster.DataBind();

        }
    }

    private void  doSaveData()
    {
        INV08_Facade _INV08_Facade = new INV08_Facade();
        INV08_InventoryApproval_DTO _INV08_InventoryApproval_DTO = new INV08_InventoryApproval_DTO();
        DataTable DtMaster = Session["gvMaster"] as DataTable;

        INV08_InventoryApproval_DTO.INVENTORY_APPROVAL_MDataTable dtINVM = doInsertINVM();
        INV08_InventoryApproval_DTO.INVENTORY_APPROVAL_DDataTable dtINVD = doInsertINVD();
        INV08_InventoryApproval_DTO.OEPO_CHECKNO_LISTDataTable dtOEPO = doInsertOEPO();

        int intResult = _INV08_Facade.SaveINV(dtINVM, dtINVD, dtOEPO, this.txtOSH_ID.Text, this.labOrderStatus.Text);


        //驗收單編號
        this.labReceivingNo.Text = _INVNO;

        this.btnSave.Enabled = false;
        this.btnCancel.Enabled = false;
        this.btnPrBarCo.Enabled = true;
        EDIT_STATUS = "20";

        if (this.labOrderStatus.Text == "已結案")
        {
            msgStr = "存檔完成，已完成驗收!";
            ScriptManager.RegisterClientScriptBlock(this, typeof(string), "儲存", "alert('" + msgStr + "!');", true);
        }
        else if (this.labOrderStatus.Text == "部份驗收")
        {
            msgStr = "存檔完成，部分驗收完成!";
            ScriptManager.RegisterClientScriptBlock(this, typeof(string), "儲存", "alert('" + msgStr + "!');", true);
        }
        string ORDER_NO = qORDER_NO.Trim();
        string STORE_NO = qSTORE_NO.Trim();

        _INV08_Facade = new INV08_Facade();

        ViewState["GetINVM"] = _INV08_Facade.GetINVM(this.labReceivingNo.Text, StringUtil.CStr(this.ViewState["PO/OE_NO"]), ORDER_NO, STORE_NO);
        doSelectINVM();

        //Bind_Detail
        Session["gvMaster"] = _INV08_Facade.GetINVD_INVANO(this.labReceivingNo.Text);
        gvMaster.DataSource = Session["gvMaster"];
        gvMaster.DataBind();
        gvMaster.PageIndex = 0;
        //完成驗收OR部份驗收===>已產生驗收單
        this.btnSave.Enabled = false;
        this.btnCancel.Enabled = false;
        this.btnPrBarCo.Enabled = true;
    }

    private INV08_InventoryApproval_DTO.INVENTORY_APPROVAL_MDataTable doInsertINVM()
    {
        INV08_InventoryApproval_DTO.INVENTORY_APPROVAL_MDataTable dtINVM = null;
        INV08_InventoryApproval_DTO.INVENTORY_APPROVAL_MRow drINVM;

        INV08_InventoryApproval_DTO _INV08_InventoryApproval_DTO = new INV08_InventoryApproval_DTO();

        dtINVM = _INV08_InventoryApproval_DTO.Tables["INVENTORY_APPROVAL_M"] as INV08_InventoryApproval_DTO.INVENTORY_APPROVAL_MDataTable;
        drINVM = dtINVM.NewINVENTORY_APPROVAL_MRow();

        //驗收單編號
        _INVNO = "SR" + this.labStoreName.Text.Substring(0, 4) + "-" + SerialNo.GenNo("SC");

        //異動的欄位
        drINVM["INV_APPROVAL_NO"] = _INVNO;
        drINVM["PO_OE_NO"] = this.labPooeNo.Text;
        drINVM["ORDER_NO"] = this.labOrderNo.Text;
        drINVM["CHECK_IN_DTM"] = Convert.ToDateTime(System.DateTime.Today);
        //完成驗收
        drINVM["STATUS"] = "70";
        drINVM["STORE_NO"] = this.labStoreName.Text.Trim().Substring(0, 4);

        drINVM["MODI_USER"] = logMsg.OPERATOR;
        drINVM["MODI_DTM"] = Convert.ToDateTime(System.DateTime.Now);

        drINVM["CREATE_USER"] = drINVM["MODI_USER"];
        drINVM["CREATE_DTM"] = drINVM["MODI_DTM"];
        drINVM["INV_APPROVAL_M_ID"] = this.MUUID.Value;
        dtINVM.Rows.Add(drINVM);

        return dtINVM;
    }

    private INV08_InventoryApproval_DTO.INVENTORY_APPROVAL_DDataTable doInsertINVD()
    {
        INV08_InventoryApproval_DTO.INVENTORY_APPROVAL_DDataTable dtINVD = null;


        dtINVD = new INV08_InventoryApproval_DTO.INVENTORY_APPROVAL_DDataTable();
        INV08_InventoryApproval_DTO.INVENTORY_APPROVAL_DRow drINVD;
        DataTable DtMaster = Session["gvMaster"] as DataTable;
        int _ON_HAND_QTY = 0;

        if (gvMaster.VisibleRowCount > 0)
        {
            for (int i = 0; i < gvMaster.VisibleRowCount; i++)
            {
                string PRODNO_KEY = StringUtil.CStr(DtMaster.Rows[i]["PRODNO"]);
                //DataRow dr = new INV08_Facade().Query_PRODNO(PRODNO_KEY).Rows[0];
                string txtACCEPT_QTY = StringUtil.CStr(DtMaster.Rows[i]["ACCEPT_QTY"]);
                if (txtACCEPT_QTY != "" && int.Parse(txtACCEPT_QTY) > 0)
                {
                    drINVD = dtINVD.NewINVENTORY_APPROVAL_DRow();

                    long IN_QTY = 0;
                    string txtIN_QTY = StringUtil.CStr(DtMaster.Rows[i]["IN_QTY"]);
                    string txtDUUID = StringUtil.CStr(DtMaster.Rows[i]["DUUID"]);
                    if (!string.IsNullOrEmpty(txtIN_QTY))
                    {
                        IN_QTY = Convert.ToInt64(txtIN_QTY);
                    }
                    long ACCEPT_QTY = 0;

                    if (!string.IsNullOrEmpty(txtACCEPT_QTY))
                    {
                        ACCEPT_QTY = Convert.ToInt64(txtACCEPT_QTY);
                    }
                    long ON_HAND_QTY = 0;
                    string txtON_HAND_QTY = StringUtil.CStr(DtMaster.Rows[i]["ON_HAND_QTY"]);
                    string txtCHECK_IN_QTY = StringUtil.CStr(DtMaster.Rows[i]["CHECK_IN_QTY"]);

                    if (!string.IsNullOrEmpty(txtON_HAND_QTY))
                    {
                        ON_HAND_QTY = int.Parse(txtIN_QTY) - int.Parse(txtACCEPT_QTY) - int.Parse(txtCHECK_IN_QTY);
                        _ON_HAND_QTY = _ON_HAND_QTY + int.Parse(txtIN_QTY) - int.Parse(txtACCEPT_QTY) - int.Parse(txtCHECK_IN_QTY);
                    }

                    drINVD["IN_QTY"] = StringUtil.CStr(IN_QTY);
                    drINVD["ACCEPT_QTY"] = StringUtil.CStr(ACCEPT_QTY);
                    drINVD["ON_HAND_QTY"] = StringUtil.CStr(ON_HAND_QTY);
                    drINVD["DS_FLAG"] = "";
                    drINVD["PRODNO"] = StringUtil.CStr(DtMaster.Rows[i]["PRODNO"]);
                    drINVD["SUPPNAME"] = StringUtil.CStr(DtMaster.Rows[i]["SUPPNAME"]);
                    drINVD["SUPPNO"] = StringUtil.CStr(DtMaster.Rows[i]["SUPPNO"]);
                    drINVD["ORDER_NO"] = this.labOrderNo.Text;
                    drINVD["MODI_USER"] = logMsg.OPERATOR;
                    drINVD["MODI_DTM"] = Convert.ToDateTime(System.DateTime.Now);
                    drINVD["CREATE_USER"] = drINVD["MODI_USER"];
                    drINVD["CREATE_DTM"] = drINVD["MODI_DTM"];
                    drINVD["INV_APPROVAL_D_ID"] = txtDUUID;
                    drINVD["INV_APPROVAL_M_ID"] = this.MUUID.Value;
                    dtINVD.Rows.Add(drINVD);
                }
                else
                {
                    string txtIN_QTY = StringUtil.CStr(DtMaster.Rows[i]["IN_QTY"]);
                    if (!string.IsNullOrEmpty(txtIN_QTY))
                    {
                        _ON_HAND_QTY = _ON_HAND_QTY + int.Parse(txtIN_QTY) - int.Parse(txtACCEPT_QTY);
                    }
                }
            }

            if (_ON_HAND_QTY > 0)
            {
                this.labOrderStatus.Text = "部份驗收";
            }
            else if (_ON_HAND_QTY == 0)
            {
                this.labOrderStatus.Text = "已結案";
            }

        }

        return dtINVD;

    }

    private INV08_InventoryApproval_DTO.OEPO_CHECKNO_LISTDataTable doInsertOEPO()
    {
        INV08_InventoryApproval_DTO.OEPO_CHECKNO_LISTDataTable dtOEPO = null;

        INV08_InventoryApproval_DTO.OEPO_CHECKNO_LISTRow drOEPO;

        INV08_InventoryApproval_DTO _INV08_InventoryApproval_DTO = new INV08_InventoryApproval_DTO();

        dtOEPO = _INV08_InventoryApproval_DTO.Tables["OEPO_CHECKNO_LIST"] as INV08_InventoryApproval_DTO.OEPO_CHECKNO_LISTDataTable;
        drOEPO = dtOEPO.NewOEPO_CHECKNO_LISTRow();

        //異動的欄位
        drOEPO["ORDER_SHIPCON_HEADER_ID"] = this.txtOSH_ID.Text;
        drOEPO["INV_APPROVE_NO"] = _INVNO;
        drOEPO["SHIP_CHECK_LIST_ID"] = GuidNo.getUUID();
        drOEPO["MODI_USER"] = logMsg.OPERATOR;
        drOEPO["MODI_DTM"] = Convert.ToDateTime(System.DateTime.Now);
        drOEPO["CREATE_USER"] = drOEPO["MODI_USER"];
        drOEPO["CREATE_DTM"] = drOEPO["MODI_DTM"];

        dtOEPO.Rows.Add(drOEPO);

        return dtOEPO;
    }

    private string SupplyContent(string suppno)
    {
        INV08_Facade _INV08_Facade = new INV08_Facade();

        //DataTable TableSupplier = _INV08_Facade.GetSUPPLIER(suppno);

        //string _SUPPNAME = "";
        //string _CONTNAME = "";
        //string _CONTTEL = "";
        //string _FAX = "";
        //string _EMAIL = "";

        string FORMAT = "<table border=\"1\">";

        FORMAT += "<tr><td>供貨商名稱：</td><td>" + suppno + "</td></tr><tr><td>聯絡窗口：</td><td>&nbsp;</td></tr><tr><td>聯絡電話：</td><td>&nbsp;</td></tr><tr><td>傳真號碼：</td><td>&nbsp;</td></tr><tr><td>email：</td><td>&nbsp;</td>";
        //**2011/03/09 Tina：註解，直接顯示畫面上的供應商名字，不需經過查詢。
        //if (TableSupplier.Rows.Count > 0)
        //{
        //    _SUPPNAME = StringUtil.CStr(TableSupplier.Rows[0]["SUPPNAME"]);
        //    _CONTNAME = StringUtil.CStr(TableSupplier.Rows[0]["CONTNAME"]);
        //    _CONTTEL = StringUtil.CStr(TableSupplier.Rows[0]["CONTTEL"]);
        //    _FAX = StringUtil.CStr(TableSupplier.Rows[0]["FAX"]);
        //    _EMAIL = StringUtil.CStr(TableSupplier.Rows[0]["EMAIL"]);
        //    FORMAT += "<tr><td>供貨商名稱：</td><td>" + _SUPPNAME + "</td></tr><tr><td>聯絡窗口：</td><td>" + _CONTNAME + "</td></tr><tr><td>聯絡電話：</td><td>" + _CONTTEL + "</td></tr><tr><td>傳真號碼：</td><td>" + _FAX. + "</td></tr><tr><td>email：</td><td>" + _EMAIL + "</td>";
        //}
        //else
        //{
        //    FORMAT += "<tr><td>供貨商名稱：</td><td> </td></tr><tr><td>聯絡窗口：</td><td> </td></tr><tr><td>聯絡電話：</td><td> </td></tr><tr><td>傳真號碼：</td><td> </td></tr><tr><td>email：</td><td> </td>";
        //}

        FORMAT += "</tr></table>";
        return FORMAT;
    }

    #region Button 觸發事件

    protected void btnSave_Click(object sender, EventArgs e)
    {
        doChangeMaster();

        INV08_Facade _INV08_Facade = new INV08_Facade();
        INV08_Facade _INV08_InventoryApproval_DTO = new INV08_Facade();
        DataTable DtMaster = Session["gvMaster"] as DataTable;
        int _ACCEPT_QTY = 0;
        int _ON_HAND_QTY = 0;
        //驗收量
        if (gvMaster.VisibleRowCount > 0)
        {
            for (int i = 0; i < gvMaster.VisibleRowCount; i++)
            {
                _ACCEPT_QTY += int.Parse(StringUtil.CStr(DtMaster.Rows[i]["ACCEPT_QTY"]));
                _ON_HAND_QTY += int.Parse(StringUtil.CStr(DtMaster.Rows[i]["ON_HAND_QTY"]));
            }
        }
        if (_ACCEPT_QTY == 0)
        {
            ScriptManager.RegisterClientScriptBlock(this, typeof(string), "驗收量", "alert('驗收量不可為0，請重新輸入!!!');", true);
            return;
        }

        //Check...
        if (gvMaster.VisibleRowCount > 0)
        {
            for (int i = 0; i < gvMaster.VisibleRowCount; i++)
            {
                if (int.Parse(StringUtil.CStr(DtMaster.Rows[i]["ACCEPT_QTY"])) < 0)
                {
                    ScriptManager.RegisterClientScriptBlock(this, typeof(string), "驗收量", "alert('本次驗收量為正整數，請重新輸入!!!');", true);
                    return;
                }

                if (int.Parse(StringUtil.CStr(DtMaster.Rows[i]["ACCEPT_QTY"])) > int.Parse(StringUtil.CStr(DtMaster.Rows[i]["IN_QTY"])))
                {
                    ScriptManager.RegisterClientScriptBlock(this, typeof(string), "驗收量", "alert('本次驗收量不可大於到貨量，請重新輸入!!!');", true);
                    return;
                }
                if (StringUtil.CStr(DtMaster.Rows[i]["IMEI_Flag"]) == "3")
                {
                    if (int.Parse(StringUtil.CStr(DtMaster.Rows[i]["ACCEPT_QTY"])) != int.Parse(StringUtil.CStr(DtMaster.Rows[i]["IMEI_QTY"])))
                    {
                        {
                            ScriptManager.RegisterClientScriptBlock(this, typeof(string), "驗收量", "alert('IMEI數量不等於驗收數，請重新輸入!!!');", true);
                            return;
                        }
                    }
                }

            }
        }
        if (_ON_HAND_QTY != 0)
        {
            string Rtn = "您尚未全部驗收，是否要部份驗收？";
            string javascript = "";
            javascript += "if (confirm('" + Rtn + "')) {";
            javascript += " SaveItem.SetText('Y');";
            javascript += " btnSave1.SendPostBack('Click');";
           // javascript += "document.all['Test'].value='Y';";
           // javascript += "document.getElementById('btnSave1').click();";
            javascript += "} ";
            ScriptManager.RegisterClientScriptBlock(this, typeof(string), "msg", javascript, true);
            return;
        }
        else {
            doSaveData();
        }
      
    }

    protected void btnSave1_Click(object sender, EventArgs e)
    {
        doSaveData();
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        string dno = qPOOE_NO;//Request.QueryString["PO/OE_NO"] ?? "";
        this.ViewState["PO/OE_NO"] = dno;
        string ReNo = qReceivingNo; //Request.QueryString["ReceivingNo"] == null ? "" : StringUtil.CStr(Request.QueryString["ReceivingNo"]).Trim();
        EDIT_STATUS = qSTATUS; //Request.QueryString["STATUS"] == null ? "" : StringUtil.CStr(Request.QueryString["STATUS"]).Trim();
        this.ViewState["ReceivingNo"] = ReNo;
        string ORDER_NO = qORDER_NO; //Request.QueryString["ORDER_NO"] == null ? "" : StringUtil.CStr(Request.QueryString["ORDER_NO"]).Trim();
        string STORE_NO = qSTORE_NO; //Request.QueryString["STORE_NO"] == null ? "" : StringUtil.CStr(Request.QueryString["STORE_NO"]).Trim();

        Session["N_STORE"] = logMsg.STORENO.Trim();
        this.MUUID.Value = GuidNo.getUUID();

        INV08_Facade _INV08_Facade = new INV08_Facade();

        ViewState["GetINVM"] = _INV08_Facade.GetINVM("", StringUtil.CStr(this.ViewState["PO/OE_NO"]), ORDER_NO, STORE_NO);
        doSelectINVM();

        this.btnSave.Enabled = true;
        this.btnCancel.Enabled = true;
        this.btnPrBarCo.Enabled = false;

        //Bind_Master
        Session["gvMaster"] = _INV08_Facade.GetINVD_POOENO(this.labPooeNo.Text, ORDER_NO, STORE_NO);
        doSelectMaster();
        this.labReceivingNo.Text = "";
    }

    #endregion

    #region gvMaster 觸發事件

    protected void gvMaster_PageIndexChanged(object sender, EventArgs e)
    {
        gvMaster.DataSource = Session["gvMaster"];
        gvMaster.DataBind();
    }

    protected void gvMaster_HtmlRowCreated(object sender, ASPxGridViewTableRowEventArgs e)
    {
        if (e.RowType == GridViewRowType.Data)
        {
            string IMEIFlag = e.Row.FindChildControl<ASPxTextBox>("txtIMEIFlag").Text;
            string TRANOUTQTY = e.Row.FindChildControl<ASPxTextBox>("txtAdjuestmentQuantity").Text;
            string IMEI_QTY = e.Row.FindChildControl<ASPxTextBox>("lblIMEI_QTY").Text;

            ASPxTextBox txt1 = e.Row.FindChildControl<ASPxTextBox>("txtAdjuestmentQuantity");
            PopupControl pControl = e.Row.FindChildControl<PopupControl>("txtIMEI");
            ASPxLabel lb13 = e.Row.FindChildControl<ASPxLabel>("lb13");
            ASPxLabel lblsuppno = e.Row.FindChildControl<ASPxLabel>("lblsuppno");

            string _PRODNO = StringUtil.CStr(gvMaster.GetRowValuesByKeyValue(e.KeyValue, "PRODNO"));
            string _POOENO = StringUtil.CStr(gvMaster.GetRowValuesByKeyValue(e.KeyValue, "PO_OE_NO"));
            string _DUUID = StringUtil.CStr(gvMaster.GetRowValuesByKeyValue(e.KeyValue, "DUUID"));
            //設定顯示IMEI圖示的ImageURL
            int intC_IMEI = int.Parse(TRANOUTQTY == "" ? "0" : TRANOUTQTY);
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

            ASPxTextBox lblIMEI_QTY = e.Row.FindChildControl<ASPxTextBox>("lblIMEI_QTY");
            // 繫結明細資料表  
            lblIMEI_QTY.Attributes["onmouseover"] = string.Format("show('{0}');", IMEIContent("INV_APPRO_IMEI", _DUUID, _PRODNO));
            lblIMEI_QTY.Attributes["onmouseout"] = "hide();";

            lb13.Attributes["onmouseover"] = string.Format("show('{0}');", SupplyContent(lb13.Text));
            lb13.Attributes["onmouseout"] = "hide();";

            PopupControl txtIMEI = e.Row.FindChildControl<PopupControl>("txtIMEI");
            ASPxPopupControl ASPxPopupControl1 = txtIMEI.FindChildControl<ASPxPopupControl>("ASPxPopupControl1");


            txtIMEI.KeyFieldValue1 = "INV_APPRO_IMEI;" + _DUUID + ";" + _PRODNO + ";" + TRANOUTQTY.Replace("-", "") + ";" + _POOENO;

           // ASPxPopupControl1.ContentUrl = "~/VSS/SAL/SAL01/SAL01_inputIMEIData.aspx?SysDate=Date()&KeyFieldValue1=" + txtIMEI.KeyFieldValue1;

            //**2011/04/26 Tina：傳遞參數時，要先以加密處理。
            string encryptUrl = Utils.Param_Encrypt("SysDate=Date()&KeyFieldValue1=" + txtIMEI.KeyFieldValue1);
            ASPxPopupControl1.ContentUrl = string.Format("~/VSS/SAL/SAL01/SAL01_inputIMEIData.aspx?Param={0}", encryptUrl);


            if (string.IsNullOrEmpty(IMEIFlag) || IMEIFlag != "3")
            {
                txtIMEI.Enabled = false;
                imgIMEI.ClientVisible = false;
            }
            else
            {
                txtIMEI.Enabled = true;
                imgIMEI.ClientVisible = true;
            }


            //已結案
            if (this.labOrderStatus.Text.Trim() == "已結案" || EDIT_STATUS == "20")
            {
                txt1.ReadOnly = true;
                pControl.Enabled = false;
                txt1.Border.BorderStyle = BorderStyle.None;
            }

            //總部人員
            if (StringUtil.CStr(Session["N_STORE"]) == StringUtil.CStr(System.Configuration.ConfigurationManager.AppSettings["DefaultRoleHQ"]))
            {
                txt1.ReadOnly = true;
                pControl.Enabled = false;
                txt1.Border.BorderStyle = BorderStyle.None;
            }
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



    protected void btnPrBarCo_Click(object sender, EventArgs e)
    {
        INV08_Facade _INV08_Facade = new INV08_Facade();
        DataTable dt = _INV08_Facade.GetINVD_INVANO(this.labReceivingNo.Text);
        //Receipt rt = new Receipt();
        //string Filename = rt.inv08barcode(dt);
        //string Path1 = @"/Downloads";
        //string path = HttpContext.Current.Server.MapPath("~") + Path1 + Filename;
        string ORDER_NO = qORDER_NO.Trim();
        string barcodestr = "";
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            //if (dataList == "")'
            //    dataList = txtProdNo2.value + "^" + txtProdName2.value + "^" + txtCount2.value;
            //else
            //    dataList = dataList + "|" + txtProdNo2.value + "^" + txtProdName2.value + "^" + txtCount2.value;
            if (barcodestr == "")
            {
                barcodestr = StringUtil.CStr(dt.Rows[i]["PRODNO"]) + "^" + StringUtil.CStr(dt.Rows[i]["PRODNAME"]) + "^" + StringUtil.CStr(dt.Rows[i]["IN_QTY"]);
            }
            else
            {
                barcodestr = barcodestr+ "|" + StringUtil.CStr(dt.Rows[i]["PRODNO"]) + "^" + StringUtil.CStr(dt.Rows[i]["PRODNAME"]) + "^" + StringUtil.CStr(dt.Rows[i]["IN_QTY"]);
            }
        }

        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "PrintInvoice", " Call_BarcodePrintFile(\""+ORDER_NO+"\",\""+barcodestr+"\");", true);
     
    }
}
