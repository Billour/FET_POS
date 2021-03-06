using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Advtek.Utility;
using DevExpress.Web.ASPxGridView;

public partial class VSS_SAL07_SAL07 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack && !IsCallback)
        {
            // 繫結空的資料表，以顯示表頭欄位
            //gvMaster.DataSource = GetEmptyDataTable();
            //gvMaster.DataBind();
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
    private DataTable GetEmptyDataTable()
    {
        DataTable dtResult = new DataTable();
        dtResult.Columns.Add("促銷代號", typeof(string));
        dtResult.Columns.Add("促銷名稱", typeof(string));
        dtResult.Columns.Add("開始日期", typeof(string));
        dtResult.Columns.Add("結束日期", typeof(string));
        dtResult.Columns.Add("促銷類別", typeof(string));
        dtResult.Columns.Add("商品料號", typeof(string));
        dtResult.Columns.Add("商品名稱", typeof(string));
        dtResult.Columns.Add("商品群", typeof(string));
        dtResult.Columns.Add("促銷價", typeof(int));

        dtResult.Columns.Add("庫存量", typeof(string));
        dtResult.Columns.Add("金融卡", typeof(string));
        dtResult.Columns.Add("HG", typeof(string));
        dtResult.Columns.Add("更新人員", typeof(string));
        dtResult.Columns.Add("更新日期", typeof(string));

        return dtResult;
    }

    private DataTable getMasterData()
    {
        DataTable dtResult = GetEmptyDataTable();

        DataRow NewRow = dtResult.NewRow();
        NewRow["促銷代號"] = "1";
        NewRow["促銷名稱"] = "促銷名稱1";
        NewRow["開始日期"] = "2010/07/08";
        NewRow["結束日期"] = "2010/07/18";
        NewRow["促銷類別"] = "促銷類別A";
        NewRow["商品料號"] = "商品料號A";
        NewRow["商品名稱"] = "商品名稱1";
        NewRow["商品群"] = "商品群1";

        NewRow["促銷價"] = 10 ;
        NewRow["庫存量"] = "0";
        NewRow["金融卡"] = "0";
        NewRow["HG"] = "0";
        NewRow["更新人員"] = "王大寶";
        NewRow["更新日期"] = "2010/07/08";
        dtResult.Rows.Add(NewRow);

        NewRow = dtResult.NewRow();
        NewRow["促銷代號"] = "2";
        NewRow["促銷名稱"] = "促銷名稱2";
        NewRow["開始日期"] = "2010/07/06";
        NewRow["結束日期"] = "2010/07/28";
        NewRow["促銷類別"] = "促銷類別B";
        NewRow["商品料號"] = "商品料號B";
        NewRow["商品名稱"] = "商品名稱2";
        NewRow["商品群"] = "商品群2";
        NewRow["促銷價"] = 10 * 2;
        NewRow["庫存量"] = "200";
        NewRow["金融卡"] = "500";
        NewRow["HG"] = "500";
        NewRow["更新人員"] = "王大寶";
        NewRow["更新日期"] = "2010/7/6";
        dtResult.Rows.Add(NewRow);


        for (int i = 3; i < 8; i++)
        {
            NewRow = dtResult.NewRow();
            NewRow["促銷代號"] = i.ToString();
            NewRow["促銷名稱"] = "促銷名稱" + i.ToString();
            NewRow["開始日期"] = "2010/07/08";
            NewRow["結束日期"] = "2010/07/18";
            NewRow["促銷類別"] = "促銷類別" + i.ToString();
            NewRow["商品料號"] = "商品料號" + i.ToString();
            NewRow["商品名稱"] = "商品名稱" + i.ToString();
            NewRow["商品群"] = "商品群" + i.ToString();
            NewRow["促銷價"] = 10 * (i + 1);
            NewRow["庫存量"] = "100";
            NewRow["金融卡"] = "1000";
            NewRow["HG"] = "0";
            NewRow["更新人員"] = "王大寶";
            NewRow["更新日期"] = "2010/07/08";
            dtResult.Rows.Add(NewRow);
        }
        return dtResult;
    }


    protected void btnSearch_Click(object sender, EventArgs e)
    {
        bindMasterData();

    }


    /// <summary>
    /// 主GridView的分頁變更事件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gvMaster_PageIndexChanged(object sender, EventArgs e)
    {
       ASPxGridView grid = sender as ASPxGridView;
       gvMaster.DataSource = getMasterData();
       gvMaster.DataBind();
    }
}
