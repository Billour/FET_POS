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

public partial class VSS_DIS_DIS01_Customer_Import : Popup
{
    protected void Page_Load(object sender, EventArgs e)
    {
     
    }

    protected void BindData()
    {
        gvProduct.DataSource = GetProductData();
        gvProduct.DataBind();


    }

    private DataTable GetEmptyDataTable1()
    {
        DataTable dtResult = new DataTable();
        dtResult.Columns.Add("客戶門號", typeof(string));

        dtResult.Columns.Add("異常原因", typeof(string));
        return dtResult;
    }

    private DataTable GetProductData()
    {
        DataTable dtResult = GetEmptyDataTable1();

        DataRow NewRow = dtResult.NewRow();
        NewRow["客戶門號"] = "0955777444";

        NewRow["異常原因"] = "";
        dtResult.Rows.Add(NewRow);

        NewRow = dtResult.NewRow();
        NewRow["客戶門號"] = "1234567890";

        NewRow["異常原因"] = "客戶門號不正確";
        dtResult.Rows.Add(NewRow);

        NewRow = dtResult.NewRow();
        NewRow["客戶門號"] = "098485893333";

        NewRow["異常原因"] = "門市門號長";
        dtResult.Rows.Add(NewRow);


        NewRow = dtResult.NewRow();
        NewRow["客戶門號"] = "09876543";

        NewRow["異常原因"] = "";
        dtResult.Rows.Add(NewRow);

        return dtResult;
    }

    protected void btnImport_Click(object sender, EventArgs e)
    {
        BindData();
        btnCommit.Visible = true;
        btnCancel.Visible = true;
    }

    protected void btnCommit_Click(object sender, EventArgs e)
    {

    }

    protected void gvProduct_HtmlRowPrepared(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewTableRowEventArgs e)
    {
        if (e.RowType != GridViewRowType.Data) return;
        if (e.GetValue("異常原因").ToString() != string.Empty)
            e.Row.ForeColor = System.Drawing.Color.Red;

    }

}
