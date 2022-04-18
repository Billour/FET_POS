using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.Web.ASPxGridView;

public partial class VSS_LEA_LEA08 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void BindData()
    {
        gvMaster.DataSource = GetProductData();
        gvMaster.DataBind();


    }

    private DataTable GetEmptyDataTable1()
    {
        DataTable dtResult = new DataTable();
        dtResult.Columns.Add("產品類別", typeof(string));
        dtResult.Columns.Add("產品名稱", typeof(string));
        dtResult.Columns.Add("手機序號", typeof(string));
        dtResult.Columns.Add("分類", typeof(string));
        return dtResult;
    }

    private DataTable GetProductData()
    {
        DataTable dtResult = GetEmptyDataTable1();

        DataRow NewRow = dtResult.NewRow();
        NewRow["產品類別"] = "手機類";
        NewRow["產品名稱"] = "HTC Desire HD";
        NewRow["手機序號"] = "";
        NewRow["分類"] = "漫遊租賃";
        dtResult.Rows.Add(NewRow);

        NewRow = dtResult.NewRow();
        NewRow["產品類別"] = "其他類";
        NewRow["產品名稱"] = "手機套";
        NewRow["手機序號"] = "";
        NewRow["分類"] = "";
        dtResult.Rows.Add(NewRow);

       
        return dtResult;
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        BindData();
       
    }
}
