using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using DevExpress.Web.ASPxGridView;

public partial class VSS_ORD_ORD14 : System.Web.UI.Page
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
        dtResult = GetMasterData();
        gvMaster.DataSource = dtResult;
        gvMaster.DataBind();

    }

    private void binDetailData()
    {
        this.ASPxPageControl1.Visible = true;
        gvDetail.DataSource = GetDetailData();
        gvDetail.DataBind();
    }

    private DataTable GetEmptyDataTable()
    {
        DataTable dtResult = new DataTable();
        dtResult.Columns.Add("卡片群組", typeof(string));
        dtResult.Columns.Add("開始日期", typeof(DateTime));
        dtResult.Columns.Add("結束日期", typeof(DateTime));
        dtResult.Columns.Add("更新日期", typeof(string));
        dtResult.Columns.Add("更新人員", typeof(string));
        return dtResult;
    }

    private DataTable GetMasterData()
    {
        DataTable dtResult = GetEmptyDataTable();

        DataRow NewRow = dtResult.NewRow();
        NewRow["卡片群組"] = "2G";
        NewRow["開始日期"] = DateTime.Today.Date.AddDays(-10);
        NewRow["結束日期"] = DateTime.Today.Date;
        NewRow["更新日期"] = DateTime.Now;
        NewRow["更新人員"] = "王小明";
        dtResult.Rows.Add(NewRow);

        NewRow = dtResult.NewRow();
        NewRow["卡片群組"] = "3G";
        NewRow["開始日期"] = DateTime.Today.Date.AddDays(-10);
        NewRow["結束日期"] = DateTime.Today.Date;
        NewRow["更新日期"] = DateTime.Now;
        NewRow["更新人員"] = "王小明";
        dtResult.Rows.Add(NewRow);

        NewRow = dtResult.NewRow();
        NewRow["卡片群組"] = "Postpaid";
        NewRow["開始日期"] = DateTime.Today.Date.AddDays(-10);
        NewRow["結束日期"] = DateTime.Today.Date;
        NewRow["更新日期"] = DateTime.Now;
        NewRow["更新人員"] = "王小明";
        dtResult.Rows.Add(NewRow);

        NewRow = dtResult.NewRow();
        NewRow["卡片群組"] = "Prepaid";
        NewRow["開始日期"] = DateTime.Today.Date.AddDays(-10);
        NewRow["結束日期"] = DateTime.Today.Date;
        NewRow["更新日期"] = DateTime.Now;
        NewRow["更新人員"] = "王小明";
        dtResult.Rows.Add(NewRow);

        return dtResult;
    }

    private DataTable GetDetailData()
    {
        DataTable dtResult = new DataTable();
        dtResult.Columns.Add("商品料號", typeof(string));
        dtResult.Columns.Add("商品名稱", typeof(string));

        DataRow NewRow = dtResult.NewRow();
        NewRow["商品料號"] = "100100100";
        NewRow["商品名稱"] = "A手機";
        dtResult.Rows.Add(NewRow);

        return dtResult;
    }

    protected void gvMaster_FocusedRowChanged(object sender, EventArgs e)
    {
        if (gvMaster.FocusedRowIndex >= 0)
        {
            binDetailData();
            ASPxPageControl1.Visible = true;
        }
    }

    protected void gvMaster_HtmlRowPrepared(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewTableRowEventArgs e)
    {
        if (e.RowType == GridViewRowType.Data)
        {
            List<object> keyValues = this.gvMaster.GetSelectedFieldValues(gvMaster.KeyFieldName);
            foreach (string key in keyValues)
            {
                if (key == e.GetValue(gvMaster.KeyFieldName).ToString())
                {
                    if (key == gvMaster.GetRowValues(gvMaster.FocusedRowIndex, gvMaster.KeyFieldName).ToString())
                    {
                        e.Row.BackColor = System.Drawing.ColorTranslator.FromHtml("#999999");
                        e.Row.ForeColor = System.Drawing.ColorTranslator.FromHtml("#FFFFFF");
                    }
                    else
                    {
                        e.Row.BackColor = System.Drawing.ColorTranslator.FromHtml("#FFFFFF");
                        e.Row.ForeColor = System.Drawing.ColorTranslator.FromHtml("#000000");
                    }

                }
            }
        }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        bindMasterData();
    }

}
