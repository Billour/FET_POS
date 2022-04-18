using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class VSS_CON14_CON14 : Advtek.Utility.BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            lblOrderNo.Text = Request.QueryString["SlipNo"];
            bindEmptyData();
           
         
        }
    }

    protected void bindEmptyData()
    {

        DataTable dtResult = new DataTable();

        gvMaster.DataSource = dtResult;
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

    private DataTable GetEmptyDataTable()
    {
        DataTable dtResult = new DataTable();
        dtResult.Columns.Add("進貨編號", typeof(string));
        dtResult.Columns.Add("商品編號", typeof(string));
        dtResult.Columns.Add("商品名稱", typeof(string));
        dtResult.Columns.Add("廠商編號", typeof(string));
        dtResult.Columns.Add("廠商名稱", typeof(string));
        dtResult.Columns.Add("到貨量", typeof(string));
        dtResult.Columns.Add("驗收量", typeof(string));
        dtResult.Columns.Add("備註", typeof(string));
        
      
        return dtResult;
    }

    private DataTable getMasterData()
    {
        DataTable dtMaster = new DataTable();
        dtMaster.Columns.Clear();
        dtMaster.Columns.Add("進貨編號", typeof(string));
        dtMaster.Columns.Add("商品編號", typeof(string));
        dtMaster.Columns.Add("商品名稱", typeof(string));
        dtMaster.Columns.Add("廠商編號", typeof(string));
        dtMaster.Columns.Add("廠商名稱", typeof(string));
        dtMaster.Columns.Add("到貨量", typeof(string));
        dtMaster.Columns.Add("驗收量", typeof(string));
        dtMaster.Columns.Add("備註", typeof(string));

        Random rnd = new Random();

        string[] rnn = { "501001", "09876543", "123456789", "09854543", "3456789", "428771", "121270", "761234", "46551", "5555123" };

        for (int i = 1; i <= 10; i++)
        {
            DataRow dtMasterRow = dtMaster.NewRow();
            dtMasterRow["進貨編號"] = rnn[i - 1]; //lblOrderNo.Text;//"CAAC120100728" + i.ToString("0#");;            
            dtMasterRow["商品編號"] = "1001002" + i.ToString("0#");
            dtMasterRow["商品名稱"] = "手機" + Convert.ToChar(64 + i).ToString();
            dtMasterRow["廠商編號"] = "AC" + i;
            dtMasterRow["廠商名稱"] = "廠商名稱" + i;
            dtMasterRow["到貨量"] = rnd.Next(6, 10);
            dtMasterRow["驗收量"] = rnd.Next(3, 6);
            dtMasterRow["備註"] = "";//"備註"+i;
            dtMaster.Rows.Add(dtMasterRow);
        }
        return dtMaster;
    }


    //private void BindData()
    //{
    //    DataTable dt = new DataTable();
    //    dt.Columns.Clear();
    //    dt.Columns.Add("出貨編號", typeof(string));
    //    dt.Columns.Add("廠商代號", typeof(string));
    //    dt.Columns.Add("廠商名稱", typeof(string));
    //    dt.Columns.Add("出貨日期", typeof(DateTime));


    //    Random rnd = new Random();

    //    for (int i = 1; i <= 5; i++)
    //    {
    //        DataRow r = dt.NewRow();
    //        r["出貨編號"] = rnd.Next(1000000, 9999999).ToString();
    //        r["廠商代號"] = "AC1";
    //        r["廠商名稱"] = "索尼";
    //        r["出貨日期"] = DateTime.Today;
    //        dt.Rows.Add(r);
    //    }
    //    GridView1.DataSource = dt;
    //    GridView1.DataBind();

    //    //GridView2.DataSource = dt;
    //    //GridView2.DataBind();



    //}


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
    protected void btnSave_Click(object sender, EventArgs e)
    {
        lblStatus.Text = "已存檔";
        
        lblModifiedDate.Text = DateTime.Now.ToString("yy/MM/dd HH:mm");
    }
    protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
    {
        bindMasterData();
        lblOrderNo.Text = "CAAC12010072800";
    }
}
