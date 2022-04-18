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

public partial class VSS_DIS_DIS01_Store_Import : Popup
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void BindData()
    {
        gvStore.DataSource = GetStoreData();
        gvStore.DataBind();



    }

    private DataTable GetEmptyDataTable2()
    {
        DataTable dtResult = new DataTable();
        dtResult.Columns.Add("門市編號", typeof(string));
        dtResult.Columns.Add("門市名稱", typeof(string));
        dtResult.Columns.Add("區域別", typeof(string));
        dtResult.Columns.Add("折扣上限次數", typeof(string));
        dtResult.Columns.Add("異常原因", typeof(string));
        return dtResult;
    }

    private DataTable GetStoreData()
    {
        DataTable dtResult = GetEmptyDataTable2();
        string[] ary1 = { "遠企", "華納", "西門", "天母", "忠孝" };
        string[] ary2 = { "門市代碼不存在", "" };

        for (int i = 1; i <= 5; i++)
        {
            DataRow NewRow = dtResult.NewRow();
            NewRow["門市編號"] = "210" + i;
            NewRow["門市名稱"] = ary1[i % 5];
            NewRow["區域別"] = "北一區";
            NewRow["折扣上限次數"] = "";
            NewRow["異常原因"] = ary2[i % 2];
            dtResult.Rows.Add(NewRow);
        }

        

        return dtResult;
    }

    protected void btnImport_Click(object sender, EventArgs e)
    {
        BindData();
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
