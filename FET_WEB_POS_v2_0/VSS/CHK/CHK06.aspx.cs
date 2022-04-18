using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Advtek.Utility;
using System.Collections;
using DevExpress.Web;
using DevExpress.Web.ASPxGridView;
using DevExpress.Web.ASPxPager;

public partial class VSS_CHK_CHK06 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        
        if (!IsPostBack)
        {

        }

     
    }

    protected void bindMasterData()
    {
        grid.DataSource = getMasterData();
        grid.DataBind();
    }

    private DataTable GetEmptyDataTable()
    {
        DataTable dtResult = new DataTable();
        dtResult.Columns.Add("對帳日期", typeof(string));
        dtResult.Columns.Add("合庫入帳", typeof(string));
        dtResult.Columns.Add("POS現金帳", typeof(string));
        dtResult.Columns.Add("NCCC信用卡入帳", typeof(string));
        dtResult.Columns.Add("POS信用卡帳", typeof(string));
        dtResult.Columns.Add("異常原因", typeof(string));
        return dtResult;
    }

    private DataTable getMasterData()
    {
        DataTable dtResult = GetEmptyDataTable();

        string[] Date = { "2010/01/04", "2010/01/05","2010/01/06" };
        string[] TotalPay = { "21,756,795", "15,756,795", "10,010,801" };

        string[] PosCash = { "10,410,801", "12,756,795", "10,010,801" };
        string[] NcccCredit = { "5,021,258", "3,021,258", "5,290,728"};
        string[] PosCredit = { "5,021,258", "3,015,258", "4,290,728" };

        string[] straReason = { "現金帳差不平", "現金、信用卡帳差不平", "信用卡帳差不平"};

        for (int i = 0; i < 3; i++)
        {
            DataRow NewRow = dtResult.NewRow();
            NewRow["對帳日期"] = Date[i % 3];
            NewRow["合庫入帳"] = TotalPay[i % 3];
            NewRow["POS現金帳"] = PosCash[i % 3];
            NewRow["NCCC信用卡入帳"] = NcccCredit[i % 3];
            NewRow["POS信用卡帳"] = PosCredit[i % 3];
            NewRow["異常原因"] = straReason[i % 3];
            dtResult.Rows.Add(NewRow);
        }

        return dtResult;
    }
    protected void bindDetailData()
    {
        detailGrid.DataSource = getDetailData();
        detailGrid.DataBind();
    }
    private DataTable getDetailData()
    {
        DataTable gvDetail = new DataTable();
        gvDetail.Columns.Clear();
        gvDetail.Columns.Add("對帳日期", typeof(string));
        gvDetail.Columns.Add("門市編號", typeof(string));
        gvDetail.Columns.Add("門市名稱", typeof(string));
        gvDetail.Columns.Add("合庫入帳", typeof(string));
        gvDetail.Columns.Add("POS現金帳", typeof(string));
        gvDetail.Columns.Add("NCCC信用卡入帳", typeof(string));
        gvDetail.Columns.Add("POS信用卡帳", typeof(string));
        gvDetail.Columns.Add("異常原因", typeof(string));


        string[] StoreNo = { "2101", "2104", "2012","2025","2405","2911","2306","2501","2127","2111" };
        string[] StonoName = { "遠企門市", "天母門市", "", "", "北新門市", "華納門市", "斗六門市", "台南民生門市", "中和連城門市", "新竹門市" };
        string[] TotalPay = { "103,121", "201,000", "101,122", "", "", "128,000", "", "111,220", "290,001", "123,456" };

        string[] PosCash = { "104,121", "201,000", "", "", "111,000", "128,000", "311,000", "", "290,001", "" };
        string[] NcccCredit = { "105,121", "195,000", "", "100,000", "", "211,000", "", "60,000", "50,000", "246,801" };
        string[] PosCredit = { "105,121", "185,000", "", "", "211,000", "228,000", "251,000", "60,000", "", "" };

        string[] straReason = { "現金帳差不平", "信用卡帳差不平", "門市編號不存在", "門市編號不存在", "合庫入帳金額不允許為空值", 
                                "NCCC信用卡入帳金額不允許為空值", "合庫入帳與NCCC入帳，不允許為空值", "POS無現金帳", "POS無信用卡帳", "POS無現金、信用卡帳" };

        for (int i = 0; i < 10; i++)
        {
            DataRow gvDetailRow = gvDetail.NewRow();
            gvDetailRow["對帳日期"] = "2010/01/04";
            gvDetailRow["門市編號"] = StoreNo[i % 10];
            gvDetailRow["門市名稱"] = StonoName[i % 10];
            gvDetailRow["合庫入帳"] = TotalPay[i % 10]; ;
            gvDetailRow["POS現金帳"] = PosCash[i % 10]; ;
            gvDetailRow["NCCC信用卡入帳"] = NcccCredit[i % 10]; ;
            gvDetailRow["POS信用卡帳"] = PosCredit[i % 10]; ;
            gvDetailRow["異常原因"] = straReason[i % 10]; ;
          
            gvDetail.Rows.Add(gvDetailRow);
        }
        return gvDetail;
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        bindMasterData();
        bindDetailData();
        //Label2.Visible = true;
    }

    protected void grid_PageIndexChanged(object sender, EventArgs e)
    {
        ASPxGridView grid = sender as ASPxGridView;
        grid.DataSource = getMasterData();
        grid.DataBind();
    }


    protected void detailGrid_PageIndexChanged(object sender, EventArgs e)
    {
        ASPxGridView grid = sender as ASPxGridView;
        grid.DataSource = getDetailData();
        grid.DataBind();
    }

    //protected void gvMaster_RowDataBound(object sender, GridViewRowEventArgs e)
    //{
    //    if (e.Row.RowType == DataControlRowType.DataRow)
    //    {
    //        Label lb1 = (Label)e.Row.Cells[7].FindControl("Label1");

    //        TextBox tb1 = (TextBox)e.Row.Cells[0].FindControl("TextBox1");
    //        TextBox tb2 = (TextBox)e.Row.Cells[2].FindControl("TextBox2");
    //        TextBox tb3 = (TextBox)e.Row.Cells[4].FindControl("TextBox3");

    //        if (lb1.Text != "")
    //        {
    //            tb1.ForeColor = System.Drawing.Color.Red;
    //            tb2.ForeColor = System.Drawing.Color.Red;
    //            tb3.ForeColor = System.Drawing.Color.Red;
    //            lb1.ForeColor = System.Drawing.Color.Red;

    //            for (int i = 0; i < e.Row.Cells.Count - 1; i++)
    //            {
    //                e.Row.Cells[i].ForeColor = System.Drawing.Color.Red;
    //            }

    //        }
    //    }

    //}

}
