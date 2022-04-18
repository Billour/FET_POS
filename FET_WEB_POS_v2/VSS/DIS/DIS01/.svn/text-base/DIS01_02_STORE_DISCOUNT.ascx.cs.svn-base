using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using DevExpress.Web.ASPxEditors;
using DevExpress.Web.ASPxGridView;
using FET.POS.Model.Common;
using DevExpress.Web.ASPxTabControl;
using FET.POS.Model.Facade.FacadeImpl;
using System.Web.UI.HtmlControls;
using Advtek.Utility;

public partial class STORE_DISCOUNT : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void btnStoreAdd_Click(object sender, EventArgs e)
    {
        if (!gvStore.IsEditing)
        {
            gvStore.Selection.UnselectAll();
            gvStore.AddNewRow();
        }

    }

    protected void btnStoreTemplate_Click(object sender, EventArgs e)
    {
        string filePath = "../../../Downloads/STORE.xls";
        HtmlControl fDownload = Page.FindChildControl<HtmlControl>("fDownload");
        ScriptManager.RegisterClientScriptBlock(this,
                                               this.GetType(),
                                               "StoreTemplate",
                                               "document.getElementById('" + fDownload.ClientID + "').src='" + filePath + "';",
                                               true);
    }

    // 均分次數按鈕
    protected void btnTimes_Click(object sender, EventArgs e)
    {
        if (Session["Store"] == null) return;
        DataTable dt = (DataTable)Session["Store"];

        if (dt.Rows.Count > 0)
        {
            ASPxTextBox lblRemainingTimes = (ASPxTextBox)gvStore.FindTitleTemplateControl("lblRemainingTimes");
            string[] strTmp = lblRemainingTimes.Text.Split('：');
            decimal decTmp = decimal.Parse(strTmp[1]);
            ASPxTextBox txtLTNDis = Page.FindChildControl<ASPxTextBox>("txtLTNDis");

            if (decTmp == 0 || (Session["numTmp"] != null && decimal.Parse(StringUtil.CStr(Session["numTmp"])) != decimal.Parse(txtLTNDis.Text)))
            {
                if (!string.IsNullOrEmpty(txtLTNDis.Text.Trim()))
                {

                    decimal avgTmp = (int)(decimal.Parse(((ASPxTextBox)txtLTNDis).Text) / decimal.Parse(StringUtil.CStr(dt.Rows.Count)));

                    int rowCount = dt.Rows.Count;
                    for (int i = 0; i < rowCount; i++)
                    {
                        dt.Rows[i]["DIS_USE_COUNT"] = avgTmp;
                    }
                    Session["Store"] = dt;

                    gvStore.DataSource = dt;
                    gvStore.DataBind();

                    //ASPxTextBox lblRemainingTimes2 = (ASPxTextBox)gvStore.FindTitleTemplateControl("lblRemainingTimes");
                    lblRemainingTimes.Text = "剩餘數量：" + decimal.Parse(txtLTNDis.Text) % decimal.Parse(StringUtil.CStr(dt.Rows.Count));
                    Session["numTmp"] = ((ASPxTextBox)txtLTNDis).Text.Trim();
                }
            }
            else
            {
                int rowCount = dt.Rows.Count;
                for (int i = 0; i < rowCount; i++)
                {
                    if (decTmp > 0)
                        dt.Rows[i]["DIS_USE_COUNT"] = int.Parse(StringUtil.CStr(dt.Rows[i]["DIS_USE_COUNT"])) + 1;

                    decTmp -= 1;
                }
                Session["Store"] = dt;
                gvStore.DataSource = dt;
                gvStore.DataBind();
                lblRemainingTimes.Text = "剩餘數量：0";


            }

            ViewState["lblRemainingTimes"] = lblRemainingTimes.Text;
        }

    }

    protected void btnStoreDelete_Click(object sender, EventArgs e)
    {
        List<object> gvPKValues = gvStore.GetSelectedFieldValues(gvStore.KeyFieldName);
        string pkFName = gvStore.KeyFieldName;

        if (Session["Store"] == null) { return; }
        DataTable dt = new DataTable();

        if (Session["Store"] != null) { dt = (DataTable)Session["Store"]; }

        for (int i = 0; i < gvPKValues.Count; i++)
        {
            if (dt.AsEnumerable().Any(dr => dr.Field<string>("STORE_NO").Equals(StringUtil.CStr(gvPKValues[i]))))
            {
                DataRow dr1 = dt.AsEnumerable().Single(dr => dr.Field<string>("STORE_NO") == StringUtil.CStr(gvPKValues[i]));
                dt.Rows.Remove(dr1);
            }
        }

        Session["Store"] = dt;
        gvStore.DataSource = dt;
        gvStore.DataBind();

    }

    protected void gvStore_InitNewRow(object sender, DevExpress.Web.Data.ASPxDataInitNewRowEventArgs e)
    {
        ASPxComboBox cbLimitTNDis = Page.FindChildControl<ASPxComboBox>("cbLimitTNDis");
        ASPxTextBox txtLTNDis = Page.FindChildControl<ASPxTextBox>("txtLTNDis");

        if (cbLimitTNDis.SelectedIndex == 1) //折扣上限次數
        {
            ASPxTextBox txtDisUseCount = gvStore.FindEditRowCellTemplateControl((GridViewDataColumn)gvStore.Columns["DIS_USE_COUNT"], "txtDisUseCount") as ASPxTextBox;
            txtDisUseCount.Text = txtLTNDis.Text;
            txtDisUseCount.Value = txtLTNDis.Text;
        }
    }

    protected void gvStore_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
    {
        DataTable dt = new DataTable();
        if (Session["Store"] == null)
        {
            dt = getGridViewDataStore();
        }
        else
        {
            dt = Session["Store"] as DataTable;
        }

        DataRow dr = dt.NewRow();
        PopupControl pop1 = (PopupControl)gvStore.FindEditRowCellTemplateControl((GridViewDataColumn)gvStore.Columns["STORE_NO"], "txtStoreNo");
        string[] strStoreInfo = getStoreInfo(pop1.Text.Trim()).Split(';');

        //ASPxTextBox tb1 = gvStore.FindChildControl<ASPxTextBox>("txtStoreName");
        //ASPxTextBox tb2 = gvStore.FindChildControl<ASPxTextBox>("txtZone");
        ASPxTextBox tb3 = gvStore.FindChildControl<ASPxTextBox>("txtDisUseCount");
        dr["STORE_NO"] = pop1.Text.Trim();
        dr["STORENAME"] = strStoreInfo[0].Trim();
        dr["ZONE_NAME"] = strStoreInfo[1].Trim();
        dr["DIS_USE_COUNT"] = tb3.Text.Trim();
        dt.Rows.Add(dr);
        Session["Store"] = dt;
        gvStore.CancelEdit();
        e.Cancel = true;
        gvStore.DataSource = dt;
        gvStore.DataBind();
    }

    protected void gvStore_RowValidating(object sender, DevExpress.Web.Data.ASPxDataValidationEventArgs e)
    {
        PopupControl pop1 = (PopupControl)gvStore.FindEditRowCellTemplateControl((GridViewDataColumn)gvStore.Columns["STORE_NO"], "txtStoreNo");
        DataTable dtStore = DIS01_PageHelper.GetStoreDataByKey(pop1.Text);
        if (dtStore.Rows.Count == 0)
        {
            e.RowError += "門市編號不存在!!";
            return;
        }
        string strCount = ((ASPxTextBox)gvStore.FindEditRowCellTemplateControl((GridViewDataColumn)gvStore.Columns["DIS_USE_COUNT"], "txtDisUseCount")).Text;
        decimal intCount = decimal.Parse(strCount == "" ? "-1" : strCount);
        if (intCount < 0)
        {
            e.RowError += "折扣上限次數不允許小於0，請重新輸入!!";
            return;
        }
        if (Session["Store"] == null) return;
        DataTable dt = (DataTable)Session["Store"];
        string strID = "";
        if (!e.IsNewRow && gvStore.IsEditing)
            strID = StringUtil.CStr(e.Keys[0]).Trim();
        if (dt.Rows.Count > 0)
        {
            int rowCount = dt.Rows.Count;
            for (int i = 0; i < rowCount; i++)
            {
                if (strID != StringUtil.CStr(dt.Rows[i]["STORE_NO"]) && StringUtil.CStr(dt.Rows[i]["STORE_NO"]) == pop1.popTextBox.Text)
                {
                    e.RowError += "門市編號重複!!";
                    return;
                }
            }

        }

    }

    protected void gvStore_StartRowEditing(object sender, DevExpress.Web.Data.ASPxStartRowEditingEventArgs e)
    {
        gvStore.Selection.UnselectAll();
    }

    protected void gvStore_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
    {
        ASPxGridView grid = (ASPxGridView)sender;
        DataTable dt = Session["Store"] as DataTable ?? null;
        DataRow[] DRSelf = dt.Select("STORE_NO='" + StringUtil.CStr(e.Keys[0]).Trim() + "'");
        if (DRSelf.Length > 0)
        {
            PopupControl pop1 = (PopupControl)gvStore.FindEditRowCellTemplateControl((GridViewDataColumn)gvStore.Columns["STORE_NO"], "txtStoreNo");
            string[] strStoreInfo = getStoreInfo(pop1.Text.Trim()).Split(';');

            DRSelf[0]["STORE_NO"] = pop1.Text.Trim();
            DRSelf[0]["STORENAME"] = strStoreInfo[0].Trim();
            DRSelf[0]["ZONE_NAME"] = strStoreInfo[1].Trim();
            DRSelf[0]["DIS_USE_COUNT"] = ((ASPxTextBox)grid.FindEditRowCellTemplateControl((GridViewDataColumn)grid.Columns["DIS_USE_COUNT"], "txtDisUseCount")).Text;
        }
        grid.CancelEdit();
        e.Cancel = true;
        grid.DataSource = dt;
        grid.DataBind();
    }

    //指定門市頁面重Lond[均分次數]按鈕顯示隱藏處理
    protected void gvStore_PreRender(object sender, EventArgs e)
    {
        ASPxComboBox cbLimitTNDis = Page.FindChildControl<ASPxComboBox>("cbLimitTNDis");
        ASPxTextBox lblRemainingTimes = (ASPxTextBox)gvStore.FindTitleTemplateControl("lblRemainingTimes");

        string strLimitTND = StringUtil.CStr(cbLimitTNDis.Value);

        if (strLimitTND == "4")  //均分
        {
            if (ViewState["lblRemainingTimes"] != null) lblRemainingTimes.Text = StringUtil.CStr(ViewState["lblRemainingTimes"]);
        }
    }

    protected void gvStore_PageIndexChanged(object sender, EventArgs e)
    {
        bindMasterDataStore();
    }

    protected void ac2_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
    {
        if (Session["BATCH_NO"] == null) { return; }
        DIS01_Facade Facade = new DIS01_Facade();
        string[] strBNO = StringUtil.CStr(Session["BATCH_NO"]).Split(new char[] { ';' });
        DataTable dt = new DataTable();
        DataTable dtTemp = new DataTable();

        //** 2011/03/07 Tina：Issue 722 => 折扣上限次數為指定時，資料匯入後，要將指定總量帶入
        ASPxComboBox cbLimitTNDis = Page.FindChildControl<ASPxComboBox>("cbLimitTNDis");
        string strLTNDis = "0";
        if (StringUtil.CStr(cbLimitTNDis.Value) == "2")  //指定
        {
            ASPxTextBox txtLTNDis = Page.FindChildControl<ASPxTextBox>("txtLTNDis");
            if (!string.IsNullOrEmpty(txtLTNDis.Text))
            {
                strLTNDis = txtLTNDis.Text;
            }
        }

        DataRow dr = null;
        switch (strBNO[0])
        {
            case "STORE":
                dt = getGridViewDataStore();
                dtTemp = Facade.Get_UploadTemp(strBNO[1], strBNO[0]);
                if (dtTemp.Rows.Count == 0) { return; }
                for (int i = 0; i < dtTemp.Rows.Count; i++)
                {
                    dr = dt.NewRow();
                    dr["STORE_NO"] = StringUtil.CStr(dtTemp.Rows[i]["STORENO"]);
                    dr["STORENAME"] = StringUtil.CStr(dtTemp.Rows[i]["STORENAME"]);
                    dr["ZONE_NAME"] = StringUtil.CStr(dtTemp.Rows[i]["ZONENAME"]);
                    dr["DIS_USE_COUNT"] = strLTNDis;
                    dt.Rows.Add(dr);
                }
                Session["Store"] = dt;
                gvStore.DataSource = dt;
                gvStore.DataBind();
                break;
        }
    }

    private DataTable getGridViewDataStore()
    {
        DataTable dtResult = new DataTable();
        dtResult.Columns.Add("STORE_NO", typeof(string));
        dtResult.Columns.Add("STORENAME", typeof(string));
        dtResult.Columns.Add("ZONE_NAME", typeof(string));
        dtResult.Columns.Add("DIS_USE_COUNT", typeof(string));
        dtResult.PrimaryKey = new DataColumn[] { dtResult.Columns["STORE_NO"] };
        return dtResult;
    }

    /// <summary>
    /// 擊結資料
    /// </summary>
    public void bindMasterDataStore()
    {
        DataTable dtResult = new DataTable();
        if (Session["Store"] == null)
        {
            dtResult = getGridViewDataStore();
        }
        else
        {
            dtResult = (DataTable)Session["Store"];
        }
        gvStore.DataSource = dtResult;
        gvStore.DataBind();

        ASPxComboBox cbLimitTNDis = Page.FindChildControl<ASPxComboBox>("cbLimitTNDis");
        string strLimitTND = StringUtil.CStr(cbLimitTNDis.Value);
        if (strLimitTND == "4")  //均分
        {
            ASPxTextBox lblRemainingTimes = (ASPxTextBox)gvStore.FindTitleTemplateControl("lblRemainingTimes");
            if (lblRemainingTimes != null)
            {
                ASPxTextBox txtLTNDis = Page.FindChildControl<ASPxTextBox>("txtLTNDis");
                string Sum = StringUtil.CStr(dtResult.Compute("Sum(DIS_USE_COUNT)", ""));
                Decimal Total_DIS_USE_COUNT = decimal.Parse(string.IsNullOrEmpty(Sum) ? "0" : Sum);
                lblRemainingTimes.Text = "剩餘數量：" + StringUtil.CStr(decimal.Parse(txtLTNDis.Text) - Total_DIS_USE_COUNT);
            }
        }

    }

    public bool Enabled
    {
        get
        {
            return this.gvStore.Enabled;
        }
        set
        {
            this.gvStore.Enabled = value;
            this.gvStore.PagerBarEnabled = true;
        }
    }

    private string getStoreInfo(string STORE_NO)
    {
        string strInfo = "";
        if (!string.IsNullOrEmpty(STORE_NO))
        {
            DataTable dtStore = DIS01_PageHelper.GetStoreDataByKey(STORE_NO);
            if (dtStore.Rows.Count > 0)
            {
                strInfo = StringUtil.CStr(dtStore.Rows[0]["STORENAME"]) + ";" + StringUtil.CStr(dtStore.Rows[0]["ZONE_NAME"]);
            }
        }

        return strInfo;
    }

}
