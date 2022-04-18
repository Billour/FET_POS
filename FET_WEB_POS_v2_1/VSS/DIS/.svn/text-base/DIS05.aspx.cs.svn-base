using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Advtek.Utility;
using System.Collections;
using DevExpress.Web.ASPxGridView;
public partial class VSS_DIS_DIS05 : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack && !IsCallback)
        {
            // 繫結空的資料表
            gvMaster.DataSource = GetEmptyDataTable();
            gvMaster.DataBind();

            bindDetailData1();
        }
    }

    private DataTable GetEmptyDataTable()
    {
        DataTable dtResult = new DataTable();
        dtResult.Columns.Add("促銷代號", typeof(string));
        dtResult.Columns.Add("促銷名稱", typeof(string));
        dtResult.Columns.Add("開始日期", typeof(string));
        dtResult.Columns.Add("結束日期", typeof(string));
        dtResult.Columns.Add("預計上架日", typeof(string));
        dtResult.Columns.Add("商品型態", typeof(string));
        dtResult.Columns.Add("PromotionSubsidy", typeof(int));
        dtResult.Columns.Add("變價規則", typeof(string));
        dtResult.Columns.Add("備註", typeof(string));
        dtResult.Columns.Add("更新人員", typeof(string));
        dtResult.Columns.Add("更新日期", typeof(DateTime));
        return dtResult;
    }

    protected void bindMasterData()
    {
        DataTable dtResult = new DataTable();
        dtResult = getMasterData();
        gvMaster.DataSource = dtResult;
        gvMaster.DataBind();
        ViewState["gvMaster"] = dtResult;
    }
    protected void bindDetailData1()
    {
        DataTable dtResult = new DataTable();
        dtResult = getDetailData1();
        gvDetail1.DataSource = dtResult;
        gvDetail1.DataBind();
    }
    protected void bindDetailData2()
    {
        DataTable dtResult = new DataTable();
        dtResult = getDetailData2();
        gvDetail2.DataSource = dtResult;
        gvDetail2.DataBind();
    }
    protected void bindDetailData3()
    {
        DataTable dtResult = new DataTable();
        dtResult = getDetailData3();
        gvDetail3.DataSource = dtResult;
        gvDetail3.DataBind();
    }


    private DataTable getMasterData()
    {
        DataTable dtResult = GetEmptyDataTable();

        DataRow NewRow = dtResult.NewRow();
        NewRow["促銷代號"] = "ATD00EN";
        NewRow["促銷名稱"] = "3G搶鮮促銷";
        NewRow["開始日期"] = "2006/10/20";
        NewRow["結束日期"] = "";
        NewRow["預計上架日"] = "";
        NewRow["商品型態"] = "指定商品";
        NewRow["PromotionSubsidy"] = 5600;
        NewRow["變價規則"] = "不變價";
        NewRow["備註"] = "";
        NewRow["更新人員"] = "王小明";
        NewRow["更新日期"] = DateTime.Now.ToShortDateString();

        dtResult.Rows.Add(NewRow);

        NewRow = dtResult.NewRow();
        NewRow["促銷代號"] = "DACLTEP";
        NewRow["促銷名稱"] = "續約-1~249";
        NewRow["開始日期"] = "2006/08/05";
        NewRow["結束日期"] = "";
        NewRow["預計上架日"] = "";
        NewRow["商品型態"] = "一般商品";
        NewRow["PromotionSubsidy"] = 3000;
        NewRow["變價規則"] = "變價";
        NewRow["備註"] = "";
        NewRow["更新人員"] = "花小如";
        NewRow["更新日期"] = DateTime.Now.ToShortDateString();
        dtResult.Rows.Add(NewRow);

        return dtResult;
    }
    private DataTable getDetailData1()
    {
        DataTable dtResult = new DataTable();
        dtResult.Columns.Add("2G", typeof(string));
        dtResult.Columns.Add("3G", typeof(string));
        dtResult.Columns.Add("35G", typeof(string));
        dtResult.Columns.Add("Netbook", typeof(string));
        dtResult.Columns.Add("Datacard", typeof(string));
        dtResult.Columns.Add("Other", typeof(string));
        dtResult.Columns.Add("補貼金額", typeof(int));

        DataRow NewRow = dtResult.NewRow();
        NewRow["2G"] = "●";
        NewRow["3G"] = "●";
        NewRow["35G"] = " ";
        NewRow["Netbook"] = " ";
        NewRow["Datacard"] = " ";
        NewRow["Other"] = " ";
        NewRow["補貼金額"] = "2500";
        dtResult.Rows.Add(NewRow);

        NewRow = dtResult.NewRow();
        NewRow["2G"] = "";
        NewRow["3G"] = "";
        NewRow["35G"] = "●";
        NewRow["Netbook"] = " ";
        NewRow["Datacard"] = " ";
        NewRow["Other"] = " ";
        NewRow["補貼金額"] = "1600";
        dtResult.Rows.Add(NewRow);

        NewRow = dtResult.NewRow();
        NewRow["2G"] = " ";
        NewRow["3G"] = " ";
        NewRow["35G"] = " ";
        NewRow["Netbook"] = " ";
        NewRow["Datacard"] = " ";
        NewRow["Other"] = "●";
        NewRow["補貼金額"] = "1500";
        dtResult.Rows.Add(NewRow);

        return dtResult;
    }
    private DataTable getDetailData2()
    {
        DataTable dtResult = new DataTable();
        dtResult.Columns.Add("商品料號", typeof(string));
        dtResult.Columns.Add("商品名稱", typeof(string));
        dtResult.Columns.Add("商品群組", typeof(int));
        dtResult.Columns.Add("促銷價", typeof(int));

        DataRow NewRow = dtResult.NewRow();
        NewRow["商品料號"] = "100100207";
        NewRow["商品名稱"] = "Motorola W220";
        NewRow["商品群組"] = 1;
        NewRow["促銷價"] = 4530;
        dtResult.Rows.Add(NewRow);

        NewRow = dtResult.NewRow();
        NewRow["商品料號"] = "150300037";
        NewRow["商品名稱"] = "Nokia 6288";
        NewRow["商品群組"] = 1;
        NewRow["促銷價"] = 2950;
        dtResult.Rows.Add(NewRow);

        NewRow = dtResult.NewRow();
        NewRow["商品料號"] = "152800003";
        NewRow["商品名稱"] = "Sharp WX-T91";
        NewRow["商品群組"] = 2;
        NewRow["促銷價"] = 29360;
        dtResult.Rows.Add(NewRow);

        NewRow = dtResult.NewRow();
        NewRow["商品料號"] = "152800044";
        NewRow["商品名稱"] = "Sharp WX-T82";
        NewRow["商品群組"] = 2;
        NewRow["促銷價"] = 5260;
        dtResult.Rows.Add(NewRow);

        return dtResult;
    }
    private DataTable getDetailData3()
    {
        DataTable dtResult = new DataTable();
        dtResult.Columns.Add("門市編號", typeof(string));
        dtResult.Columns.Add("門市名稱", typeof(string));

        return dtResult;
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        bindMasterData();
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
        bindMasterData();
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

    protected void ASPxPageControl1_ActiveTabChanged(object source, DevExpress.Web.ASPxTabControl.TabControlEventArgs e)
    {
        int a = this.ASPxPageControl1.ActiveTabIndex;

        switch (a + 1)
        {
            case 1:
                bindDetailData1();
                break;

            case 2:
                bindDetailData2();
                break;

            case 3:
                bindDetailData3();
                break;

            default:
                break;
        }
    }

}
