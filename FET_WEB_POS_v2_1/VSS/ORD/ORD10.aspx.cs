using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class VSS_ORD_ORD10 : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            //BindData();
            // 繫結空的資料表，以顯示表頭欄位
            gvMaster.DataSource = GetEmptyDataTable();
            gvMaster.DataBind();


            Literal01.Visible = false;
            Literal02.Visible = false;
            Literal03.Visible = false;
        }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        BindData();
        Panel1.Visible = true;

        Literal01.Visible = true;
        Literal02.Visible = true;
        Literal03.Visible = true;

    }


    private DataTable GetEmptyDataTable()
    {
        DataTable dataTable = new DataTable();
        dataTable.Columns.Clear();
        dataTable.Columns.Add("項次", typeof(int));
        dataTable.Columns.Add("區域", typeof(string));
        dataTable.Columns.Add("門市代號", typeof(string));
        dataTable.Columns.Add("門市名稱", typeof(string));
        dataTable.Columns.Add("比率", typeof(string));
        return dataTable;
    }

    private void BindData()
    {
        DataTable dataTable = GetEmptyDataTable();

        DataRow row = dataTable.NewRow();
        row["項次"] = 1;
        row["區域"] = "北一區";
        row["門市代號"] = "2101";
        row["門市名稱"] = "遠企";
        row["比率"] = "10%";
        dataTable.Rows.Add(row);

        row = dataTable.NewRow();
        row["項次"] = 2;
        row["區域"] = "北一區";
        row["門市代號"] = "2102";
        row["門市名稱"] = "華納";
        row["比率"] = "5%";
        dataTable.Rows.Add(row);

        row = dataTable.NewRow();
        row["項次"] = 3;
        row["區域"] = "北二區";
        row["門市代號"] = "2201";
        row["門市名稱"] = "站前";
        row["比率"] = "15%";
        dataTable.Rows.Add(row);

        row = dataTable.NewRow();
        row["項次"] = 4;
        row["區域"] = "北二區";
        row["門市代號"] = "2202";
        row["門市名稱"] = "站前";
        row["比率"] = "5%";
        dataTable.Rows.Add(row);

        row = dataTable.NewRow();
        row["項次"] = 5;
        row["區域"] = "北二區";
        row["門市代號"] = "2203";
        row["門市名稱"] = "西門";
        row["比率"] = "65%";
        dataTable.Rows.Add(row);

        gvMaster.DataSource = dataTable;
        gvMaster.DataBind();

    }


    protected void gvMaster_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        //if (e.Row.RowType == DataControlRowType.Footer)
        //{
        //    e.Row.Cells.RemoveAt(1);
        //    e.Row.Cells.RemoveAt(1);
        //    e.Row.Cells.RemoveAt(1);

        //    e.Row.Cells[0].ColumnSpan = 4;
        //    e.Row.Cells[0].Text = "比率統計";
        //    e.Row.Cells[0].HorizontalAlign = HorizontalAlign.Right;
        //    e.Row.Cells[1].Text = "100%";
        //    e.Row.Cells[1].HorizontalAlign = HorizontalAlign.Right;
        //}

    }
}
