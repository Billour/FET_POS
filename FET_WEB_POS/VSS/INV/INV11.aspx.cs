using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class VSS_INV11_INV11 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (!string.IsNullOrEmpty(Request.QueryString["InventoryNo"]))
            {
                bindMasterData();
                RadioButtonList1.Enabled = false;
            }
            else
            {
                gridBtn.Style["display"] = "none";
            }
        }
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
        dtResult.Columns.Add("項次", typeof(string));
        dtResult.Columns.Add("倉別", typeof(string));
        dtResult.Columns.Add("商品編號", typeof(string));
        dtResult.Columns.Add("商品名稱", typeof(string));
        dtResult.Columns.Add("單位", typeof(string));
        dtResult.Columns.Add("帳上庫存", typeof(string));
        dtResult.Columns.Add("門市盤點量", typeof(string));
        dtResult.Columns.Add("盤差量", typeof(string));

        Random rnd = new Random();


        for (int i = 0; i < 3; i++)
        {
            DataRow NewRow = dtResult.NewRow();
            NewRow["項次"] =   i+1;
            NewRow["倉別"] = "銷售倉";
            NewRow["商品編號"] = "00000" + i;
            NewRow["商品名稱"] = "商品名稱" + i;
            NewRow["單位"] = "SET" ;

            int inv = 7 * (i + 1) + i;

            NewRow["帳上庫存"] = inv;
            NewRow["門市盤點量"] = rnd.Next(inv - 3, inv);
            NewRow["盤差量"] = Convert.ToInt32(NewRow["帳上庫存"]) - Convert.ToInt32(NewRow["門市盤點量"]);
            dtResult.Rows.Add(NewRow);
        }
        return dtResult;
    }


    protected void btnSave_Click(object sender, EventArgs e)
    {

    }

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
    protected void Button5_Click(object sender, EventArgs e)
    {
        bindMasterData();
        gridBtn.Style["display"] = "";
    }
}
