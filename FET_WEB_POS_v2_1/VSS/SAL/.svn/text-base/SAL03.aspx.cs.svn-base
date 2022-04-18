using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using DevExpress.Web.ASPxGridView;
using DevExpress.Web.ASPxEditors;

public partial class VSS_SAL_SAL03 : BasePage
{
   protected void Page_Load(object sender, EventArgs e)
   {
      if (!IsPostBack && !Page.IsCallback)
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
         //bindMasterEditData(1);
         divpay.Visible = true;
         
      }

      //ProductExchangePopup.ShowOnPageLoad = false;
   }
   //protected void bindMasterData()
   //{
   //   DataTable dtResult = new DataTable();
   //   dtResult = getMasterData();
   //   ViewState["gvMaster"] = dtResult;
   //   gvMaster.DataSource = dtResult;
   //   gvMaster.DataBind();
   //}
   protected void bindMasterEditData()
   {
      DataTable dtResult = new DataTable();
      dtResult = getMasterEditData();
      ViewState["gvMasterEdit"] = dtResult;
      gvMasterEdit.DataSource = dtResult;
      gvMasterEdit.DataBind();

   }

   protected void bindSecondData()
   {
      DataTable dtResult = new DataTable();
      dtResult = getSecondData();
      ViewState["gvSecond"] = dtResult;
      gvSecond.DataSource = dtResult;
      gvSecond.DataBind();

   }

   private DataTable getMasterEditData()
   {
      DataTable dtResult;

      dtResult = new DataTable();
      dtResult.Columns.Add("status", typeof(string));
      dtResult.Columns.Add("項次", typeof(string));
      dtResult.Columns.Add("類別", typeof(string));
      dtResult.Columns.Add("商品編號", typeof(string));
      dtResult.Columns.Add("商品名稱", typeof(string));
      dtResult.Columns.Add("數量", typeof(string));
      dtResult.Columns.Add("單價", typeof(string));
      dtResult.Columns.Add("總價", typeof(string));
      dtResult.Columns.Add("CHECK", typeof(bool));
      dtResult.Columns.Add("IMEI", typeof(string));
      dtResult.Columns.Add("促銷名稱", typeof(string));
      dtResult.Columns.Add("OriginalData", typeof(string));


      DataRow NewRow = dtResult.NewRow();

      NewRow = dtResult.NewRow();
      NewRow["status"] = "作廢";
      NewRow["項次"] = "1";
      NewRow["類別"] = "促";
      NewRow["商品編號"] = "123456789";
      NewRow["商品名稱"] = "B";
      NewRow["數量"] = "4";
      NewRow["單價"] = "3000";
      NewRow["總價"] = "3000";
      NewRow["CHECK"] = false;
      NewRow["IMEI"] = "1";
      NewRow["促銷名稱"] = "AAA";
      NewRow["OriginalData"] = "1";
      dtResult.Rows.Add(NewRow);



      NewRow = dtResult.NewRow();
      NewRow["status"] = "作廢";
      NewRow["項次"] = "2";
      NewRow["類別"] = "促";
      NewRow["商品編號"] = "123456790";
      NewRow["商品名稱"] = "A";
      NewRow["數量"] = "5";
      NewRow["單價"] = "3000";
      NewRow["總價"] = "3000";
      NewRow["CHECK"] = true;
      NewRow["IMEI"] = "5";
      NewRow["促銷名稱"] = "AAA";
      NewRow["OriginalData"] = "1";
      dtResult.Rows.Add(NewRow);

      NewRow = dtResult.NewRow();
      NewRow["status"] = "";
      NewRow["項次"] = "3";
      NewRow["類別"] = "促";
      NewRow["商品編號"] = "123456789";
      NewRow["商品名稱"] = "B";
      NewRow["數量"] = "4";
      NewRow["單價"] = "3000";
      NewRow["總價"] = "3000";
      NewRow["CHECK"] = false;
      NewRow["IMEI"] = "1";
      NewRow["促銷名稱"] = "AAA";
      NewRow["OriginalData"] = "1";
      dtResult.Rows.Add(NewRow);

      NewRow = dtResult.NewRow();
      NewRow["status"] = "";
      NewRow["項次"] = "4";
      NewRow["類別"] = "促";
      NewRow["商品編號"] = "123456791";
      NewRow["商品名稱"] = "C";
      NewRow["數量"] = "1";
      NewRow["單價"] = "3000";
      NewRow["總價"] = "3000";
      NewRow["CHECK"] = true;
      NewRow["IMEI"] = "0";
      NewRow["促銷名稱"] = "AAA";
      NewRow["OriginalData"] = "1";
      dtResult.Rows.Add(NewRow);
      ViewState["MasterEditData"] = dtResult;
      return dtResult;
   }

   private DataTable getSecondData()
   {
      DataTable dtResult;
      if (ViewState["SecondEditData"] == null)
      {
         dtResult = new DataTable();
         dtResult.Columns.Add("status", typeof(string));
         dtResult.Columns.Add("付款方式", typeof(string));
         dtResult.Columns.Add("金額", typeof(string));
         dtResult.Columns.Add("付款明細", typeof(string));
         dtResult.Columns.Add("OriginalData", typeof(string));

         for (int i = 0; i < 1; i++)
         {
            DataRow NewRow = dtResult.NewRow();
            NewRow["status"] = "已退現";
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
         NewRow["status"] = "已退現";
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
      bindMasterEditData();
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
      bindMasterEditData();
      //gvMasterEdit.AddNewRow();
      //Label6.Text = "1000";
      //Label8.Text = "-2000";
      Label7.Text = "6000";
      Label6.Text = Label8.Text = "0";
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

      ProductExchangePopup.Enabled = true;
      ProductExchangePopup.ShowOnPageLoad = true;
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
      gvDetail.Visible = true;
      bindGvDetailData();
      Label8.Visible = true;
      btnConfirm.Enabled = false;
   }
   protected void gvMasterEdit_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
   {
      gvMasterEdit.CancelEdit();
      e.Cancel = true;
      bindMasterEditData();

   }

   protected void gvMasterEdit_HtmlRowPrepared(object sender, ASPxGridViewTableRowEventArgs e)
   {

      if (e.RowType != DevExpress.Web.ASPxGridView.GridViewRowType.Data) return;

      ASPxLabel lblIMEI = (ASPxLabel)gvMasterEdit.FindRowCellTemplateControl(e.VisibleIndex, (GridViewDataColumn)gvMasterEdit.Columns[10], "lbl1");
      // 繫結明細資料表           
      lblIMEI.Attributes["onmouseover"] = string.Format("show('{0}');", IMEIContent(Convert.ToInt16(lblIMEI.Text)));
      lblIMEI.Attributes["onmouseout"] = "hide();";

      //img result
      //bool IMEI = (bool)gvMaster.GetRowValues(e.VisibleIndex, "CHECK");
      int intC_IMEI = int.Parse(gvMasterEdit.GetRowValues(e.VisibleIndex, "數量").ToString());
      int intS_IMEI = int.Parse(gvMasterEdit.GetRowValues(e.VisibleIndex, "IMEI").ToString());
      ASPxImage imgIMEI = (ASPxImage)gvMasterEdit.FindRowCellTemplateControl(e.VisibleIndex, (GridViewDataColumn)gvMasterEdit.Columns[9], "imgIMEI");

      //img status
      //intC_IMEI - intS_IMEI = 0
      if ((intC_IMEI - intS_IMEI) == 0)
      {
         imgIMEI.ImageUrl = "~/Icon/check.png";
      }
      //intC_IMEI - intS_IMEI > 1
      if ((intC_IMEI - intS_IMEI) > 0)
      {
         imgIMEI.ImageUrl = "~/Icon/non_complete.png";
      }

      //button status
      //ASPxTextBox txtProdNo = e.Row.FindChildControl<ASPxTextBox>("txtProdNo"); // (ASPxTextBox)gvMasterEdit.FindRowCellTemplateControl(e.VisibleIndex, (GridViewDataColumn)gvMasterEdit.Columns[4], "txtProdNo");
      //ASPxButton btnProdNo = (ASPxButton)gvMasterEdit.FindRowCellTemplateControl(e.VisibleIndex, (GridViewDataColumn)gvMasterEdit.Columns[4], "btnProdNo");
      PopupControl pControl1 = e.Row.FindChildControl<PopupControl>("PopupControl1");

      //ASPxTextBox txtIMEI = (ASPxTextBox)gvMasterEdit.FindRowCellTemplateControl(e.VisibleIndex, (GridViewDataColumn)gvMasterEdit.Columns[10], "ASPxTextBox3");
     // ASPxButton btnIMEI = (ASPxButton)gvMasterEdit.FindRowCellTemplateControl(e.VisibleIndex, (GridViewDataColumn)gvMasterEdit.Columns[10], "Button2");
      PopupControl pControl3 = e.Row.FindChildControl<PopupControl>("PopupControl3");

      if ((intC_IMEI == 1) && (intC_IMEI - intS_IMEI == 1))
      {
         //btnIMEI.Enabled = false;
          if (pControl3 != null)
          {
              //pControl3.FindChildControl<ASPxButton>("btnControl").Enabled = false;
              pControl3.Enabled = false;
          }
      }

      string strStatus = (string)gvMasterEdit.GetRowValues(e.VisibleIndex, "status");

      if (strStatus == "作廢")
      {
         if (e.Row.Cells.Count >= gvMasterEdit.Columns.Count)
         {
            e.Row.Cells[1].Style.Add(HtmlTextWriterStyle.Color, "red");
            e.Row.BackColor = System.Drawing.Color.Gray;
            e.Row.Cells[0].Enabled = false;
            //txtProdNo.Enabled = false;
            //btnProdNo.Enabled = false;
            pControl1.Enabled = false;

            //txtIMEI.Enabled = false;
            //btnIMEI.Enabled = false;
            pControl3.Enabled = false;
         }
      }

   }
   private string IMEIContent(int Count)
   {
      string IMEI_FORMAT = "<table border=\"1\">";
      for (int i = 1; i <= Count; i++)
      {
         IMEI_FORMAT += "<tr><td>778194415641786" + i.ToString() + "</td></tr>";
      }
      IMEI_FORMAT += "</tr></table>";

      return IMEI_FORMAT;
   }
   protected void gvSecond_HtmlRowPrepared(object sender, ASPxGridViewTableRowEventArgs e)
   {
      if (e.RowType != DevExpress.Web.ASPxGridView.GridViewRowType.Data) return;
      e.Row.BackColor = System.Drawing.Color.Gray;
   }
}
