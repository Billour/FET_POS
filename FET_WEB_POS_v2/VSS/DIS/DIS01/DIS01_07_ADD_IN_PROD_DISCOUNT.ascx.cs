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

public partial class ADD_IN_PROD_DISCOUNT : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void btnAddProdAdd_Click(object sender, EventArgs e)
    {
        if (!gvAddProd.IsEditing)
        {
            gvAddProd.Selection.UnselectAll();
            gvAddProd.AddNewRow();
        }
    }

    protected void btnAddProdDelete_Click(object sender, EventArgs e)
    {
        List<object> gvPKValues = gvAddProd.GetSelectedFieldValues(gvAddProd.KeyFieldName);
        string pkFName = gvAddProd.KeyFieldName;

        if (Session["AddProduct"] == null) { return; }
        DataTable dt = new DataTable();

        if (Session["AddProduct"] != null) { dt = (DataTable)Session["AddProduct"]; }

        for (int i = 0; i < gvPKValues.Count; i++)
        {
            if (dt.AsEnumerable().Any(dr => dr.Field<string>("PRODNO").Equals(StringUtil.CStr(gvPKValues[i]))))
            {
                DataRow dr1 = dt.AsEnumerable().Single(dr => dr.Field<string>("PRODNO") == StringUtil.CStr(gvPKValues[i]));
                dt.Rows.Remove(dr1);
            }
        }

        Session["AddProduct"] = dt;
        gvAddProd.DataSource = dt;
        gvAddProd.DataBind();
    }

    protected void btnAddProdTemplate_Click(object sender, EventArgs e)
    {
        string filePath = "../../../Downloads/AddProd.xls";
        HtmlControl fDownload = Page.FindChildControl<HtmlControl>("fDownload");
        ScriptManager.RegisterClientScriptBlock(this,
                                               this.GetType(),
                                               "AddProdTemplate",
                                               "document.getElementById('" + fDownload.ClientID + "').src='" + filePath + "';",
                                               true);
    }

    protected void gvAddProd_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
    {
        DataTable dt = new DataTable();

        if (Session["AddProduct"] == null)
        {
            dt = getGridViewData2();
        }
        else
        {
            dt = (DataTable)Session["AddProduct"];
        }

        DataRow dr = dt.NewRow();
        PopupControl pop1 = (PopupControl)gvAddProd.FindEditRowCellTemplateControl((GridViewDataColumn)gvAddProd.Columns["PRODNO"], "txtAddProdNo");
        ASPxTextBox tb1 = gvAddProd.FindChildControl<ASPxTextBox>("txtAddProdName");
        ASPxTextBox txtUNIT_PRICE = gvAddProd.FindChildControl<ASPxTextBox>("txtUNIT_PRICE");
        dr["PRODNO"] = pop1.popTextBox.Text.Trim();
        dr["PRODNAME"] = tb1.Text.Trim();
        dr["DIS_AMT"] = StringUtil.CStr(e.NewValues["DIS_AMT"]);
        dr["UNIT_PRICE"] = txtUNIT_PRICE.Text.Trim(); //StringUtil.CStr(e.NewValues["UNIT_PRICE"]);
        dt.Rows.Add(dr);
        Session["AddProduct"] = dt;
        gvAddProd.CancelEdit();
        e.Cancel = true;
        gvAddProd.DataSource = dt;
        gvAddProd.DataBind();
    }

    protected void gvAddProd_RowValidating(object sender, DevExpress.Web.Data.ASPxDataValidationEventArgs e)
    {
        PopupControl pop1 = (PopupControl)gvAddProd.FindEditRowCellTemplateControl((GridViewDataColumn)gvAddProd.Columns["PRODNO"], "txtAddProdNo");
        DataTable dtProd = DIS01_PageHelper.GetProdDataByKey(pop1.popTextBox.Text);
        if (dtProd == null || dtProd.Rows.Count <= 0)
        {
            e.RowError += "商品料號不存在!!";
            return;
        }
        if (Session["AddProduct"] == null) return;
        DataTable dt = (DataTable)Session["AddProduct"];
        string strID = "";

        if (!e.IsNewRow && gvAddProd.IsEditing)
            strID = StringUtil.CStr(e.Keys[0]).Trim();
        if (dt.Rows.Count > 0)
        {
            int rowCount = dt.Rows.Count;
            for (int i = 0; i < rowCount; i++)
            {
                if (strID != StringUtil.CStr(dt.Rows[i]["PRODNO"]) && StringUtil.CStr(dt.Rows[i]["PRODNO"]) == pop1.popTextBox.Text)
                {
                    e.RowError += "商品料號重複!!";
                    return;
                }
            }

        }
    }

    protected void gvAddProd_StartRowEditing(object sender, DevExpress.Web.Data.ASPxStartRowEditingEventArgs e)
    {
        if (gvAddProd.IsEditing)
        {
            gvAddProd.Selection.UnselectAll();
        }
    }

    protected void gvAddProd_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
    {
        //若不使用DataSourceID時UPDATE INSERT 和DELPETE 都要自己控制以下為UPDATE時需自行控制
        ASPxGridView grid = (ASPxGridView)sender;
        DataTable dt = Session["AddProduct"] as DataTable ?? null;
        DataRow[] DRSelf = dt.Select("PRODNO='" + StringUtil.CStr(e.Keys[0]).Trim() + "'");
        if (DRSelf.Length > 0)
        {
            DRSelf[0]["PRODNO"] = ((PopupControl)grid.FindEditRowCellTemplateControl((GridViewDataColumn)grid.Columns["PRODNO"], "txtAddProdNo")).Text;
            DRSelf[0]["PRODNAME"] = ((ASPxTextBox)grid.FindEditRowCellTemplateControl((GridViewDataColumn)grid.Columns["PRODNAME"], "txtAddProdName")).Text;
            DRSelf[0]["DIS_AMT"] = StringUtil.CStr(e.NewValues["DIS_AMT"]);
            DRSelf[0]["UNIT_PRICE"] = ((ASPxTextBox)grid.FindEditRowCellTemplateControl((GridViewDataColumn)grid.Columns["UNIT_PRICE"], "txtUNIT_PRICE")).Text.Trim();
                //StringUtil.CStr(e.NewValues["UNIT_PRICE"]);
        }
        grid.CancelEdit();
        e.Cancel = true;
        grid.DataSource = dt;
        grid.DataBind();
    }

    protected void gvAddProd_PageIndexChanged(object sender, EventArgs e)
    {
        bindMaster2();
    }

    protected void ac7_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
    {
        if (Session["BATCH_NO"] == null) { return; }
        DIS01_Facade Facade = new DIS01_Facade();
        string[] strBNO = StringUtil.CStr(Session["BATCH_NO"]).Split(new char[] { ';' });
        DataTable dt = new DataTable();
        DataTable dtTemp = new DataTable();

        DataRow dr = null;
        switch (strBNO[0])
        {
            case "PRODUCT":
                dt = getGridViewData2();
                dtTemp = Facade.Get_UploadTemp(strBNO[1], strBNO[0]);
                if (dtTemp.Rows.Count == 0) { return; }
                for (int i = 0; i < dtTemp.Rows.Count; i++)
                {
                    dr = dt.NewRow();
                    dr["PRODNO"] = StringUtil.CStr(dtTemp.Rows[i]["PRODNO"]);
                    dr["PRODNAME"] = StringUtil.CStr(dtTemp.Rows[i]["PRODNAME"]);
                    dr["DIS_AMT"] = "0";
                    dr["UNIT_PRICE"] = StringUtil.CStr(dtTemp.Rows[i]["PRICE"]);//"0";

                    dt.Rows.Add(dr);
                }
                Session["AddProduct"] = dt;
                gvAddProd.DataSource = dt;
                gvAddProd.DataBind();
                break;
        }
    }

    private DataTable getGridViewData2()
    {

        DataTable dtResult = new DataTable();
        dtResult.Columns.Add("PRODNO", typeof(string));
        dtResult.Columns.Add("PRODNAME", typeof(string));
        dtResult.Columns.Add("DIS_AMT", typeof(string));
        dtResult.Columns.Add("UNIT_PRICE", typeof(string));

        dtResult.PrimaryKey = new DataColumn[] { dtResult.Columns["PRODNO"] };

        return dtResult;
    }

    /// <summary>
    /// 擊結資料
    /// </summary>
    public void bindMaster2()
    {
        DataTable dtResult = new DataTable();
        if (Session["AddProduct"] == null)
        {
            dtResult = getGridViewData2();
        }
        else
        {
            dtResult = (DataTable)Session["AddProduct"];
        }
        gvAddProd.DataSource = dtResult;
        gvAddProd.DataBind();
    }

    public bool Enabled
    {
        get
        {
            return this.gvAddProd.Enabled;
        }
        set
        {
            this.gvAddProd.Enabled = value;
            this.gvAddProd.PagerBarEnabled = true;
        }
    }
}
