using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Web.UI.HtmlControls;
using DevExpress.Web.ASPxEditors;
using DevExpress.Web.ASPxGridView;

public partial class VSS_Common_WeightRatingImportPopup : Popup
{
    protected void Page_Load(object sender, EventArgs e)
    {

        if (!Page.IsPostBack)
        {
            gvMaster.DataSource = GetEmptyDataTable1();
            gvMaster.DataBind();

        }
    }
    protected void btnImport_Click(object sender, EventArgs e)
    {
        BindData();

        //HtmlTable tdFooter = gvMaster.FindChildControl<HtmlTable>("tdFooter");
        //tdFooter.Visible = true;
    }
    protected void BindData()
    {
        gvMaster.DataSource = GetData();
        gvMaster.DataBind();
    }
    private DataTable GetData()
    {
        DataTable dtResult = GetEmptyDataTable1();

        DataRow NewRow = dtResult.NewRow();
        NewRow["門市編號"] = 2101;
        NewRow["門市名稱"] = "";
        NewRow["異常原因"] = "店組不存在";
        dtResult.Rows.Add(NewRow);

        NewRow = dtResult.NewRow();
        NewRow["門市編號"] = 2102;
        NewRow["門市名稱"] = "遠企";
        NewRow["異常原因"] = "比率不可為空值";
        dtResult.Rows.Add(NewRow);

        NewRow = dtResult.NewRow();
        NewRow["門市編號"] = 2103;
        NewRow["門市名稱"] = "西門";
        NewRow["比率"] = 0;
        NewRow["異常原因"] = "比率應大於0";
        dtResult.Rows.Add(NewRow);

        NewRow = dtResult.NewRow();
        NewRow["門市編號"] = 2105;
        NewRow["門市名稱"] = "華納";
        NewRow["比率"] = 90;
        dtResult.Rows.Add(NewRow);

        NewRow = dtResult.NewRow();
        NewRow["門市編號"] = 2106;
        NewRow["門市名稱"] = "站前";
        NewRow["比率"] = 8;
        dtResult.Rows.Add(NewRow);

        return dtResult;
    }
    private DataTable GetEmptyDataTable1()
    {
        DataTable dtResult = new DataTable();
        dtResult.Columns.Add("門市編號", typeof(int));
        dtResult.Columns.Add("門市名稱", typeof(string));
        dtResult.Columns.Add("比率", typeof(int));
        dtResult.Columns.Add("異常原因", typeof(string));
        return dtResult;
    }
    protected void btnCommit_Click(object sender, EventArgs e)
    {

    }

    protected void gvMaster_HtmlDataCellPrepared(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewTableDataCellEventArgs e)
    {
        if (e.DataColumn.FieldName == "異常原因")
        {
            if (!string.IsNullOrEmpty(e.CellValue.ToString()))
            {
                e.Cell.Style[HtmlTextWriterStyle.Color] = "red";
            }
        }
    }
    protected void gvMaster_HtmlFooterCellPrepared(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewTableFooterCellEventArgs e)
    {
        if (gvMaster.VisibleRowCount > 0)
        {
            ASPxLabel labelTotal = (ASPxLabel)gvMaster.FindFooterCellTemplateControl(e.Column, "lblFooterTotal");
            if (e.Column.Caption == "比率")
            {
                labelTotal.Text = "98";
            }
            else if (e.Column.Caption == "異常原因")
            {
                labelTotal.Text = "加總比率非100%";
            }
            else
            {
                labelTotal.Visible = false;
            }
        }
       
        //if (e.IsTotalFooter == true)
        //{
        //    ASPxLabel labelTotal = (ASPxLabel)gvMaster.FindFooterCellTemplateControl(e.Column, "lblFooterTotal");
        //    foreach (ASPxSummaryItem item in gvMaster.TotalSummary)
        //    {
        //        GridViewDataColumn column = (GridViewDataColumn)e.Column;
        //        if (item.FieldName == column.FieldName)
        //        {
        //            double total = Convert.ToDouble(e.GetSummaryValue(item));
        //            labelTotal.Text = String.Format(item.DisplayFormat, total);
        //            break;
        //        }
        //    }
        //}


    }
}
