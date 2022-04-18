using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using DevExpress.Web;
using DevExpress.Web.ASPxGridView;
using DevExpress.Web.ASPxPager;

public partial class VSS_SAL_SAL01_chooseStore : Popup
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {            
            // 繫結空的資料表，以顯示表頭欄位
            grid.DataSource = new DataTable();
            grid.DataBind();
        }
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        BindData();
        //btnCommit.Visible = true;
    }
    
    protected void BindData()
    {        
        grid.DataSource = GetMasterData();
        grid.DataBind();
    }
    private DataTable GetMasterData()
    {
        DataTable dtResult = new DataTable();
        dtResult.Columns.Add("門市編號", typeof(string));
        dtResult.Columns.Add("門市名稱", typeof(string));
      

        for (int i = 0; i < 9; i++)
        {
            DataRow NewRow = dtResult.NewRow();
            NewRow["門市編號"] = "A0000" + i;
            NewRow["門市名稱"] = "門市名稱" + i;
        
            dtResult.Rows.Add(NewRow);
        }
        return dtResult;
    }

    protected void btnCommit_Click(object sender, EventArgs e)
    {
        /*
        foreach (GridViewRow r in gvMaster.Rows)
        {
            RadioButton c = r.FindControl("radioChoose") as RadioButton;
            if (c.Checked)
            {
                ReturnValueToOpener(r.Cells[1].Text);
                return;
            }
        }
        */

        SetReturnValue("OK");
        /*
        Page.ClientScript.RegisterStartupScript(this.GetType(), "hideStoresPupupControl", @"
            //window.parent.lblResult.SetText(txtResult.GetText());
            window.parent.storesPopupControl.Hide();
        ", true);
        */

        //ReturnValueToOpener("OK");
    }

    protected void grid_HtmlRowPrepared(object sender, ASPxGridViewTableRowEventArgs e)
    {
        /*
        if (e.RowType == GridViewRowType.Detail)
        {
            // 繫結明細資料表           
            ASPxGridView detailGrid = e.Row.FindChildControl<ASPxGridView>("detailGrid");
            detailGrid.DataSource = GetDetailData();
            detailGrid.DataBind();            
        }
        */
    }

    protected void grid_HtmlRowCreated(object sender, ASPxGridViewTableRowEventArgs e)
    {
        if (e.RowType == GridViewRowType.Detail)
        {
            // 繫結明細資料表           
            ASPxGridView detailGrid = e.Row.FindChildControl<ASPxGridView>("detailGrid");
            // detailGrid.DataSource = GetDetailData();
            detailGrid.DataBind();
        }
    }

    /// <summary>
    /// 主GridView的分頁變更事件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void grid_PageIndexChanged(object sender, EventArgs e)
    {
        ASPxGridView grid = sender as ASPxGridView;
        //grid.DataSource = GetMasterData();
        grid.DataBind();
    }

    /// <summary>
    /// 明細GridView的分頁變更事件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void detailGrid_PageIndexChanged(object sender, EventArgs e)
    {
        ASPxGridView grid = sender as ASPxGridView;
        //grid.DataSource = GetDetailData();
        grid.DataBind();
    }
}
