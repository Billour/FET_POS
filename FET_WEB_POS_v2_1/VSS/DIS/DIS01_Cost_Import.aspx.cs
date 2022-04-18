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

public partial class VSS_DIS_DIS01_Cost_Import : Popup
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
        dtResult.Columns.Add("成本中心", typeof(string));
        dtResult.Columns.Add("商品分類", typeof(string));
        dtResult.Columns.Add("會計科目", typeof(string));
        dtResult.Columns.Add("金額", typeof(string));
        dtResult.Columns.Add("備註", typeof(string));
        dtResult.Columns.Add("異常原因", typeof(string));
        return dtResult;
    }

    private DataTable GetProductData()
    {
        DataTable dtResult = GetEmptyDataTable1();

        string[] ary1 = { "100", "200", "300", "500", "400" };
        string[] ary2 = { "會計科目不正確", "", "成本中心不正確", "商品分類不正確", "" };
        for (int i = 1; i <= 5; i++)
        {

            DataRow NewRow = dtResult.NewRow();
            NewRow["成本中心"] = "成本中心" + i;
            NewRow["商品分類"] = "商品分類" + i;
            NewRow["會計科目"] = "會計科目" + i;
            NewRow["金額"] = ary1[i % 5];
            NewRow["備註"] = "";
            NewRow["異常原因"] = ary2[i % 5];
            dtResult.Rows.Add(NewRow);
        }

      

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
