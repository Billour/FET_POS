using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class VSS_OPT_OPT06 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

        if (Page.IsPostBack)
        {
            Label1.Visible = true;
            //Label2.Visible = true;
            Label3.Visible = true;
            //Label4.Visible = true;
            //Label5.Visible = true;
            Label6.Visible = true;
            Label7.Visible = true;
            Label8.Visible = true;
            Label9.Visible = true;
            //tbname.Text = "遠企直營店";
           // bindMasterData();
            Div1.Visible = editMode.Visible = true;
            //btnSave.Text = "儲存";
            btnSave.Visible = true;
        }
        else
        {
            //btnSave.Text = "觸發";
            btnSave.Visible = false;
            Div1.Visible = editMode .Visible= false;
        }
    }
    protected void bindMasterEmptyData()
    {
        DataTable dtGvMaster = new DataTable();
         
        gvMaster.DataSource = dtGvMaster;
        gvMaster.DataBind();
    }
    protected void bindMasterData()
    {
        DataTable dtGvMaster = new DataTable();
        dtGvMaster = getMasterData();
        ViewState["gvMaster"] = dtGvMaster;
        gvMaster.DataSource = dtGvMaster;
        gvMaster.DataBind();
    }

    private DataTable getMasterData()
    {
        //機台號碼	機台IP	機台狀態
        DataTable dtMaster = new DataTable();
        dtMaster.Columns.Clear();
        dtMaster.Columns.Add("機台號碼", typeof(string));
        dtMaster.Columns.Add("機台IP", typeof(string));

        DataRow dtMasterRow = dtMaster.NewRow();
        dtMasterRow["機台號碼"] = "001";
        dtMasterRow["機台IP"] = "192.174.1.2";
        dtMaster.Rows.Add(dtMasterRow);

        dtMasterRow = dtMaster.NewRow();
        dtMasterRow["機台號碼"] = "002";
        dtMasterRow["機台IP"] = "192.174.1.4";
        dtMaster.Rows.Add(dtMasterRow);

        //dtMasterRow = dtMaster.NewRow();
        //dtMasterRow["機台號碼"] = "003";
        //dtMasterRow["機台IP"] = "192.174.1.12";
        //dtMasterRow["機台狀態"] = "維修中";
        //dtMaster.Rows.Add(dtMasterRow);

        return dtMaster;
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
   
    protected void tbStoreNo_TextChanged(object sender, EventArgs e)
    {
       
    }
    protected void btnNew_Click(object sender, EventArgs e)
    {
        bindMasterData();
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        bindMasterEmptyData();
    }
}
