using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.Web.ASPxGridView;
using System.Data;

public partial class ItemPage : System.Web.UI.UserControl
{

    protected void Page_Load(object sender, EventArgs e)
    {

    }

    #region 移動 按鈕事件
    protected void ASPxButton2_Click(object sender, EventArgs e)
    {
        ASPxGridView gv1 = (ASPxGridView)GridViewPanel1.FindControl("ASPxGridView1");
        ASPxGridView gv2 = (ASPxGridView)GridViewPanel2.FindControl("ASPxGridView1");

        gv1.Selection.SelectAll();
        Session["Source"] = Session["ASPxGridView1"];
        Session["Target"] = Session["ASPxGridView2"];
        MoveSelectedData(gv1, gv2);
    }

    private void MoveSelectedData(ASPxGridView gvSource, ASPxGridView gvTarget)
    {

        DataTable dt1 = (DataTable)Session["Source"]; // gvSource.DataSource;
        DataTable dt2 = (DataTable)Session["Target"]; // gvTarget.DataSource;

        DataRow dr1 = dt1.NewRow();
        DataRow dr2;

        List<object> keyValues = gvSource.GetSelectedFieldValues(gvSource.KeyFieldName);
        foreach (object key in keyValues)
        {
            dr1 = dt1.Rows.Find(key);
            dr2 = dt2.NewRow();

            int ColumnCount = dt1.Columns.Count > dt2.Columns.Count? dt2.Columns.Count: dt1.Columns.Count;
            

            for (int i = 0; i < ColumnCount; i++)
            {
                dr2[i] = dr1[i];
            }

            dt2.Rows.Add(dr2);
            dt1.Rows.Remove(dr1);
        }

        GridViewPanel1.bLeftControl = true;
        GridViewPanel2.bLeftControl = false;

        gvSource.DataSource = dt1;
        gvSource.DataBind();
        gvSource.Selection.UnselectAll();

        gvTarget.DataSource = dt2;
        gvTarget.DataBind();
        gvTarget.Selection.UnselectAll();

    }
    #endregion
    protected void ASPxButton3_Click(object sender, EventArgs e)
    {
        ASPxGridView gv1 = (ASPxGridView)GridViewPanel1.FindControl("ASPxGridView1");
        ASPxGridView gv2 = (ASPxGridView)GridViewPanel2.FindControl("ASPxGridView1");

        Session["Source"] = Session["ASPxGridView1"];
        Session["Target"] = Session["ASPxGridView2"];
        MoveSelectedData(gv1, gv2);
    }
    protected void ASPxButton4_Click(object sender, EventArgs e)
    {
        ASPxGridView gv1 = (ASPxGridView)GridViewPanel1.FindControl("ASPxGridView1");
        ASPxGridView gv2 = (ASPxGridView)GridViewPanel2.FindControl("ASPxGridView1");

        Session["Source"] = Session["ASPxGridView2"];
        Session["Target"] = Session["ASPxGridView1"];
        MoveSelectedData(gv2, gv1);
    }
    protected void ASPxButton5_Click(object sender, EventArgs e)
    {
        ASPxGridView gv1 = (ASPxGridView)GridViewPanel1.FindControl("ASPxGridView1");
        ASPxGridView gv2 = (ASPxGridView)GridViewPanel2.FindControl("ASPxGridView1");

        gv2.Selection.SelectAll();
        Session["Source"] = Session["ASPxGridView2"];
        Session["Target"] = Session["ASPxGridView1"];
        MoveSelectedData(gv2, gv1);
    }
}
