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
public partial class VSS_SAL_SAL13 : BasePage
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
        BindDetailData2();
    }

    private DataTable GetMasterData()
    {
        DataTable dtResult = new DataTable();
        dtResult.Columns.Add("項次", typeof(string));
        dtResult.Columns.Add("折扣料號", typeof(string));
        dtResult.Columns.Add("折扣名稱", typeof(string));
        dtResult.Columns.Add("折扣金額", typeof(int));
        dtResult.Columns.Add("商品折扣比率", typeof(string));
        dtResult.Columns.Add("生效起日", typeof(DateTime));
        dtResult.Columns.Add("生效訖日", typeof(DateTime));
        dtResult.Columns.Add("折扣上限次數", typeof(string));

        string[] array0 = { "賓賓有禮", "iPone折扣" };
        string[] array1 = { "在途", "已撥入" };
        Random rnd = new Random();
        for (int i = 1; i < 17; i++)
        {
            DataRow NewRow = dtResult.NewRow();
            NewRow["項次"] = i;
            NewRow["折扣料號"] = i.ToString().PadLeft(8, '0');
            NewRow["折扣名稱"] = array0[i % 2];
            NewRow["折扣金額"] = 200 * i;
            NewRow["商品折扣比率"] = "1%";
            NewRow["生效起日"] = "2010/10/12";
            NewRow["生效訖日"] = "2010/11/12";
            NewRow["折扣上限次數"] = i.ToString();

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
        Session["折扣料號"] = (sender as ASPxGridView).GetMasterRowKeyValue();
    }

    protected void BindDetailData2()
    {
        DataTable dtResult = new DataTable();
        DataTable dtResult1 = new DataTable();
        DataTable dtResult2 = new DataTable();
        DataTable dtResult3 = new DataTable();
        DataTable dtResult4 = new DataTable();
        DataTable dtResult5 = new DataTable();
        DataTable dtResult6 = new DataTable();

        dtResult = DesignatedGoods();
        dtResult1 = SpecifyStore();
        dtResult2 = PromotionCode();
        dtResult3 = CostCenter();
        dtResult4 = newdata2();
        dtResult5 = getGridViewDataCustomer1();
        dtResult6 = getGridViewDataCustomer2();

        gvMaster.DataSource = dtResult;
        gvMaster.DataBind();
        ASPxGridView1.DataSource = dtResult1;
        ASPxGridView1.DataBind();
        ASPxGridView2.DataSource = dtResult2;
        ASPxGridView2.DataBind();
        ASPxGridView3.DataSource = dtResult3;
        ASPxGridView3.DataBind();
        ASPxGridView4.DataSource = dtResult4;
        ASPxGridView4.DataBind();
        ASPxGridView5.DataSource = dtResult4;
        ASPxGridView5.DataBind();

        ASPxGridView7.DataSource = dtResult5;
        ASPxGridView7.DataBind();
        ASPxGridView6.DataSource = dtResult6;
        ASPxGridView6.DataBind();




    }

    private DataTable DesignatedGoods()
    {
        DataTable dtResult = new DataTable();
        dtResult.Columns.Add("商品料號", typeof(string));
        dtResult.Columns.Add("商品名稱", typeof(string));

        DataRow NewRow = dtResult.NewRow();
        NewRow["商品料號"] = "100100100";
        NewRow["商品名稱"] = "iPhone4";
        dtResult.Rows.Add(NewRow);

        NewRow = dtResult.NewRow();
        NewRow["商品料號"] = "100100210";
        NewRow["商品名稱"] = "HTC Desire HD";
        dtResult.Rows.Add(NewRow);

        return dtResult;
    }

    private DataTable SpecifyStore()
    {
        DataTable dtResult = new DataTable();
        dtResult.Columns.Add("門市編號", typeof(string));
        dtResult.Columns.Add("門市名稱", typeof(string));
        dtResult.Columns.Add("區域別", typeof(string));
        dtResult.Columns.Add("折扣上限次數", typeof(string));

        DataRow NewRow = dtResult.NewRow();
        NewRow["門市編號"] = "2911";
        NewRow["門市名稱"] = "華納";
        NewRow["區域別"] = "北一區";
        NewRow["折扣上限次數"] = "5";
        dtResult.Rows.Add(NewRow);

        NewRow = dtResult.NewRow();
        NewRow["門市編號"] = "2101";
        NewRow["門市名稱"] = "遠企";
        NewRow["區域別"] = "北一區";
        NewRow["折扣上限次數"] = "3";
        dtResult.Rows.Add(NewRow);

        NewRow = dtResult.NewRow();
        NewRow["門市編號"] = "9999";
        NewRow["門市名稱"] = "公館";
        NewRow["區域別"] = "北一區";
        NewRow["折扣上限次數"] = "5";
        dtResult.Rows.Add(NewRow);

        return dtResult;
    }

    private DataTable PromotionCode()
    {
        DataTable dtResult = new DataTable();
        dtResult.Columns.Add("促銷代號", typeof(string));
        dtResult.Columns.Add("促銷名稱", typeof(string));

        DataRow NewRow = dtResult.NewRow();
        NewRow["促銷代號"] = "促銷代號001";
        NewRow["促銷名稱"] = "促銷名稱111";
        dtResult.Rows.Add(NewRow);

        NewRow = dtResult.NewRow();
        NewRow["促銷代號"] = "促銷代號002";
        NewRow["促銷名稱"] = "促銷名稱112";
        dtResult.Rows.Add(NewRow);

        return dtResult;
    }

    private DataTable CostCenter()
    {
        DataTable dtResult = new DataTable();
        dtResult.Columns.Add("成本中心", typeof(string));
        dtResult.Columns.Add("商品分類", typeof(string));
        dtResult.Columns.Add("會計科目", typeof(string));
        dtResult.Columns.Add("金額", typeof(string));
        dtResult.Columns.Add("備註", typeof(string));

        DataRow NewRow = dtResult.NewRow();
        NewRow["成本中心"] = "6905";
        NewRow["商品分類"] = "手機類";
        NewRow["會計科目"] = "540101";
        NewRow["金額"] = "5000";
        NewRow["備註"] = "";
        dtResult.Rows.Add(NewRow);

        NewRow = dtResult.NewRow();
        NewRow["成本中心"] = "6905";
        NewRow["商品分類"] = "配件類";
        NewRow["會計科目"] = "540104";
        NewRow["金額"] = "3000";
        NewRow["備註"] = "";
        dtResult.Rows.Add(NewRow);

        NewRow = dtResult.NewRow();
        NewRow["成本中心"] = "6905";
        NewRow["商品分類"] = "3C";
        NewRow["會計科目"] = "540601";
        NewRow["金額"] = "3000";
        NewRow["備註"] = "";
        dtResult.Rows.Add(NewRow);

        NewRow = dtResult.NewRow();
        NewRow["成本中心"] = "6662";
        NewRow["商品分類"] = "新啟用";
        NewRow["會計科目"] = "816002";
        NewRow["金額"] = "5000";
        NewRow["備註"] = "";
        dtResult.Rows.Add(NewRow);

        return dtResult;
    }


    private DataTable newdata2()
    {
        DataTable dtResult = new DataTable();
        dtResult.Columns.Add("商品料號", typeof(string));
        dtResult.Columns.Add("商品名稱", typeof(string));

        DataRow NewRow = dtResult.NewRow();
        NewRow["商品料號"] = "300300100";
        NewRow["商品名稱"] = "公仔(紅)";
        dtResult.Rows.Add(NewRow);

        NewRow = dtResult.NewRow();
        NewRow["商品料號"] = "300300200";
        NewRow["商品名稱"] = "公仔(藍)";
        dtResult.Rows.Add(NewRow);

        return dtResult;
    }

    private DataTable getGridViewDataCustomer1()
    {

        DataTable dtResult = new DataTable();

        dtResult.Columns.Add("ARPB金額(起)", typeof(int));
        dtResult.Columns.Add("ARPB金額(訖)", typeof(int));


        for (int i = 0; i < 2; i++)
        {
            DataRow dtMasterRow = dtResult.NewRow();

            dtMasterRow["ARPB金額(起)"] = 500 + i;
            dtMasterRow["ARPB金額(訖)"] = 500 + i + 10;

            dtResult.Rows.Add(dtMasterRow);
        }

        return dtResult;
    }


    private DataTable getGridViewDataCustomer2()
    {

        DataTable dtResult = new DataTable();
        dtResult.Columns.Add("客戶門號", typeof(int));


        for (int i = 0; i < 3; i++)
        {
            DataRow dtMasterRow = dtResult.NewRow();
            dtMasterRow["客戶門號"] = "090000000" + i;

            dtResult.Rows.Add(dtMasterRow);
        }

        return dtResult;
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

    }

    protected void grid_HtmlRowCreated(object sender, ASPxGridViewTableRowEventArgs e)
    {

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

    }
    protected void btnXlsExport_Click(object sender, EventArgs e)
    {

    }


    protected void rbCustomer_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (rbCustomer.SelectedValue == "客戶等級")
        {
            this.trgv1.Visible = true;
            this.trgv2.Visible = false;
        }
        else
        {
            this.trgv1.Visible = false;
            this.trgv2.Visible = true;
        }
    }

}
