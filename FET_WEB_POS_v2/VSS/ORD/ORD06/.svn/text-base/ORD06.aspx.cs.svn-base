using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using DevExpress.Web.ASPxGridView;
using DevExpress.Web.ASPxEditors;
using FET.POS.Model.Facade.FacadeImpl;
using FET.POS.Model.Common;
using FET.POS.Model.DTO;
using Advtek.Utility;
public partial class VSS_ORD_ORD06 : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            txtStartDate.Text = DateTime.Now.AddDays(1).ToString("yyyy/MM/dd");
        }
    }

    protected void bindMasterData()
    {
        DataTable dtResult = new ORD06_Facade().GetOneToOneMethodData(txtProdNo.Text.Trim(), txtProdName.Text.Trim(),
                                                            txtWithTheProductNo.Text.Trim(), txtWithTheProductName.Text.Trim(),
                                                            txtStartDate.Text, txtEndDate.Text, StringUtil.CStr(cbStatus.SelectedItem.Value));

        Session["gvMaster"] = dtResult;
        gvMaster.DataSource = dtResult;
        gvMaster.DataBind();

    }

    private bool DataCheck()
    {
        string msgStr = "";
        bool UpdateData = false;
        string PM_PRODNO = txtProdNo.Text;
        string PD_PRODNO = txtWithTheProductNo.Text;
        string S_DATE = txtStartDate.Text;
        string E_DATE = txtEndDate.Text;

        if (PM_PRODNO == null || PM_PRODNO.Trim() == "")
        {
            msgStr = "【主商品編號】不允許空值，請重新輸入!";
        }
        else if (PD_PRODNO == null || PD_PRODNO.Trim() == "")
        {
            msgStr = "【搭配商品編號】不允許空值，請重新輸入!";
        }
        else if (PM_PRODNO.Trim().ToUpper() == PD_PRODNO.Trim().ToUpper())
        {
            msgStr = "主商品不允許與搭配商品相同，請重新輸入";
        }
        else if (ORD06_Facade.CheckMainProduct(PD_PRODNO, PM_PRODNO,"INSERT",S_DATE,E_DATE ) != "")
        {
            msgStr = "搭配商品己設定為己生效組合的主商品，請重新輸入";
        }
        else if (ORD06_Facade.CheckMainDetailProduct(PM_PRODNO, PD_PRODNO,S_DATE,E_DATE ) != "")
        {
            msgStr = "一搭一組合己存在，請重新輸入";
        }
        else
        {
            if (getProductInfo(PM_PRODNO) != "" && getProductInfo(PD_PRODNO) != "")
                UpdateData = true;
        }

        if (UpdateData)
        {
            if (S_DATE == null || S_DATE.Trim() == "")
            {
                msgStr = "【開始日期】不允許空值，請重新輸入";
                UpdateData = false;
            }
            else
            {
                if (Convert.ToDateTime(S_DATE).Date <= DateTime.Now.Date)
                {
                    msgStr = "【開始日期】必須大於系統日期";
                    UpdateData = false;
                }
                else
                {
                    if (E_DATE != null && E_DATE.Trim() != "")
                    {
                        if (E_DATE.Substring(0, 4) != "0001" && Convert.ToDateTime(E_DATE).Date <= DateTime.Now.Date)
                        {
                            msgStr = "【結束日期】必須大於系統日期";
                            UpdateData = false;
                        }
                        else
                        {
                            if (E_DATE.Substring(0, 4) != "0001" && Convert.ToDateTime(E_DATE).Date < Convert.ToDateTime(S_DATE).Date)
                            {
                                msgStr = "【結束日期】必須>=【開始日期】";
                                UpdateData = false;
                            }

                        }
                    }
                }
            }
        }
        if (msgStr != "") { ScriptManager.RegisterClientScriptBlock(this, typeof(string), "RowSave", "alert('" + msgStr + "!');", true); }
        return UpdateData;
    }

    protected void ac1_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
    {
        txtProdNo.Text = "";
        txtProdName.Text = "";
        txtWithTheProductNo.Text = "";
        txtWithTheProductName.Text = "";
        txtStartDate.Text = DateTime.Now.AddDays(1).ToString("yyyy/MM/dd");
        txtEndDate.Text = "";
        cbStatus.SelectedIndex = 2;
        bindMasterData();

    }

    #region Button 觸發事件

    protected void btnNew_Click(object sender, EventArgs e)
    {
        if (!gvMaster.IsNewRowEditing && !gvMaster.IsEditing)
        {
            if (DataCheck())
            {
                string PM_PRODNO = txtProdNo.Text;
                string PD_PRODNO = txtWithTheProductNo.Text;
                string S_DATE = txtStartDate.Text;
                string E_DATE = txtEndDate.Text;
                new ORD06_Facade().InsertOneToOneMethodData(PM_PRODNO,
                PD_PRODNO, S_DATE, E_DATE, logMsg.MODI_USER);


                bindMasterData();
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Common", "alert('存檔完成!!')", true);
                gvMaster.CancelEdit();
            }
        }
    }

    protected void btnDelete_Click(object sender, EventArgs e)
    {

        ORD06_OneToOne_DTO _ORD06_OneToOne_DTO = new ORD06_OneToOne_DTO();
        ORD06_Facade _ORD06_Facade = new ORD06_Facade();

        DataSet ds = new DataSet();
        DataTable dtm = new DataTable();
        dtm.TableName = _ORD06_OneToOne_DTO.ONETOONE_M.TableName;
        dtm.Columns.Add("SID");


        DataTable dtd = new DataTable();
        dtd.TableName = _ORD06_OneToOne_DTO.ONETOONE_D.TableName;
        dtd.Columns.Add("SID");

        List<object> keyValues = this.gvMaster.GetSelectedFieldValues(gvMaster.KeyFieldName);

        foreach (string key in keyValues)
        {
            DataRow dr = dtm.NewRow();
            dr["SID"] = key;
            dtm.Rows.Add(key);

            dtd.Rows.Add(key);
        }
        ds.Tables.Add(dtm);
        ds.Tables.Add(dtd);
        ds.AcceptChanges();


        _ORD06_Facade.DeleteOneToOne(ds);
        bindMasterData();
        this.gvMaster.Selection.UnselectAll();
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        try
        {
            gvMaster.Selection.UnselectAll();
            gvMaster.PageIndex = 0;
            gvMaster.CancelEdit();
            string sStart = txtStartDate.Text;
            string sEnd = (string.IsNullOrEmpty(txtEndDate.Text)) ? "9999/12/31" : txtEndDate.Text;
            if (sStart != "" && sStart != null)
            {
                DateTime d1 = DateTime.Parse(sStart);
                DateTime d2 = DateTime.Parse(sEnd);

                if (d1 > d2)
                    throw new Exception("【結束日期】不可小於【開始日期】");
            }
            bindMasterData();
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Common", "alert('" + ex.Message + "')", true);
        }

    }

    protected void btnReset_Click(object sender, EventArgs e)
    {
        txtProdNo.Text = "";
        txtProdName.Text = "";
        txtWithTheProductNo.Text = "";
        txtWithTheProductName.Text = "";
        txtStartDate.Text = "";
        txtEndDate.Text = "";
        cbStatus.SelectedIndex = 0;
        gvMaster.Selection.UnselectAll();
        Session["gvMaster"] = null;
        gvMaster.DataSource = null;
        gvMaster.DataBind();
    }

    #endregion

    #region gvMaster 觸發事件

    protected void gvMaster_PageIndexChanged(object sender, EventArgs e)
    {
        DataTable dt = Session["gvMaster"] as DataTable;
        gvMaster.DataSource = dt;
        gvMaster.DataBind();
    }

    protected void gvMaster_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
    {
        ASPxGridView grid = (ASPxGridView)sender;
        DataTable dt = Session["gvMaster"] as DataTable ?? null;
        DataRow[] DRSelf = dt.Select("SID='" + StringUtil.CStr(e.Keys[0]).Trim() + "'");


        if (DRSelf.Length > 0)
        {
            ASPxDateEdit sdate = grid.FindEditRowCellTemplateControl((GridViewDataDateColumn)grid.Columns["S_DATE"], "txtSDATE") as ASPxDateEdit;
            DRSelf[0]["S_DATE"] = sdate.Text;
            ASPxDateEdit edate = grid.FindEditRowCellTemplateControl((GridViewDataDateColumn)grid.Columns["E_DATE"], "txtEDATE") as ASPxDateEdit;
            if (edate.Text != "")
                DRSelf[0]["E_DATE"] = edate.Text;
            PopupControl pop1 = (PopupControl)grid.FindEditRowCellTemplateControl((GridViewDataColumn)grid.Columns["PM_PRODNO"], "txtPM_PRODNO");
            DRSelf[0]["PM_PRODNO"] = pop1.popTextBox.Text;
            PopupControl pop2 = (PopupControl)grid.FindEditRowCellTemplateControl((GridViewDataColumn)grid.Columns["PD_PRODNO"], "txtPD_PRODNO");
            DRSelf[0]["PD_PRODNO"] = pop2.popTextBox.Text;

            new ORD06_Facade().UpdateOenToOneMethodData(DRSelf[0]["SID"], DRSelf[0]["PM_PRODNO"],
                    DRSelf[0]["PD_PRODNO"], e.NewValues["S_DATE"], e.NewValues["E_DATE"], logMsg.MODI_USER);

            gvMaster.CancelEdit();
            bindMasterData();

        }

        e.Cancel = true;
        gvMaster.CancelEdit();

    }

    protected void gvMaster_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
    {
        new ORD06_Facade().InsertOneToOneMethodData(e.NewValues["PM_PRODNO"],
            e.NewValues["PD_PRODNO"], e.NewValues["S_DATE"], e.NewValues["E_DATE"], logMsg.MODI_USER);

        bindMasterData();
        e.Cancel = true;
        gvMaster.CancelEdit();
    }

    protected void gvMaster_HtmlRowPrepared(object sender, ASPxGridViewTableRowEventArgs e)
    {
        e.Row.Attributes["canSelect"] = "true";
        if (e.RowType == GridViewRowType.Data)
        {
            List<object> keyValues = this.gvMaster.GetSelectedFieldValues(gvMaster.KeyFieldName);
            foreach (string key in keyValues)
            {
                if (key == StringUtil.CStr(e.GetValue(gvMaster.KeyFieldName)))
                {
                    if (key == this.hdNo.Value)
                    {
                        e.Row.BackColor = System.Drawing.ColorTranslator.FromHtml("#999999");
                        e.Row.ForeColor = System.Drawing.ColorTranslator.FromHtml("#FFFFFF");
                    }
                    else
                    {
                        e.Row.BackColor = System.Drawing.ColorTranslator.FromHtml("#FFFFFF");
                        e.Row.ForeColor = System.Drawing.ColorTranslator.FromHtml("#000000");
                    }

                }
            }
            if (e.VisibleIndex > -1)
            {

                if (Convert.ToDateTime(StringUtil.CStr(e.GetValue("S_DATE"))).Date <= DateTime.Now.Date)
                {
                    e.Row.Attributes["canSelect"] = "false";

                }
            }


        }

    }

    protected void gvMaster_InitNewRow(object sender, DevExpress.Web.Data.ASPxDataInitNewRowEventArgs e)
    {
        ASPxGridView gv = (ASPxGridView)sender;
        PopupControl mPrdtNo = gv.FindChildControl<PopupControl>("txtPM_PRODNO");
        mPrdtNo.Text = this.txtProdNo.Text;
        ASPxLabel lPrdName = gv.FindChildControl<ASPxLabel>("lblPM_PRODNAME");
        lPrdName.Text = this.txtProdName.Text;
        PopupControl dPrdtNo = gv.FindChildControl<PopupControl>("txtPD_PRODNO");
        dPrdtNo.Text = this.txtWithTheProductNo.Text;
        ASPxLabel lPrdtName = gv.FindChildControl<ASPxLabel>("lblPD_PRODNAME");
        lPrdtName.Text = this.txtWithTheProductName.Text;
        ASPxDateEdit txtSDATE = gv.FindChildControl<ASPxDateEdit>("txtSDATE");
        txtSDATE.Text = txtStartDate.Text;
        ASPxDateEdit txtEDATE = gv.FindChildControl<ASPxDateEdit>("txtEDATE");
        txtEDATE.Text = txtEndDate.Text;
    }

    protected void gvMaster_HtmlRowCreated(object sender, ASPxGridViewTableRowEventArgs e)
    {
        if (e.RowType == GridViewRowType.InlineEdit)
        {
            if (gvMaster.IsEditing)
            {
                string checkdate = System.DateTime.Now.ToString("yyyy/MM/dd");
                ASPxGridView gv = (ASPxGridView)sender;

                ASPxDateEdit dStart = gv.FindEditRowCellTemplateControl((GridViewDataColumn)gv.Columns["S_DATE"], "txtSDATE") as ASPxDateEdit;
                ASPxLabel lblStatus = gv.FindEditRowCellTemplateControl((GridViewDataColumn)gv.Columns["STATUS"], "lblSTATUS") as ASPxLabel;
                PopupControl PM_PRODNO = gv.FindEditRowCellTemplateControl((GridViewDataColumn)gv.Columns["PM_PRODNO"], "txtPM_PRODNO") as PopupControl;
                PopupControl PD_PRODNO = gv.FindEditRowCellTemplateControl((GridViewDataColumn)gv.Columns["PD_PRODNO"], "txtPD_PRODNO") as PopupControl;

                switch (lblStatus.Text)
                {
                    case "過期":
                        dStart.ClientEnabled = false;
                        PM_PRODNO.Enabled = false;
                        PD_PRODNO.Enabled = false;
                        break;
                    case "有效":
                        dStart.ClientEnabled = false;
                        PM_PRODNO.Enabled = false;
                        PD_PRODNO.Enabled = false;
                        break;
                    case "尚未生效":
                        dStart.ClientEnabled = true;
                        PM_PRODNO.Enabled = true;
                        PD_PRODNO.Enabled = true;
                        break;
                    default:
                        break;
                }
            }
        }

    }

    protected void gvMaster_CommandButtonInitialize(object sender, ASPxGridViewCommandButtonEventArgs e)
    {
        if (e.VisibleIndex > -1)
        {
            if (e.ButtonType == ColumnCommandButtonType.Edit)
            {
                string sStatus = StringUtil.CStr(gvMaster.GetRowValues(e.VisibleIndex, "STATUS"));
                if (sStatus.CompareTo("過期") == 0)
                    e.Enabled = false;
                else
                    e.Enabled = true;
            }
            if (e.ButtonType == ColumnCommandButtonType.SelectCheckbox)
            {
                string sStatus = StringUtil.CStr(gvMaster.GetRowValues(e.VisibleIndex, "STATUS"));
                if (sStatus.CompareTo("尚未生效") == 0)
                    e.Enabled = true;
                else
                    e.Enabled = false;
            }
        }
    }

    protected void gvMaster_RowValidating(object sender, DevExpress.Web.Data.ASPxDataValidationEventArgs e)
    {
        if (e.NewValues["S_DATE"] == null || StringUtil.CStr(e.NewValues["S_DATE"]).Trim() == "")
        {
            e.RowError = "【開始日期】不允許空值，請重新輸入";
            return;
        }
        else
        {
            ASPxLabel lblStatus = gvMaster.FindEditRowCellTemplateControl((GridViewDataColumn)gvMaster.Columns["STATUS"], "lblSTATUS") as ASPxLabel;
            if (Convert.ToDateTime(e.NewValues["S_DATE"]).Date <= DateTime.Now.Date && lblStatus.Text == "尚未生效")
            {
                e.RowError = "【開始日期】必須大於系統日期";
                return;
            }
            else
            {
                if (e.NewValues["E_DATE"] != null && StringUtil.CStr(e.NewValues["E_DATE"]).Trim() != "")
                {
                    if (StringUtil.CStr(e.NewValues["E_DATE"]).Substring(0, 4) != "0001" && Convert.ToDateTime(e.NewValues["E_DATE"]).Date <= DateTime.Now.Date)
                    {
                        e.RowError = "【結束日期】必須大於系統日期";
                        return;
                    }
                    else
                    {
                        if (StringUtil.CStr(e.NewValues["E_DATE"]).Substring(0, 4) != "0001" && Convert.ToDateTime(e.NewValues["E_DATE"]).Date < Convert.ToDateTime(e.NewValues["S_DATE"]).Date)
                        {
                            e.RowError = "【結束日期】必須>=【開始日期】";
                            return;
                        }

                    }
                }
            }
        }

        if (e.NewValues["PD_PRODNO"] == null || StringUtil.CStr(e.NewValues["PD_PRODNO"]).Trim() == "")
        {
            e.RowError = "【搭配商品編號】不允許空值，請重新輸入!";
            return;
        }
        else if (e.NewValues["PM_PRODNO"] == null || StringUtil.CStr(e.NewValues["PM_PRODNO"]).Trim() == "")
        {
            e.RowError = "【主商品編號】不允許空值，請重新輸入!";
            return;
        }
        else if (StringUtil.CStr(e.NewValues["PM_PRODNO"]).Trim().ToUpper() == StringUtil.CStr(e.NewValues["PD_PRODNO"]).Trim().ToUpper())
        {
            e.RowError = "主商品不允許與搭配商品相同，請重新輸入";
            return;
        }
        else if (ORD06_Facade.CheckMainProduct(StringUtil.CStr(e.NewValues["PD_PRODNO"]), StringUtil.CStr(e.NewValues["PM_PRODNO"]), "UPDATE", StringUtil.CStr(e.NewValues["S_DATE"]), StringUtil.CStr(e.NewValues["E_DATE"])) != "")
        {
            e.RowError = "搭配商品己設定為己生效組合的主商品，請重新輸入";
            return;
        }
        else
        {
            if (e.IsNewRow)
            {
                if (ORD06_Facade.CheckMainDetailProduct(StringUtil.CStr(e.NewValues["PM_PRODNO"]), StringUtil.CStr(e.NewValues["PD_PRODNO"]), StringUtil.CStr(e.NewValues["S_DATE"]), StringUtil.CStr(e.NewValues["E_DATE"])) != "")
                {
                    e.RowError = "一搭一組合己存在，請重新輸入";
                    return;
                }
               
            }
            else
            {
                if (StringUtil.CStr(e.OldValues["PM_PRODNO"]) != StringUtil.CStr(e.NewValues["PM_PRODNO"]) && StringUtil.CStr(e.OldValues["PD_PRODNO"]) != StringUtil.CStr(e.NewValues["PD_PRODNO"]))
                {
                    if (ORD06_Facade.CheckMainDetailProduct(StringUtil.CStr(e.NewValues["PM_PRODNO"]), StringUtil.CStr(e.NewValues["PD_PRODNO"]), StringUtil.CStr(e.NewValues["S_DATE"]), StringUtil.CStr(e.NewValues["E_DATE"])) != "")
                    {
                        e.RowError = "一搭一組合己存在，請重新輸入";
                        return;
                    }
                }
            }
        }


    }
    protected void gvMaster_StartRowEditing(object sender, DevExpress.Web.Data.ASPxStartRowEditingEventArgs e)
    {
        gvMaster.Selection.UnselectAll();
    }

    #endregion

    //ajax 呼當前網頁的方式
    [System.Web.Services.WebMethod()]
    [System.Web.Script.Services.ScriptMethod()]
    static public string getProductInfo(string PRODUCT_NO)
    {
        string strInfo = "";
        if (!string.IsNullOrEmpty(PRODUCT_NO))
        {
            DataTable _dt = new Product_Facade().Query_ProductInfo(PRODUCT_NO);
            if (_dt.Rows.Count > 0)
            {
                strInfo = StringUtil.CStr(_dt.Rows[0]["PRODNAME"]);
            }
        }

        return strInfo;
    }

}
