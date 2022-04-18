using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class VSS_CON01_1 : Advtek.Utility.BasePage
{
   protected void Page_Load(object sender, EventArgs e)
   {
      if (!IsPostBack)
      {
         bindMasterData(0);
      }
   }

   protected void bindMasterData(int TempCount)
   {
      DataTable dtResult = new DataTable();
      dtResult = getMasterData(TempCount);
      ViewState["gvMaster"] = dtResult;
      gvMaster.DataSource = dtResult;
      gvMaster.DataBind();
   }  
   private DataTable getMasterData(int TempCount)
   {
      DataTable dtResult = new DataTable();
      dtResult.Columns.Add("廠商類別", typeof(string));
      dtResult.Columns.Add("廠商代號", typeof(string));
      dtResult.Columns.Add("廠商名稱", typeof(string));
      dtResult.Columns.Add("公司地址", typeof(string));
      dtResult.Columns.Add("聯絡人", typeof(string));
      dtResult.Columns.Add("聯絡電話", typeof(string));
      dtResult.Columns.Add("合作起日", typeof(string));
      dtResult.Columns.Add("合作訖日", typeof(string));
      dtResult.Columns.Add("合約號碼", typeof(string));
      dtResult.Columns.Add("結算日", typeof(string));
      dtResult.Columns.Add("統一編號", typeof(string));
      dtResult.Columns.Add("負責人", typeof(string));
      dtResult.Columns.Add("電話號碼", typeof(string));
      dtResult.Columns.Add("傳真", typeof(string));
      dtResult.Columns.Add("電子信箱", typeof(string));
      dtResult.Columns.Add("總金額底限", typeof(string));
      dtResult.Columns.Add("總金額底限勾選", typeof(string));
      dtResult.Columns.Add("備註", typeof(string));
      dtResult.Columns.Add("科目1", typeof(string));
      dtResult.Columns.Add("科目2", typeof(string));
      dtResult.Columns.Add("科目3", typeof(string));
      dtResult.Columns.Add("科目4", typeof(string));
      dtResult.Columns.Add("科目5", typeof(string));
      dtResult.Columns.Add("科目6", typeof(string));


      for (int i = 0; i < TempCount; i++)
      {
         DataRow dtMasterRow = dtResult.NewRow();
         dtMasterRow["廠商類別"] = 1;
         dtMasterRow["廠商代號"] = 1;
         dtMasterRow["廠商名稱"] = 1;
         dtMasterRow["公司地址"] = 1;
         dtMasterRow["聯絡人"] = 1;
         dtMasterRow["聯絡電話"] = 1;
         dtMasterRow["合作起日"] = 1;
         dtMasterRow["合作訖日"] = 1;
         dtMasterRow["合約號碼"] = 1;
         dtMasterRow["結算日"] = 1;
         dtMasterRow["統一編號"] = 1;
         dtMasterRow["負責人"] = 1;
         dtMasterRow["電話號碼"] = 1;
         dtMasterRow["傳真"] = 1;
         dtMasterRow["電子信箱"] = 1;
         dtMasterRow["總金額底限"] = 1;
         dtMasterRow["總金額底限勾選"] = 1;
         dtMasterRow["備註"] = 1;
         dtMasterRow["科目1"] = 1;
         dtMasterRow["科目2"] = 1;
         dtMasterRow["科目3"] = 1;
         dtMasterRow["科目4"] = 1;
         dtMasterRow["科目5"] = 1;
         dtMasterRow["科目6"] = 1;

         dtResult.Rows.Add(dtMasterRow);
      }

      return dtResult;
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

   protected void bindMasterData2(int TempCount)
   {
      DataTable dtResult = new DataTable();
      dtResult = getMasterData2(TempCount);
      ViewState["gvMaster2"] = dtResult;
      GridView2.DataSource = dtResult;
      GridView2.DataBind();
   }
   private DataTable getMasterData2(int TempCount)
   {
      DataTable dtResult = new DataTable();
      dtResult.Columns.Add("廠商代號", typeof(string));
      dtResult.Columns.Add("門市代號", typeof(string));

      for (int i = 0; i < TempCount; i++)
      {
         DataRow dtMasterRow = dtResult.NewRow();
         dtMasterRow["廠商代號"] = 1;
         dtMasterRow["門市代號"] = 1;

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
         dtMasterRow["科目1"] = 1;
         dtMasterRow["科目2"] = 1;
         dtMasterRow["科目3"] = 1;
         dtMasterRow["科目4"] = 1;
         dtMasterRow["科目5"] = 1;
         dtMasterRow["科目6"] = 1;
         dtMasterRow["單位"] = 1;
         dtMasterRow["佣金比率"] = 1;
         dtMasterRow["起始月份"] = 1;
         dtMasterRow["結束月份"] = 1;
         dtResult.Rows.Add(dtMasterRow);
      }

      return dtResult;
   }

   protected void bindMasterData4(int TempCount)
   {
      DataTable dtResult = new DataTable();
      dtResult = getMasterData4(TempCount);
      ViewState["gvMaster4"] = dtResult;
      GridView4.DataSource = dtResult;
      GridView4.DataBind();
   }
   private DataTable getMasterData4(int TempCount)
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

   protected void bindMasterData5(int TempCount)
   {
      DataTable dtResult = new DataTable();
      dtResult = getMasterData5(TempCount);
      ViewState["gvMaster5"] = dtResult;
      GridView5.DataSource = dtResult;
      GridView5.DataBind();
   }
   private DataTable getMasterData5(int TempCount)
   {
      DataTable dtResult = new DataTable();
      dtResult.Columns.Add("廠商代號", typeof(string));
      dtResult.Columns.Add("級距項次", typeof(string));
      dtResult.Columns.Add("起-金額級距", typeof(string));
      dtResult.Columns.Add("訖-金額級距", typeof(string));
      dtResult.Columns.Add("佣金比率", typeof(string));
      dtResult.Columns.Add("起始月份", typeof(string));
      dtResult.Columns.Add("結束月份", typeof(string));


      for (int i = 0; i < TempCount; i++)
      {
         DataRow dtMasterRow = dtResult.NewRow();
         dtMasterRow["廠商代號"] = 1;
         dtMasterRow["級距項次"] = 1;
         dtMasterRow["起-金額級距"] = 1;
         dtMasterRow["訖-金額級距"] = 1;
         dtMasterRow["佣金比率"] = 1;
         dtMasterRow["起始月份"] = 1;
         dtMasterRow["結束月份"] = 1;

         dtResult.Rows.Add(dtMasterRow);
      }

      return dtResult;
   }

   protected void bindMasterData6(int TempCount)
   {
      DataTable dtResult = new DataTable();
      dtResult = getMasterData6(TempCount);
      ViewState["gvMaster6"] = dtResult;
      GridView6.DataSource = dtResult;
      GridView6.DataBind();
   }
   private DataTable getMasterData6(int TempCount)
   {
      DataTable dtResult = new DataTable();
      dtResult.Columns.Add("廠商代號", typeof(string));
      dtResult.Columns.Add("商品料號", typeof(string));

      for (int i = 0; i < TempCount; i++)
      {
         DataRow dtMasterRow = dtResult.NewRow();
         dtMasterRow["廠商代號"] = 1;
         dtMasterRow["商品料號"] = 1;

         dtResult.Rows.Add(dtMasterRow);
      }

      return dtResult;
   }

   protected void bindMasterData7(int TempCount)
   {
      DataTable dtResult = new DataTable();
      dtResult = getMasterData7(TempCount);
      ViewState["gvMaster7"] = dtResult;
      GridView7.DataSource = dtResult;
      GridView7.DataBind();
   }
   private DataTable getMasterData7(int TempCount)
   {
      DataTable dtResult = new DataTable();
      dtResult.Columns.Add("項次", typeof(string));
      dtResult.Columns.Add("信用卡別", typeof(string));
      dtResult.Columns.Add("手續費", typeof(string));

      for (int i = 0; i < TempCount; i++)
      {
         DataRow dtMasterRow = dtResult.NewRow();
         dtMasterRow["項次"] = 1;
         dtMasterRow["信用卡別"] = 1;
         dtMasterRow["手續費"] = 1;

         dtResult.Rows.Add(dtMasterRow);
      }

      return dtResult;
   }

   //protected void TabContainer1_ActiveTabChanged(object sender, EventArgs e)
   //{
   //    int a = this.TabContainer1.ActiveTabIndex;

   //    switch (a)
   //    {
   //        case 1:
   //            bindMasterData1(0);
   //            break;

   //        case 2:
   //            bindMasterData2(0);
   //            break;

   //        case 3:
   //            bindMasterData3(0);
   //            break;

   //        case 4:
   //            bindMasterData4(0);
   //            break;

   //        case 5:
   //            bindMasterData5(0);
   //            break;

   //        case 6:
   //            bindMasterData6(0);
   //            break;

   //        case 7:
   //            bindMasterData7(0);
   //            break;

   //        default:
   //            bindMasterData(0);
   //            break;
   //    }
   //}
   protected void ASPxPageControl1_ActiveTabChanged(object source, DevExpress.Web.ASPxTabControl.TabControlEventArgs e)
   {
       int a = this.ASPxPageControl1.ActiveTabIndex;

       switch (a)
       {
           case 1:
               bindMasterData1(1);
               break;

           case 2:
               bindMasterData2(2);
               break;

           case 3:
               bindMasterData3(3);
               break;

           case 4:
               bindMasterData4(4);
               break;

           case 5:
               bindMasterData5(5);
               break;

           case 6:
               bindMasterData6(4);
               break;

           case 7:
               bindMasterData7(3);
               break;

           default:
               bindMasterData(2);
               break;
       }
   }
}
