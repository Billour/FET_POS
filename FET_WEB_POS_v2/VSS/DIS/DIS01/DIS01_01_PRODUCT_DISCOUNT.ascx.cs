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

public partial class PRODUCT_DISCOUNT : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void ac1_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
    {
        if (Session["BATCH_NO"] == null) { return; }
        DIS01_Facade Facade = new DIS01_Facade();
        string[] strBNO = StringUtil.CStr(Session["BATCH_NO"]).Split(new char[] { ';' });

        if (strBNO[0] == "PRODUCT")
        {
            DataTable dt = getGridViewDataProduct();
            DataRow dr = null;

            DataTable dtTemp = Facade.Get_UploadTemp(strBNO[1], strBNO[0]);
            if (dtTemp.Rows.Count == 0) { return; }

            for (int i = 0; i < dtTemp.Rows.Count; i++)
            {
                dr = dt.NewRow();
                dr["PRODNO"] = StringUtil.CStr(dtTemp.Rows[i]["PRODNO"]);
                dr["PRODNAME"] = StringUtil.CStr(dtTemp.Rows[i]["PRODNAME"]);
                dt.Rows.Add(dr);
            }
            Session["Product"] = dt;
            gvProd.DataSource = dt;
            gvProd.DataBind();
        }
    }

    protected void btnProdAdd_Click(object sender, EventArgs e)
    {
        if (!gvProd.IsEditing)
        {
            gvProd.Selection.UnselectAll();
            gvProd.AddNewRow();
        }

    }

    protected void btnProdDelete_Click(object sender, EventArgs e)
    {
        List<object> gvPKValues = gvProd.GetSelectedFieldValues(gvProd.KeyFieldName);
        string pkFName = gvProd.KeyFieldName;

        if (Session["Product"] == null) { return; }
        DataTable dt = new DataTable();

        if (Session["Product"] != null) { dt = (DataTable)Session["Product"]; }

        for (int i = 0; i < gvPKValues.Count; i++)
        {
            if (dt.AsEnumerable().Any(dr => dr.Field<string>("PRODNO").Equals(StringUtil.CStr(gvPKValues[i]))))
            {
                DataRow dr1 = dt.AsEnumerable().Single(dr => dr.Field<string>("PRODNO") == StringUtil.CStr(gvPKValues[i]));
                dt.Rows.Remove(dr1);
            }
        }

        Session["Product"] = dt;
        gvProd.DataSource = dt;
        gvProd.DataBind();

    }

    protected void btnProdTemplate_Click(object sender, EventArgs e)
    {
        string filePath = "../../../Downloads/PRODUCT.xls";
        HtmlControl fDownload = Page.FindChildControl<HtmlControl>("fDownload");
        ScriptManager.RegisterClientScriptBlock(this,
                                               this.GetType(),
                                               "ProdTemplate",
                                               "document.getElementById('" + fDownload.ClientID + "').src='" + filePath + "';",
                                               true);
    }

    protected void gvProd_StartRowEditing(object sender, DevExpress.Web.Data.ASPxStartRowEditingEventArgs e)
    {
        if (gvProd.IsEditing)
        {
            gvProd.Selection.UnselectAll();
        }
    }

    protected void gvProd_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
    {
        DataTable dt = new DataTable();

        if (Session["Product"] == null)
        {
            dt = getGridViewDataProduct();
        }
        else
        {
            dt = (DataTable)Session["Product"];
        }

        DataRow dr = dt.NewRow();
        PopupControl pop1 = (PopupControl)gvProd.FindEditRowCellTemplateControl((GridViewDataColumn)gvProd.Columns["PRODNO"], "txtProdNo");
        ASPxTextBox tb1 = gvProd.FindChildControl<ASPxTextBox>("txtProdName");
        dr["PRODNO"] = pop1.popTextBox.Text.Trim();
        dr["PRODNAME"] = tb1.Text.Trim();
        dt.Rows.Add(dr);
        Session["Product"] = dt;
        gvProd.CancelEdit();
        e.Cancel = true;
        gvProd.DataSource = dt;
        gvProd.DataBind();
    }

    protected void gvProd_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
    {
        //若不使用DataSourceID時UPDATE INSERT 和DELPETE 都要自己控制以下為UPDATE時需自行控制
        ASPxGridView grid = (ASPxGridView)sender;
        DataTable dt = Session["Product"] as DataTable ?? null;
        DataRow[] DRSelf = dt.Select("PRODNO='" + StringUtil.CStr(e.Keys[0]).Trim() + "'");
        if (DRSelf.Length > 0)
        {
            DRSelf[0]["PRODNO"] = ((PopupControl)grid.FindEditRowCellTemplateControl((GridViewDataColumn)grid.Columns["PRODNO"], "txtProdNo")).Text;
            DRSelf[0]["PRODNAME"] = ((ASPxTextBox)grid.FindEditRowCellTemplateControl((GridViewDataColumn)grid.Columns["PRODNAME"], "txtProdName")).Text;
        }
        grid.CancelEdit();
        e.Cancel = true;
        grid.DataSource = dt;
        grid.DataBind();
    }

    protected void gvProd_RowValidating(object sender, DevExpress.Web.Data.ASPxDataValidationEventArgs e)
    {
        PopupControl pop1 = (PopupControl)gvProd.FindEditRowCellTemplateControl((GridViewDataColumn)gvProd.Columns["PRODNO"], "txtProdNo");
        DataTable dtProd = DIS01_PageHelper.GetProdDataByKey(pop1.popTextBox.Text);
        if (dtProd == null || dtProd.Rows.Count <= 0)
        {
            e.RowError += "商品料號不存在!!";
            return;
        }

        if (Session["Product"] == null) return;
        DataTable dt = (DataTable)Session["Product"];
        string strID = "";

        if (!e.IsNewRow && gvProd.IsEditing)
            strID = StringUtil.CStr(e.Keys[0]).Trim();
        if (dt.AsEnumerable().Any(dr => dr.Field<string>("PRODNO").Equals(pop1.popTextBox.Text) && dr.Field<string>("PRODNO") != strID))
        {
            e.RowError += "商品料號重複!!";
            return;
        }
    }

    protected void gvProd_PageIndexChanged(object sender, EventArgs e)
    {
        bindMasterDataProduct();
    }

    private DataTable getGridViewDataProduct()
    {
        DataTable dtResult = new DataTable();
        dtResult.Columns.Add("PRODNO", typeof(string));
        dtResult.Columns.Add("PRODNAME", typeof(string));
        dtResult.PrimaryKey = new DataColumn[] { dtResult.Columns["PRODNO"] };
        return dtResult;
    }

    /// <summary>
    /// 擊結資料
    /// </summary>
    public void bindMasterDataProduct()
    {
        DataTable dtResult = new DataTable();
        if (Session["Product"] == null)
        {
            dtResult = getGridViewDataProduct();
        }
        else
        {
            dtResult = (DataTable)Session["Product"];
        }
        gvProd.DataSource = dtResult;
        gvProd.DataBind();
    }

    public bool Enabled
    {
        get
        {
            return this.gvProd.Enabled;
        }
        set
        {
            this.gvProd.Enabled = value;
            this.gvProd.PagerBarEnabled = true;
        }
    }
}
