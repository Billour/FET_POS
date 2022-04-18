using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Advtek.Utility;

public partial class VSS_LEA01_LEA01 : System.Web.UI.Page
{
   protected void Page_Load(object sender, EventArgs e)
   {
      if (!IsPostBack)
      {

         bindEmptyData();
         bindMasterData();
         bindData2();
         bindData3();

      }
   }
   protected void bindEmptyData()
   {

      DataTable dtResult = new DataTable();

      gvMaster.DataSource = dtResult;
      gvMaster.DataBind();
      
      gvDiscountItem.DataSource = dtResult;
      gvDiscountItem.DataBind();
   }
   protected void bindMasterData()
   {
      DataTable dtResult = new DataTable();

      dtResult = getMasterData();
      ViewState["gvMaster"] = dtResult;
      gvMaster.DataSource = dtResult;
      gvMaster.DataBind();
   }

   protected void bindData2()
   {
      DataTable dtResult = new DataTable();
      ViewState["gvDiscountItem"] = dtResult;
      dtResult = getData2();
      ViewState["gvDiscountItem"] = dtResult;
      gvDiscountItem.DataSource = dtResult;
      gvDiscountItem.DataBind();
   }
   protected void bindData3()
   {
      DataTable dtResult = new DataTable();
      dtResult = getData3();
      gvMobileStock.DataSource = dtResult;
      gvMobileStock.DataBind();
   }
   private DataTable getMasterData()
   {
      DataTable dtResult = new DataTable();
      dtResult.Columns.Add("賠償項目", typeof(string));
      dtResult.Columns.Add("金額", typeof(string));

      DataRow NewRow = dtResult.NewRow();
      NewRow["賠償項目"] = "電池";
      NewRow["金額"] = "1200";
      dtResult.Rows.Add(NewRow);
      return dtResult;
   }


   private DataTable getData2()
   {
      DataTable dtResult = new DataTable();
      dtResult.Columns.Add("折扣料號", typeof(string));
      dtResult.Columns.Add("折扣名稱", typeof(string));
      dtResult.Columns.Add("折扣金額", typeof(string));
      dtResult.Columns.Add("折扣比率", typeof(string));
      dtResult.Columns.Add("成本中心", typeof(string));
      dtResult.Columns.Add("會計科目", typeof(string));

      DataRow NewRow = dtResult.NewRow();
      NewRow["折扣料號"] = "折扣料號1";
      NewRow["折扣名稱"] = "折古名稱1";
      NewRow["折扣金額"] = "300";
      NewRow["折扣比率"] = "5";
      NewRow["成本中心"] = "成本中心1";
      NewRow["會計科目"] = "會計科目1";
      dtResult.Rows.Add(NewRow);
      return dtResult;
   }

   private DataTable getData3()
   {
      DataTable dtResult = new DataTable();
      dtResult.Columns.Add("門市代號", typeof(string));
      dtResult.Columns.Add("門市名稱", typeof(string));
      dtResult.Columns.Add("手機序號", typeof(string));


      DataRow NewRow = dtResult.NewRow();
      NewRow["門市代號"] = "NU001";
      NewRow["門市名稱"] = "內湖門市";
      NewRow["手機序號"] = "1234567";

      dtResult.Rows.Add(NewRow);


      return dtResult;
   }
   protected void btnAdd1_Click(object sender, EventArgs e)
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
   protected void btnCancel1_Click(object sender, EventArgs e)
   {
       gvMaster.ShowFooterWhenEmpty = false;
       gvMaster.ShowFooter = false;
       // 重新繫結資料
       if (gvMaster.Rows.Count == 0)
       {
           //gvMaster.DataSource = GetEmptyDataTable();
           gvMaster.DataBind();
       }
       else
       {
           bindMasterData();
       }
   }


   protected void btnAdd2_Click(object sender, EventArgs e)
   {
       gvDiscountItem.ShowFooter = true;
       gvDiscountItem.ShowFooterWhenEmpty = true;
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
           bindData2();
       }
   }
   /// <summary>
   /// 取消新增-隱藏Footer
   /// </summary>
   /// <param name="sender"></param>
   /// <param name="e"></param>
   protected void btnCancel2_Click(object sender, EventArgs e)
   {
       gvDiscountItem.ShowFooterWhenEmpty = false;
       gvDiscountItem.ShowFooter = false;
       // 重新繫結資料
       if (gvDiscountItem.Rows.Count == 0)
       {
           //gvMaster.DataSource = GetEmptyDataTable();
           gvDiscountItem.DataBind();
       }
       else
       {
           bindData2();
       }
   }

   protected void RadioButtonList1_SelectedIndexChanged(object sender, EventArgs e)
   {
      if (this.RadioButtonList1.SelectedValue == "2")
      {
         this.TextBox5.Enabled = (false);
         this.TextBox6.Enabled = (false);
      }
      else
      {
         this.TextBox5.Enabled = (true);
         this.TextBox6.Enabled = (true);

      }
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

   #region gvMaster 編輯/更新/取消 相關觸發事件
   protected void gvDiscountItem_RowEditing(object sender, GridViewEditEventArgs e)
   {
       GridView gridview = sender as GridView;
       //設定編輯欄位
       gridview.EditIndex = e.NewEditIndex;
       //Bind原查詢資料
       DataTable dt = ViewState["gvDiscountItem"] as DataTable;
       gridview.DataSource = dt;
       gridview.DataBind();
   }

   protected void gvDiscountItem_RowUpdating(object sender, GridViewUpdateEventArgs e)
   {
       GridView gridview = sender as GridView;

       //取得資料

       //更新資料庫

       //取消編輯狀態
       gridview.EditIndex = -1;

       //Bind新資料(重取資料)
       bindData2();
   }

   protected void gvDiscountItem_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
   {
       GridView gridview = sender as GridView;
       gridview.EditIndex = -1;
       //Bind原查詢資料
       DataTable dt = ViewState["gvDiscountItem"] as DataTable;
       gridview.DataSource = dt;
       gridview.DataBind();
   }
   #endregion

   private DataTable GetDetailedData()
   {
      DataTable dt = new DataTable();
      dt.Columns.Clear();
      dt.Columns.Add("廠商代號", typeof(string));
      dt.Columns.Add("結算金額", typeof(int));
      dt.Columns.Add("銷貨總額", typeof(int));
      dt.Columns.Add("銷貨金額", typeof(int));
     

      DataRow dr = dt.NewRow();
      dr["廠商代號"] = "AC001";
      dr["結算金額"] = 205000;
      dr["銷貨總額"] = 10000;
      dr["銷貨金額"] = 0;
      
      dt.Rows.Add(dr);

      dr = dt.NewRow();
      dr["廠商代號"] = "AC002";
      dr["結算金額"] = 89000;
      dr["銷貨總額"] = 10000;
      dr["銷貨金額"] = 0;
     
      dt.Rows.Add(dr);

      return dt;
   }
   protected void btnSave_Click(object sender, EventArgs e)
   {
      //Panel2.Visible = true;

      DataTable dt = GetDetailedData();



      DataView dv = new DataView(dt);

      FormView1.DataSource = dv;
      FormView1.DataBind();
      Panel2.Visible = true;
   }
}
