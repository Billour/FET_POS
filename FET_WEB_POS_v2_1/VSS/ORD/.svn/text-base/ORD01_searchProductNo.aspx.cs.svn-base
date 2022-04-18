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

public partial class VSS_ORD_ORD01_searchProductNo : Popup
{

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            grid.DataSource = new DataTable();            
            grid.DataBind();            
        }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        BindData();
        //btnCommit.Visible = true;
        //btnCancel.Visible = true;
    }

    protected void BindData()
    {
        grid.DataSource = GetMasterData();
        grid.DataBind();
    }
    private DataTable GetEmptyDataTable()
    {
        DataTable dtResult = new DataTable();
        dtResult.Columns.Add("商品編號", typeof(string));
        dtResult.Columns.Add("商品名稱", typeof(string));
        //dtResult.Columns.Add("庫存", typeof(string));
        //dtResult.Columns.Add("價格", typeof(string));
        return dtResult;
    }
    private DataTable GetMasterData()
    {
        DataTable dtResult = GetEmptyDataTable();

        for (int i = 0; i <= 10; i++)
        {
            DataRow NewRow = dtResult.NewRow();
            NewRow["商品編號"] = "A0000" + i;
            NewRow["商品名稱"] = "商品名稱" + i;
            //NewRow["庫存"] = i + 3;
            //NewRow["價格"] = 1000 * (i + 1);
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
        /*
        Page.ClientScript.RegisterStartupScript(this.GetType(), "hideProductsPupupControl", @"
            var ifs = window.parent.document.getElementsByTagName(""iframe"");
           for(var i = 0, len = ifs.length; i < len; i++)  {
              var f = ifs[i];
              var fDoc = f.contentDocument || f.contentWindow.document;
              if(fDoc === document)   {
                 alert(f.getAttribute(""id""));
              }
           }


            //window.parent.lblResult.SetText(txtResult.GetText());
            //alert(window.frame[0].popupArguments);
            //window.popupArguments.popupContainer.Hide();
        ", true);
        */
        List<object> keys = grid.GetSelectedFieldValues("商品編號");
        if (keys.Count > 0)
        {
           // SetReturnValue(keys[0].ToString());
        }
        else
        {
           // SetReturnValue(string.Empty);
        }
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
