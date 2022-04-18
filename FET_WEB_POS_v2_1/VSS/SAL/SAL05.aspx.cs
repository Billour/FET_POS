using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using DevExpress.Web.ASPxGridView;

public partial class VSS_SAL_SAL05 : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
       if (!IsPostBack && !IsCallback)
        {

        }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
       Session["DataSource"] = getMasterData();

        bindMasterData();
        divContent.Visible = true;
    }

    protected void bindMasterData()
    {
        DataTable dtResult = new DataTable();
        dtResult = getMasterData();
        gvMaster.DataSource = dtResult;
        gvMaster.DataBind();
    }
    private DataTable GetEmptyDataTable()
    {
        DataTable dtResult = new DataTable();
        dtResult.Columns.Add("項次", typeof(string));
        dtResult.Columns.Add("狀態", typeof(string));
        dtResult.Columns.Add("申請日期", typeof(string));
        //dtResult.Columns.Add("服務屬性", typeof(string));
        dtResult.Columns.Add("服務類別", typeof(string));
        dtResult.Columns.Add("應收總金額", typeof(int));
        dtResult.Columns.Add("客戶門號", typeof(string));
        dtResult.Columns.Add("銷售人員", typeof(string));
        return dtResult;
    }
    private DataTable getMasterData()
    {
        DataTable dtResult = GetEmptyDataTable();

        string[] array0 = { "未結帳","巳取消" };
        string[] array1 = { "IA", "IA", "IA", "HRS", "Payment", "IA" };
        //string[] array2 = { "全球卡", "換補卡", "2轉3", "新啟用", "續約", "代收", "維修", "網購", "預購" };
        string[] array2 = { "新啟用", "續約", "續約", "維修", "代收", "續約" };
        for (int i = 0; i < 6; i++)
        {
            DataRow NewRow = dtResult.NewRow();
            NewRow["項次"] = i + 1;
            NewRow["狀態"] = array0[0];
            NewRow["申請日期"] = "2010/07/01";
            //NewRow["服務屬性"] = array1[i];
            NewRow["服務類別"] = array2[i];
            NewRow["應收總金額"] = 3000;
            NewRow["客戶門號"] = "091536214" + i;
            NewRow["銷售人員"] = "王小明";
            dtResult.Rows.Add(NewRow);
        }
        return dtResult;
    }

    private DataTable GetDetailData()
    {
        DataTable dtResult = new DataTable();
        dtResult.Columns.Add("項次", typeof(string));
        dtResult.Columns.Add("促銷代號", typeof(string));
        dtResult.Columns.Add("促銷名稱", typeof(string));
        dtResult.Columns.Add("商品料號", typeof(string));
        dtResult.Columns.Add("商品名稱", typeof(string));
        dtResult.Columns.Add("卡片序號(SIM)", typeof(string));
        dtResult.Columns.Add("金額", typeof(string));

        for (int i = 0; i < 8; i++)
        {
            DataRow NewRow = dtResult.NewRow();
            NewRow["項次"] = i + 1;
            NewRow["促銷代號"] = "A0" + i;
            NewRow["促銷名稱"] = "大雙網促銷";
            NewRow["商品料號"] = "20102852" + i;
            NewRow["商品名稱"] = string.Format("BenQ S{0}20i(銀,簡)", i);
            NewRow["卡片序號(SIM)"] = "234313353453" + i;
            NewRow["金額"] = 1000 * (i + 1);
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
    protected void detailGrid_DataSelect(object sender, EventArgs e)
    {
       Session["項次"] = (sender as ASPxGridView).GetMasterRowKeyValue();
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
    protected void gvMaster_PageIndexChanged(object sender, EventArgs e)
    {
       ASPxGridView grid = sender as ASPxGridView;
       grid.DataSource = getMasterData();
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
    protected void combinedPaymentButton_Click(object sender, EventArgs e)
    {
        if (Request.QueryString["s"] == "1")
        {
            Response.Redirect("~/VSS/SAL/SAL11.aspx?s=1");
        }
        else
        {
            Response.Redirect("~/VSS/SAL/SAL01.aspx");
        }
    }
}
