using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using DevExpress.Web.ASPxGridView;
using DevExpress.Web.ASPxEditors;
public partial class VSS_INV_INV07 : BasePage
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
      dtResult.Columns.Add("IMEI控管", typeof(bool));
      dtResult.Columns.Add("帳上庫存量", typeof(string));
      dtResult.Columns.Add("未拆封數量", typeof(string));
      dtResult.Columns.Add("已拆封數量", typeof(string));
      dtResult.Columns.Add("退倉數量", typeof(string));
      dtResult.Columns.Add("IMEI", typeof(string));
      dtResult.Columns.Add("差異量", typeof(string));
      //dtResult.Columns.Add("ERP驗退日期", typeof(string));
      //dtResult.Columns.Add("ERP驗退單號", typeof(string));
      //dtResult.Columns.Add("驗退數量", typeof(string));

      for (int i = 1; i <= 6; i++)
      {
          DataRow NewRow = dtResult.NewRow();
         if (i == 3)
         {
            
            NewRow["項次"] = i;
            NewRow["商品編號"] = "100100100" + i.ToString("000");
            NewRow["商品名稱"] = i.ToString("00") + "手機";
            NewRow["IMEI控管"] = true;
            NewRow["帳上庫存量"] = 2 * i + i;
            NewRow["未拆封數量"] = i;
            NewRow["已拆封數量"] = i;  
            //NewRow["退倉數量"] = Convert.ToInt32(NewRow["未拆封數量"]) + Convert.ToInt32(NewRow["已拆封數量"]);
            NewRow["IMEI"] = "3";
            //NewRow["差異量"] = Convert.ToInt32(NewRow["帳上庫存量"]) - Convert.ToInt32(NewRow["退倉數量"]); ;
            //NewRow["ERP驗退日期"] = "";
            //NewRow["ERP驗退單號"] = "";
            //NewRow["驗退數量"] = "";
            
         }
         else
         {
            //DataRow NewRow = dtResult.NewRow();
            NewRow["項次"] = i;
            NewRow["商品編號"] = "100100100" + i.ToString("000");
            NewRow["商品名稱"] = i.ToString("00") + "手機";
             NewRow["IMEI控管"] = false;
            NewRow["帳上庫存量"] = 2 * i + i; 
            NewRow["未拆封數量"] = i;  
            NewRow["已拆封數量"] = i; 
            //NewRow["退倉數量"] = Convert.ToInt32(NewRow["未拆封數量"]) + Convert.ToInt32(NewRow["已拆封數量"]);
            NewRow["IMEI"] = "0";
            //NewRow["差異量"] = "0" ;

            //NewRow["ERP驗退日期"] = "";
                //"2010/08/01";
            //NewRow["ERP驗退單號"] = "";
                //"ERP000" + i.ToString("000");
            //NewRow["驗退數量"] = "";
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

   protected void gvMaster_HtmlRowCreated(object sender, ASPxGridViewTableRowEventArgs e)
   {
       if (e.RowType == GridViewRowType.Data)
       {
           DataRowView Rows = (DataRowView)gvMaster.GetRow(e.VisibleIndex);
           bool check = bool.Parse(Rows["IMEI控管"].ToString());

           ASPxLabel lblIMEI = e.Row.FindChildControl<ASPxLabel>("lbl1");
           ASPxButton btn1 = e.Row.FindChildControl<ASPxButton>("Button2");

           // 繫結明細資料表           
           e.Row.Cells[8].Attributes["onmouseover"] = string.Format("show('{0}');", IMEIContent(Convert.ToInt16(lblIMEI.Text)));
           e.Row.Cells[8].Attributes["onmouseout"] = "hide();";

           if (check == false)
           {
              
               btn1.Enabled = false;
           }
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

   protected void ASPxButton2_Click(object sender, EventArgs e)
   {

   }
}
