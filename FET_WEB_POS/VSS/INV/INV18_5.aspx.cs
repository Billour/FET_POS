using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Advtek.Utility;

public partial class VSS_INV_INV18_5 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //bindEmptyData();
            bindgvMaster();
            lblOrderNo.Text = Request.QueryString["No"];
        }
    }
    protected void bindEmptyData()
    {

        DataTable dtResult = new DataTable();

        gvMaster.DataSource = dtResult;
        gvMaster.DataBind();


    }
    protected void bindgvMaster()
    {
        DataTable dtGvMaster = new DataTable();
        dtGvMaster = getMasterData();
        ViewState["gvMaster"] = dtGvMaster;
        gvMaster.DataSource = dtGvMaster;
        gvMaster.DataBind();
    }

    private DataTable getMasterData()
    {
        DataTable dtMaster = new DataTable();
        dtMaster.Columns.Clear();
        dtMaster.Columns.Add("項次", typeof(int));
        dtMaster.Columns.Add("商品編號", typeof(string));
        dtMaster.Columns.Add("商品名稱", typeof(string));
        dtMaster.Columns.Add("來源倉1", typeof(int));
        dtMaster.Columns.Add("來源倉2", typeof(string));
        dtMaster.Columns.Add("目的倉1", typeof(int));
        dtMaster.Columns.Add("目的倉2", typeof(string));
        dtMaster.Columns.Add("調整量", typeof(int));
        dtMaster.Columns.Add("調整原因", typeof(string));


        List<string> ss = new List<string>
        {
            "可銷售倉",
            "拆封倉",
            "維修倉"
        };

        Random rnd = new Random();


        for (int i = 1; i < 10; i++)
        {
            DataRow dtMasterRow = dtMaster.NewRow();
            dtMasterRow["項次"] = i;
            dtMasterRow["商品編號"] = "A000" + i;
            dtMasterRow["商品名稱"] = "商品名稱" + i;
            dtMasterRow["來源倉1"] = rnd.Next(0, 2);
            dtMasterRow["來源倉2"] = ss[Convert.ToInt32(dtMasterRow["來源倉1"])];
            dtMasterRow["目的倉1"] = rnd.Next(0, 2);
            dtMasterRow["目的倉2"] = ss[Convert.ToInt32(dtMasterRow["目的倉1"])];
            dtMasterRow["調整量"] = 1000 * (i + 1);

            dtMasterRow["調整原因"] = "調整原因" + i;

            dtMaster.Rows.Add(dtMasterRow);
        }
        return dtMaster;

    }



    private DataTable getGvProdDetailData()
    {
        DataTable dtProdDetail = new DataTable();
        dtProdDetail.Columns.Clear();
        dtProdDetail.Columns.Add("商品編號", typeof(string));
        dtProdDetail.Columns.Add("商品名稱", typeof(string));
        dtProdDetail.Columns.Add("數量", typeof(int));

        for (int i = 1; i < 2; i++)
        {
            DataRow dtProdDetailRow = dtProdDetail.NewRow();
            dtProdDetailRow["商品編號"] = "A000" + i;
            dtProdDetailRow["商品名稱"] = "商品名稱" + i;
            dtProdDetailRow["數量"] = 100 * (i + 1);
            dtProdDetail.Rows.Add(dtProdDetailRow);
        }
        return dtProdDetail;

    }



    protected void btnNew_Click(object sender, EventArgs e)
    {
        //divContent.Style["display"] = "";
        bindgvMaster();



    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        lblOrderNo.Text = "SA2011-1008001";//"GG000001";

    }

    protected void gvMaster_RowEditing(object sender, GridViewEditEventArgs e)
    {
        GridView gridview = sender as GridView;
        //設定編輯欄位
        gridview.EditIndex = e.NewEditIndex;
        //Bind原查詢資料
        DataTable dt = ViewState["gvMaster"] as DataTable;
        gridview.DataSource = dt;
        gridview.DataBind();
    }
    protected void gvMaster_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        GridView gridview = sender as GridView;

        //取得資料

        //更新資料庫

        //取消編輯狀態
        gridview.EditIndex = -1;

        //Bind新資料(重取資料)
        bindgvMaster();
    }
    protected void gvMaster_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        GridView gridview = sender as GridView;
        gridview.EditIndex = -1;
        //Bind原查詢資料
        DataTable dt = ViewState["gvMaster"] as DataTable;
        gridview.DataSource = dt;
        gridview.DataBind();
    }

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
}
