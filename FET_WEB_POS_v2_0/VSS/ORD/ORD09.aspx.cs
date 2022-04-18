using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using DevExpress.Web.ASPxGridView;
using DevExpress.Web.ASPxEditors;	

public partial class VSS_ORD_ORD09 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
           bindMasterData(0);
        }
    }
    protected void bindMasterData(int tempCount)
    {
       DataTable dtResult = new DataTable();
       dtResult = getMasterData(tempCount); 
       ViewState["gvMaster"] = dtResult;
       gvMaster.DataSource = dtResult;
       gvMaster.DataBind();
    }

    private DataTable getMasterData(int tempCount)
    {
        DataTable dtResult = new DataTable();
        dtResult.Columns.Add("門市代碼", typeof(string));
        dtResult.Columns.Add("門市名稱", typeof(string));
        dtResult.Columns.Add("商品料號", typeof(string));
        dtResult.Columns.Add("商品名稱", typeof(string));
        dtResult.Columns.Add("主配量", typeof(string));
        dtResult.Columns.Add("異常原因", typeof(string));

        string[] straReason = { "店組不存在", "商品料號不存在", "主配數量不可為空", "主配數量應大於零" };

        DataRow NewRow = dtResult.NewRow();
        NewRow["門市代碼"] = "2101";
        NewRow["門市名稱"] = "永和";
        NewRow["商品料號"] = "100100200";
        NewRow["商品名稱"] = "Nokia N95";
        NewRow["主配量"] = "6";
        NewRow["異常原因"] = straReason[0];
        dtResult.Rows.Add(NewRow);

        NewRow = dtResult.NewRow();
        NewRow["門市代碼"] = "2101";
        NewRow["門市名稱"] = "永和";
        NewRow["商品料號"] = "100100200";
        NewRow["商品名稱"] = "Nokia N95";
        NewRow["主配量"] = "5";
        NewRow["異常原因"] = straReason[0];
        dtResult.Rows.Add(NewRow);

        NewRow = dtResult.NewRow();
        NewRow["門市代碼"] = "2101";
        NewRow["門市名稱"] = "永和";
        NewRow["商品料號"] = "10010021";
        NewRow["商品名稱"] = "";
        NewRow["主配量"] = "7";
        NewRow["異常原因"] = straReason[1];
        dtResult.Rows.Add(NewRow);


        NewRow = dtResult.NewRow();
        NewRow["門市代碼"] = "2101";
        NewRow["門市名稱"] = "永和";
        NewRow["商品料號"] = "100100203";
        NewRow["商品名稱"] = "Nokia N65";
        NewRow["主配量"] = "";
        NewRow["異常原因"] = straReason[2];
        dtResult.Rows.Add(NewRow);

        NewRow = dtResult.NewRow();
        NewRow["門市代碼"] = "2102";
        NewRow["門市名稱"] = "中和";
        NewRow["商品料號"] = "100100251";
        NewRow["商品名稱"] = "HTC Hero";
        NewRow["主配量"] = "0";
        NewRow["異常原因"] = straReason[3];
        dtResult.Rows.Add(NewRow);

        NewRow = dtResult.NewRow();
        NewRow["門市代碼"] = "2102";
        NewRow["門市名稱"] = "中和";
        NewRow["商品料號"] = "100100252";
        NewRow["商品名稱"] = "HTC Legend";
        NewRow["主配量"] = "10";
        NewRow["異常原因"] = "";
        dtResult.Rows.Add(NewRow);

        return dtResult;
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
       bindMasterData(2);
    }

    protected void gvMaster_RowDataBound(object sender, GridViewRowEventArgs e)
    {
       if (e.Row.RowType == DataControlRowType.DataRow)
       {
          Label lb1 = (Label)e.Row.Cells[5].FindControl("Label1");

          Label lb2 = (Label)e.Row.Cells[1].FindControl("Label2");
          TextBox tb1 = (TextBox)e.Row.Cells[0].FindControl("TextBox1");
          TextBox tb2 = (TextBox)e.Row.Cells[2].FindControl("TextBox2");
          TextBox tb3 = (TextBox)e.Row.Cells[4].FindControl("TextBox3");

          if (lb1.Text != "")
          {
             tb1.ForeColor = System.Drawing.Color.Red;
             lb2.ForeColor = System.Drawing.Color.Red;
             tb2.ForeColor = System.Drawing.Color.Red;
             tb3.ForeColor = System.Drawing.Color.Red;
             lb1.ForeColor = System.Drawing.Color.Red;

             for (int i = 0; i < e.Row.Cells.Count - 1; i++)
             {
                e.Row.Cells[i].ForeColor = System.Drawing.Color.Red;
             }
          }
       }

    }
    protected void gvMasterDV_HtmlDataCellPrepared(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewTableDataCellEventArgs e)
    {
        if (e.DataColumn.FieldName == "異常原因")
        {
            if (!string.IsNullOrEmpty(e.CellValue.ToString()))
            {
                e.Cell.Style[HtmlTextWriterStyle.Color] = "red";
                //取得控制項裏的值出來
                GridViewDataColumn col = new GridViewDataColumn();
                col = (GridViewDataColumn)((ASPxGridView)sender).Columns["主配量"];
                ASPxTextBox t = gvMaster.FindRowCellTemplateControl(e.VisibleIndex, col, "ASPxTextBox1") as ASPxTextBox;
                t.ForeColor = System.Drawing.Color.Red;
           

            }
        }
    }
}
