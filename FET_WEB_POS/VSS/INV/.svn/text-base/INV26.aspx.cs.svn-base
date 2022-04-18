using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class VSS_INV26_INV26 : Advtek.Utility.BasePage
{
   protected void Page_Load(object sender, EventArgs e)
   {
      if (!Page.IsPostBack)
      {
         // 繫結空的資料表，以顯示表頭欄位
         //gvMaster.DataSource = GetEmptyDataTable();
         //gvMaster.DataBind();
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
      dtResult.Columns.Add("移撥單號", typeof(string));
      dtResult.Columns.Add("移出門市", typeof(string));
      dtResult.Columns.Add("移出日期", typeof(string));
      dtResult.Columns.Add("撥入日期", typeof(string));
      dtResult.Columns.Add("移撥狀態", typeof(string));
      dtResult.Columns.Add("更新人員", typeof(string));
      dtResult.Columns.Add("更新日期", typeof(string));
      return dtResult;

   }
   private DataTable getMasterData()
   {
      DataTable dtResult = GetEmptyDataTable();

      for (int i = 1; i < 16; i++)
      {
         DataRow NewRow = dtResult.NewRow();
         NewRow["移撥單號"] = "ST2101-100815" + i.ToString("000");
         NewRow["移出門市"] = "移出門市" + i.ToString("000");
         NewRow["移出日期"] = "2010/08/15";
         NewRow["撥入日期"] = "";
         NewRow["移撥狀態"] = "在途";
         NewRow["更新人員"] = "王小明";
         NewRow["更新日期"] = "2010/07/13";
         dtResult.Rows.Add(NewRow);
      }
      return dtResult;

   }
   protected void bindDetailData()
   {
      DataTable dtgvDetail = new DataTable();
      dtgvDetail = getDetailData();
      ViewState["gvDetail"] = dtgvDetail;
      gvDetail.DataSource = dtgvDetail;
      gvDetail.DataBind();
   }
   private DataTable getDetailData()
   {
      DataTable gvDetail = new DataTable();
      gvDetail.Columns.Clear();
      gvDetail.Columns.Add("商品料號", typeof(string));
      gvDetail.Columns.Add("商品名稱", typeof(string));
      gvDetail.Columns.Add("移出數量", typeof(string));
      gvDetail.Columns.Add("撥入數量", typeof(string));
      gvDetail.Columns.Add("IMEI控管", typeof(string));
      gvDetail.Columns.Add("IMEI", typeof(string));
      for (int i = 1; i < 7; i++)
      {
         DataRow gvDetailRow = gvDetail.NewRow();
         gvDetailRow["商品料號"] = "ST2101-100815001";
         gvDetailRow["商品名稱"] = "商品名稱" + i.ToString("000");
         gvDetailRow["移出數量"] = i;
         gvDetailRow["撥入數量"] = i;
         gvDetailRow["IMEI控管"] = ((i % 2) == 0) ? "0" : "1";
         gvDetailRow["IMEI"] = ((i % 2) == 0) ? "0" : "5";
         gvDetail.Rows.Add(gvDetailRow);
      }
      return gvDetail;
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

   protected void gvMaster_RowCommand(object sender, GridViewCommandEventArgs e)
   {
      if (e.CommandName == "select")
      {
         bindDetailData();
         DIVdetail.Visible = true;
         this.gvDetail.Visible = true;
      }
      else if (e.CommandName == "View")
      {
         Page.Response.Redirect("CON06.aspx");
      }
   }

   //protected void gvMaster_RowDataBound(object sender, GridViewRowEventArgs e)
   //{
   //    if (e.Row.RowIndex != -1)
   //    {
   //        if (e.Row.RowType == DataControlRowType.DataRow)
   //        {
   //            if (e.Row.Cells[5].Text == "已存檔")
   //            {
   //                Button fix = (Button)e.Row.Cells[0].FindControl("btnfix");
   //                fix.Visible = true;
   //                fix.Enabled = true;
   //                Button btnView = (Button)e.Row.Cells[0].FindControl("btnView");
   //                btnView.Enabled = true;
   //                btnView.Visible = false;

   //            }
   //        }
   //    }
   //}
   protected void gvMaster_RowDataBound(object sender, GridViewRowEventArgs e)
   {
      if (e.Row.RowIndex != -1)
      {
         if (e.Row.RowType == DataControlRowType.DataRow)
         {
            if (e.Row.Cells[4].Text == "在途")
            {
               for (int i = 1; i < e.Row.Cells.Count; i++)
               {
                  e.Row.Cells[i].ForeColor = System.Drawing.Color.Red;
               }

            }
         }
      }
   }

   protected void gvDetail_RowDataBound(object sender, GridViewRowEventArgs e)
   {

      string IMEI_FORMAT = "<table border=\"1\"><tr><td>7781-9441-5641-7861</td></tr><tr><td>7783-9443-5643-7862</td></tr><tr><td>7783-9443-5643-7863</td></tr><tr><td>7783-9443-5643-7864</td><tr><td>7783-9443-5643-7865</td></tr></tr></table>";

      if (e.Row.RowIndex != -1)
      {
         if (e.Row.RowType == DataControlRowType.DataRow)
         {

            e.Row.Cells[5].Attributes["onmouseover"] = string.Format("show('{0}');", string.Format(IMEI_FORMAT, e.Row.Cells[5].Text));
            e.Row.Cells[5].Attributes["onmouseout"] = "hide();";

            HiddenField hfIMEI = (HiddenField)e.Row.Cells[4].FindControl("hidIMEI");
            CheckBox cbIMEI = (CheckBox)e.Row.Cells[4].FindControl("CheckBox3");
            Button btnIMEI = (Button)e.Row.Cells[5].FindControl("Button10");
            cbIMEI.Checked = (hfIMEI.Value == "1") ? (true) : (false);
            btnIMEI.Enabled = cbIMEI.Checked == true ? true : false;
         }
      }
   }
}
