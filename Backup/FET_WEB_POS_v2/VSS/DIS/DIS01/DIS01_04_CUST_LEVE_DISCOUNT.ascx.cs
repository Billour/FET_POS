using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.Web.ASPxGridView;
using System.Data;
using FET.POS.Model.DTO;
using Advtek.Utility;
using FET.POS.Model.Facade.FacadeImpl;
using System.Web.UI.HtmlControls;

public partial class CUST_LEVE_DISCOUNT : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    //protected void rbCustomer_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    if (rbCustomer.SelectedValue == "1")
    //    {
    //        gv1.Selection.UnselectAll();
    //        gv1.Visible = true;
    //        gv2.Visible = false;
    //        bindMasterDataCustomer1();
    //    }
    //    else
    //    {

    //        gv1.Visible = false;
    //        gv2.Visible = true;
    //        gv2.Selection.UnselectAll();
    //        bindMasterDataCustomer2();
    //    }
    //}

    #region Button 觸發的事件

    protected void btngv1Add_Click(object sender, EventArgs e)
    {
        if (!gv1.IsEditing)
        {
            gv1.Selection.UnselectAll();
            gv1.AddNewRow();
        }

        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "rbCustomer", "rbCustomer_ValueChanged();", true);

    }

    protected void btngv2Add_Click(object sender, EventArgs e)
    {
        if (!gv2.IsEditing)
        {
            gv2.Selection.UnselectAll();
            gv2.AddNewRow();
        }

        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "rbCustomer", "rbCustomer_ValueChanged();", true);

    }

    protected void btngv1Delete_Click(object sender, EventArgs e)
    {
        List<object> gvPKValues = gv1.GetSelectedFieldValues(gv1.KeyFieldName);
        string pkFName = gv1.KeyFieldName;

        if (Session["Customer1"] == null) { return; }
        DataTable dt = new DataTable();
        if (Session["Customer1"] != null) { dt = (DataTable)Session["Customer1"]; };

        for (int i = 0; i < gvPKValues.Count; i++)
        {
            if (dt.AsEnumerable().Any(dr => dr.Field<string>("CUST_LEVEL_ID").Equals(StringUtil.CStr(gvPKValues[i]))))
            {
                DataRow dr1 = dt.AsEnumerable().Single(dr => dr.Field<string>("CUST_LEVEL_ID") == StringUtil.CStr(gvPKValues[i]));
                dt.Rows.Remove(dr1);
            }
        }

        Session["Customer1"] = dt;
        gv1.DataSource = dt;
        gv1.DataBind();

        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "rbCustomer", "rbCustomer_ValueChanged();", true);

    }

    protected void btngv2Delete_Click(object sender, EventArgs e)
    {
        List<object> gvPKValues = gv2.GetSelectedFieldValues(gv2.KeyFieldName);
        string pkFName = gv2.KeyFieldName;

        if (Session["Customer2"] == null) { return; }
        DataTable dt = new DataTable();

        if (Session["Customer2"] != null) { dt = (DataTable)Session["Customer2"]; }

        for (int i = 0; i < gvPKValues.Count; i++)
        {
            if (dt.AsEnumerable().Any(dr => dr.Field<string>("CUST_LEVEL_ID").Equals(StringUtil.CStr(gvPKValues[i]))))
            {
                DataRow dr1 = dt.AsEnumerable().Single(dr => dr.Field<string>("CUST_LEVEL_ID") == StringUtil.CStr(gvPKValues[i]));
                dt.Rows.Remove(dr1);
            }
        }

        Session["Customer2"] = dt;
        gv2.DataSource = dt;
        gv2.DataBind();

        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "rbCustomer", "rbCustomer_ValueChanged();", true);

    }

    protected void btnCustomerTemplate_Click(object sender, EventArgs e)
    {
        string filePath = "../../../Downloads/Mobile.xls";
        HtmlControl fDownload = Page.FindChildControl<HtmlControl>("fDownload");
        ScriptManager.RegisterClientScriptBlock(this,
                                               this.GetType(),
                                               "CustomerTemplate",
                                               "document.getElementById('" + fDownload.ClientID + "').src='" + filePath + "'; rbCustomer_ValueChanged();",
                                               true);
    }

    #endregion

    #region GV1_客戶等級 觸發的事件

    protected void gv1_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
    {
        DataTable dt = new DataTable();
        if (Session["Customer1"] == null)
        {
            dt = getGridViewDataCustomer1();
        }
        else
        {
            dt = (DataTable)Session["Customer1"];
        }

        DataRow dr = dt.NewRow();
        dr["CUST_LEVEL_ID"] = GuidNo.getUUID();
        dr["USE_TYPE"] = "1";
        dr["ARPB_S"] = StringUtil.CStr(e.NewValues["ARPB_S"]);
        dr["ARPB_E"] = StringUtil.CStr(e.NewValues["ARPB_E"]);
        dt.Rows.Add(dr);
        Session["Customer1"] = dt;
        gv1.CancelEdit();
        e.Cancel = true;
        gv1.DataSource = dt;
        gv1.DataBind();
    }

    protected void gv1_RowValidating(object sender, DevExpress.Web.Data.ASPxDataValidationEventArgs e)
    {

        decimal intS = decimal.Parse(StringUtil.CStr(e.NewValues["ARPB_S"]));
        decimal intE = decimal.Parse(StringUtil.CStr(e.NewValues["ARPB_E"]));
        if (intE < intS) { e.RowError += "訖不可小於起!!"; return; }
        if (Session["Customer1"] == null) return;
        DataTable dt = (DataTable)Session["Customer1"];

        string strID = "";

        if (!e.IsNewRow && gv1.IsEditing)
            strID = StringUtil.CStr(e.Keys[0]).Trim();


        if (dt.AsEnumerable().Any(dr => dr.Field<decimal>("ARPB_S") <= intS && dr.Field<decimal>("ARPB_E") >= intS && dr.Field<string>("CUST_LEVEL_ID") != strID))
        {
            e.RowError += "數值重複!!";
            return;
        }
        if (dt.AsEnumerable().Any(dr => dr.Field<decimal>("ARPB_S") <= intE && dr.Field<decimal>("ARPB_E") >= intE && dr.Field<string>("CUST_LEVEL_ID") != strID))
        {
            e.RowError += "數值重複!!";
            return;
        }
        if (dt.AsEnumerable().Any(dr => intS <= dr.Field<decimal>("ARPB_S") && intE >= dr.Field<decimal>("ARPB_S") && dr.Field<string>("CUST_LEVEL_ID") != strID))
        {
            e.RowError += "數值重複!!";
            return;
        }
        if (dt.AsEnumerable().Any(dr => intS <= dr.Field<decimal>("ARPB_E") && intE >= dr.Field<decimal>("ARPB_E") && dr.Field<string>("CUST_LEVEL_ID") != strID))
        {
            e.RowError += "數值重複!!";
            return;
        }

    }

    protected void gv1_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
    {
        //若不使用DataSourceID時UPDATE INSERT 和DELPETE 都要自己控制以下為UPDATE時需自行控制
        ASPxGridView grid = (ASPxGridView)sender;
        DataTable dt = Session["Customer1"] as DataTable ?? null;
        DataRow[] DRSelf = dt.Select("CUST_LEVEL_ID='" + StringUtil.CStr(e.Keys[0]).Trim() + "'");
        if (DRSelf.Length > 0)
        {
            DRSelf[0]["ARPB_S"] = StringUtil.CStr(e.NewValues["ARPB_S"]);
            DRSelf[0]["ARPB_E"] = StringUtil.CStr(e.NewValues["ARPB_E"]);
        }
        grid.CancelEdit();
        e.Cancel = true;
        grid.DataSource = dt;
        grid.DataBind();
    }

    protected void gv1_StartRowEditing(object sender, DevExpress.Web.Data.ASPxStartRowEditingEventArgs e)
    {
        if (gv1.IsEditing)
        {
            gv1.Selection.UnselectAll();
        }
    }

    protected void gv1_PageIndexChanged(object sender, EventArgs e)
    {
        bindMasterDataCustomer1();
    }

    #endregion

    #region GV2_名單 觸發的事件

    protected void gv2_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
    {
        DataTable dt = new DataTable();

        if (Session["Customer2"] == null)
        {
            dt = getGridViewDataCustomer2();
        }
        else
        {
            dt = (DataTable)Session["Customer2"];

        }


        DataRow dr = dt.NewRow();
        dr["CUST_LEVEL_ID"] = GuidNo.getUUID();
        dr["USE_TYPE"] = "2";
        dr["MSISDN"] = StringUtil.CStr(e.NewValues["MSISDN"]);
        dt.Rows.Add(dr);
        Session["Customer2"] = dt;
        gv2.CancelEdit();
        e.Cancel = true;
        gv2.DataSource = dt;
        gv2.DataBind();
    }

    protected void gv2_RowValidating(object sender, DevExpress.Web.Data.ASPxDataValidationEventArgs e)
    {
        if (Session["Customer2"] == null) return;
        DataTable dt = (DataTable)Session["Customer2"];
        string strID = "";

        if (!e.IsNewRow && gv2.IsEditing)
            strID = StringUtil.CStr(e.Keys[0]).Trim();
        if (dt.AsEnumerable().Any(dr => dr.Field<string>("MSISDN").Equals(StringUtil.CStr(e.NewValues["MSISDN"])) && dr.Field<string>("CUST_LEVEL_ID") != strID))
        {
            e.RowError += "客戶門號重複!!";
            return;
        }
    }

    protected void gv2_StartRowEditing(object sender, DevExpress.Web.Data.ASPxStartRowEditingEventArgs e)
    {
        if (gv2.IsEditing)
        {
            gv2.Selection.UnselectAll();
        }
    }

    protected void gv2_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
    {
        //若不使用DataSourceID時UPDATE INSERT 和DELPETE 都要自己控制以下為UPDATE時需自行控制
        ASPxGridView grid = (ASPxGridView)sender;
        DataTable dt = Session["Customer2"] as DataTable ?? null;
        DataRow[] DRSelf = dt.Select("CUST_LEVEL_ID='" + StringUtil.CStr(e.Keys[0]).Trim() + "'");
        if (DRSelf.Length > 0)
        {
            DRSelf[0]["MSISDN"] = StringUtil.CStr(e.NewValues["MSISDN"]);
        }
        grid.CancelEdit();
        e.Cancel = true;
        grid.DataSource = dt;
        grid.DataBind();
    }

    protected void gv2_PageIndexChanged(object sender, EventArgs e)
    {
        bindMasterDataCustomer2();
    }

    #endregion

    protected void ac4_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
    {
        if (Session["BATCH_NO"] == null) { return; }
        DIS01_Facade Facade = new DIS01_Facade();
        string[] strBNO = StringUtil.CStr(Session["BATCH_NO"]).Split(new char[] { ';' });
        DataTable dt = new DataTable();
        DataTable dtTemp = new DataTable();

        DataRow dr = null;
        switch (strBNO[0])
        {
            case "CUSTMOBIL":
                dt = getGridViewDataCustomer2();
                dtTemp = Facade.Get_UploadTemp(strBNO[1], strBNO[0]);
                if (dtTemp.Rows.Count == 0) { return; }
                for (int i = 0; i < dtTemp.Rows.Count; i++)
                {
                    dr = dt.NewRow();
                    dr["CUST_LEVEL_ID"] = GuidNo.getUUID();
                    dr["MSISDN"] = StringUtil.CStr(dtTemp.Rows[i]["CUSTMO"]);
                    dr["USE_TYPE"] = "2";
                    dt.Rows.Add(dr);
                }
                Session["Customer2"] = dt;
                gv2.DataSource = dt;
                gv2.DataBind();
                break;
        }
    }

    /// <summary>
    /// 擊結【客戶等級】資料
    /// </summary>
    public void bindMasterDataCustomer1()
    {
        DataTable dtResult = new DataTable();
        if (Session["Customer1"] == null)
        {
            dtResult = getGridViewDataCustomer1();
        }
        else
        {
            dtResult = (DataTable)Session["Customer1"];
        }
        gv1.DataSource = dtResult;
        gv1.DataBind();
    }

    /// <summary>
    /// 擊結【名單】資料
    /// </summary>
    public void bindMasterDataCustomer2()
    {
        DataTable dtResult = new DataTable();

        if (Session["Customer2"] == null)
        {
            dtResult = getGridViewDataCustomer2();
        }
        else
        {
            dtResult = (DataTable)Session["Customer2"];
        }
        gv2.DataSource = dtResult;
        gv2.DataBind();
    }

    private DataTable getGridViewDataCustomer1()
    {
        DIS01_DiscountMasterDataSet_DTO _DiscountMasterDataSet_DTO = new DIS01_DiscountMasterDataSet_DTO();
        DIS01_DiscountMasterDataSet_DTO.CUST_LEVE_DISCOUNTDataTable dtResult;
        dtResult = (DIS01_DiscountMasterDataSet_DTO.CUST_LEVE_DISCOUNTDataTable)_DiscountMasterDataSet_DTO.Tables["CUST_LEVE_DISCOUNT"];
        dtResult.PrimaryKey = new DataColumn[] { dtResult.Columns["CUST_LEVEL_ID"] };
        return dtResult;
    }

    private DataTable getGridViewDataCustomer2()
    {
        DIS01_DiscountMasterDataSet_DTO _DiscountMasterDataSet_DTO = new DIS01_DiscountMasterDataSet_DTO();
        DIS01_DiscountMasterDataSet_DTO.CUST_LEVE_DISCOUNTDataTable dtResult;
        dtResult = (DIS01_DiscountMasterDataSet_DTO.CUST_LEVE_DISCOUNTDataTable)_DiscountMasterDataSet_DTO.Tables["CUST_LEVE_DISCOUNT"];
        dtResult.PrimaryKey = new DataColumn[] { dtResult.Columns["CUST_LEVEL_ID"] };
        return dtResult;
    }

    public bool Enabled
    {
        get
        {
            return this.gv1.Enabled;
        }
        set
        {
            this.gv1.Enabled = value;
            this.gv1.PagerBarEnabled = true;

            this.gv2.Enabled = value;
            this.gv2.PagerBarEnabled = true;

        }
    }
}
