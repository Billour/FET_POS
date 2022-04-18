using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class VSS_INV03_INV03 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            // 繫結空的資料表，以顯示表頭欄位
            gvMaster.DataSource = GetEmptyDataTable();
            gvMaster.DataBind();
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
    //private DataTable getMasterData()
    private DataTable GetEmptyDataTable()
    {
        DataTable dtResult = new DataTable();
        dtResult.Columns.Add("區域別", typeof(string));
        dtResult.Columns.Add("商品編號", typeof(string));
        dtResult.Columns.Add("商品名稱", typeof(string));
        dtResult.Columns.Add("門市編號", typeof(string));
        dtResult.Columns.Add("門市名稱", typeof(string));
        dtResult.Columns.Add("出貨倉別", typeof(string));
        dtResult.Columns.Add("數量", typeof(string));
        return dtResult;
    }

    private DataTable getMasterData()
    {
        DataTable dtResult = GetEmptyDataTable();
        string[] ary1 = { "銷售倉", "展示倉", "維修倉", "租賃倉"};
        string[] ary2 = { "北一區", "北二區", "中二區", "南一區"};
        string[] ary3 = { "1", "2", "3", "4","5" };

        Random rnd = new Random();

        for (int i = 0; i <= 16; i++)
        {
            DataRow NewRow = dtResult.NewRow();
            NewRow["區域別"] = "北一區";//ary2[i % 4];
            NewRow["商品編號"] = rnd.Next(10010010, 99010010); //"10010010";
            NewRow["商品名稱"] = "商品名稱" + i;
            NewRow["門市編號"] = "2101";
            NewRow["門市名稱"] = "遠企";
            NewRow["出貨倉別"] = "銷售倉";// ary1[i % 4];
            NewRow["數量"] = rnd.Next(1, 10);
            dtResult.Rows.Add(NewRow);

        }

            return dtResult;
        
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        bindMasterData();
    }



    #region 分頁相關 (共用)
    protected void GridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        //此函式可共用
        GridView gridview = sender as GridView;
        gridview.PageIndex = e.NewPageIndex;

        DataTable dt = ViewState[gridview.ID] as DataTable ?? null;//GridView的資料要存於ViewState以方便共用此Function
        gridview.DataSource = dt;
        gridview.DataBind();
    }
    protected void btnGoToIndex_Click(object sender, EventArgs e)
    {
        //此函式可共用
        GridView gridview = (sender as Button).Parent.Parent.Parent.Parent as GridView;
        TextBox textbox = (sender as Button).Parent.FindControl("tbGoToIndex") as TextBox;
        string strIndex = textbox.Text.Trim();
        int index = 0;
        if (int.TryParse(strIndex, out index))
        {
            index = index - 1;
            if (index >= 0 && index <= gridview.PageCount - 1)
            {
                gridview.PageIndex = index;
                DataTable dt = ViewState[gridview.ID] as DataTable ?? null;//GridView的資料要存於ViewState以方便共用此Function
                gridview.DataSource = dt;
                gridview.DataBind();
            }
            else
            {
                textbox.Text = string.Empty;
            }
        }
        else
        {
            textbox.Text = string.Empty;
        }
    }
    #endregion 
}
