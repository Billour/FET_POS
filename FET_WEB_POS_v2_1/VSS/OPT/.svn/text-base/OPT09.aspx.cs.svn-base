using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class VSS_OPT_OPT09 : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {

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
        DataTable dtMaster = new DataTable();
        dtMaster.Columns.Clear(); 
        dtMaster.Columns.Add("狀態", typeof(string));
        dtMaster.Columns.Add("門市編號", typeof(string));
        dtMaster.Columns.Add("門市名稱", typeof(string));
        dtMaster.Columns.Add("員工編號", typeof(string));
        dtMaster.Columns.Add("員工姓名", typeof(string)); 
        dtMaster.Columns.Add("更新日期", typeof(string));
        dtMaster.Columns.Add("更新人員", typeof(string));

        string[] array1 = { "有效", "過期", "已停止" };
        string[] array2 = { "王小明", "李大同", "何小美", "林阿好", "陳東東" };
        for (int i = 0; i < 8; i++)
        {
            DataRow dtMasterRow = dtMaster.NewRow(); 
            dtMasterRow["狀態"] = array1[i % 3];
            dtMasterRow["門市編號"] ="QA000"+i;
            dtMasterRow["門市名稱"] = "門市名稱" + i;
            dtMasterRow["員工編號"] = "WQ00210" + i;
            dtMasterRow["員工姓名"] =  array2[i % 5]; 
            dtMasterRow["更新日期"] = "2010/07/20";
            dtMasterRow["更新人員"] = "王小明";
            dtMaster.Rows.Add(dtMasterRow);
        }
        return dtMaster;
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        bindMasterData();
        btnNew.Visible = true;
        btnDelete.Visible = true;
        Div1.Visible = true;
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
     protected void GridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        //此函式可共用
        GridView gridview = sender as GridView;
        gridview.PageIndex = e.NewPageIndex;

        DataTable dt = ViewState[gridview.ID] as DataTable ?? null;//GridView的資料要存於ViewState以方便共用此Function
        gridview.DataSource = dt;
        gridview.DataBind();
    }
  
    protected void btnGoToIndex_Click(object sender, EventArgs e)
    {
        //此函式可共用
        GridView gridview = (sender as Button).Parent.Parent.Parent.Parent as GridView;
        TextBox textbox = (sender as Button).Parent.FindControl("tbGoToIndex") as TextBox;
        string strIndex = textbox.Text.Trim();
        int index = 0;
        if (int.TryParse(strIndex, out index))
        {
            index = index - 1;
            if (index >= 0 && index <= gridview.PageCount - 1)
            {
                gridview.PageIndex = index;
                DataTable dt = ViewState[gridview.ID] as DataTable ?? null;//GridView的資料要存於ViewState以方便共用此Function
                gridview.DataSource = dt;
                gridview.DataBind();
            }
            else
            {
                textbox.Text = string.Empty;
            }
        }
        else
        {
            textbox.Text = string.Empty;
        }
    }
}

