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

public partial class VSS_INV_INV06 : BasePage
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
        string[] array1 = { "", "", "", "", "2010/08/31" };
        string[] array2 = { "未完成", "未完成", "未完成", "未完成", "已完成" };
        string[] array3 = { "", "", "", "", "王小明" };
        //string[] array4 = { "", "", "", "", "2010/08/31" };
        DataTable dtResult = GetEmptyDataTable();
        for (int i = 0; i <= 5; i++)
        {
            DataRow NewRow = dtResult.NewRow();
            NewRow["退倉單號"] = "HR100818" + i.ToString("000");
            NewRow["退倉開始日"] = "2010/07/01";
            NewRow["退倉結束日"] = "2010/10/10";
            NewRow["退倉日"] = array1[i % 5];
            NewRow["退倉狀態"] = array2[i % 5];
            NewRow["更新人員"] = array3[i % 5]; ;
            //"人員" + i.ToString("00");
            NewRow["更新日期"] = array1[i % 5]; ;
                //"2010/07/10";
            dtResult.Rows.Add(NewRow);
        }

               return dtResult;
    }

    private DataTable getDetailData()
    {

        DataTable dtResult = new DataTable();
        dtResult.Columns.Add("項次", typeof(string));
        dtResult.Columns.Add("商品編號", typeof(string));
        dtResult.Columns.Add("商品名稱", typeof(string));
        dtResult.Columns.Add("IMEI控管", typeof(bool));
        dtResult.Columns.Add("帳上庫存量", typeof(string));
        dtResult.Columns.Add("未拆封數量", typeof(string));
        dtResult.Columns.Add("已拆封數量", typeof(string));
        dtResult.Columns.Add("退倉數量", typeof(string));
        dtResult.Columns.Add("IMEI", typeof(string));
        dtResult.Columns.Add("差異量", typeof(string));
        dtResult.Columns.Add("ERP驗退日期", typeof(string));
        dtResult.Columns.Add("ERP驗退單號", typeof(string));
        dtResult.Columns.Add("驗退數量", typeof(string));

        for (int i = 1; i <= 6; i++)
        {
            DataRow NewRow = dtResult.NewRow();
            if (i == 3)
            {

                NewRow["項次"] = i;
                NewRow["商品編號"] = "100100100" + i.ToString("000");
                NewRow["商品名稱"] = i.ToString("00") + "手機";
                NewRow["IMEI控管"] = true;
                NewRow["帳上庫存量"] = 2 * i + i;
                NewRow["未拆封數量"] = i;
                NewRow["已拆封數量"] = i;
                //NewRow["退倉數量"] = Convert.ToInt32(NewRow["未拆封數量"]) + Convert.ToInt32(NewRow["已拆封數量"]);
                NewRow["IMEI"] = "3";
                //NewRow["差異量"] = Convert.ToInt32(NewRow["帳上庫存量"]) - Convert.ToInt32(NewRow["退倉數量"]); ;
                NewRow["ERP驗退日期"] = "";
                NewRow["ERP驗退單號"] = "";
                NewRow["驗退數量"] = "";

            }
            else
            {
                //DataRow NewRow = dtResult.NewRow();
                NewRow["項次"] = i;
                NewRow["商品編號"] = "100100100" + i.ToString("000");
                NewRow["商品名稱"] = i.ToString("00") + "手機";
                NewRow["IMEI控管"] = false;
                NewRow["帳上庫存量"] = 2 * i + i;
                NewRow["未拆封數量"] = i;
                NewRow["已拆封數量"] = i;
                //NewRow["退倉數量"] = Convert.ToInt32(NewRow["未拆封數量"]) + Convert.ToInt32(NewRow["已拆封數量"]);
                NewRow["IMEI"] = "0";
                //NewRow["差異量"] = "0" ;

                NewRow["ERP驗退日期"] = "";
                //"2010/08/01";
                NewRow["ERP驗退單號"] = "";
                //"ERP000" + i.ToString("000");
                NewRow["驗退數量"] = "";
                //2 * i + i;
                //dtResult.Rows.Add(NewRow);
            }
            NewRow["退倉數量"] = Convert.ToInt32(NewRow["未拆封數量"]) + Convert.ToInt32(NewRow["已拆封數量"]);
            NewRow["差異量"] = Convert.ToInt32(NewRow["帳上庫存量"]) - Convert.ToInt32(NewRow["退倉數量"]);

            dtResult.Rows.Add(NewRow);

        }
        return dtResult;
    }

    

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        bindMasterData();
        
    }

    protected void CommandButton_Click(Object sender, CommandEventArgs e)
    {

        //gvMaster.Selection.UnselectAll();
        string s = gvMaster.GetRowValuesByKeyValue(e.CommandArgument.ToString(), "退倉狀態").ToString();
        if (s == "已完成")
        {
            gvDetail.Visible = true;
            bindDetailData();
        }
        else {
             Response.Redirect("~/VSS/INV/INV07.aspx"); 
        }
        //gvMaster.Selection.SetSelectionByKey(e.CommandArgument, true);
        //gvDetail.Visible = true;
        //bindDetailData();

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
            //DataRowView row = gvMaster.GetRow(e.VisibleIndex) as DataRowView;
            string s = gvMaster.GetRowValues(e.VisibleIndex, "退倉狀態").ToString();   
            //string s = row["退倉狀態"].ToString();
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
        ASPxGridView gvDetail = sender as ASPxGridView;
        gvDetail.DataSource = getMasterData();
        gvDetail.DataBind();
    }

    protected void gvDetail_HtmlRowCreated(object sender, ASPxGridViewTableRowEventArgs e)
    {
        if (e.RowType == GridViewRowType.Data)
        {
            //DataRowView Rows = (DataRowView)gvDetail.GetRow(e.VisibleIndex);
            //bool check = bool.Parse(Rows["IMEI控管"].ToString());
            bool check = bool.Parse(gvDetail.GetRowValues(e.VisibleIndex, "IMEI控管").ToString());
            ASPxButton btn1 = (ASPxButton)gvDetail.FindRowCellTemplateControl(e.VisibleIndex, (GridViewDataColumn)gvDetail.Columns[8], "Button2");

            if (check == false)
            {

                btn1.Enabled = false;
            }
        }
    }

    /// <summary>
    /// 明細GridView的分頁變更事件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gvDetail_PageIndexChanged(object sender, EventArgs e)
    {
        ASPxGridView gvDetail = sender as ASPxGridView;
        gvDetail.DataSource = getDetailData();
        gvDetail.DataBind();
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
