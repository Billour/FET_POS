using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class VSS_OPT_OPT07 : BasePage
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
        dtMaster.Columns.Add("兌點代號", typeof(string));
        dtMaster.Columns.Add("兌點名稱", typeof(string));
        dtMaster.Columns.Add("有效期間(起)", typeof(string));
        dtMaster.Columns.Add("有效期間(迄)", typeof(string));
        dtMaster.Columns.Add("點數", typeof(string));
        dtMaster.Columns.Add("兌換金額", typeof(string));
        dtMaster.Columns.Add("更新日期", typeof(string));
        dtMaster.Columns.Add("更新人員", typeof(string));

        string[] array1 = { "有效", "過期", "已停止" };
        for (int i = 0; i < 5; i++)
        {
            DataRow dtMasterRow = dtMaster.NewRow();
            dtMasterRow["狀態"] = array1[i % 3];
            dtMasterRow["兌點代號"] = "A0002" + i;
            dtMasterRow["兌點名稱"] = "兌點名稱" + i;
            dtMasterRow["有效期間(起)"] = "2010/07/01";
            dtMasterRow["有效期間(迄)"] = "2010/07/30";
            dtMasterRow["點數"] = 100 * (i + 1);
            dtMasterRow["兌換金額"] = 200 * (i + 1);
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
}
