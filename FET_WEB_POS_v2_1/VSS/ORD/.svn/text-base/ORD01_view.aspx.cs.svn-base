using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using Advtek.Utility;
using DevExpress.Web.ASPxGridView;
using System.Drawing;
using DevExpress.Web.ASPxEditors;

public partial class VSS_ORD_ORD01_view : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            bindEmptyData();

            bindgvMaster();

            DataTable dtProdDetail = new DataTable();
            dtProdDetail.Columns.Clear();
            dtProdDetail.Columns.Add("商品編號", typeof(string));
            dtProdDetail.Columns.Add("商品名稱", typeof(string));
            dtProdDetail.Columns.Add("數量", typeof(int));

            BindData();
        }
    }

    protected void drMasterDV_HtmlRowCreated(object sender, ASPxGridViewTableRowEventArgs e)
    {
        if (e.RowType == GridViewRowType.Data)
        {
            if (e.VisibleIndex > 3)
            {
                ASPxButton ASPxButton3 = e.Row.FindChildControl<ASPxButton>("ASPxButton3");
                ASPxButton3.Enabled = false;
            }
        }
        else if (e.RowType == GridViewRowType.Detail)
        {
            // 繫結明細資料表
            ASPxGridView detailGrid = e.Row.FindChildControl<ASPxGridView>("gvDetailDV");
            detailGrid.DataSource = getGvProdDetailData();
            detailGrid.DataBind();
        }
    }
    protected void drMasterDV_HtmlCommandCellPrepared(object sender, ASPxGridViewTableCommandCellEventArgs e)
    {
        //設定自定名令BUTTON

        if (e.VisibleIndex >= 3)
            e.Cell.Enabled = false;
    }


    protected void drMasterDV_CustomButtonCallback(object sender, ASPxGridViewCustomButtonCallbackEventArgs e)
    {
        //自定欄位回呼
        if (e.ButtonID == "btnSelect")
        {
            bindgvDetail();
        }
    }

    protected void drMasterDV_RowCommand(object sender, ASPxGridViewRowCommandEventArgs e)
    {
        if (drMasterDV.DetailRows.IsVisible(e.VisibleIndex))
        {
            drMasterDV.DetailRows.CollapseRow(e.VisibleIndex);
        }
        else
        {
            drMasterDV.DetailRows.ExpandRow(e.VisibleIndex);
        }
    }

    private void BindData()
    {
        DataTable orders = new DataTable();
        orders.Columns.Clear();
        orders.Columns.Add("商品編號", typeof(string));
        orders.Columns.Add("商品名稱", typeof(string));
        orders.Columns.Add("需求量", typeof(int));
        orders.Columns.Add("門市庫存量", typeof(int));
        orders.Columns.Add("訂購量", typeof(int));

        DataRow row = orders.NewRow();
        row["商品編號"] = "100100100";
        row["商品名稱"] = "A手機";
        row["需求量"] = 10;
        row["門市庫存量"] = 5;
        row["訂購量"] = 5;
        orders.Rows.Add(row);

        row = orders.NewRow();
        row["商品編號"] = "100100101";
        row["商品名稱"] = "B手機";
        row["需求量"] = 10;
        row["門市庫存量"] = 0;
        row["訂購量"] = 10;
        orders.Rows.Add(row);


        row = orders.NewRow();
        row["商品編號"] = "100100102";
        row["商品名稱"] = "C手機";
        row["需求量"] = 10;
        row["門市庫存量"] = 2;
        row["訂購量"] = 8;
        orders.Rows.Add(row);


    }

    protected void bindEmptyData()
    {
        DataTable dtResult = new DataTable();

        //gvMaster.DataSource = dtResult;
        //gvMaster.DataBind();
    }
    protected void bindgvMaster()
    {
        DataTable dtGvMaster = new DataTable();
        dtGvMaster = getMasterData();
        ViewState["gvMaster"] = dtGvMaster;
        drMasterDV.DataSource = dtGvMaster;
        drMasterDV.DataBind();
        //  gvMaster.DataSource = dtGvMaster;
        //  gvMaster.DataBind();

        drMasterDV.DetailRows.ExpandRow(0);
    }

    private DataTable getMasterData()
    {
        DataTable dtMaster = new DataTable();
        dtMaster.Columns.Clear();
        dtMaster.Columns.Add("項次", typeof(int));
        dtMaster.Columns.Add("商品編號", typeof(string));
        dtMaster.Columns.Add("商品名稱", typeof(string));
        dtMaster.Columns.Add("建議訂購量", typeof(int));
        dtMaster.Columns.Add("網購需求量", typeof(int));
        dtMaster.Columns.Add("門市庫存量", typeof(int));
        dtMaster.Columns.Add("在途量", typeof(int));
        dtMaster.Columns.Add("總部核准量", typeof(int));
        dtMaster.Columns.Add("當日總訂購量", typeof(int));
        dtMaster.Columns.Add("驗收量", typeof(int));
        //dtMaster.Columns.Add("業助調整數量", typeof(string));

        for (int i = 1; i < 12; i++)
        {
            DataRow dtMasterRow = dtMaster.NewRow();
            dtMasterRow["項次"] = i;
            dtMasterRow["商品編號"] = "A000" + i;
            dtMasterRow["商品名稱"] = "商品名稱" + i;
            dtMasterRow["建議訂購量"] = 10 * (i + 1) / 2;
            dtMasterRow["門市庫存量"] = (i + 1);
            dtMasterRow["在途量"] = 1 * (i + 1);
            dtMasterRow["網購需求量"] = 4 * (i + 1) - 7;
            dtMasterRow["總部核准量"] = 2;
            dtMasterRow["當日總訂購量"] = 0;
            dtMasterRow["驗收量"] = 0;

            //Convert.ToInt32(dtMasterRow["建議訂購量"]) - Convert.ToInt32(dtMasterRow["在途量"]) - Convert.ToInt32(dtMasterRow["今日已訂購量"]);
            //dtMasterRow["業助調整數量"] = "";//(i*10)%6;
            dtMaster.Rows.Add(dtMasterRow);
            dtMaster.Rows[i - 1]["驗收量"] = Convert.ToInt32(dtMaster.Rows[i - 1]["總部核准量"]);
            dtMaster.Rows[i - 1]["當日總訂購量"] = Convert.ToInt32(dtMaster.Rows[i - 1]["建議訂購量"])
                - Convert.ToInt32(dtMaster.Rows[i - 1]["總部核准量"])
                - Convert.ToInt32(dtMaster.Rows[i - 1]["網購需求量"])
                - Convert.ToInt32(dtMaster.Rows[i - 1]["在途量"]);
        }
        return dtMaster;

    }

    protected void bindgvDetail()
    {
        DataTable dtGvProdDetail = new DataTable();
        dtGvProdDetail = getGvProdDetailData();

        //  gvDetail.DataSource = dtGvProdDetail;
        // gvDetail.DataBind();
    }

    private DataTable getGvProdDetailData()
    {
        DataTable dtProdDetail = new DataTable();
        dtProdDetail.Columns.Clear();
        dtProdDetail.Columns.Add("商品編號", typeof(string));
        dtProdDetail.Columns.Add("商品名稱", typeof(string));
        dtProdDetail.Columns.Add("搭配量", typeof(int));
        dtProdDetail.Columns.Add("訂購量", typeof(int));

        for (int i = 1; i < 2; i++)
        {
            DataRow dtProdDetailRow = dtProdDetail.NewRow();
            dtProdDetailRow["商品編號"] = "A000" + i;
            dtProdDetailRow["商品名稱"] = "商品名稱" + i;
            dtProdDetailRow["搭配量"] = 1;//100 * (i + 1);
            dtProdDetailRow["訂購量"] = 2;
            dtProdDetail.Rows.Add(dtProdDetailRow);
        }
        return dtProdDetail;
    }

}
