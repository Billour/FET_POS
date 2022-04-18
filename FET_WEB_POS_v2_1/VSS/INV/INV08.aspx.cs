using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using DevExpress.Web.ASPxGridView;
using DevExpress.Web.ASPxPager;
using DevExpress.Web.ASPxEditors;

public partial class VSS_INV_INV08 : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        bindMasterData();
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {

        Session["DataSource"] = getMasterData();
        //bindMasterData();
        //繫結主要的資料表
        gvMaster.DataSource = getMasterData();
        gvMaster.DataBind(); 
    }

    protected void bindMasterData()
    {
        DataTable dtResult = new DataTable();
        dtResult = getMasterData();
        ViewState["gvMaster"] = dtResult;
        gvMaster.DataSource = dtResult;
        gvMaster.DataBind();
    }
    private DataTable getMasterData()
    {
        DataTable dtResult = new DataTable();
        dtResult.Columns.Add("PO/OE_NO", typeof(string));
        dtResult.Columns.Add("訂單編號", typeof(string));
        dtResult.Columns.Add("驗收單編號", typeof(string));
        dtResult.Columns.Add("門市編號", typeof(string));
        dtResult.Columns.Add("門市名稱", typeof(string));
        dtResult.Columns.Add("訂單狀態", typeof(string));
        dtResult.Columns.Add("驗收日期", typeof(string));
        dtResult.Columns.Add("人員", typeof(string));
        dtResult.Columns.Add("日期", typeof(string));

        string[] array1 = { "未驗收", "部份驗收", "已結案" };
        DataRow NewRow = dtResult.NewRow();
        NewRow["PO/OE_NO"] = "001";
        NewRow["訂單編號"] = "HR1007001";
        NewRow["驗收單編號"] = "";
        NewRow["門市編號"] = "2103";
        NewRow["門市名稱"] = "永和";
        NewRow["訂單狀態"] = "未驗收";
        NewRow["驗收日期"] = "";
        NewRow["人員"] = "";
        NewRow["日期"] = "";
        dtResult.Rows.Add(NewRow);

        NewRow = dtResult.NewRow();
        NewRow["PO/OE_NO"] = "001-1";
        NewRow["訂單編號"] = "HR1007002";
        NewRow["驗收單編號"] = "SR2104-1007001-1";
        NewRow["門市編號"] = "2104";
        NewRow["門市名稱"] = "永和";
        NewRow["訂單狀態"] = "部份驗收";
        NewRow["驗收日期"] = "2010/07/01";
        NewRow["人員"] = "12345-王曉明";
        NewRow["日期"] = "2010/07/01";
        dtResult.Rows.Add(NewRow);
        
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
        
        if (e.RowType == GridViewRowType.Data)
        {
            //string S = e.Row.Cells[5].Text;
            DataRowView row = gvMaster.GetRow(e.VisibleIndex) as DataRowView;
            string s = row["訂單狀態"].ToString();
            if (s != "已結案")
            {
               
                e.Row.Style.Add(HtmlTextWriterStyle.Color, "red");
            }

            ASPxHyperLink link = (ASPxHyperLink)gvMaster.FindRowCellTemplateControl(e.VisibleIndex, (GridViewDataColumn)gvMaster.Columns[0], "ASPxHyperLink1");
            link.NavigateUrl = "~/VSS/INV/INV09.aspx?PO/OE_NO=" + e.GetValue("PO/OE_NO").ToString() + "&ReceivingNo=" + e.GetValue("驗收單編號").ToString();
            link.Text = e.GetValue("PO/OE_NO").ToString();

            ASPxHyperLink lin1 = (ASPxHyperLink)gvMaster.FindRowCellTemplateControl(e.VisibleIndex, (GridViewDataColumn)gvMaster.Columns[2], "ASPxHyperLink2");
            lin1.NavigateUrl = "~/VSS/INV/INV09.aspx?ReceivingNo=" + e.GetValue("驗收單編號").ToString() + "&PO/OE_NO=" + e.GetValue("PO/OE_NO").ToString();
            lin1.Text = e.GetValue("驗收單編號").ToString();
        }
        
    }

    protected void gvMaster_CustomUnboundColumnData(object sender, ASPxGridViewColumnDataEventArgs e)
    {
        //if (e.Column.FieldName == "Total")
        //{
        //    decimal price = (decimal)e.GetListSourceFieldValue("UnitPrice");
        //    int quantity = Convert.ToInt32(e.GetListSourceFieldValue("Quantity"));
        //    e.Value = price * quantity;
        //}
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

        if (e.RowType == GridViewRowType.Data)
        {
            //~/VSS/INV/INV09.aspx?PO/OE_NO={0}
             
           
        }
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
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (e.Row.Cells[5].Text != "已結案")
            {
                for (int i = 1; i <= e.Row.Cells.Count - 1; i++)
                {
                    e.Row.Cells[i].ForeColor = System.Drawing.Color.Red;
                }
            }
        }
    }
}
