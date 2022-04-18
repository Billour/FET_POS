using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Advtek.Utility;
using System.Data;
using DevExpress.Web;
using DevExpress.Web.ASPxGridView;
using DevExpress.Web.ASPxPager;

/// <summary>
/// 主從式(Master-detail) GridView 案例
/// </summary>
public partial class VSS_INV_INV01 : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {        
        if (!IsPostBack && !IsCallback)
        {            
            
        }               
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        Session["DataSource"] = GetMasterData();
        // 繫結主要的資料表
        grid.DataSource = GetMasterData();        
        grid.DataBind();       
    }
    
    private DataTable GetMasterData()
    {
        DataTable dtResult = new DataTable();
        dtResult.Columns.Add("移撥單號", typeof(string));
        dtResult.Columns.Add("移撥狀態", typeof(string));
        dtResult.Columns.Add("移出門市", typeof(string));
        dtResult.Columns.Add("移出日期", typeof(DateTime));
        dtResult.Columns.Add("撥入門市", typeof(string));
        dtResult.Columns.Add("撥入日期", typeof(DateTime));
        dtResult.Columns.Add("更新人員", typeof(string));
        dtResult.Columns.Add("更新日期", typeof(string));

        string[] array0 = { "在途","已撥入" };
        Random rnd = new Random();
        for (int i = 1; i < 17; i++)
        {
           DataRow NewRow = dtResult.NewRow();
           NewRow["移撥單號"] = "ST2013-1007" + rnd.Next(100,999).ToString();
           NewRow["移撥狀態"] = array0[i % 2]; 
           NewRow["移出門市"] = "2103";
           NewRow["移出日期"] = "2010/07/12";
           NewRow["撥入門市"] = "2104";
           if (i % 2 == 1) NewRow["撥入日期"] = "2010/07/12";
           else NewRow["撥入日期"] = DBNull.Value;

           NewRow["更新人員"] = "王小明";
           NewRow["更新日期"] = "2010/07/12";
           dtResult.Rows.Add(NewRow);
        }

        return dtResult;
    }

    private DataTable GetDetailData()
    {
        DataTable gvDetail = new DataTable();
        gvDetail.Columns.Clear();
        gvDetail.Columns.Add("商品料號", typeof(string));
        gvDetail.Columns.Add("商品名稱", typeof(string));
        gvDetail.Columns.Add("IMEI控管", typeof(bool));
        gvDetail.Columns.Add("移出數量", typeof(string));
        gvDetail.Columns.Add("移出IMEI", typeof(string));
        gvDetail.Columns.Add("撥入數量", typeof(string));
        gvDetail.Columns.Add("撥入IMEI", typeof(string));


        for (int i = 1; i < 7; i++)
        {
            DataRow gvDetailRow = gvDetail.NewRow();
            gvDetailRow["商品料號"] = "商品料號" + i;
            gvDetailRow["商品名稱"] = "商品名稱" + i;
            gvDetailRow["IMEI控管"] = true;
            gvDetailRow["移出數量"] = 1;
            gvDetailRow["移出IMEI"] = "7780944056407860";
            gvDetailRow["撥入數量"] = 1;
            gvDetailRow["撥入IMEI"] = "7780944056407860";

            gvDetail.Rows.Add(gvDetailRow);
        }
        return gvDetail;
    }

    protected void Grid_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
    {
        //GridPageSize = int.Parse(e.Parameters);
        grid.SettingsPager.PageSize = int.Parse(e.Parameters);
        grid.DataBind();
    }

    protected void detailGrid_DataSelect(object sender, EventArgs e)
    {
        Session["移撥單號"] = (sender as ASPxGridView).GetMasterRowKeyValue();
    }
    
    /// <summary>
    /// The HtmlRowPrepared event is raised for each grid row (data row, group row, etc.) 
    /// within the ASPxGridView. 
    /// You can handle this event to change the style settings of individual rows.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
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

        if (e.RowType == GridViewRowType.Data)
        {
            if (e.GetValue("移撥狀態").ToString() == "在途")
            {
                e.Row.ForeColor = System.Drawing.Color.Red;
            }
        }
    }

    protected void grid_HtmlRowCreated(object sender, ASPxGridViewTableRowEventArgs e)
    {
        if (e.RowType == GridViewRowType.Detail)
        {
            // 繫結明細資料表           
            ASPxGridView detailGrid = e.Row.FindChildControl<ASPxGridView>("detailGrid");
            detailGrid.DataSource = GetDetailData();
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
        grid.DataSource = GetMasterData();            
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
        grid.DataSource = GetDetailData();        
        grid.DataBind();
    }
    protected void btnXlsExport_Click(object sender, EventArgs e)
    {
       
    }
}
