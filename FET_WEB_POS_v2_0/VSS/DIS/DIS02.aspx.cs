using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Advtek.Utility;
using System.Collections;
public partial class VSS_DIS02_DIS02 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            bindMasterData();

        }
    }
    protected void bindMasterData()
    {
        DataTable dtResult = new DataTable();
        dtResult = getMasterData();
        gvMaster.DataSource = dtResult;
        gvMaster.DataBind();
        ViewState["gvMaster"] = dtResult;
    }
    private DataTable getMasterData()
    {
        DataTable dtResult = new DataTable();
        dtResult.Columns.Add("狀態", typeof(string));
        dtResult.Columns.Add("折扣料號", typeof(string));
        dtResult.Columns.Add("折扣名稱", typeof(string));
        dtResult.Columns.Add("有效日期(起)", typeof(string));
        dtResult.Columns.Add("有效日期(迄)", typeof(string));
        dtResult.Columns.Add("折扣金額", typeof(string));
        dtResult.Columns.Add("人員", typeof(string));
        dtResult.Columns.Add("日期", typeof(string));



        DataRow NewRow = dtResult.NewRow();
        NewRow["狀態"] = "狀態1";
        NewRow["折扣料號"] = "折扣料號1";
        NewRow["折扣名稱"] = "折扣名稱1";
        NewRow["有效日期(起)"] = "2010/05/01";
        NewRow["有效日期(迄)"] = "2010/05/11";
        NewRow["折扣金額"] = "111";
        NewRow["人員"] = "人員1";
        NewRow["日期"] = DateTime.Now.ToShortDateString();
        dtResult.Rows.Add(NewRow);

        NewRow = dtResult.NewRow();
        NewRow["狀態"] = "狀態2";
        NewRow["折扣料號"] = "折扣料號2";
        NewRow["折扣名稱"] = "折扣名稱2";
        NewRow["有效日期(起)"] = "2010/06/22";
        NewRow["有效日期(迄)"] = "2010/07/02";
        NewRow["折扣金額"] = "222";
        NewRow["人員"] = "人員2";
        NewRow["日期"] = DateTime.Now.ToShortDateString();
        dtResult.Rows.Add(NewRow);




        return dtResult;
    }






    protected void btnQuery_Click(object sender, EventArgs e)
    {
        this.gvMaster.Visible = true;
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
