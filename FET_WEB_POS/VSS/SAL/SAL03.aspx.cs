using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class VSS_SAL_SAL03 : System.Web.UI.Page
{
   protected void Page_Load(object sender, EventArgs e)
   {
      if (!IsPostBack)
      {
         ViewState["MasterEditData"] = null;
         gvMasterEdit.Visible = false;

         Label1.Visible = false;
         //Button1.Visible = Button2.Visible = Button3.Visible = Button4.Visible = Button5.Visible = Button7.Visible = false;
         divpay.Visible = false;
         divCheckOut.Visible = false;

         //bindMasterData();
         //bindEmptySecondData();
         bindSecondData();
         gvMasterEdit.Visible = true;
         bindMasterEditData(1);
         divpay.Visible = true;



      }
   }
   protected void bindMasterData()
   {
      DataTable dtResult = new DataTable();
      dtResult = getMasterData();
      ViewState["gvMaster"] = dtResult;
      gvMaster.DataSource = dtResult;
      gvMaster.DataBind();
   }
   protected void bindMasterEditData(int count)
   {
      DataTable dtResult = new DataTable();
      dtResult = getMasterEditData(count);
      ViewState["gvMasterEdit"] = dtResult;
      gvMasterEdit.DataSource = dtResult;
      gvMasterEdit.DataBind();

   }
   protected void bindEmptySecondData()
   {
      gvSecond.Visible = true;
      DataTable dtResult = new DataTable();

      gvSecond.DataSource = dtResult;
      gvSecond.DataBind();
   }
   protected void bindSecondData()
   {
      DataTable dtResult = new DataTable();
      dtResult = getSecondData();
      ViewState["gvSecond"] = dtResult;
      gvSecond.DataSource = dtResult;
      gvSecond.DataBind();

   }
   private DataTable getMasterData()
   {
      DataTable dtResult = new DataTable();
      dtResult.Columns.Add("原交易日期", typeof(string));
      dtResult.Columns.Add("原交易序號", typeof(string));
      dtResult.Columns.Add("類別", typeof(string));
      dtResult.Columns.Add("商品編號", typeof(string));
      dtResult.Columns.Add("商品名稱", typeof(string));
      dtResult.Columns.Add("數量", typeof(string));
      dtResult.Columns.Add("單價", typeof(string));
      dtResult.Columns.Add("總價", typeof(string));
      dtResult.Columns.Add("IMEI", typeof(string));
      dtResult.Columns.Add("促銷名稱", typeof(string));

      for (int i = 0; i < 1; i++)
      {
         DataRow NewRow = dtResult.NewRow();
         NewRow["原交易日期"] = "2010/06/02";
         NewRow["原交易序號"] = "a223421";
         NewRow["類別"] = "類別1";
         NewRow["商品編號"] = "A";
         NewRow["商品名稱"] = "Aa";
         NewRow["數量"] = "1";
         NewRow["單價"] = "2000";
         NewRow["總價"] = "2000";
         NewRow["IMEI"] = "IMEI" + i;
         NewRow["促銷名稱"] = "";
         dtResult.Rows.Add(NewRow);
      }
      return dtResult;
   }
   private DataTable getMasterEditData(int count)
   {
      DataTable dtResult;
      if (ViewState["MasterEditData"] == null)
      {
         dtResult = new DataTable();

         dtResult.Columns.Add("類別", typeof(string));
         dtResult.Columns.Add("商品編號", typeof(string));
         dtResult.Columns.Add("商品名稱", typeof(string));
         dtResult.Columns.Add("數量", typeof(string));
         dtResult.Columns.Add("單價", typeof(string));
         dtResult.Columns.Add("總價", typeof(string));
         dtResult.Columns.Add("IMEI", typeof(string));
         dtResult.Columns.Add("促銷名稱", typeof(string));
         dtResult.Columns.Add("OriginalData", typeof(string));
         for (int i = 0; i < count; i++)
         {
            DataRow NewRow = dtResult.NewRow();

            NewRow["類別"] = "單";
            NewRow["商品編號"] = "A";
            NewRow["商品名稱"] = "A";
            NewRow["數量"] = "1";
            NewRow["單價"] = "3000";
            NewRow["總價"] = "3000";
            NewRow["IMEI"] = "IMEI" + i;
            NewRow["促銷名稱"] = "";
            NewRow["OriginalData"] = "1";
            dtResult.Rows.Add(NewRow);
         }
      }
      else
      {
         dtResult = (DataTable)ViewState["MasterEditData"];
         DataRow NewRow = dtResult.NewRow();

         NewRow["類別"] = "單";
         NewRow["商品編號"] = "A";
         NewRow["商品名稱"] = "A";
         NewRow["數量"] = "1";
         NewRow["單價"] = "1000";
         NewRow["總價"] = "1000";
         NewRow["IMEI"] = "IMEI";
         NewRow["促銷名稱"] = "";
         NewRow["OriginalData"] = "0";
         dtResult.Rows.Add(NewRow);
      }
      ViewState["MasterEditData"] = dtResult;
      return dtResult;
   }
   private DataTable getSecondData()
   {
      DataTable dtResult;
      if (ViewState["SecondEditData"] == null)
      {
         dtResult = new DataTable();
         dtResult.Columns.Add("付款方式", typeof(string));
         dtResult.Columns.Add("金額", typeof(string));
         dtResult.Columns.Add("付款明細", typeof(string));
         dtResult.Columns.Add("OriginalData", typeof(string));

         for (int i = 0; i < 1; i++)
         {
            DataRow NewRow = dtResult.NewRow();
            NewRow["付款方式"] = "現金";
            NewRow["金額"] = "3000";
            NewRow["付款明細"] = "";
            NewRow["OriginalData"] = "1";
            dtResult.Rows.Add(NewRow);
         }
      }
      else
      {
         dtResult = (DataTable)ViewState["SecondEditData"];

         DataRow NewRow = dtResult.NewRow();
         NewRow["付款方式"] = "現金";
         NewRow["金額"] = "2000";
         NewRow["付款明細"] = "";
         NewRow["OriginalData"] = "0";
         dtResult.Rows.Add(NewRow);

      }
      ViewState["SecondEditData"] = dtResult;
      return dtResult;
   }





   protected void gvMasterEdit_RowEditing(object sender, GridViewEditEventArgs e)
   {
      GridView gridview = sender as GridView;
      //設定編輯欄位
      gridview.EditIndex = e.NewEditIndex;
      //Bind原查詢資料
      DataTable dt = ViewState["gvMasterEdit"] as DataTable;
      gridview.DataSource = dt;
      gridview.DataBind();
   }
   protected void gvMasterEdit_RowUpdating(object sender, GridViewUpdateEventArgs e)
   {
      GridView gridview = sender as GridView;

      //取得資料

      //更新資料庫

      //取消編輯狀態
      gridview.EditIndex = -1;

      //Bind新資料(重取資料)
      bindMasterEditData(1);
   }
   protected void gvMasterEdit_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
   {
      GridView gridview = sender as GridView;
      gridview.EditIndex = -1;
      //Bind原查詢資料
      DataTable dt = ViewState["gvMasterEdit"] as DataTable;
      gridview.DataSource = dt;
      gridview.DataBind();
   }
   protected void btnSearch_Click(object sender, EventArgs e)
   {
      gvMasterEdit.Visible = true;
      bindMasterEditData(1);
      Label6.Text = "1000";
      Label8.Text = "-2000";
      divpay.Visible = true;
   }
   protected void Button1_Click(object sender, EventArgs e)
   {
      gvSecond.Visible = true;
      bindSecondData();
      divCheckOut.Visible = true;
      Label9.Text = "-1000";
      Label11.Text = "0";

   }
   protected void Button8_Click(object sender, EventArgs e)
   {
      Label1.Visible = true;
      Label1.Text = "1234-01-100912345";
      Label3.Text = "50-已結帳";
      lbInvoiceNo.Text = "PS24541254";
   }


   protected void gvMasterEdit_RowDataBound(object sender, GridViewRowEventArgs e)
   {
      DataRowView view = e.Row.DataItem as DataRowView;
      if (view != null)
      {

         if (e.Row.RowType == DataControlRowType.DataRow)
         {
            if (e.Row.RowIndex != -1)
            {
               for (int i = 0; i < e.Row.Cells.Count - 1; i++)
               {
                  e.Row.Cells[i].ForeColor = System.Drawing.Color.White;
               }
               if (view.Row["OriginalData"].ToString() == "1")
               {

                  e.Row.Cells[6].ForeColor = System.Drawing.Color.Red;
                  e.Row.BackColor = System.Drawing.Color.DarkGray;
                  if (e.Row.FindControl("linkEdit") != null)
                  {
                     LinkButton linkEdit = (LinkButton)(e.Row.FindControl("linkEdit"));
                     linkEdit.Visible = false;
                  }
                  if (e.Row.FindControl("cbIMEI") != null)
                  {
                     CheckBox cbIMEI = (CheckBox)(e.Row.FindControl("cbIMEI"));
                     cbIMEI.Visible = false;
                  }
               }
               else
               {
                  for (int i = 0; i < e.Row.Cells.Count - 1; i++)
                  {
                     e.Row.Cells[i].ForeColor = System.Drawing.Color.Black;
                  }
                  if (e.Row.FindControl("linkEdit") != null)
                  {
                     LinkButton linkEdit = (LinkButton)(e.Row.FindControl("linkEdit"));
                     linkEdit.Visible = true;
                  }
                  if (e.Row.FindControl("cbIMEI") != null)
                  {
                     CheckBox cbIMEI = (CheckBox)(e.Row.FindControl("cbIMEI"));
                     cbIMEI.Visible = true;
                     cbIMEI.Attributes["onclick"] = "imeicheckbox(this);";
                  }
               }
            }
         }

      }
   }
   protected void gvSecond_RowDataBound(object sender, GridViewRowEventArgs e)
   {
      DataRowView view = e.Row.DataItem as DataRowView;
      if (view != null)
      {

         if (e.Row.RowType == DataControlRowType.DataRow)
         {
            if (e.Row.RowIndex != -1)
            {
               for (int i = 0; i < e.Row.Cells.Count; i++)
               {
                  e.Row.Cells[i].ForeColor = System.Drawing.Color.White;
               }
               if (view.Row["OriginalData"].ToString() == "1")
               {


                  e.Row.BackColor = System.Drawing.Color.DarkGray;

                  if (e.Row.FindControl("CheckItem") != null)
                  {
                     CheckBox CheckItem = (CheckBox)(e.Row.FindControl("CheckItem"));
                     CheckItem.Visible = false;
                  }
               }
               else
               {
                  for (int i = 0; i < e.Row.Cells.Count; i++)
                  {
                     e.Row.Cells[i].ForeColor = System.Drawing.Color.Black;
                  }

                  if (e.Row.FindControl("CheckItem") != null)
                  {
                     CheckBox CheckItem = (CheckBox)(e.Row.FindControl("CheckItem"));
                     CheckItem.Visible = true;
                     //CheckItem.Attributes["onclick"] = "imeicheckbox(this);";
                  }
               }
            }
         }

      }
   }
   protected void bindGvDetailData()
   {
      DataTable dtResult = new DataTable();
      dtResult = getGvDetailData();
      gvDetail.DataSource = dtResult;
      gvDetail.DataBind();
   }

   private DataTable getGvDetailData()
   {
      DataTable dtResult = new DataTable();
      dtResult.Columns.Add("項次", typeof(string));
      dtResult.Columns.Add("折扣料號", typeof(string));
      dtResult.Columns.Add("折扣名稱", typeof(string));
      dtResult.Columns.Add("數量", typeof(string));
      dtResult.Columns.Add("單價", typeof(string));
      dtResult.Columns.Add("總價", typeof(string));
      DataRow NewRow = dtResult.NewRow();
      NewRow["項次"] = "1";
      NewRow["折扣料號"] = "折扣料號1";
      NewRow["折扣名稱"] = "折扣名稱1";
      NewRow["數量"] = "1";
      NewRow["單價"] = "1000";
      NewRow["總價"] = "1000";
      dtResult.Rows.Add(NewRow);

      NewRow = dtResult.NewRow();
      NewRow["項次"] = "2";
      NewRow["折扣料號"] = "折扣料號2";
      NewRow["折扣名稱"] = "折扣名稱2";
      NewRow["數量"] = "2";
      NewRow["單價"] = "200";
      NewRow["總價"] = "200";
      dtResult.Rows.Add(NewRow);

      return dtResult;
   }
   protected void btnConfirm_Click(object sender, EventArgs e)
   {
      bindGvDetailData();
      Label8.Visible = true;
      btnConfirm.Enabled = false;
   }
}
