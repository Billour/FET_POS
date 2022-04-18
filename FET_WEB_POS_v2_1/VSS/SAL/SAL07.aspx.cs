using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Advtek.Utility;
using DevExpress.Web.ASPxGridView;

public partial class VSS_SAL_SAL07 : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack && !IsCallback)
        {
            //繫結空的資料表，以顯示表頭欄位
            gvMaster.DataSource = GetEmptyDataTable();
            gvMaster.DataBind();
        }
    }

    protected void BindMasterData()
    {
        DataTable dtResult = new DataTable();
        dtResult = getMasterData();
        gvMaster.DataSource = dtResult;
        gvMaster.DataBind();
    }

    protected void BindDetailData()
    {
        DataTable dtResult = new DataTable();
        dtResult = getDetailData();
        gvDetail.DataSource = dtResult;
        gvDetail.DataBind();
    }

    protected void BindDetailData2()
    {
        DataTable dtResult = new DataTable();
        dtResult = getDetailData2();
        ASPxGridView1.DataSource = dtResult;
        ASPxGridView1.DataBind();
    }

    private DataTable GetEmptyDataTable()
    {
        DataTable dtResult = new DataTable();
        dtResult.Columns.Add("項次", typeof(string));
        dtResult.Columns.Add("促銷類型", typeof(string));
        dtResult.Columns.Add("促銷代號", typeof(string));
        dtResult.Columns.Add("促銷名稱", typeof(string));
        dtResult.Columns.Add("開始日期", typeof(string));
        dtResult.Columns.Add("結束日期", typeof(string));

        return dtResult;
    }

    private DataTable getMasterData()
    {
        DataTable dtResult = GetEmptyDataTable();

        DataRow NewRow = dtResult.NewRow();
        NewRow["項次"] = "1";
        NewRow["促銷類型"] = "組合促銷";
        NewRow["促銷代號"] = "AEN00ENQYDN3";
        NewRow["促銷名稱"] = "3G-大雙網765美國運通";
        NewRow["開始日期"] = "2010/07/08";
        NewRow["結束日期"] = "2010/07/18";
        dtResult.Rows.Add(NewRow);

        NewRow = dtResult.NewRow();
        NewRow["項次"] = "2";
        NewRow["促銷類型"] = "啟用促銷";
        NewRow["促銷代號"] = "081";
        NewRow["促銷名稱"] = "081續約升級-750~149";
        NewRow["開始日期"] = "2010/07/06";
        NewRow["結束日期"] = "2010/07/28";
        dtResult.Rows.Add(NewRow);


        for (int i = 3; i < 8; i++)
        {
            NewRow = dtResult.NewRow();
            NewRow["項次"] = i.ToString();
            NewRow["促銷類型"] = "促銷類型" + i.ToString();
            NewRow["促銷代號"] = "促銷代號" + i.ToString();
            NewRow["促銷名稱"] = "促銷名稱" + i.ToString();
            NewRow["開始日期"] = "2010/07/08";
            NewRow["結束日期"] = "2010/07/18";
            dtResult.Rows.Add(NewRow);
        }
        return dtResult;
    }

    private DataTable getDetailData()
    {
        DataTable dtResult = new DataTable();
        dtResult.Columns.Add("項次", typeof(string));
        dtResult.Columns.Add("商品料號", typeof(string));
        dtResult.Columns.Add("商品名稱", typeof(string));
        dtResult.Columns.Add("商品群", typeof(string));
        dtResult.Columns.Add("促銷價", typeof(int));
        dtResult.Columns.Add("庫存量", typeof(int));

        DataRow NewRow = dtResult.NewRow();
        NewRow["項次"] = "1";
        NewRow["商品料號"] = "100100207";
        NewRow["商品名稱"] = "Motorola W220銀簡";
        NewRow["商品群"] = "1";
        NewRow["促銷價"] = 4500;
        NewRow["庫存量"] = 5;
        dtResult.Rows.Add(NewRow);

        NewRow = dtResult.NewRow();
        NewRow["項次"] = "2";
        NewRow["商品料號"] = "150300037";
        NewRow["商品名稱"] = "Nokia6288白簡配(3G)";
        NewRow["商品群"] = "1";
        NewRow["促銷價"] = 2950;
        NewRow["庫存量"] = 2;
        dtResult.Rows.Add(NewRow);

        NewRow = dtResult.NewRow();
        NewRow["項次"] = "3";
        NewRow["商品料號"] = "152800003";
        NewRow["商品名稱"] = "Sharp WX-T91黑";
        NewRow["商品群"] = "2";
        NewRow["促銷價"] = 2360;
        NewRow["庫存量"] = 1;
        dtResult.Rows.Add(NewRow);

        NewRow = dtResult.NewRow();
        NewRow["項次"] = "4";
        NewRow["商品料號"] = "152800044";
        NewRow["商品名稱"] = "Sharp WX-T82黑";
        NewRow["商品群"] = "2";
        NewRow["促銷價"] = 5260;
        NewRow["庫存量"] = 3;
        dtResult.Rows.Add(NewRow);

        return dtResult;
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

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        BindMasterData();
    }

    protected void btnSearchD_Click(object sender, EventArgs e)
    {
        BindDetailData2();
    }

    protected void gvMaster_PageIndexChanged(object sender, EventArgs e)
    {
       ASPxGridView grid = sender as ASPxGridView;
       gvMaster.DataSource = getMasterData();
       gvMaster.DataBind();
    }

    protected void gvMaster_FocusedRowChanged(object sender, EventArgs e)
    {
        if (gvMaster.FocusedRowIndex >= 0)
        {

            gvDetail.Visible = true;
            BindDetailData();
        }
    }

    protected void ASPxPageControl1_ActiveTabChanged(object source, DevExpress.Web.ASPxTabControl.TabControlEventArgs e)
    {
        switch (this.ASPxPageControl1.ActiveTabIndex)
        {
            case 0:
                BindMasterData();
                break;

            case 1:
                break;

            default:
                break;
        }
    }
}
