using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class VSS_LOG_LOG03 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            bindEmptyData();           
        }
    }
    protected void bindEmptyData()
    {

        DataTable dtResult = new DataTable();

        gvMaster.DataSource = dtResult;
        gvMaster.DataBind();
    }
    protected void bindData()
    {

        gvMaster.DataSource = getMasterData();
        gvMaster.DataBind();
    }
    private DataTable getData3()
    {
        DataTable dtResult = new DataTable();
        dtResult.Columns.Add("門市編號", typeof(string));
        dtResult.Columns.Add("門市名稱", typeof(string));
        dtResult.Columns.Add("員工編號", typeof(string));
        dtResult.Columns.Add("員工姓名", typeof(string));



        DataRow NewRow = dtResult.NewRow();
        NewRow["門市編號"] = "NU001";
        NewRow["門市名稱"] = "內湖門市";
        NewRow["員工編號"] = "1234567";
        NewRow["員工姓名"] = "王小寶";
        dtResult.Rows.Add(NewRow);


        return dtResult;
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
        dtResult.Columns.Add("順序", typeof(string));
        dtResult.Columns.Add("角色代碼", typeof(string));
        dtResult.Columns.Add("角色名稱", typeof(string));
        dtResult.Columns.Add("系統別", typeof(string));
        dtResult.Columns.Add("模組名稱", typeof(string));
        dtResult.Columns.Add("功能代碼", typeof(string));
        dtResult.Columns.Add("功能名稱", typeof(string));
        dtResult.Columns.Add("url", typeof(string));
        

        string[] array1 = { "區經理", "店長", "店員"};
        string[] array2 = { "銷售交易查詢", "系統參數設定", "HG點數兌換-來店禮" };

        for (int i = 0; i <= 10; i++)
        {
            DataRow NewRow = dtResult.NewRow();
            NewRow["順序"] = i;
            NewRow["角色代碼"] = "角色代碼" + i;
            NewRow["角色名稱"]= array1 [i % 3];
            NewRow["系統別"] = "系統別" + i;
            NewRow["模組名稱"] = "模組名稱" + i;
            NewRow["功能代碼"] = "功能代碼" + i;
            NewRow["功能名稱"] = array2[i % 3];
            NewRow["url"] = "url" + i;
         
            dtResult.Rows.Add(NewRow);
        }
        return dtResult;
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

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        bindMasterData();
    }
    protected void Button11_Click(object sender, EventArgs e)
    {
        bindData();
    }

    protected void PermissionsHiddenField_ValueChanged(Object sender, EventArgs e)
    {
        bindData();
    }

    #region 分頁相關 (共用)
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
    #endregion
}
