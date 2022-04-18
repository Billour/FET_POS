using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using DevExpress.Web.ASPxGridView;

public partial class VSS_LOG_SearchEmpNum : Popup
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            gvMaster.DataSource = new DataTable();
            gvMaster.DataBind();
        }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        bindMasterData();
    }

    protected void bindMasterData()
    {
        DataTable dtResult = new DataTable();
        dtResult = getMasterData();
        //ViewState["gvMaster"] = dtResult;
        gvMaster.DataSource = dtResult;
        gvMaster.DataBind();
    }
    private DataTable getMasterData()
    {
        DataTable dtResult = new DataTable();
        dtResult.Columns.Add("員工編號", typeof(string));
        dtResult.Columns.Add("員工姓名", typeof(string));
        dtResult.Columns.Add("部門", typeof(string));


        for (int i = 0; i < 9; i++)
        {
            DataRow NewRow = dtResult.NewRow();
            NewRow["員工編號"] = "A0000" + i;
            NewRow["員工姓名"] = "員工姓名" + i;
            NewRow["部門"] = "部門" + i;

            dtResult.Rows.Add(NewRow);
        }
        return dtResult;
    }

    protected void btnOk_Click(object sender, EventArgs e)
    {
        /*
        foreach (GridViewRow r in gvMaster.Rows)
        {
            RadioButton rb = r.FindControl("RadioButton1") as RadioButton;
            if (rb.Checked)
            {
                ReturnValueToOpener(r.Cells[1].Text);
                return;
            }
        }
        */

        if (gvMaster.FocusedRowIndex > -1)
        {
            object key = gvMaster.GetRowValues(gvMaster.FocusedRowIndex, gvMaster.KeyFieldName);
            SetReturnValue(key.ToString());
        }
        else
        {
            SetReturnValue(string.Empty);
        }

        //SetReturnValue("OK");
    }
    //#region 分頁相關 (共用)
    //protected void GridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
    //{
    //    //此函式可共用
    //    GridView gridview = sender as GridView;
    //    gridview.PageIndex = e.NewPageIndex;

    //    DataTable dt = ViewState[gridview.ID] as DataTable ?? null;//GridView的資料要存於ViewState以方便共用此Function
    //    gridview.DataSource = dt;
    //    gridview.DataBind();
    //}
    //protected void btnGoToIndex_Click(object sender, EventArgs e)
    //{
    //    //此函式可共用
    //    GridView gridview = (sender as Button).Parent.Parent.Parent.Parent as GridView;
    //    TextBox textbox = (sender as Button).Parent.FindControl("tbGoToIndex") as TextBox;
    //    string strIndex = textbox.Text.Trim();
    //    int index = 0;
    //    if (int.TryParse(strIndex, out index))
    //    {
    //        index = index - 1;
    //        if (index >= 0 && index <= gridview.PageCount - 1)
    //        {
    //            gridview.PageIndex = index;
    //            DataTable dt = ViewState[gridview.ID] as DataTable ?? null;//GridView的資料要存於ViewState以方便共用此Function
    //            gridview.DataSource = dt;
    //            gridview.DataBind();
    //        }
    //        else
    //        {
    //            textbox.Text = string.Empty;
    //        }
    //    }
    //    else
    //    {
    //        textbox.Text = string.Empty;
    //    }
    //}
    //#endregion
    protected void gvMaster_SelectedIndexChanged(object sender, EventArgs e)
    {
        List<object> keys = gvMaster.GetSelectedFieldValues("員工編號");
        int i = gvMaster.FocusedRowIndex;
    }
    protected void gvMaster_PageIndexChanged(object sender, EventArgs e)
    {
        ASPxGridView grid = sender as ASPxGridView;
        grid.DataSource = getMasterData();
        grid.DataBind();
    }
}
