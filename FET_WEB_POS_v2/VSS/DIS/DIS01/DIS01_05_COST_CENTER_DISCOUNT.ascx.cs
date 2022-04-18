using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using DevExpress.Web.ASPxGridView;
using Advtek.Utility;
using FET.POS.Model.Common;
using DevExpress.Web.ASPxEditors;
using FET.POS.Model.Facade.FacadeImpl;
using FET.POS.Model.DTO;
using System.Web.UI.HtmlControls;
using DevExpress.Web.ASPxPopupControl;

public partial class COST_CENTER_DISCOUNT : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
    }

    protected void btnCCDAdd_Click(object sender, EventArgs e)
    {
        if (!gvCCD.IsEditing)
        {
            gvCCD.Selection.UnselectAll();
            gvCCD.AddNewRow();
        }
    }

    protected void btnCCDDelete_Click(object sender, EventArgs e)
    {
        List<object> gvPKValues = gvCCD.GetSelectedFieldValues(gvCCD.KeyFieldName);
        string pkFName = gvCCD.KeyFieldName;

        if (Session["CostCenter"] == null) { return; }
        DataTable dt = new DataTable();

        if (Session["CostCenter"] != null) { dt = (DataTable)Session["CostCenter"]; }

        for (int i = 0; i < gvPKValues.Count; i++)
        {
            if (dt.AsEnumerable().Any(dr => dr.Field<string>("COSTCENTER_DIS_ID").Equals(StringUtil.CStr(gvPKValues[i]))))
            {
                DataRow dr1 = dt.AsEnumerable().Single(dr => dr.Field<string>("COSTCENTER_DIS_ID") == StringUtil.CStr(gvPKValues[i]));
                dt.Rows.Remove(dr1);
            }
        }

        Session["CostCenter"] = dt;
        gvCCD.DataSource = dt;
        gvCCD.DataBind();
    }

    protected void btnCCDTemplate_Click(object sender, EventArgs e)
    {
        string filePath = "../../../Downloads/COSTER.xls";
        HtmlControl fDownload = Page.FindChildControl<HtmlControl>("fDownload");
        ScriptManager.RegisterClientScriptBlock(this,
                                               this.GetType(),
                                               "CCDTemplate",
                                               "document.getElementById('" + fDownload.ClientID + "').src='" + filePath + "';",
                                               true);
    }

    protected void gvCCD_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
    {
        DataTable dt = new DataTable();

        if (Session["CostCenter"] == null)
        {
            dt = getGridViewDataCostCenter();
        }
        else
        {
            dt = (DataTable)Session["CostCenter"];
        }

        PopupControl pop1 = (PopupControl)gvCCD.FindEditRowCellTemplateControl((GridViewDataColumn)gvCCD.Columns["COST_CENTER_NO"], "txtCCDNo");
        string strAccountCode = ((ASPxTextBox)gvCCD.FindEditRowCellTemplateControl((GridViewDataColumn)gvCCD.Columns["ACCOUNTCODE"], "txtAccountCode")).Text;

        string strCCDName = StringUtil.CStr(e.NewValues["PROD_CATEG"]);

        DataRow dr = dt.NewRow();
        dr["COSTCENTER_DIS_ID"] = GuidNo.getUUID();
        dr["COST_CENTER_NO"] = pop1.popTextBox.Text.Trim();
        dr["PROD_CATEGNAME"] = strCCDName; // DIS01_PageHelper.GetProdTypeNameByKey(StringUtil.CStr(e.NewValues["PROD_CATEG"]));
        dr["PROD_CATEG"] = strCCDName; // StringUtil.CStr(e.NewValues["PROD_CATEG"]);
        dr["ACCOUNTCODE"] = getFullAccountCode(StringUtil.CStr(strAccountCode), strCCDName, StringUtil.CStr(dr["COST_CENTER_NO"]));
        dr["AMT"] = StringUtil.CStr(e.NewValues["AMT"]);
        if (e.NewValues["REMARK"] != null)
            dr["REMARK"] = StringUtil.CStr(e.NewValues["REMARK"]);

        dt.Rows.Add(dr);
        Session["CostCenter"] = dt;
        gvCCD.CancelEdit();
        e.Cancel = true;
        gvCCD.DataSource = dt;
        gvCCD.DataBind();
    }

    protected void gvCCD_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
    {
        //若不使用DataSourceID時UPDATE INSERT 和DELPETE 都要自己控制以下為UPDATE時需自行控制
        ASPxGridView grid = (ASPxGridView)sender;
        DataTable dt = Session["CostCenter"] as DataTable ?? null;
        DataRow[] DRSelf = dt.Select("COSTCENTER_DIS_ID='" + StringUtil.CStr(e.Keys[0]).Trim() + "'");
        if (DRSelf.Length > 0)
        {
            string strAccountCode = ((ASPxTextBox)grid.FindEditRowCellTemplateControl((GridViewDataColumn)grid.Columns["ACCOUNTCODE"], "txtAccountCode")).Text;
            string strCCDName = StringUtil.CStr(e.NewValues["PROD_CATEG"]);

            DRSelf[0]["COST_CENTER_NO"] = ((PopupControl)grid.FindEditRowCellTemplateControl((GridViewDataColumn)grid.Columns["COST_CENTER_NO"], "txtCCDNo")).Text;
            DRSelf[0]["PROD_CATEGNAME"] = strCCDName; // DIS01_PageHelper.GetProdTypeNameByKey(StringUtil.CStr(e.NewValues["PROD_CATEG"]));
            DRSelf[0]["PROD_CATEG"] = strCCDName; // StringUtil.CStr(e.NewValues["PROD_CATEG"]);
            DRSelf[0]["ACCOUNTCODE"] = getFullAccountCode(StringUtil.CStr(strAccountCode), strCCDName, StringUtil.CStr(DRSelf[0]["COST_CENTER_NO"]));
            DRSelf[0]["AMT"] = StringUtil.CStr(e.NewValues["AMT"]);
            if (e.NewValues["REMARK"] != null)
                DRSelf[0]["REMARK"] = StringUtil.CStr(e.NewValues["REMARK"]);
        }
        grid.CancelEdit();
        e.Cancel = true;
        grid.DataSource = dt;
        grid.DataBind();
    }

    protected void gvCCD_RowValidating(object sender, DevExpress.Web.Data.ASPxDataValidationEventArgs e)
    {
        PopupControl pop1 = (PopupControl)gvCCD.FindEditRowCellTemplateControl((GridViewDataColumn)gvCCD.Columns["COST_CENTER_NO"], "txtCCDNo");
        string dtProd = DIS01_PageHelper.GetCostCenterNameByKey(pop1.popTextBox.Text);
        if (string.IsNullOrEmpty(dtProd))
        {
            e.RowError += "成本中心不存在!!";
            return;
        }

        ASPxTextBox txtAccountCode = gvCCD.FindEditRowCellTemplateControl((GridViewDataColumn)gvCCD.Columns["ACCOUNTCODE"], "txtAccountCode") as ASPxTextBox;
        if (string.IsNullOrEmpty(txtAccountCode.Text))
        {
            e.RowError += "會計科目不可為空白!!";
            return;
        }

        if (Session["CostCenter"] == null) return;
        DataTable dt = (DataTable)Session["CostCenter"];

        string strCCDName = StringUtil.CStr(e.NewValues["PROD_CATEG"]); // DIS01_PageHelper.GetProdTypeNameByKey(StringUtil.CStr(e.NewValues["PROD_CATEG"]));

        string strID = "";

        if (!e.IsNewRow && gvCCD.IsEditing)
            strID = StringUtil.CStr(e.Keys[0]).Trim();

        if (dt.Rows.Count > 0)
        {
            int rowCount = dt.Rows.Count;
            for (int i = 0; i < rowCount; i++)
            {
                if (strID != StringUtil.CStr(dt.Rows[i]["COSTCENTER_DIS_ID"]) && StringUtil.CStr(dt.Rows[i]["COST_CENTER_NO"]) == pop1.Text && StringUtil.CStr(dt.Rows[i]["PROD_CATEGNAME"]) == strCCDName)
                {
                    e.RowError += "資料重複!!";
                    return;
                }
            }
        }


    }

    protected void gvCCD_StartRowEditing(object sender, DevExpress.Web.Data.ASPxStartRowEditingEventArgs e)
    {
        gvCCD.Selection.UnselectAll();
    }

    protected void gvCCD_CellEditorInitialize(object sender, ASPxGridViewEditorEventArgs e)
    {
        string eName = e.Column.FieldName;
        if (eName == "PROD_CATEG")
        {
            ASPxComboBox cb1 = e.Editor as ASPxComboBox;
            cb1.DataSource = new DIS01_Facade().Query_ProductType("");
            cb1.TextField = "PRODTYPENAME";
            cb1.ValueField = "PRODTYPENO";
            cb1.DataBind();
        }
        else if (eName == "ACCOUNTCODE")
        {
            ASPxTextBox txtAccountCode = gvCCD.FindEditRowCellTemplateControl((GridViewDataColumn)gvCCD.Columns["ACCOUNTCODE"], "txtAccountCode") as ASPxTextBox;
            string CCNO = StringUtil.CStr(gvCCD.GetRowValues(e.VisibleIndex, "COST_CENTER_NO"));
            string Prod_Categ = StringUtil.CStr(gvCCD.GetRowValues(e.VisibleIndex, "PROD_CATEG"));
            string Account = GetAccountCode(StringUtil.CStr(gvCCD.GetRowValues(e.VisibleIndex, "ACCOUNTCODE")));

            string strAccoutNo = new DIS01_Facade().Query_FullAccount_ByProdType(CCNO, Prod_Categ, Account);
            if (!string.IsNullOrEmpty(strAccoutNo))
            {
                txtAccountCode.ClientEnabled = false;
            }
        }
    }

    protected void gvCCD_PageIndexChanged(object sender, EventArgs e)
    {
        bindMasterDataCostCenter();
    }

    protected void ac5_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
    {
        if (Session["BATCH_NO"] == null) { return; }
        DIS01_Facade Facade = new DIS01_Facade();
        string[] strBNO = StringUtil.CStr(Session["BATCH_NO"]).Split(new char[] { ';' });
        DataTable dt = new DataTable();
        DataTable dtTemp = new DataTable();

        DataRow dr = null;
        switch (strBNO[0])
        {
            case "CCenter":
                dt = getGridViewDataCostCenter();
                dtTemp = Facade.Get_UploadTemp(strBNO[1], strBNO[0]);
                if (dtTemp.Rows.Count == 0) { return; }
                string Prod_Categ = "";
                for (int i = 0; i < dtTemp.Rows.Count; i++)
                {
                    Prod_Categ = StringUtil.CStr(dtTemp.Rows[i]["PRODNAME"]);
                    dr = dt.NewRow();
                    dr["COSTCENTER_DIS_ID"] = GuidNo.getUUID();
                    dr["COST_CENTER_NO"] = StringUtil.CStr(dtTemp.Rows[i]["COSTNO"]);
                    dr["PROD_CATEGNAME"] = Prod_Categ;
                    dr["PROD_CATEG"] = Prod_Categ; //StringUtil.CStr(Facade.Query_ProdType_ByName(StringUtil.CStr(dtTemp.Rows[i]["PRODNAME"])).Rows[0]["PRODTYPENO"]);
                    dr["ACCOUNTCODE"] = getFullAccountCode(StringUtil.CStr(dtTemp.Rows[i]["ACCODE"]), Prod_Categ, StringUtil.CStr(dr["COST_CENTER_NO"]));
                    dr["AMT"] = decimal.Parse(StringUtil.CStr(dtTemp.Rows[i]["COSTAMT"]));
                    dr["REMARK"] = StringUtil.CStr(dtTemp.Rows[i]["REMARK"]);
                    dt.Rows.Add(dr);
                }
                Session["CostCenter"] = dt;
                gvCCD.DataSource = dt;
                gvCCD.DataBind();
                break;
        }
    }

    private DataTable getGridViewDataCostCenter()
    {
        DIS01_DiscountMasterDataSet_DTO _DiscountMasterDataSet_DTO = new DIS01_DiscountMasterDataSet_DTO();
        DIS01_DiscountMasterDataSet_DTO.COST_CENTER_DISCOUNTDataTable dtResult;
        dtResult = (DIS01_DiscountMasterDataSet_DTO.COST_CENTER_DISCOUNTDataTable)_DiscountMasterDataSet_DTO.Tables["COST_CENTER_DISCOUNT"];
        dtResult.PrimaryKey = new DataColumn[] { dtResult.Columns["COSTCENTER_DIS_ID"] };
        dtResult.Columns.Add("PROD_CATEGNAME", typeof(string));
        dtResult.AcceptChanges();
        return dtResult;
    }

    private string getFullAccountCode(string ACCOUNTCODE, string PROD_CATEG, string COST_CENTER_NO)
    {
        string strAccoutNo = new DIS01_Facade().Query_FullAccount_ByProdType(COST_CENTER_NO, PROD_CATEG, ACCOUNTCODE);
        if (string.IsNullOrEmpty(strAccoutNo))
        {
            strAccoutNo = "01" + "000" + "0000" + ACCOUNTCODE + COST_CENTER_NO + "0000";
        }

        return strAccoutNo;
    }

    public string GetAccountCode(string AccountCode)
    {
        string strCode = "";
        if (AccountCode != null && AccountCode.Length >= 15)
        {
            strCode = AccountCode.Substring(9, 6);
        }
        return strCode;

    }

    /// <summary>
    /// 擊結資料
    /// </summary>
    public void bindMasterDataCostCenter()
    {
        DataTable dtResult = new DataTable();
        if (Session["CostCenter"] == null)
        {
            dtResult = getGridViewDataCostCenter();
        }
        else
        {
            dtResult = (DataTable)Session["CostCenter"];
        }
        gvCCD.DataSource = dtResult;
        gvCCD.DataBind();
    }

    public bool Enabled
    {
        get
        {
            return this.gvCCD.Enabled;
        }
        set
        {
            this.gvCCD.Enabled = value;
            this.gvCCD.PagerBarEnabled = true;
        }
    }
}
