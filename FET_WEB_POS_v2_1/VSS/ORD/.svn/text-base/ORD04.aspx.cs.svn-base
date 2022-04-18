using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using DevExpress.Web.ASPxGridView;

public partial class VSS_ORD_ORD04 : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            bindEmptyData();
            Literal6.Text = DateTime.Now.ToString("yyyy/MM/dd HH:mm");
            txtOrdDateStart.Text = DateTime.Now.ToString("yyyy/MM/dd");
            txtOrdDateEnd.Text = DateTime.Now.ToString("yyyy/MM/dd");
            

        }
    }
    protected void bindEmptyData()
    {

        DataTable dtResult = new DataTable();

        // gvMaster.DataSource = dtResult;
        // gvMaster.DataBind();
        gvMasterDV.DataSource = dtResult;
        gvMasterDV.DataBind();
        gvDetailDV.DataSource = dtResult;
        gvDetailDV.DataBind();




    }
    //protected void bindSummaryData()
    //{
    //    divSummary.Style["display"] = "";
    //    DataTable dtResult = new DataTable();
    //    dtResult = getSummaryData();
    //    ProductListBox.DataSource = dtResult;
    //    ProductListBox.DataBind();
    //    if (ProductListBox.SelectedIndex == -1)
    //    {
    //        ProductListBox.SelectedIndex = 0;
    //    }
    //}
    protected void bindDetailData()
    {
        DataTable dtResult = new DataTable();
        dtResult = getDetailData();
        ViewState["gvDetail"] = dtResult;
        gvDetailDV.DataSource = dtResult;
        gvDetailDV.DataBind();
    }

    private DataTable getDetailData()
    {
        DataTable dtResult = new DataTable();

        dtResult.Columns.Add("商品料號", typeof(string));
        dtResult.Columns.Add("商品名稱", typeof(string));
        dtResult.Columns.Add("可分配量", typeof(string));
        dtResult.Columns.Add("總訂貨量", typeof(string));
        dtResult.Columns.Add("總調整量", typeof(string));

        for (int i = 0; i < 2; i++)
        {
            DataRow NewRow = dtResult.NewRow();


            NewRow["商品料號"] = "10010010" + 1;
            NewRow["商品名稱"] = "Nokia";
            NewRow["可分配量"] = "50";
            NewRow["總訂貨量"] = "100";
            NewRow["總調整量"] = "50";
            dtResult.Rows.Add(NewRow);
        }
        return dtResult;
    }

    protected void bindMasterData()
    {
        DataTable dtResult = new DataTable();
        dtResult = getMasterData();
        ViewState["gvMaster"] = dtResult;
        ////防編輯狀態
        //if (gvMaster.EditIndex != -1)
        //{
        //    gvMaster.EditIndex = -1;
        //}
        // gvMaster.DataSource = dtResult;
        // gvMaster.DataBind();
        gvMasterDV.DataSource = dtResult;
        gvMasterDV.DataBind();
    }


    //protected DataTable getSummaryData()
    //{
    //    DataTable dtResult = new DataTable();
    //    dtResult.Columns.Add("商品名稱", typeof(string));
    //    dtResult.Columns.Add("商品編號", typeof(string));
    //    for (int i = 0; i < 10; i++)
    //    {
    //        DataRow NewRow = dtResult.NewRow();
    //        NewRow["商品名稱"] = string.Format("(10190007{0} LG KF{0}00 黑簡配)", i);
    //        NewRow["商品編號"] = string.Format("(10190007{0} LG KF{0}00 黑簡配)", i);
    //        dtResult.Rows.Add(NewRow);
    //    }
    //    return dtResult;

    //}
    private DataTable getMasterData()
    {
        DataTable dtResult = new DataTable();
        dtResult.Columns.Add("訂單日期", typeof(string));
        dtResult.Columns.Add("訂單編號", typeof(string));
        dtResult.Columns.Add("訂單狀態", typeof(string));
        dtResult.Columns.Add("門市編號", typeof(string));
        dtResult.Columns.Add("門市名稱", typeof(string));
        dtResult.Columns.Add("訂購量", typeof(int));
        dtResult.Columns.Add("庫存量", typeof(int));
        dtResult.Columns.Add("業助調整數量", typeof(int));
        dtResult.Columns.Add("備註", typeof(string));

        string[] array1 = { "已傳輸", "已成單" };
        for (int i = 1; i < 10; i++)
        {
            DataRow NewRow = dtResult.NewRow();
            NewRow["訂單日期"] = "2010/07/13"; //"2010/07/13";
            NewRow["訂單編號"] = "SO2101-100701" + i.ToString();
            NewRow["訂單狀態"] = array1[i % 2];
            NewRow["門市編號"] = "210" + i;
            NewRow["門市名稱"] = "門市" + i;
            NewRow["訂購量"] = 3;
            NewRow["庫存量"] = (i % 2) == 0 ? 3 : 2;
            NewRow["業助調整數量"] = 1;
            NewRow["備註"] = "備註 (101900077 LG KF700 黑簡配)";
            dtResult.Rows.Add(NewRow);
        }
        string TotalOrder = string.Empty;
        string TotalInventory = string.Empty;
        string TotalRevise = string.Empty;

        //計算-總訂貨量 (count:門市調整數量)
        //TotalOrder = dtResult.Compute("sum(門市調整數量)", "").ToString();
        //lblAdjustmentQty.Text = dtResult.Compute("sum(訂購量)", "").ToString();

        //計算-總庫存量 (count:庫存量)
        //TotalInventory = dtResult.Compute("sum(庫存量)", "").ToString();
        //lblQtyOnHand.Text = dtResult.Compute("sum(庫存量)", "").ToString();
        //計算-總調整量 (count:業助調整數量)
        //TotalRevise = dtResult.Compute("sum(業助調整數量)", "").ToString();
        //lblTotalAdjustmentQty.Text = dtResult.Compute("sum(業助調整數量)", "").ToString();

        ViewState["TotalOrder"] = TotalOrder;
        ViewState["TotalInventory"] = TotalInventory;
        ViewState["TotalRevise"] = TotalRevise;

        return dtResult;
    }



    protected void btnSearch_Click(object sender, EventArgs e)
    {
        //bindSummaryData();
        bindDetailData();
        gvDetailDV.Visible = true;

        bindMasterData();

        //string ProductNo = ProductListBox.SelectedValue;
        //bindMasterData(ProductNo);
    }

    protected void ProductListBox_SelectedIndexChanged(object sender, EventArgs e)
    {
        //string ProductNo = ProductListBox.SelectedValue;
        //bindMasterData(ProductNo);
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
        //string ProductNo = ProductListBox.SelectedValue;
        //bindMasterData(ProductNo);
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
    protected void gvMaster_RowCreated(object sender, GridViewRowEventArgs e)
    {
        //string FORMAT = "<br/>(總量={0})";
        if (e.Row != null && e.Row.RowType == DataControlRowType.Header)
        {
            string TotalOrder = string.Empty;
            string TotalInventory = string.Empty;
            string TotalRevise = string.Empty;
            /*
            TotalOrder = ViewState["TotalOrder"] as string ?? "";
            TotalInventory = ViewState["TotalInventory"] as string ?? "";
            TotalRevise = ViewState["TotalRevise"] as string ?? "";

            e.Row.Cells[6].Text += string.Format(FORMAT, TotalOrder);
            e.Row.Cells[7].Text += string.Format(FORMAT, TotalInventory);
            e.Row.Cells[8].Text += string.Format(FORMAT, TotalRevise);
            */
        }
    }
    

    //int adjustmentQty = 0;

    protected void gvMaster_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        //DataRowView view = e.Row.DataItem as DataRowView;
        //if (view != null)
        //{

        //    if (e.Row.RowType == DataControlRowType.DataRow)
        //    {
        //        if (e.Row.RowIndex != -1)
        //        {
        //            if (view.Row["訂單狀態"].ToString() == "已轉單")
        //            {
        //                if (e.Row.FindControl("LinkButton3") != null)
        //                {
        //                    Button LinkButton3 = (Button)(e.Row.FindControl("LinkButton3"));
        //                    LinkButton3.Visible = false;
        //                }

        //            }
        //            else
        //            {
        //                if (e.Row.FindControl("LinkButton3") != null)
        //                {
        //                    Button LinkButton3 = (Button)(e.Row.FindControl("LinkButton3"));
        //                    LinkButton3.Visible = true;
        //                }
        //            }
        //        }
        //    }

        //}
    }
    protected void gvMasterDV_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
    {
        //若不使用DataSourceID時UPDATE INSERT 和DELPETE 都要自己控制 以下為UPDATE時需自行控制
        ASPxGridView grid = (ASPxGridView)sender;
        DataTable dt = ViewState["gvMaster"] as DataTable ?? null;
        DataRow[] DRSelf = dt.Select("訂單編號='" + e.Keys[0].ToString().Trim() + "'");
        if (DRSelf.Length > 0)
        {
            DRSelf[0]["業助調整數量"] = e.NewValues["業助調整數量"];
            DRSelf[0]["備註"] = e.NewValues["備註"];
        }
        grid.CancelEdit();
        e.Cancel = true;
        grid.DataSource = dt;
        grid.DataBind();
    }
    protected void gvMasterDV_PageIndexChanged(object sender, EventArgs e)
    {
        DataTable dt = ViewState["gvMaster"] as DataTable;
        ((ASPxGridView)sender).DataSource = dt;
        ((ASPxGridView)sender).DataBind();
    }


    protected void gvMaster_CommandButtonInitialize(object sender, ASPxGridViewCommandButtonEventArgs e)
    {
        if (e.VisibleIndex > -1)
        {
            if (e.ButtonType == ColumnCommandButtonType.Edit)
            {
                string status = gvMasterDV.GetRowValues(e.VisibleIndex, "訂單狀態").ToString();

                if (status == "已成單")
                {
                    e.Enabled = false;
                }
            }
        }

    }


}
