using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class VSS_ORD11_ORD11 : System.Web.UI.Page
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

   private DataTable GetEmptyDataTable()
   {
       DataTable dtResult = new DataTable();
       dtResult.Columns.Add("商品料號", typeof(string));
       dtResult.Columns.Add("商品名稱", typeof(string));
       dtResult.Columns.Add("銷售基準", typeof(string));
       dtResult.Columns.Add("開始日期", typeof(string));
       dtResult.Columns.Add("結束日期", typeof(string));
       dtResult.Columns.Add("安全係數", typeof(string));
       
       return dtResult;
   }

   private DataTable getMasterData()
   {
      DataTable dtResult = new DataTable();
      dtResult.Columns.Add("商品料號", typeof(string));
      dtResult.Columns.Add("商品名稱", typeof(string));
      dtResult.Columns.Add("銷售基準", typeof(string));
      dtResult.Columns.Add("開始日期", typeof(string));
      dtResult.Columns.Add("結束日期", typeof(string));  
      dtResult.Columns.Add("安全係數", typeof(string));

      for (int i = 0; i < 10; i++)
      {
         DataRow NewRow = dtResult.NewRow();
         NewRow["商品料號"] = "1001001" + i.ToString("00");
         NewRow["商品名稱"] = "商品名稱" + i;
         NewRow["銷售基準"] = (i % 3).ToString();
         NewRow["開始日期"] = (i % 3) == 2?"2010/07/01":"";
         NewRow["結束日期"] = (i % 3) == 2 ? "2010/07/01" : "";
         NewRow["安全係數"] = "7";
         dtResult.Rows.Add(NewRow);

      }
      return dtResult;
   }

   protected void btnSearch_Click(object sender, EventArgs e)
   {
      bindMasterData();
   }

   protected void gvMaster_RowEditing(object sender, GridViewEditEventArgs e)
   {
      GridView gridview = sender as GridView;
      //設定編輯欄位
      gridview.EditIndex = e.NewEditIndex;
      this.ViewState["editIndex"] = gridview.EditIndex;
      //Bind原查詢資料
      DataTable dt = ViewState["gvMaster"] as DataTable;
      gridview.DataSource = dt;
      gridview.DataBind();
   }
   /// <summary>
   /// 新增-顯示Footer
   /// </summary>
   /// <param name="sender"></param>
   /// <param name="e"></param>
   protected void btnAdd_Click(object sender, EventArgs e)
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
           gvMaster.DataSource = GetEmptyDataTable();
           gvMaster.DataBind();
       }
       else
       {
           bindMasterData();
       }
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

   protected void gvMaster_RowDataBound(object sender, GridViewRowEventArgs e)
   {
      if (e.Row.RowType == DataControlRowType.DataRow)
      {
         if (e.Row.RowState != DataControlRowState.Edit && e.Row.RowState != (DataControlRowState.Alternate|DataControlRowState.Edit))
         {
            HiddenField hf1 = (HiddenField)e.Row.Cells[4].FindControl("hf1");
            Label lb1 = (Label)e.Row.Cells[4].FindControl("Label11");
            lb1.Text = (hf1.Value == "0" ? "半個月" : (hf1.Value == "1" ? "一個月" : "指定期間"));
         }
         else
         {
            DropDownList ddl1 = (DropDownList)e.Row.Cells[4].FindControl("DropDownList1");
            HiddenField hf2 = (HiddenField)e.Row.Cells[4].FindControl("hf2");
            AdvTekUserCtrl.postbackDate_TextBox ptb1 = (AdvTekUserCtrl.postbackDate_TextBox)e.Row.Cells[5].FindControl("PostbackDate_TextBox3");
            AdvTekUserCtrl.postbackDate_TextBox ptb2 = (AdvTekUserCtrl.postbackDate_TextBox)e.Row.Cells[6].FindControl("PostbackDate_TextBox4");

            if (hf2.Value != "2") 
            {
               ptb1.Enabled = (false);
               ptb2.Enabled = (false);
            }

            ddl1.SelectedIndex = int.Parse(hf2.Value);

         }
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

   protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
   {
      DropDownList ddl = (DropDownList)gvMaster.Rows[int.Parse(this.ViewState["editIndex"].ToString())].Cells[4].FindControl("DropDownList1");

      AdvTekUserCtrl.postbackDate_TextBox ptb1 = (AdvTekUserCtrl.postbackDate_TextBox)gvMaster.Rows[int.Parse(this.ViewState["editIndex"].ToString())].Cells[5].FindControl("PostbackDate_TextBox3");
      AdvTekUserCtrl.postbackDate_TextBox ptb2 = (AdvTekUserCtrl.postbackDate_TextBox)gvMaster.Rows[int.Parse(this.ViewState["editIndex"].ToString())].Cells[6].FindControl("PostbackDate_TextBox4");

      if (ddl.SelectedIndex == 2)
      {
         ptb1.Enabled = (true);
         ptb2.Enabled = (true);
      }
      else
      {
         ptb1.Enabled = (false);
         ptb2.Enabled = (false);
      }
   }
   protected void DropDownList2_SelectedIndexChanged(object sender, EventArgs e)
   {
      //DropDownList ddl = (DropDownList)gvMaster.FooterRow.Cells[4].FindControl("DropDownList2");
      DropDownList ddl = (DropDownList)sender;

      GridViewRow gvr = ddl.Parent.Parent as GridViewRow;
      
      AdvTekUserCtrl.postbackDate_TextBox ptb1 = (AdvTekUserCtrl.postbackDate_TextBox)gvr.FindControl("PostbackDate_TextBox33");
      AdvTekUserCtrl.postbackDate_TextBox ptb2 = (AdvTekUserCtrl.postbackDate_TextBox)gvr.FindControl("PostbackDate_TextBox44");

      if (ddl.SelectedIndex == 2)
      {
         ptb1.Enabled = (true);
         ptb2.Enabled = (true);
      }
      else
      {
         ptb1.Enabled = (false);
         ptb2.Enabled = (false);
      }
   }
}
