using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using DevExpress.Web.ASPxGridView;
using DevExpress.Web.ASPxEditors;

public partial class VSS_INV09_INV09 : System.Web.UI.Page
{
   protected void Page_Load(object sender, EventArgs e)
   {
      if (!IsPostBack && !IsCallback)
      {
         bindMasterData();
         btnSave.Enabled = false;
         btnClear.Enabled = false;

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
   private DataTable getMasterData()
   {
      DataTable dtResult = new DataTable();
      dtResult.Columns.Add("商品編號", typeof(string));
      dtResult.Columns.Add("商品名稱", typeof(string));
      dtResult.Columns.Add("IMEI檢核", typeof(bool));
      dtResult.Columns.Add("到貨量", typeof(string));
      dtResult.Columns.Add("驗收量", typeof(string));
      dtResult.Columns.Add("IMEI", typeof(string));
      dtResult.Columns.Add("在途量", typeof(string));
      dtResult.Columns.Add("供貨商", typeof(string));


      for (int i = 1; i < 6; i++)
      {

         DataRow NewRow = dtResult.NewRow();
         NewRow["商品編號"] = "A021000" + i;
         NewRow["商品名稱"] = "商品名稱" + i;
         NewRow["IMEI檢核"] = i % 2;
         NewRow["到貨量"] = "10";
         NewRow["驗收量"] = "5";
         NewRow["IMEI"] = "5";
         NewRow["在途量"] = "5";
         NewRow["供貨商"] = "供貨商" + i;
         dtResult.Rows.Add(NewRow);

      }
      return dtResult;
   }

   protected void btnSave_Click(object sender, EventArgs e)
   {

   }

   protected void gvMaster_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
   {
      //GridPageSize = int.Parse(e.Parameters);
      gvMaster.SettingsPager.PageSize = int.Parse(e.Parameters);
      gvMaster.DataBind();
   }

   protected void gvMaster_HtmlRowCreated(object sender, ASPxGridViewTableRowEventArgs e)
   {
      if (e.RowType == GridViewRowType.Data)
      {
         DataRowView Rows = (DataRowView)gvMaster.GetRow(e.VisibleIndex);
         bool check = bool.Parse(Rows["IMEI檢核"].ToString());
         ASPxLabel lbl1 = (ASPxLabel)gvMaster.FindRowCellTemplateControl(e.VisibleIndex, (GridViewDataColumn)gvMaster.Columns[5], "lbl1");
         ASPxButton btn1 = (ASPxButton)gvMaster.FindRowCellTemplateControl(e.VisibleIndex, (GridViewDataColumn)gvMaster.Columns[5], "Button2");
         ASPxTextBox txt1 = (ASPxTextBox)gvMaster.FindRowCellTemplateControl(e.VisibleIndex, (GridViewDataColumn)gvMaster.Columns[4], "txt1");

         if (check == false)
         {
            txt1.Visible = false;
            lbl1.Visible = false;
            btn1.Visible = false;

         }
      }

   }

   /// <summary>
   /// 主GridView的分頁變更事件
   /// </summary>
   /// <param name="sender"></param>
   /// <param name="e"></param>
   protected void gvMaster_PageIndexChanged(object sender, EventArgs e)
   {
      ASPxGridView gvMaster = sender as ASPxGridView;
      gvMaster.DataSource = getMasterData();
      gvMaster.DataBind();
   }


   protected void gvMaster_CustomColumnDisplayText(object sender, ASPxGridViewColumnDisplayTextEventArgs e)
   {
      if (e.VisibleRowIndex > -1)
      {
         DataRowView Rows = (DataRowView)gvMaster.GetRow(e.VisibleRowIndex);
         bool check = bool.Parse(Rows["IMEI檢核"].ToString());

         if (check == false)
         {
            if (e.Column.FieldName == "在途量")
            {
               e.DisplayText = "10";
            }
         }
      }
   }
}
