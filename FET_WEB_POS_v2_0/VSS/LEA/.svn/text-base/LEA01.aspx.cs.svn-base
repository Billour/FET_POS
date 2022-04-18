using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Advtek.Utility;
using DevExpress.Web.ASPxGridView;

public partial class VSS_LEA01_LEA01 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            bindEmptyData();
            bindMasterData();
            bindData2();
            bindData3();
        }
    }

    protected void bindEmptyData()
    {

        DataTable dtResult = new DataTable();

        gvMaster.DataSource = dtResult;
        gvMaster.DataBind();

        gvDiscountItem.DataSource = dtResult;
        gvDiscountItem.DataBind();
    }
    protected void bindMasterData()
    {
        //DataTable dtResult = new DataTable();

        //dtResult = getMasterData();
        //ViewState["gvMaster"] = dtResult;
        gvMaster.DataSource = badproducts;
        gvMaster.DataBind();
    }

    protected void bindData2()
    {
        DataTable dtResult = new DataTable();
        ViewState["gvDiscountItem"] = dtResult;
        dtResult = getData2();
        ViewState["gvDiscountItem"] = dtResult;
        gvDiscountItem.DataSource = dtResult;
        gvDiscountItem.DataBind();
    }
    protected void bindData3()
    {
        DataTable dtResult = new DataTable();
        dtResult = getData3();
        gvMobileStock.DataSource = dtResult;
        gvMobileStock.DataBind();
    }
    private DataTable getMasterData()
    {
        DataTable dtResult = new DataTable();
        dtResult.Columns.Add("賠償項目", typeof(string));
        dtResult.Columns.Add("金額", typeof(string));

        DataRow NewRow = dtResult.NewRow();
        NewRow["賠償項目"] = "電池";
        NewRow["金額"] = "1200";
        dtResult.Rows.Add(NewRow);
        return dtResult;
    }


    private DataTable getData2()
    {
        DataTable dtResult = new DataTable();
        dtResult.Columns.Add("折扣料號", typeof(string));
        dtResult.Columns.Add("折扣名稱", typeof(string));
        dtResult.Columns.Add("折扣金額", typeof(string));
        dtResult.Columns.Add("折扣比率", typeof(string));
        dtResult.Columns.Add("成本中心", typeof(string));
        dtResult.Columns.Add("會計科目", typeof(string));

        DataRow NewRow = dtResult.NewRow();
        NewRow["折扣料號"] = "折扣料號1";
        NewRow["折扣名稱"] = "折古名稱1";
        NewRow["折扣金額"] = "300";
        NewRow["折扣比率"] = "5";
        NewRow["成本中心"] = "成本中心1";
        NewRow["會計科目"] = "會計科目1";
        dtResult.Rows.Add(NewRow);
        return dtResult;
    }

    private DataTable getData3()
    {
        DataTable dtResult = new DataTable();
        dtResult.Columns.Add("門市代號", typeof(string));
        dtResult.Columns.Add("門市名稱", typeof(string));
        dtResult.Columns.Add("手機序號", typeof(string));


        DataRow NewRow = dtResult.NewRow();
        NewRow["門市代號"] = "NU001";
        NewRow["門市名稱"] = "內湖門市";
        NewRow["手機序號"] = "1234567";

        dtResult.Rows.Add(NewRow);


        return dtResult;
    }

    /// <summary>
    /// 取消新增-隱藏Footer
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnCancel1_Click(object sender, EventArgs e)
    {
    }


    protected void btnAdd2_Click(object sender, EventArgs e)
    {
        gvDiscountItem.AddNewRow();
    }

    /// <summary>
    /// 取消新增-隱藏Footer
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnCancel2_Click(object sender, EventArgs e)
    {
    }

    protected void RadioButtonList1_SelectedIndexChanged(object sender, EventArgs e)
    {
        if ("2" == this.RadioButtonList1.SelectedItem.Value.ToString())
        {
            this.TextBox5.Enabled = false;
            this.TextBox6.Enabled = false;
        }
        else
        {
            this.TextBox5.Enabled = true;
            this.TextBox6.Enabled = true;

        }
    }

    #region gvMaster 編輯/更新/取消 相關觸發事件
    protected void gvMaster_RowEditing(object sender, GridViewEditEventArgs e)
    {
        GridView gridview = sender as GridView;
        //設定編輯欄位
        gridview.EditIndex = e.NewEditIndex;
        //Bind原查詢資料
        DataTable dt = ViewState["gvMaster"] as DataTable;
        gridview.DataSource = dt;
        gridview.DataBind();
    }

    protected void gvMaster_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        GridView gridview = sender as GridView;

        //取得資料

        //更新資料庫

        //取消編輯狀態
        gridview.EditIndex = -1;

        //Bind新資料(重取資料)
        bindMasterData();
    }

    protected void gvMaster_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        GridView gridview = sender as GridView;
        gridview.EditIndex = -1;
        //Bind原查詢資料
        DataTable dt = ViewState["gvMaster"] as DataTable;
        gridview.DataSource = dt;
        gridview.DataBind();
    }
    #endregion

    #region gvMaster 編輯/更新/取消 相關觸發事件
    protected void gvDiscountItem_RowEditing(object sender, GridViewEditEventArgs e)
    {
        GridView gridview = sender as GridView;
        //設定編輯欄位
        gridview.EditIndex = e.NewEditIndex;
        //Bind原查詢資料
        DataTable dt = ViewState["gvDiscountItem"] as DataTable;
        gridview.DataSource = dt;
        gridview.DataBind();
    }

    protected void gvDiscountItem_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        GridView gridview = sender as GridView;

        //取得資料

        //更新資料庫

        //取消編輯狀態
        gridview.EditIndex = -1;

        //Bind新資料(重取資料)
        bindData2();
    }

    protected void gvDiscountItem_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        GridView gridview = sender as GridView;
        gridview.EditIndex = -1;
        //Bind原查詢資料
        DataTable dt = ViewState["gvDiscountItem"] as DataTable;
        gridview.DataSource = dt;
        gridview.DataBind();
    }
    #endregion

    private DataTable GetDetailedData()
    {
        DataTable dt = new DataTable();
        dt.Columns.Clear();
        dt.Columns.Add("廠商代號", typeof(string));
        dt.Columns.Add("結算金額", typeof(int));
        dt.Columns.Add("銷貨總額", typeof(int));
        dt.Columns.Add("銷貨金額", typeof(int));


        DataRow dr = dt.NewRow();
        dr["廠商代號"] = "AC001";
        dr["結算金額"] = 205000;
        dr["銷貨總額"] = 10000;
        dr["銷貨金額"] = 0;

        dt.Rows.Add(dr);

        dr = dt.NewRow();
        dr["廠商代號"] = "AC002";
        dr["結算金額"] = 89000;
        dr["銷貨總額"] = 10000;
        dr["銷貨金額"] = 0;

        dt.Rows.Add(dr);

        return dt;
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        DataTable dt = GetDetailedData();

        DataView dv = new DataView(dt);

        FormView1.DataSource = dv;
        FormView1.DataBind();
        Panel2.Visible = true;
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        Response.Redirect("LEA07.aspx");
    }

    protected void gvDiscountItem_CustomCallback(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewCustomCallbackEventArgs e)
    {

    }

    protected void gvDiscountItem_RowUpdated(object sender, DevExpress.Web.Data.ASPxDataUpdatedEventArgs e)
    {

    }

    protected void gvDiscountItem_RowUpdating1(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
    {
        ASPxGridView grid = (ASPxGridView)sender;
        grid.CancelEdit();
        e.Cancel = true;
        bindData2();

    }
    protected void gvDiscountItem_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
    {
        ASPxGridView grid = (ASPxGridView)sender;
        grid.CancelEdit();
        e.Cancel = true;
        bindData2();
    }

    protected void gvMaster_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
    {
        ASPxGridView grid = (ASPxGridView)sender;
        grid.CancelEdit();
        e.Cancel = true;
        bindMasterData();
    }

    protected void gvMaster_RowUpdating1(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
    {
        ASPxGridView grid = (ASPxGridView)sender;
        grid.CancelEdit();
        e.Cancel = true;
        bindMasterData();
    }

    /// <summary>
    /// 賠償資料
    /// </summary>
    public class BadProduct
    {
        //賠償項目              
        public string Name { get; set; }

        //金額
        public int Amount { get; set; }

        public int ID { get; set; }
    }

    // Create a data source by using a collection initializer.
    static List<BadProduct> badproducts = new List<BadProduct>
    {
       new BadProduct {Name="電池", Amount=1000,    ID=1},
       new BadProduct {Name="鍵盤", Amount=480,     ID=2}
    };
}
