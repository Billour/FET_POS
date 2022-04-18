using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Advtek.Utility;
using System.Collections;

public partial class VSS_ORD08_ORD08 : System.Web.UI.Page
{

   protected void Page_Load(object sender, EventArgs e)
   {
      string dno = Request.QueryString["dno"] == null ? "" : Request.QueryString["dno"].ToString().Trim();
      this.ViewState["dno"] = dno;

      if (!Page.IsPostBack)
      {
         //divShow.Style["display"] = "none";
         Button2.Visible = false;
         Button4.Visible = false;
         Button13.Visible = false;
         Button14.Visible = false;
         Button15.Visible = false;
         if (this.ViewState["dno"] == "")
         {
            this.Label1.Text = "";
            this.showDetail.Visible = true;
            Button9.Enabled = false;
            DropDownList3.Enabled = false;
            Button19.Enabled = false;
            Button17.Enabled = false;
            bindEmptyData();
            bindEmptyData1();
         }
         else
         {
            bindMasterData();
         }
      }
   }

   protected void bindEmptyData()
   {

      DataTable dtResult = new DataTable();

      gvMaster.DataSource = dtResult;
      gvMaster.DataBind();


   }
   protected void bindEmptyData1()
   {
      DataTable dtResult = new DataTable();
      gvDetail.Visible = true;
      gvDetail.DataSource = dtResult;
      gvDetail.DataBind();
      System.Web.UI.HtmlControls.HtmlTableRow htr = (System.Web.UI.HtmlControls.HtmlTableRow)gvDetail.FindChildControl<System.Web.UI.HtmlControls.HtmlTableRow>("showEmpty");
      htr.Visible = false;

   }
   protected void bindMasterData()
   {
      DataTable dtResult = new DataTable();
      dtResult = getMasterData();
      ViewState["gvMaster"] = dtResult;
      gvMaster.DataSource = dtResult;
      gvMaster.DataBind();
   }
   private DataTable GetEmptyDataTable1()
   {
      DataTable dtResult = new DataTable();
      dtResult.Columns.Add("商品編號", typeof(string));
      dtResult.Columns.Add("商品名稱", typeof(string));
      dtResult.Columns.Add("ATR量", typeof(string));
      dtResult.Columns.Add("主配量", typeof(string));
      dtResult.Columns.Add("備註", typeof(string));
      return dtResult;
   }
   private DataTable getMasterData()
   {
      DataTable dtResult = new DataTable();
      dtResult.Columns.Add("商品編號", typeof(string));
      dtResult.Columns.Add("商品名稱", typeof(string));
      dtResult.Columns.Add("ATR量", typeof(string));
      dtResult.Columns.Add("主配量", typeof(string));
      dtResult.Columns.Add("備註", typeof(string));

      for (int i = 0; i < 20; i++)
      {
         DataRow NewRow = dtResult.NewRow();
         NewRow["商品編號"] = "0000" + i;
         NewRow["商品名稱"] = "品名" + i;
         NewRow["ATR量"] = "15";
         NewRow["主配量"] = "10";
         NewRow["備註"] = string.Format("BenQ S{0}20i(銀,簡)", i);
         dtResult.Rows.Add(NewRow);
      }
      return dtResult;
   }

   protected void bindDetailData(string KeyNo)
   {
      DataTable dtResult = new DataTable();
      dtResult = getDetailData(KeyNo);
      ViewState["gvDetail"] = dtResult;
      gvDetail.DataSource = dtResult;
      gvDetail.DataBind();
   }

   private DataTable GetEmptyDataTable()
   {
      DataTable dtResult = new DataTable();
      dtResult.Columns.Add("出貨倉別", typeof(string));
      dtResult.Columns.Add("門市編號", typeof(string));
      dtResult.Columns.Add("門市名稱", typeof(string));
      dtResult.Columns.Add("主動配貨量", typeof(string));
      return dtResult;
   }

