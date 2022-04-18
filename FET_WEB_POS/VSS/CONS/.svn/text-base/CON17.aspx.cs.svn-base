using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class VSS_CONS_CON17 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
       if (!Page.IsPostBack)
       {
          bindMasterData();
       }
    }

    protected void bindMasterData()
    {
        DataTable dtGvMaster = new DataTable();
        dtGvMaster = getMasterData();
        //ViewState["gvMaster"] = dtGvMaster;
        gvMaster.DataSource = dtGvMaster;
        gvMaster.DataBind();
    }


    private DataTable getMasterData()
    {
        DataTable dtMaster = new DataTable();
        dtMaster.Columns.Clear();
        dtMaster.Columns.Add("廠商代號", typeof(string));
        dtMaster.Columns.Add("廠商名稱", typeof(string));
        dtMaster.Columns.Add("結算月份", typeof(string));
        dtMaster.Columns.Add("結算起日", typeof(DateTime));
        dtMaster.Columns.Add("結算訖日", typeof(DateTime));
        dtMaster.Columns.Add("結算金額", typeof(int));
        dtMaster.Columns.Add("結算狀態", typeof(string));
        dtMaster.Columns.Add("更新人員", typeof(string));
        dtMaster.Columns.Add("更新日期", typeof(DateTime));

        DataRow dtMasterRow = dtMaster.NewRow();
        dtMasterRow["廠商代號"] = "AC001";
        dtMasterRow["廠商名稱"] = "廠商A";
        dtMasterRow["結算月份"] = "07/2010";
        dtMasterRow["結算起日"] = DateTime.Parse("2010/7/1");
        dtMasterRow["結算訖日"] = DateTime.Parse("2010/7/31");
        dtMasterRow["結算金額"] = 205000;
        dtMasterRow["結算狀態"] = "已請款";
        dtMasterRow["更新人員"] = "王大寶";
        dtMasterRow["更新日期"] = DateTime.Parse("2010/7/1");
        dtMaster.Rows.Add(dtMasterRow);

        dtMasterRow = dtMaster.NewRow();
        dtMasterRow["廠商代號"] = "AC002";
        dtMasterRow["廠商名稱"] = "廠商B";
        dtMasterRow["結算月份"] = "07/2010";
        dtMasterRow["結算起日"] = DateTime.Parse("2010/7/1");
        dtMasterRow["結算訖日"] = DateTime.Parse("2010/7/31");
        dtMasterRow["結算金額"] = 89000;
        dtMasterRow["結算狀態"] = "未請款";
        dtMasterRow["更新人員"] = "王大寶";
        dtMasterRow["更新日期"] = DateTime.Parse("2010/7/1");
        dtMaster.Rows.Add(dtMasterRow);
        
        return dtMaster;
    }

    private DataTable GetDetailedData()
    {
        DataTable dt = new DataTable();
        dt.Columns.Clear();
        dt.Columns.Add("廠商代號", typeof(string));
        dt.Columns.Add("結算金額", typeof(int));
        dt.Columns.Add("銷貨總額", typeof(int));
        dt.Columns.Add("銷貨金額", typeof(int));
        dt.Columns.Add("銷貨稅額", typeof(int));
        dt.Columns.Add("佣金總額", typeof(int));
        dt.Columns.Add("佣金金額", typeof(int));
        dt.Columns.Add("佣金稅額", typeof(int));
        dt.Columns.Add("進貨總額", typeof(int));
        dt.Columns.Add("進貨金額", typeof(int));
        dt.Columns.Add("進貨稅額", typeof(int));
        dt.Columns.Add("期末庫存總額", typeof(int));
        dt.Columns.Add("期末庫存金額", typeof(int));
        dt.Columns.Add("期末庫存稅額", typeof(int));
        dt.Columns.Add("退倉總額", typeof(int));
        dt.Columns.Add("退倉金額", typeof(int));
        dt.Columns.Add("退倉稅額", typeof(int));

        DataRow dr = dt.NewRow();
        dr["廠商代號"] = "AC001";
        dr["結算金額"] = 205000;
        dr["銷貨總額"] = 10000;
        dr["銷貨金額"] = 0;
        dr["銷貨稅額"] = 20000;
        dr["佣金總額"] = 0;
        dr["佣金金額"] = 0;
        dr["佣金稅額"] = 0;
        dr["進貨總額"] = 100000;
        dr["進貨金額"] = 100000;
        dr["進貨稅額"] = 1000;
        dr["期末庫存總額"] = 100000;
        dr["期末庫存金額"] = 100000;
        dr["期末庫存稅額"] = 10000;
        dr["退倉總額"] = 0;
        dr["退倉金額"] = 0;
        dr["退倉稅額"] = 0;
        dt.Rows.Add(dr);

        dr = dt.NewRow();
        dr["廠商代號"] = "AC002";
        dr["結算金額"] = 89000;
        dr["銷貨總額"] = 10000;
        dr["銷貨金額"] = 0;
        dr["銷貨稅額"] = 20000;
        dr["佣金總額"] = 0;
        dr["佣金金額"] = 0;
        dr["佣金稅額"] = 0;
        dr["進貨總額"] = 0;
        dr["進貨金額"] = 0;
        dr["進貨稅額"] = 0;
        dr["期末庫存總額"] = 0;
        dr["期末庫存金額"] = 0;
        dr["期末庫存稅額"] = 0;
        dr["退倉總額"] = 0;
        dr["退倉金額"] = 0;
        dr["退倉稅額"] = 0;
        dt.Rows.Add(dr);

        return dt;
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        bindMasterData();

        Panel1.Visible = true;
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {        
        //lblStatus.Text = "已存檔";
    }

    protected void gvMaster_OnRowDataBound(object sender, GridViewRowEventArgs e)
    {
        GridViewRow row = e.Row;
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            DataRowView view = row.DataItem as DataRowView;
            Button b = e.Row.FindControl("btnPay") as Button;
            if (view["結算狀態"].ToString() == "未請款")
            {
                //b.CommandName = "GoToEdit";
                //b.Text = GetGlobalResourceObject("WebResources", "Edit").ToString();
                b.Enabled = true;
            }
            else
            {
                b.Enabled = false;
                //b.CommandName = "Select";
                //b.Text = GetGlobalResourceObject("WebResources", "View").ToString();
                //b.Enabled = false;
            }
           
        }
    }

    protected void gvMaster_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Select")
        {
            DataTable dt = GetDetailedData();
           
            
            //GridViewRow row = (GridViewRow)((Control)e.CommandSource).Parent.Parent;
            //GridViewRow row = (GridViewRow)((Button)sender).NamingContainer;
            //Response.Write("Row index: " + row.RowIndex);


            DataView dv = new DataView(dt);
            dv.RowFilter = "廠商代號 = '" + e.CommandArgument.ToString() + "'";

            FormView1.DataSource = dv;
            FormView1.DataBind();
            Panel1.Visible = true;
        }
        if (e.CommandName == "Pay")
        {
           
            int index = Convert.ToInt32(e.CommandArgument);
            GridViewRow row = gvMaster.Rows[index];
            Button btnPay = (Button)row.Cells[0].FindControl("btnPay");
            row.Cells[7].Text = "已請款";
            btnPay.Enabled = false;
        }
    }
    protected void gvMaster_RowCreated(Object sender, GridViewRowEventArgs e)
    {

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Button Button1 = (Button)e.Row.Cells[0].FindControl("btnPay");
            Button1.CommandArgument = e.Row.RowIndex.ToString();
        }

    }
}
