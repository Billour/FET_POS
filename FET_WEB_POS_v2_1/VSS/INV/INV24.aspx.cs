using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using DevExpress.Web.ASPxGridView;
using DevExpress.Web.ASPxEditors;

public partial class VSS_INV_INV24 : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            // 繫結空的資料表，以顯示表頭欄位
            ////gvMaster.DataSource = GetEmptyDataTable();
            ////gvMaster.DataBind();
            bindMasterData();
        }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        bindMasterData();
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
        dtResult.Columns.Add("移撥單號", typeof(string));
        dtResult.Columns.Add("撥入門市", typeof(string));
        dtResult.Columns.Add("移出日期", typeof(string));
        dtResult.Columns.Add("撥入日期", typeof(string));
        dtResult.Columns.Add("移撥狀態", typeof(string));
        dtResult.Columns.Add("更新人員", typeof(string));
        dtResult.Columns.Add("更新日期", typeof(string));
        return dtResult;

    }
    private DataTable getMasterData()
    {
        DataTable dtResult = GetEmptyDataTable();

        //string[] ary1 = {"暫存","在途","巳撥入"};
        for (int i = 1; i < 16; i++)
        {
            DataRow NewRow = dtResult.NewRow();
            NewRow["移撥單號"] = "ST2101-100815" + i.ToString("000");
            NewRow["撥入門市"] = "撥入門市" + i.ToString("000");
            //NewRow["移出日期"] = DateTime.Now.ToString("yyyy/MM/dd");
            NewRow["移出日期"] = "2010/08/15";
            NewRow["撥入日期"] = "";
            NewRow["移撥狀態"] = "在途";
            NewRow["更新人員"] = "王小明";
            NewRow["更新日期"] = "2010/07/13";
            dtResult.Rows.Add(NewRow);
        }
        return dtResult;
    }
    protected void bindDetailData()
    {
        DataTable dtgvDetail = new DataTable();
        dtgvDetail = getDetailData();
        ViewState["gvDetail"] = dtgvDetail;
        //gvDetail.DataSource = dtgvDetail;
        //gvDetail.DataBind();
    }
    private DataTable getDetailData()
    {
        DataTable gvDetail = new DataTable();
        gvDetail.Columns.Clear();
        gvDetail.Columns.Add("商品料號", typeof(string));
        gvDetail.Columns.Add("商品名稱", typeof(string));
        gvDetail.Columns.Add("移出數量", typeof(string));
        gvDetail.Columns.Add("IMEI控管", typeof(string));
        gvDetail.Columns.Add("IMEI", typeof(string));
        gvDetail.Columns.Add("IMEI2", typeof(string));

        //Random rnd = new Random();

        //for (int i = 1; i < 5; i++)
        //{
        //    DataRow gvDetailRow = gvDetail.NewRow();
        //    gvDetailRow["商品料號"] = "ST2101-100815001";
        //    gvDetailRow["商品名稱"] = "商品名稱" +  i.ToString("000");
        //    gvDetailRow["移出數量"] = i;
        //    gvDetailRow["IMEI控管"] = ((i % 2) == 0) ? "0" : "1";
        //    gvDetailRow["IMEI"] = "1234567890123" + i.ToString();
        //    gvDetailRow["IMEI2"] = "," + gvDetailRow["IMEI"];
        //    for (int j = 0; j < 3; j++)
        //    {
        //        gvDetailRow["IMEI2"] = gvDetailRow["IMEI2"].ToString() + "\r\n"
        //            + "1234567890123" + j.ToString();
        //    }

        //    gvDetail.Rows.Add(gvDetailRow);
        //}
        DataRow gvDetailRow = gvDetail.NewRow();
        gvDetailRow["商品料號"] = "152301011";
        gvDetailRow["商品名稱"] = "商品名稱000";
        gvDetailRow["移出數量"] = 1;
        gvDetailRow["IMEI控管"] = "1";
        gvDetailRow["IMEI"] = "1";
        gvDetailRow["IMEI2"] = "12345678901231";
        gvDetail.Rows.Add(gvDetailRow);

        gvDetailRow = gvDetail.NewRow();
        gvDetailRow["商品料號"] = "152301012";
        gvDetailRow["商品名稱"] = "商品名稱001";
        gvDetailRow["移出數量"] = 2;
        gvDetailRow["IMEI控管"] = "0";
        gvDetailRow["IMEI"] = "0";
        gvDetail.Rows.Add(gvDetailRow);

        gvDetailRow = gvDetail.NewRow();
        gvDetailRow["商品料號"] = "152301013";
        gvDetailRow["商品名稱"] = "商品名稱002";
        gvDetailRow["移出數量"] = 3;
        gvDetailRow["IMEI控管"] = "1";
        gvDetailRow["IMEI"] = "3";
        gvDetailRow["IMEI2"] = "12345678901240";
        for (int j = 1; j < 3; j++)
        {
            gvDetailRow["IMEI2"] = gvDetailRow["IMEI2"].ToString() + "\r\n"
                + "1234567890124" + j.ToString();
        }
        gvDetail.Rows.Add(gvDetailRow);

        gvDetailRow = gvDetail.NewRow();
        gvDetailRow["商品料號"] = "152301014";
        gvDetailRow["商品名稱"] = "商品名稱003";
        gvDetailRow["移出數量"] = 4;
        gvDetailRow["IMEI控管"] = "1";
        gvDetailRow["IMEI"] = "4";
        gvDetailRow["IMEI2"] = "12345678901250";
        for (int j = 1; j < 4; j++)
        {
            gvDetailRow["IMEI2"] = gvDetailRow["IMEI2"].ToString() + "\r\n" + "1234567890125" + j.ToString();
        }
        gvDetail.Rows.Add(gvDetailRow);

        gvDetailRow = gvDetail.NewRow();
        gvDetailRow["商品料號"] = "152301015";
        gvDetailRow["商品名稱"] = "商品名稱004";
        gvDetailRow["移出數量"] = 1;
        gvDetailRow["IMEI控管"] = "0";
        gvDetailRow["IMEI"] = "0";
        gvDetail.Rows.Add(gvDetailRow);
        return gvDetail;

    }

    protected void gvMaster_HtmlRowCreated1(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewTableRowEventArgs e)
    {
        if (e.RowType == GridViewRowType.Detail)
        {
            // 繫結明細資料表           
            ASPxGridView detailGrid = e.Row.FindChildControl<ASPxGridView>("gvDetail");
            detailGrid.DataSource = getDetailData();
            detailGrid.DataBind();
        }
    }

    protected void gvMaster_HtmlRowPrepared(object sender, ASPxGridViewTableRowEventArgs e)
    {
        if (e.RowType == GridViewRowType.Data)
        {
            if (e.GetValue("移撥狀態").ToString() == "在途")
            {
                e.Row.ForeColor = System.Drawing.Color.Red;
            }
        }
    }

    protected void gvDetail_HtmlRowCreated(object sender, ASPxGridViewTableRowEventArgs e)
    {




        if (e.RowType == GridViewRowType.Data)
        {

            

            ASPxLabel lblIMEI = e.Row.FindChildControl<ASPxLabel>("lbl1");
            // 繫結明細資料表           
            lblIMEI.Attributes["onmouseover"] = string.Format("show('{0}');", IMEIContent(Convert.ToInt16(lblIMEI.Text)));
            lblIMEI.Attributes["onmouseout"] = "hide();";

            
            int intC_IMEI = int.Parse(e.GetValue("移出數量").ToString());
            int intS_IMEI = int.Parse(e.GetValue("IMEI").ToString());
            ASPxImage imgIMEI = e.Row.FindChildControl<ASPxImage>("imgIMEI");


            if ((intC_IMEI - intS_IMEI) == 0)
            {
                imgIMEI.ImageUrl = "~/Icon/check.png";
            }

            if ((intC_IMEI - intS_IMEI) > 0)
            {
                imgIMEI.ImageUrl = "~/Icon/non_complete.png";
            }

            //button status
            ASPxButton btnIMEI = e.Row.FindChildControl<ASPxButton>("Button10");

            if ((intC_IMEI == 1) && (intC_IMEI - intS_IMEI == 1))
            {
                btnIMEI.Enabled = false;
            }
        }
    }

    private string IMEIContent(int Count)
    {
        string IMEI_FORMAT = "<table border=\"1\">";
        for (int i = 1; i <= Count; i++)
        {
            IMEI_FORMAT += "<tr><td>7781-9441-5641-786" + i.ToString() + "</td></tr>";
        }
        IMEI_FORMAT += "</tr></table>";

        return IMEI_FORMAT;
    }

}

