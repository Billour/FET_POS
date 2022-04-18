using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class VSS_ORD04_ORD04 : System.Web.UI.Page
{
   protected void Page_Load(object sender, EventArgs e)
   {
      if (!Page.IsPostBack)
      {
         bindEmptyData();

      }
   }
   protected void bindEmptyData()
   {

      DataTable dtResult = new DataTable();

      gvMaster.DataSource = dtResult;
      gvMaster.DataBind();


   }
   protected void bindSummaryData()
   {
      divSummary.Style["display"] = "";
      DataTable dtResult = new DataTable();
      dtResult = getSummaryData();
      ProductListBox.DataSource = dtResult;
      ProductListBox.DataBind();
      if (ProductListBox.SelectedIndex == -1)
      {
         ProductListBox.SelectedIndex = 0;
      }
   }
   protected void bindMasterData(string ProductNo)
   {
      DataTable dtResult = new DataTable();
      dtResult = getMasterData(ProductNo);
      ViewState["gvMaster"] = dtResult;
      ////防編輯狀態
      //if (gvMaster.EditIndex != -1)
      //{
      //    gvMaster.EditIndex = -1;
      //}
      gvMaster.DataSource = dtResult;
      gvMaster.DataBind();

   }


   protected DataTable getSummaryData()
   {
      DataTable dtResult = new DataTable();
      dtResult.Columns.Add("商品名稱", typeof(string));
      dtResult.Columns.Add("商品編號", typeof(string));
      for (int i = 0; i < 10; i++)
      {
         DataRow NewRow = dtResult.NewRow();
         NewRow["商品名稱"] = string.Format("(10190007{0} LG KF{0}00 黑簡配)", i);
         NewRow["商品編號"] = string.Format("(10190007{0} LG KF{0}00 黑簡配)", i);
         dtResult.Rows.Add(NewRow);
      }
      return dtResult;

   }
   private DataTable getMasterData(string ProductNo)
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

      for (int i = 1; i < 10; i++)
      {
         DataRow NewRow = dtResult.NewRow();
         NewRow["訂單日期"] = "2010/07/13"; //"2010/07/13";
         NewRow["訂單編號"] = "SO2101-100701" + i.ToString();
         NewRow["訂單狀態"] = "已傳輸";
         NewRow["門市編號"] = "210" + i;
         NewRow["門市名稱"] = "門市" + i;
         NewRow["訂購量"] = 3;
         NewRow["庫存量"] = (i % 2) == 0 ? 3 : 2;
         NewRow["業助調整數量"] = 1;
         NewRow["備註"] = "備註 " + ProductNo;
         dtResult.Rows.Add(NewRow);
      }
      string TotalOrder = string.Empty;
      string TotalInventory = string.Empty;
      string TotalRevise = string.Empty;

      //計算-總訂貨量 (count:門市調整數量)
      //TotalOrder = dtResult.Compute("sum(門市調整數量)", "").ToString();
      lblAdjustmentQty.Text = dtResult.Compute("sum(訂購量)", "").ToString();

      //計算-總庫存量 (count:庫存量)
      //TotalInventory = dtResult.Compute("sum(庫存量)", "").ToString();
      //lblQtyOnHand.Text = dtResult.Compute("sum(庫存量)", "").ToString();
      //計算-總調整量 (count:業助調整數量)
      //TotalRevise = dtResult.Compute("sum(業助調整數量)", "").ToString();
      lblTotalAdjustmentQty.Text = dtResult.Compute("sum(業助調整數量)", "").ToString();

      ViewState["TotalOrder"] = TotalOrder;
      ViewState["TotalInventory"] = TotalInventory;
      ViewState["TotalRevise"] = TotalRevise;

      return dtResult;
   }



   protected void btnSearch_Click(object sender, EventArgs e)
   {
      bindSummaryData();

      string ProductNo = ProductListBox.SelectedValue;
      bindMasterData(ProductNo);
   }

   protected void ProductListBox_SelectedIndexChanged(object sender, EventArgs e)
   {
      string ProductNo = ProductListBox.SelectedValue;
      bindMasterData(ProductNo);
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
      string ProductNo = ProductListBox.SelectedValue;
      bindMasterData(ProductNo);
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
      string FORMAT = "<br/>(總量={0})";
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
   #region 分頁相關 (共用)
   protected void GridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
   {
      //此函式可共用
      GridView gridview = sender as GridView;
      gridview.PageIndex = e.NewPageIndex;

      DataTable dt = ViewState[gridview.ID] as DataTable ?? null;//GridView的資料要存於ViewState以方便共用此Function
      gridview.DataSource = dt;
      gridview.DataBind();
   }
   protected void btnGoToIndex_Click(object sender, EventArgs e)
   {
      //此函式可共用
      GridView gridview = (sender as Button).Parent.Parent.Parent.Parent as GridView;
      TextBox textbox = (sender as Button).Parent.FindControl("tbGoToIndex") as TextBox;
      string strIndex = textbox.Text.Trim();
      int index = 0;
      if (int.TryParse(strIndex, out index))
      {
         index = index - 1;
         if (index >= 0 && index <= gridview.PageCount - 1)
         {
            gridview.PageIndex = index;
            DataTable dt = ViewState[gridview.ID] as DataTable ?? null;//GridView的資料要存於ViewState以方便共用此Function
            gridview.DataSource = dt;
            gridview.DataBind();
         }
         else
         {
            textbox.Text = string.Empty;
         }
      }
      else
      {
         textbox.Text = string.Empty;
      }
   }
   #endregion

   //int adjustmentQty = 0;

   protected void gvMaster_RowDataBound(object sender, GridViewRowEventArgs e)
   {
      DataRowView view = e.Row.DataItem as DataRowView;
      if (view != null)
      {

         if (e.Row.RowType == DataControlRowType.DataRow)
         {
            if (e.Row.RowIndex != -1)
            {
               if (view.Row["訂單狀態"].ToString() == "已轉單")
               {
                  if (e.Row.FindControl("LinkButton3") != null)
                  {
                     Button LinkButton3 = (Button)(e.Row.FindControl("LinkButton3"));
                     LinkButton3.Visible = false;
                  }

               }
               else
               {
                  if (e.Row.FindControl("LinkButton3") != null)
                  {
                     Button LinkButton3 = (Button)(e.Row.FindControl("LinkButton3"));
                     LinkButton3.Visible = true;
                  }
               }
            }
         }

      }
   }
}
