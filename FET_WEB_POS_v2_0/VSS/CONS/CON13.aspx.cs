using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using DevExpress.Web.ASPxGridView;

public partial class VSS_CON13_CON13 : Advtek.Utility.BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            // 繫結空的資料表，以顯示表頭欄位
            gvMaster.DataSource = GetEmptyDataTable();
            gvMaster.DataBind();
        }
        else
        {
            DataTable dtGvMaster = ViewState["gvMaster"] as DataTable;
            if (dtGvMaster != null)
            {
                gvMaster.DataSource = dtGvMaster;
                gvMaster.DataBind();
            }
        }
    }

    private DataTable GetEmptyDataTable()
    {
        DataTable dtResult = new DataTable();
        dtResult.Columns.Add("OE_NO", typeof(string));
        dtResult.Columns.Add("訂單編號", typeof(string));
        dtResult.Columns.Add("出貨編號", typeof(string));
        dtResult.Columns.Add("門市編號", typeof(string));
        dtResult.Columns.Add("門市名稱", typeof(string));
        dtResult.Columns.Add("廠商編號", typeof(string));
        dtResult.Columns.Add("廠商名稱", typeof(string));
        dtResult.Columns.Add("驗收狀態", typeof(string));
        dtResult.Columns.Add("進貨日期", typeof(string));
        dtResult.Columns.Add("人員", typeof(string));
        dtResult.Columns.Add("日期", typeof(string));
        dtResult.PrimaryKey = new DataColumn[] { dtResult.Columns["訂單編號"] };
        return dtResult;
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
    private DataTable getMasterData()
    {
        DataTable dtResult = new DataTable();
        dtResult.Columns.Add("OE_NO", typeof(string));
        dtResult.Columns.Add("訂單編號", typeof(string));
        dtResult.Columns.Add("出貨編號", typeof(string));
        dtResult.Columns.Add("門市編號", typeof(string));
        dtResult.Columns.Add("門市名稱", typeof(string));
        dtResult.Columns.Add("廠商編號", typeof(string));
        dtResult.Columns.Add("廠商名稱", typeof(string));
        dtResult.Columns.Add("驗收狀態", typeof(string));
        dtResult.Columns.Add("進貨日期", typeof(string));
        dtResult.Columns.Add("人員", typeof(string));
        dtResult.Columns.Add("日期", typeof(string));
        dtResult.PrimaryKey = new DataColumn[] { dtResult.Columns["訂單編號"] };
        string[] array1 = { "已存檔","轉單中", "已成單", "待進貨", "已驗收" };
        Random rnd = new Random();
        for (int i = 0; i < 10; i++)
        {            
            DataRow NewRow = dtResult.NewRow();
            NewRow["OE_NO"] = "D0001000" + i;
            // 訂單編號規則：CS+廠商編號+西元年月日+2碼流水號；
            NewRow["訂單編號"] = "CAAC120100728" + i.ToString("0#");
            NewRow["出貨編號"] = "501" + i.ToString("00#");//"W000" + i;
            NewRow["門市編號"] = "AC0000" + i;
            NewRow["門市名稱"] = "門市名稱" + i;
            NewRow["廠商編號"] = "AC0000" + i;
            NewRow["廠商名稱"] = "廠商名稱" + i;
            NewRow["驗收狀態"] = array1[rnd.Next(0, array1.Length - 1)];//array1[i % 3];
            NewRow["進貨日期"] = "2010/07/14";
            NewRow["人員"] = "王小明";
            NewRow["日期"] = "2010/07/13";
            dtResult.Rows.Add(NewRow);
        }
        return dtResult;
    }

    private DataTable getDetailData()
    {      
        DataTable gvDetail = new DataTable();
        gvDetail.Columns.Clear();
        gvDetail.Columns.Add("出貨編號", typeof(string));
        gvDetail.Columns.Add("商品編號", typeof(string));
        gvDetail.Columns.Add("商品名稱", typeof(string));
        gvDetail.Columns.Add("廠商編號", typeof(string));
        gvDetail.Columns.Add("廠商名稱", typeof(string));
        gvDetail.Columns.Add("驗收量", typeof(int));

        Random rnd = new Random();
        for (int i = 1; i < 5; i++)
        {
            DataRow gvDetailRow = gvDetail.NewRow();
            gvDetailRow["出貨編號"] = "501001";
            gvDetailRow["商品編號"] = "AC11107" + i.ToString("00#");
            gvDetailRow["商品名稱"] = "手機" + Convert.ToChar(64+i).ToString();
            gvDetailRow["廠商編號"] = "AC" + i.ToString();
            gvDetailRow["廠商名稱"] = "商品名稱" + i;
            gvDetailRow["驗收量"] = rnd.Next(1, 10);
            
            gvDetail.Rows.Add(gvDetailRow);
        }
        return gvDetail;
    }

    protected void gvMaster_DetailRowExpandedChanged(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewDetailRowEventArgs e)
    {
        ASPxGridView grid = (ASPxGridView)gvMaster.FindDetailRowTemplateControl(e.VisibleIndex, "gvDetail");
        if (grid != null)
        {
            DataTable dtgvDetail = new DataTable();
            dtgvDetail = getDetailData();
            ViewState["gvDetail"] = dtgvDetail;

            grid.DataSource = dtgvDetail;
            grid.DataBind();
        }
    }
}
