using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class VSS_LOG_LOG02 : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
           
             
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
        dtResult.Columns.Add("狀態", typeof(string));
        dtResult.Columns.Add("角色代碼", typeof(string));
        dtResult.Columns.Add("角色名稱", typeof(string));
        dtResult.Columns.Add("備註說明", typeof(string));
        dtResult.Columns.Add("更新日期", typeof(string));
        dtResult.Columns.Add("更新人員", typeof(string));

        for (int i = 0; i <= 5; i++)
        {
            DataRow NewRow = dtResult.NewRow();
            NewRow["狀態"] = i;
            NewRow["角色代碼"] = "角色代碼" + i;
            NewRow["角色名稱"] = "角色名稱" + i;
            NewRow["備註說明"] = "";
            NewRow["更新日期"] = Convert.ToDateTime(DateTime.Now).ToString("yyyy/MM/dd HH:mm:ss");
            NewRow["更新人員"] = "更新人員" +i;
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
         
        this.gvMaster.Visible = true;
        this.div1.Visible = true;
    }
}
