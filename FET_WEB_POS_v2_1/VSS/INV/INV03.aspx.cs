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

public partial class VSS_INV_INV03 : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack && !IsCallback)
        {
            // 繫結空的資料表，以顯示表頭欄位
            gvMaster.DataSource = GetEmptyDataTable();
            gvMaster.DataBind();
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
    //private DataTable getMasterData()
    private DataTable GetEmptyDataTable()
    {
        DataTable dtResult = new DataTable();
        dtResult.Columns.Add("區域別", typeof(string));
        dtResult.Columns.Add("商品類別", typeof(string));
        dtResult.Columns.Add("商品編號", typeof(string));
        dtResult.Columns.Add("商品名稱", typeof(string));
        dtResult.Columns.Add("門市編號", typeof(string));
        dtResult.Columns.Add("門市名稱", typeof(string));
        dtResult.Columns.Add("出貨倉別", typeof(string));
        dtResult.Columns.Add("數量", typeof(string));
        return dtResult;
    }

    private DataTable getMasterData()
    {
        DataTable dtResult = GetEmptyDataTable();
        string[] ary1 = { "銷售倉", "展示倉", "維修倉", "租賃倉"};
        string[] ary2 = { "北一區", "北二區", "中二區", "南一區"};
        string[] ary3 = { "1", "2", "3", "4","5" };

        Random rnd = new Random();

        for (int i = 0; i <= 16; i++)
        {
            DataRow NewRow = dtResult.NewRow();
            NewRow["區域別"] = "北一區";//ary2[i % 4];
            NewRow["商品類別"] = "3G Handset";//ary2[i % 4];
            NewRow["商品編號"] = rnd.Next(10010010, 99010010); //"10010010";
            NewRow["商品名稱"] = "商品名稱" + i;
            NewRow["門市編號"] = "2101";
            NewRow["門市名稱"] = "遠企";
            NewRow["出貨倉別"] = "銷售倉";// ary1[i % 4];
            NewRow["數量"] = rnd.Next(1, 10);
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

    protected void btnSearch_Click(object sender, EventArgs e)
    {
      
        Session["DataSource"] = getMasterData();
        //bindMasterData();
        //繫結主要的資料表
        gvMaster.DataSource = getMasterData();
        gvMaster.DataBind(); 
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




}
