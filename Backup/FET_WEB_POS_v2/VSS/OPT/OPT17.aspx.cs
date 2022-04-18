using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using DevExpress.Web.ASPxGridView;

public partial class VSS_OPT_OPT17 : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            // 繫結空的資料表，以顯示表頭欄位
            gvMaster.DataSource = GetEmptyDataTable();
            gvMaster.DataBind();
        }
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        bindMasterData();
    }

    protected void bindMasterData()
    {
        DataTable dtResult = new DataTable();
        dtResult = getMasterData();
        ViewState["gvMaster"] = dtResult;
        gvMaster.DataSource = dtResult;
        gvMaster.DataBind();
    }

    private DataTable GetEmptyDataTable()
    {
        DataTable dtResult = new DataTable();
        dtResult.Columns.Add("項次", typeof(string));
        dtResult.Columns.Add("門市編號", typeof(string));
        dtResult.Columns.Add("門市名稱", typeof(string));
        dtResult.Columns.Add("發票格式", typeof(string));
        dtResult.Columns.Add("所屬年月起", typeof(string));
        dtResult.Columns.Add("所屬年月訖", typeof(string));
        dtResult.Columns.Add("字軌", typeof(string));
        dtResult.Columns.Add("起始編號", typeof(string));
        dtResult.Columns.Add("終止編號", typeof(string));
        dtResult.Columns.Add("發票張數", typeof(string));
        dtResult.Columns.Add("更新人員", typeof(string));
        dtResult.Columns.Add("更新日期", typeof(string));
        return dtResult;
    }

    private DataTable getMasterData()
    {
        DataTable dtResult = GetEmptyDataTable();

        string[] ary = { "手開二聯式", "手開三聯式" };
        Random rnd = new Random();

        for (int i = 1; i < 11; i++)
        {
            DataRow NewRow = dtResult.NewRow();
            NewRow["項次"] = i;
            NewRow["門市編號"] = "R2101";
            NewRow["門市名稱"] = "遠企";
            NewRow["發票格式"] =  ary[rnd.Next(1, 10) % 2];
            NewRow["所屬年月起"] = DateTime.Now.ToString("yyyy/MM/dd");
            NewRow["所屬年月訖"] = DateTime.Now.ToString("yyyy/MM/dd");
            NewRow["字軌"] = "AA";
            NewRow["起始編號"] = i.ToString("00");
            NewRow["終止編號"] = (i + 10).ToString("00");
            NewRow["發票張數"] = i;
            NewRow["更新人員"] = "Jackey";
            NewRow["更新日期"] = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");
            dtResult.Rows.Add(NewRow);
        }
        return dtResult;
    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
       gvMaster.AddNewRow();
    }


    /// <summary>
    /// 主GridView的分頁變更事件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gvMaster_PageIndexChanged(object sender, EventArgs e)
    {
       ASPxGridView grid = sender as ASPxGridView;
       gvMaster.DataSource = getMasterData();
       gvMaster.DataBind();
    }
    protected void gvMaster_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
    {
       gvMaster.CancelEdit();
       e.Cancel = true;
       bindMasterData();
    }
    protected void gvMaster_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
    {
       gvMaster.CancelEdit();
       e.Cancel = true;
       bindMasterData();

    }
}
