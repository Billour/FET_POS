using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class VSS_CONS_CON04_1 : BasePage
{
   protected void Page_Load(object sender, EventArgs e)
   {
      if (!IsPostBack)
      {
         bindMasterData3(0);
         bindMasterData1(0);
      }
   }

   protected void bindMasterData1(int TempCount)
   {
      DataTable dtResult = new DataTable();
      dtResult = getMasterData1(TempCount);
      ViewState["gvMaster1"] = dtResult;
      GridView1.DataSource = dtResult;
      GridView1.DataBind();
   }
   private DataTable getMasterData1(int TempCount)
   {
      DataTable dtResult = new DataTable();
      dtResult.Columns.Add("廠商代號", typeof(string));
      dtResult.Columns.Add("佣金比率", typeof(string));
      dtResult.Columns.Add("起始月份", typeof(string));
      dtResult.Columns.Add("結束月份", typeof(string));

      for (int i = 0; i < TempCount; i++)
      {
         DataRow dtMasterRow = dtResult.NewRow();
         dtMasterRow["廠商代號"] = 1;
         dtMasterRow["佣金比率"] = 1;
         dtMasterRow["起始月份"] = 1;
         dtMasterRow["結束月份"] = 1;

         dtResult.Rows.Add(dtMasterRow);
      }

      return dtResult;
   }

   protected void bindMasterData3(int TempCount)
   {
      DataTable dtResult = new DataTable();
      dtResult = getMasterData3(TempCount);
      ViewState["gvMaster3"] = dtResult;
      GridView3.DataSource = dtResult;
      GridView3.DataBind();
   }
   private DataTable getMasterData3(int TempCount)
   {
      DataTable dtResult = new DataTable();
      dtResult.Columns.Add("廠商代號", typeof(string));
      dtResult.Columns.Add("商品代號", typeof(string));
      dtResult.Columns.Add("商品類別", typeof(string));
      dtResult.Columns.Add("商品名稱", typeof(string));
      dtResult.Columns.Add("上架日", typeof(string));
      dtResult.Columns.Add("下架日", typeof(string));
      dtResult.Columns.Add("停止訂購日", typeof(string));
      dtResult.Columns.Add("科目1", typeof(string));
      dtResult.Columns.Add("科目2", typeof(string));
      dtResult.Columns.Add("科目3", typeof(string));
      dtResult.Columns.Add("科目4", typeof(string));
      dtResult.Columns.Add("科目5", typeof(string));
      dtResult.Columns.Add("科目6", typeof(string));
      dtResult.Columns.Add("單位", typeof(string));
      dtResult.Columns.Add("佣金比率", typeof(string));
      dtResult.Columns.Add("起始月份", typeof(string));
      dtResult.Columns.Add("結束月份", typeof(string));

      for (int i = 0; i < TempCount; i++)
      {
         DataRow dtMasterRow = dtResult.NewRow();
         dtMasterRow["廠商代號"] = 1;
         dtMasterRow["商品代號"] = 1;
         dtMasterRow["商品類別"] = 1;
         dtMasterRow["商品名稱"] = 1;
         dtMasterRow["上架日"] = 1;
         dtMasterRow["下架日"] = 1;
         dtMasterRow["停止訂購日"] = 1;
         dtMasterRow["科目1"] = 10;
         dtMasterRow["科目2"] = 20;
         dtMasterRow["科目3"] = 3000;
         dtMasterRow["科目4"] = 400000;
         dtMasterRow["科目5"] = 5000;
         dtMasterRow["科目6"] = 6000;
         dtMasterRow["單位"] = 1;
         dtMasterRow["佣金比率"] = 1;
         dtMasterRow["起始月份"] = 1;
         dtMasterRow["結束月份"] = 1;
         dtResult.Rows.Add(dtMasterRow);
      }

      return dtResult;
   }

}
