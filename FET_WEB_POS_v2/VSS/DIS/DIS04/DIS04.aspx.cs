using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Collections;
using DevExpress.Web.ASPxGridView;
using DevExpress.Web.ASPxPager;
using DevExpress.Web.ASPxEditors;

using Advtek.Utility;
using FET.POS.Model.Common;
using FET.POS.Model.DTO;
using FET.POS.Model.Facade;
using FET.POS.Model.Facade.FacadeImpl;

public partial class VSS_DIS_DIS04 : BasePage
{
    ASPxGridView[] grids = null;

    protected void Page_Load(object sender, EventArgs e)
    {
        grids = new ASPxGridView[] { this.gvDetail1, this.gvDetail2, this.gvDetail3, this.gvDetail4, this.gvDetail5, this.gvDetail6 };

        if (!IsPostBack && !IsCallback)
        {
            // 繫結空的資料表，以顯示表頭欄位
            gvMaster.DataSource = getMasterEmptyData();
            gvMaster.DataBind();
        }
    }

    private void bindMasterData()
    {
        gvMaster.DataSource = getMasterData();
        gvMaster.DataBind();
    }

    private DataTable getMasterEmptyData()
    {
        DataTable dtMaster = new DataTable();
        dtMaster.Columns.Clear();
        dtMaster.Columns.Add("ITEM_NO", typeof(int));
        dtMaster.Columns.Add("FLAG", typeof(int));
        dtMaster.Columns.Add("PROMO_NO", typeof(string));
        dtMaster.Columns.Add("PROMO_NAME", typeof(string));
        dtMaster.Columns.Add("MODI_USER", typeof(string));
        dtMaster.Columns.Add("MODI_DTM", typeof(string));
        return dtMaster;
    }

    private DataTable getMasterData()
    {
        return new DIS04_Facade().Query_ProdRelationMethodSet(this.txtRelationshipNo.Text,
                                                        this.txtRelationshipName.Text,
                                                        this.ASPxTextBox1.Text,
                                                        this.ASPxTextBox2.Text,
                                                        this.ASPxTextBox3.Text,
                                                        this.ASPxTextBox4.Text);
    }

    private DataTable getDetailData(string flag, int _group)
    {
        return new DIS04_Facade().Query_GroupMethodSet(flag, StringUtil.CStr(_group));
    }

    protected void BindDetailData(ASPxGridView gv, int _gvgroup)
    {
        string flag = StringUtil.CStr(gvMaster.GetRowValues(gvMaster.FocusedRowIndex, "PROMO_NO"));

        DataTable dtResult = new DataTable();
        dtResult = getDetailData(flag, _gvgroup);
        gv.Columns.Clear();
        gv.AutoGenerateColumns = true;
        if (dtResult.Rows.Count > 0)
        {
            gv.DataSource = dtResult;
            gv.DataBind();
            if (StringUtil.CStr(dtResult.Rows[0]["商品料號"]).Trim() == "" || dtResult.Rows[0]["商品料號"] == null)
            {
                gv.KeyFieldName = StringUtil.CStr(dtResult.Rows[0]["商品類別"]);
                gv.Columns[1].Visible = false;
                gv.Columns[2].Visible = false;
                gv.Columns[3].Visible = true;
                gv.Columns[4].Visible = true;
            }
            else
            {
                gv.KeyFieldName = StringUtil.CStr(dtResult.Rows[0]["商品料號"]);
                gv.Columns[1].Visible = true;
                gv.Columns[2].Visible = true;
                gv.Columns[3].Visible = false;
                gv.Columns[4].Visible = false;
            }

        }
        else
        {
            dtResult = new DataTable();
            dtResult.Columns.Add("項次", typeof(string));
            dtResult.Columns.Add("商品料號", typeof(string));
            dtResult.Columns.Add("商品名稱", typeof(string));
            gv.DataSource = dtResult;
            gv.DataBind();
            gv.KeyFieldName = "商品料號";

        }

    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        bindMasterData();
        gvMaster.FocusedRowIndex = -1;

        this.ASPxPageControl1.Visible = false;
    }

    protected void gvMaster_PageIndexChanged(object sender, EventArgs e)
    {
        ASPxGridView grid = sender as ASPxGridView;
        grid.DataSource = getMasterData();
        grid.DataBind();
    }

    protected void gvMaster_FocusedRowChanged(object sender, EventArgs e)
    {
        if (gvMaster.FocusedRowIndex >= 0)
        {
            this.ASPxPageControl1.Visible = true;
            this.ASPxPageControl1.ActiveTabIndex = 0;
            BindDetailData(grids[0], 1);
        }
    }

    protected void gvDetail_PageIndexChanged(object sender, EventArgs e)
    {
        ASPxGridView grid = sender as ASPxGridView;
        grid.DataSource = getDetailData(StringUtil.CStr(gvMaster.GetRowValues(gvMaster.FocusedRowIndex, "PROMO_NO")), int.Parse(grid.ID.Substring(8)));
        grid.DataBind();
    }

    protected void ASPxPageControl1_ActiveTabChanged(object source, DevExpress.Web.ASPxTabControl.TabControlEventArgs e)
    {
        int a = this.ASPxPageControl1.ActiveTabIndex;
        BindDetailData(grids[a], a + 1);
    }
}
