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


public partial class VSS_OPT_OPT05a : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            grid.DataSource = GetEmptyDataTable();
            grid.DataBind();
        }
    }

    private DataTable GetEmptyDataTable()
    {
        DataTable dtResult = new DataTable();
        dtResult.Columns.Add("項次", typeof(int));
        dtResult.Columns.Add("門市編號", typeof(string));
        dtResult.Columns.Add("門市名稱", typeof(string));
        dtResult.Columns.Add("用途", typeof(string));
        dtResult.Columns.Add("所屬年月起", typeof(string));
        dtResult.Columns.Add("所屬年月訖", typeof(string));
        dtResult.Columns.Add("字軌", typeof(string));
        dtResult.Columns.Add("起始編號", typeof(string));
        dtResult.Columns.Add("終止編號", typeof(string));
        dtResult.Columns.Add("已使用編號", typeof(string));
        dtResult.Columns.Add("發票張數", typeof(int));
        dtResult.Columns.Add("更新人員", typeof(string));
        dtResult.Columns.Add("更新日期", typeof(string));
        return dtResult;
    }

    private DataTable GetMasterData()
    {
        DataTable dtMaster = new DataTable();
        dtMaster.Columns.Clear();
        dtMaster.Columns.Add("項次", typeof(int));
        dtMaster.Columns.Add("門市編號", typeof(string));
        dtMaster.Columns.Add("門市名稱", typeof(string));
        dtMaster.Columns.Add("用途", typeof(string));
        dtMaster.Columns.Add("所屬年月(起)", typeof(string));
        dtMaster.Columns.Add("所屬年月(訖)", typeof(string));
        dtMaster.Columns.Add("字軌", typeof(string));
        dtMaster.Columns.Add("起始編號", typeof(string));
        dtMaster.Columns.Add("終止編號", typeof(string));
        dtMaster.Columns.Add("已使用編號", typeof(string));
        dtMaster.Columns.Add("發票張數", typeof(int));
        dtMaster.Columns.Add("更新日期", typeof(string));
        dtMaster.Columns.Add("更新人員", typeof(string));

        string[] array1 = { "有效", "過期", "已停止" };
        string[] array2 = { "現金", "信用卡", "禮券", "金融卡", "HappyGo" };

        DataRow dtMasterRow = dtMaster.NewRow();
        dtMasterRow["項次"] = "1";
        dtMasterRow["門市編號"] = "2101";
        dtMasterRow["門市名稱"] = "遠企";
        dtMasterRow["用途"] = "連線";
        dtMasterRow["所屬年月(起)"] = "2010/07";
        dtMasterRow["所屬年月(訖)"] = "2010/08";
        dtMasterRow["字軌"] = "QK";
        dtMasterRow["起始編號"] = "00000001";
        dtMasterRow["終止編號"] = "00000500";
        dtMasterRow["已使用編號"] = "QK00000001";
        dtMasterRow["發票張數"] = "500";
        dtMasterRow["更新日期"] = "2010/07/01";
        dtMasterRow["更新人員"] = "王小明";
        dtMaster.Rows.Add(dtMasterRow);

        dtMasterRow = dtMaster.NewRow();
        dtMasterRow["項次"] = "2";
        dtMasterRow["門市編號"] = "2101";
        dtMasterRow["門市名稱"] = "遠企";
        dtMasterRow["用途"] = "離線";
        dtMasterRow["所屬年月(起)"] = "2010/07";
        dtMasterRow["所屬年月(訖)"] = "2010/08";
        dtMasterRow["字軌"] = "QK";
        dtMasterRow["起始編號"] = "00000501";
        dtMasterRow["終止編號"] = "00001000";
        dtMasterRow["已使用編號"] = "QK00000501";
        dtMasterRow["發票張數"] = "500";
        dtMasterRow["更新日期"] = "2010/07/01";
        dtMasterRow["更新人員"] = "王小明";
        dtMaster.Rows.Add(dtMasterRow);

        dtMasterRow = dtMaster.NewRow();
        dtMasterRow["項次"] = "3";
        dtMasterRow["門市編號"] = "2101";
        dtMasterRow["門市名稱"] = "遠企";
        dtMasterRow["用途"] = "連線";
        dtMasterRow["所屬年月(起)"] = "2010/09";
        dtMasterRow["所屬年月(訖)"] = "2010/10";
        dtMasterRow["字軌"] = "QY";
        dtMasterRow["起始編號"] = "00001001";
        dtMasterRow["終止編號"] = "00001500";
        dtMasterRow["已使用編號"] = "QY00001001";
        dtMasterRow["發票張數"] = "500";
        dtMasterRow["更新日期"] = "2010/09/01";
        dtMasterRow["更新人員"] = "王小明";
        dtMaster.Rows.Add(dtMasterRow);

        dtMasterRow = dtMaster.NewRow();
        dtMasterRow["項次"] = "4";
        dtMasterRow["門市編號"] = "2101";
        dtMasterRow["門市名稱"] = "遠企";
        dtMasterRow["用途"] = "離線";
        dtMasterRow["所屬年月(起)"] = "2010/09";
        dtMasterRow["所屬年月(訖)"] = "2010/10";
        dtMasterRow["字軌"] = "QY";
        dtMasterRow["起始編號"] = "00001501";
        dtMasterRow["終止編號"] = "00002000";
        dtMasterRow["已使用編號"] = "QY00001501";
        dtMasterRow["發票張數"] = "500";
        dtMasterRow["更新日期"] = "2010/09/01";
        dtMasterRow["更新人員"] = "王小明";
        dtMaster.Rows.Add(dtMasterRow);
        return dtMaster;
    }

    private DataTable GetDetailData()
    {
        DataTable gvDetail = new DataTable();
        gvDetail.Columns.Clear();
        gvDetail.Columns.Add("項次", typeof(int));
        gvDetail.Columns.Add("機台號碼", typeof(string));
        gvDetail.Columns.Add("起始編號", typeof(string));
        gvDetail.Columns.Add("終止編號", typeof(string));
        gvDetail.Columns.Add("已使用編號", typeof(string));
        gvDetail.Columns.Add("張數", typeof(int));
        gvDetail.Columns.Add("發票分配日期", typeof(string));



        DataRow gvDetailRow = gvDetail.NewRow();
        gvDetailRow["項次"] = "1";
        gvDetailRow["機台號碼"] = "1";
        gvDetailRow["起始編號"] = "00000001";
        gvDetailRow["終止編號"] = "00000100";
        gvDetailRow["已使用編號"] = "QK00000001";
        gvDetailRow["張數"] = 100;
        gvDetailRow["發票分配日期"] = "2010/07/01";
        gvDetail.Rows.Add(gvDetailRow);


        gvDetailRow = gvDetail.NewRow();
        gvDetailRow["項次"] = "2";
        gvDetailRow["機台號碼"] = "2";
        gvDetailRow["起始編號"] = "00000101";
        gvDetailRow["終止編號"] = "00000200";
        gvDetailRow["已使用編號"] = "QK00000101";
        gvDetailRow["張數"] = "100";
        gvDetailRow["發票分配日期"] = "2010/07/01";
        gvDetail.Rows.Add(gvDetailRow);

        gvDetailRow = gvDetail.NewRow();
        gvDetailRow["項次"] = "3";
        gvDetailRow["機台號碼"] = "3";
        gvDetailRow["起始編號"] = "00000201";
        gvDetailRow["終止編號"] = "00000300";
        gvDetailRow["已使用編號"] = "QK00000201";
        gvDetailRow["張數"] = "100";
        gvDetailRow["發票分配日期"] = "2010/07/01";
        gvDetail.Rows.Add(gvDetailRow);

        gvDetailRow = gvDetail.NewRow();
        gvDetailRow["項次"] = "4";
        gvDetailRow["機台號碼"] = "4";
        gvDetailRow["起始編號"] = "00000301";
        gvDetailRow["終止編號"] = "00000400";
        gvDetailRow["已使用編號"] = "QK00000301";
        gvDetailRow["張數"] = "100";
        gvDetailRow["發票分配日期"] = "2010/07/01";
        gvDetail.Rows.Add(gvDetailRow);

        gvDetailRow = gvDetail.NewRow();
        gvDetailRow["項次"] = "5";
        gvDetailRow["機台號碼"] = "5";
        gvDetailRow["起始編號"] = "00000401";
        gvDetailRow["終止編號"] = "00000500";
        gvDetailRow["已使用編號"] = "QK00004001";
        gvDetailRow["張數"] = "100";
        gvDetailRow["發票分配日期"] = "2010/07/01";
        gvDetail.Rows.Add(gvDetailRow);

        return gvDetail;
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        grid.DataSource = GetMasterData();
        grid.DataBind();
        //btnNew.Visible = true;
        //btnDelete.Visible = true;
        //btnDelete0.Visible = true;
        //Div1.Visible = true;
    }

    protected void CommandButton_Click(Object sender, CommandEventArgs e)
    {
        //tabPage.Visible = true;
        //grid.Selection.UnselectAll();
        //grid.Selection.SelectRowByKey(2);
        
        showFooterBtn.Visible = true;
        detailGrid.Visible = true;
        detailGrid.DataSource = GetDetailData();
        detailGrid.DataBind();

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
    }

    protected void grid_HtmlRowCreated(object sender, ASPxGridViewTableRowEventArgs e)
    {
        if (e.RowType == GridViewRowType.Detail)
        {
            //aspxGVSelectRow('ctl00_ASPxSplitter1_MainContentPlaceHolder_grid',0,this);" 
            //LinkButton link = grid.FindRowTemplateControl(e.VisibleIndex, "commandButton") as LinkButton;
            //link.OnClientClick = "aspxGVSelectRow('ctl00_ASPxSplitter1_MainContentPlaceHolder_grid',0,this);";

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

    protected void grid_SelectionChanged(object sender, EventArgs e)
    {

    }
    protected void grid_FocusedRowChanged(object sender, EventArgs e)
    {

    }

    protected void grid_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
    {
        ASPxGridView grid = (ASPxGridView)sender;
        grid.CancelEdit();
        e.Cancel = true;
        grid.DataSource = GetMasterData();
        grid.DataBind();

    }

}
