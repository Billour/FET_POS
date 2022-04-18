using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class VSS_LEA_LEA05 : System.Web.UI.Page
{
   protected void Page_Load(object sender, EventArgs e)
   {
       if (!IsPostBack)
       {
           bindDetailData();
           showList.Visible = false;
       }
   }

   protected void bindDetailData()
   {
      DataTable dtResult = new DataTable();

      dtResult = getDetailDB();
      ViewState["gvList"] = dtResult;
      gvList.DataSource = dtResult;
      gvList.DataBind();
   }
   private DataTable getDetailDB()
   {
      DataTable dtResult = new DataTable();
      dtResult.Columns.Add("賠償項目", typeof(string));
      dtResult.Columns.Add("金額", typeof(string));

      string[] s1 = { "電池", "旅充", "螢幕面板損毀", "天線毀損", "手機無法開機/螢幕液損毀" };
      string[] s2 = { "1200", "1500", "700", "560", "2800" };


      for (int i = 0; i <= 4; i++)
      {
         DataRow NewRow = dtResult.NewRow();
         NewRow["賠償項目"] = s1[i].ToString();
         NewRow["金額"] = s2[i].ToString();
         dtResult.Rows.Add(NewRow);
      }
      return dtResult;
   }

   protected void RadioButtonList3_SelectedIndexChanged(object sender, EventArgs e)
   {
       if (this.RadioButtonList3.SelectedItem.Text == "是")
       {
           this.showList.Visible = true;
           bindDetailData();
       }
       else
       {
           this.showList.Visible = false;
       }
   }

   protected void Button1_Click(object sender, EventArgs e)
   {
      this.Label3.Visible = true;
      this.showList.Visible = false;
   }
   protected void TextBox4_TextChanged(object sender, EventArgs e)
   {
      if (this.TextBox4.Text != "")
      {
         this.Label2.Visible = true;
      }
      else
      {
         this.Label2.Visible = false;
      }
   }
   protected void TextBox6_TextChanged(object sender, EventArgs e)
   {
      if (this.TextBox6.Text != "")
      {
         this.Label1.Visible = true;
      }
      else
      {
         this.Label1.Visible = false;
      }
   }

   private DataTable GetDetailedData()
   {
      DataTable dt = new DataTable();
      dt.Columns.Clear();
      dt.Columns.Add("廠商代號", typeof(string));
      dt.Columns.Add("結算金額", typeof(int));
      dt.Columns.Add("銷貨總額", typeof(int));
      dt.Columns.Add("銷貨金額", typeof(int));


      DataRow dr = dt.NewRow();
      dr["廠商代號"] = "AC001";
      dr["結算金額"] = 205000;
      dr["銷貨總額"] = 10000;
      dr["銷貨金額"] = 0;

      dt.Rows.Add(dr);

      dr = dt.NewRow();
      dr["廠商代號"] = "AC002";
      dr["結算金額"] = 89000;
      dr["銷貨總額"] = 10000;
      dr["銷貨金額"] = 0;

      dt.Rows.Add(dr);

      return dt;
   }
   protected void btnSave_Click(object sender, EventArgs e)
   {
      //Panel2.Visible = true;

      DataTable dt = GetDetailedData();



      DataView dv = new DataView(dt);

      FormView1.DataSource = dv;
      FormView1.DataBind();
      Panel2.Visible = true;
   }
}
