using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class VSS_INV07_INV07 : System.Web.UI.Page
{
   protected void Page_Load(object sender, EventArgs e)
   {
      if (!IsPostBack)
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
   private DataTable getMasterData()
   {

      DataTable dtResult = new DataTable();
      dtResult.Columns.Add("項次", typeof(string));
      dtResult.Columns.Add("商品編號", typeof(string));
      dtResult.Columns.Add("商品名稱", typeof(string));
      dtResult.Columns.Add("IMEI控管", typeof(string));
      dtResult.Columns.Add("帳上庫存量", typeof(string));
      dtResult.Columns.Add("未拆封數量", typeof(string));
      dtResult.Columns.Add("已拆封數量", typeof(string));
      dtResult.Columns.Add("退倉數量", typeof(string));
      dtResult.Columns.Add("IMEI", typeof(string));
      dtResult.Columns.Add("差異量", typeof(string));
      dtResult.Columns.Add("ERP驗退日期", typeof(string));
      dtResult.Columns.Add("ERP驗退單號", typeof(string));
      dtResult.Columns.Add("驗退數量", typeof(string));

      for (int i = 1; i <= 6; i++)
      {
          DataRow NewRow = dtResult.NewRow();
         if (i == 3)
         {
            
            NewRow["項次"] = i;
            NewRow["商品編號"] = "100100100" + i.ToString("000");
            NewRow["商品名稱"] = i.ToString("00") + "手機";
            NewRow["IMEI控管"] = "1";
            NewRow["帳上庫存量"] = 2 * i + i;
            NewRow["未拆封數量"] = i;
            NewRow["已拆封數量"] = i;  
            //NewRow["退倉數量"] = Convert.ToInt32(NewRow["未拆封數量"]) + Convert.ToInt32(NewRow["已拆封數量"]);
            NewRow["IMEI"] = "3";
            //NewRow["差異量"] = Convert.ToInt32(NewRow["帳上庫存量"]) - Convert.ToInt32(NewRow["退倉數量"]); ;
            NewRow["ERP驗退日期"] = "";
            NewRow["ERP驗退單號"] = "";
            NewRow["驗退數量"] = "";
            
         }
         else
         {
            //DataRow NewRow = dtResult.NewRow();
            NewRow["項次"] = i;
            NewRow["商品編號"] = "100100100" + i.ToString("000");
            NewRow["商品名稱"] = i.ToString("00") + "手機";
             NewRow["IMEI控管"] = (i==3?"1":"0");
            NewRow["帳上庫存量"] = 2 * i + i; 
            NewRow["未拆封數量"] = i;  
            NewRow["已拆封數量"] = i; 
            //NewRow["退倉數量"] = Convert.ToInt32(NewRow["未拆封數量"]) + Convert.ToInt32(NewRow["已拆封數量"]);
            NewRow["IMEI"] = "0";
            //NewRow["差異量"] = "0" ;

            NewRow["ERP驗退日期"] = "";
                //"2010/08/01";
            NewRow["ERP驗退單號"] = "";
                //"ERP000" + i.ToString("000");
            NewRow["驗退數量"] = "";
                //2 * i + i;
            //dtResult.Rows.Add(NewRow);
         }
         NewRow["退倉數量"] = Convert.ToInt32(NewRow["未拆封數量"]) + Convert.ToInt32(NewRow["已拆封數量"]);
         NewRow["差異量"] = Convert.ToInt32(NewRow["帳上庫存量"]) - Convert.ToInt32(NewRow["退倉數量"]); 

         dtResult.Rows.Add(NewRow);

      }
      return dtResult;
   }
   protected void btnSave_Click(object sender, EventArgs e)
   {
      lbOrderNo.Text = "GA000001";
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
   protected void gvMaster_RowDataBound(object sender, GridViewRowEventArgs e)
   {
       string IMEI_FORMAT = "<table border=\"1\"><tr><td>7781-9441-5641-7861</td></tr><tr><td>7783-9443-5643-7862</td></tr><tr><td>7783-9443-5643-7863</td></tr></table>";
      if (e.Row.RowType == DataControlRowType.DataRow)
      {
         CheckBox cbIMEI = (CheckBox)e.Row.Cells[3].FindControl("CheckBox1");
         if (cbIMEI.Checked == false)
         {
            //e.Row.Cells[7].Text = "";
             Button btnIMEI = (Button)e.Row.Cells[7].FindControl("Button2");
             btnIMEI.Attributes["onclick"] = "imeiclick(this);";
             btnIMEI.Enabled = false;
         }
         else
         {
             //滑過row出現的table視窗
          e.Row.Cells[8].Attributes["onmouseover"] = string.Format("show('{0}');", string.Format(IMEI_FORMAT, e.Row.Cells[8].Text));
          e.Row.Cells[8].Attributes["onmouseout"] = "hide();";

             Button btnIMEI = (Button)e.Row.Cells[7].FindControl("Button2");
            btnIMEI.Attributes["onclick"] = "imeiclick(this);";
            btnIMEI.Enabled = true;
         }
      }
   }
}
