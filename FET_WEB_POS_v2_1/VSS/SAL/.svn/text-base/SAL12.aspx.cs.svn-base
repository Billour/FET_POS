using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using DevExpress.Web.ASPxGridView;

public partial class VSS_SAL_SAL12 : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            DataTable dtResult = new DataTable();
            dtResult.Columns.Add("商品料號", typeof(string));
            dtResult.Columns.Add("商品名稱", typeof(string));
            dtResult.Columns.Add("商品類別", typeof(string));
            dtResult.Columns.Add("單位", typeof(string));
            dtResult.Columns.Add("檢核IMEI", typeof(string));
            dtResult.Columns.Add("自訂價格", typeof(bool));
            dtResult.Columns.Add("扣庫存", typeof(bool));
            dtResult.Columns.Add("生效日起", typeof(string));
            dtResult.Columns.Add("生效日訖", typeof(string));
            dtResult.Columns.Add("價格", typeof(int));
            dtResult.Columns.Add("廠商名稱", typeof(string));

            DataRow NewRow = dtResult.NewRow();
            NewRow["商品料號"] = "100010026";
            NewRow["商品名稱"] = "Motorola-T2688-NAD";
            NewRow["商品類別"] = "Handset - Selling";
            NewRow["單位"] = "SET";
            NewRow["檢核IMEI"] = "銷售時控管";
            NewRow["自訂價格"] = false;
            NewRow["扣庫存"] = true;
            NewRow["生效日起"] = "2010/07/01";
            NewRow["生效日訖"] = "2010/07/01";
            NewRow["價格"] = 3500;
            NewRow["廠商名稱"] = "全虹";
            dtResult.Rows.Add(NewRow);

            NewRow = dtResult.NewRow();
            NewRow["商品料號"] = "319900315";
            NewRow["商品名稱"] = "時尚手機套";
            NewRow["商品類別"] = "Accessory - Spare Part/Premium";
            NewRow["單位"] = "EA";
            NewRow["檢核IMEI"] = "銷售時控管";
            NewRow["自訂價格"] = true;
            NewRow["扣庫存"] = false;
            NewRow["生效日起"] = "2010/07/02";
            NewRow["生效日訖"] = "2010/07/03";
            NewRow["價格"] = 6000;
            NewRow["廠商名稱"] = "全虹";
            dtResult.Rows.Add(NewRow);

            NewRow = dtResult.NewRow();
            NewRow["商品料號"] = "100010026";
            NewRow["商品名稱"] = "Motorola-T2688-NAD";
            NewRow["商品類別"] = "Handset - Selling";
            NewRow["單位"] = "SET";
            NewRow["檢核IMEI"] = "銷售時控管";
            NewRow["自訂價格"] = false;
            NewRow["扣庫存"] = true;
            NewRow["生效日起"] = "2010/07/01";
            NewRow["生效日訖"] = "2010/07/01";
            NewRow["價格"] = 3500;
            NewRow["廠商名稱"] = "全虹";
            dtResult.Rows.Add(NewRow);

            grid.DataSource = dtResult;
            grid.DataBind();
        }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {

    }

    protected void BindDetailData2()
    {
        DataTable dtResult = new DataTable();
        dtResult = getDetailData2();
        ASPxGridView1.DataSource = dtResult;
        ASPxGridView1.DataBind();
    }

    private DataTable getDetailData2()
    {
        DataTable dtResult = new DataTable();
        dtResult.Columns.Add("折扣料號", typeof(string));
        dtResult.Columns.Add("折扣名稱", typeof(string));
        dtResult.Columns.Add("折扣金額", typeof(int));
        dtResult.Columns.Add("贈品/加價購", typeof(string));

        DataRow NewRow = dtResult.NewRow();
        NewRow["折扣料號"] = "00000001";
        NewRow["折扣名稱"] = "NNNNN";
        NewRow["折扣金額"] = 100;
        NewRow["贈品/加價購"] = "";
        dtResult.Rows.Add(NewRow);

        NewRow = dtResult.NewRow();
        NewRow["折扣料號"] = "00000002";
        NewRow["折扣名稱"] = "贈品";
        NewRow["折扣金額"] = 0;
        NewRow["贈品/加價購"] = "公仔(黑), 公仔(紅)";
        dtResult.Rows.Add(NewRow);

        NewRow = dtResult.NewRow();
        NewRow["折扣料號"] = "00000003";
        NewRow["折扣名稱"] = "加價購";
        NewRow["折扣金額"] = 500;
        NewRow["贈品/加價購"] = "皮套";
        dtResult.Rows.Add(NewRow);


        return dtResult;

    }

    protected void btnSearchD_Click(object sender, EventArgs e)
    {
        BindDetailData2();
    }
}
