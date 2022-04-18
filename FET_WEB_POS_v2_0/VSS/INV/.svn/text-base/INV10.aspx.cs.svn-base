using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Drawing;
using DevExpress.Web.ASPxGridView;
using DevExpress.Web.ASPxPager;

public partial class VSS_INV10_INV10 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack && !IsCallback)
        {
            // 繫結空的資料表，以顯示表頭欄位
            bindMasterData();
            //gvMaster.DataSource = GetEmptyDataTable();
            //gvMaster.DataBind();
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
        ViewState["gvMaster"] = dtResult;
        gvMaster.DataSource = dtResult;
        gvMaster.DataBind();
    }

    private DataTable GetEmptyDataTable()
    {
        DataTable dtResult = new DataTable();
        dtResult.Columns.Add("盤點單號", typeof(string));
        dtResult.Columns.Add("盤點日期", typeof(string));
        dtResult.Columns.Add("盤點類型", typeof(string));
        dtResult.Columns.Add("盤點狀態", typeof(string));
        dtResult.Columns.Add("盤點人員", typeof(string));
        dtResult.Columns.Add("更新人員", typeof(string));
        dtResult.Columns.Add("更新日期", typeof(string));
         return dtResult;
       

    }
    private DataTable getMasterData()
    {
        DataTable dtResult = GetEmptyDataTable();
        string[] array1 = { "已盤點","已盤點","已盤點","已盤點","未盤點"};
        for (int i = 1; i <= 4; i++)
        {
            DataRow NewRow = dtResult.NewRow();
            NewRow["盤點單號"] = "SC2013-1007001";
            NewRow["盤點日期"] = DateTime.Today.ToString("yyyy/MM/dd");
            NewRow["盤點類型"] = "重盤";
            NewRow["盤點狀態"] = "未盤點";
            NewRow["盤點人員"] = "王小明";
            NewRow["更新人員"] = "王小明";
            NewRow["更新日期"] = DateTime.Today.ToString("yyyy/MM/dd");
            dtResult.Rows.Add(NewRow);
        }
        return dtResult;
    }

    protected void gvMaster_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
    {
        //GridPageSize = int.Parse(e.Parameters);
        gvMaster.SettingsPager.PageSize = int.Parse(e.Parameters);
        gvMaster.DataBind();
    }

    /// <summary>
    /// The HtmlRowPrepared event is raised for each grid row (data row, group row, etc.) 
    /// within the ASPxGridView. 
    /// You can handle this event to change the style settings of individual rows.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gvMaster_HtmlRowPrepared(object sender, ASPxGridViewTableRowEventArgs e)
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

    protected void gvMaster_HtmlRowCreated(object sender, ASPxGridViewTableRowEventArgs e)
    {
        //if (e.RowType == GridViewRowType.Detail)
        //{
        //    // 繫結明細資料表           
        //    ASPxGridView detailGrid = e.Row.FindChildControl<ASPxGridView>("detailGrid");
        //    detailGrid.DataSource = GetDetailData();
        //    detailGrid.DataBind();
        //}
    }

    /// <summary>
    /// 主GridView的分頁變更事件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gvMaster_PageIndexChanged(object sender, EventArgs e)
    {
        ASPxGridView gvMaster = sender as ASPxGridView;
        gvMaster.DataSource = getMasterData();
        gvMaster.DataBind();
    }


   
    protected void gvMaster_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowIndex != -1)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (e.Row.Cells[3].Text == "已盤點")
                {
                    for (int i =1; i < e.Row.Cells.Count;i++)
                    e.Row.Cells[i].ForeColor = Color.Red;
                }
            }
        }
    }
}