   private DataTable getDetailData(string KeyNo)
   {
      DataTable dtResult = new DataTable();
      dtResult.Columns.Add("出貨倉別", typeof(string));
      dtResult.Columns.Add("門市編號", typeof(string));
      dtResult.Columns.Add("門市名稱", typeof(string));
      dtResult.Columns.Add("主動配貨量", typeof(string));


      for (int i = 0; i < 2; i++)
      {
         DataRow NewRow = dtResult.NewRow();
         NewRow["出貨倉別"] = i + 1;
         NewRow["門市編號"] = "2101";
         NewRow["門市名稱"] = "遠傳";
         NewRow["主動配貨量"] = "20";

         dtResult.Rows.Add(NewRow);
      }
      return dtResult;
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

   protected void gvDetail_RowEditing(object sender, GridViewEditEventArgs e)
   {
      GridView gridview = sender as GridView;
      //設定編輯欄位
      gridview.EditIndex = e.NewEditIndex;
      //Bind原查詢資料
      DataTable dt = ViewState["gvDetail"] as DataTable;
      gridview.DataSource = dt;
      gridview.DataBind();
   }

   protected void gvDetail_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
   {
      GridView gridview = sender as GridView;
      gridview.EditIndex = -1;
      //Bind原查詢資料
      DataTable dt = ViewState["gvDetail"] as DataTable;
      gridview.DataSource = dt;
      gridview.DataBind();
   }

   protected void GridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
   {
      //此函式可共用
      GridView gridview = sender as GridView;
      gridview.PageIndex = e.NewPageIndex;

      DataTable dt = ViewState[gridview.ID] as DataTable ?? null;//GridView的資料要存於ViewState以方便共用此Function
      gridview.DataSource = dt;
      gridview.DataBind();
   }
   protected void gvMaster_SelectedIndexChanged(object sender, EventArgs e)
   {
      GridView mygridview = sender as GridView;
      if (mygridview.SelectedIndex >= 0)
      {
         string KeyNo = mygridview.Rows[mygridview.SelectedIndex].Cells[2].Text;
         bindDetailData(KeyNo);
         gvDetail.Visible = true;
         showDetail.Visible = true;
      }
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
   protected void Button1_Click(object sender, EventArgs e)
   {
      gvMaster.ShowFooter = true;
      gvMaster.ShowFooterWhenEmpty = true;
      System.Web.UI.HtmlControls.HtmlTableRow tr =
          gvMaster.FindChildControl<System.Web.UI.HtmlControls.HtmlTableRow>("trEmptyData");
      if (tr != null)
      {
         // 隱藏顯示文字訊息的表格列
         tr.Visible = false;
      }
      else
      {
         // 重新繫結資料
         bindMasterData();
      }
   }
   /// <summary>
   /// 取消新增-隱藏Footer
   /// </summary>
   /// <param name="sender"></param>
   /// <param name="e"></param>
   protected void btnCancel_Click(object sender, EventArgs e)
   {
      gvMaster.ShowFooterWhenEmpty = false;
      gvMaster.ShowFooter = false;
      // 重新繫結資料
      if (gvMaster.Rows.Count == 0)
      {
         gvMaster.DataSource = GetEmptyDataTable1();
         gvMaster.DataBind();
      }
      else
      {
         bindMasterData();
      }
   }

   protected void Button9_Click(object sender, EventArgs e)
   {
      Button2.Visible = true;
      Button4.Visible = true;
      Button13.Visible = true;
      Button14.Visible = true;
      Button15.Visible = true;


      gvDetail.ShowFooter = true;

      gvDetail.ShowFooterWhenEmpty = true;
      System.Web.UI.HtmlControls.HtmlTableRow tr =
          gvDetail.FindChildControl<System.Web.UI.HtmlControls.HtmlTableRow>("showEmpty");
      if (tr != null)
      {
         // 隱藏顯示文字訊息的表格列
         tr.Visible = false;
      }
      else
      {
         // 重新繫結資料
         bindDetailData("0");
      }
   }
   /// <summary>
   /// 取消新增-隱藏Footer
   /// </summary>
   /// <param name="sender"></param>
   /// <param name="e"></param>
   protected void btnCancel1_Click(object sender, EventArgs e)
   {
      gvDetail.ShowFooterWhenEmpty = false;
      gvDetail.ShowFooter = false;
      // 重新繫結資料
      if (gvDetail.Rows.Count == 0)
      {
         gvDetail.DataSource = GetEmptyDataTable();
         gvDetail.DataBind();
      }
      else
      {
        bindDetailData("0");
      }
   }
   protected void gvMaster_RowUpdating1(object sender, GridViewUpdateEventArgs e)
   {
      GridView gridview = sender as GridView;

      //取得資料

      //更新資料庫

      //取消編輯狀態
      gridview.EditIndex = -1;

      //Bind新資料(重取資料)
      bindMasterData();
   }
   protected void gvDetail_RowUpdating(object sender, GridViewUpdateEventArgs e)
   {
      GridView gridview = sender as GridView;

      //取得資料

      //更新資料庫

      //取消編輯狀態
      gridview.EditIndex = -1;

      //Bind新資料(重取資料)
      bindDetailData("3");
   }
   protected void gvMaster_RowCommand(object sender, GridViewCommandEventArgs e)
   {
      if (e.CommandName == "View")
      {
         Button2.Visible = true;
         Button4.Visible = true;
         Button13.Visible = true;
         Button14.Visible = true;
         Button15.Visible = true;
         bindDetailData("2");
         gvDetail.Visible = true;
         showDetail.Visible = true;


         //bindDetailData();
         //gvDetail.Visible = true;

      }
   }
}
