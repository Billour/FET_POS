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
using FET.POS.Model.Facade.FacadeImpl;
using System.Web.UI.HtmlControls;
using Advtek.Utility;

public partial class PROMOTION_DISCOUNT : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void btnPromoAdd_Click(object sender, EventArgs e)
    {
        if (!gvPromo.IsEditing)
        {
            gvPromo.Selection.UnselectAll();
            gvPromo.AddNewRow();
        }
    }

    protected void btnPromoDelete_Click(object sender, EventArgs e)
    {
        List<object> gvPKValues = gvPromo.GetSelectedFieldValues(gvPromo.KeyFieldName);
        string pkFName = gvPromo.KeyFieldName;

        if (Session["Promotion"] == null) { return; }
        DataTable dt = new DataTable();
        if (Session["Promotion"] != null) { dt = (DataTable)Session["Promotion"]; }

        for (int i = 0; i < gvPKValues.Count; i++)
        {
            if (dt.AsEnumerable().Any(dr => dr.Field<string>("PROMO_NO").Equals(StringUtil.CStr(gvPKValues[i]))))
            {
                DataRow dr1 = dt.AsEnumerable().Single(dr => dr.Field<string>("PROMO_NO") == StringUtil.CStr(gvPKValues[i]));
                dt.Rows.Remove(dr1);
            }
        }

        Session["Promotion"] = dt;
        gvPromo.DataSource = dt;
        gvPromo.DataBind();

    }

    protected void btnPromoTemplate_Click(object sender, EventArgs e)
    {
        string filePath = "../../../Downloads/PROMO.xls";
        HtmlControl fDownload = Page.FindChildControl<HtmlControl>("fDownload");
        ScriptManager.RegisterClientScriptBlock(this,
                                               this.GetType(),
                                               "PromoTemplate",
                                               "document.getElementById('" + fDownload.ClientID + "').src='" + filePath + "';",
                                               true);
    }

    protected void gvPromo_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
    {
        DataTable dt = new DataTable();

        if (Session["Promotion"] == null)
        {
            dt = getGridViewDataPromotion();
        }
        else
        {
            dt = (DataTable)Session["Promotion"];
        }


        DataRow dr = dt.NewRow();
        PopupControl pop1 = (PopupControl)gvPromo.FindEditRowCellTemplateControl((GridViewDataColumn)gvPromo.Columns["PROMO_NO"], "txtPromoNo");
        ASPxTextBox tb1 = gvPromo.FindChildControl<ASPxTextBox>("txtPromoName");
        dr["PROMO_NO"] = pop1.popTextBox.Text.Trim();
        dr["PROMO_NAME"] = tb1.Text.Trim();
        dt.Rows.Add(dr);
        Session["Promotion"] = dt;
        gvPromo.CancelEdit();
        e.Cancel = true;
        gvPromo.DataSource = dt;
        gvPromo.DataBind();
    }

    protected void gvPromo_RowValidating(object sender, DevExpress.Web.Data.ASPxDataValidationEventArgs e)
    {
        PopupControl pop1 = (PopupControl)gvPromo.FindEditRowCellTemplateControl((GridViewDataColumn)gvPromo.Columns["PROMO_NO"], "txtPromoNo");
        DataTable dtProd = DIS01_PageHelper.GetPromoDataByKey(pop1.popTextBox.Text);
        if (dtProd == null || dtProd.Rows.Count <= 0)
        {
            e.RowError += "促銷料號不存在!!";
            return;
        }
        if (Session["Promotion"] == null) return;
        DataTable dt = (DataTable)Session["Promotion"];
        string strID = "";

        if (!e.IsNewRow && gvPromo.IsEditing)
            strID = StringUtil.CStr(e.Keys[0]).Trim();
        if (dt.AsEnumerable().Any(dr => dr.Field<string>("PROMO_NO").Equals(pop1.popTextBox.Text) && dr.Field<string>("PROMO_NO") != strID))
        {
            e.RowError += "促銷料號重複!!";
            return;
        }
    }

    protected void gvPromo_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
    {
        //若不使用DataSourceID時UPDATE INSERT 和DELPETE 都要自己控制以下為UPDATE時需自行控制
        ASPxGridView grid = (ASPxGridView)sender;
        DataTable dt = Session["Promotion"] as DataTable ?? null;
        DataRow[] DRSelf = dt.Select("PROMO_NO='" + StringUtil.CStr(e.Keys[0]).Trim() + "'");
        if (DRSelf.Length > 0)
        {
            DRSelf[0]["PROMO_NO"] = ((PopupControl)grid.FindEditRowCellTemplateControl((GridViewDataColumn)grid.Columns["PROMO_NO"], "txtPromoNo")).Text;
            DRSelf[0]["PROMO_NAME"] = ((ASPxTextBox)grid.FindEditRowCellTemplateControl((GridViewDataColumn)grid.Columns["PROMO_NAME"], "txtPromoName")).Text;
        }
        grid.CancelEdit();
        e.Cancel = true;
        grid.DataSource = dt;
        grid.DataBind();
    }

    protected void gvPromo_StartRowEditing(object sender, DevExpress.Web.Data.ASPxStartRowEditingEventArgs e)
    {
        if (gvPromo.IsEditing)
        {
            gvPromo.Selection.UnselectAll();
        }
    }

    protected void gvPromo_PageIndexChanged(object sender, EventArgs e)
    {
        bindMasterDataPromotion();
    }

    protected void ac3_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
    {
        if (Session["BATCH_NO"] == null) { return; }
        DIS01_Facade Facade = new DIS01_Facade();
        string[] strBNO = StringUtil.CStr(Session["BATCH_NO"]).Split(new char[] { ';' });
        DataTable dt = new DataTable();
        DataTable dtTemp = new DataTable();

        DataRow dr = null;
        switch (strBNO[0])
        {
            case "PROMO":
                dt = getGridViewDataPromotion();
                dtTemp = Facade.Get_UploadTemp(strBNO[1], strBNO[0]);
                if (dtTemp.Rows.Count == 0) { return; }
                for (int i = 0; i < dtTemp.Rows.Count; i++)
                {
                    dr = dt.NewRow();
                    dr["PROMO_NO"] = StringUtil.CStr(dtTemp.Rows[i]["PROMO"]);
                    dr["PROMO_NAME"] = StringUtil.CStr(dtTemp.Rows[i]["PROMONAME"]);
                    dt.Rows.Add(dr);
                }
                Session["Promotion"] = dt;
                gvPromo.DataSource = dt;
                gvPromo.DataBind();
                break;
        }
    }

    private DataTable getGridViewDataPromotion()
    {
        DataTable dtResult = new DataTable();
        dtResult.Columns.Add("PROMO_NO", typeof(string));
        dtResult.Columns.Add("PROMO_NAME", typeof(string));
        dtResult.PrimaryKey = new DataColumn[] { dtResult.Columns["PROMO_NO"] };

        return dtResult;
    }

    /// <summary>
    /// 擊結資料
    /// </summary>
    public void bindMasterDataPromotion()
    {
        DataTable dtResult = new DataTable();
        if (Session["Promotion"] == null)
        {
            dtResult = getGridViewDataPromotion();
        }
        else
        {
            dtResult = (DataTable)Session["Promotion"];
        }
        gvPromo.DataSource = dtResult;
        gvPromo.DataBind();
    }

    public bool Enabled
    {
        get
        {
            return this.gvPromo.Enabled;
        }
        set
        {
            this.gvPromo.Enabled = value;
            this.gvPromo.PagerBarEnabled = true;
        }
    }

}
