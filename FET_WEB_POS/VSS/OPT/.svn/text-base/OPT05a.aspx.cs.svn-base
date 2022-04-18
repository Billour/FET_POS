using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class VSS_OPT_OPT05a : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            gvMaster.DataSource = GetEmptyDataTable();
            gvMaster.DataBind();
        }
    }
    protected void bindMasterData(int TempCount)
    {
        DataTable dtGvMaster = new DataTable();
        dtGvMaster = getMasterData(TempCount);
        ViewState["gvMaster"] = dtGvMaster;
        gvMaster.DataSource = dtGvMaster;
        gvMaster.DataBind();
    }
    protected void bindDetailData()
    {
        DataTable dtgvDetail = new DataTable();
        dtgvDetail = getDetailData();
        ViewState["gvDetail"] = dtgvDetail;
        gvDetail.DataSource = dtgvDetail;
        gvDetail.DataBind();
    }
    private DataTable GetEmptyDataTable()
    {
        DataTable dtResult = new DataTable();
        dtResult.Columns.Add("項次", typeof(string));
        dtResult.Columns.Add("門市編號", typeof(string));
        dtResult.Columns.Add("門市名稱", typeof(string));
        dtResult.Columns.Add("用途", typeof(string));
        dtResult.Columns.Add("所屬年月起", typeof(string));
        dtResult.Columns.Add("所屬年月訖", typeof(string));
        dtResult.Columns.Add("字軌", typeof(string));
        dtResult.Columns.Add("起始編號", typeof(string));
        dtResult.Columns.Add("終止編號", typeof(string));
        dtResult.Columns.Add("目前編號", typeof(string));
        dtResult.Columns.Add("發票張數", typeof(string));
        dtResult.Columns.Add("更新人員", typeof(string));
        dtResult.Columns.Add("更新日期", typeof(string));
        return dtResult;
    }

    private DataTable getMasterData(int TempCount)
    {
        DataTable dtMaster = new DataTable();
        dtMaster.Columns.Clear();
        dtMaster.Columns.Add("項次", typeof(string));
        dtMaster.Columns.Add("門市編號", typeof(string));
        dtMaster.Columns.Add("門市名稱", typeof(string));
        dtMaster.Columns.Add("用途", typeof(string));
        dtMaster.Columns.Add("所屬年月(起)", typeof(string));
        dtMaster.Columns.Add("所屬年月(訖)", typeof(string));
        dtMaster.Columns.Add("字軌", typeof(string));
        dtMaster.Columns.Add("起始編號", typeof(string));
        dtMaster.Columns.Add("終止編號", typeof(string));
        dtMaster.Columns.Add("目前編號", typeof(string));
        dtMaster.Columns.Add("發票張數", typeof(string));
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
        dtMasterRow["目前編號"] = "QK00000001";
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
        dtMasterRow["目前編號"] = "QK00000501";
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
        dtMasterRow["目前編號"] = "QY00001001";
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
        dtMasterRow["目前編號"] = "QY00001501";
        dtMasterRow["發票張數"] = "500";
        dtMasterRow["更新日期"] = "2010/09/01";
        dtMasterRow["更新人員"] = "王小明";
        dtMaster.Rows.Add(dtMasterRow);
        return dtMaster;
    }

    private DataTable getDetailData()
    {
        DataTable gvDetail = new DataTable();
        gvDetail.Columns.Clear();
        gvDetail.Columns.Add("項次", typeof(string));
        gvDetail.Columns.Add("機台號碼", typeof(string));
        gvDetail.Columns.Add("起始編號", typeof(string));
        gvDetail.Columns.Add("終止編號", typeof(string));
        gvDetail.Columns.Add("目前編號", typeof(string));
        gvDetail.Columns.Add("張數", typeof(string));
        gvDetail.Columns.Add("發票分配日期", typeof(string));



        DataRow gvDetailRow = gvDetail.NewRow();
        gvDetailRow["項次"] = "1";
        gvDetailRow["機台號碼"] = "1";
        gvDetailRow["起始編號"] = "00000001";
        gvDetailRow["終止編號"] = "00000100";
        gvDetailRow["目前編號"] = "QK00000001";
        gvDetailRow["張數"] = 100;
        gvDetailRow["發票分配日期"] = "2010/07/01";
        gvDetail.Rows.Add(gvDetailRow);


        gvDetailRow = gvDetail.NewRow();
        gvDetailRow["項次"] = "2";
        gvDetailRow["機台號碼"] = "2";
        gvDetailRow["起始編號"] = "00000101";
        gvDetailRow["終止編號"] = "00000200";
        gvDetailRow["目前編號"] = "QK00000101";
        gvDetailRow["張數"] = "100";
        gvDetailRow["發票分配日期"] = "2010/07/01";
        gvDetail.Rows.Add(gvDetailRow);

        gvDetailRow = gvDetail.NewRow();
        gvDetailRow["項次"] = "3";
        gvDetailRow["機台號碼"] = "3";
        gvDetailRow["起始編號"] = "00000201";
        gvDetailRow["終止編號"] = "00000300";
        gvDetailRow["目前編號"] = "QK00000201";
        gvDetailRow["張數"] = "100";
        gvDetailRow["發票分配日期"] = "2010/07/01";
        gvDetail.Rows.Add(gvDetailRow);

        gvDetailRow = gvDetail.NewRow();
        gvDetailRow["項次"] = "4";
        gvDetailRow["機台號碼"] = "4";
        gvDetailRow["起始編號"] = "00000301";
        gvDetailRow["終止編號"] = "00000400";
        gvDetailRow["目前編號"] = "QK00000301";
        gvDetailRow["張數"] = "100";
        gvDetailRow["發票分配日期"] = "2010/07/01";
        gvDetail.Rows.Add(gvDetailRow);

        gvDetailRow = gvDetail.NewRow();
        gvDetailRow["項次"] = "5";
        gvDetailRow["機台號碼"] = "5";
        gvDetailRow["起始編號"] = "00000401";
        gvDetailRow["終止編號"] = "00000500";
        gvDetailRow["目前編號"] = "QK00004001";
        gvDetailRow["張數"] = "100";
        gvDetailRow["發票分配日期"] = "2010/07/01";
        gvDetail.Rows.Add(gvDetailRow);

        return gvDetail;
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        bindMasterData(6);
        //btnNew.Visible = true;
        //btnDelete.Visible = true;
        //btnDelete0.Visible = true;
        Div1.Visible = true;
    }


    protected void gvMaster_RowEditing(object sender, GridViewEditEventArgs e)
    {
        GridView gridview = sender as GridView;
        //設定編輯欄位
        gridview.EditIndex = e.NewEditIndex;
        //Bind原查詢資料
        DataTable dt = ViewState["gvMaster"] as DataTable;
        gridview.DataSource = dt;
        gridview.DataBind();
    }
    protected void gvMaster_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        GridView gridview = sender as GridView;

        //取得資料

        //更新資料庫

        //取消編輯狀態
        gridview.EditIndex = -1;

        //Bind新資料(重取資料)
        bindMasterData(6);
    }
    protected void gvMaster_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        GridView gridview = sender as GridView;
        gridview.EditIndex = -1;
        //Bind原查詢資料
        DataTable dt = ViewState["gvMaster"] as DataTable;
        gridview.DataSource = dt;
        gridview.DataBind();
    }

    protected void gvMaster_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "select")
        {
            bindDetailData();
            this.gvDetail.Visible = true;
            this.showFooterBtn.Visible = true;
            //Button1.Visible = Button2.Visible = true;
        }
    }
    protected void gvDetail_RowEditing(object sender, GridViewEditEventArgs e)
    {
        GridView grDetail = sender as GridView;
        //設定編輯欄位
        grDetail.EditIndex = e.NewEditIndex;
        //Bind原查詢資料
        DataTable dt = ViewState["gvDetail"] as DataTable;
        grDetail.DataSource = dt;
        grDetail.DataBind();
    }
    protected void gvDetail_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        GridView grDetail = sender as GridView;
        grDetail.EditIndex = -1;
        //Bind原查詢資料
        DataTable dt = ViewState["gvDetail"] as DataTable;
        grDetail.DataSource = dt;
        grDetail.DataBind();
    }
    protected void gvDetail_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        GridView grDetail = sender as GridView;

        //取得資料

        //更新資料庫

        //取消編輯狀態
        grDetail.EditIndex = -1;

        //Bind新資料(重取資料)
        bindDetailData();
    }
    //protected void gvMaster_RowDataBound(object sender, GridViewRowEventArgs e)
    //{
    //    DataRowView view = e.Row.DataItem as DataRowView;
    //    if (view != null)
    //    {

    //        if (e.Row.RowType == DataControlRowType.DataRow)
    //        {
    //            if (e.Row.RowIndex != -1)
    //            {
    //                if (view.Row["狀態"].ToString() == "連線")
    //                {
    //                    if (e.Row.FindControl("btnLink") != null)
    //                    {
    //                        Button btnLink = (Button)(e.Row.FindControl("btnLink"));
    //                        btnLink.Enabled = false;
    //                    }

    //                }
    //                else
    //                {
    //                    if (e.Row.FindControl("btnLink") != null)
    //                    {
    //                        Button btnLink = (Button)(e.Row.FindControl("btnLink"));
    //                        btnLink.Enabled = true;
    //                    }
    //                }
    //            }
    //        }

    //    }
    //}
    #region 分頁相關 (共用)
    protected void GridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
       //此函式可共用
       GridView gridview = sender as GridView;
       gridview.PageIndex = e.NewPageIndex;

       DataTable dt = ViewState[gridview.ID] as DataTable ?? null;//GridView的資料要存於ViewState以方便共用此Function
       gridview.DataSource = dt;
       gridview.DataBind();
    }
    protected void btnGoToIndex_Click(object sender, EventArgs e)
    {
       //此函式可共用
       GridView gridview = (sender as Button).Parent.Parent.Parent.Parent as GridView;
       TextBox textbox = (sender as Button).Parent.FindControl("tbGoToIndex") as TextBox;
       string strIndex = textbox.Text.Trim();
       int index = 0;
       if (int.TryParse(strIndex, out index))
       {
          index = index - 1;
          if (index >= 0 && index <= gridview.PageCount - 1)
          {
             gridview.PageIndex = index;
             DataTable dt = ViewState[gridview.ID] as DataTable ?? null;//GridView的資料要存於ViewState以方便共用此Function
             gridview.DataSource = dt;
             gridview.DataBind();
          }
          else
          {
             textbox.Text = string.Empty;
          }
       }
       else
       {
          textbox.Text = string.Empty;
       }
    }
    #endregion

}
