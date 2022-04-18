using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Advtek.Utility;
using DevExpress.Web.ASPxGridView;

using DevExpress.Web.Data;

public partial class VSS_Demo_DevGridViewDemo : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

            bindMasterData();

            Session["FocusedRow"] = ASPxGridView1.GetRowValues(ASPxGridView1.FocusedRowIndex, new string[] { ASPxGridView1.KeyFieldName });
        }

        

    }
    public virtual string GetFieldValue(object item)
    {
        WebDescriptorRowBase row = item as WebDescriptorRowBase;
        return StringUtil.CStr(ASPxGridView1.GetRowValues(row.VisibleIndex, ASPxGridView1.KeyFieldName));
    }
    public virtual string GetFieldChecked(object item)
    {
        WebDescriptorRowBase row = item as WebDescriptorRowBase;
        object o = ASPxGridView1.GetRowValues(row.VisibleIndex, ASPxGridView1.KeyFieldName);
        return Session["FocusedRow"] == o ? "CHECKED" : "";
    }

    protected void bindEmptyData()
    {

        DataTable dtResult = new DataTable();



    }
    protected void bindMasterData()
    {
        DataTable dtResult = new DataTable();

        dtResult = getMasterData();


        ASPxGridView1.DataSource = dtResult;
        ASPxGridView1.DataBind();
        Session["dtResult"] = dtResult;


    }



    private DataTable getMasterData()
    {
        DataTable dtResult = new DataTable();
        dtResult.Columns.Add("賠償項目", typeof(string));
        dtResult.Columns.Add("金額", typeof(string));
        dtResult.Columns.Add("Quantity", typeof(string));
        dtResult.Columns.Add("Total", typeof(string));
        dtResult.Columns.Add("UnitPrice", typeof(string));
        for (int i = 0; i < 13; i++)
        {
            DataRow NewRow = dtResult.NewRow();
            NewRow["賠償項目"] = "電池" + StringUtil.CStr(i);
            NewRow["金額"] = "1200";

            NewRow["UnitPrice"] = StringUtil.CStr(10);
            NewRow["Quantity"] = StringUtil.CStr(10);
            NewRow["Total"] = Convert.ToInt32(NewRow["UnitPrice"]) * Convert.ToInt32(NewRow["Quantity"]);

            dtResult.Rows.Add(NewRow);
        }
        return dtResult;
    }





    protected void Button1_Click(object sender, EventArgs e)
    {
        bindMasterData();
        //Double a ;
        //a = Convert.ToDouble(ASPxGridView1.GetTotalSummaryValue(ASPxGridView1.TotalSummary["金額",DevExpress.Data.SummaryItemType.Max]));
        //Label2.Text = StringUtil.CStr(a);  ///this.aspxgridview1.getrowvalues
        int b = 0;
        for (int i = 0; i < ASPxGridView1.VisibleRowCount; i++)
        {

            b += Convert.ToInt32(this.ASPxGridView1.GetRowValues(i, "金額"));
        }
        Label2.Text = StringUtil.CStr(b);
    }

    protected void ASPxGridView1_DataBound(object sender, EventArgs e)
    {

        //ASPxGridView grid = sender as ASPxGridView;

        //RadioButton RadioButton1 = (RadioButton)grid.FindControl("RadioButton1");

        //RadioButton1.GroupName = "aa";
        //for (int i = 0; i < grid.Columns.Count; i++)
        //{
        //    if (grid.Columns[i] is GridViewDataTextColumn)
        //    {
        //        grid.Columns[
        //        (grid.Columns[i] as GridViewDataTextColumn).fiNewButton.Visible = false;
        //        break;
        //    }

        //}
    }
    protected void ASPxGridView1_DataBinding(object sender, EventArgs e)
    {

    }

    protected void ASPxGridView1_PageIndexChanged(object sender, EventArgs e)
    {
        DataTable dtResult = (DataTable)(Session["dtResult"]);
        ASPxGridView1.DataSource = dtResult;
        ASPxGridView1.DataBind();
    }
    protected void ASPxGridView1_CustomUnboundColumnData(object sender, ASPxGridViewColumnDataEventArgs e)
    {
        //if (e.Column.FieldName == "Total")
        //{
        //    decimal price = (decimal)e.GetListSourceFieldValue("UnitPrice");
        //    int quantity = Convert.ToInt32(e.GetListSourceFieldValue("Quantity"));
        //    e.Value = price * quantity;
        //}

    }

    protected void ASPxGridView1_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
    {
        int index = -1;
        if (int.TryParse(e.Parameters, out index))
            ASPxGridView1.SettingsEditing.Mode = (GridViewEditingMode)index;
    }
    protected void AccessDataSource1_Modifying(object sender, SqlDataSourceCommandEventArgs e)
    {
        int index = -1;

        ASPxGridView1.SettingsEditing.Mode = (GridViewEditingMode)index;
    }


    protected void ASPxGridView1_RowUpdating(object sender, ASPxDataUpdatingEventArgs e)
    {
        DataTable dtResult = (DataTable)(Session["dtResult"]);

        DataColumn[] dc = new DataColumn[1];
        dc[0] = dtResult.Columns["賠償項目"];
        dtResult.PrimaryKey = dc;

        DataRow NewRow = dtResult.Rows.Find(e.Keys["賠償項目"]);


        //NewRow["賠償項目"] = "電池" + StringUtil.CStr(i);
        //NewRow["金額"] = "1200";

        NewRow["UnitPrice"] = e.NewValues["UnitPrice"];
        NewRow["Quantity"] = e.NewValues["Quantity"];
        NewRow["Total"] = e.NewValues["Total"];
        //NewRow.AcceptChanges();
        //dtResult.AcceptChanges();



        ASPxGridView1.DataSource = dtResult;
        ASPxGridView1.DataBind();
        Session["dtResult"] = dtResult;
        e.Cancel = true;
        ASPxGridView1.CancelEdit();

    }

    protected void ASPxGridView1_RowInserting(object sender, ASPxDataInsertingEventArgs e)
    {
        DataTable dtResult = (DataTable)(Session["dtResult"]);



        DataRow NewRow = dtResult.NewRow();
        NewRow["賠償項目"] = e.NewValues["賠償項目"];
        NewRow["金額"] = e.NewValues["金額"];

        NewRow["UnitPrice"] = e.NewValues["UnitPrice"];
        NewRow["Quantity"] = e.NewValues["Quantity"];
        NewRow["Total"] = e.NewValues["Total"];

        dtResult.Rows.Add(NewRow);


        ASPxGridView1.DataSource = dtResult;
        ASPxGridView1.DataBind();
        Session["dtResult"] = dtResult;
        e.Cancel = true;
        ASPxGridView1.CancelEdit();
    }
    protected void ASPxGridView1_RowDeleting(object sender, ASPxDataDeletingEventArgs e)
    {
        DataTable dtResult = (DataTable)(Session["dtResult"]);

        DataColumn[] dc = new DataColumn[1];
        dc[0] = dtResult.Columns["賠償項目"];
        dtResult.PrimaryKey = dc;

        DataRow NewRow = dtResult.Rows.Find(e.Keys["賠償項目"]);
        dtResult.Rows.Remove(NewRow);



        ASPxGridView1.DataSource = dtResult;
        ASPxGridView1.DataBind();
        Session["dtResult"] = dtResult;
        e.Cancel = true;
        ASPxGridView1.CancelEdit();
    }
}
