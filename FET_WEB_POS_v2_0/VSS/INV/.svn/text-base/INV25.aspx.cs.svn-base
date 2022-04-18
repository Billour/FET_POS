using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using DevExpress.Web.ASPxGridView;

public partial class VSS_INV25_INV25 : Advtek.Utility.BasePage
{
   protected void Page_Load(object sender, EventArgs e)
   {
       if (!IsPostBack)
       {
           bindMasterData(0);
       }
   }

   protected void bindMasterData(int TempCount)
   {
      DataTable dtGvMaster = new DataTable();
      dtGvMaster = getMasterData(TempCount);
      ViewState["gvMaster"] = dtGvMaster;
      gvMaster.DataSource = dtGvMaster;
      gvMaster.DataBind();
   }

   private DataTable getMasterData(int TempCount)
   {
      DataTable dtMaster = new DataTable();
      dtMaster.Columns.Clear();
      dtMaster.Columns.Add("商品料號", typeof(string));
      dtMaster.Columns.Add("商品名稱", typeof(string));
      dtMaster.Columns.Add("移出數量", typeof(string));
      dtMaster.Columns.Add("IMEI控管", typeof(string));
      dtMaster.Columns.Add("IMEI", typeof(string));

      for (int i = 0; i < TempCount; i++)
      {
         DataRow dtMasterRow = dtMaster.NewRow();
         dtMasterRow["商品料號"] = "A0001";
         dtMasterRow["商品名稱"] = "測試1";
         dtMasterRow["移出數量"] = i++;
         dtMasterRow["IMEI控管"] = ((i % 2) == 0) ? "0" : "1";
         dtMasterRow["IMEI"] = "0119300067" + i.ToString("0639");

         dtMaster.Rows.Add(dtMasterRow);
      }

      return dtMaster;

   }

   protected void btnAddNew_Click(object sender, EventArgs e)
   {
       //gvMaster.ShowFooter = true;
       //gvMaster.ShowFooterWhenEmpty = true;
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
       //bindMasterData(1);
       }
      //gvMaster.Visible = true;
       gvMaster.AddNewRow();
   }
   protected void btnCancel_Click(object sender, EventArgs e)
   {
       //gvMaster.ShowFooterWhenEmpty = false;
       //gvMaster.ShowFooter = false;
       // 重新繫結資料
       //if (gvMaster.Rows.Count == 0)
       //{
       //    bindMasterData(0);
       //    //gvMaster.DataSource = GetEmptyDataTable();
       //    //gvMaster.DataBind();
       //}
       //else
       //{
       //    bindMasterData(1);
       //}
   }
   protected void btnSave_Click(object sender, EventArgs e)
   {
       lblOrderNo.Text = "ST2101-100815001";
   }


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
      bindMasterData(1);
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

   protected void gvMaster_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
   {
       ASPxGridView grid = (ASPxGridView)sender;
       grid.CancelEdit();
       e.Cancel = true;
       bindMasterData(0);
   }
   protected void gvMaster_RowUpdating1(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
   {

   }
}
