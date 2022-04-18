using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Advtek.Utility;
using System.Data;


public partial class VSS_INV02_INV02 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
       if(!Page.IsPostBack) bindMasterEmptyData();
    }
    protected void bindMasterEmptyData()
    {
        DataTable dtResult = new DataTable();
        ViewState["gvMaster"] = dtResult;
        gvMaster.DataSource = dtResult;
        gvMaster.DataBind();
    }
    protected void bindMasterData()
    {
        DataTable dtResult = new DataTable();
        dtResult = getMasterData();
        ViewState["gvMaster"] = dtResult;
        gvMaster.DataSource = dtResult;
        gvMaster.DataBind();
    }
    private DataTable getMasterData()
    {
        DataTable dtResult = new DataTable();
       
        dtResult.Columns.Add("商品編號", typeof(string));
        dtResult.Columns.Add("商品名稱", typeof(string));
        dtResult.Columns.Add("移出數量", typeof(string));
        dtResult.Columns.Add("撥入數量", typeof(string));

        DataRow NewRow = dtResult.NewRow();
        
        NewRow["商品編號"] = "157200001";
        NewRow["商品名稱"] = "Iphone 8g";
        NewRow["移出數量"] = "5";
        NewRow["撥入數量"] = "";
        
        dtResult.Rows.Add(NewRow);

        return dtResult;
    }


    protected void btnNew_Click(object sender, EventArgs e)
    {
        bindMasterData();
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        lbOrderNo.Text = "ST2103-1007001";
       
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
}
