using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class VSS_LEA_LEA07 : BasePage
{
   protected void Page_Load(object sender, EventArgs e)
   {
      if (!IsPostBack)
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
      dtResult.Columns.Add("類別", typeof(string));
      dtResult.Columns.Add("產品類別", typeof(string));
      dtResult.Columns.Add("產品名稱", typeof(string));
      dtResult.Columns.Add("外部廠商代碼", typeof(string));
      dtResult.Columns.Add("外部廠商名稱", typeof(string));
      dtResult.Columns.Add("開始日期", typeof(string));
      dtResult.Columns.Add("結束日期", typeof(string));
      dtResult.Columns.Add("更新人員", typeof(string));
      dtResult.Columns.Add("更新日期", typeof(string));

      string[] ary1 = { "漫遊租賃", "維修租賃" };

      for (int i = 0; i < 10; i++)
      {
         DataRow NewRow = dtResult.NewRow();
         NewRow["類別"] = ary1[i % 2];
         NewRow["產品類別"] = "產品類別" + i;
         NewRow["產品名稱"] = "產品名稱" + i;
         NewRow["外部廠商代碼"] = i;
         NewRow["外部廠商名稱"] = "外部廠商名稱" + i;
         NewRow["開始日期"] = "2010/09/01";
         NewRow["結束日期"] = System.DateTime.Now.AddDays(i).ToString("yyyy/MM/dd");
         NewRow["更新人員"] = "Jackey";
         NewRow["更新日期"] = DateTime.Now.AddDays(-i).ToShortDateString();
         dtResult.Rows.Add(NewRow);

      }
      return dtResult;
   }

   protected void btnSearch_Click(object sender, EventArgs e)
   {
      bindMasterData();
   }

   protected void btnSure_Click(object sender, EventArgs e)
   {
      Response.Redirect("LEA01.aspx");
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


}
