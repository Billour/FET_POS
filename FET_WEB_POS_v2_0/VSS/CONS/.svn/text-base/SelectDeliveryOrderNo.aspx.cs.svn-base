using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class VSS_CONS_SelectDeliveryOrderNo : Popup//System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            BindData();
        }
    }

    private void BindData()
    {
        DataTable dt = new DataTable();
        dt.Columns.Clear();
        dt.Columns.Add("出貨編號", typeof(string));

        int no = 123456789;

        Random rnd = new Random();

        for (int i = 1; i <= 5; i++)
        {
            DataRow r = dt.NewRow();
            r["出貨編號"] = no++; //rnd.Next(1000000, 9999999).ToString();           
            dt.Rows.Add(r);
        }
        GridView1.DataSource = dt;
        GridView1.DataBind();
    }


    protected void btnOk_Click(object sender, EventArgs e)
    {
        foreach (GridViewRow r in GridView1.Rows)
        {
            RadioButton rb = r.FindControl("RadioButton1") as RadioButton;
            if (rb.Checked)
            {
                SetReturnValue(r.Cells[1].Text);
                return;
            }
        }
    }
}
