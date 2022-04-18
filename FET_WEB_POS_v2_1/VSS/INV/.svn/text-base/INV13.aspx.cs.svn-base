using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using DevExpress.Web.ASPxGridView;
using DevExpress.Web.ASPxEditors;

public partial class VSS_INV_INV13 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            // 繫結空的資料表，以顯示表頭欄位
            ////gvMaster.DataSource = GetEmptyDataTable();
            ////gvMaster.DataBind();
            bindMasterData();

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
        dtResult.Columns.Add("進貨單號", typeof(string));
        dtResult.Columns.Add("進貨日期", typeof(string));
        dtResult.Columns.Add("備註", typeof(string));

        dtResult.Columns.Add("更新人員", typeof(string));
        dtResult.Columns.Add("更新日期", typeof(string));
        return dtResult;

    }

    private DataTable getMasterData()
    {

        DataTable dtResult = GetEmptyDataTable();

        for (int i = 1; i < 12; i++)
        {

            DataRow NewRow = dtResult.NewRow();
            NewRow["項次"] = i;
            NewRow["進貨單號"] = "NPO" + DateTime.Today.ToString("yyyyMMdd") + "00" + i;
            NewRow["進貨日期"] = DateTime.Now.ToString("yyyy/MM/dd");
            NewRow["備註"] = "";
            NewRow["更新人員"] = "王小明";
            NewRow["更新日期"] = "2010/10/28/ 15:18:36";
            //DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");

            dtResult.Rows.Add(NewRow);
        }
        return dtResult;

    }
    protected void bindDetailData()
    {
        DataTable dtgvDetail = new DataTable();
        dtgvDetail = getDetailData();
        ViewState["gvDetail"] = dtgvDetail;

    }
    private DataTable getDetailData()
    {
        DataTable gvDetail = new DataTable();
        gvDetail.Columns.Clear();
        gvDetail.Columns.Add("項次", typeof(string));
        gvDetail.Columns.Add("商品料號", typeof(string));
        gvDetail.Columns.Add("商品名稱", typeof(string));
        gvDetail.Columns.Add("單位", typeof(string));
        gvDetail.Columns.Add("數量", typeof(string));
        gvDetail.Columns.Add("總金額", typeof(string));


        DataRow gvDetailRow = gvDetail.NewRow();
        gvDetailRow["項次"] = "1";
        gvDetailRow["商品料號"] = "100100101";
        gvDetailRow["商品名稱"] = "Nokia鑰匙圈";
        gvDetailRow["單位"] = "個";
        gvDetailRow["數量"] = "1";
        gvDetailRow["總金額"] = "500";

        gvDetail.Rows.Add(gvDetailRow);

        gvDetailRow = gvDetail.NewRow();
        gvDetailRow["項次"] = "2";
        gvDetailRow["商品料號"] = "100100102";
        gvDetailRow["商品名稱"] = "Nokia N87";
        gvDetailRow["單位"] = "個";
        gvDetailRow["數量"] = "2";
        gvDetailRow["總金額"] = "7000";
        gvDetail.Rows.Add(gvDetailRow);

        gvDetailRow = gvDetail.NewRow();
        gvDetailRow["項次"] = "3";
        gvDetailRow["商品料號"] = "100100103";
        gvDetailRow["商品名稱"] = "iPhone4";
        gvDetailRow["單位"] = "個";
        gvDetailRow["數量"] = "1";
        gvDetailRow["總金額"] = "20000";
        gvDetail.Rows.Add(gvDetailRow);


        gvDetailRow = gvDetail.NewRow();
        gvDetailRow["項次"] = "4";
        
        gvDetailRow["商品料號"] = "100100104";
        gvDetailRow["商品名稱"] = "HTC";
        gvDetailRow["單位"] = "個";
        gvDetailRow["數量"] = "5";
        gvDetailRow["總金額"] = "21000";
        gvDetail.Rows.Add(gvDetailRow);
    
        gvDetailRow = gvDetail.NewRow();
        gvDetailRow["項次"] = "5";
        gvDetailRow["商品料號"] = "100100105";
        gvDetailRow["商品名稱"] = "LG";
        gvDetailRow["單位"] = "個";
        gvDetailRow["數量"] = "3";
        gvDetailRow["總金額"] = "4500";
        gvDetail.Rows.Add(gvDetailRow);


        gvDetailRow = gvDetail.NewRow();
        gvDetailRow["項次"] = "6";
        gvDetailRow["商品料號"] = "100100106";
        gvDetailRow["商品名稱"] = "商品名稱6";
        gvDetailRow["單位"] = "個";
        gvDetailRow["數量"] = "3";
        gvDetailRow["總金額"] = "4500";
        gvDetail.Rows.Add(gvDetailRow);


        return gvDetail;

    }

    protected void gvMaster_HtmlRowCreated1(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewTableRowEventArgs e)
    {
        if (e.RowType == GridViewRowType.Detail)
        {
            // 繫結明細資料表           
            ASPxGridView detailGrid = e.Row.FindChildControl<ASPxGridView>("gvDetail");
            detailGrid.DataSource = getDetailData();
            detailGrid.DataBind();
        }
    }
}
