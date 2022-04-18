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
public partial class VSS_CONS_CON08 : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string dno = Request.QueryString["dno"] == null ? "" : Request.QueryString["dno"].ToString().Trim();
        this.ViewState["dno"] = dno;

        if (!IsPostBack)
        {
            if (this.ViewState["dno"].ToString() == "")
            {
                // 繫結空的資料表，以顯示表頭欄位
                gvMaster.DataSource = GetEmptyDataTable();
                gvMaster.DataBind();
            }
            else
            {
                bindMasterData();
                //gvMaster.Rows[5].ForeColor = System.Drawing.Color.SaddleBrown;
            }

        }
        else
        {
            DataTable dtResult = ViewState["gvMaster"] as DataTable;
            if (dtResult != null)
            {
                gvMaster.DataSource = dtResult;
                gvMaster.DataBind();
            }
        }

        //if (!IsPostBack)
        //{
        //   //bindMasterData(0);
        //   //bindDetailData(0);
        //   Button3.Attributes.Add("onclick", "javascript:openwindow('CON08_1.aspx','600','400');");
        //}
    }

    protected void bindMasterData()
    {
        DataTable dtResult = new DataTable();
        dtResult = getMasterData();
        ViewState["gvMaster"] = dtResult;
        gvMaster.DataSource = dtResult;
        gvMaster.DataBind();
    }

    private DataTable GetEmptyDataTable()
    {
        DataTable dtResult = new DataTable();
        dtResult.Columns.Add("廠商代號", typeof(string));
        dtResult.Columns.Add("廠商名稱", typeof(string));
        dtResult.Columns.Add("門市編號", typeof(string));
        dtResult.Columns.Add("門市名稱", typeof(string));
        dtResult.Columns.Add("商品料號", typeof(string));
        dtResult.Columns.Add("商品名稱", typeof(string));
        dtResult.Columns.Add("實際訂購量", typeof(string));
        dtResult.Columns.Add("異常原因", typeof(string));
        return dtResult;
    }

    private DataTable getMasterData()
    {
        DataTable dtResult = GetEmptyDataTable();

        string[] SupplierNo = { "AC", "OO" };
        string[] SupplierName = { "全虹", "震旦" };

        string[] StoreNo = { "2100", "2102", "2103" };
        string[] ProductCode = { "100100200", "100100201", "100100251", "100100252" };

        string[] straReason = { "店組不存在", "廠商不存在", "商品料號不存在", "主配數量不可為空", "主配數量應大於零", "" };

        for (int i = 0; i < 6; i++)
        {
            DataRow NewRow = dtResult.NewRow();
            NewRow["廠商代號"] = SupplierNo[i % 2];
            NewRow["廠商名稱"] = SupplierName[i % 2];
            NewRow["門市編號"] = StoreNo[i % 3];
            NewRow["門市名稱"] = "門市名稱" + (i + 1);
            NewRow["商品料號"] = ProductCode[i % 4];
            NewRow["商品名稱"] = "商品名稱" + (i + 1);

            NewRow["實際訂購量"] = (i + 1) * 10;
            NewRow["異常原因"] = straReason[i];

            dtResult.Rows.Add(NewRow);
        }

        return dtResult;
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        bindMasterData();
    }

    protected void gvMaster_HtmlRowPrepared(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewTableRowEventArgs e)
    {
        if (e.RowType != GridViewRowType.Data) return;
        if (e.GetValue("異常原因").ToString() != string.Empty)
            e.Row.ForeColor = System.Drawing.Color.Red;
    }
}
