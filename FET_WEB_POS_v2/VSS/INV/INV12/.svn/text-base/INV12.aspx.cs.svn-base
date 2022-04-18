using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using DevExpress.Web.ASPxEditors;
using Advtek.Utility;
using FET.POS.Model.Common;
using FET.POS.Model.DTO;
using FET.POS.Model.Facade;
using FET.POS.Model.Facade.FacadeImpl;
using DevExpress.Web.ASPxGridView;

public partial class VSS_INV_INV12_INV12 : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack && !Page.IsCallback)
        {
            if (logMsg.ROLE_TYPE != StringUtil.CStr(System.Configuration.ConfigurationManager.AppSettings["DefaultRoleHQ"]))
            {
                //彈跳視窗
                ScriptManager.RegisterClientScriptBlock(this, GetType(), "AlertMessage", "alert('非總部人員無法使用此功能!!');", true);
                this.gvMaster.Enabled = false;
                txtStoreNO.Enabled = false;    //門市編號
                ReceivedDate.Enabled = false;   //進貨日期
                ReceivedDate.ControlStyle.BackColor = System.Drawing.Color.LightGray;
                txtRemark.Enabled = false;  //備註
                txtRemark.ControlStyle.BackColor = System.Drawing.Color.LightGray;
                Button3.Enabled = false;    //查詢修改
                this.btnPrint.ClientEnabled = false;   //列印條碼
                this.btnSave.ClientEnabled = false;   //儲存
                this.btnCancel.ClientEnabled = false;   //取消
                return;
            }

            Session["gvMaster"] = null;
            //取得空的資料表
            BindEmptyMasterData();

            ReceivedDate.Text = DateTime.Today.ToString("yyyy/MM/dd");
            ReceivedDate.MaxDate = DateTime.Today;
            this.btnPrint.ClientEnabled = false;
            this.btnSave.ClientEnabled = false;
            this.btnCancel.ClientEnabled = false;

            //gvMaster.FindChildControl<ASPxButton>("btnAddNew").ClientEnabled = false;

        }
    }

    private void BindEmptyMasterData()
    {
        DataTable dt = new DataTable();
        if (Session["gvMaster"] == null)
        {
            dt.Columns.Add("NP_ORDER_D_ID", typeof(string));
            dt.Columns.Add("NP_ORDER_ON", typeof(string));
            dt.Columns.Add("PRODNO");
            dt.Columns.Add("ProductName");
            dt.Columns.Add("ProductUnit");
            dt.Columns.Add("ORDER_QTY");
            dt.Columns.Add("ORDER_AMT");
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
        gvMaster.DataSource = (DataTable)Session["gvMaster"];
        gvMaster.DataBind();
        gvMaster.Selection.UnselectAll();
    }

    #region Button 觸發事件

    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (Session["gvMaster"] != null)
        {
            if (ReceivedDate.Date > DateTime.Today)
            {
                ScriptManager.RegisterClientScriptBlock(this, GetType(), "AlertMessage", "alert('【進貨日期】不可大於系統日!!');", true);
                return;
            }
            if (gvMaster.IsEditing)
            {
                ScriptManager.RegisterClientScriptBlock(this, GetType(), "AlertMessage", "alert('請先完成新增商品程序');", true);
                return;

            }

            DataTable dtData = Session["gvMaster"] as DataTable;

            if (dtData.Rows.Count == 0)
            {
                ScriptManager.RegisterClientScriptBlock(this, GetType(), "AlertMessage", "alert('訂單無任何商品資料，無法儲存');", true);
                return;
            }

            Store_Facade storeF = new Store_Facade();
            DataTable dtStore = null;
            if (!string.IsNullOrEmpty(this.txtStoreNO.Text))
                dtStore = storeF.Query_StoreInfo(this.txtStoreNO.Text);

            if (dtStore == null || dtStore.Rows.Count == 0)
            {
                ScriptManager.RegisterClientScriptBlock(this, GetType(), "AlertMessage", "alert('無此門市資料，無法儲存');", true);
                return;
            }

            INV12_NPOrder_DTO ds = new INV12_NPOrder_DTO();
            INV12_NPOrder_DTO.NP_ORDER_MDataTable MasterDt = ds.NP_ORDER_M;
            INV12_NPOrder_DTO.NP_ORDER_DDataTable DetailDt = ds.NP_ORDER_D;

            //取得主檔資訊
            INV12_NPOrder_DTO.NP_ORDER_MRow MasterDr = MasterDt.NewNP_ORDER_MRow();

            string M_ID = GuidNo.getUUID();

            MasterDr.REMARK = txtRemark.Text;
            MasterDr.MODI_USER = logMsg.OPERATOR;
            MasterDr.MODI_DTM = DateTime.Now;
            MasterDr.CREATE_USER = MasterDr.MODI_USER;
            MasterDr.CREATE_DTM = MasterDr.MODI_DTM;
            MasterDr.NP_ORDER_M_ID = M_ID;
            MasterDr.NP_ORDER_ON = "SN" + txtStoreNO.Text + "-" + SerialNo.GenNo("NP_ORDER");
            MasterDr.STORE_NO = txtStoreNO.Text;
            MasterDr.NP_ORDER_DATE = ReceivedDate.Date;

            MasterDt.AddNP_ORDER_MRow(MasterDr);

            foreach (DataRow dr in dtData.Rows)
            {
                INV12_NPOrder_DTO.NP_ORDER_DRow DetailDr = DetailDt.NewNP_ORDER_DRow();

                DetailDr.NP_ORDER_D_ID = StringUtil.CStr(dr["NP_ORDER_D_ID"]);
                DetailDr.NP_ORDER_M_ID = MasterDr.NP_ORDER_M_ID;
                DetailDr.ORDER_QTY = Convert.ToInt32(dr["ORDER_QTY"]);
                DetailDr.ORDER_AMT = Convert.ToInt32(dr["ORDER_AMT"]);
                DetailDr.PRODNO = StringUtil.CStr(dr["PRODNO"]);
                DetailDr.MODI_USER = MasterDr.MODI_USER;
                DetailDr.MODI_DTM = MasterDr.MODI_DTM;
                DetailDr.CREATE_USER = DetailDr.MODI_USER;
                DetailDr.CREATE_DTM = DetailDr.MODI_DTM;

                DetailDt.AddNP_ORDER_DRow(DetailDr);
            }

            ds.AcceptChanges();


            INV12_Facade facade = new INV12_Facade();

            //更新資料庫
            facade.InsertNPOrder(ds);

            NPOrderNO.Text = MasterDr.NP_ORDER_ON;
            ModifiedDate.Text = MasterDr.MODI_DTM.ToShortDateString();
            ModifiedBy.Text = MasterDr.MODI_USER + " " + new Employee_Facade().GetEmpName(logMsg.OPERATOR);
            //this.btnPrint.ClientEnabled = true;
            gvMaster.FindChildControl<ASPxButton>("btnAddNew").ClientEnabled = false;
            gvMaster.FindChildControl<ASPxButton>("btnDelete").ClientEnabled = false;
            DisableButton(true);

            //彈跳視窗，提示移出端要列印移出單
            ScriptManager.RegisterClientScriptBlock(this, GetType(), "AlertMessage", "alert('作業完成!!');", true);
        }
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

            DataRow[] dra = dt.Select("NP_ORDER_D_ID in(" + where + ")");

            foreach (DataRow dr in dra)
            {
                dt.Rows.Remove(dr);
                dt.AcceptChanges();
            }

            BindMasterData();
        }

    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("INV12.aspx");
    }

    protected void btnPrint_Click(object sender, EventArgs e)
    {
        //DataTable dtheader = new DataTable();
        //dtheader.Columns.Add("header1", typeof(string));
        //dtheader.Columns.Add("header2", typeof(string));
        //DataRow NewRowHeader = dtheader.NewRow();
        //NewRowHeader["header1"] = "移撥單號： " + this.lblOrderNo.Text;
        //NewRowHeader["header2"] = "";
        //dtheader.Rows.Add(NewRowHeader);

        //NewRowHeader = dtheader.NewRow();
        //NewRowHeader["header1"] = "移出門市： " + logMsg.STORENO;
        //NewRowHeader["header2"] = "撥入門市： " + this.txtToStoreNO.Text;
        //dtheader.Rows.Add(NewRowHeader);

        //NewRowHeader = dtheader.NewRow();
        //NewRowHeader["header1"] = "移出日期： " + System.DateTime.Today.ToString("yyyy/MM/dd");
        //NewRowHeader["header2"] = "撥入日期：";
        //dtheader.Rows.Add(NewRowHeader);

        //DataTable dtfooter = new DataTable();
        //dtfooter.Columns.Add("footer1", typeof(string));
        //DataRow NewRowFooter = dtfooter.NewRow();
        //NewRowFooter["footer1"] = "移出人員： ";
        //dtfooter.Rows.Add(NewRowFooter);
        //NewRowFooter = dtfooter.NewRow();
        //NewRowFooter["footer1"] = "撥入人員： ";
        //dtfooter.Rows.Add(NewRowFooter);

        //DataTable dt = new INV25_Facade().Query_PrintStkChk(this.lblOrderNo.Text);
        //string filename = new Output().Print("PDF", "移出單", dtheader, dt, dtfooter);
        ////Response.Redirect(Utils.CreateTamperProofDownloadURL(filename));
        //ProcessRequest(filename);
    }

    public void ProcessRequest(string filename)
    {
        string filePath = "/FET_POS/Downloads/" + filename;
        ScriptManager.RegisterClientScriptBlock(this,
                                                this.GetType(),
                                                "test",
                                                "document.getElementById('" + fDownload.ClientID + "').src='" + filePath + "';",
                                                true);
    }


    #endregion

    #region gvMaster 觸發事件

    protected void gvMaster_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
    {
        if (Session["gvMaster"] != null)
        {
            DataTable dt = Session["gvMaster"] as DataTable;

            DataRow DRNew = dt.NewRow();
            DRNew["NP_ORDER_D_ID"] = StringUtil.CStr(Session["NP_ORDER_D_ID"]);
            DRNew["NP_ORDER_ON"] = "";
            DRNew["PRODNO"] = e.NewValues["PRODNO"];
            DRNew["ProductName"] = e.NewValues["ProductName"];
            DRNew["ProductUnit"] = e.NewValues["ProductUnit"];
            DRNew["ORDER_QTY"] = e.NewValues["ORDER_QTY"];
            DRNew["ORDER_AMT"] = e.NewValues["ORDER_AMT"];

            dt.Rows.Add(DRNew);
            dt.AcceptChanges();

            gvMaster.CancelEdit();
            e.Cancel = true;

            BindMasterData();
            Session["NP_ORDER_D_ID"] = null;
            DisableButton(false); //可點擊 "確定移出" 和 "取消" 按鈕

        }

    }

    protected void gvMaster_InitNewRow(object sender, DevExpress.Web.Data.ASPxDataInitNewRowEventArgs e)
    {
        Session["NP_ORDER_D_ID"] = GuidNo.getUUID();
        DisableButton(true); //不可點擊 "確定移出" 和 "取消" 按鈕
        gvMaster.Selection.UnselectAll();
    }

    protected void gvMaster_CancelRowEditing(object sender, DevExpress.Web.Data.ASPxStartRowEditingEventArgs e)
    {
        DisableButton(false); //可點擊 "確定移出" 和 "取消" 按鈕
        gvMaster.Selection.UnselectAll();
    }

    protected void gvMaster_CommandButtonInitialize(object sender, ASPxGridViewCommandButtonEventArgs e)
    {
        if (e.ButtonType == ColumnCommandButtonType.Edit || e.ButtonType == ColumnCommandButtonType.Update || e.ButtonType == ColumnCommandButtonType.Cancel)
        {
            e.Visible = false;
        }
    }

    protected void gvMaster_RowValidating(object sender, DevExpress.Web.Data.ASPxDataValidationEventArgs e)
    {
        string strProdNo = ((PopupControl)gvMaster.FindEditRowCellTemplateControl((GridViewDataColumn)gvMaster.Columns["PRODNO"], "txtProductCode")).Text.Trim();

        DataTable dtCount = new Product_Facade().Query_ProductInfo(strProdNo);
        if (dtCount.Rows.Count == 0)
        {
            e.RowError += "商品料號不存在，請重新輸入!!";
        }
        else
        {
            if (Session["gvMaster"] != null)
            {
                DataTable dt = Session["gvMaster"] as DataTable;

                string expression;
                expression = "PRODNO = '" + strProdNo + "'";
                DataRow[] data = dt.Select(expression);
                for (int i = 0; i < data.Length; i++)
                {
                    string strSEQNO_Table = e.Keys["NP_ORDER_D_ID"] == null ? StringUtil.CStr(Session["NP_ORDER_D_ID"]) : StringUtil.CStr(e.Keys["NP_ORDER_D_ID"]);
                    string strSEQNO = StringUtil.CStr(data[i]["NP_ORDER_D_ID"]);
                    if (!string.IsNullOrEmpty(strSEQNO_Table) && strSEQNO_Table != strSEQNO)
                    {
                        e.RowError += "商品料號重複，請重新輸入!!";
                        return;
                    }

                }
            }
        }

    }

    protected void gvMaster_PageIndexChanged(object sender, EventArgs e)
    {
        BindMasterData();
        gvMaster.CancelEdit();
    }

    protected void gvMaster_StartRowEditing(object sender, DevExpress.Web.Data.ASPxStartRowEditingEventArgs e)
    {
        DisableButton(true); //不可點擊 "確定移出" 和 "取消" 按鈕
    }

    protected void gvMaster_PreRender(object sender, EventArgs e)
    {
        if (btnSave.ClientEnabled == false)
        {
            gvMaster.FindChildControl<ASPxButton>("btnAddNew").ClientEnabled = false;
            gvMaster.FindChildControl<ASPxButton>("btnDelete").ClientEnabled = false;
        }
    }

    protected void btnAddNewM_Click(object sender, EventArgs e)
    {
        if (getStoreInfo(txtStoreNO.Text) != "")
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
        this.btnSave.ClientEnabled = this.btnCancel.ClientEnabled = !IsDisabled;
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
            if (dt.Rows.Count > 0)
            {
                strInfo = StringUtil.CStr(dt.Rows[0]["PRODNAME"]) + ";" + StringUtil.CStr(dt.Rows[0]["UNIT"]);
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
}
