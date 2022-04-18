using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using DevExpress.Web.ASPxEditors;
using FET.POS.Model.Facade.FacadeImpl;
using DevExpress.Web.ASPxGridView;
using FET.POS.Model.Common;
using System.Web.UI.HtmlControls;
using Advtek.Utility;

public partial class GIFT_DISCOUNT : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void btnSetProdAdd_Click(object sender, EventArgs e)
    {
        if (!gvSet.IsEditing)
        {
            gvSet.Selection.UnselectAll();
            gvSet.AddNewRow();
        }
    }

    protected void btnSetProdDelete_Click(object sender, EventArgs e)
    {
        List<object> gvPKValues = gvSet.GetSelectedFieldValues(gvSet.KeyFieldName);
        string pkFName = gvSet.KeyFieldName;

        if (Session["SetProduct"] == null) { return; }
        DataTable dt = new DataTable();

        if (Session["SetProduct"] != null) { dt = (DataTable)Session["SetProduct"]; }

        for (int i = 0; i < gvPKValues.Count; i++)
        {
            if (dt.AsEnumerable().Any(dr => dr.Field<string>("PRODNO").Equals(StringUtil.CStr(gvPKValues[i]))))
            {
                DataRow dr1 = dt.AsEnumerable().Single(dr => dr.Field<string>("PRODNO") == StringUtil.CStr(gvPKValues[i]));
                dt.Rows.Remove(dr1);
            }
        }

        Session["SetProduct"] = dt;
        gvSet.DataSource = dt;
        gvSet.DataBind();
    }

    protected void btnSetTemplate_Click(object sender, EventArgs e)
    {
        string filePath = "../../../Downloads/GIFT.xls";
        HtmlControl fDownload = Page.FindChildControl<HtmlControl>("fDownload");
        ScriptManager.RegisterClientScriptBlock(this,
                                               this.GetType(),
                                               "SetTemplate",
                                               "document.getElementById('" + fDownload.ClientID + "').src='" + filePath + "';",
                                               true);
    }

    protected void gvSet_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
    {
        DataTable dt = new DataTable();

        if (Session["SetProduct"] == null)
        {
            dt = getGridViewData1();
        }
        else
        {
            dt = (DataTable)Session["SetProduct"];
        }

        DataRow dr = dt.NewRow();
        PopupControl pop1 = (PopupControl)gvSet.FindEditRowCellTemplateControl((GridViewDataColumn)gvSet.Columns["PRODNO"], "txtSetProdNo");
        ASPxTextBox tb1 = gvSet.FindChildControl<ASPxTextBox>("txtSetProdName");
        dr["PRODNO"] = pop1.popTextBox.Text.Trim();
        dr["PRODNAME"] = tb1.Text.Trim();
        dr["AMT"] = StringUtil.CStr(e.NewValues["AMT"]);

        dt.Rows.Add(dr);
        Session["SetProduct"] = dt;
        gvSet.CancelEdit();
        e.Cancel = true;
        gvSet.DataSource = dt;
        gvSet.DataBind();
    }

    protected void gvSet_RowValidating(object sender, DevExpress.Web.Data.ASPxDataValidationEventArgs e)
    {
        PopupControl pop1 = (PopupControl)gvSet.FindEditRowCellTemplateControl((GridViewDataColumn)gvSet.Columns["PRODNO"], "txtSetProdNo");
        DataTable dtProd = DIS01_PageHelper.GetProdDataByKey(pop1.popTextBox.Text);
        if (dtProd == null || dtProd.Rows.Count <= 0)
        {
            e.RowError += "商品料號不存在!!";
            return;
        }
        if (Session["SetProduct"] == null) return;
        DataTable dt = (DataTable)Session["SetProduct"];
        string strID = "";

        if (!e.IsNewRow && gvSet.IsEditing)
            strID = StringUtil.CStr(e.Keys[0]).Trim();
        if (dt.AsEnumerable().Any(dr => dr.Field<string>("PRODNO").Equals(pop1.popTextBox.Text) && dr.Field<string>("PRODNO") != strID))
        {
            e.RowError += "商品料號重複!!";
            return;
        }
    }

    protected void gvSet_StartRowEditing(object sender, DevExpress.Web.Data.ASPxStartRowEditingEventArgs e)
    {
        if (gvSet.IsEditing)
        {
            gvSet.Selection.UnselectAll();
        }
    }

    protected void gvSet_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
    {
        //若不使用DataSourceID時UPDATE INSERT 和DELPETE 都要自己控制以下為UPDATE時需自行控制
        ASPxGridView grid = (ASPxGridView)sender;
        DataTable dt = Session["SetProduct"] as DataTable ?? null;
        DataRow[] DRSelf = dt.Select("PRODNO='" + StringUtil.CStr(e.Keys[0]).Trim() + "'");
        if (DRSelf.Length > 0)
        {
            DRSelf[0]["PRODNO"] = ((PopupControl)grid.FindEditRowCellTemplateControl((GridViewDataColumn)grid.Columns["PRODNO"], "txtSetProdNo")).Text;
            DRSelf[0]["PRODNAME"] = ((ASPxTextBox)grid.FindEditRowCellTemplateControl((GridViewDataColumn)grid.Columns["PRODNAME"], "txtSetProdName")).Text;
            DRSelf[0]["AMT"] = StringUtil.CStr(e.NewValues["AMT"]);
        }
        grid.CancelEdit();
        e.Cancel = true;
        grid.DataSource = dt;
        grid.DataBind();
    }

    protected void gvSet_PageIndexChanged(object sender, EventArgs e)
    {
        getGridViewData1();
    }

    protected void ac6_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
    {
        if (Session["BATCH_NO"] == null) { return; }
        DIS01_Facade Facade = new DIS01_Facade();
        string[] strBNO = StringUtil.CStr(Session["BATCH_NO"]).Split(new char[] { ';' });
        DataTable dt = new DataTable();
        DataTable dtTemp = new DataTable();

        DataRow dr = null;
        switch (strBNO[0])
        {
            case "GIFT":
                dt = getGridViewData1();
                dtTemp = Facade.Get_UploadTemp(strBNO[1], strBNO[0]);
                if (dtTemp.Rows.Count == 0) { return; }
                for (int i = 0; i < dtTemp.Rows.Count; i++)
                {
                    dr = dt.NewRow();
                    dr["PRODNO"] = StringUtil.CStr(dtTemp.Rows[i]["PRODNO"]);
                    dr["PRODNAME"] = StringUtil.CStr(dtTemp.Rows[i]["PRODNAME"]);
                    dr["AMT"] = StringUtil.CStr(dtTemp.Rows[i]["AMT"]);
                    dt.Rows.Add(dr);
                }
                Session["SetProduct"] = dt;
                gvSet.DataSource = dt;
                gvSet.DataBind();
                break;
        }
    }

    private DataTable getGridViewData1()
    {
        DataTable dtResult = new DataTable();
        dtResult.Columns.Add("PRODNO", typeof(string));
        dtResult.Columns.Add("PRODNAME", typeof(string));
        dtResult.Columns.Add("AMT", typeof(string));
        dtResult.PrimaryKey = new DataColumn[] { dtResult.Columns["PRODNO"] };
        return dtResult;
    }

    /// <summary>
    /// 擊結資料
    /// </summary>
    public void bindMaster1()
    {
        DataTable dtResult = new DataTable();
        if (Session["SetProduct"] == null)
        {
            dtResult = getGridViewData1();
        }
        else
        {
            dtResult = (DataTable)Session["SetProduct"];
        }
        gvSet.DataSource = dtResult;
        gvSet.DataBind();
    }

    public bool Enabled
    {
        get
        {
            return this.gvSet.Enabled;
        }
        set
        {
            this.gvSet.Enabled = value;
            this.gvSet.PagerBarEnabled = true;
        }
    }
}
