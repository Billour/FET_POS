using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using AdvTek.CustomControls;

public partial class VSS_CONS_CON21 : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
       if (!Page.IsPostBack)
       {
          bindMasterData();
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
        dtResult.Columns.Add("移撥單號", typeof(string));
        dtResult.Columns.Add("移出門市", typeof(string));
        dtResult.Columns.Add("移出時間", typeof(string));
        dtResult.Columns.Add("撥入門市", typeof(string));
        dtResult.Columns.Add("撥入時間", typeof(string));
        dtResult.Columns.Add("移撥狀態", typeof(string));
        dtResult.Columns.Add("更新人員", typeof(string));
        dtResult.Columns.Add("更新日期", typeof(string));

        string[] array1 = {  "在途中", "已撥入"};
        string[] array2 = { "2103", "2104", "2108", "2113" };


        for (int i = 0; i < 17; i++)
        {
            DataRow NewRow = dtResult.NewRow();

            NewRow["移撥單號"] = "CAAC1201007280" + i;
            NewRow["移出門市"] = array2[i % 4];
            NewRow["移出時間"] = Convert.ToDateTime("2010/09/01" + DateTime.Now.ToLongTimeString()).ToString("yyyy/MM/dd HH:mm");
            NewRow["撥入門市"] = "2101";
            NewRow["撥入時間"] = "";
            NewRow["移撥狀態"] = "在途中";
            NewRow["更新人員"] = "王小明";
            NewRow["更新日期"] = Convert.ToDateTime("2010/09/01" + DateTime.Now.ToLongTimeString()).ToString("yyyy/MM/dd");

            dtResult.Rows.Add(NewRow);
        }
        return dtResult;
    }

    private DataTable getDetailData()
    {
        DataTable gvDetail = new DataTable();
        gvDetail.Columns.Clear();
        gvDetail.Columns.Add("項次", typeof(string));
        gvDetail.Columns.Add("商品類別", typeof(string));
        gvDetail.Columns.Add("商品料號", typeof(string));
        gvDetail.Columns.Add("商品名稱", typeof(string));
        gvDetail.Columns.Add("撥出數量", typeof(string));
        gvDetail.Columns.Add("撥入數量", typeof(string));        


        for (int i = 1; i < 5; i++)
        {
            DataRow gvDetailRow = gvDetail.NewRow();
            gvDetailRow["項次"] = i;
            gvDetailRow["商品類別"] = "商品類別" + i;
            gvDetailRow["商品料號"] = "商品料號" + i;
            gvDetailRow["商品名稱"] = "商品名稱" + i;
            gvDetailRow["撥出數量"] = 1;
            gvDetailRow["撥入數量"] = 1;
            
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
