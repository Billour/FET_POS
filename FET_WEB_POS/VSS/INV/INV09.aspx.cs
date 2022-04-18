using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class VSS_INV09_INV09 : System.Web.UI.Page
{
   protected void Page_Load(object sender, EventArgs e)
   {
      if (!IsPostBack)
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
        dtResult.Columns.Add("IMEI檢核", typeof(string));
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
   protected void gvMaster_RowDataBound(object sender, GridViewRowEventArgs e)
   {
      string FORMAT = "<table border=\"1\"><tr><td>供貨商名稱：</td><td>{0}</td></tr><tr><td>聯絡窗口：</td><td>窗口</td></tr><tr><td>聯絡電話：</td><td>02-11112222</td></tr><tr><td>傳真號碼：</td><td>02-44441452</td></tr><tr><td>email：</td><td>xxx@aaa.com</td></tr></table>";
      string IMEI_FORMAT = "<table border=\"1\"><tr><td>7781-9441-5641-7861</td></tr><tr><td>7783-9443-5643-7862</td></tr><tr><td>7783-9443-5643-7863</td></tr><tr><td>7783-9443-5643-7864</td><tr><td>7783-9443-5643-7865</td></tr></tr></table>";
      if (e.Row.RowType == DataControlRowType.DataRow)
      {
         e.Row.Cells[7].Attributes["onmouseover"] = string.Format("show('{0}');", string.Format(FORMAT, e.Row.Cells[7].Text));
         e.Row.Cells[7].Attributes["onmouseout"] = "hide();";       
         e.Row.Cells[4].Enabled = false;
         
         CheckBox cbIMEI = (CheckBox)e.Row.Cells[3].FindControl("CheckBox1");
         if (cbIMEI.Checked == false)
         {
             e.Row.Cells[6].Enabled = false;
             e.Row.Cells[4].Text = "";
             e.Row.Cells[5].Text = "";
             
             e.Row.Cells[6].Text = "10";
         }
         else
         {
            e.Row.Cells[5].Attributes["onmouseover"] = string.Format("show('{0}');", string.Format(IMEI_FORMAT, e.Row.Cells[5].Text));
            e.Row.Cells[5].Attributes["onmouseout"] = "hide();";
            Button btnIMEI = (Button)e.Row.Cells[6].FindControl("Button2");
            btnIMEI.Attributes["onclick"] = "imeiclick(this);";
            btnIMEI.Visible = true;
         }
      }
   }
}
