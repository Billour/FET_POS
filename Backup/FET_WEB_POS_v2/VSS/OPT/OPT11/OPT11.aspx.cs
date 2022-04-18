using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using DevExpress.Web;
using DevExpress.Web.ASPxGridView;
using DevExpress.Web.ASPxPager;
using DevExpress.Web.ASPxEditors;
using Microsoft.VisualBasic;


using Advtek.Utility;
using FET.POS.Model.Common;
using FET.POS.Model.DTO;
using FET.POS.Model.Facade;
using FET.POS.Model.Facade.FacadeImpl;

public partial class VSS_OPT_OPT11 : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack && !IsCallback)
        {
            // 繫結空的資料表，以顯示表頭欄位
            gvMaster.DataSource = new OPT11_HGCONVERTIBLE_DTO().HG_CONVERTIBLE;
            gvMaster.DataBind();


            bindDdlValTxt(ASPxComboBox1, OPT11_PageHelper.GetTypeId(true), "HG_EXCHANGE_TYPE", "HG_EXCHANGE_TYPE_NAME");

            aspComboBoxDefaultSetting();
        }

    }

    protected void bindMasterDataAfterWrite()
    {
        DataTable dtGvMaster = new DataTable();
        dtGvMaster = GetMasterData();
        ViewState["gvMaster"] = dtGvMaster;
        gvMaster.DataSource = dtGvMaster;
        gvMaster.DataBind();
    }

    protected void bindDdlValTxt(ASPxComboBox AspCB, object dataSrc, string valCol, string txtCol)
    {
        AspCB.DataSource = dataSrc;
        AspCB.ValueField = valCol;
        AspCB.TextField = txtCol;
        AspCB.DataBind();
    }

    protected void aspComboBoxDefaultSetting()
    {
        //若都沒選,將Index設為0
        if (ASPxComboBox1.SelectedIndex == -1)
        {
            ASPxComboBox1.SelectedIndex = 0;
        }

    }

    protected void bindMasterData()
    {
        gvMaster.DataSource = ViewState["gvMaster"];
        gvMaster.DataBind();
    }

    private DataTable GetMasterData()
    {
        OPT11_Facade OPT11_Facade = new OPT11_Facade();

        return OPT11_Facade.Query_HGCONVERTIBLE(StringUtil.CStr(ASPxComboBox1.SelectedItem.Value),
                                                txtSDate_S.Text,
                                                txtSDate_E.Text,
                                                TextBox4.Text,
                                                txtMemo1.Text,
                                                txtMemo2.Text,
                                                txtMemo3.Text,
                                                txtMemo4.Text);

    }

    private void BinDiscountProduct()
    {
        ASPxComboBox ddlDiscount = gvMaster.FindEditRowCellTemplateControl((GridViewDataColumn)gvMaster.Columns["CONVERT_NO"], "ddlPartNumberOfDiscount") as ASPxComboBox;
        if (ddlDiscount != null)
        {
            ddlDiscount.DataSource = OPT11_PageHelper.GetDiscountMaster();
            ddlDiscount.DataBind();
        }
    }

    /// <summary>
    /// 判斷期限
    /// </summary>
    /// <param name="sDate">起始日期</param>
    /// <param name="eDate">結束日期</param>
    /// <returns>1.有效 2.已過期 3.尚未生效</returns>
    private string checkDate(string sDate, string eDate)
    {
        string result = "";
        string nowDate = string.Format("{0:yyyyMMdd}", DateTime.Today);

        if (sDate != "")
        {
            if (eDate == "")
            {
                eDate = "9999/12/31";
            }

            if (Convert.ToDateTime(sDate).Date <= DateTime.Now.Date && Convert.ToDateTime(eDate).Date >= DateTime.Now.Date)
            {
                result = "有效";
            }
            else if (Convert.ToDateTime(eDate).Date < DateTime.Now.Date)
            {
                result = "已過期";
            }
            else if (Convert.ToDateTime(sDate).Date > DateTime.Now.Date)
            {
                result = "尚未生效";
            }

        }
        return result;
    }

    #region Button 觸發事件

    protected void btnAddNew_Click(object sender, EventArgs e)
    {
        gvMaster.AddNewRow();
    }

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        List<object> gvPKValues = gvMaster.GetSelectedFieldValues(gvMaster.KeyFieldName);
        string pkFName = gvMaster.KeyFieldName;
        OPT11_HGCONVERTIBLE_DTO OPT11_HgConvertible_DTO = new OPT11_HGCONVERTIBLE_DTO();
        DataTable CustomDataTable = new DataTable();

        CustomDataTable.TableName = OPT11_HgConvertible_DTO.HG_CONVERTIBLE.TableName;

        CustomDataTable.Columns.Add(pkFName, typeof(string));

        if (ViewState["gvMaster"] == null) { return; }
        DataTable dt = new DataTable();
        if (ViewState["gvMaster"] != null) { dt = (DataTable)ViewState["gvMaster"]; };

        for (int i = 0; i < gvPKValues.Count; i++)
        {
            if (dt.AsEnumerable().Any(dr => dr.Field<string>("CONVERT_ID").Equals(StringUtil.CStr(gvPKValues[i]))))
            {
                DataRow CustomDataRow = CustomDataTable.NewRow();
                CustomDataRow[pkFName] = StringUtil.CStr(gvPKValues[i]);
                CustomDataTable.Rows.Add(CustomDataRow);

                DataRow dr1 = dt.Select("CONVERT_ID='" + CustomDataRow[pkFName] + "'")[0];
                dt.Rows.Remove(dr1);
            }

        }

        OPT11_PageHelper.DeleteCutOffDateMethodData(CustomDataTable, pkFName);

        ViewState["gvMaster"] = dt;
        gvMaster.DataSource = dt;
        gvMaster.DataBind();

    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        string msgStr = "";
        gvMaster.Selection.UnselectAll();
        gvMaster.CancelEdit();
        if (txtMemo1.Text != "" && txtMemo2.Text != "")
        {
            if (Convert.ToInt64(txtMemo2.Text) < Convert.ToInt64(txtMemo1.Text))
            {
                msgStr = "[兌點點數訖]不允許小於[兌點點數起]，請重新輸入";
                ScriptManager.RegisterClientScriptBlock(this, typeof(string), "RowUpdating", "alert('" + msgStr + "!');", true);
                return;
            }
        }

        if (txtMemo3.Text != "" && txtMemo4.Text != "")
        {
            if (Convert.ToInt32(txtMemo4.Text) < Convert.ToInt32(txtMemo3.Text))
            {
                msgStr = "[兌換金額訖]不允許小於[兌換金額起]，請重新輸入";
                ScriptManager.RegisterClientScriptBlock(this, typeof(string), "RowUpdating", "alert('" + msgStr + "!');", true);
                return;
            }
        }


        ViewState["gvMaster"] = GetMasterData();
        bindMasterData();
        msgStr = "";
        gvMaster.PageIndex = 0;
        gvMaster.FocusedRowIndex = -1;
    }

    #endregion

    #region gvMaster 觸發事件

    protected void gvMaster_PageIndexChanged(object sender, EventArgs e)
    {
        gvMaster.CancelEdit();
        gvMaster.Selection.UnselectAll();
        ASPxGridView grid = sender as ASPxGridView;
        grid.DataSource = GetMasterData();
        grid.DataBind();
    }

    protected void gvMaster_HtmlRowCreated(object sender, ASPxGridViewTableRowEventArgs e)
    {
        ASPxComboBox ddlPartNumberOfDiscount = gvMaster.FindEditRowCellTemplateControl((GridViewDataColumn)gvMaster.Columns["CONVERT_NO"], "ddlPartNumberOfDiscount") as ASPxComboBox;
        BinDiscountProduct(); //繫結 折扣商品

        //設定全選勾選條件
        if (e.RowType == GridViewRowType.Data)
        {
            //取得起始日
            string sDate = StringUtil.CStr(gvMaster.GetRowValues(e.VisibleIndex, "S_DATE"));
            //取得結束日
            string eDate = StringUtil.CStr(gvMaster.GetRowValues(e.VisibleIndex, "E_DATE"));
            //取得期限
            string limit = checkDate(sDate, eDate);

            e.Row.Attributes["canSelect"] = "false";

            if (limit == "尚未生效")
            {
                e.Row.Attributes["canSelect"] = "true";
            }
        }

        if (e.RowType == GridViewRowType.InlineEdit)
        {
            if (ddlPartNumberOfDiscount != null && e.GetValue("CONVERT_NO") != null)
            {
                ddlPartNumberOfDiscount.Text = StringUtil.CStr(e.GetValue("CONVERT_NO"));
            }

            if (!gvMaster.IsNewRowEditing)
            {
                //取得起始日
                string sDate = StringUtil.CStr(gvMaster.GetRowValues(e.VisibleIndex, "S_DATE"));
                //取得結束日
                string eDate = StringUtil.CStr(gvMaster.GetRowValues(e.VisibleIndex, "E_DATE"));
                //取得期限
                string limit = checkDate(sDate, eDate);

                if (limit == "有效")
                {
                    e.Row.Cells[3].Enabled = false;
                    e.Row.Cells[4].Enabled = false;
                    e.Row.Cells[5].Enabled = false;
                    e.Row.Cells[6].Enabled = false;
                    e.Row.Cells[8].Enabled = false;
                    e.Row.Cells[9].Enabled = false;
                }
            }
        }
    }

    protected void gvMaster_CommandButtonInitialize(object sender, ASPxGridViewCommandButtonEventArgs e)
    {
        if (e.VisibleIndex > -1)
        {
            if (e.ButtonType == ColumnCommandButtonType.Edit || e.ButtonType == ColumnCommandButtonType.SelectCheckbox)
            {
                string E_DATE = StringUtil.CStr(gvMaster.GetRowValues(e.VisibleIndex, "E_DATE"));
                if (!string.IsNullOrEmpty(E_DATE) && Convert.ToDateTime(E_DATE) < DateTime.Today)
                {
                    e.Enabled = false;
                }

                if (e.ButtonType == ColumnCommandButtonType.SelectCheckbox)
                {
                    string date = StringUtil.CStr(gvMaster.GetRowValues(e.VisibleIndex, "S_DATE"));
                    if (!string.IsNullOrEmpty(date) && Convert.ToDateTime(date) <= DateTime.Now)
                    {
                        e.Enabled = false;
                    }

                }
            }
        }
    }

    protected void gvMaster_InitNewRow(object sender, DevExpress.Web.Data.ASPxDataInitNewRowEventArgs e)
    {
        e.NewValues["S_DATE"] = DateTime.Today.AddMonths(1);
        BinDiscountProduct(); //繫結 折扣商品
    }

    protected void gvMaster_StartRowEditing(object sender, DevExpress.Web.Data.ASPxStartRowEditingEventArgs e)
    {
        gvMaster.Selection.UnselectAll();
    }

    protected void gvMaster_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
    {

        OPT11_HGCONVERTIBLE_DTO OPT11_HgConvertible_DTO = new OPT11_HGCONVERTIBLE_DTO();
        OPT11_HGCONVERTIBLE_DTO.HG_CONVERTIBLEDataTable dtSYS;
        OPT11_HGCONVERTIBLE_DTO.HG_CONVERTIBLERow drSYS;
        dtSYS = OPT11_HgConvertible_DTO.Tables["HG_CONVERTIBLE"] as OPT11_HGCONVERTIBLE_DTO.HG_CONVERTIBLEDataTable;
        drSYS = dtSYS.NewHG_CONVERTIBLERow();

        drSYS["CONVERT_ID"] = GuidNo.getUUID();
        drSYS["CONVERT_NO"] = OPT11_Facade.Query_DiscountMasterID(StringUtil.CStr(e.NewValues["CONVERT_NO"]));
        drSYS["CONVERT_NAME"] = e.NewValues["CONVERT_NAME"];
        drSYS["S_DATE"] = (e.NewValues["S_DATE"] != null ? drSYS["S_DATE"] = DateTime.Parse(StringUtil.CStr(e.NewValues["S_DATE"])) : drSYS["S_DATE"] = Advtek.Utility.DateUtil.NullDateFormat("S_DATE"));
        drSYS["E_DATE"] = (e.NewValues["E_DATE"] != null ? drSYS["E_DATE"] = DateTime.Parse(StringUtil.CStr(e.NewValues["E_DATE"])) : drSYS["E_DATE"] = Advtek.Utility.DateUtil.NullDateFormat("E_DATE"));
        drSYS["DIVIDABLE_POINT"] = e.NewValues["DIVIDABLE_POINT"];
        drSYS["CONVERT_CURRENCY"] = e.NewValues["CONVERT_CURRENCY"];
        drSYS["HG_EXCHANGE_TYPE"] = e.NewValues["HG_EXCHANGE_TYPE"];
        drSYS["MODI_USER"] = this.logMsg.OPERATOR;
        drSYS["MODI_DTM"] = Convert.ToDateTime(System.DateTime.Now);
        drSYS["CREATE_USER"] = drSYS["MODI_USER"];
        drSYS["CREATE_DTM"] = drSYS["MODI_DTM"];
        dtSYS.AddHG_CONVERTIBLERow(drSYS);

        OPT11_HgConvertible_DTO.AcceptChanges();

        //更新資料庫
        new OPT11_Facade().Add_HGCONVERTIBLE(OPT11_HgConvertible_DTO);

        ((ASPxGridView)sender).CancelEdit();
        bindMasterDataAfterWrite();
        e.Cancel = true;
    }

    protected void gvMaster_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
    {
        OPT11_HGCONVERTIBLE_DTO OPT11_HgConvertible_DTO = new OPT11_HGCONVERTIBLE_DTO();
        OPT11_HGCONVERTIBLE_DTO.HG_CONVERTIBLEDataTable dtSYS;
        OPT11_HGCONVERTIBLE_DTO.HG_CONVERTIBLERow drSYS;

        dtSYS = OPT11_HgConvertible_DTO.Tables["HG_CONVERTIBLE"] as OPT11_HGCONVERTIBLE_DTO.HG_CONVERTIBLEDataTable;



        dtSYS.Columns["CREATE_USER"].AllowDBNull = true;
        dtSYS.Columns["CREATE_DTM"].AllowDBNull = true;
        drSYS = dtSYS.NewHG_CONVERTIBLERow();
        drSYS["CONVERT_ID"] = StringUtil.CStr(e.Keys["CONVERT_ID"]);
        drSYS["CONVERT_NAME"] = StringUtil.CStr(e.NewValues["CONVERT_NAME"]);
        drSYS["CONVERT_NO"] = OPT11_Facade.Query_DiscountMasterID(StringUtil.CStr(e.NewValues["CONVERT_NO"]));
        drSYS["HG_EXCHANGE_TYPE"] = StringUtil.CStr(e.NewValues["HG_EXCHANGE_TYPE"]);
        drSYS["S_DATE"] = (e.NewValues["S_DATE"] != null ? drSYS["S_DATE"] = DateTime.Parse(StringUtil.CStr(e.NewValues["S_DATE"])) : drSYS["S_DATE"] = Advtek.Utility.DateUtil.NullDateFormat("S_DATE"));
        drSYS["E_DATE"] = (e.NewValues["E_DATE"] != null ? drSYS["E_DATE"] = DateTime.Parse(StringUtil.CStr(e.NewValues["E_DATE"])) : drSYS["E_DATE"] = Advtek.Utility.DateUtil.NullDateFormat("E_DATE"));
        drSYS["DIVIDABLE_POINT"] = StringUtil.CStr(e.NewValues["DIVIDABLE_POINT"]);
        drSYS["CONVERT_CURRENCY"] = StringUtil.CStr(e.NewValues["CONVERT_CURRENCY"]);
        drSYS["MODI_USER"] = this.logMsg.OPERATOR;
        drSYS["MODI_DTM"] = Convert.ToDateTime(System.DateTime.Now);

        dtSYS.AddHG_CONVERTIBLERow(drSYS);

        OPT11_HgConvertible_DTO.AcceptChanges();

        //更新資料庫
        new OPT11_Facade().Update_HGCONVERTIBLE(OPT11_HgConvertible_DTO);


        ((ASPxGridView)sender).CancelEdit();
        e.Cancel = true;

        bindMasterDataAfterWrite();

        e.Cancel = true;

    }

    protected void gvMaster_RowValidating(object sender, DevExpress.Web.Data.ASPxDataValidationEventArgs e)
    {
        if (OPT11_PageHelper.CheckCvNo(OPT11_Facade.Query_DiscountMasterID(StringUtil.CStr(e.NewValues["CONVERT_NO"])), StringUtil.CStr(e.NewValues["HG_EXCHANGE_TYPE"]), e.Keys["CONVERT_ID"]))
        {
            e.RowError = "[折扣料號]已存在，請重新輸入\n";
            return;
        }

        string editStatus = (ViewState["editStatus"] == null ? "" : StringUtil.CStr(ViewState["editStatus"]));

        DateTime SDATE = DateTime.Parse(StringUtil.CStr(e.NewValues["S_DATE"]));
        DateTime EDATE = DateTime.Parse((e.NewValues["E_DATE"] == null ? "9999/12/31" : StringUtil.CStr(e.NewValues["E_DATE"])));

        if (StringUtil.CStr(EDATE.Date) != "9999/12/31")
        {
            if (EDATE < SDATE)
            {
                e.RowError = "結束日期不可小於開始日期!!\n";
                return;
            }
        }
        if (gvMaster.IsNewRowEditing)
        {
            if (SDATE < DateTime.Now)
            {

                e.RowError = "開始日期必須大於今天日期!!\n";
                return;
            }
        }
        if (Convert.ToInt64(e.NewValues["DIVIDABLE_POINT"]) < 1)
        {
            e.RowError = "點數不可小於等於0!\n";
            return;
        }
        if (Convert.ToInt32(e.NewValues["CONVERT_CURRENCY"]) < 1)
        {
            e.RowError = "兌換金額不可小於等於0!\n";
            return;

        }
    }

    #endregion

    //ajax 呼當前網頁的方式
    [System.Web.Services.WebMethod()]
    [System.Web.Script.Services.ScriptMethod()]
    static public string getProductInfo(string PRODNO)
    {
        string strInfo = "";
        if (!string.IsNullOrEmpty(PRODNO))
        {
            strInfo = OPT11_Facade.Query_DiscountMasterID(PRODNO);
        }

        return strInfo;
    }
}

