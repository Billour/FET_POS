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

public partial class VSS_DIS_DIS01_Permissions_Import : Popup
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
        dtResult.Columns.Add("促銷代號", typeof(string));
        dtResult.Columns.Add("促銷名稱", typeof(string));
        dtResult.Columns.Add("異常原因", typeof(string));
        return dtResult;
    }

    private DataTable GetProductData()
    {
        DataTable dtResult = GetEmptyDataTable1();

        DataRow NewRow = dtResult.NewRow();
        NewRow["促銷代號"] = "HG093FJ8VEE";
        NewRow["促銷名稱"] = "促銷名稱1";
        NewRow["異常原因"] = "";
        dtResult.Rows.Add(NewRow);

        NewRow = dtResult.NewRow();
        NewRow["促銷代號"] = "9D0F93JFID";
        NewRow["促銷名稱"] = "促銷名稱2";
        NewRow["異常原因"] = "";
        dtResult.Rows.Add(NewRow);

        NewRow = dtResult.NewRow();
        NewRow["促銷代號"] = "9DUFUVVVVV";
        NewRow["促銷名稱"] = "促銷名稱3";
        NewRow["異常原因"] = "商品代號不正確";
        dtResult.Rows.Add(NewRow);


        NewRow = dtResult.NewRow();
        NewRow["促銷代號"] = "100100251";
        NewRow["促銷名稱"] = "";
        NewRow["異常原因"] = "促銷名稱不存在";
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
