using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Advtek.Utility;
using System.Collections;

public partial class VSS_INV_INV27 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

        //if (!IsPostBack)
        //{
        //    bindMasterData(0);

        //}
    }
    protected void bindMasterData(int TempCount)
    {
        DataTable dtResult = new DataTable();
        dtResult = getMasterData(TempCount);
        ViewState["gvMaster"] = dtResult;
        gvMaster.DataSource = dtResult;
        gvMaster.DataBind();

        ViewState["gvDetail"] = getGridViewData();
        gvDetail.DataSource = (DataTable)ViewState["gvDetail"];
        gvDetail.DataBind();

       
    }
    private DataTable getMasterData(int TempCount)
    {
        DataTable dtResult = new DataTable();
        dtResult.Columns.Add("拆封日期", typeof(string));
        dtResult.Columns.Add("展示起日", typeof(string));
        dtResult.Columns.Add("展示訖日", typeof(string));
        dtResult.Columns.Add("商品料號", typeof(string));
        dtResult.Columns.Add("商品名稱", typeof(string));
        dtResult.Columns.Add("拆封數量", typeof(string));
        dtResult.Columns.Add("折扣方式", typeof(string));
        dtResult.Columns.Add("金額/占比", typeof(string));
        dtResult.Columns.Add("更新日期", typeof(string));
        dtResult.Columns.Add("更新人員", typeof(string));


       
        for (int i = 1; i < 4; i++)
        {
            DataRow NewRow = dtResult.NewRow();
            NewRow["拆封日期"] = "2010/09/0" + i;
            NewRow["展示起日"] = "2010/09/1" + i;
            NewRow["展示訖日"] = "2010/09/30";
            NewRow["商品料號"] = "15720000"+i;
            NewRow["商品名稱"] = "商品名稱"+i;
            NewRow["拆封數量"] = i - 1;
            NewRow["折扣方式"] = "2%";
            NewRow["金額/占比"] = "100";
            NewRow["更新日期"] = "2010/08/31";
            NewRow["更新人員"] = "王大同";
            dtResult.Rows.Add(NewRow);
        }

        return dtResult;
    }

    private DataTable getGridViewData()
    {
        DataTable dtResult = new DataTable();
        dtResult.Columns.Add("門市編號", typeof(int));
        dtResult.Columns.Add("門市名稱", typeof(string));
        dtResult.Columns.Add("區域別", typeof(string));

        DataRow NewRow = dtResult.NewRow();
        NewRow["門市編號"] = "2103";
        NewRow["門市名稱"] = "永和門市";
        NewRow["區域別"] = "北一區";

        dtResult.Rows.Add(NewRow);
        return dtResult;
    }



    protected void btnSearch_Click(object sender, EventArgs e)
    {
        bindMasterData(1);
        Button1.Visible = true;
        Button2.Visible = true;
        TabContainer1.Visible = false;
        btnSave.Visible = false;
        btnDiscard.Visible = false;
        Button8.Visible = false;
    }
    protected void Button1_Click(object sender, EventArgs e)
    {

    }
    protected void Button4_Click(object sender, EventArgs e)
    {

    }
    protected void btnSave_Click(object sender, EventArgs e)
    {

    }
    protected void gvMaster_RowCommand(object sender, GridViewCommandEventArgs e)
    {

        if (e.CommandName == "select")
        {

            TabContainer1.Visible = true;
            btnSave.Visible = true;
            btnDiscard.Visible = true;
            Button8.Visible = true;

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
        bindMasterData(1);
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

    #region gvDetail1 編輯/更新/取消 相關觸發事件
    protected void gvDetail_RowEditing(object sender, GridViewEditEventArgs e)
    {
        GridView gridview = sender as GridView;
        //設定編輯欄位
        gridview.EditIndex = e.NewEditIndex;
        //Bind原查詢資料
        DataTable dt = ViewState["gvDetail"] as DataTable;
        gridview.DataSource = dt;
        gridview.DataBind();
    }

    protected void gvDetail_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        GridView gridview = sender as GridView;

        //取得資料

        //更新資料庫

        //取消編輯狀態
        gridview.EditIndex = -1;

        //Bind新資料(重取資料)
        bindMasterData(1);
    }

    protected void gvDetail_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        GridView gridview = sender as GridView;
        gridview.EditIndex = -1;
        //Bind原查詢資料
        DataTable dt = ViewState["gvDetail"] as DataTable;
        gridview.DataSource = dt;
        gridview.DataBind();
    }
    #endregion

 

}
