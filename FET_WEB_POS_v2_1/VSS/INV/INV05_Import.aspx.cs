using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.Web.ASPxGridView;

public partial class VSS_INV_INV05_Import : Popup
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            gvProduct.DataSource = GetEmptyDataTable1();
            gvProduct.DataBind();

            gvStore.DataSource = GetEmptyDataTable2();
            gvStore.DataBind();

        }
    }

    protected void BindData()
    {
        gvProduct.DataSource = GetProductData();
        gvProduct.DataBind();

        gvStore.DataSource = GetStoreData();
        gvStore.DataBind();
    }

    private DataTable GetEmptyDataTable1()
    {
        DataTable dtResult = new DataTable();
        dtResult.Columns.Add("商品料號", typeof(string));
        dtResult.Columns.Add("商品名稱", typeof(string));
        dtResult.Columns.Add("異常原因", typeof(string));
        return dtResult;
    }

    private DataTable GetEmptyDataTable2()
    {
        DataTable dtResult = new DataTable();
        dtResult.Columns.Add("門市編號", typeof(string));
        dtResult.Columns.Add("門市名稱", typeof(string));
        dtResult.Columns.Add("異常原因", typeof(string));
        return dtResult;
    }


    private DataTable GetProductData()
    {
        DataTable dtResult = GetEmptyDataTable1();

        DataRow NewRow = dtResult.NewRow();
        NewRow["商品料號"] = "100100250";
        NewRow["商品名稱"] = "";
        NewRow["異常原因"] = "";
        dtResult.Rows.Add(NewRow);

        NewRow = dtResult.NewRow();
        NewRow["商品料號"] = "100100251";
        NewRow["商品名稱"] = "Noka6230";
        NewRow["異常原因"] = "商品料號不存在";
        dtResult.Rows.Add(NewRow);

        return dtResult;
    }

    private DataTable GetStoreData()
    {
        DataTable dtResult = GetEmptyDataTable2();

        DataRow NewRow = dtResult.NewRow();
        NewRow["門市編號"] = "2101";
        NewRow["門市名稱"] = "";
        NewRow["異常原因"] = "";
        dtResult.Rows.Add(NewRow);

        NewRow = dtResult.NewRow();
        NewRow["門市編號"] = "2102";
        NewRow["門市名稱"] = "西門";
        NewRow["異常原因"] = "店組不存在";
        dtResult.Rows.Add(NewRow);

        return dtResult;
    }

    protected void GridView_RowDataBound(Object sender, GridViewRowEventArgs e)
    {
        //if (e.Row.RowType == DataControlRowType.DataRow)
        //{
        //    Label lb1 = (Label)e.Row.Cells[2].FindControl("Label1");

        //    if (lb1.Text != "")
        //    {
        //        for (int i = 0; i < e.Row.Cells.Count; i++)
        //        {
        //            e.Row.Cells[i].ForeColor = System.Drawing.Color.Red;
        //        }

        //        btnCommit.Enabled = false;
        //    }

        //    //DataRowView drv = e.Row.DataItem as DataRowView;
        //    //if (!string.IsNullOrEmpty(drv["異常原因"].ToString()))
        //    //{
        //    //    e.Row.Style[HtmlTextWriterStyle.Color] = "Red";
        //    //}

        //}
    }
    protected void btnImport_Click(object sender, EventArgs e)
    {
        BindData();
    }
    protected void btnCommit_Click(object sender, EventArgs e)
    {
        //SetReturnValue("OK");
    }
    protected void gvProduct_HtmlRowPrepared(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewTableRowEventArgs e)
    {
        if (e.RowType != GridViewRowType.Data) return;
        if (e.GetValue("異常原因").ToString() != string.Empty)
            e.Row.ForeColor = System.Drawing.Color.Red;

    }
    protected void gvStore_HtmlRowPrepared(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewTableRowEventArgs e)
    {
        if (e.RowType != GridViewRowType.Data) return;
        if (e.GetValue("異常原因").ToString() != string.Empty)
            e.Row.ForeColor = System.Drawing.Color.Red;
            btnCommit.Enabled = false;
    }
}
