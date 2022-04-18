using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using DevExpress.Web.ASPxGridView;
using DevExpress.Web.ASPxPager;

public partial class VSS_INV06_INV06 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            // 繫結空的資料表，以顯示表頭欄位
            //gvMaster.DataSource = GetEmptyDataTable();
            //gvMaster.DataBind();
            bindMasterData();
        }
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
        dtResult.Columns.Add("退倉單號", typeof(string));
        dtResult.Columns.Add("退倉開始日", typeof(string));
        dtResult.Columns.Add("退倉結束日", typeof(string));
        dtResult.Columns.Add("退倉日", typeof(string));
        dtResult.Columns.Add("退倉狀態", typeof(string));
        dtResult.Columns.Add("更新人員", typeof(string));
        dtResult.Columns.Add("更新日期", typeof(string));
         return dtResult;
        }

    private DataTable getMasterData()
    {
        DataTable dtResult = GetEmptyDataTable();
        for (int i = 1; i <= 4; i++)
        {
            DataRow NewRow = dtResult.NewRow();
            NewRow["退倉單號"] = "HR100818" + i.ToString("000");
            NewRow["退倉開始日"] = "2010/07/01";
            NewRow["退倉結束日"] = "2010/10/10";
            NewRow["退倉日"] = "";
            NewRow["退倉狀態"] = "未完成";
            NewRow["更新人員"] = "";
            //"人員" + i.ToString("00");
            NewRow["更新日期"] = "";
                //"2010/07/10";
            dtResult.Rows.Add(NewRow);
        }
               return dtResult;
    }

    

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        bindMasterData();
        
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

        if (e.RowType == GridViewRowType.Data)
        {
            //string S = e.Row.Cells[5].Text;
            DataRowView row = gvMaster.GetRow(e.VisibleIndex) as DataRowView;
            string s = row["退倉狀態"].ToString();
            if (s == "未完成")
            {

                e.Row.Style.Add(HtmlTextWriterStyle.Color, "red");
            }
        }

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
                if (e.Row.Cells[4].Text == "未完成")
                {
                    for (int i = 1; i < e.Row.Cells.Count; i++)
                    {
                        e.Row.Cells[i].ForeColor = System.Drawing.Color.Red;
                    }

                }
            }
        }
    }
}
