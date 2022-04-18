using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using DevExpress.Web.ASPxGridView;
using System.Web.UI.HtmlControls;
using DevExpress.Web.ASPxEditors;

public partial class VSS_INV_INV25 : BasePage
{
   protected void Page_Load(object sender, EventArgs e)
   {

       if (!IsPostBack)
       {
           bindMasterData(1);
           btnExport.Enabled = false;
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
      dtMaster.Columns.Add("移出數量", typeof(int));
      dtMaster.Columns.Add("IMEI控管", typeof(bool));
      dtMaster.Columns.Add("IMEI", typeof(string));

      for (int i = 0; i < TempCount; i++)
      {
         DataRow dtMasterRow = dtMaster.NewRow();
         dtMasterRow["商品料號"] = "A0001";
         dtMasterRow["商品名稱"] = "測試1";
         dtMasterRow["移出數量"] = i++;
         dtMasterRow["IMEI控管"] = ((i % 2) == 0);
         dtMasterRow["IMEI"] = "0119300067" + i.ToString("0639");

         dtMaster.Rows.Add(dtMasterRow);
      }

      return dtMaster;

   }

   protected void btnAddNew_Click(object sender, EventArgs e)
   {
       //gvMaster.ShowFooter = true;
       //gvMaster.ShowFooterWhenEmpty = true;
       System.Web.UI.HtmlControls.HtmlTableRow tr = gvMaster.FindChildControl<System.Web.UI.HtmlControls.HtmlTableRow>("trEmptyData");
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
       btnExport.Enabled = true;

   }

   protected void gvMaster_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
   {
       ASPxGridView grid = (ASPxGridView)sender;
       grid.CancelEdit();
       e.Cancel = true;
       bindMasterData(1);
   }
   protected void gvMaster_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
   {
       ASPxGridView grid = sender as ASPxGridView;
       e.Cancel = true;
       grid.CancelEdit();
       bindMasterData(1);
       
   }

   protected void gvMaster_CommandButtonInitialize(object sender, ASPxGridViewCommandButtonEventArgs e)
   {
       if (e.ButtonType == ColumnCommandButtonType.Update || e.ButtonType == ColumnCommandButtonType.Cancel)
       {
           e.Visible = false;
       }
   }

   protected void gvMaster_HtmlRowCreated(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewTableRowEventArgs e)
   {
       if (e.RowType == GridViewRowType.Data)
       {
           //img result
           bool IMEI = (bool)gvMaster.GetRowValues(e.VisibleIndex, "IMEI控管");
           ASPxImage imgIMEI = (ASPxImage)gvMaster.FindRowCellTemplateControl(e.VisibleIndex, (GridViewDataColumn)gvMaster.Columns[4], "imgIMEI");
           imgIMEI.ImageUrl = (IMEI == true ? "~/Icon/check.png" : "~/Icon/non_complete.png");

           ////imei count result
           //int intIMEI = 1;//int.Parse(gvMaster.GetRowValues(e.VisibleIndex, "IMEI").ToString());
           //ASPxLabel lblIMEI = (ASPxLabel)gvMaster.FindRowCellTemplateControl(e.VisibleIndex, (GridViewDataColumn)gvMaster.Columns[6], "countLabel"); //(ASPxLabel)gvMaster.FindEditFormTemplateControl("countLabel");
           ////ASPxTextBox txtIMEI = (ASPxTextBox)gvMaster.FindRowCellTemplateControl(e.VisibleIndex, (GridViewDataColumn)gvMaster.Columns[6], "ASPxTextBox3");
           //ASPxButton btnIMEI = (ASPxButton)gvMaster.FindRowCellTemplateControl(e.VisibleIndex, (GridViewDataColumn)gvMaster.Columns[6], "popupButton");

           //lblIMEI.Visible = (intIMEI > 1);
           ////txtIMEI.Visible = !lblIMEI.Visible;

           //btnIMEI.Enabled = (intIMEI > 1);

          

       }
   }


   private string IMEIContent(int Count)
   {
       string IMEI_FORMAT = "<table border=\"1\">";
       for (int i = 1; i <= Count; i++)
       {
           IMEI_FORMAT += "<tr><td>7781-9441-5641-786" + i.ToString() + "</td></tr>";
       }
       IMEI_FORMAT += "</tr></table>";

       return IMEI_FORMAT;
   }
    
   protected void gvMaster_InitNewRow(object sender, DevExpress.Web.Data.ASPxDataInitNewRowEventArgs e)
   {
       ASPxLabel lblIMEI = gvMaster.FindEditRowCellTemplateControl((GridViewDataColumn)gvMaster.Columns[5], "countLabel") as ASPxLabel;
       ASPxButton btnIMEI = gvMaster.FindEditRowCellTemplateControl((GridViewDataColumn)gvMaster.Columns[5], "popupButton") as ASPxButton;

       if (lblIMEI != null)
       {
           //繫結明細資料表           
           lblIMEI.Attributes["onmouseover"] = string.Format("show('{0}');", IMEIContent(Convert.ToInt16(lblIMEI.Text)));
           lblIMEI.Attributes["onmouseout"] = "hide();";

           btnIMEI.Enabled = (Convert.ToInt16(lblIMEI.Text) > 0 ) ? true : false;
       }
   }
}   
